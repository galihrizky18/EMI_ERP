Public Class N_EMI_SD_Transaksi_Validasi_Formula_Main_Produksi

	Private Sub N_EMI_SD_Transaksi_Validasi_Formula_Main_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Try
			OpenConn()

			Cmb_Susuan.Items.Clear()
			SQL = $"
				select Kode_Hierarki
				from N_EMI_Master_Hierarki_Formula
				where Kode_Perusahaan = '{KodePerusahaan}'
				order by Urutan
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Susuan.Items.Add(Dr("Kode_Hierarki"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()
	End Sub

	Private Sub Kosong()
		Cmb_Susuan.SelectedIndex = -1
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Txt_NoFormula.Text.Trim.Length = 0 Then
			MessageBox.Show("Terjadi Kesalahan No Formula Tidak Ditemukan, Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub

		ElseIf Cmb_Susuan.SelectedIndex = -1 Then
			MessageBox.Show("Harap Pilih Dahulu Susunan Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Susuan.DroppedDown = True
			Exit Sub
		End If

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim NoFormula As String = Txt_NoFormula.Text.Trim

			'=======================
			'=     CEK FORMULA     =
			'=======================
			SQL = "select Status, Flag_Validasi, Flag_Validasi_Main from Emi_Transaksi_Formulator "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{NoFormula}' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Faktur {NoFormula} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					ElseIf General_Class.CekNULL(Dr("Flag_Validasi")) <> "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Faktur {NoFormula} Belum Divalidasi Formulator", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					ElseIf General_Class.CekNULL(Dr("Flag_Validasi_Main")) <> "" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Faktur {NoFormula} Sudah Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"No Faktur {NoFormula} Tidak Ditemukan pada Transaksi Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = $"update Emi_Transaksi_Formulator set Flag_Validasi_Formula_Produksi = 'Y', Kode_Hierarki = '{Cmb_Susuan.Text}' "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{NoFormula}' "
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		N_EMI_Transaksi_Validasi_Formula_Main.Kosong()
		N_EMI_SD_Transaksi_Validasi_Formula_Main.Close()
		Me.Close()

	End Sub

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

End Class