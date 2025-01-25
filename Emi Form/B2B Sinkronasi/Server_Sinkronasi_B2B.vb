Imports System.Net
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.IO.Ports
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class Server_Sinkronasi_B2B

    Dim arrId_Proyeks, arrKd_Brg, arrSO, arrNo_Rab, arrNo_Fak, arrEdit, arrHapus, arrKdSupplier, arrNo_Fak2, arrNoUrut As New ArrayList

    Dim arrNo_PO, arrNoPo2, arrKodeSupplier, arrNoPenawaranPackaging, arrNoPenawaranBahanBaku As New ArrayList

    Dim Faktur_Penawaran As String = ""

    Private Sub insert_sql_to_mysql_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListView1.Columns.Add("Error", 900, HorizontalAlignment.Left)
        ListView1.View = View.Details

        Dim Lvw As ListViewItem
        Lvw = ListView1.Items.Add("Sql-MySql : Tes")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        get_jam()
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrNo_PO.Clear()
            SQLB2B = "select no_faktur from B2B_Purchase_Order where status is null and flag_sudah_pindah is null and flag_selesai = 'Y'  order by no_faktur"
            Using DsSQL = BindingTransB2B(SQLB2B)
                With DsSQL.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrNo_PO.Add(.Rows(i).Item("no_faktur"))
                    Next
                End With
            End Using

            SQLB2B = "select kode_perusahaan,no_faktur,no_do,lokasi,kode_supplier,Id_Kendaraan,Driver,ETD,eta,plat,telpon,tanggal,jam,Id_User,cara_kirim,harga "
            SQLB2B = SQLB2B & "from B2B_Purchase_Order where flag_sudah_pindah is null and status is null and flag_selesai = 'Y' order by No_Faktur  "
            Using DsSQL = BindingTransB2B(SQLB2B)
                With DsSQL.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1

                        Dim no_do As String = ""
                        If General_Class.CekNULL(.Rows(i).Item("no_do")) = "" Then
                            no_do = "NULL,"
                        Else
                            no_do = "'" & .Rows(i).Item("no_do") & "',"
                        End If

                        SQL = "insert into emi_pembelian_loading(kode_perusahaan,no_faktur,no_sj,lokasi,kode_supplier,Driver,Tanggal_OTW,eta,No_Plat,telpon,tanggal,jam,UseriD,cara_kirim,biaya_perjalanan) "
                        SQL = SQL & "values ('" & .Rows(i).Item("kode_perusahaan") & "','" & .Rows(i).Item("no_faktur") & "',"

                        SQL = SQL & "" & no_do & " "

                        SQL = SQL & "'" & .Rows(i).Item("lokasi") & "','" & .Rows(i).Item("kode_supplier") & "',"
                        SQL = SQL & "'" & .Rows(i).Item("driver") & "', '" & Format(.Rows(i).Item("etd"), "yyyy-MM-dd") & "',"
                        SQL = SQL & "'" & Format(.Rows(i).Item("eta"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("plat") & "','" & .Rows(i).Item("telpon") & "',"
                        SQL = SQL & "'" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "','" & .Rows(i).Item("jam") & "','" & .Rows(i).Item("Id_User") & "',"
                        SQL = SQL & "'" & .Rows(i).Item("cara_kirim") & "', '" & .Rows(i).Item("harga") & "' "
                        SQL = SQL & ")"
                        ExecuteTrans(SQL)
                    Next
                End With
            End Using

            '==========================================
            ' dapatkan total berat dalam gram
            '==========================================

            For z As Integer = 0 To arrNo_PO.Count - 1

                Dim pecahan As Double = 0
                Dim totalSeluruhBarangSatuanKecil As Double = 0

                '==========================================
                ' dapatkan jumlah keseluruhan berat dalam gram
                '==========================================
                SQLB2B = "select a.Kode_Barang,  "
                SQLB2B = SQLB2B & "a.Qty_Kirim,a.Satuan, e.Berat, e.satuan as satuan_kecil, a.urut_po "
                SQLB2B = SQLB2B & "from B2B_Detail_Purchase_Order a,b2b_purchase_order c, barang e "
                SQLB2B = SQLB2B & "where a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
                SQLB2B = SQLB2B & "and a.kode_perusahaan = c.kode_perusahaan and a.No_Faktur = c.No_Faktur "
                SQLB2B = SQLB2B & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & arrNo_PO.Item(z).ToString & "' "
                SQLB2B = SQLB2B & "order by a.kode_barang  "
                Using DsB2B = BindingTransB2B(SQLB2B)
                    With DsB2B.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1

                            '==========================================
                            ' convert jumlah ke satuan kecil
                            '==========================================
                            Dim jumlahConvert As Double = 0
                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("kode_barang") & "',"
                            SQL = SQL & "'" & .Rows(i).Item("satuan") & "','" & .Rows(i).Item("satuan_kecil") & "',"
                            SQL = SQL & "" & .Rows(i).Item("qty_kirim") & ") as Hasil "
                            Using dr4 = OpenTrans(SQL)
                                If dr4.Read Then
                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                        If dr4("Hasil") = 0 Then
                                            MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & .Rows(i).Item("satuan_kecil") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            dr4.Close()
                                            CloseTrans()
                                            CloseTransB2B()
                                            CloseConn()
                                            CloseConnB2B()
                                            Exit Sub
                                        Else
                                            jumlahConvert = dr4("hasil")

                                        End If
                                    Else
                                        dr4.Close()
                                        CloseTrans()
                                        CloseTransB2B()
                                        CloseConn()
                                        CloseConnB2B()
                                        MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & .Rows(i).Item("satuan_kecil") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            Dim totalSatuanKecilPerbarang As Double = 0

                            totalSatuanKecilPerbarang = Val(jumlahConvert) * Val(.Rows(i).Item("berat"))

                            totalSeluruhBarangSatuanKecil = totalSeluruhBarangSatuanKecil + totalSatuanKecilPerbarang

                        Next
                    End With
                End Using

                '==========================
                ' Proses simpan ke EMI DB
                '=========================

                SQLB2B = "select  a.Kode_Perusahaan, c.harga as biaya_kirim	,a.No_Faktur,	a.No_PO,	a.Urut_PO,a.Kode_Stock_Owner,	a.Kode_Barang,d.Berat, "
                SQLB2B = SQLB2B & "b.Tanggal_Produksi,	b.Tanggal_Expired, b.Quantity,a.qty_order,	a.Satuan, b.urut_oto, b.No_Batch , d.Satuan as Satuan_Kecil "
                SQLB2B = SQLB2B & "from B2B_Detail_Purchase_Order a, B2B_Detail_Batch_Purchase_Order b, b2b_purchase_order c, barang d "
                SQLB2B = SQLB2B & "where a.kode_perusahaan = b.kode_perusahaan and a.No_Urut = b.No_Urut  "
                SQLB2B = SQLB2B & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Faktur  "
                SQLB2B = SQLB2B & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and a.Kode_Barang = d.Kode_Barang  "
                SQLB2B = SQLB2B & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & arrNo_PO.Item(z).ToString & "' "
                SQLB2B = SQLB2B & "order by a.kode_barang  "
                Using DsSQL = BindingTransB2B(SQLB2B)
                    With DsSQL.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim satuanBarang As String = ""
                            Dim isi_Per_Bags As Double = 0
                            Dim Satuan_Isi_Bags As String = ""
                            SQL = "select satuan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags, isnull(Satuan_Isi_Bags,'') as Satuan_Isi_Bags from barang where kode_perusahaan = '" & KodePerusahaan & "'  "
                            SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' "
                            SQL = SQL & "and kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    satuanBarang = Dr("satuan")
                                    isi_Per_Bags = Dr("isi_Per_Bags")
                                    Satuan_Isi_Bags = Dr("Satuan_Isi_Bags")
                                Else
                                    CloseTrans()
                                    CloseTransB2B()
                                    CloseConn()
                                    CloseConnB2B()
                                    MessageBox.Show("error insert purchase order, satuan barang tidak ditemukan")
                                    Exit Sub
                                End If
                            End Using

                            '==========================================
                            ' convert jumlah ke satuan kecil
                            '==========================================
                            Dim jumlahBarangDibutuhkan As Double = 0
                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("kode_barang") & "',"
                            SQL = SQL & "'" & .Rows(i).Item("satuan") & "','" & satuanBarang & "',"
                            SQL = SQL & "" & .Rows(i).Item("Quantity") & ") as Hasil "
                            Using dr4 = OpenTrans(SQL)
                                If dr4.Read Then
                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                        If dr4("Hasil") = 0 Then
                                            MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            dr4.Close()
                                            CloseTrans()
                                            CloseTransB2B()
                                            CloseConn()
                                            CloseConnB2B()
                                            Exit Sub
                                        Else
                                            jumlahBarangDibutuhkan = dr4("hasil")

                                        End If
                                    Else
                                        dr4.Close()
                                        CloseTrans()
                                        CloseTransB2B()
                                        CloseConn()
                                        CloseConnB2B()
                                        MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            Dim hargaPOSatuanKecil As Double = 0
                            Dim hargaPOSatuanDisplay As Double = 0

                            SQLB2B = "select harga_barang,harga From EMI_Pembelian_PO_Detail where No_Urut = " & .Rows(i).Item("urut_po") & " "
                            Using DrB2B = OpenTransB2B(SQLB2B)
                                If DrB2B.Read Then
                                    hargaPOSatuanKecil = DrB2B("harga_barang")
                                    hargaPOSatuanDisplay = DrB2B("harga")
                                Else
                                    DrB2B.Close()
                                    CloseTrans()
                                    CloseTransB2B()
                                    CloseConn()
                                    CloseConnB2B()
                                    MessageBox.Show("Harga Penawaran Tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '==========================================
                            ' select satuan display barang
                            '==========================================
                            Dim satuanTampilDisplay As String

                            SQL = "select satuan from barang_detail_satuan where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
                            SQL = SQL & "and flag_tampil_display = 'Y' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    satuanTampilDisplay = Dr("satuan")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseTransB2B()
                                    CloseConn()
                                    CloseConnB2B()
                                    MessageBox.Show("Detail satuan barang tampil display tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '==========================================
                            ' convert jumlah ke satuan kecil
                            '==========================================
                            Dim jumlahSatuanTampilDisplay As Double = 0
                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("kode_barang") & "',"
                            SQL = SQL & "'" & .Rows(i).Item("satuan_kecil") & "','" & satuanTampilDisplay & "',"
                            SQL = SQL & "" & .Rows(i).Item("berat") & ") as Hasil "
                            Using dr4 = OpenTrans(SQL)
                                If dr4.Read Then
                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                        If dr4("Hasil") = 0 Then
                                            MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            dr4.Close()
                                            CloseTrans()
                                            CloseTransB2B()
                                            CloseConn()
                                            CloseConnB2B()
                                            Exit Sub
                                        Else
                                            jumlahSatuanTampilDisplay = dr4("hasil")

                                        End If
                                    Else
                                        dr4.Close()
                                        CloseTrans()
                                        CloseTransB2B()
                                        CloseConn()
                                        CloseConnB2B()
                                        MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            Dim biayaPerGram As Double = 0
                            Dim totalHppPerBarang As Double = 0
                            Dim biayaKirimPerbarang As Double = 0

                            biayaPerGram = Val(.Rows(i).Item("biaya_kirim")) / Val(totalSeluruhBarangSatuanKecil)

                            biayaKirimPerbarang = Val(jumlahSatuanTampilDisplay) * biayaPerGram

                            totalHppPerBarang = Val(biayaKirimPerbarang) + Val(hargaPOSatuanDisplay)

                            SQL = "insert into EMI_Pembelian_Loading_Detail (kode_perusahaan,no_faktur,no_po,Urut_PO,Kode_Stock_Owner,Kode_Barang, "
                            SQL = SQL & "Tanggal_Produksi,Tanggal_Expired,No_Urut_B2B,Jumlah,satuan,Jumlah_Barang, jumlah_masuk,satuan_barang,jumlah_per_bag, No_Batch, Satuan_Per_Bag,harga_barang, hpp_satuan_display ) "
                            SQL = SQL & "values ('" & .Rows(i).Item("kode_perusahaan") & "','" & .Rows(i).Item("no_faktur") & "','" & .Rows(i).Item("no_po") & "',"
                            SQL = SQL & "'" & .Rows(i).Item("urut_po") & "','" & .Rows(i).Item("kode_stock_owner") & "',"
                            SQL = SQL & "'" & .Rows(i).Item("kode_barang") & "','" & .Rows(i).Item("Tanggal_Produksi") & "','" & .Rows(i).Item("Tanggal_Expired") & "',"
                            SQL = SQL & "'" & .Rows(i).Item("urut_oto") & "','" & .Rows(i).Item("Quantity") & "',"
                            SQL = SQL & "'" & .Rows(i).Item("satuan") & "'," & HilangkanTanda(Format(jumlahBarangDibutuhkan, "N2")) & ", 0 , '" & satuanBarang & "',"
                            SQL = SQL & "" & isi_Per_Bags & ", '" & .Rows(i).Item("No_Batch") & "', '" & Satuan_Isi_Bags & "', '" & hargaPOSatuanKecil & "', '" & totalHppPerBarang & "' )"
                            ExecuteTrans(SQL)
                        Next
                    End With
                End Using

                SQLB2B = "Update B2B_Purchase_Order set flag_sudah_pindah = 'Y' where "
                SQLB2B = SQLB2B & "no_faktur = '" & arrNo_PO.Item(z).ToString & "' "
                ExecuteTransB2B(SQLB2B)

            Next

            '==========================================
            ' Biaya / total berat
            '==========================================

            'CloseTrans()
            'CloseTransB2B()
            'CloseConn()
            'CloseConnB2B()
            'MessageBox.Show("Pesan Harus dihapus!")
            'Exit Sub

            'Dim j As Integer = 0
            'For z As Integer = 1 To arrNo_PO.Count
            '    SQLB2B = "select  a.Kode_Perusahaan,	a.No_Faktur,	a.No_PO,	a.Urut_PO,a.Kode_Stock_Owner,	a.Kode_Barang,	b.Tanggal_Produksi,	b.Tanggal_Expired, "
            '    SQLB2B = SQLB2B & "b.Quantity,a.qty_order,	a.Satuan, b.urut_oto, b.No_Batch "
            '    SQLB2B = SQLB2B & "from B2B_Detail_Purchase_Order a, B2B_Detail_Batch_Purchase_Order b "
            '    SQLB2B = SQLB2B & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and "
            '    SQLB2B = SQLB2B & "a.no_urut = b.no_urut "
            '    '  SQLB2B = SQLB2B & "and a.No_PO = b.No_PO and a.Kode_Barang  = b.Kode_Barang "
            '    SQLB2B = SQLB2B & "and a.no_faktur = '" & arrNo_PO.Item(j).ToString & "'"

            '    Using DsSQL = BindingTransB2B(SQLB2B)
            '        With DsSQL.Tables("MyTable")
            '            For i As Integer = 0 To .Rows.Count - 1

            '                Dim satuanBarang As String = ""
            '                Dim isi_Per_Bags As Double = 0
            '                Dim Satuan_Isi_Bags As String = ""
            '                SQL = "select satuan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags, isnull(Satuan_Isi_Bags,'') as Satuan_Isi_Bags from barang where kode_perusahaan = '" & KodePerusahaan & "'  "
            '                SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' "
            '                SQL = SQL & "and kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
            '                Using Dr = OpenTrans(SQL)
            '                    If Dr.Read Then
            '                        satuanBarang = Dr("satuan")
            '                        isi_Per_Bags = Dr("isi_Per_Bags")
            '                        Satuan_Isi_Bags = Dr("Satuan_Isi_Bags")
            '                    Else
            '                        CloseTrans()
            '                        CloseTransB2B()
            '                        CloseConn()
            '                        CloseConnB2B()
            '                        MessageBox.Show("error insert purchase order, satuan barang tidak ditemukan")
            '                        Exit Sub
            '                    End If
            '                End Using

            '                '==========================================
            '                ' convert jumlah ke satuan kecil
            '                '==========================================
            '                Dim jumlahBarangDibutuhkan As Double = 0
            '                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("kode_barang") & "',"
            '                SQL = SQL & "'" & .Rows(i).Item("satuan") & "','" & satuanBarang & "',"
            '                SQL = SQL & "" & .Rows(i).Item("Quantity") & ") as Hasil "
            '                Using dr4 = OpenTrans(SQL)
            '                    If dr4.Read Then
            '                        If General_Class.CekNULL(dr4("Hasil")) <> "" Then
            '                            If dr4("Hasil") = 0 Then
            '                                MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                                dr4.Close()
            '                                CloseTrans()
            '                                CloseTransB2B()
            '                                CloseConn()
            '                                CloseConnB2B()
            '                                Exit Sub
            '                            Else
            '                                jumlahBarangDibutuhkan = dr4("hasil")

            '                            End If
            '                        Else
            '                            dr4.Close()
            '                            CloseTrans()
            '                            CloseTransB2B()
            '                            CloseConn()
            '                            CloseConnB2B()
            '                            MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                            Exit Sub
            '                        End If
            '                    End If
            '                End Using

            '                '==========================================
            '                ' convert harga ke satuan kecil
            '                '==========================================

            '                Dim hargaPO As Double = 0

            '                SQL = "select harga_barang From EMI_Pembelian_PO_Detail where No_Urut = " & .Rows(i).Item("urut_po") & " "
            '                Using Dr5 = OpenTrans(SQL)
            '                    If Dr5.Read Then
            '                        hargaPO = Dr5("harga_barang")
            '                    Else
            '                        Dr5.Close()
            '                        CloseTrans()
            '                        CloseTransB2B()
            '                        CloseConn()
            '                        CloseConnB2B()
            '                        MessageBox.Show("Harga Penawaran Tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                        Exit Sub
            '                    End If
            '                End Using

            '                SQL = "insert into EMI_Pembelian_Loading_Detail (kode_perusahaan,no_faktur,no_po,Urut_PO,Kode_Stock_Owner,Kode_Barang, "
            '                SQL = SQL & "Tanggal_Produksi,Tanggal_Expired,No_Urut_B2B,Jumlah,satuan,Jumlah_Barang, jumlah_masuk,satuan_barang,jumlah_per_bag, No_Batch, Satuan_Per_Bag,harga_barang ) "
            '                SQL = SQL & "values ('" & .Rows(i).Item("kode_perusahaan") & "','" & .Rows(i).Item("no_faktur") & "','" & .Rows(i).Item("no_po") & "',"
            '                SQL = SQL & "'" & .Rows(i).Item("urut_po") & "','" & .Rows(i).Item("kode_stock_owner") & "',"
            '                SQL = SQL & "'" & .Rows(i).Item("kode_barang") & "','" & .Rows(i).Item("Tanggal_Produksi") & "','" & .Rows(i).Item("Tanggal_Expired") & "',"
            '                SQL = SQL & "'" & .Rows(i).Item("urut_oto") & "','" & .Rows(i).Item("Quantity") & "',"
            '                SQL = SQL & "'" & .Rows(i).Item("satuan") & "'," & HilangkanTanda(Format(jumlahBarangDibutuhkan, "N2")) & ", 0 , '" & satuanBarang & "',"
            '                SQL = SQL & "" & isi_Per_Bags & ", '" & .Rows(i).Item("No_Batch") & "', '" & Satuan_Isi_Bags & "', '" & hargaPO & "' )"
            '                ExecuteTrans(SQL)
            '            Next
            '        End With
            '    End Using

            '    SQLB2B = "Update B2B_Purchase_Order set flag_sudah_pindah = 'Y' where "
            '    SQLB2B = SQLB2B & "no_faktur = '" & arrNo_PO.Item(j).ToString & "' "
            '    ExecuteTransB2B(SQLB2B)

            '    j = j + 1
            'Next

            'SQLB2B = "select  a.Kode_Perusahaan,	a.No_Faktur,	a.No_PO,	a.Urut_PO,a.Kode_Stock_Owner,	a.Kode_Barang,	b.Tanggal_Produksi,	b.Tanggal_Expired,a.Qty_Kirim,	a.Satuan "
            'SQLB2B = SQLB2B & "from B2B_Purchase_Order where flag_sudah_pindah is null and status is null order by No_Faktur  "
            'Using DsSQL = BindingTransB2B(SQLB2B)
            '    With DsSQL.Tables("MyTable")
            '        For i As Integer = 0 To .Rows.Count - 1
            '            SQL = "insert into emi_pembelian_loading(kode_perusahaan,no_faktur,no_sj,lokasi,kode_supplier,Id_Ekspedisi,Driver,ETD,eta,No_Plat,telpon,tanggal,jam,UseriD) "
            '            SQL = SQL & "values ('" & .Rows(i).Item("kode_perusahaan") & "','" & .Rows(i).Item("no_faktur") & "',"
            '            SQL = SQL & "'" & .Rows(i).Item("lokasi") & "','" & .Rows(i).Item("kode_supplier") & "','" & .Rows(i).Item("id_kendaraan") & "',"
            '            SQL = SQL & "'" & .Rows(i).Item("driver") & "', '" & Format(.Rows(i).Item("etd"), "yyyy-MM-dd") & "',"
            '            SQL = SQL & "'" & Format(.Rows(i).Item("eta"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("plat") & "','" & .Rows(i).Item("telpon") & "',"
            '            SQL = SQL & "'" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "','" & .Rows(i).Item("jam") & "','" & .Rows(i).Item("Id_User") & "')"
            '            ExecuteTrans(SQL)
            '        Next
            '    End With
            'End Using

            'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & "insert purchase order")
            Exit Sub
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        get_jam()
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrNo_PO.Clear()

            SQL = "select a.no_faktur from emi_pembelian_po a, suppliers b, suppliers_kategori c where "
            SQL = SQL & "a.kode_Perusahaan=b.kode_Perusahaan and a.Kode_supplier=b.kode_supplier and "
            SQL = SQL & "b.kode_Perusahaan=c.kode_Perusahaan and b.id_kategori_suppliers=c.id_kategori_suppliers and "
            SQL = SQL & "a.flag_sudah_pindah is null and c.flag_jenis_lokal='Y'  "
            SQL = SQL & "order by no_faktur"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrNo_PO.Add(.Rows(i).Item("no_faktur"))
                    Next
                End With
            End Using

            Dim j As Integer = 0
            For z As Integer = 1 To arrNo_PO.Count

                SQL = " select kode_Perusahaan, no_faktur, No_Nota, Tanggal, jam, UserID, Kode_Supplier, lokasi, Jenis_Pembayaran, Cara_Bayar, "
                SQL = SQL & "Tgl_Jatuh_Tempo, Total_MUA, Mata_Uang, kurs, Total_IDR, Grand_Sebelum_PPN, ppn, grand, ETD_Simulasi, Ekspedisi, "
                SQL = SQL & "Biaya, Flag_Release, Tanggal_Release, Jam_Release, User_Release from emi_pembelian_po where "
                SQL = SQL & "flag_sudah_pindah is null and status is null and no_faktur = '" & arrNo_PO.Item(j).ToString & "'  "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1

                            SQLB2B = "insert into emi_pembelian_po(kode_Perusahaan, no_faktur, No_Nota, Tanggal, jam, UserID, Kode_Supplier, Lokasi, Jenis_Pembayaran, Cara_Bayar, "
                            SQLB2B = SQLB2B & "Tgl_Jatuh_Tempo, Total_MUA, mata_uang, kurs, Total_IDR, Grand_Sebelum_PPN, PPN, grand, ETD_Simulasi, Ekspedisi, "
                            SQLB2B = SQLB2B & "Biaya, Flag_Release, Tanggal_Release, Jam_Release, User_Release) values ("
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("kode_Perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "','" & .Rows(i).Item("No_Nota") & "','" & .Rows(i).Item("Tanggal") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("jam") & "', '" & .Rows(i).Item("UserID") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kode_Supplier") & "', '" & .Rows(i).Item("Lokasi") & "','" & .Rows(i).Item("Jenis_Pembayaran") & "',"

                            If General_Class.CekNULL(.Rows(i).Item("Cara_Bayar")) = "" Then
                                SQLB2B = SQLB2B & "NULL, "
                            Else
                                SQLB2B = SQLB2B & "'" & .Rows(i).Item("Cara_Bayar") & "', "
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Tgl_Jatuh_Tempo")) = "" Then
                                SQLB2B = SQLB2B & "NULL, "
                            Else
                                SQLB2B = SQLB2B & "'" & .Rows(i).Item("Tgl_Jatuh_Tempo") & "', "
                            End If

                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Total_MUA") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("mata_uang") & "','" & .Rows(i).Item("kurs") & "','" & .Rows(i).Item("Total_IDR") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Grand_Sebelum_PPN") & "','" & .Rows(i).Item("PPN") & "','" & .Rows(i).Item("grand") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("ETD_Simulasi") & "','" & .Rows(i).Item("Ekspedisi") & "','" & .Rows(i).Item("Biaya") & "',"

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Release")) = "" Then
                                SQLB2B = SQLB2B & "NULL,NULL,NULL,NULL)"
                            Else
                                SQLB2B = SQLB2B & "'" & .Rows(i).Item("Flag_Release") & "','" & .Rows(i).Item("Tanggal_Release") & "','" & .Rows(i).Item("Jam_Release") & "',"
                                SQLB2B = SQLB2B & "'" & .Rows(i).Item("User_Release") & "')"
                            End If

                            ExecuteTransB2B(SQLB2B)

                        Next
                    End With
                End Using

                SQL = " select kode_Perusahaan, no_faktur, Kode_Stock_Owner, Kode_Barang, No_Urut, Jumlah, Satuan, Harga, "
                SQL = SQL & "Nilai_Barang, Satuan_Barang, Harga_Barang, Total, No_Penawaran, Flag_Prepare from "
                SQL = SQL & "EMI_Pembelian_PO_detail where "
                SQL = SQL & " no_faktur = '" & arrNo_PO.Item(j).ToString & "'"

                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1
                            SQLB2B = " insert into EMI_Pembelian_PO_detail(kode_Perusahaan, no_faktur, Kode_Stock_Owner, Kode_Barang, "
                            SQLB2B = SQLB2B & "No_Urut, Jumlah, Satuan, Harga, Nilai_Barang, Satuan_Barang, Harga_Barang, "
                            SQLB2B = SQLB2B & "Total, No_Penawaran, Flag_Prepare) values ('" & .Rows(i).Item("kode_Perusahaan") & "','" & .Rows(i).Item("no_faktur") & "', "
                            SQLB2B = SQLB2B & " '" & .Rows(i).Item("Kode_Stock_Owner") & "','" & .Rows(i).Item("Kode_Barang") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("No_Urut") & "','" & .Rows(i).Item("Jumlah") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Satuan") & "','" & .Rows(i).Item("Harga") & "','" & .Rows(i).Item("Nilai_Barang") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Satuan_Barang") & "','" & .Rows(i).Item("Harga_Barang") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Total") & "','" & .Rows(i).Item("No_Penawaran") & "', "
                            SQLB2B = SQLB2B & "'T' )"
                            ExecuteTransB2B(SQLB2B)
                        Next
                    End With
                End Using

                SQL = "Update EMI_Pembelian_PO set flag_sudah_pindah = 'Y' where "
                SQL = SQL & "no_faktur = '" & arrNo_PO.Item(j).ToString & "' "
                ExecuteTrans(SQL)

                j = j + 1
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & "insert purchase order")
            Exit Sub
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrKd_Brg.Clear() : arrSO.Clear()
            SQL = "select Kode_Barang, Kode_Stock_Owner from barang where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "flag_sudah_pindah is null"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrKd_Brg.Add(.Rows(i).Item("Kode_Barang"))
                        arrSO.Add(.Rows(i).Item("Kode_Stock_Owner"))
                    Next
                End With
            End Using

            Dim j As Integer = 0
            For z As Integer = 1 To arrSO.Count

                SQL = "Select Kode_Perusahaan,Kode_Barang,Kode_Stock_Owner,Nama,Satuan,good_stock,kode_kategori_besar,kode_kategori_kecil, "
                SQL = SQL & "id_routing,metode_pengeluaran_stok,id_group_Jenis "
                SQL = SQL & "from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Kode_Stock_Owner = '" & arrSO.Item(j).ToString & "' and "
                SQL = SQL & "Kode_Barang = '" & arrKd_Brg.Item(j).ToString & "' and "
                SQL = SQL & "flag_sudah_pindah is null"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1
                            'Insert ke MySql
                            SQLB2B = "insert into barang (kode_perusahaan,kode_barang,kode_stock_owner,nama,satuan,good_stock,kode_kategori_besar,kode_kategori_kecil, "
                            SQLB2B = SQLB2B & "id_routing,metode_pengeluaran_stok,id_group_jenis, flag_sendiri"
                            SQLB2B = SQLB2B & ")"
                            SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("Kode_Barang") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kode_Stock_Owner") & "', '" & .Rows(i).Item("Nama") & "', "
                            SQLB2B = SQLB2B & " '" & .Rows(i).Item("Satuan") & "', '" & .Rows(i).Item("good_stock") & "', '" & .Rows(i).Item("kode_kategori_besar") & "' , '" & .Rows(i).Item("kode_kategori_kecil") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("id_routing") & "' , '" & .Rows(i).Item("metode_pengeluaran_stok") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("id_group_jenis") & "', 'Y' "
                            SQLB2B = SQLB2B & ")"
                            ExecuteTransB2B(SQLB2B)

                        Next
                    End With
                End Using

                SQL = "Update barang set flag_sudah_pindah = 'Y' where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
                SQL = SQL & "Kode_Stock_Owner = '" & arrSO.Item(j).ToString & "' and "
                SQL = SQL & "Kode_Barang = '" & arrKd_Brg.Item(j).ToString & "' "
                ExecuteTrans(SQL)

                j = j + 1
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & " insert barang")
            Exit Sub
        End Try
    End Sub

    Private Sub btnPnwrBahanBaku_Click(sender As Object, e As EventArgs) Handles btnPnwrBahanBaku.Click
        get_jam()
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrNoPenawaranBahanBaku.Clear()
            SQLB2B = "select a.No_Faktur From B2B_Penawaran_Bahan_Baku a , B2B_Penawaran_Bahan_Baku_Detail b "
            SQLB2B = SQLB2B & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQLB2B = SQLB2B & "and a.Status is null "
            SQLB2B = SQLB2B & "and b.Flag_Approval = 'A' "
            SQLB2B = SQLB2B & "and b.Flag_Sudah_Pindah is null "
            SQLB2B = SQLB2B & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQLB2B = SQLB2B & "group by a.No_Faktur "
            Using DsB2B = BindingTransB2B(SQLB2B)
                With DsB2B.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrNoPenawaranBahanBaku.Add(.Rows(i).Item("No_Faktur"))
                    Next
                End With
            End Using

            For z As Integer = 0 To arrNoPenawaranBahanBaku.Count - 1

                get_no_faktur_penawaran()

                Dim flag_kategori_Supplier As String = ""
                Dim Data_Faktur_Penawaran As String = ""
                Dim arrDataDetailPenawaran As New ArrayList

                Dim hasInsert As Boolean = False

                SQLB2B = "select Kode_Perusahaan, no_faktur, No_Penawaran, Tanggal_Awal_Berlaku_Pnwr as Tgl_Penawaran_Hrg, "
                SQLB2B = SQLB2B & "Tanggal_Akhir_Berlaku_Pnwr as Periode_Akhir_Penawaran, Kode_Supplier, Lokasi, Tanggal, Jam, Id_User "
                SQLB2B = SQLB2B & "from B2B_Penawaran_Bahan_Baku "
                SQLB2B = SQLB2B & "where kode_perusahaan = '" & KodePerusahaan & "'"
                SQLB2B = SQLB2B & "and no_faktur = '" & arrNoPenawaranBahanBaku.Item(z).ToString & "' "
                Using DsB2B = BindingTransB2B(SQLB2B)
                    With DsB2B.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1

                            '==================================================
                            '=     CEK APAKAH ADA DATA DI PENAWARAN INDUK     =
                            '==================================================
                            SQL = "select Top 1 Kode_Perusahaan, No_Faktur from emi_master_penawaran where Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur_B2B = '" & .Rows(i).Item("no_faktur") & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Data_Faktur_Penawaran = Dr("No_Faktur")

                                Else
                                    Dr.Close()
                                    SQL = "insert into emi_master_penawaran(Kode_Perusahaan, No_Faktur, No_Faktur_B2B, No_Penawaran, Tgl_Penawaran_Hrg, Periode_Akhir_Penawaran, Kode_Supplier, Lokasi, "
                                    SQL = SQL & "Tanggal, Jam, iduser) values ( "
                                    SQL = SQL & "'" & .Rows(i).Item("kode_perusahaan") & "', '" & Faktur_Penawaran & "', '" & .Rows(i).Item("no_faktur") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("No_Penawaran") & "', '" & .Rows(i).Item("tgl_penawaran_hrg") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("periode_akhir_penawaran") & "', '" & .Rows(i).Item("kode_supplier") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("lokasi") & "', '" & .Rows(i).Item("tanggal") & "', '" & .Rows(i).Item("jam") & "', '" & .Rows(i).Item("Id_User") & "' )"
                                    ExecuteTrans(SQL)

                                    Data_Faktur_Penawaran = Faktur_Penawaran
                                End If
                            End Using



                            '===================
                            '=     RELEASE     =
                            '===================
                            'GET KATEGORI SUPPLIER
                            SQL = "select b.kode_supplier, b.ID_Kategori_Suppliers, c.flag_jenis_import  from  Suppliers b, Suppliers_Kategori c "
                            SQL = SQL & "where "
                            SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan and b.id_kategori_suppliers = c.id_kategori_suppliers "
                            SQL = SQL & "and b.kode_perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and b.Kode_Supplier = '" & .Rows(i).Item("kode_supplier") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    flag_kategori_Supplier = General_Class.CekNULL(Dr("flag_jenis_import"))
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseTransB2B()
                                    CloseConn()
                                    CloseConnB2B()
                                    MessageBox.Show("Supplier " & .Rows(i).Item("kode_supplier") & " Tidak Ditemukan...!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub

                                End If
                            End Using


                            'MASUK KE DETAIL B2B
                            SQLB2B = "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Mata_Uang, Harga, Satuan, MOQ, No_Urut "
                            SQLB2B = SQLB2B & "from B2B_Penawaran_Bahan_Baku_Detail "
                            SQLB2B = SQLB2B & "where Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' "
                            SQLB2B = SQLB2B & "and No_Faktur = '" & .Rows(i).Item("No_Faktur") & "' "
                            SQLB2B = SQLB2B & "and Flag_Approval = 'A' "
                            SQLB2B = SQLB2B & "and Flag_Sudah_Pindah is null "
                            Using Ds2B2B = BindingTransB2B(SQLB2B)
                                For j As Integer = 0 To Ds2B2B.Tables("MyTable").Rows.Count - 1

                                    hasInsert = True

                                    Dim Detail_KdPerusahaan As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Kode_Perusahaan")
                                    Dim Detail_KdBarang As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Kode_Barang")
                                    Dim Detail_MataUang As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Mata_Uang")
                                    Dim Detail_Harga As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Harga")
                                    Dim Detail_IdSatuan As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Satuan")
                                    Dim Detail_MOQ As String = Ds2B2B.Tables("MyTable").Rows(j).Item("MOQ")
                                    Dim Detail_UrutB2B As String = Ds2B2B.Tables("MyTable").Rows(j).Item("No_Urut")

                                    '===================================
                                    '=     INSERT PENAWARAN DETAIL     =
                                    '===================================
                                    'GET KODE SATUAN
                                    Dim Kode_Satuan As String = ""
                                    SQLB2B = "select Kode_Satuan, Keterangan from b2b_satuan  "
                                    SQLB2B = SQLB2B & "where Kode_Perusahaan = '" & Detail_KdPerusahaan & "' and Id_Satuan = '" & Detail_IdSatuan & "' "
                                    Using DrB2B = OpenTransB2B(SQLB2B)
                                        If DrB2B.Read Then
                                            Kode_Satuan = DrB2B("Kode_Satuan")
                                        Else
                                            CloseTrans()
                                            CloseTransB2B()
                                            CloseConn()
                                            CloseConnB2B()
                                            MessageBox.Show("Satuan Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using


                                    ' GET SATUAN KECIL BARANG
                                    Dim satuanBarang As String
                                    SQL = "select satuan from barang where "
                                    SQL = SQL & "kode_perusahaan = '" & Detail_KdPerusahaan & "' "
                                    SQL = SQL & "and kode_barang = '" & Detail_KdBarang & "' "
                                    'SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            satuanBarang = Dr("satuan")
                                        Else

                                            Dr.Close()
                                            CloseTrans()
                                            CloseTransB2B()
                                            CloseConn()
                                            CloseConnB2B()
                                            MessageBox.Show("Satuan " & Kode_Satuan & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using


                                    ' CONVERT HARGA KESATUAN KECIL
                                    Dim ConvertHarga As Double = 0
                                    SQL = "select dbo.Ubah_Satuan('" & Detail_KdPerusahaan & "','UANG','" & Detail_KdBarang & "',"
                                    SQL = SQL & "'" & Kode_Satuan & "', '" & satuanBarang & "',"
                                    SQL = SQL & "" & Detail_Harga & ") as Hasil "
                                    Using dr4 = OpenTrans(SQL)
                                        If dr4.Read Then
                                            If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                                If dr4("Hasil") = 0 Then
                                                    dr4.Close()
                                                    CloseTrans()
                                                    CloseTransB2B()
                                                    CloseConn()
                                                    CloseConnB2B()
                                                    MessageBox.Show("Satuan " & Kode_Satuan & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                Else
                                                    ConvertHarga = dr4("hasil")

                                                End If
                                            Else
                                                dr4.Close()
                                                CloseTrans()
                                                CloseTransB2B()
                                                CloseConn()
                                                CloseConnB2B()
                                                MessageBox.Show("Satuan " & Kode_Satuan & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End Using



                                    SQL = "insert into EMI_Master_Penawaran_Detail (kode_perusahaan, no_faktur, kode_barang, min_order, satuan, Harga_Satuan, Nilai_Barang, Satuan_Barang, Mata_Uang, urut_B2B) values ( "
                                    SQL = SQL & "'" & Detail_KdPerusahaan & "' , '" & Data_Faktur_Penawaran & "', '" & Detail_KdBarang & "', "
                                    SQL = SQL & "'" & Detail_MOQ & "', '" & Kode_Satuan & "', '" & Detail_Harga & "' , "
                                    SQL = SQL & "'" & ConvertHarga & "', '" & satuanBarang & "', '" & Detail_MataUang & "', '" & Detail_UrutB2B & "' ) "
                                    ExecuteTrans(SQL)


                                    'JIKA FLAG KATEGORI SUPPLIER = Y
                                    If flag_kategori_Supplier = "Y" Then

                                        'GET SATUAN KIRIM
                                        Dim satuan_kirim As String = ""
                                        SQL = "select satuan from barang_detail_satuan where kode_barang='" & Detail_KdBarang & "' "
                                        SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' and flag_kirim='Y' "
                                        Using dr3 = OpenTrans(SQL)
                                            If dr3.Read Then
                                                satuan_kirim = dr3("satuan")
                                            Else
                                                dr3.Close()
                                                CloseTrans()
                                                CloseTransB2B()
                                                CloseConn()
                                                CloseConnB2B()
                                                MessageBox.Show("data satuan kirim tidak ada ")
                                                Exit Sub
                                            End If
                                        End Using

                                        'GET NAMA BARANG
                                        Dim nama As String = ""
                                        SQL = "select top(1) nama from barang where kode_barang ='" & Detail_KdBarang & "' "
                                        Using Dr = OpenTrans(SQL)
                                            If Dr.Read Then
                                                nama = Dr("nama")
                                            End If
                                        End Using

                                        'INSERT KOMPOSISI BARANG JADI
                                        SQL = "select kode_Perusahaan from komposisi_barang_jadi where kode_barang='" & Detail_KdBarang & "' "
                                        SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' "
                                        Using dr33 = OpenTrans(SQL)
                                            If Not dr33.Read Then
                                                dr33.Close()
                                                SQL = "insert into komposisi_barang_jadi(kode_perusahaan, "
                                                SQL = SQL & "kode_barang, Qty) Values ("
                                                SQL = SQL & " '" & Detail_KdPerusahaan & "', '" & Detail_KdBarang & "', "
                                                SQL = SQL & "'1')"
                                                ExecuteTrans(SQL)

                                                SQL = "insert into detail_komposisi_barang_jadi(kode_perusahaan, "
                                                SQL = SQL & "kode_barang, Kode_Bahan, Qty_Bahan) Values("
                                                SQL = SQL & "'" & Detail_KdPerusahaan & "', '" & Detail_KdBarang & "', "
                                                SQL = SQL & "'" & Detail_KdBarang & "', '1')"
                                                ExecuteTrans(SQL)
                                            End If
                                        End Using

                                        'INSERT BAHAN IMPORT
                                        SQL = "select kode_Perusahaan from bahan_import where kode_bahan='" & Detail_KdBarang & "' "
                                        SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' "
                                        Using dr33 = OpenTrans(SQL)
                                            If Not dr33.Read Then
                                                dr33.Close()

                                                SQL = "select kode_stock_owner_import from stock_owner_import where kode_perusahaan = '" & Detail_KdPerusahaan & "' "
                                                SQL = SQL & "order by kode_stock_owner_import"
                                                Using Dsm = BindingTrans(SQL)
                                                    If Dsm.Tables("MyTable").Rows.Count <> 0 Then
                                                        For iii As Integer = 0 To Dsm.Tables("MyTable").Rows.Count - 1

                                                            SQL = "Insert Into bahan_import(Kode_Perusahaan, Kode_STock_Owner_Import, kode_bahan, Kode_supplier, "
                                                            SQL = SQL & "nama_bahan, kategori, mata_uang, harga, satuan, Flag_Potong_Stock) Values("
                                                            SQL = SQL & "'" & Detail_KdPerusahaan & "', '" & Dsm.Tables("MyTable").Rows(iii).Item("kode_stock_owner_import") & "', "
                                                            SQL = SQL & "'" & Detail_KdBarang & "','" & .Rows(i).Item("kode_supplier") & "',"
                                                            SQL = SQL & "'" & nama & "', '" & "Utama" & "', '" & Detail_MataUang & "', "
                                                            SQL = SQL & "'" & Val(HilangkanTanda(Detail_Harga)) & "','" & satuan_kirim & "', '" & "T" & "')"
                                                            ExecuteTrans(SQL)

                                                        Next
                                                    Else
                                                        CloseTrans()
                                                        CloseTransB2B()
                                                        CloseConn()
                                                        CloseConnB2B()
                                                        MessageBox.Show("Data lokasi import tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If

                                                End Using
                                            End If
                                        End Using

                                        'UPDATE emi_master_penawaran_detail 
                                        SQL = "update emi_master_penawaran_detail set "
                                        SQL = SQL & "flag_baru = 'Y' "
                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_barang='" & Detail_KdBarang & "' "
                                        SQL = SQL & "and no_faktur='" & .Rows(i).Item("No_Faktur") & "' and urut_B2B = '" & Detail_UrutB2B & "' "
                                        ExecuteTrans(SQL)

                                    End If

                                    SQLB2B = "Update B2B_Penawaran_Bahan_Baku_Detail set flag_sudah_pindah = 'Y' where "
                                    SQLB2B = SQLB2B & "Kode_Perusahaan = '" & Detail_KdPerusahaan & "'  "
                                    SQLB2B = SQLB2B & "and No_Faktur = '" & .Rows(i).Item("no_faktur") & "' "
                                    SQLB2B = SQLB2B & "and no_urut = '" & Detail_UrutB2B & "' "
                                    ExecuteTransB2B(SQLB2B)

                                Next
                            End Using

                            '==================================================================
                            '=     CEK APAKAH EMI_MASTER_PENAWARAN_DETAIL SUDAH DI INSERT     =
                            '==================================================================
                            SQL = "select top 1 * from EMI_Master_Penawaran_Detail where Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur = '" & Data_Faktur_Penawaran & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseTransB2B()
                                    CloseConn()
                                    CloseConnB2B()
                                    MessageBox.Show("Ada Masalah saat Insert Detail Penawaran", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            If hasInsert Then
                                'UPDATE FLAG RELEASE
                                SQL = "update EMI_Master_Penawaran set "
                                SQL = SQL & "flag_release = 'Y', "
                                SQL = SQL & "Tanggal_Release = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                                SQL = SQL & "jam_release = '" & Format(tgl_skg, "HH:mm:ss") & "' , "
                                SQL = SQL & "iduser_release = '" & UserID & "' "
                                SQL = SQL & "where kode_perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur='" & Faktur_Penawaran & "' "
                                SQL = SQL & "and no_penawaran='" & .Rows(i).Item("No_Penawaran") & "' "
                                ExecuteTrans(SQL)
                            End If


                        Next
                    End With
                End Using
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub btnPenawaranPackaging_Click(sender As Object, e As EventArgs) Handles btnPenawaranPackaging.Click
        get_jam()
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction



            arrNoPenawaranPackaging.Clear()
            SQLB2B = "select a.no_transaksi From b2b_packaging a , b2b_detail_packaging b "
            SQLB2B = SQLB2B & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_transaksi = b.no_transaksi "
            SQLB2B = SQLB2B & "and b.Flag_Approval = 'A' "
            SQLB2B = SQLB2B & "and a.Status is null  "
            SQLB2B = SQLB2B & "and b.Flag_Sudah_Pindah is null "
            SQLB2B = SQLB2B & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQLB2B = SQLB2B & "group by a.no_transaksi "
            Using DsB2B = BindingTransB2B(SQLB2B)
                With DsB2B.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrNoPenawaranPackaging.Add(.Rows(i).Item("no_transaksi"))

                    Next
                End With
            End Using

            For z As Integer = 0 To arrNoPenawaranPackaging.Count - 1

                get_no_faktur_penawaran()

                Dim flag_kategori_Supplier As String = ""
                Dim Data_Faktur_Penawaran As String = ""
                Dim arrDataDetailPenawaran As New ArrayList

                Dim hasInsert As Boolean = False

                SQLB2B = "select Kode_Perusahaan, No_Transaksi as no_faktur, No_Penawaran,Tanggal_Mulai as Tgl_Penawaran_Hrg, "
                SQLB2B = SQLB2B & "Tanggal_Selesai as Periode_Akhir_Penawaran,Kode_Supplier, Lokasi, Tanggal, Jam, Id_User "
                SQLB2B = SQLB2B & "from B2B_Packaging where kode_perusahaan = '" & KodePerusahaan & "' "
                SQLB2B = SQLB2B & "and no_transaksi = '" & arrNoPenawaranPackaging.Item(z).ToString & "' "
                Using DsB2B = BindingTransB2B(SQLB2B)
                    With DsB2B.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1

                            '==================================================
                            '=     CEK APAKAH ADA DATA DI PENAWARAN INDUK     =
                            '==================================================
                            SQL = "select Top 1 Kode_Perusahaan, No_Faktur from emi_master_penawaran where Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur_B2B = '" & .Rows(i).Item("no_faktur") & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Data_Faktur_Penawaran = Dr("No_Faktur")

                                Else
                                    Dr.Close()
                                    SQL = "insert into emi_master_penawaran(Kode_Perusahaan, No_Faktur, No_Faktur_B2B, No_Penawaran, Tgl_Penawaran_Hrg, Periode_Akhir_Penawaran, Kode_Supplier, Lokasi, "
                                    SQL = SQL & "Tanggal, Jam, iduser) values ( "
                                    SQL = SQL & "'" & .Rows(i).Item("kode_perusahaan") & "', '" & Faktur_Penawaran & "', '" & .Rows(i).Item("no_faktur") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("No_Penawaran") & "', '" & .Rows(i).Item("tgl_penawaran_hrg") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("periode_akhir_penawaran") & "', '" & .Rows(i).Item("kode_supplier") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("lokasi") & "', '" & .Rows(i).Item("tanggal") & "', '" & .Rows(i).Item("jam") & "', '" & .Rows(i).Item("Id_User") & "' )"
                                    ExecuteTrans(SQL)

                                    Data_Faktur_Penawaran = Faktur_Penawaran
                                End If
                            End Using

                            '===================
                            '=     RELEASE     =
                            '===================
                            'GET KATEGORI SUPPLIER
                            SQL = "select b.kode_supplier, b.ID_Kategori_Suppliers, c.flag_jenis_import  from  Suppliers b, Suppliers_Kategori c "
                            SQL = SQL & "where b.kode_perusahaan = c.kode_perusahaan and b.id_kategori_suppliers = c.id_kategori_suppliers "
                            SQL = SQL & "and b.kode_perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and b.Kode_Supplier = '" & .Rows(i).Item("kode_supplier") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    flag_kategori_Supplier = General_Class.CekNULL(Dr("flag_jenis_import"))
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseTransB2B()
                                    CloseConn()
                                    CloseConnB2B()
                                    MessageBox.Show("Supplier " & .Rows(i).Item("kode_supplier") & " Tidak Ditemukan...!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub

                                End If
                            End Using


                            'MASUK KE DETAIL PACKAGING
                            SQLB2B = "select  a.kode_perusahaan, a.No_Transaksi AS no_faktur, a.kode_barang, a.MOQ, a.satuan, a.Harga, b.kode_satuan, a.mata_uang, a.no_urut "
                            SQLB2B = SQLB2B & "from B2B_Detail_Packaging a, b2b_satuan b "
                            SQLB2B = SQLB2B & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQLB2B = SQLB2B & "and a.Satuan = b.Id_Satuan "
                            SQLB2B = SQLB2B & "and a.Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' "
                            SQLB2B = SQLB2B & "and a.No_Transaksi = '" & .Rows(i).Item("no_faktur") & "' "
                            SQLB2B = SQLB2B & "and a.Flag_Approval = 'A' "
                            SQLB2B = SQLB2B & "and a.Flag_Sudah_Pindah is null "
                            SQLB2B = SQLB2B & "order by a.Kode_Barang"
                            Using Ds2B2B = BindingTransB2B(SQLB2B)
                                For j As Integer = 0 To Ds2B2B.Tables("MyTable").Rows.Count - 1

                                    hasInsert = True

                                    Dim Detail_KdPerusahaan As String = Ds2B2B.Tables("MyTable").Rows(j).Item("kode_perusahaan")
                                    Dim Detail_KdBarang As String = Ds2B2B.Tables("MyTable").Rows(j).Item("kode_barang")
                                    Dim Detail_KodeSatuan As String = Ds2B2B.Tables("MyTable").Rows(j).Item("kode_satuan")
                                    Dim Detail_Harga As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Harga")
                                    Dim Detail_MOQ As String = Ds2B2B.Tables("MyTable").Rows(j).Item("MOQ")
                                    Dim Detail_MataUang As String = Ds2B2B.Tables("MyTable").Rows(j).Item("mata_uang")
                                    Dim Detail_IdSatuan As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Satuan")
                                    Dim Detail_UrutB2B As String = Ds2B2B.Tables("MyTable").Rows(j).Item("no_urut")

                                    '===================================
                                    '=     INSERT PENAWARAN DETAIL     =
                                    '===================================
                                    'GET KODE SATUAN
                                    Dim Kode_Satuan As String = ""
                                    SQLB2B = "select Kode_Satuan, Keterangan from b2b_satuan  "
                                    SQLB2B = SQLB2B & "where Kode_Perusahaan = '" & Detail_KdPerusahaan & "' and Id_Satuan = '" & Detail_IdSatuan & "' "
                                    Using DrB2B = OpenTransB2B(SQLB2B)
                                        If DrB2B.Read Then
                                            Kode_Satuan = DrB2B("Kode_Satuan")
                                        Else
                                            CloseTrans()
                                            CloseTransB2B()
                                            CloseConn()
                                            CloseConnB2B()
                                            MessageBox.Show("Satuan Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using


                                    ' GET SATUAN KECIL BARANG
                                    Dim satuanBarang As String
                                    SQL = "select satuan from barang where "
                                    SQL = SQL & "kode_perusahaan = '" & Detail_KdPerusahaan & "' "
                                    SQL = SQL & "and kode_barang = '" & Detail_KdBarang & "' "
                                    'SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            satuanBarang = Dr("satuan")
                                        Else

                                            Dr.Close()
                                            CloseTrans()
                                            CloseTransB2B()
                                            CloseConn()
                                            CloseConnB2B()
                                            MessageBox.Show("Satuan " & Kode_Satuan & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using


                                    ' CONVERT HARGA KESATUAN KECIL
                                    Dim ConvertHarga As Double = 0
                                    SQL = "select dbo.Ubah_Satuan('" & Detail_KdPerusahaan & "','UANG','" & Detail_KdBarang & "',"
                                    SQL = SQL & "'" & Kode_Satuan & "', '" & satuanBarang & "',"
                                    SQL = SQL & "" & Detail_Harga & ") as Hasil "
                                    Using dr4 = OpenTrans(SQL)
                                        If dr4.Read Then
                                            If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                                If dr4("Hasil") = 0 Then
                                                    dr4.Close()
                                                    CloseTrans()
                                                    CloseTransB2B()
                                                    CloseConn()
                                                    CloseConnB2B()
                                                    MessageBox.Show("Satuan " & Kode_Satuan & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                Else
                                                    ConvertHarga = dr4("hasil")

                                                End If
                                            Else
                                                dr4.Close()
                                                CloseTrans()
                                                CloseTransB2B()
                                                CloseConn()
                                                CloseConnB2B()
                                                MessageBox.Show("Satuan " & Kode_Satuan & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End Using

                                    SQL = "insert into EMI_Master_Penawaran_Detail (kode_perusahaan, no_faktur, kode_barang, min_order, satuan, Harga_Satuan, Nilai_Barang, Satuan_Barang, Mata_Uang, urut_B2B) values ( "
                                    SQL = SQL & "'" & Detail_KdPerusahaan & "' , '" & Data_Faktur_Penawaran & "', '" & Detail_KdBarang & "', "
                                    SQL = SQL & "'" & Detail_MOQ & "', '" & Kode_Satuan & "', '" & Detail_Harga & "' , "
                                    SQL = SQL & "'" & ConvertHarga & "', '" & satuanBarang & "', '" & Detail_MataUang & "', '" & Detail_UrutB2B & "' ) "
                                    ExecuteTrans(SQL)

                                    If flag_kategori_Supplier = "Y" Then

                                        'GET SATUAN KIRIM
                                        Dim satuan_kirim As String = ""
                                        SQL = "select satuan from barang_detail_satuan where kode_barang='" & Detail_KdBarang & "' "
                                        SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' and flag_kirim='Y' "
                                        Using dr3 = OpenTrans(SQL)
                                            If dr3.Read Then
                                                satuan_kirim = dr3("satuan")
                                            Else
                                                dr3.Close()
                                                CloseTrans()
                                                CloseTransB2B()
                                                CloseConn()
                                                CloseConnB2B()
                                                MessageBox.Show("data satuan kirim tidak ada ")
                                                Exit Sub
                                            End If
                                        End Using

                                        'GET NAMA BARANG
                                        Dim nama As String = ""
                                        SQL = "select top(1) nama from barang where kode_barang ='" & Detail_KdBarang & "' "
                                        Using Dr = OpenTrans(SQL)
                                            If Dr.Read Then
                                                nama = Dr("nama")
                                            End If
                                        End Using

                                        'INSERT KOMPOSISI BARANG JADI
                                        SQL = "select kode_Perusahaan from komposisi_barang_jadi where kode_barang='" & Detail_KdBarang & "' "
                                        SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' "
                                        Using dr33 = OpenTrans(SQL)
                                            If Not dr33.Read Then
                                                dr33.Close()
                                                SQL = "insert into komposisi_barang_jadi(kode_perusahaan, "
                                                SQL = SQL & "kode_barang, Qty) Values ("
                                                SQL = SQL & " '" & Detail_KdPerusahaan & "', '" & Detail_KdBarang & "', "
                                                SQL = SQL & "'1')"
                                                ExecuteTrans(SQL)

                                                SQL = "insert into detail_komposisi_barang_jadi(kode_perusahaan, "
                                                SQL = SQL & "kode_barang, Kode_Bahan, Qty_Bahan) Values("
                                                SQL = SQL & "'" & Detail_KdPerusahaan & "', '" & Detail_KdBarang & "', "
                                                SQL = SQL & "'" & Detail_KdBarang & "', '1')"
                                                ExecuteTrans(SQL)
                                            End If
                                        End Using

                                        'INSERT BAHAN IMPORT
                                        SQL = "select kode_Perusahaan from bahan_import where kode_bahan='" & Detail_KdBarang & "' "
                                        SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' "
                                        Using dr33 = OpenTrans(SQL)
                                            If Not dr33.Read Then
                                                dr33.Close()

                                                SQL = "select kode_stock_owner_import from stock_owner_import where kode_perusahaan = '" & Detail_KdPerusahaan & "' "
                                                SQL = SQL & "order by kode_stock_owner_import"
                                                Using Dsm = BindingTrans(SQL)
                                                    If Dsm.Tables("MyTable").Rows.Count <> 0 Then
                                                        For iii As Integer = 0 To Dsm.Tables("MyTable").Rows.Count - 1

                                                            SQL = "Insert Into bahan_import(Kode_Perusahaan, Kode_STock_Owner_Import, kode_bahan, Kode_supplier, "
                                                            SQL = SQL & "nama_bahan, kategori, mata_uang, harga, satuan, Flag_Potong_Stock) Values("
                                                            SQL = SQL & "'" & Detail_KdPerusahaan & "', '" & Dsm.Tables("MyTable").Rows(iii).Item("kode_stock_owner_import") & "', "
                                                            SQL = SQL & "'" & Detail_KdBarang & "','" & .Rows(i).Item("kode_supplier") & "',"
                                                            SQL = SQL & "'" & nama & "', '" & "Utama" & "', '" & Detail_MataUang & "', "
                                                            SQL = SQL & "'" & Val(HilangkanTanda(Detail_Harga)) & "','" & satuan_kirim & "', '" & "T" & "')"
                                                            ExecuteTrans(SQL)

                                                        Next
                                                    Else
                                                        CloseTrans()
                                                        CloseTransB2B()
                                                        CloseConn()
                                                        CloseConnB2B()
                                                        MessageBox.Show("Data lokasi import tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If

                                                End Using
                                            End If
                                        End Using

                                        'UPDATE emi_master_penawaran_detail 
                                        SQL = "update emi_master_penawaran_detail set "
                                        SQL = SQL & "flag_baru = 'Y' "
                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_barang='" & Detail_KdBarang & "' "
                                        SQL = SQL & "and no_faktur='" & .Rows(i).Item("No_Faktur") & "' "
                                        ExecuteTrans(SQL)


                                    End If

                                    SQLB2B = "Update B2B_Detail_Packaging set flag_sudah_pindah = 'Y' where "
                                    SQLB2B = SQLB2B & "Kode_Perusahaan = '" & Detail_KdPerusahaan & "' "
                                    SQLB2B = SQLB2B & "and no_transaksi = '" & .Rows(i).Item("no_faktur") & "' "
                                    SQLB2B = SQLB2B & "and no_urut = '" & Detail_UrutB2B & "' "
                                    ExecuteTransB2B(SQLB2B)

                                Next

                            End Using

                            '==================================================================
                            '=     CEK APAKAH EMI_MASTER_PENAWARAN_DETAIL SUDAH DI INSERT     =
                            '==================================================================
                            SQL = "select top 1 * from EMI_Master_Penawaran_Detail where Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur = '" & Data_Faktur_Penawaran & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseTransB2B()
                                    CloseConn()
                                    CloseConnB2B()
                                    MessageBox.Show("Ada Masalah saat Insert Detail Penawaran", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            If hasInsert Then
                                'UPDATE FLAG RELEASE
                                SQL = "update EMI_Master_Penawaran set "
                                SQL = SQL & "flag_release = 'Y', "
                                SQL = SQL & "Tanggal_Release = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                                SQL = SQL & "jam_release = '" & Format(tgl_skg, "HH:mm:ss") & "' , "
                                SQL = SQL & "iduser_release = '" & UserID & "' "
                                SQL = SQL & "where kode_perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur='" & Data_Faktur_Penawaran & "' "
                                SQL = SQL & "and no_penawaran='" & .Rows(i).Item("No_Penawaran") & "' "
                                ExecuteTrans(SQL)
                            End If





                        Next
                    End With
                End Using
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & " Insert Penawaran Packaging")
            Exit Sub
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnSupplierInsert.Click
        get_jam()
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrKodeSupplier.Clear()
            SQL = "select kode_supplier from suppliers where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "flag_sudah_pindah is null"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrKodeSupplier.Add(.Rows(i).Item("kode_supplier"))

                    Next
                End With
            End Using

            Dim j As Integer = 0
            For z As Integer = 1 To arrKodeSupplier.Count

                SQL = "select Kode_Perusahaan,Kode_Supplier,Nama,Alamat,Pemilik,Telepon,Fax,Contact_Person,HP_CP,Hutang,Kode_Kategori,Inisial_Sup	,Tampil_Di_PO, "
                SQL = SQL & "Negara,Kota,Port,Kategori_Import,Nama_Supplier,PIC,Mata_Uang_Rek,Mata_Uang_Declare,Mata_Uang_Bayar,Format_Faktur,Flag_Average	,Flag_Validasi_Declare, "
                SQL = SQL & "Perhitungan_Jatuh_Tempo,Ket_Perhitungan_Jatuh_Tempo,Flag_Form_E,	Biaya_Form_E,	Jenis_Laporan_Import,	Metode_Selisih_Declare, "
                SQL = SQL & "flag_gabung_declare,ID_Kategori_Suppliers	 "
                SQL = SQL & "from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "flag_sudah_pindah is null and "
                SQL = SQL & "kode_supplier = '" & arrKodeSupplier.Item(j).ToString & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1
                            'Insert ke MySql
                            SQLB2B = "insert into suppliers (Kode_Perusahaan,Kode_Supplier,Nama,Alamat,Pemilik,Telepon,Fax,Contact_Person,HP_CP,Hutang,Kode_Kategori,Inisial_Sup	,Tampil_Di_PO,  "
                            SQLB2B = SQLB2B & "Negara,Kota,Port,Kategori_Import,Nama_Supplier,PIC,Mata_Uang_Rek,Mata_Uang_Declare,Mata_Uang_Bayar,Format_Faktur,Flag_Average	,Flag_Validasi_Declare,"
                            SQLB2B = SQLB2B & "Perhitungan_Jatuh_Tempo,Ket_Perhitungan_Jatuh_Tempo,Flag_Form_E,	Biaya_Form_E,	Jenis_Laporan_Import,	Metode_Selisih_Declare,"
                            SQLB2B = SQLB2B & "flag_gabung_declare,ID_Kategori_Suppliers "
                            SQLB2B = SQLB2B & ")"
                            SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("Kode_Supplier") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Nama") & "', '" & .Rows(i).Item("Alamat") & "', "
                            SQLB2B = SQLB2B & " '" & .Rows(i).Item("Pemilik") & "', '" & .Rows(i).Item("Telepon") & "', '" & .Rows(i).Item("Fax") & "' , '" & .Rows(i).Item("Contact_Person") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("HP_CP") & "' , '" & .Rows(i).Item("Hutang") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kode_Kategori") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Inisial_Sup") & "','" & .Rows(i).Item("Tampil_Di_PO") & "', '" & .Rows(i).Item("Negara") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kota") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Port") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kategori_Import") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Nama_Supplier") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("PIC") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Mata_Uang_Rek") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Mata_Uang_Declare") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Mata_Uang_Bayar") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Format_Faktur") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Flag_Average") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Flag_Validasi_Declare") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Perhitungan_Jatuh_Tempo") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Ket_Perhitungan_Jatuh_Tempo") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Flag_Form_E") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Biaya_Form_E") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Jenis_Laporan_Import") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("Metode_Selisih_Declare") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("flag_gabung_declare") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("ID_Kategori_Suppliers") & "' "
                            SQLB2B = SQLB2B & ")"
                            ExecuteTransB2B(SQLB2B)

                        Next
                    End With
                End Using

                SQL = "Update suppliers set flag_sudah_pindah = 'Y' where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
                SQL = SQL & "kode_supplier = '" & arrKodeSupplier.Item(j).ToString & "' "
                ExecuteTrans(SQL)

                j = j + 1
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & " Insert Suppliers")
            Exit Sub
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        get_jam()
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrNoPo2.Clear()
            SQL = "select no_faktur from EMI_Pembelian_Loading where flag_sdh_update ='Y'   order by no_faktur"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrNoPo2.Add(.Rows(i).Item("no_faktur"))
                    Next
                End With
            End Using

            Dim j As Integer = 0
            For z As Integer = 1 To arrNoPo2.Count

                '================================================
                ' update tanggal masuk b2b_purchase_order
                '================================================

                SQL = "select No_Faktur,tanggal_masuk, flag_sudah_bongkar_android from EMI_Pembelian_Loading where Status is null and Flag_Sdh_Update = 'Y'  "
                SQL = SQL & "and no_faktur = '" & arrNoPo2.Item(j).ToString() & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim flagSelesai As String = ""
                            If General_Class.CekNULL(.Rows(i).Item("flag_sudah_bongkar_android")) = "" Then
                                flagSelesai = "NULL"
                            Else
                                flagSelesai = "'" & .Rows(i).Item("flag_sudah_bongkar_android").ToString() & "'"
                            End If
                            SQLB2B = "update B2B_Purchase_Order set "
                            SQLB2B = SQLB2B & "flag_sampai = " & flagSelesai & ", "
                            SQLB2B = SQLB2B & "actual_ta = '" & .Rows(i).Item("tanggal_masuk") & "' "
                            SQLB2B = SQLB2B & "where kode_perusahaan = '" & KodePerusahaan & "'  "
                            SQLB2B = SQLB2B & "and no_faktur = '" & .Rows(i).Item("no_faktur") & "' "
                            ExecuteTransB2B(SQLB2B)

                        Next
                    End With
                End Using

                '================================================
                ' update Jumlah masuk b2b_detail_batch_purchase_order
                '================================================

                SQL = "select a.No_Faktur,a.no_urut_b2b, "
                SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',a.Kode_Barang,a.Satuan_Barang,b.Satuan,a.Jumlah_Masuk) as Jumlah_Masuk,b.Satuan "
                SQL = SQL & "from EMI_Pembelian_Loading_Detail a, Barang_Detail_Satuan b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_barang "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & arrNoPo2.Item(j).ToString() & "'  and b.Flag_Tampil_Display = 'Y'"

                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1

                            SQLB2B = "update B2B_detail_batch_Purchase_Order set "
                            SQLB2B = SQLB2B & "jumlah_masuk = '" & .Rows(i).Item("Jumlah_Masuk") & "' "
                            SQLB2B = SQLB2B & "where urut_oto = '" & .Rows(i).Item("no_urut_b2b") & "' "
                            ExecuteTransB2B(SQLB2B)
                        Next
                    End With
                End Using

                SQL = "Update EMI_Pembelian_Loading set flag_sdh_update = null where "
                SQL = SQL & "no_faktur = '" & arrNoPo2.Item(j).ToString & "' "
                ExecuteTrans(SQL)

                j = j + 1
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & " update purchase order")
            Exit Sub
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        'Button1_Click(Timer1, e)
        'Button2_Click(Timer1, e)
        'Button3_Click(Timer1, e)
        'Button4_Click(Timer1, e)
        'Button5_Click(Timer1, e)
        'Button6_Click(Timer1, e)
        'Button7_Click(Timer1, e)
        'Button8_Click(Timer1, e)
        'Button9_Click(Timer1, e)
        'Button10_Click(Timer1, e)
        'Button11_Click(Timer1, e)
        'Button12_Click(Timer1, e)
        'Button13_Click(Timer1, e)
        'Button14_Click(Timer1, e)
        'Button15_Click(Timer1, e)
        'Button16_Click(Timer1, e)
        'Button17_Click(Timer1, e)
        'Button18_Click(Timer1, e)
        'Button19_Click(Timer1, e)
        'Button20_Click(Timer1, e)
        'Button21_Click(Timer1, e)
        'btn_KeBrgMskMYSQL_Click(Timer1, e)

        Button22_Click(Timer1, e)
        Button23_Click(Timer1, e)
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click

        Dim listINSERT, listEDIT, listDELETE As New ArrayList

        Try

            OpenConnB2B()

            'INSERT
            SQLB2B = "select id, 'proyek' as dari from proyeks where flag_sdh_pindah_ke_sql is null "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "Select id, 'sub_proyek' as dari from subproyeks where flag_sdh_pindah_ke_sql is null  "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select id, 'pekerjaan' as dari from pekerjaans where flag_sdh_pindah_ke_sql is null  "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select id, 'sub_pekerjaan' as dari from sub_pekerjaans where flag_sdh_pindah_ke_sql is null  "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select no_faktur, 'request_material' as dari from request_material where flag_sdh_pindah_ke_sql is null and flag_acc = 'Y' limit 0, 1 "
            Using DsSQL = BindingTransB2B(SQLB2B)
                With DsSQL.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i).Item("dari") = "proyek" Then
                            'Button1_Click(Button22, e)
                            listINSERT.Add("proyek")
                        ElseIf .Rows(i).Item("dari") = "sub_proyek" Then
                            'Button4_Click(Button22, e)
                            listINSERT.Add("sub_proyek")
                        ElseIf .Rows(i).Item("dari") = "pekerjaan" Then
                            'Button5_Click(Button22, e)
                            listINSERT.Add("pekerjaan")
                        ElseIf .Rows(i).Item("dari") = "sub_pekerjaan" Then
                            'Button6_Click(Button22, e)
                            listINSERT.Add("sub_pekerjaan")
                        ElseIf .Rows(i).Item("dari") = "request_material" Then
                            'Button7_Click(Button22, e)
                            listINSERT.Add("request_material")
                        End If
                    Next
                End With
            End Using

            'EDIT
            SQLB2B = "select id, 'proyek' as dari from proyeks where "
            SQLB2B = SQLB2B & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_proyeks is null  "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select id, 'sub_proyek' as dari from subproyeks where "
            SQLB2B = SQLB2B & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_subproyeks is null  "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select id, 'pekerjaan' as dari from pekerjaans where "
            SQLB2B = SQLB2B & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_pekerjaans is null  "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select id, 'sub_pekerjaan' as dari from sub_pekerjaans where "
            SQLB2B = SQLB2B & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_subpekerjaans is null  "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select 1 as id, 'rab_pembelian_proyek' as dari from rab_pembelian_proyek where "
            SQLB2B = SQLB2B & "Kode_Perusahaan = '" & KodePerusahaan & "' and flag_edit_rab = 'Y'  "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select 1 as id, 'po_pembelian_proyek' as dari from po_pembelian_proyek where "
            SQLB2B = SQLB2B & "Kode_Perusahaan = '" & KodePerusahaan & "' and flag_edit_po = 'Y' "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select 1 as id, 'penjualan_proyek_sementara' as dari from penjualan_proyek_sementara where "
            SQLB2B = SQLB2B & " flag_sdh_update = 'Y' "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select 1 as id, 'approval_penjualan_proyek' as dari from approval_penjualan_proyek where  flag_WA is null "

            'SQLB2B = SQLB2B & " limit 0, 1 "
            Using DsSQL = BindingTransB2B(SQLB2B)
                With DsSQL.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i).Item("dari") = "proyek" Then
                            'Button14_Click(Button22, e)
                            listEDIT.Add("proyek")
                        ElseIf .Rows(i).Item("dari") = "sub_proyek" Then
                            'Button15_Click(Button22, e)
                            listEDIT.Add("sub_proyek")
                        ElseIf .Rows(i).Item("dari") = "pekerjaan" Then
                            'Button17_Click(Button22, e)
                            listEDIT.Add("pekerjaan")
                        ElseIf .Rows(i).Item("dari") = "sub_pekerjaan" Then
                            'Button16_Click(Button22, e)
                            listEDIT.Add("sub_pekerjaan")
                        ElseIf .Rows(i).Item("dari") = "rab_pembelian_proyek" Then
                            'Button12_Click(Button22, e)
                            listEDIT.Add("rab_pembelian_proyek")
                        ElseIf .Rows(i).Item("dari") = "po_pembelian_proyek" Then
                            'Button13_Click(Button22, e)
                            listEDIT.Add("po_pembelian_proyek")
                        ElseIf .Rows(i).Item("dari") = "penjualan_proyek_sementara" Then
                            listEDIT.Add("penjualan_proyek_sementara")
                        ElseIf .Rows(i).Item("dari") = "approval_penjualan_proyek" Then
                            listEDIT.Add("approval_penjualan_proyek")
                        End If
                    Next
                End With
            End Using

            'DELETE
            SQLB2B = "select id, 'proyek' as dari from proyeks where "
            SQLB2B = SQLB2B & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select id, 'sub_proyek' as dari from subproyeks where "
            SQLB2B = SQLB2B & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select id, 'pekerjaan' as dari from pekerjaans where "
            SQLB2B = SQLB2B & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
            SQLB2B = SQLB2B & "union all "
            SQLB2B = SQLB2B & "select id, 'sub_pekerjaan' as dari from sub_pekerjaans where "
            SQLB2B = SQLB2B & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
            Using DsSQL = BindingTransB2B(SQLB2B)
                With DsSQL.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i).Item("dari") = "proyek" Then
                            'Button1_Click(Button22, e)
                            listDELETE.Add("proyek")
                        ElseIf .Rows(i).Item("dari") = "sub_proyek" Then
                            'Button4_Click(Button22, e)
                            listDELETE.Add("sub_proyek")
                        ElseIf .Rows(i).Item("dari") = "pekerjaan" Then
                            'Button5_Click(Button22, e)
                            listDELETE.Add("pekerjaan")
                        ElseIf .Rows(i).Item("dari") = "sub_pekerjaan" Then
                            'Button6_Click(Button22, e)
                            listDELETE.Add("sub_pekerjaan")
                        End If
                    Next
                End With
            End Using

            CloseConnB2B()
        Catch ex As Exception
            CloseTransB2B()
            CloseConnB2B()

            Dim Lvw As ListViewItem
            Lvw = ListView1.Items.Add("MySql-Sql : " & ex.Message)
            'MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'Execute INSERT
        For index As Integer = 0 To listINSERT.Count - 1
            Dim item As Object = listINSERT(index)
            If item = "proyek" Then
                Button1_Click(Button22, e)
                'btnPnwrBahanBaku_Click(Button22, e)
                'btnPenawaranPackaging_Click(Button22, e)

            End If
        Next

        'Execute EDIT
        For index As Integer = 0 To listEDIT.Count - 1
            Dim item As Object = listEDIT(index)
            If item = "proyek" Then

            ElseIf item = "sub_proyek" Then

            ElseIf item = "pekerjaan" Then

            ElseIf item = "sub_pekerjaan" Then

            ElseIf item = "rab_pembelian_proyek" Then

            ElseIf item = "po_pembelian_proyek" Then

            ElseIf item = "penjualan_proyek_sementara" Then

            ElseIf item = "approval_penjualan_proyek" Then

            End If
        Next

    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click

        Dim listINSERT, listEDIT As New ArrayList

        Try
            OpenConn()

            'INSERT
            SQL = "select top(1) kode_barang, 'barang' as dari from barang_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
            SQL = SQL & "union all "
            SQL = SQL & "Select top(1) no_faktur, 'rab_pembelian_proyek' as dari from rab_pembelian_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
            SQL = SQL & "union all "
            SQL = SQL & "select top(1) kode_supplier, 'supplier' as dari from suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
            SQL = SQL & "union all "
            SQL = SQL & "select top(1) no_faktur, 'barang_masuk' as dari from barang_masuk_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
            SQL = SQL & "union all "
            SQL = SQL & "select top(1) no_faktur, 'po_pembelian_proyek' as dari from po_pembelian_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
            SQL = SQL & "union all "
            SQL = SQL & "select top(1) no_faktur, 'penjualan_proyek' as dari from penjualan_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
            SQL = SQL & "union all "
            SQL = SQL & "select top(1) no_faktur, 'penjualan_proyek_sementara' as dari from penjualan_proyek_sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
            SQL = SQL & "union all "
            SQL = SQL & "select top(1) no_val, 'val_pemb_proyek' as dari from val_pemb_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i).Item("dari") = "barang" Then
                            listINSERT.Add("barang")
                        ElseIf .Rows(i).Item("dari") = "rab_pembelian_proyek" Then
                            listINSERT.Add("rab_pembelian_proyek")
                        ElseIf .Rows(i).Item("dari") = "supplier" Then
                            listINSERT.Add("supplier")
                        ElseIf .Rows(i).Item("dari") = "barang_masuk" Then
                            listINSERT.Add("barang_masuk")
                        ElseIf .Rows(i).Item("dari") = "po_pembelian_proyek" Then
                            listINSERT.Add("po_pembelian_proyek")
                        ElseIf .Rows(i).Item("dari") = "penjualan_proyek" Then
                            listINSERT.Add("penjualan_proyek")
                        ElseIf .Rows(i).Item("dari") = "penjualan_proyek_sementara" Then
                            listINSERT.Add("penjualan_proyek_sementara")
                        ElseIf .Rows(i).Item("dari") = "val_pemb_proyek" Then
                            listINSERT.Add("val_pemb_proyek")
                        End If
                    Next
                End With
            End Using

            'EDIT
            SQL = "select top(1) no_faktur, 'rab_pembelian_proyek' as dari from rab_pembelian_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah = 'Y' and flag_edit = 'Y' "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i).Item("dari") = "rab_pembelian_proyek" Then
                            listEDIT.Add("rab_pembelian_proyek")

                        End If
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()

            Dim Lvw As ListViewItem
            Lvw = ListView1.Items.Add("Sql-MySql : " & ex.Message)

            'MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'Execute INSERT
        For index As Integer = 0 To listINSERT.Count - 1
            Dim item As Object = listINSERT(index)
            If item = "barang" Then

            ElseIf item = "rab_pembelian_proyek" Then

            ElseIf item = "supplier" Then

            ElseIf item = "barang_masuk" Then

            ElseIf item = "po_pembelian_proyek" Then

            ElseIf item = "penjualan_proyek" Then

            ElseIf item = "penjualan_proyek_sementara" Then
                Button24_Click(Button22, e)

            ElseIf item = "val_pemb_proyek" Then
                Button27_Click(Button22, e)

            End If
        Next

        'Execute EDIT
        For index As Integer = 0 To listEDIT.Count - 1
            Dim item As Object = listEDIT(index)
            If item = "rab_pembelian_proyek" Then

            End If
        Next

    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrNo_Fak.Clear()
            SQL = "select no_faktur from penjualan_proyek_sementara where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and flag_sdh_pindah is null and status is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrNo_Fak.Add(.Rows(i).Item("no_faktur"))
                    Next
                End With
            End Using

            SQL = "select Kode_Perusahaan,No_Faktur,Tanggal,Jam,Kode_Customer,Jenis_Transaksi,Tgl_Jatuh_Tempo, "
            SQL = SQL & "Flag_Lunas,Tgl_Lunas,Jam_Lunas,Flag_Lunas_Tunai,Tgl_Lunas_Tunai,Jam_Lunas_Tunai,UserValidasi_Tunai,UserID,UserValidasi, "
            SQL = SQL & "Disc1,Disc2,Disc_Cash,Status,Terbilang,Grand,Bayar,Kode_Sales,PPN,Pembeda,Total_Point, "
            SQL = SQL & "Flag_Lipat,Kurs,Pakai_Point,Diskon_Promo,Diskon_Rupiah,Flag_Tagihan, "
            SQL = SQL & "Kode_CB,RV,No_Surat_Jalan,Lokasi,Lokasi_Gdg,Total,Total_U_Dis_Member, "
            SQL = SQL & "Hasil_Diskon,Hasil_Diskon_Cash,Kode_Voucher_1,Kode_Voucher_2,Kode_Voucher_3,Kode_Voucher_4,Kode_Voucher_5,Kode_Voucher_6,Kode_Voucher_7,Kode_Voucher_8, "
            SQL = SQL & "Jenis,Nilai_PPN,No_PO_Toko,No_DO,Kode_Karyawan,Flag_Cabang_Sendiri,Flag_Cabang_Agency,COA_Piutang,Sudah_FK,No_Fak_Sebelumnya,No_Fak_Setelahnya, "
            SQL = SQL & "Init_Custm,Ket_Custm,No_KB,Jns,Akun_Kas,Akun_Piutang,Akun_Piutang_Sementara,Disc_Tambahan,Hasil_Disc_Tambahan,Harus_Retur_Semua, "
            SQL = SQL & "Persen_Insentif_1,Persen_Insentif_2,Nilai_Insentif_1,Nilai_Insentif_2,Akun_Biaya_Insentif_1,Akun_Biaya_Insentif_2,Akun_Hutang_Insentif_1,Akun_Hutang_Insentif_2, "
            SQL = SQL & "Kepala,Sudah_Voucher,Tanggal_Voucher,Jam_Voucher,User_Voucher,Sudah_Upload,Sudah_Kirim,Flag_DO_Selesai, "
            SQL = SQL & "No_Faktur_Pajak,Tgl_Faktur_Pajak,Jam_Faktur_Pajak,User_Faktur_Pajak,Subtotal_Faktur_Pajak,PPN_Faktur_Pajak,Grand_Faktur_Pajak, "
            SQL = SQL & "Val_Diskon_Cash,Tgl_Jurnal_Diskon_Cash,Tgl_Val_Diskon_Cash,Jam_Val_Diskon_Cash,User_Val_Diskon_Cash,Nilai_Val_Diskon_Cash,PPN_Val_Diskon_Cash,Ket_Val_Diskon_Cash, "
            SQL = SQL & "CB_Val_Diskon_Cash,Akun_Val_Diskon_Cash,Kode_Voucher_Diskon_Cash,zzz,z1,Diskon_Sementara,Nilai_Diskon_Sementara,Harus_Diupdate,Lama_Diskon_Sementara, "
            SQL = SQL & "Metode_Pot_Stock,Flag_Opm,Flag_Sudah_Ke_Pusat,Flag_Sementara_Saat_Opm,Flag_ACC_Plafon,User_ACC_Plafon,Harus_Updtttt,Hrs_Updatex,Nfpx,Total_Stlh_Diskon, "
            SQL = SQL & "metode_budgeting,Mulai_Deskcall,tgl_input,Flag_Lunas_tes,Flag_Lunas_Tunai_tes, "
            SQL = SQL & "Kode_Sub_Pekerjaan,Id_Sub_Pekerjaan,Flag_Sdh_Pindah,Keterangan,Kode_Unik,Flag_Pindah,Flag_ACC,Tanggal_ACC,Jam_ACC "
            SQL = SQL & "from penjualan_proyek_sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null and status is null "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        SQLB2B = "insert into penjualan_proyek_sementara (kode_perusahaan,no_faktur,tanggal,jam, "
                        SQLB2B = SQLB2B & "userid,disc_cash,lokasi,hasil_diskon_cash, "
                        SQLB2B = SQLB2B & "kode_voucher_2,flag_cabang_agency,disc_tambahan,hasil_disc_tambahan, "
                        SQLB2B = SQLB2B & "persen_insentif_1,persen_insentif_2,nilai_insentif_1,nilai_insentif_2, "
                        SQLB2B = SQLB2B & "nilai_val_diskon_cash,diskon_sementara,nilai_diskon_sementara,lama_diskon_sementara, "
                        SQLB2B = SQLB2B & "metode_pot_stock,tgl_input,sub_pekerjaan_id, keterangan)"
                        SQLB2B = SQLB2B & "values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam") & "', "
                        SQLB2B = SQLB2B & "'" & .Rows(i).Item("userid") & "', '" & .Rows(i).Item("disc_cash") & "', '" & .Rows(i).Item("lokasi") & "', '" & .Rows(i).Item("hasil_diskon_cash") & "', "
                        SQLB2B = SQLB2B & "'" & .Rows(i).Item("kode_voucher_2") & "', '" & .Rows(i).Item("flag_cabang_agency") & "', '" & .Rows(i).Item("disc_tambahan") & "', '" & .Rows(i).Item("hasil_disc_tambahan") & "', "
                        SQLB2B = SQLB2B & "'" & .Rows(i).Item("persen_insentif_1") & "', '" & .Rows(i).Item("persen_insentif_2") & "', '" & .Rows(i).Item("nilai_insentif_1") & "', '" & .Rows(i).Item("nilai_insentif_2") & "', "
                        SQLB2B = SQLB2B & "'" & .Rows(i).Item("nilai_val_diskon_cash") & "', '" & .Rows(i).Item("diskon_sementara") & "', '" & .Rows(i).Item("nilai_diskon_sementara") & "', '" & .Rows(i).Item("lama_diskon_sementara") & "', "
                        SQLB2B = SQLB2B & "'" & .Rows(i).Item("metode_pot_stock") & "', '" & Format(.Rows(i).Item("tgl_input"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("id_sub_pekerjaan") & "', '" & .Rows(i).Item("keterangan") & "') "
                        ExecuteTransB2B(SQLB2B)

                        '----Insert Approval ('RandomPIN 6 Digit,'RandomKodeUnik, RandomKodeUrl 10 Digit)

                        SQLB2B = "select id from users where flag_acc_pengeluaran ='Y'"
                        Using DsSQL = BindingTransB2B(SQLB2B)
                            For indexxx As Integer = 0 To DsSQL.Tables("MyTable").Rows.Count - 1

                                Dim randomNumber As New Random()
                                Dim randomPIN As Integer = randomNumber.Next(100000, 1000000)
                                Dim randomKdUnik As Integer = randomNumber.Next(1000000000, Integer.MaxValue)
                                Dim randomKdUrl As Integer = randomNumber.Next(1000000000, Integer.MaxValue)

                                SQLB2B = "insert into approval_penjualan_proyek (penjualan_proyek_id, pin, kode_unik, kode_url, user_id) "
                                SQLB2B = SQLB2B & "values ('" & .Rows(i).Item("no_faktur") & "', '" & randomPIN.ToString & "', '" & randomKdUnik.ToString & "', '" & randomKdUrl.ToString & "','" & DsSQL.Tables("MyTable").Rows(indexxx).Item("id") & "')"
                                ExecuteTransB2B(SQLB2B)

                            Next
                        End Using

                    Next
                End With
            End Using

            Dim j As Integer = 0
            For z As Integer = 1 To arrNo_Fak.Count
                SQL = "select Kode_Perusahaan,No_Faktur,No_Urut,Kode_Stock_Owner,Kode_Barang,Serial_Number,"
                SQL = SQL & "Keterangan,Jumlah,Harga,Persen_Diskon,Nilai_Diskon,x,kett,"
                SQL = SQL & "Harga_Min,Usermin,Pakai_SN,Barang_Hadiah,Modal,Nota_Kecil,"
                SQL = SQL & "Kode_Marketing,Subtotal,Flag_Sdr,Kode_Paket,Flag_Budgeting,Flag_Budgeting_Mbl,Flag_Budgeting_2,"
                SQL = SQL & "Harga_Terendah,Harga_Agen,Jml_Retur_Di_Pjk,Jml_Retur_Lain_Di_Pjk,Jml_Buat_Di_Pjk,Subtotal_Di_Pjk,"
                SQL = SQL & "Kode_Paket_2,Flag_Budgeting_3,Flag_Budgeting_4,Metode_Perhitungan,mdl,rvz,Flag_budgeting_new,Isi_Satuan_Besar "
                SQL = SQL & "from detail_penjualan_proyek_sementara "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "'"

                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")

                        For i As Integer = 0 To .Rows.Count - 1

                            SQLB2B = "insert into detail_penjualan_proyek_sementara (kode_perusahaan,no_faktur,no_urut,kode_stock_owner,kode_barang,jumlah) "
                            SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_urut") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("jumlah") & "') "
                            ExecuteTransB2B(SQLB2B)

                        Next
                    End With
                End Using

                '------------------------------------------------------

                SQL = "select a.Kode_Perusahaan, a.No_faktur, a.no_Urut, a.Kode_stock_owner, a.Kode_barang, a.serial_number, "
                SQL = SQL & "a.jumlah, a.id_proyek, a.id_subproyek, a.urut_oto, "
                SQL = SQL & "ISNULL((select keterangan from web_proyeks x where x.id = a.Id_Proyek "
                SQL = SQL & "),'-') as Proyek, "
                SQL = SQL & "ISNULL((select keterangan from web_subproyeks x where x.id = a.Id_Subproyek "
                SQL = SQL & "),'-') as SubProyek "
                SQL = SQL & "from det_penj_proyek_sementara a "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")

                        For i As Integer = 0 To .Rows.Count - 1

                            SQLB2B = "insert into det_penj_proyek_sementara (kode_perusahaan, no_faktur, no_urut, kode_stock_owner, kode_barang, serial_number, "
                            SQLB2B = SQLB2B & "jumlah, proyek_id, sub_proyek_id, urut_oto,proyek,subproyek) "
                            SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "',"
                            SQLB2B = SQLB2B & " '" & .Rows(i).Item("no_urut") & "', '" & .Rows(i).Item("kode_stock_owner") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("serial_number") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("jumlah") & "', "
                            'Id Proyek
                            If General_Class.CekNULL((.Rows(i).Item("id_proyek"))) = "" Then
                                SQLB2B = SQLB2B & "NULL, "
                            Else
                                SQLB2B = SQLB2B & "'" & .Rows(i).Item("id_proyek") & "', "
                            End If
                            'Id SubProyek
                            If General_Class.CekNULL((.Rows(i).Item("id_subproyek"))) = "" Then
                                SQLB2B = SQLB2B & "NULL, "
                            Else
                                SQLB2B = SQLB2B & "'" & .Rows(i).Item("id_subproyek") & "', "
                            End If
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("urut_oto") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("proyek") & "', "
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("subproyek") & "')"
                            ExecuteTransB2B(SQLB2B)

                        Next

                    End With
                End Using

                SQL = "update penjualan_proyek_sementara set flag_sdh_pindah = 'Y' where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
                SQL = SQL & "No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
                ExecuteTrans(SQL)

                j = j + 1
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & "insert penjualan proyek sementara")
            Exit Sub
        End Try
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrEdit.Clear()
            SQLB2B = "select no_faktur from penjualan_proyek_sementara where "
            SQLB2B = SQLB2B & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQLB2B = SQLB2B & " flag_sdh_update = 'Y' "
            Using Ds = BindingTransB2B(SQLB2B)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrEdit.Add(.Rows(i).Item("no_faktur"))
                    Next
                End With
            End Using

            Dim a As Integer = 0
            For b As Integer = 1 To arrEdit.Count
                SQLB2B = "select kode_perusahaan, no_faktur, flag_acc , tanggal_acc, jam_acc from penjualan_proyek_sementara rpp "
                SQLB2B = SQLB2B & "where kode_perusahaan ='" & KodePerusahaan & "' and no_faktur ='" & arrEdit.Item(a).ToString & "' and flag_sdh_update ='Y' "
                Using DsSQL = BindingTransB2B(SQLB2B)
                    With DsSQL.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1
                            Dim flag_acc As String = "NULL"
                            Dim tanggal_acc As String = "NULL"
                            Dim jam_acc As String = "NULL"

                            If General_Class.CekNULL(.Rows(i).Item("flag_acc")) <> "" Then
                                flag_acc = "'" & .Rows(i).Item("flag_acc") & "'"
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("tanggal_acc")) <> "" Then
                                tanggal_acc = "'" & Format(.Rows(i).Item("tanggal_acc"), "yyyy-MM-dd") & "'"
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("jam_acc")) <> "" Then
                                jam_acc = "'" & .Rows(i).Item("jam_acc") & "'"
                            End If

                            SQL = "update penjualan_proyek_sementara set flag_acc=" & flag_acc & ", tanggal_acc=" & tanggal_acc & ", jam_acc=" & jam_acc & " "
                            SQL = SQL & "where kode_perusahaan='" & .Rows(i).Item("Kode_Perusahaan") & "' "
                            SQL = SQL & "and no_faktur='" & .Rows(i).Item("No_Faktur") & "'"
                            ExecuteTrans(SQL)
                        Next
                    End With
                End Using

                SQLB2B = "update penjualan_proyek_sementara set flag_sdh_update = null where "
                SQLB2B = SQLB2B & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
                SQLB2B = SQLB2B & "No_faktur = '" & arrEdit.Item(a).ToString & "' "
                ExecuteTransB2B(SQLB2B)
                a = a + 1
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & "edit penjualan web")
            Exit Sub
        End Try
    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrEdit.Clear()
            SQLB2B = "select id from approval_penjualan_proyek where "
            SQLB2B = SQLB2B & " Flag_WA is null "
            Using Ds = BindingTransB2B(SQLB2B)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrEdit.Add(.Rows(i).Item("id"))
                    Next
                End With
            End Using

            Dim a As Integer = 0
            For b As Integer = 1 To arrEdit.Count
                SQLB2B = "select kode_url from approval_penjualan_proyek rpp "
                SQLB2B = SQLB2B & "where id ='" & arrEdit.Item(a).ToString & "' and Flag_WA is null "
                Using DsSQL = BindingTransB2B(SQLB2B)
                    With DsSQL.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Request As HttpWebRequest
                            Dim Response As HttpWebResponse
                            Dim responseReader As StreamReader
                            Dim result As String
                            Dim custom_uid As String = ""
                            'Dim param_wa As String = "192.168.13.23:8000/api/sentWaPembelianProyek/12341234"

                            Dim param_wa = "https://pro.evomanufacturingindonesia.id/api/sentWa/" & .Rows(i).Item("kode_url")

                            Request = HttpWebRequest.Create(param_wa)
                            Request.Method = "GET"
                            Request.ContentType = "application/json"
                            Request.ContentLength = 0
                            Response = Request.GetResponse
                            responseReader = New StreamReader(Response.GetResponseStream())
                            result = responseReader.ReadToEnd()
                            'MessageBox.Show(result)
                            Dim xSplit2() As String
                            Dim xSplit3() As String
                            Dim xMessage() As String
                            Dim xcode() As String

                            xSplit = Split(result, "data" & """:{", , CompareMethod.Text)
                            xSplit2 = xSplit(1).Split("}")
                            xSplit3 = xSplit2(0).Split(",")

                            'MessageBox.Show(xSplit3(0))
                            'MessageBox.Show(xSplit3(1))
                            'MessageBox.Show(xSplit3(2))

                            xMessage = xSplit3(1).Split(":")
                            xcode = xSplit3(2).Split(":")

                            'MessageBox.Show(xMessage(1).Trim)
                            'MessageBox.Show(xcode(1).Trim)

                            If xcode(1).Trim <> "200" Then
                                CloseTrans()
                                CloseTransB2B()
                                CloseConn()
                                CloseConnB2B()
                                MessageBox.Show(xMessage(1).Trim)
                                MessageBox.Show(xcode(1).Trim)
                                Exit Sub
                            End If

                        Next
                    End With
                End Using

                SQLB2B = "update approval_penjualan_proyek set Flag_WA='Y' "
                SQLB2B = SQLB2B & "where id='" & arrEdit.Item(a).ToString & "'"
                ExecuteTransB2B(SQLB2B)

                a = a + 1
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & "edit approval penjualan sementara")
            Exit Sub
        End Try
    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrEdit.Clear()
            SQLB2B = "select id from approval_po_pembelian_proyek where "
            SQLB2B = SQLB2B & " Flag_WA is null "
            Using Ds = BindingTransB2B(SQLB2B)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrEdit.Add(.Rows(i).Item("id"))
                    Next
                End With
            End Using

            Dim a As Integer = 0
            For b As Integer = 1 To arrEdit.Count
                SQLB2B = "select kode_url from approval_po_pembelian_proyek rpp "
                SQLB2B = SQLB2B & "where id ='" & arrEdit.Item(a).ToString & "' and Flag_WA is null "
                Using DsSQL = BindingTransB2B(SQLB2B)
                    With DsSQL.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Request As HttpWebRequest
                            Dim Response As HttpWebResponse
                            Dim responseReader As StreamReader
                            Dim result As String
                            Dim custom_uid As String = ""
                            'Dim param_wa As String = "192.168.13.23:8000/api/sentWaPembelianProyek/12341234"

                            Dim param_wa As String = "http://192.168.13.23:8000/api/sentWaPembelianProyek/" & .Rows(i).Item("kode_url")
                            Request = HttpWebRequest.Create(param_wa)
                            Request.Method = "GET"
                            Request.ContentType = "application/json"
                            Request.ContentLength = 0
                            Response = Request.GetResponse
                            responseReader = New StreamReader(Response.GetResponseStream())
                            result = responseReader.ReadToEnd()
                            'MessageBox.Show(result)
                            Dim xSplit2() As String
                            Dim xSplit3() As String
                            Dim xMessage() As String
                            Dim xcode() As String

                            xSplit = Split(result, "data" & """:{", , CompareMethod.Text)
                            xSplit2 = xSplit(1).Split("}")
                            xSplit3 = xSplit2(0).Split(",")

                            'MessageBox.Show(xSplit3(0))
                            'MessageBox.Show(xSplit3(1))
                            'MessageBox.Show(xSplit3(2))

                            xMessage = xSplit3(1).Split(":")
                            xcode = xSplit3(2).Split(":")

                            'MessageBox.Show(xMessage(1).Trim)
                            'MessageBox.Show(xcode(1).Trim)

                            If xcode(1).Trim <> "200" Then
                                CloseTrans()
                                CloseTransB2B()
                                CloseConn()
                                CloseConnB2B()
                                MessageBox.Show(xMessage(1).Trim)
                                MessageBox.Show(xcode(1).Trim)
                                Exit Sub
                            End If

                        Next
                    End With
                End Using

                SQLB2B = "update approval_po_pembelian_proyek set Flag_WA='Y' "
                SQLB2B = SQLB2B & "where id='" & arrEdit.Item(a).ToString & "'"
                ExecuteTransB2B(SQLB2B)

                a = a + 1
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & "edit approval PO Pembelian Proyek")
            Exit Sub
        End Try
    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            arrNo_Fak.Clear()
            SQL = "select no_val from Val_Pemb_Proyek where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and flag_sdh_pindah is null and status is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        arrNo_Fak.Add(.Rows(i).Item("no_val"))
                    Next
                End With
            End Using

            SQL = "select Kode_Perusahaan,No_Val,Tanggal,Jam,Keterangan,UserValidasi,Grand,Kode_Bank_Tujuan,No_Rek_Tujuan,Nama_Penerima,Alamat_Penerima,"
            SQL = SQL & "Kota_Penerima,Negara_Penerima,Telp_Penerima,No_Pengajuan "
            SQL = SQL & "from Val_Pemb_Proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null and status is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        SQLB2B = "insert into val_pemb_proyek (kode_perusahaan,no_val,tanggal,jam, "
                        SQLB2B = SQLB2B & "keterangan,uservalidasi,grand,kode_bank_tujuan,no_rek_tujuan,nama_penerima,alamat_penerima, "
                        SQLB2B = SQLB2B & "kota_penerima,negara_penerima,telp_penerima,no_pengajuan )"
                        SQLB2B = SQLB2B & "values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_val") & "',"
                        SQLB2B = SQLB2B & " '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam") & "', "
                        SQLB2B = SQLB2B & "'" & .Rows(i).Item("Keterangan") & "', '" & .Rows(i).Item("UserValidasi") & "', '" & .Rows(i).Item("grand") & "',"
                        SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kode_Bank_Tujuan") & "', '" & .Rows(i).Item("No_Rek_Tujuan") & "',"
                        SQLB2B = SQLB2B & "'" & .Rows(i).Item("Nama_Penerima") & "', '" & .Rows(i).Item("Alamat_Penerima") & "',"
                        SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kota_Penerima") & "', '" & .Rows(i).Item("Negara_Penerima") & "',"
                        SQLB2B = SQLB2B & "'" & .Rows(i).Item("Telp_Penerima") & "', '" & .Rows(i).Item("No_Pengajuan") & "') "
                        ExecuteTransB2B(SQLB2B)

                    Next
                End With
            End Using

            Dim j As Integer = 0
            For z As Integer = 1 To arrNo_Fak.Count
                SQL = "select Kode_Perusahaan,No_Val,No_Faktur,Byr,Urut from Detail_Val_Pemb_Proyek "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Val = '" & arrNo_Fak.Item(j).ToString & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")

                        For i As Integer = 0 To .Rows.Count - 1

                            SQLB2B = "insert into detail_val_pemb_proyek (kode_perusahaan,no_val,no_faktur,byr,urut) "
                            SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_val") & "',"
                            SQLB2B = SQLB2B & "'" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("byr") & "', '" & .Rows(i).Item("urut") & "') "
                            ExecuteTransB2B(SQLB2B)

                        Next
                    End With
                End Using

                SQL = "update val_pemb_proyek set flag_sdh_pindah = 'Y' where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
                SQL = SQL & "No_val = '" & arrNo_Fak.Item(j).ToString & "' "
                ExecuteTrans(SQL)

                j = j + 1
            Next

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()
        Catch ex As Exception
            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show(ex.Message & "insert val pemb proyek")
            Exit Sub
        End Try
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.Clear()
        Clipboard.SetText(ListView1.FocusedItem.Text)

        'Try
        '    Clipboard.Clear()
        '    Clipboard.SetText(ListView1.FocusedItem.Text)
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub get_no_faktur_penawaran()
        Faktur_Penawaran = fMasterPenawaran & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("emi_master_penawaran", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fMasterPenawaran) + 4 & ")", fMasterPenawaran & Format(tgl_skg, "MMyy"))
    End Sub

End Class