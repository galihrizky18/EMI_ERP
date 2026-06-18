Imports excel = Microsoft.Office.Interop.Excel

Public Class N_EMI_Laporan_Final_GI_GR_Main_Baru

	Dim judulForm As String = "Display dan Laporan Final GI GR"

	'Private cts As CancellationTokenSource

	Dim lastIndex As Integer = -1
	Dim originalColor As Color

	Dim isShowScrapPackaging As Boolean = False

	Dim arrJenis As New ArrayList

	'Dim LvDetail_No_PO, LvDetail_No_Split, LvDetail_Tgl_Produksi, LvDetail_Jam_Produksi, LvDetail_Routing, LvDetail_Keterangan,
	'    LvDetail_Kd_Barang, LvDetail_Nm_Barang, LvDetail_Jumlah, LvDetail_Satuan, LvDetail_Batch, LvDetail_Berat_KG, LvDetail_GI_KG,
	'    LvDetail_GR_1_PCS, LvDetail_GR_1_KG, LvDetail_Scrap_1_KG, LvDetail_Total_1_KG, LvDetail_Loss_1_KG, LvDetail_Loss_1_Persen,
	'    LvDetail_Waste_1_Persen, LvDetail_Time_1_Day, LvDetail_GR_2_PCS, LvDetail_GR_2_KG, LvDetail_Rejected_GR_2_PCS, LvDetail_Scrap_2_KG, LvDetail_Total_2_KG,
	'    LvDetail_Wate_2_Persen, LvDetail_Time_2_Day, LvDetail_GR_Reject_PCS, LvDetail_GR_Reject_KG, LvDetail_Waste_Reject_Persen,
	'    LvDetail_Time_Reject_Day, LvDetail_GR_Final_PCS, LvDetail_GR_Final_KG, LvDetail_Scrap_Final_KG, LvDetail_Loss_Final_KG,
	'    LvDetail_Loss_Final_Persen, LvDetail_Waste_Final_Persen As String

	'Dim LvRekap_No_PO, LvRekap_No_Split, LvRekap_Tgl_Produksi, LvRekap_Jam_Produksi, LvRekap_Routing, LvRekap_Keterangan, LvRekap_Kd_Barang,
	'    LvRekap_Nm_Barang, LvRekap_Jumlah, LvRekap_Satuan, LvRekap_Berat_KG, LvRekap_GI_KG, LvRekap_GR_KG, LvRekap_Waste_KG, LvRekap_waste_Persen,
	'    LvRekap_Loss_KG, LvRekap_Loss_Persen As String

	'Dim itemDetail_No_PO As Integer = 0
	'Dim itemDetail_No_Split As Integer = 1
	'Dim itemDetail_Tgl_Produksi As Integer = 2
	'Dim itemDetail_Jam_Produksi As Integer = 3
	'Dim itemDetail_Routing As Integer = 4
	'Dim itemDetail_Keterangan As Integer = 5
	'Dim itemDetail_Kd_Barang As Integer = 6
	'Dim itemDetail_Nm_Barang As Integer = 7
	'Dim itemDetail_Jumlah As Integer = 8

	'Dim itemDetail_Batch As Integer = 10
	'Dim itemDetail_Berat_KG As Integer = 11
	'Dim itemDetail_GI_KG As Integer = 12

	'Dim itemDetail_ScrapGR1 As Integer = 13
	'Dim itemDetail_WasteGr1KG As Integer = 14
	'Dim itemDetail_Scrap_1_KG As Integer = 15
	'Dim itemDetail_Total_1_KG As Integer = 16
	'Dim itemDetail_Loss_1_KG As Integer = 17
	'Dim itemDetail_Loss_1_Persen As Integer = 18
	'Dim itemDetail_Waste_1_Persen As Integer = 19
	'Dim itemDetail_Time_1_Day As Integer = 20
	'Dim itemDetail_GR_2_PCS As Integer = 21
	'Dim itemDetail_GR_2_KG As Integer = 22
	'Dim itemDetail_Rejected_GR_2 As Integer = 23
	'Dim itemDetail_Scrap_2_KG As Integer = 24
	'Dim itemDetail_Total_2_KG As Integer = 25
	'Dim itemDetail_Wate_2_Persen As Integer = 26
	'Dim itemDetail_Time_2_Day As Integer = 27
	'Dim itemDetail_GR_Reject_PCS As Integer = 28
	'Dim itemDetail_GR_Reject_KG As Integer = 29
	'Dim itemDetail_Waste_Reject_Persen As Integer = 30
	'Dim itemDetail_Time_Reject_Day As Integer = 31
	'Dim itemDetail_GR_Final_PCS As Integer = 32
	'Dim itemDetail_GR_Final_KG As Integer = 33
	'Dim itemDetail_Scrap_Final_KG As Integer = 34
	'Dim itemDetail_Loss_Final_KG As Integer = 35
	'Dim itemDetail_Loss_Final_Persen As Integer = 36
	'Dim itemDetail_Waste_Final_Persen As Integer = 37

	'Dim itemRekap_No_PO As Integer = 0
	'Dim itemRekap_No_Split As Integer = 1
	'Dim itemRekap_Tgl_Produksi As Integer = 2
	'Dim itemRekap_Jam_Produksi As Integer = 3
	'Dim itemRekap_Routing As Integer = 4
	'Dim itemRekap_Keterangan As Integer = 5
	'Dim itemRekap_Kd_Barang As Integer = 6
	'Dim itemRekap_Nm_Barang As Integer = 7
	'Dim itemRekap_Jumlah As Integer = 8
	'Dim itemRekap_Satuan As Integer = 9
	'Dim itemRekap_Berat_KG As Integer = 10
	'Dim itemRekap_GI_KG As Integer = 11
	'Dim itemRekap_GR_1_KG As Integer = 12
	'Dim itemRekap_Waste_1_KG As Integer = 13
	'Dim itemRekap_GR_2_KG As Integer = 14
	'Dim itemRekap_Waste_2_KG As Integer = 15
	'Dim itemRekap_Waste_3_KG As Integer = 16
	'Dim itemRekap_GR_KG As Integer = 17
	'Dim itemRekap_Waste_KG As Integer = 18
	'Dim itemRekap_waste_Persen As Integer = 19
	'Dim itemRekap_Loss_KG As Integer = 20
	'Dim itemRekap_Loss_Persen As Integer = 21

	Private Sub N_EMI_Laporan_Final_GI_GR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBufferDGV(Dgv_Rekap)
		EnableDoubleBufferDGV(Dgv_Detail2)
		EnableDoubleBufferDGV(Dgv_Detail2_Split)

		'Me.WindowState = FormWindowState.Maximized
		Me.Dock = DockStyle.Fill

		Lv_Routing.Columns.Clear()
		Lv_Routing.Columns.Add("Id Routing", 150, HorizontalAlignment.Center)
		Lv_Routing.Columns.Add("Nama Routing", 260, HorizontalAlignment.Left)
		Lv_Routing.View = View.Details

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 260, HorizontalAlignment.Left)
		Lv_Barang.View = View.Details

		Dgv_Rekap.Columns(34).DisplayIndex = 6
		Dgv_Rekap.Columns(35).DisplayIndex = 7

		Dgv_Detail2.Columns(40).DisplayIndex = 6
		Dgv_Detail2.Columns(41).DisplayIndex = 7
		Dgv_Detail2.Columns(44).DisplayIndex = 26
		Dgv_Detail2.Columns(45).DisplayIndex = 33

		Try
			OpenConn()

			'===========================
			'=     CEK ROLE BUTTON     =
			'===========================
			If CekButtonRole("Show_Scrap_Packaging_Laporan_GIGR_Final") = "Y" Then
				isShowScrapPackaging = True
			Else
				isShowScrapPackaging = False
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If isShowScrapPackaging Then
			Dgv_Rekap.Columns(36).Visible = True
			Dgv_Detail2.Columns(42).Visible = True
		Else
			Dgv_Rekap.Columns(36).Visible = False
			Dgv_Detail2.Columns(42).Visible = False
		End If

		Kosong()

	End Sub

	Private Sub Kosong()

		Tgl1.Value = DateTime.Today : Tgl2.Value = DateTime.Today
		Txt_IdRouting.Text = OpsiSeluruh : Txt_NmRouting.Text = OpsiSeluruh
		Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh
		Txt_NoSplit.Text = OpsiSeluruh

		Lv_Routing.Visible = False : Lv_Barang.Visible = False

		Try
			OpenConn()

			Cmb_Jenis.Items.Clear() : arrJenis.Clear()
			Cmb_Jenis.Items.Add(OpsiSeluruh) : arrJenis.Add(OpsiSeluruh)
			SQL = "select Kode_Group_Jenis, "
			SQL = SQL & "case when Flag_Finished_Good = 'Y' then 'Barang Jadi' "
			SQL = SQL & "when Flag_Semi_FG = 'Y' then 'Barang Setengah Jadi' "
			SQL = SQL & "end as 'Keterangan', "
			SQL = SQL & "case when Flag_Finished_Good = 'Y' and Flag_Semi_FG ='T' then 'Y' "
			SQL = SQL & "when Flag_Semi_FG = 'Y' and Flag_Finished_Good = 'T' then 'T' "
			SQL = SQL & "end as 'Flag' "
			SQL = SQL & "from EMI_Group_Jenis where kode_perusahaan = '" & KodePerusahaan & "' and (Flag_Finished_Good = 'Y' or Flag_Semi_FG = 'Y') "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Cmb_Jenis.Items.Add(dr("Keterangan")) : arrJenis.Add(dr("Flag"))
				Loop
			End Using
			Cmb_Jenis.SelectedIndex = 1

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'LoadRekap()
		'LoadDataDetail()

		Dgv_Rekap.Rows.Clear()
		Dgv_Detail2.Rows.Clear()
		Dgv_Detail2_Split.Rows.Clear()

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Async Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_IdRouting.Text.Trim.Length = 0 Then
			MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_IdRouting.Focus() : Exit Sub
		ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		ElseIf Cmb_Jenis.SelectedIndex = -1 Then
			MessageBox.Show("Jenis harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		ElseIf Txt_NoSplit.Text.Trim.Length = 0 Then
			Txt_NoSplit.Text = OpsiSeluruh
		End If
		'Dim sw As New Stopwatch()
		'sw.Start()
		TabControl1.SelectedIndex = 0
		LoadRekap(True)

		'sw.Stop()

		'Console.WriteLine("Waktu query: " & sw.ElapsedMilliseconds & " ms")

		'LoadDataDetail(True)
		'LoadDataDetailSplit(True)

		'Await LoadDLoadData_Thread(True)

	End Sub

	'Private Async Function LoadData_Thread(Filter As Boolean) As Task

	'	'=========================================================================================================
	'	'=     UNTUK GENERATE TOKEN YANG AKAN DIGUNAKAN PADA 3 TASK. JIKA 1 TASK GAGAL MAKA 2 LAINNYA CANCEL     =
	'	'=========================================================================================================
	'	cts = New CancellationTokenSource()
	'	Dim token As CancellationToken = cts.Token

	'	Try

	'		'===============================================
	'		'=     FUNGSI MENJALANKAN 3 TASK SEKALIGUS     =
	'		'===============================================

	'		Await Task.Run(Sub()
	'						   Dim t1 As Task = Task.Run(Sub() LoadRekap(token, Filter))
	'						   Dim t2 As Task = Task.Run(Sub() LoadDataDetail(token, Filter))
	'						   Dim t3 As Task = Task.Run(Sub() LoadDataDetailSplit(token, Filter))

	'						   '==============================================================
	'						   '=     TUNGGU SEMUA TASK SELESAI BAIK SELESAI MAUPUN EROR     =
	'						   '==============================================================
	'						   Task.WaitAll(t1, t2, t3)
	'					   End Sub, token)

	'		If Not token.IsCancellationRequested Then
	'			'MessageBox.Show("Semua Proses Berhasil Dicommit / Dijalankan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
	'		End If
	'	Catch ex As Exception
	'		'==============================================
	'		'=     BAGTIAN UNTUK HANDLE ERROR GLOBAL      =
	'		'==============================================
	'	Finally
	'		'===================================================================
	'		'=     BAGIAN UNTUK MENUTUP/MENGHAPUS TOKEN SETELAH DIGUNAKAN      =
	'		'===================================================================
	'		cts.Dispose()
	'	End Try

	'End Function

	Private Sub Cmb_Jenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis.SelectedIndexChanged
		If Cmb_Jenis.SelectedIndex = -1 Then Exit Sub

		'=======================
		'=     SET DECIMAL     =
		'=======================

		If arrJenis(Cmb_Jenis.SelectedIndex) = "Y" Then

			Dgv_Rekap.Columns(8).HeaderText = "Pro-RQ (PCS)"
			Dgv_Rekap.Columns(25).HeaderText = "GR Inspection (PCS)"
			Dgv_Rekap.Columns(27).HeaderText = "Final GR (PCS)"

			Dgv_Detail2.Columns(8).HeaderText = "PRO-RQ (PCS)"
			Dgv_Detail2.Columns(19).HeaderText = "GR 1 (PCS)"
			Dgv_Detail2.Columns(22).HeaderText = "Waste GR 2 (PCS)"
			Dgv_Detail2.Columns(24).HeaderText = "Reject GR 2 (PCS)"
			Dgv_Detail2.Columns(26).HeaderText = "Scrap GR 2 (PCS)"
			Dgv_Detail2.Columns(28).HeaderText = "GR 2 (PCS)"
			Dgv_Detail2.Columns(29).HeaderText = "Stock Sementara (PCS)"
			Dgv_Detail2.Columns(31).HeaderText = "Waste GR 3 (PCS)"
			Dgv_Detail2.Columns(33).HeaderText = "Reject GR 3 (PCS)"
			Dgv_Detail2.Columns(35).HeaderText = "Scrap GR 3 (PCS)"
			Dgv_Detail2.Columns(37).HeaderText = "Final GR (PCS)"
			Dgv_Detail2.Columns(38).HeaderText = "Stock Sementara (PCS)"

		ElseIf arrJenis(Cmb_Jenis.SelectedIndex) = "T" Then

			Dgv_Rekap.Columns(8).HeaderText = "Pro-RQ (KG)"
			Dgv_Rekap.Columns(25).HeaderText = "GR Inspection (KG)"
			Dgv_Rekap.Columns(27).HeaderText = "Final GR (KG)"

			Dgv_Detail2.Columns(8).HeaderText = "PRO-RQ (KG)"
			Dgv_Detail2.Columns(19).HeaderText = "GR 1 (KG)"
			Dgv_Detail2.Columns(22).HeaderText = "Waste GR 2 (KG)"
			Dgv_Detail2.Columns(24).HeaderText = "Reject GR 2 (KG)"
			Dgv_Detail2.Columns(26).HeaderText = "Scrap GR 2 (KG)"
			Dgv_Detail2.Columns(28).HeaderText = "GR 2 (KG)"
			Dgv_Detail2.Columns(29).HeaderText = "Stock Sementara (KG)"
			Dgv_Detail2.Columns(31).HeaderText = "Waste GR 3 (KG)"
			Dgv_Detail2.Columns(33).HeaderText = "Reject GR 3 (KG)"
			Dgv_Detail2.Columns(35).HeaderText = "Scrap GR 3 (KG)"
			Dgv_Detail2.Columns(37).HeaderText = "Final GR (KG)"
			Dgv_Detail2.Columns(38).HeaderText = "Stock Sementara (KG)"
		Else

			Dgv_Rekap.Columns(8).HeaderText = "Pro-RQ (PCS)"
			Dgv_Rekap.Columns(25).HeaderText = "GR Inspection (PCS)"
			Dgv_Rekap.Columns(27).HeaderText = "Final GR (PCS)"

			Dgv_Detail2.Columns(8).HeaderText = "PRO-RQ (PCS)"
			Dgv_Detail2.Columns(19).HeaderText = "GR 1 (PCS)"
			Dgv_Detail2.Columns(22).HeaderText = "Waste GR 2 (PCS)"
			Dgv_Detail2.Columns(24).HeaderText = "Reject GR 2 (PCS)"
			Dgv_Detail2.Columns(26).HeaderText = "Scrap GR 2 (PCS)"
			Dgv_Detail2.Columns(28).HeaderText = "GR 2 (PCS)"
			Dgv_Detail2.Columns(29).HeaderText = "Stock Sementara (PCS)"
			Dgv_Detail2.Columns(31).HeaderText = "Waste GR 3 (PCS)"
			Dgv_Detail2.Columns(33).HeaderText = "Reject GR 3 (PCS)"
			Dgv_Detail2.Columns(35).HeaderText = "Scrap GR 3 (PCS)"
			Dgv_Detail2.Columns(37).HeaderText = "Final GR (PCS)"
			Dgv_Detail2.Columns(38).HeaderText = "Stock Sementara (PCS)"

		End If

	End Sub

	Private Sub LoadRekap(Optional ByVal isFilter As Boolean = False)

		Dim DigitDecimal As String = ""

		Select Case arrJenis(Cmb_Jenis.SelectedIndex)
			Case "Y"
				DigitDecimal = "N0"
			Case "T"
				DigitDecimal = "N4"
			Case Else
				DigitDecimal = "N4"
		End Select

		Dim Filter_Tanggal_Awal As String = $"'{Format(Tgl1.Value, "yyyy-MM-dd")}'"
		Dim Filter_Tanggal_Akhir As String = $"'{Format(Tgl2.Value.AddDays(1), "yyyy-MM-dd")}'"
		Dim Filter_NoSplit As String = "DEFAULT"
		Dim Filter_ID_Routing As String = "DEFAULT"
		Dim Filter_KodeBarang As String = "DEFAULT"
		Dim Filter_GroupJenis As String = "DEFAULT"

		If isFilter Then

			If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
				Filter_NoSplit = $"'{Txt_NoSplit.Text.Trim}'"
			End If

			If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
				Filter_ID_Routing = $"'{Txt_IdRouting.Text.Trim}'"
			End If

			If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
				Filter_KodeBarang = $"'{Txt_KdBarang.Text.Trim}'"
			End If

			If Not Cmb_Jenis.SelectedIndex = 0 Then
				Filter_GroupJenis = $"'{arrJenis(Cmb_Jenis.SelectedIndex).ToString.Trim}'"
			End If

		End If

		Try
			OpenConn()

			'=======================
			'=     SET DECIMAL     =
			'=======================

			Dgv_Rekap.Rows.Clear()

#Region "Kode Lama 09 Juni 2026"

			'SQL = "select no_po, No_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Berat_GI, Jumlah_Dosing, "
			'SQL = SQL & "Pro_Reject_KG, Qc_Reject_KG, Warehouse_Reject_KG, Tot_Reject_KG, "
			'SQL = SQL & "ScrapGR1_KG, ScrapGR2_KG, ScrapGR3_KG, ScrapTotal_KG, "
			'SQL = SQL & "WasteGR1_KG, WasteGR2_KG, WasteGR3_KG, WasteTotal_KG, Loss_Production_Final_GR, "
			'SQL = SQL & "NilaiGRFinal_KG, NilaiGRFinal_Pcs, Tanggal_GI, Jam_GI, "
			'SQL = SQL & "Reject_Final_Persen, ScrapTotal_Persen, WasteTotal_Persen, Loss_Production_Final_GR_Persen, NilaiGRFinal_Persen, GR_Inspection_KG, "
			'SQL = SQL & "GR_Inspection_PCS, GR_Inspection_Persen, Status, Jumlah_Scrap_Packaging, Flag_Preservative "
			'SQL = SQL & "from Laporan_Akhir_GIGR_Rekap2 "
			'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value.AddDays(1), "yyyy-MM-dd") & "' "

			'If isFilter Then

			'	If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
			'		SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
			'	End If

			'	If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
			'		SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
			'	End If

			'	If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
			'		SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
			'	End If

			'	If Not Cmb_Jenis.SelectedIndex = 0 Then
			'		SQL = SQL & "and Group_Jenis = '" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
			'	End If

			'End If

			'SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "

#End Region

			SQL = $"
				SELECT no_po, No_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Berat_GI, Jumlah_Dosing,
						Pro_Reject_KG, Qc_Reject_KG, Warehouse_Reject_KG, Tot_Reject_KG,
						ScrapGR1_KG, ScrapGR2_KG, ScrapGR3_KG, ScrapTotal_KG,
						WasteGR1_KG, WasteGR2_KG, WasteGR3_KG, WasteTotal_KG, Loss_Production_Final_GR,
						NilaiGRFinal_KG, NilaiGRFinal_Pcs, Tanggal_GI, Jam_GI,
						Reject_Final_Persen, ScrapTotal_Persen, WasteTotal_Persen, Loss_Production_Final_GR_Persen, NilaiGRFinal_Persen, GR_Inspection_KG,
						GR_Inspection_PCS, GR_Inspection_Persen, Status, Jumlah_Scrap_Packaging, Flag_Preservative
				FROM N_EMI_Function_Laporan_Akhir_GIGR_Rekap2('{KodePerusahaan}', {Filter_NoSplit}, {Filter_Tanggal_Awal}, {Filter_Tanggal_Akhir}, {Filter_ID_Routing},
				{Filter_KodeBarang}, {Filter_GroupJenis})
				order by Tgl_Produksi, Jam_Produksi
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dgv_Rekap.Rows.Add(1)
							Dgv_Rekap.Rows(i).Cells(0).Value = .Rows(i).Item("no_po")
							Dgv_Rekap.Rows(i).Cells(1).Value = .Rows(i).Item("No_split")
							Dgv_Rekap.Rows(i).Cells(2).Value = .Rows(i).Item("Kode_Barang")
							Dgv_Rekap.Rows(i).Cells(3).Value = .Rows(i).Item("Nama")
							Dgv_Rekap.Rows(i).Cells(4).Value = Format(.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy")
							Dgv_Rekap.Rows(i).Cells(5).Value = .Rows(i).Item("Jam_Produksi")
							Dgv_Rekap.Rows(i).Cells(6).Value = .Rows(i).Item("Nama_Routing")
							Dgv_Rekap.Rows(i).Cells(7).Value = .Rows(i).Item("Keterangan")
							Dgv_Rekap.Rows(i).Cells(8).Value = If(General_Class.CekNULL(.Rows(i).Item("Jumlah")) = "", 0, Format(.Rows(i).Item("Jumlah"), DigitDecimal))
							Dgv_Rekap.Rows(i).Cells(9).Value = If(General_Class.CekNULL(.Rows(i).Item("Berat_GI")) = "", 0, Format(.Rows(i).Item("Berat_GI"), "N0"))
							Dgv_Rekap.Rows(i).Cells(10).Value = If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Dosing")) = "", 0, Format(.Rows(i).Item("Jumlah_Dosing"), "N4"))

							' REJECT
							Dgv_Rekap.Rows(i).Cells(11).Value = If(General_Class.CekNULL(.Rows(i).Item("Pro_Reject_KG")) = "", 0, Format(.Rows(i).Item("Pro_Reject_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(12).Value = If(General_Class.CekNULL(.Rows(i).Item("Qc_Reject_KG")) = "", 0, Format(.Rows(i).Item("Qc_Reject_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(13).Value = If(General_Class.CekNULL(.Rows(i).Item("Warehouse_Reject_KG")) = "", 0, Format(.Rows(i).Item("Warehouse_Reject_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(14).Value = If(General_Class.CekNULL(.Rows(i).Item("Tot_Reject_KG")) = "", 0, Format(.Rows(i).Item("Tot_Reject_KG"), "N4"))

							Dgv_Rekap.Rows(i).Cells(15).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR1_KG")) = "", 0, Format(.Rows(i).Item("ScrapGR1_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(16).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR2_KG")) = "", 0, Format(.Rows(i).Item("ScrapGR2_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(17).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR3_KG")) = "", 0, Format(.Rows(i).Item("ScrapGR3_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(18).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapTotal_KG")) = "", 0, Format(.Rows(i).Item("ScrapTotal_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(19).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR1_KG")) = "", 0, Format(.Rows(i).Item("WasteGR1_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(20).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR2_KG")) = "", 0, Format(.Rows(i).Item("WasteGR2_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(21).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR3_KG")) = "", 0, Format(.Rows(i).Item("WasteGR3_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(22).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteTotal_KG")) = "", 0, Format(.Rows(i).Item("WasteTotal_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(23).Value = If(General_Class.CekNULL(.Rows(i).Item("Loss_Production_Final_GR")) = "", 0, Format(.Rows(i).Item("Loss_Production_Final_GR"), "N4"))
							Dgv_Rekap.Rows(i).Cells(24).Value = If(General_Class.CekNULL(.Rows(i).Item("GR_Inspection_KG")) = "", 0, Format(.Rows(i).Item("GR_Inspection_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(25).Value = If(General_Class.CekNULL(.Rows(i).Item("GR_Inspection_PCS")) = "", 0, Format(.Rows(i).Item("GR_Inspection_PCS"), DigitDecimal))
							Dgv_Rekap.Rows(i).Cells(26).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGRFinal_KG")) = "", 0, Format(.Rows(i).Item("NilaiGRFinal_KG"), "N4"))
							Dgv_Rekap.Rows(i).Cells(27).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGRFinal_Pcs")) = "", 0, Format(.Rows(i).Item("NilaiGRFinal_Pcs"), DigitDecimal))

							Dgv_Rekap.Rows(i).Cells(28).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_Final_Persen")) = "", 0, Format(.Rows(i).Item("Reject_Final_Persen"), "N4"))
							Dgv_Rekap.Rows(i).Cells(29).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapTotal_Persen")) = "", 0, Format(.Rows(i).Item("ScrapTotal_Persen"), "N4"))
							Dgv_Rekap.Rows(i).Cells(30).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteTotal_Persen")) = "", 0, Format(.Rows(i).Item("WasteTotal_Persen"), "N4"))
							Dgv_Rekap.Rows(i).Cells(31).Value = If(General_Class.CekNULL(.Rows(i).Item("Loss_Production_Final_GR_Persen")) = "", 0, Format(.Rows(i).Item("Loss_Production_Final_GR_Persen"), "N4"))
							Dgv_Rekap.Rows(i).Cells(32).Value = If(General_Class.CekNULL(.Rows(i).Item("GR_Inspection_Persen")) = "", 0, Format(.Rows(i).Item("GR_Inspection_Persen"), "N4"))
							Dgv_Rekap.Rows(i).Cells(33).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGRFinal_Persen")) = "", 0, Format(.Rows(i).Item("NilaiGRFinal_Persen"), "N4"))

							Dgv_Rekap.Rows(i).Cells(34).Value = If(General_Class.CekNULL(.Rows(i).Item("Tanggal_GI")) = "", "-", Format(.Rows(i).Item("Tanggal_GI"), "dd MMM yyyy"))
							Dgv_Rekap.Rows(i).Cells(35).Value = If(General_Class.CekNULL(.Rows(i).Item("Jam_GI")) = "", "-", .Rows(i).Item("Jam_GI"))

							Dgv_Rekap.Rows(i).Cells(36).Value = If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Scrap_Packaging")) = "", "-", Format(.Rows(i).Item("Jumlah_Scrap_Packaging"), "N4"))
							Dgv_Rekap.Rows(i).Cells(37).Value = If(General_Class.CekNULL(.Rows(i).Item("Flag_Preservative")) = "", "-", .Rows(i).Item("Flag_Preservative"))
							Dgv_Rekap.Rows(i).Cells(38).Value = If(General_Class.CekNULL(.Rows(i).Item("Status")) = "", "-", .Rows(i).Item("Status"))

							If Not General_Class.CekNULL(.Rows(i).Item("Status")) = "" Then
								If .Rows(i).Item("Status") = "PRODUCTION" Then
									Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.LightYellow
								ElseIf .Rows(i).Item("Status") = "INSPECTION" Then
									Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.LightBlue
								ElseIf .Rows(i).Item("Status") = "WAREHOUSE" Then
									Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.LightGray
								Else
									Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.White
								End If
							Else
								Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.White
							End If

							'== WARNA =='

							Dgv_Rekap.Rows(i).Cells(11).Style.BackColor = Color.FromArgb(255, 206, 117)
							Dgv_Rekap.Rows(i).Cells(12).Style.BackColor = Color.FromArgb(255, 206, 117)
							Dgv_Rekap.Rows(i).Cells(13).Style.BackColor = Color.FromArgb(255, 206, 117)
							Dgv_Rekap.Rows(i).Cells(14).Style.BackColor = Color.FromArgb(255, 206, 117)

							Dgv_Rekap.Rows(i).Cells(15).Style.BackColor = Color.LightYellow
							Dgv_Rekap.Rows(i).Cells(16).Style.BackColor = Color.LightYellow
							Dgv_Rekap.Rows(i).Cells(17).Style.BackColor = Color.LightYellow
							Dgv_Rekap.Rows(i).Cells(18).Style.BackColor = Color.LightYellow

							Dgv_Rekap.Rows(i).Cells(19).Style.BackColor = Color.LightBlue
							Dgv_Rekap.Rows(i).Cells(20).Style.BackColor = Color.LightBlue
							Dgv_Rekap.Rows(i).Cells(21).Style.BackColor = Color.LightBlue
							Dgv_Rekap.Rows(i).Cells(22).Style.BackColor = Color.LightBlue
							Dgv_Rekap.Rows(i).Cells(23).Style.BackColor = Color.LightGreen
							Dgv_Rekap.Rows(i).Cells(24).Style.BackColor = Color.LightCoral
							Dgv_Rekap.Rows(i).Cells(25).Style.BackColor = Color.LightCoral
							Dgv_Rekap.Rows(i).Cells(26).Style.BackColor = Color.LightGray
							Dgv_Rekap.Rows(i).Cells(27).Style.BackColor = Color.LightGray
							Dgv_Rekap.Rows(i).Cells(28).Style.BackColor = Color.LightCyan
							Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.LightCyan
							Dgv_Rekap.Rows(i).Cells(30).Style.BackColor = Color.LightCyan
							Dgv_Rekap.Rows(i).Cells(31).Style.BackColor = Color.LightCyan
							Dgv_Rekap.Rows(i).Cells(32).Style.BackColor = Color.LightCyan
							Dgv_Rekap.Rows(i).Cells(33).Style.BackColor = Color.LightCyan

							Dgv_Rekap.Rows(i).Cells(36).Style.BackColor = Color.FromArgb(244, 208, 111)
							Dgv_Rekap.Rows(i).Cells(37).Style.BackColor = Color.FromArgb(188, 158, 130)

							Dgv_Rekap.Rows(i).Cells(32).Style.Font = New Font(Dgv_Rekap.Font, FontStyle.Bold)

						Next
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try

	End Sub

	Private Sub LoadDataDetail(Optional ByVal isFilter As Boolean = False)

		Dim DigitDecimal As String = ""

		Select Case arrJenis(Cmb_Jenis.SelectedIndex)
			Case "Y"
				DigitDecimal = "N0"
			Case "T"
				DigitDecimal = "N4"
			Case Else
				DigitDecimal = "N4"
		End Select

		Try
			OpenConn()

			Dgv_Detail2.Rows.Clear()
			Dim Filter_Tanggal_Awal As String = $"'{Format(Tgl1.Value, "yyyy-MM-dd")}'"
			Dim Filter_Tanggal_Akhir As String = $"'{Format(Tgl2.Value.AddDays(1), "yyyy-MM-dd")}'"
			Dim Filter_NoSplit As String = "DEFAULT"
			Dim Filter_ID_Routing As String = "DEFAULT"
			Dim Filter_KodeBarang As String = "DEFAULT"
			Dim Filter_GroupJenis As String = "DEFAULT"

			If isFilter Then

				If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
					Filter_NoSplit = $"'{Txt_NoSplit.Text.Trim}'"
				End If

				If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
					Filter_ID_Routing = $"'{Txt_IdRouting.Text.Trim}'"
				End If

				If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
					Filter_KodeBarang = $"'{Txt_KdBarang.Text.Trim}'"
				End If

				If Not Cmb_Jenis.SelectedIndex = 0 Then
					Filter_GroupJenis = $"'{arrJenis(Cmb_Jenis.SelectedIndex).ToString.Trim}'"
				End If

			End If

			SQL = $"
				SELECT No_po, no_split, Tgl_Produksi, Jam_Produksi, Tanggal_GI, Jam_GI, Nama_Routing, Keterangan, Kode_Barang, Nama,
					   Jumlah, batch, Berat_GI, Jumlah_Dosing,
					   WasteGR1_KG, WasteGR1_Persen, Reject_GR1_KG, ScrapGR1_KG, Loss_Production, Loss_Production_Persen, NilaiGR1_KG,
					   NilaiGR1_Pcs, GR1_StockSementara, WaktuGR1, Adjustment,
					   WasteGR2_Pcs, WasteGR2_Persen, Persentase_Waste_Terhadap_GR2, Reject_GR2_PCS, Reject_GR2_KG, ScrapGR2_PCS,
					   ScrapGR2_KG, NilaiGR2_Pcs, GR2_StockSementara, GR2_Stock_Belum_Validasi, WaktuGR2,
					   WasteGR3_Pcs, WasteGR3_Persen, Reject_GR3_PCS, Reject_GR3_KG, ScrapGR3_PCS, ScrapGR3_KG, NilaiGRFinal_Pcs,
					   GR3_StockSementara, WaktuGR3,
					   Jumlah_Scrap_Packaging, Flag_Preservative
				FROM N_EMI_Function_Laporan_Akhir_GIGR2('{KodePerusahaan}', {Filter_NoSplit}, {Filter_Tanggal_Awal}, {Filter_Tanggal_Akhir}, {Filter_ID_Routing},
				{Filter_KodeBarang}, {Filter_GroupJenis})
				order by Tgl_Produksi, Jam_Produksi
			"

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dgv_Detail2.Rows.Add(1)
							Dgv_Detail2.Rows(i).Cells(0).Value = .Rows(i).Item("No_po")
							Dgv_Detail2.Rows(i).Cells(1).Value = .Rows(i).Item("no_split")
							Dgv_Detail2.Rows(i).Cells(2).Value = .Rows(i).Item("Kode_Barang")
							Dgv_Detail2.Rows(i).Cells(3).Value = .Rows(i).Item("Nama")
							Dgv_Detail2.Rows(i).Cells(4).Value = Format(.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy")
							Dgv_Detail2.Rows(i).Cells(5).Value = .Rows(i).Item("Jam_Produksi")
							Dgv_Detail2.Rows(i).Cells(6).Value = .Rows(i).Item("Nama_Routing")
							Dgv_Detail2.Rows(i).Cells(7).Value = .Rows(i).Item("Keterangan")
							Dgv_Detail2.Rows(i).Cells(8).Value = If(General_Class.CekNULL(.Rows(i).Item("Jumlah")) = "", 0, Format(.Rows(i).Item("Jumlah"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(9).Value = .Rows(i).Item("batch")
							Dgv_Detail2.Rows(i).Cells(10).Value = If(General_Class.CekNULL(.Rows(i).Item("Berat_GI")) = "", 0, Format(.Rows(i).Item("Berat_GI"), "N0"))
							Dgv_Detail2.Rows(i).Cells(11).Value = If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Dosing")) = "", 0, Format(.Rows(i).Item("Jumlah_Dosing"), "N4"))
							Dgv_Detail2.Rows(i).Cells(12).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR1_KG")) = "", 0, Format(.Rows(i).Item("WasteGR1_KG"), "N4"))
							Dgv_Detail2.Rows(i).Cells(13).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR1_Persen")) = "", 0, Format(.Rows(i).Item("WasteGR1_Persen"), "N4"))
							'REJECT
							Dgv_Detail2.Rows(i).Cells(14).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_GR1_KG")) = "", 0, Format(.Rows(i).Item("Reject_GR1_KG"), "N4"))
							Dgv_Detail2.Rows(i).Cells(15).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR1_KG")) = "", 0, Format(.Rows(i).Item("ScrapGR1_KG"), "N4"))
							Dgv_Detail2.Rows(i).Cells(16).Value = If(General_Class.CekNULL(.Rows(i).Item("Loss_Production")) = "", 0, Format(.Rows(i).Item("Loss_Production"), "N4"))
							Dgv_Detail2.Rows(i).Cells(17).Value = If(General_Class.CekNULL(.Rows(i).Item("Loss_Production_Persen")) = "", 0, Format(.Rows(i).Item("Loss_Production_Persen"), "N4"))
							Dgv_Detail2.Rows(i).Cells(18).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGR1_KG")) = "", 0, Format(.Rows(i).Item("NilaiGR1_KG"), "N4"))
							Dgv_Detail2.Rows(i).Cells(19).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGR1_Pcs")) = "", 0, Format(.Rows(i).Item("NilaiGR1_Pcs"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(20).Value = If(General_Class.CekNULL(.Rows(i).Item("GR1_StockSementara")) = "", 0, Format(.Rows(i).Item("GR1_StockSementara"), "N4"))
							Dgv_Detail2.Rows(i).Cells(21).Value = .Rows(i).Item("WaktuGR1")
							Dgv_Detail2.Rows(i).Cells(22).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR2_Pcs")) = "", 0, Format(.Rows(i).Item("WasteGR2_Pcs"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(23).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR2_Persen")) = "", 0, Format(.Rows(i).Item("WasteGR2_Persen"), "N4"))

							Dgv_Detail2.Rows(i).Cells(44).Value = If(General_Class.CekNULL(.Rows(i).Item("Persentase_Waste_Terhadap_GR2")) = "", 0, Format(.Rows(i).Item("Persentase_Waste_Terhadap_GR2"), "N4"))

							'REJECT
							Dgv_Detail2.Rows(i).Cells(24).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_GR2_PCS")) = "", 0, Format(.Rows(i).Item("Reject_GR2_PCS"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(25).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_GR2_KG")) = "", 0, Format(.Rows(i).Item("Reject_GR2_KG"), "N4"))

							Dgv_Detail2.Rows(i).Cells(26).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR2_PCS")) = "", 0, Format(.Rows(i).Item("ScrapGR2_PCS"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(27).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR2_KG")) = "", 0, Format(.Rows(i).Item("ScrapGR2_KG"), "N4"))
							Dgv_Detail2.Rows(i).Cells(28).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGR2_Pcs")) = "", 0, Format(.Rows(i).Item("NilaiGR2_Pcs"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(29).Value = If(General_Class.CekNULL(.Rows(i).Item("GR2_StockSementara")) = "", 0, Format(.Rows(i).Item("GR2_StockSementara"), DigitDecimal))

							Dgv_Detail2.Rows(i).Cells(30).Value = .Rows(i).Item("WaktuGR2")
							Dgv_Detail2.Rows(i).Cells(31).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR3_Pcs")) = "", 0, Format(.Rows(i).Item("WasteGR3_Pcs"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(32).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR3_Persen")) = "", 0, Format(.Rows(i).Item("WasteGR3_Persen"), "N4"))

							'REJECT
							Dgv_Detail2.Rows(i).Cells(33).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_GR3_PCS")) = "", 0, Format(.Rows(i).Item("Reject_GR3_PCS"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(34).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_GR3_KG")) = "", 0, Format(.Rows(i).Item("Reject_GR3_KG"), "N4"))

							Dgv_Detail2.Rows(i).Cells(35).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR3_PCS")) = "", 0, Format(.Rows(i).Item("ScrapGR3_PCS"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(36).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR3_KG")) = "", 0, Format(.Rows(i).Item("ScrapGR3_KG"), "N4"))
							Dgv_Detail2.Rows(i).Cells(37).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGRFinal_Pcs")) = "", 0, Format(.Rows(i).Item("NilaiGRFinal_Pcs"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(38).Value = If(General_Class.CekNULL(.Rows(i).Item("GR3_StockSementara")) = "", 0, Format(.Rows(i).Item("GR3_StockSementara"), DigitDecimal))
							Dgv_Detail2.Rows(i).Cells(39).Value = .Rows(i).Item("WaktuGR3")

							Dgv_Detail2.Rows(i).Cells(40).Value = Format(.Rows(i).Item("Tanggal_GI"), "dd MMM yyyy")
							Dgv_Detail2.Rows(i).Cells(41).Value = .Rows(i).Item("Jam_GI")

							Dgv_Detail2.Rows(i).Cells(42).Value = Format(.Rows(i).Item("Jumlah_Scrap_Packaging"), "N4")
							Dgv_Detail2.Rows(i).Cells(43).Value = .Rows(i).Item("Flag_Preservative")

							Dgv_Detail2.Rows(i).Cells(45).Value = If(General_Class.CekNULL(.Rows(i).Item("GR2_Stock_Belum_Validasi")) = "", 0, Format(.Rows(i).Item("GR2_Stock_Belum_Validasi"), DigitDecimal))

							''== WARNA =='
							Dgv_Detail2.Rows(i).Cells(12).Style.BackColor = Color.LightYellow
							Dgv_Detail2.Rows(i).Cells(13).Style.BackColor = Color.LightYellow
							Dgv_Detail2.Rows(i).Cells(14).Style.BackColor = Color.LightYellow ' REJECT
							Dgv_Detail2.Rows(i).Cells(15).Style.BackColor = Color.LightYellow
							Dgv_Detail2.Rows(i).Cells(16).Style.BackColor = Color.LightYellow
							Dgv_Detail2.Rows(i).Cells(17).Style.BackColor = Color.LightYellow
							Dgv_Detail2.Rows(i).Cells(18).Style.BackColor = Color.LightYellow
							Dgv_Detail2.Rows(i).Cells(19).Style.BackColor = Color.LightYellow
							Dgv_Detail2.Rows(i).Cells(20).Style.BackColor = Color.LightYellow
							Dgv_Detail2.Rows(i).Cells(21).Style.BackColor = Color.FromArgb(252, 105, 108)
							Dgv_Detail2.Rows(i).Cells(22).Style.BackColor = Color.LightBlue
							Dgv_Detail2.Rows(i).Cells(23).Style.BackColor = Color.LightBlue
							Dgv_Detail2.Rows(i).Cells(24).Style.BackColor = Color.LightBlue
							Dgv_Detail2.Rows(i).Cells(25).Style.BackColor = Color.LightBlue
							Dgv_Detail2.Rows(i).Cells(26).Style.BackColor = Color.LightBlue
							Dgv_Detail2.Rows(i).Cells(27).Style.BackColor = Color.LightBlue
							Dgv_Detail2.Rows(i).Cells(28).Style.BackColor = Color.LightBlue
							Dgv_Detail2.Rows(i).Cells(29).Style.BackColor = Color.LightBlue
							Dgv_Detail2.Rows(i).Cells(44).Style.BackColor = Color.LightBlue
							Dgv_Detail2.Rows(i).Cells(45).Style.BackColor = Color.LightBlue
							Dgv_Detail2.Rows(i).Cells(30).Style.BackColor = Color.FromArgb(181, 230, 162)
							Dgv_Detail2.Rows(i).Cells(31).Style.BackColor = Color.LightGray
							Dgv_Detail2.Rows(i).Cells(32).Style.BackColor = Color.LightGray
							Dgv_Detail2.Rows(i).Cells(33).Style.BackColor = Color.LightGray
							Dgv_Detail2.Rows(i).Cells(34).Style.BackColor = Color.LightGray
							Dgv_Detail2.Rows(i).Cells(35).Style.BackColor = Color.LightGray
							Dgv_Detail2.Rows(i).Cells(36).Style.BackColor = Color.LightGray
							Dgv_Detail2.Rows(i).Cells(37).Style.BackColor = Color.LightGray
							Dgv_Detail2.Rows(i).Cells(38).Style.BackColor = Color.LightGray
							Dgv_Detail2.Rows(i).Cells(39).Style.BackColor = Color.White

							Dgv_Detail2.Rows(i).Cells(42).Style.BackColor = Color.FromArgb(244, 208, 111)
							Dgv_Detail2.Rows(i).Cells(43).Style.BackColor = Color.FromArgb(188, 158, 130)

							Dgv_Detail2.Rows(i).Cells(20).Style.Font = New Font(Dgv_Detail2.Font, FontStyle.Bold)
							Dgv_Detail2.Rows(i).Cells(29).Style.Font = New Font(Dgv_Detail2.Font, FontStyle.Bold)
							Dgv_Detail2.Rows(i).Cells(38).Style.Font = New Font(Dgv_Detail2.Font, FontStyle.Bold)

						Next
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try
	End Sub

	Private Sub LoadDataDetailSplit(Optional ByVal isFilter As Boolean = False)

		Dim DigitDecimal As String = ""

		Select Case arrJenis(Cmb_Jenis.SelectedIndex)
			Case "Y"
				DigitDecimal = "N0"
			Case "T"
				DigitDecimal = "N4"
			Case Else
				DigitDecimal = "N4"
		End Select

		Try
			OpenConn()

			Dgv_Detail2_Split.Rows.Clear()
			Dim Filter_Tanggal_Awal As String = $"'{Format(Tgl1.Value, "yyyy-MM-dd")}'"
			Dim Filter_Tanggal_Akhir As String = $"'{Format(Tgl2.Value.AddDays(1), "yyyy-MM-dd")}'"
			Dim Filter_NoSplit As String = "DEFAULT"
			Dim Filter_ID_Routing As String = "DEFAULT"
			Dim Filter_KodeBarang As String = "DEFAULT"
			Dim Filter_GroupJenis As String = "DEFAULT"

			If isFilter Then

				If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
					Filter_NoSplit = $"'{Txt_NoSplit.Text.Trim}'"
				End If

				If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
					Filter_ID_Routing = $"'{Txt_IdRouting.Text.Trim}'"
				End If

				If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
					Filter_KodeBarang = $"'{Txt_KdBarang.Text.Trim}'"
				End If

				If Not Cmb_Jenis.SelectedIndex = 0 Then
					Filter_GroupJenis = $"'{arrJenis(Cmb_Jenis.SelectedIndex).ToString.Trim}'"
				End If

			End If

			SQL = $"
				select No_PO, no_split, Kode_Barang, Nama, Tgl_Produksi, Jam_Produksi, Tanggal_GI, Jam_GI, Nama_Routing, Keterangan, Jumlah, Berat_GI, Jumlah_Dosing,
					   WasteGR1_KG, WasteGR1_Persen, Reject_GR1_KG, ScrapGR1_KG, Loss_Production, Loss_Production_Persen,
					   NilaiGR1_KG, NilaiGR1_Pcs, GR1_StockSementara, WaktuGR1,
					   WasteGR2_Pcs, WasteGR2_Persen, Persentase_Waste_Terhadap_GR2, Reject_GR2_PCS, Reject_GR2_KG, ScrapGR2_Pcs, ScrapGR2_KG, NilaiGR2_Pcs, GR2_StockSementara, GR2_Stock_Belum_Validasi, WaktuGR2,
					   WasteGR3_Pcs, WasteGR3_Persen, Reject_GR3_PCS, Reject_GR3_KG, ScrapGR3_Pcs, ScrapGR3_KG, NilaiGRFinal_Pcs, GR3_StockSementara, WaktuGR3,
					   Jumlah_Scrap_Packaging, Flag_Preservative
				FROM N_EMI_Function_Laporan_Akhir_GIGR2_By_Split('{KodePerusahaan}', {Filter_NoSplit}, {Filter_Tanggal_Awal}, {Filter_Tanggal_Akhir},
				{Filter_ID_Routing}, {Filter_KodeBarang}, {Filter_GroupJenis})
				order by Tgl_Produksi, Jam_Produksi
			"

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dgv_Detail2_Split.Rows.Add(1)
							Dgv_Detail2_Split.Rows(i).Cells(0).Value = .Rows(i).Item("No_po")
							Dgv_Detail2_Split.Rows(i).Cells(1).Value = .Rows(i).Item("no_split")
							Dgv_Detail2_Split.Rows(i).Cells(2).Value = .Rows(i).Item("Kode_Barang")
							Dgv_Detail2_Split.Rows(i).Cells(3).Value = .Rows(i).Item("Nama")
							Dgv_Detail2_Split.Rows(i).Cells(4).Value = Format(.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy")
							Dgv_Detail2_Split.Rows(i).Cells(5).Value = .Rows(i).Item("Jam_Produksi")
							Dgv_Detail2_Split.Rows(i).Cells(6).Value = Format(.Rows(i).Item("Tanggal_GI"), "dd MMM yyyy")
							Dgv_Detail2_Split.Rows(i).Cells(7).Value = .Rows(i).Item("Jam_GI")
							Dgv_Detail2_Split.Rows(i).Cells(8).Value = .Rows(i).Item("Nama_Routing")
							Dgv_Detail2_Split.Rows(i).Cells(9).Value = .Rows(i).Item("Keterangan")
							Dgv_Detail2_Split.Rows(i).Cells(10).Value = If(General_Class.CekNULL(.Rows(i).Item("Jumlah")) = "", 0, Format(.Rows(i).Item("Jumlah"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(11).Value = If(General_Class.CekNULL(.Rows(i).Item("Berat_GI")) = "", 0, Format(.Rows(i).Item("Berat_GI"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(12).Value = If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Dosing")) = "", 0, Format(.Rows(i).Item("Jumlah_Dosing"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(13).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR1_KG")) = "", 0, Format(.Rows(i).Item("WasteGR1_KG"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(14).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR1_Persen")) = "", 0, Format(.Rows(i).Item("WasteGR1_Persen"), "N4"))

							Dgv_Detail2_Split.Rows(i).Cells(15).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_GR1_KG")) = "", 0, Format(.Rows(i).Item("Reject_GR1_KG"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(16).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR1_KG")) = "", 0, Format(.Rows(i).Item("ScrapGR1_KG"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(17).Value = If(General_Class.CekNULL(.Rows(i).Item("Loss_Production")) = "", 0, Format(.Rows(i).Item("Loss_Production"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(18).Value = If(General_Class.CekNULL(.Rows(i).Item("Loss_Production_Persen")) = "", 0, Format(.Rows(i).Item("Loss_Production_Persen"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(19).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGR1_KG")) = "", 0, Format(.Rows(i).Item("NilaiGR1_KG"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(20).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGR1_Pcs")) = "", 0, Format(.Rows(i).Item("NilaiGR1_Pcs"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(21).Value = If(General_Class.CekNULL(.Rows(i).Item("GR1_StockSementara")) = "", 0, Format(.Rows(i).Item("GR1_StockSementara"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(22).Value = .Rows(i).Item("WaktuGR1")

							Dgv_Detail2_Split.Rows(i).Cells(23).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR2_Pcs")) = "", 0, Format(.Rows(i).Item("WasteGR2_Pcs"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(24).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR2_Persen")) = "", 0, Format(.Rows(i).Item("WasteGR2_Persen"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(25).Value = If(General_Class.CekNULL(.Rows(i).Item("Persentase_Waste_Terhadap_GR2")) = "", 0, Format(.Rows(i).Item("Persentase_Waste_Terhadap_GR2"), DigitDecimal))

							'REJECT
							Dgv_Detail2_Split.Rows(i).Cells(26).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_GR2_PCS")) = "", 0, Format(.Rows(i).Item("Reject_GR2_PCS"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(27).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_GR2_KG")) = "", 0, Format(.Rows(i).Item("Reject_GR2_KG"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(28).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR2_Pcs")) = "", 0, Format(.Rows(i).Item("ScrapGR2_Pcs"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(29).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR2_KG")) = "", 0, Format(.Rows(i).Item("ScrapGR2_KG"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(30).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGR2_Pcs")) = "", 0, Format(.Rows(i).Item("NilaiGR2_Pcs"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(31).Value = If(General_Class.CekNULL(.Rows(i).Item("GR2_StockSementara")) = "", 0, Format(.Rows(i).Item("GR2_StockSementara"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(32).Value = If(General_Class.CekNULL(.Rows(i).Item("GR2_Stock_Belum_Validasi")) = "", 0, Format(.Rows(i).Item("GR2_Stock_Belum_Validasi"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(33).Value = .Rows(i).Item("WaktuGR2")

							'REJECT
							Dgv_Detail2_Split.Rows(i).Cells(34).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR3_Pcs")) = "", 0, Format(.Rows(i).Item("WasteGR3_Pcs"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(35).Value = If(General_Class.CekNULL(.Rows(i).Item("WasteGR3_Persen")) = "", 0, Format(.Rows(i).Item("WasteGR3_Persen"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(36).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_GR3_PCS")) = "", 0, Format(.Rows(i).Item("Reject_GR3_PCS"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(37).Value = If(General_Class.CekNULL(.Rows(i).Item("Reject_GR3_KG")) = "", 0, Format(.Rows(i).Item("Reject_GR3_KG"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(38).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR3_Pcs")) = "", 0, Format(.Rows(i).Item("ScrapGR3_Pcs"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(39).Value = If(General_Class.CekNULL(.Rows(i).Item("ScrapGR3_KG")) = "", 0, Format(.Rows(i).Item("ScrapGR3_KG"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(40).Value = If(General_Class.CekNULL(.Rows(i).Item("NilaiGRFinal_Pcs")) = "", 0, Format(.Rows(i).Item("NilaiGRFinal_Pcs"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(41).Value = If(General_Class.CekNULL(.Rows(i).Item("GR3_StockSementara")) = "", 0, Format(.Rows(i).Item("GR3_StockSementara"), DigitDecimal))
							Dgv_Detail2_Split.Rows(i).Cells(42).Value = .Rows(i).Item("WaktuGR3")
							Dgv_Detail2_Split.Rows(i).Cells(43).Value = If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Scrap_Packaging")) = "", 0, Format(.Rows(i).Item("Jumlah_Scrap_Packaging"), "N4"))
							Dgv_Detail2_Split.Rows(i).Cells(44).Value = If(General_Class.CekNULL(.Rows(i).Item("Flag_Preservative")) = "", "", .Rows(i).Item("Flag_Preservative"))

							''== WARNA =='

							Dgv_Detail2_Split.Rows(i).Cells(13).Style.BackColor = Color.LightYellow
							Dgv_Detail2_Split.Rows(i).Cells(14).Style.BackColor = Color.LightYellow ' REJECT
							Dgv_Detail2_Split.Rows(i).Cells(15).Style.BackColor = Color.LightYellow
							Dgv_Detail2_Split.Rows(i).Cells(16).Style.BackColor = Color.LightYellow
							Dgv_Detail2_Split.Rows(i).Cells(17).Style.BackColor = Color.LightYellow
							Dgv_Detail2_Split.Rows(i).Cells(18).Style.BackColor = Color.LightYellow
							Dgv_Detail2_Split.Rows(i).Cells(19).Style.BackColor = Color.LightYellow
							Dgv_Detail2_Split.Rows(i).Cells(20).Style.BackColor = Color.LightYellow
							Dgv_Detail2_Split.Rows(i).Cells(21).Style.BackColor = Color.LightYellow
							Dgv_Detail2_Split.Rows(i).Cells(22).Style.BackColor = Color.FromArgb(252, 105, 108)

							Dgv_Detail2_Split.Rows(i).Cells(23).Style.BackColor = Color.LightBlue
							Dgv_Detail2_Split.Rows(i).Cells(24).Style.BackColor = Color.LightBlue
							Dgv_Detail2_Split.Rows(i).Cells(25).Style.BackColor = Color.LightBlue
							Dgv_Detail2_Split.Rows(i).Cells(26).Style.BackColor = Color.LightBlue
							Dgv_Detail2_Split.Rows(i).Cells(27).Style.BackColor = Color.LightBlue
							Dgv_Detail2_Split.Rows(i).Cells(28).Style.BackColor = Color.LightBlue
							Dgv_Detail2_Split.Rows(i).Cells(29).Style.BackColor = Color.LightBlue
							Dgv_Detail2_Split.Rows(i).Cells(30).Style.BackColor = Color.LightBlue
							Dgv_Detail2_Split.Rows(i).Cells(31).Style.BackColor = Color.LightBlue
							Dgv_Detail2_Split.Rows(i).Cells(32).Style.BackColor = Color.LightBlue
							Dgv_Detail2_Split.Rows(i).Cells(33).Style.BackColor = Color.FromArgb(181, 230, 162)

							Dgv_Detail2_Split.Rows(i).Cells(34).Style.BackColor = Color.LightGray
							Dgv_Detail2_Split.Rows(i).Cells(35).Style.BackColor = Color.LightGray
							Dgv_Detail2_Split.Rows(i).Cells(36).Style.BackColor = Color.LightGray
							Dgv_Detail2_Split.Rows(i).Cells(37).Style.BackColor = Color.LightGray
							Dgv_Detail2_Split.Rows(i).Cells(38).Style.BackColor = Color.LightGray
							Dgv_Detail2_Split.Rows(i).Cells(39).Style.BackColor = Color.LightGray
							Dgv_Detail2_Split.Rows(i).Cells(40).Style.BackColor = Color.LightGray
							Dgv_Detail2_Split.Rows(i).Cells(41).Style.BackColor = Color.LightGray
							Dgv_Detail2_Split.Rows(i).Cells(42).Style.BackColor = Color.White

							Dgv_Detail2_Split.Rows(i).Cells(43).Style.BackColor = Color.FromArgb(244, 208, 111)
							Dgv_Detail2_Split.Rows(i).Cells(44).Style.BackColor = Color.FromArgb(188, 158, 130)

							Dgv_Detail2_Split.Rows(i).Cells(22).Style.Font = New Font(Dgv_Detail2_Split.Font, FontStyle.Bold)
							Dgv_Detail2_Split.Rows(i).Cells(32).Style.Font = New Font(Dgv_Detail2_Split.Font, FontStyle.Bold)
							Dgv_Detail2_Split.Rows(i).Cells(42).Style.Font = New Font(Dgv_Detail2_Split.Font, FontStyle.Bold)

						Next
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

#Region "FUNGSI LOAD DENGAN THREAD"

	'Private Sub LoadRekap(token As CancellationToken, Optional ByVal isFilter As Boolean = False)
	'	Dim DigitDecimal As String = ""
	'	Dim tgl1Value, tgl2Value As Date
	'	Dim idRoutingText As String = ""
	'	Dim kdBarangText As String = ""
	'	Dim noSplitText As String = ""
	'	Dim cmbIndex As Integer = 0

	'	' --- 1. AMBIL NILAI UI DI MAIN THREAD ---
	'	Me.Invoke(Sub()
	'				  Select Case arrJenis(Cmb_Jenis.SelectedIndex)
	'					  Case "Y" : DigitDecimal = "N0"
	'					  Case "T" : DigitDecimal = "N4"
	'					  Case Else : DigitDecimal = "N4"
	'				  End Select
	'				  tgl1Value = Tgl1.Value
	'				  tgl2Value = Tgl2.Value
	'				  idRoutingText = Txt_IdRouting.Text
	'				  kdBarangText = Txt_KdBarang.Text
	'				  noSplitText = Txt_NoSplit.Text
	'				  cmbIndex = Cmb_Jenis.SelectedIndex
	'			  End Sub)

	'	Try
	'		OpenConn()
	'		If token.IsCancellationRequested Then token.ThrowIfCancellationRequested()

	'		' --- 2. LOGIC PEMBENTUKAN SQL ---
	'		SQL = "select no_po, No_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Berat_GI, Jumlah_Dosing, "
	'		SQL = SQL & "Pro_Reject_KG, Qc_Reject_KG, Warehouse_Reject_KG, Tot_Reject_KG, "
	'		SQL = SQL & "ScrapGR1_KG, ScrapGR2_KG, ScrapGR3_KG, ScrapTotal_KG, "
	'		SQL = SQL & "WasteGR1_KG, WasteGR2_KG, WasteGR3_KG, WasteTotal_KG, Loss_Production_Final_GR, "
	'		SQL = SQL & "NilaiGRFinal_KG, NilaiGRFinal_Pcs, Tanggal_GI, Jam_GI, "
	'		SQL = SQL & "Reject_Final_Persen, ScrapTotal_Persen, WasteTotal_Persen, Loss_Production_Final_GR_Persen, NilaiGRFinal_Persen, GR_Inspection_KG, "
	'		SQL = SQL & "GR_Inspection_PCS, GR_Inspection_Persen, Status, Jumlah_Scrap_Packaging, Flag_Preservative "
	'		SQL = SQL & "from Laporan_Akhir_GIGR_Rekap2 "
	'		SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
	'		SQL = SQL & "and Tgl_Produksi between '" & Format(tgl1Value, "yyyy-MM-dd") & "' and '" & Format(tgl2Value.AddDays(1), "yyyy-MM-dd") & "' "

	'		If isFilter Then
	'			If Not idRoutingText.ToUpper = OpsiSeluruh.ToUpper Then
	'				SQL = SQL & "and Id_Routing = '" & idRoutingText & "' "
	'			End If
	'			If Not kdBarangText.ToUpper = OpsiSeluruh.ToUpper Then
	'				SQL = SQL & "and Kode_Barang = '" & kdBarangText & "' "
	'			End If
	'			If noSplitText.Trim.Length > 0 AndAlso noSplitText.ToUpper <> OpsiSeluruh.ToUpper Then
	'				SQL = SQL & "and no_split like '%" & noSplitText & "%' "
	'			End If
	'			If Not cmbIndex = 0 Then
	'				SQL = SQL & "and Group_Jenis = '" & arrJenis(cmbIndex) & "' "
	'			End If
	'		End If
	'		SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "

	'		' --- 3. GET DATA DARI DATABASE ---
	'		Using Ds = BindingTrans_Thread(SQL)
	'			Dim dt As DataTable = Ds.Tables("MyTable")
	'			If token.IsCancellationRequested Then token.ThrowIfCancellationRequested()

	'			' --- 4. RENDER DATA KE UI DATAGRIDVIEW ---
	'			Me.Invoke(Sub()
	'						  Dgv_Rekap.Rows.Clear()
	'						  If dt.Rows.Count <> 0 Then
	'							  For i As Integer = 0 To dt.Rows.Count - 1
	'								  Dgv_Rekap.Rows.Add(1)
	'								  Dgv_Rekap.Rows(i).Cells(0).Value = dt.Rows(i).Item("no_po")
	'								  Dgv_Rekap.Rows(i).Cells(1).Value = dt.Rows(i).Item("No_split")
	'								  Dgv_Rekap.Rows(i).Cells(2).Value = dt.Rows(i).Item("Kode_Barang")
	'								  Dgv_Rekap.Rows(i).Cells(3).Value = dt.Rows(i).Item("Nama")
	'								  Dgv_Rekap.Rows(i).Cells(4).Value = Format(dt.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy")
	'								  Dgv_Rekap.Rows(i).Cells(5).Value = dt.Rows(i).Item("Jam_Produksi")
	'								  Dgv_Rekap.Rows(i).Cells(6).Value = dt.Rows(i).Item("Nama_Routing")
	'								  Dgv_Rekap.Rows(i).Cells(7).Value = dt.Rows(i).Item("Keterangan")
	'								  Dgv_Rekap.Rows(i).Cells(8).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Jumlah")) = "", 0, Format(dt.Rows(i).Item("Jumlah"), DigitDecimal))
	'								  Dgv_Rekap.Rows(i).Cells(9).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Berat_GI")) = "", 0, Format(dt.Rows(i).Item("Berat_GI"), "N0"))
	'								  Dgv_Rekap.Rows(i).Cells(10).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Jumlah_Dosing")) = "", 0, Format(dt.Rows(i).Item("Jumlah_Dosing"), "N4"))

	'								  Dgv_Rekap.Rows(i).Cells(11).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Pro_Reject_KG")) = "", 0, Format(dt.Rows(i).Item("Pro_Reject_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(12).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Qc_Reject_KG")) = "", 0, Format(dt.Rows(i).Item("Qc_Reject_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(13).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Warehouse_Reject_KG")) = "", 0, Format(dt.Rows(i).Item("Warehouse_Reject_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(14).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Tot_Reject_KG")) = "", 0, Format(dt.Rows(i).Item("Tot_Reject_KG"), "N4"))

	'								  Dgv_Rekap.Rows(i).Cells(15).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR1_KG")) = "", 0, Format(dt.Rows(i).Item("ScrapGR1_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(16).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR2_KG")) = "", 0, Format(dt.Rows(i).Item("ScrapGR2_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(17).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR3_KG")) = "", 0, Format(dt.Rows(i).Item("ScrapGR3_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(18).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapTotal_KG")) = "", 0, Format(dt.Rows(i).Item("ScrapTotal_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(19).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR1_KG")) = "", 0, Format(dt.Rows(i).Item("WasteGR1_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(20).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR2_KG")) = "", 0, Format(dt.Rows(i).Item("WasteGR2_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(21).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR3_KG")) = "", 0, Format(dt.Rows(i).Item("WasteGR3_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(22).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteTotal_KG")) = "", 0, Format(dt.Rows(i).Item("WasteTotal_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(23).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Loss_Production_Final_GR")) = "", 0, Format(dt.Rows(i).Item("Loss_Production_Final_GR"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(24).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR_Inspection_KG")) = "", 0, Format(dt.Rows(i).Item("GR_Inspection_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(25).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR_Inspection_PCS")) = "", 0, Format(dt.Rows(i).Item("GR_Inspection_PCS"), DigitDecimal))
	'								  Dgv_Rekap.Rows(i).Cells(26).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGRFinal_KG")) = "", 0, Format(dt.Rows(i).Item("NilaiGRFinal_KG"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(27).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGRFinal_Pcs")) = "", 0, Format(dt.Rows(i).Item("NilaiGRFinal_Pcs"), DigitDecimal))

	'								  Dgv_Rekap.Rows(i).Cells(28).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_Final_Persen")) = "", 0, Format(dt.Rows(i).Item("Reject_Final_Persen"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(29).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapTotal_Persen")) = "", 0, Format(dt.Rows(i).Item("ScrapTotal_Persen"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(30).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteTotal_Persen")) = "", 0, Format(dt.Rows(i).Item("WasteTotal_Persen"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(31).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Loss_Production_Final_GR_Persen")) = "", 0, Format(dt.Rows(i).Item("Loss_Production_Final_GR_Persen"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(32).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR_Inspection_Persen")) = "", 0, Format(dt.Rows(i).Item("GR_Inspection_Persen"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(33).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGRFinal_Persen")) = "", 0, Format(dt.Rows(i).Item("NilaiGRFinal_Persen"), "N4"))

	'								  Dgv_Rekap.Rows(i).Cells(34).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Tanggal_GI")) = "", "-", Format(dt.Rows(i).Item("Tanggal_GI"), "dd MMM yyyy"))
	'								  Dgv_Rekap.Rows(i).Cells(35).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Jam_GI")) = "", "-", dt.Rows(i).Item("Jam_GI"))

	'								  Dgv_Rekap.Rows(i).Cells(36).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Jumlah_Scrap_Packaging")) = "", "-", Format(dt.Rows(i).Item("Jumlah_Scrap_Packaging"), "N4"))
	'								  Dgv_Rekap.Rows(i).Cells(37).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Flag_Preservative")) = "", "-", dt.Rows(i).Item("Flag_Preservative"))
	'								  Dgv_Rekap.Rows(i).Cells(38).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Status")) = "", "-", dt.Rows(i).Item("Status"))

	'								  If Not General_Class.CekNULL(dt.Rows(i).Item("Status")) = "" Then
	'									  If dt.Rows(i).Item("Status") = "PRODUCTION" Then
	'										  Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.LightYellow
	'									  ElseIf dt.Rows(i).Item("Status") = "INSPECTION" Then
	'										  Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.LightBlue
	'									  ElseIf dt.Rows(i).Item("Status") = "WAREHOUSE" Then
	'										  Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.LightGray
	'									  Else
	'										  Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.White
	'									  End If
	'								  Else
	'									  Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.White
	'								  End If

	'								  Dgv_Rekap.Rows(i).Cells(11).Style.BackColor = Color.FromArgb(255, 206, 117)
	'								  Dgv_Rekap.Rows(i).Cells(12).Style.BackColor = Color.FromArgb(255, 206, 117)
	'								  Dgv_Rekap.Rows(i).Cells(13).Style.BackColor = Color.FromArgb(255, 206, 117)
	'								  Dgv_Rekap.Rows(i).Cells(14).Style.BackColor = Color.FromArgb(255, 206, 117)
	'								  Dgv_Rekap.Rows(i).Cells(15).Style.BackColor = Color.LightYellow
	'								  Dgv_Rekap.Rows(i).Cells(16).Style.BackColor = Color.LightYellow
	'								  Dgv_Rekap.Rows(i).Cells(17).Style.BackColor = Color.LightYellow
	'								  Dgv_Rekap.Rows(i).Cells(18).Style.BackColor = Color.LightYellow
	'								  Dgv_Rekap.Rows(i).Cells(19).Style.BackColor = Color.LightBlue
	'								  Dgv_Rekap.Rows(i).Cells(20).Style.BackColor = Color.LightBlue
	'								  Dgv_Rekap.Rows(i).Cells(21).Style.BackColor = Color.LightBlue
	'								  Dgv_Rekap.Rows(i).Cells(22).Style.BackColor = Color.LightBlue
	'								  Dgv_Rekap.Rows(i).Cells(23).Style.BackColor = Color.LightGreen
	'								  Dgv_Rekap.Rows(i).Cells(24).Style.BackColor = Color.LightCoral
	'								  Dgv_Rekap.Rows(i).Cells(25).Style.BackColor = Color.LightCoral
	'								  Dgv_Rekap.Rows(i).Cells(26).Style.BackColor = Color.LightGray
	'								  Dgv_Rekap.Rows(i).Cells(27).Style.BackColor = Color.LightGray
	'								  Dgv_Rekap.Rows(i).Cells(28).Style.BackColor = Color.LightCyan
	'								  Dgv_Rekap.Rows(i).Cells(29).Style.BackColor = Color.LightCyan
	'								  Dgv_Rekap.Rows(i).Cells(30).Style.BackColor = Color.LightCyan
	'								  Dgv_Rekap.Rows(i).Cells(31).Style.BackColor = Color.LightCyan
	'								  Dgv_Rekap.Rows(i).Cells(32).Style.BackColor = Color.LightCyan
	'								  Dgv_Rekap.Rows(i).Cells(33).Style.BackColor = Color.LightCyan
	'								  Dgv_Rekap.Rows(i).Cells(36).Style.BackColor = Color.FromArgb(244, 208, 111)
	'								  Dgv_Rekap.Rows(i).Cells(37).Style.BackColor = Color.FromArgb(188, 158, 130)
	'								  Dgv_Rekap.Rows(i).Cells(32).Style.Font = New Font(Dgv_Rekap.Font, FontStyle.Bold)
	'							  Next
	'						  End If
	'					  End Sub)
	'		End Using

	'		token.ThrowIfCancellationRequested()
	'		CloseConn()
	'	Catch ex As OperationCanceledException
	'		CloseConn()
	'		Exit Sub
	'	Catch ex As Exception
	'		CloseConn()
	'		If cts IsNot Nothing AndAlso Not cts.IsCancellationRequested Then
	'			cts.Cancel()
	'			MessageBox.Show("Load Rekap Error: " & ex.Message, "Error Terjadi", MessageBoxButtons.OK, MessageBoxIcon.Error)
	'		End If
	'		Exit Sub
	'	End Try
	'End Sub

	'Private Sub LoadDataDetail(token As CancellationToken, Optional ByVal isFilter As Boolean = False)
	'	Dim DigitDecimal As String = ""
	'	Dim tgl1Value, tgl2Value As Date
	'	Dim idRoutingText As String = ""
	'	Dim kdBarangText As String = ""
	'	Dim noSplitText As String = ""
	'	Dim cmbIndex As Integer = 0

	'	Me.Invoke(Sub()
	'				  Select Case arrJenis(Cmb_Jenis.SelectedIndex)
	'					  Case "Y" : DigitDecimal = "N0"
	'					  Case "T" : DigitDecimal = "N4"
	'					  Case Else : DigitDecimal = "N4"
	'				  End Select
	'				  tgl1Value = Tgl1.Value
	'				  tgl2Value = Tgl2.Value
	'				  idRoutingText = Txt_IdRouting.Text
	'				  kdBarangText = Txt_KdBarang.Text
	'				  noSplitText = Txt_NoSplit.Text
	'				  cmbIndex = Cmb_Jenis.SelectedIndex
	'			  End Sub)

	'	Try
	'		OpenConn()
	'		If token.IsCancellationRequested Then token.ThrowIfCancellationRequested()

	'		Dim Filter_Tanggal_Awal As String = $"'{Format(tgl1Value, "yyyy-MM-dd")}'"
	'		Dim Filter_Tanggal_Akhir As String = $"'{Format(tgl2Value.AddDays(1), "yyyy-MM-dd")}'"
	'		Dim Filter_NoSplit As String = "DEFAULT"
	'		Dim Filter_ID_Routing As String = "DEFAULT"
	'		Dim Filter_KodeBarang As String = "DEFAULT"
	'		Dim Filter_GroupJenis As String = "DEFAULT"

	'		If isFilter Then
	'			If noSplitText.Trim.Length > 0 AndAlso noSplitText.ToUpper <> OpsiSeluruh.ToUpper Then
	'				Filter_NoSplit = $"'{noSplitText.Trim}'"
	'			End If
	'			If Not idRoutingText.ToUpper = OpsiSeluruh.ToUpper Then
	'				Filter_ID_Routing = $"'{idRoutingText.Trim}'"
	'			End If
	'			If Not kdBarangText.ToUpper = OpsiSeluruh.ToUpper Then
	'				Filter_KodeBarang = $"'{kdBarangText.Trim}'"
	'			End If
	'			If Not cmbIndex = 0 Then
	'				Filter_GroupJenis = $"'{arrJenis(cmbIndex).ToString.Trim}'"
	'			End If
	'		End If

	'		SQL = $"
	'           SELECT No_po, no_split, Tgl_Produksi, Jam_Produksi, Tanggal_GI, Jam_GI, Nama_Routing, Keterangan, Kode_Barang, Nama,
	'                  Jumlah, batch, Berat_GI, Jumlah_Dosing,
	'                  WasteGR1_KG, WasteGR1_Persen, Reject_GR1_KG, ScrapGR1_KG, Loss_Production, Loss_Production_Persen, NilaiGR1_KG,
	'                  NilaiGR1_Pcs, GR1_StockSementara, WaktuGR1, Adjustment,
	'                  WasteGR2_Pcs, WasteGR2_Persen, Persentase_Waste_Terhadap_GR2, Reject_GR2_PCS, Reject_GR2_KG, ScrapGR2_PCS,
	'                  ScrapGR2_KG, NilaiGR2_Pcs, GR2_StockSementara, GR2_Stock_Belum_Validasi, WaktuGR2,
	'                  WasteGR3_Pcs, WasteGR3_Persen, Reject_GR3_PCS, Reject_GR3_KG, ScrapGR3_PCS, ScrapGR3_KG, NilaiGRFinal_Pcs,
	'                  GR3_StockSementara, WaktuGR3,
	'                  Jumlah_Scrap_Packaging, Flag_Preservative
	'           FROM N_EMI_Function_Laporan_Akhir_GIGR2('{KodePerusahaan}', {Filter_NoSplit}, {Filter_Tanggal_Awal}, {Filter_Tanggal_Akhir}, {Filter_ID_Routing}, {Filter_KodeBarang}, {Filter_GroupJenis})
	'           order by Tgl_Produksi, Jam_Produksi
	'       "

	'		Using Ds = BindingTrans_Thread(SQL)
	'			Dim dt As DataTable = Ds.Tables("MyTable")
	'			If token.IsCancellationRequested Then token.ThrowIfCancellationRequested()

	'			Me.Invoke(Sub()
	'						  Dgv_Detail2.Rows.Clear()
	'						  If dt.Rows.Count <> 0 Then
	'							  For i As Integer = 0 To dt.Rows.Count - 1
	'								  Dgv_Detail2.Rows.Add(1)
	'								  Dgv_Detail2.Rows(i).Cells(0).Value = dt.Rows(i).Item("No_po")
	'								  Dgv_Detail2.Rows(i).Cells(1).Value = dt.Rows(i).Item("no_split")
	'								  Dgv_Detail2.Rows(i).Cells(2).Value = dt.Rows(i).Item("Kode_Barang")
	'								  Dgv_Detail2.Rows(i).Cells(3).Value = dt.Rows(i).Item("Nama")
	'								  Dgv_Detail2.Rows(i).Cells(4).Value = Format(dt.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy")
	'								  Dgv_Detail2.Rows(i).Cells(5).Value = dt.Rows(i).Item("Jam_Produksi")
	'								  Dgv_Detail2.Rows(i).Cells(6).Value = dt.Rows(i).Item("Nama_Routing")
	'								  Dgv_Detail2.Rows(i).Cells(7).Value = dt.Rows(i).Item("Keterangan")
	'								  Dgv_Detail2.Rows(i).Cells(8).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Jumlah")) = "", 0, Format(dt.Rows(i).Item("Jumlah"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(9).Value = dt.Rows(i).Item("batch")
	'								  Dgv_Detail2.Rows(i).Cells(10).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Berat_GI")) = "", 0, Format(dt.Rows(i).Item("Berat_GI"), "N0"))
	'								  Dgv_Detail2.Rows(i).Cells(11).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Jumlah_Dosing")) = "", 0, Format(dt.Rows(i).Item("Jumlah_Dosing"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(12).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR1_KG")) = "", 0, Format(dt.Rows(i).Item("WasteGR1_KG"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(13).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR1_Persen")) = "", 0, Format(dt.Rows(i).Item("WasteGR1_Persen"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(14).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_GR1_KG")) = "", 0, Format(dt.Rows(i).Item("Reject_GR1_KG"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(15).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR1_KG")) = "", 0, Format(dt.Rows(i).Item("ScrapGR1_KG"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(16).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Loss_Production")) = "", 0, Format(dt.Rows(i).Item("Loss_Production"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(17).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Loss_Production_Persen")) = "", 0, Format(dt.Rows(i).Item("Loss_Production_Persen"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(18).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGR1_KG")) = "", 0, Format(dt.Rows(i).Item("NilaiGR1_KG"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(19).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGR1_Pcs")) = "", 0, Format(dt.Rows(i).Item("NilaiGR1_Pcs"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(20).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR1_StockSementara")) = "", 0, Format(dt.Rows(i).Item("GR1_StockSementara"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(21).Value = dt.Rows(i).Item("WaktuGR1")
	'								  Dgv_Detail2.Rows(i).Cells(22).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR2_Pcs")) = "", 0, Format(dt.Rows(i).Item("WasteGR2_Pcs"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(23).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR2_Persen")) = "", 0, Format(dt.Rows(i).Item("WasteGR2_Persen"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(44).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Persentase_Waste_Terhadap_GR2")) = "", 0, Format(dt.Rows(i).Item("Persentase_Waste_Terhadap_GR2"), "N4"))

	'								  Dgv_Detail2.Rows(i).Cells(24).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_GR2_PCS")) = "", 0, Format(dt.Rows(i).Item("Reject_GR2_PCS"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(25).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_GR2_KG")) = "", 0, Format(dt.Rows(i).Item("Reject_GR2_KG"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(26).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR2_PCS")) = "", 0, Format(dt.Rows(i).Item("ScrapGR2_PCS"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(27).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR2_KG")) = "", 0, Format(dt.Rows(i).Item("ScrapGR2_KG"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(28).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGR2_Pcs")) = "", 0, Format(dt.Rows(i).Item("NilaiGR2_Pcs"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(29).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR2_StockSementara")) = "", 0, Format(dt.Rows(i).Item("GR2_StockSementara"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(30).Value = dt.Rows(i).Item("WaktuGR2")
	'								  Dgv_Detail2.Rows(i).Cells(31).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR3_Pcs")) = "", 0, Format(dt.Rows(i).Item("WasteGR3_Pcs"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(32).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR3_Persen")) = "", 0, Format(dt.Rows(i).Item("WasteGR3_Persen"), "N4"))

	'								  Dgv_Detail2.Rows(i).Cells(33).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_GR3_PCS")) = "", 0, Format(dt.Rows(i).Item("Reject_GR3_PCS"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(34).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_GR3_KG")) = "", 0, Format(dt.Rows(i).Item("Reject_GR3_KG"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(35).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR3_PCS")) = "", 0, Format(dt.Rows(i).Item("ScrapGR3_PCS"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(36).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR3_KG")) = "", 0, Format(dt.Rows(i).Item("ScrapGR3_KG"), "N4"))
	'								  Dgv_Detail2.Rows(i).Cells(37).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGRFinal_Pcs")) = "", 0, Format(dt.Rows(i).Item("NilaiGRFinal_Pcs"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(38).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR3_StockSementara")) = "", 0, Format(dt.Rows(i).Item("GR3_StockSementara"), DigitDecimal))
	'								  Dgv_Detail2.Rows(i).Cells(39).Value = dt.Rows(i).Item("WaktuGR3")
	'								  Dgv_Detail2.Rows(i).Cells(40).Value = Format(dt.Rows(i).Item("Tanggal_GI"), "dd MMM yyyy")
	'								  Dgv_Detail2.Rows(i).Cells(41).Value = dt.Rows(i).Item("Jam_GI")
	'								  Dgv_Detail2.Rows(i).Cells(42).Value = Format(dt.Rows(i).Item("Jumlah_Scrap_Packaging"), "N4")
	'								  Dgv_Detail2.Rows(i).Cells(43).Value = dt.Rows(i).Item("Flag_Preservative")
	'								  Dgv_Detail2.Rows(i).Cells(45).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR2_Stock_Belum_Validasi")) = "", 0, Format(dt.Rows(i).Item("GR2_Stock_Belum_Validasi"), DigitDecimal))

	'								  Dgv_Detail2.Rows(i).Cells(12).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2.Rows(i).Cells(13).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2.Rows(i).Cells(14).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2.Rows(i).Cells(15).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2.Rows(i).Cells(16).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2.Rows(i).Cells(17).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2.Rows(i).Cells(18).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2.Rows(i).Cells(19).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2.Rows(i).Cells(20).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2.Rows(i).Cells(21).Style.BackColor = Color.FromArgb(252, 105, 108)
	'								  Dgv_Detail2.Rows(i).Cells(22).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2.Rows(i).Cells(23).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2.Rows(i).Cells(24).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2.Rows(i).Cells(25).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2.Rows(i).Cells(26).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2.Rows(i).Cells(27).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2.Rows(i).Cells(28).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2.Rows(i).Cells(29).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2.Rows(i).Cells(44).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2.Rows(i).Cells(45).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2.Rows(i).Cells(30).Style.BackColor = Color.FromArgb(181, 230, 162)
	'								  Dgv_Detail2.Rows(i).Cells(31).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2.Rows(i).Cells(32).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2.Rows(i).Cells(33).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2.Rows(i).Cells(34).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2.Rows(i).Cells(35).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2.Rows(i).Cells(36).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2.Rows(i).Cells(37).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2.Rows(i).Cells(38).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2.Rows(i).Cells(39).Style.BackColor = Color.White
	'								  Dgv_Detail2.Rows(i).Cells(42).Style.BackColor = Color.FromArgb(244, 208, 111)
	'								  Dgv_Detail2.Rows(i).Cells(43).Style.BackColor = Color.FromArgb(188, 158, 130)
	'								  Dgv_Detail2.Rows(i).Cells(20).Style.Font = New Font(Dgv_Detail2.Font, FontStyle.Bold)
	'								  Dgv_Detail2.Rows(i).Cells(29).Style.Font = New Font(Dgv_Detail2.Font, FontStyle.Bold)
	'								  Dgv_Detail2.Rows(i).Cells(38).Style.Font = New Font(Dgv_Detail2.Font, FontStyle.Bold)
	'							  Next
	'						  End If
	'					  End Sub)
	'		End Using

	'		token.ThrowIfCancellationRequested()
	'		CloseConn()
	'	Catch ex As OperationCanceledException
	'		CloseConn()
	'		Exit Sub
	'	Catch ex As Exception
	'		CloseConn()
	'		If cts IsNot Nothing AndAlso Not cts.IsCancellationRequested Then
	'			cts.Cancel()
	'			MessageBox.Show("Load Detail Error: " & ex.Message, "Error Terjadi", MessageBoxButtons.OK, MessageBoxIcon.Error)
	'		End If
	'	End Try
	'End Sub

	'Private Sub LoadDataDetailSplit(token As CancellationToken, Optional ByVal isFilter As Boolean = False)
	'	Dim DigitDecimal As String = ""
	'	Dim tgl1Value, tgl2Value As Date
	'	Dim idRoutingText As String = ""
	'	Dim kdBarangText As String = ""
	'	Dim noSplitText As String = ""
	'	Dim cmbIndex As Integer = 0

	'	Me.Invoke(Sub()
	'				  Select Case arrJenis(Cmb_Jenis.SelectedIndex)
	'					  Case "Y" : DigitDecimal = "N0"
	'					  Case "T" : DigitDecimal = "N4"
	'					  Case Else : DigitDecimal = "N4"
	'				  End Select
	'				  tgl1Value = Tgl1.Value
	'				  tgl2Value = Tgl2.Value
	'				  idRoutingText = Txt_IdRouting.Text
	'				  kdBarangText = Txt_KdBarang.Text
	'				  noSplitText = Txt_NoSplit.Text
	'				  cmbIndex = Cmb_Jenis.SelectedIndex
	'			  End Sub)

	'	Try
	'		OpenConn()
	'		If token.IsCancellationRequested Then token.ThrowIfCancellationRequested()

	'		Dim Filter_Tanggal_Awal As String = $"'{Format(tgl1Value, "yyyy-MM-dd")}'"
	'		Dim Filter_Tanggal_Akhir As String = $"'{Format(tgl2Value.AddDays(1), "yyyy-MM-dd")}'"
	'		Dim Filter_NoSplit As String = "DEFAULT"
	'		Dim Filter_ID_Routing As String = "DEFAULT"
	'		Dim Filter_KodeBarang As String = "DEFAULT"
	'		Dim Filter_GroupJenis As String = "DEFAULT"

	'		If isFilter Then
	'			If noSplitText.Trim.Length > 0 AndAlso noSplitText.ToUpper <> OpsiSeluruh.ToUpper Then
	'				Filter_NoSplit = $"'{noSplitText.Trim}'"
	'			End If
	'			If Not idRoutingText.ToUpper = OpsiSeluruh.ToUpper Then
	'				Filter_ID_Routing = $"'{idRoutingText.Trim}'"
	'			End If
	'			If Not kdBarangText.ToUpper = OpsiSeluruh.ToUpper Then
	'				Filter_KodeBarang = $"'{kdBarangText.Trim}'"
	'			End If
	'			If Not cmbIndex = 0 Then
	'				Filter_GroupJenis = $"'{arrJenis(cmbIndex).ToString.Trim}'"
	'			End If
	'		End If

	'		SQL = $"
	'           select No_PO, no_split, Kode_Barang, Nama, Tgl_Produksi, Jam_Produksi, Tanggal_GI, Jam_GI, Nama_Routing, Keterangan, Jumlah, Berat_GI, Jumlah_Dosing,
	'                  WasteGR1_KG, WasteGR1_Persen, Reject_GR1_KG, ScrapGR1_KG, Loss_Production, Loss_Production_Persen,
	'                  NilaiGR1_KG, NilaiGR1_Pcs, GR1_StockSementara, WaktuGR1,
	'                  WasteGR2_Pcs, WasteGR2_Persen, Persentase_Waste_Terhadap_GR2, Reject_GR2_PCS, Reject_GR2_KG, ScrapGR2_Pcs, ScrapGR2_KG, NilaiGR2_Pcs, GR2_StockSementara, GR2_Stock_Belum_Validasi, WaktuGR2,
	'                  WasteGR3_Pcs, WasteGR3_Persen, Reject_GR3_PCS, Reject_GR3_KG, ScrapGR3_Pcs, ScrapGR3_KG, NilaiGRFinal_Pcs, GR3_StockSementara, WaktuGR3,
	'                  Jumlah_Scrap_Packaging, Flag_Preservative
	'           FROM N_EMI_Function_Laporan_Akhir_GIGR2_By_Split('{KodePerusahaan}', {Filter_NoSplit}, {Filter_Tanggal_Awal}, {Filter_Tanggal_Akhir}, {Filter_ID_Routing}, {Filter_KodeBarang}, {Filter_GroupJenis})
	'           order by Tgl_Produksi, Jam_Produksi
	'       "

	'		Using Ds = BindingTrans_Thread(SQL)
	'			Dim dt As DataTable = Ds.Tables("MyTable")
	'			If token.IsCancellationRequested Then token.ThrowIfCancellationRequested()

	'			Me.Invoke(Sub()
	'						  Dgv_Detail2_Split.Rows.Clear()
	'						  If dt.Rows.Count <> 0 Then
	'							  For i As Integer = 0 To dt.Rows.Count - 1
	'								  Dgv_Detail2_Split.Rows.Add(1)
	'								  Dgv_Detail2_Split.Rows(i).Cells(0).Value = dt.Rows(i).Item("No_po")
	'								  Dgv_Detail2_Split.Rows(i).Cells(1).Value = dt.Rows(i).Item("no_split")
	'								  Dgv_Detail2_Split.Rows(i).Cells(2).Value = dt.Rows(i).Item("Kode_Barang")
	'								  Dgv_Detail2_Split.Rows(i).Cells(3).Value = dt.Rows(i).Item("Nama")
	'								  Dgv_Detail2_Split.Rows(i).Cells(4).Value = Format(dt.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy")
	'								  Dgv_Detail2_Split.Rows(i).Cells(5).Value = dt.Rows(i).Item("Jam_Produksi")
	'								  Dgv_Detail2_Split.Rows(i).Cells(6).Value = Format(dt.Rows(i).Item("Tanggal_GI"), "dd MMM yyyy")
	'								  Dgv_Detail2_Split.Rows(i).Cells(7).Value = dt.Rows(i).Item("Jam_GI")
	'								  Dgv_Detail2_Split.Rows(i).Cells(8).Value = dt.Rows(i).Item("Nama_Routing")
	'								  Dgv_Detail2_Split.Rows(i).Cells(9).Value = dt.Rows(i).Item("Keterangan")
	'								  Dgv_Detail2_Split.Rows(i).Cells(10).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Jumlah")) = "", 0, Format(dt.Rows(i).Item("Jumlah"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(11).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Berat_GI")) = "", 0, Format(dt.Rows(i).Item("Berat_GI"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(12).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Jumlah_Dosing")) = "", 0, Format(dt.Rows(i).Item("Jumlah_Dosing"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(13).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR1_KG")) = "", 0, Format(dt.Rows(i).Item("WasteGR1_KG"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(14).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR1_Persen")) = "", 0, Format(dt.Rows(i).Item("WasteGR1_Persen"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(15).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_GR1_KG")) = "", 0, Format(dt.Rows(i).Item("Reject_GR1_KG"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(16).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR1_KG")) = "", 0, Format(dt.Rows(i).Item("ScrapGR1_KG"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(17).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Loss_Production")) = "", 0, Format(dt.Rows(i).Item("Loss_Production"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(18).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Loss_Production_Persen")) = "", 0, Format(dt.Rows(i).Item("Loss_Production_Persen"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(19).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGR1_KG")) = "", 0, Format(dt.Rows(i).Item("NilaiGR1_KG"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(20).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGR1_Pcs")) = "", 0, Format(dt.Rows(i).Item("NilaiGR1_Pcs"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(21).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR1_StockSementara")) = "", 0, Format(dt.Rows(i).Item("GR1_StockSementara"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(22).Value = dt.Rows(i).Item("WaktuGR1")

	'								  Dgv_Detail2_Split.Rows(i).Cells(23).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR2_Pcs")) = "", 0, Format(dt.Rows(i).Item("WasteGR2_Pcs"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(24).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR2_Persen")) = "", 0, Format(dt.Rows(i).Item("WasteGR2_Persen"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(25).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Persentase_Waste_Terhadap_GR2")) = "", 0, Format(dt.Rows(i).Item("Persentase_Waste_Terhadap_GR2"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(26).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_GR2_PCS")) = "", 0, Format(dt.Rows(i).Item("Reject_GR2_PCS"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(27).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_GR2_KG")) = "", 0, Format(dt.Rows(i).Item("Reject_GR2_KG"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(28).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR2_Pcs")) = "", 0, Format(dt.Rows(i).Item("ScrapGR2_Pcs"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(29).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR2_KG")) = "", 0, Format(dt.Rows(i).Item("ScrapGR2_KG"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(30).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGR2_Pcs")) = "", 0, Format(dt.Rows(i).Item("NilaiGR2_Pcs"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(31).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR2_StockSementara")) = "", 0, Format(dt.Rows(i).Item("GR2_StockSementara"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(32).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR2_Stock_Belum_Validasi")) = "", 0, Format(dt.Rows(i).Item("GR2_Stock_Belum_Validasi"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(33).Value = dt.Rows(i).Item("WaktuGR2")

	'								  Dgv_Detail2_Split.Rows(i).Cells(34).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR3_Pcs")) = "", 0, Format(dt.Rows(i).Item("WasteGR3_Pcs"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(35).Value = If(General_Class.CekNULL(dt.Rows(i).Item("WasteGR3_Persen")) = "", 0, Format(dt.Rows(i).Item("WasteGR3_Persen"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(36).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_GR3_PCS")) = "", 0, Format(dt.Rows(i).Item("Reject_GR3_PCS"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(37).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Reject_GR3_KG")) = "", 0, Format(dt.Rows(i).Item("Reject_GR3_KG"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(38).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR3_Pcs")) = "", 0, Format(dt.Rows(i).Item("ScrapGR3_Pcs"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(39).Value = If(General_Class.CekNULL(dt.Rows(i).Item("ScrapGR3_KG")) = "", 0, Format(dt.Rows(i).Item("ScrapGR3_KG"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(40).Value = If(General_Class.CekNULL(dt.Rows(i).Item("NilaiGRFinal_Pcs")) = "", 0, Format(dt.Rows(i).Item("NilaiGRFinal_Pcs"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(41).Value = If(General_Class.CekNULL(dt.Rows(i).Item("GR3_StockSementara")) = "", 0, Format(dt.Rows(i).Item("GR3_StockSementara"), DigitDecimal))
	'								  Dgv_Detail2_Split.Rows(i).Cells(42).Value = dt.Rows(i).Item("WaktuGR3")
	'								  Dgv_Detail2_Split.Rows(i).Cells(43).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Jumlah_Scrap_Packaging")) = "", 0, Format(dt.Rows(i).Item("Jumlah_Scrap_Packaging"), "N4"))
	'								  Dgv_Detail2_Split.Rows(i).Cells(44).Value = If(General_Class.CekNULL(dt.Rows(i).Item("Flag_Preservative")) = "", "", dt.Rows(i).Item("Flag_Preservative"))

	'								  Dgv_Detail2_Split.Rows(i).Cells(13).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2_Split.Rows(i).Cells(14).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2_Split.Rows(i).Cells(15).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2_Split.Rows(i).Cells(16).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2_Split.Rows(i).Cells(17).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2_Split.Rows(i).Cells(18).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2_Split.Rows(i).Cells(19).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2_Split.Rows(i).Cells(20).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2_Split.Rows(i).Cells(21).Style.BackColor = Color.LightYellow
	'								  Dgv_Detail2_Split.Rows(i).Cells(22).Style.BackColor = Color.FromArgb(252, 105, 108)
	'								  Dgv_Detail2_Split.Rows(i).Cells(23).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2_Split.Rows(i).Cells(24).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2_Split.Rows(i).Cells(25).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2_Split.Rows(i).Cells(26).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2_Split.Rows(i).Cells(27).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2_Split.Rows(i).Cells(28).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2_Split.Rows(i).Cells(29).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2_Split.Rows(i).Cells(30).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2_Split.Rows(i).Cells(31).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2_Split.Rows(i).Cells(32).Style.BackColor = Color.LightBlue
	'								  Dgv_Detail2_Split.Rows(i).Cells(33).Style.BackColor = Color.FromArgb(181, 230, 162)
	'								  Dgv_Detail2_Split.Rows(i).Cells(34).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2_Split.Rows(i).Cells(35).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2_Split.Rows(i).Cells(36).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2_Split.Rows(i).Cells(37).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2_Split.Rows(i).Cells(38).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2_Split.Rows(i).Cells(39).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2_Split.Rows(i).Cells(40).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2_Split.Rows(i).Cells(41).Style.BackColor = Color.LightGray
	'								  Dgv_Detail2_Split.Rows(i).Cells(42).Style.BackColor = Color.White
	'								  Dgv_Detail2_Split.Rows(i).Cells(43).Style.BackColor = Color.FromArgb(244, 208, 111)
	'								  Dgv_Detail2_Split.Rows(i).Cells(44).Style.BackColor = Color.FromArgb(188, 158, 130)
	'								  Dgv_Detail2_Split.Rows(i).Cells(22).Style.Font = New Font(Dgv_Detail2_Split.Font, FontStyle.Bold)
	'								  Dgv_Detail2_Split.Rows(i).Cells(32).Style.Font = New Font(Dgv_Detail2_Split.Font, FontStyle.Bold)
	'								  Dgv_Detail2_Split.Rows(i).Cells(42).Style.Font = New Font(Dgv_Detail2_Split.Font, FontStyle.Bold)
	'							  Next
	'						  End If
	'					  End Sub)
	'		End Using

	'		token.ThrowIfCancellationRequested()
	'		CloseConn()
	'	Catch ex As OperationCanceledException
	'		CloseConn()
	'		Exit Sub
	'	Catch ex As Exception
	'		CloseConn()
	'		If cts IsNot Nothing AndAlso Not cts.IsCancellationRequested Then
	'			cts.Cancel()
	'			MessageBox.Show("Load Detail Split Error: " & ex.Message, "Error Terjadi", MessageBoxButtons.OK, MessageBoxIcon.Error)
	'		End If
	'	End Try
	'End Sub

#End Region

	'===============================================================================================================================================================================
	'=     HANDLE TEXT CHANGED
	'===============================================================================================================================================================================
	Private Sub Txt_IdRouting_TextChanged(sender As Object, e As EventArgs) Handles Txt_IdRouting.TextChanged
		If Txt_IdRouting.Text.Trim.Length = 0 Then
			Lv_Routing.Location = New Point(1200, 123)
			Lv_Routing.Visible = False
			Txt_IdRouting.Text = ""
			Txt_NmRouting.Text = ""
			Exit Sub
		Else
			Lv_Routing.Visible = True
			Lv_Routing.Location = New Point(140, 123)
		End If

		Try
			OpenConn()

			Lv_Routing.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Routing.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "select Id_Routing, Keterangan from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Routing like '%" & Txt_IdRouting.Text & "%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Routing.Items.Add(Dr("Id_Routing"))
					Lv.SubItems.Add(Dr("Keterangan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_NmRouting_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmRouting.TextChanged
		If Txt_NmRouting.Text.Trim.Length = 0 Then
			Lv_Routing.Location = New Point(1200, 123)
			Lv_Routing.Visible = False
			Txt_IdRouting.Text = ""
			Txt_NmRouting.Text = ""
			Exit Sub
		Else
			Lv_Routing.Visible = True
			Lv_Routing.Location = New Point(140, 123)
		End If

		Try
			OpenConn()

			Lv_Routing.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Routing.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "select Id_Routing, Keterangan from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_NmRouting.Text & "%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Routing.Items.Add(Dr("Id_Routing"))
					Lv.SubItems.Add(Dr("Keterangan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Lv_Barang.Location = New Point(1200, 152)
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(140, 152)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "select Distinct a.Kode_Barang, a.Nama "
			SQL = SQL & "from barang a, EMI_Group_Jenis b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
			SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Kode_Barang like '%" & Txt_KdBarang.Text & "%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
		If Txt_NmBarang.Text.Trim.Length = 0 Then
			Lv_Barang.Location = New Point(1200, 152)
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(140, 152)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "select Distinct a.Kode_Barang, a.Nama "
			SQL = SQL & "from barang a, EMI_Group_Jenis b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
			SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Nama like '%" & Txt_NmBarang.Text & "%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	'===============================================================================================================================================================================
	'=     HANDLE LEAVE
	'===============================================================================================================================================================================
	Private Sub Txt_IdRouting_Leave(sender As Object, e As EventArgs) Handles Txt_IdRouting.Leave
		If Txt_IdRouting.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Routing.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_IdRouting.Text = OpsiSeluruh Then

				SQL = "select Kode_Perusahaan_Biaya_Import, Nama from Perusahaan_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Perusahaan_Biaya_Import = '" & Txt_IdRouting.Text & "' "
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_IdRouting.Text = Dr("Kode_Perusahaan_Biaya_Import")
						Txt_NmRouting.Text = Dr("Nama")
						Txt_KdBarang.Focus()
					Else
						MessageBox.Show("Routing tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_IdRouting.Text = ""
						Txt_NmRouting.Text = ""
						Txt_IdRouting.Focus()
					End If

					Lv_Routing.Location = New Point(1200, 123)
					Lv_Routing.Visible = False
				End Using
			Else
				Txt_KdBarang.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
		If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Barang.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_KdBarang.Text = OpsiSeluruh Then

				SQL = "select Distinct a.Kode_Barang, a.Nama "
				SQL = SQL & "from barang a, EMI_Group_Jenis b "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
				SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
				SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' "
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_KdBarang.Text = Dr("Kode_Barang")
						Txt_NmBarang.Text = Dr("Nama")
						Txt_NoSplit.Focus()
					Else
						MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_KdBarang.Text = ""
						Txt_NmBarang.Text = ""
						Txt_KdBarang.Focus()
					End If

					Lv_Barang.Location = New Point(1200, 152)
					Lv_Barang.Visible = False
				End Using
			Else
				Txt_NoSplit.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	'===============================================================================================================================================================================
	'=     HANDLE KEYPRESS
	'===============================================================================================================================================================================
	Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
		If e.KeyChar = Chr(13) Then Tgl2.Focus()
	End Sub

	Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
		If e.KeyChar = Chr(13) Then Txt_IdRouting.Focus()
	End Sub

	Private Sub Txt_IdRouting_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_IdRouting.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_IdRouting.Text.Trim.Length = 0 Then Txt_IdRouting.Focus()
			Txt_IdRouting_Leave(Txt_IdRouting, e)

			Lv_Routing.Location = New Point(1200, 123)
			Lv_Routing.Visible = False

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_IdRouting_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_IdRouting.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Routing.Focus()
	End Sub

	Private Sub Txt_NmRouting_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmRouting.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_IdRouting_Leave(Txt_NmRouting, e)

			Lv_Routing.Location = New Point(1200, 123)
			Lv_Routing.Visible = False

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_NmRouting_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmRouting.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Routing.Focus()
	End Sub

	Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
			Txt_KdBarang_Leave(Txt_KdBarang, e)

			Lv_Barang.Location = New Point(1200, 152)
			Lv_Barang.Visible = False

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_KdBarang_Leave(Txt_NmBarang, e)

			Lv_Barang.Location = New Point(1200, 152)
			Lv_Barang.Visible = False

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Txt_NoSplit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoSplit.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Jenis.DroppedDown = True
			Cmb_Jenis.Focus()
		End If
	End Sub

	Private Sub Cmb_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
	End Sub

	'===============================================================================================================================================================================
	'=     HANDLE LV
	'===============================================================================================================================================================================
	Private Sub Lv_Routing_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Routing.DoubleClick
		If Lv_Routing.Items.Count = 0 Or Lv_Routing.FocusedItem.Index = -1 Then Exit Sub

		Dim IdRouting As String = Lv_Routing.FocusedItem.SubItems(0).Text
		Dim NmRouting As String = Lv_Routing.FocusedItem.SubItems(1).Text

		Txt_IdRouting.Text = IdRouting
		Txt_NmRouting.Text = NmRouting

		Lv_Routing.Location = New Point(1200, 123)
		Lv_Routing.Visible = False

		Txt_KdBarang.Focus()
	End Sub

	Private Sub Lv_Routing_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Routing.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Routing_DoubleClick(Lv_Routing, e)
		End If
	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

		Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim Nmbarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

		Txt_KdBarang.Text = KdBarang
		Txt_NmBarang.Text = Nmbarang

		Lv_Barang.Location = New Point(1200, 152)
		Lv_Barang.Visible = False

		Txt_NoSplit.Focus()
	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Barang_DoubleClick(Lv_Barang, e)
		End If
	End Sub

	'===============================================================================================================================================================================
	'=     HANDLE CETAK
	'===============================================================================================================================================================================

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_IdRouting.Text.Trim.Length = 0 Then
			MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_IdRouting.Focus() : Exit Sub
		ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		End If

		'Try
		'    OpenConn()

		'    '====================
		'    '=     CEK ROLE     =
		'    '====================
		'    If CekButtonRole("Cetak_FinalGIGR_Main_Rekap") = "T" Then
		'        CloseTrans()
		'        CloseConn()
		'        MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Cetak Laporan Rekap GI GR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'        Exit Sub
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		If MessageBox.Show("Yakin Ingin Cetak Rekap Laporan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

		Generate_Excel_Rekap3()

	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_IdRouting.Text.Trim.Length = 0 Then
			MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_IdRouting.Focus() : Exit Sub
		ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		End If

		'Try
		'    OpenConn()

		'    '====================
		'    '=     CEK ROLE     =
		'    '====================
		'    If CekButtonRole("Cetak_FinalGIGR_Main_Rekap") = "T" Then
		'        CloseTrans()
		'        CloseConn()
		'        MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Cetak Laporan Rekap GI GR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'        Exit Sub
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		If MessageBox.Show("Yakin Ingin Cetak Detail Laporan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

		Generate_Excel_Detail3()
	End Sub

	Private Sub Btn_Cetak_Detail_Per_Split_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_Detail_Per_Split.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_IdRouting.Text.Trim.Length = 0 Then
			MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_IdRouting.Focus() : Exit Sub
		ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		End If

		'Try
		'    OpenConn()

		'    '====================
		'    '=     CEK ROLE     =
		'    '====================
		'    If CekButtonRole("Cetak_FinalGIGR_Main_Rekap") = "T" Then
		'        CloseTrans()
		'        CloseConn()
		'        MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Cetak Laporan Rekap GI GR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'        Exit Sub
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		If MessageBox.Show("Yakin Ingin Cetak Detail Laporan Per Split?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

		Generate_Excel_Detail_PerSplit()
	End Sub

	Private Sub Generate_Excel_Rekap3()
		Try

			get_jam()

			Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

			'=======================================
			'=     CEK APAKAH EXCEL TERINSTALL     =
			'=======================================
			If xlApp Is Nothing Then
				MessageBox.Show("Excel is not properly installed!!")
				Return
			End If

			Dim JudulLaporan As String = "LAPORAN FINAL GI GR"

			Dim xlWorkBook As excel.Workbook
			Dim xlWorkSheet As excel.Worksheet
			Dim misValue As Object = System.Reflection.Missing.Value

			'Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

			'If System.IO.Directory.Exists(lokasi_file) = False Then
			'    System.IO.Directory.CreateDirectory(lokasi_file)
			'End If

			Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
			Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("Sheet1")

			'==================================
			'=     DEFINISIKAN NAMA KOLOM     =
			'==================================

			Dim DigitDecimal As String = ""

			Select Case arrJenis(Cmb_Jenis.SelectedIndex)
				Case "Y"
					DigitDecimal = "PCS"
				Case "T"
					DigitDecimal = "KG"
				Case Else
					DigitDecimal = "PCS"
			End Select

#Region "Generate Coloms"

			Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Good Issue"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Good Issue"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Keterangan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "PRO-RQ (" & DigitDecimal & ")"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Berat (Gram)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "PRO-LINE Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 200, 101))}}, 'Mulai Reject
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "QC-LINE Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 200, 101))}},
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "Warehouse-LINE Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 200, 101))}},
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "Total Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 200, 101))}},
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "PRO-LINE (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}}, 'Mulai GR 1
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "QC-LINE (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "Warehouse-LINE (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "Total (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "PRO-LINE (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}}, 'Mulai Val
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "QC-LINE (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "Warehouse-Line (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "Total (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Loss"}, {"Kolom", "LOSS (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
				New Dictionary(Of String, String) From {{"Identifier", "Inspection"}, {"Kolom", "Good Received Inspection (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(233, 69, 69))}}, 'Mulai Inspection
				New Dictionary(Of String, String) From {{"Identifier", "Inspection"}, {"Kolom", "Good Received Inspection (" & DigitDecimal & ")"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(233, 69, 69))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final_GR"}, {"Kolom", "Good Received Rejected (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final_GR"}, {"Kolom", "Good Received Rejected (" & DigitDecimal & ")"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}}, 'Mulai Rejected
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Reject (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Scrap (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received Inspection (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste_Packaging"}, {"Kolom", "Waste Packaging (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(244, 208, 111))}},
				New Dictionary(Of String, String) From {{"Identifier", "Flag_Preservative"}, {"Kolom", "Flag Preservative"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(188, 158, 130))}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Status"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}}
			}

			'Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
			'    {"Default", New Dictionary(Of String, Object) From {
			'        {"Default", "Default"},
			'        {"Kolom", 0}
			'    }}
			'}

			'====================================
			'=     HANDLE FILTER ROLE AKSES     =
			'====================================
			If Not isShowScrapPackaging Then
				dataKoloms = dataKoloms.Where(Function(k) k("Identifier") <> "Waste_Packaging").ToList()
			End If

			Dim tot_kolom As Integer = dataKoloms.Count
			Dim headerRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(3, 1), xlWorkSheet.Cells(4, tot_kolom))

			Dim headerValues(1, tot_kolom - 1) As Object
			Dim groupInfo As New Dictionary(Of String, Tuple(Of Integer, Integer, String))

			For i As Integer = 0 To tot_kolom - 1
				Dim kolom = dataKoloms(i)
				Dim currentIdentifier = kolom("Identifier").ToString()

				If currentIdentifier = "Main" OrElse currentIdentifier = "Loss" OrElse currentIdentifier = "Waste_Packaging" OrElse currentIdentifier = "Flag_Preservative" Then
					headerValues(0, i) = kolom("Kolom").ToString()
				Else
					headerValues(1, i) = kolom("Kolom").ToString()
				End If

				If Not groupInfo.ContainsKey(currentIdentifier) Then
					Dim groupTitle As String = If(currentIdentifier = "Main" OrElse currentIdentifier = "Loss" OrElse currentIdentifier = "Waste_Packaging" OrElse currentIdentifier = "Flag_Preservative", "", currentIdentifier)
					If currentIdentifier = "Inspection" Then groupTitle = "Inspection Good Received"
					If currentIdentifier = "Final_GR" Then groupTitle = "Good Received"
					If currentIdentifier = "Final" Then groupTitle = "Final Report"
					groupInfo.Add(currentIdentifier, Tuple.Create(i + 1, i + 1, groupTitle))
				Else
					Dim currentTuple = groupInfo(currentIdentifier)
					groupInfo(currentIdentifier) = Tuple.Create(currentTuple.Item1, i + 1, currentTuple.Item3)
				End If
			Next

			'Set Judul Kolom
			For Each kvp In groupInfo
				If Not String.IsNullOrEmpty(kvp.Value.Item3) Then
					headerValues(0, kvp.Value.Item1 - 1) = kvp.Value.Item3
				End If
			Next

			headerRange.Value = headerValues

			With headerRange
				.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				.VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				.Font.Bold = True
				.Borders.LineStyle = excel.XlLineStyle.xlContinuous
				.Borders.Weight = excel.XlBorderWeight.xlThin
			End With

			For Each kvp In groupInfo
				Dim identifier = kvp.Key
				Dim startCol = kvp.Value.Item1
				Dim endCol = kvp.Value.Item2
				' Mengambil warna dan melakukan casting dari Object ke Integer
				Dim color = CInt(dataKoloms.First(Function(k) k("Identifier").ToString() = identifier)("Warna"))

				If identifier = "Main" OrElse identifier = "Loss" OrElse identifier = "Waste_Packaging" OrElse identifier = "Flag_Preservative" Then
					For c As Integer = startCol To endCol
						If dataKoloms(c - 1)("Identifier").ToString() = "Main" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Loss" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Waste_Packaging" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Flag_Preservative" Then
							With xlWorkSheet.Range(xlWorkSheet.Cells(3, c), xlWorkSheet.Cells(4, c))
								.Merge()
								.Interior.Color = color
							End With
						End If
					Next
				Else
					Dim topRowRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(3, startCol), xlWorkSheet.Cells(3, endCol))
					Dim bottomRowRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(4, startCol), xlWorkSheet.Cells(4, endCol))
					topRowRange.Merge()
					topRowRange.Interior.Color = color
					bottomRowRange.Interior.Color = color
				End If
			Next

#Region "Kode Lama"

			'For i As Integer = 0 To dataKoloms.Count - 1
			'    Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

			'    If kolom("Identifier") = "Main" Then

			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

			'        xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        'BORDER
			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Reject" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Reject"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Scrap" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Scrap"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Waste" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Waste"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Loss" Then

			'        'Menambah nilai Range
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

			'        xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).ColumnWidth = 25

			'        'BORDER
			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Inspection" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Inspection Good Received"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Final_GR" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Good Received"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Final" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Final Report"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    End If

			'Next

#End Region

#End Region

			'=========================
			'=     GENERATE BODY     =
			'=========================

			Dim stringCenter As New List(Of Integer) From {2, 3, 4, 5, 6, 26, 37, 38}

			Dim numberColumn As New List(Of Integer) From {8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36}

			Dim DecimalColumn As New List(Of Integer) From {8, 25, 27}

			Dim NumberN0 As New List(Of Integer) From {8, 9, 21, 25, 27}

			Dim defaultRowIndex As Integer = 5

			Try
				OpenConn()

				' Ambil format sesuai culture
				Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
				xlApp.UseSystemSeparators = True

				'==  AMBIL SEPARATOR DARI EXCEL =='
				Dim decimalSep As String = xlApp.DecimalSeparator
				Dim groupSep As String = xlApp.ThousandsSeparator

				'==  AMBIL SEPARATOR DARI SISTEM =='
				'Dim decimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
				'Dim groupSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

				If decimalSep = "," Then
					decimalSep = "."
				ElseIf decimalSep = "." Then
					decimalSep = ","
				End If

				If groupSep = "." Then
					groupSep = ","
				ElseIf groupSep = "," Then
					groupSep = "."
				End If

				Dim templateFormat As String = "#GROUP##0DEC0000"
				Dim excelFormat As String = templateFormat _
					.Replace("GROUP", groupSep) _
					.Replace("DEC", decimalSep)

				Dim templateFormatN0 As String = "#GROUP##0"
				Dim excelFormatN0 As String = templateFormatN0 _
					.Replace("GROUP", groupSep)

				Dim jumlahRows As Integer = 0

				Dim row As Integer = 0
				SQL = "select no_po, No_split, Tgl_Produksi, Jam_Produksi, Tanggal_GI, Jam_GI, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Berat_GI, Jumlah_Dosing, "
				SQL = SQL & "Pro_Reject_KG, Qc_Reject_KG, Warehouse_Reject_KG, Tot_Reject_KG, "
				SQL = SQL & "ScrapGR1_KG, ScrapGR2_KG, ScrapGR3_KG, ScrapTotal_KG, "
				SQL = SQL & "WasteGR1_KG, WasteGR2_KG, WasteGR3_KG, WasteTotal_KG, Loss_Production_Final_GR, "
				SQL = SQL & "GR_Inspection_KG, GR_Inspection_PCS, NilaiGRFinal_KG, NilaiGRFinal_Pcs, "
				SQL = SQL & "Reject_Final_Persen, ScrapTotal_Persen, WasteTotal_Persen, Loss_Production_Final_GR_Persen, GR_Inspection_Persen, NilaiGRFinal_Persen, "
				If isShowScrapPackaging Then
					SQL = SQL & "Jumlah_Scrap_Packaging, "
				End If
				SQL = SQL & "Flag_Preservative, Status "

#Region "Kode Lama 09 Juni 2026"

				'SQL = SQL & "from Laporan_Akhir_GIGR_Rekap2 "
				'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
				'SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
				'If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
				'	SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
				'End If
				'If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
				'	SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
				'End If
				'If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
				'	SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
				'End If

				'If Not Cmb_Jenis.SelectedIndex = 0 Then
				'	SQL = SQL & "and Group_Jenis = '" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
				'End If
				'SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "

#End Region

				SQL = SQL & "from N_EMI_Function_Laporan_Akhir_GIGR_Rekap2 ( "
				SQL = SQL & "'" & KodePerusahaan & "', "

				If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
					SQL = SQL & "'" & Txt_NoSplit.Text.Trim & "', "
				Else
					SQL = SQL & "DEFAULT, "
				End If

				SQL = SQL & "'" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "', "

				If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
					SQL = SQL & "'" & Txt_IdRouting.Text.Trim & "', "
				Else
					SQL = SQL & "DEFAULT, "
				End If

				If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
					SQL = SQL & "'" & Txt_KdBarang.Text.Trim & "', "
				Else
					SQL = SQL & "DEFAULT, "
				End If

				If Not Cmb_Jenis.SelectedIndex = 0 Then
					SQL = SQL & "'" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
				Else
					SQL = SQL & "DEFAULT "
				End If
				SQL = SQL & ") "
				SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")

						If .Rows.Count = 0 Then
							CloseConn()
							MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If

						Dim rowCount As Integer = .Rows.Count
						Dim colCount As Integer = .Columns.Count
						jumlahRows = rowCount

						Dim startCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex, 1)
						Dim endCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex + rowCount - 1, colCount)
						Dim dataRange As excel.Range = xlWorkSheet.Range(startCell, endCell)

						Dim dataArray(rowCount - 1, colCount - 1) As Object

						'For r As Integer = 0 To rowCount - 1
						'    For c As Integer = 0 To colCount - 1

						'        'Ambil data dari data tabel
						'        Dim currentValue As Object = .Rows(r)(c)

						'        ' cek tipe datanya
						'        If TypeOf currentValue Is Date Then
						'            dataArray(r, c) = Format(CDate(currentValue), "dd MMM yyyy")
						'        Else
						'            dataArray(r, c) = General_Class.CekNULL(currentValue)
						'        End If

						'        'warna
						'        If .Rows(r)(34).ToString.ToUpper = "PRODUCTION" Then
						'            dataRange.Columns(35).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
						'        ElseIf .Rows(r)(34).ToString.ToUpper = "INSPECTION" Then
						'            dataRange.Columns(35).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
						'        ElseIf .Rows(r)(34).ToString.ToUpper = "WAREHOUSE" Then
						'            dataRange.Columns(35).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
						'        End If
						'    Next
						'Next

						For r As Integer = 0 To rowCount - 1
							For c As Integer = 0 To colCount - 1
								Dim currentValue As Object = .Rows(r)(c)
								If TypeOf currentValue Is Date Then
									dataArray(r, c) = CDate(currentValue).ToString("dd MMM yyyy")
								Else
									dataArray(r, c) = General_Class.CekNULL(currentValue)
								End If
							Next
						Next

						dataRange.Value = dataArray

						dataRange.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

						With dataRange.Borders
							.LineStyle = excel.XlLineStyle.xlContinuous
							.ColorIndex = 0
							.Weight = excel.XlBorderWeight.xlThin
						End With

						' Atur warna, format, dan style per celnya
						For c As Integer = 1 To colCount
							Dim colIndex As Integer = c - 1 ' Index berbasis 0
							Dim currentColumn As excel.Range = dataRange.Columns(c)

							If colIndex = 6 Then
								currentColumn.NumberFormat = "@"
							End If

							' Alignment untuk kolom String (Text)
							If stringCenter.Contains(colIndex) Then
								currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
							Else
								currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignLeft
							End If

							' Format kolom numerik (N4 atau N0)
							If numberColumn.Contains(colIndex) Then
								If NumberN0.Contains(colIndex) Then
									currentColumn.NumberFormat = excelFormatN0
								Else
									currentColumn.NumberFormat = excelFormat
								End If
								currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignRight

							End If
						Next

						xlWorkSheet.Range(dataRange.Columns(14), dataRange.Columns(17)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 217, 153))
						xlWorkSheet.Range(dataRange.Columns(18), dataRange.Columns(21)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
						xlWorkSheet.Range(dataRange.Columns(22), dataRange.Columns(25)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
						dataRange.Columns(26).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
						xlWorkSheet.Range(dataRange.Columns(27), dataRange.Columns(28)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCoral)
						xlWorkSheet.Range(dataRange.Columns(29), dataRange.Columns(30)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
						xlWorkSheet.Range(dataRange.Columns(31), dataRange.Columns(36)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCyan)

						If isShowScrapPackaging Then
							dataRange.Columns(37).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(249, 230, 117))
							dataRange.Columns(38).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(217, 200, 185))
						Else
							'dataRange.Columns(37).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(249, 230, 117))
							dataRange.Columns(37).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(217, 200, 185))
						End If

						Dim targetColorRange As excel.Range = dataRange.Columns(39) ' Kolom yang akan diwarnai

						' Hapus format kondisi lama jika ada
						targetColorRange.FormatConditions.Delete()

						' Kondisi 1: PRODUCTION
						Dim fc1 As excel.FormatCondition = targetColorRange.FormatConditions.Add(
						Type:=excel.XlFormatConditionType.xlExpression,
						Formula1:="=$AI" & defaultRowIndex & "=""PRODUCTION""")
						fc1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)

						' Kondisi 2: INSPECTION
						Dim fc2 As excel.FormatCondition = targetColorRange.FormatConditions.Add(
						Type:=excel.XlFormatConditionType.xlExpression,
						Formula1:="=$AI" & defaultRowIndex & "=""INSPECTION""")
						fc2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)

						' Kondisi 3: WAREHOUSE
						Dim fc3 As excel.FormatCondition = targetColorRange.FormatConditions.Add(
						Type:=excel.XlFormatConditionType.xlExpression,
						Formula1:="=$AI" & defaultRowIndex & "=""WAREHOUSE""")
						fc3.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

						dataRange.Columns.AutoFit()

					End With
				End Using

				' AutoFit kolom setelah semua data dimasukkan
				xlWorkSheet.Columns.AutoFit()

				'==========================
				'=     HEADER LAPORAN     =
				'==========================
				Dim panjangKolom As Integer = dataKoloms.Count

				xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

				xlWorkSheet.Cells(1, 1).Value = JudulLaporan
				xlWorkSheet.Cells(1, 1).Font.Size = 14
				xlWorkSheet.Cells(1, 1).Font.Bold = True
				xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				xlWorkSheet.Columns(1).AutoFit()

				'==========================
				'=     FOOTER LAPORAN     =
				'==========================

				Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).Value = Footer
				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				xlWorkSheet.Columns(1).AutoFit()

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			'=====================
			'=     SAVE FILE     =
			'=====================
			Dim saveFileDialog As New SaveFileDialog()

			' Set File Filter
			saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
			saveFileDialog.Title = "Save As"

			'Tampilkan Show Dialog Save as
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					Dim filePath As String = saveFileDialog.FileName

					xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

					'MessageBox.Show("File berhasil disimpan di: " & filePath)

					' Menutup workbook dan aplikasi Excel
					xlWorkBook.Close()
					xlApp.Quit()

					' Membebaskan objek Excel
					releaseObject(xlWorkSheet)
					releaseObject(xlWorkBook)
					releaseObject(xlApp)
				Catch ex As Exception
					MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
				End Try
			End If
		Catch ex As Exception
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Generate_Excel_Detail3()
		Try

			get_jam()

			Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

			'=======================================
			'=     CEK APAKAH EXCEL TERINSTALL     =
			'=======================================
			If xlApp Is Nothing Then
				MessageBox.Show("Excel is not properly installed!!")
				Return
			End If

			Dim JudulLaporan As String = "LAPORAN FINAL GI GR"

			Dim xlWorkBook As excel.Workbook
			Dim xlWorkSheet As excel.Worksheet
			Dim misValue As Object = System.Reflection.Missing.Value

			'Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

			'If System.IO.Directory.Exists(lokasi_file) = False Then
			'    System.IO.Directory.CreateDirectory(lokasi_file)
			'End If

			Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
			Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("Sheet1")

			'==================================
			'=     DEFINISIKAN NAMA KOLOM     =
			'==================================

			Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Good Issue"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Good Issue"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Keterangan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "PRO-RQ (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Batch"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Isi (Gram)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Waste (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},'Mulai GR 1
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Stock Sementara (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Adjustment (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}}, 'Mulai Val
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste PRD (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Reject (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Scrap (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Stock Sementara (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Stock Belum Validasi (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Waste (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}}, 'Mulai Rejected
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Reject (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Scrap (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Final GR (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Stock Sementara (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste_Packaging"}, {"Kolom", "Waste Packaging (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(244, 208, 111))}},
				New Dictionary(Of String, String) From {{"Identifier", "Flag_Preservative"}, {"Kolom", "Flag Preservative"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(188, 158, 130))}}
			}

			'====================================
			'=     HANDLE FILTER ROLE AKSES     =
			'====================================
			If Not isShowScrapPackaging Then
				dataKoloms = dataKoloms.Where(Function(k) k("Identifier") <> "Waste_Packaging").ToList()
			End If

			Dim tot_kolom As Integer = dataKoloms.Count
			Dim headerRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(3, 1), xlWorkSheet.Cells(4, tot_kolom))

			Dim headerValues(1, tot_kolom - 1) As Object
			Dim groupInfo As New Dictionary(Of String, Tuple(Of Integer, Integer, String))

			For i As Integer = 0 To tot_kolom - 1
				Dim kolom = dataKoloms(i)
				Dim currentIdentifier = kolom("Identifier").ToString()

				If currentIdentifier = "Main" OrElse currentIdentifier = "Loss" OrElse currentIdentifier = "Waste_Packaging" OrElse currentIdentifier = "Flag_Preservative" Then
					headerValues(0, i) = kolom("Kolom").ToString()
				Else
					headerValues(1, i) = kolom("Kolom").ToString()
				End If

				If Not groupInfo.ContainsKey(currentIdentifier) Then
					Dim groupTitle As String = If(currentIdentifier = "Main" OrElse currentIdentifier = "Loss" OrElse currentIdentifier = "Waste_Packaging" OrElse currentIdentifier = "Flag_Preservative", "", currentIdentifier)
					If currentIdentifier = "GR1" Then groupTitle = "Line Production (Good Received Lv I)"
					If currentIdentifier = "Val" Then groupTitle = "Quality Inspection (Good Received Lv II)"
					If currentIdentifier = "Rejected" Then groupTitle = "Warehouse"
					groupInfo.Add(currentIdentifier, Tuple.Create(i + 1, i + 1, groupTitle))
				Else
					Dim currentTuple = groupInfo(currentIdentifier)
					groupInfo(currentIdentifier) = Tuple.Create(currentTuple.Item1, i + 1, currentTuple.Item3)
				End If
			Next

			'Set Judul Kolom
			For Each kvp In groupInfo
				If Not String.IsNullOrEmpty(kvp.Value.Item3) Then
					headerValues(0, kvp.Value.Item1 - 1) = kvp.Value.Item3
				End If
			Next

			headerRange.Value = headerValues

			With headerRange
				.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				.VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				.Font.Bold = True
				.Borders.LineStyle = excel.XlLineStyle.xlContinuous
				.Borders.Weight = excel.XlBorderWeight.xlThin
			End With

			For Each kvp In groupInfo
				Dim identifier = kvp.Key
				Dim startCol = kvp.Value.Item1
				Dim endCol = kvp.Value.Item2
				' Mengambil warna dan melakukan casting dari Object ke Integer
				Dim color = CInt(dataKoloms.First(Function(k) k("Identifier").ToString() = identifier)("Warna"))

				If identifier = "Main" OrElse identifier = "Loss" OrElse identifier = "Waste_Packaging" OrElse identifier = "Flag_Preservative" Then
					For c As Integer = startCol To endCol
						If dataKoloms(c - 1)("Identifier").ToString() = "Main" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Loss" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Waste_Packaging" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Flag_Preservative" Then
							With xlWorkSheet.Range(xlWorkSheet.Cells(3, c), xlWorkSheet.Cells(4, c))
								.Merge()
								.Interior.Color = color
							End With
						End If
					Next
				Else
					Dim topRowRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(3, startCol), xlWorkSheet.Cells(3, endCol))
					Dim bottomRowRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(4, startCol), xlWorkSheet.Cells(4, endCol))
					topRowRange.Merge()
					topRowRange.Interior.Color = color
					bottomRowRange.Interior.Color = color
				End If
			Next

#Region "KODE LAMA"

			'Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
			'    {"Default", New Dictionary(Of String, Object) From {
			'        {"Default", "Default"},
			'        {"Kolom", 0}
			'    }}
			'}

			'For i As Integer = 0 To dataKoloms.Count - 1
			'    Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

			'    If kolom("Identifier") = "Main" Then

			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

			'        xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        'BORDER
			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "GR1" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Line Production (Good Received Lv I)"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Val" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Quality Inspection (Good Received Lv II)"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Rejected" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Warehouse"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Final" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Final Good Received"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    End If

			'Next

#End Region

			'=========================
			'=     GENERATE BODY     =
			'=========================

			'Dim stringCenter As New List(Of Integer) From {2, 3, 4, 5, 6, 9, 21, 30, 39, 41, 43}

			'Dim numberColumn As New List(Of Integer) From {8, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 23, 24, 25, 26, 27, 28, 29, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 42}

			'Dim DecimalColumn As New List(Of Integer) From {8, 19, 22, 24, 26, 28, 29, 31, 33, 35, 37, 38}

			'Dim NumberN0 As New List(Of Integer) From {8, 19, 22, 24, 26, 28, 29, 31, 33, 35, 37, 38}

			Dim stringCenter As New List(Of Integer) From {2, 3, 4, 5, 6, 11, 23, 35, 44, 46}

			Dim numberColumn As New List(Of Integer) From {
				10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21,
				22, 24, 25, 26, 27, 28, 29, 30, 31,
				32, 33, 34, 36, 37, 38, 39, 40, 41, 42, 43, 45
			}

			Dim DecimalColumn As New List(Of Integer) From {
				19, 22, 25,
				27, 29, 31, 37, 39, 41, 45
			}

			Dim NumberN0 As New List(Of Integer) From {
				11, 10, 21, 23, 24, 30, 28,
				32, 33, 34, 35, 36, 38, 40, 42, 43, 44
			}

			Dim defaultRowIndex As Integer = 5
			Try
				OpenConn()

				' Ambil format sesuai culture
				Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
				xlApp.UseSystemSeparators = True

				'==  AMBIL SEPARATOR DARI EXCEL =='
				Dim decimalSep As String = xlApp.DecimalSeparator
				Dim groupSep As String = xlApp.ThousandsSeparator

				'==  AMBIL SEPARATOR DARI SISTEM =='
				'Dim decimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
				'Dim groupSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

				If decimalSep = "," Then
					decimalSep = "."
				ElseIf decimalSep = "." Then
					decimalSep = ","
				End If

				If groupSep = "." Then
					groupSep = ","
				ElseIf groupSep = "," Then
					groupSep = "."
				End If

				Dim templateFormat As String = "#GROUP##0DEC0000"
				Dim excelFormat As String = templateFormat _
					.Replace("GROUP", groupSep) _
					.Replace("DEC", decimalSep)

				Dim templateFormatN0 As String = "#GROUP##0"
				Dim excelFormatN0 As String = templateFormatN0 _
					.Replace("GROUP", groupSep)

				Dim jumlahRows As Integer = 0

				Dim row As Integer = 0

#Region "Kode Lama, 08 Juni 2026"

				'SQL = "select No_po, no_split, Tgl_Produksi, Jam_Produksi, Tanggal_GI, Jam_GI, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, batch, Berat_GI, Jumlah_Dosing, "
				'SQL = SQL & "WasteGR1_KG, WasteGR1_Persen, Reject_GR1_KG, ScrapGR1_KG, Loss_Production, Loss_Production_Persen, NilaiGR1_KG, NilaiGR1_Pcs, GR1_StockSementara, WaktuGR1, Adjustment, "
				'SQL = SQL & "WasteGR2_Pcs, WasteGR2_Persen, Persentase_Waste_Terhadap_GR2, Reject_GR2_PCS, Reject_GR2_KG, ScrapGR2_PCS, ScrapGR2_KG,NilaiGR2_Pcs, GR2_StockSementara, WaktuGR2, "
				'SQL = SQL & "WasteGR3_Pcs, WasteGR3_Persen, Reject_GR3_PCS, Reject_GR3_KG, ScrapGR3_PCS, ScrapGR3_KG, NilaiGRFinal_Pcs, GR3_StockSementara, WaktuGR3, "

				'If isShowScrapPackaging Then
				'	SQL = SQL & "Jumlah_Scrap_Packaging, "
				'End If
				'SQL = SQL & "Flag_Preservative "
				'SQL = SQL & "from Laporan_Akhir_GIGR2 "
				'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
				'SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
				'If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
				'	SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
				'End If
				'If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
				'	SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
				'End If
				'If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
				'	SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
				'End If
				'If Not Cmb_Jenis.SelectedIndex = 0 Then
				'	SQL = SQL & "and Group_Jenis = '" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
				'End If
				'SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "

#End Region

				SQL = "select No_po, no_split, Tgl_Produksi, Jam_Produksi, Tanggal_GI, Jam_GI, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, batch, Berat_GI, Jumlah_Dosing, "
				SQL = SQL & "WasteGR1_KG, WasteGR1_Persen, Reject_GR1_KG, ScrapGR1_KG, Loss_Production, Loss_Production_Persen, NilaiGR1_KG, NilaiGR1_Pcs, GR1_StockSementara, WaktuGR1, Adjustment, "
				SQL = SQL & "WasteGR2_Pcs, WasteGR2_Persen, Persentase_Waste_Terhadap_GR2, Reject_GR2_PCS, Reject_GR2_KG, ScrapGR2_PCS, ScrapGR2_KG,NilaiGR2_Pcs, GR2_StockSementara, GR2_Stock_Belum_Validasi, WaktuGR2, "
				SQL = SQL & "WasteGR3_Pcs, WasteGR3_Persen, Reject_GR3_PCS, Reject_GR3_KG, ScrapGR3_PCS, ScrapGR3_KG, NilaiGRFinal_Pcs, GR3_StockSementara, WaktuGR3, "

				If isShowScrapPackaging Then
					SQL = SQL & "Jumlah_Scrap_Packaging, "
				End If
				SQL = SQL & "Flag_Preservative "
				SQL = SQL & "from N_EMI_Function_Laporan_Akhir_GIGR2 ( "
				SQL = SQL & "'" & KodePerusahaan & "', "

				If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
					SQL = SQL & "'" & Txt_NoSplit.Text.Trim & "', "
				Else
					SQL = SQL & "DEFAULT, "
				End If

				SQL = SQL & "'" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "', "

				If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
					SQL = SQL & "'" & Txt_IdRouting.Text.Trim & "', "
				Else
					SQL = SQL & "DEFAULT, "
				End If

				If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
					SQL = SQL & "'" & Txt_KdBarang.Text.Trim & "', "
				Else
					SQL = SQL & "DEFAULT, "
				End If

				If Not Cmb_Jenis.SelectedIndex = 0 Then
					SQL = SQL & "'" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
				Else
					SQL = SQL & "DEFAULT "
				End If
				SQL = SQL & ") "
				SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then

							If .Rows.Count = 0 Then
								CloseConn()
								MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							Dim rowCount As Integer = .Rows.Count
							Dim colCount As Integer = .Columns.Count
							jumlahRows = rowCount

							Dim startCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex, 1)
							Dim endCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex + rowCount - 1, colCount)
							Dim dataRange As excel.Range = xlWorkSheet.Range(startCell, endCell)

							Dim dataArray(rowCount - 1, colCount - 1) As Object

							'For r As Integer = 0 To rowCount - 1
							'    For c As Integer = 0 To colCount - 1

							'        'Ambil data dari data tabel
							'        Dim currentValue As Object = .Rows(r)(c)

							'        ' cek tipe datanya
							'        If TypeOf currentValue Is Date Then
							'            dataArray(r, c) = Format(CDate(currentValue), "dd MMM yyyy")
							'        Else
							'            dataArray(r, c) = General_Class.CekNULL(currentValue)
							'        End If
							'    Next
							'Next

							For r As Integer = 0 To rowCount - 1
								For c As Integer = 0 To colCount - 1
									Dim currentValue As Object = .Rows(r)(c)
									If TypeOf currentValue Is Date Then
										dataArray(r, c) = CDate(currentValue).ToString("dd MMM yyyy")
									Else
										dataArray(r, c) = General_Class.CekNULL(currentValue)
									End If
								Next
							Next

							dataRange.Value = dataArray

							dataRange.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

							With dataRange.Borders
								.LineStyle = excel.XlLineStyle.xlContinuous
								.ColorIndex = 0
								.Weight = excel.XlBorderWeight.xlThin
							End With

							For c As Integer = 1 To colCount
								Dim colIndex As Integer = c - 1 ' Index berbasis 0
								Dim currentColumn As excel.Range = dataRange.Columns(c)

								If colIndex = 6 Then
									currentColumn.NumberFormat = "@"
								End If

								' Alignment untuk kolom String (Text)
								If stringCenter.Contains(colIndex) Then
									currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
								Else
									currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignLeft
								End If

								' Format kolom numerik (N4 atau N0)
								If numberColumn.Contains(colIndex) Then
									If NumberN0.Contains(colIndex) Then
										currentColumn.NumberFormat = excelFormatN0
									Else
										currentColumn.NumberFormat = excelFormat
									End If
									currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignRight

								End If

							Next

							xlWorkSheet.Range(dataRange.Columns(15), dataRange.Columns(25)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
							xlWorkSheet.Range(dataRange.Columns(26), dataRange.Columns(36)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
							xlWorkSheet.Range(dataRange.Columns(37), dataRange.Columns(45)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
							dataRange.Columns(24).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(252, 105, 108))
							dataRange.Columns(36).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(181, 230, 162))
							dataRange.Columns(45).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)

							If isShowScrapPackaging Then
								dataRange.Columns(46).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(244, 208, 111))
								dataRange.Columns(47).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(188, 158, 130))
							Else
								'dataRange.Columns(44).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(244, 208, 111))
								dataRange.Columns(46).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(188, 158, 130))
							End If

							dataRange.Columns.AutoFit()
						Else
							CloseConn()
							MessageBox.Show("Data Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If

					End With
				End Using

				' AutoFit kolom setelah semua data dimasukkan
				xlWorkSheet.Columns.AutoFit()

				'==========================
				'=     HEADER LAPORAN     =
				'==========================
				Dim panjangKolom As Integer = dataKoloms.Count

				xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

				xlWorkSheet.Cells(1, 1).Value = JudulLaporan
				xlWorkSheet.Cells(1, 1).Font.Size = 14
				xlWorkSheet.Cells(1, 1).Font.Bold = True
				xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				xlWorkSheet.Columns(1).AutoFit()

				'==========================
				'=     FOOTER LAPORAN     =
				'==========================

				Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).Value = Footer
				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				xlWorkSheet.Columns(1).AutoFit()

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			'=====================
			'=     SAVE FILE     =
			'=====================
			Dim saveFileDialog As New SaveFileDialog()

			' Set File Filter
			saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
			saveFileDialog.Title = "Save As"

			'Tampilkan Show Dialog Save as
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					Dim filePath As String = saveFileDialog.FileName

					xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

					'MessageBox.Show("File berhasil disimpan di: " & filePath)

					' Menutup workbook dan aplikasi Excel
					xlWorkBook.Close()
					xlApp.Quit()

					' Membebaskan objek Excel
					releaseObject(xlWorkSheet)
					releaseObject(xlWorkBook)
					releaseObject(xlApp)
				Catch ex As Exception
					MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
				End Try
			End If
		Catch ex As Exception
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Generate_Excel_Detail_PerSplit()
		Try

			get_jam()

			Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

			'=======================================
			'=     CEK APAKAH EXCEL TERINSTALL     =
			'=======================================
			If xlApp Is Nothing Then
				MessageBox.Show("Excel is not properly installed!!")
				Return
			End If

			Dim JudulLaporan As String = "LAPORAN FINAL GI GR"

			Dim xlWorkBook As excel.Workbook
			Dim xlWorkSheet As excel.Worksheet
			Dim misValue As Object = System.Reflection.Missing.Value

			'Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

			'If System.IO.Directory.Exists(lokasi_file) = False Then
			'    System.IO.Directory.CreateDirectory(lokasi_file)
			'End If

			Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
			Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("Sheet1")

			'==================================
			'=     DEFINISIKAN NAMA KOLOM     =
			'==================================

			Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Good Issue"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Good Issue"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Keterangan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "PRO-RQ (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Isi (Gram)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Waste (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},'Mulai GR 1
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Stock Sementara (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Adjustment (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}}, 'Mulai Val
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste PRD (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Reject (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Scrap (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Stock Sementara (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Stock Belum Validasi (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Waste (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}}, 'Mulai Rejected
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Reject (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Scrap (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Final GR (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Stock Sementara (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste_Packaging"}, {"Kolom", "Waste Packaging (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(244, 208, 111))}},
				New Dictionary(Of String, String) From {{"Identifier", "Flag_Preservative"}, {"Kolom", "Flag Preservative"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(188, 158, 130))}}
			}

			'====================================
			'=     HANDLE FILTER ROLE AKSES     =
			'====================================
			If Not isShowScrapPackaging Then
				dataKoloms = dataKoloms.Where(Function(k) k("Identifier") <> "Waste_Packaging").ToList()
			End If

			Dim tot_kolom As Integer = dataKoloms.Count
			Dim headerRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(3, 1), xlWorkSheet.Cells(4, tot_kolom))

			Dim headerValues(1, tot_kolom - 1) As Object
			Dim groupInfo As New Dictionary(Of String, Tuple(Of Integer, Integer, String))

			For i As Integer = 0 To tot_kolom - 1
				Dim kolom = dataKoloms(i)
				Dim currentIdentifier = kolom("Identifier").ToString()

				If currentIdentifier = "Main" OrElse currentIdentifier = "Loss" OrElse currentIdentifier = "Waste_Packaging" OrElse currentIdentifier = "Flag_Preservative" Then
					headerValues(0, i) = kolom("Kolom").ToString()
				Else
					headerValues(1, i) = kolom("Kolom").ToString()
				End If

				If Not groupInfo.ContainsKey(currentIdentifier) Then
					Dim groupTitle As String = If(currentIdentifier = "Main" OrElse currentIdentifier = "Loss" OrElse currentIdentifier = "Waste_Packaging" OrElse currentIdentifier = "Flag_Preservative", "", currentIdentifier)
					If currentIdentifier = "GR1" Then groupTitle = "Line Production (Good Received Lv I)"
					If currentIdentifier = "Val" Then groupTitle = "Quality Inspection (Good Received Lv II)"
					If currentIdentifier = "Rejected" Then groupTitle = "Warehouse"
					groupInfo.Add(currentIdentifier, Tuple.Create(i + 1, i + 1, groupTitle))
				Else
					Dim currentTuple = groupInfo(currentIdentifier)
					groupInfo(currentIdentifier) = Tuple.Create(currentTuple.Item1, i + 1, currentTuple.Item3)
				End If
			Next

			'Set Judul Kolom
			For Each kvp In groupInfo
				If Not String.IsNullOrEmpty(kvp.Value.Item3) Then
					headerValues(0, kvp.Value.Item1 - 1) = kvp.Value.Item3
				End If
			Next

			headerRange.Value = headerValues

			With headerRange
				.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				.VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				.Font.Bold = True
				.Borders.LineStyle = excel.XlLineStyle.xlContinuous
				.Borders.Weight = excel.XlBorderWeight.xlThin
			End With

			For Each kvp In groupInfo
				Dim identifier = kvp.Key
				Dim startCol = kvp.Value.Item1
				Dim endCol = kvp.Value.Item2
				' Mengambil warna dan melakukan casting dari Object ke Integer
				Dim color = CInt(dataKoloms.First(Function(k) k("Identifier").ToString() = identifier)("Warna"))

				If identifier = "Main" OrElse identifier = "Loss" OrElse identifier = "Waste_Packaging" OrElse identifier = "Flag_Preservative" Then
					For c As Integer = startCol To endCol
						If dataKoloms(c - 1)("Identifier").ToString() = "Main" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Loss" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Waste_Packaging" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Flag_Preservative" Then
							With xlWorkSheet.Range(xlWorkSheet.Cells(3, c), xlWorkSheet.Cells(4, c))
								.Merge()
								.Interior.Color = color
							End With
						End If
					Next
				Else
					Dim topRowRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(3, startCol), xlWorkSheet.Cells(3, endCol))
					Dim bottomRowRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(4, startCol), xlWorkSheet.Cells(4, endCol))
					topRowRange.Merge()
					topRowRange.Interior.Color = color
					bottomRowRange.Interior.Color = color
				End If
			Next

			'=========================
			'=     GENERATE BODY     =
			'=========================

			'Dim stringCenter As New List(Of Integer) From {2, 3, 4, 5, 6, 9, 21, 30, 39, 41, 43}

			'Dim numberColumn As New List(Of Integer) From {8, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 23, 24, 25, 26, 27, 28, 29, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 42}

			'Dim DecimalColumn As New List(Of Integer) From {8, 19, 22, 24, 26, 28, 29, 31, 33, 35, 37, 38}

			'Dim NumberN0 As New List(Of Integer) From {8, 19, 22, 24, 26, 28, 29, 31, 33, 35, 37, 38}

			Dim stringCenter As New List(Of Integer) From {2, 3, 4, 5, 6, 22, 34, 43, 45}

			Dim numberColumn As New List(Of Integer) From {
				10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21,
				24, 23, 25, 26, 27, 28, 29, 30, 31,
				32, 33, 36, 37, 38, 39, 40, 41, 42, 44, 45
			}

			Dim DecimalColumn As New List(Of Integer) From {
				11, 12, 13, 15, 16, 17, 19, 21, 28, 30, 38, 40, 44
			}

			Dim NumberN0 As New List(Of Integer) From {
				10, 20, 22, 23, 24, 27, 29, 31,
				32, 33, 34, 35, 37, 39, 41, 42, 43
			}

			Dim defaultRowIndex As Integer = 5
			Try
				OpenConn()

				' Ambil format sesuai culture
				Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
				xlApp.UseSystemSeparators = True

				'==  AMBIL SEPARATOR DARI EXCEL =='
				Dim decimalSep As String = xlApp.DecimalSeparator
				Dim groupSep As String = xlApp.ThousandsSeparator

				'==  AMBIL SEPARATOR DARI SISTEM =='
				'Dim decimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
				'Dim groupSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

				If decimalSep = "," Then
					decimalSep = "."
				ElseIf decimalSep = "." Then
					decimalSep = ","
				End If

				If groupSep = "." Then
					groupSep = ","
				ElseIf groupSep = "," Then
					groupSep = "."
				End If

				Dim templateFormat As String = "#GROUP##0DEC0000"
				Dim excelFormat As String = templateFormat _
					.Replace("GROUP", groupSep) _
					.Replace("DEC", decimalSep)

				Dim templateFormatN0 As String = "#GROUP##0"
				Dim excelFormatN0 As String = templateFormatN0 _
					.Replace("GROUP", groupSep)

				Dim jumlahRows As Integer = 0

				Dim row As Integer = 0

				SQL = "select No_po, no_split, Tgl_Produksi, Jam_Produksi, Tanggal_GI, Jam_GI, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Berat_GI, Jumlah_Dosing, "
				SQL = SQL & "WasteGR1_KG, WasteGR1_Persen, Reject_GR1_KG, ScrapGR1_KG, Loss_Production, Loss_Production_Persen, NilaiGR1_KG, NilaiGR1_Pcs, GR1_StockSementara, WaktuGR1, Adjustment, "
				SQL = SQL & "WasteGR2_Pcs, WasteGR2_Persen, Persentase_Waste_Terhadap_GR2, Reject_GR2_PCS, Reject_GR2_KG, ScrapGR2_PCS, ScrapGR2_KG,NilaiGR2_Pcs, GR2_StockSementara, GR2_Stock_Belum_Validasi, WaktuGR2, "
				SQL = SQL & "WasteGR3_Pcs, WasteGR3_Persen, Reject_GR3_PCS, Reject_GR3_KG, ScrapGR3_PCS, ScrapGR3_KG, NilaiGRFinal_Pcs, GR3_StockSementara, WaktuGR3, "

				If isShowScrapPackaging Then
					SQL = SQL & "Jumlah_Scrap_Packaging, "
				End If
				SQL = SQL & "Flag_Preservative "
				SQL = SQL & "from N_EMI_Function_Laporan_Akhir_GIGR2_By_Split ( "
				SQL = SQL & "'" & KodePerusahaan & "', "

				If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
					SQL = SQL & "'" & Txt_NoSplit.Text.Trim & "', "
				Else
					SQL = SQL & "DEFAULT, "
				End If

				SQL = SQL & "'" & Format(Tgl1.Value, "yyyy-MM-dd") & "', '" & Format(Tgl2.Value, "yyyy-MM-dd") & "', "

				If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
					SQL = SQL & "'" & Txt_IdRouting.Text.Trim & "', "
				Else
					SQL = SQL & "DEFAULT, "
				End If

				If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
					SQL = SQL & "'" & Txt_KdBarang.Text.Trim & "', "
				Else
					SQL = SQL & "DEFAULT, "
				End If

				If Not Cmb_Jenis.SelectedIndex = 0 Then
					SQL = SQL & "'" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
				Else
					SQL = SQL & "DEFAULT "
				End If
				SQL = SQL & ") "
				SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then

							If .Rows.Count = 0 Then
								CloseConn()
								MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							Dim rowCount As Integer = .Rows.Count
							Dim colCount As Integer = .Columns.Count
							jumlahRows = rowCount

							Dim startCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex, 1)
							Dim endCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex + rowCount - 1, colCount)
							Dim dataRange As excel.Range = xlWorkSheet.Range(startCell, endCell)

							Dim dataArray(rowCount - 1, colCount - 1) As Object

							'For r As Integer = 0 To rowCount - 1
							'    For c As Integer = 0 To colCount - 1

							'        'Ambil data dari data tabel
							'        Dim currentValue As Object = .Rows(r)(c)

							'        ' cek tipe datanya
							'        If TypeOf currentValue Is Date Then
							'            dataArray(r, c) = Format(CDate(currentValue), "dd MMM yyyy")
							'        Else
							'            dataArray(r, c) = General_Class.CekNULL(currentValue)
							'        End If
							'    Next
							'Next

							For r As Integer = 0 To rowCount - 1
								For c As Integer = 0 To colCount - 1
									Dim currentValue As Object = .Rows(r)(c)
									If TypeOf currentValue Is Date Then
										dataArray(r, c) = CDate(currentValue).ToString("dd MMM yyyy")
									Else
										dataArray(r, c) = General_Class.CekNULL(currentValue)
									End If
								Next
							Next

							dataRange.Value = dataArray

							dataRange.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

							With dataRange.Borders
								.LineStyle = excel.XlLineStyle.xlContinuous
								.ColorIndex = 0
								.Weight = excel.XlBorderWeight.xlThin
							End With

							For c As Integer = 1 To colCount
								Dim colIndex As Integer = c - 1 ' Index berbasis 0
								Dim currentColumn As excel.Range = dataRange.Columns(c)

								If colIndex >= 6 Then
									currentColumn.NumberFormat = "@"
								End If

								' Alignment untuk kolom String (Text)
								If stringCenter.Contains(colIndex) Then
									currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
								Else
									currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignLeft
								End If

								' Format kolom numerik (N4 atau N0)
								If numberColumn.Contains(colIndex) Then
									If NumberN0.Contains(colIndex) Then
										currentColumn.NumberFormat = excelFormatN0
									Else
										currentColumn.NumberFormat = excelFormat
									End If
									currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignRight
								End If

							Next

							xlWorkSheet.Range(dataRange.Columns(14), dataRange.Columns(24)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
							xlWorkSheet.Range(dataRange.Columns(25), dataRange.Columns(35)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
							xlWorkSheet.Range(dataRange.Columns(36), dataRange.Columns(44)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
							dataRange.Columns(23).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(252, 105, 108))
							dataRange.Columns(35).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(181, 230, 162))
							dataRange.Columns(44).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)

							If isShowScrapPackaging Then
								dataRange.Columns(45).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(244, 208, 111))
								dataRange.Columns(46).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(188, 158, 130))
							Else
								'dataRange.Columns(44).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(244, 208, 111))
								dataRange.Columns(45).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(188, 158, 130))
							End If

							dataRange.Columns.AutoFit()
						Else
							CloseConn()
							MessageBox.Show("Data Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If

					End With
				End Using

				' AutoFit kolom setelah semua data dimasukkan
				xlWorkSheet.Columns.AutoFit()

				'==========================
				'=     HEADER LAPORAN     =
				'==========================
				Dim panjangKolom As Integer = dataKoloms.Count

				xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

				xlWorkSheet.Cells(1, 1).Value = JudulLaporan
				xlWorkSheet.Cells(1, 1).Font.Size = 14
				xlWorkSheet.Cells(1, 1).Font.Bold = True
				xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				xlWorkSheet.Columns(1).AutoFit()

				'==========================
				'=     FOOTER LAPORAN     =
				'==========================

				Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).Value = Footer
				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				xlWorkSheet.Columns(1).AutoFit()

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			'=====================
			'=     SAVE FILE     =
			'=====================
			Dim saveFileDialog As New SaveFileDialog()

			' Set File Filter
			saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
			saveFileDialog.Title = "Save As"

			'Tampilkan Show Dialog Save as
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					Dim filePath As String = saveFileDialog.FileName

					xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

					'MessageBox.Show("File berhasil disimpan di: " & filePath)

					' Menutup workbook dan aplikasi Excel
					xlWorkBook.Close()
					xlApp.Quit()

					' Membebaskan objek Excel
					releaseObject(xlWorkSheet)
					releaseObject(xlWorkBook)
					releaseObject(xlApp)
				Catch ex As Exception
					MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
				End Try
			End If
		Catch ex As Exception
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub releaseObject(ByVal obj As Object)
		Try
			System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
			obj = Nothing
		Catch ex As Exception
			obj = Nothing
		Finally
			GC.Collect()
		End Try
	End Sub

	Private Sub EnableDoubleBufferDGV(dgv As DataGridView)
		Dim t As Type = dgv.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(dgv, True, Nothing)
	End Sub

	Private Sub Dgv_Rekap_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Rekap.MouseMove, Dgv_Detail2.MouseMove, Dgv_Detail2_Split.MouseMove
		HandleDataGridViewHover(sender, e)
	End Sub

	Private Sub HandleDataGridViewHover(dgv As DataGridView, e As MouseEventArgs)
		Dim hit As DataGridView.HitTestInfo = dgv.HitTest(e.X, e.Y)

		dgv.Cursor = If(hit.Type = DataGridViewHitTestType.Cell, Cursors.Hand, Cursors.Default)

		If hit.RowIndex <> lastIndex Then

			If lastIndex >= 0 AndAlso lastIndex < dgv.Rows.Count Then
				dgv.Rows(lastIndex).DefaultCellStyle.BackColor = originalColor
			End If

			If hit.Type = DataGridViewHitTestType.Cell AndAlso hit.RowIndex >= 0 Then
				lastIndex = hit.RowIndex

				Dim currentRow = dgv.Rows(lastIndex)

				originalColor = currentRow.DefaultCellStyle.BackColor

				Dim displayColor As Color = currentRow.InheritedStyle.BackColor

				Dim amount As Integer = 23
				currentRow.DefaultCellStyle.BackColor = Color.FromArgb(
					Math.Max(0, displayColor.R - amount),
					Math.Max(0, displayColor.G - amount),
					Math.Max(0, displayColor.B - amount)
				)
			Else
				lastIndex = -1
			End If
		End If
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_IdRouting.Text.Trim.Length = 0 Then
			MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_IdRouting.Focus() : Exit Sub
		ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		ElseIf Cmb_Jenis.SelectedIndex = -1 Then
			MessageBox.Show("Jenis harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		ElseIf Txt_NoSplit.Text.Trim.Length = 0 Then
			Txt_NoSplit.Text = OpsiSeluruh
		End If
		'Dim sw As New Stopwatch()
		'sw.Start()
		TabControl1.SelectedIndex = 1
		'LoadRekap(True)
		LoadDataDetail(True)
		'LoadDataDetailSplit(True)
		'sw.Stop()
		'
		'Console.WriteLine("Waktu query: " & sw.ElapsedMilliseconds & " ms")
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_IdRouting.Text.Trim.Length = 0 Then
			MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_IdRouting.Focus() : Exit Sub
		ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		ElseIf Cmb_Jenis.SelectedIndex = -1 Then
			MessageBox.Show("Jenis harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		ElseIf Txt_NoSplit.Text.Trim.Length = 0 Then
			Txt_NoSplit.Text = OpsiSeluruh
		End If
		'Dim sw As New Stopwatch()
		'sw.Start()
		TabControl1.SelectedIndex = 2
		'LoadRekap(True)
		'LoadDataDetail(True)
		LoadDataDetailSplit(True)
		'sw.Stop()
		'
		'Console.WriteLine("Waktu query: " & sw.ElapsedMilliseconds & " ms")
	End Sub

	'Public Function BindingTrans_Thread(ByVal query As String) As DataSet
	'	Dim ds As New DataSet

	'	Dim strKoneksi As String = "Data Source=" & CServer & ";Initial Catalog=" & CDatabase & ";User Id=" & CUserId & ";Password=" & CPassword & ";" &
	'					";Connect Timeout=30;Max Pool Size=400"

	'	Using connLocal As New SqlConnection(strKoneksi)
	'		Using cmdLocal As New SqlCommand(query, connLocal)
	'			Using adapter As New SqlDataAdapter(cmdLocal)
	'				cmdLocal.CommandTimeout = 0
	'				connLocal.Open()
	'				adapter.Fill(ds, "MyTable")
	'			End Using
	'		End Using
	'	End Using
	'	Return ds
	'End Function

End Class