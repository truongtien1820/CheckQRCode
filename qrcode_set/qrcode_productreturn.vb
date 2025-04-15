
Imports System.Text.RegularExpressions
Imports System.IO.Ports
Imports System.Windows
Imports Oracle.ManagedDataAccess.Client



Public Class qrcode_productreturn
    Private serialPort As SerialPort
    Public ora_connectstring As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.40.31)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=topprod)));User ID=lelong;Password=lelong;"
    Public connectionString As String = "Data Source=LELONG.db;Version=3;"
    Public Trans_ora As OracleTransaction
    Public g_success As String = "Y"
    Public Sub New(port As SerialPort)
        InitializeComponent()
        Me.serialPort = port
        Me.serialPort.Open()
        ' Thêm sự kiện khi nhận dữ liệu từ SerialPort
        AddHandler Me.serialPort.DataReceived, AddressOf SerialPort_DataReceived_return
    End Sub

    Private Sub SerialPort_DataReceived_return(sender As Object, e As SerialDataReceivedEventArgs)

        ' Chạy trên UI thread để cập nhật giao diện
        Me.Invoke(Sub()
                      Dim receivedData As String = Me.serialPort.ReadExisting()
                      receivedData = Regex.Replace(receivedData, "\r\n|\n|\r|", "")
                      Dim thoigian As String = Nothing
                      Dim thoigianhoantra As String = Nothing
                      Dim macode As String = Nothing
                      Using conn As New SQLiteConnection(connectionString)
                          conn.Open()
                          Dim command = conn.CreateCommand()

                          command.CommandText = "SELECT macode,thoigian FROM laser_table WHERE   macode = '" & receivedData & "' AND loai = 'N' ;"
                          'command.Parameters.AddWithValue("@startDateTime", bdate) ' Ngày và giờ bắt đầu
                          'command.Parameters.AddWithValue("@endDateTime", edate) '
                          Using reader = command.ExecuteReader()
                              While reader.Read()
                                  macode = reader.GetString(0)
                                  thoigian = reader.GetString(1)
                                  thoigianhoantra = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")


                              End While
                          End Using

                      End Using
                      If String.IsNullOrEmpty(macode) Then
                          Me.BeginInvoke(New UpdateControlBackColorDelegate(AddressOf _mUpdateControlBackColor), New Object() {status_lb, Color.Red})
                          Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {status_lb, "Mã code chưa được quét."})
                      Else
                          ' Kiểm tra xem mã code đã tồn tại trong DataGridView chưa
                          Dim exists As Boolean = False
                          For Each row As DataGridViewRow In dgv_productreturn.Rows
                              If Not row.IsNewRow AndAlso row.Cells(0).Value IsNot Nothing AndAlso row.Cells(0).Value.ToString() = macode Then
                                  exists = True
                                  Exit For
                              End If
                          Next

                          If Not exists Then
                              Me.BeginInvoke(New UpdateControlBackColorDelegate(AddressOf _mUpdateControlBackColor), New Object() {status_lb, Color.LightGreen})
                              Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {status_lb, "Mã code đã được thêm vào danh sách."})

                              Dim rowData As Object() = {macode, thoigian, thoigianhoantra}
                              Me.Invoke(Sub()
                                            dgv_productreturn.Rows.Add(rowData)
                                        End Sub)

                              If dgv_productreturn.InvokeRequired Then
                                  dgv_productreturn.Invoke(Sub()
                                                               btn_commit.Visible = dgv_productreturn.Rows.Count > 0
                                                           End Sub)
                              Else
                                  btn_commit.Visible = dgv_productreturn.Rows.Count > 0
                              End If
                          Else
                              Me.BeginInvoke(New UpdateControlBackColorDelegate(AddressOf _mUpdateControlBackColor), New Object() {status_lb, Color.Red})
                              Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {status_lb, "Mã code đã tồn tại trong danh sách!"})
                          End If
                      End If

                  End Sub)
    End Sub

    Private Delegate Sub UpdateControlDelegate(ByVal ctrl As Control, ByVal text As String)

    Private Delegate Sub UpdateControlBackColorDelegate(ByVal ctrl As Control, ByVal color As Color)

    Private Sub _mUpdateControl(ByVal ctrl As Control, ByVal text As String)
        If ctrl.InvokeRequired Then
            ctrl.Invoke(New UpdateControlDelegate(AddressOf _mUpdateControl), ctrl, text)
        Else
            ctrl.Text = text
        End If
    End Sub
    Private Sub _mUpdateControlBackColor(ByVal ctrl As Control, ByVal color As Color)
        If ctrl.InvokeRequired Then
            ctrl.Invoke(New UpdateControlBackColorDelegate(AddressOf _mUpdateControlBackColor), ctrl, color)
        Else
            ctrl.BackColor = color
        End If
    End Sub

    Private Sub qrcode_productreturn_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        RemoveHandler Me.serialPort.DataReceived, AddressOf SerialPort_DataReceived_return
    End Sub
    Private Sub btn_commit_Click(sender As Object, e As EventArgs) Handles btn_commit.Click
        Dim totalRows As Integer = dgv_productreturn.Rows.Count
        If totalRows <= 0 Then
            MessageBox.Show("Không có dữ liệu để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Hiển thị hộp thoại xác nhận
        Dim result As DialogResult = MessageBox.Show("Bạn có chắc chắn muốn hoàn trả  " & totalRows & "  bình này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then Exit Sub

        Dim updatedRows As Integer = 0 ' Đếm số dòng đã cập nhật thành công
        Dim failedRows As Integer = 0  ' Đếm số dòng bị lỗi

        For Each row As DataGridViewRow In dgv_productreturn.Rows
            If Not row.IsNewRow Then ' Bỏ qua dòng trống (nếu có)
                Dim macode As String = row.Cells(0).Value.ToString()
                Dim thoigian As String = row.Cells(1).Value.ToString()
                Dim success As Boolean = True ' Cờ đánh dấu trạng thái cập nhật

                ' Cập nhật SQLite
                Try
                    Using conn As New SQLiteConnection(connectionString)
                        conn.Open()
                        Dim updateRowsSql As String = "UPDATE laser_table SET loai = 'X' WHERE macode = @macode AND thoigian = @thoigian;"
                        Using cmd As New SQLiteCommand(updateRowsSql, conn)
                            cmd.Parameters.AddWithValue("@macode", macode)
                            cmd.Parameters.AddWithValue("@thoigian", thoigian)
                            If cmd.ExecuteNonQuery() = 0 Then success = False ' Kiểm tra xem có dòng nào bị bỏ qua không
                        End Using
                    End Using
                Catch ex As SQLiteException
                    success = False
                End Try

                ' Cập nhật Oracle nếu SQLite thành công
                If success Then
                    Try
                        Using contpro As New OracleConnection(ora_connectstring)
                            contpro.Open()
                            Trans_ora = contpro.BeginTransaction()
                            g_success = "Y"

                            Dim updateRowsSql As String = "UPDATE tc_mtm_file SET tc_mtm004 = 'X' WHERE tc_mtm001 = :macode AND tc_mtm002 = TO_DATE(:thoigian, 'yyyy/mm/dd hh24:mi:ss')"
                            Using dr_update As New OracleCommand(updateRowsSql, contpro)
                                dr_update.Parameters.Add(":macode", OracleDbType.Varchar2).Value = macode
                                dr_update.Parameters.Add(":thoigian", OracleDbType.Varchar2).Value = thoigian
                                If dr_update.ExecuteNonQuery() = 0 Then success = False
                            End Using

                            If success Then
                                Trans_ora.Commit()
                            Else
                                Trans_ora.Rollback()
                            End If
                        End Using
                    Catch ex As OracleException
                        g_success = "N"
                        success = False
                    End Try
                End If

                ' Kiểm tra trạng thái cập nhật
                If success Then
                    updatedRows += 1
                Else
                    failedRows += 1
                End If
            End If
        Next

        ' Hiển thị thông báo tổng kết
        MessageBox.Show("Hoàn trả thành công: " & updatedRows & " bình." & vbNewLine & "Hoàn thất bại: " & failedRows & " b.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Xóa các dòng đã cập nhật thành công khỏi DataGridView
        If updatedRows > 0 Then
            For i As Integer = dgv_productreturn.Rows.Count - 1 To 0 Step -1
                If Not dgv_productreturn.Rows(i).IsNewRow Then
                    dgv_productreturn.Rows.RemoveAt(i)
                End If
            Next
        End If
    End Sub

End Class
