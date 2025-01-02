Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Display_Split_Request_Material
    Dim Jenis = "Display_Production_Order"
    Public asal As String
    Dim arrcari As New ArrayList
    Public filter_tambahan As String


    Dim LvNo_Faktur As String
    Dim LvNo_FakturProduksi As String
    Dim LvKodeBarang As String
    Dim LvNama As String
    Dim LvTanggal As String
    Dim LvJam As String
    Dim LvUserId As String

    Dim cellNo_Faktur As Integer = 0
    Dim cellNo_FaktruProduksi As Integer = 1
    Dim cellKodeBrg As Integer = 2
    Dim cellNama As Integer = 3
    Dim cellTanggal As Integer = 4
    Dim cellJam As Integer = 5
    Dim cellUserid As Integer = 6




    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvNo_Faktur = ListView1.Items(cellNo_Faktur).Text
        LvNo_FakturProduksi = ListView1.Items(NoIndex).SubItems(cellNo_FaktruProduksi).Text
        LvKodeBarang = ListView1.Items(NoIndex).SubItems(cellKodeBrg).Text
        LvNama = ListView1.Items(NoIndex).SubItems(cellNama).Text
        LvTanggal = ListView1.Items(NoIndex).SubItems(cellTanggal).Text
        LvJam = ListView1.Items(NoIndex).SubItems(cellJam).Text
        LvUserId = ListView1.Items(NoIndex).SubItems(cellUserid).Text
    End Sub
    Private Sub EMI_Production_Order_Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong3()
    End Sub

    Private Sub isi_lv()
        Try
            OpenConn()

            ListView1.Items.Clear() : ListView3.Items.Clear()
            SQL = "select a.No_Faktur,a.No_Faktur_Order,a.Kode_Barang,b.Nama,a.Tanggal,a.Jam,a.UserId from Emi_Material_Requisition a, Barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Status is null "
            If ComboBox3.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrcari.Item(ComboBox3.SelectedIndex) & "  like  '%" & Trim(TextBox3.Text) & "%' "
            End If
            SQL = SQL & " order by No_Faktur,Tanggal,Jam "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(dr("no_faktur"))
                    lvw.SubItems.Add(dr("no_faktur_order"))
                    lvw.SubItems.Add(dr("Kode_Barang"))
                    lvw.SubItems.Add(dr("Nama"))
                    lvw.SubItems.Add(Format(dr("Tanggal"), "dd MM yyyy"))
                    lvw.SubItems.Add(dr("jam"))
                    lvw.SubItems.Add(dr("UserId"))

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

            Label1.Text = "EMI Display Slit Request Material"
            Label3.Text = Base_Language.Lang_Global_Jenis
            Btn_Cari.Text = Base_Language.Lang_Global_Cari

            ComboBox3.Items.Clear() : arrcari.Clear()
            ComboBox3.Items.Add(Base_Language.Lang_Global_NoFaktur) : arrcari.Add("a.No_Faktur")
            ComboBox3.Items.Add("Nomor Produksi") : arrcari.Add("a.no_faktur_order")
            ComboBox3.Items.Add("Kode Barang") : arrcari.Add("a.kode_barang")
            ComboBox3.Items.Add("Kode Barang") : arrcari.Add("c.nama")
            ComboBox3.SelectedIndex = -1
            TextBox3.Text = ""

            ListView1.Columns.Clear()
            ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 115, HorizontalAlignment.Left)
            ListView1.Columns.Add("No Produksi", 115, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 145, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal, 100, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Jam, 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("User", 100, HorizontalAlignment.Center)
            ListView1.View = View.Details

            ListView3.Columns.Clear()
            ListView3.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Left)
            ListView3.Columns.Add(Base_Language.Lang_Global_KodeBarang, 130, HorizontalAlignment.Left)
            ListView3.Columns.Add(Base_Language.Lang_Global_NamaBarang, 350, HorizontalAlignment.Left)
            ListView3.Columns.Add(Base_Language.Lang_Global_Jumlah, 120, HorizontalAlignment.Center)
            ListView3.Columns.Add(Base_Language.Lang_Global_Satuan, 150, HorizontalAlignment.Center)
            ListView3.Columns.Add("Jenis Material", 130, HorizontalAlignment.Center)

            ListView3.Columns.Add("urut oto", 0, HorizontalAlignment.Center)
            ListView3.Columns.Add("no_faktur", 0, HorizontalAlignment.Center)
            ListView3.View = View.Details

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

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.FocusedItem.Text = "" Then Exit Sub

        Try
            OpenConn()
            ListView3.Items.Clear()
            SQL = "select a.no_faktur,b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,b.Jumlah,b.Satuan,b.Jenis_Material,b.urut_oto "
            SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and a.Status is null and a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & ListView1.FocusedItem.Text & "' "
            SQL = SQL & "order by c.nama "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView3.Items.Add(dr("kode_stock_owner"))

                    lvw.SubItems.Add(dr("Kode_Barang"))
                    lvw.SubItems.Add(dr("Nama"))
                    lvw.SubItems.Add(Format(dr("jumlah"), "N2"))
                    lvw.SubItems.Add(dr("satuan"))
                    lvw.SubItems.Add(dr("jenis_material"))

                    lvw.SubItems.Add(dr("urut_oto"))
                    lvw.SubItems.Add(dr("no_faktur"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub ListView3_DoubleClick(sender As Object, e As EventArgs) Handles ListView3.DoubleClick
        If ListView3.FocusedItem.Text = "" Then Exit Sub


        SD_Convert_Request_Material.txtLokasi.Text = ""
        SD_Convert_Request_Material.txtKodeBarang.Text = ""
        SD_Convert_Request_Material.txtLokasi.Text = ""
        SD_Convert_Request_Material.TxtNama.Text = ""
        SD_Convert_Request_Material.TxtJmlhRequest.Text = ""
        SD_Convert_Request_Material.txtSatuan.Text = ""
        SD_Convert_Request_Material.txtUrut.Text = ""
        SD_Convert_Request_Material.txtNoFaktur.Text = ""


        SD_Convert_Request_Material.txtLokasi.Text = ListView3.FocusedItem.SubItems(0).Text
        SD_Convert_Request_Material.txtKodeBarang.Text = ListView3.FocusedItem.SubItems(1).Text
        SD_Convert_Request_Material.TxtNama.Text = ListView3.FocusedItem.SubItems(2).Text
        SD_Convert_Request_Material.TxtJmlhRequest.Text = ListView3.FocusedItem.SubItems(3).Text
        SD_Convert_Request_Material.txtSatuan.Text = ListView3.FocusedItem.SubItems(4).Text
        SD_Convert_Request_Material.txtUrut.Text = ListView3.FocusedItem.SubItems(6).Text
        SD_Convert_Request_Material.txtNoFaktur.Text = ListView3.FocusedItem.SubItems(7).Text
        SD_Convert_Request_Material.ShowDialog()


    End Sub


End Class