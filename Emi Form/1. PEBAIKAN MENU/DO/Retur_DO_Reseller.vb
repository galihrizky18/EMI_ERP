Public Class Retur_DO_Reseller
    Dim Urut As String
    Dim Urut_Oto As String

    Dim arrInisialFaktur As New ArrayList
    Dim rv As Integer

    Dim hrg As Double
    Dim discp As Double
    Dim MetPer As String
    Dim no_retur_s As String


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
            SQL = SQL & "), 0) as pernahretur, a.harga, a.persen_diskon, a.subtotal_baru, a.metode_perhitungan "

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
        OpenConn()

        SQL = "select c.kode_stock_owner, a.kode_barang, b.nama, a.harga, a.good_stock, a.bad_stock from detail_r_penjualan a, barang b, stock_owner c, perusahaan d where b.kode_perusahaan = c.kode_perusahaan and c.kode_perusahaan = d.kode_perusahaan and a.kode_barang = b.kode_barang and a.kode_stock_owner = b.kode_stock_owner and a.kode_stock_owner = c.kode_stock_owner and a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_retur_jual = '" & Trim(TextBox4.Text) & "'"
        Using Dr = Open(SQL)
            ListView2.Items.Clear()

            Dim lvw As New ListViewItem
            Do While Dr.Read
                lvw = ListView2.Items.Add(Dr("kode_stock_owner"))
                lvw.SubItems.Add(Dr("kode_barang"))
                lvw.SubItems.Add(Dr("nama"))
                lvw.SubItems.Add(Format(Dr("harga"), "N4"))
                lvw.SubItems.Add(Format(Dr("good_stock"), "N4"))
                lvw.SubItems.Add(Format(Dr("bad_stock"), "N4"))
                lvw.SubItems.Add(Format((Dr("good_stock") + Dr("bad_stock")) * Dr("harga"), "N4"))
            Loop

        End Using

        HitungGrandTotal()
        CloseConn()

    End Sub

    Private Sub kosongbawah()
        hrg = 0
        discp = 0
        MetPer = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""

        Try
            OpenConn()

            ComboBox4.Items.Clear()
            Using dr = OpenTrans("Select kode_stock_owner From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner")
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
        TextBox4.Text = Rj_DO & jenis & arrInisialFaktur.Item(ComboBox1.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                             General_Class.Get_Last_Number2("retur_do", "no_retur_jual", JumlahDigit,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_retur_jual,1," & Len(Rj_DO & jenis) + Len(arrInisialFaktur.Item(ComboBox1.SelectedIndex)) + 6 & ")", Rj_DO & jenis & arrInisialFaktur.Item(ComboBox1.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))
    End Sub

    Private Sub kosong()
        rv = 0

        TextBox17.Text = "0"
        TextBox18.Text = "0"
        TextBox19.Text = "0"
        TxtTotal.Text = "0"

        DateTimePicker1.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text)
        TextBox3.Text = ""
        ComboBox2.Items.Clear() : ComboBox2.SelectedIndex = -1
        ComboBox2.Items.Add("Tunai")
        ComboBox2.Items.Add("Non Tunai")
        DateTimePicker2.Value = Now
        DateTimePicker3.Value = Now
        TextBox2.Text = ""
        TextBox7.Text = "0"

        TextBox1.Enabled = False

        ListView3.Items.Clear()
        ListView2.Items.Clear()

        kosongbawah()

        Button2.Enabled = True
        Button3.Enabled = False
        Button6.Enabled = False
        TextBox8.Enabled = False
        TextBox11.Enabled = False
        ListView2.Enabled = True

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
            SQL = SQL & "and status is null and lokasi = '" & ComboBox1.Text & "' and flag_val is null and flag_release = 'Y' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView4.Items.Add(dr("no_do"))
                    Lvw.SubItems.Add(dr("no_retur_jual_sementara"))
                Loop
            End Using

            get_no_faktur("")

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

        'ListView2.Columns.Add("Gudang", 80, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        'ListView2.Columns.Add("Nama Barang", 340, HorizontalAlignment.Left) '2
        'ListView2.Columns.Add("Harga Jual", 0, HorizontalAlignment.Right) '3
        'ListView2.Columns.Add("Jumlah", 62, HorizontalAlignment.Right) '4
        'ListView2.Columns.Add("Bad Stock", 0, HorizontalAlignment.Right) '5
        'ListView2.Columns.Add("Disc(%)", 0, HorizontalAlignment.Right) '6
        'ListView2.Columns.Add("Disc(Rp.)", 0, HorizontalAlignment.Right) '7
        'ListView2.Columns.Add("Sub Total", 0, HorizontalAlignment.Right) '8
        'ListView2.Columns.Add("*", 0, HorizontalAlignment.Right) '9
        'ListView2.Columns.Add("Serial Number", 0, HorizontalAlignment.Left) '10
        'ListView2.Columns.Add("Pakai SN", 0, HorizontalAlignment.Left) '11
        'ListView2.Columns.Add("Flag Budgeting BS", 0, HorizontalAlignment.Left) '12
        'ListView2.Columns.Add("Flag Budgeting Mbl", 0, HorizontalAlignment.Left) '13
        'ListView2.Columns.Add("Hrg Terendah", 0, HorizontalAlignment.Left) '14
        'ListView2.Columns.Add("Hrg Agen", 0, HorizontalAlignment.Left) '15
        'ListView2.Columns.Add("Kode Kategori2", 0, HorizontalAlignment.Left) '16
        'ListView2.Columns.Add("Flag Budgeting 2", 0, HorizontalAlignment.Left) '17 

        ListView2.Columns.Add("Gudang", 130, HorizontalAlignment.Center)
        ListView2.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        ListView2.Columns.Add("Nama Barang", 350, HorizontalAlignment.Left) '2  
        ListView2.Columns.Add("Jumlah Rtr", 130, HorizontalAlignment.Right) '3
        ListView2.Columns.Add("*", 0, HorizontalAlignment.Right) '4
        ListView2.Columns.Add("urut_oto", 0, HorizontalAlignment.Left) '5
        ListView2.Columns.Add("Hrg", 0, HorizontalAlignment.Left) '6
        ListView2.Columns.Add("DiscP", 0, HorizontalAlignment.Left) '7
        ListView2.Columns.Add("Subttl", 0, HorizontalAlignment.Left) '8
        ListView2.Columns.Add("Metode Perhitungan", 0, HorizontalAlignment.Left) '9
        ListView2.View = View.Details

        Lv_Hidden_Data.Columns.Add("Gudang", 100, HorizontalAlignment.Center)
        Lv_Hidden_Data.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        Lv_Hidden_Data.Columns.Add("Nama Barang", 100, HorizontalAlignment.Left) '2  
        Lv_Hidden_Data.Columns.Add("Jumlah Rtr", 100, HorizontalAlignment.Right) '3
        Lv_Hidden_Data.Columns.Add("Hrg", 100, HorizontalAlignment.Left) '6
        Lv_Hidden_Data.Columns.Add("DiscP", 100, HorizontalAlignment.Left) '7
        Lv_Hidden_Data.Columns.Add("Subttl", 100, HorizontalAlignment.Left) '8
        Lv_Hidden_Data.Columns.Add("Metode Perhitungan", 100, HorizontalAlignment.Left) '9
        Lv_Hidden_Data.Columns.Add("Barcode", 100, HorizontalAlignment.Left) '10
        Lv_Hidden_Data.View = View.Details

        ListView4.Columns.Add("No Do", 150, HorizontalAlignment.Left)
        ListView4.Columns.Add("No Retur Sementara", 150, HorizontalAlignment.Left)
        ListView4.View = View.Details

        ListView3.Columns.Add("Gudang", 130, HorizontalAlignment.Center)
        ListView3.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        ListView3.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left) '2  
        ListView3.Columns.Add("Jumlah", 110, HorizontalAlignment.Right) '3
        ListView3.Columns.Add("Pernah Retur", 110, HorizontalAlignment.Right) '4
        ListView3.Columns.Add("Satuan", 50, HorizontalAlignment.Center) '5
        ListView3.Columns.Add("*", 0, HorizontalAlignment.Right) '6
        ListView3.Columns.Add("urut_oto", 0, HorizontalAlignment.Left) '7
        ListView3.Columns.Add("Hrg", 0, HorizontalAlignment.Left) '8
        ListView3.Columns.Add("DiscP", 0, HorizontalAlignment.Left) '9
        ListView3.Columns.Add("Subttl", 0, HorizontalAlignment.Left) '10
        ListView3.Columns.Add("Metode Perhitungan", 0, HorizontalAlignment.Left) '11
        ListView3.View = View.Details

        kosong()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then ListView3.Focus()
    End Sub

    Private Sub TextBox1_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles TextBox1.Layout

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

            CloseConn()

            OpenConn()

            get_no_faktur("")

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Chr(13) Then TextBox11.Focus()
    End Sub

    Private Sub TextBox8_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox8.Leave
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

            get_no_faktur("")

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
        Dim DisableColumns As Integer() = {3, 4, 8, 9, 10, 11, 12, 13, 14, 15, 16}
        For Each DCol As Integer In DisableColumns
            If e.ColumnIndex = DCol Then
                e.Cancel = True
                e.NewWidth = ListView3.Columns(DCol).Width
            End If
        Next DCol
    End Sub

    Private Sub ListView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.DoubleClick

        Exit Sub

        If ListView3.Items.Count = 0 Then Exit Sub
        For i As Integer = 0 To ListView2.Items.Count - 1
            ' If ListView3.FocusedItem.Text = ListView2.Items(i).Text And ListView3.FocusedItem.SubItems(1).Text = ListView2.Items(i).SubItems(1).Text And ListView3.FocusedItem.SubItems(10).Text = ListView2.Items(i).SubItems(9).Text Then
            If ListView3.FocusedItem.Text = ListView2.Items(i).Text.Trim.ToUpper And ListView3.FocusedItem.SubItems(1).Text = ListView2.Items(i).SubItems(1).Text.Trim.ToUpper And ListView3.FocusedItem.SubItems(6).Text = ListView2.Items(i).SubItems(4).Text And ListView3.FocusedItem.SubItems(7).Text = ListView2.Items(i).SubItems(5).Text Then
                MessageBox.Show("Kode barang sudah anda masukkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                kosongbawah()
                Exit Sub
            End If
        Next

        'ListView2.Columns.Add("Gudang", 100, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        'ListView2.Columns.Add("Nama Barang", 350, HorizontalAlignment.Left) '2  
        'ListView2.Columns.Add("Jumlah Rtr", 130, HorizontalAlignment.Right) '3
        'ListView2.Columns.Add("*", 0, HorizontalAlignment.Right) '4
        'ListView2.Columns.Add("urut_oto", 0, HorizontalAlignment.Left) '5
        'ListView2.Columns.Add("Hrg", 0, HorizontalAlignment.Left) '6
        'ListView2.Columns.Add("DiscP", 0, HorizontalAlignment.Left) '7
        'ListView2.Columns.Add("Subttl", 0, HorizontalAlignment.Left) '8
        'ListView2.View = View.Details

        'CCCCCCCCCCS()

        'ListView3.Columns.Add("Gudang", 100, HorizontalAlignment.Center)
        'ListView3.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        'ListView3.Columns.Add("Nama Barang", 270, HorizontalAlignment.Left) '2  
        'ListView3.Columns.Add("Jumlah", 70, HorizontalAlignment.Right) '3
        'ListView3.Columns.Add("Pernah Retur", 90, HorizontalAlignment.Right) '4
        'ListView3.Columns.Add("Satuan", 50, HorizontalAlignment.Left) '5
        'ListView3.Columns.Add("*", 0, HorizontalAlignment.Right) '6
        'ListView3.Columns.Add("urut_oto", 0, HorizontalAlignment.Left) '7
        'ListView3.Columns.Add("Hrg", 0, HorizontalAlignment.Left) '8
        'ListView3.Columns.Add("DiscP", 0, HorizontalAlignment.Left) '9
        'ListView3.Columns.Add("Subttl", 0, HorizontalAlignment.Left) '10
        'ListView3.View = View.Details

        ComboBox4.Text = ListView3.FocusedItem.Text
        TextBox8.Text = ListView3.FocusedItem.SubItems(1).Text
        TextBox9.Text = ListView3.FocusedItem.SubItems(2).Text
        TextBox10.Text = Val(HilangkanTanda(ListView3.FocusedItem.SubItems(3).Text)) - Val(HilangkanTanda(ListView3.FocusedItem.SubItems(4).Text))
        TextBox11.Text = ""

        Urut = ListView3.FocusedItem.SubItems(6).Text
        Urut_Oto = ListView3.FocusedItem.SubItems(7).Text
        hrg = HilangkanTanda(ListView3.FocusedItem.SubItems(8).Text)
        discp = HilangkanTanda(ListView3.FocusedItem.SubItems(9).Text)
        MetPer = ListView3.FocusedItem.SubItems(11).Text

        TextBox11.Focus()
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

            If ListView2.Items.Count > 0 Then
                For i As Integer = 0 To ListView2.Items.Count - 1
                    If ComboBox4.Text.Trim.ToUpper = ListView2.Items(i).Text.Trim.ToUpper And TextBox8.Text.Trim.ToUpper = ListView2.Items(i).SubItems(1).Text.Trim.ToUpper And Urut = ListView2.Items(i).SubItems(4).Text And Urut_Oto = ListView2.Items(i).SubItems(5).Text Then
                        MessageBox.Show("Kode barang sudah Anda masukkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Next
            End If

            Dim lv As New ListViewItem
            lv = ListView2.Items.Add(ComboBox4.Text)
            lv.SubItems.Add(Trim(TextBox8.Text))
            lv.SubItems.Add(TextBox9.Text)
            lv.SubItems.Add(Format(Val(TextBox11.Text), "N4"))
            lv.SubItems.Add(Urut)
            lv.SubItems.Add(Urut_Oto)
            lv.SubItems.Add(Format(hrg, "N4"))
            lv.SubItems.Add(discp)

            '''cek'''
            Dim y_hrg As Double = Val(HilangkanTanda(hrg))
            Dim y_disc As Double = Val(HilangkanTanda(Format(Val(discp), "N2")))
            Dim y_jml As Double = Val(HilangkanTanda(TextBox11.Text))
            Dim subttl As Double

            If MetPer = "A" Then

                subttl = (hrg * Val(TextBox11.Text)) - (hrg * Val(TextBox11.Text) * discp / 100)


            ElseIf MetPer = "B" Then

                subttl = Hitung_Subtotal(y_hrg, y_disc, y_jml)

            Else
                MessageBox.Show("error perhitungan")

            End If
            lv.SubItems.Add(Format(subttl, "N4"))
            lv.SubItems.Add(MetPer)

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

        If Format(DateTimePicker1.Value, "yyyyMM") <> Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyyMM") Then
            MessageBox.Show("Retur tidak boleh dibulan mundur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker1.Focus()
            Exit Sub
        End If

        Dim rand As New Random
        Dim get_unik As String = "RJ" & Format(Now, "MMddHHmmss") & Format(rand.Next(0, 100000), "00000")

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            If DateDiff(DateInterval.Day, DateTimePicker3.Value, DateTimePicker1.Value) > 4 Then
                If CekButtonRole("retur_do_4_hari") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If

            '=======================================
            ' cek status penjualannya batal/tidak
            '=======================================

            'Dim total_hpp As Double = 0

            'Dim ttlbaru As Double = 0
            'Dim pointbaru As Double = 0
            'Dim pointlama As Double = 0
            'Dim flag_lipat As Double = 0
            'Dim y_kurs As Double = 0
            'Dim y_pakai_point As String = ""
            'Dim y_customers As String = ""
            'Dim y_lokasi As String = ""
            'Dim y_lokasi_gudang As String = ""
            'Dim y_jenis_nota As String = ""

            'Dim jns_trans As String = ""
            'Dim flag_lns As String = ""
            'Dim coa_piutang As String = ""
            'Dim flag_cabang_sdr As String = ""

            'Dim persen_insentif_1_lama As String = ""
            'Dim persen_insentif_2_lama As String = ""
            'Dim akun_biaya_insentif_1_lama As String = ""
            'Dim akun_biaya_insentif_2_lama As String = ""
            'Dim akun_hutang_insentif_1_lama As String = ""
            'Dim akun_hutang_insentif_2_lama As String = ""
            'Dim nama_customer As String = ""
            'Dim kepala_lama As String = ""
            'Dim kode_sales As String = ""

            Dim y_jenis_nota As String = ""
            Dim xnofak As String = ""
            Dim flag_lns_do As String = ""

            SQL = "select status, validasi_hasil, validasi_terima, no_faktur, flag_lunas_do from do_new where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_do = '" & TextBox1.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    xnofak = Dr("no_faktur")
                    flag_lns_do = General_Class.CekNULL(Dr("flag_lunas_do"))

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Retur tidak dapat dilanjutkan" & Chr(13) & "Karena penjualan untuk faktur ini berstatus Batal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("validasi_hasil")) <> "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena DO belum di validasi hasil!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("validasi_terima")) <> "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena DO belum di validasi terima!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No faktur ini tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim metode_pot_Stock As String = ""
            Dim y_lokasi_gudang As String = ""
            ' If jns_trans = "T" Or flag_lns = "Y" Then
            Dim coa_piutang As String = ""
            Dim jns_trans As String = ""
            Dim metode_budgeting As String = ""

            SQL = "select a.metode_budgeting, a.metode_pot_stock, a.status, jenis, a.lokasi_gdg, a.coa_piutang, a.jenis_transaksi from penjualan a, customers b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_customer = b.kode_customer and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & xnofak & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    y_jenis_nota = Dr("jenis")
                    metode_pot_Stock = Dr("metode_pot_stock")
                    y_lokasi_gudang = Dr("lokasi_gdg")
                    coa_piutang = Dr("coa_piutang")
                    jns_trans = Dr("jenis_transaksi")
                    metode_budgeting = Dr("metode_budgeting")

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Retur tidak dapat dilanjutkan" & Chr(13) & "Karena penjualan untuk faktur ini berstatus Batal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No faktur ini tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            HitungGrandTotal()

            Dim total_hpp As Double = 0

            get_no_faktur(y_jenis_nota)

            Dim flag_opm As String = ""

            SQL = "select flag_opname, buka_retur_Do from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_stock_owner = '" & ComboBox1.Text & "'" '
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("flag_opname") = "Y" Then
                        If Dr("buka_retur_Do") = 0 Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(err_msg_opname, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf Dr("buka_retur_Do") > 0 Then
                            Dr.Close()
                            SQL = "update stock_owner set buka_retur_Do = buka_retur_Do - 1 "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & ComboBox1.Text & "'"
                            ExecuteTrans(SQL)

                            flag_opm = "'Y'"
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        flag_opm = "NULL"
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            'awal coding reza
            Dim nilai_satu_poin As Integer = 0
            Dim nilai_poin_dari_ngrand As Integer = 0
            Dim check_nilai_poin_dari_ngrand As String = ""

            SQL = "select nilai_satu_poin from do_new where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_do = '" & TextBox1.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("nilai_satu_poin") > 1 Then
                        nilai_satu_poin = Dr("nilai_satu_poin")
                        nilai_poin_dari_ngrand = Math.Floor(Val(HilangkanTanda(TxtTotal.Text)) / Dr("nilai_satu_poin"))
                        check_nilai_poin_dari_ngrand = "Y"
                    Else
                        check_nilai_poin_dari_ngrand = "T"
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Nilai POIN tidak tersedia", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using
            'akhir coding Reza

            SQL = "insert into retur_do(Kode_Perusahaan, No_Retur_jual, No_do, Tanggal, "
            SQL = SQL & "Jam, UserID, lokasi, metode_pot_stock, NTotal, NPPN, NNilai_PPN, NGrand, hrs_updatex, xtermsc, flag_opm,nilai_satu_poin,total_poin) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TextBox4.Text) & "', '" & Trim(TextBox1.Text) & "', "
            SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
            SQL = SQL & "'" & UserID & "', '" & ComboBox1.Text & "', '" & metode_pot_Stock & "', "
            SQL = SQL & "'" & HilangkanTanda(TextBox17.Text) & "', "
            SQL = SQL & "'" & HilangkanTanda(TextBox18.Text) & "', "
            SQL = SQL & "'" & HilangkanTanda(TextBox19.Text) & "', "
            SQL = SQL & "'" & HilangkanTanda(TxtTotal.Text) & "', 'x', 'Y', " & flag_opm & ", "
            SQL = SQL & " " & nilai_satu_poin & " , " & nilai_poin_dari_ngrand & " ) "
            ExecuteTrans(SQL)


            ' awal Koding Reza

            If check_nilai_poin_dari_ngrand = "Y" Then
                SQL = "select kode_perusahaan, poin from customers where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & TextBox2.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        If Dr("poin") < nilai_poin_dari_ngrand Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses akan membuat poin menjadi negatif!!", Judul, MessageBoxButtons.OK)
                            Exit Sub
                        Else
                            Dr.Close()

                            SQL = "update customers set poin = poin - " & nilai_poin_dari_ngrand & " "
                            SQL = SQL & "where kode_customer = '" & TextBox2.Text & "' "
                            ExecuteTrans(SQL)
                        End If

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Customer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            End If

            ' akhir koding Reza


            Dim x As Integer = 1
            For i As Integer = 0 To ListView2.Items.Count - 1
                Dim y_jml_jual As Double = 0
                Dim y_pernah_retur As Double = 0

                'SQL = "select c.no_urut, c.kode_stock_owner, a.kode_barang, a.nama, a.satuan, c.keterangan, c.harga, c.jumlah, "
                'SQL = SQL & "c.persen_diskon, c.nilai_diskon, "

                'SQL = SQL & "isnull((select sum(x.good_stock + x.bad_stock) from retur_penjualan z, detail_r_penjualan x "
                'SQL = SQL & "where x.kode_stock_owner = c.kode_stock_owner and z.kode_perusahaan = c.kode_perusahaan "
                'SQL = SQL & "and z.no_faktur_jual = c.no_faktur and x.kode_perusahaan = c.kode_perusahaan and "
                'SQL = SQL & "x.no_retur_jual = z.no_retur_jual and x.kode_barang = a.kode_barang and x.urut = c.no_urut and z.status is null group by x.kode_barang), 0) as pernahretur "

                'SQL = SQL & "from barang a, penjualan b, detail_penjualan c, perusahaan d, stock_owner e where a.kode_perusahaan = b.kode_perusahaan "
                'SQL = SQL & "and b.kode_perusahaan = c.kode_perusahaan and c.kode_perusahaan = d.kode_perusahaan and "
                'SQL = SQL & "d.kode_perusahaan = e.kode_perusahaan and a.kode_barang = c.kode_barang and "
                'SQL = SQL & "a.kode_stock_owner = e.kode_stock_owner and b.no_faktur = c.no_faktur and "
                'SQL = SQL & "e.kode_stock_owner = c.kode_stock_owner and e.kode_perusahaan = c.kode_perusahaan and "
                'SQL = SQL & "c.kode_perusahaan = '" & KodePerusahaan & "' and c.no_faktur = '" & Trim(TextBox1.Text) & "' and b.status is null and "
                'SQL = SQL & "a.kode_stock_owner = '" & ListView2.Items(i).Text & "' and a.kode_barang = '" & ListView2.Items(i).SubItems(1).Text & "' and "
                'SQL = SQL & "c.no_urut = '" & ListView2.Items(i).SubItems(9).Text & "'"


                SQL = "Select a.no_do, a.kode_stock_owner, a.Kode_barang, b.nama, b.satuan, "
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
                SQL = SQL & "), 0) as pernahretur "

                SQL = SQL & "from sub_invoice a, barang b where "
                SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and a.kode_barang = b.kode_barang and "
                SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "a.no_do = '" & TextBox1.Text.Trim & "' and "
                SQL = SQL & "a.kode_stock_owner = '" & ListView2.Items(i).Text & "' and a.kode_barang = '" & ListView2.Items(i).SubItems(1).Text & "' and "
                SQL = SQL & "a.urut_oto = '" & ListView2.Items(i).SubItems(5).Text & "'"
                SQL = SQL & "order by a.urut_oto"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        y_jml_jual = Dr("sdh_selesai_validasi")
                        y_pernah_retur = Dr("pernahretur")

                        Dim Kode As String = ""
                        Dim Nama As String = ""
                        If (Val(HilangkanTanda(ListView2.Items(i).SubItems(3).Text))) > (Dr("sdh_selesai_validasi") - Val(General_Class.CekZERO(Dr("pernahretur")))) Then
                            Kode = Dr("Kode_Barang")
                            Nama = Dr("nama")

                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jumlah retur melebihi jumlah max untuk barang" & Chr(13) &
                                               Nama & " (" & Kode & ")" &
                                               "Proses tidak dapat dilanjutkan.", Judul)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'ListView2.Columns.Add("Gudang", 100, HorizontalAlignment.Center)
                'ListView2.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
                'ListView2.Columns.Add("Nama Barang", 310, HorizontalAlignment.Left) '2  
                'ListView2.Columns.Add("Jumlah Rtr", 90, HorizontalAlignment.Right) '3
                'ListView2.Columns.Add("*", 0, HorizontalAlignment.Right) '4
                'ListView2.Columns.Add("urut_oto", 0, HorizontalAlignment.Left) '5
                'ListView2.View = View.Details

                SQL = "insert into detail_r_do(Kode_Perusahaan, No_Retur_jual, Kode_Stock_Owner, urut_do, Kode_Barang, "
                SQL = SQL & "Good_Stock, Bad_Stock, urut_detail_penjualan, Nharga, npersen_diskon, nsubtotal, Metode_Perhitungan) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TextBox4.Text) & "', '" & ListView2.Items(i).Text & "', '" & ListView2.Items(i).SubItems(5).Text & "', "
                SQL = SQL & "'" & ListView2.Items(i).SubItems(1).Text & "', "
                SQL = SQL & "" & HilangkanTanda(ListView2.Items(i).SubItems(3).Text) & ", 0, "
                SQL = SQL & "" & HilangkanTanda(ListView2.Items(i).SubItems(4).Text) & ", "
                SQL = SQL & "" & HilangkanTanda(ListView2.Items(i).SubItems(6).Text) & ", "
                SQL = SQL & "" & HilangkanTanda(ListView2.Items(i).SubItems(7).Text) & ", "
                SQL = SQL & "" & HilangkanTanda(ListView2.Items(i).SubItems(8).Text) & ", '" & ListView2.Items(i).SubItems(9).Text & "')"
                ExecuteTrans(SQL)

                Dim x_no_urut_det_do As Integer = 0
                SQL = "select IDENT_CURRENT('detail_r_do') as urutan"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        x_no_urut_det_do = Dr("urutan")
                    End If
                End Using

                SQL = "select no_urut from detail_r_do where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Retur_jual = '" & Trim(TextBox4.Text) & "' and no_urut = '" & x_no_urut_det_do & "'"
                Using Dr = OpenTrans(SQL)
                    If Not (Dr.Read) Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                If metode_pot_Stock = "B" Then

                    SQL = "select nama from barang where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & ListView2.Items(i).Text & "' and "
                    SQL = SQL & "kode_barang = '" & ListView2.Items(i).SubItems(1).Text & "'"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then

                                SQL = "update barang set good_stock = good_stock + " & HilangkanTanda(ListView2.Items(i).SubItems(3).Text) & " "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_barang = '" & ListView2.Items(i).SubItems(1).Text & "' and "
                                SQL = SQL & "kode_stock_owner = '" & ListView2.Items(i).Text & "'"
                                ExecuteTrans(SQL)
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                Exit Sub
                            End If
                        End With
                    End Using


                    Dim jml_di_retur As Double = 0

                    '========================================
                    '=     GET DETAIL BARCODE SEMENTARA     =
                    '========================================
                    SQL = "select a.No_Retur_Jual_Sementara, a.No_DO, b.Kode_Stock_Owner, b.Kode_Barang, c.Barcode, c.Good_Stock, e.No_Urut, e.Urut_Oto "
                    SQL = SQL & "from retur_do_sementara a "
                    SQL = SQL & "inner join detail_r_do_sementara b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Retur_Jual_Sementara = b.No_Retur_Jual_Sementara "
                    SQL = SQL & "inner join det_r_do_sementara c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Retur_Jual_Sementara = c.No_Retur_Jual_Sementara and b.No_Urut = c.Urut_Detail "
                    SQL = SQL & "inner join do_new d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_DO = d.No_DO "
                    SQL = SQL & "inner join detail_do_new e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.No_DO = e.No_DO and b.Kode_Stock_Owner = e.Kode_Stock_Owner and b.Kode_Barang = e.Kode_Barang "
                    SQL = SQL & "where a.Status is null and d.Status is null "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Retur_Jual_Sementara = '" & no_retur_s & "' "
                    SQL = SQL & "and b.Kode_Stock_Owner = '" & ListView2.Items(i).SubItems(0).Text & "' "
                    SQL = SQL & "and b.Kode_Barang = '" & ListView2.Items(i).SubItems(1).Text & "' "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For j As Integer = 0 To .Rows.Count - 1

                                    Dim Barcode_DO_Sementara As String = .Rows(j).Item("Barcode")
                                    Dim Jumlah_Retur As Double = Val(HilangkanTanda(.Rows(j).Item("Good_Stock")))
                                    Dim Urut_Detail_Penjualan As String = .Rows(j).Item("No_Urut")
                                    Dim Urut_Do As String = .Rows(j).Item("Urut_Oto")

                                    '==================
                                    '=     GET SN     =
                                    '==================
                                    SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, b.Urut_Oto, b.jumlah "
                                    SQL = SQL & "from Barang_SN a "
                                    SQL = SQL & "inner join det_do_new b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Serial_Number = b.Serial_Number "
                                    SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and (a.qr_code+'-'+a.kode_unik_berjalan) = '" & Barcode_DO_Sementara & "' "
                                    Using Ds1 = BindingTrans(SQL)
                                        If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                            For h As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                                Dim SN_DO_Sementara As String = Ds1.Tables("MyTable").Rows(h).Item("Serial_Number")
                                                Dim Kd_Barang_Retur As String = Ds1.Tables("MyTable").Rows(h).Item("kode_barang")
                                                Dim So_Retur As String = Ds1.Tables("MyTable").Rows(h).Item("kode_stock_owner")
                                                Dim Urut_Det_Penjualan As String = Ds1.Tables("MyTable").Rows(h).Item("Urut_Oto")

                                                If Jumlah_Retur = 0 Then
                                                    Exit For
                                                ElseIf Jumlah_Retur < 0 Then
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If

                                                If Jumlah_Retur < Ds1.Tables("MyTable").Rows(h).Item("jumlah") Or Ds1.Tables("MyTable").Rows(h).Item("jumlah") = Jumlah_Retur Then
                                                    SQL = "select kode_perusahaan from barang_sn where "
                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    SQL = SQL & "kode_stock_owner = '" & So_Retur & "' and "
                                                    SQL = SQL & "kode_barang = '" & Kd_Barang_Retur & "' and "
                                                    SQL = SQL & "serial_number = '" & SN_DO_Sementara & "'"
                                                    Using Dr = OpenTrans(SQL)
                                                        If Not (Dr.Read) Then
                                                            Dr.Close()
                                                            CloseTrans()
                                                            CloseConn()
                                                            MessageBox.Show("Data barang SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    End Using

                                                    SQL = "Update barang_sn set jumlah = jumlah + " & Jumlah_Retur & " where "
                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    SQL = SQL & "kode_stock_owner = '" & So_Retur & "' and "
                                                    SQL = SQL & "kode_barang = '" & Kd_Barang_Retur & "' and "
                                                    SQL = SQL & "serial_number = '" & SN_DO_Sementara & "'"
                                                    ExecuteTrans(SQL)

                                                    SQL = "insert into det_retur_do(kode_perusahaan, no_faktur, "
                                                    SQL = SQL & "kode_stock_owner, kode_barang, serial_number, "
                                                    SQL = SQL & "jumlah, no_urut_det_penj, no_urut_do, urut_det_do, urut_det_retur) values('" & KodePerusahaan & "', "
                                                    SQL = SQL & "'" & TextBox4.Text.Trim & "', "
                                                    SQL = SQL & "'" & So_Retur & "', "
                                                    SQL = SQL & "'" & Kd_Barang_Retur & "', "
                                                    SQL = SQL & "'" & SN_DO_Sementara & "', "
                                                    SQL = SQL & "'" & Jumlah_Retur & "', "
                                                    SQL = SQL & "'" & Urut_Detail_Penjualan & "', "
                                                    SQL = SQL & "'" & Urut_Do & "', "
                                                    SQL = SQL & "'" & Urut_Det_Penjualan & "', '" & x_no_urut_det_do & "') "
                                                    ExecuteTrans(SQL)

                                                    SQL = "insert into det_r_do (Kode_Perusahaan, No_Retur_Jual, Kode_Stock_Owner, Kode_Barang, Good_Stock, Bad_Stock, Barcode, Serial_Number, "
                                                    SQL = SQL & "Urut_Detail) "
                                                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & Trim(TextBox4.Text) & "', '" & So_Retur & "', '" & Kd_Barang_Retur & "', "
                                                    SQL = SQL & "'" & Jumlah_Retur & "', 0, '" & Barcode_DO_Sementara & "', '" & SN_DO_Sementara & "', '" & x_no_urut_det_do & "') "
                                                    ExecuteTrans(SQL)

                                                    total_hpp = total_hpp + (Jumlah_Retur * Get_Harga_SN(Ds1.Tables("MyTable").Rows(h).Item("serial_number")))

                                                    Jumlah_Retur = 0

                                                ElseIf Jumlah_Retur > Ds1.Tables("MyTable").Rows(h).Item("jumlah") Then
                                                    SQL = "insert into det_retur_do(kode_perusahaan, no_faktur, "
                                                    SQL = SQL & "kode_stock_owner, kode_barang, serial_number, "
                                                    SQL = SQL & "jumlah, no_urut_det_penj, no_urut_do, urut_det_do, urut_det_retur) values('" & KodePerusahaan & "', "
                                                    SQL = SQL & "'" & TextBox4.Text.Trim & "', "
                                                    SQL = SQL & "'" & So_Retur & "', "
                                                    SQL = SQL & "'" & Kd_Barang_Retur & "', "
                                                    SQL = SQL & "'" & SN_DO_Sementara & "', "
                                                    SQL = SQL & "'" & Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(h).Item("jumlah"))) & "', "
                                                    SQL = SQL & "'" & Urut_Detail_Penjualan & "', "
                                                    SQL = SQL & "'" & Urut_Do & "', "
                                                    SQL = SQL & "'" & Urut_Det_Penjualan & "', '" & x_no_urut_det_do & "') "
                                                    ExecuteTrans(SQL)

                                                    SQL = "select kode_perusahaan from barang_sn where "
                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    SQL = SQL & "kode_stock_owner = '" & So_Retur & "' and "
                                                    SQL = SQL & "kode_barang = '" & Kd_Barang_Retur & "' and "
                                                    SQL = SQL & "serial_number = '" & SN_DO_Sementara & "'"
                                                    Using Dr = OpenTrans(SQL)
                                                        If Not (Dr.Read) Then
                                                            Dr.Close()
                                                            CloseTrans()
                                                            CloseConn()
                                                            MessageBox.Show("Data barang SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Exit Sub
                                                        End If
                                                    End Using

                                                    SQL = "Update barang_sn set jumlah = jumlah + " & .Rows(h).Item("jumlah") & " where "
                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    SQL = SQL & "kode_stock_owner = '" & So_Retur & "' and "
                                                    SQL = SQL & "kode_barang = '" & Kd_Barang_Retur & "' and "
                                                    SQL = SQL & "serial_number = '" & SN_DO_Sementara & "'"
                                                    ExecuteTrans(SQL)

                                                    SQL = "insert into det_r_do (Kode_Perusahaan, No_Retur_Jual, Kode_Stock_Owner, Kode_Barang, Good_Stock, Bad_Stock, Barcode, Serial_Number, "
                                                    SQL = SQL & "Urut_Detail) "
                                                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & Trim(TextBox4.Text) & "', '" & So_Retur & "', '" & Kd_Barang_Retur & "', "
                                                    SQL = SQL & "'" & Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(h).Item("jumlah"))) & "', 0, '" & Barcode_DO_Sementara & "', "
                                                    SQL = SQL & "'" & SN_DO_Sementara & "', '" & x_no_urut_det_do & "') "
                                                    ExecuteTrans(SQL)


                                                    total_hpp = total_hpp + (Ds1.Tables("MyTable").Rows(h).Item("jumlah") * Get_Harga_SN(Ds1.Tables("MyTable").Rows(h).Item("serial_number")))

                                                    Jumlah_Retur = Jumlah_Retur - Ds1.Tables("MyTable").Rows(h).Item("jumlah")
                                                Else
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Barang SN terjadi kesalahan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If

                                                If Jumlah_Retur <> 0 And h = .Rows.Count - 1 Then
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Jumlah stock tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If



                                            Next

                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show($"Detail DO Barang { .Rows(j).Item("Barcode")} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                            Exit Sub

                                        End If
                                    End Using




                                Next
                            End If
                        End With
                    End Using







                    '============================================================================================================
                    '=     KODE LAMA
                    '============================================================================================================
#Region "Kode Lama"

                    '                    SQL = "select no_faktur, kode_stock_owner, kode_barang, serial_number, jumlah, no_urut_det_penj, no_urut_do, urut_oto from det_do_new where "
                    '                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                    SQL = SQL & "no_faktur = '" & Trim(TextBox1.Text) & "' and "
                    '                    SQL = SQL & "no_urut_do = '" & ListView2.Items(i).SubItems(5).Text & "' and "
                    '                    SQL = SQL & "kode_stock_owner = '" & ListView2.Items(i).Text & "' and "
                    '                    SQL = SQL & "kode_barang = '" & ListView2.Items(i).SubItems(1).Text & "' "
                    '                    SQL = SQL & "order by " & SN_Tanggal("serial_number", "desc")
                    '                    Using Ds = BindingTrans(SQL)
                    '                        With Ds.Tables("MyTable")
                    '                            If .Rows.Count <> 0 Then
                    '                                jml_di_retur = HilangkanTanda(ListView2.Items(i).SubItems(3).Text)

                    '                                For h As Integer = 0 To .Rows.Count - 1
                    '                                    If .Rows(h).Item("serial_number") = "-" Then
                    '                                        If y_jml_jual = y_pernah_retur + jml_di_retur Then
                    '                                            SQL = "insert into det_retur_do(kode_perusahaan, no_retur_jual, "
                    '                                            SQL = SQL & "no_faktur, kode_stock_owner, kode_barang, "
                    '                                            SQL = SQL & "serial_number, jumlah, no_urut_det_penj, no_urut_do, urut_det_do) values("
                    '                                            SQL = SQL & "'" & KodePerusahaan & "', "
                    '                                            SQL = SQL & "'" & TextBox4.Text.Trim & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("no_faktur") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("jumlah") & "')"
                    '                                            SQL = SQL & "'" & .Rows(h).Item("   ") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("no_urut_do") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("urut_oto") & "') "
                    '                                            ExecuteTrans(SQL)

                    '                                            SQL = "delete from det_do_new where "
                    '                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                                            SQL = SQL & "no_faktur = '" & .Rows(h).Item("no_faktur") & "' and "
                    '                                            SQL = SQL & "no_urut = '" & .Rows(h).Item("no_urut") & "' and "
                    '                                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                    '                                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' "
                    '                                            ExecuteTrans(SQL)
                    '                                        End If
                    '                                    Else

                    '                                        '===========kalau SN ada

                    '                                        If jml_di_retur = 0 Then
                    '                                            Exit For
                    '                                        ElseIf jml_di_retur < 0 Then
                    '                                            CloseTrans()
                    '                                            CloseConn()
                    '                                            MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                                            Exit Sub
                    '                                        End If

                    '                                        If jml_di_retur < .Rows(h).Item("jumlah") Or .Rows(h).Item("jumlah") = jml_di_retur Then
                    '                                            SQL = "select kode_perusahaan from barang_sn where "
                    '                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                    '                                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                    '                                            SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                    '                                            Using Dr = OpenTrans(SQL)
                    '                                                If Not (Dr.Read) Then
                    '                                                    Dr.Close()
                    '                                                    CloseTrans()
                    '                                                    CloseConn()
                    '                                                    MessageBox.Show("Data barang SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                                                    Exit Sub
                    '                                                End If
                    '                                            End Using

                    '                                            SQL = "Update barang_sn set jumlah = jumlah + " & jml_di_retur & " where "
                    '                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                    '                                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                    '                                            SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                    '                                            ExecuteTrans(SQL)

                    '                                            SQL = "insert into det_retur_do(kode_perusahaan, no_faktur, "
                    '                                            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, "
                    '                                            SQL = SQL & "jumlah, no_urut_det_penj, no_urut_do, urut_det_do, urut_det_retur) values('" & KodePerusahaan & "', "
                    '                                            SQL = SQL & "'" & TextBox4.Text.Trim & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                    '                                            SQL = SQL & "'" & jml_di_retur & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("no_urut_det_penj") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("no_urut_do") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("urut_oto") & "', '" & x_no_urut_det_do & "') "
                    '                                            ExecuteTrans(SQL)

                    '                                            total_hpp = total_hpp + (jml_di_retur * Get_Harga_SN(.Rows(h).Item("serial_number")))

                    '                                            jml_di_retur = 0

                    '                                        ElseIf jml_di_retur > .Rows(h).Item("jumlah") Then
                    '                                            SQL = "insert into det_retur_do(kode_perusahaan, no_faktur, "
                    '                                            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, "
                    '                                            SQL = SQL & "jumlah, no_urut_det_penj, no_urut_do, urut_det_do, urut_det_retur) values('" & KodePerusahaan & "', "
                    '                                            SQL = SQL & "'" & TextBox4.Text.Trim & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("jumlah") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("no_urut_det_penj") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("no_urut_do") & "', "
                    '                                            SQL = SQL & "'" & .Rows(h).Item("urut_oto") & "', '" & x_no_urut_det_do & "') "
                    '                                            ExecuteTrans(SQL)

                    '                                            SQL = "select kode_perusahaan from barang_sn where "
                    '                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                    '                                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                    '                                            SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                    '                                            Using Dr = OpenTrans(SQL)
                    '                                                If Not (Dr.Read) Then
                    '                                                    Dr.Close()
                    '                                                    CloseTrans()
                    '                                                    CloseConn()
                    '                                                    MessageBox.Show("Data barang SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                                                    Exit Sub
                    '                                                End If
                    '                                            End Using

                    '                                            SQL = "Update barang_sn set jumlah = jumlah + " & .Rows(h).Item("jumlah") & " where "
                    '                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                    '                                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                    '                                            SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                    '                                            ExecuteTrans(SQL)

                    '                                            total_hpp = total_hpp + (.Rows(h).Item("jumlah") * Get_Harga_SN(.Rows(h).Item("serial_number")))

                    '                                            jml_di_retur = jml_di_retur - .Rows(h).Item("jumlah")
                    '                                        Else
                    '                                            CloseTrans()
                    '                                            CloseConn()
                    '                                            MessageBox.Show("Barang SN terjadi kesalahan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                                            Exit Sub
                    '                                        End If

                    '                                        If jml_di_retur <> 0 And h = .Rows.Count - 1 Then
                    '                                            CloseTrans()
                    '                                            CloseConn()
                    '                                            MessageBox.Show("Jumlah stock tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                                            Exit Sub
                    '                                        End If
                    '                                    End If
                    '                                Next
                    '                            Else
                    '                                CloseTrans()
                    '                                CloseConn()
                    '                                MessageBox.Show("Data detail penjualan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                                Exit Sub
                    '                            End If
                    '                        End With
                    '                    End Using

#End Region



                End If

                Dim NFlag_Budgeting As String = ""
                Dim NFlag_Budgeting_Mbl As String = ""
                Dim NFlag_Budgeting_2 As String = ""
                Dim NFlag_Budgeting_3 As String = ""
                Dim NFlag_Budgeting_4 As String = ""
                Dim NHarga_Terendah As Double = 0
                Dim NHarga_Agen As Double = 0
                Dim kode_kategori2 As String = ""

                If metode_budgeting = "B" Then
                    SQL = "select NFlag_Budgeting, NFlag_Budgeting_Mbl, NFlag_Budgeting_2, NFlag_Budgeting_3, NFlag_Budgeting_4, NHarga_Terendah, NHarga_Agen, b.kode_kategori2 from "
                    SQL = SQL & "detail_do_new a, barang b where "
                    SQL = SQL & "a.kode_Perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner and "
                    SQL = SQL & "a.kode_barang = b.kode_barang and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_do = '" & TextBox1.Text.Trim & "' and "
                    SQL = SQL & "a.urut_oto = '" & ListView2.Items(i).SubItems(5).Text & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If General_Class.CekNULL(Dr("nflag_budgeting")) = "" Then
                                NFlag_Budgeting = ""
                            Else
                                NFlag_Budgeting = Dr("nflag_budgeting")
                            End If

                            If General_Class.CekNULL(Dr("nflag_budgeting_mbl")) = "" Then
                                NFlag_Budgeting_Mbl = ""
                            Else
                                NFlag_Budgeting_Mbl = Dr("nflag_budgeting_mbl")
                            End If

                            If General_Class.CekNULL(Dr("nflag_budgeting_2")) = "" Then
                                NFlag_Budgeting_2 = ""
                            Else
                                NFlag_Budgeting_2 = Dr("nflag_budgeting_2")
                            End If

                            If General_Class.CekNULL(Dr("nflag_budgeting_3")) = "" Then
                                NFlag_Budgeting_3 = ""
                            Else
                                NFlag_Budgeting_3 = Dr("nflag_budgeting_3")
                            End If

                            If General_Class.CekNULL(Dr("nflag_budgeting_4")) = "" Then
                                NFlag_Budgeting_4 = ""
                            Else
                                NFlag_Budgeting_4 = Dr("nflag_budgeting_4")
                            End If
                            NHarga_Agen = Format(Dr("nharga_agen"), "N4")
                            NHarga_Terendah = Format(Dr("nharga_terendah"), "N4")
                            kode_kategori2 = Dr("kode_kategori2")

                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data detail DO tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                End If
            Next

            If metode_pot_Stock = "B" Then

                Dim coa_kas As String = ""
                Dim coa_retur_jual As String = ""
                Dim coa_persediaan As String = ""
                Dim coa_hpp As String = ""
                Dim coa_ppn_penjualan As String = ""

                SQL = "select retur_penjualan, hpp_tk_sdr, ppn_penjualan, ppn_penjualan, retur_penjualan_reseller, hpp, inisial_faktur, piutang, kas from "
                SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & ComboBox1.Text & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        coa_kas = dr("kas")
                        coa_retur_jual = dr("retur_penjualan_reseller")
                        coa_hpp = dr("hpp")
                        coa_ppn_penjualan = dr("ppn_penjualan")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select persediaan_Brg_Blm_Krm, Brg_Blm_Krm, persediaan_sementara, persediaan from "
                SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_stock_owner = '" & y_lokasi_gudang & "'"
                SQL = SQL & "kode_stock_owner = '" & ComboBox1.Text & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        coa_persediaan = dr("persediaan")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data lokasi gudang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                Dim pagenumber As Integer = 1
                Dim Kode_Voucher As String = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & arrInisialFaktur(ComboBox1.SelectedIndex), KodePerusahaan)
                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                SQL = SQL & "'" & Kode_Voucher & "', "
                SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Retur DO " & TextBox4.Text & ";" & TextBox1.Text.Trim & ";" & xnofak & ";" & Strings.Left(TextBox3.Text.Trim, 20) & "', '', "
                SQL = SQL & "'-', '" & UserID & "', '" & ComboBox1.Text & "')"
                ExecuteTrans(SQL)

                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_retur_jual, 1),
                                Strings.Mid(coa_retur_jual, 2, 1),
                                Strings.Mid(Ganti(coa_retur_jual), 3),
                                KodePerusahaan, KodeProyek, "Retur Penjualan " & TextBox4.Text & ";" & TextBox1.Text.Trim & ";" & Strings.Left(TextBox3.Text.Trim, 20), HilangkanTanda(TextBox17.Text), "0", pagenumber, ComboBox1.Text, Cost_center:=Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                If Val(HilangkanTanda(TextBox19.Text)) <> 0 Then
                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_ppn_penjualan, 1),
                                    Strings.Mid(coa_ppn_penjualan, 2, 1),
                                    Strings.Mid(Ganti(coa_ppn_penjualan), 3),
                                    KodePerusahaan, KodeProyek, "Retur DO " & TextBox4.Text & ";" & TextBox1.Text.Trim & ";" & xnofak & ";" & Strings.Left(TextBox3.Text.Trim, 20), HilangkanTanda(TextBox19.Text), "0", pagenumber, Ket_Lokasi_HO, Cost_center:=Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1
                End If


                If jns_trans = "T" Or flag_lns_do = "Y" Then
                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_kas, 1),
                               Strings.Mid(coa_kas, 2, 1),
                               Strings.Mid(Ganti(coa_kas), 3),
                               KodePerusahaan, KodeProyek, "Retur DO " & TextBox4.Text & ";" & TextBox1.Text.Trim & ";" & xnofak & ";" & Strings.Left(TextBox3.Text.Trim, 20), "0", HilangkanTanda(TxtTotal.Text), pagenumber, ComboBox1.Text, Cost_center:=Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1
                Else
                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_piutang, 1),
                               Strings.Mid(coa_piutang, 2, 1),
                               Strings.Mid(Ganti(coa_piutang), 3),
                               KodePerusahaan, KodeProyek, "Retur DO " & TextBox4.Text & ";" & TextBox1.Text.Trim & ";" & xnofak & ";" & Strings.Left(TextBox3.Text.Trim, 20), "0", HilangkanTanda(TxtTotal.Text), pagenumber, ComboBox1.Text, Cost_center:=Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1
                End If

                '=========
                Dim Kode_Voucher2 As String = ""
                Dim __Kode_Voucher2 As String = "NULL"

                If total_hpp <> 0 Then
                    Kode_Voucher2 = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & arrInisialFaktur(ComboBox1.SelectedIndex), KodePerusahaan)
                    __Kode_Voucher2 = "'" & Kode_Voucher2 & "'"

                    pagenumber = 1

                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                    SQL = SQL & "'" & Kode_Voucher2 & "', "
                    SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    SQL = SQL & "'" & KodeProyek & "', 'Retur DO " & TextBox4.Text & ";" & TextBox1.Text.Trim & ";" & xnofak & ";" & Strings.Left(TextBox3.Text.Trim, 20) & "', '', "
                    SQL = SQL & "'-', '" & UserID & "', '" & ComboBox1.Text & "')"
                    ExecuteTrans(SQL)

                    'REVISI ARDI /Ubah Lokasi induk ke lokasi gudang untuk akun persediaan
                    SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_persediaan, 1),
                                  Strings.Mid(coa_persediaan, 2, 1),
                                  Strings.Mid(Ganti(coa_persediaan), 3),
                                  KodePerusahaan, KodeProyek, "Retur DO " & TextBox4.Text & ";" & TextBox1.Text.Trim & ";" & xnofak & ";" & Strings.Left(TextBox3.Text.Trim, 20), total_hpp, "0", pagenumber, y_lokasi_gudang, Cost_center:=Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1


                    SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_hpp, 1),
                                  Strings.Mid(coa_hpp, 2, 1),
                                  Strings.Mid(Ganti(coa_hpp), 3),
                                  KodePerusahaan, KodeProyek, "Retur DO " & TextBox4.Text & ";" & TextBox1.Text.Trim & ";" & xnofak & ";" & Strings.Left(TextBox3.Text.Trim, 20), "0", total_hpp, pagenumber, ComboBox1.Text, Cost_center:=Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    '======================

                End If

                SQL = "update retur_do set kode_voucher_1 = '" & Kode_Voucher & "', kode_voucher_2 = " & __Kode_Voucher2 & " where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_retur_jual = '" & TextBox4.Text.Trim & "'"
                ExecuteTrans(SQL)

            End If




            'SQL = ";with cte_data as( "

            'SQL = SQL & "select "
            'SQL = SQL & "a.kode_perusahaan, a.no_faktur, a.Jenis_Transaksi, "

            'SQL = SQL & "( "
            'SQL = SQL & "select isnull(sum(x.ngrand), 0) as ttl "
            'SQL = SQL & "from do_new x, penjualan y where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_faktur = y.no_faktur and "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and "
            'SQL = SQL & "x.no_faktur = a.no_faktur And x.status Is null "
            'SQL = SQL & ") as Omset_DO, "

            'SQL = SQL & "( "
            'SQL = SQL & "select isnull(sum(x.ngrand), 0) as ttl from retur_do x, do_new y, penjualan z where "
            'SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = z.Kode_Perusahaan and "
            'SQL = SQL & "x.no_do = y.no_do and y.no_faktur = z.no_faktur and x.kode_perusahaan = a.kode_perusahaan and "
            'SQL = SQL & "z.no_faktur = a.no_faktur and "
            'SQL = SQL & "x.status is null and y.status is null "
            'SQL = SQL & ") as Retur_DO, "

            'SQL = SQL & "( "
            'SQL = SQL & "select isnull((sum(y.nilai)), 0) from Retur_MT x, Retur_MT_Det y, do_new z where "
            'SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan and "
            'SQL = SQL & "y.Kode_Perusahaan = z.Kode_Perusahaan and "
            'SQL = SQL & "x.No_Faktur = y.No_Faktur and "
            'SQL = SQL & "y.No_DO = z.no_do and "
            'SQL = SQL & "z.No_Faktur = a.no_faktur and x.Status is null and "
            'SQL = SQL & "x.flag_validasi_hutang = 'Y' "
            'SQL = SQL & ") as Retur_MT, "

            'SQL = SQL & "( "
            'SQL = SQL & "select isnull(sum(y.byr), 0) as ttl from val_do x, detail_val_do y, penjualan z, do_new r where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
            'SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and "
            'SQL = SQL & "z.no_faktur = a.no_faktur and "
            'SQL = SQL & "x.status is null "
            'SQL = SQL & ") AS Pelunasan_Kredit, "

            'SQL = SQL & "( "
            'SQL = SQL & "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from Val_DO_Tunai x, Detail_Val_DO_Tunai y, penjualan z, do_new r where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
            'SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and "
            'SQL = SQL & "z.no_faktur = a.no_faktur and "
            'SQL = SQL & "x.status is null "
            'SQL = SQL & ") AS Pelunasan_Tunai "


            'SQL = SQL & "from penjualan a, customers b where a.kode_perusahaan = b.kode_perusahaan and "
            'SQL = SQL & "b.kode_customer = a.kode_customer and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "a.status is null and "
            'SQL = SQL & "a.no_faktur = '" & xnofak & "' "
            'SQL = SQL & ") "

            'SQL = SQL & "select *, "
            'SQL = SQL & "(case "
            'SQL = SQL & "when (Omset_DO -retur_do - retur_mt) - Pelunasan_Kredit - Pelunasan_Tunai = 0 then 'Y' and  Pelunasan_Kredit + Pelunasan_Tunai <> 0 "
            'SQL = SQL & "when (Omset_DO -retur_do - retur_mt) - Pelunasan_Kredit - Pelunasan_Tunai < 0 then 'Y' and  Pelunasan_Kredit + Pelunasan_Tunai <> 0 "
            'SQL = SQL & "else 'T' "
            'SQL = SQL & "end) as Lunas "

            'SQL = SQL & "from cte_data "\

            SQL = "exec cek_pi_lunas '" & KodePerusahaan & "', '" & xnofak & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If .Rows(0).Item("flag_lns") = "Y" Then
                            If .Rows(0).Item("jenis_transaksi") = "T" Then
                                SQL = "update penjualan set "
                                SQL = SQL & "Flag_Lunas_Tunai = 'Y', "
                                SQL = SQL & "Tgl_Lunas_Tunai = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                                SQL = SQL & "Jam_Lunas_Tunai = '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                                SQL = SQL & "UserValidasi_Tunai = '" & UserID & "' where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & xnofak & "' "
                                ExecuteTrans(SQL)
                            Else
                                SQL = "update penjualan set "
                                SQL = SQL & "Flag_Lunas = 'Y', "
                                SQL = SQL & "Tgl_Lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                                SQL = SQL & "Jam_Lunas = '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                                SQL = SQL & "UserValidasi = '" & UserID & "' where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & xnofak & "' "
                                ExecuteTrans(SQL)
                            End If
                        End If
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data PI tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            SQL = "exec cek_do_lunas '" & KodePerusahaan & "', '" & TextBox1.Text.Trim & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If .Rows(0).Item("flag_lns") = "Y" Then
                            SQL = "Update do_new set flag_lunas_do = 'Y', "
                            SQL = SQL & "nTgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "njam_lunas = '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                            SQL = SQL & "nuservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_do = '" & TextBox1.Text.Trim & "'"
                            ExecuteTrans(SQL)
                        End If
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data do tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            'SQL = "update do_newxx set flag_lunas_do = NULL, sudah_dilunasi = 0 where "
            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "no_faktur = '" & xnofak & "'"
            'ExecuteTrans(SQL)

            'SQL = "select no_val, jumlah from det_do_pelunasand where kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "no_do = '" & TextBox1.Text.Trim & "' "
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        If .Rows.Count <> 0 Then
            '            For i As Integer = 0 To .Rows.Count - 1
            '                SQL = "Update detail_val_penj set sisa = byr where "
            '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            '                SQL = SQL & "no_val = '" & .Rows(i).Item("no_val") & "'"
            '                ExecuteTrans(SQL)

            '                SQL = "Update detail_val_penj_tunai set sisa = byr + disc_cash where "
            '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            '                SQL = SQL & "no_val = '" & .Rows(i).Item("no_val") & "'"
            '                ExecuteTrans(SQL)
            '            Next
            '        End If

            '    End With
            'End Using

            'SQL = "delete from det_do_pelunasand where kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "no_do = '" & TextBox1.Text.Trim & "' "
            'ExecuteTrans(SQL)

            '===========





            'Dim _no_fak As String = ""
            'Dim _tbl As String = ""
            'Dim lanjut As String = ""

            'SQL = "select no_faktur, jenis_transaksi from "
            'SQL = SQL & "rekap_sub_invoice where "
            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "no_do = '" & TextBox1.Text.Trim & "' and flag_lunas_do is null order by tanggal_do"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        _no_fak = Dr("no_faktur")
            '        lanjut = "Y"

            '        If Dr("jenis_transaksi") = "T" Then
            '            _tbl = "_tunai"
            '        Else
            '            _tbl = ""
            '        End If
            '    Else
            '        lanjut = "T"
            '        Dr.Close()
            '    End If
            'End Using

            'If lanjut = "Y" Then
            '    Dim xarr_total_sisa, xarr_no_do As New ArrayList

            '    SQL = "select no_do, (total_baru_dikurang_diskon + nilai_ppn_baru) - (retur_baru_dikurang_diskon + nilai_ppn_retur_baru) - sudah_dilunasi as ttl from "
            '    SQL = SQL & "rekap_sub_invoice where "
            '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            '    SQL = SQL & "no_faktur = '" & _no_fak & "' and flag_lunas_do is null order by tanggal_do"
            '    Using Dr = OpenTrans(SQL)
            '        Do While Dr.Read
            '            xarr_no_do.Add(Dr("no_do"))
            '            xarr_total_sisa.Add(Dr("ttl"))
            '        Loop
            '    End Using

            '    Dim sisa As Double = 0
            '    For yy As Integer = 0 To xarr_no_do.Count - 1


            '        SQL = "select a.no_val, b.sisa as total from "
            '        SQL = SQL & "val_penj" & _tbl & " a, detail_val_penj" & _tbl & " b where "
            '        SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_val = b.no_val and "
            '        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            '        SQL = SQL & "b.no_faktur = '" & _no_fak & "' and a.status is null and sisa <> 0 "
            '        SQL = SQL & "order by a.tanggal"
            '        Using Ds = BindingTrans(SQL)
            '            With Ds.Tables("MyTable")
            '                If .Rows.Count <> 0 Then
            '                    sisa = xarr_total_sisa.Item(yy)

            '                    For h As Integer = 0 To .Rows.Count - 1
            '                        If sisa = 0 Then
            '                            Exit For
            '                        ElseIf sisa < 0 Then
            '                            CloseTrans()
            '                            CloseConn()
            '                            MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                            Exit Sub
            '                        End If

            '                        If sisa < .Rows(h).Item("total") Or sisa = .Rows(h).Item("total") Then
            '                            'SQL = "select (total_baru_dikurang_diskon + nilai_ppn_baru) - sudah_dilunasi as ttl from "
            '                            'SQL = SQL & "rekap_sub_invoice where "
            '                            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            '                            'SQL = SQL & "no_do = '" & xarr_no_do.Item(yy) & "' and flag_lunas_do is null"
            '                            'Using Dr = OpenTrans(SQL)
            '                            '    If Dr.Read Then
            '                            '        'If Dr("ttl") <> sisa Then
            '                            '        '    Dr.Close()
            '                            '        '    CloseTrans()
            '                            '        '    CloseConn()
            '                            '        '    MessageBox.Show("Nilai sisa DO ini sudah berubah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                            '        '    Exit Sub
            '                            '        'Else
            '                            '        'Dr.Close()
            '                            '        'SQL = "update rekap_sub_invoice set flag_lunas_do = 'Y', sudah_dilunasi = sudah_dilunasi + " & sisa & " where "
            '                            '        'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            '                            '        'SQL = SQL & "no_do = '" & xarr_no_do.Item(yy) & "'"
            '                            '        'ExecuteTrans(SQL)
            '                            '        ' End If
            '                            '    Else
            '                            '        Dr.Close()
            '                            '        CloseTrans()
            '                            '        CloseConn()
            '                            '        MessageBox.Show("No DO tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                            '        Exit Sub
            '                            '    End If
            '                            'End Using

            '                            SQL = "update do_new set flag_lunas_do = 'Y', sudah_dilunasi = sudah_dilunasi + " & sisa & " where "
            '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            '                            SQL = SQL & "no_do = '" & xarr_no_do.Item(yy) & "'"
            '                            ExecuteTrans(SQL)

            '                            SQL = "Update detail_val_penj" & _tbl & " set sisa = sisa - " & sisa & " where "
            '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            '                            SQL = SQL & "no_val = '" & .Rows(h).Item("no_val") & "' and no_faktur = '" & _no_fak & "'"
            '                            ExecuteTrans(SQL)

            '                            SQL = "insert into det_do_pelunasan(kode_perusahaan, no_do, "
            '                            SQL = SQL & "no_val, jumlah) values('" & KodePerusahaan & "', "
            '                            SQL = SQL & "'" & xarr_no_do.Item(yy) & "', '" & .Rows(h).Item("no_val") & "', "
            '                            SQL = SQL & "'" & sisa & "')"
            '                            ExecuteTrans(SQL)

            '                            sisa = 0
            '                        ElseIf sisa > .Rows(h).Item("total") Then
            '                            'SQL = "insert into det_penj(kode_perusahaan, no_faktur, "
            '                            'SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
            '                            'SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
            '                            'SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
            '                            'SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
            '                            'SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
            '                            'SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
            '                            'SQL = SQL & "" & x_no_urut_det_penj & ", "
            '                            'SQL = SQL & "'" & .Rows(h).Item("jumlah") & "')"
            '                            'ExecuteTrans(SQL)

            '                            'SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
            '                            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            '                            'SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
            '                            'SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
            '                            'SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
            '                            'ExecuteTrans(SQL)

            '                            '  total_hpp = total_hpp + (.Rows(h).Item("jumlah") * Get_Harga_SN(.Rows(h).Item("serial_number")))

            '                            SQL = "update do_new set sudah_dilunasi = sudah_dilunasi + " & .Rows(h).Item("total") & " where "
            '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            '                            SQL = SQL & "no_do = '" & xarr_no_do.Item(yy) & "'"
            '                            ExecuteTrans(SQL)

            '                            SQL = "Update detail_val_penj" & _tbl & " set sisa = sisa - sisa where "
            '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            '                            SQL = SQL & "no_val = '" & .Rows(h).Item("no_val") & "' and no_faktur = '" & _no_fak & "'"
            '                            ExecuteTrans(SQL)

            '                            SQL = "insert into det_do_pelunasan(kode_perusahaan, no_do, "
            '                            SQL = SQL & "no_val, jumlah) values('" & KodePerusahaan & "', "
            '                            SQL = SQL & "'" & xarr_no_do.Item(yy) & "', '" & .Rows(h).Item("no_val") & "', "
            '                            SQL = SQL & "'" & .Rows(h).Item("total") & "')"
            '                            ExecuteTrans(SQL)

            '                            sisa = sisa - .Rows(h).Item("total")
            '                        Else
            '                            CloseTrans()
            '                            CloseConn()
            '                            MessageBox.Show("Detail pelunasan terjadi kesalahan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                            Exit Sub
            '                        End If
            '                    Next
            '                Else
            '                    'CloseTrans()
            '                    'CloseConn()
            '                    'MessageBox.Show("Pelunasan tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                    'Exit Sub
            '                End If
            '            End With
            '        End Using
            '    Next
            'End If

            SQL = "update retur_do_sementara set flag_val = 'Y',user_val = '" & UserID & "' where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_retur_jual_sementara = '" & no_retur_s & "'"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dim Tanya_Cetak As String = MessageBox.Show("Cetak Faktur?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If Tanya_Cetak = vbYes Then
            cetak()
        End If

        kosong()
        TextBox1.Text = ""
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

            get_no_faktur("")

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
        Dim DisableColumns As Integer() = {3, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
        For Each DCol As Integer In DisableColumns
            If e.ColumnIndex = DCol Then
                e.Cancel = True
                e.NewWidth = ListView2.Columns(DCol).Width
            End If
        Next DCol
    End Sub

    Private Sub ListView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub

#Region "KODE LAMA"
        '$$$$$$$$$$$$$
        'TextBox8.Text = ListView2.FocusedItem.SubItems(1).Text
        'TextBox8_Leave(ListView2, e)
        'TextBox11.Focus()
        'TextBox8.Text = ListView2.FocusedItem.SubItems(1).Text
        'TextBox9.Text = ListView3.FocusedItem.SubItems(2).Text
        'TextBox13.Text = ListView3.FocusedItem.SubItems(3).Text
        'TextBox10.Text = Val(HilangkanTanda(ListView3.FocusedItem.SubItems(4).Text)) - Val(HilangkanTanda(ListView3.FocusedItem.SubItems(5).Text))
        'TextBox11.Text = "0"
        'TextBox12.Text = "0"
        'TextBox5.Text = ListView3.FocusedItem.SubItems(7).Text
        'TextBox6.Text = ListView3.FocusedItem.SubItems(8).Text
        'TextBox14.Text = "0"
        'TextBox11.Focus()


        'KOMEN DULU TIDKA BOLEH HAPUS
        'ListView2.FocusedItem.Remove()
        'HitungGrandTotal()

#End Region

        Dim SelectedKDBarang As String = ListView2.FocusedItem.SubItems(1).Text

        Dim Total As Double = 0

        Try
            OpenConn()



            N_EMI_SD_Retur_DO_Reseller_Detail_Barcode.Lv_Data_Barcode.Items.Clear()
            SQL = "select b.Kode_Stock_Owner, b.Kode_Barang, d.Nama as Nama_Barang, c.Good_Stock, c.nHarga, c.nPersen_Diskon, c.nSubtotal, b.Metode_Perhitungan, c.Barcode "
            SQL = SQL & "from retur_do_sementara a "
            SQL = SQL & "inner join detail_r_do_sementara b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Retur_Jual_Sementara = b.No_Retur_Jual_Sementara "
            SQL = SQL & "inner join det_r_do_sementara c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Retur_Jual_Sementara = c.No_Retur_Jual_Sementara and b.No_Urut = c.Urut_Detail "
            SQL = SQL & "inner join Barang d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "where a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Retur_Jual_Sementara = '" & no_retur_s & "' "
            SQL = SQL & "and c.Kode_Barang = '" & SelectedKDBarang & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            N_EMI_SD_Retur_DO_Reseller_Detail_Barcode.Txt_KdBarang.Text = .Rows(i).Item("Kode_Barang")
                            N_EMI_SD_Retur_DO_Reseller_Detail_Barcode.Txt_NmBarang.Text = .Rows(i).Item("Nama_Barang")

                            Dim lv As New ListViewItem
                            lv = N_EMI_SD_Retur_DO_Reseller_Detail_Barcode.Lv_Data_Barcode.Items.Add(.Rows(i).Item("Kode_Stock_Owner"))
                            lv.SubItems.Add(.Rows(i).Item("Barcode"))
                            lv.SubItems.Add(Format(Val(HilangkanTanda(.Rows(i).Item("Good_Stock"))), "N4"))

                            Total += Val(HilangkanTanda(.Rows(i).Item("Good_Stock")))

                        Next
                    End If
                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        N_EMI_SD_Retur_DO_Reseller_Detail_Barcode.Txt_Total.Text = Format(Total, "N4")
        N_EMI_SD_Retur_DO_Reseller_Detail_Barcode.ShowDialog()




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

    Private Sub ListView4_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView4.DoubleClick
        If ListView4.Items.Count = 0 Or ListView4.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih no do yang mau validasi !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        TextBox1.Text = ListView4.FocusedItem.Text
        no_retur_s = ListView4.FocusedItem.SubItems(1).Text
        TextBox1_Leave(ListView4, e)


        Try
            OpenConn()

            ListView2.Items.Clear()
            SQL = "select a.Kode_Stock_Owner,a.Urut_DO,a.Kode_Barang,b.Nama,a.Good_Stock,"
            SQL = SQL & "a.Urut_Detail_Penjualan, a.Metode_Perhitungan,a.nHarga,a.nPersen_Diskon,a.nSubtotal "
            SQL = SQL & "from Detail_R_DO_sementara a, Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Retur_Jual_Sementara = '" & no_retur_s & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView2.Items.Add(dr("kode_stock_owner"))
                    Lvw.SubItems.Add(dr("kode_barang"))
                    Lvw.SubItems.Add(dr("nama"))
                    Lvw.SubItems.Add(dr("good_stock"))
                    Lvw.SubItems.Add(dr("urut_detail_penjualan"))
                    Lvw.SubItems.Add(dr("urut_do"))
                    Lvw.SubItems.Add(Format(dr("nharga"), "N4"))
                    Lvw.SubItems.Add(dr("npersen_diskon"))
                    Lvw.SubItems.Add(Format(dr("nsubtotal"), "N4"))
                    Lvw.SubItems.Add(dr("metode_perhitungan"))
                Loop
            End Using

            Lv_Hidden_Data.Items.Clear()
            SQL = "select b.Kode_Stock_Owner, b.Kode_Barang, d.Nama as Nama_Barang, c.Good_Stock, c.nHarga, c.nPersen_Diskon, c.nSubtotal, b.Metode_Perhitungan, c.Barcode "
            SQL = SQL & "from retur_do_sementara a "
            SQL = SQL & "inner join detail_r_do_sementara b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Retur_Jual_Sementara = b.No_Retur_Jual_Sementara "
            SQL = SQL & "inner join det_r_do_sementara c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Retur_Jual_Sementara = c.No_Retur_Jual_Sementara and b.No_Urut = c.Urut_Detail "
            SQL = SQL & "inner join Barang d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "where a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Retur_Jual_Sementara = '" & no_retur_s & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Hidden_Data.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Good_Stock"))
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