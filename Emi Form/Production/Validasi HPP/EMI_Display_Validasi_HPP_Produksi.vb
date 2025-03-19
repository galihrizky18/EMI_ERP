Public Class EMI_Display_Validasi_HPP_Produksi
    Dim Jenis = "Display_Production_Order"
    Public asal As String
    Dim arrcari As New ArrayList
    Public filter_tambahan As String

    Dim LvNo_Faktur, LvKdBrg, LvNmBrg, LvTgl_Produksi, LvJam_Produksi, LvJumlah, LvLine, LvId_Routing, LvNo_PO, LvJmlhSplit As String

    Dim itemNoFak As Integer = 0
    Dim itemKdBrg As Integer = 1
    Dim itemNmBrg As Integer = 2
    Dim itemTglProd As Integer = 3
    Dim itemJamProd As Integer = 4
    Dim itemJml As Integer = 5
    Dim itemLine As Integer = 6
    Dim itemIdRouting As Integer = 7
    Dim itemNoPO As Integer = 8
    Dim itemJmlSplit As Integer = 9

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvNo_Faktur = ListView1.Items(NoIndex).Text
        LvKdBrg = ListView1.Items(NoIndex).SubItems(itemKdBrg).Text
        LvNmBrg = ListView1.Items(NoIndex).SubItems(itemNmBrg).Text
        LvTgl_Produksi = ListView1.Items(NoIndex).SubItems(itemTglProd).Text
        LvJam_Produksi = ListView1.Items(NoIndex).SubItems(itemJamProd).Text
        LvJumlah = ListView1.Items(NoIndex).SubItems(itemJml).Text
        'LvJumlah_Pro = ListView1.Items(NoIndex).SubItems(5).Text
        'LvGood_Stock = ListView1.Items(NoIndex).SubItems(6).Text
        'LvBad_Stock = ListView1.Items(NoIndex).SubItems(7).Text
        LvLine = ListView1.Items(NoIndex).SubItems(itemLine).Text
        LvId_Routing = ListView1.Items(NoIndex).SubItems(itemIdRouting).Text
        LvNo_PO = ListView1.Items(NoIndex).SubItems(itemNoPO).Text
        LvJmlhSplit = ListView1.Items(NoIndex).SubItems(itemJmlSplit).Text
    End Sub

    Private Sub EMI_Production_Order_Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong3()
    End Sub

    Private Sub isi_lv()
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.No_Transaksi,a.kode_barang,c.Nama, a.Tgl_Produksi,a.Jam_Produksi, a.Jumlah,d.Keterangan,d.Id_Routing,a.No_PO "

            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b,Barang c,Emi_Master_routing d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur and b.Selesai is null and b.flag_release='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Flag_Produksi = 'Y'  "
            SQL = SQL & "and b.Id_Routing = d.Id_Routing and a.Flag_Val_HPP_Produksi is null "
            If ComboBox3.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrcari.Item(ComboBox3.SelectedIndex) & "  like  '%" & Trim(TextBox3.Text) & "%' "
            End If

            SQL = SQL & "group by a.No_Transaksi,a.Tgl_Produksi,a.Jam_Produksi,a.kode_barang,c.Nama,a.Jumlah,d.Keterangan,d.Id_Routing,a.No_PO "
            SQL = SQL & "order by a.Tgl_Produksi,a.Jam_Produksi "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(dr("No_Transaksi"))
                    lvw.SubItems.Add(dr("kode_barang"))
                    lvw.SubItems.Add(dr("nama"))
                    lvw.SubItems.Add(Format(dr("Tgl_Produksi"), "dd-MMM-yyyy"))
                    lvw.SubItems.Add(dr("Jam_Produksi"))
                    lvw.SubItems.Add(Format(dr("jumlah"), "N2"))
                    'lvw.SubItems.Add(Format(dr("jumlah_produksi"), "N2"))
                    'lvw.SubItems.Add(Format(dr("good_stock"), "N2"))
                    'lvw.SubItems.Add(Format(dr("bad_stock"), "N2"))
                    lvw.SubItems.Add(dr("Keterangan"))
                    lvw.SubItems.Add(dr("Id_Routing"))
                    lvw.SubItems.Add(dr("No_PO"))
                    lvw.SubItems.Add(0)
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong3()
        'Label1.Text = "Display Production Order"
        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            ' Label1.Text = "Display - Penerimaan Barang"
            Label3.Text = Base_Language.Lang_Global_Jenis
            Btn_Cari.Text = Base_Language.Lang_Global_Cari

            ComboBox3.Items.Clear() : arrcari.Clear()
            ComboBox3.Items.Add(Base_Language.Lang_Global_NoFaktur) : arrcari.Add("a.No_Transaksi")
            ComboBox3.Items.Add("Line") : arrcari.Add("d.Id_Routing")
            '''ComboBox3.Items.Add("Nama Barang") : arrcari.Add("c.Nama")
            ComboBox3.SelectedIndex = -1
            TextBox3.Text = ""

            ListView1.Columns.Clear()
            ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 125, HorizontalAlignment.Left) '0
            ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 180, HorizontalAlignment.Left) '1
            ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 200, HorizontalAlignment.Left) '1
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal_Produksi, 125, HorizontalAlignment.Center) '2
            ListView1.Columns.Add(Base_Language.Lang_Global_Jam, 100, HorizontalAlignment.Center) '3
            ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 150, HorizontalAlignment.Right) '4
            ' ListView1.Columns.Add(Base_Language.Lang_GLOBAL_Jumlah_Produksi, 150, HorizontalAlignment.Right)
            '  ListView1.Columns.Add("Qty Good Stock", 0, HorizontalAlignment.Right)
            '  ListView1.Columns.Add("Qty Bad Stock", 0, HorizontalAlignment.Right)
            ListView1.Columns.Add("Jenis Produksi", 100, HorizontalAlignment.Center) '5
            ListView1.Columns.Add("id_routing", 0, HorizontalAlignment.Center) '6
            ListView1.Columns.Add("no po", 0, HorizontalAlignment.Center) '7
            ListView1.Columns.Add("Jumlah Split", 0, HorizontalAlignment.Center) '8
            ListView1.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        isi_lv()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        isi_lv()
        'If ComboBox3.Text.Trim.Length = 0 Then
        '    MessageBox.Show("Paramters harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    ComboBox3.Focus()
        '    Exit Sub
        'End If
        'Try
        '    OpenConn()

        '    ListView1.Items.Clear()
        '    SQL = "select a.No_Transaksi,a.Tgl_Produksi,a.Jam_Produksi,c.Nama,a.Jumlah,d.Keterangan,d.Id_Routing,a.No_PO,"
        '    SQL = SQL & "isnull((select sum(x.Jumlah) from Emi_Produksi_Hasil_Perpallet x where  a.Kode_Perusahaan = b.Kode_Perusahaan "
        '    SQL = SQL & "and a.No_Transaksi = x.No_Split), 0) as Total_Jumlah "
        '    SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b,Barang c,Emi_Master_routing d "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur and b.Selesai is null and b.flag_release='Y' "
        '    SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
        '    SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Flag_Produksi = 'Y' and a.Flag_Selesai_Produksi = 'Y' "
        '    SQL = SQL & "and a.Flag_Hasil_Produksi is null and b.Id_Routing = d.Id_Routing "
        '    If ComboBox3.SelectedIndex <> -1 Then
        '        If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
        '        SQL = SQL & arrcari.Item(ComboBox3.SelectedIndex) & "  like  '%" & Trim(TextBox3.Text) & "%' "
        '    End If
        '    SQL = SQL & "order by a.Tgl_Produksi,a.Jam_Produksi"
        '    Using dr = OpenTrans(SQL)
        '        Do While dr.Read
        '            Dim lvw As ListViewItem
        '            lvw = ListView1.Items.Add(dr("No_Transaksi"))
        '            lvw.SubItems.Add(Format(dr("Tgl_Produksi"), "dd-MMM-yyyy"))
        '            lvw.SubItems.Add(dr("Jam_Produksi"))
        '            lvw.SubItems.Add(dr("Nama"))
        '            lvw.SubItems.Add(Format(dr("jumlah"), "N2"))
        '            'lvw.SubItems.Add(Format(dr("jumlah_produksi"), "N2"))
        '            'lvw.SubItems.Add(Format(dr("good_stock"), "N2"))
        '            'lvw.SubItems.Add(Format(dr("bad_stock"), "N2"))
        '            lvw.SubItems.Add(dr("Keterangan"))
        '            lvw.SubItems.Add(dr("Id_Routing"))
        '            lvw.SubItems.Add(dr("No_PO"))
        '            lvw.SubItems.Add(dr("total_jumlah"))
        '        Loop
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub


    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()
            Get_Isi_ListView(ListView1.FocusedItem.Index)

            Emi_Validasi_HPP_Produksi.Txt_NoSplitProduksi.Text = LvNo_Faktur
            Emi_Validasi_HPP_Produksi.Txt_NoFaktur.Text = LvNo_Faktur
            Emi_Validasi_HPP_Produksi.Txt_KdBarang.Text = LvKdBrg
            Emi_Validasi_HPP_Produksi.Txt_NmBarang.Text = LvNmBrg

            Dim Tanggal As Date = Date.ParseExact(LvTgl_Produksi, "dd-MMM-yyyy", Globalization.CultureInfo.InvariantCulture)
            Emi_Validasi_HPP_Produksi.Txt_Tgl.Text = Format(Tanggal, "dd MMM yyyy")

            Emi_Validasi_HPP_Produksi.Txt_jam.Text = LvJam_Produksi
            Emi_Validasi_HPP_Produksi.Txt_Jenis.Text = LvLine

            Emi_Validasi_HPP_Produksi.ShowDialog()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ComboBox3.SelectedIndex = -1
        TextBox3.Clear()
        isi_lv()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            Btn_Cari_Click(Me, Nothing)
        End If
    End Sub

End Class