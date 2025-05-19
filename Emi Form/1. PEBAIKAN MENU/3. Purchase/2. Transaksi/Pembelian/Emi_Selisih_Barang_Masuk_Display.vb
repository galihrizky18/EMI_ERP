Public Class Emi_Selisih_Barang_Masuk_Display

    Dim Lv_NoFaktur, Lv_KdSupplier, Lv_NoSJ, Lv_NoPlat, Lv_Driver, Lv_TglPO, Lv_TglMasuk, Lv_User, Lv_Lokasi, Lv_Supplier, Lv_TglBerangkat, Lv_JamMasuk As String

    Dim item_NoFak As Integer = 0
    Dim item_Supplier As Integer = 1
    Dim item_NoSJ As Integer = 2
    Dim item_NoPlat As Integer = 3
    Dim item_Driver As Integer = 4
    Dim item_TglPO As Integer = 5
    Dim item_TglMasuk As Integer = 6
    Dim item_User As Integer = 7

    Private Sub BtnSelisihBrgMsk_Refresh_Click(sender As Object, e As EventArgs) Handles BtnSelisihBrgMsk_Refresh.Click
        kosong()
        Load_Data()
    End Sub

    Dim item_KdSupplier As Integer = 8
    Dim item_Lokasi As Integer = 9
    Dim item_TglBerangkat As Integer = 10
    Dim item_JamMasuk As Integer = 11


    Private Sub Emi_Selisih_Barang_Masuk_Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Initial_ListView()
        kosong()


    End Sub
    Private Sub Emi_Selisih_Barang_Masuk_Display_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub


    Public Sub kosong()

        Lv_Data.Items.Clear()
        Load_Data()
    End Sub


    Private Sub Initial_ListView()
        Lv_Data.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Supplier", 180, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("No Surat Jalan", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("No Plat", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Supir", 180, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal PO", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tanggal Masuk", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("User", 130, HorizontalAlignment.Center)

        'HIDE
        Lv_Data.Columns.Add("Kd_Supplier", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Lokasi", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("tgl_berangkat", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("jam_berangkat", 0, HorizontalAlignment.Center)

        Lv_Data.View = View.Details
    End Sub

    Private Sub Get_Lv_DataSelisih(ByVal Index As Integer)

        Lv_NoFaktur = Lv_Data.Items(Index).SubItems(item_NoFak).Text
        Lv_Supplier = Lv_Data.Items(Index).SubItems(item_Supplier).Text
        Lv_NoSJ = Lv_Data.Items(Index).SubItems(item_NoSJ).Text
        Lv_NoPlat = Lv_Data.Items(Index).SubItems(item_NoPlat).Text
        Lv_Driver = Lv_Data.Items(Index).SubItems(item_Driver).Text
        Lv_TglPO = Lv_Data.Items(Index).SubItems(item_TglPO).Text
        Lv_TglMasuk = Lv_Data.Items(Index).SubItems(item_TglMasuk).Text
        Lv_User = Lv_Data.Items(Index).SubItems(item_User).Text
        Lv_KdSupplier = Lv_Data.Items(Index).SubItems(item_KdSupplier).Text
        Lv_Lokasi = Lv_Data.Items(Index).SubItems(item_Lokasi).Text
        Lv_TglBerangkat = Lv_Data.Items(Index).SubItems(item_TglBerangkat).Text
        Lv_JamMasuk = Lv_Data.Items(Index).SubItems(item_JamMasuk).Text

    End Sub


    Private Sub Load_Data()

        Try
            OpenConn()

            Lv_Data.Items.Clear()

            SQL = "select a.No_Faktur, b.Nama, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal as Tgl_PO, a.Tanggal_Masuk, a.UseriD, a.Kode_Supplier, a.Lokasi, "
            SQL = SQL & "a.Tanggal_OTW, a.Jam_Masuk "
            SQL = SQL & "from EMI_Pembelian_Loading a, Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.status is null and a.Flag_Sudah_Bongkar_Android ='Y' and a.Flag_Timbang_Keluar='Y' "
            SQL = SQL & "and a.Flag_Selisih_BM is null "
            SQL = SQL & "group by  a.No_Faktur, b.Nama, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal, a.Tanggal_Masuk, a.UseriD, a.Kode_Supplier, a.Lokasi, "
            SQL = SQL & "a.Tanggal_OTW, a.Jam_Masuk"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("No_SJ"))
                    lv.SubItems.Add(Dr("No_Plat"))
                    lv.SubItems.Add(Dr("Driver"))
                    lv.SubItems.Add(Format(Dr("Tgl_PO"), "dd MMM yyyy"))
                    lv.SubItems.Add(Format(Dr("Tanggal_Masuk"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("UseriD"))
                    'Hide
                    lv.SubItems.Add(Dr("Kode_Supplier"))
                    lv.SubItems.Add(Dr("Lokasi"))
                    lv.SubItems.Add(Format(Dr("Tanggal_OTW"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("Jam_Masuk"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Then Exit Sub

        Get_Lv_DataSelisih(Lv_Data.FocusedItem.Index)


        EMI_Selisih_Barang_Masuk2.P_NoFakturPO = Lv_NoFaktur
        EMI_Selisih_Barang_Masuk2.P_Supplier = Lv_Supplier
        EMI_Selisih_Barang_Masuk2.P_KdSupplier = Lv_KdSupplier
        EMI_Selisih_Barang_Masuk2.P_NoSJ = Lv_NoSJ
        EMI_Selisih_Barang_Masuk2.P_NoPlat = Lv_NoPlat
        EMI_Selisih_Barang_Masuk2.P_Driver = Lv_Driver
        EMI_Selisih_Barang_Masuk2.P_TglMasuk = Lv_TglMasuk
        EMI_Selisih_Barang_Masuk2.P_TglBerangkat = Lv_TglBerangkat
        EMI_Selisih_Barang_Masuk2.P_JamMasuk = Lv_JamMasuk

        EMI_Selisih_Barang_Masuk2.ShowDialog()

    End Sub


End Class