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
            SQL = SQL & "), 0)) AS sisa "

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
            SQL = SQL & "and x.no_urut_pr=d.no_urut_pr and x.urut_det_induk = d.No_Urut), d.Jumlah) as Sisa, d.No_Urut as Urut_det  "

            SQL = SQL & "from EMI_Pembelian_PO_Induk a, barang c, EMI_Pembelian_PO_Det_Induk d  "
            SQL = SQL & "where a.Kode_Perusahaan = c.Kode_Perusahaan  "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and d.Kode_Barang = c.Kode_Barang and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null and a.No_Faktur = '" & Lv_NoFak & "' "


            If isBahan Then

                SQL = SQL & "and d.Kode_Barang = '" & KdBarang & "' "
            End If

            SQL = SQL & "order by d.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    'TODD :Perhatikan ini
                    For i As Integer = 0 To EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows.Count - 1
                        If EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(i).Cells(EMI_PO_Pembelian_Sub.cellFakInduk).Value = Dr("No_Faktur") _
                            And EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(i).Cells(EMI_PO_Pembelian_Sub.cellPO_KdBarang).Value = Dr("Kode_Barang") _
                            And EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(i).Cells(EMI_PO_Pembelian_Sub.cellUrutDet).Value = Dr("Urut_det") _
                            And EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(i).Cells(EMI_PO_Pembelian_Sub.cellPO_Harga).Value = Dr("Harga_Satuan_Besar") Then

                            Continue Do
                        End If
                    Next

                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows.Add(1)

                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Lokasi).Value = Dr("Kode_Stock_Owner")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_KdBarang).Value = Dr("Kode_Barang")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_NmBarang).Value = Dr("Nama")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Harga).Value = Format(Dr("Harga_Satuan_Besar"), "N2")

                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_JumlahPO).Value = Format(Dr("Jumlah"), "N2")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Sisa).Value = Format(Dr("Sisa"), "N2")

                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Jumlah).Value = Format(0, "N2") 'user isi

                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Satuan).Value = Dr("Satuan")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Harga_SB).Value = Dr("Harga_Barang")

                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Jumlah_SB).Value = 0 ' generate berdasaerkan jumlah

                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Satuan_SB).Value = Dr("Satuan_Barang")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_NoPenawaran).Value = Dr("No_Penawaran")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_ID).Value = "T"

                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Total).Value = Format(0, "N2") 'TOtal auto generate jumlah * harga
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Urut).Value = ""
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_PR).Value = Dr("No_Urut_PR")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellTempoPembayaran).Value = Dr("Tempo_Pembayaran")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellJatuhTempo).Value = Dr("Lama_Pembayaran")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellFakPenawaran).Value = Dr("No_Penawaran")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellFakInduk).Value = Dr("No_Faktur")
                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellUrutDet).Value = Dr("Urut_det")

                    EMI_PO_Pembelian_Sub.LvPO_DataPO.Rows(index).Cells(EMI_PO_Pembelian_Sub.cellPO_Jumlah).Style.BackColor = Color.LightGray

                    index += 1
                Loop
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

