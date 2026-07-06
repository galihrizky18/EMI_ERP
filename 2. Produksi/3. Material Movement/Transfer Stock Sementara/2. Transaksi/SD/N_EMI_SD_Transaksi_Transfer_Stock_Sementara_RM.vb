Public Class N_EMI_SD_Transaksi_Transfer_Stock_Sementara_RM
    Dim Jenis = "Emi_Display_Request_Material"
    Public lokasi_kirim, asal, SelectedSplit As String

    Dim lv_Keterangan, lv_NoSplit, lv_kodeSO, Lv_MetPotStok, lv_KdBrg, lv_NmBrg, lv_Jenis As String
    Dim lv_TglPermintaan, lv_JamPermintaan, lv_Jumlah, lv_Satuan, lv_UserInput, lv_Warna, lv_GoodStock As String
    Dim lv_SatuanBesar, lv_SatuanDisplay, lv_JmlBags, lv_SatuanBags, Lv_Oto, lv_JumlahTF, LvJenisKemasan As String

    Dim item_NoSplit As Integer = 0
    Dim item_KodeSo As Integer = 1
    Dim item_KdBarang As Integer = 2
    Dim item_NmBarang As Integer = 3
    Dim item_Keterangan As Integer = 4
    Dim item_TglPermintaan As Integer = 5
    Dim item_Jumlah As Integer = 6
    Dim item_JumlahTransfer As Integer = 7
    Dim item_SatuanDisplay As Integer = 8
    'HIDE
    Dim item_JamPermintaan As Integer = 9
    Dim item_Satuan As Integer = 10
    Dim item_UserInput As Integer = 11
    Dim item_Warna As Integer = 12
    Dim item_Stock As Integer = 13
    Dim item_SatuanBesar As Integer = 14



    Dim item_JmlBags As Integer = 15
    Dim item_SatuanBags As Integer = 16
    Dim item_Oto As Integer = 17
    Dim item_MetodePengeluaran As Integer = 18



    Dim item_JenisKemasan As Integer = 19
    Dim Flag_Opname As Boolean

    Dim DefaultLimit As Double = 20

    Dim arrFilter As New ArrayList

    ' Variabel Global
    Dim PageSize As Integer = 5
    Dim CurrentPage As Integer = 1
    Dim TotalRows As Integer
    Dim totalpage As Integer = 10



    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click

        Dim filter As Boolean = False
        If Cmb_Filter.SelectedIndex > 0 Then
            If Txt_Value_Filter.Text.Trim.Length <> 0 Then
                filter = True
            End If
        End If

        If CurrentPage < totalpage Then
            CurrentPage += 1
            Kosong(filter, CurrentPage)


        End If

        If totalpage = CurrentPage Then
            BtnNext.Enabled = False
        Else
            BtnNext.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev.Enabled = False
        Else
            BtnPrev.Enabled = True
        End If

    End Sub



    Private Sub BtnPrev_Click(sender As Object, e As EventArgs) Handles BtnPrev.Click

        Dim filter As Boolean = False
        If Cmb_Filter.SelectedIndex > 0 Then
            If Txt_Value_Filter.Text.Trim.Length <> 0 Then
                filter = True
            End If
        End If

        If CurrentPage > 1 Then
            CurrentPage -= 1
            Kosong(filter, CurrentPage)
        End If

        If totalpage = CurrentPage Then
            BtnNext.Enabled = False
        Else
            BtnNext.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev.Enabled = False
        Else
            BtnPrev.Enabled = True
        End If

    End Sub

    Private Sub BtnFirst_Click(sender As Object, e As EventArgs) Handles BtnFirst.Click


        Dim filter As Boolean = False
        If Cmb_Filter.SelectedIndex > 0 Then
            If Txt_Value_Filter.Text.Trim.Length <> 0 Then
                filter = True
            End If
        End If

        CurrentPage = 1
        Kosong(filter, CurrentPage)

        If totalpage = CurrentPage Then
            BtnNext.Enabled = False
        Else
            BtnNext.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev.Enabled = False
        Else
            BtnPrev.Enabled = True
        End If

    End Sub


    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)

        lv_NoSplit = Lv_Data.Items(NoIndex).SubItems(item_NoSplit).Text
        lv_kodeSO = Lv_Data.Items(NoIndex).SubItems(item_KodeSo).Text
        lv_KdBrg = Lv_Data.Items(NoIndex).SubItems(item_KdBarang).Text
        lv_NmBrg = Lv_Data.Items(NoIndex).SubItems(item_NmBarang).Text
        lv_TglPermintaan = Lv_Data.Items(NoIndex).SubItems(item_TglPermintaan).Text
        lv_Jumlah = Lv_Data.Items(NoIndex).SubItems(item_Jumlah).Text
        lv_JumlahTF = Lv_Data.Items(NoIndex).SubItems(item_JumlahTransfer).Text
        lv_SatuanDisplay = Lv_Data.Items(NoIndex).SubItems(item_SatuanDisplay).Text
        lv_Keterangan = Lv_Data.Items(NoIndex).SubItems(item_Keterangan).Text
        lv_JamPermintaan = Lv_Data.Items(NoIndex).SubItems(item_JamPermintaan).Text
        lv_Satuan = Lv_Data.Items(NoIndex).SubItems(item_Satuan).Text
        lv_UserInput = Lv_Data.Items(NoIndex).SubItems(item_UserInput).Text
        lv_Warna = Lv_Data.Items(NoIndex).SubItems(item_Warna).Text
        lv_GoodStock = Lv_Data.Items(NoIndex).SubItems(item_Stock).Text
        lv_SatuanBesar = Lv_Data.Items(NoIndex).SubItems(item_SatuanBesar).Text
        lv_JmlBags = Lv_Data.Items(NoIndex).SubItems(item_JmlBags).Text
        lv_SatuanBags = Lv_Data.Items(NoIndex).SubItems(item_SatuanBags).Text
        Lv_Oto = Lv_Data.Items(NoIndex).SubItems(item_Oto).Text
        Lv_MetPotStok = Lv_Data.Items(NoIndex).SubItems(item_MetodePengeluaran).Text
        LvJenisKemasan = Lv_Data.Items(NoIndex).SubItems(item_JenisKemasan).Text

    End Sub

    Private Sub Emi_Display_Request_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Split", 150, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("Lokasi", 0, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Nama Barang", 150, HorizontalAlignment.Left) '3
        Lv_Data.Columns.Add("Keterangan", 300, HorizontalAlignment.Left) '4
        Lv_Data.Columns.Add("Tanggal Permintaan", 130, HorizontalAlignment.Center) '5
        Lv_Data.Columns.Add("Jumlah", 110, HorizontalAlignment.Right) '6
        Lv_Data.Columns.Add("Jumlah Transfer", 110, HorizontalAlignment.Right) '7
        Lv_Data.Columns.Add("Satuan Display", 100, HorizontalAlignment.Center) '8
        'HIDE
        Lv_Data.Columns.Add("Jam Permintaan", 0, HorizontalAlignment.Center) '9
        Lv_Data.Columns.Add("Satuan", 0, HorizontalAlignment.Center) '10
        Lv_Data.Columns.Add("User Input", 0, HorizontalAlignment.Center) '11
        Lv_Data.Columns.Add("Warna", 0, HorizontalAlignment.Center) '12
        Lv_Data.Columns.Add("Stock", 0, HorizontalAlignment.Right) '13
        Lv_Data.Columns.Add("Satuan Besar", 0, HorizontalAlignment.Center) '14
        Lv_Data.Columns.Add("Jumlah Bags", 0, HorizontalAlignment.Right) '15
        Lv_Data.Columns.Add("Satuan Bags", 0, HorizontalAlignment.Center) '16
        Lv_Data.Columns.Add("Urut Oto", 0, HorizontalAlignment.Center) '17
        Lv_Data.Columns.Add("Metode Stock", 0, HorizontalAlignment.Center) '18
        Lv_Data.Columns.Add("Jenis Kemasan", 0, HorizontalAlignment.Center) '19
        Lv_Data.View = View.Details

        Txt_Limit.Text = DefaultLimit

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add(OpsiSeluruh) : arrFilter.Add(OpsiSeluruh)
        Cmb_Filter.Items.Add("No Split") : arrFilter.Add("a.no_faktur_order")
        Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("c.Kode_Barang")
        Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("d.Nama")
        Cmb_Filter.SelectedIndex = 0

        CmbOrder.Items.Clear()
        CmbOrder.Items.Add("ASC")
        CmbOrder.Items.Add("DESC")
        CmbOrder.SelectedIndex = 0

        Kosong()
    End Sub

    Private Sub Kosong(Optional ByVal filter As Boolean = False, Optional ByVal page As Integer = 1)

        If filter = True Then
            If Cmb_Filter.SelectedIndex <> 1 Then
                PageSize = 22
            Else
                PageSize = 5
            End If
        End If


        Try
            OpenConn()

            SQL = "select Flag_Opname from init where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Opname")) = "Y" Then
                        Flag_Opname = True
                    Else
                        Flag_Opname = False
                    End If
                End If
            End Using
            Lv_Data.Items.Clear()

            ' Kode Lama Tanggal 23-10-2025
#Region "Kode Lama Tanggal 23-10-2025"

            'SQL = "select a.no_faktur_order, a.keterangan, d.Metode_Pengeluaran_Stok, d.jenis_kemasan, c.Kode_Stock_Owner, c.Kode_Barang, d.Nama, b.Kode_Group_Jenis, a.Tanggal, a.Jam, c.Jumlah,  a.UserId, c.warna, "
            'SQL = SQL & "dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, d.satuan, c.satuan, d.good_stock) as Good_Stock, d.Satuan, c.Satuan as Satuan_Display, "
            'SQL = SQL & "ISNULL(d.Jumlah_Bags, 0) as Jumlah_Bags, d.Satuan_Isi_Bags, c.Urut_Oto, "

            'SQL = SQL & "ISNULL(( isnull( (select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w  where "
            'SQL = SQL & "x.kode_Perusahaan=y.kode_perusahaan and x.no_faktur=y.no_faktur and x.status is null and "
            'SQL = SQL & "y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.urut_oto=z.urut_tf and (z.selesai is null or z.selesai='Y') and "
            'SQL = SQL & "z.kode_Perusahaan=w.kode_perusahaan and z.no_faktur=w.no_faktur and z.urut_oto=w.Urut_Det and "
            'SQL = SQL & "c.Kode_Perusahaan = y.Kode_Perusahaan and c.Urut_Oto = y.urut_material_requisition_convert  and y.Flag_Jenis_Request = 'PRODUKSI'),0) "
            'SQL = SQL & "+ "
            'SQL = SQL & "isnull( (select sum(w.jumlah) from Tf_Stock_QC x, Tf_Stock_QC_Detail y, Tf_Stock_QC_Det z, Tf_Stock_QC_Det2 w  where "
            'SQL = SQL & "x.kode_Perusahaan=y.kode_perusahaan and x.no_faktur=y.no_faktur and x.status is null and "
            'SQL = SQL & "y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.urut_oto=z.urut_tf and (z.selesai is null or z.selesai='Y') and "
            'SQL = SQL & "z.kode_Perusahaan=w.kode_perusahaan and z.no_faktur=w.no_faktur and z.urut_oto=w.Urut_Det and "
            'SQL = SQL & "c.Kode_Perusahaan = y.Kode_Perusahaan and c.Urut_Oto = y.urut_material_requisition_convert  and y.Flag_Jenis_Request = 'PRODUKSI'),0)), '0') as Total_TF, "

            'SQL = SQL & "(c.jumlah - (c.Jumlah * (d.Toleransi_Tf_Min / 100))) as Toleransi_Min, "
            'SQL = SQL & "(c.jumlah + (c.Jumlah * (d.Toleransi_Tf_Max / 100))) as Toleransi_Max "
            'SQL = SQL & "from Emi_Material_Requisition a, EMI_Group_Jenis b, Emi_Material_Requisition_Det_Convert c, barang d  "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            'SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            'SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Faktur = c.No_Faktur "
            'SQL = SQL & "and c.kode_barang = d.kode_barang and d.kode_stock_owner='" & lokasi_kirim & "' "
            'SQL = SQL & "and a.Flag_Process = 'Y' and a.status is null "
            'SQL = SQL & "and c.Flag_Transfer is null "
            'SQL = SQL & "and c.jumlah > 0 and c.jumlah_barang > 0"
            'SQL = SQL & "and d.Id_Kategori_Gudang = ( "
            'SQL = SQL & "select top 1 z.Id_Kategori_Gudang "
            'SQL = SQL & "from EMI_Kategori_Gudang_PerLokasi z "
            'SQL = SQL & "where a.Kode_Perusahaan = z.kode_perusahaan and z.Lokasi_Gudang = d.kode_stock_owner ) "
            'SQL = SQL & "and case when a.flag_tambah = 'Y' then a.Flag_Validasi_Tambah else 'Y' end = 'Y' "
            'SQL = SQL & "order by a.no_faktur_order, c.Kode_Barang, a.keterangan "

#End Region
            Dim offset As Integer = (page - 1) * PageSize

            SQL = ";with cte as ( "
            SQL = SQL & "select distinct "
            SQL = SQL & "No_Faktur_Order, tanggal, jam "
            SQL = SQL & "from Emi_Material_Requisition a, EMI_Group_Jenis b, Emi_Material_Requisition_Det_Convert c, barang d, Emi_Material_Requisition_Det e "
            SQL = SQL & "where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and "
            SQL = SQL & "c.Kode_Perusahaan = e.Kode_Perusahaan and "
            SQL = SQL & "d.Kode_Perusahaan = e.Kode_Perusahaan and "
            SQL = SQL & "a.Id_Group_Jenis = b.Id_Group_Jenis and "
            SQL = SQL & "a.No_Faktur = c.No_Faktur and "
            SQL = SQL & "c.no_faktur = e.no_faktur and "
            SQL = SQL & "c.kode_stock_owner = e.kode_stock_owner and "
            SQL = SQL & "c.kode_barang = e.kode_barang and "
            SQL = SQL & "c.no_urut_det = e.urut_oto and "
            SQL = SQL & "d.Kode_Stock_Owner = e.Kode_Stock_Owner_tujuan and "
            SQL = SQL & "d.kode_barang = e.kode_barang and "
            SQL = SQL & "a.Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "d.kode_stock_owner='" & lokasi_kirim & "' and "
            SQL = SQL & "a.Flag_Process = 'Y' and a.status is null and "
            SQL = SQL & "c.Flag_Transfer is null and "
            SQL = SQL & "c.jumlah > 0 and c.jumlah_barang > 0 and "

            If SelectedSplit.Trim.Length > 0 Then
                SQL = SQL & "a.no_faktur_order = '" & SelectedSplit & "' and "
            End If

            SQL = SQL & "d.Id_Kategori_Gudang = ( "
            SQL = SQL & "select top 1 z.Id_Kategori_Gudang "
            SQL = SQL & "from EMI_Kategori_Gudang_PerLokasi z "
            SQL = SQL & "where a.Kode_Perusahaan = z.kode_perusahaan and z.Lokasi_Gudang = d.kode_stock_owner ) "

            If Not Chk_Belum_Selesai.Checked Then
                If filter Then
                    If Cmb_Filter.SelectedIndex <> 0 Then
                        SQL = SQL & "and " & arrFilter(Cmb_Filter.SelectedIndex) & " like '%" & Txt_Value_Filter.Text.Trim & "%'  "
                    End If
                End If
            End If

            SQL = SQL & "order by Tanggal " & CmbOrder.Text & ", Jam " & CmbOrder.Text & " "
            SQL = SQL & "offset " & offset & " ROWS "
            SQL = SQL & "FETCH NEXT " & PageSize & " ROWS ONLY "
            SQL = SQL & ") "

            SQL = SQL & "select a.no_faktur_order, a.keterangan, d.Metode_Pengeluaran_Stok, d.jenis_kemasan, c.Kode_Stock_Owner, c.Kode_Barang, d.Nama, b.Kode_Group_Jenis, a.Tanggal, a.Jam, c.Jumlah,  a.UserId, c.warna, "
            SQL = SQL & "d.good_stock as Good_Stock, "
            SQL = SQL & "d.Satuan, c.Satuan as Satuan_Display, "
            SQL = SQL & "ISNULL(d.Jumlah_Bags, 0) as Jumlah_Bags, d.Satuan_Isi_Bags, c.Urut_Oto, "
            SQL = SQL & "ISNULL(( isnull( (select sum(z.jumlah) from N_EMI_Transaksi_Transfer_Stock_Sementara x, N_EMI_Transaksi_Transfer_Stock_Sementara_Detail y, "
            SQL = SQL & "N_EMI_Transaksi_Transfer_Stock_Sementara_Det z "
            'SQL = SQL * ", N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 w "
            SQL = SQL & "where "
            SQL = SQL & "x.kode_Perusahaan=y.kode_perusahaan and x.no_faktur=y.no_faktur and x.status is null and "
            SQL = SQL & "y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.urut_oto=z.urut_tf and (z.selesai is null or z.selesai='Y') and "

            'SQL = SQL & "z.kode_Perusahaan=w.kode_perusahaan and z.no_faktur=w.no_faktur and z.urut_oto=w.Urut_Det and "

            SQL = SQL & "c.Kode_Perusahaan = y.Kode_Perusahaan and c.Urut_Oto = y.urut_material_requisition_convert  and y.Flag_Jenis_Request = 'PRODUKSI'),0) "
            SQL = SQL & "+ "

            SQL = SQL & "isnull( (select sum(w.jumlah) from Tf_Stock_Parent x, Tf_Stock y, "
            SQL = SQL & "Tf_Stock_det z, Tf_Stock_det2 w  where "
            SQL = SQL & "x.kode_Perusahaan=y.kode_perusahaan and x.no_faktur=y.no_faktur and x.status is null and "
            SQL = SQL & "y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.urut_oto=z.urut_tf and (z.selesai is null or z.selesai='Y') and "
            SQL = SQL & "z.kode_Perusahaan=w.kode_perusahaan and z.no_faktur=w.no_faktur and z.urut_oto=w.Urut_Det and "
            SQL = SQL & "c.Kode_Perusahaan = y.Kode_Perusahaan and c.Urut_Oto = y.urut_material_requisition_convert  and y.Flag_Jenis_Request = 'PRODUKSI'),0) "

            SQL = SQL & "+ "

            SQL = SQL & "isnull( (select sum(w.jumlah) from Tf_Stock_QC x, Tf_Stock_QC_Detail y, Tf_Stock_QC_Det z, Tf_Stock_QC_Det2 w  where "
            SQL = SQL & "x.kode_Perusahaan=y.kode_perusahaan and x.no_faktur=y.no_faktur and x.status is null and "
            SQL = SQL & "y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.urut_oto=z.urut_tf and (z.selesai is null or z.selesai='Y') and "
            SQL = SQL & "z.kode_Perusahaan=w.kode_perusahaan and z.no_faktur=w.no_faktur and z.urut_oto=w.Urut_Det and "
            SQL = SQL & "c.Kode_Perusahaan = y.Kode_Perusahaan and c.Urut_Oto = y.urut_material_requisition_convert  and y.Flag_Jenis_Request = 'PRODUKSI'),0)), '0') as Total_TF, "
            SQL = SQL & "(c.jumlah - (c.Jumlah * (d.Toleransi_Tf_Min / 100))) as Toleransi_Min, "
            SQL = SQL & "(c.jumlah + (c.Jumlah * (d.Toleransi_Tf_Max / 100))) as Toleransi_Max "

            SQL = SQL & "from Emi_Material_Requisition a, EMI_Group_Jenis b, Emi_Material_Requisition_Det_Convert c, barang d, Emi_Material_Requisition_Det e "
            SQL = SQL & "where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and "
            SQL = SQL & "c.Kode_Perusahaan = e.Kode_Perusahaan and "
            SQL = SQL & "d.Kode_Perusahaan = e.Kode_Perusahaan and "
            SQL = SQL & "a.Id_Group_Jenis = b.Id_Group_Jenis and "
            SQL = SQL & "a.No_Faktur = c.No_Faktur and "
            SQL = SQL & "c.no_faktur = e.no_faktur and "
            SQL = SQL & "c.kode_stock_owner = e.kode_stock_owner and "
            SQL = SQL & "c.kode_barang = e.kode_barang and "
            SQL = SQL & "c.no_urut_det = e.urut_oto and "
            SQL = SQL & "d.Kode_Stock_Owner = e.Kode_Stock_Owner_tujuan and "
            SQL = SQL & "d.kode_barang = e.kode_barang and "
            SQL = SQL & "a.Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "d.kode_stock_owner='" & lokasi_kirim & "' and "
            SQL = SQL & "a.Flag_Process = 'Y' and a.status is null and "
            SQL = SQL & "c.Flag_Transfer is null and "
            SQL = SQL & "c.jumlah > 0 and c.jumlah_barang > 0 and "

            If SelectedSplit.Trim.Length > 0 Then
                SQL = SQL & "a.no_faktur_order = '" & SelectedSplit & "' and "
            End If

            If Not Chk_Belum_Selesai.Checked Then
                If filter Then
                    If Cmb_Filter.SelectedIndex <> 0 Then
                        SQL = SQL & arrFilter(Cmb_Filter.SelectedIndex) & " like '%" & Txt_Value_Filter.Text.Trim & "%' and "
                    End If
                End If
            End If

            SQL = SQL & "d.Id_Kategori_Gudang = ( "
            SQL = SQL & "select top 1 z.Id_Kategori_Gudang "
            SQL = SQL & "from EMI_Kategori_Gudang_PerLokasi z "
            SQL = SQL & "where a.Kode_Perusahaan = z.kode_perusahaan and z.Lokasi_Gudang = d.kode_stock_owner ) and "
            SQL = SQL & "a.No_Faktur_Order in ( "
            SQL = SQL & "select distinct No_Faktur_Order from cte z) and "
            SQL = SQL & "case when a.flag_tambah = 'Y' then a.Flag_Validasi_Tambah else 'Y' end = 'Y' "
            SQL = SQL & "order by a.Tanggal " & CmbOrder.Text & ", a.Jam " & CmbOrder.Text & ", a.Kode_Barang, a.Keterangan "
            Using Dr = OpenTrans(SQL)

                ' Gunakan list buffer agar AddRange() bisa dipakai
                Dim items As New List(Of ListViewItem)

                Do While Dr.Read
                    Dim lv As New ListViewItem(Dr("no_faktur_order").ToString())
                    lv.SubItems.Add(Dr("Kode_Stock_Owner").ToString())
                    lv.SubItems.Add(Dr("Kode_Barang").ToString())
                    lv.SubItems.Add(Dr("Nama").ToString())
                    lv.SubItems.Add(General_Class.CekNULL(Dr("keterangan")).ToString())
                    lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    lv.SubItems.Add(Format(Dr("Total_TF"), "N4"))
                    lv.SubItems.Add(Dr("Satuan_Display").ToString())

                    ' Kolom tambahan (hidden / internal)
                    lv.SubItems.Add(Dr("Jam").ToString())
                    lv.SubItems.Add(Dr("Satuan").ToString())
                    lv.SubItems.Add(Dr("UserId").ToString())
                    lv.SubItems.Add(Dr("warna").ToString())

                    ' Cek Flag Opname
                    If Flag_Opname = True Then
                        lv.SubItems.Add(Format(0, "N4"))
                        lv.SubItems.Add(Dr("Satuan").ToString())
                        lv.SubItems.Add(Format(0, "N4"))
                    Else
                        lv.SubItems.Add(Format(Dr("Good_Stock"), "N4"))
                        lv.SubItems.Add(Dr("Satuan").ToString())
                        lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N4"))
                    End If

                    If General_Class.CekNULL(Dr("Satuan_Isi_Bags")).ToString() = "" Then
                        lv.SubItems.Add("-")
                    Else
                        lv.SubItems.Add(Dr("Satuan_Isi_Bags").ToString())
                    End If

                    lv.SubItems.Add(Dr("Urut_Oto").ToString())
                    lv.SubItems.Add(Dr("Metode_Pengeluaran_Stok").ToString())
                    lv.SubItems.Add(Dr("jenis_kemasan").ToString())

                    ' Warna baris berdasarkan kondisi
                    Dim totalTF As Double = Val(Dr("Total_TF"))
                    Dim jumlah As Double = Val(Dr("Jumlah"))

                    If totalTF > jumlah Then
                        lv.BackColor = Color.LightGreen
                    ElseIf totalTF > 0 Then
                        lv.BackColor = Color.LightYellow
                    Else
                        lv.BackColor = Color.White
                    End If

                    ' Tambahkan ke buffer
                    items.Add(lv)
                Loop

                ' --- tampilkan sekaligus di ListView ---
                Lv_Data.BeginUpdate()
                Lv_Data.Items.Clear()
                Lv_Data.Items.AddRange(items.ToArray())
                Lv_Data.EndUpdate()

            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If totalpage = CurrentPage Then
            BtnNext.Enabled = False
        Else
            BtnNext.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev.Enabled = False
        Else
            BtnPrev.Enabled = True
        End If

    End Sub

    Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Limit.Text.Trim.Length = 0 Then
            MessageBox.Show("Limit Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Limit.Focus()
            Exit Sub
        End If

        Chk_Belum_Selesai.Checked = False
        Cmb_Filter.SelectedIndex = 0

        Kosong()

        Lv_Data.Focus()
    End Sub
    Private Sub Btn_cari_Click(sender As Object, e As EventArgs) Handles Btn_cari.Click
        If Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Harap Pilih Filter Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        If Cmb_Filter.SelectedIndex > 0 Then
            If Txt_Value_Filter.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Harus Diisi Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Value_Filter.Focus()
                Exit Sub
            End If
        End If

        CurrentPage = 1
        Kosong(True, CurrentPage)

        If totalpage = CurrentPage Then
            BtnNext.Enabled = False
        Else
            BtnNext.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev.Enabled = False
        Else
            BtnPrev.Enabled = True
        End If
    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If asal = "TF_Stock" Then

            'N_EMI_Transaksi_Transfer_Stock_Sementara.kosong()
            N_EMI_Transaksi_Transfer_Stock_Sementara.CmbJnsTransfer.Enabled = False
            N_EMI_Transaksi_Transfer_Stock_Sementara.CmbSO_Asal.Enabled = False
            N_EMI_Transaksi_Transfer_Stock_Sementara.CmbSo_Tujuan.Enabled = False

            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtKd_Barang.Text = String.Empty
            N_EMI_Transaksi_Transfer_Stock_Sementara.Txt_SO.Text = String.Empty
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtNm_Barang.Text = String.Empty

            Get_Isi_ListView(Lv_Data.FocusedItem.Index)
            N_EMI_Transaksi_Transfer_Stock_Sementara.asal = Jenis
            N_EMI_Transaksi_Transfer_Stock_Sementara.Lv_DetBarang.Visible = False
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtKd_Barang.Enabled = False
            'N_EMI_Transaksi_Transfer_Stock_Sementara.Btn_GetData.Enabled = False
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtKd_Barang.Text = lv_KdBrg
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtNm_Barang.Text = lv_NmBrg
            N_EMI_Transaksi_Transfer_Stock_Sementara.Txt_SO.Text = lv_kodeSO
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtSatuanKecil.Text = lv_Satuan
            N_EMI_Transaksi_Transfer_Stock_Sementara.Txt_Warna.Text = lv_Warna
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtStock.Text = Format(lv_GoodStock, "N4")
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtSatuan.Text = lv_SatuanDisplay
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtBags.Text = Format(Val(HilangkanTanda(lv_JmlBags)), "N4")

            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtJenisBags.Text = LvJenisKemasan

            N_EMI_Transaksi_Transfer_Stock_Sementara.Cmb_Warna.SelectedItem = lv_Warna

            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtStockDisplay.Text = Format(Val(HilangkanTanda(lv_GoodStock)), "N4") + " " + lv_SatuanDisplay

            Dim Jumlah_Permintaan As Double = lv_Jumlah - lv_JumlahTF

            N_EMI_Transaksi_Transfer_Stock_Sementara.Txt_JumlahPermintaan.Text = Format(Jumlah_Permintaan, "N4")
            N_EMI_Transaksi_Transfer_Stock_Sementara.Txt_SatuanPermintaan.Text = lv_SatuanDisplay
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtjmlPermintaanDisplay.Text = Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4") + " " + lv_SatuanDisplay
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtjmlPermintaanBersih.Text = HilangkanTanda(Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4"))
            N_EMI_Transaksi_Transfer_Stock_Sementara.Txt_OtoMaterial_req.Text = Lv_Oto
            N_EMI_Transaksi_Transfer_Stock_Sementara.Txt_Jenis_Transfer.Text = "PRODUKSI"
            N_EMI_Transaksi_Transfer_Stock_Sementara.TxtMetPotStok.Text = Lv_MetPotStok
            N_EMI_Transaksi_Transfer_Stock_Sementara.Txt_Urut_Request.Text = Lv_Oto


            Try
                OpenConn()

                '======================
                '=     GET SATUAN     =
                '======================
                N_EMI_Transaksi_Transfer_Stock_Sementara.Cmb_Satuan_Barang.Items.Clear()
                SQL = "select Satuan, Flag_Default from N_EMI_Master_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & lv_KdBrg & "' order by Satuan"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        N_EMI_Transaksi_Transfer_Stock_Sementara.Cmb_Satuan_Barang.Items.Add(Dr("Satuan"))
                        If General_Class.CekNULL(Dr("Flag_Default")) = "Y" Then
                            N_EMI_Transaksi_Transfer_Stock_Sementara.Cmb_Satuan_Barang.Text = Dr("Satuan")
                        End If
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            'N_EMI_Transaksi_Transfer_Stock_Sementara.Cmb_Satuan_Barang.DroppedDown = True
            'N_EMI_Transaksi_Transfer_Stock_Sementara.Cmb_Satuan_Barang.Focus()




            'N_EMI_Transaksi_Transfer_Stock_Sementara.Btn_Insert_Click(Lv_Data, e)
            'N_EMI_Transaksi_Transfer_Stock_Sementara.DGV_Data_TF.Rows.Clear()


        ElseIf asal = "Split_Stock" Then


            'N_EMI_Transaksi_Transfer_Stock_Sementara.kosong()
            Emi_Split_Stock_QC.CmbJnsTransfer.Enabled = False
            Emi_Split_Stock_QC.CmbSO_Asal.Enabled = False
            Emi_Split_Stock_QC.CmbSo_Tujuan.Enabled = False

            Emi_Split_Stock_QC.TxtKd_Barang.Text = String.Empty
            Emi_Split_Stock_QC.Txt_SO.Text = String.Empty
            Emi_Split_Stock_QC.TxtNm_Barang.Text = String.Empty

            Get_Isi_ListView(Lv_Data.FocusedItem.Index)
            Emi_Split_Stock_QC.asal = Jenis
            Emi_Split_Stock_QC.Lv_DetBarang.Visible = False
            Emi_Split_Stock_QC.TxtKd_Barang.Enabled = False
            'N_EMI_Transaksi_Transfer_Stock_Sementara.Btn_GetData.Enabled = False
            Emi_Split_Stock_QC.TxtKd_Barang.Text = lv_KdBrg
            Emi_Split_Stock_QC.TxtNm_Barang.Text = lv_NmBrg
            Emi_Split_Stock_QC.Txt_SO.Text = lv_kodeSO
            Emi_Split_Stock_QC.TxtSatuanKecil.Text = lv_Satuan
            Emi_Split_Stock_QC.Txt_Warna.Text = lv_Warna
            Emi_Split_Stock_QC.TxtStock.Text = Format(lv_GoodStock, "N4")
            Emi_Split_Stock_QC.TxtSatuan.Text = lv_SatuanDisplay
            Emi_Split_Stock_QC.TxtBags.Text = Format(Val(HilangkanTanda(lv_JmlBags)), "N4")
            Emi_Split_Stock_QC.TxtJenisBags.Text = LvJenisKemasan
            Emi_Split_Stock_QC.TxtMetPotStok.Text = Lv_MetPotStok
            Emi_Split_Stock_QC.Cmb_Warna.SelectedItem = lv_Warna
            Emi_Split_Stock_QC.TxtStockDisplay.Text = Format(Val(HilangkanTanda(lv_GoodStock)), "N4") + " " + lv_SatuanDisplay

            Dim Jumlah_Permintaan As Double = lv_Jumlah - lv_JumlahTF

            Emi_Split_Stock_QC.Txt_JumlahPermintaan.Text = Format(Jumlah_Permintaan, "N4")
            Emi_Split_Stock_QC.Txt_SatuanPermintaan.Text = lv_SatuanDisplay
            Emi_Split_Stock_QC.TxtjmlPermintaanDisplay.Text = Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4") + " " + lv_SatuanDisplay
            Emi_Split_Stock_QC.TxtjmlPermintaanBersih.Text = HilangkanTanda(Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4"))
            Emi_Split_Stock_QC.Txt_OtoMaterial_req.Text = Lv_Oto
            Emi_Split_Stock_QC.Txt_Jenis_Transfer.Text = "PRODUKSI"
            Emi_Split_Stock_QC.Txt_Urut_Request.Text = Lv_Oto

            Try
                OpenConn()

                '======================
                '=     GET SATUAN     =
                '======================
                Emi_Split_Stock_QC.Cmb_Satuan_Barang.Items.Clear()
                SQL = "select Satuan, flag_dasar from N_EMI_Master_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & lv_KdBrg & "' order by Satuan"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Emi_Split_Stock_QC.Cmb_Satuan_Barang.Items.Add(Dr("Satuan"))
                        If General_Class.CekNULL(Dr("flag_dasar")) = "Y" Then
                            Emi_Split_Stock_QC.Cmb_Satuan_Barang.Text = Dr("Satuan")
                        End If
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            'Emi_Split_Stock_QC.Cmb_Satuan_Barang.DroppedDown = True
            'Emi_Split_Stock_QC.Cmb_Satuan_Barang.Focus()


            'Emi_Split_Stock_QC.Btn_Insert_Click(Lv_Data, e)
            'N_EMI_Transaksi_Transfer_Stock_Sementara.DGV_Data_TF.Rows.Clear()




        End If



        Me.Close()


    End Sub

    Private Sub SelesaiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelesaiToolStripMenuItem.Click

        Try
            OpenConn()

            Dim Hapus1 As String = MessageBox.Show("Anda yakin ingin selesaikan data", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Hapus1 = vbYes Then
                '==============================
                '=     UPDATE FLAG TAMPIL     =
                ''=============================
                Get_Isi_ListView(Lv_Data.FocusedItem.Index)

                SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = 'Y' where Urut_Oto = '" & Lv_Oto & "' "
                ExecuteTrans(SQL)
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
    End Sub

    Private Sub Txt_Limit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Limit.KeyPress


        If e.KeyChar = Chr(13) Then
            Btn_Simpan.PerformClick()
            Return
        End If

        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            Return
        End If

        Chk_Belum_Selesai.Checked = False

        Dim txt As TextBox = DirectCast(sender, TextBox)

        Dim futureText As String = txt.Text.Substring(0, txt.SelectionStart) & e.KeyChar & txt.Text.Substring(txt.SelectionStart + txt.SelectionLength)

        If futureText.Length > 1 AndAlso futureText.StartsWith("0") Then
            e.Handled = True
            Return
        End If

        Dim value As Integer
        If Integer.TryParse(futureText, value) Then
            If value < 0 OrElse value > 10000 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If



    End Sub


    Private Sub Txt_Limit_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Limit.KeyDown
        If Txt_Limit.Text.Trim() = "" Then Exit Sub

        Dim nilai As Integer
        If Not Integer.TryParse(Txt_Limit.Text, nilai) Then
            Txt_Limit.Text = "0"
        ElseIf nilai < 0 Then
            Txt_Limit.Text = "0"
        ElseIf nilai > 10000 Then
            Txt_Limit.Text = "10000"
        End If
    End Sub

    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
        If Cmb_Filter.SelectedIndex = 0 Then
            Txt_Value_Filter.Enabled = False
        Else
            Txt_Value_Filter.Enabled = True
            Chk_Belum_Selesai.Checked = False
        End If
        Txt_Value_Filter.Text = ""
    End Sub

    Private Sub Txt_Value_Filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value_Filter.KeyPress
        If e.KeyChar = Chr(13) Then
            Btn_cari.Focus()
        End If
    End Sub
    Private Sub Chk_Belum_Selesai_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Belum_Selesai.CheckedChanged
        If Chk_Belum_Selesai.Checked Then
            Cmb_Filter.SelectedIndex = 0
        End If

        Btn_cari.PerformClick()

    End Sub



End Class