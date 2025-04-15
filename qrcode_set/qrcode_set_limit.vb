Imports System.IO.Ports
Imports QR014_CODECHECKING.My
Public Class qrcode_set_limit
    Private Sub qrcode_set_limit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        limit_txt.Text = MySettings.Default.limit
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim result As Integer

        If Integer.TryParse(limit_txt.Text, result) Then
            MySettings.Default.limit = limit_txt.Text.Trim()
            qrcode_set.lb_count_limit.Text = limit_txt.Text.Trim()
            Me.Invoke(Sub()

                          qrcode_set.lb_limit.Visible = True
                          qrcode_set.lb_count_limit.Visible = True

                      End Sub)
            Me.Close()
        Else
            MessageBox.Show("Giơi hạn phải là số !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If



    End Sub
End Class