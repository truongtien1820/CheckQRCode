Imports System.Data.SQLite
Imports System.IO

Public Class sqldb
    Dim connectionString As String = "Data Source=LELONG.db;Version=3;"
    Dim rowCount As Integer
    Dim countCheck As Integer
    Public Sub createdb()

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Dim createTableSql As String = "CREATE TABLE IF NOT EXISTS laser_table(macode TEXT,thoigian TEXT,phanloai TEXT,trangthai INTEGER,chuyendoi INTEGER, loai TEXT ,PRIMARY KEY (macode, loai) );"
            Dim createTableCommand As New SQLiteCommand(createTableSql, conn)
            createTableCommand.ExecuteNonQuery()

            Dim createTableSql_error As String = "CREATE TABLE IF NOT EXISTS error_table(error TEXT,thoigian TEXT, loai TEXT ,PRIMARY KEY (error,thoigian) );"
            Dim createTableCommand_error As New SQLiteCommand(createTableSql_error, conn)
            createTableCommand_error.ExecuteNonQuery()

        End Using


    End Sub
    Public Function insert_error(_data As String, _time As String, _type As String)
        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()


                Dim insertDataSql As String = "INSERT INTO error_table(error,thoigian) VALUES ('" & _data & "','" & _time & "','" & _type & "');"
                Dim insertDataCommand As New SQLiteCommand(insertDataSql, conn)
                insertDataCommand.ExecuteNonQuery()



            End Using
            Return "True"
        Catch ex As SQLiteException
            If ex.ErrorCode = SQLiteErrorCode.Constraint Then
                Return "Dup"
            Else
                Return "False"
            End If

        End Try

    End Function
    Public Function insert(_data As String, _time As String, _status As Integer, _convert As Integer, _class As String, _type As String)
        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()


                Dim insertDataSql As String = "INSERT INTO laser_table(macode,thoigian,phanloai,trangthai,chuyendoi,loai) VALUES ('" & _data & "','" & _time & "','" & _class & "'," & _status & "," & _convert & ",'" & _type & "');"
                    Dim insertDataCommand As New SQLiteCommand(insertDataSql, conn)
                    insertDataCommand.ExecuteNonQuery()



            End Using
            Return "True"
        Catch ex As SQLiteException
            If ex.ErrorCode = SQLiteErrorCode.Constraint Then
                Return "Dup"
            Else
                Return "False"
            End If

        End Try

    End Function

    Public Function Count() As Integer
        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()

                Dim countRowsSql As String = "SELECT COUNT(*) FROM laser_table WHERE trangthai = 0 AND chuyendoi  = 0 ;"

                Dim countRowsCommand As New SQLiteCommand(countRowsSql, conn)

                ' Thực hiện truy vấn và nhận kết quả duy nhất
                rowCount = Convert.ToInt32(countRowsCommand.ExecuteScalar())

            End Using
            Return rowCount
        Catch ex As SQLiteException

        End Try
    End Function
    Public Function Check(_data As String) As Integer
        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()

                Dim countRowsSql As String = "SELECT COUNT(*) FROM laser_table WHERE  macode = '" & _data & "' and loai = 'X' ;"

                Dim countRowsCommand As New SQLiteCommand(countRowsSql, conn)

                ' Thực hiện truy vấn và nhận kết quả duy nhất
                countCheck = Convert.ToInt32(countRowsCommand.ExecuteScalar())

            End Using
            Return countCheck
        Catch ex As SQLiteException

        End Try
    End Function
    Public Function update()
        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()

                Dim commandconn = conn.CreateCommand()


                Dim SelectSql As String = "SELECT macode,thoigian FROM laser_table WHERE trangthai=0"
                commandconn.CommandText = SelectSql
                Using reader = commandconn.ExecuteReader()

                    If reader.HasRows Then
                        While reader.Read()

                            Dim macode As String = reader.GetString(0)
                            Dim thoigian As String = reader.GetString(1)
                            Dim updateRowsSql As String = "UPDATE  laser_table SET chuyendoi=1 WHERE macode = '" & macode & "' AND thoigian= '" & thoigian & "' AND trangthai=0 ;"

                            Dim updateRowsCommand As New SQLiteCommand(updateRowsSql, conn)
                            updateRowsCommand.ExecuteNonQuery()
                        End While
                    End If


                End Using
            End Using



        Catch ex As SQLiteException

        End Try
    End Function
    Public Function delete()
        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                ' Lấy ngày hiện tại và trừ đi một tháng
                Dim oneMonthAgo As DateTime = DateTime.Now.AddMonths(-1)

                ' Format ngày thành chuỗi theo định dạng của SQLitetxg mj,
                Dim formattedDate As String = oneMonthAgo.ToString("yyyy-MM-dd")

                Dim deleteDataSql As String = "DELETE  FROM laser_table WHERE thoigian < '" & formattedDate & "'  ;"
                Dim deleteDataCommand As New SQLiteCommand(deleteDataSql, conn)
                deleteDataCommand.ExecuteNonQuery()
            End Using
            Return "True"
        Catch ex As SQLiteException
            Return "False"
        End Try

    End Function

End Class
