Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class SocketServer
    Private listener As TcpListener
    Private isRunning As Boolean = False
    Private connectedClients As New List(Of TcpClient)

    Public Event DataReceived(data As String)
    Public Event ClientDisconnected(ip As String)
    Public Event ClientCountChanged(count As Integer)
    Public Sub StartServer(port As Integer)
        Try
            listener = New TcpListener(IPAddress.Any, port)
            listener.Start()
            isRunning = True

            Dim thread As New Thread(AddressOf ListenForClients)
            thread.IsBackground = True
            thread.Start()

            Console.WriteLine("Server started on port " & port)
        Catch ex As Exception
            Console.WriteLine("Lỗi khi khởi động Socket: " & ex.Message)
        End Try
    End Sub

    Private Sub ListenForClients()
        While isRunning
            Try
                Dim client = listener.AcceptTcpClient()

                SyncLock connectedClients
                    connectedClients.Add(client)
                    Console.WriteLine("Client connected: " & client.Client.RemoteEndPoint.ToString())
                    Console.WriteLine("Tổng số client: " & connectedClients.Count)
                    RaiseEvent ClientCountChanged(connectedClients.Count)
                End SyncLock

                Dim clientThread As New Thread(Sub() HandleClient(client))
                clientThread.IsBackground = True
                clientThread.Start()
            Catch ex As Exception
                Console.WriteLine("Lỗi khi nhận kết nối client: " & ex.Message)
            End Try
        End While
    End Sub

    Private Sub HandleClient(client As TcpClient)
        Try
            Dim stream = client.GetStream()
            Dim buffer(1024) As Byte

            While client.Connected AndAlso IsClientConnected(client)

                If stream.DataAvailable Then
                    Dim bytesRead = stream.Read(buffer, 0, buffer.Length)
                    If bytesRead > 0 Then
                        Dim data = Encoding.UTF8.GetString(buffer, 0, bytesRead)
                        RaiseEvent DataReceived(data)
                    End If
                End If
                Thread.Sleep(100)
            End While
        Catch ex As Exception
            Console.WriteLine("Lỗi khi xử lý dữ liệu client: " & ex.Message)
        Finally
            SyncLock connectedClients
                If connectedClients.Contains(client) Then
                    connectedClients.Remove(client)
                    RaiseEvent ClientDisconnected(client.Client.RemoteEndPoint.ToString())
                    RaiseEvent ClientCountChanged(connectedClients.Count)
                End If
            End SyncLock

            client.Close()
        End Try
    End Sub


    Public Sub SendToAllClients(message As String)
        Dim data As Byte() = Encoding.UTF8.GetBytes(message)

        SyncLock connectedClients
            For Each client In connectedClients.ToList()
                Try
                    If client IsNot Nothing AndAlso client.Connected Then
                        Dim stream As NetworkStream = client.GetStream()
                        If stream.CanWrite Then
                            stream.Write(data, 0, data.Length)
                        End If
                    End If
                Catch ex As Exception
                    Console.WriteLine("Lỗi khi gửi dữ liệu tới client: " & ex.Message)
                End Try
            Next
        End SyncLock
    End Sub
    Private Function IsClientConnected(client As TcpClient) As Boolean
        Try
            If client Is Nothing OrElse Not client.Client.Connected Then Return False

            ' Kiểm tra xem socket có đóng hay không
            Dim poll = client.Client.Poll(1000, SelectMode.SelectRead)
            Dim available = (client.Client.Available = 0)

            Return Not (poll AndAlso available)
        Catch
            Return False
        End Try
    End Function

    Public Sub StopServer()
        isRunning = False
        listener?.Stop()

        SyncLock connectedClients
            For Each c In connectedClients
                c.Close()
            Next
            connectedClients.Clear()
        End SyncLock
        Console.WriteLine("Server đã dừng.")
    End Sub
End Class
