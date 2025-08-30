Public Class Transaksi_Biaya_Lokal_Barang_Lain

    Dim arrMaster As New ArrayList

    Dim hitung As Integer

    Dim itemLv_Lokasi As Integer = 0
    Dim itemLv_KdMaster As Integer = 1
    Dim itemLv_KdKategori As Integer = 2
    Dim itemLv_KdBiaya As Integer = 3
    Dim itemLv_NmBiaya As Integer = 4
    Dim itemLv_Kontainer As Integer = 5
    Dim itemLv_KdPerusahanBiayaImport As Integer = 6
    Dim itemLv_NmPerusahaanBiayaImport As Integer = 7
    Dim itemLv_KdGudang As Integer = 8
    Dim itemLv_TblAsal As Integer = 9

    Dim itemDGV1_NoPO As Integer = 0
    Dim itemDGV1_Lokasi As Integer = 1
    Dim itemDGV1_KdKategori As Integer = 2
    Dim itemDGV1_KdBiaya As Integer = 3
    Dim itemDGV1_NmBiaya As Integer = 4
    Dim itemDGV1_Kontainer As Integer = 5
    Dim itemDGV1_JmlhPO As Integer = 6
    Dim itemDGV1_KdPerusahaanBiayaImport As Integer = 7
    Dim itemDGV1_NmPerusahaanBiayaImport As Integer = 8

    Dim LvID As String
    Dim Lvlokasi As String
    Dim LvKodeKategori As String
    Dim LvKodeBiaya As String
    Dim LvNamaBiaya As String
    Dim LvKontainer As String
    Dim LvJumlahPo As String
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
    Dim LvFlagAvg As String

    Dim LvKategori2 As String
    Dim LvTotal2 As String

    Dim LvPerusahaan3 As String
    Dim LvMataUang3 As String
    Dim LvTotal3 As String

    Dim cellID As Integer
    Dim celllokasi As Integer
    Dim cellKodeKategori As Integer
    Dim cellKodeBiaya As Integer
    Dim cellNamaBiaya As Integer
    Dim cellKontainer As Integer
    Dim cellJumlahPo As Integer
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
    Dim cellFlagAvg As Integer


    Dim cellKategori2 As Integer
    Dim cellTotal2 As Integer

    Dim cellPerusahaan3 As Integer
    Dim cellMataUang3 As Integer
    Dim cellTotal3 As Integer



    Private Sub Transaksi_Biaya_Lokal_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub Transaksi_Biaya_Lokal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


    End Sub

    Public Sub Kosong()

        get_jam()

        arrMaster.Clear()

        DataGridView1.Rows.Clear()
        TxtNo_PO.Text = ""
        Txt_Keterangan.Text = ""
        Txt_Kd_Supplier.Text = "" : Txt_NmSupllier.Text = ""
        Txt_JumlahPO.Text = ""
        Txt_Berat.Text = ""
        Txt_TglPO.Text = ""
        Txt_User.Text = ""

        ListView2.Items.Clear() : ListView2.Columns.Clear()
        ListView2.Columns.Add("MUA", 50, HorizontalAlignment.Center)
        ListView2.Columns.Add("Nilai Kurs", 70, HorizontalAlignment.Left)
        ListView2.Columns.Add("Jenis", 70, HorizontalAlignment.Right).DisplayIndex = 1

        Dim Lvw As ListViewItem
        Lvw = ListView2.Items.Add("RP")
        Lvw.SubItems.Add(1)
        Lvw.SubItems.Add("BIAYA")
        Lvw = ListView2.Items.Add("RP")
        Lvw.SubItems.Add(1)
        Lvw.SubItems.Add("ASURANSI")

        ListView1.Columns.Clear() : ListView1.Items.Clear()
        ListView1.Columns.Add("Lokasi", 140, HorizontalAlignment.Left) '0
        ListView1.Columns.Add("Kode Master", 130, HorizontalAlignment.Center) '1
        ListView1.Columns.Add("Kode Kategori", 130, HorizontalAlignment.Center) '2
        ListView1.Columns.Add("Kode Biaya", 0, HorizontalAlignment.Center) '3
        ListView1.Columns.Add("Nama Biaya", 130, HorizontalAlignment.Left) '4
        ListView1.Columns.Add("Kontainer", 130, HorizontalAlignment.Center) '5
        ListView1.Columns.Add("Kode Perusahaan Biaya Import", 0, HorizontalAlignment.Center) '6
        ListView1.Columns.Add("Nama Perusahaan", 130, HorizontalAlignment.Left) '7
        ListView1.Columns.Add("Kode Gudang", 130, HorizontalAlignment.Center) '8
        ListView1.Columns.Add("Tabel asal", 0, HorizontalAlignment.Left) '9
        ListView1.View = View.Details

        Try
            OpenConn()
            Get_No_Faktur()


            Cmb_Lokasi.Items.Clear()
            SQL = "Select Kode_stock_owner From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
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
            ComboBox2.SelectedIndex = 0

            ComboBox3.Items.Clear()
            ComboBox3.Items.Add("-- Seluruh --")
            SQL = "select Kode_Kategori_Biaya_import from Kategori_biaya_import where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Kategori_Biaya_import"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox3.Items.Add(dr("Kode_Kategori_Biaya_import"))
                Loop
            End Using
            ComboBox3.SelectedIndex = 0

            ComboBox4.Items.Clear()
            ComboBox4.Items.Add("-- Seluruh --")
            SQL = "Select Nama From Perusahaan_Biaya_Import where kode_perusahaan = '" & KodePerusahaan & "' order by Nama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("Nama"))
                Loop
            End Using
            ComboBox4.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Button2_Click(Button2, Nothing)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Combo Master Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf ComboBox3.SelectedIndex = -1 Then
            MessageBox.Show("Combo Kategori Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus() : Exit Sub

        ElseIf ComboBox4.SelectedIndex = -1 Then
            MessageBox.Show("Combo Perusahaan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = ";with cte_X as( "
            SQL = SQL & "SELECT a.Kode_Perusahaan, a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            SQL = SQL & "a.Kode_Biaya, b.Nama as Nama_Biaya, a.Kode_Kontainer, a.Kode_Perusahaan_Biaya_Import, e.Nama, '' as Kode_Gudang, 'Biaya_Import_Detail' as tabel_asal  "
            SQL = SQL & "from Biaya_Import_Detail a, biaya_import b, Kategori_Biaya_Import c, master_Kategori_biaya_import d, perusahaan_biaya_import e where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Biaya = b.Kode_Biaya and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and a.perhitungan <> 'G' and a.Kode_Stock_Owner in('" & Cmb_Lokasi.Text & "') and a.flag_lokal='Y' "

            'SQL = SQL & "union all "

            'SQL = SQL & "SELECT a.Kode_Perusahaan, a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, a.Kode_Biaya, b.Nama AS Nama_Biaya, a.Kode_Kontainer,  "
            'SQL = SQL & "a.Kode_Perusahaan_Biaya_Import, e.Nama, Kode_Gudang, 'Biaya_Import_Detail2' AS tabel_asal  "
            'SQL = SQL & "from Biaya_Import_Detail2 a, biaya_import b, Kategori_Biaya_Import c, master_Kategori_biaya_import d, perusahaan_biaya_import e where "
            'SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Biaya = b.Kode_Biaya and "
            'SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import "
            'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_perusahaan and c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            'SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import "
            'SQL = SQL & "and a.perhitungan <> 'G' and a.Kode_Stock_Owner in('" & Cmb_Lokasi.Text & "')  "

            'SQL = SQL & "union all "

            'SQL = SQL & "SELECT a.Kode_Perusahaan, a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            'SQL = SQL & "a.Kode_Biaya, b.Nama AS Nama_Biaya, a.Kode_Kontainer, "
            'SQL = SQL & "a.Kode_Perusahaan_Biaya_Import, e.Nama, '' AS Kode_Gudang, "
            'SQL = SQL & "'Biaya_Import_Detail' AS tabel_asal "
            'SQL = SQL & "FROM Biaya_Import_Detail a, biaya_import b, Kategori_Biaya_Import c, "
            'SQL = SQL & "master_Kategori_biaya_import d, perusahaan_biaya_import e "
            'SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "AND a.Kode_Biaya = b.Kode_Biaya "
            'SQL = SQL & "AND b.Kode_Perusahaan = c.Kode_Perusahaan "
            'SQL = SQL & "AND b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import "
            'SQL = SQL & "AND c.Kode_Perusahaan = d.Kode_perusahaan "
            'SQL = SQL & "AND c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            'SQL = SQL & "AND a.Kode_Perusahaan = e.Kode_Perusahaan "
            'SQL = SQL & "AND a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import "
            'SQL = SQL & "AND a.perhitungan = 'G' "
            'SQL = SQL & "AND a.Kode_Stock_Owner IN ('" & Cmb_Lokasi.Text & "') "
            'SQL = SQL & "GROUP BY a.perhitungan, a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, "
            'SQL = SQL & "c.Kode_Kategori_Biaya_Import, a.Kode_Biaya, b.Nama, a.Kode_Kontainer, "
            'SQL = SQL & "a.Kode_Perusahaan_Biaya_Import, e.Nama, a.Kode_Perusahaan "

            'SQL = SQL & "union all "

            'SQL = SQL & "SELECT a.Kode_Perusahaan, a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, c.Kode_Kategori_Biaya_Import, "
            'SQL = SQL & "a.Kode_Biaya, b.Nama AS Nama_Biaya, a.Kode_Kontainer, a.Kode_Perusahaan_Biaya_Import, "
            'SQL = SQL & "e.Nama, a.Kode_Gudang, 'Biaya_Import_Detail2' AS tabel_asal "
            'SQL = SQL & "FROM Biaya_Import_Detail2 a, biaya_import b, Kategori_Biaya_Import c, "
            'SQL = SQL & "master_Kategori_biaya_import d, perusahaan_biaya_import e "
            'SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "AND a.Kode_Biaya = b.Kode_Biaya "
            'SQL = SQL & "AND b.Kode_Perusahaan = c.Kode_Perusahaan  "
            'SQL = SQL & "AND b.Kode_kategori_biaya_import = c.Kode_Kategori_Biaya_Import "
            'SQL = SQL & "AND c.Kode_Perusahaan = d.Kode_perusahaan "
            'SQL = SQL & "AND c.Kode_Master_Kategori_Biaya_Import = d.Kode_Master_Kategori_Biaya_Import "
            'SQL = SQL & "AND a.Kode_Perusahaan = e.Kode_Perusahaan "
            'SQL = SQL & "AND a.Kode_Perusahaan_Biaya_Import = e.Kode_Perusahaan_Biaya_Import "
            'SQL = SQL & "AND a.perhitungan = 'G' "
            'SQL = SQL & "AND a.Kode_Stock_Owner IN ('" & Cmb_Lokasi.Text & "') "
            'SQL = SQL & "GROUP BY a.perhitungan, a.kode_stock_owner, d.Kode_Master_Kategori_Biaya_import, "
            'SQL = SQL & "c.Kode_Kategori_Biaya_Import, a.Kode_Biaya, b.Nama, a.Kode_Kontainer, "
            'SQL = SQL & "a.Kode_Perusahaan_Biaya_Import, e.Nama, a.Kode_Gudang, a.Kode_Perusahaan "

            SQL = SQL & ") "
            SQL = SQL & "select* from cte_x where Kode_Perusahaan = '" & KodePerusahaan & "' "

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

    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick

        For index As Integer = 0 To DataGridView1.Rows.Count - 1

            If DataGridView1.Rows.Item(index).Cells(1).Value = ListView1.FocusedItem.Text And
                DataGridView1.Rows.Item(index).Cells(2).Value = ListView1.FocusedItem.SubItems(2).Text And
                DataGridView1.Rows.Item(index).Cells(3).Value = ListView1.FocusedItem.SubItems(3).Text And
                DataGridView1.Rows.Item(index).Cells(7).Value = ListView1.FocusedItem.SubItems(6).Text Then

                MessageBox.Show("Data Sudah Ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next


        Try
            OpenConn()

            DataGridView1.Rows.Add(1)
            Dim index As Integer = DataGridView1.Rows.Count - 1

            DataGridView1.Rows.Item(index).Cells(itemDGV1_NoPO).Value = TxtNo_PO.Text
            DataGridView1.Rows.Item(index).Cells(itemDGV1_Lokasi).Value = ListView1.FocusedItem.Text
            DataGridView1.Rows.Item(index).Cells(itemDGV1_KdKategori).Value = ListView1.FocusedItem.SubItems(itemLv_KdKategori).Text
            DataGridView1.Rows.Item(index).Cells(itemDGV1_KdBiaya).Value = ListView1.FocusedItem.SubItems(itemLv_KdBiaya).Text
            DataGridView1.Rows.Item(index).Cells(itemDGV1_NmBiaya).Value = ListView1.FocusedItem.SubItems(itemLv_NmBiaya).Text
            DataGridView1.Rows.Item(index).Cells(itemDGV1_Kontainer).Value = ListView1.FocusedItem.SubItems(itemLv_Kontainer).Text
            DataGridView1.Rows.Item(index).Cells(itemDGV1_JmlhPO).Value = TxtJumlahMobil.Text

            DataGridView1.Rows.Item(index).Cells(itemDGV1_KdPerusahaanBiayaImport).Value = ListView1.FocusedItem.SubItems(itemLv_KdPerusahanBiayaImport).Text
            DataGridView1.Rows.Item(index).Cells(itemDGV1_NmPerusahaanBiayaImport).Value = ListView1.FocusedItem.SubItems(itemLv_NmPerusahaanBiayaImport).Text

            Dim kurs As Double = 0

            Dim hitungMUA As Double = 0
            Dim Untuk_gudang As String = ""

            If ListView1.FocusedItem.SubItems(9).Text.Trim.ToUpper = "BIAYA_IMPORT_DETAIL2" Then
                Untuk_gudang = "and Y.Kode_Gudang=A.Kode_Gudang "
            End If

            SQL = "select a.*, 0 as Selisih_Tanggal, 0 as konte_per_jenis, 0 as konte_per_lokasi, 0 as konte_per_Gudang, 0 as Konte_Penjaluran, 0 as Konte_Asuransi, d.flag_average, "
            SQL = SQL & "'" & Txt_JumlahPO.Text & "' as Total_Declare "

            SQL = SQL & "from " & ListView1.FocusedItem.SubItems(9).Text & " a, Biaya_Import c, kategori_biaya_import d "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and a.kode_biaya = c.kode_biaya and c.kode_kategori_biaya_import = d.kode_kategori_biaya_import "
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
                        hitung = kurs * Dr("Nilai") * Val(TxtJumlahMobil.Text)
                        hitungMUA = Dr("Nilai") * Val(TxtJumlahMobil.Text)
                    ElseIf Dr("Perhitungan") = "B" Then
                        hitung = kurs * Dr("Nilai")
                        hitungMUA = Dr("Nilai")
                    ElseIf Dr("Perhitungan") = "C" Then
                        hitung = (Val(Txt_Berat.Text) * (kurs * Dr("Nilai"))) + Dr("Nilai_2")
                        hitungMUA = (Val(Txt_Berat.Text) * (Dr("Nilai"))) + Dr("Nilai_2")
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
                        If Val(TxtJumlahMobil.Text) < Dr("Min") Then
                            MessageBox.Show("Jumlah Container kurang dari jumlah minimal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DataGridView1.Rows.Remove(DataGridView1.Rows.Item(index))
                            Dr.Close()
                            CloseConn()
                            Exit Sub
                        ElseIf Val(TxtJumlahMobil.Text) < Dr("Max") Then
                            jml_konte = Val(TxtJumlahMobil.Text) - (Dr("Min") - 1)
                        ElseIf Val(TxtJumlahMobil.Text) >= Dr("Max") Then
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

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Harus Di isi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus()
            Exit Sub
        ElseIf DataGridView1.RowCount = 0 Then
            MessageBox.Show("Data Tidak Ada . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DataGridView1.Focus()
            Exit Sub
        End If

        GetTime()
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            Get_No_Faktur()

            '========================================
            '=     CEK APAKAH PO SUDAH BERJALAN     =
            '========================================
            SQL = "select top 1 Kode_Perusahaan from emi_pembelian_loading_detail_barang_lain where Kode_Perusahaan = '" & KodePerusahaan & "' and No_PO = '" & TxtNo_PO.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("PO Belum Berjalan", "Display Biaya Lokal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim kd_sup As String = ""
            Dim flag_average As String = ""

            SQL = "select Flag_Average, Kode_Supplier from Suppliers where Kode_Perusahaan='" & KodePerusahaan & "' and  Kode_Supplier = '" & Txt_Kd_Supplier.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    kd_sup = Dr("Kode_Supplier")
                    flag_average = If(General_Class.CekNULL(Dr("flag_average")) = "", "NULL", $"'{Dr("flag_average")}'")

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Supplier tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using

            Dim inisial_Faktur As String = ""
            SQL = "select top(1) Inisial_faktur "
            SQL = SQL & " from Stock_Owner where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_stock_Owner = '" & Cmb_Lokasi.Text & "'"
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


            SQL = "insert into Transaksi_Biaya_Lokal_Barang_Lain(kode_perusahaan, no_faktur, tanggal, jam, UserID, no_po, "
            SQL = SQL & "keterangan, jml_po, Total_Berat, Grand_Total, Flag_Average ) "
            SQL = SQL & "values('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "',"
            SQL = SQL & "'" & UserID & "', '" & TxtNo_PO.Text & "', '" & Txt_Keterangan.Text & "', "
            SQL = SQL & "'" & Val(HilangkanTanda(Txt_JumlahPO.Text)) & "', '" & Val(HilangkanTanda(Txt_Berat.Text)) & "', '" & HilangkanTanda(txtGrand.Text) & "', " & flag_average & ")"
            ExecuteTrans(SQL)


            'Insert Detail
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

                SQL = "insert into Transaksi_Biaya_Lokal_Detail_Barang_Lain (kode_perusahaan, No_faktur, No_PO, Kode_stock_Owner, Kode_Biaya, "
                SQL = SQL & "Kode_Kontainer, jml_Po, Kode_Perusahaan_Biaya_Import, Jenis_Perhitungan, "
                SQL = SQL & "Mata_Uang, Kurs, biaya, Nilai_2, Total, Kode_Master_Kategori_Biaya_Import, Jns, "
                SQL = SQL & "kode_kategori_biaya_import,flag_average_kategori, avg_biaya, total_avg_biaya, Flag_Validasi_Biaya, Flag_Masuk_HPP) values( " 'coding stenly
                SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', "
                SQL = SQL & "'" & TxtNo_PO.Text & "', '" & Lvlokasi & "', "
                SQL = SQL & "'" & LvKodeBiaya & "', '" & LvKontainer & "', "
                SQL = SQL & "" & HilangkanTanda(LvJumlahPo) & ", '" & LvKodePerusahaanBiaya & "', "
                SQL = SQL & "'" & LvPerhitungan & "', '" & LvMataUang & "', " & HilangkanTanda(LvKurs) & ", "
                SQL = SQL & HilangkanTanda(LvBiaya) & ", " & HilangkanTanda(LvNilai2) & ", " & HilangkanTanda(LvTotal) & ", '" & LvMaster & "', "
                SQL = SQL & "" & jns & ", '" & LvKodeKategori & "', '" & LvFlagAvg & "', " & HilangkanTanda(LvBiaya) & ", " & HilangkanTanda(LvTotal) & ", '" & LvValidasi & "', '" & FLAG_HPP & "')" 'coding stenly
                ExecuteTrans(SQL)
            Next

            For i As Integer = 0 To DataGridView3.Rows.Count - 1
                Get_Isi_Listview3(i)

                SQL = "insert into Detail_Transaksi_Biaya_Lokal_By_Perusahaan_Barang_Lain(kode_perusahaan, No_faktur, Kode_Perusahaan_Biaya_Import, Mata_Uang, Nilai) "
                SQL = SQL & "values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', "
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

                SQL = "select * from Master_Kategori_Biaya_Import where Kode_Master_kategori_Biaya_import = '" & arrMaster.Item(index) & "'"
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

                                    SQL = "insert into Detail_Transaksi_Biaya_Lokal2_Barang_Lain (kode_perusahaan, No_faktur, Kode_Master_Kategori_Biaya_import, Nilai) "
                                    SQL = SQL & " values( "
                                    SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', "
                                    SQL = SQL & "'" & arrMaster.Item(index) & "', '" & TotMaster & "') "
                                    ExecuteTrans(SQL)




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


            '=============================
            '=     UPDATE FLAG BIAYA     =
            '=============================
            SQL = "update EMI_Pembelian_PO_Barang_Lain set Flag_Biaya = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtNo_PO.Text & "' "
            ExecuteTrans(SQL)


#Region "UPDATE HPP SATUAN DISPLAY"

            '=========================================
            '=     GET TOTAL BERAT BARANG (GRAM)     =
            '=========================================
            SQL = "select  a.No_Faktur, b.No_Urut, c.Kode_Stock_Owner, c.Kode_Barang, c.Jumlah_Masuk, c.Satuan_Barang, b.Harga, b.harga_barang,d.berat, "
            SQL = SQL & "ISNULL(( d.Berat * c.Jumlah_Masuk ), 0) as Tot_Berat_Barang, "

            'GET TotBeratPerPO
            SQL = SQL & "ISNULL(( select sum(x.Berat * z.Jumlah_Masuk) from emi_pembelian_loading_detail_barang_lain z, Barang_Lain x "
            SQL = SQL & "where z.Kode_Perusahaan = b.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and z.No_PO = b.No_Faktur "
            SQL = SQL & "and z.Kode_Stock_Owner = x.Kode_Stock_Owner and z.Kode_Barang = x.Kode_Barang "
            SQL = SQL & "),0) as Tot_Berat_PerPO, "

            SQL = SQL & "isnull( "
            SQL = SQL & "dbo.ubah_satuan_lain(c.Kode_Perusahaan, 'masa', c.Kode_Barang,  c.Satuan,c.Satuan_Barang, 1) "
            SQL = SQL & ", 0) as Perkalian_berat "

            SQL = SQL & "from EMI_Pembelian_PO_Barang_Lain a, EMI_Pembelian_PO_Detail_Barang_Lain b, emi_pembelian_loading_detail_barang_lain c, Barang_Lain d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_PO and b.No_Urut = c.Urut_PO "
            SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & TxtNo_PO.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dim TotalBiaya As Double = Val(HilangkanTanda(txtTotalBiaya.Text))
                            Dim TotBeratBarang As Double = Val(HilangkanTanda(.Rows(i).Item("Tot_Berat_Barang")))
                            Dim TotBeratBarangPerPO As Double = Val(HilangkanTanda(.Rows(i).Item("Tot_Berat_PerPO")))
                            Dim Harga As Double = Val(HilangkanTanda(.Rows(i).Item("Harga")))
                            Dim HargaBarang As Double = Val(HilangkanTanda(.Rows(i).Item("harga_barang")))
                            Dim BeratBahan As Double = Val(HilangkanTanda(.Rows(i).Item("berat")))
                            Dim BeratBahansatuan_besar As Double = .Rows(i).Item("berat") * .Rows(i).Item("Perkalian_berat")

                            Dim BiayaPerBarang As Double = (TotalBiaya / TotBeratBarangPerPO) * TotBeratBarang
                            Dim HargBaru As Double = Harga + ((BiayaPerBarang / TotBeratBarang) * BeratBahansatuan_besar)

                            '====================================
                            '=     UPATE HPP SATUAN DISPLAY     =
                            '====================================
                            SQL = "update emi_pembelian_loading_detail_barang_lain set "
                            SQL = SQL & "HPP_Satuan_Display = '" & Math.Round(HargBaru, 0) & "', harga_barang = '" & HargaBarang & "' "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_PO = '" & .Rows(i).Item("No_Faktur") & "' and Urut_PO = '" & .Rows(i).Item("No_Urut") & "' "
                            ExecuteTrans(SQL)

                        Next
                    End If
                End With
            End Using

#End Region




            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
        EMI_Display_Transaksi_Biaya_Lokal_Barang_Lain.Kosong()
        Me.Close()


    End Sub


    '============================================================
    Private Sub Get_No_Faktur()
        Txt_NoFaktur.Text = TBiaya_Import_Lain & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Transaksi_Biaya_Lokal_Barang_Lain", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(TBiaya_Import_Lain) + 4 & ")", TBiaya_Import_Lain & Format(tgl_skg, "MMyy"))
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

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvID = DataGridView1.Rows(No_Index).Cells(0).Value.ToString : cellID = 0
        Lvlokasi = DataGridView1.Rows(No_Index).Cells(1).Value.ToString : celllokasi = 1
        LvKodeKategori = DataGridView1.Rows(No_Index).Cells(2).Value.ToString : cellKodeKategori = 2
        LvKodeBiaya = DataGridView1.Rows(No_Index).Cells(3).Value.ToString : cellKodeBiaya = 3
        LvNamaBiaya = DataGridView1.Rows(No_Index).Cells(4).Value.ToString : cellNamaBiaya = 4
        LvKontainer = DataGridView1.Rows(No_Index).Cells(5).Value.ToString : cellKontainer = 5
        LvJumlahPo = DataGridView1.Rows(No_Index).Cells(6).Value.ToString : cellJumlahPo = 6
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

    Private Sub HapusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem.Click

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
End Class
