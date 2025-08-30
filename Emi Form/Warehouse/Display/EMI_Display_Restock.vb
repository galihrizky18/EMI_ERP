Public Class EMI_Display_Restock

    Dim arrFilter, arrTanggal As New ArrayList

    Private Sub EMI_Display_Restock_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Initial_Lv()
        Kosong()

    End Sub

    Private Sub Kosong()

        Lv_Data.Items.Clear()

        Txt_Filter_Value.Text = String.Empty

        Cmb_Tanggal.Items.Clear() : arrTanggal.Clear()
        Cmb_Tanggal.Items.Add("Tanggal Produksi") : arrTanggal.Add("a.tgl_produksi")
        Cmb_Tanggal.Items.Add("Tanggal Expired") : arrTanggal.Add("a.tgl_expired")

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("a.Kode_Barang")
        Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("b.Nama")
        Cmb_Filter.Items.Add("Lokasi Gudang") : arrFilter.Add("a.Kode_Stock_Owner")

        'Cmb_Filter.Items.Add("Tanggal") : arrFilter.Add("a.Tanggal")
        'Cmb_Filter.Items.Add("Tanggal Produksi") : arrFilter.Add("a.tgl_produksi")
        'Cmb_Filter.Items.Add("Tanggal Expired") : arrFilter.Add("a.tgl_expired")

        CheckBox1.Checked = False : CheckBox2.Checked = False : CheckBox3.Checked = False
        Cmb_Tanggal.SelectedIndex = -1 : Cmb_Filter.SelectedIndex = -1
        Txt_Filter_Value.Text = ""

        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        Cmb_Filter.Enabled = False : Txt_Filter_Value.Enabled = False

        Button1_Click(Lv_Data, New EventArgs)

        CheckBox1.Focus()

    End Sub

    Private Sub Initial_Lv()
        Lv_Data.Columns.Clear()

        Lv_Data.Columns.Add("No Faktur", 165, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tanggal", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode SO", 165, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Nama Barang", 370, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah", 140, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Harga Beli", 140, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Keterangan", 350, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal Produksi", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tanggal Expired", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("User Input", 110, HorizontalAlignment.Center)

        Lv_Data.Columns.Add("SN", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Satuan Kecil", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("UserID", 0, HorizontalAlignment.Center)
        Lv_Data.View = View.Details

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
        '    MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
        '    CheckBox1.Focus() : Exit Sub
        'End If

        If CheckBox2.Checked = True Then
            If Cmb_Tanggal.SelectedIndex = -1 Then
                MessageBox.Show("Jenis Tanggal Harus Diisi Dahulu!", Judul)
                Cmb_Tanggal.DroppedDown = True
                Cmb_Tanggal.Focus()
                Exit Sub
            End If
            If DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                Exit Sub
            End If
        End If

        If CheckBox3.Checked = True Then
            If Cmb_Filter.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                Cmb_Filter.DroppedDown = True : Cmb_Filter.Focus() : Exit Sub
            ElseIf Txt_Filter_Value.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                Txt_Filter_Value.Focus() : Exit Sub
            End If

        End If

        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.Satuan_Barang, "
            SQL = SQL & "a.Keterangan, a.serial_number, a.UserID, c.UserName, a.harga_beli, a.tgl_produksi, a.tgl_expired, a.no_urut, a.Status "
            SQL = SQL & "from EMI_Restock_Barang a, Barang b, users c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.UserID = c.UserID "

            If CheckBox1.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal Between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrTanggal(Cmb_Tanggal.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilter.Item(Cmb_Filter.SelectedIndex) & " like '%" & Trim(Txt_Filter_Value.Text) & "%' "
            End If

            SQL = SQL & "order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As New ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Format(Dr("harga_beli"), "N2"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("tgl_produksi"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Format(Dr("tgl_expired"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("UserName"))

                    'Hidden
                    Lv.SubItems.Add(Dr("serial_number"))
                    Lv.SubItems.Add(Dr("Satuan_Barang"))
                    Lv.SubItems.Add(Dr("UserID"))

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Kosong()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            Button1_Click(CheckBox1, e)
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            Cmb_Tanggal.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            CheckBox1.Checked = False
            Cmb_Tanggal.DroppedDown = True
        Else
            Cmb_Tanggal.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_Tanggal.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            Cmb_Filter.Enabled = True : Txt_Filter_Value.Enabled = True
            Cmb_Filter.DroppedDown = True
        Else
            Cmb_Filter.Enabled = False : Txt_Filter_Value.Enabled = False
            Cmb_Filter.SelectedIndex = -1 : Txt_Filter_Value.Text = ""
        End If
    End Sub

    '=========================================================================================
    '=     MENU STRIP
    '=========================================================================================
    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Data.FocusedItem.Text)
    End Sub

    Private Sub CetakRestockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakRestockToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim NoFaktur As String = Lv_Data.FocusedItem.Text

            '==============================================
            '=     CEK APAKAH NO TRANSAKSI DIBATALKAN     =
            '==============================================
            SQL = "select Kode_Perusahaan from EMI_Restock_Barang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoFaktur & "' and Status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Cetak Ulang Tidak dapat Dilakukan Karena No Faktur Restock Telah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=======================
            '=     CETAK ULANG     =
            '=======================
            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select Kode_Perusahaan from view_laporan_penambahan_stock_barang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & NoFaktur & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New Rpt_Laporan_Penambahan_Stock_Barang
                    kertas = "Faktur"

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{view_laporan_penambahan_stock_barang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {view_laporan_penambahan_stock_barang.no_faktur}='" & NoFaktur & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "TF"
                    '    .Text = "TF"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '============================================================================================================================================
                    '============================================================================================================================================
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterNameTS
                    CrDoc.RecordSelectionFormula = "{view_laporan_penambahan_stock_barang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {view_laporan_penambahan_stock_barang.no_faktur}='" & NoFaktur & "' "
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterNameTS
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
                    MessageBox.Show("Cetak Ulang Tidak dapat Dilakukan Karena No Faktur Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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



    Private Sub BatalRestockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalRestockToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Register Kendaraan"
            Dim NoFaktur As String = Lv_Data.FocusedItem.Text

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Restock") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Restock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Faktur Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            '=========================================
            '=     CEK APAKAH RESTOCK DIBATALKAN     =
            '=========================================
            SQL = "select Kode_Perusahaan from EMI_Restock_Barang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoFaktur & "' and status = 'Y'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Restock Tidak dapat Dilakukan karena No Faktur ini Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '========================================
            '=     CEK APAKAH SUDAH TUTUP SALDO     =
            '========================================
            Dim HasData As Boolean = False
            Dim TglRestock As DateTime
            SQL = "select Tanggal from EMI_Restock_Barang where status is null and Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    HasData = True
                    TglRestock = Dr("Tanggal")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Restock Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If HasData Then
                If CekSudahTutupSaldo(TglRestock) = "Y" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Restock tidak dapat dilakukan karena No Faktur Sudah Tutup Saldo", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If

            '========================================
            '=     CEK APAKAH SUDAH LEWAT BULAN     =
            '========================================
            If Not tgl_skg.Month = TglRestock.Month Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Pembatalan Restock tidak dapat dilakukan karena Sudah Melewati Bulan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '==============================================
            '=     CEK APAKAH BARANG SUDAH DIGUNAAKAN     =
            '==============================================
            SQL = "select a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Barang, a.Jumlah, "
            SQL = SQL & "isnull(( select dbo.ubah_satuan(z.kode_perusahaan, 'masa',a.kode_barang, a.satuan_barang, a.satuan, sum(z.Jumlah)) "
            SQL = SQL & "from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Serial_Number = z.Serial_Number "
            SQL = SQL & "group by z.Kode_Perusahaan ), 0) as Jumlah_SN "
            SQL = SQL & "from EMI_Restock_Barang a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_Faktur = '" & NoFaktur & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If Val(.Rows(0).Item("Jumlah")) <> Val(.Rows(0).Item("Jumlah_SN")) Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pembatalan Restock tidak dapat dilakukan karena Barang Sudah Digunakan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pembatalan Restock tidak dapat dilakukan karena Data Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '=========================
            '=     ROLLBACK DATA     =
            '=========================
            Dim KdStockOwner As String = ""
            Dim KdBarang As String = ""
            Dim SN As String = ""
            Dim Jumlah, Jumlah_Kecil As Double
            Dim Satuan As String = ""
            Dim Satuan_Kecil As String = ""
            Dim Voucher As String = ""

            SQL = "select a.no_Faktur, a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, a.Jumlah, a.Satuan, a.Satuan_Barang, Kode_Voucher, "
            SQL = SQL & "(dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, a.satuan, a.Satuan_Barang, a.Jumlah)) as Jumlah_Kecil "
            SQL = SQL & "from EMI_Restock_Barang a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoFaktur & "' "
            SQL = SQL & "and a.status is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            KdStockOwner = .Rows(i).Item("Kode_Stock_Owner")
                            KdBarang = .Rows(i).Item("Kode_Barang")
                            SN = .Rows(i).Item("Serial_Number")
                            Jumlah = .Rows(i).Item("Jumlah")
                            Jumlah_Kecil = .Rows(i).Item("Jumlah_Kecil")
                            Satuan = .Rows(i).Item("Satuan")
                            Satuan_Kecil = .Rows(i).Item("Satuan_Barang")
                            Voucher = .Rows(i).Item("Kode_Voucher")

                            '=================================
                            '=     CEK KESEIMBANGAN DATA     =
                            '=================================
                            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdStockOwner & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan, Data Tidak Seimbang . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '============================
                            '=     UPDATE BARANG_SN     =
                            '============================
                            SQL = "select Kode_Stock_Owner, Kode_Barang, Jumlah, Warna "
                            SQL = SQL & "from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    'CEK WARNA
                                    If General_Class.CekNULL(Ds1.Tables("MyTable").Rows(0).Item("Warna")).ToString.ToUpper <> "HIJAU" Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Pembatalan Restock Tidak Dapat Dilakukan karena Kualitas Barang tidak Sesuai", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    If Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah"))) < Jumlah_Kecil Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback, Stock akan Menjadi Negatif", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else
                                        SQL = "update Barang_SN set Jumlah = Jumlah - " & Jumlah_Kecil & " "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN & "' "
                                        ExecuteTrans(SQL)
                                    End If
                                Else

                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Pembatalan Restock Tidak Dapat Dilakukan karena Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '=========================
                            '=     UPDATE BARANG     =
                            '=========================
                            SQL = "select Good_Stock from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner = '" & KdStockOwner & "' and Kode_Barang = '" & KdBarang & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    If Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Good_Stock"))) < Jumlah_Kecil Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback, Stock akan Menjadi Negatif", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else
                                        SQL = "update barang set Good_Stock = Good_Stock - " & Jumlah_Kecil & " "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdStockOwner & "' and Kode_Barang = '" & KdBarang & "' "
                                        ExecuteTrans(SQL)
                                    End If


                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Pembatalan Restock Tidak Dapat Dilakukan karena Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                        Next
                    End If
                End With
            End Using

            '==================================
            '=     CEK KESEIMBANGAN STOCK     =
            '==================================
            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdStockOwner & "' "
            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
            Using Ds1 = BindingTrans(SQL)
                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                    If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Data Tidak Sesuai . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================
            '=     UPDATE JURNAL     =
            '=========================
            SQL = "SELECT Kode_Perusahaan FROM Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & Voucher & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        SQL = "delete Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & Voucher & "' "
                        ExecuteTrans(SQL)
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pembatalan Restock Tidak Dapat Dilakukan karena Jurnal Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '===============================
            '=     UPDATE DATA RESTOCK     =
            '===============================
            SQL = "select Kode_Perusahaan from EMI_Restock_Barang "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null "
            SQL = SQL & "and No_Faktur = '" & NoFaktur & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        SQL = "update EMI_Restock_Barang set Status = 'Y', UserID_Batal = '" & UserID & "', "
                        SQL = SQL & "Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null "
                        SQL = SQL & "and No_Faktur = '" & NoFaktur & "' "
                        ExecuteTrans(SQL)

                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Restock Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Button1_Click(e, New EventArgs)

    End Sub




    '=========================================================================
    '=     HANDLE KEY PRESS
    '=========================================================================
    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox2.Focus()
    End Sub

    Private Sub CheckBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox2.Checked Then
                Cmb_Filter.DroppedDown = True
                Cmb_Filter.Focus()
            Else
                CheckBox3.Focus()
            End If
        End If
    End Sub
    Private Sub Cmb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub
    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub
    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox3.Focus()
    End Sub

    Private Sub CheckBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox3.Checked Then
                Cmb_Filter.DroppedDown = True
                Cmb_Filter.Focus()
            Else
                Button1.Focus()
            End If
        End If
    End Sub
    Private Sub Cmb_Filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Filter_Value.Focus()
    End Sub
    Private Sub Txt_Filter_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Filter_Value.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub









End Class