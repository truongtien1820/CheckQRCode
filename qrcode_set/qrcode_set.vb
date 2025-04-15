Imports System.Configuration
Imports System.ComponentModel
Imports System.Text
Imports System.Drawing.Bitmap
Imports QRCoder
Imports System.Text.RegularExpressions
Imports ZXing
Imports ZXing.Common
Imports System.IO.Ports
Imports System.Windows
Imports QR014_CODECHECKING.My
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SQLite
Imports System.Media
Imports System.Reflection
Imports System.IO
Imports System.Deployment.Application
Imports System.Threading.Timer
Imports System.Threading
Imports System.Drawing.Drawing2D
Imports Keyence.AutoID.SDK
Imports Microsoft.Office.Core
Imports System.Net.NetworkInformation
Imports QR014_CODECHECKING.qrcode_set
Imports Keyence.AR.VncClient
Imports ZXing.OneD
Imports Oracle.ManagedDataAccess.Client
Imports System.Net

Public Class qrcode_set
    Private serialPort As SerialPort
    Private serialPort2 As SerialPort
    Private bg_work As BackgroundWorker
    Private bg_work2 As BackgroundWorker
    Private bg_internet As BackgroundWorker
    Private m_reader As New ReaderAccessor()
    Dim db As New sqldb()
    Dim _font As Integer = 12
    Private LightTimer As Timers.Timer = Nothing
    Private OnlineStatus_Timer As Timers.Timer = Nothing
    Private autoscanner_Timer As Timers.Timer = Nothing
    Public id_ueser As String = ""
    Public ora_connectstring As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.40.31)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=topprod)));User ID=lelong;Password=lelong;"
    Public ora_connectstringtest As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.40.33)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=toptest)));User ID=lelong;Password=lelong;"
    Public flat As String = "True"
    Public Trans_ora As OracleTransaction
    Public g_success As String = "Y"
    Public limit As String = ""
    Public host As String = Dns.GetHostName() ' Lấy tên máy
    Public ipList As IPAddress() = Dns.GetHostEntry(host).AddressList
    Public local As String


    Private Sub qrcode_set_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For Each ip As IPAddress In ipList
            If ip.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then ' Lọc IP v4
                local = ip.ToString()
            End If
        Next
        Dim credentials As New NetworkCredential("install", "Lelong@2022#")
        _calamviec()
        db.delete()
        PictureBox2.Image = Nothing
        offline_count.Visible = False
        txt_noidung.Text = My.Settings.txt_noidung
        lb_code.Text = My.Settings.code_detail
        txt_usernameqr.Enabled = False
        txt_usernamematrix.Enabled = False
        db.createdb()
        LightTimer = New Timers.Timer()
        AddHandler LightTimer.Elapsed, AddressOf LightTimer_Elapsed
        OnlineStatus_Timer = New Timers.Timer()
        OnlineStatus_Timer.Interval = 3000
        autoscanner_Timer = New Timers.Timer()
        autoscanner_Timer.Interval = 3000
        AddHandler OnlineStatus_Timer.Elapsed, AddressOf OnlineStatus_Timer_Elapsed
        OnlineStatus_Timer.Start()
        AddHandler autoscanner_Timer.Elapsed, AddressOf autoscanner_Timer_Elapsed
        autoscanner_Timer.Start()
        bg_work = New BackgroundWorker()
        AddHandler bg_work.DoWork, AddressOf worker_DoWork
        bg_work2 = New BackgroundWorker()
        AddHandler bg_work2.DoWork, AddressOf backgroundWorker_bgwork2
        bg_internet = New BackgroundWorker()
        AddHandler bg_internet.DoWork, AddressOf backgroundWorker_bg_internet

        serialPort = New SerialPort()
        serialPort.PortName = "COM6"
        serialPort.BaudRate = 9600
        serialPort.DataBits = 8
        serialPort.Parity = Parity.None
        serialPort.StopBits = StopBits.One
        serialPort.ReadBufferSize = 4096

        serialPort2 = New SerialPort()
        serialPort2.PortName = "COM7"
        serialPort2.BaudRate = 9600
        serialPort2.DataBits = 8
        serialPort2.Parity = Parity.None
        serialPort2.StopBits = StopBits.One
        serialPort2.ReadBufferSize = 4096

        AddHandler serialPort.DataReceived, AddressOf SerialPort_DataReceived
        AddHandler serialPort2.DataReceived, AddressOf SerialPort_DataReceived_2
        AddHandler serialPort.PinChanged, AddressOf serialPort3_PinChanged
        AddHandler serialPort2.PinChanged, AddressOf serialPort3_PinChanged_2

        reload_layout()

        Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_count, dgv_data.Rows.Count().ToString()})


        Try
            serialPort.Open()
        Catch ex As Exception
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.stat_handlescanner, "QUÉT CẦM TAY" & vbCrLf & "Mất kết nối"})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.stat_handlescanner, Color.DarkRed})
        End Try
        Try
            serialPort2.Open()
        Catch ex As Exception
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.stat_handlescanner_2, "QUÉT CẦM TAY ZEBRA" & vbCrLf & "Mất kết nối"})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.stat_handlescanner_2, Color.DarkRed})
        End Try

        Try
            m_reader.IpAddress = MySettings.Default.IP
            m_reader.Connect(
                Sub(data)
                    BeginInvoke(New DelegateUserControl(AddressOf ReceivedDataWrite), Encoding.ASCII.GetString(data))
                End Sub
            )
            m_reader.ExecCommand("OUTON,3")
        Catch ex As Exception

        End Try
        Timer1.Start()
        lb_count_limit.Text = My.Settings.limit

        txt_8010.Text = My.Settings.matrix8010
        txt_8011.Text = My.Settings.matrix8011
        txt_90.Text = My.Settings.matrix90
        txt_91.Text = My.Settings.matrix91
        txt_92.Text = My.Settings.matrix92
        txt_93.Text = My.Settings.matrix93
        txt_94.Text = My.Settings.matrix94
        txt_95.Text = My.Settings.matrix95
        txt_96.Text = My.Settings.matrix96
        txt_97.Text = My.Settings.matrix97


        qr_90.Text = My.Settings.qrcode90
        qr_91.Text = My.Settings.qrcode91
        qr_92.Text = My.Settings.qrcode92
        qr_93.Text = My.Settings.qrcode93
        qr_94.Text = My.Settings.qrcode94
        qr_95.Text = My.Settings.qrcode95
        qr_96.Text = My.Settings.qrcode96

        'get_font_size(lb_class, 150)
        'get_font_size(lb_count_limit, 150)
        'get_font_size(lb_limit, 10)
    End Sub

    Private Sub serialPort3_PinChanged(sender As Object, e As SerialPinChangedEventArgs)
        If Not serialPort.CDHolding Then
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.stat_handlescanner, "QUÉT CẦM TAY" & vbCrLf & "Mất kết nối"})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.stat_handlescanner, Color.DarkRed})
        End If
    End Sub
    Private Sub serialPort3_PinChanged_2(sender As Object, e As SerialPinChangedEventArgs)
        If Not serialPort2.CDHolding Then
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.stat_handlescanner_2, "QUÉT CẦM TAY ZEBRA" & vbCrLf & "Mất kết nối"})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.stat_handlescanner_2, Color.DarkRed})
        End If
    End Sub
    Private Sub backgroundWorker_bgwork2(sender As Object, e As DoWorkEventArgs)
        Dim pingSender As New Ping

        Try
            Dim pingreply As PingReply = pingSender.Send(MySettings.Default.IP, 1000)

            If pingreply.Status = IPStatus.Success Then

                Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.stat_AUTOscanner, "QUÉT TỰ ĐỘNG" & vbCrLf & "Đã kết nối"})
                Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.stat_AUTOscanner, Color.Lime})


            Else
                Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.stat_AUTOscanner, "QUÉT TỰ ĐỘNG" & vbCrLf & "Mất kết nối"})
                Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.stat_AUTOscanner, Color.DarkRed})

            End If

        Catch ex As Exception
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.stat_AUTOscanner, "QUÉT TỰ ĐỘNG" & vbCrLf & "Mất kết nối"})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.stat_AUTOscanner, Color.DarkRed})
        End Try

    End Sub

    Private Sub backgroundWorker_bg_internet(sender As Object, e As DoWorkEventArgs)
        backgroundWorker_bg_internet(sender, e, offline_count)
    End Sub

    Private Sub backgroundWorker_bg_internet(sender As Object, e As DoWorkEventArgs, offline_count As Label)
        Try
            _offlinecount()
            Dim pingSender As New Ping
            Dim pingreply As PingReply = pingSender.Send("172.16.40.31", 1000)
            If pingreply.Status = IPStatus.Success Then

                Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.internetconn, "Online"})
                Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.internetconn, Color.Lime})

                If db.Count > 0 And flat = "True" Then
                    UploadDataToTipTop()

                End If
                If db.Count = 0 And flat = "True" Then
                    Me.BeginInvoke(New Action(Sub() offline_count.Visible = False))

                End If

            Else
                Me.BeginInvoke(New Action(Sub() offline_count.Visible = True))
                Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.internetconn, "Offline"})
                Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.internetconn, Color.DarkRed})
            End If
        Catch ex As Exception
            Me.BeginInvoke(New Action(Sub() offline_count.Visible = True))
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.internetconn, "Offline"})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.internetconn, Color.DarkRed})
        End Try

    End Sub

    Private Delegate Sub DelegateUserControl(str As String)
    Private Sub ReceivedDataWrite(receivedData As String)
        'Thread.Sleep(Double.Parse(MySettings.Default.delay_time) * 1000)
        'máy quét tự động
        Me.Invoke(Sub()
                      scan_content.Text = receivedData
                  End Sub)


        receivedData = Regex.Replace(receivedData, "\r\n|\n|\r|", "")
        Dim receivedData_2 As String = receivedData
        If btn_apply.Enabled = True Then
            If Not receivedData_2 = "ERROR" AndAlso MySettings.Default.code_detail = "DATA MATRIX" AndAlso receivedData_2.Substring(0, 4) <> "LONG" Then

                qrcode_set_size.qrcode_substr(receivedData_2)
                Me.Invoke(Sub()
                              txt_8010.Text = My.Settings.matrix8010
                              txt_8011.Text = My.Settings.matrix8011
                              txt_90.Text = My.Settings.matrix90
                              txt_91.Text = My.Settings.matrix91
                              txt_92.Text = My.Settings.matrix92
                              txt_93.Text = My.Settings.matrix93
                              txt_94.Text = My.Settings.matrix94
                              txt_95.Text = My.Settings.matrix95
                              txt_96.Text = My.Settings.matrix96
                              txt_97.Text = My.Settings.matrix97
                          End Sub)

            End If
            If Not receivedData_2 = "ERROR" AndAlso MySettings.Default.code_detail <> "DATA MATRIX" AndAlso receivedData_2.Substring(0, 4) = "LONG" Then

                qrcode_set_size.qrcode_substr_qr(receivedData_2)
                Me.Invoke(Sub()
                              qr_90.Text = My.Settings.qrcode90
                              qr_91.Text = My.Settings.qrcode91
                              qr_92.Text = My.Settings.qrcode92
                              qr_93.Text = My.Settings.qrcode93
                              qr_94.Text = My.Settings.qrcode94
                              qr_95.Text = My.Settings.qrcode95
                              qr_96.Text = My.Settings.qrcode96
                          End Sub)



            End If

            Dim soundPlayer As New SoundPlayer(My.Resources.sound_accept())
            soundPlayer.Play()
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.lb_msg, "Hãy áp dụng thiết lập trước..."})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.lb_msg, Color.Red})
            Return
        End If
        Me.Invoke(Sub()
                      txt_8010.BackColor = Color.White
                      txt_8011.BackColor = Color.White
                      txt_90.BackColor = Color.White
                      txt_91.BackColor = Color.White
                      txt_92.BackColor = Color.White
                      txt_93.BackColor = Color.White
                      txt_94.BackColor = Color.White
                      txt_95.BackColor = Color.White
                      txt_96.BackColor = Color.White
                      txt_97.BackColor = Color.White
                      qr_90.BackColor = Color.White
                      qr_91.BackColor = Color.White
                      qr_92.BackColor = Color.White
                      qr_93.BackColor = Color.White
                      qr_94.BackColor = Color.White
                      qr_95.BackColor = Color.White
                      qr_96.BackColor = Color.White
                  End Sub)
        Dim matrix_err As Boolean = True
        Dim qrcode_err As Boolean = True
        If receivedData_2 = "ERROR" Then
            open_Light("Red")
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "NO QRCODE."})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
            Return
        End If
        If MySettings.Default.code_detail = "DATA MATRIX" Then
            If btn_apply.Enabled = False And Not String.IsNullOrEmpty(txt_97.Text) And lb_class.Visible = False Then
                Me.Invoke(Sub()
                              Label25.Visible = True
                              lb_class.Visible = True



                          End Sub)
            ElseIf btn_apply.Enabled = False And String.IsNullOrEmpty(txt_97.Text) And lb_class.Visible = True Then
                Me.Invoke(Sub()
                              Label25.Visible = False
                              lb_class.Visible = False



                          End Sub)
            End If

        Else
            If btn_apply.Enabled = False And Not String.IsNullOrEmpty(qr_96.Text) And lb_class.Visible = False Then
                Me.Invoke(Sub()
                              Label25.Visible = True
                              lb_class.Visible = True


                          End Sub)
            ElseIf btn_apply.Enabled = False And String.IsNullOrEmpty(qr_96.Text) And lb_class.Visible = True Then
                Me.Invoke(Sub()
                              Label25.Visible = False
                              lb_class.Visible = False


                          End Sub)
            End If
            If btn_apply.Enabled = True And Not String.IsNullOrEmpty(qr_96.Text) And limit_edit.Visible = False Then
                Me.Invoke(Sub()
                              limit_edit.Visible = True

                          End Sub)
            ElseIf btn_apply.Enabled = True And Not String.IsNullOrEmpty(qr_96.Text) And limit_edit.Visible = True Then
                Me.Invoke(Sub()
                              limit_edit.Visible = False

                          End Sub)
            End If
        End If
        If MySettings.Default.code_detail = "DATA MATRIX" Then

            'Next
            Dim codelist As String()
            codelist = receivedData.Split(ChrW(29))
            Dim Index As Integer = 0
            For Each code As String In codelist
                Index += 1
                Try


                    If Index = 1 Then
                        If Not code.Equals(txt_8010.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_8010.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 2 Then
                        If Not code.Length = txt_8011.Text.Length Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_8011.BackColor = Color.Red

                                          Dim searchText As String = code
                                          Dim startIndex As Integer = Scan_content.Find(searchText)
                                          If startIndex <> -1 Then

                                              Scan_content.Select(startIndex, searchText.Length)

                                              Scan_content.SelectionColor = Color.Blue

                                          End If
                                      End Sub)
                        Else
                            Me.Invoke(Sub()
                                          Dim searchText As String = code
                                          Dim startIndex As Integer = Scan_content.Find(searchText)
                                          If startIndex <> -1 Then

                                              Scan_content.Select(startIndex, searchText.Length)

                                              Scan_content.SelectionColor = Color.Blue

                                          End If
                                      End Sub)
                        End If

                    End If
                    If Index = 3 Then
                        If Not code.Equals(txt_90.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_90.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 4 Then
                        If Not code.Equals(txt_91.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_91.BackColor = Color.Red
                                      End Sub)

                        End If

                    End If
                    If Index = 5 Then
                        If Not code.Equals(txt_92.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_92.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 6 Then
                        If Not code.Equals(txt_93.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_93.BackColor = Color.Red
                                      End Sub)

                        End If
                    End If
                    If Index = 7 Then
                        If Not code.Equals(txt_94.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_94.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 8 Then
                        If Not code.Equals(txt_95.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_95.BackColor = Color.Red
                                      End Sub)

                        End If
                    End If

                    If Index = 9 And Not String.IsNullOrEmpty(txt_96.Text) Then
                        If Not code.Equals(txt_96.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_96.BackColor = Color.Red
                                      End Sub)
                        End If
                    End If

                    If Index = 10 And Not String.IsNullOrEmpty(txt_97.Text) Then
                        If Not code.Equals(txt_97.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_97.BackColor = Color.Red
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Substring(2)
                                          lb_class.BackColor = Color.FromArgb(255, 175, 0)


                                      End Sub)
                        ElseIf code.Equals(txt_97.Text) Then
                            Me.Invoke(Sub()
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Substring(2)
                                          lb_class.BackColor = Color.FromArgb(255, 211, 182)

                                      End Sub)

                        End If


                    End If
                    If codelist.Length < 9 And Not String.IsNullOrEmpty(txt_96.Text) Then
                        matrix_err = False
                        Me.Invoke(Sub()
                                      txt_96.BackColor = Color.Red
                                  End Sub)
                    End If
                    If codelist.Length < 10 And Not String.IsNullOrEmpty(txt_97.Text) Then
                        matrix_err = False
                        Me.Invoke(Sub()
                                      txt_97.BackColor = Color.Red
                                      lb_class.Font = New Font(lb_class.Font.Name, 36, lb_class.Font.Style)
                                      lb_class.Text = "Không có Phân cấp dung lượng !"
                                      lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                  End Sub)
                    End If


                Catch ex As Exception
                    matrix_err = False
                    Exit For
                End Try
            Next
        Else

            'Next
            Dim codelist As String()
            codelist = receivedData.Split(",")
            Dim Index As Integer = 0
            For Each code As String In codelist
                Index += 1
                Try


                    If Index = 1 Then
                        If Not code.Trim(" ").Equals(qr_90.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_90.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 2 Then
                        If Not code.Trim(" ").Equals(qr_91.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_91.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 3 Then
                        If Not code.Trim(" ").Equals(qr_92.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_92.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 4 Then
                        If Not code.Trim(" ").Equals(qr_93.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_93.BackColor = Color.Red
                                      End Sub)

                        End If

                    End If
                    If Index = 5 Then
                        If Not code.Trim(" ").Equals(qr_94.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()

                                          qr_94.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 6 Then
                        If Not code.Trim(" ").Length = qr_95.Text.Length Then
                            matrix_err = False
                            Me.Invoke(Sub()

                                          qr_95.BackColor = Color.Red
                                      End Sub)
                        End If
                    End If


                    If Index = 7 And Not String.IsNullOrEmpty(qr_96.Text) Then
                        If Not code.Trim(" ").Equals(qr_96.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()

                                          qr_96.BackColor = Color.Red
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Trim(" ")
                                          lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                      End Sub)
                        ElseIf code.Trim(" ").Equals(qr_96.Text) Then
                            Me.Invoke(Sub()
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Trim(" ")
                                          lb_class.BackColor = Color.FromArgb(255, 211, 182)
                                      End Sub)

                        End If
                    End If

                    If codelist.Length = 6 And Not String.IsNullOrEmpty(qr_96.Text) Then
                        matrix_err = False
                        Me.Invoke(Sub()

                                      qr_96.BackColor = Color.Red
                                      lb_class.Font = New Font(lb_class.Font.Name, 36, lb_class.Font.Style)
                                      lb_class.Text = "Không có phân cấp dung lượng "
                                      lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                  End Sub)
                    End If

                Catch ex As Exception
                    matrix_err = False
                    Exit For
                End Try
            Next
        End If

        If Not matrix_err Or Not qrcode_err Then
            open_Light("Red")
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "Mã code không đúng."})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
            If Not bg_work.IsBusy Then
                Dim args As Tuple(Of String, String) = New Tuple(Of String, String)(receivedData_2, "E")
                bg_work.RunWorkerAsync(args)
            End If
            Return

        End If
        If Not bg_work.IsBusy Then
            Dim args As Tuple(Of String, String) = New Tuple(Of String, String)(receivedData_2, "N")
            bg_work.RunWorkerAsync(args)
        End If
        'bg_work.RunWorkerAsync(receivedData_2)
    End Sub

    Private Sub btn_scanreturn_Click(sender As Object, e As EventArgs) Handles btn_scanreturn.Click
        'Check port open/close, if Open => Close
        If serialPort IsNot Nothing AndAlso serialPort.IsOpen Then
            RemoveHandler serialPort.DataReceived, AddressOf SerialPort_DataReceived
            serialPort.Close()
        End If

        ' send port to qrcode_productreturn
        Dim frm As New qrcode_productreturn(serialPort)
        frm.ShowDialog()

        'Repert connect seriaPort
        If Not serialPort.IsOpen Then

            serialPort.Open()
        End If
        AddHandler serialPort.DataReceived, AddressOf SerialPort_DataReceived
        _calamviec()
    End Sub



    Private Sub open_Light(lightColor As String)
        Try
            Select Case lightColor
                Case "Green"
                    m_reader.ExecCommand("ALLOFF")
                    m_reader.ExecCommand("OUTON,1")
                    LightTimer.Interval = Convert.ToDouble(Double.Parse(MySettings.Default.time_ligh)) * 1000
                    LightTimer.Start()

                Case "Red"
                    m_reader.ExecCommand("ALLOFF")
                    m_reader.ExecCommand("OUTON,2")
                    LightTimer.Interval = Convert.ToDouble(Double.Parse(MySettings.Default.time_ligh)) * 1000
                    LightTimer.Start()
            End Select
        Catch
            LightTimer.Interval = Convert.ToDouble(Double.Parse(MySettings.Default.time_ligh)) * 1000
            LightTimer.Start()
        End Try
    End Sub
    Private Sub LightTimer_Elapsed(sender As Object, e As System.Timers.ElapsedEventArgs)
        Try
            m_reader.ExecCommand("ALLOFF")
            m_reader.ExecCommand("OUTON,3")
        Catch
        Finally
            LightTimer.Stop()
        End Try
    End Sub

    Private Sub OnlineStatus_Timer_Elapsed(sender As Object, e As System.Timers.ElapsedEventArgs)
        If Not bg_internet.IsBusy() Then
            bg_internet.RunWorkerAsync()

        End If
    End Sub
    Private Sub autoscanner_Timer_Elapsed(sender As Object, e As System.Timers.ElapsedEventArgs)
        If Not bg_work2.IsBusy() Then
            bg_work2.RunWorkerAsync()
        End If
    End Sub
    Sub _calamviec()
        Try
            Dim batdauca1 As DateTime = DateTime.Now.Date.AddHours(Convert.ToInt16(MySettings.Default.b_h1)).AddMinutes(Convert.ToInt16(MySettings.Default.b_m1))
            Dim ketthucca1 As DateTime = DateTime.Now.Date.AddHours(Convert.ToInt16(MySettings.Default.e_h1)).AddMinutes(Convert.ToInt16(MySettings.Default.e_m1))
            Dim batdauca2 As DateTime = DateTime.Now.Date.AddHours(Convert.ToInt16(MySettings.Default.b_h2)).AddMinutes(Convert.ToInt16(MySettings.Default.b_m2))
            Dim ketthucca2 As DateTime = DateTime.Now.Date.AddDays(1).AddHours(Convert.ToInt16(MySettings.Default.e_h2)).AddMinutes(Convert.ToInt16(MySettings.Default.e_m2))
            Dim bdate As String
            Dim edate As String
            If DateTime.Now > batdauca1 And Date.Now < ketthucca1 Then
                bdate = batdauca1.ToString("yyyy/MM/dd HH:mm:ss")
                edate = ketthucca1.ToString("yyyy/MM/dd HH:mm:ss")
            End If
            If DateTime.Now > batdauca2 And Date.Now < ketthucca2 Then
                bdate = batdauca2.ToString("yyyy/MM/dd HH:mm:ss")
                edate = ketthucca2.ToString("yyyy/MM/dd HH:mm:ss")
            End If

            Dim connectionString As String = "Data Source=LELONG.db;Version=3;"
            Dim _phanloai As String = ""
            Dim _quycach As String = ""

            If MySettings.Default.code_detail = "DATA MATRIX" Then
                _phanloai = txt_97.Text
                _quycach = txt_94.Text
            Else
                _phanloai = qr_96.Text
                _quycach = qr_92.Text
            End If
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Dim command = conn.CreateCommand()
                dgv_data.Rows.Clear()
                dgv_data_check.Rows.Clear()
                command.CommandText = "SELECT thoigian,macode,loai FROM laser_table WHERE thoigian >= '" & bdate & "' AND thoigian <= '" & edate & "' AND phanloai = '" & _phanloai & "' AND macode LIKE '%" & _quycach & "%' ;"
                'command.Parameters.AddWithValue("@startDateTime", bdate) ' Ngày và giờ bắt đầu
                'command.Parameters.AddWithValue("@endDateTime", edate) '
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        Dim thoigian As String = reader.GetString(0)
                        Dim macode As String = reader.GetString(1)
                        Dim loai As String = reader.GetString(2)
                        Dim rowData As Object() = {thoigian, macode}
                        AddDataToDataGridView(rowData)
                    End While
                    Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_count, dgv_data.Rows.Count().ToString()})
                    Sort_DGV()
                End Using
            End Using
        Catch ex As Exception
        End Try

    End Sub
    Sub _offlinecount()
        Try

            Dim connectionString As String = "Data Source=LELONG.db;Version=3;"
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {offline_count, db.Count().ToString()})

            End Using
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs)
        'Thread.Sleep(Double.Parse(MySettings.Default.delay_time) * 1000)
        'máy quét cầm tay 
        Dim receivedData As String = serialPort.ReadExisting()
        Me.Invoke(Sub()
                      scan_content.Text = receivedData
                  End Sub)
        Dim key As String = ControlChars.Cr
        Dim leght_size As Integer = receivedData.IndexOf(key)
        If leght_size > 0 Then
            receivedData = receivedData.Substring(0, leght_size)
        End If


        receivedData = Regex.Replace(receivedData, "\r\n|\n|\r|", "") 'ChrW(29)
        If btn_apply.Enabled = True Then
            If Not receivedData = "ERROR" AndAlso MySettings.Default.code_detail = "DATA MATRIX" AndAlso receivedData.Substring(0, 4) <> "LONG" Then

                qrcode_set_size.qrcode_substr(receivedData)
                Me.Invoke(Sub()
                              txt_8010.Text = My.Settings.matrix8010
                              txt_8011.Text = My.Settings.matrix8011
                              txt_90.Text = My.Settings.matrix90
                              txt_91.Text = My.Settings.matrix91
                              txt_92.Text = My.Settings.matrix92
                              txt_93.Text = My.Settings.matrix93
                              txt_94.Text = My.Settings.matrix94
                              txt_95.Text = My.Settings.matrix95
                              txt_96.Text = My.Settings.matrix96
                              txt_97.Text = My.Settings.matrix97
                          End Sub)


            End If


            If Not receivedData = "ERROR" AndAlso MySettings.Default.code_detail <> "DATA MATRIX" AndAlso receivedData.Substring(0, 4) = "LONG" Then

                qrcode_set_size.qrcode_substr_qr(receivedData)
                Me.Invoke(Sub()
                              qr_90.Text = My.Settings.qrcode90
                              qr_91.Text = My.Settings.qrcode91
                              qr_92.Text = My.Settings.qrcode92
                              qr_93.Text = My.Settings.qrcode93
                              qr_94.Text = My.Settings.qrcode94
                              qr_95.Text = My.Settings.qrcode95
                              qr_96.Text = My.Settings.qrcode96
                          End Sub)

            End If


            Dim soundPlayer As New SoundPlayer(My.Resources.sound_accept())
            soundPlayer.Play()

            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.lb_msg, "Hãy áp dụng thiết lập trước..."})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.lb_msg, Color.Red})

            Return
        End If
        Me.Invoke(Sub()

                      txt_8010.BackColor = Color.White
                      txt_8011.BackColor = Color.White
                      txt_90.BackColor = Color.White
                      txt_91.BackColor = Color.White
                      txt_92.BackColor = Color.White
                      txt_93.BackColor = Color.White
                      txt_94.BackColor = Color.White
                      txt_95.BackColor = Color.White
                      txt_96.BackColor = Color.White
                      txt_97.BackColor = Color.White
                      qr_90.BackColor = Color.White
                      qr_91.BackColor = Color.White
                      qr_92.BackColor = Color.White
                      qr_93.BackColor = Color.White
                      qr_94.BackColor = Color.White
                      qr_95.BackColor = Color.White
                      qr_96.BackColor = Color.White
                  End Sub)
        Dim receivedData_2 As String = receivedData
        If btn_apply.Enabled = True Then
            Dim soundPlayer As New SoundPlayer(My.Resources.sound_accept())
            soundPlayer.Play()

            Return

        End If
        If MySettings.Default.code_detail = "DATA MATRIX" Then
            If btn_apply.Enabled = False And Not String.IsNullOrEmpty(txt_97.Text) And lb_class.Visible = False Then
                Me.Invoke(Sub()
                              Label25.Visible = True
                              lb_class.Visible = True




                          End Sub)
            ElseIf btn_apply.Enabled = False And String.IsNullOrEmpty(txt_97.Text) And lb_class.Visible = True Then
                Me.Invoke(Sub()
                              Label25.Visible = False
                              lb_class.Visible = False



                          End Sub)
            End If

        Else
            If btn_apply.Enabled = False And Not String.IsNullOrEmpty(qr_96.Text) And lb_class.Visible = False Then
                Me.Invoke(Sub()
                              Label25.Visible = True
                              lb_class.Visible = True



                          End Sub)
            ElseIf btn_apply.Enabled = False And String.IsNullOrEmpty(qr_96.Text) And lb_class.Visible = True Then
                Me.Invoke(Sub()
                              Label25.Visible = False
                              lb_class.Visible = False

                          End Sub)
            End If

        End If
        Dim matrix_err As Boolean = True
        Dim qrcode_err As Boolean = True
        If MySettings.Default.code_detail = "DATA MATRIX" Then

            Dim codelist As String()
            codelist = receivedData.Split(ChrW(29))
            Dim Index As Integer = 0
            For Each code As String In codelist
                Index += 1
                Try


                    If Index = 1 Then
                        If Not code.Equals(txt_8010.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_8010.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 2 Then
                        If Not code.Length = txt_8011.Text.Length Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_8011.BackColor = Color.Red

                                          Dim searchText As String = code
                                          Dim startIndex As Integer = Scan_content.Find(searchText)
                                          If startIndex <> -1 Then

                                              Scan_content.Select(startIndex, searchText.Length)

                                              Scan_content.SelectionColor = Color.Blue

                                          End If
                                      End Sub)
                        Else
                            Me.Invoke(Sub()
                                          Dim searchText As String = code
                                          Dim startIndex As Integer = Scan_content.Find(searchText)
                                          If startIndex <> -1 Then

                                              Scan_content.Select(startIndex, searchText.Length)

                                              Scan_content.SelectionColor = Color.Blue

                                          End If
                                      End Sub)
                        End If

                    End If
                    If Index = 3 Then
                        If Not code.Equals(txt_90.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_90.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 4 Then
                        If Not code.Equals(txt_91.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_91.BackColor = Color.Red
                                      End Sub)

                        End If

                    End If
                    If Index = 5 Then
                        If Not code.Equals(txt_92.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_92.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 6 Then
                        If Not code.Equals(txt_93.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_93.BackColor = Color.Red
                                      End Sub)

                        End If
                    End If
                    If Index = 7 Then
                        If Not code.Equals(txt_94.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_94.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 8 Then
                        If Not code.Equals(txt_95.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_95.BackColor = Color.Red
                                      End Sub)

                        End If
                    End If

                    If Index = 9 And Not String.IsNullOrEmpty(txt_96.Text) Then
                        If Not code.Equals(txt_96.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_96.BackColor = Color.Red
                                      End Sub)
                        End If
                    End If


                    If Index = 10 And Not String.IsNullOrEmpty(txt_97.Text) Then
                        If Not code.Equals(txt_97.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_97.BackColor = Color.Red
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Substring(2)
                                          lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                      End Sub)
                        ElseIf code.Equals(txt_97.Text) Then
                            Me.Invoke(Sub()
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Substring(2)
                                          lb_class.BackColor = Color.FromArgb(255, 211, 182)
                                      End Sub)

                        End If

                    End If


                    If codelist.Length < 9 And Not String.IsNullOrEmpty(txt_96.Text) Then
                        matrix_err = False
                        Me.Invoke(Sub()
                                      txt_96.BackColor = Color.Red
                                  End Sub)
                    End If
                    If codelist.Length < 10 And Not String.IsNullOrEmpty(txt_97.Text) Then
                        matrix_err = False
                        Me.Invoke(Sub()
                                      txt_97.BackColor = Color.Red
                                      lb_class.Font = New Font(lb_class.Font.Name, 36, lb_class.Font.Style)
                                      lb_class.Text = "Không có phân cấp dung lượng "
                                      lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                  End Sub)
                    End If
                Catch ex As Exception
                    matrix_err = False
                    Exit For
                End Try
            Next
        Else

            'Next
            Dim codelist As String()
            codelist = receivedData.Split(",")
            Dim Index As Integer = 0
            For Each code As String In codelist
                Index += 1
                Try


                    If Index = 1 Then
                        If Not code.Trim(" ").Equals(qr_90.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_90.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 2 Then
                        If Not code.Trim(" ").Equals(qr_91.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_91.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 3 Then
                        If Not code.Trim(" ").Equals(qr_92.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_92.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 4 Then
                        If Not code.Trim(" ").Equals(qr_93.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_93.BackColor = Color.Red
                                      End Sub)

                        End If

                    End If
                    If Index = 5 Then
                        If Not code.Trim(" ").Equals(qr_94.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()

                                          qr_94.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 6 Then
                        If Not code.Trim(" ").Length = qr_95.Text.Length Then
                            matrix_err = False
                            Me.Invoke(Sub()

                                          qr_95.BackColor = Color.Red
                                      End Sub)
                        End If
                    End If

                    If Index = 7 And Not String.IsNullOrEmpty(qr_96.Text) Then
                        If Not code.Trim(" ").Equals(qr_96.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()

                                          qr_96.BackColor = Color.Red
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Trim(" ")
                                          lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                      End Sub)
                        ElseIf code.Trim(" ").Equals(qr_96.Text) Then
                            Me.Invoke(Sub()
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Trim(" ")
                                          lb_class.BackColor = Color.FromArgb(255, 211, 182)
                                      End Sub)

                        End If

                    End If
                    If codelist.Length = 6 And Not String.IsNullOrEmpty(qr_96.Text) Then
                        matrix_err = False
                        Me.Invoke(Sub()

                                      qr_96.BackColor = Color.Red
                                      lb_class.Font = New Font(lb_class.Font.Name, 36, lb_class.Font.Style)
                                      lb_class.Text = "Không có phân cấp dung lượng "
                                      lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                  End Sub)
                    End If
                Catch ex As Exception
                    matrix_err = False
                    Exit For
                End Try
            Next

        End If
        If Not matrix_err Then
            open_Light("Red")
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "Mã code không đúng."})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
            Dim soundPlayer As New SoundPlayer(My.Resources.sound())
            If Not bg_work.IsBusy Then
                Dim args As Tuple(Of String, String) = New Tuple(Of String, String)(receivedData_2, "E")
                bg_work.RunWorkerAsync(args)
            End If
            soundPlayer.Play()
            Return
        End If
        If Not qrcode_err Then
            open_Light("Red")
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "Mã code không đúng."})
            Dim soundPlayer As New SoundPlayer(My.Resources.sound())
            If Not bg_work.IsBusy Then
                Dim args As Tuple(Of String, String) = New Tuple(Of String, String)(receivedData_2, "E")
                bg_work.RunWorkerAsync(args)
            End If
            soundPlayer.Play()
            Return

        End If
        If Not bg_work.IsBusy Then

            Dim args As Tuple(Of String, String) = New Tuple(Of String, String)(receivedData_2, "N")
            bg_work.RunWorkerAsync(args)
        End If
        'bg_work.RunWorkerAsync(receivedData_2)

    End Sub

    Private Sub SerialPort_DataReceived_2(sender As Object, e As SerialDataReceivedEventArgs)
        Thread.Sleep(Double.Parse(MySettings.Default.delay_time) * 1000)
        'máy quét cầm tay Zebra
        Dim receivedData As String = serialPort2.ReadExisting()
        Me.Invoke(Sub()
                      Scan_content.Text = receivedData
                  End Sub)
        Dim key As String = ControlChars.Cr
        Dim leght_size As Integer = receivedData.IndexOf(key)
        If leght_size > 0 Then
            receivedData = receivedData.Substring(0, leght_size)
        End If


        receivedData = Regex.Replace(receivedData, "\r\n|\n|\r|", "") 'ChrW(29)
        If btn_apply.Enabled = True Then
            If Not receivedData = "ERROR" AndAlso MySettings.Default.code_detail = "DATA MATRIX" AndAlso receivedData.Substring(0, 4) <> "LONG" Then

                qrcode_set_size.qrcode_substr(receivedData)
                Me.Invoke(Sub()
                              txt_8010.Text = My.Settings.matrix8010
                              txt_8011.Text = My.Settings.matrix8011
                              txt_90.Text = My.Settings.matrix90
                              txt_91.Text = My.Settings.matrix91
                              txt_92.Text = My.Settings.matrix92
                              txt_93.Text = My.Settings.matrix93
                              txt_94.Text = My.Settings.matrix94
                              txt_95.Text = My.Settings.matrix95
                              txt_96.Text = My.Settings.matrix96
                              txt_97.Text = My.Settings.matrix97
                          End Sub)


            End If


            If Not receivedData = "ERROR" AndAlso MySettings.Default.code_detail <> "DATA MATRIX" AndAlso receivedData.Substring(0, 4) = "LONG" Then

                qrcode_set_size.qrcode_substr_qr(receivedData)
                Me.Invoke(Sub()
                              qr_90.Text = My.Settings.qrcode90
                              qr_91.Text = My.Settings.qrcode91
                              qr_92.Text = My.Settings.qrcode92
                              qr_93.Text = My.Settings.qrcode93
                              qr_94.Text = My.Settings.qrcode94
                              qr_95.Text = My.Settings.qrcode95
                              qr_96.Text = My.Settings.qrcode96
                          End Sub)

            End If


            Dim soundPlayer As New SoundPlayer(My.Resources.sound_accept())
            soundPlayer.Play()

            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {Me.lb_msg, "Hãy áp dụng thiết lập trước..."})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {Me.lb_msg, Color.Red})

            Return
        End If
        Me.Invoke(Sub()

                      txt_8010.BackColor = Color.White
                      txt_8011.BackColor = Color.White
                      txt_90.BackColor = Color.White
                      txt_91.BackColor = Color.White
                      txt_92.BackColor = Color.White
                      txt_93.BackColor = Color.White
                      txt_94.BackColor = Color.White
                      txt_95.BackColor = Color.White
                      txt_96.BackColor = Color.White
                      txt_97.BackColor = Color.White
                      qr_90.BackColor = Color.White
                      qr_91.BackColor = Color.White
                      qr_92.BackColor = Color.White
                      qr_93.BackColor = Color.White
                      qr_94.BackColor = Color.White
                      qr_95.BackColor = Color.White
                      qr_96.BackColor = Color.White
                  End Sub)
        Dim receivedData_2 As String = receivedData
        If btn_apply.Enabled = True Then
            Dim soundPlayer As New SoundPlayer(My.Resources.sound_accept())
            soundPlayer.Play()

            Return

        End If
        If MySettings.Default.code_detail = "DATA MATRIX" Then
            If btn_apply.Enabled = False And Not String.IsNullOrEmpty(txt_97.Text) And lb_class.Visible = False Then
                Me.Invoke(Sub()
                              Label25.Visible = True
                              lb_class.Visible = True




                          End Sub)
            ElseIf btn_apply.Enabled = False And String.IsNullOrEmpty(txt_97.Text) And lb_class.Visible = True Then
                Me.Invoke(Sub()
                              Label25.Visible = False
                              lb_class.Visible = False



                          End Sub)
            End If

        Else
            If btn_apply.Enabled = False And Not String.IsNullOrEmpty(qr_96.Text) And lb_class.Visible = False Then
                Me.Invoke(Sub()
                              Label25.Visible = True
                              lb_class.Visible = True



                          End Sub)
            ElseIf btn_apply.Enabled = False And String.IsNullOrEmpty(qr_96.Text) And lb_class.Visible = True Then
                Me.Invoke(Sub()
                              Label25.Visible = False
                              lb_class.Visible = False

                          End Sub)
            End If

        End If
        Dim matrix_err As Boolean = True
        Dim qrcode_err As Boolean = True
        If MySettings.Default.code_detail = "DATA MATRIX" Then

            Dim codelist As String()
            codelist = receivedData.Split(ChrW(29))
            Dim Index As Integer = 0
            For Each code As String In codelist
                Index += 1
                Try


                    If Index = 1 Then
                        If Not code.Equals(txt_8010.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_8010.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 2 Then
                        If Not code.Length = txt_8011.Text.Length Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_8011.BackColor = Color.Red

                                          Dim searchText As String = code
                                          Dim startIndex As Integer = Scan_content.Find(searchText)
                                          If startIndex <> -1 Then

                                              Scan_content.Select(startIndex, searchText.Length)

                                              Scan_content.SelectionColor = Color.Blue

                                          End If
                                      End Sub)
                        Else
                            Me.Invoke(Sub()
                                          Dim searchText As String = code
                                          Dim startIndex As Integer = Scan_content.Find(searchText)
                                          If startIndex <> -1 Then

                                              Scan_content.Select(startIndex, searchText.Length)

                                              Scan_content.SelectionColor = Color.Blue

                                          End If
                                      End Sub)
                        End If

                    End If
                    If Index = 3 Then
                        If Not code.Equals(txt_90.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_90.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 4 Then
                        If Not code.Equals(txt_91.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_91.BackColor = Color.Red
                                      End Sub)

                        End If

                    End If
                    If Index = 5 Then
                        If Not code.Equals(txt_92.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_92.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 6 Then
                        If Not code.Equals(txt_93.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_93.BackColor = Color.Red
                                      End Sub)

                        End If
                    End If
                    If Index = 7 Then
                        If Not code.Equals(txt_94.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_94.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 8 Then
                        If Not code.Equals(txt_95.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_95.BackColor = Color.Red
                                      End Sub)

                        End If
                    End If

                    If Index = 9 And Not String.IsNullOrEmpty(txt_96.Text) Then
                        If Not code.Equals(txt_96.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_96.BackColor = Color.Red
                                      End Sub)
                        End If
                    End If


                    If Index = 10 And Not String.IsNullOrEmpty(txt_97.Text) Then
                        If Not code.Equals(txt_97.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          txt_97.BackColor = Color.Red
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Substring(2)
                                          lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                      End Sub)
                        ElseIf code.Equals(txt_97.Text) Then
                            Me.Invoke(Sub()
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Substring(2)
                                          lb_class.BackColor = Color.FromArgb(255, 211, 182)
                                      End Sub)

                        End If

                    End If


                    If codelist.Length < 9 And Not String.IsNullOrEmpty(txt_96.Text) Then
                        matrix_err = False
                        Me.Invoke(Sub()
                                      txt_96.BackColor = Color.Red
                                  End Sub)
                    End If
                    If codelist.Length < 10 And Not String.IsNullOrEmpty(txt_97.Text) Then
                        matrix_err = False
                        Me.Invoke(Sub()
                                      txt_97.BackColor = Color.Red
                                      lb_class.Font = New Font(lb_class.Font.Name, 36, lb_class.Font.Style)
                                      lb_class.Text = "Không có phân cấp dung lượng "
                                      lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                  End Sub)
                    End If
                Catch ex As Exception
                    matrix_err = False
                    Exit For
                End Try
            Next
        Else


            'Next
            Dim codelist As String()
            codelist = receivedData.Split(",")
            Dim Index As Integer = 0
            For Each code As String In codelist
                Index += 1
                Try


                    If Index = 1 Then
                        If Not code.Trim(" ").Equals(qr_90.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_90.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 2 Then
                        If Not code.Trim(" ").Equals(qr_91.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_91.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 3 Then
                        If Not code.Trim(" ").Equals(qr_92.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_92.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 4 Then
                        If Not code.Trim(" ").Equals(qr_93.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()
                                          qr_93.BackColor = Color.Red
                                      End Sub)

                        End If

                    End If
                    If Index = 5 Then
                        If Not code.Trim(" ").Equals(qr_94.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()

                                          qr_94.BackColor = Color.Red
                                      End Sub)
                        End If

                    End If
                    If Index = 6 Then
                        If Not code.Trim(" ").Length = qr_95.Text.Length Then
                            matrix_err = False
                            Me.Invoke(Sub()

                                          qr_95.BackColor = Color.Red
                                      End Sub)
                        End If
                    End If

                    If Index = 7 And Not String.IsNullOrEmpty(qr_96.Text) Then
                        If Not code.Trim(" ").Equals(qr_96.Text) Then
                            matrix_err = False
                            Me.Invoke(Sub()

                                          qr_96.BackColor = Color.Red
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Trim(" ")
                                          lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                      End Sub)
                        ElseIf code.Trim(" ").Equals(qr_96.Text) Then
                            Me.Invoke(Sub()
                                          lb_class.Font = New Font(lb_class.Font.Name, 150, lb_class.Font.Style)
                                          lb_class.Text = code.Trim(" ")
                                          lb_class.BackColor = Color.FromArgb(255, 211, 182)
                                      End Sub)

                        End If

                    End If
                    If codelist.Length = 6 And Not String.IsNullOrEmpty(qr_96.Text) Then
                        matrix_err = False
                        Me.Invoke(Sub()

                                      qr_96.BackColor = Color.Red
                                      lb_class.Font = New Font(lb_class.Font.Name, 36, lb_class.Font.Style)
                                      lb_class.Text = "Không có phân cấp dung lượng "
                                      lb_class.BackColor = Color.FromArgb(255, 175, 0)
                                  End Sub)
                    End If
                Catch ex As Exception
                    matrix_err = False
                    Exit For
                End Try
            Next

        End If
        If Not matrix_err Then
            open_Light("Red")
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "Mã code không đúng."})
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
            Dim soundPlayer As New SoundPlayer(My.Resources.sound())

            soundPlayer.Play()
            Return
        End If
        If Not qrcode_err Then
            open_Light("Red")
            Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
            Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "Mã code không đúng."})
            Dim soundPlayer As New SoundPlayer(My.Resources.sound())
            soundPlayer.Play()
            Return

        End If
        If Not bg_work.IsBusy Then
            Dim args As Tuple(Of String, String) = New Tuple(Of String, String)(receivedData_2, "Z")
            bg_work.RunWorkerAsync(args)
        End If
        'bg_work.RunWorkerAsync(receivedData_2)

    End Sub
    Private Sub worker_DoWork(sender As Object, e As DoWorkEventArgs)


        Dim args As Tuple(Of String, String) = CType(e.Argument, Tuple(Of String, String))



        Dim data As String = args.Item1
        Dim type As String = args.Item2
        Dim _class As String = ""
        Dim rowData As Object() = {Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), data} ' Dữ liệu của dòng cần thêm
        Dim parsedValue As Integer
        Integer.TryParse(lb_count_limit.Text, parsedValue)

        If Not String.IsNullOrEmpty(txt_97.Text) Or Not String.IsNullOrEmpty(txt_97.Text) Then
            If dgv_data.Rows.Count() >= parsedValue Then
                open_Light("Red")
                Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "Vượt quá số lượng giới hạn !!!"})
                Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
                Return
            End If
        End If


        If type = "E" Then
            db.insert_error(data, Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), type)
            Return
        End If
        Try
            Dim pingSender As New Ping
            Dim pingreply As PingReply = pingSender.Send("172.16.40.31", 1000)
            If pingreply.Status = IPStatus.Success Then

                If MySettings.Default.code_detail = "DATA MATRIX" Then
                    _class = txt_97.Text
                Else
                    _class = qr_96.Text
                End If
                If db.Check(data) > 0 Then
                    Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "Sản phẩm đã hoàn trả"})
                    Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
                    Return
                End If
                If insert_data(data, Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), type) = "True" And db.insert(data, Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), 1, 1, _class, type) = "True" Then

                    AddDataToDataGridView(rowData)
                    open_Light("Green")
                    Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "OK"})
                    Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Lime})

                Else
                    If insert_data(data, Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), type) = "False" And db.insert(data, Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), 1, 1, _class, type) = "False" Then
                        open_Light("Red")
                        Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "ERROR insert"})
                        Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
                    Else
                        Dim soundPlayer As New SoundPlayer(My.Resources.Dou())
                        soundPlayer.Play()
                        open_Light("Green")
                        Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "Trùng lặp"})
                        Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Lime})

                    End If
                End If
            ElseIf pingreply.Status <> IPStatus.Success Then
                If db.insert(data, Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), 0, 0, _class, type) = "True" Then
                    AddDataToDataGridView(rowData)
                    open_Light("Green")

                    Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "OK"})
                    Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Lime})
                    _offlinecount()
                Else
                    If db.insert(data, Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), 0, 0, _class, type) = "False" Then
                        open_Light("Red")
                        Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "ERROR insert"})
                        Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
                    Else
                        Dim soundPlayer As New SoundPlayer(My.Resources.Dou())
                        soundPlayer.Play()

                        open_Light("Green")
                        Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "Trùng lặp"})
                        Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Lime})

                    End If
                End If
            End If



        Catch ex As Exception

            If db.insert(data, Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), 0, 0, _class, type) = "True" Then

                AddDataToDataGridView(rowData)
                open_Light("Green")
                Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "OK"})
                Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Lime})
                _offlinecount()
            Else
                If db.insert(data, Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), _class, 0, 0, type) = "False" Then
                    open_Light("Red")
                    Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "ERROR insert"})
                    Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Red})
                Else
                    Dim soundPlayer As New SoundPlayer(My.Resources.Dou())
                    soundPlayer.Play()
                    'Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_datamatrix, Color.White})
                    open_Light("Green")
                    Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_msg, "Trùng lặp"})
                    Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_msg, Color.Lime})
                    'Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_qrcode, Color.White})
                End If
            End If
        End Try




        'Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_datamatrix, Color.White})
        'Me.BeginInvoke(New UpdateControlColorDelegate(AddressOf _mUpdateControlColor), New Object() {lb_qrcode, Color.White})


        Me.BeginInvoke(New UpdateControlDelegate(AddressOf _mUpdateControl), New Object() {lb_count, dgv_data.Rows.Count().ToString()})
        Sort_DGV()

    End Sub
    Private Sub AddDataToDataGridView(data As Object())
        Dim connectionString As String = "Data Source=LELONG.db;Version=3;"
        Dim thoigian As String = data.GetValue(0)
        Dim macode As String = data.GetValue(1)
        Dim loai As String
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Dim command = conn.CreateCommand()
            command.CommandText = "SELECT loai FROM laser_table WHERE thoigian ='" & thoigian & "' AND macode = '" & macode & "' ;"

            Using reader = command.ExecuteReader()
                reader.Read()
                loai = reader.GetString(0)
            End Using

        End Using

        ' Kiểm tra giá trị của `loai`
        If loai = "Z" Then
            If dgv_data_check.InvokeRequired Then
                dgv_data_check.Invoke(New Action(Of Object())(AddressOf AddDataToDataGridView), New Object() {data})
            Else
                Dim newRowIndex As Integer = dgv_data_check.Rows.Add(data)

                Dim newRow As DataGridViewRow = dgv_data_check.Rows(newRowIndex)

                newRow.DefaultCellStyle.BackColor = Color.Yellow  ' Đổi màu nền thành vàng nếu `loai` là "Z"

            End If
        ElseIf loai = "N" Then
            If dgv_data.InvokeRequired Then
                dgv_data.Invoke(New Action(Of Object())(AddressOf AddDataToDataGridView), New Object() {data})
            Else
                Dim newRowIndex As Integer = dgv_data.Rows.Add(data)
                Dim newRow As DataGridViewRow = dgv_data.Rows(newRowIndex)
            End If
        End If

    End Sub

    Private Sub Sort_DGV()
        If dgv_data.InvokeRequired Then
            dgv_data.Invoke(New Action(AddressOf Sort_DGV))
        Else
            If dgv_data.Rows.Count > 0 Then
                dgv_data.Sort(dgv_data.Columns("time"), System.ComponentModel.ListSortDirection.Descending)
                dgv_data.CurrentCell = dgv_data.Rows(0).Cells(0)
                dgv_data.Rows(0).Selected = True
            End If


        End If
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs)

    End Sub

    Delegate Sub UpdateControlDelegate(ByVal Ctrl As Control, ByVal Msg As String)

    Private _objLock As New Object()

    Private Sub _mUpdateControl(ByVal Ctrl As Control, ByVal Msg As String)
        SyncLock Me._objLock
            If TypeOf Ctrl Is Label Then
                CType(Ctrl, Label).Text = Msg
            ElseIf TypeOf Ctrl Is TextBox Then
                CType(Ctrl, TextBox).Text = Msg
            End If
        End SyncLock
    End Sub
    Delegate Sub UpdateControlColorDelegate(ByVal Ctrl As Control, color As Color)
    Private _objLockColor As New Object()
    Private Sub _mUpdateControlColor(ByVal Ctrl As Control, ByVal color As Color)
        SyncLock _objLockColor
            If TypeOf Ctrl Is Label Then
                DirectCast(Ctrl, Label).BackColor = color
            End If
        End SyncLock
    End Sub

    Sub Create_code(_data As String)
        Dim writer As New BarcodeWriter()
        If MySettings.Default.code_detail = "QRCODE" Then
            writer.Format = BarcodeFormat.QR_CODE
        Else
            writer.Format = BarcodeFormat.DATA_MATRIX
        End If
        Dim datacode As Bitmap = writer.Write(_data)
        PictureBox2.Width = datacode.Width
        PictureBox2.Height = datacode.Height

        PictureBox2.Image = datacode
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '_offlinecount()
    End Sub

    Private Sub btn_edit_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub qrcode_set_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If serialPort IsNot Nothing AndAlso serialPort.IsOpen Then
            serialPort.Close()
        End If
        If serialPort2 IsNot Nothing AndAlso serialPort2.IsOpen Then
            serialPort2.Close()
        End If
        m_reader.ExecCommand("ALLOFF")
        OnlineStatus_Timer.Dispose()
        OnlineStatus_Timer.Stop()
        autoscanner_Timer.Dispose()
        autoscanner_Timer.Stop()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles setting.Click
        qrcode_setting.ShowDialog()
    End Sub


    Private Sub dgv_data_SelectionChanged(sender As Object, e As EventArgs)
        If dgv_data.Rows.Count > 0 Then
            '  readandShow(dgv_data.CurrentRow.Cells("data").Value.ToString())
        End If
    End Sub
    Private Sub tool_history_Click(sender As Object, e As EventArgs) Handles tool_history.Click
        qrcode_manage.ShowDialog()
    End Sub

    Private Sub tool_excel_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lb_code_TextChanged(sender As Object, e As EventArgs)
        'If MySettings.Default.code_detail = "DATA MATRIX" Then
        '    TableLayoutPanel4.ColumnStyles(1) = New ColumnStyle(SizeType.Absolute, 0)
        '    TableLayoutPanel4.ColumnStyles(0) = New ColumnStyle(SizeType.Absolute, 400)
        'Else
        '    TableLayoutPanel4.ColumnStyles(0) = New ColumnStyle(SizeType.Absolute, 0)
        '    TableLayoutPanel4.ColumnStyles(1) = New ColumnStyle(SizeType.Absolute, 300)
        'End If
    End Sub

    Private Sub txt_8010_Leave(sender As Object, e As EventArgs) Handles txt_8010.Leave
        txt_8010.Text = txt_8010.Text.Trim()
    End Sub
    Private Sub txt_8011_Leave(sender As Object, e As EventArgs) Handles txt_8011.Leave
        txt_8011.Text = txt_8011.Text.Trim()
    End Sub
    Private Sub txt_90_Leave(sender As Object, e As EventArgs) Handles txt_90.Leave
        txt_90.Text = txt_90.Text.Trim()
    End Sub
    Private Sub txt_91_Leave(sender As Object, e As EventArgs) Handles txt_91.Leave
        txt_91.Text = txt_91.Text.Trim()
    End Sub
    Private Sub txt_92_Leave(sender As Object, e As EventArgs) Handles txt_92.Leave
        txt_92.Text = txt_92.Text.Trim()
    End Sub
    Private Sub txt_93_Leave(sender As Object, e As EventArgs) Handles txt_93.Leave
        txt_93.Text = txt_93.Text.Trim()
    End Sub
    Private Sub txt_94_Leave(sender As Object, e As EventArgs) Handles txt_94.Leave
        txt_94.Text = txt_94.Text.Trim()
    End Sub
    Private Sub txt_95_Leave(sender As Object, e As EventArgs) Handles txt_95.Leave
        txt_95.Text = txt_95.Text.Trim()
    End Sub
    Private Sub txt_96_Leave(sender As Object, e As EventArgs) Handles txt_96.Leave
        txt_96.Text = txt_96.Text.Trim()
    End Sub
    Private Sub txt_97_Leave(sender As Object, e As EventArgs) Handles txt_97.Leave
        txt_97.Text = txt_97.Text.Trim()
    End Sub
    Private Sub txt_id_matrix_TextChanged(sender As Object, e As EventArgs) Handles txt_id_matrix.TextChanged
        btn_apply.Enabled = False
    End Sub
    Private Sub txt_id_matrix_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_id_matrix.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then

            txt_id_matrix_Leave(sender, e)
        End If
    End Sub
    Private Sub txt_id_matrix_Leave(sender As Object, e As EventArgs) Handles txt_id_matrix.Leave
        txt_id_matrix.Text = txt_id_matrix.Text.Trim()
        btn_apply.Enabled = True
        _calamviec()
        If MySettings.Default.code_detail = "DATA MATRIX" Then
            If btn_apply.Enabled = True And Not String.IsNullOrEmpty(txt_97.Text) And limit_edit.Visible = False Then
                Me.Invoke(Sub()
                              limit_edit.Visible = True

                          End Sub)

            End If
            If Not String.IsNullOrEmpty(lb_count_limit.Text) Then
                Me.Invoke(Sub()
                              lb_limit.Visible = True
                              lb_count_limit.Visible = True
                          End Sub)

            End If
        Else
            If btn_apply.Enabled = True And Not String.IsNullOrEmpty(qr_96.Text) And limit_edit.Visible = False Then
                Me.Invoke(Sub()
                              limit_edit.Visible = True

                          End Sub)

            End If
            If Not String.IsNullOrEmpty(lb_count_limit.Text) Then
                Me.Invoke(Sub()
                              lb_limit.Visible = True
                              lb_count_limit.Visible = True
                          End Sub)

            End If
        End If
        Try
            Dim pingSender As New Ping
            Dim pingreply As PingReply = pingSender.Send("172.16.40.31", 1000)
            If pingreply.Status = IPStatus.Success Then
                Using connprod As New OracleConnection(ora_connectstring)
                    connprod.Open()

                    Dim Checksql As String = "SELECT cpf01,cpf08 FROM cpf_file WHERE cpf01='" & txt_id_matrix.Text & "' "

                    Dim dr_check As OracleCommand = New OracleCommand(Checksql, connprod)
                    Dim dt_check As OracleDataReader = dr_check.ExecuteReader
                    If Not dt_check.HasRows Then
                        MessageBox.Show("Mã nhân viên không tồn tại xin nhập lại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txt_id_matrix.Text = ""
                        txt_id_matrix.Focus()
                        Return
                    Else
                        dt_check.Read()

                        txt_usernamematrix.Text = dt_check.Item("cpf08")
                        Return
                    End If

                End Using
            End If
        Catch ex As Exception

            Return
        End Try


    End Sub

    Private Sub enable_to_true()
        txt_8010.Enabled = True
        txt_8011.Enabled = True
        txt_90.Enabled = True
        txt_91.Enabled = True

        txt_92.Enabled = True
        txt_93.Enabled = True
        txt_94.Enabled = True
        txt_95.Enabled = True
        txt_96.Enabled = True

        qr_90.Enabled = True
        qr_91.Enabled = True
        qr_92.Enabled = True
        qr_93.Enabled = True
        qr_94.Enabled = True
        qr_95.Enabled = True
        setting.Enabled = True
        If Not String.IsNullOrEmpty(txt_id_matrix.Text) Then
            txt_id_matrix.Enabled = True
        Else
            txt_id_qr.Enabled = True
        End If
    End Sub
    Private Sub enable_to_false()
        txt_8010.Enabled = False
        txt_8011.Enabled = False
        txt_90.Enabled = False
        txt_91.Enabled = False

        txt_92.Enabled = False
        txt_93.Enabled = False
        txt_94.Enabled = False
        txt_95.Enabled = False
        txt_96.Enabled = False

        qr_90.Enabled = False
        qr_91.Enabled = False
        qr_92.Enabled = False
        qr_93.Enabled = False
        qr_94.Enabled = False
        qr_95.Enabled = False
        setting.Enabled = False
        If Not String.IsNullOrEmpty(txt_id_matrix.Text) Then
            txt_id_matrix.Enabled = False

        Else
            txt_id_qr.Enabled = False
        End If

    End Sub

    Private Sub ToolStripButton1_Click_1(sender As Object, e As EventArgs) Handles btn_apply.Click
        If MySettings.Default.code_detail = "DATA MATRIX" Then
            If String.IsNullOrEmpty(txt_8010.Text) Or String.IsNullOrEmpty(txt_8011.Text) Or String.IsNullOrEmpty(txt_90.Text) Or String.IsNullOrEmpty(txt_91.Text) Or String.IsNullOrEmpty(txt_92.Text) Or String.IsNullOrEmpty(txt_93.Text) Or String.IsNullOrEmpty(txt_94.Text) Or String.IsNullOrEmpty(txt_95.Text) Then
                MessageBox.Show("Nội dung không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            If String.IsNullOrEmpty(txt_id_matrix.Text) Then
                MessageBox.Show("Không thể để trống Mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            If Not String.IsNullOrEmpty(txt_97.Text) Then
                If String.IsNullOrEmpty(MySettings.Default.limit) Then
                    MessageBox.Show("Thiết lập giới hạn trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
            End If

        End If
        If MySettings.Default.code_detail = "QRCODE" Then
            If String.IsNullOrEmpty(qr_90.Text) Or String.IsNullOrEmpty(qr_91.Text) Or String.IsNullOrEmpty(qr_92.Text) Or String.IsNullOrEmpty(qr_93.Text) Or String.IsNullOrEmpty(qr_94.Text) Or String.IsNullOrEmpty(qr_95.Text) Then
                MessageBox.Show("Nội dung không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            If String.IsNullOrEmpty(txt_id_qr.Text) Then
                MessageBox.Show("Không thể để trống Mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            If Not String.IsNullOrEmpty(qr_96.Text) Then
                If String.IsNullOrEmpty(MySettings.Default.limit) Then
                    MessageBox.Show("Thiết lập giới hạn trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
            End If
        End If
        Dim noidung As String
        If MySettings.Default.code_detail = "DATA MATRIX" Then

            noidung = txt_8010.Text + txt_8011.Text + txt_90.Text + txt_91.Text + txt_92.Text + txt_93.Text + txt_94.Text + txt_95.Text
            If Not String.IsNullOrEmpty(txt_96.Text) Then
                noidung = noidung + txt_96.Text
            End If
            If Not String.IsNullOrEmpty(txt_97.Text) Then
                noidung = noidung + txt_97.Text
            End If
            txt_noidung.Text = noidung
            lb_datamatrix.Text = "GS1 Data Matrix: " + noidung
        Else
            noidung = qr_90.Text + ", " + qr_91.Text + ", " + qr_92.Text + ", " + qr_93.Text + ", " + qr_94.Text + ", " + qr_95.Text
            If Not String.IsNullOrEmpty(qr_96.Text) Then
                noidung = noidung + qr_96.Text
            End If
            lb_qrcode.Text = noidung
            txt_noidung.Text = noidung
        End If

        enable_to_false()
        If Not String.IsNullOrEmpty(txt_id_matrix.Text) Then
            TableLayoutPanel2.Enabled = False
        Else
            TableLayoutPanel5.Enabled = False
        End If


        txt_noidung.Enabled = False
        btn_apply.Enabled = False
        btn_edit.Enabled = True
        limit_edit.Enabled = False
        My.Settings.matrix8010 = txt_8010.Text
        My.Settings.matrix8011 = txt_8011.Text
        My.Settings.matrix90 = txt_90.Text
        My.Settings.matrix91 = txt_91.Text
        My.Settings.matrix92 = txt_92.Text
        My.Settings.matrix93 = txt_93.Text
        My.Settings.matrix94 = txt_94.Text
        My.Settings.matrix95 = txt_95.Text
        My.Settings.matrix96 = txt_96.Text
        My.Settings.matrix97 = txt_97.Text

        My.Settings.qrcode90 = qr_90.Text
        My.Settings.qrcode91 = qr_91.Text
        My.Settings.qrcode92 = qr_92.Text
        My.Settings.qrcode93 = qr_93.Text
        My.Settings.qrcode94 = qr_94.Text
        My.Settings.qrcode95 = qr_95.Text
        My.Settings.qrcode96 = qr_96.Text

        My.Settings.txt_noidung = noidung
        My.Settings.Save()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles btn_edit.Click
        enable_to_true()
        If Not String.IsNullOrEmpty(txt_id_matrix.Text) Then
            TableLayoutPanel2.Enabled = True

        Else
            TableLayoutPanel5.Enabled = True

        End If


        'txt_8010.Focus()
        txt_noidung.Enabled = True
        If Not String.IsNullOrEmpty(txt_id_matrix.Text) Then
            txt_id_matrix.Text = ""
            txt_usernamematrix.Text = ""
        Else
            txt_id_qr.Text = ""

            txt_usernameqr.Text = ""
        End If


        limit_edit.Enabled = True
        PictureBox2.Image = Nothing
        btn_apply.Enabled = True
        btn_edit.Enabled = False
        lb_qrcode.Text = "QRCODE DATA"
        lb_datamatrix.Text = "GS1 Data Matrix"
    End Sub

    Private Sub qr_90_Leave(sender As Object, e As EventArgs) Handles qr_90.Leave
        qr_90.Text = qr_90.Text.Trim()
    End Sub
    Private Sub qr_91_Leave(sender As Object, e As EventArgs) Handles qr_91.Leave
        qr_91.Text = qr_91.Text.Trim()
    End Sub
    Private Sub qr_92_Leave(sender As Object, e As EventArgs) Handles qr_92.Leave
        qr_92.Text = qr_92.Text.Trim()
    End Sub
    Private Sub qr_93_Leave(sender As Object, e As EventArgs) Handles qr_93.Leave
        qr_93.Text = qr_93.Text.Trim()
    End Sub
    Private Sub qr_94_Leave(sender As Object, e As EventArgs) Handles qr_94.Leave
        qr_94.Text = qr_94.Text.Trim()
    End Sub
    Private Sub qr_95_Leave(sender As Object, e As EventArgs) Handles qr_95.Leave
        qr_95.Text = qr_95.Text.Trim()
    End Sub
    Private Sub qr_96_Leave(sender As Object, e As EventArgs) Handles qr_96.Leave
        qr_96.Text = qr_96.Text.Trim()
    End Sub
    Private Sub txt_id_qr_TextChanged(sender As Object, e As EventArgs) Handles txt_id_qr.TextChanged
        btn_apply.Enabled = False
    End Sub
    Private Sub txt_id_qr_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_id_qr.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then

            txt_id_qr_Leave(sender, e)
        End If
    End Sub

    Private Sub txt_id_qr_Leave(sender As Object, e As EventArgs) Handles txt_id_qr.Leave
        If Not String.IsNullOrEmpty(txt_id_qr.Text) Then
            txt_id_qr.Text = txt_id_qr.Text.Trim()
            btn_apply.Enabled = True
            _calamviec()
            If MySettings.Default.code_detail = "DATA MATRIX" Then
                If btn_apply.Enabled = True And Not String.IsNullOrEmpty(txt_97.Text) And limit_edit.Visible = False Then
                    Me.Invoke(Sub()
                                  limit_edit.Visible = True

                              End Sub)
                End If
                If Not String.IsNullOrEmpty(lb_count_limit.Text) Then
                    Me.Invoke(Sub()
                                  lb_limit.Visible = True
                                  lb_count_limit.Visible = True
                              End Sub)

                End If
            Else
                If btn_apply.Enabled = True And Not String.IsNullOrEmpty(qr_96.Text) And limit_edit.Visible = False Then
                    Me.Invoke(Sub()
                                  limit_edit.Visible = True

                              End Sub)
                End If
                If Not String.IsNullOrEmpty(lb_count_limit.Text) Then
                    Me.Invoke(Sub()
                                  lb_limit.Visible = True
                                  lb_count_limit.Visible = True
                              End Sub)
                End If
            End If
            Try
                Dim pingSender As New Ping
                Dim pingreply As PingReply = pingSender.Send("172.16.40.31", 1000)
                If pingreply.Status = IPStatus.Success Then
                    Using connprod As New OracleConnection(ora_connectstring)
                        connprod.Open()

                        Dim Checksql As String = "SELECT cpf01,cpf08 FROM cpf_file WHERE cpf01='" & txt_id_qr.Text & "' "

                        Dim dr_check As OracleCommand = New OracleCommand(Checksql, connprod)
                        Dim dt_check As OracleDataReader = dr_check.ExecuteReader
                        If Not dt_check.HasRows Then
                            MessageBox.Show("Mã nhân viên không tồn tại xin nhập lại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            txt_id_qr.Text = ""
                            txt_id_qr.Focus()

                        Else
                            dt_check.Read()

                            txt_usernameqr.Text = dt_check.Item("cpf08")
                            Return
                        End If

                    End Using
                End If
            Catch ex As Exception
                Return
            End Try
        End If

    End Sub

    'Public Sub get_font_size(lb As Label, max_size As Single)
    '    Dim f As Font
    '    Dim g As Graphics
    '    Dim s As SizeF
    '    Dim Faktor, FaktorX, FaktorY As Single

    '    g = lb.CreateGraphics()
    '    s = g.MeasureString(lb.Text, lb.Font)
    '    g.Dispose()

    '    FaktorX = lb.Width / s.Width
    '    FaktorY = lb.Height / s.Height

    '    If FaktorX > FaktorY Then
    '        Faktor = FaktorY
    '    Else
    '        Faktor = FaktorX
    '    End If

    '    Faktor = CType(Faktor - 0.2, Single)
    '    f = lb.Font

    '    If f.SizeInPoints * Faktor > max_size Then
    '        If max_size - 5 <= 0 Then
    '            lb.Font = New Font(f.Name, 5)
    '        Else
    '            lb.Font = New Font(f.Name, max_size)
    '        End If
    '    Else
    '        If (f.SizeInPoints) * Faktor - 5 <= 0 Then
    '            lb.Font = New Font(f.Name, 5)
    '        Else
    '            lb.Font = New Font(f.Name, (f.SizeInPoints) * Faktor - 5)
    '        End If
    '    End If
    'End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        'SplitContainer1.Panel1Collapsed = Not SplitContainer1.Panel1Collapsed
        'If Not SplitContainer1.Panel1Collapsed Then
        '    Button1.Text = "<"
        'Else
        '    Button1.Text = ">"
        'End If
    End Sub
    Sub reload_layout()
        If MySettings.Default.code_detail = "QRCODE" Then
            layout_left.ColumnStyles(0).Width = 0
            layout_left.ColumnStyles(1) = New ColumnStyle(SizeType.Percent, 100)
            layout_right.ColumnStyles(0).Width = 0
            layout_right.ColumnStyles(1) = New ColumnStyle(SizeType.Percent, 100)
            SplitContainer1.Panel1Collapsed = False
            SplitContainer1.SplitterDistance = 300 ' Khoảng cách mong muốn khi Panel1 mở rộng
        Else
            layout_left.ColumnStyles(0) = New ColumnStyle(SizeType.Percent, 100)
            layout_left.ColumnStyles(1).Width = 0

            layout_right.ColumnStyles(0) = New ColumnStyle(SizeType.Percent, 100)
            layout_right.ColumnStyles(1).Width = 0
            SplitContainer1.Panel1Collapsed = False
            SplitContainer1.SplitterDistance = 400 ' Khoảng cách mong muốn khi Panel1 mở rộng
        End If


    End Sub

    Private Sub font_plus_Click(sender As Object, e As EventArgs) Handles font_plus.Click
        _font += 1
        If _font >= 5 Then
            dgv_data.DefaultCellStyle.Font = New Font("Arial", _font)
            lb_datamatrix.Font = New Font(lb_datamatrix.Font.FontFamily, _font + 5, lb_datamatrix.Font.Style)
            lb_qrcode.Font = New Font(lb_qrcode.Font.FontFamily, _font + 5, lb_qrcode.Font.Style)
        Else
            _font = 5
        End If

    End Sub

    Private Sub font_minus_Click(sender As Object, e As EventArgs) Handles font_minus.Click
        _font -= 1
        If _font >= 5 Then
            dgv_data.DefaultCellStyle.Font = New Font("Arial", _font)
            lb_datamatrix.Font = New Font(lb_datamatrix.Font.FontFamily, _font + 5, lb_datamatrix.Font.Style)
            lb_qrcode.Font = New Font(lb_qrcode.Font.FontFamily, _font + 5, lb_qrcode.Font.Style)
        Else
            _font = 5
        End If

    End Sub



    Private Sub SplitContainer1_MouseClick(sender As Object, e As MouseEventArgs) Handles SplitContainer1.MouseClick
        Button1_Click_1(sender, e)
    End Sub

    Private Sub btn_exit_Click(sender As Object, e As EventArgs) Handles btn_exit.Click
        Me.Close()
    End Sub


    Private Sub UploadDataToTipTop()

        Try
            flat = "False"

            Dim connectionString As String = "Data Source=LELONG.db;Version=3;"
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Dim commandconn = conn.CreateCommand()


                Dim SelectSql As String = "SELECT macode,thoigian,loai FROM laser_table WHERE trangthai=0"
                commandconn.CommandText = SelectSql

                Using reader = commandconn.ExecuteReader()
                    If reader.HasRows Then
                        Using connprod As New OracleConnection(ora_connectstring)
                            connprod.Open()
                            Trans_ora = connprod.BeginTransaction()
                            g_success = "Y"
                            While reader.Read()

                                Dim macode As String = reader.GetString(0)
                                Dim thoigian As String = reader.GetString(1)
                                Dim loai As String = reader.GetString(2)


                                If Not String.IsNullOrEmpty(txt_id_matrix.Text) Then
                                    Try
                                        Dim InsertSql As String = " INSERT INTO  tc_mtm_file VALUES( '" & macode & "',to_date('" & thoigian & "','YYYY/MM/DD HH24:MI:SS'),'" & txt_id_matrix.Text & "','" & loai & "','" & local & "')"
                                        Dim dr_insert As OracleCommand = New OracleCommand(InsertSql, connprod)
                                        Dim dt_insert As OracleDataReader = dr_insert.ExecuteReader

                                    Catch ex As OracleException
                                        If ex.Number = 1 Then

                                            Continue While
                                        Else
                                            g_success = "N"
                                            MessageBox.Show(ex.ToString)

                                            Return
                                        End If



                                    End Try

                                Else
                                    Try
                                        Dim InsertSql As String = " INSERT INTO  tc_mtm_file VALUES( '" & macode & "',to_date('" & thoigian & "','YYYY/MM/DD HH24:MI:SS') ,'" & txt_id_qr.Text & "','" & loai & "','" & local & "')"
                                        Dim dr_insert As OracleCommand = New OracleCommand(InsertSql, connprod)
                                        Dim dt_insert As OracleDataReader = dr_insert.ExecuteReader

                                    Catch ex As OracleException

                                        If ex.Number = 1 Then

                                            Continue While
                                        Else
                                            g_success = "N"
                                            MessageBox.Show(ex.ToString)
                                            Return
                                        End If




                                    End Try




                                End If

                            End While

                            If g_success = "Y" Then
                                Trans_ora.Commit()
                                db.update()
                                flat = "True"
                                _offlinecount()
                                Thread.Sleep(1000)
                                Me.BeginInvoke(New Action(Sub() offline_count.Visible = False))
                            Else
                                Trans_ora.Rollback()
                            End If
                        End Using
                    Else Return
                    End If


                End Using

            End Using
        Catch ex As Exception
            Return

        End Try

    End Sub
    Public Function insert_data(data As String, time As String, type As String)


        Using connprod As New OracleConnection(ora_connectstring)

            connprod.Open()
            Trans_ora = connprod.BeginTransaction()
            g_success = "Y"
            If Not String.IsNullOrEmpty(txt_id_matrix.Text) Then

                Try
                    Dim InsertSql As String = " INSERT INTO  tc_mtm_file VALUES( '" & data & "',to_date('" & time & "','YYYY/MM/DD HH24:MI:SS') ,'" & txt_id_matrix.Text & "','" & type & "','" & local & "')"
                    Dim dr_insert As OracleCommand = New OracleCommand(InsertSql, connprod)
                    Dim dt_insert As OracleDataReader = dr_insert.ExecuteReader
                Catch ex As OracleException

                    If ex.Number = 1 Then
                        Return "Dup"
                    Else

                        g_success = "N"
                        MessageBox.Show(ex.ToString)
                        Return "False"
                    End If

                End Try
                If g_success = "Y" Then
                    Trans_ora.Commit()
                Else
                    Trans_ora.Rollback()
                End If
                Return "True"

            Else
                Try
                    Dim InsertSql As String = " INSERT INTO  tc_mtm_file VALUES( '" & data & "',to_date('" & time & "','YYYY/MM/DD HH24:MI:SS') ,'" & txt_id_qr.Text & "','" & type & "','" & local & "')"
                    Dim dr_insert As OracleCommand = New OracleCommand(InsertSql, connprod)
                    Dim dt_insert As OracleDataReader = dr_insert.ExecuteReader
                Catch ex As OracleException

                    If ex.Number = 1 Then
                        Return "Dup"
                    Else

                        g_success = "N"
                        MessageBox.Show(ex.ToString)
                        Return "False"
                    End If

                End Try
                If g_success = "Y" Then
                    Trans_ora.Commit()
                Else
                    Trans_ora.Rollback()
                End If
                Return "True"
            End If
        End Using
    End Function

    Private Sub limit_edit_Click(sender As Object, e As EventArgs) Handles limit_edit.Click
        qrcode_set_limit.ShowDialog()
    End Sub
    Private Sub get_font_size(lb As Label, max_size As Single)
        Dim f As Font
        Dim g As Graphics
        Dim s As SizeF
        Dim Faktor, FaktorX, FaktorY As Single
        g = lb.CreateGraphics()
        s = g.MeasureString(lb.Text, lb.Font)
        g.Dispose()

        FaktorX = lb.Width / s.Width
        FaktorY = lb.Height / s.Height

        If FaktorX > FaktorY Then
            Faktor = FaktorY
        Else
            Faktor = FaktorX
        End If

        f = lb.Font
        If f.SizeInPoints * Faktor > max_size Then
            lb.Font = New Font(f.Name, max_size)
        Else
            lb.Font = New Font(f.Name, f.SizeInPoints * Faktor)
        End If
    End Sub


End Class