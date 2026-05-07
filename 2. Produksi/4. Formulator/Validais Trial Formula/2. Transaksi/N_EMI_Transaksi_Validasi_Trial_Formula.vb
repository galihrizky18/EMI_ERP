Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports System.Windows.Forms.DataVisualization.Charting
Imports PdfiumViewer

Public Class N_EMI_Transaksi_Validasi_Trial_Formula
	Dim arrcari As New ArrayList
	Dim Jenis = "ETA"

	Dim ValueBarcode As String = ""
	Public Property filter_tambahan As String
	Public Property asal As String

	Dim Dgv_NoSPlit, Dgv_KodeBarang, Dgv_NamaBarang, Dgv_KodeFormula, Dgv_Hasil, Dgv_Satuan, Dgv_TanggalFormula, Dgv_Validasi

	Dim Cell_NoSplit As Integer = 0
	Dim Cell_KodeFormula As Integer = 1
	Dim Cell_KodeBarang As Integer = 2
	Dim Cell_NamaBarang As Integer = 3
	Dim Cell_Hasil As Integer = 4
	Dim Cell_Satuan As Integer = 5
	Dim Cell_TanggalFormula As Integer = 6
	Dim Cell_TanggalSplit As Integer = 7
	Dim Cell_BtnValidasi As Integer = 8

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

			cmb_Formulator.Items.Add("No Split") : arrcari.Add("no_transaksi")
			cmb_Formulator.Items.Add("Kode Formula") : arrcari.Add("Kode_Formula")
			cmb_Formulator.Items.Add("Tanggal Formula") : arrcari.Add("TANGGAL_FORMULA")
			cmb_Formulator.Items.Add("Kode Barang") : arrcari.Add("kode_barang")
			cmb_Formulator.Items.Add("Nama Produk") : arrcari.Add("Nama_Produk")

			'Menangkap semua inputan dari keyboard
			Me.KeyPreview = True

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Pnl_Filter_Date.Location = New Point(223, 67)

		kosong()

	End Sub

	Public Sub kosong()
		TxtValPencarian.Clear()
		cmb_Formulator.SelectedIndex = 0

		Btn_Scan.Location = New Point(420, 64)
		Btn_Refresh.Location = New Point(520, 64)

		TxtValPencarian.Visible = True
		Pnl_Filter_Date.Visible = False

		DateTimePicker1.Value = Date.Now
		DateTimePicker2.Value = Date.Now

		get_data_formulator("T")
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		kosong()
	End Sub

	Private Sub get_data_formulator(ByRef cari As String)
		Try
			OpenConn()

			Dgv_Parent.Rows.Clear()

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
			SQL = SQL & " WHERE "
			SQL = SQL & "     u.Flag_Approval = 'Y' "
			SQL = SQL & "     AND EXISTS ( "
			SQL = SQL & "         SELECT 1 FROM N_EMI_LIMS_Uji_Pra_Final x "
			SQL = SQL & "         WHERE x.No_Sampel = u.No_Po_Sampel "
			SQL = SQL & "     ) "
			SQL = SQL & "     AND u.Flag_Selesai = 'Y' "
			SQL = SQL & "     AND u.Flag_Resampling IS NULL "
			SQL = SQL & "     AND u.Status IS NULL "

			SQL = SQL & "  GROUP BY U.No_Po_Sampel "
			SQL = SQL & "), Final_Status AS ( "
			SQL = SQL & "  SELECT b.Kode_Perusahaan, b.No_Transaksi, CTE.status_lock_view, CTE.status_analisa_lab, "
			SQL = SQL & "  c.Kode_Formula, d.Tanggal, d.Jam, d.Kode_Barang, e.Nama AS Nama_Produk, d.Hasil, d.Satuan_Hasil, "
			SQL = SQL & "  SUM(CASE WHEN ISNULL(cte.status_lock_view, '') <> 'DISETUJUI' OR ISNULL(Cte.status_analisa_lab, '') <> 'DISETUJUI' THEN 1 ELSE 0 END) "
			SQL = SQL & "  OVER(PARTITION BY a.No_Split_Po) as Tidak_Disetujui, b.Jumlah as Jumlah_Split, b.Satuan as Satuan_Split, b.tanggal as Tanggal_Split "
			SQL = SQL & "  FROM CTE "
			SQL = SQL & "  JOIN N_LIMS_PO_Sampel a ON a.No_Sampel = CTE.No_Po_Sampel "
			SQL = SQL & "  JOIN N_EMI_Transaksi_Trial_Split_Production_Order b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Split_Po = b.No_Transaksi "
			SQL = SQL & "  JOIN N_EMI_Transaksi_Trial_Order_Produksi c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_PO = c.No_Faktur "
			SQL = SQL & "  JOIN Emi_Transaksi_Formulator d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Formula = d.No_Faktur "
			SQL = SQL & "  JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang_inq = d.Kode_Barang "
			SQL = SQL & "  WHERE a.Status IS NULL AND b.Status IS NULL AND b.Flag_Validasi IS NULL AND c.Status IS NULL AND d.Status IS NULL "
			SQL = SQL & ") "
			SQL = SQL & "SELECT Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, Jumlah_Split, Satuan_Split, Tanggal_Split "
			SQL = SQL & "FROM Final_Status WHERE Tidak_Disetujui = 0 "
			If cari = "Y" Then

				If arrcari(cmb_Formulator.SelectedIndex).ToString.Trim = "TANGGAL_FORMULA" Then

					SQL = SQL & "and Tanggal between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
				Else
					SQL = SQL & "and  " & arrcari.Item(cmb_Formulator.SelectedIndex) & " like '%" & TxtValPencarian.Text.Trim & "%' "
				End If

			End If
			SQL = SQL & "GROUP BY Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, Jumlah_Split, Satuan_Split, Tanggal_Split "

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					With Dgv_Parent.Rows(Dgv_Parent.Rows.Add)
						.Cells(Cell_NoSplit).Value = Dr("No_Transaksi")
						.Cells(Cell_KodeBarang).Value = Dr("Kode_Barang")
						.Cells(Cell_NamaBarang).Value = Dr("Nama_Produk")
						.Cells(Cell_KodeFormula).Value = Dr("Kode_Formula")
						.Cells(Cell_Hasil).Value = Dr("Jumlah_Split")
						.Cells(Cell_Satuan).Value = Dr("Satuan_Split")
						.Cells(Cell_TanggalFormula).Value = Format(Dr("Tanggal"), "dd MMM yyyy")
						.Cells(Cell_TanggalSplit).Value = Format(Dr("Tanggal_Split"), "dd MMM yyyy")
						.Cells(Cell_BtnValidasi).Value = "Validasi"
					End With

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click

		If cmb_Formulator.SelectedIndex = -1 Then
			MessageBox.Show("Silahkan pilih parameter terlebih dahulu")
			cmb_Formulator.SelectedIndex = 0
			Exit Sub

		End If

		If arrcari(cmb_Formulator.SelectedIndex).ToString.Trim = "TANGGAL_FORMULA" Then
			If DateTimePicker1.Value.Date > DateTimePicker2.Value.Date Then
				MessageBox.Show("Tanggal awal tidak boleh melewati tanggal akhir!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				DateTimePicker1.Value = Date.Now
				DateTimePicker2.Value = Date.Now
				Exit Sub
			End If
		Else
			If TxtValPencarian.Text.Trim.Length = 0 Then
				MessageBox.Show("Harap Isi Value Filter Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				TxtValPencarian.Focus()
				Exit Sub
			End If
		End If

		get_data_formulator("Y")
	End Sub

	Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
		Exit Sub

		get_jam()

		Try

			'TODO JANGAN LUPA DI HAPUS
			Exit Try

			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Get_Isi_ListView(Dgv_Parent.CurrentRow.Index)

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
			SQL = SQL & " JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang_Inq = d.Kode_Barang "

			SQL = SQL & " WHERE a.Status IS NULL AND b.Status IS NULL AND b.Flag_Validasi IS NULL AND c.Status IS NULL AND d.Status IS NULL "
			SQL = SQL & ") "

			SQL = SQL & " SELECT Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, "
			SQL = SQL & " Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, "
			SQL = SQL & " No_Po_Sampel, Status_Production, Flag_Validasi, Status_Split, Status "

			SQL = SQL & " FROM Final_Status "
			SQL = SQL & " WHERE Tidak_Disetujui = 0 "
			SQL = SQL & " AND No_Transaksi = '" & Dgv_NoSPlit & "' "

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
			SQL = SQL & "where no_transaksi = '" & Dgv_NoSPlit & "' "
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

			Get_Isi_ListView(Dgv_Parent.CurrentRow.Index)

			SQL = "select * From N_EMI_View_Laporan_formula_rpt where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and no_transaksi = '" & Dgv_NoSPlit & "' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then
					With Ds.Tables(0)
						Dim CrDoc As New N_EMI_CR_Laporan_Formulator
						With A_Place_For_Printing2
							CrDoc.SetDataSource(Ds)
							CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
							CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition Barang Lain"
							CrDoc.RecordSelectionFormula = " {N_EMI_View_Laporan_formula_rpt.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Laporan_formula_rpt.no_transaksi} = '" & Dgv_NoSPlit & "'"

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

	Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)

		'LvNoSplit = Lv_List_Barang.Items(NoIndex).SubItems(itemNoSplit).Text
		'LvKode_Barang = Lv_List_Barang.Items(NoIndex).SubItems(itemKodeBarang).Text
		'LvNamaProduk = Lv_List_Barang.Items(NoIndex).SubItems(itemNamaProduk).Text
		'LvKodeFormula = Lv_List_Barang.Items(NoIndex).SubItems(itemKodeFormula).Text
		''  LvNoSample = Lv_List_Barang.Items(NoIndex).SubItems(itemNoSample).Text
		'LvHasil = Lv_List_Barang.Items(NoIndex).SubItems(itemHasil).Text
		'LvSatuan = Lv_List_Barang.Items(NoIndex).SubItems(itemSatuan).Text
		'LvTanggalFormula = Lv_List_Barang.Items(NoIndex).SubItems(itemTanggalFormula).Text
		''   LvPath = Lv_List_Barang.Items(NoIndex).SubItems(itemPath).Text
		'' LvNOfakturSample = Lv_List_Barang.Items(NoIndex).SubItems(itemNoFakturSample).Text

		Dgv_NoSPlit = Dgv_Parent.Rows(NoIndex).Cells(Cell_NoSplit).Value
		Dgv_KodeBarang = Dgv_Parent.Rows(NoIndex).Cells(Cell_KodeBarang).Value
		Dgv_NamaBarang = Dgv_Parent.Rows(NoIndex).Cells(Cell_NamaBarang).Value
		Dgv_KodeFormula = Dgv_Parent.Rows(NoIndex).Cells(Cell_KodeFormula).Value
		Dgv_Hasil = Dgv_Parent.Rows(NoIndex).Cells(Cell_Hasil).Value
		Dgv_Satuan = Dgv_Parent.Rows(NoIndex).Cells(Cell_Satuan).Value
		Dgv_TanggalFormula = Dgv_Parent.Rows(NoIndex).Cells(Cell_TanggalFormula).Value
		Dgv_Validasi = Dgv_Parent.Rows(NoIndex).Cells(Cell_BtnValidasi).Value

	End Sub

	Private Sub cmb_Formulator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Formulator.SelectedIndexChanged
		If cmb_Formulator.Items.Count = 0 Or cmb_Formulator.SelectedIndex = -1 Then Exit Sub

		'Me.SuspendLayout()

		If arrcari(cmb_Formulator.SelectedIndex).ToString.Trim = "TANGGAL_FORMULA" Then
			Btn_Scan.Location = New Point(666, 64)
			Btn_Refresh.Location = New Point(766, 64)

			TxtValPencarian.Visible = False
			Pnl_Filter_Date.Visible = True
		Else

			Btn_Scan.Location = New Point(420, 64)
			Btn_Refresh.Location = New Point(520, 64)

			TxtValPencarian.Visible = True
			Pnl_Filter_Date.Visible = False
		End If

		'Me.ResumeLayout()

		TxtValPencarian.Clear()
		DateTimePicker1.Value = Date.Now
		DateTimePicker2.Value = Date.Now

	End Sub

	Private Sub Dgv_Formula_Parent_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		If e.ColumnIndex = Cell_BtnValidasi Then

			Get_Isi_ListView(Dgv_Parent.CurrentRow.Index)

			If (MessageBox.Show($"Yakin ingin Melakukan Validasi Split {Dgv_NoSPlit} ini ???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

			get_jam()

			Dim isCompleted As Boolean = False

			Try

				OpenConn()
				Cmd.Transaction = Cn.BeginTransaction

				'========================================================
				' =============== CEK STATUS DATA =======================
				'========================================================

				'bersihkan dulu temporary
				SQL = "delete from N_EMI_LIMS_Berkas_Uji_Lab_Temp where  userid_cetak = '" & UserID & "' "
				ExecuteTrans(SQL)

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
				SQL = SQL & " AND No_Transaksi = '" & Dgv_NoSPlit & "' "

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

								Cmd.Parameters.Clear()
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
				SQL = SQL & "where no_transaksi = '" & Dgv_NoSPlit & "' "
				SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
				ExecuteTrans(SQL)

				Cmd.Transaction.Commit()
				CloseTrans()
				CloseConn()
				isCompleted = True
			Catch ex As Exception
				CloseTrans()
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			If isCompleted Then

				Dim listSatuan As New List(Of String)
				Try
					OpenConn()

					listSatuan.Clear()
					SQL = "select Satuan from EMI_Satuan where Flag_Tampil_Berat='Y' "
					SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
					Using dr = OpenTrans(SQL)
						Do While dr.Read
							listSatuan.Add(dr("Satuan").ToString())
						Loop
					End Using

					CloseConn()
				Catch ex As Exception
					CloseConn()
					MessageBox.Show(ex.Message)
					Exit Sub
				End Try

				With N_EMI_SD_Transaksi_Validasi_Trial_Formula
					.Txt_NoFormula.Text = Dgv_KodeFormula
					.Txt_Kd_Barang.Text = Dgv_KodeBarang
					.Txt_NmBarang.Text = Dgv_NamaBarang
					.Txt_Hasil.Text = Format(Val(HilangkanTanda(Dgv_Hasil)), "N4")
					.Cmb_Satuan.Items.Clear()
					.Cmb_Satuan.Items.AddRange(listSatuan.ToArray)
					.Cmb_Satuan.SelectedItem = Dgv_Satuan
					.ShowDialog()
				End With
			End If

			'Try
			'	OpenConn()

			'	Get_Isi_ListView(Dgv_Formula_Parent.CurrentRow.Index)

			'	SQL = "select * From N_EMI_View_Laporan_formula_rpt where kode_perusahaan = '" & KodePerusahaan & "' "
			'	SQL = SQL & "and no_transaksi = '" & Dgv_NoSPlit & "' "
			'	Using Ds = BindingTrans(SQL)
			'		If Ds.Tables("MyTable").Rows.Count <> 0 Then
			'			With Ds.Tables(0)
			'				Dim CrDoc As New N_EMI_CR_Laporan_Formulator
			'				With A_Place_For_Printing2
			'					CrDoc.SetDataSource(Ds)
			'					CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
			'					CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition Barang Lain"
			'					CrDoc.RecordSelectionFormula = " {N_EMI_View_Laporan_formula_rpt.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Laporan_formula_rpt.no_transaksi} = '" & Dgv_NoSPlit & "'"

			'					.Text = "Laporan Formulator"
			'					.CrystalReportViewer1.ReportSource = CrDoc
			'					.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
			'					.Refresh()
			'					.Show()
			'				End With
			'			End With
			'		Else
			'			MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			'		End If

			'	End Using

			'	CloseConn()
			'Catch ex As Exception
			'	CloseConn()
			'	MessageBox.Show(ex.Message)
			'	Exit Sub
			'End Try

			kosong()
		End If
	End Sub

	Private Sub Dgv_Formula_Parent_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellDoubleClick
		If e.RowIndex < 0 Then Exit Sub

		Try
			Me.Cursor = Cursors.WaitCursor
			Application.DoEvents()

			Dim no_split As String = Dgv_Parent.Rows(e.RowIndex).Cells(Cell_NoSplit).Value.ToString()

			Dim pdfStream As MemoryStream = GetPdfStream(Url_Api_Laporan_Formulator, no_split)

			Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

			Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
				pdfStream.CopyTo(file)
			End Using

			Dim frm As New N_EMI_PDF_Viewer(tempPath)
			frm.Show()

			Me.Cursor = Cursors.Default
		Catch ex As Exception
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then Exit Sub

		'get_jam()

		'Try

		'	OpenConn()
		'	Cmd.Transaction = Cn.BeginTransaction

		'	Get_Isi_ListView(Dgv_Parent.CurrentRow.Index)

		'	'========================================================
		'	' =============== CEK STATUS DATA =======================
		'	'========================================================

		'	'bersihkan dulu temporary
		'	SQL = "delete from N_EMI_LIMS_Berkas_Uji_Lab_Temp where  userid_cetak = '" & UserID & "' "
		'	ExecuteTrans(SQL)

		'	Dim listFormula As String = ""
		'	Dim formulas As New List(Of String)
		'	SQL = "WITH CTE AS ("
		'	SQL = SQL & " SELECT U.No_Po_Sampel, "

		'	SQL = SQL & " CASE WHEN SUM(CASE WHEN U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) > 0 "
		'	SQL = SQL & " THEN 1 ELSE 0 END as has_validasi, "

		'	SQL = SQL & " CASE "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = "
		'	SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
		'	SQL = SQL & " ELSE 'MENUNGGU VALIDASI' END as status_lock_view, "

		'	SQL = SQL & " CASE "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > "
		'	SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = "
		'	SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
		'	SQL = SQL & " ELSE 'MENUNGGU VALIDASI' END as status_analisa_lab, "

		'	SQL = SQL & " CASE "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'TERKUNCI' "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > "
		'	SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) > "
		'	SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU HASIL UJI LAB' "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
		'	SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = "
		'	SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
		'	SQL = SQL & " ELSE 'MENUNGGU VALIDASI' END as status_palatabilitas "

		'	SQL = SQL & " FROM N_EMI_LIMS_Uji_Sampel U "
		'	SQL = SQL & " JOIN N_EMI_LAB_Jenis_Analisa A ON U.Id_Jenis_Analisa = A.id "
		'	SQL = SQL & " JOIN N_EMI_LIMS_Klasifikasi_Aktivitas_Lab B ON A.Kode_Aktivitas_Lab = B.Kode_Aktivitas_Lab "
		'	SQL = SQL & " LEFT JOIN N_EMI_LIMS_Uji_Pra_Final UPF ON U.No_Po_Sampel = UPF.No_Sampel "

		'	SQL = SQL & " WHERE U.Flag_Approval = 'Y' "
		'	SQL = SQL & " AND EXISTS (SELECT 1 FROM N_EMI_LIMS_Uji_Pra_Final x WHERE x.No_Sampel = U.No_Po_Sampel) "
		'	SQL = SQL & " AND U.Flag_Selesai = 'Y' "
		'	SQL = SQL & " AND U.Flag_Resampling IS NULL "
		'	SQL = SQL & " AND U.Status IS NULL "

		'	SQL = SQL & " GROUP BY U.No_Po_Sampel "
		'	SQL = SQL & "), Final_Status AS ( "

		'	SQL = SQL & " SELECT b.Kode_Perusahaan, b.No_Transaksi, CTE.status_lock_view, CTE.status_analisa_lab, "
		'	SQL = SQL & " c.Kode_Formula, d.Tanggal, d.Jam, d.Kode_Barang, e.Nama AS Nama_Produk, d.Hasil, d.Satuan_Hasil, "

		'	SQL = SQL & " CTE.No_Po_Sampel, "
		'	SQL = SQL & " c.Status as Status_Production, b.Flag_Validasi, b.Status as Status_Split, d.Status, "

		'	SQL = SQL & " SUM(CASE WHEN ISNULL(CTE.status_lock_view, '') <> 'DISETUJUI' "
		'	SQL = SQL & " OR ISNULL(CTE.status_analisa_lab, '') <> 'DISETUJUI' THEN 1 ELSE 0 END) "
		'	SQL = SQL & " OVER(PARTITION BY a.No_Split_Po) as Tidak_Disetujui "

		'	SQL = SQL & " FROM CTE "
		'	SQL = SQL & " JOIN N_LIMS_PO_Sampel a ON a.No_Sampel = CTE.No_Po_Sampel "
		'	SQL = SQL & " JOIN N_EMI_Transaksi_Trial_Split_Production_Order b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Split_Po = b.No_Transaksi "
		'	SQL = SQL & " JOIN N_EMI_Transaksi_Trial_Order_Produksi c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_PO = c.No_Faktur "
		'	SQL = SQL & " JOIN Emi_Transaksi_Formulator d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Formula = d.No_Faktur "
		'	SQL = SQL & " JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang_Inq = d.Kode_Barang "

		'	SQL = SQL & " WHERE a.Status IS NULL AND b.Status IS NULL AND b.Flag_Validasi IS NULL AND c.Status IS NULL AND d.Status IS NULL "
		'	SQL = SQL & ") "

		'	SQL = SQL & " SELECT Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, "
		'	SQL = SQL & " Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, "
		'	SQL = SQL & " No_Po_Sampel, Status_Production, Flag_Validasi, Status_Split, Status "

		'	SQL = SQL & " FROM Final_Status "
		'	SQL = SQL & " WHERE Tidak_Disetujui = 0 "
		'	SQL = SQL & " AND No_Transaksi = '" & Dgv_NoSPlit & "' "

		'	SQL = SQL & " GROUP BY Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, "
		'	SQL = SQL & " Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, "
		'	SQL = SQL & " No_Po_Sampel, Status_Production, Flag_Validasi, Status_Split, Status "

		'	Using ds = BindingTrans(SQL)

		'		With ds.Tables("MyTable")
		'			If .Rows.Count <> 0 Then

		'				For i As Integer = 0 To .Rows.Count - 1
		'					'If General_Class.CekNULL(.Rows(i).Item("Flag_Validasi")) = "Y" Then

		'					'	CloseTrans()
		'					'	CloseConn()
		'					'	MessageBox.Show("No Split ini sudah di validasi, tidak bisa validasi ulang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'					'	Exit Sub
		'					'End If
		'					If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then

		'						CloseTrans()
		'						CloseConn()
		'						MessageBox.Show("Nomor Formula pada split ini telah dibatalkan, coba cek kembali", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'						Exit Sub
		'					End If

		'					If General_Class.CekNULL(.Rows(i).Item("status_split")) = "Y" Then

		'						CloseTrans()
		'						CloseConn()
		'						MessageBox.Show("No Split telah dibatalkan sebelumnya, coba refresh dan cek kembali", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'						Exit Sub
		'					End If

		'					If General_Class.CekNULL(.Rows(i).Item("Status_Production")) = "Y" Then

		'						CloseTrans()
		'						CloseConn()
		'						MessageBox.Show("Production order telah dibatalkan sebelumnya, coba refresh dan cek kembali", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'						Exit Sub
		'					End If

		'					'formulas.Add(.Rows(i).Item("no_po_sampel"))

		'					'Dim url = ""
		'					'SQL = "select top(1) file_path From N_EMI_LIMS_BErkas_Uji_lab  "
		'					'SQL = SQL & "where no_sampel = '" & .Rows(i).Item("no_po_sampel") & "' "
		'					'Using Dr = OpenTrans(SQL)
		'					'	If Dr.Read Then
		'					'		If General_Class.CekNULL(Dr("file_path")) = "" Then
		'					'			Dr.Close()
		'					'			CloseTrans()
		'					'			CloseConn()
		'					'			MessageBox.Show("Foto lockview NULL", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'					'			Exit Sub
		'					'		Else
		'					'			url = Dr("file_path")
		'					'		End If
		'					'	Else
		'					'		Dr.Close()
		'					'		CloseTrans()
		'					'		CloseConn()
		'					'		MessageBox.Show("Foto lockview tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'					'		Exit Sub
		'					'	End If
		'					'End Using

		'					'Dim signedUrl As String = GCSHelper.GenerateSignedUrl(bucketFormulator, url, 2)
		'					'Dim imageBytes As Byte()
		'					'Using webClient As New WebClient()
		'					'	imageBytes = webClient.DownloadData(signedUrl)
		'					'End Using

		'					''==================== COMPRESS IMAGE ====================
		'					''Dim compressedBytes As Byte() = CompressAndResizeImage(imageBytes, 90, 2000)
		'					'Dim compressedBytes As Byte() = imageBytes

		'					'Cmd.Parameters.Clear()
		'					'Cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = imageBytes

		'					'SQL = "insert into N_EMI_LIMS_BErkas_Uji_lab_temp (No_Faktur,No_Sampel,Berkas_Key,File_Path,Id_Jenis_Analisa,	Gambar_Convert_Image, tanggal_cetak,jam_cetak,userid_cetak) "
		'					'SQL = SQL & "select top(1) No_Faktur,No_Sampel,Berkas_Key,File_Path,Id_Jenis_Analisa, @image, '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' , '" & UserID & "' from N_EMI_LIMS_BErkas_Uji_lab "
		'					'SQL = SQL & "where no_sampel = '" & .Rows(i).Item("no_po_sampel") & "' order by Id_Berkas_Uji_Lab desc "
		'					'ExecuteTrans(SQL)

		'				Next
		'			Else

		'			End If
		'		End With

		'		listFormula = String.Join(",", formulas)

		'	End Using

		'	Cmd.Transaction.Commit()
		'	CloseConn()
		'Catch ex As Exception
		'	CloseTrans()
		'	CloseConn()
		'	MessageBox.Show(ex.Message)
		'	Exit Sub
		'End Try






		'Try
		'	OpenConn()

		'	Get_Isi_ListView(Dgv_Parent.CurrentRow.Index)

		'	SQL = "select * From N_EMI_View_Laporan_formula_rpt where kode_perusahaan = '" & KodePerusahaan & "' "
		'	SQL = SQL & "and no_transaksi = '" & Dgv_NoSPlit & "' "
		'	Using Ds = BindingTrans(SQL)
		'		If Ds.Tables("MyTable").Rows.Count <> 0 Then
		'			With Ds.Tables(0)
		'				Dim CrDoc As New N_EMI_CR_Laporan_Formulator
		'				With A_Place_For_Printing2
		'					CrDoc.SetDataSource(Ds)
		'					CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
		'					CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition Barang Lain"
		'					CrDoc.RecordSelectionFormula = " {N_EMI_View_Laporan_formula_rpt.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Laporan_formula_rpt.no_transaksi} = '" & Dgv_NoSPlit & "'"

		'					.Text = "Laporan Formulator"
		'					.CrystalReportViewer1.ReportSource = CrDoc
		'					.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
		'					.Refresh()
		'					.Show()
		'				End With
		'			End With
		'		Else
		'			MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		'		End If

		'	End Using

		'	CloseConn()
		'Catch ex As Exception
		'	CloseConn()
		'	MessageBox.Show(ex.Message)
		'	Exit Sub
		'End Try
	End Sub
	Public Function GetPdfStream(url As String, no_split As String) As MemoryStream
		' ----------------------------------------------------------------
		' 1. Query bahan & hpp dari DB
		' ----------------------------------------------------------------
		Dim bahan As New List(Of Dictionary(Of String, Object))
		Dim hpp As New Dictionary(Of String, Object)
		Dim namaFormula As String = ""
		Dim kategoriProduk As String = ""
		Dim tanggalUji As String = ""

		Try
			OpenConn()

			SQL = $"SELECT TOP 1 kode_formula, nama_produk, FORMAT(tanggal, 'dd MMM yyyy') AS tanggal_uji FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					namaFormula = Dr("kode_formula").ToString()
					kategoriProduk = Dr("nama_produk").ToString()
					tanggalUji = Dr("tanggal_uji").ToString()
				End If
			End Using

			' Query bahan
			SQL = $"SELECT Nama_bahan, Jumlah, Persentase FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}' GROUP BY kode_formula, nama_produk, tanggal, No_Transaksi, Nama_bahan, Jumlah, Persentase"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read()
					bahan.Add(New Dictionary(Of String, Object) From {
					{"Nama_bahan", Dr("Nama_bahan")},
					{"Jumlah", Dr("Jumlah")},
					{"Persentase", Dr("Persentase")}
				})
				Loop
			End Using

			' Query hpp
			SQL = $"SELECT SUM(Est_HPP_Per_Pcs) AS hpp_bahan_baku, HPP_Packaging, HPP_produksi, 'Per ' + satuan AS satuan FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}' GROUP BY kode_formula, nama_produk, tanggal, No_Transaksi, satuan, HPP_Produksi, HPP_Packaging"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					hpp("hpp_bahan_baku") = Dr("hpp_bahan_baku")
					hpp("hpp_packaging") = Dr("hpp_packaging")
					hpp("hpp_produksi") = Dr("hpp_produksi")
					hpp("satuan") = Dr("satuan")
				End If
			End Using
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Return Nothing
		End Try

		' ----------------------------------------------------------------
		' 2. Serialize ke JSON & kirim ke API
		' ----------------------------------------------------------------
		Dim payload As New Dictionary(Of String, Object) From {
			{"no_split", no_split},
			{"nama_formula", namaFormula},
			{"kategori_produk", kategoriProduk},
			{"tanggal_uji", tanggalUji},
			{"bahan", bahan},
			{"hpp", hpp}
		}

		Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
		Dim signature As String = GenerateHmac(json, Secret_Api_Laporan_Formulator)

		Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
		request.Method = "POST"
		request.ContentType = "application/json"
		request.Accept = "*/*"
		request.UserAgent = "Mozilla/5.0"
		request.Headers.Add("X-Signature", signature)

		Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(json)
		request.ContentLength = bytes.Length
		Using stream = request.GetRequestStream()
			stream.Write(bytes, 0, bytes.Length)
		End Using

		Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
		Dim ms As New MemoryStream()
		Using responseStream = response.GetResponseStream()
			responseStream.CopyTo(ms)
		End Using
		ms.Position = 0
		Return ms
	End Function

	'=====================================================================================================================================
	'=     UTILITY
	'=====================================================================================================================================

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub Dgv_Formula_Parent_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_Parent.CellFormatting
		Try
			If e.RowIndex < 0 OrElse e.Value Is Nothing Then Exit Sub

			' Logika untuk Kolom Qty
			If e.ColumnIndex = Cell_Hasil Then

				Dim jumlah As Double = Val(HilangkanTanda(e.Value.ToString()))
				Dim satuan As String = Dgv_Parent.Rows(e.RowIndex).Cells(Cell_Satuan).Value?.ToString()

				e.Value = $"{Format(jumlah, "N0")} {satuan}"
				e.FormattingApplied = True

			End If
		Catch ex As Exception
			Debug.WriteLine("Error di CellFormatting " & ex.Message)
		End Try
	End Sub

	Private Function CekNothing(ByVal str As String) As String
		Dim hasil As String = ""

		If str Is Nothing Then
			hasil = ""
		Else
			hasil = str
		End If

		Return hasil
	End Function

	Private Sub Dgv_Formula_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Parent.MouseMove
		Dim info As DataGridView.HitTestInfo = Dgv_Parent.HitTest(e.X, e.Y)

		If info.RowIndex >= 0 AndAlso info.ColumnIndex >= 0 Then
			Dgv_Parent.Cursor = Cursors.Hand
		Else
			Dgv_Parent.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Dgv_Formula_Parent_MouseLeave(sender As Object, e As EventArgs) Handles Dgv_Parent.MouseLeave
		Dgv_Parent.Cursor = Cursors.Default
	End Sub

	'Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
	'	If DateTimePicker1.Value <= DateTimePicker2.Value Then
	'		DateTimePicker2.MinDate = DateTimePicker1.Value
	'	End If
	'End Sub

	'Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
	'	If DateTimePicker2.Value >= DateTimePicker1.Value Then
	'		DateTimePicker1.MaxDate = DateTimePicker2.Value
	'	End If
	'End Sub

End Class