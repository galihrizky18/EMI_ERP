'Imports Org.BouncyCastle.Utilities
Imports System.Runtime.Remoting.Metadata.W3cXsd2001
Imports Microsoft.VisualBasic
Public Class EMI_Pelunasan_Biaya_Import_By_Perusahaan_Lokal
    Dim JT As String
    Dim ArrNP1 As New ArrayList
    Dim ArrNP2 As New ArrayList
    Dim ArrMataUangRek As New ArrayList

    Dim LvFak As String
    Dim LvPO As String
    Dim LvKP As String
    Dim LvNP As String
    Dim LvKdKategori As String
    Dim LvNmKategori As String
    Dim LvMT As String
    Dim LvJml As String
    Dim LvTambahan As String
    Dim LvTotal As String
    Dim LvPersenPPN As String
    Dim LvPersenPPH As String
    Dim LvNilaiPPN As String
    Dim LvNilaiPPH As String
    Dim LvLokasi As String
    Dim LvJns As String
    Dim LvJnsBiaya As String
    Dim LvKurs As String
    Dim LvKursLama As String
    Dim LvKursLamaTot As String
    Dim LvKursBaru As String
    Dim LvKursBaruTot As String

    Dim lvJenis As String

    Dim CellFak As Integer = 0
    Dim CellPO As Integer = 1
    Dim CellKP As Integer = 2
    Dim CellNP As Integer = 3
    Dim CellKdKategori As Integer = 4
    Dim CellNmKategori As Integer = 5
    Dim CellMT As Integer = 6
    Dim CellJml As Integer = 7
    Dim CellTambahan As Integer = 8
    Dim CellKursLama As Integer = 9
    Dim CellKursLamaTot As Integer = 10
    Dim CellKursBaru As Integer = 11
    Dim CellKursBaruTot As Integer = 12
    Dim CellTotal As Integer = 13
    Dim CellPersenPPN As Integer = 14
    Dim CellPersenPPH As Integer = 15
    Dim CellNilaiPPN As Integer = 16
    Dim CellNilaiPPH As Integer = 17
    Dim CellLokasi As Integer = 18
    Dim CellJns As Integer = 19
    Dim CellJnsBiaya As Integer = 20

    Dim CellFakMU As Integer = 0
    Dim CellIDRencanaMU As Integer = 1
    Dim CellKeteranganMU As Integer = 2
    Dim CellTglMU As Integer = 3
    Dim CellKdPerusahaanBiayaImportMU As Integer = 4
    Dim CellNmPerusahaanMU As Integer = 5
    Dim CellKdKategoriMU As Integer = 6
    Dim CellKeteranganKategoriMU As Integer = 7
    Dim CellMataUangMU As Integer = 8
    Dim CellNilaiMU As Integer = 9
    Dim CellSdhByrMU As Integer = 10
    Dim CellPPNMU As Integer = 11
    Dim CellPPHMU As Integer = 12
    Dim CellLokasiU As Integer = 13
    Dim CellJnsU As Integer = 14
    Dim CellSisa As Integer = 15
    Dim CellKurs As Integer = 16
    Dim CellJenis As Integer = 17

    Dim LvFakMU As String
    Dim LvIDRencanaMU As String
    Dim LvKeteranganMU As String
    Dim LvTglMU As String
    Dim LvKdPerusahaanBiayaImportMU As String
    Dim LvNmPerusahaanMU As String
    Dim LvKdKategoriMU As String
    Dim LvKeteranganKategoriMU As String
    Dim LvMataUangMU As String
    Dim LvNilaiMU As String
    Dim LvSdhByrMU As String
    Dim LvPPNMU As String
    Dim LvPPHMU As String
    Dim LvLokasiU As String
    Dim LvJnsU As String

    Dim arrCrByr1, ArrAkunCB1, ArrAkunRek1 As New ArrayList
    Dim arrCrByr2, ArrAkunCB2 As New ArrayList
    Dim varMataUang As String
    Dim kursUangBaru As Double
    Dim checkSisaHutang As String
    Dim no_fakturDeposit As String
    Dim no_fakturPengajuan, no_fakturToken As String
    Dim x_alamat, x_Kota, x_negara, x_telp As String

    Private Sub Validasi_Pemb_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Validasi_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        ListViewMT1.Columns.Add("No Faktur", 110, HorizontalAlignment.Left) '0
        ListViewMT1.Columns.Add("ID Rencana", 0, HorizontalAlignment.Center) '1
        ListViewMT1.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '2
        ListViewMT1.Columns.Add("Tgl", 0, HorizontalAlignment.Center) '3
        ListViewMT1.Columns.Add("Kode Perusahaan", 0, HorizontalAlignment.Left) '4
        ListViewMT1.Columns.Add("Nama Perusahaan", 250, HorizontalAlignment.Left) '5
        ListViewMT1.Columns.Add("Kode Kategori", 0, HorizontalAlignment.Left) '6
        ListViewMT1.Columns.Add("Kategori", 200, HorizontalAlignment.Left) '7
        ListViewMT1.Columns.Add("Mata Uang", 80, HorizontalAlignment.Center) '8
        ListViewMT1.Columns.Add("Total", 120, HorizontalAlignment.Right) '9
        ListViewMT1.Columns.Add("Dibayar", 120, HorizontalAlignment.Right) '10
        ListViewMT1.Columns.Add("PPN", 0, HorizontalAlignment.Right) '11
        ListViewMT1.Columns.Add("PPH", 0, HorizontalAlignment.Right) '12
        ListViewMT1.Columns.Add("Lokasi", 100, HorizontalAlignment.Left).DisplayIndex = 1 '13
        ListViewMT1.Columns.Add("Jenis Form", 0, HorizontalAlignment.Right) '14
        ListViewMT1.Columns.Add("Sisa", 120, HorizontalAlignment.Right) '15
        ListViewMT1.Columns.Add("Kurs Lama", 0, HorizontalAlignment.Right) '16
        ListViewMT1.Columns.Add("Jenis", 0, HorizontalAlignment.Right) '17
        ListViewMT1.View = View.Details

        ListViewMT11.Columns.Add("No Faktur", 140, HorizontalAlignment.Left)  '0
        ListViewMT11.Columns.Add("Tgl", 0, HorizontalAlignment.Center) '1
        ListViewMT11.Columns.Add("Kode Perusahaan", 0, HorizontalAlignment.Left) '2
        ListViewMT11.Columns.Add("Nama Perusahaan", 250, HorizontalAlignment.Left) '3
        ListViewMT11.Columns.Add("Kode Kategori", 0, HorizontalAlignment.Left) '4
        ListViewMT11.Columns.Add("Kategori Perusahaan", 200, HorizontalAlignment.Left) '5
        ListViewMT11.Columns.Add("Mata Uang", 80, HorizontalAlignment.Center) '6
        ListViewMT11.Columns.Add("Jumlah", 130, HorizontalAlignment.Right) '7
        ListViewMT11.Columns.Add("Nilai Tambahan", 130, HorizontalAlignment.Right) '8
        ListViewMT11.Columns.Add("Kurs Lama", 130, HorizontalAlignment.Right) '9
        ListViewMT11.Columns.Add("Total Kurs Lama", 150, HorizontalAlignment.Right) '10
        ListViewMT11.Columns.Add("Kurs Baru", 150, HorizontalAlignment.Right) '11
        ListViewMT11.Columns.Add("Total Kurs Baru", 150, HorizontalAlignment.Right) '12
        ListViewMT11.Columns.Add("Total", 130, HorizontalAlignment.Right) '13
        ListViewMT11.Columns.Add("PPN", 0, HorizontalAlignment.Right) '14
        ListViewMT11.Columns.Add("PPH", 0, HorizontalAlignment.Right) '15
        ListViewMT11.Columns.Add("Nilai PPN", 90, HorizontalAlignment.Right) '16
        ListViewMT11.Columns.Add("Nilai PPH", 90, HorizontalAlignment.Right) '17
        ListViewMT11.Columns.Add("Lokasi", 120, HorizontalAlignment.Left).DisplayIndex = 1 '18
        ListViewMT11.Columns.Add("Jenis Form", 0, HorizontalAlignment.Right) '19
        ListViewMT11.Columns.Add("JenisBiaya", 0, HorizontalAlignment.Right) '20
        ListViewMT11.View = View.Details

        ListView3.Columns.Add("No Rekening", 100, HorizontalAlignment.Left)
        ListView3.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        ListView3.Columns.Add("Bank", 100, HorizontalAlignment.Left)
        ListView3.Location = New Point(1450, 116)
        ListView3.Visible = False

        'TextBox6.Enabled = False
        Kosong()
        TxtFaktur.Focus()

    End Sub

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvFak = ListViewMT11.Items(No_Index).SubItems(CellFak).Text
        LvPO = ListViewMT11.Items(No_Index).SubItems(CellPO).Text
        LvKP = ListViewMT11.Items(No_Index).SubItems(CellKP).Text
        LvNP = ListViewMT11.Items(No_Index).SubItems(CellNP).Text
        LvKdKategori = ListViewMT11.Items(No_Index).SubItems(CellKdKategori).Text
        LvNmKategori = ListViewMT11.Items(No_Index).SubItems(CellNmKategori).Text
        LvMT = ListViewMT11.Items(No_Index).SubItems(CellMT).Text
        LvJml = ListViewMT11.Items(No_Index).SubItems(CellJml).Text
        LvTambahan = ListViewMT11.Items(No_Index).SubItems(CellTambahan).Text
        LvTotal = ListViewMT11.Items(No_Index).SubItems(CellTotal).Text
        LvPersenPPN = ListViewMT11.Items(No_Index).SubItems(CellPersenPPN).Text
        LvPersenPPH = ListViewMT11.Items(No_Index).SubItems(CellPersenPPH).Text
        LvNilaiPPN = ListViewMT11.Items(No_Index).SubItems(CellNilaiPPN).Text
        LvNilaiPPH = ListViewMT11.Items(No_Index).SubItems(CellNilaiPPH).Text
        LvLokasi = ListViewMT11.Items(No_Index).SubItems(CellLokasi).Text
        LvJns = ListViewMT11.Items(No_Index).SubItems(CellJns).Text
        LvJnsBiaya = ListViewMT11.Items(No_Index).SubItems(CellJnsBiaya).Text

        LvKursLama = ListViewMT11.Items(No_Index).SubItems(CellKursLama).Text
        LvKursLamaTot = ListViewMT11.Items(No_Index).SubItems(CellKursLamaTot).Text
        LvKursBaru = ListViewMT11.Items(No_Index).SubItems(CellKursBaru).Text
        LvKursBaruTot = ListViewMT11.Items(No_Index).SubItems(CellKursBaruTot).Text

    End Sub

    Private Sub Get_Isi_Listview1(ByVal No_Index As Integer)
        LvFakMU = ListViewMT1.Items(No_Index).SubItems(CellFakMU).Text
        LvIDRencanaMU = ListViewMT1.Items(No_Index).SubItems(CellIDRencanaMU).Text
        LvKeteranganMU = ListViewMT1.Items(No_Index).SubItems(CellKeteranganMU).Text
        LvTglMU = ListViewMT1.Items(No_Index).SubItems(CellTglMU).Text
        LvKdPerusahaanBiayaImportMU = ListViewMT1.Items(No_Index).SubItems(CellKdPerusahaanBiayaImportMU).Text
        LvNmPerusahaanMU = ListViewMT1.Items(No_Index).SubItems(CellNmPerusahaanMU).Text
        LvKdKategoriMU = ListViewMT1.Items(No_Index).SubItems(CellKdKategoriMU).Text
        LvKeteranganKategoriMU = ListViewMT1.Items(No_Index).SubItems(CellKeteranganKategoriMU).Text
        LvMataUangMU = ListViewMT1.Items(No_Index).SubItems(CellMataUangMU).Text
        LvNilaiMU = ListViewMT1.Items(No_Index).SubItems(CellNilaiMU).Text
        LvSdhByrMU = ListViewMT1.Items(No_Index).SubItems(CellSdhByrMU).Text
        LvPPNMU = ListViewMT1.Items(No_Index).SubItems(CellPPNMU).Text
        LvPPHMU = ListViewMT1.Items(No_Index).SubItems(CellPPHMU).Text
        LvLokasiU = ListViewMT1.Items(No_Index).SubItems(CellLokasiU).Text
        LvJnsU = ListViewMT1.Items(No_Index).SubItems(CellJnsU).Text
        LvKurs = ListViewMT1.Items(No_Index).SubItems(CellKurs).Text
        LvJenis = ListViewMT1.Items(No_Index).SubItems(CellJenis).Text
    End Sub



    Private Sub Cari(ByVal param As String)

        If param = "Tidak1" Then
            If ComboBoxNP1.SelectedIndex = -1 Then
                MessageBox.Show("Perusahaan belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBoxNP1.Focus() : Exit Sub
            ElseIf ComboBoxMT1.SelectedIndex = -1 Then
                MessageBox.Show("Mata Uang belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBoxMT1.Focus() : Exit Sub
            End If
            ListViewMT1.Items.Clear()
        End If

        Try
            OpenConn()

            Dim lv As New ListViewItem

            'SQL = ";with cte_a as( "
            'SQL = SQL & "select a.No_Faktur, a.Id_rencana, a.Tanggal+a.Jam as Tgl, a.Keterangan, "
            'SQL = SQL & "c.Kode_Perusahaan_Biaya_import, "
            'SQL = SQL & "d.Kode_Master_Kategori_Biaya_Import, d.Keterangan as keterangan_kategori, "
            'SQL = SQL & "b.mata_Uang, c.Nama, b.nilai, "
            'SQL = SQL & "isnull((select sum(Y.byr) from Val_Pel_Biaya_import_by_Perusahaan_Lokal X, "
            'SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
            'SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = b.NO_Faktur and Y.Kode_Perusahaan_Biaya_Import = "
            'SQL = SQL & "b.Kode_Perusahaan_Biaya_import and Y.Mata_Uang = b.Mata_Uang),0) as sudah_bayar, c.Jenis, c.PPN, c.PPH "
            'SQL = SQL & "from transaksi_Biaya_Import a, Detail_transaksi_Biaya_Import_by_Perusahaan b, Perusahaan_Biaya_Import c, Master_Kategori_Biaya_Import d "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_faktur = b.No_faktur And a.Status Is null "
            'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Perusahaan_Biaya_Import = "
            'SQL = SQL & "c.Kode_Perusahaan_biaya_import and b.Flag_Lunas is null "
            'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "

            'SQL = SQL & "union all "

            'SQL = SQL & "select a.No_Faktur, a.Id_rencana, a.Tanggal+a.Jam as Tgl, a.Keterangan, "
            'SQL = SQL & "c.Kode_Perusahaan_Biaya_import, "
            'SQL = SQL & "d.Kode_Master_Kategori_Biaya_Import, d.Keterangan as keterangan_kategori, "
            'SQL = SQL & "b.mata_Uang, c.Nama, b.nilai, "
            'SQL = SQL & "isnull((select sum(Y.byr) from Val_Pel_Biaya_import_by_Perusahaan_Lokal X, "
            'SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
            'SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = b.NO_Faktur and Y.Kode_Perusahaan_Biaya_Import = "
            'SQL = SQL & "b.Kode_Perusahaan_Biaya_import and Y.Mata_Uang = b.Mata_Uang),0) as sudah_bayar, c.Jenis, c.PPN, c.PPH "
            'SQL = SQL & "from transaksi_Biaya_Import3 a, Detail_transaksi_Biaya_Import3_by_Perusahaan b, Perusahaan_Biaya_Import c, Master_Kategori_Biaya_Import d "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_faktur = b.No_faktur And a.Status Is null "
            'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Perusahaan_Biaya_Import = "
            'SQL = SQL & "c.Kode_Perusahaan_biaya_import and b.Flag_Lunas is null "
            'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            'SQL = SQL & ")"

            SQL = "select a.no_faktur, a.tanggal, a.jam,a.id_rencana, a.tanggal_po, a.jml_kontainer, a.kode_kontainer, a.flag_average, "
            SQL = SQL & "a.lokasi, a.kode_perusahaan_biaya_import, a.Perusahaan, a.Kode_Master_Kategori_Biaya_Import, a.Master, "
            SQL = SQL & "a.Kode_Master_Kategori_Biaya_Import, a.master, a.mata_uang, a.Biaya, a.sudah_bayar, a.Jenis, a.PPN, a.PPH, a.jenis_form, a.flag_lunas, a.keterangan, a.jenis_form, a.kurs_lama "
            SQL = SQL & "from View_Detail_Transaksi_Biaya_import a where flag_lunas is null "
            If param = "Tidak1" Then
                SQL = SQL & " and Mata_Uang = '" & ComboBoxMT1.Text & "' "
                If ComboBoxNP1.SelectedIndex <> 0 Then
                    SQL = SQL & "and a.Kode_Perusahaan_Biaya_Import ='" & ArrNP1.Item(ComboBoxNP1.SelectedIndex - 1) & "' "
                End If
                If Cmb_Jenis.SelectedIndex <> 0 Then
                    SQL = SQL & "and a.Jenis =  '" & Cmb_Jenis.SelectedItem & "' "
                End If
            End If

            SQL = SQL & "order by a.tanggal+a.jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    If param = "Tidak1" Then
                        lv = ListViewMT1.Items.Add(Dr("no_faktur")) '0
                    End If

                    lv.SubItems.Add(Dr("id_rencana")) '1
                    lv.SubItems.Add(Dr("Keterangan")) '2
                    lv.SubItems.Add(Format(Dr("tanggal_po"), "MM.dd")) '3
                    lv.SubItems.Add(Dr("Kode_Perusahaan_Biaya_import")) '4
                    lv.SubItems.Add(Dr("Perusahaan")) '5
                    lv.SubItems.Add(Dr("Kode_Master_Kategori_Biaya_Import")) '6
                    lv.SubItems.Add(Dr("Master")) '7
                    lv.SubItems.Add(Dr("Mata_Uang")) '8
                    Dim x_tot As Double = Dr("Biaya")
                    lv.SubItems.Add(Format(x_tot, "N2")) '9
                    lv.SubItems.Add(Format(Dr("sudah_bayar"), "N2")) '10
                    lv.SubItems.Add(Dr("PPN")) '11
                    lv.SubItems.Add(Dr("PPH")) '12
                    lv.SubItems.Add(Dr("lokasi")) '13
                    lv.SubItems.Add(Dr("jenis_form")) '14
                    lv.SubItems.Add(Format(Val(HilangkanTanda(Format(x_tot, "N2"))) - Val(HilangkanTanda(Format(Dr("sudah_bayar"), "N2"))), "N2")) '15
                    lv.SubItems.Add(Dr("kurs_lama")) '16
                    lv.SubItems.Add(Dr("Jenis")) '17
                    'lv.SubItems.Add(Format(x_tot - Dr("pernah_val"), "N2"))
                    'lv.SubItems.Add(Dr("Mata_uang"))
                    'lv.SubItems.Add(Dr("kode_stock_owner_import"))
                    'lv.SubItems.Add(Format(Dr("kurs"), "N2"))
                Loop
            End Using

            Txt_KursBaru.Enabled = False
            ComboBoxBank1.Enabled = False
            ComboBoxRek1.Enabled = False
            ComboBoxNP1.Enabled = False
            ComboBoxMT1.Enabled = False
            Cmb_Jenis.Enabled = False

            CloseConn()
        Catch ex As Exception
            'CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Hitung()
        Dim Total As Double = 0
        Dim Tambahan As Double = 0
        Dim TotalPPN As Double = 0
        Dim TotalPPH As Double = 0
        Dim Grand As Double = 0
        Dim TotalIdr As Double = 0
        Dim TotalKurs As Double = 0
        Dim totalafterKurs As Double = 0
        Dim TotalKursBaru As Double = 0

        For i As Integer = 0 To ListViewMT11.Items.Count - 1
            Get_Isi_Listview(i)

            Total = Total + Val(HilangkanTanda(LvJml))
            Tambahan = Tambahan + Val(HilangkanTanda(LvTambahan))
            TotalPPN = TotalPPN + Val(HilangkanTanda(LvNilaiPPN))
            TotalPPH = TotalPPH + Val(HilangkanTanda(LvNilaiPPH))
            TotalKurs = TotalKurs + Val(HilangkanTanda(LvKursLamaTot))
            TotalKursBaru = TotalKursBaru + Val(HilangkanTanda(LvKursBaruTot))

        Next

        TxtTotAwal.Text = (Format(Total, "N0"))
        TxtTotTambahan.Text = (Format(Tambahan, "N0"))
        TextBoxtot1.Text = (Format(Total + Tambahan, "N0"))

        txt_TotKurs_Lama.Text = (Format(TotalKurs, "N0"))
        txt_TotKurs_Baru.Text = (Format(TotalKursBaru, "N0"))

        TextBoxSelisih.Text = (Format((TotalKursBaru - TotalKurs), "N0"))


        TextBoxtotPPN.Text = (Format(TotalPPN, "N0"))
        TextBoxtotPPH.Text = (Format(TotalPPH, "N0"))

        Txt_GrandTotal.Text = (Format((TotalKursBaru + TotalPPN) - TotalPPH, "N0"))


    End Sub

    Private Sub Get_No_Faktur()
        TxtFaktur.Text = fValPelBI & Format(DateTimePicker1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Val_Pel_Biaya_import_by_Perusahaan_Lokal", "no_val", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_val, 1, " & Len(fValPelBI) + 4 & ")", fValPelBI & Format(DateTimePicker1.Value, "MMyy"))
    End Sub

    Private Sub Get_No_Faktur_Pengajuan()
        Dim fNB = "NB"
        no_fakturPengajuan = fNB & Format(DateTimePicker1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Pengajuan", "No_Pengajuan", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Pengajuan, 1, " & Len(fNB) + 4 & ")", fNB & Format(DateTimePicker1.Value, "MMyy"))
    End Sub

    Private Sub Get_No_Faktur_Token()
        Dim fNB = "NT"
        no_fakturToken = fNB & Format(DateTimePicker1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Pengajuan_token", "No_Pengajuan", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Pengajuan, 1, " & Len(fNB) + 4 & ")", fNB & Format(DateTimePicker1.Value, "MMyy"))
    End Sub

    Public Sub Kosong_Bawah()
        TextBoxFktr.Text = ""
        TextBoxtgl.Text = ""
        TextBoxKP.Text = ""
        TextBoxNP.Text = ""
        TextBoxjml.Text = ""
        TextBoxjns.Text = ""
        TextBoxMT.Text = ""
        TextBoxbyr.Text = ""
        TxtLokasi.Text = ""
        TxtNmKategori.Text = ""
        'TextBox5.Text = ""
        'TextBox6.Text = ""
        'TextBox6.Enabled = False
        TextBoxjml.Enabled = False
    End Sub

    Private Sub Kosong()
        GetTime()
        DateTimePicker1.Value = Tanggal_Sekarang
        DateTimePicker1.Enabled = True

        TextBoxbyr.Text = "" : TextBoxket.Text = ""
        TextBoxtot1.Text = 0
        TextBoxtotPPN.Text = 0
        TextBoxtotPPH.Text = 0
        TextBoxSelisih.Text = 0
        TxtTotAwal.Text = 0
        TxtTotTambahan.Text = 0
        txt_TotKurs_Lama.Text = 0
        Txt_GrandTotal.Text = 0
        txt_TotKurs_Baru.Text = 0
        TextBox10.Text = ""
        TextBox7.Text = ""
        Txt_SelectedJenis.Text = ""
        Btn_Simpan.Text = "&Simpan"

        Txt_KursBaru.Text = ""
        Txt_SelectedKategori.Text = ""

        Txt_KursBaru.Enabled = True
        ComboBoxBank1.Enabled = True
        ComboBoxRek1.Enabled = True
        ComboBoxNP1.Enabled = True
        ComboBoxMT1.Enabled = True
        Cmb_Jenis.Enabled = True

        ComboBoxMT1.Enabled = False

        ArrMataUangRek.Clear()
        ListViewMT11.Items.Clear()
        ListViewMT1.Items.Clear()

        Kosong_Bawah()
        ComboBoxRek1.Items.Clear()
        Try

            OpenConn()

            Get_No_Faktur()

            'ComboBoxCb1.Items.Clear() : ArrCB1.Clear()
            'ComboBoxCb1.Items.Add("-- Cara Bayar --") : ArrCB1.Add("")

            'ComboBoxCb1.SelectedIndex = 0
            'SQL = "select kode_cb, keterangan from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBoxCb1.Items.Add(Dr("kode_cb")) : ArrCB1.Add(Dr("kode_cb"))
            '    Loop
            'End Using

            'ComboBoxCb1.Items.Clear() : arrCrByr1.Clear() : ArrAkunCB1.Clear()
            'ComboBoxCb1.Items.Add("-- Cara Bayar --") : arrCrByr1.Add("") : ArrAkunCB1.Add("")
            'ComboBoxCb1.SelectedIndex = 0
            'SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBoxCb1.Items.Add(Dr("keterangan")) : arrCrByr1.Add(Dr("kode_cb")) : ArrAkunCB1.Add(Dr("kode_account_cb"))
            '    Loop
            'End Using

            'ComboBoxCb2.Items.Clear() : arrCrByr2.Clear() : ArrAkunCB2.Clear()
            'ComboBoxCb2.Items.Add("-- Cara Bayar --") : arrCrByr2.Add("") : ArrAkunCB2.Add("")
            'ComboBoxCb2.SelectedIndex = 0
            'SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBoxCb2.Items.Add(Dr("keterangan")) : arrCrByr2.Add(Dr("kode_cb")) : ArrAkunCB2.Add(Dr("kode_account_cb"))
            '    Loop
            'End Using

            Cmb_Jenis.Items.Clear()
            Cmb_Jenis.Items.Add("-- Seluruh --")
            Cmb_Jenis.Items.Add("IMPORT")
            Cmb_Jenis.Items.Add("LOKAL")
            Cmb_Jenis.SelectedIndex = 0

            ComboBoxBank1.Items.Clear()
            SQL = "SELECT kode_bank from bank where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' ORDER BY kode_bank"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBoxBank1.Items.Add(dr("kode_bank"))
                Loop
            End Using

            ComboBoxNP1.Items.Clear()
            ComboBoxNP1.Items.Add("-- Seluruh --")
            ComboBoxNP1.SelectedIndex = 0
            SQL = "select Nama, Kode_Perusahaan_Biaya_Import from Perusahaan_Biaya_Import where kode_perusahaan = '" & KodePerusahaan & "' order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxNP1.Items.Add(Dr("Nama")) : ArrNP1.Add(Dr("Kode_Perusahaan_Biaya_Import"))
                Loop
            End Using

            ComboBoxMT1.Items.Clear()
            SQL = "select Kode_Mata_uang from Mata_Uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Mata_Uang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxMT1.Items.Add(Dr("Kode_Mata_uang"))
                Loop
            End Using

            ComboBox3.Items.Clear()
            SQL = "SELECT Kode_Bank FROM Bank_tujuan WHERE Kode_Perusahaan = '" & KodePerusahaan & "' ORDER BY Kode_Bank"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox3.Items.Add(dr("Kode_Bank"))
                Loop
            End Using

            'Delete PPN
            SQL = "Delete From Display_Biaya_Import_PPN "
            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' "
            ExecuteTrans(SQL)

            'Delete PPh
            SQL = "Delete From Display_Biaya_Import_PPh "
            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' "
            ExecuteTrans(SQL)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'pilihMataUang()
        Hitung()

    End Sub

    'Private Sub pilihMataUang()
    '    Try
    '        OpenConn()

    '        cbxMataUang.Items.Clear()
    '        cbxMataUang.Items.Add("--Pilih Mata Uang--")
    '        cbxMataUang.SelectedIndex = 0

    '        SQL = "select distinct(mata_uang) from pembelian_import where kode_perusahaan = '" & KodePerusahaan & "' order by mata_uang   "
    '        Using Dr = OpenTrans(SQL)
    '            Do While Dr.Read
    '                cbxMataUang.Items.Add(Dr("mata_uang"))
    '            Loop
    '        End Using
    '        CloseConn()

    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    '    'cbxMataUang.Items.Add("USD")
    '    'cbxMataUang.Items.Add("CNY")
    '    cbxMataUang.Enabled = True
    '    TextBox8.Text = ""
    '    TextBox8.Enabled = True
    '    TextBox1.Enabled = False
    '    Button3.Enabled = False
    '    Button5.Enabled = False

    'End Sub



    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then TextBoxket.Focus()
    End Sub

    Private Sub DateTimePicker1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.Leave
        Try

            OpenConn()

            Get_No_Faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxket.KeyPress
        'If e.KeyChar = Chr(13) Then ComboBoxCb1.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBoxNP1.KeyPress
        If e.KeyChar = Chr(13) Then TextBoxbyr.Focus()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxbyr.KeyPress
        Dim hasilTotal As Double = 0

        If e.KeyChar = Chr(13) Then
            If TextBoxFktr.Text.Trim.Length = 0 Then
                MessageBox.Show("Silahkan pilih faktur terlebih dahulu")
                Exit Sub
            End If

            If (Val(TextBoxbyr.Text) > Val(HilangkanTanda(TextBoxjml.Text))) Then
                MessageBox.Show("Pembayaran tidak boleh melebihi sisa hutang") : Exit Sub
            End If

            If TextBoxjns.Text = "1" Then
                For i As Integer = 0 To ListViewMT11.Items.Count - 1
                    If ListViewMT11.Items(i).Text.Trim.ToUpper = TextBoxFktr.Text.Trim.ToUpper And ListViewMT11.Items(i).SubItems(2).Text.Trim.ToUpper = TextBoxKP.Text.Trim.ToUpper Then
                        MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If ListViewMT11.Items(i).SubItems(6).Text.Trim.ToUpper <> TextBoxMT.Text.Trim.ToUpper Then
                        MessageBox.Show("Mata Uang Tidak Boleh berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Next

                'Dim lv As New ListViewItem
                'lv = ListViewMT11.Items.Add(TextBoxFktr.Text)
                'lv.SubItems.Add(TextBoxtgl.Text)
                'lv.SubItems.Add(TextBoxKP.Text)
                'lv.SubItems.Add(TextBoxNP.Text)
                'lv.SubItems.Add(TextBoxMT.Text)
                'lv.SubItems.Add(Format(Val(TextBoxbyr.Text), "N0"))
                'lv.SubItems.Add(TextBoxPPN.Text)
                'lv.SubItems.Add(TextBoxPPH.Text)
                'lv.SubItems.Add(Format(Val(TextBoxPPN.Text) * Val(TextBoxbyr.Text) / 100, "N0"))
                'lv.SubItems.Add(Format(Val(TextBoxPPH.Text) * Val(TextBoxbyr.Text) / 100, "N0"))

            ElseIf TextBoxjns.Text = "2" Then

            End If

#Region "KODE LAMA"

            'Display_Pembagi_Biaya_Import_Lokal.Txt_NoFaktur.Text = TextBoxFktr.Text
            'Display_Pembagi_Biaya_Import_Lokal.Txt_NmPerusahaan.Text = TextBoxNP.Text
            'Display_Pembagi_Biaya_Import_Lokal.Lbl_KdPerusahaan.Text = TextBoxKP.Text
            'Display_Pembagi_Biaya_Import_Lokal.Lbl_Tgl.Text = TextBoxtgl.Text
            'Display_Pembagi_Biaya_Import_Lokal.TxtKodeKategori.Text = TxtKodeKategori.Text
            'Display_Pembagi_Biaya_Import_Lokal.Txt_KategoriPerusahaan.Text = TxtDataNmKategori.Text
            'Display_Pembagi_Biaya_Import_Lokal.Txt_MataUang.Text = TextBoxMT.Text
            'Display_Pembagi_Biaya_Import_Lokal.Txt_ttlBiaya.Text = Format(Val(TextBoxbyr.Text), "N0")
            'Display_Pembagi_Biaya_Import_Lokal.Txt_NilaiTambahan.Text = 0
            'Display_Pembagi_Biaya_Import_Lokal.TxtLokasi.Text = TxtLokasi.Text
            'Display_Pembagi_Biaya_Import_Lokal.Txt_PersenPPN.Text = Format(Val(TxtDataPPN.Text), "N2")
            'Display_Pembagi_Biaya_Import_Lokal.Txt_PersenPPh.Text = Format(Val(TxtDataPPH.Text), "N2")
            'Display_Pembagi_Biaya_Import_Lokal.ShowDialog()

            ' lv.SubItems.Add(TextBox10.Text)
            'lv.SubItems.Add(Format(Val(TextBox6.Text), "N2"))

            'If TextBox6.Text = 1 Then
            '    hasilTotal = Format(Val(TextBox12.Text), "N2") * 1
            'Else
            '    hasilTotal = Format(Val(TextBox12.Text), "N2") * Format(Val(TextBox6.Text), "N2")
            'End If

            'lv.SubItems.Add(Format(hasilTotal, "N2"))
            'Hitung()
            '  Kosong_Bawah()

#End Region

            Try

                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                'Delete PPN
                SQL = "Delete From Display_Biaya_Import_PPN "
                SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and no_faktur ='" & TextBoxFktr.Text & "' and "
                SQL = SQL & "Kode_Perusahaan_Biaya_Import='" & TextBoxKP.Text & "' and Kode_Master_Kategori_Biaya_Import='" & TxtKodeKategori.Text & "' and mata_uang='" & TextBoxMT.Text & "' and UserID='" & UserID & "' and Lokasi='" & TxtLokasi.Text & "' "
                ExecuteTrans(SQL)

                'Delete PPh
                SQL = "Delete From Display_Biaya_Import_PPh "
                SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and no_faktur ='" & TextBoxFktr.Text & "' and "
                SQL = SQL & "Kode_Perusahaan_Biaya_Import='" & TextBoxKP.Text & "' and Kode_Master_Kategori_Biaya_Import='" & TxtKodeKategori.Text & "' and mata_uang='" & TextBoxMT.Text & "' and UserID='" & UserID & "' and Lokasi='" & TxtLokasi.Text & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                CloseConn()
                'MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            Dim JnsBiaya As String = ListViewMT1.FocusedItem.SubItems(CellJenis).Text

            Dim NTotal As Double = 0
            NTotal = Val(HilangkanTanda(TextBoxbyr.Text)) + Val(HilangkanTanda(0))

            Dim KursLama As Double = Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(CellKurs).Text))
            Dim KursBaru As Double = Val(HilangkanTanda(Txt_KursBaru.Text))

            Dim TotKursLama As Double = NTotal * KursLama
            Dim TotKursBaru As Double = NTotal * KursBaru

            Dim Nppn As Double = 0
            Nppn = TotKursBaru * Val(TxtDataPPN.Text) / 100
            Dim HslNppn As Double = Format(Nppn, "N0")

            Dim Npph As Double = 0
            Npph = TotKursBaru * Val(TxtDataPPH.Text) / 100
            Dim HslNpph As Double = Format(Npph, "N0")

            Try

                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction
                'Simpan PPN
                SQL = "Insert into Display_Biaya_Import_PPN (Kode_Perusahaan, Kode_Perusahaan_Biaya_Import, No_Faktur, No_Pembagi, No_Faktur_Pajak, Nilai_Pembagi, Mata_Uang, Kode_Master_Kategori_Biaya_Import, UserID, Lokasi) "
                SQL = SQL & "Values ('" & KodePerusahaan & "', '" & TextBoxKP.Text & "', '" & TextBoxFktr.Text & "', '1', '-', '" & Nppn & "', '" & TextBoxMT.Text & "', '" & TxtKodeKategori.Text & "', '" & UserID & "', '" & TxtLokasi.Text & "') "
                ExecuteTrans(SQL)

                'Simpan PPh
                SQL = "Insert into Display_Biaya_Import_PPh (Kode_Perusahaan, Kode_Perusahaan_Biaya_Import, No_Faktur, No_Pembagi, No_Faktur_Pajak, Nilai_Pembagi, Mata_Uang, Kode_Master_Kategori_Biaya_Import, UserID, Lokasi) "
                SQL = SQL & "Values ('" & KodePerusahaan & "', '" & TextBoxKP.Text & "', '" & TextBoxFktr.Text & "', '1', '-', '" & Npph & "', '" & TextBoxMT.Text & "', '" & TxtKodeKategori.Text & "', '" & UserID & "', '" & TxtLokasi.Text & "') "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                CloseConn()
                'MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            Dim lv As New ListViewItem
            lv = ListViewMT11.Items.Add(TextBoxFktr.Text) 'NoFaktur
            lv.SubItems.Add(TextBoxtgl.Text) 'Tgl
            lv.SubItems.Add(TextBoxKP.Text) 'Kode Perusahaan
            lv.SubItems.Add(TextBoxNP.Text) 'Nama Perusahaan
            lv.SubItems.Add(TxtKodeKategori.Text) 'Kategori Perusahaan
            lv.SubItems.Add(TxtDataNmKategori.Text) 'Kategori Perusahaan
            lv.SubItems.Add(TextBoxMT.Text) 'Mata Uang
            lv.SubItems.Add(Format(Val(TextBoxbyr.Text), "N0")) 'Jumlah
            lv.SubItems.Add(0) ' Nilai Tambahan

            lv.SubItems.Add(Format(KursLama, "N0")) ' Kurs Lama
            lv.SubItems.Add(Format(TotKursLama, "N0")) ' Total Kurs Lama
            lv.SubItems.Add(Format(KursBaru, "N0")) ' Kurs Baru
            lv.SubItems.Add(Format(TotKursBaru, "N0")) ' Total Kurs Baru

            lv.SubItems.Add(Format(NTotal, "N0")) ' Nilai Total
            lv.SubItems.Add(Format(Val(TxtDataPPN.Text), "N2")) 'Persen PPN
            lv.SubItems.Add(Format(Val(TxtDataPPH.Text), "N2")) 'Persen PPh
            lv.SubItems.Add(HslNppn) 'Nilai PPN
            lv.SubItems.Add(HslNpph) ' Nilai PPh
            lv.SubItems.Add(TxtLokasi.Text) ' lks
            lv.SubItems.Add(TxtJenisForm.Text) ' jns form
            lv.SubItems.Add(JnsBiaya) ' jns Biaya

            Kosong_Bawah()
            Hitung()

        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewMT11.DoubleClick
        'TextBox9.Text = ListView1.FocusedItem.Text
        'TextBox3.Text = ListView1.FocusedItem.SubItems(1).Text
        'TextBox11.Text = ListView1.FocusedItem.SubItems(2).Text
        'TextBox4.Text = ListView1.FocusedItem.SubItems(3).Text
        'TextBox7.Text = ListView1.FocusedItem.SubItems(4).Text

        'TextBox5.Text = ListView1.FocusedItem.SubItems(5).Text
        'TextBox6.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(8).Text)
        'TextBox10.Text = ListView1.FocusedItem.SubItems(7).Text
        'TextBox12.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(6).Text)

        'TextBox6.Enabled = True
        'TextBox12.Enabled = False
        'TextBox6.Focus()

        Dim tny As String = MessageBox.Show("Yakin akan dihapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If tny = vbNo Then Exit Sub

        Get_Isi_Listview1(ListViewMT11.FocusedItem.Index)

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            'Delete PPN
            SQL = "Delete From Display_Biaya_Import_PPN "
            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and no_faktur ='" & LvFak & "' and "
            SQL = SQL & "Kode_Perusahaan_Biaya_Import='" & LvKP & "' and Kode_Master_Kategori_Biaya_Import='" & LvKdKategori & "' and mata_uang='" & LvMT & "' and UserID='" & UserID & "' and Lokasi='" & LvLokasi & "' "
            ExecuteTrans(SQL)

            'Delete PPh
            SQL = "Delete From Display_Biaya_Import_PPh "
            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and no_faktur ='" & LvFak & "' and "
            SQL = SQL & "Kode_Perusahaan_Biaya_Import='" & LvKP & "' and Kode_Master_Kategori_Biaya_Import='" & LvKdKategori & "' and mata_uang='" & LvMT & "' and UserID='" & UserID & "' and Lokasi='" & LvLokasi & "' "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data berhasil dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ListViewMT11.FocusedItem.Remove()
        Hitung()
    End Sub

    Private Sub TxtFaktur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFaktur.KeyPress
        If e.KeyChar = Chr(13) Then
            If DateTimePicker1.Enabled = True Then
                DateTimePicker1.Focus()
            Else
                TextBoxket.Focus()
            End If
        End If
    End Sub

    Private Sub TxtFaktur_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFaktur.Leave
        'Try

        '    If TxtFaktur.Text.Trim.Length = 0 Then
        '        OpenConn()

        '        Get_No_Faktur()

        '        CloseConn()
        '    End If

        '    OpenConn()

        '    Dim lv As New ListViewItem

        '    SQL = "select c.cara_bayar, a.no_faktur, a.tanggal, a.tgl_jatuh_tempo, c.keterangan, "
        '    SQL = SQL & "c.grand, c.no_val, a.kode_supplier, b.nama as namasupplier, d.byr from "
        '    SQL = SQL & "pembelian a, suppliers b, val_pemb c, detail_val_pemb d where "
        '    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And b.kode_perusahaan = c.kode_perusahaan And "
        '    SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan And a.kode_supplier = b.kode_supplier and "
        '    SQL = SQL & "c.no_val = d.no_val and a.no_faktur = d.no_faktur and "
        '    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and c.no_val = '" & TxtFaktur.Text.Trim & "' "
        '    SQL = SQL & "order by d.urut "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            DateTimePicker1.Enabled = False

        '            DateTimePicker1.Value = Dr("tanggal")
        '            TxtFaktur.Text = Dr("no_val")
        '            TextBoxket.Text = Dr("keterangan")
        '            TextBoxtot1.Text = Format(Dr(" "), "N0")
        '            For i As Integer = 0 To ComboBoxCb1.Items.Count - 1
        '                If Dr("cara_bayar") = arrCrByr1.Item(i) Then
        '                    ComboBoxCb1.SelectedIndex = i
        '                    Exit For
        '                End If
        '            Next

        '            ListViewMT1.Items.Clear() : ListViewMT11.Items.Clear()

        '            lv = ListViewMT11.Items.Add(Dr("no_faktur"))
        '            lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
        '            lv.SubItems.Add(Format(Dr("tgl_jatuh_tempo"), "dd MMM yyyy"))
        '            lv.SubItems.Add(Dr("kode_supplier"))
        '            lv.SubItems.Add(Dr("namasupplier"))
        '            lv.SubItems.Add(Format(Dr("byr"), "N0"))

        '            Do While Dr.Read
        '                lv = ListViewMT11.Items.Add(Dr("no_faktur"))
        '                lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
        '                lv.SubItems.Add(Format(Dr("tgl_jatuh_tempo"), "dd MMM yyyy"))
        '                lv.SubItems.Add(Dr("kode_supplier"))
        '                lv.SubItems.Add(Dr("namasupplier"))
        '                lv.SubItems.Add(Format(Dr("byr"), "N0"))
        '            Loop

        '            Btn_Simpan.Text = "&Update"
        '        Else
        '            Kosong()
        '        End If

        '    End Using

        '    CloseConn()

        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim hasilTotal As Double = 0

        If e.KeyChar = Chr(13) Then

            If TextBoxjml.Text.Trim.Length = 0 Then
                TextBoxjml.Focus()
                MessageBox.Show("Kurs tidak boleh kosong!!") : Exit Sub
            End If

            'If TextBox10.Text = "RP" And TextBox6.Text > 1 Then
            '    MessageBox.Show("Nilai Kurs Rp harus 1") : Exit Sub
            'End If

            'If (TextBox6.Text.Trim.Length = 0 And TextBox9.Text.Trim.Length = 0) Then
            '    MessageBox.Show("Silahkan pilih faktur terlebih dahulu")
            '    TextBox6.Text = "" : Exit Sub
            'ElseIf TextBox9.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Silahkan pilih faktur terlebih dahulu")
            '    TextBox6.Text = "" : Exit Sub
            'End If

            'If (TextBox6.Text > Val(checkSisaHutang)) Then
            '    MessageBox.Show("Pembayaran tidak boleh melebihi sisa hutang") : Exit Sub
            'End If

            For i As Integer = 0 To ListViewMT11.Items.Count - 1
                If ListViewMT11.Items(i).Text.Trim.ToUpper = TextBoxFktr.Text.Trim.ToUpper And ListViewMT11.Items(i).SubItems(2).Text.Trim.ToUpper = TextBoxNP.Text.Trim.ToUpper Then
                    MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next

            Dim lv As New ListViewItem
            lv = ListViewMT11.Items.Add(TextBoxFktr.Text)
            lv.SubItems.Add(TextBoxtgl.Text)
            lv.SubItems.Add(TextBoxNP.Text)
            'lv.SubItems.Add(TextBox4.Text)

            'lv.SubItems.Add(TextBox7.Text)
            'lv.SubItems.Add(TextBox5.Text)
            lv.SubItems.Add(Format(Val(TextBoxjml.Text), "N2"))
            ' lv.SubItems.Add(TextBox10.Text)
            'lv.SubItems.Add(Format(Val(TextBox6.Text), "N2"))

            'If TextBox6.Text = 1 Then
            '    hasilTotal = Format(Val(TextBox12.Text), "N2") * 1
            'Else
            '    hasilTotal = Format(Val(TextBox12.Text), "N2") * Format(Val(TextBox6.Text), "N2")
            'End If

            lv.SubItems.Add(Format(hasilTotal, "N2"))

            Hitung()
            Kosong_Bawah()
            ListViewMT1.Focus()
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBoxCb1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then ComboBoxNP1.Focus()
    End Sub

    'Private Sub ComboBoxCb1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBoxCb1.KeyPress

    '    If e.KeyChar = Chr(13) Then cbxMataUang.Focus()

    'End Sub

    Private Sub TxtFaktur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFaktur.TextChanged

    End Sub

    'Private Sub cbxMataUang_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If cbxMataUang.SelectedIndex <> 0 Then
    '        TextBox8.Visible = True
    '        TextBox8.Focus()
    '    Else
    '        TextBox8.Visible = False
    '    End If

    'End Sub

    'Private Sub TextBox8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If e.KeyChar = Chr(13) Then

    '        'If TextBox8.Text.Trim.Length = 0 Then
    '        '    MessageBox.Show("Silahkan isi kurs terlebih dahulu") : Exit Sub
    '        'End If

    '        'TextBox8.Enabled = False
    '        'cbxMataUang.Enabled = False
    '        TextBox1.Enabled = True
    '        Button3.Enabled = True
    '        Button5.Enabled = True
    '        TextBox1.Focus()
    '        'varMataUang = cbxMataUang.Text
    '        'kursUangBaru = TextBox8.Text
    '    End If
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    'End Sub

    'Private Sub cbxMataUang_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    If cbxMataUang.SelectedIndex <> 0 Then
    '        TextBox8.Visible = True
    '        TextBox8.Focus()
    '    Else
    '        TextBox8.Visible = False
    '    End If

    'End Sub

    Private Sub TextBox12_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxjml.KeyPress
        'If e.KeyChar = Chr(13) Then
        '    If TextBox12.Text.Trim.Length = 0 Then
        '        MessageBox.Show("Silahkan isi kurs terlebih dahulu!!") : Exit Sub
        '    End If
        '    TextBox6.Focus()
        'End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    End Sub

    Private Sub ComboBoxCb1_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            TextBoxbyr.Focus()
        End If
    End Sub

    Private Sub ButtonMT2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Cari("Tidak2")
    End Sub

    Private Sub ListViewMT1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewMT1.DoubleClick
        Dim kd_kategori As String = ""
        Dim mt As String = ""
        Dim lks As String = ""
        Dim jns As String = ""

        For i As Integer = 0 To ListViewMT11.Items.Count - 1

            If ListViewMT11.Items(i).SubItems(CellFak).Text.Trim.ToUpper = ListViewMT1.FocusedItem.SubItems(CellFakMU).Text.Trim.ToUpper And
            ListViewMT11.Items(i).SubItems(CellKP).Text.Trim.ToUpper = ListViewMT1.FocusedItem.SubItems(CellKdPerusahaanBiayaImportMU).Text.Trim.ToUpper And
            ListViewMT11.Items(i).SubItems(CellKdKategori).Text.Trim.ToUpper = ListViewMT1.FocusedItem.SubItems(CellKdKategoriMU).Text.Trim.ToUpper And
            ListViewMT11.Items(i).SubItems(CellLokasi).Text.Trim.ToUpper = ListViewMT1.FocusedItem.SubItems(CellLokasiU).Text.Trim.ToUpper And
            ListViewMT11.Items(i).SubItems(CellMT).Text.Trim.ToUpper = ListViewMT1.FocusedItem.SubItems(CellMataUangMU).Text.Trim.ToUpper Then
                MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If i = 0 Then
                kd_kategori = ListViewMT11.Items(i).SubItems(CellKdKategori).Text.Trim.ToUpper
                mt = ListViewMT11.Items(i).SubItems(CellMT).Text.Trim.ToUpper
                lks = ListViewMT11.Items(i).SubItems(CellLokasi).Text.Trim.ToUpper
                jns = ListViewMT11.Items(i).SubItems(CellJnsBiaya).Text.Trim.ToUpper
            End If

            If jns <> ListViewMT1.FocusedItem.SubItems(CellJenis).Text.Trim.ToUpper Then
                MessageBox.Show("Jenis Biaya Tidak Boleh berbeda !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If kd_kategori <> ListViewMT1.FocusedItem.SubItems(CellKdKategoriMU).Text.Trim.ToUpper Then
                MessageBox.Show("kategori Tidak Boleh berbeda !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If mt <> ListViewMT1.FocusedItem.SubItems(CellMataUangMU).Text.Trim.ToUpper Then
                MessageBox.Show("Mata Uang Tidak Boleh berbeda !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If lks <> ListViewMT1.FocusedItem.SubItems(CellLokasiU).Text.Trim.ToUpper Then
                MessageBox.Show("Lokasi Tidak Boleh berbeda !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

        Next

        'Dim Total_Hutang As Double = Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(7).Text)) - Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(8).Text))
        Dim Total_Hutang As Double = Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(CellNilaiMU).Text)) - Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(CellSdhByrMU).Text))
        Dim KursLama As Double = Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(CellKurs).Text))

        If KursLama = 0 Then
            MessageBox.Show("Data Belum Sampai", "Pelunasan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        TextBoxFktr.Text = ListViewMT1.FocusedItem.Text
        TextBoxtgl.Text = ListViewMT1.FocusedItem.SubItems(CellTglMU).Text

        TxtDataNmKategori.Text = ListViewMT1.FocusedItem.SubItems(CellKeteranganKategoriMU).Text
        TxtDataPPN.Text = ListViewMT1.FocusedItem.SubItems(CellPPNMU).Text
        TxtDataPPH.Text = ListViewMT1.FocusedItem.SubItems(CellPPHMU).Text

        TextBoxKP.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(CellKdPerusahaanBiayaImportMU).Text)
        TextBoxNP.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(CellNmPerusahaanMU).Text)
        TextBoxMT.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(CellMataUangMU).Text)
        TextBoxbyr.Text = Total_Hutang
        TextBoxjml.Text = Format(Total_Hutang, "N2")
        TextBoxjns.Text = "1"
        TxtJenisForm.Text = LvJnsU
        TextBoxNilai.Text = ListViewMT1.FocusedItem.SubItems(CellNilaiMU).Text
        TextBoxPPH.Text = ListViewMT1.FocusedItem.SubItems(CellSdhByrMU).Text

        Txt_SelectedJenis.Text = ListViewMT1.FocusedItem.SubItems(CellJenis).Text

        TxtKodeKategori.Text = ListViewMT1.FocusedItem.SubItems(CellKdKategoriMU).Text
        Txt_SelectedKategori.Text = ListViewMT1.FocusedItem.SubItems(CellKdKategoriMU).Text

        TxtNmKategori.Text = ListViewMT1.FocusedItem.SubItems(CellKeteranganKategoriMU).Text

        TxtLokasi.Text = ListViewMT1.FocusedItem.SubItems(CellLokasiU).Text

    End Sub

    Private Sub TextBoxDBY1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxKV1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxADM1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxKurs1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxDBY2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxADM2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxKurs2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub


    Private Sub TextBoxDPT1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxPindahKurs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxsimpan1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
        DateTimePicker1.Focus()
    End Sub

    Private Sub Txt_KursBaru_Leave(sender As Object, e As EventArgs) Handles Txt_KursBaru.Leave
        If Txt_KursBaru.Text.Trim.Length = 0 Then Exit Sub

        Txt_KursBaru.Text = Format(Val(HilangkanTanda(Txt_KursBaru.Text)), "N2")
    End Sub

    Private Sub Txt_KursBaru_Enter(sender As Object, e As EventArgs) Handles Txt_KursBaru.Enter
        If Txt_KursBaru.Text.Trim.Length = 0 Then Exit Sub

        Txt_KursBaru.Text = HilangkanTanda(Txt_KursBaru.Text)
    End Sub



    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBoxBank1.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Bank", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxBank1.Focus() : Exit Sub
        ElseIf ComboBoxRek1.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Nomor Rekening", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxRek1.Focus() : Exit Sub
        ElseIf ComboBoxNP1.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Nama Perusahaan", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxNP1.Focus() : Exit Sub
        ElseIf ComboBoxMT1.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Mata Uang", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxMT1.Focus() : Exit Sub
        ElseIf Cmb_Jenis.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Jenis Perusahaan", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis.Focus() : Exit Sub
        ElseIf Txt_KursBaru.Text.Trim.Length = 0 Then
            MessageBox.Show("Kurs Tidak Boleh Kosong", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis.Focus() : Exit Sub
        End If

        Cari("Tidak1")
    End Sub


    Private Sub Txt_KursBaru_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KursBaru.KeyPress
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    End Sub


    Private Sub TextBoxDPT2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub ComboBoxBank1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxBank1.SelectedIndexChanged
        If ComboBoxBank1.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            ComboBoxMT1.SelectedIndex = -1 : ComboBoxMT1.Text = ""

            ComboBoxRek1.Items.Clear() : ArrAkunRek1.Clear() : ArrMataUangRek.Clear()
            'LvCheque.Items.Clear()
            SQL = "SELECT r.No_Rek, Kode_Akun, Mata_Uang FROM Rekening r WHERE "
            SQL = SQL & "r.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If ComboBoxBank1.SelectedIndex <> -1 Then
                SQL = SQL & "and r.kode_bank = '" & ComboBoxBank1.Text & "' "
            End If
            SQL = SQL & "ORDER BY r.No_Rek"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBoxRek1.Items.Add(dr("no_rek")) : ArrAkunRek1.Add(dr("Kode_Akun")) : ArrMataUangRek.Add(dr("Mata_Uang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBoxRek1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxRek1.SelectedIndexChanged
        If ComboBoxRek1.SelectedIndex = -1 Then Exit Sub

        ComboBoxMT1.SelectedItem = ArrMataUangRek(ComboBoxRek1.SelectedIndex)

        ComboBoxMT1.Enabled = False

    End Sub


    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        If TextBox7.Text.Trim.Length = 0 Then
            ListView3.Visible = False
            ListView3.Location = New Point(1353, 719)
            Exit Sub
        Else
            ListView3.Location = New Point(118, 719)
            ListView3.Visible = True
        End If

        ListView3.Items.Clear()
        Dim lv As New ListViewItem

        Try
            OpenConn()

            SQL = "select no_rekening, nama, kode_bank, Alamat_Penerima, Kota_Penerima, "
            SQL = SQL & "Negara_Penerima, Telp_Penerima from rekening_tujuan where kode_perusahaan = '" & KodePerusahaan & "' and nama like '%" & TextBox7.Text & "%' order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView3.Items.Add(Dr("no_rekening"))
                    lv.SubItems.Add(Dr("nama"))
                    lv.SubItems.Add(Dr("kode_bank"))
                    lv.SubItems.Add(Dr("Alamat_Penerima"))
                    lv.SubItems.Add(Dr("Kota_Penerima"))
                    lv.SubItems.Add(Dr("Negara_Penerima"))
                    lv.SubItems.Add(Dr("Telp_Penerima"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.DoubleClick

        Dim kode As String = ListView3.FocusedItem.Text
        Dim nama As String = ListView3.FocusedItem.SubItems(1).Text
        Dim kode_bank As String = ListView3.FocusedItem.SubItems(2).Text
        Dim alamat As String = ListView3.FocusedItem.SubItems(3).Text
        Dim kota As String = ListView3.FocusedItem.SubItems(4).Text
        Dim negara As String = ListView3.FocusedItem.SubItems(5).Text
        Dim telp As String = ListView3.FocusedItem.SubItems(6).Text

        TextBox7.Text = nama
        TextBox10.Text = kode
        ComboBox3.Text = kode_bank

        x_alamat = alamat
        x_Kota = kota
        x_negara = negara
        x_telp = telp

        ListView3.Visible = False
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If TxtFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No pelunasan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFaktur.Focus()
            Exit Sub
        ElseIf TextBoxket.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxket.Focus()
            Exit Sub
        ElseIf ComboBoxBank1.SelectedIndex = -1 Or ComboBoxRek1.SelectedIndex = -1 Then
            MessageBox.Show("Rekening harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxBank1.Focus()
            Exit Sub
        ElseIf ListViewMT11.Items.Count = 0 Then
            MessageBox.Show("Tidak Ada Data. . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ListViewMT11.Focus()
            Exit Sub
        ElseIf TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Tujuan Harus di isi. . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ListViewMT11.Focus()
            Exit Sub
        End If

        If Btn_Simpan.Text = "&Simpan" Then
            Dim tny As String = MessageBox.Show("Yakin akan disimpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If tny = vbNo Then Exit Sub

            GetTime()
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                Get_No_Faktur()

                '=====================
                '=     CEK TUJUAN    =
                '=====================
                SQL = "select no_rekening, nama, kode_bank, Alamat_Penerima, Kota_Penerima, "
                SQL = SQL & "Negara_Penerima, Telp_Penerima from rekening_tujuan where kode_perusahaan = '" & KodePerusahaan & "' and nama = '" & TextBox7.Text & "' order by nama"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Nama Tujuan Tidak ditemukan", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        TextBox7.Focus()
                        Exit Sub
                    End If

                End Using
                Dim Coa_Selisih As String = ""
                Dim SisaHutang As Double = 0
                Dim JT As String = ""
                Dim Dari As String = ""
                Dim KodeCust As String = ""
                Dim lks As String = ""
                Dim kdMaster As String = ""
                Dim x_no_urut_det_pengajuan As Integer = 0
                Dim Kode_Voucher As String = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPemb, KodePerusahaan)

                Dim pagenumber As Integer = 1

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_Voucher & "', "
                SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Pelunasan hutang " & TxtFaktur.Text.Trim & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)


                SQL = "insert into Val_Pel_Biaya_import_by_Perusahaan_Lokal(kode_perusahaan, no_val, tanggal, jam, "
                SQL = SQL & "keterangan, uservalidasi, kode_voucher, Mata_Uang, Kode_Bank, No_rek, Total, Total_PPN, Total_PPH, Grand_Total, "
                SQL = SQL & "Kode_Bank_Tujuan, No_Rek_Tujuan, Nama_Penerima, Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Total_Kurs_Lama, Total_Kurs_Baru, Jenis)"
                SQL = SQL & "values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                SQL = SQL & "'" & TextBoxket.Text.Trim & "', '" & UserID & "', "
                SQL = SQL & "NULL, "
                SQL = SQL & "'" & ListViewMT11.Items(0).SubItems(CellMT).Text & "','" & ComboBoxBank1.Text & "','" & ComboBoxRek1.Text & "', "
                SQL = SQL & "'" & HilangkanTanda(TextBoxtot1.Text) & "', '" & HilangkanTanda(TextBoxtotPPN.Text) & "', '" & HilangkanTanda(TextBoxtotPPH.Text) & "', "
                SQL = SQL & "'" & HilangkanTanda(Txt_GrandTotal.Text) & "', '" & ComboBox3.Text & "', '" & TextBox10.Text & "', '" & TextBox7.Text & "', "
                SQL = SQL & "'" & x_alamat & "', '" & x_Kota & "', '" & x_negara & "', '" & x_telp & "', '" & HilangkanTanda(txt_TotKurs_Lama.Text) & "', '" & HilangkanTanda(txt_TotKurs_Baru.Text) & "', '" & Txt_SelectedJenis.Text & "')"
                ExecuteTrans(SQL)

                For i As Integer = 0 To ListViewMT11.Items.Count - 1
                    Get_Isi_Listview(i)

                    Dim kodeMasterKategoriBI As String = ""
                    SQL = "select a.no_faktur, a.tanggal, a.jam,a.id_rencana, a.tanggal_po, a.jml_kontainer, a.kode_kontainer, a.flag_average, "
                    SQL = SQL & "a.lokasi, a.kode_perusahaan_biaya_import, a.Perusahaan, a.Kode_Master_Kategori_Biaya_Import, a.Master, "
                    SQL = SQL & "a.Kode_Master_Kategori_Biaya_Import, a.master, a.mata_uang, a.Biaya, a.sudah_bayar, a.Jenis, a.PPN, a.PPH, "
                    SQL = SQL & "a.jenis_form, a.flag_lunas, a.keterangan, a.jenis_form "
                    SQL = SQL & "from View_Detail_Transaksi_Biaya_import a where flag_lunas is null and No_faktur ='" & LvFak & "' and "
                    SQL = SQL & "Kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang = '" & LvMT & "' and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            kdMaster = Dr("Kode_Master_Kategori_Biaya_Import")
                            SisaHutang = Dr("Biaya") - Dr("sudah_bayar")
                            Dari = Dr("jenis_form")
                            JT = ""
                            lks = Dr("Lokasi")
                            'JT = Dr("jenis_transaksi")
                            'KodeCust = Dr("kode_supplier")
                            'kodeMasterKategoriBI = Dr("kode_master_kategori_biaya_import")

                            If SisaHutang < Val(HilangkanTanda(LvJml)) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub

                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Nomor faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    If SisaHutang = Val(HilangkanTanda(LvJml)) Then

                        If Dari = "1" Then
                            SQL = "Update Detail_Transaksi_Biaya_Import_By_Perusahaan set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            'SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' "
                            ExecuteTrans(SQL)
                        ElseIf Dari = "2" Then
                            SQL = "Update Detail_Transaksi_Biaya_Import_By_Perusahaan set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            'SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                            ExecuteTrans(SQL)
                        ElseIf Dari = "3" Then
                            SQL = "Update Detail_Transaksi_Biaya_Import3_By_Perusahaan set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            'SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' "
                            ExecuteTrans(SQL)
                        ElseIf Dari = "4" Then
                            SQL = "Update Detail_Transaksi_Biaya_Import3_By_Perusahaan set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            'SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                            ExecuteTrans(SQL)
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tabel Asal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                    End If

                    SQL = "insert into detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal(kode_perusahaan, no_val, no_faktur,kode_Perusahaan_biaya_import, Mata_Uang, byr, "
                    SQL = SQL & "Persen_PPN, Persen_PPH, Nilai_PPN, Nilai_PPH, Kode_Master_Kategori_Biaya_Import, Kode_stock_Owner, Tambahan, Total_Tambahan, Subtotal, Jenis_Form, "
                    SQL = SQL & "Kurs_lama, Total_Bayar_Kurs_Lama, Kurs_Baru, Total_Bayar_Kurs_Baru) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "','" & LvFak.Trim & "', '" & LvKP & "', '" & LvMT & "', " & HilangkanTanda(LvJml) & ", "
                    SQL = SQL & "" & LvPersenPPN & ", " & LvPersenPPH & ", " & HilangkanTanda(LvNilaiPPN) & ", " & HilangkanTanda(LvNilaiPPH) & ", '" & LvKdKategori & "', "
                    SQL = SQL & "'" & LvLokasi & "', " & HilangkanTanda(LvTambahan) & ", " & HilangkanTanda(LvTotal) & ", " & Val(HilangkanTanda(LvKursBaruTot)) + Val(HilangkanTanda(LvNilaiPPN)) - Val(HilangkanTanda(LvNilaiPPH)) & ", '" & Dari & "', "
                    SQL = SQL & "'" & HilangkanTanda(LvKursLama) & "', '" & HilangkanTanda(LvKursLamaTot) & "', '" & HilangkanTanda(LvKursBaru) & "', '" & HilangkanTanda(LvKursBaruTot) & "')"
                    ExecuteTrans(SQL)

                    Dim x_no_urut_detail_pelunasan As Integer = 0
                    SQL = "select IDENT_CURRENT('detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal') as urutan"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            x_no_urut_detail_pelunasan = Dr("urutan")
                        End If
                    End Using

                    SQL = "select urut from detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Val = '" & TxtFaktur.Text.Trim & "' and urut = '" & x_no_urut_detail_pelunasan & "'"
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    Dim coa_hutang As String = ""
                    Dim coa_tambahan As String = ""
                    Coa_Selisih = ""
                    Dim akunPPH As String = ""
                    Dim akunPPN As String = ""
                    Dim inisial_faktur As String = ""
                    Dim lokasi_default_PPH As String = ""

                    SQL = "select akun_2 from detail_account_master X where X.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "x.Kode_Master_Kategori_Biaya_import ='" & kdMaster & "' and "
                    SQL = SQL & "x.lokasi='" & LvLokasi & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            coa_hutang = Dr("akun_2")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Lokasi Tidak di Temukan . .  !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End Using

                    Dim jenis_PPH As String = ""
                    SQL = "select Jenis_PPH from Perusahaan_Biaya_Import where "
                    SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and Kode_Perusahaan_Biaya_Import='" & LvKP & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            jenis_PPH = General_Class.CekNULL(dr("Jenis_PPH"))
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Customer Tidak ditemukan . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select top(1) inisial_faktur, ppn_pembelian, Akun_PPH23, Akun_PPH21, Lokasi_default_PPH, Akun_Biaya_Import, akun_selisih_PO from stock_owner where Kode_Stock_Owner='" & LvLokasi & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            coa_tambahan = Dr("Akun_Biaya_Import")
                            inisial_faktur = Dr("inisial_faktur")
                            akunPPN = Dr("ppn_pembelian")
                            lokasi_default_PPH = Dr("Lokasi_default_PPH")
                            coa_selisih = Dr("akun_selisih_PO")

                            If jenis_PPH = "21" Then
                                akunPPH = Dr("Akun_PPH21")
                            ElseIf jenis_PPH = "23" Then
                                akunPPH = Dr("Akun_PPH23")
                            Else
                                akunPPH = ""
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Lokasi Tidak di Temukan . .  !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End Using


#Region "Masuk Pengajuan"
                    'If i = 0 Then
                    '    Get_No_Faktur_Pengajuan()

                    '    SQL = "INSERT INTO pengajuan(kode_perusahaan, no_pengajuan, tanggal, jam, keterangan, userid, grand, pbk, Validasi, No_Val_Declare) "
                    '    SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                    '    SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & TextBoxket.Text & "', '" & UserID & "', "
                    '    SQL = SQL & "" & HilangkanTanda(TextBoxGrand.Text) & ", 'T', NULL, '" & TxtFaktur.Text.Trim & "')"
                    '    ExecuteTrans(SQL)

                    '    SQL = "INSERT INTO detail_pengajuan(kode_perusahaan, no_pengajuan, kode_master_acc, kode_acc, kode_detail_acc, "
                    '    SQL = SQL & "keterangan_detail, tgl_jatuh_tempo, jumlah, kode_bank_tujuan, no_rek_tujuan, nama_penerima, "
                    '    SQL = SQL & "Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Lokasi, Kode_Account) "
                    '    SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Strings.Left(coa_hutang, 1) & "', "
                    '    SQL = SQL & "'" & Strings.Mid(coa_hutang, 2, 1) & "', '" & Strings.Mid(Ganti(coa_hutang), 3) & "', "
                    '    SQL = SQL & "'" & TextBoxket.Text & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', " & HilangkanTanda(TextBoxGrand.Text) & ", "
                    '    SQL = SQL & "'" & ComboBox3.Text & "', '" & TextBox10.Text & "', '" & TextBox7.Text & "', "
                    '    SQL = SQL & "'" & x_alamat & "', '" & x_Kota & "', '" & x_negara & "', '" & x_telp & "','" & LvLokasi & "','" & coa_hutang & "') "
                    '    ExecuteTrans(SQL)

                    '    SQL = "select IDENT_CURRENT('detail_pengajuan') as urutan"
                    '    Using Dr = OpenTrans(SQL)
                    '        If Dr.Read Then
                    '            x_no_urut_det_pengajuan = Dr("urutan")
                    '        End If
                    '    End Using

                    '    SQL = "select urut from detail_pengajuan where kode_perusahaan = '" & KodePerusahaan & "' and "
                    '    SQL = SQL & "no_pengajuan = '" & no_fakturPengajuan & "' and urut = '" & x_no_urut_det_pengajuan & "'"
                    '    Using Dr = OpenTrans(SQL)
                    '        If Not (Dr.Read) Then
                    '            Dr.Close()
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '            Exit Sub
                    '        End If
                    '    End Using

                    '    SQL = "update Val_Pel_Biaya_import_by_Perusahaan_Lokal set no_pengajuan='" & no_fakturPengajuan & "' where "
                    '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and No_Val = '" & TxtFaktur.Text.Trim & "' "
                    '    ExecuteTrans(SQL)

                    '    SQL = "insert into Detail_Pengajuan5(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                    '    SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & coa_hutang & "', '" & HilangkanTanda(TxtTotAwal.Text) & "','0', "
                    '    SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & LvLokasi & "')"
                    '    ExecuteTrans(SQL)

                    '    SQL = "insert into Detail_Pengajuan5(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                    '    SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & coa_tambahan & "', '" & HilangkanTanda(TxtTotTambahan.Text) & "','0', "
                    '    SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & LvLokasi & "')"
                    '    ExecuteTrans(SQL)
                    'End If

                    'If TextBoxtotPPN.Text <> 0 Then
                    '    SQL = "select no_faktur_pajak, nilai_pembagi From Display_Biaya_Import_PPN "
                    '    SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' and "
                    '    SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                    '    SQL = SQL & "and Kode_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                    '    Using ds = BindingTrans(SQL)
                    '        With ds.Tables("MyTable")
                    '            For index As Integer = 0 To .Rows.Count - 1
                    '                SQL = "insert into detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal2(kode_Perusahaan, id_detail_Pelunasan, No_faktur_Pajak, NiLai, Jenis)"
                    '                SQL = SQL & "values('" & KodePerusahaan & "','" & x_no_urut_detail_pelunasan & "', '" & .Rows(index).Item("no_faktur_pajak") & "', '" & .Rows(index).Item("nilai_pembagi") & "', 'PPN')"
                    '                ExecuteTrans(SQL)

                    '                SQL = "insert into Detail_Pengajuan5(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                    '                SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & akunPPN & "', '" & .Rows(index).Item("nilai_pembagi") & "','0', "
                    '                SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & lokasi_default_PPH & "')"
                    '                ExecuteTrans(SQL)
                    '            Next
                    '        End With
                    '    End Using

                    'End If

                    'If TextBoxtotPPH.Text <> 0 Then
                    '    If jenis_PPH = "" Then
                    '        CloseTrans()
                    '        CloseConn()
                    '        MessageBox.Show("Jenis PPH Belum ditentukan . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If

                    '    SQL = "select no_faktur_pajak, nilai_pembagi From Display_Biaya_Import_PPH "
                    '    SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' and "
                    '    SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                    '    SQL = SQL & "and Kode_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                    '    Using ds = BindingTrans(SQL)
                    '        With ds.Tables("MyTable")
                    '            For index As Integer = 0 To .Rows.Count - 1
                    '                SQL = "insert into detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal2(kode_Perusahaan, id_detail_Pelunasan, No_faktur_Pajak, NiLai, Jenis)"
                    '                SQL = SQL & "values('" & KodePerusahaan & "','" & x_no_urut_detail_pelunasan & "', '" & .Rows(index).Item("no_faktur_pajak") & "', '" & .Rows(index).Item("nilai_pembagi") & "', 'PPH')"
                    '                ExecuteTrans(SQL)

                    '                SQL = "insert into Detail_Pengajuan5(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                    '                SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & akunPPH & "', '0', '" & .Rows(index).Item("nilai_pembagi") & "', "
                    '                SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & lokasi_default_PPH & "')"
                    '                ExecuteTrans(SQL)
                    '            Next
                    '        End With
                    '    End Using
                    'End If
#End Region

#Region "Masuk Jurnal"

                    If i = 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0 "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(txt_TotKurs_Lama.Text) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0 "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang, 1),
                                      Strings.Mid(coa_hutang, 2, 1),
                                      Strings.Mid(Ganti(coa_hutang), 3),
                                      KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, HilangkanTanda(txt_TotKurs_Lama.Text), "0", pagenumber, "TSSS")
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1

                            End If
                        End Using

                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_tambahan & "' and debit <> 0 "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(TxtTotTambahan.Text) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_tambahan & "' and debit <> 0 "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_tambahan, 1),
                                      Strings.Mid(coa_tambahan, 2, 1),
                                      Strings.Mid(Ganti(coa_tambahan), 3),
                                      KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, HilangkanTanda(TxtTotTambahan.Text), "0", pagenumber, "TSSS")
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1

                            End If
                        End Using
                    End If

                    If TextBoxtotPPN.Text <> 0 Then
                        SQL = "select no_faktur_pajak, nilai_pembagi From Display_Biaya_Import_PPN "
                        SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' and "
                        SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                        SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                        Using ds = BindingTrans(SQL)
                            With ds.Tables("MyTable")
                                For index As Integer = 0 To .Rows.Count - 1

                                    SQL = "insert into detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal2(kode_Perusahaan, id_detail_Pelunasan, No_faktur_Pajak, NiLai, Jenis)"
                                    SQL = SQL & "values('" & KodePerusahaan & "','" & x_no_urut_detail_pelunasan & "', '" & .Rows(index).Item("no_faktur_pajak") & "', '" & .Rows(index).Item("nilai_pembagi") & "', 'PPN')"
                                    ExecuteTrans(SQL)

                                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPN & "' and debit <> 0 "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Dr.Close()
                                            'update

                                            SQL = "update detail_jurnal set debit = debit+ " & .Rows(index).Item("nilai_pembagi") & " where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPN & "' and debit <> 0 "
                                            ExecuteTrans(SQL)
                                        Else
                                            Dr.Close()
                                            'insert

                                            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPPN, 1),
                                                  Strings.Mid(akunPPN, 2, 1),
                                                  Strings.Mid(Ganti(akunPPN), 3),
                                                  KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, .Rows(index).Item("nilai_pembagi"), "0", pagenumber, "TSSS")
                                            ExecuteTrans(SQL)
                                            pagenumber = pagenumber + 1

                                        End If
                                    End Using
                                Next
                            End With
                        End Using

                    End If


                    If TextBoxtotPPH.Text <> 0 Then
                        If jenis_PPH = "" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jenis PPH Belum ditentukan . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        SQL = "select no_faktur_pajak, nilai_pembagi From Display_Biaya_Import_PPH "
                        SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' and "
                        SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                        SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                        Using ds = BindingTrans(SQL)
                            With ds.Tables("MyTable")
                                For index As Integer = 0 To .Rows.Count - 1
                                    SQL = "insert into detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal2(kode_Perusahaan, id_detail_Pelunasan, No_faktur_Pajak, NiLai, Jenis)"
                                    SQL = SQL & "values('" & KodePerusahaan & "','" & x_no_urut_detail_pelunasan & "', '" & .Rows(index).Item("no_faktur_pajak") & "', '" & .Rows(index).Item("nilai_pembagi") & "', 'PPH')"
                                    ExecuteTrans(SQL)

                                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPH & "' and kredit <> 0 "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Dr.Close()
                                            'update

                                            SQL = "update detail_jurnal set kredit = kredit+ " & .Rows(index).Item("nilai_pembagi") & " where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPH & "' and kredit <> 0 "
                                            ExecuteTrans(SQL)
                                        Else
                                            Dr.Close()
                                            'insert

                                            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPPH, 1),
                                                  Strings.Mid(akunPPH, 2, 1),
                                                  Strings.Mid(Ganti(akunPPH), 3),
                                                  KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, "0", .Rows(index).Item("nilai_pembagi"), pagenumber, "TSSS")
                                            ExecuteTrans(SQL)
                                            pagenumber = pagenumber + 1

                                        End If
                                    End Using
                                Next
                            End With
                        End Using
                    End If


#End Region



                Next

                'SQL = "insert into Detail_Pengajuan5(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                'SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex) & "', '0', '" & HilangkanTanda(TextBoxGrand.Text) & "', "
                'SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Ket_Lokasi_HO & "')"
                'ExecuteTrans(SQL)

                'SQL = "select round(sum(debit), 2) as debit, round(sum(kredit), 2) as kredit from Detail_Pengajuan5 where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "Urut_Detail_Pengajuan = '" & x_no_urut_det_pengajuan & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        If Dr("debit") <> Dr("kredit") Then
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                If Val(HilangkanTanda(TextBoxSelisih.Text)) <> 0 Then
                    If Val(HilangkanTanda(TextBoxSelisih.Text)) > 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih & "' and debit <> 0"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(TextBoxSelisih.Text) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih & "' and debit <> 0"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                pagenumber += 1
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Coa_Selisih, 1),
                                          Strings.Mid(Coa_Selisih, 2, 1),
                                          Strings.Mid(Ganti(Coa_Selisih), 3),
                                          KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, HilangkanTanda(TextBoxSelisih.Text), "0", pagenumber, Ket_Lokasi_HO)
                                ExecuteTrans(SQL)
                            End If
                        End Using
                    Else
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih & "' and kredit <> 0"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set kredit = kredit+ " & Math.Abs(Val(HilangkanTanda(TextBoxSelisih.Text))) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih & "' and kredit <> 0"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                pagenumber += 1
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Coa_Selisih, 1),
                                          Strings.Mid(Coa_Selisih, 2, 1),
                                          Strings.Mid(Ganti(Coa_Selisih), 3),
KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, "0", Math.Abs(Val(HilangkanTanda(TextBoxSelisih.Text))), pagenumber, Ket_Lokasi_HO)
                                ExecuteTrans(SQL)
                            End If
                        End Using
                    End If
                End If
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex) & "' and kredit <> 0 "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & HilangkanTanda(Txt_GrandTotal.Text) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex) & "' and kredit <> 0 "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex), 1),
                      Strings.Mid(ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex), 2, 1),
                      Strings.Mid(Ganti(ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex)), 3),
                      KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, "0", HilangkanTanda(Txt_GrandTotal.Text), pagenumber, "TSSS")
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using


                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("debit") <> Dr("kredit") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else 'update

            ' ''Dim tny As String = MessageBox.Show("Yakin akan diupdate?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            ' ''If tny = vbNo Then Exit Sub

            ' ''Try
            ' ''    OpenConn()

            ' ''    Cmd.Transaction = Cn.BeginTransaction

            ' ''    If CekButtonRole("update_pelunasan_pembelian") = "T" Then
            ' ''        CloseTrans()
            ' ''        CloseConn()
            ' ''        MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''        Exit Sub
            ' ''    End If

            ' ''    Dim kode_voucher_lama As String = ""
            ' ''    SQL = "select kode_voucher, status from val_pemb where "
            ' ''    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''    SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
            ' ''    Using Ds = BindingTrans(SQL)
            ' ''        With Ds.Tables("MyTable")
            ' ''            If .Rows.Count <> 0 Then
            ' ''                kode_voucher_lama = .Rows(0).Item("kode_voucher")
            ' ''                If General_Class.CekNULL(.Rows(0).Item("status")) = "Y" Then
            ' ''                    CloseTrans()
            ' ''                    CloseConn()
            ' ''                    MessageBox.Show("Pelunasan tidak bisa diupdate, karena sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' ''                    Exit Sub
            ' ''                End If
            ' ''            Else
            ' ''                CloseTrans()
            ' ''                CloseConn()
            ' ''                MessageBox.Show("Transaksi tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                Exit Sub
            ' ''            End If
            ' ''        End With
            ' ''    End Using

            ' ''    SQL = "select a.kode_supplier, a.no_faktur, b.no_val, b.byr from pembelian a, detail_val_pemb b where "
            ' ''    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur And "
            ' ''    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''    SQL = SQL & "b.no_val = '" & TxtFaktur.Text.Trim & "'"
            ' ''    Using Ds = BindingTrans(SQL)
            ' ''        With Ds.Tables("MyTable")
            ' ''            If .Rows.Count <> 0 Then
            ' ''                For i As Integer = 0 To .Rows.Count - 1
            ' ''                    SQL = "select hutang from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''                    SQL = SQL & "kode_supplier = '" & .Rows(i).Item("kode_supplier") & "'"
            ' ''                    Using Dr = OpenTrans(SQL)
            ' ''                        If Dr.Read Then
            ' ''                            Dr.Close()

            ' ''                            SQL = "update suppliers set hutang = hutang + " & .Rows(i).Item("byr") & " where "
            ' ''                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''                            SQL = SQL & "kode_supplier = '" & .Rows(i).Item("kode_supplier") & "'"
            ' ''                            ExecuteTrans(SQL)
            ' ''                        Else
            ' ''                            Dr.Close()
            ' ''                            CloseTrans()
            ' ''                            CloseConn()
            ' ''                            MessageBox.Show("Supplier tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' ''                            Exit Sub
            ' ''                        End If
            ' ''                    End Using

            ' ''                    SQL = "Update pembelian set flag_lunas = NULL, "
            ' ''                    SQL = SQL & "Tgl_lunas = NULL, "
            ' ''                    SQL = SQL & "jam_lunas = NULL, "
            ' ''                    SQL = SQL & "uservalidasi = NULL where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''                    SQL = SQL & "no_faktur = '" & .Rows(i).Item("no_faktur") & "'"
            ' ''                    ExecuteTrans(SQL)
            ' ''                Next
            ' ''            Else
            ' ''                CloseTrans()
            ' ''                CloseConn()
            ' ''                MessageBox.Show("Pelunasan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                Exit Sub
            ' ''            End If
            ' ''        End With
            ' ''    End Using

            ' ''    '=============

            ' ''    SQL = "delete from detail_val_pemb where "
            ' ''    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''    SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
            ' ''    ExecuteTrans(SQL)

            ' ''    Dim SisaHutang As Double = 0
            ' ''    Dim JT As String = ""
            ' ''    Dim KodeCust As String = ""

            ' ''    For i As Integer = 0 To ListViewMT11.Items.Count - 1
            ' ''        Get_Isi_Listview(i)

            ' ''        SQL = "select a.total_mua as grand, a.status, a.jenis_transaksi, a.kode_supplier, "
            ' ''        SQL = SQL & "isnull((select sum(x.grand) as ttl_retur from retur_pembelian x where x.kode_perusahaan = a.kode_perusahaan and x.no_faktur_beli = a.no_faktur and x.status is null), 0) as ttl_retur, "
            ' ''        SQL = SQL & "isnull((select sum(y.byr) from val_pemb x, detail_val_pemb y where "
            ' ''        SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
            ' ''        SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
            ' ''        SQL = SQL & "y.no_faktur = a.no_faktur), 0) as ttl_validasi "
            ' ''        SQL = SQL & "from pembelian a where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''        SQL = SQL & "no_faktur = '" & LvFak.Trim & "' "
            ' ''        Using Dr = OpenTrans(SQL)
            ' ''            If Dr.Read Then
            ' ''                SisaHutang = Dr("grand") - Dr("ttl_retur") - Dr("ttl_validasi")
            ' ''                JT = Dr("jenis_transaksi")
            ' ''                KodeCust = Dr("kode_supplier")

            ' ''                If JT = "T" Then
            ' ''                    Dr.Close()
            ' ''                    CloseTrans()
            ' ''                    CloseConn()
            ' ''                    MessageBox.Show("Transaksi ini termasuk transaksi tunai. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                    Exit Sub
            ' ''                ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
            ' ''                    Dr.Close()
            ' ''                    CloseTrans()
            ' ''                    CloseConn()
            ' ''                    MessageBox.Show("Transaksi ini sudah di batalkan. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                    Exit Sub
            ' ''                ElseIf SisaHutang < Val(HilangkanTanda(LvJml)) Then
            ' ''                    Dr.Close()
            ' ''                    CloseTrans()
            ' ''                    CloseConn()
            ' ''                    MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                    Exit Sub
            ' ''                    '' '' '' '' ''ElseIf KodeCust <> LvKdCus Then
            ' ''                    '' '' '' '' ''    Dr.Close()
            ' ''                    '' '' '' '' ''    CloseTrans()
            ' ''                    '' '' '' '' ''    CloseConn()
            ' ''                    '' '' '' '' ''    MessageBox.Show("Supplier sudah diubah sebelumnya. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                    '' '' '' '' ''    Exit Sub
            ' ''                End If
            ' ''            Else
            ' ''                Dr.Close()
            ' ''                CloseTrans()
            ' ''                CloseConn()
            ' ''                MessageBox.Show("Nomor faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                Exit Sub
            ' ''            End If
            ' ''        End Using

            ' ''        SQL = "select hutang from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''        SQL = SQL & "kode_supplier = '" & KodeCust & "'"
            ' ''        Using Dr = OpenTrans(SQL)
            ' ''            If Dr.Read Then
            ' ''                If Dr("hutang") - Val(HilangkanTanda(LvJml)) < 0 Then
            ' ''                    Dr.Close()
            ' ''                    CloseTrans()
            ' ''                    CloseConn()
            ' ''                    '' '' '' '' ''MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat hutang " & LvNmCus & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' ''                    Exit Sub
            ' ''                Else
            ' ''                    Dr.Close()
            ' ''                    'kurangin hutangnya
            ' ''                    SQL = "update suppliers set hutang = hutang - " & HilangkanTanda(LvJml) & " where "
            ' ''                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''                    SQL = SQL & "kode_supplier = '" & KodeCust & "'"
            ' ''                    ExecuteTrans(SQL)
            ' ''                End If
            ' ''            Else
            ' ''                Dr.Close()
            ' ''                CloseTrans()
            ' ''                CloseConn()
            ' ''                MessageBox.Show("Supplier tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' ''                Exit Sub
            ' ''            End If
            ' ''        End Using

            ' ''        If SisaHutang = Val(HilangkanTanda(LvJml)) Then
            ' ''            SQL = "Update pembelian set flag_lunas = 'Y', "
            ' ''            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
            ' ''            SQL = SQL & "jam_lunas = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
            ' ''            SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''            SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
            ' ''            ExecuteTrans(SQL)
            ' ''        End If
            ' ''    Next

            ' ''    SQL = "update val_pemb set keterangan = '" & TextBoxket.Text.Trim & "', "
            ' ''    SQL = SQL & "cara_bayar = '" & arrCrByr.Item(ComboBoxCb.SelectedIndex) & "', "
            ' ''    SQL = SQL & "grand = " & HilangkanTanda(TextBoxtot1.Text) & " where "
            ' ''    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''    SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
            ' ''    ExecuteTrans(SQL)

            ' ''    For i As Integer = 0 To ListViewMT11.Items.Count - 1
            ' ''        Get_Isi_Listview(i)

            ' ''        SQL = "insert into detail_val_pemb(kode_perusahaan, no_val, no_faktur, byr) "
            ' ''        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
            ' ''        SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ")"
            ' ''        ExecuteTrans(SQL)
            ' ''    Next

            ' ''    Dim coa_hutang As String = ""

            ' ''    SQL = "select top(1) hutang from stock_owner_import where kode_perusahaan = '" & KodePerusahaan & "'"
            ' ''    Using dr = OpenTrans(SQL)
            ' ''        If dr.Read Then
            ' ''            coa_hutang = dr("hutang")
            ' ''        Else
            ' ''            dr.Close()
            ' ''            CloseTrans()
            ' ''            CloseConn()
            ' ''            MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''            Exit Sub
            ' ''        End If
            ' ''    End Using

            ' ''    Dim pagenumber As Integer = 0

            ' ''    SQL = "delete from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''    SQL = SQL & "kode_voucher = '" & kode_voucher_lama & "'"
            ' ''    ExecuteTrans(SQL)

            ' ''    SQL = Get_Detail_Jurnal(kode_voucher_lama, Strings.Left(coa_hutang, 1), _
            ' ''                   Strings.Mid(coa_hutang, 2, 1), _
            ' ''                   Strings.Mid(Ganti(coa_hutang), 3), _
            ' ''                   KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim, HilangkanTanda(TextBoxtot1.Text), "0", pagenumber + 1)
            ' ''    ExecuteTrans(SQL)

            ' ''    SQL = Get_Detail_Jurnal(kode_voucher_lama, Strings.Left(ArrAkunCB1.Item(ComboBoxCb.SelectedIndex), 1), _
            ' ''                   Strings.Mid(ArrAkunCB1.Item(ComboBoxCb.SelectedIndex), 2, 1), _
            ' ''                   Strings.Mid(Ganti(ArrAkunCB1.Item(ComboBoxCb.SelectedIndex)), 3), _
            ' ''                   KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim, "0", HilangkanTanda(TextBoxtot1.Text), pagenumber + 1)
            ' ''    ExecuteTrans(SQL)
            ' ''MessageBox.Show("Data berhasil disimpan")
            ' ''Cmd.Transaction.Commit()

            ' ''CloseConn()
            ' ''Catch ex As Exception
            ' ''    CloseTrans()
            ' ''    CloseConn()
            ' ''    MessageBox.Show(ex.Message)
            ' ''    Exit Sub
            ' ''End Try
        End If

        Dim TanyaCetak As String = MessageBox.Show("Mau dicetak?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If TanyaCetak = vbYes Then
            cetak(Txt_SelectedJenis.Text)
        End If

        Kosong()
        DateTimePicker1.Focus()

    End Sub

    Private Sub cetak(ByVal Jenis As String)
        Try

            OpenConn()

            If Jenis.Trim.ToUpper = "IMPORT" Then

                Dim SF As String

                SQL = "select Kode_Perusahaan from View_Laporan_Pelunasan_Biaya_Import_IMPORT  "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & TxtFaktur.Text.Trim & "' "
                SQL = SQL & "and Kode_Master_Kategori_Biaya_Import = '" & TxtKodeKategori.Text & "'"

                SF = "{View_Laporan_Pelunasan_Biaya_Import_IMPORT.Kode_Perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {View_Laporan_Pelunasan_Biaya_Import_IMPORT.No_Val} = '" & TxtFaktur.Text.Trim & "' "
                SF = SF & "and {View_Laporan_Pelunasan_Biaya_Import_IMPORT.Kode_Master_Kategori_Biaya_Import} = '" & TxtKodeKategori.Text & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        Dim CrDoc As New Laporan_Pelunasan_Biaya_Import_By_Perusahaan_IMPORT    'Nama file CR

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = SF
                        With A_Place_For_Printing2
                            .Text = "Pelunasan Biaya Import (IMPORT)"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                        '=============================================================================
                        '=============================================================================
                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.PrintOptions.PrinterName = PrinterName
                        'CrDoc.RecordSelectionFormula = SF

                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        'doctoprint.PrinterSettings.PrinterName = PrinterName
                        'Dim rawKind As Integer
                        'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'For i = doctoprint.PrinterSettings.PaperSizes.Count - 1 To 0 Step -1
                        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
                        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        '        CrDoc.PrintOptions.PaperSize = rawKind
                        '        Exit For
                        '    End If
                        'Next
                        'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        'CrDoc.PrintToPrinter(1, False, 1, 99)
                    End If
                End Using

            ElseIf Jenis.Trim.ToUpper = "LOKAL" Then
                Dim SF As String

                SQL = "select Kode_Perusahaan from View_Laporan_Pelunasan_Biaya_Import_LOKAL  "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & TxtFaktur.Text.Trim & "' "
                SQL = SQL & "and Kode_Master_Kategori_Biaya_Import = '" & TxtKodeKategori.Text & "'"

                SF = "{View_Laporan_Pelunasan_Biaya_Import_LOKAL.Kode_Perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {View_Laporan_Pelunasan_Biaya_Import_LOKAL.No_Val} = '" & TxtFaktur.Text.Trim & "' "
                SF = SF & "and {View_Laporan_Pelunasan_Biaya_Import_LOKAL.Kode_Master_Kategori_Biaya_Import} = '" & TxtKodeKategori.Text & "' "
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        Dim CrDoc As New Laporan_Pelunasan_Biaya_Import_By_Perusahaan_LOKAL    'Nama file CR

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = SF
                        With A_Place_For_Printing2
                            .Text = "Pelunasan Biaya Import (LOKAL)"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                        '======================================================
                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.PrintOptions.PrinterName = PrinterName
                        'CrDoc.RecordSelectionFormula = SF
                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        'doctoprint.PrinterSettings.PrinterName = PrinterName
                        'Dim rawKind As Integer
                        'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'For i = doctoprint.PrinterSettings.PaperSizes.Count - 1 To 0 Step -1
                        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
                        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        '        CrDoc.PrintOptions.PaperSize = rawKind
                        '        Exit For
                        '    End If
                        'Next
                        'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        'CrDoc.PrintToPrinter(1, False, 1, 99)
                    End If
                End Using

            End If



            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

End Class