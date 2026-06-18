Public Class N_EMI_SD_Transaksi_Validasi_Trial_Formula
	Public NoSplit As String

	Private Sub N_EMI_SD_Transaksi_Validasi_Formula_Main_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Kosong()
	End Sub

	Private Sub Kosong()
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		get_jam()

		Try

			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'========================================================
			' =============== CEK STATUS DATA =======================
			'========================================================

			Dim listFormula As String = ""
			Dim formulas As New List(Of String)
			SQL = "WITH CTE AS ("
			SQL = SQL & " SELECT U.No_Po_Sampel, "

			SQL = SQL & " CASE WHEN SUM(CASE WHEN U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) > 0 "
			SQL = SQL & " THEN 1 ELSE 0 END as has_validasi, "

			SQL = SQL & " CASE "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = "
			SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			SQL = SQL & " ELSE 'MENUNGGU VALIDASI' END as status_lock_view, "

			SQL = SQL & " CASE "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > "
			SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = "
			SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			SQL = SQL & " ELSE 'MENUNGGU VALIDASI' END as status_analisa_lab, "

			SQL = SQL & " CASE "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'TERKUNCI' "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > "
			SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) > "
			SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU HASIL UJI LAB' "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = "
			SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			SQL = SQL & " ELSE 'MENUNGGU VALIDASI' END as status_palatabilitas "

			SQL = SQL & " FROM N_EMI_LIMS_Uji_Sampel U "
			SQL = SQL & " JOIN N_EMI_LAB_Jenis_Analisa A ON U.Id_Jenis_Analisa = A.id "
			SQL = SQL & " JOIN N_EMI_LIMS_Klasifikasi_Aktivitas_Lab B ON A.Kode_Aktivitas_Lab = B.Kode_Aktivitas_Lab "
			SQL = SQL & " LEFT JOIN N_EMI_LIMS_Uji_Pra_Final UPF ON U.No_Po_Sampel = UPF.No_Sampel "

			SQL = SQL & " WHERE U.Flag_Approval = 'Y' "
			SQL = SQL & " AND EXISTS (SELECT 1 FROM N_EMI_LIMS_Uji_Pra_Final x WHERE x.No_Sampel = U.No_Po_Sampel) "
			SQL = SQL & " AND U.Flag_Selesai = 'Y' "
			SQL = SQL & " AND U.Flag_Resampling IS NULL "
			SQL = SQL & " AND U.Status IS NULL "

			SQL = SQL & " GROUP BY U.No_Po_Sampel "
			SQL = SQL & "), Final_Status AS ( "

			SQL = SQL & " SELECT b.Kode_Perusahaan, b.No_Transaksi, CTE.status_lock_view, CTE.status_analisa_lab, "
			SQL = SQL & " c.Kode_Formula, d.Tanggal, d.Jam, d.Kode_Barang, e.Nama AS Nama_Produk, d.Hasil, d.Satuan_Hasil, "

			SQL = SQL & " CTE.No_Po_Sampel, "
			SQL = SQL & " c.Status as Status_Production, b.Flag_Validasi, b.Status as Status_Split, d.Status, "

			SQL = SQL & " SUM(CASE WHEN ISNULL(CTE.status_lock_view, '') <> 'DISETUJUI' "
			SQL = SQL & " OR ISNULL(CTE.status_analisa_lab, '') <> 'DISETUJUI' THEN 1 ELSE 0 END) "
			SQL = SQL & " OVER(PARTITION BY a.No_Split_Po) as Tidak_Disetujui "

			SQL = SQL & " FROM CTE "
			SQL = SQL & " JOIN N_LIMS_PO_Sampel a ON a.No_Sampel = CTE.No_Po_Sampel "
			SQL = SQL & " JOIN N_EMI_Transaksi_Trial_Split_Production_Order b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Split_Po = b.No_Transaksi "
			SQL = SQL & " JOIN N_EMI_Transaksi_Trial_Order_Produksi c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_PO = c.No_Faktur "
			SQL = SQL & " JOIN Emi_Transaksi_Formulator d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Formula = d.No_Faktur "
			SQL = SQL & " JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang_Inq = d.Kode_Barang "

			SQL = SQL & " WHERE a.Status IS NULL AND b.Status IS NULL AND b.Flag_Validasi IS NULL AND c.Status IS NULL AND d.Status IS NULL "
			SQL = SQL & ") "

			SQL = SQL & " SELECT Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, "
			SQL = SQL & " Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, "
			SQL = SQL & " No_Po_Sampel, Status_Production, Flag_Validasi, Status_Split, Status "

			SQL = SQL & " FROM Final_Status "
			SQL = SQL & " WHERE Tidak_Disetujui = 0 "
			SQL = SQL & " AND No_Transaksi = '" & NoSplit & "' "

			SQL = SQL & " GROUP BY Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, "
			SQL = SQL & " Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, "
			SQL = SQL & " No_Po_Sampel, Status_Production, Flag_Validasi, Status_Split, Status "

			Using ds = BindingTrans(SQL)
				With ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							If General_Class.CekNULL(.Rows(i).Item("Flag_Validasi")) = "Y" Then

								CloseTrans()
								CloseConn()
								MessageBox.Show("No Split ini sudah di validasi, tidak bisa validasi ulang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then

								CloseTrans()
								CloseConn()
								MessageBox.Show("Nomor Formula pada split ini telah dibatalkan, coba cek kembali", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							If General_Class.CekNULL(.Rows(i).Item("status_split")) = "Y" Then

								CloseTrans()
								CloseConn()
								MessageBox.Show("No Split telah dibatalkan sebelumnya, coba refresh dan cek kembali", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							If General_Class.CekNULL(.Rows(i).Item("Status_Production")) = "Y" Then

								CloseTrans()
								CloseConn()
								MessageBox.Show("Production order telah dibatalkan sebelumnya, coba refresh dan cek kembali", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							formulas.Add(.Rows(i).Item("no_po_sampel"))

							SQL = "update N_LIMS_PO_Sampel set "
							SQL = SQL & "Flag_Validasi_Formulator_Desktop = 'Y' , "
							SQL = SQL & "Tanggal_Validasi_Formulator_Desktop = '" & Format(tgl_skg, "yyyy-MM-dd") & "' , "
							SQL = SQL & "Jam_Validasi_Formulator_Desktop = '" & Format(tgl_skg, "HH:mm:ss") & "' , "
							SQL = SQL & "Id_User_Validasi_Formulator_Desktop = '" & UserID & "' "
							SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and no_sampel = '" & .Rows(i).Item("no_po_sampel") & "' "
							ExecuteTrans(SQL)
						Next
					Else

					End If
				End With

				listFormula = String.Join(",", formulas)

			End Using

			'==================== Harus dibuka, ditutup sementara biar gk ilang  ====================
			SQL = "update N_EMI_Transaksi_Trial_Split_Production_Order set flag_validasi = 'Y', "
			SQL = SQL & "userid_validasi = '" & UserID & "', "
			SQL = SQL & "tanggal_validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
			SQL = SQL & "jam_validasi = '" & Format(tgl_skg, "HH:mm:ss") & "' "
			SQL = SQL & "where no_transaksi = '" & NoSplit & "' "
			SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

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
                SET Flag_Selesai_Trial_Kitchen = 'Y', Tanggal_Selesai_Trial_Kitchen = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Selesai_Trial_Kitchen = '{Format(tgl_skg, "HH:mm:ss")}', UserID_Selesai_Trial_Kitchen = '{UserID}'
                WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Faktur = '{NoFormula}'
            "
			ExecuteTrans(SQL)

			SQL = $"
				;WITH CTE AS (
					SELECT TOP 1 b.*
					FROM N_EMI_Transaksi_Trial_Order_Produksi a
					JOIN N_EMI_Transaksi_Trial_Split_Production_Order b 
						ON a.Kode_Perusahaan = b.Kode_Perusahaan 
						AND a.No_Faktur = b.No_PO
					WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
						AND a.Kode_Formula = '{NoFormula}'
						AND a.Status is null
						AND b.Status is null
						AND b.No_Transaksi = '{NoSplit}'
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

		N_EMI_Transaksi_Validasi_Trial_Formula.kosong()
		Me.Close()

	End Sub
End Class