Imports System.IO

Public Class EMI_Validasi_GR_Display

    Dim JudulForm As String = "Display Validasi Penerimaan Barang Hasil Produksi"

    Dim arrcari, arr_tgl, arr_Lain As New ArrayList

    Dim Lv_NoTransaksi, Lv_NoSplit, Lv_Tanggal, Lv_Jam, Lv_Keterangan, Lv_KdBarang, Lv_NmBarang, Lv_JmlhPO, Lv_JmlhValidasi, Lv_Satuan, Lv_User As String

    Dim LvKeranjang_NoTransaksi, LvKeranjang_NoSplit, LvKeranjang_Keranjang As String

    Dim LvDetail_NoSplit, LvDetail_LokasiTujuan, LvDetail_Barcode, LvDetail_Routing, LvDetail_Jenis, LvDetail_Jumlah, LvDetail_Satuan As String


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
    'Dim item_JmlhValidasi As Integer = 7
    'Dim item_Satuan As Integer = 8
    Dim item_User As Integer = 7

    Dim itemKeranjang_NoTransaksi = 0
    Dim itemKeranjang_NoSplit = 1
    Dim itemKeranjang_Keranjang = 2


    Dim itemDetail_NoSplit As Integer = 0
    Dim itemDetail_LokasiTujuan As Integer = 1
    Dim itemDetail_Barode As Integer = 2
    Dim itemDetail_Routing As Integer = 3
    Dim itemDetail_Jenis As Integer = 4
    Dim itemDetail_Jumlah As Integer = 5
    Dim itemDetail_Satuan As Integer = 6





    Private Sub EMI_Display_Validasi_GR_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Validation.Columns.Clear()
        Lv_Validation.Columns.Add("No Transaksi", 120, HorizontalAlignment.Left) '0
        Lv_Validation.Columns.Add("No Split", 0, HorizontalAlignment.Left) '1
        Lv_Validation.Columns.Add("Tanggal", 110, HorizontalAlignment.Center) '2
        Lv_Validation.Columns.Add("Jam", 90, HorizontalAlignment.Center) '3
        Lv_Validation.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '4
        Lv_Validation.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left) '5
        Lv_Validation.Columns.Add("Keterangan", 290, HorizontalAlignment.Left) '6
        'Lv_Validation.Columns.Add("Jumlah Validasi", 120, HorizontalAlignment.Right) '7
        'Lv_Validation.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '8
        Lv_Validation.Columns.Add("User", 100, HorizontalAlignment.Center) '9
        Lv_Validation.View = View.Details

        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("No Split", 130, HorizontalAlignment.Left)
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
        Cmb_Lain.Items.Add("No Split") : arr_Lain.Add("c.No_Transaksi")
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

        Chk_HariIni.Checked = True

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
        'Lv_JmlhValidasi = Lv_Validation.Items(index).SubItems(item_JmlhValidasi).Text
        'Lv_Satuan = Lv_Validation.Items(index).SubItems(item_Satuan).Text
        Lv_User = Lv_Validation.Items(index).SubItems(item_User).Text

    End Sub

    Private Sub Get_Data_Keranjang(ByVal index As Integer)
        LvKeranjang_NoTransaksi = Dgv_Keranjang.Rows(index).Cells(itemKeranjang_NoTransaksi).value
        LvKeranjang_NoSplit = Dgv_Keranjang.Rows(index).Cells(itemKeranjang_NoSplit).value
        LvKeranjang_Keranjang = Dgv_Keranjang.Rows(index).Cells(itemKeranjang_Keranjang).value
    End Sub

    Private Sub Get_Data_ValidationDetail(ByVal index As Integer)

        LvDetail_NoSplit = Lv_Detail.Items(index).SubItems(itemDetail_NoSplit).Text
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
            Txt_KdBarang.Text = "" : Txt_Barang.Text = "" : Txt_Barcode.Text = "" : Txt_Jumlah.Text = "" : Txt_Jenis.Text = ""
            Dtp_Produksi.Value = Now.Date : Dtp_Expired.Value = Now.Date
            Cmb_Satuan.SelectedIndex = -1 : Cmb_Satuan.Text = ""
            'SQL = "select a.No_Transaksi, a.No_Production_Order, a.Status, a.Tanggal, a.Jam, c.Kode_Barang, d.Nama as Nama_Barang, a.Keterangan, "
            'SQL = SQL & "ISNULL(sum(b.Jumlah), 0) as JumlahValidasi, b.Satuan, a.UserID "

            'SQL = "select a.No_Transaksi, a.No_Production_Order, a.Status, a.Tanggal, a.Jam, c.Kode_Barang, d.Nama as Nama_Barang, a.Keterangan, a.UserID "
            'SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Split_Production_Order c, barang d "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = c.kode_perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            'SQL = SQL & "and a.No_Production_Order = c.No_Transaksi "
            'SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and c.Status is null "

            'If Chk_HariIni.Checked Then
            '    'Pasang And
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & "a.Tanggal between '"
            '    SQL = SQL & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' and '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            'End If

            'If Chk_Tanggal.Checked Then
            '    'Pasang And
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & arr_tgl.Item(Cmb_Tanggal.SelectedIndex) & " between '"
            '    SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            'End If

            'If Chk_Lain.Checked Then
            '    'Pasang And
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & arr_Lain.Item(Cmb_Lain.SelectedIndex) & " like '%" & Trim(Txt_ValuLain.Text) & "%' "
            'End If

            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            ''SQL = SQL & "group by a.No_Transaksi, a.No_Production_Order, a.Status, a.Tanggal, a.Jam, c.Kode_Barang, d.Nama, a.Keterangan, b.Satuan, a.UserID "
            'SQL = SQL & "group by a.No_Transaksi, a.No_Production_Order, a.Status, a.Tanggal, a.Jam, c.Kode_Barang, d.Nama, a.Keterangan, a.UserID "
            'SQL = SQL & "order by a.Tanggal, a.Jam "



            SQL = "select a.No_Transaksi, a.No_Production_Order, a.Status, a.Tanggal, a.Jam, c.Kode_Barang, d.Nama as Nama_Barang, a.Keterangan, a.UserID "
            SQL &= $"from Emi_Production_Results_Validation a "
            SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan  = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join Emi_Split_Production_Order c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Split = c.No_Transaksi and c.Status is NULL "
            SQL &= $"inner join Barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            'SQL &= $"and a.Status is NULl "
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
            SQL &= $"group by a.No_Transaksi, a.No_Production_Order, a.Status, a.Tanggal, a.Jam, c.Kode_Barang, d.Nama, a.Keterangan, a.UserID "
            SQL &= $"union all "
            SQL &= $"select a.No_Transaksi, a.No_Production_Order, a.Status, a.Tanggal, a.Jam, c.Kode_Barang, d.Nama as Nama_Barang, a.Keterangan, a.UserID "
            SQL &= $"from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Split_Production_Order c, barang d "
            SQL &= $"where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = c.kode_perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL &= $"and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"and a.No_Production_Order = c.No_Transaksi "
            SQL &= $"and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL &= $"and c.Status is null "
            SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
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
            SQL &= $"and not EXISTS ( "
            SQL &= $"select distinct z.No_Transaksi "
            SQL &= $"from Emi_Production_Results_Validation z "
            SQL &= $"inner join Emi_Production_Results_Validation_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Transaksi = x.No_Transaksi "
            SQL &= $"inner join Emi_Split_Production_Order k on x.Kode_Perusahaan = k.Kode_Perusahaan and x.No_Split = k.No_Transaksi and k.Status is NULL "
            SQL &= $"where z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL &= $"and z.Status is NULl "
            SQL &= $"and z.No_Transaksi = a.No_Transaksi ) "
            SQL &= $"group by a.No_Transaksi, a.No_Production_Order, a.Status, a.Tanggal, a.Jam, c.Kode_Barang, d.Nama, a.Keterangan, a.UserID "
            SQL &= $"order by a.Tanggal, a.Jam "
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
                            Lv.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Keterangan")) = "", "-", .Rows(i).Item("Keterangan")))
                            'Lv.SubItems.Add(Format(.Rows(i).Item("JumlahValidasi"), "N2"))
                            'Lv.SubItems.Add(.Rows(i).Item("Satuan"))
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
            Txt_KdBarang.Text = "" : Txt_Barang.Text = "" : Txt_Barcode.Text = "" : Txt_Jumlah.Text = "" : Txt_Jenis.Text = ""
            Dtp_Produksi.Value = Now.Date : Dtp_Expired.Value = Now.Date
            Cmb_Satuan.SelectedIndex = -1 : Cmb_Satuan.Text = ""
            SQL = "select distinct a.No_Transaksi, a.Nomor from Emi_Production_Results_Validation_Detail a, Emi_Production_Results_Validation b "
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
                            'Dgv_Keranjang.Rows(i).Cells(itemKeranjang_NoSplit).value = .Rows(i).Item("No_Production_Order")
                            Dgv_Keranjang.Rows(i).Cells(itemKeranjang_NoSplit).value = "-"
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


            Txt_KdBarang.Text = "" : Txt_Barang.Text = "" : Txt_Barcode.Text = "" : Txt_Jumlah.Text = "" : Txt_Jenis.Text = ""
            Dtp_Produksi.Value = Now.Date : Dtp_Expired.Value = Now.Date
            Cmb_Satuan.SelectedIndex = -1 : Cmb_Satuan.Text = ""
            'SQL = "select a.Kode_Perusahaan, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang, d.Qr_Code, d.Kode_Unik_Berjalan, d.Batch_Number,  "
            'SQL &= $"d.Tgl_Produksi, d.Tgl_Expired, sum(b.Jumlah) as Jumlah, b.Satuan, "
            'SQL &= $"case when b.jenis = 'REJECTED' then 'Disqualified ' else b.jenis end as Jenis, "
            'SQL &= $"b.Nomor as Number "
            'SQL &= $"from Emi_Production_Results_Validation a "
            'SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            'SQL &= $"inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            'SQL &= $"inner join barang_sn d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and b.Serial_Number_Akhir = d.Serial_Number "
            'SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            ''SQL &= $"and a.Status is null "
            'SQL &= $"and a.No_Transaksi = '{LvKeranjang_NoTransaksi}' "
            'SQL &= $"and b.Nomor = '{LvKeranjang_Keranjang}' "
            'SQL &= $"group by a.Kode_Perusahaan, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama, d.Qr_Code, d.Kode_Unik_Berjalan, d.Batch_Number, d.Tgl_Produksi, d.Tgl_Expired, b.Satuan, b.jenis, b.Nomor "

            'SQL &= $"union all "

            SQL = "select a.Kode_Perusahaan, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang, d.Qr_Code, d.Kode_Unik_Berjalan,  "
            SQL &= $"d.Tgl_Produksi, d.Tgl_Expired, sum(b.Jumlah) as Jumlah, b.Satuan, "
            SQL &= $"case when b.jenis = 'REJECTED' then 'Disqualified ' else b.jenis end as Jenis, "
            SQL &= $"b.Nomor as Number "
            SQL &= $"from Emi_Production_Results_Validation a "
            SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_awal = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"inner join barang_sn d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner_awal = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and b.Serial_Number_Tujuan = d.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{LvKeranjang_NoTransaksi}' "
            SQL &= $"and b.Nomor = '{LvKeranjang_Keranjang}' "
            SQL &= $"group by a.Kode_Perusahaan, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama, d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, b.Satuan, b.jenis, b.Nomor "

            SQL &= $"union all "

            SQL &= "select a.Kode_Perusahaan, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang, d.Qr_Code, d.Kode_Unik_Berjalan,  "
            SQL &= $"d.Tgl_Produksi, d.Tgl_Expired, sum(b.Jumlah) as Jumlah, b.Satuan, "
            SQL &= $"case when b.jenis = 'REJECTED' then 'Disqualified ' else b.jenis end as Jenis, "
            SQL &= $"b.Nomor as Number "
            SQL &= $"from Emi_Production_Results_Validation a "
            SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_awal = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"inner join barang_sn d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and b.Serial_Number_Tujuan = d.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{LvKeranjang_NoTransaksi}' "
            SQL &= $"and b.Nomor = '{LvKeranjang_Keranjang}' "
            SQL &= $"group by a.Kode_Perusahaan, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama, d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, b.Satuan, b.jenis, b.Nomor "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Txt_KdBarang.Text = Dr("Kode_Barang") : Txt_Barang.Text = Dr("Nama_Barang")
                    Txt_Barcode.Text = $"{Dr("Qr_Code")}-{Dr("Kode_Unik_Berjalan")}"
                    Txt_Jenis.Text = Dr("Jenis")
                    Dtp_Produksi.Value = Dr("Tgl_Produksi") : Dtp_Expired.Value = Dr("Tgl_Expired")

                    If Dr("Jenis").ToString.ToUpper.Trim = "FINISHED GOOD" Then
                        Txt_Jumlah.Text = Format(Dr("Jumlah"), "N0")
                    Else
                        Txt_Jumlah.Text = Format(Dr("Jumlah"), "N4")
                    End If

                    Cmb_Satuan.Text = Dr("Satuan")

                End If
            End Using

            Lv_Detail.Items.Clear()

            'SQL = "select a.Kode_Perusahaan, b.No_Split as No_Production_Order, b.Nomor, b.Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang,  "
            'SQL &= $"b.Batch_Number, d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, sum(b.Jumlah) as Jumlah, c.Satuan, "
            'SQL &= $"case when b.jenis = 'REJECTED' then 'Disqualified ' else b.jenis end as Jenis, "
            'SQL &= $"b.Nomor as Number, f.Id_Routing, g.Keterangan as Routing "
            'SQL &= $"from Emi_Production_Results_Validation a "
            'SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            'SQL &= $"inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            'SQL &= $"inner join barang_sn d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and b.Serial_Number_Akhir = d.Serial_Number "
            'SQL &= $"inner join Emi_Split_Production_Order e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.No_Split = e.No_Transaksi and e.Status is NULL "
            'SQL &= $"inner join EMI_Order_Produksi f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_PO = f.No_Faktur and f.Status is NULL "
            'SQL &= $"inner join EMI_Master_Routing g on f.Kode_Perusahaan = g.Kode_Perusahaan and f.Id_Routing = g.Id_Routing "
            'SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            'SQL &= $"and a.No_Transaksi = '{LvKeranjang_NoTransaksi}' "
            'SQL &= $"and b.Nomor = '{LvKeranjang_Keranjang}' "
            'SQL &= $"group by a.Kode_Perusahaan, b.No_Split, b.Nomor, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama, b.Batch_Number, d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, c.Satuan, b.jenis, "
            'SQL &= $"b.Nomor, f.Id_Routing, g.Keterangan "

            'SQL &= $"union all "

            SQL = "select a.Kode_Perusahaan, b.No_Split as No_Production_Order, b.Nomor, b.Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang,  "
            SQL &= $"d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, sum(b.Jumlah) as Jumlah, c.Satuan, "
            SQL &= $"case when b.jenis = 'REJECTED' then 'Disqualified ' else b.jenis end as Jenis, "
            SQL &= $"b.Nomor as Number, f.Id_Routing, g.Keterangan as Routing "
            SQL &= $"from Emi_Production_Results_Validation a "
            SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Awal = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"inner join barang_sn d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner_Awal = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and b.Serial_Number_Tujuan = d.Serial_Number "
            SQL &= $"inner join Emi_Split_Production_Order e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.No_Split = e.No_Transaksi and e.Status is NULL "
            SQL &= $"inner join EMI_Order_Produksi f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_PO = f.No_Faktur and f.Status is NULL "
            SQL &= $"inner join EMI_Master_Routing g on f.Kode_Perusahaan = g.Kode_Perusahaan and f.Id_Routing = g.Id_Routing "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{LvKeranjang_NoTransaksi}' "
            SQL &= $"and b.Nomor = '{LvKeranjang_Keranjang}' "
            SQL &= $"group by a.Kode_Perusahaan, b.No_Split, b.Nomor, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama, d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, c.Satuan, b.jenis, "
            SQL &= $"b.Nomor, f.Id_Routing, g.Keterangan "

            SQL &= $"union all "

            SQL &= "select a.Kode_Perusahaan, b.No_Split as No_Production_Order, b.Nomor, b.Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang,  "
            SQL &= $"d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, sum(b.Jumlah) as Jumlah, c.Satuan, "
            SQL &= $"case when b.jenis = 'REJECTED' then 'Disqualified ' else b.jenis end as Jenis, "
            SQL &= $"b.Nomor as Number, f.Id_Routing, g.Keterangan as Routing "
            SQL &= $"from Emi_Production_Results_Validation a "
            SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Awal = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"inner join barang_sn d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and b.Serial_Number_Tujuan = d.Serial_Number "
            SQL &= $"inner join Emi_Split_Production_Order e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.No_Split = e.No_Transaksi and e.Status is NULL "
            SQL &= $"inner join EMI_Order_Produksi f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_PO = f.No_Faktur and f.Status is NULL "
            SQL &= $"inner join EMI_Master_Routing g on f.Kode_Perusahaan = g.Kode_Perusahaan and f.Id_Routing = g.Id_Routing "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{LvKeranjang_NoTransaksi}' "
            SQL &= $"and b.Nomor = '{LvKeranjang_Keranjang}' "
            SQL &= $"group by a.Kode_Perusahaan, b.No_Split, b.Nomor, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama, d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, c.Satuan, b.jenis, "
            SQL &= $"b.Nomor, f.Id_Routing, g.Keterangan "

            SQL &= $"order by b.No_Split "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("No_Production_Order"))
                    Lv.SubItems.Add(Dr("Lokasi_Tujuan"))
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
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
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


        Dim kode_unik_print As String = ""

        Dim KdUnikPrint, KdUnikPrintScrap As New ArrayList

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Cetak_Ulang_Barcode_GR_2") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Cetak Ulang Barcode", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Dim selectedSplit As String = Lv_Validation.FocusedItem.SubItems(item_NoSplit).Text
            Dim selectedFaktur As String = Lv_Validation.FocusedItem.SubItems(item_NoTransaksi).Text
            Dim SelectedKeranjang As String = Dgv_Keranjang.CurrentRow.Cells(itemKeranjang_Keranjang).Value.ToString
            Dim SelectedBarcode As String = Lv_Detail.FocusedItem.SubItems(itemDetail_Barode).Text


            '===========================================
            '=     CEK APAKAH TRANSAKSI DIBATALKAN     =
            '===========================================
            SQL = "select Kode_Perusahaan from Emi_Production_Results_Validation "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and status = 'Y' "
            SQL = SQL & "and No_Transaksi = '" & selectedFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Cetak Barcode Tidak Bisa Dilakukan, Karena No Transaksi Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=======================================
            '=     CEK APAKAH SPLIT DIBATALKAN     =
            '=======================================
            SQL = "select distinct b.No_Split "
            SQL &= $"from Emi_Production_Results_Validation a "
            SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join Barang_SN c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.Status is NULL "
            SQL &= $"and a.No_Transaksi = '{selectedFaktur}' "
            SQL &= $"and b.Nomor = '{SelectedKeranjang}' "
            SQL &= $"and (c.Qr_Code+'-'+c.Kode_Unik_Berjalan) = '{SelectedBarcode}' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "select Status from Emi_Split_Production_Order "
                            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and No_Transaksi = '{ .Rows(i).Item("No_Split")}' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"No Split { .Rows(i).Item("No_Split")} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show($"No Split { .Rows(i).Item("No_Split")} Tidak Ditemnukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                        Next
                    End If
                End With
            End Using


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


            'SQL = "with cte as( "
            'SQL = SQL & "select b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            'SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan,   "
            'SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
            'SQL = SQL & "c.nomor as Number, "

            'SQL = SQL & "e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing, b.jam AS Jam_Transaksi "

            'SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN e, Emi_Split_Production_Order f, EMI_Order_Produksi g, EMI_Master_Routing h "
            'SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            'SQL = SQL & "and b.kode_perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and g.Kode_Perusahaan = h.Kode_Perusahaan "
            'SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            'SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
            'SQL = SQL & "and b.No_Production_Order = f.No_Transaksi "
            'SQL = SQL & "and f.No_PO = g.No_Faktur "
            'SQL = SQL & "and g.Id_Routing = h.Id_Routing "
            'SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and b.No_Production_Order = '" & selectedSplit & "' "
            'SQL = SQL & "and b.No_Transaksi = '" & selectedFaktur & "'  and c.Jenis<>'Finished Good' "
            'SQL = SQL & "AND (e.Qr_Code +'-' +e.Kode_Unik_Berjalan) = '" & SelectedBarcode & "' "

            'SQL = SQL & "union all "

            'SQL = SQL & "select b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            'SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan,  "
            'SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
            'SQL = SQL & "c.nomor as Number, "

            'SQL = SQL & "e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing, b.jam AS Jam_Transaksi "

            'SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN_sementara e, Emi_Split_Production_Order f, EMI_Order_Produksi g, EMI_Master_Routing h "
            'SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            'SQL = SQL & "and b.kode_perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and g.Kode_Perusahaan = h.Kode_Perusahaan "
            'SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            'SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
            'SQL = SQL & "and b.No_Production_Order = f.No_Transaksi "
            'SQL = SQL & "and f.No_PO = g.No_Faktur "
            'SQL = SQL & "and g.Id_Routing = h.Id_Routing "
            'SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and b.No_Production_Order = '" & selectedSplit & "' "
            'SQL = SQL & "and b.No_Transaksi = '" & selectedFaktur & "'  and c.Jenis='Finished Good' "
            'SQL = SQL & "AND (e.Qr_Code +'-' +e.Kode_Unik_Berjalan) = '" & SelectedBarcode & "' "

            ''KODE LAMA
            ''SQL = SQL & ") select kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number, Id_Routing, Routing "
            ''SQL = SQL & "from cte "
            ''SQL = SQL & "group by kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, Satuan, Jenis, Number, Nomor, Id_Routing, Routing "
            'SQL = SQL & ") select a.kode_perusahaan, a.no_production_order, a.Lokasi_Tujuan, a.Kode_Barang, a.Nama_Barang, a.Kode_Unik_Berjalan, a.Batch_Number, a.Qr_Code,  "
            'SQL = SQL & "isnull((select top 1 z.Tgl_Produksi from cte z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.No_Production_Order = z.No_Production_Order and a.Kode_Unik_Berjalan = z.Kode_Unik_Berjalan), '') as Tgl_Produksi, "
            'SQL = SQL & "isnull((select top 1 z.Tgl_Expired from cte z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.No_Production_Order = z.No_Production_Order and a.Kode_Unik_Berjalan = z.Kode_Unik_Berjalan), '') as Tgl_Expired, "
            'SQL = SQL & "sum(a.Jumlah) as Jumlah, a.Satuan, a.Jenis, a.Number, a.Id_Routing, a.Routing, a.Jam_Transaksi "
            'SQL = SQL & "from cte a "
            'SQL = SQL & "group by a.kode_perusahaan, a.no_production_order, a.Lokasi_Tujuan, a.Kode_Barang, a.Nama_Barang, a.Kode_Unik_Berjalan, a.Batch_Number, a.Qr_Code, a.Satuan, a.Jenis, a.Number, a.Nomor, a.Id_Routing, a.Routing, a.Jam_Transaksi "




            SQL = ";with Cte as( "
            'SQL &= $"select a.Kode_Perusahaan, a.No_Transaksi, b.No_Split as No_Production_Order, b.Nomor, b.Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang, "
            'SQL &= $"b.Batch_Number, d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, sum(b.Jumlah) as Jumlah, c.Satuan, "
            'SQL &= $"case when b.jenis = 'REJECTED' then 'Disqualified ' else b.jenis end as Jenis, "
            'SQL &= $"b.Nomor as Number, f.Id_Routing, g.Keterangan as Routing, a.jam AS Jam_Transaksi "
            'SQL &= $"from Emi_Production_Results_Validation a "
            'SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            'SQL &= $"inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            'SQL &= $"inner join barang_sn d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and b.Serial_Number_Akhir = d.Serial_Number "
            'SQL &= $"inner join Emi_Split_Production_Order e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.No_Split = e.No_Transaksi and e.Status is NULL "
            'SQL &= $"inner join EMI_Order_Produksi f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_PO = f.No_Faktur and f.Status is NULL "
            'SQL &= $"inner join EMI_Master_Routing g on f.Kode_Perusahaan = g.Kode_Perusahaan and f.Id_Routing = g.Id_Routing "
            'SQL &= $"group by a.Kode_Perusahaan, b.No_Split, b.Nomor, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama, b.Batch_Number, d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, c.Satuan, b.jenis, "
            'SQL &= $"b.Nomor, f.Id_Routing, g.Keterangan, a.jam, a.No_Transaksi "
            'SQL &= $"union all "
            SQL &= $"select a.Kode_Perusahaan, a.No_Transaksi, b.No_Split as No_Production_Order, b.Nomor, b.Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang, "
            SQL &= $"b.Batch_Number, d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, sum(b.Jumlah) as Jumlah, c.Satuan, "
            SQL &= $"case when b.jenis = 'REJECTED' then 'Disqualified ' else b.jenis end as Jenis, "
            SQL &= $"b.Nomor as Number, f.Id_Routing, g.Keterangan as Routing, a.jam AS Jam_Transaksi "
            SQL &= $"from Emi_Production_Results_Validation a "
            SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Awal = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL &= $"inner join barang_sn d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Barang = d.Kode_Barang and b.Serial_Number_Tujuan = d.Serial_Number "
            SQL &= $"inner join Emi_Split_Production_Order e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.No_Split = e.No_Transaksi and e.Status is NULL "
            SQL &= $"inner join EMI_Order_Produksi f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_PO = f.No_Faktur and f.Status is NULL "
            SQL &= $"inner join EMI_Master_Routing g on f.Kode_Perusahaan = g.Kode_Perusahaan and f.Id_Routing = g.Id_Routing "
            SQL &= $"group by a.Kode_Perusahaan, b.No_Split, b.Nomor, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama, b.Batch_Number, d.Qr_Code, d.Kode_Unik_Berjalan, d.Tgl_Produksi, d.Tgl_Expired, c.Satuan, b.jenis, "
            SQL &= $"b.Nomor, f.Id_Routing, g.Keterangan, a.jam, a.No_Transaksi "
            SQL &= $") "
            SQL &= $"select Kode_Perusahaan, String_AGG(No_Production_Order, ', ') as No_Production_Order, nomor, Lokasi_Tujuan, Kode_Barang, Nama_Barang, max(Batch_Number) as Batch_Number, Qr_Code,  "
            SQL &= $"Kode_Unik_Berjalan, min(Tgl_Produksi) as Tgl_Produksi, min(Tgl_Expired) as Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number, Jam_Transaksi  "
            SQL &= $"from cte "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and No_Transaksi = '{selectedFaktur}' "
            SQL &= $"and Nomor = '{SelectedKeranjang}' "
            SQL &= $"and (Qr_Code+'-'+Kode_Unik_Berjalan) = '{SelectedBarcode}' "
            SQL &= $"group by Kode_Perusahaan, nomor, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Qr_Code, Kode_Unik_Berjalan, "
            SQL &= $"Satuan, Jenis, Number, Jam_Transaksi "
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

                            Dim fullNewQr As String = .Rows(i).Item("Qr_Code") & "-" & .Rows(i).Item("Kode_Unik_Berjalan")

#Region "Kode Baru"

                            Using ImgBarcode2 As Image = Generate_QR_NoPadding(fullNewQr)
                                Using ms2 As New MemoryStream()
                                    ImgBarcode2.Save(ms2, Imaging.ImageFormat.Jpeg)
                                    Dim rawData2 As Byte() = ms2.ToArray()

                                    Dim param2 As String = "@newBarcode" & kode_unik_print
                                    Cmd.Parameters.Add(param2, SqlDbType.Image).Value = rawData2
                                End Using
                            End Using

                            Dim barcode As String = "@newBarcode" & kode_unik_print

#End Region

#Region "Kode Lama"

                            'barcode.Image = Nothing

                            'Barcode.Image = Generate_QR_NoPadding(fullNewQr)

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

#End Region

                            Dim asdada As String = .Rows(i).Item("Jenis").ToString.ToUpper.Trim

                            If .Rows(i).Item("Jenis").ToString.ToUpper.Trim = "FINISHED GOOD" Then

                                SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2 (Kode_Perusahaan, No_Split, Kode_Barang, Barcode, Nama_Barang, Batch_Number, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, Tgl_Expired, Jam_Expired, Jumlah, Satuan, Jenis, Number, Kode_Unik_Print) "
                                SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "', '" & .Rows(i).Item("Kode_Barang") & "', " & barcode & ", "
                                'SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "', '" & .Rows(i).Item("Kode_Barang") & "', @newBarcode" & kode_unik_print & ", "
                                SQL = SQL & "'" & .Rows(i).Item("Nama_Barang") & "', '" & .Rows(i).Item("Batch_Number") & "', '" & fullNewQr & "', '" & .Rows(i).Item("Qr_Code") & "', '" & Format(.Rows(i).Item("Tgl_Produksi"), "yyyy-MM-dd") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Jam_Transaksi") & "', '" & Format(.Rows(i).Item("Tgl_Expired"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("Jam_Transaksi") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Jenis") & "', '" & .Rows(i).Item("Number") & "', '" & kode_unik_print & "') "
                                ExecuteTrans(SQL)

                                KdUnikPrint.Add(kode_unik_print)

                            ElseIf .Rows(i).Item("Jenis").ToString.ToUpper.Trim = "DISQUALIFIED" Then
                                SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2 (Kode_Perusahaan, No_Split, Kode_Barang, Barcode, Nama_Barang, Batch_Number, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, Tgl_Expired, Jam_Expired, Jumlah, Satuan, Jenis, Number, Kode_Unik_Print) "
                                SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "', '" & .Rows(i).Item("Kode_Barang") & "', " & barcode & ", "
                                'SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "', '" & .Rows(i).Item("Kode_Barang") & "', @newBarcode" & kode_unik_print & ", "
                                SQL = SQL & "'" & .Rows(i).Item("Nama_Barang") & "', '" & .Rows(i).Item("Batch_Number") & "', '" & fullNewQr & "', '" & .Rows(i).Item("Qr_Code") & "', '" & Format(.Rows(i).Item("Tgl_Produksi"), "yyyy-MM-dd") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Jam_Transaksi") & "', '" & Format(.Rows(i).Item("Tgl_Expired"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("Jam_Transaksi") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Jenis") & "', '" & .Rows(i).Item("Number") & "', '" & kode_unik_print & "') "
                                ExecuteTrans(SQL)

                                KdUnikPrint.Add(kode_unik_print)

                            Else

                                Dim idRouting As String = ""
                                Dim Routing As String = ""
                                SQL = "select b.Id_Routing, b.Keterangan as Routing "
                                SQL &= $"from EMI_Order_Produksi a "
                                SQL &= $"inner join EMI_Master_Routing b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Routing = b.Id_Routing "
                                SQL &= $"inner join Emi_Split_Production_Order c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_PO "
                                SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
                                SQL &= $"and c.No_Transaksi = '{ .Rows(i).Item("no_production_order")}' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        idRouting = Dr("Id_Routing")
                                        Routing = Dr("Routing")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Routing Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2_Scrap (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
                                SQL = SQL & "Proses, Jumlah, Satuan, Nomor, id_routing, routing, Kode_unik_print)  "
                                SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "', " & barcode & ", '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', '" & fullNewQr & "', '" & .Rows(i).Item("Qr_Code") & "', "
                                'SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "',  @newBarcode" & kode_unik_print & ", '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', '" & fullNewQr & "', '" & .Rows(i).Item("Qr_Code") & "', "
                                SQL = SQL & "'" & Format(.Rows(i).Item("Tgl_Produksi"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("Jam_Transaksi") & "', 'X', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Number") & "', '" & idRouting & "', '" & Routing & "', '" & kode_unik_print & "') "
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


            For i As Integer = 0 To KdUnikPrint.Count - 1

                SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2 where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & KdUnikPrint(i) & "'"
                Dim SelectionFormula As String = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & KdUnikPrint(i) & "' "

                Cetak_Barcode(New N_EMI_Label_Barcode_GR_2, "Label Good Received 2", SQL, SelectionFormula, PrinterBarcode, KertasBesar)

            Next

            For i As Integer = 0 To KdUnikPrintScrap.Count - 1

                SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2_Scrap where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & KdUnikPrintScrap(i) & "'"
                Dim SelectionFormula As String = "{N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Unik_Print} = '" & KdUnikPrintScrap(i) & "' "

                Cetak_Barcode(New N_EMI_Label_Barcode_GR_2_Scrap, "Label Good Received 2 Scrap", SQL, SelectionFormula, PrinterBarcode, KertasBesar)

            Next


#Region "Kode Lama"

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


            'For i As Integer = 0 To KdUnikPrint.Count - 1

            '    SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2 where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & KdUnikPrint(i) & "'"
            '    Using Ds = BindingTrans(SQL)
            '        If Ds.Tables("MyTable").Rows.Count <> 0 Then

            '            '==========================
            '            '=     BARCODEE BESAR     =
            '            '==========================
            '            Dim printerDitemukan As Boolean = False
            '            For Each printer As String In PrinterSettings.InstalledPrinters
            '                If printer.ToLower() = PrinterBarcode.ToLower() Then
            '                    printerDitemukan = True
            '                    Exit For
            '                End If
            '            Next

            '            If printerDitemukan Then

            '                CrDoc = New N_EMI_Label_Barcode_GR_2

            '                'With A_Place_For_Printing2
            '                '    CrDoc.SetDataSource(Ds)
            '                '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '                '    CrDoc.PrintOptions.PrinterName = ""
            '                '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & KdUnikPrint(i) & "' "
            '                '    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2"
            '                '    .Text = "Label Good Received 2"
            '                '    .CrystalReportViewer1.ReportSource = CrDoc
            '                '    .Refresh()
            '                '    .Show()
            '                'End With

            '                '=====================================================

            '                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            '                CrDoc.SetDataSource(Ds)
            '                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '                CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & KdUnikPrint(i) & "' "
            '                CrDoc.PrintOptions.PrinterName = PrinterBarcode

            '                doctoprint.PrinterSettings.PrinterName = PrinterBarcode

            '                Dim rawKind As Integer
            '                Dim foundPaper As Boolean = False
            '                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            '                For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
            '                    If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasBesar Then
            '                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
            '                        CrDoc.PrintOptions.PaperSize = rawKind
            '                        foundPaper = True
            '                        Exit For
            '                    End If
            '                Next

            '                If Not foundPaper Then
            '                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            '                    MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            '                End If

            '                CrDoc.PrintToPrinter(1, False, 1, 2500)

            '            Else
            '                MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            End If

            '            printerDitemukan = False


            '        Else
            '            MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            '        End If


            '    End Using

            'Next


            'For i As Integer = 0 To KdUnikPrintScrap.Count - 1

            '    SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2_Scrap where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & KdUnikPrintScrap(i) & "'"
            '    Using Ds = BindingTrans(SQL)
            '        If Ds.Tables("MyTable").Rows.Count <> 0 Then

            '            '==========================
            '            '=     BARCODEE BESAR     =
            '            '==========================
            '            Dim printerDitemukan As Boolean = False
            '            For Each printer As String In PrinterSettings.InstalledPrinters
            '                If printer.ToLower() = PrinterBarcode.ToLower() Then
            '                    printerDitemukan = True
            '                    Exit For
            '                End If
            '            Next

            '            If printerDitemukan Then

            '                CrDoc = New N_EMI_Label_Barcode_GR_2_Scrap

            '                'With A_Place_For_Printing2
            '                '    CrDoc.SetDataSource(Ds)
            '                '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '                '    CrDoc.PrintOptions.PrinterName = ""
            '                '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Unik_Print} = '" & KdUnikPrintScrap(i) & "' "
            '                '    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2 Scrap"
            '                '    .Text = "Label Good Received 2"
            '                '    .CrystalReportViewer1.ReportSource = CrDoc
            '                '    .Refresh()
            '                '    .Show()
            '                'End With

            '                '=====================================================

            '                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            '                CrDoc.SetDataSource(Ds)
            '                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '                CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Unik_Print} = '" & KdUnikPrintScrap(i) & "' "
            '                CrDoc.PrintOptions.PrinterName = PrinterBarcode

            '                doctoprint.PrinterSettings.PrinterName = PrinterBarcode

            '                Dim rawKind As Integer
            '                Dim foundPaper As Boolean = False
            '                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            '                For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
            '                    If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasBesar Then
            '                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
            '                        CrDoc.PrintOptions.PaperSize = rawKind
            '                        foundPaper = True
            '                        Exit For
            '                    End If
            '                Next

            '                If Not foundPaper Then
            '                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            '                    MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            '                End If


            '                CrDoc.PrintToPrinter(1, False, 1, 2500)

            '            Else
            '                MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            End If

            '            printerDitemukan = False


            '        Else
            '            MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            '        End If


            '    End Using



            'Next

#End Region


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


    Private Sub BatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalToolStripMenuItem.Click
        If Lv_Validation.Items.Count = 0 Or Lv_Validation.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            Dim JudulNotif As String = "Pembatalan Validasi Penerimaan Barang"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Validasi_Penerimaan_Barang") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Vallidasi Penerimaan Barang", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan No Transaksi Ini?", JudulNotif, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim No_Transaksi As String = Lv_Validation.FocusedItem.Text

            '=======================================================
            '=     CEK APAKAH DATA SUDAH DIBATALKAN SEBELUMNYA     =
            '=======================================================
            SQL = "select top 1 a.Kode_Perusahaan "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status = 'Y' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Tidak Bisa Dilakukan, Karena Data Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '================================================================
            '=     CEK APAKAH DATA SUDAH MASUK STEP MILITARY SAMPLING 2     =
            '================================================================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from Emi_Production_Results_Validation a, N_EMI_Military_Sampling b "
            SQL = SQL & "where a.No_Production_Order = b.No_Split "
            SQL = SQL & "and a.Status is null and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.No_GR = 2 "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Tidak Bisa Dilakukan, Karena Data Sudah Masuk Step Military Sampling 2", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '========================================
            '=     CEK APAKAH SUDAH TUTUP SALDO     =
            '========================================
            Dim HasData As Boolean = False
            Dim TglTransaksi As DateTime
            SQL = "select Tanggal from Emi_Production_Results_Validation "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and Status is NULL "
            SQL &= $"and No_Transaksi = '{No_Transaksi}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    HasData = True
                    TglTransaksi = Dr("Tanggal")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Transaksi Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If HasData Then
                If CekSudahTutupSaldo(TglTransaksi) = "Y" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan tidak dapat dilakukan karena No Transaksi Sudah Tutup Saldo", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If

            '=====================
            '=     CEK BULAN     =
            '=====================
            If Not tgl_skg.Month = TglTransaksi.Month Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Pembatalan tidak dapat dilakukan karena Sudah Melewati Bulan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '=========================
            '=     ROLLBACK DATA     =
            '=========================



            SQL = "select a.No_Transaksi, a.No_Production_Order, b.Kode_Stock_Owner_Awal, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang,  "
            SQL = SQL & "b.Serial_Number_Awal, b.Serial_Number_Tujuan, b.Serial_Number_Akhir, b.Jumlah, b.Satuan, b.Jenis, "
            SQL = SQL & "b.Kode_Voucher1, b.Kode_Voucher2 "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "order by a.Jam, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim So_Awal As String = .Rows(i).Item("Kode_Stock_Owner_Awal")
                            Dim So_Tujuan As String = If(General_Class.CekNULL(.Rows(i).Item("Serial_Number_Akhir")) = "", .Rows(i).Item("Kode_Stock_Owner_awal"), .Rows(i).Item("Kode_Stock_Owner_Tujuan"))
                            Dim Kd_Barang As String = .Rows(i).Item("Kode_Barang")
                            Dim Sn_Awal As String = .Rows(i).Item("Serial_Number_Awal")
                            Dim Sn_Tujuan As String = If(General_Class.CekNULL(.Rows(i).Item("Serial_Number_Akhir")) = "", .Rows(i).Item("Serial_Number_Tujuan"), .Rows(i).Item("Serial_Number_Akhir"))
                            Dim Sn_Tujuan_Scrap As String = .Rows(i).Item("Serial_Number_Tujuan")
                            Dim Jenis As String = .Rows(i).Item("Jenis")
                            Dim Jumlah As String = .Rows(i).Item("Jumlah")
                            Dim Satuan As String = .Rows(i).Item("Satuan")

                            Dim KodeVoucher_1 As String = .Rows(i).Item("Kode_Voucher1")
                            Dim KodeVoucher_2 As String = .Rows(i).Item("Kode_Voucher2")


                            If Jenis.ToUpper = "FINISHED GOOD" Then

                                Dim satuan_brg As String = ""
                                SQL = "select satuan from barang where Kode_Stock_Owner = '" & So_Awal & "' and Kode_Barang = '" & Kd_Barang & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        satuan_brg = Dr("satuan")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Pembatalan Tidak Bisa Dilakukan, Karena Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                Dim Jumlah_Kecil As Double = 0
                                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Kd_Barang & "', "
                                SQL = SQL & "'" & Satuan & "', '" & satuan_brg & "', '" & HilangkanTanda(Jumlah) & "' ) as hasil"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Jumlah_Kecil = Dr("hasil")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Pembatalan Tidak Bisa Dilakukan, Karena Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '=========================================
                                '=     CEK APAKAH STOCK PADA SN UTUH     =
                                '=========================================
                                SQL = "select Jumlah from Barang_SN "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner = '" & So_Tujuan & "' "
                                SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                SQL = SQL & "and Serial_Number = '" & Sn_Tujuan & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        If HilangkanTanda(Dr("Jumlah")) <> HilangkanTanda(Jumlah) Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Pembatalan Tidak Bisa Dilakukan, Karena Data Barang Sudah Digunakan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        Dr.Close()
                                        '=======================
                                        '=     UPDATE DATA     =
                                        '=======================
                                        SQL = "update Barang_SN set Jumlah = Jumlah - " & HilangkanTanda(Jumlah) & ", Jumlah_Bags = Jumlah_Bags - " & HilangkanTanda(0) & " "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & So_Tujuan & "' "
                                        SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                        SQL = SQL & "and Serial_Number = '" & Sn_Tujuan & "' "
                                        ExecuteTrans(SQL)

                                        SQL = "select Kode_Perusahaan from barang "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & So_Tujuan & "' "
                                        SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                        Using Dr2 = OpenTrans(SQL)
                                            If Dr2.Read Then

                                                Dr2.Close()
                                                SQL = "update barang set Good_Stock = Good_Stock - " & HilangkanTanda(Jumlah_Kecil) & ", Jumlah_Bags = Jumlah_Bags - " & HilangkanTanda(0) & " "
                                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "and Kode_Stock_Owner = '" & So_Tujuan & "' "
                                                SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                                ExecuteTrans(SQL)

                                            Else
                                                Dr2.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End Using


                                        SQL = "update Barang_SN set Jumlah = Jumlah + " & HilangkanTanda(Jumlah_Kecil) & ", Jumlah_Bags = Jumlah_Bags + " & HilangkanTanda(0) & " "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & So_Awal & "' "
                                        SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                        SQL = SQL & "and Serial_Number = '" & Sn_Awal & "' "
                                        ExecuteTrans(SQL)

                                        SQL = "select Kode_Perusahaan from barang "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & So_Awal & "' "
                                        SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                        Using Dr2 = OpenTrans(SQL)
                                            If Dr2.Read Then

                                                Dr2.Close()
                                                SQL = "update barang set Good_Stock = Good_Stock + " & HilangkanTanda(Jumlah_Kecil) & ", Jumlah_Bags = Jumlah_Bags + " & HilangkanTanda(0) & " "
                                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "and Kode_Stock_Owner = '" & So_Awal & "' "
                                                SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                                ExecuteTrans(SQL)

                                            Else
                                                Dr2.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End Using

                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
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
                                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & So_Awal & "' "
                                SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                Using D2 = BindingTrans(SQL)

                                    If D2.Tables("MyTable").Rows.Count <> 0 Then
                                        If D2.Tables("MyTable").Rows(0).Item("good_stock") <> D2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                            Else

                                Dim Kd_Barang_2 As String = ""

                                '=========================
                                '=     GET KD BARANG     =
                                '=========================
                                SQL = "select Kode_Barang from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner = '" & So_Awal & "' and Serial_Number = '" & Sn_Awal & "' "
                                Using Dr2 = OpenTrans(SQL)
                                    If Dr2.Read Then
                                        Kd_Barang_2 = Dr2("Kode_Barang")
                                    Else
                                        Dr2.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                Dim satuan_brg As String = ""
                                SQL = "select satuan from barang where Kode_Stock_Owner = '" & So_Awal & "' and Kode_Barang = '" & Kd_Barang_2 & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        satuan_brg = Dr("satuan")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Pembatalan Tidak Bisa Dilakukan, Karena Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                Dim Jumlah_pcs As Double = 0
                                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Kd_Barang_2 & "', "
                                SQL = SQL & "'" & Satuan & "', '" & satuan_brg & "', '" & HilangkanTanda(Jumlah) & "' ) as hasil"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Jumlah_pcs = Dr("hasil")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Pembatalan Tidak Bisa Dilakukan, Karena Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '=================================
                                '=     CEK APAKAH STOCK UTUH     =
                                '=================================

                                SQL = "select Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner = '" & So_Tujuan & "' "
                                SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                SQL = SQL & "and Serial_Number = '" & Sn_Tujuan_Scrap & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        If HilangkanTanda(Dr("Jumlah")) <> HilangkanTanda(Jumlah) Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Pembatalan Tidak Bisa Dilakukan, Karena Data Barang Sudah Digunakan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        Dr.Close()
                                        '=======================
                                        '=     UPDATE DATA     =
                                        '=======================
                                        SQL = "update Barang_SN set Jumlah = Jumlah - " & HilangkanTanda(Jumlah) & " "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & So_Tujuan & "' "
                                        SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                        SQL = SQL & "and Serial_Number = '" & Sn_Tujuan_Scrap & "' "
                                        ExecuteTrans(SQL)

                                        SQL = "select Kode_Perusahaan from barang "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & So_Tujuan & "' "
                                        SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                        Using Dr2 = OpenTrans(SQL)
                                            If Dr2.Read Then

                                                Dr2.Close()
                                                SQL = "update barang set Good_Stock = Good_Stock - " & HilangkanTanda(Jumlah) & " "
                                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "and Kode_Stock_Owner = '" & So_Tujuan & "' "
                                                SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                                ExecuteTrans(SQL)

                                            Else
                                                Dr2.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End Using



                                        SQL = "update Barang_SN set Jumlah = Jumlah + '" & HilangkanTanda(Jumlah_pcs) & "' "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & So_Awal & "' "
                                        SQL = SQL & "and Kode_Barang = '" & Kd_Barang_2 & "' "
                                        SQL = SQL & "and Serial_Number = '" & Sn_Awal & "' "
                                        ExecuteTrans(SQL)

                                        SQL = "select Kode_Perusahaan from barang "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & So_Awal & "' "
                                        SQL = SQL & "and Kode_Barang = '" & Kd_Barang_2 & "' "
                                        Using Dr2 = OpenTrans(SQL)
                                            If Dr2.Read Then

                                                Dr2.Close()
                                                SQL = "update barang set Good_Stock = Good_Stock + '" & HilangkanTanda(Jumlah_pcs) & "' "
                                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "and Kode_Stock_Owner = '" & So_Awal & "' "
                                                SQL = SQL & "and Kode_Barang = '" & Kd_Barang_2 & "' "
                                                ExecuteTrans(SQL)

                                            Else
                                                Dr2.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End Using

                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
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
                                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & So_Awal & "' "
                                SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang_2 & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                Using D2 = BindingTrans(SQL)

                                    If D2.Tables("MyTable").Rows.Count <> 0 Then
                                        If D2.Tables("MyTable").Rows(0).Item("good_stock") <> D2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using


                            End If


                            '===========================
                            '=     ROLLBACK JURNAL     =
                            '===========================
                            SQL = "select Kode_Perusahaan from Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & KodeVoucher_1.Trim & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "delete Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & KodeVoucher_1.Trim & "' "
                                    ExecuteTrans(SQL)
                                    'Else
                                    '    CloseTrans()
                                    '    CloseConn()
                                    '    MessageBox.Show("Data Jurnal Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '    Exit Sub
                                End If
                            End Using

                            SQL = "select Kode_Perusahaan from Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & KodeVoucher_2.Trim & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "delete Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & KodeVoucher_2.Trim & "' "
                                    ExecuteTrans(SQL)
                                    'Else
                                    '    CloseTrans()
                                    '    CloseConn()
                                    '    MessageBox.Show("Data Jurnal Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '    Exit Sub
                                End If
                            End Using


                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Detail Data Transaksi Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using



            '==============================
            '=     ROLLBACK PACKAGING     =
            '==============================
            SQL = "select a.No_Transaksi, c.Kode_Stock_Owner, c.Kode_Barang, c.Serial_Number, c.Jumlah "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Packaging_Detail b, Emi_Production_Results_Validation_Packaging_Det c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Urut = c.No_Urut_Detail "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Kd_So As String = .Rows(i).Item("Kode_Stock_Owner")
                            Dim Kd_Barang As String = .Rows(i).Item("Kode_Barang")
                            Dim Sn As String = .Rows(i).Item("Serial_Number")
                            Dim Jumlah As Double = .Rows(i).Item("Jumlah")

                            SQL = "select Kode_Perusahaan from barang_sn "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner = '" & Kd_So & "' "
                            SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                            SQL = SQL & "and Serial_Number = '" & Sn & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then


                                    Dr.Close()
                                    SQL = "update barang_sn set jumlah = jumlah + " & HilangkanTanda(Jumlah) & " "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and Kode_Stock_Owner = '" & Kd_So & "' "
                                    SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                    SQL = SQL & "and Serial_Number = '" & Sn & "' "
                                    ExecuteTrans(SQL)

                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Packaging Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            SQL = "select good_stock from barang "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner = '" & Kd_So & "' "
                            SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Dr.Close()
                                    SQL = "update barang set Good_Stock = Good_Stock + " & HilangkanTanda(Jumlah) & " "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and Kode_Stock_Owner = '" & Kd_So & "' "
                                    SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' "
                                    ExecuteTrans(SQL)

                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Packaging Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
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
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Kd_So & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using D2 = BindingTrans(SQL)

                                If D2.Tables("MyTable").Rows.Count <> 0 Then
                                    If D2.Tables("MyTable").Rows(0).Item("good_stock") <> D2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                        Next
                    End If
                End With
            End Using



            '=======================
            '=     UPDATE FLAG     =
            '=======================
            SQL = "select Kode_Perusahaan from Emi_Production_Results_Validation "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Transaksi = '" & No_Transaksi & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = "update Emi_Production_Results_Validation set Status = 'Y' "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Transaksi = '" & No_Transaksi & "' "
                    ExecuteTrans(SQL)

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Tidak Bisa Dilakukan, Karena Data Transaksi Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            'If True Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Tahan")
            '    Exit Sub
            'End If







            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Validasi Penerimaan Barang Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Kosong()


    End Sub


End Class