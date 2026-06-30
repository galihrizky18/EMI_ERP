Public Class N_EMI_SD_Konfirmasi_Bypass_Formula
    Public NoFormula As String = ""
    Public KodeBarang As String = ""
    Public NamaBarang As String = ""
    Public QtyHasil As String = ""
    Public SatuanHasil As String = ""

    Public IsConfirmed As Boolean = False
    Public KeteranganBypass As String = ""

    Private Sub N_EMI_SD_Konfirmasi_Bypass_Formula_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub N_EMI_SD_Konfirmasi_Bypass_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        TXT_NoFormula.Text = NoFormula
        TXT_KodeBarang.Text = KodeBarang
        TXT_NamaBarang.Text = NamaBarang
        TXT_QtyHasil.Text = QtyHasil
        If Not CMB_SatuanHasil.Items.Contains(SatuanHasil) Then
            CMB_SatuanHasil.Items.Add(SatuanHasil)
        End If

        CMB_SatuanHasil.Text = SatuanHasil
    End Sub

    Private Sub BTN_Simpan_Click(sender As Object, e As EventArgs) Handles BTN_Simpan.Click
        If TXT_Keterangan.Text.Trim = "" Then
            MessageBox.Show(
                "Keterangan bypass wajib diisi.",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            Exit Sub
        End If

        KeteranganBypass = TXT_Keterangan.Text.Trim
        IsConfirmed = True

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class