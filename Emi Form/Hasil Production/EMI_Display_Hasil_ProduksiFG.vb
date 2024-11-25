Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Display_Hasil_ProduksiFG
    Dim Jenis = "Display_Production_Order"
    Public asal As String
    Dim arrcari As New ArrayList
    Public filter_tambahan As String

    Dim LvNo_Faktur As String
    Dim LvTgl_Produksi As String
    Dim LvJam_Produksi As String
    Dim LvNm_Barang As String
    Dim LvJumlah As String
    'Dim LvJumlah_Pro As String
    'Dim LvGood_Stock As String
    'Dim LvBad_Stock As String
    Dim LvLine As String
    Dim LvId_Routing As String
    Dim LvNo_PO As String
    Dim LvJmlhSplit As String
    Dim LvKdBrg As String

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvNo_Faktur = ListView1.Items(NoIndex).Text
        LvTgl_Produksi = ListView1.Items(NoIndex).SubItems(1).Text
        LvJam_Produksi = ListView1.Items(NoIndex).SubItems(2).Text
        LvNm_Barang = ListView1.Items(NoIndex).SubItems(3).Text
        LvJumlah = ListView1.Items(NoIndex).SubItems(4).Text
        'LvJumlah_Pro = ListView1.Items(NoIndex).SubItems(5).Text
        'LvGood_Stock = ListView1.Items(NoIndex).SubItems(6).Text
        'LvBad_Stock = ListView1.Items(NoIndex).SubItems(7).Text
        LvLine = ListView1.Items(NoIndex).SubItems(5).Text
        LvId_Routing = ListView1.Items(NoIndex).SubItems(6).Text
        LvNo_PO = ListView1.Items(NoIndex).SubItems(7).Text
        LvJmlhSplit = ListView1.Items(NoIndex).SubItems(8).Text
        LvKdBrg = ListView1.Items(NoIndex).SubItems(9).Text
    End Sub
    Private Sub EMI_Production_Order_Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong3()
    End Sub

    Private Sub isi_lv()
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.No_Transaksi, a.Tgl_Produksi,a.Jam_Produksi,a.kode_barang,c.Nama,a.Jumlah,d.Keterangan,d.Id_Routing,a.No_PO "

            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b,Barang c,Emi_Master_routing d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur and b.Selesai is null and b.flag_release='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Flag_Produksi = 'Y'  "
            SQL = SQL & "and a.Flag_Hasil_Produksi is null and b.Id_Routing = d.Id_Routing  "
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
                    lvw.SubItems.Add(Format(dr("Tgl_Produksi"), "dd-MMM-yyyy"))
                    lvw.SubItems.Add(dr("Jam_Produksi"))
                    lvw.SubItems.Add(dr("Nama"))
                    lvw.SubItems.Add(Format(dr("jumlah"), "N2"))
                    'lvw.SubItems.Add(Format(dr("jumlah_produksi"), "N2"))
                    'lvw.SubItems.Add(Format(dr("good_stock"), "N2"))
                    'lvw.SubItems.Add(Format(dr("bad_stock"), "N2"))
                    lvw.SubItems.Add(dr("Keterangan"))
                    lvw.SubItems.Add(dr("Id_Routing"))
                    lvw.SubItems.Add(dr("No_PO"))
                    lvw.SubItems.Add(0)
                    lvw.SubItems.Add(dr("kode_barang"))
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

            Label1.Text = "Display - Penerimaan Barang"
            Label3.Text = Base_Language.Lang_Global_Jenis
            Btn_Cari.Text = Base_Language.Lang_Global_Cari

            ComboBox3.Items.Clear() : arrcari.Clear()
            ComboBox3.Items.Add(Base_Language.Lang_Global_NoFaktur) : arrcari.Add("a.No_Faktur")
            ComboBox3.Items.Add("Line") : arrcari.Add("d.Id_Routing")
            ComboBox3.Items.Add("Nama Barang") : arrcari.Add("c.Nama")
            ComboBox3.SelectedIndex = -1
            TextBox3.Text = ""

            ListView1.Columns.Clear()
            ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 125, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal_Produksi, 125, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Jam, 100, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 150, HorizontalAlignment.Right)
            ' ListView1.Columns.Add(Base_Language.Lang_GLOBAL_Jumlah_Produksi, 150, HorizontalAlignment.Right)
            '  ListView1.Columns.Add("Qty Good Stock", 0, HorizontalAlignment.Right)
            '  ListView1.Columns.Add("Qty Bad Stock", 0, HorizontalAlignment.Right)
            ListView1.Columns.Add("Jenis Produsi", 100, HorizontalAlignment.Center)
            ListView1.Columns.Add("id_routing", 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("no po", 0, HorizontalAlignment.Center)
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

            EMI_Hasil_ProductionFG.TxtNoSplit.Text = LvNo_Faktur
            EMI_Hasil_ProductionFG.ShowDialog()

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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class