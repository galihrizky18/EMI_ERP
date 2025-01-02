Public Class Display_Account
    Private Sub Display_Account_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        intial_DataGridView()
        insert_Data_GridView()

    End Sub

    Private Sub intial_DataGridView()

        Dg_DisplayAkun.Columns.Add("kode_akun", "Kode Akun")
        Dg_DisplayAkun.Columns("kode_akun").Width = 270

        Dg_DisplayAkun.Columns.Add("nama_akun", "Nama Akun")
        Dg_DisplayAkun.Columns("nama_akun").Width = 275

        Dim productCost As New DataGridViewCheckBoxColumn()
        productCost.HeaderText = "Production Cost"
        productCost.Width = 150
        Dg_DisplayAkun.Columns.Add(productCost)

        Dim costCenter As New DataGridViewCheckBoxColumn()
        costCenter.HeaderText = "Cost Center"
        costCenter.Width = 150
        Dg_DisplayAkun.Columns.Add(costCenter)

        'Dim workCenter As New DataGridViewCheckBoxColumn()
        'workCenter.HeaderText = "Work Center"
        'workCenter.Width = 150
        'Dg_DisplayAkun.Columns.Add(workCenter)

    End Sub

    Private Sub insert_Data_GridView()
        Dg_DisplayAkun.Rows.Add("100.001.01", "Akun Listrik")
        Dg_DisplayAkun.Rows.Add("100.001.02", "Akun Air")
        Dg_DisplayAkun.Rows.Add("100.001.03", "Akun Bahan")
    End Sub
End Class