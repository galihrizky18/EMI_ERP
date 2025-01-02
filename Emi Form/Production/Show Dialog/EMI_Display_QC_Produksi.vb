Public Class EMI_Display_QC_Produksi

    Dim arrcari As New ArrayList

    Dim Lv_NoTrans, Lv_Nama, Lv_Rak, Lv_Tgl, Lv_Jam, Lv_Jumlah, Lv_Satuan, Lv_SnBaru, Lv_KdBarang, LvKso, Lv_UrutOto, LvUrutScanQI As String

    Dim item_NoTrans As Integer = 0
    Dim item_Nama As Integer = 1
    Dim item_Rak As Integer = 2
    Dim item_Tanggal As Integer = 3
    Dim item_Jam As Integer = 4
    Dim item_Jumlah As Integer = 5
    Dim item_Satuan As Integer = 6
    Dim item_SNBaru As Integer = 7
    Dim item_KDBrg As Integer = 8
    Dim item_Kso As Integer = 9

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Refresh.Click
        Kosong()
    End Sub

    Dim item_urutOto As Integer = 10
    Dim item_urutScanQI As Integer = 11

    'Private Sub TxtQRCode_KeyDown(sender As Object, e As KeyEventArgs)

    '    If e.KeyCode = Keys.Enter Then
    '        loadDataQrCode()
    '    End If




    'End Sub


    'Private Sub loadDataQrCode()
    '    Try
    '        OpenConn()

    '        SQL = "select a.No_Transaksi, a.No_Production_Order,b.urut_oto,c.kode_barang,c.kode_stock_owner, c.Kode_Barang, e.Nama, b.Id_Warehouse, d.Keterangan, a.Tanggal, a.Jam, "
    '        SQL = SQL & "b.Jumlah, b.Satuan, b.NIlai_Barang, b.Satuan_Barang, b.Urut_Oto as urut_detail_pallet, b.SN_Baru "
    '        SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Barang_SN c, View_Warehouse_Position d, "
    '        SQL = SQL & "barang e where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan "
    '        SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
    '        SQL = SQL & "and b.SN_Baru = c.Serial_Number and b.Id_Warehouse = d.Id_WMS_Warehouse_Position and c.Kode_Stock_Owner = e.Kode_Stock_Owner and c.Kode_Barang = e.Kode_Barang "
    '        SQL = SQL & "and a.Kode_Perusahaan = '001' and c.Flag_QI = 'Y' and a.Status is null and b.Flag_Sudah_QI Is NULL  "
    '        SQL = SQL & "and c.qr_code + '-' + c.kode_unik_berjalan = '" & TxtQRCode.Text.Trim & "'"
    '        Using Dr = OpenTrans(SQL)
    '            If Dr.Read Then

    '                If Lv_Data.Items.Count <> 0 Then

    '                    If Dr("No_Transaksi") <> Lv_Data.Items(0).SubItems(item_NoTrans).Text Then
    '                        Dr.Close()
    '                        CloseConn()
    '                        MessageBox.Show("No Production tidak boleh berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                        Exit Sub
    '                    End If
    '                End If

    '                Dim lvw As ListViewItem


    '                lvw = Lv_Data.Items.Add(Dr("no_transaksi"))
    '                lvw.SubItems.Add(Dr("nama"))
    '                lvw.SubItems.Add(Dr("keterangan"))
    '                lvw.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
    '                lvw.SubItems.Add(Dr("jam"))
    '                lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
    '                lvw.SubItems.Add(Dr("satuan"))
    '                lvw.SubItems.Add(Dr("sn_baru"))
    '                lvw.SubItems.Add(Dr("kode_barang"))
    '                lvw.SubItems.Add(Dr("kode_stock_owner"))
    '                lvw.SubItems.Add(Dr("urut_oto"))
    '            End If
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try

    '    TxtQRCode.Clear()
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Kosong()
    End Sub

    Private Sub Btn_Mulai_QC_Click(sender As Object, e As EventArgs) Handles Btn_Mulai_QC.Click
        If Lv_Data.Items.Count = 0 Then
            MessageBox.Show("Barang harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        EMI_Transaksi_QC_Finish_Good.SN_Baru_For_Update = Lv_Data.Items(0).SubItems(item_SNBaru).Text
        EMI_Transaksi_QC_Finish_Good.TxtNoProduksi.Text = Lv_Data.Items(0).SubItems(item_NoTrans).Text
        EMI_Transaksi_QC_Finish_Good.TxtNamaBarang.Text = Lv_Data.Items(0).SubItems(item_Nama).Text
        EMI_Transaksi_QC_Finish_Good.TxtKdBarang.Text = Lv_Data.Items(0).SubItems(item_KDBrg).Text
        EMI_Transaksi_QC_Finish_Good.txtKso.Text = Lv_Data.Items(0).SubItems(item_Kso).Text

        EMI_Transaksi_QC_Finish_Good.ShowDialog()
    End Sub

    'Private Sub Txt_Filter_Value_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If e.KeyChar = Chr(13) Then
    '        Try
    '            OpenConn()


    '            SQL = "select a.No_Transaksi, a.No_Production_Order,b.urut_oto,c.kode_barang,c.kode_stock_owner, c.Kode_Barang, e.Nama, b.Id_Warehouse, d.Keterangan, a.Tanggal, a.Jam, "
    '            SQL = SQL & "b.Jumlah, b.Satuan, b.NIlai_Barang, b.Satuan_Barang, b.Urut_Oto as urut_detail_pallet, b.SN_Baru "
    '            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Barang_SN c, View_Warehouse_Position d, "
    '            SQL = SQL & "barang e where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan "
    '            SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
    '            SQL = SQL & "and b.SN_Baru = c.Serial_Number and b.Id_Warehouse = d.Id_WMS_Warehouse_Position and c.Kode_Stock_Owner = e.Kode_Stock_Owner and c.Kode_Barang = e.Kode_Barang "
    '            SQL = SQL & "and a.Kode_Perusahaan = '001' and c.Flag_QI = 'Y' and a.Status is null and b.Flag_Sudah_QI Is NULL  "
    '            SQL = SQL & "and c.qr_code + '-' + c.kode_unik_berjalan = '" & TxtQRCode.Text.Trim & "'"
    '            Using Dr = OpenTrans(SQL)
    '                If Dr.Read Then

    '                    If Lv_Data.Items.Count <> 0 Then

    '                        If Dr("No_Transaksi") <> Lv_Data.Items(0).SubItems(item_NoTrans).Text Then
    '                            Dr.Close()
    '                            CloseConn()
    '                            MessageBox.Show("No Production tidak boleh berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                            Exit Sub
    '                        End If
    '                    End If

    '                    Dim lvw As ListViewItem


    '                    lvw = Lv_Data.Items.Add(Dr("no_transaksi"))
    '                    lvw.SubItems.Add(Dr("nama"))
    '                    lvw.SubItems.Add(Dr("keterangan"))
    '                    lvw.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
    '                    lvw.SubItems.Add(Dr("jam"))
    '                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
    '                    lvw.SubItems.Add(Dr("satuan"))
    '                    lvw.SubItems.Add(Dr("sn_baru"))
    '                    lvw.SubItems.Add(Dr("kode_barang"))
    '                    lvw.SubItems.Add(Dr("kode_stock_owner"))
    '                    lvw.SubItems.Add(Dr("urut_oto"))
    '                End If
    '            End Using

    '            CloseConn()
    '        Catch ex As Exception
    '            CloseConn()
    '            MessageBox.Show(ex.Message)
    '            Exit Sub
    '        End Try

    '        TxtQRCode.Clear()
    '    End If
    'End Sub

    'Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
    '    If Lv_Data.Items.Count = 0 Then Exit Sub

    '    Lv_Data.FocusedItem.Remove()


    'End Sub






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

    Public Sub Kosong()
        Lv_Data.Items.Clear()
        arrcari.Clear()

        'TxtQRCode.Text = String.Empty


    End Sub

    Private Sub Initial_Lv()
        Lv_Data.Columns.Clear()

        Lv_Data.Columns.Add("No Transaksi", 140, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama", 290, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Rak", 160, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jumlah", 110, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 90, HorizontalAlignment.Center)

        'HIDE

        Lv_Data.Columns.Add("Sn_Baru", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("kode_barang", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("kode_stock_owner", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Urut", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Urut_Scan_QI", 0, HorizontalAlignment.Left)

        Lv_Data.View = View.Details
    End Sub



    Private Sub Get_Data_Lv(ByVal index As Integer)

        Lv_NoTrans = Lv_Data.Items(index).SubItems(item_NoTrans).Text
        Lv_Nama = Lv_Data.Items(index).SubItems(item_Nama).Text
        Lv_Rak = Lv_Data.Items(index).SubItems(item_Rak).Text
        Lv_Tgl = Lv_Data.Items(index).SubItems(item_Tanggal).Text
        Lv_Jam = Lv_Data.Items(index).SubItems(item_Jam).Text
        Lv_Jumlah = Lv_Data.Items(index).SubItems(item_Jumlah).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_SnBaru = Lv_Data.Items(index).SubItems(item_SNBaru).Text
        Lv_KdBarang = Lv_Data.Items(index).SubItems(item_KDBrg).Text
        LvKso = Lv_Data.Items(index).SubItems(item_Kso).Text
        Lv_UrutOto = Lv_Data.Items(index).SubItems(item_urutOto).Text

    End Sub


    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles btnSummaryDataQI.Click
        Emi_Display_QI.ShowDialog()
    End Sub






End Class