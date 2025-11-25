Public Class Test_Trasanction_2_Form_Induk
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "insert into EMI_Master_Jasa (Kode_Perusahaan, Kode_Jasa, Keterangan)values ('1', '1', '1')"
            ExecuteTrans(SQL)

            Test_Trasanction_2_Form_Anak.ShowDialog()


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Oke")
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class