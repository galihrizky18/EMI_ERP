Public Class SD_ValidasiGR_Split

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim arrSelectedBarcode As New List(Of Dictionary(Of String, String))
	Dim arrSelectedBarcode_PalletPacking As New List(Of (QRCode As String, KdUnikBerjalan As String, BarcodePalletPack As String))
	Dim arrSelectedBarcode_WastePacking As New List(Of (QRCode As String, KdUnikBerjalan As String))
	Public arrBarcodeFromParent As New List(Of Dictionary(Of String, String))
	Public arrBarcodeFromParent_PalletPacking As New List(Of (QRCode As String, KdUnikBerjalan As String, BarcodePalletPack As String))
	Public arrBarcodeFromParent_WastePacking As New List(Of (QRCode As String, KdUnikBerjalan As String))
	Public MenuAsal As String

	Dim Fitur_Military_Sampling As Boolean = False

	Dim Lv_NoSplit, Lv_Lokasi, Lv_Barcode, Lv_Batch, Lv_KdBarang, Lv_NmBarang, Lv_Jumlah, Lv_Satuan, Lv_TglProduksi, Lv_TglExpired, Lv_Kualitas, Lv_QrCode, Lv_KdUnikBerjalan, Lv_StatMilitary As String

	Dim item_NoSplit As Integer = 0
	Dim item_Lokasi As Integer = 1
	Dim item_Barcode As Integer = 2
	Dim item_Batch As Integer = 3
	Dim item_KdBarang As Integer = 4
	Dim item_NmBarang As Integer = 5
	Dim item_Jumlah As Integer = 6
	Dim item_Satuan As Integer = 7
	Dim item_TglProduksi As Integer = 8
	Dim item_TglExpired As Integer = 9
	Dim item_Kualitas As Integer = 10
	Dim item_Nomor As Integer = 11
	Dim item_QrCode As Integer = 12
	Dim item_KdUnikBerjalan As Integer = 13
	Dim item_StatMilitarySampling As Integer = 14

	Dim LvPack_NoTransaksi, LvPack_NoSplit, LvPack_Line, LvPack_Barcode, LvPack_Jumlah, LvPack_Satuan, LvPack_BarcodeGR1, LvPack_QrCode, LvPack_KdUnikBerjalan, LvPack_Batch,
		LvPack_FlagValidasiAndroid, LvPack_FlagRepacking As String

	Dim ItemPalletPack_NoTransaksi As Integer = 0
	Dim ItemPalletPack_NoSplit As Integer = 1
	Dim ItemPalletPack_Line As Integer = 2
	Dim ItemPalletPack_Barcode As Integer = 3
	Dim ItemPalletPack_Jumlah As Integer = 4
	Dim ItemPalletPack_Satuan As Integer = 5
	Dim ItemPalletPack_BarcodeGr1 As Integer = 6
	Dim ItemPalletPack_QrCode As Integer = 7
	Dim ItemPalletPack_KodeUnikBerjalan As Integer = 8
	Dim ItemPalletPack_Batch As Integer = 9
	Dim ItemPalletPack_FlagValidasiAndroid As Integer = 10
	Dim ItemPalletPack_FlagRepacking As Integer = 11

	Dim LvPackWaste_NoTransaksi, LvPackWaste_NoSplit, LvPackWaste_TglProduksi, LvPackWaste_TglExpired, LvPackWaste_Barcode,
		LvPackWaste_Jumlah, LvPackWaste_Satuan, LvPackWaste_QrCode, LvPackWaste_KdUnikBerjalan As String

	Dim ItemWastePack_NoTransaksi As Integer = 0
	Dim ItemWastePack_NoSplit As Integer = 1
	Dim ItemWastePack_TglProduksi As Integer = 2
	Dim ItemWastePack_TglExpired As Integer = 3
	Dim ItemWastePack_Barcode As Integer = 4
	Dim ItemWastePack_Jumlah As Integer = 5
	Dim ItemWastePack_Satuan As Integer = 6
	Dim ItemWastePack_QrCode As Integer = 7
	Dim ItemWastePack_KdUnikBerjalan As Integer = 8

	Dim SelectedSplit As String = ""

	Dim CurrentVariant As String = ""

	Dim DataCmbLain_GR1 As New List(Of (Combobox As String, FilterSql As String)) From {
			(OpsiSeluruh, OpsiSeluruh),
			("No Split", "b.No_Production_Order"),
			("Lokasi", "a.Lokasi_Gudang"),
			("Barcode", "(a.Qr_Code + '-' + a.Kode_Unik_Berjalan)"),
			("Kode Barang", "c.Kode_Barang"),
			("Nama Barang", "d.Nama"),
			("Kualitas", "e.Keterangan")
		}

	Dim DataCmbLain_PalletPacking As New List(Of (Combobox As String, FilterSql As String)) From {
			(OpsiSeluruh, OpsiSeluruh),
			("No Transaksi", "a.No_Transaksi"),
			("No Split", "h.No_Production_Order"),
			("Line", "b.Line"),
			("Barcode GR 1", "(f.Qr_Code+'-'+f.Kode_Unik_Berjalan)"),
			("Barcode Pallet", "a.Kode_Unik_Print")
		}

	Dim DataCmbLain_WastePacking As New List(Of (Combobox As String, FilterSql As String)) From {
			(OpsiSeluruh, OpsiSeluruh),
			("No Transaksi", "a.No_Transaksi"),
			("No Split", "d.No_Production_Order"),
			("Batch", "b.Tahap"),
			("Barcode", "(c.Qr_Code+'-'+c.Kode_Unik_Berjalan)")
		}

	Dim DataCmbTanggal_GR1 As New List(Of (Combobox As String, FilterSql As String)) From {
		(OpsiSeluruh, OpsiSeluruh),
		("Tanggal Produksi", "a.Tgl_Produksi"),
		("Tanggal Expired", "a.Tgl_Expired")
	}

	Dim DataCmbTanggal_PackingPallet As New List(Of (Combobox As String, FilterSql As String)) From {
		(OpsiSeluruh, OpsiSeluruh)
	}

	Dim DataCmbTanggal_PackingWaste As New List(Of (Combobox As String, FilterSql As String)) From {
		(OpsiSeluruh, OpsiSeluruh),
		("Tanggal Produksi", "a.Tgl_Produksi"),
		("Tanggal Expired", "a.Tgl_Expired")
	}

	Private Sub SD_ValidasiGR_Split_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBuffer(Lv_Data)
		EnableDoubleBuffer(Lv_Packing_Pallet)
		EnableDoubleBuffer(Lv_Packing_Waste)

		Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
		Lv_Data.Columns.Add("No Split", 120, HorizontalAlignment.Left) '0
		Lv_Data.Columns.Add("Lokasi", 140, HorizontalAlignment.Left) '1
		Lv_Data.Columns.Add("Barcode", 250, HorizontalAlignment.Left) '2
		Lv_Data.Columns.Add("Batch Number", 0, HorizontalAlignment.Left) '3
		Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left) '4
		Lv_Data.Columns.Add("Nama Barang", 180, HorizontalAlignment.Left) '5
		Lv_Data.Columns.Add("Jumlah", 110, HorizontalAlignment.Right) '6
		Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '7
		Lv_Data.Columns.Add("Tgl Produksi", 100, HorizontalAlignment.Center) '8
		Lv_Data.Columns.Add("Tgl Expired", 100, HorizontalAlignment.Center) '9
		Lv_Data.Columns.Add("Kualitas", 130, HorizontalAlignment.Center) '10
		Lv_Data.Columns.Add("Nomor", 90, HorizontalAlignment.Center) '11
		'Hide
		Lv_Data.Columns.Add("QrCode", 0, HorizontalAlignment.Left) '12
		Lv_Data.Columns.Add("kdUnikBerjalan", 0, HorizontalAlignment.Left) '13
		Lv_Data.Columns.Add("StatMil", 0, HorizontalAlignment.Left) '14
		Lv_Data.View = View.Details

		Lv_Data.Columns(11).DisplayIndex = 3

		Lv_Packing_Pallet.Columns.Clear()
		Lv_Packing_Pallet.Columns.Add("No Transaksi", 120, HorizontalAlignment.Left) '0
		Lv_Packing_Pallet.Columns.Add("No Split", 120, HorizontalAlignment.Left) '1
		Lv_Packing_Pallet.Columns.Add("Line", 80, HorizontalAlignment.Center) '2
		Lv_Packing_Pallet.Columns.Add("Barcode", 150, HorizontalAlignment.Left) '3
		Lv_Packing_Pallet.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '4
		Lv_Packing_Pallet.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '5
		Lv_Packing_Pallet.Columns.Add("BarcodeGR1", 170, HorizontalAlignment.Left).DisplayIndex = 4 '6
		Lv_Packing_Pallet.Columns.Add("QrCode", 0, HorizontalAlignment.Left) '7
		Lv_Packing_Pallet.Columns.Add("KodeUnikBerjalan", 0, HorizontalAlignment.Left) '8
		Lv_Packing_Pallet.Columns.Add("Batch", 80, HorizontalAlignment.Center).DisplayIndex = 2 '9
		Lv_Packing_Pallet.Columns.Add("FlagValidasiAndroid", 0, HorizontalAlignment.Center) '10
		Lv_Packing_Pallet.Columns.Add("FlagRepacking", 0, HorizontalAlignment.Center) '11
		Lv_Packing_Pallet.View = View.Details

		Lv_Packing_Waste.Columns.Clear()
		Lv_Packing_Waste.Columns.Add("No Transaksi", 120, HorizontalAlignment.Left) '0
		Lv_Packing_Waste.Columns.Add("No Split", 120, HorizontalAlignment.Left) '1
		Lv_Packing_Waste.Columns.Add("Tanggal Produksi", 100, HorizontalAlignment.Center) '2
		Lv_Packing_Waste.Columns.Add("Tanggal Expired", 100, HorizontalAlignment.Center) '3
		Lv_Packing_Waste.Columns.Add("Barcode", 280, HorizontalAlignment.Left) '4
		Lv_Packing_Waste.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '5
		Lv_Packing_Waste.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '6
		'Hide
		Lv_Packing_Waste.Columns.Add("QrCode", 0, HorizontalAlignment.Left) '7
		Lv_Packing_Waste.Columns.Add("KodeUnikBerjalan", 0, HorizontalAlignment.Left) '8
		Lv_Packing_Waste.View = View.Details

		'Cmb_Lain.Items.Clear() : arrParamLain.Clear()
		'Cmb_Lain.Items.Add(OpsiSeluruh) : arrParamLain.Add(OpsiSeluruh)
		'Cmb_Lain.Items.Add("No Split") : arrParamLain.Add("b.No_Production_Order")
		'Cmb_Lain.Items.Add("Lokasi") : arrParamLain.Add("a.Lokasi_Gudang")
		'Cmb_Lain.Items.Add("Barcode") : arrParamLain.Add("(a.Qr_Code + '-' + a.Kode_Unik_Berjalan)")
		'Cmb_Lain.Items.Add("Kode Barang") : arrParamLain.Add("c.Kode_Barang")
		'Cmb_Lain.Items.Add("Nama Barang") : arrParamLain.Add("d.Nama")
		'Cmb_Lain.Items.Add("Kualitas") : arrParamLain.Add("e.Keterangan")

		Kosong()
	End Sub

	Private Sub Kosong()

		Txt_ValueLain.Text = ""
		arrSelectedBarcode.Clear()
		arrSelectedBarcode_PalletPacking.Clear()
		arrSelectedBarcode_WastePacking.Clear()

		SelectedSplit = ""
		CurrentVariant = ""

		Tgl1.Enabled = False : Tgl2.Enabled = False

		'TabControl1.SelectedIndex = 0
		'TabControl1_SelectedIndexChanged(TabControl1, EventArgs.Empty)

		Load_Data_GR_1()
		Load_Data_Pallet_Packing()
		Load_Data_Waste_Packing()
		TabControl1.SelectedIndex = 0
		TabControl1_SelectedIndexChanged(Txt_ValueLain, EventArgs.Empty)

	End Sub

	Private Sub Get_Data_Listview(ByVal index As Integer)

		Lv_NoSplit = Lv_Data.Items(index).SubItems(item_NoSplit).Text
		Lv_Lokasi = Lv_Data.Items(index).SubItems(item_Lokasi).Text
		Lv_Barcode = Lv_Data.Items(index).SubItems(item_Barcode).Text
		Lv_Batch = Lv_Data.Items(index).SubItems(item_Batch).Text
		Lv_KdBarang = Lv_Data.Items(index).SubItems(item_KdBarang).Text
		Lv_NmBarang = Lv_Data.Items(index).SubItems(item_NmBarang).Text
		Lv_Jumlah = Lv_Data.Items(index).SubItems(item_Jumlah).Text
		Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
		Lv_TglProduksi = Lv_Data.Items(index).SubItems(item_TglProduksi).Text
		Lv_TglExpired = Lv_Data.Items(index).SubItems(item_TglExpired).Text
		Lv_Kualitas = Lv_Data.Items(index).SubItems(item_Kualitas).Text
		Lv_QrCode = Lv_Data.Items(index).SubItems(item_QrCode).Text
		Lv_KdUnikBerjalan = Lv_Data.Items(index).SubItems(item_KdUnikBerjalan).Text
		Lv_StatMilitary = Lv_Data.Items(index).SubItems(item_StatMilitarySampling).Text

	End Sub

	Private Sub Get_Data_Lv_Packing_Pallet(ByVal index As Integer)

		LvPack_NoTransaksi = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_NoTransaksi).Text
		LvPack_NoSplit = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_NoSplit).Text
		LvPack_Line = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_Line).Text
		LvPack_Barcode = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_Barcode).Text
		LvPack_Jumlah = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_Jumlah).Text
		LvPack_Satuan = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_Satuan).Text
		LvPack_BarcodeGR1 = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_BarcodeGr1).Text
		LvPack_QrCode = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_QrCode).Text
		LvPack_KdUnikBerjalan = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_KodeUnikBerjalan).Text
		LvPack_Batch = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_Batch).Text
		LvPack_FlagValidasiAndroid = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_FlagValidasiAndroid).Text
		LvPack_FlagRepacking = Lv_Packing_Pallet.Items(index).SubItems(ItemPalletPack_FlagRepacking).Text

	End Sub

	Private Sub Get_Data_Lv_Packing_Waste(ByVal index As Integer)

		LvPackWaste_NoTransaksi = Lv_Packing_Waste.Items(index).SubItems(ItemWastePack_NoTransaksi).Text
		LvPackWaste_NoSplit = Lv_Packing_Waste.Items(index).SubItems(ItemWastePack_NoSplit).Text
		LvPackWaste_TglProduksi = Lv_Packing_Waste.Items(index).SubItems(ItemWastePack_TglProduksi).Text
		LvPackWaste_TglExpired = Lv_Packing_Waste.Items(index).SubItems(ItemWastePack_TglExpired).Text
		LvPackWaste_Barcode = Lv_Packing_Waste.Items(index).SubItems(ItemWastePack_Barcode).Text
		LvPackWaste_Jumlah = Lv_Packing_Waste.Items(index).SubItems(ItemWastePack_Jumlah).Text
		LvPackWaste_QrCode = Lv_Packing_Waste.Items(index).SubItems(ItemWastePack_QrCode).Text
		LvPackWaste_KdUnikBerjalan = Lv_Packing_Waste.Items(index).SubItems(ItemWastePack_KdUnikBerjalan).Text

	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Cmb_Lain.SelectedIndex <> 0 Then
			If Txt_ValueLain.Text.Trim.Length = 0 Then
				MessageBox.Show("Value Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If
		End If

		If TabControl1.SelectedIndex = 0 Then
			Load_Data_GR_1()
		ElseIf TabControl1.SelectedIndex = 1 Then
			Load_Data_Pallet_Packing()
		ElseIf TabControl1.SelectedIndex = 2 Then
			Load_Data_Waste_Packing()
		End If

	End Sub

	Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

		Cmb_Lain.Items.Clear() : Cmb_Periode.Items.Clear()
		Cmb_Lain.SelectedIndex = -1 : Cmb_Periode.SelectedIndex = -1

		If TabControl1.SelectedIndex = 0 Then

			For Each Datas In DataCmbLain_GR1
				Cmb_Lain.Items.Add(Datas.Combobox)
			Next

			For Each Datas In DataCmbTanggal_GR1
				Cmb_Periode.Items.Add(Datas.Combobox)
			Next

		ElseIf TabControl1.SelectedIndex = 1 Then
			For Each Datas In DataCmbLain_PalletPacking
				Cmb_Lain.Items.Add(Datas.Combobox)
			Next

			For Each Datas In DataCmbTanggal_PackingPallet
				Cmb_Periode.Items.Add(Datas.Combobox)
			Next

		ElseIf TabControl1.SelectedIndex = 2 Then
			For Each Datas In DataCmbLain_WastePacking
				Cmb_Lain.Items.Add(Datas.Combobox)
			Next

			For Each Datas In DataCmbTanggal_PackingWaste
				Cmb_Periode.Items.Add(Datas.Combobox)
			Next
		End If

		Cmb_Lain.SelectedIndex = 0
		Cmb_Periode.SelectedIndex = 0
		'Btn_Cari_Click(Me, New EventArgs)
	End Sub

	Private Sub Load_Data_GR_1()
		Try
			OpenConn()

			Lv_Data.Items.Clear()

#Region "Kode Lama"

			'SQL = "select top(300) b.No_Production_Order as No_Split, a.Lokasi_Gudang as Kode_Stock_Owner, a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Batch_Number,  "
			'SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama as Nama_Barang, a.Nomor, "
			''SQL = SQL & "sum(f.Jumlah) as Jumlah, "

			'SQL = SQL & "isnull((sum(f.Jumlah) -  "
			'SQL = SQL & "(select isnull(sum(jumlah), 0) from N_EMI_Validation_GR_Temp z "
			'SQL = SQL & "where b.kode_perusahaan = z.kode_perusahaan "
			'SQL = SQL & "and b.no_production_order = z.no_production_order "
			'SQL = SQL & "and z.barcode = (a.Qr_Code + '-' + a.Kode_Unik_Berjalan)) ), 0) as Jumlah, "

			'SQL = SQL & "a.Satuan, a.Jenis, e.Keterangan as Kualitas, a.proses, "

			'SQL = SQL & "isnull(( "
			'SQL = SQL & "select top 1 'Y' from N_EMI_Military_Sampling z "
			'SQL = SQL & "where z.kode_perusahaan = a.Kode_Perusahaan and z.status is null "
			'SQL = SQL & "and z.No_Split = b.No_Production_Order and z.No_Batch = a.tahap "
			'SQL = SQL & "and z.No_GR = '1' "
			'SQL = SQL & "), 'T') as Status_Military_Sampling, "

			'SQL = SQL & "isnull(( "
			'SQL = SQL & "select isnull(flag_commercial, 'T') "
			'SQL = SQL & "from emi_split_production_order x, emi_order_produksi y where "
			'SQL = SQL & "x.kode_perusahaan =y.kode_perusahaan and x.no_po=y.no_faktur and x.status is null and y.status is null "
			'SQL = SQL & "and b.kode_perusahaan=x.kode_perusahaan and b.no_production_order=x.no_transaksi), null) as Flag_Commercial "

			'SQL = SQL & "from Emi_Production_Results_Detail_Pallet a, Emi_Production_Results b, EMI_Production_Results_Detail_Barang c, barang d, EMI_Master_Warna e, Barang_SN f "
			'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
			'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
			'SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and a.Proses = c.Proses "
			'SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
			'SQL = SQL & "and a.Jenis = e.Kode_Warna "
			'SQL = SQL & "and a.SN_Baru = f.Serial_Number "
			'SQL = SQL & "and b.Status is null "
			'SQL = SQL & "and a.Lokasi_Gudang in (select Kode_Stock_Owner from Stock_Owner_Gudang z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Flag_QI = 'Y') "

			'If Cmb_Periode.SelectedIndex > 0 Then
			'	If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

			'	SQL = SQL & arrPeriode(Cmb_Periode.SelectedIndex) & " between '"
			'	SQL = SQL & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
			'End If

			'If Cmb_Lain.SelectedIndex > 0 Then
			'	If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

			'	SQL = SQL & DataCmbLain_GR1(Cmb_Lain.SelectedIndex).FilterSql & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
			'End If

			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and f.jumlah<>0 "
			'SQL = SQL & "group by  b.No_Production_Order, a.Lokasi_Gudang , a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) , a.Batch_Number, "
			'SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama, a.Satuan, a.Jenis, e.Keterangan, b.kode_perusahaan, a.Nomor, a.Kode_Perusahaan, a.Proses, a.tahap "
			'SQL = SQL & "order by  b.No_Production_Order, a.Nomor, a.Lokasi_Gudang, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan), a.Tgl_Expired ASC "

#End Region

			SQL = "select top (300) b.No_Production_Order as No_Split, a.Lokasi_Gudang as Kode_Stock_Owner, a.Qr_Code, "
			SQL = SQL & "a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Batch_Number, "
			SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama as Nama_Barang, a.Nomor, "
			SQL = SQL & "isnull((sum(f.Jumlah) - (select isnull(sum(jumlah), 0) "
			SQL = SQL & "from N_EMI_Validation_GR_Temp z "
			SQL = SQL & "where b.kode_perusahaan = z.kode_perusahaan "
			SQL = SQL & "and b.no_production_order = z.no_production_order "
			SQL = SQL & "and z.barcode = (a.Qr_Code + '-' + a.Kode_Unik_Berjalan))), 0) as Jumlah, "
			SQL = SQL & "a.Satuan, a.Jenis, e.Keterangan as Kualitas, a.proses, "
			SQL = SQL & "isnull((select top 1 'Y' from N_EMI_Military_Sampling z "
			SQL = SQL & "where z.kode_perusahaan = a.Kode_Perusahaan "
			SQL = SQL & "and z.status is null "
			SQL = SQL & "and z.No_Split = b.No_Production_Order "
			SQL = SQL & "and z.No_Batch = a.tahap "
			SQL = SQL & "and z.No_GR = '1'), 'T') as Status_Military_Sampling, "
			SQL = SQL & "isnull((select isnull(flag_commercial, 'T') "
			SQL = SQL & "from emi_split_production_order x, emi_order_produksi y "
			SQL = SQL & "where x.kode_perusahaan = y.kode_perusahaan "
			SQL = SQL & "and x.no_po = y.no_faktur "
			SQL = SQL & "and x.status is null and y.status is null "
			SQL = SQL & "and b.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and b.no_production_order = x.no_transaksi), null) as Flag_Commercial "
			SQL = SQL & "from Emi_Production_Results_Detail_Pallet a "
			SQL = SQL & "inner join Emi_Production_Results b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "inner join EMI_Production_Results_Detail_Barang c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Transaksi = c.No_Transaksi and a.Proses = c.Proses "
			SQL = SQL & "inner join barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
			SQL = SQL & "inner join EMI_Master_Warna e on a.Kode_Perusahaan = e.Kode_Perusahaan and a.Jenis = e.Kode_Warna "
			SQL = SQL & "inner join Barang_SN f on a.Kode_Perusahaan = f.Kode_Perusahaan and a.SN_Baru = f.Serial_Number "
			SQL = SQL & "left join N_EMI_Transaksi_Packing_Detail g on a.Kode_Perusahaan = g.Kode_Perusahaan and a.SN_Baru = g.SN_Baru "
			SQL = SQL & "where b.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and f.jumlah <> 0 "
			SQL = SQL & "and g.Kode_Perusahaan is null "
			If Cmb_Periode.SelectedIndex > 0 Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & DataCmbTanggal_GR1(Cmb_Periode.SelectedIndex).FilterSql & " between '"
				SQL = SQL & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
			End If

			If Cmb_Lain.SelectedIndex > 0 Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & DataCmbLain_GR1(Cmb_Lain.SelectedIndex).FilterSql & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
			End If
			SQL = SQL & "and a.Lokasi_Gudang in ( select Kode_Stock_Owner from Stock_Owner_Gudang z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Flag_QI = 'Y') "
			SQL = SQL & "group by b.No_Production_Order, a.Lokasi_Gudang, a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan), a.Batch_Number, "
			SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama, a.Satuan, a.Jenis, e.Keterangan, "
			SQL = SQL & "b.kode_perusahaan, a.Nomor, a.Kode_Perusahaan, a.Proses, a.tahap "
			SQL = SQL & "order by b.No_Production_Order, a.Nomor, a.Lokasi_Gudang, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan), a.Tgl_Expired ASC "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim Lv As ListViewItem
					Lv = Lv_Data.Items.Add(Dr("No_Split")) '0
					Lv.SubItems.Add(Dr("Kode_Stock_Owner")) '1
					Lv.SubItems.Add(Dr("Barcode")) '2
					Lv.SubItems.Add(Dr("Batch_Number")) '3
					Lv.SubItems.Add(Dr("Kode_Barang")) '4
					Lv.SubItems.Add(Dr("Nama_Barang")) '5
					Lv.SubItems.Add(Format(Dr("Jumlah"), "N2")) '6
					Lv.SubItems.Add(Dr("Satuan")) '7
					Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy")) '8
					Lv.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy")) '9
					Lv.SubItems.Add(Dr("Kualitas")) '10
					Lv.SubItems.Add(Dr("Nomor")) '11

					'Hide
					Lv.SubItems.Add(Dr("Qr_Code")) '12
					Lv.SubItems.Add(Dr("Kode_Unik_Berjalan")) '13
					If Dr("Flag_Commercial") = "Y" Then

						Lv.SubItems.Add(Dr("Status_Military_Sampling")) '14
						If Dr("Status_Military_Sampling") = "Y" Then
							Lv.BackColor = Color.LightBlue
						End If
					Else
						Lv.SubItems.Add("Y")
						Lv.BackColor = Color.Tan
					End If

					'=========================================================
					'=     LAKUKAN CEK APAKAH ADA SUDAH ADA DI LV PARENT     =
					'=========================================================
					' Cek dengan Lamda Expression
					' Any akan return true jika ada 1 saja data di function bernilai true
					' function / sub adalah syarat yang harus ada pada scope lambda expression
					Dim foundQrCode As Boolean = arrBarcodeFromParent.Any(Function(dict) dict.ContainsKey("QrCode") AndAlso dict("QrCode") = Dr("Qr_Code"))
					Dim foundKodeUnik As Boolean = arrBarcodeFromParent.Any(Function(dict) dict.ContainsKey("KdUnikBerjalan") AndAlso dict("KdUnikBerjalan").Trim.ToUpper() = Dr("Kode_Unik_Berjalan").ToString.Trim.ToUpper)

					If foundQrCode And foundKodeUnik Then
						Lv.Checked = True
					Else
						Lv.Checked = False
					End If

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Load_Data_Pallet_Packing()
		Try
			OpenConn()

			Lv_Packing_Pallet.Items.Clear()

#Region "Kode Jika Repacking Berdasarkan Pallet Packking"

			'SQL = "select a.No_Transaksi, h.No_Production_Order as No_Split, a.Kode_Unik_Print as Barcode_Pallet, a.Urut_Oto as Urut_Pallet, b.Line, "
			'SQL = SQL & "a.Jumlah, 'Box' as Satuan_Pallet, sum(c.Jumlah) as Jumlah_Pcs, "
			'SQL = SQL & "a.Tgl_Cetak, a.Jam_Cetak, (f.Qr_Code+'-'+f.Kode_Unik_Berjalan) as Barcode_GR1_Detail_Pallet, f.Qr_Code, f.Kode_Unik_Berjalan, g.tahap, a.Flag_Approval_Android, "
			'SQL = SQL & "CASE WHEN i.Urut_Pallet IS NULL THEN NULL WHEN i.Flag_Selesai IS NULL THEN 'Y' WHEN i.Flag_Selesai = 'Y' THEN NULL ELSE NULL END AS Flag_Repacking "
			'SQL = SQL & "from N_EMI_Transaksi_Packing_Pallet a "
			'SQL = SQL & "inner join N_EMI_Transaksi_Packing_Box b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Oto = b.Urut_Pallet and b.Status is null "
			'SQL = SQL & "inner join N_EMI_Transaksi_Packing_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_Oto = c.Urut_Transaksi_Box and c.Status is null "
			'SQL = SQL & "inner join N_EMI_Transaksi_Packing_Detail e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Urut_Detail_Transaksi_Packing = e.Urut_Oto "
			'SQL = SQL & "inner join Barang_SN f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.SN_Baru = f.Serial_Number "
			'SQL = SQL & "inner join Emi_Production_Results_Detail_Pallet g on e.Kode_Perusahaan = g.Kode_Perusahaan and e.SN_Baru = g.Serial_Number "
			'SQL = SQL & "inner join Emi_Production_Results h on h.Kode_Perusahaan = h.Kode_Perusahaan and g.No_Transaksi = h.No_Transaksi and h.Status is null "
			'SQL = SQL & "left join ( select z.Kode_Perusahaan, z.Urut_Pallet, "
			'SQL = SQL & "case when COUNT(z.Urut_Pallet) > COUNT(z.Flag_Selesai) then null else 'Y' end as Flag_Selesai "
			'SQL = SQL & "from N_EMI_Transaksi_Packing_Repacking z "
			'SQL = SQL & "where z.Status IS NULL "
			'SQL = SQL & "group by z.Kode_Perusahaan, z.Urut_Pallet "
			'SQL = SQL & ") i on a.Kode_Perusahaan = i.Kode_Perusahaan and a.Urut_Oto = i.Urut_Pallet "
			'SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.Status is null "
			'SQL = SQL & "and a.Flag_Input_GR2 is null "
			'If Cmb_Lain.SelectedIndex > 0 Then
			'	If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

			'	SQL = SQL & DataCmbLain_PalletPacking(Cmb_Lain.SelectedIndex).FilterSql & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
			'End If
			'SQL = SQL & "group by a.No_Transaksi, h.No_Production_Order, a.Kode_Unik_Print, a.Urut_Oto, b.Line, a.Jumlah, a.Tgl_Cetak, a.Jam_Cetak, (f.Qr_Code+'-'+f.Kode_Unik_Berjalan), "
			'SQL = SQL & "f.Qr_Code, f.Kode_Unik_Berjalan, g.tahap, a.Flag_Approval_Android, i.Flag_Selesai, i.Urut_Pallet "
			'SQL = SQL & "order by a.Tgl_Cetak, a.Jam_Cetak "

#End Region

#Region "Kode Jika Repacking Berdasarkan Pallet GR 1"

			'SQL = "SELECT top (300) a.No_Transaksi, h.No_Production_Order AS No_Split, a.Kode_Unik_Print AS Barcode_Pallet, a.Urut_Oto AS Urut_Pallet, b.Line, "
			'SQL = SQL & "a.Jumlah, 'Box' AS Satuan_Pallet, SUM(c.Jumlah) AS Jumlah_Pcs, "
			'SQL = SQL & "a.Tgl_Cetak, a.Jam_Cetak, (f.Qr_Code + '-' + f.Kode_Unik_Berjalan) AS Barcode_GR1_Detail_Pallet, f.Qr_Code, f.Kode_Unik_Berjalan, g.tahap, a.Flag_Approval_Android, "
			'SQL = SQL & "MAX(CASE WHEN i.Urut_Oto_Detail IS NOT NULL AND i.Flag_Selesai_Repack IS NULL THEN 'Y' ELSE NULL END) AS Flag_Repacking "
			'SQL = SQL & "FROM N_EMI_Transaksi_Packing_Pallet a "
			'SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Box b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Urut_Oto = b.Urut_Pallet AND b.Status IS NULL "
			'SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Det c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.Urut_Oto = c.Urut_Transaksi_Box AND c.Status IS NULL "
			'SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Detail e ON c.Kode_Perusahaan = e.Kode_Perusahaan AND c.Urut_Detail_Transaksi_Packing = e.Urut_Oto "
			'SQL = SQL & "INNER JOIN Barang_SN f ON e.Kode_Perusahaan = f.Kode_Perusahaan AND e.SN_Baru = f.Serial_Number "
			'SQL = SQL & "INNER JOIN Emi_Production_Results_Detail_Pallet g ON e.Kode_Perusahaan = g.Kode_Perusahaan AND e.SN_Baru = g.Serial_Number "
			'SQL = SQL & "INNER JOIN Emi_Production_Results h ON h.Kode_Perusahaan = h.Kode_Perusahaan AND g.No_Transaksi = h.No_Transaksi AND h.Status IS NULL "
			'SQL = SQL & "LEFT JOIN ( "
			'SQL = SQL & "SELECT z.Kode_Perusahaan, z.Urut_Pallet AS Urut_Oto_Detail, "
			'SQL = SQL & "CASE WHEN COUNT(*) > COUNT(z.Flag_Selesai) THEN NULL ELSE 'Y' END AS Flag_Selesai_Repack "
			'SQL = SQL & "FROM N_EMI_Transaksi_Packing_Repacking z WHERE z.Status IS NULL "
			'SQL = SQL & "GROUP BY z.Kode_Perusahaan, z.Urut_Pallet "
			'SQL = SQL & ") i ON e.Kode_Perusahaan = i.Kode_Perusahaan AND e.Urut_Oto = i.Urut_Oto_Detail "
			'SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "AND a.Status IS NULL AND a.Flag_Input_GR2 IS NULL "
			'If Cmb_Lain.SelectedIndex > 0 Then
			'	If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

			'	SQL = SQL & DataCmbLain_PalletPacking(Cmb_Lain.SelectedIndex).FilterSql & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
			'End If
			'SQL = SQL & "GROUP BY a.No_Transaksi, h.No_Production_Order, a.Kode_Unik_Print, a.Urut_Oto, b.Line, a.Jumlah, "
			'SQL = SQL & "a.Tgl_Cetak, a.Jam_Cetak, f.Qr_Code, f.Kode_Unik_Berjalan, g.tahap, a.Flag_Approval_Android "
			'SQL = SQL & "ORDER BY a.Tgl_Cetak, a.Jam_Cetak "

			SQL = "SELECT TOP (300) a.No_Transaksi, h.No_Production_Order AS No_Split, a.Kode_Unik_Print AS Barcode_Pallet, "
			SQL = SQL & "a.Urut_Oto AS Urut_Pallet, b.Line, a.Jumlah, 'Box' AS Satuan_Pallet, "
			SQL = SQL & "ISNULL((SUM(c.Jumlah) - ISNULL(j.Jumlah_Awal, 0)), 0) AS Jumlah_Pcs, a.Tgl_Cetak, a.Jam_Cetak, "
			SQL = SQL & "(f.Qr_Code + '-' + f.Kode_Unik_Berjalan) AS Barcode_GR1_Detail_Pallet, f.Qr_Code, f.Kode_Unik_Berjalan, "
			SQL = SQL & "g.tahap, a.Flag_Approval_Android, MAX(CASE WHEN i.Urut_Oto_Detail IS NOT NULL AND i.Flag_Selesai_Repack IS NULL "
			SQL = SQL & "THEN 'Y' ELSE NULL END) AS Flag_Repacking FROM N_EMI_Transaksi_Packing_Pallet a "
			SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Box b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Urut_Oto = b.Urut_Pallet AND b.Status IS NULL "
			SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Det c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.Urut_Oto = c.Urut_Transaksi_Box AND c.Status IS NULL "
			SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Detail e ON c.Kode_Perusahaan = e.Kode_Perusahaan AND c.Urut_Detail_Transaksi_Packing = e.Urut_Oto "
			SQL = SQL & "INNER JOIN Barang_SN f ON e.Kode_Perusahaan = f.Kode_Perusahaan AND e.SN_Baru = f.Serial_Number "
			SQL = SQL & "INNER JOIN Emi_Production_Results_Detail_Pallet g ON e.Kode_Perusahaan = g.Kode_Perusahaan AND e.SN_Baru = g.Serial_Number "
			SQL = SQL & "INNER JOIN Emi_Production_Results h ON h.Kode_Perusahaan = h.Kode_Perusahaan AND g.No_Transaksi = h.No_Transaksi AND h.Status IS NULL "
			SQL = SQL & "LEFT JOIN (SELECT z.Kode_Perusahaan, z.Urut_Pallet AS Urut_Oto_Detail, CASE WHEN COUNT(*) > COUNT(z.Flag_Selesai) "
			SQL = SQL & "THEN NULL ELSE 'Y' END AS Flag_Selesai_Repack FROM N_EMI_Transaksi_Packing_Repacking z WHERE z.Status IS NULL "
			SQL = SQL & "GROUP BY z.Kode_Perusahaan, z.Urut_Pallet) i ON e.Kode_Perusahaan = i.Kode_Perusahaan AND e.Urut_Oto = i.Urut_Oto_Detail "
			SQL = SQL & "LEFT JOIN (SELECT z.Kode_Perusahaan, (y.Qr_Code + '-' + y.Kode_Unik_Berjalan) AS Barcode, l.Barcode_Packing, "
			SQL = SQL & "SUM(l.Jumlah) AS Jumlah_Awal FROM Emi_Production_Results_Validation z "
			SQL = SQL & "INNER JOIN Emi_Production_Results_Validation_Detail x ON z.Kode_Perusahaan = x.Kode_Perusahaan AND z.No_Transaksi = x.No_Transaksi "
			SQL = SQL & "INNER JOIN barang_sn y ON x.Kode_Perusahaan = y.Kode_Perusahaan AND x.Serial_Number_Awal = y.Serial_Number "
			SQL = SQL & "INNER JOIN Emi_Production_Results_Validation_Detail_Packing l ON x.Kode_Perusahaan = l.Kode_Perusahaan "
			SQL = SQL & "AND x.No_Transaksi = l.No_Transaksi_GR2 and (y.Qr_Code + '-' + y.Kode_Unik_Berjalan) = l.Barcode_GR1 WHERE z.Status IS NULL "
			SQL = SQL & "AND x.Jenis = 'FINISHED GOOD' AND x.Flag_Packing_Pallet = 'Y' and l.Jenis = 'PALLET' GROUP BY z.Kode_Perusahaan, (y.Qr_Code + '-' + y.Kode_Unik_Berjalan), l.Barcode_Packing) j "
			SQL = SQL & "ON e.Kode_Perusahaan = j.Kode_Perusahaan AND (f.Qr_Code + '-' + f.Kode_Unik_Berjalan) = j.Barcode AND a.Kode_Unik_Print = j.Barcode_Packing "
			SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' AND a.Status IS NULL AND a.Flag_Input_GR2 IS NULL "
			If Cmb_Lain.SelectedIndex > 0 Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & DataCmbLain_PalletPacking(Cmb_Lain.SelectedIndex).FilterSql & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
			End If
			SQL = SQL & "GROUP BY a.No_Transaksi, h.No_Production_Order, a.Kode_Unik_Print, a.Urut_Oto, b.Line, a.Jumlah, "
			SQL = SQL & "a.Tgl_Cetak, a.Jam_Cetak, f.Qr_Code, f.Kode_Unik_Berjalan, g.tahap, a.Flag_Approval_Android, j.Jumlah_Awal "
			SQL = SQL & "HAVING ISNULL((SUM(c.Jumlah) - ISNULL(j.Jumlah_Awal, 0)), 0) > 0 "
			SQL = SQL & "ORDER BY a.Tgl_Cetak, a.Jam_Cetak"

#End Region

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Packing_Pallet.Items.Add(Dr("No_Transaksi"))
					Lv.SubItems.Add(Dr("No_Split"))
					Lv.SubItems.Add(Dr("Line"))
					Lv.SubItems.Add(Dr("Barcode_Pallet"))
					Lv.SubItems.Add(Format(Dr("Jumlah_Pcs"), "N4"))
					Lv.SubItems.Add("Pcs")
					Lv.SubItems.Add(Dr("Barcode_GR1_Detail_Pallet"))
					Lv.SubItems.Add(Dr("Qr_Code"))
					Lv.SubItems.Add(Dr("Kode_Unik_Berjalan"))
					Lv.SubItems.Add(Dr("tahap"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Flag_Approval_Android")) = "", "T", Dr("Flag_Approval_Android")))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Flag_Repacking")) = "", "T", Dr("Flag_Repacking")))

					Dim searchBarcode As String = Dr("Barcode_Pallet").ToString().Trim()
					Dim searchBarcodeGR1 As String = Dr("Barcode_GR1_Detail_Pallet").ToString().Trim()

					Dim found_DetailPacking As Boolean = arrBarcodeFromParent_PalletPacking.Any(Function(x) x.BarcodePalletPack.Trim() = searchBarcode And (x.QRCode.Trim & "-" & x.KdUnikBerjalan.Trim) = searchBarcodeGR1)

					If found_DetailPacking Then
						Lv.Checked = True
					Else
						Lv.Checked = False
					End If

					If General_Class.CekNULL(Dr("Flag_Approval_Android")) = "Y" Then
						Lv.BackColor = Color.LightBlue
					End If
					If General_Class.CekNULL(Dr("Flag_Repacking")) = "Y" Then
						Lv.BackColor = Color.Tan
					End If

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Load_Data_Waste_Packing()
		Try
			OpenConn()

			Lv_Packing_Waste.Items.Clear()

			'SQL = "select top (300) a.No_Transaksi, d.No_Production_Order, b.Tahap as batch, a.Tgl_Produksi, a.Tgl_Expired, "
			'SQL = SQL & "(c.Qr_Code+'-'+c.Kode_Unik_Berjalan) as Barcode_GR1, b.Qr_Code, b.Kode_Unik_Berjalan, "
			'SQL = SQL & "a.jumlah as Jumlah_FG,a.Jumlah_Waste as Jumlah_Waste_Sblm_Gr2, "
			'SQL = SQL & "(a.Jumlah_Waste - isnull(e.Jumlah_Awal, 0)) as Jumlah_Waste, 'Pcs' as Satuan_Waste "
			'SQL = SQL & "from N_EMI_Transaksi_Packing_Detail a "
			'SQL = SQL & "inner join Emi_Production_Results_Detail_Pallet b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.SN_Baru = b.SN_Baru "
			'SQL = SQL & "inner join barang_sn c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.SN_Baru = c.Serial_Number "
			'SQL = SQL & "inner join Emi_Production_Results d on b.Kode_Perusahaan= d.Kode_Perusahaan and b.No_Transaksi = d.No_Transaksi "
			'SQL = SQL & "left join ( "
			'SQL = SQL & "select z.Kode_Perusahaan, x.Serial_Number_Awal, x.Jumlah_Awal "
			'SQL = SQL & "from Emi_Production_Results_Validation z "
			'SQL = SQL & "inner join Emi_Production_Results_Validation_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Transaksi = x.No_Transaksi "
			'SQL = SQL & "where z.Status is null and x.Jenis <> 'FINISHED GOOD' "
			'SQL = SQL & ") e on a.Kode_Perusahaan = e.Kode_Perusahaan and a.SN_Baru = e.Serial_Number_Awal "
			'SQL = SQL & "where d.Status is null "
			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and (a.Jumlah_Waste - isnull(e.Jumlah_Awal, 0)) > 0 "
			'If Cmb_Lain.SelectedIndex > 0 Then
			'	If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

			'	SQL = SQL & DataCmbLain_WastePacking(Cmb_Lain.SelectedIndex).FilterSql & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
			'End If
			'SQL = SQL & "order by a.No_Transaksi "

			SQL = "SELECT TOP (300) a.No_Transaksi, d.No_Production_Order, b.Tahap AS batch, a.Tgl_Produksi, a.Tgl_Expired, "
			SQL = SQL & "(c.Qr_Code + '-' + c.Kode_Unik_Berjalan) AS Barcode_GR1, b.Qr_Code, b.Kode_Unik_Berjalan, "
			SQL = SQL & "a.jumlah AS Jumlah_FG, a.Jumlah_Waste AS Jumlah_Waste_Sblm_Gr2, "
			SQL = SQL & "(a.Jumlah_Waste - ISNULL(e.Jumlah_Awal, 0)) AS Jumlah_Waste, 'Pcs' AS Satuan_Waste "
			SQL = SQL & "FROM N_EMI_Transaksi_Packing_Detail a "
			SQL = SQL & "INNER JOIN Emi_Production_Results_Detail_Pallet b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.SN_Baru = b.SN_Baru "
			SQL = SQL & "INNER JOIN barang_sn c ON a.Kode_Perusahaan = c.Kode_Perusahaan AND a.SN_Baru = c.Serial_Number "
			SQL = SQL & "INNER JOIN Emi_Production_Results d ON b.Kode_Perusahaan = d.Kode_Perusahaan AND b.No_Transaksi = d.No_Transaksi "
			SQL = SQL & "LEFT JOIN (SELECT z.Kode_Perusahaan, (k.Qr_Code + '-' + k.Kode_Unik_Berjalan) AS Barcode, SUM(x.Jumlah_Awal) AS Jumlah_Awal "
			SQL = SQL & "FROM Emi_Production_Results_Validation z INNER JOIN Emi_Production_Results_Validation_Detail x ON z.Kode_Perusahaan = x.Kode_Perusahaan "
			SQL = SQL & "AND z.No_Transaksi = x.No_Transaksi INNER JOIN barang_sn k ON x.Kode_Perusahaan = k.Kode_Perusahaan AND x.Serial_Number_Awal = k.Serial_Number "
			SQL = SQL & "WHERE z.Status IS NULL AND x.Jenis <> 'FINISHED GOOD' AND x.Flag_Packing_Waste = 'Y' "
			SQL = SQL & "GROUP BY z.Kode_Perusahaan, (k.Qr_Code + '-' + k.Kode_Unik_Berjalan)) e ON a.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "AND (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) = e.Barcode "
			SQL = SQL & "WHERE d.Status IS NULL AND a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			If Cmb_Lain.SelectedIndex > 0 Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & DataCmbLain_WastePacking(Cmb_Lain.SelectedIndex).FilterSql & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
			End If
			SQL = SQL & "AND (a.Jumlah_Waste - ISNULL(e.Jumlah_Awal, 0)) > 0 "
			SQL = SQL & "ORDER BY a.No_Transaksi "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Packing_Waste.Items.Add(Dr("No_Transaksi"))
					Lv.SubItems.Add(Dr("No_Production_Order"))
					Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
					Lv.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Barcode_GR1"))
					Lv.SubItems.Add(Format(Dr("Jumlah_Waste"), "N4"))
					Lv.SubItems.Add(Dr("Satuan_Waste"))
					Lv.SubItems.Add(Dr("Qr_Code"))
					Lv.SubItems.Add(Dr("Kode_Unik_Berjalan"))

					Dim Barcode As String = Dr("Barcode_GR1").ToString().Trim()
					Dim isExist As Boolean = arrBarcodeFromParent_WastePacking.Any(Function(x) (x.QRCode & "-" & x.KdUnikBerjalan) = Barcode)
					If isExist Then
						Lv.Checked = True
					Else
						Lv.Checked = False
					End If

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Lv_Data_Sum_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles Lv_Data.ItemChecked
		If Lv_Data.Items.Count = 0 Then Exit Sub

		If Lv_Data.CheckedItems.Count = 0 Then
			SelectedSplit = ""
			CurrentVariant = ""
		End If

		Dim SelectedSplitParent As String = EMI_Validasi_GR.SelectedCurrentSplit
		Dim SelectedVarian As String = EMI_Validasi_GR.SelectedVariant

		If Not Lv_Data.FocusedItem Is Nothing AndAlso Lv_Data.FocusedItem.Checked Then

			Dim CheckedVarian As String = Lv_Data.FocusedItem.SubItems(item_KdBarang).Text

			Dim Barcode As String = Lv_Data.FocusedItem.SubItems(item_Barcode).Text
			Dim QrCode As String = Lv_Data.FocusedItem.SubItems(item_QrCode).Text
			Dim KdUnikBerjalan As String = Lv_Data.FocusedItem.SubItems(item_KdUnikBerjalan).Text

			'================================================================================
			'=     BARCODE YANG SUDAH DI CHECK PACKING TIDAK BOLEH DI CHECK LAGI DI GR1     =
			'================================================================================

			Dim foundQrCode As Boolean = arrBarcodeFromParent.Any(Function(dict) dict.ContainsKey("QrCode") AndAlso dict("QrCode") = QrCode)
			Dim foundKodeUnik As Boolean = arrBarcodeFromParent.Any(Function(dict) dict.ContainsKey("KdUnikBerjalan") AndAlso dict("KdUnikBerjalan").Trim.ToUpper() = KdUnikBerjalan.ToString.Trim.ToUpper)

			If foundQrCode And foundKodeUnik Then
				Lv_Data.FocusedItem.Checked = False
				Exit Sub
			End If

			If Fitur_Military_Sampling Then
				If Lv_Data.FocusedItem.SubItems(item_StatMilitarySampling).Text = "T" Then
					Lv_Data.FocusedItem.Checked = False
					Exit Sub
				End If
			End If

			If MenuAsal <> "VALIDASI_GR_MERGE" Then
				If SelectedSplitParent = "" Then
					If SelectedSplit = "" Then
						SelectedSplit = Lv_Data.FocusedItem.Text
					Else
						If Lv_Data.FocusedItem.Text <> SelectedSplit Then
							Lv_Data.FocusedItem.Checked = False
						End If
					End If
				Else
					If Lv_Data.FocusedItem.Text <> SelectedSplitParent Then
						Lv_Data.FocusedItem.Checked = False
					End If
				End If
			End If

			If SelectedVarian = "" Then
				If CurrentVariant = "" Then
					CurrentVariant = CheckedVarian
				Else
					If CheckedVarian <> CurrentVariant Then
						Lv_Data.FocusedItem.Checked = False
					End If
				End If
			Else
				If CheckedVarian <> SelectedVarian Then
					Lv_Data.FocusedItem.Checked = False
				End If
			End If

		End If

	End Sub

	Private Sub Lv_Pallet_Packing_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles Lv_Packing_Pallet.ItemChecked
		If Lv_Packing_Pallet.Items.Count = 0 Then Exit Sub

		If Lv_Packing_Pallet.CheckedItems.Count = 0 Then
			SelectedSplit = ""
			CurrentVariant = ""
		End If

		Dim SelectedSplitParent As String = EMI_Validasi_GR.SelectedCurrentSplit
		Dim SelectedVarian As String = EMI_Validasi_GR.SelectedVariant

		If Not Lv_Packing_Pallet.FocusedItem Is Nothing AndAlso Lv_Packing_Pallet.FocusedItem.Checked Then

			Dim Barcode As String = Lv_Packing_Pallet.FocusedItem.SubItems(ItemPalletPack_BarcodeGr1).Text
			Dim QrCode As String = Lv_Packing_Pallet.FocusedItem.SubItems(ItemPalletPack_QrCode).Text
			Dim KdUnikBerjalan As String = Lv_Packing_Pallet.FocusedItem.SubItems(ItemPalletPack_KodeUnikBerjalan).Text
			Dim IsRepacking As String = Lv_Packing_Pallet.FocusedItem.SubItems(ItemPalletPack_FlagRepacking).Text
			Dim IsValidasiAndroid As String = Lv_Packing_Pallet.FocusedItem.SubItems(ItemPalletPack_FlagValidasiAndroid).Text

			If IsRepacking.Trim = "Y" Then
				Lv_Packing_Pallet.FocusedItem.Checked = False
				Exit Sub
			ElseIf IsValidasiAndroid.Trim = "T" Then
				Lv_Packing_Pallet.FocusedItem.Checked = False
				Exit Sub
			End If

			If Lv_Packing_Pallet.CheckedItems.Count > 1 Then
				Lv_Packing_Pallet.FocusedItem.Checked = False
				Exit Sub
			End If

			'================================================================================
			'=     BARCODE YANG SUDAH DI CHECK PACKING TIDAK BOLEH DI CHECK LAGI DI GR1     =
			'================================================================================

			Dim isExist As Boolean = arrBarcodeFromParent_PalletPacking.Any(Function(x) (x.QRCode & "-" & x.KdUnikBerjalan) = Barcode)

			If isExist Then
				Lv_Packing_Pallet.FocusedItem.Checked = False
				Exit Sub
			End If

			If Fitur_Military_Sampling Then
				If Lv_Packing_Pallet.FocusedItem.SubItems(item_StatMilitarySampling).Text = "T" Then
					Lv_Packing_Pallet.FocusedItem.Checked = False
					Exit Sub
				End If
			End If

			If MenuAsal <> "VALIDASI_GR_MERGE" Then
				If SelectedSplitParent = "" Then
					If SelectedSplit = "" Then
						SelectedSplit = Lv_Packing_Pallet.FocusedItem.SubItems(ItemPalletPack_NoSplit).Text
					Else
						If Lv_Packing_Pallet.FocusedItem.SubItems(ItemPalletPack_NoSplit).Text <> SelectedSplit Then
							Lv_Packing_Pallet.FocusedItem.Checked = False
						End If
					End If
				Else
					If Lv_Packing_Pallet.FocusedItem.SubItems(ItemPalletPack_NoSplit).Text <> SelectedSplitParent Then
						Lv_Packing_Pallet.FocusedItem.Checked = False
					End If
				End If
			End If

		End If

	End Sub

	Private Sub Lv_Packing_Waste_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles Lv_Packing_Waste.ItemChecked
		If Lv_Packing_Waste.Items.Count = 0 Then Exit Sub

		If Lv_Packing_Waste.CheckedItems.Count = 0 Then
			SelectedSplit = ""
			CurrentVariant = ""
		End If

		Dim SelectedSplitParent As String = EMI_Validasi_GR.SelectedCurrentSplit
		Dim SelectedVarian As String = EMI_Validasi_GR.SelectedVariant

		If Not Lv_Packing_Waste.FocusedItem Is Nothing AndAlso Lv_Packing_Waste.FocusedItem.Checked Then

			Dim Barcode As String = Lv_Packing_Waste.FocusedItem.SubItems(ItemWastePack_Barcode).Text
			Dim QrCode As String = Lv_Packing_Waste.FocusedItem.SubItems(ItemWastePack_QrCode).Text
			Dim KdUnikBerjalan As String = Lv_Packing_Waste.FocusedItem.SubItems(ItemWastePack_KdUnikBerjalan).Text

			'================================================================================
			'=     BARCODE YANG SUDAH DI CHECK PACKING TIDAK BOLEH DI CHECK LAGI DI GR1     =
			'================================================================================

			Dim isExist As Boolean = arrBarcodeFromParent_WastePacking.Any(Function(x) (x.QRCode & "-" & x.KdUnikBerjalan) = Barcode)

			If isExist Then
				Lv_Packing_Waste.FocusedItem.Checked = False
				Exit Sub
			End If

			If Fitur_Military_Sampling Then
				If Lv_Packing_Waste.FocusedItem.SubItems(item_StatMilitarySampling).Text = "T" Then
					Lv_Packing_Waste.FocusedItem.Checked = False
					Exit Sub
				End If
			End If

			If MenuAsal <> "VALIDASI_GR_MERGE" Then
				If SelectedSplitParent = "" Then
					If SelectedSplit = "" Then
						SelectedSplit = Lv_Packing_Waste.FocusedItem.SubItems(ItemWastePack_NoSplit).Text
					Else
						If Lv_Packing_Waste.FocusedItem.SubItems(ItemWastePack_NoSplit).Text <> SelectedSplit Then
							Lv_Packing_Waste.FocusedItem.Checked = False
						End If
					End If
				Else
					If Lv_Packing_Waste.FocusedItem.SubItems(ItemWastePack_NoSplit).Text <> SelectedSplitParent Then
						Lv_Packing_Waste.FocusedItem.Checked = False
					End If
				End If
			End If

		End If
	End Sub

	Private Sub Btn_Tambah_Click(sender As Object, e As EventArgs) Handles Btn_Tambah.Click

		If TabControl1.SelectedIndex = 0 Then
			If Lv_Data.Items.Count = 0 Then Exit Sub
		ElseIf TabControl1.SelectedIndex = 1 Then
			If Lv_Packing_Pallet.Items.Count = 0 Then Exit Sub
		ElseIf TabControl1.SelectedIndex = 2 Then
			If Lv_Packing_Waste.Items.Count = 0 Then Exit Sub
		End If

		Dim hasData As Boolean = False
		Dim IsDataPacking As Boolean = False
		Dim IsDataWaste As Boolean = False
		arrSelectedBarcode.Clear()
		For i As Integer = 0 To Lv_Data.Items.Count - 1
			If Lv_Data.Items(i).Checked = True Then
				Get_Data_Listview(i)

				If MenuAsal <> "VALIDASI_GR_MERGE" Then
					If EMI_Validasi_GR.SelectedCurrentSplit.Trim.Length = 0 Then
						EMI_Validasi_GR.SelectedCurrentSplit = Lv_NoSplit
					Else
						If EMI_Validasi_GR.SelectedCurrentSplit.Trim.ToUpper <> Lv_NoSplit.Trim.ToUpper Then
							Continue For
						End If
					End If
				End If

				hasData = True

				Dim Dict As New Dictionary(Of String, String)
				Dict("QrCode") = Lv_QrCode
				Dict("KdUnikBerjalan") = Lv_KdUnikBerjalan

				arrSelectedBarcode.Add(Dict)
				IsDataPacking = False
				IsDataWaste = False
			End If
		Next

		arrSelectedBarcode_PalletPacking.Clear()
		For i As Integer = 0 To Lv_Packing_Pallet.Items.Count - 1
			If Lv_Packing_Pallet.Items(i).Checked = True Then
				Get_Data_Lv_Packing_Pallet(i)

				If LvPack_FlagRepacking.Trim = "Y" Then
					MessageBox.Show($"Barcode {LvPack_Barcode.Trim} Masih dalam Proses Repacking", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				ElseIf LvPack_FlagValidasiAndroid.Trim = "T" Then
					MessageBox.Show($"Barcode {LvPack_Barcode.Trim} Belum Divalidasi Android", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				If MenuAsal <> "VALIDASI_GR_MERGE" Then
					If EMI_Validasi_GR.SelectedCurrentSplit.Trim.Length = 0 Then
						EMI_Validasi_GR.SelectedCurrentSplit = LvPack_NoSplit
					Else
						If EMI_Validasi_GR.SelectedCurrentSplit.Trim.ToUpper <> LvPack_NoSplit.Trim.ToUpper Then
							Continue For
						End If
					End If
				End If

				hasData = True

				Dim Dict As New Dictionary(Of String, String)
				arrSelectedBarcode_PalletPacking.Add((LvPack_QrCode, LvPack_KdUnikBerjalan, LvPack_Barcode))
				IsDataPacking = True
				IsDataWaste = False

			End If
		Next

		arrSelectedBarcode_WastePacking.Clear()
		For i As Integer = 0 To Lv_Packing_Waste.Items.Count - 1
			If Lv_Packing_Waste.Items(i).Checked = True Then
				Get_Data_Lv_Packing_Waste(i)

				If MenuAsal <> "VALIDASI_GR_MERGE" Then
					If EMI_Validasi_GR.SelectedCurrentSplit.Trim.Length = 0 Then
						EMI_Validasi_GR.SelectedCurrentSplit = LvPackWaste_NoSplit
					Else
						If EMI_Validasi_GR.SelectedCurrentSplit.Trim.ToUpper <> LvPackWaste_NoSplit.Trim.ToUpper Then
							Continue For
						End If
					End If
				End If

				hasData = True

				Dim Dict As New Dictionary(Of String, String)
				arrSelectedBarcode_WastePacking.Add((LvPackWaste_QrCode, LvPackWaste_KdUnikBerjalan))
				IsDataPacking = False
				IsDataWaste = True

			End If
		Next

		If Not hasData Then
			MessageBox.Show("Tidak Ada Data yang Ditambahkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Lv_Data.Focus() : Exit Sub
		End If

		EMI_Validasi_GR.arrBarcodeFromSD = arrSelectedBarcode
		EMI_Validasi_GR.arrBarcodeFromSD_Pallet_Packing = arrSelectedBarcode_PalletPacking
		EMI_Validasi_GR.arrBarcodeFromSD_Waste_Packing = arrSelectedBarcode_WastePacking
		EMI_Validasi_GR.LoadFromSD()

		'Kosong()
		Me.Close()

	End Sub

	Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
		Kosong()
		Me.Close()
	End Sub

	'============================================================================================================================================
	'=     HANDLE KEY PRESS
	'============================================================================================================================================

	Private Sub Cmb_Lain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lain.SelectedIndexChanged
		If Cmb_Lain.SelectedIndex = 0 Then
			Txt_ValueLain.Enabled = False
		Else
			Txt_ValueLain.Enabled = True
		End If
		Txt_ValueLain.Text = ""
	End Sub

	Private Sub Cmb_Periode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Periode.SelectedIndexChanged
		If Cmb_Periode.SelectedIndex = 0 Then
			Tgl1.Enabled = False : Tgl2.Enabled = False
		Else
			Tgl1.Enabled = True : Tgl2.Enabled = True
		End If
		Tgl1.Value = Now : Tgl2.Value = Now
	End Sub

	Private Sub Cmb_Periode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Periode.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Periode.SelectedIndex = 0 Then
				Cmb_Lain.DroppedDown = True
				Cmb_Lain.Focus()
			Else
				Tgl1.Focus()
			End If
		End If
	End Sub

	Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
		If e.KeyChar = Chr(13) Then Tgl2.Focus()
	End Sub

	Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Lain.DroppedDown = True
			Cmb_Lain.Focus()
		End If
	End Sub

	Private Sub Cmb_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lain.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Periode.SelectedIndex = 0 Then
				Btn_Cari.Focus()
			Else
				Txt_ValueLain.Focus()
			End If
		End If
	End Sub

	Private Sub Txt_ValueLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ValueLain.KeyPress
		If e.KeyChar = Chr(13) Then
			Btn_Cari.Focus()
			e.Handled = True
		End If
	End Sub

	Private Sub Lv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data.MouseMove, Lv_Packing_Pallet.MouseMove, Lv_Packing_Waste.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	Private Sub EnableDoubleBuffer(lvw As ListView)
		Dim t As Type = lvw.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(lvw, True, Nothing)
	End Sub

	Private Sub HandleListViewHover(lvw As ListView, e As MouseEventArgs)
		Dim hit As ListViewHitTestInfo = lvw.HitTest(e.Location)
		lvw.Cursor = If(hit.Item IsNot Nothing, Cursors.Hand, Cursors.Default)

		If hit.Item IsNot lastHoverItem Then
			lvw.BeginUpdate()

			If lastHoverItem IsNot Nothing Then
				lastHoverItem.BackColor = originalItemColor
			End If

			If hit.Item IsNot Nothing AndAlso hit.Item.Tag Is Nothing Then
				lastHoverItem = hit.Item
				originalItemColor = lastHoverItem.BackColor

				Dim amt As Integer = 10
				lastHoverItem.BackColor = Color.FromArgb(
				Math.Max(0, originalItemColor.R - amt),
				Math.Max(0, originalItemColor.G - amt),
				Math.Max(0, originalItemColor.B - amt)
			)
			Else
				lastHoverItem = Nothing
			End If

			lvw.EndUpdate()
		End If
	End Sub

End Class