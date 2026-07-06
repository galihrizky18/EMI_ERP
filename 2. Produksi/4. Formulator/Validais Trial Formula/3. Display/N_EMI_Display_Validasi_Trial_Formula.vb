Imports System.IO
Imports System.Net
Imports System.Security.Cryptography

Public Class N_EMI_Display_Validasi_Trial_Formula

	Dim Property_AnalisaLab As String = "AnalisaLab"
	Dim Property_LookView As String = "Lookview"
	Dim Property_Palatabilitas As String = "Palatabilitas"

	Dim ArrTanggal, ArrParamLainLama As New ArrayList

	Dim Dgv_Parent_NoSplit, Dgv_Parent_NoFormula, Dgv_Parent_TanggalSplit, Dgv_Parent_JamSplit, Dgv_Parent_TanggalValidasi, Dgv_Parent_TanggalFormula,
		Dgv_Parent_JamFormula, Dgv_Parent_KodeBarang, Dgv_Parent_NamaBarang, Dgv_Parent_Jumlah, Dgv_Parent_Satuan As String

	Dim Cell_Parent_NoSplit As Integer = 0
	Dim Cell_Parent_NoFormula As Integer = 1
	Dim Cell_Parent_TanggalSplit As Integer = 2
	Dim Cell_Parent_JamSplit As Integer = 3
	Dim Cell_Parent_TanggalValidasi As Integer = 4
	Dim Cell_Parent_TanggalFormula As Integer = 5
	Dim Cell_Parent_JamFormula As Integer = 6
	Dim Cell_Parent_KodeBarang As Integer = 7
	Dim Cell_Parent_NamaBarang As Integer = 8
	Dim Cell_Parent_Jumlah As Integer = 9
	Dim Cell_Parent_Satuan As Integer = 10
	Dim Cell_Parent_BtnCetak As Integer = 11

	Dim Cell_Detail_Batch As Integer = 0
	Dim Cell_Detail_NamaMesin As Integer = 1
	Dim Cell_Detail_KodeAktivitas As Integer = 2
	Dim Cell_Detail_IdJenisAnalisa As Integer = 3
	Dim Cell_Detail_JenisAnalisa As Integer = 4
	Dim Cell_Detail_StandarMin As Integer = 5
	Dim Cell_Detail_StandarMax As Integer = 6
	Dim Cell_Detail_AvgHasil As Integer = 7
	Dim Cell_Detail_HasilUji As Integer = 8

	Private Sub N_EMI_Displayi_Validasi_Trial_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		TblLayout_TrackingProgress.Controls.Clear()
		SetUpTabelLayoutProgress("Lookview", Property_LookView, 0, 0)
		SetUpTabelLayoutProgress("Analisa Lab", Property_AnalisaLab, 0, 1)
		SetUpTabelLayoutProgress("Palatabilitas", Property_Palatabilitas, 0, 2)

		Cmb_ParamTgl.Items.Clear() : ArrTanggal.Clear()
		Cmb_ParamTgl.Items.Add("Tanggal Split") : ArrTanggal.Add("Tanggal_Split")
		Cmb_ParamTgl.Items.Add("Tanggal Validasi") : ArrTanggal.Add("Tanggal_Validasi")

		Cmb_ParamLain.Items.Clear() : ArrParamLainLama.Clear()
		Cmb_ParamLain.Items.Add("No Split") : ArrParamLainLama.Add("No_Transaksi")
		Cmb_ParamLain.Items.Add("No Formula") : ArrParamLainLama.Add("No_Transaksi")
		Cmb_ParamLain.Items.Add("Kode Barang") : ArrParamLainLama.Add("Kode_Barang")
		Cmb_ParamLain.Items.Add("Nama Barang") : ArrParamLainLama.Add("Nama_Produk")

		Kosong()

	End Sub

	Private Sub GetDataParent(ByVal index As Integer)
		Dgv_Parent_NoSplit = Dgv_Parent.Rows(index).Cells(Cell_Parent_NoSplit).Value
		Dgv_Parent_NoFormula = Dgv_Parent.Rows(index).Cells(Cell_Parent_NoFormula).Value
		Dgv_Parent_TanggalSplit = Dgv_Parent.Rows(index).Cells(Cell_Parent_TanggalSplit).Value
		Dgv_Parent_JamSplit = Dgv_Parent.Rows(index).Cells(Cell_Parent_JamSplit).Value
		Dgv_Parent_TanggalFormula = Dgv_Parent.Rows(index).Cells(Cell_Parent_TanggalFormula).Value
		Dgv_Parent_JamFormula = Dgv_Parent.Rows(index).Cells(Cell_Parent_JamFormula).Value
		Dgv_Parent_KodeBarang = Dgv_Parent.Rows(index).Cells(Cell_Parent_KodeBarang).Value
		Dgv_Parent_NamaBarang = Dgv_Parent.Rows(index).Cells(Cell_Parent_NamaBarang).Value
		Dgv_Parent_Jumlah = Dgv_Parent.Rows(index).Cells(Cell_Parent_Jumlah).Value
		Dgv_Parent_Satuan = Dgv_Parent.Rows(index).Cells(Cell_Parent_Satuan).Value
	End Sub

	Private Sub Kosong()

		Dgv_Parent.Rows.Clear()
		Dgv_Detail_Uji.Rows.Clear()

		Cmb_ParamTgl.SelectedIndex = -1
		DateTimePicker1.Value = Date.Now
		DateTimePicker2.Value = Date.Now

		Cmb_ParamLain.SelectedIndex = -1
		Txt_ParamValue.Text = ""

		Label3.Text = "Detail Uji"

		Chk_TransaksiHrIni.Checked = True

		ResetTabelLayout()
		LoadDataParent()
	End Sub

	Private Sub LoadDataParent()
		Try
			OpenConn()

			Dim Filter As String = " "
			If Cmb_Lokasi.SelectedIndex > 0 Then
				'Filter &= "AND b.lokasi = '" & Cmb_Lokasi.Text & "' "
			End If

			If Chk_TransaksiHrIni.Checked Then
				Filter &= "AND Tanggal_Split between '" & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now.AddDays(1), "yyyy-MM-dd") & "' "
			End If

			If Chk_ParamTgl.Checked Then
				Filter &= "AND " & ArrTanggal(Cmb_ParamTgl.SelectedIndex) & " between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value.AddDays(1), "yyyy-MM-dd") & "' "
			End If

			If Chk_ParamLain.Checked Then
				Filter &= "AND " & ArrParamLainLama(Cmb_ParamLain.SelectedIndex) & " like '%" & Txt_ParamValue.Text & "%' "
			End If

			Dgv_Parent.Rows.Clear() : Dgv_Detail_Uji.Rows.Clear()
			ResetTabelLayout()
			SQL = $"
				WITH CTE AS (
					SELECT
						u.kode_perusahaan, U.No_Po_Sampel
					FROM
						N_EMI_LIMS_Uji_Sampel as U
						JOIN N_EMI_LAB_Jenis_Analisa as A ON U.Id_Jenis_Analisa = A.id
						JOIN N_EMI_LIMS_Klasifikasi_Aktivitas_Lab as B ON A.Kode_Aktivitas_Lab = B.Kode_Aktivitas_Lab
						LEFT JOIN N_EMI_LIMS_Uji_Pra_Final as UPF ON U.No_Po_Sampel = UPF.No_Sampel
					WHERE --u.Flag_Approval = 'Y'
						--AND EXISTS (
						--	SELECT 1
						--	FROM N_EMI_LIMS_Uji_Pra_Final x
						--	WHERE x.No_Sampel = u.No_Po_Sampel )
						u.Flag_Selesai = 'Y'
						AND u.Flag_Resampling IS NULL
						AND u.Status IS NULL
					GROUP BY
						U.No_Po_Sampel, u.kode_perusahaan
				),

				Final_Status AS (
					SELECT b.Kode_Perusahaan, b.flag_validasi, b.No_Transaksi, c.Kode_Formula,  b.tanggal as Tanggal_Split, b.jam as Jam_Split,
						d.Tanggal as Tanggal_Formula, d.Jam as Jam_Formula, b.tanggal_validasi as Tanggal_Validasi, d.Kode_Barang, e.Nama AS Nama_Produk, d.Hasil, d.Satuan_Hasil,
						b.Jumlah as Jumlah_Split, b.Satuan as Satuan_Split
					FROM CTE
						JOIN N_LIMS_PO_Sampel a ON a.No_Sampel = CTE.No_Po_Sampel
						JOIN N_EMI_Transaksi_Trial_Split_Production_Order b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Split_Po = b.No_Transaksi
						JOIN N_EMI_Transaksi_Trial_Order_Produksi c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_PO = c.No_Faktur
						JOIN Emi_Transaksi_Formulator d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Formula = d.No_Faktur
						JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang_Inq = d.Kode_Barang
					WHERE
						a.Status IS NULL and b.status is null and c.status is null and d.status is null
						AND b.Status IS NULL
						--AND b.Flag_Validasi IS NULL
						AND c.Status IS NULL
						AND d.Status IS NULL
				)
				SELECT Kode_Perusahaan, flag_validasi, No_Transaksi, Kode_Formula, Tanggal_Split, Jam_Split, Tanggal_Formula, Jam_Formula, Tanggal_Validasi,
					Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, Jumlah_Split, Satuan_Split
				FROM Final_Status
				where Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				GROUP BY Kode_Perusahaan, flag_validasi, No_Transaksi, Kode_Formula, Tanggal_Split, Jam_Split, Tanggal_Formula, Jam_Formula, Tanggal_Validasi, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil,
				Jumlah_Split, Satuan_Split
			"

			'SQL = $"
			'	with cte as (
			'	SELECT b.Kode_Perusahaan, b.flag_validasi, b.No_Transaksi, c.Kode_Formula,  b.tanggal as Tanggal_Split, b.jam as Jam_Split,
			'		d.Tanggal as Tanggal_Formula, d.Jam as Jam_Formula, b.tanggal_validasi as Tanggal_Validasi, d.Kode_Barang, e.Nama AS Nama_Produk, d.Hasil, d.Satuan_Hasil,
			'		b.Jumlah as Jumlah_Split, b.Satuan as Satuan_Split
			'	FROM N_LIMS_PO_Sampel a
			'		JOIN N_EMI_Transaksi_Trial_Split_Production_Order b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Split_Po = b.No_Transaksi
			'		JOIN N_EMI_Transaksi_Trial_Order_Produksi c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_PO = c.No_Faktur
			'		JOIN Emi_Transaksi_Formulator d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Formula = d.No_Faktur
			'		JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang_Inq = d.Kode_Barang
			'	WHERE
			'		a.Status IS NULL and b.status is null and c.status is null and d.status is null
			'		AND b.Status IS NULL
			'		--AND b.Flag_Validasi IS NULL
			'		AND c.Status IS NULL
			'		AND d.Status IS NULL
			'	)
			'	SELECT Kode_Perusahaan, flag_validasi, No_Transaksi, Kode_Formula, Tanggal_Split, Jam_Split, Tanggal_Formula, Jam_Formula, Tanggal_Validasi,
			'					Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, Jumlah_Split, Satuan_Split
			'	FROM cte
			'	where Kode_Perusahaan = '{KodePerusahaan}'
			'	{Filter}
			'	GROUP BY Kode_Perusahaan, flag_validasi, No_Transaksi, Kode_Formula, Tanggal_Split, Jam_Split, Tanggal_Formula, Jam_Formula, Tanggal_Validasi, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil,
			'	Jumlah_Split, Satuan_Split
			'"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					With Dgv_Parent.Rows(Dgv_Parent.Rows.Add())
						.Cells(Cell_Parent_NoSplit).Value = Dr("No_Transaksi")
						.Cells(Cell_Parent_NoFormula).Value = Dr("Kode_Formula")
						.Cells(Cell_Parent_TanggalSplit).Value = Format(Dr("Tanggal_Split"), "dd MMM yyyy")
						.Cells(Cell_Parent_JamSplit).Value = Dr("Jam_Split")
						.Cells(Cell_Parent_TanggalFormula).Value = Format(Dr("Tanggal_Formula"), "dd MMM yyyy")
						.Cells(Cell_Parent_TanggalValidasi).Value = If(General_Class.CekNULL(Dr("Tanggal_Validasi")) = "", "-", Format(Dr("Tanggal_Validasi"), "dd MMM yyyy"))
						.Cells(Cell_Parent_JamFormula).Value = Dr("Jam_Formula")
						.Cells(Cell_Parent_KodeBarang).Value = Dr("Kode_Barang")
						.Cells(Cell_Parent_NamaBarang).Value = Dr("Nama_Produk")
						.Cells(Cell_Parent_Jumlah).Value = Format(Dr("Jumlah_Split"), "N0")
						.Cells(Cell_Parent_Satuan).Value = Dr("Satuan_Split")
						.Cells(Cell_Parent_BtnCetak).Value = "Cetak Laporan"

						'.DefaultCellStyle.BackColor = If(General_Class.CekNULL(Dr("flag_validasi")) = "", Color.White, Color.LightGreen)

						Dim rowColor As Color = If(General_Class.CekNULL(Dr("flag_validasi")) = "", Color.White, Color.LightGreen)

						For Each cell As DataGridViewCell In .Cells
							If cell.ColumnIndex <> Cell_Parent_BtnCetak Then
								cell.Style.BackColor = rowColor
							Else
								cell.Style.BackColor = Color.Empty
							End If
						Next

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

	Private Sub Dgv_Parent_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		If e.ColumnIndex = Cell_Parent_BtnCetak Then

			GetDataParent(e.RowIndex)

			If (MessageBox.Show($"Yakin ingin Cetak Laporan Split {Dgv_Parent_NoSplit} ini ???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

			get_jam()

			Try
				OpenConn()

				'===============================================
				'=     CEK APAKAH SAMPEL SUDAH DI PRAFINAL     =
				'===============================================
				SQL = $"
					select b.No_Sampel,
						isnull((
							select 'Y' from N_EMI_LIMS_Uji_Pra_Final z
							where b.No_Sampel = z.No_Sampel
						), 'T') as Flag_Pra_Final
					from N_EMI_Transaksi_Trial_Split_Production_Order a
						inner join N_LIMS_PO_Sampel b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Split_Po
						inner join N_EMI_Transaksi_Trial_Order_Produksi c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_PO = c.No_Faktur
						inner join Emi_Transaksi_Formulator d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Formula = d.No_Faktur
					where a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.No_Transaksi = '{Dgv_Parent_NoSplit}'
				"
				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						Dim noSampel As String = Dr("No_Sampel")

						If Dr("Flag_Pra_Final") = "T" Then
							Dr.Close()
							CloseConn()
							MessageBox.Show($"No Sampel {noSampel} Belum Melakukan Pra Finalisasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If

					Loop
				End Using

				''========================================================
				'' =============== CEK STATUS DATA =======================
				''========================================================

				''bersihkan dulu temporary
				'SQL = "delete from N_EMI_LIMS_Berkas_Uji_Lab_Temp where  userid_cetak = '" & UserID & "' "
				'ExecuteTrans(SQL)

				'Dim listFormula As String = ""
				'Dim formulas As New List(Of String)
				'SQL = "WITH CTE AS ( "
				'SQL = SQL & " SELECT U.No_Po_Sampel, "

				'SQL = SQL & " CASE WHEN SUM(CASE WHEN U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) > 0 "
				'SQL = SQL & " THEN 1 ELSE 0 END as has_validasi, "

				'SQL = SQL & " CASE "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = "
				'SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
				'SQL = SQL & " ELSE 'MENUNGGU VALIDASI' END as status_lock_view, "

				'SQL = SQL & " CASE "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > "
				'SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = "
				'SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
				'SQL = SQL & " ELSE 'MENUNGGU VALIDASI' END as status_analisa_lab, "

				'SQL = SQL & " CASE "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'TERKUNCI' "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > "
				'SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) > "
				'SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU HASIL UJI LAB' "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
				'SQL = SQL & " WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = "
				'SQL = SQL & "      SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
				'SQL = SQL & " ELSE 'MENUNGGU VALIDASI' END as status_palatabilitas "

				'SQL = SQL & " FROM N_EMI_LIMS_Uji_Sampel U "
				'SQL = SQL & " JOIN N_EMI_LAB_Jenis_Analisa A ON U.Id_Jenis_Analisa = A.id "
				'SQL = SQL & " JOIN N_EMI_LIMS_Klasifikasi_Aktivitas_Lab B ON A.Kode_Aktivitas_Lab = B.Kode_Aktivitas_Lab "
				'SQL = SQL & " LEFT JOIN N_EMI_LIMS_Uji_Pra_Final UPF ON U.No_Po_Sampel = UPF.No_Sampel "

				'SQL = SQL & " WHERE U.Flag_Approval = 'Y' "
				'SQL = SQL & " AND EXISTS (SELECT 1 FROM N_EMI_LIMS_Uji_Pra_Final x WHERE x.No_Sampel = U.No_Po_Sampel) "
				'SQL = SQL & " AND U.Flag_Selesai = 'Y' "
				'SQL = SQL & " AND U.Flag_Resampling IS NULL "
				'SQL = SQL & " AND U.Status IS NULL "

				'SQL = SQL & " GROUP BY U.No_Po_Sampel "
				'SQL = SQL & " ), Final_Status AS ( "

				'SQL = SQL & " SELECT b.Kode_Perusahaan, b.No_Transaksi, CTE.status_lock_view, CTE.status_analisa_lab, "
				'SQL = SQL & " c.Kode_Formula, d.Tanggal, d.Jam, d.Kode_Barang, e.Nama AS Nama_Produk, d.Hasil, d.Satuan_Hasil, "

				'SQL = SQL & " CTE.No_Po_Sampel, "
				'SQL = SQL & " c.Status as Status_Production, b.Flag_Validasi, b.Status as Status_Split, d.Status, "

				'SQL = SQL & " SUM(CASE WHEN ISNULL(CTE.status_lock_view, '') <> 'DISETUJUI' "
				'SQL = SQL & " OR ISNULL(CTE.status_analisa_lab, '') <> 'DISETUJUI' THEN 1 ELSE 0 END) "
				'SQL = SQL & " OVER(PARTITION BY a.No_Split_Po) as Tidak_Disetujui "

				'SQL = SQL & " FROM CTE "
				'SQL = SQL & " JOIN N_LIMS_PO_Sampel a ON a.No_Sampel = CTE.No_Po_Sampel "
				'SQL = SQL & " JOIN N_EMI_Transaksi_Trial_Split_Production_Order b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Split_Po = b.No_Transaksi "
				'SQL = SQL & " JOIN N_EMI_Transaksi_Trial_Order_Produksi c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_PO = c.No_Faktur "
				'SQL = SQL & " JOIN Emi_Transaksi_Formulator d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Formula = d.No_Faktur "
				'SQL = SQL & " JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang_Inq = d.Kode_Barang "

				'SQL = SQL & " WHERE a.Status IS NULL AND b.Status IS NULL AND c.Status IS NULL AND d.Status IS NULL "
				'SQL = SQL & " ) "

				'SQL = SQL & " SELECT Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, "
				'SQL = SQL & " Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, "
				'SQL = SQL & " No_Po_Sampel, Status_Production, Flag_Validasi, Status_Split, Status "

				'SQL = SQL & " FROM Final_Status "
				'SQL = SQL & " WHERE Tidak_Disetujui = 0 "
				'SQL = SQL & " AND No_Transaksi = '" & Dgv_Parent_NoSplit & "' "

				'SQL = SQL & " GROUP BY Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, "
				'SQL = SQL & " Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, "
				'SQL = SQL & " No_Po_Sampel, Status_Production, Flag_Validasi, Status_Split, Status "

				'Using ds = BindingTrans(SQL)

				'	With ds.Tables("MyTable")
				'		If .Rows.Count <> 0 Then

				'			For i As Integer = 0 To .Rows.Count - 1
				'				'If General_Class.CekNULL(.Rows(i).Item("Flag_Validasi")) = "Y" Then

				'				'	CloseTrans()
				'				'	CloseConn()
				'				'	MessageBox.Show("No Split ini sudah di validasi, tidak bisa validasi ulang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'				'	Exit Sub
				'				'End If
				'				If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then

				'					CloseTrans()
				'					CloseConn()
				'					MessageBox.Show("Nomor Formula pada split ini telah dibatalkan, coba cek kembali", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'					Exit Sub
				'				End If

				'				If General_Class.CekNULL(.Rows(i).Item("status_split")) = "Y" Then

				'					CloseTrans()
				'					CloseConn()
				'					MessageBox.Show("No Split telah dibatalkan sebelumnya, coba refresh dan cek kembali", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'					Exit Sub
				'				End If

				'				If General_Class.CekNULL(.Rows(i).Item("Status_Production")) = "Y" Then

				'					CloseTrans()
				'					CloseConn()
				'					MessageBox.Show("Production order telah dibatalkan sebelumnya, coba refresh dan cek kembali", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'					Exit Sub
				'				End If

				'				formulas.Add(.Rows(i).Item("no_po_sampel"))

				'				Dim url = ""
				'				SQL = "select top(1) file_path From N_EMI_LIMS_BErkas_Uji_lab  "
				'				SQL = SQL & "where no_sampel = '" & .Rows(i).Item("no_po_sampel") & "' "
				'				Using Dr = OpenTrans(SQL)
				'					If Dr.Read Then
				'						If General_Class.CekNULL(Dr("file_path")) = "" Then
				'							Dr.Close()
				'							CloseTrans()
				'							CloseConn()
				'							MessageBox.Show("Foto lockview NULL", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'							Exit Sub
				'						Else
				'							url = Dr("file_path")
				'						End If
				'					Else
				'						Dr.Close()
				'						CloseTrans()
				'						CloseConn()
				'						MessageBox.Show("Foto lockview tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'						Exit Sub
				'					End If
				'				End Using

				'				Dim signedUrl As String = GCSHelper.GenerateSignedUrl(bucketFormulator, url, 120)
				'				Dim imageBytes As Byte()
				'				Using webClient As New WebClient()
				'					imageBytes = webClient.DownloadData(signedUrl)
				'				End Using

				'				'==================== COMPRESS IMAGE ====================
				'				Dim compressedBytes As Byte() = CompressAndResizeImage(imageBytes, 90, 2000)

				'				Cmd.Parameters.Clear()
				'				Cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = compressedBytes

				'				SQL = "insert into N_EMI_LIMS_BErkas_Uji_lab_temp (No_Faktur,No_Sampel,Berkas_Key,File_Path,Id_Jenis_Analisa,	Gambar_Convert_Image, tanggal_cetak,jam_cetak,userid_cetak) "
				'				SQL = SQL & "select top(1) No_Faktur,No_Sampel,Berkas_Key,File_Path,Id_Jenis_Analisa, @image, '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' , '" & UserID & "' from N_EMI_LIMS_BErkas_Uji_lab "
				'				SQL = SQL & "where no_sampel = '" & .Rows(i).Item("no_po_sampel") & "' order by Id_Berkas_Uji_Lab desc "
				'				ExecuteTrans(SQL)

				'			Next
				'		Else

				'		End If
				'	End With

				'	listFormula = String.Join(",", formulas)

				'End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			Try
				Me.Cursor = Cursors.WaitCursor
				Application.DoEvents()

				Dim no_split As String = Dgv_Parent.Rows(e.RowIndex).Cells(0).Value.ToString()

				Dim pdfStream As MemoryStream = GetPdfStream(Url_Api_Laporan_Formulator, no_split)

				Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

				Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
					pdfStream.CopyTo(file)
				End Using

				Dim frm As New N_EMI_PDF_Viewer(tempPath)
				frm.Show()
			Catch ex As Exception
				MessageBox.Show(ex.Message)
			Finally
				Me.Cursor = Cursors.Default
			End Try

			'Try
			'	OpenConn()

			'	SQL = "select * From N_EMI_View_Laporan_formula_rpt where kode_perusahaan = '" & KodePerusahaan & "' "
			'	SQL = SQL & "and no_transaksi = '" & Dgv_Parent_NoSplit & "' "
			'	Using Ds = BindingTrans(SQL)
			'		If Ds.Tables("MyTable").Rows.Count <> 0 Then
			'			With Ds.Tables(0)
			'				Dim CrDoc As New N_EMI_CR_Laporan_Formulator
			'				With A_Place_For_Printing2
			'					CrDoc.SetDataSource(Ds)
			'					CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
			'					CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition Barang Lain"
			'					CrDoc.RecordSelectionFormula = " {N_EMI_View_Laporan_formula_rpt.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Laporan_formula_rpt.no_transaksi} = '" & Dgv_Parent_NoSplit & "'"

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

		End If

	End Sub

	Private Function GenerateHmac(payload As String, secret As String) As String
		Dim keyBytes = System.Text.Encoding.UTF8.GetBytes(secret)
		Dim payloadBytes = System.Text.Encoding.UTF8.GetBytes(payload)
		Using hmac As New HMACSHA256(keyBytes)
			Dim hash = hmac.ComputeHash(payloadBytes)
			Return BitConverter.ToString(hash).Replace("-", "").ToLower()
		End Using
	End Function

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

	Private Sub Dgv_Parent_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellClick
		If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then Exit Sub

		Try
			OpenConn()

			ResetTabelLayout()

			Dim lbl_Lab = TblLayout_TrackingProgress.Controls.Find($"lbl_{Property_AnalisaLab}", True).FirstOrDefault()
			Dim lbl_LookView = TblLayout_TrackingProgress.Controls.Find($"lbl_{Property_LookView}", True).FirstOrDefault()
			Dim lbl_Palatabilitas = TblLayout_TrackingProgress.Controls.Find($"lbl_{Property_Palatabilitas}", True).FirstOrDefault()

			Dim SelectedSplit As String = Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoSplit).Value
			Dgv_Detail_Uji.Rows.Clear()
			SQL = $"
				;with cte as (
					select d.kode_perusahaan, d.No_Transaksi as No_split, U.No_Po_Sampel,
						CASE
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA'
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK'
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI'
							ELSE 'MENUNGGU VALIDASI'
						END as status_lock_view,

						CASE
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END)
							THEN CONVERT(VARCHAR, MAX(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN a.Created_At END), 106)
							ELSE '-'
						END as Tanggal_lock_view,

						CASE
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA'
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW'
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK'
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI'
							ELSE 'MENUNGGU VALIDASI'
						END as status_analisa_lab,

						CASE
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END)
							THEN CONVERT(VARCHAR, MAX(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN a.Created_At END), 106)
							ELSE '-'
						END as Tanggal_status_analisa_lab,

						CASE
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA'
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'TERKUNCI'
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW'
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU HASIL UJI LAB'
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK'
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI'
							ELSE 'MENUNGGU VALIDASI'
						END as status_palatabilitas,

						CASE
							WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END)
							THEN CONVERT(VARCHAR, MAX(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN a.Created_At END), 106)
							ELSE '-'
						END as Tanggal_status_palatabilitas

					from N_EMI_LIMS_Uji_Sampel u
						inner join N_EMI_LAB_Jenis_Analisa a on u.Id_Jenis_Analisa = a.id
						inner join N_EMI_LIMS_Klasifikasi_Aktivitas_Lab b on a.Kode_Aktivitas_Lab = b.Kode_Aktivitas_Lab
						inner join N_LIMS_PO_Sampel c on No_Sampel = u.No_Po_Sampel
						inner join N_EMI_Transaksi_Trial_Split_Production_Order d on c.kode_perusahaan = d.kode_perusahaan and c.No_Split_Po = d.no_transaksi
						left join N_EMI_LIMS_Uji_Pra_Final upf on u.No_Po_Sampel = upf.No_Sampel
					WHERE --u.Flag_Approval = 'Y'
						--AND EXISTS (
						--	SELECT 1
						--	FROM N_EMI_LIMS_Uji_Pra_Final x
						--	WHERE x.No_Sampel = u.No_Po_Sampel
						--)
						 u.Flag_Selesai = 'Y'
						AND u.Flag_Resampling IS NULL
						AND u.Status IS NULL
					GROUP BY d.kode_perusahaan, d.No_Transaksi, U.No_Po_Sampel
				)
				select No_Split, MAX(Tanggal_lock_view) as Tanggal_lock_view,
					MAX(Tanggal_status_analisa_lab) as Tanggal_status_analisa_lab,
					MAX(Tanggal_status_palatabilitas) as Tanggal_status_palatabilitas,
					CASE
						WHEN COUNT(CASE WHEN status_lock_view = 'TIDAK ADA' THEN 1 END) > 0
							THEN 'TIDAK_ADA'
						WHEN COUNT(CASE WHEN status_lock_view = 'DITOLAK' THEN 1 END) > 0
							THEN 'DITOLAK'
						WHEN COUNT(CASE WHEN status_lock_view = 'DISETUJUI' THEN 1 END) > 0
							THEN 'DISETUJUI'
						ELSE 'MENUNGGU_VALIDASI'
					END AS Status_LookView,
					CASE
						WHEN COUNT(CASE WHEN status_analisa_lab = 'TIDAK ADA' THEN 1 END) > 0
							THEN 'TIDAK_ADA'
						WHEN COUNT(CASE WHEN status_analisa_lab = 'MENUNGGU LOCK VIEW' THEN 1 END) > 0
							THEN 'MENUNGGU_LOCK_VIEW'
						WHEN COUNT(CASE WHEN status_analisa_lab = 'DITOLAK' THEN 1 END) > 0
							THEN 'DITOLAK'
						WHEN COUNT(CASE WHEN status_analisa_lab = 'DISETUJUI' THEN 1 END) > 0
							THEN 'DISETUJUI'
						ELSE 'MENUNGGU_VALIDASI'
					END AS Status_AnalisaLab,
					CASE
						WHEN COUNT(CASE WHEN status_palatabilitas = 'TIDAK_ADA' THEN 1 END) > 0
							THEN 'TIDAK_ADA'
						WHEN COUNT(CASE WHEN status_palatabilitas = 'TERKUNCI' THEN 1 END) > 0
							THEN 'TERKUNCI'
						WHEN COUNT(CASE WHEN status_palatabilitas = 'MENUNGGU LOCK VIEW' THEN 1 END) > 0
							THEN 'MENUNGGU_LOCK_VIEW'
						WHEN COUNT(CASE WHEN status_palatabilitas = 'MENUNGGU HASIL UJI LAB' THEN 1 END) > 0
							THEN 'MENUNGGU_HASIL_UJI_LAB'
						WHEN COUNT(CASE WHEN status_palatabilitas = 'DITOLAK' THEN 1 END) > 0
							THEN 'DITOLAK'
						WHEN COUNT(CASE WHEN status_palatabilitas = 'DISETUJUI' THEN 1 END) > 0
							THEN 'DISETUJUI'
						ELSE 'MENUNGGU_VALIDASI'
					END AS Status_Palatabilitas
				from cte
				where kode_perusahaan = '{KodePerusahaan}'
				and No_Split = '{SelectedSplit}'
				group by No_Split
			"
			Using Dr = OpenTrans(SQL)

				TblLayout_TrackingProgress.SuspendLayout()

				Do While Dr.Read

					Dim Satatus_Lab As String = CleanStatus(Dr("Status_AnalisaLab"))
					Dim Satatus_LookView As String = CleanStatus(Dr("Status_LookView"))
					Dim Satatus_Palatabilitas As String = CleanStatus(Dr("Status_Palatabilitas"))

					Dim arrStatusNull As New ArrayList From {"TIDAK_ADA", "MENUNGGU"}

					If arrStatusNull.Contains(Satatus_Lab) Then
						lbl_Lab.Text = "-"
					Else
						lbl_Lab.Text = General_Class.CekNULL(Dr("Tanggal_status_analisa_lab"))
					End If

					If arrStatusNull.Contains(Satatus_LookView) Then
						lbl_LookView.Text = "-"
					Else
						lbl_LookView.Text = General_Class.CekNULL(Dr("Tanggal_lock_view"))
					End If

					If arrStatusNull.Contains(Satatus_Palatabilitas) Then
						lbl_Palatabilitas.Text = "-"
					Else
						lbl_Palatabilitas.Text = General_Class.CekNULL(Dr("Tanggal_status_palatabilitas"))
					End If

					Dim Bdg = TblLayout_TrackingProgress.Controls.Find($"lblBadge_{Property_AnalisaLab}_{Satatus_Lab}", True).FirstOrDefault()
					Bdg.Visible = True

					Dim Bdg2 = TblLayout_TrackingProgress.Controls.Find($"lblBadge_{Property_LookView}_{Satatus_LookView}", True).FirstOrDefault()
					Bdg2.Visible = True

					Dim Bdg3 = TblLayout_TrackingProgress.Controls.Find($"lblBadge_{Property_Palatabilitas}_{Satatus_Palatabilitas}", True).FirstOrDefault()
					Bdg3.Visible = True

				Loop

				TblLayout_TrackingProgress.ResumeLayout()
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub PanelProgress_Click(sender As Object, e As EventArgs)
		If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then Exit Sub
		Dim ctrl = TryCast(sender, Control)

		Dim propertyName As String = ctrl.Tag.ToString()

		If String.IsNullOrEmpty(propertyName) Then
			MessageBox.Show("Detail Analisa Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Try
			OpenConn()

			Dim SelectedSplit As String = Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoSplit).Value

			Dim KodeAktivitasLab As String = ""
			Select Case propertyName
				Case Property_AnalisaLab
					KodeAktivitasLab = "ANL"
					Label3.Text = "Detail Uji Analisa Lab"
				Case Property_LookView
					KodeAktivitasLab = "LCKV"
					Label3.Text = "Detail Uji Look View"
				Case Property_Palatabilitas
					KodeAktivitasLab = "PLT"
					Label3.Text = "Detail Uji Palatabilitas"
				Case Else
					MessageBox.Show("Kode Aktivitas Lab Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
			End Select

			Dgv_Detail_Uji.Rows.Clear()
			SQL = $"
				WITH cte AS (
					SELECT
						a.No_Split_Po,
						a.No_Batch,
						f.Nama_Mesin,
						c.Kode_Aktivitas_Lab,
						b.Id_Jenis_Analisa,
						c.Jenis_Analisa,
						b.No_Po_Sampel,
						ROUND(AVG(b.Hasil), 2) AS Avg_Hasil,
						isnull(e.keterangan_kriteria, '-') as keterangan_kriteria,
						CASE
							WHEN b.Flag_Perhitungan = 'Y' THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '0')
							ELSE ISNULL(e.Keterangan_Kriteria, '-')
						END AS Std_Min,
						CASE
							WHEN b.Flag_Perhitungan = 'Y' THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '0')
							ELSE ISNULL(e.Keterangan_Kriteria, '-')
						END AS Std_Max,
						 CASE
							WHEN b.Flag_Perhitungan = 'Y' AND ROUND(AVG(b.Hasil), 2) BETWEEN TRY_CAST(d.Range_Awal AS FLOAT) AND TRY_CAST(d.Range_Akhir AS FLOAT) THEN 'Lulus'
							WHEN b.Flag_Perhitungan = 'Y' AND (ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT) OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)) THEN 'Tidak Lulus'
							ELSE (CASE WHEN e.Flag_Layak = 'Y' THEN 'Lulus' ELSE 'Tidak Lulus' END)
						END AS Hasil_Uji,
						b.Status,
						b.Flag_Final,
						b.Flag_Approval,

						CASE
							WHEN SUM(CASE WHEN c.Kode_Aktivitas_Lab = 'LCKV' AND b.Flag_Approval = 'T' THEN 1 ELSE 0 END) OVER(PARTITION BY a.No_Split_Po) > 0 THEN 'DITOLAK'
							WHEN SUM(CASE WHEN c.Kode_Aktivitas_Lab = 'LCKV' AND b.Flag_Approval = 'Y' THEN 1 ELSE 0 END) OVER(PARTITION BY a.No_Split_Po) =
								 SUM(CASE WHEN c.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) OVER(PARTITION BY a.No_Split_Po) THEN 'DISETUJUI'
							ELSE 'MENUNGGU VALIDASI'
						END as status_lock_view_split,

						CASE
							WHEN SUM(CASE WHEN c.Kode_Aktivitas_Lab = 'ANL' AND b.Flag_Approval = 'T' THEN 1 ELSE 0 END) OVER(PARTITION BY a.No_Split_Po) > 0 THEN 'DITOLAK'
							WHEN SUM(CASE WHEN c.Kode_Aktivitas_Lab = 'ANL' AND b.Flag_Approval = 'Y' THEN 1 ELSE 0 END) OVER(PARTITION BY a.No_Split_Po) =
								 SUM(CASE WHEN c.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) OVER(PARTITION BY a.No_Split_Po) THEN 'DISETUJUI'
							ELSE 'MENUNGGU VALIDASI'
						END as status_analisa_lab_split

					FROM N_LIMS_PO_Sampel a
					JOIN N_EMI_LIMS_Uji_Sampel b ON a.No_Sampel = b.No_Po_Sampel
					JOIN N_EMI_LAB_Jenis_Analisa c ON b.Id_Jenis_Analisa = c.id
					LEFT JOIN N_EMI_LIMS_Uji_Pra_Final UPF ON b.No_Po_Sampel = UPF.No_Sampel
					LEFT JOIN N_EMI_LAB_Standar_Rentang d ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa AND b.Flag_Perhitungan = 'Y' and a.Kode_Barang = d.Kode_Barang
					LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e ON b.Id_Jenis_Analisa = e.Id_Jenis_Analisa AND b.Flag_Perhitungan IS NULL
					AND CAST(b.Hasil AS VARCHAR(50)) = CAST(e.Nilai_Kriteria AS VARCHAR(50))
					JOIN EMI_Master_Mesin f on a.Kode_Perusahaan = f.Kode_Perusahaan and a.Id_Mesin = f.Id_Master_Mesin

					WHERE
						b.Flag_Approval = 'Y'
						--and UPF.No_Sampel IS NOT NULL
						AND a.Status IS NULL
						AND b.Flag_Selesai = 'Y'
						AND b.Status IS NULL

					GROUP BY
						a.No_Split_Po, a.No_Batch, f.Nama_Mesin, b.Id_Jenis_Analisa, c.Jenis_Analisa,
						d.Range_Awal, d.Range_Akhir, b.Status, b.Flag_Final, b.Flag_Approval,
						b.Flag_Perhitungan, e.Keterangan_Kriteria, e.Flag_Layak, c.Kode_Aktivitas_Lab, b.No_Po_Sampel
				)
				SELECT * FROM cte
				WHERE
					 status_lock_view_split = 'DISETUJUI'
					AND status_analisa_lab_split = 'DISETUJUI'
					AND No_Split_PO = '{SelectedSplit}'
					AND Kode_Aktivitas_Lab = '{KodeAktivitasLab}'
				order by Kode_Aktivitas_Lab
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					With Dgv_Detail_Uji.Rows(Dgv_Detail_Uji.Rows.Add)
						.Cells(Cell_Detail_Batch).Value = Dr("No_Batch")
						.Cells(Cell_Detail_NamaMesin).Value = Dr("Nama_Mesin")
						.Cells(Cell_Detail_KodeAktivitas).Value = Dr("Kode_Aktivitas_Lab")
						.Cells(Cell_Detail_IdJenisAnalisa).Value = Dr("Id_Jenis_Analisa")
						.Cells(Cell_Detail_JenisAnalisa).Value = Dr("Jenis_Analisa")
						.Cells(Cell_Detail_StandarMin).Value = Format(Val(HilangkanTanda(Dr("Std_Min"))), "N4")
						.Cells(Cell_Detail_StandarMax).Value = Format(Val(HilangkanTanda(Dr("Std_Max"))), "N4")

						Dim AvgHasil As String = ""
						If KodeAktivitasLab = "ANL" Then
							AvgHasil = Format(Val(HilangkanTanda(Dr("Avg_Hasil"))), "N4")
							.Cells(Cell_Detail_AvgHasil).Style.Alignment = DataGridViewContentAlignment.MiddleRight
						Else
							AvgHasil = Dr("keterangan_kriteria")
							.Cells(Cell_Detail_AvgHasil).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
						End If

						.Cells(Cell_Detail_AvgHasil).Value = AvgHasil
						.Cells(Cell_Detail_HasilUji).Value = Dr("Hasil_Uji")
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

	'==================================================================================================================================
	'=     HANDLE FILTER
	'==================================================================================================================================
	Private Sub Chk_TransaksiHrIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_TransaksiHrIni.CheckedChanged
		If Chk_TransaksiHrIni.Checked = True Then
			Chk_ParamTgl.Checked = False
			Btn_Cari_Click(Chk_TransaksiHrIni, e)
		End If
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Not Chk_TransaksiHrIni.Checked And Not Chk_ParamTgl.Checked And Not Chk_ParamLain.Checked Then
			MessageBox.Show("Pilih Dahulu Parameter Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Chk_TransaksiHrIni.Focus()
			Exit Sub
		End If

		If Chk_ParamTgl.Checked Then
			If Cmb_ParamTgl.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Tanggal Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_ParamTgl.DroppedDown = True : Cmb_ParamTgl.Focus()
				Exit Sub
			End If
		End If

		If Chk_ParamLain.Checked Then
			If Cmb_ParamLain.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_ParamLain.DroppedDown = True : Cmb_ParamLain.Focus()
				Exit Sub
			Else
				If Txt_ParamValue.Text.Trim.Length = 0 Then
					MessageBox.Show("Value Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_ParamValue.Focus()
					Exit Sub
				End If
			End If
		End If

		LoadDataParent()
	End Sub

	Private Sub Chk_ParamTgl_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamTgl.CheckedChanged
		If Chk_ParamTgl.Checked Then
			Cmb_ParamTgl.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
			Chk_TransaksiHrIni.Checked = False
		Else
			Cmb_ParamTgl.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
			Cmb_ParamTgl.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
		End If
	End Sub

	Private Sub Chk_ParamLain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamLain.CheckedChanged
		If Chk_ParamLain.Checked Then
			Cmb_ParamLain.Enabled = True : Txt_ParamValue.Enabled = True
		Else
			Cmb_ParamLain.Enabled = False : Txt_ParamValue.Enabled = False
			Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamValue.Text = ""
		End If
	End Sub

	Private Sub Cmb_ParamTgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamTgl.KeyPress
		If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
	End Sub

	Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
		If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
	End Sub

	Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
		If e.KeyChar = Chr(13) Then Chk_ParamLain.Focus()
	End Sub

	Private Sub Cmb_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamLain.KeyPress
		If e.KeyChar = Chr(13) Then Txt_ParamValue.Focus()
	End Sub

	Private Sub Txt_ParamValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamValue.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
	End Sub

	Private Sub Chk_ParamTgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_ParamTgl.KeyPress
		If e.KeyChar = Chr(13) Then
			If Chk_ParamTgl.Checked Then
				Cmb_ParamTgl.DroppedDown = True
				Cmb_ParamTgl.Focus()
			End If
		End If
	End Sub

	Private Sub Chk_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_ParamLain.KeyPress
		If e.KeyChar = Chr(13) Then
			If Chk_ParamLain.Checked Then
				Cmb_ParamLain.DroppedDown = True
				Cmb_ParamLain.Focus()
			End If
		End If
	End Sub

	Private Sub Dgv_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Parent.MouseMove
		Dim info As DataGridView.HitTestInfo = Dgv_Parent.HitTest(e.X, e.Y)

		If info.RowIndex >= 0 AndAlso info.ColumnIndex >= 0 Then
			Dgv_Parent.Cursor = Cursors.Hand
		Else
			Dgv_Parent.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Dgv_Parent_MouseLeave(sender As Object, e As EventArgs) Handles Dgv_Parent.MouseLeave
		Dgv_Parent.Cursor = Cursors.Default
	End Sub

	'==================================================================================================================================
	'=     UTILITY
	'==================================================================================================================================

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub SetUpTabelLayoutProgress(ByVal Judul As String, ByVal NameProperty As String, ByVal Column As Integer, ByVal Row As Integer)

		Dim container As New FlowLayoutPanel With {
			.FlowDirection = FlowDirection.TopDown,
			.WrapContents = False,
			.AutoSize = True,
			.Padding = New Padding(0),
			.Margin = New Padding(0),
			.Dock = DockStyle.Fill
		}

		' JUDUL
		Dim lblJudul As New Label With {
			.Text = Judul,
			.Font = New Font("Work Sans SemiBold", 9, FontStyle.Bold),
			.AutoSize = True,
			.Margin = New Padding(10, 3, 0, 0)
		}

		' ROW BODY
		Dim rowStatus As New FlowLayoutPanel With {
			.FlowDirection = FlowDirection.LeftToRight,
			.AutoSize = True,
			.Margin = New Padding(0),
			.WrapContents = False
		}

		' Label "Status:"
		rowStatus.Controls.Add(New Label With {
							   .Text = "Status: ",
							   .ForeColor = Color.Gray,
							   .AutoSize = True,
							   .Margin = New Padding(10, 2, 0, 0)
							   })

		' Label Tanggal
		rowStatus.Controls.Add(New Label With {
			.Name = $"lbl_{NameProperty}",
			.Text = "-",
			.ForeColor = Color.Gray,
			.Margin = New Padding(10, 2, 0, 0),
			.AutoSize = True
		})

		' Label Badge

		rowStatus.Controls.Add(New Label With {
			.Name = $"lblBadge_{NameProperty}_TIDAK_ADA",
			.Text = "TIDAK ADA",
			.BackColor = Color.FromArgb(243, 244, 246),
			.ForeColor = Color.FromArgb(75, 85, 99),
			.Padding = New Padding(0, 0, 0, 0),
			.Visible = False,
			.AutoSize = True,
			.Margin = New Padding(10, 2, 0, 0)
		})

		rowStatus.Controls.Add(New Label With {
			.Name = $"lblBadge_{NameProperty}_MENUNGGU",
			.Text = "MENUNGGU VALIDASI",
			.BackColor = Color.FromArgb(254, 249, 195),
			.ForeColor = Color.FromArgb(161, 98, 7),
			.Padding = New Padding(0, 0, 0, 0),
			.Visible = False,
			.AutoSize = True,
			.Margin = New Padding(10, 2, 0, 0)
		})

		rowStatus.Controls.Add(New Label With {
			.Name = $"lblBadge_{NameProperty}_DISETUJUI",
			.Text = "DISETUJUI",
			.BackColor = Color.FromArgb(220, 252, 231),
			.ForeColor = Color.FromArgb(21, 128, 61),
			.Padding = New Padding(0, 0, 0, 0),
			.Visible = False,
			.AutoSize = True,
			.Margin = New Padding(10, 2, 0, 0)
		})

		rowStatus.Controls.Add(New Label With {
			.Name = $"lblBadge_{NameProperty}_DITOLAK",
			.Text = "DITOLAK",
			.BackColor = Color.FromArgb(254, 226, 226),
			.ForeColor = Color.FromArgb(185, 28, 28),
			.Padding = New Padding(0, 0, 0, 0),
			.Visible = False,
			.AutoSize = True,
			.Margin = New Padding(10, 2, 0, 0)
		})

		container.Tag = NameProperty
		lblJudul.Tag = NameProperty
		rowStatus.Tag = NameProperty
		container.Controls.Add(lblJudul)
		container.Controls.Add(rowStatus)

		AddHandlerRecursive(container, AddressOf PanelProgress_Click)
		AddHandlerRecursive_MouseEnter(container, AddressOf Progress_MouseEnter)
		AddHandlerRecursive_MouseLeave(container, AddressOf Progress_MouseLeave)

		TblLayout_TrackingProgress.Controls.Add(container, Column, Row)
	End Sub

	Private Sub AddHandlerRecursive(ByVal parent As Control, ByVal handler As EventHandler)

		AddHandler parent.Click, handler

		For Each child As Control In parent.Controls
			' KOREKSI: Hapus "AddressOf" sebelum "Sub"
			AddHandler child.Click, Sub(s, ev)
										' Meneruskan klik ke handler utama dengan sender tetap si 'parent'
										handler.Invoke(parent, ev)
									End Sub

			' Jika child memiliki kontrol di dalamnya (seperti FlowLayoutPanel rowStatus)
			If child.HasChildren Then
				AddHandlerRecursive(child, handler)
			End If
		Next
	End Sub

	Private Sub AddHandlerRecursive_MouseEnter(ByVal parent As Control, ByVal handler As EventHandler)

		AddHandler parent.MouseEnter, handler

		For Each child As Control In parent.Controls
			' KOREKSI: Hapus "AddressOf" sebelum "Sub"
			AddHandler child.MouseEnter, Sub(s, ev)
											 ' Meneruskan klik ke handler utama dengan sender tetap si 'parent'
											 handler.Invoke(parent, ev)
										 End Sub

			' Jika child memiliki kontrol di dalamnya (seperti FlowLayoutPanel rowStatus)
			If child.HasChildren Then
				AddHandlerRecursive_MouseEnter(child, handler)
			End If
		Next
	End Sub

	Private Sub AddHandlerRecursive_MouseLeave(ByVal parent As Control, ByVal handler As EventHandler)

		AddHandler parent.MouseLeave, handler

		For Each child As Control In parent.Controls
			' KOREKSI: Hapus "AddressOf" sebelum "Sub"
			AddHandler child.MouseLeave, Sub(s, ev)
											 ' Meneruskan klik ke handler utama dengan sender tetap si 'parent'
											 handler.Invoke(parent, ev)
										 End Sub

			' Jika child memiliki kontrol di dalamnya (seperti FlowLayoutPanel rowStatus)
			If child.HasChildren Then
				AddHandlerRecursive_MouseLeave(child, handler)
			End If
		Next
	End Sub

	Private Sub Progress_MouseEnter(sender As Object, e As EventArgs)
		Dim ctrl = DirectCast(sender, Control)
		ctrl.Cursor = Cursors.Hand

		If TypeOf sender Is FlowLayoutPanel Then sender.BackColor = Color.WhiteSmoke
	End Sub

	Private Sub Progress_MouseLeave(sender As Object, e As EventArgs)
		Dim ctrl = DirectCast(sender, Control)
		ctrl.Cursor = Cursors.Default

		If TypeOf sender Is FlowLayoutPanel Then sender.BackColor = Color.Transparent
	End Sub

	Private Sub ResetTabelLayout()

		Dim allProperties() As String = {Property_AnalisaLab, Property_LookView, Property_Palatabilitas}
		Dim suffixes() As String = {"_TIDAK_ADA", "_MENUNGGU", "_DISETUJUI", "_DITOLAK"}

		For Each prop In allProperties
			Dim ctrl0 = TblLayout_TrackingProgress.Controls.Find($"lbl_{prop}", True).FirstOrDefault()
			ctrl0.Text = ""

			For Each suffix In suffixes
				Dim ctrl = TblLayout_TrackingProgress.Controls.Find($"lblBadge_{prop}{suffix}", True).FirstOrDefault()
				If ctrl IsNot Nothing Then ctrl.Visible = False
			Next
		Next

	End Sub

	Private Function CleanStatus(status As Object) As String
		Dim strStatus As String = General_Class.CekNULL(status).Trim()

		If strStatus.Contains("MENUNGGU") Then
			Return strStatus.Split("_"c)(0)
		End If

		Return strStatus
	End Function

End Class