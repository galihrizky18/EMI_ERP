Public Class N_EMI_Transaksi_Waste_Proses

	Public Property asal_menu As String
	'asal_menu = PROCESS
	'asal_menu = PRODUCT

	Dim Bulan_Romawi As New List(Of (Bulan As Integer, Romawi As String)) From {
			(1, "I"), (2, "II"), (3, "III"), (4, "IV"), (5, "V"), (6, "VI"), (7, "VII"),
			(8, "VIII"), (9, "IX"), (10, "X"), (11, "XI"), (12, "XII")
		}

	Dim Lv_NoTransaksi, Lv_NoSplit, Lv_Tanggal, Lv_Jam, Lv_Lokasi, Lv_Kd_Barang, Lv_Nm_Barang, Lv_Barcode, Lv_Jumlah, Lv_Satuan, Lv_Asal As String

	Dim item_NoTransaksi As Integer = 0
	Dim item_NoSplit As Integer = 1
	Dim item_Tanggal As Integer = 2
	Dim item_Jam As Integer = 3
	Dim item_Lokasi As Integer = 4
	Dim item_Kd_Barang As Integer = 5
	Dim item_Nm_Barang As Integer = 6
	Dim item_Barcode As Integer = 7
	Dim item_Jumlah As Integer = 8
	Dim item_Satuan As Integer = 9
	Dim item_Asal As Integer = 10

	Dim Initial_Faktur As String = ""

	Dim arr_Rekap_Data As New List(Of (kd_Barang As String, jumlah As Double, satuan As String, lokasi As String))

	Dim Random As New Random()

	Private Sub N_EMI_Transaksi_Waste_Proses_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Lv_Data.Columns.Clear()
		Lv_Data.Columns.Add("No Transaksi", 140, HorizontalAlignment.Left) '0
		Lv_Data.Columns.Add("No Split", 130, HorizontalAlignment.Left) '1
		Lv_Data.Columns.Add("Tanggal", 120, HorizontalAlignment.Center) '2
		Lv_Data.Columns.Add("Jam", 90, HorizontalAlignment.Center) '3
		Lv_Data.Columns.Add("Lokasi", 150, HorizontalAlignment.Left) '4
		Lv_Data.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '5
		Lv_Data.Columns.Add("Barang", 200, HorizontalAlignment.Left) '6
		Lv_Data.Columns.Add("Barcode", 250, HorizontalAlignment.Left) '7
		Lv_Data.Columns.Add("Jumlah", 130, HorizontalAlignment.Right) '8
		Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '9
		Lv_Data.Columns.Add("Asal", 150, HorizontalAlignment.Center) '10
		Lv_Data.View = View.Details

		Lv_Data.Columns(10).DisplayIndex = 2

		Try
			OpenConn()

			Cmb_Gudang.Items.Clear()
			SQL = "select Kode_Stock_Owner "
			SQL &= $"from Stock_Owner_Gudang "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			If asal_menu.ToString.ToUpper = "PROCESS" Then
				SQL &= $"and Flag_Waste_Process = 'Y' "
			ElseIf asal_menu.ToString.ToUpper = "PRODUCT" Then
				SQL &= $"and Flag_Waste_Process_Product = 'Y' "
			End If
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Gudang.Items.Add(Dr("Kode_Stock_Owner"))
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

	Private Sub get_no_faktur_pemusnahan()
		Dim FPro_Results As String = "PMB-"
		Txt_No_Transaksi.Text = FPro_Results & Initial_Faktur & "-" & Format(tgl_skg, "MM/yy") & "-" &
									  General_Class.Get_Last_Number2("N_EMI_Transaksi_Transfer_Waste", "no_faktur", JumlahDigit,
									  "Kode_perusahaan", KodePerusahaan,
									  "And", "substring(no_faktur,1," & Len(FPro_Results) + Len(Initial_Faktur) + 6 & ")", FPro_Results & Initial_Faktur & "-" & Format(tgl_skg, "MM/yy"))

	End Sub

	Private Function Get_No_Berita_Acara() As String
		Dim No_Ba As String = "-"

		SQL = "SELECT "
		SQL &= $"CAST((COUNT(*) + 1) AS VARCHAR) "
		SQL &= $"+ '/BATWP/PRCS/' "
		SQL &= $"+ CASE MONTH('{Format(tgl_skg, "yyyy-MM-dd")}') "
		SQL &= $"WHEN 1 THEN 'I' WHEN 2 THEN 'II' WHEN 3 THEN 'III' WHEN 4 THEN 'IV' "
		SQL &= $"WHEN 5 THEN 'V' WHEN 6 THEN 'VI' WHEN 7 THEN 'VII' WHEN 8 THEN 'VIII' "
		SQL &= $"WHEN 9 THEN 'IX' WHEN 10 THEN 'X' WHEN 11 THEN 'XI' WHEN 12 THEN 'XII' "
		SQL &= $"END "
		SQL &= $"+ '/' + CAST(YEAR('{Format(tgl_skg, "yyyy-MM-dd")}') AS VARCHAR) AS No_Surat "
		SQL &= $"FROM N_EMI_Transaksi_Transfer_Waste "
		SQL &= $"WHERE MONTH(Tanggal) = MONTH('{Format(tgl_skg, "yyyy-MM-dd")}') "
		SQL &= $"AND YEAR(Tanggal) = YEAR('{Format(tgl_skg, "yyyy-MM-dd")}') "
		Using Dr123 = OpenTrans(SQL)
			If Dr123.Read Then
				No_Ba = Dr123("No_Surat").ToString.Trim
			End If
		End Using

		Return No_Ba

	End Function

	Private Function get_no_faktur_approval_pemusnahan() As String
		Dim FPro_Results As String = "AWP"
		Return FPro_Results & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("N_EMI_Transaksi_Approval_Waste", "No_Transaksi", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(No_Transaksi, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & Format(tgl_skg, "MMyy"))

	End Function

	Private Sub Kosong()

		Lv_Data.Items.Clear()

		Dtp_1.Value = Date.Now : Dtp_2.Value = Date.Now
		Txt_No_Split.Text = ""
		Txt_Keterangan.Text = ""

		Cmb_Gudang.SelectedIndex = -1 : Cmb_Gudang.Text = ""

		arr_Rekap_Data.Clear()

		get_jam()
		Try
			OpenConn()

			get_no_faktur_pemusnahan()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Cmb_Gudang.Focus()
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Cmb_Gudang.SelectedIndex = -1 Then
			MessageBox.Show("Pilih dahulu lokasi gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Gudang.DroppedDown = True
			Exit Sub
		End If

		Get_Data()
	End Sub

	Private Sub Get_Data_Lv(ByVal index As Integer)

		Lv_NoTransaksi = Lv_Data.Items(index).SubItems(item_NoTransaksi).Text
		Lv_NoSplit = Lv_Data.Items(index).SubItems(item_NoSplit).Text
		Lv_Tanggal = Lv_Data.Items(index).SubItems(item_Tanggal).Text
		Lv_Jam = Lv_Data.Items(index).SubItems(item_Jam).Text
		Lv_Lokasi = Lv_Data.Items(index).SubItems(item_Lokasi).Text
		Lv_Kd_Barang = Lv_Data.Items(index).SubItems(item_Kd_Barang).Text
		Lv_Nm_Barang = Lv_Data.Items(index).SubItems(item_Nm_Barang).Text
		Lv_Barcode = Lv_Data.Items(index).SubItems(item_Barcode).Text
		Lv_Jumlah = Lv_Data.Items(index).SubItems(item_Jumlah).Text
		Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
		Lv_Asal = Lv_Data.Items(index).SubItems(item_Asal).Text

	End Sub

	Private Sub Get_Data(ByVal Optional filter As Boolean = False)
		If Cmb_Gudang.SelectedIndex = -1 Then
			MessageBox.Show("Harap pilih dahulu lokasi gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Gudang.DroppedDown = True
			Cmb_Gudang.Focus()
			Exit Sub
		End If

		Try
			OpenConn()

			Lv_Data.Items.Clear()

			If asal_menu.ToString.ToUpper = "PROCESS" Then

				SQL = "select "
				'SQL = SQL & "'GOOD RECEIVED' as Asal, "
				SQL = SQL & "case when ( select top 1 'Y' from N_EMI_Transaksi_Waste_Sampel_GR_1 z where z.status is null "
				SQL = SQL & "and a.kode_perusahaan = z.kode_perusahaan "
				SQL = SQL & "and z.no_split = a.No_Production_Order "
				SQL = SQL & "and z.no_production_result = a.no_transaksi "
				SQL = SQL & "and z.proses = c.Proses "
				SQL = SQL & ") = 'Y' then 'WASTE SAMPEL' "
				SQL = SQL & "else 'GOOD RECEIVED' "
				SQL = SQL & "end as Asal, "
				SQL = SQL & "a.No_Transaksi, a.No_Production_Order, g.Tanggal, g.Jam, (c.Qr_Code +'-'+c.Kode_Unik_Berjalan) as Barcode, "
				SQL = SQL & "d.Kode_Stock_Owner, d.Kode_Barang, e.Nama as Nama_Barang, sum(d.Jumlah) as Jumlah, c.Satuan "
				SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_HPP b, Emi_Production_Results_Detail_Scrap c, Barang_SN d, barang e, N_EMI_Master_Waste_Items f, emi_production_results_detail_barang g, emi_split_production_order h "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan  "
				SQL = SQL & "and e.Kode_Perusahaan = f.Kode_Perusahaan and c.Kode_Perusahaan = g.Kode_Perusahaan "
				SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
				SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Urut = c.Urut_HPP "
				SQL = SQL & "and c.Serial_Number = d.Serial_Number "
				SQL = SQL & "and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang "
				SQL = SQL & "and e.Kode_Barang = f.Kode_Barang and f.Flag_Waste_Process = 'Y' and a.Cutoff_BA_Waste is null and round(d.jumlah,4)<>0 "
				SQL = SQL & "and c.No_Transaksi = g.No_Transaksi and c.Proses = g.Proses "
				SQL = SQL & "and a.Status is null and g.status is null "
				SQL = SQL & "and a.kode_perusahaan=h.kode_perusahaan and a.no_production_order=h.no_transaksi and h.Cutoff_BA_Waste is null "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and d.Kode_Stock_Owner = '" & Cmb_Gudang.Text.Trim & "' "
				SQL = SQL & "and c.Flag_Input_Waste is null "

				If filter Then
					SQL = SQL & "and g.Tanggal between '" & Format(Dtp_1.Value, "yyyy-MM-dd") & "' and '" & Format(Dtp_2.Value, "yyyy-MM-dd") & "' "

					If Txt_No_Split.Text.Trim.Length <> 0 Then
						SQL = SQL & "and a.No_Production_Order like '" & Txt_No_Split.Text & "%' "
					End If

				End If

				'SQL = SQL & "and c.Serial_Number not in ( "
				'SQL = SQL & "select y.Serial_Number_Awal "
				'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste z "
				'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur "
				'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
				'SQL = SQL & "where z.Status is null "
				'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
				'SQL = SQL & "and z.Flag_Waste_Proses = 'Y' ) "

				'SQL = SQL & "and (c.Qr_Code +'-'+c.Kode_Unik_Berjalan) not in ( "
				'SQL = SQL & "select (k.Qr_Code +'-'+k.Kode_Unik_Berjalan) "
				'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste z, N_EMI_Transaksi_Transfer_Waste_Detail x, N_EMI_Transaksi_Transfer_Waste_Det y, Barang_SN k "
				'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = k.Kode_Perusahaan "
				'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
				'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
				'SQL = SQL & "and z.Status is null and z.Flag_Waste_Proses = 'Y' "
				'SQL = SQL & "and y.Serial_Number_Awal = k.Serial_Number and z.Kode_Stock_Owner = k.Kode_Stock_Owner and x.Kode_Barang = k.Kode_Barang "
				''SQL = SQL & "and y.Selesai ='Y' "
				''SQL = SQL & "and y.Selesai is null and z.Flag_Validasi is null "
				'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan) "
				SQL = SQL & "group by a.kode_perusahaan, c.Proses, a.No_Transaksi, a.No_Production_Order, g.Tanggal, g.Jam, (c.Qr_Code +'-'+c.Kode_Unik_Berjalan), d.Kode_Stock_Owner, d.Kode_Barang, e.Nama, c.Satuan "

				'UNION ALL
				SQL = SQL & "union all "
				SQL = SQL & "select 'GOOD RECEIVED 2' as Asal, a.No_Transaksi, a.No_Production_Order, a.Tanggal, a.Jam, (c.Qr_Code +'-'+c.Kode_Unik_Berjalan) as Barcode, "
				SQL = SQL & "b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama as Nama_Barang, sum(c.Jumlah) as Jumlah, b.Satuan "
				SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Barang_SN c, barang d, N_EMI_Master_Waste_Items e, emi_split_production_order h  "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
				SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
				SQL = SQL & "and b.Serial_Number_Tujuan = c.Serial_Number and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
				SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
				SQL = SQL & "and b.Kode_Barang = e.Kode_Barang "
				SQL = SQL & "and e.Flag_Waste_Process = 'Y'  and a.Cutoff_BA_Waste is null and round(c.jumlah,4)<>0  "
				SQL = SQL & "and a.kode_perusahaan=h.kode_perusahaan and a.no_production_order=h.no_transaksi and h.Cutoff_BA_Waste is null "
				SQL = SQL & "and a.Status is null "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = '" & Cmb_Gudang.Text.Trim & "' "
				SQL = SQL & "and b.Flag_Input_Waste is null "
				If filter Then
					SQL = SQL & "and a.Tanggal between '" & Format(Dtp_1.Value, "yyyy-MM-dd") & "' and '" & Format(Dtp_2.Value, "yyyy-MM-dd") & "' "

					If Txt_No_Split.Text.Trim.Length <> 0 Then
						SQL = SQL & "and a.No_Production_Order like '" & Txt_No_Split.Text & "%' "
					End If

				End If

				'SQL = SQL & "and c.Serial_Number not in ( "
				'SQL = SQL & "select y.Serial_Number_Awal "
				'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste z "
				'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur "
				'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
				'SQL = SQL & "where z.Status is null "
				'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
				'SQL = SQL & "and z.Flag_Waste_Proses = 'Y' ) "

				'SQL = SQL & "and (c.Qr_Code +'-'+c.Kode_Unik_Berjalan) not in ( "
				'SQL = SQL & "select (k.Qr_Code +'-'+k.Kode_Unik_Berjalan) "
				'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste z, N_EMI_Transaksi_Transfer_Waste_Detail x, N_EMI_Transaksi_Transfer_Waste_Det y, Barang_SN k "
				'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = k.Kode_Perusahaan "
				'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
				'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
				'SQL = SQL & "and y.Serial_Number_Awal = k.Serial_Number and z.Kode_Stock_Owner = k.Kode_Stock_Owner and x.Kode_Barang = k.Kode_Barang "
				'SQL = SQL & "and z.Status is null and z.Flag_Waste_Proses = 'Y'  "
				''SQL = SQL & "and y.Selesai ='Y' "
				'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan) "
				SQL = SQL & "group by  a.No_Transaksi, a.No_Production_Order, a.Tanggal, a.Jam, (c.Qr_Code +'-'+c.Kode_Unik_Berjalan), b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama, b.Satuan "
				SQL = SQL & "order by tanggal, jam "

			ElseIf asal_menu.ToString.ToUpper = "PRODUCT" Then

				'=== GET DATA WASTE PRODICT YANG SUDAH DI APPROVE
				SQL = ";with Data_Approved as ( "
				SQL = SQL & "select distinct a.Kode_Perusahaan, a.No_Transaksi, a.No_Faktur_Waste, a.Jenis_Approval "
				SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
				SQL = SQL & "where a.Status is null "
				SQL = SQL & "and a.Jenis_Approval = 'Waste_Produk' "
				SQL = SQL & "and a.Flag_Selesai = 'Y' "
				SQL = SQL & "and not exists ( select 1 from N_EMI_Transaksi_Approval_Waste z "
				SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
				SQL = SQL & "and z.No_Transaksi = a.No_Transaksi "
				SQL = SQL & "and z.No_Transaksi = a.No_Transaksi "
				SQL = SQL & "and z.Flag_Approve IS NULL) "
				SQL = SQL & "and not EXISTS ( "
				SQL = SQL & "select 1 from N_EMI_Transaksi_Transfer_Waste z "
				SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur "
				SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "
				SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
				SQL = SQL & "and z.status is null "
				SQL = SQL & "and y.No_Faktur_Produk = a.No_Faktur_Waste) "
				SQL = SQL & ") "

				SQL = SQL & "select 'PENGAJUAN PEMINDAHAN WASTE' as Asal, a.No_Faktur as No_Transaksi, e.No_Split as No_Production_Order, a.Tanggal, a.Jam, "
				SQL = SQL & "(f.Qr_Code+'-'+f.Kode_Unik_Berjalan) as Barcode, a.Kode_Stock_Owner_Tujuan as Kode_Stock_Owner, b.Kode_Barang, g.Nama as Nama_Barang, sum(d.Jumlah) as Jumlah, b.Satuan "
				SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
				SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
				SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.kode_perusahaan = c.kode_perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
				SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
				SQL = SQL & "inner join N_EMI_Binding_Transaksi_Transfer_Waste_Produk e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.No_Faktur = e.No_Faktur and c.Urut_Oto = e.Urut_det "
				SQL = SQL & "inner join Barang_SN f on c.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner and b.Kode_Barang = f.Kode_Barang and c.Serial_Number_Awal = f.Serial_Number "
				SQL = SQL & "inner join Barang g on a.Kode_Perusahaan = g.Kode_Perusahaan and a.Kode_Stock_Owner_Tujuan = g.Kode_Stock_Owner and b.Kode_Barang = g.Kode_Barang "
				SQL = SQL & "inner join Data_Approved h on a.Kode_Perusahaan = h.Kode_Perusahaan and a.No_Faktur = h.No_Faktur_Waste "
				SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.status is null "
				SQL = SQL & "and a.Kode_Stock_Owner_Tujuan = '" & Cmb_Gudang.Text.Trim & "' "

				If filter Then
					SQL = SQL & "and a.Tanggal between '" & Format(Dtp_1.Value, "yyyy-MM-dd") & "' and '" & Format(Dtp_2.Value, "yyyy-MM-dd") & "' "

					If Txt_No_Split.Text.Trim.Length <> 0 Then
						SQL = SQL & "and e.No_Split like '" & Txt_No_Split.Text & "%' "
					End If

				End If

				SQL = SQL & "group by (f.Qr_Code+'-'+f.Kode_Unik_Berjalan), a.No_Faktur, e.No_Split, a.Tanggal, a.Jam, a.Kode_Stock_Owner_Tujuan, b.Kode_Barang, g.Nama, b.Satuan "

			End If
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim Lv As ListViewItem
					Lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
					Lv.SubItems.Add(Dr("No_Production_Order"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Jam"))
					Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
					Lv.SubItems.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama_Barang"))
					Lv.SubItems.Add(Dr("Barcode"))
					Lv.SubItems.Add(Dr("Jumlah"))
					Lv.SubItems.Add(Dr("Satuan"))
					Lv.SubItems.Add(Dr("Asal"))

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

		Get_Data(True)
	End Sub

	Private Sub Lv_Data_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Lv_Data.ItemCheck
		If Lv_Data.Items.Count = 0 Then Exit Sub
		If e.NewValue = CheckState.Unchecked Then Exit Sub

		If Lv_Data.CheckedItems.Count > 0 Then
			Dim refItem As ListViewItem = Lv_Data.CheckedItems(0)
			Dim refValue As String = refItem.SubItems(item_Lokasi).Text
			Dim refValue2 As String = refItem.SubItems(item_NoSplit).Text

			Dim currentItem As ListViewItem = Lv_Data.Items(e.Index)
			Dim currentValue As String = currentItem.SubItems(item_Lokasi).Text
			Dim currentValue2 As String = currentItem.SubItems(item_NoSplit).Text

			'If refValue2.ToUpper() <> currentValue2.ToUpper() Then
			'    e.NewValue = CheckState.Unchecked '

			'    MessageBox.Show("Hanya bisa memilih item dengan Split yang sama.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			'    Exit Sub
			'End If

			If refValue.ToUpper() <> currentValue.ToUpper() Then
				e.NewValue = CheckState.Unchecked '

				MessageBox.Show("Hanya bisa memilih item dengan lokasi yang sama.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End If
		End If

	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

		If Txt_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Keterangan.Focus() : Exit Sub
		End If

		Dim hasData As Boolean = False
		For i As Integer = 0 To Lv_Data.Items.Count - 1
			If Lv_Data.Items(i).Checked Then
				hasData = True
				Exit For
			End If
		Next

		If Not hasData Then
			MessageBox.Show("Pilih Dahulu Data yang Ingin di Proses", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Lv_Data.Focus() : Exit Sub
		End If

		Dim Faktur_Pemusnahaan As String = ""

		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===========================
			'=     CEK BUTTON ROLE     =
			'===========================
			If CekButtonRole("Validasi_Transaksi_Waste_Proses") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Transaksi Waste Proses", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			If MessageBox.Show("Yakin Ingin Simpan ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

			'=============================
			'=     MENU ASAL PROCESS     =
			'=============================
			If asal_menu.ToString.ToUpper = "PROCESS" Then

#Region "Proses Transaksi Pemushanan Barang"

				Dim Keterangan As String = Txt_Keterangan.Text

				arr_Rekap_Data.Clear()

				For i As Integer = 0 To Lv_Data.Items.Count - 1

					If Not Lv_Data.Items(i).Checked Then Continue For

					Get_Data_Lv(i)

					'Dim index As Integer = arr_Rekap_Data.FindIndex(Function(item) item.kd_Barang = Lv_Kd_Barang)

					Dim index As Integer = arr_Rekap_Data.FindIndex(Function(item)
																		Return item.kd_Barang = Lv_Kd_Barang AndAlso
																			   item.lokasi = Lv_Lokasi
																	End Function)

					If index <> -1 Then
						Dim item = arr_Rekap_Data(index)
						item.jumlah += Lv_Jumlah
						arr_Rekap_Data(index) = item
					Else
						arr_Rekap_Data.Add((kd_Barang:=Lv_Kd_Barang, jumlah:=Lv_Jumlah, satuan:=Lv_Satuan, lokasi:=Lv_Lokasi))
					End If

				Next

				SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
				SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and kode_Stock_owner = '" & arr_Rekap_Data(0).lokasi & "' "
				SQL = SQL & "order by kode_stock_owner"
				Using dr = OpenTrans(SQL)
					Do While dr.Read
						Initial_Faktur = dr("inisial_faktur")
					Loop
				End Using

				get_no_faktur_pemusnahan()

				Faktur_Pemusnahaan = Txt_No_Transaksi.Text

				If arr_Rekap_Data.Count = 0 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Tidak Ada Data yang Diinsert . . ! ! ")
					Exit Sub
				End If

				'Dim No_BA As String = Generate_No_Berita_Acara()

				'=================================================
				'=     INSERT N_EMI_Transaksi_Transfer_Waste8     =
				'=================================================

				SQL = "insert into N_EMI_Transaksi_Transfer_Waste (kode_perusahaan, No_faktur, Kode_Stock_Owner, Tanggal, Jam, UserID, Lokasi, Keterangan, Flag_Waste_Proses) Values "
				SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(Txt_No_Transaksi.Text) & "', '" & arr_Rekap_Data(0).lokasi & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
				SQL = SQL & "'" & Ket_Lokasi_HO & "', '" & Keterangan & "', 'Y')"
				ExecuteTrans(SQL)

				If Not Add_Transaksi_Approval() Then
					CloseTrans()
					CloseConn()
					Exit Sub
				End If

				For i As Integer = 0 To arr_Rekap_Data.Count - 1

					Dim data = arr_Rekap_Data(i)

					Dim Jenis_Berat As String = ""
					SQL = "Select isnull(flag_tampil_berat,'T') as flag_tampil_berat from emi_satuan where "
					SQL = SQL & "satuan='" & data.satuan & "' and kode_perusahaan='" & KodePerusahaan & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							Jenis_Berat = dr("flag_tampil_berat")
						Else
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("data Satuan Tidak ada . . ! ! ")
							Exit Sub
						End If
					End Using

					Dim Jenis_kemasan As String = ""
					SQL = "Select Jenis_Kemasan from barang where "
					SQL = SQL & "Kode_Barang='" & data.kd_Barang & "' and kode_perusahaan='" & KodePerusahaan & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							Jenis_kemasan = dr("Jenis_Kemasan")
						Else
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("data Satuan Tidak ada . . ! ! ")
							Exit Sub
						End If
					End Using

					Dim Berat_Per_Bags As Double = 0
					SQL = "select top 1 berat from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & data.kd_Barang & "' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							Berat_Per_Bags = Dr("berat")
						End If
					End Using

					Dim Jumlah_Bags As Double = 0
					If Jenis_kemasan.ToUpper = "ORIGINAL BAGS" Then
						Jumlah_Bags = Math.Ceiling(Val(HilangkanTanda(data.jumlah)) / Val(HilangkanTanda(Berat_Per_Bags)))
					Else
						Jumlah_Bags = 0

					End If

					Dim Flag_Timbang As String = "T"

					'========================================================
					'=     INSERT N_EMI_Transaksi_Transfer_Waste_Detail     =
					'========================================================
					SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Detail (Kode_Perusahaan, No_faktur, Kode_Barang, Total, Satuan, "
					SQL = SQL & "Total_Barang, Satuan_Barang, Total_Bags, Flag_Timbang) values "
					SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(Txt_No_Transaksi.Text) & "', '" & data.kd_Barang & "', "
					SQL = SQL & "'" & HilangkanTanda(data.jumlah) & "', '" & data.satuan & "', "
					SQL = SQL & "'" & HilangkanTanda(data.jumlah) & "', '" & data.satuan & "', "
					SQL = SQL & "'" & HilangkanTanda(Jumlah_Bags) & "', '" & Flag_Timbang & "')"
					ExecuteTrans(SQL)

					Dim x_ident_current As Integer = 0
					SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Transfer_Waste_Detail') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							x_ident_current = Dr("urutan")
						End If
					End Using

					SQL = "select kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste_Detail where "
					SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and "
					SQL = SQL & "No_Faktur='" & Txt_No_Transaksi.Text & "' and "
					SQL = SQL & "Kode_barang='" & data.kd_Barang & "' and "
					SQL = SQL & "urut_oto='" & x_ident_current & "' "
					Using Dr = OpenTrans(SQL)
						If Not Dr.Read Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Terjadi Kesalahan, Silahkan Ulangi Transaksi  . . ! ! ")
							Exit Sub
						End If
					End Using

					For j As Integer = 0 To Lv_Data.Items.Count - 1
						Get_Data_Lv(j)

						If Not data.kd_Barang = Lv_Kd_Barang Or Not data.lokasi = Lv_Lokasi Then Continue For
						If Not Lv_Data.Items(j).Checked Then Continue For

						If data.kd_Barang = Lv_Kd_Barang Then

							'============================================
							'=     CEK APAKAH DATA BELUM DI TIMBANG     =
							'============================================

							'SQL = "select a.Kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Det b, barang_sn c  where "
							'SQL = SQL & "a.kode_Perusahaan=b.kode_Perusahaan And a.No_Faktur=b.no_faktur and "
							'SQL = SQL & "b.kode_Perusahaan=c.kode_Perusahaan And b.Serial_Number_Awal=c.serial_number and "
							'SQL = SQL & "c.qr_Code+'-'+kode_unik_berjalan = '" & Lv_Barcode & "' and "
							'SQL = SQL & "selesai is null and a.status is null "
							'Using Dr = OpenTrans(SQL)
							'    If Dr.Read Then
							'        Dr.Close()
							'        CloseTrans()
							'        CloseConn()
							'        MessageBox.Show("Barang " & Lv_Nm_Barang & " belum melalui proses pencetakan barcode.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'        Exit Sub
							'    Else
							'        Dr.Close()
							'    End If
							'End Using

							Dim jenis_bags As String = ""
							Dim isi_per_bags As Double = 0
							SQL = "select Jenis_Kemasan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags from barang where "
							SQL = SQL & "kode_perusahaan='" & KodePerusahaan & "' and "
							SQL = SQL & "kode_barang='" & Lv_Kd_Barang & "' and "
							SQL = SQL & "kode_stock_owner='" & Lv_Lokasi & "'"
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									jenis_bags = Dr("Jenis_Kemasan").ToString.ToUpper
									isi_per_bags = Dr("Isi_Per_Bags")
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							Dim sisaPotong As Double = 0
							Dim JumlahDipotong As Double = 0
							SQL = "select a.Jumlah as Stock_SN, a.serial_number "
							SQL = SQL & "from Barang_SN a where "
							SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and a.qr_Code+'-'+a.kode_unik_berjalan = '" & Lv_Barcode & "' "
							SQL = SQL & "and a.Kode_stock_owner = '" & Lv_Lokasi & "' and a.jumlah<>0 "
							SQL = SQL & "order by a.Tgl_Expired "
							Using Ds = BindingTrans(SQL)
								With Ds.Tables("MyTable")
									If .Rows.Count <> 0 Then

										sisaPotong = Val(HilangkanTanda(Lv_Jumlah))

										For Index As Integer = 0 To .Rows.Count - 1
											If sisaPotong = 0 Then
												Exit For
											ElseIf sisaPotong < 0 Then
												CloseTrans()
												CloseConn()
												MessageBox.Show("Terdapat Kesalahan saat Potong Barang Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If

											Dim JumlahInsert As Double = 0
											Dim Satuan As String = ""

											Dim Data_SN As String = .Rows(Index).Item("serial_number")

											If sisaPotong < Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Or sisaPotong = Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Then

												JumlahInsert = sisaPotong
												' Satuan = .Rows(Index).Item("Satuan").ToString.Trim

												JumlahDipotong += sisaPotong
												sisaPotong = 0

											ElseIf sisaPotong > Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Then

												JumlahInsert = Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
												'Satuan = .Rows(Index).Item("Satuan").ToString.Trim

												JumlahDipotong += Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
												sisaPotong = sisaPotong - Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
											Else
												CloseTrans()
												CloseConn()
												MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If

											Dim Jumlah_Bags_Potong = 0
											'If jenis_bags = "ORIGINAL BAGS" Then
											'    Jumlah_Bags_Potong = Math.Ceiling(JumlahInsert / isi_per_bags)
											'Else
											'    Jumlah_Bags_Potong = 0
											'End If

											'=============================
											'=     GET DETAIL BARANG     =
											'=============================
											Dim Id_Warehouse_Awal As String = ""
											Dim Id_Pallet_Awal As String = ""
											Dim Warna As String = ""
											SQL = "select Id_Warehouse, Nomor_Pallet, Warna from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' "
											SQL = SQL & "and Serial_Number = '" & .Rows(Index).Item("serial_number") & "' and Kode_Barang = '" & Lv_Kd_Barang & "' "
											Using Dr = OpenTrans(SQL)
												If Dr.Read Then
													Id_Warehouse_Awal = Dr("Id_Warehouse")
													Id_Pallet_Awal = Dr("Nomor_Pallet")
													Warna = Dr("Warna")
												End If
											End Using

											'=====================================================
											'=     INSERT N_EMI_Transaksi_Transfer_Waste_Det     =
											'=====================================================

											Dim nilai_BesarInsert As Double = 0
											SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Lv_Kd_Barang & "', '" & Lv_Satuan & "',"
											SQL = SQL & "'" & Lv_Satuan & "', '" & HilangkanTanda(JumlahInsert) & "' ) as hasil"
											Using Dr1 = OpenTrans(SQL)
												If Dr1.Read Then
													If General_Class.CekNULL(Dr1("hasil")) = "" Then
														Dr1.Close()
														CloseTrans()
														CloseConn()
														MessageBox.Show("data konversi satuan kirim tidak ada ")
														Exit Sub
													End If

													nilai_BesarInsert = Dr1("hasil")
												Else
													Dr1.Close()
													CloseTrans()
													CloseConn()
													MessageBox.Show("data konversi satuan kirim tidak ada ")
													Exit Sub
												End If
											End Using

											Dim Id_Warehouse_Tujuan As String = ""

											SQL = "select top(1) a.id_wms_warehouse_position, 0 as nomor_urut from "
											SQL = SQL & "view_warehouse_position a "
											SQL = SQL & "where a.kode_Perusahaan ='" & KodePerusahaan & "' "
											SQL = SQL & "and a.Kode_Stock_Owner='" & Lv_Lokasi & "' "
											Using Dr2 = OpenTrans(SQL)
												Do While Dr2.Read
													Id_Warehouse_Tujuan = Dr2("Id_WMS_Warehouse_Position")
												Loop
											End Using

											Dim No_Faktur_Produk As String = "NULL"
											If asal_menu.ToString.ToUpper = "PRODUCT" Then
												No_Faktur_Produk = $"'{Lv_NoTransaksi}'"
											End If
											SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det(Kode_Perusahaan, No_faktur, Id_Wms_Awal, No_Pallet_Awal, Id_Wms_Tujuan, "
											SQL = SQL & "Serial_Number_Awal, Jumlah, Jumlah_Barang, Jumlah_Bags, Warna, Urut_TF, No_Split, No_Production_Result, No_Faktur_Produk) values( "
											SQL = SQL & "'" & KodePerusahaan & "', '" & Trim(Txt_No_Transaksi.Text) & "', '" & Id_Warehouse_Awal & "', "
											SQL = SQL & "'" & Id_Pallet_Awal & "', '" & Id_Warehouse_Tujuan & "', '" & Data_SN & "', "
											SQL = SQL & "'" & nilai_BesarInsert & "', '" & JumlahInsert & "', "
											SQL = SQL & "'" & Jumlah_Bags & "', "
											SQL = SQL & "'" & Warna & "', '" & x_ident_current & "', '" & Lv_NoSplit & "', '" & Lv_NoTransaksi & "', " & No_Faktur_Produk & ")"
											ExecuteTrans(SQL)

										Next
									Else
										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi Kesalahan Pada Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								End With
							End Using

							If Val(HilangkanTanda(JumlahDipotong)) <> Val(HilangkanTanda(Lv_Jumlah)) Then
								CloseTrans()
								CloseConn()
								MessageBox.Show("Terjadi Kesalahan Saat Memotong Stock Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							'=======================
							'=     UPDATE FLAG     =
							'=======================
							If Lv_Asal.Trim = "WASTE SAMPEL" Or Lv_Asal.Trim = "GOOD RECEIVED" Then
								SQL = "select 1 "
								SQL = SQL & "from Emi_Production_Results_Detail_Scrap "
								SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
								SQL = SQL & "and No_Transaksi = '" & Lv_NoTransaksi & "' "
								SQL = SQL & "and (Qr_Code +'-'+ Kode_Unik_Berjalan) = '" & Lv_Barcode & "' "
								Using Dr = OpenTrans(SQL)
									If Dr.Read Then

										Dr.Close()
										SQL = "update Emi_Production_Results_Detail_Scrap set Flag_Input_Waste = 'Y' "
										SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
										SQL = SQL & "and No_Transaksi = '" & Lv_NoTransaksi & "' "
										SQL = SQL & "and (Qr_Code +'-'+ Kode_Unik_Berjalan) = '" & Lv_Barcode & "' "
										ExecuteTrans(SQL)

									End If
								End Using

							ElseIf Lv_Asal.Trim = "GOOD RECEIVED 2" Then

								SQL = "select a.No_Transaksi, a.Urut "
								SQL = SQL & "from Emi_Production_Results_Validation_Detail a "
								SQL = SQL & "inner join Barang_SN b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang and a.Serial_Number_Tujuan = b.Serial_Number "
								SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								SQL = SQL & "and a.No_Transaksi = '" & Lv_NoTransaksi & "' "
								SQL = SQL & "and (b.Qr_Code+'-'+b.Kode_Unik_Berjalan) = '" & Lv_Barcode & "' "
								Using Ds = BindingTrans(SQL)
									With Ds.Tables("MyTable")
										If .Rows.Count <> 0 Then
											For l As Integer = 0 To .Rows.Count - 1

												SQL = "update Emi_Production_Results_Validation_Detail set Flag_Input_Waste = 'Y' "
												SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
												SQL = SQL & "and No_Transaksi = '" & .Rows(l).Item("No_Transaksi").ToString.Trim & "' "
												SQL = SQL & "and Urut = " & .Rows(l).Item("Urut").ToString.Trim & " "
												ExecuteTrans(SQL)

											Next
										End If
									End With
								End Using

							End If

						End If
					Next
				Next

#End Region

			ElseIf asal_menu.ToString.ToUpper = "PRODUCT" Then

#Region "Proses Product"

				SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
				SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and kode_Stock_owner = '" & Cmb_Gudang.Text & "' "
				SQL = SQL & "order by kode_stock_owner"
				Using dr = OpenTrans(SQL)
					Do While dr.Read
						Initial_Faktur = dr("inisial_faktur")
					Loop
				End Using

				get_no_faktur_pemusnahan()

				Faktur_Pemusnahaan = Txt_No_Transaksi.Text
				Dim Keterangan As String = Txt_Keterangan.Text

				'=================================================
				'=     INSERT N_EMI_Transaksi_Transfer_Waste     =
				'=================================================

				SQL = "insert into N_EMI_Transaksi_Transfer_Waste (kode_perusahaan, No_faktur, Kode_Stock_Owner, Tanggal, Jam, UserID, Lokasi, Keterangan, Flag_Waste_Proses) Values "
				SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(Txt_No_Transaksi.Text) & "', '" & Cmb_Gudang.Text & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
				SQL = SQL & "'" & Ket_Lokasi_HO & "', '" & Keterangan & "', 'Y')"
				ExecuteTrans(SQL)

				If Not Add_Transaksi_Approval() Then
					CloseTrans()
					CloseConn()
					Exit Sub
				End If

				For i As Integer = 0 To Lv_Data.Items.Count - 1
					If Not Lv_Data.Items(i).Checked Then Continue For

					Get_Data_Lv(i)

					Dim Jenis_Berat As String = ""
					SQL = "Select isnull(flag_tampil_berat,'T') as flag_tampil_berat from emi_satuan where "
					SQL = SQL & "satuan='" & Lv_Satuan & "' and kode_perusahaan='" & KodePerusahaan & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							Jenis_Berat = dr("flag_tampil_berat")
						Else
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("data Satuan Tidak ada . . ! ! ")
							Exit Sub
						End If
					End Using

					Dim Jenis_kemasan As String = ""
					SQL = "Select Jenis_Kemasan from barang where "
					SQL = SQL & "Kode_Barang='" & Lv_Kd_Barang & "' and kode_perusahaan='" & KodePerusahaan & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							Jenis_kemasan = dr("Jenis_Kemasan")
						Else
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("data Satuan Tidak ada . . ! ! ")
							Exit Sub
						End If
					End Using

					Dim Berat_Per_Bags As Double = 0
					SQL = "select top 1 berat from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Lv_Kd_Barang & "' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							Berat_Per_Bags = Dr("berat")
						End If
					End Using

					Dim Jumlah_Bags As Double = 0
					If Jenis_kemasan.ToUpper = "ORIGINAL BAGS" Then
						Jumlah_Bags = Math.Ceiling(Val(HilangkanTanda(Lv_Satuan)) / Val(HilangkanTanda(Berat_Per_Bags)))
					Else
						Jumlah_Bags = 0

					End If

					Dim Flag_Timbang As String = "T"

					'========================================================
					'=     INSERT N_EMI_Transaksi_Transfer_Waste_Detail     =
					'========================================================
					SQL = "select 1 from N_EMI_Transaksi_Transfer_Waste a "
					SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
					SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
					SQL &= $"and a.Status is NULL "
					SQL &= $"and a.No_Faktur = '{Trim(Txt_No_Transaksi.Text)}' "
					SQL &= $"and b.Kode_Barang = '{Lv_Kd_Barang}' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							Dr.Close()
							SQL = $"update N_EMI_Transaksi_Transfer_Waste_Detail set Total += '{HilangkanTanda(Lv_Jumlah)}', "
							SQL = SQL & $"Total_Barang += '{HilangkanTanda(Lv_Jumlah)}', Total_Bags += '{HilangkanTanda(Jumlah_Bags)}' "
							SQL = SQL & $"where Kode_Perusahaan = '{KodePerusahaan}' "
							SQL = SQL & $"and No_Faktur = '{Trim(Txt_No_Transaksi.Text)}' "
							SQL = SQL & $"and Kode_Barang = '{Lv_Kd_Barang}' "
							ExecuteTrans(SQL)
						Else
							Dr.Close()
							SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Detail (Kode_Perusahaan, No_faktur, Kode_Barang, Total, Satuan, "
							SQL = SQL & "Total_Barang, Satuan_Barang, Total_Bags, Flag_Timbang) values "
							SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(Txt_No_Transaksi.Text) & "', '" & Lv_Kd_Barang & "', "
							SQL = SQL & "'" & HilangkanTanda(Lv_Jumlah) & "', '" & Lv_Satuan & "', "
							SQL = SQL & "'" & HilangkanTanda(Lv_Jumlah) & "', '" & Lv_Satuan & "', "
							SQL = SQL & "'" & HilangkanTanda(Jumlah_Bags) & "', '" & Flag_Timbang & "')"
							ExecuteTrans(SQL)
						End If
					End Using

					Dim x_ident_current As Integer = 0
					SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Transfer_Waste_Detail') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							x_ident_current = Dr("urutan")
						End If
					End Using

					SQL = "select kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste_Detail where "
					SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and "
					SQL = SQL & "No_Faktur='" & Txt_No_Transaksi.Text & "' and "
					SQL = SQL & "Kode_barang='" & Lv_Kd_Barang & "' and "
					SQL = SQL & "urut_oto='" & x_ident_current & "' "
					Using Dr = OpenTrans(SQL)
						If Not Dr.Read Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Terjadi Kesalahan, Silahkan Ulangi Transaksi  . . ! ! ")
							Exit Sub
						End If
					End Using

					SQL = ";with Data_Approved as ( "
					SQL &= $"select distinct a.Kode_Perusahaan, a.No_Transaksi, a.No_Faktur_Waste, a.Jenis_Approval "
					SQL &= $"from N_EMI_Transaksi_Approval_Waste a "
					SQL &= $"where a.Status is null "
					SQL &= $"and a.Jenis_Approval = 'Waste_Produk' "
					SQL &= $"and a.Flag_Selesai = 'Y' "
					SQL &= $"and not exists ( select 1 from N_EMI_Transaksi_Approval_Waste z "
					SQL &= $"where z.Kode_Perusahaan = a.Kode_Perusahaan "
					SQL &= $"and z.No_Transaksi = a.No_Transaksi "
					SQL &= $"and z.Flag_Approve IS NULL) "
					SQL &= $"and not EXISTS ( "
					SQL &= $"select 1 from N_EMI_Transaksi_Transfer_Waste z "
					SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur "
					SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "
					SQL &= $"inner join barang_sn k on z.Kode_Perusahaan = k.Kode_Perusahaan and z.Kode_Stock_Owner = k.Kode_Stock_Owner and x.Kode_Barang = k.Kode_Barang and y.Serial_Number_Awal = k.Serial_Number "
					SQL &= $"where z.Kode_Perusahaan = a.Kode_Perusahaan "
					SQL &= $"and z.status is null "
					SQL &= $"and (k.Qr_Code+'-'+k.Kode_Unik_Berjalan) = '{Lv_Barcode}' "
					SQL &= $"and y.No_Faktur_Produk = a.No_Faktur_Waste)) "
					SQL &= $"select 'PENGAJUAN PEMINDAHAN WASTE' as Asal, a.No_Faktur as No_Transaksi, e.No_Split as No_Production_Order, a.Tanggal, a.Jam, a.Kode_Stock_Owner as Kode_Stock_Awal, "
					SQL &= $"(i.Qr_Code+'-'+i.Kode_Unik_Berjalan) as Barcode_Awal, (f.Qr_Code+'-'+f.Kode_Unik_Berjalan) as Barcode, a.Kode_Stock_Owner_Tujuan as Kode_Stock_Owner, b.Kode_Barang, g.Nama as Nama_Barang, sum(d.Jumlah) as Jumlah, b.Satuan "
					SQL &= $"from N_EMI_Transaksi_Transfer_Waste_Produk a "
					SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
					SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.kode_perusahaan = c.kode_perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
					SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
					SQL &= $"inner join N_EMI_Binding_Transaksi_Transfer_Waste_Produk e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.No_Faktur = e.No_Faktur and c.Urut_Oto = e.Urut_det "
					SQL &= $"inner join Barang_SN f on c.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner_Tujuan = f.Kode_Stock_Owner and b.Kode_Barang = f.Kode_Barang and d.Serial_Number = f.Serial_Number "
					SQL &= $"inner join Barang_SN i on c.Kode_Perusahaan = i.Kode_Perusahaan and a.Kode_Stock_Owner = i.Kode_Stock_Owner and b.Kode_Barang = i.Kode_Barang and c.Serial_Number_Awal = i.Serial_Number "
					SQL &= $"inner join Barang g on a.Kode_Perusahaan = g.Kode_Perusahaan and a.Kode_Stock_Owner_Tujuan = g.Kode_Stock_Owner and b.Kode_Barang = g.Kode_Barang "
					SQL &= $"inner join Data_Approved h on a.Kode_Perusahaan = h.Kode_Perusahaan and a.No_Faktur = h.No_Faktur_Waste "
					SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
					SQL &= $"and a.status is null "
					SQL &= $"and a.Kode_Stock_Owner_Tujuan = 'Waste Storage' "
					SQL &= $"and a.No_Faktur = '{Lv_NoTransaksi}' "
					SQL &= $"and e.No_Split = '{Lv_NoSplit}' "
					SQL &= $"and (i.Qr_Code+'-'+i.Kode_Unik_Berjalan) = '{Lv_Barcode}' "
					SQL &= $"and b.Kode_Barang = '{Lv_Kd_Barang}' "
					SQL &= $"group by (i.Qr_Code+'-'+i.Kode_Unik_Berjalan), (f.Qr_Code+'-'+f.Kode_Unik_Berjalan), a.Kode_Stock_Owner, a.No_Faktur, e.No_Split, a.Tanggal, a.Jam, a.Kode_Stock_Owner_Tujuan, b.Kode_Barang, g.Nama, b.Satuan "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then
								For p As Integer = 0 To .Rows.Count - 1

									Dim NoFakturProduk As String = .Rows(p).Item("No_Transaksi")
									Dim NoSplit As String = .Rows(p).Item("No_Production_Order")
									Dim Satuan As String = .Rows(p).Item("Satuan")
									Dim KdSo As String = .Rows(p).Item("Kode_Stock_Awal")
									Dim KdSoTujuan As String = .Rows(p).Item("Kode_Stock_Owner")
									Dim Kdbarang As String = .Rows(p).Item("Kode_Barang")
									Dim Jmlh As String = Val(HilangkanTanda(.Rows(p).Item("Jumlah")))
									Dim Barcode As String = .Rows(p).Item("Barcode")

									Dim jenis_bags As String = ""
									Dim isi_per_bags As Double = 0
									SQL = "select Jenis_Kemasan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags from barang where "
									SQL = SQL & "kode_perusahaan='" & KodePerusahaan & "' and "
									SQL = SQL & "kode_barang='" & Kdbarang & "' and "
									SQL = SQL & "kode_stock_owner='" & KdSoTujuan & "'"
									Using Dr = OpenTrans(SQL)
										If Dr.Read Then
											jenis_bags = Dr("Jenis_Kemasan").ToString.ToUpper
											isi_per_bags = Dr("Isi_Per_Bags")
										Else
											Dr.Close()
											CloseTrans()
											CloseConn()
											MessageBox.Show("Data barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using

									Dim sisaPotong As Double = 0
									Dim JumlahDipotong As Double = 0
									SQL = "select a.Jumlah as Stock_SN, a.serial_number "
									SQL = SQL & "from Barang_SN a where "
									SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "and a.qr_Code+'-'+a.kode_unik_berjalan = '" & Barcode & "' "
									SQL = SQL & "and a.Kode_stock_owner = '" & KdSoTujuan & "' and a.jumlah<>0 "
									SQL = SQL & "order by a.Tgl_Expired "
									Using Ds2 = BindingTrans(SQL)
										If Ds2.Tables("MyTable").Rows.Count <> 0 Then

											sisaPotong = Val(HilangkanTanda(Jmlh))

											For Index As Integer = 0 To .Rows.Count - 1
												If sisaPotong = 0 Then
													Exit For
												ElseIf sisaPotong < 0 Then
													CloseTrans()
													CloseConn()
													MessageBox.Show("Terdapat Kesalahan saat Potong Barang Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Exit Sub
												End If

												Dim JumlahInsert As Double = 0

												Dim Data_SN As String = Ds2.Tables("MyTable").Rows(Index).Item("serial_number")

												If sisaPotong < Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(Index).Item("Stock_SN"))) Or sisaPotong = Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(Index).Item("Stock_SN"))) Then

													JumlahInsert = sisaPotong
													' Satuan = .Rows(Index).Item("Satuan").ToString.Trim

													JumlahDipotong += sisaPotong
													sisaPotong = 0

												ElseIf sisaPotong > Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(Index).Item("Stock_SN"))) Then

													JumlahInsert = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(Index).Item("Stock_SN"), "N4")))
													'Satuan = .Rows(Index).Item("Satuan").ToString.Trim

													JumlahDipotong += Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(Index).Item("Stock_SN"), "N4")))
													sisaPotong = sisaPotong - Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(Index).Item("Stock_SN"), "N4")))
												Else
													CloseTrans()
													CloseConn()
													MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Exit Sub
												End If

												Dim Jumlah_Bags_Potong = 0
												'If jenis_bags = "ORIGINAL BAGS" Then
												'    Jumlah_Bags_Potong = Math.Ceiling(JumlahInsert / isi_per_bags)
												'Else
												'    Jumlah_Bags_Potong = 0
												'End If

												'=============================
												'=     GET DETAIL BARANG     =
												'=============================
												Dim Id_Warehouse_Awal As String = ""
												Dim Id_Pallet_Awal As String = ""
												Dim Warna As String = ""
												SQL = "select Id_Warehouse, Nomor_Pallet, Warna from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' "
												SQL = SQL & "and Serial_Number = '" & Ds2.Tables("MyTable").Rows(Index).Item("serial_number") & "' and Kode_Barang = '" & Kdbarang & "' "
												Using Dr = OpenTrans(SQL)
													If Dr.Read Then
														Id_Warehouse_Awal = Dr("Id_Warehouse")
														Id_Pallet_Awal = Dr("Nomor_Pallet")
														Warna = Dr("Warna")
													End If
												End Using

												'=====================================================
												'=     INSERT N_EMI_Transaksi_Transfer_Waste_Det     =
												'=====================================================

												Dim nilai_BesarInsert As Double = 0
												SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Kdbarang & "', '" & Satuan & "',"
												SQL = SQL & "'" & Satuan & "', '" & HilangkanTanda(JumlahInsert) & "' ) as hasil"
												Using Dr1 = OpenTrans(SQL)
													If Dr1.Read Then
														If General_Class.CekNULL(Dr1("hasil")) = "" Then
															Dr1.Close()
															CloseTrans()
															CloseConn()
															MessageBox.Show("data konversi satuan kirim tidak ada ")
															Exit Sub
														End If

														nilai_BesarInsert = Dr1("hasil")
													Else
														Dr1.Close()
														CloseTrans()
														CloseConn()
														MessageBox.Show("data konversi satuan kirim tidak ada ")
														Exit Sub
													End If
												End Using

												Dim Id_Warehouse_Tujuan As String = ""
												SQL = "select top(1) a.id_wms_warehouse_position, 0 as nomor_urut from "
												SQL = SQL & "view_warehouse_position a "
												SQL = SQL & "where a.kode_Perusahaan ='" & KodePerusahaan & "' "
												SQL = SQL & "and a.Kode_Stock_Owner='" & Lv_Lokasi & "' "
												Using Dr2 = OpenTrans(SQL)
													Do While Dr2.Read
														Id_Warehouse_Tujuan = Dr2("Id_WMS_Warehouse_Position")
													Loop
												End Using

												Dim No_Faktur_Produk As String = "NULL"
												If asal_menu.ToString.ToUpper = "PRODUCT" Then
													No_Faktur_Produk = $"'{NoFakturProduk}'"
												End If

												SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det(Kode_Perusahaan, No_faktur, Id_Wms_Awal, No_Pallet_Awal, Id_Wms_Tujuan, "
												SQL = SQL & "Serial_Number_Awal, Jumlah, Jumlah_Barang, Jumlah_Bags, Warna, Urut_TF, No_Split, No_Production_Result, No_Faktur_Produk) values( "
												SQL = SQL & "'" & KodePerusahaan & "', '" & Trim(Txt_No_Transaksi.Text) & "', '" & Id_Warehouse_Awal & "', "
												SQL = SQL & "'" & Id_Pallet_Awal & "', '" & Id_Warehouse_Tujuan & "', '" & Data_SN & "', "
												SQL = SQL & "'" & nilai_BesarInsert & "', '" & JumlahInsert & "', "
												SQL = SQL & "'" & Jumlah_Bags & "', "
												SQL = SQL & "'" & Warna & "', '" & x_ident_current & "', '" & NoSplit & "', '" & NoFakturProduk & "', " & No_Faktur_Produk & ")"
												ExecuteTrans(SQL)

											Next
										Else
											CloseTrans()
											CloseConn()
											MessageBox.Show("Terjadi Kesalahan Pada Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using

									If Val(HilangkanTanda(JumlahDipotong)) <> Val(HilangkanTanda(Jmlh)) Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi Kesalahan Saat Memotong Stock Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If

								Next
							Else
								CloseTrans()
								CloseConn()
								MessageBox.Show("Terjadi Kesalahan Data Det Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End With
					End Using

				Next

#End Region

			End If

#Region "Validasi Warehouse "

			'            Dim QrLama As String = ""
			'            Dim expDate As String = ""
			'            Dim batchLama As String = ""
			'            Dim tglMsk As String = ""
			'            Dim metodePengeluaranStock As String = ""
			'            Dim GetDataKodeTransfer, GetDataLokasi, GetDataKdBrg, GetDataNmBrg, GetDataBrgSN, GetDataJmlEstimasi, GetDataSatuanBesar, GetDataSatuanKecil, GetDataUrutOto As String
			'            Dim GetJumlahBags, GetRakTujuan, GetPalletTujuan, GetWarna As String
			'            Dim SN As String = ""

			'            For i As Integer = 0 To Lv_Data.Items.Count - 1
			'                If Not Lv_Data.Items(i).Checked Then Continue For

			'                Get_Data_Lv(i)

			'                Dim arr_Sn As New ArrayList

			'                Dim ada_data As Boolean = False
			'                SQL = "Select distinct c.serial_number from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Det b, barang_sn c where "
			'                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur "
			'                SQL = SQL & "And a.status Is null And b.selesai Is null  "
			'                SQL = SQL & "And b.kode_perusahaan=c.kode_Perusahaan And b.serial_number_awal=c.serial_number "
			'                SQL = SQL & "And c.kode_perusahaan='" & KodePerusahaan & "' and c.qr_code+'-'+kode_unik_berjalan='" & Lv_Barcode & "' "
			'                Using dr = OpenTrans(SQL)
			'                    Do While dr.Read
			'                        ada_data = True
			'                        arr_Sn.Add(dr("serial_number"))
			'                    Loop
			'                End Using

			'                If ada_data = False Then
			'                    CloseTrans()
			'                    CloseConn()
			'                    MessageBox.Show("Data Barcode Tidak di temukan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                    Kosong()
			'                    Exit Sub
			'                End If

			'                For Indxx = 0 To arr_Sn.Count - 1

			'                    'Ambil Data SN Berdasar Barcode
			'                    SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, b.Metode_Pengeluaran_Stok, a.Tgl_Masuk, a.Blok_SN "
			'                    SQL = SQL & "from barang_sn a, barang b "
			'                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			'                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
			'                    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
			'                    SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'                    SQL = SQL & "and a.Jumlah <> 0 "
			'                    SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & Lv_Barcode & "' "
			'                    Using Dr = OpenTrans(SQL)
			'                        If Dr.Read Then

			'                            If General_Class.CekNULL(Dr("Blok_SN")) = "Y" Then
			'                                Dr.Close()
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("SN Pada Pallet di Block, Validasi di Batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                Kosong()
			'                                Exit Sub
			'                            End If

			'                            QrLama = General_Class.CekNULL(Dr("Qr_Code"))
			'                            batchLama = General_Class.CekNULL(Dr("Batch_Number"))
			'                            SN = Dr("serial_number")
			'                            expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
			'                            tglMsk = General_Class.CekNULL(Dr("tgl_masuk"))
			'                            metodePengeluaranStock = General_Class.CekNULL(Dr("Metode_Pengeluaran_Stok"))
			'                        Else
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Barang tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Kosong()
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    'Cek data YG Mau di TF, Berdasar SN dr Barcode
			'                    SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, d.nama as Nama_Barang, c.Jumlah, c.Jumlah_Bags, b.Satuan, c.Serial_Number_Awal, "
			'                    SQL = SQL & "b.Satuan_Barang, c.Urut_Oto, c.Warna, c.Id_Wms_Tujuan "
			'                    SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c, Barang d "
			'                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
			'                    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
			'                    SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
			'                    SQL = SQL & "and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.kode_barang "
			'                    SQL = SQL & "and a.status is null and a.Flag_Validasi is null "
			'                    SQL = SQL & "and b.Flag_Timbang = 'T' and c.Selesai is null "
			'                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'                    SQL = SQL & "and c.Serial_Number_Awal = '" & SN & "' "

			'                    Using Dr = OpenTrans(SQL)
			'                        If Dr.Read Then

			'                            GetDataKodeTransfer = Dr("No_faktur")
			'                            GetDataLokasi = Dr("Kode_Stock_Owner")
			'                            GetDataKdBrg = Dr("Kode_Barang")
			'                            GetDataNmBrg = Dr("Nama_Barang")
			'                            GetDataBrgSN = Dr("Serial_Number_Awal")
			'                            GetDataJmlEstimasi = HilangkanTanda(Format(Dr("Jumlah"), "N4"))
			'                            GetJumlahBags = Dr("Jumlah_Bags")
			'                            GetDataSatuanKecil = Dr("Satuan_Barang")
			'                            GetDataSatuanBesar = Dr("Satuan")
			'                            GetWarna = Dr("Warna")
			'                            GetDataUrutOto = Dr("urut_oto")
			'                            GetRakTujuan = Dr("Id_Wms_Tujuan")
			'                        Else
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Barang tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Kosong()
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
			'                    SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c "
			'                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
			'                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
			'                    SQL = SQL & "and a.No_Faktur = '" & GetDataKodeTransfer & "' and c.urut_oto = '" & GetDataUrutOto & "'  "
			'                    Using Dr = OpenTrans(SQL)
			'                        If Dr.Read Then

			'                            If General_Class.CekNULL(Dr("status")) <> "" Then
			'                                Dr.Close()
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                Exit Sub
			'                            ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
			'                                Dr.Close()
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                Exit Sub
			'                            ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
			'                                Dr.Close()
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                Exit Sub
			'                            End If
			'                        Else
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
			'                    SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
			'                    SQL = SQL & "id_wms_warehouse_position = '" & GetRakTujuan & "' "
			'                    SQL = SQL & "order by nomor_urut "
			'                    Using dr = OpenTrans(SQL)
			'                        If dr.Read Then
			'                            GetPalletTujuan = dr("nomor_urut")
			'                        Else
			'                            dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    '=============================================================================================
			'                    '=============================================================================================
			'                    '=======================================================================================

			'                    '====================================
			'                    '=       CONVERT SATUAN KECIL       =
			'                    '====================================
			'                    Dim nilai_kecildetail As Double = 0
			'                    SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & GetDataKdBrg & "', '" & GetDataSatuanBesar & "',"
			'                    SQL = SQL & "'" & GetDataSatuanKecil & "', '" & GetDataJmlEstimasi & "' ) as hasil"
			'                    Using Dr1 = OpenTrans(SQL)
			'                        If Dr1.Read Then
			'                            If General_Class.CekNULL(Dr1("hasil")) = "" Then
			'                                Dr1.Close()
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("data konversi satuan kirim tidak ada ")
			'                                Exit Sub
			'                            End If

			'                            nilai_kecildetail = Dr1("hasil")
			'                        Else
			'                            Dr1.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("data konversi satuan kirim tidak ada ")
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    '============================
			'                    '=       POTONG STOCK       =
			'                    '============================

			'                    Dim nilai_persediaan_min As Double = 0
			'                    SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
			'                    SQL = SQL & "Kode_Stock_Owner='" & GetDataLokasi & "' and Kode_Barang='" & GetDataKdBrg & "' "
			'                    SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
			'                    Using dr = OpenTrans(SQL)
			'                        If dr.Read Then
			'                            nilai_persediaan_min = dr("rp_persediaan_min")
			'                        Else
			'                            dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    Dim Nama As String = ""
			'                    'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
			'                    SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
			'                    SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
			'                    Using dr = OpenTrans(SQL)
			'                        If dr.Read Then
			'                            Nama = dr("Kode_Barang")
			'                            If dr("good_stock") < nilai_kecildetail Then
			'                                dr.Close()
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			'                                Exit Sub
			'                            ElseIf dr("Jumlah_Bags") < GetJumlahBags Then
			'                                dr.Close()
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			'                                Exit Sub
			'                            Else
			'                                dr.Close()
			'                                SQL = "update barang set Good_Stock = Good_Stock - Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & GetJumlahBags & " "
			'                                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
			'                                SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
			'                                ExecuteTrans(SQL)
			'                            End If
			'                        Else
			'                            dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
			'                    SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
			'                    SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
			'                    Using dr = OpenTrans(SQL)
			'                        If dr.Read Then
			'                            If dr("jumlah") < nilai_kecildetail Then
			'                                dr.Close()
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			'                                Exit Sub
			'                            ElseIf dr("Jumlah_Bags") < GetJumlahBags Then
			'                                dr.Close()
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			'                                Exit Sub
			'                            Else
			'                                dr.Close()
			'                                SQL = "update barang_sn set jumlah = jumlah - Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & GetJumlahBags & " "
			'                                SQL = SQL & "where Kode_Stock_Owner='" & GetDataLokasi & "' and Kode_Barang='" & GetDataKdBrg & "' "
			'                                SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
			'                                ExecuteTrans(SQL)
			'                            End If
			'                        Else
			'                            dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    '====================================
			'                    '=       CEK KESESUAIAN STOCK       =
			'                    '====================================
			'                    SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
			'                    SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
			'                    SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
			'                    SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
			'                    SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
			'                    SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
			'                    SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetDataLokasi & "' "
			'                    SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'                    SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
			'                    Using Ds = BindingTrans(SQL)
			'                        With Ds.Tables("MyTable")
			'                            If .Rows.Count <> 0 Then
			'                                If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
			'                                    CloseTrans()
			'                                    CloseConn()
			'                                    MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                    Exit Sub
			'                                End If
			'                            Else
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                Exit Sub
			'                            End If
			'                        End With
			'                    End Using

			'                    '==============================
			'                    '=       INSERT SN BARU       =
			'                    '==============================

			'                    Dim hargaIsn As String = ""
			'                    Dim namaBarang As String = ""
			'                    Dim warnaLama As String = ""

			'                    'Ambil Data Lama
			'                    SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
			'                    SQL = SQL & "from barang_sn a, barang b "
			'                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			'                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
			'                    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
			'                    SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'                    SQL = SQL & "and a.Kode_Stock_Owner='" & GetDataLokasi & "' "
			'                    SQL = SQL & "and a.Kode_Barang ='" & GetDataKdBrg & "' "
			'                    SQL = SQL & "and a.Serial_Number='" & GetDataBrgSN & "' "
			'                    'SQL = SQL & "and a.Jumlah <> 0 "
			'                    Using Dr = OpenTrans(SQL)
			'                        Do While Dr.Read
			'                            hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
			'                            QrLama = General_Class.CekNULL(Dr("Qr_Code"))
			'                            batchLama = General_Class.CekNULL(Dr("Batch_Number"))
			'                            namaBarang = General_Class.CekNULL(Dr("Nama"))
			'                            expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
			'                            warnaLama = General_Class.CekNULL(Dr("warna"))
			'                        Loop
			'                    End Using

			'                    'GENERATE SN BARU
			'                    Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
			'                    Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
			'                    Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

			'                    Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

			'                    'INSERT BARANG SN BARU
			'                    SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
			'                    SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
			'                    SQL = SQL & "select Kode_Perusahaan, '" & GetDataLokasi & "', Kode_Barang, '" & SN_Baru & "', '" & nilai_kecildetail & "', " & GetJumlahBags & ", "
			'                    SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & GetRakTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
			'                    SQL = SQL & "Kode_Unik_Asal, '" & GetPalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, 'Y' "
			'                    SQL = SQL & "from Barang_SN "
			'                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
			'                    SQL = SQL & "and Kode_Stock_Owner='" & GetDataLokasi & "' "
			'                    SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
			'                    SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "' "
			'                    ExecuteTrans(SQL)

			'                    '============================
			'                    '=       TAMBAH STOCK       =
			'                    '============================

			'                    SQL = "update barang set Good_Stock= Good_Stock + Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & GetJumlahBags & " "
			'                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
			'                    SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
			'                    ExecuteTrans(SQL)

			'                    'CEK KESESUAIAN STOCK
			'                    SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
			'                    SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
			'                    SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
			'                    SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
			'                    SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
			'                    SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
			'                    SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetDataLokasi & "' "
			'                    SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'                    SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
			'                    Using Ds = BindingTrans(SQL)
			'                        With Ds.Tables("MyTable")
			'                            If .Rows.Count <> 0 Then
			'                                If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
			'                                    CloseTrans()
			'                                    CloseConn()
			'                                    MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                    Exit Sub
			'                                End If
			'                            Else
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                Exit Sub
			'                            End If
			'                        End With
			'                    End Using

			'#Region "Jurnal"

			'                    'dari
			'                    Dim inisial_faktur_dari As String = ""
			'                    Dim akun_persediaan_dari As String = ""
			'                    Dim akun_persediaan_tujuan As String = ""

			'                    SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
			'                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & GetDataLokasi & "' "
			'                    Using Dr = OpenTrans(SQL)
			'                        If Dr.Read Then
			'                            'akun_persediaan_dari = Dr("persediaan")
			'                            inisial_faktur_dari = Dr("inisial_faktur")
			'                        Else
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    SQL = "select c.akun_Persediaan "
			'                    SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
			'                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
			'                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
			'                    SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'                    SQL = SQL & "and b.kode_stock_owner = '" & GetDataLokasi & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
			'                    Using Dr = OpenTrans(SQL)
			'                        If Dr.Read Then
			'                            akun_persediaan_dari = Dr("akun_Persediaan")
			'                        Else
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    SQL = "select c.akun_Persediaan "
			'                    SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
			'                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
			'                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
			'                    SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'                    SQL = SQL & "and b.kode_stock_owner = '" & GetDataLokasi & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
			'                    Using Dr = OpenTrans(SQL)
			'                        If Dr.Read Then
			'                            akun_persediaan_tujuan = Dr("akun_Persediaan")
			'                        Else
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    Dim Kode_voucher As String = ""
			'                    Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
			'                    Dim pagenumber As Integer = 1

			'                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
			'                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
			'                    SQL = SQL & "'" & Kode_voucher & "', "
			'                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
			'                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
			'                    SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & GetDataKodeTransfer & "', '', "
			'                    SQL = SQL & "'-', '" & UserID & "')"
			'                    ExecuteTrans(SQL)

			'                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
			'                              Strings.Mid(akun_persediaan_dari, 2, 1),
			'                              Strings.Mid(Ganti(akun_persediaan_dari), 3),
			'                              KodePerusahaan, KodeProyek, "Persedian " & GetDataKodeTransfer, "0", nilai_persediaan_min, pagenumber, GetDataLokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
			'                    ExecuteTrans(SQL)
			'                    pagenumber = pagenumber + 1

			'                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
			'                             Strings.Mid(akun_persediaan_tujuan, 2, 1),
			'                             Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
			'                             KodePerusahaan, KodeProyek, "Persedian " & GetDataKodeTransfer, nilai_persediaan_min, "0", pagenumber, GetDataLokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
			'                    ExecuteTrans(SQL)
			'                    pagenumber = pagenumber + 1

			'                    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
			'                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
			'                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
			'                    Using Dr = OpenTrans(SQL)
			'                        If Dr.Read Then
			'                            If Dr("debit") <> Dr("kredit") Then
			'                                Dr.Close()
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                Exit Sub
			'                            End If
			'                        Else
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    End Using

			'#End Region

			'                    SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
			'                    SQL = SQL & "Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags) values( "
			'                    SQL = SQL & "'" & KodePerusahaan & "', '" & GetDataKodeTransfer & "', '" & GetDataUrutOto & "', "
			'                    SQL = SQL & "'" & GetPalletTujuan & "', '" & SN_Baru & "', '" & nilai_kecildetail & "', "
			'                    SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'                    SQL = SQL & "'" & Kode_voucher & "', '" & GetJumlahBags & "') "
			'                    ExecuteTrans(SQL)

			'                    SQL = "update N_EMI_Transaksi_Transfer_Waste_Det set  "
			'                    SQL = SQL & "Selesai = 'Y' "
			'                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
			'                    SQL = SQL & "and urut_oto = '" & GetDataUrutOto & "' "
			'                    ExecuteTrans(SQL)

			'                Next

			'            Next

#End Region

			'If True Then

			'    CloseTrans()
			'    CloseConn()
			'    MessageBox.Show("Tahan")
			'    Exit Sub
			'End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'=================================
		'=     CETAK FAKTUR TF STOCK     =
		'=================================
		'Try
		'    OpenConn()

		'    Dim CrDoc As New Object
		'    Dim kertas As String = ""

		'    SQL = "select kode_perusahaan from N_EMI_View_Berita_Acara_Waste_Process "
		'    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Faktur_Pemusnahaan & "' "
		'    Using Ds = BindingTrans(SQL)
		'        If Ds.Tables("MyTable").Rows.Count <> 0 Then

		'            CrDoc = New N_EMI_CR_Berita_Acara_Waste_Proses
		'            kertas = "Faktur"

		'            'With A_Place_For_Printing2
		'            '    CrDoc.SetDataSource(Ds)
		'            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
		'            '    CrDoc.PrintOptions.PrinterName = ""
		'            '    CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Waste_Process.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Waste_Process.no_faktur}='" & Faktur_Pemusnahaan & "' "
		'            '    CrDoc.SummaryInfo.ReportTitle = "TF"
		'            '    .Text = "TF"
		'            '    .CrystalReportViewer1.ReportSource = CrDoc
		'            '    .Refresh()
		'            '    .Show()
		'            'End With

		'            '============================================================================================================================================
		'            '============================================================================================================================================
		'            CrDoc.SetDataSource(Ds)
		'            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
		'            CrDoc.PrintOptions.PrinterName = PrinterNameTS
		'            CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Waste_Process.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Waste_Process.no_faktur}='" & Faktur_Pemusnahaan & "' "
		'            'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

		'            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
		'            doctoprint.PrinterSettings.PrinterName = PrinterNameTS
		'            'doctoprint.DefaultPageSettings.Landscape = True
		'            Dim rawKind As Integer
		'            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
		'            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
		'                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
		'                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
		'                    CrDoc.PrintOptions.PaperSize = rawKind
		'                    Exit For
		'                End If
		'            Next

		'            CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
		'            CrDoc.PrintToPrinter(1, False, 1, 99)

		'            MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

		'        End If
		'    End Using

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		Kosong()

	End Sub

	Private Function Add_Transaksi_Approval() As Boolean

		Dim No_Faktur_Approval As String = get_no_faktur_approval_pemusnahan()

		Dim No_BA As String = Get_No_Berita_Acara()

		'==============================
		'=     GET APPROVAL LEVEL     =
		'==============================
		SQL = $"
            select Approval_Level, Jenis_Approval, kode_stock_owner, isnull(flag_koneksi,'T') as  flag_koneksi
            from N_EMI_Master_Hierarchy_Approval_Waste
            where Kode_Perusahaan = '{KodePerusahaan}' and Jenis_Approval = 'Waste_Process' and Kode_Stock_Owner = '{Cmb_Gudang.Text}'
            order by Approval_Level
        "
		Using Ds9 = BindingTrans(SQL)
			If Ds9.Tables("MyTable").Rows.Count <> 0 Then
				For y As Integer = 0 To Ds9.Tables("MyTable").Rows.Count - 1

					'==============================
					'=     Insert User per Approval     =
					'==============================

					SQL = $"
                            select Approval_Level, Jenis_Approval, kode_stock_owner, ID_User_Android, Jabatan, Peran, no_hp
                            from N_EMI_Master_Hierarchy_Approval_Waste_user
                            where Kode_Perusahaan = '{KodePerusahaan}' and isActive = 'Y'
                            and Jenis_Approval = '{Ds9.Tables("MyTable").Rows(y).Item("Jenis_Approval")}'
                            and Kode_Stock_Owner = '{Ds9.Tables("MyTable").Rows(y).Item("kode_stock_owner")}'
                            and Approval_Level = '{Ds9.Tables("MyTable").Rows(y).Item("Approval_Level")}'
                           "
					If Ds9.Tables("MyTable").Rows(y).Item("Approval_Level") = "1" Then

						SQL &= $" and ID_User_Desktop='{UserID}' "

					End If

					If Ds9.Tables("MyTable").Rows(y).Item("flag_koneksi") = "Y" Then

						SQL &= $" and ID_User_Koneksi='{UserID}' "

					End If

					SQL &= $"
                            order by Approval_Level
                            "
					Using Ds10 = BindingTrans(SQL)
						If Ds10.Tables("MyTable").Rows.Count <> 0 Then
							For z As Integer = 0 To Ds10.Tables("MyTable").Rows.Count - 1

								Dim Flag_Approve As String = "NULL"
								Dim Flag_Sudah_Kirim_WA As String = "NULL"
								Dim UserID_Desktop As String = "NULL"

								If Ds10.Tables("MyTable").Rows(z).Item("Approval_Level") = "1" Then

									Flag_Approve = "'Y'"
									Flag_Sudah_Kirim_WA = "'Y'"
									UserID_Desktop = $"'{UserID}'"

								End If

								SQL = $"
                                        insert into N_EMI_Transaksi_Approval_Waste (Kode_Perusahaan, No_Transaksi, No_Faktur_Waste, Tanggal, Jam, ID_User_Android_Approve, Approval_Level,
                                        Jabatan, Jenis_Approval, Flag_Approve, Flag_Sudah_Kirim_WA, Peran, Tanggal_Approve, Jam_Approve, User_ID_Desktop, No_Berita_Acara, Kode_Stock_Owner, no_hp)
                                        values ('{KodePerusahaan}', '{No_Faktur_Approval}', '{Txt_No_Transaksi.Text}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}',
                                        '{Ds10.Tables("MyTable").Rows(z).Item("ID_User_Android")}', '{Ds10.Tables("MyTable").Rows(z).Item("Approval_Level")}',
                                        '{Ds10.Tables("MyTable").Rows(z).Item("Jabatan")}', '{Ds10.Tables("MyTable").Rows(z).Item("Jenis_Approval")}', {Flag_Approve}, {Flag_Sudah_Kirim_WA}, '{Ds10.Tables("MyTable").Rows(z).Item("Peran")}',
                                        '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', {UserID_Desktop}, '{No_BA}', '{Cmb_Gudang.Text}', '{Ds10.Tables("MyTable").Rows(z).Item("no_hp")}' )
                                        "
								ExecuteTrans(SQL)

							Next
						Else
							MessageBox.Show($"User Approval Level {Ds9.Tables("MyTable").Rows(y).Item("Approval_Level")} Belum Diset", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Return False
						End If
					End Using

				Next
			Else
				MessageBox.Show($"User Approval Belum Diset", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Return False
			End If
		End Using

		Return True

	End Function

	Private Sub Dtp_1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dtp_1.KeyPress
		If e.KeyChar = Chr(13) Then Dtp_2.Focus()
	End Sub

	Private Sub Dtp_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dtp_2.KeyPress
		If e.KeyChar = Chr(13) Then Txt_No_Split.Focus()
	End Sub

	Private Sub Txt_No_Split_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Split.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
	End Sub

	Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
	End Sub

	'Private Function Generate_No_Berita_Acara() As String

	'    Dim Bulan As Integer = tgl_skg.Month
	'    Dim Tahun As Integer = tgl_skg.Year
	'    Dim Jumlah_Data As Integer = 0

	'    SQL = $"
	'        select
	'            case
	'                when Jumlah = 0 THEN 0
	'                else Jumlah
	'            end as Jumlah_Data
	'        from (
	'            select COUNT(*) AS Jumlah
	'            from N_EMI_Transaksi_Transfer_Waste
	'            where Kode_Perusahaan = '{KodePerusahaan}'
	'              and Status IS NULL
	'              and MONTH(Tanggal) = {Bulan}
	'              and YEAR(Tanggal) = {Tahun}
	'        ) as x
	'    "
	'    Using Dr = OpenTrans(SQL)
	'        If Dr.Read Then
	'            Jumlah_Data = Jumlah_Data + 1
	'        End If
	'    End Using

	'    Dim romawi = Bulan_Romawi.FirstOrDefault(Function(x) x.Bulan = Bulan).Romawi

	'    Dim Format_Surat As String = $"{Jumlah_Data}/BATWP/PROD/{romawi}/{Tahun}"

	'    Return Format_Surat

	'End Function

End Class