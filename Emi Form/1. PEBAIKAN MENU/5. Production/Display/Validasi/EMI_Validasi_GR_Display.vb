Imports System.Drawing.Printing
Imports System.IO

Public Class EMI_Validasi_GR_Display

    Dim JudulForm As String = "Display Validasi Penerimaan Barang Hasil Produksi"

    Dim arrcari, arr_tgl, arr_Lain As New ArrayList

    Dim Lv_NoTransaksi, Lv_NoSplit, Lv_Tanggal, Lv_Jam, Lv_Keterangan, Lv_KdBarang, Lv_NmBarang, Lv_JmlhPO, Lv_JmlhValidasi, Lv_Satuan, Lv_User As String

    Dim LvKeranjang_NoTransaksi, LvKeranjang_NoSplit, LvKeranjang_Keranjang As String

    Dim LvDetail_LokasiAwal, LvDetail_LokasiTujuan, LvDetail_Barcode, LvDetail_Routing, LvDetail_Jenis, LvDetail_Jumlah, LvDetail_Satuan As String


    Private random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream


    Dim item_NoTransaksi As Integer = 0
    Dim item_NoSplit As Integer = 1
    Dim item_Tanggal As Integer = 2
    Dim item_Jam As Integer = 3
    Dim item_KdBarang As Integer = 4
    Dim item_NmBarang As Integer = 5
    Dim item_Keterangan As Integer = 6
    Dim item_JmlhValidasi As Integer = 7
    Dim item_Satuan As Integer = 8
    Dim item_User As Integer = 9

    Dim itemKeranjang_NoTransaksi = 0
    Dim itemKeranjang_NoSplit = 1
    Dim itemKeranjang_Keranjang = 2


    Dim itemDetail_LokasiTujuan As Integer = 0
    Dim itemDetail_Barode As Integer = 1
    Dim itemDetail_Routing As Integer = 2
    Dim itemDetail_Jenis As Integer = 3
    Dim itemDetail_Jumlah As Integer = 4
    Dim itemDetail_Satuan As Integer = 5





    Private Sub EMI_Display_Validasi_GR_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Validation.Columns.Clear()
        Lv_Validation.Columns.Add("No Transaksi", 120, HorizontalAlignment.Left) '0
        Lv_Validation.Columns.Add("No Split", 120, HorizontalAlignment.Left) '1
        Lv_Validation.Columns.Add("Tanggal", 110, HorizontalAlignment.Center) '2
        Lv_Validation.Columns.Add("Jam", 90, HorizontalAlignment.Center) '3
        Lv_Validation.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '4
        Lv_Validation.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left) '5
        Lv_Validation.Columns.Add("Keterangan", 180, HorizontalAlignment.Left) '6
        Lv_Validation.Columns.Add("Jumlah Validasi", 120, HorizontalAlignment.Right) '7
        Lv_Validation.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '8
        Lv_Validation.Columns.Add("User", 100, HorizontalAlignment.Center) '9
        Lv_Validation.View = View.Details

        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("Lokasi Tujuan", 110, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Barcode", 180, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Routing", 120, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Jenis", 120, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan ", 80, HorizontalAlignment.Center)
        Lv_Detail.View = View.Details

        Lv_Detail_Packaging.Columns.Clear()
        Lv_Detail_Packaging.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Barang", 0, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
        Lv_Detail_Packaging.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Detail_Packaging.View = View.Details


        arr_tgl.Clear() : Cmb_Tanggal.Items.Clear()
        Cmb_Tanggal.Items.Add("Tanggal") : arr_tgl.Add("a.Tanggal")

        arr_Lain.Clear() : Cmb_Lain.Items.Clear()
        Cmb_Lain.Items.Add("No Transaksi") : arr_Lain.Add("a.No_Transaksi")
        Cmb_Lain.Items.Add("No Split") : arr_Lain.Add("a.No_Production_Order")
        Cmb_Lain.Items.Add("Kode Barang") : arr_Lain.Add("c.Kode_Barang")
        Cmb_Lain.Items.Add("User") : arr_Lain.Add("a.UserID")

        Chk_HariIni.Focus()

        Try
            OpenConn()

            Cmb_Satuan.Items.Clear()
            SQL = "select Satuan from EMI_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Satuan.Items.Add(Dr("Satuan"))
                Loop
                Cmb_Satuan.SelectedIndex = -1
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


        Cmb_Tanggal.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        Cmb_Lain.Enabled = False : Txt_ValuLain.Enabled = False : Txt_ValuLain.Text = ""


        LoadDataValidation()



    End Sub


    Private Sub Get_Data_Validation(ByVal index As Integer)

        Lv_NoTransaksi = Lv_Validation.Items(index).SubItems(item_NoTransaksi).Text
        Lv_NoSplit = Lv_Validation.Items(index).SubItems(item_NoSplit).Text
        Lv_Tanggal = Lv_Validation.Items(index).SubItems(item_Tanggal).Text
        Lv_Jam = Lv_Validation.Items(index).SubItems(item_Jam).Text
        Lv_KdBarang = Lv_Validation.Items(index).SubItems(item_KdBarang).Text
        Lv_NmBarang = Lv_Validation.Items(index).SubItems(item_NmBarang).Text
        Lv_Keterangan = Lv_Validation.Items(index).SubItems(item_Keterangan).Text
        Lv_JmlhValidasi = Lv_Validation.Items(index).SubItems(item_JmlhValidasi).Text
        Lv_Satuan = Lv_Validation.Items(index).SubItems(item_Satuan).Text
        Lv_User = Lv_Validation.Items(index).SubItems(item_User).Text

    End Sub

    Private Sub Get_Data_Keranjang(ByVal index As Integer)
        LvKeranjang_NoTransaksi = Dgv_Keranjang.Rows(index).Cells(itemKeranjang_NoTransaksi).value
        LvKeranjang_NoSplit = Dgv_Keranjang.Rows(index).Cells(itemKeranjang_NoSplit).value
        LvKeranjang_Keranjang = Dgv_Keranjang.Rows(index).Cells(itemKeranjang_Keranjang).value
    End Sub

    Private Sub Get_Data_ValidationDetail(ByVal index As Integer)

        LvDetail_LokasiTujuan = Lv_Detail.Items(index).SubItems(itemDetail_LokasiTujuan).Text
        LvDetail_Barcode = Lv_Detail.Items(index).SubItems(itemDetail_Barode).Text
        LvDetail_Jenis = Lv_Detail.Items(index).SubItems(itemDetail_Jenis).Text
        LvDetail_Jumlah = Lv_Detail.Items(index).SubItems(itemDetail_Jumlah).Text
        LvDetail_Satuan = Lv_Detail.Items(index).SubItems(itemDetail_Satuan).Text
        LvDetail_Routing = Lv_Detail.Items(index).SubItems(itemDetail_Routing).Text

    End Sub

    Private Sub LoadDataValidation()

        Try
            OpenConn()

            Lv_Validation.Items.Clear() : Dgv_Keranjang.Rows.Clear()
            Lv_Detail.Items.Clear() : Lv_Detail_Packaging.Items.Clear()
            Txt_KdBarang.Text = "" : Txt_Barang.Text = "" : Txt_Barcode.Text = "" : Txt_Jumlah.Text = ""
            Dtp_Produksi.Value = Now.Date : Dtp_Expired.Value = Now.Date
            Cmb_Satuan.SelectedIndex = -1 : Cmb_Satuan.Text = ""
            SQL = "select a.No_Transaksi, a.No_Production_Order, a.Status, a.Tanggal, a.Jam, c.Kode_Barang, d.Nama as Nama_Barang, a.Keterangan, "
            SQL = SQL & "ISNULL(sum(b.Jumlah), 0) as JumlahValidasi, b.Satuan, a.UserID "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Split_Production_Order c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = c.kode_perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.No_Production_Order = c.No_Transaksi "
            SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Status is null "

            If Chk_HariIni.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal between '"
                SQL = SQL & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' and '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            End If

            If Chk_Tanggal.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arr_tgl.Item(Cmb_Tanggal.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If Chk_Lain.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arr_Lain.Item(Cmb_Lain.SelectedIndex) & " like '%" & Trim(Txt_ValuLain.Text) & "%' "
            End If

            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "group by a.No_Transaksi, a.No_Production_Order, a.Status, a.Tanggal, a.Jam, c.Kode_Barang, d.Nama, a.Keterangan, b.Satuan, a.UserID "
            SQL = SQL & "order by a.Tanggal, a.Jam "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Lv As ListViewItem
                            Lv = Lv_Validation.Items.Add(.Rows(i).Item("No_Transaksi"))
                            Lv.SubItems.Add(.Rows(i).Item("No_Production_Order"))
                            Lv.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            Lv.SubItems.Add(.Rows(i).Item("Jam"))
                            Lv.SubItems.Add(.Rows(i).Item("Kode_Barang"))
                            Lv.SubItems.Add(.Rows(i).Item("Nama_Barang"))
                            Lv.SubItems.Add(.Rows(i).Item("Keterangan"))
                            Lv.SubItems.Add(Format(.Rows(i).Item("JumlahValidasi"), "N2"))
                            Lv.SubItems.Add(.Rows(i).Item("Satuan"))
                            Lv.SubItems.Add(.Rows(i).Item("UserID"))

                            If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then
                                Lv.BackColor = Color.DarkRed
                                Lv.ForeColor = Color.White
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

    Private Sub Lv_Validation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Validation.SelectedIndexChanged
        If Lv_Validation.Items.Count = 0 Or Lv_Validation.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Get_Data_Validation(Lv_Validation.FocusedItem.Index)

            Dim selectedSplit As String = Lv_NoSplit
            Dim selectedFaktur As String = Lv_NoTransaksi
            'Dim SelectedBarcode As String = Lv_Detail.FocusedItem.SubItems(itemDetail_Barode).Text

            Dgv_Keranjang.Rows.Clear() : Lv_Detail.Items.Clear() : Lv_Detail_Packaging.Items.Clear()
            Txt_KdBarang.Text = "" : Txt_Barang.Text = "" : Txt_Barcode.Text = "" : Txt_Jumlah.Text = ""
            Dtp_Produksi.Value = Now.Date : Dtp_Expired.Value = Now.Date
            Cmb_Satuan.SelectedIndex = -1 : Cmb_Satuan.Text = ""
            SQL = "select distinct a.No_Transaksi, a.Nomor, b.No_Production_Order from Emi_Production_Results_Validation_Detail a, Emi_Production_Results_Validation b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & selectedFaktur & "' "
            SQL = SQL & "order by a.Nomor "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Keranjang.Rows.Add(1)
                            Dgv_Keranjang.Rows(i).Cells(itemKeranjang_NoTransaksi).value = .Rows(i).Item("No_Transaksi")
                            Dgv_Keranjang.Rows(i).Cells(itemKeranjang_NoSplit).value = .Rows(i).Item("No_Production_Order")
                            Dgv_Keranjang.Rows(i).Cells(itemKeranjang_Keranjang).value = .Rows(i).Item("Nomor")


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

    Private Sub Dgv_Keranjang_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Keranjang.CellClick
        If Dgv_Keranjang.Rows.Count = 0 Or Dgv_Keranjang.SelectedRows Is Nothing Then Exit Sub

        Dim asdaa As String = Dgv_Keranjang.SelectedRows(0).Index
        Get_Data_Keranjang(Dgv_Keranjang.SelectedRows(0).Index)

        Dim SelectedNoTransaksi As String = ""
        Dim SelectedKeranjang As String = ""

        Try
            OpenConn()


            Txt_KdBarang.Text = "" : Txt_Barang.Text = "" : Txt_Barcode.Text = "" : Txt_Jumlah.Text = ""
            Dtp_Produksi.Value = Now.Date : Dtp_Expired.Value = Now.Date
            Cmb_Satuan.SelectedIndex = -1 : Cmb_Satuan.Text = ""
            SQL = "with cte as( "
            SQL = SQL & "select Distinct b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan, "
            SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
            SQL = SQL & "c.nomor as Number, e.Kode_Unik_Berjalan "
            SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN e "
            SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Production_Order = '" & LvKeranjang_NoSplit & "' "
            SQL = SQL & "and b.No_Transaksi = '" & LvKeranjang_NoTransaksi & "'  and c.Jenis<>'Finished Good' "
            SQL = SQL & "and c.nomor = " & LvKeranjang_Keranjang & " "
            SQL = SQL & "union all "
            SQL = SQL & "select Distinct b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan, "
            SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
            SQL = SQL & "c.nomor as Number, e.Kode_Unik_Berjalan "
            SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN_sementara e "
            SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Production_Order = '" & LvKeranjang_NoSplit & "' "
            SQL = SQL & "and b.No_Transaksi = '" & LvKeranjang_NoTransaksi & "'  and c.Jenis='Finished Good' "
            SQL = SQL & "and c.nomor = " & LvKeranjang_Keranjang & " "
            SQL = SQL & ") select kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number "
            SQL = SQL & "from cte "
            SQL = SQL & "group by kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, Satuan, Jenis, Number, Nomor "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Txt_KdBarang.Text = Dr("Kode_Barang") : Txt_Barang.Text = Dr("Nama_Barang")
                    Txt_Barcode.Text = $"{Dr("Qr_Code")}-{Dr("Kode_Unik_Berjalan")}"
                    Dtp_Produksi.Value = Dr("Tgl_Produksi") : Dtp_Expired.Value = Dr("Tgl_Expired")
                    Txt_Jumlah.Text = Format(Dr("Jumlah"), "N0") : Cmb_Satuan.Text = Dr("Satuan")

                End If
            End Using

            Lv_Detail.Items.Clear()
            'SQL = "SELECT b.Kode_Stock_Owner_Awal, b.Kode_Stock_Owner_Tujuan, b.Batch_Number, (d.Qr_Code +'-' + d.Kode_Unik_Berjalan)as Barcode_tujuan, c.Keterangan as Kualitas, b.Jenis, b.Jumlah, b.Satuan "
            'SQL = SQL & "FROM Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, EMI_Master_Warna c, Barang_SN d "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            'SQL = SQL & "and b.Warna = c.Kode_Warna "
            'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Serial_Number_Tujuan = d.Serial_Number "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Transaksi = '" & LvKeranjang_NoTransaksi & "' "
            'SQL = SQL & "and b.Nomor = " & LvKeranjang_Keranjang & " and b.Jenis<>'Finished Good' "
            'SQL = SQL & "Union All "
            'SQL = SQL & "SELECT b.Kode_Stock_Owner_Awal, b.Kode_Stock_Owner_Tujuan, b.Batch_Number, (d.Qr_Code +'-' + d.Kode_Unik_Berjalan)as Barcode_tujuan, c.Keterangan as Kualitas, b.Jenis, b.Jumlah, b.Satuan "
            'SQL = SQL & "FROM Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, EMI_Master_Warna c, Barang_SN_sementara d "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            'SQL = SQL & "and b.Warna = c.Kode_Warna "
            'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Serial_Number_Tujuan = d.Serial_Number "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Transaksi = '" & LvKeranjang_NoTransaksi & "' "
            'SQL = SQL & "and b.Nomor = " & LvKeranjang_Keranjang & " and b.Jenis='Finished Good'"

            SQL = ";with cte as( "
            SQL = SQL & "select Distinct b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan,  "
            SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan,  "
            SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
            SQL = SQL & "c.nomor as Number, "
            SQL = SQL & "e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing "
            SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN e, Emi_Split_Production_Order f, EMI_Order_Produksi g, EMI_Master_Routing h "
            SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan  "
            SQL = SQL & "and b.kode_perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and g.Kode_Perusahaan = h.Kode_Perusahaan "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi  "
            SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang  "
            SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
            SQL = SQL & "and b.No_Production_Order = f.No_Transaksi "
            SQL = SQL & "and f.No_PO = g.No_Faktur "
            SQL = SQL & "and g.Id_Routing = h.Id_Routing "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Production_Order = '" & LvKeranjang_NoSplit & "' "
            SQL = SQL & "and b.No_Transaksi = '" & LvKeranjang_NoTransaksi & "'  and c.Jenis<>'Finished Good' "
            SQL = SQL & "and c.nomor = '" & LvKeranjang_Keranjang & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select Distinct b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan,  "
            SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan,  "
            SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
            SQL = SQL & "c.nomor as Number, "
            SQL = SQL & "e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing "
            SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN_sementara e, Emi_Split_Production_Order f, EMI_Order_Produksi g, EMI_Master_Routing h "
            SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan  "
            SQL = SQL & "and b.kode_perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and g.Kode_Perusahaan = h.Kode_Perusahaan "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
            SQL = SQL & "and b.No_Production_Order = f.No_Transaksi "
            SQL = SQL & "and f.No_PO = g.No_Faktur "
            SQL = SQL & "and g.Id_Routing = h.Id_Routing "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and b.No_Production_Order = '" & LvKeranjang_NoSplit & "' "
            SQL = SQL & "and b.No_Transaksi = '" & LvKeranjang_NoTransaksi & "' and c.Jenis='Finished Good' "
            SQL = SQL & "and c.nomor = '" & LvKeranjang_Keranjang & "' "
            SQL = SQL & ") select kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number, Id_Routing, Routing "
            SQL = SQL & "from cte  "
            SQL = SQL & "group by kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, Satuan, Jenis, Number, Nomor, Id_Routing, Routing "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("Lokasi_Tujuan"))
                    Lv.SubItems.Add(Dr("Qr_Code") & "-" & Dr("Kode_Unik_Berjalan"))
                    Lv.SubItems.Add(Dr("Routing"))
                    Lv.SubItems.Add(Dr("Jenis"))
                    If General_Class.CekNULL(Dr("Satuan")).ToUpper = "PCS" Then
                        Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                    Else
                        Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    End If
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using


            Lv_Detail_Packaging.Items.Clear()
            SQL = "SELECT c.Kode_Stock_Owner, c.Kode_Barang, d.Nama as Nama_Barang, sum(c.Jumlah) as Jumlah, c.Satuan "
            SQL = SQL & "FROM Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Production_Results_Validation_Packaging_Detail c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Urut = c.Urut_Detail "
            SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & LvKeranjang_NoTransaksi & "' "
            SQL = SQL & "and b.Nomor = " & LvKeranjang_Keranjang & "  "
            SQL = SQL & "group by c.Kode_Stock_Owner, c.Kode_Barang, d.Nama, c.Satuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Packaging.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))

                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Chk_HariIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_HariIni.CheckedChanged
        If Chk_HariIni.Checked = True Then
            Chk_Tanggal.Checked = False
            Cmb_Tanggal.SelectedIndex = -1
            Cmb_Tanggal.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            DateTimePicker1.Value = Now
            DateTimePicker2.Value = Now

            If Chk_Lain.Checked Then
                If Cmb_Lain.SelectedIndex = -1 Then
                    MessageBox.Show("Pilih Dahulu FIlter", JudulForm)
                    Cmb_Lain.Focus() : Cmb_Lain.DroppedDown = True : Exit Sub
                ElseIf Txt_ValuLain.Text.Trim.Length = 0 Then
                    MessageBox.Show("Value FIlter Tidak Boleh Kosong", JudulForm)
                    Txt_ValuLain.Focus() : Exit Sub
                End If
            End If

            LoadDataValidation()
        End If
    End Sub

    Private Sub Chk_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Tanggal.CheckedChanged
        If Chk_Tanggal.Checked = True Then
            Chk_HariIni.Checked = False
            Cmb_Tanggal.Enabled = True
            Cmb_Tanggal.DroppedDown = True
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        Else
            Cmb_Tanggal.SelectedIndex = -1
            Cmb_Tanggal.Enabled = False
            Cmb_Tanggal.DroppedDown = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            DateTimePicker1.Value = Now
            DateTimePicker2.Value = Now
        End If
    End Sub

    Private Sub Chk_Lain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Lain.CheckedChanged
        If Chk_Lain.Checked = True Then
            Cmb_Lain.Enabled = True
            Cmb_Lain.DroppedDown = True
            Txt_ValuLain.Enabled = True
        Else
            Cmb_Lain.Enabled = False
            Cmb_Lain.DroppedDown = False
            Txt_ValuLain.Enabled = False
            Cmb_Lain.SelectedIndex = -1
            Txt_ValuLain.Text = ""
        End If
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Chk_Tanggal.Checked = False And Chk_Lain.Checked = False And Chk_HariIni.Checked = False Then
            MessageBox.Show("Pilih Dahulu Filter")
            Chk_Tanggal.Focus() : Exit Sub
        End If
        If Chk_Tanggal.Checked Then
            If Cmb_Tanggal.SelectedIndex = -1 Then
                MessageBox.Show("Pilih Dahulu Filter Tanggal", JudulForm)
                Cmb_Tanggal.Focus() : Cmb_Tanggal.DroppedDown = True : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Tanggal Awal dan Akhir tidak Boleh Sama", JudulForm)
                DateTimePicker1.Value = Now : DateTimePicker2.Value = Now
                Exit Sub
            End If
        End If
        If Chk_Lain.Checked Then
            If Cmb_Lain.SelectedIndex = -1 Then
                MessageBox.Show("Pilih Dahulu FIlter", JudulForm)
                Cmb_Lain.Focus() : Cmb_Lain.DroppedDown = True : Exit Sub
            ElseIf Txt_ValuLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value FIlter Tidak Boleh Kosong", JudulForm)
                Txt_ValuLain.Focus() : Exit Sub
            End If
        End If

        LoadDataValidation()
    End Sub

    Private Sub SalinNoTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoTransaksiToolStripMenuItem.Click
        If Lv_Validation.Items.Count = 0 Or Lv_Validation.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih Dahulu No Transaksi yang Ingin Disalin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Validation.FocusedItem.Text)
    End Sub

    Private Sub CetakUlangBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeToolStripMenuItem.Click
        If Lv_Detail.Items.Count = 0 Or Lv_Detail.FocusedItem Is Nothing Then Exit Sub

        Dim SelectedTransaksi As String = Lv_Detail.FocusedItem.Text

        Dim kode_unik_print As String = ""

        Dim KdUnikPrint, KdUnikPrintScrap As New ArrayList

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim selectedSplit As String = Lv_Validation.FocusedItem.SubItems(item_NoSplit).Text
            Dim selectedFaktur As String = Lv_Validation.FocusedItem.SubItems(item_NoTransaksi).Text
            Dim SelectedBarcode As String = Lv_Detail.FocusedItem.SubItems(itemDetail_Barode).Text

            'SQL = "Truncate Table N_EMI_Barcode_Label_Barcode_GR_2"
            'ExecuteTrans(SQL)


            'SQL = "With cte As( "
            'SQL = SQL & "select Distinct b.Kode_Perusahaan, b.no_Transaksi, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan,  "
            'SQL = SQL & "e.Tgl_Expired, c.Jumlah As jumlah, d.Satuan, "
            'SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
            'SQL = SQL & "c.nomor as Number, "

            'SQL = SQL & "e.Kode_Unik_Berjalan, f.Keterangan as Kualitas "

            'SQL = SQL & "From Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN e, EMI_Master_Warna f "
            'SQL = SQL & "Where b.Kode_Perusahaan = c.Kode_Perusahaan And c.Kode_Perusahaan = d.Kode_Perusahaan And b.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan "
            'SQL = SQL & "And b.No_Transaksi = c.No_Transaksi "
            'SQL = SQL & "And c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner And c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "And c.Kode_Barang = e.Kode_Barang And c.Serial_Number_Tujuan=e.Serial_Number "
            'SQL = SQL & "and c.Warna = f.Kode_Warna "
            'SQL = SQL & "And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "And b.No_Production_Order = '" & selectedSplit & "' "
            'SQL = SQL & "And b.No_Transaksi = '" & selectedFaktur & "'  and c.Jenis<>'Finished Good' "
            'SQL = SQL & "and (e.Qr_Code + '-' + e.Kode_Unik_Berjalan) = '" & SelectedBarcode & "' "

            'SQL = SQL & " union all "

            'SQL = SQL & "select Distinct b.Kode_Perusahaan, b.no_Transaksi, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            'SQL = SQL & "e.Tgl_Expired, c.Jumlah As jumlah, d.Satuan, "
            'SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
            'SQL = SQL & "c.nomor as Number, "

            'SQL = SQL & "e.Kode_Unik_Berjalan, f.Keterangan as Kualitas "

            'SQL = SQL & "From Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN_sementara e, EMI_Master_Warna f "
            'SQL = SQL & "Where b.Kode_Perusahaan = c.Kode_Perusahaan And c.Kode_Perusahaan = d.Kode_Perusahaan And b.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan "
            'SQL = SQL & "And b.No_Transaksi = c.No_Transaksi "
            'SQL = SQL & "And c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner And c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "And c.Kode_Barang = e.Kode_Barang And c.Serial_Number_Tujuan=e.Serial_Number "
            'SQL = SQL & "and c.Warna = f.Kode_Warna "
            'SQL = SQL & "And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "And b.No_Production_Order = '" & selectedSplit & "' "
            'SQL = SQL & "And b.No_Transaksi = '" & selectedFaktur & "'  and c.Jenis='Finished Good' "
            'SQL = SQL & "and (e.Qr_Code + '-' + e.Kode_Unik_Berjalan) = '" & SelectedBarcode & "' "

            'SQL = SQL & ") select kode_perusahaan, no_production_order, no_Transaksi, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number, Kualitas "
            'SQL = SQL & "From cte "
            'SQL = SQL & "Group By kode_perusahaan, no_production_order, no_Transaksi, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, Satuan, Jenis, Number, Nomor, Kualitas "


            SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_2 "
            ExecuteTrans(SQL)

            SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_2_Scrap "
            ExecuteTrans(SQL)


            SQL = "with cte as( "
            SQL = SQL & "select b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan,   "
            SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
            SQL = SQL & "c.nomor as Number, "

            SQL = SQL & "e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing "

            SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN e, Emi_Split_Production_Order f, EMI_Order_Produksi g, EMI_Master_Routing h "
            SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and b.kode_perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and g.Kode_Perusahaan = h.Kode_Perusahaan "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
            SQL = SQL & "and b.No_Production_Order = f.No_Transaksi "
            SQL = SQL & "and f.No_PO = g.No_Faktur "
            SQL = SQL & "and g.Id_Routing = h.Id_Routing "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Production_Order = '" & selectedSplit & "' "
            SQL = SQL & "and b.No_Transaksi = '" & selectedFaktur & "'  and c.Jenis<>'Finished Good' "

            SQL = SQL & "union all "

            SQL = SQL & "select Distinct b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan,  "
            SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
            SQL = SQL & "c.nomor as Number, "

            SQL = SQL & "e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing "

            SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN_sementara e, Emi_Split_Production_Order f, EMI_Order_Produksi g, EMI_Master_Routing h "
            SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and b.kode_perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and g.Kode_Perusahaan = h.Kode_Perusahaan "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
            SQL = SQL & "and b.No_Production_Order = f.No_Transaksi "
            SQL = SQL & "and f.No_PO = g.No_Faktur "
            SQL = SQL & "and g.Id_Routing = h.Id_Routing "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Production_Order = '" & selectedSplit & "' "
            SQL = SQL & "and b.No_Transaksi = '" & selectedFaktur & "'  and c.Jenis='Finished Good' "

            SQL = SQL & ") select kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number, Id_Routing, Routing "
            SQL = SQL & "from cte "
            SQL = SQL & "group by kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, Satuan, Jenis, Number, Nomor, Id_Routing, Routing "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            'kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")

                            'Dim fullNewQrScrap As String = .Rows(i).Item("Qr_Code") & "-" & .Rows(i).Item("Kode_Unik_Berjalan")

                            'Barcode.Image = Nothing

                            'Barcode.Image = Generate_QR_NoPadding(fullNewQrScrap)

                            'Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")

                            ''   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

                            ''If Not (System.IO.File.Exists(FileToSaveAs1)) Then
                            'Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
                            ''End If

                            'fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                            'FileSize1 = fs1.Length
                            'rawData1 = New Byte(FileSize1) {}
                            'fs1.Read(rawData1, 0, FileSize1)
                            'fs1.Close()
                            'Cmd.Parameters.Add("@newBarcode" & kode_unik_print, SqlDbType.Image).Value = rawData1



                            'SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2 (Kode_Perusahaan, No_Split, Kode_Barang, Barcode, Nama_Barang, Batch_Number, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, Tgl_Expired, Jam_Expired, Jumlah, Satuan, Jenis, Number, Kode_Unik_Print) "
                            'SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "', '" & .Rows(i).Item("Kode_Barang") & "', @newBarcode" & kode_unik_print & ", "
                            'SQL = SQL & "'" & .Rows(i).Item("Nama_Barang") & "', '" & .Rows(i).Item("Batch_Number") & "', '" & fullNewQrScrap & "', '" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Tgl_Produksi") & "', "
                            'SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Tgl_Expired") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                            'SQL = SQL & "'" & .Rows(i).Item("Jenis") & "', '" & .Rows(i).Item("Number") & "', '" & kode_unik_print & "') "
                            'ExecuteTrans(SQL)


                            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

                            Dim fullNewQrScrap As String = .Rows(i).Item("Qr_Code") & "-" & .Rows(i).Item("Kode_Unik_Berjalan")

                            Barcode.Image = Nothing

                            Barcode.Image = Generate_QR_NoPadding(fullNewQrScrap)

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
                            Cmd.Parameters.Add("@newBarcode" & kode_unik_print, SqlDbType.Image).Value = rawData1

                            Dim asdada As String = .Rows(i).Item("Jenis").ToString.ToUpper

                            If .Rows(i).Item("Jenis").ToString.ToUpper.Trim = "FINISHED GOOOD" Then

                                SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2 (Kode_Perusahaan, No_Split, Kode_Barang, Barcode, Nama_Barang, Batch_Number, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, Tgl_Expired, Jam_Expired, Jumlah, Satuan, Jenis, Number, Kode_Unik_Print) "
                                SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "', '" & .Rows(i).Item("Kode_Barang") & "', @newBarcode" & kode_unik_print & ", "
                                SQL = SQL & "'" & .Rows(i).Item("Nama_Barang") & "', '" & .Rows(i).Item("Batch_Number") & "', '" & fullNewQrScrap & "', '" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Tgl_Produksi") & "', "
                                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Tgl_Expired") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Jenis") & "', '" & .Rows(i).Item("Number") & "', '" & kode_unik_print & "') "
                                ExecuteTrans(SQL)

                                KdUnikPrint.Add(kode_unik_print)

                            ElseIf .Rows(i).Item("Jenis").ToString.ToUpper.Trim = "DISQUALIFIED" Then
                                SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2 (Kode_Perusahaan, No_Split, Kode_Barang, Barcode, Nama_Barang, Batch_Number, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, Tgl_Expired, Jam_Expired, Jumlah, Satuan, Jenis, Number, Kode_Unik_Print) "
                                SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "', '" & .Rows(i).Item("Kode_Barang") & "', @newBarcode" & kode_unik_print & ", "
                                SQL = SQL & "'" & .Rows(i).Item("Nama_Barang") & "', '" & .Rows(i).Item("Batch_Number") & "', '" & fullNewQrScrap & "', '" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Tgl_Produksi") & "', "
                                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Tgl_Expired") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Jenis") & "', '" & .Rows(i).Item("Number") & "', '" & kode_unik_print & "') "
                                ExecuteTrans(SQL)

                                KdUnikPrint.Add(kode_unik_print)

                            Else



                                SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2_Scrap (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
                                SQL = SQL & "Proses, Jumlah, Satuan, Nomor, id_routing, routing, Kode_unik_print)  "
                                SQL = SQL & "values ('" & KodePerusahaan & "', '" & selectedSplit & "', @newBarcode" & kode_unik_print & ", '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', '" & fullNewQrScrap & "', '" & .Rows(i).Item("Qr_Code") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Tgl_Produksi") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'X', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Number") & "', '" & .Rows(i).Item("Id_Routing") & "', '" & .Rows(i).Item("Routing") & "', '" & kode_unik_print & "') "
                                ExecuteTrans(SQL)

                                KdUnikPrintScrap.Add(kode_unik_print)
                            End If




                        Next
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

        Try
            OpenConn()

            Dim CrDoc As New Object

            Dim KertasBesar As String = "BarcodeFG"
            Dim KertasKecil As String = "BarcodeQC"

            'SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2 where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & kode_unik_print & "'"
            'Using Ds = BindingTrans(SQL)
            '    If Ds.Tables("MyTable").Rows.Count <> 0 Then

            '        '==========================
            '        '=     BARCODEE BESAR     =
            '        '==========================
            '        Dim printerDitemukan As Boolean = False
            '        For Each printer As String In PrinterSettings.InstalledPrinters
            '            If printer.ToLower() = PrinterBarcode.ToLower() Then
            '                printerDitemukan = True
            '                Exit For
            '            End If
            '        Next

            '        If printerDitemukan Then

            '            CrDoc = New N_EMI_Label_Barcode_GR_2

            '            With A_Place_For_Printing2
            '                CrDoc.SetDataSource(Ds)
            '                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '                CrDoc.PrintOptions.PrinterName = ""
            '                CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & kode_unik_print & "' "
            '                CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2"
            '                .Text = "Label Good Received 2"
            '                .CrystalReportViewer1.ReportSource = CrDoc
            '                .Refresh()
            '                .Show()
            '            End With

            '            '=====================================================

            '            '    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            '            '    CrDoc.SetDataSource(Ds)
            '            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & Kkode_unik_print & "' "
            '            '    CrDoc.PrintOptions.PrinterName = PrinterBarcode

            '            '    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

            '            '    Dim rawKind As Integer
            '            '    Dim foundPaper As Boolean = False
            '            '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            '            '    For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
            '            '        If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasBesar Then
            '            '            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
            '            '            CrDoc.PrintOptions.PaperSize = rawKind
            '            '            foundPaper = True
            '            '            Exit For
            '            '        End If
            '            '    Next

            '            '    If Not foundPaper Then
            '            '        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            '            '        MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            '            '    End If

            '            '    CrDoc.PrintToPrinter(1, False, 1, 2500)

            '        Else
            '            MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        End If

            '        printerDitemukan = False


            '    Else
            '        MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            '    End If


            'End Using


            For i As Integer = 0 To KdUnikPrint.Count - 1

                SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2 where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & KdUnikPrint(i) & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        '==========================
                        '=     BARCODEE BESAR     =
                        '==========================
                        Dim printerDitemukan As Boolean = False
                        For Each printer As String In PrinterSettings.InstalledPrinters
                            If printer.ToLower() = PrinterBarcode.ToLower() Then
                                printerDitemukan = True
                                Exit For
                            End If
                        Next

                        If printerDitemukan Then

                            CrDoc = New N_EMI_Label_Barcode_GR_2

                            'With A_Place_For_Printing2
                            '    CrDoc.SetDataSource(Ds)
                            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            '    CrDoc.PrintOptions.PrinterName = ""
                            '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & KdUnikPrint(i) & "' "
                            '    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2"
                            '    .Text = "Label Good Received 2"
                            '    .CrystalReportViewer1.ReportSource = CrDoc
                            '    .Refresh()
                            '    .Show()
                            'End With

                            '=====================================================

                            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & KdUnikPrint(i) & "' "
                            CrDoc.PrintOptions.PrinterName = PrinterBarcode

                            doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                            Dim rawKind As Integer
                            Dim foundPaper As Boolean = False
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasBesar Then
                                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                    CrDoc.PrintOptions.PaperSize = rawKind
                                    foundPaper = True
                                    Exit For
                                End If
                            Next

                            If Not foundPaper Then
                                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                                MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            End If

                            CrDoc.PrintToPrinter(1, False, 1, 2500)

                        Else
                            MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                        printerDitemukan = False


                    Else
                        MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End If


                End Using



            Next


            For i As Integer = 0 To KdUnikPrintScrap.Count - 1

                SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2_Scrap where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & KdUnikPrintScrap(i) & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        '==========================
                        '=     BARCODEE BESAR     =
                        '==========================
                        Dim printerDitemukan As Boolean = False
                        For Each printer As String In PrinterSettings.InstalledPrinters
                            If printer.ToLower() = PrinterBarcode.ToLower() Then
                                printerDitemukan = True
                                Exit For
                            End If
                        Next

                        If printerDitemukan Then

                            CrDoc = New N_EMI_Label_Barcode_GR_2_Scrap

                            'With A_Place_For_Printing2
                            '    CrDoc.SetDataSource(Ds)
                            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            '    CrDoc.PrintOptions.PrinterName = ""
                            '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Unik_Print} = '" & KdUnikPrintScrap(i) & "' "
                            '    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2 Scrap"
                            '    .Text = "Label Good Received 2"
                            '    .CrystalReportViewer1.ReportSource = CrDoc
                            '    .Refresh()
                            '    .Show()
                            'End With

                            '=====================================================

                            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Unik_Print} = '" & KdUnikPrintScrap(i) & "' "
                            CrDoc.PrintOptions.PrinterName = PrinterBarcode

                            doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                            Dim rawKind As Integer
                            Dim foundPaper As Boolean = False
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasBesar Then
                                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                    CrDoc.PrintOptions.PaperSize = rawKind
                                    foundPaper = True
                                    Exit For
                                End If
                            Next

                            If Not foundPaper Then
                                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                                MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            End If


                            CrDoc.PrintToPrinter(1, False, 1, 2500)

                        Else
                            MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                        printerDitemukan = False


                    Else
                        MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End If


                End Using



            Next

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



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



End Class