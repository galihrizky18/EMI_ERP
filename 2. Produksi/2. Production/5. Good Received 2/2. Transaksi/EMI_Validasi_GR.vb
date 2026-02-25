
Imports System.Drawing.Printing
Imports System.IO

Public Class EMI_Validasi_GR

    Public Property MenuAsal As String

    Dim JudulForm As String = "Validasi Penerimaan Barang"
    Dim arrLokasiAsal, arrLokasiTujuan, arrSO As New ArrayList
    Dim arrKdBarangSrap, arrNamaScrap, arrSatuanScrap, arrSatuanKecilScrap, arrIdJenisKategori As New ArrayList

    Dim Fitur_Military_Sampling As Boolean = False

    Public Property SelectedVariant As String = ""


    Dim isCombine As Boolean = False
    Dim NoSplitTemp As String = ""
    Public SelectedCurrentSplit As String = ""

    Private random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream


    Public arrBarcodeFromSD As New List(Of Dictionary(Of String, String))

    Dim arrGudangProduksi As New ArrayList


    Dim ReadyForPackaging As Boolean = False
    Dim Prefix, Tahun_MulaiProduksi As String

    Dim LvPallet_KdSO, LvPallet_Barcode, LvPallet_BatchNumber, LvPallet_TglProduksi, LvPallet_TglExpired, LvPallet_KdBarang, LvPallet_NmBarang, LvPallet_Jumlah As String
    Dim LvPallet_Satuan, LvPallet_Kualitas, LvPallet_Warna, LvPallet_ID, LvPallet_QR, LvPallet_KdUnikBerjalan, LvPallet_Nomor, LvPallet_Batch, LvPallet_No_Split As String

    Dim LvData_Barcode, LvData_Nomor, LvData_Jumlah, LvData_Satuan, LvData_Berat, LvData_Tahap, LvData_Batch, LvData_Tgl_Produksi, LvData_Tgl_Expired, LvData_NoSplit As String

    Dim LvBarcode_NomorBaru, LvBarcode_ID, LvBarcode_Jenis, LvBarcode_LokasiTujuan, LvBarcode_Total, LvBarcode_Satuan, LvBarcode_Barcode, LvBarcode_Batch, LvBarcode_NamaJenis As String

    Dim LvBarcodeDet_Barcode, LvBarcodeDet_NomorLama, LvBarcodeDet_Jumlah As String


    Dim CurrentBatch As String = ""

    Dim Kode_Unik_Transaksi As String = ""


    Dim itemPallet_KdSo As Integer = 0
    Dim itemPallet_Barcode As Integer = 1
    Dim itemPallet_BatchNumber As Integer = 2
    Dim itemPallet_TglProduksi As Integer = 3
    Dim itemPallet_TglExpired As Integer = 4
    Dim itemPallet_KdBarang As Integer = 5
    Dim itemPallet_NmBarang As Integer = 6
    Dim itemPallet_Jumlah As Integer = 7
    Dim itemPallet_Satuan As Integer = 8
    Dim itemPallet_Kualitas As Integer = 9
    Dim itemPallet_Warna As Integer = 10
    Dim itemPallet_ID As Integer = 11
    Dim itemPallet_QR As Integer = 12
    Dim itemPallet_KdUnikBerjalan As Integer = 13
    Dim itemPallet_Nomor As Integer = 14
    Dim itemPallet_Batch As Integer = 15
    Dim itemPallet_No_Split As Integer = 16


    Dim itemData_Barcode As Integer = 0
    Dim itemData_Batch As Integer = 1
    Dim itemData_Nomor As Integer = 2
    Dim itemData_Jumlah As Integer = 3
    Dim itemData_Satuan As Integer = 4
    Dim itemData_Berat As Integer = 5
    Dim itemData_Tahap As Integer = 6
    Dim itemData_Tgl_Produksi As Integer = 7
    Dim itemData_Tgl_Expired As Integer = 8
    Dim itemData_NoSplit As Integer = 9

    Dim itemBarcode_ID As Integer = 0
    Dim itemBarcode_NomorBaru As Integer = 1
    Dim itemBarcode_Jenis As Integer = 2
    Dim itemBarcode_LokasiTujuan As Integer = 3
    Dim itemBarcode_Total As Integer = 4
    Dim itemBarcode_Satuan As Integer = 5
    Dim itemBarcode_Barcode As Integer = 6
    Dim itemBarcode_Batch As Integer = 7
    Dim itemBarcode_NamaJenis As Integer = 8
    Dim itemBarcode_JenisKategori As Integer = 9
    'Dim itemBarcode_No_Split As Integer = 10

    Dim itemBarcodeDet_Barcode As Integer = 0
    Dim itemBarcodeDet_NomorLama As Integer = 1
    Dim itemBarcodeDet_Jumlah As Integer = 2



    Dim ValTemp_TglProduksi, ValTemp_TglExpired As String



    Private Sub LvBarcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvBarcode.SelectedIndexChanged
        If LvBarcode.Items.Count = 0 Or LvBarcode.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Get_Lv_Data_Barcode(LvBarcode.FocusedItem.Index)

            'Dim NoSplit As String = Txt_NoSplit.Text.Trim
            Dim KdUnikTransaksi As String = Kode_Unik_Transaksi

            LvBarcodeDetail.Items.Clear()
            SQL = "select Barcode, Nomor_Sebelum, sum(jumlah) as Jumlah, No_Production_Order "
            SQL = SQL & "from N_EMI_Validation_GR_Temp "
            'SQL = SQL & "where kode_Perusahaan ='" & KodePerusahaan & "' and No_production_Order='" & NoSplit & "' and userID='" & UserID & "' "
            SQL = SQL & "where kode_Perusahaan ='" & KodePerusahaan & "' and Kode_Unik_Transaksi='" & KdUnikTransaksi & "' and userID='" & UserID & "' "
            SQL = SQL & "and Nomor='" & LvBarcode_ID & "' "
            SQL = SQL & "group by Barcode, Nomor_Sebelum, No_Production_Order "
            SQL = SQL & "order by Nomor_Sebelum "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    Lv = LvBarcodeDetail.Items.Add(Dr("Barcode"))
                    Lv.SubItems.Add(Dr("Nomor_Sebelum"))
                    Lv.SubItems.Add(Dr("Jumlah"))
                    Lv.SubItems.Add(Dr("No_Production_Order"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub EMI_Validasi_GR_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Cmb_LokasiTujuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_LokasiTujuan.SelectedIndexChanged
        If Cmb_LokasiTujuan.SelectedIndex = -1 Then Exit Sub


    End Sub

    Private Sub EMI_Validasi_GR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        If Fitur_Military_Sampling Then
            Lbl_Bypass_MS.Visible = False
        Else
            Lbl_Bypass_MS.Visible = True
        End If

        Kosong()

    End Sub




    Private Sub Cmb_Jenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis.SelectedIndexChanged

        If Cmb_Jenis.Items.Count = 0 Or Cmb_Jenis.SelectedIndex = -1 Then Exit Sub
        If Not ReadyForPackaging Then
            If Cmb_Jenis.SelectedIndex = 0 Then Cmb_Jenis.SelectedIndex = -1
            Cmb_Jenis_Kategori.Items.Clear() : arrIdJenisKategori.Clear()
            Cmb_LokasiTujuan.Items.Clear() : Cmb_LokasiTujuan.SelectedIndex = -1 : arrSO.Clear()
            Exit Sub
        End If

        If arrKdBarangSrap.Item(Cmb_Jenis.SelectedIndex) = "-------" Then Cmb_Jenis.SelectedIndex = -1

        'If Cmb_LokasiTujuan.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            'Cmb_Jenis.SelectedIndex = -1
            'Cmb_Jenis_Kategori.Items.Clear() : arrIdJenisKategori.Clear()
            'SQL = "select Id_Jenis_Kategori, Keterangan from N_EMI_Master_Jenis_Kategori_Produksi "
            'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "order by Id_Jenis_Kategori "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Cmb_Jenis_Kategori.Items.Add(Dr("Keterangan")) : arrIdJenisKategori.Add(Dr("Id_Jenis_Kategori"))
            '    Loop
            'End Using
            'Cmb_Jenis_Kategori.SelectedIndex = -1

            Cmb_LokasiTujuan.Items.Clear() : Cmb_LokasiTujuan.SelectedIndex = -1 : arrSO.Clear()
            Cmb_Jenis_Kategori.Items.Clear() : arrIdJenisKategori.Clear()
            SQL = "select Id_Jenis_Kategori, Keterangan from N_EMI_Master_Jenis_Kategori_Produksi "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Id_Jenis_Kategori "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Jenis_Kategori.Items.Add(Dr("Keterangan")) : arrIdJenisKategori.Add(Dr("Id_Jenis_Kategori"))
                Loop
            End Using


            'Cmb_LokasiTujuan.Items.Clear() : Cmb_LokasiTujuan.SelectedIndex = -1 : arrSO.Clear()
            'Cmb_Jenis_Kategori.Items.Clear() : arrIdJenisKategori.Clear()
            'SQL = "select Id_Jenis_Kategori, Keterangan from N_EMI_Master_Jenis_Kategori_Produksi "
            'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "

            'If Cmb_Jenis.SelectedIndex = 0 Then
            '    SQL = SQL & "and Flag_OK = 'Y' "
            'Else
            '    SQL = SQL & "and Flag_OK is null "
            'End If

            'SQL = SQL & "order by Id_Jenis_Kategori "

            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Cmb_Jenis_Kategori.Items.Add(Dr("Keterangan")) : arrIdJenisKategori.Add(Dr("Id_Jenis_Kategori"))
            '    Loop
            'End Using

            If Cmb_Jenis_Kategori.Items.Count <> 0 Then
                If Cmb_Jenis.SelectedIndex = 0 Then
                    Cmb_Jenis_Kategori.SelectedIndex = 0
                End If
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Cmb_Jenis_Kategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Kategori.SelectedIndexChanged
        If Cmb_Jenis_Kategori.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()


            Dim isProduksi As Boolean = arrGudangProduksi.Contains(arrKdBarangSrap(Cmb_Jenis.SelectedIndex))

            Cmb_LokasiTujuan.Items.Clear() : Cmb_LokasiTujuan.SelectedIndex = -1 : arrSO.Clear()
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' "
            'SQL = SQL & "and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            If isProduksi Then
                'SQL = SQL & "and Flag_Produksi = 'Y' "
                SQL = SQL & "and Flag_GR2_Produksi = 'Y' "
            Else
                'SQL = SQL & "and Flag_Finish_Goods = 'Y' "
                SQL = SQL & "and Flag_GR2_Finish_Goods = 'Y' "
            End If
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    arrSO.Add(dr("kode_stock_owner"))
                    Cmb_LokasiTujuan.Items.Add(dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub HapusToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem1.Click

        If LvBarcode.Items.Count = 0 Or LvBarcode.FocusedItem Is Nothing Then Exit Sub

        'Dim SelectedSplit As String = Txt_NoSplit.Text
        'Dim SelectedSplit As String = LvBarcode.FocusedItem.SubItems(itemBarcode_No_Split).Text
        Dim SelectedNomor As String = LvBarcode.FocusedItem.SubItems(itemBarcode_Batch).Text

        If MessageBox.Show("Yakin Ingin Hapus Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        Dim data As Integer = 0

        Try
            OpenConn()

            SQL = "select Kode_Perusahaan from N_EMI_Validation_GR_Temp where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL &= $"and nomor = '" & SelectedNomor & "' and UserID = '" & UserID & "' and Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    SQL = "delete N_EMI_Validation_GR_Temp where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL &= $"and nomor = '" & SelectedNomor & "' and UserID = '" & UserID & "' and Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "' "
                    ExecuteTrans(SQL)
                Else
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            SQL = "select isnull(count(Kode_Perusahaan),0) as data from N_EMI_Validation_GR_Temp where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL &= $"and UserID = '" & UserID & "' and Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    data = Dr("data")
                Else
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        KosongTab1()
        TampilTemp()

        If data = 0 Then
            Cmb_Jenis.SelectedIndex = -1
            Cmb_Jenis_Kategori.SelectedIndex = -1
            Cmb_Jenis.Enabled = True
        End If



    End Sub

    Private Sub Btn_Simpan_Barcode_Click(sender As Object, e As EventArgs) Handles Btn_Simpan_Barcode.Click

        If Cmb_LokasiTujuan.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi Tujuan Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_LokasiTujuan.DroppedDown = True : Cmb_LokasiTujuan.Focus() : Exit Sub
        ElseIf Cmb_Jenis.Text.Trim.Length = 0 Then
            MessageBox.Show("Jenis Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis.DroppedDown = True : Cmb_Jenis.Focus() : Exit Sub
        ElseIf Cmb_Jenis_Kategori.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Kategori Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis_Kategori.DroppedDown = True : Cmb_Jenis_Kategori.Focus() : Exit Sub
        ElseIf Lv_Data.Items.Count = 0 Then
            MessageBox.Show("Tidak Ada Data yang Disimpan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_Data.Focus() : Exit Sub
        End If

        get_jam()


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            'Dim asdas As Double = arrBarcodeFromSD.Count

            Dim lokasi_awal As String = ""



            'SQL = "select top 1 Nomor from N_EMI_Validation_GR_Temp a "
            'SQL = SQL & "where "
            'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_production_Order = '" & Txt_NoSplit.Text & "' and a.userid='" & UserID & "' "
            'SQL = SQL & "order by Nomor Desc "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        Nomor = Dr("Nomor") + 1
            '    Else
            '        Nomor = 1
            '    End If
            'End Using

            Dim Nomor As Integer = 0
            SQL = "SELECT ISNULL(( "
            SQL = SQL & "SELECT TOP 1 Nomor "
            SQL = SQL & "FROM N_EMI_Validation_GR_Temp a "
            SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "AND a.No_production_Order = '" & Txt_NoSplit.Text & "' "
            SQL = SQL & "AND a.Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "' "
            SQL = SQL & "AND a.userid = '" & UserID & "' "
            SQL = SQL & "ORDER BY Nomor DESC ), 0) AS Nomor "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Nomor = Dr("Nomor") + 1

                    'If Val(Dr("Nomor")) = 0 Then

                    '    Dr.Close()
                    '    SQL = "select top 1 b.Nomor "
                    '    SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Split_Production_Order c "
                    '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = b.kode_perusahaan "
                    '    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and a.no_production_order = c.No_Transaksi "
                    '    SQL = SQL & "and a.Status is null and c.Status is null "
                    '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    '    SQL = SQL & "and a.No_Production_Order = '" & Txt_NoSplit.Text & "' "
                    '    SQL = SQL & "order by b.nomor DESC "
                    '    Using Dr1 = OpenTrans(SQL)
                    '        If Dr1.Read Then
                    '            Nomor = Val(Dr1("Nomor")) + 1
                    '        Else
                    '            Nomor = 1
                    '        End If
                    '    End Using

                    'Else
                    '    Nomor = Dr("Nomor") + 1
                    'End If
                End If
            End Using


            For i As Integer = 0 To Lv_Data.Items.Count - 1
                Get_Lv_Data_GR(i)

                '========================================
                '=     CEK APAKAH SPLIT DI BATALKAN     =
                '========================================
                SQL = "select top 1 a.Kode_Perusahaan from Emi_Split_Production_Order a, emi_production_results b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Production_Order "
                SQL = SQL & "and a.Status = 'Y' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & LvData_NoSplit & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Gagal Simpan, No Split {LvData_NoSplit} Sudah Di Batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "insert into N_EMI_Validation_GR_Temp(Kode_perusahaan, No_production_Order, UserID, Nomor, Barcode, Jenis, "
                SQL = SQL & "Jumlah, Lokasi_Tujuan, Nomor_Sebelum, Satuan, Batch, Tahap, Id_Jenis_Kategori, Jenis_Kategori, Kode_Unik_Transaksi)  "
                SQL = SQL & "values ('" & KodePerusahaan & "', '" & LvData_NoSplit & "', '" & UserID & "', '" & Nomor & "', "
                SQL = SQL & "'" & LvData_Barcode & "', '" & arrKdBarangSrap(Cmb_Jenis.SelectedIndex) & "', '" & HilangkanTanda(LvData_Jumlah) & "', "
                SQL = SQL & "'" & arrSO.Item(Cmb_LokasiTujuan.SelectedIndex) & "', '" & LvData_Nomor & "', '" & LvData_Satuan & "', '" & LvData_Batch & "', '" & LvData_Tahap & "', "
                SQL = SQL & "'" & arrIdJenisKategori(Cmb_Jenis_Kategori.SelectedIndex) & "', '" & Cmb_Jenis_Kategori.Text & "', '" & Kode_Unik_Transaksi & "')"
                ExecuteTrans(SQL)

            Next



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            'MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Lv_Data.Items.Clear()
        Cmb_LokasiTujuan.SelectedIndex = -1
        'Cmb_Jenis.SelectedIndex = -1

        KosongTab1()
        TampilTemp()
    End Sub

    Private Sub get_no_faktur()
        Dim FValidasiGR As String = "VGR-"
        TxtNo_Transaksi.Text = FValidasiGR & Format(tgl_skg, "MMyy") & "-" &
                            General_Class.Get_Last_Number2("Emi_Production_Results_Validation", "No_Transaksi", 5,
                            "Kode_perusahaan", KodePerusahaan,
                            "And", "substring(No_Transaksi, 1, " & Len(FValidasiGR) + 4 & ")", FValidasiGR & Format(tgl_skg, "MMyy"))

    End Sub

    Private Sub Kosong()

        Kode_Unik_Transaksi = Generate_Random_Kode(10)

        isCombine = False

        NoSplitTemp = ""
        SelectedCurrentSplit = ""

        TxtKeterangan.Text = ""
        'Txt_NoSplit.Text = ""
        SelectedVariant = ""
        Txt_Barcode.Text = ""
        Txt_TotFG.Text = ""
        Txt_TotBeratKG.Text = ""
        Txt_ScanBarcode.Text = ""
        Txt_HslProduksi.Text = ""
        Txt_Jumlah.Text = ""
        Txt_Satuan.Text = ""
        Txt_TotFG.Text = ""
        Txt_TotBeratKG.Text = ""
        Btn_Scan.Text = "&Scan"

        Txt_SelectedBatch.Text = ""
        Txt_SelectedKdBarang.Text = ""
        Txt_SelectedNmBarang.Text = ""
        Txt_SelectedID.Text = ""
        Txt_SelectedQR.Text = ""
        Txt_SelectedKdUnikBerjalan.Text = ""
        Txt_TglExpired.Text = ""
        SelectedBatch.Text = ""
        CurrentBatch = ""
        Txt_JmlhKeranjang.Text = ""

        ValTemp_TglProduksi = ""
        ValTemp_TglExpired = ""

        ReadyForPackaging = False

        get_jam()

        Try
            OpenConn()

            get_no_faktur()

            SQL = "delete N_EMI_Validation_GR_Temp where kode_perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
            ExecuteTrans(SQL)

            Cmb_Jenis.Items.Clear() : arrKdBarangSrap.Clear() : arrNamaScrap.Clear() : arrSatuanScrap.Clear() : arrSatuanKecilScrap.Clear()
            Cmb_Jenis_Kategori.Items.Clear()
            Cmb_LokasiTujuan.Items.Clear() : Cmb_LokasiTujuan.SelectedIndex = -1 : arrSO.Clear()

            Cmb_Jenis.Items.Add("FINISHED GOOD") : arrKdBarangSrap.Add("FINISHED GOOD") : arrNamaScrap.Add("FINISHED GOOD") : arrSatuanScrap.Add("FINISHED GOOD")
            'Cmb_Jenis.Items.Add("BLOCKED") : arrKdBarangSrap.Add("REJECTED") : arrNamaScrap.Add("REJECTED") : arrSatuanScrap.Add("REJECTED") : arrSatuanKecilScrap.Add("REJECTED")
            Cmb_Jenis.Items.Add("SAMPLE") : arrKdBarangSrap.Add("SAMPLE") : arrNamaScrap.Add("SAMPLE") : arrSatuanScrap.Add("SAMPLE") : arrSatuanKecilScrap.Add("SAMPLE")
            Cmb_Jenis.Items.Add(" ----------------------- ") : arrKdBarangSrap.Add("-------") : arrNamaScrap.Add("-------") : arrSatuanScrap.Add("-------") : arrSatuanKecilScrap.Add("-------")

            If MenuAsal <> "VALIDASI_GR_MERGE" Then
                arrGudangProduksi.Clear()
                arrSatuanKecilScrap.Add("Finished Good")
                SQL = "select distinct a.kode_barang_Scrap, b.nama, b.satuan as satuan_kecil, c.satuan "
                SQL = SQL & "from emi_binding_scrap a, barang b, barang_detail_satuan c "
                SQL = SQL & "where a.kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang_scrap=B.Kode_Barang "
                SQL = SQL & "and b.kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang "
                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and b.kode_stock_Owner='" & LvPallet_KdSO & "' "
                SQL = SQL & "and c.flag_tampil_display = 'Y' and a.Flag_GR2='Y' "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Cmb_Jenis.Items.Add(Dr("nama")) : arrKdBarangSrap.Add(Dr("kode_barang_Scrap"))
                        arrNamaScrap.Add(Dr("nama")) : arrSatuanScrap.Add(Dr("satuan"))
                        arrSatuanKecilScrap.Add(Dr("satuan_kecil"))
                        arrGudangProduksi.Add(Dr("kode_barang_Scrap"))
                    Loop
                End Using
                Cmb_Jenis.SelectedIndex = -1
                Cmb_Jenis.Enabled = True
            End If


            ExecuteTrans("Delete From N_EMI_Validation_GR_Temp where UserID='" & UserID & "'")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_DataPallet.Columns.Clear() : Lv_DataPallet.Items.Clear()
        Lv_DataPallet.Columns.Add("Lokasi Barang", 0, HorizontalAlignment.Center) ' 0
        Lv_DataPallet.Columns.Add("Barcode", 260, HorizontalAlignment.Left) '1
        Lv_DataPallet.Columns.Add("Batch Number", 0, HorizontalAlignment.Center) '2
        Lv_DataPallet.Columns.Add("Tanggal Produksi", 110, HorizontalAlignment.Center) '3
        Lv_DataPallet.Columns.Add("Tanggal Expired", 0, HorizontalAlignment.Center) '4
        Lv_DataPallet.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '5
        Lv_DataPallet.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left) '6
        Lv_DataPallet.Columns.Add("Jumlah", 110, HorizontalAlignment.Right) '7
        Lv_DataPallet.Columns.Add("Satuan", 70, HorizontalAlignment.Center) '8
        Lv_DataPallet.Columns.Add("Kualitas", 0, HorizontalAlignment.Center) '9
        Lv_DataPallet.Columns.Add("warna", 0, HorizontalAlignment.Center) '10
        Lv_DataPallet.Columns.Add("ID", 0, HorizontalAlignment.Center) '11
        Lv_DataPallet.Columns.Add("QR", 0, HorizontalAlignment.Center) '12
        Lv_DataPallet.Columns.Add("KdUnikBerjalan", 0, HorizontalAlignment.Center) '13
        Lv_DataPallet.Columns.Add("Nomor", 60, HorizontalAlignment.Center).DisplayIndex = 2 '14
        Lv_DataPallet.Columns.Add("Batch", 60, HorizontalAlignment.Center).DisplayIndex = 3 '15
        Lv_DataPallet.Columns.Add("No Split", 130, HorizontalAlignment.Left).DisplayIndex = 3 '16
        Lv_DataPallet.View = View.Details
        Lv_DataPallet.Columns(itemPallet_No_Split).DisplayIndex = 1


        Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
        Lv_Data.Columns.Add("Barcode", 630, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("Batch", 80, HorizontalAlignment.Center) '1
        Lv_Data.Columns.Add("Nomor", 100, HorizontalAlignment.Center) '2
        Lv_Data.Columns.Add("Jumlah", 180, HorizontalAlignment.Right) '3
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '4
        Lv_Data.Columns.Add("Berat", 0, HorizontalAlignment.Center) '5
        Lv_Data.Columns.Add("Tahap", 0, HorizontalAlignment.Center) '6
        'hide
        Lv_Data.Columns.Add("tglproduksi", 0, HorizontalAlignment.Center) '7
        Lv_Data.Columns.Add("tglexpired", 0, HorizontalAlignment.Center) '8
        Lv_Data.Columns.Add("NoSplit", 0, HorizontalAlignment.Center) '9
        Lv_Data.View = View.Details

        LvBarcode.Columns.Clear() : LvBarcode.Items.Clear()
        LvBarcode.Columns.Add("ID", 0, HorizontalAlignment.Center)
        LvBarcode.Columns.Add("Nomor", 50, HorizontalAlignment.Center)
        LvBarcode.Columns.Add("Jenis", 0, HorizontalAlignment.Left)
        LvBarcode.Columns.Add("Lokasi Tujuan", 110, HorizontalAlignment.Left)
        LvBarcode.Columns.Add("Total", 100, HorizontalAlignment.Right)
        LvBarcode.Columns.Add("Satuan", 60, HorizontalAlignment.Center)
        LvBarcode.Columns.Add("Barcode", 0, HorizontalAlignment.Center)
        LvBarcode.Columns.Add("Batch", 50, HorizontalAlignment.Center).DisplayIndex = 2
        LvBarcode.Columns.Add("Jenis", 120, HorizontalAlignment.Center).DisplayIndex = 3
        LvBarcode.Columns.Add("Jenis Kategori", 120, HorizontalAlignment.Center).DisplayIndex = 4
        'LvBarcode.Columns.Add("No Split", 120, HorizontalAlignment.Center).DisplayIndex = 1

        LvBarcode.View = View.Details

        LvBarcodeDetail.Columns.Clear() : LvBarcodeDetail.Items.Clear()
        LvBarcodeDetail.Columns.Add("Barcode", 250, HorizontalAlignment.Left)
        LvBarcodeDetail.Columns.Add("Nomor Lama", 90, HorizontalAlignment.Center)
        LvBarcodeDetail.Columns.Add("Jumlah", 120, HorizontalAlignment.Center)
        LvBarcodeDetail.Columns.Add("No Split", 120, HorizontalAlignment.Left)
        LvBarcodeDetail.View = View.Details

        LvBarcodeDetail.Columns(3).DisplayIndex = 0


        Lv_SummaryPackaging.Columns.Clear() : Lv_SummaryPackaging.Items.Clear()
        Lv_SummaryPackaging.Columns.Add("Lokasi", 0, HorizontalAlignment.Left)
        Lv_SummaryPackaging.Columns.Add("Kode Barang", 110, HorizontalAlignment.Left)
        Lv_SummaryPackaging.Columns.Add("Barang", 200, HorizontalAlignment.Left)
        Lv_SummaryPackaging.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
        Lv_SummaryPackaging.Columns.Add("Satuan", 70, HorizontalAlignment.Center)
        Lv_SummaryPackaging.View = View.Details

        Cmb_Barcode.Items.Clear()
        Cmb_Barcode.Items.Add("Barcode")
        Cmb_Barcode.Items.Add("No Split")
        Cmb_Barcode.SelectedIndex = 0

        'Cmb_Jenis.Items.Clear() : arrKdBarangSrap.Clear() : arrNamaScrap.Clear() : arrSatuanScrap.Clear() : arrSatuanKecilScrap.Clear() : arrBarcodeFromSD.Clear()

        Cmb_Barcode.Enabled = True
        Txt_ScanBarcode.Enabled = True
        Btn_Scan.Enabled = True

        'Cmb_Jenis.Enabled = False
        'Cmb_LokasiTujuan.Enabled = False
        Txt_Jumlah.Enabled = False
        Btn_Tambah.Enabled = False



    End Sub

    Private Sub KosongTab1()


        Txt_Barcode.Text = ""
        Txt_Nomor.Text = ""
        Txt_Satuan.Text = ""
        Txt_HslProduksi.Text = ""
        Txt_Sisa.Text = ""
        Txt_Jumlah.Text = ""
        Txt_TotFG.Text = ""
        Txt_TotBeratKG.Text = ""
        CurrentBatch = ""

        Cmb_Jenis_Kategori.SelectedIndex = -1
        Btn_Tambah.Enabled = True : Btn_Simpan_Barcode.Enabled = True

        Lv_Data.Items.Clear()
        Cmb_LokasiTujuan.SelectedIndex = -1
        'Cmb_Jenis.SelectedIndex = -1
        Cmb_Jenis.Enabled = False
        'arrBarcodeFromSD.Clear()

        LoadFromSD()
        Cmb_Jenis_SelectedIndexChanged(Cmb_Jenis, Nothing)

    End Sub



    Private Sub Get_LvPallet_Data(ByVal index As Integer)

        LvPallet_KdSO = Lv_DataPallet.Items(index).SubItems(itemPallet_KdSo).Text
        LvPallet_Barcode = Lv_DataPallet.Items(index).SubItems(itemPallet_Barcode).Text
        LvPallet_BatchNumber = Lv_DataPallet.Items(index).SubItems(itemPallet_BatchNumber).Text
        LvPallet_TglProduksi = Lv_DataPallet.Items(index).SubItems(itemPallet_TglProduksi).Text
        LvPallet_TglExpired = Lv_DataPallet.Items(index).SubItems(itemPallet_TglExpired).Text
        LvPallet_KdBarang = Lv_DataPallet.Items(index).SubItems(itemPallet_KdBarang).Text
        LvPallet_NmBarang = Lv_DataPallet.Items(index).SubItems(itemPallet_NmBarang).Text
        LvPallet_Jumlah = Lv_DataPallet.Items(index).SubItems(itemPallet_Jumlah).Text
        LvPallet_Satuan = Lv_DataPallet.Items(index).SubItems(itemPallet_Satuan).Text
        LvPallet_Kualitas = Lv_DataPallet.Items(index).SubItems(itemPallet_Kualitas).Text
        LvPallet_Warna = Lv_DataPallet.Items(index).SubItems(itemPallet_Warna).Text
        LvPallet_ID = Lv_DataPallet.Items(index).SubItems(itemPallet_ID).Text
        LvPallet_QR = Lv_DataPallet.Items(index).SubItems(itemPallet_QR).Text
        LvPallet_KdUnikBerjalan = Lv_DataPallet.Items(index).SubItems(itemPallet_KdUnikBerjalan).Text
        LvPallet_Nomor = Lv_DataPallet.Items(index).SubItems(itemPallet_Nomor).Text
        LvPallet_Batch = Lv_DataPallet.Items(index).SubItems(itemPallet_Batch).Text
        LvPallet_No_Split = Lv_DataPallet.Items(index).SubItems(itemPallet_No_Split).Text
    End Sub

    Private Sub Get_Lv_Data_GR(ByVal index As Integer)

        LvData_Barcode = Lv_Data.Items(index).SubItems(itemData_Barcode).Text
        LvData_Batch = Lv_Data.Items(index).SubItems(itemData_Batch).Text
        LvData_Nomor = Lv_Data.Items(index).SubItems(itemData_Nomor).Text
        LvData_Jumlah = Lv_Data.Items(index).SubItems(itemData_Jumlah).Text
        LvData_Satuan = Lv_Data.Items(index).SubItems(itemData_Satuan).Text
        LvData_Berat = Lv_Data.Items(index).SubItems(itemData_Berat).Text
        LvData_Tahap = Lv_Data.Items(index).SubItems(itemData_Tahap).Text

        LvData_Tgl_Produksi = Lv_Data.Items(index).SubItems(itemData_Tgl_Produksi).Text
        LvData_Tgl_Expired = Lv_Data.Items(index).SubItems(itemData_Tgl_Expired).Text
        LvData_NoSplit = Lv_Data.Items(index).SubItems(itemData_NoSplit).Text


    End Sub

    Private Sub Get_Lv_Data_Barcode(ByVal index As Integer)

        LvBarcode_NomorBaru = LvBarcode.Items(index).SubItems(itemBarcode_NomorBaru).Text
        LvBarcode_ID = LvBarcode.Items(index).SubItems(itemBarcode_ID).Text
        LvBarcode_Jenis = LvBarcode.Items(index).SubItems(itemBarcode_Jenis).Text
        LvBarcode_LokasiTujuan = LvBarcode.Items(index).SubItems(itemBarcode_LokasiTujuan).Text
        LvBarcode_Total = LvBarcode.Items(index).SubItems(itemBarcode_Total).Text
        LvBarcode_Satuan = LvBarcode.Items(index).SubItems(itemBarcode_Satuan).Text
        LvBarcode_Barcode = LvBarcode.Items(index).SubItems(itemBarcode_Barcode).Text
        LvBarcode_Batch = LvBarcode.Items(index).SubItems(itemBarcode_Batch).Text
        'LvBarcode_No_Split = LvBarcode.Items(index).SubItems(itemBarcode_No_Split).Text

    End Sub

    Private Sub Get_Lv_Data_BarcodeDet(ByVal index As Integer)

        LvBarcodeDet_Barcode = LvBarcodeDetail.Items(index).SubItems(itemBarcodeDet_Barcode).Text
        LvBarcodeDet_NomorLama = LvBarcodeDetail.Items(index).SubItems(itemBarcodeDet_NomorLama).Text
        LvBarcodeDet_Jumlah = LvBarcodeDetail.Items(index).SubItems(itemBarcodeDet_Jumlah).Text

    End Sub

    Private Sub TampilTemp()
        Try
            OpenConn()

            'Dim NoSplit As String = Txt_NoSplit.Text.Trim

            LvBarcode.Items.Clear()
            LvBarcodeDetail.Items.Clear() : Lv_SummaryPackaging.Items.Clear()
            SQL = "select ROW_NUMBER() OVER (ORDER BY Nomor) as Count, Nomor,  Jenis, Lokasi_Tujuan, sum(jumlah) as Jumlah, satuan, "
            SQL = SQL & "isnull((select top(1) nama from Barang x where x.kode_barang=a.Jenis),Jenis) as Jenis2, Jenis_Kategori "
            SQL = SQL & "from N_EMI_Validation_GR_Temp a "
            'SQL = SQL & "where kode_Perusahaan ='" & KodePerusahaan & "' and No_production_Order='" & NoSplit & "' and userID='" & UserID & "' "
            SQL = SQL & "where kode_Perusahaan ='" & KodePerusahaan & "' and Kode_Unik_Transaksi='" & Kode_Unik_Transaksi & "' and userID='" & UserID & "' "
            SQL = SQL & "group by Nomor, Jenis, Lokasi_Tujuan, Satuan, Jenis_Kategori "
            SQL = SQL & "order by Nomor "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    Lv = LvBarcode.Items.Add(Dr("Nomor"))
                    Lv.SubItems.Add(Dr("Count"))

                    If Dr("Jenis").ToString.ToUpper = "REJECTED" Then
                        Lv.SubItems.Add("Disqualified")
                    Else
                        Lv.SubItems.Add(Dr("Jenis"))
                    End If
                    Lv.SubItems.Add(Dr("Lokasi_Tujuan"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add("X")
                    Lv.SubItems.Add(Dr("Nomor"))
                    Lv.SubItems.Add(Dr("Jenis2"))
                    Lv.SubItems.Add(Dr("Jenis_Kategori"))
                    'Lv.SubItems.Add(Dr("No_Production_Order"))

                Loop
            End Using



            '==========================
            '=     LOAD PACKAGING     =
            '==========================
            Dim TotalInput As Double = 0
            Dim jenisGR As String = ""
            For i As Integer = 0 To LvBarcode.Items.Count - 1
                Get_Lv_Data_Barcode(i)

                If LvBarcode_Jenis.ToUpper.Trim = "FINISHED GOOD" Then
                    TotalInput += Val(HilangkanTanda(LvBarcode_Total))
                End If


            Next




            '======================================
            '=     HITUNG KEBUTUHAN PACKAGING     =
            '======================================

            Dim DetailPackaging As New List(Of (Kd_So As String, KdBahan As String, NmBahan As String, jumlah As Double, Satuan As String, Flag_Pembulatan_Produksi As Boolean))
            Lv_SummaryPackaging.Items.Clear()

            SQL = "select Kode_Unik_Transaksi, No_Production_Order, Jumlah, Jenis_Kategori "
            SQL &= $"from N_EMI_Validation_GR_Temp a "
            SQL &= $"where kode_Perusahaan ='{KodePerusahaan}' "
            SQL &= $"and Kode_Unik_Transaksi='{Kode_Unik_Transaksi}' "
            SQL &= $"and userID='{UserID}' "
            SQL &= $"order by Nomor "
            Using Ds9 = BindingTrans(SQL)
                If Ds9.Tables("MyTable").Rows.Count <> 0 Then
                    For zz As Integer = 0 To Ds9.Tables("MyTable").Rows.Count - 1

                        Dim KdBarang As String = SelectedVariant
                        SQL = "select distinct top 1 b.Kode_Barang, b.Nama, b.Berat "
                        SQL = SQL & "from Barang_SN a, barang b, N_EMI_Validation_GR_Temp c "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
                        SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and (qr_code +'-'+Kode_Unik_Berjalan ) = c.Barcode "
                        SQL = SQL & "and c.No_Production_Order = '" & Ds9.Tables("MyTable").Rows(zz).Item("No_Production_Order") & "' and c.jenis='FINISHED GOOD' and c.userid='" & UserID & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                KdBarang = Dr("Kode_Barang")
                            End If
                        End Using

                        Dim kd_inq As String = ""
                        SQL = "select top(1) kode_barang_inq from barang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_barang = '" & KdBarang & "'  "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                kd_inq = Dr("kode_barang_inq")

                            End If
                        End Using

                        If Ds9.Tables("MyTable").Rows(zz).Item("Jenis_Kategori").ToString.ToUpper = "FINISHED GOOD" Then

                            SQL = "Select a.No_Transaksi, b.Jumlah_Bahan, b.Jumlah_Barang, "
                            SQL = SQL & "b.Kode_Stock_Owner, a.Kode_Barang, b.Kode_Barang As Kode_Bahan, c.Nama ,b.Jumlah, b.Satuan, c.flag_potong_stok, "
                            SQL = SQL & "isnull(c.standar_price,0) As standar_price, isnull(c.Flag_Pembulatan_Produksi,'T') as Flag_Pembulatan_Produksi "
                            SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
                            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
                            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & Ds9.Tables("MyTable").Rows(zz).Item("No_Production_Order") & "' "
                            SQL = SQL & "and a.Status is null and b.jenis <> 'KEMASAN UTAMA' "
                            SQL = SQL & "order by c.nama "
                            Using Ds = BindingTrans(SQL)
                                With Ds.Tables("MyTable")
                                    If .Rows.Count <> 0 Then
                                        For i As Integer = 0 To .Rows.Count - 1

                                            '================================
                                            '=     GET JUMLAH KEBUTUHAN     =
                                            '================================
                                            Dim KebutuhanBarang As Double = 0
                                            Dim KebutuhanBahan As Double = 0
                                            'SQL = "select a.Kode_Bahan, a.Jumlah_Bahan, a.Jumlah_Barang "
                                            'SQL = SQL & "from Barang_Detail_Bahan_Penolong a "
                                            'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                            'SQL = SQL & "and Kode_Barang = '" & kd_inq & "' "
                                            'SQL = SQL & "and Kode_Bahan = '" & .Rows(i).Item("Kode_Bahan") & "' "
                                            'Using Dr = OpenTrans(SQL)
                                            '    If Dr.Read Then
                                            '        KebutuhanBarang = Val(HilangkanTanda(Format(Dr("Jumlah_Barang"), "N4")))
                                            '        KebutuhanBahan = Val(HilangkanTanda(Format(Dr("Jumlah_Bahan"), "N4")))
                                            '    End If
                                            'End Using

                                            KebutuhanBarang = Val(HilangkanTanda(Format(.Rows(i).Item("Jumlah_Barang"), "N4")))
                                            KebutuhanBahan = Val(HilangkanTanda(Format(.Rows(i).Item("Jumlah_Bahan"), "N4")))

                                            'Hitung Kebutuhan Packaging Untuk 1 Data
                                            Dim PackagingDigunakan As Double = Val(HilangkanTanda(Format((Val(HilangkanTanda(Ds9.Tables("MyTable").Rows(zz).Item("Jumlah"))) / KebutuhanBarang) * KebutuhanBahan, "N4")))

                                            Dim IsPembulatan As Boolean = False
                                            If .Rows(i).Item("Flag_Pembulatan_Produksi") = "Y" Then
                                                'PackagingDigunakan = Math.Ceiling(PackagingDigunakan)
                                                IsPembulatan = True
                                            End If

                                            Dim IndexDetailPackaging As Integer = DetailPackaging.FindIndex(Function(x) x.KdBahan = .Rows(i).Item("Kode_Bahan").ToString.Trim)

                                            'TODO Show Detail Packaing Summary

                                            If IndexDetailPackaging <> -1 Then
                                                Dim Data = DetailPackaging(IndexDetailPackaging)
                                                Data.jumlah += Val(Format(PackagingDigunakan, "N2"))
                                                DetailPackaging(IndexDetailPackaging) = Data
                                            Else
                                                'Dim DetailPackaging As New List(Of (Kd_So As String, KdBahan As String, NmBahan As String, jumlah As Double, Satuan As String))
                                                DetailPackaging.Add((Kd_So:= .Rows(i).Item("Kode_Stock_Owner").ToString.Trim, KdBahan:= .Rows(i).Item("Kode_Bahan").ToString.Trim,
                                                                    NmBahan:= .Rows(i).Item("Nama").ToString.Trim, jumlah:=Val(Format(PackagingDigunakan, "N2")),
                                                                    Satuan:= .Rows(i).Item("Satuan").ToString.Trim, Flag_Pembulatan_Produksi:=IsPembulatan))
                                            End If

                                        Next
                                    End If
                                End With
                            End Using

                        End If
                    Next
                End If
            End Using

            For i As Integer = 0 To DetailPackaging.Count - 1

                Dim Data = DetailPackaging(i)

                Dim Jumlah As Double = 0
                If Data.Flag_Pembulatan_Produksi Then
                    Jumlah = Math.Ceiling(Data.jumlah)
                Else
                    Jumlah = Data.jumlah
                End If

                Dim Lv2 As ListViewItem
                Lv2 = Lv_SummaryPackaging.Items.Add(Data.Kd_So)
                Lv2.SubItems.Add(Data.KdBahan)
                Lv2.SubItems.Add(Data.NmBahan)
                'Lv2.SubItems.Add(Format(.Rows(i).Item("JumlahStock"), "N2"))
                Lv2.SubItems.Add(Jumlah)
                Lv2.SubItems.Add(Data.Satuan)
            Next






            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub
    Private Sub Cmb_Barcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Barcode.SelectedIndexChanged
        If Cmb_Barcode.Items.Count = 0 Then Exit Sub

        Txt_ScanBarcode.Text = ""

        If Cmb_Barcode.SelectedIndex = 0 Then
            Txt_ScanBarcode.Enabled = True
            Btn_Scan.Text = "&Scan"
            If Not Cmb_Barcode.Focused Then Txt_ScanBarcode.Focus()

        ElseIf Cmb_Barcode.SelectedIndex = 1 Then
            Txt_ScanBarcode.Enabled = False
            Btn_Scan.Text = "&Pilih Split"
            If Not Cmb_Barcode.Focused Then Btn_Scan.Focus()
        End If

    End Sub

    Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click

        If Cmb_Barcode.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Jenis Barcode", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Barcode.Focus() : Exit Sub
        End If

        If Cmb_Barcode.SelectedIndex = 0 Then

            If Txt_ScanBarcode.Text.Trim.Length = 0 Then
                MessageBox.Show("Harap Isi Barcode Terlebih Dahulu", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_ScanBarcode.Focus() : Exit Sub
            End If

            Try
                OpenConn()

                'Dim NoSplit As String = ""
                Dim CurrentSplit As String = ""

                Dim NoSplit As String = SelectedCurrentSplit

                'SQL = "select b.No_Production_Order as No_Split, a.Lokasi_Gudang as Kode_Stock_Owner, a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Batch_Number,  "
                'SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama as Nama_Barang, "
                ''SQL = SQL & "sum(f.Jumlah) as Jumlah, "
                'SQL = SQL & "isnull((sum(f.Jumlah) -  "
                'SQL = SQL & "(select isnull(sum(jumlah), 0) from N_EMI_Validation_GR_Temp z "
                'SQL = SQL & "where b.kode_perusahaan = z.kode_perusahaan "
                'SQL = SQL & "and b.no_production_order = z.no_production_order "
                'SQL = SQL & "and z.barcode = (a.Qr_Code + '-' + a.Kode_Unik_Berjalan)) ), 0) as Jumlah, "
                'SQL = SQL & "a.Satuan, a.Jenis, e.Keterangan as Kualitas, isnull(a.nomor,0) as nomor "
                'SQL = SQL & "from Emi_Production_Results_Detail_Pallet a, Emi_Production_Results b, EMI_Production_Results_Detail_Barang c, barang d, EMI_Master_Warna e, Barang_SN f "
                'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
                'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                'SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and a.Proses = c.Proses "
                'SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
                'SQL = SQL & "and a.Jenis = e.Kode_Warna "
                'SQL = SQL & "and a.SN_Baru = f.Serial_Number "
                'SQL = SQL & "and b.Status is null "
                'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) ='" & Txt_ScanBarcode.Text.Trim & "' "
                'SQL = SQL & "group by  b.No_Production_Order, a.Lokasi_Gudang , a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) , a.Batch_Number, "
                'SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama, a.Satuan, a.Jenis, e.Keterangan, a.nomor, b.kode_perusahaan "
                'SQL = SQL & "order by b.No_Production_Order, a.Lokasi_Gudang, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) "


                SQL = "select b.No_Production_Order as No_Split, a.Lokasi_Gudang as Kode_Stock_Owner, a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Batch_Number,  "
                SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama as Nama_Barang, "
                'SQL = SQL & "sum(f.Jumlah) as Jumlah, "
                SQL = SQL & "isnull((sum(f.Jumlah) -  "
                SQL = SQL & "(select isnull(sum(jumlah), 0) from N_EMI_Validation_GR_Temp z "
                SQL = SQL & "where b.kode_perusahaan = z.kode_perusahaan "
                SQL = SQL & "and b.no_production_order = z.no_production_order "
                SQL = SQL & "and z.barcode = (a.Qr_Code + '-' + a.Kode_Unik_Berjalan)) ), 0) as Jumlah, "
                SQL = SQL & "a.Satuan, a.Jenis, e.Keterangan as Kualitas, isnull(a.nomor,0) as nomor, a.Tahap, "

                SQL = SQL & "case "
                SQL = SQL & "when isnull(( select top 1 'Y' from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = b.No_Production_Order "
                SQL = SQL & "and z.No_Batch = a.Tahap and z.Flag_Ready_For_Packaging = 'Y' and z.No_GR = '1' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' "
                SQL = SQL & "then 'READY FOR PACKING' "
                SQL = SQL & "when isnull(( select top 1 'Y' from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = b.No_Production_Order "
                SQL = SQL & "and z.No_Batch = a.Tahap and z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging is null and z.No_GR = '1' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' "
                SQL = SQL & "then 'HOLD' "
                SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = b.No_Production_Order and z.No_Batch = a.Tahap), 'U') = 'T' "
                SQL = SQL & "then 'DITOLAK' "
                SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = b.No_Production_Order and z.No_Batch = a.Tahap), 'U') = 'Y' "
                SQL = SQL & "then 'DITERIMA' "
                SQL = SQL & "else 'NO DATA' "
                SQL = SQL & "end as Status_Split, "

                SQL = SQL & "isnull(( "
                SQL = SQL & "select top 1 'Y' from N_EMI_Military_Sampling z "
                SQL = SQL & "where z.kode_perusahaan = a.Kode_Perusahaan and z.status is null "
                SQL = SQL & "and z.No_Split = b.No_Production_Order and z.No_Batch = a.tahap "
                SQL = SQL & "and z.No_GR = '1'  "
                SQL = SQL & "), 'T') as Status_Military_Sampling "

                SQL = SQL & "from Emi_Production_Results_Detail_Pallet a, Emi_Production_Results b, EMI_Production_Results_Detail_Barang c, barang d, EMI_Master_Warna e, Barang_SN f "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
                SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and a.Proses = c.Proses "
                SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
                SQL = SQL & "and a.Jenis = e.Kode_Warna "
                SQL = SQL & "and a.SN_Baru = f.Serial_Number "
                SQL = SQL & "and b.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) ='" & Txt_ScanBarcode.Text.Trim & "' "
                'SQL = SQL & "and a.Qr_Code = '" & DataDic("QrCode") & "' and a.Kode_Unik_Berjalan = '" & DataDic("KdUnikBerjalan") & "' "
                SQL = SQL & "group by a.kode_perusahaan, b.No_Production_Order, a.Lokasi_Gudang , a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) , a.Batch_Number, "
                SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama, a.Satuan, a.Jenis, e.Keterangan, a.nomor, b.kode_perusahaan, a.Tahap "
                SQL = SQL & "order by a.Batch_Number, nomor, b.No_Production_Order, a.Lokasi_Gudang, a.nomor, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        If Fitur_Military_Sampling Then
                            If General_Class.CekNULL(Dr("Status_Military_Sampling")) = "T" Then
                                Dr.Close()
                                CloseConn()
                                MessageBox.Show("Step Military Sampling Terhadap Split Belum Selesai", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If

                        If NoSplit = "" Then
                            SelectedCurrentSplit = Dr("No_Split")
                        Else

                            If NoSplit <> Dr("No_Split") Then
                                isCombine = True
                                If MenuAsal <> "VALIDASI_GR_MERGE" Then
                                    Dr.Close()
                                    CloseConn()
                                    MessageBox.Show("No Split Tidak Boleh Berbeda", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End If

                        For i As Integer = 0 To Lv_DataPallet.Items.Count - 1
                            Get_LvPallet_Data(i)

                            If LvPallet_Barcode.ToUpper = Dr("Barcode").ToString.ToUpper Then
                                Dr.Close()
                                CloseConn()
                                MessageBox.Show("Barcode Sudah Ada", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                        Next

                        Dim Lv As ListViewItem
                        Lv = Lv_DataPallet.Items.Add(Dr("Kode_Stock_Owner"))
                        Lv.SubItems.Add(Dr("Barcode"))
                        Lv.SubItems.Add(Dr("Batch_Number"))
                        Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
                        Lv.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
                        Lv.SubItems.Add(Dr("kode_barang"))
                        Lv.SubItems.Add(Dr("Nama_Barang"))
                        Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                        Lv.SubItems.Add(Dr("Satuan"))
                        Lv.SubItems.Add(Dr("Kualitas"))
                        Lv.SubItems.Add(Dr("Jenis"))
                        Lv.SubItems.Add("X")
                        Lv.SubItems.Add(Dr("Qr_Code"))
                        Lv.SubItems.Add(Dr("Kode_Unik_Berjalan"))
                        Lv.SubItems.Add(Dr("nomor"))
                        Lv.SubItems.Add(Dr("Tahap"))
                        Lv.SubItems.Add(Dr("No_Split"))

                        'Txt_NoSplit.Text = Dr("No_Split")
                        Txt_ScanBarcode.Text = ""
                        SelectedVariant = Dr("kode_barang")

                        If Fitur_Military_Sampling Then
                            If General_Class.CekNULL(Dr("Status_Split")).ToUpper = "DITOLAK" Then
                                Lv.BackColor = Color.DarkRed
                                Lv.ForeColor = Color.White
                            ElseIf General_Class.CekNULL(Dr("Status_Split")).ToUpper = "DITERIMA" Then
                                Lv.BackColor = Color.LightGreen
                            ElseIf General_Class.CekNULL(Dr("Status_Split")).ToUpper = "READY FOR PACKING" Then
                                Lv.BackColor = Color.LightGreen
                            ElseIf General_Class.CekNULL(Dr("Status_Split")).ToUpper = "HOLD" Then
                                Lv.BackColor = Color.LightYellow
                            Else
                                Lv.BackColor = Color.White
                            End If
                        End If

                    Else
                        MessageBox.Show("Data Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        If Lv_DataPallet.Items.Count = 0 Then
                            'Txt_NoSplit.Text = ""
                            SelectedVariant = ""
                        End If
                        Txt_ScanBarcode.Text = ""
                    End If
                End Using

                'Txt_ScanBarcode.Enabled = False
                'Btn_Scan.Enabled = False
                'Cmb_Barcode.Enabled = False

                'If Not Txt_NoSplit.Text = "" Then

                '    SQL = "select top 1 b.Nomor "
                '    SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Split_Production_Order c "
                '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = b.kode_perusahaan "
                '    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and a.no_production_order = c.No_Transaksi "
                '    SQL = SQL & "and a.Status is null and c.Status is null "
                '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                '    SQL = SQL & "and a.No_Production_Order = '" & Txt_NoSplit.Text & "' "
                '    SQL = SQL & "order by b.nomor DESC "
                '    Using Dr = OpenTrans(SQL)
                '        If Dr.Read Then
                '            If General_Class.CekNULL(Dr("Nomor")) = "" Then
                '                Txt_JmlhKeranjang.Text = 1
                '            Else
                '                Txt_JmlhKeranjang.Text = Val(Dr("Nomor")) + 1
                '            End If
                '        Else
                '            Txt_JmlhKeranjang.Text = 1
                '        End If
                '    End Using

                'Else
                '    Txt_JmlhKeranjang.Text = ""
                'End If

                If Not isCombine Then

                    SQL = "select top 1 b.Nomor "
                    SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Split_Production_Order c "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = b.kode_perusahaan "
                    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and a.no_production_order = c.No_Transaksi "
                    SQL = SQL & "and a.Status is null and c.Status is null "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Production_Order = '" & NoSplit & "' "
                    SQL = SQL & "order by b.nomor DESC "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If General_Class.CekNULL(Dr("Nomor")) = "" Then
                                Txt_JmlhKeranjang.Text = 1
                            Else
                                Txt_JmlhKeranjang.Text = Val(Dr("Nomor")) + 1
                            End If
                        Else
                            Txt_JmlhKeranjang.Text = 1
                        End If
                    End Using

                Else
                    Txt_JmlhKeranjang.Text = ""
                End If



                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else

            Dim dataBarcode As New List(Of Dictionary(Of String, String))
            For i As Integer = 0 To Lv_DataPallet.Items.Count - 1
                Get_LvPallet_Data(i)

                Dim dic As New Dictionary(Of String, String)
                dic("QrCode") = LvPallet_QR
                dic("KdUnikBerjalan") = LvPallet_KdUnikBerjalan

                dataBarcode.Add(dic)
            Next

            SD_ValidasiGR_Split.MenuAsal = MenuAsal
            SD_ValidasiGR_Split.arrBarcodeFromParent.Clear()
            SD_ValidasiGR_Split.arrBarcodeFromParent = dataBarcode
            SD_ValidasiGR_Split.ShowDialog()

        End If

    End Sub

    Private Sub Lv_DataPallet_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DataPallet.DoubleClick
        If Lv_DataPallet.Items.Count = 0 Or Lv_DataPallet.FocusedItem.Index = -1 Then Exit Sub

        Get_LvPallet_Data(Lv_DataPallet.FocusedItem.Index)
        SelectedBatch.Text = LvPallet_Batch

        If Not CurrentBatch = "" Then
            If CurrentBatch <> SelectedBatch.Text Then
                'MessageBox.Show("Batch yang Diinput Berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Exit Sub
            End If
        End If



        Txt_Barcode.Text = LvPallet_Barcode
        Txt_HslProduksi.Text = Format(Val(HilangkanTanda(LvPallet_Jumlah)), "N0")
        Txt_Satuan.Text = LvPallet_Satuan
        Txt_Nomor.Text = LvPallet_Nomor
        NoSplitTemp = LvPallet_No_Split

        ValTemp_TglProduksi = LvPallet_TglProduksi
        ValTemp_TglExpired = LvPallet_TglExpired

        Txt_Jumlah.Text = 0

        Dim Sisa As Double = 0
        For i As Integer = 0 To Lv_Data.Items.Count - 1
            Get_Lv_Data_GR(i)
            If LvPallet_Barcode = LvData_Barcode Then
                Sisa += Val(HilangkanTanda(LvData_Jumlah))
            End If
        Next

        Txt_Sisa.Text = Format((Val(HilangkanTanda(LvPallet_Jumlah)) - (Sisa)), "N0")





        'Txt_SelectedBatch.Text = LvPallet_BatchNumber
        'Txt_SelectedKdBarang.Text = LvPallet_KdBarang
        'Txt_SelectedNmBarang.Text = LvPallet_NmBarang
        'Txt_SelectedID.Text = LvPallet_ID
        'Txt_SelectedQR.Text = LvPallet_QR
        'Txt_SelectedKdUnikBerjalan.Text = LvPallet_KdUnikBerjalan
        'Txt_TglExpired.Text = LvPallet_TglExpired


        'Cmb_Jenis.Enabled = True
        'Cmb_LokasiTujuan.Enabled = True
        Txt_Jumlah.Enabled = True
        Btn_Tambah.Enabled = True

        Txt_Jumlah.Focus()


    End Sub

    Private Sub Txt_Jumlah_Leave(sender As Object, e As EventArgs) Handles Txt_Jumlah.Leave
        If Txt_Jumlah.Text.Trim.Length = 0 Then Exit Sub

        Dim Nominal As Double = Val(HilangkanTanda(Txt_Jumlah.Text))

        If Val(HilangkanTanda(Txt_Jumlah.Text)) > Val(HilangkanTanda(Txt_Sisa.Text)) Then
            MessageBox.Show("Jumlah Tidak Boleh Lebih dari Sisa", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jumlah.Text = 0 : Txt_Jumlah.Focus()
            Exit Sub
        End If

        Txt_Jumlah.Text = Format(Nominal, "N0")

    End Sub

    Private Sub Btn_Tambah_Click(sender As Object, e As EventArgs) Handles Btn_Tambah.Click

        If Txt_Jumlah.Text.Trim.Length = 0 Or Val(Txt_Jumlah.Text) = 0 Then
            MessageBox.Show("Jumlah Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jumlah.Focus() : Exit Sub
        ElseIf Txt_Barcode.Text.Trim.Length = 0 Then
            MessageBox.Show("Barcode Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Barcode.Focus() : Exit Sub
        End If

        If Txt_Jumlah.Text.Trim.Length = 0 Then
            If Val(HilangkanTanda(Txt_Jumlah.Text)) > Val(HilangkanTanda(Txt_Sisa.Text)) Then
                MessageBox.Show("Jumlah Tidak Boleh Lebih dari Sisa", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Jumlah.Focus() : Exit Sub
            End If
        End If

        Dim BeratBarang As Double = 0
        Dim Tahapan As Integer = 0
        Dim Batch As Integer = 0


        Try
            OpenConn()

            SQL = "select distinct top 1 b.Kode_Barang, b.Nama, b.Berat "
            SQL = SQL & "from Barang_SN a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and (qr_code +'-'+Kode_Unik_Berjalan ) = '" & Txt_Barcode.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    BeratBarang = Dr("Berat")
                Else
                    CloseConn()
                    MessageBox.Show("Data Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select top 1 Tahap, Proses "
            SQL = SQL & "from Emi_Production_Results_Detail_Pallet "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and (Qr_Code +'-'+Kode_Unik_Berjalan) = '" & Txt_Barcode.Text & "' "
            SQL = SQL & "order by Proses DESC, Tahap DESC "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Tahapan = If(General_Class.CekNULL(Dr("Tahap")) = "", 0, General_Class.CekNULL(Dr("Tahap")))
                    Batch = If(General_Class.CekNULL(Dr("Proses")) = "", 0, General_Class.CekNULL(Dr("Proses")))
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dim BeratBarangKG As Double = (Val(HilangkanTanda(Txt_Jumlah.Text)) * BeratBarang) / 1000

        Dim ada_data As Boolean = False


        If Lv_Data.Items.Count = 0 Then
            If CurrentBatch = "" Then
                CurrentBatch = SelectedBatch.Text
                SelectedBatch.Text = ""
            End If
        End If


        For i As Integer = 0 To Lv_Data.Items.Count - 1
            Get_Lv_Data_GR(i)

            If LvData_Barcode = Txt_Barcode.Text Then

                Lv_Data.Items(i).SubItems(itemData_Jumlah).Text = Val(HilangkanTanda(LvData_Jumlah)) + Val(HilangkanTanda(Txt_Jumlah.Text))
                Lv_Data.Items(i).SubItems(itemData_Berat).Text = Val(HilangkanTanda(LvData_Berat)) + Val(HilangkanTanda(BeratBarangKG))

                ada_data = True

            End If

        Next


        If ada_data = False Then
            Dim Lv As ListViewItem
            Lv = Lv_Data.Items.Add(Txt_Barcode.Text)
            Lv.SubItems.Add(Batch)
            Lv.SubItems.Add(Txt_Nomor.Text)
            Lv.SubItems.Add(Format(Val(HilangkanTanda(Txt_Jumlah.Text)), "N0"))
            Lv.SubItems.Add(Txt_Satuan.Text)
            Lv.SubItems.Add(BeratBarangKG)
            Lv.SubItems.Add(Tahapan)

            Lv.SubItems.Add(ValTemp_TglProduksi)
            Lv.SubItems.Add(ValTemp_TglExpired)
            Lv.SubItems.Add(NoSplitTemp)
        End If



        Txt_Barcode.Text = ""
        Txt_HslProduksi.Text = ""
        Txt_Sisa.Text = ""
        Txt_Satuan.Text = ""
        Txt_Jumlah.Text = ""
        Txt_Nomor.Text = ""

        ValTemp_TglProduksi = ""
        ValTemp_TglExpired = ""

        'Txt_SelectedBatch.Text = ""
        'Txt_SelectedKdBarang.Text = ""
        'Txt_SelectedNmBarang.Text = ""
        'Txt_SelectedID.Text = ""
        'Txt_SelectedQR.Text = ""
        'Txt_SelectedKdUnikBerjalan.Text = ""
        'Txt_TglExpired.Text = ""

        'Cmb_Jenis.SelectedIndex = -1
        'Cmb_LokasiAwal.SelectedIndex = -1
        'Cmb_LokasiTujuan.SelectedIndex = -1

        'Cmb_Jenis.Enabled = False
        'Cmb_LokasiTujuan.Enabled = False
        'Txt_Jumlah.Enabled = False
        'Btn_Tambah.Enabled = False

        'Dim Sisa As Double = 0
        'For i As Integer = 0 To Lv_Data.Items.Count - 1
        '    Get_Lv_Data_GR(i)
        '    If LvPallet_Barcode = LvData_Barcode Then
        '        Sisa += Val(HilangkanTanda(LvData_Jumlah))
        '    End If
        'Next
        'Txt_Sisa.Text = Format(Val(HilangkanTanda(Txt_HslProduksi.Text)) - Sisa, "N0")



        Hitung_Data()
        If Fitur_Military_Sampling Then
            CekMilitarySampling(NoSplitTemp)
        Else
            ReadyForPackaging = True
        End If

        If Not ReadyForPackaging Then
            MessageBox.Show("Batch Belum siap dipackaging", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        NoSplitTemp = ""

        Lv_DataPallet.Focus()
    End Sub

    Private Sub CekMilitarySampling(ByVal NoSplit As String)
        If Lv_Data.Items.Count = 0 Then Exit Sub


        Try
            OpenConn()

            For i As Integer = 0 To Lv_Data.Items.Count - 1

                Get_Lv_Data_GR(i)

                SQL = "select top 1 Kode_Perusahaan, Tahap_Military_Sampling, Flag_Ready_For_Packaging from N_EMI_Military_Sampling where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Split = '" & NoSplit & "' "
                SQL = SQL & "and No_Batch = '" & LvData_Tahap & "' and Flag_Ready_For_Packaging = 'Y' order by Tahap_Military_Sampling DESC"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        ReadyForPackaging = True

                        'If General_Class.CekNULL(Dr("Flag_Ready_For_Packaging")) = "" Then
                        '    ReadyForPackaging = False
                        '    Cmb_Jenis.SelectedIndex = -1
                        '    Dr.Close()
                        '    Exit For
                        'Else
                        '    ReadyForPackaging = True
                        'End If

                    Else
                        Cmb_Jenis.SelectedIndex = -1
                        ReadyForPackaging = False
                    End If
                End Using


            Next



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If TxtKeterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKeterangan.Focus() : Exit Sub
            'ElseIf Txt_NoSplit.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Harap Lakukan Scan Barang Terlebih Dahulu", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Txt_ScanBarcode.Focus() : Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()


            '========================
            '=     INSERT INDUK     =
            '========================
            SQL = "insert into Emi_Production_Results_Validation(Kode_Perusahaan, No_Transaksi, No_Production_Order, Tanggal, jam, Keterangan, UserID)  "
            SQL = SQL & "values ('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text.Trim & "', '-', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & TxtKeterangan.Text & "', '" & UserID & "')"
            ExecuteTrans(SQL)


            Dim arrNoSplitInput As New ArrayList
            Dim CurrentNoSplit As String = ""

            'Pastikan Semua Data Di dalamny tersummary
            'Berdasarkan data Temp yang di ambil, Petakan Berdasarkan Barcode Baru
            SQL = "select nomor as ID, Jenis, lokasi_tujuan, sum(jumlah) as Jumlah from N_EMI_Validation_GR_Temp a "
            SQL = SQL & "where "
            'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_production_Order = '" & Txt_NoSplit.Text & "' and a.userid='" & UserID & "' "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "' and a.userid='" & UserID & "' "
            SQL = SQL & "group by nomor, Jenis, lokasi_tujuan "
            SQL = SQL & "order by Nomor asc "
            Using dsNomor = BindingTrans(SQL)
                If dsNomor.Tables("MyTable").Rows.Count <> 0 Then
                    For IndNomor = 0 To dsNomor.Tables("MyTable").Rows.Count - 1


                        Dim SumHPPAwal As Double = 0
                        Dim sumPackaging As Double = 0
                        Dim ArrHPP_GroupJenisID As New ArrayList
                        Dim ArrHPP_GroupJenisNm As New ArrayList
                        Dim ArrHPP_Lokasi As New ArrayList
                        Dim ArrHPP_Akun As New ArrayList
                        Dim ArrHPP_Nilai As New ArrayList

                        'Kode Unik Berjalan ny Taruh disini agar dalam 1 NOmmor, Barcodeny Sama
                        Dim Kode_Berjalan As String = Generate_Random_Kode(10)
                        Dim ID_Nomor As String = dsNomor.Tables("MyTable").Rows(IndNomor).Item("ID")
                        Dim Lks_tujuan_Nomor As String = dsNomor.Tables("MyTable").Rows(IndNomor).Item("lokasi_tujuan")
                        Dim Jenis_Nomor As String = dsNomor.Tables("MyTable").Rows(IndNomor).Item("Jenis")
                        Dim Jumlah_Nomor As Double = dsNomor.Tables("MyTable").Rows(IndNomor).Item("Jumlah")
                        Dim qrCodeTemp As String = ""

                        Dim newQrCode As String = ""

                        Dim Tanggal_Produksi_Pertama As String = ""
                        Dim Tanggal_Expired_Pertama As String = ""
                        Dim Tanggal_Masuk_Pertama As String = ""





#Region "Create Data Per Nomor"

                        Dim Top1NoSplit As String = ""
                        Dim lokasi_awal As String = ""

                        'Setelah di petakan, baru update per barcode
                        SQL = "select Barcode, Nomor_Sebelum, satuan, No_Production_Order, sum(jumlah) as Jumlah, Tahap, Jenis, Id_Jenis_Kategori from N_EMI_Validation_GR_Temp a "
                        SQL = SQL & "where "
                        'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_production_Order = '" & Txt_NoSplit.Text & "' and a.userid='" & UserID & "' "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "' and a.userid='" & UserID & "' "
                        SQL = SQL & "and a.nomor='" & ID_Nomor & "' "
                        SQL = SQL & "group by Barcode, Nomor_Sebelum, satuan, Tahap, Jenis, Id_Jenis_Kategori, No_Production_Order "
                        SQL = SQL & "order by Nomor_Sebelum asc "
                        Using dsPallet = BindingTrans(SQL)
                            If dsPallet.Tables("MyTable").Rows.Count <> 0 Then
                                For IndPallet = 0 To dsPallet.Tables("MyTable").Rows.Count - 1

                                    If Top1NoSplit = "" Then
                                        Top1NoSplit = dsPallet.Tables("MyTable").Rows(IndPallet).Item("No_Production_Order")
                                        CurrentNoSplit = dsPallet.Tables("MyTable").Rows(IndPallet).Item("No_Production_Order")
                                    End If

                                    Dim NoSplitt As String = dsPallet.Tables("MyTable").Rows(IndPallet).Item("No_Production_Order")
                                    Dim Barcode_Pallet As String = dsPallet.Tables("MyTable").Rows(IndPallet).Item("Barcode")
                                    Dim Nomor_Pallet As String = dsPallet.Tables("MyTable").Rows(IndPallet).Item("Nomor_Sebelum")
                                    Dim Satuan_Pallet As String = dsPallet.Tables("MyTable").Rows(IndPallet).Item("satuan")
                                    Dim Jumlah_Pallet As String = dsPallet.Tables("MyTable").Rows(IndPallet).Item("Jumlah")
                                    Dim Tahap As String = dsPallet.Tables("MyTable").Rows(IndPallet).Item("Tahap")
                                    Dim Id_Jenis_Kategori As Double = dsPallet.Tables("MyTable").Rows(IndPallet).Item("Id_Jenis_Kategori")


                                    Dim KodeAsal As String = ""
                                    Dim BatchNumber As String = ""
                                    Dim Warna As String = ""



                                    '========================================
                                    '=     CEK APAKAH SPLIT DI BATALKAN     =
                                    '========================================
                                    SQL = "select top 1 a.Kode_Perusahaan from Emi_Split_Production_Order a, emi_production_results b "
                                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Production_Order "
                                    SQL = SQL & "and a.Status = 'Y' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & NoSplitt & "'"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Gagal Simpan, Split Sudah Di Batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using


                                    Dim kd_barang As String = ""
                                    SQL = "select a.kode_barang from Emi_Split_Production_Order a "
                                    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & NoSplitt & "'"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            kd_barang = Dr("kode_barang")
                                        Else

                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Data Tidak di temukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using

                                    Dim Lks_Awal As String = ""
                                    SQL = "select top(1) b.lokasi_gudang from emi_production_results a, emi_production_results_detail_pallet b "
                                    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Order = '" & NoSplitt & "' "
                                    SQL = SQL & "and a.kode_perusahaan=b.kode_perusahaan and a.no_transaksi=b.no_transaksi and a.status is null "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Lks_Awal = Dr("lokasi_gudang")
                                        Else

                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Data Tidak di temukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using




                                    'Ini Ambil data barang yg berubah kode atau nggak
                                    '3 Data Teratas Pake Kode Barang, Di bawahnya menyesuaikan Master

                                    If dsPallet.Tables("MyTable").Rows(IndPallet).Item("Jenis").ToString.ToUpper.Trim = "FINISHED GOOD" Or
                                        dsPallet.Tables("MyTable").Rows(IndPallet).Item("Jenis").ToString.ToUpper.Trim = "REJECTED" Or
                                        dsPallet.Tables("MyTable").Rows(IndPallet).Item("Jenis").ToString.ToUpper.Trim = "SAMPLE" Then

                                        SQL = "select a.kode_barang from Emi_Split_Production_Order a "
                                        'SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Txt_NoSplit.Text & "'"
                                        SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & NoSplitt & "'"
                                        Using Dr4 = OpenTrans(SQL)
                                            If Dr4.Read Then
                                                kd_barang = Dr4("kode_barang")
                                            Else

                                                Dr4.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Data Tidak di temukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End Using

                                    Else

                                        kd_barang = dsPallet.Tables("MyTable").Rows(IndPallet).Item("Jenis")

                                    End If


                                    'Semua Data di Replace, dan Ambil Top 1
                                    SQL = "Select distinct top 1 b.Kode_Barang, b.Nama, b.Berat, a.Qr_Code "
                                    SQL = SQL & "From Barang_SN a, barang b, N_EMI_Validation_GR_Temp c "
                                    SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Stock_Owner = b.Kode_Stock_Owner And a.Kode_Barang = b.Kode_Barang "
                                    SQL = SQL & "And a.Kode_Perusahaan = c.Kode_Perusahaan And (qr_code +'-'+Kode_Unik_Berjalan ) = c.Barcode "
                                    SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and (qr_code +'-'+ Kode_Unik_Berjalan ) = '" & Barcode_Pallet & "' and c.userid='" & UserID & "' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            qrCodeTemp = Dr("Qr_Code")
                                        End If
                                    End Using

                                    'Berdasarkan Barcode, Ambil Data Sebelum
                                    SQL = "select top 1 b.No_Production_Order as No_Split, a.Qr_Code, a.Qr_Code, a.Kode_Unik_Berjalan, a.Kode_Unik_Asal, a.SN_Baru, a.Tgl_Expired, a.Tgl_Produksi, "
                                    SQL = SQL & "a.Kode_Unik_Asal, d.Jumlah as Stock_SN, a.Lokasi_Gudang as Kode_Stock_Owner, c.Kode_Barang, a.Batch_Number, a.Jenis, a.Jumlah, a.Satuan, d.Tgl_Masuk "
                                    SQL = SQL & "from Emi_Production_Results_Detail_Pallet a, Emi_Production_Results b, EMI_Production_Results_Detail_Barang c, Barang_SN d "
                                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
                                    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                                    SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and a.Proses = c.Proses "
                                    SQL = SQL & "and a.SN_Baru = d.Serial_Number "
                                    SQL = SQL & "and b.Status is null "
                                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and b.No_Production_Order = '" & NoSplitt & "' "
                                    SQL = SQL & "and a.Qr_Code+'-'+a.Kode_Unik_Berjalan = '" & Barcode_Pallet & "' "
                                    SQL = SQL & "and d.jumlah<>0 "
                                    SQL = SQL & "order by a.Tgl_Expired "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then

                                            'Pastikan Batch Number di GR 1 SAMA
                                            If newQrCode = "" Then
                                                newQrCode = Generate_QR_Batch(kd_barang, Dr("Batch_Number"))
                                            End If

                                            KodeAsal = Dr("Kode_Unik_Asal")
                                            BatchNumber = Dr("Batch_Number")
                                            Warna = Dr("Jenis")

                                        Else
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using



                                    '===========================
                                    '=     BARANG PRODUKSI     =
                                    '===========================
                                    Dim sisaPotong As Double = 0
                                    Dim JumlahDipotong As Double = 0
                                    'Data Dikurangi Secara FIFO, Karena Dalam 1 Barcode Punya banyak Data
                                    SQL = "select b.No_Production_Order as No_Split, a.Qr_Code, a.Qr_Code, a.Kode_Unik_Berjalan, a.Kode_Unik_Asal, a.SN_Baru, a.Tgl_Expired, a.Tgl_Produksi, "
                                    SQL = SQL & "a.Kode_Unik_Asal, d.Jumlah as Stock_SN, a.Lokasi_Gudang as Kode_Stock_Owner, c.Kode_Barang, a.Batch_Number, a.Jenis, a.Jumlah, a.Satuan, d.Tgl_Masuk "
                                    SQL = SQL & "from Emi_Production_Results_Detail_Pallet a, Emi_Production_Results b, EMI_Production_Results_Detail_Barang c, Barang_SN d "
                                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
                                    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                                    SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and a.Proses = c.Proses "
                                    SQL = SQL & "and a.SN_Baru = d.Serial_Number "
                                    SQL = SQL & "and b.Status is null "
                                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and b.No_Production_Order = '" & NoSplitt & "' "
                                    SQL = SQL & "and a.Qr_Code+'-'+a.Kode_Unik_Berjalan = '" & Barcode_Pallet & "' and d.jumlah<>0 "
                                    SQL = SQL & "order by a.Tgl_Expired "
                                    Using Ds = BindingTrans(SQL)
                                        With Ds.Tables("MyTable")
                                            If .Rows.Count <> 0 Then
                                                'Nilai Berdasarkan data yg sudah di input
                                                sisaPotong = Val(HilangkanTanda(Jumlah_Pallet))
                                                For j As Integer = 0 To .Rows.Count - 1
                                                    If sisaPotong = 0 Then
                                                        Exit For
                                                    ElseIf sisaPotong < 0 Then
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Terdapat Kesalahan saat Potong Barang Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If

                                                    Dim JumlahInsert As Double = 0
                                                    Dim JumlahKurang As Double = 0
                                                    Dim Satuan As String = ""

                                                    If Tanggal_Produksi_Pertama = "" Then
                                                        Tanggal_Produksi_Pertama = .Rows(j).Item("Tgl_Produksi")
                                                    End If

                                                    If Tanggal_Expired_Pertama = "" Then
                                                        Tanggal_Expired_Pertama = .Rows(j).Item("Tgl_Expired")
                                                    End If

                                                    If Tanggal_Masuk_Pertama = "" Then
                                                        Tanggal_Masuk_Pertama = .Rows(j).Item("Tgl_Masuk")
                                                    End If

#Region "Bagian_POtong"
                                                    If sisaPotong < Val(HilangkanTanda(.Rows(j).Item("Stock_SN"))) Or sisaPotong = Val(HilangkanTanda(.Rows(j).Item("Stock_SN"))) Then

                                                        'Ambil Jumlah yg Kepotong
                                                        JumlahKurang = sisaPotong

                                                        'Untuk Data Selain Finished GOOD dan Rejected dan SAMPLE, data satuannya KG, Jadi Data yg Masuk Harus di Convert
                                                        If Jenis_Nomor.ToUpper = "FINISHED GOOD" Or Jenis_Nomor.ToUpper = "REJECTED" Or Jenis_Nomor.ToUpper = "SAMPLE" Then
                                                            JumlahInsert = sisaPotong
                                                            Satuan = .Rows(j).Item("Satuan").ToString.Trim
                                                        Else

                                                            Satuan = "KG"
                                                            'UBAH SATUAN
                                                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & .Rows(j).Item("Kode_Barang") & "', "
                                                            SQL = SQL & "'" & .Rows(j).Item("Satuan") & "', 'KG', '" & HilangkanTanda(sisaPotong) & "' ) as hasil"
                                                            Using Dr = OpenTrans(SQL)
                                                                If Dr.Read Then
                                                                    JumlahInsert = Val(HilangkanTanda(Dr("hasil")))
                                                                Else
                                                                    CloseTrans()
                                                                    CloseConn()
                                                                    MessageBox.Show("Terdapat Kesalahan saat Ubah Satuan Scrap", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                    Exit Sub
                                                                End If
                                                            End Using
                                                        End If

                                                        SQL = "Update barang set "
                                                        SQL = SQL & "Good_Stock = Good_Stock - " & HilangkanTanda(sisaPotong) & ",  "
                                                        SQL = SQL & "Jumlah_Bags=Jumlah_Bags-" & HilangkanTanda(sisaPotong) & " "
                                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_stock_owner = '" & .Rows(j).Item("kode_stock_owner") & "' and kode_barang = '" & .Rows(j).Item("Kode_Barang") & "' "
                                                        ExecuteTrans(SQL)

                                                        SQL = "Update barang_sn set jumlah = jumlah - " & HilangkanTanda(sisaPotong) & ",  "
                                                        SQL = SQL & "Jumlah_Bags=Jumlah_Bags-" & HilangkanTanda(sisaPotong) & " where "
                                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_stock_owner = '" & .Rows(j).Item("kode_stock_owner") & "' and "
                                                        SQL = SQL & "kode_barang = '" & .Rows(j).Item("Kode_Barang") & "' and "
                                                        SQL = SQL & "serial_number = '" & .Rows(j).Item("SN_Baru") & "'"
                                                        ExecuteTrans(SQL)

                                                        JumlahDipotong += sisaPotong
                                                        sisaPotong = 0

                                                    ElseIf sisaPotong > Val(HilangkanTanda(.Rows(j).Item("Stock_SN"))) Then

                                                        'Ambil Jumlah yg Kepotong
                                                        JumlahKurang = Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4")))


                                                        If Jenis_Nomor.ToUpper = "FINISHED GOOD" Or Jenis_Nomor.ToUpper = "REJECTED" Or Jenis_Nomor.ToUpper = "SAMPLE" Then
                                                            JumlahInsert = Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4")))
                                                            Satuan = .Rows(j).Item("Satuan").ToString.Trim
                                                        Else

                                                            Satuan = "KG"

                                                            'UBAH SATUAN
                                                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & .Rows(j).Item("Kode_Barang") & "', "
                                                            SQL = SQL & "'" & .Rows(j).Item("Satuan") & "', 'KG', '" & Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4"))) & "' ) as hasil"
                                                            Using Dr = OpenTrans(SQL)
                                                                If Dr.Read Then
                                                                    JumlahInsert = Val(HilangkanTanda(Dr("hasil")))
                                                                Else
                                                                    CloseTrans()
                                                                    CloseConn()
                                                                    MessageBox.Show("Terdapat Kesalahan saat Ubah Satuan Scrap", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                    Exit Sub
                                                                End If
                                                            End Using
                                                        End If

                                                        SQL = "Update barang set "
                                                        SQL = SQL & "Good_Stock = Good_Stock - " & Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4"))) & ",  "
                                                        SQL = SQL & "Jumlah_Bags=Jumlah_Bags - " & Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4"))) & " "
                                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_stock_owner = '" & .Rows(j).Item("kode_stock_owner") & "' and kode_barang = '" & .Rows(j).Item("Kode_Barang") & "' "
                                                        ExecuteTrans(SQL)

                                                        SQL = "Update barang_sn set jumlah = jumlah - " & Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4"))) & ",  "
                                                        SQL = SQL & "Jumlah_Bags=Jumlah_Bags - " & Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4"))) & " "
                                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_stock_owner = '" & .Rows(j).Item("kode_stock_owner") & "' and "
                                                        SQL = SQL & "kode_barang = '" & .Rows(j).Item("kode_barang") & "' and "
                                                        SQL = SQL & "serial_number = '" & .Rows(j).Item("SN_Baru") & "'"
                                                        ExecuteTrans(SQL)

                                                        JumlahDipotong += Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4")))
                                                        sisaPotong = sisaPotong - Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4")))
                                                    Else
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang " & kd_barang & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If

#End Region


                                                    '====================================
                                                    '=     CEK APAKAH JUMLAH SESUAI     =
                                                    '====================================
                                                    SQL = "SELECT "
                                                    SQL = SQL & "ROUND(SUM(good_stock), 4) AS good_stock, "
                                                    SQL = SQL & "ISNULL((SELECT ROUND(SUM(jumlah), 4) FROM Barang_sn x WHERE a.kode_Barang = x.kode_Barang AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.kode_Perusahaan = x.kode_Perusahaan), 0) AS Jumlah_sn, "
                                                    SQL = SQL & "ISNULL(ROUND(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
                                                    SQL = SQL & "ISNULL((SELECT ROUND(SUM(Jumlah_Bags), 4) FROM Barang_sn y WHERE a.kode_Barang = y.kode_Barang AND a.Kode_Stock_Owner = y.Kode_Stock_Owner AND a.kode_Perusahaan = y.Kode_Perusahaan), 0) AS jumlah_bags_sn "
                                                    SQL = SQL & "FROM "
                                                    SQL = SQL & "barang a "
                                                    SQL = SQL & "WHERE "
                                                    SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' And a.Kode_Stock_Owner = '" & .Rows(j).Item("kode_stock_owner") & "' AND a.Kode_Barang = '" & .Rows(j).Item("kode_barang") & "' "
                                                    SQL = SQL & "GROUP BY "
                                                    SQL = SQL & "a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan"
                                                    Using Ds2 = BindingTrans(SQL)
                                                        If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                                            Dim Stock_Barang As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("good_stock"), "N4")))
                                                            Dim Stock_Sn As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn"), "N4")))
                                                            Dim Bags_Barang As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang"), "N4")))
                                                            Dim Bags_Sn As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn"), "N4")))

                                                            If Stock_Barang <> Stock_Sn Or Bags_Barang <> Bags_Sn Then
                                                                CloseTrans()
                                                                CloseConn()
                                                                MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                Exit Sub
                                                            End If
                                                        Else
                                                            CloseTrans()
                                                            CloseConn()
                                                            MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub

                                                        End If

                                                    End Using


                                                    Dim KualitasBarang As String = ""
                                                    If Jenis_Nomor.ToUpper = "REJECTED" Then
                                                        If Jenis_Nomor.ToUpper = "REJECTED" Then
                                                            KualitasBarang = "MERAH"
                                                        Else
                                                            KualitasBarang = .Rows(j).Item("Jenis")
                                                        End If
                                                    Else
                                                        KualitasBarang = "HIJAU"
                                                    End If


                                                    'Summary HPP Yg Udah Kepotong
                                                    SumHPPAwal += Get_Harga_SN(.Rows(j).Item("SN_Baru")) * JumlahKurang

                                                    Dim Nilai_Packaging As Double = 0

                                                    '=========================
                                                    '=     INSERT DETAIL     =
                                                    '=========================
                                                    SQL = "insert into Emi_Production_Results_Validation_Detail "
                                                    SQL = SQL & "(Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner_Awal, Kode_Stock_Owner_Tujuan, Kode_Barang, "
                                                    SQL = SQL & "Serial_Number_Awal, Serial_Number_Tujuan, Batch_Number, Warna, Jumlah, Satuan, Jenis, Nomor, Tahap, Jumlah_awal, No_Split ) "
                                                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text.Trim & "', '" & Lks_Awal & "', '" & Lks_tujuan_Nomor & "', "
                                                    SQL = SQL & "'" & kd_barang & "', '" & .Rows(j).Item("SN_Baru") & "', "
                                                    SQL = SQL & "NULL, '" & .Rows(j).Item("Batch_Number") & "', '" & KualitasBarang & "', '" & HilangkanTanda(JumlahInsert) & "', "
                                                    SQL = SQL & "'" & Satuan & "', '" & Jenis_Nomor & "', '" & ID_Nomor & "', '" & Tahap & "', '" & JumlahKurang & "', '" & NoSplitt & "')"
                                                    ExecuteTrans(SQL)

                                                    '==========================
                                                    '=     GET URUT DETAiL     =
                                                    '==========================
                                                    Dim NoUrut_DetailResult As Integer = 0
                                                    SQL = "select IDENT_CURRENT('Emi_Production_Results_Validation_Detail') as urutan"
                                                    Using Dr = OpenTrans(SQL)
                                                        If Dr.Read Then
                                                            NoUrut_DetailResult = Dr("urutan")
                                                        End If
                                                    End Using

                                                    '===================================
                                                    '=     GET KODE BARANG INQUIRY     =
                                                    '===================================
                                                    'karena kode packaging pake kode barang inq
                                                    Dim kd_inq As String = ""
                                                    SQL = "select top(1) kode_barang_inq from barang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_barang = '" & kd_barang & "'  "
                                                    Using Dr = OpenTrans(SQL)
                                                        If Dr.Read Then
                                                            kd_inq = Dr("kode_barang_inq")
                                                        Else
                                                            Dr.Close()
                                                            CloseTrans()
                                                            CloseConn()
                                                            MessageBox.Show("Data Tidak di temukan . !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    End Using


                                                    'Potong Packaging Khusus Finnished GOOD, Karena Barang di Packing

#Region "Bagian_Potong_Packaging"

                                                    '====================================
                                                    '=     CEK APAKAH SCRAP ATAU FG     =
                                                    '====================================
                                                    If Jenis_Nomor.ToUpper = "FINISHED GOOD" Then

                                                        '==================================
                                                        '=     POTONG STOCK PACKAGING     =
                                                        '==================================
                                                        ' Get Data Packing Yang Digunakan
                                                        SQL = "select a.No_Transaksi, b.Jumlah_Bahan, b.Jumlah_Barang, "
                                                        SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang as Kode_Bahan, c.Nama ,b.Jumlah, b.Satuan, c.flag_potong_stok, "
                                                        SQL = SQL & "isnull(c.standar_price,0) as standar_price, isnull(c.Flag_Pembulatan_Produksi,'T') as Flag_Pembulatan_Produksi "
                                                        SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
                                                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
                                                        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
                                                        SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & NoSplitt & "' "
                                                        SQL = SQL & "and a.Status is null and b.jenis <> 'KEMASAN UTAMA' "
                                                        SQL = SQL & "order by c.nama "
                                                        Using Ds1 = BindingTrans(SQL)
                                                            If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                                                For k As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                                                    '================================
                                                                    '=     GET JUMLAH KEBUTUHAN     =
                                                                    '================================
                                                                    Dim KebutuhanBarang As Double = 0
                                                                    Dim KebutuhanBahan As Double = 0

                                                                    KebutuhanBarang = Val(HilangkanTanda(Format(Ds1.Tables("MyTable").Rows(k).Item("Jumlah_Barang"), "N4")))
                                                                    KebutuhanBahan = Val(HilangkanTanda(Format(Ds1.Tables("MyTable").Rows(k).Item("Jumlah_Bahan"), "N4")))

                                                                    'Hitung Kebutuhan Packaging Untuk 1 Data
                                                                    Dim PackagingDigunakan As Double = Val(HilangkanTanda(Format((Val(HilangkanTanda(JumlahInsert)) / KebutuhanBarang) * KebutuhanBahan, "N4")))

                                                                    If Ds1.Tables("MyTable").Rows(k).Item("Flag_Pembulatan_Produksi") = "Y" Then
                                                                        PackagingDigunakan = Math.Ceiling(PackagingDigunakan)
                                                                    End If

                                                                    'TODO Potong Packaging

                                                                    '=========================================
                                                                    '=     INSERT TABLE PACKAGING DETAIL     =
                                                                    '=========================================
                                                                    SQL = "insert into Emi_Production_Results_Validation_Packaging_Detail (Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner, Kode_Barang, "
                                                                    SQL = SQL & "Jumlah, Satuan, Urut_Detail) "
                                                                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & TxtNo_Transaksi.Text.Trim & "', '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner") & "', '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "', "
                                                                    SQL = SQL & "'" & HilangkanTanda(PackagingDigunakan) & "', "
                                                                    SQL = SQL & "'" & Ds1.Tables("MyTable").Rows(k).Item("Satuan") & "', '" & NoUrut_DetailResult & "')"
                                                                    ExecuteTrans(SQL)

                                                                    '====================================
                                                                    '=     GET URUT PACKAGING DETAL     =
                                                                    '====================================
                                                                    Dim NoUrut_PackagingDetail As Integer = 0
                                                                    SQL = "select IDENT_CURRENT('Emi_Production_Results_Validation_Packaging_Detail') as urutan"
                                                                    Using Dr = OpenTrans(SQL)
                                                                        If Dr.Read Then
                                                                            NoUrut_PackagingDetail = Dr("urutan")
                                                                        End If
                                                                    End Using

                                                                    '===============================
                                                                    '=     POTONG STOCK BARANG     =
                                                                    '===============================
                                                                    SQL = "select round(good_stock,4) as good_stock "
                                                                    SQL = SQL & "from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                                                    SQL = SQL & "and Kode_Stock_Owner = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "' "
                                                                    Using Ds2 = BindingTrans(SQL)
                                                                        If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                                                            If (Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("good_stock"))) - PackagingDigunakan) < BolehNegatif Then
                                                                                CloseTrans()
                                                                                CloseConn()
                                                                                MessageBox.Show("Proses akan membuat stock menjadi negatif untuk kode barang " & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                                Exit Sub
                                                                            Else
                                                                                SQL = "Update barang set good_stock = good_stock - " & PackagingDigunakan & " where "
                                                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                                                SQL = SQL & "kode_stock_owner = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner") & "' and "
                                                                                SQL = SQL & "kode_barang = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "'"
                                                                                ExecuteTrans(SQL)
                                                                            End If
                                                                        Else
                                                                            CloseTrans()
                                                                            CloseConn()
                                                                            MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                                                            Exit Sub
                                                                        End If
                                                                    End Using

                                                                    '===============================
                                                                    '=     CEK STOCK BARANG SN     =
                                                                    '===============================
                                                                    Dim StockKurang As Boolean = False
                                                                    SQL = "select isnull(round(sum(jumlah),4), 0) as stock from barang_sn where "
                                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                                    SQL = SQL & "kode_stock_owner = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner") & "' and "
                                                                    SQL = SQL & "kode_barang = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "' and jumlah <> 0 "
                                                                    Using Dr = OpenTrans(SQL)
                                                                        If Dr.Read Then
                                                                            If Dr("stock") < Val(PackagingDigunakan) Then
                                                                                StockKurang = True
                                                                            Else
                                                                                StockKurang = False
                                                                            End If
                                                                        Else
                                                                            Dr.Close()
                                                                            CloseTrans()
                                                                            CloseConn()
                                                                            MessageBox.Show("Terjadi Kesalahan Barang Sn untuk kode barang " & .Rows(j).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                            Exit Sub
                                                                        End If
                                                                    End Using

                                                                    '==================================
                                                                    '=     POTONG STOCK BARANG SN     =
                                                                    '==================================
                                                                    If Not StockKurang Then
                                                                        Dim sisa As Double = 0
                                                                        Dim JumlahPotong As Double = 0
                                                                        SQL = "select kode_stock_owner, kode_barang, serial_number, dbo.get_hpp(Serial_Number) as HPP, round(jumlah,4) as jumlah from barang_sn where "
                                                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                                        SQL = SQL & "kode_stock_owner = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner") & "' and "
                                                                        SQL = SQL & "kode_barang = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "' and jumlah <> 0 "
                                                                        SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                                                                        Using Ds2 = BindingTrans(SQL)
                                                                            If Ds2.Tables("Mytable").Rows.Count <> 0 Then
                                                                                sisa = Val(PackagingDigunakan)

                                                                                For l As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                                                                                    If sisa = 0 Then
                                                                                        Exit For
                                                                                    ElseIf sisa < 0 Then
                                                                                        CloseTrans()
                                                                                        CloseConn()
                                                                                        MessageBox.Show("Terdapat Kesalahan saat Potong Barang SN", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                                        Exit Sub
                                                                                    End If

                                                                                    Dim HppPackaging As Double = Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(l).Item("HPP")))

                                                                                    If sisa < Val(Ds2.Tables("MyTable").Rows(l).Item("jumlah")) Or sisa = Val(Ds2.Tables("MyTable").Rows(l).Item("jumlah")) Then
                                                                                        SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                                                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                                                        SQL = SQL & "kode_stock_owner = '" & Ds2.Tables("MyTable").Rows(l).Item("kode_stock_owner") & "' and "
                                                                                        SQL = SQL & "kode_barang = '" & Ds2.Tables("MyTable").Rows(l).Item("kode_barang") & "' and "
                                                                                        SQL = SQL & "serial_number = '" & Ds2.Tables("MyTable").Rows(l).Item("serial_number") & "'"
                                                                                        ExecuteTrans(SQL)

                                                                                        SQL = "INSERT INTO Emi_Production_Results_Validation_Packaging_Det(Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner, Kode_Barang,"
                                                                                        SQL = SQL & "Jumlah, Serial_Number, no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtNo_Transaksi.Text & "',"
                                                                                        SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(l).Item("kode_stock_owner") & "','" & Ds2.Tables("MyTable").Rows(l).Item("kode_barang") & "',"
                                                                                        SQL = SQL & "" & sisa & ",'" & Ds2.Tables("MyTable").Rows(l).Item("serial_number") & "', '" & NoUrut_PackagingDetail & "')"
                                                                                        ExecuteTrans(SQL)

                                                                                        Nilai_Packaging = Nilai_Packaging + (HppPackaging * sisa)
                                                                                        JumlahPotong += sisa
                                                                                        sisa = 0
                                                                                    ElseIf sisa > Val(Ds2.Tables("MyTable").Rows(l).Item("jumlah")) Then
                                                                                        SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                                                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                                                        SQL = SQL & "kode_stock_owner = '" & Ds2.Tables("MyTable").Rows(l).Item("kode_stock_owner") & "' and "
                                                                                        SQL = SQL & "kode_barang = '" & Ds2.Tables("MyTable").Rows(l).Item("kode_barang") & "' and "
                                                                                        SQL = SQL & "serial_number = '" & Ds2.Tables("MyTable").Rows(l).Item("serial_number") & "'"
                                                                                        ExecuteTrans(SQL)

                                                                                        SQL = "INSERT INTO Emi_Production_Results_Validation_Packaging_Det(Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner, Kode_Barang,"
                                                                                        SQL = SQL & "Jumlah, Serial_Number, no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtNo_Transaksi.Text & "',"
                                                                                        SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(l).Item("kode_stock_owner") & "','" & Ds2.Tables("MyTable").Rows(l).Item("kode_barang") & "',"
                                                                                        SQL = SQL & "" & Ds2.Tables("MyTable").Rows(l).Item("jumlah") & ",'" & Ds2.Tables("MyTable").Rows(l).Item("serial_number") & "', '" & NoUrut_PackagingDetail & "')"
                                                                                        ExecuteTrans(SQL)

                                                                                        Nilai_Packaging = Nilai_Packaging + (HppPackaging * Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(l).Item("jumlah"), "N4"))))
                                                                                        JumlahPotong += Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(l).Item("jumlah"), "N4")))
                                                                                        sisa = sisa - Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(l).Item("jumlah"), "N4")))
                                                                                    Else
                                                                                        CloseTrans()
                                                                                        CloseConn()
                                                                                        MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang " & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                                        Exit Sub
                                                                                    End If

                                                                                    If Math.Round(sisa, 4) <> 0 And l = Ds2.Tables("MyTable").Rows.Count - 1 Then
                                                                                        CloseTrans()
                                                                                        CloseConn()
                                                                                        MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                                        Exit Sub
                                                                                    End If
                                                                                Next
                                                                            End If
                                                                        End Using

                                                                        '================================================
                                                                        '=     CEK KESESUAIAN JUMLAH YANG DI POTONG     =
                                                                        '================================================
                                                                        If Val(HilangkanTanda(Format(JumlahPotong, "N4"))) <> Val(HilangkanTanda(Format(PackagingDigunakan, "N4"))) Then
                                                                            CloseTrans()
                                                                            CloseConn()
                                                                            MessageBox.Show("Terjadi Kesalahan Saat Memotong Stock Packaging!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                            Exit Sub
                                                                        End If
                                                                    Else
                                                                        CloseTrans()
                                                                        CloseConn()
                                                                        MessageBox.Show("Terjadi Kesalahan Pada Barang SN untuk kode barang " & .Rows(j).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                        Exit Sub

                                                                    End If

                                                                Next
                                                            End If
                                                        End Using

                                                    End If

#End Region


#Region "Bagian_Tambah"

                                                    '============================
                                                    '=     PENAMBAHAN STOCK     =
                                                    '============================


                                                    sumPackaging += Nilai_Packaging

                                                    ' GENEREATE SN BARU
                                                    Dim TotalhppLama As Double = (Get_Harga_SN(.Rows(j).Item("SN_Baru")) * JumlahKurang) / JumlahInsert


                                                    Dim hppSekarang As Double = Math.Round(TotalhppLama, 0)

                                                    If JumlahInsert = 0 Then
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Terjadi Kesalahan Pada Data!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If

                                                    Dim HppBaru As Double = hppSekarang + (Math.Round(Nilai_Packaging / JumlahInsert, 0))


                                                    'INI Untuk Bagi AKun Per Group Jenis, Sesuai Kode Barang akhir ny
                                                    Dim idgroup_jenis As String = ""
                                                    Dim Nmgroup_jenis As String = ""
                                                    Dim kode_akun As String = ""
                                                    SQL = "select a.id_group_jenis, Akun_Persediaan, Kode_Group_Jenis "
                                                    SQL = SQL & "from emi_group_jenis a, barang b, emi_group_jenis_akun c "
                                                    SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
                                                    SQL = SQL & "and a.id_group_jenis = b.id_group_jenis and b.kode_barang='" & kd_barang & "' "
                                                    SQL = SQL & "and a.id_group_jenis = c.id_group_jenis and b.kode_stock_owner='" & Lks_tujuan_Nomor & "' "
                                                    Using Dr = OpenTrans(SQL)
                                                        If Dr.Read Then
                                                            kode_akun = Dr("Akun_Persediaan")
                                                            idgroup_jenis = Dr("id_group_jenis")
                                                            Nmgroup_jenis = Dr("Kode_Group_Jenis")
                                                        Else
                                                            Dr.Close()
                                                            CloseTrans()
                                                            CloseConn()
                                                            MessageBox.Show("Barang detail jenis tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    End Using

                                                    Dim ada_data As Boolean = False
                                                    For ind = 0 To ArrHPP_Akun.Count - 1

                                                        If idgroup_jenis = ArrHPP_GroupJenisID.Item(ind) And Lks_tujuan_Nomor = ArrHPP_Lokasi.Item(ind) Then
                                                            ada_data = True

                                                            ArrHPP_Nilai.Item(ind) += (Math.Round(HppBaru * JumlahInsert, 0))
                                                        End If

                                                    Next

                                                    If ada_data = False Then
                                                        ArrHPP_GroupJenisID.Add(idgroup_jenis)
                                                        ArrHPP_GroupJenisNm.Add(Nmgroup_jenis)
                                                        ArrHPP_Lokasi.Add(Lks_tujuan_Nomor)
                                                        ArrHPP_Akun.Add(kode_akun)
                                                        ArrHPP_Nilai.Add(Math.Round(HppBaru * JumlahInsert, 0))
                                                    End If

                                                    Dim tgl_skg2 As DateTime
                                                    SQL = "declare @ab int; declare @ac int; select @ab = Selisih_Jam, @ac= expired_proforma from Init; "
                                                    SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss')  as Tanggal_Sekarang , @ac as expired"
                                                    Using dr = OpenTrans(SQL)
                                                        Do While dr.Read
                                                            tgl_skg2 = dr("Tanggal_Sekarang")

                                                        Loop
                                                    End Using

                                                    Dim Str As String = Format(random.Next(0, 999), "000") & Format(tgl_skg2, "HHmmss")
                                                    Dim Kode_Unik As String = Str.Substring(0, 5) & "BB" & Chr(64 + Str.Substring(6, 1)) & Str.Substring(6, Len(Str) - 6)
                                                    Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & HppBaru & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                                                    '===========================================
                                                    '=     GET WAREHOUSE DAN PALLET KOSONG     =
                                                    '===========================================
                                                    Dim available_Id_Warehouse As String = ""
                                                    Dim available_NoPallet As String = ""
                                                    SQL = "select top(1) a.id_wms_warehouse_position, 0 as nomor_urut from "
                                                    SQL = SQL & "view_warehouse_position a "
                                                    SQL = SQL & "where a.kode_Perusahaan ='" & KodePerusahaan & "' "
                                                    SQL = SQL & "and a.Kode_Stock_Owner='" & Lks_tujuan_Nomor & "' "
                                                    Using Dr2 = OpenTrans(SQL)
                                                        Do While Dr2.Read
                                                            available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                                                            available_NoPallet = Dr2("nomor_urut")
                                                        Loop
                                                    End Using

                                                    '============================
                                                    '=     INSERT BARANG SN     =
                                                    '============================

                                                    If Jenis_Nomor.ToUpper = "FINISHED GOOD" Then

                                                        ''Kalo Finished Good Data Harus Validasi Android
                                                        'SQL = "insert into barang_sn_sementara(kode_perusahaan, kode_stock_owner, kode_barang, "
                                                        'SQL = SQL & "serial_number, Jumlah, Jumlah_Bags, Warna, Kode_Unik_Berjalan, Kode_Unik_Asal, "
                                                        'SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet, Flag_Produksi, Flag_QI, Tgl_Produksi, Tgl_Expired, Tgl_masuk, Id_Jenis_Kategori_Produksi) values('" & KodePerusahaan & "', "
                                                        'SQL = SQL & "'" & Lks_tujuan_Nomor & "', '" & kd_barang & "', "
                                                        'SQL = SQL & "'" & SN_Baru & "', " & HilangkanTanda(JumlahInsert) & ", " & HilangkanTanda(JumlahInsert) & ", '" & KualitasBarang & "', "
                                                        'SQL = SQL & "'" & Kode_Berjalan & "', '" & .Rows(j).Item("Kode_Unik_Asal") & "-" & Kode_Berjalan & "', '" & newQrCode & "', "
                                                        'SQL = SQL & "'" & .Rows(j).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', 'Y', 'Y', "
                                                        'SQL = SQL & " '" & Tanggal_Produksi_Pertama & "', '" & Tanggal_Expired_Pertama & "', '" & Tanggal_Masuk_Pertama & "', '" & Id_Jenis_Kategori & "')"
                                                        'ExecuteTrans(SQL)

                                                        'Kalo Finished Good Data Harus Validasi Android
                                                        SQL = "insert into Barang_SN(kode_perusahaan, kode_stock_owner, kode_barang, "
                                                        SQL = SQL & "serial_number, Jumlah, Jumlah_Bags, Warna, Kode_Unik_Berjalan, Kode_Unik_Asal, "
                                                        SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet, Blok_SN, Flag_QI, Tgl_Produksi, Tgl_Expired, Tgl_masuk, Id_Jenis_Kategori_Produksi) values('" & KodePerusahaan & "', "
                                                        SQL = SQL & "'" & Lks_Awal & "', '" & kd_barang & "', "
                                                        SQL = SQL & "'" & SN_Baru & "', " & HilangkanTanda(JumlahInsert) & ", " & HilangkanTanda(JumlahInsert) & ", '" & KualitasBarang & "', "
                                                        SQL = SQL & "'" & Kode_Berjalan & "', '" & .Rows(j).Item("Kode_Unik_Asal") & "-" & Kode_Berjalan & "', '" & newQrCode & "', "
                                                        SQL = SQL & "'" & .Rows(j).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', 'Y', 'Y', "
                                                        SQL = SQL & " '" & Tanggal_Produksi_Pertama & "', '" & Tanggal_Expired_Pertama & "', '" & Tanggal_Masuk_Pertama & "', '" & Id_Jenis_Kategori & "')"
                                                        ExecuteTrans(SQL)

                                                        '=========================
                                                        '=     INSERT BARANG     =
                                                        '=========================
                                                        SQL = "Update barang set "
                                                        SQL = SQL & "Good_Stock = Good_Stock + " & HilangkanTanda(JumlahInsert) & " ,  Jumlah_Bags=Jumlah_Bags+" & HilangkanTanda(JumlahInsert) & " "
                                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_stock_owner = '" & Lks_Awal & "' and kode_barang = '" & kd_barang & "'"
                                                        ExecuteTrans(SQL)


                                                    Else

                                                        Dim jumlah_bags As Double = 0

                                                        If Jenis_Nomor.ToUpper = "REJECTED" Or Jenis_Nomor.ToUpper = "SAMPLE" Then
                                                            jumlah_bags = JumlahInsert
                                                        End If

                                                        SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah, Jumlah_Bags, Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, "
                                                        SQL = SQL & "Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN, Id_Jenis_Kategori_Produksi) "
                                                        SQL = SQL & "values('" & KodePerusahaan & "', '" & Lks_tujuan_Nomor & "', '" & kd_barang & "', '" & SN_Baru & "', "
                                                        SQL = SQL & "'" & HilangkanTanda(JumlahInsert) & "', '" & jumlah_bags & "', '" & Tanggal_Expired_Pertama & "', '" & Tanggal_Produksi_Pertama & "', 0, 0, "
                                                        SQL = SQL & "'" & available_Id_Warehouse & "', '" & newQrCode & "', '" & Kode_Berjalan & "', '" & .Rows(j).Item("Kode_Unik_Asal") & "-" & Kode_Berjalan & "', '" & available_NoPallet & "', "
                                                        SQL = SQL & "'" & .Rows(j).Item("Batch_Number") & "', '" & KualitasBarang & "', '" & Tanggal_Masuk_Pertama & "', NULL, '" & Id_Jenis_Kategori & "')"
                                                        ExecuteTrans(SQL)

                                                        '=========================
                                                        '=     INSERT BARANG     =
                                                        '=========================
                                                        SQL = "Update barang set "
                                                        SQL = SQL & "Good_Stock = Good_Stock + " & HilangkanTanda(JumlahInsert) & " ,  Jumlah_Bags=Jumlah_Bags+" & jumlah_bags & " "
                                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_stock_owner = '" & Lks_tujuan_Nomor & "' and kode_barang = '" & kd_barang & "'"
                                                        ExecuteTrans(SQL)

                                                    End If



                                                    '====================================
                                                    '=     CEK APAKAH JUMLAH SESUAI     =
                                                    '====================================
                                                    SQL = "SELECT "
                                                    SQL = SQL & "ROUND(SUM(good_stock), 4) AS good_stock, "
                                                    SQL = SQL & "ISNULL((SELECT ROUND(SUM(jumlah), 4) FROM Barang_sn x WHERE a.kode_Barang = x.kode_Barang AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.kode_Perusahaan = x.kode_Perusahaan), 0) AS Jumlah_sn, "
                                                    SQL = SQL & "ISNULL(ROUND(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
                                                    SQL = SQL & "ISNULL((SELECT ROUND(SUM(Jumlah_Bags), 4) FROM Barang_sn y WHERE a.kode_Barang = y.kode_Barang AND a.Kode_Stock_Owner = y.Kode_Stock_Owner AND a.kode_Perusahaan = y.Kode_Perusahaan), 0) AS jumlah_bags_sn "
                                                    SQL = SQL & "FROM "
                                                    SQL = SQL & "barang a "
                                                    SQL = SQL & "WHERE "
                                                    SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' And a.Kode_Stock_Owner = '" & Lks_tujuan_Nomor & "' AND a.Kode_Barang = '" & kd_barang & "' "
                                                    SQL = SQL & "GROUP BY "
                                                    SQL = SQL & "a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan"
                                                    Using Ds2 = BindingTrans(SQL)
                                                        If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                                            Dim Stock_Barang As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("good_stock"), "N4")))
                                                            Dim Stock_Sn As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn"), "N4")))
                                                            Dim Bags_Barang As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang"), "N4")))
                                                            Dim Bags_Sn As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn"), "N4")))

                                                            If Stock_Barang <> Stock_Sn Or Bags_Barang <> Bags_Sn Then
                                                                CloseTrans()
                                                                CloseConn()
                                                                MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                Exit Sub
                                                            End If
                                                        Else
                                                            CloseTrans()
                                                            CloseConn()
                                                            MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub

                                                        End If

                                                    End Using

                                                    '=========================
                                                    '=     UPDATE DETAIL     =
                                                    '=========================
                                                    SQL = "update Emi_Production_Results_Validation_Detail set Serial_Number_Tujuan = '" & SN_Baru & "' "
                                                    SQL = SQL & "where No_Transaksi = '" & TxtNo_Transaksi.Text.Trim & "' and Urut = '" & NoUrut_DetailResult & "'"
                                                    ExecuteTrans(SQL)


#End Region


                                                Next
                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Terjadi Kesalahan Pada Barang " & kd_barang & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End With
                                    End Using




                                    '================================================
                                    '=     CEK KESESUAIAN JUMLAH YANG DI POTONG     =
                                    '================================================
                                    If Val(HilangkanTanda(JumlahDipotong)) <> Val(HilangkanTanda(Jumlah_Pallet)) Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Memotong Stock Barang Produksi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    lokasi_awal = Lks_Awal
                                    arrNoSplitInput.Add(NoSplitt)

                                Next
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Tidak ada Data yang di simpan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using
#End Region


#Region "jurnal per nomor"
                        Dim inisial_faktur_dari As String = ""
                        Dim fso As String = ""
                        Dim Kode_Barang As String = ""
                        SQL = "Select b.Inisial_Faktur,a.Kode_Stock_Owner, kode_barang from Emi_Split_Production_Order a,Stock_Owner_Gudang b "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                        SQL = SQL & "And a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Top1NoSplit & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                inisial_faktur_dari = Dr("inisial_faktur")
                                fso = Dr("Kode_Stock_Owner")
                                Kode_Barang = Dr("kode_barang")
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        Dim akun_HPP_FG As String = ""
                        Dim Akun_Pembulatan_FG As String = ""

                        Dim keterangaN0 As String = ""
                        Dim keterangan3 As String = ""

                        SQL = "select HPP_Barang_Setengah_Jadi, Persediaan_Barang_Dalam_Proses, "
                        SQL = SQL & "HPP,Pembulatan_Finished_Good,Pembulatan_Semi_FG, Persediaan_Scrap from stock_owner_gudang "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then

                                akun_HPP_FG = Dr("HPP")
                                keterangaN0 = "HPP Barang Jadi "

                                Akun_Pembulatan_FG = Dr("Pembulatan_Finished_Good")
                                keterangan3 = "Pembulatan HPP Barang Jadi "
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        Dim Akun_Persediaan As String = ""
                        Dim keterangan As String = ""
                        SQL = "select c.akun_Persediaan, a.kode_group_jenis "
                        SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                        SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                        SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and b.kode_stock_owner = '" & lokasi_awal & "' and b.Kode_Barang='" & Kode_Barang & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Akun_Persediaan = Dr("akun_Persediaan")
                                keterangan = "Persediaan " & Dr("kode_group_jenis")
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        Dim ket_packaging As String = ""
                        Dim akun_kredit_packaging As String = ""
                        Dim lok_packaging As String = ""

                        SQL = "select top(1) "
                        SQL = SQL & "b.Id_Group_Jenis, b.kode_stock_owner, c.akun_persediaan, Kode_Group_Jenis "
                        SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a, Barang b, EMI_Group_Jenis_Akun c, "
                        SQL = SQL & "EMI_Group_Jenis d where "
                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                        SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                        SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Id_Group_Jenis = d.Id_Group_Jenis "
                        SQL = SQL & "and d.Kode_Perusahaan = c.Kode_Perusahaan and d.Id_Group_Jenis = c.Id_Group_Jenis "
                        SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner "
                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and a.no_faktur = '" & Top1NoSplit & "' "
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For h As Integer = 0 To .Rows.Count - 1

                                        lok_packaging = .Rows(h).Item("kode_stock_owner")
                                        akun_kredit_packaging = .Rows(h).Item("akun_persediaan")
                                        ket_packaging = "Persediaan " + .Rows(h).Item("Kode_Group_Jenis")

                                    Next
                                End If
                            End With
                        End Using

#Region "Jurnal 1"

                        sumPackaging = Math.Round(sumPackaging)
                        SumHPPAwal = Math.Round(SumHPPAwal)

                        'HPP Pada Barang Dalam Proses

                        Dim Kode_voucher As String = ""
                        Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                        Dim pagenumber As Integer = 1

                        SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                        SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                        SQL = SQL & "'" & Kode_voucher & "', "
                        SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                        SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                        SQL = SQL & "'" & KodeProyek & "', 'HPP Barang Jadi " & TxtNo_Transaksi.Text & "', '', "
                        SQL = SQL & "'-', '" & UserID & "')"
                        ExecuteTrans(SQL)

                        'Insert HPP Total
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_HPP_FG, 1),
                                 Strings.Mid(akun_HPP_FG, 2, 1),
                                 Strings.Mid(Ganti(akun_HPP_FG), 3),
                                 KodePerusahaan, KodeProyek, keterangaN0 & TxtNo_Transaksi.Text, SumHPPAwal + sumPackaging, "0", pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                        'Insert Data Bahan dan Packaging yg dipakai
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Persediaan, 1),
                                              Strings.Mid(Akun_Persediaan, 2, 1),
                                              Strings.Mid(Ganti(Akun_Persediaan), 3),
                                              KodePerusahaan, KodeProyek, keterangan & "; " & TxtNo_Transaksi.Text, "0", SumHPPAwal, pagenumber, lokasi_awal, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                        If sumPackaging <> 0 Then
                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_kredit_packaging, 1),
                                              Strings.Mid(akun_kredit_packaging, 2, 1),
                                              Strings.Mid(Ganti(akun_kredit_packaging), 3),
                                              KodePerusahaan, KodeProyek, ket_packaging & "; " & TxtNo_Transaksi.Text, "0", sumPackaging, pagenumber, fso, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1
                        End If

                        SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
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

#End Region

#Region "Jurnal 2"

                        'Persediaan Pada HPP

                        Dim Kode_voucher2 As String = ""
                        Kode_voucher2 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                        Dim pagenumber2 As Integer = 1

                        SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                        SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                        SQL = SQL & "'" & Kode_voucher2 & "', "
                        SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                        SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                        SQL = SQL & "'" & KodeProyek & "', 'Persediaan Barang Jadi " & TxtNo_Transaksi.Text & "', '', "
                        SQL = SQL & "'-', '" & UserID & "')"
                        ExecuteTrans(SQL)

                        'Insert HPP Total

                        Dim total_hpp As Double = 0
                        For index = 0 To ArrHPP_Akun.Count - 1

                            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(ArrHPP_Akun.Item(index), 1),
                                Strings.Mid(ArrHPP_Akun.Item(index), 2, 1),
                                Strings.Mid(Ganti(ArrHPP_Akun.Item(index)), 3),
                                KodePerusahaan, KodeProyek, "Persediaan " & ArrHPP_GroupJenisNm.Item(index) & "; " & TxtNo_Transaksi.Text, ArrHPP_Nilai(index), "0", pagenumber2, ArrHPP_Lokasi.Item(index), Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber2 = pagenumber2 + 1

                            total_hpp += ArrHPP_Nilai(index)
                        Next

                        'Insert Data Bahan dan Packaging yg dipakai
                        SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_HPP_FG, 1),
                                              Strings.Mid(akun_HPP_FG, 2, 1),
                                              Strings.Mid(Ganti(akun_HPP_FG), 3),
                                              KodePerusahaan, KodeProyek, "HPP Barang Jadi " & TxtNo_Transaksi.Text, "0", sumPackaging + SumHPPAwal, pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber2 = pagenumber2 + 1

                        Dim selisih_pembulatan As Double = total_hpp - (sumPackaging + SumHPPAwal)

                        If selisih_pembulatan > 0 Then
                            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(Akun_Pembulatan_FG, 1),
                                              Strings.Mid(Akun_Pembulatan_FG, 2, 1),
                                              Strings.Mid(Ganti(Akun_Pembulatan_FG), 3),
                                              KodePerusahaan, KodeProyek, "Selisih HPP Barang Jadi " & TxtNo_Transaksi.Text, "0", selisih_pembulatan, pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber2 = pagenumber2 + 1
                        Else
                            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(Akun_Pembulatan_FG, 1),
                                              Strings.Mid(Akun_Pembulatan_FG, 2, 1),
                                              Strings.Mid(Ganti(Akun_Pembulatan_FG), 3),
                                              KodePerusahaan, KodeProyek, "Selisih HPP Barang Jadi " & TxtNo_Transaksi.Text, Math.Abs(selisih_pembulatan), "0", pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber2 = pagenumber2 + 1
                        End If

                        SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher2 & "'"
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

                        'SQL = "update Emi_Production_Results_Validation_detail set Kode_Voucher1 ='" & Kode_voucher & "', Kode_Voucher2=' " & Kode_voucher2 & "', No_split='" & Txt_NoSplit.Text & "' "
                        SQL = "update Emi_Production_Results_Validation_detail set Kode_Voucher1 ='" & Kode_voucher & "', Kode_Voucher2=' " & Kode_voucher2 & "' "
                        SQL = SQL & "where no_transaksi='" & TxtNo_Transaksi.Text & "' and kode_perusahaan='" & KodePerusahaan & "' and nomor = '" & ID_Nomor & "' "
                        ExecuteTrans(SQL)

#End Region



#End Region

                    Next
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Tidak ada Data yang di simpan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            'Ini Cek klo ada data SPlit lain kemasuk!
            'SQL = "select c.Kode_perusahaan from "
            'SQL = SQL & "emi_production_results_validation_detail a, Emi_Production_Results_Detail_Pallet b, Emi_Production_Results c "
            'SQL = SQL & "where a.no_transaksi='" & TxtNo_Transaksi.Text.Trim & "' and a.kode_perusahaan='" & KodePerusahaan & "' and c.no_production_order<>'" & Txt_NoSplit.Text & "' and "
            'SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and a.serial_number_awal=b.sn_baru and "
            'SQL = SQL & "b.kode_perusahaan=c.kode_perusahaan and b.no_transaksi=c.no_transaksi"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Terjadi Kesalahan Pada data, Silahkan Ulangi Proses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using




            If MenuAsal <> "VALIDASI_GR_MERGE" Then

                'Ini Cek klo ada data SPlit lain kemasuk!
                SQL = "select c.Kode_perusahaan from "
                SQL = SQL & "emi_production_results_validation_detail a, Emi_Production_Results_Detail_Pallet b, Emi_Production_Results c "
                SQL = SQL & "where a.no_transaksi='" & TxtNo_Transaksi.Text.Trim & "' and a.kode_perusahaan='" & KodePerusahaan & "' and c.no_production_order<>'" & CurrentNoSplit & "' and "
                SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and a.serial_number_awal=b.sn_baru and "
                SQL = SQL & "b.kode_perusahaan=c.kode_perusahaan and b.no_transaksi=c.no_transaksi"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan Pada data, Silahkan Ulangi Proses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                '=========================
                '=     UPDATE PARENT     =
                '=========================

                SQL = $"select 1 from Emi_Production_Results_Validation where Kode_Perusahaan = '{KodePerusahaan}' and No_Transaksi = '{TxtNo_Transaksi.Text.Trim}' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        SQL = $"update Emi_Production_Results_Validation set No_Production_Order = '{CurrentNoSplit}' where Kode_Perusahaan = '{KodePerusahaan}' and No_Transaksi = '{TxtNo_Transaksi.Text.Trim}' "
                        ExecuteTrans(SQL)


                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Transaksi tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


            End If








            '=======================
            '=     UPDATE DATA     =
            '=======================
            For i As Integer = 0 To arrNoSplitInput.Count - 1
                Dim NoSplittt As String = arrNoSplitInput(i)

                SQL = "Select flag_hasil_Produksi_GR, b.no_transaksi from Emi_Split_Production_Order a, emi_production_results b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Production_Order "
                SQL = SQL & "And a.status Is null And b.status Is null And a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & NoSplittt & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dim no_transaksi As String = Dr("no_transaksi")

                        If General_Class.CekNULL(Dr("flag_hasil_Produksi_GR")) = "Y" Then
                            Dr.Close()
                            SQL = "select a.kode_perusahaan from EMI_Production_Results_Detail_Barang a, Emi_Production_Results_Detail_Pallet b, Barang_SN c where "
                            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Transaksi=b.No_Transaksi and a.status is null "
                            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.SN_Baru=c.Serial_Number and c.jumlah<>0 and a.no_transaksi='" & no_transaksi & "' "
                            Using dr2 = OpenTrans(SQL)
                                If Not dr2.Read Then


                                    dr2.Close()

                                    SQL = "update Emi_Split_Production_Order set Flag_Hasil_Produksi_GR2 = 'Y', UserID_Selesai_GR2 = '" & UserID & "', "
                                    SQL = SQL & "Tgl_Hasil_Produksi_GR2 = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Hasil_Produksi_GR2 = '" & Format(tgl_skg, "HH:mm:ss") & "'  "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & NoSplittt & "' "
                                    ExecuteTrans(SQL)

                                End If
                            End Using

                        End If

                    Else

                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Split tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            Next


            'If True Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Tahan")
            '    Exit Sub
            'End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        '=========================
        '=     CETAK BARCODE     =
        '=========================
        get_jam()

        Dim KdUnikPrint, KdUnikPrintScrap As New ArrayList

        Try
            OpenConn()
            Dim CrDoc As New Object

            Dim KertasBesar As String = "BarcodeFG"
            Dim KertasKecil As String = "BarcodeQC"

            Dim kode_unik_print As String = ""

            'HAPUS TABEL SEMENTARA
            SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_2 "
            ExecuteTrans(SQL)

            SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_2_Scrap "
            ExecuteTrans(SQL)


            'SQL = "with cte as( "
            ''SQL = SQL & "select b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            'SQL = SQL & "select b.Kode_Perusahaan, c.No_Split, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            'SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan,   "
            'SQL = SQL & "case when c.jenis = 'REJECTED' then 'Blocked ' else c.jenis end as Jenis, "
            'SQL = SQL & "c.nomor as Number, "

            'SQL = SQL & "e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing "

            'SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN e, Emi_Split_Production_Order f, EMI_Order_Produksi g, EMI_Master_Routing h "
            'SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            'SQL = SQL & "and b.kode_perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and g.Kode_Perusahaan = h.Kode_Perusahaan "
            'SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            'SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
            'SQL = SQL & "and b.No_Production_Order = f.No_Transaksi "
            'SQL = SQL & "and f.No_PO = g.No_Faktur "
            'SQL = SQL & "and g.Id_Routing = h.Id_Routing "
            'SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            ''SQL = SQL & "and b.No_Production_Order = '" & Txt_NoSplit.Text & "' "
            'SQL = SQL & "and b.No_Transaksi = '" & TxtNo_Transaksi.Text & "'  and c.Jenis<>'Finished Good' "

            'SQL = SQL & "union all "

            ''SQL = SQL & "select b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            'SQL = SQL & "select b.Kode_Perusahaan, c.No_Split, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
            'SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan,  "
            'SQL = SQL & "case when c.jenis = 'REJECTED' then 'Blocked ' else c.jenis end as Jenis, "
            'SQL = SQL & "c.nomor as Number, "

            'SQL = SQL & "e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing "

            'SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN e, Emi_Split_Production_Order f, EMI_Order_Produksi g, EMI_Master_Routing h "
            'SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            'SQL = SQL & "and b.kode_perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and g.Kode_Perusahaan = h.Kode_Perusahaan "
            'SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            'SQL = SQL & "and c.Kode_Stock_Owner_AWal = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
            'SQL = SQL & "and c.no_split = f.No_Transaksi "
            'SQL = SQL & "and f.No_PO = g.No_Faktur "
            'SQL = SQL & "and g.Id_Routing = h.Id_Routing "
            'SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            ''SQL = SQL & "and b.No_Production_Order = '" & Txt_NoSplit.Text & "' "
            'SQL = SQL & "and b.No_Transaksi = '" & TxtNo_Transaksi.Text & "'  and c.Jenis='Finished Good' "

            ''SQL = SQL & ") select kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number, Id_Routing, Routing "
            'SQL = SQL & ") select kode_perusahaan, No_Split, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number, Id_Routing, Routing "
            'SQL = SQL & "from cte "
            'SQL = SQL & "group by kode_perusahaan, No_Split, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, Satuan, Jenis, Number, Nomor, Id_Routing, Routing "




            SQL = $"
                with cte as(
	                select b.Kode_Perusahaan, c.No_Split,  c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c.Kode_Stock_Owner_Tujuan as Lokasi_Tujuan,
		                e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan, 
		                case when c.jenis = 'REJECTED' then 'Blocked ' else c.jenis end as Jenis,
		                c.nomor as Number,
		                e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing
	                from Emi_Production_Results_Validation b
		                inner join Emi_Production_Results_Validation_Detail c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Transaksi = c.No_Transaksi
		                inner join Barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner_Awal = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang
		                inner join Barang_SN e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Stock_Owner_Awal = e.Kode_Stock_Owner and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan = e.Serial_Number
		                inner join Emi_Split_Production_Order f on c.Kode_Perusahaan = f.Kode_Perusahaan and c.No_Split = f.No_Transaksi and f.Status is null
		                inner join EMI_Order_Produksi g on f.Kode_Perusahaan = g.kode_perusahaan and f.No_PO = g.no_faktur and g.status is null
		                inner join EMI_Master_Routing h on g.kode_perusahaan = h.kode_perusahaan and g.Id_Routing = h.Id_Routing
	                where b.Kode_Perusahaan = '{KodePerusahaan}'
	                and b.No_Transaksi = '{TxtNo_Transaksi.Text}'
	
	                union all
	
	                select b.Kode_Perusahaan, c.No_Split,  c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c.Kode_Stock_Owner_Tujuan as Lokasi_Tujuan,
		                e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan, 
		                case when c.jenis = 'REJECTED' then 'Blocked ' else c.jenis end as Jenis,
		                c.nomor as Number,
		                e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing
	                from Emi_Production_Results_Validation b
		                inner join Emi_Production_Results_Validation_Detail c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Transaksi = c.No_Transaksi
		                inner join Barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner_Awal = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang
		                inner join Barang_SN e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan = e.Serial_Number
		                inner join Emi_Split_Production_Order f on c.Kode_Perusahaan = f.Kode_Perusahaan and c.No_Split = f.No_Transaksi and f.Status is null
		                inner join EMI_Order_Produksi g on f.Kode_Perusahaan = g.kode_perusahaan and f.No_PO = g.no_faktur and g.status is null
		                inner join EMI_Master_Routing h on g.kode_perusahaan = h.kode_perusahaan and g.Id_Routing = h.Id_Routing
	                where b.Kode_Perusahaan = '{KodePerusahaan}'
	                and b.No_Transaksi = '{TxtNo_Transaksi.Text}'
                ),Cte_Group AS (
                    select kode_perusahaan, No_Split, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number, Id_Routing, Routing
                    from cte
                    group by kode_perusahaan, No_Split, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, Satuan, Jenis, Number, Id_Routing, Routing
                )
                select kode_perusahaan, 
                    STRING_AGG(No_Split, ', ') AS No_Split,
                    Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number, Id_Routing, Routing
                from Cte_Group
                group by kode_perusahaan, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, Satuan, Jenis, Number, Id_Routing, Routing
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1


                            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

                            Dim fullNewQrScrap As String = .Rows(i).Item("Qr_Code") & "-" & .Rows(i).Item("Kode_Unik_Berjalan")

                            Barcode.Image = Nothing

                            Barcode.Image = Generate_QR_NoPadding(fullNewQrScrap)

                            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")

                            '   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

                            'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
                            Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
                            'End If

                            fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                            FileSize1 = fs1.Length
                            rawData1 = New Byte(FileSize1) {}
                            fs1.Read(rawData1, 0, FileSize1)
                            fs1.Close()
                            Cmd.Parameters.Add("@newBarcode" & kode_unik_print, SqlDbType.Image).Value = rawData1

                            Dim asdada As String = .Rows(i).Item("Jenis").ToString.ToUpper

                            If .Rows(i).Item("Jenis").ToString.ToUpper.Trim = "FINISHED GOOOD" Then

                                SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2 (Kode_Perusahaan, No_Split, Kode_Barang, Barcode, Nama_Barang, Batch_Number, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, Tgl_Expired, Jam_Expired, Jumlah, Satuan, Jenis, Number, Kode_Unik_Print) "
                                SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("No_Split") & "', '" & .Rows(i).Item("Kode_Barang") & "', @newBarcode" & kode_unik_print & ", "
                                SQL = SQL & "'" & .Rows(i).Item("Nama_Barang") & "', '" & .Rows(i).Item("Batch_Number") & "', '" & fullNewQrScrap & "', '" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Tgl_Produksi") & "', "
                                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Tgl_Expired") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Jenis") & "', '" & .Rows(i).Item("Number") & "', '" & kode_unik_print & "') "
                                ExecuteTrans(SQL)

                                KdUnikPrint.Add(kode_unik_print)

                            ElseIf .Rows(i).Item("Jenis").ToString.ToUpper.Trim = "DISQUALIFIED" Then
                                SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2 (Kode_Perusahaan, No_Split, Kode_Barang, Barcode, Nama_Barang, Batch_Number, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, Tgl_Expired, Jam_Expired, Jumlah, Satuan, Jenis, Number, Kode_Unik_Print) "
                                SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("No_Split") & "', '" & .Rows(i).Item("Kode_Barang") & "', @newBarcode" & kode_unik_print & ", "
                                SQL = SQL & "'" & .Rows(i).Item("Nama_Barang") & "', '" & .Rows(i).Item("Batch_Number") & "', '" & fullNewQrScrap & "', '" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Tgl_Produksi") & "', "
                                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Tgl_Expired") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Jenis") & "', '" & .Rows(i).Item("Number") & "', '" & kode_unik_print & "') "
                                ExecuteTrans(SQL)

                                KdUnikPrint.Add(kode_unik_print)

                            Else



                                SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2_Scrap (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
                                SQL = SQL & "Proses, Jumlah, Satuan, Nomor, id_routing, routing, Kode_unik_print)  "
                                SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("No_Split") & "', @newBarcode" & kode_unik_print & ", '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', '" & fullNewQrScrap & "', '" & .Rows(i).Item("Qr_Code") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Tgl_Produksi") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'X', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Number") & "', '" & .Rows(i).Item("Id_Routing") & "', '" & .Rows(i).Item("Routing") & "', '" & kode_unik_print & "') "
                                ExecuteTrans(SQL)

                                KdUnikPrintScrap.Add(kode_unik_print)
                            End If



                        Next
                    End If
                End With
            End Using


            For i As Integer = 0 To KdUnikPrint.Count - 1

                SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2 where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & KdUnikPrint(i) & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        '==========================
                        '=     BARCODEE BESAR     =
                        '==========================
                        Dim printerDitemukan As Boolean = False
                        For Each printer As String In PrinterSettings.InstalledPrinters
                            If printer.ToLower() = PrinterBarcode.ToLower() Then
                                printerDitemukan = True
                                Exit For
                            End If
                        Next

                        If printerDitemukan Then

                            CrDoc = New N_EMI_Label_Barcode_GR_2

                            'With A_Place_For_Printing2
                            '    CrDoc.SetDataSource(Ds)
                            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            '    CrDoc.PrintOptions.PrinterName = ""
                            '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & KdUnikPrint(i) & "' "
                            '    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2"
                            '    .Text = "Label Good Received 2"
                            '    .CrystalReportViewer1.ReportSource = CrDoc
                            '    .Refresh()
                            '    .Show()
                            'End With

                            '=====================================================

                            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & KdUnikPrint(i) & "' "
                            CrDoc.PrintOptions.PrinterName = PrinterBarcode

                            doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                            Dim rawKind As Integer
                            Dim foundPaper As Boolean = False
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasBesar Then
                                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                    CrDoc.PrintOptions.PaperSize = rawKind
                                    foundPaper = True
                                    Exit For
                                End If
                            Next

                            If Not foundPaper Then
                                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                                MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            End If

                            CrDoc.PrintToPrinter(1, False, 1, 2500)

                        Else
                            MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                        printerDitemukan = False


                    Else
                        MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End If


                End Using



            Next


            For i As Integer = 0 To KdUnikPrintScrap.Count - 1

                SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2_Scrap where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & KdUnikPrintScrap(i) & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        '==========================
                        '=     BARCODEE BESAR     =
                        '==========================
                        Dim printerDitemukan As Boolean = False
                        For Each printer As String In PrinterSettings.InstalledPrinters
                            If printer.ToLower() = PrinterBarcode.ToLower() Then
                                printerDitemukan = True
                                Exit For
                            End If
                        Next

                        If printerDitemukan Then

                            CrDoc = New N_EMI_Label_Barcode_GR_2_Scrap

                            'With A_Place_For_Printing2
                            '    CrDoc.SetDataSource(Ds)
                            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            '    CrDoc.PrintOptions.PrinterName = ""
                            '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Unik_Print} = '" & KdUnikPrintScrap(i) & "' "
                            '    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2 Scrap"
                            '    .Text = "Label Good Received 2"
                            '    .CrystalReportViewer1.ReportSource = CrDoc
                            '    .Refresh()
                            '    .Show()
                            'End With

                            '=====================================================

                            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Unik_Print} = '" & KdUnikPrintScrap(i) & "' "
                            CrDoc.PrintOptions.PrinterName = PrinterBarcode

                            doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                            Dim rawKind As Integer
                            Dim foundPaper As Boolean = False
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasBesar Then
                                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                    CrDoc.PrintOptions.PaperSize = rawKind
                                    foundPaper = True
                                    Exit For
                                End If
                            Next

                            If Not foundPaper Then
                                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                                MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            End If


                            CrDoc.PrintToPrinter(1, False, 1, 2500)

                        Else
                            MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                        printerDitemukan = False


                    Else
                        MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End If


                End Using



            Next



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        Kosong()




    End Sub

    Private Sub Hitung_Data()
        If Lv_Data.Items.Count = 0 Then Exit Sub

        Dim TotFG As Double = 0
        Dim TotKG As Double = 0
        For i As Integer = 0 To Lv_Data.Items.Count - 1
            Get_Lv_Data_GR(i)

            TotFG += LvData_Jumlah
            TotKG += LvData_Berat
        Next

        Txt_TotFG.Text = Format(TotFG, "N0")
        Txt_TotBeratKG.Text = Format(TotKG, "N4")

    End Sub


    Public Sub LoadFromSD()
        If Cmb_Barcode.SelectedIndex = 1 Then
            If arrBarcodeFromSD.Count = 0 Then
                MessageBox.Show("Tidak Ada Data yang Dipilih", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If

        Try
            OpenConn()

            Dim NoSplit As String = SelectedCurrentSplit

            Lv_DataPallet.Items.Clear()
            For i As Integer = 0 To arrBarcodeFromSD.Count - 1

                Dim DataDic As Dictionary(Of String, String) = arrBarcodeFromSD.Item(i)

                'Dim NoSplit As String = Txt_NoSplit.Text.Trim


                SQL = "select b.No_Production_Order as No_Split, a.Lokasi_Gudang as Kode_Stock_Owner, a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Batch_Number,  "
                SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama as Nama_Barang, "
                'SQL = SQL & "sum(f.Jumlah) as Jumlah, "
                SQL = SQL & "isnull((sum(f.Jumlah) -  "
                SQL = SQL & "(select isnull(sum(jumlah), 0) from N_EMI_Validation_GR_Temp z "
                SQL = SQL & "where b.kode_perusahaan = z.kode_perusahaan "
                SQL = SQL & "and b.no_production_order = z.no_production_order "
                SQL = SQL & "and z.barcode = (a.Qr_Code + '-' + a.Kode_Unik_Berjalan)) ), 0) as Jumlah, "
                SQL = SQL & "a.Satuan, a.Jenis, e.Keterangan as Kualitas, isnull(a.nomor,0) as nomor, a.Tahap, "

                'SQL = SQL & "case "
                'SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = b.No_Production_Order and z.No_Batch = a.Tahap), 'U') = 'T' "
                'SQL = SQL & "then 'DITOLAK' "
                'SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = b.No_Production_Order and z.No_Batch = a.Tahap), 'U') = 'Y' "
                'SQL = SQL & "then 'DITERIMA' "
                'SQL = SQL & "when isnull(( select top 1 z.Flag_Ready_For_Packaging from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = b.No_Production_Order "
                'SQL = SQL & "and z.No_Batch = a.Tahap and z.Flag_Ready_For_Packaging = 'Y' and z.No_GR = '1' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' "
                'SQL = SQL & "then 'READY FOR PACKING' "
                'SQL = SQL & "when isnull(( select top 1 z.Kode_Perusahaan from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = b.No_Production_Order "
                'SQL = SQL & "and z.No_Batch = a.Tahap and z.No_GR = '1' and z.flag_military_sampling='Y' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' "
                'SQL = SQL & "then 'HOLD' "
                'SQL = SQL & "else 'NO DATA' "
                'SQL = SQL & "end as Status_Split "

                SQL = SQL & "case "
                SQL = SQL & "when isnull(( select top 1 'Y' from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = b.No_Production_Order "
                SQL = SQL & "and z.No_Batch = a.Tahap and z.Flag_Ready_For_Packaging = 'Y' and z.No_GR = '1' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' "
                SQL = SQL & "then 'READY FOR PACKING' "
                SQL = SQL & "when isnull(( select top 1 'Y' from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = b.No_Production_Order "
                SQL = SQL & "and z.No_Batch = a.Tahap and z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging is null and z.No_GR = '1' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' "
                SQL = SQL & "then 'HOLD' "
                SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = b.No_Production_Order and z.No_Batch = a.Tahap), 'U') = 'T' "
                SQL = SQL & "then 'DITOLAK' "
                SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = b.No_Production_Order and z.No_Batch = a.Tahap), 'U') = 'Y' "
                SQL = SQL & "then 'DITERIMA' "
                SQL = SQL & "else 'NO DATA' "
                SQL = SQL & "end as Status_Split, isnull(h.flag_commercial, 'T') as Flag_Commercial "

                SQL = SQL & "from Emi_Production_Results_Detail_Pallet a, Emi_Production_Results b, EMI_Production_Results_Detail_Barang c, barang d, EMI_Master_Warna e, Barang_SN f, "
                SQL = SQL & "emi_split_production_order g, emi_order_produksi h "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Perusahaan = c.Kode_Perusahaan And c.Kode_Perusahaan = d.Kode_Perusahaan And a.Kode_Perusahaan = e.Kode_Perusahaan "
                SQL = SQL & "And a.No_Transaksi = b.No_Transaksi "
                SQL = SQL & "And a.No_Transaksi = c.No_Transaksi And a.Proses = c.Proses "
                SQL = SQL & "And c.Kode_Stock_Owner = d.Kode_Stock_Owner And c.Kode_Barang = d.Kode_Barang "
                SQL = SQL & "And a.Jenis = e.Kode_Warna "
                SQL = SQL & "And a.SN_Baru = f.Serial_Number "
                SQL = SQL & "And b.Status Is null and "
                SQL = SQL & "b.kode_perusahaan =g.kode_perusahaan and b.no_production_order=g.no_transaksi and g.status is null and "
                SQL = SQL & "g.kode_perusahaan =h.kode_perusahaan and g.no_po=h.no_faktur and h.status is null "
                SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) ='" & Txt_ScanBarcode.Text.Trim & "' "

                If Fitur_Military_Sampling Then

                    SQL = SQL & "and (b.No_Production_Order in ( "
                    SQL = SQL & "select distinct z.no_Split from N_EMI_Military_Sampling z "
                    SQL = SQL & "where z.kode_perusahaan = a.Kode_Perusahaan and z.status is null "
                    SQL = SQL & "and z.No_Split = b.No_Production_Order and z.No_Batch = a.tahap "
                    SQL = SQL & "and z.No_GR = '1' and z.flag_military_sampling='Y') or isnull(h.flag_commercial, 'T')='T' ) "

                End If

                SQL = SQL & "and a.Qr_Code = '" & DataDic("QrCode") & "' and a.Kode_Unik_Berjalan = '" & DataDic("KdUnikBerjalan") & "' "
                SQL = SQL & "group by a.kode_perusahaan, b.No_Production_Order, a.Lokasi_Gudang , a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) , a.Batch_Number, "
                SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama, a.Satuan, a.Jenis, e.Keterangan, a.nomor, b.kode_perusahaan, a.Tahap, h.Flag_Commercial "
                SQL = SQL & "order by a.Batch_Number, nomor, b.No_Production_Order, a.Lokasi_Gudang, a.nomor, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        If NoSplit = "" Then
                            SelectedCurrentSplit = Dr("No_Split")
                        Else

                            If NoSplit <> Dr("No_Split") Then
                                isCombine = True
                                If MenuAsal <> "VALIDASI_GR_MERGE" Then
                                    Dr.Close()
                                    CloseConn()
                                    MessageBox.Show("No Split Tidak Boleh Berbeda", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End If

                        Dim AvailableData As Boolean = False
                        For j As Integer = 0 To Lv_DataPallet.Items.Count - 1
                            Get_LvPallet_Data(j)

                            If LvPallet_Barcode.ToUpper = Dr("Barcode").ToString.ToUpper Then
                                AvailableData = True
                                Exit For
                            End If
                        Next

                        If AvailableData Then
                            Dr.Close()
                            Continue For
                        End If

                        Dim Lv As ListViewItem
                        Lv = Lv_DataPallet.Items.Add(Dr("Kode_Stock_Owner"))
                        Lv.SubItems.Add(Dr("Barcode"))
                        Lv.SubItems.Add(Dr("Batch_Number"))
                        Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
                        Lv.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
                        Lv.SubItems.Add(Dr("kode_barang"))
                        Lv.SubItems.Add(Dr("Nama_Barang"))
                        Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                        Lv.SubItems.Add(Dr("Satuan"))
                        Lv.SubItems.Add(Dr("Kualitas"))
                        Lv.SubItems.Add(Dr("Jenis"))
                        Lv.SubItems.Add("X")
                        Lv.SubItems.Add(Dr("Qr_Code"))
                        Lv.SubItems.Add(Dr("Kode_Unik_Berjalan"))
                        Lv.SubItems.Add(Dr("nomor"))
                        Lv.SubItems.Add(Dr("Tahap"))
                        Lv.SubItems.Add(Dr("No_Split"))

                        'Txt_NoSplit.Text = Dr("No_Split")
                        Txt_ScanBarcode.Text = ""
                        SelectedVariant = Dr("kode_barang")

                        If Fitur_Military_Sampling Then

                            If Dr("Flag_Commercial") = "Y" Then
                                If General_Class.CekNULL(Dr("Status_Split")).ToUpper = "DITOLAK" Then
                                    Lv.BackColor = Color.DarkRed
                                    Lv.ForeColor = Color.White
                                ElseIf General_Class.CekNULL(Dr("Status_Split")).ToUpper = "DITERIMA" Then
                                    Lv.BackColor = Color.LightGreen
                                ElseIf General_Class.CekNULL(Dr("Status_Split")).ToUpper = "READY FOR PACKING" Then
                                    Lv.BackColor = Color.LightGreen
                                ElseIf General_Class.CekNULL(Dr("Status_Split")).ToUpper = "HOLD" Then
                                    Lv.BackColor = Color.LightYellow
                                Else
                                    Lv.BackColor = Color.White
                                End If
                            Else
                                Lv.BackColor = Color.LightYellow
                            End If

                        End If



                    Else
                        'Txt_NoSplit.Text = ""
                        SelectedVariant = ""
                        Txt_ScanBarcode.Text = ""
                    End If
                End Using

            Next

            'If Not Txt_NoSplit.Text = "" Then

            '    SQL = "select top 1 b.Nomor "
            '    SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Split_Production_Order c "
            '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = b.kode_perusahaan "
            '    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and a.no_production_order = c.No_Transaksi "
            '    SQL = SQL & "and a.Status is null and c.Status is null "
            '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            '    SQL = SQL & "and a.No_Production_Order = '" & Txt_NoSplit.Text & "' "
            '    SQL = SQL & "order by b.nomor DESC "
            '    Using Dr = OpenTrans(SQL)
            '        If Dr.Read Then
            '            If General_Class.CekNULL(Dr("Nomor")) = "" Then
            '                Txt_JmlhKeranjang.Text = 1
            '            Else
            '                Txt_JmlhKeranjang.Text = Val(Dr("Nomor")) + 1
            '            End If
            '        Else
            '            Txt_JmlhKeranjang.Text = 1
            '        End If
            '    End Using

            'Else
            '    Txt_JmlhKeranjang.Text = ""
            'End If

            If Not isCombine Then
                SQL = "select top 1 b.Nomor "
                SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Split_Production_Order c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = b.kode_perusahaan "
                SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and a.no_production_order = c.No_Transaksi "
                SQL = SQL & "and a.Status is null and c.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Production_Order = '" & NoSplit & "' "
                SQL = SQL & "order by b.nomor DESC "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("Nomor")) = "" Then
                            Txt_JmlhKeranjang.Text = 1
                        Else
                            Txt_JmlhKeranjang.Text = Val(Dr("Nomor")) + 1
                        End If
                    Else
                        Txt_JmlhKeranjang.Text = 1
                    End If
                End Using
            Else
                Txt_JmlhKeranjang.Text = ""

            End If



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub DetailPackagingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailPackagingToolStripMenuItem.Click
        If Lv_Data.FocusedItem Is Nothing OrElse Lv_Data.FocusedItem.Index = -1 Then Exit Sub

        Dim SelectedIndex As Integer = Lv_Data.FocusedItem.Index

        Get_Lv_Data_GR(SelectedIndex)

        'If LvData_Jenis.ToUpper = "FINISHED GOOD" Then
        '    SD_ValidasiGR_Detail_Packaging.No_Split = Txt_NoSplit.Text
        '    SD_ValidasiGR_Detail_Packaging.Kd_Barang = LvData_KdBarang
        '    SD_ValidasiGR_Detail_Packaging.JumlahInput = LvData_Jumlah

        '    SD_ValidasiGR_Detail_Packaging.ShowDialog()

        'End If

    End Sub

    Private Sub ButtoN0_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Kosong()
    End Sub

    '==================================================================================================================================================================================
    '=     HANDLE KEY PRESS
    '==================================================================================================================================================================================
    Private Sub Txt_ScanBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ScanBarcode.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_ScanBarcode.Text.Trim.Length <> 0 Then
                Btn_Scan_Click(Me, Nothing)
            End If
        End If
    End Sub

    Private Sub Txt_Jumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Jumlah.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "."c Or e.KeyChar = ControlChars.Back) Then
            e.Handled = True
        End If
        If e.KeyChar = Chr(13) Then Btn_Tambah.Focus()
    End Sub
    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        Dim cm As ContextMenuStrip = CType(sender, ContextMenuStrip)
        Dim lv As ListView = CType(cm.SourceControl, ListView)

        If lv IsNot Nothing Then
            If lv Is Lv_Data Then
                If Lv_Data.Items.Count = 0 Then
                    e.Cancel = True
                    Exit Sub
                End If

            ElseIf lv Is Lv_DataPallet Then
                If Lv_DataPallet.Items.Count = 0 Then
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip2_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip2.Opening
        If LvBarcode.Items.Count = 0 Or LvBarcode.FocusedItem Is Nothing Then
            e.Cancel = True : Exit Sub
        End If
    End Sub



    Private Sub HapusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem.Click
        Dim cm As ContextMenuStrip = CType(sender, ToolStripMenuItem).Owner
        Dim lv As ListView = CType(cm.SourceControl, ListView)

        If lv IsNot Nothing Then
            If lv Is Lv_Data Then
                If Lv_Data.FocusedItem Is Nothing OrElse Lv_Data.FocusedItem.Index = -1 Then Exit Sub

                If Lv_Data.SelectedItems.Count > 0 Then
                    Lv_Data.Items.Remove(Lv_Data.FocusedItem)
                End If

                If LvBarcode.Items.Count = 0 Then
                    If Lv_Data.Items.Count = 0 Then
                        CurrentBatch = ""
                    End If
                End If

                Dim NoSplit As String = Lv_Data.FocusedItem.SubItems(itemData_NoSplit).Text

                Hitung_Data()
                If Fitur_Military_Sampling Then
                    CekMilitarySampling(NoSplit)
                End If
            ElseIf lv Is Lv_DataPallet Then
                If Lv_DataPallet.SelectedItems.Count > 0 Then

                    For i As Integer = Lv_Data.Items.Count - 1 To 0 Step -1
                        If Lv_Data.Items(i).SubItems(0).Text = Lv_DataPallet.FocusedItem.SubItems(itemPallet_Barcode).Text Then
                            Lv_Data.Items.RemoveAt(i)
                        End If
                    Next
                    Lv_DataPallet.Items.Remove(Lv_DataPallet.FocusedItem)
                End If

                If Lv_DataPallet.Items.Count = 0 Then
                    'Txt_NoSplit.Text = ""

                End If
            End If
        End If



        'If Txt_NoSplit.Text = "" Then
        '    Txt_JmlhKeranjang.Text = ""
        'End If



    End Sub

    Private Sub DetailPackagingToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DetailPackagingToolStripMenuItem1.Click
        If LvBarcode.FocusedItem Is Nothing OrElse LvBarcode.FocusedItem.Index = -1 Then Exit Sub

        Dim SelectedIndex As Integer = LvBarcode.FocusedItem.Index

        Get_Lv_Data_Barcode(SelectedIndex)

        Dim KdBarang As String = ""
        Try
            OpenConn()

            SQL = "select distinct top 1 b.Kode_Barang, b.Nama, b.Berat "
            SQL = SQL & "from Barang_SN a, barang b, N_EMI_Validation_GR_Temp c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and (qr_code +'-'+Kode_Unik_Berjalan ) = c.Barcode "
            SQL = SQL & "and c.Nomor = '" & LvBarcode_ID & "' and c.userid='" & UserID & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    KdBarang = Dr("Kode_Barang")
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If LvBarcode_Jenis.ToUpper = "FINISHED GOOD" Then

            'SD_ValidasiGR_Detail_Packaging.No_Split = LvBarcode_No_Split
            SD_ValidasiGR_Detail_Packaging.Kd_Barang = KdBarang
            SD_ValidasiGR_Detail_Packaging.JumlahInput = LvBarcode_Total

            SD_ValidasiGR_Detail_Packaging.ShowDialog()

        End If
    End Sub


    Private Sub TxtKeterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKeterangan.KeyPress, Txt_JmlhKeranjang.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Barcode.DroppedDown = True
            Cmb_Barcode.Focus()
        End If
    End Sub
    Private Sub Cmb_Barcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Barcode.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Barcode.SelectedIndex = 0 Then
                Txt_ScanBarcode.Focus()
            Else
                Btn_Scan.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Jenis_Kategori.DroppedDown = True
            Cmb_Jenis_Kategori.Focus()
        End If
    End Sub

    Private Sub Cmb_LokasiTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_LokasiTujuan.KeyPress
        If e.KeyChar = Chr(13) Then
            Btn_Simpan_Barcode.Focus()
        End If
    End Sub



    Private Sub Cmb_Jenis_Kategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_Kategori.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_LokasiTujuan.DroppedDown = True
            Cmb_LokasiTujuan.Focus()
        End If
    End Sub



    Private Sub EMI_Validasi_GR_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            OpenConn()

            SQL = "delete N_EMI_Validation_GR_Temp where kode_perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
            ExecuteTrans(SQL)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Function Generate_QR_NoPadding(ByVal isi As String)

        Dim options As New ZXing.QrCode.QrCodeEncodingOptions()

        options.DisableECI = True
        options.CharacterSet = "UTF-8"
        options.Width = 80
        options.Height = 80
        options.Margin = 0

        Dim qr As New ZXing.BarcodeWriter()
        qr.Format = ZXing.BarcodeFormat.QR_CODE
        qr.Options = options

        Dim result As New Bitmap(qr.Write(isi))
        Return result
    End Function

    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub


End Class

