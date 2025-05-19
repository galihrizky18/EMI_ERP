Imports System.Drawing.Printing
Imports System.IO
Public Class Emi_Production_Barcode

    Dim fno_po As String = ""
    Dim Prefix As String = ""
    Dim kategoriQuality As New ArrayList({"HIJAU", "MERAH"})
    Dim kategoriQualityKet As New ArrayList({"Good Stock", "Bad Stock"})
    Dim arrInisialFaktur, arrSO As New ArrayList
    Dim arrKdBarangSrap, arrNamaScrap, arrSatuanScrap, arrSatuanKecilScrap As New ArrayList
    Private random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Dim Lv_NoSplit, Lv_NoPO, Lv_Lokasi, Lv_Tanggal, Lv_Jam, Lv_KdSo, Lv_KdBarang, Lv_NmBarang, Lv_Jmlh, Lv_Satuan, Lv_Catatan, Lv_Routing, Lv_TglSelesaiProduksi, Lv_TglExpired, Lv_PrefixCode As String
    Dim Lv_Life_Time As String
    Dim Tahun_MulaiProduksi As String

    Dim item_NoSplit As Integer = 0
    Dim item_NoPO As Integer = 1
    Dim item_Lokasi As Integer = 2
    Dim item_Tanggal As Integer = 3
    Dim item_Jam As Integer = 4
    Dim item_KdSo As Integer = 5
    Dim item_KdBarang As Integer = 6
    Dim item_NamaBarang As Integer = 7
    Dim item_Catatan As Integer = 8
    Dim item_Routing As Integer = 9
    Dim item_TglSelesaiProduksi As Integer = 10
    Dim item_TglExpired As Integer = 11
    Dim item_PrefixCode As Integer = 12
    Dim item_lf As Integer = 13
    Dim item_Jmlh As Integer = 14
    Dim item_Satuan As Integer = 15

    Private Sub Chk_FullPallet_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_FullPallet.CheckedChanged

        Try
            OpenConn()

            SQL = "select kode_barang, total, Satuan_Jumlah from barang_detail_susunan where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and flag_default = 'Y' and kode_barang ='" & Txt_KdBarang.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Cmb_Satuan.Items.Clear()

                    If Chk_FullPallet.Checked = True Then
                        Txt_Jumlah.Enabled = False
                        Txt_Jumlah.Text = String.Empty
                        Txt_Jumlah.Text = Dr("total")

                    Else
                        Txt_Jumlah.Enabled = True
                        Txt_Jumlah.Text = 0
                    End If



                Else
                    Txt_Jumlah.Enabled = True
                    Txt_Jumlah.Text = 0

                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub DtpProduksi_ValueChanged(sender As Object, e As EventArgs) Handles DtpProduksi.ValueChanged

        Dim tanggal_produksi As Date = DtpProduksi.Value

        Dim tglExp As Date = tanggal_produksi.AddDays(Val(TxtLifeTime.Text))

        DtpExpired.Value = tglExp

    End Sub

    Private Sub Lv_Data_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub



    Private Sub Emi_Barcode_FinishGood_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()

    End Sub

    Private Sub kosong()

        'Txt_NoSplit.Text = String.Empty
        Txt_KdBarang.Text = String.Empty
        Txt_NamaBarang.Text = String.Empty
        Txt_Jumlah.Text = String.Empty
        Txt_HasilProduksi.Text = String.Empty
        TxtLifeTime.Text = ""
        TxtJmlScrap.Text = ""
        DtpProduksi.ResetText()
        DtpExpired.ResetText()

        Cmb_Satuan.Items.Clear()

        Chk_FullPallet.Checked = False


        'GET TAHUN MULAI PRODUKSI
        Try
            OpenConn()

            SQL = "select Tahun_Mulai_Produksi from Init"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Tahun_MulaiProduksi = If(General_Class.CekNULL(dr("Tahun_Mulai_Produksi")) = "", "0", dr("Tahun_Mulai_Produksi"))
                Loop
            End Using

            Cmb_Lokasi.Items.Clear()
            SQL = "select Kode_Stock_Owner from Stock_Owner where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("Kode_Stock_Owner"))
                Loop
                Cmb_Lokasi.SelectedIndex = 0
            End Using

            CmbJenis.Items.Clear() : kategoriQuality.Clear()
            kategoriQualityKet.Clear()
            SQL = "select Kode_warna, Keterangan from emi_master_warna where kode_warna<>'HIJAU' and "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_warna"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbJenis.Items.Add(dr("Keterangan"))
                    kategoriQuality.Add(dr("Kode_warna"))
                    kategoriQualityKet.Add(dr("Keterangan"))
                Loop
            End Using

            SQL = "select a.No_Transaksi, a.no_po, a.lokasi, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.jumlah, a.satuan,a.Tgl_Produksi, a.jam_Produksi "
            SQL = SQL & "from Emi_Split_Production_Order a, barang b where "
            SQL = SQL & "a.kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner And a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_transaksi = '" & Txt_NoSplit.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    DateTimePicker1.Value = Dr("Tgl_Produksi")
                    TxtJam.Text = Dr("jam_Produksi")
                    Txt_HasilProduksi.Text = Dr("jumlah")
                    Txt_NamaBarang.Text = Dr("Nama")
                    fno_po = Dr("no_po")
                    Txt_KdBarang.Text = Dr("Kode_Barang")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data Tidak ditemukan . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Select b.prefix_code from EMI_Order_Produksi a, EMI_Master_Routing b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Id_Routing = b.Id_Routing "
            SQL = SQL & "And a.status Is null and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.no_faktur='" & fno_po & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Prefix = Dr("prefix_code")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data Tidak ditemukan . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select no_transaksi from Emi_Production_Results where "
            SQL = SQL & "No_Production_Order = '" & Txt_NoSplit.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtFormulator_NoFaktur.Text = Dr("no_transaksi")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data Tidak ditemukan . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim Lokasi_Produksi As String = ""
            SQL = "select Kode_Stock_Owner From Stock_Owner_Gudang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Produksi = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Lokasi_Produksi = Dr("kode_stock_owner")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Lokasi Produksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmb_Satuan.Items.Clear()
            Cmb_SatuanProduksi.Items.Clear()
            CmbSatScrap.Items.Clear()
            SQL = "select satuan from EMI_Satuan where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Cmb_Satuan.Items.Add(Dr("satuan"))
                    Cmb_SatuanProduksi.Items.Add(Dr("satuan"))
                    CmbSatScrap.Items.Add(Dr("satuan"))
                End If

            End Using

            Dim kode_barang_scrap As String = ""
            Dim kode_barang As String = Txt_KdBarang.Text

            CmbSisaProduksi.Items.Clear()
            arrKdBarangSrap.Clear() : arrNamaScrap.Clear() : arrSatuanScrap.Clear() : arrSatuanKecilScrap.Clear()
            SQL = "select a.kode_barang_Scrap, b.nama, b.satuan as satuan_kecil, c.satuan from "
            SQL = SQL & "emi_binding_scrap a, barang b, barang_detail_satuan c "
            SQL = SQL & "where a.kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang_scrap=B.Kode_Barang "
            SQL = SQL & "and b.kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_stock_Owner='" & Lokasi_Produksi & "' "
            SQL = SQL & "and c.flag_tampil_display = 'Y' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbSisaProduksi.Items.Add(Dr("nama")) : arrKdBarangSrap.Add(Dr("kode_barang_Scrap"))
                    arrNamaScrap.Add(Dr("nama")) : arrSatuanScrap.Add(Dr("satuan"))
                    arrSatuanKecilScrap.Add(Dr("satuan_kecil"))
                Loop
            End Using

            Dim satuanKodeBarang As String = ""
            SQL = "select top(1) a.satuan, b.satuan as Satuan_Kecil, isnull(Life_Time,0) as Life_Time from barang_detail_satuan a, barang b where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_barang = '" & kode_barang & "' and a.kode_Barang=b.kode_barang "
            SQL = SQL & "and a.flag_tampil_display = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Cmb_Satuan.Text = Dr("satuan")
                    Cmb_SatuanProduksi.Text = Dr("satuan")
                    TxtSatuanKecil.Text = Dr("Satuan_Kecil")
                    TxtLifeTime.Text = Dr("Life_Time")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang detail satuan tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmb_LokasiSimpan.Items.Clear() : arrSO.Clear()
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and Flag_Penyimpanan='Y' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_LokasiSimpan.Items.Add(dr("Keterangan")) : arrSO.Add(dr("kode_stock_owner"))
                    arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            Dim tanggal_produksi As Date = DtpProduksi.Value

            Dim tglExp As Date = tanggal_produksi.AddDays(Val(TxtLifeTime.Text))

            DtpExpired.Value = tglExp

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub


    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_NoSplit.Text.Trim.Length = 0 Then Exit Sub

        If TxtJmlScrap.Text.Trim.Length <> 0 And Val(TxtJmlScrap.Text) <> 0 Then
            If CmbSisaProduksi.SelectedIndex = -1 Then
                MessageBox.Show("Sisa Produksi harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSisaProduksi.Focus()
                Exit Sub
            End If
        End If

        If Txt_Jumlah.Text.Trim.Length <> 0 And Val(Txt_Jumlah.Text) <> 0 Then
            If CmbJenis.Text.Trim.Length = 0 Then
                MessageBox.Show("Jenis harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbJenis.Focus()
                Exit Sub
            ElseIf Cmb_LokasiSimpan.Text.Trim.Length = 0 Then
                MessageBox.Show("Lokasi harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_LokasiSimpan.Focus()
                Exit Sub
            End If
        End If


        If Val(TxtJmlScrap.Text) = 0 And Val(Txt_Jumlah.Text) = 0 Then

            MessageBox.Show("Tidak ada data yg di input!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jumlah.Focus()
            Exit Sub

        End If

        'If CmbJenis.SelectedIndex = -1 Then
        '    MessageBox.Show("Jenis Quality Harus diPilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    CmbJenis.Focus()
        '    Exit Sub
        'End If

        Simpan_Data(True)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Txt_NoSplit.Text.Trim.Length = 0 Then Exit Sub

        If TxtJmlScrap.Text.Trim.Length <> 0 And Val(TxtJmlScrap.Text) <> 0 Then
            If CmbSisaProduksi.SelectedIndex = -1 Then
                MessageBox.Show("Sisa Produksi harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSisaProduksi.Focus()
                Exit Sub
            End If
        End If

        If Txt_Jumlah.Text.Trim.Length <> 0 And Val(Txt_Jumlah.Text) <> 0 Then
            If CmbJenis.Text.Trim.Length = 0 Then
                MessageBox.Show("Jenis harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbJenis.Focus()
                Exit Sub
            ElseIf Cmb_LokasiSimpan.Text.Trim.Length = 0 Then
                MessageBox.Show("Lokasi harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_LokasiSimpan.Focus()
                Exit Sub
            End If
        End If


        If Val(TxtJmlScrap.Text) = 0 And Val(Txt_Jumlah.Text) = 0 Then

            MessageBox.Show("Tidak ada data yg di input!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Jumlah.Focus()
            Exit Sub

        End If

        Simpan_Data(False)
    End Sub

    Private Sub Txt_Jumlah_Leave(sender As Object, e As EventArgs) Handles Txt_Jumlah.Leave
        If Not IsNumeric(Txt_Jumlah.Text) Then Txt_Jumlah.Text = String.Empty : Exit Sub
    End Sub

    Private Function Ubah_Angka_Kecil(ByVal kodeBarang As String, ByVal satuanBesar As String, ByVal satuanKecil As String, ByVal jumlahConvert As String) As Double

        Dim total_kecil As Double = 0
        SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & kodeBarang & "', '" & satuanBesar & "',"
        SQL = SQL & "'" & satuanKecil & "', '" & HilangkanTanda(jumlahConvert) & "' ) as hasil"
        Using Dr1 = OpenTrans(SQL)
            If Dr1.Read Then
                If General_Class.CekNULL(Dr1("hasil")) = "" Then
                    Dr1.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data konversi satuan kirim tidak ada ")
                End If

                total_kecil = Dr1("hasil")
            Else
                Dr1.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("data konversi satuan kirim tidak ada ")
            End If
        End Using

        Return total_kecil
    End Function

    Private Sub CmbSisaProduksi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSisaProduksi.SelectedIndexChanged
        If CmbSisaProduksi.SelectedIndex = -1 Then
            Exit Sub
        End If

        CmbSatScrap.Text = arrSatuanScrap.Item(CmbSisaProduksi.SelectedIndex)
        TxtSatScrapKecil.Text = arrSatuanKecilScrap.Item(CmbSisaProduksi.SelectedIndex)
    End Sub

    Private Sub Txt_Jumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Jumlah.KeyPress
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TxtJmlScrap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJmlScrap.KeyPress
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Simpan_Data(ByVal isNormalSave As Boolean)
        get_jam()

        Dim kode_unik_print, kode_unik_print_scrap As String

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim Nilai_Packaging As Double = 0


            Dim proses As Integer
            SQL = "select TOP(1) no_transaksi, "
            SQL = SQL & "isnull((select top(1) proses from Emi_Production_Results_Detail_barang x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = x.No_Transaksi order by proses desc "
            SQL = SQL & "),0) as proses "
            SQL = SQL & "from Emi_Production_Results a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Order = '" & Txt_NoSplit.Text & "' order by proses desc "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtFormulator_NoFaktur.Text = Dr("no_transaksi")
                    proses = Dr("proses") + 1
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Tidak di temukan . !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim kd_inq As String = ""
            SQL = "select top(1) kode_barang_inq from barang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_barang = '" & Txt_KdBarang.Text & "'  "
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

            '==================================
            '=     POTONG STOCK PACKAGING     =
            '==================================
            'GET DETAIL DATA PACKAGING BY NO SPLIT
            SQL = "select a.No_Transaksi, "
            SQL = SQL & "isnull(( select z.kode_barang_inq from barang z where a.kode_perusahaan = z.kode_perusahaan "
            SQL = SQL & "and a.kode_stock_owner = z.kode_stock_owner and a.kode_barang = z.kode_barang "
            SQL = SQL & "), '-') as Kode_Barang, "
            SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang as Kode_Bahan, c.Nama ,b.Jumlah, b.Satuan, c.flag_potong_stok,isnull(c.standar_price,0) as standar_price "
            SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & Txt_NoSplit.Text & "' "
            If Not isNormalSave Then
                SQL = SQL & "and b.Kode_Barang in ( "
                SQL = SQL & "select z.Kode_Bahan from Barang_Detail_Bahan_Penolong z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Barang = '" & kd_inq & "' and jenis = 'KEMASAN UTAMA') "
            End If
            SQL = SQL & "order by c.nama "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            '================================================================
                            '=     GET JUMLAH KEBUTUHAN PACKAGING TERHADAP KD_BARANG PO     =
                            '================================================================
                            Dim jumlahKebutuhanPackaging As Double = 0
                            Dim jumlahBarangPerBahan As Double = 0
                            SQL = "select jumlah_barang, jumlah_bahan  "
                            SQL = SQL & "from Barang_Detail_Bahan_Penolong  "
                            SQL = SQL & "where kode_barang='" & .Rows(i).Item("Kode_Barang") & "' "
                            SQL = SQL & "and kode_Bahan = '" & .Rows(i).Item("Kode_Bahan") & "' "
                            SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    jumlahKebutuhanPackaging = Val(HilangkanTanda(Format(Dr("jumlah_bahan"), "N4")))
                                    jumlahBarangPerBahan = Dr("jumlah_barang")
                                End If
                            End Using

                            Dim NilaiProduksiPck As Double = Val(HilangkanTanda(Format((Val(HilangkanTanda(Txt_Jumlah.Text)) / jumlahBarangPerBahan) * jumlahKebutuhanPackaging, "N4")))


                            '======                              =========='
                            '======   Awal convert satuan barang =========='
                            '=========                           =========='
                            Dim convertKeSatuanAsli_pckg As String = ""
                            Dim jumlahConvertPckg As Double = 0

                            If NilaiProduksiPck > 0 Then
                                SQL = "select satuan From barang where Kode_barang = '" & .Rows(i).Item("Kode_Bahan") & "' "
                                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' "
                                Using Dr3 = OpenTrans(SQL)
                                    If Dr3.Read Then
                                        convertKeSatuanAsli_pckg = Dr3("satuan")
                                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("Kode_Bahan") & "',"
                                        SQL = SQL & "'" & .Rows(i).Item("Satuan") & "','" & Dr3("satuan") & "',"
                                        SQL = SQL & "" & HilangkanTanda(NilaiProduksiPck) & ") as Hasil "
                                        Dr3.Close()
                                        Using dr4 = OpenTrans(SQL)
                                            If dr4.Read Then
                                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                                    If dr4("Hasil") = 0 Then
                                                        dr4.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Satuan " & .Rows(i).Item("Satuan") & " Ke " & convertKeSatuanAsli_pckg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    Else
                                                        jumlahConvertPckg = Val(HilangkanTanda(Format(dr4("hasil"), "N4")))

                                                    End If
                                                Else
                                                    dr4.Close()
                                                    CloseTrans()
                                                    CloseConn()

                                                    MessageBox.Show("Gagal Convert Satuan. . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                        End Using
                                    Else
                                        Dr3.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '======                              =========='
                                '======   Akhir convert satuan barang =========='
                                '=========                           =========='
                                SQL = "INSERT INTO Emi_Production_Results_Packaging_Detail(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan,proses,"
                                SQL = SQL & "nilai_barang,satuan_barang,userid,tanggal,jam) "
                                SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & .Rows(i).Item("Kode_Stock_Owner") & "','" & .Rows(i).Item("Kode_Bahan") & "',"
                                SQL = SQL & "'" & HilangkanTanda(.Rows(i).Item("Jumlah")) & "','" & NilaiProduksiPck & "','" & .Rows(i).Item("Satuan") & "', '" & proses & "',"
                                SQL = SQL & "'" & jumlahConvertPckg & "', '" & convertKeSatuanAsli_pckg & "', '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "'"
                                SQL = SQL & ") "
                                ExecuteTrans(SQL)


                                Dim x_ident_currentPackaging As Integer = 0
                                SQL = "select IDENT_CURRENT('Emi_Production_Results_Packaging_Detail') as urutan"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        x_ident_currentPackaging = Dr("urutan")
                                    End If
                                End Using

                                SQL = "select round(good_stock,4) as good_stock, flag_ppn from barang where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and "
                                SQL = SQL & "kode_barang = '" & .Rows(i).Item("Kode_Bahan") & "'"
                                Using Dss = BindingTrans(SQL)

                                    If Dss.Tables("MyTable").Rows.Count <> 0 Then
                                        If Dss.Tables("MyTable").Rows(0).Item("good_stock") - jumlahConvertPckg < BolehNegatif Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Proses membuat stock menjadi negatif untuk kode barang " & .Rows(i).Item("Kode_Bahan") & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            SQL = "Update barang set good_stock = good_stock - " & jumlahConvertPckg & " where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and "
                                            SQL = SQL & "kode_barang = '" & .Rows(i).Item("Kode_Bahan") & "'"
                                            ExecuteTrans(SQL)
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        Exit Sub
                                    End If

                                End Using


                                Dim lewatin As String = "T"
                                SQL = "select isnull(round(sum(jumlah),4), 0) as stock from barang_sn where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and "
                                SQL = SQL & "kode_barang = '" & .Rows(i).Item("Kode_Bahan") & "' and jumlah <> 0 "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        If Dr("stock") < Val(jumlahConvertPckg) Then
                                            lewatin = "Y"
                                        Else
                                            lewatin = "T"
                                        End If
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & .Rows(i).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '==================================================================================
                                '======================  CHECK APAKAH FLAG POTONG STOK NYA Y atau T ================
                                '==================================================================================
                                'If LvPotStokPckg = "Y" Then
                                If lewatin = "T" Then
                                    Dim sisa As Double = 0
                                    SQL = "select kode_stock_owner, kode_barang, serial_number, dbo.get_hpp(Serial_Number) as HPP, round(jumlah,4) as jumlah from barang_sn where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and "
                                    SQL = SQL & "kode_barang = '" & .Rows(i).Item("Kode_Bahan") & "' and jumlah <> 0 "
                                    SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                                    Using Dss = BindingTrans(SQL)
                                        If Dss.Tables("MyTable").Rows.Count <> 0 Then
                                            sisa = Val(jumlahConvertPckg)
                                            For h As Integer = 0 To Dss.Tables("MyTable").Rows.Count - 1
                                                If sisa = 0 Then
                                                    Exit For
                                                ElseIf sisa < 0 Then
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If

                                                Dim hpp As Double = Dss.Tables("MyTable").Rows(h).Item("HPP")

                                                If sisa < Dss.Tables("MyTable").Rows(h).Item("jumlah") Or sisa = Dss.Tables("MyTable").Rows(h).Item("jumlah") Then
                                                    SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    SQL = SQL & "kode_stock_owner = '" & Dss.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "' and "
                                                    SQL = SQL & "kode_barang = '" & Dss.Tables("MyTable").Rows(h).Item("kode_barang") & "' and "
                                                    SQL = SQL & "serial_number = '" & Dss.Tables("MyTable").Rows(h).Item("serial_number") & "'"
                                                    ExecuteTrans(SQL)

                                                    SQL = "INSERT INTO Emi_Production_Results_Packaging_Det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                                                    SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                                                    SQL = SQL & "'" & Dss.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "','" & Dss.Tables("MyTable").Rows(h).Item("kode_barang") & "',"
                                                    SQL = SQL & "" & sisa & ",'" & Dss.Tables("MyTable").Rows(h).Item("serial_number") & "', '" & x_ident_currentPackaging & "')"
                                                    ExecuteTrans(SQL)

                                                    Nilai_Packaging = Nilai_Packaging + (hpp * sisa)
                                                    sisa = 0
                                                ElseIf sisa > Dss.Tables("MyTable").Rows(h).Item("jumlah") Then
                                                    SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    SQL = SQL & "kode_stock_owner = '" & Dss.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "' and "
                                                    SQL = SQL & "kode_barang = '" & Dss.Tables("MyTable").Rows(h).Item("kode_barang") & "' and "
                                                    SQL = SQL & "serial_number = '" & Dss.Tables("MyTable").Rows(h).Item("serial_number") & "'"
                                                    ExecuteTrans(SQL)

                                                    SQL = "INSERT INTO Emi_Production_Results_Packaging_Det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                                                    SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                                                    SQL = SQL & "'" & Dss.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "','" & Dss.Tables("MyTable").Rows(h).Item("kode_barang") & "',"
                                                    SQL = SQL & "" & Dss.Tables("MyTable").Rows(h).Item("jumlah") & ",'" & Dss.Tables("MyTable").Rows(h).Item("serial_number") & "', '" & x_ident_currentPackaging & "')"
                                                    ExecuteTrans(SQL)

                                                    Nilai_Packaging = Nilai_Packaging + (hpp * Val(HilangkanTanda(Format(Dss.Tables("MyTable").Rows(h).Item("jumlah"), "N4"))))
                                                    sisa = sisa - Val(HilangkanTanda(Format(Dss.Tables("MyTable").Rows(h).Item("jumlah"), "N4")))
                                                Else
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & .Rows(i).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If

                                                If Math.Round(sisa, 4) <> 0 And h = Dss.Tables("MyTable").Rows.Count - 1 Then
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & .Rows(i).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            Next ' for barang sn
                                        End If 'count <> 0

                                    End Using
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & .Rows(i).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If



                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Packaging Terhadap No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            'KALO ADA INPUT AJA DIA GENERATE
            'TODO : Barcode FG
            If Val(HilangkanTanda(Txt_Jumlah.Text)) <> 0 Then

                Dim newBatch As String = Generate_Batch_FG(tgl_skg, Prefix, DtpExpired.Value, Tahun_MulaiProduksi)
                Dim newQrCode As String = Generate_QR_Batch(Txt_KdBarang.Text, newBatch)
                Dim Kode_Berjalan As String = Generate_Random_Kode(10)
                Dim Kode_Asal As String = Kode_Berjalan

                '=========================
                '=      INSERT DATA      =
                '=========================
                SQL = "insert into Emi_Produksi_Hasil_Perpallet (Kode_Perusahaan, No_Split, Lokasi, Tanggal, Jam, "
                SQL = SQL & "UserID, Kode_Stock_Owner, Kode_Barang, Jumlah, Satuan, Batch_Number, "
                SQL = SQL & "Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Jenis, Tgl_Expired, tgl_produksi) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoSplit.Text & "', '" & Cmb_Lokasi.SelectedItem & "', "
                SQL = SQL & " '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
                SQL = SQL & "'" & arrSO.Item(Cmb_LokasiSimpan.SelectedIndex) & "', '" & Txt_KdBarang.Text & "', '" & Txt_Jumlah.Text & "', "
                SQL = SQL & " '" & Cmb_Satuan.Text & "', '" & newBatch & "', '" & newQrCode & "', '" & Kode_Berjalan & "', "
                SQL = SQL & " '" & Kode_Asal & "', '" & kategoriQuality.Item(CmbJenis.SelectedIndex) & "', "
                SQL = SQL & " '" & Format(DtpExpired.Value, "yyyy-MM-dd") & "',  '" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "') "
                ExecuteTrans(SQL)


                Dim MetodePengeluaranStok As String = ""
                SQL = "select  Metode_Pengeluaran_Stok from Barang where kode_perusahaan  = '" & KodePerusahaan & "'  "
                SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text.Trim & "' and  Kode_Stock_Owner = '" & arrSO.Item(Cmb_LokasiSimpan.SelectedIndex) & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        MetodePengeluaranStok = Dr("metode_pengeluaran_stok")
                    Else

                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Metode Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using



                '==================================
                '=      GENERATE NEW BARCODE      =
                '==================================
                'HAPUS TABEL SEMENTARA
                SQL = "truncate table Cetak_Finish_Good "
                ExecuteTrans(SQL)

                kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

                Dim fullNewQr As String = newQrCode & "-" & Kode_Berjalan

                Barcode.Image = Generate_QR(fullNewQr)

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
                Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1


                'INSERT TABEL CETAK QR
                SQL = "insert into Cetak_Finish_Good (Kode_Perusahaan, Kode_Barang, Barcode, QrUtuh, Qr, Tgl_Expired, batch, tgl_produksi, kode_unik_print, tanggal_masuk,metode_pengeluaran_stok) values "
                'SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_KdBarang.Text & "', @newBarcode, '" & Txt_NamaBarang.Text & "', "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_KdBarang.Text & "', @newBarcode, "
                SQL = SQL & "'" & fullNewQr & "', '" & newQrCode & "', '" & Format(Date.Parse(DtpExpired.Value), "yyyy-MM-dd") & "', '" & newBatch & "',  '" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & kode_unik_print & "', '" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "', '" & MetodePengeluaranStok & "'"
                SQL = SQL & ")"
                ExecuteTrans(SQL)

            End If



            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''=================================================================================================================='''

            '''======================================== KODINGAN DARI PENERIMAAN BARANG ========================================='''

            '''=================================================================================================================='''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim SoProduction As String
            Dim berat As Double = 0
            SQL = "select Kode_Stock_Owner From Stock_Owner_Gudang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Produksi = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    SoProduction = Dr("kode_stock_owner")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Lokasi Produksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select top(1) berat From barang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_barang='" & Txt_KdBarang.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    berat = Dr("berat")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Lokasi Produksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            If TxtJmlScrap.Text = "" Then TxtJmlScrap.Text = "0"

            Dim badstock As Double = 0
            Dim goodstock As Double = 0

            If Not CmbJenis.SelectedIndex = -1 Then
                If kategoriQuality.Item(CmbJenis.SelectedIndex) = "KUNING" Then
                    goodstock = Val(HilangkanTanda(Txt_Jumlah.Text))
                    badstock = 0
                Else
                    goodstock = 0
                    badstock = Val(HilangkanTanda(Txt_Jumlah.Text))
                End If

            Else
                goodstock = 0
                badstock = 0
            End If



            Dim kd_barang_scrap As String = ""

            If Val(TxtJmlScrap.Text) = 0 Then
                kd_barang_scrap = "NULL"
            Else
                kd_barang_scrap = "'" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "'"
            End If



            SQL = "insert into emi_production_results_detail_barang(kode_perusahaan,no_transaksi,proses,tanggal,jam,userid,kode_Stock_owner,"
            SQL = SQL & "kode_barang, qty_hasil_produksi, qty_good_stock, qty_bad_stock, satuan, qty_scrap, satuan_scrap, Kode_Barang_Scrap) values("
            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
            SQL = SQL & "'" & SoProduction & "', '" & Txt_KdBarang.Text & "','" & Txt_Jumlah.Text & "', '" & goodstock & "', "
            SQL = SQL & "'" & badstock & "', '" & Cmb_Satuan.Text & "', '" & TxtJmlScrap.Text & "', '" & CmbSatScrap.Text & "' ," & kd_barang_scrap & ") "
            ExecuteTrans(SQL)





            'Get data Barang berdasarkan NoSplit
            Dim Kd_So As String = ""
            Dim Kd_Brg As String = ""
            SQL = "Select b.Status,b.Selesai,b.Kode_Stock_Owner,b.Kode_Barang "
            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b "
            SQL = SQL & "where a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoSplit.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Kd_So = dr("Kode_Stock_Owner")
                    Kd_Brg = dr("Kode_Barang")
                    If General_Class.CekNULL(dr("Status")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_NoFaktur & " " & Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using



            Dim fbulan As String = Format(tgl_skg, "MM")
            Dim ftahun As String = Format(tgl_skg, "yyyy")

            Dim nilai_Produksi As Double = 0

            Dim TotalTf As Double = 0
            Dim TotalHPP As Double = 0
            Dim TotalHPPScrap As Double = 0
#Region "INSERT PRODUKSI"


            '==============================
            '=     INSERT KE PRODUKSI     =
            '==============================

            ''GET DATA DI Emi_Produksi_Hasil_Perpallet
            SQL = "select a.No_Split, a.Kode_Unik_Berjalan, a.Kode_Unik_Asal, a.Qr_Code, a.Jumlah, a.Satuan, a.Batch_Number, a.Kode_Barang, a.ID as urut_oto, a.jenis, a.Serial_Number, Tanggal, Tgl_Expired "
            SQL = SQL & "from Emi_Produksi_Hasil_Perpallet a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Split='" & Txt_NoSplit.Text & "' and flag_simpan_pallet is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Warna_stock As String = ""

                            Warna_stock = .Rows(i).Item("Jenis")


                            '====================================
                            '=       CONVERT SATUAN KECIL       =
                            '====================================
                            Dim nilai_kecildetail As Double = Val(HilangkanTanda(Format(Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), .Rows(i).Item("Satuan"), TxtSatuanKecil.Text, .Rows(i).Item("Jumlah")), "N4")))


                            ''GET ID_WAREHOUSE YG KOSONG
                            Dim available_Id_Warehouse As String = ""
                            Dim available_NoPallet As String = ""

                            SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                            SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                            SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                            SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                            SQL = SQL & "and a.Kode_Stock_Owner='" & Kd_So & "' and b.Kode_Barang is null"
                            Using Dr2 = OpenTrans(SQL)
                                Do While Dr2.Read
                                    available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                                    available_NoPallet = Dr2("nomor_urut")
                                Loop
                            End Using


                            'Cek HPP Per Dosing
                            Dim sisa_barang As Double = nilai_kecildetail
                            SQL = "Select b.jumlah_dosing, b.jumlah_dosing_pcs, b.jumlah_dosing - b.jumlah_Terpakai As sisa, Total_bahan_baku, "
                            SQL = SQL & "total_biaya_produksi, nilai_loss_production, total_packaging, b.urut "
                            SQL = SQL & "From EMI_Production_Results a, Emi_Production_Results_HPP b Where "
                            SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
                            SQL = SQL & "And round(jumlah_dosing,4)<>round(jumlah_terpakai,4) "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
                            SQL = SQL & "and b.Tanggal is not null "
                            SQL = SQL & "order by proses "
                            Using dss = BindingTrans(SQL)
                                For ind = 0 To dss.Tables("MyTable").Rows.Count - 1

                                    If sisa_barang = 0 Then
                                        Exit For
                                    End If
                                    Dim urut_HPP As String = dss.Tables("MyTable").Rows(ind).Item("urut")
                                    Dim sisa As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("sisa"), "N4")))

                                    Dim jumlah_dosing As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("jumlah_dosing"), "N4")))
                                    Dim jumlah_dosing_pcs As Double = dss.Tables("MyTable").Rows(ind).Item("jumlah_dosing_pcs")


                                    'Ubah Nilai Dosing(Nilai Dosing Dalam KG), ke Nilai Pcs
                                    Dim nilai_sisa_Pcs As Double = Math.Floor(Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), "KG", TxtSatuanKecil.Text, sisa))

                                    Dim nilai_potong As Double = 0
                                    If nilai_sisa_Pcs > sisa_barang Then

                                        nilai_potong = sisa_barang
                                        sisa_barang -= sisa_barang

                                    Else

                                        nilai_potong = nilai_sisa_Pcs
                                        sisa_barang -= nilai_sisa_Pcs

                                    End If

                                    SQL = "insert into Emi_Production_Results_Detail_Pallet (Kode_Perusahaan, No_Transaksi, Kode_Unik_Berjalan, Kode_Unik_Asal, Qr_Code, Jumlah, Satuan, NIlai_Barang, "
                                    SQL = SQL & "Satuan_Barang, Batch_Number, Id_Warehouse, Nomor_Pallet, proses, serial_number, Jenis, Tgl_Produksi, Tgl_Expired, Urut_HPP) values "
                                    SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', '" & nilai_potong & "', '" & TxtSatuanKecil.Text & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', '" & proses & "', '', '" & .Rows(i).Item("Jenis") & "', "
                                    SQL = SQL & " '" & .Rows(i).Item("Tanggal") & "','" & .Rows(i).Item("Tgl_Expired") & "', '" & urut_HPP & "') "
                                    ExecuteTrans(SQL)

                                    Dim x_ident_currentPallet As Integer = 0
                                    SQL = "select IDENT_CURRENT('Emi_Production_Results_Detail_Pallet') as urutan"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            x_ident_currentPallet = Dr("urutan")
                                        End If
                                    End Using

                                    Dim Kode_barang_inq As String = ""
                                    SQL = "select top(1) Kode_Barang_inq from barang where "
                                    SQL = SQL & "kode_perusahaan='" & KodePerusahaan & "' and kode_barang='" & Txt_KdBarang.Text & "' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Kode_barang_inq = Dr("Kode_Barang_inq")
                                        End If
                                    End Using




                                    Dim nilai_bahan_KG As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("Total_bahan_baku") / jumlah_dosing, "N0")))
                                    Dim nilai_biaya_KG As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("total_biaya_produksi") / jumlah_dosing, "N0")))
                                    Dim nilai_loss_KG As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("nilai_loss_production") / jumlah_dosing, "N0")))

                                    Dim nilai_packaging_Pcs As Double = Val(HilangkanTanda(Format(Nilai_Packaging / nilai_potong, "N0")))

                                    Dim nilai_HPP_pcs As Double = Val(HilangkanTanda(Format((((nilai_bahan_KG + nilai_biaya_KG + nilai_loss_KG) * berat) / 1000) + nilai_packaging_Pcs, "N0")))

                                    TotalHPP += (nilai_HPP_pcs * nilai_potong)

                                    Dim Rand As New Random
                                    Dim str As String = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                                    Dim Kode_Unik As String = str.Substring(0, 5) & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                    Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & nilai_HPP_pcs & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")


                                    SQL = "Update barang set "
                                    SQL = SQL & "Good_Stock = Good_Stock + " & nilai_potong & " "
                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and kode_barang = '" & Kd_Brg & "'"
                                    ExecuteTrans(SQL)

                                    'Cek Apakah Sn baru sama dengan Sn pada Barang Split
                                    SQL = "select kode_barang from barang_sn where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and "
                                    SQL = SQL & "kode_barang = '" & Kd_Brg & "' and serial_number = '" & SN & "'"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi kesalahan pada Barang . . !, Silahkan Ulangi Transaksi . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else

                                            'Insert Barang SN
                                            SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                                            SQL = SQL & "serial_number, Jumlah, Warna, Kode_Unik_Berjalan, Kode_Unik_Asal, "
                                            SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
                                            SQL = SQL & "'" & Kd_So & "', '" & Kd_Brg & "', "
                                            SQL = SQL & "'" & SN & "', " & nilai_potong & ", '" & Warna_stock & "', "
                                            SQL = SQL & "'" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', '" & .Rows(i).Item("Qr_Code") & "', "
                                            SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "','" & .Rows(i).Item("Tanggal") & "','" & .Rows(i).Item("Tgl_Expired") & "')"
                                            Dr.Close()
                                            ExecuteTrans(SQL)
                                        End If
                                    End Using

                                    SQL = "Update Emi_Production_Results_Detail_Pallet set "
                                    SQL = SQL & "serial_number = '" & SN & "' "
                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "Urut_oto = '" & x_ident_currentPallet & "' "
                                    ExecuteTrans(SQL)


                                    'Nilai yg di pakai, di kembalikan ke satuan dosing(KG)
                                    Dim nilai_pakai As Double = Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), TxtSatuanKecil.Text, "KG", nilai_potong)
                                    SQL = "Update Emi_Production_Results_HPP set "
                                    SQL = SQL & "jumlah_Terpakai = jumlah_Terpakai + " & nilai_pakai & " "
                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "Urut = '" & urut_HPP & "' "
                                    ExecuteTrans(SQL)

                                Next
                            End Using

                            If sisa_barang <> 0 Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan ! ! . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If


                            TotalTf = TotalTf + nilai_kecildetail

                        Next

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
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' And a.Kode_Stock_Owner = '" & Kd_So & "' AND a.Kode_Barang = '" & Kd_Brg & "' "
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

                    End If
                End With
            End Using

#End Region





            '===============================================================
            '=     GET Emi_Production_Results_Detail_Pallet SEBELUMNYA     =
            '===============================================================
            SQL = "select a.No_Transaksi, a.Proses, a.Jumlah, a.NIlai_Barang, a.Satuan, a.Satuan_Barang, a.Id_Warehouse, a.Serial_Number, a.Jenis, a.Nomor_Pallet, a.Proses, a.urut_oto, Kode_Unik_Berjalan, Kode_Unik_Asal, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired "
            SQL = SQL & "from Emi_Production_Results_Detail_Pallet a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi ='" & TxtFormulator_NoFaktur.Text & "' "
            SQL = SQL & "and a.Proses = '" & proses & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim SnLama As String = .Rows(i).Item("Serial_Number")
                            Dim WarnaLama As String = .Rows(i).Item("Jenis")
                            Dim HargaHpp2 As String = Get_Harga_SN(SnLama)

                            'Generate Sn Baru
                            Dim Rand2 As New Random
                            Dim sts2 As String = Format(Rand2.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                            Dim Kode_Unik2 As String = sts2.Substring(0, 5) & Chr(64 + sts2.Substring(6, 1)) & sts2.Substring(6, Len(sts2) - 6)
                            Dim SN2 As String = Kode_Unik2 & Tanda_SN & "01" & Tanda_SN & HargaHpp2 & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                            '================================
                            '=     POTONG SN SEBELUMNYA     =
                            '================================
                            SQL = "update Barang_SN set Jumlah = Jumlah - " & .Rows(i).Item("NIlai_Barang") & " where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SnLama & "' "
                            SQL = SQL & "and Warna = '" & WarnaLama & "'"
                            ExecuteTrans(SQL)

                            SQL = "Update barang set "
                            SQL = SQL & "Good_Stock = Good_Stock - " & .Rows(i).Item("NIlai_Barang") & " "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and kode_barang = '" & Kd_Brg & "'"
                            ExecuteTrans(SQL)

                            SQL = "SELECT "
                            SQL = SQL & "ROUND(SUM(good_stock), 4) AS good_stock, "
                            SQL = SQL & "ISNULL((SELECT ROUND(SUM(jumlah), 4) FROM Barang_sn x WHERE a.kode_Barang = x.kode_Barang AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.kode_Perusahaan = x.kode_Perusahaan), 0) AS Jumlah_sn, "
                            SQL = SQL & "ISNULL(ROUND(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "ISNULL((SELECT ROUND(SUM(Jumlah_Bags), 4) FROM Barang_sn y WHERE a.kode_Barang = y.kode_Barang AND a.Kode_Stock_Owner = y.Kode_Stock_Owner AND a.kode_Perusahaan = y.Kode_Perusahaan), 0) AS jumlah_bags_sn "
                            SQL = SQL & "FROM "
                            SQL = SQL & "barang a "
                            SQL = SQL & "WHERE "
                            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' And a.Kode_Stock_Owner = '" & Kd_So & "' AND a.Kode_Barang = '" & Kd_Brg & "' "
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

                            '==========================
                            '=     CEK SN HARUS 0     =
                            '==========================
                            SQL = "select Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SnLama & "' and Warna = '" & WarnaLama & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(Dr("Jumlah")) <> 0 Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                            End Using

                            SQL = "update Emi_Production_Results_Detail_Pallet set SN_Baru ='" & SN2 & "', Lokasi_Gudang='" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and urut_oto = '" & .Rows(i).Item("urut_oto") & "'"
                            ExecuteTrans(SQL)

                            '================================
                            '=     GET WAREHOUSE KOSONG     =
                            '================================
                            Dim available_Id_Warehouse2 As String = ""
                            Dim available_NoPallet2 As String = ""

                            SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                            SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                            SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                            SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                            SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "' and b.Kode_Barang is null"
                            Using Dr2 = OpenTrans(SQL)
                                If Dr2.Read Then
                                    available_Id_Warehouse2 = Dr2("id_wms_warehouse_position")
                                    available_NoPallet2 = Dr2("nomor_urut")
                                Else
                                    Dr2.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Rak sudah Penuh")
                                    Exit Sub
                                End If
                            End Using

                            SQL = "insert into barang_sn_sementara(kode_perusahaan, kode_stock_owner, kode_barang, "
                            SQL = SQL & "serial_number, Jumlah, Warna, Kode_Unik_Berjalan, Kode_Unik_Asal, "
                            SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet, Flag_Produksi, Flag_QI, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
                            SQL = SQL & "'" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "', '" & Kd_Brg & "', "
                            SQL = SQL & "'" & SN2 & "', " & .Rows(i).Item("NIlai_Barang") & ", '" & WarnaLama & "', "
                            SQL = SQL & "'" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', '" & .Rows(i).Item("Qr_Code") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse2 & "', '" & available_NoPallet2 & "', 'Y', 'Y', "
                            SQL = SQL & " '" & .Rows(i).Item("Tgl_Produksi") & "', '" & .Rows(i).Item("Tgl_Expired") & "')"
                            ExecuteTrans(SQL)



                        Next
                    End If
                End With
            End Using



            If Val(TxtJmlScrap.Text) <> 0 Then

                'GENERATE BAROCDE
                'TODO : Barcode Scrap
                Dim newBatchScrap As String = Generate_Batch_FG(tgl_skg, Prefix, DtpExpired.Value, Tahun_MulaiProduksi)
                Dim newQrCodeScrap As String = Generate_QR_Batch(arrKdBarangSrap(CmbSisaProduksi.SelectedIndex), newBatchScrap)
                Dim Kode_BerjalanScrap As String = Generate_Random_Kode(10)
                Dim Kode_AsalScrap As String = Kode_BerjalanScrap


                Dim nilai_kecildetail As Double = 0
                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa', '" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "', '" & CmbSatScrap.Text & "', "
                SQL = SQL & "'" & TxtSatScrapKecil.Text & " ', '" & TxtJmlScrap.Text & "' ) as hasil "
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If

                        nilai_kecildetail = Val(HilangkanTanda(Format(Dr1("hasil"), "N4")))
                    Else
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using

                Dim available_Id_Warehouse As String = ""
                Dim available_NoPallet As String = ""

                SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner='" & Kd_So & "' and b.Kode_Barang is null"
                Using Dr2 = OpenTrans(SQL)
                    Do While Dr2.Read
                        available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                        available_NoPallet = Dr2("nomor_urut")
                    Loop
                End Using

                Dim sisa_barang As Double = 0
                sisa_barang = nilai_kecildetail
                SQL = "Select b.jumlah_dosing, b.jumlah_dosing_pcs, b.jumlah_dosing - b.jumlah_Terpakai As sisa, Total_bahan_baku, "
                SQL = SQL & "total_biaya_produksi, nilai_loss_production, total_packaging, b.urut "
                SQL = SQL & "From EMI_Production_Results a, Emi_Production_Results_HPP b Where "
                SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
                SQL = SQL & "And round(jumlah_dosing,4)<>round(jumlah_terpakai,4) "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
                SQL = SQL & "and b.Tanggal is not null "
                SQL = SQL & "order by proses "
                Using dss = BindingTrans(SQL)
                    For ind = 0 To dss.Tables("MyTable").Rows.Count - 1

                        Dim urut_HPP As String = dss.Tables("MyTable").Rows(ind).Item("urut")
                        Dim sisa As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("sisa"), "N4")))

                        Dim jumlah_dosing As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("jumlah_dosing"), "N4")))
                        Dim jumlah_dosing_pcs As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("jumlah_dosing_pcs"), "N4")))
                        Dim nilai_bahan_KG As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("Total_bahan_baku") / jumlah_dosing, "N0")))
                        Dim nilai_biaya_KG As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("total_biaya_produksi") / jumlah_dosing, "N0")))
                        Dim nilai_loss_KG As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("nilai_loss_production") / jumlah_dosing, "N0")))

                        Dim nilai_HPP_pcs As Double = Math.Round(nilai_bahan_KG + nilai_biaya_KG + nilai_loss_KG, 0)

                        Dim Rand As New Random
                        Dim str As String = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                        Dim Kode_Unik As String = str.Substring(0, 5) & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                        Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & nilai_HPP_pcs & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")


                        Dim nilai_potong As Double = 0
                        If sisa > sisa_barang Then

                            nilai_potong = sisa_barang
                            sisa_barang -= sisa_barang

                        Else

                            nilai_potong = sisa
                            sisa_barang -= sisa

                        End If

                        TotalHPPScrap += Math.Round(nilai_potong * nilai_HPP_pcs, 0)

                        SQL = "Update barang set "
                        SQL = SQL & "good_stock = good_stock + " & nilai_potong & " "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and kode_barang = '" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "'"
                        ExecuteTrans(SQL)

                        SQL = "select kode_barang from barang_sn where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and "
                        SQL = SQL & "kode_barang = '" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "' and serial_number = '" & SN & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi kesalahan pada Barang . . !, Silahkan Ulangi Transaksi . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else
                                SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                                SQL = SQL & "serial_number, jumlah, Kode_Unik_Berjalan, Kode_Unik_Asal, "
                                SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet) values('" & KodePerusahaan & "', "
                                SQL = SQL & "'" & Kd_So & "', '" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "', "
                                SQL = SQL & "'" & SN & "', " & nilai_potong & ", '" & "X" & "', "
                                SQL = SQL & "'" & "X" & "', '" & "X" & "', "
                                SQL = SQL & "'" & "X" & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "')"
                                Dr.Close()
                                ExecuteTrans(SQL)
                            End If
                        End Using

                        SQL = "insert into Emi_Production_Results_Detail_Scrap (Kode_Perusahaan, No_Transaksi, Kode_Unik_Berjalan, Kode_Unik_Asal, Qr_Code, Jumlah, Satuan, NIlai_Barang, "
                        SQL = SQL & "Satuan_Barang, Batch_Number, Id_Warehouse, Nomor_Pallet,proses, serial_number, Urut_HPP) values "
                        SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & Kode_BerjalanScrap & "', '" & Kode_AsalScrap & "', "
                        SQL = SQL & "'" & newQrCodeScrap & "', '" & TxtJmlScrap.Text & "', '" & CmbSatScrap.Text & "', '" & nilai_potong & "', '" & TxtSatScrapKecil.Text & "', "
                        SQL = SQL & "'" & newBatchScrap & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', '" & proses & "', '" & SN & "', '" & urut_HPP & "') "
                        ExecuteTrans(SQL)

                        SQL = "Update Emi_Production_Results_HPP set "
                        SQL = SQL & "jumlah_Terpakai = jumlah_Terpakai + " & nilai_potong & " "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "Urut = '" & urut_HPP & "' "
                        ExecuteTrans(SQL)

                    Next
                End Using

                If sisa_barang <> 0 Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Terjadi Kesalahan ! ! . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If


                Dim MetodePengeluaranStokScrap As String = ""
                SQL = "select top 1 Metode_Pengeluaran_Stok from Barang where kode_perusahaan  = '" & KodePerusahaan & "'  "
                SQL = SQL & "and Kode_Barang = '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        MetodePengeluaranStokScrap = Dr("metode_pengeluaran_stok")
                    Else

                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Metode Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                '==================================
                '=      GENERATE NEW BARCODE      =
                '==================================
                'HAPUS TABEL SEMENTARA
                If Txt_Jumlah.Text.Trim.Length = 0 Then
                    SQL = "truncate table Cetak_Finish_Good "
                    ExecuteTrans(SQL)
                End If

                kode_unik_print_scrap = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

                Dim fullNewQrScrap As String = newQrCodeScrap & "-" & Kode_BerjalanScrap

                Barcode.Image = Generate_QR(fullNewQrScrap)

                Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print_scrap & ".jpg")

                '   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

                'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
                Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
                'End If

                fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                FileSize1 = fs1.Length
                rawData1 = New Byte(FileSize1) {}
                fs1.Read(rawData1, 0, FileSize1)
                fs1.Close()
                Cmd.Parameters.Add("@newBarcodescrap", SqlDbType.Image).Value = rawData1


                'INSERT TABEL CETAK QR
                SQL = "insert into Cetak_Finish_Good (Kode_Perusahaan, Kode_Barang, Barcode, QrUtuh, Qr, Tgl_Expired, batch, tgl_produksi, kode_unik_print, tanggal_masuk,metode_pengeluaran_stok) values "
                'SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_KdBarang.Text & "', @newBarcode, '" & Txt_NamaBarang.Text & "', "
                SQL = SQL & "('" & KodePerusahaan & "', '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "', @newBarcodescrap, "
                SQL = SQL & "'" & fullNewQrScrap & "', '" & newQrCodeScrap & "', '" & Format(Date.Parse(DtpExpired.Value), "yyyy-MM-dd") & "', '" & newBatchScrap & "',  '" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & kode_unik_print_scrap & "', '" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "', '" & MetodePengeluaranStokScrap & "'"
                SQL = SQL & ")"
                ExecuteTrans(SQL)







            End If


            SQL = "update Emi_Produksi_Hasil_Perpallet set flag_simpan_pallet = 'Y'  where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Split = '" & Txt_NoSplit.Text & "' "
            ExecuteTrans(SQL)

            Dim inisial_faktur_dari As String = ""
            Dim fso As String = ""
            SQL = "Select b.Inisial_Faktur,a.Kode_Stock_Owner from Emi_Split_Production_Order a,Stock_Owner_Gudang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "And a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Txt_NoSplit.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    inisial_faktur_dari = Dr("inisial_faktur")
                    fso = Dr("Kode_Stock_Owner")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim fFlag_Semi_FG As String = ""
            Dim fFlag_Finished_Good As String = ""

            SQL = "select b.Kode_Barang,d.Flag_Semi_FG,d.Flag_Finished_Good "
            SQL = SQL & "from Emi_Production_Results a,Emi_Split_Production_Order b,Barang c,EMI_Group_Jenis d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and b.Kode_Barang = c.Kode_Barang and c.Kode_Perusahaan = d.Kode_Perusahaan and a.status is null "
            SQL = SQL & "and c.Id_Group_Jenis = d.Id_Group_Jenis and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1
                            fFlag_Semi_FG = .Rows(h).Item("Flag_Semi_FG")
                            fFlag_Finished_Good = .Rows(h).Item("Flag_Finished_Good")
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            Dim Akun_Persediaan_Dalam_Proses As String = ""

            Dim Akun_PersediaanScrap As String = ""
            Dim akun_HPP_FG As String = ""
            Dim Akun_HPPScrap As String = ""
            Dim Akun_Pembulatan_FG As String = ""


            Dim keterangan2 As String = ""
            Dim keterangan3 As String = ""






            SQL = "select HPP_Barang_Setengah_Jadi, Persediaan_Barang_Dalam_Proses, "
            SQL = SQL & "HPP,Pembulatan_Finished_Good,Pembulatan_Semi_FG, Persediaan_Scrap from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Akun_PersediaanScrap = Dr("Persediaan_Scrap")
                    Akun_HPPScrap = Dr("Persediaan_Scrap")

                    Akun_Persediaan_Dalam_Proses = Dr("Persediaan_Barang_Dalam_Proses")

                    If fFlag_Semi_FG = "Y" Then



                        akun_HPP_FG = Dr("HPP_Barang_Setengah_Jadi")
                        keterangan2 = "HPP Barang Setengah Jadi "

                        Akun_Pembulatan_FG = Dr("Pembulatan_Semi_FG")
                        keterangan3 = "Pembulatan HPP Barang Setengah Jadi "
                    ElseIf fFlag_Finished_Good = "Y" Then


                        akun_HPP_FG = Dr("HPP")
                        keterangan2 = "HPP Barang Jadi "

                        Akun_Pembulatan_FG = Dr("Pembulatan_Finished_Good")
                        keterangan3 = "Pembulatan HPP Barang Jadi "
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If TotalHPP <> 0 Then

                Dim Akun_Persediaan As String = ""
                Dim keterangan As String = ""
                SQL = "select c.akun_Persediaan, a.kode_group_jenis "
                SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.kode_stock_owner = '" & arrSO.Item(Cmb_LokasiSimpan.SelectedIndex) & "' and b.Kode_Barang='" & Txt_KdBarang.Text & "' "
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

#Region "Jurnal 1"


                'HPP Pada Barang Dalam Proses

                Dim Kode_voucher As String = ""
                Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                Dim pagenumber As Integer = 1

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_voucher & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Penerimaan Barang Jadi " & TxtFormulator_NoFaktur.Text & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                'Insert HPP Total
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_HPP_FG, 1),
                     Strings.Mid(akun_HPP_FG, 2, 1),
                     Strings.Mid(Ganti(akun_HPP_FG), 3),
                     KodePerusahaan, KodeProyek, keterangan2 & TxtFormulator_NoFaktur.Text, TotalHPP, "0", pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1



                'Insert Data Bahan dan Packaging yg dipakai
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Persediaan_Dalam_Proses, 1),
                                  Strings.Mid(Akun_Persediaan_Dalam_Proses, 2, 1),
                                  Strings.Mid(Ganti(Akun_Persediaan_Dalam_Proses), 3),
                                  KodePerusahaan, KodeProyek, "Persediaan Dalam Proses " & TxtFormulator_NoFaktur.Text, "0", TotalHPP, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

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

                SQL = "insert into Emi_Production_Results_Jurnal (Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses, Jenis) values ("
                SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & Kode_voucher & "',"
                SQL = SQL & "'" & proses & "', 'GR1') "
                ExecuteTrans(SQL)

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
                SQL = SQL & "'" & KodeProyek & "', 'Pengeluaran Bahan Baku " & TxtFormulator_NoFaktur.Text & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                'Insert HPP Total
                SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(Akun_Persediaan, 1),
                     Strings.Mid(Akun_Persediaan, 2, 1),
                     Strings.Mid(Ganti(Akun_Persediaan), 3),
                     KodePerusahaan, KodeProyek, keterangan2 & TxtFormulator_NoFaktur.Text, TotalHPP, "0", pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber2 = pagenumber2 + 1



                'Insert Data Bahan dan Packaging yg dipakai
                SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_HPP_FG, 1),
                                  Strings.Mid(akun_HPP_FG, 2, 1),
                                  Strings.Mid(Ganti(akun_HPP_FG), 3),
                                  KodePerusahaan, KodeProyek, "Persediaan Dalam Proses " & TxtFormulator_NoFaktur.Text, "0", TotalHPP, pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber2 = pagenumber2 + 1

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

                SQL = "insert into Emi_Production_Results_Jurnal (Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses, Jenis) values ("
                SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & Kode_voucher2 & "',"
                SQL = SQL & "'" & proses & "', 'GR2') "
                ExecuteTrans(SQL)
#End Region
            End If


#Region "Jurnal 3"

            'Persediaan Scrap Pada Barang Dalam Proses
            If TotalHPPScrap <> 0 Then
                Dim Kode_voucher3 As String = ""
                Kode_voucher3 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                Dim pagenumber3 As Integer = 1

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_voucher3 & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Pengeluaran Bahan Baku " & TxtFormulator_NoFaktur.Text & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                'Insert HPP Total
                SQL = Get_Detail_Jurnal(Kode_voucher3, Strings.Left(Akun_PersediaanScrap, 1),
                         Strings.Mid(Akun_PersediaanScrap, 2, 1),
                         Strings.Mid(Ganti(Akun_PersediaanScrap), 3),
                         KodePerusahaan, KodeProyek, keterangan2 & TxtFormulator_NoFaktur.Text, TotalHPPScrap, "0", pagenumber3, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber3 = pagenumber3 + 1



                'Insert Data Bahan dan Packaging yg dipakai
                SQL = Get_Detail_Jurnal(Kode_voucher3, Strings.Left(Akun_Persediaan_Dalam_Proses, 1),
                                      Strings.Mid(Akun_Persediaan_Dalam_Proses, 2, 1),
                                      Strings.Mid(Ganti(Akun_Persediaan_Dalam_Proses), 3),
                                      KodePerusahaan, KodeProyek, "Persediaan Dalam Proses " & TxtFormulator_NoFaktur.Text, "0", TotalHPPScrap, pagenumber3, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber3 = pagenumber3 + 1

                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher3 & "'"
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

                SQL = "insert into Emi_Production_Results_Jurnal (Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses, Jenis) values ("
                SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & Kode_voucher3 & "',"
                SQL = SQL & "'" & proses & "', 'GR3') "
                ExecuteTrans(SQL)
            End If
#End Region


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        '===========================
        '=      CETAK BARCODE      =
        '===========================
        'TODO : Cetak
        If Val(HilangkanTanda(Txt_Jumlah.Text)) <> 0 Then

            Try
                OpenConn()
                Dim CrDoc As New Object

                Dim KertasBesar As String = "BarcodeFG"
                Dim KertasKecil As String = "BarcodeQC"

                SQL = "select Kode_Perusahaan from Cetak_Finish_Good where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & Txt_KdBarang.Text & "' and Kode_Unik_Print = '" & kode_unik_print & "' "
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

                            CrDoc = New NewBarcodeFinishGood

                            'With A_Place_For_Printing2
                            '    CrDoc.SetDataSource(Ds)
                            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            '    CrDoc.PrintOptions.PrinterName = ""
                            '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & Txt_KdBarang.Text & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                            '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                            '    .Text = "New Barcode Finish Good"
                            '    .CrystalReportViewer1.ReportSource = CrDoc
                            '    .Refresh()
                            '    .Show()
                            'End With

                            '=====================================================

                            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & Txt_KdBarang.Text & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                            CrDoc.PrintOptions.PrinterName = PrinterBarcode

                            doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                            Dim rawKind As Integer
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                    CrDoc.PrintOptions.PaperSize = rawKind
                                    Exit For
                                End If
                            Next

                            CrDoc.PrintToPrinter(1, False, 1, 2500)

                        Else
                            MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                        printerDitemukan = False


                        '==========================
                        '=     BARCODEE KECIL     =
                        '==========================
                        For Each printer As String In PrinterSettings.InstalledPrinters
                            If printer.ToLower() = PrinterBarcodeQC.ToLower() Then
                                printerDitemukan = True
                                Exit For
                            End If
                        Next

                        If printerDitemukan Then
                            CrDoc = New NewBarcodeFinishGoodKecil

                            'With A_Place_For_Printing2
                            '    CrDoc.SetDataSource(Ds)
                            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            '    CrDoc.PrintOptions.PrinterName = ""
                            '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & Txt_KdBarang.Text & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                            '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                            '    .Text = "New Barcode Finish Good"
                            '    .CrystalReportViewer1.ReportSource = CrDoc
                            '    .Refresh()
                            '    .Show()
                            'End With

                            '=======================================================

                            Dim doctoprint2 As New System.Drawing.Printing.PrintDocument()
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & Txt_KdBarang.Text & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                            CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC

                            doctoprint2.PrinterSettings.PrinterName = PrinterBarcodeQC

                            Dim rawKind2 As Integer
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For i = 0 To doctoprint2.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint2.PrinterSettings.PaperSizes(i).PaperName = KertasKecil Then
                                    rawKind2 = CInt(doctoprint2.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint2.PrinterSettings.PaperSizes(i)))
                                    CrDoc.PrintOptions.PaperSize = rawKind2
                                    Exit For
                                End If
                            Next

                            CrDoc.PrintToPrinter(1, False, 1, 2500)
                        End If

                        printerDitemukan = False

                    Else
                        MessageBox.Show("Printer QC Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End If
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        End If

        If Val(TxtJmlScrap.Text) <> 0 Then

            Try
                OpenConn()
                Dim CrDoc As New Object

                Dim KertasBesar As String = "BarcodeFG"
                Dim KertasKecil As String = "BarcodeQC"

                SQL = "select Kode_Perusahaan from Cetak_Finish_Good where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "' and Kode_Unik_Print = '" & kode_unik_print_scrap & "' "
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then


                        Dim printerDitemukan As Boolean = False
                        '==========================
                        '=     BARCODEE BESAR     =
                        '==========================
                        For Each printer As String In PrinterSettings.InstalledPrinters
                            If printer.ToLower() = PrinterBarcode.ToLower() Then
                                printerDitemukan = True
                                Exit For
                            End If
                        Next


                        If printerDitemukan Then
                            CrDoc = New NewBarcodeFinishGood

                            'With A_Place_For_Printing2
                            '    CrDoc.SetDataSource(Ds)
                            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            '    CrDoc.PrintOptions.PrinterName = ""
                            '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print_scrap & "'  "
                            '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                            '    .Text = "New Barcode Finish Good"
                            '    .CrystalReportViewer1.ReportSource = CrDoc
                            '    .Refresh()
                            '    .Show()
                            'End With

                            '============================================

                            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print_scrap & "'  "
                            CrDoc.PrintOptions.PrinterName = PrinterBarcode

                            doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                            Dim rawKind As Integer
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                    CrDoc.PrintOptions.PaperSize = rawKind
                                    Exit For
                                End If
                            Next

                            CrDoc.PrintToPrinter(1, False, 1, 2500)

                        Else
                            MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        End If

                        printerDitemukan = False

                        '==========================
                        '=     BARCODEE KECIL     =
                        '==========================
                        For Each printer As String In PrinterSettings.InstalledPrinters
                            If printer.ToLower() = PrinterBarcodeQC.ToLower() Then
                                'printerDitemukan = True
                                Exit For
                            End If
                        Next
                        CrDoc = New NewBarcodeFinishGoodKecil

                        If printerDitemukan Then

                            'With A_Place_For_Printing2
                            '    CrDoc.SetDataSource(Ds)
                            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            '    CrDoc.PrintOptions.PrinterName = ""
                            '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print_scrap & "' "
                            '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                            '    .Text = "New Barcode Finish Good"
                            '    .CrystalReportViewer1.ReportSource = CrDoc
                            '    .Refresh()
                            '    .Show()
                            'End With

                            '============================================

                            Dim doctoprint2 As New System.Drawing.Printing.PrintDocument()
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print_scrap & "' "
                            CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC

                            doctoprint2.PrinterSettings.PrinterName = PrinterBarcodeQC

                            Dim rawKind2 As Integer
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For i = 0 To doctoprint2.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint2.PrinterSettings.PaperSizes(i).PaperName = KertasKecil Then
                                    rawKind2 = CInt(doctoprint2.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint2.PrinterSettings.PaperSizes(i)))
                                    CrDoc.PrintOptions.PaperSize = rawKind2
                                    Exit For
                                End If
                            Next

                            CrDoc.PrintToPrinter(1, False, 1, 2500)
                        Else
                            MessageBox.Show("Printer QC Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        End If

                    End If
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        End If



        EMI_Controlling_Produksi.Kosong()
        kosong()
        Me.Close()
    End Sub
End Class