Public Class Biaya_Work_Center
    Private Sub Biaya_Work_Center_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Initial_ListView()
        Isi_ListView()
    End Sub
    Private Sub Initial_ListView()

        Lv_Biaya.Columns.Add("Akun", 200, HorizontalAlignment.Left)
        Lv_Biaya.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Biaya.Columns.Add("Nominal", 200, HorizontalAlignment.Left)
        Lv_Biaya.View = View.Details

    End Sub

    Private Sub Isi_ListView()
        Dim LV As New ListViewItem()
        LV = Lv_Biaya.Items.Add("12.0001.11")
        LV.SubItems.Add("Biaya Listrik")
        LV.SubItems.Add("11.750.000")

        LV = Lv_Biaya.Items.Add("12.0001.15")
        LV.SubItems.Add("Biaya Air")
        LV.SubItems.Add("8.970.000")

    End Sub

End Class