Public Class Display_Emi_Pelunasan_Hutang

    Dim lv As New ListViewItem

    Dim Arr1, Arr2, Arr3, Arr4, arr_jenis_display As New ArrayList
    Dim arrSfA, arrSfB As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black


    Dim Lv_NoPO As String
    Dim Lv_TglPO As String
    Dim Lv_NoPemb As String
    Dim Lv_TglPemb As String
    Dim Lv_Keterangan As String
    Dim Lv_KdPerusahaanBiayaImport As String
    Dim Lv_Perusahaan As String
    Dim Lv_KdKategori As String
    Dim Lv_NmKategori As String
    Dim Lv_MataUang As String
    Dim Lv_Hutang As String
    Dim Lv_HutangIDR As String
    Dim Lv_PPN As String
    Dim Lv_PPH As String
    Dim Lv_Pelunasan As String
    Dim Lv_PelunasanIDR As String
    Dim Lv_Sisa As String
    Dim Lv_SisaIDR As String
    Dim Lv_Lokasi As String
    Dim Lv_JatuhTempo As String
    Dim Lv_Jenis As String
    Dim Lv_PengajuanIDR As String

    Dim item_NoPO As Integer = 0
    Dim item_TglPO As Integer = 1
    Dim item_NoPemb As Integer = 2
    Dim item_TglPemb As Integer = 3
    Dim item_Keterangan As Integer = 4
    Dim item_KdPerusahaanBiayaImport As Integer = 5
    Dim item_Perusahaan As Integer = 6
    Dim item_KdKategori As Integer = 7
    Dim item_NmKategori As Integer = 8
    Dim item_MataUang As Integer = 9
    Dim item_Hutang As Integer = 10
    Dim item_HutangIDR As Integer = 11
    Dim item_PPN As Integer = 12
    Dim item_PPH As Integer = 13
    Dim item_Pelunasan As Integer = 14
    Dim item_PelunasanIDR As Integer = 15
    Dim item_Sisa As Integer = 16
    Dim item_SisaIDR As Integer = 17
    Dim item_Lokasi As Integer = 18
    Dim item_JatuhTempo As Integer = 19
    Dim item_Jenis As Integer = 20
    Dim item_PengajuanIDR As Integer = 21

    Private Sub Laporan_Bahan_Tidak_Potong_Stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Display_Barang_Masuk")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        LvHutangBiaya.Columns.Add("No PO", 130, HorizontalAlignment.Left) '0
        LvHutangBiaya.Columns.Add("Tanggal PO", 120, HorizontalAlignment.Center) '1
        LvHutangBiaya.Columns.Add("No Pembelian", 130, HorizontalAlignment.Left) '2
        LvHutangBiaya.Columns.Add("Tanggal Pembelian", 120, HorizontalAlignment.Center) '3
        LvHutangBiaya.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '4
        LvHutangBiaya.Columns.Add("KdPerusahanBiayaImport", 0, HorizontalAlignment.Left) '5
        LvHutangBiaya.Columns.Add("Perusahaan", 180, HorizontalAlignment.Left) '6
        LvHutangBiaya.Columns.Add("KdKategori", 0, HorizontalAlignment.Left) '7
        LvHutangBiaya.Columns.Add("Kategori", 150, HorizontalAlignment.Left) '8
        LvHutangBiaya.Columns.Add("Mata Uang", 90, HorizontalAlignment.Center) '9
        LvHutangBiaya.Columns.Add("Total Hutang", 150, HorizontalAlignment.Right) '10
        LvHutangBiaya.Columns.Add("Total Hutang IDR", 150, HorizontalAlignment.Right) '11
        LvHutangBiaya.Columns.Add("PPN", 150, HorizontalAlignment.Right) '12
        LvHutangBiaya.Columns.Add("PPH", 150, HorizontalAlignment.Right) '13
        LvHutangBiaya.Columns.Add("Pelunasan", 150, HorizontalAlignment.Right) '14
        LvHutangBiaya.Columns.Add("Pembayaran IDR", 150, HorizontalAlignment.Right) '15
        LvHutangBiaya.Columns.Add("Sisa", 150, HorizontalAlignment.Right) '16
        LvHutangBiaya.Columns.Add("Sisa IDR", 150, HorizontalAlignment.Right) '17
        'HIDE
        LvHutangBiaya.Columns.Add("Lokasi", 0, HorizontalAlignment.Right) '18
        LvHutangBiaya.Columns.Add("Jatuh Tempo", 120, HorizontalAlignment.Center).DisplayIndex = 2 '19
        LvHutangBiaya.Columns.Add("Jenis", 80, HorizontalAlignment.Left) '20
        LvHutangBiaya.Columns.Add("Pengajuan IDR", 150, HorizontalAlignment.Right).DisplayIndex = 16 '21
        LvHutangBiaya.View = View.Details

        ' get_lokasi()

        Cmb_Jenis_Display.Items.Clear() : arr_jenis_display.Clear()
        Cmb_Jenis_Display.Items.Add("Histori") : arr_jenis_display.Add("HISTORI")
        Cmb_Jenis_Display.Items.Add("Pelunasan") : arr_jenis_display.Add("PELUNASAN")

        kosong()

    End Sub

    Private Sub kosong()

        Try
            OpenConn()

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)

            'xSplit = CekKotaRole().Split(", ")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            'ComboBox1.Items.Add("Seluruh")
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox6.Text = Lokasi

            If CekButtonRole("Ganti_Lokasi_Display_Biaya_Import") = "T" Then
                ComboBox6.Enabled = False
            Else
                ComboBox6.Enabled = True
            End If

            'ComboBox3.Items.Add("Y") : Arr4.Add("Y")
            'ComboBox3.Items.Add("T") : Arr4.Add("T")
            'ComboBox3.SelectedIndex = 1

            ComboBox3.Items.Clear() : Arr1.Clear() : arrSfB.Clear()
            'ComboBox3.Items.Add("Tanggal") : Arr1.Add("a.tanggal")
            ComboBox3.Items.Add("Tanggal PO") : Arr1.Add("a.Tanggal_PO") : arrSfB.Add("Tanggal_PO")
            ComboBox3.Items.Add("Jatuh Tempo") : Arr1.Add("a.Tgl_Jatuh_Tempo") : arrSfB.Add("Tgl_Jatuh_Tempo")
            ComboBox3.Items.Add("Tanggal Pembelian") : Arr1.Add("a.Tanggal_Pembelian") : arrSfB.Add("Tanggal_Pembelian")

            'TextBoxa.Text = "0"
            ComboBox3.Enabled = False : ComboBox2.Enabled = False
            Tgl1.Enabled = False : Tgl2.Enabled = False
            TextBox4.Enabled = False
            Tgl1.Enabled = False : Tgl2.Enabled = False : ComboBox3.Enabled = False

            CheckBox5.Checked = False
            CheckBox1.Checked = False
            ComboBox3.Text = "" : ComboBox3.SelectedIndex = -1 : Tgl1.Value = DateTime.Now : Tgl2.Value = DateTime.Now

            CheckBox6.Checked = False
            ComboBox2.Text = "" : ComboBox2.SelectedIndex = -1 : TextBox4.Text = ""

            ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear() : arrSfA.Clear()
            ComboBox2.Items.Add("No PO") : Arr2.Add("a.No_PO") : arrSfA.Add("No_PO")
            ComboBox2.Items.Add("No Pembelian") : Arr2.Add("a.No_faktur") : arrSfA.Add("No_Faktur")
            'ComboBox2.Items.Add("Kode Perusahaan") : Arr2.Add("a.Kode_Perusahaan_Biaya_Import") : arrSfA.Add("Kode_Perusahaan_Biaya_Import")
            ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.Nama") : arrSfA.Add("Nama")
            ComboBox2.Items.Add("Kode Kategori") : Arr2.Add("a.Kode_Master_Kategori_Biaya_Import") : arrSfA.Add("Kode_Master_Kategori_Biaya_Import")
            ComboBox2.Items.Add("Mata Uang") : Arr2.Add("a.Mata_Uang") : arrSfA.Add("Mata_Uang")
            ComboBox2.Items.Add("Keterangan") : Arr2.Add("a.Keterangan") : arrSfA.Add("Keterangan")


            Cmb_Jenis_Display.SelectedIndex = 1

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        GetData()

    End Sub

    Private Sub Get_Lv_Data(ByVal index As Integer)

        Lv_NoPO = LvHutangBiaya.Items(index).SubItems(item_NoPO).Text
        Lv_TglPO = LvHutangBiaya.Items(index).SubItems(item_TglPO).Text
        Lv_NoPemb = LvHutangBiaya.Items(index).SubItems(item_NoPemb).Text
        Lv_TglPemb = LvHutangBiaya.Items(index).SubItems(item_TglPemb).Text
        Lv_Keterangan = LvHutangBiaya.Items(index).SubItems(item_Keterangan).Text
        Lv_KdPerusahaanBiayaImport = LvHutangBiaya.Items(index).SubItems(item_KdPerusahaanBiayaImport).Text
        Lv_Perusahaan = LvHutangBiaya.Items(index).SubItems(item_Perusahaan).Text
        Lv_KdKategori = LvHutangBiaya.Items(index).SubItems(item_KdKategori).Text
        Lv_NmKategori = LvHutangBiaya.Items(index).SubItems(item_NmKategori).Text
        Lv_MataUang = LvHutangBiaya.Items(index).SubItems(item_MataUang).Text
        Lv_Hutang = LvHutangBiaya.Items(index).SubItems(item_Hutang).Text
        Lv_HutangIDR = LvHutangBiaya.Items(index).SubItems(item_HutangIDR).Text
        Lv_PPN = LvHutangBiaya.Items(index).SubItems(item_PPN).Text
        Lv_PPH = LvHutangBiaya.Items(index).SubItems(item_PPH).Text
        Lv_Pelunasan = LvHutangBiaya.Items(index).SubItems(item_Pelunasan).Text
        Lv_PelunasanIDR = LvHutangBiaya.Items(index).SubItems(item_PelunasanIDR).Text
        Lv_Sisa = LvHutangBiaya.Items(index).SubItems(item_Sisa).Text
        Lv_SisaIDR = LvHutangBiaya.Items(index).SubItems(item_SisaIDR).Text
        Lv_Lokasi = LvHutangBiaya.Items(index).SubItems(item_Lokasi).Text
        Lv_JatuhTempo = LvHutangBiaya.Items(index).SubItems(item_JatuhTempo).Text
        Lv_Jenis = LvHutangBiaya.Items(index).SubItems(item_Jenis).Text
        Lv_PengajuanIDR = LvHutangBiaya.Items(index).SubItems(item_PengajuanIDR).Text

    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            ComboBox2.Enabled = True : TextBox4.Enabled = True
        Else
            ComboBox2.Enabled = False : TextBox4.Enabled = False
            ComboBox2.SelectedIndex = -1 : TextBox4.Text = ""
        End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = -1 Then Exit Sub
        TextBox4.Text = ""
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox3.Enabled = True : Tgl1.Enabled = True : Tgl2.Enabled = True
            CheckBox5.Checked = False
        Else
            ComboBox3.Enabled = False : Tgl1.Enabled = False : Tgl2.Enabled = False
            ComboBox3.SelectedIndex = -1 : Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
        End If
    End Sub

    Private Sub LvHutangBiaya_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvHutangBiaya.SelectedIndexChanged

    End Sub

    Private Sub Cmb_Jenis_Display_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Display.SelectedIndexChanged
        If arr_jenis_display(Cmb_Jenis_Display.SelectedIndex) = "PELUNASAN" Then
            Panel_Status.Visible = False
        Else
            Panel_Status.Visible = True
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            CheckBox1.Checked = False
            BtnRefresh_Click(CheckBox5, e)
        End If
    End Sub

    Private Sub CheckBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox5.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox1.Focus()
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox1.Checked Then
                ComboBox3.DroppedDown = True
                ComboBox3.Focus()
            Else
                CheckBox6.Focus()
            End If

        End If
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then Tgl1.Focus()
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox6.Focus()
    End Sub

    Private Sub CheckBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox6.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox6.Checked Then
                ComboBox2.DroppedDown = True
                ComboBox2.Focus()
            Else
                BtnRefresh.Focus()
            End If

        End If
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox4.Focus()
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then BtnRefresh.Focus()
    End Sub

    Private Sub LvHutangBiaya_DoubleClick(sender As Object, e As EventArgs) Handles LvHutangBiaya.DoubleClick

        If LvHutangBiaya.FocusedItem.Index = -1 Then Exit Sub

        Dim row As Integer = LvHutangBiaya.FocusedItem.Index

        Get_Lv_Data(row)

        Try
            OpenConn()

            SQL = "select isnull(a.no_pengajuan, '-') as no_pengajuan, isnull(a.no_val, '-') as no_pelunasan, b.Tanggal_Bayar, isnull(b.Byr, 0) as Byr, isnull(b.Total_Bayar_Kurs_Baru, 0) as Kurs_Baru, isnull(b.Mata_Uang, '-') as Mata_Uang, "
            SQL = SQL & "isnull(( "
            SQL = SQL & "select "
            SQL = SQL & "case when x.Flag_Pengajuan is null and x.Flag_pelunasan is null then 'BELUM VALIDASI' "
            SQL = SQL & "when x.Flag_Pengajuan = 'Y' and x.Flag_pelunasan is null then 'PENGAJUAN' "
            SQL = SQL & "when x.Flag_Pengajuan = 'Y' and x.Flag_pelunasan = 'Y' then 'PELUNASAN' "
            SQL = SQL & "else 'UNDEFINED' "
            SQL = SQL & "end "
            SQL = SQL & "from Pengajuan_Temp z, Detail_Pengajuan_Temp x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Pengajuan = x.No_Pengajuan "
            SQL = SQL & "and z.No_Pengajuan = a.No_Pengajuan "
            SQL = SQL & "and x.Urut_Pelunasan = b.Urut "
            SQL = SQL & "), 'UNDEFINED') as Status_Proses "
            SQL = SQL & "from EMI_Pelunasan a, EMI_Pelunasan_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Val = b.No_Val "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & Lv_NoPemb & "' "
            SQL = SQL & "and b.Kode_Perusahaan_Biaya_Import = '" & Lv_KdPerusahaanBiayaImport & "' "
            SQL = SQL & "and b.Kode_Master_Kategori_Biaya_Import = '" & Lv_KdKategori & "' "
            SQL = SQL & "order by b.Urut "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count = 0 Then

                        CloseConn()
                        MessageBox.Show("Data Pelunsan Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        EMI_Detail_Hutang_Biaya_Import.Kosong()
        EMI_Detail_Hutang_Biaya_Import.Txt_NoPO.Text = Lv_NoPemb
        EMI_Detail_Hutang_Biaya_Import.Txt_TanggalPO.Text = Lv_TglPO
        EMI_Detail_Hutang_Biaya_Import.Txt_TanggalJatuhTempo.Text = Lv_JatuhTempo
        EMI_Detail_Hutang_Biaya_Import.Txt_Kategori.Text = Lv_NmKategori
        EMI_Detail_Hutang_Biaya_Import.Txt_Perusahaan.Text = Lv_Perusahaan
        EMI_Detail_Hutang_Biaya_Import.Txt_Keterangan.Text = Lv_Keterangan
        EMI_Detail_Hutang_Biaya_Import.Txt_KdPerusahaanBiayaImport.Text = Lv_KdPerusahaanBiayaImport
        EMI_Detail_Hutang_Biaya_Import.Txt_KdMasterKategori.Text = Lv_KdKategori
        EMI_Detail_Hutang_Biaya_Import.Txt_TotalHutang.Text = Lv_Hutang

        EMI_Detail_Hutang_Biaya_Import.Txt_MataUang.Text = Lv_MataUang
        EMI_Detail_Hutang_Biaya_Import.Txt_MataUang2.Text = Lv_MataUang
        EMI_Detail_Hutang_Biaya_Import.Txt_MataUang3.Text = Lv_MataUang

        'EMI_Detail_Hutang_Biaya_Import.Load_Lv()
        EMI_Detail_Hutang_Biaya_Import.ShowDialog()

    End Sub

    Private Sub BtnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCetak.Click
        'If CheckBox1.Checked = False And CheckBox5.Checked = False And CheckBox6.Checked = False Then
        '    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Judul)
        '    CheckBox1.Focus() : Exit Sub
        'End If

        'If CheckBox1.Checked Then
        '    If ComboBox3.SelectedIndex = -1 Then
        '        MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Judul)
        '        ComboBox3.Focus() : Exit Sub
        '    ElseIf Tgl1.Value > Tgl2.Value Then
        '        MessageBox.Show("Periode I " & Base_Language.Lang_Global_TidakBolehLebihDari & " periode II!", Judul)
        '        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
        '        Exit Sub
        '    End If
        'ElseIf CheckBox6.Checked Then
        '    If ComboBox2.Text.Trim.Length = 0 And TextBox4.Text.Trim.Length = 0 Then
        '        MessageBox.Show("Parameter Lainnya harus di isi!")
        '        ComboBox2.Focus()

        '        Exit Sub
        '    End If
        'End If

        Try
            OpenConn()

            Dim SF As String = ""

            '---------- SQL
            SQL = "select a.kode_perusahaan "
            SQL = SQL & "from View_Emi_Pelunasan_Cetak a "
            SQL = SQL & "where a.kode_Perusahaan = '" & KodePerusahaan & "' "
            SF = "{View_Emi_Pelunasan_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            If arr_jenis_display(Cmb_Jenis_Display.SelectedIndex) = "PELUNASAN" Then
                SQL = SQL & "and a.flag_lunas is null and a.Nilai <> 0 "
                SQL = SQL & "and HUtangIDR-PelunasanHutangIDR2 <> 0 "

                SF = SF & "and IsNull({View_Emi_Pelunasan_Cetak.flag_lunas}) and {View_Emi_Pelunasan_Cetak.Nilai} <> 0  "
                SF = SF & "and ((If IsNull({View_Emi_Pelunasan_Cetak.HUtangIDR}) Then 0 Else {View_Emi_Pelunasan_Cetak.HUtangIDR}) - (If IsNull({View_Emi_Pelunasan_Cetak.PelunasanHutangIDR2}) Then 0 Else {View_Emi_Pelunasan_Cetak.PelunasanHutangIDR2}) <> 0 ) "
            End If

            '---------- SF


            'LOKASI
            If ComboBox6.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.lokasi = '" & ComboBox6.Text & "' "

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SF = SF & "AND "

                SF = SF & "{View_Emi_Pelunasan_Cetak.lokasi} = '" & ComboBox6.Text & "' "
            End If

            'TRANSAKSI HARI INI
            If CheckBox5.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & " a.Tanggal_PO between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SF = SF & "AND "

                SF = SF & " {View_Emi_Pelunasan_Cetak.Tanggal_PO} "
                SF = SF & " >=date('" & Format(Now, "yyyy-MM-dd") & "') "
                SF = SF & " and {View_Emi_Pelunasan_Cetak.Tanggal_PO} "
                SF = SF & " <=date('" & Format(Now, "yyyy-MM-dd") & "') "
            End If

            'Tanggal
            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr1.Item(ComboBox3.SelectedIndex) & " between '"
                SQL = SQL & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SF = SF & "AND "

                SF = SF & " {View_Emi_Pelunasan_Cetak." & arrSfB.Item(ComboBox3.SelectedIndex) & "} "
                SF = SF & " >=date('" & Format(Tgl1.Value, "yyyy-MM-dd") & "') "
                SF = SF & " and {View_Emi_Pelunasan_Cetak." & arrSfB.Item(ComboBox3.SelectedIndex) & "} "
                SF = SF & " <=date('" & Format(Tgl2.Value, "yyyy-MM-dd") & "') "
            End If

            'Param Lain
            If CheckBox6.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "

                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SF = SF & "AND "

                SF = SF & " ToText({View_Emi_Pelunasan_Cetak." & arrSfA.Item(ComboBox2.SelectedIndex) & "}) like '*" & Trim(TextBox4.Text) & "*' "
            End If

            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    Dim CrDoc As New Laporan_Emi_Pelunasan_Hutang    'Nama file CR
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = SF
                    With A_Place_For_Printing2
                        .Text = "Laporan Hutang Per PO "
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                        .Refresh()
                        .Show()
                    End With
                Else
                    MessageBox.Show("Data tidak ada . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click

        If CheckBox1.Checked = False And CheckBox5.Checked = False And CheckBox6.Checked = False Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Judul)
            CheckBox1.Focus() : Exit Sub
        End If

        If CheckBox1.Checked Then
            If ComboBox3.SelectedIndex = -1 Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Judul)
                ComboBox3.Focus() : Exit Sub
            ElseIf Tgl1.Value > Tgl2.Value Then
                MessageBox.Show("Periode I " & Base_Language.Lang_Global_TidakBolehLebihDari & " periode II!", Judul)
                Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
                Exit Sub
            End If
        ElseIf CheckBox6.Checked Then
            If ComboBox2.Text.Trim.Length = 0 Then
                MessageBox.Show("Parameter Lainnya harus di isi!")
                ComboBox2.Focus() : TextBox4.Text = ""
                Exit Sub
            End If

            If TextBox4.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Tidak Boleh Kosong!")
                TextBox4.Focus()
                Exit Sub
            End If
        End If

        GetData(True)

    End Sub

    Private Sub GetData(ByVal Optional Filter As Boolean = False)

        Try
            OpenConn()

            LvHutangBiaya.Items.Clear()

            SQL = "select a.kode_Perusahaan, a.No_PO, a.Tanggal_PO, a.No_faktur as No_Pembelian, Tanggal_Pembelian, a.Keterangan, a.Kode_Perusahaan_Biaya_Import, a.Nama as Perusahaan, a.Kode_Master_Kategori_Biaya_Import, a.Nama_Kategori, "
            SQL = SQL & "a.Mata_uang, a.ppn as persenPPN, a.pph as persenPPH, "
            SQL = SQL & "Hutang, HUtangIDR, "
            SQL = SQL & "Nilai_PPN as PPN, Nilai_PPH as PPH, "
            SQL = SQL & "isnull(a.Pelunasan_hutang,0) as Pelunasan, isnull(a.PelunasanHutangIDR1,0) as PengajuanIDR, isnull(a.PelunasanHutangIDR2,0) as PelunasanIDR,"
            SQL = SQL & ""
            SQL = SQL & "Hutang-isnull(a.Pelunasan_hutang,0) as Sisa, "
            SQL = SQL & "HutangIDR-isnull(a.PelunasanHutangIDR1+a.PelunasanHutangIDR2,0) as SisaIDR, "
            SQL = SQL & "a.lokasi, a.Tgl_Jatuh_Tempo, a.Jenis1, "

            SQL = SQL & "case when (HUtangIDR-PelunasanHutangIDR2) <> 0 then 'BELUM LUNAS' "
            SQL = SQL & "when (HUtangIDR-PelunasanHutangIDR2) = 0 then 'LUNAS' "
            SQL = SQL & "when (HUtangIDR-PelunasanHutangIDR2) < 0 then 'NILAI MINUS' "
            SQL = SQL & "else 'TIDAK DIKETAHUI' "
            SQL = SQL & "end as Status_Pelunsan "

            SQL = SQL & "from View_EMI_Pelunasan a "
            SQL = SQL & "where a.kode_Perusahaan = '" & KodePerusahaan & "' "

            If arr_jenis_display(Cmb_Jenis_Display.SelectedIndex) = "PELUNASAN" Then
                SQL = SQL & "and (HUtangIDR-PelunasanHutangIDR2) <> 0 "
            End If

            If Filter Then

                'LOKASI
                If ComboBox6.SelectedIndex <> -1 Then
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & " a.lokasi = '" & ComboBox6.Text & "' "
                End If

                'TRANSAKSI HARI INI
                If CheckBox5.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & " a.Tanggal_PO between '"
                    SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
                End If

                'Tanggal
                If CheckBox1.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & Arr1.Item(ComboBox3.SelectedIndex) & " between '"
                    SQL = SQL & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                End If

                'Param Lain
                If CheckBox6.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "
                End If

            End If

            SQL = SQL & "order by a.Tanggal_PO"

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem

                    lv = LvHutangBiaya.Items.Add(Dr("No_PO"))
                    lv.SubItems.Add(Format(Dr("Tanggal_PO"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("No_Pembelian"))
                    lv.SubItems.Add(Format(Dr("Tanggal_Pembelian"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("Keterangan"))
                    lv.SubItems.Add(Dr("Kode_Perusahaan_Biaya_Import"))
                    lv.SubItems.Add(Dr("Perusahaan"))
                    lv.SubItems.Add(Dr("Kode_Master_Kategori_Biaya_Import"))
                    lv.SubItems.Add(Dr("Nama_Kategori"))
                    lv.SubItems.Add(Dr("Mata_uang"))

                    lv.SubItems.Add(Format(Dr("Hutang"), "N2"))
                    lv.SubItems.Add(Format(Dr("HutangIDR"), "N2"))
                    lv.SubItems.Add(Format(Dr("PPN"), "N0"))
                    lv.SubItems.Add(Format(Dr("PPH"), "N0"))
                    lv.SubItems.Add(Format(Dr("Pelunasan"), "N2"))
                    lv.SubItems.Add(Format(Dr("PelunasanIDR"), "N0"))
                    lv.SubItems.Add(Format(Dr("sisa"), "N2"))
                    lv.SubItems.Add(Format(Dr("sisaIDR"), "N0"))
                    lv.SubItems.Add(Dr("lokasi"))
                    lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tgl_Jatuh_Tempo")) = "", "-", Format(Dr("Tgl_Jatuh_Tempo"), "dd MMM yyyy")))
                    lv.SubItems.Add(Dr("Jenis1"))
                    lv.SubItems.Add(Format(Dr("PengajuanIDR"), "N0"))


                    If Dr("Status_Pelunsan").ToString.Trim = "LUNAS" Then
                        lv.BackColor = Color.LightGreen
                    ElseIf Dr("Status_Pelunsan").ToString.Trim = "NILAI MINUS" Then
                        lv.BackColor = Color.DarkRed
                        lv.ForeColor = Color.White
                    ElseIf Dr("Status_Pelunsan").ToString.Trim = "TIDAK DIKETAHUI" Then
                        lv.BackColor = Color.LightGray
                    End If

                    'If Sisa <> 0 Then
                    '    lv.BackColor = Color.LightYellow
                    'ElseIf Sisa = 0 Then
                    '    lv.BackColor = Color.LightGreen
                    'End If

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub LvHutangBiaya_DpiChangedBeforeParent(sender As Object, e As EventArgs) Handles LvHutangBiaya.DpiChangedBeforeParent

    End Sub
End Class