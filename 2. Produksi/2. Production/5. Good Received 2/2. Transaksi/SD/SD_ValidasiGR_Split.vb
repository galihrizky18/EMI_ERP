Public Class SD_ValidasiGR_Split

	Dim arrPeriode As New ArrayList

	Dim arrSelectedBarcode As New List(Of Dictionary(Of String, String))
	Public arrBarcodeFromParent As New List(Of Dictionary(Of String, String))
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

	Dim LvPack_NoTransaksi, LvPack_NoSplit, LvPack_Line, LvPack_Barcode, LvPack_Jumlah, LvPack_Satuan, LvPack_BarcodeGR1, LvPack_QrCode, LvPack_KdUnikBerjalan, LvPack_Batch As String

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

	Private Sub SD_ValidasiGR_Split_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

		Lv_Pallet_Packing.Columns.Clear()
		Lv_Pallet_Packing.Columns.Add("No Transaksi", 120, HorizontalAlignment.Left) '0
		Lv_Pallet_Packing.Columns.Add("No Split", 120, HorizontalAlignment.Left) '1
		Lv_Pallet_Packing.Columns.Add("Line", 80, HorizontalAlignment.Center) '2
		Lv_Pallet_Packing.Columns.Add("Barcode", 150, HorizontalAlignment.Left) '3
		Lv_Pallet_Packing.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '4
		Lv_Pallet_Packing.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '5
		Lv_Pallet_Packing.Columns.Add("BarcodeGR1", 150, HorizontalAlignment.Left).DisplayIndex = 4 '6
		Lv_Pallet_Packing.Columns.Add("QrCode", 0, HorizontalAlignment.Left) '7
		Lv_Pallet_Packing.Columns.Add("KodeUnikBerjalan", 0, HorizontalAlignment.Left) '8
		Lv_Pallet_Packing.Columns.Add("Batch", 80, HorizontalAlignment.Center).DisplayIndex = 2 '9
		Lv_Pallet_Packing.View = View.Details

		Cmb_Periode.Items.Clear() : arrPeriode.Clear()
		Cmb_Periode.Items.Add(OpsiSeluruh) : arrPeriode.Add(OpsiSeluruh)
		Cmb_Periode.Items.Add("Tanggal Produksi") : arrPeriode.Add("a.Tgl_Produksi")
		Cmb_Periode.Items.Add("Tanggal Expired") : arrPeriode.Add("a.Tgl_Expired")
		Cmb_Periode.SelectedIndex = 0

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

		SelectedSplit = ""
		CurrentVariant = ""

		Tgl1.Enabled = False : Tgl2.Enabled = False

		TabControl1.SelectedIndex = 0
		TabControl1_SelectedIndexChanged(TabControl1, EventArgs.Empty)

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

		LvPack_NoTransaksi = Lv_Pallet_Packing.Items(index).SubItems(ItemPalletPack_NoTransaksi).Text
		LvPack_NoSplit = Lv_Pallet_Packing.Items(index).SubItems(ItemPalletPack_NoSplit).Text
		LvPack_Line = Lv_Pallet_Packing.Items(index).SubItems(ItemPalletPack_Line).Text
		LvPack_Barcode = Lv_Pallet_Packing.Items(index).SubItems(ItemPalletPack_Barcode).Text
		LvPack_Jumlah = Lv_Pallet_Packing.Items(index).SubItems(ItemPalletPack_Jumlah).Text
		LvPack_Satuan = Lv_Pallet_Packing.Items(index).SubItems(ItemPalletPack_Satuan).Text
		LvPack_BarcodeGR1 = Lv_Pallet_Packing.Items(index).SubItems(ItemPalletPack_BarcodeGr1).Text
		LvPack_QrCode = Lv_Pallet_Packing.Items(index).SubItems(ItemPalletPack_QrCode).Text
		LvPack_KdUnikBerjalan = Lv_Pallet_Packing.Items(index).SubItems(ItemPalletPack_KodeUnikBerjalan).Text
		LvPack_Batch = Lv_Pallet_Packing.Items(index).SubItems(ItemPalletPack_Batch).Text

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

		End If

	End Sub

	Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

		Cmb_Lain.Items.Clear()
		Cmb_Lain.SelectedIndex = -1
		If TabControl1.SelectedIndex = 0 Then

			For Each Datas In DataCmbLain_GR1
				Cmb_Lain.Items.Add(Datas.Combobox)
			Next

			If Lv_Data.Items.Count = 0 Then
				Load_Data_GR_1()
			End If
		ElseIf TabControl1.SelectedIndex = 1 Then
			For Each Datas In DataCmbLain_PalletPacking
				Cmb_Lain.Items.Add(Datas.Combobox)
			Next
			If Lv_Pallet_Packing.Items.Count = 0 Then
				Load_Data_Pallet_Packing()
			End If
		ElseIf TabControl1.SelectedIndex = 2 Then
			For Each Datas In DataCmbLain_PalletPacking
				Cmb_Lain.Items.Add(Datas.Combobox)
			Next
		End If

		Cmb_Lain.SelectedIndex = 0
		Btn_Cari_Click(Me, New EventArgs)
	End Sub

	Private Sub Load_Data_GR_1()
		Try
			OpenConn()

			Lv_Data.Items.Clear()

			SQL = "select top(300) b.No_Production_Order as No_Split, a.Lokasi_Gudang as Kode_Stock_Owner, a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Batch_Number,  "
			SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama as Nama_Barang, a.Nomor, "
			'SQL = SQL & "sum(f.Jumlah) as Jumlah, "

			SQL = SQL & "isnull((sum(f.Jumlah) -  "
			SQL = SQL & "(select isnull(sum(jumlah), 0) from N_EMI_Validation_GR_Temp z "
			SQL = SQL & "where b.kode_perusahaan = z.kode_perusahaan "
			SQL = SQL & "and b.no_production_order = z.no_production_order "
			SQL = SQL & "and z.barcode = (a.Qr_Code + '-' + a.Kode_Unik_Berjalan)) ), 0) as Jumlah, "

			SQL = SQL & "a.Satuan, a.Jenis, e.Keterangan as Kualitas, a.proses, "

			SQL = SQL & "isnull(( "
			SQL = SQL & "select top 1 'Y' from N_EMI_Military_Sampling z "
			SQL = SQL & "where z.kode_perusahaan = a.Kode_Perusahaan and z.status is null "
			SQL = SQL & "and z.No_Split = b.No_Production_Order and z.No_Batch = a.tahap "
			SQL = SQL & "and z.No_GR = '1' "
			SQL = SQL & "), 'T') as Status_Military_Sampling, "

			SQL = SQL & "isnull(( "
			SQL = SQL & "select isnull(flag_commercial, 'T') "
			SQL = SQL & "from emi_split_production_order x, emi_order_produksi y where "
			SQL = SQL & "x.kode_perusahaan =y.kode_perusahaan and x.no_po=y.no_faktur and x.status is null and y.status is null "
			SQL = SQL & "and b.kode_perusahaan=x.kode_perusahaan and b.no_production_order=x.no_transaksi), null) as Flag_Commercial "

			SQL = SQL & "from Emi_Production_Results_Detail_Pallet a, Emi_Production_Results b, EMI_Production_Results_Detail_Barang c, barang d, EMI_Master_Warna e, Barang_SN f "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and a.Proses = c.Proses "
			SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
			SQL = SQL & "and a.Jenis = e.Kode_Warna "
			SQL = SQL & "and a.SN_Baru = f.Serial_Number "
			SQL = SQL & "and b.Status is null "
			SQL = SQL & "and a.Lokasi_Gudang in (select Kode_Stock_Owner from Stock_Owner_Gudang z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Flag_QI = 'Y') "

			If Cmb_Periode.SelectedIndex > 0 Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & arrPeriode(Cmb_Periode.SelectedIndex) & " between '"
				SQL = SQL & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
			End If

			If Cmb_Lain.SelectedIndex > 0 Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & DataCmbLain_GR1(Cmb_Lain.SelectedIndex).FilterSql & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
			End If

			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and f.jumlah<>0 "
			SQL = SQL & "group by  b.No_Production_Order, a.Lokasi_Gudang , a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) , a.Batch_Number, "
			SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama, a.Satuan, a.Jenis, e.Keterangan, b.kode_perusahaan, a.Nomor, a.Kode_Perusahaan, a.Proses, a.tahap "
			SQL = SQL & "order by  b.No_Production_Order, a.Nomor, a.Lokasi_Gudang, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan), a.Tgl_Expired ASC "

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

			Lv_Pallet_Packing.Items.Clear()
			SQL = "select a.No_Transaksi, h.No_Production_Order as No_Split, a.Kode_Unik_Print as Barcode_Pallet, a.Urut_Oto as Urut_Pallet, b.Line, "
			SQL = SQL & "a.Jumlah, 'Box' as Satuan_Pallet, sum(c.Jumlah) as Jumlah_Pcs, "
			SQL = SQL & "a.Tgl_Cetak, a.Jam_Cetak, (f.Qr_Code+'-'+f.Kode_Unik_Berjalan) as Barcode_GR1_Detail_Pallet, f.Qr_Code, f.Kode_Unik_Berjalan, g.tahap "
			SQL = SQL & "from N_EMI_Transaksi_Packing_Pallet a "
			SQL = SQL & "inner join N_EMI_Transaksi_Packing_Box b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Oto = b.Urut_Pallet and b.Status is null "
			SQL = SQL & "inner join N_EMI_Transaksi_Packing_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_Oto = c.Urut_Transaksi_Box and c.Status is null "
			SQL = SQL & "inner join N_EMI_Transaksi_Packing_Detail e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Urut_Detail_Transaksi_Packing = e.Urut_Oto "
			SQL = SQL & "inner join Barang_SN f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.SN_Baru = f.Serial_Number "
			SQL = SQL & "inner join Emi_Production_Results_Detail_Pallet g on e.Kode_Perusahaan = g.Kode_Perusahaan and e.SN_Baru = g.Serial_Number "
			SQL = SQL & "inner join Emi_Production_Results h on h.Kode_Perusahaan = h.Kode_Perusahaan and g.No_Transaksi = h.No_Transaksi and h.Status is null "
			SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Status is null "
			If Cmb_Lain.SelectedIndex > 0 Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & DataCmbLain_PalletPacking(Cmb_Lain.SelectedIndex).FilterSql & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
			End If
			SQL = SQL & "group by a.No_Transaksi, h.No_Production_Order, a.Kode_Unik_Print, a.Urut_Oto, b.Line, a.Jumlah, a.Tgl_Cetak, a.Jam_Cetak, (f.Qr_Code+'-'+f.Kode_Unik_Berjalan), f.Qr_Code, f.Kode_Unik_Berjalan, g.tahap "
			SQL = SQL & "order by a.Tgl_Cetak, a.Jam_Cetak "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Pallet_Packing.Items.Add(Dr("No_Transaksi"))
					Lv.SubItems.Add(Dr("No_Split"))
					Lv.SubItems.Add(Dr("Line"))
					Lv.SubItems.Add(Dr("Barcode_Pallet"))
					Lv.SubItems.Add(Format(Dr("Jumlah_Pcs"), "N4"))
					Lv.SubItems.Add("Pcs")
					Lv.SubItems.Add(Dr("Barcode_GR1_Detail_Pallet"))
					Lv.SubItems.Add(Dr("Qr_Code"))
					Lv.SubItems.Add(Dr("Kode_Unik_Berjalan"))
					Lv.SubItems.Add(Dr("tahap"))
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

	Private Sub Btn_Tambah_Click(sender As Object, e As EventArgs) Handles Btn_Tambah.Click

		If TabControl1.SelectedIndex = 0 Then
			If Lv_Data.Items.Count = 0 Then Exit Sub
		ElseIf TabControl1.SelectedIndex = 1 Then
			If Lv_Pallet_Packing.Items.Count = 0 Then Exit Sub
		ElseIf TabControl1.SelectedIndex = 2 Then
			If Lv_Waste_Packing.Items.Count = 0 Then Exit Sub
		End If

		Dim hasData As Boolean = False
		Dim IsDataPacking As Boolean = False
		If TabControl1.SelectedIndex = 0 Then
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

				End If
			Next
		ElseIf TabControl1.SelectedIndex = 1 Then
			For i As Integer = 0 To Lv_Pallet_Packing.Items.Count - 1
				If Lv_Pallet_Packing.Items(i).Checked = True Then
					Get_Data_Lv_Packing_Pallet(i)

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
					Dict("QrCode") = LvPack_QrCode
					Dict("KdUnikBerjalan") = LvPack_KdUnikBerjalan

					arrSelectedBarcode.Add(Dict)
					IsDataPacking = True

				End If
			Next
		ElseIf TabControl1.SelectedIndex = 2 Then

		End If

		If Not hasData Then
			MessageBox.Show("Tidak Ada Data yang Ditambahkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Lv_Data.Focus() : Exit Sub
		End If

		EMI_Validasi_GR.arrBarcodeFromSD = arrSelectedBarcode
		EMI_Validasi_GR.LoadFromSD(IsDataPacking)

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

End Class