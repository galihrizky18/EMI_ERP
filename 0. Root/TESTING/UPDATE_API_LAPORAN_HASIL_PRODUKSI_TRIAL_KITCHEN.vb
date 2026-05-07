Public Class UPDATE_API_LAPORAN_HASIL_PRODUKSI_TRIAL_KITCHEN

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

		Url_Api_Laporan_Formulator = $"{TextBox1.Text.Trim}/api/laporan-formula"

		TextBox1.Text = ""
		MessageBox.Show("API Berhasil Diupdate", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

	End Sub

End Class