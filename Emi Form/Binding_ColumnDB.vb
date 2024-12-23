Public Class Binding_ColumnDB

    Dim query As String = ""

    Private Sub Binding_ColumnDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()



    End Sub

    Private Sub Kosong()


        ListView1.Clear()
        query = ""

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            OpenConn()

            SQL = "select query from alter_table"
            Using Ds = BindingTrans(SQL)

            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


End Class