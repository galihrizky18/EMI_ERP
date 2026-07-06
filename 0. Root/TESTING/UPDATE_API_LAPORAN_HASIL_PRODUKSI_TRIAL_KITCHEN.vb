Public Class UPDATE_API_LAPORAN_HASIL_PRODUKSI_TRIAL_KITCHEN

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

		Url_Api_Laporan_Formulator = $"{TextBox1.Text.Trim}/api/laporan-formula-lims-new"
		Url_Api_Laporan_Formulator_Trial_Produksi = $"{TextBox2.Text.Trim}/api/laporan-formula-trial-produksi"

		Url_Api_Laporan_Formulator_Compare = $"{TextBox3.Text}/api/laporan-formula-lims-new-compare"
		Url_Api_Laporan_Formulator_Trial_Produksi_Compare = $"{TextBox4.Text}/api/laporan-formula-trial-produksi-compare"

		TextBox1.Text = ""
		TextBox2.Text = ""
		TextBox3.Text = ""
		TextBox4.Text = ""
		MessageBox.Show("API Berhasil Diupdate", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

	End Sub

End Class