Public Class Emi_Display_Transfer_Quality_Produksi

	Dim arrcari As New ArrayList

	Dim Lv_NoTransaksi, Lv_NoPO, Lv_So, Lv_Nama, Lv_JmlhSplit, Lv_Satuan, Lv_Tgl, Lv_Jam, Lv_Catatan, Lv_User, Lv_KdBarang As String

	Dim item_NoFak As Integer = 0
	Dim item_NoPo As Integer = 1
	Dim item_So As Integer = 2
	Dim item_KdBarang As Integer = 3
	Dim item_Nama As Integer = 4
	Dim item_JmlhSplit As Integer = 5
	Dim item_Satuan As Integer = 6
	Dim item_Tgl As Integer = 7
	Dim item_Jam As Integer = 8
	Dim item_Catatan As Integer = 9
	Dim item_User As Integer = 10

	Private Sub Emi_Display_Transfer_Quality_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Emi_Display_Transfer_Quality_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Initial_Lv()
		kosong()
	End Sub

	Private Sub kosong()
		Lv_Data.Items.Clear()
		arrcari.Clear()

		Txt_Filter_Value.Text = String.Empty

		Cmb_Filter_Jenis.Items.Clear()
		Cmb_Filter_Jenis.Items.Add("No Transaksi") : arrcari.Add("a.no_transaksi")
		Cmb_Filter_Jenis.Items.Add("No PO") : arrcari.Add("a.no_po")
		Cmb_Filter_Jenis.Items.Add("Lokasi") : arrcari.Add("a.kode_stock_owner")
		Cmb_Filter_Jenis.Items.Add("Nama Barang") : arrcari.Add("b.nama")
		Cmb_Filter_Jenis.Items.Add("User") : arrcari.Add("a.userId")

		Load_LV()
	End Sub

	Private Sub Initial_Lv()

		Lv_Data.Columns.Clear()

		Lv_Data.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("No PO", 150, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Lokasi", 180, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Kode Barang", 180, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Jumlah Split", 80, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("Jam", 100, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("Catatan", 250, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("User ", 100, HorizontalAlignment.Left)

		Lv_Data.View = View.Details

	End Sub

	Private Sub Get_Data_Lv(ByVal index As Integer)

		Lv_NoTransaksi = Lv_Data.Items(index).SubItems(item_NoFak).Text
		Lv_NoPO = Lv_Data.Items(index).SubItems(item_NoPo).Text
		Lv_So = Lv_Data.Items(index).SubItems(item_So).Text
		Lv_Nama = Lv_Data.Items(index).SubItems(item_Nama).Text
		Lv_JmlhSplit = Lv_Data.Items(index).SubItems(item_JmlhSplit).Text
		Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
		Lv_Tgl = Lv_Data.Items(index).SubItems(item_Tgl).Text
		Lv_Jam = Lv_Data.Items(index).SubItems(item_Jam).Text
		Lv_Catatan = Lv_Data.Items(index).SubItems(item_Catatan).Text
		Lv_User = Lv_Data.Items(index).SubItems(item_User).Text
		Lv_KdBarang = Lv_Data.Items(index).SubItems(item_KdBarang).Text
	End Sub

	Private Sub Load_LV()
		Try
			OpenConn()

			Lv_Data.Items.Clear()
			SQL = "select a.No_Transaksi, a.No_PO, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.Tanggal, a.Jam, a.Catatan, a.UserID "
			SQL = SQL & "from Emi_Split_Production_Order a, barang b, Emi_Production_Results c "
			SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
			SQL = SQL & "and a.No_Transaksi = c.No_Production_Order "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Hasil_Produksi = 'Y' "
			SQL = SQL & "order by a.No_Transaksi, a.No_PO, a.Kode_Stock_Owner "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim lv As New ListViewItem
					lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
					lv.SubItems.Add(Dr("No_PO"))
					lv.SubItems.Add(Dr("Kode_Stock_Owner"))
					lv.SubItems.Add(Dr("Kode_Barang"))
					lv.SubItems.Add(Dr("Nama"))
					lv.SubItems.Add(Dr("Jumlah"))
					lv.SubItems.Add(Dr("Satuan"))
					lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					lv.SubItems.Add(Dr("Jam"))
					lv.SubItems.Add(General_Class.CekNULL(Dr("Catatan")))
					lv.SubItems.Add(Dr("UserID"))

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

		If Cmb_Filter_Jenis.SelectedIndex = -1 Then
			MessageBox.Show("Jenis Harus Di Pilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt_Filter_Value.Text.Trim.Length = 0 Then
			MessageBox.Show("Value Harus Di Isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Try
			OpenConn()

			Lv_Data.Items.Clear()
			SQL = "select a.No_Transaksi, a.No_PO, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.Tanggal, a.Jam, a.Catatan, a.UserID "
			SQL = SQL & "from Emi_Split_Production_Order a, barang b "
			SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
			SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Selesai_Produksi is null "

			If Cmb_Filter_Jenis.SelectedIndex <> -1 Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
				SQL = SQL & arrcari.Item(Cmb_Filter_Jenis.SelectedIndex) & "  like  '%" & Trim(Txt_Filter_Value.Text) & "%' "
			End If

			SQL = SQL & "order by a.No_Transaksi, a.No_PO, a.Kode_Stock_Owner "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim lv As New ListViewItem
					lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
					lv.SubItems.Add(Dr("No_PO"))
					lv.SubItems.Add(Dr("Kode_Stock_Owner"))
					lv.SubItems.Add(Dr("Kode_Barang"))
					lv.SubItems.Add(Dr("Nama"))
					lv.SubItems.Add(Dr("Jumlah"))
					lv.SubItems.Add(Dr("Satuan"))
					lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					lv.SubItems.Add(Dr("Jam"))
					lv.SubItems.Add(General_Class.CekNULL(Dr("Catatan")))
					lv.SubItems.Add(Dr("UserID"))

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		kosong()
	End Sub

	Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick

		If Lv_Data.Items.Count = 0 Then Exit Sub

		Get_Data_Lv(Lv_Data.FocusedItem.Index)

		Emi_Transfer_Quality_Production.Lokasi = Lv_So
		Emi_Transfer_Quality_Production.Txt_NoSplit.Text = Lv_NoTransaksi
		Emi_Transfer_Quality_Production.Txt_NoPO.Text = Lv_NoPO
		Emi_Transfer_Quality_Production.Txt_KdBarang.Text = Lv_KdBarang
		Emi_Transfer_Quality_Production.Txt_NmBarang.Text = Lv_Nama
		Emi_Transfer_Quality_Production.Txt_Stock.Text = Lv_JmlhSplit
		Emi_Transfer_Quality_Production.Txt_Satuan.Text = Lv_Satuan
		Emi_Transfer_Quality_Production.kosong()
		Emi_Transfer_Quality_Production.Load_Dgv()

		Emi_Transfer_Quality_Production.ShowDialog()

	End Sub

End Class