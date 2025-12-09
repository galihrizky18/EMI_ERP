
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Display_Data_Penawaran_Barang_Lain
    Dim arrcari, arrcari2 As New ArrayList
    Public MinHari As Integer = 300

    Public arrIdProvAsal, arrIdKabKotaAsal, arrIdKecAsal, arrIdKelAsal, arrIdLokasiAwal As New ArrayList
    Public arrIdProvTujuan, arrIdKabKotaTujuan, arrIdKecTujuan, arrIdKelTujuan, arrIdLokasiTujuan As New ArrayList
    Public arrCaraKirim, arrMediaKirim, arrUkuran, arrSatuan As New ArrayList

    Dim Jenis = "Master_Penawaran_Lain"
    Dim Jenis2 = "Master_Ongkir_Lain"

    Dim lv2NoFaktur, lv2NoPenawaran, lv2TglPenawaranHrg, lv2PeriodeAkhir, lv2KdSupplier, lv2NoUrut As String

    Dim item2NoFaktur As Integer = 0
    Dim item2NoPenawaran As Integer = 1
    Dim item2TglPenawaranHrg As Integer = 2
    Dim item2PeriodeAkhir As Integer = 3
    Dim item2KdSupplier As Integer = 4
    Dim item2NoUrut As Integer = 5

    Dim LvOngkirNmEkspedisi As String '0
    Dim LvOngkirNoPenawaran As String '1
    Dim LvOngkirTglPenawaranHrg As String '2
    Dim LvOngkirPeriodeAkhir As String '3
    Dim LvOngkirProvAsal As String '4
    Dim LvOngkirKabKotaAsal As String '5
    Dim LvOngkirKecAsal As String '6
    Dim LvOngkirKelAsal As String '7
    Dim LvOngkirLokasiAsal As String '8
    Dim LvOngkirProvTujuan As String '9
    Dim LvOngkirKabKotaTujuan As String '10
    Dim LvOngkirKecTujuan As String '11
    Dim LvOngkirKelTujuan As String '12
    Dim LvOngkirLokasiTujuan As String '13
    Dim LvOngkirCaraKirim As String '14
    Dim LvOngkirMediaKirim As String '15
    '
    Dim LvPanjang, LvLebar, LvTinggi, LvVolume As String '16,17,18,19
    Dim LvSatuanPanjang, LvSatuanVolume, LvBerat, LvSatuanBerat As String '20,21,22,23
    '
    'Dim LvOngkirUkuran As String '24
    Dim LvOngkirSatuan As String '25 ''24
    Dim LvOngkirHarga As String '26 ''25
    Dim LvOngkirNoUrut As String '27 ''26

    Dim itemNmEkspedisi As Integer = 0
    Dim itemNoPenawaran As Integer = 1
    Dim itemTglPenawaranHrg As Integer = 2
    Dim itemPeriodeAkhir As Integer = 3
    Dim itemProvAsal As Integer = 4
    Dim itemKabKotaAsal As Integer = 5
    Dim itemKecAsal As Integer = 6
    Dim itemKelAsal As Integer = 7
    Dim itemLokasiAsal As Integer = 8
    Dim itemProvTujuan As Integer = 9
    Dim itemKabKotaTujuan As Integer = 10
    Dim itemKecTujuan As Integer = 11
    Dim itemKelTujuan As Integer = 12
    Dim itemLokasiTujuan As Integer = 13
    Dim itemCaraKirim As Integer = 14
    Dim itemMediaKirim As Integer = 15
    '
    Dim itemPanjang As Integer = 16
    Dim itemLebar As Integer = 17
    Dim itemTinggi As Integer = 18
    Dim itemVolume As Integer = 19
    Dim itemSatuanPanjang As Integer = 20
    Dim itemSatuanVolume As Integer = 21
    Dim itemBerat As Integer = 22
    Dim itemSatuanBerat As Integer = 23
    '
    'Dim itemUkuran As Integer = 24
    Dim itemSatuan As Integer = 24

    Private Sub BtnOngkir_Refresh_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BtnOngkir_Refresh_Click_1(sender As Object, e As EventArgs) Handles BtnOngkir_Refresh.Click
        kosong()
    End Sub

    Private Sub AkhiriPenawaranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AkhiriPenawaranToolStripMenuItem.Click
        If Lv_Penawaran.Items.Count = 0 Or Lv_Penawaran.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Validasi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tny As String = MessageBox.Show(Base_Language.Lang_GLOBAL_Tanya_Validasi, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If tny = vbNo Then Exit Sub
        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction
            SQL = "SELECT Kode_Perusahaan From EMI_Master_Penawaran_Barang_Lain "
            SQL = SQL & "Where NoUrut = '" & Lv_Penawaran.FocusedItem.SubItems(item2NoUrut).Text & "' and "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dr.Close()
                    SQL = "Update EMI_Master_Penawaran_Barang_Lain set selesai='Y' "
                    SQL = SQL & "Where NoUrut = '" & Lv_Penawaran.FocusedItem.SubItems(item2NoUrut).Text & "' and "
                    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_GLOBAL_Tidak_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub AkhiriPenawaranToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AkhiriPenawaranToolStripMenuItem1.Click
        If Lv_Ongkir.Items.Count = 0 Or Lv_Ongkir.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Validasi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tny As String = MessageBox.Show(Base_Language.Lang_GLOBAL_Tanya_Validasi, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If tny = vbNo Then Exit Sub
        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction
            SQL = "SELECT Kode_Perusahaan From EMI_Master_Ongkir_Lain "
            SQL = SQL & "Where NoUrut = '" & Lv_Ongkir.FocusedItem.SubItems(itemNoUrut).Text & "' and "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dr.Close()
                    SQL = "Update EMI_Master_Ongkir_Lain set selesai='Y' "
                    SQL = SQL & "Where NoUrut = '" & Lv_Ongkir.FocusedItem.SubItems(itemNoUrut).Text & "' and "
                    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_GLOBAL_Tidak_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub BtnPenawaranTampil_Click(sender As Object, e As EventArgs) Handles BtnPenawaranTampil.Click
        Cari("Y")
    End Sub

    Private Sub BtnOngkirTampil_Click(sender As Object, e As EventArgs) Handles BtnOngkirTampil.Click
        Cari2("Y")
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Dim itemHarga As Integer = 25
    Dim itemNoUrut As Integer = 26

    Public cellKdBrg As Integer = 0
    Public cellNmBrg As Integer = 1
    Dim cellMinOrder As Integer = 2
    Public cellSatuan As Integer = 3
    Dim cellHrgSatuan As Integer = 4
    Dim cellCheckbox As Integer = 5

    Public Sub Get_Isi_Listview_Penawaran(ByVal NoIndex As Integer)
        lv2NoFaktur = Lv_Penawaran.Items(NoIndex).Text
        lv2NoPenawaran = Lv_Penawaran.Items(NoIndex).SubItems(item2NoPenawaran).Text
        lv2TglPenawaranHrg = Lv_Penawaran.Items(NoIndex).SubItems(item2TglPenawaranHrg).Text
        lv2PeriodeAkhir = Lv_Penawaran.Items(NoIndex).SubItems(item2PeriodeAkhir).Text
        lv2KdSupplier = Lv_Penawaran.Items(NoIndex).SubItems(item2KdSupplier).Text
        lv2NoUrut = Lv_Penawaran.Items(NoIndex).SubItems(item2NoUrut).Text
    End Sub

    Public Sub Get_Isi_Listview_Ongkir(ByVal No_Index As Integer)
        LvOngkirNmEkspedisi = Lv_Ongkir.Items(No_Index).Text ''
        LvOngkirNoPenawaran = Lv_Ongkir.Items(No_Index).SubItems(itemNoPenawaran).Text
        LvOngkirTglPenawaranHrg = Lv_Ongkir.Items(No_Index).SubItems(itemTglPenawaranHrg).Text
        LvOngkirPeriodeAkhir = Lv_Ongkir.Items(No_Index).SubItems(itemPeriodeAkhir).Text
        LvOngkirProvAsal = Lv_Ongkir.Items(No_Index).SubItems(itemProvAsal).Text
        LvOngkirKabKotaAsal = Lv_Ongkir.Items(No_Index).SubItems(itemKabKotaAsal).Text
        LvOngkirKecAsal = Lv_Ongkir.Items(No_Index).SubItems(itemKecAsal).Text
        LvOngkirKelAsal = Lv_Ongkir.Items(No_Index).SubItems(itemKelAsal).Text
        LvOngkirLokasiAsal = Lv_Ongkir.Items(No_Index).SubItems(itemLokasiAsal).Text
        LvOngkirProvTujuan = Lv_Ongkir.Items(No_Index).SubItems(itemProvTujuan).Text
        LvOngkirKabKotaTujuan = Lv_Ongkir.Items(No_Index).SubItems(itemKabKotaTujuan).Text
        LvOngkirKecTujuan = Lv_Ongkir.Items(No_Index).SubItems(itemKecTujuan).Text
        LvOngkirKelTujuan = Lv_Ongkir.Items(No_Index).SubItems(itemKelTujuan).Text
        LvOngkirLokasiTujuan = Lv_Ongkir.Items(No_Index).SubItems(itemLokasiTujuan).Text
        LvOngkirCaraKirim = Lv_Ongkir.Items(No_Index).SubItems(itemCaraKirim).Text
        LvOngkirMediaKirim = Lv_Ongkir.Items(No_Index).SubItems(itemMediaKirim).Text
        '
        LvPanjang = Lv_Ongkir.Items(No_Index).SubItems(itemPanjang).Text
        LvLebar = Lv_Ongkir.Items(No_Index).SubItems(itemLebar).Text
        LvTinggi = Lv_Ongkir.Items(No_Index).SubItems(itemTinggi).Text
        LvVolume = Lv_Ongkir.Items(No_Index).SubItems(itemVolume).Text
        LvSatuanPanjang = Lv_Ongkir.Items(No_Index).SubItems(itemSatuanPanjang).Text
        LvSatuanVolume = Lv_Ongkir.Items(No_Index).SubItems(itemSatuanVolume).Text
        LvBerat = Lv_Ongkir.Items(No_Index).SubItems(itemBerat).Text
        LvSatuanBerat = Lv_Ongkir.Items(No_Index).SubItems(itemSatuanBerat).Text
        '
        'LvOngkirUkuran = Lv_Ongkir.Items(No_Index).SubItems(itemUkuran).Text
        LvOngkirSatuan = Lv_Ongkir.Items(No_Index).SubItems(itemSatuan).Text
        LvOngkirHarga = Lv_Ongkir.Items(No_Index).SubItems(itemHarga).Text
        LvOngkirNoUrut = Lv_Ongkir.Items(No_Index).SubItems(itemNoUrut).Text
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

    Private Sub Get_Binding_Lokasi_Gudang()
        Try
            OpenConn()

            SQL = "SELECT * From Binding_Lokasi_Gudang_Lain "
            SQL = SQL & "Where Kode_stock_owner = '" & Lokasi & "' and "
            SQL = SQL & "Gudang_Default = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Lbl_BindingLokasiGudang.Text = Dr("Kode_Stock_Owner_Gudang")
                End If
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Master_Penawaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox3.Enabled = True
        TxtOngkir_Value.Enabled = True

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis2)

            Label1.Text = "Master Data - Penawaran Barang Lain"

            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            BtnPenawaranTampil.Text = "Tampil Semua"
            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom

            LvAutoCompleteSupplier.Location = New Point(685, 90)

            LvAutoCompleteSupplier.Hide()

            LvAutoCompleteSupplier.Columns.Clear()
            LvAutoCompleteSupplier.Columns.Add(Base_Language.Lang_Global_Supplier, 130, HorizontalAlignment.Left)
            LvAutoCompleteSupplier.Columns.Add(Base_Language.Lang_Global_Nama, 175, HorizontalAlignment.Left)
            'LvAutoCompleteSupplier.View = View.Details

            Lv_Penawaran.Columns.Clear()
            Lv_Penawaran.Columns.Add(Base_Language.Lang_Global_NoFaktur, 120, HorizontalAlignment.Left)
            Lv_Penawaran.Columns.Add(Base_Language.Lang_Penawaran_NoPenawaran, 190, HorizontalAlignment.Left)
            Lv_Penawaran.Columns.Add(Base_Language.Lang_global_Periode_Awal, 120, HorizontalAlignment.Center)
            Lv_Penawaran.Columns.Add(Base_Language.Lang_global_Periode_Akhir, 120, HorizontalAlignment.Center)
            Lv_Penawaran.Columns.Add(Base_Language.Lang_Global_Supplier, 0, HorizontalAlignment.Left)
            Lv_Penawaran.Columns.Add("NoUrut", 0, HorizontalAlignment.Right)
            Lv_Penawaran.Columns.Add(Base_Language.lang_global_Nama_Supplier, 220, HorizontalAlignment.Left).DisplayIndex = 1
            Lv_Penawaran.Columns.Add(Base_Language.Lang_Global_Aktif, 80, HorizontalAlignment.Center)
            Lv_Penawaran.Columns.Add("sisa hari", 120, HorizontalAlignment.Center)
            'Lv_Penawaran.View = View.Details

            Lv_Penawaran_Detail.Columns.Clear()
            Lv_Penawaran_Detail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 140, HorizontalAlignment.Left)
            Lv_Penawaran_Detail.Columns.Add(Base_Language.Lang_Global_Nama, 300, HorizontalAlignment.Left)
            Lv_Penawaran_Detail.Columns.Add(Base_Language.Lang_Global_MinOrder, 120, HorizontalAlignment.Center)
            Lv_Penawaran_Detail.Columns.Add(Base_Language.Lang_Global_Satuan, 110, HorizontalAlignment.Center)
            Lv_Penawaran_Detail.Columns.Add(Base_Language.Lang_Global_HargaSatuan, 150, HorizontalAlignment.Right)
            'Lv_Penawaran_Detail.View = View.Details

            'SECTION - ONGKIR
            Lv_AutoCompleteNmEkspedisi.Location = New Point(235, 55)
            Lv_AutoCompleteNmEkspedisi.Hide()

            Lv_AutoCompleteNmEkspedisi.Columns.Clear()
            Lv_AutoCompleteNmEkspedisi.Columns.Add("Id", 0, HorizontalAlignment.Left) '0
            Lv_AutoCompleteNmEkspedisi.Columns.Add("Kode", 80, HorizontalAlignment.Left) '1
            Lv_AutoCompleteNmEkspedisi.Columns.Add(Base_Language.Lang_Ongkir_NmEkspedisi, 170, HorizontalAlignment.Left) '2
            'Lv_AutoCompleteNmEkspedisi.View = View.Details

            'Lbl_OngkirJudul.Text = Base_Language.Lang_Ongkir_Lain_Judul 

            LblOngkir_Kolom.Text = Base_Language.Lang_Global_Kolom
            BtnOngkir_Cari.Text = Base_Language.Lang_Global_Cari
            BtnOngkir_Refresh.Text = Base_Language.Lang_Global_Refresh
            BtnOngkirTampil.Text = "Tampil Semua"

            Lv_Ongkir.Columns.Clear()
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Ongkir_NmEkspedisi, 120, HorizontalAlignment.Left) '0
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Penawaran_NoPenawaran, 120, HorizontalAlignment.Left) '1
            Lv_Ongkir.Columns.Add(Base_Language.Lang_global_Periode_Awal, 120, HorizontalAlignment.Center) '2
            Lv_Ongkir.Columns.Add(Base_Language.Lang_global_Periode_Akhir, 120, HorizontalAlignment.Center) '3
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Ongkir_ProvAsal, 150, HorizontalAlignment.Left) '4
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Ongkir_KabKotaAsal, 200, HorizontalAlignment.Left) '5
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Asal + " " + Base_Language.Lang_Global_Kecamatan, 170, HorizontalAlignment.Left) '5
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Asal + " " + Base_Language.Lang_Global_Kelurahan, 170, HorizontalAlignment.Left) '7
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Lokasi_Awal, 100, HorizontalAlignment.Left) '8
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Ongkir_ProvTujuan, 150, HorizontalAlignment.Left) '9
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Ongkir_KabKotaTujuan, 200, HorizontalAlignment.Left) '10
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Tujuan + " " + Base_Language.Lang_Global_Kelurahan, 170, HorizontalAlignment.Left) '11
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Tujuan + " " + Base_Language.Lang_Global_Kelurahan, 170, HorizontalAlignment.Left) '12
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Lokasi_Tujuan, 150, HorizontalAlignment.Left) '13
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Cara_Kirim, 150, HorizontalAlignment.Center) '14
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Ongkir_MediaKirim, 150, HorizontalAlignment.Center) '15
            '
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_P & " x " & Base_Language.Lang_Global_L & " x " & Base_Language.Lang_Global_T, 120, HorizontalAlignment.Left) '16
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Lebar, 0, HorizontalAlignment.Left) '17
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Tinggi, 0, HorizontalAlignment.Left) '18
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Satuan_Panjang, 0, HorizontalAlignment.Center) '20
            Lv_Ongkir.Columns.Add("Volume", 120, HorizontalAlignment.Left) '19
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_SatuanVolume, 0, HorizontalAlignment.Center) '21
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Berat, 100, HorizontalAlignment.Left) '22
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_SatuanBerat, 0, HorizontalAlignment.Center) '23
            '
            'Lv_Ongkir.Columns.Add(Base_Language.Lang_Ongkir_Lain_Ukuran, 100, HorizontalAlignment.Center) '24
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center) '25
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Ongkir_Hrg, 100, HorizontalAlignment.Right) '26
            Lv_Ongkir.Columns.Add("NoUrut", 0, HorizontalAlignment.Right) '27
            Lv_Ongkir.Columns.Add(Base_Language.Lang_Global_Aktif, 80, HorizontalAlignment.Center)
            Lv_Ongkir.Columns.Add("sisa hari", 120, HorizontalAlignment.Center)
            'Lv_Ongkir.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub


    Public Sub kosong()

        Get_Binding_Lokasi_Gudang()

        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add(Base_Language.Lang_Global_NoFaktur) : arrcari.Add("a.No_Faktur")
        ComboBox1.Items.Add("No Penawaran") : arrcari.Add("a.No_Penawaran")
        TextBox3.Text = ""

        CmbpenawaranAktif.Items.Clear()
        CmbpenawaranAktif.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)
        CmbpenawaranAktif.Items.Add("Y")
        CmbpenawaranAktif.Items.Add("T")
        CmbpenawaranAktif.SelectedIndex = 0




        CmbOngkir_Kolom.Items.Clear() : arrcari2.Clear()
        CmbOngkir_Kolom.Items.Add("Nama Expedisi") : arrcari2.Add("a.Nama_Ekspedisi")
        CmbOngkir_Kolom.Items.Add("No Penawaran") : arrcari2.Add("a.No_Penawaran")
        TxtOngkir_Value.Text = ""

        CmbOngkirAktif.Items.Clear()
        CmbOngkirAktif.Items.Add("Seluruh")
        CmbOngkirAktif.Items.Add("Y")
        CmbOngkirAktif.Items.Add("T")
        CmbOngkirAktif.SelectedIndex = 0

        Cari("Y")
        Cari2("Y")
        'DgvMaster_Penawaran.Cell(cellCheckbox).Value = "True"
        'DgvMaster_Penawaran.Rows(0).Cells(cellCheckbox).Value = True
    End Sub



    Private Sub Txt_Supplier_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Down Then LvAutoCompleteSupplier.Focus()
    End Sub

    Private Sub Cari(ByVal semua As String)
        ' kosong()

        get_jam()

        Try
            OpenConn()

            Lv_Penawaran.Items.Clear()
            Lv_Penawaran_Detail.Items.Clear()
            SQL = ";with cte as ( "
            SQL = SQL & "Select a.kode_Perusahaan, a.no_faktur, a.no_penawaran,Tgl_Penawaran_Hrg,Periode_Akhir_Penawaran,a.Kode_Supplier,b.nama, a.noUrut, "
            SQL = SQL & "(case "
            SQL = SQL & "when a.Selesai='Y' then 'T' "
            SQL = SQL & "when '" & Format(tgl_skg, "yyyy-MM-dd") & "' not between a.Tgl_Penawaran_Hrg And a.Periode_Akhir_Penawaran then 'T' "
            SQL = SQL & "else 'Y' end "
            SQL = SQL & ") as aktif, datediff(day, '" & Format(tgl_skg, "yyyy-MM-dd") & "',a.Periode_Akhir_Penawaran) as sisa_hari "
            SQL = SQL & "From EMI_Master_Penawaran_Barang_Lain a, suppliers b "
            SQL = SQL & "Where a.Kode_Perusahaan =b.kode_Perusahaan and a.Kode_supplier=b.kode_supplier "
            SQL = SQL & ") select * from cte a where Kode_Perusahaan = '" & KodePerusahaan & "' "

            If CmbpenawaranAktif.SelectedIndex = 1 Then
                SQL = SQL & "and aktif ='Y' "
            ElseIf CmbpenawaranAktif.SelectedIndex = 2 Then
                SQL = SQL & "and aktif ='T' "
            End If


            If semua = "T" Then
                SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & "order by Tgl_Penawaran_Hrg "
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_Penawaran.Items.Add(dr("No_Faktur"))
                    Lvw.SubItems.Add(dr("No_Penawaran"))
                    Lvw.SubItems.Add(Format(dr("Tgl_Penawaran_Hrg"), "dd MMM yyyy"))
                    Lvw.SubItems.Add(Format(dr("Periode_Akhir_Penawaran"), "dd MMM yyyy"))
                    Lvw.SubItems.Add(dr("Kode_Supplier"))
                    Lvw.SubItems.Add(dr("NoUrut"))
                    Lvw.SubItems.Add(dr("nama"))
                    Lvw.SubItems.Add(dr("aktif"))
                    If dr("aktif") = "Y" Then
                        Lvw.SubItems.Add(dr("sisa_hari") & " hari")
                    Else
                        Lvw.SubItems.Add("-")
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

    Private Sub Btn_Cari_Click_1(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub


    Private Sub BtnOngkir_Cari_Click(sender As Object, e As EventArgs) Handles BtnOngkir_Cari.Click
        If CmbOngkir_Kolom.Text.Trim.Length = 0 Then Exit Sub
        If TxtOngkir_Value.Text.Trim.Length = 0 Then Exit Sub

        Cari2("T")
    End Sub

    Private Sub Cari2(ByVal semua2 As String)
        get_jam()
        Try
            OpenConn()

            Lv_Ongkir.Items.Clear()

            SQL = ";with cte as ( "
            SQL = SQL & "Select a.kode_Perusahaan, b.Nama_Ekspedisi, a.No_Penawaran, a.Tgl_Penawaran_Hrg, a.Periode_Akhir_Penawaran, "
            SQL = SQL & "c.nama_provinsi as provAsal, d.nama_kabupaten_kota as kabKotaAsal, "
            SQL = SQL & "e.nama_kecamatan as kecAsal, f.nama_kelurahan as kelAsal, a.Lokasi_Asal, "
            SQL = SQL & "c.nama_provinsi as provTujuan, d.nama_kabupaten_kota as kabKotaTujuan, "
            SQL = SQL & "e.nama_kecamatan as kecTujuan, f.nama_kelurahan as kelTujuan, a.Lokasi_Tujuan, "
            SQL = SQL & "g.Keterangan as ketCaraKirim, h.Keterangan as ketMediaKirim, "
            SQL = SQL & "a.P, a.L, a.T, a.Volume, "
            SQL = SQL & "a.Satuan_Panjang, a.Satuan_Volume, "
            SQL = SQL & "a.Berat, a.Satuan_Berat, "
            SQL = SQL & "i.Satuan as satuanPengiriman, a.Harga, a.NoUrut, "

            SQL = SQL & "(case "
            SQL = SQL & "when a.Selesai='Y' then 'T' "
            SQL = SQL & "when '" & Format(tgl_skg, "yyyy-MM-dd") & "' not between a.Tgl_Penawaran_Hrg And a.Periode_Akhir_Penawaran then 'T' "
            SQL = SQL & "else 'Y' end "
            SQL = SQL & ") as aktif, datediff(day, '" & Format(tgl_skg, "yyyy-MM-dd") & "',a.Periode_Akhir_Penawaran) as sisa_hari "

            SQL = SQL & "From EMI_Master_Ongkir_Lain a, EMI_Master_Ekspedisi b, tbl_provinsi c, "
            SQL = SQL & "tbl_kabupaten_kota d, tbl_kecamatan e, tbl_kelurahan f, "
            SQL = SQL & "EMI_Cara_Kirim g, EMI_Media_Kirim h,  EMI_Satuan_Pengiriman i "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = g.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = h.Kode_Perusahaan and a.Kode_Perusahaan = i.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Ekspedisi = b.Id_Ekspedisi and a.Id_Provinsi_Asal = c.id_provinsi "
            SQL = SQL & "and a.Id_Kab_Kota_Asal = d.id_kabupaten_kota and a.Id_Kecamatan_Asal = e.id_kecamatan "
            SQL = SQL & "and a.Id_Kelurahan_Asal = f.id_kelurahan and a.Id_Cara_Kirim = g.Id_Cara_Kirim "
            SQL = SQL & "and a.Id_Media_Kirim = h.Id_Media_Kirim and  a.Id_Satuan = i.Id_Satuan_Pengiriman "
            SQL = SQL & ") select * from cte a where Kode_Perusahaan = '" & KodePerusahaan & "' "

            If CmbOngkirAktif.SelectedIndex = 1 Then
                SQL = SQL & "and aktif ='Y' "
            ElseIf CmbOngkirAktif.SelectedIndex = 2 Then
                SQL = SQL & "and aktif ='T' "
            End If

            If semua2 = "T" Then
                SQL = SQL & "and " & arrcari2.Item(CmbOngkir_Kolom.SelectedIndex) & " like '%" & TxtOngkir_Value.Text & "%' "
                SQL = SQL & "order by " & arrcari2.Item(CmbOngkir_Kolom.SelectedIndex) & " "
            Else
                SQL = SQL & "Order By a.NoUrut "
            End If

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_Ongkir.Items.Add(dr("Nama_Ekspedisi")) '0
                    Lvw.SubItems.Add(dr("No_Penawaran")) '1
                    Lvw.SubItems.Add(Format(dr("Tgl_Penawaran_Hrg"), "dd MMM yyyy")) '2
                    Lvw.SubItems.Add(Format(dr("Periode_Akhir_Penawaran"), "dd MMM yyyy")) '3
                    Lvw.SubItems.Add(dr("provAsal")) '4
                    Lvw.SubItems.Add(dr("kabKotaAsal")) '5
                    Lvw.SubItems.Add(dr("kecAsal")) '6
                    Lvw.SubItems.Add(dr("kelAsal")) '7
                    Lvw.SubItems.Add(dr("Lokasi_Asal")) '8
                    Lvw.SubItems.Add(dr("provTujuan")) '9
                    Lvw.SubItems.Add(dr("kabKotaTujuan")) '10
                    Lvw.SubItems.Add(dr("kecTujuan")) '11
                    Lvw.SubItems.Add(dr("kelTujuan")) '12
                    Lvw.SubItems.Add(dr("Lokasi_Tujuan")) '13
                    Lvw.SubItems.Add(dr("ketCaraKirim")) '14
                    Lvw.SubItems.Add(dr("ketMediaKirim")) '15
                    '
                    Lvw.SubItems.Add(dr("P") & " x " & dr("L") & " x " & dr("T") & " " & dr("Satuan_Panjang")) '16
                    Lvw.SubItems.Add(dr("L")) '17
                    Lvw.SubItems.Add(dr("T")) '18
                    Lvw.SubItems.Add(dr("Satuan_Panjang")) '20
                    Lvw.SubItems.Add(dr("Volume") & " " & dr("Satuan_Volume")) '19
                    Lvw.SubItems.Add(dr("Satuan_Volume")) '21
                    Lvw.SubItems.Add(Format(dr("Berat"), "N2") & " " & dr("Satuan_Berat")) '22
                    Lvw.SubItems.Add(dr("Satuan_Berat")) '23
                    '
                    'Lvw.SubItems.Add(dr("Ukuran")) '24
                    Lvw.SubItems.Add(dr("satuanPengiriman")) '25
                    Lvw.SubItems.Add(Format(dr("Harga"), "N0")) '26
                    Lvw.SubItems.Add(dr("NoUrut")) '27
                    Lvw.SubItems.Add(dr("aktif"))
                    If dr("aktif") = "Y" Then
                        Lvw.SubItems.Add(dr("sisa_hari") & " hari")
                    Else
                        Lvw.SubItems.Add("-")
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



    Private Sub Btn_Refresh_Click_1(sender As Object, e As EventArgs)
        kosong()
    End Sub



    Private Sub Lv_Penawaran_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles Lv_Penawaran.SelectedIndexChanged
        If Lv_Penawaran.Items.Count = 0 Then Exit Sub
        Get_Master_Penawaran_Detail()
    End Sub

    Private Sub Get_Master_Penawaran_Detail()
        Try
            OpenConn()

            Lv_Penawaran_Detail.Items.Clear()
            SQL = "Select a.Kode_Barang, b.Nama, a.Min_Order, a.Satuan, a.Harga_Satuan "
            SQL = SQL & "From EMI_Master_Penawaran_Detail_Barang_Lain a, Barang_Lain b "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Barang = b.Kode_Barang and b.Kode_Stock_Owner = '" & Lbl_BindingLokasiGudang.Text & "' and "
            SQL = SQL & "a.No_Faktur = '" & Lv_Penawaran.FocusedItem.Text & "' "

            Dim lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        lvw = Lv_Penawaran_Detail.Items.Add(.Rows(i).Item("Kode_Barang"))
                        lvw.SubItems.Add(.Rows(i).Item("Nama"))
                        lvw.SubItems.Add(Format(.Rows(i).Item("Min_Order"), "N0"))
                        lvw.SubItems.Add(.Rows(i).Item("Satuan"))
                        lvw.SubItems.Add(Format(.Rows(i).Item("Harga_Satuan"), "N2"))
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Lv_Penawaran_Detail.Items.Clear()
            Exit Sub
        End Try

    End Sub



End Class