Imports System.Data.SqlClient
Imports System.Text
Imports Newtonsoft.Json

Public Class Transfer_Stock_3
    Public asal As String
    Dim arrSO, arrInisialFaktur, arrIdWMSWarehouse, WarehosePosition As New ArrayList
    Private random As New Random()

    Dim arr2RakTujuan As New List(Of List(Of String))

    Dim lv_DetKodeSO, lv_DetKodeBarang, lv_DetNamaBarang, lv_DetGoodStock, lv_DetSatuan, lv_DetSatuanDIsplay, lv_DetJmlhBags, lv_DetSatuanBags As String

    Dim dgv_Lokasi, dgv_KodeBarang, dgv_SerialNumber, dgv_Nama, dgv_IDWareHouse, dgv_KodeRak As String
    Dim dgv_IDPallet, dgv_GoodStock, dgv_Satuan, dgv_Jumlah, dgv_RakTujuan, dgv_IDWarehouseTujuan, dgv_JmlhBags, dgv_Warna, dgv_JenisKemasan As String
    Dim dgv_IsiPerBags, dgv_SatuanIsiBags As String
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
    Dim itemDgvKodeRak As Integer = 5
    Dim itemDgvIDPallet As Integer = 6
    Dim itemDgvGoodStock As Integer = 7
    Dim itemDgvSatuan As Integer = 8
    Dim itemDgvStockBags As Integer = 9
    Dim itemDgvCheckBox As Integer = 10
    Dim itemDgvJumlah As Integer = 11
    Dim itemDgvBags As Integer = 12
    Dim itemDgvRakTujuan As Integer = 13
    Dim itemIDWarehouseTujuan As Integer = 14
    Dim itemDgvWarna As Integer = 15
    Dim itemJenisKemasan As Integer = 16
    Dim itemDGVIsiPerBags As Integer = 17
    Dim itemDGVSatuanIsiBags As Integer = 18


    'Dim itemDgvIDWareHouseTujuan As Integer = 10

    Private Sub Transfer_Stock_3_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CmbJnsTransfer.SelectedIndex = 0 Then
            If CmbSO_Asal.Items.Count = 0 Or CmbSO_Asal.SelectedIndex = -1 Then Exit Sub
            If Not CmbSO_Asal.SelectedItem.ToString = "PRODUCTION" Then
                MessageBox.Show("List Request Hanya Tersedia Untuk Lokasi Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Else
            If CmbSo_Tujuan.Items.Count = 0 Or CmbSo_Tujuan.SelectedIndex = -1 Then Exit Sub
            If Not CmbSo_Tujuan.SelectedItem.ToString = "PRODUCTION" Then
                MessageBox.Show("List Request Hanya Tersedia Untuk Lokasi Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If


        Try
            OpenConn()
            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Emi_Display_Request_Material.lokasi_kirim = CmbSO_Asal.Text
        Emi_Display_Request_Material.ShowDialog()
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
        dgv_RakTujuan = DGV_Data_TF.Rows(index).Cells(itemDgvRakTujuan).Value
        dgv_JmlhBags = DGV_Data_TF.Rows(index).Cells(itemDgvBags).Value
        dgv_Warna = DGV_Data_TF.Rows(index).Cells(itemDgvWarna).Value
        dgv_JenisKemasan = DGV_Data_TF.Rows(index).Cells(itemJenisKemasan).Value
        dgv_IsiPerBags = DGV_Data_TF.Rows(index).Cells(itemDGVIsiPerBags).Value
        dgv_SatuanIsiBags = DGV_Data_TF.Rows(index).Cells(itemDGVSatuanIsiBags).Value

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
    Public Sub kosong()
        get_jam()

        Try
            OpenConn()
            'get_no_faktur()
            TxtKd_Barang.Enabled = True
            Btn_GetData.Enabled = True
            asal = "Transfer_Stock_3"

            DGV_Data_TF.Columns(itemDgvWarna).DisplayIndex = 6

            Cmb_Warna.Items.Clear() : Cmb_Warna.SelectedIndex = -1
            Cmb_Warna.Items.Add("MERAH") : Cmb_Warna.Items.Add("KUNING") : Cmb_Warna.Items.Add("HIJAU")
            Cmb_Warna.SelectedIndex = 2

            CmbSo_Tujuan.Items.Clear() : CmbSo_Tujuan.SelectedIndex = -1
            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSo_Tujuan.Items.Add(dr("Keterangan")) : arrSO.Add(dr("kode_stock_owner"))
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

        CmbJnsTransfer.Items.Clear()
        CmbJnsTransfer.Items.Add("Antar Rak")
        CmbJnsTransfer.Items.Add("Antar Gudang")

        Txt_OtoMaterial_req.Text = ""
        Txt_JumlahPermintaan.Text = ""
        Txt_SatuanPermintaan.Text = ""
        TxtSatuanKecil.Text = ""
        Txt_Warna.Text = ""
        TxtSatuan.Text = ""
        TxtStock.Text = ""
        TxtKeterangan.Text = String.Empty
        TxtKd_Barang.Text = String.Empty
        Txt_SO.Text = String.Empty
        TxtNm_Barang.Text = String.Empty
        TxtBags.Text = String.Empty
        'TxtSatuanBags.Text = String.Empty
        TxtTotalTransferBags.Text = String.Empty

        CmbJnsTransfer.Enabled = True
        CmbSO_Asal.Enabled = False
        CmbSo_Tujuan.Enabled = False

        DGV_Data_TF.Rows.Clear()
    End Sub

    Private Sub DGV_Data_TF_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellClick
        If e.ColumnIndex = rak_tujuan.Index Then
            If Not DGV_Data_TF.CurrentCell.Value = "" Then
                DGV_Data_TF.CurrentCell = DGV_Data_TF.Rows(e.RowIndex).Cells(e.ColumnIndex)
                DGV_Data_TF.BeginEdit(True)
            End If
        End If
    End Sub

    Private Sub DGV_Data_TF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellEndEdit
        If DGV_Data_TF.Rows.Count = 0 Then Exit Sub

        If DGV_Data_TF.CurrentRow.Cells(itemDgvCheckBox).Value = "True" Then

            Dim currentColumn As Integer = DGV_Data_TF.CurrentCell.ColumnIndex
            Dim cellValue As Object = DGV_Data_TF.CurrentRow.Cells(currentColumn).Value

            If currentColumn = itemDgvBags OrElse currentColumn = itemDgvJumlah Then
                If Not IsNumeric(cellValue) Then
                    DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
                    DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
                    Exit Sub
                End If
            End If


            If DGV_Data_TF.CurrentRow.Cells(itemJenisKemasan).Value.ToString.ToUpper = "ORIGINAL BAGS" Then
                DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = True

                If Not DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = "" Then
                    Dim isiPerbags As Double = Val(DGV_Data_TF.CurrentRow.Cells(itemDGVIsiPerBags).Value)
                    Dim jumlahInputBags As Double = Val(DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value)

                    Dim valueJumlah As Double = isiPerbags * jumlahInputBags
                    DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = valueJumlah
                End If

            Else
                DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = False
            End If

            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).ReadOnly = False
            DGV_Data_TF.CurrentRow.Cells(itemDgvRakTujuan).ReadOnly = False

        Else
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
            DGV_Data_TF.CurrentRow.Cells(itemDgvRakTujuan).Value = ""

            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = True
            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).ReadOnly = True
            DGV_Data_TF.CurrentRow.Cells(itemDgvRakTujuan).ReadOnly = True
        End If

        Dim currentColumnIndex As Integer = DGV_Data_TF.CurrentCell.ColumnIndex

        If currentColumnIndex = itemDgvBags OrElse currentColumnIndex = itemDgvJumlah Then
            Dim jumlahValue As Object = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value
            If jumlahValue IsNot Nothing AndAlso IsNumeric(jumlahValue) Then
                DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = Convert.ToDecimal(jumlahValue).ToString("N2")
            Else
                DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
            End If

            Dim bagsValue As Object = DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value
            If bagsValue IsNot Nothing AndAlso IsNumeric(bagsValue) Then
                DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = Convert.ToDecimal(bagsValue).ToString("N2")
            Else
                DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
            End If
        End If

        GetGrandTotal()

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
        Dim FPro_Results As String = "TS-"
        TxtNo_Transaksi.Text = FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("Tf_Stock", "kode_transfer", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(kode_transfer,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

    End Sub

    Private Sub DGV_Data_TF_Leave(sender As Object, e As EventArgs) Handles DGV_Data_TF.Leave

    End Sub

    'FUNCTION HANDLE
    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged, Txt_SO.TextChanged, Txt_SatuanPermintaan.TextChanged, Txt_JumlahPermintaan.TextChanged, Txt_OtoMaterial_req.TextChanged
        If asal <> "Emi_Display_Request_Material" Then
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
                get_no_faktur()

                Lv_DetBarang.Items.Clear()

                SQL = "select a.kode_stock_owner, a.kode_barang, a.nama, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
                SQL = SQL & "b.satuan, a.good_stock) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
                SQL = SQL & "a.Satuan_Isi_Bags from barang a, barang_detail_satuan b "
                SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
                SQL = SQL & "and a.nama like '" & TxtKd_Barang.Text & "%' and a.Kode_Barang=b.kode_barang "
                SQL = SQL & "And a.kode_Perusahaan = b.kode_Perusahaan And b.flag_tampil_display ='Y'  "
                SQL = SQL & "order by a.Kode_Barang "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As New ListViewItem
                        Lv = Lv_DetBarang.Items.Add(Dr("kode_stock_owner"))
                        Lv.SubItems.Add(Dr("kode_barang"))
                        Lv.SubItems.Add(Dr("nama"))
                        Lv.SubItems.Add(Format(Dr("Good_Stock"), "N2"))
                        Lv.SubItems.Add(Dr("Satuan"))
                        Lv.SubItems.Add(Dr("satuan_display"))
                        Lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N0"))
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("Satuan_Isi_Bags")))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

    End Sub

    Private Sub Lv_DetBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetBarang.DoubleClick

        If Lv_DetBarang.Items.Count = 0 Or TxtKd_Barang.Text.Trim = "" Then Exit Sub

        CmbJnsTransfer.Enabled = False
        CmbSO_Asal.Enabled = False
        CmbSo_Tujuan.Enabled = False

        get_det_barang(Lv_DetBarang.FocusedItem.Index)

        TxtKd_Barang.Text = String.Empty
        Txt_SO.Text = String.Empty
        TxtNm_Barang.Text = String.Empty

        TxtKd_Barang.Text = lv_DetKodeBarang
        Txt_SO.Text = lv_DetKodeSO
        TxtNm_Barang.Text = lv_DetNamaBarang
        TxtSatuan.Text = lv_DetSatuanDIsplay
        TxtSatuanKecil.Text = lv_DetSatuan
        TxtStock.Text = lv_DetGoodStock
        TxtBags.Text = lv_DetJmlhBags
        'TxtSatuanBags.Text = lv_DetSatuanBags

        Lv_DetBarang.Location = New Point(803, 258)
        Lv_DetBarang.Visible = False

        Btn_GetData.Focus()
    End Sub

    Private Sub CmbJnsTransfer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbJnsTransfer.SelectedIndexChanged

        If CmbJnsTransfer.Items.Count = 0 Then Exit Sub

        If CmbJnsTransfer.SelectedIndex = 0 Then
            CmbSO_Asal.Enabled = True
            CmbSo_Tujuan.SelectedIndex = -1
            CmbSo_Tujuan.Enabled = False

        ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
            CmbSO_Asal.Enabled = True
            CmbSo_Tujuan.Enabled = True
        End If

        DGV_Data_TF.Rows.Clear()
        TxtTotalTransfer.Text = String.Empty

    End Sub

    Public Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_GetData.Click

        If CmbJnsTransfer.SelectedIndex = 0 Then
            If CmbSO_Asal.SelectedIndex = -1 Then
                MessageBox.Show("Isi SO Awal terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSO_Asal.Focus() : Exit Sub
            End If
        ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
            If CmbSo_Tujuan.SelectedIndex = -1 Then
                MessageBox.Show("Isi SO Awal terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSO_Asal.Focus() : Exit Sub
            End If
        End If

        If TxtKd_Barang.Text.Trim.Length = 0 Or TxtKd_Barang.Text.Trim = "" Or TxtNm_Barang.Text.Trim.Length = 0 Then Exit Sub

        If Cmb_Warna.SelectedIndex = -1 Then
            MessageBox.Show("Isi Warna terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Warna.Focus() : Exit Sub
        End If

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
            SQL = SQL & " And b.kode_Barang Is null "

            If CmbJnsTransfer.SelectedIndex = 0 Then
                SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
            ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
                SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(CmbSo_Tujuan.SelectedIndex) & "' "
            End If

            SQL = SQL & "group by a.Id_WMS_Warehouse_Position, a.Keterangan "
            SQL = SQL & "order by a.Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    WarehosePosition.Add(Dr("Keterangan")) : arrIdWMSWarehouse.Add(Dr("Id_WMS_Warehouse_Position"))
                Loop
            End Using

            SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, b.Nama, a.Id_Warehouse, c.Keterangan as kode_rak, "
            SQL = SQL & " a.Id_Nametag_pallet, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, b.satuan, "
            SQL = SQL & "'" & TxtSatuan.Text & "', a.jumlah) as jumlah, b.satuan, a.nomor_pallet, ISNULL(a.Jumlah_Bags, 0) as stock_bags, a.warna, b.Jenis_Kemasan, "
            SQL = SQL & "b.Isi_Per_Bags, b.Satuan_Isi_Bags "
            SQL = SQL & "from barang_sn a, barang b, View_Warehouse_Position c "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang and a.Kode_Stock_Owner=b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.Id_Warehouse=c.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and b.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "' "
            SQL = SQL & "and a.warna = '" & Cmb_Warna.SelectedItem.ToString & "' "
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
                    DGV_Data_TF.Rows(rows).Cells(itemDgvGoodStock).Value = Format(Dr("jumlah"), "N2")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvStockBags).Value = Format(Dr("stock_bags"), "N0")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvWarna).Value = Dr("warna")
                    DGV_Data_TF.Rows(rows).Cells(itemJenisKemasan).Value = Dr("Jenis_Kemasan")
                    DGV_Data_TF.Rows(rows).Cells(itemDGVIsiPerBags).Value = Dr("Isi_Per_Bags")
                    DGV_Data_TF.Rows(rows).Cells(itemDGVSatuanIsiBags).Value = Dr("Satuan_Isi_Bags")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvSatuan).Value = TxtSatuan.Text

                    Dim dgvCmbValueRak As DataGridViewComboBoxCell
                    dgvCmbValueRak = DGV_Data_TF.Rows(rows).Cells(itemDgvRakTujuan)
                    dgvCmbValueRak.Items.Clear()

                    'dgvCmbValueRak.Items.Add("-- Tidak Berubah --") : subArr.Add(Dr("Id_Warehouse"))
                    For i As Integer = 0 To WarehosePosition.Count - 1
                        dgvCmbValueRak.Items.Add(WarehosePosition(i)) : subArr.Add(arrIdWMSWarehouse(i))
                    Next

                    arr2RakTujuan.Add(subArr)

                    DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
                    DGV_Data_TF.Rows(rows).Cells(itemDgvBags).ReadOnly = True
                    DGV_Data_TF.Rows(rows).Cells(itemDgvRakTujuan).ReadOnly = True

                    If Dr("Jenis_Kemasan").ToString.ToUpper = "ORIGINAL BAGS" Then
                        DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
                    End If

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
        ElseIf CmbSO_Asal.Text = CmbSo_Tujuan.Text Then
            MessageBox.Show("SO asal dan so tujuan sama", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        If CmbJnsTransfer.SelectedIndex = 0 Then
            If CmbSO_Asal.SelectedIndex = -1 Then
                MessageBox.Show("SO asal harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSO_Asal.Focus() : Exit Sub
            End If
        Else
            If CmbSo_Tujuan.SelectedIndex = -1 Then
                MessageBox.Show("SO tujuan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSo_Tujuan.Focus() : Exit Sub
            End If

        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            Dim nilai_kecil As Double = 0
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

                    nilai_kecil = Dr1("hasil")
                Else
                    Dr1.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data konversi satuan kirim tidak ada ")
                    Exit Sub
                End If
            End Using

            Dim lokasi_tujuan As String = ""

            If Not CmbSo_Tujuan.SelectedIndex = -1 Then
                lokasi_tujuan = arrSO(CmbSo_Tujuan.SelectedIndex)
            Else

                lokasi_tujuan = arrSO(CmbSO_Asal.SelectedIndex)
            End If

            SQL = "insert into Tf_Stock (Kode_Perusahaan, Kode_Transfer, SO_Awal, SO_Tujuan ,Kode_Barang, "
            SQL = SQL & "Tanggal, Jam, Jenis_Transfer, Total, Satuan, Keterangan, Total_Barang, Satuan_Barang, userid, Total_Transfer_Bags) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & arrSO(CmbSO_Asal.SelectedIndex) & "', "
            SQL = SQL & "'" & lokasi_tujuan & "', "
            SQL = SQL & "'" & kd_barang & "', '" & tgl_skg & "', '" & tgl_skg.ToString("HH:mm:ss") & "', '" & CmbJnsTransfer.SelectedItem & "', "
            SQL = SQL & "'" & HilangkanTanda(TxtTotalTransfer.Text.ToString) & "', '" & TxtSatuan.Text & "', '" & TxtKeterangan.Text.ToString & "', "
            SQL = SQL & " '" & nilai_kecil & "', '" & TxtSatuanKecil.Text.ToString & "', '" & UserID & "', '" & HilangkanTanda(TxtTotalTransferBags.Text.ToString) & "')"
            ExecuteTrans(SQL)

            Dim Jenis_Berat As String = ""
            SQL = "Select isnull(flag_tampil_berat,'T') as flag_tampil_berat from emi_satuan where "
            SQL = SQL & "satuan='" & TxtSatuan.Text & "' and kode_perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Jenis_Berat = dr("flag_tampil_berat")
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data Satuan Tidak ada . . ! ! ")
                    Exit Sub
                End If
            End Using

            Dim isCheck As Boolean = False
            Dim nilai_persediaan_min As Double = 0

#Region "Potong Stock dan Jurnal"

            For row As Integer = 0 To DGV_Data_TF.RowCount - 1

                get_grid_view(row)

                If dgv_CheckBox = False Then
                    Continue For
                End If

                If dgv_Jumlah = "" Or dgv_JmlhBags = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf CType(DGV_Data_TF.Rows(row).Cells(itemDgvRakTujuan), DataGridViewComboBoxCell).Value = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Rak Tujuan harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                isCheck = True

                Dim comboBoxCell As DataGridViewComboBoxCell = CType(DGV_Data_TF.Rows(row).Cells(itemDgvRakTujuan), DataGridViewComboBoxCell)
                Dim selectedIndex As Integer = comboBoxCell.Items.IndexOf(comboBoxCell.Value)

                Dim nilai_kecildetail As Double = 0
                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKd_Barang.Text & "', '" & TxtSatuan.Text & "',"
                SQL = SQL & "'" & TxtSatuanKecil.Text & "', '" & HilangkanTanda(dgv_Jumlah.ToString) & "' ) as hasil"
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If

                        nilai_kecildetail = Dr1("hasil")
                    Else
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using

                Dim palletTujuan As Double = 0
                SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
                SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
                SQL = SQL & "id_wms_warehouse_position = '" & arr2RakTujuan(row)(selectedIndex) & "' "
                SQL = SQL & "order by nomor_urut "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        palletTujuan = dr("nomor_urut")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                        Exit Sub
                    End If
                End Using




                Dim flag_pot_stock As String = "NULL"
                Dim jumlah_pot_stock As String = "0"
                Dim sn As String = "NULL"

                If Jenis_Berat = "T" Then
                    flag_pot_stock = "'T'"
                    jumlah_pot_stock = nilai_kecildetail

                    SQL = "update barang_sn set jumlah = jumlah-'" & nilai_kecildetail & "', Jumlah_Bags = Jumlah_Bags-" & dgv_JmlhBags & " "
                    SQL = SQL & "where Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' and Kode_Barang='" & dgv_KodeBarang & "' "
                    SQL = SQL & "and Serial_Number='" & dgv_SerialNumber & "'"
                    ExecuteTrans(SQL)

                    SQL = "update barang set Good_Stock= Good_Stock-" & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags-" & dgv_JmlhBags & " "
                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
                    SQL = SQL & " and Kode_Barang='" & kd_barang & "'"
                    ExecuteTrans(SQL)

                    Dim nilai_Per_Row As Double = 0
                    SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
                    SQL = SQL & "Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' and Kode_Barang='" & dgv_KodeBarang & "' "
                    SQL = SQL & "and Serial_Number='" & dgv_SerialNumber & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            nilai_Per_Row = dr("rp_persediaan_min")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    nilai_persediaan_min += nilai_Per_Row

                    Dim hargaIsn As String = ""
                    Dim QrLama As String = ""
                    Dim batchLama As String = ""
                    Dim namaBarang As String = ""
                    Dim expDate As String = ""

                    'Ambil Data Lama
                    SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired "
                    SQL = SQL & "from barang_sn a, barang b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                    SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
                    SQL = SQL & "and a.Kode_Barang ='" & dgv_KodeBarang & "' "
                    SQL = SQL & "and a.Serial_Number='" & dgv_SerialNumber & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                            QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                            batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                            namaBarang = General_Class.CekNULL(Dr("Nama"))
                            expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data Tidak ada")
                            Exit Sub
                        End If
                    End Using

                    'GENERATE SN BARU
                    Dim Random As New Random()
                    Dim str As String = Format(Random.Next(0, 999), "000") & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HHmmss")
                    Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                    Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(DateTime.Now, "yyyy-MM-dd")

                    Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)
                    sn = "'" & SN_Baru & "'"

                    'INSERT BARANG SN BARU
                    SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, "
                    SQL = SQL & "Jumlah, Jumlah_Bags, Tgl_Expired, Tgl_Produksi, "
                    SQL = SQL & "Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, "
                    SQL = SQL & "Kode_Unik_Asal, Nomor_Pallet, batch_number, warna) "
                    SQL = SQL & "select Kode_Perusahaan, '" & lokasi_tujuan & "', Kode_Barang, '" & SN_Baru & "', "
                    'SQL = SQL & "'" & nilai_kecildetail & "', Warning_Stock, Bad_Stock, " & dgv_JmlhBags & ", "
                    SQL = SQL & "'" & nilai_kecildetail & "', " & dgv_JmlhBags & ", "
                    SQL = SQL & "Tgl_Expired, Tgl_Produksi, '" & arr2RakTujuan(row)(selectedIndex) & "', "
                    SQL = SQL & "id_Susunan, Qr_Code, '" & newKodeUnikBerjalan & "', Kode_Unik_Asal, '" & palletTujuan & "', batch_number, '" & dgv_Warna & "' "
                    SQL = SQL & "from Barang_SN "
                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
                    SQL = SQL & "and Kode_Barang='" & dgv_KodeBarang & "' "
                    SQL = SQL & "and Serial_Number='" & dgv_SerialNumber & "' "
                    ExecuteTrans(SQL)

                    '============================
                    '=       TAMBAH STOCK       =
                    '============================

                    SQL = "update barang set Good_Stock= Good_Stock + " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags + " & dgv_JmlhBags & " "
                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & lokasi_tujuan & "' "
                    SQL = SQL & " and Kode_Barang='" & dgv_KodeBarang & "'"
                    ExecuteTrans(SQL)

                    'CEK KESESUAIAN STOCK
                    SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                    SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                    SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                    SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                    SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                    SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                    SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & lokasi_tujuan & "' "
                    SQL = SQL & "AND a.Kode_Barang = '" & dgv_KodeBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                    SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
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
                        End With
                    End Using


                End If

                SQL = "insert into Tf_Stock_det (Kode_Perusahaan, No_Faktur, Serial_Number_Awal, Serial_Number_Akhir, "
                SQL = SQL & "Id_Wms_Awal, Id_Wms_Tujuan, Jumlah, Jumlah_Bags, Satuan ,No_Pallet_Awal, No_Pallet_Tujuan, "
                SQL = SQL & "Flag_Pot_Stock, jumlah_pot_Stock, warna) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', "
                SQL = SQL & "'" & dgv_SerialNumber & "', " & sn & ", " & dgv_IDWareHouse & ", " & arr2RakTujuan(row)(selectedIndex) & ", '" & nilai_kecildetail & "', " & dgv_JmlhBags & ", "
                SQL = SQL & "'" & TxtSatuanKecil.Text & "','" & dgv_IDPallet & "', " & palletTujuan & ", "
                SQL = SQL & "" & flag_pot_stock & "," & jumlah_pot_stock & ", "
                SQL = SQL & "'" & dgv_Warna & "')"
                ExecuteTrans(SQL)

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
                'ExecuteTrans(SQL)

            Next

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

            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner =  "
            SQL = SQL & "'" & lokasi_tujuan & "' "
            SQL = SQL & "AND a.Kode_Barang = '" & kd_barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
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

            'dari
            'JURNAL
            If Jenis_Berat = "T" Then
                Dim fRaw_Material_dari As String = ""
                Dim fFinished_Good_dari As String = ""
                Dim fSemi_FG_dari As String = ""
                Dim fScrap_dari As String = ""
                Dim fPackaging_dari As String = ""
                Dim akun_persediaan_dari As String = ""

                Dim fRaw_Material_tujuan As String = ""
                Dim fFinished_Good_tujuan As String = ""
                Dim fSemi_FG_tujuan As String = ""
                Dim fScrap_tujuan As String = ""
                Dim akun_persediaan_tujuan As String = ""
                Dim fPackaging_tujuan As String = ""
                Dim inisial_faktur_dari As String = ""

                SQL = "select a.Flag_Raw_Material,a.Flag_Finished_Good,a.Flag_Semi_FG,a.Flag_Scrap, a.Flag_Packaging "
                SQL = SQL & "from Barang b,EMI_Group_Jenis a where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        fRaw_Material_dari = Dr("Flag_Raw_Material")
                        fFinished_Good_dari = Dr("Flag_Finished_Good")
                        fSemi_FG_dari = Dr("Flag_Semi_FG")
                        fScrap_dari = Dr("Flag_Scrap")
                        fPackaging_dari = Dr("Flag_Packaging")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        'akun_persediaan_dari = Dr("persediaan")
                        inisial_faktur_dari = Dr("inisial_faktur")
                        If fRaw_Material_dari = "Y" Then
                            akun_persediaan_dari = Dr("Persediaan_Bahan_Baku")
                        ElseIf fFinished_Good_dari = "Y" Then
                            akun_persediaan_dari = Dr("Persediaan")
                        ElseIf fSemi_FG_dari = "Y" Then
                            akun_persediaan_dari = Dr("Persediaan_Bahan_Setengah_Jadi")
                        ElseIf fScrap_dari = "Y" Then
                            akun_persediaan_dari = Dr("Persediaan_Scrap")
                        ElseIf fPackaging_dari = "Y" Then
                            akun_persediaan_dari = Dr("Persediaan_Packaging")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select a.Flag_Raw_Material,a.Flag_Finished_Good,a.Flag_Semi_FG,a.Flag_Scrap, a.Flag_Packaging "
                SQL = SQL & "from Barang b,EMI_Group_Jenis a where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.kode_stock_owner = '" & lokasi_tujuan & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        fRaw_Material_tujuan = Dr("Flag_Raw_Material")
                        fFinished_Good_tujuan = Dr("Flag_Finished_Good")
                        fSemi_FG_tujuan = Dr("Flag_Semi_FG")
                        fScrap_tujuan = Dr("Flag_Scrap")
                        fPackaging_tujuan = Dr("Flag_Packaging")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lokasi_tujuan & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        'akun_persediaan_dari = Dr("persediaan")
                        If fRaw_Material_tujuan = "Y" Then
                            akun_persediaan_tujuan = Dr("Persediaan_Bahan_Baku")
                        ElseIf fFinished_Good_tujuan = "Y" Then
                            akun_persediaan_tujuan = Dr("Persediaan")
                        ElseIf fSemi_FG_tujuan = "Y" Then
                            akun_persediaan_tujuan = Dr("Persediaan_Bahan_Setengah_Jadi")
                        ElseIf fScrap_tujuan = "Y" Then
                            akun_persediaan_tujuan = Dr("Persediaan_Scrap")
                        ElseIf fPackaging_tujuan = "Y" Then
                            akun_persediaan_tujuan = Dr("Persediaan_Packaging")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                Dim Kode_voucher As String = ""
                Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                Dim pagenumber As Integer = 1

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_voucher & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & Trim(TxtNo_Transaksi.Text) & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                          Strings.Mid(akun_persediaan_dari, 2, 1),
                          Strings.Mid(Ganti(akun_persediaan_dari), 3),
                          KodePerusahaan, KodeProyek, "Persedian " & Trim(TxtNo_Transaksi.Text), "0", nilai_persediaan_min, pagenumber, "TSSS")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                         Strings.Mid(akun_persediaan_tujuan, 2, 1),
                         Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                         KodePerusahaan, KodeProyek, "Persedian " & Trim(TxtNo_Transaksi.Text), nilai_persediaan_min, "0", pagenumber, "TSSS")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("debit") <> Dr("kredit") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using



                SQL = "update Tf_Stock set kode_voucher = '" & Kode_voucher & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Transfer = '" & Trim(TxtNo_Transaksi.Text) & "' "
                ExecuteTrans(SQL)
            End If


#End Region

            '==============================
            '=     UPDATE FLAG TAMPIL     =
            ''==============================
            'SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = 'Y' where Urut_Oto = '" & Txt_OtoMaterial_req.Text & "' "
            'ExecuteTrans(SQL)



            If isCheck <> True Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Tidak Ada Data yang Dikirim . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

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

        If CmbSO_Asal.SelectedIndex = -1 Then Exit Sub

        If CmbJnsTransfer.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Jenis Transfer Dahulu", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.SelectedIndex = -1
            Exit Sub
        End If

        DGV_Data_TF.Rows.Clear()
        TxtKd_Barang.Text = ""
        Txt_SO.Text = ""
        TxtNm_Barang.Text = ""
        CmbSo_Tujuan.SelectedIndex = -1
    End Sub

    Private Sub CmbSo_Tujuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSo_Tujuan.SelectedIndexChanged

        If CmbSo_Tujuan.SelectedIndex = -1 Then Exit Sub

        If arrSO(CmbSo_Tujuan.SelectedIndex) = arrSO(CmbSO_Asal.SelectedIndex) Then
            MessageBox.Show("Lokasi Tujuan Tidak Boleh Sama Dengan Lokasi Awal", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSo_Tujuan.SelectedIndex = -1
            Exit Sub
        End If

        DGV_Data_TF.Rows.Clear()
        TxtKd_Barang.Text = ""
        Txt_SO.Text = ""
        TxtNm_Barang.Text = ""
    End Sub

    Private Sub GetGrandTotal()
        If DGV_Data_TF.Rows.Count = 0 Then Exit Sub

        Dim total As Double = 0
        Dim totalBags As Double = 0

        For i As Integer = 0 To DGV_Data_TF.Rows.Count - 1

            If DGV_Data_TF.Rows(i).Cells(itemDgvCheckBox).Value = "False" Then
                Continue For
            End If

            total = total + Val(DGV_Data_TF.Rows(i).Cells(itemDgvJumlah).Value)
            totalBags = totalBags + Val(DGV_Data_TF.Rows(i).Cells(itemDgvBags).Value)

        Next

        TxtTotalTransfer.Text = total
        TxtTotalTransferBags.Text = totalBags

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

    Private Function Generate_Random_Kode(ByVal length As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim result As New StringBuilder()

        For i As Integer = 1 To length
            Dim index As Integer = random.Next(0, chars.Length)
            result.Append(chars(index))
        Next

        Return result.ToString()
    End Function



End Class