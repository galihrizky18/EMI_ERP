Imports System.Net

Public Class N_EMI_Validasi_LIMS_Formula
	Dim arrcari As New ArrayList
	Dim Jenis = "ETA"

	Dim ValueBarcode As String = ""
	Public Property filter_tambahan As String
	Public Property asal As String

	Dim LvNoSplit, LvKode_Barang, LvNamaProduk, LvKodeFormula, LvHasil, LvSatuan, LvTanggalFormula As String

	Dim itemNoSplit As Integer = 0
	Dim itemKodeBarang As Integer = 1
	Dim itemNamaProduk As Integer = 2
	Dim itemKodeFormula As Integer = 3

	'  Dim itemNoSample As Integer = 4
	Dim itemHasil As Integer = 4

	Dim itemSatuan As Integer = 5
	Dim itemTanggalFormula As Integer = 6
	'  Dim itemPath As Integer = 8
	' Dim itemNoFakturSample As Integer = 9

	Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click

		If cmb_Formulator.SelectedIndex = -1 Then
			MessageBox.Show("Silahkan pilih parameter terlebih dahulu")
			cmb_Formulator.SelectedIndex = 0
			Exit Sub

		End If

		get_data_formulator("Y")
	End Sub

	Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
		If Lv_List_Barang.Items.Count = 0 Then
			MessageBox.Show("Silahkan pilih data terlebih dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		get_jam()

		Try
			OpenConn()

			Get_Isi_ListView(Lv_List_Barang.FocusedItem.Index)

			SQL = "select * From N_EMI_View_Laporan_formula_rpt where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and no_transaksi = '" & LvNoSplit & "' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then
					With Ds.Tables(0)
						Dim CrDoc As New N_EMI_CR_Laporan_Formulator
						With A_Place_For_Printing2
							CrDoc.SetDataSource(Ds)
							CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
							CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition Barang Lain"
							CrDoc.RecordSelectionFormula = " {N_EMI_View_Laporan_formula_rpt.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Laporan_formula_rpt.no_transaksi} = '" & LvNoSplit & "'"

							.Text = "Laporan Formulator"
							.CrystalReportViewer1.ReportSource = CrDoc
							.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
							.Refresh()
							.Show()
						End With
					End With
				Else
					MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				End If

			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Get_Isi_ListView(Lv_List_Barang.FocusedItem.Index)

			'========================================================
			' =============== CEK STATUS DATA =======================
			'========================================================

			'bersihkan dulu temporary
			SQL = "delete from N_EMI_LIMS_Berkas_Uji_Lab_Temp where  userid_cetak = '" & UserID & "' "
			ExecuteTrans(SQL)

			Dim listFormula As String = ""
			Dim formulas As New List(Of String)

			'SQL = "WITH CTE AS ("
			'SQL = SQL & "  SELECT U.No_Po_Sampel, "
			'SQL = SQL & "  CASE WHEN SUM(CASE WHEN U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) > 0 THEN 1 ELSE 0 END as has_validasi, "
			'SQL = SQL & "  CASE "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			'SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			'SQL = SQL & "  END as status_lock_view, "
			'SQL = SQL & "  CASE "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			'SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			'SQL = SQL & "  END as status_analisa_lab, "
			'SQL = SQL & "  CASE "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'TERKUNCI' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU HASIL UJI LAB' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			'SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			'SQL = SQL & "  END as status_palatabilitas "
			'SQL = SQL & "  FROM N_EMI_LIMS_Uji_Sampel as U "
			'SQL = SQL & "  JOIN N_EMI_LAB_Jenis_Analisa as A ON U.Id_Jenis_Analisa = A.id "
			'SQL = SQL & "  JOIN N_EMI_LIMS_Klasifikasi_Aktivitas_Lab as B ON A.Kode_Aktivitas_Lab = B.Kode_Aktivitas_Lab "
			'SQL = SQL & "  LEFT JOIN N_EMI_LIMS_Uji_Pra_Final as UPF ON U.No_Po_Sampel = UPF.No_Sampel "
			'SQL = SQL & "  WHERE U.Flag_Selesai = 'Y' AND U.Flag_Resampling IS NULL AND UPF.No_Sampel IS NULL "
			'SQL = SQL & "  GROUP BY U.No_Po_Sampel "
			'SQL = SQL & "), Final_Status AS ( "
			'SQL = SQL & "  SELECT b.Kode_Perusahaan, b.No_Transaksi, CTE.status_lock_view, CTE.status_analisa_lab, "
			'SQL = SQL & "  c.Kode_Formula, d.Tanggal, d.Jam, d.Kode_Barang, e.Nama AS Nama_Produk, d.Hasil, d.Satuan_Hasil, "
			'SQL = SQL & "  SUM(CASE WHEN ISNULL(cte.status_lock_view, '') <> 'DISETUJUI' OR ISNULL(Cte.status_analisa_lab, '') <> 'DISETUJUI' THEN 1 ELSE 0 END) "
			'SQL = SQL & "  OVER(PARTITION BY a.No_Split_Po) as Tidak_Disetujui, "
			'SQL = SQL & "  cte.no_po_sampel,c.Status as Status_Production, b.Flag_Validasi, b.Status as Status_Split ,d.Status "
			'SQL = SQL & "  FROM CTE "
			'SQL = SQL & "  JOIN N_LIMS_PO_Sampel a ON a.No_Sampel = CTE.No_Po_Sampel "
			'SQL = SQL & "  JOIN N_EMI_Transaksi_Trial_Split_Production_Order b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Split_Po = b.No_Transaksi "
			'SQL = SQL & "  JOIN N_EMI_Transaksi_Trial_Order_Produksi c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_PO = c.No_Faktur "
			'SQL = SQL & "  JOIN Emi_Transaksi_Formulator d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Formula = d.No_Faktur "
			'SQL = SQL & "  JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang = d.Kode_Barang "
			''  SQL = SQL & "  WHERE a.Status IS NULL "
			'SQL = SQL & ") "
			'SQL = SQL & "SELECT Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil,no_po_sampel ,"
			'SQL = SQL & "Status_Uji_Sampel,Status_Production ,Flag_Validasi,Status_Split,status"
			'SQL = SQL & "FROM Final_Status WHERE Tidak_Disetujui = 0 "
			'SQL = SQL & "GROUP BY Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil "

			'SQL = "WITH CTE AS ("
			'SQL = SQL & "  SELECT U.No_Po_Sampel, "
			'SQL = SQL & "  CASE WHEN SUM(CASE WHEN U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) > 0 THEN 1 ELSE 0 END as has_validasi, "
			'SQL = SQL & "  CASE "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			'SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			'SQL = SQL & "  END as status_lock_view, "
			'SQL = SQL & "  CASE "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			'SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			'SQL = SQL & "  END as status_analisa_lab, "
			'SQL = SQL & "  CASE "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'TERKUNCI' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU HASIL UJI LAB' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			'SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			'SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			'SQL = SQL & "  END as status_palatabilitas "
			'SQL = SQL & "  FROM N_EMI_LIMS_Uji_Sampel as U "
			'SQL = SQL & "  JOIN N_EMI_LAB_Jenis_Analisa as A ON U.Id_Jenis_Analisa = A.id "
			'SQL = SQL & "  JOIN N_EMI_LIMS_Klasifikasi_Aktivitas_Lab as B ON A.Kode_Aktivitas_Lab = B.Kode_Aktivitas_Lab "
			'SQL = SQL & "  LEFT JOIN N_EMI_LIMS_Uji_Pra_Final as UPF ON U.No_Po_Sampel = UPF.No_Sampel "
			'SQL = SQL & "  WHERE U.Flag_Selesai = 'Y' AND U.Flag_Resampling IS NULL AND UPF.No_Sampel IS NULL "
			'SQL = SQL & "  GROUP BY U.No_Po_Sampel "
			'SQL = SQL & "), Final_Status AS ( "
			'SQL = SQL & "  SELECT b.Kode_Perusahaan, b.No_Transaksi, CTE.status_lock_view, CTE.status_analisa_lab, "
			'SQL = SQL & "  c.Kode_Formula, d.Tanggal, d.Jam, d.Kode_Barang, e.Nama AS Nama_Produk, d.Hasil, d.Satuan_Hasil, "
			'SQL = SQL & "  SUM(CASE WHEN ISNULL(cte.status_lock_view, '') <> 'DISETUJUI' OR ISNULL(Cte.status_analisa_lab, '') <> 'DISETUJUI' THEN 1 ELSE 0 END) "
			'SQL = SQL & "  OVER(PARTITION BY a.No_Split_Po) as Tidak_Disetujui, "
			'SQL = SQL & "  cte.no_po_sampel,c.Status as Status_Production, b.Flag_Validasi, b.Status as Status_Split ,d.Status "
			'SQL = SQL & "  FROM CTE "
			'SQL = SQL & "  JOIN N_LIMS_PO_Sampel a ON a.No_Sampel = CTE.No_Po_Sampel "
			'SQL = SQL & "  JOIN N_EMI_Transaksi_Trial_Split_Production_Order b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Split_Po = b.No_Transaksi "
			'SQL = SQL & "  JOIN N_EMI_Transaksi_Trial_Order_Produksi c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_PO = c.No_Faktur "
			'SQL = SQL & "  JOIN Emi_Transaksi_Formulator d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Formula = d.No_Faktur "
			'SQL = SQL & "  JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang = d.Kode_Barang "
			'SQL = SQL & "  WHERE a.Status IS NULL AND b.Status IS NULL AND b.Flag_Validasi IS NULL AND c.Status IS NULL AND d.Status IS NULL "
			'SQL = SQL & ") "
			'SQL = SQL & "SELECT Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, no_po_sampel ,"
			'SQL = SQL & "Status_Production ,Flag_Validasi,Status_Split,status "
			'SQL = SQL & "FROM Final_Status WHERE Tidak_Disetujui = 0 "
			'SQL = SQL & "GROUP BY Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, no_po_sampel ,"
			'SQL = SQL & "Status_Production ,Flag_Validasi,Status_Split,status "

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
			SQL = SQL & " JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang = d.Kode_Barang "

			SQL = SQL & " WHERE a.Status IS NULL AND b.Status IS NULL AND b.Flag_Validasi IS NULL AND c.Status IS NULL AND d.Status IS NULL "
			SQL = SQL & ") "

			SQL = SQL & " SELECT Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, "
			SQL = SQL & " Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, "
			SQL = SQL & " No_Po_Sampel, Status_Production, Flag_Validasi, Status_Split, Status "

			SQL = SQL & " FROM Final_Status "
			SQL = SQL & " WHERE Tidak_Disetujui = 0 "
			SQL = SQL & " AND No_Transaksi = '" & LvNoSplit & "' "

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

							Dim url = ""
							SQL = "select top(1) file_path From N_EMI_LIMS_BErkas_Uji_lab  "
							SQL = SQL & "where no_sampel = '" & .Rows(i).Item("no_po_sampel") & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									If General_Class.CekNULL(Dr("file_path")) = "" Then
										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Foto lockview NULL", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									Else
										url = Dr("file_path")
									End If
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Foto lockview tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							SQL = "update N_LIMS_PO_Sampel set "
							SQL = SQL & "Flag_Validasi_Formulator_Desktop = 'Y' , "
							SQL = SQL & "Tanggal_Validasi_Formulator_Desktop = '" & Format(tgl_skg, "yyyy-MM-dd") & "' , "
							SQL = SQL & "Jam_Validasi_Formulator_Desktop = '" & Format(tgl_skg, "HH:mm:ss") & "' , "
							SQL = SQL & "Id_User_Validasi_Formulator_Desktop = '" & UserID & "' "
							SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and no_sampel = '" & .Rows(i).Item("no_po_sampel") & "' "

							ExecuteTrans(SQL)

							Dim signedUrl As String = GCSHelper.GenerateSignedUrl(bucketFormulator, url, 2)
							Dim imageBytes As Byte()
							Using webClient As New WebClient()
								imageBytes = webClient.DownloadData(signedUrl)
							End Using

							'==================== COMPRESS IMAGE ====================
							Dim compressedBytes As Byte() = CompressAndResizeImage(imageBytes, 90, 2000)

							Cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = compressedBytes

							SQL = "insert into N_EMI_LIMS_BErkas_Uji_lab_temp (No_Faktur,No_Sampel,Berkas_Key,File_Path,Id_Jenis_Analisa,	Gambar_Convert_Image, tanggal_cetak,jam_cetak,userid_cetak) "
							SQL = SQL & "select top(1) No_Faktur,No_Sampel,Berkas_Key,File_Path,Id_Jenis_Analisa, @image, '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' , '" & UserID & "' from N_EMI_LIMS_BErkas_Uji_lab "
							SQL = SQL & "where no_sampel = '" & .Rows(i).Item("no_po_sampel") & "' order by Id_Berkas_Uji_Lab desc "

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
			SQL = SQL & "where no_transaksi = '" & LvNoSplit & "' "
			SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			Get_Isi_ListView(Lv_List_Barang.FocusedItem.Index)

			SQL = "select * From N_EMI_View_Laporan_formula_rpt where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and no_transaksi = '" & LvNoSplit & "' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then
					With Ds.Tables(0)
						Dim CrDoc As New N_EMI_CR_Laporan_Formulator
						With A_Place_For_Printing2
							CrDoc.SetDataSource(Ds)
							CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
							CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition Barang Lain"
							CrDoc.RecordSelectionFormula = " {N_EMI_View_Laporan_formula_rpt.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Laporan_formula_rpt.no_transaksi} = '" & LvNoSplit & "'"

							.Text = "Laporan Formulator"
							.CrystalReportViewer1.ReportSource = CrDoc
							.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
							.Refresh()
							.Show()
						End With
					End With
				Else
					MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				End If

			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Dim itemIsTimbangMasuk As Integer = 11

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		kosong()
	End Sub

	Dim itemIsTimbangKeluar As Integer = 12

	Private Function CekNothing(ByVal str As String) As String
		Dim hasil As String = ""

		If str Is Nothing Then
			hasil = ""
		Else
			hasil = str
		End If

		Return hasil
	End Function

	Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)

		LvNoSplit = Lv_List_Barang.Items(NoIndex).SubItems(itemNoSplit).Text
		LvKode_Barang = Lv_List_Barang.Items(NoIndex).SubItems(itemKodeBarang).Text
		LvNamaProduk = Lv_List_Barang.Items(NoIndex).SubItems(itemNamaProduk).Text
		LvKodeFormula = Lv_List_Barang.Items(NoIndex).SubItems(itemKodeFormula).Text
		'  LvNoSample = Lv_List_Barang.Items(NoIndex).SubItems(itemNoSample).Text
		LvHasil = Lv_List_Barang.Items(NoIndex).SubItems(itemHasil).Text
		LvSatuan = Lv_List_Barang.Items(NoIndex).SubItems(itemSatuan).Text
		LvTanggalFormula = Lv_List_Barang.Items(NoIndex).SubItems(itemTanggalFormula).Text
		'   LvPath = Lv_List_Barang.Items(NoIndex).SubItems(itemPath).Text
		' LvNOfakturSample = Lv_List_Barang.Items(NoIndex).SubItems(itemNoFakturSample).Text
	End Sub

	Private Sub Popup_Timbang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Popup_Timbang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Try
			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			Label1.Text = "Validasi - Formulator"

			Lv_List_Barang.Columns.Clear()

			Lv_List_Barang.Columns.Add("No Split", 130, HorizontalAlignment.Left).DisplayIndex = 0
			Lv_List_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left).DisplayIndex = 1
			Lv_List_Barang.Columns.Add("Nama Produk", 250, HorizontalAlignment.Left).DisplayIndex = 2
			Lv_List_Barang.Columns.Add("Kode Formula", 130, HorizontalAlignment.Left).DisplayIndex = 3
			'  Lv_List_Barang.Columns.Add("No Sample", 130, HorizontalAlignment.Left).DisplayIndex = 4
			Lv_List_Barang.Columns.Add("Hasil", 130, HorizontalAlignment.Right).DisplayIndex = 4
			Lv_List_Barang.Columns.Add("Satuan", 110, HorizontalAlignment.Center).DisplayIndex = 5
			Lv_List_Barang.Columns.Add("Tanggal Formula", 110, HorizontalAlignment.Center).DisplayIndex = 6

			'   Lv_List_Barang.Columns.Add("Path", 0, HorizontalAlignment.Left).DisplayIndex = 8
			'    Lv_List_Barang.Columns.Add("No Faktur sample", 0, HorizontalAlignment.Left).DisplayIndex = 9

			Lv_List_Barang.View = View.Details

			cmb_Formulator.Items.Add("No Split") : arrcari.Add("no_transaksi")
			cmb_Formulator.Items.Add("Kode Formula") : arrcari.Add("Kode_Formula")
			cmb_Formulator.Items.Add("Kode Barang") : arrcari.Add("kode_barang")
			cmb_Formulator.Items.Add("Nama Produk") : arrcari.Add("Nama")

			'Menangkap semua inputan dari keyboard
			Me.KeyPreview = True

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		kosong()

	End Sub

	Public Sub kosong()
		TxtValPencarian.Clear()
		cmb_Formulator.SelectedIndex = 0
		get_data_formulator("T")
	End Sub

	Private Sub get_data_formulator(ByRef cari As String)
		Try
			OpenConn()

			Lv_List_Barang.Items.Clear()
			Lv_List_Barang.View = View.Details

			'SQL = "select a.Kode_Perusahaan, ab.No_Transaksi, a.Kode_Formula,f.no_faktur,f.No_Po_Sampel, b.Tanggal, b.Jam, b.Kode_Barang, "
			'SQL = SQL & "d.Nama as Nama_Produk, b.hasil, b.Satuan_Hasil,h.File_Path "
			'SQL = SQL & "from N_EMI_Transaksi_Trial_Order_Produksi a "
			'SQL = SQL & "inner join N_EMI_Transaksi_Trial_Split_Production_Order ab "
			'SQL = SQL & "on a.Kode_Perusahaan = ab.Kode_Perusahaan and a.No_Faktur = ab.No_PO "
			'SQL = SQL & "inner join Emi_Transaksi_Formulator b "
			'SQL = SQL & "on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Formula = b.No_Faktur "
			'SQL = SQL & "inner join Barang d "
			'SQL = SQL & "on b.Kode_Perusahaan = d.Kode_Perusahaan "
			'SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner "
			'SQL = SQL & "and b.Kode_Barang = d.Kode_Barang "
			'SQL = SQL & "inner join N_LIMS_PO_Sampel e "
			'SQL = SQL & "on ab.Kode_Perusahaan = e.Kode_Perusahaan and ab.No_Transaksi = e.No_Split_PO "
			'SQL = SQL & "inner join N_EMI_LIMS_Uji_Sampel f "
			'SQL = SQL & "on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Sampel = f.No_PO_Sampel "
			'SQL = SQL & "inner join N_EMI_LIMS_Uji_Pra_Final g "
			'SQL = SQL & "on f.No_PO_Sampel = g.No_Sampel "
			'SQL = SQL & "left join N_EMI_LIMS_Berkas_Uji_Lab h on  f.No_Faktur = h.No_Faktur and f.No_Po_Sampel = h.No_Sampel "
			'SQL = SQL & "where a.Status is null "
			'SQL = SQL & "and ab.Status is null "
			'SQL = SQL & "and f.Flag_Approval is null "
			'SQL = SQL & "and f.Status_Keputusan_Sampel = 'terima' "
			'SQL = SQL & "and f.Status is null "
			'SQL = SQL & "and ab.Flag_Validasi is null "

			SQL = "WITH CTE AS ("
			SQL = SQL & "  SELECT U.No_Po_Sampel, "
			SQL = SQL & "  CASE WHEN SUM(CASE WHEN U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) > 0 THEN 1 ELSE 0 END as has_validasi, "
			SQL = SQL & "  CASE "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			SQL = SQL & "  END as status_lock_view, "
			SQL = SQL & "  CASE "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			SQL = SQL & "  END as status_analisa_lab, "
			SQL = SQL & "  CASE "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'TERKUNCI' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU HASIL UJI LAB' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			SQL = SQL & "  END as status_palatabilitas "
			SQL = SQL & "  FROM N_EMI_LIMS_Uji_Sampel as U "
			SQL = SQL & "  JOIN N_EMI_LAB_Jenis_Analisa as A ON U.Id_Jenis_Analisa = A.id "
			SQL = SQL & "  JOIN N_EMI_LIMS_Klasifikasi_Aktivitas_Lab as B ON A.Kode_Aktivitas_Lab = B.Kode_Aktivitas_Lab "
			SQL = SQL & "  LEFT JOIN N_EMI_LIMS_Uji_Pra_Final as UPF ON U.No_Po_Sampel = UPF.No_Sampel "
			'   SQL = SQL & "  WHERE U.Flag_Selesai = 'Y' AND U.Flag_Resampling IS NULL AND UPF.No_Sampel IS NULL "
			SQL &= " WHERE "
			SQL &= "     u.Flag_Approval = 'Y' "
			SQL &= "     AND EXISTS ( "
			SQL &= "         SELECT 1 FROM N_EMI_LIMS_Uji_Pra_Final x "
			SQL &= "         WHERE x.No_Sampel = u.No_Po_Sampel "
			SQL &= "     ) "
			SQL &= "     AND u.Flag_Selesai = 'Y' "
			SQL &= "     AND u.Flag_Resampling IS NULL "
			SQL &= "     AND u.Status IS NULL "

			SQL = SQL & "  GROUP BY U.No_Po_Sampel "
			SQL = SQL & "), Final_Status AS ( "
			SQL = SQL & "  SELECT b.Kode_Perusahaan, b.No_Transaksi, CTE.status_lock_view, CTE.status_analisa_lab, "
			SQL = SQL & "  c.Kode_Formula, d.Tanggal, d.Jam, d.Kode_Barang, e.Nama AS Nama_Produk, d.Hasil, d.Satuan_Hasil, "
			SQL = SQL & "  SUM(CASE WHEN ISNULL(cte.status_lock_view, '') <> 'DISETUJUI' OR ISNULL(Cte.status_analisa_lab, '') <> 'DISETUJUI' THEN 1 ELSE 0 END) "
			SQL = SQL & "  OVER(PARTITION BY a.No_Split_Po) as Tidak_Disetujui "
			SQL = SQL & "  FROM CTE "
			SQL = SQL & "  JOIN N_LIMS_PO_Sampel a ON a.No_Sampel = CTE.No_Po_Sampel "
			SQL = SQL & "  JOIN N_EMI_Transaksi_Trial_Split_Production_Order b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Split_Po = b.No_Transaksi "
			SQL = SQL & "  JOIN N_EMI_Transaksi_Trial_Order_Produksi c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_PO = c.No_Faktur "
			SQL = SQL & "  JOIN Emi_Transaksi_Formulator d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Formula = d.No_Faktur "
			SQL = SQL & "  JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang = d.Kode_Barang "
			SQL = SQL & "  WHERE a.Status IS NULL AND b.Status IS NULL AND b.Flag_Validasi IS NULL AND c.Status IS NULL AND d.Status IS NULL "
			SQL = SQL & ") "
			SQL = SQL & "SELECT Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil "
			SQL = SQL & "FROM Final_Status WHERE Tidak_Disetujui = 0 "
			SQL = SQL & "GROUP BY Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil "

			If cari = "Y" Then
				SQL = SQL & "and  " & arrcari.Item(cmb_Formulator.SelectedIndex) & " like '%" & TxtValPencarian.Text.Trim & "%' "
			End If

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lvw As ListViewItem

					Lvw = Lv_List_Barang.Items.Add(Dr("No_Transaksi"))

					Lvw.SubItems.Add(Dr("Kode_Barang"))
					Lvw.SubItems.Add(Dr("Nama_Produk"))
					Lvw.SubItems.Add(Dr("Kode_Formula"))
					'   Lvw.SubItems.Add(Dr("No_Po_Sampel"))
					Lvw.SubItems.Add(Format(Dr("hasil"), "N2"))
					Lvw.SubItems.Add(Dr("Satuan_Hasil"))

					Lvw.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					'     Lvw.SubItems.Add(Dr("File_Path"))
					'    Lvw.SubItems.Add(Dr("no_faktur"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

End Class