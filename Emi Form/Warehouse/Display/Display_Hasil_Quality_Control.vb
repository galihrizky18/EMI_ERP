Imports System.Diagnostics.Eventing.Reader
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class Display_Hasil_Quality_Control
    Dim Jenis = "Display_Inquiry"
    Dim arr_tgl, arr_Lain As New ArrayList

    Dim lvKd_SO As String
    Dim lvKd_Brg As String
    Dim lvNm_Brg As String

    Dim CrDoc As Object

    Private Sub get_isi_listview(ByVal index As Integer)
        lvKd_SO = ListView2.Items(index).SubItems(0).Text
        lvKd_Brg = ListView2.Items(index).SubItems(1).Text
        lvNm_Brg = ListView2.Items(index).SubItems(2).Text
    End Sub
    Private Sub Display_Hasil_QC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            'Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            ListView1.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
            ListView1.Columns.Add("Kode Supplier", 130, HorizontalAlignment.Left)
            ListView1.Columns.Add("Nama Supplier", 200, HorizontalAlignment.Left)
            ListView1.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
            ListView1.Columns.Add("No Surat Jalan", 150, HorizontalAlignment.Left)
            ListView1.Columns.Add("No Plat", 100, HorizontalAlignment.Left)
            ListView1.Columns.Add("Driver", 150, HorizontalAlignment.Left)
            ListView1.Columns.Add("Tgl Masuk", 120, HorizontalAlignment.Center)
            ListView1.Columns.Add("Jam Masuk", 100, HorizontalAlignment.Center)
            ListView1.View = View.Details

            ListView2.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
            ListView2.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
            ListView2.Columns.Add("Nama Barang", 340, HorizontalAlignment.Left)
            ListView2.View = View.Details

            ListView3.Columns.Add("Tanggal", 120, HorizontalAlignment.Center) '0
            ListView3.Columns.Add("Jam", 100, HorizontalAlignment.Center) '1
            ListView3.Columns.Add("UserID", 100, HorizontalAlignment.Left) '2
            ListView3.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '3
            ListView3.Columns.Add("Warna", 120, HorizontalAlignment.Left) '4
            ListView3.Columns.Add("Jenis QC", 100, HorizontalAlignment.Center) '5
            ListView3.Columns.Add("Step", 100, HorizontalAlignment.Center) '6
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
                    Lvw.SubItems.Add(dr("lokasi"))
                    Lvw.SubItems.Add(dr("no_sj"))
                    Lvw.SubItems.Add(dr("No_Plat"))
                    Lvw.SubItems.Add(dr("driver"))
                    Lvw.SubItems.Add(Format(dr("tanggal_masuk"), "dd-MM-yyyy"))
                    Lvw.SubItems.Add(dr("Jam_Masuk"))
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
                    Lvw.SubItems.Add(dr("lokasi"))
                    Lvw.SubItems.Add(dr("no_sj"))
                    Lvw.SubItems.Add(dr("No_Plat"))
                    Lvw.SubItems.Add(dr("driver"))
                    Lvw.SubItems.Add(Format(dr("tanggal_masuk"), "dd-MM-yyyy"))
                    Lvw.SubItems.Add(dr("Jam_Masuk"))
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

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub
        Try
            OpenConn()

            ListView2.Items.Clear() : ListView3.Items.Clear()
            SQL = "select a.kode_stock_owner,a.kode_barang,b.nama "
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
        If ListView3.Items.Count = 0 Then Exit Sub

        If ListView3.Items.Count = 0 Or ListView3.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu data yang akan dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""
            SQL = "select Kode_Perusahaan from View_Laporan_Hasil_QC where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "no_hsl_qc = '" & ListView3.FocusedItem.SubItems(7).Text & "' "

            SF = "{View_Laporan_Hasil_QC.no_hsl_qc} = '" & ListView3.FocusedItem.SubItems(7).Text & "' "
            SF = SF & "and {View_Laporan_Hasil_QC.kode_perusahaan} = '" & KodePerusahaan & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New Rpt_Laporan_Hasil_QC
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = SF
                        'CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
                        .Text = "Laporan Hasil QC"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        '.CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                    End With
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

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub
        Try
            OpenConn()

            get_isi_listview(ListView2.FocusedItem.Index)
            ListView3.Items.Clear()
            SQL = "select a.tanggal,a.jam,a.userid,a.keterangan,a.warna,a.jenis_qc,a.step,a.no_faktur "
            SQL = SQL & "from emi_hasil_quality_control a where  a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_fak_loading_barang = '" & ListView1.FocusedItem.Text & "' "
            SQL = SQL & "and a.Kode_stock_owner = '" & lvKd_SO & "' and a.Kode_Barang = '" & lvKd_Brg & "' order by a.step "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView3.Items.Add(Format(dr("tanggal"), "dd-MM-yyyy"))
                    Lvw.SubItems.Add(dr("jam"))
                    Lvw.SubItems.Add(dr("userid"))
                    Lvw.SubItems.Add(dr("keterangan"))
                    Lvw.SubItems.Add(dr("warna"))
                    Lvw.SubItems.Add(dr("jenis_qc"))
                    Lvw.SubItems.Add(dr("step"))
                    Lvw.SubItems.Add(dr("no_faktur"))
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