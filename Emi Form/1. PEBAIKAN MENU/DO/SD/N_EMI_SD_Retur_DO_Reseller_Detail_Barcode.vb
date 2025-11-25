Public Class N_EMI_SD_Retur_DO_Reseller_Detail_Barcode
    Private Sub N_EMI_SD_Retur_DO_Reseller_Detail_Barcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lv_Data_Barcode.Columns.Clear()
        Lv_Data_Barcode.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        Lv_Data_Barcode.Columns.Add("Barcode", 300, HorizontalAlignment.Left)
        Lv_Data_Barcode.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
        Lv_Data_Barcode.View = View.Details
    End Sub
End Class