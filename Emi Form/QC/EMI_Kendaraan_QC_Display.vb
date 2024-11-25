Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Kendaraan_QC_Display
    Dim arrcari As New ArrayList
    Dim Jenis = "ETA"

    Public filter_tambahan As String
    Public asal As String

    Dim LvNoFaktur, LvKdBrg As String
    Dim LvKdSupplier, LvNmSupplier, LvNoSJ As String
    Dim LvIdEkspedisi, LvEkspedisi, LvSupir, LvPlatNomor As String
    Dim LvNoTimbangan, LvNoPO, LvNoSJTimbangan, LvTgl, LvJam As String
    Dim LvBruto, LvTglBruto, LvJamBruto, LvFotoBruto1, LvFotoBruto2 As String
    Dim LvMasuk, LvTara, LvTglTara, LvJamTara As String
    Dim LvFotoTara1, LvFotoTara2, LvKeluar, LvNetto, LvLokasi, LvNoLoading As String

    Dim itemNoFaktur As Integer = 0
    Dim itemKdSupplier As Integer = 1
    Dim itemNmSupplier As Integer = 2
    Dim itemNoSJ As Integer = 3
    Dim itemIDEkspedisi As Integer = 4
    Dim itemEkspedisi As Integer = 5
    Dim itemSupir As Integer = 6
    Dim itemPlatNomor As Integer = 7
    Dim itemKdBarang As Integer = 8
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

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)

        LvNoFaktur = Lv_ListKendaraan.Items(NoIndex).Text
        LvKdSupplier = Lv_ListKendaraan.Items(NoIndex).SubItems(itemKdSupplier).Text
        LvNmSupplier = Lv_ListKendaraan.Items(NoIndex).SubItems(itemNmSupplier).Text
        LvNoSJ = Lv_ListKendaraan.Items(NoIndex).SubItems(itemNoSJ).Text
        LvIdEkspedisi = Lv_ListKendaraan.Items(NoIndex).SubItems(itemIDEkspedisi).Text
        LvEkspedisi = Lv_ListKendaraan.Items(NoIndex).SubItems(itemEkspedisi).Text
        LvSupir = Lv_ListKendaraan.Items(NoIndex).SubItems(itemSupir).Text
        LvPlatNomor = Lv_ListKendaraan.Items(NoIndex).SubItems(itemPlatNomor).Text
        LvKdBrg = Lv_ListKendaraan.Items(NoIndex).SubItems(itemKdBarang).Text
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
    End Sub

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

            Lv_ListKendaraan.Columns.Clear()
            Lv_ListKendaraan.Columns.Add("No Faktur QC", 110, HorizontalAlignment.Left) '0

            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Kode_Supplier, 110, HorizontalAlignment.Left) '2
            Lv_ListKendaraan.Columns.Add(Base_Language.lang_global_Nama_Supplier, 150, HorizontalAlignment.Left) '3
            Lv_ListKendaraan.Columns.Add("No. SJ", 100, HorizontalAlignment.Left) '4
            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Ekspedisi, 0, HorizontalAlignment.Left) '5
            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Ekspedisi, 130, HorizontalAlignment.Left) '6
            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Supir, 100, HorizontalAlignment.Left) '7
            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_PlatNomor, 100, HorizontalAlignment.Center) '8
            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_KodeBarang, 120, HorizontalAlignment.Left).DisplayIndex = 3 '1
            Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_NamaBarang, 220, HorizontalAlignment.Left).DisplayIndex = 4 '1
            Lv_ListKendaraan.Columns.Add("Step", 70, HorizontalAlignment.Center)
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_NoTimbangan, 0, HorizontalAlignment.Left) '9
            'Lv_ListKendaraan.Columns.Add("No. PO", 0, HorizontalAlignment.Left) '8
            'Lv_ListKendaraan.Columns.Add("No. SJ" + " " + Base_Language.Lang_Global_Timbangan, 0, HorizontalAlignment.Left) '9
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Tanggal, 0, HorizontalAlignment.Left) '10
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Jam, 0, HorizontalAlignment.Left) '11
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Bruto, 100, HorizontalAlignment.Left) '12
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_TglBruto, 100, HorizontalAlignment.Left) '13
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_JamBruto, 100, HorizontalAlignment.Left) '14
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_FotoBruto + "1", 0, HorizontalAlignment.Left) '15
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_FotoBruto + "2", 0, HorizontalAlignment.Left) '16
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_TimbangMasuk, 0, HorizontalAlignment.Left) '17
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Tara, 100, HorizontalAlignment.Left) '18
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_TglTara, 100, HorizontalAlignment.Left) '19
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_JamTara, 100, HorizontalAlignment.Left) '20
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_FotoTara + "1", 0, HorizontalAlignment.Left) '21
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_FotoTara + "2", 0, HorizontalAlignment.Left) '22
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_TimbangKeluar, 0, HorizontalAlignment.Left) '23
            'Lv_ListKendaraan.Columns.Add("Netto", 100, HorizontalAlignment.Left) '24
            'Lv_ListKendaraan.Columns.Add(Base_Language.Lang_Global_Lokasi, 120, HorizontalAlignment.Left).DisplayIndex = 0 '25
            'Lv_ListKendaraan.Columns.Add("No Loading", 0, HorizontalAlignment.Left) '26
            Lv_ListKendaraan.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Public Sub kosong()
        Get_Timbangan_Unloading()
    End Sub

    Private Sub Get_Timbangan_Unloading()
        Try
            OpenConn()

            Lv_ListKendaraan.Items.Clear()
            Lv_ListKendaraan.View = View.Details


            'SQL = "Select a.lokasi, a.Kode_Supplier, b.Nama, a.No_SJ, a.ETA, a.Id_Ekspedisi, e.Kode_Ekspedisi, a.driver as Supir, a.no_plat as Plat_Number, "
            'SQL = SQL & "c.No_Faktur as No_Timbangan, c.Tanggal, c.Jam, c.Bruto, c.Tgl_Bruto, c.Jam_Bruto, c.Foto_Bruto_1, Foto_Bruto_2, "
            'SQL = SQL & "c.Timbang_Masuk, c.Tara, c.Tgl_Tara, c.Jam_Tara, c.Foto_Tara_1, c.Foto_Tara_2, c.Timbang_Keluar, c.Netto, a.No_faktur as no_loading from "
            'SQL = SQL & "EMI_Pembelian_Loading a "
            'SQL = SQL & "inner join Suppliers b  "
            'SQL = SQL & "on a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            'SQL = SQL & "left outer join EMI_Timbang_Unloading c "
            'SQL = SQL & "on a.Kode_Perusahaan =a.Kode_Perusahaan and a.No_Faktur=c.No_Loading "
            'SQL = SQL & "left outer join EMI_Master_Ekspedisi e "
            'SQL = SQL & "on a.Id_Ekspedisi = e.Id_Ekspedisi "
            'SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null " & filter_tambahan
            'SQL = SQL & " Order By a.ETA Desc "

            SQL = "select a.No_Faktur,b.Kode_Supplier,c.Nama,b.driver as Supir,b.No_Plat as Plat_Number,b.No_SJ,a.Tanggal, a.Kode_Barang, d.nama as nama_barang, "
            SQL = SQL & "b.Id_EkspedisiX,e.Kode_Ekspedisi,a.step "
            SQL = SQL & "from EMI_Hasil_Quality_Control a, EMI_Pembelian_Loading b, Suppliers c, barang d, emi_master_ekspedisi e "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Perusahaan=c.Kode_Perusahaan and a.Kode_Perusahaan=d.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan=e.Kode_Perusahaan and a.No_Fak_Loading_Barang=b.No_Faktur and b.Kode_Supplier = c.Kode_Supplier  "
            SQL = SQL & "and a.Kode_Barang = d.Kode_Barang and b.Id_EkspedisiX=e.Id_Ekspedisi "
            SQL = SQL & "and a.Status is null and a.flag_sudah_qc_dekstop is null  "
            SQL = SQL & "group by a.No_Faktur,b.Kode_Supplier,c.Nama,b.driver, b.No_Plat ,b.No_SJ,a.Tanggal, a.Kode_Barang, d.nama , b.Id_EkspedisiX, e.Kode_Ekspedisi,a.step "
            SQL = SQL & "order by b.Kode_Supplier "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_ListKendaraan.Items.Add(dr("no_faktur"))
                    Lvw.SubItems.Add(dr("kode_supplier"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(dr("No_SJ"))
                    Lvw.SubItems.Add(dr("Id_EkspedisiX"))
                    Lvw.SubItems.Add(dr("Kode_Ekspedisi"))
                    Lvw.SubItems.Add(dr("Supir"))
                    Lvw.SubItems.Add(dr("Plat_Number"))
                    Lvw.SubItems.Add(dr("kode_barang"))
                    Lvw.SubItems.Add(dr("nama_barang"))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("step")))

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


        Get_Isi_ListView(Lv_ListKendaraan.FocusedItem.Index)

        EMI_Transaksi_Quality_Control.noQc = LvNoFaktur

        EMI_Transaksi_Quality_Control.ShowDialog()

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

    End Sub

End Class