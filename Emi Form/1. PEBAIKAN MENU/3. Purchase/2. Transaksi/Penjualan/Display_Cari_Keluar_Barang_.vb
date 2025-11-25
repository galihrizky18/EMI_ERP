

Public Class Display_Cari_Keluar_Barang_
    Dim Arr1, Arr2, Arr3 As New ArrayList
    Dim Batal As Color = Color.Black
    Dim CrDoc As Object
    Dim tgl_skrg As String
    Dim boleh_lihat_global As Boolean

    Dim LvNo_Faktur As String
    Dim LvTgl As String
    Dim LvJam As String
    Dim LvUserID As String
    Dim LvLokasi As String
    Dim LvKdCust As String
    Dim LvNmCust As String
    Dim LvKdProduk As String
    Dim LvNmProduk As String

    Dim itemNoFaktur As Integer = 0
    Dim itemTgl As Integer = 1
    Dim itemJam As Integer = 2
    Dim itemUserID As Integer = 3
    Dim itemLokasi As Integer = 4
    Dim itemKdCust As Integer = 5
    Dim itemNmCust As Integer = 6
    Dim itemKdProduk As Integer = 7
    Dim itemNmProduk As Integer = 8

    Public Sub Get_Isi_Listview(ByVal NoIndex As Integer)
        LvNo_Faktur = ListView1.Items(NoIndex).Text
        LvTgl = ListView1.Items(NoIndex).SubItems(itemTgl).Text
        LvJam = ListView1.Items(NoIndex).SubItems(itemJam).Text
        LvUserID = ListView1.Items(NoIndex).SubItems(itemUserID).Text
        LvLokasi = ListView1.Items(NoIndex).SubItems(itemLokasi).Text
        LvKdCust = ListView1.Items(NoIndex).SubItems(itemKdCust).Text
        LvNmCust = ListView1.Items(NoIndex).SubItems(itemNmCust).Text
        LvKdProduk = ListView1.Items(NoIndex).SubItems(itemKdProduk).Text
        LvNmProduk = ListView1.Items(NoIndex).SubItems(itemNmProduk).Text
    End Sub


    Private Sub Display_Cari_Keluar_Barang__Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Display_Cari_Keluar_Barang__Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView1.Columns.Clear()
        DataGridView1.Rows.Clear()

        'tgl_skg = ""
        'Timer2.Enabled = False

        ''ListView1.Columns.Add("No KB", 110, HorizontalAlignment.Center)
        ''ListView1.Columns.Add("Tgl", 70, HorizontalAlignment.Center)
        ''ListView1.Columns.Add("Jam", 70, HorizontalAlignment.Center)
        ''ListView1.Columns.Add("#", 0, HorizontalAlignment.Left)
        ''ListView1.Columns.Add("User ID", 100, HorizontalAlignment.Center)
        ''ListView1.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
        ''ListView1.Columns.Add("Total", 70, HorizontalAlignment.Right)
        ''ListView1.Columns.Add("Kode Cust", 120, HorizontalAlignment.Center)
        ''ListView1.Columns.Add("Nama", 120, HorizontalAlignment.Center)
        ''ListView1.Columns.Add("Kode Sales", 120, HorizontalAlignment.Center)
        ''ListView1.Columns.Add("Nama", 120, HorizontalAlignment.Center)
        ''ListView1.Columns.Add("#", 0, HorizontalAlignment.Left)
        ''ListView1.Columns.Add("No. ", 30, HorizontalAlignment.Right).DisplayIndex = 0
        ''ListView1.View = View.Details

        ListView1.Columns.Add("No Faktur", 110, HorizontalAlignment.Center)
        ListView1.Columns.Add("Tgl", 70, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jam", 70, HorizontalAlignment.Center)
        ListView1.Columns.Add("User ID", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode Customer", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama Customer", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Sales", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama Sales", 120, HorizontalAlignment.Center)
        'ListView1.Columns.Add("Status", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("#", 0, HorizontalAlignment.Left)
        ListView1.Columns.Add("No. ", 30, HorizontalAlignment.Right).DisplayIndex = 0
        ListView1.View = View.Details

        Button1_Click(Me, e)
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            OpenConn()

            SQL = "Select a.No_Faktur, a.Tanggal, a.Jam, "
            SQL = SQL & "a.UserId, a.Lokasi, a.Kode_Customer, "
            SQL = SQL & "b.Nama as Nama_Customer, a.kode_karyawan, c.nama as nama_karyawan, a.Status, cast(a.rv as bigint) as rvx  "
            SQL = SQL & "From Emi_PO a, Customers b, karyawan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.kode_customer = b.kode_customer and a.kode_karyawan = c.kode_karyawan "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null and a.flag_penjualan is null "
            SQL = SQL & "and a.lokasi = '" & Lokasi & "' "
            SQL = SQL & "Order By a.Lokasi, a.Tanggal + a.Jam "
            ListView1.Items.Clear()

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView1.Items.Add(.Rows(i).Item("No_Faktur")) '0
                        Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd-MMM-yyyy")) '1
                        Lvw.SubItems.Add(.Rows(i).Item("Jam")) '2
                        Lvw.SubItems.Add(.Rows(i).Item("UserId")) '3
                        Lvw.SubItems.Add(.Rows(i).Item("Lokasi")) '4
                        Lvw.SubItems.Add(.Rows(i).Item("Kode_Customer")) '5
                        Lvw.SubItems.Add(.Rows(i).Item("Nama_Customer")) '6  
                        Lvw.SubItems.Add(.Rows(i).Item("kode_karyawan")) '7
                        Lvw.SubItems.Add(.Rows(i).Item("nama_karyawan")) '8  
                        Lvw.SubItems.Add(.Rows(i).Item("rvx")) '9
                        Lvw.SubItems.Add(i + 1) '10

                        If General_Class.CekNULL(.Rows(i).Item("Status")) = "Y" Then
                            ListView1.Items(i).ForeColor = Batal
                        Else
                            ListView1.Items(i).ForeColor = Color.Blue
                        End If
                    Next
                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_Click(sender As Object, e As EventArgs) Handles ListView1.Click
        Get_Isi_Listview(ListView1.FocusedItem.Index)
        If ListView1.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            DataGridView1.Rows.Clear()
            Dim no As Integer = 0

            Dim boleh_lihat As Boolean

            SQL = "select flag_hide_stock, "
            SQL = SQL & "ISNULL(("
            SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            SQL = SQL & "), 'T') AS boleh_lihat_stock "
            SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_stock_owner = '" & Penjualan_New.ComboBox4.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("flag_hide_stock") = "Y" Then
                        If Dr("boleh_lihat_stock") = "Y" Then
                            boleh_lihat = True
                        Else
                            boleh_lihat = False
                        End If
                    Else
                        boleh_lihat = True
                    End If
                Else
                    boleh_lihat = False
                End If
            End Using

            SQL = "Select a.kode_stock_owner, a.Kode_Produk, b.Nama, b.Good_Stock, a.Jumlah_Produksi as Jumlah_kirim,  a.Jenis_Satuan, b.kode_kategori2, a.id_gudang, "

            SQL = SQL & "isnull(("
            SQL = SQL & "Select x.Good_Stock from Barang x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_barang = b.kode_barang and x.kode_stock_owner = a.kode_stock_owner "
            SQL = SQL & "), 0) as Stock_Dist "

            SQL = SQL & ",isnull((select  z.nama_kabupaten_kota+' - '+ v.nama_Kecamatan from Emi_Customer_Gudang x, tbl_provinsi y, "
            SQL = SQL & "tbl_kabupaten_kota z, tbl_kecamatan v, tbl_kelurahan w where x.kode_perusahaan=a.Kode_Perusahaan and "
            SQL = SQL & "x.Urut_Oto=a.Id_Gudang and x.Id_Provinsi=y.Id_Provinsi and x.Id_Kabupaten_Kota=z.id_kabupaten_kota "
            SQL = SQL & "and x.Id_Kecamatan=v.id_kecamatan and x.Id_Kelurahan=w.id_kelurahan),'-') as Lokasi_Tujuan "

            SQL = SQL & "From Emi_PO_Detail a, Barang b, emi_po c "
            SQL = SQL & "Where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & " And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & " And a.Kode_Produk = b.Kode_Barang "

            SQL = SQL & "and a.No_Faktur = c.No_Faktur and a.Kode_Perusahaan =c.Kode_Perusahaan "

            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & LvNo_Faktur & "' "
            SQL = SQL & "order by a.no_urut"

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    DataGridView1.Rows.Add(1)

                    DataGridView1.Rows.Item(no).Cells(0).Value = Dr("kode_stock_owner")
                    DataGridView1.Rows.Item(no).Cells(1).Value = Dr("Kode_Produk")
                    DataGridView1.Rows.Item(no).Cells(2).Value = Dr("nama")
                    If boleh_lihat = True Then
                        DataGridView1.Rows.Item(no).Cells(3).Value = Dr("Stock_Dist")
                    Else
                        DataGridView1.Rows.Item(no).Cells(3).Value = ""
                    End If
                    DataGridView1.Rows.Item(no).Cells(4).Value = "-"
                    DataGridView1.Rows.Item(no).Cells(5).Value = Dr("Jumlah_kirim")
                    DataGridView1.Rows.Item(no).Cells(6).Value = "-"
                    DataGridView1.Rows.Item(no).Cells(7).Value = "0"
                    DataGridView1.Rows.Item(no).Cells(8).Value = "-"
                    DataGridView1.Rows.Item(no).Cells(9).Value = "-"
                    DataGridView1.Rows.Item(no).Cells(10).Value = "-"
                    DataGridView1.Rows.Item(no).Cells(11).Value = "-"
                    DataGridView1.Rows.Item(no).Cells(12).Value = "-"
                    DataGridView1.Rows.Item(no).Cells(13).Value = Dr("Lokasi_Tujuan")
                    DataGridView1.Rows.Item(no).Cells(14).Value = Dr("id_gudang")
                    no = no + 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub ListView1_DockChanged(sender As Object, e As EventArgs) Handles ListView1.DockChanged

    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick

    End Sub

    ''Public Sub laporan(ByVal formula As String, ByVal cr_title As String, ByVal form_title As String)
    ''    CrDoc.RecordSelectionFormula = formula
    ''    CrDoc.SummaryInfo.ReportTitle = cr_title
    ''    A_Place_For_Printing.Text = form_title
    ''End Sub

    ''Private Sub cetak()
    ''    Try

    ''        OpenConn()

    ''        Dim CrDoc As New Object
    ''        Dim kertas As String = ""

    ''        SQL = "select kode_perusahaan from detail_permintaan_keluar where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
    ''        Using Ds = BindingTrans(SQL)
    ''            If Ds.Tables("MyTable").Rows.Count <> 0 Then
    ''                CrDoc = New Faktur_PO_Toko
    ''                kertas = "Faktur"

    ''                CrDoc.SetDataSource(Ds)
    ''                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabaseTetangga)
    ''                CrDoc.PrintOptions.PrinterName = PrinterName
    ''                CrDoc.RecordSelectionFormula = "{detail_permintaan_keluar.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_permintaan_keluar.no_faktur} = '" & ListView1.FocusedItem.Text & "'"
    ''                'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

    ''                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
    ''                doctoprint.PrinterSettings.PrinterName = PrinterName
    ''                Dim rawKind As Integer
    ''                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
    ''                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
    ''                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
    ''                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
    ''                        CrDoc.PrintOptions.PaperSize = rawKind
    ''                        Exit For
    ''                    End If
    ''                Next

    ''                CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
    ''                CrDoc.PrintToPrinter(1, False, 1, 99)
    ''            End If
    ''        End Using

    ''        CloseConn()

    ''    Catch ex As Exception

    ''    End Try
    ''End Sub



    ''Private Sub Display_Data_Penjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    ''    Try
    ''        OpenConn()

    ''        Using Dr = OpenTrans("select dateadd(hh, -1, getdate()) as Jam")
    ''            If Dr.Read Then
    ''                tgl_skrg = Format(Dr("jam"), "yyyy-MM-dd HH:mm:ss")
    ''                Timer2.Enabled = True
    ''            End If
    ''        End Using

    ''        'iniiiii
    ''        'Dim boleh_lihat_global As Boolean

    ''        SQL = "select flag_hide_stock, "
    ''        SQL = SQL & "ISNULL(("
    ''        SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
    ''        SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
    ''        SQL = SQL & "), 'T') AS boleh_lihat_stock "
    ''        SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
    ''        SQL = SQL & "x.kode_stock_owner = '" & Penjualan_New.ComboBox4.Text & "'"
    ''        Using Dr = OpenTrans(SQL)
    ''            If Dr.Read Then
    ''                If Dr("flag_hide_stock") = "Y" Then
    ''                    If Dr("boleh_lihat_stock") = "Y" Then
    ''                        boleh_lihat_global = True
    ''                    Else
    ''                        boleh_lihat_global = False
    ''                    End If
    ''                Else
    ''                    boleh_lihat_global = True
    ''                End If
    ''            Else
    ''                boleh_lihat_global = False
    ''            End If
    ''        End Using

    ''        'iniiii
    ''        If boleh_lihat_global = True Then
    ''            DataGridView1.Columns(2).Visible = True
    ''        Else
    ''            DataGridView1.Columns(2).Visible = False
    ''        End If
    ''        'iniiii

    ''        CloseConn()

    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show("Error pada jam!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        tgl_skrg = ""
    ''        Timer2.Enabled = False

    ''    End Try





    ''    Dim boleh As String = "T"
    ''    Try
    ''        OpenConn()

    ''        If CekButtonRole("ubah_diskon") = "T" Then
    ''            boleh = "T"
    ''        Else
    ''            boleh = "Y"
    ''        End If

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try


    ''    If boleh = "Y" Then
    ''        DiskonPerBarangToolStripMenuItem.Visible = True
    ''        DiskonSemuaBarangToolStripMenuItem.Visible = True
    ''        ToolStripMenuItem1.Visible = True
    ''        PaketPerBarangToolStripMenuItem.Visible = True
    ''        PaketSemuaBarangToolStripMenuItem.Visible = True
    ''    Else
    ''        DiskonPerBarangToolStripMenuItem.Visible = False
    ''        DiskonSemuaBarangToolStripMenuItem.Visible = False
    ''        ToolStripMenuItem1.Visible = False
    ''        PaketPerBarangToolStripMenuItem.Visible = False
    ''        PaketSemuaBarangToolStripMenuItem.Visible = False
    ''    End If

    ''    Button1_Click(Me, e)
    ''End Sub

    ''Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    ''    'If CheckBox1.Checked = False And CheckBox2.Checked = False Then
    ''    '    MessageBox.Show("Pilih terlebih dahulu parameter pencarian data . . ! !", Judul)
    ''    '    CheckBox1.Focus() : Exit Sub
    ''    'End If

    ''    'If CheckBox1.Checked Then
    ''    '    If ComboBox1.SelectedIndex = -1 Then
    ''    '        MessageBox.Show("Parameter pencarian per tanggal harus diisi . . ! !", Judul)
    ''    '        ComboBox1.Focus() : Exit Sub
    ''    '    ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
    ''    '        MessageBox.Show("Periode I tidak boleh lebih dari periode II . . ! !", Judul)
    ''    '        DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
    ''    '        Exit Sub
    ''    '    End If
    ''    'End If
    ''    'If CheckBox2.Checked Then
    ''    '    If ComboBox2.SelectedIndex = -1 Then
    ''    '        MessageBox.Show("Parameter lain harus diisi . . ! !", Judul)
    ''    '        ComboBox2.Focus() : Exit Sub
    ''    '    ElseIf TextBox1.Text.Trim.Length = 0 Then
    ''    '        MessageBox.Show("Value parameter lain harus diisi . . ! !", Judul)
    ''    '        TextBox1.Focus() : Exit Sub
    ''    '    End If
    ''    'End If

    ''    SQL = "select a.lokasi, a.status, a.no_faktur, a.tanggal, a.jam, d.nama, "
    ''    SQL = SQL & "a.Userid, a.total_jml, d.kode_customer, d.nama as nama_cust, f.kode_karyawan, "
    ''    SQL = SQL & "f.nama as nama_karyawan, cast(a.rv as bigint) as rvx from "
    ''    SQL = SQL & "permintaan_keluar a, detail_permintaan_keluar b, barang c, "
    ''    SQL = SQL & "customers d, stock_owner e, karyawan f where "
    ''    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
    ''    SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and d.kode_perusahaan = e.kode_perusahaan and "
    ''    SQL = SQL & "e.kode_perusahaan = f.kode_perusahaan and "
    ''    SQL = SQL & "a.no_faktur = b.no_faktur and b.kode_stock_owner = c.kode_stock_owner and "
    ''    SQL = SQL & "b.kode_barang = c.kode_barang and a.kode_customer = d.kode_customer and "
    ''    SQL = SQL & "a.lokasi = e.kode_stock_owner and a.kode_sales = f.kode_karyawan and "
    ''    SQL = SQL & "c.Kode_Perusahaan = '" & KodePerusahaan & "' and "
    ''    SQL = SQL & "a.semua is null and b.pakai is null and a.status is null and "
    ''    SQL = SQL & "a.lokasi = '" & Lokasi & "' and "
    ''    SQL = SQL & "flag_apps is null "
    ''    If Penjualan_New.TextBox21.Text = "R" Then
    ''        SQL = SQL & "and c.flag_ppn = 'T' "
    ''    ElseIf Penjualan_New.TextBox21.Text = "C" Then
    ''        SQL = SQL & "and c.flag_ppn = 'Y' "
    ''    End If

    ''    SQL = SQL & "group by a.lokasi, a.status, a.no_faktur, a.tanggal, a.jam, d.nama, "
    ''    SQL = SQL & "a.Userid, a.total_jml, d.kode_customer, d.nama, f.kode_karyawan, f.nama, a.rv "
    ''    SQL = SQL & "Order by a.lokasi, a.tanggal + a.jam asc "

    ''    Try

    ''        OpenConn()

    ''        ListView1.Items.Clear()

    ''        Using Ds = BindingTrans(SQL)
    ''            With Ds.Tables("MyTable")
    ''                For i As Integer = 0 To .Rows.Count - 1
    ''                    Dim Lvw As ListViewItem
    ''                    Lvw = ListView1.Items.Add(.Rows(i).Item("no_faktur"))
    ''                    Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd-MMM-yyyy"))
    ''                    Lvw.SubItems.Add(.Rows(i).Item("jam"))
    ''                    Lvw.SubItems.Add("#")
    ''                    Lvw.SubItems.Add(.Rows(i).Item("Userid"))
    ''                    Lvw.SubItems.Add(.Rows(i).Item("lokasi"))
    ''                    Lvw.SubItems.Add(Format(.Rows(i).Item("total_jml"), "N0"))
    ''                    Lvw.SubItems.Add(.Rows(i).Item("kode_customer"))
    ''                    Lvw.SubItems.Add(.Rows(i).Item("nama_cust"))
    ''                    Lvw.SubItems.Add(.Rows(i).Item("kode_karyawan"))
    ''                    Lvw.SubItems.Add(.Rows(i).Item("nama_karyawan"))
    ''                    Lvw.SubItems.Add(.Rows(i).Item("rvx"))
    ''                    Lvw.SubItems.Add(i + 1)

    ''                    If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then
    ''                        ListView1.Items(i).ForeColor = Batal
    ''                    Else
    ''                        ListView1.Items(i).ForeColor = Color.Blue
    ''                    End If
    ''                Next
    ''            End With
    ''        End Using

    ''        CloseConn()

    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try
    ''End Sub

    ''Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
    ''    If ListView1.Items.Count = 0 Then Exit Sub

    ''    Try

    ''        OpenConn()

    ''        DataGridView1.Rows.Clear()
    ''        Dim no As Integer = 0

    ''        'iniiiii
    ''        Dim boleh_lihat As Boolean

    ''        SQL = "select flag_hide_stock, "
    ''        SQL = SQL & "ISNULL(("
    ''        SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
    ''        SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
    ''        SQL = SQL & "), 'T') AS boleh_lihat_stock "
    ''        SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
    ''        SQL = SQL & "x.kode_stock_owner = '" & Penjualan_New.ComboBox4.Text & "'"
    ''        Using Dr = OpenTrans(SQL)
    ''            If Dr.Read Then
    ''                If Dr("flag_hide_stock") = "Y" Then
    ''                    If Dr("boleh_lihat_stock") = "Y" Then
    ''                        boleh_lihat = True
    ''                    Else
    ''                        boleh_lihat = False
    ''                    End If
    ''                Else
    ''                    boleh_lihat = True
    ''                End If
    ''            Else
    ''                boleh_lihat = False
    ''            End If
    ''        End Using


    ''        SQL = "select b.Harus_Budgeting, b.kode_pkt2, b.pkt_2, c.kode_kategori2, b.no_urut, b.kode_pkt, b.disc_prsn, b.kode_barang, c.nama, b.jumlah, c.satuan, "

    ''        SQL = SQL & "isnull(("
    ''        SQL = SQL & "select x.good_stock from barang x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
    ''        SQL = SQL & "x.kode_barang = c.kode_barang and x.kode_stock_owner = '" & ListView1.Items(0).SubItems(5).Text & "'"
    ''        SQL = SQL & "), 0) as stock_dist "

    ''        SQL = SQL & "from detail_permintaan_keluar b, barang c where "
    ''        SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan and b.kode_stock_owner = c.kode_stock_owner and "
    ''        SQL = SQL & "b.kode_barang = c.kode_barang and c.Kode_Perusahaan = '" & KodePerusahaan & "' and "
    ''        SQL = SQL & "b.no_faktur = '" & ListView1.FocusedItem.Text & "' and b.pakai is null "
    ''        If Penjualan_New.TextBox21.Text = "R" Then
    ''            SQL = SQL & "and c.flag_ppn = 'T' "
    ''        ElseIf Penjualan_New.TextBox21.Text = "C" Then
    ''            SQL = SQL & "and c.flag_ppn = 'Y' "
    ''        End If
    ''        SQL = SQL & "order by no_urut"

    ''        Using Dr = Open(SQL)
    ''            Do While Dr.Read
    ''                DataGridView1.Rows.Add(1)

    ''                DataGridView1.Rows.Item(no).Cells(0).Value = Dr("kode_barang")
    ''                DataGridView1.Rows.Item(no).Cells(1).Value = Dr("nama")
    ''                'iniiiii
    ''                If boleh_lihat = True Then
    ''                    DataGridView1.Rows.Item(no).Cells(2).Value = Dr("stock_dist")
    ''                Else
    ''                    DataGridView1.Rows.Item(no).Cells(2).Value = ""
    ''                End If
    ''                'iniiiii
    ''                'DataGridView1.Rows.Item(no).Cells(2).Value = Dr("stock_dist")
    ''                DataGridView1.Rows.Item(no).Cells(3).Value = Dr("jumlah")
    ''                DataGridView1.Rows.Item(no).Cells(4).Value = Dr("jumlah")
    ''                DataGridView1.Rows.Item(no).Cells(5).Value = General_Class.CekNULL(Dr("kode_pkt"))
    ''                DataGridView1.Rows.Item(no).Cells(6).Value = Dr("disc_prsn")
    ''                DataGridView1.Rows.Item(no).Cells(7).Value = Dr("no_urut")
    ''                DataGridView1.Rows.Item(no).Cells(8).Value = Dr("kode_kategori2")
    ''                DataGridView1.Rows.Item(no).Cells(9).Value = General_Class.CekNULL(Dr("pkt_2"))
    ''                DataGridView1.Rows.Item(no).Cells(10).Value = General_Class.CekNULL(Dr("kode_pkt2"))
    ''                DataGridView1.Rows.Item(no).Cells(11).Value = General_Class.CekNULL(Dr("Harus_Budgeting"))

    ''                If General_Class.CekNULL(Dr("kode_pkt")) <> "" Then
    ''                    DataGridView1.Rows(no).DefaultCellStyle.BackColor = Color.SandyBrown
    ''                End If

    ''                If General_Class.CekNULL(Dr("kode_pkt2")) <> "" Then
    ''                    DataGridView1.Rows(no).DefaultCellStyle.BackColor = Color.Tan
    ''                End If

    ''                no = no + 1
    ''            Loop
    ''        End Using

    ''        CloseConn()

    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try
    ''End Sub

    ''Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
    ''    DataGridView1.Rows.Clear()
    ''End Sub

    ''Private Sub Display_Data_Transfer_Stock_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
    ''    Label1.Size = New Point(Me.Width, 33)
    ''End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            OpenConn()
            '8

            'Dim arrhrgmana As New ArrayList

            'SQL = "select c.kode_kategori2, sum((b.jumlah * c.x_hrg_mid) + (b.jumlah * c.x_hrg_mid * 11 / 100)) as ttl, d.total "
            'SQL = SQL & "from detail_permintaan_keluar b, barang c, promo_budgeting_bs d where "
            'SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan and "
            'SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and "
            'SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner And "
            'SQL = SQL & "b.kode_barang = c.kode_barang and "
            'SQL = SQL & "c.kode_kategori2 = d.kode_kategori2 and "
            'SQL = SQL & "c.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "b.no_faktur = '" & ListView1.FocusedItem.Text & "' and b.pakai is null "
            'SQL = SQL & "group by d.total, c.kode_kategori2 "
            'SQL = SQL & "order by c.kode_kategori2"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        If Dr("ttl") >= Dr("total") Then
            '            arrhrgmana.Add(Dr("kode_kategori2"))
            '        End If
            '    Loops
            ''End Using

            'Lvw = ListView1.Items.Add(.Rows(i).Item("No_Faktur")) '0
            'Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd-MMM-yyyy")) '1
            'Lvw.SubItems.Add(.Rows(i).Item("Jam")) '2
            'Lvw.SubItems.Add(.Rows(i).Item("UserId")) '3
            'Lvw.SubItems.Add(.Rows(i).Item("Lokasi")) '4
            'Lvw.SubItems.Add(.Rows(i).Item("Kode_Customer")) '5
            'Lvw.SubItems.Add(.Rows(i).Item("Nama_Customer")) '6
            'Lvw.SubItems.Add(.Rows(i).Item("Kode_Produk")) '7
            'Lvw.SubItems.Add(.Rows(i).Item("Nama_Produk")) '8
            'Lvw.SubItems.Add(.Rows(i).Item("Nama_Produk")) '9   



            'ListView1.Columns.Add("No Faktur", 110, HorizontalAlignment.Center)
            'ListView1.Columns.Add("Tgl", 70, HorizontalAlignment.Center)
            'ListView1.Columns.Add("Jam", 70, HorizontalAlignment.Center)
            'ListView1.Columns.Add("User ID", 100, HorizontalAlignment.Center)
            'ListView1.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
            'ListView1.Columns.Add("Kode Customer", 120, HorizontalAlignment.Center)
            'ListView1.Columns.Add("Nama Customer", 120, HorizontalAlignment.Center)
            'ListView1.Columns.Add("Kode Sales", 120, HorizontalAlignment.Center)
            'ListView1.Columns.Add("Nama Sales", 120, HorizontalAlignment.Center)
            ''ListView1.Columns.Add("Status", 120, HorizontalAlignment.Center)
            'ListView1.Columns.Add("#", 0, HorizontalAlignment.Left)
            'ListView1.Columns.Add("No. ", 30, HorizontalAlignment.Right).DisplayIndex = 0

            Penjualan_New.listview1.Items.Clear()
            Penjualan_New.TextBox15.Text = ListView1.FocusedItem.SubItems(5).Text
            Penjualan_New.RV_Permintaan_Keluar = ListView1.FocusedItem.SubItems(9).Text
            Penjualan_New.TextBox15_Leave(Button2, e)

            Penjualan_New.kd.Enabled = False
            Penjualan_New.Button7.Enabled = False


            Penjualan_New.CheckBox3.Checked = True
            Penjualan_New.CheckBox3.Enabled = True
            Penjualan_New.TextBox22.Text = ""
            Penjualan_New.TextBox22.Enabled = True


            Penjualan_New.TextBox2.Text = ListView1.FocusedItem.SubItems(7).Text
            Penjualan_New.TextBox2_Leave(Button2, e)
            Penjualan_New.TextBox2.Enabled = False
            Penjualan_New.TextBox3.Enabled = False

            Penjualan_New.TextBox23.Text = ListView1.FocusedItem.Text

            Penjualan_New.ComboBox5.SelectedIndex = 0
            'Penjualan_New.ComboBox2.SelectedIndex = -1
            Penjualan_New.ComboBox2.Enabled = False

            If Penjualan_New.ComboBox2.SelectedIndex = 0 Then 'tunai
                Penjualan_New.DateTimePicker2.Visible = False
                Penjualan_New.ComboBox5.Visible = False

                Penjualan_New.ComboBox2.Enabled = False
                Penjualan_New.ComboBox5.Enabled = False

                'Penjualan_New.ComboBoxCb1.Text = Penjualan_New.cb_default
                'Penjualan_New.ComboBoxCb1.Enabled = True
            Else 'kredit
                Penjualan_New.DateTimePicker2.Visible = False
                Penjualan_New.ComboBox5.Visible = False

                Penjualan_New.ComboBox2.Enabled = True
                Penjualan_New.ComboBox5.Enabled = True

                'Penjualan_New.ComboBoxCb1.SelectedIndex = 0
                'Penjualan_New.ComboBoxCb1.Enabled = False
            End If

            Penjualan_New.TextBoxGudang.Text = DataGridView1.Rows(0).Cells(0).Value

            Dim lvx As New ListViewItem
            For i As Integer = 0 To DataGridView1.RowCount - 1
                Dim dpt_budgeting As String = ""

                OpenConn()

                Dim harusbudgeting As String = ""
                harusbudgeting = "Y"


                Dim lv As New ListViewItem

                Penjualan_New.jml.Text = Val(DataGridView1.Rows(i).Cells(5).Value)
                Penjualan_New.kd.Text = DataGridView1.Rows(i).Cells(1).Value
                Penjualan_New.idgudang.Text = DataGridView1.Rows(i).Cells(14).Value
                Penjualan_New.kd_Leave_Retail(DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, harusbudgeting)
            Next

            CloseConn()

            Me.Close()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    ''Private Sub CetakPOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''    If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
    ''        MessageBox.Show("Pilih dahulu no PO yang mau dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        Exit Sub
    ''    End If

    ''    Try
    ''        OpenConn()

    ''        SQL = "select kode_perusahaan from detail_permintaan_keluar where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
    ''        Using Ds = BindingTrans(SQL)
    ''            If Ds.Tables("MyTable").Rows.Count <> 0 Then
    ''                CrDoc = New Faktur_PO_Toko

    ''                With A_Place_For_Printing
    ''                    CrDoc.SetDataSource(Ds)
    ''                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabaseTetangga)
    ''                    CrDoc.RecordSelectionFormula = "{detail_permintaan_keluar.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_permintaan_keluar.no_faktur} = '" & ListView1.FocusedItem.Text & "'"
    ''                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

    ''                    'CrDoc.SummaryInfo.ReportTitle = "Laporan Laba/Rugi " & Chr(13) & Format(DateTimePicker3.Value, "dd MMM yyyy") & " - " & Format(DateTimePicker4.Value, "dd MMM yyyy")
    ''                    .Text = "Faktur PO Toko"
    ''                    .CrystalReportViewer1.ReportSource = CrDoc
    ''                    .CrystalReportViewer1.ShowGroupTreeButton = False
    ''                    .Refresh()
    ''                    .Show()
    ''                    .Focus()
    ''                End With
    ''            Else
    ''                MessageBox.Show("Data tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''            End If
    ''        End Using

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try
    ''End Sub

    ''Private Sub DiskonPerBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiskonPerBarangToolStripMenuItem.Click
    ''    If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedRows.Count = 0 Then
    ''        MessageBox.Show("Pilih dahulu item yang akan diubah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        Exit Sub
    ''    End If

    ''    Try
    ''        OpenConn()

    ''        If CekButtonRole("ubah_diskon") = "T" Then
    ''            CloseTrans()
    ''            CloseConn()
    ''            MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''            Exit Sub
    ''        End If

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try


    ''    Dim x As String = InputBox("Masukan diskon", "Masukan diskon", "")
    ''    Dim hasil As Double = 0

    ''    If x.Trim.Length = 0 Then
    ''        hasil = 0
    ''    Else
    ''        hasil = HilangkanTanda(Format(Val(x), "N1"))
    ''    End If

    ''    Try
    ''        OpenConn()

    ''        SQL = "update detail_permintaan_keluar set disc_prsn = '" & hasil & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
    ''        SQL = SQL & "no_urut = '" & DataGridView1.Rows.Item(DataGridView1.CurrentCellAddress.Y).Cells(7).Value() & "'"
    ''        ExecuteTrans(SQL)

    ''        SQL = "insert into log_ubah_disc(kode_perusahaan, no_faktur, tanggal, jam, userid, jenis) values("
    ''        SQL = SQL & "'" & KodePerusahaan & "', '" & ListView1.FocusedItem.Text & "', "
    ''        SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
    ''        SQL = SQL & "'" & UserID & "', 'A')"
    ''        ExecuteTrans(SQL)

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try

    ''    ListView1_SelectedIndexChanged(DiskonPerBarangToolStripMenuItem, e)

    ''    ' DataGridView1.Rows.Item(DataGridView1.CurrentCellAddress.Y).Cells(6).Value() = hasil
    ''End Sub

    ''Private Sub DiskonSemuaBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiskonSemuaBarangToolStripMenuItem.Click
    ''    If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedRows.Count = 0 Then
    ''        MessageBox.Show("Pilih dahulu item yang akan diubah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        Exit Sub
    ''    End If

    ''    Try
    ''        OpenConn()

    ''        If CekButtonRole("ubah_diskon") = "T" Then
    ''            CloseTrans()
    ''            CloseConn()
    ''            MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''            Exit Sub
    ''        End If

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try


    ''    Dim x As String = InputBox("Masukan diskon", "Masukan diskon", "")
    ''    Dim hasil As Double = 0

    ''    If x.Trim.Length = 0 Then
    ''        hasil = 0
    ''    Else
    ''        hasil = HilangkanTanda(Format(Val(x), "N1"))
    ''    End If

    ''    Try
    ''        OpenConn()

    ''        SQL = "update detail_permintaan_keluar set disc_prsn = '" & hasil & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
    ''        SQL = SQL & "no_faktur = '" & ListView1.FocusedItem.Text & "'"
    ''        ExecuteTrans(SQL)

    ''        SQL = "insert into log_ubah_disc(kode_perusahaan, no_faktur, tanggal, jam, userid, jenis) values("
    ''        SQL = SQL & "'" & KodePerusahaan & "', '" & ListView1.FocusedItem.Text & "', "
    ''        SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
    ''        SQL = SQL & "'" & UserID & "', 'B')"
    ''        ExecuteTrans(SQL)

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try

    ''    ListView1_SelectedIndexChanged(DiskonPerBarangToolStripMenuItem, e)

    ''End Sub

    ''Private Sub PaketPerBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaketPerBarangToolStripMenuItem.Click
    ''    If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedRows.Count = 0 Then
    ''        MessageBox.Show("Pilih dahulu item yang akan diubah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        Exit Sub
    ''    End If

    ''    Try
    ''        OpenConn()

    ''        If CekButtonRole("ubah_diskon") = "T" Then
    ''            CloseTrans()
    ''            CloseConn()
    ''            MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''            Exit Sub
    ''        End If

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try


    ''    Dim x As String = InputBox("Masukan nama paket", "Masukan nama paket", "")

    ''    If x.Trim.Length <> 0 Then

    ''        Try
    ''            OpenConn()

    ''            SQL = "update detail_permintaan_keluar set kode_pkt = '" & x & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
    ''            SQL = SQL & "no_urut = '" & DataGridView1.Rows.Item(DataGridView1.CurrentCellAddress.Y).Cells(7).Value() & "'"
    ''            ExecuteTrans(SQL)

    ''            SQL = "insert into log_ubah_disc(kode_perusahaan, no_faktur, tanggal, jam, userid, jenis) values("
    ''            SQL = SQL & "'" & KodePerusahaan & "', '" & ListView1.FocusedItem.Text & "', "
    ''            SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
    ''            SQL = SQL & "'" & UserID & "', 'C')"
    ''            ExecuteTrans(SQL)

    ''            CloseConn()
    ''        Catch ex As Exception
    ''            CloseConn()
    ''            MessageBox.Show(ex.Message)
    ''            Exit Sub
    ''        End Try

    ''    End If

    ''    ListView1_SelectedIndexChanged(DiskonPerBarangToolStripMenuItem, e)

    ''End Sub

    ''Private Sub PaketSemuaBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaketSemuaBarangToolStripMenuItem.Click
    ''    If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedRows.Count = 0 Then
    ''        MessageBox.Show("Pilih dahulu item yang akan diubah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        Exit Sub
    ''    End If

    ''    Try
    ''        OpenConn()

    ''        If CekButtonRole("ubah_diskon") = "T" Then
    ''            CloseTrans()
    ''            CloseConn()
    ''            MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''            Exit Sub
    ''        End If

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try


    ''    Dim x As String = InputBox("Masukan nama paket", "Masukan nama paket", "")

    ''    If x.Trim.Length <> 0 Then

    ''        Try
    ''            OpenConn()

    ''            SQL = "update detail_permintaan_keluar set kode_pkt = '" & x & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
    ''            SQL = SQL & "no_faktur = '" & ListView1.FocusedItem.Text & "'"
    ''            ExecuteTrans(SQL)

    ''            SQL = "insert into log_ubah_disc(kode_perusahaan, no_faktur, tanggal, jam, userid, jenis) values("
    ''            SQL = SQL & "'" & KodePerusahaan & "', '" & ListView1.FocusedItem.Text & "', "
    ''            SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
    ''            SQL = SQL & "'" & UserID & "', 'D')"
    ''            ExecuteTrans(SQL)

    ''            CloseConn()
    ''        Catch ex As Exception
    ''            CloseConn()
    ''            MessageBox.Show(ex.Message)
    ''            Exit Sub
    ''        End Try

    ''    End If

    ''    ListView1_SelectedIndexChanged(DiskonPerBarangToolStripMenuItem, e)

    ''End Sub

    ''Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
    ''    tgl_skrg = Format(DateAdd(DateInterval.Second, 1, CDate(tgl_skrg)), "yyyy-MM-dd HH:mm:ss")
    ''End Sub

End Class