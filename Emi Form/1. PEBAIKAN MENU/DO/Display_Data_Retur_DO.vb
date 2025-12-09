Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6

Public Class Display_Data_Retur_DO
    Dim Arr1, Arr2, Arr3 As New ArrayList
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Private Sub cetak()
        Try

            OpenConn()

            SQL = "select kode_perusahaan from detail_r_penjualan where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "no_retur_jual = '" & ListView1.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Faktur_Retur_Penjualan_Akhir     'Nama file CR
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{detail_r_penjualan.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_r_penjualan.no_retur_jual} = '" & ListView1.FocusedItem.Text & "'"
                    CrDoc.SummaryInfo.ReportTitle = "Faktur Retur Penjualan"

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterName
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 99)
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cetak_rtr()
        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""
            Dim boleh_cetak As String = "T"


            SQL = "select a.kode_perusahaan, a.flag_cabang_sendiri, a.jenis from retur_jual a, detail_retur_jual b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = 'RTCJW-02/20-0002'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New Faktur_Retur_Jual

                    kertas = "Faktur"

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{detail_retur_jual.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_retur_jual.No_faktur} = 'RTCJW-02/20-0002'"
                    'CrDoc.SummaryInfo.ReportTitle = "[DUPLIKAT]"

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterName
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
                End If
            End Using

            CloseConn()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Display_Data_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView1.Columns.Add("No Retur Sementara", 120, HorizontalAlignment.Left) '0
        ListView1.Columns.Add("No DO", 120, HorizontalAlignment.Left) '1
        ListView1.Columns.Add("No Faktur", 120, HorizontalAlignment.Left) '2
        ListView1.Columns.Add("Tanggal DO", 70, HorizontalAlignment.Center) '3
        ListView1.Columns.Add("Jam", 70, HorizontalAlignment.Center) '4
        ListView1.Columns.Add("Kode Customer", 100, HorizontalAlignment.Left) '5
        ListView1.Columns.Add("Customer", 210, HorizontalAlignment.Left) '6
        ListView1.Columns.Add("User ID", 120, HorizontalAlignment.Center) '7
        ListView1.Columns.Add("Status", 120, HorizontalAlignment.Center) '8
        ListView1.Columns.Add("No Retur", 120, HorizontalAlignment.Left) '9
        ListView1.View = View.Details

        ListView1.Columns(9).DisplayIndex = 3

        ListView2.Columns.Add("Stock Owner", 0, HorizontalAlignment.Center)
        ListView2.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        ListView2.Columns.Add("Nama Barang", 210, HorizontalAlignment.Left)
        ListView2.Columns.Add("Good Stock", 60, HorizontalAlignment.Right)
        ListView2.Columns.Add("Satuan", 100, HorizontalAlignment.Left)
        ListView2.View = View.Details

        ListView4.Columns.Add("Barcode Barang", 280, HorizontalAlignment.Left)
        ListView4.Columns.Add("Good Stock", 100, HorizontalAlignment.Right)
        ListView4.View = View.Details

        CheckBox1.Checked = False : CheckBox2.Checked = False
        ComboBox1.Items.Clear() : ComboBox1.Text = "" : Arr1.Clear()
        ComboBox1.Items.Add("Tanggal") : Arr1.Add("a.tanggal")

        ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
        ComboBox2.Items.Add("No Retur") : Arr2.Add("a.No_Retur_Jual_Sementara")
        ComboBox2.Items.Add("No DO") : Arr2.Add("a.No_DO")
        ComboBox2.Items.Add("No Faktur Jual") : Arr2.Add("c.No_Faktur")
        ComboBox2.Items.Add("Kode Customer") : Arr2.Add("d.Kode_Customer")
        ComboBox2.Items.Add("Nama Customer") : Arr2.Add("d.nama")
        'ComboBox2.Items.Add("Kode Barang") : Arr2.Add("c.Kode_Barang")
        'ComboBox2.Items.Add("Nama Barang") : Arr2.Add("a.Nama") 

        DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        TextBox1.Text = ""
        ComboBox3.Items.Clear() : ComboBox3.Text = "" : Arr3.Clear()
        ComboBox3.Items.Add("Tanggal") : Arr3.Add("b.Tanggal")
        ComboBox3.Items.Add("Tgl Jatuh_Tempo") : Arr3.Add("b.Tgl_Jatuh_tempo")
        ComboBox3.Items.Add("Tgl Lunas") : Arr3.Add("b.Tgl_Lunas")
        ComboBox3.Items.Add("No Faktur") : Arr3.Add("b.No_Faktur")
        ComboBox3.Items.Add("Kode Supplier") : Arr3.Add("b.Kode_Supplier")
        ComboBox3.Items.Add("Nama Supplier") : Arr3.Add("d.Nama")
        ComboBox3.Items.Add("Flag Lunas") : Arr3.Add("b.Flag_Lunas")
        ComboBox3.Items.Add("UserID") : Arr3.Add("b.Userid")
        ComboBox3.Items.Add("Kode Stock_Owner") : Arr3.Add("c.Kode_Stock_Owner")
        ComboBox3.Items.Add("Kode Barang") : Arr3.Add("c.Kode_Barang")
        ComboBox3.Items.Add("Nama Barang") : Arr3.Add("a.Nama")
        ComboBox3.SelectedIndex = 0

        ComboBox4.Items.Clear() : ComboBox4.Text = ""
        ComboBox4.Items.Add("Asc")
        ComboBox4.Items.Add("Desc")
        ComboBox4.SelectedIndex = 1

        ComboBox1.Enabled = False : ComboBox2.Enabled = False
        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        TextBox1.Enabled = False

        Try
            OpenConn()

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add("-- Seluruh --")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox6.Text = Lokasi

            If CekButtonRole("Ganti_Lokasi_Display_Retur_Penjualan") = "T" Then
                ComboBox6.Enabled = False
            Else
                ComboBox6.Enabled = True
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox1.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            CheckBox3.Checked = False
        Else
            ComboBox1.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            ComboBox1.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ComboBox2.Enabled = True : TextBox1.Enabled = True
        Else
            ComboBox2.Enabled = False : TextBox1.Enabled = False
            ComboBox2.SelectedIndex = -1 : TextBox1.Text = ""
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            Button1_Click(CheckBox3, e)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data . . ! !", Judul)
            CheckBox1.Focus() : Exit Sub
        End If

        If CheckBox1.Checked Then
            If ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Parameter pencarian per tanggal harus diisi . . ! !", Judul)
                ComboBox1.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II . . ! !", Judul)
                DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                Exit Sub
            End If
        ElseIf CheckBox2.Checked Then
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi . . ! !", Judul)
                ComboBox2.Focus() : Exit Sub
            ElseIf TextBox1.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi . . ! !", Judul)
                TextBox1.Focus() : Exit Sub
            End If
        End If


        Try

            OpenConn()

            ListView1.Items.Clear() : ListView2.Items.Clear()
            'SQL = "select "
            'SQL = SQL & "b.lokasi, b.kode_perusahaan, b.no_retur_jual, b.no_do, e.no_faktur, b.tanggal, b.jam, "
            'SQL = SQL & "b.userid, b.status, e.kode_customer, d.nama "
            'SQL = SQL & "from barang a, retur_do b, detail_r_do c, Customers d, penjualan e, do_new f where "
            'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and c.kode_perusahaan = d.kode_perusahaan and d.kode_perusahaan = e.kode_perusahaan and e.kode_perusahaan = f.kode_perusahaan and "
            'SQL = SQL & "a.kode_barang = c.kode_barang and a.kode_stock_owner = c.kode_stock_owner and b.no_retur_jual = c.no_retur_jual and "
            'SQL = SQL & "b.no_do = f.no_do and e.no_faktur = f.no_faktur and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and d.kode_customer = e.kode_customer "

            'If CheckBox3.Checked Then
            '    'Pasang And
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & " b.tanggal between '"
            '    SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            'End If

            'If CheckBox1.Checked Then
            '    'Pasang And
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & Arr1.Item(ComboBox1.SelectedIndex) & " between '"
            '    SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            'End If

            'If CheckBox2.Checked Then
            '    'Pasang And
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
            'End If

            'If ComboBox6.SelectedIndex <> 0 Then
            '    SQL = SQL & " and b.lokasi = '" & ComboBox6.Text & "' "
            'End If

            'SQL = SQL & "group by b.lokasi, b.kode_perusahaan, b.no_retur_jual, b.no_do, e.no_faktur, b.tanggal, b.jam, "
            'SQL = SQL & "b.userid, b.status, e.kode_customer, d.nama "

            'SQL = SQL & "Order by b.no_retur_jual Desc"



            SQL = "SELECT a.lokasi, a.kode_perusahaan, a.No_Retur_Jual_Sementara AS no_retur_jual, a.No_DO, c.No_Faktur, "
            SQL = SQL & "a.tanggal, a.jam, a.UserId_Release AS userid, a.Status, d.Kode_Customer, d.nama, "
            SQL = SQL & "isnull(( "
            SQL = SQL & "SELECT z.No_Retur_Jual "
            SQL = SQL & "FROM retur_do z "
            SQL = SQL & "WHERE z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "AND z.No_Retur_Sementara = a.No_Retur_Jual_Sementara "
            SQL = SQL & "AND z.Status is null "
            SQL = SQL & "), '-') as No_Validasi, "
            SQL = SQL & "CASE WHEN EXISTS ( SELECT 1 FROM retur_do z WHERE z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "AND z.No_Retur_Sementara = a.No_Retur_Jual_Sementara and z.Status is null ) THEN 'Validasi Accounting' "
            SQL = SQL & "WHEN EXISTS ( SELECT 1 FROM detail_r_do_sementara z INNER JOIN det_r_do_sementara x  "
            SQL = SQL & "ON z.Kode_Perusahaan = x.Kode_Perusahaan AND z.No_Retur_Jual_Sementara = x.No_Retur_Jual_Sementara "
            SQL = SQL & "AND z.No_Urut = x.Urut_Detail "
            SQL = SQL & "WHERE z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "AND z.No_Retur_Jual_Sementara = a.No_Retur_Jual_Sementara) THEN 'Validasi Warehouse' "
            SQL = SQL & "ELSE 'Retur Marketing' END AS Status_Retur "
            SQL = SQL & "FROM retur_do_sementara a "
            SQL = SQL & "INNER JOIN do_new b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_DO = b.No_DO "
            SQL = SQL & "INNER JOIN penjualan c on b.Kode_Perusahaan = c.Kode_Perusahaan AND b.no_faktur = c.No_Faktur "
            SQL = SQL & "INNER JOIN Customers d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Customer = d.Kode_Customer "
            SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "AND b.Status is null AND c.Status is null "

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr1.Item(ComboBox1.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
            End If

            If ComboBox6.SelectedIndex <> 0 Then
                SQL = SQL & " and a.lokasi = '" & ComboBox6.Text & "' "
            End If
            SQL = SQL & "Order by a.tanggal, a.Jam "


            Ds = New DataSet
            Ds = Binding(SQL)
            Dim NomorFaktur As String = ""

            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim GrandTidakBatal As Double = 0
            Dim GrandBatal As Double = 0

            While i <= Ds.Tables("MyTable").Rows.Count - 1
                ' Application.DoEvents()
                With Ds.Tables("MyTable").Rows(i)
                    'If NomorFaktur = .Item("No_Faktur") Then
                    'Else
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(.Item("no_retur_jual")) '0
                    Lvw.SubItems.Add(.Item("no_do")) '1
                    Lvw.SubItems.Add(.Item("no_faktur")) '2
                    Lvw.SubItems.Add(Format(.Item("tanggal"), "dd MMM yyyy")) '3
                    Lvw.SubItems.Add(.Item("jam")) '4
                    Lvw.SubItems.Add(.Item("kode_customer")) '5
                    Lvw.SubItems.Add(.Item("nama")) '6
                    Lvw.SubItems.Add(If(General_Class.CekNULL(.Item("Userid")) = "", "-", .Item("Userid"))) '7
                    Lvw.SubItems.Add(.Item("Status_Retur")) '8
                    Lvw.SubItems.Add(.Item("No_Validasi")) '9
                    If Not IsDBNull(.Item("status")) Then
                        Lvw.BackColor = Color.DarkRed
                        Lvw.ForeColor = Color.White
                    Else
                        Lvw.ForeColor = Color.Black
                    End If


                    If General_Class.CekNULL(.Item("Status_Retur")).Trim.ToUpper = "Validasi Accounting".Trim.ToUpper Then
                        Lvw.BackColor = Color.LightGreen
                        Lvw.ForeColor = Color.Black
                    ElseIf General_Class.CekNULL(.Item("Status_Retur")).Trim.ToUpper = "Validasi Warehouse".Trim.ToUpper Then
                        Lvw.BackColor = Color.LightGray
                        Lvw.ForeColor = Color.Black
                    Else
                        Lvw.BackColor = Color.White
                        Lvw.ForeColor = Color.Black
                    End If

                    j += 1
                End With
                i += 1
            End While

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
    '    If ListView1.Items.Count = 0 Then Exit Sub

    '    OpenConn()

    '    Dim Grand As Double = 0
    '    ListView2.Items.Clear()
    '    SQL = "Select b.satuan, a.persen_diskon, a.nilai_diskon, a.kode_stock_owner,a.Kode_barang,b.nama,a.harga,a.jumlah,(a.harga * a.jumlah) as SubTotal "
    '    SQL = SQL & "from detail_pembelian as a inner join barang as b on a.kode_perusahaan = b.kode_Perusahaan and "
    '    SQL = SQL & "a.kode_barang = b.kode_barang where a.kode_perusahaan = '" & KodePerusahaan & "' and "
    '    SQL = SQL & "a.no_faktur = '" & ListView1.FocusedItem.Text & "'"
    '    Using Dr = Open(SQL)
    '        Do While Dr.Read
    '            Dim Lvw As ListViewItem
    '            Lvw = ListView2.Items.Add(Dr("kode_stock_owner"))
    '            Lvw.SubItems.Add(Dr("kode_barang"))
    '            Lvw.SubItems.Add(Dr("nama"))
    '            Lvw.SubItems.Add(Format(Dr("harga"), "N0"))
    '            Lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
    '            Lvw.SubItems.Add(Dr("satuan"))
    '            Lvw.SubItems.Add(Dr("persen_diskon"))
    '            Lvw.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))
    '            Lvw.SubItems.Add(Format(Dr("subtotal"), "N0"))
    '            Grand += Dr("subtotal")
    '        Loop
    '    End Using
    '    TextBox2.Text = Format(Grand, "N0")

    '    CloseConn()
    'End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then Button1_Click(TextBox1, e)
    End Sub

    Private Sub CheckBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If ComboBox1.Enabled = True Then
                ComboBox1.Focus()
            Else
                CheckBox2.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Button1_Click(DateTimePicker2, e)
    End Sub

    Private Sub CheckBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CheckBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If ComboBox2.Enabled = True Then
                ComboBox2.Focus()
            Else
                Button1.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        DateTimePicker1.Focus()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        TextBox1.Focus()
    End Sub

    Private Sub BatalkanTransaksiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatalkanTransaksiToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim TotalRetur As Double = 0 ' total retur
        'Dim TotalSeluruhRetur As Double = 0
        'Dim TotalJual As Double = 0
        'Dim TotalValidasi As Double = 0
        'Dim SisaHutang As Double = 0
        Dim JT As String = "" 'Jenis Transaksi
        Dim Flag_Lunas As String = ""

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan transaksi retur penjualan ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                SQL = "select b.status, a.jenis_transaksi, a.grand as grand_jual, b.grand as grand_retur "
                'SQL = SQL & "isnull((select sum(y.grand) as ttl_retur from retur_penjualan y where y.kode_perusahaan = a.kode_perusahaan and y.no_faktur_jual = b.no_faktur_jual and x.status is null), 0) as ttl_Seluruh_retur, "
                'SQL = SQL & "isnull((select sum(x.bayar) from validasi_penjualan x where x.kode_perusahaan = a.kode_perusahaan and x.no_faktur = b.no_faktur_jual), 0) as Total_Validasi "
                SQL = SQL & "from penjualan a, retur_penjualan b where a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur_jual and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and b.no_retur_jual = '" & ListView1.FocusedItem.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        TotalRetur = Dr("grand_retur")
                        ' TotalSeluruhRetur = Dr("ttl_seluruh_retur")
                        'TotalJual = Dr("grand_jual")
                        'TotalValidasi = Dr("total_validasi")
                        JT = Dr("jenis_transaksi")
                        'SisaHutang = Dr("grand_jual") - Dr("ttl_seluruh_retur") - Dr("total_validasi")
                        If General_Class.CekNULL(Dr("status")) = "Y" Then
                            MessageBox.Show("Transaksi retur penjualan tidak bisa dibatalkan, karena sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            CloseConn()
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Transaksi tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CloseConn()
                        Exit Sub
                    End If
                End Using

                SQL = "select * from detail_r_penjualan where kode_perusahaan = '" & KodePerusahaan & "' and no_retur_jual = '" & ListView1.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1
                            SQL = "select * from barang where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' and kode_barang = '" & .Rows(i).Item("kode_barang") & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Dr("good_stock") - .Rows(i).Item("good_stock") < BolehNegatif Then
                                        Dim Barang As String = Dr("nama")
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses membuat stock menjadi negatif untuk barang " & Barang & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        Exit Sub
                                    ElseIf Dr("bad_stock") - .Rows(i).Item("bad_stock") < BolehNegatif Then
                                        Dim Barang As String = Dr("nama")
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses membuat bad stock menjadi negatif untuk barang " & Barang & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        Exit Sub
                                    End If
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                    Exit Sub
                                End If
                            End Using
                            SQL = "Update barang set good_stock = good_stock - " & .Rows(i).Item("good_stock") & ", bad_stock = bad_stock - " & .Rows(i).Item("bad_stock") & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' and kode_barang = '" & .Rows(i).Item("kode_barang") & "'"
                            ExecuteTrans(SQL)
                        Next
                    End With
                End Using

                'For i As Integer = 0 To ListView2.Items.Count - 1
                '    'tambah stock
                '    SQL = "Update barang set good_stock = good_stock + " & HilangkanTanda(ListView2.Items(i).SubItems(4).Text) & " where "
                '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ListView2.Items(i).Text & "' and kode_barang = '" & ListView2.Items(i).SubItems(1).Text & "'"
                '    ExecuteTrans(SQL)
                'Next

                If JT = "N" Then
                    'Dim SisaHutang As Double = 0
                    'Dim Piutang As Double = 0
                    'SisaHutang = TotalJual - TotalRetur - TotalValidasi
                    'If SisaHutang >= TotalRetur Then
                    '    Piutang = TotalRetur
                    'ElseIf SisaHutang < TotalRetur Then
                    '    Piutang = SisaHutang
                    'End If
                    'TotalRetur = Val(HilangkanTanda(TextBox20.Text))

                    'If SisaHutang >= TotalRetur Then
                    '    SQL = "update customers set piutang = piutang - " & TotalRetur & " where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & KodeCust & "'"
                    '    ExecuteTrans(SQL)
                    'ElseIf SisaHutang < TotalRetur Then
                    '    CloseTrans()
                    '    CloseConn()
                    '    MessageBox.Show("Retur melebihi total piutang untuk faktur " & TextBox1.Text.Trim & Chr(13) & "Hapus dahulu validasi yang ada." & Chr(13) & "Proses tidak dapat dilanjutkan..", Judul)
                    '    Exit Sub
                    'End If
                    SQL = "update customers set piutang = piutang + " & TotalRetur & " where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & ListView1.FocusedItem.SubItems(5).Text & "'"
                    ExecuteTrans(SQL)
                End If

                SQL = "Update penjualan set flag_lunas = NULL, Tgl_lunas = NULL,"
                SQL = SQL & "uservalidasi = NULL where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_faktur = '" & ListView1.FocusedItem.SubItems(1).Text & "'"
                ExecuteTrans(SQL)

                SQL = "update retur_penjualan set status = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and no_retur_jual = '" & ListView1.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                MessageBox.Show("Transaksi retur penjualan berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ListView1.FocusedItem.ForeColor = Batal

                Label17.Text = Format(Val(HilangkanTanda(Label17.Text)) - TotalRetur, "N0")
                Label18.Text = Format(Val(HilangkanTanda(Label18.Text)) + TotalRetur, "N0")

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub KembalikanTransaksiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KembalikanTransaksiToolStripMenuItem.Click
        'If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
        '    MessageBox.Show("Pilih dahulu no faktur yang mau dikembalikan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'Dim lanjut As Boolean
        'Dim Transaksi As String = ""
        'Dim Lunas As String = ""
        'Dim TotalBelanja As Double = 0
        'Dim JT As String = "" 'Jenis Transaksi

        'Try
        '    OpenConn()

        '    Using Dr = OpenTrans("select * from penjualan where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'")
        '        If Dr.Read Then
        '            TotalBelanja = Dr("grand")
        '            JT = Dr("jenis_transaksi")
        '            Transaksi = Dr("jenis_transaksi")
        '            Lunas = General_Class.CekNULL(Dr("flag_lunas"))
        '            If UCase(General_Class.CekNULL(Dr("status"))) = "Y" Then
        '                lanjut = True
        '            Else
        '                lanjut = False
        '            End If
        '        Else
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Transaksi tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using

        '    If lanjut = True Then
        '        Dim tanya1 As String = MessageBox.Show("Yakin akan kembalikan transaksi penjualan ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '        If tanya1 = vbYes Then
        '            Cmd.Transaction = Cn.BeginTransaction

        '            Dim rand As New Random   '= Format(Now, "MMddHHmmss") & Format(rand.Next(0, 100000), "00000")
        '            Dim StrRand As String = ""
        '            StrRand = Format(Now, "MMddHHmmss") & Format(rand.Next(0, 100000), "00000")

        '            For i As Integer = 0 To ListView2.Items.Count - 1
        '                Using dr = OpenTrans("select * from cart where kode_perusahaan = '" & KodePerusahaan & "' and no_unik = '" & StrRand & "' and kode_stock_owner = '" & ListView2.Items(i).Text & "' and kode_barang = '" & ListView2.Items(i).SubItems(1).Text & "'")
        '                    If dr.Read Then
        '                        'update
        '                        dr.Close()
        '                        ExecuteTrans("update cart set jumlah = jumlah + " & Val(HilangkanTanda(ListView2.Items(i).SubItems(4).Text)) & " where kode_perusahaan = '" & KodePerusahaan & "' and no_unik = '" & StrRand & "' and kode_stock_owner = '" & ListView2.Items(i).Text & "' and kode_barang = '" & ListView2.Items(i).SubItems(1).Text & "'")
        '                    Else
        '                        'insert 
        '                        dr.Close()
        '                        ExecuteTrans("insert into cart(kode_perusahaan, no_unik, kode_stock_owner, kode_barang, jumlah) values('" & KodePerusahaan & "', '" & StrRand & "', '" & ListView2.Items(i).Text & "', '" & ListView2.Items(i).SubItems(1).Text & "', " & Val(HilangkanTanda(ListView2.Items(i).SubItems(4).Text)) & ")")
        '                    End If
        '                End Using
        '            Next

        '            '-----insert ke listview-------
        '            Dim lv As New ListViewItem
        '            ListView3.Items.Clear()

        '            Using Dr = OpenTrans("select * from cart where kode_perusahaan = '" & KodePerusahaan & "' and no_unik = '" & StrRand & "'")
        '                Do While Dr.Read
        '                    lv = ListView3.Items.Add(Dr("kode_stock_owner"))
        '                    lv.SubItems.Add(Dr("kode_barang"))
        '                    lv.SubItems.Add(Dr("jumlah"))
        '                Loop
        '            End Using

        '            '"""""cek stok negatif
        '            For i As Integer = 0 To ListView3.Items.Count - 1
        '                Using Dr = OpenTrans("select * from barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ListView3.Items(i).Text & "' and kode_barang = '" & ListView3.Items(i).SubItems(1).Text & "'")
        '                    If Dr.Read Then
        '                        If Dr("good_stock") - HilangkanTanda(ListView3.Items(i).SubItems(2).Text) < 0 Then
        '                            MessageBox.Show("Proses kembalikan membuat stock menjadi negatif untuk barang " & ListView3.Items(i).SubItems(1).Text & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        '                            Dr.Close()
        '                            CloseTrans()
        '                            ' ExecuteTrans("delete from cart where kode_perusahaan = '" & KodePerusahaan & "' and no_unik = '" & StrRand & "'")
        '                            CloseConn()
        '                            Exit Sub
        '                        End If
        '                    End If
        '                End Using
        '            Next

        '            SQL = "delete from cart where kode_perusahaan = '" & KodePerusahaan & "' and no_unik = '" & StrRand & "'"
        '            ExecuteTrans(SQL)

        '            'update stok (kurangin)
        '            For i As Integer = 0 To ListView2.Items.Count - 1
        '                'tambah stock
        '                SQL = "Update barang set good_stock = good_stock - " & HilangkanTanda(ListView2.Items(i).SubItems(4).Text) & " where "
        '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ListView2.Items(i).Text & "' and kode_barang = '" & ListView2.Items(i).SubItems(1).Text & "'"
        '                ExecuteTrans(SQL)
        '            Next

        '            SQL = "update penjualan set status = NULL where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
        '            ExecuteTrans(SQL)

        '            If JT = "N" Then
        '                SQL = "update customers set piutang = piutang + " & TotalBelanja & " where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & ListView1.FocusedItem.SubItems(4).Text & "'"
        '                ExecuteTrans(SQL)
        '            End If

        '            Cmd.Transaction.Commit()

        '            MessageBox.Show("Transaksi penjualan berhasil dikembalikan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        End If
        '    Else
        '        MessageBox.Show("Kembalikan transaksi gagal, karena transaksi ini bukan transaksi batal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    End If

        '    CloseConn()
        'Catch ex As Exception
        '    CloseTrans()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub ReturToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
        '    MessageBox.Show("Pilih dahulu no faktur yang mau diretur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'retur_pembelian.TextBox1.Text = ListView1.FocusedItem.Text
        'retur_pembelian.Show()
        'retur_pembelian.TextBox1_Leave(Me.ReturToolStripMenuItem, e)
    End Sub

    Private Sub CetakUlangFakturToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakUlangFakturToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        cetak()

        OpenConn()

        SQL = "insert into log_table (kode_perusahaan, userid, tanggal, jam, no_faktur, keterangan) values ('" & KodePerusahaan & "', '" & UserID & "', '" & Format(Now, "yyyy-MM-dd") & "', '" & Format(Now, "HH:mm") & "', '" & ListView1.FocusedItem.Text & "', 'Cetak Ulang Faktur Retur Penjualan')"
        Execute(SQL)

        CloseConn()
        'A_Place_For_Printing.Show()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            If ListView1.SelectedItems.Count = 0 Then Exit Sub

            Dim noRetur As String = ListView1.SelectedItems(0).Text

            OpenConn()
            ListView2.Items.Clear() : ListView4.Items.Clear()

            SQL = $"
                SELECT 
                    a.kode_stock_owner,
                    a.kode_barang,
                    b.nama,
                    a.good_stock,
                    b.satuan
                FROM detail_r_do_sementara a
                JOIN barang b 
                    ON a.kode_perusahaan = b.kode_perusahaan
                    AND a.kode_barang = b.kode_barang
                    AND a.kode_stock_owner = b.kode_stock_owner
                WHERE a.kode_perusahaan = '{KodePerusahaan}'
                  AND a.No_Retur_Jual_Sementara = '{noRetur}'
            "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim kodeStockOwner As String = If(IsDBNull(Dr("kode_stock_owner")), "", Dr("kode_stock_owner").ToString)
                    Dim kodeBarang As String = If(IsDBNull(Dr("kode_barang")), "", Dr("kode_barang").ToString)
                    Dim nama As String = If(IsDBNull(Dr("nama")), "", Dr("nama").ToString)
                    Dim goodStock As String = If(IsDBNull(Dr("good_stock")), "0", Format(Dr("good_stock"), "N0"))
                    Dim satuan As String = If(IsDBNull(Dr("satuan")), "", Dr("satuan").ToString)

                    Dim Lvw As ListViewItem = ListView2.Items.Add(kodeStockOwner)
                    Lvw.SubItems.Add(kodeBarang)
                    Lvw.SubItems.Add(nama)
                    Lvw.SubItems.Add(goodStock)
                    Lvw.SubItems.Add(satuan)
                Loop
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CloseConn()
        End Try
    End Sub

    Private Sub ListView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListView2.SelectedIndexChanged
        Try
            If ListView2.SelectedItems.Count = 0 OrElse ListView1.SelectedItems.Count = 0 Then Exit Sub

            Dim noRetur As String = ListView1.SelectedItems(0).Text
            Dim kodeStockOwner As String = ListView2.SelectedItems(0).SubItems(0).Text
            Dim kodeBarang As String = ListView2.SelectedItems(0).SubItems(1).Text

            OpenConn()
            ListView4.Items.Clear()

            SQL = $"
                SELECT 
                    barcode,
                    good_stock
                FROM det_r_do_sementara
                WHERE kode_perusahaan = '{KodePerusahaan}'
                  AND No_Retur_Jual_Sementara = '{noRetur}'
                  AND kode_stock_owner = '{kodeStockOwner}'
                  AND kode_barang = '{kodeBarang}'
            "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim barcode As String = If(IsDBNull(Dr("barcode")), "", Dr("barcode").ToString)
                    Dim goodStock As String = If(IsDBNull(Dr("good_stock")), "0", Format(Dr("good_stock"), "N0"))

                    Dim Lvw As ListViewItem = ListView4.Items.Add(barcode)
                    Lvw.SubItems.Add(goodStock)
                Loop
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CloseConn()
        End Try
    End Sub

    'Private Sub Display_Data_Penjualan_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
    '    Label1.Size = New Point(Me.Width, 33)
    'End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Cetak_rtr()
    End Sub
End Class