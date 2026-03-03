Imports System.Web
Imports Guna.UI2.WinForms

Public Class N_EMI_SD_Procurement_Barang_Lain_Loading
    Public Property FilterKodeBarang As String

    Private searchTimer As Timer
    Private Const DEBOUNCE_DELAY As Integer = 750
    Private Const MIN_SEARCH_LENGTH As Integer = 3

    Private Sub SD_Card_DO_Outstanding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        searchTimer = New Timer With {.Interval = DEBOUNCE_DELAY}
        AddHandler searchTimer.Tick, AddressOf OnSearchTimerTick
        AddHandler TB_Search.TextChanged, AddressOf TB_Search_TextChanged
        AddHandler CB_Status.SelectedIndexChanged, AddressOf CB_Status_SelectedIndexChanged

        Fetch_DO_Outstanding()
    End Sub

    Private Sub TB_Search_TextChanged(sender As Object, e As EventArgs)
        searchTimer.Stop()
        searchTimer.Start()
    End Sub

    Private Sub OnSearchTimerTick(sender As Object, e As EventArgs)
        searchTimer.Stop()
        Fetch_DO_Outstanding()
    End Sub

    Private Sub CB_Status_SelectedIndexChanged(sender As Object, e As EventArgs)
        Fetch_DO_Outstanding()
    End Sub

    Private Sub Fetch_DO_Outstanding()
        Try
            If Guna2DataGridView1 Is Nothing OrElse Guna2DataGridView1.Columns.Count = 0 Then Return

            OpenConn()
            Guna2DataGridView1.Rows.Clear()
            Guna2DataGridView1.RowHeadersVisible = True
            Guna2DataGridView1.RowHeadersWidth = 30
            Guna2DataGridView1.AllowUserToResizeRows = False
            Guna2DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            Dim statusFilter As String = ""
            Select Case CB_Status.Text.ToUpper()
                Case "TELAT"
                    statusFilter = "AND di.Status_Group = 'Terlambat'"
                Case "ON SCHEDULE"
                    statusFilter = "AND di.Status_Group = 'On Schedule'"
                Case Else
                    statusFilter = ""
            End Select

            Dim searchText As String = TB_Search.Text.Trim().Replace("'", "''")
            Dim searchFilter As String = "
                AND (
                    di.No_Faktur LIKE '%" & searchText & "%' 
                    OR di.No_PO LIKE '%" & searchText & "%'
                    OR ISNULL(b.Nama, '') LIKE '%" & searchText & "%'
                )
            "

            If FilterKodeBarang = "" Then
                CloseConn()
                Exit Sub
            End If

            SQL = $"
               ;WITH DO_Items AS (
                    SELECT
                        do.Kode_Perusahaan,
                        do.No_Faktur,
                        do.UseriD,
                        TRY_CAST(spo.ETD_Simulasi AS DATE)     AS ETD_date,
                        TRY_CAST(spo.ETA_Simulasi AS DATE)     AS ETA_date,
                        TRY_CAST(do.Tanggal_OTW AS DATE)  AS OTW_date,
                        TRY_CAST(do.Tanggal_Masuk AS DATE) AS Masuk_date,
                        do_d.Kode_Barang,
                        do_d.Satuan,
                        do_d.Jumlah AS Qty_DO,
                        do_d.Kode_Stock_Owner,
                        do_d.Flag_Pembelian,
                        do_d.Flag_Timbang_Keluar,
                        do_d.Flag_Timbang_Masuk,
                        do_d.Flag_QC,
                        do_d.Flag_QC_Pertama,
                        do_d.Flag_Sudah_Bongkar_Android,
                        do_d.No_PO,
                        CASE
                            WHEN spo.ETA_Simulasi IS NULL
                                THEN 'Terlambat'
                            WHEN do.Tanggal_Masuk IS NOT NULL
                                 AND do.Tanggal_Masuk <= spo.ETA_Simulasi
                                THEN 'On Schedule'
                            WHEN do.Tanggal_Masuk IS NULL
                                 AND spo.ETA_Simulasi >= CAST(GETDATE() AS DATE)
                                THEN 'On Schedule'
                            ELSE 'Terlambat'
                        END AS Status_Group,
                        do.No_SJ + '-' + do.No_Plat + '-' + do.Driver AS KeteranganDO
                    FROM EMI_Pembelian_Loading_Barang_Lain do
                    JOIN EMI_Pembelian_Loading_Detail_Barang_Lain do_d
                        ON do_d.Kode_Perusahaan = do.Kode_Perusahaan
                        AND do_d.No_Faktur = do.No_Faktur
                    JOIN EMI_Pembelian_PO_Barang_Lain spo
                        ON spo.Kode_Perusahaan = do_d.Kode_Perusahaan
                        AND spo.No_Faktur = do_d.No_PO
                    WHERE
                        do.Kode_Perusahaan = '{KodePerusahaan}'
                        AND do.status IS NULL
                        AND do_d.Flag_Pembelian IS NULL
                        AND spo.Status IS NULL
                        AND do.Flag_Retur IS NULL
                ),
                Barang_Masuk_Summary AS (
                    SELECT
                        bmp.Kode_Perusahaan,
                        bmp.No_Pembelian_Loading,
                        bmp.Kode_Barang,
                        COUNT(*) AS Total_Pallet,
                        SUM(CASE WHEN bmp.Selesai = 'Y' THEN 1 ELSE 0 END) AS Pallet_Selesai,
                        CASE 
                            WHEN COUNT(*) > 0 AND COUNT(*) = SUM(CASE WHEN bmp.Selesai = 'Y' THEN 1 ELSE 0 END)
                            THEN 'Y'
                            ELSE 'N'
                        END AS Semua_Selesai
                    FROM EMI_Barang_Masuk_Perpallet_Barang_Lain bmp
                    WHERE bmp.Status IS NULL
                    GROUP BY bmp.No_Pembelian_Loading, bmp.Kode_Barang, bmp.Kode_Perusahaan
                )
                SELECT
                    di.No_Faktur AS No_DO,
                    di.No_PO,
                    di.KeteranganDO,
                    di.UseriD,
                    ISNULL(FORMAT(di.ETD_date,   'dd MMM yyyy'), '-') AS Tanggal_ETD_DO,
                    ISNULL(FORMAT(di.ETA_date,   'dd MMM yyyy'), '-') AS Tanggal_ETA_DO,
                    ISNULL(FORMAT(di.OTW_date,   'dd MMM yyyy'), '-') AS Tanggal_OTW_DO,
                    ISNULL(FORMAT(di.Masuk_date, 'dd MMM yyyy'), '-') AS Tanggal_Masuk_DO,
                    CASE
                        WHEN di.Masuk_date IS NOT NULL 
                            THEN 'Telah Tiba'
                        WHEN di.ETD_date < CAST(GETDATE() AS DATE) AND di.OTW_date IS NULL 
                            THEN 'Telat Kirim ' + CAST(DATEDIFF(DAY, di.ETD_date, CAST(GETDATE() AS DATE)) AS VARCHAR) + ' hari'
                        WHEN di.ETA_date < CAST(GETDATE() AS DATE) 
                             AND di.Masuk_date IS NULL 
                             AND di.OTW_date IS NOT NULL
                            THEN 'Telat Datang ' + CAST(DATEDIFF(DAY, di.ETA_date, CAST(GETDATE() AS DATE)) AS VARCHAR) + ' hari'
                        WHEN di.OTW_date IS NOT NULL 
                             AND di.Masuk_date IS NULL 
                             AND di.ETA_date >= CAST(GETDATE() AS DATE)
                            THEN 'OTW - ' + CAST(DATEDIFF(DAY, CAST(GETDATE() AS DATE), di.ETA_date) AS VARCHAR) + ' hari lagi tiba'
                        WHEN di.OTW_date IS NULL 
                             AND di.ETD_date >= CAST(GETDATE() AS DATE)
                            THEN 'Belum Kirim - ' + CAST(DATEDIFF(DAY, CAST(GETDATE() AS DATE), di.ETD_date) AS VARCHAR) + ' hari lagi'
                        ELSE '-'
                    END AS Status_Pengiriman,
                    di.Status_Group,
                    CASE 
                        WHEN bms.Semua_Selesai = 'Y' THEN 'Validasi'
                        WHEN di.Flag_Sudah_Bongkar_Android = 'Y' THEN 'Proses BM'
                        ELSE '-'
                    END AS Status_Bongkar_DO,
                    CASE WHEN di.Flag_Pembelian = 'Y' THEN 'Selesai' ELSE 'Outstanding' END AS Status_Pembelian_DO,
                    di.Kode_Barang,
                    di.Satuan,
                    b.Nama AS Nama_Barang,
                    di.Qty_DO
                FROM DO_Items di
                LEFT JOIN Barang_Lain b
                    ON b.Kode_Perusahaan = di.Kode_Perusahaan
                    AND b.Kode_Barang = di.Kode_Barang
                    AND b.Kode_Stock_Owner = di.Kode_Stock_Owner
                LEFT JOIN Barang_Masuk_Summary bms
                    ON bms.Kode_Perusahaan = di.Kode_Perusahaan
                    AND bms.No_Pembelian_Loading = di.No_Faktur
                    AND bms.Kode_Barang = di.Kode_Barang
                WHERE 1 = 1
                {statusFilter}
                {searchFilter}
                AND di.Kode_Barang IN ({FilterKodeBarang})
                ORDER BY 
                    CASE WHEN di.Status_Group = 'Terlambat' THEN 0 ELSE 1 END,
                    di.No_Faktur, 
                    di.Kode_Barang;
            "

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim idx As Integer = Guna2DataGridView1.Rows.Add(
                        Dr("No_DO"),
                        Dr("No_PO"),
                        Dr("Kode_Barang"),
                        Dr("Nama_Barang"),
                        Dr("Satuan"),
                        Dr("Qty_DO"),
                        Dr("KeteranganDO"),
                        Dr("Tanggal_OTW_DO"),
                        Dr("Tanggal_ETA_DO"),
                        Dr("Tanggal_Masuk_DO"),
                        Dr("Status_Pengiriman"),
                        Dr("Status_Bongkar_DO"),
                        Dr("Status_Pembelian_DO"),
                        Dr("UseriD")
                    )

                    Dim statusPengiriman As String = Dr("Status_Pengiriman").ToString().Trim()
                    Dim cellPengiriman = Guna2DataGridView1.Rows(idx).Cells(10)

                    If statusPengiriman = "-" Or statusPengiriman.StartsWith("Belum Kirim", StringComparison.OrdinalIgnoreCase) Then
                        cellPengiriman.Style.BackColor = Color.FromArgb(34, 40, 49)
                        cellPengiriman.Style.ForeColor = Color.White
                    ElseIf statusPengiriman.StartsWith("Telat Datang", StringComparison.OrdinalIgnoreCase) Then
                        cellPengiriman.Style.BackColor = Color.FromArgb(215, 53, 53)
                        cellPengiriman.Style.ForeColor = Color.White
                    ElseIf statusPengiriman.StartsWith("Telat Kirim", StringComparison.OrdinalIgnoreCase) Then
                        cellPengiriman.Style.BackColor = Color.FromArgb(215, 53, 53)
                        cellPengiriman.Style.ForeColor = Color.White
                    ElseIf statusPengiriman.StartsWith("OTW", StringComparison.OrdinalIgnoreCase) Then
                        cellPengiriman.Style.BackColor = Color.FromArgb(255, 204, 0)
                        cellPengiriman.Style.ForeColor = Color.FromArgb(57, 18, 13)
                    ElseIf statusPengiriman = "Telah Tiba" Then
                        cellPengiriman.Style.BackColor = Color.FromArgb(17, 139, 80)
                        cellPengiriman.Style.ForeColor = Color.White
                    End If

                    Dim statusBongkar As String = Dr("Status_Bongkar_DO").ToString().Trim()
                    Dim cellBongkar = Guna2DataGridView1.Rows(idx).Cells(11)

                    If statusBongkar = "-" Then
                        cellBongkar.Style.BackColor = Color.FromArgb(34, 40, 49)
                        cellBongkar.Style.ForeColor = Color.White
                    ElseIf statusBongkar = "Validasi" Then
                        cellBongkar.Style.BackColor = Color.FromArgb(17, 139, 80)
                        cellBongkar.Style.ForeColor = Color.White
                    Else
                        cellBongkar.Style.BackColor = Color.FromArgb(255, 204, 0)
                        cellBongkar.Style.ForeColor = Color.FromArgb(57, 18, 13)
                    End If

                    Dim statusPembelian As String = Dr("Status_Pembelian_DO").ToString().Trim()
                    Dim cellPembelian = Guna2DataGridView1.Rows(idx).Cells(12)

                    If statusPembelian = "-" Then
                        cellPembelian.Style.BackColor = Color.FromArgb(34, 40, 49)
                        cellPembelian.Style.ForeColor = Color.White
                    ElseIf statusPembelian = "Selesai" Then
                        cellPembelian.Style.BackColor = Color.FromArgb(17, 139, 80)
                        cellPembelian.Style.ForeColor = Color.White
                    Else
                        cellPembelian.Style.BackColor = Color.FromArgb(34, 40, 49)
                        cellPembelian.Style.ForeColor = Color.White
                    End If

                    Dim tanggalETA As String = Dr("Tanggal_ETA_DO").ToString().Trim()
                    Dim tanggalMasuk As String = Dr("Tanggal_Masuk_DO").ToString().Trim()
                    Dim shouldColorRed As Boolean = False

                    If tanggalETA = "-" Then
                        shouldColorRed = True
                    ElseIf tanggalMasuk <> "-" Then
                        Try
                            Dim etaDate As DateTime = DateTime.ParseExact(tanggalETA, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture)
                            Dim masukDate As DateTime = DateTime.ParseExact(tanggalMasuk, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture)

                            If masukDate > etaDate Then
                                shouldColorRed = True
                            End If
                        Catch ex As Exception
                        End Try
                    Else
                        Try
                            Dim etaDate As DateTime = DateTime.ParseExact(tanggalETA, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture)

                            If DateTime.Now > etaDate Then
                                shouldColorRed = True
                            End If
                        Catch ex As Exception
                        End Try
                    End If

                    If shouldColorRed Then
                        For col As Integer = 0 To Guna2DataGridView1.Rows(idx).Cells.Count - 1
                            If col <> 10 AndAlso col <> 11 AndAlso col <> 12 Then
                                Guna2DataGridView1.Rows(idx).Cells(col).Style.BackColor = Color.FromArgb(215, 53, 53)
                                Guna2DataGridView1.Rows(idx).Cells(col).Style.ForeColor = Color.White
                            End If
                        Next
                    End If
                End While
            End Using

            Guna2DataGridView1.ClearSelection()

            DGV_Empty_Placeholder.Visible = (Guna2DataGridView1.Rows.Count = 0)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button_Export_Excel_Click(sender As Object, e As EventArgs) Handles Button_Export_Excel.Click
        If Guna2DataGridView1.Rows.Count = 0 Then
            MsgBox("Tidak ada data untuk di-export!", MsgBoxStyle.Information)
            Return
        End If

        Dim xlApp As New Microsoft.Office.Interop.Excel.Application()

        If xlApp Is Nothing Then
            MsgBox("Excel is not properly installed!", MsgBoxStyle.Critical)
            Return
        End If

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim format_akhir As String = Now.ToString("dd_MM_yyyy_HH_mm")
        Dim nama_file As String = "DO_Outstanding_" & format_akhir & ".xlsx"
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("Sheet1")
        xlApp.ScreenUpdating = False
        xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

        Try
            For colIndex As Integer = 0 To Guna2DataGridView1.Columns.Count - 1
                xlWorkSheet.Cells(1, colIndex + 1).Value = Guna2DataGridView1.Columns(colIndex).HeaderText
            Next

            Dim rowIndex As Integer = 2
            Dim rows = Guna2DataGridView1.Rows.Count
            Dim cols = Guna2DataGridView1.Columns.Count

            If rows > 0 Then
                Dim dataArr(rows - 1, cols - 1) As Object

                For r As Integer = 0 To rows - 1
                    For c As Integer = 0 To cols - 1
                        Dim value = Guna2DataGridView1.Rows(r).Cells(c).Value
                        Dim cellValue As String = If(value IsNot Nothing AndAlso Not IsDBNull(value), value.ToString(), "")
                        dataArr(r, c) = cellValue
                    Next
                Next

                Dim startCell = xlWorkSheet.Cells(2, 1)
                Dim endCell = xlWorkSheet.Cells(rows + 1, cols)
                Dim writeRange = xlWorkSheet.Range(startCell, endCell)
                Dim lastRow As Integer = rows + 1

                Dim rangeText As String =
                    "A2:E" & lastRow & ";" &
                    "G2:G" & lastRow & ";" &
                    "K2:N" & lastRow

                xlWorkSheet.Range(rangeText).NumberFormat = "@"
                xlWorkSheet.Range(rangeText).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft


                Dim rangeDate As String =
                    "H2:J" & lastRow

                xlWorkSheet.Range(rangeDate).NumberFormat = "dd mmm yyyy"
                xlWorkSheet.Range(rangeDate).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter

                Dim rangeNumber As String =
                    "F2:F" & lastRow

                xlWorkSheet.Range(rangeNumber).NumberFormat = "#,##0.00"
                xlWorkSheet.Range(rangeNumber).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

                writeRange.Value = dataArr
                rowIndex = rows + 2
            End If

            xlWorkSheet.Cells.EntireColumn.AutoFit()

            Dim dataRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(rowIndex - 1, Guna2DataGridView1.Columns.Count))
            With dataRange.Borders
                .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
            End With

            Dim headerRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, Guna2DataGridView1.Columns.Count))
            With headerRange
                .Font.Bold = True
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                .VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
                .WrapText = True
            End With

            xlApp.ScreenUpdating = True
            xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationAutomatic
        Catch ex As Exception
            MsgBox("Error generating template: " & ex.Message, MsgBoxStyle.Critical)
            xlApp.ScreenUpdating = True
            xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationAutomatic
            xlWorkBook.Close(False)
            xlApp.Quit()
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            Exit Sub
        End Try

        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx"
        saveFileDialog.FileName = nama_file

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                Dim filePath As String = saveFileDialog.FileName
                xlWorkBook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook)
                MsgBox("DO Outstanding berhasil di-export!", MsgBoxStyle.Information)
                xlWorkBook.Close()
                xlApp.Quit()
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)
            Catch ex As Exception
                MsgBox("Error saat menyimpan file: " & ex.Message, MsgBoxStyle.Critical)
                xlWorkBook.Close(False)
                xlApp.Quit()
                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)
            End Try
        Else
            xlWorkBook.Close(False)
            xlApp.Quit()
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
        End If
    End Sub

    Private Function CellName(columnNumber As Integer) As String
        Dim result As String = ""

        While columnNumber > 0
            columnNumber -= 1
            result = Chr(65 + (columnNumber Mod 26)) & result
            columnNumber = columnNumber \ 26
        End While

        Return result
    End Function

    Private Sub Guna2DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Guna2DataGridView1.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            Guna2DataGridView1.ClearSelection()
            Guna2DataGridView1.Rows(e.RowIndex).Selected = True
            Guna2DataGridView1.CurrentCell = Guna2DataGridView1.Rows(e.RowIndex).Cells(0)

            Guna2DataGridView1.ContextMenuStrip = Guna2ContextMenuStrip1
        End If
    End Sub

    Private Sub BatalkanDOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanDOToolStripMenuItem.Click
        If Guna2DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih data DO terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row As DataGridViewRow = Guna2DataGridView1.SelectedRows(0)
        Dim NoDO As String = row.Cells("Column1").Value.ToString()

        ' ============================================
        ' KONFIRMASI PEMBATALAN
        ' ============================================
        Dim result As DialogResult = MessageBox.Show(
    $"Yakin ingin membatalkan DO: {NoDO} ?",
    "Konfirmasi",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Question
)

        If result <> DialogResult.Yes Then Exit Sub

        ' ============================================
        ' PROSES PEMBATALAN
        ' ============================================
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Batal_DO_Barang_Lain") = "T" Then
                MsgBox("Anda tidak memiliki akses membatalkan DO.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoDO", NoDO)

            ' Query untuk mengecek apakah ada record dengan Flag = 'Y'
            Cmd.CommandText = "SELECT COUNT(*) AS JumlahFlagY FROM EMI_Pembelian_Loading_Detail_Barang_Lain " &
                  "WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoDO AND Flag_Sudah_Bongkar_Android = 'Y'"

            Dim countFlagY As Integer = CInt(Cmd.ExecuteScalar())

            ' Jika ada 1 atau lebih Flag = 'Y', tolak pembatalan
            If countFlagY > 0 Then
                MessageBox.Show("Pembatalan tidak bisa dilakukan karena di DO sudah ada barang yang diproses.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Update Status, User Batal, Tanggal Batal, Jam Batal di tabel emi_pembelian_loading_Barang_Lain
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoDO", NoDO)
            Cmd.Parameters.AddWithValue("@UserID", UserID)
            Cmd.Parameters.AddWithValue("@TanggalBatal", Date.Now)
            Cmd.Parameters.AddWithValue("@JamBatal", Date.Now.ToString("HH:mm:ss"))
            Dim SQL_UpdateDO As String = "UPDATE emi_pembelian_loading_Barang_Lain SET Status = 'Y', UserID_Batal = @UserID, Tanggal_Batal = @TanggalBatal, Jam_Batal = @JamBatal WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoDO"
            ExecuteTrans(SQL_UpdateDO)

            Dim dataList As New List(Of Dictionary(Of String, Object))

            ' SELECT data DO Detail
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoDO", NoDO)
            Dim SQL_DO_Detail As String = "SELECT No_Faktur, No_PO, Kode_Barang FROM emi_pembelian_loading_detail_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoDO"

            Cmd.CommandText = SQL_DO_Detail
            Using Dr = Cmd.ExecuteReader()
                While Dr.Read()
                    Dim record As New Dictionary(Of String, Object)
                    record.Add("No_PO", Dr("No_PO"))
                    record.Add("Kode_Barang", Dr("Kode_Barang"))
                    dataList.Add(record)
                End While
            End Using

            ' SELECT Urut_Det_Induk dari emi_pembelian_po_det_Barang_Lain untuk setiap No_PO dan Kode_Barang
            For i = 0 To dataList.Count - 1
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", dataList(i)("No_PO"))
                Cmd.Parameters.AddWithValue("@KodeBarang", dataList(i)("Kode_Barang"))
                Dim SQL_SelectUrutDet As String = "SELECT Urut_Det_Induk FROM emi_pembelian_po_det_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang"

                Cmd.CommandText = SQL_SelectUrutDet
                Dim objUrutDet = Cmd.ExecuteScalar()
                If objUrutDet IsNot Nothing Then
                    dataList(i).Add("Urut_Det_Induk", objUrutDet)
                End If
            Next

            For Each record In dataList
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", record("No_PO"))
                Cmd.Parameters.AddWithValue("@UserID", UserID)
                Cmd.Parameters.AddWithValue("@TanggalBatal", Date.Now)
                Cmd.Parameters.AddWithValue("@JamBatal", Date.Now.ToString("HH:mm:ss"))
                Dim SQL_UpdateSubPO As String = "UPDATE emi_pembelian_po_Barang_Lain SET Status = 'Y', UserID_Batal = @UserID, Tanggal_Batal = @TanggalBatal, Jam_Batal = @JamBatal, Flag_Selesai_PO = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"
                ExecuteTrans(SQL_UpdateSubPO)

                ' SELECT No_FakInduk dari emi_Pembelian_po_det_barang_lain
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", record("No_PO"))
                Dim SQL_SelectNoFakInduk As String = "SELECT No_FakInduk FROM emi_Pembelian_po_det_barang_lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Cmd.CommandText = SQL_SelectNoFakInduk
                Dim NoFakInduk As String = Cmd.ExecuteScalar().ToString()

                Dim KodeBarang As String = record("Kode_Barang").ToString()
                Dim UrutDetInduk As String = record("Urut_Det_Induk").ToString()

                ' UPDATE Emi_Pembelian_PO_Induk_Barang_Lain
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
                Dim SQL_UpdateInduk As String = "UPDATE Emi_Pembelian_PO_Induk_Barang_Lain SET Flag_Selesai_SubPO = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"
                ExecuteTrans(SQL_UpdateInduk)

                ' UPDATE Emi_Pembelian_PO_det_Induk_Barang_Lain dengan compare No_Urut = Urut_Det_Induk
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                Cmd.Parameters.AddWithValue("@NoUrut", UrutDetInduk)
                Dim SQL_UpdateDetInduk As String = "UPDATE Emi_Pembelian_PO_det_Induk_Barang_Lain SET Flag_Selesai = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND Kode_Barang = @KodeBarang AND No_Urut = @NoUrut"
                ExecuteTrans(SQL_UpdateDetInduk)
            Next

            Cmd.Transaction.Commit()
            CloseConn()

            Fetch_DO_Outstanding()
            MessageBox.Show("DO berhasil dibatalkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            If Cmd.Transaction IsNot Nothing Then
                Cmd.Transaction.Rollback()
            End If

            CloseTrans()
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
