Public Class Account
    Private Sub Acoount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lv_DisplayAccount.Columns.Add("Kode Akun", 220, HorizontalAlignment.Center)
        Lv_DisplayAccount.Columns.Add("Nama Akun", 250, HorizontalAlignment.Left)
        Lv_DisplayAccount.View = View.Details

        'ISI DATA 2
        Dim LV2 As ListViewItem = Lv_DisplayAccount.Items.Add("2315")
        LV2.SubItems.Add("Akun Listrik")

        LV2 = Lv_DisplayAccount.Items.Add("2316")
        LV2.SubItems.Add("Akun Air")

        LV2 = Lv_DisplayAccount.Items.Add("2317")
        LV2.SubItems.Add("Akun Bahan")
    End Sub
End Class