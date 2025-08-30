Public Class Display_Hasil_Quality_Control
    Dim Jenis = "Display_Inquiry"
    Dim arr_tgl, arr_Lain As New ArrayList

    Dim lvKd_SO As String
    Dim lvKd_Brg As String
    Dim lvNm_Brg As String
    Dim lvNoLoading As String

    Dim CrDoc As Object

    Private Sub get_isi_listview(ByVal index As Integer)
        lvKd_SO = ListView2.Items(index).SubItems(0).Text
        lvKd_Brg = ListView2.Items(index).SubItems(1).Text
        lvNm_Brg = ListView2.Items(index).SubItems(2).Text
        lvNoLoading = ListView2.Items(index).SubItems(3).Text
    End Sub

    Private Sub Display_Hasil_QC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            'Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            ListView1.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
            ListView1.Columns.Add("Kode Supplier", 100, HorizontalAlignment.Center)
            ListView1.Columns.Add("Nama Supplier", 200, HorizontalAlignment.Left)
            ListView1.Columns.Add("Lokasi", 110, HorizontalAlignment.Center)
            ListView1.Columns.Add("No Surat Jalan", 150, HorizontalAlignment.Left)
            ListView1.Columns.Add("No Plat", 110, HorizontalAlignment.Center)
            ListView1.Columns.Add("Driver", 130, HorizontalAlignment.Left)
            ListView1.Columns.Add("Tgl Masuk", 100, HorizontalAlignment.Center)
            ListView1.Columns.Add("Jam Masuk", 90, HorizontalAlignment.Center)
            ListView1.View = View.Details

            ListView2.Columns.Add("Kode Stock Owner", 120, HorizontalAlignment.Left) '0
            ListView2.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '1
            ListView2.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left) '2
            ListView2.Columns.Add("No Loading", 0, HorizontalAlignment.Left) '3
            ListView2.View = View.Details

            ListView3.Columns.Add("Tanggal", 100, HorizontalAlignment.Center) '0
            ListView3.Columns.Add("Jam", 90, HorizontalAlignment.Center) '1
            ListView3.Columns.Add("UserID", 100, HorizontalAlignment.Center) '2
            ListView3.Columns.Add("Keterangan", 180, HorizontalAlignment.Left) '3
            ListView3.Columns.Add("Warna", 100, HorizontalAlignment.Center) '4
            ListView3.Columns.Add("Jenis QC", 90, HorizontalAlignment.Center) '5
            ListView3.Columns.Add("Step", 80, HorizontalAlignment.Center) '6
            ListView3.Columns.Add("No Faktur Hasil QC", 0, HorizontalAlignment.Center) '7
            ListView3.View = View.Details

            ListView1.Items.Clear() : ListView2.Items.Clear() : ListView3.Items.Clear()
            SQL = "select a.no_faktur,a.kode_supplier,b.Nama,a.lokasi,a.no_sj,a.No_Plat,a.driver,a.tanggal_masuk,a.Jam_Masuk "
            SQL = SQL & "from emi_pembelian_loading a,Suppliers b where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier and a.Status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("no_faktur"))
                    Lvw.SubItems.Add(dr("kode_supplier"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(If(General_Class.CekNULL(dr("lokasi")) = "", "-", dr("lokasi")))
                    Lvw.SubItems.Add(dr("no_sj"))
                    Lvw.SubItems.Add(If(General_Class.CekNULL(dr("No_Plat")) = "", "-", dr("No_Plat")))
                    Lvw.SubItems.Add(If(General_Class.CekNULL(dr("driver")) = "", "-", dr("driver")))
                    If General_Class.CekNULL(dr("tanggal_masuk")) = "" Then
                        Lvw.SubItems.Add("")
                    Else
                        Lvw.SubItems.Add(Format(dr("tanggal_masuk"), "dd-MM-yyyy"))
                    End If
                    If General_Class.CekNULL(dr("Jam_Masuk")) = "" Then
                        Lvw.SubItems.Add("")
                    Else
                        Lvw.SubItems.Add(dr("Jam_Masuk"))
                    End If
                Loop
            End Using

            CheckBox3.Checked = False
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            ComboBox3.Enabled = False
            ComboBox3.SelectedIndex = -1
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            ComboBox2.Enabled = False
            TextBox4.Enabled = False
            TextBox4.Text = ""
            DateTimePicker1.Value = Now
            DateTimePicker2.Value = Now

            arr_tgl.Clear() : ComboBox3.Items.Clear()
            ComboBox3.Items.Add("Tgl Masuk") : arr_tgl.Add("a.tanggal_masuk")

            arr_Lain.Clear() : ComboBox2.Items.Clear()
            ComboBox2.Items.Add("No Faktur") : arr_Lain.Add("a.no_faktur")
            ComboBox2.Items.Add("Kode Supplier") : arr_Lain.Add("a.kode_supplier")
            ComboBox2.Items.Add("Nama Supplier") : arr_Lain.Add("b.nama")
            ComboBox2.Items.Add("No Surat Jalan") : arr_Lain.Add("a.No_Sj")
            ComboBox2.Items.Add("No Plat") : arr_Lain.Add("a.No_Plat")
            ComboBox2.Items.Add("Driver") : arr_Lain.Add("a.Driver")

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add("-- Seluruh --")

            xSplit = CekKotaRole().Split(",")

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
            End Using

            ComboBox6.Text = Lokasi

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CheckBox3.Focus()

    End Sub

    Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            ComboBox3.SelectedIndex = -1
            ComboBox3.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            DateTimePicker1.Value = Now
            DateTimePicker2.Value = Now
            button1_Click(CheckBox3, e)
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox3.Checked = False
            ComboBox3.Enabled = True
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        Else
            ComboBox3.SelectedIndex = -1
            ComboBox3.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            DateTimePicker1.Value = Now
            DateTimePicker2.Value = Now
        End If
    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Base_Language.Lang_Global_Perhatian)
            CheckBox1.Focus() : Exit Sub
        End If

        If CheckBox1.Checked Then
            If ComboBox3.SelectedIndex = -1 Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Base_Language.Lang_Global_Perhatian)
                ComboBox3.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl2, Base_Language.Lang_Global_Perhatian)
                DateTimePicker1.Value = Now : DateTimePicker2.Value = Now
                Exit Sub
            End If

            If CheckBox2.Checked Then
                If ComboBox2.SelectedIndex = -1 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain, Base_Language.Lang_Global_Perhatian)
                    ComboBox2.Focus() : Exit Sub
                ElseIf TextBox4.Text.Trim.Length = 0 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Base_Language.Lang_Global_Perhatian)
                    TextBox4.Focus() : Exit Sub
                End If
            End If
        End If

        Try
            OpenConn()

            ListView1.Items.Clear() : ListView2.Items.Clear() : ListView3.Items.Clear()
            SQL = "select a.no_faktur,a.kode_supplier,b.Nama,a.lokasi,a.no_sj,a.No_Plat,a.driver,a.tanggal_masuk,a.Jam_Masuk "
            SQL = SQL & "from emi_pembelian_loading a,Suppliers b where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier and a.Status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and a.lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and a.lokasi = '" & ComboBox6.Text & "'"
            End If

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.tanggal_masuk between '"
                SQL = SQL & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' and '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            End If

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arr_tgl.Item(ComboBox3.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                If ComboBox2.SelectedIndex = -1 Then
                    CloseConn()
                    MessageBox.Show("Parameter Tidak Boleh Kosong", "Display Hasil Quality Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arr_Lain.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "
            End If

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("no_faktur"))
                    Lvw.SubItems.Add(dr("kode_supplier"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(If(General_Class.CekNULL(dr("lokasi")) = "", "-", dr("lokasi")))
                    Lvw.SubItems.Add(dr("no_sj"))
                    Lvw.SubItems.Add(If(General_Class.CekNULL(dr("No_Plat")) = "", "-", dr("No_Plat")))
                    Lvw.SubItems.Add(If(General_Class.CekNULL(dr("driver")) = "", "-", dr("driver")))
                    If General_Class.CekNULL(dr("tanggal_masuk")) = "" Then
                        Lvw.SubItems.Add("")
                    Else
                        Lvw.SubItems.Add(Format(dr("tanggal_masuk"), "dd-MM-yyyy"))
                    End If
                    If General_Class.CekNULL(dr("Jam_Masuk")) = "" Then
                        Lvw.SubItems.Add("")
                    Else
                        Lvw.SubItems.Add(dr("Jam_Masuk"))
                    End If
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            ComboBox2.Enabled = True
            TextBox4.Enabled = True
        Else
            ComboBox2.Enabled = False
            TextBox4.Enabled = False
            ComboBox2.SelectedIndex = -1
            TextBox4.Text = ""
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.Items.Count = 0 Or ListView1.FocusedItem Is Nothing Then Exit Sub
        Try
            OpenConn()

            ListView2.Items.Clear() : ListView3.Items.Clear()
            SQL = "select a.kode_stock_owner, a.kode_barang, b.nama "
            SQL = SQL & "from emi_pembelian_loading_detail a,Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan  "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & ListView1.FocusedItem.Text & "' "
            SQL = SQL & "group by a.kode_stock_owner,a.kode_barang,b.nama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView2.Items.Add(dr("kode_stock_owner"))
                    Lvw.SubItems.Add(dr("kode_barang"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(ListView1.FocusedItem.Text)

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CetakHasilToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakHasilToolStripMenuItem.Click
        If ListView2.Items.Count = 0 Then Exit Sub

        If ListView2.Items.Count = 0 Or ListView2.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu data yang akan dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()


            '====================================
            '=     CEK APAKAH SUDAH FLAG QC     =
            '====================================
            get_isi_listview(ListView2.FocusedItem.Index)
            SQL = "select Status, no_fak_loading_barang from emi_hasil_quality_control "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_fak_loading_barang = '" & ListView1.FocusedItem.Text & "' "
            SQL = SQL & "and Kode_Barang = '" & lvKd_Brg & "' "
            SQL = SQL & "and Flag_Sudah_QC_Dekstop is null "
            SQL = SQL & "and status is null "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    CloseConn()
                    MessageBox.Show("Tidak Bisa Cetak Ulang Karena Data Belum Selesai QC Desktop", "Cetak Ulang", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            Dim CrDoc As New Object
            Dim kertas As String = ""

            Dim SF As String = ""
            SQL = "select Kode_Perusahaan from View_Laporan_Hasil_QC where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Fak_Loading_Barang = '" & ListView1.FocusedItem.Text & "' "
            SQL = SQL & "and kode_barang = '" & ListView2.FocusedItem.SubItems(1).Text & "' "

            SF = "{View_Laporan_Hasil_QC.No_Fak_Loading_Barang} = '" & ListView1.FocusedItem.Text & "' "
            SF = SF & "and {View_Laporan_Hasil_QC.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {View_Laporan_Hasil_QC.Kode_Barang} = '" & ListView2.FocusedItem.SubItems(1).Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New Rpt_Laporan_Hasil_QC

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = SF
                    '    'CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
                    '    .Text = "Laporan Hasil QC"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    '.CrystalReportViewer1.DisplayGroupTree = False
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '============================================================================================================================================
                    '============================================================================================================================================

                    kertas = "A4"

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterQC
                    CrDoc.RecordSelectionFormula = SF
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterQC
                    'doctoprint.DefaultPageSettings.Landscape = True
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 99)

                    MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    CloseConn()
                    MessageBox.Show("Data Tidak diTemukan", "Cetak Ulang", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub

                End If
            End Using

            A_Place_For_Printing2.Focus()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView2.SelectedIndexChanged
        If ListView2.Items.Count = 0 Or ListView2.FocusedItem Is Nothing Then Exit Sub
        Try
            OpenConn()

            get_isi_listview(ListView2.FocusedItem.Index)
            ListView3.Items.Clear()
            SQL = "select a.tanggal,a.jam,a.userid,a.keterangan,a.warna,a.jenis_qc,a.step,a.no_faktur, a.status "
            SQL = SQL & "from emi_hasil_quality_control a where  a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_fak_loading_barang = '" & ListView1.FocusedItem.Text & "' "
            'SQL = SQL & "and a.Kode_stock_owner = '" & lvKd_SO & "' "
            SQL = SQL & "and a.Kode_Barang = '" & lvKd_Brg & "' order by a.step"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView3.Items.Add(Format(dr("tanggal"), "dd-MM-yyyy"))
                    Lvw.SubItems.Add(dr("jam"))
                    Lvw.SubItems.Add(dr("userid"))
                    Lvw.SubItems.Add(If(General_Class.CekNULL(dr("keterangan")) = "-", "-", dr("keterangan")))
                    Lvw.SubItems.Add(If(General_Class.CekNULL(dr("warna")) = "", "-", dr("warna")))
                    Lvw.SubItems.Add(If(General_Class.CekNULL(dr("jenis_qc")) = "", "-", dr("jenis_qc")))
                    Lvw.SubItems.Add(If(General_Class.CekNULL(dr("step")) = "", "-", dr("step")))
                    Lvw.SubItems.Add(dr("no_faktur"))

                    If General_Class.CekNULL(dr("status")) = "Y" Then
                        Lvw.BackColor = Color.DarkRed
                        Lvw.ForeColor = Color.White
                    End If
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    '=============================================================================================================================================
    '=     CONTEXT MENU
    '=============================================================================================================================================
    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Or ListView1.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(ListView1.FocusedItem.Text)

    End Sub

    Private Sub PembatalanQC1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembatalanQC1ToolStripMenuItem.Click
        If ListView2.Items.Count = 0 Or ListView2.FocusedItem Is Nothing Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Quality Control 1"

            get_isi_listview(ListView2.FocusedItem.Index)

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_QC_1") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Quality Control 1", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Quality Control ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim NoLoading As String = lvNoLoading

            '=========================================
            '=     CEK APAKAH LOADING DIBATALKAN     =
            '=========================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan QC tidak dapat dilakukan karena No Loading Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim NoFakturQC As String = ""
            '=========================
            '=     GET NO FAKTUR     =
            '=========================
            SQL = "select No_Faktur from EMI_Hasil_Quality_Control "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Fak_Loading_Barang = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' "
            SQL = SQL & "and Jenis_QC = '1' and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    NoFakturQC = Dr("No_Faktur")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("PData QC Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '==========================================
            '=     CEK APAKAH QC SUDAH DIBATALKAN     =
            '==========================================
            SQL = "select Kode_Perusahaan from EMI_Hasil_Quality_Control "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Fak_Loading_Barang = '" & NoLoading & "' "
            SQL = SQL & "and Jenis_QC = '1' and Kode_Barang = '" & lvKd_Brg & "' and status = 'Y' and No_Faktur = '" & NoFakturQC & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan QC tidak dapat dilakukan karena QC Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================================
            '=     CEK DATA REGISTRASI KENDARAAN     =
            '=========================================
            SQL = "select a.Kode_Perusahaan, a.Status "
            SQL = SQL & "from EMI_Register_Kendaraan_BM a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Fak_Loading_Barang = b.No_Faktur "
            SQL = SQL & "and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_Loading_Barang = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pembatalan QC 1 tidak dapat dilakukan karena Data Registerasi Kendaraan Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan QC 1 tidak dapat dilakukan karena No Loading Belum Melalui Proses Registerasi Kendaraan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=======================================================
            '=     CEK APAKAH DATA SUDAH DIBATALKAN SEBELUMNYA     =
            '=======================================================
            SQL = "select a.Status as Status_QC, b.Status as Status_Loading "
            SQL = SQL & "from EMI_Hasil_Quality_Control a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Fak_Loading_Barang = b.No_Faktur "
            SQL = SQL & "and a.Jenis_QC = '1' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_Loading_Barang = '" & NoLoading & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoFakturQC & "' "
            SQL = SQL & "and a.Kode_Barang = '" & lvKd_Brg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status_Loading")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No Loading Telah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

                    ElseIf General_Class.CekNULL(Dr("Status_QC")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("QC 1 Telah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data QC Tidak Ditemukan atau Data Tidak Berada pada Step QC 1", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==================================
            '=     CEK DATA TIMBANG MASUK     =
            '==================================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Loading = b.No_Faktur "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan QC 1 tidak dapat dilakukan karena Data sudah melewati proses Timbang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==============================
            '=     ROLLBACK DATA QC 1     =
            '==============================
            SQL = "select a.No_Faktur "
            SQL = SQL & "from EMI_Hasil_Quality_Control a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Fak_Loading_Barang = b.No_Faktur "
            SQL = SQL & "and a.status is null and b.status is null "
            SQL = SQL & "and a.Jenis_QC = '1' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_Loading_Barang = '" & NoLoading & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoFakturQC & "' "
            SQL = SQL & "and a.Kode_Barang = '" & lvKd_Brg & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        'RollBack Detail QC
                        'SQL = "update EMI_Hasil_Detail_Quality_Control "
                        'SQL = SQL & "set value_kode_uji = NULL, Warna = NULL, Keterangan = NULL "
                        'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & .Rows(0).Item("No_Faktur") & "' "
                        'ExecuteTrans(SQL)

                        ''RollBack Switch QC
                        'SQL = "update EMI_Hasil_Detail_Switch_QC "
                        'SQL = SQL & "set value_kode_uji = NULL, Warna = NULL, Keterangan = NULL "
                        'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & .Rows(0).Item("No_Faktur") & "' "
                        'ExecuteTrans(SQL)
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data QC Tidak Ditemukan atau Data Tidak Berada pada Step QC 1", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            'Update EMI_Hasil_Quality_Control
            SQL = "select Kode_Perusahaan from EMI_Hasil_Quality_Control "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Jenis_QC = '1' "
            SQL = SQL & "and No_Fak_Loading_Barang = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' and No_Faktur = '" & NoFakturQC & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Hasil_Quality_Control "
                    SQL = SQL & "set Status = 'Y', UserID_Batal = '" & UserID & "', Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Jenis_QC = '1' "
                    SQL = SQL & "and No_Fak_Loading_Barang = '" & NoLoading & "'"
                    SQL = SQL & "and No_Faktur = '" & NoFakturQC & "' "
                    SQL = SQL & "and Kode_Barang = '" & lvKd_Brg & "' "
                    ExecuteTrans(SQL)

                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data QC Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            'CEK APAKAH REFRAKSI
            SQL = "Select b.Flag_Refraksi, b.Harga_Refraksi, a.urut_oto, a.urut_Po, b.no_faktur from "
            SQL = SQL & "EMI_Pembelian_Loading_detail a, EMI_Pembelian_PO_Detail b where "
            SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.urut_Po = b.No_Urut "
            SQL = SQL & "And a.no_faktur='" & NoLoading & "' and a.kode_barang='" & lvKd_Brg & "' "
            SQL = SQL & " and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For index = 0 To .Rows.Count - 1

                            If General_Class.CekNULL(.Rows(index).Item("Flag_Refraksi")) = "Y" Then

                                SQL = "update EMI_Pembelian_Loading_detail set flag_refraksi = NULL, "
                                SQL = SQL & "Harga_Refraksi=NULL "
                                SQL = SQL & "where No_faktur='" & NoLoading & "' "
                                SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and kode_barang='" & lvKd_Brg & "'"
                                SQL = SQL & "and urut_oto='" & .Rows(index).Item("urut_oto") & "'"
                                ExecuteTrans(SQL)
                            Else

                                SQL = "update EMI_Pembelian_Loading_detail set Flag_Permintaan_Refraksi = NULL "
                                SQL = SQL & "where No_faktur='" & NoLoading & "' "
                                SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and kode_barang='" & lvKd_Brg & "'"
                                SQL = SQL & "and urut_oto='" & .Rows(index).Item("urut_oto") & "'"
                                ExecuteTrans(SQL)

                                SQL = "update EMI_Pembelian_PO_Detail set Flag_Permintaan_Refraksi = NULL "
                                SQL = SQL & "where No_faktur='" & .Rows(index).Item("no_faktur") & "' "
                                SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and kode_barang='" & lvKd_Brg & "'"
                                SQL = SQL & "and No_Urut='" & .Rows(index).Item("urut_Po") & "'"
                                ExecuteTrans(SQL)

                            End If
                        Next
                    End If
                End With
            End Using

            SQL = "select Flag_Tolak_Sebagian, Flag_Tolak "
            SQL = SQL & "from EMI_Pembelian_Loading_detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        If General_Class.CekNULL(.Rows(0).Item("Flag_Tolak_Sebagian")) = "Y" Then

                            SQL = " update EMI_Pembelian_Loading_detail set Flag_Tolak_Sebagian = NULL "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' "
                            ExecuteTrans(SQL)

                        ElseIf General_Class.CekNULL(.Rows(0).Item("Flag_Tolak")) = "Y" Then

                            SQL = " update EMI_Pembelian_Loading_detail set Flag_Tolak = NULL "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' "
                            ExecuteTrans(SQL)

                        End If

                    End If
                End With
            End Using

            '============================
            '=     UPDATE DATA QC 1     =
            '============================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading_Detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
            SQL = SQL & "and Kode_Barang = '" & lvKd_Brg & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading_detail set Warna = NULL, flag_qc_pertama = NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Faktur = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' "
                    ExecuteTrans(SQL)

                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Select kode_perusahaan from EMI_Pembelian_Loading where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and status is null"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading set Flag_QC_Pertama = NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
                    ExecuteTrans(SQL)

                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            'If True Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Tahan")
            '    Exit Sub
            'End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Quality Control 1 Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        button1_Click(Me, New EventArgs)

    End Sub

    Private Sub PembatalanQC2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembatalanQC2ToolStripMenuItem.Click
        If ListView2.Items.Count = 0 Or ListView2.FocusedItem Is Nothing Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Quality Control 2"

            get_isi_listview(ListView2.FocusedItem.Index)

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_QC_2") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Quality Control 2", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Quality Control ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim NoLoading As String = lvNoLoading

            '=========================================
            '=     CEK APAKAH LOADING DIBATALKAN     =
            '=========================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan QC tidak dapat dilakukan karena No Loading Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            Dim NoFakturQC As String = ""
            '=========================
            '=     GET NO FAKTUR     =
            '=========================
            SQL = "select No_Faktur from EMI_Hasil_Quality_Control "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Fak_Loading_Barang = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' "
            SQL = SQL & "and Jenis_QC = '2' and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    NoFakturQC = Dr("No_Faktur")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data QC Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==========================================
            '=     CEK APAKAH QC SUDAH DIBATALKAN     =
            '==========================================
            SQL = "select Kode_Perusahaan from EMI_Hasil_Quality_Control "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Fak_Loading_Barang = '" & NoLoading & "' "
            SQL = SQL & "and Jenis_QC = '2' and Kode_Barang = '" & lvKd_Brg & "' and status = 'Y' and No_Faktur = '" & NoFakturQC & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan QC tidak dapat dilakukan karena QC Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==================================
            '=     CEK DATA TIMBANG MASUK     =
            '==================================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Loading = b.No_Faktur "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan QC 2 tidak dapat dilakukan karena Data sudah melewati proses Timbang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==============================================
            '=     CEK DATA BARANG MASUK ANDROID FLAG     =
            '==============================================
            SQL = "select a.Kode_Perusahaan from EMI_Pembelian_Loading_Detail a, emi_pembelian_loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Status is null "
            SQL = SQL & "and a.Flag_Sudah_Bongkar_Android = 'Y' "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan QC 2 tidak dapat dilakukan karena No Loading Sudah Dibongkar", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================================
            '=     CEK DATA BARANG MASUK ANDROID     =
            '=========================================
            'SQL = "select a.Kode_Perusahaan "
            'SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, emi_pembelian_loading b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Pembelian_Loading = b.No_Faktur "
            'SQL = SQL & "and b.Status is null and a.Status is null "
            'SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Pembelian_Loading = '" & NoLoading & "' "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Pembatalan QC 2 tidak dapat dilakukan karena Loading Sudah Masuk ke Tahan Barang Masuk Android", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            '==============================
            '=     ROLLBACK DATA QC 2     =
            '==============================
            SQL = "select a.No_Faktur "
            SQL = SQL & "from EMI_Hasil_Quality_Control a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Fak_Loading_Barang = b.No_Faktur "
            SQL = SQL & "and a.status is null and b.status is null "
            SQL = SQL & "and a.Jenis_QC = '2' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_Loading_Barang = '" & NoLoading & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoFakturQC & "' "
            SQL = SQL & "and a.Kode_Barang = '" & lvKd_Brg & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        'RollBack Detail QC
                        'SQL = "update EMI_Hasil_Detail_Quality_Control "
                        'SQL = SQL & "set value_kode_uji = NULL, Warna = NULL, Keterangan = NULL "
                        'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & .Rows(0).Item("No_Faktur") & "' "
                        'ExecuteTrans(SQL)

                        ''RollBack Switch QC
                        'SQL = "update EMI_Hasil_Detail_Switch_QC "
                        'SQL = SQL & "set value_kode_uji = NULL, Warna = NULL, Keterangan = NULL "
                        'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & .Rows(0).Item("No_Faktur") & "' "
                        'ExecuteTrans(SQL)
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data QC Tidak Ditemukan atau Data Tidak Berada pada Step QC 1", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            'Update EMI_Hasil_Quality_Control
            SQL = "select Kode_Perusahaan from EMI_Hasil_Quality_Control "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Jenis_QC = '2' "
            SQL = SQL & "and No_Fak_Loading_Barang = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' and No_Faktur = '" & NoFakturQC & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Hasil_Quality_Control "
                    SQL = SQL & "set Status = 'Y', UserID_Batal = '" & UserID & "', Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Jenis_QC = '2' "
                    SQL = SQL & "and No_Fak_Loading_Barang = '" & NoLoading & "'"
                    SQL = SQL & "and No_Faktur = '" & NoFakturQC & "' "
                    SQL = SQL & "and Kode_Barang = '" & lvKd_Brg & "' "
                    ExecuteTrans(SQL)

                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data QC Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            'CEK APAKAH REFRAKSI
            SQL = "Select b.Flag_Refraksi, b.Harga_Refraksi, a.urut_oto, a.urut_Po, b.no_faktur from "
            SQL = SQL & "EMI_Pembelian_Loading_detail a, EMI_Pembelian_PO_Detail b where "
            SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.urut_Po = b.No_Urut "
            SQL = SQL & "And a.no_faktur='" & NoLoading & "' and a.kode_barang='" & lvKd_Brg & "' "
            SQL = SQL & " and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For index = 0 To .Rows.Count - 1

                            If General_Class.CekNULL(.Rows(index).Item("Flag_Refraksi")) = "Y" Then

                                SQL = "update EMI_Pembelian_Loading_detail set flag_refraksi = NULL, "
                                SQL = SQL & "Harga_Refraksi=NULL "
                                SQL = SQL & "where No_faktur='" & NoLoading & "' "
                                SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and kode_barang='" & lvKd_Brg & "'"
                                SQL = SQL & "and urut_oto='" & .Rows(index).Item("urut_oto") & "'"
                                ExecuteTrans(SQL)
                            Else

                                SQL = "update EMI_Pembelian_Loading_detail set Flag_Permintaan_Refraksi = NULL "
                                SQL = SQL & "where No_faktur='" & NoLoading & "' "
                                SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and kode_barang='" & lvKd_Brg & "'"
                                SQL = SQL & "and urut_oto='" & .Rows(index).Item("urut_oto") & "'"
                                ExecuteTrans(SQL)

                                SQL = "update EMI_Pembelian_PO_Detail set Flag_Permintaan_Refraksi = NULL "
                                SQL = SQL & "where No_faktur='" & .Rows(index).Item("no_faktur") & "' "
                                SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and kode_barang='" & lvKd_Brg & "'"
                                SQL = SQL & "and No_Urut='" & .Rows(index).Item("urut_Po") & "'"
                                ExecuteTrans(SQL)

                            End If
                        Next
                    End If
                End With
            End Using

            SQL = "select Flag_Tolak_Sebagian, Flag_Tolak "
            SQL = SQL & "from EMI_Pembelian_Loading_detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        If General_Class.CekNULL(.Rows(0).Item("Flag_Tolak_Sebagian")) = "Y" Then

                            SQL = " update EMI_Pembelian_Loading_detail set Flag_Tolak_Sebagian = NULL "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' "
                            ExecuteTrans(SQL)

                        ElseIf General_Class.CekNULL(.Rows(0).Item("Flag_Tolak")) = "Y" Then

                            SQL = " update EMI_Pembelian_Loading_detail set Flag_Tolak = NULL "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' "
                            ExecuteTrans(SQL)

                        End If

                    End If
                End With
            End Using

            '============================
            '=     UPDATE DATA QC 2     =
            '============================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading_Detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
            SQL = SQL & "and Kode_Barang = '" & lvKd_Brg & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading_detail set Warna = NULL, flag_qc = NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Faktur = '" & NoLoading & "' and Kode_Barang = '" & lvKd_Brg & "' "
                    ExecuteTrans(SQL)

                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            'If True Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Tahan")
            '    Exit Sub
            'End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Quality Control 2 Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        button1_Click(Me, New EventArgs)
    End Sub

    '============================================================================================================================================================================
    '=     HANDLE KEY PRESS
    '============================================================================================================================================================================
    Private Sub ListView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ListView1.KeyPress
        If e.KeyChar = Chr(13) Then ListView2.Focus()
    End Sub

    Private Sub ListView2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ListView2.KeyPress
        If e.KeyChar = Chr(13) Then ListView3.Focus()
    End Sub

    Private Sub ListView3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ListView3.KeyPress
        If e.KeyChar = Chr(13) Then
            ComboBox6.DroppedDown = True
            ComboBox6.Focus()
        End If
    End Sub

    Private Sub ComboBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox6.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox3.Focus()
    End Sub

    Private Sub CheckBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox3.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox1.Focus()
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox1.Checked Then
                ComboBox3.DroppedDown = True
                ComboBox3.Focus()
            Else
                CheckBox2.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox2.Focus()
    End Sub

    Private Sub CheckBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox2.Checked Then
                ComboBox2.DroppedDown = True
                ComboBox2.Focus()
            Else
                button1.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox4.Focus()
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then button1.Focus()
    End Sub

End Class