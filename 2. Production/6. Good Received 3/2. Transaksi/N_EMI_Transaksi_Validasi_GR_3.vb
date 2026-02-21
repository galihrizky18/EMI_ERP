Imports System.IO

Public Class N_EMI_Transaksi_Validasi_GR_3

    Dim JudulForm As String = "Validasi Penerimaan Barang"
    Dim arrLokasiAsal, arrLokasiTujuan, arrSO As New ArrayList
    Dim arrKdBarangSrap, arrNamaScrap, arrSatuanScrap, arrSatuanKecilScrap As New ArrayList

    Private random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Dim Kode_Unik_Transaksi As String = ""

    'Public Property SelectedVariant As String = ""
    Dim isCombine As Boolean = False
    Dim NoSplitTemp As String = ""


    Public arrBarcodeFromSD As New List(Of Dictionary(Of String, String))

    Dim ReadyForPackaging As Boolean = False

    Dim Prefix, Tahun_MulaiProduksi As String

    Dim LvPallet_KdSO, LvPallet_Barcode, LvPallet_BatchNumber, LvPallet_TglProduksi, LvPallet_TglExpired, LvPallet_KdBarang, LvPallet_NmBarang, LvPallet_Jumlah As String
    Dim LvPallet_Satuan, LvPallet_Kualitas, LvPallet_Warna, LvPallet_ID, LvPallet_QR, LvPallet_KdUnikBerjalan, LvPallet_NoTransGr2 As String

    Dim LvData_KdSoAwal, LvData_KdSoTujuan, LvData_Barcode, LvData_BatchNumber, LvData_KdBarang, LvData_NmBarang, LvData_Jenis, LvData_Jumlah, LvData_Satuan, LvData_ID As String
    Dim LvData_QR, LvData_KdUnikBerjalan, LvData_TglExpired, LvData_KdBarangScrap, LvData_NoTransGR2, LvData_NoFaktur As String

    Dim LvBarcode_NoTransksi, LvBarcode_NoTransaksiGR2, LvBarcode_Barcode, LvBarcode_Jenis, LvBarcode_Lokasi_Awal, LvBarcode_Lokasi_Tujuan, LvBarcode_Jumlah, LvBarcode_User, LvBarcode_QrCode, LvBarcode_KdUnikBerjalan As String


    Dim LvBarcodeDet_Barcode, LvBarcodeDet_NomorLama, LvBarcodeDet_Jumlah As String


    Dim CurrentBatch As String = ""


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
    Dim itemPallet_NoTransGR2 As Integer = 14

    Dim itemData_KdSoAwal As Integer = 0
    'Dim itemData_KdSoTujuan As Integer = 1
    Dim itemData_Barcode As Integer = 1
    Dim itemData_BatchNumber As Integer = 2
    Dim itemData_KdBarang As Integer = 3
    Dim itemData_NmBarang As Integer = 4
    'Dim itemData_Jenis As Integer = 5
    Dim itemData_Jumlah As Integer = 5
    Dim itemData_Satuan As Integer = 6
    Dim itemData_ID As Integer = 7
    Dim itemData_QR As Integer = 8
    Dim itemData_KdUnikBerjalan As Integer = 9
    Dim itemData_TglExpired As Integer = 10
    'Dim itemData_KdBarangScrap As Integer = 11
    Dim itemData_NoTransGR2 As Integer = 11
    Dim itemData_NoFaktur As Integer = 12


    Dim itemBarcode_NoTransaksi As Integer = 0
    Dim itemBarcode_NoTransaksiGR2 As Integer = 1
    Dim itemBarcode_Barcode As Integer = 2
    Dim itemBarcode_Jenis As Integer = 3
    Dim itemBarcode_Lokasi_Awal As Integer = 4
    Dim itemBarcode_Lokasi_Tujuan As Integer = 5
    Dim itemBarcode_Jumlah As Integer = 6
    Dim itemBarcode_UserID As Integer = 7
    Dim itemBarcode_QrCode As Integer = 8
    Dim itemBarcode_KdUnikBerjalan As Integer = 9







    Dim itemBarcodeDet_Barcode As Integer = 0
    Dim itemBarcodeDet_NomorLama As Integer = 1
    Dim itemBarcodeDet_Jumlah As Integer = 2




    Private Sub LvBarcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvBarcode.SelectedIndexChanged
        'If LvBarcode.Items.Count = 0 Or LvBarcode.FocusedItem Is Nothing Then Exit Sub

        'Try
        '    OpenConn()

        '    Get_Lv_Data_Barcode(LvBarcode.FocusedItem.Index)

        '    Dim NoSplit As String = Txt_NoSplit.Text.Trim

        '    LvBarcodeDetail.Items.Clear()
        '    SQL = "select Barcode, Nomor_Sebelum, sum(jumlah) as Jumlah "
        '    SQL = SQL & "from N_EMI_Validation_GR_Temp "
        '    SQL = SQL & "where kode_Perusahaan ='" & KodePerusahaan & "' and No_production_Order='" & NoSplit & "' and userID='" & UserID & "' "
        '    SQL = SQL & "and Nomor='" & LvBarcode_ID & "' "
        '    SQL = SQL & "group by Barcode, Nomor_Sebelum "
        '    SQL = SQL & "order by Nomor_Sebelum "
        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read

        '            Dim Lv As ListViewItem
        '            Lv = LvBarcodeDetail.Items.Add(Dr("Barcode"))
        '            Lv.SubItems.Add(Dr("Nomor_Sebelum"))
        '            Lv.SubItems.Add(Dr("Jumlah"))

        '        Loop
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

    End Sub



    Private Sub EMI_Validasi_GR_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Cmb_LokasiTujuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_LokasiTujuan.SelectedIndexChanged
        If Cmb_LokasiTujuan.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Cmb_Jenis.Items.Clear() : arrKdBarangSrap.Clear() : arrNamaScrap.Clear() : arrSatuanScrap.Clear() : arrSatuanKecilScrap.Clear()

            'Cmb_Jenis.Items.Add("FINISHED GOOD") : arrKdBarangSrap.Add("FINISHED GOOD") : arrNamaScrap.Add("FINISHED GOOD") : arrSatuanScrap.Add("FINISHED GOOD")
            'Cmb_Jenis.Items.Add("BLOCKED") : arrKdBarangSrap.Add("REJECTED") : arrNamaScrap.Add("REJECTED") : arrSatuanScrap.Add("REJECTED") : arrSatuanKecilScrap.Add("REJECTED")
            'Cmb_Jenis.Items.Add(" ------------- ") : arrKdBarangSrap.Add("-------") : arrNamaScrap.Add("-------") : arrSatuanScrap.Add("-------") : arrSatuanKecilScrap.Add("-------")

            arrSatuanKecilScrap.Add("Finished Good")
            SQL = "select a.kode_barang_Scrap, b.nama, b.satuan as satuan_kecil, c.satuan "
            SQL = SQL & "from emi_binding_scrap a, barang b, barang_detail_satuan c "
            SQL = SQL & "where a.kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang_scrap=B.Kode_Barang "
            SQL = SQL & "and b.kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_stock_Owner='" & LvPallet_KdSO & "' "
            SQL = SQL & "and c.flag_tampil_display = 'Y' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Jenis.Items.Add(Dr("nama")) : arrKdBarangSrap.Add(Dr("kode_barang_Scrap"))
                    arrNamaScrap.Add(Dr("nama")) : arrSatuanScrap.Add(Dr("satuan"))
                    arrSatuanKecilScrap.Add(Dr("satuan_kecil"))
                Loop
            End Using

            Cmb_Jenis.SelectedIndex = -1

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub EMI_Validasi_GR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        Kosong()

    End Sub




    Private Sub Cmb_Jenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis.SelectedIndexChanged

        'If Cmb_Jenis.Items.Count = 0 Or Cmb_Jenis.SelectedIndex = -1 Then Exit Sub
        'If Not ReadyForPackaging Then
        '    If Cmb_Jenis.SelectedIndex = 0 Then Cmb_Jenis.SelectedIndex = -1
        'End If

        'If Cmb_Jenis.SelectedIndex = 2 Then Cmb_Jenis.SelectedIndex = -1

    End Sub

    Private Sub HapusToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem1.Click

        If LvBarcode.Items.Count = 0 Or LvBarcode.FocusedItem Is Nothing Then Exit Sub

        'Dim SelectedSplit As String = Txt_NoSplit.Text
        Dim SelectedBarcode As String = LvBarcode.FocusedItem.SubItems(itemBarcode_Barcode).Text
        Dim SelectedNoTransaksi As String = LvBarcode.FocusedItem.SubItems(itemBarcode_NoTransaksi).Text

        If MessageBox.Show("Yakin Ingin Hapus Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        Try
            OpenConn()

            SQL = "select Kode_Perusahaan from N_EMI_Validation_GR3_Temp where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "' "
            SQL = SQL & $"and UserID = '" & UserID & "' and (Qr_Code+'-'+Kode_Unik_Berjalan) = '" & SelectedBarcode & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'SQL = "delete N_EMI_Validation_GR_Temp where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & SelectedSplit & "' and nomor = '" & SelectedNomor & "' and UserID = '" & UserID & "' "
                    SQL = "delete N_EMI_Validation_GR3_Temp where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "' "
                    SQL = SQL & "and UserID = '" & UserID & "' and (Qr_Code+'-'+Kode_Unik_Berjalan) = '" & SelectedBarcode & "' "
                    ExecuteTrans(SQL)
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

        TampilTemp()



    End Sub

    Private Sub Btn_Simpan_Sementara_Click(sender As Object, e As EventArgs) Handles Btn_Simpan_Sementara.Click

        If Cmb_LokasiTujuan.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi Tujuan Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_LokasiTujuan.DroppedDown = True : Cmb_LokasiTujuan.Focus() : Exit Sub
        ElseIf Cmb_Jenis.Text.Trim.Length = 0 Then
            MessageBox.Show("Jenis Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis.DroppedDown = True : Cmb_Jenis.Focus() : Exit Sub
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

            'Dim Nomor As Integer = 0
            'SQL = "SELECT ISNULL(( "
            'SQL = SQL & "SELECT TOP 1 Nomor "
            'SQL = SQL & "FROM N_EMI_Validation_GR_Temp a "
            'SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "AND a.No_production_Order = '" & Txt_NoSplit.Text & "' "
            'SQL = SQL & "AND a.userid = '" & UserID & "' "
            'SQL = SQL & "ORDER BY Nomor DESC ), 0) AS Nomor "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Val(Dr("Nomor")) = 0 Then

            '            Dr.Close()
            '            SQL = "select top 1 b.Nomor "
            '            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Split_Production_Order c "
            '            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = b.kode_perusahaan "
            '            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and a.no_production_order = c.No_Transaksi "
            '            SQL = SQL & "and a.Status is null and c.Status is null "
            '            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            '            SQL = SQL & "and a.No_Production_Order = '" & Txt_NoSplit.Text & "' "
            '            SQL = SQL & "order by b.nomor DESC "
            '            Using Dr1 = OpenTrans(SQL)
            '                If Dr1.Read Then
            '                    Nomor = Val(Dr1("Nomor")) + 1
            '                Else
            '                    Nomor = 1
            '                End If
            '            End Using

            '        Else
            '            Nomor = Dr("Nomor") + 1
            '        End If
            '    End If
            'End Using




            For i As Integer = 0 To Lv_Data.Items.Count - 1
                Get_Lv_Data_GR(i)

                SQL = "select DISTINCT b.No_Split "
                SQL &= $"from Emi_Production_Results_Validation a "
                SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan  = b.Kode_Perusahaan and a.No_Transaksi  = b.No_Transaksi "
                SQL &= $"inner join Barang_SN c on b.Kode_Perusahaan  = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number  "
                SQL &= $"where a.Kode_Perusahaan  = '{KodePerusahaan}' "
                SQL &= $"and a.Status is null "
                SQL &= $"and b.Flag_Validasi_Loading = 'Y' "
                SQL &= $"and (c.Qr_Code+'-'+c.Kode_Unik_Berjalan) = '{LvData_Barcode}' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For j As Integer = 0 To .Rows.Count - 1

                                '========================================
                                '=     CEK APAKAH SPLIT DI BATALKAN     =
                                '========================================
                                SQL = "Select top 1 a.Kode_Perusahaan from Emi_Split_Production_Order a, emi_production_results b "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Production_Order "
                                SQL = SQL & "And a.Status = 'Y' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & .Rows(j).Item("No_Split") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Gagal Simpan, Split Sudah Di Batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using


                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show($"data pada Barcode {LvData_Barcode} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using





                'SQL = "insert into N_EMI_Validation_GR_Temp(Kode_perusahaan, No_production_Order, UserID, Nomor, Barcode, Jenis, "
                'SQL = SQL & "Jumlah, Lokasi_Tujuan, Nomor_Sebelum, Satuan, Batch, Tahap)  "
                'SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoSplit.Text & "', '" & UserID & "', '" & Nomor & "', "
                'SQL = SQL & "'" & LvData_Barcode & "', '" & arrKdBarangSrap(Cmb_Jenis.SelectedIndex) & "', '" & HilangkanTanda(LvData_Jumlah) & "', "
                ''SQL = SQL & " '" & arrSO.Item(Cmb_LokasiTujuan.SelectedIndex) & "', '" & LvData_Nomor & "', '" & LvData_Satuan & "', '" & LvData_Batch & "', '" & LvData_Tahap & "' )"
                'ExecuteTrans(SQL)

                SQL = "insert into N_EMI_Validation_GR3_Temp(Kode_Perusahaan, No_Transaksi, No_Transaksi_GR2, No_Split, Qr_Code, Kode_Unik_Berjalan, Jenis, Kd_Barang, So_AWal, So_Tujuan, Jumlah, UserID, Kode_Unik_Transaksi) "
                SQL = SQL & "values ('" & KodePerusahaan & "', '" & LvData_NoFaktur & "', '" & LvData_NoTransGR2 & "', '-', '" & LvData_QR & "', '" & LvData_KdUnikBerjalan & "', "
                SQL = SQL & "'" & Cmb_Jenis.Text & "', '" & arrKdBarangSrap(Cmb_Jenis.SelectedIndex) & "', '" & LvData_KdSoAwal & "', "
                SQL = SQL & "'" & arrSO(Cmb_LokasiTujuan.SelectedIndex) & "', '" & HilangkanTanda(LvData_Jumlah) & "', '" & UserID & "', '" & Kode_Unik_Transaksi & "') "
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
        Cmb_Jenis.SelectedIndex = -1

        KosongTab1()
        TampilTemp()
    End Sub

    Private Sub get_no_faktur()
        Dim FValidasiGR As String = "VGR3-"
        TxtNo_Transaksi.Text = FValidasiGR & Format(tgl_skg, "MMyy") & "-" &
                            General_Class.Get_Last_Number2("N_EMI_Validation_GR_3", "No_Transaksi", 5,
                            "Kode_perusahaan", KodePerusahaan,
                            "And", "substring(No_Transaksi, 1, " & Len(FValidasiGR) + 4 & ")", FValidasiGR & Format(tgl_skg, "MMyy"))

    End Sub

    Private Sub get_no_faktur_Temp()
        Dim FValidasiGR As String = "VGR3-"
        TxtNo_Transaksi.Text = FValidasiGR & Format(tgl_skg, "MMyy") & "-" &
                            General_Class.Get_Last_Number2("N_EMI_Validation_GR3_Temp", "No_Transaksi", 5,
                            "Kode_perusahaan", KodePerusahaan,
                            "And", "substring(No_Transaksi, 1, " & Len(FValidasiGR) + 4 & ")", FValidasiGR & Format(tgl_skg, "MMyy"))

    End Sub

    Private Sub Kosong()

        Kode_Unik_Transaksi = Generate_Random_Kode(10)

        TxtKeterangan.Text = ""
        'Txt_NoSplit.Text = ""
        Txt_Barcode.Text = ""
        Txt_TotFG.Text = ""
        Txt_TotBeratKG.Text = ""
        Txt_ScanBarcode.Text = ""
        Txt_HslProduksi.Text = ""
        Txt_Jumlah.Text = ""
        Txt_Satuan.Text = ""
        Txt_TotFG.Text = ""
        Txt_TotBeratKG.Text = ""
        Txt_NoTransGR2.Text = ""
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

        ReadyForPackaging = False

        get_jam()

        Try
            OpenConn()

            get_no_faktur()

            'SQL = "delete N_EMI_Validation_GR_Temp where kode_perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
            SQL = "delete N_EMI_Validation_GR3_Temp where kode_perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
            ExecuteTrans(SQL)

            Cmb_LokasiTujuan.Items.Clear() : Cmb_LokasiTujuan.SelectedIndex = -1 : arrSO.Clear()
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' "
            SQL = SQL & "and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    arrSO.Add(dr("kode_stock_owner"))
                    Cmb_LokasiTujuan.Items.Add(dr("Keterangan"))
                Loop
            End Using

            Cmb_Jenis.Items.Clear() : arrKdBarangSrap.Clear() : arrNamaScrap.Clear() : arrSatuanScrap.Clear() : arrSatuanKecilScrap.Clear()

            'Cmb_Jenis.Items.Add("FINISHED GOOD") : arrKdBarangSrap.Add("FINISHED GOOD") : arrNamaScrap.Add("FINISHED GOOD") : arrSatuanScrap.Add("FINISHED GOOD")
            'Cmb_Jenis.Items.Add("BLOCKED") : arrKdBarangSrap.Add("REJECTED") : arrNamaScrap.Add("REJECTED") : arrSatuanScrap.Add("REJECTED") : arrSatuanKecilScrap.Add("REJECTED")
            'Cmb_Jenis.Items.Add("------- ") : arrKdBarangSrap.Add("-------") : arrNamaScrap.Add("-------") : arrSatuanScrap.Add("-------") : arrSatuanKecilScrap.Add("-------")

            arrSatuanKecilScrap.Add("Finished Good")
            SQL = "select a.kode_barang_Scrap, b.nama, b.satuan as satuan_kecil, c.satuan "
            SQL = SQL & "from emi_binding_scrap a, barang b, barang_detail_satuan c "
            SQL = SQL & "where a.kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang_scrap=B.Kode_Barang "
            SQL = SQL & "and b.kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_stock_Owner='" & LvPallet_KdSO & "' "
            SQL = SQL & "and c.flag_tampil_display = 'Y' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Jenis.Items.Add(Dr("nama")) : arrKdBarangSrap.Add(Dr("kode_barang_Scrap"))
                    arrNamaScrap.Add(Dr("nama")) : arrSatuanScrap.Add(Dr("satuan"))
                    arrSatuanKecilScrap.Add(Dr("satuan_kecil"))
                Loop
            End Using


            Cmb_Jenis.SelectedIndex = -1


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
        Lv_DataPallet.Columns.Add("Nama Barang", 220, HorizontalAlignment.Left) '6
        Lv_DataPallet.Columns.Add("Jumlah", 130, HorizontalAlignment.Right) '7
        Lv_DataPallet.Columns.Add("Satuan", 70, HorizontalAlignment.Center) '8
        Lv_DataPallet.Columns.Add("Kualitas", 0, HorizontalAlignment.Center) '9
        Lv_DataPallet.Columns.Add("warna", 0, HorizontalAlignment.Center) '10
        Lv_DataPallet.Columns.Add("ID", 0, HorizontalAlignment.Center) '11
        Lv_DataPallet.Columns.Add("QR", 0, HorizontalAlignment.Center) '12
        Lv_DataPallet.Columns.Add("KdUnikBerjalan", 0, HorizontalAlignment.Center) '13
        Lv_DataPallet.Columns.Add("No Transaksi GR 2", 130, HorizontalAlignment.Center).DisplayIndex = 2 '14
        Lv_DataPallet.View = View.Details

        'Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
        'Lv_Data.Columns.Add("Barcode", 630, HorizontalAlignment.Left)
        'Lv_Data.Columns.Add("Batch", 80, HorizontalAlignment.Center)
        'Lv_Data.Columns.Add("Nomor", 100, HorizontalAlignment.Center)
        'Lv_Data.Columns.Add("Jumlah", 180, HorizontalAlignment.Right)
        'Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        'Lv_Data.Columns.Add("Berat", 0, HorizontalAlignment.Center)
        'Lv_Data.Columns.Add("Tahap", 0, HorizontalAlignment.Center)
        'Lv_Data.View = View.Details

        Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
        Lv_Data.Columns.Add("Lokasi Awal", 120, HorizontalAlignment.Center) '0
        'Lv_Data.Columns.Add("Lokasi Tujuan", 120, HorizontalAlignment.Center) '1
        Lv_Data.Columns.Add("Barcode", 250, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Batch Number", 100, HorizontalAlignment.Center) '2
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left) '3
        Lv_Data.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left) '4
        'Lv_Data.Columns.Add("Jenis", 130, HorizontalAlignment.Center) '5
        Lv_Data.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '5
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '6
        Lv_Data.Columns.Add("ID", 0, HorizontalAlignment.Center) '7
        Lv_Data.Columns.Add("QR", 0, HorizontalAlignment.Center) '8
        Lv_Data.Columns.Add("KdUnikBerjalan", 0, HorizontalAlignment.Center) '9
        Lv_Data.Columns.Add("TglExpired", 0, HorizontalAlignment.Center) '10
        'Lv_Data.Columns.Add("KdBarangScrap", 0, HorizontalAlignment.Center) '10
        Lv_Data.Columns.Add("NoTransGr2", 0, HorizontalAlignment.Center) '11
        Lv_Data.Columns.Add("NoFaktur", 0, HorizontalAlignment.Center) '12
        Lv_Data.View = View.Details


        LvBarcode.Columns.Clear() : LvBarcode.Items.Clear()
        LvBarcode.Columns.Add("No_Transaksi", 0, HorizontalAlignment.Left)
        LvBarcode.Columns.Add("No Transaksi GR 2", 130, HorizontalAlignment.Left)
        LvBarcode.Columns.Add("Barcode", 200, HorizontalAlignment.Left)
        LvBarcode.Columns.Add("Jenis", 100, HorizontalAlignment.Center)
        LvBarcode.Columns.Add("Lokasi Awal", 130, HorizontalAlignment.Left)
        LvBarcode.Columns.Add("Lokasi Tujuan", 130, HorizontalAlignment.Left)
        LvBarcode.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        LvBarcode.Columns.Add("User", 100, HorizontalAlignment.Center)
        LvBarcode.Columns.Add("QrCode", 0, HorizontalAlignment.Center)
        LvBarcode.Columns.Add("KodeUnikBerjalan", 0, HorizontalAlignment.Center)
        LvBarcode.View = View.Details

        'LvBarcodeDetail.Columns.Clear() : LvBarcodeDetail.Items.Clear()
        'LvBarcodeDetail.Columns.Add("Barcode", 250, HorizontalAlignment.Left)
        'LvBarcodeDetail.Columns.Add("Nomor Lama", 100, HorizontalAlignment.Center)
        'LvBarcodeDetail.Columns.Add("Jumlah", 120, HorizontalAlignment.Center)
        'LvBarcodeDetail.View = View.Details


        'Lv_SummaryPackaging.Columns.Clear() : Lv_SummaryPackaging.Items.Clear()
        'Lv_SummaryPackaging.Columns.Add("Lokasi", 0, HorizontalAlignment.Left)
        'Lv_SummaryPackaging.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        'Lv_SummaryPackaging.Columns.Add("Barang", 200, HorizontalAlignment.Left)
        'Lv_SummaryPackaging.Columns.Add("Jumlah", 110, HorizontalAlignment.Right)
        'Lv_SummaryPackaging.Columns.Add("Satuan", 70, HorizontalAlignment.Center)
        'Lv_SummaryPackaging.View = View.Details

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
        Txt_NoTransGR2.Text = ""
        Txt_Satuan.Text = ""
        Txt_HslProduksi.Text = ""
        Txt_Sisa.Text = ""
        Txt_Jumlah.Text = ""
        Txt_TotFG.Text = ""
        Txt_TotBeratKG.Text = ""
        CurrentBatch = ""

        Btn_Tambah.Enabled = True : Btn_Simpan_Sementara.Enabled = True

        Lv_Data.Items.Clear()
        Cmb_LokasiTujuan.SelectedIndex = -1
        Cmb_Jenis.SelectedIndex = -1

        LoadFromSD()
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
        LvPallet_NoTransGr2 = Lv_DataPallet.Items(index).SubItems(itemPallet_NoTransGR2).Text

    End Sub

    Private Sub Get_Lv_Data_GR(ByVal index As Integer)

        LvData_KdSoAwal = Lv_Data.Items(index).SubItems(itemData_KdSoAwal).Text
        'LvData_KdSoTujuan = Lv_Data.Items(index).SubItems(itemData_KdSoTujuan).Text
        LvData_Barcode = Lv_Data.Items(index).SubItems(itemData_Barcode).Text
        LvData_BatchNumber = Lv_Data.Items(index).SubItems(itemData_BatchNumber).Text
        LvData_KdBarang = Lv_Data.Items(index).SubItems(itemData_KdBarang).Text
        LvData_NmBarang = Lv_Data.Items(index).SubItems(itemData_NmBarang).Text
        'LvData_Jenis = Lv_Data.Items(index).SubItems(itemData_Jenis).Text
        LvData_Jumlah = Lv_Data.Items(index).SubItems(itemData_Jumlah).Text
        LvData_Satuan = Lv_Data.Items(index).SubItems(itemData_Satuan).Text
        LvData_ID = Lv_Data.Items(index).SubItems(itemData_ID).Text
        LvData_QR = Lv_Data.Items(index).SubItems(itemData_QR).Text
        LvData_KdUnikBerjalan = Lv_Data.Items(index).SubItems(itemData_KdUnikBerjalan).Text
        LvData_TglExpired = Lv_Data.Items(index).SubItems(itemData_TglExpired).Text
        'LvData_KdBarangScrap = Lv_Data.Items(index).SubItems(itemData_KdBarangScrap).Text
        LvData_NoTransGR2 = Lv_Data.Items(index).SubItems(itemData_NoTransGR2).Text
        LvData_NoFaktur = Lv_Data.Items(index).SubItems(itemData_NoFaktur).Text

    End Sub

    Private Sub Get_Lv_Data_Barcode(ByVal index As Integer)

        LvBarcode_NoTransksi = LvBarcode.Items(index).SubItems(itemBarcode_NoTransaksi).Text
        LvBarcode_NoTransaksiGR2 = LvBarcode.Items(index).SubItems(itemBarcode_NoTransaksiGR2).Text
        LvBarcode_Barcode = LvBarcode.Items(index).SubItems(itemBarcode_Barcode).Text
        LvBarcode_Jenis = LvBarcode.Items(index).SubItems(itemBarcode_Jenis).Text
        LvBarcode_Lokasi_Awal = LvBarcode.Items(index).SubItems(itemBarcode_Lokasi_Awal).Text
        LvBarcode_Lokasi_Tujuan = LvBarcode.Items(index).SubItems(itemBarcode_Lokasi_Tujuan).Text
        LvBarcode_Jumlah = LvBarcode.Items(index).SubItems(itemBarcode_Jumlah).Text
        LvBarcode_User = LvBarcode.Items(index).SubItems(itemBarcode_UserID).Text
        LvBarcode_QrCode = LvBarcode.Items(index).SubItems(itemBarcode_QrCode).Text
        LvBarcode_KdUnikBerjalan = LvBarcode.Items(index).SubItems(itemBarcode_KdUnikBerjalan).Text

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
            'SQL = "select No_Transaksi, No_Transaksi_GR2, (Qr_Code +'-'+Kode_Unik_Berjalan) as Barcode, Jenis, So_Awal, So_Tujuan, Jumlah, UserID, Qr_Code, Kode_Unik_Berjalan "
            'SQL = SQL & "from N_EMI_Validation_GR3_Temp  "
            'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "' "
            'SQL = SQL & "order by no_Transaksi "

            SQL = "select No_Transaksi_GR2, (Qr_Code +'-'+Kode_Unik_Berjalan) as Barcode, Jenis, So_Awal, So_Tujuan, sum(Jumlah) as Jumlah , UserID, Qr_Code, Kode_Unik_Berjalan "
            SQL &= $"from N_EMI_Validation_GR3_Temp "
            SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
            SQL &= $"and Kode_Unik_Transaksi = '{Kode_Unik_Transaksi}' "
            SQL &= $"group by No_Transaksi_GR2, (Qr_Code +'-'+Kode_Unik_Berjalan), Jenis, So_Awal, So_Tujuan, UserID, Qr_Code, Kode_Unik_Berjalan "
            SQL &= $"order by No_Transaksi_GR2 "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    'Lv = LvBarcode.Items.Add(Dr("No_Transaksi"))
                    Lv = LvBarcode.Items.Add("-")
                    Lv.SubItems.Add(Dr("No_Transaksi_GR2"))

                    'If Dr("Jenis").ToString.ToUpper = "REJECTED" Then
                    '    Lv.SubItems.Add("Disqualified")
                    'Else
                    '    Lv.SubItems.Add(Dr("Jenis"))
                    'End If
                    Lv.SubItems.Add(Dr("Barcode"))
                    Lv.SubItems.Add(Dr("Jenis"))
                    Lv.SubItems.Add(Dr("So_Awal"))
                    Lv.SubItems.Add(Dr("So_Tujuan"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                    Lv.SubItems.Add(Dr("UserID"))
                    Lv.SubItems.Add(Dr("Qr_Code"))
                    Lv.SubItems.Add(Dr("Kode_Unik_Berjalan"))


                Loop
            End Using

            'Dim KdBarang As String = ""
            'SQL = "select distinct top 1 b.Kode_Barang, b.Nama, b.Berat "
            'SQL = SQL & "from Barang_SN a, barang b, N_EMI_Validation_GR_Temp c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            'SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and (qr_code +'-'+Kode_Unik_Berjalan ) = c.Barcode "
            'SQL = SQL & "and c.No_Production_Order = '" & NoSplit & "' "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        KdBarang = Dr("Kode_Barang")
            '    End If
            'End Using

            ''==========================
            ''=     LOAD PACKAGING     =
            ''==========================
            'Dim TotalInput As Double = 0
            'For i As Integer = 0 To LvBarcode.Items.Count - 1
            '    Get_Lv_Data_Barcode(i)

            '    If LvBarcode_Jenis.ToUpper.Trim = "FINISHED GOOD" Then
            '        TotalInput += Val(HilangkanTanda(LvBarcode_Total))
            '    End If


            'Next

            'Dim kd_inq As String = ""
            'SQL = "select top(1) kode_barang_inq from barang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_barang = '" & KdBarang & "'  "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        kd_inq = Dr("kode_barang_inq")
            '    Else
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Data Tidak di temukan . !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            'Lv_SummaryPackaging.Items.Clear()
            'SQL = "select a.No_Transaksi, b.Jumlah_Bahan, b.Jumlah_Barang, "
            'SQL = SQL & "b.Kode_Stock_Owner, a.Kode_Barang, b.Kode_Barang as Kode_Bahan, c.Nama ,b.Jumlah, b.Satuan, c.flag_potong_stok, "
            'SQL = SQL & "isnull(c.standar_price,0) as standar_price, isnull(c.Flag_Pembulatan_Produksi,'T') as Flag_Pembulatan_Produksi "
            'SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
            'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
            'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & Txt_NoSplit.Text & "' "
            'SQL = SQL & "and a.Status is null and b.jenis <> 'KEMASAN UTAMA' "
            'SQL = SQL & "order by c.nama "
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        If .Rows.Count <> 0 Then
            '            For i As Integer = 0 To .Rows.Count - 1
            '                '================================
            '                '=     GET JUMLAH KEBUTUHAN     =
            '                '================================
            '                Dim KebutuhanBarang As Double = 0
            '                Dim KebutuhanBahan As Double = 0
            '                SQL = "select a.Kode_Bahan, a.Jumlah_Bahan, a.Jumlah_Barang "
            '                SQL = SQL & "from Barang_Detail_Bahan_Penolong a "
            '                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            '                SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
            '                SQL = SQL & "and Kode_Bahan = '" & .Rows(i).Item("Kode_Bahan") & "' "
            '                Using Dr = OpenTrans(SQL)
            '                    If Dr.Read Then
            '                        KebutuhanBarang = Val(HilangkanTanda(Format(Dr("Jumlah_Barang"), "N4")))
            '                        KebutuhanBahan = Val(HilangkanTanda(Format(Dr("Jumlah_Bahan"), "N4")))
            '                    End If
            '                End Using

            '                Dim PackagingDigunakan As Double = Val(HilangkanTanda(Format((Val(HilangkanTanda(TotalInput)) / KebutuhanBarang) * KebutuhanBahan, "N4")))

            '                If .Rows(i).Item("Flag_Pembulatan_Produksi") = "Y" Then
            '                    PackagingDigunakan = Math.Ceiling(PackagingDigunakan)
            '                End If


            '                Dim Lv As ListViewItem
            '                Lv = Lv_SummaryPackaging.Items.Add(.Rows(i).Item("Kode_Stock_Owner"))
            '                Lv.SubItems.Add(.Rows(i).Item("Kode_Bahan"))
            '                Lv.SubItems.Add(.Rows(i).Item("Nama"))
            '                'Lv.SubItems.Add(Format(.Rows(i).Item("JumlahStock"), "N2"))
            '                Lv.SubItems.Add(Format(PackagingDigunakan, "N2"))
            '                Lv.SubItems.Add(.Rows(i).Item("Satuan"))

            '            Next
            '        End If
            '    End With
            'End Using










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

                'Dim NoSplit As String = Txt_NoSplit.Text.Trim

                'SQL = "select a.no_transaksi as No_GR2, a.No_Production_Order as No_Split, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama as Nama_Barang, b.Jenis, c.Qr_Code, c.Kode_Unik_Berjalan, c.Batch_Number, "
                'SQL = SQL & "c.Tgl_Produksi, c.Tgl_Expired, a.UserID, b.tahap, "

                ''SQL = SQL & "isnull(((isnull(sum(c.Jumlah), 0)) - "
                ''SQL = SQL & "ISNULL((select isnull(sum(z.jumlah), 0) from N_EMI_Validation_GR_3_Detail z, N_EMI_Validation_GR_3 x "
                ''SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = a.Kode_Perusahaan "
                ''SQL = SQL & "and z.No_Transaksi = x.No_Transaksi and z.No_Transaksi_GR2 = a.No_Transaksi and x.status is null "
                ''SQL = SQL & "),0)), 0) as Jumlah, "

                'SQL = SQL & "isnull(sum(c.Jumlah), 0) as Jumlah, "

                'SQL = SQL & "b.Satuan, b.Warna, e.Keterangan as Kualitas, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, "

                'SQL = SQL & "case "
                'SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = a.No_Production_Order and z.No_Batch = b.Tahap), 'U') = 'T' then 'DITOLAK' "
                'SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = a.No_Production_Order and z.No_Batch = b.Tahap), 'U') = 'Y' then 'DITERIMA' "
                'SQL = SQL & "when isnull(( select top 1 z.Flag_Ready_For_Packaging from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = a.No_Production_Order "
                'SQL = SQL & "and z.No_Batch = b.Tahap and z.Flag_Ready_For_Packaging = 'Y' and z.No_GR = '2' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' then 'READY FOR PACKING' "
                'SQL = SQL & "when isnull(( select top 1 z.Kode_Perusahaan from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = a.No_Production_Order "
                'SQL = SQL & "and z.No_Batch = b.Tahap and z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging is null and z.No_GR = '2' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' then 'HOLD' "
                'SQL = SQL & "else 'NO DATA' "
                'SQL = SQL & "end as Status_Split "

                'SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Barang_SN c, barang d, EMI_Master_Warna e "
                'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
                'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                'SQL = SQL & "and b.kode_stock_owner_tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Tujuan = c.Serial_Number "
                'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
                'SQL = SQL & "and b.Warna = e.Kode_Warna "
                'SQL = SQL & "and a.Status is null and a.Flag_Validasi is null "
                'SQL = SQL & "and b.Jenis = 'Finished Good' "
                'SQL = SQL & "and c.Jumlah <> 0 "

                'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) = '" & Txt_ScanBarcode.Text.Trim & "' "
                'SQL = SQL & "group by a.kode_perusahaan, a.No_Transaksi, a.No_Production_Order, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama, b.Jenis, c.Qr_Code, "
                'SQL = SQL & "c.Kode_Unik_Berjalan, c.Batch_Number, c.Tgl_Produksi, c.Tgl_Expired, a.UserID, b.Satuan, b.Warna, e.Keterangan, b.Tahap "
                'SQL = SQL & "order by a.No_Production_Order, b.Kode_Stock_Owner_Tujuan, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan), c.Tgl_Expired ASC "

                SQL = ";With Cte as ("
                SQL &= $"select a.No_Transaksi as No_GR2, isnull(b.No_Split, '-') as No_Split, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama as Nama_Barang, b.Jenis, c.Qr_Code, c.Kode_Unik_Berjalan, c.Batch_Number, "
                SQL &= $"c.Tgl_Produksi, c.Tgl_Expired, a.UserID,  "
                SQL &= $"isnull(sum(c.Jumlah), 0) as Jumlah, "
                SQL &= $"b.Satuan, b.Warna, e.Keterangan as Kualitas, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, "
                SQL &= $"case when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = b.No_Split and z.No_Batch = b.Tahap), 'U') = 'T' then 'DITOLAK' "
                SQL &= $"when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = b.No_Split and z.No_Batch = b.Tahap), 'U') = 'Y' then 'DITERIMA' "
                SQL &= $"when isnull(( select top 1 z.Flag_Ready_For_Packaging from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = b.No_Split "
                SQL &= $"and z.No_Batch = b.Tahap and z.Flag_Ready_For_Packaging = 'Y' and z.No_GR = '2' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' then 'READY FOR PACKING' "
                SQL &= $"when isnull(( select top 1 z.Kode_Perusahaan from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = b.No_Split "
                SQL &= $"and z.No_Batch = b.Tahap and z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging is null and z.No_GR = '2' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' then 'HOLD' "
                SQL &= $"else 'NO DATA' "
                SQL &= $"end as Status_Split "
                SQL &= $"from Emi_Production_Results_Validation a "
                SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
                SQL &= $"inner join Barang_SN c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number  "
                SQL &= $"inner join Barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
                SQL &= $"inner join EMI_Master_Warna e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.Warna  = e.Kode_Warna  "
                SQL &= $"where a.Kode_Perusahaan  = '{KodePerusahaan}' "
                SQL &= $"and a.Status is null "
                SQL &= $"and a.Flag_Validasi is NULl "
                SQL &= $"and b.Flag_Validasi_Loading = 'Y' "
                SQL &= $"and b.Jenis = 'Finished Good' "
                SQL &= $"and c.Jumlah <> 0 "
                SQL &= $"and (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) = '" & Txt_ScanBarcode.Text.Trim & "' "
                SQL &= $"group by a.kode_perusahaan, a.No_Transaksi, b.No_Split, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama, b.Jenis, c.Qr_Code, "
                SQL &= $"c.Kode_Unik_Berjalan, c.Batch_Number, c.Tgl_Produksi, c.Tgl_Expired, a.UserID, b.Satuan, b.Warna, e.Keterangan, b.Tahap "
                SQL &= $") select No_GR2, Kode_Stock_Owner_Tujuan, Kode_Barang, Nama_Barang, Jenis, Qr_Code, Kode_Unik_Berjalan, Batch_Number, Tgl_Produksi, Tgl_Expired, UserID, sum(Jumlah) as Jumlah, "
                SQL &= $"Satuan, Warna, Kualitas, Barcode, Status_Split "
                SQL &= $"from cte "
                SQL &= $"group by No_GR2, Kode_Stock_Owner_Tujuan, Kode_Barang, Nama_Barang, Jenis, Qr_Code, Kode_Unik_Berjalan, Batch_Number, Tgl_Produksi, Tgl_Expired, UserID, "
                SQL &= $"Satuan, Warna, Kualitas, Barcode, Status_Split "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        'If Not NoSplit = "" Then
                        '    If NoSplit <> Dr("No_Split") Then
                        '        Dr.Close()
                        '        CloseConn()
                        '        MessageBox.Show("No Split Tidak Boleh Berbeda", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        Exit Sub
                        '    End If
                        'End If

                        'If NoSplit = "" Then
                        '    NoSplit = Dr("No_Split")
                        'Else

                        '    If NoSplit <> Dr("No_Split") Then
                        '        isCombine = True
                        '        'Dr.Close()
                        '        'CloseConn()
                        '        'MessageBox.Show("No Split Tidak Boleh Berbeda", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        'Exit Sub
                        '    End If
                        'End If

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

                        Lv = Lv_DataPallet.Items.Add(Dr("Kode_Stock_Owner_Tujuan"))
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
                        Lv.SubItems.Add(Dr("No_GR2"))

                        'Txt_NoSplit.Text = Dr("No_Split")
                        Txt_ScanBarcode.Text = ""


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
                        If Lv_DataPallet.Items.Count = 0 Then
                            'Txt_NoSplit.Text = ""
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

            N_EMI_SD_Validasi_GR_3.arrBarcodeFromParent.Clear()
            N_EMI_SD_Validasi_GR_3.arrBarcodeFromParent = dataBarcode
            N_EMI_SD_Validasi_GR_3.ShowDialog()

        End If

    End Sub

    Private Sub Lv_DataPallet_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DataPallet.DoubleClick
        If Lv_DataPallet.Items.Count = 0 Or Lv_DataPallet.FocusedItem.Index = -1 Then Exit Sub

        Get_LvPallet_Data(Lv_DataPallet.FocusedItem.Index)

        If Not CurrentBatch = "" Then
            If CurrentBatch <> SelectedBatch.Text Then
                MessageBox.Show("Batch yang Diinput Berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If



        Txt_Barcode.Text = LvPallet_Barcode
        Txt_HslProduksi.Text = Format(Val(HilangkanTanda(LvPallet_Jumlah)), "N0")
        Txt_Satuan.Text = LvPallet_Satuan

        Txt_Jumlah.Text = 0
        Txt_NoTransGR2.Text = LvPallet_NoTransGr2




        Dim Sisa As Double = 0
        For i As Integer = 0 To Lv_Data.Items.Count - 1
            Get_Lv_Data_GR(i)
            If LvPallet_Barcode = LvData_Barcode Then
                Sisa += Val(HilangkanTanda(LvData_Jumlah))
            End If
        Next

        Txt_Sisa.Text = Format((Val(HilangkanTanda(LvPallet_Jumlah)) - (Sisa)), "N0")





        Txt_SelectedBatch.Text = LvPallet_BatchNumber
        Txt_SelectedKdBarang.Text = LvPallet_KdBarang
        Txt_SelectedNmBarang.Text = LvPallet_NmBarang
        Txt_SelectedID.Text = LvPallet_ID
        Txt_SelectedQR.Text = LvPallet_QR
        Txt_SelectedKdUnikBerjalan.Text = LvPallet_KdUnikBerjalan
        Txt_TglExpired.Text = LvPallet_TglExpired


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

        If Txt_Jumlah.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jumlah.Focus() : Exit Sub
        End If


        If Txt_Jumlah.Text.Trim.Length = 0 Then
            If Val(HilangkanTanda(Txt_Jumlah.Text)) > Val(HilangkanTanda(Txt_Sisa.Text)) Then
                MessageBox.Show("Jumlah Tidak Boleh Lebih dari Sisa", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Jumlah.Focus() : Exit Sub
            End If
        End If

        'For i As Integer = 0 To Lv_Data.Items.Count - 1
        '    Get_Lv_Data_GR(i)

        '    If LvData_Barcode = Txt_Barcode.Text And LvData_Jenis = Cmb_Jenis.Text And LvData_ID = Txt_SelectedID.Text Then
        '        MessageBox.Show("Barcode Sudah Ada", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Txt_Jumlah.Focus() : Exit Sub
        '    End If

        'Next

        Try
            OpenConn()

            SQL = "select no_transaksi from N_EMI_Validation_GR3_Temp where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    get_no_faktur_Temp()
                Else
                    Dr.Close()
                    get_no_faktur()
                End If
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dim Lv As ListViewItem
        Lv = Lv_Data.Items.Add(LvPallet_KdSO) '0
        'Lv.SubItems.Add(arrSO(Cmb_LokasiTujuan.SelectedIndex))
        Lv.SubItems.Add(Txt_Barcode.Text) '1
        Lv.SubItems.Add(Txt_SelectedBatch.Text) '2
        Lv.SubItems.Add(Txt_SelectedKdBarang.Text) '3
        Lv.SubItems.Add(Txt_SelectedNmBarang.Text) '4
        Lv.SubItems.Add(Format(Val(HilangkanTanda(Txt_Jumlah.Text)), "N2")) '5
        Lv.SubItems.Add(Txt_Satuan.Text) '6
        Lv.SubItems.Add(Txt_SelectedID.Text) '7
        Lv.SubItems.Add(Txt_SelectedQR.Text) '8
        Lv.SubItems.Add(Txt_SelectedKdUnikBerjalan.Text) '9
        Lv.SubItems.Add(Txt_TglExpired.Text) '10
        'Lv.SubItems.Add(arrKdBarangSrap(Cmb_Jenis.SelectedIndex)) '11
        Lv.SubItems.Add(Txt_NoTransGR2.Text) '11
        Lv.SubItems.Add(TxtNo_Transaksi.Text) '12




        'Txt_Barcode.Text = ""
        'Txt_HslProduksi.Text = ""
        'Txt_Sisa.Text = ""
        'Txt_Satuan.Text = ""
        Txt_Jumlah.Text = ""
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

        Dim Sisa As Double = 0
        For i As Integer = 0 To Lv_Data.Items.Count - 1
            Get_Lv_Data_GR(i)
            If LvPallet_Barcode = LvData_Barcode Then
                Sisa += Val(HilangkanTanda(LvData_Jumlah))
            End If
        Next
        Txt_Sisa.Text = Format(Val(HilangkanTanda(Txt_HslProduksi.Text)) - Sisa, "N0")

        Hitung_Data()

        Txt_Jumlah.Focus()
    End Sub

    Private Sub CekMilitarySampling()
        'If Lv_Data.Items.Count = 0 Then Exit Sub

        'Try
        '    OpenConn()

        '    For i As Integer = 0 To Lv_Data.Items.Count - 1

        '        Get_Lv_Data_GR(i)

        '        SQL = "select top 1 Kode_Perusahaan, Tahap_Military_Sampling, Flag_Ready_For_Packaging from N_EMI_Military_Sampling where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Split = '" & Txt_NoSplit.Text & "' "
        '        SQL = SQL & "and No_Batch = '" & LvData_Tahap & "' and Flag_Ready_For_Packaging = 'Y' order by Tahap_Military_Sampling DESC"
        '        Using Dr = OpenTrans(SQL)
        '            If Dr.Read Then

        '                ReadyForPackaging = True

        '                'If General_Class.CekNULL(Dr("Flag_Ready_For_Packaging")) = "" Then
        '                '    ReadyForPackaging = False
        '                '    Cmb_Jenis.SelectedIndex = -1
        '                '    Dr.Close()
        '                '    Exit For
        '                'Else
        '                '    ReadyForPackaging = True
        '                'End If

        '            Else
        '                Cmb_Jenis.SelectedIndex = -1
        '                ReadyForPackaging = False
        '            End If
        '        End Using


        '    Next



        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try


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

            Dim lokasi_awal As String = ""


            Dim SumHPPAwal As Double = 0
            Dim sumPackaging As Double = 0
            Dim ArrHPP_GroupJenisID As New ArrayList
            Dim ArrHPP_GroupJenisNm As New ArrayList
            Dim ArrHPP_Lokasi As New ArrayList
            Dim ArrHPP_Akun As New ArrayList
            Dim ArrHPP_Nilai As New ArrayList

            'Dim Temp_NoSplit As String = Txt_NoSplit.Text.Trim

            Dim Temp_NoTransksi = TxtNo_Transaksi.Text


            '========================
            '=     INSERT INDUK     =
            '========================
            SQL = "insert into N_EMI_Validation_GR_3(Kode_Perusahaan, No_Transaksi, No_Production_Order, Tanggal, jam, Keterangan, UserID)  "
            SQL = SQL & "values ('" & KodePerusahaan & "', '" & Temp_NoTransksi & "', '-', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & TxtKeterangan.Text & "', '" & UserID & "')"
            ExecuteTrans(SQL)

            SQL = "select kode_Perusahaan, '' as No_Transaksi, No_Transaksi_GR2, No_Split, Qr_Code, Kode_Unik_Berjalan, Jenis, Kd_Barang, So_Awal, So_Tujuan, sum(Jumlah) as Jumlah, UserID "
            SQL = SQL & "from N_EMI_Validation_GR3_Temp  "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and no_split = '" & Txt_NoSplit.Text & "' "
            SQL = SQL & "and Kode_Unik_Transaksi = '" & Kode_Unik_Transaksi & "' "
            SQL = SQL & "group by kode_Perusahaan, No_Transaksi_GR2, No_Split, Qr_Code, Kode_Unik_Berjalan, Jenis, Kd_Barang, So_Awal, So_Tujuan, UserID "
            SQL = SQL & "order by no_Transaksi "
            Using Ds0 = BindingTrans(SQL)
                If Ds0.Tables("MyTable").Rows.Count <> 0 Then
                    For z As Integer = 0 To Ds0.Tables("MyTable").Rows.Count - 1

                        Dim NoTransaksiGR2 As String = Ds0.Tables("MyTable").Rows(z).Item("No_Transaksi_GR2")
                        Dim CurrentBarcode As String = $"{Ds0.Tables("MyTable").Rows(z).Item("Qr_Code")}-{Ds0.Tables("MyTable").Rows(z).Item("Kode_Unik_Berjalan")}"

                        '========================================
                        '=     CEK APAKAH SPLIT DI BATALKAN     =
                        '========================================
                        SQL = "select DISTINCT b.No_Split "
                        SQL &= $"from Emi_Production_Results_Validation a "
                        SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan  = b.Kode_Perusahaan and a.No_Transaksi  = b.No_Transaksi "
                        SQL &= $"inner join Barang_SN c on b.Kode_Perusahaan  = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number  "
                        SQL &= $"where a.Kode_Perusahaan  = '{KodePerusahaan}' "
                        SQL &= $"and a.Status is null "
                        SQL &= $"and b.Flag_Validasi_Loading = 'Y' "
                        SQL &= $"and a.No_Transaksi  = '{NoTransaksiGR2}' "
                        SQL &= $"and (c.Qr_Code+'-'+c.Kode_Unik_Berjalan) = '{CurrentBarcode}' "
                        Using Ds999 = BindingTrans(SQL)
                            If Ds999.Tables("MyTable").Rows.Count <> 0 Then
                                For k As Integer = 0 To Ds999.Tables("MyTable").Rows.Count - 1

                                    Dim CurrentSplit As String = Ds999.Tables("MyTable").Rows(k).Item("No_Split")

                                    SQL = "select top 1 a.Kode_Perusahaan from Emi_Split_Production_Order a, emi_production_results b "
                                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Production_Order "
                                    SQL = SQL & "and a.Status = 'Y' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & CurrentSplit & "'"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Gagal Simpan, Split Sudah Di Batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using
                                Next
                            End If
                        End Using

                        ' Dim Temp_NoTransksi As String = Ds0.Tables("MyTable").Rows(z).Item("No_Transaksi")
                        Dim Temp_NoTransksi_GR2 As String = Ds0.Tables("MyTable").Rows(z).Item("No_Transaksi_GR2")
                        'Temp_NoSplit = Ds0.Tables("MyTable").Rows(z).Item("No_Split")z
                        Dim Temp_Qr_Code As String = Ds0.Tables("MyTable").Rows(z).Item("Qr_Code")
                        Dim Temp_Kd_Unik_Berjalan As String = Ds0.Tables("MyTable").Rows(z).Item("Kode_Unik_Berjalan")
                        Dim Temp_Jenis As String = Ds0.Tables("MyTable").Rows(z).Item("Jenis")
                        Dim Temp_KdBarang As String = Ds0.Tables("MyTable").Rows(z).Item("Kd_Barang")
                        Dim Temp_So_Awal As String = Ds0.Tables("MyTable").Rows(z).Item("So_Awal")
                        Dim Temp_So_Tujuan As String = Ds0.Tables("MyTable").Rows(z).Item("So_Tujuan")
                        Dim Temp_Jumlah As String = Ds0.Tables("MyTable").Rows(z).Item("Jumlah")
                        Dim Temp_UserID As String = Ds0.Tables("MyTable").Rows(z).Item("UserID")


                        Dim newQrCode As String = ""
                        Dim Kode_Berjalan As String = ""

                        Dim KodeAsal As String = ""
                        Dim BatchNumber As String = ""
                        Dim Warna As String = ""

                        'SQL = "select top 1 b.No_Production_Order as No_Split, a.Qr_Code, a.Qr_Code, a.Kode_Unik_Berjalan, a.Kode_Unik_Asal, a.SN_Baru, a.Tgl_Expired, a.Tgl_Produksi, "
                        'SQL = SQL & "a.Kode_Unik_Asal, d.Jumlah as Stock_SN, a.Lokasi_Gudang as Kode_Stock_Owner, c.Kode_Barang, a.Batch_Number, a.Jenis, a.Jumlah, a.Satuan, d.Tgl_Masuk "
                        'SQL = SQL & "from Emi_Production_Results_Detail_Pallet a, Emi_Production_Results b, EMI_Production_Results_Detail_Barang c, Barang_SN d "
                        'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
                        'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                        'SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and a.Proses = c.Proses "
                        'SQL = SQL & "and a.SN_Baru = d.Serial_Number "
                        'SQL = SQL & "and b.Status is null "
                        'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        'SQL = SQL & "and b.No_Production_Order = '" & Txt_NoSplit.Text & "' "
                        'SQL = SQL & "and a.Qr_Code = '" & LvData_QR & "' "
                        'SQL = SQL & "and a.Kode_Unik_Berjalan = '" & LvData_KdUnikBerjalan & "' and d.jumlah<>0 "
                        'SQL = SQL & "order by a.Tgl_Expired "

                        'SQL = "select top 1 c.Tgl_Expired , a.No_Production_Order as No_Split, c.Qr_Code, c.Kode_Unik_Berjalan, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama as Nama_Barang, b.Jenis, c.Batch_Number, c.Kode_Unik_Asal, "
                        'SQL = SQL & "c.Tgl_Produksi, c.Tgl_Expired, a.UserID, c.Jumlah, b.Satuan, b.Warna, e.Keterangan as Kualitas, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, c.tgl_masuk "
                        'SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Barang_SN c, barang d, EMI_Master_Warna e "
                        'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
                        'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                        'SQL = SQL & "and b.kode_stock_owner_tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Tujuan = c.Serial_Number "
                        'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
                        'SQL = SQL & "and b.Warna = e.Kode_Warna "
                        'SQL = SQL & "and a.Status is null "
                        'SQL = SQL & "and b.Jenis = 'Finished Good' "
                        'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        'SQL = SQL & "and a.No_Production_Order = '" & Temp_NoSplit & "' "
                        'SQL = SQL & "and c.Qr_Code = '" & Temp_Qr_Code & "' "
                        'SQL = SQL & "and c.Kode_Unik_Berjalan = '" & Temp_Kd_Unik_Berjalan & "' "
                        'SQL = SQL & "and c.jumlah <> 0 "
                        'SQL = SQL & "order by c.Tgl_Expired ASC "

                        SQL = "select top 1 c.Tgl_Expired , a.No_Production_Order as No_Split, c.Qr_Code, c.Kode_Unik_Berjalan, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama as Nama_Barang, b.Jenis, c.Batch_Number, c.Kode_Unik_Asal, "
                        SQL &= $"c.Tgl_Produksi, c.Tgl_Expired, a.UserID, c.Jumlah, b.Satuan, b.Warna, e.Keterangan as Kualitas, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, c.tgl_masuk "
                        SQL &= $"from Emi_Production_Results_Validation a "
                        SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan  = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
                        SQL &= $"inner join Barang_SN c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number "
                        SQL &= $"inner join barang d on c.Kode_Perusahaan = d.Kode_Perusahaan  and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
                        SQL &= $"inner join EMI_Master_Warna e on b.Kode_Perusahaan = e.Kode_Perusahaan  and b.Warna = e.Kode_Warna  "
                        SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
                        SQL &= $"and a.Status is null "
                        SQL &= $"and b.Jenis = 'Finished Good' "
                        SQL &= $"and c.Jumlah <> 0 "
                        SQL &= $"and c.Qr_Code = '{Temp_Qr_Code}' "
                        SQL &= $"and c.Kode_Unik_Berjalan = '{Temp_Kd_Unik_Berjalan}' "
                        SQL &= $"order by c.Tgl_Expired ASC "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then

                                newQrCode = Generate_QR_Batch(Temp_KdBarang, Dr("Batch_Number"))
                                Kode_Berjalan = Generate_Random_Kode(10)
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


                        'TODO Inisital data
                        '===========================
                        '=     BARANG PRODUKSI     =
                        '===========================
                        Dim sisaPotong As Double = 0
                        Dim JumlahDipotong As Double = 0
                        'SQL = "select b.No_Production_Order as No_Split, a.Qr_Code, a.Qr_Code, a.Kode_Unik_Berjalan, a.Kode_Unik_Asal, a.SN_Baru, a.Tgl_Expired, a.Tgl_Produksi, "
                        'SQL = SQL & "a.Kode_Unik_Asal, d.Jumlah as Stock_SN, a.Lokasi_Gudang as Kode_Stock_Owner, c.Kode_Barang, a.Batch_Number, a.Jenis, a.Jumlah, a.Satuan, d.Tgl_Masuk "
                        'SQL = SQL & "from Emi_Production_Results_Detail_Pallet a, Emi_Production_Results b, EMI_Production_Results_Detail_Barang c, Barang_SN d "
                        'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
                        'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                        'SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and a.Proses = c.Proses "
                        'SQL = SQL & "and a.SN_Baru = d.Serial_Number "
                        'SQL = SQL & "and b.Status is null "
                        'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        'SQL = SQL & "and b.No_Production_Order = '" & Txt_NoSplit.Text & "' "
                        'SQL = SQL & "and a.Qr_Code = '" & LvData_QR & "' "
                        'SQL = SQL & "and a.Kode_Unik_Berjalan = '" & LvData_KdUnikBerjalan & "' and d.jumlah<>0 "
                        'SQL = SQL & "order by a.Tgl_Expired "

                        'SQL = "select c.Tgl_Expired , a.No_Production_Order as No_Split, c.Qr_Code, c.Kode_Unik_Berjalan, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama as Nama_Barang, b.Jenis, c.Batch_Number, c.Kode_Unik_Asal, b.Serial_Number_Tujuan, "
                        'SQL = SQL & "c.Tgl_Produksi, c.Tgl_Expired, a.UserID, c.Jumlah as Stock_SN, b.Satuan, b.Warna, e.Keterangan as Kualitas, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, c.tgl_masuk "
                        'SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Barang_SN c, barang d, EMI_Master_Warna e "
                        'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
                        'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                        'SQL = SQL & "and b.kode_stock_owner_tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Tujuan = c.Serial_Number "
                        'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
                        'SQL = SQL & "and b.Warna = e.Kode_Warna "
                        'SQL = SQL & "and a.Status is null "
                        'SQL = SQL & "and b.Jenis = 'Finished Good' "
                        'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        'SQL = SQL & "and a.No_Production_Order = '" & Temp_NoSplit & "' "
                        'SQL = SQL & "and c.Qr_Code = '" & Temp_Qr_Code & "' "
                        'SQL = SQL & "and c.Kode_Unik_Berjalan = '" & Temp_Kd_Unik_Berjalan & "' "
                        'SQL = SQL & "and c.jumlah <> 0 "
                        'SQL = SQL & "order by c.Tgl_Expired ASC "

                        SQL = "select c.Tgl_Expired , a.No_Production_Order as No_Split, c.Qr_Code, c.Kode_Unik_Berjalan, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama as Nama_Barang, b.Jenis, c.Batch_Number, c.Kode_Unik_Asal, b.Serial_Number_Akhir, "
                        SQL &= $"c.Tgl_Produksi, c.Tgl_Expired, a.UserID, c.Jumlah as Stock_SN, b.Satuan, b.Warna, e.Keterangan as Kualitas, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, c.tgl_masuk "
                        SQL &= $"from Emi_Production_Results_Validation a "
                        SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan  = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
                        SQL &= $"inner join Barang_SN c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number "
                        SQL &= $"inner join barang d on c.Kode_Perusahaan = d.Kode_Perusahaan  and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
                        SQL &= $"inner join EMI_Master_Warna e on b.Kode_Perusahaan = e.Kode_Perusahaan  and b.Warna = e.Kode_Warna  "
                        SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
                        SQL &= $"and a.Status is null "
                        SQL &= $"and b.Jenis = 'Finished Good' "
                        SQL &= $"and c.Jumlah <> 0 "
                        SQL &= $"and c.Qr_Code = '{Temp_Qr_Code}' "
                        SQL &= $"and c.Kode_Unik_Berjalan = '{Temp_Kd_Unik_Berjalan}' "
                        SQL &= $"order by c.Tgl_Expired ASC "
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    sisaPotong = Val(HilangkanTanda(Temp_Jumlah))
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


                                        If sisaPotong < Val(HilangkanTanda(.Rows(j).Item("Stock_SN"))) Or sisaPotong = Val(HilangkanTanda(.Rows(j).Item("Stock_SN"))) Then

                                            If Temp_Jenis.ToUpper = "FINISHED GOOD" Or Temp_Jenis.ToUpper = "REJECTED" Then
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
                                            SQL = SQL & "kode_stock_owner = '" & .Rows(j).Item("Kode_Stock_Owner_Tujuan") & "' and kode_barang = '" & .Rows(j).Item("Kode_Barang") & "' "
                                            ExecuteTrans(SQL)

                                            SQL = "Update barang_sn set jumlah = jumlah - " & HilangkanTanda(sisaPotong) & ",  "
                                            SQL = SQL & "Jumlah_Bags=Jumlah_Bags-" & HilangkanTanda(sisaPotong) & " where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & .Rows(j).Item("Kode_Stock_Owner_Tujuan") & "' and "
                                            SQL = SQL & "kode_barang = '" & .Rows(j).Item("Kode_Barang") & "' and "
                                            SQL = SQL & "serial_number = '" & .Rows(j).Item("Serial_Number_Akhir") & "'"
                                            ExecuteTrans(SQL)

                                            JumlahKurang = sisaPotong
                                            JumlahDipotong += sisaPotong
                                            sisaPotong = 0

                                        ElseIf sisaPotong > Val(HilangkanTanda(.Rows(j).Item("Stock_SN"))) Then

                                            If Temp_Jenis.ToUpper = "FINISHED GOOD" Or Temp_Jenis.ToUpper = "REJECTED" Then
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
                                            SQL = SQL & "kode_stock_owner = '" & .Rows(j).Item("Kode_Stock_Owner_Tujuan") & "' and kode_barang = '" & .Rows(j).Item("Kode_Barang") & "' "
                                            ExecuteTrans(SQL)

                                            SQL = "Update barang_sn set jumlah = jumlah - " & Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4"))) & ",  "
                                            SQL = SQL & "Jumlah_Bags=Jumlah_Bags - " & Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4"))) & " "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & .Rows(j).Item("Kode_Stock_Owner_Tujuan") & "' and "
                                            SQL = SQL & "kode_barang = '" & .Rows(j).Item("kode_barang") & "' and "
                                            SQL = SQL & "serial_number = '" & .Rows(j).Item("Serial_Number_Akhir") & "'"
                                            ExecuteTrans(SQL)

                                            JumlahKurang = Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4")))
                                            JumlahDipotong += Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4")))
                                            sisaPotong = sisaPotong - Val(HilangkanTanda(Format(.Rows(j).Item("Stock_SN"), "N4")))
                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang " & Temp_KdBarang & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        Dim KualitasBarang As String = ""
                                        If Temp_Jenis.ToUpper = "REJECTED" Then
                                            KualitasBarang = "MERAH"
                                        Else
                                            KualitasBarang = "HIJAU"
                                        End If




                                        SumHPPAwal += Get_Harga_SN(.Rows(j).Item("Serial_Number_Akhir")) * JumlahKurang

                                        Dim Nilai_Packaging As Double = 0

                                        '=========================
                                        '=     INSERT DETAIL     =
                                        '=========================
                                        SQL = "insert into N_EMI_Validation_GR_3_Detail "
                                        SQL = SQL & "(Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner_Awal, Kode_Stock_Owner_Tujuan, Kode_Barang, "
                                        SQL = SQL & "Serial_Number_Awal, Serial_Number_Tujuan, Batch_Number, Warna, Jumlah, Satuan, Jenis, No_Transaksi_GR2, jumlah_awal) "
                                        SQL = SQL & "values ('" & KodePerusahaan & "', '" & Temp_NoTransksi & "', '" & Temp_So_Awal & "', '" & Temp_So_Tujuan & "', "
                                        SQL = SQL & "'" & Temp_KdBarang & "', '" & .Rows(j).Item("Serial_Number_Akhir") & "', "
                                        SQL = SQL & "NULL, '" & .Rows(j).Item("Batch_Number") & "', '" & KualitasBarang & "', '" & HilangkanTanda(JumlahInsert) & "', "
                                        SQL = SQL & "'" & Satuan & "', '" & Temp_Jenis & "', '" & Temp_NoTransksi_GR2 & "', '" & HilangkanTanda(JumlahKurang) & "')"
                                        ExecuteTrans(SQL)

                                        '==========================
                                        '=     GET URUT DETAiL     =
                                        '==========================
                                        Dim NoUrut_DetailResult As Integer = 0
                                        SQL = "select IDENT_CURRENT('N_EMI_Validation_GR_3_Detail') as urutan"
                                        Using Dr = OpenTrans(SQL)
                                            If Dr.Read Then
                                                NoUrut_DetailResult = Dr("urutan")
                                            End If
                                        End Using

                                        '===================================
                                        '=     GET KODE BARANG INQUIRY     =
                                        '===================================
                                        Dim kd_inq As String = ""
                                        SQL = "select top(1) kode_barang_inq from barang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_barang = '" & Temp_KdBarang & "'  "
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

                                        '====================================
                                        '=     CEK APAKAH SCRAP ATAU FG     =
                                        '====================================
                                        If Temp_Jenis.ToUpper = "FINISHED GOOD" Then


#Region "KODE FG KOMEN"

                                            ''==================================
                                            ''=     POTONG STOCK PACKAGING     =
                                            ''==================================
                                            '' Get Data Packing Yang Digunakan
                                            'SQL = "select a.No_Transaksi, isnull((  "
                                            'SQL = SQL & "select z.kode_barang_inq from barang z where a.kode_perusahaan = z.kode_perusahaan "
                                            'SQL = SQL & "and a.kode_stock_owner = z.kode_stock_owner and a.kode_barang = z.kode_barang ), '-') as Kode_Barang, "
                                            'SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang as Kode_Bahan, c.Nama ,b.Jumlah, b.Satuan, c.flag_potong_stok, "
                                            'SQL = SQL & "isnull(c.standar_price,0) as standar_price, isnull(c.Flag_Pembulatan_Produksi,'T') as Flag_Pembulatan_Produksi "
                                            'SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
                                            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
                                            'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
                                            'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & Txt_NoSplit.Text & "' "
                                            'SQL = SQL & "and a.Status is null "
                                            'SQL = SQL & "and b.Kode_Barang not in ( "
                                            'SQL = SQL & "select z.Kode_Bahan from Barang_Detail_Bahan_Penolong z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Barang = '" & kd_inq & "' and jenis = 'KEMASAN UTAMA') "
                                            'SQL = SQL & "order by c.nama "
                                            'Using Ds1 = BindingTrans(SQL)
                                            '    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                            '        For k As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                            '            '================================
                                            '            '=     GET JUMLAH KEBUTUHAN     =
                                            '            '================================
                                            '            Dim KebutuhanBarang As Double = 0
                                            '            Dim KebutuhanBahan As Double = 0
                                            '            SQL = "select a.Kode_Bahan, a.Jumlah_Bahan, a.Jumlah_Barang "
                                            '            SQL = SQL & "from Barang_Detail_Bahan_Penolong a "
                                            '            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                            '            SQL = SQL & "and Kode_Barang = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Barang") & "' "
                                            '            SQL = SQL & "and Kode_Bahan = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "' "
                                            '            Using Dr = OpenTrans(SQL)
                                            '                If Dr.Read Then
                                            '                    KebutuhanBarang = Val(HilangkanTanda(Format(Dr("Jumlah_Barang"), "N4")))
                                            '                    KebutuhanBahan = Val(HilangkanTanda(Format(Dr("Jumlah_Bahan"), "N4")))
                                            '                End If
                                            '            End Using

                                            '            Dim PackagingDigunakan As Double = Val(HilangkanTanda(Format((Val(HilangkanTanda(JumlahInsert)) / KebutuhanBarang) * KebutuhanBahan, "N4")))

                                            '            If Ds1.Tables("MyTable").Rows(k).Item("Flag_Pembulatan_Produksi") = "Y" Then
                                            '                PackagingDigunakan = Math.Ceiling(PackagingDigunakan)
                                            '            End If

                                            '            '=========================================
                                            '            '=     INSERT TABLE PACKAGING DETAIL     =
                                            '            '=========================================
                                            '            SQL = "insert into Emi_Production_Results_Validation_Packaging_Detail (Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner, Kode_Barang, "
                                            '            SQL = SQL & "Jumlah, Satuan, Urut_Detail) "
                                            '            SQL = SQL & "values ('" & KodePerusahaan & "', '" & Temp_NoTransksi.Trim & "', '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner") & "', '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "', "
                                            '            SQL = SQL & "'" & HilangkanTanda(PackagingDigunakan) & "', "
                                            '            SQL = SQL & "'" & Ds1.Tables("MyTable").Rows(k).Item("Satuan") & "', '" & NoUrut_DetailResult & "')"
                                            '            ExecuteTrans(SQL)

                                            '            '====================================
                                            '            '=     GET URUT PACKAGING DETAL     =
                                            '            '====================================
                                            '            Dim NoUrut_PackagingDetail As Integer = 0
                                            '            SQL = "select IDENT_CURRENT('Emi_Production_Results_Validation_Packaging_Detail') as urutan"
                                            '            Using Dr = OpenTrans(SQL)
                                            '                If Dr.Read Then
                                            '                    NoUrut_PackagingDetail = Dr("urutan")
                                            '                End If
                                            '            End Using

                                            '            '===============================
                                            '            '=     POTONG STOCK BARANG     =
                                            '            '===============================
                                            '            SQL = "select round(good_stock,4) as good_stock "
                                            '            SQL = SQL & "from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                            '            SQL = SQL & "and Kode_Stock_Owner = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "' "
                                            '            Using Ds2 = BindingTrans(SQL)
                                            '                If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                            '                    If (Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("good_stock"))) - PackagingDigunakan) < BolehNegatif Then
                                            '                        CloseTrans()
                                            '                        CloseConn()
                                            '                        MessageBox.Show("Proses akan membuat stock menjadi negatif untuk kode barang " & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            '                        Exit Sub
                                            '                    Else
                                            '                        SQL = "Update barang set good_stock = good_stock - " & PackagingDigunakan & " where "
                                            '                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            '                        SQL = SQL & "kode_stock_owner = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner") & "' and "
                                            '                        SQL = SQL & "kode_barang = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "'"
                                            '                        ExecuteTrans(SQL)
                                            '                    End If
                                            '                Else
                                            '                    CloseTrans()
                                            '                    CloseConn()
                                            '                    MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                            '                    Exit Sub
                                            '                End If
                                            '            End Using

                                            '            '===============================
                                            '            '=     CEK STOCK BARANG SN     =
                                            '            '===============================
                                            '            Dim StockKurang As Boolean = False
                                            '            SQL = "select isnull(round(sum(jumlah),4), 0) as stock from barang_sn where "
                                            '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            '            SQL = SQL & "kode_stock_owner = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner") & "' and "
                                            '            SQL = SQL & "kode_barang = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "' and jumlah <> 0 "
                                            '            Using Dr = OpenTrans(SQL)
                                            '                If Dr.Read Then
                                            '                    If Dr("stock") < Val(PackagingDigunakan) Then
                                            '                        StockKurang = True
                                            '                    Else
                                            '                        StockKurang = False
                                            '                    End If
                                            '                Else
                                            '                    Dr.Close()
                                            '                    CloseTrans()
                                            '                    CloseConn()
                                            '                    MessageBox.Show("Terjadi Kesalahan Barang Sn untuk kode barang " & .Rows(j).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            '                    Exit Sub
                                            '                End If
                                            '            End Using

                                            '            '==================================
                                            '            '=     POTONG STOCK BARANG SN     =
                                            '            '==================================
                                            '            If Not StockKurang Then
                                            '                Dim sisa As Double = 0
                                            '                Dim JumlahPotong As Double = 0
                                            '                SQL = "select kode_stock_owner, kode_barang, serial_number, dbo.get_hpp(Serial_Number) as HPP, round(jumlah,4) as jumlah from barang_sn where "
                                            '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            '                SQL = SQL & "kode_stock_owner = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner") & "' and "
                                            '                SQL = SQL & "kode_barang = '" & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "' and jumlah <> 0 "
                                            '                SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                                            '                Using Ds2 = BindingTrans(SQL)
                                            '                    If Ds2.Tables("Mytable").Rows.Count <> 0 Then
                                            '                        sisa = Val(PackagingDigunakan)

                                            '                        For l As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                                            '                            If sisa = 0 Then
                                            '                                Exit For
                                            '                            ElseIf sisa < 0 Then
                                            '                                CloseTrans()
                                            '                                CloseConn()
                                            '                                MessageBox.Show("Terdapat Kesalahan saat Potong Barang SN", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            '                                Exit Sub
                                            '                            End If

                                            '                            Dim HppPackaging As Double = Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(l).Item("HPP")))

                                            '                            If sisa < Val(Ds2.Tables("MyTable").Rows(l).Item("jumlah")) Or sisa = Val(Ds2.Tables("MyTable").Rows(l).Item("jumlah")) Then
                                            '                                SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                            '                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            '                                SQL = SQL & "kode_stock_owner = '" & Ds2.Tables("MyTable").Rows(l).Item("kode_stock_owner") & "' and "
                                            '                                SQL = SQL & "kode_barang = '" & Ds2.Tables("MyTable").Rows(l).Item("kode_barang") & "' and "
                                            '                                SQL = SQL & "serial_number = '" & Ds2.Tables("MyTable").Rows(l).Item("serial_number") & "'"
                                            '                                ExecuteTrans(SQL)

                                            '                                SQL = "INSERT INTO Emi_Production_Results_Validation_Packaging_Det(Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner, Kode_Barang,"
                                            '                                SQL = SQL & "Jumlah, Serial_Number, no_urut_detail) VALUES('" & KodePerusahaan & "','" & Temp_NoTransksi & "',"
                                            '                                SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(l).Item("kode_stock_owner") & "','" & Ds2.Tables("MyTable").Rows(l).Item("kode_barang") & "',"
                                            '                                SQL = SQL & "" & sisa & ",'" & Ds2.Tables("MyTable").Rows(l).Item("serial_number") & "', '" & NoUrut_PackagingDetail & "')"
                                            '                                ExecuteTrans(SQL)

                                            '                                Nilai_Packaging = Nilai_Packaging + (HppPackaging * sisa)
                                            '                                JumlahPotong += sisa
                                            '                                sisa = 0
                                            '                            ElseIf sisa > Val(Ds2.Tables("MyTable").Rows(l).Item("jumlah")) Then
                                            '                                SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                            '                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            '                                SQL = SQL & "kode_stock_owner = '" & Ds2.Tables("MyTable").Rows(l).Item("kode_stock_owner") & "' and "
                                            '                                SQL = SQL & "kode_barang = '" & Ds2.Tables("MyTable").Rows(l).Item("kode_barang") & "' and "
                                            '                                SQL = SQL & "serial_number = '" & Ds2.Tables("MyTable").Rows(l).Item("serial_number") & "'"
                                            '                                ExecuteTrans(SQL)

                                            '                                SQL = "INSERT INTO Emi_Production_Results_Validation_Packaging_Det(Kode_Perusahaan, No_Transaksi, Kode_Stock_Owner, Kode_Barang,"
                                            '                                SQL = SQL & "Jumlah, Serial_Number, no_urut_detail) VALUES('" & KodePerusahaan & "','" & Temp_NoTransksi & "',"
                                            '                                SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(l).Item("kode_stock_owner") & "','" & Ds2.Tables("MyTable").Rows(l).Item("kode_barang") & "',"
                                            '                                SQL = SQL & "" & Ds2.Tables("MyTable").Rows(l).Item("jumlah") & ",'" & Ds2.Tables("MyTable").Rows(l).Item("serial_number") & "', '" & NoUrut_PackagingDetail & "')"
                                            '                                ExecuteTrans(SQL)

                                            '                                Nilai_Packaging = Nilai_Packaging + (HppPackaging * Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(l).Item("jumlah"), "N4"))))
                                            '                                JumlahPotong += Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(l).Item("jumlah"), "N4")))
                                            '                                sisa = sisa - Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(l).Item("jumlah"), "N4")))
                                            '                            Else
                                            '                                CloseTrans()
                                            '                                CloseConn()
                                            '                                MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang " & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            '                                Exit Sub
                                            '                            End If

                                            '                            If Math.Round(sisa, 4) <> 0 And l = Ds2.Tables("MyTable").Rows.Count - 1 Then
                                            '                                CloseTrans()
                                            '                                CloseConn()
                                            '                                MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & Ds1.Tables("MyTable").Rows(k).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            '                                Exit Sub
                                            '                            End If
                                            '                        Next
                                            '                    End If
                                            '                End Using

                                            '                '================================================
                                            '                '=     CEK KESESUAIAN JUMLAH YANG DI POTONG     =
                                            '                '================================================
                                            '                If Val(HilangkanTanda(Format(JumlahPotong, "N4"))) <> Val(HilangkanTanda(Format(PackagingDigunakan, "N4"))) Then
                                            '                    CloseTrans()
                                            '                    CloseConn()
                                            '                    MessageBox.Show("Terjadi Kesalahan Saat Memotong Stock Packaging!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            '                    Exit Sub
                                            '                End If
                                            '            Else
                                            '                CloseTrans()
                                            '                CloseConn()
                                            '                MessageBox.Show("Terjadi Kesalahan Pada Barang SN untuk kode barang " & .Rows(j).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            '                Exit Sub

                                            '            End If

                                            '        Next
                                            '    End If
                                            'End Using
#End Region

                                        End If

                                        '============================
                                        '=     PENAMBAHAN STOCK     =
                                        '============================


                                        sumPackaging += Nilai_Packaging

                                        ' GENEREATE SN BARU
                                        Dim hppSekarang As Double = Get_Harga_SN(.Rows(j).Item("Serial_Number_Akhir")) * JumlahKurang / JumlahInsert

                                        If JumlahInsert = 0 Then

                                        End If

                                        Dim HppBaru As Double = hppSekarang + (Math.Round(Nilai_Packaging / JumlahInsert, 0))

                                        Dim idgroup_jenis As String = ""
                                        Dim Nmgroup_jenis As String = ""
                                        Dim kode_akun As String = ""
                                        SQL = "select a.id_group_jenis, Akun_Persediaan, Kode_Group_Jenis "
                                        SQL = SQL & "from emi_group_jenis a, barang b, emi_group_jenis_akun c "
                                        SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and a.id_group_jenis = b.id_group_jenis and b.kode_barang='" & Temp_KdBarang & "' "
                                        SQL = SQL & "and a.id_group_jenis = c.id_group_jenis and b.kode_stock_owner='" & Temp_So_Tujuan & "' "
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

                                            If idgroup_jenis = ArrHPP_GroupJenisID.Item(ind) And Temp_So_Tujuan = ArrHPP_Lokasi.Item(ind) Then
                                                ada_data = True

                                                ArrHPP_Nilai.Item(ind) += (HppBaru * JumlahInsert)
                                            End If

                                        Next

                                        If ada_data = False Then
                                            ArrHPP_GroupJenisID.Add(idgroup_jenis)
                                            ArrHPP_GroupJenisNm.Add(Nmgroup_jenis)
                                            ArrHPP_Lokasi.Add(Temp_So_Tujuan)
                                            ArrHPP_Akun.Add(kode_akun)
                                            ArrHPP_Nilai.Add(HppBaru * JumlahInsert)
                                        End If

                                        Dim Str As String = Format(random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                                        Dim Kode_Unik As String = Str.Substring(0, 5) & "BB" & Chr(64 + Str.Substring(6, 1)) & Str.Substring(6, Len(Str) - 6)
                                        Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & HppBaru & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                                        '===========================================
                                        '=     GET WAREHOUSE DAN PALLET KOSONG     =
                                        '===========================================
                                        Dim available_Id_Warehouse As String = ""
                                        Dim available_NoPallet As String = ""
                                        SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                                        SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                                        SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                                        SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                                        SQL = SQL & "and a.Kode_Stock_Owner='" & Temp_So_Tujuan & "' and b.Kode_Barang is null"
                                        Using Dr2 = OpenTrans(SQL)
                                            Do While Dr2.Read
                                                available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                                                available_NoPallet = Dr2("nomor_urut")
                                            Loop
                                        End Using

                                        '============================
                                        '=     INSERT BARANG SN     =
                                        '============================

                                        If Temp_Jenis.ToUpper = "FINISHED GOOD" Then

                                            SQL = "insert into barang_sn_sementara(kode_perusahaan, kode_stock_owner, kode_barang, "
                                            SQL = SQL & "serial_number, Jumlah, Jumlah_Bags, Warna, Kode_Unik_Berjalan, Kode_Unik_Asal, "
                                            SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet, Flag_Produksi, Flag_QI, Tgl_Produksi, Tgl_Expired, Tgl_masuk, Blok_SN) values('" & KodePerusahaan & "', "
                                            SQL = SQL & "'" & Temp_So_Tujuan & "', '" & Temp_KdBarang & "', "
                                            SQL = SQL & "'" & SN_Baru & "', " & HilangkanTanda(JumlahInsert) & ", " & HilangkanTanda(JumlahInsert) & ", '" & KualitasBarang & "', "
                                            SQL = SQL & "'" & Kode_Berjalan & "', '" & .Rows(j).Item("Kode_Unik_Asal") & "-" & Kode_Berjalan & "', '" & newQrCode & "', "
                                            SQL = SQL & "'" & .Rows(j).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', 'Y', 'Y', "
                                            SQL = SQL & " '" & .Rows(j).Item("Tgl_Produksi") & "', '" & .Rows(j).Item("Tgl_Expired") & "', '" & .Rows(j).Item("Tgl_Masuk") & "', 'Y')"
                                            ExecuteTrans(SQL)
                                        Else

                                            Dim jumlah_bags As Double = 0

                                            If Temp_Jenis.ToUpper = "REJECTED" Then
                                                jumlah_bags = JumlahInsert
                                            End If


                                            SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah, Jumlah_Bags, Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, "
                                            SQL = SQL & "Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk) "
                                            SQL = SQL & "values('" & KodePerusahaan & "', '" & Temp_So_Tujuan & "', '" & Temp_KdBarang & "', '" & SN_Baru & "', "
                                            SQL = SQL & "'" & HilangkanTanda(JumlahInsert) & "', '" & jumlah_bags & "', '" & .Rows(j).Item("Tgl_Expired") & "', '" & .Rows(j).Item("Tgl_Produksi") & "', 0, 0, "
                                            SQL = SQL & "'" & available_Id_Warehouse & "', '" & newQrCode & "', '" & Kode_Berjalan & "', '" & .Rows(j).Item("Kode_Unik_Asal") & "-" & Kode_Berjalan & "', '" & available_NoPallet & "', "
                                            SQL = SQL & "'" & .Rows(j).Item("Batch_Number") & "', '" & KualitasBarang & "', '" & .Rows(j).Item("Tgl_Masuk") & "')"
                                            ExecuteTrans(SQL)

                                            '=========================
                                            '=     INSERT BARANG     =
                                            '=========================
                                            SQL = "Update barang set "
                                            SQL = SQL & "Good_Stock = Good_Stock + " & HilangkanTanda(JumlahInsert) & " ,  Jumlah_Bags=Jumlah_Bags+" & jumlah_bags & " "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & Temp_So_Tujuan & "' and kode_barang = '" & Temp_KdBarang & "'"
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
                                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' And a.Kode_Stock_Owner = '" & Temp_So_Tujuan & "' AND a.Kode_Barang = '" & Temp_KdBarang & "' "
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
                                        SQL = "update N_EMI_Validation_GR_3_Detail set Serial_Number_Tujuan = '" & SN_Baru & "' "
                                        SQL = SQL & "where No_Transaksi = '" & Temp_NoTransksi.Trim & "' and Urut = '" & NoUrut_DetailResult & "'"
                                        ExecuteTrans(SQL)

                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesalahan Pada Barang " & Temp_KdBarang & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using




                        'TODO : INsert
                        '================================================
                        '=     CEK KESESUAIAN JUMLAH YANG DI POTONG     =
                        '================================================
                        If Val(HilangkanTanda(JumlahDipotong)) <> Val(HilangkanTanda(Temp_Jumlah)) Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan Saat Memotong Stock Barang Produksi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        lokasi_awal = Temp_So_Awal


                        '==================
                        '=     JURNAL     =
                        '==================
                        Dim Top1NoSplitGR2 As String = ""
                        ' GET TOP 1 NO SPLIT
                        SQL = "select top 1 b.No_Split "
                        SQL &= $"from Emi_Production_Results_Validation a "
                        SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi  "
                        SQL &= $"inner join barang_sn c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number  "
                        SQL &= $"where a.Kode_Perusahaan ='{KodePerusahaan}' "
                        SQL &= $"and a.Status is null "
                        SQL &= $"and a.No_Transaksi = '{Temp_NoTransksi_GR2}' "
                        SQL &= $"and (c.Qr_Code+'-'+c.Kode_Unik_Berjalan) = '{Temp_Qr_Code}-{Temp_Kd_Unik_Berjalan}' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Top1NoSplitGR2 = Dr("No_Split")
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan, No Split Pada GR2 Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

#Region "JURNAL"

                        Dim inisial_faktur_dari As String = ""
                        Dim fso As String = ""
                        Dim Kode_Barang As String = ""
                        SQL = "Select b.Inisial_Faktur,a.Kode_Stock_Owner, kode_barang from Emi_Split_Production_Order a,Stock_Owner_Gudang b "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                        SQL = SQL & "And a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Top1NoSplitGR2 & "' "
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

                        Dim keterangan2 As String = ""
                        Dim keterangan3 As String = ""

                        SQL = "select HPP_Barang_Setengah_Jadi, Persediaan_Barang_Dalam_Proses, "
                        SQL = SQL & "HPP,Pembulatan_Finished_Good,Pembulatan_Semi_FG, Persediaan_Scrap from stock_owner_gudang "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then

                                akun_HPP_FG = Dr("HPP")
                                keterangan2 = "HPP Barang Jadi "

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
                        SQL = SQL & "and a.no_faktur = '" & Top1NoSplitGR2 & "' "
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
                        SQL = SQL & "'" & KodeProyek & "', 'HPP Barang Jadi " & Temp_NoTransksi & "', '', "
                        SQL = SQL & "'-', '" & UserID & "')"
                        ExecuteTrans(SQL)

                        'Insert HPP Total
                        Dim total_hpp As Double = 0
                        For index = 0 To ArrHPP_Akun.Count - 1

                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(ArrHPP_Akun.Item(index), 1),
                                Strings.Mid(ArrHPP_Akun.Item(index), 2, 1),
                                Strings.Mid(Ganti(ArrHPP_Akun.Item(index)), 3),
                                KodePerusahaan, KodeProyek, "Persediaan " & ArrHPP_GroupJenisNm.Item(index) & "; " & Temp_NoTransksi, ArrHPP_Nilai(index), "0", pagenumber, ArrHPP_Lokasi.Item(index), Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                            total_hpp += ArrHPP_Nilai(index)
                        Next

                        'Insert Data Bahan dan Packaging yg dipakai
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Persediaan, 1),
                      Strings.Mid(Akun_Persediaan, 2, 1),
                      Strings.Mid(Ganti(Akun_Persediaan), 3),
                      KodePerusahaan, KodeProyek, keterangan & "; " & Temp_NoTransksi, "0", SumHPPAwal, pagenumber, lokasi_awal, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                        If sumPackaging <> 0 Then
                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_kredit_packaging, 1),
                      Strings.Mid(akun_kredit_packaging, 2, 1),
                      Strings.Mid(Ganti(akun_kredit_packaging), 3),
                      KodePerusahaan, KodeProyek, ket_packaging & "; " & Temp_NoTransksi, "0", sumPackaging, pagenumber, fso, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1
                        End If

                        Dim selisih_pembulatan As Double = total_hpp - (sumPackaging + SumHPPAwal)

                        If selisih_pembulatan > 0 Then
                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Pembulatan_FG, 1),
                      Strings.Mid(Akun_Pembulatan_FG, 2, 1),
                      Strings.Mid(Ganti(Akun_Pembulatan_FG), 3),
                      KodePerusahaan, KodeProyek, "Selisih HPP Barang Jadi " & Temp_NoTransksi, "0", selisih_pembulatan, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1
                        Else
                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Pembulatan_FG, 1),
                      Strings.Mid(Akun_Pembulatan_FG, 2, 1),
                      Strings.Mid(Ganti(Akun_Pembulatan_FG), 3),
                      KodePerusahaan, KodeProyek, "Selisih HPP Barang Jadi " & Temp_NoTransksi, Math.Abs(selisih_pembulatan), "0", pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
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

                        '    Dim Kode_voucher2 As String = ""
                        '    Kode_voucher2 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                        '    Dim pagenumber2 As Integer = 1

                        '    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                        '    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                        '    SQL = SQL & "'" & Kode_voucher2 & "', "
                        '    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                        '    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                        '    SQL = SQL & "'" & KodeProyek & "', 'Persediaan Barang Jadi " & Temp_NoTransksi & "', '', "
                        '    SQL = SQL & "'-', '" & UserID & "')"
                        '    ExecuteTrans(SQL)

                        '    'Insert HPP Total

                        '    Dim total_hpp As Double = 0
                        '    For index = 0 To ArrHPP_Akun.Count - 1

                        '        SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(ArrHPP_Akun.Item(index), 1),
                        'Strings.Mid(ArrHPP_Akun.Item(index), 2, 1),
                        'Strings.Mid(Ganti(ArrHPP_Akun.Item(index)), 3),
                        'KodePerusahaan, KodeProyek, "Persediaan " & ArrHPP_GroupJenisNm.Item(index) & "; " & Temp_NoTransksi, ArrHPP_Nilai(index), "0", pagenumber2, ArrHPP_Lokasi.Item(index), Bahasa_Pilihan, Ket_Cost_Center_HO)
                        '        ExecuteTrans(SQL)
                        '        pagenumber2 = pagenumber2 + 1

                        '        total_hpp += ArrHPP_Nilai(index)
                        '    Next

                        '    'Insert Data Bahan dan Packaging yg dipakai
                        '    SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_HPP_FG, 1),
                        '              Strings.Mid(akun_HPP_FG, 2, 1),
                        '              Strings.Mid(Ganti(akun_HPP_FG), 3),
                        '              KodePerusahaan, KodeProyek, "HPP Barang Jadi " & Temp_NoTransksi, "0", sumPackaging + SumHPPAwal, pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        '    ExecuteTrans(SQL)
                        '    pagenumber2 = pagenumber2 + 1

                        '    Dim selisih_pembulatan As Double = total_hpp - (sumPackaging + SumHPPAwal)

                        '    If selisih_pembulatan > 0 Then
                        '        SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(Akun_Pembulatan_FG, 1),
                        '              Strings.Mid(Akun_Pembulatan_FG, 2, 1),
                        '              Strings.Mid(Ganti(Akun_Pembulatan_FG), 3),
                        '              KodePerusahaan, KodeProyek, "Selisih HPP Barang Jadi " & Temp_NoTransksi, "0", selisih_pembulatan, pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        '        ExecuteTrans(SQL)
                        '        pagenumber2 = pagenumber2 + 1
                        '    Else
                        '        SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(Akun_Pembulatan_FG, 1),
                        '              Strings.Mid(Akun_Pembulatan_FG, 2, 1),
                        '              Strings.Mid(Ganti(Akun_Pembulatan_FG), 3),
                        '              KodePerusahaan, KodeProyek, "Selisih HPP Barang Jadi " & Temp_NoTransksi, Math.Abs(selisih_pembulatan), "0", pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        '        ExecuteTrans(SQL)
                        '        pagenumber2 = pagenumber2 + 1
                        '    End If

                        '    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                        '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        '    SQL = SQL & "kode_voucher = '" & Kode_voucher2 & "'"
                        '    Using Dr = OpenTrans(SQL)
                        '        If Dr.Read Then
                        '            If Dr("debit") <> Dr("kredit") Then
                        '                Dr.Close()
                        '                CloseTrans()
                        '                CloseConn()
                        '                MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '                Exit Sub
                        '            End If
                        '        Else
                        '            Dr.Close()
                        '            CloseTrans()
                        '            CloseConn()
                        '            MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '            Exit Sub
                        '        End If
                        '    End Using

#End Region

                        'SQL = "update N_EMI_Validation_GR_3_Detail set Kode_Voucher1 ='" & Kode_voucher & "' " ', Kode_Voucher2=' " & Kode_voucher2 & "' "
                        'SQL = SQL & "where no_transaksi='" & Temp_NoTransksi & "' and kode_perusahaan='" & KodePerusahaan & "' "
                        'ExecuteTrans(SQL)


                        'SQL = $"update N_EMI_Validation_GR_3_Detail set Kode_Voucher1 = '{Kode_voucher}' "
                        'SQL &= $"where no_transaksi='{Temp_NoTransksi}' and kode_perusahaan='{KodePerusahaan}' "
                        'SQL &= $"and Serial_Number_Awal in ( "
                        'SQL &= $"select b.Serial_Number_Akhir "
                        'SQL &= $"from Emi_Production_Results_Validation a "
                        'SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi  "
                        'SQL &= $"inner join Barang_SN c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number   "
                        'SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
                        'SQL &= $"and a.Status is null "
                        'SQL &= $"and b.Jenis = 'Finished Good' "
                        'SQL &= $"and c.Jumlah <> 0 "
                        'SQL &= $"and c.Qr_Code = '{Temp_Qr_Code}' "
                        'SQL &= $"and c.Kode_Unik_Berjalan = '{Temp_Kd_Unik_Berjalan}') "
                        'ExecuteTrans(SQL)




                        SQL = $"
                            Select 1
                            from N_EMI_Validation_GR_3_Detail z
	                            inner join Barang_SN x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Stock_Owner_Awal = x.Kode_Stock_Owner and z.Serial_Number_Awal = x.Serial_Number 
                            where z.no_transaksi='{Temp_NoTransksi}' 
                            and z.kode_perusahaan='{KodePerusahaan}' 
                            and (x.Qr_Code+'-'+x.Kode_Unik_Berjalan) in (
	                            select distinct (c.Qr_Code+'-'+c.Kode_Unik_Berjalan) --, a.no_transaksi
	                            from Emi_Production_Results_Validation a
		                            inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi 
		                            inner join Barang_SN c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number  
	                            where a.Kode_Perusahaan = '{KodePerusahaan}'
	                            and a.Status is null
	                            and b.Jenis = 'Finished Good'
	                            and c.Jumlah <> 0
	                            and c.Qr_Code = '{Temp_Qr_Code}'
	                            and c.Kode_Unik_Berjalan = '{Temp_Kd_Unik_Berjalan}'
                            )
                        "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then

                                Dr.Close()
                                SQL = $"
                                    update z set z.Kode_Voucher1 = '{Kode_voucher}'
                                    from N_EMI_Validation_GR_3_Detail z
	                                    inner join Barang_SN x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Stock_Owner_Awal = x.Kode_Stock_Owner and z.Serial_Number_Awal = x.Serial_Number 
                                    where z.no_transaksi='{Temp_NoTransksi}' 
                                    and z.kode_perusahaan='{KodePerusahaan}' 
                                    and (x.Qr_Code+'-'+x.Kode_Unik_Berjalan) in (
	                                    select distinct (c.Qr_Code+'-'+c.Kode_Unik_Berjalan) --, a.no_transaksi
	                                    from Emi_Production_Results_Validation a
		                                    inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi 
		                                    inner join Barang_SN c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number  
	                                    where a.Kode_Perusahaan = '{KodePerusahaan}'
	                                    and a.Status is null
	                                    and b.Jenis = 'Finished Good'
	                                    and c.Jumlah <> 0
	                                    and c.Qr_Code = '{Temp_Qr_Code}'
	                                    and c.Kode_Unik_Berjalan = '{Temp_Kd_Unik_Berjalan}'
                                    )
                                "
                                ExecuteTrans(SQL)

                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show($"Terjadi Kesalaham. Data Gr 3 Detail Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using





                        'SQL = "select flag_hasil_Produksi_GR, b.no_transaksi from Emi_Split_Production_Order a, emi_production_results b "
                        'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Production_Order "
                        'SQL = SQL & "and a.status is null and b.status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Temp_NoSplit & "'"
                        'Using Dr = OpenTrans(SQL)
                        '    If Dr.Read Then

                        '        Dim no_transaksi As String = Dr("no_transaksi")

                        '        If General_Class.CekNULL(Dr("flag_hasil_Produksi_GR")) = "Y" Then
                        '            Dr.Close()
                        '            SQL = "select a.kode_perusahaan from EMI_Production_Results_Detail_Barang a, Emi_Production_Results_Detail_Pallet b, Barang_SN c where "
                        '            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Transaksi=b.No_Transaksi and a.status is null "
                        '            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.SN_Baru=c.Serial_Number and c.jumlah<>0 and a.no_transaksi='" & no_transaksi & "' "
                        '            Using dr2 = OpenTrans(SQL)
                        '                If Not dr2.Read Then


                        '                    dr2.Close()

                        '                    SQL = "update Emi_Split_Production_Order set Flag_Hasil_Produksi_GR = 'Y', UserID_Selesai_GR2 = '" & Temp_UserID & "', "
                        '                    SQL = SQL & "Tgl_Hasil_Produksi_GR2 = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Hasil_Produksi_GR2 = '" & Format(tgl_skg, "HH:mm:ss") & "'  "
                        '                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Temp_NoSplit & "' "
                        '                    ExecuteTrans(SQL)

                        '                End If
                        '            End Using

                        '        End If

                        '    Else

                        '        Dr.Close()
                        '        CloseTrans()
                        '        CloseConn()
                        '        MessageBox.Show("Data Split tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        Exit Sub
                        '    End If
                        'End Using
#End Region




                    Next
                End If
            End Using








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



#Region "CETAK BARCODE"


        ''=========================
        ''=     CETAK BARCODE     =
        ''=========================
        'get_jam()

        'Dim KdUnikPrint, KdUnikPrintScrap As New ArrayList

        'Try
        '    OpenConn()
        '    Dim CrDoc As New Object

        '    Dim KertasBesar As String = "BarcodeFG"
        '    Dim KertasKecil As String = "BarcodeQC"

        '    Dim kode_unik_print As String = ""

        '    'HAPUS TABEL SEMENTARA
        '    SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_2 "
        '    ExecuteTrans(SQL)

        '    SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_2_Scrap "
        '    ExecuteTrans(SQL)


        '    SQL = "with cte as( "
        '    SQL = SQL & "select b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
        '    SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan,   "
        '    SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
        '    SQL = SQL & "c.nomor as Number, "

        '    SQL = SQL & "e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing "

        '    SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN e, Emi_Split_Production_Order f, EMI_Order_Produksi g, EMI_Master_Routing h "
        '    SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
        '    SQL = SQL & "and b.kode_perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and g.Kode_Perusahaan = h.Kode_Perusahaan "
        '    SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
        '    SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
        '    SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
        '    SQL = SQL & "and b.No_Production_Order = f.No_Transaksi "
        '    SQL = SQL & "and f.No_PO = g.No_Faktur "
        '    SQL = SQL & "and g.Id_Routing = h.Id_Routing "
        '    SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and b.No_Production_Order = '" & Txt_NoSplit.Text & "' "
        '    SQL = SQL & "and b.No_Transaksi = '" & Temp_NoTransksi & "'  and c.Jenis<>'Finished Good' "

        '    SQL = SQL & "union all "

        '    SQL = SQL & "select Distinct b.Kode_Perusahaan, b.No_Production_Order, c.Nomor, c.Kode_Barang, d.nama as Nama_Barang, c.Batch_Number, e.Qr_Code, e.Tgl_Produksi, c. Kode_Stock_Owner_Tujuan as Lokasi_Tujuan, "
        '    SQL = SQL & "e.Tgl_Expired, c.Jumlah as jumlah, d.Satuan,  "
        '    SQL = SQL & "case when c.jenis = 'REJECTED' then 'Disqualified ' else c.jenis end as Jenis, "
        '    SQL = SQL & "c.nomor as Number, "

        '    SQL = SQL & "e.Kode_Unik_Berjalan, g.Id_Routing, h.Keterangan as Routing "

        '    SQL = SQL & "from Emi_Production_Results_Validation b, Emi_Production_Results_Validation_Detail c, Barang d, Barang_SN_sementara e, Emi_Split_Production_Order f, EMI_Order_Produksi g, EMI_Master_Routing h "
        '    SQL = SQL & "where  b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
        '    SQL = SQL & "and b.kode_perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and g.Kode_Perusahaan = h.Kode_Perusahaan "
        '    SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
        '    SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
        '    SQL = SQL & "and c.Kode_Barang = e.Kode_Barang and c.Serial_Number_Tujuan=e.Serial_Number "
        '    SQL = SQL & "and b.No_Production_Order = f.No_Transaksi "
        '    SQL = SQL & "and f.No_PO = g.No_Faktur "
        '    SQL = SQL & "and g.Id_Routing = h.Id_Routing "
        '    SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and b.No_Production_Order = '" & Txt_NoSplit.Text & "' "
        '    SQL = SQL & "and b.No_Transaksi = '" & Temp_NoTransksi & "'  and c.Jenis='Finished Good' "

        '    SQL = SQL & ") select kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, sum(Jumlah) as Jumlah, Satuan, Jenis, Number, Id_Routing, Routing "
        '    SQL = SQL & "from cte "
        '    SQL = SQL & "group by kode_perusahaan, no_production_order, Lokasi_Tujuan, Kode_Barang, Nama_Barang, Kode_Unik_Berjalan, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired, Satuan, Jenis, Number, Nomor, Id_Routing, Routing "
        '    Using Ds = BindingTrans(SQL)
        '        With Ds.Tables("MyTable")
        '            If .Rows.Count <> 0 Then
        '                For i As Integer = 0 To .Rows.Count - 1


        '                    kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

        '                    Dim fullNewQrScrap As String = .Rows(i).Item("Qr_Code") & "-" & .Rows(i).Item("Kode_Unik_Berjalan")

        '                    Barcode.Image = Nothing

        '                    Barcode.Image = Generate_QR_NoPadding(fullNewQrScrap)

        '                    Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")

        '                    '   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

        '                    'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
        '                    Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
        '                    'End If

        '                    fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
        '                    FileSize1 = fs1.Length
        '                    rawData1 = New Byte(FileSize1) {}
        '                    fs1.Read(rawData1, 0, FileSize1)
        '                    fs1.Close()
        '                    Cmd.Parameters.Add("@newBarcode" & kode_unik_print, SqlDbType.Image).Value = rawData1

        '                    Dim asdada As String = .Rows(i).Item("Jenis").ToString.ToUpper

        '                    If .Rows(i).Item("Jenis").ToString.ToUpper.Trim = "FINISHED GOOOD" Then

        '                        SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2 (Kode_Perusahaan, No_Split, Kode_Barang, Barcode, Nama_Barang, Batch_Number, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, Tgl_Expired, Jam_Expired, Jumlah, Satuan, Jenis, Number, Kode_Unik_Print) "
        '                        SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "', '" & .Rows(i).Item("Kode_Barang") & "', @newBarcode" & kode_unik_print & ", "
        '                        SQL = SQL & "'" & .Rows(i).Item("Nama_Barang") & "', '" & .Rows(i).Item("Batch_Number") & "', '" & fullNewQrScrap & "', '" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Tgl_Produksi") & "', "
        '                        SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Tgl_Expired") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
        '                        SQL = SQL & "'" & .Rows(i).Item("Jenis") & "', '" & .Rows(i).Item("Number") & "', '" & kode_unik_print & "') "
        '                        ExecuteTrans(SQL)

        '                        KdUnikPrint.Add(kode_unik_print)

        '                    ElseIf .Rows(i).Item("Jenis").ToString.ToUpper.Trim = "DISQUALIFIED" Then
        '                        SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2 (Kode_Perusahaan, No_Split, Kode_Barang, Barcode, Nama_Barang, Batch_Number, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, Tgl_Expired, Jam_Expired, Jumlah, Satuan, Jenis, Number, Kode_Unik_Print) "
        '                        SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("no_production_order") & "', '" & .Rows(i).Item("Kode_Barang") & "', @newBarcode" & kode_unik_print & ", "
        '                        SQL = SQL & "'" & .Rows(i).Item("Nama_Barang") & "', '" & .Rows(i).Item("Batch_Number") & "', '" & fullNewQrScrap & "', '" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Tgl_Produksi") & "', "
        '                        SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Tgl_Expired") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
        '                        SQL = SQL & "'" & .Rows(i).Item("Jenis") & "', '" & .Rows(i).Item("Number") & "', '" & kode_unik_print & "') "
        '                        ExecuteTrans(SQL)

        '                        KdUnikPrint.Add(kode_unik_print)

        '                    Else



        '                        SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2_Scrap (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
        '                        SQL = SQL & "Proses, Jumlah, Satuan, Nomor, id_routing, routing, Kode_unik_print)  "
        '                        SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoSplit.Text & "', @newBarcode" & kode_unik_print & ", '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', '" & fullNewQrScrap & "', '" & .Rows(i).Item("Qr_Code") & "', "
        '                        SQL = SQL & "'" & .Rows(i).Item("Tgl_Produksi") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'X', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
        '                        SQL = SQL & "'" & .Rows(i).Item("Number") & "', '" & .Rows(i).Item("Id_Routing") & "', '" & .Rows(i).Item("Routing") & "', '" & kode_unik_print & "') "
        '                        ExecuteTrans(SQL)

        '                        KdUnikPrintScrap.Add(kode_unik_print)
        '                    End If



        '                Next
        '            End If
        '        End With
        '    End Using


        '    For i As Integer = 0 To KdUnikPrint.Count - 1

        '        SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2 where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & KdUnikPrint(i) & "'"
        '        Using Ds = BindingTrans(SQL)
        '            If Ds.Tables("MyTable").Rows.Count <> 0 Then

        '                '==========================
        '                '=     BARCODEE BESAR     =
        '                '==========================
        '                Dim printerDitemukan As Boolean = False
        '                For Each printer As String In PrinterSettings.InstalledPrinters
        '                    If printer.ToLower() = PrinterBarcode.ToLower() Then
        '                        printerDitemukan = True
        '                        Exit For
        '                    End If
        '                Next

        '                If printerDitemukan Then

        '                    CrDoc = New N_EMI_Label_Barcode_GR_2

        '                    'With A_Place_For_Printing2
        '                    '    CrDoc.SetDataSource(Ds)
        '                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '                    '    CrDoc.PrintOptions.PrinterName = ""
        '                    '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & KdUnikPrint(i) & "' "
        '                    '    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2"
        '                    '    .Text = "Label Good Received 2"
        '                    '    .CrystalReportViewer1.ReportSource = CrDoc
        '                    '    .Refresh()
        '                    '    .Show()
        '                    'End With

        '                    '=====================================================

        '                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        '                    CrDoc.SetDataSource(Ds)
        '                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '                    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & KdUnikPrint(i) & "' "
        '                    CrDoc.PrintOptions.PrinterName = PrinterBarcode

        '                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

        '                    Dim rawKind As Integer
        '                    Dim foundPaper As Boolean = False
        '                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        '                    For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        '                        If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasBesar Then
        '                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
        '                            CrDoc.PrintOptions.PaperSize = rawKind
        '                            foundPaper = True
        '                            Exit For
        '                        End If
        '                    Next

        '                    If Not foundPaper Then
        '                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        '                        MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        '                    End If

        '                    CrDoc.PrintToPrinter(1, False, 1, 2500)

        '                Else
        '                    MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                End If

        '                printerDitemukan = False


        '            Else
        '                MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        '            End If


        '        End Using



        '    Next


        '    For i As Integer = 0 To KdUnikPrintScrap.Count - 1

        '        SQL = "select kode_perusahaan from N_EMI_Barcode_Label_Barcode_GR_2_Scrap where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & KdUnikPrintScrap(i) & "'"
        '        Using Ds = BindingTrans(SQL)
        '            If Ds.Tables("MyTable").Rows.Count <> 0 Then

        '                '==========================
        '                '=     BARCODEE BESAR     =
        '                '==========================
        '                Dim printerDitemukan As Boolean = False
        '                For Each printer As String In PrinterSettings.InstalledPrinters
        '                    If printer.ToLower() = PrinterBarcode.ToLower() Then
        '                        printerDitemukan = True
        '                        Exit For
        '                    End If
        '                Next

        '                If printerDitemukan Then

        '                    CrDoc = New N_EMI_Label_Barcode_GR_2_Scrap

        '                    'With A_Place_For_Printing2
        '                    '    CrDoc.SetDataSource(Ds)
        '                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '                    '    CrDoc.PrintOptions.PrinterName = ""
        '                    '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Unik_Print} = '" & KdUnikPrintScrap(i) & "' "
        '                    '    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2 Scrap"
        '                    '    .Text = "Label Good Received 2"
        '                    '    .CrystalReportViewer1.ReportSource = CrDoc
        '                    '    .Refresh()
        '                    '    .Show()
        '                    'End With

        '                    '=====================================================

        '                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        '                    CrDoc.SetDataSource(Ds)
        '                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '                    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Barcode_Label_Barcode_GR_2_Scrap.Kode_Unik_Print} = '" & KdUnikPrintScrap(i) & "' "
        '                    CrDoc.PrintOptions.PrinterName = PrinterBarcode

        '                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

        '                    Dim rawKind As Integer
        '                    Dim foundPaper As Boolean = False
        '                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        '                    For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        '                        If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasBesar Then
        '                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
        '                            CrDoc.PrintOptions.PaperSize = rawKind
        '                            foundPaper = True
        '                            Exit For
        '                        End If
        '                    Next

        '                    If Not foundPaper Then
        '                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        '                        MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        '                    End If


        '                    CrDoc.PrintToPrinter(1, False, 1, 2500)

        '                Else
        '                    MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                End If

        '                printerDitemukan = False


        '            Else
        '                MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        '            End If


        '        End Using



        '    Next



        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

#End Region

        Kosong()




    End Sub

    Private Sub Hitung_Data()
        If Lv_Data.Items.Count = 0 Then Exit Sub

        Dim TotFG As Double = 0
        Dim TotKG As Double = 0
        For i As Integer = 0 To Lv_Data.Items.Count - 1
            Get_Lv_Data_GR(i)

            TotFG += LvData_Jumlah
            'TotKG += LvData_Berat
        Next

        Txt_TotFG.Text = Format(TotFG, "N0")
        'Txt_TotBeratKG.Text = Format(TotKG, "N4")

    End Sub


    Public Sub LoadFromSD()

        If arrBarcodeFromSD.Count = 0 Then
            'MessageBox.Show("Tidak Ada Data yang Dipilih", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_DataPallet.Items.Clear()
            For i As Integer = 0 To arrBarcodeFromSD.Count - 1

                Dim DataDic As Dictionary(Of String, String) = arrBarcodeFromSD.Item(i)

                'Dim NoSplit As String = Txt_NoSplit.Text.Trim

                'SQL = "select a.No_Transaksi as No_GR2, a.No_Production_Order as No_Split, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama as Nama_Barang, b.Jenis, c.Qr_Code, c.Kode_Unik_Berjalan, c.Batch_Number, "
                'SQL = SQL & "c.Tgl_Produksi, c.Tgl_Expired, a.UserID, b.tahap, "

                ''SQL = SQL & "isnull(((isnull(sum(c.Jumlah), 0)) - "
                ''SQL = SQL & "ISNULL((select isnull(sum(z.jumlah), 0) from N_EMI_Validation_GR_3_Detail z, N_EMI_Validation_GR_3 x "
                ''SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = a.Kode_Perusahaan "
                ''SQL = SQL & "and z.No_Transaksi = x.No_Transaksi and z.No_Transaksi_GR2 = a.No_Transaksi and x.status is null "
                ''SQL = SQL & "),0)), 0) as Jumlah, "

                'SQL = SQL & "isnull(sum(c.Jumlah), 0) as Jumlah, "


                'SQL = SQL & "b.Satuan, b.Warna, e.Keterangan as Kualitas, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, "

                'SQL = SQL & "case "
                'SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = a.No_Production_Order and z.No_Batch = b.Tahap), 'U') = 'T' then 'DITOLAK' "
                'SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = a.No_Production_Order and z.No_Batch = b.Tahap), 'U') = 'Y' then 'DITERIMA' "
                'SQL = SQL & "when isnull(( select top 1 z.Flag_Ready_For_Packaging from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = a.No_Production_Order "
                'SQL = SQL & "and z.No_Batch = b.Tahap and z.Flag_Ready_For_Packaging = 'Y' and z.No_GR = '2' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' then 'READY FOR PACKING' "
                'SQL = SQL & "when isnull(( select top 1 z.Kode_Perusahaan from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = a.No_Production_Order "
                'SQL = SQL & "and z.No_Batch = b.Tahap and z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging is null and z.No_GR = '2' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' then 'HOLD' "
                'SQL = SQL & "else 'NO DATA' "
                'SQL = SQL & "end as Status_Split "

                'SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Barang_SN c, barang d, EMI_Master_Warna e "
                'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
                'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                'SQL = SQL & "and b.kode_stock_owner_tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Tujuan = c.Serial_Number "
                'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
                'SQL = SQL & "and b.Warna = e.Kode_Warna "
                'SQL = SQL & "and a.Status is null and a.Flag_Validasi is null "
                'SQL = SQL & "and b.Jenis = 'Finished Good' "

                'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                ''SQL = SQL & "and (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) = '" & Txt_ScanBarcode.Text.Trim & "' "
                'SQL = SQL & "and c.Qr_Code = '" & DataDic("QrCode") & "' and c.Kode_Unik_Berjalan = '" & DataDic("KdUnikBerjalan") & "' "
                'SQL = SQL & "group by a.kode_perusahaan, a.No_Transaksi, a.No_Production_Order, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama, b.Jenis, c.Qr_Code, "
                'SQL = SQL & "c.Kode_Unik_Berjalan, c.Batch_Number, c.Tgl_Produksi, c.Tgl_Expired, a.UserID, b.Satuan, b.Warna, e.Keterangan, b.Tahap "
                'SQL = SQL & "order by a.No_Production_Order, b.Kode_Stock_Owner_Tujuan, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan), c.Tgl_Expired ASC "


                SQL = ";With Cte as ("
                SQL &= $"select a.No_Transaksi as No_GR2, isnull(b.No_Split, '-') as No_Split, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama as Nama_Barang, b.Jenis, c.Qr_Code, c.Kode_Unik_Berjalan, c.Batch_Number, "
                SQL &= $"c.Tgl_Produksi, c.Tgl_Expired, a.UserID,  "
                SQL &= $"isnull(sum(c.Jumlah), 0) as Jumlah, "
                SQL &= $"b.Satuan, b.Warna, e.Keterangan as Kualitas, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, "
                SQL &= $"case when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = b.No_Split and z.No_Batch = b.Tahap), 'U') = 'T' then 'DITOLAK' "
                SQL &= $"when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = b.No_Split and z.No_Batch = b.Tahap), 'U') = 'Y' then 'DITERIMA' "
                SQL &= $"when isnull(( select top 1 z.Flag_Ready_For_Packaging from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = b.No_Split "
                SQL &= $"and z.No_Batch = b.Tahap and z.Flag_Ready_For_Packaging = 'Y' and z.No_GR = '2' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' then 'READY FOR PACKING' "
                SQL &= $"when isnull(( select top 1 z.Kode_Perusahaan from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = b.No_Split "
                SQL &= $"and z.No_Batch = b.Tahap and z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging is null and z.No_GR = '2' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' then 'HOLD' "
                SQL &= $"else 'NO DATA' "
                SQL &= $"end as Status_Split "
                SQL &= $"from Emi_Production_Results_Validation a "
                SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
                SQL &= $"inner join Barang_SN c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number  "
                SQL &= $"inner join Barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
                SQL &= $"inner join EMI_Master_Warna e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.Warna  = e.Kode_Warna  "
                SQL &= $"where a.Kode_Perusahaan  = '{KodePerusahaan}' "
                SQL &= $"and a.Status is null "
                SQL &= $"and a.Flag_Validasi is NULl "
                SQL &= $"and b.Flag_Validasi_Loading = 'Y' "
                SQL &= $"and b.Jenis = 'Finished Good' "
                SQL &= $"and c.Jumlah <> 0 "
                SQL &= $"and c.Qr_Code = '{DataDic("QrCode")}' and c.Kode_Unik_Berjalan = '{DataDic("KdUnikBerjalan")}' "
                SQL &= $"group by a.kode_perusahaan, a.No_Transaksi, b.No_Split, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama, b.Jenis, c.Qr_Code, "
                SQL &= $"c.Kode_Unik_Berjalan, c.Batch_Number, c.Tgl_Produksi, c.Tgl_Expired, a.UserID, b.Satuan, b.Warna, e.Keterangan, b.Tahap "
                SQL &= $") select No_GR2, Kode_Stock_Owner_Tujuan, Kode_Barang, Nama_Barang, Jenis, Qr_Code, Kode_Unik_Berjalan, Batch_Number, Tgl_Produksi, Tgl_Expired, UserID, sum(Jumlah) as Jumlah, "
                SQL &= $"Satuan, Warna, Kualitas, Barcode, Status_Split "
                SQL &= $"from cte "
                SQL &= $"group by No_GR2, Kode_Stock_Owner_Tujuan, Kode_Barang, Nama_Barang, Jenis, Qr_Code, Kode_Unik_Berjalan, Batch_Number, Tgl_Produksi, Tgl_Expired, UserID, "
                SQL &= $"Satuan, Warna, Kualitas, Barcode, Status_Split "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        'If Not NoSplit = "" Then
                        '    If NoSplit <> Dr("No_Split") Then
                        '        Dr.Close()
                        '        CloseConn()
                        '        MessageBox.Show("No Split Tidak Boleh Berbeda", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        Exit Sub
                        '    End If
                        'End If

                        'If NoSplit = "" Then
                        '    NoSplit = Dr("No_Split")
                        'Else

                        '    If NoSplit <> Dr("No_Split") Then
                        '        isCombine = True

                        '        'Dr.Close()
                        '        'CloseConn()
                        '        'MessageBox.Show("No Split Tidak Boleh Berbeda", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        'Exit Sub
                        '    End If
                        'End If

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
                        Lv = Lv_DataPallet.Items.Add(Dr("Kode_Stock_Owner_Tujuan"))
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
                        Lv.SubItems.Add(Dr("No_GR2"))

                        'Txt_NoSplit.Text = Dr("No_Split")
                        Txt_ScanBarcode.Text = ""

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
                        'Txt_NoSplit.Text = ""
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

                Hitung_Data()
                CekMilitarySampling()
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
        'If LvBarcode.FocusedItem Is Nothing OrElse LvBarcode.FocusedItem.Index = -1 Then Exit Sub

        'Dim SelectedIndex As Integer = LvBarcode.FocusedItem.Index

        'Get_Lv_Data_Barcode(SelectedIndex)

        'Dim KdBarang As String = ""
        'Try
        '    OpenConn()

        '    SQL = "select distinct top 1 b.Kode_Barang, b.Nama, b.Berat "
        '    SQL = SQL & "from Barang_SN a, barang b, N_EMI_Validation_GR_Temp c "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
        '    SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and (qr_code +'-'+Kode_Unik_Berjalan ) = c.Barcode "
        '    SQL = SQL & "and c.Nomor = '" & LvBarcode_ID & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            KdBarang = Dr("Kode_Barang")
        '        End If
        '    End Using


        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'If LvBarcode_Jenis.ToUpper = "FINISHED GOOD" Then
        '    SD_ValidasiGR_Detail_Packaging.No_Split = Txt_NoSplit.Text
        '    SD_ValidasiGR_Detail_Packaging.Kd_Barang = KdBarang
        '    SD_ValidasiGR_Detail_Packaging.JumlahInput = LvBarcode_Total

        '    SD_ValidasiGR_Detail_Packaging.ShowDialog()

        'End If
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
        If e.KeyChar = Chr(13) Then Txt_Jumlah.Focus()
    End Sub

    Private Sub Cmb_LokasiTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_LokasiTujuan.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Jenis.DroppedDown = True
            Cmb_Jenis.Focus()
        End If
    End Sub



    Private Sub EMI_Validasi_GR_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            OpenConn()

            'SQL = "delete N_EMI_Validation_GR_Temp where kode_perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
            SQL = "delete N_EMI_Validation_GR3_Temp where kode_perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "' "
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

End Class

