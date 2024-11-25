Imports System.Data.SqlClient
Imports System.Text
Imports Newtonsoft.Json

Public Class EMI_Transfer_Quality

    Private random As New Random()

    Dim arrSO, arrInisialFaktur, arrIdWMSWarehouse, WarehosePosition, kategoriQuality As New ArrayList

    Dim arr2RakTujuan As New List(Of List(Of String))

    Dim lv_DetKodeSO, lv_DetKodeBarang, lv_DetNamaBarang, lv_DetGoodStock, lv_DetSatuan, lv_DetSatuanDIsplay, lv_DetJmlhBags, lv_DetSatuanBags As String

    Dim dgv_Lokasi, dgv_KodeBarang, dgv_SerialNumber, dgv_Nama, dgv_IDWareHouse, dgv_KodeRak As String
    Dim dgv_IDPallet, dgv_GoodStock, dgv_Satuan, dgv_Jumlah, dgv_JmlhBags, dgv_Batch, dgv_Harga, dgv_HargaDisplay As String
    Dim dgv_CheckBox As Boolean

    Dim kd_barang As String

    Dim TotalTransfer As Double = 0

    Dim itemDetKodeSO As Integer = 0
    Dim itemDetKodeBarang As Integer = 1
    Dim itemDetNamaBarang As Integer = 2
    Dim itemDetGoodStock As Integer = 3
    Dim itemDetSatuan As Integer = 4
    Dim itemDetSatuandisplay As Integer = 5
    Dim itemDetJmlhBags As Integer = 6
    Dim itemDetSatuanBags As Integer = 7

    Dim itemDgvLokasi As Integer = 0
    Dim itemDgvKodeBarang As Integer = 1
    Dim itemDgvSerialNumber As Integer = 2
    Dim itemDgvNama As Integer = 3
    Dim itemDgvIDWareHose As Integer = 4
    Dim itemDgvIDPallet As Integer = 5
    Dim itemDgvCheckBox As Integer = 6
    Dim itemDgvBatch As Integer = 7
    Dim itemDgvKodeRak As Integer = 8
    Dim itemDgvHargaDisplay As Integer = 9
    Dim itemDgvGoodStock As Integer = 10
    Dim itemDgvSatuan As Integer = 11
    Dim itemDgvStockBags As Integer = 12
    Dim itemDgvJumlah As Integer = 13
    Dim itemDgvBags As Integer = 14
    Dim itemDgvHarga As Integer = 15


    'Dim itemDgvIDWareHouseTujuan As Integer = 10

    Private Sub Transfer_Stock_3_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Transfer_Stock_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
        Initial_List_View()

    End Sub

    Private Sub get_det_barang(ByVal index As Integer)
        lv_DetKodeSO = Lv_DetBarang.Items(index).SubItems(itemDetKodeSO).Text
        lv_DetKodeBarang = Lv_DetBarang.Items(index).SubItems(itemDetKodeBarang).Text
        lv_DetNamaBarang = Lv_DetBarang.Items(index).SubItems(itemDetNamaBarang).Text
        lv_DetGoodStock = Lv_DetBarang.Items(index).SubItems(itemDetGoodStock).Text
        lv_DetSatuan = Lv_DetBarang.Items(index).SubItems(itemDetSatuan).Text
        lv_DetSatuanDIsplay = Lv_DetBarang.Items(index).SubItems(itemDetSatuandisplay).Text
        lv_DetJmlhBags = Lv_DetBarang.Items(index).SubItems(itemDetJmlhBags).Text
        lv_DetSatuanBags = Lv_DetBarang.Items(index).SubItems(itemDetSatuanBags).Text
    End Sub

    Private Sub get_grid_view(ByVal index As Integer)
        dgv_Lokasi = DGV_Data_TF.Rows(index).Cells(itemDgvLokasi).Value
        dgv_KodeBarang = DGV_Data_TF.Rows(index).Cells(itemDgvKodeBarang).Value
        dgv_SerialNumber = DGV_Data_TF.Rows(index).Cells(itemDgvSerialNumber).Value
        dgv_Nama = DGV_Data_TF.Rows(index).Cells(itemDgvNama).Value
        dgv_IDWareHouse = DGV_Data_TF.Rows(index).Cells(itemDgvIDWareHose).Value
        dgv_KodeRak = DGV_Data_TF.Rows(index).Cells(itemDgvKodeRak).Value
        dgv_IDPallet = DGV_Data_TF.Rows(index).Cells(itemDgvIDPallet).Value
        dgv_GoodStock = DGV_Data_TF.Rows(index).Cells(itemDgvGoodStock).Value
        dgv_Satuan = DGV_Data_TF.Rows(index).Cells(itemDgvSatuan).Value
        dgv_Jumlah = DGV_Data_TF.Rows(index).Cells(itemDgvJumlah).Value
        dgv_Batch = DGV_Data_TF.Rows(index).Cells(itemDgvBatch).Value
        dgv_Harga = DGV_Data_TF.Rows(index).Cells(itemDgvHarga).Value
        dgv_HargaDisplay = DGV_Data_TF.Rows(index).Cells(itemDgvHargaDisplay).Value
        dgv_JmlhBags = DGV_Data_TF.Rows(index).Cells(itemDgvBags).Value

        dgv_CheckBox = Convert.ToBoolean(DGV_Data_TF.Rows(index).Cells(itemDgvCheckBox).Value)

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

    'FUNCTION UTILITY
    Private Sub kosong()
        get_jam()

        'DGV_Data_TF.Columns(itemDgvHarga).DisplayIndex = 0
        'DGV_Data_TF.Columns(itemDgvHargaDisplay).DisplayIndex = 6
        'DGV_Data_TF.Columns(itemDgvBatch).DisplayIndex = 0

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

            Cmb_QualityFrom.Items.Clear() : Cmb_QualityTo.Items.Clear() : kategoriQuality.Clear()
            SQL = "select Kode_Warna from emi_master_warna where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    kategoriQuality.Add(Dr("Kode_Warna"))
                    If Dr("Kode_Warna") = "HIJAU" Then
                        Cmb_QualityFrom.Items.Add("Good Stock") : Cmb_QualityTo.Items.Add("Good Stock")
                    ElseIf Dr("Kode_Warna") = "KUNING" Then
                        Cmb_QualityFrom.Items.Add("Warning Stock") : Cmb_QualityTo.Items.Add("Warning Stock")
                    ElseIf Dr("Kode_Warna") = "MERAH" Then
                        Cmb_QualityFrom.Items.Add("Bad Stock") : Cmb_QualityTo.Items.Add("Bad Stock")
                    End If

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_DetBarang.Items.Clear()

        TxtTotalTransfer.Text = ""
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
        Cmb_QualityFrom.Enabled = True
        Cmb_QualityTo.Enabled = True

        Lv_DetBarang.Location = New Point(803, 258)
        Lv_DetBarang.Visible = False

        DGV_Data_TF.Rows.Clear()

    End Sub



    Private Sub DGV_Data_TF_MouseLeave(sender As Object, e As EventArgs) Handles DGV_Data_TF.MouseLeave
        If DGV_Data_TF.RowCount = 0 Then Exit Sub

        Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value

        If Not IsNumeric(cellValue) Then
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
        End If

        'PINDAH FOKUS
        DGV_Data_TF.CurrentCell = DGV_Data_TF.CurrentRow.Cells(itemDgvSatuan)
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

        TxtNo_Transaksi.Text = FTQ & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("Emi_Transfer_Quality", "No_Faktur", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(No_Faktur,1," & Len(FTQ) + Len(arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex)) + 6 & ")", FTQ & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

    End Sub


    'FUNCTION HANDLE
    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged, Txt_SO.TextChanged
        If CmbSO_Asal.Items.Count = 0 Or CmbSO_Asal.SelectedIndex = -1 Then Exit Sub
        If Cmb_QualityFrom.Items.Count = 0 Or Cmb_QualityFrom.SelectedIndex = -1 Then Exit Sub
        If Cmb_QualityTo.Items.Count = 0 Or Cmb_QualityTo.SelectedIndex = -1 Then Exit Sub

        If TxtKd_Barang.Text.Trim.Length = 0 Then
            TxtNm_Barang.Text = String.Empty
            TxtStock.Text = String.Empty
            TxtSatuan.Text = String.Empty
            TxtBags.Text = String.Empty
        End If

        If Not TxtKd_Barang.Text.Trim.Count = 0 Then
            Lv_DetBarang.Location = New Point(22, 256)
            Lv_DetBarang.Visible = True
        Else
            Lv_DetBarang.Location = New Point(803, 278)
            Lv_DetBarang.Visible = False
        End If

        Try
            OpenConn()

            Lv_DetBarang.Items.Clear()

            SQL = "select a.kode_stock_owner, a.kode_barang, a.nama, isnull((dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
            SQL = SQL & "b.satuan, a.Good_Stock)),0) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
            SQL = SQL & " a.Satuan_Isi_Bags from barang a, barang_detail_satuan b "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
            SQL = SQL & "and a.nama like '" & TxtKd_Barang.Text & "%' and a.Kode_Barang = b.kode_barang "
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

    Private Sub Lv_DetBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetBarang.DoubleClick

        If Lv_DetBarang.Items.Count = 0 Or TxtKd_Barang.Text.Trim = "" Then Exit Sub

        get_det_barang(Lv_DetBarang.FocusedItem.Index)

        TxtKd_Barang.Text = String.Empty
        Txt_SO.Text = String.Empty
        TxtNm_Barang.Text = String.Empty

        TxtKd_Barang.Text = lv_DetKodeBarang
        Txt_SO.Text = lv_DetKodeSO
        TxtNm_Barang.Text = lv_DetNamaBarang
        TxtSatuan.Text = lv_DetSatuanDIsplay
        TxtSatuanKecil.Text = lv_DetSatuan
        TxtStock.Text = Format(Val(lv_DetGoodStock), "N0")
        TxtBags.Text = Format(Val(lv_DetJmlhBags), "N0")
        'TxtSatuanBags.Text = lv_DetSatuanBags

        Lv_DetBarang.Location = New Point(803, 258)
        Lv_DetBarang.Visible = False

        Btn_GetData.Focus()
    End Sub

    Private Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_GetData.Click
        If TxtKd_Barang.Text.Trim.Length = 0 Or TxtKd_Barang.Text.Trim = "" Or TxtNm_Barang.Text.Trim.Length = 0 Then Exit Sub

        If TxtNm_Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Pilih Barang Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKd_Barang.Focus() : Exit Sub
        End If

        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("Isi SO Awal terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        If Cmb_QualityFrom.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Dim rows As Integer = 0
            DGV_Data_TF.Rows.Clear()

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

            Dim Warna_Awal As String = kategoriQuality(Cmb_QualityFrom.SelectedIndex)

            SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, dbo.get_hpp(a.Serial_Number) as Harga, b.Nama, a.Id_Warehouse, c.Keterangan as kode_rak, "
            SQL = SQL & " a.Id_Nametag_pallet, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, b.satuan, "
            SQL = SQL & "'" & TxtSatuan.Text & "', a.jumlah) as jumlah, b.satuan, a.nomor_pallet, ISNULL(a.Jumlah_Bags, 0) as stock_bags, a.Batch_number, "

            SQL = SQL & "dbo.ubah_satuan('" & KodePerusahaan & "', 'UANG','" & TxtKd_Barang.Text & "', '" & TxtSatuanKecil.Text & "', "
            SQL = SQL & "'" & TxtSatuan.Text & "', dbo.get_hpp(a.Serial_Number) ) as Harga_display "

            SQL = SQL & "from barang_sn a, barang b, View_Warehouse_Position c "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang and a.kode_stock_Owner=b.kode_stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.Id_Warehouse=c.Id_WMS_Warehouse_Position and a.jumlah <> 0 "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and b.Kode_Stock_Owner='" & Txt_SO.Text & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "' "
            SQL = SQL & "and a.Warna = '" & Warna_Awal & "' "
            SQL = SQL & "order by a.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim subArr As New List(Of String)

                    DGV_Data_TF.Rows.Add(1)
                    DGV_Data_TF.Rows(rows).Cells(itemDgvLokasi).Value = Dr("Kode_Stock_Owner")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvKodeBarang).Value = Dr("Kode_Barang")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvSerialNumber).Value = Dr("Serial_Number")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvNama).Value = Dr("Nama")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvIDWareHose).Value = Dr("Id_Warehouse")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvKodeRak).Value = Dr("kode_rak")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvIDPallet).Value = Dr("nomor_pallet")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvGoodStock).Value = Format(Dr("jumlah"), "N0")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvStockBags).Value = Format(Dr("stock_bags"), "N0")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvBatch).Value = Dr("Batch_number")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvHarga).Value = Dr("Harga")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvHargaDisplay).Value = Format(Dr("Harga_display"), "N0")

                    DGV_Data_TF.Rows(rows).Cells(itemDgvSatuan).Value = TxtSatuan.Text

                    DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
                    DGV_Data_TF.Rows(rows).Cells(itemDgvBags).ReadOnly = True

                    rows = rows + 1

                Loop
            End Using

            TxtTotalTransfer.Text = 0
            TxtTotalTransferBags.Text = 0

            CmbSO_Asal.Enabled = False
            Cmb_QualityFrom.Enabled = False
            Cmb_QualityTo.Enabled = False

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

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If DGV_Data_TF.RowCount = 0 Then
            MessageBox.Show("Belum ada barang yang mau di transfer!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKd_Barang.Focus() : Exit Sub
        ElseIf TxtKeterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKeterangan.Focus() : Exit Sub
        End If

        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("SO asal harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            Dim Tot_Stock_Kecil As Double = 0
            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKd_Barang.Text & "', '" & TxtSatuan.Text & "',"
            SQL = SQL & "'" & TxtSatuanKecil.Text & "', '" & HilangkanTanda(TxtTotalTransfer.Text) & "' ) as hasil"
            Using Dr1 = OpenTrans(SQL)
                If Dr1.Read Then
                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If

                    Tot_Stock_Kecil = Dr1("hasil")
                Else
                    Dr1.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data konversi satuan kirim tidak ada ")
                    Exit Sub
                End If
            End Using

            Dim isCheck As Boolean = False

            '=================================================
            '=       INSERT TABEL Emi_Transfer_Quality       =
            '=================================================
            SQL = "insert into Emi_Transfer_Quality (Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Quality_Awal, Quality_Tujuan, Kode_Barang, Tanggal, Jam, UserID, Total_Stock, Total_Bags, Satuan, keterangan, Total_Barang, Satuan_Barang) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & arrSO(CmbSO_Asal.SelectedIndex) & "', '" & kategoriQuality(Cmb_QualityFrom.SelectedIndex) & "', "
            SQL = SQL & "'" & kategoriQuality(Cmb_QualityTo.SelectedIndex) & "', '" & TxtKd_Barang.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
            SQL = SQL & "'" & TxtTotalTransfer.Text & "', '" & TxtTotalTransferBags.Text & "', '" & TxtSatuan.Text & "', '" & TxtKeterangan.Text & "', "
            SQL = SQL & "" & Tot_Stock_Kecil & ", '" & TxtSatuanKecil.Text & "')"
            ExecuteTrans(SQL)


            For row As Integer = 0 To DGV_Data_TF.RowCount - 1

                get_grid_view(row)

                'LAKUKAN CEK PADA DATAGRIDVIEW
                If dgv_CheckBox = False Then
                    Continue For
                End If

                'If dgv_Jumlah = "" Or dgv_JmlhBags = "" Then
                '    CloseTrans()
                '    CloseConn()
                '    MessageBox.Show("Jumlah dan Bags harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                isCheck = True

                Dim Warna_Awal As String = kategoriQuality(Cmb_QualityFrom.SelectedIndex)
                Dim Warna_Akhir As String = kategoriQuality(Cmb_QualityTo.SelectedIndex)



                '============================
                '=       UPDATE STOCK       =
                '============================

                'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)

                'SQL = "update barang_sn set jumlah = jumlah-'" & nilai_kecildetail & "', Jumlah_Bags = Jumlah_Bags-" & dgv_JmlhBags & " "
                'SQL = SQL & "where Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' and Kode_Barang='" & dgv_KodeBarang & "' "
                'SQL = SQL & "and Serial_Number='" & dgv_SerialNumber & "'"
                'ExecuteTrans(SQL)

                'SQL = "update barang set Good_Stock= Good_Stock-" & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags-" & dgv_JmlhBags & " "
                'SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
                'SQL = SQL & " and Kode_Barang='" & kd_barang & "'"
                'ExecuteTrans(SQL




                '============================
                '=       UPDATE WARNA       =
                '============================

                SQL = "update Barang_SN set Warna = '" & Warna_Akhir & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Stock_Owner = '" & dgv_Lokasi & "' and Kode_Barang = '" & dgv_KodeBarang & "' "
                SQL = SQL & "and Serial_Number = '" & dgv_SerialNumber & "'"
                ExecuteTrans(SQL)


#Region "KODE LAMA"

                '                'CEK APAKAH ADA BARANG SN BERDASARKAN 
                '                SQL = "select kode_stock_owner, kode_barang, serial_number, " & jenis_stockAwalSN & " as jumlah from barang_sn where "
                '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                SQL = SQL & "kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' and "
                '                SQL = SQL & "kode_barang = '" & dgv_KodeBarang & "' and serial_number='" & dgv_SerialNumber & "' "
                '                SQL = SQL & "order by " & SN_Tanggal("serial_number")
                '                Using Ds = BindingTrans(SQL)
                '                    With Ds.Tables("MyTable")
                '                        If .Rows.Count <> 0 Then
                '                            sisa = Math.Abs(nilai_kecildetail)

                '                            For h As Integer = 0 To .Rows.Count - 1
                '                                If sisa = 0 Then
                '                                    Exit For
                '                                ElseIf sisa < 0 Then
                '                                    CloseTrans()
                '                                    CloseConn()
                '                                    MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                                    Exit Sub
                '                                End If

                '                                If sisa < .Rows(h).Item("jumlah") Or sisa = .Rows(h).Item("jumlah") Then
                '                                    SQL = "Update barang_sn set " & jenis_stockAwalSN & " = " & jenis_stockAwalSN & " - " & sisa & ", Jumlah_Bags = Jumlah_Bags - " & dgv_JmlhBags & " where "
                '                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                                    SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                '                                    SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                '                                    SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                '                                    ExecuteTrans(SQL)

                '                                    SQL = "Update barang_sn set " & jenis_stockAkhirSN & " = " & jenis_stockAkhirSN & " + " & sisa & ", Jumlah_Bags = Jumlah_Bags + " & dgv_JmlhBags & " where "
                '                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                                    SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                '                                    SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                '                                    SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                '                                    ExecuteTrans(SQL)

                '                                    total_hpp = total_hpp + (sisa * Get_Harga_SN(.Rows(h).Item("serial_number")))

                '                                    sisa = 0
                '                                ElseIf sisa > .Rows(h).Item("jumlah") Then

                '                                    SQL = "Update barang_sn set " & jenis_stockAwalSN & " = " & jenis_stockAwalSN & " - " & jenis_stockAwalSN & ", Jumlah_Bags = Jumlah_Bags - " & dgv_JmlhBags & " where "
                '                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                                    SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                '                                    SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                '                                    SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                '                                    ExecuteTrans(SQL)

                '                                    SQL = "Update barang_sn set " & jenis_stockAkhirSN & " = " & jenis_stockAkhirSN & " + " & .Rows(h).Item("jumlah") & ", Jumlah_Bags = Jumlah_Bags + " & dgv_JmlhBags & " where "
                '                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                                    SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                '                                    sql = sql & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                '                                    SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                '                                    ExecuteTrans(SQL)

                '                                    total_hpp = total_hpp + (.Rows(h).Item("jumlah") * Get_Harga_SN(.Rows(h).Item("serial_number")))

                '                                    sisa = sisa - .Rows(h).Item("jumlah")
                '                                Else
                '                                    CloseTrans()
                '                                    CloseConn()
                '                                    MessageBox.Show("Barang SN terjadi kesalahan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                                    Exit Sub
                '                                End If

                '                                If sisa <> 0 And h = .Rows.Count - 1 Then
                '                                    CloseTrans()
                '                                    CloseConn()
                '                                    MessageBox.Show("Jumlah stock tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                                    Exit Sub
                '                                End If
                '                            Next
                '                        Else
                '                            CloseTrans()
                '                            CloseConn()
                '                            MessageBox.Show("SN untuk barang ini tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                            Exit Sub
                '                        End If
                '                    End With
                '                End Using


                'SQL = "Update barang set " & jenis_stockAwal & " = " & jenis_stockAwal & " - " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags - " & dgv_JmlhBags & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' and kode_Barang = '" & dgv_KodeBarang & "'"
                'ExecuteTrans(SQL)

                'SQL = "Update barang set " & jenis_stockAkhir & " = " & jenis_stockAkhir & " + " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags + " & dgv_JmlhBags & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' and kode_Barang = '" & dgv_KodeBarang & "'"
                'ExecuteTrans(SQL)

#End Region


                '=====================================================
                '=       INSERT TABEL Emi_Transfer_Quality_Det       =
                '=====================================================
                SQL = "insert into Emi_Transfer_Quality_Det (Kode_Perusahaan, No_Faktur, Serial_Number, Batch_Number, Id_Warehouse, id_Pallet, Jumlah_Stock, Jumlah_Bags, Warna) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text & "', '" & dgv_SerialNumber & "', "
                SQL = SQL & "'" & dgv_Batch & "', '" & dgv_IDWareHouse & "', '" & dgv_IDPallet & "', '" & dgv_Jumlah & "', '" & dgv_JmlhBags & "', '" & Warna_Akhir & "')"
                ExecuteTrans(SQL)

            Next

            If isCheck <> True Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Tidak Ada Data yang Dikirim . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            'Cek STOCK BARANG
            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
            SQL = SQL & "AND a.Kode_Barang = '" & kd_barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
                            Dim a As Double = .Rows(0).Item("good_stock")
                            Dim b As Double = .Rows(0).Item("Jumlah_sn")
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CmbSO_Asal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSO_Asal.SelectedIndexChanged
        DGV_Data_TF.Rows.Clear()
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

    Private Sub DGV_Data_TF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellEndEdit

        Dim totalJmlh As Double = 0
        Dim totalBags As Double = 0

        For i As Integer = 0 To DGV_Data_TF.RowCount - 1
            If DGV_Data_TF.Rows(i).Cells(itemDgvCheckBox).Value = "true" Then
                totalJmlh = totalJmlh + Val(isNull(DGV_Data_TF.Rows(i).Cells(itemDgvGoodStock).Value))
                totalBags = totalBags + Val(isNull(DGV_Data_TF.Rows(i).Cells(itemDgvStockBags).Value))
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

    Private Sub Cmb_QualityFrom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_QualityFrom.SelectedIndexChanged
        If Cmb_QualityFrom.Items.Count = 0 Then Exit Sub
        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Lokasi Dahulu", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_QualityFrom.SelectedIndex = -1
            Exit Sub
        End If
    End Sub

    Private Sub Cmb_QualityTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_QualityTo.SelectedIndexChanged

        If Cmb_QualityTo.Items.Count = 0 Or Cmb_QualityTo.SelectedIndex = -1 Then Exit Sub


        If Cmb_QualityFrom.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Stock Awal Dahulu", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_QualityTo.SelectedIndex = -1
            Exit Sub
        End If

        If Cmb_QualityTo.SelectedIndex = Cmb_QualityFrom.SelectedIndex Then
            MessageBox.Show("Stock Tujuan Tidak Boleh Sama Dengan Stock Awal", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_QualityTo.SelectedIndex = -1
            Exit Sub
        End If

    End Sub

    Private Function Generate_Random_Kode(ByVal length As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim result As New StringBuilder()

        For i As Integer = 1 To length
            Dim index As Integer = Random.Next(0, chars.Length)
            result.Append(chars(index))
        Next

        Return result.ToString()
    End Function

    Private Function Get_Rak_Kosong() As (String, String)

        Dim available_Id_Warehouse As String = ""
        Dim available_NoPallet As String = ""

        SQL = "select top(1) id_wms_warehouse_position, nomor_urut from view_warehouse_position_detail where kode_barang is null "
        Using Dr2 = OpenTrans(SQL)
            Do While Dr2.Read
                available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                available_NoPallet = Dr2("nomor_urut")
            Loop
        End Using

        Return (available_Id_Warehouse, available_NoPallet)
    End Function

End Class