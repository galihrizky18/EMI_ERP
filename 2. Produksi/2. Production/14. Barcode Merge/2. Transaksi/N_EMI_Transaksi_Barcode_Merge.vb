Imports System.IO

Public Class N_EMI_Transaksi_Barcode_Merge

	Dim arrKodeStockOwner, arrInisialFaktur As New ArrayList

	Dim Random As New Random()

	Dim Dgv_Tab_1_TglProduksi, Dgv_Tab_1_TglExpired, Dgv_Tab_1_TglMasuk, Dgv_Tab_1_Serial_Number, Dgv_Tab_1_Barcode,
		Dgv_Tab_1_Jumlah, Dgv_Tab_1_Satuan, Dgv_Tab_1_Chkbox, Dgv_Tab_1_Jumlah_Input,
		Dgv_Tab_1_Qr_Code, Dgv_Tab_1_Kode_Unik_Berjalan, Dgv_Tab_1_Kode_Unik_Asal As String

	Dim Tab_1_Cell_TglProduksi As Integer = 0
	Dim Tab_1_Cell_TglExpired As Integer = 1
	Dim Tab_1_Cell_TglMasuk As Integer = 2
	Dim Tab_1_Cell_Serial_Number As Integer = 3
	Dim Tab_1_Cell_Barcode As Integer = 4
	Dim Tab_1_Cell_Jumlah As Integer = 5
	Dim Tab_1_Cell_Satuan As Integer = 6
	Dim Tab_1_Cell_Chkbox As Integer = 7
	Dim Tab_1_Cell_Jumlah_Input As Integer = 8
	Dim Tab_1_Cell_Qr_Code As Integer = 9
	Dim Tab_1_Cell_Kode_Unik_Berjalan As Integer = 10
	Dim Tab_1_Cell_Kode_Unik_Asal As Integer = 11

	Dim Dgv_Tab_3_KdSo, Dgv_Tab_3_KdBarang, Dgv_Tab_3_NmBarang, Dgv_Tab_3_QrCode,
		Dgv_Tab_3_Kd_Unik_Berjalan_Lama, Dgv_Tab_3_Kd_Unik_Berjalan_Baru,
		Dgv_Tab_3_Barcode_Lama, Dgv_Tab_3_Barcode_Baru,
		Dgv_Tab_3_Jumlah, Dgv_Tab_3_Satuan As String

	Dim Tab_3_Cell_KdSo As Integer = 0
	Dim Tab_3_Cell_KdBarang As Integer = 1
	Dim Tab_3_Cell_NmBarang As Integer = 2
	Dim Tab_3_Cell_QrCode As Integer = 3
	Dim Tab_3_Cell_Kd_Unik_Berjalan_Lama As Integer = 4
	Dim Tab_3_Cell_Kd_Unik_Berjalan_Baru As Integer = 5
	Dim Tab_3_Cell_Barcode_Lama As Integer = 6
	Dim Tab_3_Cell_Barcode_Baru As Integer = 7
	Dim Tab_3_Cell_Jumlah As Integer = 8
	Dim Tab_3_Cell_Satuan As Integer = 9

	Dim Dgv_Tab_2_KdSo, Dgv_Tab_2_TglProduksi, Dgv_Tab_2_TglExpired, Dgv_Tab_2_TglMasuk,
		Dgv_Tab_2_KdBarang, Dgv_Tab_2_NmBarang, Dgv_Tab_2_Barcode,
		Dgv_Tab_2_Jumlah, Dgv_Tab_2_Satuan, Dgv_Tab_2_NoBarcode, Dgv_Tab_2_Qr_Code, Dgv_Tab_2_Kd_Unik_Berjalan, Dgv_Tab_2_First_Expired_Barcode As String

	Dim Tab_2_Cell_KdSo As Integer = 0
	Dim Tab_2_Cell_TglProduksi As Integer = 1
	Dim Tab_2_Cell_TglExpired As Integer = 2
	Dim Tab_2_Cell_TglMasuk As Integer = 3
	Dim Tab_2_Cell_KdBarang As Integer = 4
	Dim Tab_2_Cell_NmBarang As Integer = 5

	Private Sub Cmb_Kode_Stock_Owner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kode_Stock_Owner.SelectedIndexChanged
		If Cmb_Kode_Stock_Owner.Items.Count = 0 Then Exit Sub
		If Cmb_Kode_Stock_Owner.SelectedIndex = -1 Then Exit Sub

		Try
			OpenConn()

			get_no_faktur()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Dim Tab_2_Cell_Barcode As Integer = 6
	Dim Tab_2_Cell_Jumlah As Integer = 7
	Dim Tab_2_Cell_Satuan As Integer = 8
	Dim Tab_2_Cell_NoBarcode As Integer = 9
	Dim Tab_2_Cell_Qr_code As Integer = 10
	Dim Tab_2_Cell_Kd_Unik_Berjalan As Integer = 11
	Dim Tab_2_Cell_First_Expired_Barcode As Integer = 12

	Dim switch_Auto_Complete As Boolean = False

	Private Sub get_no_faktur()
		Dim FPro_Results As String = "MR-"
		TxtNo_Transaksi.Text = FPro_Results & arrInisialFaktur.Item(Cmb_Kode_Stock_Owner.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
									  General_Class.Get_Last_Number2("N_EMI_Transaksi_Barcode_Merge", "no_faktur", JumlahDigit,
									  "Kode_perusahaan", KodePerusahaan,
									  "And", "substring(no_faktur,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(Cmb_Kode_Stock_Owner.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(Cmb_Kode_Stock_Owner.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

	End Sub

	Private Sub N_EMI_Transaksi_Barcode_Merge_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub N_EMI_Transaksi_Barcode_Merge_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
		Lv_Barang.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
		Lv_Barang.View = View.Details

		DGV_Data_Rekap_Barcode.Columns(Tab_2_Cell_NoBarcode).DisplayIndex = 1

		Try
			OpenConn()

			'=================================
			'=     LOAD KODE STOCK OWNER     =
			'=================================
			Cmb_Kode_Stock_Owner.Items.Clear() : arrKodeStockOwner.Clear() : arrInisialFaktur.Clear()
			SQL = "select kode_stock_owner, Keterangan, inisial_faktur from Stock_Owner_Gudang "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"order by kode_Stock_owner"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Kode_Stock_Owner.Items.Add(Dr("Keterangan")) : arrKodeStockOwner.Add(Dr("kode_stock_owner")) : arrInisialFaktur.Add(Dr("inisial_faktur"))
				Loop
			End Using

			'=======================
			'=     LOAD SATUAN     =
			'=======================
			Cmb_Satuan_Barang.Items.Clear()
			SQL = "select satuan from EMI_Satuan "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"order by satuan "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Satuan_Barang.Items.Add(Dr("satuan").ToString.Trim)
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

	Private Sub Kosong()

		'Try
		'    OpenConn()

		'    get_no_faktur()

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		TxtKeterangan.Text = ""
		Cmb_Kode_Stock_Owner.SelectedIndex = -1
		Cmb_Kode_Stock_Owner.Enabled = True

		Kosong_Tab_1()
		Kosong_Tab_2()
		Kosong_Tab_3()

		TabControl1.SelectedIndex = 0

	End Sub

	Private Sub Kosong_Tab_1()
		switch_Auto_Complete = True
		TxtKd_Barang.Text = ""
		Txt_Nm_Barang.Text = ""
		Txt_Stock.Text = ""
		switch_Auto_Complete = False

		Txt_QR.Enabled = False

		Cmb_Satuan_Barang.SelectedIndex = -1

		Txt_QR.Text = ""

		DGV_Data_Barcode.Rows.Clear()

		Txt_Total_Stock.Text = 0
		Txt_Total_Merge.Text = 0

	End Sub

	Private Sub Kosong_Tab_3()
		DGV_Data_Rekap_Barcode.Rows.Clear()
	End Sub

	Private Sub Kosong_Tab_2()
		DGV_Data_Detail_Barcode.Rows.Clear()
	End Sub

	Private Sub Get_Dgv_Tab_1(index As Integer)
		Dgv_Tab_1_TglProduksi = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_TglProduksi).Value
		Dgv_Tab_1_TglExpired = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_TglExpired).Value
		Dgv_Tab_1_TglMasuk = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_TglMasuk).Value
		Dgv_Tab_1_Serial_Number = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_Serial_Number).Value
		Dgv_Tab_1_Barcode = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_Barcode).Value
		Dgv_Tab_1_Jumlah = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_Jumlah).Value
		Dgv_Tab_1_Satuan = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_Satuan).Value
		Dgv_Tab_1_Chkbox = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_Chkbox).Value
		Dgv_Tab_1_Jumlah_Input = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_Jumlah_Input).Value
		Dgv_Tab_1_Qr_Code = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_Qr_Code).Value
		Dgv_Tab_1_Kode_Unik_Berjalan = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_Kode_Unik_Berjalan).Value
		Dgv_Tab_1_Kode_Unik_Asal = DGV_Data_Barcode.Rows(index).Cells(Tab_1_Cell_Kode_Unik_Asal).Value

	End Sub

	Private Sub Get_Dgv_Tab_2(index As Integer)
		Dgv_Tab_2_KdSo = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_KdSo).Value
		Dgv_Tab_2_TglProduksi = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_TglProduksi).Value
		Dgv_Tab_2_TglExpired = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_TglExpired).Value
		Dgv_Tab_2_TglMasuk = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_TglMasuk).Value
		Dgv_Tab_2_KdBarang = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_KdBarang).Value
		Dgv_Tab_2_NmBarang = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_NmBarang).Value
		Dgv_Tab_2_Barcode = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_Barcode).Value
		Dgv_Tab_2_Jumlah = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_Jumlah).Value
		Dgv_Tab_2_Satuan = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_Satuan).Value
		Dgv_Tab_2_NoBarcode = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_NoBarcode).Value
		Dgv_Tab_2_Qr_Code = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_Qr_code).Value
		Dgv_Tab_2_Kd_Unik_Berjalan = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_Kd_Unik_Berjalan).Value
		Dgv_Tab_2_First_Expired_Barcode = DGV_Data_Rekap_Barcode.Rows(index).Cells(Tab_2_Cell_First_Expired_Barcode).Value
	End Sub

	Private Sub Get_Dgv_Tab_3(index As Integer)
		Dgv_Tab_3_KdSo = DGV_Data_Detail_Barcode.Rows(index).Cells(Tab_3_Cell_KdSo).Value
		Dgv_Tab_3_KdBarang = DGV_Data_Detail_Barcode.Rows(index).Cells(Tab_3_Cell_KdBarang).Value
		Dgv_Tab_3_NmBarang = DGV_Data_Detail_Barcode.Rows(index).Cells(Tab_3_Cell_NmBarang).Value
		Dgv_Tab_3_QrCode = DGV_Data_Detail_Barcode.Rows(index).Cells(Tab_3_Cell_QrCode).Value
		Dgv_Tab_3_Kd_Unik_Berjalan_Lama = DGV_Data_Detail_Barcode.Rows(index).Cells(Tab_3_Cell_Kd_Unik_Berjalan_Lama).Value
		Dgv_Tab_3_Kd_Unik_Berjalan_Baru = DGV_Data_Detail_Barcode.Rows(index).Cells(Tab_3_Cell_Kd_Unik_Berjalan_Baru).Value
		Dgv_Tab_3_Barcode_Lama = DGV_Data_Detail_Barcode.Rows(index).Cells(Tab_3_Cell_Barcode_Lama).Value
		Dgv_Tab_3_Barcode_Baru = DGV_Data_Detail_Barcode.Rows(index).Cells(Tab_3_Cell_Barcode_Baru).Value
		Dgv_Tab_3_Jumlah = DGV_Data_Detail_Barcode.Rows(index).Cells(Tab_3_Cell_Jumlah).Value
		Dgv_Tab_3_Satuan = DGV_Data_Detail_Barcode.Rows(index).Cells(Tab_3_Cell_Satuan).Value
	End Sub

	Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged
		If switch_Auto_Complete Then Exit Sub

		If Cmb_Kode_Stock_Owner.SelectedIndex = -1 Then
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(1189, 224)

			MessageBox.Show("Harap pilih dahulu gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			switch_Auto_Complete = True
			TxtKd_Barang.Text = ""
			Txt_Nm_Barang.Text = ""
			switch_Auto_Complete = False
			Cmb_Kode_Stock_Owner.DroppedDown = True
			Cmb_Kode_Stock_Owner.Focus()

			Exit Sub
		End If

		If TxtKd_Barang.Text.Trim.Length = 0 Then
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(1189, 224)
			TxtKd_Barang.Text = ""
			Txt_Nm_Barang.Text = ""
			Cmb_Satuan_Barang.Text = ""
			Exit Sub
		Else
			Lv_Barang.Location = New Point(34, 224)
			Lv_Barang.Visible = True
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()
			SQL = "select top 20 kode_barang, nama, isnull(good_Stock, 0) as good_Stock, satuan "
			SQL &= $"from barang "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and Kode_Stock_Owner = '{arrKodeStockOwner(Cmb_Kode_Stock_Owner.SelectedIndex)}' "
			SQL &= $"and kode_barang like '%{TxtKd_Barang.Text.Trim}%' "
			SQL &= $"order by Kode_Barang, Nama "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Barang.Items.Add(Dr("kode_barang"))
					Lv.SubItems.Add(Dr("nama"))
					Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("good_Stock"))), "N4"))
					Lv.SubItems.Add(Dr("satuan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub TxtKd_Barang_Leave(sender As Object, e As EventArgs) Handles TxtKd_Barang.Leave
		If TxtKd_Barang.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Barang.Focused = True Then Exit Sub

		Try
			OpenConn()

			DGV_Data_Barcode.Rows.Clear()
			SQL = "select top 1 kode_barang, nama, good_Stock, satuan "
			SQL &= $"from barang "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and Kode_Stock_Owner = '{arrKodeStockOwner(Cmb_Kode_Stock_Owner.SelectedIndex)}' "
			SQL &= $"and kode_barang = '{TxtKd_Barang.Text.Trim}' "
			SQL &= $"order by Kode_Barang, Nama "
			Using Dr = Open(SQL)
				If Dr.Read Then
					TxtKd_Barang.Text = Dr("kode_barang")
					Txt_Nm_Barang.Text = Dr("nama")
					Txt_Stock.Text = Format(Dr("good_Stock"), "N4")
					Cmb_Satuan_Barang.SelectedItem = Dr("satuan").ToString.Trim
					Txt_QR.Enabled = True
					Btn_GetData.Focus()
				Else
					MessageBox.Show("Kode Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					TxtKd_Barang.Text = ""
					Txt_Nm_Barang.Text = ""
					Txt_Stock.Text = ""
					Cmb_Satuan_Barang.SelectedIndex = -1
					Txt_QR.Enabled = False
					TxtKd_Barang.Focus()
				End If

				Lv_Barang.Visible = False
				Lv_Barang.Location = New Point(1189, 224)
			End Using

			Txt_QR.Text = ""

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		If MessageBox.Show("Yakin ingin melakukan refresh semua data yang telah diinput?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Kosong()
	End Sub

	Private Sub TxtKd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKd_Barang.KeyPress
		If e.KeyChar = Chr(13) Then
			If TxtKd_Barang.Text.Trim.Length = 0 Then TxtKd_Barang.Focus()
			TxtKd_Barang_Leave(TxtKd_Barang, e)

			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(1189, 224)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub TxtKd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKd_Barang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

		Dim kdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text
		Dim Jumlah As String = Lv_Barang.FocusedItem.SubItems(2).Text
		Dim Satuan As String = Lv_Barang.FocusedItem.SubItems(3).Text

		switch_Auto_Complete = True
		TxtKd_Barang.Text = kdBarang
		Txt_Nm_Barang.Text = NmBarang
		Txt_Stock.Text = Format(Val(HilangkanTanda(Jumlah)), "N4")
		Cmb_Satuan_Barang.SelectedItem = Satuan.ToString.Trim
		switch_Auto_Complete = False

		DGV_Data_Barcode.Rows.Clear()
		Txt_QR.Enabled = True
		Txt_QR.Text = ""

		Lv_Barang.Visible = False
		Lv_Barang.Location = New Point(1189, 224)

		Btn_GetData.Focus()
	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Barang_DoubleClick(Lv_Barang, e)
		End If
	End Sub

	Private Sub Btn_GetData_Click(sender As Object, e As EventArgs) Handles Btn_GetData.Click
		If Cmb_Kode_Stock_Owner.SelectedIndex = -1 Then
			MessageBox.Show("Harap pilih dahulu gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Cmb_Kode_Stock_Owner.DroppedDown = True
			Cmb_Kode_Stock_Owner.Focus()
			Exit Sub
		ElseIf TxtKd_Barang.Text.Trim.Length = 0 Then
			MessageBox.Show("Harap isi kode barang terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			TxtKd_Barang.Focus()
			Exit Sub
		End If

		Try
			OpenConn()

			Cmb_Kode_Stock_Owner.Enabled = False

			Dim Total_Stok As Double = 0

			Dim rows As Integer = 0
			DGV_Data_Barcode.Rows.Clear()

			SQL = "select top 40 a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, 'X' as Serial_Number, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, "
			SQL &= $"sum(a.Jumlah) as Jumlah, b.Satuan, "
			SQL &= $"a.qr_code, a.Kode_Unik_Berjalan, a.Tgl_Produksi, a.Tgl_Expired, a.Tgl_Masuk "
			SQL &= $"from barang_sn a "
			SQL &= $"inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Kode_Stock_Owner = '{arrKodeStockOwner(Cmb_Kode_Stock_Owner.SelectedIndex)}' "
			SQL &= $"and a.Kode_Barang = '{TxtKd_Barang.Text}' "
			SQL &= $"and a.Blok_SN is null "
			SQL &= $"and a.Jumlah <> 0 "
			SQL &= $"group by a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan), b.Satuan, a.qr_code, "
			SQL &= $"a.Kode_Unik_Berjalan, a.Tgl_Produksi, a.Tgl_Expired, a.Tgl_Masuk, b.Metode_Pengeluaran_Stok "
			SQL &= $"order by case "
			SQL &= $"when b.Metode_Pengeluaran_Stok='FIFO' then a.Tgl_Masuk "
			SQL &= $"Else a.Tgl_Expired End "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					DGV_Data_Barcode.Rows.Add(1)
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_TglProduksi).Value = If(General_Class.CekNULL(Dr("Tgl_Produksi")) = "", "-", Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_TglExpired).Value = If(General_Class.CekNULL(Dr("Tgl_Expired")) = "", "-", Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_TglMasuk).Value = If(General_Class.CekNULL(Dr("Tgl_Masuk")) = "", "-", Format(Dr("Tgl_Masuk"), "dd MMM yyyy"))

					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Serial_Number).Value = If(General_Class.CekNULL(Dr("Serial_Number")) = "", "-", Dr("Serial_Number"))
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Barcode).Value = If(General_Class.CekNULL(Dr("Barcode")) = "", "-", Dr("Barcode"))
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Jumlah).Value = If(General_Class.CekNULL(Dr("Jumlah")) = "", "-", Format(Dr("Jumlah"), "N4"))
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Satuan).Value = If(General_Class.CekNULL(Dr("Satuan")) = "", "-", Dr("Satuan"))
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Chkbox).Value = False
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Jumlah_Input).Value = Format(0, "N4")
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Qr_Code).Value = If(General_Class.CekNULL(Dr("qr_code")) = "", "-", Dr("qr_code"))
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Kode_Unik_Berjalan).Value = If(General_Class.CekNULL(Dr("Kode_Unik_Berjalan")) = "", "-", Dr("Kode_Unik_Berjalan"))
					DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Kode_Unik_Asal).Value = "" 'If(General_Class.CekNULL(Dr("Kode_Unik_Asal")) = "", "-", Dr("Kode_Unik_Asal"))

					Total_Stok += If(General_Class.CekNULL(Dr("Jumlah")) = "", 0, Val(HilangkanTanda(Dr("Jumlah"))))
					rows += 1

				Loop
			End Using

			Txt_Total_Stock.Text = Format(Total_Stok, "N4")

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub DGV_Data_Barcode_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_Barcode.CellEndEdit
		If DGV_Data_Barcode.Rows.Count = 0 Then Exit Sub

		If DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Chkbox).Value = "True" Then

			DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).ReadOnly = False

			Dim currentColumn As Integer = DGV_Data_Barcode.CurrentCell.ColumnIndex
			Dim cellValue As Object = DGV_Data_Barcode.CurrentRow.Cells(currentColumn).Value

			If currentColumn = Tab_1_Cell_Jumlah_Input Then
				If Not IsNumeric(cellValue) Then
					DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).Value = Format(0, "N4")
					GetGrandTotal()
					Exit Sub
				End If
			End If

			Dim jumlahStock As Double = Val(HilangkanTanda(DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah).Value))
			Dim jumlahInput As Double = 0

			If Val(HilangkanTanda(DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).Value)) = 0 Then
				jumlahInput = jumlahStock
			Else
				jumlahInput = Val(HilangkanTanda(DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).Value))
			End If

			If jumlahInput < 0 Then
				MessageBox.Show("Jumlah Input Tidak Boleh Minus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).Value = Format(0, "N4")
				GetGrandTotal()
				Exit Sub
			End If

			If jumlahInput > jumlahStock Then
				MessageBox.Show("Jumlah Tidak Boleh Melebihi Stock ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).Value = Format(0, "N4")
				Exit Sub
			End If

			DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).Value = Format(jumlahInput, "N4")

			If jumlahStock <> jumlahInput Then
				DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).Style.BackColor = Color.LightGray
			Else
				DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).Style.BackColor = Color.LightSalmon
			End If
		Else

			DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).Style.BackColor = Color.White

			DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).Value = Format(0, "N4")
			DGV_Data_Barcode.CurrentRow.Cells(Tab_1_Cell_Jumlah_Input).ReadOnly = True

		End If

		DGV_Data_Barcode.CurrentRow.DefaultCellStyle.BackColor = Color.White

		GetGrandTotal()

	End Sub

	Private Sub GetGrandTotal()
		If DGV_Data_Barcode.Rows.Count = 0 Then Exit Sub

		Dim total As Double = 0
		Dim request As Double = 0

		For i As Integer = 0 To DGV_Data_Barcode.Rows.Count - 1
			Get_Dgv_Tab_1(i)
			If DGV_Data_Barcode.Rows(i).Cells(Tab_1_Cell_Chkbox).Value = "False" Then
				Continue For
			End If

			total = total + Val(HilangkanTanda(DGV_Data_Barcode.Rows(i).Cells(Tab_1_Cell_Jumlah_Input).Value))

		Next

		Txt_Total_Merge.Text = Format(total, "N4")

	End Sub

	Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click
		If DGV_Data_Barcode.Rows.Count = 0 Then Exit Sub
		If Txt_QR.Text.Trim.Length = 0 Then
			MessageBox.Show("Harap isi dahulu Barcode yang akan discan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Txt_QR.Focus()
			Exit Sub
		End If

		Dim foundMatch As Boolean = False
		Dim keyword As String = Txt_QR.Text.Trim().ToUpper()

		For i As Integer = 0 To DGV_Data_Barcode.Rows.Count - 1
			Get_Dgv_Tab_1(i)

			If Dgv_Tab_1_Barcode.Trim().ToUpper().Contains(keyword) Then

				'DGV_Data_TF.Rows(i).Cells(itemDgvCheckBox).Value = "True"
				DGV_Data_Barcode.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
				DGV_Data_Barcode.Rows(i).DefaultCellStyle.ForeColor = Color.Black

				If Not foundMatch Then
					DGV_Data_Barcode.FirstDisplayedScrollingRowIndex = i ' Scroll ke hasil pertama
				End If

				foundMatch = True

			End If

		Next

		If Not foundMatch Then

			Try
				OpenConn()

				Dim rows As Integer = DGV_Data_Barcode.Rows.Count

				SQL = "select top 40 a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, 'X' as Serial_Number, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, "
				SQL &= $"sum(a.Jumlah) as Jumlah, b.Satuan, "
				SQL &= $"a.qr_code, a.Kode_Unik_Berjalan, a.Tgl_Produksi, a.Tgl_Expired, a.Tgl_Masuk "
				SQL &= $"from barang_sn a "
				SQL &= $"inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
				SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and a.Kode_Stock_Owner = '{arrKodeStockOwner(Cmb_Kode_Stock_Owner.SelectedIndex)}' "
				SQL &= $"and a.Kode_Barang = '{TxtKd_Barang.Text}' "
				SQL &= $"and (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) like '%{Txt_QR.Text.Trim().ToUpper()}%' "
				SQL &= $"and a.Blok_SN is null "
				SQL &= $"and a.Jumlah <> 0 "
				SQL &= $"group by a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan), b.Satuan, a.qr_code, "
				SQL &= $"a.Kode_Unik_Berjalan, a.Tgl_Produksi, a.Tgl_Expired, a.Tgl_Masuk, b.Metode_Pengeluaran_Stok "
				SQL &= $"order by case "
				SQL &= $"when b.Metode_Pengeluaran_Stok='FIFO' then a.Tgl_Masuk "
				SQL &= $"Else a.Tgl_Expired End "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						DGV_Data_Barcode.Rows.Add(1)
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_TglProduksi).Value = If(General_Class.CekNULL(Dr("Tgl_Produksi")) = "", "-", Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_TglExpired).Value = If(General_Class.CekNULL(Dr("Tgl_Expired")) = "", "-", Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_TglMasuk).Value = If(General_Class.CekNULL(Dr("Tgl_Masuk")) = "", "-", Format(Dr("Tgl_Masuk"), "dd MMM yyyy"))

						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Serial_Number).Value = If(General_Class.CekNULL(Dr("Serial_Number")) = "", "-", Dr("Serial_Number"))
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Barcode).Value = If(General_Class.CekNULL(Dr("Barcode")) = "", "-", Dr("Barcode"))
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Jumlah).Value = If(General_Class.CekNULL(Dr("Jumlah")) = "", "-", Format(Dr("Jumlah"), "N4"))
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Satuan).Value = If(General_Class.CekNULL(Dr("Satuan")) = "", "-", Dr("Satuan"))
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Chkbox).Value = False
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Jumlah_Input).Value = Format(0, "N4")
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Qr_Code).Value = If(General_Class.CekNULL(Dr("qr_code")) = "", "-", Dr("qr_code"))
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Kode_Unik_Berjalan).Value = If(General_Class.CekNULL(Dr("Kode_Unik_Berjalan")) = "", "-", Dr("Kode_Unik_Berjalan"))
						'DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Kode_Unik_Asal).Value = If(General_Class.CekNULL(Dr("Kode_Unik_Asal")) = "", "-", Dr("Kode_Unik_Asal"))
						DGV_Data_Barcode.Rows(rows).Cells(Tab_1_Cell_Kode_Unik_Asal).Value = ""

						DGV_Data_Barcode.Rows(rows).DefaultCellStyle.BackColor = Color.LightBlue
						DGV_Data_Barcode.Rows(rows).DefaultCellStyle.ForeColor = Color.Black

						rows += 1
					Else
						Dr.Close()
						CloseConn()
						MessageBox.Show("Data barcode tidak ditemukan.", "Pencarian", MessageBoxButtons.OK, MessageBoxIcon.Information)
						Txt_QR.Text = ""
						Exit Sub
					End If

				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

		End If

		Tab_1_GetAllStock()
		Txt_QR.Text = ""

	End Sub

	Private Sub Tab_1_GetAllStock()
		If DGV_Data_Barcode.Rows.Count = 0 Then Exit Sub

		Dim total As Double = 0

		For i As Integer = 0 To DGV_Data_Barcode.Rows.Count - 1
			Get_Dgv_Tab_1(i)

			total = total + Val(HilangkanTanda(DGV_Data_Barcode.Rows(i).Cells(Tab_1_Cell_Jumlah).Value))

		Next

		Txt_Total_Stock.Text = Format(total, "N4")
	End Sub

	Private Sub DGV_Data_Rekap_Barcode_KeyDown(sender As Object, e As KeyEventArgs) Handles DGV_Data_Rekap_Barcode.KeyDown
		If DGV_Data_Rekap_Barcode.Rows.Count = 0 Or DGV_Data_Rekap_Barcode.SelectedCells.Count = 0 Then Exit Sub

		Dim currentRow = DGV_Data_Rekap_Barcode.CurrentRow.Index
		Dim currentCell = DGV_Data_Rekap_Barcode.CurrentCellAddress.X

		If e.KeyCode = Keys.Delete Then

			Dim Hapus1 As String = MessageBox.Show("Apakah Anda Yakin, Ingin Menghapus Data ini?", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If Hapus1 = vbNo Then Exit Sub

			Get_Dgv_Tab_2(currentRow)

			For index = DGV_Data_Detail_Barcode.Rows.Count - 1 To 0 Step -1
				Get_Dgv_Tab_3(index)

				If Dgv_Tab_2_KdBarang = Dgv_Tab_3_KdBarang And Dgv_Tab_2_Barcode = Dgv_Tab_3_Barcode_Baru Then
					DGV_Data_Detail_Barcode.Rows.RemoveAt(index)
				End If

			Next

			DGV_Data_Rekap_Barcode.Rows.RemoveAt(currentRow)

		End If

	End Sub

	Private Sub Btn_SimpanSementara_Click(sender As Object, e As EventArgs) Handles Btn_SimpanSementara.Click
		If DGV_Data_Barcode.Rows.Count = 0 Then Exit Sub

		'========================================================
		'=     CEK APAKAH BARANG SUDAH DI SIMPANN SEMENTARA     =
		'========================================================
		For i As Integer = 0 To DGV_Data_Rekap_Barcode.Rows.Count - 1
			Get_Dgv_Tab_2(i)
			If Dgv_Tab_2_KdBarang.Trim.ToUpper = TxtKd_Barang.Text.Trim.ToUpper Then
				MessageBox.Show($"Kode Barang {TxtKd_Barang.Text.Trim} sudah pernah di simpan sementara", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End If
		Next

		Try
			OpenConn()

			Dim DgvTab2_Rows As Integer = DGV_Data_Rekap_Barcode.Rows.Count
			Dim DgvTab3_Rows As Integer = DGV_Data_Detail_Barcode.Rows.Count

			Dim hasDataSelected As Boolean = False

			Dim Qr_Code_Baru As String = ""
			Dim Kode_Unik_Berjalan_Baru As String = ""
			Dim Tgl_Produksi_Baru As String = ""
			Dim Tgl_Expired_Baru As String = ""
			Dim Tgl_Masuk_Baru As String = ""

			Dim TotalJumlahMerge As Double = 0
			Dim SatuanMerge As String = ""

			Dim arrSelectedBarcode As New ArrayList

			For i As Integer = 0 To DGV_Data_Barcode.RowCount - 1
				Get_Dgv_Tab_1(i)

				If Dgv_Tab_1_Chkbox = False Then
					Continue For
				End If

				If String.IsNullOrWhiteSpace(Dgv_Tab_1_Jumlah_Input) Then
					DGV_Data_Barcode.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
					DGV_Data_Barcode.Rows(i).DefaultCellStyle.ForeColor = Color.Black

					CloseConn()
					MessageBox.Show($"Jumlah pada Barcode {Dgv_Tab_1_Barcode} Harus Di Input", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				If Val(HilangkanTanda(Dgv_Tab_1_Jumlah_Input)) < 0 Then
					DGV_Data_Barcode.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
					DGV_Data_Barcode.Rows(i).DefaultCellStyle.ForeColor = Color.Black

					CloseConn()
					MessageBox.Show($"Jumlah Input pada Barcode {Dgv_Tab_1_Barcode} Tidak Boleh Kurang dari 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				If Val(HilangkanTanda(Dgv_Tab_1_Jumlah)) < Val(HilangkanTanda(Dgv_Tab_1_Jumlah_Input)) Then
					DGV_Data_Barcode.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
					DGV_Data_Barcode.Rows(i).DefaultCellStyle.ForeColor = Color.Black

					CloseConn()
					MessageBox.Show($"Jumlah Input pada Barcode {Dgv_Tab_1_Barcode} Tidak Boleh Lebih dari Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				hasDataSelected = True

				'===================================
				'=      CEK STOCK REAL PADA SN     =
				'===================================
				SQL = "select isnuLL(sum(Jumlah), 0) as Jumlah "
				SQL &= $"from Barang_SN "
				SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and Kode_Stock_Owner = '{arrKodeStockOwner(Cmb_Kode_Stock_Owner.SelectedIndex)}' "
				SQL &= $"and Kode_Barang = '{TxtKd_Barang.Text.Trim}' "
				SQL &= $"and (Qr_Code+'-'+Kode_Unik_Berjalan) = '{Dgv_Tab_1_Barcode}' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						If Val(HilangkanTanda(Dr("Jumlah"))) < Val(HilangkanTanda(Dgv_Tab_1_Jumlah_Input)) Then

							CloseConn()
							MessageBox.Show($"Jumlah real stock pada barcode {Dgv_Tab_1_Barcode} kurang dari jumlah input, Harap lakukan pengecekan atau lakukan refresh data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					Else
						DGV_Data_Barcode.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
						DGV_Data_Barcode.Rows(i).DefaultCellStyle.ForeColor = Color.Black

						CloseConn()
						MessageBox.Show($"Data Stock pada barcode {Dgv_Tab_1_Barcode} Tidak ditemukan, harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'====================================
				'=     CREATE DATA BARCODE BARU     =
				'====================================
				If Qr_Code_Baru = "" Then
					Qr_Code_Baru = Dgv_Tab_1_Qr_Code
					Kode_Unik_Berjalan_Baru = Generate_Random_Kode(10)
					Tgl_Produksi_Baru = Dgv_Tab_1_TglProduksi
					Tgl_Expired_Baru = Dgv_Tab_1_TglExpired
					Tgl_Masuk_Baru = Dgv_Tab_1_TglMasuk
				End If

				'=============================
				'=     INSERT DATA TAB 3     =
				'=============================
				DGV_Data_Detail_Barcode.Rows.Add(1)
				DGV_Data_Detail_Barcode.Rows(DgvTab3_Rows).Cells(Tab_3_Cell_KdSo).Value = arrKodeStockOwner(Cmb_Kode_Stock_Owner.SelectedIndex)
				DGV_Data_Detail_Barcode.Rows(DgvTab3_Rows).Cells(Tab_3_Cell_KdBarang).Value = TxtKd_Barang.Text.Trim
				DGV_Data_Detail_Barcode.Rows(DgvTab3_Rows).Cells(Tab_3_Cell_NmBarang).Value = Txt_Nm_Barang.Text.Trim
				DGV_Data_Detail_Barcode.Rows(DgvTab3_Rows).Cells(Tab_3_Cell_QrCode).Value = Dgv_Tab_1_Qr_Code
				DGV_Data_Detail_Barcode.Rows(DgvTab3_Rows).Cells(Tab_3_Cell_Kd_Unik_Berjalan_Lama).Value = Dgv_Tab_1_Kode_Unik_Berjalan
				DGV_Data_Detail_Barcode.Rows(DgvTab3_Rows).Cells(Tab_3_Cell_Kd_Unik_Berjalan_Baru).Value = Kode_Unik_Berjalan_Baru
				DGV_Data_Detail_Barcode.Rows(DgvTab3_Rows).Cells(Tab_3_Cell_Barcode_Lama).Value = Dgv_Tab_1_Barcode
				DGV_Data_Detail_Barcode.Rows(DgvTab3_Rows).Cells(Tab_3_Cell_Barcode_Baru).Value = Qr_Code_Baru & "-" & Kode_Unik_Berjalan_Baru
				DGV_Data_Detail_Barcode.Rows(DgvTab3_Rows).Cells(Tab_3_Cell_Jumlah).Value = Format(Val(HilangkanTanda(Dgv_Tab_1_Jumlah_Input)), "N4")
				DGV_Data_Detail_Barcode.Rows(DgvTab3_Rows).Cells(Tab_3_Cell_Satuan).Value = Dgv_Tab_1_Satuan

				DgvTab3_Rows += 1

				TotalJumlahMerge += Val(HilangkanTanda(Dgv_Tab_1_Jumlah_Input))
				SatuanMerge = Dgv_Tab_1_Satuan

				arrSelectedBarcode.Add($"{Dgv_Tab_1_Qr_Code}-{Dgv_Tab_1_Kode_Unik_Berjalan}")

			Next

			'===========================================
			'=     GET BARCODE EXPIRED PALING AWAL     =
			'===========================================
			Dim dataBarcode As String = "'" & String.Join("','", arrSelectedBarcode.Cast(Of String)()) & "'"
			Dim Barcode_Expired_Duluan As String = ""
			SQL = "select top 1 (Qr_Code+'-'+Kode_Unik_Berjalan) as Barcode "
			SQL &= $"from barang_sn "
			SQL &= $"where Kode_Perusahaan ='{KodePerusahaan}' "
			SQL &= $"and (Qr_Code+'-'+Kode_Unik_Berjalan) in ({dataBarcode}) "
			SQL &= $"order by Tgl_Expired "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Barcode_Expired_Duluan = Dr("Barcode")
				End If
			End Using

			If hasDataSelected Then
				DGV_Data_Rekap_Barcode.Rows.Add(1)
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_KdSo).Value = arrKodeStockOwner(Cmb_Kode_Stock_Owner.SelectedIndex)
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_TglProduksi).Value = Tgl_Produksi_Baru
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_TglExpired).Value = Tgl_Expired_Baru
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_TglMasuk).Value = Tgl_Masuk_Baru
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_KdBarang).Value = TxtKd_Barang.Text.Trim
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_NmBarang).Value = Txt_Nm_Barang.Text.Trim
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_Barcode).Value = Qr_Code_Baru & "-" & Kode_Unik_Berjalan_Baru
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_Jumlah).Value = Format(Val(HilangkanTanda(TotalJumlahMerge)), "N4")
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_Satuan).Value = SatuanMerge
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_NoBarcode).Value = DgvTab2_Rows + 1
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_Qr_code).Value = Qr_Code_Baru
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_Kd_Unik_Berjalan).Value = Kode_Unik_Berjalan_Baru
				DGV_Data_Rekap_Barcode.Rows(DgvTab2_Rows).Cells(Tab_2_Cell_First_Expired_Barcode).Value = Barcode_Expired_Duluan
			Else
				MessageBox.Show("Tidak Ada Data Yang Di Pilih, Harap pilih dahulu barcode yang ingin digabungkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub

			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		MessageBox.Show("Data Barcode berhasil disimpan sementara", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

		Kosong_Tab_1()

	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If DGV_Data_Rekap_Barcode.RowCount = 0 Then
			MessageBox.Show("Belum ada barang yang akan di gabungkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKd_Barang.Focus() : Exit Sub
		ElseIf TxtKeterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKeterangan.Focus() : Exit Sub
		ElseIf Cmb_Kode_Stock_Owner.SelectedIndex = -1 Then
			MessageBox.Show("Lokasi belum dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Kode_Stock_Owner.Focus() : Exit Sub
		End If

		If MessageBox.Show("Yakin ingin menyimpan data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Dim arrKdUnikBarcode As New ArrayList

		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			get_no_faktur()

			arrKdUnikBarcode.Clear()

			'========================
			'=     INSERT INDUK     =
			'========================
			SQL = "insert into  N_EMI_Transaksi_Barcode_Merge (Kode_Perusahaan, No_Faktur, Keterangan, Tanggal, Jam, Kode_Stock_Owner, UserID) "
			SQL &= $"values ('{KodePerusahaan}', '{TxtNo_Transaksi.Text.Trim}', '{TxtKeterangan.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
			SQL &= $"'{arrKodeStockOwner(Cmb_Kode_Stock_Owner.SelectedIndex)}', '{UserID}') "
			ExecuteTrans(SQL)

			Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

			SQL = "delete from N_EMI_Cetak_Transaksi_Barcode_Merge where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
			ExecuteTrans(SQL)

			For i As Integer = 0 To DGV_Data_Rekap_Barcode.Rows.Count - 1
				Get_Dgv_Tab_2(i)

				Dim Qr_Code_Baru As String = Dgv_Tab_2_Qr_Code
				Dim Qr_Code_Kd_Unik_Baru As String = Dgv_Tab_2_Kd_Unik_Berjalan
				Dim Total_Potong_Stock_Barcode As Double = 0
				Dim Batch_Number_Akhir As String = ""

				Dim tgl_msk As String = ""
				Dim tgl_produksi As String = ""
				Dim tgl_expired As String = ""
				'==============================
				'=     GET DETAIL TANGGAL     =
				'==============================
				SQL = "select top 1 Tgl_Masuk, Tgl_Produksi, Tgl_Expired "
				SQL &= $"from barang_sn "
				SQL &= $"where Kode_Perusahaan ='{KodePerusahaan}' "
				SQL &= $"and Kode_Stock_Owner = '{Dgv_Tab_2_KdSo}' "
				SQL &= $"and Kode_Barang = '{Dgv_Tab_2_KdBarang}' "
				SQL &= $"and (Qr_Code+'-'+Kode_Unik_Berjalan) = '{Dgv_Tab_2_First_Expired_Barcode}' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						tgl_msk = Dr("Tgl_Masuk")
						tgl_produksi = Dr("Tgl_Produksi")
						tgl_expired = Dr("Tgl_Expired")
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terjadi Kesalahan, Tanggal Tidak Ditemukan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
						Exit Sub
					End If
				End Using

				'=========================
				'=     INSERT DETAIL     =
				'=========================
				SQL = "insert into N_EMI_Transaksi_Barcode_Merge_Detail (Kode_Perusahaan, No_Faktur, Kode_Barang, Qr_Code, Kode_Unik_Berjalan, Total, Satuan, Nomor_Barcode) "
				SQL &= $"values ('{KodePerusahaan}', '{TxtNo_Transaksi.Text.Trim}', '{Dgv_Tab_2_KdBarang}', '{Dgv_Tab_2_Qr_Code}', '{Dgv_Tab_2_Kd_Unik_Berjalan}', "
				SQL &= $"'{Val(HilangkanTanda(Dgv_Tab_2_Jumlah))}', '{Dgv_Tab_2_Satuan}', '{Dgv_Tab_2_NoBarcode}') "
				ExecuteTrans(SQL)

				'=======================
				'=     URUT DETAIL     =
				'=======================
				Dim Urut_Detail As Integer = 0
				SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Barcode_Merge_Detail') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Urut_Detail = Dr("urutan")
					End If
				End Using

				'=============================================
				'=     CEK APAKAH DETAIL SUDAH DI INSERT     =
				'=============================================
				SQL = "select Kode_Perusahaan "
				SQL &= $"from N_EMI_Transaksi_Barcode_Merge_Detail "
				SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and No_Faktur = '{TxtNo_Transaksi.Text.Trim}' "
				SQL &= $"and Kode_Barang = '{Dgv_Tab_2_KdBarang}' "
				SQL &= $"and Urut_Oto = '{Urut_Detail}' "
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terjadi Kesalahan, Silahkan Ulangi Transaksi  . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
						Exit Sub
					End If
				End Using

				'=============================================================================
				'=     CEK APAKAH QR CODE, DAN KODE UNIK BERJALAN SUDAH ADA DI BARANG SN     =
				'=============================================================================
				SQL = "select top 1 Kode_Perusahaan from Barang_SN "
				SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and (qr_code+'-'+kode_unik_berjalan) = '{Dgv_Tab_2_Barcode}' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Barcode {Dgv_Tab_2_Barcode} Sudah Ada Didatabase, Harap Ulangi Transaksi . . ! ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
						Exit Sub
					End If
				End Using

				For j As Integer = 0 To DGV_Data_Detail_Barcode.Rows.Count - 1
					Get_Dgv_Tab_3(j)

					If Not Dgv_Tab_2_KdBarang = Dgv_Tab_3_KdBarang And Not Dgv_Tab_2_Barcode = Dgv_Tab_3_Barcode_Baru Then
						Continue For
					End If

					'===============================
					'=     POTONG STOCK BARANG     =
					'===============================

#Region "Potong Stock Barang"

					'==============================
					'=     GET SN PER BARCODE     =
					'==============================
					Dim Sisa_Jumlah As Double = 0
					Dim Total_Potong As Double = 0
					SQL = $"select isnull(a.Jumlah, 0) as Stock_SN, a.serial_number, a.Batch_Number "
					SQL &= $"from Barang_SN a "
					SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
					SQL &= $"and a.Kode_stock_owner = '{Dgv_Tab_3_KdSo}' "
					SQL &= $"and a.Kode_Barang = '{Dgv_Tab_3_KdBarang}' "
					SQL &= $"and a.qr_Code+'-'+a.kode_unik_berjalan = '{Dgv_Tab_3_Barcode_Lama}' "
					SQL &= $"and a.jumlah<>0 "
					SQL &= $"order by a.Tgl_Expired "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then

								Sisa_Jumlah = Val(HilangkanTanda(Dgv_Tab_3_Jumlah))

								For k As Integer = 0 To .Rows.Count - 1

									If Sisa_Jumlah = 0 Then
										Exit For
									ElseIf Sisa_Jumlah < 0 Then
										CloseTrans()
										CloseConn()
										MessageBox.Show($"Terjadi kesalahan saat potong stock barcode {Dgv_Tab_3_Barcode_Lama},{vbCrLf} Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If

									Dim Stock_SN As Double = Val(HilangkanTanda(.Rows(k).Item("Stock_SN")))
									Dim Sn_Perbarcode As String = .Rows(k).Item("serial_number")
									Dim Batch_Number_Awal As String = .Rows(k).Item("Batch_Number")

									If Batch_Number_Akhir = "" Then
										Batch_Number_Akhir = Batch_Number_Awal
									End If

									Dim JumlahPotong As Double = 0

									If Sisa_Jumlah < Stock_SN Or Sisa_Jumlah = Stock_SN Then

										JumlahPotong = Sisa_Jumlah
										Sisa_Jumlah = 0

									ElseIf Sisa_Jumlah > Stock_SN Then

										JumlahPotong = Stock_SN
										Sisa_Jumlah -= Stock_SN
									Else
										CloseTrans()
										CloseConn()
										MessageBox.Show($"Terjadi kesalahan saat potong stock barcode {Dgv_Tab_3_Barcode_Lama},{vbCrLf} Harap hubungi tim IT !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If

									'===============================
									'=     POTONG TABEL BARANG     =
									'===============================
									SQL = "update barang set Good_Stock = Round(Good_Stock - " & Val(HilangkanTanda(JumlahPotong)) & ", 4), Jumlah_Bags = Jumlah_Bags - 0 "
									SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Dgv_Tab_3_KdSo & "' "
									SQL = SQL & " and Kode_Barang='" & Dgv_Tab_3_KdBarang & "'"
									ExecuteTrans(SQL)

									'==================================
									'=     POTONG TABEL BARANG SN     =
									'==================================
									SQL = "update barang_sn set jumlah = Round(jumlah - " & Val(HilangkanTanda(JumlahPotong)) & ", 4), Jumlah_Bags = Jumlah_Bags - 0 "
									SQL = SQL & "where Kode_Stock_Owner='" & Dgv_Tab_3_KdSo & "' and Kode_Barang='" & Dgv_Tab_3_KdBarang & "' "
									SQL = SQL & "and Serial_Number='" & Sn_Perbarcode & "'"
									ExecuteTrans(SQL)

									Total_Potong += JumlahPotong

									'==============================
									'=       INSERT SN BARU       =
									'==============================
									Dim hargaIsn As String = Get_Harga_SN(Sn_Perbarcode)

									'GET RAK
									Dim GetRakTujuan As String = ""
									SQL = "Select top 1 a.Id_WMS_Warehouse_Position, a.Keterangan "
									SQL &= $"from view_warehouse_position a "
									SQL &= $"where a.kode_perusahaan='{KodePerusahaan}' "
									SQL &= $"and a.Kode_Stock_Owner='{Dgv_Tab_3_KdSo}' "
									SQL &= $"group by a.Id_WMS_Warehouse_Position, a.Keterangan "
									SQL &= $"order by a.Keterangan "
									Using Dr = OpenTrans(SQL)
										If Dr.Read Then
											GetRakTujuan = Dr("Id_WMS_Warehouse_Position")
										End If
									End Using

									'GENERATE SN BARU
									Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
									Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
									Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

									SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah, Jumlah_Bags, "
									SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN, id_jenis_kategori_produksi) "
									SQL = SQL & "select Kode_Perusahaan, '" & Dgv_Tab_2_KdSo & "', Kode_Barang, '" & SN_Baru & "', '" & Val(HilangkanTanda(JumlahPotong)) & "', 0, "
									SQL = SQL & "'" & tgl_expired & "', '" & tgl_produksi & "', Stock_PO, Stock_Inquiry, "
									SQL = SQL & "'" & GetRakTujuan & "', id_Susunan , '" & Dgv_Tab_2_Qr_Code & "', '" & Dgv_Tab_2_Kd_Unik_Berjalan & "',  "
									SQL = SQL & "Kode_Unik_Asal, 0, batch_number, Warna, '" & tgl_msk & "', NULL, id_jenis_kategori_produksi "
									SQL = SQL & "from Barang_SN "
									SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
									SQL = SQL & "and Kode_Stock_Owner='" & Dgv_Tab_3_KdSo & "' "
									SQL = SQL & "and Kode_Barang='" & Dgv_Tab_3_KdBarang & "' "
									SQL = SQL & "and Serial_Number='" & Sn_Perbarcode & "' "
									ExecuteTrans(SQL)

									SQL = "update barang set Good_Stock= Round(Good_Stock + " & Val(HilangkanTanda(JumlahPotong)) & ", 4), Jumlah_Bags = Jumlah_Bags + 0 "
									SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Dgv_Tab_2_KdSo & "' "
									SQL = SQL & " and Kode_Barang='" & Dgv_Tab_2_KdBarang & "'"
									ExecuteTrans(SQL)

									'================================
									'=       INSERT TABEL DET       =
									'================================
									SQL = "insert into N_EMI_Transaksi_Barcode_Merge_Det(Kode_Perusahaan, No_Faktur, Kode_Barang, Serial_Number_Awal, Serial_Number_Akhir, Jumlah, Satuan, Urut_Detail, Batch_Number_Awal, Batch_Number_Akhir) "
									SQL &= $"values ('{KodePerusahaan}', '{TxtNo_Transaksi.Text.Trim}', '{Dgv_Tab_3_KdBarang}', "
									SQL &= $"'{Sn_Perbarcode}', '{SN_Baru}', '{Val(HilangkanTanda(JumlahPotong))}', '{Dgv_Tab_3_Satuan}', '{Urut_Detail}', "
									SQL &= $"'{Batch_Number_Awal}', '{Batch_Number_Akhir}')"
									ExecuteTrans(SQL)

								Next
							End If
						End With
					End Using

					'====================================
					'=       CEK KESESUAIAN STOCK       =
					'====================================
					SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
					SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
					SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
					SQL = SQL & "isnull(round(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
					SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 4) from Barang_sn y "
					SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
					SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Dgv_Tab_3_KdSo & "' "
					SQL = SQL & "AND a.Kode_Barang = '" & Dgv_Tab_3_KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
					SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then
								If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
									CloseTrans()
									CloseConn()
									MessageBox.Show($"Terjadi Kesalahan . . ! ! {vbCrLf} Stock Kode Barang {Dgv_Tab_3_KdBarang} Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							Else
								CloseTrans()
								CloseConn()
								MessageBox.Show($"Data Kode barang {Dgv_Tab_3_KdBarang} tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End With
					End Using

					If Math.Round(Val(HilangkanTanda(Total_Potong)), 4) <> Math.Round(Val(HilangkanTanda(Dgv_Tab_3_Jumlah)), 4) Then
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Jumlah potong barang {Dgv_Tab_3_KdBarang} tidak sesuai . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					Total_Potong_Stock_Barcode += Total_Potong

#End Region

				Next

				'=========================================================================
				'=       CEK APAKAH JUMLAH PER SN SESUAI DENGAN JUMLAH PER BARCODE       =
				'=========================================================================
				If Math.Round(Val(HilangkanTanda(Total_Potong_Stock_Barcode)), 4) <> Math.Round(Val(HilangkanTanda(Dgv_Tab_2_Jumlah)), 4) Then
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Jumlah potong barang ({Dgv_Tab_3_KdBarang}) tidak sesuai dengan jumlah hasil penggabungan barcode." & vbCrLf & vbCrLf &
						"Silakan hubungi tim IT.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				'============================================================
				'=       CEK APAKAH JUMLAH STOCK BARCODE BARU SESUSAI       =
				'============================================================
				SQL = "select isnull(sum(jumlah), 0) as jumlah from Barang_SN "
				SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
				SQL &= $"and Kode_Stock_Owner = '{Dgv_Tab_2_KdSo}' "
				SQL &= $"and kode_barang = '{Dgv_Tab_2_KdBarang}' "
				SQL &= $"and (Qr_Code+'-'+Kode_Unik_Berjalan) ='{Dgv_Tab_2_Barcode}' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						If Math.Round(Val(HilangkanTanda(Dr("jumlah"))), 4) <> Math.Round(Val(HilangkanTanda(Dgv_Tab_2_Jumlah)), 4) Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show($"jumlah stock pada barcode gabungan {Dgv_Tab_2_Barcode} tidak sesuai, harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
							Exit Sub
						End If
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Barcode gabungan {Dgv_Tab_2_Barcode} tidak tersimpan, harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
						Exit Sub
					End If
				End Using

				'=====================================
				'=       GENERATE BARCODE BARU       =
				'=====================================
				Dim kode_unik_print As String = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
				Dim fullNewQr As String = Dgv_Tab_2_Barcode

				Cmd.Parameters.Clear()
				Using ImgBarcode1 As Image = Generate_QR(fullNewQr)
					Using ms1 As New MemoryStream()
						ImgBarcode1.Save(ms1, Imaging.ImageFormat.Jpeg)
						Dim rawData1 As Byte() = ms1.ToArray()

						Dim param1 As String = "@newBarcode" & kode_unik_print
						Cmd.Parameters.Add(param1, SqlDbType.Image).Value = rawData1
					End Using
				End Using

				Dim barcode As String = "@newBarcode" & kode_unik_print

				'=============================================
				'=       INSERT KE TABEL CETAK BARCODE       =
				'=============================================
				SQL = "insert into N_EMI_Cetak_Transaksi_Barcode_Merge (Kode_Perusahaan, Barcode, Kode_Barang, NamaBarang, Tgl_Produksi, "
				SQL &= $"Tgl_Expired, Tgl_Masuk, Batch_Number, Jumlah_Input, Satuan_Input, No_Barcode, QrUtuh, "
				SQL &= $"Qr, Tanggal_Cetak, Jam_Cetak, Kode_Unik_Print) "
				SQL &= $"values ('{KodePerusahaan}', {barcode}, '{Dgv_Tab_2_KdBarang}', '{Dgv_Tab_2_NmBarang}', "
				SQL &= $"'{tgl_produksi}', '{tgl_expired}', '{tgl_msk}', "
				SQL &= $"'{Batch_Number_Akhir}', '{Val(HilangkanTanda(Dgv_Tab_2_Jumlah))}', '{Dgv_Tab_2_Satuan}', '{Dgv_Tab_2_NoBarcode}', "
				SQL &= $"'{Dgv_Tab_2_Barcode}', '{Dgv_Tab_2_Qr_Code}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{kode_unik_print}')"
				ExecuteTrans(SQL)

				arrKdUnikBarcode.Add(kode_unik_print)

			Next

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Data barcode berhasil digabungkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'=========================
		'=     CETAK BARCODE     =
		'=========================

		Try
			OpenConn()

			Dim CrDoc As New Object

			Dim kertasBarcode As String = "BarcodeFG"

			If arrKdUnikBarcode.Count <> 0 Then

				For i As Integer = 0 To arrKdUnikBarcode.Count - 1
					SQL = "select Kode_Perusahaan from N_EMI_Cetak_Transaksi_Barcode_Merge where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & arrKdUnikBarcode(i) & "'"
					Using Ds = BindingTrans(SQL)
						If Ds.Tables("MyTable").Rows.Count <> 0 Then

							CrDoc = New N_EMI_CR_Barcode_Transaksi_Barcode_Merge
							CrDoc.SetDataSource(Ds)
							CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
							CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Transaksi_Barcode_Merge.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Cetak_Transaksi_Barcode_Merge.kode_unik_print} = '" & arrKdUnikBarcode(i) & "' "

							CrDoc.PrintOptions.PrinterName = PrinterBarcode

							Dim doctoprint As New System.Drawing.Printing.PrintDocument()
							doctoprint.PrinterSettings.PrinterName = PrinterBarcode

							'Dim rawKind As Integer
							'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
							'For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
							'    If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcode Then
							'        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
							'        CrDoc.PrintOptions.PaperSize = rawKind
							'        Exit For
							'    End If
							'Next

							'If rawKind = Nothing Or rawKind = 0 Then
							'    CloseConn()
							'    MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Kertas Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'    Exit Sub
							'End If

							Dim rawKind As Integer
							Dim isPaperFound As Boolean = False
							CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
							For k = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
								If doctoprint.PrinterSettings.PaperSizes(k).PaperName = kertasBarcode Then
									rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(k).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(k)))
									CrDoc.PrintOptions.PaperSize = rawKind
									isPaperFound = True
									Exit For
								End If
							Next

							If Not isPaperFound Then
								'CloseConn()
								MessageBox.Show("Kertas Tidak DiTemukan, Kertas di set ke default", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
								'Exit Sub
							End If

							CrDoc.PrintToPrinter(1, False, 1, 2500)

						End If
					End Using
				Next
			Else
				CloseConn()
				MessageBox.Show("Tidak ada barcode yang di cetak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub

			End If

			CloseConn()
			MessageBox.Show("Berhasil Di Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()

	End Sub

	'=================================================================================================================================================================================
	'=     UTILITY
	'=================================================================================================================================================================================

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

End Class