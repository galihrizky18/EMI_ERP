Public Class Transaksi_Biaya_import
    Dim arrcari, arrAkun As New ArrayList
    Dim arrMaster As New ArrayList
    Dim arrKategori As New ArrayList

    Dim LvID As String
    Dim Lvlokasi As String
    Dim LvKodeKategori As String
    Dim LvKodeBiaya As String
    Dim LvNamaBiaya As String
    Dim LvKontainer As String
    Dim LvJumlahKontainer As String
    Dim LvKodePerusahaanBiaya As String
    Dim LvNamaPerusahaan As String
    Dim LvPerhitungan As String
    Dim LvMataUang As String
    Dim LvKurs As String
    Dim LvBiaya As String
    Dim LvNilai2 As String
    Dim LvTotal As String
    Dim LvMaster As String
    Dim LvJns As String
    Dim LvTotalMUA As String
    Dim LvValidasi As String

    Dim LvKategori2 As String
    Dim LvTotal2 As String

    Dim LvPerusahaan3 As String
    Dim LvMataUang3 As String
    Dim LvTotal3 As String


    Dim LvFlagAvg As String 'coding stenly

    Dim cellID As Integer
    Dim celllokasi As Integer
    Dim cellKodeKategori As Integer
    Dim cellKodeBiaya As Integer
    Dim cellNamaBiaya As Integer
    Dim cellKontainer As Integer
    Dim cellJumlahKontainer As Integer
    Dim cellKodePerusahaanBiaya As Integer
    Dim cellNamaPerusahaan As Integer
    Dim cellPerhitungan As Integer
    Dim cellMataUang As Integer
    Dim cellKurs As Integer
    Dim cellBiaya As Integer
    Dim cellNilai2 As Integer
    Dim cellTotal As Integer
    Dim cellMaster As Integer
    Dim cellJns As Integer
    Dim cellTotalMUA As Integer
    Dim cellValidasi As Integer

    Dim cellKategori2 As Integer
    Dim cellTotal2 As Integer

    Dim cellPerusahaan3 As Integer
    Dim cellMataUang3 As Integer
    Dim cellTotal3 As Integer


    Dim cellFlagAvg As Integer 'coding stenly

    Dim hitung As Integer

    Dim id_rencana_group As String = ""
    Dim lokasi_group As String = ""
    Dim Konte_group As Double = 0

    Dim dari_tombol_cari As Boolean = False

    Dim err As Boolean = False

    Dim _filter_tambahan As String = "and isnull((select X.Id_rencana_induk from rencana_order_gabungan X where ro.Flag_Gabungan = 'Y' and ro.ID_Rencana = X.ID_Rencana ),ro.id_rencana) = ro.id_rencana and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw = 'Y' and ro.Flag_Draft = 'Y' and ro.Flag_Final = 'Y' and ro.Flag_Kirim = 'Y' and ro.Flag_Finish = 'Y' and ro.flag_kapal_tiba = 'Y' and ro.flag_penjaluran = 'Y' and ro.flag_sppb = 'Y' and ro.flag_tarik_kontainer = 'Y' and ro.flag_bongkar = 'Y' and ro.flag_lokasi_tujuan = 'Y' and Flag_Validasi_Biaya = 'Y' and  ro.flag_sudah_transaksi is null "

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvID = DataGridView1.Rows(No_Index).Cells(0).Value.ToString : cellID = 0
        Lvlokasi = DataGridView1.Rows(No_Index).Cells(1).Value.ToString : celllokasi = 1
        LvKodeKategori = DataGridView1.Rows(No_Index).Cells(2).Value.ToString : cellKodeKategori = 2
        LvKodeBiaya = DataGridView1.Rows(No_Index).Cells(3).Value.ToString : cellKodeBiaya = 3
        LvNamaBiaya = DataGridView1.Rows(No_Index).Cells(4).Value.ToString : cellNamaBiaya = 4
        LvKontainer = DataGridView1.Rows(No_Index).Cells(5).Value.ToString : cellKontainer = 5
        LvJumlahKontainer = DataGridView1.Rows(No_Index).Cells(6).Value.ToString : cellJumlahKontainer = 6
        LvKodePerusahaanBiaya = DataGridView1.Rows(No_Index).Cells(7).Value.ToString : cellKodePerusahaanBiaya = 7
        LvNamaPerusahaan = DataGridView1.Rows(No_Index).Cells(8).Value.ToString : cellNamaPerusahaan = 8
        LvPerhitungan = DataGridView1.Rows(No_Index).Cells(9).Value.ToString : cellPerhitungan = 9
        LvMataUang = DataGridView1.Rows(No_Index).Cells(10).Value.ToString : cellMataUang = 10
        LvKurs = DataGridView1.Rows(No_Index).Cells(11).Value.ToString : cellKurs = 11
        LvBiaya = DataGridView1.Rows(No_Index).Cells(12).Value.ToString : cellBiaya = 12
        LvNilai2 = DataGridView1.Rows(No_Index).Cells(13).Value.ToString : cellNilai2 = 13
        LvTotal = DataGridView1.Rows(No_Index).Cells(14).Value.ToString : cellTotal = 14
        LvMaster = DataGridView1.Rows(No_Index).Cells(15).Value.ToString : cellMaster = 15
        LvJns = DataGridView1.Rows(No_Index).Cells(16).Value.ToString : cellJns = 16
        LvFlagAvg = DataGridView1.Rows(No_Index).Cells(17).Value.ToString : cellFlagAvg = 17 'coding stenly
        LvTotalMUA = DataGridView1.Rows(No_Index).Cells(18).Value.ToString : cellTotalMUA = 18
        LvValidasi = DataGridView1.Rows(No_Index).Cells(19).Value.ToString : cellValidasi = 19
    End Sub

    Private Sub Perbandingan_harga()
        err = False

        Try
            OpenConn()

            SQL = "select a.no_faktur, a.id_rencana, c.lokasi,c.Kode_Kontainer, c.kode_supplier, b.*, d.nama, d.Kode "
            SQL = SQL & "from biaya_import_validasi a, biaya_import_validasi_detail b, "
            SQL = SQL & "rencana_order c, biaya_import_validasi_detail2 d where "
            SQL = SQL & "a.Kode_perusahaan =b.Kode_Perusahaan and a.No_faktur=b.No_faktur and "
            SQL = SQL & "a.Kode_Perusahaan=c.Kode_Perusahaan and a.Id_rencana=c.Id_rencana and "
            SQL = SQL & "a.Kode_Perusahaan=d.Kode_Perusahaan and a.No_faktur=d.No_faktur and d.flag_val_Akhir='Y' "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.status is null and "
            SQL = SQL & "a.id_rencana='" & TxtId_Rencana.Text & "' and a.flag_validasi='Y' and asal_form <>3 "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")

                    For i As Integer = 0 To .Rows.Count - 1
                        Dim asal As String = ""
                        Dim nilai As Double = .Rows(i).Item("Perusahaan_" & .Rows(i).Item("Kode") & "_PCS")
                        If .Rows(i).Item("Asal_form") = "1" Then
                            asal = "BIAYA_IMPORT_DETAIL"
                        ElseIf .Rows(i).Item("Asal_form") = "2" Then
                            asal = "BIAYA_IMPORT_DETAIL2"
                        Else
                            CloseConn()
                            MessageBox.Show("Asal Form Tidak dI temukan .  . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            err = True
                            Exit Sub
                        End If



                        Dim kurs As Double = 0

                        If id_rencana_group.Trim.Length = 0 Then
                            id_rencana_group = "'" & TxtId_Rencana.Text & "'"
                        End If

                        Dim hitungMUA As Double = 0
                        Dim Untuk_gudang As String = ""

                        If asal = "BIAYA_IMPORT_DETAIL2" Then
                            Untuk_gudang = "and Y.Kode_Gudang=A.Kode_Gudang "
                        End If

                        SQL = "select a.*, isnull((select datediff(day,format(Tanggal_Bongkar, 'yyyy-MM-dd'),format(DATEADD(dd, Free_Time, Eta), 'yyyy-MM-dd')) from ubah_status_otw a, Bongkar_Import b where "
                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_Rencana = b.ID_Rencana and A.id_rencana ='" & TxtId_Rencana.Text & "'),0) as Selisih_Tanggal, "

                        SQL = SQL & "round(isnull(( select sum(Z.Persentase) from submit_PO X, Kontainer_Masuk_Per_Lokasi Y, Kontainer_masuk_Per_jenis Z "
                        SQL = SQL & "where(Y.Kode_Perusahaan = X.Kode_Perusahaan And X.No_faktur = Y.No_Faktur And Y.Lokasi_Tujuan = a.Kode_Stock_Owner) "
                        SQL = SQL & "and Y.Kode_Perusahaan= Z.Kode_Perusahaan and Y.No_Faktur= Z.No_Faktur and Y.No_Container= Z.No_Container and "
                        SQL = SQL & "X.id_rencana in(" & id_rencana_group & ") and X.status is null and Z.Jenis=a.Jns ),0)/100,0) as konte_per_jenis, "

                        SQL = SQL & "isnull(( select COUNT(Y.No_Container) from submit_PO X, "
                        SQL = SQL & "Kontainer_masuk_Per_Lokasi Y where Y.Kode_Perusahaan = X.Kode_Perusahaan and "
                        SQL = SQL & "X.No_faktur = Y.No_Faktur and Y.Lokasi_Tujuan = a.Kode_Stock_Owner and X.id_rencana in(" & id_rencana_group & ")"
                        SQL = SQL & "and X.status is null ),0) as konte_per_lokasi,"

                        SQL = SQL & "isnull((select case when 'Biaya_Import_Detail'='" & asal & "' then 0 else "
                        SQL = SQL & "isnull((select COUNT(Y.No_Container) from submit_PO X, Kontainer_masuk_Per_Lokasi Y where "
                        SQL = SQL & "Y.Kode_Perusahaan = X.Kode_Perusahaan And X.No_faktur = Y.No_Faktur And Y.Lokasi_Tujuan = a.Kode_Stock_Owner "
                        SQL = SQL & "and X.id_rencana in(" & id_rencana_group & ") " & Untuk_gudang & " and X.status is null),0) end),0) as konte_per_Gudang, "

                        SQL = SQL & "isnull(( select sum(X.Jml_Kontainer) from Log_Penjaluran_Import X where X.id_rencana "
                        SQL = SQL & "in(" & id_rencana_group & ") and Warna ='Merah'),0) as Konte_Penjaluran, "

                        SQL = SQL & "isnull(( select sum(X.Jml_Kontainer_Karantina) from Penjaluran_Import X where X.id_rencana "
                        SQL = SQL & "in(" & id_rencana_group & ")),0) as Konte_Asuransi, "

                        SQL = SQL & "isnull(( select sum(Y.Total) from submit_PO X, Detail_submit_PO Y where Y.Kode_Perusahaan "
                        SQL = SQL & "= X.Kode_Perusahaan and X.No_faktur = Y.No_Faktur and X.id_rencana "
                        SQL = SQL & "in(" & id_rencana_group & ") and X.status is null ),0) as Total_Declare, "

                        SQL = SQL & "d.flag_average, d.kode_kategori_biaya_Import, d.kode_master_kategori_biaya_import, c.nama as nama_biaya, e.Nama as Nama_Perusahaan "
                        SQL = SQL & "from " & asal & " a, Biaya_Import c, kategori_biaya_import d, Perusahaan_Biaya_Import e, "
                        SQL = SQL & "perusahaan_biaya_import_kategori f where a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Perusahaan_Biaya_Import=f.Kode_Perusahaan_Biaya_Import "
                        SQL = SQL & "and a.Kode_Perusahaan=e.Kode_Perusahaan and a.Kode_Perusahaan_Biaya_Import=e.Kode_Perusahaan_Biaya_Import and "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = c.Kode_Perusahaan " 'coding stenly
                        SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and a.kode_biaya = c.kode_biaya and c.kode_kategori_biaya_import = d.kode_kategori_biaya_import " 'coding stenly
                        SQL = SQL & "and a.Kode_Biaya = '" & .Rows(i).Item("Kode_Biaya") & "' and a.Kode_Stock_Owner = '" & .Rows(i).Item("Lokasi") & "' "
                        SQL = SQL & "and f.Kategori_Perusahaan_Biaya_Import = '" & .Rows(i).Item("nama") & "' and a.Kode_Kontainer = '" & .Rows(i).Item("Kode_Kontainer") & "' and a.Kode_Mata_Uang='" & .Rows(i).Item("Mata_Uang") & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then


                                DataGridView1.Rows.Add(1)
                                Dim index As Integer = DataGridView1.Rows.Count - 1
                                DataGridView1.Rows.Item(index).Cells(0).Value = .Rows(i).Item("id_rencana")
                                DataGridView1.Rows.Item(index).Cells(1).Value = .Rows(i).Item("Lokasi")
                                DataGridView1.Rows.Item(index).Cells(2).Value = Dr("kode_kategori_biaya_import")
                                DataGridView1.Rows.Item(index).Cells(3).Value = Dr("Kode_biaya")
                                DataGridView1.Rows.Item(index).Cells(4).Value = Dr("Nama_Biaya")
                                DataGridView1.Rows.Item(index).Cells(5).Value = .Rows(i).Item("Kode_Kontainer")
                                DataGridView1.Rows.Item(index).Cells(6).Value = TxtJumlah_conte.Text

                                DataGridView1.Rows.Item(index).Cells(7).Value = Dr("Kode_Perusahaan_biaya_import")
                                DataGridView1.Rows.Item(index).Cells(8).Value = Dr("Nama_Perusahaan")

                                DataGridView1.Rows.Item(index).Cells(9).Value = Dr("Perhitungan")
                                DataGridView1.Rows.Item(index).Cells(10).Value = Dr("Kode_Mata_Uang")

                                Dim Kategori_Kurs = ""

                                If Dr("Perhitungan") <> "I" Then
                                    Kategori_Kurs = "BIAYA"
                                Else
                                    Kategori_Kurs = "ASURANSI"
                                End If

                                For index1 As Integer = 0 To ListView2.Items.Count - 1

                                    If Dr("Kode_Mata_Uang") = ListView2.Items(index1).SubItems(0).Text And Kategori_Kurs = ListView2.Items(index1).SubItems(2).Text Then
                                        kurs = ListView2.Items(index1).SubItems(1).Text
                                    End If

                                Next

                                If kurs = 0 Then
                                    MessageBox.Show("Kurs Mata Uang " & Dr("Kode_Mata_Uang") & " Pada Kategori " & Kategori_Kurs & " Tidak Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Dr.Close()
                                    CloseConn()
                                    DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                                    err = True
                                    Exit Sub
                                End If

                                DataGridView1.Rows.Item(index).Cells(11).Value = Format(kurs, "N2")

                                If Dr("Perhitungan") <> "I" Then
                                    DataGridView1.Rows.Item(index).Cells(12).Value = Format(nilai, "N2")
                                Else
                                    DataGridView1.Rows.Item(index).Cells(12).Value = Format(nilai, "N3")
                                End If


                                DataGridView1.Rows.Item(index).Cells(13).Value = Format(0, "N2")
                                If Dr("Perhitungan") = "A" Then
                                    hitung = kurs * nilai * Val(TxtJumlah_conte.Text)
                                    hitungMUA = nilai * Val(TxtJumlah_conte.Text)
                                ElseIf Dr("Perhitungan") = "B" Then
                                    hitung = kurs * nilai
                                    hitungMUA = nilai
                                ElseIf Dr("Perhitungan") = "C" Then
                                    'hitung = (Val(Berat.Text) * (kurs * Dr("Nilai"))) + Dr("Nilai_2")
                                    'hitungMUA = (Val(Berat.Text) * (Dr("Nilai"))) + Dr("Nilai_2")
                                    Dr.Close()
                                    CloseConn()
                                    MessageBox.Show("Perhitungan tidak Mendukung . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    err = True
                                    Exit Sub
                                ElseIf Dr("Perhitungan") = "D" Then
                                    hitung = (kurs * nilai) * Dr("Selisih_Tanggal")
                                    hitungMUA = (nilai) * Dr("Selisih_Tanggal")
                                    JumlahHari.Text = Dr("Selisih_Tanggal")
                                ElseIf Dr("Perhitungan") = "E" Then
                                    Dim jml_konte As Integer = 0
                                    If Dr("Konte_Penjaluran") < Dr("Min") Then
                                        MessageBox.Show("Jumlah Container kurang dari jumlah minimal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                                        Dr.Close()
                                        CloseConn()
                                        err = True
                                        Exit Sub
                                    ElseIf Dr("Konte_Penjaluran") < Dr("Max") Then
                                        jml_konte = Dr("Konte_Penjaluran") - (Dr("Min") - 1)
                                    ElseIf Dr("Konte_Penjaluran") >= Dr("Max") Then
                                        jml_konte = Dr("Max") - (Dr("Min") - 1)
                                    Else
                                        MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                                        Dr.Close()
                                        CloseConn()
                                        err = True
                                        Exit Sub
                                    End If

                                    hitung = (kurs * nilai) * jml_konte
                                    hitungMUA = (nilai) * jml_konte
                                    DataGridView1.Rows.Item(index).Cells(6).Value = jml_konte
                                ElseIf Dr("Perhitungan") = "F" Then
                                    Dim jml_konte As Integer = 0
                                    If Val(TxtJumlah_conte.Text) < Dr("Min") Then
                                        MessageBox.Show("Jumlah Container kurang dari jumlah minimal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                                        Dr.Close()
                                        CloseConn()
                                        err = True
                                        Exit Sub
                                    ElseIf Val(TxtJumlah_conte.Text) < Dr("Max") Then
                                        jml_konte = Val(TxtJumlah_conte.Text) - (Dr("Min") - 1)
                                    ElseIf Val(TxtJumlah_conte.Text) >= Dr("Max") Then
                                        jml_konte = Dr("Max") - (Dr("Min") - 1)
                                    Else
                                        MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                                        Dr.Close()
                                        CloseConn()
                                        err = True
                                        Exit Sub
                                    End If

                                    hitung = (kurs * nilai) * jml_konte
                                    hitungMUA = (nilai) * jml_konte
                                    DataGridView1.Rows.Item(index).Cells(6).Value = jml_konte
                                ElseIf Dr("Perhitungan") = "G" Then

                                    Dim Konte As Integer = 0

                                    If General_Class.CekNULL(Dr("Jns")) = "WET" Or General_Class.CekNULL(Dr("Jns")) = "DRY" Then
                                        Konte = Dr("konte_per_jenis")
                                    ElseIf asal = "BIAYA_IMPORT_DETAIL2" And General_Class.CekNULL(Dr("Jns")) = "ALL" Then
                                        Konte = Dr("konte_per_Gudang")
                                    Else
                                        Konte = Dr("konte_per_lokasi")
                                    End If

                                    hitung = (kurs * nilai) * Konte
                                    hitungMUA = (nilai) * Konte
                                    DataGridView1.Rows.Item(index).Cells(6).Value = Konte
                                ElseIf Dr("Perhitungan") = "H" Then

                                    hitung = kurs * nilai * Dr("Konte_Asuransi")
                                    hitungMUA = nilai * Dr("Konte_Asuransi")
                                    DataGridView1.Rows.Item(index).Cells(6).Value = Dr("Konte_Asuransi")
                                ElseIf Dr("Perhitungan") = "I" Then

                                    'Total_Dec.Text = Dr("Total_Declare")
                                    'hitung = (((Dr("Total_Declare") * nilai) / 100) + Dr("Nilai_2")) * kurs
                                    'hitungMUA = ((Dr("Total_Declare") * nilai) / 100) + Dr("Nilai_2")
                                    Dr.Close()
                                    CloseConn()
                                    MessageBox.Show("Perhitungan tidak Mendukung . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    err = True
                                    Exit Sub
                                Else
                                    MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                                    Dr.Close()
                                    CloseConn()
                                    err = True
                                    Exit Sub
                                End If

                                DataGridView1.Rows.Item(index).Cells(14).Value = Format(hitung, "N2")
                                DataGridView1.Rows.Item(index).Cells(15).Value = Dr("Kode_master_kategori_biaya_import")
                                DataGridView1.Rows.Item(index).Cells(16).Value = General_Class.CekNULL(Dr("Jns"))
                                DataGridView1.Rows.Item(index).Cells(17).Value = General_Class.CekNULL(Dr("Flag_average")) 'coding stenly
                                DataGridView1.Rows.Item(index).Cells(18).Value = Format(hitungMUA, "N2")
                                DataGridView1.Rows.Item(index).Cells(19).Value = "Y"
                                'ambil data master
                                If arrMaster.Count = 0 Then
                                    arrMaster.Add(Dr("Kode_master_kategori_biaya_import"))
                                End If

                                Dim ada_data As Boolean = True
                                For index1 As Integer = 0 To arrMaster.Count - 1
                                    'FREIGHT 'FREIGHT
                                    If arrMaster.Item(index1) = Dr("kode_master_kategori_biaya_import") Then
                                        ada_data = False
                                    End If

                                Next

                                If ada_data = True Then
                                    arrMaster.Add(Dr("Kode_master_kategori_biaya_import"))
                                End If



                                'ambil data Kategori
                                If DataGridView2.Rows.Count = 0 Then
                                    DataGridView2.Rows.Add(1)
                                    DataGridView2.Rows.Item(0).Cells(0).Value = Dr("kode_kategori_biaya_import")
                                    DataGridView2.Rows.Item(0).Cells(1).Value = 0
                                End If


                                Dim ada_data2 As Boolean = True
                                For index1 As Integer = 0 To DataGridView2.Rows.Count - 1
                                    Get_Isi_Listview2(index1)
                                    If LvKategori2 = Dr("kode_kategori_biaya_import") Then
                                        ada_data2 = False
                                    End If

                                Next

                                If ada_data2 = True Then
                                    DataGridView2.Rows.Add(1)
                                    Dim index2 As Integer = DataGridView2.Rows.Count - 1
                                    DataGridView2.Rows.Item(index2).Cells(0).Value = Dr("kode_kategori_biaya_import")
                                    DataGridView2.Rows.Item(index2).Cells(1).Value = 0
                                End If

                                'ambil data perusahaan
                                If DataGridView3.Rows.Count = 0 Then
                                    DataGridView3.Rows.Add(1)

                                    DataGridView3.Rows.Item(0).Cells(0).Value = Dr("Kode_Perusahaan_biaya_import")
                                    DataGridView3.Rows.Item(0).Cells(1).Value = Dr("Kode_Mata_Uang")
                                    DataGridView3.Rows.Item(0).Cells(2).Value = 0
                                End If


                                Dim ada_data3 As Boolean = True
                                For index1 As Integer = 0 To DataGridView3.Rows.Count - 1
                                    Get_Isi_Listview3(index1)
                                    If LvPerusahaan3 = Dr("Kode_Perusahaan_biaya_import") And LvMataUang3 = Dr("Kode_Mata_Uang") Then
                                        ada_data3 = False
                                    End If

                                Next

                                If ada_data3 = True Then
                                    DataGridView3.Rows.Add(1)
                                    Dim index2 As Integer = DataGridView3.Rows.Count - 1
                                    DataGridView3.Rows.Item(index2).Cells(0).Value = Dr("Kode_Perusahaan_biaya_import")
                                    DataGridView3.Rows.Item(index2).Cells(1).Value = Dr("Kode_Mata_Uang")
                                    DataGridView3.Rows.Item(index2).Cells(2).Value = 0
                                End If
                            Else
                                Dr.Close()
                                CloseConn()
                                MessageBox.Show("Data Tidak Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                err = True
                                Exit Sub
                            End If
                        End Using
                    Next

                End With
            End Using





            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            err = True
            Exit Sub
        End Try

        HitungGrand()
    End Sub

    Public Sub Get_Isi_Listview2(ByVal No_Index As Integer)

        LvKategori2 = DataGridView2.Rows(No_Index).Cells(0).Value.ToString : cellKategori2 = 0
        LvTotal2 = DataGridView2.Rows(No_Index).Cells(1).Value.ToString : cellTotal2 = 1

    End Sub

    Public Sub Get_Isi_Listview3(ByVal No_Index As Integer)

        LvPerusahaan3 = DataGridView3.Rows(No_Index).Cells(0).Value.ToString : cellPerusahaan3 = 0
        LvMataUang3 = DataGridView3.Rows(No_Index).Cells(1).Value.ToString : cellMataUang3 = 1
        LvTotal3 = DataGridView3.Rows(No_Index).Cells(2).Value.ToString : cellTotal3 = 1

    End Sub

    Private Sub HitungGrand()
        Dim ttl As Double = 0

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Get_Isi_Listview(i)

            ttl = ttl + Val(HilangkanTanda(LvTotal))
        Next

        txtTotalBiaya.Text = Format(ttl, "N0")
        txtBiayaLama.Text = "0"
        txtGrand.Text = Format(ttl, "N0")


        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            Get_Isi_Listview2(i)
            Dim ttlKategori As Double = 0

            For j As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(j)

                If LvKategori2 = LvKodeKategori Then
                    ttlKategori = ttlKategori + Val(HilangkanTanda(LvTotal))
                End If

            Next
            DataGridView2.Rows.Item(i).Cells(1).Value = Format(ttlKategori, "N0")
        Next

        For i As Integer = 0 To DataGridView3.Rows.Count - 1
            Get_Isi_Listview3(i)
            Dim ttlperusahaan As Double = 0

            For j As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(j)

                If LvPerusahaan3 = LvKodePerusahaanBiaya And LvMataUang3 = LvMataUang Then
                    ttlperusahaan = ttlperusahaan + Val(HilangkanTanda(LvTotalMUA))
                End If

            Next
            DataGridView3.Rows.Item(i).Cells(2).Value = Format(ttlperusahaan, "N0")
        Next

    End Sub

    Private Sub Transaksi_Biaya_import_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Barang_Kategori_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView2.Clear()
        ListView2.Columns.Add("MUA", 50, HorizontalAlignment.Center)
        ListView2.Columns.Add("Nilai Kurs", 70, HorizontalAlignment.Left)
        ListView2.Columns.Add("Jenis", 70, HorizontalAlignment.Right).DisplayIndex = 1

        Kosong()
        kosong_MUA()
    End Sub

    Public Sub Tampil_Data_Group()

        If TxtContainer.Text.Trim.Length = 0 Then
            MessageBox.Show("Masukkan Rencana Order Terlebih Dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try

            OpenConn()
            id_rencana_group = ""
            lokasi_group = ""
            Konte_group = 0
            Dim index As Integer = 0
            SQL = "select a.id_rencana, a.lokasi, a.Total_Persen from rencana_order a, rencana_order_gabungan b where "
            SQL = SQL & "a.id_rencana = b.Id_rencana and b.Id_rencana_induk = '" & TxtId_Rencana.Text & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    If index <> 0 Then
                        id_rencana_group = id_rencana_group & ", "
                        lokasi_group = lokasi_group & ", "
                    End If
                    id_rencana_group = id_rencana_group & "'" & Dr("id_rencana") & "'"
                    lokasi_group = lokasi_group & "'" & Dr("lokasi") & "'"

                    Dim kontainer As Integer = Dr("Total_Persen") / 100
                    Dim jumlah As Integer = kontainer * 100
                    Dim selisih As Integer = Dr("Total_Persen") - jumlah

                    If selisih = 0 Or selisih <= 99 Then
                        kontainer = kontainer
                    Else
                        kontainer = kontainer + 1
                    End If
                    Konte_group = Konte_group + kontainer
                    index += 1
                Loop
            End Using

            TxtJumlah_conte.Text = Konte_group

            SQL = "select a.id_rencana from rencana_order a where "
            SQL = SQL & " a.id_rencana in(" & id_rencana_group & ")"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            SQL = "select kode_perusahaan from Rencana_Order ro where "
                            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Rencana = '" & .Rows(i).Item("id_rencana") & "' and "
                            SQL = SQL & "ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw = 'Y' and ro.Flag_Draft = 'Y' and ro.Flag_Final = 'Y' "
                            SQL = SQL & "and ro.Flag_Kirim = 'Y' and ro.Flag_Finish = 'Y' and ro.flag_kapal_tiba = 'Y' and ro.flag_penjaluran = 'Y' and ro.flag_sppb = 'Y' "
                            SQL = SQL & "and ro.flag_bongkar = 'Y' and ro.flag_lokasi_tujuan = 'Y' "
                            Using Dr = OpenTrans(SQL)
                                If Not Dr.Read Then
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("ada tahapan yang belum di selesaikan pada ID Rencana ini!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Id Rencana tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            SQL = "select round(sum((b.Total_berat_Bersih/1000)),2) as Total  from "
            SQL = SQL & "loading_barang a, detail_loading_barang b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.id_rencana in(" & id_rencana_group & ") and a.status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Berat.Text = Dr("total")
                End If
            End Using

            ListView1.Items.Clear()

            SQL = ";with cte_X as( "
            SQL = SQL & "select a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, a.Kode_Biaya, b.Nama as Nama_Biaya, a.Kode_Kontainer, "
            SQL = SQL & "a.Kode_Perusahaan_Biaya_Import, e.Nama, '' as Kode_Gudang, 'Biaya_Import_Detail' as tabel_asal from Biaya_Import_Detail a, biaya_import b, Kategori_Biaya_Import c, "
            SQL = SQL & "master_Kategori_biaya_import d, perusahaan_biaya_import e where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Biaya = b.Kode_Biaya and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and a.perhitungan <> 'G' and a.Kode_Stock_Owner in(" & lokasi_group & ")"

            SQL = SQL & "union all "

            SQL = SQL & "select a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, a.Kode_Biaya, b.Nama as Nama_Biaya, a.Kode_Kontainer, "
            SQL = SQL & "a.Kode_Perusahaan_Biaya_Import, e.Nama, Kode_Gudang, 'Biaya_Import_Detail2' as tabel_asal from Biaya_Import_Detail2 a, biaya_import b, Kategori_Biaya_Import c, "
            SQL = SQL & "master_Kategori_biaya_import d, perusahaan_biaya_import e where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Biaya = b.Kode_Biaya and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and a.perhitungan <> 'G' and a.Kode_Stock_Owner in(" & lokasi_group & ") "

            SQL = SQL & "union all "

            SQL = SQL & "select a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            SQL = SQL & "a.Kode_Biaya, b.Nama as Nama_Biaya, a.Kode_Kontainer, a.Kode_Perusahaan_Biaya_Import, e.Nama, '' "
            SQL = SQL & "as Kode_Gudang, 'Biaya_Import_Detail' as tabel_asal from Biaya_Import_Detail a, biaya_import b, "
            SQL = SQL & " Kategori_Biaya_Import c, master_Kategori_biaya_import d, perusahaan_biaya_import e, Submit_PO f, "
            SQL = SQL & "Kontainer_masuk_per_lokasi g where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Biaya = b.Kode_Biaya And b.Kode_Perusahaan "
            SQL = SQL & "= c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import and "
            SQL = SQL & "c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = "
            SQL = SQL & "d.Kode_Master_Kategori_Biaya_Import and a.Kode_Perusahaan = e.Kode_Perusahaan and "
            SQL = SQL & " a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import and a.perhitungan = 'G' "
            SQL = SQL & "and f.Kode_Perusahaan = g.Kode_Perusahaan and f.No_faktur = g.No_Faktur "
            SQL = SQL & "and f.id_rencana in(" & id_rencana_group & ") and a.Kode_stock_Owner = g.Lokasi_Tujuan and f.status is null "
            SQL = SQL & "group by a.perhitungan,a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            SQL = SQL & "a.Kode_Biaya, b.Nama, a.Kode_Kontainer, a.Kode_Perusahaan_Biaya_Import, e.Nama "

            SQL = SQL & "union all "

            SQL = SQL & "select a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            SQL = SQL & "a.Kode_Biaya, b.Nama as Nama_Biaya, a.Kode_Kontainer, a.Kode_Perusahaan_Biaya_Import, e.Nama, a.Kode_Gudang, "
            SQL = SQL & "'Biaya_Import_Detail2' as tabel_asal from Biaya_Import_Detail2 a, biaya_import b, "
            SQL = SQL & " Kategori_Biaya_Import c, master_Kategori_biaya_import d, perusahaan_biaya_import e, Submit_PO f, "
            SQL = SQL & "Kontainer_masuk_per_lokasi g where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Biaya = b.Kode_Biaya And b.Kode_Perusahaan "
            SQL = SQL & "= c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import and "
            SQL = SQL & "c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = "
            SQL = SQL & "d.Kode_Master_Kategori_Biaya_Import and a.Kode_Perusahaan = e.Kode_Perusahaan and "
            SQL = SQL & " a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import and a.perhitungan = 'G' "
            SQL = SQL & "and f.Kode_Perusahaan = g.Kode_Perusahaan and f.No_faktur = g.No_Faktur "
            SQL = SQL & "and f.id_rencana in(" & id_rencana_group & ") and a.Kode_stock_Owner = g.Lokasi_Tujuan and f.status is null "
            SQL = SQL & "group by a.perhitungan,a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            SQL = SQL & "a.Kode_Biaya, b.Nama, a.Kode_Kontainer, a.Kode_Perusahaan_Biaya_Import, e.Nama, a.Kode_Gudang "


            SQL = SQL & ") "
            SQL = SQL & "select* from cte_x where Kode_Kontainer = '" & TxtContainer.Text & "' "

            If ComboBox2.SelectedIndex > 0 Then
                SQL = SQL & " and Kode_Master_Kategori_Biaya_import ='" & ComboBox2.Text & "'"
            End If
            If ComboBox3.SelectedIndex > 0 Then
                SQL = SQL & " and Kode_Kategori_Biaya_Import ='" & ComboBox3.Text & "'"
            End If
            If ComboBox4.SelectedIndex > 0 Then
                SQL = SQL & " and Nama ='" & ComboBox4.Text & "'"
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("kode_stock_owner"))
                    Lvw.SubItems.Add(dr("Kode_Master_Kategori_Biaya_import"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Biaya_Import"))
                    Lvw.SubItems.Add(dr("Kode_Biaya"))
                    Lvw.SubItems.Add(dr("Nama_Biaya"))
                    Lvw.SubItems.Add(dr("Kode_Kontainer"))
                    Lvw.SubItems.Add(dr("Kode_Perusahaan_Biaya_Import"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(dr("Kode_Gudang"))
                    Lvw.SubItems.Add(dr("tabel_asal"))

                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        If dari_tombol_cari <> True Then
            Perbandingan_harga()


            If err = True Then
                ListView1.Items.Clear()
                Exit Sub
            End If
        End If
        dari_tombol_cari = False

    End Sub

    Public Sub Tampil_Data()

        If TxtContainer.Text.Trim.Length = 0 Then
            MessageBox.Show("Masukkan Rencana Order Terlebih Dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If



        Try

            OpenConn()


            SQL = "select isnull(round(sum((b.Total_berat_Bersih/1000)),2), 0) as Total  from "
            SQL = SQL & "loading_barang a, detail_loading_barang b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.id_rencana = '" & TxtId_Rencana.Text & "' and a.status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Berat.Text = Dr("total")
                End If
            End Using
            ListView1.Items.Clear()

            SQL = ";with cte_X as( "
            SQL = SQL & "select a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, a.Kode_Biaya, b.Nama as Nama_Biaya, a.Kode_Kontainer, "
            SQL = SQL & "a.Kode_Perusahaan_Biaya_Import, e.Nama, '' as Kode_Gudang, 'Biaya_Import_Detail' as tabel_asal from Biaya_Import_Detail a, biaya_import b, Kategori_Biaya_Import c, "
            SQL = SQL & "master_Kategori_biaya_import d, perusahaan_biaya_import e where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Biaya = b.Kode_Biaya and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and a.perhitungan <> 'G' and a.Kode_Stock_Owner ='" & CmbLokasi.SelectedItem & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, a.Kode_Biaya, b.Nama as Nama_Biaya, a.Kode_Kontainer, "
            SQL = SQL & "a.Kode_Perusahaan_Biaya_Import, e.Nama, Kode_Gudang, 'Biaya_Import_Detail2' as tabel_asal from Biaya_Import_Detail2 a, biaya_import b, Kategori_Biaya_Import c, "
            SQL = SQL & "master_Kategori_biaya_import d, perusahaan_biaya_import e where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Biaya = b.Kode_Biaya and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and a.perhitungan <> 'G' and a.Kode_Stock_Owner ='" & CmbLokasi.SelectedItem & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            SQL = SQL & "a.Kode_Biaya, b.Nama as Nama_Biaya, a.Kode_Kontainer, a.Kode_Perusahaan_Biaya_Import, e.Nama, '' "
            SQL = SQL & "as Kode_Gudang, 'Biaya_Import_Detail' as tabel_asal from Biaya_Import_Detail a, biaya_import b, "
            SQL = SQL & " Kategori_Biaya_Import c, master_Kategori_biaya_import d, perusahaan_biaya_import e, Submit_PO f, "
            SQL = SQL & "Kontainer_masuk_per_lokasi g where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Biaya = b.Kode_Biaya And b.Kode_Perusahaan "
            SQL = SQL & "= c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import and "
            SQL = SQL & "c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = "
            SQL = SQL & "d.Kode_Master_Kategori_Biaya_Import and a.Kode_Perusahaan = e.Kode_Perusahaan and "
            SQL = SQL & " a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import and a.perhitungan = 'G' "
            SQL = SQL & "and f.Kode_Perusahaan = g.Kode_Perusahaan and f.No_faktur = g.No_Faktur "
            SQL = SQL & "and f.Id_Rencana = '" & TxtId_Rencana.Text & "' and a.Kode_stock_Owner = g.Lokasi_Tujuan and f.status is null "
            SQL = SQL & "group by a.perhitungan,a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            SQL = SQL & "a.Kode_Biaya, b.Nama, a.Kode_Kontainer, a.Kode_Perusahaan_Biaya_Import, e.Nama "

            SQL = SQL & "union all "

            SQL = SQL & "select a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            SQL = SQL & "a.Kode_Biaya, b.Nama as Nama_Biaya, a.Kode_Kontainer, a.Kode_Perusahaan_Biaya_Import, e.Nama, a.Kode_Gudang, "
            SQL = SQL & "'Biaya_Import_Detail2' as tabel_asal from Biaya_Import_Detail2 a, biaya_import b, "
            SQL = SQL & " Kategori_Biaya_Import c, master_Kategori_biaya_import d, perusahaan_biaya_import e, Submit_PO f, "
            SQL = SQL & "Kontainer_masuk_per_lokasi g where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Biaya = b.Kode_Biaya And b.Kode_Perusahaan "
            SQL = SQL & "= c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import and "
            SQL = SQL & "c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = "
            SQL = SQL & "d.Kode_Master_Kategori_Biaya_Import and a.Kode_Perusahaan = e.Kode_Perusahaan and "
            SQL = SQL & " a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import and a.perhitungan = 'G' "
            SQL = SQL & "and f.Kode_Perusahaan = g.Kode_Perusahaan and f.No_faktur = g.No_Faktur "
            SQL = SQL & "and f.Id_Rencana = '" & TxtId_Rencana.Text & "' and a.Kode_stock_Owner = g.Lokasi_Tujuan and f.status is null "
            SQL = SQL & "group by a.perhitungan,a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            SQL = SQL & "a.Kode_Biaya, b.Nama, a.Kode_Kontainer, a.Kode_Perusahaan_Biaya_Import, e.Nama, a.Kode_Gudang "


            SQL = SQL & ") "
            SQL = SQL & "select* from cte_x where Kode_Kontainer = '" & TxtContainer.Text & "' "

            If ComboBox2.SelectedIndex > 0 Then
                SQL = SQL & " and Kode_Master_Kategori_Biaya_import ='" & ComboBox2.Text & "'"
            End If
            If ComboBox3.SelectedIndex > 0 Then
                SQL = SQL & " and Kode_Kategori_Biaya_Import ='" & ComboBox3.Text & "'"
            End If
            If ComboBox4.SelectedIndex > 0 Then
                SQL = SQL & " and Nama ='" & ComboBox4.Text & "'"
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("kode_stock_owner"))
                    Lvw.SubItems.Add(dr("Kode_Master_Kategori_Biaya_import"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Biaya_Import"))
                    Lvw.SubItems.Add(dr("Kode_Biaya"))
                    Lvw.SubItems.Add(dr("Nama_Biaya"))
                    Lvw.SubItems.Add(dr("Kode_Kontainer"))
                    Lvw.SubItems.Add(dr("Kode_Perusahaan_Biaya_Import"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(dr("Kode_Gudang"))
                    Lvw.SubItems.Add(dr("tabel_asal"))

                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If dari_tombol_cari <> True Then
            Perbandingan_harga()


            If err = True Then
                ListView1.Items.Clear()
                Exit Sub
            End If
        End If
        dari_tombol_cari = False

    End Sub

    Private Sub Get_No_Faktur()
        TxtNo_Faktur.Text = TBiaya_Import & Format(DtTanggal.Value, "MMyy") & "-" & _
                             General_Class.Get_Last_Number2("Transaksi_Biaya_Import", "no_Faktur", 5, _
                             "Kode_perusahaan", KodePerusahaan, _
                             "And", "substring(no_Faktur, 1, " & Len(TBiaya_Import) + 4 & ")", TBiaya_Import & Format(DtTanggal.Value, "MMyy"))
    End Sub

    Private Sub kosong_MUA()

        ListView2.Items.Clear()

        ComboBox1.Items.Clear()

        Try
            OpenConn()
            SQL = "Select Kode_mata_uang From mata_uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_mata_uang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Kode_Mata_Uang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        ComboBox5.Items.Clear()
        ComboBox5.Items.Add("BIAYA")
        ComboBox5.Items.Add("ASURANSI")

    End Sub

    Public Sub Kosong()
        GetTime()
        DtTanggal.Value = Tanggal_Sekarang
        Total_Dec.Text = ""
        JumlahHari.Text = ""
        txtTotalBiaya.Text = ""
        TxtNo_Faktur.Text = ""
        TxtNo_PO.Text = ""
        TxtKeterangan.Text = ""
        TxtContainer.Text = ""
        TxtId_Rencana.Text = ""
        TxtJumlah_conte.Text = ""
        TxtSupplier.Text = ""
        Berat.Text = ""
        TxtFlag_Group.Text = ""
        id_rencana_group = ""
        lokasi_group = ""
        Konte_group = 0
        DtTanggal_Po.Value = Tanggal_Sekarang
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        DataGridView3.Rows.Clear()

        arrMaster.Clear()

        ListView1.Clear()
        ListView1.Columns.Add("Lokasi", 140, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode Master", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Kategori", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Biaya", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama Biaya", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kontainer", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Perusahaan Biaya Import", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama Perusahaan", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode Gudang", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Tabel asal", 0, HorizontalAlignment.Left)
        ListView1.View = View.Details


        Try
            OpenConn()

            Get_No_Faktur()
            DataGridView1.Rows.Clear()

            CmbLokasi.Items.Clear()
            SQL = "Select Kode_stock_owner From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbLokasi.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using


            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("-- Seluruh --")
            SQL = "select Kode_Master_Kategori_Biaya_import from master_Kategori_biaya_import where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Master_Kategori_Biaya_import"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox2.Items.Add(dr("Kode_Master_Kategori_Biaya_import"))
                Loop
            End Using

            ComboBox3.Items.Clear()
            ComboBox3.Items.Add("-- Seluruh --")
            SQL = "select Kode_Kategori_Biaya_import from Kategori_biaya_import where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Kategori_Biaya_import"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox3.Items.Add(dr("Kode_Kategori_Biaya_import"))
                Loop
            End Using

            ComboBox4.Items.Clear()
            ComboBox4.Items.Add("-- Seluruh --")
            SQL = "Select Nama From Perusahaan_Biaya_Import where kode_perusahaan = '" & KodePerusahaan & "' order by Nama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        CmbLokasi.Text = Lokasi
    End Sub

    Private Sub TxtNo_Faktur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNo_Faktur.KeyPress
        If e.KeyChar = Chr(13) Then CmbLokasi.Focus()
    End Sub

    Private Sub CmbLokasi_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbLokasi.KeyPress
        If e.KeyChar = Chr(13) Then DtTanggal.Focus()
    End Sub

    Private Sub DtTanggal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DtTanggal.KeyPress
        If e.KeyChar = Chr(13) Then TxtKeterangan.Focus()
    End Sub

    Private Sub TxtKeterangan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtKeterangan.KeyPress
        If e.KeyChar = Chr(13) Then TxtNo_PO.Focus()
    End Sub

    Private Sub TxtNo_PO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNo_PO.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan.Focus()
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Kosong()
    End Sub

    Private Sub BtCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCari.Click
        Display_Rencana_Order_Lain_Lain.dari_mana = "TRANSAKSI_BIAYA"
        Display_Rencana_Order_Lain_Lain.filter_tambahan = _filter_tambahan
        Display_Rencana_Order_Lain_Lain.ShowDialog()
    End Sub

    Private Sub TxtNo_PO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNo_PO.TextChanged
        'Try
        '    OpenConn()
        '    DataGridView1.Rows.Clear()
        '    Dim no As Integer = 0

        '    SQL = "select a.kode_kategori_biaya_import, b.kode_biaya, b.keterangan , b.biaya, c.total_persen, b.kode_mata_uang, c.kode_kontainer from kategori_biaya_import a, biaya_import b, rencana_order c "
        '    SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.kode_kategori_biaya_import = b.kode_kategori_biaya_import and b.lokasi = c.lokasi and b.kode_kontainer = c.kode_kontainer and b.kode_kontainer = '" & TxtContainer.Text & "' and b.lokasi = '" & CmbLokasi.Text & "' order by a.urutan,b.keterangan"
        '    Ds = BindingTrans(SQL)
        '    For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
        '        With Ds.Tables("MyTable").Rows(i)
        '            DataGridView1.Rows.Add(1)

        '            Dim kontainer As Integer = (.Item("total_persen")) / 100
        '            Dim jumlah As Integer = kontainer * 100
        '            Dim selisih As Integer = (.Item("total_persen")) - jumlah
        '            Dim a As Double
        '            Dim jmlhkonte As Integer = 0
        '            'If (.Item("kode_mata_uang")) = "" Then
        '            If selisih = 0 Or selisih <= 20 Then
        '                a = kontainer * .Item("Biaya")
        '                jmlhkonte = kontainer
        '            Else
        '                a = (kontainer + 1) * .Item("Biaya")
        '                jmlhkonte = kontainer + 1
        '            End If

        '            DataGridView1.Rows.Item(no).Cells(0).Value = .Item("kode_kategori_biaya_import")
        '            DataGridView1.Rows.Item(no).Cells(1).Value = .Item("kode_biaya")
        '            DataGridView1.Rows.Item(no).Cells(2).Value = .Item("Keterangan")
        '            If selisih = 0 Or selisih <= 20 Then
        '                DataGridView1.Rows.Item(no).Cells(3).Value = Format(jmlhkonte, "N0")
        '            Else
        '                DataGridView1.Rows.Item(no).Cells(3).Value = Format(jmlhkonte, "N2")
        '            End If
        '            DataGridView1.Rows.Item(no).Cells(4).Value = Format(.Item("Biaya"), "N2")
        '            DataGridView1.Rows.Item(no).Cells(5).Value = Format(a, "N2")

        '            no = no + 1
        '        End With
        '    Next

        '    CloseConn()
        'Catch ex As Exception
        '    DataGridView1.Rows.Clear()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub

        'End Try
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        If TxtKeterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Harus Di isi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKeterangan.Focus()
            Exit Sub
        ElseIf DataGridView1.RowCount = 0 Then
            MessageBox.Show("Data Tidak Ada . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DataGridView1.Focus()
            Exit Sub
        End If
        GetTime()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Get_No_Faktur()

            Dim kd_sup As String = ""
            Dim flag_average As String = ""

            SQL = "Select a.Flag_Sudah_Transaksi, a.kode_supplier, b.flag_average from "
            SQL = SQL & "Rencana_Order a, suppliers b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.kode_supplier = b.kode_supplier and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Id_Rencana = '" & TxtId_Rencana.Text & "' and a.status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    kd_sup = Dr("kode_supplier")
                    flag_average = Dr("flag_average")

                    If General_Class.CekNULL(Dr("Flag_Sudah_Transaksi")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Sudah Dilakukan Transaksi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Rencana order tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using

            Dim inisial_Faktur As String = ""
            SQL = "select top(1) Inisial_faktur "
            SQL = SQL & " from Stock_Owner where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_stock_Owner = '" & CmbLokasi.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    inisial_Faktur = dr("Inisial_faktur")
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim flag_group As String = ""
            If TxtFlag_Group.Text = "" Then
                flag_group = "T"
            Else
                flag_group = "Y"
            End If

            SQL = "insert into transaksi_biaya_import(kode_perusahaan, no_faktur, tanggal, jam, UserID, Id_Rencana, "
            SQL = SQL & "keterangan, jml_kontainer, Total_Berat, Grand_Total, Flag_Group, Flag_Average) "
            SQL = SQL & "values('" & KodePerusahaan & "','" & TxtNo_Faktur.Text & "', '" & Format(CDate(DtTanggal.Text), "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "',"
            SQL = SQL & "'" & UserID & "', '" & TxtId_Rencana.Text & "', '" & TxtKeterangan.Text & "', "
            SQL = SQL & "'" & TxtJumlah_conte.Text & "', '" & Berat.Text & "', '" & HilangkanTanda(txtGrand.Text) & "', '" & flag_group & "', '" & flag_average & "')"
            ExecuteTrans(SQL)


            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(i)

                Dim jns As String = ""
                If LvJns = "" Then
                    jns = "NULL"
                Else
                    jns = "'" & LvJns & "'"
                End If

                Dim FLAG_HPP As String = ""
                SQL = "select isnull(Flag_Masuk_HPP,'T') as Flag_Masuk_HPP "
                SQL = SQL & "from Kategori_Biaya_Import where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Kode_Kategori_Biaya_Import = '" & LvKodeKategori & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        FLAG_HPP = dr("Flag_Masuk_HPP")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Kategori tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "insert into detail_transaksi_biaya_import (kode_perusahaan, No_faktur, Id_Rencana, Kode_stock_Owner, Kode_Biaya, "
                SQL = SQL & "Kode_Kontainer, jml_kontainer, Kode_Perusahaan_Biaya_Import, Jenis_Perhitungan, "
                SQL = SQL & "Mata_Uang, Kurs, biaya, Nilai_2, Total, Kode_Master_Kategori_Biaya_Import, Jns, "
                SQL = SQL & "kode_kategori_biaya_import,flag_average_kategori, avg_biaya, total_avg_biaya, Flag_Validasi_Biaya, Flag_Masuk_HPP) values( " 'coding stenly
                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtNo_Faktur.Text & "', "
                SQL = SQL & "'" & TxtId_Rencana.Text & "', '" & Lvlokasi & "', "
                SQL = SQL & "'" & LvKodeBiaya & "', '" & LvKontainer & "', "
                SQL = SQL & "'" & LvJumlahKontainer & "', '" & LvKodePerusahaanBiaya & "', "
                SQL = SQL & "'" & LvPerhitungan & "', '" & LvMataUang & "', " & HilangkanTanda(LvKurs) & ", "
                SQL = SQL & HilangkanTanda(LvBiaya) & ", " & HilangkanTanda(LvNilai2) & ", " & HilangkanTanda(LvTotal) & ", '" & LvMaster & "', "
                SQL = SQL & "" & jns & ", '" & LvKodeKategori & "', '" & LvFlagAvg & "', '" & HilangkanTanda(LvBiaya) & "', " & HilangkanTanda(LvTotal) & ", '" & LvValidasi & "', '" & FLAG_HPP & "')" 'coding stenly
                ExecuteTrans(SQL)
            Next

            For i As Integer = 0 To DataGridView3.Rows.Count - 1
                Get_Isi_Listview3(i)

                SQL = "insert into detail_transaksi_biaya_import_by_Perusahaan(kode_perusahaan, No_faktur, Kode_Perusahaan_Biaya_Import, Mata_Uang, Nilai) "
                SQL = SQL & "values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtNo_Faktur.Text & "', "
                SQL = SQL & "'" & LvPerusahaan3 & "', '" & LvMataUang3 & "', "
                SQL = SQL & "'" & HilangkanTanda(LvTotal3) & "') "
                ExecuteTrans(SQL)
            Next

            Dim pagenumber As Integer = 1

            Dim Kode_Voucher2 As String = ""

            Dim __Kode_Voucher2 As String = "NULL"

            Kode_Voucher2 = GetLastNumberJurnal(Format(Tanggal_Sekarang, "yyyyMM"), fJU & inisial_Faktur, KodePerusahaan)
            __Kode_Voucher2 = "'" & Kode_Voucher2 & "'"


            For index As Integer = 0 To arrMaster.Count - 1
                Dim TotMaster As Double = 0

                SQL = "select* from Master_Kategori_Biaya_Import where Kode_Master_kategori_Biaya_import = '" & arrMaster.Item(index) & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        With Ds.Tables("MyTable")
                            For index2 As Integer = 0 To .Rows.Count - 1

                                If General_Class.CekNULL(.Rows(index2).Item("flag_masuk_Jurnal")) = "Y" Then
                                    For i As Integer = 0 To DataGridView1.Rows.Count - 1
                                        Get_Isi_Listview(i)

                                        If LvMaster = arrMaster(index) Then
                                            TotMaster = TotMaster + HilangkanTanda(LvTotal)
                                        End If

                                    Next

                                    SQL = "insert into detail_transaksi_biaya_import2 (kode_perusahaan, No_faktur, Kode_Master_Kategori_Biaya_import, Nilai) "
                                    SQL = SQL & " values( "
                                    SQL = SQL & "'" & KodePerusahaan & "', '" & TxtNo_Faktur.Text & "', "
                                    SQL = SQL & "'" & arrMaster.Item(index) & "', '" & TotMaster & "') "
                                    ExecuteTrans(SQL)


                                    'DIKOMEN SEMENTARA SAJA


                                    'Dim Akun1 As String = ""
                                    'Dim Akun2 As String = ""


                                    'SQL = "select top(1) akun_1, akun_2 "
                                    'SQL = SQL & " from Detail_Account_Master where "
                                    'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    'SQL = SQL & "Kode_Master_Kategori_Biaya_Import = '" & arrMaster(index) & "'"
                                    'Using dr = OpenTrans(SQL)
                                    '    If dr.Read Then
                                    '        Akun1 = dr("akun_1")
                                    '        Akun2 = dr("akun_2")

                                    '    Else
                                    '        dr.Close()
                                    '        CloseTrans()
                                    '        CloseConn()
                                    '        MessageBox.Show("Data Master tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '        Exit Sub
                                    '    End If
                                    'End Using

                                    'pagenumber = 1
                                    'SQL = "select kode_perusahaan from detail_jurnal where "
                                    'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    'SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "'"
                                    'Using Dr = OpenTrans(SQL)
                                    '    If Not Dr.Read Then
                                    '        Dr.Close()
                                    '        SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                                    '        SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                                    '        SQL = SQL & "'" & Kode_Voucher2 & "', "
                                    '        SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
                                    '        SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                                    '        SQL = SQL & "'" & KodeProyek & "', 'Biaya Import " & TxtNo_Faktur.Text & "', '', "
                                    '        SQL = SQL & "'-', '" & UserID & "', '" & CmbLokasi.Text & "')"
                                    '        ExecuteTrans(SQL)
                                    '    End If
                                    'End Using


                                    'SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(Akun1, 1), _
                                    '              Strings.Mid(Akun1, 2, 1), _
                                    '              Strings.Mid(Ganti(Akun1), 3), _
                                    '              KodePerusahaan, KodeProyek, "Transaksi Biaya Import " & TxtNo_Faktur.Text & " ; " & arrMaster(index), TotMaster, "0", pagenumber, "BELUM")
                                    'ExecuteTrans(SQL)
                                    'pagenumber = pagenumber + 1
                                    ''vv
                                    'SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(Akun2, 1), _
                                    '              Strings.Mid(Akun2, 2, 1), _
                                    '              Strings.Mid(Ganti(Akun2), 3), _
                                    '              KodePerusahaan, KodeProyek, "Transaksi Biaya Import " & TxtNo_Faktur.Text & " ; " & arrMaster(index), "0", TotMaster, pagenumber, "BELUM")
                                    'ExecuteTrans(SQL)
                                    'pagenumber = pagenumber + 1

                                    'SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                                    'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    'SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "'"
                                    'Using Dr = OpenTrans(SQL)
                                    '    If Dr.Read Then
                                    '        If Dr("debit") <> Dr("kredit") Then
                                    '            Dr.Close()
                                    '            CloseTrans()
                                    '            CloseConn()
                                    '            MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '            Exit Sub
                                    '        End If
                                    '    Else
                                    '        Dr.Close()
                                    '        CloseTrans()
                                    '        CloseConn()
                                    '        MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '        Exit Sub
                                    '    End If
                                    'End Using

                                End If

                            Next
                        End With
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Master tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                End Using


            Next

            If flag_average = "Y" Then
                SQL = ";with "
                SQL = SQL & "cte_Total_Konte as( "
                SQL = SQL & "select X.No_Faktur, X.Jml_Kontainer, Y.Kode_Kategori_Biaya_Import, sum(Total) as Total, "
                SQL = SQL & "isnull((select M.Selisih_lama from avg_kategori_biaya_Import M where "
                SQL = SQL & "M.No_Faktur = X.No_faktur and M.Kode_Kategori_Biaya_Import = Y.Kode_Kategori_Biaya_import),0) as Selisih_Lama  "
                SQL = SQL & "from Transaksi_Biaya_Import X, Detail_Transaksi_Biaya_Import Y, Rencana_Order Z, Suppliers V "
                SQL = SQL & "where X.Kode_Perusahaan = Y.Kode_Perusahaan and X.No_Faktur = Y.No_faktur and "
                SQL = SQL & "X.Kode_Perusahaan = Z. Kode_Perusahaan and X.Id_rencana = Z.Id_Rencana and "
                SQL = SQL & "Z.kode_Perusahaan = V.Kode_Perusahaan And Z.Kode_Supplier = v.Kode_Supplier "
                SQL = SQL & "and X.Flag_Average ='Y' and Z.Kode_Supplier = '" & kd_sup & "' and "
                SQL = SQL & "Y.Flag_Average_Kategori = 'Y' and x.status is null "
                SQL = SQL & "group by X.no_faktur, X.Jml_Kontainer, Y.Kode_Kategori_Biaya_Import "
                SQL = SQL & ") "

                SQL = SQL & ",cte_AVG as( "
                SQL = SQL & "select a.Kode_Perusahaan, a.No_Faktur, a.Id_rencana, a.Jml_Kontainer, "
                SQL = SQL & "b.Kode_kategori_Biaya_Import, sum(Total) as Total "

                SQL = SQL & ",isnull((select sum(X.Jml_Kontainer) from cte_Total_Konte X "
                SQL = SQL & "where X.Kode_Kategori_Biaya_Import = b.Kode_kategori_Biaya_Import "
                SQL = SQL & "),0) total_Kontainer "

                SQL = SQL & ",isnull((select sum(X.Total)+sum(X.Selisih_Lama) from cte_Total_Konte X "
                SQL = SQL & "where X.Kode_Kategori_Biaya_Import = b.Kode_kategori_Biaya_Import "
                SQL = SQL & "),0) total_Biaya "

                SQL = SQL & ",isnull((select top(1) Selisih from Detail_Selisih_Transaksi_Biaya_Import_By_Kategori X "
                SQL = SQL & "where X.Kode_Kategori_Biaya_Import = b.Kode_kategori_Biaya_Import and X.Kode_SUpplier ='" & kd_sup & "' "
                SQL = SQL & "and X.Pakai is null and X.Status is null),0) as Selisih_Lama "

                SQL = SQL & ",isnull((select top(1) Urut from Detail_Selisih_Transaksi_Biaya_Import_By_Kategori X "
                SQL = SQL & "where X.Kode_Kategori_Biaya_Import = b.Kode_kategori_Biaya_Import and X.Kode_SUpplier ='" & kd_sup & "' "
                SQL = SQL & "and X.Pakai is null and X.Status is null),NULL) as Urut_Selisih_Lama "

                SQL = SQL & "from Transaksi_Biaya_Import a, detail_Transaksi_Biaya_import b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_faktur  "
                SQL = SQL & "and a.No_Faktur = '" & TxtNo_Faktur.Text.Trim & "' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "a.flag_Average = 'Y' and b.flag_Average_Kategori = 'Y' and a.status is null "
                SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur, a.Id_rencana, a.Jml_Kontainer, b.Kode_kategori_Biaya_Import "
                SQL = SQL & ") "

                SQL = SQL & ",Cte_data as( "
                SQL = SQL & "select a.Kode_Perusahaan, a.No_Faktur, a.ID_rencana, a.Kode_Kategori_Biaya_Import, a.selisih_lama, a.Urut_Selisih_Lama, a.Total_Kontainer, isnull(b.Flag_Masuk_HPP,'T') as Flag_Masuk_HPP, "
                SQL = SQL & "round(((a.Total_Biaya+a.Selisih_Lama)/a.Total_Kontainer),0) as Biaya_Avg, "
                SQL = SQL & "round((((a.Total_Biaya+a.Selisih_Lama)/a.Total_Kontainer)*a.Jml_Kontainer),0) as Total_Biaya_Avg, "
                SQL = SQL & "round((a.Total+a.selisih_lama-(((a.Total_Biaya+a.Selisih_Lama)/a.Total_Kontainer)*a.Jml_Kontainer)),0) as selisih_Baru "
                SQL = SQL & "from cte_AVG a, Kategori_Biaya_Import b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Kategori_Biaya_Import = b.Kode_Kategori_Biaya_Import "
                SQL = SQL & ") "

                SQL = SQL & "insert into avg_Kategori_Biaya_import(Kode_Perusahaan, No_Faktur, Kode_Kategori_Biaya_Import, "
                SQL = SQL & "Avg_Biaya, Total_Avg_Biaya, Selisih_Lama, Urut_Selisih_Lama, Selisih_Baru, Id_Rencana, Total_Kontainer, Flag_Masuk_HPP) "
                SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Kategori_Biaya_Import, Biaya_Avg, "
                SQL = SQL & "Total_Biaya_Avg, selisih_lama, Urut_Selisih_Lama, selisih_Baru, Id_Rencana, Total_Kontainer, Flag_Masuk_HPP from cte_data "
                ExecuteTrans(SQL)



                SQL = "select No_faktur, Kode_Kategori_Biaya_Import, Urut_Selisih_Lama "
                SQL = SQL & "from avg_Kategori_Biaya_import where Urut_Selisih_Lama is not null and No_Faktur = '" & TxtNo_Faktur.Text & "' and Kode_Perusahaan = '" & KodePerusahaan & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1

                                SQL = "update Detail_Selisih_Transaksi_Biaya_Import_By_Kategori "
                                SQL = SQL & "set Pakai = 'Y' where Urut = '" & .Rows(i).Item("Urut_Selisih_Lama") & "'"
                                ExecuteTrans(SQL)
                            Next
                        End If
                    End With
                End Using


                SQL = "select No_faktur, Kode_Kategori_Biaya_Import, selisih_Baru "
                SQL = SQL & "from avg_Kategori_Biaya_import where Selisih_baru <> 0 and No_Faktur = '" & TxtNo_Faktur.Text & "' and Kode_Perusahaan = '" & KodePerusahaan & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1

                                SQL = "insert into Detail_Selisih_Transaksi_Biaya_Import_By_Kategori"
                                SQL = SQL & "(Kode_perusahaan, No_faktur, Kode_Kategori_Biaya_Import, selisih, Kode_Supplier) values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & .Rows(i).Item("No_faktur") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Kode_Kategori_Biaya_Import") & "', '" & .Rows(i).Item("selisih_Baru") & "', '" & kd_sup & "')"
                                ExecuteTrans(SQL)
                            Next
                        End If
                    End With
                End Using

                SQL = " select a.id_rencana, a.No_Faktur, a.Kode_Kategori_Biaya_Import, sum(a.Biaya) as Biaya, sum(a.Total) as Total, isnull(b.Flag_Masuk_HPP,'T') as Flag_Masuk_HPP "
                SQL = SQL & "from Detail_Transaksi_Biaya_Import a, Kategori_Biaya_Import b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Kategori_Biaya_Import = b.Kode_Kategori_Biaya_Import "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & TxtNo_Faktur.Text & "' "
                SQL = SQL & "and a.Flag_Average_Kategori = 'T' "
                SQL = SQL & "group by a.id_rencana, a.No_Faktur, a.Kode_Kategori_Biaya_Import, b.Flag_Masuk_HPP "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1

                                SQL = "insert into avg_Kategori_Biaya_import(Kode_Perusahaan, No_Faktur, Kode_Kategori_Biaya_Import, "
                                SQL = SQL & "Avg_Biaya, Total_Avg_Biaya, Id_Rencana, Flag_Masuk_HPP) Values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & .Rows(i).Item("No_Faktur") & "', '" & .Rows(i).Item("Kode_Kategori_Biaya_Import") & "',  "
                                SQL = SQL & "'" & .Rows(i).Item("Biaya") & "', '" & .Rows(i).Item("Total") & "', '" & .Rows(i).Item("id_rencana") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Flag_Masuk_HPP") & "')"
                                ExecuteTrans(SQL)
                            Next
                        End If
                    End With
                End Using

            Else
                'SQL = "update Detail_Transaksi_Biaya_Import set avg_biaya = biaya, total_avg_biaya = total where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "no_faktur = '" & TxtNo_Faktur.Text.Trim & "'"
                'ExecuteTrans(SQL)

                SQL = " select a.id_rencana, a.No_Faktur, a.Kode_Kategori_Biaya_Import, sum(a.Biaya) as Biaya, sum(a.Total) as Total, isnull(b.Flag_Masuk_HPP,'T') as Flag_Masuk_HPP "
                SQL = SQL & "from Detail_Transaksi_Biaya_Import a, Kategori_Biaya_Import b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Kategori_Biaya_Import = b.Kode_Kategori_Biaya_Import "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & TxtNo_Faktur.Text & "' "
                SQL = SQL & "group by a.id_rencana, a.No_Faktur, a.Kode_Kategori_Biaya_Import, b.Flag_Masuk_HPP "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1

                                SQL = "insert into avg_Kategori_Biaya_import(Kode_Perusahaan, No_Faktur, Kode_Kategori_Biaya_Import, "
                                SQL = SQL & "Avg_Biaya, Total_Avg_Biaya, Id_Rencana, Flag_Masuk_HPP) Values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & .Rows(i).Item("No_Faktur") & "', '" & .Rows(i).Item("Kode_Kategori_Biaya_Import") & "',  "
                                SQL = SQL & "'" & .Rows(i).Item("Biaya") & "', '" & .Rows(i).Item("Total") & "', '" & .Rows(i).Item("id_rencana") & "', '" & .Rows(i).Item("Flag_Masuk_HPP") & "') "
                                ExecuteTrans(SQL)
                            Next
                        End If
                    End With
                End Using

                SQL = "update transaksi_biaya_import set Grand_Biaya = '" & HilangkanTanda(txtTotalBiaya.Text) & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_faktur = '" & TxtNo_Faktur.Text.Trim & "'"
                ExecuteTrans(SQL)
            End If


            SQL = "select Flag_Gabungan from rencana_order where "
            SQL = SQL & "Id_rencana = '" & TxtId_Rencana.Text & "'"
            Using Dr2 = OpenTrans(SQL)
                If Dr2.Read Then
                    If General_Class.CekNULL(Dr2("Flag_Gabungan")) = "Y" Then
                        Dr2.Close()
                        SQL = "select a.id_rencana from rencana_order a, rencana_order_gabungan b where "
                        SQL = SQL & "a.id_rencana = b.Id_rencana and b.Id_rencana_induk = '" & TxtId_Rencana.Text & "'"
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For i As Integer = 0 To .Rows.Count - 1
                                        SQL = "update rencana_Order set Flag_Sudah_Transaksi = 'Y' "
                                        SQL = SQL & " where Id_Rencana = '" & .Rows(i).Item("id_rencana") & "' and Flag_Sudah_Transaksi Is Null"
                                        ExecuteTrans(SQL)
                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using
                    Else
                        Dr2.Close()
                        SQL = "update rencana_Order set Flag_Sudah_Transaksi = 'Y' "
                        SQL = SQL & " where Id_Rencana = '" & TxtId_Rencana.Text.Trim & "' and Flag_Sudah_Transaksi Is Null"
                        ExecuteTrans(SQL)
                    End If
                Else
                    Dr2.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            'SQL = "Update Rencana_order set Flag_Sudah_Transaksi = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "Id_Rencana = '" & TxtId_Rencana.Text & "' and Flag_Sudah_Transaksi Is Null"
            'ExecuteTrans(SQL)

            Cmd.Transaction.Commit()

            CloseConn()

            MessageBox.Show("Data Tersimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
        kosong_MUA()
    End Sub

    Private Sub TxtSupplier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSupplier.TextChanged
        'Try
        '    OpenConn()
        '    DataGridView1.Rows.Clear()
        '    Dim no As Integer = 0

        '    SQL = "select a.kode_kategori_biaya_import, b.kode_biaya, b.keterangan , b.biaya, c.total_persen, b.kode_mata_uang, c.kode_kontainer from kategori_biaya_import a, biaya_import b, rencana_order c "
        '    SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.kode_kategori_biaya_import = b.kode_kategori_biaya_import and b.lokasi = c.lokasi and b.kode_kontainer = c.kode_kontainer and b.kode_kontainer = '" & TxtContainer.Text & "' and b.lokasi = '" & CmbLokasi.Text & "' and c.id_rencana = '" & TxtId_Rencana.Text & "' order by a.urutan,b.keterangan"
        '    Ds = BindingTrans(SQL)
        '    For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
        '        With Ds.Tables("MyTable").Rows(i)
        '            DataGridView1.Rows.Add(1)

        '            Dim kontainer As Integer = (.Item("total_persen")) / 100
        '            Dim jumlah As Integer = kontainer * 100
        '            Dim selisih As Integer = (.Item("total_persen")) - jumlah
        '            Dim a As Double
        '            Dim jmlhkonte As Integer = 0
        '            'If selisih = 0 Or selisih <= 20 Then
        '            '    a = kontainer * .Item("Biaya")
        '            '    jmlhkonte = kontainer
        '            'Else
        '            '    a = (kontainer + 1) * .Item("Biaya")
        '            '    jmlhkonte = kontainer + 1
        '            'End If

        '            DataGridView1.Rows.Item(no).Cells(0).Value = .Item("kode_kategori_biaya_import")
        '            DataGridView1.Rows.Item(no).Cells(1).Value = .Item("kode_biaya")
        '            DataGridView1.Rows.Item(no).Cells(2).Value = .Item("Keterangan")
        '            If selisih = 0 Or selisih <= 20 Then
        '                a = kontainer * .Item("Biaya")
        '                DataGridView1.Rows.Item(no).Cells(3).Value = Format(kontainer, "N0")
        '            Else
        '                a = kontainer * .Item("Biaya")
        '                DataGridView1.Rows.Item(no).Cells(3).Value = Format(kontainer + 1, "N2")
        '            End If
        '            DataGridView1.Rows.Item(no).Cells(4).Value = Format(.Item("Biaya"), "N2")
        '            DataGridView1.Rows.Item(no).Cells(5).Value = Format(a, "N2")

        '            no = no + 1
        '        End With
        '    Next

        '    CloseConn()
        'Catch ex As Exception
        '    DataGridView1.Rows.Clear()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub

        'End Try
    End Sub

    Private Sub TxtContainer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtContainer.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Get_Isi_Listview(DataGridView1.CurrentRow.Index)


        If IsNumeric(LvJumlahKontainer) = False Or Val(LvJumlahKontainer) < 0 Then
            DataGridView1.CurrentRow.Cells(cellJumlahKontainer).Value = 0
        ElseIf IsNumeric(LvBiaya) = False Or Val(LvBiaya) < 0 Then
            DataGridView1.CurrentRow.Cells(cellBiaya).Value = 0
        ElseIf IsNumeric(LvNilai2) = False Or Val(LvNilai2) < 0 Then
            DataGridView1.CurrentRow.Cells(cellNilai2).Value = 0
        End If

        If LvPerhitungan <> "C" And LvPerhitungan <> "I" Then
            DataGridView1.CurrentRow.Cells(cellNilai2).Value = 0
        End If

        Get_Isi_Listview(DataGridView1.CurrentRow.Index)

        Dim Edit As Integer
        Dim EditMUA As Double = 0
        If LvPerhitungan = "A" Then
            Edit = HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya) * HilangkanTanda(LvJumlahKontainer)
            EditMUA = HilangkanTanda(LvBiaya) * HilangkanTanda(LvJumlahKontainer)
        ElseIf LvPerhitungan = "B" Then
            Edit = HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya)
            EditMUA = HilangkanTanda(LvBiaya)
        ElseIf LvPerhitungan = "C" Then
            Edit = (Val(Berat.Text) * (HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya))) + HilangkanTanda(LvNilai2)
            EditMUA = (Val(Berat.Text) * (HilangkanTanda(LvBiaya))) + HilangkanTanda(LvNilai2)
        ElseIf LvPerhitungan = "D" Then
            Edit = (HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya)) * Val(JumlahHari.Text)
            EditMUA = (HilangkanTanda(LvBiaya)) * Val(JumlahHari.Text)
        ElseIf LvPerhitungan = "E" Or LvPerhitungan = "F" Then
            Edit = HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya) * HilangkanTanda(LvJumlahKontainer)
            EditMUA = HilangkanTanda(LvBiaya) * HilangkanTanda(LvJumlahKontainer)
        ElseIf LvPerhitungan = "G" Then
            Edit = HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya) * HilangkanTanda(LvJumlahKontainer)
            EditMUA = HilangkanTanda(LvBiaya) * HilangkanTanda(LvJumlahKontainer)
        ElseIf LvPerhitungan = "H" Then
            Edit = HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya) * HilangkanTanda(LvJumlahKontainer)
            EditMUA = HilangkanTanda(LvBiaya) * HilangkanTanda(LvJumlahKontainer)
        ElseIf LvPerhitungan = "I" Then
            Edit = (((Val(Total_Dec.Text) * HilangkanTanda(LvBiaya)) / 100) + HilangkanTanda(LvNilai2)) * HilangkanTanda(LvKurs)
            EditMUA = ((Val(Total_Dec.Text) * HilangkanTanda(LvBiaya)) / 100) + HilangkanTanda(LvNilai2)
        End If

        DataGridView1.CurrentRow.Cells(cellBiaya).Value = Format(Val(HilangkanTanda(LvBiaya)), "N2")
        DataGridView1.CurrentRow.Cells(cellNilai2).Value = Format(Val(HilangkanTanda(LvNilai2)), "N2")
        DataGridView1.CurrentRow.Cells(cellTotal).Value = Format(Edit, "N2")
        DataGridView1.CurrentRow.Cells(cellTotalMUA).Value = Format(EditMUA, "N2")
        HitungGrand()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        dari_tombol_cari = True
        If TxtFlag_Group.Text = "Y" Then
            Tampil_Data_Group()
        Else
            Tampil_Data()
        End If

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Mata Uang Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        ElseIf ComboBox5.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Kurs Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox5.Focus()
            Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Nilai Kurs Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus()
            Exit Sub
        End If

        For index As Integer = 0 To ListView2.Items.Count - 1

            If ListView2.Items(index).SubItems(0).Text = ComboBox1.Text And ListView2.Items(index).SubItems(2).Text = ComboBox5.Text Then
                ListView2.Items(index).SubItems(1).Text = TextBox1.Text

                ComboBox1.SelectedIndex = -1
                ComboBox5.SelectedIndex = -1
                TextBox1.Text = ""

                Exit Sub
            End If

        Next

        Dim Lvw As ListViewItem
        Lvw = ListView2.Items.Add(ComboBox1.Text)
        Lvw.SubItems.Add(TextBox1.Text)
        Lvw.SubItems.Add(ComboBox5.Text)
        ComboBox1.SelectedIndex = -1
        ComboBox5.SelectedIndex = -1
        TextBox1.Text = ""
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        'ListView1.Columns.Add("Lokasi", 140, HorizontalAlignment.Left) '0
        'ListView1.Columns.Add("Kode Master", 130, HorizontalAlignment.Center) '1
        'ListView1.Columns.Add("Kode Kategori", 130, HorizontalAlignment.Center) '2
        'ListView1.Columns.Add("Kode Biaya", 130, HorizontalAlignment.Center) '3
        'ListView1.Columns.Add("Nama Biaya", 130, HorizontalAlignment.Left) '4
        'ListView1.Columns.Add("Kontainer", 130, HorizontalAlignment.Left) '5
        'ListView1.Columns.Add("Kode Perusahaan Biaya Import", 130, HorizontalAlignment.Center) '6
        'ListView1.Columns.Add("Nama Perusahaan", 130, HorizontalAlignment.Left) '7
        'ListView1.Columns.Add("Kode Gudang", 130, HorizontalAlignment.Left) '8
        'ListView1.Columns.Add("Tabel asal", 130, HorizontalAlignment.Left) '9


        For index As Integer = 0 To DataGridView1.Rows.Count - 1

            If DataGridView1.Rows.Item(index).Cells(1).Value = ListView1.FocusedItem.Text And _
                DataGridView1.Rows.Item(index).Cells(3).Value = ListView1.FocusedItem.SubItems(3).Text And _
                DataGridView1.Rows.Item(index).Cells(5).Value = ListView1.FocusedItem.SubItems(5).Text And _
                DataGridView1.Rows.Item(index).Cells(7).Value = ListView1.FocusedItem.SubItems(6).Text Then

                MessageBox.Show("Data Sudah Ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next


        Try
            OpenConn()

            DataGridView1.Rows.Add(1)
            Dim index As Integer = DataGridView1.Rows.Count - 1
            DataGridView1.Rows.Item(index).Cells(0).Value = TxtId_Rencana.Text
            DataGridView1.Rows.Item(index).Cells(1).Value = ListView1.FocusedItem.Text
            DataGridView1.Rows.Item(index).Cells(2).Value = ListView1.FocusedItem.SubItems(2).Text
            DataGridView1.Rows.Item(index).Cells(3).Value = ListView1.FocusedItem.SubItems(3).Text
            DataGridView1.Rows.Item(index).Cells(4).Value = ListView1.FocusedItem.SubItems(4).Text
            DataGridView1.Rows.Item(index).Cells(5).Value = ListView1.FocusedItem.SubItems(5).Text
            DataGridView1.Rows.Item(index).Cells(6).Value = TxtJumlah_conte.Text

            DataGridView1.Rows.Item(index).Cells(7).Value = ListView1.FocusedItem.SubItems(6).Text
            DataGridView1.Rows.Item(index).Cells(8).Value = ListView1.FocusedItem.SubItems(7).Text

            Dim kurs As Double = 0

            If id_rencana_group.Trim.Length = 0 Then
                id_rencana_group = "'" & TxtId_Rencana.Text & "'"
            End If

            Dim hitungMUA As Double = 0
            Dim Untuk_gudang As String = ""

            If ListView1.FocusedItem.SubItems(9).Text.Trim.ToUpper = "BIAYA_IMPORT_DETAIL2" Then
                Untuk_gudang = "and Y.Kode_Gudang=A.Kode_Gudang "
            End If

            SQL = "select a.*, isnull((select datediff(day,format(Tanggal_Bongkar, 'yyyy-MM-dd'),format(DATEADD(dd, Free_Time, Eta), 'yyyy-MM-dd')) from ubah_status_otw a, Bongkar_Import b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_Rencana = b.ID_Rencana and A.id_rencana ='" & TxtId_Rencana.Text & "'),0) as Selisih_Tanggal, "

            SQL = SQL & "round(isnull(( select sum(Z.Persentase) from submit_PO X, Kontainer_Masuk_Per_Lokasi Y, Kontainer_masuk_Per_jenis Z "
            SQL = SQL & "where(Y.Kode_Perusahaan = X.Kode_Perusahaan And X.No_faktur = Y.No_Faktur And Y.Lokasi_Tujuan = a.Kode_Stock_Owner) "
            SQL = SQL & "and Y.Kode_Perusahaan= Z.Kode_Perusahaan and Y.No_Faktur= Z.No_Faktur and Y.No_Container= Z.No_Container and "
            SQL = SQL & "X.id_rencana in(" & id_rencana_group & ") and X.status is null and Z.Jenis=a.Jns ),0)/100,0) as konte_per_jenis, "

            SQL = SQL & "isnull(( select COUNT(Y.No_Container) from submit_PO X, "
            SQL = SQL & "Kontainer_masuk_Per_Lokasi Y where Y.Kode_Perusahaan = X.Kode_Perusahaan and "
            SQL = SQL & "X.No_faktur = Y.No_Faktur and Y.Lokasi_Tujuan = a.Kode_Stock_Owner and X.id_rencana in(" & id_rencana_group & ")"
            SQL = SQL & "and X.status is null ),0) as konte_per_lokasi,"

            SQL = SQL & "isnull((select case when 'Biaya_Import_Detail'='" & ListView1.FocusedItem.SubItems(9).Text & "' then 0 else "
            SQL = SQL & "isnull((select COUNT(Y.No_Container) from submit_PO X, Kontainer_masuk_Per_Lokasi Y where "
            SQL = SQL & "Y.Kode_Perusahaan = X.Kode_Perusahaan And X.No_faktur = Y.No_Faktur And Y.Lokasi_Tujuan = a.Kode_Stock_Owner "
            SQL = SQL & "and X.id_rencana in(" & id_rencana_group & ") " & Untuk_gudang & " and X.status is null),0) end),0) as konte_per_Gudang, "

            SQL = SQL & "isnull(( select sum(X.Jml_Kontainer) from Log_Penjaluran_Import X where X.id_rencana "
            SQL = SQL & "in(" & id_rencana_group & ") and Warna ='Merah'),0) as Konte_Penjaluran, "

            SQL = SQL & "isnull(( select sum(X.Jml_Kontainer_Karantina) from Penjaluran_Import X where X.id_rencana "
            SQL = SQL & "in(" & id_rencana_group & ")),0) as Konte_Asuransi, "

            SQL = SQL & "isnull(( select sum((y.Jumlah-y.barang_free)*y.Harga_Declare) from submit_PO X, Detail_submit_PO Y where Y.Kode_Perusahaan "
            SQL = SQL & "= X.Kode_Perusahaan and X.No_faktur = Y.No_Faktur and X.id_rencana "
            SQL = SQL & "in(" & id_rencana_group & ") and X.status is null ),0) as Total_Declare, d.flag_average "

            SQL = SQL & "from " & ListView1.FocusedItem.SubItems(9).Text & " a, Biaya_Import c, kategori_biaya_import d " 'coding stenly
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = c.Kode_Perusahaan " 'coding stenly
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and a.kode_biaya = c.kode_biaya and c.kode_kategori_biaya_import = d.kode_kategori_biaya_import " 'coding stenly
            SQL = SQL & "and a.Kode_Biaya = '" & ListView1.FocusedItem.SubItems(3).Text & "' and a.Kode_Stock_Owner = '" & ListView1.FocusedItem.SubItems(0).Text & "' "
            SQL = SQL & "and a.Kode_Perusahaan_Biaya_Import = '" & ListView1.FocusedItem.SubItems(6).Text & "' and a.Kode_Kontainer = '" & ListView1.FocusedItem.SubItems(5).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    DataGridView1.Rows.Item(index).Cells(9).Value = Dr("Perhitungan")
                    DataGridView1.Rows.Item(index).Cells(10).Value = Dr("Kode_Mata_Uang")

                    Dim Kategori_Kurs = ""

                    If Dr("Perhitungan") <> "I" Then
                        Kategori_Kurs = "BIAYA"
                    Else
                        Kategori_Kurs = "ASURANSI"
                    End If

                    For index1 As Integer = 0 To ListView2.Items.Count - 1

                        If Dr("Kode_Mata_Uang") = ListView2.Items(index1).SubItems(0).Text And Kategori_Kurs = ListView2.Items(index1).SubItems(2).Text Then
                            kurs = ListView2.Items(index1).SubItems(1).Text
                        End If

                    Next

                    If kurs = 0 Then
                        MessageBox.Show("Kurs Mata Uang " & Dr("Kode_Mata_Uang") & " Pada Kategori " & Kategori_Kurs & " Tidak Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Dr.Close()
                        CloseConn()
                        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                        Exit Sub
                    End If

                    DataGridView1.Rows.Item(index).Cells(11).Value = Format(kurs, "N2")

                    If Dr("Perhitungan") <> "I" Then
                        DataGridView1.Rows.Item(index).Cells(12).Value = Format(Dr("Nilai"), "N2")
                    Else
                        DataGridView1.Rows.Item(index).Cells(12).Value = Format(Dr("Nilai"), "N3")
                    End If


                    DataGridView1.Rows.Item(index).Cells(13).Value = Format(Dr("Nilai_2"), "N2")
                    If Dr("Perhitungan") = "A" Then
                        hitung = kurs * Dr("Nilai") * Val(TxtJumlah_conte.Text)
                        hitungMUA = Dr("Nilai") * Val(TxtJumlah_conte.Text)
                    ElseIf Dr("Perhitungan") = "B" Then
                        hitung = kurs * Dr("Nilai")
                        hitungMUA = Dr("Nilai")
                    ElseIf Dr("Perhitungan") = "C" Then
                        hitung = (Val(Berat.Text) * (kurs * Dr("Nilai"))) + Dr("Nilai_2")
                        hitungMUA = (Val(Berat.Text) * (Dr("Nilai"))) + Dr("Nilai_2")
                    ElseIf Dr("Perhitungan") = "D" Then
                        hitung = (kurs * Dr("Nilai")) * Dr("Selisih_Tanggal")
                        hitungMUA = (Dr("Nilai")) * Dr("Selisih_Tanggal")
                        JumlahHari.Text = Dr("Selisih_Tanggal")
                    ElseIf Dr("Perhitungan") = "E" Then
                        Dim jml_konte As Integer = 0
                        If Dr("Konte_Penjaluran") < Dr("Min") Then
                            MessageBox.Show("Jumlah Container kurang dari jumlah minimal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                            Dr.Close()
                            CloseConn()
                            Exit Sub
                        ElseIf Dr("Konte_Penjaluran") < Dr("Max") Then
                            jml_konte = Dr("Konte_Penjaluran") - (Dr("Min") - 1)
                        ElseIf Dr("Konte_Penjaluran") >= Dr("Max") Then
                            jml_konte = Dr("Max") - (Dr("Min") - 1)
                        Else
                            MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                            Dr.Close()
                            CloseConn()
                            Exit Sub
                        End If

                        hitung = (kurs * Dr("Nilai")) * jml_konte
                        hitungMUA = (Dr("Nilai")) * jml_konte
                        DataGridView1.Rows.Item(index).Cells(6).Value = jml_konte
                    ElseIf Dr("Perhitungan") = "F" Then
                        Dim jml_konte As Integer = 0
                        If Val(TxtJumlah_conte.Text) < Dr("Min") Then
                            MessageBox.Show("Jumlah Container kurang dari jumlah minimal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                            Dr.Close()
                            CloseConn()
                            Exit Sub
                        ElseIf Val(TxtJumlah_conte.Text) < Dr("Max") Then
                            jml_konte = Val(TxtJumlah_conte.Text) - (Dr("Min") - 1)
                        ElseIf Val(TxtJumlah_conte.Text) >= Dr("Max") Then
                            jml_konte = Dr("Max") - (Dr("Min") - 1)
                        Else
                            MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                            Dr.Close()
                            CloseConn()
                            Exit Sub
                        End If

                        hitung = (kurs * Dr("Nilai")) * jml_konte
                        hitungMUA = (Dr("Nilai")) * jml_konte
                        DataGridView1.Rows.Item(index).Cells(6).Value = jml_konte
                    ElseIf Dr("Perhitungan") = "G" Then

                        Dim Konte As Integer = 0

                        If General_Class.CekNULL(Dr("Jns")) = "WET" Or General_Class.CekNULL(Dr("Jns")) = "DRY" Then
                            Konte = Dr("konte_per_jenis")
                        ElseIf ListView1.FocusedItem.SubItems(9).Text.Trim.ToUpper = "BIAYA_IMPORT_DETAIL2" And General_Class.CekNULL(Dr("Jns")) = "ALL" Then
                            Konte = Dr("konte_per_Gudang")
                        Else
                            Konte = Dr("konte_per_lokasi")
                        End If

                        hitung = (kurs * Dr("Nilai")) * Konte
                        hitungMUA = (Dr("Nilai")) * Konte
                        DataGridView1.Rows.Item(index).Cells(6).Value = Konte
                    ElseIf Dr("Perhitungan") = "H" Then

                        hitung = kurs * Dr("Nilai") * Dr("Konte_Asuransi")
                        hitungMUA = Dr("Nilai") * Dr("Konte_Asuransi")
                        DataGridView1.Rows.Item(index).Cells(6).Value = Dr("Konte_Asuransi")
                    ElseIf Dr("Perhitungan") = "I" Then

                        Total_Dec.Text = Dr("Total_Declare")
                        hitung = (((Dr("Total_Declare") * Dr("Nilai")) / 100) + Dr("Nilai_2")) * kurs
                        hitungMUA = ((Dr("Total_Declare") * Dr("Nilai")) / 100) + Dr("Nilai_2")
                    Else
                        MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                        Dr.Close()
                        CloseConn()
                        Exit Sub
                    End If

                    DataGridView1.Rows.Item(index).Cells(14).Value = Format(hitung, "N2")
                    DataGridView1.Rows.Item(index).Cells(15).Value = ListView1.FocusedItem.SubItems(1).Text
                    DataGridView1.Rows.Item(index).Cells(16).Value = General_Class.CekNULL(Dr("Jns"))
                    DataGridView1.Rows.Item(index).Cells(17).Value = General_Class.CekNULL(Dr("Flag_average")) 'coding stenly
                    DataGridView1.Rows.Item(index).Cells(18).Value = Format(hitungMUA, "N2")
                    DataGridView1.Rows.Item(index).Cells(19).Value = "T"

                    'ambil data master
                    If arrMaster.Count = 0 Then
                        arrMaster.Add(ListView1.FocusedItem.SubItems(1).Text)
                    End If

                    Dim ada_data As Boolean = True
                    For index1 As Integer = 0 To arrMaster.Count - 1
                        'FREIGHT 'FREIGHT
                        If arrMaster.Item(index1) = ListView1.FocusedItem.SubItems(1).Text Then
                            ada_data = False
                        End If

                    Next

                    If ada_data = True Then
                        arrMaster.Add(ListView1.FocusedItem.SubItems(1).Text)
                    End If



                    'ambil data Kategori
                    If DataGridView2.Rows.Count = 0 Then
                        DataGridView2.Rows.Add(1)
                        DataGridView2.Rows.Item(0).Cells(0).Value = ListView1.FocusedItem.SubItems(2).Text
                        DataGridView2.Rows.Item(0).Cells(1).Value = 0
                    End If


                    Dim ada_data2 As Boolean = True
                    For index1 As Integer = 0 To DataGridView2.Rows.Count - 1
                        Get_Isi_Listview2(index1)
                        If LvKategori2 = ListView1.FocusedItem.SubItems(2).Text Then
                            ada_data2 = False
                        End If

                    Next

                    If ada_data2 = True Then
                        DataGridView2.Rows.Add(1)
                        Dim index2 As Integer = DataGridView2.Rows.Count - 1
                        DataGridView2.Rows.Item(index2).Cells(0).Value = ListView1.FocusedItem.SubItems(2).Text
                        DataGridView2.Rows.Item(index2).Cells(1).Value = 0
                    End If

                    'ambil data perusahaan
                    If DataGridView3.Rows.Count = 0 Then
                        DataGridView3.Rows.Add(1)

                        DataGridView3.Rows.Item(0).Cells(0).Value = ListView1.FocusedItem.SubItems(6).Text
                        DataGridView3.Rows.Item(0).Cells(1).Value = Dr("Kode_Mata_Uang")
                        DataGridView3.Rows.Item(0).Cells(2).Value = 0
                    End If


                    Dim ada_data3 As Boolean = True
                    For index1 As Integer = 0 To DataGridView3.Rows.Count - 1
                        Get_Isi_Listview3(index1)
                        If LvPerusahaan3 = ListView1.FocusedItem.SubItems(6).Text And LvMataUang3 = Dr("Kode_Mata_Uang") Then
                            ada_data3 = False
                        End If

                    Next

                    If ada_data3 = True Then
                        DataGridView3.Rows.Add(1)
                        Dim index2 As Integer = DataGridView3.Rows.Count - 1
                        DataGridView3.Rows.Item(index2).Cells(0).Value = ListView1.FocusedItem.SubItems(6).Text
                        DataGridView3.Rows.Item(index2).Cells(1).Value = Dr("Kode_Mata_Uang")
                        DataGridView3.Rows.Item(index2).Cells(2).Value = 0
                    End If
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Tidak Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungGrand()
    End Sub

    Private Sub HapusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HapusToolStripMenuItem.Click

        If DataGridView1.SelectedCells.Count = 0 Or DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("Pilih Data Terlebih Dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Get_Isi_Listview(DataGridView1.CurrentRow.Index)

        If LvValidasi = "Y" Then
            MessageBox.Show("Data Ini Tidak Bisa Dihapus. Karena dari Validasi . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DataGridView1.Rows.Remove(DataGridView1.CurrentRow)


        For index As Integer = DataGridView2.Rows.Count - 1 To 0 Step -1
            Get_Isi_Listview2(index)
            Dim ada_data As Boolean = True

            For index2 As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(index2)

                If LvKategori2 = LvKodeKategori Then
                    ada_data = False
                End If

            Next

            If ada_data = True Then
                DataGridView2.Rows.RemoveAt(index)
            End If

        Next

        For index As Integer = DataGridView3.Rows.Count - 1 To 0 Step -1
            Get_Isi_Listview3(index)
            Dim ada_data3 As Boolean = True

            For index2 As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(index2)

                If LvPerusahaan3 = LvKodePerusahaanBiaya And LvMataUang3 = LvMataUang Then
                    ada_data3 = False
                End If

            Next

            If ada_data3 = True Then
                DataGridView3.Rows.RemoveAt(index)
            End If

        Next


        For index As Integer = arrMaster.Count - 1 To 0 Step -1
            Dim ada_data2 As Boolean = True

            For index2 As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(index2)

                If arrMaster.Item(index) = LvMaster Then
                    ada_data2 = False
                End If

            Next

            If ada_data2 = True Then
                arrMaster.RemoveAt(index)
            End If

        Next


        HitungGrand()
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("Tidak Ada Data yang Bisa Dimasukkan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "delete from detail_transaksi_biaya_import_sementara where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "userid = '" & UserID & "'"
            ExecuteTrans(SQL)
            Dim jns As String = ""
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(i)

                If LvJns = "" Then
                    jns = "NULL"
                Else
                    jns = "'" & LvJns & "'"
                End If

                SQL = "insert into detail_transaksi_biaya_import_sementara (kode_perusahaan, userid, Id_Rencana, Kode_stock_Owner, Kode_Kategori, Kode_Biaya, Nama_Biaya, "

                SQL = SQL & "Kode_Kontainer, jml_kontainer, Kode_Perusahaan_Biaya_Import, Nama_Perusahaan, Jenis_Perhitungan, "

                SQL = SQL & "Mata_Uang, Kurs, biaya, Nilai_2, Total, Kode_Master_Kategori_Biaya_Import, Jns,flag_average_kategori, TotalMUA, Flag_Validasi_Biaya) values( " 'coding stenly
                SQL = SQL & "'" & KodePerusahaan & "', '" & UserID & "', "
                SQL = SQL & "'" & LvID & "', '" & Lvlokasi & "', '" & LvKodeKategori & "', '" & LvKodeBiaya & "', '" & LvNamaBiaya & "', "

                SQL = SQL & "'" & LvKontainer & "', '" & LvJumlahKontainer & "', '" & LvKodePerusahaanBiaya & "', '" & LvNamaPerusahaan & "', '" & LvPerhitungan & "', "

                SQL = SQL & "'" & LvMataUang & "', " & HilangkanTanda(LvKurs) & ", "
                SQL = SQL & HilangkanTanda(LvBiaya) & ", " & HilangkanTanda(LvNilai2) & ", " & HilangkanTanda(LvTotal) & ",'" & LvMaster & "'," & jns & ", '" & LvFlagAvg & "','" & HilangkanTanda(LvTotalMUA) & "', '" & LvValidasi & "')" 'coding stenly
                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()

            CloseConn()

            MessageBox.Show("Data Tersimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Transaksi_Biaya_import_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            OpenConn()


            Dim index As Integer = 0
            DataGridView1.Rows.Clear()
            DataGridView2.Rows.Clear()
            arrMaster.Clear()

            If TxtContainer.Text.Trim.Length = 0 Then
                MessageBox.Show("Masukkan Rencana Order Terlebih Dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            SQL = "select * from detail_transaksi_biaya_import_sementara where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & TxtId_Rencana.Text & "' and userid ='" & UserID & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    DataGridView1.Rows.Add(1)
                    DataGridView1.Rows.Item(index).Cells(0).Value = Dr("id_rencana")
                    DataGridView1.Rows.Item(index).Cells(1).Value = Dr("Kode_Stock_Owner")
                    DataGridView1.Rows.Item(index).Cells(2).Value = Dr("Kode_Kategori")
                    DataGridView1.Rows.Item(index).Cells(3).Value = Dr("Kode_biaya")
                    DataGridView1.Rows.Item(index).Cells(4).Value = Dr("Nama_Biaya")
                    DataGridView1.Rows.Item(index).Cells(5).Value = Dr("Kode_Kontainer")
                    DataGridView1.Rows.Item(index).Cells(6).Value = Dr("Jml_Kontainer")
                    DataGridView1.Rows.Item(index).Cells(7).Value = Dr("Kode_Perusahaan_Biaya_Import")
                    DataGridView1.Rows.Item(index).Cells(8).Value = Dr("Nama_Perusahaan")
                    DataGridView1.Rows.Item(index).Cells(9).Value = Dr("Jenis_Perhitungan")
                    DataGridView1.Rows.Item(index).Cells(10).Value = Dr("Mata_Uang")
                    DataGridView1.Rows.Item(index).Cells(11).Value = Format(Dr("Kurs"), "N2")

                    If Dr("Jenis_Perhitungan") <> "I" Then
                        DataGridView1.Rows.Item(index).Cells(12).Value = Format(Dr("Biaya"), "N2")
                    Else
                        DataGridView1.Rows.Item(index).Cells(12).Value = Format(Dr("Biaya"), "N3")
                    End If

                    DataGridView1.Rows.Item(index).Cells(13).Value = Format(Dr("Nilai_2"), "N2")
                    DataGridView1.Rows.Item(index).Cells(14).Value = Format(Dr("Total"), "N2")
                    DataGridView1.Rows.Item(index).Cells(15).Value = Dr("Kode_Master_Kategori_biaya_import")
                    DataGridView1.Rows.Item(index).Cells(16).Value = General_Class.CekNULL(Dr("Jns"))
                    DataGridView1.Rows.Item(index).Cells(17).Value = General_Class.CekNULL(Dr("flag_average_kategori")) 'coding stenly
                    DataGridView1.Rows.Item(index).Cells(18).Value = General_Class.CekNULL(Dr("TotalMUA"))
                    DataGridView1.Rows.Item(index).Cells(19).Value = General_Class.CekNULL(Dr("Flag_Validasi_Biaya"))

                    'ambil data master
                    If arrMaster.Count = 0 Then
                        arrMaster.Add(Dr("Kode_Master_Kategori_biaya_import"))
                    End If

                    Dim ada_data As Boolean = True
                    For index1 As Integer = 0 To arrMaster.Count - 1

                        If arrMaster.Item(index1) = Dr("Kode_Master_Kategori_biaya_import") Then
                            ada_data = False
                        End If

                    Next

                    If ada_data = True Then
                        arrMaster.Add(Dr("Kode_Master_Kategori_biaya_import"))
                    End If



                    'ambil data kategori
                    If DataGridView2.Rows.Count = 0 Then
                        DataGridView2.Rows.Add(1)
                        DataGridView2.Rows.Item(0).Cells(0).Value = Dr("Kode_Kategori")
                        DataGridView2.Rows.Item(0).Cells(1).Value = 0
                    End If


                    Dim ada_data2 As Boolean = True
                    For index1 As Integer = 0 To DataGridView2.Rows.Count - 1
                        Get_Isi_Listview2(index1)
                        If LvKategori2 = Dr("Kode_Kategori") Then
                            ada_data2 = False
                        End If

                    Next

                    If ada_data2 = True Then
                        DataGridView2.Rows.Add(1)
                        Dim index2 As Integer = DataGridView2.Rows.Count - 1
                        DataGridView2.Rows.Item(index2).Cells(0).Value = Dr("Kode_Kategori")
                        DataGridView2.Rows.Item(index2).Cells(1).Value = 0
                    End If

                    'ambil data perusahaan
                    If DataGridView3.Rows.Count = 0 Then
                        DataGridView3.Rows.Add(1)
                        DataGridView3.Rows.Item(0).Cells(0).Value = Dr("Kode_Perusahaan_Biaya_Import")
                        DataGridView3.Rows.Item(0).Cells(1).Value = Dr("Mata_Uang")
                        DataGridView3.Rows.Item(0).Cells(2).Value = 0
                    End If


                    Dim ada_data3 As Boolean = True
                    For index1 As Integer = 0 To DataGridView3.Rows.Count - 1
                        Get_Isi_Listview3(index1)
                        If LvPerusahaan3 = Dr("Kode_Perusahaan_Biaya_Import") And LvMataUang3 = Dr("Mata_Uang") Then
                            ada_data3 = False
                        End If

                    Next

                    If ada_data3 = True Then
                        DataGridView3.Rows.Add(1)
                        Dim index2 As Integer = DataGridView3.Rows.Count - 1
                        DataGridView3.Rows.Item(index2).Cells(0).Value = Dr("Kode_Perusahaan_Biaya_Import")
                        DataGridView3.Rows.Item(index2).Cells(1).Value = Dr("Mata_Uang")
                        DataGridView3.Rows.Item(index2).Cells(2).Value = 0
                    End If

                    index += 1

                Loop
            End Using

            If index = 0 Then
                MessageBox.Show("Tidak Ada Data!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungGrand()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox5.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub TxtId_Rencana_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtId_Rencana.TextChanged

    End Sub

    Private Sub ComboBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub
End Class