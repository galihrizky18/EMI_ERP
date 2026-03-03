Imports System.Web
Imports Guna.UI2.WinForms

Public Class N_EMI_SD_Procurement_Barang_Lain_PR
    Public Property FilterKodeBarang As String

    Private searchTimer As Timer
    Private Const DEBOUNCE_DELAY As Integer = 750
    Private Const MIN_SEARCH_LENGTH As Integer = 3

    Private Sub SD_Card_PR_Outstanding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        searchTimer = New Timer With {.Interval = DEBOUNCE_DELAY}
        AddHandler searchTimer.Tick, AddressOf OnSearchTimerTick
        AddHandler TB_Search.TextChanged, AddressOf TB_Search_TextChanged
        AddHandler CB_Status.SelectedIndexChanged, AddressOf CB_Status_SelectedIndexChanged

        Fetch_PR_Outstanding()
    End Sub

    Private Sub TB_Search_TextChanged(sender As Object, e As EventArgs)
        searchTimer.Stop()
        searchTimer.Start()
    End Sub

    Private Sub OnSearchTimerTick(sender As Object, e As EventArgs)
        searchTimer.Stop()
        Fetch_PR_Outstanding()
    End Sub

    Private Sub CB_Status_SelectedIndexChanged(sender As Object, e As EventArgs)
        Fetch_PR_Outstanding()
    End Sub

    Private Sub Fetch_PR_Outstanding()
        Try
            If Guna2DataGridView1 Is Nothing OrElse Guna2DataGridView1.Columns.Count = 0 Then Return

            OpenConn()
            Guna2DataGridView1.Rows.Clear()
            Guna2DataGridView1.RowHeadersVisible = True
            Guna2DataGridView1.RowHeadersWidth = 30
            Guna2DataGridView1.AllowUserToResizeRows = False
            Guna2DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            Dim statusFilter As String = ""
            Select Case CB_Status.Text
                Case "TELAT"
                    statusFilter = "AND DATEDIFF(DAY, GETDATE(), pr_d.Tanggal_Delivery) < 0"
                Case "ON SCHEDULE"
                    statusFilter = "AND DATEDIFF(DAY, GETDATE(), pr_d.Tanggal_Delivery) >= 0"
                Case Else
                    statusFilter = ""
            End Select

            If FilterKodeBarang = "" Then
                CloseConn()
                Exit Sub
            End If

            SQL = $"
                SELECT 
                    pr.No_Faktur as No_PR,
                    FORMAT(pr.Tanggal_Release, 'dd MMM yyyy') as Tanggal_PR,
                    FORMAT(pr_d.Tanggal_Delivery, 'dd MMM yyyy') as Tanggal_Kebutuhan_PR,
                    CASE 
                        WHEN pr_d.Tanggal_Delivery >= CAST(GETDATE() AS DATE) 
                        THEN CAST(DATEDIFF(DAY, CAST(GETDATE() AS DATE), pr_d.Tanggal_Delivery) AS VARCHAR) + ' hari lagi'
                        ELSE 'Telat ' + CAST(DATEDIFF(DAY, pr_d.Tanggal_Delivery, CAST(GETDATE() AS DATE)) AS VARCHAR) + ' hari'
                    END AS Sisa_Waktu_PR,
                    pr_d.Kode_Barang,
                    b.Nama as Nama_Barang,
                    pr_d.Keterangan,
                    pr_d.Satuan,
                    pr_d.No_Urut,
                    pr_d.Jumlah as Qty_PR,
                    ISNULL(SUM(CASE 
                        WHEN poi.Status IS NULL AND poi.Flag_Release = 'Y' 
                        THEN poi_d.Jumlah 
                        ELSE 0 
                    END), 0) as Qty_Sudah_PO_Induk,
                    pr_d.Jumlah - ISNULL(SUM(CASE 
                        WHEN poi.Status IS NULL AND poi.Flag_Release = 'Y' 
                        THEN poi_d.Jumlah 
                        ELSE 0 
                    END), 0) as Qty_Outstanding,
                    pr.User_Release
                FROM EMI_Purchase_Requisition_Barang_Lain pr
                JOIN EMI_Purchase_Requisition_Barang_Lain_Detail pr_d 
                    ON pr_d.No_Faktur = pr.No_Faktur
                    AND pr_d.Kode_Perusahaan = pr.Kode_Perusahaan
                LEFT JOIN EMI_Pembelian_PO_Det_Induk_Barang_Lain poi_d 
                    ON poi_d.No_Urut_PR = pr_d.No_Urut 
                    AND poi_d.Kode_Barang = pr_d.Kode_Barang
                    AND poi_d.Kode_Perusahaan = pr.Kode_Perusahaan
                LEFT JOIN EMI_Pembelian_PO_Induk_Barang_Lain poi 
                    ON poi.No_Faktur = poi_d.No_Faktur
                    AND poi.Kode_Perusahaan = pr.Kode_Perusahaan
                LEFT JOIN Barang_Lain b 
                    ON b.Kode_Barang = pr_d.Kode_Barang 
                    AND b.Kode_Stock_Owner = pr_d.Kode_Stock_Owner
                    AND b.Kode_Perusahaan = pr.Kode_Perusahaan
                WHERE
                    pr.Kode_Perusahaan = '{KodePerusahaan}'
                    AND pr.Status IS NULL
                    AND pr.Flag_Release = 'Y'
                    AND pr_d.Flag_Sudah_po IS NULL
                    AND (
                        pr.No_Faktur LIKE '%{TB_Search.Text}%'
                        OR b.Kode_Barang LIKE '%{TB_Search.Text}%'
                        OR b.Nama LIKE '%{TB_Search.Text}%'
                    )
                    {statusFilter}
                    AND pr_d.Kode_Barang IN ({FilterKodeBarang})
                GROUP BY
                    pr.User_Release,
                    pr.No_Faktur,
                    pr.Tanggal_Release,
                    pr_d.Tanggal_Delivery,
                    pr_d.Kode_Barang,
                    pr_d.Keterangan,
                    pr_d.Satuan,
                    pr_d.Jumlah,
                    pr_d.No_Urut,
                    b.Nama
                HAVING 
                    pr_d.Jumlah - ISNULL(SUM(CASE 
                        WHEN poi.Status IS NULL AND poi.Flag_Release = 'Y' 
                        THEN poi_d.Jumlah 
                        ELSE 0 
                    END), 0) > 0
                ORDER BY 
                    CASE 
                        WHEN pr_d.Tanggal_Delivery < CAST(GETDATE() AS DATE) THEN 0
                        ELSE 1
                    END,
                    pr_d.Tanggal_Delivery,
                    pr.No_Faktur,
                    pr_d.Kode_Barang
            "

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim idx As Integer = Guna2DataGridView1.Rows.Add(
                        Dr("No_PR"),
                        Dr("Tanggal_PR"),
                        Dr("Kode_Barang"),
                        Dr("Nama_Barang"),
                        Dr("Keterangan"),
                        Dr("Satuan"),
                        Dr("Qty_PR"),
                        Dr("Qty_Sudah_PO_Induk"),
                        Dr("Qty_Outstanding"),
                        Dr("Tanggal_Kebutuhan_PR"),
                        Dr("Sisa_Waktu_PR"),
                        Dr("User_Release")
                    )

                    Dim sisaWaktu As String = Dr("Sisa_Waktu_PR").ToString()
                    If sisaWaktu.StartsWith("Telat", StringComparison.OrdinalIgnoreCase) Then
                        Guna2DataGridView1.Rows(idx).DefaultCellStyle.BackColor = Color.FromArgb(215, 53, 53)
                        Guna2DataGridView1.Rows(idx).DefaultCellStyle.ForeColor = Color.White
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
        Dim nama_file As String = "PR_Outstanding_Barang_Lain_" & format_akhir & ".xlsx"
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
                    "A2:F" & lastRow & ";" &
                    "K2:K" & lastRow & ";" &
                    "L2:L" & lastRow

                xlWorkSheet.Range(rangeText).NumberFormat = "@"
                xlWorkSheet.Range(rangeText).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft


                Dim rangeDate As String =
                    "B2:B" & lastRow & ";" &
                    "J2:J" & lastRow

                xlWorkSheet.Range(rangeDate).NumberFormat = "dd mmm yyyy"
                xlWorkSheet.Range(rangeDate).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter

                Dim rangeNumber As String =
                    "G2:I" & lastRow

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
                MsgBox("PR Outstanding Barang Lain berhasil di-export!", MsgBoxStyle.Information)
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
End Class
