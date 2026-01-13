Public Class N_EMI_Display_Pemusnahan_Barang

    Dim arrLokasi, arrTanggal, arrLain As New ArrayList

    Private Sub N_EMI_Display_Pemusnahan_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
        Lv_Data.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Jumlah Bags", 100, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("User ID", 110, HorizontalAlignment.Center)
        Lv_Data.View = View.Details

        Lv_Detail.Columns.Clear() : Lv_Detail.Items.Clear()
        Lv_Detail.Columns.Add("Barcode Awal", 240, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Barcode Akhir", 240, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Jumlah Bags", 130, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_Detail.View = View.Details

        kosong()

    End Sub

    Private Sub kosong()

        Try
            OpenConn()

            Tgl1.Value = Date.Now : Tgl2.Value = Date.Now

            Cmb_Lokasi.Items.Clear() : arrLokasi.Clear()
            Cmb_Lokasi.Items.Add(OpsiSeluruh) : arrLokasi.Add(OpsiSeluruh)
            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner")) : arrLokasi.Add(dr("kode_stock_owner"))
                Loop
            End Using
            Cmb_Lokasi.SelectedIndex = 0

            Cmb_Tanggal.Items.Clear() : arrTanggal.Clear()
            Cmb_Tanggal.Items.Add("Tanggal") : arrTanggal.Add("a.Tanggal")

            Cmb_Lain.Items.Clear() : arrLain.Clear()
            Cmb_Lain.Items.Add("UserID") : arrLain.Add("a.UserID")
            Cmb_Lain.Items.Add("Kode Barang") : arrLain.Add("b.Kode_Barang")
            Cmb_Lain.Items.Add("Keterangan") : arrLain.Add("a.keterangan")

            Txt_ValueLain.Text = ""

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        LoadData()
        Chk_HariIni.Focus()

    End Sub

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Not Chk_HariIni.Checked And Not Chk_Tanggal.Checked And Not Chk_Lain.Checked Then
            MessageBox.Show("Filter Harus Dipilih Minimal 1 Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Chk_HariIni.Focus()
            Exit Sub
        End If

        If Chk_Tanggal.Checked Then
            If Cmb_Tanggal.SelectedIndex = -1 Then
                MessageBox.Show("Tanggal Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Tanggal.DroppedDown = True : Cmb_Tanggal.Focus() : Exit Sub
            ElseIf Tgl1.Value > Tgl2.Value Then
                MessageBox.Show("Periode I " & Base_Language.Lang_Global_TidakBolehLebihDari & " periode II!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
                Exit Sub
            End If
        End If

        If Chk_Lain.Checked Then
            If Cmb_Lain.SelectedIndex = -1 Then
                MessageBox.Show("FIlter Lain Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Lain.DroppedDown = True : Cmb_Lain.Focus() : Exit Sub
            ElseIf Txt_ValueLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_ValueLain.Focus() : Exit Sub
            End If
        End If

        LoadData()

    End Sub

    Private Sub LoadData()
        Try
            OpenConn()

            Lv_Data.Items.Clear() : Lv_Detail.Items.Clear()
            SQL = "select a.No_Faktur, a.Tanggal, a.Jam, a.Status, a.UserID, a.Keterangan, a.Flag_Validasi, "
            SQL = SQL & "b.Kode_Barang, c.Nama as Nama_Barang, b.Total, b.Total_Bags, b.Satuan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            If Not Cmb_Lokasi.SelectedIndex = 0 Then
                SQL = SQL & "and a.Lokasi = '" & arrLokasi(Cmb_Lokasi.SelectedIndex) & "' "
            End If

            If Chk_HariIni.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now.AddDays(1), "yyyy-MM-dd") & "' "
            End If

            If Chk_Tanggal.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrTanggal.Item(Cmb_Tanggal.SelectedIndex) & " between '"
                SQL = SQL & Format(Tgl1.Value, "yyyy-MM-dd") & "' AND '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            End If

            If Chk_Lain.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrLain.Item(Cmb_Lain.SelectedIndex) & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
            End If

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Total"), "N2"))
                    Lv.SubItems.Add(Dr("Total_Bags"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("UserID"))

                    If General_Class.CekNULL(Dr("Flag_Validasi")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
                    End If

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

    Private Sub Lv_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data.SelectedIndexChanged
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Data.FocusedItem.Text

            Lv_Detail.Items.Clear()
            SQL = "select (e.Qr_Code + '-' + e.Kode_Unik_Berjalan) as Barcode_Awal, "
            SQL = SQL & "(f.Qr_Code + '-' + f.Kode_Unik_Berjalan) as Barcode_Akhir, "
            SQL = SQL & "dbo.ubah_satuan(a.Kode_Perusahaan, 'masa', b.Kode_Barang, b.Satuan_Barang, b.Satuan, d.Jumlah) as Jumlah, "
            SQL = SQL & "d.Jumlah_Bags, b.Satuan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c, N_EMI_Transaksi_Transfer_Waste_Det2 d, Barang_SN e, Barang_SN f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and c.Kode_Perusahaan = e.Kode_Perusahaan and d.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            SQL = SQL & "and a.Kode_Stock_Owner = e.Kode_Stock_Owner and b.kode_barang = e.Kode_Barang and c.Serial_Number_Awal = e.Serial_Number "
            SQL = SQL & "and a.Kode_Stock_Owner = f.Kode_Stock_Owner and b.kode_barang = f.Kode_Barang and d.Serial_Number = f.Serial_Number "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("Barcode_Awal"))
                    Lv.SubItems.Add(Dr("Barcode_Akhir"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Dr("Jumlah_Bags"))
                    Lv.SubItems.Add(Dr("Satuan"))
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
            BtnBarangMasuk_Cari_Click(Chk_HariIni, e)
        End If
    End Sub

    Private Sub Chk_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Tanggal.CheckedChanged
        If Chk_Tanggal.Checked Then
            Cmb_Tanggal.Enabled = True : Tgl1.Enabled = True : Tgl2.Enabled = True
            Chk_HariIni.Checked = False
        Else
            Cmb_Tanggal.Enabled = False : Tgl1.Enabled = False : Tgl2.Enabled = False
            Cmb_Tanggal.SelectedIndex = -1 : Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
        End If
    End Sub

    Private Sub Chk_Lain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Lain.CheckedChanged
        If Chk_Lain.Checked Then
            Cmb_Lain.Enabled = True : Txt_ValueLain.Enabled = True
        Else
            Cmb_Lain.Enabled = False : Txt_ValueLain.Enabled = False
            Cmb_Lain.SelectedIndex = -1 : Txt_ValueLain.Text = ""
        End If
    End Sub

    Private Sub Cmb_Tanggal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Tanggal.SelectedIndexChanged

        If Not Cmb_Tanggal.SelectedIndex = -1 Then
            Tgl1.Enabled = True : Tgl2.Enabled = True
        Else
            Tgl1.Enabled = False : Tgl2.Enabled = False
        End If
        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

    End Sub

    Private Sub Cmb_Lain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lain.SelectedIndexChanged
        If Not Cmb_Lain.SelectedIndex = -1 Then
            Txt_ValueLain.Enabled = True
        Else
            Txt_ValueLain.Enabled = False
        End If
        Txt_ValueLain.Text = ""
    End Sub

    Private Sub SalinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Data.FocusedItem.Text)
    End Sub


    Private Sub CetakFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakFakturToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang akan di Cetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        Dim NoTransaksi As String = Lv_Data.FocusedItem.Text

        Try
            OpenConn()


            '==============================================
            '=     CEK APAKAH FAKTUR SUDAH DIBATALKAN     =
            '==============================================
            SQL = "select status from N_EMI_Transaksi_Transfer_Waste "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoTransaksi & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena No Faktur sudah dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    CloseConn()
                    MessageBox.Show("No Faktur tidak ditemukan, Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            Dim isPrinted As Boolean = False
            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from N_EMI_View_Transfer_Waste a where "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & Trim(NoTransaksi) & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New N_EMI_CR_Faktur_Transaksi_Pemusnahan_Barang
                    kertas = "Faktur"

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{N_EMI_View_Transfer_Waste.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Transfer_Waste.No_Faktur}='" & Trim(NoTransaksi) & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "Faktur Pengajuan Pemusnahan Barang"
                    '    .Text = "Faktur Pengajuan Pemusnahan Barang"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '============================================================================================================================================
                    '============================================================================================================================================
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterNameTS
                    CrDoc.RecordSelectionFormula = "{N_EMI_View_Transfer_Waste.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Transfer_Waste.No_Faktur}='" & Trim(NoTransaksi) & "' "
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterNameTS
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

                    isPrinted = True

                Else
                    CloseConn()
                    MessageBox.Show($"No Faktur {Trim(NoTransaksi)} tidak ada didalam View cetak ulang. Harap hubungi tim IT")
                    Exit Sub

                End If
            End Using

            If isPrinted Then
                MessageBox.Show("Faktur Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Faktur Gagal Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub





    '===============================================================================================================================================
    '=     HANDLE KEYPRESS
    '===============================================================================================================================================
    Private Sub Chk_HariIni_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_HariIni.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_HariIni.Checked Then
                Btn_Cari.Focus()
            Else
                Chk_Tanggal.Focus()
            End If
        End If
    End Sub

    Private Sub Chk_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_Tanggal.Checked Then
                Cmb_Tanggal.DroppedDown = True
                Cmb_Tanggal.Focus()
            Else
                Chk_Lain.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then
            Tgl1.Enabled = True : Tgl2.Enabled = True
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus()
        End If
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Chk_Lain.Focus()
        End If
    End Sub

    Private Sub Chk_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Lain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_Lain.Checked Then
                Cmb_Lain.DroppedDown = True
                Cmb_Lain.Focus()
            Else
                Btn_Cari.Focus()
            End If
        End If
    End Sub


    Private Sub Cmb_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Lain.SelectedIndex <> -1 Then
                Txt_ValueLain.Enabled = True
                Txt_ValueLain.Text = ""
                Txt_ValueLain.Focus()

            End If
        End If
    End Sub

    Private Sub Txt_ValueLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ValueLain.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

End Class