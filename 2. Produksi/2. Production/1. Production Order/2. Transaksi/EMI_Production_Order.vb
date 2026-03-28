Public Class EMI_Production_Order

	Dim arrIdLine, arrInisialFaktur, arrInisialRouting As New ArrayList

	Dim RV_Parent_Formula, RV_Parent_Transaksi As String

	Dim arrOrderBy As New ArrayList

	Dim jumlah_po As Double = 0

	Dim skipCB As String = "Y"

	Dim dataListview2Update = "T"

	Dim flag_barang_berbeda As String = "T"

	'  Dim lvNoAntri As String
	Dim lvNoPo As String

	Dim lvLokasi As String
	Dim lvKdCus As String
	Dim lvNmCus As String
	Dim lvKdBrg As String
	Dim lvNmBrg As String
	Dim lvJmlh As String
	Dim lvJmlhSisa As String
	Dim lvSatuan As String
	Dim lvJenisProduk As String
	Dim lvUrut As String
	Dim lvIdJenisProduk As String

	' Dim cellLvNoAntri As Integer = 0
	Dim cellLvNoPo As Integer = 0

	Dim cellLvLokasi As Integer = 1
	Dim cellLvKdCus As Integer = 2
	Dim cellLvNmCus As Integer = 3
	Dim cellLvKdBrg As Integer = 4
	Dim cellLvNmBrg As Integer = 5
	Dim cellLvJmlh As Integer = 6
	Dim cellLvJmlhSisa As Integer = 7
	Dim cellLvSatuan As Integer = 8
	Dim cellLvJenisProduk As Integer = 9
	Dim cellLvUrut As Integer = 10
	Dim cellLvIdJenisProduk As Integer = 11

	Dim fakturBB As String
	Dim Kode_Unik As String = ""

	Private Sub get_isi_listview(ByVal index As Integer)

		lvNoPo = LvData.Items(index).SubItems(cellLvNoPo).Text '0
		lvLokasi = LvData.Items(index).SubItems(cellLvLokasi).Text '1
		lvKdCus = LvData.Items(index).SubItems(cellLvKdCus).Text '2
		lvNmCus = LvData.Items(index).SubItems(cellLvNmCus).Text '3
		lvKdBrg = LvData.Items(index).SubItems(cellLvKdBrg).Text '4
		lvNmBrg = LvData.Items(index).SubItems(cellLvNmBrg).Text '5
		lvJmlh = LvData.Items(index).SubItems(cellLvJmlh).Text '6
		lvJmlhSisa = LvData.Items(index).SubItems(cellLvJmlhSisa).Text '7
		lvSatuan = LvData.Items(index).SubItems(cellLvSatuan).Text '8
		lvIdJenisProduk = LvData.Items(index).SubItems(cellLvIdJenisProduk).Text '9
		lvUrut = LvData.Items(index).SubItems(cellLvUrut).Text '10
		lvIdJenisProduk = LvData.Items(index).SubItems(cellLvIdJenisProduk).Text '11

	End Sub

	Dim LvFaktur2 As String
	Dim LvLokasi2 As String
	Dim lvKdCust2 As String
	Dim LvNmCust2 As String
	Dim LvKdBrg2 As String
	Dim LvJmlh2 As String
	Dim LvSatuan2 As String
	Dim LvJenis2 As String
	Dim LvUrut2 As String
	Dim LvIdJnsPrdk2 As String
	Dim LvFromUpdate2 As String
	Dim LvDeleted2 As String

	Dim cellNoSo2 As Integer = 0
	Dim cellLokasi2 As Integer = 1
	Dim cellKdCust2 As Integer = 2
	Dim cellNmCust2 As Integer = 3
	Dim cellKdBrg2 As Integer = 4
	Dim cellJmlh2 As Integer = 5
	Dim cellSatuan2 As Integer = 6
	Dim cellJenis2 As Integer = 7
	Dim cellUrut2 As Integer = 8
	Dim cellIdJnsPrdk2 As Integer = 9
	Dim cellFromUpdate2 As Integer = 10
	Dim cellDeleted2 As Integer = 11

	Private Sub get_isi_listview_detail(ByVal index As Integer)

		LvFaktur2 = LvOrder.Items(index).SubItems(cellNoSo2).Text '0
		LvLokasi2 = LvOrder.Items(index).SubItems(cellLokasi2).Text '1
		lvKdCust2 = LvOrder.Items(index).SubItems(cellKdCust2).Text '2
		LvNmCust2 = LvOrder.Items(index).SubItems(cellNmCust2).Text '3
		LvKdBrg2 = LvOrder.Items(index).SubItems(cellKdBrg2).Text '4

		LvJmlh2 = LvOrder.Items(index).SubItems(cellJmlh2).Text '5
		LvSatuan2 = LvOrder.Items(index).SubItems(cellSatuan2).Text '6
		LvJenis2 = LvOrder.Items(index).SubItems(cellJenis2).Text '7
		LvUrut2 = LvOrder.Items(index).SubItems(cellUrut2).Text '8
		LvIdJnsPrdk2 = LvOrder.Items(index).SubItems(cellIdJnsPrdk2).Text '9
		LvFromUpdate2 = LvOrder.Items(index).SubItems(cellFromUpdate2).Text '10
		LvDeleted2 = LvOrder.Items(index).SubItems(cellDeleted2).Text '11

	End Sub

	Dim LvNoSo3 As String
	Dim LvKdBhn3 As String
	Dim lvJmlh3 As String
	Dim LvSatuan3 As String
	Dim LvStock3 As String
	Dim LvSatuanStock3 As String
	Dim LvNilaiBrg3 As String
	Dim LvSatuanBrg3 As String
	Dim LvStockBrg3 As String
	Dim LvFlagPotStok3 As String
	Dim LvRV As String

	Dim cellNoSo3 As Integer = 0
	Dim cellKdBhn3 As Integer = 1
	Dim cellJmlh3 As Integer = 2
	Dim cellSatuan3 As Integer = 3
	Dim cellStock3 As Integer = 4
	Dim cellSatuanStock3 As Integer = 5
	Dim cellNilaiBrg3 As Integer = 6
	Dim cellSatuanBrg3 As Integer = 7
	Dim cellStockBrg3 As Integer = 8
	Dim cellFlagPotStok3 As Integer = 9
	Dim cellRV As Integer = 11

	Private Sub Get_Isi_Listview_Bahan(ByVal index As Integer)

		LvNoSo3 = LvBahan.Items(index).SubItems(cellNoSo3).Text '0
		LvKdBhn3 = LvBahan.Items(index).SubItems(cellKdBhn3).Text '1
		lvJmlh3 = LvBahan.Items(index).SubItems(cellJmlh3).Text '2
		LvSatuan3 = LvBahan.Items(index).SubItems(cellSatuan3).Text '3
		LvStock3 = LvBahan.Items(index).SubItems(cellStock3).Text '4
		LvSatuanStock3 = LvBahan.Items(index).SubItems(cellSatuanStock3).Text '5
		LvNilaiBrg3 = LvBahan.Items(index).SubItems(cellNilaiBrg3).Text '6
		LvSatuanBrg3 = LvBahan.Items(index).SubItems(cellSatuanBrg3).Text '7
		LvStockBrg3 = LvBahan.Items(index).SubItems(cellStockBrg3).Text '8
		LvFlagPotStok3 = LvBahan.Items(index).SubItems(cellFlagPotStok3).Text
		LvRV = LvBahan.Items(index).SubItems(cellRV).Text

	End Sub

	Dim LvNoSo4 As String
	Dim LvKdBhn4 As String
	Dim lvJmlh4 As String
	Dim LvSatuan4 As String
	Dim LvStock4 As String
	Dim LvSatuanStock4 As String
	Dim LvNilaiBrg4 As String
	Dim LvSatuanBrg4 As String
	Dim LvStockBrg4 As String
	Dim LvKeepStockBrg4 As String
	Dim LvFlagPotStok4 As String
	Dim LvJenis4 As String
	Dim LvMasterJumlah_Barang4 As String
	Dim LvMasterJumlah_Bahan4 As String

	Dim cellNoSo4 As Integer = 0
	Dim cellKdBhn4 As Integer = 1
	Dim cellJmlh4 As Integer = 2
	Dim cellSatuan4 As Integer = 3
	Dim cellStock4 As Integer = 4
	Dim cellSatuanStock4 As Integer = 5
	Dim cellNilaiBrg4 As Integer = 6
	Dim cellSatuanBrg4 As Integer = 7
	Dim cellStockBrg4 As Integer = 8
	Dim cellKeepStockBrg4 As Integer = 9
	Dim cellPotStok4 As Integer = 10
	Dim cellJenis4 As Integer = 11
	Dim cellMasterJumlah_Barang4 As Integer = 12
	Dim cellMasterJumlah_Bahan4 As Integer = 13

	Private Sub Get_Isi_Listview_Packaging(ByVal index As Integer)

		LvNoSo4 = LvPackaging.Items(index).SubItems(cellNoSo4).Text '0
		LvKdBhn4 = LvPackaging.Items(index).SubItems(cellKdBhn4).Text '1
		lvJmlh4 = LvPackaging.Items(index).SubItems(cellJmlh4).Text '2
		LvSatuan4 = LvPackaging.Items(index).SubItems(cellSatuan4).Text '3
		LvStock4 = LvPackaging.Items(index).SubItems(cellStock4).Text '4
		LvSatuanStock4 = LvPackaging.Items(index).SubItems(cellSatuanStock4).Text '5
		LvNilaiBrg4 = LvPackaging.Items(index).SubItems(cellNilaiBrg4).Text '6
		LvSatuanBrg4 = LvPackaging.Items(index).SubItems(cellSatuanBrg4).Text '7
		LvStockBrg4 = LvPackaging.Items(index).SubItems(cellStockBrg4).Text '8
		LvKeepStockBrg4 = LvPackaging.Items(index).SubItems(cellKeepStockBrg4).Text '9
		LvFlagPotStok4 = LvPackaging.Items(index).SubItems(cellPotStok4).Text '10
		LvJenis4 = LvPackaging.Items(index).SubItems(cellJenis4).Text '11
		LvMasterJumlah_Barang4 = LvPackaging.Items(index).SubItems(cellMasterJumlah_Barang4).Text '12
		LvMasterJumlah_Bahan4 = LvPackaging.Items(index).SubItems(cellMasterJumlah_Bahan4).Text '13
	End Sub

	Private Sub get_no_faktur()

		txtNoFaktur.Text = fProduksi & Format(tgl_skg, "MMyy") & "-" &
					 General_Class.Get_Last_Number2("emi_order_produksi", "no_Faktur", 5,
					 "Kode_perusahaan", KodePerusahaan,
					 "And", "substring(no_Faktur, 1, " & Len(fProduksi) + 4 & ")", fProduksi & Format(tgl_skg, "MMyy"))

		txt_faktur_bayangan.Text = fProduksi & Format(tgl_skg, "MMyy") & "-" &
				   General_Class.Get_Last_Number2("emi_order_produksi", "no_Faktur", 5,
				   "Kode_perusahaan", KodePerusahaan,
				   "And", "substring(no_Faktur, 1, " & Len(fProduksi) + 4 & ")", fProduksi & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub EMI_Production_Order_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Master_Gudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
		get_jam()

		Try
			OpenConn()
			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, "Rencana_Produksi")
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		' Label1.Text = Base_Language.Lang_Rencana_Produksi_Judul
		'LblNoFaktur.Text = Base_Language.Lang_Global_NoFaktur
		'    lblTanggal.Text = Base_Language.Lang_Global_Tanggal
		'   lblJam.Text = Base_Language.Lang_Global_Jam

		lbltxtNoPo.Text = Base_Language.Lang_Global_No_PO
		'lblTxtLokasi.Text = Base_Language.Lang_Global_Lokasi
		lblTxtKodeCustomer.Text = Base_Language.Lang_Global_KodeCustomer
		lblTxtNmCust.Text = Base_Language.Lang_Global_NamaCustomer
		lblTxtKdBrng.Text = Base_Language.Lang_Global_KodeBarang
		'lblTxtNmBrng.Text = Base_Language.Lang_Global_NamaBarang
		lblTxtJumlah.Text = Base_Language.Lang_Global_Jumlah
		lblTxtSatuan.Text = Base_Language.Lang_Global_Satuan

		Btn_Simpan.Text = Base_Language.Lang_Global_Simpan

		' ListView1.Columns.Add(Base_Language.Lang_Global_NO, 40, HorizontalAlignment.Center)
		LvData.Columns.Add("No SO", 130, HorizontalAlignment.Left) '0
		LvData.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Left) '1
		LvData.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 125, HorizontalAlignment.Left) '2
		LvData.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 200, HorizontalAlignment.Left) '3
		LvData.Columns.Add(Base_Language.Lang_Global_KodeBarang, 125, HorizontalAlignment.Left) '4
		LvData.Columns.Add(Base_Language.Lang_Global_NamaBarang, 200, HorizontalAlignment.Left) '5
		LvData.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Right) '6
		LvData.Columns.Add(Base_Language.Lang_Global_Jumlah_Sisa, 0, HorizontalAlignment.Center) '7
		LvData.Columns.Add(Base_Language.Lang_Global_Satuan, 90, HorizontalAlignment.Center) '8
		LvData.Columns.Add(Base_Language.Lang_Global_Jenis, 100, HorizontalAlignment.Center) '9
		LvData.Columns.Add("Urut", 0, HorizontalAlignment.Center) '10
		LvData.Columns.Add("id Jenis Produk", 0, HorizontalAlignment.Center) '11

		'  ListView1.View = View.Details

		LvOrder.Columns.Add("No SO", 130, HorizontalAlignment.Left) '0
		LvOrder.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Left) '1
		LvOrder.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 0, HorizontalAlignment.Left) '2
		LvOrder.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 0, HorizontalAlignment.Left) '3
		LvOrder.Columns.Add(Base_Language.Lang_Global_KodeBarang, 0, HorizontalAlignment.Left) '4
		'LvOrder.Columns.Add(Base_Language.Lang_Global_NamaBarang, 0, HorizontalAlignment.Left) '5
		LvOrder.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Right) '5
		LvOrder.Columns.Add(Base_Language.Lang_Global_Satuan, 80, HorizontalAlignment.Center) '6
		LvOrder.Columns.Add(Base_Language.Lang_Global_Jenis, 0, HorizontalAlignment.Center) '7
		LvOrder.Columns.Add("Urut", 0, HorizontalAlignment.Center) '8
		LvOrder.Columns.Add("Id Jenis Produk", 0, HorizontalAlignment.Center) '9
		LvOrder.Columns.Add("from update", 100, HorizontalAlignment.Center) '10
		LvOrder.Columns.Add("deleted", 100, HorizontalAlignment.Center) '11

		' ListView2.View = View.Details
		LvBahan.Columns.Add("kode_stock_owner", 0, HorizontalAlignment.Left) '0
		LvBahan.Columns.Add("Kode Bahan", 200, HorizontalAlignment.Left) '1
		'LvBahan.Columns.Add("Nama Bahan", 320, HorizontalAlignment.Left) '2
		LvBahan.Columns.Add("Jumlah", 130, HorizontalAlignment.Right) '3
		LvBahan.Columns.Add("Satuan", 0, HorizontalAlignment.Center) '4
		LvBahan.Columns.Add("Stock", 130, HorizontalAlignment.Right) '5
		LvBahan.Columns.Add("Satuan", 117, HorizontalAlignment.Center) '6
		LvBahan.Columns.Add("nilai_barang", 0, HorizontalAlignment.Right) '7
		LvBahan.Columns.Add("satuan_barang", 0, HorizontalAlignment.Center) '8
		LvBahan.Columns.Add("stock_barang", 0, HorizontalAlignment.Right) '9
		LvBahan.Columns.Add("Keep Stock", 0, HorizontalAlignment.Right).DisplayIndex = 6
		LvBahan.Columns.Add("RV", 0, HorizontalAlignment.Right) '11

		LvPackaging.Columns.Add("kode_stock_owner", 0, HorizontalAlignment.Left) '0
		LvPackaging.Columns.Add("Kode Bahan", 200, HorizontalAlignment.Left) '1

		LvPackaging.Columns.Add("Jumlah", 130, HorizontalAlignment.Right) '2
		LvPackaging.Columns.Add("Satuan", 0, HorizontalAlignment.Center) '3
		LvPackaging.Columns.Add("Stock", 130, HorizontalAlignment.Right) '4
		LvPackaging.Columns.Add("Satuan", 117, HorizontalAlignment.Center) '5
		LvPackaging.Columns.Add("nilai_barang", 0, HorizontalAlignment.Right) '6
		LvPackaging.Columns.Add("satuan_barang", 0, HorizontalAlignment.Center) '7
		LvPackaging.Columns.Add("stock_barang", 0, HorizontalAlignment.Right) '8
		LvPackaging.Columns.Add("Keep Stock", 0, HorizontalAlignment.Right).DisplayIndex = 6
		LvPackaging.Columns.Add("Pot Stock", 0, HorizontalAlignment.Center) '10
		LvPackaging.Columns.Add("Jenis", 100, HorizontalAlignment.Right) '11
		LvPackaging.Columns.Add("jumlah_barang", 100, HorizontalAlignment.Right) '12
		LvPackaging.Columns.Add("Jumlah Bahan", 100, HorizontalAlignment.Right) '13

		Cmb_Jenis.Items.Clear()
		Cmb_Jenis.Items.Add("Commercial")
		Cmb_Jenis.Items.Add("Trial")
		'Cmb_Jenis.SelectedIndex = 0

		kosong()

	End Sub

	Private Sub kosong()

		'''Dim Rand As New Random
		'''Kode_Unik = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "ddMMyyHHmmss")

		jumlah_po = 0
		display_default()
		get_jam()

		RV_Parent_Formula = ""
		RV_Parent_Transaksi = ""

		Try
			OpenConn()

			Btn_Simpan.Tag = "&Simpan"

			ComboBox2.Items.Clear() : arrOrderBy.Clear()

			ComboBox2.Items.Add("No Faktur") : arrOrderBy.Add("a.no_faktur")
			'ComboBox2.Items.Add("Nama Barang") : arrOrderBy.Add("c.nama")

			arrInisialFaktur.Clear() : CmbLokasi.Items.Clear()
			SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & Lokasi & "' order by Kode_Stock_Owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbLokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
				Loop
			End Using
			CmbLokasi.Text = Lokasi

			arrInisialRouting.Clear() : cmb_routing.Items.Clear()

			SQL = "select id_routing, keterangan from  emi_master_routing where kode_perusahaan = '" & KodePerusahaan & "'"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					cmb_routing.Items.Add(dr("keterangan")) : arrInisialRouting.Add(dr("id_routing"))
				Loop
			End Using

			'DateTimePicker2.ResetText()
			DateTimePicker3.ResetText()
			cb_seluruh.Checked = False
			TxtCatatan.Text = ""
			txtKdBrgPO.Text = ""
			txtNmBrgPO.Text = ""
			Txt_No_Formula.Text = ""

			OpenConn()
			get_no_faktur()
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Button1_Click_1(Button1, Nothing)

		cmbLine.Items.Clear() : arrIdLine.Clear()
		LvOrder.Items.Clear()
		LvBahan.Items.Clear()
		Cmb_Jenis.SelectedIndex = -1
		Button3.Visible = False
		Btn_UnRelease.Visible = False

	End Sub

	Private Sub kosong_sebagian()

		jumlah_po = 0

		txtNoPo.Clear()
		'txtLokasi.Clear()
		txtKodeBarang.Clear()
		'txtNamaBarang.Clear()
		txtKodeCustomer.Clear()
		txtNamaCustomer.Clear()
		txtJumlah.Clear()
		txtSatuan.Clear()
		'txtJenisProduk.Clear()
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
		kosong()
	End Sub

	'Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
	'If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
	'    MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Belum_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'    Exit Sub
	'End If

	'If ListView2.Items.Count = 0 Then
	'    'looping data listview 1
	'    For indexLv1 As Integer = 0 To ListView1.Items.Count - 1
	'        get_isi_listview(indexLv1)

	'        If ListView1.FocusedItem.SubItems(1).Text <> lvNoPo Then
	'            If lvJmlhSisa <> 0 Then
	'                MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Harus_Sesuai_Antrian & " " & vbNewLine & Base_Language.Lang_Rencana_Produksi_No_Antrian & " :  " & lvNoAntri & vbNewLine & Base_Language.Lang_Rencana_Produksi_No_PO & " : " & lvNoPo & vbNewLine & Base_Language.Lang_Rencana_Produksi_Sisa & ",  : " & lvJmlhSisa)
	'                kosong_sebagian()
	'                Exit Sub
	'            End If
	'        End If

	'        Exit For
	'    Next
	'End If

	'If ListView2.Items.Count <> 0 Then
	'    If ListView1.FocusedItem.SubItems(cellLvJenisProduk).Text <> ListView2.Items(0).SubItems(cellLvJnsProdukDet).Text Then
	'        MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Jns_Produk_Beda, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'        Exit Sub
	'    End If

	'    If ListView1.FocusedItem.Text <> ListView2.Items.Count + 1 Then
	'        MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Hrs_Sesuai)
	'        Exit Sub
	'    End If

	'End If

	'For indexLv2 As Integer = 0 To ListView2.Items.Count - 1
	'    get_isi_listview_detail(indexLv2)

	'    Dim check_sisa As Double = 0

	'    If ListView1.FocusedItem.SubItems(cellLvNoPo).Text = lvNoPoDet Then

	'        check_sisa = Val(HilangkanTanda(ListView1.FocusedItem.SubItems(cellLvJmlhSisa).Text)) - Val(HilangkanTanda(lvJmlhDet))

	'        If (check_sisa <> 0) Then
	'            MessageBox.Show("Harap selesaikan semua produksi sesuai antrian." & vbNewLine & "No antrian :  " & lvNoAntri & vbNewLine & "No Po : " & lvNoPo & ", sisa : " & check_sisa)
	'            kosong_sebagian()
	'            Exit Sub
	'        End If
	'    Else

	'        For indexlv1 As Integer = 0 To ListView1.Items.Count - 1
	'            get_isi_listview(indexlv1)

	'            If lvNoPo = lvNoPoDet Then
	'                check_sisa = Val(HilangkanTanda(lvJmlhSisa)) - Val(HilangkanTanda(lvJmlhDet))

	'                If (check_sisa <> 0) Then
	'                    MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Hrs_Selesai_Semua & " " & vbNewLine & Base_Language.Lang_Rencana_Produksi_No_Antrian & " :  " & lvNoAntri & vbNewLine & Base_Language.Lang_Rencana_Produksi_No_PO & " : " & lvNoPo & Base_Language.Lang_Rencana_Produksi_Sisa & ", : " & check_sisa)
	'                    kosong_sebagian()
	'                    Exit Sub
	'                End If
	'            End If

	'        Next

	'    End If

	'Next

	'jumlah_po = ListView1.FocusedItem.SubItems(cellLvJmlhSisa).Text

	'txtNoPo.Text = ListView1.FocusedItem.SubItems(cellLvNoPo).Text
	'txtLokasi.Text = ListView1.FocusedItem.SubItems(cellLvLokasi).Text
	'txtKodeCustomer.Text = ListView1.FocusedItem.SubItems(cellLvKdCus).Text
	'txtNamaCustomer.Text = ListView1.FocusedItem.SubItems(cellLvNmCus).Text
	'txtKodeBarang.Text = ListView1.FocusedItem.SubItems(cellLvKdBrg).Text
	'txtNamaBarang.Text = ListView1.FocusedItem.SubItems(cellLvNmBrg).Text
	'txtJumlah.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(cellLvJmlhSisa).Text)
	'txtSatuan.Text = ListView1.FocusedItem.SubItems(cellLvSatuan).Text
	'txtJenisProduk.Text = ListView1.FocusedItem.SubItems(cellLvJenisProduk).Text
	'txtIdJenisProduk.Text = ListView1.FocusedItem.SubItems(cellLvIdJenisProduk).Text

	' End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnOk.Click
		If txtNoPo.Text.Trim.Length = 0 Then Exit Sub

		If txtJumlah.Text = 0 Or txtJumlah.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Jumlah_hrs_diisi)
			Exit Sub
		End If

		For i As Integer = 0 To LvOrder.Items.Count - 1
			If LvOrder.Items(i).SubItems(0).Text = LvData.FocusedItem.SubItems(1).Text Then
				MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_No_PO_Sdh_Ada, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If
		Next

		If txtJumlah.Text > jumlah_po Then
			MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Jumlah_Lebih_Besar, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If LvOrder.Items.Count = 0 Then

			'Try
			'    OpenConn()
			'    cmbLine.Items.Clear() : arrIdLine.Clear()
			'    SQL = "select a.id_line,a.kode_line,a.keterangan,a.Id_Jenis_Produk, b.Keterangan as jenis_produk from emi_line a, EMI_Jenis_Produk b "
			'    SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.id_jenis_produk = b.Id_Jenis_Produk "
			'    SQL = SQL & "and a.id_jenis_produk =  '" & txtIdJenisProduk.Text & "' "
			'    SQL = SQL & "order by a.kode_line "
			'    Using Dr = OpenTrans(SQL)
			'        Do While Dr.Read
			'            cmbLine.Items.Add(Dr("keterangan")) : arrIdLine.Add(Dr("id_line"))
			'        Loop
			'    End Using

			'    CloseConn()
			'Catch ex As Exception
			'    CloseConn()
			'    MessageBox.Show(ex.Message)
			'    Exit Sub
			'End Try

		End If

		Dim lvw As ListViewItem

		lvw = LvOrder.Items.Add(txtNoPo.Text)
		' lvw.SubItems.Add(txtLokasi.Text)
		lvw.SubItems.Add(txtKodeCustomer.Text)
		lvw.SubItems.Add(txtNamaCustomer.Text)
		lvw.SubItems.Add(txtKodeBarang.Text)
		'lvw.SubItems.Add(txtNamaBarang.Text)
		lvw.SubItems.Add(Format(Val(txtJumlah.Text), "N2"))
		lvw.SubItems.Add(txtSatuan.Text)
		' lvw.SubItems.Add(txtJenisProduk.Text)
		' lvw.SubItems.Add(txtIdJenisProduk.Text)

		kosong_sebagian()

	End Sub

	Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlah.KeyPress
		If e.KeyChar = Chr(13) Then
			Button2_Click(Me, Nothing)
		End If

		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If txtNoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Faktur, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf cmb_routing.Text = "" Then
			MessageBox.Show("Routing harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
			'ElseIf DateDiff(DateInterval.Day, Tanggal_Sekarang, DateTimePicker2.Value) < 0 Then
			'    MessageBox.Show("Tanggal tidak boleh lebih kecil dari tanggal sekarang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    Exit Sub
		ElseIf Cmb_Jenis.SelectedIndex = -1 Then
			MessageBox.Show("Jenis Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Jenis.DroppedDown = True : Cmb_Jenis.Focus()
			Exit Sub
		End If

		If LvOrder.Items.Count = 0 Or LvBahan.Items.Count = 0 Then
			MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Belum_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub

		End If

		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'=============================
			'=     CEK STATUS FAKTUR     =
			'=============================
			SQL = "select Status from EMI_Order_Produksi where Kode_Perusahaan = '" & KodePerusahaan & "' and status is not null and No_Faktur = '" & txtNoFaktur.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("No Faktur telah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'set stock owner nya menjadi production

			Dim SoProduction As String

			SQL = "select Kode_Stock_Owner From Stock_Owner_Gudang "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Produksi = 'Y' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					SoProduction = Dr("kode_stock_owner")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Lokasi Produksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Dim pesan As String = "Berikut terdapat bahan yang tidak mencukupi " & vbNewLine
			Dim flag_stok_cukup As Boolean = True

			If Btn_Simpan.Tag = "&Simpan" Then

				get_no_faktur()

				Dim jumlah As Double = 0
				Dim kode_so As String = ""
				Dim kode_brg As String = ""
				Dim sat As String = ""
				For i As Integer = 0 To LvOrder.Items.Count - 1
					get_isi_listview_detail(i)

					jumlah = jumlah + Val(HilangkanTanda(LvJmlh2))
					kode_so = LvLokasi2
					kode_brg = LvKdBrg2
					sat = LvSatuan2
				Next

				Dim berat As Double = 0
				SQL = "select top(1) berat From barang "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & kode_brg & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						berat = Dr("berat")
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Lokasi Produksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				If berat = 0 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Berat Satuan Belum di Set !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				SQL = "insert into emi_order_produksi(kode_perusahaan, no_faktur, tanggal, jam, userid, keterangan, kode_formula, "
				SQL = SQL & "id_routing, id_jenis_produk, Lokasi, kode_stock_owner, kode_barang, jumlah, satuan, berat, Flag_Commercial) values( "
				SQL = SQL & "'" & KodePerusahaan & "', '" & txtNoFaktur.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "' , "
				SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "','" & TxtCatatan.Text.Trim & "',"
				SQL = SQL & "'" & Txt_No_Formula.Text.Trim & "', '" & arrInisialRouting.Item(cmb_routing.SelectedIndex) & "',"
				SQL = SQL & "'" & txt_IdJenisProduk.Text & "','" & CmbLokasi.Text & "',"
				SQL = SQL & "'" & SoProduction & "', '" & kode_brg & "','" & jumlah & "', '" & sat & "', '" & berat & "', "
				If Cmb_Jenis.SelectedIndex = 0 Then
					SQL = SQL & "'Y' "
				Else
					SQL = SQL & "NULL "
				End If
				SQL = SQL & ")"
				ExecuteTrans(SQL)

				For i As Integer = 0 To LvOrder.Items.Count - 1

					get_isi_listview_detail(i)

					Dim faktur As String = LvOrder.Items(i).SubItems(0).Text

					SQL = "insert into emi_order_produksi_detail(kode_perusahaan,no_faktur,no_so,kode_stock_owner,kode_barang,jumlah,satuan,jenis_order,urut_po) values ("
					SQL = SQL & "'" & KodePerusahaan & "','" & txtNoFaktur.Text.Trim & "', '" & LvFaktur2 & "',"
					SQL = SQL & "'" & SoProduction & "', '" & LvKdBrg2 & "', "
					SQL = SQL & "'" & HilangkanTanda(LvJmlh2) & "', '" & LvSatuan2 & "', "
					SQL = SQL & "'" & faktur.Substring(0, 2) & "', '" & LvUrut2 & "' )"
					ExecuteTrans(SQL)

					If faktur.Substring(0, 2).ToUpper = "PO" Then
						'selesaikan flag di emi po
						SQL = "update emi_po_detail set flag_sudah_produksi = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and "
						SQL = SQL & "no_faktur = '" & LvFaktur2 & "' and No_Urut='" & LvUrut2 & "' "
						ExecuteTrans(SQL)
					Else
						'selesaikan flag di indepedent order
						SQL = "update EMI_Independent_Order_detail set flag_sudah_produksi = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and "
						SQL = SQL & "no_faktur = '" & LvFaktur2 & "' and No_Urut='" & LvUrut2 & "' "
						ExecuteTrans(SQL)
					End If
				Next

				'==========================
				'=     CEK RV FORMULA     =
				'==========================
				SQL = "select cast(RV as bigint) as RV from EMI_Transaksi_Formulator "
				SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and No_Faktur = '{Txt_No_Formula.Text.Trim}' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						If Dr("RV") <> RV_Parent_Formula.Trim Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Proses tidak dapat dilanjutkan karena formula ini sudah pernah diubah sebelumnya! Ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
							Exit Sub
						End If

					End If
				End Using

				For i As Integer = 0 To LvBahan.Items.Count - 1
					Get_Isi_Listview_Bahan(i)
					' MessageBox.Show(HilangkanTanda(ListView3.Items(i).SubItems(7).Text) & " - " & HilangkanTanda(ListView3.Items(i).SubItems(9).Text))
					'TODO : SIMPAN
					If Val(HilangkanTanda(LvNilaiBrg3)) > Val(HilangkanTanda(LvStockBrg3)) Then
						flag_stok_cukup = False

						pesan = pesan & LvKdBhn3 & "   :   " & Format(Val(HilangkanTanda(LvStock3)) - Val(HilangkanTanda(lvJmlh3)), "N4") & " " & LvSatuan3 & vbNewLine

						'CloseTrans()
						'CloseConn()
						'flag_stok_cukup = False
						''''MessageBox.Show("Terjadi kesalahan, Stock " & LvNmBhn3 & " Tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'MessageBox.Show("Terjadi kesalahan, Stock dengan kode barang " & LvKdBhn3 & " Tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'Exit Sub
					End If

					'==========================
					'=     CEK RV FORMULA     =
					'==========================
					SQL = "select cast(RV as bigint) as RV from EMI_Transaksi_Formulator_Detail_Bahan "
					SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
					SQL &= $"and No_Faktur = '{Txt_No_Formula.Text.Trim}' "
					SQL &= $"and Kode_Barang = '{LvKdBhn3.Trim}' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then

							If Dr("RV").ToString.Trim <> LvRV.Trim Then
								Dr.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("Proses tidak dapat dilanjutkan karena formula ini sudah pernah diubah sebelumnya! Ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
								Exit Sub
							End If

						End If
					End Using

					SQL = "insert into emi_order_produksi_detail_bahan(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang) values("
					SQL = SQL & "'" & KodePerusahaan & "','" & txtNoFaktur.Text & "' ,'" & SoProduction & "', '" & LvKdBhn3 & "', "
					SQL = SQL & "'" & HilangkanTanda(lvJmlh3) & "' , '" & LvSatuan3 & "',  "
					SQL = SQL & "'" & HilangkanTanda(LvNilaiBrg3) & "', '" & LvSatuanBrg3 & "' )"
					ExecuteTrans(SQL)

				Next

				For i As Integer = 0 To LvPackaging.Items.Count - 1
					Get_Isi_Listview_Packaging(i)
					' MessageBox.Show(HilangkanTanda(ListView3.Items(i).SubItems(7).Text) & " - " & HilangkanTanda(ListView3.Items(i).SubItems(9).Text))

					If Val(HilangkanTanda(LvNilaiBrg4)) > Val(HilangkanTanda(LvStockBrg4)) Then

						flag_stok_cukup = False
						pesan = pesan & LvKdBhn4 & "   :   " & Format(Val(HilangkanTanda(LvStock4)) - Val(HilangkanTanda(lvJmlh4)), "N4") & LvSatuan4 & vbNewLine
						'CloseTrans()
						'CloseConn()
						''''MessageBox.Show("Terjadi kesalahan, Stock " & LvNmBhn4 & " Tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'MessageBox.Show("Terjadi kesalahan, Stock dengan kode barang " & LvKdBhn4 & " Tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'Exit Sub
					End If

					SQL = "insert into emi_order_produksi_detail_Packaging(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang, jenis, jumlah_barang, Jumlah_Bahan) values("
					SQL = SQL & "'" & KodePerusahaan & "','" & txtNoFaktur.Text & "' ,'" & SoProduction & "', '" & LvKdBhn4 & "', "
					SQL = SQL & "'" & HilangkanTanda(lvJmlh4) & "' , '" & LvSatuan4 & "',  "
					SQL = SQL & "'" & HilangkanTanda(LvNilaiBrg4) & "', '" & LvSatuanBrg4 & "', '" & LvJenis4 & "', '" & LvMasterJumlah_Barang4 & "', '" & LvMasterJumlah_Bahan4 & "' )"
					ExecuteTrans(SQL)

				Next

				'CloseTrans()
				'CloseConn()
				'MessageBox.Show("pesan ini harus dikomen!")
				'Exit Sub

				'For i As Integer = 0 To ListView2.Items.Count - 1

				'Next
			Else

				SQL = "select status,selesai,flag_release from emi_order_produksi "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and no_faktur = '" & txtNoFaktur.Text & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						If General_Class.CekNULL(dr("status")) = "Y" Then
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show(Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						ElseIf General_Class.CekNULL(dr("selesai")) = "Y" Then
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Transaksi untuk no faktur ini telah selesai di produksi, tidak dapat di ubah kembali!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						ElseIf General_Class.CekNULL(dr("flag_release")) = "Y" Then
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Produksi Order sudah pernah direlease!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					Else
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Data Produksi Order tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'simpan dulu ke log sebelum update
				SQL = "insert into emi_order_produksi_log(Kode_Perusahaan,No_Faktur,Status,Tanggal,Jam,UserId,Tanggal_Produksi,Jam_Produksi,Id_Routing,selesai,Keterangan,Id_Jenis_Produk	,Kode_Formula) "
				SQL = SQL & "select Kode_Perusahaan,No_Faktur,Status,Tanggal,Jam,UserId,Tanggal_Produksi,Jam_Produksi,Id_Routing,selesai,Keterangan,Id_Jenis_Produk	,Kode_Formula from  "
				SQL = SQL & "emi_order_produksi where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "' "
				ExecuteTrans(SQL)

				SQL = "insert into Emi_Order_Produksi_Detail_log(Kode_Perusahaan,No_Faktur,No_SO,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Jenis_Order,Urut,Urut_PO) "
				SQL = SQL & "select Kode_Perusahaan,No_Faktur,No_SO,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Jenis_Order,Urut,Urut_PO from Emi_Order_Produksi_Detail  "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "' "
				ExecuteTrans(SQL)

				SQL = "insert into Emi_Order_Produksi_Detail_Bahan_log(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang) "
				SQL = SQL & "select Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang  from Emi_Order_Produksi_Detail_Bahan "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "'"
				ExecuteTrans(SQL)

				SQL = "insert into EMI_Order_Produksi_Detail_Packaging_log(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang) "
				SQL = SQL & "select Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang  from EMI_Order_Produksi_Detail_Packaging "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "'"
				ExecuteTrans(SQL)

				SQL = "delete from emi_order_produksi_detail where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "' "
				ExecuteTrans(SQL)

				SQL = "delete from emi_order_produksi_detail_bahan where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "' "
				ExecuteTrans(SQL)

				SQL = "delete from EMI_Order_Produksi_Detail_Packaging where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "' "
				ExecuteTrans(SQL)

				Dim jumlah As Double = 0
				Dim kode_so As String = ""
				Dim kode_brg As String = ""
				Dim sat As String = ""
				For i As Integer = 0 To LvOrder.Items.Count - 1
					'jumlah = jumlah + Val(HilangkanTanda(LvOrder.Items(i).SubItems(6).Text))
					jumlah = jumlah + Val(HilangkanTanda(LvOrder.Items(i).SubItems(5).Text))
					kode_so = LvOrder.Items(i).SubItems(1).Text
					kode_brg = LvOrder.Items(i).SubItems(4).Text
					'sat = LvOrder.Items(i).SubItems(7).Text
					sat = LvOrder.Items(i).SubItems(6).Text
				Next

				SQL = "update emi_order_produksi set "
				SQL = SQL & "id_routing = '" & arrInisialRouting.Item(cmb_routing.SelectedIndex) & "',"
				SQL = SQL & "keterangan =  '" & TxtCatatan.Text.Trim & "',"
				SQL = SQL & "id_jenis_produk = '" & txt_IdJenisProduk.Text & "',"
				SQL = SQL & "kode_formula = '" & Txt_No_Formula.Text & "', "
				SQL = SQL & "kode_stock_owner = '" & kode_so & "', "
				SQL = SQL & "kode_barang = '" & kode_brg & "', "
				SQL = SQL & "jumlah = '" & jumlah & "', "
				SQL = SQL & "satuan = '" & sat & "', "

				If Cmb_Jenis.SelectedIndex = 0 Then
					SQL = SQL & "Flag_Commercial='Y' "
				Else
					SQL = SQL & "Flag_Commercial=NULL "
				End If

				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and no_faktur = '" & txtNoFaktur.Text & "' "
				ExecuteTrans(SQL)

				For i As Integer = 0 To LvOrder.Items.Count - 1
					get_isi_listview_detail(i)
					Dim faktur As String = LvFaktur2
					If LvDeleted2 = "Y" Then
						If faktur.Substring(0, 2) = "PO" Then
							SQL = "update emi_po_detail set Flag_Sudah_Produksi = null "
							SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and no_faktur = '" & LvFaktur2 & "'"
							ExecuteTrans(SQL)
						ElseIf faktur.Substring(0, 2) = "IO" Then
							SQL = "update EMI_Independent_Order_detail set Flag_Sudah_Produksi = null "
							SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and no_faktur = '" & LvFaktur2 & "'"
							ExecuteTrans(SQL)
						End If
					Else

						SQL = "insert into emi_order_produksi_detail(kode_perusahaan,no_faktur,no_so,kode_stock_owner,kode_barang,jumlah,satuan,jenis_order,urut_po) values ("
						SQL = SQL & "'" & KodePerusahaan & "','" & txtNoFaktur.Text.Trim & "', '" & LvFaktur2 & "',"
						SQL = SQL & "'" & SoProduction & "', '" & LvKdBrg2 & "', "
						SQL = SQL & "'" & HilangkanTanda(LvJmlh2) & "', '" & LvSatuan2 & "', "
						SQL = SQL & "'" & faktur.Substring(0, 2) & "', '" & LvUrut2 & "' )"
						ExecuteTrans(SQL)

						If faktur.Substring(0, 2).ToUpper = "PO" Then
							'selesaikan flag di emi po
							SQL = "update emi_po_detail set flag_sudah_produksi = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and "
							SQL = SQL & "no_faktur = '" & LvFaktur2 & "' and No_Urut='" & LvUrut2 & "' "
							ExecuteTrans(SQL)
						Else
							'selesaikan flag di indepedent order
							SQL = "update EMI_Independent_Order_detail set flag_sudah_produksi = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and "
							SQL = SQL & "no_faktur = '" & LvFaktur2 & "' and No_Urut='" & LvUrut2 & "' "
							ExecuteTrans(SQL)
						End If

					End If

				Next

				''==========================
				''=     CEK RV FORMULA     =
				''==========================
				'SQL = "select cast(RV as bigint) as RV from Emi_Order_Produksi "
				'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
				'SQL &= $"and No_Faktur = '{txtNoFaktur.Text.Trim}' "
				'Using Dr = OpenTrans(SQL)
				'    If Dr.Read Then

				'        If Dr("RV").ToString.Trim <> RV_Parent_Transaksi Then
				'            Dr.Close()
				'            CloseTrans()
				'            CloseConn()
				'            MessageBox.Show("Proses tidak dapat dilanjutkan karena formula ini sudah pernah diubah sebelumnya! Ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
				'            Exit Sub
				'        End If

				'    End If
				'End Using

				For i As Integer = 0 To LvBahan.Items.Count - 1

					Get_Isi_Listview_Bahan(i)

					If Val(HilangkanTanda(LvNilaiBrg3)) > Val(HilangkanTanda(LvStockBrg3)) Then

						flag_stok_cukup = False
						pesan = pesan & LvKdBhn3 & "   :   " & Format(Val(HilangkanTanda(LvStock3)) - Val(HilangkanTanda(lvJmlh3)), "N4") & LvSatuan3 & vbNewLine
						'CloseTrans()
						'CloseConn()
						''''MessageBox.Show("Terjadi kesalahan, Stock " & LvNmBrg2 & " Tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'MessageBox.Show("Terjadi kesalahan, Stock " & LvKdBrg2 & " Tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'Exit Sub
					End If

					''==========================
					''=     CEK RV FORMULA     =
					''==========================
					'SQL = "select cast(RV as bigint) as RV from Emi_Order_Produksi_Detail_bahan "
					'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
					'SQL &= $"and No_Faktur = '{txtNoFaktur.Text.Trim}' "
					'SQL &= $"and Kode_Barang = '{LvKdBhn3.Trim}' "
					'Using Dr = OpenTrans(SQL)
					'    If Dr.Read Then

					'        If Dr("RV").ToString.Trim <> LvRV.Trim Then
					'            Dr.Close()
					'            CloseTrans()
					'            CloseConn()
					'            MessageBox.Show("Proses tidak dapat dilanjutkan karena formula ini sudah pernah diubah sebelumnya! Ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
					'            Exit Sub
					'        End If

					'    End If
					'End Using

					SQL = "insert into emi_order_produksi_detail_bahan(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang) values("
					SQL = SQL & "'" & KodePerusahaan & "','" & txtNoFaktur.Text & "' ,'" & SoProduction & "', '" & LvKdBhn3 & "', "
					SQL = SQL & "'" & HilangkanTanda(lvJmlh3) & "' , '" & LvSatuan3 & "',  "
					SQL = SQL & "'" & HilangkanTanda(LvNilaiBrg3) & "', '" & LvSatuanBrg3 & "' )"
					ExecuteTrans(SQL)

				Next

				For i As Integer = 0 To LvPackaging.Items.Count - 1
					Get_Isi_Listview_Packaging(i)
					' MessageBox.Show(HilangkanTanda(ListView3.Items(i).SubItems(7).Text) & " - " & HilangkanTanda(ListView3.Items(i).SubItems(9).Text))

					If Val(HilangkanTanda(LvNilaiBrg4)) > Val(HilangkanTanda(LvStockBrg4)) Then

						flag_stok_cukup = False
						pesan = pesan & "Kode Bahan : " & LvKdBhn4 & " : " & Format(Val(HilangkanTanda(LvStock4)) - Val(HilangkanTanda(lvJmlh4))) & LvSatuan4 & vbNewLine
						'CloseTrans()
						'CloseConn()
						''''MessageBox.Show("Terjadi kesalahan, Stock " & LvNmBhn4 & " Tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'MessageBox.Show("Terjadi kesalahan, Stock " & LvKdBhn4 & " Tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'Exit Sub
					End If

					SQL = "insert into emi_order_produksi_detail_Packaging(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Nilai_Barang,Satuan_Barang, jenis, jumlah_barang, Jumlah_Bahan) values("
					SQL = SQL & "'" & KodePerusahaan & "','" & txtNoFaktur.Text & "' ,'" & SoProduction & "', '" & LvKdBhn4 & "', "
					SQL = SQL & "'" & HilangkanTanda(lvJmlh4) & "' , '" & LvSatuan4 & "',  "
					SQL = SQL & "'" & HilangkanTanda(LvNilaiBrg4) & "', '" & LvSatuanBrg4 & "', '" & LvJenis4 & "', '" & LvMasterJumlah_Barang4 & "', '" & LvMasterJumlah_Bahan4 & "' )"
					ExecuteTrans(SQL)

				Next

			End If

			If flag_stok_cukup = False Then

				Dim tanya As String = MessageBox.Show(pesan & vbNewLine & "Apakah ingin melanjutkan transaksi ? ", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				If tanya = vbNo Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Transaksi dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End If

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK)
			kosong()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub BtnPembelian_Clear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
		kosong_sebagian()
	End Sub

	'Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles LvOrder.DoubleClick
	'    For i As Integer = 0 To LvOrder.Items.Count - 1

	'        If LvOrder.Items(i).SubItems(11).Text = "" Then
	'            If LvOrder.FocusedItem.Index <> LvOrder.Items.Count - 1 Then
	'                MessageBox.Show(Base_Language.Lang_Rencana_Produksi_Err_Hapus_Temp, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'                Exit Sub
	'            End If

	'            LvOrder.FocusedItem.Remove()

	'            If LvOrder.Items.Count = 0 Then
	'                cmbLine.Items.Clear() : arrIdLine.Clear()
	'            End If
	'        End If

	'    Next
	'End Sub

	Private Sub UbahAntrianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UbahAntrianToolStripMenuItem.Click
		'EMI_Perencanaan_Produksi_SD_Antrian.ComboBox1.Items.Clear()

		'For i As Integer = 1 To ListView1.Items.Count
		'    EMI_Perencanaan_Produksi_SD_Antrian.ComboBox1.Items.Add(i)
		'Next

		'EMI_Perencanaan_Produksi_SD_Antrian.ComboBox1.Text = ListView1.FocusedItem.Text

		'EMI_Perencanaan_Produksi_SD_Antrian.ShowDialog()
	End Sub

	Private Sub ListView1_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles LvData.ItemChecked

		If flag_barang_berbeda = "Y" Then
			flag_barang_berbeda = "T"
			MessageBox.Show("Barang yang ingin diproduksi tidak boleh berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If skipCB = "Y" Then
			If LvData.CheckedItems.Count > 0 Then

				'jika jumlah checked tidak sama dengan jumlah datanya
				If LvData.CheckedItems.Count <> LvData.Items.Count Then
					'matikan centang seluruh
					cb_seluruh.Checked = False
				Else
					'hidupkan centang seluruh
					cb_seluruh.Checked = True
				End If

				Dim totalJumlahProduksi As Double = 0
				Dim totalSerapan As Double = 0
				Dim nilai_production_order As Double = 0
				Dim nilaiPersentase As Double = 0

				Dim lks As String = ""
				Dim kodeBarang As String = ""
				Dim satuan_awal As String = ""
				Dim id_jenis_produk As String = ""

				' LvOrder.Items.Clear()

				For i As Integer = 0 To LvData.Items.Count - 1
					get_isi_listview(i)

					If LvData.Items(i).Checked = True Then

						If LvOrder.Items.Count <> 0 Then

							If LvOrder.Items(0).SubItems(4).Text <> lvKdBrg Then
								flag_barang_berbeda = "Y"
								LvData.Items(i).Checked = False
								Exit Sub
							End If
						End If

					End If
				Next

				Dim indexFocused As Integer = LvData.FocusedItem.Index

				If LvData.Items(indexFocused).Checked Then

					Dim lvw1 As ListViewItem

					lvw1 = LvOrder.Items.Add(LvData.FocusedItem.SubItems(cellLvNoPo).Text) '0
					lvw1.SubItems.Add(LvData.FocusedItem.SubItems(cellLvLokasi).Text) '1
					lvw1.SubItems.Add(LvData.FocusedItem.SubItems(cellLvKdCus).Text) '2
					lvw1.SubItems.Add(LvData.FocusedItem.SubItems(cellLvNmCus).Text) '3
					lvw1.SubItems.Add(LvData.FocusedItem.SubItems(cellLvKdBrg).Text) '4
					lvw1.SubItems.Add(Format(Val(HilangkanTanda(LvData.FocusedItem.SubItems(cellLvJmlh).Text)), "N2")) '5
					lvw1.SubItems.Add(LvData.FocusedItem.SubItems(cellLvSatuan).Text) '6
					lvw1.SubItems.Add(LvData.FocusedItem.SubItems(cellLvJenisProduk).Text) '7
					lvw1.SubItems.Add(LvData.FocusedItem.SubItems(cellLvUrut).Text) '8
					lvw1.SubItems.Add(LvData.FocusedItem.SubItems(cellLvIdJenisProduk).Text) '9
					lvw1.SubItems.Add("") '10
					lvw1.SubItems.Add("") '11

					txtKdBrgPO.Text = LvData.FocusedItem.SubItems(cellLvKdBrg).Text
					txtNmBrgPO.Text = LvData.FocusedItem.SubItems(cellLvNmBrg).Text

					'Dim cellNoSo2 As Integer = 0
					'Dim cellLokasi2 As Integer = 1
					'Dim cellKdCust2 As Integer = 2
					'Dim cellNmCust2 As Integer = 3
					'Dim cellKdBrg2 As Integer = 4
					'Dim cellJmlh2 As Integer = 5
					'Dim cellSatuan2 As Integer = 6
					'Dim cellJenis2 As Integer = 7
					'Dim cellUrut2 As Integer = 8
					'Dim cellIdJnsPrdk2 As Integer = 9
					'Dim cellFromUpdate2 As Integer = 10
					'Dim cellDeleted2 As Integer = 11

					'Dim cellLvNoPo As Integer = 0
					'Dim cellLvLokasi As Integer = 1
					'Dim cellLvKdCus As Integer = 2
					'Dim cellLvNmCus As Integer = 3
					'Dim cellLvKdBrg As Integer = 4
					'Dim cellLvJmlh As Integer = 5
					'Dim cellLvJmlhSisa As Integer = 6
					'Dim cellLvSatuan As Integer = 7
					'Dim cellLvJenisProduk As Integer = 8
					'Dim cellLvUrut As Integer = 9
					'Dim cellLvIdJenisProduk As Integer = 10

					'LvFaktur2 = LvOrder.Items(Index).SubItems(cellNoSo2).Text '0
					'LvLokasi2 = LvOrder.Items(Index).SubItems(cellLokasi2).Text '1
					'lvKdCust2 = LvOrder.Items(Index).SubItems(cellKdCust2).Text '2
					'LvNmCust2 = LvOrder.Items(Index).SubItems(cellNmCust2).Text '3
					'LvKdBrg2 = LvOrder.Items(Index).SubItems(cellKdBrg2).Text '4
					''LvNmBrg2 = LvOrder.Items(index).SubItems(cellNmBrg2).Text
					'LvJmlh2 = LvOrder.Items(Index).SubItems(cellJmlh2).Text '5
					'LvSatuan2 = LvOrder.Items(Index).SubItems(cellSatuan2).Text '6
					'LvJenis2 = LvOrder.Items(Index).SubItems(cellJenis2).Text '7
					'LvUrut2 = LvOrder.Items(Index).SubItems(cellUrut2).Text '8
					'LvIdJnsPrdk2 = LvOrder.Items(Index).SubItems(cellIdJnsPrdk2).Text '9
					'LvFromUpdate2 = LvOrder.Items(Index).SubItems(cellFromUpdate2).Text '10
					'LvDeleted2 = LvOrder.Items(Index).SubItems(cellDeleted2).Text '11
				Else

					Dim ambilIndexYangAkanDihapus As Integer

					For i As Integer = 0 To LvOrder.Items.Count - 1
						get_isi_listview_detail(i)
						If LvData.FocusedItem.SubItems(0).Text = LvFaktur2 Then
							ambilIndexYangAkanDihapus = i
						End If
					Next

					LvOrder.Items(ambilIndexYangAkanDihapus).Remove()

				End If

				For i As Integer = 0 To LvOrder.Items.Count - 1
					get_isi_listview_detail(i)
					If i = 0 Then
						lks = LvLokasi2
						kodeBarang = LvKdBrg2
						satuan_awal = LvSatuan2
						txt_IdJenisProduk.Text = LvIdJnsPrdk2
					End If

					totalJumlahProduksi = totalJumlahProduksi + LvJmlh2
				Next

				Try
					OpenConn()

					Dim satuan_akhir_init_barang As String = ""

					LvBahan.Items.Clear()
					LvPackaging.Items.Clear()
					Dim kd_barangINq As String = ""
					SQL = "select top(1) Kode_Barang_inq from barang "
					SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' "
					SQL = SQL & "and Kode_Barang ='" & kodeBarang & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							kd_barangINq = dr("Kode_Barang_inq")
						Else
							dr.Close()

							CloseConn()
							MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
							Exit Sub
						End If
					End Using

					SQL = "select Satuan_Berat From Init "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							satuan_akhir_init_barang = Dr("satuan_berat")
						Else
							Dr.Close()
							CloseConn()
							MessageBox.Show("Data tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End Using

					SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarang & "',"
					SQL = SQL & "'" & satuan_awal & "','" & satuan_akhir_init_barang & "',"
					SQL = SQL & "" & totalJumlahProduksi & ") as Hasil "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							If General_Class.CekNULL(dr("Hasil")) <> "" Then
								If dr("Hasil") = 0 Then
									LvOrder.Items.Clear()
									txtNmBrgPO.Text = ""
									txtKdBrgPO.Text = ""
									LvBahan.Items.Clear()
									LvPackaging.Items.Clear()
									MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								Else
									nilai_production_order = dr("hasil")
								End If
							Else
								dr.Close()
								LvOrder.Items.Clear()
								txtNmBrgPO.Text = ""
								txtKdBrgPO.Text = ""
								LvBahan.Items.Clear()
								LvPackaging.Items.Clear()
								CloseConn()
								'''
								MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End If
					End Using

					'============================
					'=     GET KODE FORMULA     =
					'============================
					Dim kode_formula As String = ""
					Dim tanggal_formula As String = ""

#Region "Kode Lama"

					'SQL = "select kode_formula, tanggal from EMI_Transaksi_Formulator_Binding where  "
					'SQL = SQL & "Kode_Barang = '" & kd_barangINq & "' and Aktif = 'Y'"
					'Using Dr = OpenTrans(SQL)
					'	If Dr.Read Then
					'		If General_Class.CekNULL(Dr("kode_formula")) = "" Then
					'			Dr.Close()

					'			CloseConn()
					'			TextBox1.Text = ""
					'			LvOrder.Items.Clear()
					'			MessageBox.Show("terjadi kesalahan, kode_formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					'			Exit Sub
					'		Else
					'			kode_formula = Dr("kode_formula")
					'			tanggal_formula = Dr("tanggal")
					'		End If
					'	Else
					'		Dr.Close()

					'		CloseConn()
					'		LvOrder.Items.Clear()
					'		TextBox1.Text = ""
					'		txtNmBrgPO.Text = ""
					'		txtKdBrgPO.Text = ""

					'		MessageBox.Show("Kode formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					'		Exit Sub
					'	End If
					'End Using
					''=========================================================

#End Region

#Region "Kode Baru"

					SQL = "select top 1 c.No_Faktur, c.Tanggal, c.Jam "
					SQL = SQL & "from N_EMI_Transaksi_Formulator_Binding a "
					SQL = SQL & "inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
					SQL = SQL & "inner join Emi_Transaksi_Formulator c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Formulator = c.No_Faktur and c.Status is null "
					SQL = SQL & "where a.Status is NULL "
					SQL = SQL & "and a.Flag_Validasi_Main = 'Y' "
					SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.Kode_Barang = '" & kd_barangINq & "' "
					SQL = SQL & "order by a.Tanggal DESC, a.Jam DESC, b.No_Prioritas ASC "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							kode_formula = Dr("No_Faktur")
							tanggal_formula = Dr("Tanggal")
						Else
							Dr.Close()
							CloseConn()
							LvOrder.Items.Clear()
							Txt_No_Formula.Text = ""
							txtNmBrgPO.Text = ""
							txtKdBrgPO.Text = ""

							MessageBox.Show("Kode formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End Using

#End Region

					SQL = "select hasil,satuan_hasil from Emi_Transaksi_Formulator where "
					SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & kode_formula & "' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then

							SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarang & "',"
							SQL = SQL & "'" & Dr("satuan_hasil") & "','" & satuan_akhir_init_barang & "',"
							SQL = SQL & "" & Dr("hasil") & ") as Hasil "
							Dr.Close()
							Using dr2 = OpenTrans(SQL)
								If dr2.Read Then
									If General_Class.CekNULL(dr2("Hasil")) <> "" Then
										If dr2("Hasil") = 0 Then
											MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										Else
											totalSerapan = dr2("hasil")
										End If
									Else
										dr2.Close()
										CloseConn()
										'''
										MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								End If
							End Using
						Else
							Dr.Close()
							CloseConn()
							LvOrder.Items.Clear()
							Txt_No_Formula.Text = ""
							MessageBox.Show("Formula tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End Using

					SQL = "select a.no_faktur,a.kode_stock_owner,a.kode_barang, c.nama, c.flag_potong_stok, "
					'   SQL = SQL & " isnull((select top(1) Nama From barang x where a.Kode_Perusahaan = x.Kode_Perusahaan and a.Kode_Barang  = x.Kode_Barang),null) as Nama,  "
					SQL = SQL & "a.nilai_barang,a.persentase,a.satuan_barang, "
					SQL = SQL & "isnull((select sum(jumlah) From barang_sn x, stock_owner_gudang y where x.kode_perusahaan=y.kode_perusahaan and x.kode_stock_owner=y.kode_stock_owner and y.Flag_Wharehouse='Y' and a.Kode_Perusahaan = x.Kode_Perusahaan and a.Kode_Barang  = x.Kode_Barang and x.warna='HIJAU'),0) as stock, "
					SQL = SQL & "isnull((select 0 ),0) as Keep_Stock, "
					SQL = SQL & "cast(b.RV as bigint) as RV_Parent, cast(a.RV as bigint) as RV_Detail "
					SQL = SQL & "From EMI_Transaksi_Formulator_Detail_Bahan a, Emi_Transaksi_Formulator b,barang c  "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and b.Status is null "
					SQL = SQL & "and a.kode_perusahaan = c.kode_perusahaan and a.kode_stock_owner = c.kode_stock_owner and a.kode_barang = c.kode_barang "
					SQL = SQL & "and b.kode_perusahaan = '" & KodePerusahaan & "' and b.no_faktur = '" & kode_formula & "' "
					SQL = SQL & "Order by a.kode_barang "
					Using ds = BindingTrans(SQL)
						With ds.Tables("MyTable")
							If .Rows.Count <> 0 Then
								For indexFormulator As Integer = 0 To .Rows.Count - 1
									Dim lvwFormulator As ListViewItem

									Dim jumlah As Double = 0

									nilaiPersentase = nilai_production_order / totalSerapan

									jumlah = Val(HilangkanTanda(Format(.Rows(indexFormulator).Item("nilai_barang"), "N4"))) * nilaiPersentase

									Dim convertKeSatuanAsli As String = ""
									Dim jumlahBarangDibutuhkan As Double = 0

									SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexFormulator).Item("kode_barang") & "' "
									SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
									Using Dr3 = OpenTrans(SQL)
										If Dr3.Read Then
											convertKeSatuanAsli = Dr3("satuan")
											SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexFormulator).Item("kode_barang") & "',"
											SQL = SQL & "'" & .Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
											SQL = SQL & "" & jumlah & ") as Hasil "
											Dr3.Close()

											Using dr4 = OpenTrans(SQL)
												If dr4.Read Then
													If General_Class.CekNULL(dr4("Hasil")) <> "" Then
														If dr4("Hasil") = 0 Then
															MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
															Exit Sub
														Else
															jumlahBarangDibutuhkan = Val(HilangkanTanda(Format(dr4("hasil"), "N4")))

														End If
													Else
														dr4.Close()
														CloseConn()
														'''
														MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If
												End If
											End Using
										Else
											Dr3.Close()
											CloseConn()
											MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using

									Dim stockConvert As Double = 0
									Dim converKesatuanAsliBarangStok As String = ""
									'============= convert nilai dan satuan stock barang ke tampilan display
									SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexFormulator).Item("kode_barang") & "' "
									SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
									Using Dr3 = OpenTrans(SQL)
										If Dr3.Read Then
											converKesatuanAsliBarangStok = Dr3("satuan")
											SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexFormulator).Item("kode_barang") & "',"
											SQL = SQL & "'" & .Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
											SQL = SQL & "" & .Rows(indexFormulator).Item("stock") & ") as Hasil "
											Dr3.Close()

											Using dr4 = OpenTrans(SQL)
												If dr4.Read Then
													If General_Class.CekNULL(dr4("Hasil")) <> "" Then

														stockConvert = Val(HilangkanTanda(Format(dr4("hasil"), "N4")))
													Else
														dr4.Close()
														CloseConn()
														'''
														MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If
												End If
											End Using
										Else
											Dr3.Close()
											CloseConn()
											MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using

									lvwFormulator = LvBahan.Items.Add(.Rows(indexFormulator).Item("kode_stock_owner"))
									lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("kode_barang"))
									''lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("nama"))
									lvwFormulator.SubItems.Add(Format(jumlahBarangDibutuhkan, "N4"))
									lvwFormulator.SubItems.Add(convertKeSatuanAsli)
									lvwFormulator.SubItems.Add(Format(stockConvert, "N4"))
									lvwFormulator.SubItems.Add(converKesatuanAsliBarangStok)
									lvwFormulator.SubItems.Add(Format(jumlah, "N4"))
									lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("satuan_barang"))
									lvwFormulator.SubItems.Add(Format(.Rows(indexFormulator).Item("stock"), "N4"))
									lvwFormulator.SubItems.Add(Format(.Rows(indexFormulator).Item("keep_stock"), "N4"))

									If General_Class.CekNULL(.Rows(indexFormulator).Item("flag_potong_stok")) = "" Then
										lvwFormulator.SubItems.Add("")
									Else
										lvwFormulator.SubItems.Add(Format(.Rows(indexFormulator).Item("flag_potong_stok")))
									End If
									lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("RV_Detail"))

									Txt_No_Formula.Text = kode_formula
									DateTimePicker3.Value = tanggal_formula
									RV_Parent_Formula = .Rows(indexFormulator).Item("RV_Parent")

								Next
							Else
								CloseConn()
								MessageBox.Show("Formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End With
					End Using

					SQL = "select a.kode_Barang,b.nama, b.Satuan as Satuan_Barang, a.Jumlah_Barang, a.Kode_Bahan, c.Nama as nama_bahan, c.flag_potong_stok, "
					SQL = SQL & "c.satuan as satuan_bahan, A.Jumlah_Bahan "
					SQL = SQL & ",isnull((select sum(jumlah) from barang_sn x, stock_owner_gudang y where x.kode_perusahaan=y.kode_perusahaan and x.kode_stock_owner=y.kode_stock_owner and y.Flag_Wharehouse='Y' and x.kode_perusahaan = a.kode_perusahaan and  x.Kode_Barang = a.Kode_Bahan and x.warna='HIJAU' "
					SQL = SQL & "),0) as good_stock, "
					SQL = SQL & "isnull((select 0),0) as keep_stock, a.Jenis "

					SQL = SQL & "from barang_detail_Bahan_Penolong a, barang b, barang c "
					SQL = SQL & "where b.Kode_barang='" & kodeBarang & "' and a.kode_Perusahaan=b.kode_Perusahaan and a.Kode_Barang=b.Kode_Barang_Inq and b.Kode_Stock_Owner='" & lks & "'  "
					SQL = SQL & "And a.kode_Perusahaan = c.kode_Perusahaan And a.Kode_Bahan = c.Kode_Barang And c.Kode_Stock_Owner ='" & lks & "' "
					'SQL = SQL & "and a.kode_Perusahaan=b.kode_Perusahaan and a.Kode_Barang=b.Kode_Barang_Inq and b.Kode_Stock_Owner='" & lks & "' "
					'SQL = SQL & " And a.kode_Perusahaan = c.kode_Perusahaan And a.Kode_Bahan = c.Kode_Barang And c.Kode_Stock_Owner ='" & lks & "' "
					SQL = SQL & "Order by a.kode_barang "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							For indexBahan = 0 To .Rows.Count - 1

								Dim lvwPackaging As ListViewItem
								Dim satuan_barang As String = .Rows(indexBahan).Item("Satuan_Barang")
								Dim Kode_bahan As String = .Rows(indexBahan).Item("Kode_Bahan")
								Dim satuan_bahan As String = .Rows(indexBahan).Item("Satuan_Bahan")
								Dim Jenis_bahan As String = .Rows(indexBahan).Item("Jenis")

								Dim jumlah As Double = .Rows(indexBahan).Item("Jumlah_Barang")
								Dim jumlahbahan As Double = Val(HilangkanTanda(Format(.Rows(indexBahan).Item("Jumlah_Bahan"), "N4")))
								Dim jumlahstock As Double = Val(HilangkanTanda(Format(.Rows(indexBahan).Item("good_stock"), "N4")))
								Dim jumlah_barang_satuan_barang As Double = 0

								SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarang & "',"
								SQL = SQL & "'" & satuan_awal & "','" & satuan_barang & "',"
								SQL = SQL & "" & totalJumlahProduksi & ") as Hasil "
								Using dr4 = OpenTrans(SQL)
									If dr4.Read Then
										If General_Class.CekNULL(dr4("Hasil")) <> "" Then
											If dr4("Hasil") = 0 Then
												MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											Else
												jumlah_barang_satuan_barang = dr4("hasil")

											End If
										Else
											dr4.Close()
											CloseConn()
											'''
											MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End If
								End Using

								Dim jumlahBahan_Total As Double = (jumlah_barang_satuan_barang / jumlah) * jumlahbahan

								Dim jumlahBahan_Total_display As Double = 0
								Dim jumlahstock_Total_display As Double = 0
								Dim satuan_display As String = ""

								'============= convert nilai dan satuan stock barang ke tampilan display
								SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & Kode_bahan & "' "
								SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
								Using Dr3 = OpenTrans(SQL)
									If Dr3.Read Then
										satuan_display = Dr3("satuan")

										'==== Convert NIlai Bahan
										SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Kode_bahan & "',"
										SQL = SQL & "'" & satuan_bahan & "','" & satuan_display & "',"
										SQL = SQL & "" & jumlahBahan_Total & ") as Hasil "
										Dr3.Close()

										Using dr4 = OpenTrans(SQL)
											If dr4.Read Then
												If General_Class.CekNULL(dr4("Hasil")) <> "" Then

													jumlahBahan_Total_display = dr4("hasil")
												Else
													dr4.Close()
													CloseConn()
													'''
													MessageBox.Show("Satuan " & satuan_bahan & " Ke " & satuan_display & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Exit Sub
												End If
											End If
										End Using

										'==== Convert NIlai Stock
										SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Kode_bahan & "',"
										SQL = SQL & "'" & satuan_bahan & "','" & satuan_display & "',"
										SQL = SQL & "" & jumlahstock & ") as Hasil "
										Dr3.Close()

										Using dr4 = OpenTrans(SQL)
											If dr4.Read Then
												If General_Class.CekNULL(dr4("Hasil")) <> "" Then

													jumlahstock_Total_display = dr4("hasil")
												Else
													dr4.Close()
													CloseConn()
													'''
													MessageBox.Show("Satuan " & satuan_bahan & " Ke " & satuan_display & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Exit Sub
												End If
											End If
										End Using
									Else
										Dr3.Close()
										CloseConn()
										MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								End Using
								'TODO : Check
								lvwPackaging = LvPackaging.Items.Add(lks)
								lvwPackaging.SubItems.Add(Kode_bahan)
								''lvwPackaging.SubItems.Add(.Rows(indexBahan).Item("nama_bahan"))
								lvwPackaging.SubItems.Add(Format(jumlahBahan_Total_display, "N4"))
								lvwPackaging.SubItems.Add(satuan_display)
								lvwPackaging.SubItems.Add(Format(jumlahstock_Total_display, "N4"))
								lvwPackaging.SubItems.Add(satuan_display)
								lvwPackaging.SubItems.Add(Format(jumlahBahan_Total, "N4"))
								lvwPackaging.SubItems.Add(satuan_bahan)
								lvwPackaging.SubItems.Add(Format(jumlahstock, "N4"))
								lvwPackaging.SubItems.Add(Format(.Rows(indexBahan).Item("keep_stock"), "N4"))
								If General_Class.CekNULL(.Rows(indexBahan).Item("flag_potong_stok")) = "" Then
									lvwPackaging.SubItems.Add("")
								Else
									lvwPackaging.SubItems.Add(Format(.Rows(indexBahan).Item("flag_potong_stok")))
								End If

								lvwPackaging.SubItems.Add(Jenis_bahan)
								lvwPackaging.SubItems.Add(jumlah)
								lvwPackaging.SubItems.Add(jumlahbahan)
							Next
						End With
					End Using

					CloseConn()
				Catch ex As Exception
					CloseConn()
					MessageBox.Show(ex.Message)
					Exit Sub
				End Try
			Else
				LvOrder.Items.Clear()
				LvBahan.Items.Clear()
				LvPackaging.Items.Clear()
				txtNmBrgPO.Text = ""
				txtKdBrgPO.Text = ""

				If txtNoFaktur.Text <> txt_faktur_bayangan.Text Then
					txtNoFaktur_Leave(Me, Nothing)
				End If

			End If
		End If

	End Sub

	Private Sub txtNoFaktur_Leave(sender As Object, e As EventArgs) Handles txtNoFaktur.Leave

		For i As Integer = 0 To LvData.Items.Count - 1
			LvData.Items(i).Checked = False
		Next

		Try
			OpenConn()

			If CekButtonRole("Release_PO_Produksi") = "T" Then
				Button3.Visible = False
			Else
				Button3.Visible = True
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Dim id_jenis_produk As Integer = 0
		Dim jenis_produk As String = ""
		Dim flag_release As String = ""

		Try
			OpenConn()

			LvOrder.Items.Clear()
			LvBahan.Items.Clear()
			LvPackaging.Items.Clear()
			TxtCatatan.Text = ""
			'  DateTimePicker1.ResetText()
			' DateTimePicker2.ResetText()

			Btn_Simpan.Tag = "&Update"

			SQL = "select a.Status,a.Flag_Release, a.Id_Routing,c.Keterangan as routing,a.Id_Jenis_Produk, b.Keterangan as jenis_produk, a.keterangan,a.kode_formula, a.Flag_Commercial "
			SQL = SQL & "from EMI_Order_Produksi a, EMI_Jenis_Produk b, EMI_Master_Routing c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Jenis_Produk = b.Id_Jenis_Produk "
			SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Routing = c.Id_Routing "
			SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & txtNoFaktur.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL("status") = "Y" Then
						kosong()
						Exit Sub
					Else
						If General_Class.CekNULL(Dr("keterangan")) = "" Then
							TxtCatatan.Text = ""
						Else
							TxtCatatan.Text = Dr("keterangan")
						End If

						' DateTimePicker1.Value = Dr("tanggal_produksi")
						'  DateTimePicker2.Value = Dr("jam_produksi")

						cmb_routing.Text = Dr("routing")

						id_jenis_produk = Dr("id_jenis_produk")
						jenis_produk = Dr("jenis_produk")
						txt_IdJenisProduk.Text = Dr("id_jenis_produk")
						Txt_No_Formula.Text = Dr("kode_formula")
						If General_Class.CekNULL(Dr("flag_release")) = "" Then
							flag_release = ""
						Else
							flag_release = Dr("flag_release")

						End If

						If General_Class.CekNULL(Dr("Flag_Commercial")) = "Y" Then
							Cmb_Jenis.SelectedIndex = 0
						Else
							Cmb_Jenis.SelectedIndex = 1
						End If

					End If
				Else
					CloseConn()
					kosong()
					Exit Sub
				End If
			End Using

			SQL = "select a.No_SO,a.Kode_Stock_Owner, a.Kode_Barang,b.Nama,a.Jumlah,a.Satuan,a.jenis_order,urut_po "
			SQL = SQL & "from Emi_Order_Produksi_detail a, barang b"
			SQL = SQL & " where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
			SQL = SQL & "and a.Kode_Barang =  b.Kode_Barang and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & txtNoFaktur.Text & "' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then

						For i As Integer = 0 To .Rows.Count - 1

							If i = 0 Then
								txtNmBrgPO.Text = .Rows(i).Item("nama")
								txtKdBrgPO.Text = .Rows(i).Item("kode_barang")
							End If

							Dim lvw As ListViewItem

							lvw = LvOrder.Items.Add(.Rows(i).Item("no_so"))
							lvw.SubItems.Add(.Rows(i).Item("kode_stock_owner"))
							lvw.SubItems.Add("-")
							lvw.SubItems.Add("-")
							lvw.SubItems.Add(.Rows(i).Item("kode_barang"))
							''lvw.SubItems.Add(.Rows(i).Item("nama"))
							lvw.SubItems.Add(Format(.Rows(i).Item("jumlah"), "N2"))
							lvw.SubItems.Add(.Rows(i).Item("satuan"))
							lvw.SubItems.Add(.Rows(i).Item("jenis_order"))
							lvw.SubItems.Add(.Rows(i).Item("urut_po"))
							lvw.SubItems.Add(id_jenis_produk)
							lvw.SubItems.Add("Y")
							lvw.SubItems.Add("")
						Next
					Else
						CloseConn()
						MessageBox.Show("Data Production Order tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						kosong()
						Exit Sub
					End If
				End With
			End Using

			SQL = "select a.kode_stock_owner,a.kode_barang,b.nama,a.jumlah,a.satuan as satuan_display,a.nilai_barang,a.satuan_barang,b.flag_potong_stok, "
			SQL = SQL & "isnull((select sum(Jumlah) from barang_sn x, stock_owner_gudang y where x.kode_perusahaan=y.kode_perusahaan and x.kode_stock_owner=y.kode_stock_owner and y.Flag_Wharehouse='Y' and a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and x.kode_barang = a.kode_barang and x.warna='HIJAU'),0) as stock "
			SQL = SQL & ",b.satuan,  "
			SQL = SQL & "isnull((select 0),0) as Keep_Stock, cast(a.RV as bigint) as RV_Detail, cast(c.RV as bigint) as RV_Parent "
			SQL = SQL & "from Emi_Order_Produksi_Detail_bahan a,barang b, Emi_Order_Produksi c "
			SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan  and a.kode_stock_owner = b.Kode_Stock_Owner "
			SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Faktur "
			SQL = SQL & "and a.kode_barang = b.kode_barang and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.no_faktur = '" & txtNoFaktur.Text & "' "
			SQL = SQL & "Order by a.kode_barang "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then

						Dim converKesatuanAsliBarangStok As String = ""
						Dim stockConvert As Double = 0

						For i As Integer = 0 To .Rows.Count - 1
							SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
							SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
							Using Dr3 = OpenTrans(SQL)
								If Dr3.Read Then
									converKesatuanAsliBarangStok = Dr3("satuan")
									SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("kode_barang") & "',"
									SQL = SQL & "'" & .Rows(i).Item("satuan") & "','" & Dr3("satuan") & "',"
									SQL = SQL & "" & .Rows(i).Item("stock") & ") as Hasil "
									Dr3.Close()

									Using dr4 = OpenTrans(SQL)
										If dr4.Read Then
											If General_Class.CekNULL(dr4("Hasil")) <> "" Then

												stockConvert = dr4("hasil")
											Else
												dr4.Close()
												CloseConn()
												MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & Dr3("satuan") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If
										End If
									End Using
								Else
									Dr3.Close()
									CloseConn()
									MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							Dim lvw As ListViewItem
							lvw = LvBahan.Items.Add(.Rows(i).Item("kode_stock_owner"))
							lvw.SubItems.Add(.Rows(i).Item("kode_barang"))
							''lvw.SubItems.Add(.Rows(i).Item("nama"))
							lvw.SubItems.Add(Format(.Rows(i).Item("jumlah"), "N4"))
							lvw.SubItems.Add(.Rows(i).Item("satuan_display"))
							lvw.SubItems.Add(Format(stockConvert, "N4"))
							lvw.SubItems.Add(converKesatuanAsliBarangStok)
							lvw.SubItems.Add(.Rows(i).Item("nilai_barang"))
							lvw.SubItems.Add(.Rows(i).Item("satuan_barang"))
							lvw.SubItems.Add(.Rows(i).Item("stock"))
							lvw.SubItems.Add(Format(.Rows(i).Item("keep_stock"), "N4"))

							If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
								lvw.SubItems.Add("")
							Else
								lvw.SubItems.Add(Format(.Rows(i).Item("flag_potong_stok")))
							End If

							lvw.SubItems.Add(.Rows(i).Item("RV_Detail"))

							RV_Parent_Transaksi = .Rows(i).Item("RV_Parent")

						Next
						'Else
						'    CloseConn()
						'    MessageBox.Show("Data produksi Order tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'    kosong()
						'    Exit Sub
					End If
				End With
			End Using

			'packaging
			SQL = "select a.kode_stock_owner,a.kode_barang,b.nama,a.jumlah,a.satuan as satuan_display,a.nilai_barang,a.satuan_barang,b.flag_potong_stok, "
			SQL = SQL & "isnull((select sum(jumlah) from barang_sn x, stock_owner_gudang y where x.kode_perusahaan=y.kode_perusahaan and x.kode_stock_owner=y.kode_stock_owner and y.Flag_Wharehouse='Y' and a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and x.kode_barang = a.kode_barang and x.warna='HIJAU'),0) as stock "
			SQL = SQL & ",b.satuan,"
			SQL = SQL & "isnull((select 0),0) as Keep_Stock, a.jenis, jumlah_barang, Jumlah_Bahan "
			SQL = SQL & " from EMI_Order_Produksi_Detail_Packaging a,barang b  "
			SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan  and a.kode_stock_owner = b.Kode_Stock_Owner "
			SQL = SQL & "and a.kode_barang = b.kode_barang and a.kode_perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "a.no_faktur = '" & txtNoFaktur.Text & "' "
			SQL = SQL & "Order by a.kode_barang "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then

						Dim converKesatuanAsliBarangStok As String = ""
						Dim stockConvert As Double = 0

						For i As Integer = 0 To .Rows.Count - 1
							SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
							SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
							Using Dr3 = OpenTrans(SQL)
								If Dr3.Read Then
									converKesatuanAsliBarangStok = Dr3("satuan")
									SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("kode_barang") & "',"
									SQL = SQL & "'" & .Rows(i).Item("satuan") & "','" & Dr3("satuan") & "',"
									SQL = SQL & "" & .Rows(i).Item("stock") & ") as Hasil "
									Dr3.Close()

									Using dr4 = OpenTrans(SQL)
										If dr4.Read Then
											If General_Class.CekNULL(dr4("Hasil")) <> "" Then

												stockConvert = dr4("hasil")
											Else
												dr4.Close()
												CloseConn()
												MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & Dr3("satuan") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If
										End If
									End Using
								Else
									Dr3.Close()
									CloseConn()
									MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							Dim lvw As ListViewItem
							lvw = LvPackaging.Items.Add(.Rows(i).Item("kode_stock_owner"))
							lvw.SubItems.Add(.Rows(i).Item("kode_barang"))
							''lvw.SubItems.Add(.Rows(i).Item("nama"))
							lvw.SubItems.Add(Format(.Rows(i).Item("jumlah"), "N4"))
							lvw.SubItems.Add(.Rows(i).Item("satuan_display"))
							lvw.SubItems.Add(Format(stockConvert, "N4"))
							lvw.SubItems.Add(converKesatuanAsliBarangStok)
							lvw.SubItems.Add(.Rows(i).Item("nilai_barang"))
							lvw.SubItems.Add(.Rows(i).Item("satuan_barang"))
							lvw.SubItems.Add(.Rows(i).Item("stock"))
							lvw.SubItems.Add(Format(.Rows(i).Item("keep_stock"), "N4"))

							If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
								lvw.SubItems.Add("")
							Else
								lvw.SubItems.Add(Format(.Rows(i).Item("flag_potong_stok")))
							End If
							lvw.SubItems.Add(.Rows(i).Item("jenis"))
							lvw.SubItems.Add(.Rows(i).Item("jumlah_barang"))
							lvw.SubItems.Add(.Rows(i).Item("Jumlah_Bahan"))
						Next
						'Else
						'    CloseConn()
						'    MessageBox.Show("Data produksi Order tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'    kosong()
						'    Exit Sub
					End If
				End With
			End Using

			'ListView2.Columns.Add(Base_Language.Lang_Global_No_PO, 130, HorizontalAlignment.Left) '0
			'ListView2.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Left) '1
			'ListView2.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 0, HorizontalAlignment.Left) '2
			'ListView2.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 0, HorizontalAlignment.Left) '3
			'ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 135, HorizontalAlignment.Left) '4
			'ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 200, HorizontalAlignment.Left) '5
			'ListView2.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center) '6
			'ListView2.Columns.Add(Base_Language.Lang_Global_Satuan, 90, HorizontalAlignment.Center) '7
			'ListView2.Columns.Add(Base_Language.Lang_Global_Jenis, 0, HorizontalAlignment.Center) '8
			'ListView1.Columns.Add("Urut", 0, HorizontalAlignment.Center)
			'ListView2.Columns.Add("Id Jenis Produk", 0, HorizontalAlignment.Center)

			'' ListView2.View = View.Details
			'ListView3.Columns.Add("kode_stock_owner", 0, HorizontalAlignment.Left) '0
			'ListView3.Columns.Add("Kode Bahan", 130, HorizontalAlignment.Left) '1
			'ListView3.Columns.Add("Nama Bahan", 130, HorizontalAlignment.Left) '2
			'ListView3.Columns.Add("Jumlah", 130, HorizontalAlignment.Center) '3
			'ListView3.Columns.Add("Satuan", 90, HorizontalAlignment.Center) '4
			'ListView3.Columns.Add("Stock", 130, HorizontalAlignment.Center) '5
			'ListView3.Columns.Add("Satuan", 90, HorizontalAlignment.Center) '6
			'ListView3.Columns.Add("nilai_barang", 0, HorizontalAlignment.Center) '7
			'ListView3.Columns.Add("satuan_barang", 0, HorizontalAlignment.Center) '8
			'ListView3.Columns.Add("stock_barang", 0, HorizontalAlignment.Center) '9

			If flag_release = "Y" Then
				display_summary()
			Else
				display_default()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			If flag_release = "Y" Then
				If CekButtonRole("UnRelease_PO_Produksi") = "T" Then
					Btn_UnRelease.Visible = False
				Else
					Btn_UnRelease.Visible = True
				End If
			Else
				Btn_UnRelease.Visible = False
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub txtNoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoFaktur.KeyPress
		If e.KeyChar = Chr(13) Then btnRefresh.Focus()
	End Sub

	Private Sub display_summary()
		LvData.Enabled = False
		LvOrder.Enabled = False
		' LvBahan.Enabled = False
		' LvPackaging.Enabled = False
		Btn_Simpan.Enabled = False
		Button3.Enabled = False

	End Sub

	Private Sub display_default()
		LvData.Enabled = True
		LvOrder.Enabled = True
		LvBahan.Enabled = True
		LvPackaging.Enabled = True
		Btn_Simpan.Enabled = True

		Button3.Enabled = True

	End Sub

	Private Sub HapusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem.Click
		If LvOrder.Items.Count = 0 Then
			MessageBox.Show("Silahkan pilih data terlebih dahulu untuk dihapus")
			Exit Sub
		End If

		If LvOrder.FocusedItem.SubItems(11).Text = "Y" Then
			LvOrder.FocusedItem.SubItems(11).Text = ""
		Else
			LvOrder.FocusedItem.SubItems(11).Text = "Y"
		End If

		If LvOrder.FocusedItem.SubItems(11).Text = "Y" Then
			LvOrder.FocusedItem.BackColor = Color.Red
		Else
			LvOrder.FocusedItem.BackColor = Color.Transparent
		End If

		Dim totalJumlahProduksi As Double = 0

		Dim lks As String = ""
		Dim kodeBarang As String = ""
		Dim satuan_awal As String = ""
		Dim id_jenis_produk As String = ""
		Dim dataYangDiDelete As Integer = 0

		For i As Integer = 0 To LvOrder.Items.Count - 1
			get_isi_listview_detail(i)

			If i = 0 Then
				lks = LvLokasi2
				kodeBarang = LvKdBrg2
				satuan_awal = LvSatuan2
				txt_IdJenisProduk.Text = LvIdJnsPrdk2
			End If

			If LvOrder.Items(i).SubItems(11).Text = "" Then
				totalJumlahProduksi = totalJumlahProduksi + LvOrder.Items(i).SubItems(6).Text
			End If

			If LvOrder.Items(i).SubItems(11).Text = "Y" Then
				dataYangDiDelete = dataYangDiDelete + 1
			End If
		Next

		Dim totalSerapan As Double = 0
		Dim nilai_production_order As Double = 0
		Dim nilaiPersentase As Double = 0
		'' mulai baru

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim satuan_akhir_init_barang As String = ""

			LvBahan.Items.Clear()
			LvPackaging.Items.Clear()

			LvBahan.Items.Clear()
			LvPackaging.Items.Clear()
			Dim kd_barangINq As String = ""
			SQL = "select top(1) Kode_Barang_inq from barang "
			SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' "
			SQL = SQL & "and Kode_Barang ='" & kodeBarang & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					kd_barangINq = dr("Kode_Barang_inq")
				Else
					dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Exit Sub
				End If
			End Using

			SQL = "select Satuan_Berat From Init "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					satuan_akhir_init_barang = Dr("satuan_berat")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarang & "',"
			SQL = SQL & "'" & satuan_awal & "','" & satuan_akhir_init_barang & "',"
			SQL = SQL & "" & totalJumlahProduksi & ") as Hasil "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					If General_Class.CekNULL(dr("Hasil")) <> "" Then
						If dr("Hasil") = 0 Then
							MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						Else
							nilai_production_order = dr("hasil")
						End If
					Else
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End If
			End Using

			''=========================     'ambil kode formula============================'
			Dim kode_formula As String = ""
			Dim tanggal_formula As String = ""

			SQL = "select kode_formula,tanggal from EMI_Transaksi_Formulator_Binding where  "
			SQL = SQL & "Kode_Barang = '" & kd_barangINq & "' and Aktif = 'Y'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("kode_formula")) = "" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("terjadi kesalahan, kode_formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					Else
						kode_formula = Dr("kode_formula")
						tanggal_formula = Dr("tanggal")
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Kode formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using
			'=========================================================

			SQL = "select hasil,satuan_hasil from Emi_Transaksi_Formulator where "
			SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & kode_formula & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarang & "',"
					SQL = SQL & "'" & Dr("satuan_hasil") & "','" & satuan_akhir_init_barang & "',"
					SQL = SQL & "" & Dr("hasil") & ") as Hasil "
					Dr.Close()

					Using dr2 = OpenTrans(SQL)
						If dr2.Read Then
							If General_Class.CekNULL(dr2("Hasil")) <> "" Then
								If dr2("Hasil") = 0 Then
									dr2.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								Else
									totalSerapan = dr2("hasil")
								End If
							Else
								dr2.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End If
					End Using
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Formula tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "select a.no_faktur,a.kode_stock_owner,a.kode_barang,b.nama,a.nilai_barang,a.persentase,a.satuan_barang, "
			SQL = SQL & "isnull((select sum(x.jumlah) from barang_sn x , stock_owner_gudang y where x.kode_perusahaan=y.kode_perusahaan and x.kode_stock_owner=y.kode_stock_owner and y.Flag_Wharehouse='Y' and x.kode_barang=b.kode_barang and x.warna='HIJAU'),0) as stock "
			SQL = SQL & "from EMI_Transaksi_Formulator_Detail_Bahan a, barang b where  "
			SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.kode_stock_owner = b.kode_stock_owner "
			SQL = SQL & "And a.kode_barang = b.kode_barang "
			SQL = SQL & "And a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & kode_formula & "' "
			SQL = SQL & "Order by a.kode_barang "
			Using ds = BindingTrans(SQL)
				With ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For indexFormulator As Integer = 0 To .Rows.Count - 1
							Dim lvwFormulator As ListViewItem

							Dim jumlah As Double = 0

							nilaiPersentase = nilai_production_order / totalSerapan

							jumlah = Val(HilangkanTanda(Format(.Rows(indexFormulator).Item("nilai_barang"), "N4"))) * nilaiPersentase

							Dim convertKeSatuanAsli As String = ""
							Dim jumlahBarangDibutuhkan As Double = 0

							SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexFormulator).Item("kode_barang") & "' "
							SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
							Using Dr3 = OpenTrans(SQL)
								If Dr3.Read Then
									convertKeSatuanAsli = Dr3("satuan")
									SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexFormulator).Item("kode_barang") & "',"
									SQL = SQL & "'" & .Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
									SQL = SQL & "" & jumlah & ") as Hasil "
									Dr3.Close()

									Using dr4 = OpenTrans(SQL)
										If dr4.Read Then
											If General_Class.CekNULL(dr4("Hasil")) <> "" Then
												If dr4("Hasil") = 0 Then
													dr4.Close()
													CloseTrans()
													CloseConn()
													MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Exit Sub
												Else
													jumlahBarangDibutuhkan = dr4("hasil")

												End If
											Else
												dr4.Close()
												CloseTrans()
												CloseConn()
												MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If
										End If
									End Using
								Else
									Dr3.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							Dim stockConvert As Double = 0
							Dim converKesatuanAsliBarangStok As String = ""
							'============= convert nilai dan satuan stock barang ke tampilan display
							SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexFormulator).Item("kode_barang") & "' "
							SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
							Using Dr3 = OpenTrans(SQL)
								If Dr3.Read Then
									converKesatuanAsliBarangStok = Dr3("satuan")
									SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexFormulator).Item("kode_barang") & "',"
									SQL = SQL & "'" & .Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
									SQL = SQL & "" & .Rows(indexFormulator).Item("stock") & ") as Hasil "
									Dr3.Close()

									Using dr4 = OpenTrans(SQL)
										If dr4.Read Then
											If General_Class.CekNULL(dr4("Hasil")) <> "" Then

												stockConvert = dr4("hasil")
											Else
												dr4.Close()
												CloseTrans()
												CloseConn()
												MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If
										End If
									End Using
								Else
									Dr3.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							lvwFormulator = LvBahan.Items.Add(.Rows(indexFormulator).Item("kode_stock_owner"))
							lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("kode_barang"))
							''lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("nama"))
							lvwFormulator.SubItems.Add(Format(jumlahBarangDibutuhkan, "N4"))
							lvwFormulator.SubItems.Add(convertKeSatuanAsli)
							lvwFormulator.SubItems.Add(Format(stockConvert, "N4"))
							lvwFormulator.SubItems.Add(converKesatuanAsliBarangStok)
							lvwFormulator.SubItems.Add(Format(jumlah, "N4"))
							lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("satuan_barang"))
							lvwFormulator.SubItems.Add(Format(.Rows(indexFormulator).Item("stock"), "N4"))
							Txt_No_Formula.Text = kode_formula
							DateTimePicker3.Value = tanggal_formula

						Next
					Else
						CloseConn()
						MessageBox.Show("Formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			'packaging

			SQL = "select a.kode_Barang,b.nama, b.Satuan as Satuan_Barang, a.Jumlah_Barang, a.Kode_Bahan, c.Nama as nama_bahan, c.satuan as satuan_bahan, A.Jumlah_Bahan, "
			SQL = SQL & "isnull((select sum(x.jumlah) from barang_sn x , stock_owner_gudang y where x.kode_perusahaan=y.kode_perusahaan and x.kode_stock_owner=y.kode_stock_owner and y.Flag_Wharehouse='Y' and x.kode_barang=c.kode_barang and x.warna='HIJAU'),0) as good_stock, a.jenis, c.flag_potong_stok, "

			SQL = SQL & "isnull((select 0),0) as keep_stock "

			SQL = SQL & "from barang_detail_Bahan_Penolong a, barang b, barang c where b.Kode_barang='" & kodeBarang & "' "
			SQL = SQL & "and a.kode_Perusahaan=b.kode_Perusahaan and a.Kode_Barang=b.Kode_Barang_Inq and b.Kode_Stock_Owner='" & lks & "' "
			SQL = SQL & " And a.kode_Perusahaan = c.kode_Perusahaan And a.Kode_Bahan = c.Kode_Barang And c.Kode_Stock_Owner ='" & lks & "' "
			SQL = SQL & "Order by a.kode_barang "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For indexBahan = 0 To .Rows.Count - 1

						Dim lvwPackaging As ListViewItem
						Dim satuan_barang As String = .Rows(indexBahan).Item("Satuan_Barang")
						Dim Kode_bahan As String = .Rows(indexBahan).Item("Kode_Bahan")
						Dim satuan_bahan As String = .Rows(indexBahan).Item("Satuan_Bahan")
						Dim Jenis_bahan As String = .Rows(indexBahan).Item("Jenis_Bahan")

						Dim flag_potong_stock As String = .Rows(indexBahan).Item("flag_potong_stok")

						Dim jumlah As Double = .Rows(indexBahan).Item("Jumlah_Barang")
						Dim jumlahbahan As Double = Format(.Rows(indexBahan).Item("Jumlah_Bahan"), "N4")
						Dim jumlahstock As Double = Format(.Rows(indexBahan).Item("good_stock"), "N4")
						Dim jumlah_barang_satuan_barang As Double = 0

						SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarang & "',"
						SQL = SQL & "'" & satuan_awal & "','" & satuan_barang & "',"
						SQL = SQL & "" & totalJumlahProduksi & ") as Hasil "
						Using dr4 = OpenTrans(SQL)
							If dr4.Read Then
								If General_Class.CekNULL(dr4("Hasil")) <> "" Then
									If dr4("Hasil") = 0 Then
										dr4.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									Else
										jumlah_barang_satuan_barang = dr4("hasil")
									End If
								Else
									dr4.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End If
						End Using

						Dim jumlahBahan_Total As Double = (jumlah_barang_satuan_barang / jumlah) * jumlahbahan

						Dim jumlahBahan_Total_display As Double = 0
						Dim jumlahstock_Total_display As Double = 0
						Dim satuan_display As String = ""

						'============= convert nilai dan satuan stock barang ke tampilan display
						SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & Kode_bahan & "' "
						SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
						Using Dr3 = OpenTrans(SQL)
							If Dr3.Read Then
								satuan_display = Dr3("satuan")

								'==== Convert NIlai Bahan
								SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Kode_bahan & "',"
								SQL = SQL & "'" & satuan_bahan & "','" & satuan_display & "',"
								SQL = SQL & "" & jumlahBahan_Total & ") as Hasil "
								Dr3.Close()

								Using dr4 = OpenTrans(SQL)
									If dr4.Read Then
										If General_Class.CekNULL(dr4("Hasil")) <> "" Then

											jumlahBahan_Total_display = dr4("hasil")
										Else
											dr4.Close()
											CloseTrans()
											CloseConn()
											MessageBox.Show("Satuan " & satuan_bahan & " Ke " & satuan_display & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End If
								End Using

								'==== Convert NIlai Stock
								SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Kode_bahan & "',"
								SQL = SQL & "'" & satuan_bahan & "','" & satuan_display & "',"
								SQL = SQL & "" & jumlahstock & ") as Hasil "
								Dr3.Close()

								Using dr4 = OpenTrans(SQL)
									If dr4.Read Then
										If General_Class.CekNULL(dr4("Hasil")) <> "" Then

											jumlahstock_Total_display = dr4("hasil")
										Else
											dr4.Close()
											CloseTrans()
											CloseConn()
											MessageBox.Show("Satuan " & satuan_bahan & " Ke " & satuan_display & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End If
								End Using
							Else
								Dr3.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End Using

						'TODO  asdasdas()

						lvwPackaging = LvPackaging.Items.Add(lks)
						lvwPackaging.SubItems.Add(Kode_bahan)
						''lvwPackaging.SubItems.Add(.Rows(indexBahan).Item("nama_bahan"))
						lvwPackaging.SubItems.Add(Format(jumlahBahan_Total_display, "N4"))
						lvwPackaging.SubItems.Add(satuan_display)
						lvwPackaging.SubItems.Add(Format(jumlahstock_Total_display, "N4"))
						lvwPackaging.SubItems.Add(satuan_display)
						lvwPackaging.SubItems.Add(Format(jumlahBahan_Total, "N4"))
						lvwPackaging.SubItems.Add(satuan_bahan)
						lvwPackaging.SubItems.Add(Format(jumlahstock, "N4"))
						lvwPackaging.SubItems.Add(Format(.Rows(indexBahan).Item("keep_stock"), "N4"))
						If General_Class.CekNULL(.Rows(indexBahan).Item("flag_potong_stok")) = "" Then
							lvwPackaging.SubItems.Add("")
						Else
							lvwPackaging.SubItems.Add(Format(.Rows(indexBahan).Item("flag_potong_stok")))
						End If

						lvwPackaging.SubItems.Add(Jenis_bahan)
						lvwPackaging.SubItems.Add(jumlah)
						lvwPackaging.SubItems.Add(jumlahbahan)

					Next
				End With
			End Using

			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		''akhir mulai baru

		'Dim dataYangDiDelete As Integer = 0
		'For index As Integer = 0 To LvOrder.Items.Count - 1
		'    ' Dim totalJumlahProduksi As Double = 0
		'    Dim totalSerapan As Double = 0
		'    Dim nilai_production_order As Double = 0
		'    Dim nilaiPersentase As Double = 0

		'    'Dim kodeBarang As String = ""
		'    'Dim satuan_awal As String = ""
		'    'Dim id_jenis_produk As String = ""

		'    If LvOrder.Items(index).SubItems(11).Text = "" Then
		'        LvOrder.Items(index).Remove()
		'    End If

		'    If LvOrder.Items.Count <> 0 Then
		'        'For i As Integer = 0 To LvOrder.Items.Count - 1
		'        '    If i = 0 Then
		'        '        kodeBarang = LvOrder.Items(i).SubItems(4).Text
		'        '        satuan_awal = LvOrder.Items(i).SubItems(7).Text
		'        '        txt_IdJenisProduk.Text = LvOrder.Items(i).SubItems(10).Text
		'        '    End If

		'        '    If LvOrder.Items(i).SubItems(12).Text = "" Then
		'        '        totalJumlahProduksi = totalJumlahProduksi + LvOrder.Items(i).SubItems(6).Text
		'        '    End If
		'        'Next

		'        If LvOrder.Items(index).SubItems(12).Text = "Y" Then
		'            LvOrder.Items(index).BackColor = Color.Red
		'        Else
		'            LvOrder.Items(index).BackColor = Color.Transparent
		'        End If

		'        If LvOrder.Items(index).SubItems(12).Text = "" Then
		'            Try
		'                OpenConn()

		'                Dim satuan_akhir_init_barang As String = ""

		'                LvBahan.Items.Clear()
		'                LvPackaging.Items.Clear()

		'                SQL = "select Satuan_Berat From Init "
		'                Using Dr = OpenTrans(SQL)
		'                    If Dr.Read Then
		'                        satuan_akhir_init_barang = Dr("satuan_berat")
		'                    Else
		'                        Dr.Close()
		'                        CloseConn()
		'                        MessageBox.Show("Data tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                        Exit Sub
		'                    End If
		'                End Using

		'                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarang & "',"
		'                SQL = SQL & "'" & satuan_awal & "','" & satuan_akhir_init_barang & "',"
		'                SQL = SQL & "" & totalJumlahProduksi & ") as Hasil "
		'                Using dr = OpenTrans(SQL)
		'                    If dr.Read Then
		'                        If General_Class.CekNULL(dr("Hasil")) <> "" Then
		'                            If dr("Hasil") = 0 Then
		'                                MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                                Exit Sub
		'                            Else
		'                                nilai_production_order = dr("hasil")
		'                            End If
		'                        Else
		'                            dr.Close()
		'                            CloseConn()
		'                            MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                            Exit Sub
		'                        End If
		'                    End If
		'                End Using

		'                ''=========================     'ambil kode formula============================'
		'                Dim kode_formula As String = ""
		'                Dim tanggal_formula As String = ""

		'                SQL = "select kode_formula,tanggal from EMI_Transaksi_Formulator_Binding where  "
		'                SQL = SQL & "Kode_Barang = '" & kodeBarang & "' and Aktif = 'Y'"
		'                Using Dr = OpenTrans(SQL)
		'                    If Dr.Read Then
		'                        If General_Class.CekNULL(Dr("kode_formula")) = "" Then
		'                            Dr.Close()
		'                            CloseConn()
		'                            MessageBox.Show("terjadi kesalahan, kode_formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                            Exit Sub
		'                        Else
		'                            kode_formula = Dr("kode_formula")
		'                            tanggal_formula = Dr("tanggal")
		'                        End If
		'                    Else
		'                        Dr.Close()
		'                        CloseConn()
		'                        MessageBox.Show("Kode formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                        Exit Sub
		'                    End If
		'                End Using
		'                '=========================================================

		'                SQL = "select hasil,satuan_hasil from Emi_Transaksi_Formulator where "
		'                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & kode_formula & "' "
		'                Using Dr = OpenTrans(SQL)
		'                    If Dr.Read Then
		'                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarang & "',"
		'                        SQL = SQL & "'" & Dr("satuan_hasil") & "','" & satuan_akhir_init_barang & "',"
		'                        SQL = SQL & "" & Dr("hasil") & ") as Hasil "
		'                        Dr.Close()

		'                        Using dr2 = OpenTrans(SQL)
		'                            If dr2.Read Then
		'                                If General_Class.CekNULL(dr2("Hasil")) <> "" Then
		'                                    If dr2("Hasil") = 0 Then
		'                                        MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                                        Exit Sub
		'                                    Else
		'                                        totalSerapan = dr2("hasil")
		'                                    End If
		'                                Else
		'                                    dr2.Close()
		'                                    CloseConn()
		'                                    MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                                    Exit Sub
		'                                End If
		'                            End If
		'                        End Using
		'                    Else
		'                        Dr.Close()
		'                        CloseConn()
		'                        MessageBox.Show("Formula tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                        Exit Sub
		'                    End If
		'                End Using

		'                SQL = "select a.no_faktur,a.kode_stock_owner,a.kode_barang,b.nama,a.nilai_barang,a.persentase,a.satuan_barang, b.good_stock as stock"
		'                SQL = SQL & " from EMI_Transaksi_Formulator_Detail_Bahan a, barang b where  "
		'                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner "
		'                SQL = SQL & "and a.kode_barang = b.kode_barang "
		'                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & kode_formula & "' "
		'                Using ds = BindingTrans(SQL)
		'                    With ds.Tables("MyTable")
		'                        If .Rows.Count <> 0 Then
		'                            For indexFormulator As Integer = 0 To .Rows.Count - 1
		'                                Dim lvwFormulator As ListViewItem

		'                                Dim jumlah As Double = 0

		'                                nilaiPersentase = nilai_production_order / totalSerapan

		'                                jumlah = .Rows(indexFormulator).Item("nilai_barang") * nilaiPersentase

		'                                Dim convertKeSatuanAsli As String = ""
		'                                Dim jumlahBarangDibutuhkan As Double = 0

		'                                SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexFormulator).Item("kode_barang") & "' "
		'                                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
		'                                Using Dr3 = OpenTrans(SQL)
		'                                    If Dr3.Read Then
		'                                        convertKeSatuanAsli = Dr3("satuan")
		'                                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexFormulator).Item("kode_barang") & "',"
		'                                        SQL = SQL & "'" & .Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
		'                                        SQL = SQL & "" & jumlah & ") as Hasil "
		'                                        Dr3.Close()

		'                                        Using dr4 = OpenTrans(SQL)
		'                                            If dr4.Read Then
		'                                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
		'                                                    If dr4("Hasil") = 0 Then
		'                                                        MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                                                        Exit Sub
		'                                                    Else
		'                                                        jumlahBarangDibutuhkan = dr4("hasil")

		'                                                    End If
		'                                                Else
		'                                                    dr4.Close()
		'                                                    CloseConn()
		'                                                    MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                                                    Exit Sub
		'                                                End If
		'                                            End If
		'                                        End Using
		'                                    Else
		'                                        Dr3.Close()
		'                                        CloseConn()
		'                                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                                        Exit Sub
		'                                    End If
		'                                End Using

		'                                Dim stockConvert As Double = 0
		'                                Dim converKesatuanAsliBarangStok As String = ""
		'                                '============= convert nilai dan satuan stock barang ke tampilan display
		'                                SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexFormulator).Item("kode_barang") & "' "
		'                                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
		'                                Using Dr3 = OpenTrans(SQL)
		'                                    If Dr3.Read Then
		'                                        converKesatuanAsliBarangStok = Dr3("satuan")
		'                                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexFormulator).Item("kode_barang") & "',"
		'                                        SQL = SQL & "'" & .Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
		'                                        SQL = SQL & "" & .Rows(indexFormulator).Item("stock") & ") as Hasil "
		'                                        Dr3.Close()

		'                                        Using dr4 = OpenTrans(SQL)
		'                                            If dr4.Read Then
		'                                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then

		'                                                    stockConvert = dr4("hasil")

		'                                                Else
		'                                                    dr4.Close()
		'                                                    CloseConn()
		'                                                    MessageBox.Show("Satuan " & satuan_awal & " Ke " & satuan_akhir_init_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                                                    Exit Sub
		'                                                End If
		'                                            End If
		'                                        End Using
		'                                    Else
		'                                        Dr3.Close()
		'                                        CloseConn()
		'                                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                                        Exit Sub
		'                                    End If
		'                                End Using
		'                                lvwFormulator = LvBahan.Items.Add(.Rows(indexFormulator).Item("kode_stock_owner"))
		'                                lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("kode_barang"))
		'                                lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("nama"))
		'                                lvwFormulator.SubItems.Add(Format(jumlahBarangDibutuhkan, "N2"))
		'                                lvwFormulator.SubItems.Add(convertKeSatuanAsli)
		'                                lvwFormulator.SubItems.Add(Format(stockConvert, "N2"))
		'                                lvwFormulator.SubItems.Add(converKesatuanAsliBarangStok)
		'                                lvwFormulator.SubItems.Add(jumlah)
		'                                lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("satuan_barang"))
		'                                lvwFormulator.SubItems.Add(.Rows(indexFormulator).Item("stock"))
		'                                TextBox1.Text = kode_formula
		'                                DateTimePicker3.Value = tanggal_formula

		'                            Next
		'                        Else
		'                            CloseConn()
		'                            MessageBox.Show("Formula tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                            Exit Sub
		'                        End If
		'                    End With
		'                End Using

		'                CloseConn()
		'            Catch ex As Exception
		'                CloseConn()
		'                MessageBox.Show(ex.Message)
		'                Exit Sub
		'            End Try

		'        End If

		'    End If

		'    If LvOrder.Items(index).SubItems(12).Text = "Y" Then
		'        dataYangDiDelete = dataYangDiDelete + 1
		'    End If

		'Next

		If dataYangDiDelete = LvOrder.Items.Count Then
			LvBahan.Items.Clear()
			LvPackaging.Items.Clear()
		End If

	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If CekButtonRole("Release_PO_Produksi") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("anda tidak memiliki akses ! !")
				Exit Sub
			End If

			'=============================
			'=     CEK STATUS FAKTUR     =
			'=============================
			SQL = "select Status from EMI_Order_Produksi where Kode_Perusahaan = '" & KodePerusahaan & "' and status is not null and No_Faktur = '" & txtNoFaktur.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("No Faktur telah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "select status,selesai,flag_release from emi_order_produksi "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and no_faktur = '" & txtNoFaktur.Text & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					If General_Class.CekNULL(dr("status")) = "Y" Then
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show(Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
						'ElseIf General_Class.CekNULL(dr("selesai")) = "" Then
						'    dr.Close()
						'    CloseConn()
						'    MessageBox.Show("Produksi Order belum selesai untuk direlease!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'    Exit Sub
					ElseIf General_Class.CekNULL(dr("flag_release")) = "Y" Then
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Produksi Order sudah pernah direlease!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data Produksi Order tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "update emi_order_produksi set flag_release = 'Y', "
			SQL = SQL & "tanggal_release = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
			SQL = SQL & "jam_release = '" & Format(tgl_skg, "HH:MM:ss") & "',"
			SQL = SQL & "userid_release = '" & UserID & "' "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "'"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show("Data berhasil direlease ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			kosong()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_UnRelease_Click(sender As Object, e As EventArgs) Handles Btn_UnRelease.Click

		Dim pertanyaan As String = MessageBox.Show("Yakin Ingin UnRelease", "Production Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If pertanyaan = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If CekButtonRole("UnRelease_PO_Produksi") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("anda tidak memiliki akses ! !")
				Exit Sub
			End If

			'=============================
			'=     CEK STATUS FAKTUR     =
			'=============================
			SQL = "select Status from EMI_Order_Produksi where Kode_Perusahaan = '" & KodePerusahaan & "' and status is not null and No_Faktur = '" & txtNoFaktur.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("No Faktur telah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'==========================================
			'=     CEK APAKAH PO SUDAH BERJALANAN     =
			'==========================================
			SQL = "select Kode_Perusahaan from Emi_Split_Production_Order where Kode_Perusahaan = '" & KodePerusahaan & "' and No_PO = '" & txtNoFaktur.Text & "' and status is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						CloseTrans()
						CloseConn()
						MessageBox.Show("PO yang Sudah Berjalan tidak bisa Dibatalkan", "Production Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			'====================================
			'=     CEK APAKAH SUDAH RELEASE     =
			'====================================
			SQL = "select Kode_Perusahaan from EMI_Order_Produksi where Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "' and Flag_Release is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						CloseTrans()
						CloseConn()
						MessageBox.Show("PO Belum Di Release", "Production Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			SQL = "Select no_faktur from emi_order_produksi where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "'"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						'=======================
						'=     UPDATE DATA     =
						'=======================
						SQL = "update emi_order_produksi set flag_release = NULL, "
						SQL = SQL & "tanggal_release = NULL, "
						SQL = SQL & "jam_release = NULL "
						SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "'"
						ExecuteTrans(SQL)
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Data Tidak Ditemukan", "Production Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			kosong()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Public Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
		LvData.Items.Clear()
		SQL = "select a.No_Faktur,b.kode_stock_owner,a.Kode_Customer,c.Nama as nama_customer,b.Kode_Stock_Owner,b.Kode_Produk,d.Nama,b.Jumlah_Produksi, "
		SQL = SQL & "b.Jumlah_Produksi-ISNULL((select sum(y.Jumlah)  from emi_order_produksi x,emi_order_produksi_detail y  "
		SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan "
		SQL = SQL & "and x.No_Faktur = y.No_Faktur and y.Kode_Perusahaan = b.Kode_Perusahaan "
		SQL = SQL & "and y.Kode_Stock_Owner = b.Kode_Stock_Owner and y.Kode_Barang = b.Kode_Produk "
		SQL = SQL & "and x.Status is null ),0) as Jumlah_Sisa "
		SQL = SQL & ",b.Jenis_Satuan,f.Id_Jenis_Produk,f.Keterangan as Jenis,a.Tanggal ,b.No_Urut  "

		SQL = SQL & "from emi_po a,Emi_PO_Detail b, Customers c, Barang d, emi_varian e, EMI_Jenis_Produk f where  "
		SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and a.kode_perusahaan = c.Kode_Perusahaan  "
		SQL = SQL & "and a.Kode_Customer = c.Kode_Customer and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Produk = d.Kode_Barang  "
		SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and d.id_varian = e.Id_Varian and d.Kode_Perusahaan = e.Kode_Perusahaan "
		SQL = SQL & "and e.Id_Jenis_Produk = f.Id_Jenis_Produk and e.Kode_Perusahaan = f.Kode_Perusahaan and b.flag_sudah_produksi is null "

		SQL = SQL & "union all "

		SQL = SQL & "select a.No_Faktur,b.kode_stock_owner,'-' as Kode_Customer,'-' as nama_customer,b.Kode_Stock_Owner,b.Kode_Barang "
		SQL = SQL & ",d.Nama,b.Jumlah_Produksi, "
		SQL = SQL & "b.Jumlah_Produksi-ISNULL((select  sum(y.Jumlah)  from emi_order_produksi x,emi_order_produksi_detail y  "
		SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan "
		SQL = SQL & "and x.No_Faktur = y.No_Faktur and y.Kode_Perusahaan = b.Kode_Perusahaan "
		SQL = SQL & "and y.Kode_Stock_Owner = b.Kode_Stock_Owner and y.Kode_Barang = b.Kode_Barang "
		SQL = SQL & "and x.Status is null ),0) as Jumlah_Sisa "
		SQL = SQL & ",b.satuan,f.Id_Jenis_Produk,f.Keterangan as Jenis,a.Tanggal ,b.No_Urut  "
		SQL = SQL & "from EMI_Independent_Order a,EMI_Independent_Order_Detail b, Barang d, emi_varian e, EMI_Jenis_Produk f where  "
		SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur  and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Barang = d.Kode_Barang  "
		SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and d.id_varian = e.Id_Varian and d.Kode_Perusahaan = e.Kode_Perusahaan "
		SQL = SQL & "and e.Id_Jenis_Produk = f.Id_Jenis_Produk and e.Kode_Perusahaan = f.Kode_Perusahaan and b.flag_sudah_produksi is null "

		'SQL = SQL & "select a.No_Faktur,a.lokasi,'-' as Kode_Customer,'-' as nama_customer,b.Kode_Stock_Owner,b.Kode_Barang "
		'SQL = SQL & ",d.Nama,b.Jumlah_Produksi, "
		'SQL = SQL & "ISNULL((select b.Jumlah_Produksi - y.Jumlah  from emi_order_produksi x,emi_order_produksi_detail y  "
		'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and y.Kode_Perusahaan = b.Kode_Perusahaan  "
		'SQL = SQL & "and y.Kode_Stock_Owner = b.Kode_Stock_Owner and y.Kode_Barang = b.Kode_Barang and x.Status is null ),0) as Jumlah_Sisa "
		'SQL = SQL & ",b.satuan,f.Id_Jenis_Produk,f.Keterangan as Jenis,a.Tanggal ,b.No_Urut from EMI_Independent_Order a,EMI_Independent_Order_Detail b, Barang d, emi_varian e, EMI_Jenis_Produk f where   "
		'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur  and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Barang = d.Kode_Barang  "
		'SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and d.id_varian = e.Id_Varian and d.Kode_Perusahaan = e.Kode_Perusahaan "
		'SQL = SQL & "and e.Id_Jenis_Produk = f.Id_Jenis_Produk and e.Kode_Perusahaan = f.Kode_Perusahaan and a.flag_sudah_produksi is null "
		'SQL = SQL & "order by a.No_Faktur,a.Kode_Customer,b.Kode_Produk,a.Tanggal  "

		If ComboBox2.SelectedIndex = -1 Then
			SQL = SQL & "order by a.No_Faktur,a.Kode_Customer,b.Kode_Produk,a.Tanggal  "
		Else
			SQL = SQL & "order by " & arrOrderBy.Item(ComboBox2.SelectedIndex) & ""
		End If

		Using Ds = BindingTrans(SQL)
			With Ds.Tables("MyTable")
				If .Rows.Count <> 0 Then

					For i As Integer = 0 To .Rows.Count - 1
						Dim lvw As ListViewItem
						lvw = LvData.Items.Add(.Rows(i).Item("no_faktur")) '0
						lvw.SubItems.Add(.Rows(i).Item("kode_stock_owner")) '1
						lvw.SubItems.Add(.Rows(i).Item("Kode_Customer")) '2
						lvw.SubItems.Add(.Rows(i).Item("nama_customer")) '3
						lvw.SubItems.Add(.Rows(i).Item("Kode_Produk")) '4
						lvw.SubItems.Add(.Rows(i).Item("nama")) '5
						lvw.SubItems.Add(Format(.Rows(i).Item("Jumlah_Produksi"), "N2")) '6 Barang
						lvw.SubItems.Add(Format(.Rows(i).Item("jumlah_sisa"), "N2")) '7 Barang
						lvw.SubItems.Add(.Rows(i).Item("Jenis_Satuan")) '8
						lvw.SubItems.Add(.Rows(i).Item("jenis")) '9
						lvw.SubItems.Add(.Rows(i).Item("no_urut")) '10
						lvw.SubItems.Add(.Rows(i).Item("id_jenis_produk")) '11

					Next
				End If
			End With
		End Using
	End Sub

	Private Sub cb_seluruh_CheckedChanged(sender As Object, e As EventArgs) Handles cb_seluruh.CheckedChanged
		skipCB = "T"

		If cb_seluruh.Checked = True Then
			For i As Integer = 0 To LvData.Items.Count - 1

				If i = LvData.Items.Count - 1 Then
					skipCB = "Y"
				End If
				LvData.Items(i).Checked = True

			Next
		Else
			skipCB = "Y"
			If LvData.CheckedItems.Count = LvData.Items.Count Then
				For i As Integer = 0 To LvData.Items.Count - 1
					LvData.Items(i).Checked = False
				Next
			End If
		End If
	End Sub

	Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
		EMI_Independent_Order.lokasi_asal = CmbLokasi.Text
		EMI_Independent_Order.asal = "B"
		EMI_Independent_Order.ShowDialog()
	End Sub

	Private Sub BatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalToolStripMenuItem.Click
		If LvData.Items.Count = 0 Then
			MessageBox.Show("Silahkan pilih data terlebih dahulu untuk dihapus")
			Exit Sub
		End If

		Dim selectedindex As Integer = LvData.FocusedItem.Index
		Dim faktur As String = LvData.Items(selectedindex).SubItems(0).Text
		If Not faktur.Substring(0, 2).ToUpper = "IO" Then
			MessageBox.Show("Data Harus dari Independent Order", "Production Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Dim pertanyaan As String = MessageBox.Show("Yakin Ingin Menghapus Data?", "Production Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If pertanyaan = vbNo Then
			Exit Sub
		End If

		Dim noFaktur As String = LvData.Items(selectedindex).SubItems(0).Text
		Dim KDBarang As String = LvData.Items(selectedindex).SubItems(4).Text
		Dim noUrut As String = LvData.Items(selectedindex).SubItems(10).Text

		Try
			OpenConn()

			SQL = "select Kode_Perusahaan from EMI_Independent_Order_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noFaktur & "' and Kode_Barang = '" & KDBarang & "' and No_Urut = '" & noUrut & "'"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						SQL = "update EMI_Independent_Order_Detail set Flag_Sudah_Produksi = 'X' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noFaktur & "' and Kode_Barang = '" & KDBarang & "' and No_Urut = '" & noUrut & "'"
						ExecuteTrans(SQL)
					Else
						CloseConn()
						MessageBox.Show("Data Tidak Ditemukan")
						Exit Sub
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Button1_Click_1(sender, e)

	End Sub

End Class