Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports ZXing.QrCode

Public Class EMI_Display_Transfer_Tidak_Timbang
    Dim arrcari As New ArrayList
    Dim Jenis = "ETA"

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

    Private Sub Txt_ScanBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_ScanBarcode.KeyDown

        If e.KeyCode = Keys.Enter Then
            Btn_TimbangFloorScale_Click(Me, Nothing)
        End If
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
        get_jam()

        Dim QrLama As String = ""
        Dim expDate As String = ""
        Dim batchLama As String = ""
        Dim kode_unik_print As String
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim SN As String = ""
            Dim GetDataKodeTransfer, GetDataLokasi, GetDataKdBrg, GetDataNmBrg, GetDataBrgSN, GetDataJmlEstimasi, GetDataSatuanBesar, GetDataSatuanKecil, GetDataUrutOto As String
            Dim GetJumlahBags, GetSoAwal, GetSoTujuan, GetSnAwal, GetRakTujuan, GetPalletTujuan, GetWarna As String

            'Ambil Data Lama
            SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired "
            SQL = SQL & "from barang_sn a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & Txt_ScanBarcode.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                    batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                    SN = Dr("serial_number")
                    expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                End If
            End Using

            SQL = "select a.Kode_Transfer,a.lokasi, a.SO_Awal, a.SO_Tujuan,b.urut_oto, a.Kode_Barang, c.Nama, a.Total, a.Satuan, a.Satuan_Barang, b.Serial_Number_Awal, "
            SQL = SQL & "isnull((select distinct x.Labeling_WMS_Position from View_Warehouse_Position x,View_Warehouse_Position_Detail y where "
            SQL = SQL & "x.Kode_Perusahaan = y.kode_Perusahaan and x.Id_WMS_Warehouse_Position = y.Id_WMS_Warehouse_Position "
            SQL = SQL & "and y.Kode_Perusahaan = b.Kode_Perusahaan and y.Id_WMS_Warehouse_Position = b.Id_Wms_Awal and y.nomor_urut = b.No_Pallet_Awal), null) as Rak_Awal, "
            SQL = SQL & "b.Jumlah_Bags, b.Id_Wms_Tujuan, b.No_Pallet_Tujuan, b.Warna "
            SQL = SQL & "From tf_stock a, Tf_Stock_det b, Barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and  a.Kode_Transfer = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.SO_Awal = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and b.Flag_Pot_Stock = 'T' and a.Status is null and b.flag_sudah_cetak is null "
            SQL = SQL & "and b.Serial_Number_Awal = '" & SN & "' "
            SQL = SQL & "order by a.kode_transfer, a.tanggal,a.jam "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    GetDataKodeTransfer = Dr("Kode_Transfer")
                    GetDataLokasi = Dr("SO_Awal")
                    GetDataKdBrg = Dr("Kode_Barang")
                    GetDataNmBrg = Dr("Nama")
                    GetDataBrgSN = Dr("Serial_Number_Awal")
                    GetDataJmlEstimasi = Format(Dr("Total"), "N2")
                    GetDataSatuanKecil = Dr("Satuan_Barang")
                    GetDataSatuanBesar = Dr("Satuan")
                    GetDataUrutOto = Dr("urut_oto")

                    GetJumlahBags = Dr("Jumlah_Bags")
                    GetSoAwal = Dr("SO_Awal")
                    GetSoTujuan = Dr("SO_Tujuan")
                    GetSnAwal = Dr("Serial_Number_Awal")
                    GetRakTujuan = Dr("Id_Wms_Tujuan")
                    GetPalletTujuan = Dr("No_Pallet_Tujuan")
                    GetWarna = Dr("Warna")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    kosong()
                    Exit Sub
                End If
            End Using

            '=====================================
            '=       GENERATE BARCODE BARU       =
            '=====================================

            Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)
            Dim newQrCode As String = Generate_QR_Batch(GetDataKdBrg, batchLama)

            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")

            Dim fullNewQr As String = newQrCode & "-" & newKodeUnikBerjalan

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

            SQL = "insert into Cetak_TransferStock (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & GetDataKdBrg & "', @newBarcode, '" & GetDataNmBrg & "', '" & fullNewQr & "', '" & newQrCode & "', "
            SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "' ) "
            ExecuteTrans(SQL)

            SQL = "update Tf_Stock_det set  "
            SQL = SQL & "Flag_sudah_cetak = 'Y' "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and urut_oto = '" & GetDataUrutOto & "' "
            ExecuteTrans(SQL)

            '=============================================================================================
            '=============================================================================================

#Region "Potong Stock dan Jurnal"

            'Dim isCheck As Boolean = False
            Dim nilai_persediaan_min As Double = 0

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

            Dim flag_pot_stock As String = "NULL"
            Dim jumlah_pot_stock As String = "0"

            flag_pot_stock = "'T'"
            jumlah_pot_stock = nilai_kecildetail

            '========================
            '=     POTONG STOCK     =
            '========================

            SQL = "update barang_sn set jumlah = jumlah-'" & nilai_kecildetail & "', Jumlah_Bags = Jumlah_Bags-" & GetJumlahBags & " "
            SQL = SQL & "where Kode_Stock_Owner='" & GetSoAwal & "' and Kode_Barang='" & GetDataKdBrg & "' "
            SQL = SQL & "and Serial_Number='" & GetSnAwal & "'"
            ExecuteTrans(SQL)

            SQL = "update barang set Good_Stock= Good_Stock-" & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags-" & GetJumlahBags & " "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetSoAwal & "' "
            SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
            ExecuteTrans(SQL)

            Dim nilai_Per_Row As Double = 0
            SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
            SQL = SQL & "Kode_Stock_Owner='" & GetSoAwal & "' and Kode_Barang='" & GetDataKdBrg & "' "
            SQL = SQL & "and Serial_Number='" & GetSnAwal & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    nilai_Per_Row = dr("rp_persediaan_min")
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            nilai_persediaan_min += nilai_Per_Row

            Dim hargaIsn As String = ""

            'Ambil Data Lama
            SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired "
            SQL = SQL & "from barang_sn a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Stock_Owner='" & GetSoAwal & "' "
            SQL = SQL & "and a.Kode_Barang ='" & GetDataKdBrg & "' "
            SQL = SQL & "and a.Serial_Number='" & GetSnAwal & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Tidak ada")
                    Exit Sub
                End If
            End Using

            'GENERATE SN BARU
            Dim str As String = Format(Random.Next(0, 999), "000") & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HHmmss")
            Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
            Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(DateTime.Now, "yyyy-MM-dd")

            SN = "'" & SN_Baru & "'"

            'INSERT BARANG SN BARU
            SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, "
            SQL = SQL & "Jumlah, Jumlah_Bags, Tgl_Expired, Tgl_Produksi, "
            SQL = SQL & "Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, "
            SQL = SQL & "Kode_Unik_Asal, Nomor_Pallet, batch_number, warna) "
            SQL = SQL & "select Kode_Perusahaan, '" & GetSoTujuan & "', Kode_Barang, '" & SN_Baru & "', "
            'SQL = SQL & "'" & nilai_kecildetail & "', Warning_Stock, Bad_Stock, " & dgv_JmlhBags & ", "
            SQL = SQL & "'" & nilai_kecildetail & "', " & GetJumlahBags & ", "
            SQL = SQL & "Tgl_Expired, Tgl_Produksi, '" & GetRakTujuan & "', "
            SQL = SQL & "id_Susunan, Qr_Code, '" & newKodeUnikBerjalan & "', Kode_Unik_Asal, '" & GetPalletTujuan & "', batch_number, '" & GetWarna & "' "
            SQL = SQL & "from Barang_SN "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
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

            'Cek STOCK BARANG
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
                            MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner =  "
            SQL = SQL & "'" & GetSoTujuan & "' "
            SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
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
                End With
            End Using

            'dari
            'JURNAL
            Dim fRaw_Material_dari As String = ""
            Dim fFinished_Good_dari As String = ""
            Dim fSemi_FG_dari As String = ""
            Dim fScrap_dari As String = ""
            Dim fPackaging_dari As String = ""
            Dim akun_persediaan_dari As String = ""

            Dim fRaw_Material_tujuan As String = ""
            Dim fFinished_Good_tujuan As String = ""
            Dim fSemi_FG_tujuan As String = ""
            Dim fScrap_tujuan As String = ""
            Dim akun_persediaan_tujuan As String = ""
            Dim fPackaging_tujuan As String = ""
            Dim inisial_faktur_dari As String = ""

            SQL = "select a.Flag_Raw_Material,a.Flag_Finished_Good,a.Flag_Semi_FG,a.Flag_Scrap, a.Flag_Packaging "
            SQL = SQL & "from Barang b,EMI_Group_Jenis a where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.kode_stock_owner = '" & GetSoAwal & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    fRaw_Material_dari = Dr("Flag_Raw_Material")
                    fFinished_Good_dari = Dr("Flag_Finished_Good")
                    fSemi_FG_dari = Dr("Flag_Semi_FG")
                    fScrap_dari = Dr("Flag_Scrap")
                    fPackaging_dari = Dr("Flag_Packaging")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & GetSoAwal & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'akun_persediaan_dari = Dr("persediaan")
                    inisial_faktur_dari = Dr("inisial_faktur")
                    If fRaw_Material_dari = "Y" Then
                        akun_persediaan_dari = Dr("Persediaan_Bahan_Baku")
                    ElseIf fFinished_Good_dari = "Y" Then
                        akun_persediaan_dari = Dr("Persediaan")
                    ElseIf fSemi_FG_dari = "Y" Then
                        akun_persediaan_dari = Dr("Persediaan_Bahan_Setengah_Jadi")
                    ElseIf fScrap_dari = "Y" Then
                        akun_persediaan_dari = Dr("Persediaan_Scrap")
                    ElseIf fPackaging_dari = "Y" Then
                        akun_persediaan_dari = Dr("Persediaan_Packaging")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.Flag_Raw_Material,a.Flag_Finished_Good,a.Flag_Semi_FG,a.Flag_Scrap, a.Flag_Packaging "
            SQL = SQL & "from Barang b,EMI_Group_Jenis a where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.kode_stock_owner = '" & GetSoTujuan & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    fRaw_Material_tujuan = Dr("Flag_Raw_Material")
                    fFinished_Good_tujuan = Dr("Flag_Finished_Good")
                    fSemi_FG_tujuan = Dr("Flag_Semi_FG")
                    fScrap_tujuan = Dr("Flag_Scrap")
                    fPackaging_tujuan = Dr("Flag_Packaging")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & GetSoTujuan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'akun_persediaan_dari = Dr("persediaan")
                    If fRaw_Material_tujuan = "Y" Then
                        akun_persediaan_tujuan = Dr("Persediaan_Bahan_Baku")
                    ElseIf fFinished_Good_tujuan = "Y" Then
                        akun_persediaan_tujuan = Dr("Persediaan")
                    ElseIf fSemi_FG_tujuan = "Y" Then
                        akun_persediaan_tujuan = Dr("Persediaan_Bahan_Setengah_Jadi")
                    ElseIf fScrap_tujuan = "Y" Then
                        akun_persediaan_tujuan = Dr("Persediaan_Scrap")
                    ElseIf fPackaging_tujuan = "Y" Then
                        akun_persediaan_tujuan = Dr("Persediaan_Packaging")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
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
                      KodePerusahaan, KodeProyek, "Persedian " & GetDataKodeTransfer, "0", nilai_persediaan_min, pagenumber, "TSSS")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                     Strings.Mid(akun_persediaan_tujuan, 2, 1),
                     Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                     KodePerusahaan, KodeProyek, "Persedian " & GetDataKodeTransfer, nilai_persediaan_min, "0", pagenumber, "TSSS")
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



            SQL = "update Tf_Stock_det set kode_voucher = '" & Kode_voucher & "', "
            SQL = SQL & "Serial_Number_Akhir = '" & SN_Baru & "' "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & GetDataKodeTransfer & "' "
            SQL = SQL & "and urut_oto = '" & GetDataUrutOto & "'"
            ExecuteTrans(SQL)

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

        '=================
        '=     CETAK     =
        '=================
        Try
            OpenConn()

            SQL = "select Kode_Perusahaan from Cetak_TransferStock where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & kode_unik_print & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Object
                    CrDoc = New NewBarcodeTransferStock
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unik_print & "' and {Cetak_TransferStock.batch} = '" & batchLama & "' "
                        CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Stock"
                        .Text = "New Barcode Transfer Stock"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With

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
            Lv_List_Barang.Columns.Add("SO Awal", 130, HorizontalAlignment.Left) '1
            Lv_List_Barang.Columns.Add("SO Akhir", 130, HorizontalAlignment.Left) '2
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_KodeBarang, 120, HorizontalAlignment.Left) '3
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_NamaBarang, 180, HorizontalAlignment.Left) '4
            Lv_List_Barang.Columns.Add("Total", 130, HorizontalAlignment.Center) '5
            Lv_List_Barang.Columns.Add(Base_Language.Lang_Global_Satuan, 120, HorizontalAlignment.Center) '6
            Lv_List_Barang.Columns.Add("Lokasi RAK", 150, HorizontalAlignment.Left) '7
            Lv_List_Barang.Columns.Add("barangSn", 0, HorizontalAlignment.Left) '8

            Lv_List_Barang.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
        '''Txt_ScanBarcode.Text = "1825003-0118L9B301124-T1X7VBQWEH"
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

            SQL = "select a.Kode_Transfer,a.lokasi, a.SO_Awal, a.SO_Tujuan, a.Kode_Barang, c.Nama, a.Total, a.Satuan, a.Satuan_Barang, b.Serial_Number_Awal, "
            SQL = SQL & "isnull((select distinct x.Labeling_WMS_Position from View_Warehouse_Position x,View_Warehouse_Position_Detail y where "
            SQL = SQL & "x.Kode_Perusahaan = y.kode_Perusahaan and x.Id_WMS_Warehouse_Position = y.Id_WMS_Warehouse_Position "
            SQL = SQL & "and y.Kode_Perusahaan = b.Kode_Perusahaan and y.Id_WMS_Warehouse_Position = b.Id_Wms_Awal and y.nomor_urut = b.No_Pallet_Awal), null) as Rak_Awal "
            SQL = SQL & "From tf_stock a, Tf_Stock_det b, Barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and  a.Kode_Transfer = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.SO_Awal = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and b.Flag_Pot_Stock ='T' and a.Status is null  and flag_sudah_cetak is null "
            SQL = SQL & "order by a.kode_transfer, a.tanggal,a.jam "

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem

                    Lvw = Lv_List_Barang.Items.Add(dr("Kode_Transfer"))
                    '  Lvw.SubItems.Add(dr("lokasi"))
                    Lvw.SubItems.Add(dr("SO_Awal"))
                    Lvw.SubItems.Add(dr("so_tujuan"))
                    Lvw.SubItems.Add(dr("kode_barang"))
                    Lvw.SubItems.Add(dr("nama"))
                    Lvw.SubItems.Add(Format(dr("total"), "N2"))
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

End Class