Public Class N_EMI_SD_Transaksi_Validasi_Formula_Trial_Produksi
	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		get_jam()

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
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"No Faktur {NoFormula} Tidak Ditemukan pada Transaksi Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = $"
                UPDATE Emi_Transaksi_Formulator
                SET Flag_Selesai_Trial_Produksi = 'Y', Tanggal_Selesai_Trial_Produksi = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Selesai_Trial_Produksi = '{Format(tgl_skg, "HH:mm:ss")}', UserID_Selesai_Trial_Produksi = '{UserID}'
                WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Faktur = '{NoFormula}'
            "
			ExecuteTrans(SQL)

			SQL = $"
				;WITH CTE AS (
					SELECT TOP 1 a.*
					FROM Emi_Split_Production_Order a
					JOIN EMI_Order_Produksi b 
						ON b.Kode_Perusahaan = a.Kode_Perusahaan 
						AND b.No_Faktur = a.No_PO
					WHERE b.Kode_Perusahaan = '{KodePerusahaan}'
						AND b.Kode_Formula = '{NoFormula}'
						AND b.Status is null
					ORDER BY b.Tanggal DESC, b.Jam DESC
				)
				UPDATE CTE
				SET Deskripsi = '{TBDeskripsi.Text.Trim}', Keterangan = '{TB_Keterangan.Rtf}'
			"
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

		N_EMI_Transaksi_Validasi_Formula_Trial_Produksi.kosong()
		Me.Close()
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		TBDeskripsi.Text = ""
	End Sub
End Class