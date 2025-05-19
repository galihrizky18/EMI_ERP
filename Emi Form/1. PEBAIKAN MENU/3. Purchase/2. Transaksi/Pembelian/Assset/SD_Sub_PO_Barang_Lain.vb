Public Class SD_Sub_PO_Barang_Lain


    Dim Lv_NoFak, Lv_Lokasi, Lv_Kategori, Lv_Tanggal, Lv_Supplier, Lv_Keterangan, Lv_POBerjalan As String
    Dim item_NoFak As Integer = 0
    Dim item_Lokasi As Integer = 1
    Dim item_Kategori As Integer = 2
    Dim item_Tanggal As Integer = 3
    Dim item_Supplier As Integer = 4
    Dim item_Keterangan As Integer = 5
    Dim item_POBerjalan As Integer = 6

    Private Sub SD_Sub_PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Kosong()
    End Sub


    Private Sub Kosong()

        Lv_Data_Induk.Items.Clear()
        Lv_Detail.Items.Clear()

        Lv_Data_Induk.Columns.Clear()
        Lv_Data_Induk.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_Data_Induk.Columns.Add("Lokasi", 130, HorizontalAlignment.Left)
        Lv_Data_Induk.Columns.Add("Kategori PO", 130, HorizontalAlignment.Center)
        Lv_Data_Induk.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_Data_Induk.Columns.Add("Supplier", 150, HorizontalAlignment.Left)
        Lv_Data_Induk.Columns.Add("Keterangan", 230, HorizontalAlignment.Left)
        Lv_Data_Induk.Columns.Add("Jumlah Sub PO", 80, HorizontalAlignment.Center)
        Lv_Data_Induk.View = View.Details

        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Jumlah PO", 130, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Sisa PO", 130, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Detail.View = View.Details


        Try
            OpenConn()

            Lv_Data_Induk.Items.Clear()
            SQL = "select a.No_Faktur, a.Lokasi, a.Tanggal_Release, c.Kode_Supplier, c.Nama, 1 as ID, ETD, Flag_ETD, Flag_Release , a.No_Nota as Keterangan , a.Flag_Import, "
            SQL = SQL & "isnull((select top(1) 'T' from EMI_Pembelian_PO_Detail_Induk_Barang_Lain x where x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Faktur=a.no_Faktur and x.Flag_loading is null),'Y') as Selesai_ETA , "
            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x, EMI_Pembelian_Loading y where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.No_Faktur and y.status is null and x.Kode_Perusahaan=a.Kode_Perusahaan and x.no_PO=a.no_Faktur),null) as Flag_ETA , "
            SQL = SQL & "isnull((select top(1) ETA from EMI_Pembelian_Loading_detail x, EMI_Pembelian_Loading y where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.No_Faktur and y.status is null and x.Kode_Perusahaan=a.Kode_Perusahaan and x.no_PO=a.no_Faktur),null) as ETA, "
            SQL = SQL & "isnull(( select count(*) from EMI_Pembelian_PO_Barang_Lain z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.No_Faktur = z.No_Faktur_Induk ), 0) as PO_Berjalan "
            SQL = SQL & "from EMI_Pembelian_PO_Induk_Barang_Lain a, Suppliers c, Suppliers_Kategori d where Selesai is null and Status is null and "
            SQL = SQL & "a.Kode_Perusahaan=c.Kode_Perusahaan and a.Kode_Supplier=c.Kode_Supplier and a.Kode_Perusahaan='" & KodePerusahaan & "' and "
            'SQL = SQL & "c.ID_Kategori_Suppliers=d.ID_Kategori_Suppliers and (d.Flag_Jenis_Lokal='Y' or d.Flag_Jenis_import='Y' ) and a.flag_pembelian is null and Flag_Selesai_SubPO is null "
            SQL = SQL & "c.ID_Kategori_Suppliers=d.ID_Kategori_Suppliers and (d.Flag_Jenis_Lokal='Y' or d.Flag_Jenis_import='Y' ) and Flag_Selesai_SubPO is null and a.Flag_Release='Y' "
            SQL = SQL & "order by no_faktur  "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data_Induk.Items.Add(Dr.Item("No_Faktur").ToString)
                    Lv.SubItems.Add(Dr("Lokasi"))
                    If General_Class.CekNULL(Dr("Flag_Import")) = "" Then
                        Lv.SubItems.Add("LOKAL")
                    Else
                        Lv.SubItems.Add("IMPORT")
                    End If
                    Lv.SubItems.Add(Format(Dr("Tanggal_Release"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("PO_Berjalan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)

        Lv_NoFak = Lv_Data_Induk.Items(index).SubItems(item_NoFak).Text
        Lv_Lokasi = Lv_Data_Induk.Items(index).SubItems(item_Lokasi).Text
        Lv_Kategori = Lv_Data_Induk.Items(index).SubItems(item_Kategori).Text
        Lv_Tanggal = Lv_Data_Induk.Items(index).SubItems(item_Tanggal).Text
        Lv_Supplier = Lv_Data_Induk.Items(index).SubItems(item_Supplier).Text
        Lv_Keterangan = Lv_Data_Induk.Items(index).SubItems(item_Keterangan).Text
        Lv_POBerjalan = Lv_Data_Induk.Items(index).SubItems(item_POBerjalan).Text

    End Sub

    Private Sub Lv_Data_Induk_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data_Induk.DoubleClick
        If Lv_Data_Induk.Items.Count = 0 Then Exit Sub

        Get_Data_Lv(Lv_Data_Induk.FocusedItem.Index)


        EMI_PO_Pembelian_Sub_Barang_Lain.Txt_Faktur_Induk.Text = Lv_NoFak
        EMI_PO_Pembelian_Sub_Barang_Lain.kosong()
        EMI_PO_Pembelian_Sub_Barang_Lain.LvSupplier2.Visible = False

        Me.Close()

    End Sub

    Private Sub Lv_Data_Induk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data_Induk.SelectedIndexChanged

        If Lv_Data_Induk.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            Get_Data_Lv(Lv_Data_Induk.FocusedItem.Index)

            Lv_Detail.Items.Clear()
            SQL = "select a.No_Faktur, b.Kode_Barang, c.Nama, b.Jumlah, b.Satuan, "

            SQL = SQL & "ISNULL(( "
            SQL = SQL & "SELECT SUM(x.Jumlah) "
            SQL = SQL & "FROM EMI_Pembelian_PO_Barang_Lain z, EMI_Pembelian_PO_Detail_Barang_Lain x "
            SQL = SQL & "WHERE b.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "AND b.No_Faktur = z.No_Faktur_Induk "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "AND b.Kode_Stock_Owner = x.Kode_Stock_Owner AND b.Kode_Barang = x.Kode_Barang and z.status is null "
            SQL = SQL & "GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
            SQL = SQL & "), 0) AS jumlah_masuk, "

            SQL = SQL & "(b.jumlah - ISNULL(( "
            SQL = SQL & "SELECT  SUM(x.Jumlah) "
            SQL = SQL & "FROM EMI_Pembelian_PO_Barang_Lain z, EMI_Pembelian_PO_Detail_Barang_Lain x "
            SQL = SQL & "WHERE b.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "AND b.No_Faktur = z.No_Faktur_Induk  "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "AND b.Kode_Stock_Owner = x.Kode_Stock_Owner AND b.Kode_Barang = x.Kode_Barang and z.status is null "
            SQL = SQL & "GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
            SQL = SQL & "), 0)) AS sisa "

            SQL = SQL & "from EMI_Pembelian_PO_Induk_Barang_Lain a, EMI_Pembelian_PO_Detail_Induk_Barang_Lain b, Barang_Lain c "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.no_faktur = b.no_faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.kode_barang = c.kode_barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_NoFak & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Format(Dr("jumlah_masuk"), "N2"))
                    Lv.SubItems.Add(Format(Dr("sisa"), "N2"))
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

End Class

