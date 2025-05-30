Imports System.Data.OracleClient
Imports System.IO.Ports
Imports QR014_CODECHECKING.qrcode_set
Imports System.Text.RegularExpressions
Imports QR014_CODECHECKING.My
Imports System.Net.NetworkInformation
Imports System.Data.SqlClient

Public Class qrcode_compensatory_scan
    Private serialPort As SerialPort
    Public ora_connectstring As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.40.31)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=topprod)));User ID=lelong;Password=lelong;"
    Public connectionString As String = "Data Source=LELONG.db;Version=3;"
    Public Trans_ora As OracleTransaction
    Dim db As New sqldb()
    Public g_success As String = "Y"
    Private _class As String = Nothing

    Public Sub New(port As SerialPort)
        InitializeComponent()
        Me.serialPort = port
        Me.serialPort.Open()
        ' Thêm sự kiện khi nhận dữ liệu từ SerialPort
        AddHandler Me.serialPort.DataReceived, AddressOf SerialPort_DataReceived_compensatory
        '  AddHandler dgv_productcompensatory.RowValidating, AddressOf dgv_productcompensatory_RowValidating
    End Sub

    Private Sub SerialPort_DataReceived_compensatory(sender As Object, even As SerialDataReceivedEventArgs)
        ' Chạy trên UI thread để cập nhật giao diện
        Me.Invoke(Sub()
                      Dim receivedData As String = Me.serialPort.ReadExisting()
                      receivedData = Regex.Replace(receivedData, "\r\n|\n|\r", "")
                      Dim currentRow As DataGridViewRow = Nothing

                      ' Nếu chưa có dòng nào, hoặc dòng cuối là dòng mới (IsNewRow), thì thêm dòng mới
                      If dgv_productcompensatory.Rows.Count = 0 OrElse dgv_productcompensatory.Rows(dgv_productcompensatory.Rows.Count - 1).IsNewRow Then
                          dgv_productcompensatory.Rows.Add()
                      End If

                      ' Lấy dòng cuối
                      currentRow = dgv_productcompensatory.Rows(dgv_productcompensatory.Rows.Count - 1)

                      ' Kiểm tra nếu dòng hiện tại đã hoàn thành (đủ mã khắc và BC) thì tạo dòng mới
                      Dim hasCode014 As Boolean = currentRow.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(currentRow.Cells(0).Value.ToString())
                      Dim hasCode010 As Boolean = currentRow.Cells(1).Value IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(currentRow.Cells(1).Value.ToString())

                      If hasCode014 AndAlso hasCode010 Then
                          dgv_productcompensatory.Rows.Add()
                          currentRow = dgv_productcompensatory.Rows(dgv_productcompensatory.Rows.Count - 1)
                      End If

                      ' Phân loại mã quét
                      If receivedData.StartsWith("BC") Then
                          ' Kiểm tra trùng database QRcode010
                          If db.CheckDuplicateQRCode(receivedData) = 1 Then
                              Me.BeginInvoke(New UpdateControlBackColorDelegate(AddressOf _mUpdateControlBackColor), status_lb, Color.Red)
                              Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), status_lb, "QRcode010 đã tồn tại.")
                              Return
                          End If

                          ' Kiểm tra nếu đã có mã BC trong dòng này
                          If currentRow.Cells(1).Value IsNot Nothing Then
                              Me.BeginInvoke(New UpdateControlBackColorDelegate(AddressOf _mUpdateControlBackColor), status_lb, Color.Orange)
                              Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), status_lb, "Dòng hiện tại đã có mã QR010")
                              Return
                          End If

                          ' Ghi mã BC vào cột thứ 2
                          currentRow.Cells(1).Value = receivedData
                          Me.BeginInvoke(New UpdateControlBackColorDelegate(AddressOf _mUpdateControlBackColor), status_lb, Color.LightGreen)
                          Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), status_lb, "Đã thêm QRcode010.")

                      Else
                          ' Kiểm tra trùng database QRcode014
                          If db.CheckDuplicateQRCode014(receivedData) = 1 Then
                              Me.BeginInvoke(New UpdateControlBackColorDelegate(AddressOf _mUpdateControlBackColor), status_lb, Color.Red)
                              Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), status_lb, "QRcode014 đã tồn tại.")
                              Return
                          End If

                          ' Kiểm tra nếu đã có mã khắc trong dòng này
                          If currentRow.Cells(0).Value IsNot Nothing Then
                              Me.BeginInvoke(New UpdateControlBackColorDelegate(AddressOf _mUpdateControlBackColor), status_lb, Color.Orange)
                              Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), status_lb, "Đã có mã khác lazer.")
                              Return
                          End If

                          ' Ghi mã khắc vào cột đầu tiên
                          currentRow.Cells(0).Value = receivedData
                          Me.BeginInvoke(New UpdateControlBackColorDelegate(AddressOf _mUpdateControlBackColor), status_lb, Color.LightGreen)
                          Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), status_lb, "Đã thêm QRcode014.")
                      End If

                      ' Nếu cả hai mã đã có, thì ghi thời gian vào cột thứ 3
                      hasCode014 = currentRow.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(currentRow.Cells(0).Value.ToString())
                      hasCode010 = currentRow.Cells(1).Value IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(currentRow.Cells(1).Value.ToString())

                      If hasCode014 AndAlso hasCode010 Then
                          currentRow.Cells(2).Value = Date.Now.ToString("yyyy/MM/dd HH:mm:ss")
                          Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), status_lb, " OK ")
                          If MySettings.Default.code_detail = "DATA MATRIX" Then
                              _class = qrcode_set.txt_97.Text
                          Else
                              _class = qrcode_set.qr_96.Text
                          End If
                          Dim pingSender As New Ping
                          Dim pingreply As PingReply = pingSender.Send("172.16.40.31", 1000)
                          If pingreply.Status = IPStatus.Success Then
                              db.insert(currentRow.Cells(0).Value.ToString, currentRow.Cells(2).Value.ToString, 1, 1, _class, "N", currentRow.Cells(1).Value.ToString)
                              qrcode_set.insert_data(currentRow.Cells(0).Value.ToString, currentRow.Cells(2).Value.ToString, "N", currentRow.Cells(1).Value.ToString)
                          ElseIf pingreply.Status <> IPStatus.Success Then
                              db.insert(currentRow.Cells(0).Value.ToString, currentRow.Cells(2).Value.ToString, 0, 0, _class, "N", currentRow.Cells(1).Value.ToString)
                          End If
                          qrcode_set.server.SendToAllClients(currentRow.Cells(1).Value.ToString)
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

    Private Sub qrcode_compensatory_scan_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        RemoveHandler Me.serialPort.DataReceived, AddressOf SerialPort_DataReceived_compensatory
    End Sub
End Class