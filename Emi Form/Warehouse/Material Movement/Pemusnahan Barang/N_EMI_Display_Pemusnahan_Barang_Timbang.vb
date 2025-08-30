Public Class N_EMI_Display_Pemusnahan_Barang_Timbang

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
    Dim LvNamaBarang, LvTotal, LvSatuan, LvRak, LvSn, LvSatuanBarang As String

    Dim itemKodeTransfer As Integer = 0
    Dim itemSOAwal As Integer = 1
    Dim itemSOAkhir As Integer = 2
    Dim itemKodeBarang As Integer = 3
    Dim itemNamaBarang As Integer = 4
    Dim itemTotal As Integer = 5
    Dim itemSatuan As Integer = 6
    Dim itemLokasiRak As Integer = 7
    Dim itemSN As Integer = 8
    Dim itemSatuanBarang As Integer = 9

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
            Label1.Text = "Display - List Pengajuan Pemusnahan Barang"


            'If filter_tambahan = " timbang_masuk='Y'" Then
            '    Label1.Text = "Display - Kendaraan Masuk"
            'Else
            '    Label1.Text = "Display - Kendaraan Keluar"
            'End If

            Lv_List_Barang.Columns.Clear()

            Lv_List_Barang.Columns.Add("No Faktur", 120, HorizontalAlignment.Left).DisplayIndex = 0 '0
            Lv_List_Barang.Columns.Add("Lokasi", 180, HorizontalAlignment.Left) '1
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_KodeBarang, 130, HorizontalAlignment.Left) '2
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_NamaBarang, 0, HorizontalAlignment.Left) '3
            Lv_List_Barang.Columns.Add("Total", 130, HorizontalAlignment.Center) '4
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_Satuan, 120, HorizontalAlignment.Center) '5
            Lv_List_Barang.Columns.Add("barangSn", 0, HorizontalAlignment.Left) '6

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

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang Tidak Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, d.nama as Nama_Barang, c.Jumlah, c.Jumlah_Bags, b.Satuan, c.Serial_Number_Awal, "
            SQL = SQL & "b.Satuan_Barang, c.Urut_Oto, c.Warna, c.Id_Wms_Tujuan, d.Berat_Bags, d.Satuan_Berat_Bags "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c, Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.kode_barang "
            SQL = SQL & "and a.status is null and a.Flag_Validasi is null "
            SQL = SQL & "and b.Flag_Timbang = 'Y' and c.Selesai is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.Serial_Number_Awal = '" & SN & "' "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    GetDataKodeTransfer = Dr("no_faktur")
                    GetDataLokasi = Dr("Kode_Stock_Owner")
                    GetDataKdBrg = Dr("Kode_Barang")
                    GetDataNmBrg = Dr("Nama_Barang")
                    GetDataBrgSN = Dr("Serial_Number_Awal")
                    GetDataJmlEstimasi = Format(Dr("jumlah"), "N2")
                    GetDataSatuanKecil = Dr("Satuan_Barang")
                    GetDataJumlahBags = Format(Dr("Jumlah_Bags"), "N2")
                    GetDataBeratBags = Format(Dr("Berat_Bags"), "N2")
                    GetDataSatuanBeratBags = Dr("Satuan_Berat_Bags")
                    GetDataSatuanBeratBesar = Dr("satuan")
                    GetDataUrutOto = Dr("urut_oto")
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
        EMI_Timbang_Floor_Scale.CmbJenisTimbang.SelectedItem = "PEMUSNAHAN BARANG"

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

            SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, d.nama as Nama_Barang, c.Jumlah, b.Satuan, c.Serial_Number_Awal "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c, Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.kode_barang "
            SQL = SQL & "and a.status is null and a.Flag_Validasi is null "
            SQL = SQL & "and b.Flag_Timbang = 'Y' and c.Selesai is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by a.No_Faktur, a.Tanggal, a.Jam, b.Kode_Barang "

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_List_Barang.Items.Add(dr("No_Faktur"))
                    Lvw.SubItems.Add(dr("Kode_Stock_Owner"))
                    Lvw.SubItems.Add(dr("Kode_Barang"))
                    Lvw.SubItems.Add(dr("Nama_Barang"))
                    Lvw.SubItems.Add(Format(dr("Jumlah"), "N2"))
                    Lvw.SubItems.Add(dr("Satuan"))
                    Lvw.SubItems.Add(dr("Serial_Number_Awal"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


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