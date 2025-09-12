Public Class N_EMI_Transaksi_Add_Nominal_Stock






    Private Sub N_EMI_Transaksi_Add_Nominal_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data_Split.Columns.Clear()
        Lv_Data_Split.Columns.Add("No Transaksi", 120, HorizontalAlignment.Left)
        Lv_Data_Split.Columns.Add("No PO", 120, HorizontalAlignment.Left)
        Lv_Data_Split.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_Data_Split.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_Data_Split.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        Lv_Data_Split.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Data_Split.Columns.Add("Barang", 200, HorizontalAlignment.Left)
        Lv_Data_Split.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Data_Split.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Data_Split.View = View.Details


        Kosong()
    End Sub

    Private Sub get_no_faktur()

        Dim FNominalStock = "NSS"
        Txt_NoTransaksi.Text = FNominalStock & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("N_EMI_Transaksi_Nilai_Stock_Split", "No_Transaksi", 5,
                             "No_Transaksi", KodePerusahaan,
                             "And", "substring(No_Transaksi, 1, " & Len(FNominalStock) + 4 & ")", FNominalStock & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Kosong()

        Dtp_1.Value = Now.Date
        Lv_Data_Split.Items.Clear()

        Try
            OpenConn()

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Load_Data_LV()

    End Sub

    Private Sub BtnFormulator_Refresh_Click(sender As Object, e As EventArgs) Handles BtnFormulator_Refresh.Click
        Kosong()
    End Sub

    Private Sub Load_Data_LV()
        Try
            OpenConn()

            Lv_Data_Split.Items.Clear()
            SQL = "select a.No_Transaksi as No_Split, a.No_PO, a.tanggal, a.Jam, a.Jumlah_Batch, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.UserID "
            SQL = SQL & "from Emi_Split_Production_Order a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.flag_nominal_Stock is null "
            SQL = SQL & "order by a.No_Transaksi, a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data_Split.Items.Add(Dr("No_Split"))
                    Lv.SubItems.Add(Dr("No_PO"))
                    Lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
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

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If MessageBox.Show("Yakin Ingin Simpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

        If Lv_Data_Split.Items.Count = 0 Then
            MessageBox.Show("Tidak ada Split yang akan di simpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction



            SQL = "exec EMI_Mutasi_Bahan_Dalam_Proses '" & KodePerusahaan & "', '1990-01-01', '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            ExecuteTrans(SQL)

            '================================================================
            '=     CEK DATA YANG BELUM DI INSERT KE TABEL NOMINAL STOCK     =
            '================================================================
            SQL = "select a.No_Transaksi as No_Split, a.No_PO, a.tanggal, a.Jam, a.Jumlah_Batch, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.UserID "
            SQL = SQL & "from Emi_Split_Production_Order a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.flag_nominal_Stock is null "
            SQL = SQL & "order by a.No_Transaksi, a.Tanggal, a.Jam "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim inisial_faktur_dari As String = ""
                            SQL = "select inisial_faktur from stock_owner "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Ket_Lokasi_HO & "' "
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

                            Dim Kode_voucher As String = ""
                            Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)

                            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                            SQL = SQL & "'" & Kode_voucher & "', "
                            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                            SQL = SQL & "'" & KodeProyek & "', 'Nilai Stock Barang " & .Rows(i).Item("Kode_Barang") & "', '', "
                            SQL = SQL & "'-', '" & UserID & "')"
                            ExecuteTrans(SQL)

                            Dim Akun_Nilai_Stock_1 As String
                            Dim Akun_Nilai_Stock_2 As String
                            SQL = "select Akun_Nilai_Stock_1, Akun_Nilai_Stock_2 "
                            SQL = SQL & "from stock_owner "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Akun_Nilai_Stock_1 = Dr("Akun_Nilai_Stock_1")
                                    Akun_Nilai_Stock_2 = Dr("Akun_Nilai_Stock_2")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using



                            'SQL = "WITH cte AS ( SELECT a.kode_perusahaan, a.No_Faktur, a.Tanggal, a.Jam, a.Jenis, a.Keterangan, c.Kode_Group_Jenis, a.Kode_stock_owner AS Lokasi, a.Kode_barang, a.Nama, a.QR, ROUND(Masuk_Saldo,4) - ROUND(Keluar_Saldo,4) AS Nilai "
                            'SQL = SQL & "FROM Data_CutOffTracking3 a, barang b, EMI_Group_Jenis c, Emi_Production_Results d "
                            'SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.kode_perusahaan = d.Kode_Perusahaan "
                            'SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                            'SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                            'SQL = SQL & "and a.No_Faktur = d.No_Transaksi "
                            'SQL = SQL & "and d.No_Production_Order = '" & .Rows(i).Item("No_Split") & "' "
                            'SQL = SQL & "and a.Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and a.Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                            'SQL = SQL & "and b.Id_Group_Jenis = c.Id_Group_Jenis) "
                            'SQL = SQL & ",cte_b as( SELECT kode_perusahaan, Kode_Group_Jenis, Lokasi, kode_barang, Nama, QR, ISNULL([Saldo Awal],0) AS [Saldo Awal], ISNULL([Barang Masuk],0) AS [Barang Masuk], "
                            'SQL = SQL & "ISNULL([Terima Transfer Stock],0) AS [Terima Transfer Stock], ISNULL([Terima Split Stock],0) AS [Terima Split Stock], "
                            'SQL = SQL & "ISNULL([Terima Transfer Material],0) AS [Terima Transfer Material], ISNULL([Penambahan Stock],0) AS [Penambahan Stock], "
                            'SQL = SQL & "ISNULL([Retur Produksi],0) AS [Retur Produksi], ISNULL([Penerimaan Produksi 1],0) AS [Penerimaan Produksi 1], "
                            'SQL = SQL & "ISNULL([Penerimaan Sisa Produksi 1],0) AS [Penerimaan Sisa Produksi 1], ISNULL([Penerimaan Produksi 2],0) AS [Penerimaan Produksi 2], "
                            'SQL = SQL & "ISNULL([Transfer Stock],0) AS [Transfer Stock], ISNULL([Split Stock],0) AS [Split Stock], ISNULL([Transfer Material],0) AS [Transfer Material], "
                            'SQL = SQL & "ISNULL([Pengeluaran Stock],0) AS [Pengeluaran Stock], ISNULL([Pengeluaran Bahan Bakar],0) AS [Pengeluaran Bahan Bakar], "
                            'SQL = SQL & "ISNULL([Pemakaian Produksi 1],0) AS [Pemakaian Produksi 1], ISNULL([Pemakaian Produksi 2],0) AS [Pemakaian Produksi 2], "
                            'SQL = SQL & "ISNULL([Pengeluaran Produksi 2],0) AS [Pengeluaran Produksi 2] "
                            'SQL = SQL & "FROM ( SELECT kode_perusahaan, Lokasi, Kode_Group_Jenis, kode_barang, Nama, QR, Jenis  AS JenisKolom, Nilai FROM cte ) src "
                            'SQL = SQL & "PIVOT ( SUM(Nilai) FOR JenisKolom IN ( [Saldo Awal], [Barang Masuk], [Transfer Stock], [Terima Transfer Stock], [Split Stock], [Terima Split Stock], [Transfer Material], "
                            'SQL = SQL & "[Terima Transfer Material], [Penambahan Stock], [Pengeluaran Stock], [Pengeluaran Bahan Bakar], [Pemakaian Produksi 1], [Retur Produksi], [Penerimaan Produksi 1], "
                            'SQL = SQL & "[Penerimaan Sisa Produksi 1], [Pengeluaran Produksi 2], [Penerimaan Produksi 2], [Pemakaian Produksi 2] ) ) p ) "
                            'SQL = SQL & "select *, Round( [Saldo Awal]+ [Barang Masuk]+ [Transfer Stock]+ [Terima Transfer Stock]+ [Split Stock]+ [Terima Split Stock]+ [Transfer Material]+ [Terima Transfer Material]+ "
                            'SQL = SQL & "[Penambahan Stock]+ [Pengeluaran Stock]+ [Pengeluaran Bahan Bakar]+ [Pemakaian Produksi 1]+ [Retur Produksi]+ [Penerimaan Produksi 1]+ [Penerimaan Sisa Produksi 1]+ "
                            'SQL = SQL & "[Pengeluaran Produksi 2]+ [Penerimaan Produksi 2]+ [Pemakaian Produksi 2],4) as Stock from cte_b a "
                            'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "

                            SQL = ";WITH cte AS (SELECT a.kode_perusahaan,a.No_Faktur, b.No_Production_Order as No_Split, a.Tanggal, a.Jam, a.Jenis, a.Keterangan, a.Kode_stock_owner AS Lokasi, a.Kode_barang, "
                            SQL = SQL & "a.Nama, a.QR, a.serial_number,ROUND(a.Masuk_Saldo,4) - ROUND(a.Keluar_Saldo,4) AS Nilai "
                            SQL = SQL & "FROM Data_CutOffTracking3 a, Emi_Production_Results b "
                            SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "and a.no_Faktur = b.No_Transaksi "
                            SQL = SQL & "and b.No_Production_Order = '" & .Rows(i).Item("No_Split") & "' ) "
                            SQL = SQL & ",cte_b as( SELECT kode_perusahaan, No_Faktur, No_Split, ISNULL([Bahan Baku],0) AS [Bahan Baku], ISNULL([Packaging],0) AS [Packaging], ISNULL([Biaya_Bahan_Bakar],0) AS [Biaya_Bahan_Bakar], "
                            SQL = SQL & "ISNULL([Biaya_Gaji],0) AS [Biaya_Gaji], ISNULL([Biaya_Listrik],0) AS [Biaya_Listrik], ISNULL([Biaya_Air],0) AS [Biaya_Air], ISNULL([Budget Loss],0) AS [Budget Loss], "
                            SQL = SQL & "ISNULL([Penerimaan Produksi 1],0) AS [Penerimaan Produksi 1], ISNULL([Penerimaan Sisa Produksi 1],0) AS [Penerimaan Sisa Produksi 1], "
                            SQL = SQL & "ISNULL([Retur Packaging],0) AS [Retur Packaging], ISNULL([Retur Bahan],0) AS [Retur Bahan], ISNULL([Retur Budget Loss],0) AS [Retur Budget Loss], "
                            SQL = SQL & "ISNULL([Retur Biaya],0) AS [Retur Biaya] "
                            SQL = SQL & "FROM ( SELECT kode_perusahaan, no_faktur,No_Split, Jenis  AS JenisKolom, Nilai FROM cte ) src "
                            SQL = SQL & "PIVOT ( SUM(Nilai) FOR JenisKolom IN ( [Bahan Baku], [Packaging], [Budget Loss], [Biaya_Bahan_Bakar], [Biaya_Gaji], [Biaya_Listrik], [Biaya_Air], [Penerimaan Produksi 1], "
                            SQL = SQL & "[Penerimaan Sisa Produksi 1], [Retur Packaging], [Retur Bahan], [Retur Budget Loss], [Retur Biaya]) "
                            SQL = SQL & ") p) "
                            SQL = SQL & "select *, Round([Bahan Baku]+[Packaging]+[Budget Loss]+[Biaya_Bahan_Bakar]+[Biaya_Gaji]+[Biaya_Listrik]+[Biaya_Air]+[Penerimaan Produksi 1]+[Penerimaan Sisa Produksi 1]+ "
                            SQL = SQL & "[Retur Packaging]+[Retur Bahan]+[Retur Budget Loss]+[Retur Biaya],4) as Stock, "
                            SQL = SQL & "isnull((select isnull(y.flag_val_hpp_produksi,'') from Emi_Production_Results x, Emi_Split_Production_Order y "
                            SQL = SQL & "where x.no_production_order=y.no_transaksi and x.no_transaksi=a.no_faktur),null) as Validasi "
                            SQL = SQL & "from cte_b a where a.kode_perusahaan = '" & KodePerusahaan & "' order by no_faktur "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                        Dim Stock As Double = Ds1.Tables("MyTable").Rows(j).Item("Stock")
                                        Dim pagenumber As Integer = 1
                                        Dim Type As String = ""

                                        If Ds1.Tables("MyTable").Rows(j).Item("Stock") < 0 Then

                                            Type = "D"
                                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Nilai_Stock_1 & "' "
                                            SQL = SQL & "and debit <> 0 "

                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    Dr.Close()
                                                    'update
                                                    SQL = "update detail_jurnal set debit = debit+ " & Stock & " where "
                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Nilai_Stock_1 & "'  "
                                                    SQL = SQL & "and debit <> 0"

                                                    ExecuteTrans(SQL)
                                                Else
                                                    Dr.Close()
                                                    'insert
                                                    If Type = "D" Then
                                                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Nilai_Stock_1, 1),
                                                              Strings.Mid(Akun_Nilai_Stock_1, 2, 1),
                                                              Strings.Mid(Ganti(Akun_Nilai_Stock_1), 3),
                                                              KodePerusahaan, KodeProyek, "Nilai Stock Debit : " & .Rows(i).Item("No_Split"), Stock, "0", pagenumber, Ket_Lokasi_HO, Bahasa_Pilihan, Ket_Cost_Center_HO)

                                                    End If
                                                    ExecuteTrans(SQL)
                                                    pagenumber = pagenumber + 1

                                                End If
                                            End Using

                                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Nilai_Stock_2 & "' "
                                            SQL = SQL & "and kredit <> 0 "

                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    Dr.Close()
                                                    'update

                                                    SQL = "update detail_jurnal set kredit = kredit+ " & Stock & " where "
                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Nilai_Stock_2 & "'  "
                                                    SQL = SQL & "and kredit <> 0"

                                                    ExecuteTrans(SQL)
                                                Else
                                                    Dr.Close()
                                                    'insert
                                                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Nilai_Stock_2, 1),
                                                              Strings.Mid(Akun_Nilai_Stock_2, 2, 1),
                                                              Strings.Mid(Ganti(Akun_Nilai_Stock_2), 3),
                                                              KodePerusahaan, KodeProyek, "Nilai Stock Barang Kredit : " & .Rows(i).Item("No_Split"), "0", Stock, pagenumber, Ket_Lokasi_HO, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                                    ExecuteTrans(SQL)
                                                    pagenumber = pagenumber + 1

                                                End If
                                            End Using

                                        ElseIf Ds1.Tables("MyTable").Rows(j).Item("Stock") >= 0 Then

                                            Type = "K"
                                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Nilai_Stock_1 & "' "
                                            SQL = SQL & "and kredit <> 0 "

                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    Dr.Close()
                                                    'update

                                                    SQL = "update detail_jurnal set kredit = kredit+ " & Stock & " where "
                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Nilai_Stock_1 & "'  "
                                                    SQL = SQL & "and kredit <> 0"

                                                    ExecuteTrans(SQL)
                                                Else
                                                    Dr.Close()
                                                    'insert
                                                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Nilai_Stock_1, 1),
                                                              Strings.Mid(Akun_Nilai_Stock_1, 2, 1),
                                                              Strings.Mid(Ganti(Akun_Nilai_Stock_1), 3),
                                                              KodePerusahaan, KodeProyek, "Nilai Stock Barang Kredit : " & .Rows(i).Item("No_Split"), "0", Stock, pagenumber, Ket_Lokasi_HO, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                                    ExecuteTrans(SQL)
                                                    pagenumber = pagenumber + 1

                                                End If
                                            End Using

                                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Nilai_Stock_2 & "' "
                                            SQL = SQL & "and debit <> 0 "

                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    Dr.Close()
                                                    'update
                                                    SQL = "update detail_jurnal set debit = debit+ " & Stock & " where "
                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Nilai_Stock_2 & "'  "
                                                    SQL = SQL & "and debit <> 0"

                                                    ExecuteTrans(SQL)
                                                Else
                                                    Dr.Close()
                                                    'insert
                                                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Nilai_Stock_2, 1),
                                                              Strings.Mid(Akun_Nilai_Stock_2, 2, 1),
                                                              Strings.Mid(Ganti(Akun_Nilai_Stock_2), 3),
                                                              KodePerusahaan, KodeProyek, "Nilai Stock Debit : " & .Rows(i).Item("No_Split"), Stock, "0", pagenumber, Ket_Lokasi_HO, Bahasa_Pilihan, Ket_Cost_Center_HO)


                                                    ExecuteTrans(SQL)
                                                    pagenumber = pagenumber + 1

                                                End If
                                            End Using

                                        End If


                                        '======================
                                        '=     CEK JURNAL     =
                                        '======================
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


                                        SQL = "insert into N_EMI_Transaksi_Nilai_Stock_Split (Kode_Perusahaan, No_Transaksi, No_Split, Tanggal, Jam, Akun, Nilai_Stock, Kode_Stock_Owner, Kode_Barang, Kode_Voucher) "
                                        SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoTransaksi.Text & "', '" & .Rows(i).Item("No_Split") & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                        SQL = SQL & "'" & If(Type = "D", Akun_Nilai_Stock_1, Akun_Nilai_Stock_2) & "', "
                                        SQL = SQL & "'" & Stock & "', '" & .Rows(i).Item("Kode_Stock_Owner") & "', '" & .Rows(i).Item("Kode_Barang") & "', '" & Kode_voucher & "') "
                                        ExecuteTrans(SQL)


                                    Next
                                End If
                            End Using


                            SQL = "update Emi_Split_Production_Order set Flag_Nominal_Stock = 'Y' "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Transaksi = '" & .Rows(i).Item("No_Split") & "' "
                            ExecuteTrans(SQL)



                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
    End Sub
End Class