Public Class Emi_Display_QI

    Dim arrcari As New ArrayList

    Dim Lv_NoTrans, Lv_Nama, Lv_Rak, Lv_Tgl, Lv_Jam, Lv_Jumlah, Lv_Satuan, Lv_NoProduksiOrder, Lv_KdBarang, Lv_IdWarehouse, Lv_UrutDetailPallet, Lv_SnBaru, Lv_JumlahKecil, Lv_SatuanKecil As String

    Dim item_NoTrans As Integer = 0
    Dim item_Nama As Integer = 1
    Dim item_Rak As Integer = 2
    Dim item_Tanggal As Integer = 3
    Dim item_Jam As Integer = 4
    Dim item_Jumlah As Integer = 5
    Dim item_Satuan As Integer = 6
    Dim item_NoProduksiOrder As Integer = 7
    Dim item_KdBarang As Integer = 8
    Dim item_IdWarehouse As Integer = 9

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Then Exit Sub

        Try

            OpenConn()
            Lv_QI_detail.Items.Clear()
            EMI_Display_QC_Produksi.Lv_Data.Items.Clear()
            SQL = "select a.No_Transaksi,a.kode_stock_owner,d.nama,c.Labeling_WMS_Position as keterangan,b.Tanggal,b.Jam,a.Jumlah,a.Satuan,f.no_transaksi as no_production_result, f.No_Production_Order,"
            SQL = SQL & "e.SN_Baru, a.Id_Warehouse,a.Kode_Barang,a.Nilai_Barang,a.Satuan_barang,a.Urut_Production_Result_Pallet as urut_oto_pallet, a.urut_oto as urut_scan_qi  "
            SQL = SQL & "From EMI_Scan_QI_Detail a, EMI_Scan_QI b, View_Warehouse_Position c, barang d, Emi_Production_Results_Detail_Pallet e,Emi_Production_Results f  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Warehouse = c.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and a.Kode_Barang= d.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Urut_Production_Result_Pallet = e.Urut_Oto "
            SQL = SQL & "and e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Lv_Data.FocusedItem.Text & "' "
            SQL = SQL & "order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    'If Lv_Data.Items.Count <> 0 Then

                    '    If Dr("No_Transaksi") <> Lv_Data.Items(0).SubItems(item_NoTrans).Text Then
                    '        Dr.Close()
                    '        CloseConn()
                    '        MessageBox.Show("No Production tidak boleh berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If
                    'End If


                    'For i As Integer = 0 To Emi_QC_Hasil_Produksi.Lv_Data.Items.Count - 1

                    '    If Emi_QC_Hasil_Produksi.Lv_Data.Items(i).SubItems(10).Text = Dr("urut_oto") Then
                    '        Dr.Close()
                    '        CloseConn()
                    '        MessageBox.Show("Barang sudah ada di list QI", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If
                    'Next


                    Dim lvw As ListViewItem


                    lvw = EMI_Display_QC_Produksi.Lv_Data.Items.Add(Dr("no_transaksi"))
                    lvw.SubItems.Add(Dr("nama"))
                    lvw.SubItems.Add(Dr("keterangan"))
                    lvw.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Dr("jam"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(Dr("sn_baru"))
                    lvw.SubItems.Add(Dr("kode_barang"))
                    lvw.SubItems.Add(Dr("kode_stock_owner"))
                    lvw.SubItems.Add(Dr("urut_oto_pallet"))
                    lvw.SubItems.Add(Dr("urut_scan_qi"))
                Loop
            End Using



            CloseConn()

            Me.Close()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data.SelectedIndexChanged

        If Lv_Data.Items.Count = 0 Then Exit Sub

        Try

            OpenConn()
            Lv_QI_detail.Items.Clear()
            SQL = "select a.No_Transaksi,d.nama,c.Labeling_WMS_Position as keterangan,b.Tanggal,b.Jam,a.Jumlah,a.Satuan,f.no_transaksi as no_production_result, f.No_Production_Order,"
            SQL = SQL & "e.SN_Baru, a.Id_Warehouse,a.Kode_Barang,a.Nilai_Barang,a.Satuan_barang,a.Urut_Production_Result_Pallet as urut_detail_pallet "
            SQL = SQL & "From EMI_Scan_QI_Detail a, EMI_Scan_QI b, View_Warehouse_Position c, barang d, Emi_Production_Results_Detail_Pallet e,Emi_Production_Results f  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Warehouse = c.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and a.Kode_Barang= d.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Urut_Production_Result_Pallet = e.Urut_Oto "
            SQL = SQL & "and e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Lv_Data.FocusedItem.Text & "' "
            SQL = SQL & "order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim lv As ListViewItem
                    lv = Lv_QI_detail.Items.Add(Dr("no_production_result"))
                    lv.SubItems.Add(Dr("No_Production_Order"))
                    lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("Keterangan"))
                    lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("Jam"))
                    lv.SubItems.Add(Dr("Jumlah"))
                    lv.SubItems.Add(Dr("Satuan"))

                    'hide
                    lv.SubItems.Add(Dr("No_Production_Order"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Id_Warehouse"))
                    lv.SubItems.Add(Dr("urut_detail_pallet"))
                    lv.SubItems.Add(General_Class.CekNULL(Dr("SN_Baru")))
                    lv.SubItems.Add(Dr("NIlai_Barang"))
                    lv.SubItems.Add(Dr("Satuan_Barang"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Dim item_UrutDetailPallet As Integer = 10
    Dim item_SnBaru As Integer = 11
    Dim item_JmlhKecil As Integer = 12
    Dim item_SatuanKecil As Integer = 13


    Private Sub Emi_Display_QI_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Display_QI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Initial_Lv()
        Kosong()
    End Sub

    Private Sub Kosong()
        Lv_Data.Items.Clear()
        Lv_QI_detail.Items.Clear()

        arrcari.Clear()

        Txt_Filter_Value.Text = String.Empty

        Cmb_Filter_Jenis.Items.Clear()
        Cmb_Filter_Jenis.Items.Add("No Transaksi") : arrcari.Add("no_transaksi")
        Cmb_Filter_Jenis.Items.Add("Tanggal") : arrcari.Add("tanggal")
        Cmb_Filter_Jenis.Items.Add("User") : arrcari.Add("userid")

        Load_Lv()
    End Sub

    Private Sub Initial_Lv()
        Lv_Data.Columns.Clear()

        Lv_Data.Columns.Add("No Transaksi", 300, HorizontalAlignment.Left)

        Lv_Data.Columns.Add("Tanggal", 300, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 250, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("User", 155, HorizontalAlignment.Center)


        Lv_Data.View = View.Details

        Lv_QI_detail.Columns.Clear()
        Lv_QI_detail.Columns.Add("No Result Production", 140, HorizontalAlignment.Left)
        Lv_QI_detail.Columns.Add("No Production Order", 140, HorizontalAlignment.Left)
        Lv_QI_detail.Columns.Add("Nama", 250, HorizontalAlignment.Left)
        Lv_QI_detail.Columns.Add("Rak", 180, HorizontalAlignment.Left)
        Lv_QI_detail.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_QI_detail.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_QI_detail.Columns.Add("Jumlah", 80, HorizontalAlignment.Center)
        Lv_QI_detail.Columns.Add("Satuan", 90, HorizontalAlignment.Center)



        'HIDE
        Lv_QI_detail.Columns.Add("No_PO", 0, HorizontalAlignment.Left)
        Lv_QI_detail.Columns.Add("Kd_Barang", 0, HorizontalAlignment.Left)
        Lv_QI_detail.Columns.Add("Id_Warehouse", 0, HorizontalAlignment.Left)
        Lv_QI_detail.Columns.Add("Urut_Detail_Pallet", 0, HorizontalAlignment.Left)
        Lv_QI_detail.Columns.Add("Sn_Baru", 0, HorizontalAlignment.Left)
        Lv_QI_detail.Columns.Add("Jumlah_Kecil", 0, HorizontalAlignment.Left)
        Lv_QI_detail.Columns.Add("Satuan_KEcil", 0, HorizontalAlignment.Left)
        Lv_QI_detail.View = View.Details
    End Sub

    Private Sub Load_Lv()

        Try
            OpenConn()

            Lv_Data.Items.Clear()


            SQL = "select No_Transaksi,tanggal,jam,UserId "
            SQL = SQL & "From EMI_Scan_QI where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_QI = 'Y' and status is null "
            SQL = SQL & "order by no_transaksi "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As New ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))

                    Lv.SubItems.Add(Dr("userid"))



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

        Lv_NoTrans = Lv_Data.Items(index).SubItems(item_NoTrans).Text
        Lv_Nama = Lv_Data.Items(index).SubItems(item_Nama).Text
        Lv_Rak = Lv_Data.Items(index).SubItems(item_Rak).Text
        Lv_Tgl = Lv_Data.Items(index).SubItems(item_Tanggal).Text
        Lv_Jam = Lv_Data.Items(index).SubItems(item_Jam).Text
        Lv_Jumlah = Lv_Data.Items(index).SubItems(item_Jumlah).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_NoProduksiOrder = Lv_Data.Items(index).SubItems(item_NoProduksiOrder).Text
        Lv_KdBarang = Lv_Data.Items(index).SubItems(item_KdBarang).Text
        Lv_IdWarehouse = Lv_Data.Items(index).SubItems(item_IdWarehouse).Text
        Lv_UrutDetailPallet = Lv_Data.Items(index).SubItems(item_UrutDetailPallet).Text
        Lv_SnBaru = Lv_Data.Items(index).SubItems(item_SnBaru).Text
        Lv_JumlahKecil = Lv_Data.Items(index).SubItems(item_JmlhKecil).Text
        Lv_SatuanKecil = Lv_Data.Items(index).SubItems(item_SatuanKecil).Text

    End Sub


    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub
    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter_Jenis.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Harus Di Pilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Txt_Filter_Value.Text.Trim.Length = 0 Then
            MessageBox.Show("Value Harus Di Isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_Data.Items.Clear()

            'SQL = "select a.No_Transaksi, a.No_Production_Order, c.Kode_Barang, e.Nama, b.Id_Warehouse, d.Keterangan, a.Tanggal, a.Jam, "
            'SQL = SQL & "b.Jumlah, b.Satuan, b.NIlai_Barang, b.Satuan_Barang, b.Urut_Oto as urut_detail_pallet, b.SN_Baru "
            'SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Barang_SN c, View_Warehouse_Position d, barang e "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            'SQL = SQL & "and b.SN_Baru = c.Serial_Number "
            'SQL = SQL & "and b.Id_Warehouse = d.Id_WMS_Warehouse_Position "
            'SQL = SQL & "and c.Kode_Stock_Owner = e.Kode_Stock_Owner and c.Kode_Barang = e.Kode_Barang "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and c.Flag_QI = 'Y' "
            'SQL = SQL & "and b.Flag_Sudah_QI Is NULL "
            'SQL = SQL & "and a.Status is null "
            SQL = "select No_Transaksi,tanggal,jam,UserId "
            SQL = SQL & "From EMI_Scan_QI where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_QI = 'Y' and status is null "
            If Cmb_Filter_Jenis.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrcari.Item(Cmb_Filter_Jenis.SelectedIndex) & "  like  '%" & Trim(Txt_Filter_Value.Text) & "%' "
            End If

            SQL = SQL & "order by a.No_Transaksi "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As New ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))

                    Lv.SubItems.Add(Dr("userid"))

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