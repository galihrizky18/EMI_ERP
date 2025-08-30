Public Class EMI_PO_Pembelian_Display2_Barang_Lain

    Public Property filter_tambahan As String

    Public bolehLewat As Boolean

    Public Property asal As String

    Dim arrcariLocal, arrcariImport As New ArrayList
    Dim Jenis = "Lokasi_PO"

    Dim Lv_PO_NoFak, Lv_PO_Lokasi, Lv_PO_Keterangan, Lv_PO_Release, Lv_PO_Tanggal, Lv_PO_Jam, Lv_PO_Supplier, Lv_PO_KategoriSupplier, Lv_PO_MUA, Lv_PO_MataUang, Lv_PO_ETD As String
    Dim Lv_Barang_NoFak, Lv_Barang_NoPenawaran, Lv_Barang_KdSo, Lv_Barang_KdBarang, Lv_Barang_NmBarang, Lv_Barang_JmlhPO, Lv_Barang_JmlhMasuk, Lv_Barang_Satuan, Lv_Barang_UrutPO, Lv_Barang_JumlahHutang As String

    Dim ItemPO_NoFaktur As Integer = 0
    Dim ItemPO_Lokasi As Integer = 1
    Dim ItemPO_Keterangan As Integer = 2
    Dim ItemPO_Release As Integer = 3
    Dim ItemPO_Tanggal As Integer = 4
    Dim ItemPO_Jam As Integer = 5
    Dim ItemPO_Supplier As Integer = 6
    Dim ItemPO_KategoriSupplier As Integer = 7
    Dim ItemPO_MUA As Integer = 8
    Dim ItemPO_MataUang As Integer = 9
    Dim ItemPO_ETD As Integer = 10

    Dim itemBarang_NoFak As Integer = 0
    Dim itemBarang_NoPenawaran As Integer = 1
    Dim itemBarang_KdSo As Integer = 2
    Dim itemBarang_KdBarang As Integer = 3
    Dim itemBarang_NmBarang As Integer = 4
    Dim itemBarang_JmlhPO As Integer = 5
    Dim itemBarang_JmlhMasuk As Integer = 6
    Dim itemBarang_Satuan As Integer = 7
    Dim itemBarang_UrutPO As Integer = 8
    Dim itemBarang_JumlahHutang As Integer = 9

    Dim ID_Prepare As String = "0"
    Dim ID_PO As String = "1"
    Dim ID_ETD As String = "2"
    Dim ID_ETA As String = "3"
    Dim ID_Timbang As String = "4"
    Dim ID_Selisih As String = "5"
    Dim ID_Pembelian As String = "6"

    Private Sub SD_Pilih_PO_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub SD_Pilih_PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try

            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Label1.Text = Base_Language.Lang_Lokasi_PO_Judul_Display
            Label4.Text = Base_Language.Lang_Lokasi_PO_Kolom

            ComboBox1.Items.Clear() : arrcariLocal.Clear() : arrcariImport.Clear()
            ComboBox1.Items.Add("No Faktur") : arrcariLocal.Add("a.No_Faktur") : arrcariImport.Add("a.No_Faktur")
            ComboBox1.Items.Add("Lokasi") : arrcariLocal.Add("a.Lokasi") : arrcariImport.Add("a.Lokasi")
            ComboBox1.Items.Add("Supplier") : arrcariLocal.Add("b.Nama") : arrcariImport.Add("b.Nama")
            ComboBox1.Items.Add("Kategori Supplier") : arrcariLocal.Add("c.Kode_Kategori_Suppliers") : arrcariImport.Add("c.Kode_Kategori_Suppliers")
            ComboBox1.Items.Add("Mata Uang") : arrcariLocal.Add("a.Mata_Uang") : arrcariImport.Add("a.Mata_Uang")

            Lv_PO.Columns.Clear()
            Lv_PO.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
            Lv_PO.Columns.Add("Lokasi", 0, HorizontalAlignment.Left)
            Lv_PO.Columns.Add("Keterangan", 370, HorizontalAlignment.Left)
            Lv_PO.Columns.Add("Release", 0, HorizontalAlignment.Center)
            Lv_PO.Columns.Add("Tanggal", 150, HorizontalAlignment.Center)
            Lv_PO.Columns.Add("Jam", 100, HorizontalAlignment.Center)
            Lv_PO.Columns.Add("Supplier", 250, HorizontalAlignment.Left)
            Lv_PO.Columns.Add("Kategori", 0, HorizontalAlignment.Center)
            Lv_PO.Columns.Add("MUA", 0, HorizontalAlignment.Right)
            Lv_PO.Columns.Add("Mata Uang", 0, HorizontalAlignment.Center)
            Lv_PO.Columns.Add("ETD", 0, HorizontalAlignment.Center)
            Lv_PO.View = View.Details

            Lv_Barang.Columns.Clear()
            Lv_Barang.Columns.Add("No Faktur", 0, HorizontalAlignment.Left)
            Lv_Barang.Columns.Add("No Penawaran", 0, HorizontalAlignment.Left)
            Lv_Barang.Columns.Add("Lokasi", 0, HorizontalAlignment.Left)
            Lv_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
            Lv_Barang.Columns.Add("Nama Barang", 260, HorizontalAlignment.Left)
            Lv_Barang.Columns.Add("Jumlah PO", 100, HorizontalAlignment.Right)
            Lv_Barang.Columns.Add("Jumlah Masuk", 100, HorizontalAlignment.Right)
            Lv_Barang.Columns.Add("Satuan", 0, HorizontalAlignment.Center)
            Lv_Barang.Columns.Add("No Urut", 0, HorizontalAlignment.Left)
            Lv_Barang.Columns.Add("Jumlah Hutang", 100, HorizontalAlignment.Right).DisplayIndex = 7
            'Hide
            Lv_Barang.View = View.Details

            Lv_Kendaraan.Columns.Clear()
            Lv_Kendaraan.Columns.Add("No Faktur", 0, HorizontalAlignment.Left)
            Lv_Kendaraan.Columns.Add("Lokasi", 0, HorizontalAlignment.Left)
            Lv_Kendaraan.Columns.Add("Surat Jalan", 100, HorizontalAlignment.Left)
            Lv_Kendaraan.Columns.Add("Plat Kendaraan", 110, HorizontalAlignment.Left)
            Lv_Kendaraan.Columns.Add("Driver", 150, HorizontalAlignment.Left)
            Lv_Kendaraan.Columns.Add("Flag Masuk", 0, HorizontalAlignment.Left)
            Lv_Kendaraan.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
            Lv_Kendaraan.Columns.Add("Jam", 100, HorizontalAlignment.Center)
            Lv_Kendaraan.Columns.Add("User", 0, HorizontalAlignment.Left)
            'HideS
            Lv_Kendaraan.Columns.Add("Flag_Validasi", 0, HorizontalAlignment.Center)
            Lv_Kendaraan.Columns.Add("urutLoading", 0, HorizontalAlignment.Center)
            Lv_Kendaraan.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cari("Y")
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        ComboBox1.SelectedIndex = -1
        TextBox3.Text = ""

        Cari("Y")
    End Sub

    Public Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            Lv_PO.Items.Clear() : Lv_Barang.Items.Clear() : Lv_Kendaraan.Items.Clear()
            SQL = "select a.No_Faktur, a.Lokasi, a.No_Nota as Keterangan, a.Flag_Release, a.Tanggal, a.Jam, a.Tanggal_Release, a.Jam_Release, a.Kode_Supplier, b.Nama as Nama_Supplier, c.Kode_Kategori_Suppliers, "
            SQL = SQL & "a.Total_MUA, a.Mata_Uang, a.ETD "
            SQL = SQL & "from EMI_Pembelian_PO_Barang_Lain a, Suppliers b, Suppliers_Kategori c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and b.ID_Kategori_Suppliers = c.ID_Kategori_Suppliers "
            SQL = SQL & "and a.Status is null and a.flag_pembelian is null and a.Flag_Biaya = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & " and " & arrcariLocal.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
            Else
                SQL = SQL & " "
            End If
            SQL = SQL & "order by a.No_Faktur "
            Using dr = OpenTrans(SQL)
                Do While dr.Read

                    Dim Lv As ListViewItem
                    Lv = Lv_PO.Items.Add(dr("No_Faktur"))
                    Lv.SubItems.Add(dr("Lokasi"))
                    Lv.SubItems.Add(dr("Keterangan"))
                    If General_Class.CekNULL(dr("Flag_Release")) = "Y" Then
                        Lv.SubItems.Add("Sudah Release")
                        Lv.SubItems.Add(Format(dr("Tanggal_Release"), "dd MMMM yyyy"))
                        Lv.SubItems.Add(dr("Jam_Release"))
                    Else
                        Lv.SubItems.Add("Belum Release")
                        Lv.SubItems.Add("-")
                        Lv.SubItems.Add("-")
                    End If

                    Lv.SubItems.Add(dr("Nama_Supplier"))
                    Lv.SubItems.Add(dr("Kode_Kategori_Suppliers"))
                    Lv.SubItems.Add(Format(dr("Total_MUA"), "N2"))
                    Lv.SubItems.Add(dr("Mata_Uang"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(dr("ETD")) = "", "-", Format(dr("ETD"), "dd MMMM yyyy")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Isi_LvPO(ByVal No_Index As Integer)

        Lv_PO_NoFak = Lv_PO.Items(No_Index).SubItems(ItemPO_NoFaktur).Text
        Lv_PO_Lokasi = Lv_PO.Items(No_Index).SubItems(ItemPO_Lokasi).Text
        Lv_PO_Keterangan = Lv_PO.Items(No_Index).SubItems(ItemPO_Keterangan).Text
        Lv_PO_Release = Lv_PO.Items(No_Index).SubItems(ItemPO_Release).Text
        Lv_PO_Tanggal = Lv_PO.Items(No_Index).SubItems(ItemPO_Tanggal).Text
        Lv_PO_Jam = Lv_PO.Items(No_Index).SubItems(ItemPO_Jam).Text
        Lv_PO_Supplier = Lv_PO.Items(No_Index).SubItems(ItemPO_Supplier).Text
        Lv_PO_KategoriSupplier = Lv_PO.Items(No_Index).SubItems(ItemPO_KategoriSupplier).Text
        Lv_PO_MUA = Lv_PO.Items(No_Index).SubItems(ItemPO_MUA).Text
        Lv_PO_MataUang = Lv_PO.Items(No_Index).SubItems(ItemPO_MataUang).Text
        Lv_PO_ETD = Lv_PO.Items(No_Index).SubItems(ItemPO_ETD).Text

    End Sub

    Private Sub Get_Isi_Lv_Barang(ByVal Index As Integer)

        Lv_Barang_NoFak = Lv_Barang.Items(Index).SubItems(itemBarang_NoFak).Text
        Lv_Barang_NoPenawaran = Lv_Barang.Items(Index).SubItems(itemBarang_NoPenawaran).Text
        Lv_Barang_KdSo = Lv_Barang.Items(Index).SubItems(itemBarang_KdSo).Text
        Lv_Barang_KdBarang = Lv_Barang.Items(Index).SubItems(itemBarang_KdBarang).Text
        Lv_Barang_NmBarang = Lv_Barang.Items(Index).SubItems(itemBarang_NmBarang).Text
        Lv_Barang_JmlhPO = Lv_Barang.Items(Index).SubItems(itemBarang_JmlhPO).Text
        Lv_Barang_JmlhMasuk = Lv_Barang.Items(Index).SubItems(itemBarang_JmlhMasuk).Text
        Lv_Barang_Satuan = Lv_Barang.Items(Index).SubItems(itemBarang_Satuan).Text
        Lv_Barang_UrutPO = Lv_Barang.Items(Index).SubItems(itemBarang_UrutPO).Text
        Lv_Barang_JumlahHutang = Lv_Barang.Items(Index).SubItems(itemBarang_JumlahHutang).Text
    End Sub

    Private Sub Lv_PO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PO.SelectedIndexChanged
        If Lv_PO.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()
            Get_Isi_LvPO(Lv_PO.FocusedItem.Index)



            Lv_Barang.Items.Clear() : Lv_Kendaraan.Items.Clear()
            SQL = "select a.No_Faktur, b.No_Penawaran, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah, "

            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select (dbo.ubah_satuan_lain(b.Kode_Perusahaan, 'masa', z.Kode_Barang, z.Satuan_Barang, z.Satuan, sum(z.Jumlah_Masuk))) "
            SQL = SQL & "from emi_pembelian_loading_detail_barang_lain z, emi_pembelian_loading_barang_lain y where b.Kode_Perusahaan = z.Kode_Perusahaan  "
            SQL = SQL & "and b.No_Faktur = z.No_PO and b.Kode_Stock_Owner = z.Kode_Stock_Owner and b.Kode_Barang = z.Kode_Barang "
            SQL = SQL & "and b.No_Urut = z.Urut_PO and z.kode_perusahaan = y.kode_perusahaan and z.no_faktur = y.no_faktur and y.status is null "
            SQL = SQL & "group by z.Kode_Barang, z.Satuan_Barang, z.Satuan "
            SQL = SQL & "), '0') as Jumlah_Masuk, "

            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select sum(x.Jumlah_Utang) from EMI_Pembelian_Selisih_Barang_Masuk_Barang_Lain z, EMI_Pembelian_Selisih_Barang_Masuk_Det_Barang_Lain x, emi_pembelian_loading_detail_barang_lain y, emi_pembelian_loading_barang_lain w "
            SQL = SQL & "where z.Kode_Perusahaan  =  x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and z.status is null "
            SQL = SQL & "and y.Kode_Perusahaan  = z.Kode_Perusahaan and y.No_Faktur = z.No_Faktur_BM  "
            SQL = SQL & "and y.Kode_Stock_Owner = x.Kode_Stock_Owner and  y.Kode_Barang =  x.Kode_Barang and y.Urut_Oto =  x.Urut_Loading "
            SQL = SQL & "and y.Kode_Perusahaan  =  w.Kode_Perusahaan and y.No_Faktur = w.No_Faktur and w.status is null "
            SQL = SQL & "and b.No_Faktur = y.No_PO and b.Kode_Stock_Owner = y.Kode_Stock_Owner and b.Kode_Barang = y.Kode_Barang "
            SQL = SQL & "and b.No_Urut = y.Urut_PO  "
            SQL = SQL & "), '0') as Jumlah_Hutang,  "

            SQL = SQL & "b.Satuan, b.No_Urut "
            SQL = SQL & "from EMI_Pembelian_PO_Barang_Lain a, EMI_Pembelian_PO_Detail_Barang_Lain b, Barang_Lain c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang =  c.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_Faktur = '" & Lv_PO_NoFak & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Barang.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_Penawaran"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Dr("Jumlah") & " " & Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Jumlah_Masuk") & " " & Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("No_Urut"))
                    Lv.SubItems.Add(Dr("Jumlah_Hutang") & " " & Dr("Satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Load_Kendaraan()

    End Sub

    Private Sub Lv_PO_DoubleClick(sender As Object, e As EventArgs) Handles Lv_PO.DoubleClick
        If Lv_PO.Items.Count = 0 Then
            Exit Sub
        End If
        Dim currentRow = Lv_PO.FocusedItem.Index
        Get_Isi_LvPO(currentRow)

        'If LvIDLocal = ID_Selisih Then
        '    EMI_Selisih_Barang_Masuk.kosong()
        '    EMI_Selisih_Barang_Masuk.txtNoPO.Text = LvNo_PoLocal
        '    EMI_Selisih_Barang_Masuk.dtpTanggalPO.Value = LvTanggalLocal
        '    EMI_Selisih_Barang_Masuk.txtKodeSupplier.Text = LvKd_SupplierLocal
        '    EMI_Selisih_Barang_Masuk.txtNamaSupplier.Text = LvNm_SupplierLocal
        '    'EMI_Selisih_Barang_Masuk.get_data()

        '    EMI_Selisih_Barang_Masuk.ShowDialog()

        'ElseIf LvIDLocal = ID_Pembelian Then
        EMI_Pembelian2_Barang_Lain.Kosong()
        EMI_Pembelian2_Barang_Lain.TxtPembelian_NoPO.Text = Lv_PO_NoFak
        EMI_Pembelian2_Barang_Lain.TxtPembelian_NoPO_Leave(Lv_PO, e)

        If bolehLewat Then
            EMI_Pembelian2_Barang_Lain.ShowDialog()
        End If

        'End If
    End Sub


    Private Sub Load_Kendaraan()
        If Lv_PO.Items.Count = 0 Then Exit Sub


        Try
            OpenConn()

            Lv_Kendaraan.Items.Clear()
            SQL = "select distinct a.No_Faktur, a.Lokasi, a.No_SJ, a.No_Plat, a.Driver, a.Flag_Timbang, a.Tanggal_Masuk,  a.Jam_Masuk,  a.UseriD, "
            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select coalesce(x.Flag_Validasi,'T') from EMI_Pembelian_Selisih_Barang_Masuk_Barang_Lain z, EMI_Pembelian_Selisih_Barang_Masuk_Det_Barang_Lain x "
            SQL = SQL & "where a.Kode_Perusahaan  = z.Kode_Perusahaan and z.Kode_Perusahaan  =  x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = z.No_Faktur_BM  "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur  "
            SQL = SQL & "and b.Kode_Stock_Owner = x.Kode_Stock_Owner and  b.Kode_Barang =  x.Kode_Barang and b.Urut_Oto =  x.Urut_Loading "
            SQL = SQL & "), 'X') as Flag_Validasi  "
            SQL = SQL & "from emi_pembelian_loading_barang_lain a,  emi_pembelian_loading_detail_barang_lain b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a .No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan  = '" & KodePerusahaan & "'"
            SQL = SQL & "and b.No_PO =  '" & Lv_PO_NoFak & "' "
            'SQL = SQL & "and ISNULL(( "
            'SQL = SQL & "select x.Flag_Validasi from EMI_Pembelian_Selisih_Barang_Masuk_Barang_Lain z, EMI_Pembelian_Selisih_Barang_Masuk_Det_Barang_Lain x "
            'SQL = SQL & "where a.Kode_Perusahaan  = z.Kode_Perusahaan and z.Kode_Perusahaan  =  x.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Faktur = z.No_Faktur_BM  "
            'SQL = SQL & "and z.No_Faktur = x.No_Faktur  "
            'SQL = SQL & "and b.Kode_Stock_Owner = x.Kode_Stock_Owner and  b.Kode_Barang =  x.Kode_Barang and b.Urut_Oto =  x.Urut_Loading "
            'SQL = SQL & "), 'X')<>'X' "
            SQL = SQL & "order by no_faktur"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Kendaraan.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("No_SJ"))
                    Lv.SubItems.Add(Dr("No_Plat"))
                    Lv.SubItems.Add(Dr("Driver"))

                    If General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
                        Lv.SubItems.Add("Sudah Masuk")
                        Lv.SubItems.Add(Format(Dr("Tanggal_Masuk"), "dd MMMM yyyy"))
                        Lv.SubItems.Add(Dr("Jam_Masuk"))
                    Else
                        Lv.SubItems.Add("Belum Masuk")
                        Lv.SubItems.Add("-")
                        Lv.SubItems.Add("-")
                    End If

                    Lv.SubItems.Add(Dr("UseriD"))

                    'Hide
                    Lv.SubItems.Add(Dr("Flag_Validasi"))
                    Lv.SubItems.Add("")

                    If General_Class.CekNULL(Dr("Flag_Validasi")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
                    ElseIf General_Class.CekNULL(Dr("Flag_Validasi")) = "X" Then
                        Lv.BackColor = Color.White
                    Else
                        Lv.BackColor = Color.LightYellow
                    End If

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