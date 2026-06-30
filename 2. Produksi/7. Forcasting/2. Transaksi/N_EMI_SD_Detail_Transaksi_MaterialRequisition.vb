'Imports Microsoft.SqlServer.Server
Imports Microsoft.Office.Interop

Public Class N_EMI_SD_Detail_Transaksi_MaterialRequisition
	Public arrBulan, arrBulanMM As New ArrayList
	Dim Jenis = "Master_Jenis_Hewan"
	Public fRef As String
	Private lastHeaderColumnIndex As Integer = -1
	Dim ToolTip1 As New ToolTip

	Public lokasi_mrp As String
	Public bulanSkrng As String
	Public tahunSkrng As String
	Public BulanKe As String
	Public ListEndingStock As New List(Of (Kode_Bahan As String, Bulan As String, Jenis As String, Jumlah As Double))
	Dim JenisEndingStock As String

	Dim arrCellInputPPIC As New ArrayList
	Dim DataOutStandingPRD As New List(Of (KdBahan As String, Jumlah As Double))

	Dim Lv0 As String
	Dim LVKd_Barang As String
	Dim LvNm_Barang As String
	Dim LvSatuanBarang As String
	Dim LvBelumTerjadwal As String
	Dim LvM1 As String
	Dim LvPOM1 As String
	Dim LvReqStokM1 As String
	Dim LvPemisah1 As String
	Dim LvM2 As String
	Dim LvPOM2 As String
	Dim LvReqStokM2 As String
	Dim LvPemisah2 As String
	Dim LvM3 As String
	Dim LvPOM3 As String
	Dim LvReqStokM3 As String
	Dim LvPemisah3 As String
	Dim LvM4 As String
	Dim LvPOM4 As String
	Dim LvReqStokM4 As String
	Dim LvEndingStock As String
	Dim LvOutStandingPRD As String

	Dim Cell0 As Integer = 0
	Dim CellKd_Barang As Integer = 1
	Dim CellNm_Barang As Integer = 2
	Dim cell_Satuan As Integer = 3

	Dim cell_BelumTerjadwal As Integer = 4

	' ===== M1 =====
	Dim cell_M1 As Integer = 5

	Dim cell_POM1 As Integer = 6
	Dim cell_ReqStokM1 As Integer = 7
	Dim cellPemisah1 As Integer = 8

	' ===== M2 =====
	Dim cell_M2 As Integer = 9

	Dim cell_POM2 As Integer = 10
	Dim cell_ReqStokM2 As Integer = 11
	Dim cellPemisah2 As Integer = 12

	' ===== M3 =====
	Dim cell_M3 As Integer = 13

	Dim cell_POM3 As Integer = 14
	Dim cell_ReqStokM3 As Integer = 15
	Dim cellPemisah3 As Integer = 16

	' ===== M4 =====
	Dim cell_M4 As Integer = 17

	Dim cell_POM4 As Integer = 18
	Dim cell_ReqStokM4 As Integer = 19

	Dim cellPemisah4 As Integer = 20
	Dim cellEndingStock As Integer = 21
	Dim cellOutStandingPRD As Integer = 22

	Dim Cell_PRIncomM1 As Integer = 23
	Dim Cell_PRIncomM2 As Integer = 24
	Dim Cell_PRIncomM3 As Integer = 25
	Dim Cell_PRIncomM4 As Integer = 26

	Public Property fstatus As String = ""

	Public Arrbarang As New ArrayList
	Public Arrlokasi As New ArrayList
	Public ArrNama As New ArrayList
	' Public ArrJenis As New ArrayList

	Dim NamaBulan As New List(Of String) From {
		"Index", "Januari", "Februari", "Maret", "April", "Mei", "Juni",
		"Juli", "Agustus", "September", "Oktober", "November", "Desember"
	}

	'Private Sub FunTotalJumlahCurrentMonth(ByVal jumlahProductionPlan As Double, ByVal stok As Double, ByVal Jumlah_out_pr As Double, ByVal Jumlah_out_po As Double, ByVal index As Integer, ByVal Jumlah_PRD As Double)

	'    Dim EstJumlahStok As Double = 0

	'    Dim nilaiAkhir As Double = 0

	'    EstJumlahStok = (Val(stok) + Val(Jumlah_out_pr) + Val(Jumlah_out_po))

	'    'nilaiAkhir = Val(jumlahProductionPlan) - Val(EstJumlahStok)

	'    '  DataGridView1.Rows(index).Cells(cellRequirmentStok).Value = Format((jumlahProductionPlan - Jumlah_PRD) - EstJumlahStok, "N2")

	'End Sub

	Private Sub FunTotalJumlahLooping(ByVal jumlahProductionPlan As Double, ByVal jumlah_out_pr As Double, ByVal Jumlah_out_po As Double, ByVal index As Integer, ByVal cells As Integer)

		Dim EstJumlahStok As Double = 0

		Dim nilaiAkhir As Double = 0

		EstJumlahStok = Val(Jumlah_out_po) + Val(jumlah_out_pr)

		'nilaiAkhir = Val(jumlahProductionPlan) - Val(EstJumlahStok)

		If EstJumlahStok - (jumlahProductionPlan) >= 0 Then
			DataGridView1.Rows(index).Cells(cells).Style.BackColor = Color.FromArgb(26, 191, 98)
		Else
			DataGridView1.Rows(index).Cells(cells).Style.BackColor = Color.FromArgb(255, 204, 0)
		End If

		DataGridView1.Rows(index).Cells(cells).Value = Format(EstJumlahStok - jumlahProductionPlan, "N2")

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

	Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
		'Lv0 = DataGridView1.Rows(No_Index).Cells(Cell0).Value.ToString
		' Ambil Data dari grid
		LVKd_Barang = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKd_Barang).Value)
		LvNm_Barang = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNm_Barang).Value)
		LvSatuanBarang = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_Satuan).Value)

		LvBelumTerjadwal = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_BelumTerjadwal).Value)

		' Minggu 1
		LvM1 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_M1).Value)
		LvPOM1 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_POM1).Value)
		LvReqStokM1 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_ReqStokM1).Value)

		' Minggu 2
		LvM2 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_M2).Value)
		LvPOM2 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_POM2).Value)
		LvReqStokM2 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_ReqStokM2).Value)

		' Minggu 3
		LvM3 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_M3).Value)
		LvPOM3 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_POM3).Value)
		LvReqStokM3 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_ReqStokM3).Value)

		' Minggu 4
		LvM4 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_M4).Value)
		LvPOM4 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_POM4).Value)
		LvReqStokM4 = CekNothing(DataGridView1.Rows(No_Index).Cells(cell_ReqStokM4).Value)
		LvEndingStock = CekNothing(DataGridView1.Rows(No_Index).Cells(cellEndingStock).Value)
		LvOutStandingPRD = CekNothing(DataGridView1.Rows(No_Index).Cells(cellOutStandingPRD).Value)

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If ComboBox1.Text.Trim.Length = 0 Then
			MessageBox.Show("Bulan Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox1.Focus() : Exit Sub
		ElseIf ComboBox2.Text.Trim.Length = 0 Then
			MessageBox.Show("Tahun Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox2.Focus() : Exit Sub
		ElseIf ComboBox3.Text.Trim.Length = 0 Then
			MessageBox.Show("Lokasi Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox3.Focus() : Exit Sub
		End If

		'ComboBox1.Enabled = False
		'ComboBox2.Enabled = False
		'ComboBox3.Enabled = False
		'SD_Pilih_Produk.urutcmb = ComboBox1.SelectedIndex
		'SD_Pilih_Produk.ShowDialog()
	End Sub

	Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub kosong()
		ComboBox1.Items.Clear() : arrBulan.Clear() : arrBulanMM.Clear()
		ComboBox1.Items.Add("Januari") : arrBulan.Add("1") : arrBulanMM.Add("01")
		ComboBox1.Items.Add("Februari") : arrBulan.Add("2") : arrBulanMM.Add("02")
		ComboBox1.Items.Add("Maret") : arrBulan.Add("3") : arrBulanMM.Add("03")
		ComboBox1.Items.Add("April") : arrBulan.Add("4") : arrBulanMM.Add("04")
		ComboBox1.Items.Add("Mei") : arrBulan.Add("5") : arrBulanMM.Add("05")
		ComboBox1.Items.Add("Juni") : arrBulan.Add("6") : arrBulanMM.Add("06")
		ComboBox1.Items.Add("Juli") : arrBulan.Add("7") : arrBulanMM.Add("07")
		ComboBox1.Items.Add("Agustus") : arrBulan.Add("8") : arrBulanMM.Add("08")
		ComboBox1.Items.Add("September") : arrBulan.Add("9") : arrBulanMM.Add("09")
		ComboBox1.Items.Add("Oktober") : arrBulan.Add("10") : arrBulanMM.Add("10")
		ComboBox1.Items.Add("November") : arrBulan.Add("11") : arrBulanMM.Add("11")
		ComboBox1.Items.Add("Desember") : arrBulan.Add("12") : arrBulanMM.Add("12")
		ComboBox1.SelectedIndex = -1
		ComboBox1.Enabled = False

		'ComboBox2.Items.Clear()
		'Dim tahun_awal As Integer = Date.Now.Year - 2
		'Dim tahun_akhir As Integer = Date.Now.Year + 2
		'For a As Integer = tahun_awal To tahun_akhir
		'    ComboBox2.Items.Add(a)
		'Next
		'ComboBox2.SelectedIndex = -1
		''ComboBox2.Enabled = True
		'ComboBox2.Enabled = False
		Btn_Simpan.Tag = "&Simpan"

		get_jam()

		Dim akses_ubah As String = "Y"
		Dim akses_realease As String = "Y"
		Dim akses_unrealease As String = "Y"

		Dim CurrentMonth As String = Val(HilangkanTanda(bulanSkrng)) - 1
		If BulanKe = 1 Then
			JenisEndingStock = "STOCK BAHAN BAKU"
			DataGridView1.Columns(cellEndingStock).HeaderText = $"Actual Stock ({NamaBulan(CurrentMonth)} {tahunSkrng})"
		ElseIf BulanKe = 2 Then
			JenisEndingStock = "ENDING STOCK CURRENT"
		ElseIf BulanKe = 3 Then
			JenisEndingStock = "ENDING STOCK M1"
		ElseIf BulanKe = 4 Then
			JenisEndingStock = "ENDING STOCK M2"
		ElseIf BulanKe = 5 Then
			JenisEndingStock = "ENDING STOCK M3"
		ElseIf BulanKe = 6 Then
			JenisEndingStock = "ENDING STOCK M4"
		ElseIf BulanKe = 7 Then
			JenisEndingStock = "ENDING STOCK M5"
		End If

		'Try
		'    OpenConn()

		'    If CekButtonRole("MRP_PPIC") = "Y" Then
		'        akses_ubah = "Y"
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		''
		'Try
		'    OpenConn()

		'    If CekButtonRole("MRP_Realease") = "Y" Then
		'        akses_realease = "Y"
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		'Try
		'    OpenConn()

		'    If CekButtonRole("MRP_Unrealease") = "Y" Then
		'        akses_unrealease = "Y"
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		Try
			OpenConn()
			ComboBox3.Items.Clear()

			ComboBox3.Items.Add(lokasi_mrp)

			ComboBox3.SelectedIndex = 0
			ComboBox3.Enabled = True

			'get_no_faktur()
			TxtBarangMasuk_NoFaktur.Enabled = True

			'If fstatus = "MRP_PPIC" Then
			'    If akses_ubah = "Y" Then
			'        DataGridView1.Columns(Cell0).ReadOnly = False
			'        DataGridView1.Columns(CellKd_Barang).ReadOnly = True
			'        DataGridView1.Columns(CellNm_Barang).ReadOnly = True

			'        DataGridView1.Columns(CellStock_BB).ReadOnly = True
			'        DataGridView1.Columns(CellOPRequesition).ReadOnly = True
			'        DataGridView1.Columns(CellOPOrder).ReadOnly = True
			'        DataGridView1.Columns(CellTotal).ReadOnly = True

			'        DataGridView1.Columns(CellStatus).ReadOnly = True
			'        DataGridView1.Columns(CellSatuanBarang).ReadOnly = True

			'        CheckBox1.Enabled = False
			'        Button1.Enabled = False
			'        Btn_Simpan.Enabled = True
			'        Btn_Simpan.Tag = "&Simpan"

			'    Else
			'        DataGridView1.Columns(Cell0).ReadOnly = False
			'        DataGridView1.Columns(CellKd_Barang).ReadOnly = True
			'        DataGridView1.Columns(CellNm_Barang).ReadOnly = True

			'        DataGridView1.Columns(CellStock_BB).ReadOnly = True
			'        DataGridView1.Columns(CellOPRequesition).ReadOnly = True
			'        DataGridView1.Columns(CellOPOrder).ReadOnly = True
			'        DataGridView1.Columns(CellTotal).ReadOnly = True

			'        DataGridView1.Columns(CellStatus).ReadOnly = True

			'        DataGridView1.Columns(CellSatuanBarang).ReadOnly = True
			'        DataGridView1.Columns(CellSatuanBarang).DisplayIndex = 3

			'        CheckBox1.Enabled = False
			'        Button1.Enabled = False
			'        Btn_Simpan.Enabled = False
			'    End If

			'    If akses_realease = "Y" Then
			'        Btn_Realese.Enabled = True
			'    Else
			'        Btn_Realese.Enabled = False
			'    End If

			'    If akses_unrealease = "Y" Then
			'        btnUnRelease.Enabled = True
			'    Else
			'        btnUnRelease.Enabled = False
			'    End If
			'End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		DateTimePicker1.Enabled = True

		DataGridView1.Rows.Clear()
		Dim selectedDate As Date = New Date(CInt(tahunSkrng), CInt(bulanSkrng), 1)
		Dim selectedMonthName As String = selectedDate.ToString("MMMM", New Globalization.CultureInfo("id-ID"))
		Dim selectedYear As Integer = selectedDate.Year

		ComboBox2.Items.Add(tahunSkrng)

		ComboBox1.SelectedItem = selectedMonthName
		ComboBox2.SelectedItem = selectedYear

		ComboBox2.SelectedIndex = 0

		If ComboBox1.SelectedIndex = -1 Then
			Exit Sub
		ElseIf ComboBox2.SelectedIndex = -1 Then
			Exit Sub
		ElseIf ComboBox3.SelectedIndex = -1 Then
			Exit Sub
		End If

		Start_Loading(Me)
		GetDataRix()
		End_Loading(Me)

	End Sub

	Private Sub get_no_faktur()
		Dim FPro_Results As String = "TMR"
		TxtBarangMasuk_NoFaktur.Text = FPro_Results & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("EMI_Transaksi_Material_Requsition", "No_Faktur", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(No_Faktur, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & Format(tgl_skg, "MMyy"))
	End Sub

	Function GetMinggu(tgl As Date) As Integer


		Dim day As Integer = 0
		If tgl_skg > tgl Then
			day = tgl_skg.Day
		Else
			day = tgl.Day
		End If

		If day <= 7 Then
			Return 1
		ElseIf day <= 14 Then
			Return 2
		ElseIf day <= 21 Then
			Return 3
		Else
			Return 4
		End If
	End Function
	Private Sub releaseObject(ByVal obj As Object)
		Try
			System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
			obj = Nothing
		Catch
			obj = Nothing
		Finally
			GC.Collect()
		End Try
	End Sub
	Private Sub DataGridView1_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellMouseEnter
		' Cek jika mouse berada di HEADER kolom yang valid
		If e.RowIndex = -1 AndAlso e.ColumnIndex >= 0 Then

			If e.ColumnIndex <> lastHeaderColumnIndex Then
				lastHeaderColumnIndex = e.ColumnIndex

				Dim judulBalon As String = ""
				Dim isiBalon As String = ""

				' Ambil NAMA objek kolom
				Dim namaKolom As String = DataGridView1.Columns(e.ColumnIndex).Name

				Dim bulanTahun As String = ""
				Dim headerText As String = DataGridView1.Columns(e.ColumnIndex).HeaderText
				If headerText.Contains("-") Then
					Dim potongan() As String = headerText.Split("-"c)

					' Ambil tahun dari potongan paling terakhir
					Dim tahun As String = potongan(potongan.Length - 1).Trim()

					' Ambil bagian sebelum tahun, lalu pecah per spasi untuk ambil nama bulannya
					Dim bagianKiri As String = potongan(potongan.Length - 2).Trim()
					Dim kataBagianKiri() As String = bagianKiri.Split(" "c)
					Dim bulan As String = kataBagianKiri(kataBagianKiri.Length - 1).Trim()

					' Satukan jadi format: "Juni - 2026"
					bulanTahun = bulan & " - " & tahun
				End If

				' PAKAI SELECT CASE BERDASARKAN NAMA KOLOM
				Select Case namaKolom
					Case "Column46"
						judulBalon = "Finished PRD"
						isiBalon = "PRD yang telah di GOOD ISSUE"

					Case Else
						judulBalon = DataGridView1.Columns(e.ColumnIndex).HeaderText
						isiBalon = "Data " & judulBalon
				End Select

				' ==========================================================
				' LOGIKA PEMBAGI KATA BIAR AUTO ENTER
				' ==========================================================
				Dim kata() As String = isiBalon.Split(" "c)
				Dim teksHasil As String = ""
				For i As Integer = 0 To kata.Length - 1
					teksHasil &= kata(i) & " "
					If (i + 1) Mod 6 = 0 Then
						teksHasil &= vbCrLf
					End If
				Next
				isiBalon = teksHasil.Trim()

				' Hancurkan ToolTip lama agar fresh
				If ToolTip1 IsNot Nothing Then
					ToolTip1.Hide(DataGridView1)
					ToolTip1.Dispose()
				End If

				' Buat ulang objek ToolTip baru
				ToolTip1 = New ToolTip()
				ToolTip1.ToolTipIcon = ToolTipIcon.Info
				ToolTip1.ToolTipTitle = judulBalon
				ToolTip1.AutoPopDelay = Int32.MaxValue

				' Ambil kotak area header kolom
				Dim headerRect As Rectangle = DataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False)

				Dim centerX As Integer = headerRect.Left + (headerRect.Width \ 2)
				Dim centerY As Integer = headerRect.Bottom - 5

				' Hitung sisa ruang kanan
				Dim sisaRuangKanan As Integer = DataGridView1.Width - headerRect.Left


				If sisaRuangKanan < 420 Then
					' --- UNTUK 3 KOLOM UJUNG KANAN ---
					ToolTip1.IsBalloon = False ' Ubah jadi KOTAK biasa (Anti Meleset)

					' KUNCI: Tetap tembak pakai centerX & centerY biar pas di tengah bawah kolom!
					ToolTip1.Show(isiBalon, DataGridView1, centerX, centerY)
				Else
					' --- UNTUK KOLOM NORMAL (SISA KOLOM SEBELAH KIRI) ---
					ToolTip1.IsBalloon = True ' Tetap pakai BALON cakep

					ToolTip1.Show(isiBalon, DataGridView1, centerX, centerY)
				End If
			End If

		End If
	End Sub
	Private Sub DataGridView1_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellMouseLeave
		' JIKA mouse keluar dari area Header, langsung sembunyikan balonnya
		If e.RowIndex = -1 Then
			ToolTip1.Hide(DataGridView1)
			lastHeaderColumnIndex = -1
		End If
	End Sub
	Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		get_jam()

		kosong()

		For Each col As DataGridViewColumn In DataGridView1.Columns
			col.SortMode = DataGridViewColumnSortMode.NotSortable
		Next

		ToolTip1.IsBalloon = True

		ToolTip1.ToolTipIcon = ToolTipIcon.Info


		DataGridView1.ShowCellToolTips = False


		Me.Dock = DockStyle.Fill

		DataGridView1.Columns(Cell0).HeaderText = "#"
		DataGridView1.Columns(CellKd_Barang).HeaderText = "Kode Barang"
		DataGridView1.Columns(CellNm_Barang).HeaderText = "Nama Barang"

		'DataGridView1.Columns(CellStock_BB).HeaderText = "Stok Bahan Baku"
		'DataGridView1.Columns(CellOPRequesition).HeaderText = "Open Purchase Requsition"
		'DataGridView1.Columns(CellOPOrder).HeaderText = "Open Purchase Order"
		'DataGridView1.Columns(CellTotal).HeaderText = "Total Stock + Open PR + Open PO"

		'    DataGridView1.Columns(CellKosong_6).HeaderText = ""
		'DataGridView1.Columns(CellReferensi).HeaderText = "Referensi"
		' DataGridView1.Columns(CellStatus).HeaderText = "Status"

		' Ambil jumlah hari dalam bulan (otomatis handle Feb, kabisat, dll)
		Dim jumlahHari As Integer = DateTime.DaysInMonth(tahunSkrng, bulanSkrng)

		Dim m1_start As New DateTime(tahunSkrng, bulanSkrng, 1)
		Dim m1_end As New DateTime(tahunSkrng, bulanSkrng, 7)

		Dim m2_start As New DateTime(tahunSkrng, bulanSkrng, 8)
		Dim m2_end As New DateTime(tahunSkrng, bulanSkrng, 14)

		Dim m3_start As New DateTime(tahunSkrng, bulanSkrng, 15)
		Dim m3_end As New DateTime(tahunSkrng, bulanSkrng, 21)

		Dim m4_start As New DateTime(tahunSkrng, bulanSkrng, 22)
		Dim m4_end As New DateTime(tahunSkrng, bulanSkrng, jumlahHari)

		DataGridView1.Columns(cell_M1).HeaderText = "M1 (" & m1_start.ToString("dd MMM") & " - " & m1_end.ToString("dd MMM") & ")"
		DataGridView1.Columns(cell_M2).HeaderText = "M2 (" & m2_start.ToString("dd MMM") & " - " & m2_end.ToString("dd MMM") & ")"
		DataGridView1.Columns(cell_M3).HeaderText = "M3 (" & m3_start.ToString("dd MMM") & " - " & m3_end.ToString("dd MMM") & ")"
		DataGridView1.Columns(cell_M4).HeaderText = "M4 (" & m4_start.ToString("dd MMM") & " - " & m4_end.ToString("dd MMM") & ")"

		DataGridView1.Columns(cellEndingStock).DisplayIndex = 4
		DataGridView1.Columns(cellOutStandingPRD).DisplayIndex = 5

		DataGridView1.Columns(Cell_PRIncomM1).DisplayIndex = 9
		DataGridView1.Columns(Cell_PRIncomM2).DisplayIndex = 14
		DataGridView1.Columns(Cell_PRIncomM3).DisplayIndex = 19
		DataGridView1.Columns(Cell_PRIncomM4).DisplayIndex = 24

		Dim CurrentMonth As String = Val(HilangkanTanda(bulanSkrng)) - 1
		DataGridView1.Columns(cellEndingStock).HeaderText = $"Ending Stock ({NamaBulan(CurrentMonth)} {tahunSkrng})"

		Try
			OpenConn()

			get_no_faktur()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		'End If

	End Sub

	Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
		'If ComboBox1.SelectedIndex = -1 Then
		'    Exit Sub
		'ElseIf ComboBox2.SelectedIndex = -1 Then
		'    Exit Sub
		'ElseIf ComboBox3.SelectedIndex = -1 Then
		'    Exit Sub
		'End If

		'Start_Loading(Me)
		'GetDataRix()
		'End_Loading(Me)

	End Sub

	Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
		'If ComboBox1.SelectedIndex = -1 Then
		'    Exit Sub
		'ElseIf ComboBox2.SelectedIndex = -1 Then
		'    Exit Sub
		'ElseIf ComboBox3.SelectedIndex = -1 Then
		'    Exit Sub
		'End If

		'Start_Loading(Me)
		'GetDataRix()
		'End_Loading(Me)
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		kosong()
	End Sub

	Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
		'If ComboBox1.SelectedIndex = -1 Then
		'    Exit Sub
		'ElseIf ComboBox2.SelectedIndex = -1 Then
		'    Exit Sub
		'End If

		''Start_Loading(Me)
		'''getdata()
		''GetDataRix()
		''End_Loading(Me)
	End Sub

	Private Sub DateTimePicker1_CloseUp(sender As Object, e As EventArgs) Handles DateTimePicker1.CloseUp
		If DateTimePicker1.Value = Nothing Then Exit Sub
		'If ComboBox3.SelectedIndex = -1 Then Exit Sub

		Dim selectedDate As Date = DateTimePicker1.Value
		Dim selectedMonthName As String = selectedDate.ToString("MMMM", New Globalization.CultureInfo("id-ID"))
		Dim selectedYear As Integer = selectedDate.Year

		'If selectedYear < tahun_awal Or selectedYear > tahun_akhir Then
		'    MessageBox.Show("Tahun harus dalam rentang dua tahun dari tahun ini.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    Exit Sub
		'End If

		ComboBox1.SelectedItem = selectedMonthName
		ComboBox2.SelectedItem = selectedYear
	End Sub

	'==============================================================================================================
	'==============================================================================================================
	'==============================================================================================================
	Private Sub GetDataRix()
		DataGridView1.Rows.Clear()

		DataGridView1.Columns(Cell0).HeaderText = "#"
		DataGridView1.Columns(CellKd_Barang).HeaderText = "Kode Barang"
		DataGridView1.Columns(CellNm_Barang).HeaderText = "Nama Barang"

		'DataGridView1.Columns(CellStock_BB).HeaderText = "Stok Bahan Baku"
		'DataGridView1.Columns(CellOPRequesition).HeaderText = "Open Purchase Requsition"
		'DataGridView1.Columns(CellOPOrder).HeaderText = "Open Purchase Order"
		'DataGridView1.Columns(CellTotal).HeaderText = "Total Stock + Open PR + Open PO"

		Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
		Dim fthn As Integer = Val(ComboBox2.Text)
		Dim panggil_databulan As String = ""
		Dim panggil_datatahun As String = ""

		Dim listBulan1 As New List(Of String)

		listBulan1.Add(a.ToString("00"))

		Dim kondisi As New List(Of String)

		Dim kondisiRix As New List(Of String)

		kondisi.Add("(b.bulan='" & a.ToString("00") & "' AND b.Tahun='" & fthn & "')")

		kondisiRix.Add("(a.bulan='" & a.ToString("00") & "' AND a.Tahun='" & fthn & "')")

		Dim b As String = ""

		Dim BulanAwal As String = ""

		BulanAwal = arrBulanMM.Item(arrBulan.Item(ComboBox1.SelectedIndex) - 1)

		For i As Integer = 1 To 6
			' Perbarui nilai bulan dan tahun
			If a = 12 Then
				a = 1
				fthn = fthn + 1
			Else
				a = a + 1

				'  a = a
			End If

			kondisi.Add("(b.bulan='" & a.ToString("00") & "' AND b.Tahun='" & fthn & "')")

			kondisiRix.Add("(a.bulan='" & a.ToString("00") & "' AND a.Tahun='" & fthn & "')")

			listBulan1.Add(a.ToString("00"))

			' Temukan nama bulan yang sesuai
			For index = 0 To arrBulan.Count - 1
				If arrBulan.Item(index) = a Then
					b = ComboBox1.Items(index)
					If i = 1 Then ' Hanya sekali untuk panggil_databulan di iterasi pertama
						panggil_databulan = arrBulanMM.Item(index)
						panggil_datatahun = fthn
					End If
				End If
			Next

		Next

		Dim listBulan As String = "'" & String.Join("','", listBulan1) & "'"

		'   DataGridView1.Columns(CellStatus).HeaderText = "Status"

		Dim fLoad As Boolean = False

		Dim filterSQL As String = String.Join(" OR ", kondisi)

		Dim filterSQL2 As String = String.Join(" OR ", kondisiRix)

		Try
			OpenConn()

			get_no_faktur()

			Arrbarang.Clear()
			Arrlokasi.Clear()
			ArrNama.Clear()

			'SQL = "with cte as ("
			'SQL = SQL & "SELECT e.Kode_Stock_Owner,e.Kode_Barang,c.Nama "
			'SQL = SQL & "FROM EMI_Transaksi_Sales_Forecasting a "
			'SQL = SQL & "INNER JOIN EMI_Transaksi_Sales_Forecasting_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur "

			'SQL = SQL & "INNER JOIN Emi_Transaksi_Formulator d ON d.Kode_Perusahaan = b.Kode_Perusahaan AND d.No_Faktur =  b.Kode_Formula "
			'SQL = SQL & "INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan e ON d.Kode_Perusahaan = e.Kode_Perusahaan AND d.No_Faktur = e.No_Faktur "
			'SQL = SQL & "INNER JOIN barang c ON e.Kode_Perusahaan = c.Kode_Perusahaan AND e.Kode_Stock_Owner = c.Kode_Stock_Owner AND e.Kode_Barang = c.Kode_Barang "
			'SQL = SQL & "WHERE a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'SQL = SQL & "AND a.lokasi= '" & ComboBox3.Text & "' "
			''SQL = SQL & "AND b.bulan in (" & listBulan & ") "
			''SQL = SQL & "AND b.Tahun='" & panggil_datatahun & "' "

			'SQL = SQL & "and a.tahun = '" & tahunSkrng & " ' and a.bulan = '" & bulanSkrng & "' "

			'SQL = SQL & "AND b.Flag_Validasi='Y' "
			'SQL = SQL & "AND b.Flag_Validasi_PPIC='Y' "
			'SQL = SQL & "GROUP BY e.Kode_Stock_Owner,e.Kode_Barang,c.Nama "

			'SQL = SQL & "Union all "

			'SQL = SQL & "SELECT e.Kode_Stock_Owner,e.Kode_Barang,c.Nama "
			'SQL = SQL & "FROM EMI_Transaksi_Sales_Forecasting a "
			'SQL = SQL & "INNER JOIN EMI_Transaksi_Sales_Forecasting_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur "

			'SQL = SQL & "INNER JOIN Emi_Transaksi_Formulator d ON d.Kode_Perusahaan = b.Kode_Perusahaan  "
			'SQL = SQL & "INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan e ON d.Kode_Perusahaan = e.Kode_Perusahaan AND d.No_Faktur = e.No_Faktur "
			'SQL = SQL & "INNER JOIN N_EMI_Production_Plan_Schedule_Detail f on b.kode_perusahaan =  f.kode_perusahaan and b.urut = f.urut_production_plan and f.kode_formula = d.no_faktur "
			'SQL = SQL & "INNER JOIN N_EMI_Production_Plan_Schedule g on f.kode_perusahaan = g.kode_perusahaan and f.no_transaksi = g.no_transaksi and g.status is null "
			'SQL = SQL & "INNER JOIN barang c ON e.Kode_Perusahaan = c.Kode_Perusahaan AND e.Kode_Stock_Owner = c.Kode_Stock_Owner AND e.Kode_Barang = c.Kode_Barang "
			'SQL = SQL & "WHERE a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'SQL = SQL & "AND a.lokasi= '" & ComboBox3.Text & "' "

			'SQL = SQL & "and a.tahun = '" & tahunSkrng & " ' and a.bulan = '" & bulanSkrng & "' "

			'SQL = SQL & "AND b.Flag_Validasi='Y' "
			'SQL = SQL & "AND b.Flag_Validasi_PPIC='Y' "
			'SQL = SQL & "GROUP BY e.Kode_Stock_Owner,e.Kode_Barang,c.Nama ) "

			'SQL = SQL & "select distinct Kode_Stock_Owner,Kode_Barang,Nama from cte "

			'SQL = SQL & "Union all "

			'SQL = SQL & "Select d.Kode_Stock_Owner,c.Kode_Bahan As Kode_Barang,d.Nama from "
			'SQL = SQL & "EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_Detail b, barang_detail_bahan_penolong c, barang d, barang e "
			'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And b.Kode_Perusahaan = c.Kode_Perusahaan "
			'SQL = SQL & "And b.Kode_Perusahaan=e.Kode_Perusahaan And b.Kode_barang=e.Kode_Barang And b.kode_stock_owner=e.kode_stock_owner "
			'SQL = SQL & "And e.Kode_Barang_inq = c.Kode_Barang And c.Kode_Perusahaan = d.Kode_Perusahaan "
			'SQL = SQL & " And c.Kode_Bahan = d.Kode_Barang And b.Kode_Stock_Owner = d.Kode_Stock_Owner And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "And a.lokasi = '" & ComboBox3.Text & "'  "

			'SQL = SQL & "and a.tahun = '" & tahunSkrng & " ' and a.bulan = '" & bulanSkrng & "' "

			'SQL = SQL & "and b.Flag_Validasi = 'Y' and b.Flag_Validasi_PPIC = 'Y'  "
			'SQL = SQL & "group by d.Kode_Stock_Owner, c.Kode_Bahan, d.Nama "

			'SQL = SQL & "order by nama "

			SQL = $"
					with cte as (

					select distinct isnull((select top 1 c.No_Faktur
					from N_EMI_Transaksi_Formulator_Binding a
					inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join Emi_Transaksi_Formulator c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Formulator = c.No_Faktur and c.Status is null
					where a.Status is NULL
					and a.Flag_Validasi_Main = 'Y'
					and c.Flag_Validasi_Formula_Produksi_BOD = 'Y'
					and no_prioritas = 1
					and a.Kode_Perusahaan = x.kode_perusahaan
					and a.Kode_Barang = x.kode_barang_inq
					order by a.Tanggal DESC, a.Jam DESC, b.No_Prioritas ASC),'') as kode_formula, x.kode_barang_inq, x.kode_perusahaan
					from barang x, emI_group_jenis y where x.kode_perusahaan=y.kode_perusahaan and x.id_group_jenis=y.id_group_jenis and (y.Flag_Finished_Good='Y' or y.Flag_Semi_FG='Y')
				
					), cte_b as(

					SELECT e.Kode_Stock_Owner,e.Kode_Barang,c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis
					FROM EMI_Transaksi_Sales_Forecasting a
					INNER JOIN EMI_Transaksi_Sales_Forecasting_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur
					INNER JOIN barang bb ON b.kode_perusahaan=bb.kode_perusahaan and b.kode_barang=bb.kode_barang and b.kode_stock_owner=bb.kode_stock_owner
					INNER JOIN cte bc ON bb.kode_perusahaan=bc.kode_perusahaan and bb.kode_barang_inq=bc.kode_barang_inq
					INNER JOIN Emi_Transaksi_Formulator d ON d.Kode_Perusahaan = bc.Kode_Perusahaan AND d.No_Faktur =  bc.Kode_Formula
					INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan e ON d.Kode_Perusahaan = e.Kode_Perusahaan AND d.No_Faktur = e.No_Faktur
					INNER JOIN barang c ON e.Kode_Perusahaan = c.Kode_Perusahaan AND e.Kode_Stock_Owner = c.Kode_Stock_Owner AND e.Kode_Barang = c.Kode_Barang
					WHERE a.Kode_Perusahaan='{KodePerusahaan}' AND a.lokasi= '{ComboBox3.Text}' AND a.tahun = '{tahunSkrng}' and a.bulan = '{bulanSkrng}'
					AND b.Flag_Validasi='Y'
					AND b.Flag_Validasi_PPIC='Y'
					GROUP BY e.Kode_Stock_Owner,e.Kode_Barang,c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis

					Union all

					SELECT e.Kode_Stock_Owner,e.Kode_Barang,c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis
					FROM EMI_Transaksi_Sales_Forecasting a
					INNER JOIN EMI_Transaksi_Sales_Forecasting_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur
					INNER JOIN Emi_Transaksi_Formulator d ON d.Kode_Perusahaan = b.Kode_Perusahaan
					INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan e ON d.Kode_Perusahaan = e.Kode_Perusahaan AND d.No_Faktur = e.No_Faktur
					INNER JOIN N_EMI_Production_Plan_Schedule_Detail f on b.kode_perusahaan =  f.kode_perusahaan and b.urut = f.urut_production_plan and f.kode_formula = d.no_faktur
					INNER JOIN N_EMI_Production_Plan_Schedule g on f.kode_perusahaan = g.kode_perusahaan and f.no_transaksi = g.no_transaksi and g.status is null
					INNER JOIN barang c ON e.Kode_Perusahaan = c.Kode_Perusahaan AND e.Kode_Stock_Owner = c.Kode_Stock_Owner AND e.Kode_Barang = c.Kode_Barang
					WHERE a.Kode_Perusahaan='{KodePerusahaan}' AND a.lokasi= '{ComboBox3.Text}' AND a.tahun = '{tahunSkrng}' and a.bulan = '{bulanSkrng}'
					AND b.Flag_Validasi='Y'
					AND b.Flag_Validasi_PPIC='Y'
					GROUP BY e.Kode_Stock_Owner,e.Kode_Barang,c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis

					)
					select distinct a.Kode_Stock_Owner,a.Kode_Barang,a.Nama, '1' as asal from cte_b a, EMI_Group_Jenis b where a.kode_perusahaan=b.Kode_Perusahaan and a.id_group_jenis=b.Id_Group_Jenis
					and b.Flag_Raw_Material='Y'

					Union all

					Select d.Kode_Stock_Owner,c.Kode_Bahan As Kode_Barang,d.Nama, '2' as asal from
					EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_Detail b, barang_detail_bahan_penolong c, barang d, barang e
					where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And b.Kode_Perusahaan = c.Kode_Perusahaan
					And b.Kode_Perusahaan=e.Kode_Perusahaan And b.Kode_barang=e.Kode_Barang And b.kode_stock_owner=e.kode_stock_owner
					And e.Kode_Barang_inq = c.Kode_Barang And c.Kode_Perusahaan = d.Kode_Perusahaan
					And c.Kode_Bahan = d.Kode_Barang And b.Kode_Stock_Owner = d.Kode_Stock_Owner And
					a.Kode_Perusahaan='{KodePerusahaan}' AND a.lokasi= '{ComboBox3.Text}' AND a.tahun = '{tahunSkrng}' and a.bulan = '{bulanSkrng}'
					and b.Flag_Validasi = 'Y' and b.Flag_Validasi_PPIC = 'Y'
					group by d.Kode_Stock_Owner, c.Kode_Bahan, d.Nama

					order by asal, nama

"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows().Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Arrbarang.Add(.Rows(i).Item("kode_barang"))
							Arrlokasi.Add(.Rows(i).Item("kode_stock_owner"))
							ArrNama.Add(.Rows(i).Item("nama"))
						Next
					Else
						CloseConn()
						MessageBox.Show("Tidak ada data forecasting pada bulan " & ComboBox1.Text & " " & ComboBox2.Text)
						Exit Sub
					End If
				End With
			End Using

			Dim ListOfBarang As String = $"'{String.Join("', '", Arrbarang.ToArray())}'"

			DataOutStandingPRD.Clear()
			SQL = "SELECT distinct a.kode_barang, (Gantungan_PO + Gantungan_Split1 + Gantungan_Split2) AS Keep_Stock "
			SQL = SQL & "FROM N_Emi_View_Stock_Gantung a "
			SQL = SQL & "INNER JOIN barang c ON a.kode_barang = c.kode_barang "
			SQL = SQL & "INNER JOIN emi_group_jenis b ON b.id_group_jenis = c.id_group_jenis "
			SQL = SQL & "AND b.kode_perusahaan = c.kode_perusahaan "
			SQL = SQL & "WHERE b.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "AND a.kode_barang IN (" & ListOfBarang & ") "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					DataOutStandingPRD.Add((Dr("kode_barang"), Val(HilangkanTanda(Dr("Keep_Stock")))))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Get_Barang_Rix(BulanAwal, panggil_datatahun, filterSQL2)

	End Sub

	Dim dictSatuanDisplay As New Dictionary(Of String, String)

	Public Sub Get_Barang_Rix(ByVal bulanSaatIni As String, ByVal tahunSaatIni As String, ByVal filterBulan As String)

		Dim akses_ubah As String = "Y"

		'Try
		'    OpenConn()

		'    If CekButtonRole("MRP_PPIC") = "Y" Then
		'        akses_ubah = "Y"
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		Try
			OpenConn()

			'cek pointt terbaru dan bener

			Dim dictPRBulanan As New Dictionary(Of String, Double)
			Dim dictPOBulanan As New Dictionary(Of String, Double)
			Dim dictPRD As New Dictionary(Of String, Double)

			Dim tglAwal As Date = New Date(tahunSaatIni, bulanSaatIni, 1)

			Dim tglAwalStr As String = Format(tglAwal, "yyyy-MM-dd")

			Dim query As String = ""

			'Kalo di bulan berjalan, ambil data2 sebelum juga

			If tgl_skg.Month = bulanSaatIni And tgl_skg.Year = tahunSaatIni Then

				query = "and ((tahun = '" & tahunSkrng & "' and bulan = '" & bulanSkrng & "') or DATEFROMPARTS(tahun, bulan, tanggal)<'" & tglAwalStr & "' ) "
			Else
				query = "and tahun = '" & tahunSkrng & "' and bulan = '" & bulanSkrng & "' "
			End If


			SQL = "SELECT kode_barang,tanggal, bulan, tahun, "
			SQL = SQL & "isnull(SUM(outstanding_po) ,0) AS total_po, "
			SQL = SQL & "isnull(sum(outstanding_pr), 0) as total_pr "
			SQL = SQL & "FROM N_EMI_View_PO_ETA "
			SQL = SQL & "WHERE kode_perusahaan = '" & KodePerusahaan & "' "

			SQL = SQL & query

			SQL = SQL & "GROUP BY kode_barang,tanggal, bulan,  tahun "
			Using ds = BindingTrans(SQL)
				For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1

					Dim r = ds.Tables("MyTable").Rows(i)

					Dim kode As String = r("kode_barang").ToString()
					Dim bulan As String = r("bulan").ToString().PadLeft(2, "0")
					Dim tahun As String = r("tahun").ToString()
					Dim hari As String = r("tanggal").ToString()
					Dim nilai As Double = Val(r("total_po"))
					Dim nilai_out_pr As Double = Val(r("total_pr"))


					Dim tgl As Date = CDate(tahun & "-" & bulan & "-" & hari)
					If tgl_skg > tgl Then
						tahun = tgl_skg.Year
						bulan = Format(tgl_skg.Month, "00")
						hari = Format(tgl_skg.Day, "00")
					End If

					Dim minggu As Integer = GetMinggu(tgl)

					Dim key As String = kode & "|" & bulan & "|" & tahun & "|M" & minggu

					If dictPOBulanan.ContainsKey(key) Then
						dictPOBulanan(key) += nilai
					Else
						dictPOBulanan.Add(key, nilai)
					End If

					If dictPRBulanan.ContainsKey(key) Then
						dictPRBulanan(key) += nilai_out_pr
					Else
						dictPRBulanan.Add(key, nilai_out_pr)
					End If

				Next
			End Using

			' =========================
			' 🔥 PRELOAD 2: Info barang (satuan, good_stock, flag) — SATU QUERY untuk semua barang
			' =========================
			Dim dictBarangInfo As New Dictionary(Of String, (satuan As String, good_stock As Double, flag_packaging As String, flag_raw_material As String))

			If Arrbarang.Count > 0 Then
				Dim inList As String = String.Join(",", Arrbarang.Cast(Of String).Select(Function(k) "'" & k & "'"))
				SQL = "SELECT distinct a.kode_barang, a.satuan, (Stock_Wharehouse+Stock_Unloading) as good_stock, "

				SQL = SQL & "b.Flag_raw_material, b.flag_packaging "

				SQL = SQL & "FROM N_Emi_View_Stock_Gantung a "
				SQL = SQL & "INNER JOIN barang c ON a.kode_barang = c.kode_barang "

				SQL = SQL & "INNER JOIN emi_group_jenis b ON b.id_group_jenis = c.id_group_jenis "
				SQL = SQL & "AND b.kode_perusahaan = b.kode_perusahaan "

				SQL = SQL & "WHERE b.kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "AND a.kode_barang IN (" & inList & ")"

				Using ds = BindingTrans(SQL)
					For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
						Dim row = ds.Tables("MyTable").Rows(i)
						Dim key As String = row("kode_barang").ToString()
						dictBarangInfo(key) = (
						satuan:=row("satuan").ToString(),
						good_stock:=Val(row("good_stock")),
						flag_packaging:=row("flag_packaging").ToString(),
						flag_raw_material:=row("Flag_raw_material").ToString()
					)
					Next
				End Using
			End If

			If Arrbarang.Count > 0 Then
				Dim inList As String = String.Join(",", Arrbarang.Cast(Of String).Select(Function(k) "'" & k & "'"))
				SQL = "SELECT kode_barang, satuan FROM Barang_Detail_Satuan "

				SQL = SQL & "WHERE kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "AND flag_tampil_display = 'Y' "
				SQL = SQL & "AND kode_barang IN (" & inList & ")"

				Using ds = BindingTrans(SQL)
					For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
						Dim row = ds.Tables("MyTable").Rows(i)
						Dim key As String = row("kode_barang").ToString()
						If Not dictSatuanDisplay.ContainsKey(key) Then
							dictSatuanDisplay(key) = row("satuan").ToString()
						End If
					Next
				End Using
			End If

			' =========================
			' 🔥 PRELOAD 4: Ubah_Satuan good_stock — SATU QUERY untuk semua barang
			' Gabungkan good_stock conversion ke satu batch SQL
			' =========================
			Dim dictGoodStockDisplay As New Dictionary(Of String, Double)

			If Arrbarang.Count > 0 Then
				' Bangun UNION ALL untuk semua barang sekaligus
				Dim unionParts As New List(Of String)
				For i As Integer = 0 To Arrbarang.Count - 1
					Dim kb As String = Arrbarang.Item(i)
					If dictBarangInfo.ContainsKey(kb) AndAlso dictSatuanDisplay.ContainsKey(kb) Then
						Dim info = dictBarangInfo(kb)
						Dim satuanDisplay As String = dictSatuanDisplay(kb)
						unionParts.Add(
						"SELECT '" & kb & "' AS kode_barang, " &
						"" & info.good_stock & " AS hasil"
					)
					End If
				Next

				If unionParts.Count > 0 Then
					SQL = String.Join(" UNION ALL ", unionParts)
					Using ds = BindingTrans(SQL)
						For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
							Dim row = ds.Tables("MyTable").Rows(i)
							dictGoodStockDisplay(row("kode_barang").ToString()) = Val(row("hasil"))
						Next
					End Using
				End If
			End If

			Dim listBulan As New List(Of String)
			Dim listTahun As New List(Of String)

			For i As Integer = 0 To 6
				Dim dt As Date = New Date(CInt(tahunSaatIni), CInt(bulanSaatIni), 1).AddMonths(i)
				listBulan.Add(dt.Month.ToString("D2"))
				listTahun.Add(dt.Year.ToString())
			Next

			Dim strBulan As String = "'" & String.Join("','", listBulan) & "'"
			Dim strTahun As String = "'" & String.Join("','", listTahun.Distinct()) & "'"
			' =========================
			' 🔥 PRELOAD 5: NilaiCurrentMonth (CTE formulator) — SATU QUERY untuk SEMUA barang sekaligus
			' =========================

			Dim dictForecastRM As New Dictionary(Of String, Double)
			Dim dictForecastPKG As New Dictionary(Of String, Double)

			Dim dictDataSchedule As New Dictionary(Of String, Double)
			Dim dictDataSchedulePckg As New Dictionary(Of String, Double)

			SQL = "WITH cte AS ( "

			SQL = SQL & "  SELECT DISTINCT ISNULL(f.No_Faktur,'') AS kode_formula, "

			SQL = SQL & "x.kode_barang_inq,x.kode_perusahaan  FROM barang x  INNER JOIN emI_group_jenis y ON x.kode_perusahaan=y.kode_perusahaan AND x.id_group_jenis=y.id_group_jenis "
			SQL = SQL & "OUTER APPLY ( "
			SQL = SQL & "SELECT TOP 1 c.No_Faktur, flag_invalid "
			SQL = SQL & "FROM N_EMI_Transaksi_Formulator_Binding a "
			SQL = SQL & "INNER JOIN N_EMI_Transaksi_Formulator_Binding_Detail b "
			SQL = SQL & "ON a.Kode_Perusahaan=b.Kode_Perusahaan "
			SQL = SQL & "AND a.No_Faktur=b.No_Faktur "
			SQL = SQL & "INNER JOIN Emi_Transaksi_Formulator c "
			SQL = SQL & "ON b.Kode_Perusahaan=c.Kode_Perusahaan "
			SQL = SQL & "AND b.No_Formulator=c.No_Faktur "
			SQL = SQL & "AND c.Status IS NULL "
			SQL = SQL & "WHERE a.Status IS NULL "
			SQL = SQL & " And a.Flag_Validasi_Main ='Y' "
			SQL = SQL & " and c.Flag_Validasi_Formula_Produksi_BOD = 'Y' 	and no_prioritas = 1 "

			SQL = SQL & "AND a.Kode_Perusahaan=x.kode_perusahaan "
			SQL = SQL & "AND a.Kode_Barang=x.kode_barang_inq "
			SQL = SQL & "ORDER BY a.Tanggal DESC,a.Jam DESC,b.No_Prioritas ASC "
			SQL = SQL & ") f "
			SQL = SQL & "WHERE (y.Flag_Finished_Good='Y' OR y.Flag_Semi_FG='Y') and flag_invalid is null "

			SQL = SQL & "), cte_b as( "

			'================= RM =================
			SQL = SQL & " SELECT "
			SQL = SQL & " f.tanggal_start, a.bulan, a.tahun, "
			SQL = SQL & " d.kode_barang AS Kode_Bahan, "
			SQL = SQL & " d.Nilai_Barang, d.satuan_barang, "
			SQL = SQL & " isnull((f.Jumlah - k.Qty_PO- 0) ,f.Jumlah)* bb.Berat / 1000 AS nilai_ppic, "
			SQL = SQL & " c.hasil AS nilai_Formula, "
			SQL = SQL & " 'RM' AS jenis "

			SQL = SQL & " FROM emi_transaksi_sales_forecasting a "
			SQL = SQL & " INNER JOIN emi_transaksi_sales_forecasting_detail b ON a.Kode_Perusahaan=b.kode_Perusahaan AND a.no_faktur=b.no_faktur "
			SQL = SQL & " INNER JOIN barang bb ON b.kode_perusahaan=bb.kode_perusahaan and b.kode_barang=bb.kode_barang and b.kode_stock_owner=bb.kode_stock_owner "
			SQL = SQL & " INNER JOIN cte bc ON bb.kode_perusahaan=bc.kode_perusahaan and bb.kode_barang_inq=bc.kode_barang_inq"
			SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule_Detail f ON f.Kode_Perusahaan=b.Kode_Perusahaan AND f.Urut_Production_Plan=b.urut "
			SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule g ON f.Kode_Perusahaan=g.Kode_Perusahaan AND f.No_Transaksi=g.No_Transaksi "
			SQL = SQL & "  INNER JOIN emi_transaksi_formulator c ON c.Kode_Perusahaan = b.Kode_Perusahaan  "
			SQL = SQL & " AND c.No_Faktur = CASE "

			SQL = SQL & " WHEN f.kode_formula IS NULL "
			SQL = SQL & " THEN bc.Kode_Formula "
			SQL = SQL & "ELSE f.kode_formula end "

			SQL = SQL & " INNER JOIN emi_transaksi_formulator_detail_Bahan d ON c.Kode_Perusahaan=d.Kode_Perusahaan AND c.no_faktur=d.no_faktur "
			SQL = SQL & " INNER JOIN barang z ON d.Kode_Perusahaan=z.Kode_Perusahaan and d.Kode_Barang=z.Kode_Barang and d.Kode_Stock_Owner=z.Kode_Stock_Owner "
			SQL = SQL & " INNER JOIN emi_group_jenis y ON z.Kode_Perusahaan=y.Kode_Perusahaan and z.id_group_jenis=y.id_group_jenis "
			SQL = SQL & " INNER JOIN init e ON a.kode_Perusahaan=e.kode_Perusahaan "

			SQL = SQL & "outer apply ("

			SQL = SQL & " select sum(xyz.Jumlah) as Qty_PO from EMI_Order_Produksi xyz "
			SQL = SQL & "where xyz.Kode_Perusahaan = f.Kode_Perusahaan "
			SQL = SQL & " and xyz.Urut_Production_Schedule = f.No_Urut "
			SQL = SQL & "	 and xyz.Status is null "

			SQL = SQL & ") k "

			'SQL = SQL & "outer apply ("

			'SQL = SQL & " select sum(xyz.Jumlah) as Qty_PO_Formula_Default from EMI_Order_Produksi xyz ,N_EMI_Production_Plan_Schedule_Detail ab "
			'SQL = SQL & "where xyz.Kode_Perusahaan = ab.Kode_Perusahaan "
			'SQL = SQL & " and xyz.Urut_Production_Schedule = ab.No_Urut "
			'SQL = SQL & "and ab.Kode_Perusahaan=b.Kode_Perusahaan AND ab.Urut_Production_Plan=b.urut "
			'SQL = SQL & "	 and xyz.Status is null and ab.kode_formula is null "

			'SQL = SQL & ") l "




			SQL = SQL & " WHERE a.status IS NULL "
			SQL = SQL & " AND b.Kode_Perusahaan='" & KodePerusahaan & "' "
			SQL = SQL & " AND a.bulan='" & bulanSkrng & "' AND a.tahun='" & tahunSkrng & "' "
			SQL = SQL & " AND b.flag_validasi='Y' AND b.flag_validasi_PPIC='Y' and flag_sudah_production_order is null "
			SQL = SQL & " AND c.status IS NULL AND g.Status IS NULL and y.flag_raw_material='Y' "

			SQL = SQL & " UNION ALL "

			'================= PACKAGING =================
			SQL = SQL & " SELECT "
			SQL = SQL & " x.Tanggal_Start, a.bulan, a.tahun, "
			SQL = SQL & " c.Kode_Bahan, "
			SQL = SQL & " c.Jumlah_Bahan AS Nilai_Barang, "
			SQL = SQL & " b.satuan AS satuan_barang, "
			SQL = SQL & " x.Jumlah AS nilai_ppic, "
			SQL = SQL & " c.jumlah_barang AS nilai_Formula, "
			SQL = SQL & " 'PKG' AS jenis "

			SQL = SQL & " FROM emi_transaksi_sales_forecasting a, "
			SQL = SQL & " emi_transaksi_sales_forecasting_detail b, "
			SQL = SQL & " N_EMI_Production_Plan_Schedule_Detail x, "
			SQL = SQL & " N_EMI_Production_Plan_Schedule y, "
			SQL = SQL & " barang_detail_bahan_penolong c, "
			SQL = SQL & " barang d "

			SQL = SQL & " WHERE a.Kode_Perusahaan = b.kode_Perusahaan "
			SQL = SQL & " AND a.no_faktur = b.no_faktur "
			SQL = SQL & " AND a.status IS NULL "
			SQL = SQL & " AND b.Kode_Perusahaan = '" & KodePerusahaan & "' "

			SQL = SQL & " AND x.Kode_Perusahaan = d.Kode_Perusahaan "
			SQL = SQL & " AND x.Kode_barang = d.Kode_Barang "
			SQL = SQL & " AND x.kode_stock_owner = d.kode_stock_owner "

			SQL = SQL & " AND b.Kode_Perusahaan = x.Kode_Perusahaan "
			SQL = SQL & " AND b.urut = x.Urut_Production_Plan "

			SQL = SQL & " AND x.Kode_Perusahaan = y.Kode_Perusahaan "
			SQL = SQL & " AND x.No_Transaksi = y.No_Transaksi "
			SQL = SQL & " AND y.Status IS NULL "

			SQL = SQL & " AND a.bulan = '" & bulanSkrng & "' "
			SQL = SQL & " AND a.tahun = '" & tahunSkrng & "' "

			SQL = SQL & " AND b.flag_validasi = 'Y' "
			SQL = SQL & " AND b.flag_validasi_PPIC = 'Y' "

			SQL = SQL & " AND d.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & " AND d.kode_barang_inq = c.kode_barang "

			SQL = SQL & " ) "

			'================= FINAL =================
			SQL = SQL & " SELECT "
			SQL = SQL & " cte.tanggal_start, cte.Kode_Bahan, cte.bulan, cte.tahun, cte.jenis, "
			SQL = SQL & " ISNULL(SUM(ROUND(cte.Nilai_Barang*(cte.nilai_ppic/cte.nilai_Formula),2)),0) AS Nilai "

			SQL = SQL & " FROM cte_b as cte "
			SQL = SQL & " GROUP BY cte.Kode_Bahan, cte.bulan, cte.tahun, cte.tanggal_start, cte.jenis "

			Using Ds = BindingTrans(SQL)
				For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
					Dim r = Ds.Tables("MyTable").Rows(i)

					Dim kode = r("Kode_Bahan").ToString()
					Dim bulan = r("bulan").ToString().PadLeft(2, "0")
					Dim tahun = r("tahun").ToString()
					Dim nilai = Val(r("Nilai"))
					Dim jenis = r("jenis").ToString()

					Dim tgl As Date = CDate(r("tanggal_start"))
					Dim minggu As Integer = GetMinggu(tgl)

					Dim key = kode & "|" & bulan & "|" & tahun & "|M" & minggu

					If dictDataSchedule.ContainsKey(key) Then
						dictDataSchedule(key) += nilai
					Else
						dictDataSchedule.Add(key, nilai)
					End If
				Next
			End Using

			'Dim dictDataSchedule As New Dictionary(Of String, Double)
			'Dim dictDataSchedulePckg As New Dictionary(Of String, Double)

			''============Simpan data ke dictionary untuk data schedule ==========
			''============Perbulan  =========================================
			'SQL = "WITH cte AS ( "

			'SQL = SQL & " SELECT f.tanggal_start,a.bulan,a.tahun,b.kode_Barang AS kode_fg, d.kode_barang AS Kode_Bahan, d.Nilai_Barang, d.satuan_barang,"
			'SQL = SQL & " f.Kode_Formula,"
			'SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,f.Jumlah) AS nilai_ppic,"
			'SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil) AS nilai_Formula"

			'SQL = SQL & " FROM emi_transaksi_sales_forecasting a"
			'SQL = SQL & " INNER JOIN emi_transaksi_sales_forecasting_detail b ON a.Kode_Perusahaan=b.kode_Perusahaan AND a.no_faktur=b.no_faktur"
			'SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule_Detail f ON f.Kode_Perusahaan=b.Kode_Perusahaan AND f.Urut_Production_Plan=b.urut"
			'SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule g ON f.Kode_Perusahaan=g.Kode_Perusahaan AND f.No_Transaksi=g.No_Transaksi"
			'SQL = SQL & " INNER JOIN emi_transaksi_formulator c ON f.Kode_Perusahaan=c.Kode_Perusahaan AND f.Kode_Formula=c.No_Faktur"
			'SQL = SQL & " INNER JOIN emi_transaksi_formulator_detail_Bahan d ON c.Kode_Perusahaan=d.Kode_Perusahaan AND c.no_faktur=d.no_faktur"
			'SQL = SQL & " INNER JOIN init e ON a.kode_Perusahaan=e.kode_Perusahaan"

			'SQL = SQL & " WHERE a.status IS NULL AND b.Kode_Perusahaan='" & KodePerusahaan & "'"

			'SQL = SQL & "and a.tahun = '" & tahunSkrng & " ' and a.bulan = '" & bulanSkrng & "' "
			'SQL = SQL & " AND b.flag_validasi='Y' AND b.flag_validasi_PPIC='Y' AND c.status IS NULL AND g.Status IS NULL"

			'SQL = SQL & " )"

			'SQL = SQL & " SELECT cte.tanggal_start, cte.Kode_Bahan, cte.satuan_barang, cte.bulan, cte.tahun,"
			'SQL = SQL & " ISNULL(SUM(ROUND(cte.Nilai_Barang*(cte.nilai_ppic/cte.nilai_Formula) ,2)),0) AS Nilai,"
			'SQL = SQL & " bds.satuan AS satuan_display"

			'SQL = SQL & " FROM cte "
			'SQL = SQL & " INNER JOIN Barang_Detail_Satuan bds ON bds.kode_barang = cte.Kode_Bahan"
			'SQL = SQL & " AND bds.kode_perusahaan = '" & KodePerusahaan & "' AND bds.flag_tampil_display = 'Y'"

			'SQL = SQL & " GROUP BY cte.Kode_Bahan, cte.satuan_barang, cte.bulan, cte.tahun, bds.satuan, cte.tanggal_start "

			'Using Ds = BindingTrans(SQL)
			'    For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
			'        Dim r = Ds.Tables("MyTable").Rows(i)

			'        Dim kode As String = r("Kode_Bahan").ToString()
			'        Dim bulan As String = r("bulan").ToString().PadLeft(2, "0")
			'        Dim tahun As String = r("tahun").ToString()
			'        Dim nilai As Double = Val(r("Nilai"))

			'        Dim tgl As Date = CDate(r("tanggal_start"))
			'        Dim minggu As Integer = GetMinggu(tgl)

			'        Dim key As String = kode & "|" & bulan & "|" & tahun & "|M" & minggu

			'        If dictDataSchedule.ContainsKey(key) Then
			'            dictDataSchedule(key) += nilai
			'        Else
			'            dictDataSchedule.Add(key, nilai)
			'        End If
			'    Next
			'End Using

			''============Simpan data ke dictionary untuk packaging untuk data schedule ==========
			''============Perbulan  =========================================
			'SQL = "WITH cte AS ( "

			'SQL = SQL & " SELECT "
			'SQL = SQL & " x.Tanggal_Start, a.bulan, a.tahun, "
			'SQL = SQL & " b.kode_Barang AS kode_fg, "
			'SQL = SQL & " c.Kode_Bahan, "
			'SQL = SQL & " c.Jumlah_Bahan AS Nilai_Barang, "
			'SQL = SQL & " x.Jumlah AS nilai_ppic, "
			'SQL = SQL & " c.jumlah_barang AS nilai_Formula, "
			'SQL = SQL & " b.satuan AS satuan_barang "

			'SQL = SQL & " FROM emi_transaksi_sales_forecasting a, "
			'SQL = SQL & " emi_transaksi_sales_forecasting_detail b, "
			'SQL = SQL & " N_EMI_Production_Plan_Schedule_Detail x, "
			'SQL = SQL & " N_EMI_Production_Plan_Schedule y, "
			'SQL = SQL & " barang_detail_bahan_penolong c, "
			'SQL = SQL & " barang d "

			'SQL = SQL & " WHERE a.Kode_Perusahaan = b.kode_Perusahaan "
			'SQL = SQL & " AND a.no_faktur = b.no_faktur "
			'SQL = SQL & " AND a.status IS NULL "
			'SQL = SQL & " AND b.Kode_Perusahaan = '" & KodePerusahaan & "' "

			'SQL = SQL & " AND x.Kode_Perusahaan = d.Kode_Perusahaan "
			'SQL = SQL & " AND x.Kode_barang = d.Kode_Barang "
			'SQL = SQL & " AND x.kode_stock_owner = d.kode_stock_owner "

			'SQL = SQL & " AND b.Kode_Perusahaan = x.Kode_Perusahaan "
			'SQL = SQL & " AND b.urut = x.Urut_Production_Plan "

			'SQL = SQL & " AND x.Kode_Perusahaan = y.Kode_Perusahaan "
			'SQL = SQL & " AND x.No_Transaksi = y.No_Transaksi "
			'SQL = SQL & " AND y.Status IS NULL "

			'SQL = SQL & " AND a.bulan = '" & bulanSkrng & "' "
			'SQL = SQL & " AND a.tahun = '" & tahunSkrng & "' "

			'SQL = SQL & " AND b.flag_validasi = 'Y' "
			'SQL = SQL & " AND b.flag_validasi_PPIC = 'Y' "

			'SQL = SQL & " AND d.Kode_Perusahaan = c.Kode_Perusahaan "
			'SQL = SQL & " AND d.kode_barang_inq = c.kode_barang "

			'SQL = SQL & " ) "

			'SQL = SQL & " SELECT "
			'SQL = SQL & " cte.Kode_Bahan, "
			'SQL = SQL & " cte.satuan_barang, "
			'SQL = SQL & " cte.bulan, "
			'SQL = SQL & " cte.tahun, "
			'SQL = SQL & " cte.tanggal_start, "
			'SQL = SQL & " ISNULL(SUM(ROUND(cte.Nilai_Barang * (cte.nilai_ppic / cte.nilai_Formula), 2)), 0) AS Nilai, "
			'SQL = SQL & " bds.satuan AS satuan_display "

			'SQL = SQL & " FROM cte "
			'SQL = SQL & " INNER JOIN Barang_Detail_Satuan bds "
			'SQL = SQL & " ON bds.kode_barang = cte.Kode_Bahan "
			'SQL = SQL & " AND bds.kode_perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & " AND bds.flag_tampil_display = 'Y' "

			'SQL = SQL & " GROUP BY "
			'SQL = SQL & " cte.Kode_Bahan, "
			'SQL = SQL & " cte.satuan_barang, "
			'SQL = SQL & " cte.bulan, "
			'SQL = SQL & " cte.tahun, "
			'SQL = SQL & " bds.satuan, "
			'SQL = SQL & " cte.tanggal_start "

			'Using Ds = BindingTrans(SQL)
			'    For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
			'        Dim r = Ds.Tables("MyTable").Rows(i)

			'        Dim kode As String = r("Kode_Bahan").ToString()
			'        Dim bulan As String = r("bulan").ToString().PadLeft(2, "0")
			'        Dim tahun As String = r("tahun").ToString()
			'        Dim nilai As Double = Val(r("Nilai"))

			'        Dim tgl As Date = CDate(r("tanggal_start"))
			'        Dim minggu As Integer = GetMinggu(tgl)

			'        Dim key As String = kode & "|" & bulan & "|" & tahun & "|M" & minggu

			'        If dictDataSchedulePckg.ContainsKey(key) Then
			'            dictDataSchedulePckg(key) += nilai
			'        Else
			'            dictDataSchedulePckg.Add(key, nilai)
			'        End If
			'    Next
			'End Using

			'    SQL = "WITH cte AS ( "

			'    SQL = SQL & " SELECT a.bulan,a.tahun,b.kode_Barang AS kode_fg, d.kode_barang AS Kode_Bahan, d.Nilai_Barang, d.satuan_barang,"
			'    SQL = SQL & " b.Kode_Formula,"
			'    SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic-total_qty) AS nilai_ppic,"
			'    SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil) AS nilai_Formula"

			'    SQL = SQL & " FROM emi_transaksi_sales_forecasting a"
			'    SQL = SQL & " INNER JOIN emi_transaksi_sales_forecasting_detail b ON a.Kode_Perusahaan=b.kode_Perusahaan AND a.no_faktur=b.no_faktur"
			'    SQL = SQL & " INNER JOIN emi_transaksi_formulator c ON b.Kode_Perusahaan=c.Kode_Perusahaan AND b.Kode_Formula=c.No_faktur"
			'    SQL = SQL & " INNER JOIN emi_transaksi_formulator_detail_Bahan d ON c.Kode_Perusahaan=d.Kode_Perusahaan AND c.no_faktur=d.no_faktur"
			'    SQL = SQL & " INNER JOIN init e ON a.kode_Perusahaan=e.kode_Perusahaan"

			'    SQL = SQL & " OUTER APPLY (SELECT isnull(SUM(f.Jumlah),0) AS total_qty FROM N_EMI_Production_Plan_Schedule_Detail f,  "
			'    SQL = SQL & " N_EMI_Production_Plan_Schedule g "
			'    SQL = SQL & "WHERE f.No_Transaksi = g.No_Transaksi and f.Kode_Perusahaan = g.Kode_Perusahaan "
			'    SQL = SQL & "and g.Status is null "
			'    SQL = SQL & " and   f.Kode_Perusahaan=b.Kode_Perusahaan AND f.Urut_Production_Plan=b.urut) f_sum"

			'    SQL = SQL & " WHERE a.status IS NULL AND b.Kode_Perusahaan='" & KodePerusahaan & "' "

			'    SQL = SQL & "and a.tahun = '" & tahunSkrng & " ' and a.bulan = '" & bulanSkrng & "' "

			'    SQL = SQL & " AND b.flag_validasi='Y' AND b.flag_validasi_PPIC='Y' AND c.status IS NULL"

			'    SQL = SQL & " )"

			'    SQL = SQL & " SELECT cte.Kode_Bahan, cte.satuan_barang, cte.bulan, cte.tahun,"
			'    SQL = SQL & " ISNULL(SUM(ROUND(cte.Nilai_Barang*(cte.nilai_ppic/cte.nilai_Formula) ,2)),0) AS Nilai,"
			'    SQL = SQL & " bds.satuan AS satuan_display"

			'    SQL = SQL & " FROM cte"
			'    SQL = SQL & " INNER JOIN Barang_Detail_Satuan bds ON bds.kode_barang = cte.Kode_Bahan"
			'    SQL = SQL & " AND bds.kode_perusahaan = '" & KodePerusahaan & "' AND bds.flag_tampil_display = 'Y'"

			'    SQL = SQL & " GROUP BY cte.Kode_Bahan, cte.satuan_barang, cte.bulan, cte.tahun, bds.satuan"
			'    Dim listRM As New List(Of (kb As String, sb As String, sd As String, bln As String, thn As String, nilai As Double))

			'    Using ds = BindingTrans(SQL)
			'        For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
			'            Dim r = ds.Tables("MyTable").Rows(i)

			'            listRM.Add((
			'    kb:=r("Kode_Bahan").ToString(),
			'    sb:=r("satuan_barang").ToString(),
			'    sd:=r("satuan_display").ToString(),
			'    bln:=r("bulan").ToString().PadLeft(2, "0"),
			'    thn:=r("tahun").ToString(),
			'    nilai:=Val(r("Nilai"))
			'))
			'        Next
			'    End Using

			'    If listRM.Count > 0 Then
			'        Dim unionConv As New List(Of String)

			'        For i As Integer = 0 To listRM.Count - 1
			'            Dim r = listRM(i)

			'            unionConv.Add(
			'    "SELECT '" & r.kb & "' kode_bahan,'" & r.bln & "' bulan,'" & r.thn & "' tahun," &
			'    "dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & r.kb & "','" &
			'    r.sb & "','" & r.sd & "'," & r.nilai & ") hasil"
			')
			'        Next

			'        SQL = String.Join(" UNION ALL ", unionConv)

			'        Using ds = BindingTrans(SQL)
			'            For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
			'                Dim row = ds.Tables("MyTable").Rows(i)

			'                Dim key = row("kode_bahan").ToString() & "|" &
			'              row("bulan").ToString() & "|" &
			'              row("tahun").ToString()

			'                If dictForecastRM.ContainsKey(key) Then
			'                    dictForecastRM(key) += Val(row("hasil"))
			'                Else
			'                    dictForecastRM(key) = Val(row("hasil"))
			'                End If
			'            Next
			'        End Using
			'    End If

			'    SQL = ";WITH cte AS ("

			'    SQL = SQL & " SELECT a.bulan,a.tahun, b.kode_Barang AS kode_fg, c.Kode_Bahan, c.Jumlah_Bahan AS Nilai_Barang,"
			'    SQL = SQL & " b.nilai_ppic AS nilai_ppic2, c.jumlah_barang AS nilai_Formula, b.satuan AS satuan_barang , "

			'    SQL = SQL & "isnull(( SELECT isnull(b.nilai_ppic,0) - isnull(SUM(f.Jumlah), 0)  FROM "
			'    SQL = SQL & "N_EMI_Production_Plan_Schedule_Detail f, N_EMI_Production_Plan_Schedule g "
			'    SQL = SQL & "WHERE f.No_Transaksi = g.No_Transaksi and f.Kode_Perusahaan = g.Kode_Perusahaan "
			'    SQL = SQL & " and g.Status is null "
			'    SQL = SQL & "and f.Kode_Perusahaan = b.Kode_Perusahaan  AND f.Urut_Production_Plan = b.urut "
			'    SQL = SQL & " ),0) as nilai_ppic "

			'    SQL = SQL & " FROM emi_transaksi_sales_forecasting a,"
			'    SQL = SQL & " emi_transaksi_sales_forecasting_detail b,"
			'    SQL = SQL & " barang_detail_bahan_penolong c, barang d"

			'    SQL = SQL & " WHERE a.Kode_Perusahaan = b.kode_Perusahaan AND a.no_faktur = b.no_faktur"
			'    SQL = SQL & " AND a.status IS NULL AND b.Kode_Perusahaan = '" & KodePerusahaan & "'"

			'    SQL = SQL & " AND b.Kode_Perusahaan=d.Kode_Perusahaan AND b.Kode_barang=d.Kode_Barang AND b.kode_stock_owner=d.kode_stock_owner "

			'    SQL = SQL & "and a.tahun = '" & tahunSkrng & " ' and a.bulan = '" & bulanSkrng & "' "

			'    SQL = SQL & " AND b.flag_validasi='Y' AND b.flag_validasi_PPIC='Y'"

			'    SQL = SQL & " AND d.Kode_Perusahaan = c.Kode_Perusahaan AND d.kode_barang_inq = c.kode_barang"

			'    SQL = SQL & " )"

			'    SQL = SQL & " SELECT cte.Kode_Bahan, cte.satuan_barang, cte.bulan, cte.tahun,"
			'    SQL = SQL & " ISNULL(SUM(ROUND(cte.Nilai_Barang*(cte.nilai_ppic/cte.nilai_Formula)  ,2)),0) AS Nilai,"
			'    SQL = SQL & " bds.satuan AS satuan_display"

			'    SQL = SQL & " FROM cte"

			'    SQL = SQL & " INNER JOIN Barang_Detail_Satuan bds ON bds.kode_barang = cte.Kode_Bahan"
			'    SQL = SQL & " AND bds.kode_perusahaan = '" & KodePerusahaan & "' AND bds.flag_tampil_display = 'Y'"

			'    SQL = SQL & " GROUP BY cte.Kode_Bahan, cte.satuan_barang, cte.bulan, cte.tahun, bds.satuan"

			'    Dim listPKG As New List(Of (kb As String, sb As String, sd As String, bln As String, thn As String, nilai As Double))

			'    Using ds = BindingTrans(SQL)
			'        For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
			'            Dim r = ds.Tables("MyTable").Rows(i)

			'            listPKG.Add((
			'                    kb:=r("Kode_Bahan").ToString(),
			'                    sb:=r("satuan_barang").ToString(),
			'                    sd:=r("satuan_display").ToString(),
			'                    bln:=r("bulan").ToString().PadLeft(2, "0"),
			'                    thn:=r("tahun").ToString(),
			'                    nilai:=Val(r("Nilai"))
			'                ))
			'        Next
			'    End Using

			'    If listPKG.Count > 0 Then
			'        Dim unionConv As New List(Of String)

			'        For i As Integer = 0 To listPKG.Count - 1
			'            Dim r = listPKG(i)

			'            unionConv.Add(
			'                    "SELECT '" & r.kb & "' kode_bahan,'" & r.bln & "' bulan,'" & r.thn & "' tahun," &
			'                    "dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & r.kb & "','" &
			'                    r.sb & "','" & r.sd & "'," & r.nilai & ") hasil"
			'                )
			'        Next

			'        SQL = String.Join(" UNION ALL ", unionConv)

			'        Using ds = BindingTrans(SQL)
			'            For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
			'                Dim row = ds.Tables("MyTable").Rows(i)

			'                Dim key = row("kode_bahan").ToString() & "|" &
			'                              row("bulan").ToString() & "|" &
			'                              row("tahun").ToString()

			'                dictForecastPKG(key) = Val(row("hasil"))
			'            Next
			'        End Using
			'    End If

			' baru

			Dim listErrorSatuan As New List(Of String)

			SQL = $"

					WITH cte AS (

					select distinct isnull((select top 1 c.No_Faktur
					from N_EMI_Transaksi_Formulator_Binding a
					inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join Emi_Transaksi_Formulator c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Formulator = c.No_Faktur and c.Status is null
					where a.Status is NULL
					and a.Flag_Validasi_Main = 'Y'
					and c.Flag_Validasi_Formula_Produksi_BOD = 'Y'
					and c.Flag_Deprecated_Binding is null
					and no_prioritas = 1
					and a.Kode_Perusahaan = x.kode_perusahaan
					and a.Kode_Barang = x.kode_barang_inq

					order by a.Tanggal DESC, a.Jam DESC, b.No_Prioritas ASC),'') as kode_formula, x.kode_barang_inq, x.kode_perusahaan
					from barang x, emI_group_jenis y where x.kode_perusahaan=y.kode_perusahaan and x.id_group_jenis=y.id_group_jenis and (y.Flag_Finished_Good='Y' or y.Flag_Semi_FG='Y')
				
					), cte_b as(

				   SELECT a.bulan,a.tahun, d.kode_barang AS Kode_Bahan, d.Nilai_Barang, d.satuan_barang,
				   (b.nilai_ppic - f_sum_beda_formula.total_qty_beda_formula - f_sum_sudah_po.total_sudah_po)*bb.Berat/1000 AS nilai_ppic,
				   c.hasil AS nilai_Formula

				   FROM emi_transaksi_sales_forecasting a
				   INNER JOIN emi_transaksi_sales_forecasting_detail b ON a.Kode_Perusahaan=b.kode_Perusahaan AND a.no_faktur=b.no_faktur
				   INNER JOIN barang bb ON b.kode_perusahaan=bb.kode_perusahaan and b.kode_barang=bb.kode_barang and b.kode_stock_owner=bb.kode_stock_owner
				   INNER JOIN cte bc ON bb.kode_perusahaan=bc.kode_perusahaan and bb.kode_barang_inq=bc.kode_barang_inq
				   INNER JOIN Emi_Transaksi_Formulator c ON c.Kode_Perusahaan = bc.Kode_Perusahaan AND c.No_Faktur =  bc.Kode_Formula
				   INNER JOIN emi_transaksi_formulator_detail_Bahan d ON c.Kode_Perusahaan=d.Kode_Perusahaan AND c.no_faktur=d.no_faktur
				   INNER JOIN barang z ON d.Kode_Perusahaan=z.Kode_Perusahaan and d.Kode_Barang=z.Kode_Barang and d.Kode_Stock_Owner=z.Kode_Stock_Owner
				   INNER JOIN emi_group_jenis y ON z.Kode_Perusahaan=y.Kode_Perusahaan and z.id_group_jenis=y.id_group_jenis
				   INNER JOIN init e ON a.kode_Perusahaan=e.kode_Perusahaan


														   OUTER APPLY (SELECT ISNULL(SUM(f.Jumlah - ISNULL(po.Jumlah_PO, 0)), 0) AS total_qty_beda_formula
                                     FROM N_EMI_Production_Plan_Schedule_Detail f
                                              INNER JOIN N_EMI_Production_Plan_Schedule g
                                                         ON f.No_Transaksi = g.No_Transaksi AND
                                                            f.Kode_Perusahaan = g.Kode_Perusahaan

															  LEFT JOIN (SELECT Urut_Production_Schedule,
																				Kode_Perusahaan,
																				SUM(Jumlah) AS Jumlah_PO
																		 FROM EMI_Order_Produksi
																		 WHERE Status IS NULL
																		 GROUP BY Urut_Production_Schedule, Kode_Perusahaan) po
																		ON po.Urut_Production_Schedule = f.No_Urut AND
																		   po.Kode_Perusahaan = f.Kode_Perusahaan

													 WHERE g.Status IS NULL
													  and  f.kode_formula is not null
													   AND f.Kode_Perusahaan = b.Kode_Perusahaan
													   AND f.Urut_Production_Plan = b.urut) f_sum_beda_formula

										OUTER APPLY (select isnull(sum(y.Jumlah), 0) as total_sudah_po
													 from N_EMI_Production_Plan_Schedule_Detail x,
														  emi_order_produksi y,
														  n_emi_production_plan_schedule z
													 where x.Urut_Production_Plan = b.urut
													   and x.Kode_Perusahaan = y.Kode_Perusahaan
													   and x.No_Urut = y.Urut_Production_Schedule
													   and y.Status is null
													   and x.Urut_Production_Plan = b.urut
													   and x.Kode_Perusahaan = z.Kode_Perusahaan
													   and x.No_Transaksi = z.No_Transaksi
													   and y.Status is null) f_sum_sudah_po


				   WHERE a.status IS NULL AND b.Kode_Perusahaan='{KodePerusahaan}'
				   AND a.tahun = '{tahunSkrng}' AND a.bulan = '{bulanSkrng}'
				   AND b.flag_validasi='Y' AND b.flag_validasi_PPIC='Y' AND c.status IS NULL and y.Flag_raw_material='Y'

				   UNION ALL

				   SELECT a.bulan,a.tahun, c.Kode_Bahan, c.Jumlah_Bahan AS Nilai_Barang, b.satuan AS satuan_barang,
				   isnull(( SELECT isnull(b.nilai_ppic,0) - isnull(SUM(f.Jumlah),0)
				   FROM N_EMI_Production_Plan_Schedule_Detail f, N_EMI_Production_Plan_Schedule g
				   WHERE f.No_Transaksi = g.No_Transaksi AND f.Kode_Perusahaan = g.Kode_Perusahaan
				   AND g.Status is null
				   AND f.Kode_Perusahaan=b.Kode_Perusahaan AND f.Urut_Production_Plan=b.urut),0) AS nilai_ppic,
				   c.jumlah_barang AS nilai_Formula

				   FROM emi_transaksi_sales_forecasting a,
				   emi_transaksi_sales_forecasting_detail b,
				   barang_detail_bahan_penolong c, barang d

				   WHERE a.Kode_Perusahaan = b.kode_Perusahaan AND a.no_faktur = b.no_faktur
				   AND a.status IS NULL AND b.Kode_Perusahaan = '{KodePerusahaan}'
				   AND b.Kode_Perusahaan=d.Kode_Perusahaan AND b.Kode_barang=d.Kode_Barang AND b.kode_stock_owner=d.kode_stock_owner
				   AND a.tahun = '{tahunSkrng}' AND a.bulan = '{bulanSkrng}'
				   AND b.flag_validasi='Y' AND b.flag_validasi_PPIC='Y'
				   AND d.Kode_Perusahaan = c.Kode_Perusahaan AND d.kode_barang_inq = c.kode_barang

				   )
				   SELECT cte.Kode_Bahan, cte.bulan, cte.tahun,
				   SUM(
				   ROUND(cte.Nilai_Barang*(cte.nilai_ppic/cte.nilai_Formula),2)) AS hasil,

				   CASE WHEN bds.satuan IS NULL THEN 1 ELSE 0 END AS flag_satuan_kosong

				   FROM cte_b as cte
				   LEFT JOIN Barang_Detail_Satuan bds ON bds.kode_barang = cte.Kode_Bahan
				   AND bds.kode_perusahaan = '{KodePerusahaan}' AND bds.flag_tampil_display = 'Y'

				   GROUP BY cte.Kode_Bahan, cte.bulan, cte.tahun,bds.satuan

			"

			'================ RM =================

			Dim dictForecast As New Dictionary(Of String, Double)

			Using ds = BindingTrans(SQL)
				For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
					Dim row = ds.Tables("MyTable").Rows(i)

					If row("flag_satuan_kosong").ToString() = "1" Then
						listErrorSatuan.Add(row("Kode_Bahan").ToString())
					End If

					Dim key = row("Kode_Bahan").ToString() & "|" &
				  row("bulan").ToString().PadLeft(2, "0") & "|" &
				  row("tahun").ToString()

					dictForecast(key) = Val(row("hasil"))
				Next
			End Using

			If listErrorSatuan.Count > 0 Then

				' hapus duplikat (penting!)
				Dim uniqueError = listErrorSatuan.Distinct().ToList()

				MessageBox.Show("Satuan belum di set untuk barang: " & vbCrLf &
					String.Join(vbCrLf, uniqueError),
					"Warning",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning)

				Exit Sub
			End If

			For indexxx = 0 To Arrbarang.Count - 1
				DataGridView1.Rows.Add(1)
				Dim ind As Integer = DataGridView1.Rows.Count - 1

				Dim satuan_barang As String = ""
				Dim good_stock As Double = 0
				Dim Flag_Packaging As String = ""
				Dim Flag_Raw_Material As String = ""

				Dim ada_data As String = ""
				Dim FValidasi As String = ""

				Dim kodeBarang As String = Arrbarang.Item(indexxx)

				' Validasi data ada
				If Not dictBarangInfo.ContainsKey(kodeBarang) Then
					MessageBox.Show("Data tidak ada untuk: " & kodeBarang)
					Exit Sub
				End If
				If Not dictSatuanDisplay.ContainsKey(kodeBarang) Then
					MessageBox.Show("Barang detail satuan belum di set!")
					Exit Sub
				End If

				Dim info = dictBarangInfo(kodeBarang)
				Dim convertKesatuanDisplay As String = dictSatuanDisplay(kodeBarang)
				Dim good_stock_tampil_display As Double = If(dictGoodStockDisplay.ContainsKey(kodeBarang), dictGoodStockDisplay(kodeBarang), 0)

				Dim key = kodeBarang & "|" & bulanSaatIni & "|" & tahunSaatIni

				Dim nilai As Double = 0

				'If info.flag_raw_material = "Y" Then
				'    If dictForecastRM.ContainsKey(key) Then
				'        nilai = dictForecastRM(key)
				'    End If
				'ElseIf info.flag_packaging = "Y" Then
				'    If dictForecastPKG.ContainsKey(key) Then
				'        nilai = dictForecastPKG(key)
				'    End If
				'End If
				If dictForecast.ContainsKey(key) Then
					nilai = dictForecast(key)
				End If

				'Next

				' Assign ke grid
				DataGridView1.Rows(ind).Cells(cell_BelumTerjadwal).Value = Format(nilai, "N2")
				DataGridView1.Rows(ind).Cells(CellKd_Barang).Value = kodeBarang
				DataGridView1.Rows(ind).Cells(CellNm_Barang).Value = ArrNama.Item(indexxx)
				DataGridView1.Rows(ind).Cells(cell_Satuan).Value = convertKesatuanDisplay

				Dim data = ListEndingStock.Find(Function(x) x.Kode_Bahan.Trim = kodeBarang.Trim And x.Bulan.Trim = bulanSkrng.Trim And x.Jenis.Trim = JenisEndingStock.Trim)
				If Not data.Equals(Nothing) Then
					DataGridView1.Rows(ind).Cells(cellEndingStock).Value = Format(Val(HilangkanTanda(data.Jumlah)), "N2")
				Else
					DataGridView1.Rows(ind).Cells(cellEndingStock).Value = Format(0, "N2")
				End If

				Dim totalPRD As Double = If(dictPRD.ContainsKey(kodeBarang), dictPRD(kodeBarang), 0)

				If BulanKe = 1 Then
					Dim DataOutStanding = DataOutStandingPRD.Find(Function(x) x.KdBahan = kodeBarang)
					If Not DataOutStanding.Equals(Nothing) Then
						DataGridView1.Rows(ind).Cells(cellOutStandingPRD).Value = Format(Val(HilangkanTanda(DataOutStanding.Jumlah)), "N2")
					Else
						DataGridView1.Rows(ind).Cells(cellOutStandingPRD).Value = Format(0, "N2")
					End If
				Else
					DataGridView1.Rows(ind).Cells(cellOutStandingPRD).Value = Format(0, "N2")
				End If

				Dim ActualStock As Double = Val(HilangkanTanda(DataGridView1.Rows(ind).Cells(cellEndingStock).Value))
				Dim OutStanding As Double = Val(HilangkanTanda(DataGridView1.Rows(ind).Cells(cellOutStandingPRD).Value))
				Dim BelumTerjadwal As Double = Val(HilangkanTanda(DataGridView1.Rows(ind).Cells(cell_BelumTerjadwal).Value))

				Dim ending_stock As Double = ActualStock - OutStanding '- BelumTerjadwal
				For m As Integer = 1 To 4

					' === ambil cell schedule (M1–M4)
					Dim cellM As Integer = CType(Me.GetType().GetField("cell_M" & m, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)

					' === ambil cell PO (PO_M1–PO_M4)
					Dim cellPO As Integer = CType(Me.GetType().GetField("cell_POM" & m, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)

					' === ambil cellRequest Stok
					Dim cellReq As Integer = CType(Me.GetType().GetField("cell_ReqStokM" & m, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)

					' === ambil cell PR Income
					Dim cellPrIncom As Integer = CType(Me.GetType().GetField("Cell_PRIncomM" & m, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)

					Dim cellPisah As Integer = CType(Me.GetType().GetField("cellPemisah" & m, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)

					Dim keyNew As String = kodeBarang & "|" & bulanSaatIni & "|" & tahunSaatIni & "|M" & m

					' ambil nilai schedule
					Dim valM As Double = If(dictDataSchedule.ContainsKey(keyNew), dictDataSchedule(keyNew), 0)

					' ambil nilai PO
					Dim valPO As Double = If(dictPOBulanan.ContainsKey(keyNew), dictPOBulanan(keyNew), 0)

					' ambil nilai PR
					Dim valPR As Double = If(dictPRBulanan.ContainsKey(keyNew), dictPRBulanan(keyNew), 0)

					' hitung request stok
					'Dim req As Double = valM - valPO

					' isi ke grid
					DataGridView1.Rows(ind).Cells(cellM).Value = Format(valM, "N2")
					DataGridView1.Rows(ind).Cells(cellPO).Value = Format(valPO, "N2")
					DataGridView1.Rows(ind).Cells(cellPrIncom).Value = Format(valPR, "N2")



					Dim HitungEndingStock As Double = (ending_stock + valPO + valPR) - valM

					ending_stock = HitungEndingStock
					DataGridView1.Rows(ind).Cells(cellReq).Value = Format(HitungEndingStock, "N2")

					If ind = 6 Then
						Dim asdas As String = ""
					End If

					' === warna selang seling
					If m Mod 2 = 1 Then ' M1, M3
						DataGridView1.Rows(ind).Cells(cellM).Style.BackColor = Color.LightYellow
						DataGridView1.Rows(ind).Cells(cellPO).Style.BackColor = Color.LightYellow
						DataGridView1.Rows(ind).Cells(cellPrIncom).Style.BackColor = Color.LightYellow
						DataGridView1.Rows(ind).Cells(cellReq).Style.BackColor = Color.LightYellow
					Else
						DataGridView1.Rows(ind).Cells(cellM).Style.BackColor = Color.White
						DataGridView1.Rows(ind).Cells(cellPO).Style.BackColor = Color.White
						DataGridView1.Rows(ind).Cells(cellPrIncom).Style.BackColor = Color.White
						DataGridView1.Rows(ind).Cells(cellReq).Style.BackColor = Color.White
					End If

					If HitungEndingStock >= 0 Then
						DataGridView1.Rows(ind).Cells(cellReq).Style.BackColor = Color.FromArgb(26, 191, 98)
					Else
						DataGridView1.Rows(ind).Cells(cellReq).Style.BackColor = Color.FromArgb(255, 204, 0)
					End If

					' === warna pemisah (abu-abu)
					DataGridView1.Rows(ind).Cells(cellPisah).Style.BackColor = Color.LightGray

				Next

			Next

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
		ExportToExcel()
	End Sub

	Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
		Start_Loading(Me)

		GetDataRix()
		End_Loading(Me)
	End Sub

	Private Sub ExportToExcel()

		Dim xlApp As New Excel.Application
		Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add()
		Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Sheets(1)

		xlApp.ScreenUpdating = False

		' mapping kolom
		Dim mapping As Integer() = {
			CellKd_Barang,
			CellNm_Barang,
			cell_Satuan,
			cellEndingStock,
			cellOutStandingPRD,
			cell_BelumTerjadwal,
			cell_M1,
			cell_POM1,
			Cell_PRIncomM1,
			cell_ReqStokM1,
			cell_M2,
			cell_POM2,
			Cell_PRIncomM2,
			cell_ReqStokM2,
			cell_M3,
			cell_POM3,
			Cell_PRIncomM3,
			cell_ReqStokM3,
			cell_M4,
			cell_POM4,
			Cell_PRIncomM4,
			cell_ReqStokM4
		}

		Dim CellInteger As Integer() = {
			cellEndingStock,
			cellOutStandingPRD,
			cell_BelumTerjadwal,
			cell_M1,
			cell_POM1,
			Cell_PRIncomM1,
			cell_ReqStokM1,
			cell_M2,
			cell_POM2,
			Cell_PRIncomM2,
			cell_ReqStokM2,
			cell_M3,
			cell_POM3,
			Cell_PRIncomM3,
			cell_ReqStokM3,
			cell_M4,
			cell_POM4,
			Cell_PRIncomM4,
			cell_ReqStokM4
		}

		Dim CellCenter As Integer() = {
			cell_Satuan
		}

		Dim colCount As Integer = mapping.Length
		Dim rowCount As Integer = DataGridView1.Rows.Count

		' 🔥 HEADER
		For j As Integer = 0 To colCount - 1
			xlWorkSheet.Cells(1, j + 1) = DataGridView1.Columns(mapping(j)).HeaderText
		Next

		' 🔥 HITUNG ROW VALID
		Dim validRowCount As Integer = 0
		For i As Integer = 0 To rowCount - 1
			If Not DataGridView1.Rows(i).IsNewRow Then
				validRowCount += 1
			End If
		Next

		If validRowCount = 0 Then Exit Sub

		' 🔥 ARRAY 2D
		Dim data(validRowCount - 1, colCount - 1) As Object

		Dim rowIndex As Integer = 0

		For i As Integer = 0 To rowCount - 1
			If Not DataGridView1.Rows(i).IsNewRow Then

				For j As Integer = 0 To colCount - 1
					data(rowIndex, j) = DataGridView1.Item(mapping(j), i).Value
				Next

				rowIndex += 1
			End If
		Next

		' 🔥 BULK WRITE (INI YANG NGEBOOST SPEED)
		Dim startCell = xlWorkSheet.Cells(2, 1)
		Dim endCell = xlWorkSheet.Cells(validRowCount + 1, colCount)

		xlWorkSheet.Range(startCell, endCell).Value = data

		' format
		'xlWorkSheet.Columns(1).NumberFormat = "@"
		'xlWorkSheet.Columns.AutoFit()

		' 🔥 FORMATTING LOGIC
		' 1. Default: Semua Kolom Set ke Text (@) dan Rata Kiri
		Dim allDataRange = xlWorkSheet.Range(xlWorkSheet.Cells(2, 1), xlWorkSheet.Cells(validRowCount + 1, colCount))
		allDataRange.NumberFormat = "@"
		allDataRange.HorizontalAlignment = Excel.Constants.xlLeft

		' 🔥 1. FORMAT ACCOUNTING / NUMBER (Rata Kanan)
		Dim rangeNumeric As Excel.Range = Nothing

		For Each colIdx In CellInteger
			Dim targetColIndex As Integer = Array.IndexOf(mapping, colIdx) + 1
			If targetColIndex > 0 Then
				If rangeNumeric Is Nothing Then
					rangeNumeric = xlWorkSheet.Columns(targetColIndex)
				Else
					rangeNumeric = xlApp.Union(rangeNumeric, xlWorkSheet.Columns(targetColIndex))
				End If
			End If
		Next

		If rangeNumeric IsNot Nothing Then
			rangeNumeric.NumberFormat = "#,##0.00"
			rangeNumeric.HorizontalAlignment = Excel.Constants.xlRight
		End If

		' 🔥 2. FORMAT RATA TENGAH (Center)
		Dim rangeCenter As Excel.Range = Nothing

		For Each colIdx In CellCenter
			Dim targetColIndex As Integer = Array.IndexOf(mapping, colIdx) + 1
			If targetColIndex > 0 Then
				If rangeCenter Is Nothing Then
					rangeCenter = xlWorkSheet.Columns(targetColIndex)
				Else
					rangeCenter = xlApp.Union(rangeCenter, xlWorkSheet.Columns(targetColIndex))
				End If
			End If
		Next

		If rangeCenter IsNot Nothing Then
			rangeCenter.HorizontalAlignment = Excel.Constants.xlCenter
		End If

		' 🔥 3. CLEANUP RANGE OBJECTS
		If rangeNumeric IsNot Nothing Then releaseObject(rangeNumeric)
		If rangeCenter IsNot Nothing Then releaseObject(rangeCenter)

		' 3. Header Styling (Opsional: Bold & Center)
		Dim headerRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, colCount))
		headerRange.Font.Bold = True
		headerRange.HorizontalAlignment = Excel.Constants.xlCenter

		xlWorkSheet.Columns.AutoFit()

		' SAVE
		Dim saveFileDialog As New SaveFileDialog
		saveFileDialog.Filter = "Excel Files|*.xlsx"

		If saveFileDialog.ShowDialog = DialogResult.OK Then
			xlWorkBook.SaveAs(saveFileDialog.FileName)
		End If

		' CLEANUP
		xlWorkBook.Close(False)
		xlApp.Quit()

		releaseObject(xlWorkSheet)
		releaseObject(xlWorkBook)
		releaseObject(xlApp)

		MessageBox.Show("Export berhasil!", "Sukses Simpan", MessageBoxButtons.OK, MessageBoxIcon.Information)

	End Sub

End Class