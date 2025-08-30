'Imports Org.BouncyCastle.Utilities

Imports System.Globalization

'Imports Microsoft.SqlServer.Server
'Imports Microsoft.SqlServer.Server

Public Class EMI_Pelunasan_Asset

    Dim arrCrByr1, ArrAkunCB1, ArrAkunRek1, ArrPencarian As New ArrayList
    Dim arrCrByr2, ArrAkunCB2 As New ArrayList
    Dim arrKodeBankTujuan, arrRekeningTujuan, arrNamaPemilikiRekTujuan, arrKotaTujuan, arrNegaraTujuan As New ArrayList
    Dim ArrMataUangRek As New ArrayList
    Dim ArrNP1 As New ArrayList
    Dim ArrNP2 As New ArrayList
    Dim Cell1_DiBayar As Integer = 12
    Dim Cell1_Jenis1 As Integer = 14
    Dim Cell1_Jenis2 As Integer = 15
    Dim Cell1_JenisLokasi As Integer = 17
    Dim Cell1_KdKategori As Integer = 5
    Dim Cell1_KdPerusahaanBiayaImport As Integer = 3
    Dim Cell1_Keterangan As Integer = 1
    Dim Cell1_KursLama As Integer = 8
    Dim Cell1_Lokasi As Integer = 16
    Dim Cell1_MataUang As Integer = 7
    Dim Cell1_NmKategori As Integer = 6
    Dim Cell1_NmPerusahaanBiayaImport As Integer = 4
    Dim Cell1_No_FakPO As Integer = 18
    Dim Cell1_NoPemb As Integer = 0
    Dim Cell1_PPH As Integer = 10
    Dim Cell1_PPN As Integer = 9
    Dim Cell1_Sisa As Integer = 13
    Dim Cell1_tgl_FakPO As Integer = 19
    Dim Cell1_TglPemb As Integer = 2
    Dim Cell1_Total As Integer = 11
    Dim CellChecklistDP As Integer = 34
    Dim CellDPDipakai As Integer = 32
    Dim CellDPP As Integer = 31
    'Cell dgv 2
    Dim CellFak As Integer = 0

    Dim CellJenis1 As Integer = 29
    Dim CellJenis2 As Integer = 30
    Dim CellJml As Integer = 7
    Dim CellJns As Integer = 19
    Dim CellJnsBiaya As Integer = 20
    Dim CellKdBankTujuan As Integer = 23
    Dim CellKdKategori As Integer = 4
    Dim CellKotaTujuan As Integer = 27
    Dim CellKP As Integer = 2
    Dim CellKursBaru As Integer = 11
    Dim CellKursBaruTot As Integer = 12
    Dim CellKursLama As Integer = 9
    Dim CellKursLamaTot As Integer = 10
    Dim CellLokasi As Integer = 18
    Dim CellMT As Integer = 6
    Dim CellNegaraTujuan As Integer = 28
    Dim CellNilaiPPH As Integer = 17
    Dim CellNilaiPPN As Integer = 16
    Dim CellNmKategori As Integer = 5
    Dim CellNmTujuan As Integer = 25
    Dim CellNP As Integer = 3
    Dim CellPersenPPH As Integer = 15
    Dim CellPersenPPN As Integer = 14
    Dim CellPO As Integer = 1
    Dim CellRekTujuan As Integer = 21
    Dim CellRekTujuanData As Integer = 24
    Dim CellTambahan As Integer = 8
    Dim CellTglPelData As Integer = 26
    Dim CellTglPelDisplay As Integer = 22
    Dim CellTotal As Integer = 13
    Dim CellTotDP As Integer = 33
    Dim checkSisaHutang As String
    Dim JT As String
    Dim judulForm As String = "Pengajuan Pembayaran Hutang"

    Dim kursUangBaru As Double
    Dim Lv1_Jenis1, Lv1_Jenis2, Lv1_Lokasi, Lv1_JenisLokasi As String
    Dim Lv1_NoPemb, Lv1_Keterangan, Lv1_TglPemb, Lv1_KdPerusahaanBiaya, Lv1_NmPerusahaanBiaya, Lv1_KdKategori, Lv1_NmKategori, Lv1_MataUang, Lv1_KursLama, Lv1_PPN, Lv1_PPH, Lv1_Total, Lv1_Dibayar, Lv1_Sisa, Lv1_No_FakPO, Lv1_Tgl_FakPO As String
    Dim LvDet_KotaTujuan, LvDet_NegaraTujuan, LvDet_Jenis1, LvDet_Jenis2, LvDPP, LvDPdiPakai, LvTotDP, LvChecklistDP As String
    Dim LvFak, LvPO As String
    Dim LvKP, LvNP, LvKdKategori, LvNmKategori, LvMT, LvJml, LvTambahan, LvTotal, LvPersenPPN, LvPersenPPH, LvNilaiPPN, LvNilaiPPH As String
    Dim LvLokasi, LvJns, LvJnsBiaya, LvKurs, LvKursLama, LvKursLamaTot, LvKursBaru, LvKursBaruTot, LvDet_RekTujuan, LvDet_TglPelDisplay, LvDet_KdBank, LvDet_RekTujuanData, LvDet_NmTujuan, LvDet_TglPelData As String

    Dim no_fakturDeposit As String
    Dim no_fakturPengajuan, no_fakturToken As String
    Dim tempDataPajak As New List(Of (noPO As String, kdPerusahaanImport As String, kodeTarif As String, persentase As String, akun As String, isPPN As Boolean))
    Dim varMataUang As String
    Dim x_alamat, x_Kota, x_negara, x_telp As String

    Private Sub Validasi_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        'ListViewMT1.Columns.Add("No Faktur", 110, HorizontalAlignment.Left) '0
        'ListViewMT1.Columns.Add("ID Rencana", 0, HorizontalAlignment.Center) '1
        'ListViewMT1.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '2
        'ListViewMT1.Columns.Add("Tgl", 0, HorizontalAlignment.Center) '3
        'ListViewMT1.Columns.Add("Kode Perusahaan", 0, HorizontalAlignment.Left) '4
        'ListViewMT1.Columns.Add("Nama Perusahaan", 250, HorizontalAlignment.Left) '5
        'ListViewMT1.Columns.Add("Kode Kategori", 0, HorizontalAlignment.Left) '6
        'ListViewMT1.Columns.Add("Kategori", 200, HorizontalAlignment.Left) '7
        'ListViewMT1.Columns.Add("Mata Uang", 80, HorizontalAlignment.Center) '8
        'ListViewMT1.Columns.Add("Total", 120, HorizontalAlignment.Right) '9
        'ListViewMT1.Columns.Add("Dibayar", 120, HorizontalAlignment.Right) '10
        'ListViewMT1.Columns.Add("PPN", 0, HorizontalAlignment.Right) '11
        'ListViewMT1.Columns.Add("PPH", 0, HorizontalAlignment.Right) '12
        'ListViewMT1.Columns.Add("Lokasi", 100, HorizontalAlignment.Left).DisplayIndex = 1 '13
        'ListViewMT1.Columns.Add("Jenis Form", 0, HorizontalAlignment.Right) '14
        'ListViewMT1.Columns.Add("Sisa", 120, HorizontalAlignment.Right) '15
        'ListViewMT1.Columns.Add("Kurs Lama", 0, HorizontalAlignment.Right) '16
        'ListViewMT1.Columns.Add("Jenis", 0, HorizontalAlignment.Right) '17
        'ListViewMT1.Columns.Add("Jenis1", 0, HorizontalAlignment.Right) '18
        'ListViewMT1.Columns.Add("Jenis2", 0, HorizontalAlignment.Right) '19
        'ListViewMT1.View = View.Details

        ListViewMT1.Columns.Add("No Pembelian", 130, HorizontalAlignment.Left) '0
        ListViewMT1.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '1
        ListViewMT1.Columns.Add("Tanggal Pembelian", 120, HorizontalAlignment.Center).DisplayIndex = 1 '2
        ListViewMT1.Columns.Add("Kd_Perusahaan_Biaya_Import", 0, HorizontalAlignment.Left) '3
        ListViewMT1.Columns.Add("Nama Perusahaan", 190, HorizontalAlignment.Left) '4
        ListViewMT1.Columns.Add("Kd_Master_KAtegori", 0, HorizontalAlignment.Left) '5
        ListViewMT1.Columns.Add("Kategori", 200, HorizontalAlignment.Left) '6
        ListViewMT1.Columns.Add("Mata Uang", 90, HorizontalAlignment.Center) '7
        ListViewMT1.Columns.Add("Kurs Lama", 0, HorizontalAlignment.Right) '8
        ListViewMT1.Columns.Add("PPN", 70, HorizontalAlignment.Right) '9
        ListViewMT1.Columns.Add("PPH", 70, HorizontalAlignment.Right) '10
        ListViewMT1.Columns.Add("Total", 120, HorizontalAlignment.Right) '11
        ListViewMT1.Columns.Add("Dibayar", 120, HorizontalAlignment.Right) '12
        ListViewMT1.Columns.Add("Sisa", 120, HorizontalAlignment.Right) '13
        ListViewMT1.Columns.Add("Jenis1", 0, HorizontalAlignment.Left) '14
        ListViewMT1.Columns.Add("Jenis2", 0, HorizontalAlignment.Left) '15
        ListViewMT1.Columns.Add("Lokasi", 0, HorizontalAlignment.Left) '16
        ListViewMT1.Columns.Add("Jenis_Lokasi", 0, HorizontalAlignment.Left) '17
        ListViewMT1.Columns.Add("No PO", 130, HorizontalAlignment.Left).DisplayIndex = 2 '18
        ListViewMT1.Columns.Add("Tanggal PO", 120, HorizontalAlignment.Center).DisplayIndex = 3 '19
        ListViewMT1.View = View.Details

        ListViewMT11.Columns.Add("No Faktur", 140, HorizontalAlignment.Left)  '0
        ListViewMT11.Columns.Add("Tgl", 0, HorizontalAlignment.Center) '1
        ListViewMT11.Columns.Add("Kode Perusahaan", 0, HorizontalAlignment.Left) '2
        ListViewMT11.Columns.Add("Nama Perusahaan", 250, HorizontalAlignment.Left) '3
        ListViewMT11.Columns.Add("Kode Kategori", 0, HorizontalAlignment.Left) '4
        ListViewMT11.Columns.Add("Kategori Perusahaan", 200, HorizontalAlignment.Left) '5
        ListViewMT11.Columns.Add("Mata Uang", 80, HorizontalAlignment.Center) '6
        ListViewMT11.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '7
        ListViewMT11.Columns.Add("Nilai Tambahan", 0, HorizontalAlignment.Right) '8
        ListViewMT11.Columns.Add("Kurs Lama", 120, HorizontalAlignment.Right) '9
        ListViewMT11.Columns.Add("Total Kurs Lama", 130, HorizontalAlignment.Right) '10
        ListViewMT11.Columns.Add("Kurs Baru", 120, HorizontalAlignment.Right) '11
        ListViewMT11.Columns.Add("Total Kurs Baru", 130, HorizontalAlignment.Right) '12
        ListViewMT11.Columns.Add("Total", 0, HorizontalAlignment.Right) '13
        ListViewMT11.Columns.Add("PPN", 0, HorizontalAlignment.Right) '14
        ListViewMT11.Columns.Add("PPH", 0, HorizontalAlignment.Right) '15
        ListViewMT11.Columns.Add("Nilai PPN", 90, HorizontalAlignment.Right) '16
        ListViewMT11.Columns.Add("Nilai PPH", 90, HorizontalAlignment.Right) '17
        ListViewMT11.Columns.Add("Lokasi", 120, HorizontalAlignment.Left).DisplayIndex = 1 '18
        ListViewMT11.Columns.Add("Jenis Form", 0, HorizontalAlignment.Right) '19
        ListViewMT11.Columns.Add("JenisBiaya", 0, HorizontalAlignment.Right) '20
        ListViewMT11.Columns.Add("Rekening Tujuan", 150, HorizontalAlignment.Right) '21
        ListViewMT11.Columns.Add("Tanggal Pelunasan", 150, HorizontalAlignment.Right) '22
        ListViewMT11.Columns.Add("KodeBank", 0, HorizontalAlignment.Right) '23
        ListViewMT11.Columns.Add("Rek Tujuan", 0, HorizontalAlignment.Right) '24
        ListViewMT11.Columns.Add("Nama Tujuan", 0, HorizontalAlignment.Right) '25
        ListViewMT11.Columns.Add("TanggalPelunasan", 0, HorizontalAlignment.Right) '26
        ListViewMT11.Columns.Add("KotaTujuan", 0, HorizontalAlignment.Right) '27
        ListViewMT11.Columns.Add("NegaraTujuan", 0, HorizontalAlignment.Right) '28
        ListViewMT11.Columns.Add("Jenis1", 0, HorizontalAlignment.Right) '29
        ListViewMT11.Columns.Add("Jenis2", 0, HorizontalAlignment.Right) '30
        ListViewMT11.Columns.Add("DPP", 0, HorizontalAlignment.Right) '31
        ListViewMT11.Columns.Add("DP Dipakai", 150, HorizontalAlignment.Right) '32
        ListViewMT11.Columns.Add("TotDP", 150, HorizontalAlignment.Right) '33
        ListViewMT11.Columns.Add("checklistDP", 100, HorizontalAlignment.Right) '34
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

        Cmb_Rekening_Tujuan.SelectedIndex = -1
        Cmb_Rekening_Tujuan.Text = ""
        Dtp_TglBayar.Value = DateTime.Now

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBoxNP1.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Nama Perusahaan", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxNP1.DroppedDown = True : ComboBoxNP1.Focus() : Exit Sub
        ElseIf ComboBoxMT1.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Mata Uang", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxMT1.DroppedDown = True : ComboBoxMT1.Focus() : Exit Sub
        ElseIf Cmb_Jenis.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Jenis Perusahaan", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis.DroppedDown = True : Cmb_Jenis.Focus() : Exit Sub
        ElseIf Txt_KursBaru.Text.Trim.Length = 0 Then
            MessageBox.Show("Kurs Tidak Boleh Kosong", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis.DroppedDown = True : Cmb_Jenis.Focus() : Exit Sub
        End If

        Cari("Tidak1")
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click

        If ListViewMT11.Items.Count <> 0 Then

            Try
                OpenConn()

                '========================================
                '=     CEK APAKAH USER SUDAH SIMPAN     =
                '========================================
                SQL = "select Kode_Perusahaan from EMI_Pelunasan WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & TxtFaktur.Text.Trim & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count = 0 Then

                            'Delete PPN
                            SQL = "delete from Display_Biaya_Import_PPN_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
                            ExecuteTrans(SQL)

                            'Delete PPh Detail
                            SQL = "select Kode_Perusahaan_Biaya_Import, No_Faktur from Display_Biaya_Import_PPH_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For i As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
                                        'Delete PPH Detail
                                        SQL = "delete from Display_Biaya_Import_PPH_Detail_Barang_Lain where kode_perusahaan = '" & KodePerusahaan & "'  "
                                        SQL = SQL & "and Kode_Perusahaan_Biaya_Import = '" & Ds1.Tables("MyTable").Rows(i).Item("Kode_Perusahaan_Biaya_Import") & "'  "
                                        SQL = SQL & "and No_BiayaImportPPH = '" & Ds1.Tables("MyTable").Rows(i).Item("No_Faktur") & "' "
                                        ExecuteTrans(SQL)
                                    Next
                                End If
                            End Using

                            'Delete PPh
                            SQL = "delete from Display_Biaya_Import_PPH_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
                            ExecuteTrans(SQL)

                        End If
                    End With
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        End If

        Kosong()
        DateTimePicker1.Focus()
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
        ElseIf ListViewMT11.Items.Count = 0 Then
            MessageBox.Show("Tidak Ada Data. . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

                Dim Coa_Selisih As String = ""
                Dim SisaHutang As Double = 0
                Dim JT As String = ""
                Dim Jenis1 As String = ""
                Dim Jenis2 As String = ""
                Dim KodeCust As String = ""
                Dim lks As String = ""
                Dim kdMaster As String = ""
                Dim x_no_urut_det_pengajuan As Integer = 0


                Dim jenis_DP As String = ""
                For i As Integer = 0 To ListViewMT11.Items.Count - 1
                    Get_Isi_Listview(i)

                    jenis_DP = LvChecklistDP

                Next

                Dim No_pengajuan As String = ""
                Dim Kode_Voucher As String = ""
                Dim Kode_VoucherX As String = ""

                Dim pagenumber As Integer = 1
                Dim NilaiTranfer As Double = HilangkanTanda(Txt_GrandTotal.Text) - Val(HilangkanTanda(TextBoxtotPPH.Text))

                If jenis_DP = "Y" Then
                    No_pengajuan = "NULL"

                    Kode_Voucher = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPemb, KodePerusahaan)
                    Kode_VoucherX = "'" & Kode_Voucher & "'"



                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                    SQL = SQL & "'" & Kode_Voucher & "', "
                    SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    SQL = SQL & "'" & KodeProyek & "', 'Pelunasan hutang " & TxtFaktur.Text.Trim & "', '', "
                    SQL = SQL & "'-', '" & UserID & "')"
                    ExecuteTrans(SQL)

                Else

                    Get_No_Faktur_Pengajuan()

                    SQL = "INSERT INTO pengajuan_Temp(kode_perusahaan, no_pengajuan, tanggal, jam, keterangan, userid, grand, pbk, "
                    SQL = SQL & "Validasi) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & TextBoxket.Text & "', '" & UserID & "', "
                    SQL = SQL & "" & HilangkanTanda(NilaiTranfer) & ", 'T', NULL)"
                    ExecuteTrans(SQL)

                    No_pengajuan = "'" & no_fakturPengajuan & "'"
                    Kode_VoucherX = "NULL"

                End If

                SQL = "insert into EMI_Pelunasan_Barang_Lain (Kode_Perusahaan, No_Val, Tanggal, Jam, Keterangan, UserValidasi, Kode_Voucher, "
                SQL = SQL & "Mata_Uang, Total, Total_PPN, Total_PPH, Grand_Total, "
                SQL = SQL & "Total_Kurs_Lama, Total_Kurs_Baru, jenis, No_Pengajuan) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & TextBoxket.Text.Trim & "', '" & UserID & "', " & Kode_VoucherX & ", "
                SQL = SQL & "'" & ComboBoxMT1.Text & "', '" & HilangkanTanda(TextBoxtot1.Text) & "', '" & HilangkanTanda(TextBoxtotPPN.Text) & "', "
                SQL = SQL & "'" & HilangkanTanda(TextBoxtotPPH.Text) & "' , '" & HilangkanTanda(Txt_GrandTotal.Text) & "', '" & HilangkanTanda(txt_TotKurs_Lama.Text) & "', "
                SQL = SQL & " '" & HilangkanTanda(txt_TotKurs_Baru.Text) & "', '" & Txt_SelectedJenis.Text & "', " & No_pengajuan & ")"
                ExecuteTrans(SQL)

                For i As Integer = 0 To ListViewMT11.Items.Count - 1
                    Get_Isi_Listview(i)

                    Dim kodeMasterKategoriBI As String = ""

                    Dim No_PO As String = ""
                    SQL = "select No_Faktur, No_PO from EMI_Pembelian_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Faktur = '" & LvFak & "' "
                    SQL = SQL & "and status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            No_PO = Dr("No_PO")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data PO Tidak ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select Kode_Master_Kategori_Biaya_Import, Nilai, sudah_bayar, Jenis1, Jenis2, lokasi, PPN, PPH "
                    SQL = SQL & "from View_EMI_Pelunasan_Barang_Lain "
                    SQL = SQL & "where flag_lunas is null and kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & LvFak & "' "
                    SQL = SQL & "and Kode_Perusahaan_Biaya_Import = '" & LvKP & "' and Mata_uang = '" & LvMT & "'  "
                    SQL = SQL & "and Kode_Master_Kategori_Biaya_Import = '" & LvKdKategori & "' and lokasi = '" & LvLokasi & "' "
                    Using Ds = BindingTrans(SQL)
                        If Ds.Tables("MyTable").Rows.Count <> 0 Then

                            Dim nilai_total_hutang As Double = 0

                            If Ds.Tables("MyTable").Rows(0).Item("Jenis1").Trim.ToUpper = "SUPPLIER" Then

                                '========================
                                '=     GET DATA PPH     =
                                '========================
                                Dim SumPersenPPH As Double = 0
                                Dim SumNilaiPPH As Double = 0

                                Dim TotPPH As Double = 0

                                SQL = "select Kode_Tarif, Persentase, Flag_PPN, Kode_Akun from EMI_Detail_PPH_PO_Barang_Lain  "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and No_Faktur = '" & No_PO & "' and flag_ppn is null"
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                        For k As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                                            Dim PersenPPH = Ds2.Tables("MyTable").Rows(k).Item("Persentase")
                                            SumPersenPPH += PersenPPH
                                            SumNilaiPPH += Math.Round((Ds.Tables("MyTable").Rows(0).Item("Nilai") * PersenPPH / 100), 0)

                                        Next
                                    End If
                                End Using

                                '========================
                                '=     GET DATA PPN     =
                                '========================
                                Dim PPNSup As Double = 0
                                SQL = "select top 1 Persentase "
                                SQL = SQL & "from EMI_Detail_PPH_PO_Barang_Lain "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and No_Faktur = '" & No_PO & "' "
                                SQL = SQL & "and Flag_PPN = 'Y'"
                                Using Ds3 = BindingTrans(SQL)
                                    If Ds3.Tables("MyTable").Rows.Count <> 0 Then
                                        PPNSup = Format(Val(HilangkanTanda(Ds3.Tables("MyTable").Rows(0).Item("Persentase"))), "N2")
                                    Else
                                        PPNSup = Format(0, "N2")
                                    End If
                                End Using

                                Dim nilai_PPN As Double = Math.Round((Ds.Tables("MyTable").Rows(0).Item("Nilai") * PPNSup / 100), 0)

                                nilai_total_hutang = Ds.Tables("MyTable").Rows(0).Item("Nilai") + nilai_PPN - SumNilaiPPH
                            Else
                                nilai_total_hutang = Ds.Tables("MyTable").Rows(0).Item("Nilai") + Math.Round((Ds.Tables("MyTable").Rows(0).Item("Nilai") * Ds.Tables("MyTable").Rows(0).Item("PPN") / 100), 0) - Math.Round((Ds.Tables("MyTable").Rows(0).Item("Nilai") * Ds.Tables("MyTable").Rows(0).Item("PPH") / 100), 0)
                            End If

                            Dim nilai_total_bayar As Double = Ds.Tables("MyTable").Rows(0).Item("sudah_bayar") '+ Math.Round((Dr("sudah_bayar") * Dr("PPN") / 100), 0) - Math.Round((Dr("sudah_bayar") * Dr("PPH") / 100), 0)

                            kdMaster = Ds.Tables("MyTable").Rows(0).Item("Kode_Master_Kategori_Biaya_Import")
                            SisaHutang = nilai_total_hutang - nilai_total_bayar
                            Jenis1 = Ds.Tables("MyTable").Rows(0).Item("Jenis1")
                            Jenis2 = Ds.Tables("MyTable").Rows(0).Item("Jenis2")
                            JT = ""
                            lks = Ds.Tables("MyTable").Rows(0).Item("Lokasi")
                            'JT = Dr("jenis_transaksi")
                            'KodeCust = Dr("kode_supplier")
                            'kodeMasterKategoriBI = Dr("kode_master_kategori_biaya_import")

                            If SisaHutang < Val(HilangkanTanda(LvJml)) Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub

                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Nomor faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    'CEK Apakah sudah Lunas
                    If SisaHutang = Val(HilangkanTanda(LvJml)) Then
                        If Jenis1.Trim.ToUpper = "AGENT" And Jenis2.Trim.ToUpper = "A" Then
                            'UNtuk Jenis 1 agent & Jenis 2 A
                            SQL = "Update Detail_Transaksi_Biaya_Import_By_Perusahaan_Barang_Lain set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            'SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' "
                            ExecuteTrans(SQL)
                        ElseIf Jenis1.Trim.ToUpper = "AGENT" And Jenis2.Trim.ToUpper = "B" Then
                            'UNtuk Jenis 1 agent & Jenis 2 B
                            SQL = "Update Detail_Transaksi_Biaya_Import3_By_Perusahaan_Barang_Lain set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            'SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' "
                            ExecuteTrans(SQL)
                        ElseIf Jenis1.Trim.ToUpper = "AGENT" And Jenis2.Trim.ToUpper = "C" Then
                            'UNtuk Jenis 1 agent & Jenis 2 B
                            SQL = "Update Detail_Transaksi_Biaya_lokal_By_Perusahaan_Barang_Lain set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            'SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' "
                            ExecuteTrans(SQL)
                        ElseIf Jenis1.Trim.ToUpper = "SUPPLIER" And Jenis2.Trim.ToUpper = "A" Then
                            'UNtuk Jenis 1 Supplier & Jenis 2 a
                            SQL = "Update EMI_Pembelian_Barang_Lain set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' "
                            'SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                            ExecuteTrans(SQL)
                        ElseIf Jenis1.Trim.ToUpper = "SUPPLIER" And Jenis2.Trim.ToUpper = "B" Then
                            'UNtuk Jenis 1 Supplier & Jenis 2 a
                            SQL = "Update Pelunasan_Pembelian_Barang_Lain set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode='" & LvKdKategori & "' "
                            'SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                            ExecuteTrans(SQL)
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tabel Asal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                    End If

                    Dim selisih As Double = Val(HilangkanTanda(LvKursBaruTot)) - Val(HilangkanTanda(LvKursLamaTot))
                    Dim Nilai_Total As Double = ((Val(HilangkanTanda(LvKursLamaTot)) + Val(HilangkanTanda(LvNilaiPPN))) - Val(HilangkanTanda(LvNilaiPPH))) + selisih
                    Dim Nilai_hutang As Double = ((Val(HilangkanTanda(LvKursLamaTot)) + Val(HilangkanTanda(LvNilaiPPN))))

                    SQL = "insert into EMI_Pelunasan_Detail_Barang_Lain(kode_perusahaan, no_val, no_faktur,kode_Perusahaan_biaya_import, Mata_Uang, byr, "
                    SQL = SQL & "Persen_PPN, Persen_PPH, Nilai_PPN, Nilai_PPH, Kode_Master_Kategori_Biaya_Import, Kode_stock_Owner, Tambahan, Total_Tambahan, Subtotal, "
                    SQL = SQL & "Kurs_lama, Total_Bayar_Kurs_Lama, Kurs_Baru, Total_Bayar_Kurs_Baru, Kode_Bank_Tujuan, No_Rek_Tujuan, Nama_Penerima, Kota_Penerima, Negara_Penerima, Tanggal_Bayar, Jenis1, Jenis2, DP_Digunakan) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "','" & LvFak.Trim & "', '" & LvKP & "', '" & LvMT & "', " & HilangkanTanda(LvJml) & ", "
                    SQL = SQL & "" & LvPersenPPN & ", " & LvPersenPPH & ", " & HilangkanTanda(LvNilaiPPN) & ", " & HilangkanTanda(LvNilaiPPH) & ", '" & LvKdKategori & "', "
                    SQL = SQL & "'" & LvLokasi & "', " & HilangkanTanda(LvTambahan) & ", " & HilangkanTanda(LvTotal) & ", " & Nilai_Total & ", "
                    SQL = SQL & "'" & HilangkanTanda(LvKursLama) & "', '" & HilangkanTanda(LvKursLamaTot) & "', '" & HilangkanTanda(LvKursBaru) & "', '" & HilangkanTanda(LvKursBaruTot) & "', "
                    SQL = SQL & "'" & LvDet_KdBank & "', '" & LvDet_RekTujuanData & "', '" & LvDet_NmTujuan & "', '" & LvDet_KotaTujuan & "', '" & LvDet_NegaraTujuan & "', '" & LvDet_TglPelData & "', "
                    SQL = SQL & "'" & LvDet_Jenis1 & "', '" & LvDet_Jenis2 & "', '" & HilangkanTanda(LvDPdiPakai) & "')"
                    ExecuteTrans(SQL)

                    Dim x_no_urut_detail_pelunasan As Integer = 0
                    SQL = "select IDENT_CURRENT('EMI_Pelunasan_Detail_Barang_Lain') as urutan"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            x_no_urut_detail_pelunasan = Dr("urutan")
                        End If
                    End Using

                    SQL = "select urut from EMI_Pelunasan_Detail_Barang_Lain where kode_perusahaan = '" & KodePerusahaan & "' and "
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

                    '====================================
                    '=     INSERT DETAIL DP DiPakai     =
                    '====================================

                    Dim DpDigunakan As Double = Val(HilangkanTanda(LvDPdiPakai))

                    SQL = "with Cte as ( select a.Nilai as Nilai_DP, a.no_urut, ( "
                    SQL = SQL & "(a.Nilai-isnull((select sum(x.nilai) from EMI_Transaksi_Pembayaran_Dimuka_Asset_Pajak x where "
                    SQL = SQL & "x.kode_perusahaan=a.kode_perusahaan and x.no_faktur=a.No_Transaksi and x.flag_ppn is null ),0)) - "
                    SQL = SQL & "ISNULL(( select z.nilai from EMI_Pelunasan_Detail_DP_Barang_Lain z, EMI_Pelunasan_Barang_Lain w where "
                    SQL = SQL & "z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
                    SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
                    SQL = SQL & " ), 0) ) as Sisa "
                    SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset a, EMI_Transaksi_Pembayaran_Dimuka_Asset b  "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "And a.No_Transaksi = b.No_Transaksi  "
                    SQL = SQL & "And b.Status Is null  "
                    SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
                    SQL = SQL & "and a.No_Fak_PO in ( "

                    SQL = SQL & "select x.No_FakInduk "
                    SQL = SQL & "from Emi_Pembelian_PO_Barang_Lain z, Emi_Pembelian_PO_det_Barang_Lain x "
                    SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                    SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                    SQL = SQL & "and z.Status is null "
                    SQL = SQL & "and x.Kode_Perusahaan = '001' "
                    SQL = SQL & "and x.No_Faktur = '" & No_PO & "' "
                    SQL = SQL & "group by x.No_FakInduk ) "

                    SQL = SQL & ")select no_urut, isnull(sisa,0) as Nilai_DP from Cte where sisa<>0 "
                    SQL = SQL & "order by No_Urut "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For j As Integer = 0 To .Rows.Count - 1

                                    If DpDigunakan < 0 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalaham", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    If DpDigunakan = 0 Then
                                        Exit For
                                    End If

                                    Dim Tot_DP As Double = .Rows(j).Item("Nilai_DP")


                                    If Tot_DP >= DpDigunakan Then


                                        'SQL = "update EMI_Transaksi_Pembayaran_Dimuka_Detail set Nilai = Nilai - '" & HilangkanTanda(Nilai) & "', "
                                        'SQL = SQL & "Nilai_IDR = Nilai_IDR - '" & HilangkanTanda(DpDigunakan) & "'"
                                        'SQL = SQL & "where No_Transaksi= '" & .Rows(j).Item("No_Fak_DP") & "' and No_Fak_PO = '" & .Rows(j).Item("No_FakInduk") & "' "
                                        'ExecuteTrans(SQL)

                                        SQL = "insert into EMI_Pelunasan_Detail_DP_Barang_Lain (Kode_Perusahaan, no_val, Urut_Detail_Pelunasan, Urut_DP, nilai) values "
                                        SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & x_no_urut_detail_pelunasan & "', "
                                        SQL = SQL & "'" & .Rows(j).Item("no_urut") & "', '" & HilangkanTanda(DpDigunakan) & "')"
                                        ExecuteTrans(SQL)

                                        DpDigunakan = 0
                                    Else

                                        'SQL = "update EMI_Transaksi_Pembayaran_Dimuka_Detail set Nilai = Nilai - '" & HilangkanTanda(Nilai) & "', "
                                        'SQL = SQL & "Nilai_IDR = Nilai_IDR - '" & HilangkanTanda(.Rows(j).Item("Nilai")) & "'"
                                        'SQL = SQL & "where No_Transaksi= '" & .Rows(j).Item("No_Fak_DP") & "' and No_Fak_PO = '" & .Rows(j).Item("No_FakInduk") & "' "
                                        'ExecuteTrans(SQL)

                                        SQL = "insert into EMI_Pelunasan_Detail_DP_Barang_Lain (Kode_Perusahaan, no_val, Urut_Detail_Pelunasan, Urut_DP, nilai) values "
                                        SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & x_no_urut_detail_pelunasan & "', "
                                        SQL = SQL & "'" & .Rows(j).Item("no_urut") & "', '" & HilangkanTanda(Tot_DP) & "')"
                                        ExecuteTrans(SQL)

                                        DpDigunakan -= Tot_DP
                                    End If

                                Next
                            End If
                        End With
                    End Using

                    If Math.Round(DpDigunakan, 2) <> 0 Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan !!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    '=============================
                    '=     INSERT DETAIL PPH     =
                    '=============================
                    SQL = "select a.Persentase, a.Kode_Tarif, b.Kode_Akun, 'T' as isPPN "
                    SQL = SQL & "from Display_Biaya_Import_PPH_Detail_Barang_Lain a, EMI_Master_Pajak b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Tarif = b.Kode_Tarif "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_BiayaImportPPH = '" & LvFak & "'  "
                    SQL = SQL & "and a.Kode_Perusahaan_Biaya_Import = '" & LvKP & "' "
                    Using Ds = BindingTrans(SQL)
                        If Ds.Tables("MyTable").Rows.Count <> 0 Then
                            For j As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
                                Dim Nilai As Double = Val(HilangkanTanda(LvDPP)) * (Val(HilangkanTanda(Ds.Tables("MyTable").Rows(j).Item("Persentase"))) / 100)
                                SQL = "insert into EMI_Detail_PPH_Pelunasan_Barang_Lain (Kode_Perusahaan, No_Faktur, No_Faktur_PO, Persentase, Nilai, Kode_Tarif, Flag_PPN, Kode_Akun) values "
                                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', '" & Ds.Tables("MyTable").Rows(j).Item("Persentase") & "', "
                                SQL = SQL & "'" & HilangkanTanda(Format(Nilai, "N0")) & "', '" & Ds.Tables("MyTable").Rows(j).Item("Kode_Tarif") & "', NULL, "
                                SQL = SQL & "'" & Ds.Tables("MyTable").Rows(j).Item("Kode_Akun") & "')"
                                ExecuteTrans(SQL)
                            Next
                        End If
                    End Using


                    '=============================
                    '=     INSERT DETAIL PPN     =
                    '=============================
                    SQL = "select Persentase, Kode_Tarif, Kode_Akun from EMI_Detail_PPH_PO_Barang_Lain "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & No_PO & "' and Flag_PPN = 'Y' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dim Nilai As Double = Val(HilangkanTanda(LvDPP)) * (Val(HilangkanTanda(Dr("Persentase"))) / 100)
                            SQL = "insert into EMI_Detail_PPH_Pelunasan_Barang_Lain (Kode_Perusahaan, No_Faktur, No_Faktur_PO, Persentase, Nilai, Kode_Tarif, Flag_PPN, Kode_Akun) values "
                            SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', '" & Dr("Persentase") & "', "
                            SQL = SQL & "'" & HilangkanTanda(Format(Nilai, "N0")) & "', '" & Dr("Kode_Tarif") & "', 'Y', "
                            SQL = SQL & "'" & Dr("Kode_Akun") & "')"
                            Dr.Close()
                            ExecuteTrans(SQL)
                        End If
                    End Using

                    'For j As Integer = 0 To tempDataPajak.Count - 1
                    '    If tempDataPajak(j).noPO = LvFak And tempDataPajak(j).kdPerusahaanImport = LvKP Then
                    '        Dim Nilai As Double = Val(HilangkanTanda(LvDPP)) * (tempDataPajak(j).persentase / 100)
                    '        SQL = "insert into EMI_Detail_PPH_Pelunasan (Kode_Perusahaan, No_Faktur, No_Faktur_PO, Persentase, Nilai, Kode_Tarif, Flag_PPN, Kode_Akun) values "
                    '        SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', '" & tempDataPajak(j).persentase & "', "
                    '        SQL = SQL & "'" & HilangkanTanda(Format(Nilai, "N0")) & "', '" & tempDataPajak(j).kodeTarif & "', " & If(tempDataPajak(j).isPPN, "'Y'", "NULL") & ", "
                    '        SQL = SQL & "'" & tempDataPajak(j).akun & "')"
                    '        ExecuteTrans(SQL)
                    '    End If
                    'Next

                    Dim coa_hutang As String = ""
                    Dim coa_tambahan As String = ""
                    Dim Akun_DP As String = ""
                    Coa_Selisih = ""
                    Dim akunPPH As String = ""
                    Dim akunPPN As String = ""
                    Dim inisial_faktur As String = ""
                    Dim lokasi_default_PPH As String = ""
                    Dim jenis_PPH As String = ""

                    If Jenis1.Trim.ToUpper = "SUPPLIER" Then
                        SQL = "select Hutang_Supplier, akun_selisih_PO, Akun_DP_Asset "
                        SQL = SQL & "from stock_owner "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                coa_hutang = Dr("Hutang_Supplier")
                                Akun_DP = Dr("Akun_DP_Asset")
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        SQL = "select Jenis_PPH from suppliers where "
                        SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and Kode_Supplier='" & LvKP & "' "
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

                    ElseIf Jenis1.Trim.ToUpper = "AGENT" Then
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
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak di temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    SQL = "select top(1) inisial_faktur, ppn_pembelian, Akun_PPH23, Akun_PPH21, Lokasi_default_PPH, Akun_Biaya_Import, akun_selisih_PO from stock_owner where Kode_Stock_Owner='" & LvLokasi & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            coa_tambahan = Dr("Akun_Biaya_Import")
                            inisial_faktur = Dr("inisial_faktur")
                            akunPPN = Dr("ppn_pembelian")
                            lokasi_default_PPH = Dr("Lokasi_default_PPH")
                            Coa_Selisih = Dr("akun_selisih_PO")

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

                    'Nilai_Total - PPH dulu

                    'PERHATIKAN DIBAWAH INI

                    If jenis_DP = "Y" Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update detail_jurnal set debit = debit+ " & Nilai_hutang & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                pagenumber += 1
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang, 1),
                                                  Strings.Mid(coa_hutang, 2, 1),
                                                  Strings.Mid(Ganti(coa_hutang), 3),
                                                  KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, Nilai_hutang, "0", pagenumber, Ket_Lokasi_HO, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                            End If
                        End Using


                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_DP & "' and kredit <> 0 "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update detail_jurnal set kredit = kredit+ " & Nilai_hutang & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_DP & "' and kredit <> 0 "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Akun_DP, 1),
                              Strings.Mid(Akun_DP, 2, 1),
                              Strings.Mid(Ganti(Akun_DP), 3),
                              KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, "0", Nilai_hutang, pagenumber, Ket_Lokasi_HO, Bahasa_Pilihan, Ket_Cost_Center_HO)
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

                    Else
                        SQL = "INSERT INTO detail_pengajuan_Temp(kode_perusahaan, no_pengajuan, kode_master_acc, kode_acc, kode_detail_acc, "
                        SQL = SQL & "keterangan_detail, tgl_jatuh_tempo, jumlah, kode_bank_tujuan, no_rek_tujuan, nama_penerima, "
                        SQL = SQL & "Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Lokasi, Kode_Account, "
                        SQL = SQL & "Id_Cost_Center, No_Pelunasan, Urut_Pelunasan, Tgl_Bayar) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Strings.Left(coa_hutang, 1) & "', "
                        SQL = SQL & "'" & Strings.Mid(coa_hutang, 2, 1) & "', '" & Strings.Mid(Ganti(coa_hutang), 3) & "', "
                        SQL = SQL & "'" & TextBoxket.Text & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', " & Nilai_Total & ", "
                        SQL = SQL & "'" & LvDet_KdBank & "', '" & LvDet_RekTujuanData & "', '" & LvDet_NmTujuan & "', "
                        SQL = SQL & "'-', '" & LvDet_KotaTujuan & "', '" & LvDet_NegaraTujuan & "', '-','" & LvLokasi & "','" & coa_hutang & "', "
                        SQL = SQL & "'0', '" & TxtFaktur.Text.Trim & "', '" & x_no_urut_detail_pelunasan & "', '" & LvDet_TglPelData & "') "
                        ExecuteTrans(SQL)

                        SQL = "select IDENT_CURRENT('detail_pengajuan_Temp') as urutan"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                x_no_urut_det_pengajuan = Dr("urutan")
                            End If
                        End Using

                        SQL = "select urut from detail_pengajuan_Temp where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_pengajuan = '" & no_fakturPengajuan & "' and urut = '" & x_no_urut_det_pengajuan & "'"
                        Using Dr = OpenTrans(SQL)
                            If Not (Dr.Read) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        If Val(HilangkanTanda(LvKursLamaTot)) <> 0 Then
                            SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                            SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & coa_hutang & "', '" & Val(HilangkanTanda(LvKursLamaTot)) + Val(HilangkanTanda(LvNilaiPPN)) & "','0', "
                            SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Lokasi & "')"
                            ExecuteTrans(SQL)
                        End If

                        If Val(HilangkanTanda(LvTambahan)) <> 0 Then
                            SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                            SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & coa_tambahan & "', '" & HilangkanTanda(LvTambahan) & "','0', "
                            SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Lokasi & "')"
                            ExecuteTrans(SQL)
                        End If

                        If Val(HilangkanTanda(LvNilaiPPN)) <> 0 Then

                            SQL = "select no_faktur_pajak, nilai_pembagi From Display_Biaya_Import_PPN_Barang_Lain "
                            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                            Using ds = BindingTrans(SQL)
                                With ds.Tables("MyTable")
                                    For index As Integer = 0 To .Rows.Count - 1

                                        SQL = "insert into EMI_Pelunasan_Detail2_Barang_Lain(kode_Perusahaan, id_detail_Pelunasan, No_faktur_Pajak, NiLai, Jenis)"
                                        SQL = SQL & "values('" & KodePerusahaan & "','" & x_no_urut_detail_pelunasan & "', '" & .Rows(index).Item("no_faktur_pajak") & "', '" & .Rows(index).Item("nilai_pembagi") & "', 'PPN')"
                                        ExecuteTrans(SQL)

                                        If Jenis1.Trim.ToUpper <> "SUPPLIER" Then
                                            Dim AkunPPnPembelian As String = ""
                                            SQL = "select PPN_Pembelian "
                                            SQL = SQL & "from stock_owner "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    AkunPPnPembelian = Dr("PPN_Pembelian")
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End Using

                                            Dim Nilai As Double = Val(HilangkanTanda(LvNilaiPPN))

                                            SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                                            SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & AkunPPnPembelian & "', '" & HilangkanTanda(Format(Nilai, "N0")) & "','0', "
                                            SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & lokasi_default_PPH & "')"
                                            ExecuteTrans(SQL)

                                        End If


                                    Next
                                End With
                            End Using

                        End If

                        'INSERT PPH
                        If Val(HilangkanTanda(LvNilaiPPH)) <> 0 Then
                            If jenis_PPH = "" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Jenis PPH Belum ditentukan . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            SQL = "select no_faktur_pajak, nilai_pembagi From Display_Biaya_Import_PPH_Barang_Lain "
                            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                            Using ds = BindingTrans(SQL)
                                With ds.Tables("MyTable")
                                    For index As Integer = 0 To .Rows.Count - 1

                                        SQL = "insert into EMI_Pelunasan_Detail2_Barang_Lain(kode_Perusahaan, id_detail_Pelunasan, No_faktur_Pajak, NiLai, Jenis)"
                                        SQL = SQL & "values('" & KodePerusahaan & "','" & x_no_urut_detail_pelunasan & "', '" & .Rows(index).Item("no_faktur_pajak") & "', '" & .Rows(index).Item("nilai_pembagi") & "', 'PPH')"
                                        ExecuteTrans(SQL)

                                        If LvDet_Jenis1.ToUpper = "SUPPLIER" Then

                                            Dim hasData As Boolean = False
                                            For j As Integer = 0 To tempDataPajak.Count - 1
                                                If tempDataPajak(j).noPO = LvFak And tempDataPajak(j).kdPerusahaanImport = LvKP Then
                                                    If Not tempDataPajak(j).isPPN Then
                                                        Dim Nilai As Double = Val(HilangkanTanda(LvDPP)) * (tempDataPajak(j).persentase / 100)

                                                        SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan, Urut_Detail_Pengajuan, Kode_Account, Debit, Kredit, Keterangan, Tgl, lokasi)"
                                                        SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & tempDataPajak(j).akun & "', '0', '" & HilangkanTanda(Format(Nilai, "N0")) & "', "
                                                        SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & lokasi_default_PPH & "')"
                                                        ExecuteTrans(SQL)

                                                        hasData = True
                                                    End If

                                                End If
                                            Next

                                            If Not hasData Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Data PPN Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        Else

                                            Dim Nilai As Double = Val(HilangkanTanda(LvDPP)) * (Val(HilangkanTanda(LvPersenPPH)) / 100)

                                            SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan, Urut_Detail_Pengajuan, Kode_Account, Debit, Kredit, Keterangan, Tgl, lokasi)"
                                            SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & akunPPH & "', '0', '" & HilangkanTanda(Format(Nilai, "N0")) & "', "
                                            SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & lokasi_default_PPH & "')"
                                            ExecuteTrans(SQL)

                                        End If
                                    Next
                                End With
                            End Using
                        End If

                        If selisih <> 0 Then
                            If selisih > 0 Then

                                SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                                SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & Coa_Selisih & "', '" & selisih & "', '0', "
                                SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Ket_Lokasi_HO & "')"
                                ExecuteTrans(SQL)
                            Else
                                SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                                SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & Coa_Selisih & "', '0', '" & Math.Abs(selisih) & "', "
                                SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Ket_Lokasi_HO & "')"
                                ExecuteTrans(SQL)
                            End If
                        End If

                        'UNTUK CEK APAKAH DATA
                        SQL = "select Keterangan, Debit, Kredit from Detail_Pengajuan5_Temp where Urut_Detail_Pengajuan = '" & x_no_urut_det_pengajuan & "'"
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    'CloseTrans()
                                    'CloseConn()
                                    'MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    'Exit Sub
                                Else
                                    'CloseTrans()
                                    'CloseConn()
                                    'MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    'Exit Sub
                                End If
                            End With
                        End Using

                        SQL = "select round(sum(debit), 2) - round(sum(kredit), 2) as data from Detail_Pengajuan5_Temp where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "Urut_Detail_Pengajuan = '" & x_no_urut_det_pengajuan & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                If Dr("data") <> Nilai_Total Then
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using
                    End If


#End Region

#Region "Masuk Jurnal"

                    'If i = 0 Then
                    '    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    '    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    '    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0 "
                    '    Using Dr = OpenTrans(SQL)
                    '        If Dr.Read Then
                    '            Dr.Close()
                    '            'update

                    '            SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(txt_TotKurs_Lama.Text) & " where "
                    '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    '            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0 "
                    '            ExecuteTrans(SQL)
                    '        Else
                    '            Dr.Close()
                    '            'insert

                    '            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang, 1),
                    '                  Strings.Mid(coa_hutang, 2, 1),
                    '                  Strings.Mid(Ganti(coa_hutang), 3),
                    '                  KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, HilangkanTanda(txt_TotKurs_Lama.Text), "0", pagenumber, "TSSS")
                    '            ExecuteTrans(SQL)
                    '            pagenumber = pagenumber + 1

                    '        End If
                    '    End Using

                    '    If Val(HilangkanTanda(TxtTotTambahan.Text)) <> 0 Then
                    '        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    '        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    '        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_tambahan & "' and debit <> 0 "
                    '        Using Dr = OpenTrans(SQL)
                    '            If Dr.Read Then
                    '                Dr.Close()
                    '                'update

                    '                SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(TxtTotTambahan.Text) & " where "
                    '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    '                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_tambahan & "' and debit <> 0 "
                    '                ExecuteTrans(SQL)
                    '            Else
                    '                Dr.Close()
                    '                'insert

                    '                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_tambahan, 1),
                    '                      Strings.Mid(coa_tambahan, 2, 1),
                    '                      Strings.Mid(Ganti(coa_tambahan), 3),
                    '                      KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, HilangkanTanda(TxtTotTambahan.Text), "0", pagenumber, "TSSS")
                    '                ExecuteTrans(SQL)
                    '                pagenumber = pagenumber + 1

                    '            End If
                    '        End Using
                    '    End If
                    'End If

                    'If TextBoxtotPPN.Text <> 0 Then
                    '    SQL = "select no_faktur_pajak, nilai_pembagi From Display_Biaya_Import_PPN "
                    '    SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' and "
                    '    SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                    '    SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                    '    Using ds = BindingTrans(SQL)
                    '        With ds.Tables("MyTable")
                    '            For index As Integer = 0 To .Rows.Count - 1

                    '                SQL = "insert into detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal2(kode_Perusahaan, id_detail_Pelunasan, No_faktur_Pajak, NiLai, Jenis)"
                    '                SQL = SQL & "values('" & KodePerusahaan & "','" & x_no_urut_detail_pelunasan & "', '" & .Rows(index).Item("no_faktur_pajak") & "', '" & .Rows(index).Item("nilai_pembagi") & "', 'PPN')"
                    '                ExecuteTrans(SQL)

                    '                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    '                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPN & "' and debit <> 0 "
                    '                Using Dr = OpenTrans(SQL)
                    '                    If Dr.Read Then
                    '                        Dr.Close()
                    '                        'update

                    '                        SQL = "update detail_jurnal set debit = debit+ " & .Rows(index).Item("nilai_pembagi") & " where "
                    '                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    '                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPN & "' and debit <> 0 "
                    '                        ExecuteTrans(SQL)
                    '                    Else
                    '                        Dr.Close()
                    '                        'insert

                    '                        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPPN, 1),
                    '                              Strings.Mid(akunPPN, 2, 1),
                    '                              Strings.Mid(Ganti(akunPPN), 3),
                    '                              KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, .Rows(index).Item("nilai_pembagi"), "0", pagenumber, "TSSS")
                    '                        ExecuteTrans(SQL)
                    '                        pagenumber = pagenumber + 1

                    '                    End If
                    '                End Using
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
                    '    SQL = SQL & "and Kode_Master_Kategori_Biaya_Import ='" & LvKdKategori & "' and lokasi='" & LvLokasi & "' "
                    '    Using ds = BindingTrans(SQL)
                    '        With ds.Tables("MyTable")
                    '            For index As Integer = 0 To .Rows.Count - 1
                    '                SQL = "insert into detail_Val_Pel_Biaya_import_by_Perusahaan_Lokal2(kode_Perusahaan, id_detail_Pelunasan, No_faktur_Pajak, NiLai, Jenis)"
                    '                SQL = SQL & "values('" & KodePerusahaan & "','" & x_no_urut_detail_pelunasan & "', '" & .Rows(index).Item("no_faktur_pajak") & "', '" & .Rows(index).Item("nilai_pembagi") & "', 'PPH')"
                    '                ExecuteTrans(SQL)

                    '                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    '                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPH & "' and kredit <> 0 "
                    '                Using Dr = OpenTrans(SQL)
                    '                    If Dr.Read Then
                    '                        Dr.Close()
                    '                        'update

                    '                        SQL = "update detail_jurnal set kredit = kredit+ " & .Rows(index).Item("nilai_pembagi") & " where "
                    '                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    '                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPH & "' and kredit <> 0 "
                    '                        ExecuteTrans(SQL)
                    '                    Else
                    '                        Dr.Close()
                    '                        'insert

                    '                        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPPH, 1),
                    '                              Strings.Mid(akunPPH, 2, 1),
                    '                              Strings.Mid(Ganti(akunPPH), 3),
                    '                              KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, "0", .Rows(index).Item("nilai_pembagi"), pagenumber, "TSSS")
                    '                        ExecuteTrans(SQL)
                    '                        pagenumber = pagenumber + 1

                    '                    End If
                    '                End Using
                    '            Next
                    '        End With
                    '    End Using
                    'End If

#End Region

                Next

#Region "Kode Lama"

                '                If Val(HilangkanTanda(TextBoxSelisih.Text)) <> 0 Then
                '                    If Val(HilangkanTanda(TextBoxSelisih.Text)) > 0 Then
                '                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                '                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                '                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih & "' and debit <> 0"
                '                        Using Dr = OpenTrans(SQL)
                '                            If Dr.Read Then
                '                                Dr.Close()
                '                                'update

                '                                SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(TextBoxSelisih.Text) & " where "
                '                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                '                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih & "' and debit <> 0"
                '                                ExecuteTrans(SQL)
                '                            Else
                '                                Dr.Close()
                '                                'insert
                '                                pagenumber += 1
                '                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Coa_Selisih, 1),
                '                                          Strings.Mid(Coa_Selisih, 2, 1),
                '                                          Strings.Mid(Ganti(Coa_Selisih), 3),
                '                                          KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, HilangkanTanda(TextBoxSelisih.Text), "0", pagenumber, Ket_Lokasi_HO)
                '                                ExecuteTrans(SQL)
                '                            End If
                '                        End Using
                '                    Else
                '                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                '                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                '                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih & "' and kredit <> 0"
                '                        Using Dr = OpenTrans(SQL)
                '                            If Dr.Read Then
                '                                Dr.Close()
                '                                'update

                '                                SQL = "update detail_jurnal set kredit = kredit+ " & Math.Abs(Val(HilangkanTanda(TextBoxSelisih.Text))) & " where "
                '                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                '                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih & "' and kredit <> 0"
                '                                ExecuteTrans(SQL)
                '                            Else
                '                                Dr.Close()
                '                                'insert
                '                                pagenumber += 1
                '                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Coa_Selisih, 1),
                '                                          Strings.Mid(Coa_Selisih, 2, 1),
                '                                          Strings.Mid(Ganti(Coa_Selisih), 3),
                'KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, "0", Math.Abs(Val(HilangkanTanda(TextBoxSelisih.Text))), pagenumber, Ket_Lokasi_HO)
                '                                ExecuteTrans(SQL)
                '                            End If
                '                        End Using
                '                    End If
                '                End If
                '                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                '                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                '                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex) & "' and kredit <> 0 "
                '                Using Dr = OpenTrans(SQL)
                '                    If Dr.Read Then
                '                        Dr.Close()
                '                        'update

                '                        SQL = "update detail_jurnal set kredit = kredit+ " & HilangkanTanda(Txt_GrandTotal.Text) & " where "
                '                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                '                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex) & "' and kredit <> 0 "
                '                        ExecuteTrans(SQL)
                '                    Else
                '                        Dr.Close()
                '                        'insert

                '                        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex), 1),
                '                      Strings.Mid(ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex), 2, 1),
                '                      Strings.Mid(Ganti(ArrAkunRek1.Item(ComboBoxRek1.SelectedIndex)), 3),
                '                      KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBoxket.Text.Trim, "0", HilangkanTanda(Txt_GrandTotal.Text), pagenumber, "TSSS")
                '                        ExecuteTrans(SQL)
                '                        pagenumber = pagenumber + 1

                '                    End If
                '                End Using

                '                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                '                Using Dr = OpenTrans(SQL)
                '                    If Dr.Read Then
                '                        If Dr("debit") <> Dr("kredit") Then
                '                            Dr.Close()
                '                            CloseTrans()
                '                            CloseConn()
                '                            MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                            Exit Sub
                '                        End If
                '                    Else
                '                        Dr.Close()
                '                        CloseTrans()
                '                        CloseConn()
                '                        MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Exit Sub
                '                    End If
                '                End Using

#End Region

                MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else

#Region "Kode Update Lama"

            'update

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

#End Region

        End If

        Dim TanyaCetak As String = MessageBox.Show("Mau dicetak?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If TanyaCetak = vbYes Then
            cetak(Txt_SelectedJenis.Text)
        End If

        Kosong()
        DateTimePicker1.Focus()

    End Sub

    Private Sub ButtonMT2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Cari("Tidak2")
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

            'SQL = "select a.no_faktur, a.tanggal, a.jam,a.id_rencana, a.tanggal_po, a.jml_kontainer, a.kode_kontainer, a.flag_average, "
            'SQL = SQL & "a.lokasi, a.kode_perusahaan_biaya_import, a.Perusahaan, a.Kode_Master_Kategori_Biaya_Import, a.Master, "
            'SQL = SQL & "a.Kode_Master_Kategori_Biaya_Import, a.master, a.mata_uang, a.Biaya, a.sudah_bayar, a.Jenis, a.PPN, a.PPH, a.jenis_form, a.flag_lunas, a.keterangan, a.jenis_form, a.kurs_lama, 'SUP' as Jenis1, 'A' as Jenis2 "
            'SQL = SQL & "from View_Detail_Transaksi_Biaya_import a where flag_lunas is null "

            SQL = "select no_faktur, tanggal_pembelian, no_po, keterangan, tanggal_PO, Kode_perusahaan_Biaya_Import, Nama as NmPerusahaanBiayaImport, Kode_Master_Kategori_Biaya_Import, "
            SQL = SQL & "Nama_Kategori, Mata_uang, Nilai, Jenis1, Jenis2, Jenis_Lokasi, PPN, PPH, kurs_lama, sudah_bayar, lokasi "
            SQL = SQL & "from View_EMI_Pelunasan_Barang_Lain "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and flag_lunas is null "

            If param = "Tidak1" Then
                SQL = SQL & " and Mata_Uang = '" & ComboBoxMT1.Text & "' "
                If ComboBoxNP1.SelectedIndex <> 0 Then
                    SQL = SQL & "and Kode_Perusahaan_Biaya_Import ='" & ArrNP1.Item(ComboBoxNP1.SelectedIndex - 1) & "' "
                End If
                If Cmb_Jenis.SelectedIndex <> 0 Then
                    SQL = SQL & "and Jenis_Lokasi =  '" & Cmb_Jenis.SelectedItem & "' "
                End If

                If CmbKolom.SelectedIndex <> 0 Then
                    SQL = SQL & "and " & ArrPencarian.Item(CmbKolom.SelectedIndex) & "  like '%" & TxtInput.Text.Trim & "%' "
                End If
            End If

            SQL = SQL & "order by tanggal_po "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            If param = "Tidak1" Then
                                lv = ListViewMT1.Items.Add(.Rows(i).Item("no_faktur")) '0
                            End If

                            '========================
                            '=     GET DATA PPH     =
                            '========================
                            Dim SumPersenPPH As Double = 0

                            Dim TotPPH As Double = 0
                            Dim PPN As Double = 0

                            If .Rows(i).Item("Jenis1").ToString.ToUpper = "SUPPLIER" Then

                                SQL = "select Kode_Tarif, Persentase, Flag_PPN, Kode_Akun from EMI_Detail_PPH_PO_Barang_Lain  "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and No_Faktur = '" & .Rows(i).Item("no_po") & "' "
                                SQL = SQL & "and Flag_PPN is null "
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                        For k As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                            SumPersenPPH += Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(k).Item("Persentase")))

                                        Next
                                    End If
                                End Using

                                '========================
                                '=     GET DATA PPN     =
                                '========================

                                SQL = "select top 1 Persentase "
                                SQL = SQL & "from EMI_Detail_PPH_PO_Barang_Lain "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and No_Faktur = '" & .Rows(i).Item("no_po") & "' "
                                SQL = SQL & "and Flag_PPN = 'Y'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        PPN = Format(Val(HilangkanTanda(Dr("Persentase"))), "N2")
                                    Else
                                        Dr.Close()
                                        PPN = Format(0, "N2")
                                    End If
                                End Using

                            Else
                                SumPersenPPH = .Rows(i).Item("PPH")
                                PPN = .Rows(i).Item("PPN")
                            End If




                            Dim nilai_total_hutang As Double = .Rows(i).Item("Nilai") + Math.Round((.Rows(i).Item("Nilai") * PPN / 100), 0) - Math.Round((.Rows(i).Item("Nilai") * SumPersenPPH / 100), 0)
                            Dim nilai_total_bayar As Double = .Rows(i).Item("sudah_bayar") ' + Math.Round((Dr("sudah_bayar") * Dr("PPN") / 100), 0) - Math.Round((Dr("sudah_bayar") * Dr("PPH") / 100), 0)

                            lv.SubItems.Add(.Rows(i).Item("keterangan")) '1
                            lv.SubItems.Add(Format(.Rows(i).Item("tanggal_pembelian"), "dd MMM yyyy")) '2
                            lv.SubItems.Add(.Rows(i).Item("Kode_perusahaan_Biaya_Import")) '3
                            lv.SubItems.Add(.Rows(i).Item("NmPerusahaanBiayaImport")) '4
                            lv.SubItems.Add(.Rows(i).Item("Kode_Master_Kategori_Biaya_Import")) '5
                            lv.SubItems.Add(.Rows(i).Item("Nama_Kategori")) '6
                            lv.SubItems.Add(.Rows(i).Item("Mata_uang")) '7
                            lv.SubItems.Add(.Rows(i).Item("kurs_lama")) '8

                            lv.SubItems.Add(Format(PPN, "N2")) '9
                            lv.SubItems.Add(Format(SumPersenPPH, "N2")) '10


                            lv.SubItems.Add(Format(nilai_total_hutang, "N2")) '11
                            lv.SubItems.Add(Format(nilai_total_bayar, "N2")) '12
                            Dim sisa As Double = nilai_total_hutang - nilai_total_bayar
                            lv.SubItems.Add(Format(sisa, "N2")) '13
                            lv.SubItems.Add(.Rows(i).Item("Jenis1")) '14
                            lv.SubItems.Add(.Rows(i).Item("Jenis2")) '15
                            lv.SubItems.Add(.Rows(i).Item("lokasi")) '16
                            lv.SubItems.Add(.Rows(i).Item("Jenis_Lokasi")) '17
                            lv.SubItems.Add(.Rows(i).Item("no_po"))
                            lv.SubItems.Add(Format(.Rows(i).Item("tanggal_po"), "dd MMM yyyy"))

                        Next
                    End If
                End With
            End Using

            Txt_KursBaru.Enabled = False
            'ComboBoxBank1.Enabled = False
            'ComboBoxRek1.Enabled = False
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

    Private Sub cetak(ByVal Jenis As String)
        Try

            OpenConn()

            Dim SF As String

            SQL = "select Kode_Perusahaan "
            SQL = SQL & "from View_Laporan_Pengajuan_Pelunasan  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & TxtFaktur.Text.Trim & "' "
            SQL = SQL & "and no_pengajuan = '" & no_fakturPengajuan & "' "

            SF = "{View_Laporan_Pengajuan_Pelunasan.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {View_Laporan_Pengajuan_Pelunasan.No_Val} = '" & TxtFaktur.Text.Trim & "' "
            SF = SF & "and {View_Laporan_Pengajuan_Pelunasan.no_pengajuan} = '" & no_fakturPengajuan & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Laporan_Emi_Pelunasan    'Nama file CR

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = SF
                    With A_Place_For_Printing2
                        .Text = "Pelunasan Biaya Import "
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

#Region "REPORT LAMA IMPORT LOKAL"

            'If Jenis.Trim.ToUpper = "IMPORT" Then

            '    SQL = "select Kode_Perusahaan from View_Laporan_Pelunasan_Biaya_Import_IMPORT  "
            '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & TxtFaktur.Text.Trim & "' "
            '    SQL = SQL & "and Kode_Master_Kategori_Biaya_Import = '" & TxtKodeKategori.Text & "'"

            '    SF = "{View_Laporan_Pelunasan_Biaya_Import_IMPORT.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            '    SF = SF & "and {View_Laporan_Pelunasan_Biaya_Import_IMPORT.No_Val} = '" & TxtFaktur.Text.Trim & "' "
            '    SF = SF & "and {View_Laporan_Pelunasan_Biaya_Import_IMPORT.Kode_Master_Kategori_Biaya_Import} = '" & TxtKodeKategori.Text & "'"
            '    Using Ds = BindingTrans(SQL)
            '        If Ds.Tables("MyTable").Rows.Count <> 0 Then
            '            Dim CrDoc As New Laporan_Pelunasan_Biaya_Import_By_Perusahaan_IMPORT    'Nama file CR

            '            CrDoc.SetDataSource(Ds)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            CrDoc.RecordSelectionFormula = SF
            '            With A_Place_For_Printing2
            '                .Text = "Pelunasan Biaya Import (IMPORT)"
            '                .CrystalReportViewer1.ReportSource = CrDoc
            '                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            '                .Refresh()
            '                .Show()
            '            End With

            '            '=============================================================================
            '            '=============================================================================
            '            'CrDoc.SetDataSource(Ds)
            '            'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            'CrDoc.PrintOptions.PrinterName = PrinterName
            '            'CrDoc.RecordSelectionFormula = SF

            '            'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            '            'doctoprint.PrinterSettings.PrinterName = PrinterName
            '            'Dim rawKind As Integer
            '            'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            '            'For i = doctoprint.PrinterSettings.PaperSizes.Count - 1 To 0 Step -1
            '            '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
            '            '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
            '            '        CrDoc.PrintOptions.PaperSize = rawKind
            '            '        Exit For
            '            '    End If
            '            'Next
            '            'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
            '            'CrDoc.PrintToPrinter(1, False, 1, 99)
            '        End If
            '    End Using

            'ElseIf Jenis.Trim.ToUpper = "LOKAL" Then

            '    SQL = "select Kode_Perusahaan from View_Laporan_Pelunasan_Biaya_Import_LOKAL  "
            '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & TxtFaktur.Text.Trim & "' "
            '    SQL = SQL & "and Kode_Master_Kategori_Biaya_Import = '" & TxtKodeKategori.Text & "'"

            '    SF = "{View_Laporan_Pelunasan_Biaya_Import_LOKAL.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            '    SF = SF & "and {View_Laporan_Pelunasan_Biaya_Import_LOKAL.No_Val} = '" & TxtFaktur.Text.Trim & "' "
            '    SF = SF & "and {View_Laporan_Pelunasan_Biaya_Import_LOKAL.Kode_Master_Kategori_Biaya_Import} = '" & TxtKodeKategori.Text & "' "
            '    Using Ds = BindingTrans(SQL)
            '        If Ds.Tables("MyTable").Rows.Count <> 0 Then
            '            Dim CrDoc As New Laporan_Pelunasan_Biaya_Import_By_Perusahaan_LOKAL    'Nama file CR

            '            CrDoc.SetDataSource(Ds)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            CrDoc.RecordSelectionFormula = SF
            '            With A_Place_For_Printing2
            '                .Text = "Pelunasan Biaya Import (LOKAL)"
            '                .CrystalReportViewer1.ReportSource = CrDoc
            '                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            '                .Refresh()
            '                .Show()
            '            End With

            '            '======================================================
            '            'CrDoc.SetDataSource(Ds)
            '            'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            'CrDoc.PrintOptions.PrinterName = PrinterName
            '            'CrDoc.RecordSelectionFormula = SF
            '            'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            '            'doctoprint.PrinterSettings.PrinterName = PrinterName
            '            'Dim rawKind As Integer
            '            'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            '            'For i = doctoprint.PrinterSettings.PaperSizes.Count - 1 To 0 Step -1
            '            '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
            '            '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
            '            '        CrDoc.PrintOptions.PaperSize = rawKind
            '            '        Exit For
            '            '    End If
            '            'Next
            '            'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
            '            'CrDoc.PrintToPrinter(1, False, 1, 99)
            '        End If
            '    End Using

            'End If

#End Region

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then

            If Txt_DP.Text.Trim.Length = 0 Or Val(HilangkanTanda(Txt_DP.Text)) = 0 Then
                CheckBox1.Checked = False
                Exit Sub
            End If

            TextBoxbyr.ReadOnly = True

            If Val(HilangkanTanda(TextBoxjml.Text)) > Val(HilangkanTanda(Txt_DP.Text)) Then
                TextBoxbyr.Text = Val(HilangkanTanda(Txt_DP.Text))
            Else
                TextBoxbyr.Text = Val(HilangkanTanda(TextBoxjml.Text))
            End If
        Else
            TextBoxbyr.ReadOnly = False
            TextBoxbyr.Text = 0
        End If

    End Sub

    Private Sub CmbJenisPT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbJenisPT.SelectedIndexChanged
        If CmbJenisPT.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            ComboBoxNP1.Items.Clear() : ArrNP1.Clear()
            ComboBoxNP1.Items.Add("-- Seluruh --")
            ComboBoxNP1.SelectedIndex = 0
            SQL = ";with cte as( "
            SQL = SQL & "select Nama, Kode_Perusahaan_Biaya_Import, 'AGENT' as Jenis from Perusahaan_Biaya_Import where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "Union all "
            SQL = SQL & "select Nama, Kode_supplier as Kode_Perusahaan_Biaya_Import, 'SUPPLIER' as Jenis from suppliers where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & ") select * from cte "
            If CmbJenisPT.SelectedIndex <> 0 Then
                SQL = SQL & "where Jenis='" & CmbJenisPT.Text & "' "
            End If
            SQL = SQL & "order by Nama "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxNP1.Items.Add(Dr("Nama")) : ArrNP1.Add(Dr("Kode_Perusahaan_Biaya_Import"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBoxNP1.KeyPress
        If e.KeyChar = Chr(13) Then TextBoxbyr.Focus()
    End Sub

    Private Sub ComboBoxCb1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then ComboBoxNP1.Focus()
    End Sub

    Private Sub ComboBoxCb1_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            TextBoxbyr.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then TextBoxket.Focus()
    End Sub

    'End Sub
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

    Private Sub EMI_Pelunasan_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Try
            OpenConn()

            TxtFaktur.Text.Trim()

            '========================================
            '=     CEK APAKAH USER SUDAH SIMPAN     =
            '========================================
            SQL = "select Kode_Perusahaan from EMI_Pelunasan WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & TxtFaktur.Text.Trim & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count = 0 Then

                        'Delete PPN
                        SQL = "delete from Display_Biaya_Import_PPN_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
                        ExecuteTrans(SQL)

                        'Delete PPh Detail
                        SQL = "select Kode_Perusahaan_Biaya_Import, No_Faktur from Display_Biaya_Import_PPH_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
                        Using Ds1 = BindingTrans(SQL)
                            If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                For i As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
                                    'Delete PPH Detail
                                    SQL = "delete from Display_Biaya_Import_PPH_Detail_Barang_Lain where kode_perusahaan = '" & KodePerusahaan & "'  "
                                    SQL = SQL & "and Kode_Perusahaan_Biaya_Import = '" & Ds1.Tables("MyTable").Rows(i).Item("Kode_Perusahaan_Biaya_Import") & "'  "
                                    SQL = SQL & "and No_BiayaImportPPH = '" & Ds1.Tables("MyTable").Rows(i).Item("No_Faktur") & "' "
                                    ExecuteTrans(SQL)
                                Next
                            End If
                        End Using

                        'Delete PPh
                        SQL = "delete from Display_Biaya_Import_PPH_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
                        ExecuteTrans(SQL)

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
        LvDet_RekTujuan = ListViewMT11.Items(No_Index).SubItems(CellRekTujuan).Text
        LvDet_TglPelDisplay = ListViewMT11.Items(No_Index).SubItems(CellTglPelDisplay).Text
        LvDet_KdBank = ListViewMT11.Items(No_Index).SubItems(CellKdBankTujuan).Text
        LvDet_RekTujuanData = ListViewMT11.Items(No_Index).SubItems(CellRekTujuanData).Text
        LvDet_NmTujuan = ListViewMT11.Items(No_Index).SubItems(CellNmTujuan).Text
        LvDet_TglPelData = ListViewMT11.Items(No_Index).SubItems(CellTglPelData).Text
        LvDet_KotaTujuan = ListViewMT11.Items(No_Index).SubItems(CellKotaTujuan).Text
        LvDet_NegaraTujuan = ListViewMT11.Items(No_Index).SubItems(CellNegaraTujuan).Text
        LvDet_Jenis1 = ListViewMT11.Items(No_Index).SubItems(CellJenis1).Text
        LvDet_Jenis2 = ListViewMT11.Items(No_Index).SubItems(CellJenis2).Text
        LvDPP = ListViewMT11.Items(No_Index).SubItems(CellDPP).Text
        LvDPdiPakai = ListViewMT11.Items(No_Index).SubItems(CellDPDipakai).Text
        LvTotDP = ListViewMT11.Items(No_Index).SubItems(CellTotDP).Text
        LvChecklistDP = ListViewMT11.Items(No_Index).SubItems(CellChecklistDP).Text
    End Sub

    Private Sub Get_Isi_Listview1(ByVal No_Index As Integer)
        Lv1_NoPemb = ListViewMT1.Items(No_Index).SubItems(Cell1_NoPemb).Text
        Lv1_Keterangan = ListViewMT1.Items(No_Index).SubItems(Cell1_Keterangan).Text
        Lv1_TglPemb = ListViewMT1.Items(No_Index).SubItems(Cell1_TglPemb).Text
        Lv1_KdPerusahaanBiaya = ListViewMT1.Items(No_Index).SubItems(Cell1_KdPerusahaanBiayaImport).Text
        Lv1_NmPerusahaanBiaya = ListViewMT1.Items(No_Index).SubItems(Cell1_NmPerusahaanBiayaImport).Text
        Lv1_KdKategori = ListViewMT1.Items(No_Index).SubItems(Cell1_KdKategori).Text
        Lv1_NmKategori = ListViewMT1.Items(No_Index).SubItems(Cell1_NmKategori).Text
        Lv1_MataUang = ListViewMT1.Items(No_Index).SubItems(Cell1_MataUang).Text
        Lv1_KursLama = ListViewMT1.Items(No_Index).SubItems(Cell1_KursLama).Text
        Lv1_PPN = ListViewMT1.Items(No_Index).SubItems(Cell1_PPN).Text
        Lv1_PPH = ListViewMT1.Items(No_Index).SubItems(Cell1_PPH).Text
        Lv1_Total = ListViewMT1.Items(No_Index).SubItems(Cell1_Total).Text
        Lv1_Dibayar = ListViewMT1.Items(No_Index).SubItems(Cell1_DiBayar).Text
        Lv1_Sisa = ListViewMT1.Items(No_Index).SubItems(Cell1_Sisa).Text
        Lv1_Jenis1 = ListViewMT1.Items(No_Index).SubItems(Cell1_Jenis1).Text
        Lv1_Jenis2 = ListViewMT1.Items(No_Index).SubItems(Cell1_Jenis2).Text
        Lv1_Lokasi = ListViewMT1.Items(No_Index).SubItems(Cell1_Lokasi).Text
        Lv1_JenisLokasi = ListViewMT1.Items(No_Index).SubItems(Cell1_JenisLokasi).Text
        Lv1_No_FakPO = ListViewMT1.Items(No_Index).SubItems(Cell1_No_FakPO).Text
        Lv1_Tgl_FakPO = ListViewMT1.Items(No_Index).SubItems(Cell1_tgl_FakPO).Text
    End Sub

    Private Sub Get_No_Faktur()
        TxtFaktur.Text = fValPelBI & Format(DateTimePicker1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Pelunasan", "no_val", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_val, 1, " & Len(fValPelBI) + 4 & ")", fValPelBI & Format(DateTimePicker1.Value, "MMyy"))
    End Sub

    Private Sub Get_No_Faktur_Pengajuan()
        Dim fNB = "NB"
        no_fakturPengajuan = fNB & Format(DateTimePicker1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Pengajuan_temp", "No_Pengajuan", 5,
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
        Txt_SelectedJenis.Text = ""
        Btn_Simpan.Text = "&Simpan"

        Txt_KursBaru.Text = ""
        Txt_SelectedJns1.Text = ""
        Txt_SelectedJns2.Text = ""
        Txt_DP.Text = ""
        'Txt_NilaiPPH.Text = ""
        Txt_KursBaru.Enabled = True
        'ComboBoxBank1.Enabled = True
        'ComboBoxRek1.Enabled = True
        ComboBoxNP1.Enabled = True
        ComboBoxMT1.Enabled = True
        Cmb_Jenis.Enabled = True

        ArrMataUangRek.Clear()
        ListViewMT11.Items.Clear()
        ListViewMT1.Items.Clear()

        Cmb_Rekening_Tujuan.Items.Clear()

        Kosong_Bawah()
        'ComboBoxRek1.Items.Clear()
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

            'ComboBoxBank1.Items.Clear()
            'SQL = "SELECT kode_bank from bank where "
            'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' ORDER BY kode_bank"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        ComboBoxBank1.Items.Add(dr("kode_bank"))
            '    Loop
            'End Using

            'ComboBoxNP1.Items.Clear()
            'ComboBoxNP1.Items.Add("-- Seluruh --")
            'ComboBoxNP1.SelectedIndex = 0
            'SQL = "select Nama, Kode_Perusahaan_Biaya_Import from Perusahaan_Biaya_Import where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "Union all "
            'SQL = SQL & "select Nama, Kode_supplier as Kode_Perusahaan_Biaya_Import from suppliers where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "order by Nama "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBoxNP1.Items.Add(Dr("Nama")) : ArrNP1.Add(Dr("Kode_Perusahaan_Biaya_Import"))
            '    Loop
            'End Using

            ComboBoxMT1.Items.Clear()
            SQL = "select Kode_Mata_uang from Mata_Uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Mata_Uang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxMT1.Items.Add(Dr("Kode_Mata_uang"))
                Loop
            End Using

            'ComboBox3.Items.Clear()
            'SQL = "SELECT Kode_Bank FROM Bank_tujuan WHERE Kode_Perusahaan = '" & KodePerusahaan & "' ORDER BY Kode_Bank"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        ComboBox3.Items.Add(dr("Kode_Bank"))
            '    Loop
            'End Using

            'Delete PPN
            SQL = "Delete From Display_Biaya_Import_PPN_Barang_Lain "
            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' "
            ExecuteTrans(SQL)

            'Delete PPh
            SQL = "Delete From Display_Biaya_Import_PPH_Barang_Lain "
            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and UserID='" & UserID & "' "
            ExecuteTrans(SQL)

            CmbJenisPT.Items.Clear()
            CmbJenisPT.Items.Add("-- Seluruh --")
            CmbJenisPT.Items.Add("AGENT")
            CmbJenisPT.Items.Add("SUPPLIER")
            CmbJenisPT.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        CmbKolom.Items.Clear() : ArrPencarian.Clear()
        CmbKolom.Items.Add(" - -SELURUH - -") : ArrPencarian.Add("")
        CmbKolom.Items.Add("No PO") : ArrPencarian.Add("no_po")
        CmbKolom.Items.Add("No Pembelian") : ArrPencarian.Add("no_faktur")
        CmbKolom.Items.Add("Nama Supplier") : ArrPencarian.Add("Nama")
        CmbKolom.Items.Add("Nama Kategori") : ArrPencarian.Add("Nama_Kategori")

        CmbKolom.SelectedIndex = 0
        'pilihMataUang()
        Hitung()

    End Sub

    Private Sub LihatDetailPPHToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LihatDetailPPHToolStripMenuItem.Click
        If ListViewMT11.Items.Count = 0 Then Exit Sub

        Dim selectedIndex As Integer = ListViewMT11.FocusedItem.Index

        Get_Isi_Listview(selectedIndex)

        'SD_Detail_PajakPO_Pelunasan.NoPO = LvFak
        'SD_Detail_PajakPO_Pelunasan.KdPerusahaanBiayaImport = LvKP
        'SD_Detail_PajakPO_Pelunasan.TxtPO_GrandTotal.Text = LvDPP
        'SD_Detail_PajakPO_Pelunasan.ShowDialog()

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
            SQL = "Delete From Display_Biaya_Import_PPN_Barang_Lain "
            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and no_faktur ='" & LvFak & "' and "
            SQL = SQL & "Kode_Perusahaan_Biaya_Import='" & LvKP & "' and Kode_Master_Kategori_Biaya_Import='" & LvKdKategori & "' and mata_uang='" & LvMT & "' and UserID='" & UserID & "' and Lokasi='" & LvLokasi & "' "
            ExecuteTrans(SQL)

            'Delete PPh Detail
            SQL = "select Kode_Perusahaan_Biaya_Import, No_Faktur from Display_Biaya_Import_PPH_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
            Using Ds1 = BindingTrans(SQL)
                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                    For i As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
                        'Delete PPH Detail
                        SQL = "delete from Display_Biaya_Import_PPH_Detail_Barang_Lain where kode_perusahaan = '" & KodePerusahaan & "'  "
                        SQL = SQL & "and Kode_Perusahaan_Biaya_Import = '" & Ds1.Tables("MyTable").Rows(i).Item("Kode_Perusahaan_Biaya_Import") & "'  "
                        SQL = SQL & "and No_BiayaImportPPH = '" & Ds1.Tables("MyTable").Rows(i).Item("No_Faktur") & "' "
                        ExecuteTrans(SQL)
                    Next
                End If
            End Using

            'Delete PPh
            SQL = "Delete From Display_Biaya_Import_PPH_Barang_Lain "
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

    Private Sub ListView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.DoubleClick

        Dim kode As String = ListView3.FocusedItem.Text
        Dim nama As String = ListView3.FocusedItem.SubItems(1).Text
        Dim kode_bank As String = ListView3.FocusedItem.SubItems(2).Text
        Dim alamat As String = ListView3.FocusedItem.SubItems(3).Text
        Dim kota As String = ListView3.FocusedItem.SubItems(4).Text
        Dim negara As String = ListView3.FocusedItem.SubItems(5).Text
        Dim telp As String = ListView3.FocusedItem.SubItems(6).Text

        'TextBox7.Text = nama
        'TextBox10.Text = kode
        'ComboBox3.Text = kode_bank

        x_alamat = alamat
        x_Kota = kota
        x_negara = negara
        x_telp = telp

        ListView3.Visible = False
    End Sub

    Private Sub ListViewMT1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewMT1.DoubleClick
        Dim kd_kategori As String = ""
        Dim mt As String = ""
        Dim lks As String = ""
        Dim jns As String = ""

        CheckBox1.Checked = False

        For i As Integer = 0 To ListViewMT11.Items.Count - 1

            Get_Isi_Listview(i)
            If LvFak.Trim.ToUpper = ListViewMT1.FocusedItem.SubItems(Cell1_NoPemb).Text.Trim.ToUpper And LvKP.Trim.ToUpper = ListViewMT1.FocusedItem.SubItems(Cell1_KdPerusahaanBiayaImport).Text.Trim.ToUpper Then
                MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            If i = 0 Then
                kd_kategori = ListViewMT11.Items(i).SubItems(CellKdKategori).Text.Trim.ToUpper
                mt = ListViewMT11.Items(i).SubItems(CellMT).Text.Trim.ToUpper
                lks = ListViewMT11.Items(i).SubItems(CellLokasi).Text.Trim.ToUpper
                jns = ListViewMT11.Items(i).SubItems(CellJnsBiaya).Text.Trim.ToUpper
            End If

            'If jns <> ListViewMT1.FocusedItem.SubItems(Cell1_JenisLokasi).Text.Trim.ToUpper Then
            '    MessageBox.Show("Jenis Biaya Tidak Boleh berbeda !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            'If kd_kategori <> ListViewMT1.FocusedItem.SubItems(CellKdKategoriMU).Text.Trim.ToUpper Then
            '    MessageBox.Show("kategori Tidak Boleh berbeda !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            If mt <> ListViewMT1.FocusedItem.SubItems(Cell1_MataUang).Text.Trim.ToUpper Then
                MessageBox.Show("Mata Uang Tidak Boleh berbeda !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If lks <> ListViewMT1.FocusedItem.SubItems(Cell1_Lokasi).Text.Trim.ToUpper Then
                MessageBox.Show("Lokasi Tidak Boleh berbeda !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

        Next

        'Dim Total_Hutang As Double = Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(7).Text)) - Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(8).Text))
        Dim Total_Hutang As Double = Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(Cell1_Total).Text)) - Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(Cell1_DiBayar).Text))
        Dim KursLama As Double = Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(Cell1_KursLama).Text))

        If KursLama = 0 Then
            MessageBox.Show("Data Belum Sampai", "Pelunasan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        TextBoxFktr.Text = ListViewMT1.FocusedItem.Text
        TextBoxtgl.Text = ListViewMT1.FocusedItem.SubItems(Cell1_TglPemb).Text

        TxtDataNmKategori.Text = ListViewMT1.FocusedItem.SubItems(Cell1_NmKategori).Text
        TxtDataPPN.Text = ListViewMT1.FocusedItem.SubItems(Cell1_PPN).Text
        TxtDataPPH.Text = ListViewMT1.FocusedItem.SubItems(Cell1_PPH).Text

        TextBoxKP.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(Cell1_KdPerusahaanBiayaImport).Text)
        TextBoxNP.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(Cell1_NmPerusahaanBiayaImport).Text)
        TextBoxMT.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(Cell1_MataUang).Text)
        TextBoxjml.Text = Format(Total_Hutang, "N2")
        TextBoxbyr.Text = Format(Total_Hutang, "N2")
        TextBoxjns.Text = "1"
        'TxtJenisForm.Text = LvJnsU
        TextBoxNilai.Text = ListViewMT1.FocusedItem.SubItems(Cell1_Total).Text
        TextBoxPPH.Text = ListViewMT1.FocusedItem.SubItems(Cell1_PPH).Text

        Txt_SelectedJenis.Text = ListViewMT1.FocusedItem.SubItems(Cell1_JenisLokasi).Text

        TxtKodeKategori.Text = ListViewMT1.FocusedItem.SubItems(Cell1_KdKategori).Text
        TxtNmKategori.Text = ListViewMT1.FocusedItem.SubItems(Cell1_NmKategori).Text
        TxtLokasi.Text = ListViewMT1.FocusedItem.SubItems(Cell1_Lokasi).Text

        Txt_SelectedJns1.Text = ListViewMT1.FocusedItem.SubItems(Cell1_Jenis1).Text
        Txt_SelectedJns2.Text = ListViewMT1.FocusedItem.SubItems(Cell1_Jenis2).Text

        Cmb_Rekening_Tujuan.Enabled = True
        Dtp_TglBayar.Enabled = True
        TextBoxbyr.Enabled = True

        Try
            OpenConn()

            Dim jns1 As String = ListViewMT1.FocusedItem.SubItems(Cell1_Jenis1).Text

            'TODO Double Klik

            If jns1.ToUpper = "SUPPLIER" Then

                '=================================
                '=     CEK DP Berdasarkan PO     =
                '=================================

                Dim NoFakturPO As String = ListViewMT1.FocusedItem.SubItems(Cell1_No_FakPO).Text

                Dim Tot_DP As Double = 0
                SQL = "with Cte as ( select a.Nilai as Nilai_DP, ( "
                SQL = SQL & "(a.Nilai-isnull((select sum(x.nilai) from EMI_Transaksi_Pembayaran_Dimuka_Asset_Pajak x where "
                SQL = SQL & "x.kode_perusahaan=a.kode_perusahaan and x.no_faktur=a.No_Transaksi and x.flag_ppn is null ),0)) - "
                SQL = SQL & "ISNULL(( select z.nilai from EMI_Pelunasan_Detail_DP_Barang_Lain z, EMI_Pelunasan_Barang_Lain w where "
                SQL = SQL & "z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
                SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
                SQL = SQL & " ), 0) ) as Sisa "
                SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset a, EMI_Transaksi_Pembayaran_Dimuka_Asset b  "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "And a.No_Transaksi = b.No_Transaksi  "
                SQL = SQL & "And b.Status Is null  "
                SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and a.No_Fak_PO in ( "
                SQL = SQL & "select x.No_Faktur_Induk "
                SQL = SQL & "from EMI_Pembelian_PO_Barang_Lain x "
                SQL = SQL & "where x.Kode_Perusahaan = a.Kode_Perusahaan "
                SQL = SQL & "and x.Status is null "
                SQL = SQL & "and x.Kode_Perusahaan = a.Kode_Perusahaan "
                SQL = SQL & "and x.No_Faktur = '" & NoFakturPO & "') "
                SQL = SQL & ")select isnull(sum(Sisa), 0) as Nilai_DP from Cte "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Tot_DP = Dr("Nilai_DP")
                    End If
                End Using

                Txt_DP.Text = Format(Tot_DP, "N2")
            Else
                Txt_DP.Text = Format(0, "N2")
            End If

            '================================
            '=     LOAD REKENING TUJUAN     =
            '================================
            If jns1.Trim.ToUpper = "AGENT" Then

                Cmb_Rekening_Tujuan.Items.Clear() : arrRekeningTujuan.Clear() : arrKodeBankTujuan.Clear() : arrNamaPemilikiRekTujuan.Clear() : arrKotaTujuan.Clear() : arrNegaraTujuan.Clear()
                SQL = "select Nama_Pemilik, Nama_Bank, No_Rekening, Kota_Pemilik, Negara_Pemilik "
                SQL = SQL & "from Rekening_Perusahaan_Biaya_Import "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Perusahaan_Biaya_Import = '" & TextBoxKP.Text & "' "
                SQL = SQL & "and status is null "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read

                        Dim TemplateView As String = $"{Dr("Nama_Pemilik")}-{Dr("Nama_Bank")}-{Dr("No_Rekening")}"
                        Cmb_Rekening_Tujuan.Items.Add(TemplateView)

                        arrKodeBankTujuan.Add(Dr("Nama_Bank"))
                        arrRekeningTujuan.Add(Dr("No_Rekening"))
                        arrNamaPemilikiRekTujuan.Add(Dr("Nama_Pemilik"))
                        arrKotaTujuan.Add(Dr("Kota_Pemilik"))
                        arrNegaraTujuan.Add(Dr("Negara_Pemilik"))

                    Loop
                End Using

            ElseIf jns1.Trim.ToUpper = "SUPPLIER" Then

                Cmb_Rekening_Tujuan.Items.Clear() : arrRekeningTujuan.Clear() : arrKodeBankTujuan.Clear() : arrNamaPemilikiRekTujuan.Clear() : arrKotaTujuan.Clear() : arrNegaraTujuan.Clear()
                SQL = "select Nama_Pemilik, Nama_Bank, No_Rekening	, Kota_Pemilik, Negara_Pemilik "
                SQL = SQL & "from Rekening_Suppliers "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Supplier = '" & TextBoxKP.Text & "' "
                SQL = SQL & "and Mata_Uang = '" & HilangkanTanda(ListViewMT1.FocusedItem.SubItems(Cell1_MataUang).Text) & "' "
                'SQL = "select 'Nama_Pemilik' as Nama_Pemilik, 'Nama_Bank' as Nama_Bank, 'No_Rekening' as No_Rekening	, 'Kota_Pemilik' as Kota_Pemilik, 'Negara_Pemilik' as Negara_Pemilik "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read

                        Dim TemplateView As String = $"{Dr("Nama_Pemilik")}-{Dr("Nama_Bank")}-{Dr("No_Rekening")}"
                        Cmb_Rekening_Tujuan.Items.Add(TemplateView)

                        arrKodeBankTujuan.Add(Dr("Nama_Bank"))
                        arrRekeningTujuan.Add(Dr("No_Rekening"))
                        arrNamaPemilikiRekTujuan.Add(Dr("Nama_Pemilik"))
                        arrKotaTujuan.Add(Dr("Kota_Pemilik"))
                        arrNegaraTujuan.Add(Dr("Negara_Pemilik"))

                    Loop
                End Using

            End If
            Cmb_Rekening_Tujuan.DroppedDown = True
            Cmb_Rekening_Tujuan.Focus()


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxbyr.KeyPress
        Dim hasilTotal As Double = 0

        If e.KeyChar = Chr(13) Then

            If TextBoxFktr.Text.Trim.Length = 0 Then
                MessageBox.Show("Silahkan pilih faktur terlebih dahulu")
                Exit Sub
            End If

            If Cmb_Rekening_Tujuan.SelectedIndex = -1 Then
                MessageBox.Show("Silahkan pilih Rekening Tujuan terlebih dahulu")
                Cmb_Rekening_Tujuan.DroppedDown = True : Cmb_Rekening_Tujuan.Focus() : Exit Sub
            End If

            If (Val(TextBoxbyr.Text) > Val(HilangkanTanda(TextBoxjml.Text))) Then
                MessageBox.Show("Pembayaran tidak boleh melebihi sisa hutang") : Exit Sub
            End If

            Dim Pakai_DP As String = ""

            If CheckBox1.Checked = True Then
                Pakai_DP = "Y"
            Else
                Pakai_DP = "T"
            End If

            If TextBoxjns.Text = "1" Then
                For i As Integer = 0 To ListViewMT11.Items.Count - 1
                    Get_Isi_Listview(i)
                    If LvFak.Trim.ToUpper = TextBoxFktr.Text.Trim.ToUpper And LvKP.Trim.ToUpper = TextBoxKP.Text.Trim.ToUpper Then
                        MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If LvMT <> TextBoxMT.Text.Trim.ToUpper Then
                        MessageBox.Show("Mata Uang Tidak Boleh berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If LvChecklistDP.Trim.ToUpper <> Pakai_DP Then
                        MessageBox.Show("Jenis DP Tidak Boleh berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                SQL = "Delete From Display_Biaya_Import_PPN_Barang_Lain "
                SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and no_faktur ='" & TextBoxFktr.Text & "' and "
                SQL = SQL & "Kode_Perusahaan_Biaya_Import='" & TextBoxKP.Text & "' and Kode_Master_Kategori_Biaya_Import='" & TxtKodeKategori.Text & "' and mata_uang='" & TextBoxMT.Text & "' and UserID='" & UserID & "' and Lokasi='" & TxtLokasi.Text & "' "
                ExecuteTrans(SQL)

                'Delete PPH Detail
                SQL = "select Kode_Perusahaan_Biaya_Import, No_Faktur From Display_Biaya_Import_PPH_Barang_Lain "
                SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and "
                SQL = SQL & "Kode_Perusahaan_Biaya_Import='" & TextBoxKP.Text & "' and Kode_Master_Kategori_Biaya_Import='" & TxtKodeKategori.Text & "' and mata_uang='" & TextBoxMT.Text & "' and UserID='" & UserID & "' and Lokasi='" & TxtLokasi.Text & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                'Delete PPH Detail
                                SQL = "delete from Display_Biaya_Import_PPH_Detail_Barang_Lain where kode_perusahaan = '" & KodePerusahaan & "'  "
                                SQL = SQL & "and Kode_Perusahaan_Biaya_Import = '" & .Rows(i).Item("Kode_Perusahaan_Biaya_Import") & "'  "
                                SQL = SQL & "and No_BiayaImportPPH = '" & .Rows(i).Item("No_Faktur") & "' "
                                ExecuteTrans(SQL)
                            Next

                            'Delete PPh
                            SQL = "Delete From Display_Biaya_Import_PPH_Barang_Lain "
                            SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and no_faktur ='" & TextBoxFktr.Text & "' and "
                            SQL = SQL & "Kode_Perusahaan_Biaya_Import='" & TextBoxKP.Text & "' and Kode_Master_Kategori_Biaya_Import='" & TxtKodeKategori.Text & "' and mata_uang='" & TextBoxMT.Text & "' and UserID='" & UserID & "' and Lokasi='" & TxtLokasi.Text & "' "
                            ExecuteTrans(SQL)
                        End If
                    End With
                End Using

                Cmd.Transaction.Commit()
                CloseConn()
                'MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            Dim JnsBiaya As String = ListViewMT1.FocusedItem.SubItems(Cell1_JenisLokasi).Text

            'TODO DPP

            Dim NTotal As Double = 0
            NTotal = Val(HilangkanTanda(TextBoxbyr.Text)) + Val(HilangkanTanda(0))

            Dim KursLama As Double = Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(Cell1_KursLama).Text))
            Dim KursBaru As Double = Val(HilangkanTanda(Txt_KursBaru.Text))

            Dim TotKursLama As Double = NTotal * KursLama
            Dim TotKursBaru As Double = NTotal * KursBaru

            'INI UNtuk Ambil Nilai DPP
            'Dim Persentase As Double = 1 + (Val(HilangkanTanda(TxtDataPPN.Text)) / 100) - (Val(HilangkanTanda(TxtDataPPH.Text)) / 100)
            Dim DPP As Double = Val(HilangkanTanda(txtDPP.Text))

            Dim Nppn As Double = Val(HilangkanTanda(txtPPN.Text))
            'Nppn = DPP * Val(HilangkanTanda(TxtDataPPN.Text)) / 100
            'Dim HslNppn As Double = Format(Nppn, "N0")

            Dim HslNpph As Double = 0
            Dim HslNpph2 As Double = 0

            Try

                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                Dim FakturPO As String = ""
                Dim FakturPembelianPO As String = ""

                '========================
                '=     GET DATA PPH     =
                '========================
                If Txt_SelectedJns1.Text = "SUPPLIER" Then

                    SQL = "select b.No_Faktur_Induk, b.No_Faktur "
                    SQL = SQL & "from EMI_Pembelian_Barang_Lain a, EMI_Pembelian_PO_Barang_Lain b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.No_PO = b.No_Faktur "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Faktur = '" & TextBoxFktr.Text & "' "
                    SQL = SQL & "and a.Status is null and b.Status is null "

                    Using Ds1 = BindingTrans(SQL)
                        If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                            For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                Dim TotPPH As Double = 0

                                SQL = "select Kode_Tarif, Persentase, Flag_PPN, Kode_Akun from EMI_Detail_PPH_PO_Barang_Lain  "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and No_Faktur = '" & Ds1.Tables("MyTable").Rows(j).Item("No_Faktur") & "' and Flag_PPN is null "
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                        For k As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                            'TODO Insert tempDataPajak

                                            Dim isPPN As Boolean = If(General_Class.CekNULL(Ds2.Tables("MyTable").Rows(k).Item("Flag_PPN")) = "", False, True)
                                            tempDataPajak.Add((TextBoxFktr.Text, TextBoxKP.Text, Ds2.Tables("MyTable").Rows(k).Item("Kode_Tarif"), Ds2.Tables("MyTable").Rows(k).Item("Persentase"), Ds2.Tables("MyTable").Rows(k).Item("Kode_Akun"), isPPN))

                                            Dim Npph As Double = 0
                                            Npph = Val(HilangkanTanda(Format(DPP * Ds2.Tables("MyTable").Rows(k).Item("Persentase") / 100, "N0")))

                                            SQL = "insert into Display_Biaya_Import_PPH_Detail_Barang_Lain (Kode_Perusahaan, Kode_Perusahaan_Biaya_Import, No_BiayaImportPPH, Kode_Tarif, Persentase, Nilai) values "
                                            SQL = SQL & "('" & KodePerusahaan & "', '" & TextBoxKP.Text & " ', '" & TextBoxFktr.Text & "', '" & Ds2.Tables("MyTable").Rows(k).Item("Kode_Tarif") & "', "
                                            SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(k).Item("Persentase") & "', '" & HilangkanTanda(Format(Npph, "N0")) & "') "
                                            ExecuteTrans(SQL)

                                            TotPPH += Npph
                                        Next

                                    End If
                                End Using

                                HslNpph = Format(TotPPH, "N0")

                                'Simpan PPh Total
                                SQL = "Insert into Display_Biaya_Import_PPH_Barang_Lain (Kode_Perusahaan, Kode_Perusahaan_Biaya_Import, No_Faktur, No_Pembagi, No_Faktur_Pajak, Nilai_Pembagi, Mata_Uang, Kode_Master_Kategori_Biaya_Import, UserID, Lokasi) "
                                SQL = SQL & "Values ('" & KodePerusahaan & "', '" & TextBoxKP.Text & "', '" & TextBoxFktr.Text & "', '1', '-', " & TotPPH & ", '" & TextBoxMT.Text & "', '" & TxtKodeKategori.Text & "', '" & UserID & "', '" & TxtLokasi.Text & "') "
                                ExecuteTrans(SQL)
                            Next
                        End If
                    End Using
                Else
                    Dim Npph2 As Double = DPP * (Val(HilangkanTanda(TxtDataPPH.Text)) / 100)
                    HslNpph2 = Format(Npph2, "N0")

                    SQL = "insert into Display_Biaya_Import_PPH_Detail_Barang_Lain (Kode_Perusahaan, Kode_Perusahaan_Biaya_Import, No_BiayaImportPPH, Kode_Tarif, Persentase, Nilai) values "
                    SQL = SQL & "('" & KodePerusahaan & "', '" & TextBoxKP.Text & " ', '" & TextBoxFktr.Text & "', 'AGENT', "
                    SQL = SQL & "'" & HilangkanTanda(TxtDataPPH.Text) & "', '" & HilangkanTanda(Format(Npph2, "N0")) & "') "
                    ExecuteTrans(SQL)

                    'Simpan PPh Total
                    SQL = "Insert into Display_Biaya_Import_PPH_Barang_Lain (Kode_Perusahaan, Kode_Perusahaan_Biaya_Import, No_Faktur, No_Pembagi, No_Faktur_Pajak, Nilai_Pembagi, Mata_Uang, Kode_Master_Kategori_Biaya_Import, UserID, Lokasi) "
                    SQL = SQL & "Values ('" & KodePerusahaan & "', '" & TextBoxKP.Text & "', '" & TextBoxFktr.Text & "', '1', '-', " & Npph2 & ", '" & TextBoxMT.Text & "', '" & TxtKodeKategori.Text & "', '" & UserID & "', '" & TxtLokasi.Text & "') "
                    ExecuteTrans(SQL)

                End If

                '======================================
                '=     GET FAKTUR PEMBELIAN BY PO     =
                '======================================
                SQL = "select No_Faktur, No_PO from EMI_Pembelian_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and No_Faktur = '" & TextBoxFktr.Text & "' "
                SQL = SQL & "and status is null"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        FakturPO = Dr("No_PO")
                        FakturPembelianPO = Dr("No_Faktur")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data PO Tidak ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'Simpan PPN
                SQL = "Insert into Display_Biaya_Import_PPN_Barang_Lain (Kode_Perusahaan, Kode_Perusahaan_Biaya_Import, No_Faktur, No_Pembagi, No_Faktur_Pajak, Nilai_Pembagi, Mata_Uang, Kode_Master_Kategori_Biaya_Import, UserID, Lokasi) "
                SQL = SQL & "Values ('" & KodePerusahaan & "', '" & TextBoxKP.Text & "', '" & TextBoxFktr.Text & "', '1', '-', " & Nppn & ", '" & TextBoxMT.Text & "', '" & TxtKodeKategori.Text & "', '" & UserID & "', '" & TxtLokasi.Text & "') "
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

            Dim TotalColmn As Integer = ListViewMT11.Columns.Count

            Dim lv As New ListViewItem
            lv = ListViewMT11.Items.Add(TextBoxFktr.Text) 'NoFaktur 0
            lv.SubItems.Add(TextBoxtgl.Text) 'Tgl 1
            lv.SubItems.Add(TextBoxKP.Text) 'Kode Perusahaan 2
            lv.SubItems.Add(TextBoxNP.Text) 'Nama Perusahaan 3
            lv.SubItems.Add(TxtKodeKategori.Text) 'Kategori Perusahaan 4
            lv.SubItems.Add(TxtDataNmKategori.Text) 'Kategori Perusahaan 5
            lv.SubItems.Add(TextBoxMT.Text) 'Mata Uang 6
            lv.SubItems.Add(Format(Val(HilangkanTanda(TextBoxbyr.Text)), "N2")) 'Jumlah 7
            lv.SubItems.Add(0) ' Nilai Tambahan 8

            lv.SubItems.Add(Format(KursLama, "N2")) ' Kurs Lama 9
            lv.SubItems.Add(Format(DPP, "N2")) ' Total Kurs Lama 10
            lv.SubItems.Add(Format(KursBaru, "N2")) ' Kurs Baru 11
            lv.SubItems.Add(Format(DPP, "N2")) ' Total Kurs Baru 12

            lv.SubItems.Add(Format(DPP, "N2")) ' Nilai Total 13
            lv.SubItems.Add(Format(Val(HilangkanTanda(TxtDataPPN.Text)), "N2")) 'Persen PPN 14
            lv.SubItems.Add(Format(Val(HilangkanTanda(TxtDataPPH.Text)), "N2")) 'Persen PPh 15
            lv.SubItems.Add(Format(Nppn, "N2")) 'Nilai PPN 16

            If Txt_SelectedJns1.Text = "SUPPLIER" Then
                lv.SubItems.Add(Format(HslNpph, "N2")) ' Nilai PPh
            Else
                lv.SubItems.Add(Format(HslNpph2, "N2")) ' Nilai PPh
            End If
            lv.SubItems.Add(TxtLokasi.Text) ' lks
            lv.SubItems.Add(TxtJenisForm.Text) ' jns form
            lv.SubItems.Add(JnsBiaya) ' jns Biaya

            lv.SubItems.Add(Cmb_Rekening_Tujuan.Text) ' Rekening Tujuan Display
            lv.SubItems.Add(Format(Dtp_TglBayar.Value, "dd MMM yyyy")) ' Tgl Pelunasan Display
            lv.SubItems.Add(arrKodeBankTujuan(Cmb_Rekening_Tujuan.SelectedIndex)) ' Kd Bank Tujuan
            lv.SubItems.Add(arrRekeningTujuan(Cmb_Rekening_Tujuan.SelectedIndex)) ' Rek Tujuan
            lv.SubItems.Add(arrNamaPemilikiRekTujuan(Cmb_Rekening_Tujuan.SelectedIndex)) ' Nm Tujuan
            lv.SubItems.Add(Format(Dtp_TglBayar.Value, "yyyy-MM-dd")) ' Tgl Pelunasan Data

            lv.SubItems.Add(arrKotaTujuan(Cmb_Rekening_Tujuan.SelectedIndex)) ' Kota Tujuan
            lv.SubItems.Add(arrNegaraTujuan(Cmb_Rekening_Tujuan.SelectedIndex)) ' Negara TUjuan
            lv.SubItems.Add(Txt_SelectedJns1.Text) ' Jenis1
            lv.SubItems.Add(Txt_SelectedJns2.Text) ' jenis2
            lv.SubItems.Add(Format(DPP, "N2")) ' DPP

            ' Dp TOtal

            If CheckBox1.Checked = True Then
                lv.SubItems.Add(Format(Val(HilangkanTanda(Txt_DP.Text)), "N2"))
                lv.SubItems.Add(Format(Val(HilangkanTanda(Txt_DP.Text)), "N2"))
                lv.SubItems.Add("Y")

            Else
                lv.SubItems.Add(Format(0, "N2"))
                lv.SubItems.Add(Format(0, "N2"))
                lv.SubItems.Add("T")

            End If

            ListViewMT11.Focus()

            Kosong_Bawah()
            Hitung()

        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    End Sub

    Private Sub TextBox12_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxjml.KeyPress
        'If e.KeyChar = Chr(13) Then
        '    If TextBox12.Text.Trim.Length = 0 Then
        '        MessageBox.Show("Silahkan isi kurs terlebih dahulu!!") : Exit Sub
        '    End If
        '    TextBox6.Focus()
        'End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    End Sub

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

    Private Sub TextBoxADM1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxADM2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxbyr_Enter(sender As Object, e As EventArgs) Handles TextBoxbyr.Enter

        '======================
        '=     SET FORMAT     =
        '======================

        If TextBoxbyr.Text.Trim.Length = 0 Then
            Dim cellKuantity As String = TextBoxbyr.Text

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            TextBoxbyr.Text = nilai
        End If
    End Sub

    Private Sub TextBoxbyr_Leave(sender As Object, e As EventArgs) Handles TextBoxbyr.Leave
        Dim culture As CultureInfo = CultureInfo.CurrentCulture

        If TextBoxbyr.Text.Trim.Length = 0 Then
            Dim cellKuantity As String = TextBoxbyr.Text

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N2", culture)

            TextBoxbyr.Text = formattedValue

        End If
    End Sub

    Private Sub TextBoxbyr_TextChanged(sender As Object, e As EventArgs) Handles TextBoxbyr.TextChanged
        If TextBoxbyr.Text.Trim.Length = 0 Then
            txtDPP.Text = ""
            txtPPN.Text = ""
            TxtPPH.Text = ""
            Exit Sub
        End If

        Try
            OpenConn()

            Dim arrPPH As New ArrayList
            Dim NilaiPPH As Double = 0
            Dim NilaiPPN As Double = 0

            Dim NTotal As Double = 0
            NTotal = Val(HilangkanTanda(TextBoxbyr.Text)) + Val(HilangkanTanda(0))

            Dim KursBaru As Double = Val(HilangkanTanda(Txt_KursBaru.Text))
            Dim TotKursBaru As Double = NTotal * KursBaru

            Dim Persentase As Double = 1 + (Val(HilangkanTanda(TxtDataPPN.Text)) / 100) - (Val(HilangkanTanda(TxtDataPPH.Text)) / 100)
            Dim DPP As Double = Val(HilangkanTanda(Format(TotKursBaru / Persentase, "N0")))

            NilaiPPN = Val(HilangkanTanda(Format(DPP * (Val(HilangkanTanda(TxtDataPPN.Text)) / 100), "N0")))

            arrPPH.Clear()

            If Txt_SelectedJns1.Text.ToUpper = "SUPPLIER" Then
                SQL = "select b.No_Faktur "
                SQL = SQL & "from EMI_Pembelian_Barang_Lain a, EMI_Pembelian_PO_Barang_Lain b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.No_PO = b.No_Faktur "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & TextBoxFktr.Text & "' "
                SQL = SQL & "and a.Status is null and b.Status is null "
                Using Ds1 = BindingTrans(SQL)
                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                        For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                            Dim TotPPH As Double = 0

                            SQL = "select Kode_Tarif, Persentase, Flag_PPN, Kode_Akun from EMI_Detail_PPH_PO_Barang_Lain  "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Faktur = '" & Ds1.Tables("MyTable").Rows(j).Item("No_Faktur") & "' and Flag_PPN is null "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                    For k As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                        If General_Class.CekNULL(Ds2.Tables("MyTable").Rows(k).Item("Flag_PPN")) = "" Then
                                            arrPPH.Add(Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(k).Item("Persentase"))))
                                        End If
                                    Next

                                End If
                            End Using
                        Next
                    End If
                End Using

                If arrPPH.Count <> 0 Then
                    For i As Integer = 0 To arrPPH.Count - 1
                        NilaiPPH += Val(HilangkanTanda(Format(DPP * (arrPPH(i) / 100), "N0")))
                    Next
                End If
            Else
                NilaiPPH = Val(HilangkanTanda(Format(DPP * (Val(HilangkanTanda(TxtDataPPH.Text)) / 100), "N0")))
            End If

            txtDPP.Text = Format(DPP, "N0")
            txtPPN.Text = Format(NilaiPPN, "N0")
            TxtPPH.Text = Format(NilaiPPH, "N0")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    'End Sub
    Private Sub TextBoxDBY1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxDBY2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxDPT1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxDPT2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxKurs1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub TextBoxKurs2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    '    If cbxMataUang.SelectedIndex <> 0 Then
    '        TextBox8.Visible = True
    '        TextBox8.Focus()
    '    Else
    '        TextBox8.Visible = False
    '    End If
    Private Sub TextBoxKV1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    'Private Sub cbxMataUang_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    Private Sub TextBoxPindahKurs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

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
    Private Sub TextBoxsimpan1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Hitung()
    End Sub

    Private Sub Txt_DP_TextChanged(sender As Object, e As EventArgs) Handles Txt_DP.TextChanged

    End Sub
    Private Sub Txt_KursBaru_Enter(sender As Object, e As EventArgs) Handles Txt_KursBaru.Enter
        If Txt_KursBaru.Text.Trim.Length = 0 Then Exit Sub

        Txt_KursBaru.Text = HilangkanTanda(Txt_KursBaru.Text)
    End Sub



    '        'If TextBox8.Text.Trim.Length = 0 Then
    '        '    MessageBox.Show("Silahkan isi kurs terlebih dahulu") : Exit Sub
    '        'End If
    Private Sub Txt_KursBaru_Leave(sender As Object, e As EventArgs) Handles Txt_KursBaru.Leave
        If Txt_KursBaru.Text.Trim.Length = 0 Then Exit Sub

        Txt_KursBaru.Text = Format(Val(HilangkanTanda(Txt_KursBaru.Text)), "N2")
    End Sub

    '        SQL = "select distinct(mata_uang) from pembelian_import where kode_perusahaan = '" & KodePerusahaan & "' order by mata_uang   "
    '        Using Dr = OpenTrans(SQL)
    '            Do While Dr.Read
    '                cbxMataUang.Items.Add(Dr("mata_uang"))
    '            Loop
    '        End Using
    '        CloseConn()
    Private Sub TxtFaktur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFaktur.KeyPress
        If e.KeyChar = Chr(13) Then
            If DateTimePicker1.Enabled = True Then
                DateTimePicker1.Focus()
            Else
                TextBoxket.Focus()
            End If
        End If
    End Sub

    '        cbxMataUang.Items.Clear()
    '        cbxMataUang.Items.Add("--Pilih Mata Uang--")
    '        cbxMataUang.SelectedIndex = 0
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

    Private Sub TxtFaktur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFaktur.TextChanged

    End Sub

    Private Sub Validasi_Pemb_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub


    'Private Sub pilihMataUang()
    '    Try
    '        OpenConn()
    'Private Sub ComboBoxCb1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBoxCb1.KeyPress

    '    If e.KeyChar = Chr(13) Then cbxMataUang.Focus()

    'End Sub
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





    '====================================================================================================================================================
    '=     HANDLE KEYPRESS
    '====================================================================================================================================================
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxket.KeyPress
        If e.KeyChar = Chr(13) Then
            ComboBoxMT1.DroppedDown = True
            ComboBoxMT1.Focus()
        End If
    End Sub
    Private Sub ComboBoxMT1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBoxMT1.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KursBaru.Focus()
    End Sub
    Private Sub Txt_KursBaru_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KursBaru.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub
    Private Sub Cmb_Rekening_Tujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Rekening_Tujuan.KeyPress
        If e.KeyChar = Chr(13) Then TextBoxbyr.Focus()
    End Sub




End Class