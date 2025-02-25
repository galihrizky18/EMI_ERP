Public Class Display_Emi_Pelunasan_Hutang

    Dim lv As New ListViewItem

    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList
    Dim arrSfA, arrSfB As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Dim Lv_NoPO, Lv_TglPO, Lv_Keterangan, Lv_KdPerusahaanBiayaImport, Lv_Perusahaan, Lv_KdKategori, Lv_NmKategori, Lv_MataUang As String
    Dim Lv_DPP, Lv_PPN, Lv_PPH, Lv_Total, Lv_Pelunasan, Lv_Sisa, Lv_Lokasi, Lv_JatuhTempo, Lv_Jenis As String

    Dim item_NoPO As Integer = 0
    Dim item_TglPO As Integer = 1
    Dim item_Keterangan As Integer = 2
    Dim item_KdPerusahaanBiayaImport As Integer = 3
    Dim item_Perusahaan As Integer = 4
    Dim item_KdKategori As Integer = 5
    Dim item_NmKategori As Integer = 6
    Dim item_MataUang As Integer = 7
    Dim item_DPP As Integer = 8
    Dim item_PPN As Integer = 9
    Dim item_PPH As Integer = 10
    Dim item_Total As Integer = 11
    Dim item_Pelunasan As Integer = 12
    Dim item_Sisa As Integer = 13
    Dim item_Lokasi As Integer = 14
    Dim item_JatuhTempo As Integer = 15
    Dim item_Jenis As Integer = 16

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
        LvHutangBiaya.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '2
        LvHutangBiaya.Columns.Add("KdPerusahanBiayaImport", 0, HorizontalAlignment.Left) '3
        LvHutangBiaya.Columns.Add("Perusahaan", 180, HorizontalAlignment.Left) '4
        LvHutangBiaya.Columns.Add("KdKategori", 0, HorizontalAlignment.Left) '5
        LvHutangBiaya.Columns.Add("Kategori", 150, HorizontalAlignment.Left) '6
        LvHutangBiaya.Columns.Add("Mata Uang", 90, HorizontalAlignment.Center) '7
        LvHutangBiaya.Columns.Add("DPP", 150, HorizontalAlignment.Right) '8
        LvHutangBiaya.Columns.Add("PPN", 150, HorizontalAlignment.Right) '9
        LvHutangBiaya.Columns.Add("PPH", 150, HorizontalAlignment.Right) '10
        LvHutangBiaya.Columns.Add("Total", 150, HorizontalAlignment.Right) '11
        LvHutangBiaya.Columns.Add("Pelunasan", 150, HorizontalAlignment.Right) '12
        LvHutangBiaya.Columns.Add("Sisa", 150, HorizontalAlignment.Right) '13
        'HIDE
        LvHutangBiaya.Columns.Add("Lokasi", 0, HorizontalAlignment.Right) '14
        LvHutangBiaya.Columns.Add("Jatuh Tempo", 120, HorizontalAlignment.Center).DisplayIndex = 2 '15
        LvHutangBiaya.Columns.Add("Jenis", 0, HorizontalAlignment.Left) '16

        LvHutangBiaya.View = View.Details

        ' get_lokasi()

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
            ComboBox2.Items.Add("Kode Perusahaan") : Arr2.Add("a.Kode_Perusahaan_Biaya_Import") : arrSfA.Add("Kode_Perusahaan_Biaya_Import")
            ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.Kode_Perusahaan_Biaya_Import") : arrSfA.Add("Kode_Perusahaan_Biaya_Import")
            ComboBox2.Items.Add("Mata Uang") : Arr2.Add("a.Mata_Uang") : arrSfA.Add("Mata_Uang")
            ComboBox2.Items.Add("Keterangan") : Arr2.Add("a.Keterangan") : arrSfA.Add("Keterangan")

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
        Lv_Keterangan = LvHutangBiaya.Items(index).SubItems(item_Keterangan).Text
        Lv_KdPerusahaanBiayaImport = LvHutangBiaya.Items(index).SubItems(item_KdPerusahaanBiayaImport).Text
        Lv_Perusahaan = LvHutangBiaya.Items(index).SubItems(item_Perusahaan).Text
        Lv_KdKategori = LvHutangBiaya.Items(index).SubItems(item_KdKategori).Text
        Lv_NmKategori = LvHutangBiaya.Items(index).SubItems(item_NmKategori).Text
        Lv_MataUang = LvHutangBiaya.Items(index).SubItems(item_MataUang).Text
        Lv_DPP = LvHutangBiaya.Items(index).SubItems(item_DPP).Text
        Lv_PPN = LvHutangBiaya.Items(index).SubItems(item_PPN).Text
        Lv_PPH = LvHutangBiaya.Items(index).SubItems(item_PPH).Text
        Lv_Total = LvHutangBiaya.Items(index).SubItems(item_Total).Text
        Lv_Pelunasan = LvHutangBiaya.Items(index).SubItems(item_Pelunasan).Text
        Lv_Sisa = LvHutangBiaya.Items(index).SubItems(item_Sisa).Text
        Lv_Lokasi = LvHutangBiaya.Items(index).SubItems(item_Lokasi).Text
        Lv_JatuhTempo = LvHutangBiaya.Items(index).SubItems(item_JatuhTempo).Text
        Lv_Jenis = LvHutangBiaya.Items(index).SubItems(item_Jenis).Text

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
        Else
            ComboBox3.Enabled = False : Tgl1.Enabled = False : Tgl2.Enabled = False
            ComboBox3.SelectedIndex = -1
        End If
    End Sub

    Private Sub LvHutangBiaya_DoubleClick(sender As Object, e As EventArgs) Handles LvHutangBiaya.DoubleClick

        If LvHutangBiaya.FocusedItem.Index = -1 Then Exit Sub

        Dim row As Integer = LvHutangBiaya.FocusedItem.Index

        Get_Lv_Data(row)

        EMI_Detail_Hutang_Biaya_Import.Kosong()
        EMI_Detail_Hutang_Biaya_Import.Txt_NoPO.Text = Lv_NoPO
        EMI_Detail_Hutang_Biaya_Import.Txt_TanggalPO.Text = Lv_TglPO
        EMI_Detail_Hutang_Biaya_Import.Txt_TanggalJatuhTempo.Text = Lv_JatuhTempo
        EMI_Detail_Hutang_Biaya_Import.Txt_Kategori.Text = Lv_NmKategori
        EMI_Detail_Hutang_Biaya_Import.Txt_Perusahaan.Text = Lv_Perusahaan
        EMI_Detail_Hutang_Biaya_Import.Txt_Keterangan.Text = Lv_Keterangan
        EMI_Detail_Hutang_Biaya_Import.Txt_KdPerusahaanBiayaImport.Text = Lv_KdPerusahaanBiayaImport
        EMI_Detail_Hutang_Biaya_Import.Txt_KdMasterKategori.Text = Lv_KdKategori
        EMI_Detail_Hutang_Biaya_Import.Txt_TotalHutang.Text = Lv_Total

        EMI_Detail_Hutang_Biaya_Import.Txt_MataUang.Text = Lv_MataUang
        EMI_Detail_Hutang_Biaya_Import.Txt_MataUang2.Text = Lv_MataUang
        EMI_Detail_Hutang_Biaya_Import.Txt_MataUang3.Text = Lv_MataUang

        EMI_Detail_Hutang_Biaya_Import.Load_Lv()
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
            SQL = SQL & "and a.flag_lunas is null and a.Nilai <> 0 "

            '---------- SF
            SF = "{View_Emi_Pelunasan_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
            SF = SF & "IsNull({View_Emi_Pelunasan_Cetak.flag_lunas}) and {View_Emi_Pelunasan_Cetak.Nilai} <> 0  "

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

            SQL = "select a.kode_Perusahaan, a.No_PO, a.Tanggal_PO, a.Keterangan, a.Kode_Perusahaan_Biaya_Import, a.Nama as Perusahaan, a.Kode_Master_Kategori_Biaya_Import, a.Nama_Kategori, "
            SQL = SQL & "a.Mata_uang, ISNULL(a.Nilai, 0) as DPP, a.ppn as persenPPN, a.pph as persenPPH, "
            SQL = SQL & "ISNULL(( (ISNULL(a.Nilai, 0) * isnull(a.PPN, 0) / 100) ), 0) as PPN, "
            SQL = SQL & "ISNULL(( (ISNULL(a.Nilai, 0) * isnull(a.PPH, 0) / 100) ), 0) as PPH, "
            SQL = SQL & "ISNULL(( (a.Nilai + ISNULL(((ISNULL(a.Nilai, 0) * isnull(a.PPN, 0) / 100)), 0)) - ISNULL(((ISNULL(a.Nilai, 0) * isnull(a.PPH, 0) / 100)), 0) ),0) as total, "
            SQL = SQL & "ISNULL(( (a.sudah_bayar + ISNULL(((ISNULL(a.Nilai, 0) * isnull(a.PPN, 0) / 100)), 0)) - ISNULL(((ISNULL(a.Nilai, 0) * isnull(a.PPH, 0) / 100)), 0) ), 0) as Pelunasan,"

            SQL = SQL & "ISNULL(( ISNULL(((a.Nilai + ISNULL(((ISNULL(a.Nilai, 0) * isnull(a.PPN, 0) / 100)), 0)) -ISNULL(((ISNULL(a.Nilai, 0) * isnull(a.PPH, 0) / 100)), 0)),0) - "
            SQL = SQL & "ISNULL(((a.sudah_bayar + ISNULL(((ISNULL(a.Nilai, 0) * isnull(a.PPN, 0) / 100)), 0)) -ISNULL(((ISNULL(a.Nilai, 0) * isnull(a.PPH, 0) / 100)), 0)), 0) ),0) as Sisa, "

            SQL = SQL & "a.lokasi, a.Tgl_Jatuh_Tempo, a.Jenis1 "
            SQL = SQL & "from View_EMI_Pelunasan a "
            SQL = SQL & "where a.kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.flag_lunas is null and a.Nilai <> 0 "

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
                    lv.SubItems.Add(Dr("Keterangan"))
                    lv.SubItems.Add(Dr("Kode_Perusahaan_Biaya_Import"))
                    lv.SubItems.Add(Dr("Perusahaan"))
                    lv.SubItems.Add(Dr("Kode_Master_Kategori_Biaya_Import"))
                    lv.SubItems.Add(Dr("Nama_Kategori"))
                    lv.SubItems.Add(Dr("Mata_uang"))
                    lv.SubItems.Add(Format(Dr("DPP"), "N2"))
                    lv.SubItems.Add(Format(Dr("PPN"), "N2"))
                    lv.SubItems.Add(Format(Dr("PPH"), "N2"))
                    lv.SubItems.Add(Format(Dr("total"), "N2"))
                    lv.SubItems.Add(Format(Dr("Pelunasan"), "N2"))
                    lv.SubItems.Add(Format(Dr("Sisa"), "N2"))
                    lv.SubItems.Add(Dr("lokasi"))
                    lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tgl_Jatuh_Tempo")) = "", "-", Format(Dr("Tgl_Jatuh_Tempo"), "dd MMM yyyy")))
                    lv.SubItems.Add(Dr("Jenis1"))


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

End Class