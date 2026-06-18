Imports System.IO

Public Class EMI_Display_Production_Result

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList
	Dim pertama As Integer = 1
	Dim T As Color = Color.Blue
	Dim KT As Color = Color.Red
	Dim KY As Color = Color.Green
	Dim Batal As Color = Color.Black

	Private random As New Random()
	Private imageBytes1 As Byte = Nothing
	Private FileSize1 As UInt32
	Private rawData1() As Byte
	Private fs1 As FileStream

	Dim itemPR_NoFak As Integer = 0
	Dim itemPR_NoPO As Integer = 1
	Dim itemPR_Tanggal As Integer = 2
	Dim itemPR_Jam As Integer = 3
	Dim itemPR_UserID As Integer = 4
	Dim itemPR_KdBarang As Integer = 5
	Dim itemPR_NmBarang As Integer = 6
	Dim itemPR_JumlahProduksi As Integer = 7
	Dim itemPR_Satuan As Integer = 8
	Dim itemPR_Catatan As Integer = 9
	Dim itemPR_TanggalSelesaiProduksi As Integer = 10
	Dim itemPR_JamSelesaiProduksi As Integer = 11
	Dim itemPR_FlagSelesai As Integer = 12

	Dim itemFG_NoFak As Integer = 0
	Dim itemFG_NoPO As Integer = 1
	Dim itemFG_FullQR As Integer = 2
	Dim itemFG_Jumlah As Integer = 3
	Dim itemFG_Satuan As Integer = 4
	Dim itemFG_TglExp As Integer = 5
	Dim itemFG_Kualitas As Integer = 6

	Private Sub Display_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBuffer(Lv_ProductionResult)
		EnableDoubleBuffer(Lv_DetailFinishedGood)
		EnableDoubleBuffer(Lv_DetailRawMaterial)
		EnableDoubleBuffer(Lv_DetailPackaging)
		EnableDoubleBuffer(Lv_DetailScrap)

		kosong()
	End Sub

	Private Sub kosong()

		Try
			OpenConn()
			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, "Display_Barang_Masuk")
			Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Lv_ProductionResult.Items.Clear()
		Lv_ProductionResult.Columns.Add("No Split", 125, HorizontalAlignment.Left)
		Lv_ProductionResult.Columns.Add("No PO", 135, HorizontalAlignment.Left)
		Lv_ProductionResult.Columns.Add(Base_Language.Lang_Global_Tanggal, 150, HorizontalAlignment.Center)
		Lv_ProductionResult.Columns.Add(Base_Language.Lang_Global_Jam, 100, HorizontalAlignment.Center)
		Lv_ProductionResult.Columns.Add("User ID", 100, HorizontalAlignment.Left)
		Lv_ProductionResult.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
		Lv_ProductionResult.Columns.Add("Nama", 250, HorizontalAlignment.Left)
		Lv_ProductionResult.Columns.Add("Jumlah Produksi", 130, HorizontalAlignment.Right)
		Lv_ProductionResult.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_ProductionResult.Columns.Add("Catatan", 0, HorizontalAlignment.Left)
		Lv_ProductionResult.Columns.Add("Tanggal Selesai Produksi", 150, HorizontalAlignment.Center) 'NULLable
		Lv_ProductionResult.Columns.Add("Jam Selesai Produksi", 100, HorizontalAlignment.Center) 'NULLable
		'Hide
		Lv_ProductionResult.Columns.Add("Flag Selesai Produksi", 0, HorizontalAlignment.Center) 'NULLable
		Lv_ProductionResult.View = View.Details

		Lv_DetailFinishedGood.Items.Clear()
		Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
		Lv_DetailFinishedGood.Columns.Add("No_Production_Order", 0, HorizontalAlignment.Left)
		Lv_DetailFinishedGood.Columns.Add("QR Code", 385, HorizontalAlignment.Left)
		Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_Jumlah, 150, HorizontalAlignment.Right)
		Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
		Lv_DetailFinishedGood.Columns.Add("Tanggal Expired", 150, HorizontalAlignment.Center)
		Lv_DetailFinishedGood.Columns.Add("Kualitas", 200, HorizontalAlignment.Center)
		Lv_DetailFinishedGood.View = View.Details

		Lv_DetailRawMaterial.Items.Clear()
		Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
		Lv_DetailRawMaterial.Columns.Add("Kode Stock Owner", 0, HorizontalAlignment.Left)
		Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
		Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_NamaBarang, 400, HorizontalAlignment.Left)
		Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_Nilai_Produksi, 180, HorizontalAlignment.Right)
		Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
		Lv_DetailRawMaterial.View = View.Details

		Lv_DetailPackaging.Items.Clear()
		Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
		Lv_DetailPackaging.Columns.Add("Kode Stock Owner", 0, HorizontalAlignment.Left)
		Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_KodeBarang, 220, HorizontalAlignment.Left)
		Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_NamaBarang, 450, HorizontalAlignment.Left)
		Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_Nilai_Produksi, 200, HorizontalAlignment.Right)
		Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
		Lv_DetailPackaging.View = View.Details

		Lv_DetailScrap.Items.Clear()
		Lv_DetailScrap.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
		Lv_DetailScrap.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
		Lv_DetailScrap.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
		Lv_DetailScrap.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
		Lv_DetailScrap.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_DetailScrap.Columns.Add("Proses", 0, HorizontalAlignment.Center)
		'Hide
		Lv_DetailScrap.Columns.Add("Barcode", 300, HorizontalAlignment.Left)
		Lv_DetailScrap.View = View.Details

		Lv_DetailScrap.Columns(6).DisplayIndex = 0

		Try
			OpenConn()

			Cmb_Lokasi.Items.Clear()
			Cmb_Lokasi.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)

			'xSplit = CekKotaRole().Split(", ")

			SQL = "Select kode_stock_owner From "
			SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and kode_kota in( "
			'For i As Integer = 0 To xSplit.Count - 1
			'    SQL = SQL & "'" & xSplit(i).Trim & "', "
			'Next
			'SQL = Strings.Left(SQL, Len(SQL) - 2)

			'SQL = SQL & ") "
			SQL = SQL & "order by kode_stock_owner"
			'ComboBox1.Items.Add("Seluruh")
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
				Loop
			End Using

			Cmb_Lokasi.Text = Lokasi

			'If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
			'    ComboBox6.Enabled = False
			'Else
			'    ComboBox6.Enabled = True
			'End If

			'ComboBox3.Items.Add("Y") : Arr4.Add("Y")
			'ComboBox3.Items.Add("T") : Arr4.Add("T")
			'ComboBox3.SelectedIndex = 1

			Cmb_ParamTgl.Items.Clear() : Arr1.Clear()
			Cmb_ParamTgl.Items.Add("Tanggal") : Arr1.Add("a.Tanggal")
			Cmb_ParamTgl.Items.Add("Tanggal Selesai") : Arr1.Add("a.Tgl_Hasil_Produksi")

			'TextBoxa.Text = "0"
			Cmb_ParamTgl.Enabled = False : Cmb_ParamLain.Enabled = False
			DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
			Txt_ParamValue.Enabled = False

			Cmb_ParamLain.Items.Clear() : Cmb_ParamLain.Text = "" : Arr2.Clear()
			Cmb_ParamLain.Items.Add(Base_Language.Lang_Global_No_Transaksi) : Arr2.Add("a.no_transaksi")
			Cmb_ParamLain.Items.Add("No Production Order") : Arr2.Add("a.No_PO")
			Cmb_ParamLain.Items.Add("User ID") : Arr2.Add("a.UserID")
			Cmb_ParamLain.Items.Add("Kode Barang") : Arr2.Add("a.Kode_Barang")
			Cmb_ParamLain.Items.Add("Nama Barang") : Arr2.Add("b.Nama")
			Cmb_ParamLain.Items.Add("Satuan") : Arr2.Add("a.satuan")

			Label1.Text = "Display - Production Result"
			Cb_TransaksiHrIni.Text = Base_Language.Lang_Global_Hari_ini
			Cb_ParamTgl.Text = Base_Language.Lang_Global_Para_Tbl
			Cb_ParamLain.Text = Base_Language.Lang_Global_Para_lain
			Btn_Cari.Text = Base_Language.Lang_Global_Cari
			CloseConn()
		Catch ex As Exception
			Cmb_Lokasi.Items.Clear()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_TransaksiHrIni.CheckedChanged
		If Cb_TransaksiHrIni.Checked = True Then
			Cb_ParamTgl.Checked = False
			BtnBarangMasuk_Cari_Click(Cb_TransaksiHrIni, e)
		End If
	End Sub

	Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

		If Cb_ParamTgl.Checked = False And Cb_ParamLain.Checked = False And Cb_TransaksiHrIni.Checked = False Then
			MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Judul)
			Cb_ParamTgl.Focus() : Exit Sub
		End If

		If Cb_ParamTgl.Checked Then
			If Cmb_ParamTgl.SelectedIndex = -1 Then
				MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Judul)
				Cmb_ParamTgl.Focus() : Exit Sub
			ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
				MessageBox.Show("Periode I " & Base_Language.Lang_Global_TidakBolehLebihDari & " periode II!", Judul)
				DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
				Exit Sub
			End If
		End If

		If Cb_ParamLain.Checked Then
			If Cmb_ParamLain.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Lain Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_ParamLain.DroppedDown = True : Cmb_ParamLain.Focus() : Exit Sub
			ElseIf Txt_ParamValue.Text.Trim.Length = 0 Then
				MessageBox.Show("Value Filter Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_ParamValue.Focus() : Exit Sub
			End If
		End If

		pertama = 1

		Try

			OpenConn()

			Lv_ProductionResult.Items.Clear()
			Lv_DetailFinishedGood.Items.Clear()
			Lv_DetailRawMaterial.Items.Clear()
			Lv_DetailPackaging.Items.Clear()
			Lv_DetailScrap.Items.Clear()

			SQL = "select a.No_Transaksi, a.No_PO, a.Tanggal, a.Jam, a.UserID, a.Kode_Barang, b.Nama, a.Jumlah, a.satuan, a.Catatan,  a.Flag_Hasil_Produksi, a.Tgl_Hasil_Produksi, a.Jam_Hasil_Produksi "
			SQL = SQL & "from Emi_Split_Production_Order a, barang b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan  "
			SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
			SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.status is null "

			If Cb_TransaksiHrIni.Checked Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & " a.tanggal between '"
				SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
			End If

			If Cb_ParamTgl.Checked Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "and "

				SQL = SQL & Arr1.Item(Cmb_ParamTgl.SelectedIndex) & " between '"
				SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
			End If

			If Cb_ParamLain.Checked Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & Arr2.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamValue.Text) & "%' "
			End If

			SQL = SQL & "order by No_PO, No_Transaksi, Tanggal, Jam, Flag_Hasil_Produksi"

			Dim Lvw As ListViewItem
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Lvw = Lv_ProductionResult.Items.Add(.Rows(i).Item("No_Transaksi"))
							Lvw.SubItems.Add(.Rows(i).Item("No_PO"))
							Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
							Lvw.SubItems.Add(.Rows(i).Item("Jam"))
							Lvw.SubItems.Add(.Rows(i).Item("UserID"))
							Lvw.SubItems.Add(.Rows(i).Item("Kode_Barang"))
							Lvw.SubItems.Add(.Rows(i).Item("Nama"))
							Lvw.SubItems.Add(.Rows(i).Item("Jumlah"))
							Lvw.SubItems.Add(.Rows(i).Item("satuan"))
							Lvw.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Catatan")) = "", "-", General_Class.CekNULL(.Rows(i).Item("Catatan"))))
							Lvw.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Tgl_Hasil_Produksi")) = "", "-", Format(.Rows(i).Item("Tgl_Hasil_Produksi"), "dd MMM yyyy")))
							Lvw.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Jam_Hasil_Produksi")) = "", "-", General_Class.CekNULL(.Rows(i).Item("Jam_Hasil_Produksi"))))
							'Hide
							Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Flag_Hasil_Produksi")))

							If General_Class.CekNULL(.Rows(i).Item("Flag_Hasil_Produksi")) = "Y" Then
								Lvw.BackColor = Color.LightGreen
							Else
								Lvw.BackColor = Color.LightYellow
							End If

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

	Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_ProductionResult.SelectedIndexChanged

		If Lv_ProductionResult.Items.Count = 0 Then Exit Sub

		Try
			OpenConn()
			Lv_DetailFinishedGood.Items.Clear()
			Lv_DetailRawMaterial.Items.Clear()
			Lv_DetailPackaging.Items.Clear()
			Lv_DetailScrap.Items.Clear()

			'Finished Good
			SQL = "select b.No_Transaksi, a.No_Production_Order, sum(b.Jumlah) as Jumlah, b.Satuan, c.Keterangan as Kualitas, "
			SQL = SQL & "b.Kode_Unik_Berjalan, b.Qr_Code, b.Tgl_Expired "
			SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_detail_Pallet b, EMI_Master_Warna c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "and b.Jenis = c.Kode_Warna "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Production_Order = '" & Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text & "' "
			SQL = SQL & "group by b.No_Transaksi, a.No_Production_Order, b.Satuan, c.Keterangan, "
			SQL = SQL & "b.Kode_Unik_Berjalan, b.Qr_Code, b.Tgl_Expired"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim lvw As ListViewItem
					lvw = Lv_DetailFinishedGood.Items.Add(Dr("No_Transaksi"))
					lvw.SubItems.Add(Dr("No_Production_Order"))
					lvw.SubItems.Add(Dr("Qr_Code") + "-" + Dr("Kode_Unik_Berjalan"))
					lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
					lvw.SubItems.Add(Dr("Satuan"))
					lvw.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
					lvw.SubItems.Add(Dr("Kualitas"))
				Loop
			End Using

			'Raw Material
			SQL = "select b.No_Transaksi, b.kode_stock_owner, b.Kode_Barang, c.Nama as Nama_Barang, "
			SQL = SQL & "sum(b.Nilai_Formula) as Nilai_Formula, sum(b.Nilai_Produksi) as Nilai_Produksi, b.Satuan "
			SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b, barang c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and  b.Kode_Barang = c.Kode_Barang and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
			SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Production_Order = '" & Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text & "' "
			SQL = SQL & "group by b.No_Transaksi, b.kode_stock_owner, b.Kode_Barang, c.Nama,b.Satuan "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim lvw As ListViewItem
					lvw = Lv_DetailRawMaterial.Items.Add(Dr("No_Transaksi"))
					lvw.SubItems.Add(Dr("kode_stock_owner"))
					lvw.SubItems.Add(Dr("Kode_Barang"))
					lvw.SubItems.Add(Dr("Nama_Barang"))
					lvw.SubItems.Add(Format(Dr("Nilai_Produksi"), "N4"))
					lvw.SubItems.Add(Dr("Satuan"))
				Loop
			End Using

			'Packaging
			SQL = "select b.No_Transaksi, b.kode_stock_owner, b.Kode_Barang, c.Nama as Nama_Barang, "
			SQL = SQL & "sum(b.Nilai_Formula) as Nilai_Formula, sum(b.Nilai_Produksi) as Nilai_Produksi, b.Satuan "
			SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Packaging_detail b, barang c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and  b.Kode_Barang = c.Kode_Barang and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
			SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Production_Order = '" & Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text & "' "
			SQL = SQL & "group by a.No_Production_Order, b.No_Transaksi, b.kode_stock_owner, b.Kode_Barang, c.Nama, b.Satuan "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim lvw As ListViewItem
					lvw = Lv_DetailPackaging.Items.Add(Dr("No_Transaksi"))
					lvw.SubItems.Add(Dr("kode_stock_owner"))
					lvw.SubItems.Add(Dr("Kode_Barang"))
					lvw.SubItems.Add(Dr("Nama_Barang"))
					lvw.SubItems.Add(Format(Dr("Nilai_Produksi"), "N4"))
					lvw.SubItems.Add(Dr("Satuan"))
				Loop
			End Using

			'Scrap
			SQL = "select b.No_Transaksi, d.Kode_Barang, d.Nama as Nama_Barang, sum(b.Jumlah) as Jumlah, b.Satuan, b.proses,(c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode "
			SQL = SQL & "from Emi_Production_Results a, EMI_Production_Results_Detail_Scrap b, Barang_SN c, barang d "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
			SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "and b.Serial_Number = c.Serial_Number "
			SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Production_Order = '" & Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text & "' "
			SQL = SQL & "group by b.No_Transaksi, d.Kode_Barang, d.Nama, b.Satuan, b.proses, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lvw As ListViewItem
					Lvw = Lv_DetailScrap.Items.Add(Dr("No_Transaksi"))
					Lvw.SubItems.Add(Dr("Kode_Barang"))
					Lvw.SubItems.Add(Dr("Nama_Barang"))
					Lvw.SubItems.Add(Format(Dr("Jumlah"), "N4"))
					Lvw.SubItems.Add(Dr("Satuan"))
					Lvw.SubItems.Add(Dr("Proses"))
					Lvw.SubItems.Add(Dr("Barcode"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub CopyNoTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyNoTransaksiToolStripMenuItem.Click
		If Lv_ProductionResult.Items.Count = 0 Or Lv_ProductionResult.SelectedItems.Count = 0 Then
			MessageBox.Show(Base_Language.Lang_Pilih_Dahulu_No_Transaksi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Lv_ProductionResult.FocusedItem.Text)
	End Sub

	'Private Sub LaporanDetailBatchMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs)
	'    If Lv_ProductionResult.Items.Count = 0 Then Exit Sub

	'    If Lv_ProductionResult.Items.Count = 0 Or Lv_ProductionResult.SelectedItems.Count = 0 Then
	'        MessageBox.Show("Pilih dahulu data yang akan dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'        Exit Sub
	'    End If

	'    Try
	'        OpenConn()

	'        Dim CrDoc As New Object
	'        Dim kertas As String = ""

	'        Dim SF As String = ""
	'        SQL = "select Kode_Perusahaan from View_Laporan_Hasil_QC where Kode_Perusahaan = '" & KodePerusahaan & "' and "
	'        SQL = SQL & "No_Fak_Loading_Barang = '" & ListView1.FocusedItem.Text & "' "
	'        SQL = SQL & "and kode_barang = '" & ListView2.FocusedItem.SubItems(1).Text & "' "

	'        SF = "{View_Laporan_Hasil_QC.No_Fak_Loading_Barang} = '" & ListView1.FocusedItem.Text & "' "
	'        SF = SF & "and {View_Laporan_Hasil_QC.kode_perusahaan} = '" & KodePerusahaan & "' "
	'        SF = SF & "and {View_Laporan_Hasil_QC.Kode_Barang} = '" & ListView2.FocusedItem.SubItems(1).Text & "' "
	'        Using Ds = BindingTrans(SQL)
	'            If Ds.Tables("MyTable").Rows.Count <> 0 Then
	'                CrDoc = New Rpt_Laporan_Hasil_QC

	'                'With A_Place_For_Printing2
	'                '    CrDoc.SetDataSource(Ds)
	'                '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
	'                '    CrDoc.PrintOptions.PrinterName = ""
	'                '    CrDoc.RecordSelectionFormula = SF
	'                '    'CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
	'                '    .Text = "Laporan Hasil QC"
	'                '    .CrystalReportViewer1.ReportSource = CrDoc
	'                '    '.CrystalReportViewer1.DisplayGroupTree = False
	'                '    .Refresh()
	'                '    .Show()
	'                'End With

	'                '============================================================================================================================================
	'                '============================================================================================================================================

	'                kertas = "A4"

	'                CrDoc.SetDataSource(Ds)
	'                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
	'                CrDoc.PrintOptions.PrinterName = PrinterQC
	'                CrDoc.RecordSelectionFormula = SF
	'                'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

	'                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
	'                doctoprint.PrinterSettings.PrinterName = PrinterQC
	'                'doctoprint.DefaultPageSettings.Landscape = True
	'                Dim rawKind As Integer
	'                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
	'                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
	'                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
	'                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
	'                        CrDoc.PrintOptions.PaperSize = rawKind
	'                        Exit For
	'                    End If
	'                Next

	'                CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
	'                CrDoc.PrintToPrinter(1, False, 1, 99)

	'                MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'            Else
	'                CloseConn()
	'                MessageBox.Show("Data Tidak diTemukan", "Cetak Ulang", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'                Exit Sub

	'            End If
	'        End Using

	'        A_Place_For_Printing2.Focus()

	'        CloseConn()
	'    Catch ex As Exception
	'        CloseConn()
	'        MessageBox.Show(ex.Message)
	'        Exit Sub
	'    End Try
	'End Sub

	Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
		If Lv_ProductionResult.Items.Count = 0 Or Lv_ProductionResult.SelectedItems.Count = 0 Then
			Exit Sub
		End If
		EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = Lv_ProductionResult.FocusedItem.Text
		EMI_Barang_Masuk_Display_Rak.ShowDialog()
	End Sub

	Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamTgl.CheckedChanged
		If Cb_ParamTgl.Checked Then
			Cmb_ParamTgl.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
			Cb_TransaksiHrIni.Checked = False
		Else
			Cmb_ParamTgl.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
			Cmb_ParamTgl.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
		End If
	End Sub

	Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamLain.CheckedChanged
		If Cb_ParamLain.Checked Then
			Cmb_ParamLain.Enabled = True : Txt_ParamValue.Enabled = True
		Else
			Cmb_ParamLain.Enabled = False : Txt_ParamValue.Enabled = False
			Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamValue.Text = ""
		End If
	End Sub

	'======= CETAK ULANG ======='

	Private Sub LaporanGIGRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanGIGRToolStripMenuItem.Click
		If Lv_ProductionResult.Items.Count = 0 Then Exit Sub

		If Not Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_FlagSelesai).Text = "Y" Then
			MessageBox.Show("Order Produksi Belum Selesai", "Production Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim kertas As String = ""

			Dim NoPO As String = Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_NoPO).Text
			Dim NoTransaksi As String = Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_NoFak).Text

			SQL = "select Kode_Perusahaan from Vw_Laporan_Perfaktur_GI_GR "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_PO = '" & NoPO & "' and No_Transaksi = '" & NoTransaksi & "'"
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					CrDoc = New Laporan_Perfaktur_GI_GR
					kertas = "A4"

					'With A_Place_For_Printing2
					'    CrDoc.SetDataSource(Ds)
					'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'    CrDoc.PrintOptions.PrinterName = ""
					'    CrDoc.RecordSelectionFormula = "{Vw_Laporan_Perfaktur_GI_GR.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Laporan_Perfaktur_GI_GR.No_PO}='" & NoPO & "' and {Vw_Laporan_Perfaktur_GI_GR.No_Transaksi}='" & NoTransaksi & "' "
					'    CrDoc.SummaryInfo.ReportTitle = "Laporan GI GR"
					'    .Text = "Laporan GI GR"
					'    .CrystalReportViewer1.ReportSource = CrDoc
					'    .Refresh()
					'    .Show()
					'End With

					'============================================================================================================================================
					'============================================================================================================================================
					CrDoc.SetDataSource(Ds)
					CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					CrDoc.PrintOptions.PrinterName = PrinterQC
					CrDoc.RecordSelectionFormula = "{Vw_Laporan_Perfaktur_GI_GR.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Laporan_Perfaktur_GI_GR.No_PO}='" & NoPO & "' and {Vw_Laporan_Perfaktur_GI_GR.No_Transaksi}='" & NoTransaksi & "' "
					'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

					Dim doctoprint As New System.Drawing.Printing.PrintDocument()
					doctoprint.PrinterSettings.PrinterName = PrinterQC
					doctoprint.DefaultPageSettings.Landscape = True
					Dim rawKind As Integer
					CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
						If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
							rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
							CrDoc.PrintOptions.PaperSize = rawKind
							Exit For
						End If
					Next

					CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
					CrDoc.PrintToPrinter(1, False, 1, 99)

					MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub LaporanGIGRDetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanGIGRDetailToolStripMenuItem.Click
		If Lv_ProductionResult.Items.Count = 0 Then Exit Sub

		If Not Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_FlagSelesai).Text = "Y" Then
			MessageBox.Show("Order Produksi Belum Selesai", "Production Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim kertas As String = ""

			Dim NoPO As String = Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_NoPO).Text
			Dim NoTransaksi As String = Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_NoFak).Text

			SQL = "select Kode_Perusahaan from Vw_Laporan_Perfaktur_GI_GR_Detail "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_PO = '" & NoPO & "' and No_Transaksi = '" & NoTransaksi & "'"
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					CrDoc = New Laporan_Perfaktur_GI_GR_Detail
					kertas = "A4"

					'With A_Place_For_Printing2
					'    CrDoc.SetDataSource(Ds)
					'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'    CrDoc.PrintOptions.PrinterName = ""
					'    CrDoc.RecordSelectionFormula = "{Vw_Laporan_Perfaktur_GI_GR_Detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Laporan_Perfaktur_GI_GR_Detail.No_PO}='" & NoPO & "' and {Vw_Laporan_Perfaktur_GI_GR_Detail.No_Transaksi}='" & NoTransaksi & "' "
					'    CrDoc.SummaryInfo.ReportTitle = "Laporan GI GR"
					'    .Text = "Laporan GI GR"
					'    .CrystalReportViewer1.ReportSource = CrDoc
					'    .Refresh()
					'    .Show()
					'End With

					'============================================================================================================================================
					'============================================================================================================================================
					CrDoc.SetDataSource(Ds)
					CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					CrDoc.PrintOptions.PrinterName = PrinterQC
					CrDoc.RecordSelectionFormula = "{Vw_Laporan_Perfaktur_GI_GR.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Laporan_Perfaktur_GI_GR.No_PO}='" & NoPO & "' and {Vw_Laporan_Perfaktur_GI_GR.No_Transaksi}='" & NoTransaksi & "' "
					'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

					Dim doctoprint As New System.Drawing.Printing.PrintDocument()
					doctoprint.PrinterSettings.PrinterName = PrinterQC
					doctoprint.DefaultPageSettings.Landscape = True
					Dim rawKind As Integer
					CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
						If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
							rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
							CrDoc.PrintOptions.PaperSize = rawKind
							Exit For
						End If
					Next

					CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
					CrDoc.PrintToPrinter(1, False, 1, 99)

					MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
		If Lv_ProductionResult.Items.Count = 0 Then
			BatalkanToolStripMenuItem.Visible = False
			BatalkanToolStripMenuItem.Enabled = False
			Exit Sub
		End If
		If Lv_ProductionResult.FocusedItem Is Nothing Then Exit Sub

		Dim selectedSplit As String = Lv_ProductionResult.FocusedItem.Text

		Try
			OpenConn()

			'================================================
			'=     CEK APAKAH SPLIT SUDAH DI GOOD ISSUE     =
			'================================================
			SQL = "select Kode_Perusahaan from Emi_Production_Results where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & selectedSplit & "' and status is null "
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					BatalkanToolStripMenuItem.Visible = False
					BatalkanToolStripMenuItem.Enabled = False
				Else
					BatalkanToolStripMenuItem.Visible = True
					BatalkanToolStripMenuItem.Enabled = True
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub CetakUlangBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeToolStripMenuItem.Click

		If Lv_DetailFinishedGood.Items.Count = 0 Then Exit Sub

		Dim KdBarang, Qr_Code, TglExp, Tgl_Produksi, Tgl_Masuk, Batch, MetodePengeluaranStok As String
		Dim kode_unik_print As String
		Dim SelectedIndex As Integer = Lv_DetailFinishedGood.FocusedItem.Index
		Dim NoSplit As String = Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'======================
			'=      GET DATA      =
			'======================
			'Finished Good
			SQL = "select a.Kode_Perusahaan, a.Kode_Barang, b.Qr_Code, Tgl_Expired, b.Batch_Number, b.Tgl_Produksi, b.Tgl_Masuk, a.Metode_Pengeluaran_Stok "
			SQL = SQL & "from barang a, barang_sn b "
			SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
			SQL = SQL & "and a.kode_stock_owner = b.kode_stock_owner "
			SQL = SQL & "and a.kode_barang = b.kode_barang "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and b.Qr_Code + '-' + b.Kode_Unik_Berjalan = '" & Lv_DetailFinishedGood.FocusedItem.SubItems(itemFG_FullQR).Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					KdBarang = Dr("Kode_Barang")
					Qr_Code = Dr("Qr_Code")
					TglExp = If(General_Class.CekNULL(Dr("Tgl_Expired")) = "", "", Dr("Tgl_Expired"))
					Batch = Dr("Batch_Number")
					Tgl_Produksi = If(General_Class.CekNULL(Dr("Tgl_Produksi")) = "", "", Dr("Tgl_Produksi"))
					Tgl_Masuk = If(General_Class.CekNULL(Dr("Tgl_Masuk")) = "", "", Dr("Tgl_Masuk"))
					MetodePengeluaranStok = Dr("Metode_Pengeluaran_Stok")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data Tidak Ditemukan", "Production Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'==================================
			'=      GENERATE NEW BARCODE      =
			'==================================

			kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

			Dim fullNewQr As String = Lv_DetailFinishedGood.Items(SelectedIndex).SubItems(itemFG_FullQR).Text

			Barcode.Image = Generate_QR_NoPadding(fullNewQr)

			Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")

			'   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

			'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
			Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
			'End If

			fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
			FileSize1 = fs1.Length
			rawData1 = New Byte(FileSize1) {}
			fs1.Read(rawData1, 0, FileSize1)
			fs1.Close()
			Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1

			''INSERT TABEL CETAK QR
			'SQL = "insert into Cetak_Finish_Good (Kode_Perusahaan, Kode_Barang, Barcode, QrUtuh, Qr, Tgl_Expired, batch, tgl_produksi, kode_unik_print, tanggal_masuk, metode_pengeluaran_stok) values "
			''SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_KdBarang.Text & "', @newBarcode, '" & Txt_NamaBarang.Text & "', "
			'SQL = SQL & "('" & KodePerusahaan & "', '" & KdBarang & "', @newBarcode, "
			'SQL = SQL & "'" & fullNewQr & "', '" & Qr_Code & "', '" & Format(Date.Parse(TglExp), "yyyy-MM-dd") & "', '" & Batch & "',  '" & Format(Date.Parse(Tgl_Produksi), "yyyy-MM-dd") & "', "
			'SQL = SQL & "'" & kode_unik_print & "', '" & If(String.IsNullOrEmpty(Tgl_Masuk), "", Format(Date.Parse(Tgl_Masuk), "yyyy-MM-dd")) & "', '" & MetodePengeluaranStok & "'"
			'SQL = SQL & ")"
			'ExecuteTrans(SQL)

			'HAPUS TABEL SEMENTARA
			SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_1"
			ExecuteTrans(SQL)

			SQL = "select a.No_Production_Order, c.Kode_Barang, d.Nama as Nama_Barang, b.proses, b.tahap, sum(b.Jumlah) as Jumlah, b.Satuan, b.Troli, b.nomor, e.Id_Routing, f.Keterangan as Routing "
			SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_detail_Pallet b, Emi_Split_Production_Order c, barang d, EMI_Order_Produksi e, EMI_Master_Routing f "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and e.Kode_Perusahaan = f.Kode_Perusahaan "
			SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "and a.No_Production_Order = c.no_transaksi "
			SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
			SQL = SQL & "and c.No_PO = e.No_Faktur "
			SQL = SQL & "and e.Id_Routing = f.Id_Routing "
			SQL = SQL & "and a.Status is null and c.Status is null and e.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Production_Order = '" & NoSplit & "' "
			SQL = SQL & "and (b.Qr_Code + '-' + b.Kode_Unik_Berjalan) = '" & fullNewQr & "' "
			SQL = SQL & "group by a.No_Production_Order, c.Kode_Barang, d.Nama, b.proses, b.tahap, b.Satuan, b.Troli, b.nomor, e.Id_Routing, f.Keterangan "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_1 (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
							SQL = SQL & "Proses, Tahap, Jumlah, Satuan, Troli, Nomor, id_routing, routing, Kode_unik_print)  "
							SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("No_Production_Order") & "', @newBarcode, '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', '" & fullNewQr & "', '" & Qr_Code & "', "
							SQL = SQL & "'" & Format(Date.Parse(Tgl_Produksi), "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("proses") & "', '" & .Rows(i).Item("tahap") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
							SQL = SQL & "'" & .Rows(i).Item("Troli") & "', '" & .Rows(i).Item("nomor") & "', '" & .Rows(i).Item("Id_Routing") & "', '" & .Rows(i).Item("Routing") & "', '" & kode_unik_print & "') "
							ExecuteTrans(SQL)

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Production Result Tidak Ditemukann", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'===========================
		'=      CETAK BARCODE      =
		'===========================

		Try
			OpenConn()
			Dim CrDoc As New Object

			Dim KertasBesar As String = "BarcodeFG"
			Dim KertasKecil As String = "BarcodeQC"

			SQL = "select Kode_Perusahaan from N_EMI_Barcode_Label_Barcode_GR_1 where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & KdBarang & "' and Kode_Unik_Print = '" & kode_unik_print & "' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					CrDoc = New N_EMI_Label_Barcode_GR_1

					'With A_Place_For_Printing2
					'    CrDoc.SetDataSource(Ds)
					'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'    CrDoc.PrintOptions.PrinterName = ""
					'    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "

					'    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
					'    Dim rawKind As Integer
					'    Dim foundPaper As Boolean = False
					'    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					'    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
					'        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
					'            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
					'            CrDoc.PrintOptions.PaperSize = rawKind
					'            foundPaper = True
					'            Exit For
					'        End If
					'    Next

					'    If Not foundPaper Then
					'        CloseConn()
					'        MessageBox.Show("Kertas Tidak Ditemukan", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					'        Exit Sub
					'    End If

					'    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
					'    .Text = "New Barcode Finish Good"
					'    .CrystalReportViewer1.ReportSource = CrDoc
					'    .Refresh()
					'    .Show()
					'End With

					'========================================================================================================================================================================================

					'With A_Place_For_Printing2
					'    CrDoc.SetDataSource(Ds)
					'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'    CrDoc.PrintOptions.PrinterName = ""
					'    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
					'    CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
					'    .Text = "Faktur Premix Label"
					'    .CrystalReportViewer1.ReportSource = CrDoc
					'    .Refresh()
					'    .Show()
					'End With

					'========================================================================================================================================================================================

					'==========================
					'=     BARCODEE BESAR     =
					'==========================
					Dim doctoprint As New System.Drawing.Printing.PrintDocument()

					CrDoc.SetDataSource(Ds)
					CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_1.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Barang} = '" & KdBarang & "' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Unik_Print} = '" & kode_unik_print & "' "
					CrDoc.PrintOptions.PrinterName = PrinterBarcode

					doctoprint.PrinterSettings.PrinterName = PrinterBarcode

					Dim rawKind As Integer
					Dim foundPaper As Boolean = False
					CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
						If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
							rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
							CrDoc.PrintOptions.PaperSize = rawKind
							foundPaper = True
							Exit For
						End If
					Next

					If Not foundPaper Then
						'CloseConn()
						CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
						MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'Exit Sub
					End If

					CrDoc.PrintToPrinter(1, False, 1, 2500)

				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub CetakUlangBarcodeFGToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeFGToolStripMenuItem.Click
		If Lv_DetailScrap.Items.Count = 0 Then Exit Sub

		Dim kode_unik_print As String
		Dim SelectedIndex As Integer = Lv_DetailScrap.FocusedItem.Index
		Dim NoSplit As String = Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text
		Dim Selected_Barcode As String = Lv_DetailScrap.FocusedItem.SubItems(6).Text

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'======================
			'=      GET DATA      =
			'======================

			'HAPUS TABEL SEMENTARA
			SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_1_Scrap "
			ExecuteTrans(SQL)

			SQL = "select a.No_Production_Order, d.Kode_Barang, e.Nama as Nama_Barang, (c.Qr_Code +'-' + c.Kode_Unik_Berjalan) as Barcode, c.Qr_Code,  "
			SQL = SQL & "d.Tgl_Produksi, c.Proses, sum(c.jumlah) as jumlah, c.Satuan, c.nomor, f.Id_Routing, g.Keterangan as Routing "
			SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_detail_barang b, Emi_Production_Results_Detail_Scrap c, Emi_Split_Production_Order d, Barang e, EMI_Order_Produksi f, EMI_Master_Routing g "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.kode_perusahaan "
			SQL = SQL & "and d.Kode_Perusahaan = f.Kode_Perusahaan and g.Kode_Perusahaan = g.Kode_Perusahaan "
			SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
			SQL = SQL & "and a.No_Production_Order = d.No_Transaksi "
			SQL = SQL & "and b.Kode_Stock_Owner = e.Kode_Stock_Owner and b.Kode_Barang_scrap = e.Kode_Barang "
			SQL = SQL & "and d.No_PO = f.No_Faktur "
			SQL = SQL & "and g.Id_Routing = f.Id_Routing "
			SQL = SQL & "and a.Status is null and d.Status is null and f.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Production_Order = '" & NoSplit & "' "
			SQL = SQL & "and (c.Qr_Code +'-' + c.Kode_Unik_Berjalan) = '" & Selected_Barcode & "' "
			SQL = SQL & "group by a.No_Production_Order, d.Kode_Barang, e.Nama, (c.Qr_Code +'-' + c.Kode_Unik_Berjalan), c.Qr_Code,  "
			SQL = SQL & "d.Tgl_Produksi, c.Proses, c.Satuan, c.nomor, f.Id_Routing, g.Keterangan"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							'==================================
							'=      GENERATE New BARCODE      =
							'==================================
							kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

							Dim fullNewQr As String = .Rows(i).Item("Barcode")

							Barcode.Image = Generate_QR_NoPadding(fullNewQr)

							Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")

							'   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

							'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
							Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
							'End If

							fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
							FileSize1 = fs1.Length
							rawData1 = New Byte(FileSize1) {}
							fs1.Read(rawData1, 0, FileSize1)
							fs1.Close()
							Cmd.Parameters.Add("@newBarcodescrap" & kode_unik_print, SqlDbType.Image).Value = rawData1

							SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_1_Scrap (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
							SQL = SQL & "Proses, Jumlah, Satuan, Nomor, id_routing, routing, Kode_unik_print)  "
							SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("No_Production_Order") & "', @newBarcodescrap" & kode_unik_print & ", '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', "
							SQL = SQL & "'" & fullNewQr & "', '" & .Rows(i).Item("Qr_Code") & "',  "
							SQL = SQL & "'" & Format(.Rows(i).Item("Tgl_Produksi"), "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Proses") & "', '" & .Rows(i).Item("jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
							SQL = SQL & "'" & .Rows(i).Item("nomor") & "', '" & .Rows(i).Item("Id_Routing") & "', '" & .Rows(i).Item("Routing") & "', '" & kode_unik_print & "') "
							ExecuteTrans(SQL)

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Production Result Tidak Ditemukann", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'===========================
		'=      CETAK BARCODE      =
		'===========================

		Try
			OpenConn()
			Dim CrDoc As New Object

			Dim KertasBesar As String = "BarcodeFG"
			Dim KertasKecil As String = "BarcodeQC"

			SQL = "select Kode_Perusahaan from N_EMI_Barcode_Label_Barcode_GR_1_Scrap where Kode_Perusahaan='" & KodePerusahaan & "'  and Kode_Unik_Print = '" & kode_unik_print & "' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					CrDoc = New N_EMI_Label_Barcode_GR_1_Scrap

					'With A_Place_For_Printing2
					'    CrDoc.SetDataSource(Ds)
					'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'    CrDoc.PrintOptions.PrinterName = ""
					'    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "

					'    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
					'    Dim rawKind As Integer
					'    Dim foundPaper As Boolean = False
					'    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					'    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
					'        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
					'            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
					'            CrDoc.PrintOptions.PaperSize = rawKind
					'            foundPaper = True
					'            Exit For
					'        End If
					'    Next

					'    If Not foundPaper Then
					'        CloseConn()
					'        MessageBox.Show("Kertas Tidak Ditemukan", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					'        Exit Sub
					'    End If

					'    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
					'    .Text = "New Barcode Finish Good"
					'    .CrystalReportViewer1.ReportSource = CrDoc
					'    .Refresh()
					'    .Show()
					'End With

					'========================================================================================================================================================================================

					'With A_Place_For_Printing2
					'    CrDoc.SetDataSource(Ds)
					'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'    CrDoc.PrintOptions.PrinterName = ""
					'    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
					'    CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
					'    .Text = "Faktur Premix Label"
					'    .CrystalReportViewer1.ReportSource = CrDoc
					'    .Refresh()
					'    .Show()
					'End With

					'========================================================================================================================================================================================

					'==========================
					'=     BARCODEE BESAR     =
					'==========================
					Dim doctoprint As New System.Drawing.Printing.PrintDocument()

					CrDoc.SetDataSource(Ds)
					CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_1_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_1_Scrap.Kode_Unik_Print} = '" & kode_unik_print & "' "
					CrDoc.PrintOptions.PrinterName = PrinterBarcode

					doctoprint.PrinterSettings.PrinterName = PrinterBarcode

					Dim rawKind As Integer
					Dim foundPaper As Boolean = False
					CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
						If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
							rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
							CrDoc.PrintOptions.PaperSize = rawKind
							foundPaper = True
							Exit For
						End If
					Next

					If Not foundPaper Then
						'CloseConn()
						CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
						MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'Exit Sub
					End If

					CrDoc.PrintToPrinter(1, False, 1, 2500)

				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub CetakUlangBarcodeQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeQCToolStripMenuItem.Click

		Exit Sub

		If Lv_DetailFinishedGood.Items.Count = 0 Then Exit Sub

		Dim KdBarang, Qr_Code, TglExp, Tgl_Produksi, Tgl_Masuk, Batch, MetodePengeluaranStok As String
		Dim kode_unik_print As String
		Dim SelectedIndex As Integer = Lv_DetailFinishedGood.FocusedItem.Index

		Try
			OpenConn()

			'======================
			'=      GET DATA      =
			'======================
			'Finished Good
			SQL = "select distinct a.Kode_Perusahaan, a.Kode_Barang, b.Qr_Code, Tgl_Expired, b.Batch_Number, b.Tgl_Produksi, b.Tgl_Masuk, a.Metode_Pengeluaran_Stok "
			SQL = SQL & "from barang a, barang_sn b "
			SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
			SQL = SQL & "and a.kode_stock_owner = b.kode_stock_owner "
			SQL = SQL & "and a.kode_barang = b.kode_barang "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and b.Qr_Code + '-' + b.Kode_Unik_Berjalan = '" & Lv_DetailFinishedGood.FocusedItem.SubItems(itemFG_FullQR).Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					KdBarang = Dr("Kode_Barang")
					Qr_Code = Dr("Qr_Code")
					TglExp = If(General_Class.CekNULL(Dr("Tgl_Expired")) = "", "", Dr("Tgl_Expired"))
					Batch = Dr("Batch_Number")
					Tgl_Produksi = If(General_Class.CekNULL(Dr("Tgl_Produksi")) = "", "", Dr("Tgl_Produksi"))
					Tgl_Masuk = If(General_Class.CekNULL(Dr("Tgl_Masuk")) = "", "", Dr("Tgl_Masuk"))
					MetodePengeluaranStok = Dr("Metode_Pengeluaran_Stok")
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show("Data Tidak Ditemukan", "Production Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'==================================
			'=      GENERATE NEW BARCODE      =
			'==================================
			'HAPUS TABEL SEMENTARA
			SQL = "truncate table Cetak_Finish_Good "
			ExecuteTrans(SQL)

			kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

			Dim fullNewQr As String = Lv_DetailFinishedGood.Items(SelectedIndex).SubItems(itemFG_FullQR).Text

			Barcode.Image = Generate_QR(fullNewQr)

			Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")

			'   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

			'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
			Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
			'End If

			fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
			FileSize1 = fs1.Length
			rawData1 = New Byte(FileSize1) {}
			fs1.Read(rawData1, 0, FileSize1)
			fs1.Close()
			Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1

			'INSERT TABEL CETAK QR
			SQL = "insert into Cetak_Finish_Good (Kode_Perusahaan, Kode_Barang, Barcode, QrUtuh, Qr, Tgl_Expired, batch, tgl_produksi, kode_unik_print, tanggal_masuk, metode_pengeluaran_stok) values "
			'SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_KdBarang.Text & "', @newBarcode, '" & Txt_NamaBarang.Text & "', "
			SQL = SQL & "('" & KodePerusahaan & "', '" & KdBarang & "', @newBarcode, "
			SQL = SQL & "'" & fullNewQr & "', '" & Qr_Code & "', '" & Format(Date.Parse(TglExp), "yyyy-MM-dd") & "', '" & Batch & "',  '" & Format(Date.Parse(Tgl_Produksi), "yyyy-MM-dd") & "', "
			SQL = SQL & "'" & kode_unik_print & "', '" & If(String.IsNullOrEmpty(Tgl_Masuk), "", Format(Date.Parse(Tgl_Masuk), "yyyy-MM-dd")) & "', '" & MetodePengeluaranStok & "'"
			SQL = SQL & ")"
			ExecuteTrans(SQL)

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'===========================
		'=      CETAK BARCODE      =
		'===========================

		Try
			OpenConn()
			Dim CrDoc As New Object

			Dim KertasBesar As String = "BarcodeFG"
			Dim KertasKecil As String = "BarcodeQC"

			SQL = "select Kode_Perusahaan from Cetak_Finish_Good where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & KdBarang & "' and Kode_Unik_Print = '" & kode_unik_print & "' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					CrDoc = New NewBarcodeFinishGoodKecil

					'With A_Place_For_Printing2
					'    CrDoc.SetDataSource(Ds)
					'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'    CrDoc.PrintOptions.PrinterName = ""
					'    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "

					'    Dim doctoprint2 As New System.Drawing.Printing.PrintDocument()
					'    doctoprint2.PrinterSettings.PrinterName = PrinterBarcodeQC
					'    Dim rawKind2 As Integer
					'    Dim foundPaper As Boolean = False
					'    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					'    For i = 0 To doctoprint2.PrinterSettings.PaperSizes.Count - 1
					'        If doctoprint2.PrinterSettings.PaperSizes(i).PaperName = KertasKecil Then
					'            rawKind2 = CInt(doctoprint2.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint2.PrinterSettings.PaperSizes(i)))
					'            CrDoc.PrintOptions.PaperSize = rawKind2
					'            foundPaper = True
					'            Exit For
					'        End If
					'    Next

					'    If Not foundPaper Then
					'        CloseConn()
					'        MessageBox.Show("Kertas Tidak Ditemukan", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					'        Exit Sub
					'    End If

					'    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
					'    .Text = "New Barcode Finish Good"
					'    .CrystalReportViewer1.ReportSource = CrDoc
					'    .Refresh()
					'    .Show()
					'End With

					'========================================================================================================================================================================================

					'With A_Place_For_Printing2
					'    CrDoc.SetDataSource(Ds)
					'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'    CrDoc.PrintOptions.PrinterName = ""
					'    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
					'    CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
					'    .Text = "Faktur Premix Label"
					'    .CrystalReportViewer1.ReportSource = CrDoc
					'    .Refresh()
					'    .Show()
					'End With

					'========================================================================================================================================================================================

					'==========================
					'=     BARCODEE KECIL     =
					'==========================

					Dim doctoprint2 As New System.Drawing.Printing.PrintDocument()

					CrDoc.SetDataSource(Ds)
					CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
					CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC

					doctoprint2.PrinterSettings.PrinterName = PrinterBarcodeQC

					Dim rawKind2 As Integer
					Dim foundPaper As Boolean = False
					CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					For i = 0 To doctoprint2.PrinterSettings.PaperSizes.Count - 1
						If doctoprint2.PrinterSettings.PaperSizes(i).PaperName = KertasKecil Then
							rawKind2 = CInt(doctoprint2.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint2.PrinterSettings.PaperSizes(i)))
							CrDoc.PrintOptions.PaperSize = rawKind2
							foundPaper = True
							Exit For
						End If
					Next

					If Not foundPaper Then
						'CloseConn()
						CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
						MessageBox.Show("Kertas Tidak Ditemukan", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'Exit Sub
					End If

					CrDoc.PrintToPrinter(1, False, 1, 2500)

				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	'========================================================================
	'=     PEMBATALAN
	'========================================================================

	Private Sub BatalkanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanToolStripMenuItem.Click
		If Lv_ProductionResult.Items.Count = 0 Or Lv_ProductionResult.FocusedItem.Index = -1 Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim JudulNotif As String = "Pembatalan Production Result"

			'====================
			'=     CEK ROLE     =
			'====================
			If CekButtonRole("Batal_Good_Issue") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Production Result", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Transaksi Production Result Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If tanya = vbNo Then
				CloseTrans()
				CloseConn()
				Exit Sub
			End If

			Dim No_Split As String = Lv_ProductionResult.FocusedItem.Text

			'=========================================================
			'=     CEK APAKAH SPLIT SUDAH DI BATALKAN SEBELUMNYA     =
			'=========================================================
			SQL = "select status from Emi_Split_Production_Order where kode_perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & No_Split & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan Karena No Split Sudah Dibatalkan Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'================================================
			'=     CEK APAKAH SPLIT SUDAH DI GOOD ISSUE     =
			'================================================
			SQL = "select Kode_Perusahaan from Emi_Production_Results where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & No_Split & "' and status is null "
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan Karena No Split Belum pada Step Good Issue", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'================================================
			'=     CEK APAKAH SPLIT SUDAH GOOD RECEIVED     =
			'================================================
			SQL = "select b.Kode_Perusahaan "
			SQL = SQL & "from emi_production_results a, emi_production_results_detail_barang b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "and a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan karena No Split Sudah Masuk Ketahap Good Received", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'====================================
			'=     MASUK KE STEP PEMBATALAN     =
			'====================================

			'PEMBATALAN PRODUCTION RESULT DETAIL
			SQL = "select a.No_Transaksi from Emi_Production_Results a, Emi_Production_Results_Detail b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Order = '" & No_Split & "' and a.Status is null  "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					Dim NoPR As String = Dr("No_Transaksi")
					Dr.Close()

					SQL = "update Emi_Production_Results_Detail set status = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & NoPR & "'"
					ExecuteTrans(SQL)
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data Production Detail Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "select Kode_Perusahaan from Emi_Production_Results where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & No_Split & "' and status is null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					SQL = "update Emi_Production_Results set Status = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & No_Split & "' and status is null "
					ExecuteTrans(SQL)
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data Production Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Pembatalan Production Result Berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		BtnBarangMasuk_Cari_Click(sender, e)

	End Sub

	Private Function Generate_QR_NoPadding(ByVal isi As String)

		Dim options As New ZXing.QrCode.QrCodeEncodingOptions()

		options.DisableECI = True
		options.CharacterSet = "UTF-8"
		options.Width = 80
		options.Height = 80
		options.Margin = 0

		Dim qr As New ZXing.BarcodeWriter()
		qr.Format = ZXing.BarcodeFormat.QR_CODE
		qr.Options = options

		Dim result As New Bitmap(qr.Write(isi))
		Return result
	End Function

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

	Private Sub Lv_ProductionResult_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_ProductionResult.MouseMove, Lv_DetailFinishedGood.MouseMove,
			Lv_DetailRawMaterial.MouseMove, Lv_DetailPackaging.MouseMove, Lv_DetailScrap.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	Private Sub ContextMenuStrip2_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip2.Opening
		If Lv_DetailFinishedGood.Items.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If

		'=========================================================
		'=     CEK APAKAH MOUSE BERADA DI ATAS ROWS LISTVIEW     =
		'=========================================================
		Dim mousePos As Point = Lv_DetailFinishedGood.PointToClient(Cursor.Position)
		Dim info As ListViewHitTestInfo = Lv_DetailFinishedGood.HitTest(mousePos)

		If info.Item Is Nothing Then
			e.Cancel = True
			Exit Sub
		End If

		Lv_DetailFinishedGood.FocusedItem = info.Item
		info.Item.Selected = True
	End Sub

	Private Sub ContextMenuStrip3_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip3.Opening
		If Lv_DetailScrap.Items.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If

		'=========================================================
		'=     CEK APAKAH MOUSE BERADA DI ATAS ROWS LISTVIEW     =
		'=========================================================
		Dim mousePos As Point = Lv_DetailScrap.PointToClient(Cursor.Position)
		Dim info As ListViewHitTestInfo = Lv_DetailScrap.HitTest(mousePos)

		If info.Item Is Nothing Then
			e.Cancel = True
			Exit Sub
		End If

		Lv_DetailScrap.FocusedItem = info.Item
		info.Item.Selected = True
	End Sub

End Class