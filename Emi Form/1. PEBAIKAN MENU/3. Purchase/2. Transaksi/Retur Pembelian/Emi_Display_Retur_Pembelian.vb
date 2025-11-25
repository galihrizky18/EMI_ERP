Public Class Emi_Display_Retur_Pembelian

    Dim arrLokasi, arrTanggal, arrParamLain As New ArrayList

    Private Sub Emi_Display_Retur_Pembelian_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Display_Retur_Pembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")



        Kosong()
    End Sub

    Private Sub Kosong()

        'TODO : Kosong

        Lv_Retur.Columns.Clear()
        Lv_Retur.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left) '0
        Lv_Retur.Columns.Add("No Pembelian", 150, HorizontalAlignment.Left) '1
        Lv_Retur.Columns.Add("No PO", 150, HorizontalAlignment.Left) '2
        Lv_Retur.Columns.Add("Lokasi", 150, HorizontalAlignment.Center) '3
        Lv_Retur.Columns.Add("Keterangan", 250, HorizontalAlignment.Left) '4
        Lv_Retur.Columns.Add("Tanggal Pembelian", 130, HorizontalAlignment.Center) '5
        Lv_Retur.Columns.Add("Jam Pembelian", 110, HorizontalAlignment.Center) '6
        Lv_Retur.Columns.Add("Tanggal Retur", 130, HorizontalAlignment.Center) '7
        Lv_Retur.Columns.Add("Jam Retur", 110, HorizontalAlignment.Center) '8
        Lv_Retur.Columns.Add("User", 130, HorizontalAlignment.Center) '9
        Lv_Retur.View = View.Details

        Lv_Retur_Detail.Columns.Clear()
        Lv_Retur_Detail.Columns.Add("No Loading", 150, HorizontalAlignment.Left) '0
        Lv_Retur_Detail.Columns.Add("Mobil", 300, HorizontalAlignment.Left) '1
        Lv_Retur_Detail.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '`2
        Lv_Retur_Detail.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left) '`3
        Lv_Retur_Detail.Columns.Add("Total", 150, HorizontalAlignment.Right) '4
        Lv_Retur_Detail.Columns.Add("Total Bags", 150, HorizontalAlignment.Right) '5
        Lv_Retur_Detail.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '6
        'HIDe
        Lv_Retur_Detail.Columns.Add("urut", 0, HorizontalAlignment.Center) '7
        Lv_Retur_Detail.View = View.Details

        Lv_Retur_Mobil.Columns.Clear()
        Lv_Retur_Mobil.Columns.Add("Rak", 150, HorizontalAlignment.Left) '0
        Lv_Retur_Mobil.Columns.Add("Barcode", 300, HorizontalAlignment.Left) '1
        Lv_Retur_Mobil.Columns.Add("Jumlah", 150, HorizontalAlignment.Right) '2
        Lv_Retur_Mobil.Columns.Add("Jumlah Bags", 150, HorizontalAlignment.Right) '3
        Lv_Retur_Mobil.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '4
        Lv_Retur_Mobil.Columns.Add("Tanggal Produksi", 120, HorizontalAlignment.Center) '5
        Lv_Retur_Mobil.Columns.Add("Tanggal Expired", 120, HorizontalAlignment.Center) '6
        Lv_Retur_Mobil.Columns.Add("Kualitas", 120, HorizontalAlignment.Center) '7
        Lv_Retur_Mobil.View = View.Details

        Try
            OpenConn()

            '============================
            '=     LOAD DATA FILTER     =
            '============================
            Cmb1.Items.Clear() : arrLokasi.Clear()
            SQL = "select kode_stock_owner, keterangan from Stock_Owner "
            Using Dr = OpenTrans(SQL)
                Cmb1.Items.Add("--- SELURUH ---") : arrLokasi.Add("--- SELURUH ---")
                Do While Dr.Read
                    Cmb1.Items.Add(Dr("keterangan")) : arrLokasi.Add(Dr("kode_stock_owner"))
                Loop
                Cmb1.SelectedIndex = 0
            End Using

            Cmb_Tanggal.Items.Clear() : arrTanggal.Clear() : DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
            Cmb_Tanggal.Items.Add("Tanggal Pembelian") : arrTanggal.Add("b.Tanggal")
            Cmb_Tanggal.Items.Add("Tanggal Retur") : arrTanggal.Add("a.Tanggal")

            Cmb_ParamLain.Items.Clear() : arrParamLain.Clear() : Txt_ParamLain.Text = ""
            Cmb_ParamLain.Items.Add("No Transaksi") : arrParamLain.Add("a.No_Transaksi")
            Cmb_ParamLain.Items.Add("No Pembelian") : arrParamLain.Add("a.No_Pembelian")
            Cmb_ParamLain.Items.Add("No PO") : arrParamLain.Add("b.No_PO")
            Cmb_ParamLain.Items.Add("User ID") : arrParamLain.Add("a.UserID")
            Cmb_ParamLain.Items.Add("Keterangan") : arrParamLain.Add("a.Keterangan")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'Btn_Cari_Click(Btn_Cari, New EventArgs)

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Chk_HariIni.Checked = False And Chk_Tanggal.Checked = False And Chk_ParamLain.Checked = False And Chk_ParamLain.Checked = False Then
            MessageBox.Show("Checkbox harus centang salah satu", Judul)
            Chk_HariIni.Focus() : Exit Sub
        End If

        If Chk_Tanggal.Checked Then
            If Cmb_Tanggal.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Tanggal Harus Dipilih", Judul)
                Cmb_Tanggal.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I Tidak Boleh Lebih Dari periode II!", Judul)
                DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                Exit Sub
            End If
        End If
        If Chk_ParamLain.Checked Then
            If Cmb_ParamLain.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Lain Harus Dipilih", Judul)
                Cmb_ParamLain.Focus() : Exit Sub
            ElseIf Txt_ParamLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Parameter Lain Tidak Boleh Kosong", Judul)
                Txt_ParamLain.Focus() : Exit Sub
            End If
        End If



        Try
            OpenConn()

            Lv_Retur.Items.Clear() : Lv_Retur_Detail.Items.Clear() : Lv_Retur_Mobil.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.No_Transaksi, b.No_PO, a.kode_stock_owner, a.No_Pembelian, a.Tanggal, a.Jam, "
            SQL = SQL & "b.Tanggal as Tgl_Pembelian, b.Jam as Jam_Pembelian, a.UserID, a.Keterangan "
            SQL = SQL & "from Emi_Retur_Pembelian a, EMI_Pembelian b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Pembelian = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.status is null and b.status is null "

            If Not Cmb1.SelectedIndex = 0 Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.kode_stock_owner = '" & arrLokasi(Cmb1.SelectedIndex) & "' "
            End If

            If Chk_HariIni.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal Between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(DateAdd(DateInterval.Day, 1, Now), "yyyy-MM-dd") & "' "

            End If

            If Chk_Tanggal.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrTanggal.Item(Cmb_Tanggal.SelectedIndex) + " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If Chk_ParamLain.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrParamLain.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
            End If

            SQL = SQL & "order by a.No_Transaksi, a.tanggal, a.Jam"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Retur.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("No_Pembelian"))
                    Lv.SubItems.Add(Dr("No_PO"))
                    Lv.SubItems.Add(Dr("kode_stock_owner"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tgl_Pembelian"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam_Pembelian"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("UserID"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_Retur_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Retur.SelectedIndexChanged

        If Lv_Retur.Items.Count = 0 OrElse Lv_Retur.FocusedItem.Index = -1 OrElse Lv_Retur.FocusedItem.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Dim NoTransaksi As String = Lv_Retur.FocusedItem.Text

            Lv_Retur_Detail.Items.Clear() : Lv_Retur_Mobil.Items.Clear()

            SQL = "select a.No_Transaksi, b.No_Loading, (c.No_SJ + ' - ' + c.No_Plat + ' - ' + c.Driver) as Mobil,  "
            SQL = SQL & "b.Kode_Barang, d.Nama as Nama_Barang, b.Total, b.Total_Barang, b.Total_Bags, b.Satuan, b.Satuan_Barang, b.Urut  "
            SQL = SQL & "from Emi_Retur_Pembelian a, Emi_Retur_Pembelian_Detail b, emi_pembelian_loading c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi  "
            SQL = SQL & "and b.No_Loading = c.No_Faktur "
            SQL = SQL & "and b.Lokasi = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and a.No_Transaksi = '" & NoTransaksi & "'  "
            SQL = SQL & "and a.Status is null and c.Status is null "
            SQL = SQL & "order by a.No_Transaksi, b.No_Loading, b.Lokasi, b.Urut  "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Retur_Detail.Items.Add(Dr("No_Loading"))
                    Lv.SubItems.Add(Dr("Mobil"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Total"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Total_Bags"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Urut"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Lv_Retur_Detail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Retur_Detail.SelectedIndexChanged
        If Lv_Retur_Detail.Items.Count = 0 OrElse Lv_Retur_Detail.FocusedItem.Index = -1 OrElse Lv_Retur_Detail.FocusedItem.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Dim NoTransaksi As String = Lv_Retur.FocusedItem.Text
            Dim NoLoading As String = Lv_Retur_Detail.FocusedItem.Text
            Dim Urut As String = Lv_Retur_Detail.FocusedItem.SubItems(7).Text

            Lv_Retur_Mobil.Items.Clear()

            SQL = "select b.Id_Wms, c.Keterangan as Rak, (h.Qr_Code + '-' + h.Kode_Unik_Berjalan) as Barcode, b.Jumlah, b.Jumlah_Bags, a.Satuan, g.Tanggal_Produksi, g.Tanggal_Expired, b.Warna, d.Keterangan as Kualitas  "
            SQL = SQL & "from Emi_Retur_Pembelian_Detail a, Emi_Retur_Pembelian_Det b, View_Warehouse_Position c, EMI_Master_Warna d, barang e, emi_pembelian_loading f, emi_pembelian_loading_detail g, barang_sn h "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan  "
            SQL = SQL & "and a.Kode_Perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and b.Kode_Perusahaan = h.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and a.Urut = b.Urut_Retur_Pembelian  "
            SQL = SQL & "and b.Id_Wms = c.Id_WMS_Warehouse_Position  "
            SQL = SQL & "and b.Warna = d.Kode_Warna "
            SQL = SQL & "and a.lokasi = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and a.No_Loading = f.No_Faktur "
            SQL = SQL & "and f.No_Faktur = g.No_Faktur "
            SQL = SQL & "and b.Serial_Number = h.Serial_Number and a.Lokasi = h.Kode_Stock_Owner and a.Kode_Barang = h.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Transaksi = '" & NoTransaksi & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            SQL = SQL & "and b.Urut = '" & Urut & "' "
            SQL = SQL & "order by g.No_PO, a.Kode_Barang, b.Id_Wms, b.Warna "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Retur_Mobil.Items.Add(Dr("Rak"))
                    Lv.SubItems.Add(Dr("Barcode"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal_Produksi"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Format(Dr("Tanggal_Expired"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kualitas"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Chk_HariIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_HariIni.CheckedChanged
        If Chk_HariIni.Checked = True Then
            Chk_Tanggal.Checked = False
            Btn_Cari_Click(Chk_Tanggal, e)
        End If
    End Sub



    Private Sub Chk_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Tanggal.CheckedChanged

        If Chk_Tanggal.Checked Then
            Cmb_Tanggal.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Chk_HariIni.Checked = False
        Else
            Cmb_Tanggal.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_Tanggal.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub



    Private Sub Chk_ParamLain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamLain.CheckedChanged
        If Chk_ParamLain.Checked = True Then
            Cmb_ParamLain.Enabled = True : Txt_ParamLain.Enabled = True
        Else
            Cmb_ParamLain.Enabled = False : Txt_ParamLain.Enabled = False
            Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamLain.Text = ""
        End If
    End Sub


    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If Lv_Retur.Items.Count = 0 Or Lv_Retur.SelectedItems.Count = 0 Or Lv_Retur.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Retur.FocusedItem.Text)
    End Sub


    Private Sub CetakUlangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangToolStripMenuItem.Click
        If Lv_Retur.Items.Count = 0 Or Lv_Retur.SelectedItems.Count = 0 Or Lv_Retur.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang akan di Cetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim NoTransaksi As String = Lv_Retur.FocusedItem.Text
        'Dim NoTransaksi As String = "RPDS-09/25-000012"

        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select kode_perusahaan from N_EMI_View_Transaksi_Retur_Pembelian "
            SQL = SQL & "where kode_perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and no_transaksi = '" & NoTransaksi & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New N_EMI_CR_Transaksi_Retur_Pembelian
                    kertas = "Faktur"


                    With A_Place_For_Printing2
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SetDataSource(Ds)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = "{N_EMI_View_Transaksi_Retur_Pembelian.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Transaksi_Retur_Pembelian.no_transaksi}='" & NoTransaksi & "' "
                        CrDoc.SummaryInfo.ReportTitle = "TF"
                        .Text = "TF"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With

                    '============================================================================================================================================
                    '============================================================================================================================================
                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.PrintOptions.PrinterName = PrinterNameTS
                    'CrDoc.RecordSelectionFormula = "{N_EMI_View_Transaksi_Retur_Pembelian.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Transaksi_Retur_Pembelian.no_transaksi}='" & Trim(TxtNo_Transaksi.Text) & "' "
                    ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterNameTS
                    ''doctoprint.DefaultPageSettings.Landscape = True
                    'Dim rawKind As Integer
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        Exit For
                    '    End If
                    'Next

                    'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    'CrDoc.PrintToPrinter(1, False, 1, 99)

                    'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


                Else
                    CloseConn()
                    MessageBox.Show("No Transaksi Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
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