Public Class Retur_DO_Reseller_Sementara
    Protected Friend Urut As String
    Protected Friend Urut_Oto As String
    Protected Friend hrg As Double
    Protected Friend discp As Double

    Dim arrInisialFaktur As New ArrayList
    Dim rv As Integer


    Protected Friend MetPer As String

    Private Sub cetak()
        'Try

        '    OpenConn()

        '    SQL = "select kode_perusahaan from detail_r_penjualan where "
        '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "no_retur_jual = '" & TextBox4.Text & "'"
        '    Using Ds = BindingTrans(SQL)
        '        If Ds.Tables("MyTable").Rows.Count <> 0 Then
        '            Dim CrDoc As New Faktur_Retur_Penjualan_Akhir     'Nama file CR
        '            CrDoc.SetDataSource(Ds)
        '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '            CrDoc.PrintOptions.PrinterName = PrinterName
        '            CrDoc.RecordSelectionFormula = "{detail_r_penjualan.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_r_penjualan.no_retur_jual} = '" & TextBox4.Text & "'"
        '            CrDoc.SummaryInfo.ReportTitle = "Faktur Retur Penjualan"

        '            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        '            doctoprint.PrinterSettings.PrinterName = PrinterName
        '            Dim rawKind As Integer
        '            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        '            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
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

    Private Sub tampil_brg()
        Try

            OpenConn()


            'SQL = "Select a.no_do, a.kode_stock_owner, a.Kode_barang, b.nama, a.jumlah, b.satuan, "
            'SQL = SQL & "sdh_selesai_validasi, no_urut, urut_oto, "

            'SQL = SQL & "isnull(("
            'SQL = SQL & "select sum(x.good_stock + x.bad_stock) from retur_do z, detail_r_do x where "
            'SQL = SQL & "z.kode_perusahaan = x.kode_perusahaan and "
            'SQL = SQL & "z.no_retur_jual = x.no_retur_jual and "
            'SQL = SQL & "x.kode_stock_owner = a.kode_stock_owner and "
            'SQL = SQL & "x.kode_barang = a.kode_barang and "
            'SQL = SQL & "x.urut_do = a.urut_oto and "
            'SQL = SQL & "z.kode_perusahaan = a.kode_perusahaan and z.no_do = a.no_do and "
            'SQL = SQL & "z.status is null group by x.kode_barang "
            'SQL = SQL & "), 0) as pernahretur, a.harga, a.persen_diskon, a.subtotal_baru, a.metode_perhitungan "

            'SQL = SQL & "from sub_invoice a, barang b where "
            'SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and a.kode_barang = b.kode_barang and "
            'SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "a.no_do = '" & TextBox1.Text.Trim & "' "
            'SQL = SQL & "order by a.no_urut"
            'Dr = OpenTrans(SQL)
            'ListView3.Items.Clear()

            'Do While Dr.Read
            '    Dim Lvw As ListViewItem
            '    Lvw = ListView3.Items.Add(Dr("kode_stock_owner"))
            '    Lvw.SubItems.Add(Dr("kode_barang"))
            '    Lvw.SubItems.Add(Dr("nama"))
            '    Lvw.SubItems.Add(Format(Dr("sdh_selesai_validasi"), "N4"))
            '    Lvw.SubItems.Add(Format(General_Class.CekZERO(Dr("pernahretur")), "N4"))
            '    Lvw.SubItems.Add(Dr("satuan"))
            '    Lvw.SubItems.Add(Dr("no_urut"))
            '    Lvw.SubItems.Add(Dr("urut_oto"))
            '    Lvw.SubItems.Add(Format(Dr("harga"), "N4"))
            '    Lvw.SubItems.Add(Dr("persen_diskon"))
            '    Lvw.SubItems.Add(Format(Dr("subtotal_baru"), "N4"))
            '    If General_Class.CekNULL(Dr("Metode_Perhitungan")) = "" Then
            '        Lvw.SubItems.Add("A")
            '    Else
            '        Lvw.SubItems.Add(Dr("Metode_Perhitungan"))
            '    End If
            'Loop

            SQL = "Select a.no_do, a.kode_stock_owner, a.Kode_barang, b.nama, a.jumlah, b.satuan, "
            SQL = SQL & "sdh_selesai_validasi, no_urut, urut_oto, "
            SQL = SQL & "isnull(("
            SQL = SQL & "select sum(x.good_stock + x.bad_stock) from retur_do z, detail_r_do x where "
            SQL = SQL & "z.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "z.no_retur_jual = x.no_retur_jual and "
            SQL = SQL & "x.kode_stock_owner = a.kode_stock_owner and "
            SQL = SQL & "x.kode_barang = a.kode_barang and "
            SQL = SQL & "x.urut_do = a.urut_oto and "
            SQL = SQL & "z.kode_perusahaan = a.kode_perusahaan and z.no_do = a.no_do and "
            SQL = SQL & "z.status is null group by x.kode_barang "
            SQL = SQL & "), 0) as pernahretur, "
            SQL = SQL & "isnull(("
            SQL = SQL & "select sum(y.good_stock) from detail_r_do_sementara y where "
            SQL = SQL & "y.no_retur_jual_sementara = '" & TextBox4.Text.Trim & "' and "
            SQL = SQL & "y.kode_stock_owner = a.kode_stock_owner and "
            SQL = SQL & "y.kode_barang = a.kode_barang "
            SQL = SQL & "group by y.kode_barang "
            SQL = SQL & "), 0) as max_retur, a.harga, a.persen_diskon, a.subtotal_baru, a.metode_perhitungan "
            SQL = SQL & "from sub_invoice a, barang b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and a.kode_barang = b.kode_barang and "
            SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.no_do = '" & TextBox1.Text.Trim & "' "
            SQL = SQL & "order by a.no_urut"
            Dr = OpenTrans(SQL)
            ListView3.Items.Clear()
            Do While Dr.Read
                Dim Lvw As ListViewItem
                Lvw = ListView3.Items.Add(Dr("kode_stock_owner"))
                Lvw.SubItems.Add(Dr("kode_barang"))
                Lvw.SubItems.Add(Dr("nama"))
                Lvw.SubItems.Add(Format(Dr("sdh_selesai_validasi"), "N4"))
                Lvw.SubItems.Add(Format(General_Class.CekZERO(Dr("pernahretur")), "N4"))
                Lvw.SubItems.Add(Dr("satuan"))
                Lvw.SubItems.Add(Dr("no_urut"))
                Lvw.SubItems.Add(Dr("urut_oto"))
                Lvw.SubItems.Add(Format(Dr("harga"), "N4"))
                Lvw.SubItems.Add(Dr("persen_diskon"))
                Lvw.SubItems.Add(Format(Dr("subtotal_baru"), "N4"))
                If General_Class.CekNULL(Dr("Metode_Perhitungan")) = "" Then
                    Lvw.SubItems.Add("A")
                Else
                    Lvw.SubItems.Add(Dr("Metode_Perhitungan"))
                End If
                Lvw.SubItems.Add(Format(General_Class.CekZERO(Dr("max_retur")), "N4"))
            Loop


            'SQL = "Select a.no_do, a.kode_stock_owner, a.Kode_barang, b.nama, b.satuan, "
            'SQL = SQL & "c.jml_terima, c.no_urut, urut_oto, "

            'SQL = SQL & "isnull(("
            'SQL = SQL & "select sum(x.good_stock + x.bad_stock) from retur_do z, detail_r_do x where "
            'SQL = SQL & "z.kode_perusahaan = x.kode_perusahaan and "
            'SQL = SQL & "z.no_retur_jual = x.no_retur_jual and "
            'SQL = SQL & "x.kode_stock_owner = a.kode_stock_owner and "
            'SQL = SQL & "x.kode_barang = a.kode_barang and "
            'SQL = SQL & "x.urut_do = a.urut_oto and "
            'SQL = SQL & "z.kode_perusahaan = a.kode_perusahaan and z.no_do = a.no_do and "
            'SQL = SQL & "z.status is null group by x.kode_barang "
            'SQL = SQL & "), 0) as pernahretur, a.harga, a.persen_diskon, a.subtotal_baru "

            'SQL = SQL & "from do_new a, barang b, detail_do_new c where "
            'SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and b.kode_perusahaan = c.kode_Perusahaan and a.kode_barang = b.kode_barang and "
            'SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and a.no_do = c.no_do and "
            'SQL = SQL & "a.status is null and a.validasi_terima = 'Y' and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "a.no_do = '" & TextBox1.Text.Trim & "' "
            'SQL = SQL & "order by a.no_urut"
            'Dr = OpenTrans(SQL)
            'ListView3.Items.Clear()

            'Do While Dr.Read
            '    Dim Lvw As ListViewItem
            '    Lvw = ListView3.Items.Add(Dr("kode_stock_owner"))
            '    Lvw.SubItems.Add(Dr("kode_barang"))
            '    Lvw.SubItems.Add(Dr("nama"))
            '    Lvw.SubItems.Add(Format(Dr("sdh_selesai_validasi"), "N4"))
            '    Lvw.SubItems.Add(Format(General_Class.CekZERO(Dr("pernahretur")), "N4"))
            '    Lvw.SubItems.Add(Dr("satuan"))
            '    Lvw.SubItems.Add(Dr("no_urut"))
            '    Lvw.SubItems.Add(Dr("urut_oto"))
            '    Lvw.SubItems.Add(Format(Dr("harga"), "N4"))
            '    Lvw.SubItems.Add(Dr("persen_diskon"))
            '    Lvw.SubItems.Add(Format(Dr("subtotal_baru"), "N4"))
            'Loop

            CloseConn()

        Catch ex As Exception
            ListView3.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub tampil_brg_retur()
        'OpenConn()

        'SQL = "select c.kode_stock_owner, a.kode_barang, b.nama, a.harga, a.good_stock, a.bad_stock from detail_r_penjualan a, barang b, stock_owner c, perusahaan d where b.kode_perusahaan = c.kode_perusahaan and c.kode_perusahaan = d.kode_perusahaan and a.kode_barang = b.kode_barang and a.kode_stock_owner = b.kode_stock_owner and a.kode_stock_owner = c.kode_stock_owner and a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_retur_jual = '" & Trim(TextBox4.Text) & "'"
        'Using Dr = Open(SQL)
        '    ListView2.Items.Clear()

        '    Dim lvw As New ListViewItem
        '    Do While Dr.Read
        '        lvw = ListView2.Items.Add(Dr("kode_stock_owner"))
        '        lvw.SubItems.Add(Dr("kode_barang"))
        '        lvw.SubItems.Add(Dr("nama"))
        '        lvw.SubItems.Add(Format(Dr("harga"), "N4"))
        '        lvw.SubItems.Add(Format(Dr("good_stock"), "N4"))
        '        lvw.SubItems.Add(Format(Dr("bad_stock"), "N4"))
        '        lvw.SubItems.Add(Format((Dr("good_stock") + Dr("bad_stock")) * Dr("harga"), "N4"))
        '    Loop

        'End Using

        'HitungGrandTotal()
        'CloseConn()

    End Sub

    Protected Friend Sub kosongbawah()
        hrg = 0
        discp = 0
        MetPer = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        Txt_Barcode.Text = ""

        Try
            OpenConn()

            ComboBox4.Items.Clear()
            SQL = "select kode_stock_owner from Stock_Owner_Gudang where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox4.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub get_no_faktur(ByVal jenis As String)
        TextBox4.Text = Rj_DO_S & jenis & arrInisialFaktur.Item(ComboBox1.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                             General_Class.Get_Last_Number2("retur_do_sementara", "no_retur_jual_sementara", JumlahDigit,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_retur_jual_sementara,1," & Len(Rj_DO_S & jenis) + Len(arrInisialFaktur.Item(ComboBox1.SelectedIndex)) + 6 & ")", Rj_DO_S & jenis & arrInisialFaktur.Item(ComboBox1.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))
    End Sub

    Private Sub kosong()
        rv = 0

        TextBox17.Text = "0"
        TextBox18.Text = "0"
        TextBox19.Text = "0"
        TxtTotal.Text = "0"
        get_jam()
        DateTimePicker1.Value = tgl_skg
        TextBox3.Text = ""
        ComboBox2.Items.Clear() : ComboBox2.SelectedIndex = -1
        ComboBox2.Items.Add("Tunai")
        ComboBox2.Items.Add("Non Tunai")
        DateTimePicker2.Value = Now
        DateTimePicker3.Value = Now
        TextBox2.Text = ""
        TextBox7.Text = "0"

        ListView3.Items.Clear()
        ListView2.Items.Clear()
        Lv_Hidden_Data.Items.Clear()

        kosongbawah()

        Button2.Enabled = True
        Button3.Enabled = False
        Button6.Enabled = False
        TextBox8.Enabled = False
        TextBox11.Enabled = True
        ListView2.Enabled = True

        TextBox1.Enabled = False

        Try

            OpenConn()

            ComboBox1.Items.Clear()
            arrInisialFaktur.Clear()

            SQL = "Select kode_stock_owner, inisial_faktur From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("kode_stock_owner"))
                    arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            ComboBox1.Text = Lokasi

            ListView4.Items.Clear()
            SQL = "select no_do,no_retur_jual_sementara from retur_do_sementara where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and status is null and lokasi = '" & ComboBox1.Text & "' and flag_val is null and flag_release is null "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView4.Items.Add(dr("no_do"))
                    Lvw.SubItems.Add(dr("no_retur_jual_sementara"))
                Loop
            End Using

            'get_no_faktur("")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub HitungGrandTotal()
        Dim Grand As Double = 0
        Dim Disc1 As Double = 0
        For i As Integer = 0 To ListView2.Items.Count - 1
            Grand = Grand + Val(HilangkanTanda(ListView2.Items(i).SubItems(8).Text))
        Next


        Dim nilai_ppn As Double = 0

        TextBox17.Text = Format(Grand, "N4")
        nilai_ppn = Grand * Val(TextBox18.Text) / 100
        nilai_ppn = Val(HilangkanTanda(Format(nilai_ppn, "N4")))
        TextBox19.Text = Format(nilai_ppn, "N4")

        Dim grandttl As Double = Grand + nilai_ppn
        TxtTotal.Text = Format(grandttl, "N4")

        'TextBox15.Text = Format(Grand, "N4")
        'If CheckBox2.Checked Then
        '    Disc1 = Grand * Val(TextBox16.Text) / 100
        '    TextBox17.Text = Format(Disc1, "N4")
        'End If
        'TextBox24.Text = Format(Grand - Val(HilangkanTanda(TextBox17.Text)) - Val(TextBox19.Text), "N4")
        'TextBox23.Text = Format(Val(HilangkanTanda(TextBox24.Text)) * Val(TextBox25.Text) / 100, "N4")
        'TextBox20.Text = Format(Grand - Val(HilangkanTanda(TextBox17.Text)) - Val(TextBox19.Text) + Val(HilangkanTanda(TextBox23.Text)), "N4")
    End Sub

    Private Sub Retur_Penjualan_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub retur_penjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView2.Columns.Add("Gudang", 130, HorizontalAlignment.Center)
        ListView2.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '1
        ListView2.Columns.Add("Nama Barang", 350, HorizontalAlignment.Left) '2  
        ListView2.Columns.Add("Max Rtr", 80, HorizontalAlignment.Right) '3
        ListView2.Columns.Add("*", 0, HorizontalAlignment.Right) '4
        ListView2.Columns.Add("urut_oto", 0, HorizontalAlignment.Left) '5
        ListView2.Columns.Add("Hrg", 0, HorizontalAlignment.Left) '6
        ListView2.Columns.Add("DiscP", 0, HorizontalAlignment.Left) '7
        ListView2.Columns.Add("Subttl", 0, HorizontalAlignment.Left) '8
        ListView2.Columns.Add("Metode Perhitungan", 0, HorizontalAlignment.Left) '9
        ListView2.Columns.Add("Jumlah Rtr", 80, HorizontalAlignment.Right) '10
        ListView2.View = View.Details

        Lv_Hidden_Data.Columns.Add("Gudang", 100, HorizontalAlignment.Center)
        Lv_Hidden_Data.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        Lv_Hidden_Data.Columns.Add("Nama Barang", 100, HorizontalAlignment.Left) '2  
        Lv_Hidden_Data.Columns.Add("Jumlah Rtr", 100, HorizontalAlignment.Right) '3
        Lv_Hidden_Data.Columns.Add("*", 100, HorizontalAlignment.Right) '4
        Lv_Hidden_Data.Columns.Add("urut_oto", 100, HorizontalAlignment.Left) '5
        Lv_Hidden_Data.Columns.Add("Hrg", 100, HorizontalAlignment.Left) '6
        Lv_Hidden_Data.Columns.Add("DiscP", 100, HorizontalAlignment.Left) '7
        Lv_Hidden_Data.Columns.Add("Subttl", 100, HorizontalAlignment.Left) '8
        Lv_Hidden_Data.Columns.Add("Metode Perhitungan", 100, HorizontalAlignment.Left) '9
        Lv_Hidden_Data.Columns.Add("Barcode", 200, HorizontalAlignment.Left) '10
        Lv_Hidden_Data.View = View.Details

        ListView3.Columns.Add("Gudang", 100, HorizontalAlignment.Center)
        ListView3.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        ListView3.Columns.Add("Nama Barang", 280, HorizontalAlignment.Left) '2  
        ListView3.Columns.Add("Jumlah", 80, HorizontalAlignment.Right) '3
        ListView3.Columns.Add("Pernah Retur", 80, HorizontalAlignment.Right) '4
        ListView3.Columns.Add("Satuan", 50, HorizontalAlignment.Left) '5
        ListView3.Columns.Add("*", 0, HorizontalAlignment.Right) '6
        ListView3.Columns.Add("urut_oto", 0, HorizontalAlignment.Left) '7
        ListView3.Columns.Add("Hrg", 0, HorizontalAlignment.Left) '8
        ListView3.Columns.Add("DiscP", 0, HorizontalAlignment.Left) '9
        ListView3.Columns.Add("Subttl", 0, HorizontalAlignment.Left) '10
        ListView3.Columns.Add("Metode Perhitungan", 0, HorizontalAlignment.Left) '11
        ListView3.Columns.Add("Max Retur", 80, HorizontalAlignment.Right) '12
        ListView3.View = View.Details

        ListView4.Columns.Add("No Do", 150, HorizontalAlignment.Left)
        ListView4.Columns.Add("No Retur Sementara", 150, HorizontalAlignment.Left)
        ListView4.View = View.Details

        kosong()
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then ListView3.Focus()
    End Sub


    Public Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        If TextBox1.Text.Trim.Length = 0 Then Exit Sub
        TextBox18.Text = "0"

        Try
            OpenConn()

            'tampilin data customer
            SQL = "select b.ppn, b.no_do, b.kode_customer, b.tanggal_do, b.nama_cust "
            SQL = SQL & "from rekap_sub_invoice b where "
            SQL = SQL & "b.kode_perusahaan = '" & KodePerusahaan & "' and b.no_do = '" & Trim(TextBox1.Text) & "' and "
            SQL = SQL & "b.lokasi = '" & ComboBox1.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables("MyTable")
                        For i As Integer = 0 To .Rows.Count - 1
                            kosong()

                            TextBox18.Text = .Rows(i).Item("ppn")
                            TextBox1.Text = .Rows(i).Item("no_do")
                            TextBox2.Text = .Rows(i).Item("kode_customer")
                            TextBox3.Text = .Rows(i).Item("nama_cust")
                            DateTimePicker3.Value = .Rows(i).Item("tanggal_do")

                            tampil_brg()
                        Next
                    End With
                Else
                    MessageBox.Show("No DO tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    kosong()

                    TextBox1.Text = ""
                    TextBox1.Focus()
                End If
            End Using

            Dim initials As String = ""
            SQL = "select a.Jenis "
            SQL = SQL & "from penjualan a, customers b  "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.kode_customer = b.kode_customer and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = ( "
            SQL = SQL & "select z.no_faktur "
            SQL = SQL & "from do_new z "
            SQL = SQL & "where z.kode_perusahaan = a.Kode_Perusahaan and z.no_do = '" & Trim(TextBox1.Text) & "')"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        initials = .Rows(0).Item("Jenis")
                    End If
                End With
            End Using




            CloseConn()

            OpenConn()

            'get_no_faktur(initials)

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress, Txt_Barcode.KeyPress
        If e.KeyChar = Chr(13) Then TextBox11.Focus()
    End Sub

    Private Sub TextBox8_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox8.Leave, Txt_Barcode.Leave
        'If TextBox8.Text.Trim.Length = 0 Then Exit Sub

        'For i As Integer = 0 To ListView2.Items.Count - 1
        '    If TextBox8.Text.ToUpper = ListView2.Items(i).SubItems(1).Text Then
        '        MessageBox.Show("Kode barang sudah anda masukkan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        kosongbawah()
        '        TextBox8.Focus()
        '        Exit Sub
        '    End If
        'Next

        'OpenConn()

        'SQL = "select c.kode_stock_owner, a.kode_barang, a.nama, a.satuan, a.keterangan, c.harga, c.jumlah, "
        'SQL = SQL & "(select sum(x.good_stock + x.bad_stock) from retur_pembelian z, detail_r_pembelian x where "
        'SQL = SQL & "x.kode_stock_owner = c.kode_stock_owner and z.kode_perusahaan = c.kode_perusahaan and "
        'SQL = SQL & "z.no_faktur_beli = c.no_faktur and x.kode_perusahaan = c.kode_perusahaan and x.no_retur_beli = z.no_retur_beli and "
        'SQL = SQL & "x.kode_barang = a.kode_barang and z.status is null group by x.kode_barang) as pernahretur from barang a, pembelian b, "
        'SQL = SQL & "detail_pembelian c, perusahaan d, stock_owner e where a.kode_perusahaan = d.kode_perusahaan and "
        'SQL = SQL & "b.kode_perusahaan = d.kode_perusahaan and c.kode_perusahaan = d.kode_perusahaan and e.kode_perusahaan = d.kode_perusahaan and "
        'SQL = SQL & "a.kode_barang = c.kode_barang and a.kode_stock_owner = e.kode_stock_owner and b.no_faktur = c.no_faktur and "
        'SQL = SQL & "e.kode_stock_owner = c.kode_stock_owner and e.kode_perusahaan = c.kode_perusahaan and "
        'SQL = SQL & "c.kode_perusahaan = '" & KodePerusahaan & "' and c.no_faktur = '" & Trim(TextBox1.Text) & "' and b.status is null"
        'SQL = SQL & "c.kode_barang = '" & Trim(TextBox8.Text) & "'"
        'Using Dr = Open(SQL)
        '    If Dr.Read Then
        '        ComboBox4.Text = Dr("kode_stock_owner")
        '        TextBox8.Text = Dr("kode_Barang")
        '        TextBox9.Text = Dr("nama")
        '        TextBox13.Text = Format(Dr("harga"), "N4")
        '        TextBox10.Text = Format(HilangkanTanda(Dr("jumlah")) - HilangkanTanda(General_Class.CekZERO(Dr("pernahretur"))), "N4")
        '        TextBox11.Focus()
        '    Else
        '        MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        kosongbawah()
        '        TextBox8.Focus()
        '    End If
        'End Using

        'CloseConn()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = -1 Then
            DateTimePicker2.Visible = False
        ElseIf ComboBox2.SelectedIndex = 0 Then 'kalo tunai
            DateTimePicker2.Visible = False
        ElseIf ComboBox2.SelectedIndex = 1 Then 'kalo kredit
            DateTimePicker2.Visible = True
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub TextBox4_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.Leave
        If TextBox4.Text.Trim.Length = 0 Then
            OpenConn()

            'get_no_faktur("")

            CloseConn()
        End If

        Try
            OpenConn()

            SQL = "select * from retur_penjualan where kode_perusahaan = '" & KodePerusahaan & "' and no_retur_jual = '" & Trim(TextBox4.Text) & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("no_faktur_jual")
                    tampil_brg()
                    TextBox8.Enabled = False
                    TextBox11.Enabled = False

                    ListView2.Enabled = False
                    Button2.Enabled = False
                    Button3.Enabled = True
                    Button6.Enabled = True
                    Button3.Focus()

                    tampil_brg_retur()
                Else
                    kosong()
                    TextBox1.Text = ""
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView3_ColumnWidthChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnWidthChangingEventArgs) Handles ListView3.ColumnWidthChanging
        Dim DisableColumns As Integer() = {6, 7, 8, 9, 10, 11}
        For Each DCol As Integer In DisableColumns
            If e.ColumnIndex = DCol Then
                e.Cancel = True
                e.NewWidth = ListView3.Columns(DCol).Width
            End If
        Next DCol
    End Sub

    Private Sub ListView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.DoubleClick
        If ListView3.Items.Count = 0 Then Exit Sub



        N_EMI_SD_Retur_DO_Reseller_Sementara.Txt_No_DO.Text = TextBox1.Text.Trim
        N_EMI_SD_Retur_DO_Reseller_Sementara.No_Fak_Sementara = TextBox4.Text.Trim
        N_EMI_SD_Retur_DO_Reseller_Sementara.Txt_KdBarang.Text = ListView3.FocusedItem.SubItems(1).Text
        N_EMI_SD_Retur_DO_Reseller_Sementara.Txt_NmBarang.Text = ListView3.FocusedItem.SubItems(2).Text
        N_EMI_SD_Retur_DO_Reseller_Sementara.Gudang = ListView3.FocusedItem.Text
        N_EMI_SD_Retur_DO_Reseller_Sementara.urut = ListView3.FocusedItem.SubItems(6).Text
        N_EMI_SD_Retur_DO_Reseller_Sementara.urut_oto = ListView3.FocusedItem.SubItems(7).Text
        N_EMI_SD_Retur_DO_Reseller_Sementara.hrg = HilangkanTanda(ListView3.FocusedItem.SubItems(8).Text)
        N_EMI_SD_Retur_DO_Reseller_Sementara.discp = HilangkanTanda(ListView3.FocusedItem.SubItems(9).Text)
        N_EMI_SD_Retur_DO_Reseller_Sementara.metper = ListView3.FocusedItem.SubItems(11).Text
        N_EMI_SD_Retur_DO_Reseller_Sementara.Max_Retur = HilangkanTanda(ListView3.FocusedItem.SubItems(12).Text)
        N_EMI_SD_Retur_DO_Reseller_Sementara.ShowDialog()


        Exit Sub

        'For i As Integer = 0 To ListView2.Items.Count - 1
        '    ' If ListView3.FocusedItem.Text = ListView2.Items(i).Text And ListView3.FocusedItem.SubItems(1).Text = ListView2.Items(i).SubItems(1).Text And ListView3.FocusedItem.SubItems(10).Text = ListView2.Items(i).SubItems(9).Text Then
        '    If ListView3.FocusedItem.Text = ListView2.Items(i).Text.Trim.ToUpper And ListView3.FocusedItem.SubItems(1).Text = ListView2.Items(i).SubItems(1).Text.Trim.ToUpper And ListView3.FocusedItem.SubItems(6).Text = ListView2.Items(i).SubItems(4).Text And ListView3.FocusedItem.SubItems(7).Text = ListView2.Items(i).SubItems(5).Text Then
        '        MessageBox.Show("Kode barang sudah anda masukkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        kosongbawah()
        '        Exit Sub
        '    End If
        'Next

        ''ListView2.Columns.Add("Gudang", 100, HorizontalAlignment.Center)
        ''ListView2.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        ''ListView2.Columns.Add("Nama Barang", 350, HorizontalAlignment.Left) '2  
        ''ListView2.Columns.Add("Jumlah Rtr", 130, HorizontalAlignment.Right) '3
        ''ListView2.Columns.Add("*", 0, HorizontalAlignment.Right) '4
        ''ListView2.Columns.Add("urut_oto", 0, HorizontalAlignment.Left) '5
        ''ListView2.Columns.Add("Hrg", 0, HorizontalAlignment.Left) '6
        ''ListView2.Columns.Add("DiscP", 0, HorizontalAlignment.Left) '7
        ''ListView2.Columns.Add("Subttl", 0, HorizontalAlignment.Left) '8
        ''ListView2.View = View.Details

        ''CCCCCCCCCCS()

        ''ListView3.Columns.Add("Gudang", 100, HorizontalAlignment.Center)
        ''ListView3.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        ''ListView3.Columns.Add("Nama Barang", 270, HorizontalAlignment.Left) '2  
        ''ListView3.Columns.Add("Jumlah", 70, HorizontalAlignment.Right) '3
        ''ListView3.Columns.Add("Pernah Retur", 90, HorizontalAlignment.Right) '4
        ''ListView3.Columns.Add("Satuan", 50, HorizontalAlignment.Left) '5
        ''ListView3.Columns.Add("*", 0, HorizontalAlignment.Right) '6
        ''ListView3.Columns.Add("urut_oto", 0, HorizontalAlignment.Left) '7
        ''ListView3.Columns.Add("Hrg", 0, HorizontalAlignment.Left) '8
        ''ListView3.Columns.Add("DiscP", 0, HorizontalAlignment.Left) '9
        ''ListView3.Columns.Add("Subttl", 0, HorizontalAlignment.Left) '10
        ''ListView3.View = View.Details

        'ComboBox4.Text = ListView3.FocusedItem.Text
        'TextBox8.Text = ListView3.FocusedItem.SubItems(1).Text
        'TextBox9.Text = ListView3.FocusedItem.SubItems(2).Text
        'TextBox10.Text = Val(HilangkanTanda(ListView3.FocusedItem.SubItems(3).Text)) - Val(HilangkanTanda(ListView3.FocusedItem.SubItems(4).Text))
        'TextBox11.Text = ""

        'Urut = ListView3.FocusedItem.SubItems(6).Text
        'Urut_Oto = ListView3.FocusedItem.SubItems(7).Text
        'hrg = HilangkanTanda(ListView3.FocusedItem.SubItems(8).Text)
        'discp = HilangkanTanda(ListView3.FocusedItem.SubItems(9).Text)
        'MetPer = ListView3.FocusedItem.SubItems(11).Text

        'TextBox11.Focus()
    End Sub

    Private Sub TextBox11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
        If e.KeyChar = Chr(13) Then

            If TextBox8.Text.Trim.Length = 0 Then
                MessageBox.Show("Kode barang harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox8.Focus()
                Exit Sub
            ElseIf TextBox11.Text.Trim = "0" Then
                MessageBox.Show("Jumlah retur tidak boleh nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox11.Focus()
                Exit Sub
            ElseIf (Val(TextBox11.Text)) > Val(HilangkanTanda(TextBox10.Text)) Then
                MessageBox.Show("Jumlah retur lebih besar dari jumlah Max!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox11.Focus()
                Exit Sub
            End If

            'If ListView2.Items.Count > 0 Then
            '    For i As Integer = 0 To ListView2.Items.Count - 1
            '        If ComboBox4.Text.Trim.ToUpper = ListView2.Items(i).Text.Trim.ToUpper And TextBox8.Text.Trim.ToUpper = ListView2.Items(i).SubItems(1).Text.Trim.ToUpper And Urut = ListView2.Items(i).SubItems(4).Text And Urut_Oto = ListView2.Items(i).SubItems(5).Text Then
            '            MessageBox.Show("Kode barang sudah Anda masukkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        End If
            '    Next
            'End If

            Dim foundIndex As Integer = -1
            For i As Integer = 0 To ListView2.Items.Count - 1

                Dim item = ListView2.Items(i)

                If ComboBox4.Text.Trim.ToUpper() = item.Text.Trim.ToUpper() AndAlso
                   TextBox8.Text.Trim.ToUpper() = item.SubItems(1).Text.Trim.ToUpper() AndAlso
                   ListView3.FocusedItem.SubItems(6).Text = item.SubItems(4).Text AndAlso
                   ListView3.FocusedItem.SubItems(7).Text = item.SubItems(5).Text Then

                    foundIndex = i
                    Exit For
                End If
            Next

            Dim y_hrg As Double = Val(HilangkanTanda(hrg))
            Dim y_disc As Double = Val(HilangkanTanda(Format(Val(discp), "N2")))
            Dim y_jml As Double = Val(HilangkanTanda(TextBox11.Text))
            Dim subttl As Double


            If foundIndex <> -1 Then

                If MetPer = "A" Then
                    subttl = (hrg * Val(TextBox11.Text)) - (hrg * Val(TextBox11.Text) * discp / 100)
                ElseIf MetPer = "B" Then
                    subttl = Hitung_Subtotal(y_hrg, y_disc, y_jml)
                Else
                    MessageBox.Show("error perhitungan")
                End If


                ListView2.Items(foundIndex).SubItems(3).Text = Format(Val(HilangkanTanda(ListView2.Items(foundIndex).SubItems(3).Text)) + Val(TextBox11.Text), "N4")
                ListView2.Items(foundIndex).SubItems(8).Text = Format(Val(HilangkanTanda(ListView2.Items(foundIndex).SubItems(8).Text)) + subttl, "N4")


            Else
                Dim lv As New ListViewItem
                lv = ListView2.Items.Add(ComboBox4.Text) '0
                lv.SubItems.Add(Trim(TextBox8.Text)) '1
                lv.SubItems.Add(TextBox9.Text) '2
                lv.SubItems.Add(Format(Val(TextBox11.Text), "N4")) '3
                lv.SubItems.Add(Urut) '4
                lv.SubItems.Add(Urut_Oto) '5
                lv.SubItems.Add(Format(hrg, "N4")) '6
                lv.SubItems.Add(discp) '7



                If MetPer = "A" Then
                    subttl = (hrg * Val(TextBox11.Text)) - (hrg * Val(TextBox11.Text) * discp / 100)
                ElseIf MetPer = "B" Then
                    subttl = Hitung_Subtotal(y_hrg, y_disc, y_jml)
                Else
                    MessageBox.Show("error perhitungan")
                End If

                lv.SubItems.Add(Format(subttl, "N4")) '8
                lv.SubItems.Add(MetPer) '9
            End If

            '=========================
            '=     ADD LV HIDDEN     =
            '=========================

            Dim lv2 As New ListViewItem
            lv2 = Lv_Hidden_Data.Items.Add(ComboBox4.Text) '0
            lv2.SubItems.Add(Trim(TextBox8.Text)) '1
            lv2.SubItems.Add(TextBox9.Text) '2
            lv2.SubItems.Add(Format(Val(TextBox11.Text), "N4")) '3
            lv2.SubItems.Add(Urut) '4
            lv2.SubItems.Add(Urut_Oto) '5
            lv2.SubItems.Add(Format(hrg, "N4")) '6
            lv2.SubItems.Add(discp) '7
            lv2.SubItems.Add(Format(subttl, "N4")) '8
            lv2.SubItems.Add(MetPer) '9
            lv2.SubItems.Add(Txt_Barcode.Text) '10




            kosongbawah()

            ListView3.Focus()

            HitungGrandTotal()
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox11_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox11.Leave
        If TextBox11.Text.Trim.Length = 0 Then TextBox11.Text = 0

        If (Val(TextBox11.Text)) > Val(HilangkanTanda(TextBox10.Text)) Then
            MessageBox.Show("Jumlah retur lebih besar dari jumlah Max!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox11.Text = "0"
            'TextBox11.Focus()
            Exit Sub
        End If

        'TextBox11.Focus()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListView2.Items.Count = 0 Then
            MessageBox.Show("Belum ada barang yang diretur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("No DO belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Format(DateTimePicker1.Value, "yyyyMM") <> Format(tgl_skg, "yyyyMM") Then
            MessageBox.Show("Retur tidak boleh dibulan mundur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker1.Focus()
            Exit Sub
        End If

        Dim rand As New Random
        Dim get_unik As String = "RJ" & Format(Now, "MMddHHmmss") & Format(rand.Next(0, 100000), "00000")

        get_jam()

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            'If DateDiff(DateInterval.Day, DateTimePicker3.Value, DateTimePicker1.Value) > 4 Then
            '    If CekButtonRole("retur_do_4_hari") = "T" Then
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End If

            'Dim y_jenis_nota As String = ""
            'Dim xnofak As String = ""
            'Dim flag_lns_do As String = ""

            'SQL = "select status, validasi_hasil, validasi_terima, no_faktur, flag_lunas_do from do_new where "
            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_do = '" & TextBox1.Text.Trim & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        xnofak = Dr("no_faktur")
            '        flag_lns_do = General_Class.CekNULL(Dr("flag_lunas_do"))

            '        If General_Class.CekNULL(Dr("status")) = "Y" Then
            '            Dr.Close()
            '            CloseTrans()
            '            CloseConn()
            '            MessageBox.Show("Retur tidak dapat dilanjutkan" & Chr(13) & "Karena penjualan untuk faktur ini berstatus Batal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        ElseIf General_Class.CekNULL(Dr("validasi_hasil")) <> "Y" Then
            '            Dr.Close()
            '            CloseTrans()
            '            CloseConn()
            '            MessageBox.Show("Proses tidak dapat dilanjutkan karena DO belum di validasi hasil!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        ElseIf General_Class.CekNULL(Dr("validasi_terima")) <> "Y" Then
            '            Dr.Close()
            '            CloseTrans()
            '            CloseConn()
            '            MessageBox.Show("Proses tidak dapat dilanjutkan karena DO belum di validasi terima!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        End If
            '    Else
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("No faktur ini tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            'Dim metode_pot_Stock As String = ""
            'Dim y_lokasi_gudang As String = ""
            ' If jns_trans = "T" Or flag_lns = "Y" Then
            'Dim coa_piutang As String = ""
            'Dim jns_trans As String = ""
            'Dim metode_budgeting As String = ""

            'SQL = "select a.metode_budgeting, a.metode_pot_stock, a.status, jenis, a.lokasi_gdg, a.coa_piutang, a.jenis_transaksi from penjualan a, customers b where "
            'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_customer = b.kode_customer and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & xnofak & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        y_jenis_nota = Dr("jenis")
            '        metode_pot_Stock = Dr("metode_pot_stock")
            '        y_lokasi_gudang = Dr("lokasi_gdg")
            '        coa_piutang = Dr("coa_piutang")
            '        jns_trans = Dr("jenis_transaksi")
            '        metode_budgeting = Dr("metode_budgeting")

            '        If General_Class.CekNULL(Dr("status")) = "Y" Then
            '            Dr.Close()
            '            CloseTrans()
            '            CloseConn()
            '            MessageBox.Show("Retur tidak dapat dilanjutkan" & Chr(13) & "Karena penjualan untuk faktur ini berstatus Batal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        End If
            '    Else
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("No faktur ini tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            'HitungGrandTotal()

            'Dim total_hpp As Double = 0

            'get_no_faktur(y_jenis_nota)

            'Dim flag_opm As String = "NULL"

            'SQL = "select flag_opname, buka_retur_Do from stock_owner "
            'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "kode_stock_owner = '" & ComboBox1.Text & "'" '
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("flag_opname") = "Y" Then
            '            flag_opm = "'Y'"
            '        Else
            '            flag_opm = "NULL"
            '        End If
            '    Else
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Data Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            'awal coding reza
            'Dim nilai_satu_poin As Integer = 0
            'Dim nilai_poin_dari_ngrand As Integer = 0
            'Dim check_nilai_poin_dari_ngrand As String = ""

            'SQL = "select nilai_satu_poin from do_new where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and no_do = '" & TextBox1.Text.Trim & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("nilai_satu_poin") > 1 Then
            '            nilai_satu_poin = Dr("nilai_satu_poin")
            '            nilai_poin_dari_ngrand = Math.Floor(Val(HilangkanTanda(TxtTotal.Text)) / Dr("nilai_satu_poin"))
            '            check_nilai_poin_dari_ngrand = "Y"
            '        Else
            '            check_nilai_poin_dari_ngrand = "T"
            '        End If
            '    Else
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Nilai POIN tidak tersedia", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using
            ''akhir coding Reza

            'SQL = "insert into retur_do_sementara(Kode_Perusahaan, No_Retur_jual_sementara, No_do, Tanggal, "
            'SQL = SQL & "Jam, UserID, lokasi, metode_pot_stock, NTotal, NPPN, NNilai_PPN, NGrand, hrs_updatex, xtermsc, flag_opm,nilai_satu_poin,total_poin) values "
            'SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TextBox4.Text) & "', '" & Trim(TextBox1.Text) & "', "
            'SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
            'SQL = SQL & "'" & UserID & "', '" & ComboBox1.Text & "', '" & metode_pot_Stock & "', "
            'SQL = SQL & "'" & HilangkanTanda(TextBox17.Text) & "', "
            'SQL = SQL & "'" & HilangkanTanda(TextBox18.Text) & "', "
            'SQL = SQL & "'" & HilangkanTanda(TextBox19.Text) & "', "
            'SQL = SQL & "'" & HilangkanTanda(TxtTotal.Text) & "', 'x', 'Y', " & flag_opm & ", "
            'SQL = SQL & " " & nilai_satu_poin & " , " & nilai_poin_dari_ngrand & " ) "
            'ExecuteTrans(SQL)


            Dim x As Integer = 1
            For i As Integer = 0 To ListView2.Items.Count - 1
                'Dim y_jml_jual As Double = 0
                'Dim y_pernah_retur As Double = 0

                'SQL = "Select a.no_do, a.kode_stock_owner, a.Kode_barang, b.nama, b.satuan, "
                'SQL = SQL & "sdh_selesai_validasi, no_urut, urut_oto, "

                'SQL = SQL & "isnull(("
                'SQL = SQL & "select sum(x.good_stock + x.bad_stock) from retur_do z, detail_r_do x where "
                'SQL = SQL & "z.kode_perusahaan = x.kode_perusahaan and "
                'SQL = SQL & "z.no_retur_jual = x.no_retur_jual and "
                'SQL = SQL & "x.kode_stock_owner = a.kode_stock_owner and "
                'SQL = SQL & "x.kode_barang = a.kode_barang and "
                'SQL = SQL & "x.urut_do = a.urut_oto and "
                'SQL = SQL & "z.kode_perusahaan = a.kode_perusahaan and z.no_do = a.no_do and "
                'SQL = SQL & "z.status is null group by x.kode_barang "
                'SQL = SQL & "), 0) as pernahretur "

                'SQL = SQL & "from sub_invoice a, barang b where "
                'SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and a.kode_barang = b.kode_barang and "
                'SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
                'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "a.no_do = '" & TextBox1.Text.Trim & "' and "
                'SQL = SQL & "a.kode_stock_owner = '" & ListView2.Items(i).Text & "' and a.kode_barang = '" & ListView2.Items(i).SubItems(1).Text & "' and "
                'SQL = SQL & "a.urut_oto = '" & ListView2.Items(i).SubItems(5).Text & "'"
                'SQL = SQL & "order by a.urut_oto"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        y_jml_jual = Dr("sdh_selesai_validasi")
                '        y_pernah_retur = Dr("pernahretur")

                '        Dim Kode As String = ""
                '        Dim Nama As String = ""
                '        If (Val(HilangkanTanda(ListView2.Items(i).SubItems(3).Text))) > (Dr("sdh_selesai_validasi") - Val(General_Class.CekZERO(Dr("pernahretur")))) Then
                '            Kode = Dr("Kode_Barang")
                '            Nama = Dr("nama")

                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Jumlah retur melebihi jumlah max untuk barang" & Chr(13) &
                '                               Nama & " (" & Kode & ")" &
                '                               "Proses tidak dapat dilanjutkan.", Judul)
                '            Exit Sub
                '        End If
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                'SQL = "insert into detail_r_do_sementara(Kode_Perusahaan, No_Retur_jual_sementara, Kode_Stock_Owner, urut_do, Kode_Barang, "
                'SQL = SQL & "Good_Stock, Bad_Stock, urut_detail_penjualan, Nharga, npersen_diskon, nsubtotal, Metode_Perhitungan) values "
                'SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TextBox4.Text) & "', '" & ListView2.Items(i).Text & "', '" & ListView2.Items(i).SubItems(5).Text & "', "
                'SQL = SQL & "'" & ListView2.Items(i).SubItems(1).Text & "', "
                'SQL = SQL & "" & HilangkanTanda(ListView2.Items(i).SubItems(3).Text) & ", 0, "
                'SQL = SQL & "" & HilangkanTanda(ListView2.Items(i).SubItems(4).Text) & ", "
                'SQL = SQL & "" & HilangkanTanda(ListView2.Items(i).SubItems(6).Text) & ", "
                'SQL = SQL & "" & HilangkanTanda(ListView2.Items(i).SubItems(7).Text) & ", "
                'SQL = SQL & "" & HilangkanTanda(ListView2.Items(i).SubItems(8).Text) & ", '" & ListView2.Items(i).SubItems(9).Text & "')"
                'ExecuteTrans(SQL)

                'Dim x_no_urut_det_do As Integer = 0
                'SQL = "select IDENT_CURRENT('detail_r_do_sementara') as urutan"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        x_no_urut_det_do = Dr("urutan")
                '    End If
                'End Using

                'SQL = "select no_urut from detail_r_do_sementara where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "No_Retur_jual_sementara = '" & Trim(TextBox4.Text) & "' and no_urut = '" & x_no_urut_det_do & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Not (Dr.Read) Then
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                'For j As Integer = 0 To Lv_Hidden_Data.Items.Count - 1

                '    If Lv_Hidden_Data.Items(j).SubItems(0).Text.Trim.ToUpper = ListView2.Items(i).SubItems(0).Text.Trim.ToUpper And
                '            Lv_Hidden_Data.Items(j).SubItems(1).Text.Trim.ToUpper = ListView2.Items(i).SubItems(1).Text.Trim.ToUpper And
                '            Lv_Hidden_Data.Items(j).SubItems(4).Text.Trim.ToUpper = ListView2.Items(i).SubItems(4).Text.Trim.ToUpper And
                '            Lv_Hidden_Data.Items(j).SubItems(5).Text.Trim.ToUpper = ListView2.Items(i).SubItems(5).Text.Trim.ToUpper Then

                '        SQL = "insert into det_r_do_sementara (Kode_Perusahaan, No_Retur_Jual_Sementara, Kode_Stock_Owner, Kode_Barang, Good_Stock, "
                '        SQL = SQL & "Barcode, Bad_Stock, Urut_Detail, nHarga, nPersen_Diskon, nSubtotal) "
                '        SQL = SQL & "values ('" & KodePerusahaan & "', '" & Trim(TextBox4.Text) & "', '" & Lv_Hidden_Data.Items(j).Text & "', '" & Lv_Hidden_Data.Items(j).SubItems(1).Text & "', "
                '        SQL = SQL & "'" & HilangkanTanda(Lv_Hidden_Data.Items(j).SubItems(3).Text) & "', '" & Lv_Hidden_Data.Items(j).SubItems(10).Text & "', "
                '        SQL = SQL & "0, '" & x_no_urut_det_do & "', '" & HilangkanTanda(Lv_Hidden_Data.Items(j).SubItems(6).Text) & "', "
                '        SQL = SQL & "'" & HilangkanTanda(Lv_Hidden_Data.Items(j).SubItems(7).Text) & "', '" & HilangkanTanda(Lv_Hidden_Data.Items(j).SubItems(8).Text) & "') "
                '        ExecuteTrans(SQL)
                '    End If
                'Next

                SQL = "select b.No_Urut "
                SQL = SQL & "from retur_do_sementara a "
                SQL = SQL & "inner join Detail_R_DO_sementara b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Retur_Jual_Sementara = b.No_Retur_Jual_Sementara "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Status is null "
                SQL = SQL & "and a.Flag_Val is null and a.Flag_Release is null "
                SQL = SQL & "and a.No_Retur_Jual_Sementara = '" & TextBox4.Text.Trim & "' "
                SQL = SQL & "and b.Kode_Stock_Owner ='" & ListView2.Items(i).SubItems(0).Text.Trim & "' "
                SQL = SQL & "and b.Kode_Barang = '" & ListView2.Items(i).SubItems(1).Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        SQL = "update Detail_R_DO_sementara set Good_Stock = '" & Val(HilangkanTanda(ListView2.Items(i).SubItems(10).Text.Trim)) & "', "
                        SQL = SQL & "nHarga = '" & Val(HilangkanTanda(ListView2.Items(i).SubItems(6).Text.Trim)) & "', "
                        SQL = SQL & "nPersen_Diskon = '" & Val(HilangkanTanda(ListView2.Items(i).SubItems(7).Text.Trim)) & "', "
                        SQL = SQL & "nSubtotal = '" & Val(HilangkanTanda(ListView2.Items(i).SubItems(8).Text.Trim)) & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and No_Retur_Jual_Sementara = '" & TextBox4.Text.Trim & "' "
                        SQL = SQL & "and Kode_Stock_Owner = '" & ListView2.Items(i).SubItems(0).Text.Trim & "' "
                        SQL = SQL & "and Kode_Barang = '" & ListView2.Items(i).SubItems(1).Text.Trim & "' "
                        SQL = SQL & "and No_Urut = '" & Dr("No_Urut") & "' "
                        Dr.Close()
                        ExecuteTrans(SQL)


                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Detail Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


            Next



            SQL = "update retur_do_sementara set Flag_Release = 'Y', "
            SQL = SQL & "Tanggal_Release = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Release = '" & Format(tgl_skg, "HH:mm:ss") & "', UserId_Release = '" & UserID & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Retur_Jual_Sementara = '" & TextBox4.Text.Trim & "' "
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
        TextBox1.Text = ""
        TextBox4.Text = ""
        TextBox1.Focus()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'OpenConn()

        'For i As Integer = 0 To ListView2.Items.Count - 1
        '    Execute("update barang set good_stock = good_stock + " & HilangkanTanda(ListView2.Items(i).SubItems(3).Text) & ", bad_stock = bad_stock + " & HilangkanTanda(ListView2.Items(i).SubItems(4).Text) & " where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & ListView2.Items(i).Text & "'")
        'Next

        'Execute("delete from retur_pembelian where kode_perusahaan = '" & KodePerusahaan & "' and no_retur_beli = '" & TextBox4.Text & "'")

        'kosong()
        'TextBox1.Text = ""
        'TextBox1.Focus()

        'CloseConn()
    End Sub

    Private Sub DateTimePicker1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.Leave
        Try
            OpenConn()

            'get_no_faktur("")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub ListView2_ColumnWidthChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnWidthChangingEventArgs) Handles ListView2.ColumnWidthChanging
        Dim DisableColumns As Integer() = {4, 5, 6, 7, 8, 9}
        For Each DCol As Integer In DisableColumns
            If e.ColumnIndex = DCol Then
                e.Cancel = True
                e.NewWidth = ListView2.Columns(DCol).Width
            End If
        Next DCol
    End Sub

    Private Sub ListView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Or ListView2.FocusedItem Is Nothing Then Exit Sub

        Dim Total As Double = 0

        N_EMI_SD_Retur_DO_Reseller_Sementara_Detail_Barcode.Lv_Data_Barcode.Items.Clear()
        For i As Integer = Lv_Hidden_Data.Items.Count - 1 To 0 Step -1

            If Lv_Hidden_Data.Items(i).SubItems(0).Text.Trim.ToUpper = ListView2.FocusedItem.SubItems(0).Text.ToUpper And
                    Lv_Hidden_Data.Items(i).SubItems(1).Text.Trim.ToUpper = ListView2.FocusedItem.SubItems(1).Text.ToUpper And
                    Lv_Hidden_Data.Items(i).SubItems(4).Text.Trim.ToUpper = ListView2.FocusedItem.SubItems(4).Text.ToUpper And
                    Lv_Hidden_Data.Items(i).SubItems(5).Text.Trim.ToUpper = ListView2.FocusedItem.SubItems(5).Text.ToUpper Then

                N_EMI_SD_Retur_DO_Reseller_Sementara_Detail_Barcode.Txt_KdBarang.Text = Lv_Hidden_Data.Items(i).SubItems(1).Text
                N_EMI_SD_Retur_DO_Reseller_Sementara_Detail_Barcode.Txt_NmBarang.Text = Lv_Hidden_Data.Items(i).SubItems(2).Text

                Dim lv As New ListViewItem
                lv = N_EMI_SD_Retur_DO_Reseller_Sementara_Detail_Barcode.Lv_Data_Barcode.Items.Add(Lv_Hidden_Data.Items(i).SubItems(0).Text)
                lv.SubItems.Add(Lv_Hidden_Data.Items(i).SubItems(10).Text)
                lv.SubItems.Add(Lv_Hidden_Data.Items(i).SubItems(3).Text)

                Total += Val(HilangkanTanda(Lv_Hidden_Data.Items(i).SubItems(3).Text))
            End If

        Next

        N_EMI_SD_Retur_DO_Reseller_Sementara_Detail_Barcode.Txt_Total.Text = Format(Total, "N4")
        N_EMI_SD_Retur_DO_Reseller_Sementara_Detail_Barcode.ShowDialog()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        kosong()
        TextBox1.Text = ""
        TextBox1.Focus()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        OpenConn()

        cetak()

        CloseConn()

        kosong()
        TextBox1.Text = ""
        TextBox1.Focus()
        A_Place_For_Printing.Show()
    End Sub

    Private Sub HapusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem.Click
        If ListView2.Items.Count = 0 Then Exit Sub
        '$$$$$$$$$$$$$
        'TextBox8.Text = ListView2.FocusedItem.SubItems(1).Text
        'TextBox8_Leave(ListView2, e)
        'TextBox11.Focus()
        'TextBox8.Text = ListView2.FocusedItem.SubItems(1).Text
        'TextBox9.Text = ListView3.FocusedItem.SubItems(2).Text
        'TextBox13.Text = ListView3.FocusedItem.SubItems(3).Text
        'TextBox10.Text = Val(HilangkanTanda(ListView3.FocusedItem.SubItems(4).Text)) - Val(HilangkanTanda(ListView3.FocusedItem.SubItems(5).Text))
        'TextBox11.Text = "0"
        'TextBox12.Text = "0"zd
        'TextBox5.Text = ListView3.FocusedItem.SubItems(7).Text
        'TextBox6.Text = ListView3.FocusedItem.SubItems(8).Text
        'TextBox14.Text = "0"
        'TextBox11.Focus()

        If MessageBox.Show("Yakin Ingin Menghapus Baris Ini???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = vbNo Then Exit Sub

        For i As Integer = Lv_Hidden_Data.Items.Count - 1 To 0 Step -1
            If Lv_Hidden_Data.Items(i).SubItems(0).Text.Trim.ToUpper = ListView2.FocusedItem.SubItems(0).Text.ToUpper And
                    Lv_Hidden_Data.Items(i).SubItems(1).Text.Trim.ToUpper = ListView2.FocusedItem.SubItems(1).Text.ToUpper And
                    Lv_Hidden_Data.Items(i).SubItems(4).Text.Trim.ToUpper = ListView2.FocusedItem.SubItems(4).Text.ToUpper And
                    Lv_Hidden_Data.Items(i).SubItems(5).Text.Trim.ToUpper = ListView2.FocusedItem.SubItems(5).Text.ToUpper Then

                Lv_Hidden_Data.Items.RemoveAt(i)
            End If
        Next

        ListView2.FocusedItem.Remove()


        HitungGrandTotal()
    End Sub

    Private Sub ListView4_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView4.DoubleClick
        If ListView4.Items.Count = 0 Or ListView4.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih no do yang mau validasi !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        TextBox1.Text = ListView4.FocusedItem.Text
        TextBox4.Text = ListView4.FocusedItem.SubItems(1).Text
        TextBox1_Leave(ListView4, e)

        Try
            OpenConn()

            ListView2.Items.Clear()
            SQL = "select a.Kode_Stock_Owner,a.Urut_DO,a.Kode_Barang,b.Nama,a.Good_Stock, "
            SQL = SQL & "a.Urut_Detail_Penjualan, a.Metode_Perhitungan,a.nHarga,a.nPersen_Diskon,a.nSubtotal, "
            SQL = SQL & "isnull((select sum(c.Good_Stock) from Det_R_DO_Sementara c where "
            SQL = SQL & "c.Kode_Perusahaan = a.Kode_Perusahaan and "
            SQL = SQL & "c.No_Retur_Jual_Sementara = a.No_Retur_Jual_Sementara and "
            SQL = SQL & "c.Kode_Barang = a.Kode_Barang and "
            SQL = SQL & "c.Kode_Stock_Owner = a.Kode_Stock_Owner "
            SQL = SQL & "group by c.Kode_Barang), 0) as det_good_stock "
            SQL = SQL & "from Detail_R_DO_sementara a, Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Retur_Jual_Sementara = '" & TextBox4.Text & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView2.Items.Add(dr("kode_stock_owner"))
                    Lvw.SubItems.Add(dr("kode_barang"))
                    Lvw.SubItems.Add(dr("nama"))
                    Lvw.SubItems.Add(Format(dr("good_stock"), "N4"))
                    Lvw.SubItems.Add(dr("urut_detail_penjualan"))
                    Lvw.SubItems.Add(dr("urut_do"))
                    Lvw.SubItems.Add(Format(dr("nharga"), "N4"))
                    Lvw.SubItems.Add(dr("npersen_diskon"))
                    Lvw.SubItems.Add(Format(dr("nsubtotal"), "N4"))
                    Lvw.SubItems.Add(dr("metode_perhitungan"))
                    Lvw.SubItems.Add(Format(dr("det_good_stock"), "N4"))
                Loop
            End Using

            Lv_Hidden_Data.Items.Clear()
            SQL = "select b.Kode_Stock_Owner, b.Kode_Barang, b.urut_detail_penjualan, b.Urut_DO, d.Nama as Nama_Barang, b.No_Urut, c.Good_Stock, c.nHarga, c.nPersen_Diskon, c.nSubtotal, b.Metode_Perhitungan, c.Barcode, c.Urut_Oto "
            SQL = SQL & "from retur_do_sementara a "
            SQL = SQL & "inner join detail_r_do_sementara b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Retur_Jual_Sementara = b.No_Retur_Jual_Sementara "
            SQL = SQL & "inner join det_r_do_sementara c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Retur_Jual_Sementara = c.No_Retur_Jual_Sementara and b.No_Urut = c.Urut_Detail "
            SQL = SQL & "inner join Barang d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "where a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Retur_Jual_Sementara = '" & TextBox4.Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Hidden_Data.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Good_Stock"))
                    Lv.SubItems.Add(Dr("urut_detail_penjualan"))
                    Lv.SubItems.Add(Dr("urut_do"))
                    Lv.SubItems.Add(Dr("nHarga"))
                    Lv.SubItems.Add(Dr("nPersen_Diskon"))
                    Lv.SubItems.Add(Dr("nSubtotal"))
                    Lv.SubItems.Add(Dr("Metode_Perhitungan"))
                    Lv.SubItems.Add(Dr("Barcode"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub

End Class