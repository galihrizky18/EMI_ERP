Imports System.IO

Public Class Emi_Display_Tf_Stock_QC

    Dim Random As New Random()

    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Private Sub Emi_Display_Tf_Stock_QC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Initial_Listview()
        Kosong()
    End Sub

    Private Sub Kosong()

        Txt_ScanBarcode.Text = ""

        Load_Data()

    End Sub

    Private Sub Initial_Listview()

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Faktur", 180, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("Lokasi Awal", 200, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Lokasi Tujuan", 200, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Kode Barang", 200, HorizontalAlignment.Left) '3
        Lv_Data.Columns.Add("Nama", 300, HorizontalAlignment.Left) '4
        Lv_Data.Columns.Add("Total Input", 150, HorizontalAlignment.Right) '5
        Lv_Data.Columns.Add("Satuan Input", 100, HorizontalAlignment.Center) '6
        Lv_Data.Columns.Add("Total", 150, HorizontalAlignment.Right) '7
        Lv_Data.Columns.Add("Satuan", 90, HorizontalAlignment.Center) '8
        Lv_Data.View = View.Details

    End Sub

    Private Sub Load_Data()

        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.No_Faktur, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang, d.Total, d.Satuan_Barang, "
            SQL = SQL & " d.Total  as Total_Besar, b.Satuan, e.Keterangan as Rak_Awal, d.Serial_Number_Awal, "

            SQL = SQL & "isnull(( select isnull((d.Total / z.Nilai), 0) as Hasil from N_EMI_Master_Satuan z "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and z.Satuan = d.Satuan_Input "
            SQL = SQL & "), 0) as Jumlah_Input, isnull(d.Satuan_Input, '-') as Satuan_Input "

            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, barang c, Tf_Stock_QC_det d, View_Warehouse_Position e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.SO_Awal = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and b.No_Faktur = d.No_Faktur and b.Urut_Oto = d.Urut_TF "
            SQL = SQL & "and d.Id_Wms_Awal = e.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and d.Selesai is null "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "order by a.No_Faktur, a.tanggal,a.jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("SO_Awal"))
                    Lv.SubItems.Add(Dr("SO_Tujuan"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Input"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan_Input"))
                    Lv.SubItems.Add(Format(Dr("Total_Besar"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Txt_ScanBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ScanBarcode.KeyPress
        If e.KeyChar = Chr(13) Then

            If Txt_ScanBarcode.Text.Trim.Length <> 0 Then
                Btn_Scan_Click(Me, Nothing)
            End If

        End If
    End Sub

    Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        If Txt_ScanBarcode.Text.Trim.Length = 0 Then
            MessageBox.Show("Scan terlebih dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_ScanBarcode.Focus()
            Exit Sub
            Exit Sub
        End If

        get_jam()

        Dim QrLama As String = ""
        Dim expDate As String = ""
        Dim batchLama As String = ""
        Dim SN_Awal As String = ""

        Dim NoFaktur As String = ""
        Dim SoAwal As String = ""
        Dim SoTujuan As String = ""
        Dim id_WmsAwal As String = ""
        Dim id_WmsTujuan As String = ""
        Dim noPallet_Awal As String = ""
        Dim noPallet_Tujuan As String = ""
        Dim kodeBarang As String = ""
        Dim DataUrutQCDet As String = ""
        Dim DataUrutQCDetail As String = ""
        Dim beratBagi As Double = 0
        Dim jumlahBags As Double = 0
        Dim count As Double = 0
        Dim totalPotong As Double = 0

        Dim kode_unik_print As String = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")

        Dim Kd_Soo As String = ""
        Dim Kd_Barangg As String = ""
        Dim Urut_Det_Convert As String = ""

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '===========================
            '=     GET DATA BARANG     =
            '===========================
            SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.Warna, a.Blok_SN "
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
                        Kosong()
                        Exit Sub
                    End If

                    'Cek Warna Barang
                    If General_Class.CekNULL(Dr("warna")) = "" Or General_Class.CekNULL(Dr("warna")) <> "HIJAU" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Ada Masalah dengan Kualitas Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Kosong()
                        Exit Sub
                    End If

                    QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                    batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                    SN_Awal = Dr("serial_number")
                    expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Kosong()
                    Exit Sub

                End If
            End Using

            '==============================
            '=      GET DATA TRANSFER     =
            '==============================
            SQL = "select a.No_Faktur, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang, b.Total, b.Satuan, b.Total_Barang, b.Satuan_Barang, "
            SQL = SQL & "c.Id_Wms_Awal, c.No_Pallet_Awal, c.Id_Wms_Tujuan, c.No_Pallet_Tujuan, c.Serial_Number_Awal, c.Berat_Bagi, c.Jumlah_Bags, c.Count, c.Total, c.Satuan_Barang as Satuan_PEcah, b.Urut_Oto, c.Urut_Oto as Urut_QC_Det,  "
            SQL = SQL & "b.Urut_Material_Requisition_Convert "
            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.Selesai is null   "
            SQL = SQL & "and c.Serial_Number_Awal = '" & SN_Awal & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    NoFaktur = Dr("No_Faktur")
                    SoAwal = Dr("SO_Awal")
                    SoTujuan = Dr("SO_Tujuan")
                    id_WmsAwal = Dr("Id_Wms_Awal")
                    id_WmsTujuan = Dr("Id_Wms_Tujuan")
                    noPallet_Awal = Dr("No_Pallet_Awal")
                    noPallet_Tujuan = Dr("No_Pallet_Tujuan")
                    kodeBarang = Dr("Kode_Barang")
                    beratBagi = Dr("Berat_Bagi") 'Satuan Gram tidak perlu di N4
                    jumlahBags = Dr("Jumlah_Bags")
                    count = Dr("Count")
                    totalPotong = Val(HilangkanTanda(Dr("Berat_Bagi"))) * Val(HilangkanTanda(Dr("Count")))
                    DataUrutQCDet = Dr("Urut_QC_Det")
                    DataUrutQCDetail = Dr("Urut_Oto")

                    Kd_Soo = Dr("SO_Awal")
                    Kd_Barangg = Dr("Kode_Barang")
                    Urut_Det_Convert = Dr("Urut_Material_Requisition_Convert")

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Kosong()
                    Exit Sub

                End If
            End Using

            '================================
            '=       NILAI PERSESIDAAN      =
            '================================
            Dim nilai_persediaan_min As Double = 0
            SQL = "select round(dbo.get_hpp(serial_number) * " & totalPotong & ", 4) as rp_persediaan_min from barang_sn where "
            SQL = SQL & "Kode_Stock_Owner='" & SoAwal & "' and Kode_Barang='" & kodeBarang & "' "
            SQL = SQL & "and Serial_Number='" & SN_Awal & "'"
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

            '=========================
            '=      POTONG STOCK     =
            '=========================

#Region "POOTONG STOCK"

            '================================
            '=      POTONG STOCK BARANG     =
            '================================
            Dim Nama As String = ""
            SQL = "select Nama, kode_barang, round(good_stock,4) as good_stock,Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & SoAwal & "' "
            SQL = SQL & "and Kode_Barang='" & kodeBarang & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Nama = dr("kode_barang")
                    If dr("good_stock") < totalPotong Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    ElseIf dr("Jumlah_Bags") < jumlahBags Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    Else
                        dr.Close()
                        SQL = "update barang set Good_Stock = Good_Stock - Round(" & totalPotong & ",4), Jumlah_Bags = Jumlah_Bags - " & jumlahBags & " "
                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & SoAwal & "' "
                        SQL = SQL & " and Kode_Barang='" & kodeBarang & "'"
                        'ExecuteTrans(SQL)
                    End If
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===================================
            '=      POTONG STOCK BARANG SN     =
            '===================================
            SQL = "select round(jumlah,4) as jumlah,Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & SoAwal & "' "
            SQL = SQL & "and Kode_Barang='" & kodeBarang & "' "
            SQL = SQL & "and Serial_Number='" & SN_Awal & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If dr("jumlah") < totalPotong Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    ElseIf dr("Jumlah_Bags") < jumlahBags Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    Else
                        dr.Close()
                        SQL = "update barang_sn set jumlah = jumlah - Round(" & totalPotong & ",4), Jumlah_Bags = Jumlah_Bags - " & jumlahBags & " "
                        SQL = SQL & "where Kode_Stock_Owner='" & SoAwal & "' and Kode_Barang='" & kodeBarang & "' "
                        SQL = SQL & "and Serial_Number='" & SN_Awal & "'"
                        'ExecuteTrans(SQL)
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
            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & SoAwal & "' "
            SQL = SQL & "AND a.Kode_Barang = '" & kodeBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
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

#End Region

            '=========================
            '=      TAMBAH STOCK     =
            '=========================

#Region "TAMBAH STOCK"

            Dim hargaIsn As String = ""
            Dim namaBarang As String = ""
            Dim warnaLama As String = ""

            Dim totalTambahStock As Double = 0
            Dim totalTambahBags As Double = 0

            Dim urutBarcode As Integer = 1

            For i As Integer = 0 To count - 1

                'SQL = "Select Top(1) nomor_urut from dbo.N_EMI_Wharehouse_Position_Fn('" & KodePerusahaan & "','" & SoTujuan & "', " & id_WmsTujuan & ") where "
                'SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null "
                'SQL = SQL & "order by nomor_urut "
                'Using dr = OpenTrans(SQL)
                '    If dr.Read Then
                '        noPallet_Tujuan = dr("nomor_urut")
                '    Else
                '        dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                '        Exit Sub
                '    End If
                'End Using

                '=================================
                '=      AMBIL DATA PENDUKUNG     =
                '=================================
                SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
                SQL = SQL & "from barang_sn a, barang b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner='" & SoAwal & "' "
                SQL = SQL & "and a.Kode_Barang ='" & kodeBarang & "' "
                SQL = SQL & "and a.Serial_Number='" & SN_Awal & "' "
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

                Dim JumlahBagsS As Double = 0
                SQL = "select top 1 jenis_kemasan, isnull(Isi_Per_Bags, 0) as Isi_Per_Bags, Satuan from barang "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Barang = '" & kodeBarang & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dim JenisKemasanS As String = Dr("jenis_kemasan")
                        Dim IsiPerBagsS As String = Dr("Isi_Per_Bags")
                        Dim SatuanS As String = Dr("Satuan")

                        Dr.Close()

                        If JenisKemasanS.ToString.ToUpper = "ORIGINAL BAGS" Then

                            JumlahBagsS = Math.Ceiling(beratBagi / IsiPerBagsS)

                        End If

                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                '=========================================================
                '=      GENERATE SN BARU DAN KODE UNIK BERJALAN BARU     =
                '=========================================================
                Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)


                Dim tgl_skg2 As DateTime
                SQL = "declare @ab int; declare @ac int; select @ab = Selisih_Jam, @ac= expired_proforma from Init; "
                SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss')  as Tanggal_Sekarang , @ac as expired"
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        tgl_skg2 = dr("Tanggal_Sekarang")

                    Loop
                End Using

                Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg2, "HHmmss")
                Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                '=============================
                '=      INSERT BARANB SN     =
                '=============================
                SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Blok_SN) "
                SQL = SQL & "select Kode_Perusahaan, '" & SoTujuan & "', Kode_Barang, '" & SN_Baru & "', '" & beratBagi & "', " & JumlahBagsS & ", " '& jumlahBags & ", "
                SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & id_WmsTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                SQL = SQL & "Kode_Unik_Asal, '" & noPallet_Tujuan & "', batch_number, '" & warnaLama & "', NULL "
                SQL = SQL & "from Barang_SN "
                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Stock_Owner='" & SoAwal & "' "
                SQL = SQL & "and Kode_Barang='" & kodeBarang & "' "
                SQL = SQL & "and Serial_Number='" & SN_Awal & "' "
                'ExecuteTrans(SQL)

                InsertNewBarcode(QrLama, newKodeUnikBerjalan, kodeBarang, namaBarang, expDate, batchLama, kode_unik_print, urutBarcode)

                totalTambahStock += beratBagi
                totalTambahBags += JumlahBagsS
                urutBarcode += 1

                '====================================
                '=      INSERT Tf_Stock_QC_det2     =
                '====================================
                SQL = "insert into Tf_Stock_QC_det2 (Kode_Perusahaan, No_Faktur, Urut_Det, No_Pallet, Serial_Number, Jumlah, UserID, Tanggal, Jam) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & NoFaktur & "', '" & DataUrutQCDet & "', '" & noPallet_Tujuan & "', '" & SN_Baru & "', "
                SQL = SQL & "'" & beratBagi & "', '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & tgl_skg.ToString("HH:mm:ss") & "')"
                'ExecuteTrans(SQL)

            Next

            '===================================
            '=       TAMBAH STOCK BARANG       =
            '===================================
            SQL = "update barang set Good_Stock= Good_Stock + Round(" & totalTambahStock & ",4), Jumlah_Bags = Jumlah_Bags + " & totalTambahBags & " "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & SoTujuan & "' "
            SQL = SQL & " and Kode_Barang='" & kodeBarang & "'"
            'ExecuteTrans(SQL)

            '=================================
            '=     CEK KESESUAIAN STOCK      =
            '=================================
            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & SoTujuan & "' "
            SQL = SQL & "AND a.Kode_Barang = '" & kodeBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
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

#End Region

#Region "JURNAL"

            'dari
            Dim akun_persediaan_dari As String = ""
            Dim akun_persediaan_tujuan As String = ""
            Dim inisial_faktur_dari As String = ""

            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & SoAwal & "' "
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
            SQL = SQL & "and b.kode_stock_owner = '" & SoAwal & "' and b.Kode_Barang='" & kodeBarang & "' "
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
            SQL = SQL & "and b.kode_stock_owner = '" & SoTujuan & "' and b.Kode_Barang='" & kodeBarang & "' "
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
            SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & NoFaktur & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                      Strings.Mid(akun_persediaan_dari, 2, 1),
                      Strings.Mid(Ganti(akun_persediaan_dari), 3),
                      KodePerusahaan, KodeProyek, "Persedian " & NoFaktur, "0", nilai_persediaan_min, pagenumber, SoAwal, Bahasa_Pilihan, Ket_Cost_Center_HO)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                     Strings.Mid(akun_persediaan_tujuan, 2, 1),
                     Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                     KodePerusahaan, KodeProyek, "Persedian " & NoFaktur, nilai_persediaan_min, "0", pagenumber, SoAwal, Bahasa_Pilihan, Ket_Cost_Center_HO)
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

#End Region

            ''PERHATIKAN INI
            SQL = "update Tf_Stock_QC_det set Selesai = 'Y', Kode_Voucher = '" & Kode_voucher & "' "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoFaktur & "' "
            SQL = SQL & "and urut_oto = '" & DataUrutQCDet & "'"
            ExecuteTrans(SQL)

            SQL = "update Tf_Stock_QC_Detail set  "
            SQL = SQL & "Flag_Timbang = 'Y' "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and urut_oto = '" & DataUrutQCDetail & "' "
            ExecuteTrans(SQL)

            '================================
            '=     CEK REQUEST MATERIAL     =
            '================================
#Region "Request Material"

            '=======================================================
            '=     CEK APAKAH DATA RM DAN CEK JUMLAH KEBUTUHAN     =
            '=======================================================
            Dim Jumlah_Kebutuhan_Request As Double = 0
            Dim isDataRequest As Boolean = False
            SQL = "select c.Jumlah as Jumlah_Kebutuhan "
            SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            'SQL = SQL & "and c.Urut_Oto = ( "
            'SQL = SQL & "select x.Urut_Material_Requisition_Convert "
            'SQL = SQL & "from Tf_Stock_QC z, Tf_Stock x, Tf_Stock_Det y, barang_sn r "
            'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.kode_perusahaan = r.kode_perusahaan "
            'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
            'SQL = SQL & "and y.serial_number_awal = r.serial_number "
            'SQL = SQL & "and z.Status is null "
            'SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
            'SQL = SQL & "and y.Selesai is null "
            'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            'SQL = SQL & "and (r.Qr_Code+'-'+r.Kode_Unik_Berjalan) = '" & Txt_ScanBarcode.Text & "') "

            SQL = SQL & "and c.Urut_Oto = " & Urut_Det_Convert & " "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Jumlah_Kebutuhan_Request = Dr("Jumlah_Kebutuhan")
                    isDataRequest = True
                Else
                    isDataRequest = False
                End If
            End Using

            If isDataRequest Then
                '================================
                '=     CEK APAKAH LAST DATA     =
                '================================
                Dim isLastData As Boolean = False
                SQL = "select c.Serial_Number_Awal, (d.Qr_Code+'-'+d.Kode_Unik_Berjalan) as barcode "
                SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_Det c, barang_sn d "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                SQL = SQL & "and c.Serial_Number_Awal = d.Serial_Number "
                SQL = SQL & "and a.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

                'SQL = SQL & "and a.No_Faktur = ( "
                'SQL = SQL & "select z.No_Faktur "
                'SQL = SQL & "from Tf_Stock_QC z, Tf_Stock x, Tf_Stock_Det y, barang_sn r "
                'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.kode_perusahaan = r.kode_perusahaan "
                'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
                'SQL = SQL & "and y.serial_number_awal = r.serial_number "
                'SQL = SQL & "and z.Status is null "
                'SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
                'SQL = SQL & "and y.Selesai is null "
                'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
                'SQL = SQL & "and (r.Qr_Code+'-'+r.Kode_Unik_Berjalan) = '" & Txt_ScanBarcode.Text & "') "

                SQL = SQL & "and a.No_Faktur = '" & NoFaktur & "' "

                SQL = SQL & "and not exists ( "
                SQL = SQL & "select 1 from Tf_Stock_QC_Det2 z where z.kode_perusahaan = c.Kode_Perusahaan and z.no_faktur = c.No_Faktur and z.Urut_Det = c.Urut_Oto) "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        isLastData = True
                    End If
                End Using


                '=====================================
                '=     CEK JUMLAH SUDAH TRANSFER     =
                '=====================================
                Dim Jumlah_Sudah_Transfer As Double = 0
                SQL = "select isnull(sum(d.Jumlah), 0) as Jumlah_Transfer "
                SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_Det c, Tf_Stock_QC_Det2 d "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
                SQL = SQL & "and a.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

                'SQL = SQL & "and b.Urut_Material_Requisition_Convert = ( "
                'SQL = SQL & "select x.Urut_Material_Requisition_Convert "
                'SQL = SQL & "from Tf_Stock_QC z, Tf_Stock x, Tf_Stock_Det y, barang_sn r "
                'SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.kode_perusahaan = r.kode_perusahaan "
                'SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                'SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF "
                'SQL = SQL & "and y.serial_number_awal = r.serial_number "
                'SQL = SQL & "and x.Flag_Jenis_Request = 'PRODUKSI' "
                'SQL = SQL & "and y.Selesai is null "
                'SQL = SQL & "and z.Status is null "
                'SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
                'SQL = SQL & "and (r.Qr_Code+'-'+r.Kode_Unik_Berjalan) = '" & Txt_ScanBarcode.Text & "') "
                SQL = SQL & "and b.Urut_Material_Requisition_Convert = " & Urut_Det_Convert & " "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Jumlah_Sudah_Transfer = Dr("Jumlah_Transfer")
                    End If
                End Using

                '===========================================
                '=     GET NILAI TOLERANSI MIN DAN MAX     =
                '===========================================
                Dim Persen_Toleransi_Min As Double = 0
                Dim Persen_Toleransi_Max As Double = 0
                SQL = "select Toleransi_Tf_Min, Toleransi_Tf_Max from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Stock_Owner = '" & Kd_Soo & "' and Kode_Barang = '" & Kd_Barangg & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Persen_Toleransi_Min = Dr("Toleransi_Tf_Min")
                        Persen_Toleransi_Max = Dr("Toleransi_Tf_Max")
                    End If
                End Using

                Dim nilai_toleransi_min As Double = Jumlah_Kebutuhan_Request - (Jumlah_Kebutuhan_Request * (Persen_Toleransi_Min / 100))
                Dim nilai_toleransi_max As Double = Jumlah_Kebutuhan_Request + (Jumlah_Kebutuhan_Request * (Persen_Toleransi_Max / 100))


                If Jumlah_Sudah_Transfer > nilai_toleransi_max Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah Sudah Transfer Lebih Dari Jumlah Request", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If isLastData Then

                    If Jumlah_Sudah_Transfer < nilai_toleransi_min Then
                        MessageBox.Show("Jumlah Sudah Transfer Kurang Dari Jumlah Request", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else


                        '================================
                        '=       UPDATE DATA FLAG       =
                        '================================
                        SQL = "select a.Kode_Perusahaan "
                        SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                        SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                        SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
                        SQL = SQL & "and a.Status is null "
                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and c.Urut_Oto = '" & Urut_Det_Convert & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then

                                Dr.Close()
                                SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = 'Y' "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Urut_Det_Convert & "' "
                                ExecuteTrans(SQL)

                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using



                    End If

                Else
                    If Jumlah_Sudah_Transfer >= nilai_toleransi_min And Jumlah_Sudah_Transfer <= nilai_toleransi_max Then
                        '================================
                        '=       UPDATE DATA FLAG       =
                        '================================
                        SQL = "select a.Kode_Perusahaan "
                        SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                        SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                        SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
                        SQL = SQL & "and a.Status is null "
                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and c.Urut_Oto = '" & Urut_Det_Convert & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then

                                Dr.Close()
                                SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = 'Y' "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Urut_Det_Convert & "' "
                                ExecuteTrans(SQL)

                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using
                    End If

                End If


            End If

#End Region


            If True Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Tahan")
                Exit Sub
            End If



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Berhasil Di simpan", "Split Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cetak(kode_unik_print, NoFaktur)
        Kosong()

    End Sub

    Private Sub InsertNewBarcode(ByVal QrLama As String, ByVal newKodeUnikBerjalan As String, ByVal kodeBarang As String, ByVal namaBarang As String, ByVal expDate As String, ByVal batchLama As String, ByVal kode_unik_print As String, ByVal urut As String)

        '=====================================
        '=       GENERATE BARCODE BARU       =
        '=====================================

        Dim fullNewQr As String = QrLama & "-" & newKodeUnikBerjalan

        Barcode.Image = Generate_QR(fullNewQr)

        Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStockQC" & kode_unik_print & ".jpg")
        'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
        Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
        'End If

        fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
        FileSize1 = fs1.Length
        rawData1 = New Byte(FileSize1) {}
        fs1.Read(rawData1, 0, FileSize1)
        fs1.Close()
        Cmd.Parameters.Add($"@newBarcode{urut}", SqlDbType.Image).Value = rawData1

        '===================================
        '=       INSERT BARCODE BARU       =
        ''===================================
        Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)
        'SQL = "delete from Cetak_TransferStock_QC where Kode_Perusahaan = '" & KodePerusahaan & "' and "
        'SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
        'ExecuteTrans(SQL)

        SQL = "insert into Cetak_TransferStock_QC (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print) values "
        SQL = SQL & "('" & KodePerusahaan & "', '" & kodeBarang & "', @newBarcode" & urut & ", '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
        SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "' ) "
        ExecuteTrans(SQL)

    End Sub

    Private Sub Cetak(ByVal kodeUnikPrint As String, ByVal noFaktur As String)
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            '=========================
            '=     CETAK BARCODE     =
            '=========================
            SQL = "select Kode_Perusahaan, QrUtuh from Cetak_TransferStock_QC where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & kodeUnikPrint & "'"
            Using Ds2 = BindingTrans(SQL)
                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                    For i As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                        CrDoc = New NewBarcodeTransferStockQC

                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds2)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = "{Cetak_TransferStock_QC.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock_QC.kode_unik_print} = '" & kodeUnikPrint & "' and {Cetak_TransferStock_QC.QrUtuh} = '" & Ds2.Tables("MyTable").Rows(i).Item("QrUtuh") & "'"
                        '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Stock"
                        '    .Text = "New Barcode Transfer Stock QC"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        '==============================================================================================================================================================

                        CrDoc.SetDataSource(Ds2)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{Cetak_TransferStock_QC.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock_QC.kode_unik_print} = '" & kodeUnikPrint & "' and {Cetak_TransferStock_QC.QrUtuh} = '" & Ds2.Tables("MyTable").Rows(i).Item("QrUtuh") & "'"

                        CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        CrDoc.PrintToPrinter(1, False, 1, 2500)

                    Next

                End If
            End Using

            '================================================
            '=     CEK APAKAH SEMUA SN SUDAH DI VALIDASI    =
            '================================================
            SQL = "select Kode_Perusahaan from Tf_Stock_QC_det "
            SQL = SQL + "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noFaktur & "' and Selesai is null"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CloseConn()
                    Kosong()
                    Exit Sub
                End If
            End Using

            '=================================
            '=     CETAK FAKTUR TF STOCK     =
            '=================================

            SQL = "select Kode_Perusahaan from Vw_Tf_Stock_Detail_QC where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noFaktur & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New Rpt_EMI_Faktur_Transfer_Stock_Detail_QC
                    kertas = "Faktur"

                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = "{Vw_Tf_Stock_Detail_QC.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Tf_Stock_Detail_QC.No_Faktur}='" & noFaktur & "' "
                        CrDoc.SummaryInfo.ReportTitle = "TF"
                        .Text = "TF"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With

                    '============================================================================================================================================
                    '============================================================================================================================================
                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.PrintOptions.PrinterName = PrinterNameTS
                    'CrDoc.RecordSelectionFormula = "{Vw_Tf_Stock_Detail_QC.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Tf_Stock_Detail_QC.No_Faktur}='" & noFaktur & "' "
                    ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterNameTS
                    'doctoprint.DefaultPageSettings.Landscape = True
                    'Dim rawKind As Integer
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        Exit For
                    '    End If
                    'Next

                    'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    'CrDoc.PrintToPrinter(1, False, 1, 99)

                    'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_ScanBarcode_TextChanged(sender As Object, e As EventArgs) Handles Txt_ScanBarcode.TextChanged

    End Sub

End Class