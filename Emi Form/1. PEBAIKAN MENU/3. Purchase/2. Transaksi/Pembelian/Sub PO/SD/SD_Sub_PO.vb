Public Class SD_Sub_PO

    Public Kode_Supplier As String

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

    Private Sub Lv_Data_Induk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lv_Data_Induk.KeyPress
        If e.KeyChar = Chr(13) Then
            Lv_Data_Induk_DoubleClick(Lv_Data_Induk, e)
        End If
    End Sub

    Private Sub Lv_Detail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Detail.SelectedIndexChanged

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
        Lv_Detail.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '0
        Lv_Detail.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left) '1

        Lv_Detail.Columns.Add("Jumlah Dasar", 0, HorizontalAlignment.Right) '2
        Lv_Detail.Columns.Add("Jumlah PO Dasar", 0, HorizontalAlignment.Right) '3
        Lv_Detail.Columns.Add("Sisa PO Dasar", 0, HorizontalAlignment.Right) '4
        Lv_Detail.Columns.Add("Satuan Dasar", 0, HorizontalAlignment.Center) '5

        Lv_Detail.Columns.Add("Jumlah PO", 130, HorizontalAlignment.Right) '6
        Lv_Detail.Columns.Add("Jumlah Masuk", 130, HorizontalAlignment.Right) '7
        Lv_Detail.Columns.Add("Sisa PO", 130, HorizontalAlignment.Right) '8
        Lv_Detail.Columns.Add("Satuan", 100, HorizontalAlignment.Center) '9
        Lv_Detail.View = View.Details


        Try
            OpenConn()

            Lv_Data_Induk.Items.Clear()
            SQL = "select a.No_Faktur, a.Lokasi, a.Tanggal_Release, c.Kode_Supplier, c.Nama, 1 as ID, ETD, Flag_ETD, Flag_Release , a.No_Nota as Keterangan , a.Flag_Import, "
            SQL = SQL & "isnull((select top(1) 'T' from EMI_Pembelian_PO_Detail_Induk x where x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Faktur=a.no_Faktur and x.Flag_loading is null),'Y') as Selesai_ETA , "
            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x, EMI_Pembelian_Loading y where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.No_Faktur and y.status is null and x.Kode_Perusahaan=a.Kode_Perusahaan and x.no_PO=a.no_Faktur),null) as Flag_ETA , "
            SQL = SQL & "isnull((select top(1) ETA from EMI_Pembelian_Loading_detail x, EMI_Pembelian_Loading y where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_faktur=y.No_Faktur and y.status is null and x.Kode_Perusahaan=a.Kode_Perusahaan and x.no_PO=a.no_Faktur),null) as ETA, "
            SQL = SQL & "isnull(( select count(*) from EMI_Pembelian_PO z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.No_Faktur = z.No_Faktur_Induk ), 0) as PO_Berjalan "
            SQL = SQL & "from EMI_Pembelian_PO_Induk a, Suppliers c, Suppliers_Kategori d where Selesai is null and Status is null and "
            SQL = SQL & "a.Kode_Perusahaan=c.Kode_Perusahaan and a.Kode_Supplier=c.Kode_Supplier and a.Kode_Perusahaan='" & KodePerusahaan & "' and "
            'SQL = SQL & "c.ID_Kategori_Suppliers=d.ID_Kategori_Suppliers and (d.Flag_Jenis_Lokal='Y' or d.Flag_Jenis_import='Y' ) and a.flag_pembelian is null and Flag_Selesai_SubPO is null "
            SQL = SQL & "c.ID_Kategori_Suppliers=d.ID_Kategori_Suppliers and (d.Flag_Jenis_Lokal='Y' or d.Flag_Jenis_import='Y' ) and Flag_Selesai_SubPO is null and a.Flag_Release='Y' "
            If Not String.IsNullOrEmpty(Kode_Supplier) Then
                SQL = SQL & "and a.Kode_Supplier = '" & Kode_Supplier & "' "
            End If
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

        Ambil_Data_Induk(False)

    End Sub

    Private Sub Lv_Detail_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Detail.DoubleClick
        If Lv_Data_Induk.Items.Count = 0 Then Exit Sub

        Ambil_Data_Induk(True, Lv_Detail.FocusedItem.SubItems(0).Text)
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
            SQL = SQL & "FROM EMI_Pembelian_PO z, EMI_Pembelian_PO_Det x "
            SQL = SQL & "WHERE b.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "AND b.No_Faktur = x.No_FakInduk "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "AND b.Kode_Stock_Owner = x.Kode_Stock_Owner AND b.Kode_Barang = x.Kode_Barang and z.status is null "
            SQL = SQL & "and x.urut_det_induk = b.No_Urut "
            SQL = SQL & "GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
            SQL = SQL & "), 0) AS jumlah_masuk, "

            SQL = SQL & "(b.jumlah - ISNULL(( "
            SQL = SQL & "SELECT  SUM(x.Jumlah) "
            SQL = SQL & "FROM EMI_Pembelian_PO z, EMI_Pembelian_PO_Det x "
            SQL = SQL & "WHERE b.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "AND b.No_Faktur = x.No_FakInduk "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "AND b.Kode_Stock_Owner = x.Kode_Stock_Owner AND b.Kode_Barang = x.Kode_Barang and z.status is null "
            SQL = SQL & "and x.urut_det_induk = b.No_Urut "
            SQL = SQL & "GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
            SQL = SQL & "), 0)) AS sisa, "

            SQL = SQL & "b.Jumlah_Input, b.Satuan_Input, "

            SQL = SQL & "ISNULL(( "
            SQL = SQL & "SELECT SUM(x.Jumlah_Input) "
            SQL = SQL & "FROM EMI_Pembelian_PO z, EMI_Pembelian_PO_Det x "
            SQL = SQL & "WHERE b.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "AND b.No_Faktur = x.No_FakInduk "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "AND b.Kode_Stock_Owner = x.Kode_Stock_Owner AND b.Kode_Barang = x.Kode_Barang and z.status is null "
            SQL = SQL & "and x.urut_det_induk = b.No_Urut "
            SQL = SQL & "GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
            SQL = SQL & "), 0) AS jumlah_masuk_input, "

            SQL = SQL & "(b.Jumlah_Input - ISNULL(( "
            SQL = SQL & "SELECT  SUM(x.Jumlah_Input) "
            SQL = SQL & "FROM EMI_Pembelian_PO z, EMI_Pembelian_PO_Det x "
            SQL = SQL & "WHERE b.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "AND b.No_Faktur = x.No_FakInduk "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "AND b.Kode_Stock_Owner = x.Kode_Stock_Owner AND b.Kode_Barang = x.Kode_Barang and z.status is null "
            SQL = SQL & "and x.urut_det_induk = b.No_Urut "
            SQL = SQL & "GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
            SQL = SQL & "), 0)) AS sisa_input "

            SQL = SQL & "from EMI_Pembelian_PO_Induk a, EMI_Pembelian_PO_Det_Induk b, Barang c "
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


                    Lv.SubItems.Add(Format(If(General_Class.CekNULL(Dr("Jumlah_Input")) = "", 0, Dr("Jumlah_Input")), "N2"))
                    Lv.SubItems.Add(Format(If(General_Class.CekNULL(Dr("jumlah_masuk_input")) = "", 0, Dr("jumlah_masuk_input")), "N2"))
                    Lv.SubItems.Add(Format(If(General_Class.CekNULL(Dr("sisa_input")) = "", 0, Dr("sisa_input")), "N4"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Satuan_Input")) = "", "-", Dr("Satuan_Input")))

                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Public Sub Ambil_Data_Induk(ByVal isBahan As Boolean, Optional ByVal KdBarang As String = "")


        Get_Data_Lv(Lv_Data_Induk.FocusedItem.Index)

        Try
            OpenConn()



            SQL = "select a.No_Faktur, a.Tanggal_Release, a.ETD_Simulasi, a.No_Nota as Keterangan, a.Kode_Supplier, c.Nama as Nama_Supplier, a.Mata_Uang, a.Jenis_Pembayaran, a.Tempo_Pembayaran, a.Lama_Pembayaran, "
            SQL = SQL & "sum(b.Jumlah) as Berat, a.Ekspedisi, a.Biaya, a.Total_MUA, a.Grand_Sebelum_PPN, a.PPN, (a.Grand_Sebelum_PPN * (a.ppn / 100)) as Total_PPN, a.Grand, a.Grand_PPH "
            SQL = SQL & "from EMI_Pembelian_PO_Induk a, EMI_Pembelian_PO_Detail_Induk b, Suppliers c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Kode_Supplier = c.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_Faktur = '" & Lv_NoFak & "' "
            SQL = SQL & "group by a.No_Faktur, a.Tanggal_Release, a.ETD_Simulasi, a.No_Nota, a.Kode_Supplier, c.Nama, a.Mata_Uang, a.Jenis_Pembayaran, a.Tempo_Pembayaran, a.Lama_Pembayaran, "
            SQL = SQL & "a.Ekspedisi, a.Biaya, a.Total_MUA, a.Grand_Sebelum_PPN, a.PPN, a.Grand, a.Grand_PPH "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    EMI_PO_Pembelian_Sub.Txt_Faktur_Induk.Text = Dr("No_Faktur")
                    'EMI_PO_Pembelian_Sub.DtpPO_Tgl.Value = Dr("Tanggal_Release")
                    'EMI_PO_Pembelian_Sub.DtpPO_ETD.Value = Dr("ETD_Simulasi")
                    EMI_PO_Pembelian_Sub.TxtPO_NoNota.Text = Dr("Keterangan")
                    EMI_PO_Pembelian_Sub.TxtPO_KdSupplier.Text = Dr("Kode_Supplier")
                    EMI_PO_Pembelian_Sub.TxtPO_NmSupplier.Text = Dr("Nama_Supplier")
                    EMI_PO_Pembelian_Sub.CmbPO_MataUang.Text = Dr("Mata_Uang")
                    EMI_PO_Pembelian_Sub.CmbPO_JnsBayar.SelectedIndex = EMI_PO_Pembelian_Sub.arrPembayaran.IndexOf(Dr("Jenis_Pembayaran"))
                    EMI_PO_Pembelian_Sub.cmbJenisPengiriman.SelectedItem = Dr("Tempo_Pembayaran")
                    EMI_PO_Pembelian_Sub.txtJatuhTempo.Text = Dr("Lama_Pembayaran")
                    EMI_PO_Pembelian_Sub.PPN = Dr("PPN")
                    EMI_PO_Pembelian_Sub.TxtPO_PersenPPN.Text = Dr("PPN")

                    EMI_PO_Pembelian_Sub.LvSupplier2.Visible = False

                    EMI_PO_Pembelian_Sub.CmbPO_MataUang.Enabled = False
                    EMI_PO_Pembelian_Sub.TxtPO_NoNota.Focus()
                Else
                    Dr.Close()
                    CloseConn()
                    'MessageBox.Show("Data Tidak Ditermukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '===========================
            '=     GET DATA BARANG     =
            '===========================
            Dim index As Integer = EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows.Count
            'LvPO_DataPO.Rows.Clear()
            SQL = "select a.No_Faktur, d.Kode_Stock_Owner, d.Kode_Barang, c.Nama, d.Harga as Harga_Satuan_Besar, d.Satuan, "
            SQL = SQL & "d.Harga_Barang, d.Satuan_Barang, d.No_Penawaran, d.No_Urut_PR, a.Tempo_Pembayaran, a.Lama_Pembayaran, d.Jumlah, "
            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select (d.Jumlah - sum(x.Jumlah)) "
            SQL = SQL & "from EMI_Pembelian_PO z, EMI_Pembelian_PO_Det x "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and x.No_FakInduk = a.No_Faktur and z.No_Faktur = x.No_Faktur and x.Kode_Stock_Owner = d.Kode_Stock_Owner and x.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and x.no_urut_pr=d.no_urut_pr and x.urut_det_induk = d.No_Urut and z.status is null), d.Jumlah) as Sisa, d.No_Urut as Urut_det, e.Jenis_Kategori, "

            SQL = SQL & "isnull(d.Jumlah_Input, 0) as Jumlah_Input, isnull(d.Satuan_Input, '-') as Satuan_Input, "

            SQL = SQL & "isnull(( select z.No_Penawaran from EMI_Pembelian_PO_Detail_Induk z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = z.No_Faktur "
            SQL = SQL & "and d.Kode_Barang = z.Kode_Barang and z.no_penawaran=d.no_penawaran "
            SQL = SQL & "), '-') as No_FakPenawaran, "

            SQL = SQL & "ISNULL(( select (d.Jumlah_Input - sum(x.Jumlah_input)) from EMI_Pembelian_PO z, EMI_Pembelian_PO_Det x "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan  "
            SQL = SQL & "and x.No_FakInduk = a.No_Faktur and z.No_Faktur = x.No_Faktur and x.Kode_Stock_Owner = d.Kode_Stock_Owner and x.Kode_Barang = d.Kode_Barang  "
            SQL = SQL & "and x.no_urut_pr=d.no_urut_pr and x.urut_det_induk = d.No_Urut and z.status is null) "
            SQL = SQL & ", d.Jumlah_Input) as Sisa_Input "

            SQL = SQL & "from EMI_Pembelian_PO_Induk a, barang c, EMI_Pembelian_PO_Det_Induk d, kategori_besar e "
            SQL = SQL & "where a.Kode_Perusahaan = c.Kode_Perusahaan  "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and c.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Kategori_Besar = e.Kode_Kategori_Besar "
            SQL = SQL & "and d.Kode_Barang = c.Kode_Barang and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null and a.No_Faktur = '" & Lv_NoFak & "' "


            If isBahan Then
                SQL = SQL & "and d.Kode_Barang = '" & KdBarang & "' "
            End If

            SQL = SQL & "order by d.Kode_Barang "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For j As Integer = 0 To .Rows.Count - 1

                            '===================================
                            '=     GET DATA KATEGORI DI PO     =
                            '===================================
                            If EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows.Count <> 0 Then
                                If Not EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(0).Cells(EMI_PO_Pembelian_Sub.cellJnsKategori).Value = .Rows(j).Item("Jenis_Kategori") Then
                                    CloseConn()
                                    MessageBox.Show("Kode Kategori Berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If

                            Dim NilaiSatuanInput As Double = 0
                            SQL = "select nilai from N_EMI_Master_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Barang = '" & .Rows(j).Item("Kode_Barang") & "' and Satuan = '" & .Rows(j).Item("Satuan_Input") & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    NilaiSatuanInput = Dr("nilai")
                                Else
                                    CloseConn()
                                    MessageBox.Show("Satuan Tidak Ditemukan Pada Tabel Master Satuan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            Dim HargaDisplay As Double = Val(HilangkanTanda(.Rows(j).Item("Harga_Satuan_Besar"))) * NilaiSatuanInput

                            'TODD :Perhatikan ini
                            Dim skipNext As Boolean = False
                            For i As Integer = 0 To EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows.Count - 1

                                If EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(i).Cells(EMI_PO_Pembelian_Sub.cellFakInduk).Value = .Rows(j).Item("No_Faktur") _
                                    And EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(i).Cells(EMI_PO_Pembelian_Sub.cellPO_KdBarang).Value = .Rows(j).Item("Kode_Barang") _
                                    And EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(i).Cells(EMI_PO_Pembelian_Sub.cellUrutDet).Value = .Rows(j).Item("Urut_det") _
                                    And Val(HilangkanTanda(EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(i).Cells(EMI_PO_Pembelian_Sub.cellPO_Harga).Value)) = Val(HilangkanTanda(.Rows(j).Item("Harga_Satuan_Besar"))) Then

                                    skipNext = True
                                    Exit For
                                End If
                            Next
                            If skipNext Then
                                Continue For
                            End If

                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows.Add(1)

                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Lokasi).Value = .Rows(j).Item("Kode_Stock_Owner") '0
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_KdBarang).Value = .Rows(j).Item("Kode_Barang") '1
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_NmBarang).Value = .Rows(j).Item("Nama") '2
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Harga).Value = Format(.Rows(j).Item("Harga_Satuan_Besar"), "N4") '3

                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_JumlahPO).Value = Format(.Rows(j).Item("Jumlah"), "N2") '4
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Sisa).Value = Format(.Rows(j).Item("Sisa"), "N2") '5

                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Jumlah).Value = Format(0, "N2") '6 'user isi

                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Satuan).Value = .Rows(j).Item("Satuan") '7
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Harga_SB).Value = .Rows(j).Item("Harga_Barang") '8

                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Jumlah_SB).Value = 0 '9 'generate berdasaerkan jumlah

                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Satuan_SB).Value = .Rows(j).Item("Satuan_Barang") '10
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_NoPenawaran).Value = .Rows(j).Item("No_Penawaran") '11
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_ID).Value = "T" '12

                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Total).Value = Format(0, "N4") '13 'TOtal auto generate jumlah * harga
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Urut).Value = "" '14
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_PR).Value = .Rows(j).Item("No_Urut_PR") '15
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellTempoPembayaran).Value = .Rows(j).Item("Tempo_Pembayaran") '16
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellJatuhTempo).Value = .Rows(j).Item("Lama_Pembayaran") '17
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellFakPenawaran).Value = .Rows(j).Item("No_Penawaran") '18
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellFakInduk).Value = .Rows(j).Item("No_Faktur") '19
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellUrutDet).Value = .Rows(j).Item("Urut_det") '20

                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellJnsKategori).Value = .Rows(j).Item("Jenis_Kategori") '21


                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellJumlahPOInput).Value = .Rows(j).Item("Jumlah_Input") '22
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellSatuanInput).Value = .Rows(j).Item("Satuan_Input") '23


                            Dim Kdbarang2 As String = .Rows(j).Item("Kode_Barang")
                            Dim NmBarang2 As String = .Rows(j).Item("Nama")
                            Dim SatuanInput As String = .Rows(j).Item("Satuan_Input")
                            Dim FakPenawaran As String = .Rows(j).Item("No_FakPenawaran")

                            '============================
                            '=     CEK SATUAN DASAR     =
                            '============================
                            Dim isSatuanDasar As Boolean = False
                            SQL = "select Satuan from N_EMI_Master_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Barang = '" & Kdbarang2 & "' and Flag_Dasar = 'Y' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Dr("Satuan") = SatuanInput Then
                                        isSatuanDasar = True
                                    Else
                                        isSatuanDasar = False
                                    End If
                                Else
                                    Dr.Close()
                                    CloseConn()
                                    MessageBox.Show($"Satuan Dasar pada Barang {NmBarang2} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '==================================
                            '=     HARGA PER SATUAN DASAR     =
                            '==================================
                            Dim HargaPerSatuanDasar As Double = 0
                            SQL = "select "
                            SQL = SQL & "b.Harga_Satuan as hasil "
                            'If isSatuanDasar Then
                            'Else
                            '    SQL = SQL & "(b.Min_Order * b.Harga_Satuan) as hasil "
                            'End If
                            SQL = SQL & "from EMI_Master_Penawaran a, EMI_Master_Penawaran_Detail b "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                            SQL = SQL & "and a.Status is null "
                            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.No_Faktur= '" & FakPenawaran & "' "
                            SQL = SQL & "and b.Kode_Barang = '" & Kdbarang2 & "' "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows().Count <> 0 Then
                                    For k As Integer = 0 To Ds2.Tables("MyTable").Rows().Count - 1
                                        HargaPerSatuanDasar = Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(k).Item("hasil")))
                                    Next
                                Else
                                    CloseConn()
                                    MessageBox.Show("Harga Penawaran Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                            End Using


                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellHargaInput).Value = Format(Val(HargaPerSatuanDasar), "N4") '24
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellSisaPOInput).Value = .Rows(j).Item("Sisa_Input") '25
                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellHargaDisplay).Value = Format(Val(HilangkanTanda(HargaDisplay)), "N4") '26




                            EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Jumlah).Style.BackColor = Color.LightGray

                            index += 1
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

        EMI_PO_Pembelian_Sub.disableSebagian()
        EMI_PO_Pembelian_Sub.Cmb_Ekspedisi.Enabled = True
        Me.Close()

    End Sub

End Class

