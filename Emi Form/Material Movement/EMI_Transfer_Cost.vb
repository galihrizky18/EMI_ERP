Imports System.Data.SqlClient
Imports Newtonsoft.Json

Public Class EMI_Transfer_Cost

    Dim arrSO, arrInisialFaktur, arrIdWMSWarehouse, WarehosePosition As New ArrayList

    Dim arr2RakTujuan As New List(Of List(Of String))

    Dim lv_DetKodeSO, lv_DetKodeBarang, lv_DetNamaBarang, lv_DetGoodStock, lv_DetSatuan, lv_DetSatuanDIsplay, lv_DetJmlhBags, lv_DetSatuanBags As String

    Dim dgv_Lokasi, dgv_KodeBarang, dgv_SerialNumber, dgv_Nama, dgv_IDWareHouse, dgv_KodeRak As String
    Dim dgv_IDPallet, dgv_GoodStock, dgv_StockBags, dgv_Satuan, dgv_Jumlah, dgv_RakTujuan, dgv_JmlhBags, dgv_HPP As String
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
    Dim itemDgvHPP As Integer = 13

    'Dim itemDgvIDWareHouseTujuan As Integer = 10
    Private Sub EMI_Transfer_Cost_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Transfer_Cost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        dgv_StockBags = DGV_Data_TF.Rows(index).Cells(itemDgvStockBags).Value
        dgv_CheckBox = Convert.ToBoolean(DGV_Data_TF.Rows(index).Cells(itemDgvCheckBox).Value)
        dgv_Jumlah = DGV_Data_TF.Rows(index).Cells(itemDgvJumlah).Value
        dgv_JmlhBags = DGV_Data_TF.Rows(index).Cells(itemDgvBags).Value
        dgv_HPP = DGV_Data_TF.Rows(index).Cells(itemDgvHPP).Value

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

        'Lv_DetBarang.View = View.Details

    End Sub

    'FUNCTION UTILITY
    Private Sub kosong()
        get_jam()

        Try
            OpenConn()

            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1 : arrInisialFaktur.Clear() : arrSO.Clear()
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSO_Asal.Items.Add(dr("Keterangan")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                    arrSO.Add(dr("kode_stock_owner"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_DetBarang.Items.Clear()

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
        Dim FPro_Results As String = "TC-"
        TxtNo_Transaksi.Text = FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("EMI_Transfer_Cost", "No_Faktur", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(No_Faktur,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

    End Sub

    'FUNCTION HANDLE
    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged, Txt_SO.TextChanged
        If CmbSO_Asal.Items.Count = 0 Or CmbSO_Asal.SelectedIndex = -1 Then Exit Sub

        If Not TxtKd_Barang.Text.Trim.Count = 0 Then
            Lv_DetBarang.Location = New Point(25, 203)
            Lv_DetBarang.Visible = True
        Else
            Lv_DetBarang.Location = New Point(25, 203)
            Lv_DetBarang.Visible = False
        End If

        Try
            OpenConn()

            Lv_DetBarang.Items.Clear()

            SQL = "select a.kode_stock_owner, a.kode_barang, a.nama, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
            SQL = SQL & "b.satuan, a.good_stock) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
            SQL = SQL & " a.Satuan_Isi_Bags from barang a, barang_detail_satuan b "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO.Item(CmbSO_Asal.SelectedIndex) & "' "
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
        TxtStock.Text = lv_DetGoodStock
        TxtBags.Text = lv_DetJmlhBags
        'TxtSatuanBags.Text = lv_DetSatuanBags

        Lv_DetBarang.Location = New Point(803, 258)
        Lv_DetBarang.Visible = False

        Btn_GetData.Focus()
    End Sub

    Private Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_GetData.Click
        If TxtKd_Barang.Text.Trim.Length = 0 Or TxtKd_Barang.Text.Trim = "" Or TxtNm_Barang.Text.Trim.Length = 0 Then Exit Sub

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

            SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "

            SQL = SQL & "group by a.Id_WMS_Warehouse_Position, a.Keterangan "
            SQL = SQL & "order by a.Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    WarehosePosition.Add(Dr("Keterangan")) : arrIdWMSWarehouse.Add(Dr("Id_WMS_Warehouse_Position"))
                Loop
            End Using

            SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, b.Nama, a.Id_Warehouse, c.Keterangan as kode_rak, "
            SQL = SQL & " a.Id_Nametag_pallet, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, b.satuan, "
            SQL = SQL & "'" & TxtSatuan.Text & "', a.jumlah) as jumlah, b.satuan, a.nomor_pallet, ISNULL(a.Jumlah_Bags, 0) as stock_bags, "
            SQL = SQL & "dbo.get_hpp(Serial_Number) as HPP from barang_sn a, barang b, View_Warehouse_Position c "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.Id_Warehouse=c.Id_WMS_Warehouse_Position and a.Kode_Stock_Owner=b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and b.Kode_Stock_Owner='" & Txt_SO.Text & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "'"
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
                    DGV_Data_TF.Rows(rows).Cells(itemDgvGoodStock).Value = Dr("jumlah")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvStockBags).Value = Dr("stock_bags")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvSatuan).Value = TxtSatuan.Text

                    DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
                    DGV_Data_TF.Rows(rows).Cells(itemDgvBags).ReadOnly = True

                    DGV_Data_TF.Rows(rows).Cells(itemDgvHPP).Value = Dr("HPP")

                    rows = rows + 1

                Loop
            End Using

            TxtTotalTransfer.Text = 0
            TxtTotalTransferBags.Text = 0

            CmbSO_Asal.Enabled = False

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
        ElseIf CmbSO_Asal.Text.Trim.Length = 0 Then
            MessageBox.Show("SO belum di pilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
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

            Dim thpp As Double = 0
            Dim chkBox As String = DGV_Data_TF.CurrentRow.Cells(itemDgvCheckBox).Value
            Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value
            Dim cellValue2 As String = DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value
            Dim shpp As Double = 0

            For i As Integer = 0 To DGV_Data_TF.RowCount - 1
                If chkBox = "True" Then
                    get_grid_view(i)

                    Dim nilai_kecildetail1 As Double = 0
                    SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKd_Barang.Text & "', '" & TxtSatuan.Text & "',"
                    SQL = SQL & "'" & TxtSatuanKecil.Text & "', '" & dgv_Jumlah.ToString & "' ) as hasil"
                    Using Dr1 = OpenTrans(SQL)
                        If Dr1.Read Then
                            If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                Dr1.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("data konversi satuan kirim tidak ada ")
                                Exit Sub
                            End If

                            nilai_kecildetail1 = Dr1("hasil")
                        Else
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If
                    End Using

                    shpp = nilai_kecildetail1 * Val(HilangkanTanda(dgv_HPP))
                    thpp = thpp + shpp
                Else
                    Continue For
                End If
            Next

            SQL = "INSERT INTO EMI_Transfer_Cost(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Kode_Stock_Owner,Kode_Barang,"
            SQL = SQL & "Jumlah,Satuan,Total_Barang,Satuan_Barang,Keterangan,UserID,Grand,Tgl_Input,Jumlah_Bags) VALUES("
            SQL = SQL & "'" & KodePerusahaan & "','" & TxtNo_Transaksi.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & arrSO(CmbSO_Asal.SelectedIndex) & "',"
            SQL = SQL & "'" & kd_barang & "','" & HilangkanTanda(TxtTotalTransfer.Text) & "','" & TxtSatuan.Text & "',"
            SQL = SQL & "'" & nilai_kecil & "', '" & TxtSatuanKecil.Text.ToString & "','" & TxtKeterangan.Text.ToString & "',"
            SQL = SQL & "'" & UserID & "','" & thpp & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtTotalTransferBags.Text) & "') "
            ExecuteTrans(SQL)

            SQL = "Update Barang Set Good_Stock = Good_Stock - '" & nilai_kecil & "', "
            SQL = SQL & "Jumlah_Bags = Jumlah_Bags - '" & HilangkanTanda(TxtTotalTransferBags.Text) & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Stock_Owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
            SQL = SQL & "and Kode_Barang = '" & kd_barang & "' "
            ExecuteTrans(SQL)

            Dim isCheck As Boolean = False

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
                End If

                isCheck = True

                Dim nilai_kecildetail As Double = 0
                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKd_Barang.Text & "', '" & TxtSatuan.Text & "',"
                SQL = SQL & "'" & TxtSatuanKecil.Text & "', '" & dgv_Jumlah.ToString & "' ) as hasil"
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

                SQL = "INSERT INTO EMI_Transfer_Cost_Det(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Serial_Number,"
                SQL = SQL & "Jumlah,Jumlah_Bags) VALUES('" & KodePerusahaan & "','" & TxtNo_Transaksi.Text & "',"
                SQL = SQL & "'" & dgv_Lokasi & "','" & dgv_KodeBarang & "','" & dgv_SerialNumber & "',"
                SQL = SQL & "'" & nilai_kecildetail & "','" & Val(dgv_JmlhBags) & "')"
                ExecuteTrans(SQL)

                SQL = "Update Barang_SN Set Jumlah = Jumlah - '" & nilai_kecildetail & "',"
                SQL = SQL & "Jumlah_Bags = Jumlah_Bags - '" & Val(dgv_JmlhBags) & "' "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Stock_Owner = '" & dgv_Lokasi & "' "
                SQL = SQL & "and Kode_Barang = '" & dgv_KodeBarang & "' "
                SQL = SQL & "and Serial_Number = '" & dgv_SerialNumber & "' "
                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()
            'MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'kosong()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        cetak()

    End Sub

    Private Sub cetak()
        Try

            OpenConn()

            SQL = "select kode_perusahaan from Faktur_Transfer_Cost "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "no_faktur = '" & TxtNo_Transaksi.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Rpt_Faktur_Transfer_Cost       'Nama file CR
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{Faktur_Transfer_Cost.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Faktur_Transfer_Cost.no_faktur} = '" & TxtNo_Transaksi.Text & "'"
                        'CrDoc.SummaryInfo.ReportTitle = "Periode" & Format(DateTimePicker3.Value, "dd MMM yyyy") & " s/d " & Format(DateTimePicker4.Value, "dd MMM yyyy") & Chr(13) & "Akun : " & ComboBox1.Text
                        .Text = "Faktur Transfer Cost"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                    End With
                Else
                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub CmbSO_Asal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSO_Asal.SelectedIndexChanged
        DGV_Data_TF.Rows.Clear()
        TxtKd_Barang.Text = ""
        Txt_SO.Text = ""
        TxtNm_Barang.Text = ""
    End Sub

    Private Sub DGV_Data_TF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellEndEdit
        Dim chkBox As String = DGV_Data_TF.CurrentRow.Cells(itemDgvCheckBox).Value
        Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value
        Dim cellValue2 As String = DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value
        Dim shpp As Double = 0

        Dim totalJmlh As Integer = 0
        Dim totalBags As Integer = 0

        If chkBox = "True" Then
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = False
            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).ReadOnly = False
        Else
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = True
            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).ReadOnly = True

            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
        End If

        If Not IsNumeric(cellValue) Then
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
        End If
        If Not IsNumeric(cellValue2) Then
            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
        End If

        For i As Integer = 0 To DGV_Data_TF.RowCount - 1
            If chkBox = "True" Then
                get_grid_view(i)
                totalJmlh = totalJmlh + Val(isNull(DGV_Data_TF.Rows(i).Cells(itemDgvJumlah).Value))
                totalBags = totalBags + Val(isNull(DGV_Data_TF.Rows(i).Cells(itemDgvBags).Value))
                'shpp = Val(HilangkanTanda(dgv_Jumlah) + Val(HilangkanTanda(dgv_HPP)
                'thpp = thpp + shpp
            Else
                Continue For
            End If
        Next

        TxtTotalTransfer.Text = totalJmlh
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

End Class