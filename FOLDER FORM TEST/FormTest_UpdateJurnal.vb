Public Class FormTest_UpdateJurnal

    Dim arrInisialFaktur As New ArrayList

    Dim itemNoFak As Integer = 0
    Dim itemNoPO As Integer = 1
    Dim itemNoNota As Integer = 2

    Private Sub FormTest_UpdateJurnal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            OpenConn()

            Lv_Data.Columns.Clear()
            Lv_Data.Columns.Add("No Faktur", 200, HorizontalAlignment.Left)
            Lv_Data.Columns.Add("No PO", 200, HorizontalAlignment.Left)
            Lv_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
            Lv_Data.View = View.Details

            Lv_Data.Items.Clear()
            SQL = "select a.No_Faktur, a.No_PO, a.No_Nota "
            SQL = SQL & "from EMI_Pembelian a, EMI_Pembelian_PO b "
            SQL = SQL & "where a.No_PO = b.No_Faktur "
            SQL = SQL & "and b.Flag_Import is null "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_PO"))
                    Lv.SubItems.Add(Dr("No_Nota"))
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
        If Lv_Data.Items.Count = 0 Then Exit Sub

        Jurnal_Lokal(Lv_Data.FocusedItem.SubItems(itemNoFak).Text, Lv_Data.FocusedItem.SubItems(itemNoPO).Text, "HEAD OFFICE")

    End Sub

    Private Sub Jurnal_Lokal(ByVal noPembelian As String, ByVal noPembelianPO As String, ByVal Lokasi As String)

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            '==================================
            '=     HAPUS DATA LAMA Jurnal     =
            '==================================
            SQL = "select Kode_Perusahaan from Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and keterangan = 'Pembelian " & noPembelian & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        SQL = "delete Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and keterangan = 'Pembelian " & noPembelian & "'"
                        ExecuteTrans(SQL)
                    End If
                End With
            End Using

            '===============================================
            '=     HAPUS DATA LAMA Pelunasan_Pembelian     =
            '===============================================
            SQL = "select Kode_Perusahaan from Pelunasan_Pembelian where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noPembelian & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        SQL = "Delete Pelunasan_Pembelian where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noPembelian & "'"
                        ExecuteTrans(SQL)
                    End If
                End With
            End Using


            Dim inisial_faktur_dari As String = ""
            Dim lokasi_Barang As String = ""
            Dim persen_PPN As Integer = 0
            SQL = "select inisial_faktur from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
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

            SQL = "select PPN from emi_pembelian_po "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noPembelianPO & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    persen_PPN = Dr("PPN")
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
            SQL = SQL & "'" & KodeProyek & "', 'Pembelian " & noPembelian & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            Dim Total_Persediaan As Double = 0
            Dim Total_PPN As Double = 0
            Dim Total_Perjalanan As Double = 0
            Dim Total_Hutang As Double = 0
            Dim Total_SelisihHutang As Double = 0
            Dim Total_selisih As Double = 0
            Dim Biaya_Lokal_Total As Double = 0
            Dim akun_Selisih_pembulatan As String = ""

            Dim TotDebit As Double = 0
            Dim TotKredit As Double = 0

            '====================
            '=     GET DATA     =
            '====================
            SQL = "select No_Faktur, Kode_Stock_Owner, Kode_Barang, harga as HargaPO, Harga_Akhir, Jumlah_Masuk, jumlah_hutang, Persen_PPN, No_loading "
            SQL = SQL & "from EMI_Pembelian_detail where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noPembelian & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim akun_persediaan_dari As String = ""
                            Dim akun_ppn As String = ""
                            Dim akun_hutang_sup As String = ""
                            Dim akun_hutang_ppn As String = ""

                            SQL = "select c.akun_Persediaan "
                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and b.kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and b.Kode_Barang='" & .Rows(i).Item("Kode_Barang") & "' "
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

                            SQL = "select Hutang_Supplier, Hutang_Perjalanan, Hutang_PPN, PPN_Pembelian, Selisih_Hutang_Import "
                            SQL = SQL & "from stock_owner "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    akun_hutang_sup = Dr("Hutang_Supplier")
                                    'akun_hutang_perjalanan = Dr("Hutang_Perjalanan")
                                    akun_hutang_ppn = Dr("Hutang_Supplier")
                                    akun_ppn = Dr("PPN_Pembelian")
                                    akun_Selisih_pembulatan = Dr("PPN_Pembelian")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            Dim HargaAkhir As Double = Val(HilangkanTanda(.Rows(i).Item("Harga_Akhir")))
                            Dim HargaPO As Double = Val(HilangkanTanda(.Rows(i).Item("HargaPO")))
                            Dim JumlahMasuk As Double = Val(HilangkanTanda(.Rows(i).Item("Jumlah_Masuk")))
                            Dim JumlahHutang As Double = Val(HilangkanTanda(.Rows(i).Item("jumlah_hutang")))
                            Dim PersenPPN As Double = Val(HilangkanTanda(.Rows(i).Item("Persen_PPN")))



                            'PERHITUNGAN
                            Dim NilaiPersediaan As Double = Math.Round(JumlahMasuk * HargaAkhir)
                            Dim NilaiPPN As Double = Math.Round((JumlahHutang * HargaPO) * (PersenPPN / 100))
                            'Dim NilaiPerjalanan As Double = Math.Round((HargaAkhir - HargaPO) * JumlahMasuk)
                            Dim NilaiHutang As Double = Math.Round((JumlahHutang * HargaPO) + NilaiPPN)
                            Dim selisih As Double = Math.Round(JumlahHutang - JumlahMasuk)
                            Dim NilaiSelisihHutang As Double = Math.Round(selisih * HargaPO)
                            'Dim TotalPO As Double = Math.Round(JumlahMasuk * HargaPO)


                            Total_Persediaan += NilaiPersediaan
                            Total_PPN += NilaiPPN
                            'Total_Perjalanan += NilaiPerjalanan
                            Total_Hutang += NilaiHutang
                            Total_SelisihHutang += NilaiSelisihHutang



                            '1
                            'JURNAL PERSEDIAAN
                            If NilaiPersediaan <> 0 Then
                                'Tambah Lokasi 
                                If Not Insert_Jurnal(.Rows(i).Item("No_Faktur"), Kode_voucher, akun_persediaan_dari, "PERSEDIAAN", NilaiPersediaan, pagenumber, .Rows(i).Item("Kode_Stock_Owner"), "D") Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Gagal Insert Nilai Persediaan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If

                            'JURNAL PPN
                            If NilaiPPN <> 0 Then
                                'RAgu pake akun yg mana :)
                                If Not Insert_Jurnal(.Rows(i).Item("No_Faktur"), Kode_voucher, akun_ppn, "PPN", NilaiPPN, pagenumber, Lokasi, "D") Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Gagal Insert PPN!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If

                            'JURNAL HUTANG SUPPLIER
                            If NilaiHutang <> 0 Then
                                If Not Insert_Jurnal(.Rows(i).Item("No_Faktur"), Kode_voucher, akun_hutang_sup, "HUTANG SUPPLIER", NilaiHutang, pagenumber, Lokasi, "K") Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Gagal Insert Hutang Supplier!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If




                            'JURNAL SELISIH HUTANG 
                            If selisih <> 0 Then
                                Dim akun_hutang_sup_selisih As String = ""
                                Total_selisih += Math.Round(selisih * HargaPO)

                                SQL = "select Akun_Bahan, Akun_Perjalanan from EMI_Pembelian_Selisih_Barang_Masuk "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur_bm = '" & .Rows(i).Item("No_loading") & "' and status is null "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        akun_hutang_sup_selisih = Dr("Akun_Bahan")
                                        'akun_hutang_perjalanan_selisih = Dr("Akun_Perjalanan")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                If selisih > 0 Then
                                    If Not Insert_Jurnal(.Rows(i).Item("No_Faktur"), Kode_voucher, akun_hutang_sup_selisih, "BIAYA TAMBAHAN", NilaiSelisihHutang, pagenumber, Lokasi, "D") Then
                                        CloseTrans()
                                        CloseConn()
                                        Exit Sub
                                    End If
                                ElseIf selisih < 0 Then
                                    If Not Insert_Jurnal(.Rows(i).Item("No_Faktur"), Kode_voucher, akun_hutang_sup_selisih, "BIAYA TAMBAHAN", NilaiSelisihHutang, pagenumber, Lokasi, "K") Then
                                        CloseTrans()
                                        CloseConn()
                                        Exit Sub
                                    End If
                                End If
                            End If

                        Next
                    End If
                End With
            End Using


#Region "JURNAL PERJALANAN"

            'Get Data Perjalanan Lokal
            Dim Arr_Biaya_Lokal_Master As New ArrayList
            Dim Arr_Biaya_Lokal_Kategori As New ArrayList
            Dim Arr_Biaya_Lokal As New ArrayList
            Dim Arr_Akun1 As New ArrayList
            Dim Arr_Akun2 As New ArrayList

            SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, "
            SQL = SQL & "round(sum(b.total), 0) as Biaya, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_1 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi & "' "
            SQL = SQL & "),0) as Akun_1, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_2 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi & "' "
            SQL = SQL & "),0) as Akun_2 "

            SQL = SQL & "from transaksi_biaya_Lokal a, transaksi_biaya_Lokal_detail b, Master_Kategori_Biaya_Import c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
            SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.no_Po = '" & noPembelianPO & "' " 'and C.Flag_Gabungan = 'Y' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        Arr_Biaya_Lokal_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                        Arr_Biaya_Lokal_Kategori.Add(.Rows(index3).Item("kode_kategori_biaya_import"))
                        Arr_Biaya_Lokal.Add(Val(HilangkanTanda(Format(.Rows(index3).Item("Biaya"), "N0"))))
                        Arr_Akun1.Add(.Rows(index3).Item("Akun_1"))
                        Arr_Akun2.Add(.Rows(index3).Item("Akun_2"))

                        Biaya_Lokal_Total = Biaya_Lokal_Total + Val(HilangkanTanda(Format(.Rows(index3).Item("Biaya"), "N0")))

                    Next
                End With

            End Using

            For indexKategoriImport As Integer = 0 To Arr_Biaya_Lokal_Master.Count - 1

                SQL = "select* from Detail_Account_Master where "
                SQL = SQL & "Lokasi = '" & Lokasi & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Lokal_Master.Item(indexKategoriImport) & "' "
                SQL = SQL & "and Akun_1 ='" & Arr_Akun1.Item(indexKategoriImport) & "'  and Akun_2 = '" & Arr_Akun2.Item(indexKategoriImport) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Akun " & Arr_Biaya_Lokal_Master.Item(indexKategoriImport) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End Using

            Next

            'INsert Jurnal
            For indexKategoriImport As Integer = 0 To Arr_Biaya_Lokal_Master.Count - 1
                If Val(Arr_Biaya_Lokal.Item(indexKategoriImport)) <> 0 Then

                    'Jurnal Kategori Biaya Lokal
                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2.Item(indexKategoriImport) & "' and kredit <> 0 "
                    SQL = SQL & "and keterangan ='Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update

                            SQL = "update detail_jurnal set kredit = kredit+ " & Arr_Biaya_Lokal.Item(indexKategoriImport) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2.Item(indexKategoriImport) & "' and kredit <> 0 "
                            SQL = SQL & "and keterangan ='Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "'"
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Akun2.Item(indexKategoriImport), 1),
                                        Strings.Mid(Arr_Akun2.Item(indexKategoriImport), 2, 1),
                                        Strings.Mid(Ganti(Arr_Akun2.Item(indexKategoriImport)), 3),
                                        KodePerusahaan, KodeProyek, "Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport), "0", Arr_Biaya_Lokal.Item(indexKategoriImport), pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                        End If
                    End Using

                    'Data Kategori Biaya Lokal
                    SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & noPembelian.Trim & "' and "
                    SQL = SQL & "Kode = '" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "' and "
                    SQL = SQL & "Kode_Akun = '" & Arr_Akun2.Item(indexKategoriImport) & "'  "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update

                            SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Arr_Biaya_Lokal.Item(indexKategoriImport) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "No_Faktur = '" & noPembelian.Trim & "' and "
                            SQL = SQL & "Kode = '" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "' and "
                            SQL = SQL & "Kode_Akun = '" & Arr_Akun2.Item(indexKategoriImport) & "'  "
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                            SQL = SQL & "values('" & KodePerusahaan & "', '" & noPembelian.Trim & "', "
                            SQL = SQL & "'" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "', '" & Arr_Biaya_Lokal.Item(indexKategoriImport) & "', "
                            SQL = SQL & "'" & Arr_Akun2.Item(indexKategoriImport) & "')"
                            ExecuteTrans(SQL)

                        End If
                    End Using
                End If
            Next

#End Region

            'SELISIH PEmbualatan
            Dim nilai_selisih As Double = (Total_Persediaan + Total_PPN + Total_SelisihHutang) - (Biaya_Lokal_Total + Total_Hutang)

            If nilai_selisih <> 0 Then
                If nilai_selisih < 0 Then
                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Selisih_pembulatan, 1),
                            Strings.Mid(akun_Selisih_pembulatan, 2, 1),
                            Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
                            KodePerusahaan, KodeProyek, " SELISIH HUTANG", Math.Abs(nilai_selisih), "0", pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1
                Else
                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Selisih_pembulatan, 1),
                            Strings.Mid(akun_Selisih_pembulatan, 2, 1),
                            Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
                            KodePerusahaan, KodeProyek, " SELISIH HUTANG", "0", nilai_selisih, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If

                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & noPembelian.Trim & "', "
                SQL = SQL & "'SELISIH HUTANG', '" & nilai_selisih & "', "
                SQL = SQL & "'" & akun_Selisih_pembulatan & "')"
                ExecuteTrans(SQL)
            End If

            '==========================================================
            '=     UNTUK TES / LIAT DEBIT KREDIT BENAR ATAU SALAH     =
            '==========================================================
            SQL = "select debit, kredit, Keterangan, Kode_Account from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_voucher = '" & Kode_voucher & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        'CloseTrans()
                        'CloseConn()
                        'Exit Sub
                    Else
                        'CloseTrans()
                        'CloseConn()
                        'Exit Sub
                    End If
                End With
            End Using

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

            SQL = "Update emi_pembelian "
            SQL = SQL & "Set Kode_Voucher = '" & Kode_voucher & "' "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & noPembelian & "' "
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
    End Sub


    Private Function Insert_Jurnal(ByVal noPembelian As String, ByVal Kode_voucher As String, ByVal akun As String, ByVal Kode_Akun_Pembelian As String, ByVal Nilai As String, ByVal pagenumber As Integer, ByVal lks As String, ByVal Type As String) As Boolean

        Try

            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun & "'  "
            If Type = "D" Then
                SQL = SQL & "and debit <> 0"
            ElseIf Type = "K" Then
                SQL = SQL & "and kredit <> 0"
            End If
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update


                    If Type = "D" Then
                        SQL = "update detail_jurnal set debit = debit+ " & Nilai & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun & "'  "
                        SQL = SQL & "and debit <> 0"

                    ElseIf Type = "K" Then
                        SQL = "update detail_jurnal set kredit = kredit+ " & Nilai & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun & "'  "
                        SQL = SQL & "and kredit <> 0"
                    End If
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert
                    If Type = "D" Then
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun, 1),
                              Strings.Mid(akun, 2, 1),
                              Strings.Mid(Ganti(akun), 3),
                              KodePerusahaan, KodeProyek, Kode_Akun_Pembelian & "; " & noPembelian, Nilai, "0", pagenumber, lks, Bahasa_Pilihan, Ket_Cost_Center_HO)

                    ElseIf Type = "K" Then
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun, 1),
                              Strings.Mid(akun, 2, 1),
                              Strings.Mid(Ganti(akun), 3),
                              KodePerusahaan, KodeProyek, Kode_Akun_Pembelian & "; " & noPembelian, "0", Nilai, pagenumber, lks, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    End If
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

            SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & noPembelian & "' and "
            SQL = SQL & "Kode = '" & Kode_Akun_Pembelian & "' and "
            SQL = SQL & "Kode_Akun = '" & akun & "'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Nilai & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & noPembelian & "' and "
                    SQL = SQL & "Kode = '" & Kode_Akun_Pembelian & "' and "
                    SQL = SQL & "Kode_Akun = '" & akun & "'  "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & noPembelian & "', "
                    SQL = SQL & "'" & Kode_Akun_Pembelian & "', '" & Nilai & "', "
                    SQL = SQL & "'" & akun & "')"
                    ExecuteTrans(SQL)

                End If
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

End Class
