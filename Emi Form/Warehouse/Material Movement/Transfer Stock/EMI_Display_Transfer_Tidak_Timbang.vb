Imports System.IO

Public Class EMI_Display_Transfer_Tidak_Timbang
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

    Dim Random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

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
        get_jam()

        Dim QrLama As String = ""
        Dim expDate As String = ""
        Dim batchLama As String = ""
        Dim tglMsk As String = ""
        Dim metodePengeluaranStock As String = ""

        Dim kode_unik_print As String
        Dim GetDataKodeTransfer, GetDataLokasi, GetDataKdBrg, GetDataNmBrg, GetDataBrgSN, GetDataJmlEstimasi, GetDataSatuanBesar, GetDataSatuanKecil, GetDataUrutOto As String
        Dim GetJumlahBags, GetSoAwal, GetSoTujuan, GetSnAwal, GetRakTujuan, GetPalletTujuan, GetWarna As String
        Dim SN As String = ""

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction



            'Ambil Data SN Berdasar Barcode
            SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired,b.Metode_Pengeluaran_Stok,a.Tgl_Masuk, a.Blok_SN "
            SQL = SQL & "from barang_sn a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Jumlah <> 0 "
            SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & Txt_ScanBarcode.Text & "' "
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

                    QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                    batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                    SN = Dr("serial_number")
                    expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                    tglMsk = General_Class.CekNULL(Dr("tgl_masuk"))
                    metodePengeluaranStock = General_Class.CekNULL(Dr("Metode_Pengeluaran_Stok"))
                End If
            End Using


            'Cek data YG Mau di TF, Berdasar SN dr Barcode
            SQL = "Select a.no_faktur, a.lokasi, a.so_awal, a.so_tujuan, c.urut_Oto, b.kode_Barang, "
            SQL = SQL & "d.nama, b.Total, b.satuan, b.Satuan_Barang, c.serial_number_awal, "
            SQL = SQL & "c.jumlah, c.Jumlah_Bags, c.Id_Wms_Tujuan, c.Warna, "

            SQL = SQL & "isnull((select x.Labeling_WMS_Position from View_Warehouse_Position x where "
            SQL = SQL & "x.Kode_Perusahaan = c.Kode_Perusahaan And x.Id_WMS_Warehouse_Position = c.Id_Wms_Awal), null) As Rak_Awal "

            SQL = SQL & "From tf_stock_parent a, tf_stock b, tf_stock_det c, barang d Where "
            SQL = SQL & "a.kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And "
            SQL = SQL & "b.kode_Perusahaan = c.kode_Perusahaan And b.no_faktur = c.no_faktur And b.Urut_Oto = c.urut_TF "
            SQL = SQL & "And b.Kode_Barang=d.Kode_Barang And a.so_awal=d.kode_stock_Owner And b.kode_Perusahaan=d.Kode_Perusahaan "
            SQL = SQL & "And a.status Is null And b.Flag_Timbang ='T' and c.selesai is null "
            SQL = SQL & "And c.Serial_Number_Awal = '" & SN & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    GetDataKodeTransfer = Dr("No_faktur")
                    GetDataLokasi = Dr("SO_Awal")
                    GetDataKdBrg = Dr("Kode_Barang")
                    GetDataNmBrg = Dr("Nama")
                    GetDataBrgSN = Dr("Serial_Number_Awal")
                    GetDataJmlEstimasi = HilangkanTanda(Format(Dr("jumlah"), "N2"))
                    GetDataSatuanKecil = Dr("Satuan_Barang")
                    GetDataSatuanBesar = Dr("Satuan")
                    GetDataUrutOto = Dr("urut_oto")

                    GetJumlahBags = Dr("Jumlah_Bags")
                    GetSoAwal = Dr("SO_Awal")
                    GetSoTujuan = Dr("SO_Tujuan")
                    GetSnAwal = Dr("Serial_Number_Awal")
                    GetRakTujuan = Dr("Id_Wms_Tujuan")
                    'GetPalletTujuan = Dr("No_Pallet_Tujuan")
                    GetWarna = Dr("Warna")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    kosong()
                    Exit Sub
                End If
            End Using


            SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
            SQL = SQL & "from tf_stock_parent a, tf_stock b, Tf_Stock_det c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
            SQL = SQL & "and a.No_Faktur = '" & GetDataKodeTransfer & "' and c.urut_oto = '" & GetDataUrutOto & "'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("status")) <> "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
            SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
            SQL = SQL & "id_wms_warehouse_position = '" & GetRakTujuan & "' "
            SQL = SQL & "order by nomor_urut "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    GetPalletTujuan = dr("nomor_urut")
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                    Exit Sub
                End If
            End Using

            '=============================================================================================
            '=============================================================================================
            '=======================================================================================


            '====================================
            '=       CONVERT SATUAN KECIL       =
            '====================================
            Dim nilai_kecildetail As Double = 0
            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & GetDataKdBrg & "', '" & GetDataSatuanBesar & "',"
            SQL = SQL & "'" & GetDataSatuanKecil & "', '" & GetDataJmlEstimasi & "' ) as hasil"
            Using Dr1 = OpenTrans(SQL)
                If Dr1.Read Then
                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If

                    nilai_kecildetail = Dr1("hasil")
                Else
                    Dr1.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data konversi satuan kirim tidak ada ")
                    Exit Sub
                End If
            End Using

            '============================
            '=       POTONG STOCK       =
            '============================

            Dim nilai_persediaan_min As Double = 0
            SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
            SQL = SQL & "Kode_Stock_Owner='" & GetSoAwal & "' and Kode_Barang='" & GetDataKdBrg & "' "
            SQL = SQL & "and Serial_Number='" & GetSnAwal & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    nilai_persediaan_min = dr("rp_persediaan_min")
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim Nama As String = ""
            'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
            SQL = "select Nama, Kode_Barang, round(good_stock,2) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetSoAwal & "' "
            SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Nama = dr("Kode_Barang")
                    If dr("good_stock") < nilai_kecildetail Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    ElseIf dr("Jumlah_Bags") < GetJumlahBags Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    Else
                        dr.Close()
                        SQL = "update barang set Good_Stock = Good_Stock - " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags - " & GetJumlahBags & " "
                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetSoAwal & "' "
                        SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
                        ExecuteTrans(SQL)
                    End If
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select round(jumlah,2) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetSoAwal & "' "
            SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
            SQL = SQL & "and Serial_Number='" & GetSnAwal & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If dr("jumlah") < nilai_kecildetail Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    ElseIf dr("Jumlah_Bags") < GetJumlahBags Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    Else
                        dr.Close()
                        SQL = "update barang_sn set jumlah = jumlah - " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags - " & GetJumlahBags & " "
                        SQL = SQL & "where Kode_Stock_Owner='" & GetSoAwal & "' and Kode_Barang='" & GetDataKdBrg & "' "
                        SQL = SQL & "and Serial_Number='" & GetSnAwal & "'"
                        ExecuteTrans(SQL)
                    End If
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '====================================
            '=       CEK KESESUAIAN STOCK       =
            '====================================
            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetSoAwal & "' "
            SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using


            '==============================
            '=       INSERT SN BARU       =
            '==============================

            Dim hargaIsn As String = ""
            Dim namaBarang As String = ""
            Dim warnaLama As String = ""

            'Ambil Data Lama
            SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
            SQL = SQL & "from barang_sn a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Stock_Owner='" & GetSoAwal & "' "
            SQL = SQL & "and a.Kode_Barang ='" & GetDataKdBrg & "' "
            SQL = SQL & "and a.Serial_Number='" & GetSnAwal & "' "
            'SQL = SQL & "and a.Jumlah <> 0 "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                    QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                    batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                    namaBarang = General_Class.CekNULL(Dr("Nama"))
                    expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                    warnaLama = General_Class.CekNULL(Dr("warna"))
                Loop
            End Using

            'GENERATE SN BARU
            Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
            Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
            Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

            Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

            'INSERT BARANG SN BARU  
            SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
            SQL = SQL & "select Kode_Perusahaan, '" & GetSoTujuan & "', Kode_Barang, '" & SN_Baru & "', '" & nilai_kecildetail & "', " & GetJumlahBags & ", "
            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & GetRakTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
            SQL = SQL & "Kode_Unik_Asal, '" & GetPalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, NULL "
            SQL = SQL & "from Barang_SN "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Stock_Owner='" & GetSoAwal & "' "
            SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
            SQL = SQL & "and Serial_Number='" & GetSnAwal & "' "
            ExecuteTrans(SQL)

            '============================
            '=       TAMBAH STOCK       =
            '============================

            SQL = "update barang set Good_Stock= Good_Stock + " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags + " & GetJumlahBags & " "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetSoTujuan & "' "
            SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
            ExecuteTrans(SQL)

            'CEK KESESUAIAN STOCK
            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetSoTujuan & "' "
            SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using



            'dari
            Dim inisial_faktur_dari As String = ""
            Dim akun_persediaan_dari As String = ""
            Dim akun_persediaan_tujuan As String = ""

            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & GetSoAwal & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'akun_persediaan_dari = Dr("persediaan")
                    inisial_faktur_dari = Dr("inisial_faktur")

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select c.akun_Persediaan "
            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.kode_stock_owner = '" & GetSoAwal & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_persediaan_dari = Dr("akun_Persediaan")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select c.akun_Persediaan "
            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.kode_stock_owner = '" & GetSoTujuan & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_persediaan_tujuan = Dr("akun_Persediaan")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim Kode_voucher As String = ""
            Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
            Dim pagenumber As Integer = 1

            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
            SQL = SQL & "'" & Kode_voucher & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
            SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & GetDataKodeTransfer & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                      Strings.Mid(akun_persediaan_dari, 2, 1),
                      Strings.Mid(Ganti(akun_persediaan_dari), 3),
                      KodePerusahaan, KodeProyek, "Persedian " & GetDataKodeTransfer, "0", nilai_persediaan_min, pagenumber, GetSoAwal, Bahasa_Pilihan, Ket_Cost_Center_HO)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                     Strings.Mid(akun_persediaan_tujuan, 2, 1),
                     Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                     KodePerusahaan, KodeProyek, "Persedian " & GetDataKodeTransfer, nilai_persediaan_min, "0", pagenumber, GetSoTujuan, Bahasa_Pilihan, Ket_Cost_Center_HO)
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

            SQL = "insert into Tf_Stock_det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
            SQL = SQL & "Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags) values( "
            SQL = SQL & "'" & KodePerusahaan & "', '" & GetDataKodeTransfer & "', '" & GetDataUrutOto & "', "
            SQL = SQL & "'" & GetPalletTujuan & "', '" & SN_Baru & "', '" & nilai_kecildetail & "', "
            SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
            SQL = SQL & "'" & Kode_voucher & "', '" & GetJumlahBags & "') "
            ExecuteTrans(SQL)

            SQL = "update Tf_Stock_det set  "
            SQL = SQL & "Selesai = 'Y' "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and urut_oto = '" & GetDataUrutOto & "' "
            ExecuteTrans(SQL)

            '=====================================
            '=       GENERATE BARCODE BARU       =
            '=====================================
            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
            Dim fullNewQr As String = QrLama & "-" & newKodeUnikBerjalan

            Barcode.Image = Generate_QR(fullNewQr)

            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")
            'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
            Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
            'End If

            fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
            FileSize1 = fs1.Length
            rawData1 = New Byte(FileSize1) {}
            fs1.Read(rawData1, 0, FileSize1)
            fs1.Close()
            Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1


            '===================================
            '=       INSERT BARCODE BARU       =
            '===================================
            Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

            SQL = "delete from Cetak_TransferStock where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            ExecuteTrans(SQL)

            SQL = "insert into Cetak_TransferStock (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print,tanggal_masuk,metode_pengeluaran_stok) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & GetDataKdBrg & "', @newBarcode, '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
            SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "' , "
            SQL = SQL & "'" & tglMsk & "', '" & metodePengeluaranStock & "' ) "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '=================
        '=     CETAK     =
        '=================
        Try
            OpenConn()

            '=================================
            '=     CETAK FAKTUR TF STOCK     =
            '=================================
            Dim CrDoc As New Object
            Dim kertas As String = ""

            Dim Selesai As String = ""
            SQL = "Select a.Kode_Perusahaan from Tf_Stock_Parent a, Tf_Stock_det b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And "
            SQL = SQL & "a.No_Faktur = b.No_Faktur And a.Status Is null And "
            SQL = SQL & "b.selesai Is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & GetDataKodeTransfer & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Selesai = "T"
                Else
                    Selesai = "Y"
                End If
            End Using

            If Selesai = "Y" Then
                SQL = "select a.Kode_Perusahaan "
                SQL = SQL & "from Vw_tf_stock_detail a "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & GetDataKodeTransfer & "' "
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New Rpt_EMI_Faktur_Transfer_Stock_Detail
                        kertas = "Faktur"

                        '''With A_Place_For_Printing2
                        '''    CrDoc.SetDataSource(Ds)
                        '''    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '''    CrDoc.PrintOptions.PrinterName = ""
                        '''    CrDoc.RecordSelectionFormula = "{Vw_tf_stock_detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_tf_stock_detail.No_Faktur}='" & GetDataKodeTransfer & "' "
                        '''    CrDoc.SummaryInfo.ReportTitle = "TF"
                        '''    .Text = "TF"
                        '''    .CrystalReportViewer1.ReportSource = CrDoc
                        '''    .Refresh()
                        '''    .Show()
                        '''End With

                        '============================================================================================================================================
                        '============================================================================================================================================
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = PrinterNameTS
                        CrDoc.RecordSelectionFormula = "{Vw_tf_stock_detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_tf_stock_detail.No_Faktur}='" & GetDataKodeTransfer & "' "
                        'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterNameTS
                        doctoprint.DefaultPageSettings.Landscape = True
                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        CrDoc.PrintToPrinter(1, False, 1, 99)

                        MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


                    End If
                End Using
            End If


            '=========================
            '=     CETAK BARCODE     =
            '=========================
            SQL = "select Kode_Perusahaan from Cetak_TransferStock where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & kode_unik_print & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New NewBarcodeTransferStock
                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unik_print & "' and {Cetak_TransferStock.batch} = '" & batchLama & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Stock"
                    '    .Text = "New Barcode Transfer Stock"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unik_print & "' and {Cetak_TransferStock.batch} = '" & batchLama & "' "

                    CrDoc.PrintOptions.PrinterName = PrinterBarcode

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    CrDoc.PrintToPrinter(1, False, 1, 2500)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
        '---------------------------------------------------------------

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
            Label1.Text = "Display - List Transfer Stock (Tidak Timbang)"

            'If filter_tambahan = " timbang_masuk='Y'" Then
            '    Label1.Text = "Display - Kendaraan Masuk"
            'Else
            '    Label1.Text = "Display - Kendaraan Keluar"
            'End If

            Lv_List_Barang.Columns.Clear()

            Lv_List_Barang.Columns.Add("Kode Transfer", 120, HorizontalAlignment.Left).DisplayIndex = 0 '0
            '  Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_Lokasi, 130, HorizontalAlignment.Left) '1
            Lv_List_Barang.Columns.Add("SO Awal", 150, HorizontalAlignment.Left) '1
            Lv_List_Barang.Columns.Add("SO Akhir", 150, HorizontalAlignment.Left) '2
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left) '3
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_NamaBarang, 0, HorizontalAlignment.Left) '4
            Lv_List_Barang.Columns.Add("Total", 150, HorizontalAlignment.Center) '5
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_Satuan, 120, HorizontalAlignment.Center) '6
            Lv_List_Barang.Columns.Add("Lokasi RAK", 150, HorizontalAlignment.Left) '7
            Lv_List_Barang.Columns.Add("barangSn", 0, HorizontalAlignment.Left) '8

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

    Private Sub EMI_Display_Transfer_Tidak_Timbang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

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
        '    ' If Char.IsLetterOrDigit(e.KeyChar) OrElse Char.IsSymbol(e.KeyChar) OrElse e.KeyChar = "-"c Then
        '    ValueBarcode &= e.KeyChar.ToString.Trim
        '    'End If

        'End If
    End Sub

    'Private Sub Txt_ScanBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_ScanBarcode.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        Btn_TimbangFloorScale_Click(Me, Nothing)
    '    End If
    'End Sub

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
            SQL = SQL & "x.Kode_Perusahaan = c.Kode_Perusahaan And x.Id_WMS_Warehouse_Position = c.Id_Wms_Awal), null) As Rak_Awal "

            SQL = SQL & "From tf_stock_parent a, tf_stock b, tf_stock_det c, barang d Where "
            SQL = SQL & "a.kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And "
            SQL = SQL & "b.kode_Perusahaan = c.kode_Perusahaan And b.no_faktur = c.no_faktur And b.Urut_Oto = c.urut_TF "
            SQL = SQL & "And b.Kode_Barang=d.Kode_Barang And a.so_awal=d.kode_stock_Owner And b.kode_Perusahaan=d.Kode_Perusahaan "
            SQL = SQL & "And a.status Is null And b.Flag_Timbang ='T' and c.selesai is null "
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
                    Lvw.SubItems.Add(Format(dr("jumlah"), "N2"))
                    Lvw.SubItems.Add(dr("satuan"))
                    Lvw.SubItems.Add(dr("Rak_Awal"))
                    Lvw.SubItems.Add(dr("Serial_Number_Awal"))
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