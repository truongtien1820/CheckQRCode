Imports Oracle.ManagedDataAccess.Client
Imports System.Data.SQLite
Imports System.Net.NetworkInformation
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports qrcode_set.My
Imports System.Diagnostics.Eventing.Reader

Public Class qrcode_manage

    Private Sub qrcode_manage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim items As New Dictionary(Of String, String)()
        items.Add("1", "普通的 Bình thường")
        items.Add("2", "掃描槍 Máy quét Zebra")
        items.Add("3", "掃描返回 Hoàn trả")

        cb_type.DataSource = New BindingSource(items, Nothing)
        cb_type.DisplayMember = "Value"   ' Hiển thị trong ComboBox
        cb_type.ValueMember = "Key"       ' Giá trị ẩn

    End Sub

    Private Sub AddDataToDataGridView(data As Object())
        If qrcode_history.InvokeRequired Then
            qrcode_history.Invoke(New Action(Of Object())(AddressOf AddDataToDataGridView), New Object() {data})
        Else
            qrcode_history.Rows.Add(data)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim bdate As String = dtp_bdate.Value.ToString("yyyy/MM/dd HH:mm:ss")
        Dim edate As String = dtp_edate.Value.ToString("yyyy/MM/dd HH:mm:ss")
        Try
            qrcode_history.Rows.Clear()
            Dim connectionString As String = "Data Source=LELONG.db;Version=3;"
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Dim command = conn.CreateCommand()
                Dim sql As String = "SELECT macode,thoigian,qrcode_010 FROM laser_table WHERE thoigian>='" & bdate & "' AND thoigian<='" & edate & "'  "

                If cb_type.SelectedValue.ToString() = "1" Then
                    sql = sql + " AND loai = 'N' ;"
                ElseIf cb_type.SelectedValue.ToString() = "2" Then
                    sql = sql + " AND loai = 'Z' ;"
                ElseIf cb_type.SelectedValue.ToString() = "3" Then
                    sql = sql + " AND loai = 'X' ;"
                End If
                command.CommandText = sql
                Using reader = command.ExecuteReader()
                    While reader.Read()
                        Dim macode As String = reader.GetString(0)
                        Dim thoigian As String = reader.GetString(1)
                        Dim qr_code010 As String = reader.GetString(2)
                        Dim rowData As Object() = {macode, thoigian, qr_code010}
                        AddDataToDataGridView(rowData)
                    End While
                End Using

            End Using
            count.Text = qrcode_history.Rows.Count
        Catch ex As Exception
            Return
        End Try





    End Sub

    Private Sub btn_excel_Click(sender As Object, e As EventArgs) Handles btn_excel.Click
        If qrcode_history.Rows.Count = 0 Then
        End If
        Try
            ' Tạo đối tượng Excel và các đối tượng liên quan
            Dim excelApp As New Excel.Application()
            Dim workbook As Excel.Workbook = excelApp.Workbooks.Add()
            Dim worksheet As Excel.Worksheet = workbook.ActiveSheet

            ' Ghi dữ liệu từ DataGridView vào Excel
            Dim rowIndex As Integer = 1

            ' Ghi tiêu đề
            For colIndex As Integer = 1 To qrcode_history.Columns.Count
                worksheet.Cells(rowIndex, colIndex) = qrcode_history.Columns(colIndex - 1).HeaderText
            Next

            ' Ghi dữ liệu từ các ô DataGridView vào Excel
            rowIndex += 1
            For Each row As DataGridViewRow In qrcode_history.Rows
                For colIndex As Integer = 1 To qrcode_history.Columns.Count
                    worksheet.Cells(rowIndex, colIndex) = row.Cells(colIndex - 1).Value
                Next
                rowIndex += 1
            Next

            ' Lưu tệp Excel
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx"
            saveFileDialog.Title = "Chọn thư mục lưu file"
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Dim fileName As String = saveFileDialog.FileName

                ' Lưu tệp Excel
                workbook.SaveAs(fileName)
                workbook.Close()
                excelApp.Quit()

                MessageBox.Show("Xuất Excel thành công!")
            End If
        Catch ex As Exception
            MessageBox.Show("Đã xảy ra lỗi: " + ex.Message)
        End Try
    End Sub
End Class