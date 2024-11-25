Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Emi_Display_Quality_Control
    Dim arrcari As New ArrayList
    Dim Jenis = "ETA"

    Public filter_tambahan As String
    Public asal As String

    Dim LvNoFaktur, LvKdBrg As String
    Dim LvKdSupplier, LvNmSupplier, LvNoSJ As String
    Dim LvSupir, LvPlatNomor As String
    Dim LvNoTimbangan, LvNoPO, LvNoSJTimbangan, LvTgl, LvJam As String
    Dim LvBruto, LvTglBruto, LvJamBruto, LvFotoBruto1, LvFotoBruto2 As String

    Dim LvMasuk, LvTara, LvTglTara, LvJamTara As String

    Dim Lv_NoFakturLoading, Lv_NoFakturQC, Lv_NoSj, Lv_NmSupplier, Lv_KdBarang, Lv_NmBarang, Lv_AssismentQC, Lv_NoPengujian, Lv_PlatNomor, Lv_Supir, Lv_TglMasuk As String

    Dim item_NoFakturLoading = 0
    Dim item_NoFakturQC = 1
    Dim item_NoSj = 2
    Dim item_NmSupplier = 3
    Dim item_KdBarang = 4
    Dim item_NmBarang = 5
    Dim item_AssismentQc = 6
    Dim item_NoPengujian = 7
    Dim item_PlatNomor = 8
    Dim item_Supir = 9
    Dim item_TglMasuk = 10


    'Dim LvFotoTara1, LvFotoTara2, LvKeluar, LvNetto, LvLokasi, LvNoLoading As String

    'Dim itemNoFaktur As Integer = 0
    'Dim itemKdSupplier As Integer = 1
    'Dim itemNmSupplier As Integer = 2
    'Dim itemNoSJ As Integer = 3
    'Dim itemSupir As Integer = 4
    'Dim itemPlatNomor As Integer = 5
    'Dim itemKdBarang As Integer = 6
    'Dim ItemNoTimbangan As Integer = 7
    'Dim itemNoPO As Integer = 8
    'Dim itemNoSJTimbangan As Integer = 9
    'Dim itemTgl As Integer = 10
    'Dim itemJam As Integer = 11
    'Dim itemBruto As Integer = 12
    'Dim itemTglBruto As Integer = 13
    'Dim itemJamBruto As Integer = 14
    'Dim itemFotoBruto1 As Integer = 15
    'Dim itemFotoBruto2 As Integer = 16
    'Dim itemMasuk As Integer = 17
    'Dim itemTara As Integer = 18
    'Dim itemTglTara As Integer = 19
    'Dim itemJamTara As Integer = 20
    'Dim itemFotoTara1 As Integer = 21
    'Dim itemFotoTara2 As Integer = 22
    'Dim itemKeluar As Integer = 23
    'Dim itemNetto As Integer = 24
    'Dim itemLokasi As Integer = 25
    'Dim itemNoLoading As Integer = 26



    Private Sub Popup_Timbang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub Popup_Timbang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Label1.Text = Base_Language.Lang_Global_ListKendaraan

            'Lv_ListKendaraan.Columns.Clear()
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_NoFaktur, 110, HorizontalAlignment.Left) '0
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Kode_Supplier, 110, HorizontalAlignment.Left) '2
            'Lv_ListKendaraan.Columns.Add(Base_Language.lang_global_Nama_Supplier, 305, HorizontalAlignment.Left) '3
            'Lv_ListKendaraan.Columns.Add("No. SJ", 170, HorizontalAlignment.Left) '4
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Supir, 150, HorizontalAlignment.Left) '7
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_PlatNomor, 100, HorizontalAlignment.Center) '8
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Tanggal, 100, HorizontalAlignment.Center)
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_KodeBarang, 120, HorizontalAlignment.Left).DisplayIndex = 3 '1
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_NamaBarang, 220, HorizontalAlignment.Left).DisplayIndex = 4 '1
            'Lv_ListKendaraan.Columns.Add("Step", 70, HorizontalAlignment.Center)

            Lv_ListKendaraan.Columns.Clear()
            Lv_ListKendaraan.Columns.Add("No Faktur", 130, HorizontalAlignment.Center) '0
            Lv_ListKendaraan.Columns.Add("No Faktur QC", 0, HorizontalAlignment.Center) '1
            Lv_ListKendaraan.Columns.Add("No SJ", 130, HorizontalAlignment.Center) '2
            Lv_ListKendaraan.Columns.Add("Supplier", 130, HorizontalAlignment.Left) '3
            Lv_ListKendaraan.Columns.Add("Kode Barang", 100, HorizontalAlignment.Center) '4
            Lv_ListKendaraan.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left) '5
            Lv_ListKendaraan.Columns.Add("Assisment QC", 110, HorizontalAlignment.Center) '6
            Lv_ListKendaraan.Columns.Add("Nomor Pengujian", 110, HorizontalAlignment.Center) '7
            Lv_ListKendaraan.Columns.Add("Plat Nomor", 100, HorizontalAlignment.Center) '8
            Lv_ListKendaraan.Columns.Add("Supir", 140, HorizontalAlignment.Left) '9
            Lv_ListKendaraan.Columns.Add("Tanggal Masuk", 110, HorizontalAlignment.Center) '10

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Public Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Get_Timbangan_Unloading()
    End Sub

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)

        'LvNoFaktur = Lv_ListKendaraan.Items(NoIndex).Text
        'LvKdSupplier = Lv_ListKendaraan.Items(NoIndex).SubItems(itemKdSupplier).Text
        'LvNmSupplier = Lv_ListKendaraan.Items(NoIndex).SubItems(itemNmSupplier).Text
        'LvNoSJ = Lv_ListKendaraan.Items(NoIndex).SubItems(itemNoSJ).Text
        'LvSupir = Lv_ListKendaraan.Items(NoIndex).SubItems(itemSupir).Text
        'LvPlatNomor = Lv_ListKendaraan.Items(NoIndex).SubItems(itemPlatNomor).Text
        'LvKdBrg = Lv_ListKendaraan.Items(NoIndex).SubItems(itemKdBarang).Text
        'LvNoTimbangan = Lv_ListKendaraan.Items(NoIndex).SubItems(ItemNoTimbangan).Text
        'LvNoPO = Lv_ListKendaraan.Items(NoIndex).SubItems(itemNoPO).Text
        'LvNoSJTimbangan = Lv_ListKendaraan.Items(NoIndex).SubItems(itemNoSJTimbangan).Text
        'LvTgl = Lv_ListKendaraan.Items(NoIndex).SubItems(itemTgl).Text
        'LvJam = Lv_ListKendaraan.Items(NoIndex).SubItems(itemJam).Text
        'LvBruto = Lv_ListKendaraan.Items(NoIndex).SubItems(itemBruto).Text
        'LvTglBruto = Lv_ListKendaraan.Items(NoIndex).SubItems(itemTglBruto).Text
        'LvJamBruto = Lv_ListKendaraan.Items(NoIndex).SubItems(itemJamBruto).Text
        'LvFotoBruto1 = Lv_ListKendaraan.Items(NoIndex).SubItems(itemFotoBruto1).Text
        'LvFotoBruto2 = Lv_ListKendaraan.Items(NoIndex).SubItems(itemFotoBruto2).Text
        'LvMasuk = Lv_ListKendaraan.Items(NoIndex).SubItems(itemMasuk).Text
        'LvTara = Lv_ListKendaraan.Items(NoIndex).SubItems(itemTara).Text
        'LvTglTara = Lv_ListKendaraan.Items(NoIndex).SubItems(itemTglTara).Text
        'LvJamTara = Lv_ListKendaraan.Items(NoIndex).SubItems(itemJamTara).Text
        'LvFotoTara1 = Lv_ListKendaraan.Items(NoIndex).SubItems(itemFotoTara1).Text
        'LvFotoTara2 = Lv_ListKendaraan.Items(NoIndex).SubItems(itemFotoTara2).Text
        'LvKeluar = Lv_ListKendaraan.Items(NoIndex).SubItems(itemKeluar).Text
        'LvNetto = Lv_ListKendaraan.Items(NoIndex).SubItems(itemNetto).Text
        'LvLokasi = Lv_ListKendaraan.Items(NoIndex).SubItems(itemLokasi).Text
        'LvNoLoading = Lv_ListKendaraan.Items(NoIndex).SubItems(itemNoLoading).Text

        Lv_NoFakturLoading = Lv_ListKendaraan.Items(NoIndex).SubItems(item_NoFakturLoading).Text
        Lv_NoFakturQC = Lv_ListKendaraan.Items(NoIndex).SubItems(item_NoFakturQC).Text
        Lv_NoSj = Lv_ListKendaraan.Items(NoIndex).SubItems(item_NoSj).Text
        Lv_NmSupplier = Lv_ListKendaraan.Items(NoIndex).SubItems(item_NmSupplier).Text
        Lv_KdBarang = Lv_ListKendaraan.Items(NoIndex).SubItems(item_KdBarang).Text
        Lv_NmBarang = Lv_ListKendaraan.Items(NoIndex).SubItems(item_NmBarang).Text
        Lv_AssismentQC = Lv_ListKendaraan.Items(NoIndex).SubItems(item_AssismentQc).Text
        Lv_NoPengujian = Lv_ListKendaraan.Items(NoIndex).SubItems(item_NoPengujian).Text
        Lv_PlatNomor = Lv_ListKendaraan.Items(NoIndex).SubItems(item_PlatNomor).Text
        Lv_Supir = Lv_ListKendaraan.Items(NoIndex).SubItems(item_Supir).Text
        Lv_TglMasuk = Lv_ListKendaraan.Items(NoIndex).SubItems(item_TglMasuk).Text

    End Sub



    Public Sub kosong()

        ComboBox1.Items.Clear()
        TextBox3.Text = ""
        arrcari.Clear()

        ComboBox1.Items.Add("No Faktur") : arrcari.Add("a.No_Faktur")
        ComboBox1.Items.Add("No SJ") : arrcari.Add("a.No_SJ")
        ComboBox1.Items.Add("Supplier") : arrcari.Add("c.Nama")
        ComboBox1.Items.Add("Kode Barang") : arrcari.Add("b.Kode_Barang")
        ComboBox1.Items.Add("Nama Barang") : arrcari.Add("d.Nama")

        Lv_ListKendaraan.Items.Clear()

        Get_Timbangan_Unloading()
    End Sub

    Private Sub Get_Timbangan_Unloading()
        Try
            OpenConn()

            Lv_ListKendaraan.Items.Clear()
            Lv_ListKendaraan.View = View.Details

#Region "Kode Lama"

            ''SQL = "Select a.lokasi, a.Kode_Supplier, b.Nama, a.No_SJ, a.ETA, a.Id_Ekspedisi, e.Kode_Ekspedisi, a.driver as Supir, a.no_plat as Plat_Number, "
            ''SQL = SQL & "c.No_Faktur as No_Timbangan, c.Tanggal, c.Jam, c.Bruto, c.Tgl_Bruto, c.Jam_Bruto, c.Foto_Bruto_1, Foto_Bruto_2, "
            ''SQL = SQL & "c.Timbang_Masuk, c.Tara, c.Tgl_Tara, c.Jam_Tara, c.Foto_Tara_1, c.Foto_Tara_2, c.Timbang_Keluar, c.Netto, a.No_faktur as no_loading from "
            ''SQL = SQL & "EMI_Pembelian_Loading a "
            ''SQL = SQL & "inner join Suppliers b  "
            ''SQL = SQL & "on a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            ''SQL = SQL & "left outer join EMI_Timbang_Unloading c "
            ''SQL = SQL & "on a.Kode_Perusahaan =a.Kode_Perusahaan and a.No_Faktur=c.No_Loading "
            ''SQL = SQL & "left outer join EMI_Master_Ekspedisi e "
            ''SQL = SQL & "on a.Id_Ekspedisi = e.Id_Ekspedisi "
            ''SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null " & filter_tambahan
            ''SQL = SQL & " Order By a.ETA Desc "

            'SQL = "select a.No_Faktur,a.Kode_Supplier,b.Nama,a.No_SJ,a.No_Plat as plat_number,a.Driver as supir,a.Tanggal "
            'SQL = SQL & "From emi_pembelian_loading a, Suppliers b "
            'SQL = SQL & "where  a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            'SQL = SQL & "and a.Status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            ''SQL = "select a.No_Faktur,b.Kode_Supplier,d.Nama,b.driver as Supir,b.No_Plat as Plat_Number,b.No_SJ,a.Tanggal, "
            ''SQL = SQL & "a.Kode_Barang, c.nama as nama_barang, a.step "
            ''SQL = SQL & "from EMI_Hasil_Quality_Control a, emi_pembelian_loading b, barang c, Suppliers d where "
            ''SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Fak_Loading_Barang=b.no_faktur and "
            ''SQL = SQL & "a.status is null and b.status is null and a.Kode_Perusahaan = c.Kode_Perusahaan and "
            ''SQL = SQL & "a.Kode_Barang=c.Kode_Barang and a.Kode_Stock_Owner=c.Kode_Stock_Owner "
            ''SQL = SQL & "and b.Kode_Perusahaan=d.Kode_Perusahaan and b.Kode_Supplier=d.Kode_Supplier "
            ''SQL = SQL & "and a.flag_sudah_qc_dekstop is null "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = Lv_ListKendaraan.Items.Add(dr("no_faktur"))
            '        Lvw.SubItems.Add(dr("kode_supplier"))
            '        Lvw.SubItems.Add(dr("Nama"))
            '        Lvw.SubItems.Add(dr("No_SJ"))
            '        Lvw.SubItems.Add(dr("Supir"))
            '        Lvw.SubItems.Add(dr("Plat_Number"))
            '        Lvw.SubItems.Add(Format(dr("tanggal"), "dd MMM yyyy"))

            '    Loop
            'End Using

#End Region

            SQL = "select a.No_Faktur, b.No_Faktur as faktur_QC, c.Nama as supplier, a.No_SJ, b.Kode_Barang, d.Nama as Nama_Barang, "
            SQL = SQL & "b.Jenis_QC as asssisment_QC, b.Step as no_pengujian, a.No_Plat, a.Driver, a.Tanggal_Masuk "
            SQL = SQL & "from emi_pembelian_loading a,  EMI_Hasil_Quality_Control b, Suppliers c, Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Fak_Loading_Barang "
            SQL = SQL & "and a.Kode_Supplier = c.Kode_Supplier "
            SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and b.Flag_Sudah_QC_Dekstop is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If ComboBox1.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrcari.Item(ComboBox1.SelectedIndex) & "  like  '%" & Trim(TextBox3.Text) & "%' "
            End If
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_ListKendaraan.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("faktur_QC"))
                    Lv.SubItems.Add(Dr("No_SJ"))
                    Lv.SubItems.Add(Dr("supplier"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("asssisment_QC"))
                    Lv.SubItems.Add(Dr("no_pengujian"))
                    Lv.SubItems.Add(Dr("No_Plat"))
                    Lv.SubItems.Add(Dr("Driver"))
                    Lv.SubItems.Add(Format(Dr("Tanggal_Masuk"), "dd MMM yyyy"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles Lv_ListKendaraan.DoubleClick

        'If asal = "Unloading_Barang" Then
        '    EMI_Timbang_Unloading.kosong()
        '    If LvBruto = "-" Then
        '        EMI_Timbang_Unloading.LblNo_Loading.Text = LvNoLoading
        '        EMI_Timbang_Unloading.Lbl_KodeSupplier.Text = LvKdSupplier
        '        EMI_Timbang_Unloading.Lbl_NamaSupplier.Text = LvNmSupplier
        '        EMI_Timbang_Unloading.Txt_Supplier.Text = LvKdSupplier + "(" + LvNmSupplier + ")"
        '        EMI_Timbang_Unloading.Lbl_IDEkspedisi.Text = LvIdEkspedisi
        '        EMI_Timbang_Unloading.Lbl_NmEkspedisi.Text = LvEkspedisi
        '        EMI_Timbang_Unloading.Lbl_NoSJ.Text = LvNoSJ
        '        EMI_Timbang_Unloading.Txt_Ekspedisi.Text = LvEkspedisi
        '        EMI_Timbang_Unloading.Txt_Supir.Text = LvSupir
        '        EMI_Timbang_Unloading.Txt_PlatNomor.Text = LvPlatNomor
        '        EMI_Timbang_Unloading.Get_Timbang_Masuk()
        '        EMI_Timbang_Unloading.Get_DGV()

        '        EMI_Timbang_Unloading.Lbl_WaktuTimbangBruto.Visible = True
        '        EMI_Timbang_Unloading.DTP_Bruto.Visible = True
        '        EMI_Timbang_Unloading.Lbl_Bruto.Visible = True
        '        EMI_Timbang_Unloading.Txt_Bruto.Visible = True
        '        EMI_Timbang_Unloading.Label21.Visible = True
        '        'EMI_Timbang_Unloading.Lbl_Bruto.Location = New Point(15, 307)
        '        'EMI_Timbang_Unloading.Txt_Bruto.Location = New Point(130, 307)
        '        'EMI_Timbang_Unloading.Label21.Location = New Point(274, 307)

        '        EMI_Timbang_Unloading.Lbl_Tara.Visible = False
        '        EMI_Timbang_Unloading.Lbl_Netto.Visible = False
        '        EMI_Timbang_Unloading.Txt_Tara.Visible = False
        '        EMI_Timbang_Unloading.Txt_Netto.Visible = False
        '        EMI_Timbang_Unloading.Label8.Visible = False
        '        EMI_Timbang_Unloading.Label23.Visible = False
        '        EMI_Timbang_Unloading.Lbl_WaktuTimbangTara.Visible = False
        '        EMI_Timbang_Unloading.DTP_Tara.Visible = False
        '        EMI_Timbang_Unloading.Btn_Simpan.Tag = "&SimpanBruto"
        '        EMI_Timbang_Unloading.Btn_Simpan.Text = "&Simpan Bruto"
        '    Else
        '        EMI_Timbang_Unloading.LblNo_Loading.Text = LvNoLoading
        '        EMI_Timbang_Unloading.Txt_NoFaktur.Text = LvNoTimbangan
        '        EMI_Timbang_Unloading.Txt_Ekspedisi.Text = LvEkspedisi
        '        EMI_Timbang_Unloading.Lbl_KodeSupplier.Text = LvKdSupplier
        '        EMI_Timbang_Unloading.Lbl_NamaSupplier.Text = LvNmSupplier
        '        EMI_Timbang_Unloading.Txt_Supplier.Text = LvKdSupplier + "(" + LvNmSupplier + ")"
        '        EMI_Timbang_Unloading.Txt_Supir.Text = LvSupir
        '        EMI_Timbang_Unloading.Txt_PlatNomor.Text = LvPlatNomor
        '        EMI_Timbang_Unloading.Txt_Bruto.Text = LvBruto
        '        EMI_Timbang_Unloading.Lbl_NoSJ.Text = LvNoSJ
        '        EMI_Timbang_Unloading.ListView2.CheckBoxes = False

        '        EMI_Timbang_Unloading.Lbl_WaktuTimbangBruto.Visible = True
        '        EMI_Timbang_Unloading.DTP_Bruto.Visible = True
        '        EMI_Timbang_Unloading.Lbl_Bruto.Visible = True
        '        EMI_Timbang_Unloading.Txt_Bruto.Visible = True
        '        EMI_Timbang_Unloading.Label21.Visible = True

        '        EMI_Timbang_Unloading.Lbl_Tara.Visible = True
        '        EMI_Timbang_Unloading.Lbl_Netto.Visible = True
        '        EMI_Timbang_Unloading.Txt_Tara.Visible = True
        '        EMI_Timbang_Unloading.Txt_Netto.Visible = True
        '        EMI_Timbang_Unloading.Label8.Visible = True
        '        EMI_Timbang_Unloading.Label23.Visible = True
        '        EMI_Timbang_Unloading.Lbl_WaktuTimbangTara.Visible = True
        '        EMI_Timbang_Unloading.DTP_Tara.Visible = True
        '        ' EMI_Timbang_Unloading.Lbl_Tara.Location = New Point(15, 307)
        '        'EMI_Timbang_Unloading.Txt_Tara.Location = New Point(130, 307)
        '        'EMI_Timbang_Unloading.Label8.Location = New Point(274, 307)


        '        EMI_Timbang_Unloading.Get_Timbang_Keluar()
        '        EMI_Timbang_Unloading.Get_DGV()
        '        EMI_Timbang_Unloading.Hitung_Netto()
        '        EMI_Timbang_Unloading.Btn_Simpan.Tag = "&SimpanTara"
        '        EMI_Timbang_Unloading.Btn_Simpan.Text = "&Simpan Tara"
        '    End If

        '    EMI_Timbang_Unloading.ShowDialog()
        'ElseIf asal = "QC_BAHAN" Then
        '    'EMI_QC_Bahan.TxtNoLoading.Text = LvNoLoading
        '    'EMI_QC_Bahan.txtNoSJ.Text = LvNoSJ
        '    'EMI_QC_Bahan.txtNomorPlat.Text = LvPlatNomor
        '    'EMI_QC_Bahan.ShowDialog()

        'ElseIf asal = "Barang_Masuk" Then
        '    'Emi_Barang_Masuk.kosong()


        '    'Emi_Barang_Masuk.txtKodeSupp.Text = LvKdSupplier
        '    'Emi_Barang_Masuk.TxtBarangMasuk_NmSupplier.Text = LvNmSupplier
        '    'Emi_Barang_Masuk.TxtBarangMasuk_NoPO.Text = ""

        '    'Dim gudang As String = ""
        '    'Try
        '    '    OpenConn()
        '    '    SQL = "select Kode_Stock_Owner_gudang from Binding_Lokasi_Gudang where kode_perusahaan = '" & KodePerusahaan & "' and Gudang_Default = 'Y' and Kode_Stock_Owner='" & LvLokasi & "' "
        '    '    Using dr = OpenTrans(SQL)
        '    '        If dr.Read Then
        '    '            gudang = dr("Kode_Stock_Owner_gudang")
        '    '        Else
        '    '            dr.Close()
        '    '            CloseConn()
        '    '            MessageBox.Show(Base_Language.lang_global_Error_LokasiTidakAda, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    '            Exit Sub
        '    '        End If
        '    '    End Using

        '    '    CloseConn()
        '    'Catch ex As Exception
        '    '    CloseConn()
        '    '    MessageBox.Show(ex.Message)
        '    '    Exit Sub
        '    'End Try

        '    'Emi_Barang_Masuk.txtBarangMasuk_LokasiGudang.Text = gudang
        '    'Emi_Barang_Masuk.CmbBarangMasuk_Lokasi.Text = LvLokasi
        '    'Emi_Barang_Masuk.TxtBarang_Masuk_NoNota.Text = LvNoSJ
        '    'Emi_Barang_Masuk.TxtBarangMasuk_NoPlat.Text = LvPlatNomor

        '    'Emi_Barang_Masuk.TxtBarang_Masuk_NoNota.Focus()
        '    'Emi_Barang_Masuk.LvBarangMasuk_DataPO.Items.Clear()

        '    'Emi_Barang_Masuk.TxtBarangMasuk_KdBarang.Clear()
        '    'Emi_Barang_Masuk.TxtBarangMasuk_NmBarang.Clear()
        '    'Emi_Barang_Masuk.TxtBarangMasuk_Jml.Clear()

        '    'Emi_Barang_Masuk.ShowDialog()
        'Else
        '    MessageBox.Show(Base_Language.Lang_Global_FormAsal & " . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If


        Get_Isi_ListView(Lv_ListKendaraan.FocusedItem.Index)

        EMI_Transaksi_Quality_Control.noQc = Lv_NoFakturQC
        EMI_Transaksi_Quality_Control.ShowDialog()

    End Sub


End Class