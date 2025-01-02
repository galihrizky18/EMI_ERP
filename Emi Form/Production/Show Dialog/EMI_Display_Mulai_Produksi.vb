Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Display_Mulai_Produksi
    Dim Jenis = "Display_Production_Order"
    Public asal As String
    Dim arrcari As New ArrayList
    Public filter_tambahan As String

    Dim LvNo_Faktur, LvLokasi, LvTgl, LvJam, LvUserID, LvKdSO, LvKDBrg, LvNmBrg, LvJnsProduk, LvJml, LvSatuan, LvRouting As String

    Dim itemNoFak As Integer = 0
    Dim itemLokasi As Integer = 1
    Dim itemTgl As Integer = 2
    Dim itemJam As Integer = 3
    Dim itemUserID As Integer = 4
    Dim itemKdSO As Integer = 5
    Dim itemKdBrg As Integer = 6
    Dim itemNmBrg As Integer = 7
    Dim itemJnsProduk As Integer = 8
    Dim itemJml As Integer = 9
    Dim itemSatuan As Integer = 10
    Dim itemRouting As Integer = 11

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvNo_Faktur = ListView1.Items(NoIndex).Text
        LvLokasi = ListView1.Items(NoIndex).SubItems(itemLokasi).Text
        LvTgl = ListView1.Items(NoIndex).SubItems(itemTgl).Text
        LvJam = ListView1.Items(NoIndex).SubItems(itemJam).Text
        LvUserID = ListView1.Items(NoIndex).SubItems(itemUserID).Text
        LvKdSO = ListView1.Items(NoIndex).SubItems(itemKdSO).Text
        LvKDBrg = ListView1.Items(NoIndex).SubItems(itemKdBrg).Text
        LvNmBrg = ListView1.Items(NoIndex).SubItems(itemNmBrg).Text
        LvJnsProduk = ListView1.Items(NoIndex).SubItems(itemJnsProduk).Text
        LvJml = ListView1.Items(NoIndex).SubItems(itemJml).Text
        LvSatuan = ListView1.Items(NoIndex).SubItems(itemSatuan).Text
        LvRouting = ListView1.Items(NoIndex).SubItems(itemRouting).Text
    End Sub

    Private Sub EMI_Production_Order_Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong3()
    End Sub

    Private Sub isi_lv()
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.No_Faktur, a.Lokasi, a.Tanggal, a.Jam, a.UserId, a.Kode_stock_Owner, a.kode_barang, b.nama as nama_barang, d.keterangan as jenis_produk, a.jumlah, a.satuan, c.Keterangan as Routing "
            SQL = SQL & "from emi_order_produksi a, barang b, emi_master_routing c, emi_jenis_produk d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.Kode_stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Id_Routing = c.Id_Routing and a.Id_Jenis_Produk = d.Id_Jenis_Produk "
            SQL = SQL & "and a.status is null and a.flag_release = 'Y' and Flag_Selesai_Split is null "
            If ComboBox3.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrcari.Item(ComboBox3.SelectedIndex) & "  like  '%" & Trim(TextBox3.Text) & "%' "
            End If
            SQL = SQL & "order by a.No_Faktur "

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(dr("No_Faktur"))
                    lvw.SubItems.Add(dr("Lokasi"))
                    lvw.SubItems.Add(Format(dr("Tanggal"), "dd MMM yyyy"))
                    lvw.SubItems.Add(dr("Jam"))
                    lvw.SubItems.Add(dr("UserId"))
                    lvw.SubItems.Add(dr("Kode_stock_Owner"))
                    lvw.SubItems.Add(dr("kode_barang"))
                    lvw.SubItems.Add(dr("nama_barang"))
                    lvw.SubItems.Add(dr("jenis_produk"))
                    lvw.SubItems.Add(Format(dr("Jumlah"), "N2"))
                    lvw.SubItems.Add(dr("Satuan"))
                    lvw.SubItems.Add(dr("Routing"))
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

            '''Label1.Text = Base_Language.Lang_Display_Production_Order_Judul
            Label1.Text = "Display - Mulai Produksi"
            Label3.Text = Base_Language.Lang_Global_Jenis
            Btn_Cari.Text = Base_Language.Lang_Global_Cari

            ComboBox3.Items.Clear()
            ComboBox3.Items.Add(Base_Language.Lang_Global_SeluruhCombobox) : arrcari.Add("seluruh")
            'Cmb_Jenis.Items.Add("No Split PO") : arrDataJenis.Add("a.No_Transaksi")
            ComboBox3.Items.Add("No Faktur") : arrcari.Add("a.No_Faktur")
            ComboBox3.Items.Add("Lokasi") : arrcari.Add("a.Lokasi")
            ComboBox3.Items.Add("Kode Barang") : arrcari.Add("a.Kode_Barang")
            ComboBox3.Items.Add("Jenis Produk") : arrcari.Add("d.keterangan")
            ComboBox3.Items.Add("Routing") : arrcari.Add("c.Keterangan")
            ComboBox3.SelectedIndex = 0
            TextBox3.Text = ""

            ListView1.Columns.Clear()
            ListView1.Columns.Add("No Faktur", 140, HorizontalAlignment.Left)
            ListView1.Columns.Add("Lokasi", 140, HorizontalAlignment.Left)
            ListView1.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
            ListView1.Columns.Add("Jam", 120, HorizontalAlignment.Center)
            ListView1.Columns.Add("UserID", 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("Kode Stock Owner", 0, HorizontalAlignment.Left)
            ListView1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
            ListView1.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
            ListView1.Columns.Add("Jenis Produk", 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("Jumlah", 120, HorizontalAlignment.Center)
            ListView1.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
            ListView1.Columns.Add("Routing", 120, HorizontalAlignment.Center)
            ListView1.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        isi_lv()
    End Sub

    Public Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        isi_lv()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Get_Isi_ListView(ListView1.FocusedItem.Index)
        EMI_Produksi.Txt_NoTransaksi.Text = LvNo_Faktur
        EMI_Produksi.ShowDialog()

    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ComboBox3.SelectedIndex = 0
        TextBox3.Text = ""
        isi_lv()
    End Sub

End Class