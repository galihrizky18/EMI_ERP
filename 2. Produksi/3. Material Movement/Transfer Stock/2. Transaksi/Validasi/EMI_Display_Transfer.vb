Public Class EMI_Display_Transfer

    Dim arrcari As New ArrayList
    Dim Jenis = "ETA"

    Dim ValueBarcode As String = ""

    Public Property filter_tambahan As String
    Public Property asal As String

    'Dim LvKdSupplier, LvNmSupplier, LvNoSJ As String
    'Dim LvIdEkspedisi, LvEkspedisi, LvSupir, LvPlatNomor As String
    'Dim LvNoTimbangan, LvNoPO, LvNoSJTimbangan, LvTgl, LvJam As String
    'Dim LvBruto, LvTglBruto, LvJamBruto, LvFotoBruto1, LvFotoBruto2 As String
    'Dim LvMasuk, LvTara, LvTglTara, LvJamTara As String
    'Dim LvFotoTara1, LvFotoTara2, LvKeluar, LvNetto, LvLokasi, LvNoLoading, LvProsesLoading As String

    'Dim isTimbangMasuk, isTimbangKeluar As String

    Dim LvKodeTransfer, LvSoAwal, LvSoAkhir, LvKodeBarang As String
    Dim LvNamaBarang, LvTotal, LvSatuan, lv_JumlahInput, Lv_SatuanInput, LvRak, LvSn, LvSatuanBarang As String

    Dim itemKodeTransfer As Integer = 0
    Dim itemSOAwal As Integer = 1
    Dim itemSOAkhir As Integer = 2
    Dim itemKodeBarang As Integer = 3
    Dim itemNamaBarang As Integer = 4
    Dim itemJumlahInput As Integer = 5
    Dim itemSatuanInput As Integer = 6
    Dim itemTotal As Integer = 7
    Dim itemSatuan As Integer = 8
    Dim itemLokasiRak As Integer = 9
    Dim itemSN As Integer = 10
    Dim itemSatuanBarang As Integer = 11

    Dim GetDataKodeTransfer, GetDataLokasi, GetDataKdBrg, GetDataNmBrg, GetDataBrgSN, GetDataJmlEstimasi, GetDataSatuanKecil, GetDataUrutOto, GetDataJumlahBags, GetDataBeratBags As String
    Dim GetDataSatuanBeratBags, GetDataSatuanBeratBesar As String

    ''Dim itemNoFaktur As Integer = 0
    'Dim itemKdSupplier As Integer = 0
    'Dim itemNmSupplier As Integer = 1
    'Dim itemNoSJ As Integer = 2
    'Dim itemIDEkspedisi As Integer = 3
    'Dim itemEkspedisi As Integer = 4
    'Dim itemSupir As Integer = 5
    'Dim itemPlatNomor As Integer = 6
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
    'Dim itemProsesLoading As Integer = 27

    Dim itemLokasi As Integer = 0
    Dim itemNmSupplier As Integer = 1
    Dim itemNoSJ As Integer = 2
    Dim itemSupir As Integer = 3
    Dim itemPlatNomor As Integer = 4
    Dim itemBruto As Integer = 5
    Dim itemNoLoading As Integer = 6
    Dim itemKdSupplier As Integer = 7

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


            ValueBarcode = ""

            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Label1.Text = "Display - List Transfer Stock"


            'If filter_tambahan = " timbang_masuk='Y'" Then
            '    Label1.Text = "Display - Kendaraan Masuk"
            'Else
            '    Label1.Text = "Display - Kendaraan Keluar"
            'End If

            Lv_List_Barang.Columns.Clear()

            Lv_List_Barang.Columns.Add("Kode Transfer", 120, HorizontalAlignment.Left).DisplayIndex = 0 '0
            '  Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_Lokasi, 130, HorizontalAlignment.Left) '1
            Lv_List_Barang.Columns.Add("SO Awal", 180, HorizontalAlignment.Left) '1
            Lv_List_Barang.Columns.Add("SO Akhir", 180, HorizontalAlignment.Left) '2
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_KodeBarang, 130, HorizontalAlignment.Left) '3
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_NamaBarang, 0, HorizontalAlignment.Left) '4
            Lv_List_Barang.Columns.Add("Total Input", 130, HorizontalAlignment.Center) '7
            Lv_List_Barang.Columns.Add("Satuan Input", 120, HorizontalAlignment.Center) '8
            Lv_List_Barang.Columns.Add("Total", 130, HorizontalAlignment.Center) '5
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_Satuan, 120, HorizontalAlignment.Center) '6
            Lv_List_Barang.Columns.Add("Lokasi RAK", 150, HorizontalAlignment.Left) '9
            Lv_List_Barang.Columns.Add("barangSn", 0, HorizontalAlignment.Left) '10

            Lv_List_Barang.View = View.Details


            'Menangkap semua inputan dari keyboard
            Me.KeyPreview = True

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
        '''Txt_ScanBarcode.Text = "1825003-0118L9B301124-T1X7VBQWEH"
    End Sub

    Private Sub Emi_Display_Transfer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

        'If e.KeyChar = Chr(13) Then
        '    If ValueBarcode <> "" Then
        '        Txt_ScanBarcode.Text = ValueBarcode.ToUpper
        '        ValueBarcode = ""

        '        If Txt_ScanBarcode.Text.Trim.Length <> 0 Then
        '            Btn_TimbangFloorScale_Click(Me, Nothing)
        '        End If
        '    Else
        '        Txt_ScanBarcode.Text = ""
        '    End If
        'Else
        '    If Char.IsLetterOrDigit(e.KeyChar) OrElse Char.IsSymbol(e.KeyChar) OrElse e.KeyChar = "-"c Then
        '        ValueBarcode &= e.KeyChar.ToString.Trim
        '    End If

        'End If

    End Sub

    'Private Sub Txt_ScanBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_ScanBarcode.KeyDown

    '    If e.KeyCode = Keys.Enter Then
    '        Btn_TimbangFloorScale_Click(Me, Nothing)
    '    End If
    'End Sub

    Private Sub Txt_ScanBarcode_TextChanged(sender As Object, e As EventArgs) Handles Txt_ScanBarcode.TextChanged
        '''Btn_TimbangFloorScale.PerformClick()
    End Sub

    Dim itemIDEkspedisi As Integer = 8
    Dim itemEkspedisi As Integer = 9
    Dim ItemNoTimbangan As Integer = 10
    Dim itemIsTimbangMasuk As Integer = 11
    Dim itemIsTimbangKeluar As Integer = 12

    Private Sub Btn_TimbangFloorScale_Click(sender As Object, e As EventArgs) Handles Btn_TimbangFloorScale.Click

        If Txt_ScanBarcode.Text.Trim.Length = 0 Then
            MessageBox.Show("Scan terlebih dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_ScanBarcode.Focus()
            Exit Sub
        End If

        Dim JumlahRequst As Double = 0
        Dim SisaRequest As Double = 0

        Try
            OpenConn()

            Dim SN As String = ""

            SQL = "select serial_number, Blok_SN from barang_sn "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Qr_Code+'-'+Kode_Unik_Berjalan = '" & Txt_ScanBarcode.Text & "' and jumlah<>0 "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Blok_SN")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("SN Pada Pallet di Block, Validasi di Batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        kosong()
                        Exit Sub
                    End If

                    SN = Dr("serial_number")
                End If
            End Using



            SQL = "Select a.no_faktur, a.lokasi, a.so_awal, a.so_tujuan, c.urut_Oto, b.kode_Barang, "
            SQL = SQL & "d.nama, b.Total, b.satuan, b.Satuan_Barang, c.serial_number_awal, "
            SQL = SQL & "c.jumlah, c.Jumlah_Bags, d.Berat_Bags, d.Satuan_Berat_Bags, c.Id_Wms_Tujuan, c.Warna, "

            SQL = SQL & "isnull((select x.Labeling_WMS_Position from View_Warehouse_Position x where "
            SQL = SQL & "x.Kode_Perusahaan = c.Kode_Perusahaan And x.Id_WMS_Warehouse_Position = c.Id_Wms_Awal), null) As Rak_Awal, "

            SQL = SQL & "isnull((select e.Jumlah "
            SQL = SQL & "from Emi_Material_Requisition q, Emi_Material_Requisition_Det w, Emi_Material_Requisition_Det_Convert e "
            SQL = SQL & "where q.Kode_Perusahaan = w.Kode_Perusahaan and w.Kode_Perusahaan = e.Kode_Perusahaan  "
            SQL = SQL & "and q.No_Faktur = w.No_Faktur "
            SQL = SQL & "and w.No_Faktur = e.No_Faktur and w.Urut_Oto = e.No_Urut_Det "
            SQL = SQL & "and q.Status is null "
            SQL = SQL & "and q.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.Urut_Oto = b.Urut_Material_Requisition_Convert "
            SQL = SQL & "), 0) as Jumlah_Kebutuhan, "

            SQL = SQL & "ISNULL(( select sum(w.jumlah) from tf_stock y, Tf_Stock_det z, Tf_Stock_det2 w "
            SQL = SQL & "where y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.urut_oto=z.urut_tf and (z.selesai is null or z.selesai='Y') and "
            SQL = SQL & "z.kode_Perusahaan=w.kode_perusahaan and z.no_faktur=w.no_faktur and z.urut_oto=w.Urut_Det and "
            SQL = SQL & "a.Kode_Perusahaan = y.Kode_Perusahaan and b.urut_material_requisition_convert = y.urut_material_requisition_convert  and y.Flag_Jenis_Request = 'PRODUKSI' ) "
            SQL = SQL & ", '0') as Total_TF "

            SQL = SQL & "From tf_stock_parent a, tf_stock b, tf_stock_det c, barang d Where "
            SQL = SQL & "a.kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And "
            SQL = SQL & "b.kode_Perusahaan = c.kode_Perusahaan And b.no_faktur = c.no_faktur And b.Urut_Oto = c.urut_TF "
            SQL = SQL & "And b.Kode_Barang=d.Kode_Barang And a.so_awal=d.kode_stock_Owner And b.kode_Perusahaan=d.Kode_Perusahaan "
            SQL = SQL & "And a.status Is null And b.Flag_Timbang ='Y' and c.selesai is null "
            SQL = SQL & "And c.Serial_Number_Awal = '" & SN & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    GetDataKodeTransfer = Dr("no_faktur")
                    GetDataLokasi = Dr("SO_Awal")
                    GetDataKdBrg = Dr("Kode_Barang")
                    GetDataNmBrg = Dr("nama")
                    GetDataBrgSN = Dr("Serial_Number_Awal")
                    GetDataJmlEstimasi = Format(Dr("jumlah"), "N2")
                    GetDataSatuanKecil = Dr("Satuan_Barang")
                    GetDataJumlahBags = Format(Dr("Jumlah_Bags"), "N2")
                    GetDataBeratBags = Format(Dr("Berat_Bags"), "N2")
                    GetDataSatuanBeratBags = Dr("Satuan_Berat_Bags")
                    GetDataSatuanBeratBesar = Dr("satuan")
                    GetDataUrutOto = Dr("urut_oto")
                    JumlahRequst = Format(Dr("Jumlah_Kebutuhan"), "N2")
                    SisaRequest = Format(Val(HilangkanTanda(Dr("Jumlah_Kebutuhan")) - Val(HilangkanTanda(Dr("Total_TF")))), "N2")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kode SN tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '--------------------------------------------------------------- 
        EMI_Timbang_Floor_Scale.kosong()
        EMI_Timbang_Floor_Scale.txtUrutOto.Text = GetDataUrutOto
        EMI_Timbang_Floor_Scale.txtKodeTransfer.Text = GetDataKodeTransfer
        EMI_Timbang_Floor_Scale.TxtKdBarang.Text = GetDataKdBrg
        EMI_Timbang_Floor_Scale.txt_lokasi.Text = GetDataLokasi
        EMI_Timbang_Floor_Scale.txt_barang.Text = GetDataNmBrg
        EMI_Timbang_Floor_Scale.txt_Barang_SN.Text = GetDataBrgSN
        EMI_Timbang_Floor_Scale.txt_Jml_Estimasi.Text = GetDataJmlEstimasi
        EMI_Timbang_Floor_Scale.TxtJumlahBags.Text = GetDataJumlahBags
        EMI_Timbang_Floor_Scale.TxtBeratBags.Text = GetDataBeratBags & " " & GetDataSatuanBeratBags
        EMI_Timbang_Floor_Scale.Txt_Berat_Bags_Bersih.Text = GetDataBeratBags
        EMI_Timbang_Floor_Scale.TxtSatuan_FloorScale.Text = GetDataSatuanBeratBesar

        EMI_Timbang_Floor_Scale.Txt_SatuanKecil.Text = GetDataSatuanKecil
        EMI_Timbang_Floor_Scale.TxtBarcode.Text = Txt_ScanBarcode.Text
        EMI_Timbang_Floor_Scale.Txt_Sisa_Request.Text = SisaRequest
        EMI_Timbang_Floor_Scale.CmbJenisTimbang.SelectedItem = "TRANSFER STOCK"

        EMI_Timbang_Floor_Scale.Btn_Refresh.Visible = False
        EMI_Timbang_Floor_Scale.UNIX.Visible = False


        EMI_Timbang_Floor_Scale.GetSisaTransfer()
        EMI_Timbang_Floor_Scale.ShowDialog()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing Then
            hasil = ""
        Else
            hasil = str
        End If

        Return hasil
    End Function

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)

        LvKodeTransfer = Lv_List_Barang.Items(NoIndex).SubItems(itemKodeTransfer).Text
        LvSoAwal = Lv_List_Barang.Items(NoIndex).SubItems(itemSOAwal).Text
        LvSoAkhir = Lv_List_Barang.Items(NoIndex).SubItems(itemSOAkhir).Text
        LvKodeBarang = Lv_List_Barang.Items(NoIndex).SubItems(itemKodeBarang).Text
        LvNamaBarang = Lv_List_Barang.Items(NoIndex).SubItems(itemNamaBarang).Text
        LvTotal = Lv_List_Barang.Items(NoIndex).SubItems(itemTotal).Text
        LvSatuan = Lv_List_Barang.Items(NoIndex).SubItems(itemSatuan).Text
        lv_JumlahInput = Lv_List_Barang.Items(NoIndex).SubItems(itemJumlahInput).Text
        Lv_SatuanInput = Lv_List_Barang.Items(NoIndex).SubItems(itemSatuanInput).Text
        LvRak = Lv_List_Barang.Items(NoIndex).SubItems(itemLokasiRak).Text
        LvSn = Lv_List_Barang.Items(NoIndex).SubItems(itemSN).Text
        LvSatuanBarang = Lv_List_Barang.Items(NoIndex).SubItems(itemSatuanBarang).Text


    End Sub




    Public Sub kosong()
        Txt_ScanBarcode.Text = ""
        Txt_ScanBarcode.Focus()
        Txt_ScanBarcode.Select()
        get_transfer_stock()
    End Sub

    Private Sub get_transfer_stock()
        Try
            OpenConn()

            Lv_List_Barang.Items.Clear()
            Lv_List_Barang.View = View.Details

            SQL = "Select a.no_faktur, a.lokasi, a.so_awal, a.so_tujuan, c.urut_Oto, b.kode_Barang, "
            SQL = SQL & "d.nama, b.Total, b.satuan, b.Satuan_Barang, c.serial_number_awal, "
            SQL = SQL & "c.jumlah, c.Jumlah_Bags, c.Id_Wms_Tujuan, c.Warna, "

            SQL = SQL & "isnull((select x.Labeling_WMS_Position from View_Warehouse_Position x where "
            SQL = SQL & "x.Kode_Perusahaan = c.Kode_Perusahaan And x.Id_WMS_Warehouse_Position = c.Id_Wms_Awal), null) As Rak_Awal, "

            SQL = SQL & "isnull(( select case when r.Flag_Dasar IS NULL THEN (c.Jumlah / r.Nilai) "
            SQL = SQL & "else (c.Jumlah * isnull(z.Nilai, 1)) end as Hasil "
            SQL = SQL & "from N_EMI_Master_Satuan r "
            SQL = SQL & "left join N_EMI_Master_Satuan z ON r.Kode_Perusahaan = z.Kode_Perusahaan and r.Kode_Barang = z.Kode_Barang and z.Flag_Dasar = 'Y' "
            SQL = SQL & "where r.Kode_Perusahaan = a.Kode_Perusahaan and r.Kode_Barang = b.Kode_Barang and r.Satuan = c.Satuan_Input "
            SQL = SQL & "), 0) as Jumlah_Input, isnull(c.Satuan_Input, '-') as Satuan_Input "

            SQL = SQL & "From tf_stock_parent a, tf_stock b, tf_stock_det c, barang d Where "
            SQL = SQL & "a.kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And "
            SQL = SQL & "b.kode_Perusahaan = c.kode_Perusahaan And b.no_faktur = c.no_faktur And b.Urut_Oto = c.urut_TF "
            SQL = SQL & "And b.Kode_Barang=d.Kode_Barang And a.so_awal=d.kode_stock_Owner And b.kode_Perusahaan=d.Kode_Perusahaan "
            SQL = SQL & "And a.status Is null And b.Flag_Timbang ='Y' and c.selesai is null "
            SQL = SQL & "order by a.no_faktur, a.tanggal,a.jam "

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem

                    Lvw = Lv_List_Barang.Items.Add(dr("no_faktur"))
                    '  Lvw.SubItems.Add(dr("lokasi"))
                    Lvw.SubItems.Add(dr("SO_Awal"))
                    Lvw.SubItems.Add(dr("so_tujuan"))
                    Lvw.SubItems.Add(dr("kode_barang"))
                    Lvw.SubItems.Add("X")

                    Lvw.SubItems.Add(Format(dr("Jumlah_Input"), "N0"))
                    Lvw.SubItems.Add(dr("Satuan_Input"))
                    Lvw.SubItems.Add(Format(dr("jumlah"), "N2"))
                    Lvw.SubItems.Add(dr("satuan"))

                    Lvw.SubItems.Add(dr("Rak_Awal"))
                    Lvw.SubItems.Add("X")
                    Lvw.SubItems.Add(dr("Satuan_Barang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles Lv_List_Barang.DoubleClick

        ''Get_Isi_ListView(Lv_List_Barang.FocusedItem.Index)

        ''EMI_Timbang_Floor_Scale.kosong()

        ''EMI_Timbang_Floor_Scale.txtKodeTransfer.Text = LvKodeTransfer
        ''EMI_Timbang_Floor_Scale.txt_lokasi.Text = LvSoAwal
        ''EMI_Timbang_Floor_Scale.txt_barang.Text = LvNamaBarang
        ''EMI_Timbang_Floor_Scale.TxtKdBarang.Text = LvKodeBarang
        ''EMI_Timbang_Floor_Scale.txt_Barang_SN.Text = LvSn
        ''EMI_Timbang_Floor_Scale.txt_Jml_Estimasi.Text = LvTotal
        ''EMI_Timbang_Floor_Scale.Txt_SatuanKecil.Text = LvSatuanBarang
        ''EMI_Timbang_Floor_Scale.CmbJenisTimbang.SelectedItem = "TRANSFER STOCK"

        ''EMI_Timbang_Floor_Scale.Btn_Refresh.Visible = False
        ''EMI_Timbang_Floor_Scale.UNIX.Visible = False

        ''EMI_Timbang_Floor_Scale.ShowDialog()

        'If asal = "Unloading_Barang" Then
        '    EMI_Timbang_Unloading.kosong()
        '    If LvBruto = "-" Or isTimbangMasuk = "Y" Then
        '        EMI_Timbang_Unloading.LblNo_Loading.Text = LvNoLoading
        '        EMI_Timbang_Unloading.Lbl_KodeSupplier.Text = LvKdSupplier
        '        EMI_Timbang_Unloading.Lbl_NamaSupplier.Text = LvNmSupplier
        '        EMI_Timbang_Unloading.Txt_Supplier.Text = LvKdSupplier + "(" + LvNmSupplier + ")"
        '        EMI_Timbang_Unloading.Lbl_IDEkspedisi.Text = LvIdEkspedisi
        '        EMI_Timbang_Unloading.Lbl_NmEkspedisi.Text = LvEkspedisi
        '        EMI_Timbang_Unloading.Lbl_NoSJ.Text = LvNoSJ
        '        'EMI_Timbang_Unloading.Txt_Ekspedisi.Text = LvEkspedisi
        '        EMI_Timbang_Unloading.Txt_Ekspedisi = LvEkspedisi
        '        EMI_Timbang_Unloading.Txt_Supir.Text = LvSupir
        '        EMI_Timbang_Unloading.Txt_PlatNomor.Text = LvPlatNomor
        '        ' EMI_Timbang_Unloading.Get_Timbang_Masuk()


        '        'EMI_Timbang_Unloading.Lbl_WaktuTimbangBruto.Visible = True
        '        'EMI_Timbang_Unloading.DTP_Bruto.Visible = True
        '        'EMI_Timbang_Unloading.Lbl_Timbang1.Visible = True
        '        'EMI_Timbang_Unloading.Txt_Timbang1.Visible = True
        '        'EMI_Timbang_Unloading.Txt_Timbang1.Enabled = True
        '        'EMI_Timbang_Unloading.Label21.Visible = True
        '        'EMI_Timbang_Unloading.Lbl_Bruto.Location = New Point(15, 307)
        '        'EMI_Timbang_Unloading.Txt_Bruto.Location = New Point(130, 307)
        '        'EMI_Timbang_Unloading.Label21.Location = New Point(274, 307)

        '        'EMI_Timbang_Unloading.Lbl_Timbang2.Visible = False
        '        'EMI_Timbang_Unloading.Lbl_Netto.Visible = False
        '        'EMI_Timbang_Unloading.Txt_Timbang2.Visible = False
        '        ' EMI_Timbang_Unloading.Txt_Timbang2.Enabled = False
        '        'EMI_Timbang_Unloading.Txt_Netto.Visible = False
        '        'EMI_Timbang_Unloading.Label8.Visible = False
        '        'EMI_Timbang_Unloading.Label23.Visible = False
        '        ' EMI_Timbang_Unloading.Lbl_WaktuTimbangTara.Visible = False
        '        'EMI_Timbang_Unloading.DTP_Tara.Visible = False

        '        EMI_Timbang_Unloading.filterDetailBarang = "and b.Flag_Timbang_Masuk is null and b.Flag_Sudah_Bongkar_Android is null and b.Flag_Timbang_Keluar is null"
        '        EMI_Timbang_Unloading.Get_DGV()
        '        EMI_Timbang_Unloading.Btn_Simpan.Tag = "&SimpanBruto"
        '        EMI_Timbang_Unloading.Btn_Simpan.Text = "&Simpan Bruto"

        '    Else
        '        EMI_Timbang_Unloading.LblNo_Loading.Text = LvNoLoading
        '        EMI_Timbang_Unloading.Txt_NoFaktur.Text = LvNoTimbangan
        '        'EMI_Timbang_Unloading.Txt_Ekspedisi.Text = LvEkspedisi
        '        EMI_Timbang_Unloading.Txt_Ekspedisi = LvEkspedisi
        '        EMI_Timbang_Unloading.Lbl_KodeSupplier.Text = LvKdSupplier
        '        EMI_Timbang_Unloading.Lbl_NamaSupplier.Text = LvNmSupplier
        '        EMI_Timbang_Unloading.Txt_Supplier.Text = LvKdSupplier + "(" + LvNmSupplier + ")"
        '        EMI_Timbang_Unloading.Txt_Supir.Text = LvSupir
        '        EMI_Timbang_Unloading.Txt_PlatNomor.Text = LvPlatNomor
        '        EMI_Timbang_Unloading.Txt_Timbang1.Text = LvBruto
        '        EMI_Timbang_Unloading.Lbl_NoSJ.Text = LvNoSJ
        '        EMI_Timbang_Unloading.ListView2.CheckBoxes = False

        '        'EMI_Timbang_Unloading.Lbl_WaktuTimbangBruto.Visible = True
        '        ' EMI_Timbang_Unloading.DTP_Bruto.Visible = True
        '        'EMI_Timbang_Unloading.Lbl_Timbang1.Visible = True
        '        'EMI_Timbang_Unloading.Txt_Timbang1.Visible = True
        '        'EMI_Timbang_Unloading.Txt_Timbang1.Enabled = True
        '        'EMI_Timbang_Unloading.Label21.Visible = True

        '        ' EMI_Timbang_Unloading.Lbl_Timbang2.Visible = True
        '        ' EMI_Timbang_Unloading.Lbl_Netto.Visible = True
        '        ' EMI_Timbang_Unloading.Txt_Timbang2.Visible = True
        '        '  EMI_Timbang_Unloading.Txt_Timbang2.Enabled = True
        '        '  EMI_Timbang_Unloading.Txt_Netto.Visible = True
        '        ' EMI_Timbang_Unloading.Label8.Visible = True
        '        ' EMI_Timbang_Unloading.Label23.Visible = True
        '        ' EMI_Timbang_Unloading.Lbl_WaktuTimbangTara.Visible = True
        '        '  EMI_Timbang_Unloading.DTP_Tara.Visible = True
        '        ' EMI_Timbang_Unloading.Lbl_Tara.Location = New Point(15, 307)
        '        'EMI_Timbang_Unloading.Txt_Tara.Location = New Point(130, 307)
        '        'EMI_Timbang_Unloading.Label8.Location = New Point(274, 307)


        '        ' EMI_Timbang_Unloading.Get_Timbang_Keluar()
        '        EMI_Timbang_Unloading.filterDetailBarang = "and b.flag_timbang_masuk='Y' and b.Flag_Sudah_Bongkar_Android='Y'"
        '        EMI_Timbang_Unloading.Get_DGV()
        '        ' EMI_Timbang_Unloading.Hitung_Netto()
        '        EMI_Timbang_Unloading.Btn_Simpan.Tag = "&SimpanTara"
        '        EMI_Timbang_Unloading.Btn_Simpan.Text = "&Simpan Tara"
        '        EMI_Timbang_Unloading.DataGridView1.Columns(4).Visible = True
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

    Private Sub Txt_ScanBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ScanBarcode.KeyPress
        If e.KeyChar = Chr(13) Then


            If Txt_ScanBarcode.Text.Trim.Length <> 0 Then
                Btn_TimbangFloorScale_Click(Me, Nothing)
            End If

        Else
            'If Char.IsLetterOrDigit(e.KeyChar) OrElse Char.IsSymbol(e.KeyChar) OrElse e.KeyChar = "-"c Then
            '    ValueBarcode &= e.KeyChar.ToString.Trim
            'End If

        End If
    End Sub
End Class