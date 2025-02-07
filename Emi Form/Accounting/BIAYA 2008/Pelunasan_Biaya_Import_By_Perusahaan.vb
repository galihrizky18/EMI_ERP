Public Class Pelunasan_Biaya_Import_By_Perusahaan
    Dim JT As String
    Dim ArrNP1 As New ArrayList
    Dim ArrNP2 As New ArrayList

    Dim LvFak As String
    Dim LvTgl As String
    Dim LvKP As String
    Dim LvNP As String
    Dim LvMT As String
    Dim LvJml As String
    Dim Lvkurshpp As String
    Dim Lvnopo As String
    Dim Lvlks As String
    Dim Lvdec As String
    Dim Lvnodec As String
    Dim Lvselisih As String
    Dim LvRencana As String

    Dim LvFak2 As String
    Dim LvTgl2 As String
    Dim LvKP2 As String
    Dim LvNP2 As String
    Dim LvMT2 As String
    Dim LvJml2 As String
    Dim Lvkurshpp2 As String
    Dim Lvnopo2 As String
    Dim Lvlks2 As String
    Dim Lvdec2 As String
    Dim Lvnodec2 As String
    Dim Lvselisih2 As String
    Dim LvRencana2 As String

    Dim arrCrByr1, ArrAkunCB1 As New ArrayList
    Dim arrCrByr2, ArrAkunCB2 As New ArrayList
    Dim varMataUang As String
    Dim kursUangBaru As Double
    Dim checkSisaHutang As String
    Dim no_fakturDeposit As String

    Dim arrMUA, arrSupp, arrRek, ArrRencana, arrakunrek1 As New ArrayList
    Dim arrMUA2, arrSupp2, arrRek2, ArrRencana2, arrakunrek2 As New ArrayList

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvFak = ListViewMT11.Items(No_Index).Text
        LvTgl = ListViewMT11.Items(No_Index).SubItems(1).Text
        LvKP = ListViewMT11.Items(No_Index).SubItems(2).Text
        LvNP = ListViewMT11.Items(No_Index).SubItems(3).Text
        LvMT = ListViewMT11.Items(No_Index).SubItems(4).Text
        LvJml = ListViewMT11.Items(No_Index).SubItems(5).Text
        Lvkurshpp = ListViewMT11.Items(No_Index).SubItems(6).Text
        Lvnopo = ListViewMT11.Items(No_Index).SubItems(7).Text
        Lvlks = ListViewMT11.Items(No_Index).SubItems(8).Text
        Lvdec = ListViewMT11.Items(No_Index).SubItems(9).Text
        Lvnodec = ListViewMT11.Items(No_Index).SubItems(10).Text
        Lvselisih = ListViewMT11.Items(No_Index).SubItems(11).Text
        LvRencana = ListViewMT11.Items(No_Index).SubItems(12).Text
    End Sub

    Private Sub Get_Isi_Listview2(ByVal No_Index As Integer)
        LvFak2 = ListViewMT22.Items(No_Index).Text
        LvTgl2 = ListViewMT22.Items(No_Index).SubItems(1).Text
        LvKP2 = ListViewMT22.Items(No_Index).SubItems(2).Text
        LvNP2 = ListViewMT22.Items(No_Index).SubItems(3).Text
        LvMT2 = ListViewMT22.Items(No_Index).SubItems(4).Text
        LvJml2 = ListViewMT22.Items(No_Index).SubItems(5).Text
        Lvkurshpp2 = ListViewMT22.Items(No_Index).SubItems(6).Text
        Lvnopo2 = ListViewMT22.Items(No_Index).SubItems(7).Text
        Lvlks2 = ListViewMT22.Items(No_Index).SubItems(8).Text
        Lvdec2 = ListViewMT22.Items(No_Index).SubItems(9).Text
        Lvnodec2 = ListViewMT22.Items(No_Index).SubItems(10).Text
        Lvselisih2 = ListViewMT22.Items(No_Index).SubItems(11).Text
        LvRencana2 = ListViewMT22.Items(No_Index).SubItems(12).Text
    End Sub

    Private Sub ambil_kurs1()
        Try
            OpenConn()


            SQL = ";with cte_a as("
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, b.Nilai as Masuk, 0 as Keluar, b.Mata_Uang, b.No_Rek, a.jenis_deposit as jenis "
            SQL = SQL & ",a.kurs_idr as kurs, a.kurs_idr*b.nilai as Masuk_IDR, 0 as Keluar_IDR, b.kode_supplier, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier),b.Kode_Supplier) as nama, keterangan, "
            SQL = SQL & "a.no_faktur "
            SQL = SQL & "from Deposit_Pelunasan a, Deposit_Val_Pel_Biaya_Import_By_Perusahaan b where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.No_Faktur = b.No_Faktur and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.status is null and ("


            SQL = SQL & "(b.kode_supplier = '" & arrSupp.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.no_rek = '" & arrRek.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.mata_uang = '" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan) as nama,  c.keterangan, "
            SQL = SQL & " a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, Deposit_Pelunasan c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.no_faktur  and c.status is null and "
            SQL = SQL & "a.jenis='TRANSFER' and  a.Kode_Perusahaan = '" & KodePerusahaan & "' and ( "

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan) as nama,  c.keterangan, "
            SQL = SQL & " a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, Deposit_Pelunasan c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.no_faktur  and c.status is null and "
            SQL = SQL & "a.jenis='TF DECLARE' and  a.Kode_Perusahaan = '" & KodePerusahaan & "' and ( "

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan)  as nama,  c.keterangan, "
            SQL = SQL & " a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, Deposit_Pelunasan c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.no_faktur  and c.status is null and "
            SQL = SQL & "a.jenis='DECLARE' and  a.Kode_Perusahaan = '" & KodePerusahaan & "' and ( "

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan)  as nama,  c.keterangan, "
            SQL = SQL & "a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, val_pemb_import c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.No_Val  and c.status is null and "
            SQL = SQL & "a.jenis='PEL_BAHAN' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and ( "

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan)  as nama,  c.keterangan, "
            SQL = SQL & "a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, Val_Pel_Hutang_Loading_Barang c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.No_Val  and c.status is null and "
            SQL = SQL & "a.jenis='PEL_BAHAN_TDK_STOCK' and  a.Kode_Perusahaan = '" & KodePerusahaan & "' and ( "

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan)  as nama,  c.keterangan, "
            SQL = SQL & " a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, val_pel_biaya_import_by_perusahaan c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.No_Val  and c.status is null and "
            SQL = SQL & "a.jenis='AGENT' and  a.Kode_Perusahaan = '" & KodePerusahaan & "' and ("

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek.Item(ComboBoxRek1.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            'SQL = SQL & "order by No_Rek, Tanggal ,jam, keluar desc  "
            'SQL = SQL & ") "
            SQL = SQL & ") select round((sum(round(Masuk_IDR,2))-SUM(round(Keluar_IDR,2)))/(sum(round(masuk,2))-sum(round(keluar,2))),5) as kurs from cte_a "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("kurs")) = "" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Rekening Kosong . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        ComboBoxRek1.SelectedIndex = -1
                        Exit Sub
                    End If

                    TextBoxKurs1.Text = dr("kurs")
                    TextBoxKurs1.Enabled = False
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Rekening Kosong . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    TextBoxKurs1.Enabled = True
                End If
            End Using


            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub ambil_kurs2()
        Try
            OpenConn()


            SQL = ";with cte_a as("
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, b.Nilai as Masuk, 0 as Keluar, b.Mata_Uang, b.No_Rek, a.jenis_deposit as jenis "
            SQL = SQL & ",a.kurs_idr as kurs, a.kurs_idr*b.nilai as Masuk_IDR, 0 as Keluar_IDR, b.kode_supplier, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier),b.Kode_Supplier) as nama, keterangan, "
            SQL = SQL & "a.no_faktur "
            SQL = SQL & "from Deposit_Pelunasan a, Deposit_Val_Pel_Biaya_Import_By_Perusahaan b where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.No_Faktur = b.No_Faktur and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.status is null and ("


            SQL = SQL & "(b.kode_supplier = '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.no_rek = '" & arrRek2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.mata_uang = '" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan) as nama,  c.keterangan, "
            SQL = SQL & " a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, Deposit_Pelunasan c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.no_faktur  and c.status is null and "
            SQL = SQL & "a.jenis='TRANSFER' and  a.Kode_Perusahaan = '" & KodePerusahaan & "' and ( "

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan) as nama,  c.keterangan, "
            SQL = SQL & " a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, Deposit_Pelunasan c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.no_faktur  and c.status is null and "
            SQL = SQL & "a.jenis='TF DECLARE' and  a.Kode_Perusahaan = '" & KodePerusahaan & "' and ( "

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan)  as nama,  c.keterangan, "
            SQL = SQL & " a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, Deposit_Pelunasan c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.no_faktur  and c.status is null and "
            SQL = SQL & "a.jenis='DECLARE' and  a.Kode_Perusahaan = '" & KodePerusahaan & "' and ( "

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan)  as nama,  c.keterangan, "
            SQL = SQL & "a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, val_pemb_import c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.No_Val  and c.status is null and "
            SQL = SQL & "a.jenis='PEL_BAHAN' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and ( "

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan)  as nama,  c.keterangan, "
            SQL = SQL & "a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, Val_Pel_Hutang_Loading_Barang c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.No_Val  and c.status is null and "
            SQL = SQL & "a.jenis='PEL_BAHAN_TDK_STOCK' and  a.Kode_Perusahaan = '" & KodePerusahaan & "' and ( "

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            SQL = SQL & "Union All "

            SQL = SQL & "Select a.Kode_Perusahaan, a.Tanggal, a.jam, 0 as Masuk, a.Nilai as Keluar, b.MUA_Tujuan as Mata_Uang, b.rek_tujuan as No_rek, "
            SQL = SQL & "a.jenis,a.kurs as kurs, 0 as Masuk_IDR, a.nilai_idr as Keluar_IDR, b.Kode_Supplier_Tujuan, isnull((select nama from suppliers x where x.Kode_Perusahaan=b.Kode_Perusahaan and x.Kode_Supplier=b.Kode_Supplier_Tujuan),b.Kode_Supplier_Tujuan)  as nama,  c.keterangan, "
            SQL = SQL & " a.no_faktur_masuk from "
            SQL = SQL & "Log_Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, Deposit_Pelunasan b, val_pel_biaya_import_by_perusahaan c where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur_Keluar=b.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.no_faktur_masuk= c.No_Val  and c.status is null and "
            SQL = SQL & "a.jenis='AGENT' and  a.Kode_Perusahaan = '" & KodePerusahaan & "' and ("

            SQL = SQL & "(b.Kode_Supplier_Tujuan = '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.rek_tujuan = '" & arrRek2.Item(ComboBoxRek2.SelectedIndex).ToString & "' and b.MUA_Tujuan = '" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "') or "

            If Len(SQL) >= 2 Then
                SQL = Strings.Left(SQL, Len(SQL) - 3)
            End If

            SQL = SQL & ") "

            'SQL = SQL & "order by No_Rek, Tanggal ,jam, keluar desc  "
            'SQL = SQL & ") "
            SQL = SQL & ") select round((sum(round(Masuk_IDR,2))-SUM(round(Keluar_IDR,2)))/(sum(round(masuk,2))-sum(round(keluar,2))),5) as kurs from cte_a "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("kurs")) = "" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Rekening Kosong . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        ComboBoxRek2.SelectedIndex = -1
                        Exit Sub
                    End If
                    TextBoxKurs2.Text = dr("kurs")
                    TextBoxKurs2.Enabled = False
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Rekening Kosong . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    TextBoxKurs2.Enabled = True
                End If
            End Using


            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub ambil_kurs_declare()
        Try
            OpenConn()


            SQL = "Select round((sum(total_idr)/sum(total_MUA)),5) as Kurs_Akhir From "
            SQL = SQL & "Pelunasan_Val_Pel_Biaya_Import_By_Perusahaan where kode_perusahaan = '" & KodePerusahaan & "' and "

            SQL = SQL & "No_Val in (" & Lvnodec & ")"

            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBoxKurs1.Text = dr("Kurs_Akhir")
                    TextBoxKurs1.Enabled = False
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Declare Kosong . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    TextBoxKurs1.Enabled = True
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ambil_kurs_Sup1()
        Try
            OpenConn()

            Dim Flag_Gabung As String = ""
            Dim Faktur_Gabung As String = ""
            Dim Faktur_declare As String = ""

            

            For index As Integer = 0 To ListViewMT11.Items.Count - 1
                Get_Isi_Listview(index)

                Dim flag_gabung_declare As String = ""

                SQL = "select isnull(flag_Gabung_declare,'T') as flag_Gabung_declare from loading_barang where status is null and "
                SQL = SQL & " id_rencana='" & LvRencana & "' and kode_Perusahaan='" & KodePerusahaan & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        flag_gabung_declare = dr("flag_Gabung_declare")
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Tidak Di temukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                Dim no_faktur_gabung = ""
                If flag_gabung_declare = "Y" Then


                    SQL = "select a.no_faktur from Pelunasan_Declare_Gabungan a, Pelunasan_Declare_Gabungan_Detail2 b where "
                    SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.NO_faktur=b.NO_faktur and a.status is null and "
                    SQL = SQL & "b.ID_Rencana='" & LvRencana & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            no_faktur_gabung = dr("no_faktur")
                        Else
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Data Gabungan Tidak Di temukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                End If

                SQL = "select*,"

                SQL = SQL & "CASE when flag_gabung_declare = 'Y' then "
                SQL = SQL & "isnull(round(("
                SQL = SQL & "select round(sum(b.Nilai*b.Kurs_IDR)/SUM(b.Nilai),5) as kurs_akhir from "
                SQL = SQL & "deposit_pelunasan a,Deposit_Val_Pel_Biaya_Import_By_Perusahaan b "
                SQL = SQL & "where Status Is null And a.Kode_Perusahaan = b.Kode_perusahaan And a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.Kode_supplier='" & arrSupp.Item(ComboBoxRek1.SelectedIndex) & "' and "
                SQL = SQL & "b.No_Rek='" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "' and b.Mata_Uang='" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "' and b.Kode_Perusahaan ='" & KodePerusahaan & "'"
                SQL = SQL & "AND B.ID_RENCANA IN( "
                SQL = SQL & "select B.ID_RENCANA from  Pelunasan_Declare_Gabungan a , Pelunasan_Declare_Gabungan_Detail1 b where "
                SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.NO_faktur=b.NO_faktur and a.status is null and a.nO_faktur='" & no_faktur_gabung & "' "
                SQL = SQL & ") "
                SQL = SQL & "),5),0)  "
                SQL = SQL & "ELSE "
                SQL = SQL & "isnull(round(("
                SQL = SQL & "select round(sum(b.Nilai*b.Kurs_IDR)/SUM(b.Nilai),5) as kurs_akhir from "
                SQL = SQL & "deposit_pelunasan a,Deposit_Val_Pel_Biaya_Import_By_Perusahaan b "
                SQL = SQL & "where Status Is null And a.Kode_Perusahaan=b.Kode_perusahaan and a.No_Faktur = b.No_Faktur And ROUND(b.Sisa, 2) <> 0 "
                SQL = SQL & "and b.Id_Rencana='" & LvRencana & "' and "
                SQL = SQL & "b.Kode_supplier='" & arrSupp.Item(ComboBoxRek1.SelectedIndex) & "' and "
                SQL = SQL & "b.No_Rek='" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "' and b.Mata_Uang='" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "' and b.Kode_Perusahaan ='" & KodePerusahaan & "'"
                SQL = SQL & "),5),0) END as Kurs_Akhir "

                SQL = SQL & ",isnull((select  top(1) x.no_faktur  "
                SQL = SQL & "from pelunasan_declare_gabungan x, pelunasan_declare_gabungan_detail2 y "
                SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.status is null "
                SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.id_rencana=a.id_rencana),'') as Faktur_gabung_Declare  "

                SQL = SQL & ",isnull((select STRING_AGG('''' + x.no_val +'''',',') from Val_Pel_Declare_Invoice_Import X, detail_Val_Pel_Declare_Invoice_Import Y "
                SQL = SQL & "where X.Kode_Perusahaan=Y.Kode_Perusahaan and X.No_Val=Y.No_Val and X.status is null and "
                SQL = SQL & "Y.Kode_Perusahaan=a.Kode_Perusahaan and Y.No_faktur=a.Id_Rencana ),'') as No_Declare "

                SQL = SQL & ",isnull((select Flag_Lunas_Declare from submit_PO x where "
                SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.id_rencana=a.id_rencana and x.Status is null),'')as Flag_Lunas_Declare_submit "


                SQL = SQL & ",isnull((select Kode_Supplier from Rencana_Order x where "
                SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.id_rencana=a.id_rencana and x.Status is null),'')as Kode_Supplier "
                SQL = SQL & "from loading_barang a where Id_Rencana='" & LvRencana & "' and a.Status is null "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then


                        If arrSupp.Item(ComboBoxRek1.SelectedIndex) <> dr("Kode_Supplier") Then
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Supplier Berbeda . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            TextBoxKurs1.Enabled = True
                            ComboBoxRek1.SelectedIndex = -1
                            Exit Sub
                        End If

                        If General_Class.CekNULL(dr("flag_gabung_declare")) <> "Y" Then
                            If General_Class.CekNULL(dr("Flag_Lunas_Declare_submit")) <> "Y" Then
                                dr.Close()
                                CloseConn()
                                MessageBox.Show("Tidak ada data Declare . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBoxKurs1.Enabled = True
                                ComboBoxRek1.SelectedIndex = -1
                                Exit Sub
                            End If


                            If index = 0 Then
                                Flag_Gabung = General_Class.CekNULL(dr("flag_gabung_declare"))
                                Faktur_declare = General_Class.CekNULL(dr("No_Declare"))
                            End If

                            If Faktur_declare <> General_Class.CekNULL(dr("No_Declare")) Then
                                dr.Close()
                                CloseConn()
                                MessageBox.Show("Terdapat data PO Berbeda . . !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBoxKurs1.Enabled = True
                                ComboBoxRek1.SelectedIndex = -1
                                Exit Sub
                            End If
                        Else
                            If index = 0 Then
                                Flag_Gabung = General_Class.CekNULL(dr("flag_gabung_declare"))
                                Faktur_Gabung = General_Class.CekNULL(dr("Faktur_gabung_Declare"))
                            End If

                            If Faktur_Gabung <> General_Class.CekNULL(dr("Faktur_gabung_Declare")) Then
                                dr.Close()
                                CloseConn()
                                MessageBox.Show("Terdapat data PO Berbeda . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBoxKurs1.Enabled = True
                                ComboBoxRek1.SelectedIndex = -1
                                Exit Sub
                            End If
                        End If

                        TextBoxKurs1.Text = dr("Kurs_Akhir")
                        TextBoxKurs1.Enabled = False
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Tidak ada data Declare . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        TextBoxKurs1.Enabled = True
                        ComboBoxRek1.SelectedIndex = -1
                        Exit Sub
                    End If
                End Using
            Next

            

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ambil_kurs_Sup2()
        Try
            OpenConn()

            Dim Flag_Gabung As String = ""
            Dim Faktur_Gabung As String = ""
            Dim Faktur_declare As String = ""

            For index As Integer = 0 To ListViewMT22.Items.Count - 1
                Get_Isi_Listview2(index)

                SQL = "select*,"

                SQL = SQL & "CASE when flag_gabung_declare = 'Y' then "
                SQL = SQL & "isnull(round((select  top(1) x.kurs_akhir  "
                SQL = SQL & "from pelunasan_declare_gabungan x, pelunasan_declare_gabungan_detail2 y "
                SQL = SQL & "where(x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur And x.status Is null) "
                SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.id_rencana=a.id_rencana),5),0)  "
                SQL = SQL & "ELSE "
                SQL = SQL & "isnull(round((select sum(x.Total_IDR)/sum(x.Nilai_Tambahan) from Val_Pel_Declare_Invoice_Import x, "
                SQL = SQL & "detail_Val_Pel_Declare_Invoice_Import y where x.Kode_Perusahaan= y.Kode_Perusahaan and x.No_Val=y.No_Val "
                SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.No_Faktur=a.id_rencana and x.Status is null),5),0) END as Kurs_Akhir "

                SQL = SQL & ",isnull((select  top(1) x.no_faktur  "
                SQL = SQL & "from pelunasan_declare_gabungan x, pelunasan_declare_gabungan_detail2 y "
                SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.status is null "
                SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.id_rencana=a.id_rencana),'') as Faktur_gabung_Declare  "

                SQL = SQL & ",isnull((select STRING_AGG('''' + x.no_val +'''',',') from Val_Pel_Declare_Invoice_Import X, detail_Val_Pel_Declare_Invoice_Import Y "
                SQL = SQL & "where X.Kode_Perusahaan=Y.Kode_Perusahaan and X.No_Val=Y.No_Val and X.status is null and "
                SQL = SQL & "Y.Kode_Perusahaan=a.Kode_Perusahaan and Y.No_faktur=a.Id_Rencana ),'') as No_Declare "

                SQL = SQL & ",isnull((select Flag_Lunas_Declare from submit_PO x where "
                SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.id_rencana=a.id_rencana and x.Status is null),'')as Flag_Lunas_Declare_submit "


                SQL = SQL & ",isnull((select Kode_Supplier from Rencana_Order x where "
                SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.id_rencana=a.id_rencana and x.Status is null),'')as Kode_Supplier "
                SQL = SQL & "from loading_barang a where Id_Rencana='" & LvRencana2 & "' and a.Status is null "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then


                        If arrSupp2.Item(ComboBoxRek2.SelectedIndex) <> dr("Kode_Supplier") Then
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Supplier Berbeda . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            TextBoxKurs2.Enabled = True
                            ComboBoxRek2.SelectedIndex = -1
                            Exit Sub
                        End If

                        If General_Class.CekNULL(dr("flag_gabung_declare")) <> "Y" Then
                            If General_Class.CekNULL(dr("Flag_Lunas_Declare_submit")) <> "Y" Then
                                dr.Close()
                                CloseConn()
                                MessageBox.Show("Tidak ada data Declare . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBoxKurs2.Enabled = True
                                ComboBoxRek2.SelectedIndex = -1
                                Exit Sub
                            End If


                            If index = 0 Then
                                Flag_Gabung = General_Class.CekNULL(dr("flag_gabung_declare"))
                                Faktur_declare = General_Class.CekNULL(dr("No_Declare"))
                            End If

                            If Faktur_declare <> General_Class.CekNULL(dr("No_Declare")) Then
                                dr.Close()
                                CloseConn()
                                MessageBox.Show("Terdapat data PO Berbeda . . !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBoxKurs2.Enabled = True
                                ComboBoxRek2.SelectedIndex = -1
                                Exit Sub
                            End If
                        Else
                            If index = 0 Then
                                Flag_Gabung = General_Class.CekNULL(dr("flag_gabung_declare"))
                                Faktur_Gabung = General_Class.CekNULL(dr("Faktur_gabung_Declare"))
                            End If

                            If Faktur_Gabung <> General_Class.CekNULL(dr("Faktur_gabung_Declare")) Then
                                dr.Close()
                                CloseConn()
                                MessageBox.Show("Terdapat data PO Berbeda . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBoxKurs2.Enabled = True
                                ComboBoxRek2.SelectedIndex = -1
                                Exit Sub
                            End If
                        End If

                        TextBoxKurs2.Text = dr("Kurs_Akhir")
                        TextBoxKurs2.Enabled = False
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Tidak ada data Declare . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        TextBoxKurs2.Enabled = True
                        ComboBoxRek2.SelectedIndex = -1
                        Exit Sub
                    End If
                End Using
            Next



            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ambil_kurs_declare2()
        Try
            OpenConn()

            SQL = "Select round((sum(total_idr)/sum(total_MUA)),5) as Kurs_Akhir From "
            SQL = SQL & "Pelunasan_Val_Pel_Biaya_Import_By_Perusahaan where kode_perusahaan = '" & KodePerusahaan & "' and "

            SQL = SQL & "No_Val in (" & Lvnodec2 & ")"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBoxKurs2.Text = dr("Kurs_Akhir")
                    TextBoxKurs2.Enabled = False
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Declare Kosong . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    TextBoxKurs2.Enabled = True
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub cetak()
        'Try

        '    OpenConn()

        '    SQL = "select kode_perusahaan from val_pemb where "
        '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
        '    Using Ds = BindingTrans(SQL)
        '        If Ds.Tables("MyTable").Rows.Count <> 0 Then
        '            Dim CrDoc As New Faktur_Pelunasan_Pemb    'Nama file CR
        '            CrDoc.SetDataSource(Ds)
        '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '            CrDoc.PrintOptions.PrinterName = PrinterName
        '            CrDoc.RecordSelectionFormula = "{val_pemb.Kode_Perusahaan} = '" & KodePerusahaan & "' and {val_pemb.no_val} = '" & TxtFaktur.Text.Trim & "'"

        '            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        '            doctoprint.PrinterSettings.PrinterName = PrinterName
        '            Dim rawKind As Integer
        '            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        '            For i = doctoprint.PrinterSettings.PaperSizes.Count - 1 To 0 Step -1
        '                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
        '                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
        '                    CrDoc.PrintOptions.PaperSize = rawKind
        '                    Exit For
        '                End If
        '            Next

        '            CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
        '            CrDoc.PrintToPrinter(1, False, 1, 99)
        '        End If
        '    End Using

        '    CloseConn()

        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub


    Private Sub Cari(ByVal param As String)

        If param = "Tidak1" Then
            If ComboBoxNP1.SelectedIndex = -1 Then
                MessageBox.Show("Perusahaan belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBoxNP1.Focus() : Exit Sub
            ElseIf ComboBoxMT1.SelectedIndex = -1 Then
                MessageBox.Show("Mata Uang belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBoxMT1.Focus() : Exit Sub
            End If
            ListViewMT1.Items.Clear()
        ElseIf param = "Tidak2" Then
            If ComboBoxNP2.SelectedIndex = -1 Then
                MessageBox.Show("Perusahaan belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBoxNP2.Focus() : Exit Sub
            ElseIf ComboBoxMT2.SelectedIndex = -1 Then
                MessageBox.Show("Mata Uang belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBoxMT2.Focus() : Exit Sub
            End If
            ListViewMT2.Items.Clear()
        End If

        Try
            OpenConn()

            Dim lv As New ListViewItem


            SQL = ";with cte_a as( "
            SQL = SQL & "select c.flag_declare, Format(d.Tanggal_PO,'MM.dd') as no_po,d.Lokasi, a.No_Faktur, a.Id_rencana, a.Tanggal+a.Jam as Tgl, a.Keterangan, "
            SQL = SQL & "c.Kode_Perusahaan_Biaya_import, b.mata_Uang, c.Nama, b.nilai, "
            SQL = SQL & "isnull((select sum(Y.byr) from Val_Pel_Biaya_import_by_Perusahaan X, "
            SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
            SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = b.NO_Faktur and Y.Kode_Perusahaan_Biaya_Import = "
            SQL = SQL & "b.Kode_Perusahaan_Biaya_import and Y.Mata_Uang = b.Mata_Uang AND X.STATUS IS NULL),0) as sudah_bayar, c.jenis, "

            SQL = SQL & "isnull((select top(1) kurs from detail_transaksi_biaya_import x where x.Kode_Perusahaan=b.Kode_Perusahaan and "
            SQL = SQL & "x.no_faktur=b.no_faktur and x.Kode_Perusahaan_biaya_Import = b.Kode_Perusahaan_biaya_import and x.Mata_Uang=b.Mata_Uang),0) as kurs_hpp "

            SQL = SQL & " ,'' as no_declare ,'T' as flag_lunas_declare "

            SQL = SQL & ",round(isnull((select sum(Nilai_Selisih) from hpp_import x, HPP_Import_Log_Selisih y where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_Faktur=y.NO_Faktur and x.Status is null "
            SQL = SQL & " and x.Kode_Perusahaan=a.Kode_Perusahaan and x.Id_Rencana=a.id_rencana and y.jenis=b.Kode_Perusahaan_Biaya_Import ),0),0) as selisih_kurs_sebelum "

            SQL = SQL & ",isnull((select sum(Y.selisih_po_Sebelum) from Val_Pel_Biaya_import_by_Perusahaan X, "
            SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
            SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = b.NO_Faktur and Y.Kode_Perusahaan_Biaya_Import = "
            SQL = SQL & "b.Kode_Perusahaan_Biaya_import and Y.Mata_Uang = b.Mata_Uang and x.status is null),0) as selisih_PO "

            SQL = SQL & "from transaksi_Biaya_Import a, Detail_transaksi_Biaya_Import_by_Perusahaan b, Perusahaan_Biaya_Import c, Rencana_Order d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_faktur = b.No_faktur And a.Status Is null "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Perusahaan_Biaya_Import = "
            SQL = SQL & "c.Kode_Perusahaan_biaya_import and b.Flag_Lunas is null and a.Kode_Perusahaan=d.Kode_Perusahaan and a.Id_rencana=d.Id_Rencana and d.flag_HPP='Y' "

            SQL = SQL & "union all "

            SQL = SQL & "select c.flag_declare, Format(d.Tanggal_PO,'MM.dd') as no_po ,d.Lokasi, a.No_Faktur, a.Id_rencana, a.Tanggal+a.Jam as Tgl, a.Keterangan, "
            SQL = SQL & "c.Kode_Perusahaan_Biaya_import, b.mata_Uang, c.Nama, b.nilai, "
            SQL = SQL & "isnull((select sum(Y.byr) from Val_Pel_Biaya_import_by_Perusahaan X, "
            SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
            SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = b.NO_Faktur and Y.Kode_Perusahaan_Biaya_Import = "
            SQL = SQL & "b.Kode_Perusahaan_Biaya_import and Y.Mata_Uang = b.Mata_Uang  AND X.STATUS IS NULL),0) as sudah_bayar, c.jenis, "

            SQL = SQL & "isnull((select nilai from hpp_import x, Kurs_HPP_import y where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_Faktur=y.no_faktur "
            SQL = SQL & "and x.status is null and y.jenis='FREIGHT' and x.Kode_Perusahaan=a.Kode_Perusahaan and x.Id_rencana=a.Id_rencana and y.mata_uang=b.mata_uang),0) as kurs_hpp, "

            SQL = SQL & "isnull((select STRING_AGG('''' + x.no_val +'''',',') from Pelunasan_Val_Pel_Biaya_Import_By_Perusahaan X, Pelunasan_Val_Pel_Biaya_Import_By_Perusahaan_detail Y "
            SQL = SQL & "where X.Kode_Perusahaan=Y.Kode_Perusahaan and X.No_Val=Y.No_Val and X.status is null and "
            SQL = SQL & "Y.Kode_Perusahaan=a.Kode_Perusahaan and Y.ID_Rencana=a.Id_Rencana and Y.Kode_Perusahaan_Biaya_Import=b.Kode_Perusahaan_Biaya_Import),'') as No_Declare "

            SQL = SQL & ",b.flag_lunas_declare "

            SQL = SQL & ",round(isnull((select sum(Nilai_Selisih) from hpp_import x, HPP_Import_Log_Selisih y where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_Faktur=y.NO_Faktur and x.Status is null "
            SQL = SQL & " and x.Kode_Perusahaan=a.Kode_Perusahaan and x.Id_Rencana=a.id_rencana and y.jenis=b.Kode_Perusahaan_Biaya_Import ),0),0) as selisih_kurs_sebelum "

            SQL = SQL & ",isnull((select sum(Y.selisih_po_Sebelum) from Val_Pel_Biaya_import_by_Perusahaan X, "
            SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
            SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = b.NO_Faktur and Y.Kode_Perusahaan_Biaya_Import = "
            SQL = SQL & "b.Kode_Perusahaan_Biaya_import and Y.Mata_Uang = b.Mata_Uang and x.status is null),0) as selisih_PO "

            SQL = SQL & "from transaksi_Biaya_Import3 a, Detail_transaksi_Biaya_Import3_by_Perusahaan b, Perusahaan_Biaya_Import c, Rencana_Order d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_faktur = b.No_faktur And a.Status Is null "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Perusahaan_Biaya_Import = "
            SQL = SQL & "c.Kode_Perusahaan_biaya_import and b.Flag_Lunas is null and a.Kode_Perusahaan=d.Kode_Perusahaan and a.Id_rencana=d.Id_Rencana and d.flag_HPP='Y' "
            SQL = SQL & ")"
            SQL = SQL & "select* from cte_a where Jenis='LUAR' and "
            If param = "Tidak1" Then
                SQL = SQL & "Mata_Uang = '" & ComboBoxMT1.Text & "' "
                If ComboBoxNP1.SelectedIndex <> 0 Then
                    SQL = SQL & "and Kode_Perusahaan_Biaya_Import ='" & ArrNP1.Item(ComboBoxNP1.SelectedIndex - 1) & "' "
                End If

            ElseIf param = "Tidak2" Then
                SQL = SQL & "Mata_Uang = '" & ComboBoxMT2.Text & "' "
                If ComboBoxNP2.SelectedIndex <> 0 Then
                    SQL = SQL & "and Kode_Perusahaan_Biaya_Import ='" & ArrNP2.Item(ComboBoxNP2.SelectedIndex - 1) & "' "
                End If

            End If


            SQL = SQL & "order by no_po, Nama "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    If param = "Tidak1" Then
                        lv = ListViewMT1.Items.Add(Dr("no_faktur"))
                    ElseIf param = "Tidak2" Then
                        lv = ListViewMT2.Items.Add(Dr("no_faktur"))
                    End If

                    lv.SubItems.Add(Dr("id_rencana"))
                    lv.SubItems.Add(Dr("Keterangan"))
                    lv.SubItems.Add(Format(Dr("tgl"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("Kode_Perusahaan_Biaya_import"))
                    lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("Mata_Uang"))
                    Dim x_tot As Double = Dr("nilai")
                    lv.SubItems.Add(Format(x_tot, "N2"))
                    lv.SubItems.Add(Format(Dr("sudah_bayar"), "N2"))
                    lv.SubItems.Add(Format(Dr("kurs_hpp"), "N5"))
                    lv.SubItems.Add(Dr("No_PO"))
                    lv.SubItems.Add(Dr("Lokasi"))
                    lv.SubItems.Add(General_Class.CekNULL(Dr("Flag_Declare")))
                    lv.SubItems.Add(General_Class.CekNULL(Dr("No_Declare")))
                    lv.SubItems.Add(Format(Dr("selisih_kurs_sebelum") - (Dr("selisih_PO")), "N2"))
                    'lv.SubItems.Add(0)
                    'lv.SubItems.Add(Format(x_tot - Dr("pernah_val"), "N2"))
                    'lv.SubItems.Add(Dr("Mata_uang"))
                    'lv.SubItems.Add(Dr("kode_stock_owner_import"))
                    'lv.SubItems.Add(Format(Dr("kurs"), "N2"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            'CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Hitung()
        Dim Grand As Double = 0
        Dim GrandHPP As Double = 0
        Dim Grand2 As Double = 0
        Dim Grand2HPP As Double = 0
        Dim TotalIdr As Double = 0

        For i As Integer = 0 To ListViewMT11.Items.Count - 1
            Get_Isi_Listview(i)

            Grand = Grand + Val(HilangkanTanda(LvJml))
            GrandHPP = GrandHPP + (Val(HilangkanTanda(LvJml)) * Val(HilangkanTanda(Lvkurshpp)))
        Next

        For i As Integer = 0 To ListViewMT22.Items.Count - 1
            Get_Isi_Listview2(i)

            Grand2 = Grand2 + Val(HilangkanTanda(LvJml2))
            Grand2HPP = Grand2HPP + (Val(HilangkanTanda(LvJml2)) * Val(HilangkanTanda(Lvkurshpp2)))

        Next

        TextBoxtotHPP1.Text = (Format(GrandHPP, "N2"))
        TextBoxtotHPP2.Text = (Format(Grand2HPP, "N2"))




        TextBoxtot1.Text = (Format(Grand, "N2"))
        TextBoxtot2.Text = (Format(Grand2, "N2"))
        TextBoxSLS1.Text = Format((Val(TextBoxDBY1.Text) + Val(HilangkanTanda(TextBoxDPT1.Text))) - Val(HilangkanTanda(TextBoxtot1.Text)) - Val(HilangkanTanda(TextBoxPindahKurs.Text)) - Val(HilangkanTanda(TextBoxsimpan1.Text)), "N2")

        ' ''If Val(HilangkanTanda(TextBoxSLS1.Text)) < 0 Then
        ' ''    MessageBox.Show("Nilai Pindah Kurs/Simpanan Lebih Besar dari Jumlah Sisa Bayar")
        ' ''    TextBoxPindahKurs.Text = 0
        ' ''    TextBoxsimpan1.Text = 0
        ' ''    TextBoxSLS1.Text = Format((Val(TextBoxDBY1.Text) + Val(HilangkanTanda(TextBoxDPT1.Text))) - Val(HilangkanTanda(TextBoxtot1.Text)) - TextBoxPindahKurs.Text - TextBoxsimpan1.Text, "N2")
        ' ''End If

        TextBoxtotIDR1.Text = Format(((Val(HilangkanTanda(TextBoxDBY1.Text)) + Val(HilangkanTanda(TextBoxDPT1.Text))) * Val(TextBoxKurs1.Text)) + Val(HilangkanTanda(TextBoxADM1.Text)), "N0")

        TextBoxKV2.Text = Format(Val(HilangkanTanda(TextBoxPindahKurs.Text)) * Val(TextBoxKV1.Text), "N2")
        TextBoxSLS2.Text = Format((Val(HilangkanTanda(TextBoxDBY2.Text)) + Val(HilangkanTanda(TextBoxKV2.Text)) + Val(HilangkanTanda(TextBoxDPT2.Text))) - Val(HilangkanTanda(TextBoxtot2.Text)), "N2")
        TextBoxtotIDR2.Text = Format(((Val(HilangkanTanda(TextBoxDBY2.Text)) + Val(HilangkanTanda(TextBoxDPT2.Text))) * Val(TextBoxKurs2.Text)) + Val(HilangkanTanda(TextBoxADM2.Text)), "N0")
        'TextBox13.Text = (Format(TotalIdr, "N2"))

        TextBoxSelisihKurs1.Text = Val(HilangkanTanda(TextBoxtotIDR1.Text)) - GrandHPP
        TextBoxSelisihKurs2.Text = Val(HilangkanTanda(TextBoxtotIDR2.Text)) - Grand2HPP

    End Sub

    Private Sub Get_No_Faktur()
        TxtFaktur.Text = fValPelBI & Format(DateTimePicker1.Value, "MMyy") & "-" & _
                             General_Class.Get_Last_Number2("val_pel_biaya_import_by_Perusahaan", "no_val", 5, _
                             "Kode_perusahaan", KodePerusahaan, _
                             "And", "substring(no_val, 1, " & Len(fValPelBI) + 4 & ")", fValPelBI & Format(DateTimePicker1.Value, "MMyy"))
    End Sub
    Private Sub Get_No_Faktur_Deposit()
        Dim fTransferRekening As String = "TRS"
        no_fakturDeposit = fTransferRekening & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" & _
                             General_Class.Get_Last_Number2("Deposit_Pelunasan", "No_Faktur", JumlahDigit, _
                             "Kode_perusahaan", KodePerusahaan, _
                             "And", "substring(No_Faktur,1," & Len(fTransferRekening) + 6 & ")", _
                              fTransferRekening & "-" & Format(DateTimePicker1.Value, "MM/yy"))
    End Sub

    Private Sub Kosong_Bawah()
        TextBoxFktr.Text = ""
        TextBoxtgl.Text = ""
        TextBoxKP.Text = ""
        TextBoxNP.Text = ""
        TextBoxjml.Text = ""
        TextBoxjns.Text = ""
        TextBoxMT.Text = ""
        TextBoxbyr.Text = ""
        TextBoxselisih.Text = ""
        'TextBox5.Text = ""
        'TextBox6.Text = ""
        'TextBox6.Enabled = False
        TextBoxjml.Enabled = False
    End Sub

    Private Sub Kosong()
        GetTime()
        DateTimePicker1.Value = Tanggal_Sekarang
        DateTimePicker1.Enabled = True
        TextBoxKurs1.Enabled = True
        TextBoxKurs2.Enabled = True
        TextBoxbyr.Text = "" : TextBoxket.Text = ""
        TextBoxtot1.Text = 0 : TextBoxtot2.Text = 0
        TextBoxDPT1.Text = 0 : TextBoxDPT2.Text = 0
        TextBoxDBY1.Text = 0 : TextBoxDBY2.Text = 0
        TextBoxSLS1.Text = 0 : TextBoxSLS2.Text = 0
        TextBoxKV1.Text = 0 : TextBoxKV2.Text = 0
        TextBoxADM1.Text = 0 : TextBoxADM2.Text = 0
        TextBoxKurs1.Text = 0 : TextBoxKurs2.Text = 0
        TextBoxtotIDR1.Text = 0 : TextBoxtotIDR2.Text = 0
        TextBoxPindahKurs.Text = 0 : TextBoxsimpan1.Text = 0

        TabPage2.Text = "Mata Uang 2"
        txt21.Text = " "
        txt22.Text = " "
        txt23.Text = " "
        txt24.Text = " "
        txt25.Text = " "
        txt26.Text = " "
        txt27.Text = " "
        txt28.Text = " "
        txt29.Text = " "

        TabPage1.Text = "Mata Uang 1"
        txt11.Text = " "
        txt12.Text = " "
        txt13.Text = " "
        txt14.Text = " "
        txt15.Text = " "
        txt16.Text = " "
        txt17.Text = " "
        txt18.Text = " "
        txt19.Text = " "

        CheckBox1.Checked = False
        CheckBox2.Checked = False
        TextBoxPindahKurs.Enabled = False
        TextBoxKV1.Enabled = False
        TextBoxsimpan1.Enabled = False

    
        Button1.Text = "&Simpan"

        ListViewMT11.Items.Clear()
        ListViewMT1.Items.Clear()

        ListViewMT22.Items.Clear()
        ListViewMT2.Items.Clear()

        ComboBoxBank1.Items.Clear()
        ComboBoxBank1.Enabled = False

        ComboBoxRek1.Items.Clear()
        ComboBoxRek1.Enabled = False


        cmbPenerima.Items.Clear()
        cmbPenerima.Items.Add("SUPPLIER")
        cmbPenerima.Items.Add("AGENT")
        cmbPenerima.Items.Add("BANK")


        ComboBoxBank2.Items.Clear()
        ComboBoxBank2.Enabled = False

        ComboBoxRek2.Items.Clear()
        ComboBoxRek2.Enabled = False


        cmbPenerima2.Items.Clear()
        cmbPenerima2.Items.Add("SUPPLIER")
        cmbPenerima2.Items.Add("AGENT")
        cmbPenerima2.Items.Add("BANK")


        Kosong_Bawah()

        Try

            OpenConn()

            Get_No_Faktur()

            'ComboBoxCb1.Items.Clear() : ArrCB1.Clear()
            'ComboBoxCb1.Items.Add("-- Cara Bayar --") : ArrCB1.Add("")

            'ComboBoxCb1.SelectedIndex = 0
            'SQL = "select kode_cb, keterangan from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBoxCb1.Items.Add(Dr("kode_cb")) : ArrCB1.Add(Dr("kode_cb"))
            '    Loop
            'End Using

            'ComboBoxCb1.Items.Clear() : arrCrByr1.Clear() : ArrAkunCB1.Clear()
            'ComboBoxCb1.Items.Add("-- Cara Bayar --") : arrCrByr1.Add("") : ArrAkunCB1.Add("")
            'ComboBoxCb1.SelectedIndex = 0
            'SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBoxCb1.Items.Add(Dr("keterangan")) : arrCrByr1.Add(Dr("kode_cb")) : ArrAkunCB1.Add(Dr("kode_account_cb"))
            '    Loop
            'End Using

            'ComboBoxCb2.Items.Clear() : arrCrByr2.Clear() : ArrAkunCB2.Clear()
            'ComboBoxCb2.Items.Add("-- Cara Bayar --") : arrCrByr2.Add("") : ArrAkunCB2.Add("")
            'ComboBoxCb2.SelectedIndex = 0
            'SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBoxCb2.Items.Add(Dr("keterangan")) : arrCrByr2.Add(Dr("kode_cb")) : ArrAkunCB2.Add(Dr("kode_account_cb"))
            '    Loop
            'End Using

            'SQL = "SELECT kode_bank from bank where "
            'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' ORDER BY kode_bank"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        ComboBoxBank1.Items.Add(dr("kode_bank"))
            '    Loop
            'End Using

            'SQL = "SELECT kode_bank from bank where "
            'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' ORDER BY kode_bank"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        ComboBoxBank2.Items.Add(dr("kode_bank"))
            '    Loop
            'End Using

            ComboBoxNP1.Items.Clear()
            ComboBoxNP1.Items.Add("-- Seluruh --")
            ComboBoxNP1.SelectedIndex = 0
            SQL = "select Nama, Kode_Perusahaan_Biaya_Import from Perusahaan_Biaya_Import where kode_perusahaan = '" & KodePerusahaan & "' and Jenis='LUAR'  order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxNP1.Items.Add(Dr("Nama")) : ArrNP1.Add(Dr("Kode_Perusahaan_Biaya_Import"))
                Loop
            End Using

            ComboBoxMT1.Items.Clear()
            SQL = "select Kode_Mata_uang from Mata_Uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Mata_Uang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxMT1.Items.Add(Dr("Kode_Mata_uang"))
                Loop
            End Using

            ComboBoxNP2.Items.Clear()
            ComboBoxNP2.Items.Add("-- Seluruh --")
            ComboBoxNP2.SelectedIndex = 0
            SQL = "select Nama, Kode_Perusahaan_Biaya_Import from Perusahaan_Biaya_Import where kode_perusahaan = '" & KodePerusahaan & "' and Jenis='LUAR' order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxNP2.Items.Add(Dr("Nama")) : ArrNP2.Add(Dr("Kode_Perusahaan_Biaya_Import"))
                Loop
            End Using

            ComboBoxMT2.Items.Clear()
            SQL = "select Kode_Mata_uang from Mata_Uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Mata_Uang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxMT2.Items.Add(Dr("Kode_Mata_uang"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'pilihMataUang()
        Hitung()


    End Sub
    'Private Sub pilihMataUang()
    '    Try
    '        OpenConn()

    '        cbxMataUang.Items.Clear()
    '        cbxMataUang.Items.Add("--Pilih Mata Uang--")
    '        cbxMataUang.SelectedIndex = 0

    '        SQL = "select distinct(mata_uang) from pembelian_import where kode_perusahaan = '" & KodePerusahaan & "' order by mata_uang   "
    '        Using Dr = OpenTrans(SQL)
    '            Do While Dr.Read
    '                cbxMataUang.Items.Add(Dr("mata_uang"))
    '            Loop
    '        End Using
    '        CloseConn()

    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    '    'cbxMataUang.Items.Add("USD")
    '    'cbxMataUang.Items.Add("CNY")
    '    cbxMataUang.Enabled = True
    '    TextBox8.Text = ""
    '    TextBox8.Enabled = True
    '    TextBox1.Enabled = False
    '    Button3.Enabled = False
    '    Button5.Enabled = False

    'End Sub


    Private Sub Validasi_Pemb_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

    End Sub

    Private Sub Validasi_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListViewMT11.Columns.Add("No Faktur", 0, HorizontalAlignment.Left)
        ListViewMT11.Columns.Add("Tgl Transaksi", 0, HorizontalAlignment.Center)
        ListViewMT11.Columns.Add("Kode Perusahaan", 150, HorizontalAlignment.Left)
        ListViewMT11.Columns.Add("Nama Perusahaan", 0, HorizontalAlignment.Left)
        ListViewMT11.Columns.Add("Mata Uang", 80, HorizontalAlignment.Left)
        ListViewMT11.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        ListViewMT11.Columns.Add("Kurs_hpp", 140, HorizontalAlignment.Right)
        ListViewMT11.Columns.Add("No PO", 80, HorizontalAlignment.Center).DisplayIndex = 0
        ListViewMT11.Columns.Add("Lokasi", 80, HorizontalAlignment.Center).DisplayIndex = 1
        ListViewMT11.Columns.Add("flag declare", 0, HorizontalAlignment.Center)
        ListViewMT11.Columns.Add("no declare", 0, HorizontalAlignment.Center)
        ListViewMT11.Columns.Add("Selisih PO Sebelum", 100, HorizontalAlignment.Right)
        ListViewMT11.Columns.Add("Id Rencana", 0, HorizontalAlignment.Center)
        ListViewMT11.View = View.Details

        ListViewMT1.Columns.Add("No Faktur", 0, HorizontalAlignment.Left)
        ListViewMT1.Columns.Add("ID Rencana", 80, HorizontalAlignment.Center)
        ListViewMT1.Columns.Add("Keterangan", 0, HorizontalAlignment.Left)
        ListViewMT1.Columns.Add("Tgl Transaksi", 0, HorizontalAlignment.Center)
        ListViewMT1.Columns.Add("Kode Perusahaan", 150, HorizontalAlignment.Left)
        ListViewMT1.Columns.Add("Nama Perusahaan", 0, HorizontalAlignment.Left)
        ListViewMT1.Columns.Add("Mata Uang", 80, HorizontalAlignment.Left)
        ListViewMT1.Columns.Add("Total", 140, HorizontalAlignment.Right)
        ListViewMT1.Columns.Add("Dibayar", 140, HorizontalAlignment.Right)
        ListViewMT1.Columns.Add("Kurs_hpp", 140, HorizontalAlignment.Right)
        ListViewMT1.Columns.Add("No PO", 80, HorizontalAlignment.Center).DisplayIndex = 0
        ListViewMT1.Columns.Add("Lokasi", 80, HorizontalAlignment.Center).DisplayIndex = 1
        ListViewMT1.Columns.Add("flag declare", 0, HorizontalAlignment.Center)
        ListViewMT1.Columns.Add("no declare", 0, HorizontalAlignment.Center)
        ListViewMT1.Columns.Add("Selisih PO Sebelum", 100, HorizontalAlignment.Right)
        ListViewMT1.View = View.Details


        ListViewMT22.Columns.Add("No Faktur", 0, HorizontalAlignment.Left)
        ListViewMT22.Columns.Add("Tgl Transaksi", 0, HorizontalAlignment.Center)
        ListViewMT22.Columns.Add("Kode Perusahaan", 150, HorizontalAlignment.Left)
        ListViewMT22.Columns.Add("Nama Perusahaan", 0, HorizontalAlignment.Left)
        ListViewMT22.Columns.Add("Mata Uang", 80, HorizontalAlignment.Left)
        ListViewMT22.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        ListViewMT22.Columns.Add("Kurs_hpp", 140, HorizontalAlignment.Right)
        ListViewMT22.Columns.Add("No PO", 80, HorizontalAlignment.Center).DisplayIndex = 0
        ListViewMT22.Columns.Add("Lokasi", 80, HorizontalAlignment.Center).DisplayIndex = 1
        ListViewMT22.Columns.Add("flag declare", 0, HorizontalAlignment.Center)
        ListViewMT22.Columns.Add("no declare", 0, HorizontalAlignment.Center)
        ListViewMT22.Columns.Add("Selisih PO Sebelum", 100, HorizontalAlignment.Right)
        ListViewMT22.Columns.Add("Id Rencana", 0, HorizontalAlignment.Center)
        ListViewMT22.View = View.Details


        ListViewMT2.Columns.Add("No Faktur", 0, HorizontalAlignment.Left)
        ListViewMT2.Columns.Add("ID Rencana", 80, HorizontalAlignment.Center)
        ListViewMT2.Columns.Add("Keterangan", 0, HorizontalAlignment.Left)
        ListViewMT2.Columns.Add("Tgl Transaksi", 0, HorizontalAlignment.Center)
        ListViewMT2.Columns.Add("Kode Perusahaan", 150, HorizontalAlignment.Left)
        ListViewMT2.Columns.Add("Nama Perusahaan", 0, HorizontalAlignment.Left)
        ListViewMT2.Columns.Add("Mata Uang", 80, HorizontalAlignment.Left)
        ListViewMT2.Columns.Add("Total", 140, HorizontalAlignment.Right)
        ListViewMT2.Columns.Add("Dibayar", 140, HorizontalAlignment.Right)
        ListViewMT2.Columns.Add("Kurs_hpp", 140, HorizontalAlignment.Right)
        ListViewMT2.Columns.Add("No PO", 80, HorizontalAlignment.Center).DisplayIndex = 0
        ListViewMT2.Columns.Add("Lokasi", 80, HorizontalAlignment.Center).DisplayIndex = 1
        ListViewMT2.Columns.Add("flag declare", 0, HorizontalAlignment.Center)
        ListViewMT2.Columns.Add("no declare", 0, HorizontalAlignment.Center)
        ListViewMT2.Columns.Add("Selisih PO Sebelum", 100, HorizontalAlignment.Right)
        ListViewMT2.View = View.Details

        'TextBox6.Enabled = False
        Kosong()
        TxtFaktur.Focus()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Kosong()
        DateTimePicker1.Focus()
    End Sub

    Private Sub Validasi_Pembelian_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub



    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMT1.Click

        Cari("Tidak1")
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TxtFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No pelunasan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFaktur.Focus()
            Exit Sub
        ElseIf ListViewMT11.Items.Count = 0 And ListViewMT22.Items.Count = 0 Then
            MessageBox.Show("Yang akan dilunasi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxbyr.Focus()
            Exit Sub
        ElseIf TextBoxket.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxket.Focus()
            Exit Sub
        ElseIf Val(HilangkanTanda(TextBoxSLS1.Text)) <> 0 Or Val(HilangkanTanda(TextBoxSLS2.Text)) < 0 Then
            MessageBox.Show("Nilai Bayar salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If ListViewMT11.Items.Count <> 0 Then
            If cmbPenerima.SelectedIndex = -1 Or ComboBoxRek1.SelectedIndex = -1 Then
                MessageBox.Show("Rekening harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBoxBank1.Focus()
                Exit Sub
            ElseIf TextBoxKurs1.Text = 0 Then
                MessageBox.Show("Mata Uang harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBoxKurs1.Focus()
                Exit Sub
            End If
        End If

        If ListViewMT22.Items.Count <> 0 Then
            If cmbPenerima2.SelectedIndex = -1 Or ComboBoxRek2.SelectedIndex = -1 Then
                MessageBox.Show("Rekening harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBoxBank2.Focus()
                Exit Sub
            ElseIf TextBoxKurs2.Text = 0 Then
                MessageBox.Show("Mata Uang harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBoxKurs2.Focus()
                Exit Sub
            End If
        End If

        If Button1.Text = "&Simpan" Then
            Dim tny As String = MessageBox.Show("Yakin akan disimpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If tny = vbNo Then Exit Sub


            GetTime()
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                Get_No_Faktur()

                Dim flag_gabungan As String = ""
                Dim SisaHutang As Double = 0
                Dim JT As String = ""
                Dim Dari As String = ""
                Dim KodeCust As String = ""
                Dim id_rencana As String = ""
                Dim lks As String = ""


                Dim coa_hutang_declare As String = ""
                Dim coa_adm As String = ""



                Dim kode_voucher2_ As String = "NULL"
                Dim Kode_Voucher2 As String = ""
                Dim Kode_Voucher As String = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPemb, KodePerusahaan)

                Dim pagenumber As Integer = 0
                Dim pagenumber2 As Integer = 0

                Dim sudah_jurnal As Boolean = False

                Dim total_selisih As Double = 0
                Dim total_selisih2 As Double = 0
                Dim sum_biaya As Double = 0
                Dim sum_biaya2 As Double = 0

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_Voucher & "', "
                SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Pelunasan hutang " & TxtFaktur.Text.Trim & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "insert into val_pel_biaya_import_By_Perusahaan(kode_perusahaan, no_val, tanggal, jam, "
                SQL = SQL & "keterangan, uservalidasi, kode_voucher, Mata_Uang1, Total_Mata_Uang1, cara_bayar1, Kode_Bank1, No_rek1, Mata_Uang2, Total_Mata_Uang2, cara_bayar2, Kode_Bank2, No_rek2, "
                SQL = SQL & "Deposit_Keluar1, Deposit_Keluar2, Bayar1, Bayar2, Konversi_Dari_MUA1, Kurs_Konversi, Konversi_Ke_MUA2, "
                SQL = SQL & "Deposit_Masuk1, Deposit_Masuk2, Biaya_ADM1, Biaya_ADM2, Kurs_IDR1, Kurs_IDR2, Total_IDR1, Total_IDR2, jenis_rek) "
                SQL = SQL & "values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                SQL = SQL & "'" & TextBoxket.Text.Trim & "', '" & UserID & "', "
                SQL = SQL & "'" & Kode_Voucher & "', "

                If ListViewMT11.Items.Count <> 0 Then
                    If cmbPenerima.SelectedIndex = 2 Then
                        SQL = SQL & "'" & ListViewMT11.Items(0).SubItems(4).Text & "', '" & HilangkanTanda(TextBoxtot1.Text) & "', null,'" & ComboBoxBank1.Text & "','" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "', "
                    Else
                        SQL = SQL & "'" & ListViewMT11.Items(0).SubItems(4).Text & "', '" & HilangkanTanda(TextBoxtot1.Text) & "', null,'-','" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "', "
                    End If
                Else
                    SQL = SQL & "NULL, NULL, NULL, NULL, NULL, "
                End If


                If ListViewMT22.Items.Count <> 0 Then


                    If cmbPenerima.SelectedIndex = 2 Then
                        SQL = SQL & "'" & ListViewMT22.Items(0).SubItems(4).Text & "', '" & HilangkanTanda(TextBoxtot2.Text) & "', null,'" & ComboBoxBank1.Text & "','" & arrRek2.Item(ComboBoxRek2.SelectedIndex) & "', "
                    Else
                        SQL = SQL & "'" & ListViewMT22.Items(0).SubItems(4).Text & "', '" & HilangkanTanda(TextBoxtot2.Text) & "', null,'-','" & arrRek2.Item(ComboBoxRek2.SelectedIndex) & "', "
                    End If
                Else

                    SQL = SQL & "NULL, NULL, NULL, NULL, NULL, "

                End If
                SQL = SQL & "'" & HilangkanTanda(TextBoxDPT1.Text) & "', '" & HilangkanTanda(TextBoxDPT2.Text) & "', '" & HilangkanTanda(TextBoxDBY1.Text) & "', '" & HilangkanTanda(TextBoxDBY2.Text) & "', "
                SQL = SQL & "'" & HilangkanTanda(TextBoxPindahKurs.Text) & "', '" & HilangkanTanda(TextBoxKV1.Text) & "', '" & HilangkanTanda(TextBoxKV2.Text) & "', '" & HilangkanTanda(TextBoxsimpan1.Text) & "', "
                SQL = SQL & "'" & HilangkanTanda(TextBoxSLS2.Text) & "', '" & HilangkanTanda(TextBoxADM1.Text) & "', '" & HilangkanTanda(TextBoxADM2.Text) & "', '" & HilangkanTanda(TextBoxKurs1.Text) & "', "
                SQL = SQL & "'" & HilangkanTanda(TextBoxKurs2.Text) & "', '" & HilangkanTanda(TextBoxtotIDR1.Text) & "', '" & HilangkanTanda(TextBoxtotIDR2.Text) & "', '" & cmbPenerima.Text & "') "

                ExecuteTrans(SQL)


                Dim Lokasi_Group As String = ""
                Dim Konte_group As String = ""
                Dim PO_Induk As String = ""
                Dim sup As String = ""

                For index As Integer = 0 To ListViewMT11.Items.Count - 1
                    Get_Isi_Listview(index)

                    Dim idrcn As String = ""
                    Dim abc As Integer = 0

                    Dim gabungan As String = ""
                    SQL = "select b.nama, a.Flag_Gabungan from Rencana_Order a, Suppliers b where a.Kode_Perusahaan=b.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Supplier=b.Kode_Supplier and a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
                    SQL = SQL & "a.id_rencana ='" & LvRencana & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            gabungan = General_Class.CekNULL(Dr("Flag_Gabungan"))
                            sup = Dr("nama")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Id Rencana tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    If gabungan = "Y" Then
                        SQL = "select a.Id_rencana from Rencana_Order_gabungan a "
                        SQL = SQL & "where a.Kode_Perusahaan ='" & KodePerusahaan & "' and a.id_rencana_induk ='" & LvRencana & "'"
                        Using Dr = OpenTrans(SQL)
                            Do While Dr.Read
                                If abc <> 0 Then
                                    idrcn = idrcn & ", "
                                End If
                                idrcn = idrcn & Dr("Id_rencana")
                                abc += 1
                            Loop
                        End Using
                    Else
                        idrcn = LvRencana
                    End If

                

                    SQL = "select Tanggal_PO from Rencana_Order a where "
                    SQL = SQL & "a.id_rencana ='" & LvRencana & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            If index <> 0 Then
                                PO_Induk = PO_Induk & ", "
                            End If

                            PO_Induk = PO_Induk & "PO " & Format(Dr("Tanggal_PO"), "MM.dd")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Id Rencana tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    Dim Konte As Double = 0
                    Dim xxz As Integer = 0
                    SQL = "select a.id_rencana, a.Total_Persen, a.Lokasi, B.Inisial_Faktur "
                    SQL = SQL & "from rencana_order a, Stock_Owner b where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and A.Lokasi = B.Kode_Stock_Owner and "
                    SQL = SQL & "a.id_rencana in(" & idrcn & ")"
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read
                            If index <> 0 Or xxz <> 0 Then
                                Lokasi_Group = Lokasi_Group & ", "
                            End If
                            Lokasi_Group = Lokasi_Group & Dr("Inisial_Faktur")
                            Konte = Konte + Dr("Total_Persen")

                            xxz += 1
                        Loop
                    End Using


                    Dim kontainer As Integer = Konte / 100
                    Dim jumlah As Integer = kontainer * 100
                    Dim selisih As Integer = Konte - jumlah

                    If selisih = 0 Or selisih <= 99 Then
                        kontainer = kontainer
                    Else
                        kontainer = kontainer + 1
                    End If


                    If index <> 0 Then
                        Konte_group = Konte_group & ", "
                    End If

                    Konte_group = Konte_group & kontainer & " C"





                Next
                Dim ket As String = Strings.Left(TxtFaktur.Text & "; " & PO_Induk & "; " & Lokasi_Group & "; " & sup & "; " & Konte_group, 180)

                For i As Integer = 0 To ListViewMT11.Items.Count - 1
                    Get_Isi_Listview(i)

                    Dim kodeMasterKategoriBI As String = ""

                    SQL = ";with cte_a as( "
                    SQL = SQL & "select d.Kode_Supplier,d.flag_gabungan,Format(d.Tanggal_PO,'MM.dd') as no_po,d.Lokasi, a.No_Faktur, a.Id_rencana, a.Tanggal+a.Jam as Tgl, a.Keterangan, "
                    SQL = SQL & "c.Kode_Perusahaan_Biaya_import, b.mata_Uang, c.Nama, b.nilai, "
                    SQL = SQL & "isnull((select sum(Y.byr) from Val_Pel_Biaya_import_by_Perusahaan X, "
                    SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
                    SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = b.NO_Faktur and Y.Kode_Perusahaan_Biaya_Import = "
                    SQL = SQL & "b.Kode_Perusahaan_Biaya_import and Y.Mata_Uang = b.Mata_Uang  AND X.STATUS IS NULL),0) as sudah_bayar, c.jenis, "
                    SQL = SQL & "isnull((select top(1)kurs from detail_transaksi_biaya_import x where x.Kode_Perusahaan=b.Kode_Perusahaan and "
                    SQL = SQL & "x.no_faktur=b.no_faktur and x.Kode_Perusahaan_biaya_Import = b.Kode_Perusahaan_biaya_import and x.Mata_Uang=b.Mata_Uang),0) as kurs_hpp, '1' as dari, d.Flag_Barang_Masuk "
                    SQL = SQL & "from transaksi_Biaya_Import a, Detail_transaksi_Biaya_Import_by_Perusahaan b, Perusahaan_Biaya_Import c, Rencana_Order d "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_faktur = b.No_faktur And a.Status Is null "
                    SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Perusahaan_Biaya_Import = "
                    SQL = SQL & "c.Kode_Perusahaan_biaya_import and b.Flag_Lunas is null and a.Kode_Perusahaan=d.Kode_Perusahaan and a.Id_rencana=d.Id_Rencana "

                    SQL = SQL & "union all "

                    SQL = SQL & "select d.Kode_Supplier,d.flag_gabungan, Format(d.Tanggal_PO,'MM.dd') as no_po ,d.Lokasi, a.No_Faktur, a.Id_rencana, a.Tanggal+a.Jam as Tgl, a.Keterangan, "
                    SQL = SQL & "c.Kode_Perusahaan_Biaya_import, b.mata_Uang, c.Nama, b.nilai, "
                    SQL = SQL & "isnull((select sum(Y.byr) from Val_Pel_Biaya_import_by_Perusahaan X, "
                    SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
                    SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = b.NO_Faktur and Y.Kode_Perusahaan_Biaya_Import = "
                    SQL = SQL & "b.Kode_Perusahaan_Biaya_import and Y.Mata_Uang = b.Mata_Uang AND X.STATUS IS NULL),0 ) as sudah_bayar, c.jenis, "
                    SQL = SQL & "isnull((select nilai from hpp_import x, Kurs_HPP_import y where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_Faktur=y.no_faktur "
                    SQL = SQL & "and x.status is null and y.jenis='FREIGHT' and x.Kode_Perusahaan=a.Kode_Perusahaan and x.Id_rencana=a.Id_rencana and y.mata_uang=b.mata_uang),0) as kurs_hpp, '3' as dari, d.Flag_Barang_Masuk "
                    SQL = SQL & "from transaksi_Biaya_Import3 a, Detail_transaksi_Biaya_Import3_by_Perusahaan b, Perusahaan_Biaya_Import c, Rencana_Order d "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_faktur = b.No_faktur And a.Status Is null "
                    SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Perusahaan_Biaya_Import = "
                    SQL = SQL & "c.Kode_Perusahaan_biaya_import and b.Flag_Lunas is null and a.Kode_Perusahaan=d.Kode_Perusahaan and a.Id_rencana=d.Id_Rencana "
                    SQL = SQL & ")"
                    SQL = SQL & "select* from cte_a where Jenis='LUAR' and No_faktur ='" & LvFak & "' and Kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang = '" & LvMT & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            flag_gabungan = General_Class.CekNULL(Dr("Flag_Gabungan"))
                            lks = Dr("lokasi")
                            id_rencana = Dr("Id_rencana")
                            SisaHutang = Dr("nilai") - Dr("sudah_bayar")
                            Dari = Dr("dari")
                            JT = ""
                            'JT = Dr("jenis_transaksi")
                            KodeCust = Dr("kode_supplier")
                            'kodeMasterKategoriBI = Dr("kode_master_kategori_biaya_import")


                            If SisaHutang < Val(HilangkanTanda(LvJml)) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf General_Class.CekNULL(General_Class.CekNULL(Dr("Flag_Barang_Masuk"))) <> "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Barang Masuk Belum Selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            Dr.Close()
                            Dim tanggal_barang_Masuk As DateTime
                            SQL = "select top(1) Tanggal from barang_masuk where id_rencana in("
                            If flag_gabungan = "Y" Then
                                SQL = SQL & "select id_rencana from rencana_order_gabungan where id_rencana_induk=" & id_rencana & " "
                            Else
                                SQL = SQL & " " & id_rencana & " "
                            End If
                            SQL = SQL & ")"
                            SQL = SQL & "order by tanggal desc"
                            Using dr2 = OpenTrans(SQL)
                                If dr2.Read Then
                                    tanggal_barang_Masuk = dr2("Tanggal")
                                Else
                                    dr2.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Barang Masuk Tidak Di Temukan . . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            If tanggal_barang_Masuk > DateTimePicker1.Value Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Tanggal Pelunasan Tidak Boleh sebelum tanggal barang masuk . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Nomor faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

       
                    If SisaHutang = Val(HilangkanTanda(LvJml)) Then

                        If Dari = "1" Then
                            SQL = "Update detail_transaksi_Biaya_Import_by_Perusahaan set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            ExecuteTrans(SQL)
                        ElseIf Dari = "3" Then
                            SQL = "Update detail_transaksi_Biaya_Import3_by_Perusahaan set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP & "' and Mata_Uang ='" & LvMT & "' "
                            ExecuteTrans(SQL)
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tabel Asal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                    End If

                    Dim nilai_hpp As Double = Val(HilangkanTanda(Format(Val(HilangkanTanda(LvJml)) * Val(HilangkanTanda(Lvkurshpp)), "N0")))
                    Dim nilai_akhir As Double = Val(HilangkanTanda(Format(Val(HilangkanTanda(LvJml)) * Val(HilangkanTanda(TextBoxKurs1.Text)), "N0")))
                    Dim selisih As Double = nilai_akhir - nilai_hpp

                    If Val(HilangkanTanda(Lvselisih)) <> 0 Then

                        SQL = "select isnull(sum(Nilai_Selisih),0) as sisa_selisih, "

                        If Dari = "1" Then
                            SQL = SQL & "isnull((select a.flag_lunas from "
                            SQL = SQL & "detail_transaksi_Biaya_Import_by_Perusahaan a where a.no_faktur='" & LvFak & "' and"
                            SQL = SQL & " a.Kode_Perusahaan_biaya_import ='" & LvKP & "' and a.mata_uang='" & LvMT & "'),NULL) as flag_lunas "
                        Else
                            SQL = SQL & "isnull((select a.flag_lunas from "
                            SQL = SQL & "detail_transaksi_Biaya_Import3_by_Perusahaan a where a.no_faktur='" & LvFak & "' and"
                            SQL = SQL & " a.Kode_Perusahaan_biaya_import ='" & LvKP & "' and a.mata_uang='" & LvMT & "'),NULL) as flag_lunas "

                        End If


                        SQL = SQL & ",isnull((select sum(Y.selisih_po_Sebelum) from Val_Pel_Biaya_import_by_Perusahaan X, "
                        SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
                        SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = '" & LvFak & "' and Y.Kode_Perusahaan_Biaya_Import = "
                        SQL = SQL & "'" & LvKP & "' and Y.Mata_Uang = '" & LvMT & "'and x.status is null),0) as selisih_PO "
                        SQL = SQL & "from hpp_import j, HPP_Import_Log_Selisih k where j.Kode_Perusahaan=k.Kode_Perusahaan and j.No_Faktur=k.NO_Faktur and j.Status is null "
                        SQL = SQL & " and j.Kode_Perusahaan='" & KodePerusahaan & "' and j.Id_Rencana='" & id_rencana & "' and k.jenis='" & LvKP & "'"

                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then

                                If Math.Abs(Dr("Sisa_Selisih")) - Math.Abs(Dr("selisih_PO")) < Math.Abs(Val(HilangkanTanda(Lvselisih))) Then
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Selisih yang di input melebihi Jumlah selisih . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Exit Sub
                                End If

                                If General_Class.CekNULL(Dr("Flag_Lunas")) = "Y" Then

                                    If Dr("Sisa_Selisih") - (Dr("selisih_PO")) - (Val(HilangkanTanda(Lvselisih))) <> 0 Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Semua sisa selisih harus di input . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Exit Sub
                                    End If

                                End If


                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("No Faktur Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Exit Sub
                            End If
                        End Using
                    End If


                    SQL = "insert into detail_val_pel_biaya_import_By_Perusahaan(kode_perusahaan, no_val, no_faktur,kode_Perusahaan_biaya_import, Mata_Uang, byr, Kurs_HPP, Kurs_Akhir, Selisih_Kurs,  selisih_po_sebelum) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & LvFak.Trim & "', '" & LvKP & "', '" & LvMT & "'," & HilangkanTanda(LvJml) & "," & HilangkanTanda(Lvkurshpp) & ", '" & HilangkanTanda(TextBoxKurs1.Text) & "', '" & selisih & "', '" & HilangkanTanda(Lvselisih) & "') "
                    ExecuteTrans(SQL)

                    
                    Dim gabung As String = "NULL"
                    If flag_gabungan = "Y" Then
                        gabung = "'Y'"
                    End If


                    If selisih <> 0 Then
                        SQL = "insert into Selisih_kurs_biaya_import_By_Perusahaan( kode_perusahaan, No_Val, Id_rencana, Kode_Perusahaan_Biaya_Import, Flag_Gabungan, Nilai, Kode_supplier, sisa) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                        SQL = SQL & "'" & id_rencana & "', '" & LvKP & "', " & gabung & "  ," & selisih & ",'" & KodeCust & "', '" & selisih & "')"
                        ExecuteTrans(SQL)
                    End If


                    Dim coa_Selisih As String = ""
                    Dim coa_hutang As String = ""
                    SQL = "select hutang_Import, Akun_Selisih_PO_biaya, akun_hutang_declare, Biaya_admin from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "Kode_STock_Owner = '" & lks & "' "

                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            coa_hutang = dr("hutang_Import")
                            coa_Selisih = dr("Akun_Selisih_PO_biaya")
                            coa_hutang_declare = dr("akun_hutang_declare")
                            coa_adm = dr("Biaya_Admin")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data kode master tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    sum_biaya = sum_biaya + nilai_akhir

                    ' ''SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ' ''SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "'"
                    ' ''Using Dr = OpenTrans(SQL)
                    ' ''    If Dr.Read Then
                    ' ''        Dr.Close()
                    ' ''        'update 

                    ' ''        SQL = "update detail_jurnal set debit = debit+ " & nilai_hpp & " where "
                    ' ''        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ' ''        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "'"
                    ' ''        ExecuteTrans(SQL)
                    ' ''    Else
                    ' ''        Dr.Close()
                    ' ''        'insert
                    ' ''        pagenumber += 1
                    ' ''        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang, 1), _
                    ' ''              Strings.Mid(coa_hutang, 2, 1), _
                    ' ''              Strings.Mid(Ganti(coa_hutang), 3), _
                    ' ''              KodePerusahaan, KodeProyek, ket, nilai_hpp, "0", pagenumber, Ket_Lokasi_HO)
                    ' ''        ExecuteTrans(SQL)

                    ' ''    End If
                    ' ''End Using

                    ' ''If selisih <> 0 Then

                    ' ''    If selisih > 0 Then
                    ' ''        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ' ''        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and debit <> 0"
                    ' ''        Using Dr = OpenTrans(SQL)
                    ' ''            If Dr.Read Then
                    ' ''                Dr.Close()
                    ' ''                'update 

                    ' ''                SQL = "update detail_jurnal set debit = debit+ " & selisih & " where "
                    ' ''                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ' ''                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and debit <> 0"
                    ' ''                ExecuteTrans(SQL)
                    ' ''            Else
                    ' ''                Dr.Close()
                    ' ''                'insert
                    ' ''                pagenumber += 1
                    ' ''                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Selisih, 1), _
                    ' ''                      Strings.Mid(coa_Selisih, 2, 1), _
                    ' ''                      Strings.Mid(Ganti(coa_Selisih), 3), _
                    ' ''                      KodePerusahaan, KodeProyek, ket, selisih, "0", pagenumber, Ket_Lokasi_HO)
                    ' ''                ExecuteTrans(SQL)
                    ' ''            End If
                    ' ''        End Using
                    ' ''    Else
                    ' ''        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ' ''        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and kredit <> 0"
                    ' ''        Using Dr = OpenTrans(SQL)
                    ' ''            If Dr.Read Then
                    ' ''                Dr.Close()
                    ' ''                'update 

                    ' ''                SQL = "update detail_jurnal set kredit = kredit+ " & Math.Abs(selisih) & " where "
                    ' ''                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ' ''                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and kredit <> 0"
                    ' ''                ExecuteTrans(SQL)
                    ' ''            Else
                    ' ''                Dr.Close()
                    ' ''                'insert
                    ' ''                pagenumber += 1
                    ' ''                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Selisih, 1), _
                    ' ''                      Strings.Mid(coa_Selisih, 2, 1), _
                    ' ''                      Strings.Mid(Ganti(coa_Selisih), 3), _
                    ' ''                      KodePerusahaan, KodeProyek, ket, "0", Math.Abs(selisih), pagenumber, Ket_Lokasi_HO)
                    ' ''                ExecuteTrans(SQL)
                    ' ''            End If
                    ' ''        End Using
                    ' ''    End If
                    ' ''End If
                    '---------------------------------------------------------------

                    ' ''If Val(HilangkanTanda(Lvselisih)) <> 0 Then
                    ' ''    total_selisih = total_selisih + Val(HilangkanTanda(Lvselisih))
                    ' ''    If sudah_jurnal = False Then
                    ' ''        Kode_Voucher2 = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPemb, KodePerusahaan)
                    ' ''        kode_voucher2_ = "'" & Kode_Voucher2 & "'"

                    ' ''        SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    ' ''        SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                    ' ''        SQL = SQL & "'" & Kode_Voucher2 & "', "
                    ' ''        SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                    ' ''        SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    ' ''        SQL = SQL & "'" & KodeProyek & "', 'Pelunasan hutang " & TxtFaktur.Text.Trim & "', '', "
                    ' ''        SQL = SQL & "'-', '" & UserID & "')"
                    ' ''        ExecuteTrans(SQL)

                    ' ''        sudah_jurnal = True
                    ' ''    End If


                    ' ''    'voucher selisih sebelum
                    ' ''    If Val(HilangkanTanda(Lvselisih)) > 0 Then
                    ' ''        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                    ' ''        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and kredit <> 0"
                    ' ''        Using Dr = OpenTrans(SQL)
                    ' ''            If Dr.Read Then
                    ' ''                Dr.Close()
                    ' ''                'update 

                    ' ''                SQL = "update detail_jurnal set kredit = kredit+ " & Val(HilangkanTanda(Lvselisih)) & " where "
                    ' ''                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                    ' ''                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and kredit <> 0"
                    ' ''                ExecuteTrans(SQL)
                    ' ''            Else
                    ' ''                Dr.Close()
                    ' ''                'insert
                    ' ''                pagenumber2 += 1
                    ' ''                SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_Selisih, 1), _
                    ' ''                      Strings.Mid(coa_Selisih, 2, 1), _
                    ' ''                      Strings.Mid(Ganti(coa_Selisih), 3), _
                    ' ''                      KodePerusahaan, KodeProyek, ket, "0", Val(HilangkanTanda(Lvselisih)), pagenumber2)
                    ' ''                ExecuteTrans(SQL)
                    ' ''            End If
                    ' ''        End Using

                    ' ''        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                    ' ''        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0"
                    ' ''        Using Dr = OpenTrans(SQL)
                    ' ''            If Dr.Read Then
                    ' ''                Dr.Close()
                    ' ''                'update 

                    ' ''                SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Val(HilangkanTanda(Lvselisih))) & " where "
                    ' ''                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                    ' ''                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0"
                    ' ''                ExecuteTrans(SQL)
                    ' ''            Else
                    ' ''                Dr.Close()
                    ' ''                'insert
                    ' ''                pagenumber2 += 1
                    ' ''                SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_hutang, 1), _
                    ' ''                      Strings.Mid(coa_hutang, 2, 1), _
                    ' ''                      Strings.Mid(Ganti(coa_hutang), 3), _
                    ' ''                      KodePerusahaan, KodeProyek, ket, Math.Abs(Val(HilangkanTanda(Lvselisih))), "0", pagenumber2)
                    ' ''                ExecuteTrans(SQL)
                    ' ''            End If
                    ' ''        End Using
                    ' ''    Else
                    ' ''        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                    ' ''        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and debit <> 0"
                    ' ''        Using Dr = OpenTrans(SQL)
                    ' ''            If Dr.Read Then
                    ' ''                Dr.Close()
                    ' ''                'update 

                    ' ''                SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Val(HilangkanTanda(Lvselisih))) & " where "
                    ' ''                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                    ' ''                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and debit <> 0"
                    ' ''                ExecuteTrans(SQL)
                    ' ''            Else
                    ' ''                Dr.Close()
                    ' ''                'insert
                    ' ''                pagenumber2 += 1
                    ' ''                SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_Selisih, 1), _
                    ' ''                      Strings.Mid(coa_Selisih, 2, 1), _
                    ' ''                      Strings.Mid(Ganti(coa_Selisih), 3), _
                    ' ''                      KodePerusahaan, KodeProyek, ket, Math.Abs(Val(HilangkanTanda(Lvselisih))), "0", pagenumber2)
                    ' ''                ExecuteTrans(SQL)
                    ' ''            End If
                    ' ''        End Using

                    ' ''        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                    ' ''        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and kredit <> 0"
                    ' ''        Using Dr = OpenTrans(SQL)
                    ' ''            If Dr.Read Then
                    ' ''                Dr.Close()
                    ' ''                'update 

                    ' ''                SQL = "update detail_jurnal set kredit = kredit+ " & Math.Abs(Val(HilangkanTanda(Lvselisih))) & " where "
                    ' ''                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ' ''                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                    ' ''                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and kredit <> 0"
                    ' ''                ExecuteTrans(SQL)
                    ' ''            Else
                    ' ''                Dr.Close()
                    ' ''                'insert
                    ' ''                pagenumber2 += 1
                    ' ''                SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_hutang, 1), _
                    ' ''                      Strings.Mid(coa_hutang, 2, 1), _
                    ' ''                      Strings.Mid(Ganti(coa_hutang), 3), _
                    ' ''                      KodePerusahaan, KodeProyek, ket, "0", Math.Abs(Val(HilangkanTanda(Lvselisih))), pagenumber2)
                    ' ''                ExecuteTrans(SQL)
                    ' ''            End If
                    ' ''        End Using
                    ' ''    End If

                    ' ''End If

                    If Val(HilangkanTanda(TextBoxDPT1.Text)) > 0 Then
                        ''cmbPenerima2.Items.Add("SUPPLIER")
                        ''cmbPenerima2.Items.Add("AGENT")
                        ''cmbPenerima2.Items.Add("BANK")
                        'If cmbPenerima.Text = "SUPPLIER" Then

                        'Else

                        'End If

                        Dim flag_gabung As String = ""
                        Dim faktur_gabung As String = ""
                        SQL = "select*,"

                        SQL = SQL & "CASE when flag_gabung_declare = 'Y' then "
                        SQL = SQL & "isnull(round((select  top(1) x.kurs_akhir  "
                        SQL = SQL & "from pelunasan_declare_gabungan x, pelunasan_declare_gabungan_detail2 y "
                        SQL = SQL & "where(x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur And x.status Is null) "
                        SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.id_rencana=a.id_rencana),5),0)  "
                        SQL = SQL & "ELSE "
                        SQL = SQL & "isnull(round((select sum(x.Total_IDR)/sum(x.Nilai_Tambahan) from Val_Pel_Declare_Invoice_Import x, "
                        SQL = SQL & "detail_Val_Pel_Declare_Invoice_Import y where x.Kode_Perusahaan= y.Kode_Perusahaan and x.No_Val=y.No_Val "
                        SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.No_Faktur=a.id_rencana and x.Status is null),5),0) END as Kurs_Akhir "

                        SQL = SQL & ",isnull((select  top(1) x.no_faktur  "
                        SQL = SQL & "from pelunasan_declare_gabungan x, pelunasan_declare_gabungan_detail2 y "
                        SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.status is null "
                        SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.id_rencana=a.id_rencana),'') as Faktur_gabung_Declare  "

                        SQL = SQL & ",isnull((select STRING_AGG('''' + x.no_val +'''',',') from Val_Pel_Declare_Invoice_Import X, detail_Val_Pel_Declare_Invoice_Import Y "
                        SQL = SQL & "where X.Kode_Perusahaan=Y.Kode_Perusahaan and X.No_Val=Y.No_Val and X.status is null and "
                        SQL = SQL & "Y.Kode_Perusahaan=a.Kode_Perusahaan and Y.No_faktur=a.Id_Rencana ),'') as No_Declare "

                        SQL = SQL & ",isnull((select Flag_Lunas_Declare from submit_PO x where "
                        SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.id_rencana=a.id_rencana and x.Status is null),'')as Flag_Lunas_Declare_submit "


                        SQL = SQL & ",isnull((select Kode_Supplier from Rencana_Order x where "
                        SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.id_rencana=a.id_rencana and x.Status is null),'')as Kode_Supplier "
                        SQL = SQL & "from loading_barang a where Id_Rencana='" & LvRencana & "' and a.Status is null "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                flag_gabung = General_Class.CekNULL(dr("flag_gabung_declare"))
                                faktur_gabung = General_Class.CekNULL(dr("Faktur_gabung_Declare"))

                            End If
                        End Using


                        If flag_gabung = "Y" And cmbPenerima.Text = "SUPPLIER" Then
                            Dim nilai_sisa As Double = Val(HilangkanTanda(LvJml))

                            SQL = "select Id_Rencana from Pelunasan_Declare_Gabungan a, Pelunasan_Declare_Gabungan_detail1 b  where a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur=b.No_faktur and "
                            SQL = SQL & " a.status is null and a.no_faktur='" & faktur_gabung & "' "
                            Using Ds3 = BindingTrans(SQL)
                                If Ds3.Tables("MyTable").Rows.Count <> 0 Then

                                    For index3 As Integer = 0 To Ds3.Tables("MyTable").Rows.Count - 1

                                        'For index4 As Integer = 0 To ListView1.Items.Count - 1
                                        '    Get_Isi_Listview(index4)
                                        SQL = "select no_urut, sisa from Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, "
                                        SQL = SQL & "Deposit_Pelunasan b  where "
                                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur= B.No_Faktur "
                                        SQL = SQL & "and round(a.sisa,2) <> 0 and b.Status is null and Mata_Uang= '" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "' "
                                        SQL = SQL & " and No_Rek = '" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "' and a.Kode_Supplier = '" & arrSupp.Item(ComboBoxRek1.SelectedIndex) & "' "
                                        SQL = SQL & "and a.id_rencana='" & Ds3.Tables("MyTable").Rows(index3).Item("id_rencana") & "' "
                                        SQL = SQL & "order by No_urut"
                                        Using Ds = BindingTrans(SQL)
                                            With Ds.Tables("MyTable")
                                                If .Rows.Count <> 0 Then

                                                    For index As Integer = 0 To .Rows.Count - 1

                                                        SQL = "select top(1) no_urut, sisa, a.No_Faktur from Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, "
                                                        SQL = SQL & "Deposit_Pelunasan b  where "
                                                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur= B.No_Faktur "
                                                        SQL = SQL & "and round(a.sisa,2) <> 0 and b.Status is null and Mata_Uang='" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "' "
                                                        SQL = SQL & " and No_Rek ='" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "' and a.Kode_Supplier ='" & arrSupp.Item(ComboBoxRek1.SelectedIndex) & "' "
                                                        SQL = SQL & "and a.id_rencana='" & Ds3.Tables("MyTable").Rows(index3).Item("id_rencana") & "' "
                                                        SQL = SQL & "order by No_urut"
                                                        Using Ds2 = BindingTrans(SQL)
                                                            If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                                                For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                                                    If nilai_sisa > Ds2.Tables("MyTable").Rows(j).Item("sisa") Then

                                                                        nilai_sisa = nilai_sisa - Ds2.Tables("MyTable").Rows(j).Item("sisa")


                                                                        SQL = "update Deposit_Val_Pel_Biaya_Import_By_Perusahaan set sisa = sisa - " & Ds2.Tables("MyTable").Rows(j).Item("sisa")
                                                                        SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_urut = '" & Ds2.Tables("MyTable").Rows(j).Item("no_urut") & "' "
                                                                        ExecuteTrans(SQL)

                                                                        SQL = "insert into LOG_DEPOSIT_VAL_PEL_bIAYA_IMPORT_BY_PERUSAHAAN(kode_Perusahaan, No_Faktur_Masuk, no_faktur_Keluar, Nilai, Jenis, Tanggal, Jam, UserID, Id_Rencana_Masuk, Kurs, Nilai_IDR) Values ("
                                                                        SQL = SQL & " '" & KodePerusahaan & "', '" & TxtFaktur.Text & "','" & Ds2.Tables("MyTable").Rows(j).Item("No_Faktur") & "', "
                                                                        SQL = SQL & " '" & Ds2.Tables("MyTable").Rows(j).Item("sisa") & "', 'AGENT', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & UserID & "', "
                                                                        SQL = SQL & "'" & LvRencana & "', '" & Val(HilangkanTanda(TextBoxKurs1.Text)) & "', '" & Ds2.Tables("MyTable").Rows(j).Item("sisa") * Val(HilangkanTanda(TextBoxKurs1.Text)) & "') "
                                                                        ExecuteTrans(SQL)

                                                                        'SQL = "update selisih_pelunasan_loading_barang set sisa = sisa - " & Ds2.Tables("MyTable").Rows(j).Item("sisa")
                                                                        'SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_pelunasan='" & TxtFaktur.Text & "' and Id_rencana = '" & Ds3.Tables("MyTable").Rows(index3).Item("id_rencana") & "' "
                                                                        'ExecuteTrans(SQL)

                                                                    ElseIf nilai_sisa <= Ds2.Tables("MyTable").Rows(j).Item("sisa") Then

                                                                        SQL = "update Deposit_Val_Pel_Biaya_Import_By_Perusahaan set sisa = sisa - " & nilai_sisa
                                                                        SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_urut = '" & Ds2.Tables("MyTable").Rows(j).Item("no_urut") & "' "
                                                                        ExecuteTrans(SQL)

                                                                        SQL = "insert into LOG_DEPOSIT_VAL_PEL_bIAYA_IMPORT_BY_PERUSAHAAN(kode_Perusahaan, No_Faktur_Masuk, no_faktur_Keluar, Nilai, Jenis, Tanggal, Jam, UserID, Id_Rencana_Masuk, Kurs, Nilai_IDR) Values ("
                                                                        SQL = SQL & " '" & KodePerusahaan & "', '" & TxtFaktur.Text & "','" & Ds2.Tables("MyTable").Rows(j).Item("No_Faktur") & "', "
                                                                        SQL = SQL & " '" & nilai_sisa & "', 'AGENT', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & UserID & "', '" & LvRencana & "', "
                                                                        SQL = SQL & "'" & Val(HilangkanTanda(TextBoxKurs1.Text)) & "', '" & nilai_sisa * Val(HilangkanTanda(TextBoxKurs1.Text)) & "') "
                                                                        ExecuteTrans(SQL)

                                                                        'SQL = "update selisih_pelunasan_loading_barang set sisa = sisa - " & nilai_sisa
                                                                        'SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_pelunasan='" & TxtFaktur.Text & "' and Id_rencana = '" & Ds3.Tables("MyTable").Rows(index3).Item("id_rencana") & "' "
                                                                        'ExecuteTrans(SQL)

                                                                        nilai_sisa = nilai_sisa - nilai_sisa

                                                                    End If

                                                                Next

                                                            Else
                                                                CloseTrans()
                                                                CloseConn()
                                                                MessageBox.Show("Terjadi Kesalahan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                Exit Sub
                                                            End If
                                                        End Using

                                                        If nilai_sisa = 0 Then
                                                            Exit For
                                                        End If

                                                        If nilai_sisa < 0 Then
                                                            CloseTrans()
                                                            CloseConn()
                                                            MessageBox.Show("Nilai minus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    Next
                                                    'Else
                                                    '    CloseTrans()
                                                    '    CloseConn()
                                                    '    MessageBox.Show("Saldo Rekening Tidak Cukup!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    '    Exit Sub
                                                End If
                                            End With

                                        End Using
                                        'Next


                                    Next

                                    If nilai_sisa <> 0 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Rekening Tidak Cukup . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Exit Sub
                                    End If

                                End If

                            End Using
                        Else
                            Dim idrncn As String = ""

                            If cmbPenerima.SelectedIndex = 2 Then
                                idrncn = ArrRencana.Item(ComboBoxRek1.SelectedIndex)
                            Else
                                idrncn = id_rencana
                            End If

                            Dim total_deposit As Double = Val(HilangkanTanda(LvJml))

                            SQL = "insert into log_deposit_Pelunasan_Per_faktur(kode_Perusahaan, No_Faktur_Masuk, no_Rek_Keluar, Nilai, Jenis, Tanggal, Jam, UserID, Kurs, Nilai_IDR, Kode_Supplier, Mata_Uang) Values ("
                            SQL = SQL & " '" & KodePerusahaan & "', '" & TxtFaktur.Text & "','" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "', "
                            SQL = SQL & " '" & total_deposit & "', 'AGENT', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & UserID & "', "
                            SQL = SQL & "'" & TextBoxKurs1.Text & "', '" & total_deposit * Val(TextBoxKurs1.Text) & "', '" & arrSupp.Item(ComboBoxRek1.SelectedIndex) & "', '" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "') "
                            ExecuteTrans(SQL)

                            SQL = "select no_urut, sisa from Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, "
                            SQL = SQL & "Deposit_Pelunasan b  where "
                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur= B.No_Faktur "
                            SQL = SQL & "and round(a.sisa,2) <> 0 and b.Status is null and kode_supplier = '" & arrSupp.Item(ComboBoxRek1.SelectedIndex) & "' and Mata_Uang='" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "' "
                            SQL = SQL & " and No_Rek ='" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "' and a.id_rencana='" & idrncn & "' "
                            SQL = SQL & "order by No_urut"
                            Using Ds = BindingTrans(SQL)
                                With Ds.Tables("MyTable")
                                    If .Rows.Count <> 0 Then

                                        For index As Integer = 0 To .Rows.Count - 1

                                            SQL = "select top(1) no_urut, sisa, a.No_Faktur from Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, "
                                            SQL = SQL & "Deposit_Pelunasan b  where "
                                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur= B.No_Faktur "
                                            SQL = SQL & "and round(a.sisa,2) <> 0 and b.Status is null and kode_supplier = '" & arrSupp.Item(ComboBoxRek1.SelectedIndex) & "'  and Mata_Uang='" & arrMUA.Item(ComboBoxRek1.SelectedIndex) & "' "
                                            SQL = SQL & " and No_Rek ='" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "' and a.id_rencana='" & idrncn & "'  "
                                            SQL = SQL & "order by No_urut"
                                            Using Ds2 = BindingTrans(SQL)
                                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                                    For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                                        If total_deposit > Ds2.Tables("MyTable").Rows(j).Item("sisa") Then

                                                            total_deposit = total_deposit - Ds2.Tables("MyTable").Rows(j).Item("sisa")


                                                            SQL = "update Deposit_Val_Pel_Biaya_Import_By_Perusahaan set sisa = sisa - " & Ds2.Tables("MyTable").Rows(j).Item("sisa")
                                                            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_urut = '" & Ds2.Tables("MyTable").Rows(j).Item("no_urut") & "' "
                                                            ExecuteTrans(SQL)

                                                            SQL = "insert into LOG_DEPOSIT_VAL_PEL_bIAYA_IMPORT_BY_PERUSAHAAN(kode_Perusahaan, No_Faktur_Masuk, no_faktur_Keluar, Nilai, Jenis, Tanggal, Jam, UserID, Id_Rencana_Masuk, Kurs, Nilai_IDR) Values ("
                                                            SQL = SQL & " '" & KodePerusahaan & "', '" & TxtFaktur.Text & "','" & Ds2.Tables("MyTable").Rows(j).Item("No_Faktur") & "', "
                                                            SQL = SQL & " '" & Ds2.Tables("MyTable").Rows(j).Item("sisa") & "', 'AGENT', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & UserID & "',"
                                                            SQL = SQL & " '" & id_rencana & "', '" & HilangkanTanda(TextBoxKurs1.Text) & "', '" & Ds2.Tables("MyTable").Rows(j).Item("sisa") * Val(HilangkanTanda(TextBoxKurs1.Text)) & "') "
                                                            ExecuteTrans(SQL)

                                                        ElseIf total_deposit <= Ds2.Tables("MyTable").Rows(j).Item("sisa") Then

                                                            SQL = "update Deposit_Val_Pel_Biaya_Import_By_Perusahaan set sisa = sisa - " & total_deposit
                                                            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_urut = '" & Ds2.Tables("MyTable").Rows(j).Item("no_urut") & "' "
                                                            ExecuteTrans(SQL)

                                                            SQL = "insert into LOG_DEPOSIT_VAL_PEL_bIAYA_IMPORT_BY_PERUSAHAAN(kode_Perusahaan, No_Faktur_Masuk, no_faktur_Keluar, Nilai, Jenis, Tanggal, Jam, UserID, Id_Rencana_Masuk, Kurs, Nilai_IDR) Values ("
                                                            SQL = SQL & " '" & KodePerusahaan & "', '" & TxtFaktur.Text & "','" & Ds2.Tables("MyTable").Rows(j).Item("No_Faktur") & "', "
                                                            SQL = SQL & " '" & total_deposit & "', 'AGENT', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & UserID & "', "
                                                            SQL = SQL & " '" & id_rencana & "', '" & HilangkanTanda(TextBoxKurs1.Text) & "', '" & total_deposit * Val(HilangkanTanda(TextBoxKurs1.Text)) & "') "
                                                            ExecuteTrans(SQL)

                                                            total_deposit = total_deposit - total_deposit

                                                        End If

                                                    Next

                                                Else
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Terjadi Kesalahan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End Using

                                            If total_deposit = 0 Then
                                                Exit For
                                            End If

                                            If total_deposit < 0 Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Nilai minus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        Next



                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Saldo Rekening Tidak Cukup!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End With
                            End Using

                            If total_deposit <> 0 Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Saldo Rekening Tidak Cukup . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If

                       
                    End If


               

                Next


                If Val(HilangkanTanda(TextBoxADM1.Text)) <> 0 Then
                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_adm & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update 

                            SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(TextBoxADM1.Text) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_adm & "'"
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert
                            pagenumber += 1
                            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_adm, 1), _
                                  Strings.Mid(coa_adm, 2, 1), _
                                  Strings.Mid(Ganti(coa_adm), 3), _
                                  KodePerusahaan, KodeProyek, ket, HilangkanTanda(TextBoxADM1.Text), "0", pagenumber, Ket_Lokasi_HO)
                            ExecuteTrans(SQL)
                        End If
                    End Using
                End If

                If Val(HilangkanTanda(TextBoxsimpan1.Text)) > 0 Then
                    Get_No_Faktur_Deposit()
                    SQL = "INSERT INTO Deposit_Pelunasan"
                    SQL = SQL & "(Kode_Perusahaan,No_Faktur, Tanggal,Jam,UserID,Jenis,Kode_Supplier_Awal,Rek_Awal,MUA_Awal,Nilai_Awal,"
                    SQL = SQL & "Kode_Supplier_Tujuan,Rek_Tujuan,MUA_Tujuan,Nilai_Tujuan,Kurs_Ubah_MUA,Kurs_IDR,Jenis_Deposit,No_Faktur_Deposit,id_rencana_awal,id_rencana_tujuan,keterangan)"
                    SQL = SQL & "VALUES('" & KodePerusahaan & "','" & no_fakturDeposit & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                    SQL = SQL & "'" & UserID & "','DEPOSIT','-','" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "','" & TabPage1.Text & "'," & Val(HilangkanTanda(TextBoxsimpan1.Text)) & ", "
                    SQL = SQL & "'-','" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "','" & TabPage1.Text & "'," & Val(HilangkanTanda(TextBoxsimpan1.Text)) & ",1," & TextBoxKurs1.Text & ",'AGENT','" & TxtFaktur.Text & "', '" & id_rencana & "', '" & id_rencana & "', '" & TextBoxket.Text & "')"
                    ExecuteTrans(SQL)

                    SQL = "insert into Deposit_Val_Pel_Biaya_Import_By_Perusahaan(Kode_Perusahaan, No_Faktur, Mata_Uang, Nilai, Sisa, Kode_supplier, No_Rek, Kurs_IDR, Jenis, id_rencana) Values "
                    SQL = SQL & " ('" & KodePerusahaan & "', '" & no_fakturDeposit & "', '" & TabPage1.Text & "', " & Val(HilangkanTanda(TextBoxsimpan1.Text)) & ", " & Val(HilangkanTanda(TextBoxsimpan1.Text)) & ", "
                    SQL = SQL & "'-', '" & arrRek.Item(ComboBoxRek1.SelectedIndex) & "', " & TextBoxKurs1.Text & ",'DEPOSIT', '" & id_rencana & "')"
                    ExecuteTrans(SQL)

                End If


                For i As Integer = 0 To ListViewMT22.Items.Count - 1
                    Get_Isi_Listview2(i)

                    Dim kodeMasterKategoriBI As String = ""
                    SQL = ";with cte_a as( "
                    SQL = SQL & "select d.Kode_Supplier,d.flag_gabungan,Format(d.Tanggal_PO,'MM.dd') as no_po,d.Lokasi, a.No_Faktur, a.Id_rencana, a.Tanggal+a.Jam as Tgl, a.Keterangan, "
                    SQL = SQL & "c.Kode_Perusahaan_Biaya_import, b.mata_Uang, c.Nama, b.nilai, "
                    SQL = SQL & "isnull((select sum(Y.byr) from Val_Pel_Biaya_import_by_Perusahaan X, "
                    SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
                    SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = b.NO_Faktur and Y.Kode_Perusahaan_Biaya_Import = "
                    SQL = SQL & "b.Kode_Perusahaan_Biaya_import and Y.Mata_Uang = b.Mata_Uang  AND X.STATUS IS NULL),0) as sudah_bayar, c.jenis, "
                    SQL = SQL & "isnull((select top(1)kurs from detail_transaksi_biaya_import x where x.Kode_Perusahaan=b.Kode_Perusahaan and "
                    SQL = SQL & "x.no_faktur=b.no_faktur and x.Kode_Perusahaan_biaya_Import = b.Kode_Perusahaan_biaya_import and x.Mata_Uang=b.Mata_Uang),0) as kurs_hpp, '1' as dari "
                    SQL = SQL & "from transaksi_Biaya_Import a, Detail_transaksi_Biaya_Import_by_Perusahaan b, Perusahaan_Biaya_Import c, Rencana_Order d "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_faktur = b.No_faktur And a.Status Is null "
                    SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Perusahaan_Biaya_Import = "
                    SQL = SQL & "c.Kode_Perusahaan_biaya_import and b.Flag_Lunas is null and a.Kode_Perusahaan=d.Kode_Perusahaan and a.Id_rencana=d.Id_Rencana "

                    SQL = SQL & "union all "

                    SQL = SQL & "select d.Kode_Supplier,d.flag_gabungan, Format(d.Tanggal_PO,'MM.dd') as no_po ,d.Lokasi, a.No_Faktur, a.Id_rencana, a.Tanggal+a.Jam as Tgl, a.Keterangan, "
                    SQL = SQL & "c.Kode_Perusahaan_Biaya_import, b.mata_Uang, c.Nama, b.nilai, "
                    SQL = SQL & "isnull((select sum(Y.byr) from Val_Pel_Biaya_import_by_Perusahaan X, "
                    SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
                    SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = b.NO_Faktur and Y.Kode_Perusahaan_Biaya_Import = "
                    SQL = SQL & "b.Kode_Perusahaan_Biaya_import and Y.Mata_Uang = b.Mata_Uang AND X.STATUS IS NULL),0 ) as sudah_bayar, c.jenis, "
                    SQL = SQL & "isnull((select nilai from hpp_import x, Kurs_HPP_import y where x.Kode_Perusahaan=y.Kode_Perusahaan and x.No_Faktur=y.no_faktur "
                    SQL = SQL & "and x.status is null and y.jenis='FREIGHT' and x.Kode_Perusahaan=a.Kode_Perusahaan and x.Id_rencana=a.Id_rencana and y.mata_uang=b.mata_uang),0) as kurs_hpp, '3' as dari "
                    SQL = SQL & "from transaksi_Biaya_Import3 a, Detail_transaksi_Biaya_Import3_by_Perusahaan b, Perusahaan_Biaya_Import c, Rencana_Order d "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_faktur = b.No_faktur And a.Status Is null "
                    SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Perusahaan_Biaya_Import = "
                    SQL = SQL & "c.Kode_Perusahaan_biaya_import and b.Flag_Lunas is null and a.Kode_Perusahaan=d.Kode_Perusahaan and a.Id_rencana=d.Id_Rencana "
                    SQL = SQL & ")"
                    SQL = SQL & "select* from cte_a where Jenis='LUAR' and No_faktur ='" & LvFak2 & "' and Kode_Perusahaan_biaya_import = '" & LvKP2 & "' and Mata_Uang = '" & LvMT2 & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            flag_gabungan = General_Class.CekNULL(Dr("flag_gabungan"))
                            lks = Dr("lokasi")
                            SisaHutang = Dr("nilai") - Dr("sudah_bayar")
                            JT = ""
                            Dari = Dr("dari")
                            'JT = Dr("jenis_transaksi")
                            KodeCust = Dr("kode_supplier")
                            'kodeMasterKategoriBI = Dr("kode_master_kategori_biaya_import")
                            id_rencana = Dr("id_rencana")

                            'If General_Class.CekNULL(Dr("status")) = "Y" Then
                            '    Dr.Close()
                            '    CloseTrans()
                            '    CloseConn()
                            '    MessageBox.Show("Transaksi ini sudah di batalkan. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'Else
                            If SisaHutang < Val(HilangkanTanda(LvJml2)) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Nomor faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    If SisaHutang = Val(HilangkanTanda(LvJml2)) Then

                        If Dari = "1" Then

                            SQL = "Update detail_transaksi_Biaya_Import_by_Perusahaan set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak2.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP2 & "' and Mata_Uang ='" & LvMT2 & "' "
                            ExecuteTrans(SQL)
                        ElseIf Dari = "3" Then

                            SQL = "Update detail_transaksi_Biaya_Import3_by_Perusahaan set flag_lunas = 'Y', "
                            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "jam_lunas = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                            SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & LvFak2.Trim & "' and kode_Perusahaan_biaya_import = '" & LvKP2 & "' and Mata_Uang ='" & LvMT2 & "' "
                            ExecuteTrans(SQL)
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tabel asal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                    End If


                    Dim nilai_hpp As Double = Val(HilangkanTanda(Format(Val(HilangkanTanda(LvJml2)) * Val(HilangkanTanda(Lvkurshpp2)), "N0")))
                    Dim nilai_akhir As Double = Val(HilangkanTanda(Format(Val(HilangkanTanda(LvJml2)) * Val(HilangkanTanda(TextBoxKurs2.Text)), "N0")))
                    Dim selisih As Double = nilai_akhir - nilai_hpp

                    SQL = "insert into detail_val_pel_biaya_import_By_Perusahaan(kode_perusahaan, no_val, no_faktur,kode_Perusahaan_biaya_import, Mata_Uang, byr, Kurs_HPP, Kurs_Akhir, Selisih_Kurs,  Selisih_Po_Sebelum) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & LvFak2.Trim & "', '" & LvKP2 & "', '" & LvMT2 & "'," & HilangkanTanda(LvJml2) & "," & HilangkanTanda(Lvkurshpp2) & ", '" & HilangkanTanda(TextBoxKurs2.Text) & "', '" & selisih & "', '" & HilangkanTanda(Lvselisih2) & "') "
                    ExecuteTrans(SQL)

                    If Val(HilangkanTanda(Lvselisih2)) <> 0 Then
                        SQL = SQL & "select isnull(sum(Nilai_Selisih),0) as sisa_selisih, isnull((select a.flag_lunas from "
                        SQL = SQL & "detail_transaksi_Biaya_Import_by_Perusahaan a where a.no_faktur='" & LvFak2 & "' and a.Kode_Perusahaan_biaya_import ='" & LvKP2 & "' and a.mata_uang='" & LvMT2 & "'),NULL) as flag_lunas "
                        SQL = SQL & ",isnull((select sum(Y.selisih_po_Sebelum) from Val_Pel_Biaya_import_by_Perusahaan X, "
                        SQL = SQL & "detail_Val_Pel_Biaya_import_by_Perusahaan Y where X.Kode_Perusahaan = Y.Kode_Perusahaan and "
                        SQL = SQL & "X.No_Val = Y.No_Val and Y.No_faktur = '" & LvFak2 & "' and Y.Kode_Perusahaan_Biaya_Import = "
                        SQL = SQL & "'" & LvKP2 & "' and Y.Mata_Uang = '" & LvMT2 & "'),0) as selisih_PO "
                        SQL = SQL & "from hpp_import j, HPP_Import_Log_Selisih k where j.Kode_Perusahaan=k.Kode_Perusahaan and j.No_Faktur=k.NO_Faktur and j.Status is null "
                        SQL = SQL & " and j.Kode_Perusahaan='" & KodePerusahaan & "' and j.Id_Rencana='" & id_rencana & "' and k.jenis='" & LvKP2 & "'"

                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then

                                If Math.Abs(Dr("Sisa_Selisih")) - Math.Abs(Dr("selisih_PO")) < Math.Abs(Val(HilangkanTanda(Lvselisih2))) Then
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Selisih yang di input melebihi Jumlah selisih . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Exit Sub
                                End If

                                If General_Class.CekNULL(Dr("Flag_Lunas")) = "Y" Then

                                    If Dr("Sisa_Selisih") - (Dr("selisih_PO")) - (Val(HilangkanTanda(Lvselisih2))) <> 0 Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Semua sisa selisih harus di input . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Exit Sub
                                    End If

                                End If


                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("No Faktur Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Exit Sub
                            End If
                        End Using
                    End If

                    Dim gabung As String = "NULL"
                    If flag_gabungan = "Y" Then
                        gabung = "'Y'"
                    End If


                    If selisih <> 0 Then
                        SQL = "insert into Selisih_kurs_biaya_import_By_Perusahaan( kode_perusahaan, No_Val, Id_rencana, Kode_Perusahaan_Biaya_Import, Flag_Gabungan, Nilai, Kode_supplier, sisa) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                        SQL = SQL & "'" & id_rencana & "', '" & LvKP2 & "', " & gabung & "  ," & selisih & ",'" & KodeCust & "'," & selisih & ")"
                        ExecuteTrans(SQL)
                    End If


                    Dim coa_Selisih As String = ""
                    Dim coa_hutang As String = ""
                    SQL = "select hutang_Import, Akun_Selisih_PO_biaya, akun_hutang_declare, Biaya_admin from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "Kode_STock_Owner = '" & lks & "' "

                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            coa_hutang = dr("hutang_Import")
                            coa_Selisih = dr("Akun_Selisih_PO_biaya")
                            coa_hutang_declare = dr("akun_hutang_declare")
                            coa_adm = dr("Biaya_Admin")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data kode master tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    sum_biaya2 = sum_biaya2 + nilai_akhir

                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update 

                            SQL = "update detail_jurnal set debit = debit+ " & nilai_hpp & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "'"
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert
                            pagenumber += 1
                            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang, 1), _
                                  Strings.Mid(coa_hutang, 2, 1), _
                                  Strings.Mid(Ganti(coa_hutang), 3), _
                                  KodePerusahaan, KodeProyek, ket, nilai_hpp, "0", pagenumber, Ket_Lokasi_HO)
                            ExecuteTrans(SQL)

                        End If
                    End Using

                    If selisih <> 0 Then

                        If selisih > 0 Then
                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and debit <> 0"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set debit = debit+ " & selisih & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and debit <> 0"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    pagenumber += 1
                                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Selisih, 1), _
                                          Strings.Mid(coa_Selisih, 2, 1), _
                                          Strings.Mid(Ganti(coa_Selisih), 3), _
                                          KodePerusahaan, KodeProyek, ket, selisih, "0", pagenumber, Ket_Lokasi_HO)
                                    ExecuteTrans(SQL)
                                End If
                            End Using
                        Else
                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and kredit <> 0"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set kredit = kredit+ " & Math.Abs(selisih) & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and kredit <> 0"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    pagenumber += 1
                                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Selisih, 1), _
                                          Strings.Mid(coa_Selisih, 2, 1), _
                                          Strings.Mid(Ganti(coa_Selisih), 3), _
                                          KodePerusahaan, KodeProyek, ket, "0", Math.Abs(selisih), pagenumber, Ket_Lokasi_HO)
                                    ExecuteTrans(SQL)
                                End If
                            End Using
                        End If
                    End If

                    If Val(HilangkanTanda(Lvselisih2)) <> 0 Then
                        total_selisih2 = total_selisih2 + Val(HilangkanTanda(Lvselisih2))
                        If sudah_jurnal = False Then
                            Kode_Voucher2 = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPemb, KodePerusahaan)
                            kode_voucher2_ = "'" & Kode_Voucher2 & "'"

                            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                            SQL = SQL & "'" & Kode_Voucher2 & "', "
                            SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                            SQL = SQL & "'" & KodeProyek & "', 'Pelunasan hutang " & TxtFaktur.Text.Trim & "', '', "
                            SQL = SQL & "'-', '" & UserID & "')"
                            ExecuteTrans(SQL)

                            sudah_jurnal = True
                        End If


                        'voucher selisih sebelum
                        If Val(HilangkanTanda(Lvselisih2)) > 0 Then
                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and kredit <> 0"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set kredit = kredit+ " & Val(HilangkanTanda(Lvselisih2)) & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and kredit <> 0"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    pagenumber2 += 1
                                    SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_Selisih, 1), _
                                          Strings.Mid(coa_Selisih, 2, 1), _
                                          Strings.Mid(Ganti(coa_Selisih), 3), _
                                          KodePerusahaan, KodeProyek, ket, "0", Val(HilangkanTanda(Lvselisih2)), pagenumber2, Ket_Lokasi_HO)
                                    ExecuteTrans(SQL)
                                End If
                            End Using

                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Val(HilangkanTanda(Lvselisih2))) & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    pagenumber2 += 1
                                    SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_hutang, 1), _
                                          Strings.Mid(coa_hutang, 2, 1), _
                                          Strings.Mid(Ganti(coa_hutang), 3), _
                                          KodePerusahaan, KodeProyek, ket, Math.Abs(Val(HilangkanTanda(Lvselisih2))), "0", pagenumber2, Ket_Lokasi_HO)
                                    ExecuteTrans(SQL)
                                End If
                            End Using
                        Else
                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and debit <> 0"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Val(HilangkanTanda(Lvselisih2))) & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Selisih & "' and debit <> 0"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    pagenumber2 += 1
                                    SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_Selisih, 1), _
                                          Strings.Mid(coa_Selisih, 2, 1), _
                                          Strings.Mid(Ganti(coa_Selisih), 3), _
                                          KodePerusahaan, KodeProyek, ket, Math.Abs(Val(HilangkanTanda(Lvselisih2))), "0", pagenumber2, Ket_Lokasi_HO)
                                    ExecuteTrans(SQL)
                                End If
                            End Using

                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and kredit <> 0"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set kredit = kredit+ " & Math.Abs(Val(HilangkanTanda(Lvselisih2))) & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and kredit <> 0"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    pagenumber2 += 1
                                    SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_hutang, 1), _
                                          Strings.Mid(coa_hutang, 2, 1), _
                                          Strings.Mid(Ganti(coa_hutang), 3), _
                                          KodePerusahaan, KodeProyek, ket, "0", Math.Abs(Val(HilangkanTanda(Lvselisih2))), pagenumber2, Ket_Lokasi_HO)
                                    ExecuteTrans(SQL)
                                End If
                            End Using
                        End If

                    End If

                    If Val(HilangkanTanda(TextBoxDPT2.Text)) > 0 Then

                        Dim flag_gabung As String = ""
                        Dim faktur_gabung As String = ""
                        SQL = "select*,"

                        SQL = SQL & "CASE when flag_gabung_declare = 'Y' then "
                        SQL = SQL & "isnull(round((select  top(1) x.kurs_akhir  "
                        SQL = SQL & "from pelunasan_declare_gabungan x, pelunasan_declare_gabungan_detail2 y "
                        SQL = SQL & "where(x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur And x.status Is null) "
                        SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.id_rencana=a.id_rencana),5),0)  "
                        SQL = SQL & "ELSE "
                        SQL = SQL & "isnull(round((select sum(x.Total_IDR)/sum(x.Nilai_Tambahan) from Val_Pel_Declare_Invoice_Import x, "
                        SQL = SQL & "detail_Val_Pel_Declare_Invoice_Import y where x.Kode_Perusahaan= y.Kode_Perusahaan and x.No_Val=y.No_Val "
                        SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.No_Faktur=a.id_rencana and x.Status is null),5),0) END as Kurs_Akhir "

                        SQL = SQL & ",isnull((select  top(1) x.no_faktur  "
                        SQL = SQL & "from pelunasan_declare_gabungan x, pelunasan_declare_gabungan_detail2 y "
                        SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.status is null "
                        SQL = SQL & "and y.Kode_Perusahaan=a.Kode_Perusahaan and y.id_rencana=a.id_rencana),'') as Faktur_gabung_Declare  "

                        SQL = SQL & ",isnull((select STRING_AGG('''' + x.no_val +'''',',') from Val_Pel_Declare_Invoice_Import X, detail_Val_Pel_Declare_Invoice_Import Y "
                        SQL = SQL & "where X.Kode_Perusahaan=Y.Kode_Perusahaan and X.No_Val=Y.No_Val and X.status is null and "
                        SQL = SQL & "Y.Kode_Perusahaan=a.Kode_Perusahaan and Y.No_faktur=a.Id_Rencana ),'') as No_Declare "

                        SQL = SQL & ",isnull((select Flag_Lunas_Declare from submit_PO x where "
                        SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.id_rencana=a.id_rencana and x.Status is null),'')as Flag_Lunas_Declare_submit "


                        SQL = SQL & ",isnull((select Kode_Supplier from Rencana_Order x where "
                        SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.id_rencana=a.id_rencana and x.Status is null),'')as Kode_Supplier "
                        SQL = SQL & "from loading_barang a where Id_Rencana='" & LvRencana2 & "' and a.Status is null "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                flag_gabung = General_Class.CekNULL(dr("flag_gabung_declare"))
                                faktur_gabung = General_Class.CekNULL(dr("Faktur_gabung_Declare"))

                            End If
                        End Using


                        If flag_gabung = "Y" Then
                            Dim nilai_sisa As Double = Val(HilangkanTanda(LvJml2))

                            SQL = "select Id_Rencana from Pelunasan_Declare_Gabungan a, Pelunasan_Declare_Gabungan_detail1 b  where a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Faktur=b.No_faktur and "
                            SQL = SQL & " a.status is null and a.no_faktur='" & faktur_gabung & "' "
                            Using Ds3 = BindingTrans(SQL)
                                If Ds3.Tables("MyTable").Rows.Count <> 0 Then

                                    For index3 As Integer = 0 To Ds3.Tables("MyTable").Rows.Count - 1

                                        'For index4 As Integer = 0 To ListView1.Items.Count - 1
                                        '    Get_Isi_Listview(index4)
                                        SQL = "select no_urut, sisa from Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, "
                                        SQL = SQL & "Deposit_Pelunasan b  where "
                                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur= B.No_Faktur "
                                        SQL = SQL & "and round(a.sisa,2) <> 0 and b.Status is null and Mata_Uang= '" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "' "
                                        SQL = SQL & " and No_Rek = '" & arrRek2.Item(ComboBoxRek2.SelectedIndex) & "' and a.Kode_Supplier = '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex) & "' "
                                        SQL = SQL & "and a.id_rencana='" & Ds3.Tables("MyTable").Rows(index3).Item("id_rencana") & "' "
                                        SQL = SQL & "order by No_urut"
                                        Using Ds = BindingTrans(SQL)
                                            With Ds.Tables("MyTable")
                                                If .Rows.Count <> 0 Then

                                                    For index As Integer = 0 To .Rows.Count - 1

                                                        SQL = "select top(1) no_urut, sisa, a.No_Faktur from Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, "
                                                        SQL = SQL & "Deposit_Pelunasan b  where "
                                                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur= B.No_Faktur "
                                                        SQL = SQL & "and round(a.sisa,2) <> 0 and b.Status is null and Mata_Uang='" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "' "
                                                        SQL = SQL & " and No_Rek ='" & arrRek2.Item(ComboBoxRek2.SelectedIndex) & "' and a.Kode_Supplier ='" & arrSupp2.Item(ComboBoxRek2.SelectedIndex) & "' "
                                                        SQL = SQL & "and a.id_rencana='" & Ds3.Tables("MyTable").Rows(index3).Item("id_rencana") & "' "
                                                        SQL = SQL & "order by No_urut"
                                                        Using Ds2 = BindingTrans(SQL)
                                                            If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                                                For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                                                    If nilai_sisa > Ds2.Tables("MyTable").Rows(j).Item("sisa") Then

                                                                        nilai_sisa = nilai_sisa - Ds2.Tables("MyTable").Rows(j).Item("sisa")


                                                                        SQL = "update Deposit_Val_Pel_Biaya_Import_By_Perusahaan set sisa = sisa - " & Ds2.Tables("MyTable").Rows(j).Item("sisa")
                                                                        SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_urut = '" & Ds2.Tables("MyTable").Rows(j).Item("no_urut") & "' "
                                                                        ExecuteTrans(SQL)

                                                                        SQL = "insert into LOG_DEPOSIT_VAL_PEL_bIAYA_IMPORT_BY_PERUSAHAAN(kode_Perusahaan, No_Faktur_Masuk, no_faktur_Keluar, Nilai, Jenis, Tanggal, Jam, UserID, Id_Rencana_Masuk, Kurs, Nilai_IDR) Values ("
                                                                        SQL = SQL & " '" & KodePerusahaan & "', '" & TxtFaktur.Text & "','" & Ds2.Tables("MyTable").Rows(j).Item("No_Faktur") & "', "
                                                                        SQL = SQL & " '" & Ds2.Tables("MyTable").Rows(j).Item("sisa") & "', 'AGENT', '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & UserID & "', "
                                                                        SQL = SQL & "'" & LvRencana2 & "', '" & Val(HilangkanTanda(TextBoxKurs2.Text)) & "', '" & Ds2.Tables("MyTable").Rows(j).Item("sisa") * Val(HilangkanTanda(TextBoxKurs2.Text)) & "') "
                                                                        ExecuteTrans(SQL)

                                                                        'SQL = "update selisih_pelunasan_loading_barang set sisa = sisa - " & Ds2.Tables("MyTable").Rows(j).Item("sisa")
                                                                        'SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_pelunasan='" & TxtFaktur.Text & "' and Id_rencana = '" & Ds3.Tables("MyTable").Rows(index3).Item("id_rencana") & "' "
                                                                        'ExecuteTrans(SQL)

                                                                    ElseIf nilai_sisa <= Ds2.Tables("MyTable").Rows(j).Item("sisa") Then

                                                                        SQL = "update Deposit_Val_Pel_Biaya_Import_By_Perusahaan set sisa = sisa - " & nilai_sisa
                                                                        SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_urut = '" & Ds2.Tables("MyTable").Rows(j).Item("no_urut") & "' "
                                                                        ExecuteTrans(SQL)

                                                                        SQL = "insert into LOG_DEPOSIT_VAL_PEL_bIAYA_IMPORT_BY_PERUSAHAAN(kode_Perusahaan, No_Faktur_Masuk, no_faktur_Keluar, Nilai, Jenis, Tanggal, Jam, UserID, Id_Rencana_Masuk, Kurs, Nilai_IDR) Values ("
                                                                        SQL = SQL & " '" & KodePerusahaan & "', '" & TxtFaktur.Text & "','" & Ds2.Tables("MyTable").Rows(j).Item("No_Faktur") & "', "
                                                                        SQL = SQL & " '" & nilai_sisa & "', 'AGENT', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & UserID & "', '" & LvRencana2 & "', "
                                                                        SQL = SQL & "'" & Val(HilangkanTanda(TextBoxKurs2.Text)) & "', '" & nilai_sisa * Val(HilangkanTanda(TextBoxKurs2.Text)) & "') "
                                                                        ExecuteTrans(SQL)

                                                                        'SQL = "update selisih_pelunasan_loading_barang set sisa = sisa - " & nilai_sisa
                                                                        'SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_pelunasan='" & TxtFaktur.Text & "' and Id_rencana = '" & Ds3.Tables("MyTable").Rows(index3).Item("id_rencana") & "' "
                                                                        'ExecuteTrans(SQL)

                                                                        nilai_sisa = nilai_sisa - nilai_sisa

                                                                    End If

                                                                Next

                                                            Else
                                                                CloseTrans()
                                                                CloseConn()
                                                                MessageBox.Show("Terjadi Kesalahan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                Exit Sub
                                                            End If
                                                        End Using

                                                        If nilai_sisa = 0 Then
                                                            Exit For
                                                        End If

                                                        If nilai_sisa < 0 Then
                                                            CloseTrans()
                                                            CloseConn()
                                                            MessageBox.Show("Nilai minus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    Next
                                                    'Else
                                                    '    CloseTrans()
                                                    '    CloseConn()
                                                    '    MessageBox.Show("Saldo Rekening Tidak Cukup!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    '    Exit Sub
                                                End If
                                            End With

                                        End Using
                                        'Next


                                    Next

                                    If nilai_sisa <> 0 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Rekening Tidak Cukup . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Exit Sub
                                    End If

                                End If

                            End Using
                        Else
                            Dim idrncn As String = ""

                            If cmbPenerima.SelectedIndex = 2 Then
                                idrncn = ArrRencana2.Item(ComboBoxRek2.SelectedIndex)
                            Else
                                idrncn = id_rencana
                            End If

                            Dim total_deposit As Double = Val(HilangkanTanda(LvJml2))

                            SQL = "insert into log_deposit_Pelunasan_Per_faktur(kode_Perusahaan, No_Faktur_Masuk, no_Rek_Keluar, Nilai, Jenis, Tanggal, Jam, UserID, Kurs, Nilai_IDR, Kode_Supplier, Mata_Uang) Values ("
                            SQL = SQL & " '" & KodePerusahaan & "', '" & TxtFaktur.Text & "','" & arrRek2.Item(ComboBoxRek2.SelectedIndex) & "', "
                            SQL = SQL & " '" & total_deposit & "', 'AGENT', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & UserID & "', "
                            SQL = SQL & "'" & TextBoxKurs2.Text & "', '" & total_deposit * Val(TextBoxKurs2.Text) & "', '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex) & "', '" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "') "
                            ExecuteTrans(SQL)

                            SQL = "select no_urut, sisa from Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, "
                            SQL = SQL & "Deposit_Pelunasan b  where "
                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur= B.No_Faktur "
                            SQL = SQL & "and round(a.sisa,2) <> 0 and b.Status is null and kode_supplier = '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex) & "' and Mata_Uang='" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "' "
                            SQL = SQL & " and No_Rek ='" & arrRek2.Item(ComboBoxRek2.SelectedIndex) & "' and a.id_rencana='" & idrncn & "' "
                            SQL = SQL & "order by No_urut"
                            Using Ds = BindingTrans(SQL)
                                With Ds.Tables("MyTable")
                                    If .Rows.Count <> 0 Then

                                        For index As Integer = 0 To .Rows.Count - 1

                                            SQL = "select top(1) no_urut, sisa, a.No_Faktur from Deposit_Val_Pel_Biaya_Import_By_Perusahaan a, "
                                            SQL = SQL & "Deposit_Pelunasan b  where "
                                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur= B.No_Faktur "
                                            SQL = SQL & "and round(a.sisa,2) <> 0 and b.Status is null and kode_supplier = '" & arrSupp2.Item(ComboBoxRek2.SelectedIndex) & "'  and Mata_Uang='" & arrMUA2.Item(ComboBoxRek2.SelectedIndex) & "' "
                                            SQL = SQL & " and No_Rek ='" & arrRek2.Item(ComboBoxRek2.SelectedIndex) & "' and a.id_rencana='" & idrncn & "'  "
                                            SQL = SQL & "order by No_urut"
                                            Using Ds2 = BindingTrans(SQL)
                                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                                    For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                                        If total_deposit > Ds2.Tables("MyTable").Rows(j).Item("sisa") Then

                                                            total_deposit = total_deposit - Ds2.Tables("MyTable").Rows(j).Item("sisa")


                                                            SQL = "update Deposit_Val_Pel_Biaya_Import_By_Perusahaan set sisa = sisa - " & Ds2.Tables("MyTable").Rows(j).Item("sisa")
                                                            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_urut = '" & Ds2.Tables("MyTable").Rows(j).Item("no_urut") & "' "
                                                            ExecuteTrans(SQL)

                                                            SQL = "insert into LOG_DEPOSIT_VAL_PEL_bIAYA_IMPORT_BY_PERUSAHAAN(kode_Perusahaan, No_Faktur_Masuk, no_faktur_Keluar, Nilai, Jenis, Tanggal, Jam, UserID, Id_Rencana_Masuk, Kurs, Nilai_IDR) Values ("
                                                            SQL = SQL & " '" & KodePerusahaan & "', '" & TxtFaktur.Text & "','" & Ds2.Tables("MyTable").Rows(j).Item("No_Faktur") & "', "
                                                            SQL = SQL & " '" & Ds2.Tables("MyTable").Rows(j).Item("sisa") & "', 'AGENT', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & UserID & "',"
                                                            SQL = SQL & " '" & id_rencana & "', '" & HilangkanTanda(TextBoxKurs2.Text) & "', '" & Ds2.Tables("MyTable").Rows(j).Item("sisa") * Val(HilangkanTanda(TextBoxKurs2.Text)) & "') "
                                                            ExecuteTrans(SQL)

                                                        ElseIf total_deposit <= Ds2.Tables("MyTable").Rows(j).Item("sisa") Then

                                                            SQL = "update Deposit_Val_Pel_Biaya_Import_By_Perusahaan set sisa = sisa - " & total_deposit
                                                            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and no_urut = '" & Ds2.Tables("MyTable").Rows(j).Item("no_urut") & "' "
                                                            ExecuteTrans(SQL)

                                                            SQL = "insert into LOG_DEPOSIT_VAL_PEL_bIAYA_IMPORT_BY_PERUSAHAAN(kode_Perusahaan, No_Faktur_Masuk, no_faktur_Keluar, Nilai, Jenis, Tanggal, Jam, UserID, Id_Rencana_Masuk, Kurs, Nilai_IDR) Values ("
                                                            SQL = SQL & " '" & KodePerusahaan & "', '" & TxtFaktur.Text & "','" & Ds2.Tables("MyTable").Rows(j).Item("No_Faktur") & "', "
                                                            SQL = SQL & " '" & total_deposit & "', 'AGENT', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & UserID & "', "
                                                            SQL = SQL & " '" & id_rencana & "', '" & HilangkanTanda(TextBoxKurs2.Text) & "', '" & total_deposit * Val(HilangkanTanda(TextBoxKurs2.Text)) & "') "
                                                            ExecuteTrans(SQL)

                                                            total_deposit = total_deposit - total_deposit

                                                        End If

                                                    Next

                                                Else
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Terjadi Kesalahan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End Using

                                            If total_deposit = 0 Then
                                                Exit For
                                            End If

                                            If total_deposit < 0 Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Nilai minus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        Next



                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Saldo Rekening Tidak Cukup!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End With
                            End Using

                            If total_deposit <> 0 Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Saldo Rekening Tidak Cukup . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If

                    End If
                Next




                If Val(HilangkanTanda(TextBoxSLS2.Text)) > 0 Then
                    Get_No_Faktur_Deposit()
                    SQL = "INSERT INTO Deposit_Pelunasan"
                    SQL = SQL & "(Kode_Perusahaan,No_Faktur, Tanggal,Jam,UserID,Jenis,Kode_Supplier_Awal,Rek_Awal,MUA_Awal,Nilai_Awal,"
                    SQL = SQL & "Kode_Supplier_Tujuan,Rek_Tujuan,MUA_Tujuan,Nilai_Tujuan,Kurs_Ubah_MUA,Kurs_IDR,Jenis_Deposit,No_Faktur_Deposit,id_rencana_awal,id_rencana_tujuan,keterangan)"
                    SQL = SQL & "VALUES('" & KodePerusahaan & "','" & no_fakturDeposit & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                    SQL = SQL & "'" & UserID & "','DEPOSIT','-','" & arrRek2.Item(ComboBoxRek2.SelectedIndex) & "','" & TabPage2.Text & "'," & Val(HilangkanTanda(TextBoxSLS2.Text)) & ", "
                    SQL = SQL & "'-','" & arrRek2.Item(ComboBoxRek2.SelectedIndex) & "','" & TabPage2.Text & "'," & Val(HilangkanTanda(TextBoxSLS2.Text)) & ",1," & TextBoxKurs2.Text & ",'AGENT','" & TxtFaktur.Text & "','" & id_rencana & "','" & id_rencana & "','" & TextBoxket.Text & "')"
                    ExecuteTrans(SQL)

                    SQL = "insert into Deposit_Val_Pel_Biaya_Import_By_Perusahaan(Kode_Perusahaan, No_Faktur, Mata_Uang, Nilai, Sisa, Kode_supplier, No_Rek, Kurs_IDR, Jenis, Id_Rencana) Values "
                    SQL = SQL & " ('" & KodePerusahaan & "', '" & no_fakturDeposit & "', '" & TabPage2.Text & "', " & Val(HilangkanTanda(TextBoxSLS2.Text)) & ", " & Val(HilangkanTanda(TextBoxSLS2.Text)) & ", "
                    SQL = SQL & "'-', '" & arrRek2.Item(ComboBoxRek2.SelectedIndex) & "', " & TextBoxKurs2.Text & ",'DEPOSIT','" & id_rencana & "')"
                    ExecuteTrans(SQL)
                End If


                If Val(HilangkanTanda(TextBoxADM2.Text)) <> 0 Then
                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_adm & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update 

                            SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(TextBoxADM2.Text) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_adm & "'"
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert
                            pagenumber += 1
                            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_adm, 1), _
                                  Strings.Mid(coa_adm, 2, 1), _
                                  Strings.Mid(Ganti(coa_adm), 3), _
                                  KodePerusahaan, KodeProyek, ket, HilangkanTanda(TextBoxADM2.Text), "0", pagenumber, Ket_Lokasi_HO)
                            ExecuteTrans(SQL)
                        End If
                    End Using
                End If

                

                
                If Val(HilangkanTanda(TextBoxtotIDR1.Text)) <> 0 Then
                    If cmbPenerima.SelectedIndex = 2 Then

                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & arrakunrek1.Item(ComboBoxRek1.SelectedIndex) & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set kredit = kredit+ " & sum_biaya + Val(HilangkanTanda(TextBoxADM1.Text)) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & arrakunrek1.Item(ComboBoxRek1.SelectedIndex) & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                pagenumber += 1
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(arrakunrek1.Item(ComboBoxRek1.SelectedIndex), 1), _
                                      Strings.Mid(arrakunrek1.Item(ComboBoxRek1.SelectedIndex), 2, 1), _
                                      Strings.Mid(Ganti(arrakunrek1.Item(ComboBoxRek1.SelectedIndex)), 3), _
                                      KodePerusahaan, KodeProyek, ket, "0", sum_biaya + Val(HilangkanTanda(TextBoxADM1.Text)), pagenumber, Ket_Lokasi_HO)
                                ExecuteTrans(SQL)
                            End If
                        End Using

                    Else

                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang_declare & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set kredit = kredit+ " & sum_biaya + Val(HilangkanTanda(TextBoxADM1.Text)) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang_declare & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                pagenumber += 1
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang_declare, 1), _
                                      Strings.Mid(coa_hutang_declare, 2, 1), _
                                      Strings.Mid(Ganti(coa_hutang_declare), 3), _
                                      KodePerusahaan, KodeProyek, ket, "0", sum_biaya + Val(HilangkanTanda(TextBoxADM1.Text)), pagenumber, Ket_Lokasi_HO)
                                ExecuteTrans(SQL)
                            End If
                        End Using
                    End If


                End If

                If Val(HilangkanTanda(TextBoxtotIDR2.Text)) <> 0 Then
                    If cmbPenerima.SelectedIndex = 2 Then

                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & arrakunrek1.Item(ComboBoxRek1.SelectedIndex) & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set kredit = kredit+ " & sum_biaya2 + Val(HilangkanTanda(TextBoxADM2.Text)) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & arrakunrek1.Item(ComboBoxRek1.SelectedIndex) & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                pagenumber += 1
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(arrakunrek1.Item(ComboBoxRek1.SelectedIndex), 1), _
                                      Strings.Mid(arrakunrek1.Item(ComboBoxRek1.SelectedIndex), 2, 1), _
                                      Strings.Mid(Ganti(arrakunrek1.Item(ComboBoxRek1.SelectedIndex)), 3), _
                                      KodePerusahaan, KodeProyek, ket, "0", sum_biaya2 + Val(HilangkanTanda(TextBoxADM2.Text)), pagenumber, Ket_Lokasi_HO)
                                ExecuteTrans(SQL)
                            End If
                        End Using

                    Else

                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang_declare & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set kredit = kredit+ " & sum_biaya2 + Val(HilangkanTanda(TextBoxADM2.Text)) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang_declare & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                pagenumber += 1
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang_declare, 1), _
                                      Strings.Mid(coa_hutang_declare, 2, 1), _
                                      Strings.Mid(Ganti(coa_hutang_declare), 3), _
                                      KodePerusahaan, KodeProyek, ket, "0", sum_biaya2 + Val(HilangkanTanda(TextBoxADM2.Text)), pagenumber, Ket_Lokasi_HO)
                                ExecuteTrans(SQL)
                            End If
                        End Using
                    End If


                End If

                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                SQL = "select round(sum(debit), 0) as debit, round(sum(kredit), 0) as kredit from detail_jurnal where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("debit") <> Dr("kredit") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                If Kode_Voucher2 <> "" Then
                    SQL = "select round(sum(debit), 2) as debit, round(sum(kredit), 2) as kredit from detail_jurnal where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("debit") <> Dr("kredit") Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Jurnal 2 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data jurnal 2 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "Update val_pel_biaya_import_By_Perusahaan set Kode_voucher2=" & kode_voucher2_ & " "
                    SQL = SQL & "where No_Val='" & TxtFaktur.Text.Trim & "'"
                    ExecuteTrans(SQL)

                End If

                MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else 'update

            ' ''Dim tny As String = MessageBox.Show("Yakin akan diupdate?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            ' ''If tny = vbNo Then Exit Sub

            ' ''Try
            ' ''    OpenConn()

            ' ''    Cmd.Transaction = Cn.BeginTransaction

            ' ''    If CekButtonRole("update_pelunasan_pembelian") = "T" Then
            ' ''        CloseTrans()
            ' ''        CloseConn()
            ' ''        MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''        Exit Sub
            ' ''    End If

            ' ''    Dim kode_voucher_lama As String = ""
            ' ''    SQL = "select kode_voucher, status from val_pemb where "
            ' ''    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''    SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
            ' ''    Using Ds = BindingTrans(SQL)
            ' ''        With Ds.Tables("MyTable")
            ' ''            If .Rows.Count <> 0 Then
            ' ''                kode_voucher_lama = .Rows(0).Item("kode_voucher")
            ' ''                If General_Class.CekNULL(.Rows(0).Item("status")) = "Y" Then
            ' ''                    CloseTrans()
            ' ''                    CloseConn()
            ' ''                    MessageBox.Show("Pelunasan tidak bisa diupdate, karena sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' ''                    Exit Sub
            ' ''                End If
            ' ''            Else
            ' ''                CloseTrans()
            ' ''                CloseConn()
            ' ''                MessageBox.Show("Transaksi tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                Exit Sub
            ' ''            End If
            ' ''        End With
            ' ''    End Using

            ' ''    SQL = "select a.kode_supplier, a.no_faktur, b.no_val, b.byr from pembelian a, detail_val_pemb b where "
            ' ''    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur And "
            ' ''    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''    SQL = SQL & "b.no_val = '" & TxtFaktur.Text.Trim & "'"
            ' ''    Using Ds = BindingTrans(SQL)
            ' ''        With Ds.Tables("MyTable")
            ' ''            If .Rows.Count <> 0 Then
            ' ''                For i As Integer = 0 To .Rows.Count - 1
            ' ''                    SQL = "select hutang from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''                    SQL = SQL & "kode_supplier = '" & .Rows(i).Item("kode_supplier") & "'"
            ' ''                    Using Dr = OpenTrans(SQL)
            ' ''                        If Dr.Read Then
            ' ''                            Dr.Close()

            ' ''                            SQL = "update suppliers set hutang = hutang + " & .Rows(i).Item("byr") & " where "
            ' ''                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''                            SQL = SQL & "kode_supplier = '" & .Rows(i).Item("kode_supplier") & "'"
            ' ''                            ExecuteTrans(SQL)
            ' ''                        Else
            ' ''                            Dr.Close()
            ' ''                            CloseTrans()
            ' ''                            CloseConn()
            ' ''                            MessageBox.Show("Supplier tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' ''                            Exit Sub
            ' ''                        End If
            ' ''                    End Using

            ' ''                    SQL = "Update pembelian set flag_lunas = NULL, "
            ' ''                    SQL = SQL & "Tgl_lunas = NULL, "
            ' ''                    SQL = SQL & "jam_lunas = NULL, "
            ' ''                    SQL = SQL & "uservalidasi = NULL where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''                    SQL = SQL & "no_faktur = '" & .Rows(i).Item("no_faktur") & "'"
            ' ''                    ExecuteTrans(SQL)
            ' ''                Next
            ' ''            Else
            ' ''                CloseTrans()
            ' ''                CloseConn()
            ' ''                MessageBox.Show("Pelunasan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                Exit Sub
            ' ''            End If
            ' ''        End With
            ' ''    End Using


            ' ''    '=============

            ' ''    SQL = "delete from detail_val_pemb where "
            ' ''    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''    SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
            ' ''    ExecuteTrans(SQL)

            ' ''    Dim SisaHutang As Double = 0
            ' ''    Dim JT As String = ""
            ' ''    Dim KodeCust As String = ""

            ' ''    For i As Integer = 0 To ListViewMT11.Items.Count - 1
            ' ''        Get_Isi_Listview(i)

            ' ''        SQL = "select a.total_mua as grand, a.status, a.jenis_transaksi, a.kode_supplier, "
            ' ''        SQL = SQL & "isnull((select sum(x.grand) as ttl_retur from retur_pembelian x where x.kode_perusahaan = a.kode_perusahaan and x.no_faktur_beli = a.no_faktur and x.status is null), 0) as ttl_retur, "
            ' ''        SQL = SQL & "isnull((select sum(y.byr) from val_pemb x, detail_val_pemb y where "
            ' ''        SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
            ' ''        SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
            ' ''        SQL = SQL & "y.no_faktur = a.no_faktur), 0) as ttl_validasi "
            ' ''        SQL = SQL & "from pembelian a where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''        SQL = SQL & "no_faktur = '" & LvFak.Trim & "' "
            ' ''        Using Dr = OpenTrans(SQL)
            ' ''            If Dr.Read Then
            ' ''                SisaHutang = Dr("grand") - Dr("ttl_retur") - Dr("ttl_validasi")
            ' ''                JT = Dr("jenis_transaksi")
            ' ''                KodeCust = Dr("kode_supplier")

            ' ''                If JT = "T" Then
            ' ''                    Dr.Close()
            ' ''                    CloseTrans()
            ' ''                    CloseConn()
            ' ''                    MessageBox.Show("Transaksi ini termasuk transaksi tunai. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                    Exit Sub
            ' ''                ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
            ' ''                    Dr.Close()
            ' ''                    CloseTrans()
            ' ''                    CloseConn()
            ' ''                    MessageBox.Show("Transaksi ini sudah di batalkan. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                    Exit Sub
            ' ''                ElseIf SisaHutang < Val(HilangkanTanda(LvJml)) Then
            ' ''                    Dr.Close()
            ' ''                    CloseTrans()
            ' ''                    CloseConn()
            ' ''                    MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                    Exit Sub
            ' ''                    '' '' '' '' ''ElseIf KodeCust <> LvKdCus Then
            ' ''                    '' '' '' '' ''    Dr.Close()
            ' ''                    '' '' '' '' ''    CloseTrans()
            ' ''                    '' '' '' '' ''    CloseConn()
            ' ''                    '' '' '' '' ''    MessageBox.Show("Supplier sudah diubah sebelumnya. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                    '' '' '' '' ''    Exit Sub
            ' ''                End If
            ' ''            Else
            ' ''                Dr.Close()
            ' ''                CloseTrans()
            ' ''                CloseConn()
            ' ''                MessageBox.Show("Nomor faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''                Exit Sub
            ' ''            End If
            ' ''        End Using

            ' ''        SQL = "select hutang from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''        SQL = SQL & "kode_supplier = '" & KodeCust & "'"
            ' ''        Using Dr = OpenTrans(SQL)
            ' ''            If Dr.Read Then
            ' ''                If Dr("hutang") - Val(HilangkanTanda(LvJml)) < 0 Then
            ' ''                    Dr.Close()
            ' ''                    CloseTrans()
            ' ''                    CloseConn()
            ' ''                    '' '' '' '' ''MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat hutang " & LvNmCus & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' ''                    Exit Sub
            ' ''                Else
            ' ''                    Dr.Close()
            ' ''                    'kurangin hutangnya
            ' ''                    SQL = "update suppliers set hutang = hutang - " & HilangkanTanda(LvJml) & " where "
            ' ''                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''                    SQL = SQL & "kode_supplier = '" & KodeCust & "'"
            ' ''                    ExecuteTrans(SQL)
            ' ''                End If
            ' ''            Else
            ' ''                Dr.Close()
            ' ''                CloseTrans()
            ' ''                CloseConn()
            ' ''                MessageBox.Show("Supplier tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' ''                Exit Sub
            ' ''            End If
            ' ''        End Using

            ' ''        If SisaHutang = Val(HilangkanTanda(LvJml)) Then
            ' ''            SQL = "Update pembelian set flag_lunas = 'Y', "
            ' ''            SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
            ' ''            SQL = SQL & "jam_lunas = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
            ' ''            SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''            SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
            ' ''            ExecuteTrans(SQL)
            ' ''        End If
            ' ''    Next

            ' ''    SQL = "update val_pemb set keterangan = '" & TextBoxket.Text.Trim & "', "
            ' ''    SQL = SQL & "cara_bayar = '" & arrCrByr.Item(ComboBoxCb.SelectedIndex) & "', "
            ' ''    SQL = SQL & "grand = " & HilangkanTanda(TextBoxtot1.Text) & " where "
            ' ''    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''    SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
            ' ''    ExecuteTrans(SQL)

            ' ''    For i As Integer = 0 To ListViewMT11.Items.Count - 1
            ' ''        Get_Isi_Listview(i)

            ' ''        SQL = "insert into detail_val_pemb(kode_perusahaan, no_val, no_faktur, byr) "
            ' ''        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
            ' ''        SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ")"
            ' ''        ExecuteTrans(SQL)
            ' ''    Next

            ' ''    Dim coa_hutang As String = ""

            ' ''    SQL = "select top(1) hutang from stock_owner_import where kode_perusahaan = '" & KodePerusahaan & "'"
            ' ''    Using dr = OpenTrans(SQL)
            ' ''        If dr.Read Then
            ' ''            coa_hutang = dr("hutang")
            ' ''        Else
            ' ''            dr.Close()
            ' ''            CloseTrans()
            ' ''            CloseConn()
            ' ''            MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ' ''            Exit Sub
            ' ''        End If
            ' ''    End Using

            ' ''    Dim pagenumber As Integer = 0

            ' ''    SQL = "delete from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            ' ''    SQL = SQL & "kode_voucher = '" & kode_voucher_lama & "'"
            ' ''    ExecuteTrans(SQL)

            ' ''    SQL = Get_Detail_Jurnal(kode_voucher_lama, Strings.Left(coa_hutang, 1), _
            ' ''                   Strings.Mid(coa_hutang, 2, 1), _
            ' ''                   Strings.Mid(Ganti(coa_hutang), 3), _
            ' ''                   KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim, HilangkanTanda(TextBoxtot1.Text), "0", pagenumber + 1)
            ' ''    ExecuteTrans(SQL)


            ' ''    SQL = Get_Detail_Jurnal(kode_voucher_lama, Strings.Left(ArrAkunCB1.Item(ComboBoxCb.SelectedIndex), 1), _
            ' ''                   Strings.Mid(ArrAkunCB1.Item(ComboBoxCb.SelectedIndex), 2, 1), _
            ' ''                   Strings.Mid(Ganti(ArrAkunCB1.Item(ComboBoxCb.SelectedIndex)), 3), _
            ' ''                   KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim, "0", HilangkanTanda(TextBoxtot1.Text), pagenumber + 1)
            ' ''    ExecuteTrans(SQL)
            ' ''MessageBox.Show("Data berhasil disimpan")
            ' ''Cmd.Transaction.Commit()

            ' ''CloseConn()
            ' ''Catch ex As Exception
            ' ''    CloseTrans()
            ' ''    CloseConn()
            ' ''    MessageBox.Show(ex.Message)
            ' ''    Exit Sub
            ' ''End Try
        End If

        Dim TanyaCetak As String = MessageBox.Show("Mau dicetak?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If TanyaCetak = vbYes Then
            cetak()
        End If


        Kosong()
        DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then TextBoxket.Focus()
    End Sub

    Private Sub DateTimePicker1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.Leave
        Try

            OpenConn()

            Get_No_Faktur()

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxket.KeyPress
        'If e.KeyChar = Chr(13) Then ComboBoxCb1.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBoxNP1.KeyPress
        If e.KeyChar = Chr(13) Then TextBoxbyr.Focus()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxbyr.KeyPress
        Dim hasilTotal As Double = 0

        If e.KeyChar = Chr(13) Then




            If TextBoxFktr.Text.Trim.Length = 0 Then
                MessageBox.Show("Silahkan pilih faktur terlebih dahulu")
                Exit Sub
            End If

            If (Val(TextBoxbyr.Text) > Val(HilangkanTanda(TextBoxjml.Text))) Then
                MessageBox.Show("Pembayaran tidak boleh melebihi sisa hutang") : Exit Sub
            End If


            If TextBoxjns.Text = "1" Then
                For i As Integer = 0 To ListViewMT11.Items.Count - 1
                    If ListViewMT11.Items(i).Text.Trim.ToUpper = TextBoxFktr.Text.Trim.ToUpper And ListViewMT11.Items(i).SubItems(2).Text.Trim.ToUpper = TextBoxKP.Text.Trim.ToUpper Then
                        MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If ListViewMT11.Items(i).SubItems(4).Text.Trim.ToUpper <> TextBoxMT.Text.Trim.ToUpper Then
                        MessageBox.Show("Mata Uang Tidak Boleh berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If ListViewMT11.Items(i).SubItems(10).Text.Trim.ToUpper <> TextBoxNodeclare.Text.Trim.ToUpper Then
                        MessageBox.Show("Jenis Declare berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Next
                TabPage1.Text = TextBoxMT.Text
                txt11.Text = TextBoxMT.Text
                txt12.Text = TextBoxMT.Text
                txt13.Text = TextBoxMT.Text
                txt14.Text = TextBoxMT.Text
                txt15.Text = TextBoxMT.Text
                txt16.Text = TextBoxMT.Text
                txt17.Text = TextBoxMT.Text
                txt18.Text = TextBoxMT.Text
                txt19.Text = TextBoxMT.Text

                Dim lv As New ListViewItem
                lv = ListViewMT11.Items.Add(TextBoxFktr.Text)
                lv.SubItems.Add(TextBoxtgl.Text)
                lv.SubItems.Add(TextBoxKP.Text)
                lv.SubItems.Add(TextBoxNP.Text)
                lv.SubItems.Add(TextBoxMT.Text)
                lv.SubItems.Add(Format(Val(TextBoxbyr.Text), "N2"))
                lv.SubItems.Add(Format(Val(TextBoxHPP.Text), "N5"))
                lv.SubItems.Add(TextBoxnopo.Text)
                lv.SubItems.Add(TextBoxlokasi.Text)
                lv.SubItems.Add(TextBoxdeclare.Text)
                lv.SubItems.Add(TextBoxNodeclare.Text)
                lv.SubItems.Add(TextBoxselisih.Text)
                lv.SubItems.Add(TextBoxrencana.Text)

                ComboBoxRek1.SelectedIndex = -1
            ElseIf TextBoxjns.Text = "2" Then
                For i As Integer = 0 To ListViewMT22.Items.Count - 1
                    If ListViewMT22.Items(i).Text.Trim.ToUpper = TextBoxFktr.Text.Trim.ToUpper And ListViewMT22.Items(i).SubItems(2).Text.Trim.ToUpper = TextBoxKP.Text.Trim.ToUpper Then
                        MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    If ListViewMT22.Items(i).SubItems(4).Text.Trim.ToUpper <> TextBoxMT.Text.Trim.ToUpper Then
                        MessageBox.Show("Mata Uang Tidak Boleh berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If ListViewMT22.Items(i).SubItems(10).Text.Trim.ToUpper <> TextBoxNodeclare.Text.Trim.ToUpper Then
                        MessageBox.Show("Jenis Declare berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Next

                TabPage2.Text = TextBoxMT.Text
                txt21.Text = TextBoxMT.Text
                txt22.Text = TextBoxMT.Text
                txt23.Text = TextBoxMT.Text
                txt24.Text = TextBoxMT.Text
                txt25.Text = TextBoxMT.Text
                txt26.Text = TextBoxMT.Text
                txt27.Text = TextBoxMT.Text
                txt28.Text = TextBoxMT.Text
                txt29.Text = TextBoxMT.Text

                Dim lv As New ListViewItem
                lv = ListViewMT22.Items.Add(TextBoxFktr.Text)
                lv.SubItems.Add(TextBoxtgl.Text)
                lv.SubItems.Add(TextBoxKP.Text)
                lv.SubItems.Add(TextBoxNP.Text)
                lv.SubItems.Add(TextBoxMT.Text)
                lv.SubItems.Add(Format(Val(TextBoxbyr.Text), "N2"))
                lv.SubItems.Add(Format(Val(TextBoxHPP.Text), "N5"))
                lv.SubItems.Add(TextBoxnopo.Text)
                lv.SubItems.Add(TextBoxlokasi.Text)
                lv.SubItems.Add(TextBoxdeclare.Text)
                lv.SubItems.Add(TextBoxNodeclare.Text)
                lv.SubItems.Add(TextBoxselisih.Text)
                lv.SubItems.Add(TextBoxrencana.Text)

                ComboBoxRek2.SelectedIndex = -1
            End If






            ' lv.SubItems.Add(TextBox10.Text)
            'lv.SubItems.Add(Format(Val(TextBox6.Text), "N2"))

            'If TextBox6.Text = 1 Then
            '    hasilTotal = Format(Val(TextBox12.Text), "N2") * 1
            'Else
            '    hasilTotal = Format(Val(TextBox12.Text), "N2") * Format(Val(TextBox6.Text), "N2")
            'End If

            'lv.SubItems.Add(Format(hasilTotal, "N2"))




            Hitung()
            Kosong_Bawah()

        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewMT11.DoubleClick
        'TextBox9.Text = ListView1.FocusedItem.Text
        'TextBox3.Text = ListView1.FocusedItem.SubItems(1).Text
        'TextBox11.Text = ListView1.FocusedItem.SubItems(2).Text
        'TextBox4.Text = ListView1.FocusedItem.SubItems(3).Text
        'TextBox7.Text = ListView1.FocusedItem.SubItems(4).Text

        'TextBox5.Text = ListView1.FocusedItem.SubItems(5).Text
        'TextBox6.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(8).Text)
        'TextBox10.Text = ListView1.FocusedItem.SubItems(7).Text
        'TextBox12.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(6).Text)


        ListViewMT11.FocusedItem.Remove()
        'TextBox6.Enabled = True
        'TextBox12.Enabled = False
        'TextBox6.Focus()
        Hitung()
    End Sub

    Private Sub TxtFaktur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFaktur.KeyPress
        If e.KeyChar = Chr(13) Then
            If DateTimePicker1.Enabled = True Then
                DateTimePicker1.Focus()
            Else
                TextBoxket.Focus()
            End If
        End If
    End Sub

    Private Sub TxtFaktur_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFaktur.Leave
        'Try

        '    If TxtFaktur.Text.Trim.Length = 0 Then
        '        OpenConn()

        '        Get_No_Faktur()

        '        CloseConn()
        '    End If

        '    OpenConn()

        '    Dim lv As New ListViewItem

        '    SQL = "select c.cara_bayar, a.no_faktur, a.tanggal, a.tgl_jatuh_tempo, c.keterangan, "
        '    SQL = SQL & "c.grand, c.no_val, a.kode_supplier, b.nama as namasupplier, d.byr from "
        '    SQL = SQL & "pembelian a, suppliers b, val_pemb c, detail_val_pemb d where "
        '    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And b.kode_perusahaan = c.kode_perusahaan And "
        '    SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan And a.kode_supplier = b.kode_supplier and "
        '    SQL = SQL & "c.no_val = d.no_val and a.no_faktur = d.no_faktur and "
        '    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and c.no_val = '" & TxtFaktur.Text.Trim & "' "
        '    SQL = SQL & "order by d.urut "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            DateTimePicker1.Enabled = False

        '            DateTimePicker1.Value = Dr("tanggal")
        '            TxtFaktur.Text = Dr("no_val")
        '            TextBoxket.Text = Dr("keterangan")
        '            TextBoxtot1.Text = Format(Dr(" "), "N0")
        '            For i As Integer = 0 To ComboBoxCb1.Items.Count - 1
        '                If Dr("cara_bayar") = arrCrByr1.Item(i) Then
        '                    ComboBoxCb1.SelectedIndex = i
        '                    Exit For
        '                End If
        '            Next

        '            ListViewMT1.Items.Clear() : ListViewMT11.Items.Clear()

        '            lv = ListViewMT11.Items.Add(Dr("no_faktur"))
        '            lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
        '            lv.SubItems.Add(Format(Dr("tgl_jatuh_tempo"), "dd MMM yyyy"))
        '            lv.SubItems.Add(Dr("kode_supplier"))
        '            lv.SubItems.Add(Dr("namasupplier"))
        '            lv.SubItems.Add(Format(Dr("byr"), "N0"))

        '            Do While Dr.Read
        '                lv = ListViewMT11.Items.Add(Dr("no_faktur"))
        '                lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
        '                lv.SubItems.Add(Format(Dr("tgl_jatuh_tempo"), "dd MMM yyyy"))
        '                lv.SubItems.Add(Dr("kode_supplier"))
        '                lv.SubItems.Add(Dr("namasupplier"))
        '                lv.SubItems.Add(Format(Dr("byr"), "N0"))
        '            Loop

        '            Button1.Text = "&Update"
        '        Else
        '            Kosong()
        '        End If

        '    End Using

        '    CloseConn()

        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim hasilTotal As Double = 0

        If e.KeyChar = Chr(13) Then

            If TextBoxjml.Text.Trim.Length = 0 Then
                TextBoxjml.Focus()
                MessageBox.Show("Kurs tidak boleh kosong!!") : Exit Sub
            End If


            'If TextBox10.Text = "RP" And TextBox6.Text > 1 Then
            '    MessageBox.Show("Nilai Kurs Rp harus 1") : Exit Sub
            'End If


            'If (TextBox6.Text.Trim.Length = 0 And TextBox9.Text.Trim.Length = 0) Then
            '    MessageBox.Show("Silahkan pilih faktur terlebih dahulu")
            '    TextBox6.Text = "" : Exit Sub
            'ElseIf TextBox9.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Silahkan pilih faktur terlebih dahulu")
            '    TextBox6.Text = "" : Exit Sub
            'End If

            'If (TextBox6.Text > Val(checkSisaHutang)) Then
            '    MessageBox.Show("Pembayaran tidak boleh melebihi sisa hutang") : Exit Sub
            'End If



            For i As Integer = 0 To ListViewMT11.Items.Count - 1
                If ListViewMT11.Items(i).Text.Trim.ToUpper = TextBoxFktr.Text.Trim.ToUpper And ListViewMT11.Items(i).SubItems(2).Text.Trim.ToUpper = TextBoxNP.Text.Trim.ToUpper Then
                    MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next



            Dim lv As New ListViewItem
            lv = ListViewMT11.Items.Add(TextBoxFktr.Text)
            lv.SubItems.Add(TextBoxtgl.Text)
            lv.SubItems.Add(TextBoxNP.Text)
            'lv.SubItems.Add(TextBox4.Text)

            'lv.SubItems.Add(TextBox7.Text)
            'lv.SubItems.Add(TextBox5.Text)
            lv.SubItems.Add(Format(Val(TextBoxjml.Text), "N2"))
            ' lv.SubItems.Add(TextBox10.Text)
            'lv.SubItems.Add(Format(Val(TextBox6.Text), "N2"))

            'If TextBox6.Text = 1 Then
            '    hasilTotal = Format(Val(TextBox12.Text), "N2") * 1
            'Else
            '    hasilTotal = Format(Val(TextBox12.Text), "N2") * Format(Val(TextBox6.Text), "N2")
            'End If

            lv.SubItems.Add(Format(hasilTotal, "N2"))




            Hitung()
            Kosong_Bawah()
            ListViewMT1.Focus()
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBoxCb1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then ComboBoxNP1.Focus()
    End Sub

    'Private Sub ComboBoxCb1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBoxCb1.KeyPress

    '    If e.KeyChar = Chr(13) Then cbxMataUang.Focus()

    'End Sub

    Private Sub TxtFaktur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFaktur.TextChanged

    End Sub



    'Private Sub cbxMataUang_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If cbxMataUang.SelectedIndex <> 0 Then
    '        TextBox8.Visible = True
    '        TextBox8.Focus()
    '    Else
    '        TextBox8.Visible = False
    '    End If

    'End Sub

    'Private Sub TextBox8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If e.KeyChar = Chr(13) Then

    '        'If TextBox8.Text.Trim.Length = 0 Then
    '        '    MessageBox.Show("Silahkan isi kurs terlebih dahulu") : Exit Sub
    '        'End If

    '        'TextBox8.Enabled = False
    '        'cbxMataUang.Enabled = False
    '        TextBox1.Enabled = True
    '        Button3.Enabled = True
    '        Button5.Enabled = True
    '        TextBox1.Focus()
    '        'varMataUang = cbxMataUang.Text
    '        'kursUangBaru = TextBox8.Text
    '    End If
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    'End Sub





    'Private Sub cbxMataUang_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    If cbxMataUang.SelectedIndex <> 0 Then
    '        TextBox8.Visible = True
    '        TextBox8.Focus()
    '    Else
    '        TextBox8.Visible = False
    '    End If

    'End Sub



    Private Sub TextBox12_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxjml.KeyPress
        'If e.KeyChar = Chr(13) Then
        '    If TextBox12.Text.Trim.Length = 0 Then
        '        MessageBox.Show("Silahkan isi kurs terlebih dahulu!!") : Exit Sub
        '    End If
        '    TextBox6.Focus()
        'End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    End Sub

    Private Sub ComboBoxCb1_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            TextBoxbyr.Focus()
        End If
    End Sub


    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub ButtonMT2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMT2.Click
        Cari("Tidak2")
    End Sub

    Private Sub ListViewMT1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewMT1.DoubleClick
        For i As Integer = 0 To ListViewMT11.Items.Count - 1

            If ListViewMT11.Items(i).Text.Trim.ToUpper = ListViewMT1.FocusedItem.Text.Trim.ToUpper And ListViewMT11.Items(i).SubItems(2).Text.Trim.ToUpper = ListViewMT1.FocusedItem.SubItems(4).Text.Trim.ToUpper Then
                MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If ListViewMT11.Items(i).SubItems(9).Text.Trim.ToUpper <> ListViewMT1.FocusedItem.SubItems(12).Text.Trim.ToUpper Then
                MessageBox.Show("Jenis Declare Tidak Boleh Berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If ListViewMT11.Items(i).SubItems(9).Text.Trim.ToUpper = "Y" Then
                If ListViewMT11.Items(i).SubItems(10).Text.Trim.ToUpper <> ListViewMT1.FocusedItem.SubItems(13).Text.Trim.ToUpper Then
                    MessageBox.Show("Pelunasan Harus dalam declare yang sama !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If
        Next

        If ListViewMT1.FocusedItem.SubItems(12).Text = "Y" Then

            If ListViewMT1.FocusedItem.SubItems(13).Text = "" Then
                MessageBox.Show("Blum Ada Data Declare . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Try
                OpenConn()

                SQL = "Select Flag_Lunas_Declare From "
                SQL = SQL & "Detail_Transaksi_Biaya_Import3_By_Perusahaan where kode_perusahaan = '" & KodePerusahaan & "' and "

                SQL = SQL & "No_faktur = '" & ListViewMT1.FocusedItem.Text & "' and Kode_Perusahaan_Biaya_Import ='" & HilangkanTanda(ListViewMT1.FocusedItem.SubItems(4).Text) & "'"
                SQL = SQL & "and Mata_Uang ='" & HilangkanTanda(ListViewMT1.FocusedItem.SubItems(6).Text) & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then

                        If General_Class.CekNULL(dr("Flag_Lunas_Declare")) <> "Y" Then
                            CloseConn()
                            MessageBox.Show("Declare ini Belum Selesai . .", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                    Else
                        CloseConn()
                        MessageBox.Show("Data tidak ditemukan . .", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End Using

                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
          
        End If

        If Val(HilangkanTanda(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(9).Text))) = 0 Then
            MessageBox.Show("Kurs HPP Tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If



        Dim Total_Hutang As Double = Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(7).Text)) - Val(HilangkanTanda(ListViewMT1.FocusedItem.SubItems(8).Text))


        TextBoxFktr.Text = ListViewMT1.FocusedItem.Text
        TextBoxtgl.Text = ListViewMT1.FocusedItem.SubItems(3).Text
    
        TextBoxKP.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(4).Text)
        TextBoxNP.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(5).Text)
        TextBoxMT.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(6).Text)
        TextBoxbyr.Text = Total_Hutang
        TextBoxjml.Text = Format(Total_Hutang, "N2")
        TextBoxjns.Text = "1"
        TextBoxHPP.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(9).Text)
        TextBoxnopo.Text = ListViewMT1.FocusedItem.SubItems(10).Text
        TextBoxlokasi.Text = ListViewMT1.FocusedItem.SubItems(11).Text
        TextBoxdeclare.Text = ListViewMT1.FocusedItem.SubItems(12).Text
        TextBoxNodeclare.Text = ListViewMT1.FocusedItem.SubItems(13).Text
        TextBoxselisih.Text = HilangkanTanda(ListViewMT1.FocusedItem.SubItems(14).Text)
        TextBoxrencana.Text = ListViewMT1.FocusedItem.SubItems(1).Text
    End Sub

    Private Sub ListViewMT2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewMT2.DoubleClick
        For i As Integer = 0 To ListViewMT22.Items.Count - 1

            If ListViewMT22.Items(i).Text.Trim.ToUpper = ListViewMT2.FocusedItem.Text.Trim.ToUpper And ListViewMT22.Items(i).SubItems(2).Text.Trim.ToUpper = ListViewMT2.FocusedItem.SubItems(4).Text.Trim.ToUpper Then
                MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            If ListViewMT22.Items(i).SubItems(9).Text.Trim.ToUpper <> ListViewMT2.FocusedItem.SubItems(12).Text.Trim.ToUpper Then
                MessageBox.Show("Jenis Declare Tidak Boleh Berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If ListViewMT22.Items(i).SubItems(9).Text.Trim.ToUpper = "Y" Then
                If ListViewMT22.Items(i).SubItems(10).Text.Trim.ToUpper <> ListViewMT2.FocusedItem.SubItems(13).Text.Trim.ToUpper Then
                    MessageBox.Show("Pelunasan Harus dalam declare yang sama !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If
        Next

        If ListViewMT2.FocusedItem.SubItems(12).Text = "Y" Then

            If ListViewMT2.FocusedItem.SubItems(13).Text = "" Then
                MessageBox.Show("Blum Ada Data Declare . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Try
                OpenConn()

                SQL = "Select Flag_Lunas_Declare From "
                SQL = SQL & "Detail_Transaksi_Biaya_Import3_By_Perusahaan where kode_perusahaan = '" & KodePerusahaan & "' and "

                SQL = SQL & "No_faktur = '" & ListViewMT2.FocusedItem.Text & "' and Kode_Perusahaan_Biaya_Import ='" & HilangkanTanda(ListViewMT2.FocusedItem.SubItems(4).Text) & "'"
                SQL = SQL & "and Mata_Uang ='" & HilangkanTanda(ListViewMT2.FocusedItem.SubItems(6).Text) & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then

                        If General_Class.CekNULL(dr("Flag_Lunas_Declare")) <> "Y" Then
                            CloseConn()
                            MessageBox.Show("Declare ini Belum Selesai . .", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                    Else
                        CloseConn()
                        MessageBox.Show("Data tidak ditemukan . .", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End Using

                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        End If

        If Val(HilangkanTanda(HilangkanTanda(ListViewMT2.FocusedItem.SubItems(9).Text))) = 0 Then
            MessageBox.Show("Kurs HPP Tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim Total_Hutang As Double = Val(HilangkanTanda(ListViewMT2.FocusedItem.SubItems(7).Text)) - Val(HilangkanTanda(ListViewMT2.FocusedItem.SubItems(8).Text))


        TextBoxFktr.Text = ListViewMT2.FocusedItem.Text
        TextBoxtgl.Text = ListViewMT2.FocusedItem.SubItems(3).Text

        TextBoxKP.Text = HilangkanTanda(ListViewMT2.FocusedItem.SubItems(4).Text)
        TextBoxNP.Text = HilangkanTanda(ListViewMT2.FocusedItem.SubItems(5).Text)
        TextBoxMT.Text = HilangkanTanda(ListViewMT2.FocusedItem.SubItems(6).Text)
        TextBoxbyr.Text = Total_Hutang
        TextBoxjml.Text = Format(Total_Hutang, "N2")
        TextBoxjns.Text = "2"
        TextBoxHPP.Text = HilangkanTanda(ListViewMT2.FocusedItem.SubItems(9).Text)
        TextBoxnopo.Text = ListViewMT2.FocusedItem.SubItems(10).Text
        TextBoxlokasi.Text = ListViewMT2.FocusedItem.SubItems(11).Text
        TextBoxdeclare.Text = ListViewMT2.FocusedItem.SubItems(12).Text
        TextBoxNodeclare.Text = ListViewMT2.FocusedItem.SubItems(13).Text
        TextBoxselisih.Text = HilangkanTanda(ListViewMT2.FocusedItem.SubItems(14).Text)
        TextBoxrencana.Text = ListViewMT2.FocusedItem.SubItems(1).Text
    End Sub

    
    Private Sub TextBoxDBY1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxDBY1.TextChanged
        Hitung()
    End Sub

    Private Sub TextBoxKV1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxKV1.TextChanged
        Hitung()
    End Sub

    Private Sub TextBoxADM1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxADM1.TextChanged
        Hitung()
    End Sub

    Private Sub TextBoxKurs1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxKurs1.TextChanged
        Hitung()
    End Sub

    Private Sub TextBoxDBY2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxDBY2.TextChanged
        Hitung()
    End Sub

    Private Sub TextBoxADM2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxADM2.TextChanged
        Hitung()
    End Sub

    Private Sub TextBoxKurs2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxKurs2.TextChanged
        Hitung()
    End Sub

    Private Sub ListViewMT22_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewMT22.DoubleClick
        ListViewMT22.FocusedItem.Remove()
        'TextBox6.Enabled = True
        'TextBox12.Enabled = False
        'TextBox6.Focus()
        Hitung()
    End Sub

    Private Sub TextBoxbyr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxbyr.TextChanged

    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged

        If CheckBox2.Checked = True Then
            TextBoxPindahKurs.Enabled = True
            TextBoxKV1.Enabled = True
        Else
            TextBoxPindahKurs.Enabled = False
            TextBoxKV1.Enabled = False
            TextBoxPindahKurs.Text = 0
            TextBoxKV1.Text = 0
        End If

    End Sub

    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub Label19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label19.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBoxsimpan1.Enabled = True

        Else
            TextBoxsimpan1.Enabled = False
            TextBoxsimpan1.Text = 0
        End If
    End Sub

    Private Sub Label22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label22.Click

    End Sub

    Private Sub TextBoxDPT1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxDPT1.DoubleClick



        If ListViewMT11.Items.Count = 0 Then
            MessageBox.Show("Tidak ada data!.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ListViewMT11.Focus()
            Exit Sub
        ElseIf cmbPenerima.SelectedIndex = -1 Or ComboBoxRek1.SelectedIndex = -1 Then
            MessageBox.Show("Rekening harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxBank1.Focus()
            Exit Sub
        End If

        Dim checkMataUAng = arrMUA.Item(ComboBoxRek1.SelectedIndex)

        If TabPage1.Text <> checkMataUAng Then
            MessageBox.Show("Mata Uang Rekening tidak boleh berbeda")
            Exit Sub
        End If

        'Display_Deposit.Par_Kode_Supplier = arrSupp.Item(ComboBoxRek1.SelectedIndex)
        'Display_Deposit.TextBox3.Text = arrRek.Item(ComboBoxRek1.SelectedIndex)
        'Display_Deposit.dari_mana = arrMUA.Item(ComboBoxRek1.SelectedIndex)
        'Display_Deposit.MT = "1"
        'Display_Deposit.ShowDialog()
    End Sub

    Private Sub TextBoxDPT1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxDPT1.TextChanged
        Hitung()
    End Sub

    Private Sub TextBoxDPT2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxDPT2.DoubleClick

       

        If ListViewMT22.Items.Count = 0 Then
            MessageBox.Show("Tidak ada data!.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ListViewMT22.Focus()
            Exit Sub
        ElseIf cmbPenerima2.SelectedIndex = -1 Or ComboBoxRek2.SelectedIndex = -1 Then
            MessageBox.Show("Rekening harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxBank2.Focus()
            Exit Sub
        End If

        Dim checkMataUang2 = arrMUA2.Item(ComboBoxRek2.SelectedIndex)

        If TabPage2.Text <> checkMataUang2 Then
            MessageBox.Show("Mata Uang Rekening tidak boleh berbeda2")
            Exit Sub
        End If

        'Display_Deposit.Par_Kode_Supplier = arrSupp2.Item(ComboBoxRek2.SelectedIndex)
        'Display_Deposit.TextBox3.Text = arrRek2.Item(ComboBoxRek2.SelectedIndex)
        'Display_Deposit.dari_mana = arrMUA2.Item(ComboBoxRek2.SelectedIndex)
        'Display_Deposit.MT = "2"
        'Display_Deposit.ShowDialog()
    End Sub

 

    Private Sub TextBoxPindahKurs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxPindahKurs.TextChanged
        Hitung()
    End Sub

    Private Sub TextBoxsimpan1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxsimpan1.TextChanged
        Hitung()
    End Sub

    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label21.Click

    End Sub

    Private Sub txt18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt18.Click

    End Sub

    Private Sub txt15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt15.Click

    End Sub

    Private Sub TextBoxDPT2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxDPT2.TextChanged
        Hitung()
    End Sub

    Private Sub txt11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt11.Click

    End Sub

    Private Sub ComboBoxBank1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxBank1.SelectedIndexChanged
        If ComboBoxBank1.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()
            ComboBoxRek1.Enabled = True
            ComboBoxRek1.Items.Clear()
            arrSupp.Clear() : arrRek.Clear() : arrMUA.Clear() : ArrRencana.Clear() : arrakunrek1.Clear()


            SQL = "Select No_Rek,mata_uang, Kode_Akun From "
            SQL = SQL & "Rekening where kode_perusahaan = '" & KodePerusahaan & "' and "

            SQL = SQL & "Kode_Bank = '" & ComboBoxBank1.Text & "' "

            SQL = SQL & "order by No_Rek DESC"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBoxRek1.Items.Add("----" & " - " & dr("No_Rek") & " - " & dr("mata_uang"))
                    arrSupp.Add("----")
                    arrRek.Add(dr("No_Rek"))
                    arrMUA.Add(dr("mata_uang"))
                    arrakunrek1.Add(dr("Kode_Akun"))
                    ArrRencana.Add("000")
                Loop
            End Using


            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBoxBank2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxBank2.SelectedIndexChanged
        If ComboBoxBank2.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            ComboBoxRek2.Enabled = True
            ComboBoxRek2.Items.Clear()
            arrSupp2.Clear() : arrRek2.Clear() : arrMUA2.Clear() : ArrRencana2.Clear() : arrakunrek2.Clear()


            SQL = "Select No_Rek,mata_uang, Kode_Akun From "
            SQL = SQL & "Rekening where kode_perusahaan = '" & KodePerusahaan & "' and "

            SQL = SQL & "Kode_Bank = '" & ComboBoxBank2.Text & "' "

            SQL = SQL & "order by No_Rek DESC"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBoxRek2.Items.Add("----" & " - " & dr("No_Rek") & " - " & dr("mata_uang"))
                    arrSupp2.Add("----")
                    arrRek2.Add(dr("No_Rek"))
                    arrMUA2.Add(dr("mata_uang"))
                    arrakunrek2.Add(dr("Kode_Akun"))
                    ArrRencana2.Add("000")
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub cmbPenerima_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPenerima.SelectedIndexChanged


        If cmbPenerima.SelectedIndex = -1 Then
            Exit Sub
        End If

        If ListViewMT11.Items.Count = 0 Then
            MessageBox.Show("Belum ada data . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbPenerima.SelectedIndex = -1
            Exit Sub
        End If


        Try
            OpenConn()

            ComboBoxRek1.Items.Clear()

            arrSupp.Clear() : arrRek.Clear() : arrMUA.Clear()

            If cmbPenerima.SelectedIndex = 0 Then 'supplier
                ComboBoxBank1.Enabled = False
                ComboBoxRek1.Enabled = True
                ComboBoxBank1.Items.Clear()
                SQL = "Select a.kode_supplier as kode_sup, a.no_rekening, a.mata_uang, b.nama From rekening_suppliers a, suppliers b "
                SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.kode_supplier = b.kode_supplier and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "'  and a.aktif = 'Y' "
                SQL = SQL & "order by nama, mata_uang "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        ComboBoxRek1.Items.Add(dr("nama") & " - " & dr("no_rekening") & " - " & dr("mata_uang"))
                        arrSupp.Add(dr("kode_sup"))
                        arrRek.Add(dr("no_rekening"))
                        arrMUA.Add(dr("mata_uang"))
                    Loop
                End Using

            ElseIf cmbPenerima.SelectedIndex = 1 Then 'agent
                ComboBoxBank1.Enabled = False
                ComboBoxRek1.Enabled = True
                ComboBoxBank1.Items.Clear()
                SQL = "Select kode_perusahaan_biaya_import as kode_sup, no_rekening, mata_uang, kode_perusahaan_biaya_import as nama From rekening_perusahaan_biaya_import "
                SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "order by nama, mata_uang "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        ComboBoxRek1.Items.Add(dr("nama") & " - " & dr("no_rekening") & " - " & dr("mata_uang"))
                        arrSupp.Add(dr("kode_sup"))
                        arrRek.Add(dr("no_rekening"))
                        arrMUA.Add(dr("mata_uang"))
                    Loop
                End Using

            Else
                ComboBoxBank1.Enabled = True
                ComboBoxBank1.Items.Clear()
                ComboBoxRek1.Enabled = False
                SQL = "Select Kode_Bank From "
                SQL = SQL & "Bank where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "order by Kode_Bank"
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        ComboBoxBank1.Items.Add(dr("Kode_Bank"))
                    Loop
                End Using
            End If
            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub cmbPenerima2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPenerima2.SelectedIndexChanged
        If cmbPenerima2.SelectedIndex = -1 Then
            Exit Sub
        End If

        If ListViewMT22.Items.Count = 0 Then
            MessageBox.Show("Belum ada data . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbPenerima2.SelectedIndex = -1
            Exit Sub
        End If

        Try
            OpenConn()

            ComboBoxRek2.Items.Clear()

            arrSupp2.Clear() : arrRek2.Clear() : arrMUA2.Clear()

            If cmbPenerima2.SelectedIndex = 0 Then 'supplier
                ComboBoxBank2.Enabled = False
                ComboBoxRek2.Enabled = True
                ComboBoxBank2.Items.Clear()
                SQL = "Select a.kode_supplier as kode_sup, a.no_rekening, a.mata_uang, b.nama From rekening_suppliers a, suppliers b "
                SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.kode_supplier = b.kode_supplier and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "'  and a.aktif = 'Y' "
                SQL = SQL & "order by nama, mata_uang "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        ComboBoxRek2.Items.Add(dr("nama") & " - " & dr("no_rekening") & " - " & dr("mata_uang"))
                        arrSupp2.Add(dr("kode_sup"))
                        arrRek2.Add(dr("no_rekening"))
                        arrMUA2.Add(dr("mata_uang"))
                    Loop
                End Using

            ElseIf cmbPenerima2.SelectedIndex = 1 Then 'agent
                ComboBoxBank2.Enabled = False
                ComboBoxRek2.Enabled = True
                ComboBoxBank2.Items.Clear()
                SQL = "Select kode_perusahaan_biaya_import as kode_sup, no_rekening, mata_uang, kode_perusahaan_biaya_import as nama From rekening_perusahaan_biaya_import "
                SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "order by nama, mata_uang "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        ComboBoxRek2.Items.Add(dr("nama") & " - " & dr("no_rekening") & " - " & dr("mata_uang"))
                        arrSupp2.Add(dr("kode_sup"))
                        arrRek2.Add(dr("no_rekening"))
                        arrMUA2.Add(dr("mata_uang"))
                    Loop
                End Using

            Else
                ComboBoxBank2.Enabled = True
                ComboBoxBank2.Items.Clear()
                ComboBoxRek2.Enabled = False
                SQL = "Select Kode_Bank From "
                SQL = SQL & "Bank where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "order by Kode_Bank"
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        ComboBoxBank2.Items.Add(dr("Kode_Bank"))
                    Loop
                End Using
            End If
            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

   
    Private Sub ComboBoxRek1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxRek1.SelectedIndexChanged

        If ComboBoxRek1.SelectedIndex = -1 Then
            Exit Sub
        End If


        If TabPage1.Text <> arrMUA.Item(ComboBoxRek1.SelectedIndex) Then
            MessageBox.Show("Mata Uang Rekening Berbeda . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ComboBoxRek1.SelectedIndex = -1
            Exit Sub
        End If


        TextBoxDPT1.Text = 0
        Hitung()

        If ListViewMT11.Items.Count = 0 Then
            MessageBox.Show("Pilih data terlebih dahulu . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ComboBoxRek1.SelectedIndex = -1
            Exit Sub
        End If

        Get_Isi_Listview(0)

        If Lvdec = "Y" Then
            ambil_kurs_declare()
        Else
 
            If cmbPenerima.Text = "SUPPLIER" Then
                ambil_kurs_Sup1()
            ElseIf cmbPenerima.Text = "BANK" Then
                ambil_kurs1()
            Else
                MessageBox.Show("Penerima ini Untuk Jenis Declare . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ComboBoxRek1.SelectedIndex = -1
                Exit Sub
            End If

        End If


    End Sub

   
    Private Sub ComboBoxRek2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxRek2.SelectedIndexChanged

        If ComboBoxRek2.SelectedIndex = -1 Then
            Exit Sub
        End If


        If TabPage2.Text <> arrMUA2.Item(ComboBoxRek2.SelectedIndex) Then
            MessageBox.Show("Mata Uang Rekening Berbeda . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ComboBoxRek2.SelectedIndex = -1
            Exit Sub
        End If


        TextBoxDPT2.Text = 0
        Hitung()

        If ListViewMT22.Items.Count = 0 Then
            MessageBox.Show("Pilih data terlebih dahulu . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ComboBoxRek2.SelectedIndex = -1
            Exit Sub
        End If

        Get_Isi_Listview2(0)

        If Lvdec2 = "Y" Then
            ambil_kurs_declare2()
        Else

            If cmbPenerima2.Text = "SUPPLIER" Then
                ambil_kurs_Sup2()
            ElseIf cmbPenerima2.Text = "BANK" Then
                ambil_kurs2()
            Else
                MessageBox.Show("Penerima ini Untuk Jenis Declare . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ComboBoxRek2.SelectedIndex = -1
                Exit Sub
            End If

        End If
    End Sub

    Private Sub ListViewMT1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListViewMT1.SelectedIndexChanged

    End Sub

    Private Sub TextBoxselisih_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxselisih.KeyPress

    End Sub

    Private Sub TextBoxselisih_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxselisih.TextChanged

    End Sub
End Class