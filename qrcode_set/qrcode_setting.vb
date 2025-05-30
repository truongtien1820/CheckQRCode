Imports System.IO.Ports
Imports QR014_CODECHECKING.My

Public Class qrcode_setting
    Private serialPort As SerialPort


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        MySettings.Default.code_detail = cb_code.SelectedItem
        MySettings.Default.b_h1 = b_h1.SelectedItem
        MySettings.Default.b_m1 = b_m1.SelectedItem
        MySettings.Default.e_h1 = e_h1.SelectedItem
        MySettings.Default.e_m1 = e_m1.SelectedItem

        MySettings.Default.b_h2 = b_h2.SelectedItem
        MySettings.Default.b_m2 = b_m2.SelectedItem
        MySettings.Default.e_h2 = e_h2.SelectedItem
        MySettings.Default.e_m2 = e_m2.SelectedItem
        MySettings.Default.time_ligh = time_light.SelectedItem
        MySettings.Default.delay_time = delay_time.SelectedItem
        MySettings.Default.connect = chb_connect.Checked
        MySettings.Default.IP = txt_IP.Text.Trim()
        MySettings.Default.IPQR010 = txt_ipqr010.Text.Trim()
        MySettings.Default.Save()
        Me.Close()
        qrcode_set.lb_code.Text = cb_code.SelectedItem
        'qrcode_set.PictureBox2.Image = Nothing
        qrcode_set.btn_apply.Enabled = True

        qrcode_set.reload_layout()
    End Sub
    Private Sub chb_connect_CheckedChanged(sender As Object, e As EventArgs)
        If chb_connect.Checked Then
            txt_ipqr010.Text = ""
            txt_ipqr010.ReadOnly = True
            txt_ipqr010.BackColor = SystemColors.Control
            txt_ipqr010.Enabled = False
        Else
            txt_ipqr010.Enabled = True
            txt_ipqr010.ReadOnly = False
            txt_ipqr010.BackColor = Color.White
        End If
    End Sub

    Private Sub qrcode_setting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Gọi luôn để áp dụng trạng thái ngay khi form load
        cb_code.SelectedItem = MySettings.Default.code_detail
        b_h1.SelectedItem = MySettings.Default.b_h1
        b_m1.SelectedItem = MySettings.Default.b_m1
        e_h1.SelectedItem = MySettings.Default.e_h1
        e_m1.SelectedItem = MySettings.Default.e_m1
        e_h2.SelectedItem = MySettings.Default.e_h2

        b_h2.SelectedItem = MySettings.Default.b_h2
        b_m2.SelectedItem = MySettings.Default.b_m2
        e_m2.SelectedItem = MySettings.Default.e_m2
        chb_connect.Checked = MySettings.Default.connect
        delay_time.SelectedItem = MySettings.Default.delay_time
        time_light.SelectedItem = MySettings.Default.time_ligh

        txt_IP.Text = MySettings.Default.IP
        txt_ipqr010.Text = MySettings.Default.IPQR010
        AddHandler chb_connect.CheckedChanged, AddressOf chb_connect_CheckedChanged
        chb_connect_CheckedChanged(Nothing, Nothing)

    End Sub

End Class