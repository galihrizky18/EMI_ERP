Public Class EMI_Independent_Order
	Public filter_tambahan, filter_kdSupplier As String
	Public asal As String
	Dim arrcari, arrJenis, arrInisialFaktur As New ArrayList
	Dim arrJenisBarang, arrJenisBarang_FG As New ArrayList
	Dim Jenis = "Tampil_Barang"
	Public lokasi_asal As String

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim fIO = "IO"

	Dim SelectefFormulaTrial As String = ""
	Dim SelectefTransaksiFormulaTrial As String = ""

	Dim Item_Lv_Utama_IDJenisProduk As Integer = 0
	Dim Item_Lv_Utama_JenisProduk As Integer = 1
	Dim Item_Lv_Utama_KdBarang As Integer = 2
	Dim Item_Lv_Utama_NmBarang As Integer = 3
	Dim Item_Lv_Utama_Satuan As Integer = 4
	Dim Item_Lv_Utama_Jumlah As Integer = 5
	Dim Item_Lv_Utama_NoFormula As Integer = 6
	Dim Item_Lv_Utama_NoTransaksi As Integer = 7

	Dim Item_Lv_Formula_NoFormula As Integer = 0
	Dim Item_Lv_Formula_KdBarang As Integer = 1
	Dim Item_Lv_Formula_NmBarang As Integer = 2
	Dim Item_Lv_Formula_NoTranskasi As Integer = 3

	Private Sub get_no_faktur()
		txt_no_faktur.Text = fIO & arrInisialFaktur.Item(cmb_Lokasi_Init_Faktur.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
									 General_Class.Get_Last_Number2("emi_independent_order", "no_faktur", Jumlah_Digit,
									 "Kode_perusahaan", KodePerusahaan,
									 "And", "substring(no_faktur,1," & Len(fIO) + Len(arrInisialFaktur.Item(cmb_Lokasi_Init_Faktur.SelectedIndex)) + 6 & ")", fIO & arrInisialFaktur.Item(cmb_Lokasi_Init_Faktur.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))
	End Sub

	Private Sub SD_Pilih_Barang_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub SD_Pilih_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		EnableDoubleBuffer(ListView1)
		EnableDoubleBuffer(LvPilihBarang_DataBarang)
		EnableDoubleBuffer(Lv_Formula_Trial)

		Try
			OpenConn()

			Base_Language.Get_Languages_Global(Bahasa_Pilihan)
			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			LblPilihBarang_Judul.Text = "Transaksi - Independent Order"
			LblPilihBarang_Lokasi.Text = Base_Language.Lang_Global_Lokasi
			LblPilihBarang_KodeBarang.Text = Base_Language.Lang_Global_KodeBarang

			LvPilihBarang_DataBarang.Columns.Add(Base_Language.Lang_Global_Lokasi, 0)
			LvPilihBarang_DataBarang.Columns.Add(Base_Language.Lang_Global_KodeBarang, 110, HorizontalAlignment.Left)
			LvPilihBarang_DataBarang.Columns.Add(Base_Language.Lang_Global_Nama, 220, HorizontalAlignment.Left)
			LvPilihBarang_DataBarang.Columns.Add(Base_Language.Lang_Global_Satuan, 90, HorizontalAlignment.Left)
			LvPilihBarang_DataBarang.View = Windows.Forms.View.Details

			ListView1.Columns.Clear()
			ListView1.Columns.Add("id_jenis_produk", 0) '0
			ListView1.Columns.Add(Base_Language.Lang_Global_Jenis_Produk, 90, HorizontalAlignment.Center) '1
			ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left) '2
			ListView1.Columns.Add(Base_Language.Lang_Global_Nama, 300, HorizontalAlignment.Left) '3
			ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 80, HorizontalAlignment.Center) '4
			ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 130, HorizontalAlignment.Right) '5
			ListView1.Columns.Add("NoFormula", 0, HorizontalAlignment.Right) '6
			ListView1.Columns.Add("NoTransaksi", 0, HorizontalAlignment.Right) '7

			Lv_Formula_Trial.Columns.Clear()
			Lv_Formula_Trial.Columns.Add("No Formula", 110, HorizontalAlignment.Left) '0
			Lv_Formula_Trial.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
			Lv_Formula_Trial.Columns.Add("Nama Barang", 150, HorizontalAlignment.Left) '2
			Lv_Formula_Trial.Columns.Add("No Transaksi", 0, HorizontalAlignment.Left) '3
			Lv_Formula_Trial.View = View.Details

			'
			BtnPilihBarang_Simpan.Text = Base_Language.Lang_Global_Simpan
			BtnPilihBarang_Refresh.Text = Base_Language.Lang_Global_Refresh

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try
		kosong()
		LvPilihBarang_DataBarang.Visible = False
		LvPilihBarang_DataBarang.Location = New Point(119, 113)

	End Sub

	Private Sub kosong()
		lokasi_asal = Lokasi
		Try
			OpenConn()
			TextBox2.Focus()

			cmb_Lokasi_Init_Faktur.Items.Clear()

			arrInisialFaktur.Clear() : cmb_Lokasi_Init_Faktur.Items.Clear()
			SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & lokasi_asal & "' order by Kode_Stock_Owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					cmb_Lokasi_Init_Faktur.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
				Loop
			End Using

			cmb_Lokasi_Init_Faktur.Text = Lokasi

			'CmbJenis.Items.Clear() : arrJenis.Clear()
			'SQL = "select Id_Group_Jenis, Kode_Group_Jenis, isnull(Flag_Finished_Good,'T') as Flag_Finished_Good from emi_group_jenis "
			'SQL = SQL & "where flag_produksi='Y' and kode_Perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        CmbJenis.Items.Add(dr("Kode_Group_Jenis")) : arrJenisBarang.Add(dr("Id_Group_Jenis")) : arrJenisBarang_FG.Add(dr("Flag_Finished_Good"))
			'    Loop
			'End Using

			'CmbJenisProduk.Items.Clear() : arrJenis.Clear()
			'SQL = "select Id_Jenis_Produk,Keterangan from EMI_Jenis_Produk "
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        CmbJenisProduk.Items.Add(dr("keterangan")) : arrJenis.Add(dr("id_jenis_produk"))
			'    Loop
			'End Using

			get_no_faktur()

			'kosong()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try

		ListView1.Items.Clear()
		TxtPilihBarang_KodeBarang.Text = ""
		''TxtPilihBarang_NamaBarang.Text = ""
		TxtPilihBarang_Satuan.Text = ""
		TextBox1.Text = ""
		TextBox2.Text = ""
		TxtNamaBarang.Text = ""
		txtLokasi_Gudang.Text = ""
		TxtKso.Text = ""
		TxtJenisProduk.Text = ""
		txtIdJenisProduk.Text = ""
		'CmbJenisProduk.Focus()
		'CmbJenisProduk.Enabled = False

		SelectefFormulaTrial = ""
		SelectefTransaksiFormulaTrial = ""
		LoadDataFormulaTrial()

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
		kosong()
	End Sub

	Private Sub LoadDataFormulaTrial()
		Try
			OpenConn()

			Lv_Formula_Trial.Items.Clear()
			SQL = "select No_Transaksi, No_Faktur_Formula, Kode_Barang, Nama_Barang "
			SQL = SQL & "from N_EMI_Transaksi_PO_Trial_Produksi "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and Status is NULL "
			SQL = SQL & "and Flag_Mulai_Trial_Produksi is null "
			SQL = SQL & "order by Tanggal, Jam "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Formula_Trial.Items.Add(Dr("No_Faktur_Formula"))
					Lv.SubItems.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama_Barang"))
					Lv.SubItems.Add(Dr("No_Transaksi"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then BtnPilihBarang_Simpan.Focus()
	End Sub

	Private Sub TxtKode_TextChanged(sender As Object, e As EventArgs) Handles TxtPilihBarang_KodeBarang.TextChanged
		If TxtPilihBarang_KodeBarang.Text.Length >= 3 Then

			'If CmbJenis.SelectedIndex = -1 Then
			'    MessageBox.Show("pilih dahulu jenis", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TxtPilihBarang_KodeBarang.Text = ""
			'    CmbJenis.Focus()
			'    Exit Sub
			'Else
			'    If arrJenisBarang_FG.Item(CmbJenis.SelectedIndex) = "Y" Then
			'        If CmbJenisProduk.SelectedIndex = -1 Then
			'            MessageBox.Show("pilih dahulu jenis produk", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'            TxtPilihBarang_KodeBarang.Text = ""
			'            CmbJenisProduk.Focus()
			'            Exit Sub
			'        End If
			'    End If
			'End If

			If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then
				LvPilihBarang_DataBarang.Visible = False : Exit Sub
			Else
				LvPilihBarang_DataBarang.Visible = True
			End If

			If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then
				LvPilihBarang_DataBarang.Visible = False : Exit Sub
			Else
				LvPilihBarang_DataBarang.Visible = True
			End If

			TxtPilihBarang_Satuan.Text = ""
			txtLokasi_Gudang.Text = ""
			TextBox1.Text = ""

			Dim var As String = ""

			Try
				OpenConn()

				LvPilihBarang_DataBarang.Items.Clear()
				Dim Lvw As ListViewItem

				SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang  "
				SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c  "
				SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
				SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "

				SQL = SQL & "and Kode_Barang like '%" & TxtPilihBarang_KodeBarang.Text & "%' and aktif = 'Y'  " & filter_tambahan & " "
				SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang "

				Using dr = OpenTrans(SQL)
					Do While dr.Read
						Lvw = LvPilihBarang_DataBarang.Items.Add(dr("lokasi_gudang"))
						Lvw.SubItems.Add(dr("kode_barang"))
						Lvw.SubItems.Add(dr("Nama"))
						Lvw.SubItems.Add(dr("satuan"))
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		Else
			LvPilihBarang_DataBarang.Visible = False
		End If
	End Sub

	Private Sub TxtKode_Leave(sender As Object, e As EventArgs) Handles TxtPilihBarang_KodeBarang.Leave

		If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then Exit Sub
		If LvPilihBarang_DataBarang.Focused = True Then Exit Sub

		Dim var As String = ""

		Try
			OpenConn()

			SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang, b.Kode_Group_Jenis, e.Keterangan,e.Id_Jenis_Produk  "
			SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c, EMI_Varian d, EMI_Jenis_Produk e  "
			SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
			SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
			SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Id_Varian = d.Id_Varian and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Jenis_Produk = e.Id_Jenis_Produk "
			SQL = SQL & "and (b.flag_semi_fg='Y' or b.flag_finished_good = 'Y' ) "

			SQL = SQL & "and a.kode_barang like '%" & TxtPilihBarang_KodeBarang.Text & "%' and aktif = 'Y'  " & filter_tambahan & " "
			SQL = SQL & "group by a.Kode_Barang,b.Kode_Group_Jenis,a.Nama, a.Satuan,c.lokasi_gudang ,e.Keterangan, e.Id_Jenis_Produk"

			Using dr = OpenTrans(SQL)
				If dr.Read Then
					txtLokasi_Gudang.Text = dr("lokasi_gudang")
					TxtPilihBarang_KodeBarang.Text = dr("kode_barang")
					TxtNamaBarang.Text = dr("nama")
					TxtPilihBarang_Satuan.Text = dr("Satuan")
					txtIdJenisProduk.Text = dr("id_jenis_produk")
					TxtKso.Text = dr("Kode_Group_Jenis")
					TxtJenisProduk.Text = dr("keterangan")
					dr.Close()
				Else
					TxtPilihBarang_KodeBarang.Text = ""
					TxtNamaBarang.Text = ""
					'TxtPilihBarang_NamaBarang.Text = ""
					TxtPilihBarang_Satuan.Text = ""
					TxtPilihBarang_KodeBarang.Focus()
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub TxtKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPilihBarang_KodeBarang.KeyPress
		' If e.KeyChar = Chr(13) Then CmbPilihBarang_Satuan.Focus()

	End Sub

	Private Sub TxtKode_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPilihBarang_KodeBarang.KeyDown
		If e.KeyCode = Keys.Down Then
			If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub
			LvPilihBarang_DataBarang.Focus()
		End If
	End Sub

	Private Sub Lv2_DoubleClick(sender As Object, e As EventArgs) Handles LvPilihBarang_DataBarang.DoubleClick
		If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub

		TxtPilihBarang_KodeBarang.Text = LvPilihBarang_DataBarang.FocusedItem.SubItems(1).Text
		TxtPilihBarang_KodeBarang.Focus() : TextBox1.Focus()
		LvPilihBarang_DataBarang.Visible = False
	End Sub

	Private Sub Lv2_KeyDown(sender As Object, e As KeyEventArgs) Handles LvPilihBarang_DataBarang.KeyDown
		If e.KeyCode = Keys.Enter Then Lv2_DoubleClick(LvPilihBarang_DataBarang, e)
	End Sub

	Private Sub Btn_Refresh_Click_1(sender As Object, e As EventArgs) Handles BtnPilihBarang_Refresh.Click
		kosong()
	End Sub

	Private Sub Btn_Simpan_Click_1(sender As Object, e As EventArgs) Handles BtnPilihBarang_Simpan.Click
		If ListView1.Items.Count = 0 Then
			MessageBox.Show("Barang tidak boleh kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			get_no_faktur()
			SQL = "insert into emi_independent_order(kode_perusahaan,no_faktur,lokasi,tanggal,jam,userid,keterangan) values ("
			SQL = SQL & "'" & KodePerusahaan & "', '" & txt_no_faktur.Text & "', '" & lokasi_asal & "' , '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
			SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "',"
			SQL = SQL & "'" & UserID & "', '" & TextBox2.Text.Trim & "' )"
			ExecuteTrans(SQL)

			Dim lokasi_barang As String

			SQL = " select  b.Kode_Stock_Owner_Gudang from stock_owner a , Binding_Lokasi_Gudang b where "
			SQL = SQL & "  a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.Kode_Stock_Owner "
			SQL = SQL & "  and a.Kode_Stock_Owner = '" & lokasi_asal & "' and b.Gudang_Default = 'Y'"
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					lokasi_barang = dr("kode_stock_owner_gudang")
				Else
					dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show(Base_Language.Lang_Global_Error_Lokasi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			For i As Integer = 0 To ListView1.Items.Count - 1
				Dim jenis_produk As String = "NULL"

				Dim ValueLVFormula As String = ListView1.Items(i).SubItems(Item_Lv_Utama_NoFormula).Text.Trim
				Dim ValueLVTransaksiFormula As String = ListView1.Items(i).SubItems(Item_Lv_Utama_NoTransaksi).Text.Trim
				Dim NoFormula As String = If(String.IsNullOrWhiteSpace(ValueLVFormula), "NULL", $"'{ValueLVFormula}'")
				Dim NoTransaksiFormula As String = If(String.IsNullOrWhiteSpace(ValueLVTransaksiFormula), "NULL", $"'{ValueLVTransaksiFormula}'")
				Dim FlagFormula As String = If(String.IsNullOrWhiteSpace(ValueLVFormula), "NULL", $"'Y'")

				If ListView1.Items(i).SubItems(0).Text.Trim <> "" Then
					jenis_produk = "'" & ListView1.Items(i).SubItems(0).Text.Trim & "'"
				End If
				SQL = "insert into EMI_Independent_Order_Detail(kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,jumlah_produksi,satuan,id_jenis_produk, Flag_Trial_Produksi, No_Formula) values ( "
				SQL = SQL & "'" & KodePerusahaan & "', '" & txt_no_faktur.Text & "', '" & lokasi_barang & "' ,'" & ListView1.Items(i).SubItems(2).Text & "',  "
				SQL = SQL & "'" & HilangkanTanda(ListView1.Items(i).SubItems(5).Text) & "', '" & ListView1.Items(i).SubItems(4).Text & "'," & jenis_produk & ", " & FlagFormula & ", " & NoFormula & ") "
				ExecuteTrans(SQL)

				'=======================
				'=     UPDATE FLAG     =
				'=======================
				If FlagFormula = "'Y'" Then
					SQL = "select top 1 1 "
					SQL = SQL & "from N_EMI_Transaksi_PO_Trial_Produksi "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and Status is NULL "
					SQL = SQL & "and No_Transaksi = " & NoTransaksiFormula & " "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then

							Dr.Close()
							SQL = "update N_EMI_Transaksi_PO_Trial_Produksi set Flag_Mulai_Trial_Produksi = 'Y' "
							SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and Status is NULL "
							SQL = SQL & "and No_Transaksi = " & NoTransaksiFormula & " "
							ExecuteTrans(SQL)
						Else
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show($"No Transaksi Formula {ListView1.Items(i).SubItems(Item_Lv_Utama_NoTransaksi).Text.Trim} Tidak DItemulan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End Using

				End If

			Next

			'ListView1.Columns.Add("id_jenis_produk", 0)
			'ListView1.Columns.Add(Base_Language.Lang_Global_Jenis_Produk, 150)
			'ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150)
			'ListView1.Columns.Add(Base_Language.Lang_Global_Nama, 250)
			'ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 100)
			'ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 100)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			kosong()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If asal = "B" Then
			Me.Close()
			EMI_Production_Order.Button1_Click_1(BtnPilihBarang_Simpan, e)
		End If

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If TextBox1.Text.Trim.Length = 0 Then
			MessageBox.Show("Jumlah Harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox1.Focus()
			Exit Sub
		End If

		For i As Integer = 0 To ListView1.Items.Count - 1

			If ListView1.Items(i).SubItems(2).Text = TxtPilihBarang_KodeBarang.Text Then
				MessageBox.Show("Data sudah ada ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

		Next

		Dim data As String = ""
		'If CmbJenisProduk.SelectedIndex <> -1 Then
		'    data = arrJenis.Item(CmbJenisProduk.SelectedIndex)
		'End If

		Dim lvw As ListViewItem
		lvw = ListView1.Items.Add(txtIdJenisProduk.Text)
		lvw.SubItems.Add(TxtJenisProduk.Text)
		lvw.SubItems.Add(TxtPilihBarang_KodeBarang.Text)
		lvw.SubItems.Add(TxtNamaBarang.Text)
		lvw.SubItems.Add(TxtPilihBarang_Satuan.Text)
		lvw.SubItems.Add(Format(Val(TextBox1.Text), "N2"))
		If SelectefFormulaTrial.Trim.Length = 0 Then
			lvw.SubItems.Add("")
			lvw.SubItems.Add("")
		Else
			lvw.SubItems.Add(SelectefFormulaTrial.Trim)
			lvw.SubItems.Add(SelectefTransaksiFormulaTrial.Trim)

		End If

		TxtPilihBarang_KodeBarang.Text = ""
		TxtNamaBarang.Text = ""
		TxtPilihBarang_Satuan.Text = ""
		txtLokasi_Gudang.Text = ""
		TextBox1.Text = ""
		txtIdJenisProduk.Text = ""
		TxtJenisProduk.Text = ""
		TxtKso.Text = ""

		SelectefFormulaTrial = ""
		SelectefTransaksiFormulaTrial = ""

	End Sub

	Private Sub LvPilihBarang_DataBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvPilihBarang_DataBarang.SelectedIndexChanged

	End Sub

	Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
		'If ListView1.FocusedItem.Index <> ListView1.Items.Count - 1 Then
		'    MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    Exit Sub
		'End If

		If ListView1.Items.Count = -1 Then
			MessageBox.Show(Base_Language.Lang_Global_Pilih_Hapus, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		For i As Integer = 0 To ListView1.Items.Count - 1
			If ListView1.Items(i).SubItems(1).Text = TxtPilihBarang_KodeBarang.Text Then
				MessageBox.Show("Barang sudah ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If
		Next

		ListView1.FocusedItem.Remove()

	End Sub

	Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
		TxtPilihBarang_KodeBarang.Focus()
	End Sub

	Private Sub TxtNamaBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtNamaBarang.KeyDown
		If e.KeyCode = Keys.Down Then
			If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub
			LvPilihBarang_DataBarang.Focus()
		End If
	End Sub

	Private Sub TxtNamaBarang_Leave(sender As Object, e As EventArgs) Handles TxtNamaBarang.Leave
		If LvPilihBarang_DataBarang.Focused = True Then Exit Sub
		TxtNamaBarang.Text = "" : TxtPilihBarang_KodeBarang.Text = ""

		'If TxtNamaBarang.Text.Trim.Length = 0 Then Exit Sub

		'If LvPilihBarang_DataBarang.Focused = True Then Exit Sub

		'Dim var As String = ""

		'Try
		'    OpenConn()

		'    SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang, b.Kode_Group_Jenis, e.Keterangan,e.Id_Jenis_Produk  "
		'    SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c, EMI_Varian d, EMI_Jenis_Produk e  "
		'    SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
		'    SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
		'    SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Id_Varian = d.Id_Varian and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Jenis_Produk = e.Id_Jenis_Produk "
		'    SQL = SQL & "and (b.flag_semi_fg='Y' or b.flag_finished_good = 'Y' ) "

		'    SQL = SQL & "and a.nama like '%" & TxtNamaBarang.Text & "%' and aktif = 'Y'  " & filter_tambahan & " "
		'    SQL = SQL & "group by a.Kode_Barang,b.Kode_Group_Jenis,a.Nama, a.Satuan,c.lokasi_gudang ,e.Keterangan, e.Id_Jenis_Produk"

		'    Using dr = OpenTrans(SQL)
		'        If dr.Read Then
		'            txtLokasi_Gudang.Text = dr("lokasi_gudang")
		'            TxtPilihBarang_KodeBarang.Text = dr("kode_barang")
		'            TxtNamaBarang.Text = dr("nama")
		'            TxtPilihBarang_Satuan.Text = dr("Satuan")
		'            txtIdJenisProduk.Text = dr("id_jenis_produk")
		'            TxtKso.Text = dr("Kode_Group_Jenis")
		'            TxtJenisProduk.Text = dr("keterangan")
		'            dr.Close()
		'        Else
		'            TxtPilihBarang_KodeBarang.Text = ""
		'            TxtNamaBarang.Text = ""
		'            'TxtPilihBarang_NamaBarang.Text = ""
		'            TxtPilihBarang_Satuan.Text = ""
		'            TxtPilihBarang_KodeBarang.Focus()
		'        End If
		'    End Using

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try
	End Sub

	Private Sub TextBox1_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
		If e.KeyChar = Chr(13) Then
			Button1_Click(Me, Nothing)
		End If
	End Sub

	Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TxtNamaBarang.TextChanged
		If TxtNamaBarang.Text.Length >= 3 Then

			'If CmbJenis.SelectedIndex = -1 Then
			'    MessageBox.Show("pilih dahulu jenis", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TxtPilihBarang_KodeBarang.Text = ""
			'    CmbJenis.Focus()
			'    Exit Sub
			'Else
			'    If arrJenisBarang_FG.Item(CmbJenis.SelectedIndex) = "Y" Then
			'        If CmbJenisProduk.SelectedIndex = -1 Then
			'            MessageBox.Show("pilih dahulu jenis produk", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'            TxtPilihBarang_KodeBarang.Text = ""
			'            CmbJenisProduk.Focus()
			'            Exit Sub
			'        End If
			'    End If
			'End If

			If TxtNamaBarang.Text.Trim.Length = 0 Then
				LvPilihBarang_DataBarang.Visible = False : Exit Sub
			Else
				LvPilihBarang_DataBarang.Visible = True
			End If

			If TxtNamaBarang.Text.Trim.Length = 0 Then
				LvPilihBarang_DataBarang.Visible = False : Exit Sub
			Else
				LvPilihBarang_DataBarang.Visible = True
			End If

			TxtPilihBarang_Satuan.Text = ""
			txtLokasi_Gudang.Text = ""
			TextBox1.Text = ""

			Dim var As String = ""

			Try
				OpenConn()

				LvPilihBarang_DataBarang.Items.Clear()
				Dim Lvw As ListViewItem

				SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang  "
				SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c  "
				SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
				SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "

				SQL = SQL & "and nama like '%" & TxtNamaBarang.Text & "%' and aktif = 'Y'  " & filter_tambahan & " "
				SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang "

				Using dr = OpenTrans(SQL)
					Do While dr.Read
						Lvw = LvPilihBarang_DataBarang.Items.Add(dr("lokasi_gudang"))
						Lvw.SubItems.Add(dr("kode_barang"))
						Lvw.SubItems.Add(dr("Nama"))
						Lvw.SubItems.Add(dr("satuan"))
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		Else
			LvPilihBarang_DataBarang.Visible = False
		End If
	End Sub

	Private Sub CmbPilihBarang_Satuan_KeyPress(sender As Object, e As KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then BtnPilihBarang_Simpan.Focus()
	End Sub

	Private Sub Lv_Formula_Trial_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Formula_Trial.DoubleClick
		If Lv_Formula_Trial.Items.Count = 0 Or Lv_Formula_Trial.FocusedItem Is Nothing Then Exit Sub

		SelectefFormulaTrial = Lv_Formula_Trial.FocusedItem.SubItems(Item_Lv_Formula_NoFormula).Text.Trim
		SelectefTransaksiFormulaTrial = Lv_Formula_Trial.FocusedItem.SubItems(Item_Lv_Formula_NoTranskasi).Text.Trim
		TxtPilihBarang_KodeBarang.Text = Lv_Formula_Trial.FocusedItem.SubItems(Item_Lv_Formula_KdBarang).Text.Trim
		TxtKode_Leave(sender, e)
		LvPilihBarang_DataBarang.Visible = False
		TextBox1.Focus()

	End Sub

	'===============================================================================================================================================================
	'=     HELPER
	'===============================================================================================================================================================

	Private Sub ListView1_MouseMove(sender As Object, e As MouseEventArgs) Handles ListView1.MouseMove, LvPilihBarang_DataBarang.MouseMove, Lv_Formula_Trial.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	Private Sub EnableDoubleBuffer(lvw As ListView)
		Dim t As Type = lvw.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(lvw, True, Nothing)
	End Sub

	Private Sub HandleListViewHover(lvw As ListView, e As MouseEventArgs)
		Dim hit As ListViewHitTestInfo = lvw.HitTest(e.Location)

		' 1. Set Cursor
		lvw.Cursor = If(hit.Item IsNot Nothing, Cursors.Hand, Cursors.Default)

		' 2. Cek jika pindah item atau keluar area
		If hit.Item IsNot lastHoverItem Then
			lvw.BeginUpdate()

			' Kembalikan warna item sebelumnya ke warna asli
			If lastHoverItem IsNot Nothing Then
				lastHoverItem.BackColor = originalItemColor
			End If

			' Jika hover di item baru yang valid (dan tidak memiliki Tag/Skip)
			If hit.Item IsNot Nothing AndAlso hit.Item.Tag Is Nothing Then
				lastHoverItem = hit.Item
				originalItemColor = lastHoverItem.BackColor ' Simpan warna asli (misal: Hijau)

				' Gelapkan sedikit secara halus (minus 10 poin RGB)
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