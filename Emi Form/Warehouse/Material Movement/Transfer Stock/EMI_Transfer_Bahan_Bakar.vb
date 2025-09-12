Public Class EMI_Transfer_Bahan_Bakar

    Dim Flag_Opname As Boolean = False

    Dim arrSO, arrInisialFaktur, arrIdWMSWarehouse, WarehosePosition As New ArrayList

    Dim arr2RakTujuan As New List(Of List(Of String))

    Dim lv_DetKodeSO, lv_DetKodeBarang, lv_DetNamaBarang, lv_DetGoodStock, lv_DetSatuan, lv_DetSatuanDIsplay, lv_DetJmlhBags, lv_DetSatuanBags As String

    Dim dgv_Lokasi, dgv_KodeBarang, dgv_SerialNumber, dgv_Nama, dgv_IDWareHouse, dgv_KodeRak As String
    Dim dgv_IDPallet, dgv_GoodStock, dgv_StockBags, dgv_Satuan, dgv_Jumlah, dgv_RakTujuan, dgv_JmlhBags, dgv_HPP, dgv_BlokSN As String
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
    Dim itemDgvBlokSN As Integer = 14



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
        dgv_BlokSN = DGV_Data_TF.Rows(index).Cells(itemDgvBlokSN).Value

    End Sub

    Private Sub Initial_List_View()

        Lv_DetBarang.Columns.Add("Kode SO", 180, HorizontalAlignment.Left)
        Lv_DetBarang.Columns.Add("Kode Barang", 180, HorizontalAlignment.Left)
        Lv_DetBarang.Columns.Add("Nama", 0, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Stock", 150, HorizontalAlignment.Right)
        Lv_DetBarang.Columns.Add("Satuan", 0, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Jumlah Bags", 150, HorizontalAlignment.Right)
        Lv_DetBarang.Columns.Add("Satuan", 0, HorizontalAlignment.Center)

        'Lv_DetBarang.View = View.Details

    End Sub

    'FUNCTION UTILITY
    Private Sub kosong()
        get_jam()

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

            If Flag_Opname Then
                DGV_Data_TF.Columns(itemDgvGoodStock).Visible = False
                DGV_Data_TF.Columns(itemDgvStockBags).Visible = False
            Else
                DGV_Data_TF.Columns(itemDgvGoodStock).Visible = True
                DGV_Data_TF.Columns(itemDgvStockBags).Visible = True
            End If

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
        TxtTotalTransfer.Text = ""
        TxtNo_Transaksi.Text = ""
        TxtKeterangan.Text = String.Empty
        TxtKd_Barang.Text = String.Empty
        Txt_SO.Text = String.Empty
        TxtNm_Barang.Text = String.Empty
        TxtBags.Text = String.Empty
        'TxtSatuanBags.Text = String.Empty
        TxtTotalTransferBags.Text = String.Empty

        CmbSO_Asal.Enabled = True

        DGV_Data_TF.Rows.Clear()

        TxtKeterangan.Focus()

    End Sub

    Private Sub DGV_Data_TF_MouseLeave(sender As Object, e As EventArgs) Handles DGV_Data_TF.MouseLeave
        If DGV_Data_TF.RowCount = 0 Then Exit Sub

        Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value

        If Not IsNumeric(cellValue) Then
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
        End If

        'PINDAH FOKUS
        'DGV_Data_TF.CurrentCell = DGV_Data_TF.CurrentRow.Cells(itemDgvSatuan)
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
                                      General_Class.Get_Last_Number2("EMI_Pengeluaran_Bahan_Bakar", "No_Faktur", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(No_Faktur,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

    End Sub

    'FUNCTION HANDLE
    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged, Txt_SO.TextChanged
        If CmbSO_Asal.Items.Count = 0 Or CmbSO_Asal.SelectedIndex = -1 Then Exit Sub

        If Not TxtKd_Barang.Text.Trim.Count = 0 Then
            Lv_DetBarang.Location = New Point(21, 203)
            Lv_DetBarang.Visible = True
        Else
            Lv_DetBarang.Location = New Point(21, 203)
            Lv_DetBarang.Visible = False
        End If

        Try
            OpenConn()

            Lv_DetBarang.Items.Clear()

            SQL = "select top(20) a.kode_stock_owner, a.kode_barang, a.nama, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
            SQL = SQL & "b.satuan, a.good_stock) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
            SQL = SQL & " a.Satuan_Isi_Bags from barang a, barang_detail_satuan b, emi_group_jenis c "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO.Item(CmbSO_Asal.SelectedIndex) & "' "
            SQL = SQL & "and a.kode_barang like '%" & TxtKd_Barang.Text & "%' and a.Kode_Barang=b.kode_barang "
            SQL = SQL & "And a.kode_Perusahaan = b.kode_Perusahaan And b.flag_tampil_display ='Y'  "
            SQL = SQL & "And a.kode_Perusahaan = c.kode_Perusahaan And a.id_group_jenis=c.id_group_jenis and c.flag_bahan_bakar='Y'  "
            SQL = SQL & "order by a.Kode_Barang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As New ListViewItem
                    Lv = Lv_DetBarang.Items.Add(Dr("kode_stock_owner"))
                    Lv.SubItems.Add(Dr("kode_barang"))
                    Lv.SubItems.Add("X")
                    If Flag_Opname Then
                        Lv.SubItems.Add(0)
                    Else
                        Lv.SubItems.Add(Dr("Good_Stock"))
                    End If
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("satuan_display"))
                    If Flag_Opname Then
                        Lv.SubItems.Add(0)
                    Else
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("Jumlah_Bags")))
                    End If
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

        If Lv_DetBarang.Items.Count = 0 Or Lv_DetBarang.FocusedItem.Index = -1 Then Exit Sub

        get_det_barang(Lv_DetBarang.FocusedItem.Index)


        Dim KdBarang As String = lv_DetKodeBarang
        Dim KdSO As String = lv_DetKodeSO
        Dim NmBarang As String = lv_DetNamaBarang
        Dim Satuan As String = lv_DetSatuanDIsplay
        Dim SatuanKecil As String = lv_DetSatuan
        Dim Stock As String = lv_DetGoodStock
        Dim Bags As String = lv_DetJmlhBags

        TxtKd_Barang.Text = KdBarang
        Txt_SO.Text = KdSO
        TxtNm_Barang.Text = NmBarang
        TxtSatuan.Text = Satuan
        TxtSatuanKecil.Text = SatuanKecil
        TxtStock.Text = Stock
        TxtBags.Text = Bags
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
            SQL = SQL & "dbo.get_hpp(Serial_Number) as HPP, a.Blok_SN from barang_sn a, barang b, View_Warehouse_Position c "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.Id_Warehouse=c.Id_WMS_Warehouse_Position and a.Kode_Stock_Owner=b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and b.Kode_Stock_Owner='" & Txt_SO.Text & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "' and a.jumlah<>0 "
            SQL = SQL & "order by a.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim subArr As New List(Of String)

                    DGV_Data_TF.Rows.Add(1)
                    DGV_Data_TF.Rows(rows).Cells(itemDgvLokasi).Value = Dr("Kode_Stock_Owner")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvKodeBarang).Value = Dr("Kode_Barang")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvSerialNumber).Value = Dr("Serial_Number")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvNama).Value = "X"
                    DGV_Data_TF.Rows(rows).Cells(itemDgvIDWareHose).Value = Dr("Id_Warehouse")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvKodeRak).Value = Dr("kode_rak")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvIDPallet).Value = Dr("nomor_pallet")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvGoodStock).Value = Dr("jumlah")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvStockBags).Value = Dr("stock_bags")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvSatuan).Value = TxtSatuan.Text

                    DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
                    DGV_Data_TF.Rows(rows).Cells(itemDgvBags).ReadOnly = True

                    DGV_Data_TF.Rows(rows).Cells(itemDgvHPP).Value = Dr("HPP")

                    DGV_Data_TF.Rows(rows).Cells(itemDgvBlokSN).Value = General_Class.CekNULL(Dr("Blok_SN"))

                    rows = rows + 1

                Loop
            End Using

            TxtTotalTransfer.Text = 0
            TxtTotalTransferBags.Text = 0

            CmbSO_Asal.Enabled = False

            DGV_Data_TF.Focus()

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
        If TxtKeterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKeterangan.Focus() : Exit Sub
        ElseIf DGV_Data_TF.RowCount = 0 Then
            MessageBox.Show("Belum ada barang yang mau di transfer!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKd_Barang.Focus() : Exit Sub
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
            For i As Integer = 0 To DGV_Data_TF.RowCount - 1

                Dim shpp As Double = 0
                Dim chkBox As String = DGV_Data_TF.Rows(i).Cells(itemDgvCheckBox).Value
                Dim cellValue As String = DGV_Data_TF.Rows(i).Cells(itemDgvJumlah).Value
                Dim cellValue2 As String = DGV_Data_TF.Rows(i).Cells(itemDgvBags).Value

                If chkBox = "True" Then
                    get_grid_view(i)

                    If dgv_BlokSN = "Y" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Transaksi tidak bisa di lanjutkan karena SN pada baris ke : " & i + 1 & "Telah Di Blok", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

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



            SQL = "INSERT INTO EMI_Pengeluaran_Bahan_Bakar(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Kode_Stock_Owner,Kode_Barang,"
            SQL = SQL & "Jumlah,Satuan,Total_Barang,Satuan_Barang,Keterangan,UserID,Grand,Jumlah_Bags) VALUES("
            SQL = SQL & "'" & KodePerusahaan & "','" & TxtNo_Transaksi.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & arrSO(CmbSO_Asal.SelectedIndex) & "',"
            SQL = SQL & "'" & kd_barang & "','" & HilangkanTanda(TxtTotalTransfer.Text) & "','" & TxtSatuan.Text & "',"
            SQL = SQL & "'" & nilai_kecil & "', '" & TxtSatuanKecil.Text.ToString & "','" & TxtKeterangan.Text.ToString & "',"
            SQL = SQL & "'" & UserID & "','" & thpp & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtTotalTransferBags.Text) & "') "
            ExecuteTrans(SQL)

            SQL = "Update Barang Set Good_Stock = Good_Stock - Round(" & nilai_kecil & ",4), "
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

                SQL = "INSERT INTO EMI_Pengeluaran_Bahan_Bakar_Det(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Serial_Number,"
                SQL = SQL & "Jumlah,Jumlah_Bags, jumlah_Terpakai) VALUES('" & KodePerusahaan & "','" & TxtNo_Transaksi.Text & "',"
                SQL = SQL & "'" & dgv_Lokasi & "','" & dgv_KodeBarang & "','" & dgv_SerialNumber & "',"
                SQL = SQL & "'" & nilai_kecildetail & "','" & Val(dgv_JmlhBags) & "', 0)"
                ExecuteTrans(SQL)

                SQL = "Update Barang_SN Set Jumlah = Jumlah - Round(" & nilai_kecildetail & ",4),"
                SQL = SQL & "Jumlah_Bags = Jumlah_Bags - '" & Val(dgv_JmlhBags) & "' "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Stock_Owner = '" & dgv_Lokasi & "' "
                SQL = SQL & "and Kode_Barang = '" & dgv_KodeBarang & "' "
                SQL = SQL & "and Serial_Number = '" & dgv_SerialNumber & "' "
                ExecuteTrans(SQL)
            Next


#Region "JURNAL"

            'dari
            Dim inisial_faktur_dari As String = ""
            Dim akun_persediaan_dari As String = ""
            Dim akun_persediaan_tujuan As String = ""

            SQL = "select inisial_faktur, Persediaan_BB_Dalam_Proses from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbSO_Asal.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_persediaan_tujuan = Dr("Persediaan_BB_Dalam_Proses")
                    inisial_faktur_dari = Dr("inisial_faktur")

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select c.akun_Persediaan "
            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.kode_stock_owner = '" & CmbSO_Asal.Text & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_persediaan_dari = Dr("akun_Persediaan")
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
            SQL = SQL & "'" & KodeProyek & "', 'Pengeluaran Bahan Bakar " & TxtNo_Transaksi.Text & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                      Strings.Mid(akun_persediaan_dari, 2, 1),
                      Strings.Mid(Ganti(akun_persediaan_dari), 3),
                      KodePerusahaan, KodeProyek, "Persedian " & TxtNo_Transaksi.Text, "0", thpp, pagenumber, CmbSO_Asal.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                     Strings.Mid(akun_persediaan_tujuan, 2, 1),
                     Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                     KodePerusahaan, KodeProyek, "Persedian " & TxtNo_Transaksi.Text, thpp, "0", pagenumber, CmbSO_Asal.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
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

            SQL = "update EMI_Pengeluaran_Bahan_Bakar set KOde_Voucher='" & Kode_voucher & "' "
            SQL = SQL & "where No_Faktur='" & TxtNo_Transaksi.Text & "' and Kode_Perusahaan ='" & KodePerusahaan & "' "
            ExecuteTrans(SQL)

            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & CmbSO_Asal.Text & "' "
            SQL = SQL & "AND a.Kode_Barang = '" & TxtKd_Barang.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
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

#End Region

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
        'cetak()

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
        Try
            OpenConn()

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub DGV_Data_TF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellEndEdit
        Dim chkBox As String = DGV_Data_TF.CurrentRow.Cells(itemDgvCheckBox).Value
        Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value
        Dim cellValue2 As String = DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value
        Dim isBlockSN As Boolean = If(DGV_Data_TF.CurrentRow.Cells(itemDgvBlokSN).Value = "Y", True, False)
        Dim shpp As Double = 0

        Dim totalJmlh As Integer = 0
        Dim totalBags As Integer = 0

        If isBlockSN Then
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = True
            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).ReadOnly = True

            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""

            DGV_Data_TF.CurrentRow.Cells(itemDgvCheckBox).Value = False

            MessageBox.Show("Tidak bisa menggunakan SN ini karena SN telah di Blok", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

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


        Dim jumlahStock As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvGoodStock).Value))
        Dim jumlahInput As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value))
        Dim stockBags As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvStockBags).Value))
        Dim jumlahInputBags As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value))

        If jumlahInput > jumlahStock Then
            MessageBox.Show("Jumlah Tidak Boleh Melebihi Stock ", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
            Exit Sub
        End If

        'cek apakah input melebihi
        If jumlahInputBags > stockBags Then
            MessageBox.Show("Bags Tidak Boleh Melebihi Stock Bags", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
            Exit Sub
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


    Private Sub TxtKd_Barang_Leave(sender As Object, e As EventArgs) Handles TxtKd_Barang.Leave
        If TxtKd_Barang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_DetBarang.Focused = True Then Exit Sub

        Try
            OpenConn()


            SQL = "select a.kode_stock_owner, a.kode_barang, a.nama, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
            SQL = SQL & "b.satuan, a.good_stock) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
            SQL = SQL & " a.Satuan_Isi_Bags from barang a, barang_detail_satuan b, emi_group_jenis c "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO.Item(CmbSO_Asal.SelectedIndex) & "' "
            SQL = SQL & "and a.kode_barang = '" & TxtKd_Barang.Text & "' and a.Kode_Barang = b.kode_barang "
            SQL = SQL & "And a.kode_Perusahaan = b.kode_Perusahaan And b.flag_tampil_display ='Y'  "
            SQL = SQL & "And a.kode_Perusahaan = c.kode_Perusahaan And a.id_group_jenis=c.id_group_jenis and c.flag_bahan_bakar='Y'  "
            SQL = SQL & "order by a.Kode_Barang"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    TxtKd_Barang.Text = Dr("kode_barang")
                    Txt_SO.Text = Dr("kode_stock_owner")
                    TxtNm_Barang.Text = Dr("nama")
                    TxtSatuan.Text = Dr("satuan_display")
                    TxtSatuanKecil.Text = Dr("Satuan")
                    If Flag_Opname Then
                        TxtBags.Text = 0
                        TxtStock.Text = 0
                    Else
                        TxtBags.Text = Dr("Jumlah_Bags")
                        TxtStock.Text = Dr("Good_Stock")
                    End If

                    Btn_GetData.Focus()
                Else
                    MessageBox.Show("Barang tidak ditemukan . . ! !", Judul)
                    TxtKd_Barang.Text = ""
                    Txt_SO.Text = ""
                    TxtNm_Barang.Text = ""
                    TxtSatuan.Text = ""
                    TxtSatuanKecil.Text = ""
                    TxtStock.Text = ""
                    TxtBags.Text = ""

                    TxtKd_Barang.Focus()

                End If

                Lv_DetBarang.Location = New Point(803, 258)
                Lv_DetBarang.Visible = False

            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub


    '===========================================================================================================
    '=     HANDLE KEY PRESS
    '===========================================================================================================

    Private Sub TxtKeterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKeterangan.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbSO_Asal.DroppedDown = True
            CmbSO_Asal.Focus()
        End If
    End Sub
    Private Sub CmbSO_Asal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSO_Asal.KeyPress
        If e.KeyChar = Chr(13) Then TxtKd_Barang.Focus()
    End Sub
    Private Sub TxtKd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKd_Barang.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKd_Barang.Text.Trim.Length = 0 Then TxtKd_Barang.Focus()
            TxtKd_Barang_Leave(TxtKd_Barang, e)

            Lv_DetBarang.Location = New Point(803, 258)
            Lv_DetBarang.Visible = False

            'Txt_KdBarang.Focus()
        End If
    End Sub
    Private Sub TxtKd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKd_Barang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_DetBarang.Focus()
    End Sub
    Private Sub Lv_DetBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_DetBarang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_DetBarang_DoubleClick(Lv_DetBarang, e)
        End If
    End Sub




End Class