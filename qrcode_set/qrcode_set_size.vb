Imports System.IO.Ports
Imports System.Windows.Media.Effects
Imports QR014_CODECHECKING.My

Public Class qrcode_set_size
    Public Scan_Content As String
    Public size_8010 As String
    Public size_8011 As String

    Public content As String()

    Public size_90 As String
    Public size_91 As String
    Public size_92 As String
    Public size_93 As String
    Public size_94 As String
    Public size_95 As String


    Public Sub qrcode_substr(Scan_Content)

        Dim key As String = ChrW(29)
        Dim abc As String = Scan_Content
        content = abc.Split(key)
        Dim index As Integer = 0
        For Each word As String In content
            index += 1
            If index = 1 Then
                My.Settings.matrix8010 = word

            End If
            If index = 2 Then
                My.Settings.matrix8011 = word

            End If
            If index = 3 Then
                My.Settings.matrix90 = word

            End If
            If index = 4 Then
                My.Settings.matrix91 = word

            End If
            If index = 5 Then
                My.Settings.matrix92 = word

            End If
            If index = 6 Then
                My.Settings.matrix93 = word

            End If
            If index = 7 Then
                My.Settings.matrix94 = word

            End If
            If index = 8 Then
                My.Settings.matrix95 = word

            End If
            If index = 9 Then
                My.Settings.matrix96 = word

            End If
            If index = 10 Then
                My.Settings.matrix97 = word

            End If

        Next
        If content.Length < 9 Then
            My.Settings.matrix96 = ""
            My.Settings.matrix97 = ""
        End If
        If content.Length = 9 Then
            My.Settings.matrix97 = ""
        End If
        qrcode_set.reload_layout()



    End Sub

    Public Sub qrcode_substr_qr(Scan_Content)

        Dim key As String = ","
        Dim abc As String = Scan_Content
        content = abc.Split(key)
        Dim index As Integer = 0
        For Each word As String In content
            index += 1
            If index = 1 Then
                My.Settings.qrcode90 = word.Trim(" ")

            End If
            If index = 2 Then
                My.Settings.qrcode91 = word.Trim(" ")

            End If
            If index = 3 Then
                My.Settings.qrcode92 = word.Trim(" ")

            End If
            If index = 4 Then
                My.Settings.qrcode93 = word.Trim(" ")

            End If
            If index = 5 Then
                My.Settings.qrcode94 = word.Trim(" ")

            End If
            If index = 6 Then
                My.Settings.qrcode95 = word.Trim(" ")

            End If
            If content.Length = 6 Then
                My.Settings.qrcode96 = ""
            ElseIf index = 7 Then
                My.Settings.qrcode96 = word.Trim(" ")

            End If
            qrcode_set.reload_layout()
        Next





    End Sub

End Class