Public Class EMI_Display_HPP

    Dim arrCariTgl, arrCariParamLain As New ArrayList

    Dim LvP_NoFak, LvP_NoNota, LvP_Lokasi, LvP_Status, LvP_TglPO, LvP_TglJatuhTempo, LvP_KdSupplier, LvP_Supplier, LvP_User, LvP_JenisHPP, LvP_IdRencana As String

    '=== ITEM PARENT ==='
    Dim itemP_NoFak As Integer = 0
    Dim itemP_NoNota As Integer = 1
    Dim itemP_Lokasi As Integer = 2
    Dim itemP_Status As Integer = 3
    Dim itemP_TglPO As Integer = 4
    Dim itemP_TglJatuhTempo As Integer = 5
    Dim itemP_KdSupplier As Integer = 6
    Dim itemP_Supplier As Integer = 7
    Dim itemP_User As Integer = 8
    Dim itemP_JenisHPP As Integer = 9
    Dim itemP_IdRencana As Integer = 10

    '=== ITEM DATA KENDARAAN ==='
    Dim itemDK_NoPO As Integer = 0
    Dim itemDK_NoFak As Integer = 1
    Dim itemDK_NoSJ As Integer = 2
    Dim itemDK_Driver As Integer = 3
    Dim itemDK_NoPlat As Integer = 4
    Dim itemDK_BiayaPerjalanan As Integer = 5
    Dim itemDK_TglMasuk As Integer = 6
    Dim itemDK_JamMasuk As Integer = 7

    '=== ITEM DATA BARANG ==='
    Dim itemDB_NoFak As Integer = 0
    Dim itemDB_NoPO As Integer = 1
    Dim itemDB_KdBarang As Integer = 2
    Dim itemDB_NmBarang As Integer = 3
    Dim itemDB_JumlahPO As Integer = 4
    Dim itemDB_JumlahMasuk As Integer = 5
    Dim itemDB_HargaPO As Integer = 6
    Dim itemDB_HargaTotal As Integer = 7
    Dim itemDB_GrandPO As Integer = 8
    Dim itemDB_GrandTotal As Integer = 9

    '=== ITEM TOT HPP ==='
    Dim itemHP_NoFak As Integer = 0
    Dim itemHP_Lokasi As Integer = 1
    Dim itemHP_LokasiTujuan As Integer = 2
    Dim itemHP_KdBarang As Integer = 3
    Dim itemHP_NmBarang As Integer = 4
    Dim itemHP_Jumlah As Integer = 5
    Dim itemHP_HargaDeclare As Integer = 6
    Dim itemHP_TotHarga As Integer = 7
    Dim itemHP_PPH29 As Integer = 8
    Dim itemHP_BeratBersih As Integer = 9
    Dim itemHP_BeratKotor As Integer = 10
    Dim itemHP_BiayaImport As Integer = 11
    Dim itemHP_BiayaDryWet As Integer = 12
    Dim itemHP_BiayaBilling As Integer = 13
    Dim itemHP_BiayaStorage As Integer = 14
    Dim itemHP_BiayaFreight As Integer = 15
    Dim itemHP_HPPPerPcs As Integer = 16

    '=== ITEM BIAYA STORAGE ==='
    Dim itemBS_NoFak As Integer = 0
    Dim itemBS_Lokasi As Integer = 1
    Dim itemBS_KdKontainer As Integer = 2
    Dim itemBS_Pelabuhan As Integer = 3
    Dim itemBS_JenisKontainer As Integer = 4
    Dim itemBS_Hari As Integer = 5
    Dim itemBS_Harga As Integer = 6
    Dim itemBS_JmlhKontainer As Integer = 7
    Dim itemBS_JmlhHari As Integer = 8
    Dim itemBS_Biaya As Integer = 9

    '=== ITEM BIAYA STORAGE ==='
    Dim itemKurs_NoFak As Integer = 0
    Dim itemKurs_Lokasi As Integer = 1
    Dim itemKurs_IdRencana As Integer = 2
    Dim itemKurs_MataUang As Integer = 3
    Dim itemKurs_Jenis As Integer = 4
    Dim itemKurs_Nilai As Integer = 5


    Private Sub EMI_Display_HPP_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '=== Lv Parent ==='
        Lv_PembelianPO.Columns.Clear()
        Lv_PembelianPO.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_PembelianPO.Columns.Add("No Nota", 130, HorizontalAlignment.Left)
        Lv_PembelianPO.Columns.Add("Lokasi", 130, HorizontalAlignment.Left)
        Lv_PembelianPO.Columns.Add("Status", 80, HorizontalAlignment.Center)
        Lv_PembelianPO.Columns.Add("Tanggal PO", 110, HorizontalAlignment.Center)
        Lv_PembelianPO.Columns.Add("Jatuh Tempo", 110, HorizontalAlignment.Center)
        Lv_PembelianPO.Columns.Add("KdSupplier", 0, HorizontalAlignment.Center)
        Lv_PembelianPO.Columns.Add("Supplier", 200, HorizontalAlignment.Left)
        Lv_PembelianPO.Columns.Add("User", 120, HorizontalAlignment.Left)
        Lv_PembelianPO.Columns.Add("Jenis HPP", 100, HorizontalAlignment.Center)
        'HIde
        Lv_PembelianPO.Columns.Add("idRencana", 0, HorizontalAlignment.Center)
        Lv_PembelianPO.View = View.Details

        kosong()

    End Sub

    Private Sub Get_LvP(ByVal index As Integer)

        LvP_NoFak = Lv_PembelianPO.Items(index).SubItems(itemP_NoFak).Text
        LvP_NoNota = Lv_PembelianPO.Items(index).SubItems(itemP_NoNota).Text
        LvP_Lokasi = Lv_PembelianPO.Items(index).SubItems(itemP_Lokasi).Text
        LvP_Status = Lv_PembelianPO.Items(index).SubItems(itemP_Status).Text
        LvP_TglPO = Lv_PembelianPO.Items(index).SubItems(itemP_TglPO).Text
        LvP_TglJatuhTempo = Lv_PembelianPO.Items(index).SubItems(itemP_TglJatuhTempo).Text
        LvP_KdSupplier = Lv_PembelianPO.Items(index).SubItems(itemP_KdSupplier).Text
        LvP_Supplier = Lv_PembelianPO.Items(index).SubItems(itemP_Supplier).Text
        LvP_User = Lv_PembelianPO.Items(index).SubItems(itemP_User).Text
        LvP_JenisHPP = Lv_PembelianPO.Items(index).SubItems(itemP_JenisHPP).Text
        LvP_IdRencana = Lv_PembelianPO.Items(index).SubItems(itemP_IdRencana).Text

    End Sub


    Private Sub kosong()
        '=== FILTER ==='
        ComboBox6.Items.Clear()
        CheckBox3.Checked = False
        CheckBox1.Checked = False : ComboBox1.Items.Clear()
        DateTimePicker1.Value = DateTime.Now : DateTimePicker2.Value = DateTime.Now
        CheckBox2.Checked = False : ComboBox2.Items.Clear() : TextBox1.Text = ""

        ComboBox1.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        ComboBox2.Enabled = False : TextBox1.Enabled = False

        arrCariTgl.Clear()
        ComboBox1.Items.Add("Tanggal PO") : arrCariTgl.Add("Tgl_PO")
        ComboBox1.Items.Add("Jatuh Tempo") : arrCariTgl.Add("Tgl_Jatuh_Tempo")

        arrCariParamLain.Clear()
        ComboBox2.Items.Add("No Faktur") : arrCariParamLain.Add("No_Faktur")
        ComboBox2.Items.Add("No Nota") : arrCariParamLain.Add("No_Nota")
        ComboBox2.Items.Add("Status") : arrCariParamLain.Add("Status")
        ComboBox2.Items.Add("Supplier") : arrCariParamLain.Add("Supplier")
        ComboBox2.Items.Add("User") : arrCariParamLain.Add("UserID")
        ComboBox2.Items.Add("Jenis HPP") : arrCariParamLain.Add("Jenis_HPP")


        '=== TAB CONTROL ==='
        TabControl1.TabPages.Clear()
        TabControl1.TabPages.Add(Data_Kendaraan)
        TabControl1.TabPages.Add(Data_Barang)
        TabControl1.TabPages.Remove(Tot_HPP)
        TabControl1.TabPages.Remove(Biaya_Storage)
        TabControl1.TabPages.Remove(Kurs)
        TabControl1.TabPages.Remove(Biaya_Import)

        '=== LISTVIEW ==='
        Load_Header_Lv_Detail()

        Try
            OpenConn()

            '=======================
            '=     LOAD LOKASI     =
            '=======================
            xSplit = CekKotaRole().Split(",")
            ComboBox6.Items.Clear()
            ComboBox6.Items.Add("-- Seluruh --")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)
            SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
                ComboBox6.Text = Lokasi
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Btn_Cari_Click(Btn_Cari, EventArgs.Empty)

    End Sub

    Private Sub Load_Header_Lv_Detail()

        '=== Data Kendaraan ==='
        Lv_DataKendaraan.Columns.Clear()
        Lv_DataKendaraan.Columns.Add("No PO", 130, HorizontalAlignment.Left) '0
        Lv_DataKendaraan.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '1
        Lv_DataKendaraan.Columns.Add("No SJ", 130, HorizontalAlignment.Left) '2
        Lv_DataKendaraan.Columns.Add("Driver", 180, HorizontalAlignment.Left) '3
        Lv_DataKendaraan.Columns.Add("No Plat", 130, HorizontalAlignment.Left) '4
        Lv_DataKendaraan.Columns.Add("Biaya Perjalanan", 130, HorizontalAlignment.Right) '5
        Lv_DataKendaraan.Columns.Add("Tanggal Masuk", 130, HorizontalAlignment.Center) '6
        Lv_DataKendaraan.Columns.Add("Jam Masuk", 130, HorizontalAlignment.Center) '7
        Lv_DataKendaraan.View = View.Details

        '=== Data Barang ==='
        Lv_DataBarang.Columns.Clear()
        Lv_DataBarang.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '0
        Lv_DataBarang.Columns.Add("NO PO", 130, HorizontalAlignment.Left) '1
        Lv_DataBarang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '2
        Lv_DataBarang.Columns.Add("Nama", 200, HorizontalAlignment.Left) '3
        Lv_DataBarang.Columns.Add("Jumlah PO", 120, HorizontalAlignment.Right) '4
        Lv_DataBarang.Columns.Add("Jumlah Masuk", 120, HorizontalAlignment.Right) '5
        Lv_DataBarang.Columns.Add("Harga PO", 120, HorizontalAlignment.Right) '6
        Lv_DataBarang.Columns.Add("Harga Total", 120, HorizontalAlignment.Right) '7
        Lv_DataBarang.Columns.Add("Grand PO", 120, HorizontalAlignment.Right) '8
        Lv_DataBarang.Columns.Add("Grand Total", 120, HorizontalAlignment.Right) '9
        Lv_DataBarang.View = View.Details

        '=== TOT HPP ==='
        Lv_TotHpp.Columns.Clear()
        Lv_TotHpp.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '0
        Lv_TotHpp.Columns.Add("Lokasi", 100, HorizontalAlignment.Left) '1
        Lv_TotHpp.Columns.Add("Lokasi Tujuan", 150, HorizontalAlignment.Left) '2
        Lv_TotHpp.Columns.Add("Kode Barang", 110, HorizontalAlignment.Left) '3
        Lv_TotHpp.Columns.Add("Nama", 200, HorizontalAlignment.Left) '4
        Lv_TotHpp.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '5
        Lv_TotHpp.Columns.Add("Harga Declare", 120, HorizontalAlignment.Right) '6
        Lv_TotHpp.Columns.Add("Total Harga", 120, HorizontalAlignment.Right) '7
        Lv_TotHpp.Columns.Add("PPH29", 120, HorizontalAlignment.Right) '8
        Lv_TotHpp.Columns.Add("Berat Bersih", 120, HorizontalAlignment.Right) '9
        Lv_TotHpp.Columns.Add("Berat Kotor", 120, HorizontalAlignment.Right) '10
        Lv_TotHpp.Columns.Add("Biaya Import", 120, HorizontalAlignment.Right) '11
        Lv_TotHpp.Columns.Add("Biaya Dry Wet", 120, HorizontalAlignment.Right) '12
        Lv_TotHpp.Columns.Add("Biaya Billing", 120, HorizontalAlignment.Right) '13
        Lv_TotHpp.Columns.Add("Biaya Storage", 120, HorizontalAlignment.Right) '14
        Lv_TotHpp.Columns.Add("Biaya Freight", 120, HorizontalAlignment.Right) '15
        Lv_TotHpp.Columns.Add("Total Hpp Per Pcs", 120, HorizontalAlignment.Right) '16
        Lv_TotHpp.View = View.Details


        '=== BIAYA STORAGE ==='
        Lv_BiayaStorage.Columns.Clear()
        Lv_BiayaStorage.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '0
        Lv_BiayaStorage.Columns.Add("Lokasi", 150, HorizontalAlignment.Left) '1
        Lv_BiayaStorage.Columns.Add("Kode Kontainer", 110, HorizontalAlignment.Left) '2
        Lv_BiayaStorage.Columns.Add("Pelabuhan", 150, HorizontalAlignment.Left) '3
        Lv_BiayaStorage.Columns.Add("Jenis Kontainer", 120, HorizontalAlignment.Left) '4
        Lv_BiayaStorage.Columns.Add("Hari", 100, HorizontalAlignment.Center) '5
        Lv_BiayaStorage.Columns.Add("Harga", 120, HorizontalAlignment.Right) '6
        Lv_BiayaStorage.Columns.Add("Jumlah Kontainer", 120, HorizontalAlignment.Right) '7
        Lv_BiayaStorage.Columns.Add("Jumlah Hari", 100, HorizontalAlignment.Right) '8
        Lv_BiayaStorage.Columns.Add("Biaya", 120, HorizontalAlignment.Right) '9
        Lv_BiayaStorage.View = View.Details

        '=== KURS ==='
        Lv_Kurs.Columns.Clear()
        Lv_Kurs.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '0
        Lv_Kurs.Columns.Add("Lokasi", 120, HorizontalAlignment.Left) '1
        Lv_Kurs.Columns.Add("iD Rencana", 100, HorizontalAlignment.Center) '2
        Lv_Kurs.Columns.Add("Mata Uang", 100, HorizontalAlignment.Center) '3
        Lv_Kurs.Columns.Add("Jenis", 130, HorizontalAlignment.Left) '4
        Lv_Kurs.Columns.Add("Nilai", 130, HorizontalAlignment.Right) '5
        Lv_Kurs.View = View.Details

        '=== BIAYA IMPORT ==='
        Lv_BiayaImport.Columns.Clear()
        Lv_BiayaImport.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_BiayaImport.Columns.Add("Kode Master", 200, HorizontalAlignment.Left)
        Lv_BiayaImport.Columns.Add("Kode Kategori", 200, HorizontalAlignment.Left)
        Lv_BiayaImport.Columns.Add("Total", 130, HorizontalAlignment.Right)
        Lv_BiayaImport.Columns.Add("Masuk HPP", 80, HorizontalAlignment.Center)
        Lv_BiayaImport.View = View.Details


    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked Then
            ComboBox1.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
        Else
            ComboBox1.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        End If

        ComboBox1.SelectedIndex = -1 : ComboBox1.Text = ""
        DateTimePicker1.Value = DateTime.Now : DateTimePicker2.Value = DateTime.Now

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        If CheckBox2.Checked Then
            ComboBox2.Enabled = True : TextBox1.Enabled = True
        Else
            ComboBox2.Enabled = False : TextBox1.Enabled = False
        End If

        ComboBox2.SelectedIndex = -1 : ComboBox2.Text = ""
        TextBox1.Text = ""

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        'If CheckBox1.Checked = False And CheckBox3.Checked = False And CheckBox2.Checked = False Then
        '    MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
        '    CheckBox1.Focus() : Exit Sub
        'End If

        If CheckBox1.Checked Then
            If ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Parameter pencarian per tanggal harus diisi!", Judul)
                ComboBox1.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text)
                Exit Sub
            End If

        ElseIf CheckBox2.Checked Then
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                ComboBox2.Focus() : Exit Sub
            ElseIf TextBox1.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                TextBox1.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            Lv_PembelianPO.Items.Clear()
            SQL = ";with cte_a as ( "
            SQL = SQL & "select a.Kode_Perusahaan, a.No_Faktur, a.No_Nota, a.Lokasi, a.Status, a.Tanggal as Tgl_PO, a.Tgl_Jatuh_Tempo, a.Kode_Supplier, b.Nama as Supplier, a.UserID,  "
            SQL = SQL & "'-' as id_rencana, 'LOKAL' as Jenis_HPP "
            SQL = SQL & "from emi_pembelian_po a, suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select a.Kode_Perusahaan, a.no_faktur, d.No_Nota, b.lokasi, a.status, b.Tanggal_PO as Tgl_PO, d.Tgl_Jatuh_Tempo, c.kode_supplier, c.nama as Supplier, a.userid, "
            SQL = SQL & "a.id_rencana, 'IMPORT' as Jenis_HPP "
            SQL = SQL & "from hpp_import a, rencana_order b, suppliers c,emi_pembelian_po d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = c.Kode_Supplier and b.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.ID_Rencana = b.ID_Rencana "
            SQL = SQL & "and a.Kode_Supplier = c.Kode_Supplier "
            SQL = SQL & "and b.No_PO = d.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            SQL = SQL & ") select * from cte_a "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "

            If CheckBox3.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " Tgl_PO between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If CheckBox1.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrCariTgl.Item(ComboBox1.SelectedIndex) & " Between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrCariParamLain.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
            End If

            SQL = SQL & "order by Tgl_Jatuh_Tempo DESC "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_PembelianPO.Items.Add(Dr("no_faktur"))
                    Lv.SubItems.Add(Dr("No_Nota"))
                    Lv.SubItems.Add(Dr("lokasi"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("status")) = "", "-", Dr("status")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tgl_PO")) = "", "-", Format(Dr("Tgl_PO"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tgl_Jatuh_Tempo")) = "", "-", Format(Dr("Tgl_Jatuh_Tempo"), "dd MMM yyyy")))
                    Lv.SubItems.Add(Dr("kode_supplier"))
                    Lv.SubItems.Add(Dr("Supplier"))
                    Lv.SubItems.Add(Dr("userid"))
                    Lv.SubItems.Add(Dr("Jenis_HPP"))
                    Lv.SubItems.Add(Dr("id_rencana"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_PembelianPO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PembelianPO.SelectedIndexChanged
        If Lv_PembelianPO.Items.Count = 0 Or Lv_PembelianPO.FocusedItem.Index = -1 Then Exit Sub

        Dim SelectedRowsIndex = Lv_PembelianPO.FocusedItem.Index

        Get_LvP(SelectedRowsIndex)

        If LvP_JenisHPP = "LOKAL" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(Data_Kendaraan)
            TabControl1.TabPages.Add(Data_Barang)
            TabControl1.TabPages.Remove(Tot_HPP) : Lv_TotHpp.Items.Clear()
            TabControl1.TabPages.Remove(Biaya_Storage) : Lv_BiayaStorage.Items.Clear()
            TabControl1.TabPages.Remove(Kurs) : Lv_Kurs.Items.Clear()
            TabControl1.TabPages.Remove(Biaya_Import) : Lv_BiayaImport.Items.Clear()

            Try
                OpenConn()

                '===============================
                '=     LOAD DATA KENDARAAN     =
                '===============================
                Lv_DataKendaraan.Items.Clear()
                SQL = "select b.No_PO, a.No_Faktur, a.No_SJ, a.Driver, a.No_Plat, a.Biaya_Perjalanan, a.Tanggal_Masuk, a.Jam_Masuk "
                SQL = SQL & "from emi_pembelian_loading a, EMI_Pembelian_Loading_Detail b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.No_PO = '" & LvP_NoFak & "' "
                SQL = SQL & "group by a.No_Faktur,b.No_PO, a.No_SJ, a.Driver, a.No_Plat, a.Biaya_Perjalanan, a.Tanggal_Masuk, a.Jam_Masuk "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = Lv_DataKendaraan.Items.Add(Dr("No_PO"))
                        Lv.SubItems.Add(Dr("No_Faktur"))
                        Lv.SubItems.Add(Dr("No_SJ"))
                        Lv.SubItems.Add(Dr("Driver"))
                        Lv.SubItems.Add(Dr("No_Plat"))
                        Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Biaya_Perjalanan")) = "", "0", Format(Dr("Biaya_Perjalanan"), "N2")))
                        Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Masuk")) = "", "-", Format(Dr("Tanggal_Masuk"), "dd MMM yyyy")))
                        Lv.SubItems.Add(Dr("Jam_Masuk"))

                    Loop
                End Using

                '============================
                '=     LOAD DATA BARANG     =
                '============================
                Lv_DataBarang.Items.Clear()
                SQL = "select a.no_faktur, a.no_po, a.kode_barang, b.nama, "
                SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'MASA', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.jumlah_barang) as Jumlah_PO, "
                SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'MASA', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.jumlah_masuk)  as Jumlah_Masuk, "
                SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'UANG', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.Harga_Barang)  as Harga_PO, "
                SQL = SQL & "a.HPP_Satuan_Display as Harga_Akhir, "
                SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'MASA', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.jumlah_masuk) * "
                SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'UANG', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.Harga_Barang) as Grand_PO, "
                SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'MASA', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.jumlah_masuk) * "
                SQL = SQL & "a.HPP_Satuan_Display as Grand_Akhir "
                SQL = SQL & "from EMI_Pembelian_Loading_Detail a, barang b, EMI_Pembelian_Loading c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.kode_barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.no_po = '" & LvP_NoFak & "' "
                SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan  and a.no_faktur=c.No_Faktur "
                SQL = SQL & "and a.Flag_Timbang_Keluar='Y' and ((c.Flag_Import='Y' and c.Flag_Import_HPP='Y') or Flag_Import is null) "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = Lv_DataBarang.Items.Add(Dr("no_faktur"))
                        Lv.SubItems.Add(Dr("no_po"))
                        Lv.SubItems.Add(Dr("kode_barang"))
                        Lv.SubItems.Add(Dr("nama"))
                        Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jumlah_PO")) = "", "0", Format(Dr("Jumlah_PO"), "N2")))
                        Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jumlah_Masuk")) = "", "0", Format(Dr("Jumlah_Masuk"), "N2")))
                        Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Harga_PO")) = "", "0", Format(Dr("Harga_PO"), "N2")))
                        Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Harga_Akhir")) = "", "0", Format(Dr("Harga_Akhir"), "N2")))
                        Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Grand_PO")) = "", "0", Format(Dr("Grand_PO"), "N2")))
                        Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Grand_Akhir")) = "", "0", Format(Dr("Grand_Akhir"), "N2")))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



        ElseIf LvP_JenisHPP = "IMPORT" Then
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Remove(Data_Kendaraan) : Lv_DataKendaraan.Items.Clear()
            TabControl1.TabPages.Remove(Data_Barang) : Lv_DataBarang.Items.Clear()
            TabControl1.TabPages.Add(Tot_HPP)
            TabControl1.TabPages.Add(Biaya_Storage)
            TabControl1.TabPages.Add(Kurs)
            TabControl1.TabPages.Add(Biaya_Import)

            Try
                OpenConn()

                '=========================
                '=     LOAD DATA HPP     =
                '=========================
                Lv_TotHpp.Items.Clear()
                SQL = "select a.No_Faktur, e.Lokasi, "
                SQL = SQL & "COALESCE(NULLIF(c.lokasi_tujuan, null), b.Kode_Stock_Owner) as Lokasi_Tujuan, b.Kode_Barang, d.Nama, "
                SQL = SQL & "c.Jumlah, c.Harga, COALESCE(NULLIF(c.total_harga, 0), b.total_harga) as Total_Harga, b.PPH29,  "
                SQL = SQL & "COALESCE(NULLIF(c.Berat_Bersih, 0), b.Berat_Bersih) as Berat_Bersih, COALESCE(NULLIF(c.berat_kotor, 0), b.berat_kotor) as Berat_Kotor, "
                SQL = SQL & "COALESCE(NULLIF(c.Biaya_import2, 0), b.Biaya_Import) AS Biaya_Import, c.Biaya_import_Wet_Dry, b.Biaya_Billing, b.Biaya_Kontainer as Biaya_Storage, "
                SQL = SQL & "b.Biaya_Freight_int, COALESCE(NULLIF(c.nilai_hpp_barang_per_pcs_brsh, 0), b.nilai_hpp_barang_per_pcs) AS Total_HPP_Per_Pcs "
                SQL = SQL & "from hpp_import a, detail_hpp_import b, detail_hpp_import2 c, barang d, Rencana_Order e "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
                SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
                SQL = SQL & "and a.ID_Rencana = e.ID_Rencana "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.id_rencana = '" & LvP_IdRencana & "' "
                SQL = SQL & "order by a.No_Faktur "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = Lv_TotHpp.Items.Add(Dr("no_faktur"))
                        Lv.SubItems.Add(Dr("lokasi"))
                        Lv.SubItems.Add(Dr("lokasi_tujuan"))
                        Lv.SubItems.Add(Dr("kode_barang"))
                        Lv.SubItems.Add(Dr("nama"))
                        Lv.SubItems.Add(Format(Dr("jumlah"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Harga"), "N2"))
                        Lv.SubItems.Add(Format(Dr("total_harga"), "N2"))
                        Lv.SubItems.Add(Format(Dr("PPH29"), "N2"))
                        Lv.SubItems.Add(Format(Dr("berat_bersih"), "N2"))
                        Lv.SubItems.Add(Format(Dr("berat_kotor"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Biaya_Import"), "N2"))
                        Lv.SubItems.Add(Format(Dr("biaya_import_wet_dry"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Biaya_Billing"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Biaya_Storage"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Biaya_Freight_int"), "N2"))
                        Lv.SubItems.Add(Format(Dr("Total_HPP_Per_Pcs"), "N2"))
                    Loop
                End Using

                '==============================
                '=     LOAD BIAYA STORAGE     =
                '==============================
                Lv_BiayaStorage.Items.Clear()
                SQL = " select a.no_faktur, a.kode_Stock_owner, a.kode_kontainer,b.Lokasi, a.dari,  a.harga, a.jumlah_kontainer, a.biaya  "
                SQL = SQL & " from detail_storage_hpp_a a, pelabuhan b "
                SQL = SQL & " where a.kode_perusahaan = b.Kode_Perusahaan and a.Kode_Pelabuhan = b.Kode_Pelabuhan and a.no_faktur = '" & LvP_NoFak & "' "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = Lv_BiayaStorage.Items.Add(Dr("no_faktur"))
                        Lv.SubItems.Add(Dr("kode_Stock_owner"))
                        Lv.SubItems.Add(Dr("kode_kontainer"))
                        Lv.SubItems.Add(Dr("Lokasi"))
                        Lv.SubItems.Add("-")
                        Lv.SubItems.Add(Dr("dari"))
                        Lv.SubItems.Add(Format(Dr("harga"), "N2"))
                        Lv.SubItems.Add(Dr("jumlah_kontainer"))
                        Lv.SubItems.Add("")
                        Lv.SubItems.Add(Format(Dr("biaya"), "N2"))
                    Loop
                End Using

                '=====================
                '=     LOAD KURS     =
                '=====================
                Lv_Kurs.Items.Clear()
                SQL = " select a.no_faktur,b.id_rencana, c.lokasi, a.mata_uang, a.jenis,a.nilai from kurs_hpp_import a, HPP_Import b, Rencana_Order c "
                SQL = SQL & " where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.no_faktur and b.id_rencana = c.id_rencana "
                SQL = SQL & " and b.id_rencana = '" & LvP_IdRencana & "' "
                SQL = SQL & " group by  a.no_faktur,b.id_rencana, c.lokasi, a.mata_uang, a.jenis,a.nilai "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = Lv_Kurs.Items.Add(Dr("no_faktur"))
                        Lv.SubItems.Add(Dr("lokasi"))
                        Lv.SubItems.Add(Dr("id_rencana"))
                        Lv.SubItems.Add(Dr("mata_uang"))
                        Lv.SubItems.Add(Dr("jenis"))
                        Lv.SubItems.Add(Format(Dr("nilai"), "N2"))
                    Loop
                End Using

                '=============================
                '=     LOAD BIAYA IMPORT     =
                '=============================
                Dim TotMasukHPP As Double = 0
                Dim TotTidakMasukHPP As Double = 0
                Dim TotFreight As Double = 0

                Lv_BiayaImport.Items.Clear()
                SQL = "select a.no_faktur,a.kode_master_kategori_biaya_import, c.Kode_Kategori_Biaya_Import,"
                SQL = SQL & "sum(a.total) as Total, a.flag_masuk_HPP, 'A' as Jenis "
                SQL = SQL & "from detail_transaksi_biaya_import a,"
                SQL = SQL & "Master_Kategori_Biaya_Import b,Kategori_Biaya_Import c,biaya_import d,"
                SQL = SQL & "perusahaan_biaya_import e,transaksi_biaya_import f where "
                SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and b.kode_perusahaan=c.kode_perusahaan "
                SQL = SQL & "and c.kode_perusahaan=d.kode_perusahaan and d.kode_perusahaan=e.kode_perusahaan "
                SQL = SQL & "and e.kode_perusahaan=f.kode_perusahaan and a.kode_perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and a.kode_master_kategori_biaya_import=b.kode_master_kategori_biaya_import "
                SQL = SQL & "and b.kode_master_kategori_biaya_import=c.kode_master_kategori_biaya_import "
                SQL = SQL & "and c.kode_kategori_biaya_import=d.Kode_Kategori_Biaya_Import "
                SQL = SQL & "and a.Kode_Biaya=d.Kode_Biaya and a.no_faktur=f.No_Faktur "
                SQL = SQL & "and a.kode_perusahaan_biaya_import=e.Kode_Perusahaan_Biaya_Import "
                SQL = SQL & "and f.id_rencana='" & LvP_IdRencana & "' "
                SQL = SQL & "group by a.no_faktur,a.kode_master_kategori_biaya_import, c.Kode_Kategori_Biaya_Import, a.flag_masuk_HPP  "
                SQL = SQL & " union all "
                SQL = SQL & "select a.no_faktur,a.kode_master_kategori_biaya_import, c.Kode_Kategori_Biaya_Import,"
                SQL = SQL & "sum(a.total) as Total, a.flag_masuk_HPP, 'B' as Jenis  "
                SQL = SQL & "from detail_transaksi_biaya_import3 a,"
                SQL = SQL & "Master_Kategori_Biaya_Import b,Kategori_Biaya_Import c,biaya_import d,"
                SQL = SQL & "perusahaan_biaya_import e,transaksi_biaya_import3 f where "
                SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and b.kode_perusahaan=c.kode_perusahaan "
                SQL = SQL & "and c.kode_perusahaan=d.kode_perusahaan and d.kode_perusahaan=e.kode_perusahaan "
                SQL = SQL & "and e.kode_perusahaan=f.kode_perusahaan and a.kode_perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and a.kode_master_kategori_biaya_import=b.kode_master_kategori_biaya_import "
                SQL = SQL & "and b.kode_master_kategori_biaya_import=c.kode_master_kategori_biaya_import "
                SQL = SQL & "and c.kode_kategori_biaya_import=d.Kode_Kategori_Biaya_Import "
                SQL = SQL & "and a.Kode_Biaya=d.Kode_Biaya and a.no_faktur=f.No_Faktur "
                SQL = SQL & "and a.kode_perusahaan_biaya_import=e.Kode_Perusahaan_Biaya_Import "
                SQL = SQL & "and f.id_rencana='" & LvP_IdRencana & "' "
                SQL = SQL & "group by a.no_faktur,a.kode_master_kategori_biaya_import, c.Kode_Kategori_Biaya_Import, a.flag_masuk_HPP  "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lvw As ListViewItem
                        Lvw = Lv_BiayaImport.Items.Add(Dr("no_faktur"))
                        Lvw.SubItems.Add(Dr("kode_master_kategori_biaya_import"))
                        Lvw.SubItems.Add(Dr("Kode_Kategori_Biaya_Import"))
                        Lvw.SubItems.Add(Format(Dr("Total"), "N2"))

                        If General_Class.CekNULL(Dr("flag_masuk_HPP")) = "" Then
                            Lvw.SubItems.Add("T")
                        Else
                            Lvw.SubItems.Add(Dr("flag_masuk_HPP"))
                        End If


                        If General_Class.CekNULL(Dr("flag_masuk_HPP")) = "" Then
                            Lvw.SubItems.Add("T")
                            TotTidakMasukHPP = TotTidakMasukHPP + Val(HilangkanTanda(Dr("Total")))
                        Else
                            If Dr("flag_masuk_HPP") = "Y" Then
                                TotMasukHPP = TotMasukHPP + Val(HilangkanTanda(Dr("Total")))
                            Else
                                TotTidakMasukHPP = TotTidakMasukHPP + Val(HilangkanTanda(Dr("Total")))
                            End If
                        End If

                        If Dr("Jenis") = "B" Then
                            TotFreight = TotFreight + Val(HilangkanTanda(Dr("Total")))
                        End If

                    Loop

                    Txt_TotMskHPP.Text = Format(TotMasukHPP, "N2")
                    Txt_TotTdkMskHPP.Text = Format(TotTidakMasukHPP, "N2")
                    Txt_TotFreight.Text = Format(TotFreight, "N2")
                End Using



                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        End If




    End Sub
















End Class