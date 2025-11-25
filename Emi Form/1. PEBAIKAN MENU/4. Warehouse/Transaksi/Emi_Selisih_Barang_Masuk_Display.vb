Public Class Emi_Selisih_Barang_Masuk_Display

    Dim Lv_NoFaktur, Lv_KdSupplier, Lv_NoSJ, Lv_NoPlat, Lv_Driver, Lv_TglPO, Lv_TglMasuk, Lv_User, Lv_Lokasi, Lv_Supplier, Lv_TglBerangkat, Lv_JamMasuk, Lv_Flag_Selisih, Lv_No_PO As String

    Dim item_NoFak As Integer = 0
    Dim item_Supplier As Integer = 1
    Dim item_NoSJ As Integer = 2
    Dim item_NoPlat As Integer = 3
    Dim item_Driver As Integer = 4
    Dim item_TglPO As Integer = 5
    Dim item_TglMasuk As Integer = 6
    Dim item_User As Integer = 7
    Dim item_KdSupplier As Integer = 8
    Dim item_Lokasi As Integer = 9
    Dim item_TglBerangkat As Integer = 10
    Dim item_JamMasuk As Integer = 11
    Dim item_Flag_Selisih As Integer = 12
    Dim item_No_PO As Integer = 13


    Private Sub BtnSelisihBrgMsk_Refresh_Click(sender As Object, e As EventArgs) Handles BtnSelisihBrgMsk_Refresh.Click
        kosong()
        Load_Data()
    End Sub


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
        Lv_Data.Columns.Add("flag_Selisih", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("No_PO", 0, HorizontalAlignment.Center)

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
        Lv_Flag_Selisih = Lv_Data.Items(Index).SubItems(item_Flag_Selisih).Text
        Lv_No_PO = Lv_Data.Items(Index).SubItems(item_No_PO).Text

    End Sub


    Private Sub Load_Data()

        Try
            OpenConn()

            Lv_Data.Items.Clear()

            SQL = "select a.No_Faktur, b.Nama, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal as Tgl_PO, a.Tanggal_Masuk, a.UseriD, a.Kode_Supplier, a.Lokasi, "
            SQL = SQL & "a.Tanggal_OTW, a.Jam_Masuk, "
            SQL = SQL & "isnull(( select top 1 z.No_PO from EMI_Pembelian_Loading_Detail z "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = z.No_Faktur "
            SQL = SQL & "), '-') as No_PO "
            SQL = SQL & "from EMI_Pembelian_Loading a, Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.status is null and a.Flag_Sudah_Bongkar_Android ='Y' and a.Flag_Timbang_Keluar='Y' "
            SQL = SQL & "and a.Flag_Selisih_BM is null "
            SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur, b.Nama, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal, a.Tanggal_Masuk, a.UseriD, a.Kode_Supplier, a.Lokasi, "
            SQL = SQL & "a.Tanggal_OTW, a.Jam_Masuk"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim isDifferenceValue As Boolean = False
                            SQL = "select b.Jumlah as Jmlh_Pl, dbo.Ubah_Satuan(a.Kode_Perusahaan, 'MASA', b.Kode_Barang, b.satuan_barang, b.Satuan, b.Jumlah_Masuk) as Jmlh_BM "
                            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, Barang c, EMI_Pembelian_PO_Detail d "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
                            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
                            SQL = SQL & "and b.Urut_PO = d.No_Urut "
                            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Faktur='" & .Rows(i).Item("No_Faktur") & "' "
                            SQL = SQL & "and a.No_SJ='" & .Rows(i).Item("No_SJ") & "' and a.No_Plat='" & .Rows(i).Item("No_Plat") & "' "
                            Using Dr = OpenTrans(SQL)
                                Do While Dr.Read

                                    If Val(HilangkanTanda(Dr("Jmlh_Pl"))) <> Val(HilangkanTanda(Dr("Jmlh_BM"))) Then
                                        isDifferenceValue = True
                                        Exit Do
                                    End If
                                Loop
                            End Using

                            Dim lv As ListViewItem
                            lv = Lv_Data.Items.Add(If(General_Class.CekNULL(.Rows(i).Item("No_Faktur")) = "", "-", .Rows(i).Item("No_Faktur")))
                            lv.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Nama")) = "", "-", .Rows(i).Item("Nama")))
                            lv.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("No_SJ")) = "", "-", .Rows(i).Item("No_SJ")))
                            lv.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("No_Plat")) = "", "-", .Rows(i).Item("No_Plat")))
                            lv.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Driver")) = "", "-", .Rows(i).Item("Driver")))
                            lv.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Tgl_PO")) = "", "-", Format(.Rows(i).Item("Tgl_PO"), "dd MMM yyyy")))
                            lv.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Tanggal_Masuk")) = "", "-", Format(.Rows(i).Item("Tanggal_Masuk"), "dd MMM yyyy")))
                            lv.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("UseriD")) = "", "-", .Rows(i).Item("UseriD")))
                            'Hide
                            lv.SubItems.Add(.Rows(i).Item("Kode_Supplier"))
                            lv.SubItems.Add(.Rows(i).Item("Lokasi"))
                            lv.SubItems.Add(Format(.Rows(i).Item("Tanggal_OTW"), "dd MMM yyyy"))
                            lv.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Jam_Masuk")) = "", "-'", .Rows(i).Item("Jam_Masuk")))


                            If isDifferenceValue Then
                                lv.BackColor = Color.LightYellow
                                lv.SubItems.Add("Y")
                            Else
                                lv.BackColor = Color.LightGreen
                                lv.SubItems.Add("T")
                            End If

                            lv.SubItems.Add(.Rows(i).Item("No_PO"))

                        Next
                    End If
                End With

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
        EMI_Selisih_Barang_Masuk2.P_Flag_Selisih = Lv_Flag_Selisih
        EMI_Selisih_Barang_Masuk2.P_No_PO = Lv_No_PO


        EMI_Selisih_Barang_Masuk2.ShowDialog()

    End Sub


End Class