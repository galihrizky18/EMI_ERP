Public Class Transaksi_Biaya_import3
    Dim arrcari, arrAkun As New ArrayList
    Dim arrMaster As New ArrayList

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
    Dim LvMataUangBilling As String
    Dim LvBiayaBilling As String
    Dim LvFlagAvg As String 'coding stenly
    Dim LvValidasi As String

    Dim LvPerusahaan3 As String
    Dim LvMataUang3 As String
    Dim LvTotal3 As String

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
    Dim cellFlagAvg As Integer 'coding stenly
    Dim cellValidasi As String

    Public cellMataUangBilling As Integer
    Public cellBiayaBilling As Integer

    Dim cellPerusahaan3 As Integer
    Dim cellMataUang3 As Integer
    Dim cellTotal3 As Integer

    Dim id_rencana_group As String = ""
    Dim lokasi_group As String = ""
    Dim Konte_group As Integer = 0

    Dim err As Boolean = False
    Dim dari_tombol_cari As Boolean = False
    Dim hitung As Double
    Dim _filter_tambahan As String = "and isnull((select X.Id_rencana_induk from rencana_order_gabungan X where ro.Flag_Gabungan = 'Y' and ro.ID_Rencana = X.ID_Rencana ),ro.id_rencana) = ro.id_rencana and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw = 'Y' and ro.Flag_Draft = 'Y' and ro.Flag_Final = 'Y' and ro.Flag_Kirim = 'Y' and ro.Flag_Finish = 'Y' and ro.flag_kapal_tiba = 'Y' and ro.flag_penjaluran = 'Y' and ro.flag_sppb = 'Y' and ro.flag_tarik_kontainer = 'Y' and ro.flag_bongkar = 'Y' and ro.flag_sudah_transaksi3 is null "

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
        LvMataUangBilling = DataGridView1.Rows(No_Index).Cells(16).Value.ToString : cellMataUangBilling = 16 
        LvBiayaBilling = DataGridView1.Rows(No_Index).Cells(17).Value.ToString : cellBiayaBilling = 17
        LvFlagAvg = DataGridView1.Rows(No_Index).Cells(18).Value.ToString : cellFlagAvg = 18 'coding stenly
        LvValidasi = DataGridView1.Rows(No_Index).Cells(19).Value.ToString : cellValidasi = 19
    End Sub

    Public Sub Get_Isi_Listview3(ByVal No_Index As Integer)

        LvPerusahaan3 = DataGridView3.Rows(No_Index).Cells(0).Value.ToString : cellPerusahaan3 = 0
        LvMataUang3 = DataGridView3.Rows(No_Index).Cells(1).Value.ToString : cellMataUang3 = 1
        LvTotal3 = DataGridView3.Rows(No_Index).Cells(2).Value.ToString : cellTotal3 = 1

    End Sub
    Private Sub HitungGrand()
        Dim ttl As Double = 0
        Dim ttlBilling As Double = 0

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Get_Isi_Listview(i)

            ttl = ttl + Val(HilangkanTanda(LvTotal))
            ttlBilling = ttlBilling + (Val(HilangkanTanda(LvJumlahKontainer)) * Val(HilangkanTanda(LvBiayaBilling)))

        Next

        txtTotalBiayaBilling.Text = Format(ttlBilling, "N2")
        txtTotalBiaya.Text = Format(ttl, "N2")
        txtBiayaLama.Text = "0"
        txtGrand.Text = Format(ttl, "N2")

        For i As Integer = 0 To DataGridView3.Rows.Count - 1
            Get_Isi_Listview3(i)
            Dim ttlperusahaan As Double = 0

            For j As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(j)

                If LvPerusahaan3 = LvKodePerusahaanBiaya And LvMataUang3 = LvMataUang Then
                    ttlperusahaan = ttlperusahaan + Val(HilangkanTanda(LvTotal))
                End If

            Next
            DataGridView3.Rows.Item(i).Cells(2).Value = Format(ttlperusahaan, "N0")
        Next
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
            SQL = SQL & "a.id_rencana='" & TxtId_Rencana.Text & "' and a.flag_validasi='Y' and asal_form =3 "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")

                    For i As Integer = 0 To .Rows.Count - 1
                        Dim asal As String = ""
                        Dim nilai As Double = .Rows(i).Item("Perusahaan_" & .Rows(i).Item("Kode") & "_PCS")
                        If .Rows(i).Item("Asal_form") = "3" Then
                            asal = "BIAYA_IMPORT_DETAIL3"
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

                                'For index1 As Integer = 0 To ListView2.Items.Count - 1

                                '    If Dr("Kode_Mata_Uang") = ListView2.Items(index1).SubItems(0).Text Then
                                kurs = 1 'ListView2.Items(index1).SubItems(1).Text
                                '    End If

                                'Next

                                If kurs = 0 Then
                                    MessageBox.Show("Kurs Mata Uang " & Dr("Kode_Mata_Uang") & " Tidak Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Dr.Close()
                                    CloseConn()
                                    DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
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
                                    'ElseIf Dr("Perhitungan") = "B" Then
                                    '    hitung = kurs * nilai
                                    '    hitungMUA = nilai
                                    'ElseIf Dr("Perhitungan") = "C" Then
                                    '    'hitung = (Val(Berat.Text) * (kurs * Dr("Nilai"))) + Dr("Nilai_2")
                                    '    'hitungMUA = (Val(Berat.Text) * (Dr("Nilai"))) + Dr("Nilai_2")
                                    '    Dr.Close()
                                    '    CloseConn()
                                    '    MessageBox.Show("Perhitungan tidak Mendukung . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    '    err = True
                                    '    Exit Sub
                                    'ElseIf Dr("Perhitungan") = "D" Then
                                    '    hitung = (kurs * nilai) * Dr("Selisih_Tanggal")
                                    '    hitungMUA = (nilai) * Dr("Selisih_Tanggal")
                                    '    JumlahHari.Text = Dr("Selisih_Tanggal")
                                    'ElseIf Dr("Perhitungan") = "E" Then
                                    '    Dim jml_konte As Integer = 0
                                    '    If Dr("Konte_Penjaluran") < Dr("Min") Then
                                    '        MessageBox.Show("Jumlah Container kurang dari jumlah minimal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                                    '        Dr.Close()
                                    '        CloseConn()
                                    '        err = True
                                    '        Exit Sub
                                    '    ElseIf Dr("Konte_Penjaluran") < Dr("Max") Then
                                    '        jml_konte = Dr("Konte_Penjaluran") - (Dr("Min") - 1)
                                    '    ElseIf Dr("Konte_Penjaluran") >= Dr("Max") Then
                                    '        jml_konte = Dr("Max") - (Dr("Min") - 1)
                                    '    Else
                                    '        MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                                    '        Dr.Close()
                                    '        CloseConn()
                                    '        err = True
                                    '        Exit Sub
                                    '    End If

                                    '    hitung = (kurs * nilai) * jml_konte
                                    '    hitungMUA = (nilai) * jml_konte
                                    '    DataGridView1.Rows.Item(index).Cells(6).Value = jml_konte
                                    'ElseIf Dr("Perhitungan") = "F" Then
                                    '    Dim jml_konte As Integer = 0
                                    '    If Val(TxtJumlah_conte.Text) < Dr("Min") Then
                                    '        MessageBox.Show("Jumlah Container kurang dari jumlah minimal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                                    '        Dr.Close()
                                    '        CloseConn()
                                    '        err = True
                                    '        Exit Sub
                                    '    ElseIf Val(TxtJumlah_conte.Text) < Dr("Max") Then
                                    '        jml_konte = Val(TxtJumlah_conte.Text) - (Dr("Min") - 1)
                                    '    ElseIf Val(TxtJumlah_conte.Text) >= Dr("Max") Then
                                    '        jml_konte = Dr("Max") - (Dr("Min") - 1)
                                    '    Else
                                    '        MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                                    '        Dr.Close()
                                    '        CloseConn()
                                    '        err = True
                                    '        Exit Sub
                                    '    End If

                                    '    hitung = (kurs * nilai) * jml_konte
                                    '    hitungMUA = (nilai) * jml_konte
                                    '    DataGridView1.Rows.Item(index).Cells(6).Value = jml_konte
                                    'ElseIf Dr("Perhitungan") = "G" Then

                                    '    Dim Konte As Integer = 0

                                    '    If General_Class.CekNULL(Dr("Jns")) = "WET" Or General_Class.CekNULL(Dr("Jns")) = "DRY" Then
                                    '        Konte = Dr("konte_per_jenis")
                                    '    ElseIf asal = "BIAYA_IMPORT_DETAIL2" And General_Class.CekNULL(Dr("Jns")) = "ALL" Then
                                    '        Konte = Dr("konte_per_Gudang")
                                    '    Else
                                    '        Konte = Dr("konte_per_lokasi")
                                    '    End If

                                    '    hitung = (kurs * nilai) * Konte
                                    '    hitungMUA = (nilai) * Konte
                                    '    DataGridView1.Rows.Item(index).Cells(6).Value = Konte
                                    'ElseIf Dr("Perhitungan") = "H" Then

                                    '    hitung = kurs * nilai * Dr("Konte_Asuransi")
                                    '    hitungMUA = nilai * Dr("Konte_Asuransi")
                                    '    DataGridView1.Rows.Item(index).Cells(6).Value = Dr("Konte_Asuransi")
                                    'ElseIf Dr("Perhitungan") = "I" Then

                                    '    'Total_Dec.Text = Dr("Total_Declare")
                                    '    'hitung = (((Dr("Total_Declare") * nilai) / 100) + Dr("Nilai_2")) * kurs
                                    '    'hitungMUA = ((Dr("Total_Declare") * nilai) / 100) + Dr("Nilai_2")
                                    '    Dr.Close()
                                    '    CloseConn()
                                    '    MessageBox.Show("Perhitungan tidak Mendukung . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    '    err = True
                                    '    Exit Sub
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
                                DataGridView1.Rows.Item(index).Cells(16).Value = ""
                                DataGridView1.Rows.Item(index).Cells(17).Value = "" 'coding stenly
                                DataGridView1.Rows.Item(index).Cells(18).Value = General_Class.CekNULL(Dr("Flag_average"))
                                DataGridView1.Rows.Item(index).Cells(19).Value = "Y"


                                ''ambil data master
                                'If arrMaster.Count = 0 Then
                                '    arrMaster.Add(Dr("Kode_master_kategori_biaya_import"))
                                'End If

                                'Dim ada_data As Boolean = True
                                'For index1 As Integer = 0 To arrMaster.Count - 1
                                '    'FREIGHT 'FREIGHT
                                '    If arrMaster.Item(index1) = Dr("kode_master_kategori_biaya_import") Then
                                '        ada_data = False
                                '    End If

                                'Next

                                'If ada_data = True Then
                                '    arrMaster.Add(Dr("Kode_master_kategori_biaya_import"))
                                'End If



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

    Private Sub Transaksi_Biaya_import3_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Barang_Kategori_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Kosong()
    End Sub

    Public Sub Tampil_Data()

        If TxtContainer.Text.Trim.Length = 0 Then
            MessageBox.Show("Masukkan Rencana Order Terlebih Dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If



        Try

            OpenConn()

            id_rencana_group = ""
            lokasi_group = ""
            Konte_group = 0


            If Txt_Flag_Group.Text = "Y" Then
                Dim index As Integer = 0
                SQL = "select a.id_rencana, a.lokasi, a.Total_Persen, a.Kode_supplier from rencana_order a, rencana_order_gabungan b where "
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

            Else
                id_rencana_group = "'" & TxtId_Rencana.Text & "'"
                lokasi_group = "'" & CmbLokasi.Text & "'"
            End If

            SQL = "select a.id_rencana, Kode_supplier from rencana_order a where "
            SQL = SQL & " a.id_rencana in(" & id_rencana_group & ")"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            txtKdSup.Text = .Rows(i).Item("Kode_supplier")

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

            SQL = "select a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, a.Kode_Biaya, b.Nama as Nama_Biaya, a.Kode_Kontainer, "
            SQL = SQL & "a.Kode_Perusahaan_Biaya_Import, e.Nama, a.Tanggal, a.Urut, a.Kode_Pelayaran from Biaya_Import_Detail3 a, biaya_import b, Kategori_Biaya_Import c, "
            SQL = SQL & "master_Kategori_biaya_import d, perusahaan_biaya_import e where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Biaya = b.Kode_Biaya and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and a.Kode_Stock_Owner in(" & lokasi_group & ") and a.Kode_Kontainer = '" & TxtContainer.Text & "' and Kode_Supplier='" & txtKdSup.Text & "' and a.Validasi_Penawaran= 'Y' "

            If ComboBox2.SelectedIndex > 0 Then
                SQL = SQL & " and d.Kode_Master_Kategori_Biaya_import ='" & ComboBox2.Text & "' "
            End If
            If ComboBox3.SelectedIndex > 0 Then
                SQL = SQL & " and c.Kode_Kategori_Biaya_Import ='" & ComboBox3.Text & "' "
            End If
            If ComboBox4.SelectedIndex > 0 Then
                SQL = SQL & " and e.Nama ='" & ComboBox4.Text & "' "
            End If

            If ChkTgl.Checked Then
                SQL = SQL & " and a.tanggal BETWEEN '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If
            SQL = SQL & " Order by a.Tanggal desc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem

                    'DateTimePicker1.Value = dr("Tanggal")

                    Lvw = ListView1.Items.Add(dr("kode_stock_owner"))
                    Lvw.SubItems.Add(dr("Kode_Master_Kategori_Biaya_import"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_Biaya_Import"))
                    Lvw.SubItems.Add(dr("Kode_Biaya"))
                    Lvw.SubItems.Add(dr("Nama_Biaya"))
                    Lvw.SubItems.Add(dr("Kode_Kontainer"))
                    Lvw.SubItems.Add(dr("Kode_Perusahaan_Biaya_Import"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(dr("urut"))
                    Lvw.SubItems.Add(dr("Kode_Pelayaran"))
                    Lvw.SubItems.Add(Format(dr("Tanggal"), "dd MMM yyyy"))

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

        'Try
        '    OpenConn()
        '    DataGridView1.Rows.Clear()
        '    Dim no As Integer = 0

        '    SQL = "select b.kode_perusahaan, a.Id_Rencana, a.Lokasi, c.Kode_Kategori_Biaya_Import, a.kode_biaya, c.Nama as nama_biaya, a.Kontainer, "
        '    SQL = SQL & "a.Kode_Perusahaan_Biaya_Import, d.Nama as nama_perusahaan, b.Perhitungan, b.Kode_Mata_Uang, b.Nilai, "
        '    SQL = SQL & "ISNULL((select top(1) x.Nilai from kurs_new x "
        '    SQL = SQL & "where x.kode_perusahaan = b.Kode_Perusahaan and x.Kode_Mata_Uang = b.Kode_Mata_Uang "
        '    SQL = SQL & "order by x.No_Urut DESC),0) as Kurs "

        '    'SQL = SQL & "isnull(("
        '    'SQL = SQL & "select nilai from tes_2x x where x.kode_biaya = c.kode_biaya "
        '    'SQL = SQL & "), 0) as nilaix, "

        '    'SQL = SQL & "isnull(("
        '    'SQL = SQL & "select kurs from tes_2x x where x.kode_biaya = c.kode_biaya "
        '    'SQL = SQL & "), 0) as kursx "


        '    SQL = SQL & "from Rencana_Order_Biaya_Import a, Biaya_Import_Detail b, Biaya_Import c, Perusahaan_Biaya_Import d "
        '    SQL = SQL & "where a.kode_perusahaan = b.kode_Perusahaan and a.Lokasi = b.Kode_Stock_Owner and a.Kode_Biaya = b.Kode_Biaya and "
        '    SQL = SQL & "a.Kontainer = b.Kode_Kontainer and a.kode_perusahaan_biaya_import = b.Kode_Perusahaan_Biaya_Import and "
        '    SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Biaya =c.Kode_Biaya and "
        '    SQL = SQL & "a.Kode_Perusahaan = d.Kode_Perusahaan and a.kode_perusahaan_biaya_import = d.Kode_Perusahaan_Biaya_Import and "
        '    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.Id_Rencana = '" & TxtId_Rencana.Text & "' "
        '    SQL = SQL & "order by c.Kode_Kategori_Biaya_Import, c.Nama"
        '    Using Ds = BindingTrans(SQL)
        '        For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
        '            With Ds.Tables("MyTable").Rows(i)
        '                DataGridView1.Rows.Add(1)

        '                DataGridView1.Rows.Item(no).Cells(0).Value = .Item("Id_Rencana")
        '                DataGridView1.Rows.Item(no).Cells(1).Value = .Item("Lokasi")
        '                DataGridView1.Rows.Item(no).Cells(2).Value = .Item("Kode_Kategori_Biaya_Import")
        '                DataGridView1.Rows.Item(no).Cells(3).Value = .Item("Kode_Biaya")
        '                DataGridView1.Rows.Item(no).Cells(4).Value = .Item("Nama_Biaya")
        '                DataGridView1.Rows.Item(no).Cells(5).Value = .Item("Kontainer")
        '                DataGridView1.Rows.Item(no).Cells(6).Value = TxtJumlah_conte.Text

        '                DataGridView1.Rows.Item(no).Cells(7).Value = .Item("Kode_Perusahaan_Biaya_Import")
        '                DataGridView1.Rows.Item(no).Cells(8).Value = .Item("Nama_Perusahaan")
        '                DataGridView1.Rows.Item(no).Cells(9).Value = .Item("Perhitungan")
        '                DataGridView1.Rows.Item(no).Cells(10).Value = .Item("Kode_Mata_Uang")


        '                'DataGridView1.Rows.Item(no).Cells(11).Value = Format(.Item("Kursx"), "N0")
        '                'DataGridView1.Rows.Item(no).Cells(12).Value = Format(.Item("Nilaix"), "N2")
        '                'If .Item("Perhitungan") = "A" Then
        '                '    hitung = .Item("Kursx") * .Item("Nilaix") * Val(TxtJumlah_conte.Text)
        '                'Else
        '                '    hitung = .Item("Kursx") * .Item("Nilaix")
        '                'End If
        '                'DataGridView1.Rows.Item(no).Cells(13).Value = Format(hitung, "N0")
        '                'no = no + 1

        '                DataGridView1.Rows.Item(no).Cells(11).Value = Format(.Item("Kurs"), "N0")
        '                DataGridView1.Rows.Item(no).Cells(12).Value = Format(.Item("Nilai"), "N2")
        '                If .Item("Perhitungan") = "A" Then
        '                    hitung = .Item("Kurs") * .Item("Nilai") * Val(TxtJumlah_conte.Text)
        '                Else
        '                    hitung = .Item("Kurs") * .Item("Nilai")
        '                End If
        '                DataGridView1.Rows.Item(no).Cells(13).Value = Format(hitung, "N0")
        '                no = no + 1
        '            End With
        '        Next
        '    End Using
        '    CloseConn()
        'Catch ex As Exception
        '    DataGridView1.Rows.Clear()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'HitungGrand()
    End Sub

    Private Sub Get_No_Faktur()
        TxtNo_Faktur.Text = TBiaya_Import & Format(DtTanggal.Value, "MMyy") & "-" & _
                             General_Class.Get_Last_Number2("Transaksi_Biaya_Import3", "no_Faktur", 5, _
                             "Kode_perusahaan", KodePerusahaan, _
                             "And", "substring(no_Faktur, 1, " & Len(TBiaya_Import) + 4 & ")", TBiaya_Import & Format(DtTanggal.Value, "MMyy"))
    End Sub

    Public Sub Kosong()
        GetTime()
        DtTanggal.Value = Tanggal_Sekarang
        txtTotalBiaya.Text = ""
        txtTotalBiayaBilling.Text = ""
        TxtNo_Faktur.Text = ""
        TxtNo_PO.Text = ""
        TxtKeterangan.Text = ""
        TxtContainer.Text = ""
        TxtId_Rencana.Text = ""
        TxtJumlah_conte.Text = ""
        TxtSupplier.Text = ""
        Berat.Text = ""
        DtTanggal_Po.Value = Tanggal_Sekarang
        DataGridView1.Rows.Clear()
        DataGridView3.Rows.Clear()
        arrMaster.Clear()
        id_rencana_group = ""
        lokasi_group = ""
        Konte_group = 0
        txtKdSup.Text = ""

        ChkTgl.Checked = False
        DateTimePicker1.Enabled = False
        DateTimePicker2.Enabled = False

        ListView1.Clear()
        ListView1.Columns.Add("Lokasi", 140, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode Master", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Kategori", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Biaya", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama Biaya", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kontainer", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Perusahaan Biaya Import", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama Perusahaan", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Urut", 0, HorizontalAlignment.Left) '8
        ListView1.Columns.Add("Kode Pelayaran", 130, HorizontalAlignment.Left) '8
        ListView1.Columns.Add("Tanggal Penawaran", 130, HorizontalAlignment.Left).DisplayIndex = 0
        ListView1.View = View.Details

        ListView2.Clear()
        ListView2.Columns.Add("Mata Uang", 70, HorizontalAlignment.Center)
        ListView2.Columns.Add("Nilai Kurs", 70, HorizontalAlignment.Right)
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
            SQL = "select Kode_Master_Kategori_Biaya_import from master_Kategori_biaya_import where kode_perusahaan = '" & KodePerusahaan & "' and Flag_Form = '3' order by Kode_Master_Kategori_Biaya_import"
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

            ComboBox1.Items.Clear()
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
        Display_Rencana_Order_Lain_Lain.dari_mana = "TRANSAKSI_BIAYA3"
        'Display_Rencana_Order_Lain_Lain.filter_tambahan = "and ro.flag_sudah_transaksi = 'Y' and ro.flag_submit_po = 'Y' and  ro.flag_loading_barang = 'Y' "
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

    'Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
    '    If TxtKeterangan.Text.Trim.Length = 0 Then
    '        MessageBox.Show("Keterangan Harus Di isi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        TxtKeterangan.Focus()
    '        Exit Sub
    '    ElseIf DataGridView1.RowCount = 0 Then
    '        MessageBox.Show("Data Tidak Ada . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        DataGridView1.Focus()
    '        Exit Sub
    '    End If
    '    GetTime()
    '    Try
    '        OpenConn()
    '        Cmd.Transaction = Cn.BeginTransaction

    '        Get_No_Faktur()

    '        Dim kd_sup As String = ""
    '        SQL = "Select Flag_Sudah_Transaksi3, kode_supplier from Rencana_Order where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Rencana = '" & TxtId_Rencana.Text & "'"
    '        Using Dr = OpenTrans(SQL)
    '            If Dr.Read Then
    '                kd_sup = Dr("kode_supplier")

    '                If General_Class.CekNULL(Dr("Flag_Sudah_Transaksi3")) = "Y" Then
    '                    Dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Sudah Dilakukan Transaksi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Exit Sub
    '                End If
    '            Else
    '                Dr.Close()
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Rencana order tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If
    '        End Using

    '        Dim inisial_Faktur As String = ""
    '        SQL = "select top(1) Inisial_faktur "
    '        SQL = SQL & " from Stock_Owner where "
    '        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
    '        SQL = SQL & "Kode_stock_Owner = '" & CmbLokasi.Text & "'"
    '        Using dr = OpenTrans(SQL)
    '            If dr.Read Then
    '                inisial_Faktur = dr("Inisial_faktur")

    '            Else
    '                dr.Close()
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Data Lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If
    '        End Using

    '        SQL = "insert into transaksi_biaya_import3(kode_perusahaan, no_faktur, tanggal, jam, UserID, Id_Rencana, "
    '        SQL = SQL & "keterangan, jml_kontainer, Total_Berat, Grand_Total_hpp, Grand_Total) "
    '        SQL = SQL & "values('" & KodePerusahaan & "','" & TxtNo_Faktur.Text & "', '" & Format(CDate(DtTanggal.Text), "yyyy-MM-dd") & "', "
    '        SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "',"
    '        SQL = SQL & "'" & UserID & "', '" & TxtId_Rencana.Text & "', '" & TxtKeterangan.Text & "', "
    '        SQL = SQL & "'" & TxtJumlah_conte.Text & "', '" & Berat.Text & "', '" & HilangkanTanda(txtGrand.Text) & "', '" & HilangkanTanda(txtTotalBiayaBilling.Text) & "')"
    '        ExecuteTrans(SQL)

    '        For i As Integer = 0 To DataGridView1.Rows.Count - 1
    '            Get_Isi_Listview(i)

    '            If LvBiayaBilling = "" Then
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Biaya billing belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If

    '            SQL = "insert into detail_transaksi_biaya_import3 (kode_perusahaan, No_faktur, Id_Rencana, Kode_stock_Owner, Kode_Biaya, "
    '            SQL = SQL & "Kode_Kontainer, jml_kontainer, Kode_Perusahaan_Biaya_Import, Jenis_Perhitungan, "
    '            SQL = SQL & "Mata_Uang, Kurs, biaya, Nilai_2, Total, Kode_Master_Kategori_Biaya_Import, Mata_Uang_HPP, Biaya_HPP, Total_HPP) values( "
    '            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtNo_Faktur.Text & "', "
    '            SQL = SQL & "'" & TxtId_Rencana.Text & "', '" & Lvlokasi & "', "
    '            SQL = SQL & "'" & LvKodeBiaya & "', '" & LvKontainer & "', "
    '            SQL = SQL & "'" & LvJumlahKontainer & "', '" & LvKodePerusahaanBiaya & "', "
    '            SQL = SQL & "'" & LvPerhitungan & "', '" & LvMataUangBilling & "', 1, "
    '            SQL = SQL & HilangkanTanda(LvBiayaBilling) & ", " & HilangkanTanda(LvNilai2) & ", " & HilangkanTanda(LvTotal) & ",'" & LvMaster & "', "
    '            SQL = SQL & "'" & LvMataUang & "', " & HilangkanTanda(LvBiaya) & ", " & Val(HilangkanTanda(LvBiaya)) * Val(HilangkanTanda(LvJumlahKontainer)) & ")"
    '            ExecuteTrans(SQL)
    '        Next


    '        SQL = "select Flag_Gabungan from rencana_order where "
    '        SQL = SQL & "Id_rencana = '" & TxtId_Rencana.Text & "'"
    '        Using Dr2 = OpenTrans(SQL)
    '            If Dr2.Read Then
    '                If General_Class.CekNULL(Dr2("Flag_Gabungan")) = "Y" Then
    '                    Dr2.Close()
    '                    SQL = "select a.id_rencana from rencana_order a, rencana_order_gabungan b where "
    '                    SQL = SQL & "a.id_rencana = b.Id_rencana and b.Id_rencana_induk = '" & TxtId_Rencana.Text & "'"
    '                    Using Ds = BindingTrans(SQL)
    '                        With Ds.Tables("MyTable")
    '                            If .Rows.Count <> 0 Then
    '                                For i As Integer = 0 To .Rows.Count - 1
    '                                    SQL = "update rencana_Order set Flag_Sudah_Transaksi3 = 'Y' "
    '                                    SQL = SQL & " where Id_Rencana = '" & .Rows(i).Item("id_rencana") & "'"
    '                                    ExecuteTrans(SQL)
    '                                Next
    '                            Else
    '                                CloseTrans()
    '                                CloseConn()
    '                                MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                                Exit Sub
    '                            End If
    '                        End With
    '                    End Using
    '                Else
    '                    Dr2.Close()
    '                    SQL = "update rencana_Order set Flag_Sudah_Transaksi3 = 'Y' "
    '                    SQL = SQL & " where Id_Rencana = '" & TxtId_Rencana.Text.Trim & "'"
    '                    ExecuteTrans(SQL)
    '                End If
    '            Else
    '                Dr2.Close()
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If
    '        End Using

    '        'SQL = "Update Rencana_order set Flag_Sudah_Transaksi3 = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and "
    '        'SQL = SQL & "Id_Rencana = '" & TxtId_Rencana.Text & "' "
    '        'ExecuteTrans(SQL)

    '        Cmd.Transaction.Commit()
    '        MessageBox.Show("Data Tersimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        CloseConn()
    '    Catch ex As Exception
    '        CloseTrans()
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    '    Kosong()
    'End Sub


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

            SQL = "Select a.Flag_Sudah_Transaksi3, a.kode_supplier, b.flag_average from "
            SQL = SQL & "Rencana_Order a, suppliers b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.kode_supplier = b.kode_supplier and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Id_Rencana = '" & TxtId_Rencana.Text & "' and a.status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    kd_sup = Dr("kode_supplier")
                    flag_average = Dr("flag_average")

                    If General_Class.CekNULL(Dr("Flag_Sudah_Transaksi3")) = "Y" Then
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

            SQL = "insert into transaksi_biaya_import3(kode_perusahaan, no_faktur, tanggal, jam, UserID, Id_Rencana, "
            SQL = SQL & "keterangan, jml_kontainer, Total_Berat, Grand_Total_hpp, Grand_Total, Flag_Average) "
            SQL = SQL & "values('" & KodePerusahaan & "','" & TxtNo_Faktur.Text & "', '" & Format(CDate(DtTanggal.Text), "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "',"
            SQL = SQL & "'" & UserID & "', '" & TxtId_Rencana.Text & "', '" & TxtKeterangan.Text & "', "
            SQL = SQL & "'" & TxtJumlah_conte.Text & "', '" & Berat.Text & "', '" & HilangkanTanda(txtGrand.Text) & "', '" & HilangkanTanda(txtTotalBiayaBilling.Text) & "', '" & flag_average & "')"
            ExecuteTrans(SQL)

            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(i)

                If LvBiayaBilling = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Biaya billing belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "insert into detail_transaksi_biaya_import3 (kode_perusahaan, No_faktur, Id_Rencana, Kode_stock_Owner, Kode_Biaya, "
                SQL = SQL & "Kode_Kontainer, jml_kontainer, Kode_Perusahaan_Biaya_Import, Jenis_Perhitungan, "
                SQL = SQL & "Mata_Uang, Kurs, biaya, Nilai_2, Total, Kode_Master_Kategori_Biaya_Import, Mata_Uang_HPP, Biaya_HPP, Total_HPP"
                SQL = SQL & ",kode_kategori_biaya_Import,flag_average_kategori, avg_biaya, total_avg_biaya, Flag_Validasi_Biaya) values( " 'coding stenly
                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtNo_Faktur.Text & "', "
                SQL = SQL & "'" & TxtId_Rencana.Text & "', '" & Lvlokasi & "', "
                SQL = SQL & "'" & LvKodeBiaya & "', '" & LvKontainer & "', "
                SQL = SQL & "'" & LvJumlahKontainer & "', '" & LvKodePerusahaanBiaya & "', "
                SQL = SQL & "'" & LvPerhitungan & "', '" & LvMataUangBilling & "', 1, "
                SQL = SQL & HilangkanTanda(LvBiayaBilling) & ", " & HilangkanTanda(LvNilai2) & ", "
                SQL = SQL & "" & Val(HilangkanTanda(LvBiayaBilling)) * Val(HilangkanTanda(LvJumlahKontainer)) & ", '" & LvMaster & "', "
                SQL = SQL & "'" & LvMataUang & "', " & HilangkanTanda(LvBiaya) & "," & HilangkanTanda(LvTotal) & ", "
                SQL = SQL & "'" & LvKodeKategori & "', '" & LvFlagAvg & "', "
                SQL = SQL & "" & HilangkanTanda(LvBiaya) & "," & HilangkanTanda(LvTotal) & ", '" & LvValidasi & "')"
                ExecuteTrans(SQL)
            Next

            For i As Integer = 0 To DataGridView3.Rows.Count - 1
                Get_Isi_Listview3(i)

                SQL = "insert into detail_transaksi_biaya_import3_by_Perusahaan(kode_perusahaan, No_faktur, Kode_Perusahaan_Biaya_Import, Mata_Uang, Nilai) "
                SQL = SQL & "values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtNo_Faktur.Text & "', "
                SQL = SQL & "'" & LvPerusahaan3 & "', '" & LvMataUang3 & "', "
                SQL = SQL & "'" & HilangkanTanda(LvTotal3) & "') "
                ExecuteTrans(SQL)
            Next

            If flag_average = "Y" Then

                SQL = ";with "
                SQL = SQL & "cte_Total_Konte as( "
                SQL = SQL & "select X.No_Faktur, X.Jml_Kontainer, Y.Kode_Kategori_Biaya_Import, sum(Total_HPP) as Total,  "
                SQL = SQL & "isnull((select M.Selisih_lama from avg_kategori_biaya_Import3 M where "
                SQL = SQL & "M.No_Faktur = X.No_faktur and M.Kode_Kategori_Biaya_Import = Y.Kode_Kategori_Biaya_import),0) as Selisih_Lama  "
                SQL = SQL & "from Transaksi_Biaya_Import3 X, Detail_Transaksi_Biaya_Import3 Y, Rencana_Order Z, Suppliers V "
                SQL = SQL & "where X.Kode_Perusahaan = Y.Kode_Perusahaan and X.No_Faktur = Y.No_faktur and "
                SQL = SQL & "X.Kode_Perusahaan = Z. Kode_Perusahaan and X.Id_rencana = Z.Id_Rencana and "
                SQL = SQL & "Z.kode_Perusahaan = V.Kode_Perusahaan And Z.Kode_Supplier = v.Kode_Supplier "
                SQL = SQL & "and X.Flag_Average ='Y' and Z.Kode_Supplier = '" & kd_sup & "' and "
                SQL = SQL & "Y.Flag_Average_Kategori = 'Y' and x.status is null "
                SQL = SQL & "group by X.no_faktur, X.Jml_Kontainer, Y.Kode_Kategori_Biaya_Import "
                SQL = SQL & ") "

                SQL = SQL & ",cte_AVG as( "
                SQL = SQL & "select a.Kode_Perusahaan, a.No_Faktur, a.Id_rencana, a.Jml_Kontainer, "
                SQL = SQL & "b.Kode_kategori_Biaya_Import, sum(Total_HPP) as Total "

                SQL = SQL & ",isnull((select sum(X.Jml_Kontainer) from cte_Total_Konte X "
                SQL = SQL & "where X.Kode_Kategori_Biaya_Import = b.Kode_kategori_Biaya_Import "
                SQL = SQL & "),0) total_Kontainer "

                SQL = SQL & ",isnull((select sum(X.Total)+sum(X.Selisih_Lama) from cte_Total_Konte X "
                SQL = SQL & "where X.Kode_Kategori_Biaya_Import = b.Kode_kategori_Biaya_Import "
                SQL = SQL & "),0) total_Biaya "

                SQL = SQL & ",isnull((select top(1) Selisih from Detail_Selisih_Transaksi_Biaya_Import3_By_Kategori X "
                SQL = SQL & "where X.Kode_Kategori_Biaya_Import = b.Kode_kategori_Biaya_Import and X.Kode_SUpplier ='" & kd_sup & "' "
                SQL = SQL & "and X.Pakai is null and X.Status is null),0) as Selisih_Lama "

                SQL = SQL & ",isnull((select top(1) Urut from Detail_Selisih_Transaksi_Biaya_Import3_By_Kategori X "
                SQL = SQL & "where X.Kode_Kategori_Biaya_Import = b.Kode_kategori_Biaya_Import and X.Kode_SUpplier ='" & kd_sup & "' "
                SQL = SQL & "and X.Pakai is null and X.Status is null),NULL) as Urut_Selisih_Lama "

                SQL = SQL & "from Transaksi_Biaya_Import3 a, detail_Transaksi_Biaya_import3 b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_faktur  "
                SQL = SQL & "and a.No_Faktur = '" & TxtNo_Faktur.Text.Trim & "' and b.flag_Average_Kategori = 'Y' and "
                SQL = SQL & "a.flag_Average = 'Y' and a.status is null "
                SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur, a.Id_rencana, a.Jml_Kontainer, b.Kode_kategori_Biaya_Import "
                SQL = SQL & ") "

                SQL = SQL & ",Cte_data as( "
                SQL = SQL & "select a.Kode_Perusahaan, a.No_Faktur, a.ID_rencana, a.Kode_Kategori_Biaya_Import, a.selisih_lama, a.Urut_Selisih_Lama, a.Total_Kontainer, b.Flag_Masuk_HPP, "
                SQL = SQL & "round(((a.Total_Biaya + a.Selisih_Lama) / a.Total_Kontainer),0) as Biaya_Avg, "
                SQL = SQL & "round((((a.Total_Biaya + a.Selisih_Lama) / a.Total_Kontainer) * a.Jml_Kontainer),0) as Total_Biaya_Avg, "
                SQL = SQL & "round((a.Total + a.selisih_lama-(((a.Total_Biaya + a.Selisih_Lama) / a.Total_Kontainer)* a.Jml_Kontainer)),0) as selisih_Baru "
                SQL = SQL & "from cte_AVG a, Kategori_Biaya_Import b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Kategori_Biaya_Import = b.Kode_Kategori_Biaya_Import "
                SQL = SQL & ") "

                SQL = SQL & "insert into avg_Kategori_Biaya_import3(Kode_Perusahaan, No_Faktur, Kode_Kategori_Biaya_Import, "
                SQL = SQL & "Avg_Biaya, Total_Avg_Biaya, Selisih_Lama, Urut_Selisih_Lama, Selisih_Baru, Id_Rencana, Total_Kontainer, Flag_Masuk_HPP) "
                SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Kategori_Biaya_Import, Biaya_Avg, "
                SQL = SQL & "Total_Biaya_Avg, selisih_lama, Urut_Selisih_Lama, selisih_Baru, Id_Rencana, Total_Kontainer, Flag_Masuk_HPP from cte_data "
                ExecuteTrans(SQL)


                SQL = "select No_faktur, Kode_Kategori_Biaya_Import, Urut_Selisih_Lama "
                SQL = SQL & "from avg_Kategori_Biaya_import3 where Urut_Selisih_Lama is not null and No_Faktur = '" & TxtNo_Faktur.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1

                                SQL = "update Detail_Selisih_Transaksi_Biaya_Import3_By_Kategori "
                                SQL = SQL & "set Pakai = 'Y' where Urut = '" & .Rows(i).Item("Urut_Selisih_Lama") & "'"
                                ExecuteTrans(SQL)
                            Next
                        End If
                    End With
                End Using


                SQL = "select No_faktur, Kode_Kategori_Biaya_Import, selisih_Baru "
                SQL = SQL & "from avg_Kategori_Biaya_import3 where Selisih_baru <> 0 and No_Faktur = '" & TxtNo_Faktur.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1

                                SQL = "insert into Detail_Selisih_Transaksi_Biaya_Import3_By_Kategori"
                                SQL = SQL & "(Kode_perusahaan, No_faktur, Kode_Kategori_Biaya_Import, selisih, Kode_Supplier) values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & .Rows(i).Item("No_faktur") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Kode_Kategori_Biaya_Import") & "', '" & .Rows(i).Item("selisih_Baru") & "', '" & kd_sup & "')"
                                ExecuteTrans(SQL)
                            Next
                        End If
                    End With
                End Using

                SQL = " select a.id_rencana, a.No_Faktur, a.Kode_Kategori_Biaya_Import, sum(a.Biaya_HPP) as Biaya, sum(a.Total_HPP) as Total, b.Flag_Masuk_HPP "
                SQL = SQL & "from Detail_Transaksi_Biaya_Import3 a, Kategori_Biaya_Import b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Kategori_Biaya_Import = b.Kode_Kategori_Biaya_Import "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'"
                SQL = SQL & "and a.No_Faktur = '" & TxtNo_Faktur.Text & "' and a.Flag_Average_Kategori = 'T' "
                SQL = SQL & "group by a.id_rencana, a.No_Faktur, a.Kode_Kategori_Biaya_Import, b.Flag_Masuk_HPP "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1

                                SQL = "insert into avg_Kategori_Biaya_import3(Kode_Perusahaan, No_Faktur, Kode_Kategori_Biaya_Import, "
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
                'SQL = "update Detail_transaksi_Biaya_import3 set avg_biaya = biaya_HPP, total_avg_biaya = total_HPP where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "no_faktur = '" & TxtNo_Faktur.Text.Trim & "'"
                'ExecuteTrans(SQL)

                SQL = " select a.id_rencana, a.No_Faktur, a.Kode_Kategori_Biaya_Import, sum(a.Biaya_HPP) as Biaya, sum(a.Total_HPP) as Total, b.Flag_Masuk_HPP "
                SQL = SQL & "from Detail_Transaksi_Biaya_Import3 a, Kategori_Biaya_Import b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Kategori_Biaya_Import = b.Kode_Kategori_Biaya_Import "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & TxtNo_Faktur.Text & "' "
                SQL = SQL & "group by a.id_rencana, a.No_Faktur, a.Kode_Kategori_Biaya_Import, b.Flag_Masuk_HPP "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1

                                SQL = "insert into avg_Kategori_Biaya_import3(Kode_Perusahaan, No_Faktur, Kode_Kategori_Biaya_Import, "
                                SQL = SQL & "Avg_Biaya, Total_Avg_Biaya, Id_Rencana, Flag_Masuk_HPP) Values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & .Rows(i).Item("No_Faktur") & "', '" & .Rows(i).Item("Kode_Kategori_Biaya_Import") & "',  "
                                SQL = SQL & "'" & .Rows(i).Item("Biaya") & "', '" & .Rows(i).Item("Total") & "', '" & .Rows(i).Item("id_rencana") & "', "
                                SQL = SQL & "'" & .Rows(i).Item("Flag_Masuk_HPP") & "')"
                                ExecuteTrans(SQL)
                            Next
                        End If
                    End With
                End Using

                SQL = "update transaksi_Biaya_import3 set Grand_Biaya = '" & HilangkanTanda(txtTotalBiaya.Text) & "' "
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
                                        SQL = "update rencana_Order set Flag_Sudah_Transaksi3 = 'Y' "
                                        SQL = SQL & " where Id_Rencana = '" & .Rows(i).Item("id_rencana") & "' and Flag_Sudah_Transaksi3 Is Null"
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
                        SQL = "update rencana_Order set Flag_Sudah_Transaksi3 = 'Y' "
                        SQL = SQL & " where Id_Rencana = '" & TxtId_Rencana.Text.Trim & "' and Flag_Sudah_Transaksi3 Is Null"
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

            'SQL = "Update Rencana_order set Flag_Sudah_Transaksi3 = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "Id_Rencana = '" & TxtId_Rencana.Text & "' "
            'ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            MessageBox.Show("Data Tersimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
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

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Display_Kurs.indexbrp = DataGridView1.CurrentRow.Index
        Display_Kurs.ComboBox1.SelectedIndex = -1
        Display_Kurs.TextBox2.Text = ""

        Display_Kurs.ShowDialog()
        HitungGrand()
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

        If LvPerhitungan <> "C" Then
            DataGridView1.CurrentRow.Cells(cellNilai2).Value = 0
        End If

        Get_Isi_Listview(DataGridView1.CurrentRow.Index)

        Dim Edit As Double = 0
        If LvPerhitungan = "A" Then
            Edit = HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya) * HilangkanTanda(LvJumlahKontainer)
        ElseIf LvPerhitungan = "B" Then
            Edit = HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya)
        ElseIf LvPerhitungan = "C" Then
            Edit = (Val(Berat.Text) * (HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya))) + HilangkanTanda(LvNilai2)
        ElseIf LvPerhitungan = "D" Then
            Edit = (HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya)) * Val(JumlahHari.Text)
        ElseIf LvPerhitungan = "E" Or LvPerhitungan = "F" Then
            Edit = HilangkanTanda(LvKurs) * HilangkanTanda(LvBiaya) * HilangkanTanda(LvJumlahKontainer)
        End If

        DataGridView1.CurrentRow.Cells(cellBiaya).Value = Format(Val(HilangkanTanda(LvBiaya)), "N2")
        DataGridView1.CurrentRow.Cells(cellNilai2).Value = Format(Val(HilangkanTanda(LvNilai2)), "N2")
        DataGridView1.CurrentRow.Cells(cellTotal).Value = Format(Edit, "N2")
        HitungGrand()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        dari_tombol_cari = True
        Tampil_Data()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Mata Uang Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Nilai Kurs Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus()
            Exit Sub
        End If

        For index As Integer = 0 To ListView2.Items.Count - 1

            If ListView2.Items(index).SubItems(0).Text = ComboBox1.Text Then
                ListView2.Items(index).SubItems(1).Text = TextBox1.Text

                ComboBox1.SelectedIndex = -1
                TextBox1.Text = ""

                Exit Sub
            End If

        Next

        Dim Lvw As ListViewItem
        Lvw = ListView2.Items.Add(ComboBox1.Text)
        Lvw.SubItems.Add(TextBox1.Text)
        ComboBox1.SelectedIndex = -1
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

            SQL = "select e.Kode_Perusahaan, e.Kode_Biaya, e.Kode_Stock_Owner, e.Kode_Kontainer, e.Kode_Perusahaan_Biaya_Import, e.Kode_Mata_Uang, "
            SQL = SQL & "isnull(Nilai, 0) as nilai, isnull(Nilai_2, 0) as nilai_2, Perhitungan, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select datediff(day,format(Tanggal_Bongkar, 'yyyy-MM-dd'),format(DATEADD(dd, Free_Time, Eta), 'yyyy-MM-dd')) from "
            SQL = SQL & "ubah_status_otw a, Bongkar_Import b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_Rencana = b.ID_Rencana and A.id_rencana ='" & TxtId_Rencana.Text & "'"
            SQL = SQL & "),0) as Selisih_Tanggal, d.flag_average from Biaya_Import_detail3 e, Biaya_Import c, kategori_biaya_import d where " 'coding stenly where "

            SQL = SQL & "e.Kode_Perusahaan = '" & KodePerusahaan & "' and e.Kode_Perusahaan = c.Kode_Perusahaan " 'coding stenly
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and e.kode_biaya = c.kode_biaya and c.kode_kategori_biaya_import = d.kode_kategori_biaya_import " 'coding stenly
            SQL = SQL & "and e.Kode_Biaya = '" & ListView1.FocusedItem.SubItems(3).Text & "' and "
            SQL = SQL & "e.Kode_Stock_Owner = '" & ListView1.FocusedItem.SubItems(0).Text & "' "
            SQL = SQL & "and e.Kode_Perusahaan_Biaya_Import = '" & ListView1.FocusedItem.SubItems(6).Text & "' and "
            SQL = SQL & "e.Kode_Kontainer = '" & ListView1.FocusedItem.SubItems(5).Text & "' and Urut= '" & ListView1.FocusedItem.SubItems(8).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    DataGridView1.Rows.Item(index).Cells(9).Value = Dr("Perhitungan")
                    DataGridView1.Rows.Item(index).Cells(10).Value = Dr("Kode_Mata_Uang")


                    'For index1 As Integer = 0 To ListView2.Items.Count - 1

                    '    If Dr("Kode_Mata_Uang") = ListView2.Items(index1).SubItems(0).Text Then
                    kurs = 1 'ListView2.Items(index1).SubItems(1).Text
                    '    End If

                    'Next

                    If kurs = 0 Then
                        MessageBox.Show("Kurs Mata Uang " & Dr("Kode_Mata_Uang") & " Tidak Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Dr.Close()
                        CloseConn()
                        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                        Exit Sub
                    End If

                    DataGridView1.Rows.Item(index).Cells(11).Value = Format(kurs, "N2")
                    DataGridView1.Rows.Item(index).Cells(12).Value = Format(Dr("Nilai"), "N2")
                    DataGridView1.Rows.Item(index).Cells(13).Value = Format(Dr("Nilai_2"), "N2")
                    If Dr("Perhitungan") = "A" Then
                        hitung = kurs * Dr("Nilai") * Val(TxtJumlah_conte.Text)
                        'ElseIf Dr("Perhitungan") = "B" Then
                        '    hitung = kurs * Dr("Nilai")
                        'ElseIf Dr("Perhitungan") = "C" Then
                        '    hitung = (Val(Berat.Text) * (kurs * Dr("Nilai"))) + Dr("Nilai_2")
                        'ElseIf Dr("Perhitungan") = "D" Then
                        '    hitung = (kurs * Dr("Nilai")) * Dr("Selisih_Tanggal")
                        '    JumlahHari.Text = Dr("Selisih_Tanggal")
                        'ElseIf Dr("Perhitungan") = "E" Then
                        '    Dim jml_konte As Integer = 0
                        '    If Val(TxtJumlah_conte.Text) < Dr("Min") Then
                        '        MessageBox.Show("Jumlah Container kurang dari jumlah minimal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                        '        Dr.Close()
                        '        CloseConn()
                        '        Exit Sub
                        '    ElseIf Val(TxtJumlah_conte.Text) < Dr("Max") Then
                        '        jml_konte = Val(TxtJumlah_conte.Text) - (Dr("Min") - 1)
                        '    ElseIf Val(TxtJumlah_conte.Text) >= Dr("Max") Then
                        '        jml_konte = Dr("Max") - (Dr("Min") - 1)
                        '    Else
                        '        MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                        '        Dr.Close()
                        '        CloseConn()
                        '        Exit Sub
                        '    End If

                        '    hitung = (kurs * Dr("Nilai")) * jml_konte
                        '    DataGridView1.Rows.Item(index).Cells(6).Value = jml_konte
                        'ElseIf Dr("Perhitungan") = "F" Then
                        '    Dim jml_konte As Integer = 0
                        '    If Val(TxtJumlah_conte.Text) < Dr("Min") Then
                        '        MessageBox.Show("Jumlah Container kurang dari jumlah minimal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                        '        Dr.Close()
                        '        CloseConn()
                        '        Exit Sub
                        '    ElseIf Val(TxtJumlah_conte.Text) < Dr("Max") Then
                        '        jml_konte = Val(TxtJumlah_conte.Text) - (Dr("Min") - 1)
                        '    ElseIf Val(TxtJumlah_conte.Text) >= Dr("Max") Then
                        '        jml_konte = Dr("Max") - (Dr("Min") - 1)
                        '    Else
                        '        MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                        '        Dr.Close()
                        '        CloseConn()
                        '        Exit Sub
                        '    End If

                        '    hitung = (kurs * Dr("Nilai")) * jml_konte
                        '    DataGridView1.Rows.Item(index).Cells(6).Value = jml_konte
                    Else
                        MessageBox.Show("Error Perhitungan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                        Dr.Close()
                        CloseConn()
                        Exit Sub
                    End If

                    DataGridView1.Rows.Item(index).Cells(14).Value = Format(hitung, "N2")
                    DataGridView1.Rows.Item(index).Cells(15).Value = ListView1.FocusedItem.SubItems(1).Text
                    DataGridView1.Rows.Item(index).Cells(16).Value = ""
                    DataGridView1.Rows.Item(index).Cells(17).Value = ""
                    DataGridView1.Rows.Item(index).Cells(18).Value = Dr("flag_average")
                    DataGridView1.Rows.Item(index).Cells(19).Value = "T"

                    ''ambil data master
                    'If arrMaster.Count = 0 Then
                    '    arrMaster.Add(ListView1.FocusedItem.SubItems(1).Text)
                    'End If

                    'Dim ada_data As Boolean = True
                    'For index1 As Integer = 0 To arrMaster.Count - 1

                    '    If arrMaster.Item(index1) = ListView1.FocusedItem.SubItems(1).Text Then
                    '        ada_data = False
                    '    End If

                    'Next

                    'If ada_data = True Then
                    '    arrMaster.Add(ListView1.FocusedItem.SubItems(1).Text)
                    'End If
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

        HitungGrand()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub ChkTgl_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTgl.CheckedChanged

        If ChkTgl.Checked Then
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        Else
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        End If

    End Sub
End Class