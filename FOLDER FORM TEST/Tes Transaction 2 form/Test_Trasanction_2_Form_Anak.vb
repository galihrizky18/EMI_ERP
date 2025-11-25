Public Class Test_Trasanction_2_Form_Anak
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SQL = " insert into EMI_Master_Sub_Jasa (Kode_Perusahaan, Kode_Sub_Jasa, Kode_Jasa, Keterangan)values ('1', '1', '1', '1', '1')"
        ExecuteTrans(SQL)
        Me.Close()
    End Sub
End Class