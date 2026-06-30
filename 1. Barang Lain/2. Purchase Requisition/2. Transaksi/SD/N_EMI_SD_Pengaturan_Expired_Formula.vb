Public Class N_EMI_SD_Pengaturan_Expired_Formula
    Public NoFormula As String = ""
    Public KodeBarang As String = ""
    Public NamaBarang As String = ""
    Public QtyHasil As String = ""
    Public SatuanHasil As String = ""
    Public JenisExpired As String = ""
    Public startExp As DateTime
    Public endExp As DateTime

    Private Sub N_EMI_SD_Konfirmasi_Bypass_Formula_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub N_EMI_SD_Pengaturan_Expired_Formula_Load(sender As Object, e As EventArgs) Handles Me.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        With Cmb_PeriodePemakaian
            .Items.Clear()
            .Items.Add("Permanen")
            .Items.Add("Sementara")
            .SelectedIndex = 0
        End With

        TXT_NoFormula.Text = NoFormula
        TXT_KodeBarang.Text = KodeBarang
        TXT_NamaBarang.Text = NamaBarang
        TXT_QtyHasil.Text = QtyHasil
        If Not CMB_SatuanHasil.Items.Contains(SatuanHasil) Then
            CMB_SatuanHasil.Items.Add(SatuanHasil)
        End If

        CMB_SatuanHasil.Text = SatuanHasil
    End Sub

    Private Sub Cmb_PeriodePemakaian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_PeriodePemakaian.SelectedIndexChanged
        Dim isSementara As Boolean = (Cmb_PeriodePemakaian.Text = "Sementara")

        Tgl1.Visible = isSementara
        Tgl2.Visible = isSementara
        Label4.Visible = isSementara
    End Sub

    Private Sub BTN_Simpan_Click(sender As Object, e As EventArgs) Handles BTN_Simpan.Click
        JenisExpired = Cmb_PeriodePemakaian.Text

        If JenisExpired = "Sementara" Then
            If Tgl1.Value > Tgl2.Value Then
                MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal akhir.",
                            Judul,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
                Exit Sub
            End If

            startExp = Tgl1.Value
            endExp = Tgl2.Value

        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class