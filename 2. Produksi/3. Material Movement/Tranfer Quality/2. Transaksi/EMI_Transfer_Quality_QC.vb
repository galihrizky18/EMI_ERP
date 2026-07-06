Imports System.IO

Public Class EMI_Transfer_Quality_QC

	Dim arrWarna, arrInisialFaktur As New ArrayList
	Dim JudulMessage As String = "Transfer Quality"

	Dim Random As New Random()
	Private imageBytes1 As Byte = Nothing
	Private FileSize1 As UInt32
	Private rawData1() As Byte
	Private fs1 As FileStream

	Dim Lv_Barcode, Lv_KdSo, Lv_KdBarang, Lv_NmBarang, Lv_JumlahStock, Lv_JumlahBags, Lv_Satuan, Lv_Rak, Lv_Batch, Lv_IdWarehouse, Lv_NoPallet, Lv_Warna As String

	Dim item_Barcode As Integer = 0
	Dim item_KdSo As Integer = 1
	Dim item_KdBarang As Integer = 2
	Dim item_NmBarang As Integer = 3
	Dim item_JumlahStock As Integer = 4
	Dim item_JumlaBags As Integer = 5
	Dim item_Satuan As Integer = 6
	Dim item_Rak As Integer = 7
	Dim item_Batch As Integer = 8
	Dim item_IdWarehouse As Integer = 9
	Dim item_NoPallet As Integer = 10
	Dim item_Warna As Integer = 11

	Private Sub EMI_Transfer_Quality_QC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		IntialListView()
		Kosong()

	End Sub

	Private Sub Kosong()
		get_jam()

		TxtNo_Transaksi.Text = ""
		Txt_ScanBarcode.Text = ""
		TxtKeterangan.Text = ""

		Lv_Data.Items.Clear()
		Cmb_Lokasi.Items.Clear()
		Cmb_KualitasAwal.Items.Clear()
		Cmb_KualitasAkhir.Items.Clear()

		Txt_ScanBarcode.Enabled = False
		Cmb_KualitasAwal.Enabled = True
		Cmb_KualitasAkhir.Enabled = True

		Try
			OpenConn()

			'LOAD KUALITAS
			SQL = "select Kode_Warna, Keterangan from EMI_Master_Warna where Kode_Perusahaan = '" & KodePerusahaan & "'"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Cmb_KualitasAwal.Items.Add(Dr("Keterangan")) : Cmb_KualitasAkhir.Items.Add(Dr("Keterangan"))
					arrWarna.Add(Dr("Kode_Warna"))

				Loop
			End Using

			Cmb_Lokasi.Items.Clear() : arrInisialFaktur.Clear()
			SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and aktif = 'Y' and kode_stock_owner = '" & Module1.Lokasi & "' order by Kode_Stock_Owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Cmb_Lokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
				Loop
				Cmb_Lokasi.SelectedIndex = 0
			End Using

			get_no_faktur()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub get_no_faktur()
		Dim FPro_Results As String = "TFQ-"
		TxtNo_Transaksi.Text = FPro_Results & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
									  General_Class.Get_Last_Number2("Emi_TF_Quality", "no_faktur", JumlahDigit,
									  "Kode_perusahaan", KodePerusahaan,
									  "And", "substring(no_faktur,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Kosong()
	End Sub

	Private Sub IntialListView()

		Lv_Data.Columns.Clear()
		Lv_Data.Columns.Add("Barcode", 220, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("Kode Stock Owner", 170, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Nama Barang", 0, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Jumlah Stock", 190, HorizontalAlignment.Right)
		Lv_Data.Columns.Add("Jumlah Bags", 190, HorizontalAlignment.Right)
		Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("Rak", 140, HorizontalAlignment.Left)

		'Hide
		Lv_Data.Columns.Add("Batch", 0, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("id_warehouse", 0, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("no_pallet", 0, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("warna", 0, HorizontalAlignment.Center)
		Lv_Data.View = View.Details

	End Sub

	Private Sub Get_LvData(ByVal index As Integer)

		Lv_KdSo = Lv_Data.Items(index).SubItems(item_KdSo).Text
		Lv_KdBarang = Lv_Data.Items(index).SubItems(item_KdBarang).Text
		Lv_NmBarang = Lv_Data.Items(index).SubItems(item_NmBarang).Text
		Lv_JumlahStock = Lv_Data.Items(index).SubItems(item_JumlahStock).Text
		Lv_JumlahBags = Lv_Data.Items(index).SubItems(item_JumlaBags).Text
		Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
		Lv_Rak = Lv_Data.Items(index).SubItems(item_Rak).Text
		Lv_Batch = Lv_Data.Items(index).SubItems(item_Batch).Text
		Lv_IdWarehouse = Lv_Data.Items(index).SubItems(item_IdWarehouse).Text
		Lv_NoPallet = Lv_Data.Items(index).SubItems(item_NoPallet).Text
		Lv_Warna = Lv_Data.Items(index).SubItems(item_Warna).Text
		Lv_Barcode = Lv_Data.Items(index).SubItems(item_Barcode).Text
	End Sub

	Private Sub Cmb_KualitasAwal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_KualitasAwal.SelectedIndexChanged
		If Cmb_KualitasAwal.SelectedIndex = -1 Then Exit Sub

		If Cmb_Lokasi.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu Lokasi", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Lokasi.Focus()
			Exit Sub
		End If

		Cmb_KualitasAkhir.SelectedIndex = -1 : Cmb_KualitasAkhir.Text = ""
		Txt_ScanBarcode.Enabled = False : Txt_ScanBarcode.Text = ""

	End Sub

	Private Sub Cmb_KualitasAkhir_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_KualitasAkhir.SelectedIndexChanged
		If Cmb_KualitasAkhir.SelectedIndex = -1 Then Exit Sub

		If Cmb_KualitasAwal.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu Qualitas Awal", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_KualitasAkhir.SelectedIndex = -1 : Cmb_KualitasAkhir.Text = ""
			Cmb_KualitasAwal.Focus()
			Txt_ScanBarcode.Enabled = False : Txt_ScanBarcode.Text = ""
			Exit Sub
		ElseIf Cmb_KualitasAkhir.SelectedIndex = Cmb_KualitasAwal.SelectedIndex Then
			MessageBox.Show("Kualitas Akhir Tidak Boleh sama Dengan Awal", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_KualitasAkhir.SelectedIndex = -1 : Cmb_KualitasAkhir.Text = ""
			Cmb_KualitasAkhir.Focus()
			Txt_ScanBarcode.Enabled = False : Txt_ScanBarcode.Text = ""
			Exit Sub
		Else
			Txt_ScanBarcode.Text = ""
		End If

	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

		If Cmb_Lokasi.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu Lokasi", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Lokasi.Focus()
			Exit Sub
		ElseIf Cmb_KualitasAwal.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu Qualitas Awal", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_KualitasAwal.Focus()
			Exit Sub
		ElseIf Cmb_KualitasAkhir.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu Qualitas Akhir", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_KualitasAkhir.Focus()
			Exit Sub
		End If

		Txt_ScanBarcode.Enabled = True : Txt_ScanBarcode.Focus()
		Cmb_KualitasAwal.Enabled = False : Cmb_KualitasAkhir.Enabled = False

	End Sub

	Private Sub Txt_ScanBarcode_Leave(sender As Object, e As EventArgs) Handles Txt_ScanBarcode.Leave
		If Txt_ScanBarcode.Text.Trim.Length = 0 Then Exit Sub

		Btn_Insert_Click(sender, e)
	End Sub

	Private Sub Txt_ScanBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ScanBarcode.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			Lv_Data.Focus()
		End If
	End Sub

	Private Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_Insert.Click
		If Txt_ScanBarcode.Text.Trim.Length = 0 Then
			MessageBox.Show($"Harap isi dahulu barcode yang ingin ditransfer", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Txt_ScanBarcode.Focus()
			Exit Sub
		End If

		For i As Integer = 0 To Lv_Data.Items.Count - 1
			Dim Barcode As String = Lv_Data.Items(i).SubItems(item_Barcode).Text.Trim
			If Barcode.Trim = Txt_ScanBarcode.Text.Trim Then
				MessageBox.Show($"Barcode {Txt_ScanBarcode.Text.Trim} sudah ada di dalam list", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Txt_ScanBarcode.Focus()
				Exit Sub
			End If
		Next

		Try
			OpenConn()

			'=================================================
			'=     AMBIL DATA BARANG BERDASARKAN BARCODE     =
			'=================================================

#Region "Kode Lama"

			'SQL = "SELECT top 1 a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, a.Kode_Stock_Owner, "
			'SQL = SQL & "a.Kode_Barang, b.Nama, "
			'SQL = SQL & "dbo.Ubah_Satuan(a.Kode_Perusahaan, 'masa' , a.Kode_Barang, b.Satuan, b.Satuan, a.Jumlah) as Jumlah, "
			'SQL = SQL & "a.Jumlah_Bags, 'KG' as satuan, a.Batch_Number, a.Id_Warehouse, c.Keterangan as Rak, "
			'SQL = SQL & "a.Nomor_Pallet, a.Tgl_Expired, b.Metode_Pengeluaran_Stok, a.Tgl_Masuk, a.Warna, a.Blok_SN "
			'SQL = SQL & "from barang_sn a, barang b, view_warehouse_position c "
			'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
			'SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
			'SQL = SQL & "and a.Id_Warehouse = c.Id_WMS_Warehouse_Position "
			'SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
			'SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'SQL = SQL & "and a.Jumlah <> 0 "
			'SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & Txt_ScanBarcode.Text & "' "

#End Region

			SQL = $"
				select (b.Qr_Code+'-'+b.Kode_Unik_Berjalan) as Barcode, a.Kode_Stock_Owner, a.Kode_Barang, a.nama,
					   sum(b.Jumlah) as Jumlah, sum(b.Jumlah_Bags) as Jumlah_Bags, a.Satuan, b.Id_Warehouse, c.Keterangan as Rak, b.Batch_Number,
					   b.Nomor_Pallet, b.Warna
				from barang a
					inner join Barang_SN b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang
					inner join view_warehouse_position c on a.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Warehouse = c.Id_WMS_Warehouse_Position
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and b.Jumlah <> 0
				and b.Blok_SN is null
				and (b.Qr_Code+'-'+b.Kode_Unik_Berjalan) = '{Txt_ScanBarcode.Text}'
				group by (b.Qr_Code+'-'+b.Kode_Unik_Berjalan), a.Kode_Stock_Owner, a.Kode_Barang, a.nama,
						 a.Satuan, b.Id_Warehouse, c.Keterangan, b.Batch_Number, b.Nomor_Pallet, b.Warna
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If Not arrWarna(Cmb_KualitasAwal.SelectedIndex) = Dr("Warna") Then
						Dr.Close()
						CloseConn()
						MessageBox.Show("Kualitas Barcode Tidak Sama Dengan Kualitas Awal", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_ScanBarcode.Text = ""
						Txt_ScanBarcode.Focus()
						Exit Sub
					End If

					Dim Lv As New ListViewItem
					Do
						Lv = Lv_Data.Items.Add(Dr("Barcode"))
						Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
						Lv.SubItems.Add(Dr("Kode_Barang"))
						'Lv.SubItems.Add(Dr("nama"))
						Lv.SubItems.Add("X")
						Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
						Lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N0"))
						Lv.SubItems.Add(Dr("Satuan"))
						Lv.SubItems.Add(Dr("Rak"))
						Lv.SubItems.Add(Dr("Batch_Number"))
						Lv.SubItems.Add(Dr("Id_Warehouse"))
						Lv.SubItems.Add(Dr("Nomor_Pallet"))
						Lv.SubItems.Add(Dr("Warna"))

					Loop While Dr.Read
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show("Data Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_ScanBarcode.Text = ""
					Txt_ScanBarcode.Focus()
					Exit Sub
				End If
			End Using

			Txt_ScanBarcode.Text = ""
			Txt_ScanBarcode.Focus()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Lv_Data.Items.Count = 0 Then Exit Sub

		If Cmb_Lokasi.SelectedIndex = -1 Then
			MessageBox.Show("Lokasi Tidak Boleh Kosong", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Lokasi.Focus() : Exit Sub
		ElseIf Cmb_KualitasAwal.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu Kualitas Awal", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_KualitasAwal.Focus() : Exit Sub
		ElseIf Cmb_KualitasAkhir.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu Kualitas Akhir", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_KualitasAkhir.Focus() : Exit Sub
		ElseIf TxtKeterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Tidak Boleh Kosong", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKeterangan.Focus() : Exit Sub
		End If

		Dim berhasilSimpan As Boolean = False
		Dim kode_unik_print As String = ""
		Dim NoBarcode As Integer = 1

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			get_no_faktur()

			'==============================
			'=     INSERT TABEL INDUK     =
			'==============================
			SQL = "insert into Emi_TF_Quality (Kode_Perusahaan, No_Faktur, Quality_Awal, Quality_Tujuan, keterangan, Tanggal, Jam, UserID) values "
			SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & arrWarna(Cmb_KualitasAwal.SelectedIndex) & "', '" & arrWarna(Cmb_KualitasAkhir.SelectedIndex) & "', "
			SQL = SQL & "'" & TxtKeterangan.Text.Trim & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "') "
			ExecuteTrans(SQL)

			''======================================
			''=     DELETE Cetak_TransferStock     =
			''======================================
			'SQL = "delete from Cetak_TransferQuality where Kode_Perusahaan = '" & KodePerusahaan & "' "
			'ExecuteTrans(SQL)

			For i As Integer = 0 To Lv_Data.Items.Count - 1
				Get_LvData(i)

				'AMBIL SATUAN
				Dim satuanKecil As String = ""
				SQL = "select top 1 Satuan from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_barang = '" & Lv_KdBarang & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						satuanKecil = Dr("Satuan")
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Satuan Tidak Ditemukan", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'CONVERT SATUAN KECIL
				Dim jumlah_Kecil As Double = 0
				SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Lv_KdBarang & "', '" & Lv_Satuan & "',"
				SQL = SQL & "'" & satuanKecil & "', '" & HilangkanTanda(Lv_JumlahStock) & "' ) as hasil"
				Using Dr1 = OpenTrans(SQL)
					If Dr1.Read Then
						If General_Class.CekNULL(Dr1("hasil")) = "" Then
							Dr1.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("data konversi satuan kirim tidak ada ")
							Exit Sub
						End If

						jumlah_Kecil = Dr1("hasil")
					Else
						Dr1.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("data konversi satuan kirim tidak ada ")
						Exit Sub
					End If
				End Using

				'===============================
				'=     INSERT TABEL DETAIL     =
				'===============================
				SQL = "insert into Emi_TF_Quality_Detail (Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah, Jumlah_Bags, Satuan, Nilai_Barang, Satuan_Barang) values "
				SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & Lv_KdSo & "', '" & Lv_KdBarang & "', '" & Lv_JumlahStock & "', '" & Lv_JumlahBags & "', "
				SQL = SQL & "'" & Lv_Satuan & "', '" & jumlah_Kecil & "', '" & satuanKecil & "')"
				ExecuteTrans(SQL)

				''========================
				''=     POTONG STOCK     =
				''========================
				''POTONG STOCK DI BARANG
				'Dim Nama As String = ""
				'SQL = "select Nama, round(good_stock,2) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner = '" & Lv_KdSo & "' "
				'SQL = SQL & "and Kode_Barang='" & Lv_KdBarang & "' "
				'Using dr = OpenTrans(SQL)
				'    If dr.Read Then
				'        Nama = dr("nama")
				'        If dr("good_stock") < jumlah_Kecil Then
				'            dr.Close()
				'            CloseTrans()
				'            CloseConn()
				'            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
				'            Exit Sub
				'        ElseIf dr("Jumlah_Bags") < Lv_JumlahBags Then
				'            dr.Close()
				'            CloseTrans()
				'            CloseConn()
				'            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
				'            Exit Sub
				'        Else

				'            dr.Close()
				'            SQL = "update barang set Good_Stock = Good_Stock - " & jumlah_Kecil & ", Jumlah_Bags = Jumlah_Bags - " & Lv_JumlahBags & " "
				'            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Lv_KdSo & "' "
				'            SQL = SQL & " and Kode_Barang='" & Lv_KdBarang & "'"
				'            ExecuteTrans(SQL)
				'        End If
				'    Else
				'        dr.Close()
				'        CloseTrans()
				'        CloseConn()
				'        MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'        Exit Sub
				'    End If
				'End Using

				''POTONG STOCK DI BARANG_SN
				'SQL = "select round(jumlah,2) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Lv_KdSo & "' "
				'SQL = SQL & "and Kode_Barang='" & Lv_KdBarang & "' "
				'SQL = SQL & "and Serial_Number='" & Lv_Sn & "'"
				'Using dr = OpenTrans(SQL)
				'    If dr.Read Then
				'        If dr("jumlah") < jumlah_Kecil Then
				'            dr.Close()
				'            CloseTrans()
				'            CloseConn()
				'            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
				'            Exit Sub
				'        ElseIf dr("Jumlah_Bags") < Lv_JumlahBags Then
				'            dr.Close()
				'            CloseTrans()
				'            CloseConn()
				'            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
				'            Exit Sub
				'        Else
				'            dr.Close()
				'            SQL = "update barang_sn set jumlah = jumlah - " & jumlah_Kecil & ", Jumlah_Bags = Jumlah_Bags - " & Lv_JumlahBags & " "
				'            SQL = SQL & "where Kode_Stock_Owner='" & Lv_KdSo & "' and Kode_Barang='" & Lv_KdBarang & "' "
				'            SQL = SQL & "and Serial_Number='" & Lv_Sn & "'"
				'            ExecuteTrans(SQL)
				'        End If
				'    Else
				'        dr.Close()
				'        CloseTrans()
				'        CloseConn()
				'        MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'        Exit Sub
				'    End If
				'End Using

				'' CEK KESESUAIAN STOCK
				'SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
				'SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
				'SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
				'SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
				'SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
				'SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
				'SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Lv_KdSo & "' "
				'SQL = SQL & "AND a.Kode_Barang = '" & Lv_KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
				'SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
				'Using Ds = BindingTrans(SQL)
				'    With Ds.Tables("MyTable")
				'        If .Rows.Count <> 0 Then
				'            If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
				'                CloseTrans()
				'                CloseConn()
				'                MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                Exit Sub
				'            End If
				'        Else
				'            CloseTrans()
				'            CloseConn()
				'            MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'            Exit Sub
				'        End If
				'    End With
				'End Using

				''========================
				''=     TAMBAH STOCK     =
				''========================

				''GET WAREHOUSE KOSONG
				'Dim Id_WarehouseTujuan, NoPalletTujuan As String
				'SQL = "SELECT TOP(1) "
				'SQL = SQL & "a.id_wms_warehouse_position, b.nomor_urut "
				'SQL = SQL & "FROM view_warehouse_position a, view_warehouse_position_detail b "
				'SQL = SQL & "WHERE a.Id_WMS_Warehouse_Position = b.Id_WMS_Warehouse_Position "
				'SQL = SQL & "AND a.kode_Perusahaan = b.kode_Perusahaan "
				'SQL = SQL & "AND a.kode_Perusahaan = '" & KodePerusahaan & "' "
				'SQL = SQL & "AND a.Kode_Stock_Owner = '" & Lv_KdSo & "' "
				'SQL = SQL & "AND b.Kode_Barang IS NULL;"
				'Using Dr = OpenTrans(SQL)
				'    If Dr.Read Then
				'        Id_WarehouseTujuan = Dr("id_wms_warehouse_position")
				'        NoPalletTujuan = Dr("nomor_urut")
				'    Else
				'        Dr.Close()
				'        CloseTrans()
				'        CloseConn()
				'        MessageBox.Show("Pallet Kosong Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'        Exit Sub
				'    End If
				'End Using

				'Dim hargaIsn As String = Get_Harga_SN(Lv_Sn)

				''GENERATE SN BARU
				'Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
				'Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
				'Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

				'Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

				''INSERT BARANG SN BARU
				'SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
				'SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk) "
				'SQL = SQL & "select Kode_Perusahaan, '" & Lv_KdSo & "', Kode_Barang, '" & SN_Baru & "', '" & jumlah_Kecil & "', " & Lv_JumlahBags & ", "
				'SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & Id_WarehouseTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
				'SQL = SQL & "Kode_Unik_Asal, '" & NoPalletTujuan & "', batch_number, '" & arrWarna(Cmb_KualitasAkhir.SelectedIndex) & "', Tgl_Masuk "
				'SQL = SQL & "from Barang_SN "
				'SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
				'SQL = SQL & "and Kode_Stock_Owner='" & Lv_KdSo & "' "
				'SQL = SQL & "and Kode_Barang='" & Lv_KdBarang & "' "
				'SQL = SQL & "and Serial_Number='" & Lv_Sn & "' "
				'ExecuteTrans(SQL)

				''============================
				''=       TAMBAH BARANG       =
				''============================

				'SQL = "update barang set Good_Stock= Good_Stock + " & jumlah_Kecil & ", Jumlah_Bags = Jumlah_Bags + " & Lv_JumlahBags & " "
				'SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Lv_KdSo & "' "
				'SQL = SQL & " and Kode_Barang='" & Lv_KdBarang & "'"
				'ExecuteTrans(SQL)

				''CEK KESESUAIAN STOCK
				'SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
				'SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
				'SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
				'SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
				'SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
				'SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
				'SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Lv_KdSo & "' "
				'SQL = SQL & "AND a.Kode_Barang = '" & Lv_KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
				'SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
				'Using Ds = BindingTrans(SQL)
				'    With Ds.Tables("MyTable")
				'        If .Rows.Count <> 0 Then
				'            If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
				'                CloseTrans()
				'                CloseConn()
				'                MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                Exit Sub
				'            End If
				'        Else
				'            CloseTrans()
				'            CloseConn()
				'            MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'            Exit Sub
				'        End If
				'    End With
				'End Using

				'GET URUT Emi_TF_Quality_Detail
				Dim x_urut_TF_Quality_Detail As Integer = 0
				SQL = "select IDENT_CURRENT('Emi_TF_Quality_Detail') as urut"
				Using Dr1 = OpenTrans(SQL)
					If Dr1.Read Then
						x_urut_TF_Quality_Detail = Dr1("urut")
					End If
				End Using

				'CEK Emi_TF_Quality_Detail
				SQL = "select No_Faktur from Emi_TF_Quality_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtNo_Transaksi.Text.Trim & "' "
				SQL = SQL & "and Urut_Oto = '" & x_urut_TF_Quality_Detail & "'"
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terdapat Masalah Saat Simpan, Harap Ulangi Transaksi", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = "select distinct b.Kode_Stock_Owner, b.Kode_Barang, b.Serial_Number, b.Jumlah, b.Jumlah_Bags "
				SQL &= $"from barang a "
				SQL &= $"inner join Barang_SN b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
				SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and b.Jumlah <> 0 "
				SQL &= $"and (b.Qr_Code+'-'+b.Kode_Unik_Berjalan) = '{Lv_Barcode}' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For j As Integer = 0 To .Rows.Count - 1

								Dim Kdso As String = .Rows(j).Item("Kode_Stock_Owner")
								Dim KdBarang As String = .Rows(j).Item("Kode_Barang")
								Dim Sn As String = .Rows(j).Item("Serial_Number")
								Dim jumlah As Double = Val(HilangkanTanda(.Rows(j).Item("Jumlah")))
								Dim jumlah_Bags As Double = Val(HilangkanTanda(.Rows(j).Item("Jumlah_Bags")))

								SQL = "update barang_sn set Warna =  '" & arrWarna(Cmb_KualitasAkhir.SelectedIndex) & "' "
								SQL = SQL & "where Kode_Stock_Owner='" & Kdso & "' and Kode_Barang='" & KdBarang & "' "
								SQL = SQL & "and Serial_Number='" & Sn & "'"
								ExecuteTrans(SQL)

								'============================
								'=     INSERT TABEL DET     =
								'============================
								SQL = "insert into Emi_TF_Quality_Det (Kode_Perusahaan, No_Faktur, Serial_Number_Awal, Serial_Number_Akhir, Batch_Number, Id_Warehouse_Awal, "
								SQL = SQL & "id_Pallet_Awal, Id_Warehouse_Akhir, id_Pallet_Akhir, Warna_Awal, Warna_Akhir, Urut_TF, Jumlah, Jumlah_Bags) values "
								SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text.Trim & "', '" & Sn & "', '" & Sn & "', '" & Lv_Batch & "', "
								SQL = SQL & "'" & Lv_IdWarehouse & "', '" & Lv_NoPallet & "', '" & Lv_IdWarehouse & "', '" & Lv_NoPallet & "', '" & Lv_Warna & "', "
								SQL = SQL & "'" & arrWarna(Cmb_KualitasAkhir.SelectedIndex) & "', '" & x_urut_TF_Quality_Detail & "', '" & jumlah & "', '" & jumlah_Bags & "')"
								ExecuteTrans(SQL)

							Next
						Else
							CloseTrans()
							CloseConn()
							MessageBox.Show("Data Barang Tidak Ditemukan", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End With
				End Using

				''=====================================
				''=       GENERATE BARCODE BARU       =
				''=====================================

				'kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
				'Dim fullNewQr As String = Lv_QrCode & "-" & newKodeUnikBerjalan

				'Barcode.Image = Generate_QR(fullNewQr)

				'Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfQuality" & kode_unik_print & ".jpg")
				''If Not (System.IO.File.Exists(FileToSaveAs1)) Then
				'Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
				''End If

				'fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
				'FileSize1 = fs1.Length
				'rawData1 = New Byte(FileSize1) {}
				'fs1.Read(rawData1, 0, FileSize1)
				'fs1.Close()
				'Cmd.Parameters.Add("@newBarcode" & NoBarcode, SqlDbType.Image).Value = rawData1

				''==========================
				''=     INSERT BARCODE     =
				''==========================
				'Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)
				'SQL = "insert into Cetak_TransferQuality (Kode_Perusahaan, No_Faktur, Kode_Barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, Tanggal_Cetak, Kode_Unik_Print, Tanggal_Masuk, Metode_Pengeluaran_Stok) values "
				'SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text.Trim & "', '" & Lv_KdBarang & "', @newBarcode" & NoBarcode & ", '" & Lv_NmBarang & "', "
				'SQL = SQL & "'" & fullNewQr & "', '" & Lv_QrCode & "', '" & Lv_TglExp & "', '" & Lv_Batch & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
				'SQL = SQL & "'" & kode_unik_print & "', '" & Lv_TglMsk & "', '" & Lv_MetodePengeluaran & "')"
				'ExecuteTrans(SQL)

				NoBarcode += 1
			Next

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Berhasil Disimpan", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Information)
			berhasilSimpan = True
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'If berhasilSimpan Then
		'    Cetak(kode_unik_print)
		'Else
		'    MessageBox.Show("Gagal Simpan", JudulMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'End If

		Kosong()
	End Sub

	Private Sub Cetak(ByVal kode_unik_print As String)
		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim kertas As String = ""

			'=========================
			'=     CETAK BARCODE     =
			'=========================
			Dim PrinterBarcode As String = "TSC TE210"

			SQL = "select Kode_Perusahaan, QrUtuh from Cetak_TransferQuality where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtNo_Transaksi.Text.Trim & "' and Kode_Unik_Print = '" & kode_unik_print & "'"
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then
					For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
						CrDoc = New NewBarcodeTransferQuality
						With A_Place_For_Printing2
							CrDoc.SetDataSource(Ds)
							CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
							CrDoc.PrintOptions.PrinterName = ""
							CrDoc.RecordSelectionFormula = "{Cetak_TransferQuality.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferQuality.kode_unik_print} = '" & kode_unik_print & "' and {Cetak_TransferQuality.QrUtuh} = '" & Ds.Tables("MyTable").Rows(i).Item("QrUtuh") & "' "
							CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Stock"
							.Text = "New Barcode Transfer Quality"
							.CrystalReportViewer1.ReportSource = CrDoc
							.Refresh()
							.Show()
						End With

						'===========================================================================================================================================================================================

						'CrDoc.SetDataSource(Ds)
						'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						'CrDoc.RecordSelectionFormula = "{Cetak_TransferQuality.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferQuality.kode_unik_print} = '" & kode_unik_print & "' "

						'CrDoc.PrintOptions.PrinterName = PrinterBarcode

						'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
						'doctoprint.PrinterSettings.PrinterName = PrinterBarcode
						'CrDoc.PrintToPrinter(1, False, 1, 2500)
					Next
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Lv_Data_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Data.KeyDown
		If Lv_Data.Items.Count = 0 Then Exit Sub

		Dim currentRow = Lv_Data.FocusedItem.Index

		If e.KeyCode = Keys.Delete Then

			Dim Hapus1 As String = MessageBox.Show("Apakah Anda Yakin, Ingin Menghapus Data ?", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If Hapus1 = vbNo Then Exit Sub

			Lv_Data.Items.RemoveAt(currentRow)
		End If
	End Sub

	'asd

End Class