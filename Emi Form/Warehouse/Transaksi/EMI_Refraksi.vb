Imports System.Data
Imports Newtonsoft.Json

Public Class EMI_Refraksi

    Dim arrSO, arrInisialFaktur, arrIdWMSWarehouse, WarehosePosition As New ArrayList

    Dim lv_DetKodeSO, lv_DetKodeBarang, lv_DetNamaBarang As String

    Dim dgv_Lokasi, dgv_KodeBarang, dgv_SerialNumber, dgv_Nama, dgv_IDWareHouse, dgv_KodeRak As String
    Dim dgv_IDPallet, dgv_GoodStock, dgv_Satuan, dgv_Jumlah, dgv_JmlhBags, dgv_Batch, dgv_Harga, dgv_HargaDisplay As String
    Dim dgv_Warna, dgv_HargaBerubah, dgv_tglProduksi, dgv_TglExpired, dgv_KdUnikBerjalan, dgv_KdUnikAsal, dgv_stockBags As String
    Dim dgv_CheckBox As Boolean

    '''Dim itemDgvLokasi As Integer = 0
    '''Dim itemDgvKodeBarang As Integer = 1
    '''Dim itemDgvNamaBarang As Integer = 2
    '''Dim itemDgvSerialNumber As Integer = 3
    '''Dim itemDgvStock As Integer = 4
    '''Dim itemDgvSatuan As Integer = 5
    '''Dim itemDgvStockBags As Integer = 6
    '''Dim itemDgvCheckBox As Integer = 7
    '''Dim itemDgvJumlah As Integer = 8
    '''Dim itemDgvBags As Integer = 9
    '''Dim itemDgvBatchNumber As Integer = 10
    '''Dim itemDgvHargaDisplay As Integer = 11

    Dim itemDgvLokasi As Integer = 0
    Dim itemDgvKodeBarang As Integer = 1
    Dim itemDgvSerialNumber As Integer = 2
    Dim itemDgvNama As Integer = 3
    Dim itemDgvIDWareHose As Integer = 4
    Dim itemDgvKodeRak As Integer = 5
    Dim itemDgvIDPallet As Integer = 6
    Dim itemDgvGoodStock As Integer = 7
    Dim itemDgvSatuan As Integer = 8
    Dim itemDgvStockBags As Integer = 9
    Dim itemDgvCheckBox As Integer = 10
    Dim itemDgvJumlah As Integer = 11
    Dim itemDgvBags As Integer = 12
    Dim itemDgvBatch As Integer = 13
    Dim itemDgvHarga As Integer = 14
    Dim itemDgvHargaDisplay As Integer = 15
    Dim itemDgvWarna As Integer = 16
    Dim itemDgvHargaBerubah As Integer = 17
    Dim itemDgvTglProduksi As Integer = 18
    Dim itemDgvTglExpired As Integer = 19
    Dim itemDgvKdUnikBerjalan As Integer = 20
    Dim itemDgvKdUnikAsal As Integer = 21

    Dim kd_barang As String
    Dim TotalTransfer As Double = 0

    Private Sub Dgv_DataDetailRefraksi_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataDetailRefraksi.CellContentClick

    End Sub

    Public getDataNoFak, getDataKodeSO, getDataKdBrg, getDataSatuan, getDataSatuanBrg, getDataNoSJ As String

    Private Sub Transfer_Stock_3_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Transfer_Stock_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()

        ''Initial_List_View()

    End Sub

    Private Sub Load_Data_DGV()

        '''Try
        '''    OpenConn()
        '''    Dim rows As Integer = 0
        '''    Dgv_DataDetailRefraksi.Rows.Clear()
        '''SQL = "select a.lokasi, a.Kode_Barang, b.nama as nama_barang, a.Serial_Number, a.Jumlah as stock, a.satuan, a.Jumlah_Bags as stock_bags, "
        '''SQL = SQL & "a.Batch_Number, dbo.get_hpp(a.Serial_Number) as Harga "
        '''SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, barang b "
        '''SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_perusahaan = b.Kode_Perusahaan "
        '''SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
        '''SQL = SQL & "order by a.kode_barang "
        '''Using Dr = OpenTrans(SQL)
        '''    Do While Dr.Read
        '''        Dim subArr As New List(Of String)
        '''        Dgv_DataDetailRefraksi.Rows.Add(1)
        '''        '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvLokasi).Value = Dr("lokasi")
        '''        '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvKodeBarang).Value = Dr("Kode_Barang")
        '''        '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvNamaBarang).Value = Dr("nama_barang")
        '''        '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvSerialNumber).Value = Dr("Serial_Number")
        '''        '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvStock).Value = Format(Dr("stock"), "N0")
        '''        '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvSerialNumber).Value = Dr("satuan")
        '''        '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvStock).Value = Format(Dr("stock_bags"), "N0")
        '''        '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvJumlah).Value = Format(Dr("Harga"), "N0")
        '''        '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvBatchNumber).Value = Dr("Batch_Number")
        '''        '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvHargaDisplay).Value = Format(Dr("Harga"), "N0")
        '''        Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
        '''        Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvBags).ReadOnly = True
        '''        rows = rows + 1
        '''    Loop
        '''End Using
        '''

        '''    CloseConn()
        '''Catch ex As Exception
        '''    CloseConn()
        '''    MessageBox.Show(ex.Message)
        '''    Exit Sub
        '''End Try

        Try
            OpenConn()

            Dim rows As Integer = 0
            Dgv_DataDetailRefraksi.Rows.Clear()

            kd_barang = String.Empty
            kd_barang = TxtKd_Barang.Text

            arrIdWMSWarehouse.Clear()
            WarehosePosition.Clear()

            SQL = "Select a.Id_WMS_Warehouse_Position, a.Keterangan  from "
            SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
            SQL = SQL & "where a.id_wms_warehouse_position = b.id_wms_warehouse_position "
            SQL = SQL & "And a.KOde_Perusahaan = b.KOde_Perusahaan "
            SQL = SQL & "And b.kode_Barang Is null "
            SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
            SQL = SQL & "group by a.Id_WMS_Warehouse_Position, a.Keterangan "
            SQL = SQL & "order by a.Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    WarehosePosition.Add(Dr("Keterangan")) : arrIdWMSWarehouse.Add(Dr("Id_WMS_Warehouse_Position"))
                Loop
            End Using

            '''''SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, dbo.get_hpp(a.Serial_Number) as Harga, b.Nama, a.Id_Warehouse, c.Keterangan as kode_rak, "
            '''''SQL = SQL & " a.Id_Nametag_pallet,
            '''''            dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, b.satuan, "
            '''''SQL = SQL & "'" & TxtSatuan.Text & "', a.jumlah) as jumlah, b.satuan, a.nomor_pallet, ISNULL(a.Jumlah_Bags, 0) as stock_bags, a.Batch_number "

            '''''SQL = SQL & ",dbo.ubah_satuan('" & KodePerusahaan & "', 'UANG','" & TxtKd_Barang.Text & "', '" & TxtSatuanKecil.Text & "',"
            '''''SQL = SQL & "'" & TxtSatuan.Text & "', dbo.get_hpp(a.Serial_Number) ) as Harga_display "

            '''''SQL = SQL & "from barang_sn a, barang b, View_Warehouse_Position c "
            '''''SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang and a.kode_stock_Owner=b.kode_stock_Owner "
            '''''SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner=c.Kode_Stock_Owner "
            '''''SQL = SQL & "and a.Id_Warehouse=c.Id_WMS_Warehouse_Position "
            '''''SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            '''''SQL = SQL & "and b.Kode_Stock_Owner='" & Txt_SO.Text & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "'"
            '''''SQL = SQL & "order by a.Kode_Barang "

            SQL = "select b.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, dbo.get_hpp(a.Serial_Number) as Harga, d.Nama as nama_barang, "
            SQL = SQL & "a.Id_Warehouse, e.Keterangan as kode_rak, "
            SQL = SQL & "dbo.Ubah_Satuan(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, '" & getDataSatuan & "' ,a.Jumlah) as jumlah, "
            SQL = SQL & "a.satuan, c.nomor_pallet, "
            SQL = SQL & "ISNULL(a.Jumlah_Bags, 0) as stock_bags, "
            SQL = SQL & "a.Batch_Number, "
            SQL = SQL & "dbo.ubah_satuan('" & KodePerusahaan & "', 'UANG','" & getDataKdBrg & "', '" & getDataSatuanBrg & "',"
            SQL = SQL & "'" & getDataSatuan & "', dbo.get_hpp(a.Serial_Number) ) as Harga_display, a.Warna, "
            SQL = SQL & "a.Tgl_Produksi_Real, a.Tgl_Expired_Real, a.Kode_Unik_Berjalan, a.Kode_Unik_Asal "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, EMI_Barang_Masuk_Perpallet_Detail b, barang_sn c, barang d, View_Warehouse_Position e "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.kode_perusahaan = d.kode_perusahaan and a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur and a.No_Pembelian_Loading = '" & getDataNoFak & "' "
            SQL = SQL & "and a.kode_barang = b.kode_barang and a.kode_barang = c.kode_barang and a.kode_barang = d.kode_barang "
            SQL = SQL & "and a.serial_number = c.serial_number and b.kode_stock_owner = c.kode_stock_owner and b.kode_stock_owner = d.kode_stock_owner "
            SQL = SQL & "and b.kode_stock_owner = e.kode_stock_owner and a.Id_Warehouse = e.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.kode_barang = '" & getDataKdBrg & "' "

            Using Dr = OpenTrans(SQL)

                Do While Dr.Read

                    Dim subArr As New List(Of String)

                    Dgv_DataDetailRefraksi.Rows.Add(1)
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvLokasi).Value = Dr("Kode_Stock_Owner")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvKodeBarang).Value = Dr("Kode_Barang")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvSerialNumber).Value = Dr("Serial_Number")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvNama).Value = Dr("nama_barang")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvIDWareHose).Value = Dr("Id_Warehouse")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvKodeRak).Value = Dr("kode_rak")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvIDPallet).Value = Dr("nomor_pallet")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvGoodStock).Value = Format(Dr("jumlah"), "N0")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvStockBags).Value = Format(Dr("stock_bags"), "N0")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvBatch).Value = Dr("Batch_number")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvHarga).Value = Dr("Harga")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvHargaDisplay).Value = Format(Dr("Harga_display"), "N0")

                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvWarna).Value = Dr("Warna")
                    If Dr("Warna").ToString.ToUpper = "HIJAU" Then
                        Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvWarna).Style.BackColor = Color.LightGreen
                    ElseIf Dr("Warna").ToString.ToUpper = "KUNING" Then
                        Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvWarna).Style.BackColor = Color.LightYellow
                    ElseIf Dr("Warna").ToString.ToUpper = "MERAH" Then
                        Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvWarna).Style.BackColor = ColorTranslator.FromHtml("#f73333") ' LightRed
                    End If

                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvTglProduksi).Value = Dr("Tgl_Produksi_Real")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvTglExpired).Value = Dr("Tgl_Expired_Real")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvKdUnikBerjalan).Value = Dr("Kode_Unik_Berjalan")
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvKdUnikAsal).Value = Dr("Kode_Unik_Asal")

                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvSatuan).Value = getDataSatuan

                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvBags).ReadOnly = True

                    rows = rows + 1
                Loop
            End Using

            TxtTotalTransfer.Text = 0
            TxtTotalTransferBags.Text = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    'FUNCTION UTILITY
    Private Sub kosong()
        get_jam()

        Lv_DataRefraksi.Columns.Clear()
        Lv_DataRefraksi.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Nama Supplier", 200, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("No SJ", 100, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("No Plat", 100, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Driver", 100, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("Jam", 80, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("Tgl Masuk", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("Jam Masuk", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("UserID", 80, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("Tgl OTW", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("ETA", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.Columns.Add("ETD", 0, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Jenis Muatan", 350, HorizontalAlignment.Left)
        Lv_DataRefraksi.Columns.Add("Telepon", 0, HorizontalAlignment.Center)
        Lv_DataRefraksi.View = View.Details

        'DGV_Data_TF.Columns(itemDgvHarga).DisplayIndex = 0
        Dgv_DataDetailRefraksi.Columns(itemDgvHargaDisplay).DisplayIndex = 6
        Dgv_DataDetailRefraksi.Columns(itemDgvBatch).DisplayIndex = 0

        Try
            OpenConn()
            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    arrSO.Add(dr("kode_stock_owner"))
                    CmbSO_Asal.Items.Add(dr("Keterangan")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_DetBarang.Items.Clear()

        Txt_Tot_Harga.Text = "0"
        TxtSatuanKecil.Text = ""
        TxtSatuan.Text = ""
        TxtStock.Text = ""
        TxtKeterangan.Text = String.Empty
        TxtKd_Barang.Text = String.Empty
        Txt_SO.Text = String.Empty
        TxtNm_Barang.Text = String.Empty
        TxtBags.Text = String.Empty
        'TxtSatuanBags.Text = String.Empty
        TxtTotalTransferBags.Text = String.Empty

        CmbSO_Asal.Enabled = True
        CmbSO_Asal.SelectedIndex = 1

        Dgv_DataDetailRefraksi.Rows.Clear()

        Load_Data_DGV()

    End Sub

    Private Sub Lv_DataRefraksi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_DataRefraksi.SelectedIndexChanged
        If CmbSO_Asal.Items.Count = 0 Or CmbSO_Asal.SelectedIndex = -1 Then Exit Sub

        If TxtKd_Barang.Text.Trim.Length = 0 Or TxtKd_Barang.Text.Trim = "" Or TxtNm_Barang.Text.Trim.Length = 0 Then Exit Sub

        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("Isi SO Awal terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Dim rows As Integer = 0
            Dgv_DataDetailRefraksi.Rows.Clear()

            SQL = "select a.lokasi, a.Kode_Barang, b.nama as nama_barang, a.Serial_Number, a.Jumlah as stock, a.satuan, a.Jumlah_Bags as stock_bags, "
            SQL = SQL & "a.Batch_Number, dbo.get_hpp(a.Serial_Number) as Harga "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, barang b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "order by a.kode_barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim subArr As New List(Of String)
                    Dgv_DataDetailRefraksi.Rows.Add(1)

                    '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvLokasi).Value = Dr("lokasi")
                    '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvKodeBarang).Value = Dr("Kode_Barang")
                    '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvNamaBarang).Value = Dr("nama_barang")
                    '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvSerialNumber).Value = Dr("Serial_Number")
                    '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvStock).Value = Format(Dr("stock"), "N0")
                    '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvSerialNumber).Value = Dr("satuan")
                    '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvStock).Value = Format(Dr("stock_bags"), "N0")
                    '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvJumlah).Value = Format(Dr("Harga"), "N0")
                    '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvBatchNumber).Value = Dr("Batch_Number")
                    '''Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvHargaDisplay).Value = Format(Dr("Harga"), "N0")

                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
                    Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvBags).ReadOnly = True

                    rows = rows + 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    '''Private Sub get_det_barang(ByVal index As Integer)
    '''    lv_DetKodeSO = Lv_DetBarang.Items(index).SubItems(itemDetKodeSO).Text
    '''    lv_DetKodeBarang = Lv_DetBarang.Items(index).SubItems(itemDetKodeBarang).Text
    '''    lv_DetNamaBarang = Lv_DetBarang.Items(index).SubItems(itemDetNamaBarang).Text
    '''    lv_DetGoodStock = Lv_DetBarang.Items(index).SubItems(itemDetGoodStock).Text
    '''    lv_DetSatuan = Lv_DetBarang.Items(index).SubItems(itemDetSatuan).Text
    '''    lv_DetSatuanDIsplay = Lv_DetBarang.Items(index).SubItems(itemDetSatuandisplay).Text
    '''    lv_DetJmlhBags = Lv_DetBarang.Items(index).SubItems(itemDetJmlhBags).Text
    '''    lv_DetSatuanBags = Lv_DetBarang.Items(index).SubItems(itemDetSatuanBags).Text
    '''End Sub

    Private Sub get_grid_view(ByVal index As Integer)
        dgv_Lokasi = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvLokasi).Value
        dgv_KodeBarang = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvKodeBarang).Value
        dgv_SerialNumber = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvSerialNumber).Value
        dgv_Nama = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvNama).Value
        dgv_GoodStock = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvGoodStock).Value
        dgv_Satuan = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvSatuan).Value
        dgv_Jumlah = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvJumlah).Value
        dgv_Batch = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvBatch).Value
        dgv_HargaDisplay = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvHargaDisplay).Value
        dgv_JmlhBags = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvBags).Value
        dgv_Warna = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvWarna).Value
        dgv_HargaBerubah = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvHargaBerubah).Value

        dgv_tglProduksi = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvTglProduksi).Value
        dgv_TglExpired = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvTglExpired).Value
        dgv_KdUnikBerjalan = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvKdUnikBerjalan).Value
        dgv_KdUnikAsal = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvKdUnikAsal).Value
        dgv_stockBags = Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvStockBags).Value

        dgv_CheckBox = Convert.ToBoolean(Dgv_DataDetailRefraksi.Rows(index).Cells(itemDgvCheckBox).Value)

    End Sub

    Private Sub Initial_List_View()

        Lv_DetBarang.Columns.Add("Kode SO", 140, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Nama", 250, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Stock", 90, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Satuan", 0, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Jumlah Bags", 90, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Satuan", 0, HorizontalAlignment.Center)

        Lv_DetBarang.View = View.Details

    End Sub

    Private Sub DGV_Data_TF_MouseLeave(sender As Object, e As EventArgs)
        If Dgv_DataDetailRefraksi.RowCount = 0 Then Exit Sub

        Dim cellValue As String = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).Value

        If Not IsNumeric(cellValue) Then
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).Value = ""
        End If

        'PINDAH FOKUS
        Dgv_DataDetailRefraksi.CurrentCell = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvSatuan)
        'DGV_Data_TF.BeginEdit(True)
    End Sub

    Private Sub get_jam()
        Try
            OpenConn()

            SQL = "declare @ab int; select @ab = Selisih_Jam from Init; "
            SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss') as Tanggal_Sekarang "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    tgl_skg = dr("Tanggal_Sekarang")
                Loop
            End Using
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_no_faktur()
        TxtNo_Transaksi.Text = FRefraksi & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("EMI_Adjustment", "kode_adjustment", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(kode_adjustment,1," & Len(FRefraksi) + Len(arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex)) + 6 & ")", FRefraksi & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))
    End Sub

    'FUNCTION HANDLE
    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged, Txt_SO.TextChanged
        If CmbSO_Asal.Items.Count = 0 Or CmbSO_Asal.SelectedIndex = -1 Then Exit Sub

        If Not TxtKd_Barang.Text.Trim.Count = 0 Then
            Lv_DetBarang.Location = New Point(22, 256)
            Lv_DetBarang.Visible = True
        Else
            Lv_DetBarang.Location = New Point(803, 258)
            Lv_DetBarang.Visible = False
        End If

        Try
            OpenConn()

            Lv_DetBarang.Items.Clear()

            SQL = "select a.kode_stock_owner, a.kode_barang, a.nama, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
            SQL = SQL & "b.satuan, a.good_stock) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
            SQL = SQL & " a.Satuan_Isi_Bags from barang a, barang_detail_satuan b "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
            SQL = SQL & "and a.nama like '" & TxtKd_Barang.Text & "%' and a.Kode_Barang=b.kode_barang "
            SQL = SQL & "And a.kode_Perusahaan = b.kode_Perusahaan And b.flag_tampil_display ='Y'  "
            SQL = SQL & "order by a.Kode_Barang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As New ListViewItem
                    Lv = Lv_DetBarang.Items.Add(Dr("kode_stock_owner"))
                    Lv.SubItems.Add(Dr("kode_barang"))
                    Lv.SubItems.Add(Dr("nama"))
                    Lv.SubItems.Add(Dr("Good_Stock"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("satuan_display"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jumlah_Bags")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Satuan_Isi_Bags")))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    '''Private Sub Lv_DetBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetBarang.DoubleClick

    '''    If Lv_DetBarang.Items.Count = 0 Or TxtKd_Barang.Text.Trim = "" Then Exit Sub

    '''    get_det_barang(Lv_DetBarang.FocusedItem.Index)

    '''    TxtKd_Barang.Text = String.Empty
    '''    Txt_SO.Text = String.Empty
    '''    TxtNm_Barang.Text = String.Empty

    '''    TxtKd_Barang.Text = lv_DetKodeBarang
    '''    Txt_SO.Text = lv_DetKodeSO
    '''    TxtNm_Barang.Text = lv_DetNamaBarang
    '''    TxtSatuan.Text = lv_DetSatuanDIsplay
    '''    TxtSatuanKecil.Text = lv_DetSatuan
    '''    TxtStock.Text = Format(Val(lv_DetGoodStock), "N0")
    '''    TxtBags.Text = Format(Val(lv_DetJmlhBags), "N0")
    '''    'TxtSatuanBags.Text = lv_DetSatuanBags

    '''    Lv_DetBarang.Location = New Point(803, 258)
    '''    Lv_DetBarang.Visible = False

    '''    Btn_GetData.Focus()
    '''End Sub

    Private Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_GetData.Click
        If TxtKd_Barang.Text.Trim.Length = 0 Or TxtKd_Barang.Text.Trim = "" Or TxtNm_Barang.Text.Trim.Length = 0 Then Exit Sub

        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("Isi SO Awal terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        '''Try
        '''    OpenConn()

        '''    Dim rows As Integer = 0
        '''    Dgv_DataDetailRefraksi.Rows.Clear()

        '''    kd_barang = String.Empty
        '''    kd_barang = TxtKd_Barang.Text

        '''    arrIdWMSWarehouse.Clear()
        '''    WarehosePosition.Clear()

        '''    SQL = "Select a.Id_WMS_Warehouse_Position, a.Keterangan  from "
        '''    SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
        '''    SQL = SQL & "where a.id_wms_warehouse_position = b.id_wms_warehouse_position "
        '''    SQL = SQL & "And a.KOde_Perusahaan = b.KOde_Perusahaan "
        '''    SQL = SQL & "And b.kode_Barang Is null "
        '''    SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
        '''    SQL = SQL & "group by a.Id_WMS_Warehouse_Position, a.Keterangan "
        '''    SQL = SQL & "order by a.Keterangan "
        '''    Using Dr = OpenTrans(SQL)
        '''        Do While Dr.Read
        '''            WarehosePosition.Add(Dr("Keterangan")) : arrIdWMSWarehouse.Add(Dr("Id_WMS_Warehouse_Position"))
        '''        Loop
        '''    End Using

        '''    SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, dbo.get_hpp(a.Serial_Number) as Harga, b.Nama, a.Id_Warehouse, c.Keterangan as kode_rak, "
        '''    SQL = SQL & " a.Id_Nametag_pallet, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, b.satuan, "
        '''    SQL = SQL & "'" & TxtSatuan.Text & "', a.jumlah) as jumlah, b.satuan, a.nomor_pallet, ISNULL(a.Jumlah_Bags, 0) as stock_bags, a.Batch_number "

        '''    SQL = SQL & ",dbo.ubah_satuan('" & KodePerusahaan & "', 'UANG','" & TxtKd_Barang.Text & "', '" & TxtSatuanKecil.Text & "',"
        '''    SQL = SQL & "'" & TxtSatuan.Text & "', dbo.get_hpp(a.Serial_Number) ) as Harga_display "

        '''    SQL = SQL & "from barang_sn a, barang b, View_Warehouse_Position c "
        '''    SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang and a.kode_stock_Owner=b.kode_stock_Owner "
        '''    SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner=c.Kode_Stock_Owner "
        '''    SQL = SQL & "and a.Id_Warehouse=c.Id_WMS_Warehouse_Position "
        '''    SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
        '''    SQL = SQL & "and b.Kode_Stock_Owner='" & Txt_SO.Text & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "'"
        '''    SQL = SQL & "order by a.Kode_Barang "
        '''    Using Dr = OpenTrans(SQL)
        '''        Do While Dr.Read

        '''            Dim subArr As New List(Of String)

        '''            Dgv_DataDetailRefraksi.Rows.Add(1)
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvLokasi).Value = Dr("Kode_Stock_Owner")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvKodeBarang).Value = Dr("Kode_Barang")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvSerialNumber).Value = Dr("Serial_Number")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvNama).Value = Dr("Nama")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvIDWareHose).Value = Dr("Id_Warehouse")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvKodeRak).Value = Dr("kode_rak")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvIDPallet).Value = Dr("nomor_pallet")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvGoodStock).Value = Format(Dr("jumlah"), "N0")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvStockBags).Value = Format(Dr("stock_bags"), "N0")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvBatch).Value = Dr("Batch_number")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvHarga).Value = Dr("Harga")
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvHargaDisplay).Value = Format(Dr("Harga_display"), "N0")

        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvSatuan).Value = TxtSatuan.Text

        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
        '''            Dgv_DataDetailRefraksi.Rows(rows).Cells(itemDgvBags).ReadOnly = True

        '''            rows = rows + 1

        '''        Loop
        '''    End Using

        '''    TxtTotalTransfer.Text = 0
        '''    TxtTotalTransferBags.Text = 0

        '''    CmbSO_Asal.Enabled = False

        '''    CloseConn()
        '''Catch ex As Exception
        '''    CloseConn()
        '''    MessageBox.Show(ex.Message)
        '''    Exit Sub
        '''End Try

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Dgv_DataDetailRefraksi.RowCount = 0 Then
            MessageBox.Show("Belum ada barang yang mau di transfer!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKd_Barang.Focus() : Exit Sub
            'ElseIf TxtKeterangan.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TxtKeterangan.Focus() : Exit Sub
        End If

        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("SO asal harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        '==========================================
        '=     CEK APAKAH ADA DATA DI CENTANG     =
        '==========================================
        Dim checked = False
        For i As Integer = 0 To Dgv_DataDetailRefraksi.RowCount - 1
            get_grid_view(i)

            Dim asdasda As String = dgv_CheckBox
            If dgv_CheckBox = True Then
                checked = True
                Exit For
            End If

            checked = False
        Next

        If checked = False Then
            MessageBox.Show("Tidak Ada yang di centang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()

#Region "KODE LAMA"

        '''Try
        '''    OpenConn()
        '''    Cmd.Transaction = Cn.BeginTransaction

        '''    get_no_faktur()

        '''    Dim nilai_kecil As Double = 0
        '''    SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKd_Barang.Text & "', '" & TxtSatuan.Text & "',"
        '''    SQL = SQL & "'" & TxtSatuanKecil.Text & "', '" & HilangkanTanda(TxtTotalTransfer.Text) & "' ) as hasil"
        '''    Using Dr1 = OpenTrans(SQL)
        '''        If Dr1.Read Then
        '''            If General_Class.CekNULL(Dr1("hasil")) = "" Then
        '''                Dr1.Close()
        '''                CloseTrans()
        '''                CloseConn()
        '''                MessageBox.Show("data konversi satuan kirim tidak ada ")
        '''                Exit Sub
        '''            End If

        '''            nilai_kecil = Dr1("hasil")
        '''        Else
        '''            Dr1.Close()
        '''            CloseTrans()
        '''            CloseConn()
        '''            MessageBox.Show("data konversi satuan kirim tidak ada ")
        '''            Exit Sub
        '''        End If
        '''    End Using

        '''    SQL = "Insert Into EMI_Adjustment (kode_perusahaan, kode_adjustment, tanggal, jam, kode_stock_owner, kode_barang, jumlah, Jumlah_Bags, "
        '''    SQL = SQL & "keterangan, userid, kode_voucher, Satuan, Total_Barang, Satuan_Barang) "
        '''    SQL = SQL & "Values('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
        '''    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & arrSO(CmbSO_Asal.SelectedIndex) & "', "
        '''    SQL = SQL & "'" & TxtKd_Barang.Text & "', " & HilangkanTanda(TxtTotalTransfer.Text) & ", "
        '''    SQL = SQL & "'" & HilangkanTanda(TxtTotalTransferBags.Text) & "', '" & TxtKeterangan.Text & "', '" & UserID & "', NULL, "
        '''    SQL = SQL & "'" & TxtSatuan.Text & "', '" & nilai_kecil & "', '" & TxtSatuanKecil.Text & "')"
        '''    ExecuteTrans(SQL)

        '''    Dim isCheck As Boolean = False

        '''    For row As Integer = 0 To Dgv_DataDetailRefraksi.RowCount - 1

        '''        get_grid_view(row)

        '''        If dgv_CheckBox = False Then
        '''            Continue For
        '''        End If

        '''        If dgv_Jumlah = "" Or dgv_JmlhBags = "" Then
        '''            CloseTrans()
        '''            CloseConn()
        '''            MessageBox.Show("Jumlah harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '''            Exit Sub
        '''        End If

        '''        isCheck = True

        '''        Dim nilai_kecildetail As Double = 0
        '''        SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKd_Barang.Text & "', '" & TxtSatuan.Text & "',"
        '''        SQL = SQL & "'" & TxtSatuanKecil.Text & "', '" & dgv_Jumlah.ToString & "' ) as hasil"
        '''        Using Dr1 = OpenTrans(SQL)
        '''            If Dr1.Read Then
        '''                If General_Class.CekNULL(Dr1("hasil")) = "" Then
        '''                    Dr1.Close()
        '''                    CloseTrans()
        '''                    CloseConn()
        '''                    MessageBox.Show("data konversi satuan kirim tidak ada ")
        '''                    Exit Sub
        '''                End If

        '''                nilai_kecildetail = Dr1("hasil")
        '''            Else
        '''                Dr1.Close()
        '''                CloseTrans()
        '''                CloseConn()
        '''                MessageBox.Show("data konversi satuan kirim tidak ada ")
        '''                Exit Sub
        '''            End If
        '''        End Using

        '''        '============================
        '''        '=       UPDATE STOCK       =
        '''        '============================

        '''        'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)

        '''        'SQL = "update barang_sn set jumlah = jumlah-'" & nilai_kecildetail & "', Jumlah_Bags = Jumlah_Bags-" & dgv_JmlhBags & " "
        '''        'SQL = SQL & "where Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' and Kode_Barang='" & dgv_KodeBarang & "' "
        '''        'SQL = SQL & "and Serial_Number='" & dgv_SerialNumber & "'"
        '''        'ExecuteTrans(SQL)

        '''        'SQL = "update barang set Good_Stock= Good_Stock-" & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags-" & dgv_JmlhBags & " "
        '''        'SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
        '''        'SQL = SQL & " and Kode_Barang='" & kd_barang & "'"
        '''        'ExecuteTrans(SQL)
        '''        Dim total_hpp As Double = 0

        '''        If Val(dgv_Jumlah) < 0 Then 'minus

        '''            Dim sisa As Double = 0

        '''            SQL = "select kode_stock_owner, kode_barang, serial_number, jumlah from barang_sn where "
        '''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '''            SQL = SQL & "kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' and "
        '''            SQL = SQL & "kode_barang = '" & dgv_KodeBarang & "' and serial_number='" & dgv_SerialNumber & "' "
        '''            SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
        '''            Using Ds = BindingTrans(SQL)
        '''                With Ds.Tables("MyTable")
        '''                    If .Rows.Count <> 0 Then
        '''                        sisa = Math.Abs(nilai_kecildetail)

        '''                        For h As Integer = 0 To .Rows.Count - 1
        '''                            If sisa = 0 Then
        '''                                Exit For
        '''                            ElseIf sisa < 0 Then
        '''                                CloseTrans()
        '''                                CloseConn()
        '''                                MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '''                                Exit Sub
        '''                            End If

        '''                            If sisa < .Rows(h).Item("jumlah") Or sisa = .Rows(h).Item("jumlah") Then
        '''                                SQL = "Update barang_sn set jumlah = jumlah - " & sisa & ", Jumlah_Bags = Jumlah_Bags - " & dgv_JmlhBags & " where "
        '''                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '''                                SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
        '''                                SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
        '''                                SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
        '''                                ExecuteTrans(SQL)

        '''                                SQL = "insert into EMI_Det_Adj(kode_perusahaan, no_faktur, "
        '''                                SQL = SQL & "kode_stock_owner, kode_barang, serial_number, "
        '''                                SQL = SQL & "jumlah, Jumlah_Bags) values('" & KodePerusahaan & "', "
        '''                                SQL = SQL & "'" & Trim(TxtNo_Transaksi.Text) & "', "
        '''                                SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
        '''                                SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
        '''                                SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
        '''                                SQL = SQL & "'" & sisa & "','" & dgv_JmlhBags & "')"
        '''                                ExecuteTrans(SQL)

        '''                                total_hpp = total_hpp + (sisa * Get_Harga_SN(.Rows(h).Item("serial_number")))

        '''                                sisa = 0
        '''                            ElseIf sisa > .Rows(h).Item("jumlah") Then
        '''                                SQL = "insert into EMI_Det_Adj(kode_perusahaan, no_faktur, "
        '''                                SQL = SQL & "kode_stock_owner, kode_barang, serial_number, "
        '''                                SQL = SQL & "jumlah, Jumlah_Bags) values('" & KodePerusahaan & "', "
        '''                                SQL = SQL & "'" & Trim(TxtNo_Transaksi.Text) & "', "
        '''                                SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
        '''                                SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
        '''                                SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
        '''                                SQL = SQL & "'" & .Rows(h).Item("jumlah") & "','" & dgv_JmlhBags & "')"
        '''                                ExecuteTrans(SQL)

        '''                                SQL = "Update barang_sn set jumlah = jumlah - jumlah, Jumlah_Bags = Jumlah_Bags - " & dgv_JmlhBags & " where "
        '''                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '''                                SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
        '''                                SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
        '''                                SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
        '''                                ExecuteTrans(SQL)

        '''                                total_hpp = total_hpp + (.Rows(h).Item("jumlah") * Get_Harga_SN(.Rows(h).Item("serial_number")))

        '''                                sisa = sisa - .Rows(h).Item("jumlah")
        '''                            Else
        '''                                CloseTrans()
        '''                                CloseConn()
        '''                                MessageBox.Show("Barang SN terjadi kesalahan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '''                                Exit Sub
        '''                            End If

        '''                            If sisa <> 0 And h = .Rows.Count - 1 Then
        '''                                CloseTrans()
        '''                                CloseConn()
        '''                                MessageBox.Show("Jumlah stock tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '''                                Exit Sub
        '''                            End If
        '''                        Next
        '''                    Else
        '''                        CloseTrans()
        '''                        CloseConn()
        '''                        MessageBox.Show("SN untuk barang ini tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '''                        Exit Sub
        '''                    End If
        '''                End With
        '''            End Using

        '''            SQL = "Update barang set good_stock = good_stock - " & Math.Abs(nilai_kecildetail) & ", Jumlah_Bags = Jumlah_Bags - " & dgv_JmlhBags & " where kode_perusahaan = '" & KodePerusahaan & "' and "
        '''            SQL = SQL & "kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' and kode_Barang = '" & dgv_KodeBarang & "'"
        '''            ExecuteTrans(SQL)
        '''        Else ' kalo nambahin stock

        '''            Dim Rand As New Random
        '''            Dim str As String = Format(Rand.Next(0, 999), "000") & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HHmmss")
        '''            Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)

        '''            Dim SN As String = dgv_SerialNumber 'Kode_Unik & Tanda_SN & "01" & Tanda_SN & dgv_Harga & Tanda_SN & "02" & Tanda_SN & Format(DateTimePicker1.Value, "yyyy-MM-dd")

        '''            SQL = "select kode_barang from barang_sn where "
        '''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '''            SQL = SQL & "kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' and "
        '''            SQL = SQL & "kode_barang = '" & dgv_KodeBarang & "' and serial_number = '" & SN & "'"
        '''            Using Dr = OpenTrans(SQL)
        '''                If Dr.Read Then
        '''                    Dr.Close()
        '''                    SQL = "Update barang_sn set jumlah = jumlah + " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags + " & dgv_JmlhBags & " where "
        '''                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '''                    SQL = SQL & "kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' and kode_barang = '" & dgv_KodeBarang & "' and "
        '''                    SQL = SQL & "serial_number = '" & SN & "'"
        '''                    ExecuteTrans(SQL)
        '''                    'Else
        '''                    '    SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
        '''                    '    SQL = SQL & "serial_number, jumlah, rr, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
        '''                    '    SQL = SQL & "'" & arrSO(CmbSO_Asal.SelectedIndex) & "', '" & dgv_KodeBarang & "', "
        '''                    '    SQL = SQL & "'" & SN & "', " & TextBox5.Text & ", 'X', '" & Format(Dtp_TglProd.Value, "yyyy-MM-dd") & "', '" & Format(Dtp_TglEx.Value, "yyyy-MM-dd") & "')"
        '''                    '    Dr.Close()
        '''                    '    ExecuteTrans(SQL)
        '''                End If
        '''            End Using

        '''            SQL = "insert into EMI_Det_Adj(kode_perusahaan, no_faktur, "
        '''            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, "
        '''            SQL = SQL & "jumlah, Jumlah_Bags) values('" & KodePerusahaan & "', "
        '''            SQL = SQL & "'" & Trim(TxtNo_Transaksi.Text) & "', "
        '''            SQL = SQL & "'" & arrSO(CmbSO_Asal.SelectedIndex) & "', "
        '''            SQL = SQL & "'" & dgv_KodeBarang & "', "
        '''            SQL = SQL & "'" & SN & "', "
        '''            SQL = SQL & "'" & nilai_kecildetail & "', '" & dgv_JmlhBags & "')"
        '''            ExecuteTrans(SQL)

        '''            '==========================================================

        '''            SQL = "Update barang set good_stock = good_stock + " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags + " & dgv_JmlhBags & " where kode_perusahaan = '" & KodePerusahaan & "' and "
        '''            SQL = SQL & "kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' and kode_Barang = '" & dgv_KodeBarang & "'"
        '''            ExecuteTrans(SQL)
        '''        End If

        '''    Next

        '''    'Cek STOCK BARANG
        '''    SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
        '''    SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
        '''    SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
        '''    SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
        '''    SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
        '''    SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
        '''    SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
        '''    SQL = SQL & "AND a.Kode_Barang = '" & kd_barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
        '''    SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
        '''    Using Ds = BindingTrans(SQL)
        '''        With Ds.Tables("MyTable")
        '''            If .Rows.Count <> 0 Then
        '''                If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
        '''                    Dim a As Double = .Rows(0).Item("good_stock")
        '''                    Dim b As Double = .Rows(0).Item("Jumlah_sn")
        '''                    CloseTrans()
        '''                    CloseConn()
        '''                    MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '''                    Exit Sub
        '''                End If
        '''            Else
        '''                CloseTrans()
        '''                CloseConn()
        '''                MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '''                Exit Sub
        '''            End If
        '''        End With
        '''    End Using

        '''    If isCheck <> True Then
        '''        CloseTrans()
        '''        CloseConn()
        '''        MessageBox.Show("Tidak Ada Data yang Dikirim . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '''        Exit Sub
        '''    End If

        '''    Cmd.Transaction.Commit()
        '''    MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '''    kosong()
        '''    CloseTrans()
        '''    CloseConn()
        '''Catch ex As Exception
        '''    CloseTrans()
        '''    CloseConn()
        '''    MessageBox.Show(ex.Message)
        '''    Exit Sub
        '''End Try

#End Region

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            '==============================
            '=     INSERT TABEL INDUK     =
            '==============================
            SQL = "insert into emi_refraksi (kode_perusahaan, no_transaksi, No_Pembelian_Loading, No_Sj, Tanggal, Jam, "
            SQL = SQL & "Kode_Stock_Owner, Kode_Barang, Total_Stock, Satuan, Total_Bags, Harga_Berubah, UserID) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & getDataNoFak & "', '" & getDataNoSJ & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & getDataKodeSO & "', '" & getDataKdBrg & "', "
            SQL = SQL & "'" & TxtTotalTransfer.Text & "', '" & getDataSatuan & "', '" & TxtTotalTransferBags.Text & "', '" & Txt_Tot_Harga.Text & "', "
            SQL = SQL & "'" & UserID & "')"
            ExecuteTrans(SQL)

            For i As Integer = 0 To Dgv_DataDetailRefraksi.RowCount - 1
                get_grid_view(i)

                If dgv_CheckBox = False Then
                    Continue For
                End If

                'Generate Sn Baru
                Dim Rand2 As New Random
                Dim sts2 As String = Format(Rand2.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                Dim Kode_Unik2 As String = sts2.Substring(0, 5) & Chr(64 + sts2.Substring(6, 1)) & sts2.Substring(6, Len(sts2) - 6)
                Dim SN_Baru As String = Kode_Unik2 & Tanda_SN & "01" & Tanda_SN & Get_Harga_SN(dgv_SerialNumber) & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                '===============================
                '=     INSERT TABEL DETAIL     =
                '===============================
                SQL = "insert into emi_refraksi_detail (kode_perusahaan, No_transaksi, Serial_Number, Serial_Number_Baru, Batch_Number, Kode_Unik_Berjalan, kode_unik_asal, "
                SQL = SQL & "id_warehouse, tgl_produksi, tgl_expired, stock, bags, satuan, harga_Awal, harga_berubah, warna) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & dgv_SerialNumber & "', '" & SN_Baru & "', '" & dgv_Batch & "', "
                SQL = SQL & "'" & dgv_KdUnikBerjalan & "', '" & dgv_KdUnikAsal & "', '" & dgv_IDWareHouse & "', '" & dgv_tglProduksi & "', "
                SQL = SQL & "'" & dgv_TglExpired & "', '" & dgv_GoodStock & "', '" & dgv_stockBags & "', '" & dgv_Satuan & "', '" & dgv_HargaDisplay & "', "
                SQL = SQL & "'" & dgv_HargaBerubah & "', '" & dgv_Warna & "')"
                ExecuteTrans(SQL)

                '===========================
                '=     UPDATE BARANG_SN    =
                '===========================

                SQL = "update barang_sn set jumlah = jumlah - " & dgv_GoodStock & " where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & dgv_Lokasi & "' "
                SQL = SQL & "and Kode_Barang = '" & dgv_KodeBarang & "' and Serial_Number = '" & dgv_SerialNumber & "' and Warna = '" & dgv_Warna & "'"
                ExecuteTrans(SQL)

                '================================
                '=     INSERT BARANG SN BARU    =
                '================================
                SQL = "select jumlah from barang_sn "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & dgv_Lokasi & "' and Kode_Barang = '" & dgv_KodeBarang & "' and Serial_Number = '" & dgv_SerialNumber & "' "
                SQL = SQL & "and Warna = '" & dgv_Warna & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            If Val(.Rows(0).Item("jumlah")) = 0 Then

                                SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                                SQL = SQL & "serial_number, jumlah, rr, Tgl_Produksi, Tgl_Expired, Warna) values('" & KodePerusahaan & "', "
                                SQL = SQL & "'" & dgv_Lokasi & "', '" & dgv_KodeBarang & "', "
                                SQL = SQL & "'" & SN_Baru & "', " & dgv_GoodStock & ", 'X', '" & Format(dgv_tglProduksi, "yyyy-MM-dd") & "', '" & Format(dgv_TglExpired, "yyyy-MM-dd") & "', "
                                SQL = SQL & "'" & dgv_Warna & "')"
                                ExecuteTrans(SQL)
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan Pada Barang SN ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang SN Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        End If
                    End With
                End Using

            Next

            CloseTrans()
            CloseConn()
            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CmbSO_Asal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSO_Asal.SelectedIndexChanged
        Dgv_DataDetailRefraksi.Rows.Clear()
        TxtKd_Barang.Text = ""
        Txt_SO.Text = ""
        TxtNm_Barang.Text = ""

        Try
            OpenConn()
            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub DGV_Data_TF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        Dim chkBox As String = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvCheckBox).Value
        Dim cellValue As String = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).Value
        Dim cellValue2 As String = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvBags).Value

        If chkBox = "True" Then
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).ReadOnly = False
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvBags).ReadOnly = False
        Else
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).ReadOnly = True
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvBags).ReadOnly = True

            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).Value = ""
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvBags).Value = ""

        End If

        If Not IsNumeric(cellValue) Then
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).Value = ""
        End If
        If Not IsNumeric(cellValue2) Then
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvBags).Value = ""
        End If

        Dim totalJmlh As Double = 0
        Dim totalBags As Double = 0

        For i As Integer = 0 To Dgv_DataDetailRefraksi.RowCount - 1
            If chkBox = "True" Then
                totalJmlh = totalJmlh + Val(isNull(Dgv_DataDetailRefraksi.Rows(i).Cells(itemDgvJumlah).Value))
                totalBags = totalBags + Val(isNull(Dgv_DataDetailRefraksi.Rows(i).Cells(itemDgvBags).Value))
            Else
                Continue For
            End If
        Next

        TxtTotalTransfer.Text = Format(totalJmlh, "N0")
        TxtTotalTransferBags.Text = Format(totalBags, "N0")

    End Sub

    Public Shared Function isNull(ByVal xNullString As String) As String
        Try
            If String.IsNullOrWhiteSpace(xNullString) Then
                Return "0"
            Else
                Return xNullString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "0"
        End Try
    End Function

    Private Sub Dgv_DataDetailRefraksi_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DataDetailRefraksi.CellEndEdit
        Dim chkBox As String = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvCheckBox).Value
        Dim cellValue As String = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).Value
        Dim cellValue2 As String = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvBags).Value
        Dim cellValue3 As String = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvHargaBerubah).Value

        Dim Stock As String = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvGoodStock).Value
        Dim Bags As String = Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvStockBags).Value

        If chkBox = "True" Then
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).ReadOnly = False
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvBags).ReadOnly = False
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvHargaBerubah).ReadOnly = False
        Else
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).ReadOnly = True
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvBags).ReadOnly = True
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvHargaBerubah).ReadOnly = True

            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).Value = ""
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvBags).Value = ""
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvHargaBerubah).Value = ""

        End If

        'If Not IsNumeric(cellValue) Then
        '    Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvJumlah).Value = ""
        '
        'End If
        'If Not IsNumeric(cellValue2) Then
        '    Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvBags).Value = ""
        '
        'End If
        If Not IsNumeric(cellValue3) Then
            Dgv_DataDetailRefraksi.CurrentRow.Cells(itemDgvHargaBerubah).Value = ""

        End If

        Dim totalJmlh As Double = 0
        Dim totalBags As Double = 0
        Dim totalHarga As Double = 0

        For i As Integer = 0 To Dgv_DataDetailRefraksi.RowCount - 1
            If chkBox = "True" Then
                totalJmlh = totalJmlh + Val(isNull(Dgv_DataDetailRefraksi.Rows(i).Cells(itemDgvGoodStock).Value))
                totalBags = totalBags + Val(isNull(Dgv_DataDetailRefraksi.Rows(i).Cells(itemDgvStockBags).Value))
                totalHarga = totalHarga + Val(isNull(Dgv_DataDetailRefraksi.Rows(i).Cells(itemDgvHargaBerubah).Value))
            Else
                Continue For
            End If
        Next

        TxtTotalTransfer.Text = "0"
        TxtTotalTransferBags.Text = "0"
        Txt_Tot_Harga.Text = "0"

        TxtTotalTransfer.Text = Format(totalJmlh, "N0")
        TxtTotalTransferBags.Text = Format(totalBags, "N0")
        Txt_Tot_Harga.Text = Format(totalHarga, "N2")
    End Sub

End Class