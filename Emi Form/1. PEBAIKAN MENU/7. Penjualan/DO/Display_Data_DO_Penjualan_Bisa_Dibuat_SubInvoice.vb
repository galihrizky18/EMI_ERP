Public Class Display_Data_DO_Penjualan_Bisa_Dibuat_SubInvoice
    Dim Arr1, Arr2, Arr3, arrbatal, ArrFilterMana As New ArrayList
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black


    'Private Sub cetak()
    '    Dim printers As New Printing.PrintDocument()
    '    Dim printerlama As String = printers.DefaultPageSettings.PrinterSettings.PrinterName
    '    Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", My.Settings.Prt_Name))

    '    Try
    '        OpenConn()

    '        SQL = "select b.subtotal, a.bayar, b.nota_kecil, b.kode_barang, a.jenis_transaksi, a.bayar, e.nama as namacustomer, "
    '        SQL = SQL & "c.nama as namabarang, b.harga, b.jumlah, d.nama as nama_perusahaan, "
    '        SQL = SQL & "d.alamat as alamat_perusahaan, d.telepon, a.no_faktur, f.username, "
    '        SQL = SQL & "a.tanggal, a.jam, a.disc1, a.disc2, a.grand, b.persen_diskon, b.nilai_diskon from "
    '        SQL = SQL & "penjualan a, detail_penjualan b, barang c, perusahaan d, customers e, users f where "
    '        SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
    '        SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and d.kode_perusahaan = e.kode_perusahaan and "
    '        SQL = SQL & "e.kode_perusahaan = f.kode_perusahaan and "
    '        SQL = SQL & "a.no_faktur = b.no_faktur and b.kode_stock_owner = c.kode_stock_owner and "
    '        SQL = SQL & "b.kode_barang = c.kode_barang and a.userid = f.userid and a.kode_customer = e.kode_customer and "
    '        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
    '        SQL = SQL & "a.no_faktur = '" & ListView1.FocusedItem.Text & "'"
    '        Using Ds = BindingTrans(SQL)
    '            With Ds.Tables("MyTable")
    '                Dim Prnt As New Printer
    '                Dim Subtotal As Double = 0
    '                Dim SubtotalTanpaDiskon As Double = 0
    '                Dim Grand As Double = 0
    '                '================

    '                '================

    '                'Prnt.Print()
    '                'Prnt.Print()
    '                'Prnt.Print()
    '                ' Prnt.Print()
    '                'Prnt.Print()
    '                'Prnt.Print()
    '                'Prnt.Print()
    '                'Prnt.Print()

    '                Prnt.Font = New Font("Courier New", 14)

    '                'Prnt.Print("".PadLeft((Pjg_Karakter_Print - Len(.Rows(0).Item("nama_perusahaan"))) / 3, " ") & .Rows(0).Item("nama_perusahaan"))
    '                Prnt.Print(.Rows(0).Item("nama_perusahaan"))
    '                Prnt.Font = New Font("Courier New", 8)
    '                Prnt.Print(.Rows(0).Item("alamat_perusahaan"))
    '                Prnt.Print(.Rows(0).Item("telepon"))
    '                Prnt.Print() 'Courier New
    '                Prnt.Font = New Font("Courier New", 7)
    '                Prnt.Print("=========================================")
    '                Prnt.Print(.Rows(0).Item("no_faktur"))
    '                Prnt.Print(Format(.Rows(0).Item("Tanggal"), "dd MMM yyyy") & " " & .Rows(0).Item("jam"))
    '                Prnt.Print(.Rows(0).Item("username"))
    '                Prnt.Print("=========================================")

    '                Prnt.Font = New Font("Courier New", 9, FontStyle.Regular)
    '                Dim tot_sblm_diskon As Double = 0
    '                Dim tot_diskon As Double = 0
    '                Dim tot_qty As Integer = 0

    '                For i As Integer = 0 To .Rows.Count - 1
    '                    SubtotalTanpaDiskon = .Rows(i).Item("harga") * .Rows(i).Item("jumlah")
    '                    Subtotal = SubtotalTanpaDiskon - (SubtotalTanpaDiskon * .Rows(i).Item("persen_diskon") / 100)
    '                    Subtotal = .Rows(i).Item("subtotal") ' HilangkanTanda(Format(Subtotal, "N0"))
    '                    Prnt.Print(Strings.Left(.Rows(i).Item("kode_barang"), 15).PadRight(15, " ") & " X " & Format(.Rows(i).Item("jumlah"), "N0").PadLeft(4) & "  " & Strings.Left(.Rows(i).Item("nota_kecil"), 1) & " " & Strings.Mid(.Rows(i).Item("nota_kecil"), 2))
    '                    Prnt.Print(Format(.Rows(i).Item("harga"), "N0").ToString.PadLeft(9, " ") & " " & " " & Format(.Rows(i).Item("persen_diskon"), "N0").ToString.PadLeft(3, " ") & "% " & Format(.Rows(i).Item("nilai_diskon"), "N0").ToString.PadLeft(7, " ") & " " & Format(Subtotal, "N0").ToString.PadLeft(9, " "))
    '                    tot_sblm_diskon = tot_sblm_diskon + (.Rows(i).Item("jumlah") * .Rows(i).Item("harga"))
    '                    Grand = Grand + Subtotal
    '                    tot_qty = tot_qty + .Rows(i).Item("jumlah")
    '                Next

    '                Dim Kembali As Double = 0
    '                tot_diskon = tot_sblm_diskon - Grand
    '                If .Rows(0).Item("jenis_transaksi") = "T" Then
    '                    Kembali = .Rows(0).Item("bayar") - .Rows(0).Item("grand")
    '                Else
    '                    Kembali = 0
    '                End If

    '                Prnt.Print("-----------------------------------------")

    '                Prnt.Print("Subtotal   : Rp.".ToString.PadLeft(24, " ") & Format(tot_sblm_diskon, "N0").ToString.PadLeft(9, " "))
    '                'Prnt.Print("Disc  ".ToString.PadLeft(16, " ") & .Rows(0).Item("disc1").ToString.PadLeft(4, " ") & "%: Rp." & Format(Grand * .Rows(0).Item("disc1").ToString.PadLeft(4, " ") / 100, "N0").ToString.PadLeft(15, " "))
    '                Prnt.Print("Disc(Rp)   : Rp.".ToString.PadLeft(24, " ") & Format(tot_diskon, "N0").ToString.PadLeft(9, " "))
    '                Prnt.Print("Grand Total: Rp.".ToString.PadLeft(24, " ") & Format(.Rows(0).Item("grand"), "N0").ToString.PadLeft(9, " "))

    '                Prnt.Print("Bayar      : Rp.".ToString.PadLeft(24, " ") & Format(.Rows(0).Item("bayar"), "N0").ToString.PadLeft(9, " "))
    '                Prnt.Print("Kembali    : Rp.".ToString.PadLeft(24, " ") & Format(Kembali, "N0").ToString.PadLeft(9, " "))
    '                Prnt.Print()
    '                Prnt.Print("Total Qty  : " & Format(tot_qty, "N0"))
    '                Prnt.Print("-----------------------------------------")
    '                Prnt.Font = New Font("Courier New", 7, FontStyle.Regular)
    '                Prnt.Print("*** Terima Kasih ***".ToString.PadLeft(31, " "))
    '                Prnt.Print("  Barang yang sudah dibeli tidak dapat")
    '                Prnt.Print("       ditukar atau dikembalikan")

    '                Prnt.EndDoc()
    '            End With
    '        End Using

    '        Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", printerlama))

    '        CloseConn()

    '    Catch ex As Exception
    '        Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", printerlama))

    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub cetak()
        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select kode_perusahaan from rekap_sub_invoice where kode_perusahaan = '" & KodePerusahaan & "' and no_do = '" & ListView1.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    'komen kedua
                    CrDoc = New Faktur_Sub_Invoice
                    kertas = "Faktur"

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{rekap_sub_invoice.Kode_Perusahaan} = '" & KodePerusahaan & "' and {rekap_sub_invoice.no_do} = '" & ListView1.FocusedItem.Text & "'"
                    '    CrDoc.SummaryInfo.ReportTitle = "[DUPLIKAT]"
                    '    .Text = "[DUPLIKAT]"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With


                    '=================================================================================================================================================
                    '=================================================================================================================================================

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{rekap_sub_invoice.Kode_Perusahaan} = '" & KodePerusahaan & "' and {rekap_sub_invoice.no_do} = '" & ListView1.FocusedItem.Text & "'"
                    CrDoc.SummaryInfo.ReportTitle = "[DUPLIKAT]"

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


    Private Sub cetakAlfamart()
        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select kode_perusahaan, lokasi from rekap_sub_invoice where kode_perusahaan = '" & KodePerusahaan & "' and no_do = '" & ListView1.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    If Strings.Left(Ds.Tables("MyTable").Rows(0).Item("lokasi"), 4) = "ALFA" Then
                        'komen pertama
                        CrDoc = New Faktur_Sub_Invoice_Alfamart

                        kertas = "Faktur"

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = PrinterName
                        CrDoc.RecordSelectionFormula = "{rekap_sub_invoice.Kode_Perusahaan} = '" & KodePerusahaan & "' and {rekap_sub_invoice.no_do} = '" & ListView1.FocusedItem.Text & "'"
                        'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

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
                End If
            End Using

            CloseConn()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cetak_byMuat()
        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select a.kode_perusahaan, b.flag_by_muat from detail_do_new a, stock_owner b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and no_do = '" & ListView1.FocusedItem.Text & "' and "
            SQL = SQL & "b.flag_by_muat = 'Y'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New Faktur_DO_Reseller_Hrg_Muat
                    kertas = "Faktur"

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{detail_do_new.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_do_new.no_do} = '" & ListView1.FocusedItem.Text & "'"
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

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
                Else
                    CloseConn()
                    MessageBox.Show("Tidak ada yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
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
        ListView1.Columns.Add("No DO", 105, HorizontalAlignment.Center)
        ListView1.Columns.Add("No Sub Invoice", 105, HorizontalAlignment.Center)
        ListView1.Columns.Add("No Faktur Penjualan", 105, HorizontalAlignment.Center)
        ListView1.Columns.Add("Tanggal DO", 70, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Customer", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Customer", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("Total", 80, HorizontalAlignment.Right)
        ListView1.Columns.Add("PPN", 80, HorizontalAlignment.Right)
        ListView1.Columns.Add("Grand", 80, HorizontalAlignment.Right)
        ListView1.Columns.Add("No. ", 30, HorizontalAlignment.Right).DisplayIndex = 0
        'ListView1.Columns.Add("Jns Kendaraan", 100, HorizontalAlignment.Center)
        'ListView1.Columns.Add("Kode Kendaraan", 120, HorizontalAlignment.Center)
        'ListView1.Columns.Add("Jns Driver", 100, HorizontalAlignment.Center)
        'ListView1.Columns.Add("Kode Driver", 120, HorizontalAlignment.Center)
        'ListView1.Columns.Add("Tujuan", 200, HorizontalAlignment.Left)
        'ListView1.Columns.Add("HP", 100, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        ListView1.View = View.Details
        'Lvw = ListView1.Items.Add(.Rows(i).Item("no_do"))
        'Lvw.SubItems.Add(.Rows(i).Item("no_do") & .Rows(i).Item("urutan_do"))
        'Lvw.SubItems.Add(.Rows(i).Item("no_faktur"))
        'Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_do"), "dd-MMM-yyyy"))
        'Lvw.SubItems.Add(.Rows(i).Item("jam"))
        'Lvw.SubItems.Add(.Rows(i).Item("kode_customer"))
        'Lvw.SubItems.Add(.Rows(i).Item("nama"))
        'Lvw.SubItems.Add(Format(.Rows(i).Item("total_baru_dikurang_diskon"), "N0"))
        'Lvw.SubItems.Add(Format(.Rows(i).Item("nilai_ppn_baru"), "N0"))
        'Lvw.SubItems.Add(Format(.Rows(i).Item("total_baru_dikurang_diskon") + .Rows(i).Item("nilai_ppn_baru"), "N0"))
        'Lvw.SubItems.Add(i + 1)
        ListView2.Columns.Add("Kode Stock Owner", 100, HorizontalAlignment.Center)
        ListView2.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
        ListView2.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Jml Kirim", 70, HorizontalAlignment.Right)
        ListView2.Columns.Add("Jml Validasi", 70, HorizontalAlignment.Right)
        ListView2.Columns.Add("Satuan", 80, HorizontalAlignment.Left)
        ListView2.Columns.Add("Disc(%)", 70, HorizontalAlignment.Right)
        ListView2.Columns.Add("Disc(Rp)", 0, HorizontalAlignment.Right)
        ListView2.Columns.Add("Subtotal", 70, HorizontalAlignment.Right)
        ListView2.View = View.Details

        'Lvw = ListView2.Items.Add(Dr("kode_stock_owner"))
        'Lvw.SubItems.Add(Dr("kode_barang"))
        'Lvw.SubItems.Add(Dr("nama"))
        'Lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
        'Lvw.SubItems.Add(Format(Dr("sdh_selesai_validasi"), "N0"))
        'Lvw.SubItems.Add(Dr("satuan"))
        'Lvw.SubItems.Add(Dr("persen_diskon"))
        'Lvw.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))
        'Lvw.SubItems.Add(Format(Dr("subtotal_baru"), "N0"))

        CheckBox1.Checked = False : CheckBox2.Checked = False
        ComboBox1.Items.Clear() : ComboBox1.Text = "" : Arr1.Clear()
        ComboBox1.Items.Add("Tanggal") : Arr1.Add("a.tanggal_do")

        ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear() : ArrFilterMana.Clear()
        ComboBox2.Items.Add("No DO") : Arr2.Add("no_do") : ArrFilterMana.Add("BAWAH")
        ComboBox2.Items.Add("No Faktur Penjualan") : Arr2.Add("No_Faktur") : ArrFilterMana.Add("BAWAH")
        ComboBox2.Items.Add("Kode Customer") : Arr2.Add("Kode_customer") : ArrFilterMana.Add("BAWAH")
        ComboBox2.Items.Add("Nama Customer") : Arr2.Add("nama_cust") : ArrFilterMana.Add("BAWAH")
        ComboBox2.Items.Add("Nama Group") : Arr2.Add("nama_group") : ArrFilterMana.Add("BAWAH")
        ComboBox2.Items.Add("Kode Barang") : Arr2.Add("Kode_Barang") : ArrFilterMana.Add("ATAS")
        ComboBox2.Items.Add("Nama Barang") : Arr2.Add("Nama") : ArrFilterMana.Add("ATAS")
        'a.urutan_do, a.no_do, a.no_faktur, a.tanggal_do, a.jam, ""
        'SQL = SQL & "b.kode_stock_owner, a.kode_customer, "
        'SQL = SQL & "b.nama_cust, a.lokasi, a.total_baru_dikurang_diskon, a.nilai_ppn_baru "
        'SQL = SQL & "from rekap_sub_invoice a, sub_invoice b "
        DateTimePicker1.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text)
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

        ComboBox5.Items.Clear() : arrbatal.Clear()
        ComboBox5.Items.Add("-- Seluruh --") : arrbatal.Add("")
        ComboBox5.Items.Add("Batal") : arrbatal.Add("and a.status = 'Y' ")
        ComboBox5.Items.Add("Tdk Batal") : arrbatal.Add("and a.status is null ")
        ComboBox5.SelectedIndex = 2

        ComboBox1.Enabled = False : ComboBox2.Enabled = False
        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        TextBox1.Enabled = False
        Label17.Text = "0"

        Try
            OpenConn()

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

            If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
                ComboBox6.Enabled = False
            Else
                ComboBox6.Enabled = True
            End If
            'If ComboBox6.SelectedIndex = 0 Then
            '    SQL = SQL & " and b.lokasi in("
            '    Dim list_kota As String = ""
            '    For x As Integer = 1 To ComboBox6.Items.Count - 1
            '        list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
            '    Next

            '    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            '    SQL = SQL & list_kota & ")"
            'Else
            '    SQL = SQL & " and b.lokasi = '" & ComboBox6.Text & "'"
            'End If
            CloseConn()
        Catch ex As Exception
            ComboBox6.Items.Clear()
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
            ComboBox1.SelectedIndex = -1 : DateTimePicker1.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text)
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
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            CheckBox1.Focus() : Exit Sub
        End If

        If CheckBox1.Checked Then
            If ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Parameter pencarian per tanggal harus diisi!", Judul)
                ComboBox1.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text)
                Exit Sub
            End If
        End If
        If CheckBox2.Checked Then
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

            SQL = ";with rekap_subinvoice_cte as ( "
            SQL = SQL & "select a.urutan_do, a.no_do, a.no_faktur, a.tanggal_do, a.jam,  "
            SQL = SQL & "b.kode_stock_owner, a.kode_customer,"

            SQL = SQL & "isnull((select x.nama from Customers x where x.kode_perusahaan = a.kode_perusahaan "
            SQL = SQL & "and x.kode_customer = a.kode_cust_group), null) as nama_group,"

            SQL = SQL & "b.nama_cust, a.lokasi, a.total_baru_dikurang_diskon, a.nilai_ppn_baru "
            SQL = SQL & "from rekap_sub_invoice a, sub_invoice b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and "
            SQL = SQL & "a.no_do = b.no_do and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "

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

                SQL = SQL & " a.tanggal_do between '"
                SQL = SQL & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' and '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            End If

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr1.Item(ComboBox1.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                If ArrFilterMana.Item(ComboBox2.SelectedIndex) = "ATAS" Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "WHERE "

                    SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
                End If
            End If

            'If CheckBox2.Checked Then
            '    'Pasang And
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
            'End If

            SQL = SQL & "group by a.urutan_do,a.kode_perusahaan,a.kode_cust_group, a.no_do, a.no_faktur, a.tanggal_do, a.jam,  "
            SQL = SQL & "b.kode_stock_owner, a.kode_customer, "
            SQL = SQL & "b.nama_cust, a.lokasi, a.total_baru_dikurang_diskon, a.nilai_ppn_baru )"
            SQL = SQL & "select * from rekap_subinvoice_cte "
            If CheckBox2.Checked Then
                If ArrFilterMana.Item(ComboBox2.SelectedIndex) = "BAWAH" Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "WHERE "

                    SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
                End If
            End If
            SQL = SQL & "Order by lokasi, tanggal_do Desc "

            Dim GrandFaktur As Double = 0
            Label17.Text = "0"

            ListView1.Items.Clear() : ListView2.Items.Clear()
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Lvw = ListView1.Items.Add(.Rows(i).Item("no_do"))
                        Lvw.SubItems.Add(.Rows(i).Item("no_faktur") & .Rows(i).Item("urutan_do"))
                        Lvw.SubItems.Add(.Rows(i).Item("no_faktur"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_do"), "dd-MMM-yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_customer"))
                        If General_Class.CekNULL(.Rows(i).Item("nama_group")) = "" Then
                            Lvw.SubItems.Add(.Rows(i).Item("nama_cust"))
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("nama_cust") & " (" & .Rows(i).Item("nama_group") & ")")
                        End If
                        Lvw.SubItems.Add(Format(.Rows(i).Item("total_baru_dikurang_diskon"), "N0"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("nilai_ppn_baru"), "N0"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("total_baru_dikurang_diskon") + .Rows(i).Item("nilai_ppn_baru"), "N0"))
                        Lvw.SubItems.Add(i + 1)
                        'Lvw.SubItems.Add(.Rows(i).Item("jenis_kendaraan"))
                        'Lvw.SubItems.Add(.Rows(i).Item("kode_kendaraan"))
                        'Lvw.SubItems.Add(.Rows(i).Item("jenis_driver"))
                        'Lvw.SubItems.Add(.Rows(i).Item("kode_driver"))
                        'Lvw.SubItems.Add(.Rows(i).Item("tujuan"))
                        'Lvw.SubItems.Add(.Rows(i).Item("hp"))
                        'Lvw.SubItems.Add(.Rows(i).Item("keterangan"))
                        ListView1.Items(i).ForeColor = T
                        GrandFaktur = GrandFaktur + (.Rows(i).Item("total_baru_dikurang_diskon") + .Rows(i).Item("nilai_ppn_baru"))
                    Next
                End With
            End Using

            Label17.Text = Format(GrandFaktur, "N0")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

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

    Private Sub BatalkanTransaksiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan transaksi DO penjualan ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbYes Then

            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("batal_do_penjualan") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "select a.status from do_penjualan a where "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_do = '" & ListView1.FocusedItem.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No DO tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "update do_penjualan set status = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and no_do = '" & ListView1.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "insert into log_do_penjualan(kode_perusahaan, no_faktur, tanggal, jam, userid, jenis) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & ListView1.FocusedItem.Text & "', "
                SQL = SQL & "'" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "', 'B')"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Transaksi DO penjualan berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ListView1.FocusedItem.ForeColor = Batal
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub KembalikanTransaksiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
        '    MessageBox.Show("Pilih dahulu no faktur yang mau dikembalikan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'Dim lanjut As Boolean
        'Dim Transaksi As String = ""
        'Dim Lunas As String = ""
        'Dim grand As Double = 0
        'Dim ttl_point As Double = 0
        'Dim x_pakai_point As String = ""
        'Dim x_cust As String = ""

        'Try
        '    OpenConn()

        '    SQL = "select grand, jenis_transaksi, flag_lunas, total_point, pakai_point, kode_customer, status from penjualan where "
        '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            grand = Dr("grand")
        '            Transaksi = Dr("jenis_transaksi")
        '            Lunas = General_Class.CekNULL(Dr("flag_lunas"))
        '            ttl_point = Dr("total_point")
        '            x_pakai_point = Dr("pakai_point")
        '            x_cust = Dr("kode_customer")

        '            If UCase(General_Class.CekNULL(Dr("status"))) = "Y" Then
        '                lanjut = True
        '            Else
        '                lanjut = False
        '            End If
        '        Else
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Data penjualan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using

        '    If lanjut = True Then
        '        Dim tanya1 As String = MessageBox.Show("Yakin akan kembalikan transaksi penjualan ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '        If tanya1 = vbYes Then
        '            Cmd.Transaction = Cn.BeginTransaction

        '            Dim rand As New Random   '= Format(Now, "MMddHHmmss") & Format(rand.Next(0, 100000), "00000")
        '            Dim StrRand As String = ""
        '            StrRand = Format(CDate(FmenuDevFix.ToolStripStatusLabel3.Text), "MMddHHmmss") & Format(rand.Next(0, 100000), "00000")

        '            SQL = "select * from detail_penjualan where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
        '            Using Ds = BindingTrans(SQL)
        '                With Ds.Tables("MyTable")
        '                    If .Rows.Count <> 0 Then
        '                        For i As Integer = 0 To .Rows.Count - 1
        '                            SQL = "select * from cart where kode_perusahaan = '" & KodePerusahaan & "' and no_unik = '" & StrRand & "' and "
        '                            SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' and "
        '                            SQL = SQL & "kode_barang = '" & .Rows(i).Item("kode_barang") & "'"
        '                            Using dr = OpenTrans(SQL)
        '                                If dr.Read Then
        '                                    'update
        '                                    dr.Close()
        '                                    SQL = "update cart set jumlah = jumlah + " & .Rows(i).Item("jumlah") & " where "
        '                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_unik = '" & StrRand & "' and "
        '                                    SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' and "
        '                                    SQL = SQL & "kode_barang = '" & .Rows(i).Item("kode_barang") & "'"
        '                                    ExecuteTrans(SQL)
        '                                Else
        '                                    'insert 
        '                                    dr.Close()
        '                                    SQL = "insert into cart(kode_perusahaan, no_unik, kode_stock_owner, kode_barang, jumlah) values("
        '                                    SQL = SQL & "'" & KodePerusahaan & "', '" & StrRand & "', '" & .Rows(i).Item("kode_stock_owner") & "', "
        '                                    SQL = SQL & "'" & .Rows(i).Item("kode_barang") & "', " & .Rows(i).Item("jumlah") & ")"
        '                                    ExecuteTrans(SQL)
        '                                End If
        '                            End Using
        '                        Next
        '                    Else
        '                        CloseTrans()
        '                        CloseConn()
        '                        MessageBox.Show("Data penjualan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                        Exit Sub
        '                    End If
        '                End With
        '            End Using

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
        '                SQL = "select * from barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ListView3.Items(i).Text & "' and kode_barang = '" & ListView3.Items(i).SubItems(1).Text & "'"
        '                Using Dr = OpenTrans(SQL)
        '                    If Dr.Read Then
        '                        If Dr("good_stock") - HilangkanTanda(ListView3.Items(i).SubItems(2).Text) < BolehNegatif Then
        '                            Dr.Close()
        '                            CloseTrans()
        '                            ' ExecuteTrans("delete from cart where kode_perusahaan = '" & KodePerusahaan & "' and no_unik = '" & StrRand & "'")
        '                            CloseConn()
        '                            MessageBox.Show("Proses kembalikan membuat stock menjadi negatif untuk barang " & ListView3.Items(i).SubItems(1).Text & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        '                            Exit Sub
        '                        End If
        '                    End If
        '                End Using
        '            Next

        '            SQL = "delete from cart where kode_perusahaan = '" & KodePerusahaan & "' and no_unik = '" & StrRand & "'"
        '            ExecuteTrans(SQL)

        '            'update stok (kurangin)
        '            SQL = "select * from detail_penjualan where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
        '            Using Ds = BindingTrans(SQL)
        '                With Ds.Tables("MyTable")
        '                    If .Rows.Count <> 0 Then
        '                        For i As Integer = 0 To .Rows.Count - 1
        '                            SQL = "Update barang set good_stock = good_stock - " & .Rows(i).Item("jumlah") & " where "
        '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '                            SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' and "
        '                            SQL = SQL & "kode_barang = '" & .Rows(i).Item("kode_barang") & "'"
        '                            ExecuteTrans(SQL)
        '                        Next
        '                    Else
        '                        CloseTrans()
        '                        CloseConn()
        '                        MessageBox.Show("Data penjualan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                        Exit Sub
        '                    End If
        '                End With
        '            End Using

        '            If x_pakai_point = "Y" Then
        '                SQL = "update customers set point = point + " & ttl_point & " where "
        '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & x_cust & "'"
        '                ExecuteTrans(SQL)
        '            End If

        '            SQL = "update penjualan set status = NULL where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
        '            ExecuteTrans(SQL)

        '            SQL = "insert into log_penjualan(kode_perusahaan, no_faktur, tanggal, jam, userid, jenis) values("
        '            SQL = SQL & "'" & KodePerusahaan & "', '" & ListView1.FocusedItem.Text & "', "
        '            SQL = SQL & "'" & Format(CDate(FmenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FmenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
        '            SQL = SQL & "'" & UserID & "', 'K')"
        '            ExecuteTrans(SQL)

        '            Cmd.Transaction.Commit()

        '            MessageBox.Show("Transaksi penjualan berhasil dikembalikan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            If Transaksi = "T" Then
        '                ListView1.FocusedItem.ForeColor = T
        '                Label17.Text = Format(Val(HilangkanTanda(Label17.Text)) + grand, "N0")
        '            ElseIf Transaksi = "N" And Lunas = "" Then
        '                ListView1.FocusedItem.ForeColor = KT
        '                Label18.Text = Format(Val(HilangkanTanda(Label18.Text)) + grand, "N0")
        '            ElseIf Transaksi = "N" And Lunas = "Y" Then
        '                ListView1.FocusedItem.ForeColor = KY
        '                Label19.Text = Format(Val(HilangkanTanda(Label19.Text)) + grand, "N0")
        '            End If
        '            Label20.Text = Format(Val(HilangkanTanda(Label20.Text)) - grand, "N0")
        '            Label21.Text = Format(Val(HilangkanTanda(Label21.Text)) + grand, "N0")
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

    Private Sub CetakUlangFakturToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakUlangFakturToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        cetak()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            If ListView1.Items.Count = 0 Then Exit Sub

            OpenConn()

            Dim Grand As Double = 0
            ListView2.Items.Clear()
            SQL = "Select a.kode_stock_owner, a.Kode_barang, b.nama, a.jumlah, b.satuan, sdh_selesai_validasi, persen_diskon, nilai_diskon,subtotal_baru "

            SQL = SQL & "from sub_invoice a, barang b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and a.kode_barang = b.kode_barang and "
            SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_do = '" & ListView1.FocusedItem.Text & "' "
            SQL = SQL & "order by a.no_urut"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim GrandTotal As Double = 0
                    Dim Lvw As ListViewItem
                    Lvw = ListView2.Items.Add(Dr("kode_stock_owner"))
                    Lvw.SubItems.Add(Dr("kode_barang"))
                    Lvw.SubItems.Add(Dr("nama"))
                    Lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    Lvw.SubItems.Add(Format(Dr("sdh_selesai_validasi"), "N0"))
                    Lvw.SubItems.Add(Dr("satuan"))
                    Lvw.SubItems.Add(Dr("persen_diskon"))
                    Lvw.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))
                    Lvw.SubItems.Add(Format(Dr("subtotal_baru"), "N0"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Private Sub ValidasiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ValidasiToolStripMenuItem.Click
    '    If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
    '        MessageBox.Show("Pilih dahulu no faktur yang mau di validasi lunas!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    End If

    '    Dim TanyaDulu As String = MessageBox.Show("Anda yakin akan memvalidasi penjualan non tunai ini . . ? ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '    If TanyaDulu = vbNo Then
    '        Exit Sub
    '    End If

    '    Try
    '        Dim grand As Double = 0
    '        OpenConn()

    '        Cmd.Transaction = Cn.BeginTransaction

    '        SQL = "select * from penjualan where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
    '        Using dr = OpenTrans(SQL)
    '            If dr.Read Then
    '                grand = dr("grand")
    '                If UCase(General_Class.CekNULL(dr("status"))) = "Y" Then
    '                    dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Validasi tidak dapat dilakukan. Transaksi ini sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                ElseIf dr("jenis_transaksi") = "T" Then
    '                    dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Validasi tidak dapat dilakukan. Transaksi ini adalah transaksi tunai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                ElseIf dr("jenis_transaksi") = "N" And General_Class.CekNULL(dr("flag_lunas")) = "Y" Then
    '                    dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Validasi tidak dapat dilakukan. Transaksi ini sudah di validasi sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            Else
    '                dr.Close()
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Transaksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If
    '        End Using

    '        SQL = "Update penjualan set flag_lunas = 'Y', Tgl_lunas = '" & Format(CDate(FmenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', jam_lunas = '" & Format(CDate(FmenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "',"
    '        SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
    '        SQL = SQL & "no_faktur = '" & ListView1.FocusedItem.Text & "'"
    '        ExecuteTrans(SQL)

    '        Cmd.Transaction.Commit()

    '        CloseConn()

    '        MessageBox.Show("Transaksi penjualan berhasil divalidasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        ListView1.FocusedItem.ForeColor = KY
    '        Label18.Text = Format(Val(HilangkanTanda(Label18.Text)) - grand, "N0")
    '        Label19.Text = Format(Val(HilangkanTanda(Label19.Text)) + grand, "N0")
    '        ListView1.FocusedItem.SubItems(7).Text = "Y"
    '        ListView1.FocusedItem.SubItems(8).Text = Format(CDate(FmenuDevFix.ToolStripStatusLabel3.Text), "dd MMM yyyy")
    '        ListView1.FocusedItem.SubItems(10).Text = UserID
    '    Catch ex As Exception
    '        CloseTrans()
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    'Private Sub HapusValidasiLunasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HapusValidasiLunasToolStripMenuItem.Click
    '    If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
    '        MessageBox.Show("Pilih dahulu no faktur yang mau di hapus validasi lunas!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    End If

    '    Dim TanyaDulu As String = MessageBox.Show("Anda yakin akan menghapus validasi penjualan non tunai ini . . ? ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '    If TanyaDulu = vbNo Then
    '        Exit Sub
    '    End If

    '    Try
    '        Dim grand As Double = 0
    '        OpenConn()

    '        Cmd.Transaction = Cn.BeginTransaction

    '        SQL = "select * from penjualan where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
    '        Using dr = OpenTrans(SQL)
    '            If dr.Read Then
    '                grand = dr("grand")
    '                If UCase(General_Class.CekNULL(dr("status"))) = "Y" Then
    '                    dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Hapus validasi tidak dapat dilakukan. Transaksi ini sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                ElseIf dr("jenis_transaksi") = "T" Then
    '                    dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Hapus validasi tidak dapat dilakukan. Transaksi ini adalah transaksi tunai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                ElseIf dr("jenis_transaksi") = "N" And General_Class.CekNULL(dr("flag_lunas")) = "" Then
    '                    dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Hapus validasi tidak dapat dilakukan. Transaksi ini belum di validasi sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            Else
    '                dr.Close()
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Transaksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If
    '        End Using

    '        SQL = "Update penjualan set flag_lunas = NULL, Tgl_lunas = NULL, jam_lunas = '" & Format(CDate(FmenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "',"
    '        SQL = SQL & "uservalidasi = NULL where kode_perusahaan = '" & KodePerusahaan & "' and "
    '        SQL = SQL & "no_faktur = '" & ListView1.FocusedItem.Text & "'"
    '        ExecuteTrans(SQL)

    '        Cmd.Transaction.Commit()

    '        CloseConn()
    '        MessageBox.Show("Validasi penjualan berhasil dihapus.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        ListView1.FocusedItem.ForeColor = KT
    '        Label18.Text = Format(Val(HilangkanTanda(Label18.Text)) + grand, "N0")
    '        Label19.Text = Format(Val(HilangkanTanda(Label19.Text)) - grand, "N0")
    '        ListView1.FocusedItem.SubItems(7).Text = "-"
    '        ListView1.FocusedItem.SubItems(8).Text = "-"
    '        ListView1.FocusedItem.SubItems(10).Text = "-"
    '    Catch ex As Exception
    '        CloseTrans()
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    Private Sub TambahKeTagihanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Or ListView1.CheckedItems.Count = 0 Then
        '    MessageBox.Show("Pilih dahulu no faktur yang mau di tambah ke tagihan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'For i As Integer = 0 To ListView1.Items.Count - 1
        '    If ListView1.Items(i).Checked = True Then
        '        If ListView1.Items(i).SubItems(5).Text = "T" Then
        '            MessageBox.Show("No faktur " & ListView1.Items(i).Text & " adalah transaksi tunai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If

        '        For j As Integer = 0 To Tagihan.ListView1.Items.Count - 1
        '            If ListView1.Items(i).Text.ToUpper = Tagihan.ListView1.Items(j).Text.ToUpper Then
        '                MessageBox.Show("No faktur " & ListView1.Items(i).Text & " sudah di masukkan di tagihan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        Next

        '        Try
        '            OpenConn()

        '            SQL = "select a.status, a.no_faktur, a.tanggal, a.kode_customer, b.nama, a.grand from penjualan a, customers b where "
        '            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_customer = b.kode_customer and "
        '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & ListView1.Items(i).Text & "'"
        '            Using Dr = OpenTrans(SQL)
        '                If Dr.Read Then
        '                    If General_Class.CekNULL(Dr("status")) = "Y" Then
        '                        Dr.Close()
        '                        MessageBox.Show("No faktur " & ListView1.Items(i).Text & " sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                        Exit Sub
        '                    End If

        '                    Dim lv As New ListViewItem
        '                    lv = Tagihan.ListView1.Items.Add(Dr("no_faktur"))
        '                    lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
        '                    lv.SubItems.Add(Dr("kode_customer"))
        '                    lv.SubItems.Add(Dr("nama"))
        '                    lv.SubItems.Add(Format(Dr("grand"), "N0"))
        '                Else
        '                    Dr.Close()
        '                    MessageBox.Show("No faktur " & ListView1.Items(i).Text & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                End If
        '            End Using

        '            CloseConn()
        '        Catch ex As Exception
        '            CloseConn()
        '            MessageBox.Show(ex.Message)
        '            Exit Sub
        '        End Try
        '        ListView1.Items(i).Checked = False
        '    End If
        'Next
    End Sub

    Private Sub CopyNoFakturToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(ListView1.FocusedItem.Text)
    End Sub

    Private Sub Display_Data_Penjualan_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub PelunasanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau dilunasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan melunasi transaksi penjualan ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbYes Then

            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("lunas_penjualan") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim grand As Double = 0
                Dim JT As String = ""
                Dim Flag_Lunas As String = ""
                Dim ttl_point As Double = 0
                Dim x_pakai_point As String = ""
                Dim x_cust As String = ""

                SQL = "select a.status, a.grand, a.jenis_transaksi, a.flag_lunas "
                SQL = SQL & "from penjualan a where a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & ListView1.FocusedItem.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        grand = Dr("grand")
                        JT = Dr("jenis_transaksi")
                        Flag_Lunas = General_Class.CekNULL(Dr("flag_lunas"))

                        If General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("jenis_transaksi")) = "T" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini transaksi Tunai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("flag_lunas")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah lunas!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "update penjualan set flag_lunas = 'Y', "
                SQL = SQL & "tgl_lunas = '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', "
                SQL = SQL & "jam_lunas = '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "uservalidasi = '" & UserID & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Transaksi penjualan berhasil dilunasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ListView1.FocusedItem.ForeColor = Color.Green
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub CetakUlangModernOutletToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CetakUlangBiayaMuatToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        cetak_byMuat()
    End Sub

    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""
            Dim lokasi_file As String = Application.StartupPath & "\" & My.Computer.Name

            If System.IO.Directory.Exists(lokasi_file) = False Then
                System.IO.Directory.CreateDirectory(lokasi_file)
            End If

            Dim format_akhir As String = Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "ddMMMyyyyHHmmss")


            Dim nama_file As String = Replace(ListView1.FocusedItem.SubItems(1).Text, "/", "") & "_" & format_akhir

            SQL = "select kode_perusahaan from rekap_sub_invoice where kode_perusahaan = '" & KodePerusahaan & "' and no_do = '" & ListView1.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    'komen ketiga
                    CrDoc = New Faktur_Sub_Invoice

                    kertas = "Faktur"

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{rekap_sub_invoice.Kode_Perusahaan} = '" & KodePerusahaan & "' and {rekap_sub_invoice.no_do} = '" & ListView1.FocusedItem.Text & "'"
                    CrDoc.SummaryInfo.ReportTitle = "[DUPLIKAT]"

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterName
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Letter" Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next
                    '   export_inv("", "PDF", "Letter", Application.StartupPath & "\" & My.Computer.Name, "T")

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

                    CrDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, lokasi_file & "\" & nama_file & ".pdf")

                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CetakAlfamartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakAlfamartToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        cetakAlfamart()
    End Sub

    Private Sub ExportAlfamartKePDFToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportAlfamartKePDFToolStripMenuItem.Click
        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""
            Dim lokasi_file As String = Application.StartupPath & "\" & My.Computer.Name

            If System.IO.Directory.Exists(lokasi_file) = False Then
                System.IO.Directory.CreateDirectory(lokasi_file)
            End If

            Dim format_akhir As String = Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "ddMMMyyyyHHmmss")


            Dim nama_file As String = Replace(ListView1.FocusedItem.SubItems(1).Text, "/", "") & "_" & format_akhir

            SQL = "select kode_perusahaan, lokasi from rekap_sub_invoice where kode_perusahaan = '" & KodePerusahaan & "' and no_do = '" & ListView1.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    If Strings.Left(Ds.Tables("MyTable").Rows(0).Item("lokasi"), 4) = "ALFA" Then
                        'komen keempat
                        CrDoc = New Faktur_Sub_Invoice_Alfamart

                        kertas = "Faktur"

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = PrinterName
                        CrDoc.RecordSelectionFormula = "{rekap_sub_invoice.Kode_Perusahaan} = '" & KodePerusahaan & "' and {rekap_sub_invoice.no_do} = '" & ListView1.FocusedItem.Text & "'"
                        'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterName
                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Letter" Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next
                        '   export_inv("", "PDF", "Letter", Application.StartupPath & "\" & My.Computer.Name, "T")

                        CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

                        CrDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, lokasi_file & "\" & nama_file & ".pdf")
                    End If
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class