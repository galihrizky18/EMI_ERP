Imports System.Data.SqlClient

Public Class N_EMI_SD_Procurement_PO
    Public Property IDGroupJenis As Integer

    Private searchTimer As Timer
    Private Const DEBOUNCE_DELAY As Integer = 750
    Private Const MIN_SEARCH_LENGTH As Integer = 3

    Private hasETDValue As Boolean = False

    Private Sub SD_Card_PO_Outstanding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()
            If CekButtonRole("Ubah_ETD_ETA_PO_Induk") = "T" Then
                Button_Simpan.Enabled = False
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        searchTimer = New Timer With {.Interval = DEBOUNCE_DELAY}
        AddHandler searchTimer.Tick, AddressOf OnSearchTimerTick
        AddHandler TB_Search.TextChanged, AddressOf TB_Search_TextChanged
        AddHandler CB_Status.SelectedIndexChanged, AddressOf CB_Status_SelectedIndexChanged

        Guna2TextBox1.Enabled = False

        Guna2DateTimePicker1.Format = DateTimePickerFormat.Long

        ClearFormInputs()

        Fetch_PO_Outstanding()
    End Sub

    Private Sub ClearFormInputs()
        Guna2TextBox1.Text = ""

        Guna2DateTimePicker1.Format = DateTimePickerFormat.Long
        Guna2DateTimePicker1.Value = DateTime.Now.Date
        hasETDValue = True
    End Sub

    Private Sub Guna2DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView2.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim row = Guna2DataGridView2.Rows(e.RowIndex)

                Guna2TextBox1.Text = If(IsDBNull(row.Cells("NoPOInduk").Value), "", row.Cells("NoPOInduk").Value.ToString())

                Dim noPO As String = Guna2TextBox1.Text
                If Not String.IsNullOrEmpty(noPO) Then
                    Try
                        OpenConn()
                        SQL = $"SELECT ETD_Simulasi FROM EMI_Pembelian_PO_Induk WHERE No_Faktur = '{noPO}'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read() Then
                                If Not IsDBNull(Dr("ETD_Simulasi")) AndAlso Dr("ETD_Simulasi") IsNot Nothing Then
                                    Guna2DateTimePicker1.Value = Convert.ToDateTime(Dr("ETD_Simulasi"))
                                    hasETDValue = True
                                Else
                                    Guna2DateTimePicker1.Value = DateTime.Now.Date
                                    hasETDValue = True
                                End If
                            End If
                        End Using
                        CloseConn()
                    Catch ex As Exception
                        CloseConn()
                        MessageBox.Show("Error mengambil data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TB_Search_TextChanged(sender As Object, e As EventArgs)
        searchTimer.Stop()
        searchTimer.Start()
    End Sub

    Private Sub OnSearchTimerTick(sender As Object, e As EventArgs)
        searchTimer.Stop()
        Fetch_PO_Outstanding()
    End Sub

    Private Sub CB_Status_SelectedIndexChanged(sender As Object, e As EventArgs)
        Fetch_PO_Outstanding()
    End Sub

    Private Sub Fetch_PO_Outstanding()
        Try
            If Guna2DataGridView2 Is Nothing OrElse Guna2DataGridView2.Columns.Count = 0 Then Return

            OpenConn()
            Guna2DataGridView2.Rows.Clear()
            Guna2DataGridView2.RowHeadersVisible = True
            Guna2DataGridView2.RowHeadersWidth = 30
            Guna2DataGridView2.AllowUserToResizeRows = False
            Guna2DataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            Dim statusFilter As String = ""
            Select Case CB_Status.Text
                Case "TELAT"
                    statusFilter = "AND DATEDIFF(DAY, GETDATE(), poi.ETD_Simulasi) < 0"
                Case "ON SCHEDULE"
                    statusFilter = "AND DATEDIFF(DAY, GETDATE(), poi.ETD_Simulasi) >= 0"
                Case Else
                    statusFilter = ""
            End Select

            SQL = $"
                ;WITH SubPO AS (
                    SELECT
                        spo_d.Kode_Perusahaan,
                        spo_d.No_FakInduk,
                        spo_d.No_Urut_PR,
                        spo_d.Kode_Barang,
                        spo_d.Kode_Stock_Owner,
                        SUM(spo_d.Jumlah) AS Total_Sub_PO
                    FROM EMI_Pembelian_PO_Det spo_d
                    INNER JOIN EMI_Pembelian_PO spo 
                        ON spo.No_Faktur = spo_d.No_Faktur
                        AND spo.Status IS NULL
                        AND spo.Flag_Release = 'Y'
                        AND spo.Kode_Perusahaan = spo_d.Kode_Perusahaan
                    GROUP BY spo_d.Kode_Perusahaan, spo_d.No_FakInduk, spo_d.No_Urut_PR, spo_d.Kode_Barang, spo_d.Kode_Stock_Owner
                ),
                PR AS (
                    SELECT
                        pr_d.Kode_Perusahaan,
                        pr_d.No_Faktur,
                        pr_d.No_Urut,
                        pr_d.Kode_Barang,
                        pr_d.Kode_Stock_Owner,
                        pr_d.Tanggal_Delivery
                    FROM EMI_Purchase_Requisition_Detail pr_d
                    INNER JOIN EMI_Purchase_Requisition pr 
                        ON pr_D.No_Faktur = pr.No_Faktur
                        AND pr.Status IS NULL
                        AND pr.Flag_Release = 'Y'
                        AND pr_d.Kode_Perusahaan = pr.Kode_Perusahaan
                    GROUP BY pr_d.Kode_Perusahaan, pr_d.No_Faktur, pr_d.No_Urut, pr_d.Kode_Barang, pr_d.Kode_Stock_Owner, pr_d.Tanggal_Delivery
                )
                SELECT 
                    poi.No_Faktur AS No_PO_Induk,
                    poi.User_Release,
                    pr.No_Faktur AS No_PR,
                    FORMAT(pr.Tanggal_Delivery, 'dd MMM yyyy') AS Tanggal_Kebutuhan,
                    FORMAT(poi.Tanggal_Release, 'dd MMM yyyy') AS Tanggal_PO_Induk,
                    FORMAT(poi.ETD_Simulasi, 'dd MMM yyyy') AS Tanggal_ETD_PO_Induk,
                    CASE 
                        WHEN pr.Tanggal_Delivery >= CAST(GETDATE() AS DATE) 
                            THEN CAST(DATEDIFF(DAY, CAST(GETDATE() AS DATE), pr.Tanggal_Delivery) AS VARCHAR) + ' hari lagi'
                        ELSE 
                            'Telat ' + CAST(DATEDIFF(DAY, pr.Tanggal_Delivery, CAST(GETDATE() AS DATE)) AS VARCHAR) + ' hari'
                    END AS Sisa_Waktu_PO_Induk,
                    poi_d.Kode_Barang,
                    poi_d.Satuan,
                    b.Nama AS Nama_Barang,
                    poi_d.Jumlah AS Qty_PO_Induk,
                    ISNULL(s.Total_Sub_PO, 0) AS Qty_Sudah_Sub_PO,
                    poi_d.Jumlah - ISNULL(s.Total_Sub_PO, 0) AS Qty_Outstanding,
                    poi_d.No_Urut,
                    poi_d.No_Urut_PR,
                    poi_d.Nilai_Barang, 
                    poi_d.Harga_Barang,
                    poi_d.Jumlah_Input
                FROM EMI_Pembelian_PO_Induk poi
                INNER JOIN EMI_Pembelian_PO_Det_Induk poi_d 
                    ON poi_d.No_Faktur = poi.No_Faktur
                    AND poi_D.Kode_Perusahaan = poi.Kode_Perusahaan
                LEFT JOIN Barang b 
                    ON b.Kode_Barang = poi_d.Kode_Barang 
                    AND b.Kode_Stock_Owner = poi_d.Kode_Stock_Owner
                    AND b.Kode_Perusahaan = poi.Kode_Perusahaan
                INNER JOIN PR pr 
                    ON pr.No_Urut = poi_d.No_Urut_PR
                    AND pr.Kode_Barang = poi_d.Kode_Barang
                    AND pr.Kode_Perusahaan = poi_d.Kode_Perusahaan
                LEFT JOIN SubPO s 
                    ON s.No_FakInduk = poi_d.No_Faktur
                    AND s.No_Urut_PR = poi_d.No_Urut_PR
                    AND s.Kode_Barang = poi_d.Kode_Barang
                    AND s.Kode_Perusahaan = poi_d.Kode_Perusahaan
                WHERE
                    poi.Kode_Perusahaan = '{KodePerusahaan}'
                    AND poi.Status IS NULL
                    AND poi.Flag_Release = 'Y'
                    AND poi.Flag_Selesai_SubPO IS NULL
                    AND (
                        poi.No_Faktur LIKE '%{TB_Search.Text}%'
                        OR pr.No_Faktur LIKE '%{TB_Search.Text}%'
                        OR b.Kode_Barang LIKE '%{TB_Search.Text}%'
                        OR b.Nama LIKE '%{TB_Search.Text}%'
                    )
                    AND poi_d.Jumlah - ISNULL(s.Total_Sub_PO, 0) > 0
                    {If(IDGroupJenis <> 0, $" AND b.Id_Group_Jenis = {IDGroupJenis} ", "")}
                    {statusFilter}
                ORDER BY 
                    poi.No_Faktur, poi_d.Kode_Barang;
            "

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim idx As Integer = Guna2DataGridView2.Rows.Add(
                        Dr("No_PR"),
                        Dr("No_PO_Induk"),
                        Dr("Tanggal_Kebutuhan"),
                        Dr("Tanggal_PO_Induk"),
                        Dr("Kode_Barang"),
                        Dr("Nama_Barang"),
                        Dr("Satuan"),
                        Dr("Qty_PO_Induk"),
                        Dr("Qty_Sudah_Sub_PO"),
                        Dr("Qty_Outstanding"),
                        Dr("Tanggal_ETD_PO_Induk"),
                        Dr("Sisa_Waktu_PO_Induk"),
                        Dr("User_Release"),
                        Dr("No_Urut"),
                        Dr("No_Urut_PR"),
                        Dr("Nilai_Barang"),
                        Dr("Harga_Barang"),
                        Dr("Jumlah_Input")
                    )

                    Dim sisaWaktu As String = Dr("Sisa_Waktu_PO_Induk").ToString()
                    If sisaWaktu.StartsWith("Telat", StringComparison.OrdinalIgnoreCase) Then
                        Guna2DataGridView2.Rows(idx).DefaultCellStyle.BackColor = Color.FromArgb(215, 53, 53)
                        Guna2DataGridView2.Rows(idx).DefaultCellStyle.ForeColor = Color.White
                    End If
                End While
            End Using

            Guna2DataGridView2.ClearSelection()

            DGV_Empty_Placeholder.Visible = (Guna2DataGridView2.Rows.Count = 0)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CB_Status_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles CB_Status.SelectedIndexChanged
        Fetch_PO_Outstanding()
    End Sub

    Private Sub Button_Simpan_Click(sender As Object, e As EventArgs) Handles Button_Simpan.Click
        Try
            OpenConn()
            If CekButtonRole("Ubah_ETD_ETA_PO_Induk") = "T" Then
                MsgBox("Anda tidak memiliki akses untuk melakukan tindakan ini!")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        Try
            If String.IsNullOrEmpty(Guna2TextBox1.Text) Then
                MessageBox.Show("Pilih PO Induk terlebih dahulu dari data grid!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim noPO As String = Guna2TextBox1.Text
            Dim newETD As DateTime = Guna2DateTimePicker1.Value.Date

            OpenConn()

            Dim oldETD As DateTime? = Nothing
            SQL = $"SELECT ETD_Simulasi FROM EMI_Pembelian_PO_Induk WHERE No_Faktur = '{noPO}'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    If Not IsDBNull(Dr("ETD_Simulasi")) AndAlso Dr("ETD_Simulasi") IsNot Nothing Then
                        oldETD = Convert.ToDateTime(Dr("ETD_Simulasi")).Date
                    End If
                End If
            End Using

            Dim hasETDChanged As Boolean = False
            If oldETD.HasValue Then
                hasETDChanged = (oldETD.Value <> newETD)
            Else
                hasETDChanged = True
            End If

            SQL = $"SELECT DISTINCT No_Faktur FROM EMI_Pembelian_PO_Det WHERE No_FakInduk = '{noPO}'"
            Dim listNoSubPO As New List(Of String)

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    If Not IsDBNull(Dr("No_Faktur")) AndAlso Not String.IsNullOrEmpty(Dr("No_Faktur").ToString()) Then
                        listNoSubPO.Add(Dr("No_Faktur").ToString())
                    End If
                End While
            End Using

            If listNoSubPO.Count > 0 Then
                Dim inClause As String = String.Join("','", listNoSubPO)
                SQL = $"SELECT MIN(ETD_Simulasi) as MinETD, No_Faktur 
                    FROM EMI_Pembelian_PO 
                    WHERE No_Faktur IN ('{inClause}')
                    AND ETD_Simulasi IS NOT NULL
                    GROUP BY No_Faktur
                    ORDER BY MIN(ETD_Simulasi)"

                Dim minETDSubPO As DateTime? = Nothing
                Dim noSubPOTercepat As String = ""

                Using Dr = OpenTrans(SQL)
                    If Dr.Read() Then
                        If Not IsDBNull(Dr("MinETD")) AndAlso Dr("MinETD") IsNot Nothing Then
                            minETDSubPO = Convert.ToDateTime(Dr("MinETD")).Date
                            noSubPOTercepat = Dr("No_Faktur").ToString()
                        End If
                    End If
                End Using

                If minETDSubPO.HasValue AndAlso newETD > minETDSubPO.Value Then
                    CloseConn()
                    MessageBox.Show($"ETD PO Induk tidak boleh lebih lama dari ETD di Sub PO!{vbCrLf}{vbCrLf}" &
                              $"Sub PO: {noSubPOTercepat}{vbCrLf}" &
                              $"ETD Sub PO tercepat: {minETDSubPO.Value:dd/MM/yyyy}{vbCrLf}" &
                              $"ETD PO Induk yang Anda input: {newETD:dd/MM/yyyy}{vbCrLf}{vbCrLf}" &
                              $"Silakan ubah ETD PO Induk menjadi maksimal {minETDSubPO.Value:dd/MM/yyyy} atau ubah ETD Sub PO terlebih dahulu.",
                              "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If


            If hasETDChanged Then
                SQL = $"UPDATE EMI_Pembelian_PO_Induk SET ETD_Simulasi = '{newETD:yyyy-MM-dd}' WHERE No_Faktur = '{noPO}'"
                ExecuteTrans(SQL)

                Dim currentDate As DateTime = DateTime.Now
                Dim currentTime As String = currentDate.ToString("HH:mm:ss")
                Dim etdLamaStr As String = If(oldETD.HasValue, $"'{oldETD.Value:yyyy-MM-dd}'", "NULL")

                SQL = $"INSERT INTO N_EMI_LOG_ETD_Pembelian_PO_Induk 
                    (Kode_Perusahaan, Tanggal, Jam, UserID, No_Faktur, ETD_Lama, ETD_Baru) 
                    VALUES 
                    ('{KodePerusahaan}', '{currentDate:yyyy-MM-dd HH:mm:ss}', '{currentTime}', '{UserID}', '{noPO}', {etdLamaStr}, '{newETD:yyyy-MM-dd}')"
                ExecuteTrans(SQL)
            End If

            CloseConn()

            ClearFormInputs()
            Fetch_PO_Outstanding()

            MessageBox.Show("Data berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error saat menyimpan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button_Export_Excel_Click(sender As Object, e As EventArgs) Handles Button_Export_Excel.Click
        If Guna2DataGridView2.Rows.Count = 0 Then
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
        Dim nama_file As String = "PO_Induk_Outstanding_" & format_akhir & ".xlsx"
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("Sheet1")
        xlApp.ScreenUpdating = False
        xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

        Try
            For colIndex As Integer = 0 To Guna2DataGridView2.Columns.Count - 6
                xlWorkSheet.Cells(1, colIndex + 1).Value = Guna2DataGridView2.Columns(colIndex).HeaderText
            Next

            Dim rowIndex As Integer = 2
            Dim rows = Guna2DataGridView2.Rows.Count
            Dim cols = Guna2DataGridView2.Columns.Count - 5

            If rows > 0 Then
                Dim dataArr(rows - 1, cols - 1) As Object

                For r As Integer = 0 To rows - 1
                    For c As Integer = 0 To cols - 1
                        Dim value = Guna2DataGridView2.Rows(r).Cells(c).Value
                        Dim cellValue As String = If(value IsNot Nothing AndAlso Not IsDBNull(value), value.ToString(), "")
                        dataArr(r, c) = cellValue
                    Next
                Next

                Dim startCell = xlWorkSheet.Cells(2, 1)
                Dim endCell = xlWorkSheet.Cells(rows + 1, cols)
                Dim writeRange = xlWorkSheet.Range(startCell, endCell)
                Dim lastRow As Integer = rows + 1

                Dim rangeText As String =
                    "A2:B" & lastRow & ";" &
                    "E2:G" & lastRow & ";" &
                    "K2:K" & lastRow

                xlWorkSheet.Range(rangeText).NumberFormat = "@"
                xlWorkSheet.Range(rangeText).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft


                Dim rangeDate As String =
                    "C2:D" & lastRow

                xlWorkSheet.Range(rangeDate).NumberFormat = "dd mmm yyyy"
                xlWorkSheet.Range(rangeDate).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter

                Dim rangeNumber As String =
                    "H2:J" & lastRow

                xlWorkSheet.Range(rangeNumber).NumberFormat = "#,##0.00"
                xlWorkSheet.Range(rangeNumber).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

                writeRange.Value = dataArr
                rowIndex = rows + 2
            End If

            xlWorkSheet.Cells.EntireColumn.AutoFit()

            Dim dataRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(rowIndex - 1, Guna2DataGridView2.Columns.Count - 5))
            With dataRange.Borders
                .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
            End With

            Dim headerRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, Guna2DataGridView2.Columns.Count - 5))
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
                MsgBox("PO Induk Outstanding berhasil di-export!", MsgBoxStyle.Information)
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

    Private Sub BatalkanPOInduk_Click(sender As Object, e As EventArgs) Handles BatalkanPOInduk.Click
        If Guna2DataGridView2.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih data PO Induk terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row As DataGridViewRow = Guna2DataGridView2.SelectedRows(0)
        Dim NoPOInduk As String = row.Cells("NoPOInduk").Value?.ToString()

        If String.IsNullOrEmpty(NoPOInduk) Then
            MessageBox.Show("Data tidak lengkap.", "Error")
            Exit Sub
        End If

        Dim result As DialogResult = MessageBox.Show(
        $"Yakin ingin membatalkan PO Induk {NoPOInduk} ?",
        "Konfirmasi",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If result <> DialogResult.Yes Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Batal_PO_Induk") = "T" Then
                MsgBox("Anda tidak memiliki akses membatalkan PO Induk.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'Cek apakah sudah ada qty sub po
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Dim SQL As String = "
            ;WITH SubPO AS (
                SELECT
                    spo_d.Kode_Perusahaan,
                    spo_d.No_FakInduk,
                    spo_d.No_Urut_PR,
                    spo_d.Kode_Barang,
                    spo_d.Kode_Stock_Owner,
                    SUM(spo_d.Jumlah) AS Total_Sub_PO
                FROM EMI_Pembelian_PO_Det spo_d
                INNER JOIN EMI_Pembelian_PO spo 
                    ON spo.No_Faktur = spo_d.No_Faktur
                    AND spo.Status IS NULL
                    AND spo.Flag_Release = 'Y'
                    AND spo.Kode_Perusahaan = spo_d.Kode_Perusahaan
                GROUP BY spo_d.Kode_Perusahaan, spo_d.No_FakInduk, spo_d.No_Urut_PR, spo_d.Kode_Barang, spo_d.Kode_Stock_Owner
            )
            SELECT 
                epd.Kode_Barang,
                epd.No_Urut,
                epd.Jumlah AS Qty_POInduk,
                ISNULL(s.Total_Sub_PO, 0) AS Qty_SubPO,
                epd.Nilai_Barang,
                epd.Harga_Barang,
                epd.Jumlah_Input,
                epd.No_Penawaran,
                epd.No_Urut_PR,
                pr.No_Faktur AS NoPR
            FROM emi_pembelian_po_det_induk epd
            LEFT JOIN SubPO s 
                ON s.No_FakInduk = epd.No_Faktur
                AND s.No_Urut_PR = epd.No_Urut_PR
                AND s.Kode_Barang = epd.Kode_Barang
                AND s.Kode_Perusahaan = epd.Kode_Perusahaan
            LEFT JOIN Emi_Purchase_Requisition_Detail pr
                ON pr.No_Urut = epd.No_Urut_PR
                AND pr.Kode_Barang = epd.Kode_Barang
            WHERE 
                epd.Kode_Perusahaan = @KodePerusahaan 
                AND epd.No_Faktur = @NoPOInduk
        "

            Cmd.CommandText = SQL
            Dim dtDetail As New DataTable
            Using adapter = New SqlDataAdapter(Cmd)
                adapter.Fill(dtDetail)
            End Using

            Dim adaBarangDenganQtySubPO As Boolean = False

            ' Cek satu-satu setiap barang
            For Each detRow As DataRow In dtDetail.Rows
                Dim qtyDO As Decimal = CDec(detRow("Qty_SubPO"))

                ' Jika ada 1 barang saja yang Qty_SubPO > 0, stop proses
                If qtyDO > 0 Then
                    adaBarangDenganQtySubPO = True
                    Exit For
                End If
            Next

            ' Jika ada barang dengan QtyDO > 0, batalkan proses
            If adaBarangDenganQtySubPO Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Pembatalan tidak bisa dilakukan karena barang sudah dalam Sub PO.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' ============================================
            ' UPDATE: Emi_Pembelian_PO_Induk
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Induk WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoPOInduk"

            Dim cekPOInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekPOInduk = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data PO Induk tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.Parameters.AddWithValue("@UserID", UserID)
            Cmd.Parameters.AddWithValue("@TanggalBatal", Date.Now())

            Dim SQL_Batal_PO_Induk As String = "UPDATE Emi_Pembelian_PO_Induk SET Status = 'Y', UserID_Batal = @UserID, Tanggal_Batal = @TanggalBatal WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoPOInduk"
            ExecuteTrans(SQL_Batal_PO_Induk)

            ' ============================================
            ' Ambil data PR untuk update flag
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)

            SQL = "
            SELECT DISTINCT
                po_d.No_Urut_PR,
                pr_d.No_Faktur AS NoPR,
                po_d.Kode_Barang
            FROM emi_pembelian_po_det_induk po_d
            JOIN emi_purchase_requisition_detail pr_d
                ON pr_d.No_Urut = po_d.No_Urut_PR
                AND pr_d.Kode_Barang = po_d.Kode_Barang
                AND pr_d.Kode_Perusahaan = po_d.Kode_Perusahaan
            WHERE po_d.Kode_Perusahaan = @KodePerusahaan
            AND po_d.No_Faktur = @NoPOInduk
        "

            Cmd.CommandText = SQL
            Dim dtPR As New DataTable
            Using adapter = New SqlDataAdapter(Cmd)
                adapter.Fill(dtPR)
            End Using

            ' ============================================
            ' UPDATE: Emi_Purchase_Requisition_Detail
            ' ============================================
            For Each prRow As DataRow In dtPR.Rows
                Dim NoUrutPR As String = prRow("No_Urut_PR").ToString()
                Dim No_PR As String = prRow("NoPR").ToString()
                Dim KodeBarang As String = prRow("Kode_Barang").ToString()

                ' Cek keberadaan data terlebih dahulu
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPR", No_PR)
                Cmd.Parameters.AddWithValue("@NoUrutPR", NoUrutPR)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Purchase_Requisition_Detail WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoPR AND Kode_Barang = @KodeBarang AND No_Urut = @NoUrutPR"

                Dim cekPRDetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekPRDetail = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data PR tidak ditemukan untuk No PR={No_PR}, No={NoUrutPR}, Kode Barang={KodeBarang}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPR", No_PR)
                Cmd.Parameters.AddWithValue("@NoUrutPR", NoUrutPR)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)

                SQL = "UPDATE Emi_Purchase_Requisition_Detail SET Flag_Sudah_PO = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoPR AND Kode_Barang = @KodeBarang AND No_Urut = @NoUrutPR"
                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()
            CloseConn()
            Fetch_PO_Outstanding()
            MessageBox.Show("PO Induk berhasil dibatalkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            If Cmd.Transaction IsNot Nothing Then
                Cmd.Transaction.Rollback()
            End If
            CloseTrans()
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Guna2DataGridView2_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs)

    End Sub

    Private Sub Guna2DataGridView2_CellMouseDown_1(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Guna2DataGridView2.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            Guna2DataGridView2.ClearSelection()
            Guna2DataGridView2.Rows(e.RowIndex).Selected = True
            Guna2DataGridView2.CurrentCell = Guna2DataGridView2.Rows(e.RowIndex).Cells(0)

            Guna2DataGridView2.ContextMenuStrip = Guna2ContextMenuStrip1
        End If
    End Sub

    Private Sub SelesaikanPOIndukBarangIni_Click(sender As Object, e As EventArgs) Handles SelesaikanPOIndukBarangIni.Click
        If Guna2DataGridView2.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih data Sub PO terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row As DataGridViewRow = Guna2DataGridView2.SelectedRows(0)
        Dim NoPOInduk As String = row.Cells("NoPOInduk").Value?.ToString()
        Dim NoUrut As String = row.Cells("No_Urut").Value?.ToString()
        Dim NoUrutPR As String = row.Cells("NoUrutPR").Value?.ToString()
        Dim NoPR As String = row.Cells("NoPR").Value?.ToString()
        Dim KodeBarang As String = row.Cells("KodeBarang").Value?.ToString()
        Dim NamaBarang As String = row.Cells("NamaBarang").Value?.ToString()
        Dim QtyPOInduk As Decimal = CDec(row.Cells("QtyPOInduk").Value)
        Dim NilaiBarang As Decimal = CDec(row.Cells("NilaiBarang").Value)
        Dim HargaBarang As Decimal = CDec(row.Cells("HargaBarang").Value)
        Dim JumlahInput As Decimal = CDec(row.Cells("JumlahInput").Value)

        If String.IsNullOrEmpty(NoPOInduk) OrElse String.IsNullOrEmpty(KodeBarang) OrElse String.IsNullOrEmpty(NoUrut) Then
            MessageBox.Show("Data tidak lengkap.", "Error")
            Exit Sub
        End If

        ' ============================================
        ' 1. AMBIL QtySubPO DARI DATABASE
        ' ============================================
        Dim QtySubPO As Decimal = 0

        Try
            OpenConn()

            If CekButtonRole("Selesai_Barang_PO_Induk") = "T" Then
                MsgBox("Anda tidak memiliki akses menyelesaikan sebagian barang di PO Induk.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim SQL As String = "
        ;WITH SubPO AS (
            SELECT
                spo_d.Kode_Perusahaan,
                spo_d.No_FakInduk,
                spo_d.No_Urut_PR,
                spo_d.Kode_Barang,
                spo_d.Kode_Stock_Owner,
                SUM(spo_d.Jumlah) AS Total_Sub_PO
            FROM EMI_Pembelian_PO_Det spo_d
            INNER JOIN EMI_Pembelian_PO spo 
                ON spo.No_Faktur = spo_d.No_Faktur
                AND spo.Status IS NULL
                AND spo.Flag_Release = 'Y'
                AND spo.Kode_Perusahaan = spo_d.Kode_Perusahaan
            GROUP BY spo_d.Kode_Perusahaan, spo_d.No_FakInduk, spo_d.No_Urut_PR, spo_d.Kode_Barang, spo_d.Kode_Stock_Owner
        )
        SELECT 
            ISNULL(s.Total_Sub_PO, 0) AS Qty_SubPO
        FROM emi_pembelian_po_det_induk epd
        LEFT JOIN SubPO s 
            ON s.No_FakInduk = epd.No_Faktur
            AND s.No_Urut_PR = epd.No_Urut_PR
            AND s.Kode_Barang = epd.Kode_Barang
            AND s.Kode_Perusahaan = epd.Kode_Perusahaan
        WHERE 
            epd.Kode_Perusahaan = @KodePerusahaan 
            AND epd.No_Faktur = @NoPOInduk
            AND epd.Kode_Barang = @KodeBarang
            AND epd.No_Urut = @NoUrut
    "

            Cmd.CommandText = SQL
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

            Dim objResult As Object = Cmd.ExecuteScalar()
            If objResult IsNot Nothing AndAlso Not IsDBNull(objResult) Then
                QtySubPO = CDec(objResult)
            End If

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error mengambil QtySubPO dari database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim NilaiBarangBaru As Decimal = (NilaiBarang / QtyPOInduk) * QtySubPO
        Dim JumlahInputBaru As Decimal = (JumlahInput / QtyPOInduk) * QtySubPO
        Dim TotalBaru As Decimal = QtySubPO * HargaBarang

        Dim result As DialogResult = MessageBox.Show(
            $"Yakin ingin menyelesaikan item {KodeBarang}-{NamaBarang} sejumlah {QtyPOInduk:N2} menjadi {QtySubPO:N2} di PO Induk {NoPOInduk} ?",
            "Konfirmasi",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        )

        If result <> DialogResult.Yes Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            ' 1. Cek dan Backup EMI_Pembelian_PO_Induk ke EMI_Pembelian_PO_Induk_Origin
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Induk_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoPOInduk
        "
            Dim CekPOInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPOInduk = 0 Then
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
                Dim SQL_Backup_PO_Induk As String = "
                    INSERT INTO EMI_Pembelian_PO_Induk_Origin (
                        Kode_Perusahaan,
                        No_Faktur,
                        No_Nota,
                        Status,
                        Tanggal,
                        Jam,
                        UserID,
                        Kode_Supplier,
                        Lokasi,
                        Jenis_Pembayaran,
                        Cara_Bayar,
                        Tgl_Jatuh_Tempo,
                        Total_MUA,
                        Mata_Uang,
                        Kurs,
                        Total_IDR,
                        Grand_Sebelum_PPN,
                        PPN,
                        Grand,
                        ETD_Simulasi,
                        No_Prepare_Bahan,
                        Flag_Lokasi_Tujuan,
                        Flag_ETD,
                        ETD,
                        Flag_Loading,
                        Flag_Timbang,
                        Flag_QC,
                        Flag_BM,
                        Flag_Val_BM,
                        Flag_Selisih_BM,
                        Flag_Val_Selisih_BM,
                        Flag_Pembelian,
                        Selesai,
                        Ekspedisi,
                        Biaya,
                        Flag_Release,
                        Tanggal_Release,
                        Jam_Release,
                        User_Release,
                        flag_sudah_pindah,
                        Jumlah_Print,
                        Flag_Import,
                        Tempo_Pembayaran,
                        Flag_Selesai_PO,
                        Flag_Biaya,
                        Lama_Pembayaran,
                        Grand_Total_Terbilang,
                        Flag_Selesai_SubPO,
                        Grand_PPH,
                        UserID_Batal,
                        Tanggal_Batal,
                        Jam_Batal
                        --UserID_Selesai,
                        --Tanggal_Selesai,
                        --Jam_Selesai
                    )
                    SELECT
                        Kode_Perusahaan,
                        No_Faktur,
                        No_Nota,
                        Status,
                        Tanggal,
                        Jam,
                        UserID,
                        Kode_Supplier,
                        Lokasi,
                        Jenis_Pembayaran,
                        Cara_Bayar,
                        Tgl_Jatuh_Tempo,
                        Total_MUA,
                        Mata_Uang,
                        Kurs,
                        Total_IDR,
                        Grand_Sebelum_PPN,
                        PPN,
                        Grand,
                        ETD_Simulasi,
                        No_Prepare_Bahan,
                        Flag_Lokasi_Tujuan,
                        Flag_ETD,
                        ETD,
                        Flag_Loading,
                        Flag_Timbang,
                        Flag_QC,
                        Flag_BM,
                        Flag_Val_BM,
                        Flag_Selisih_BM,
                        Flag_Val_Selisih_BM,
                        Flag_Pembelian,
                        Selesai,
                        Ekspedisi,
                        Biaya,
                        Flag_Release,
                        Tanggal_Release,
                        Jam_Release,
                        User_Release,
                        flag_sudah_pindah,
                        Jumlah_Print,
                        Flag_Import,
                        Tempo_Pembayaran,
                        Flag_Selesai_PO,
                        Flag_Biaya,
                        Lama_Pembayaran,
                        Grand_Total_Terbilang,
                        Flag_Selesai_SubPO,
                        Grand_PPH,
                        UserID_Batal,
                        Tanggal_Batal,
                        Jam_Batal
                        --UserID_Selesai,
                        --Tanggal_Selesai,
                        --Jam_Selesai
                    FROM EMI_Pembelian_PO_Induk
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoPOInduk
                    "

                ExecuteTrans(SQL_Backup_PO_Induk)
            End If

            ' 2. Cek dan Backup EMI_Pembelian_PO_Det_Induk ke EMI_Pembelian_PO_Det_Induk_Origin
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Det_Induk_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoPOInduk
        "
            Dim CekPODetInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODetInduk = 0 Then
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
                Dim SQL_Backup_PO_Det_Induk As String = "
                    INSERT INTO EMI_Pembelian_PO_Det_Induk_Origin (
                        Kode_Perusahaan,
                        No_Faktur,
                        Kode_Stock_Owner,
                        Kode_Barang,
                        No_Urut,
                        Jumlah,
                        Satuan,
                        Harga,
                        Nilai_Barang,
                        Satuan_Barang,
                        Harga_Barang,
                        Total,
                        No_Penawaran,
                        No_Urut_PR,
                        Flag_Selesai,
                        Jumlah_Input,
                        Satuan_Input
                    )
                    SELECT
                        Kode_Perusahaan,
                        No_Faktur,
                        Kode_Stock_Owner,
                        Kode_Barang,
                        No_Urut,
                        Jumlah,
                        Satuan,
                        Harga,
                        Nilai_Barang,
                        Satuan_Barang,
                        Harga_Barang,
                        Total,
                        No_Penawaran,
                        No_Urut_PR,
                        Flag_Selesai,
                        Jumlah_Input,
                        Satuan_Input
                    FROM EMI_Pembelian_PO_Det_Induk
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoPOInduk
                "

                ExecuteTrans(SQL_Backup_PO_Det_Induk)
            End If

            ' 3. Cek dan Backup EMI_Pembelian_PO_Detail_Induk ke EMI_Pembelian_PO_Detail_Induk_Origin
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Detail_Induk_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoPOInduk
        "
            Dim CekPODetailInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODetailInduk = 0 Then
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
                Dim SQL_Backup_PO_Detail_Induk As String = "
                    INSERT INTO EMI_Pembelian_PO_Detail_Induk_Origin (
                        Kode_Perusahaan,
                        No_Faktur,
                        Kode_Stock_Owner,
                        Kode_Barang,
                        No_Urut,
                        Jumlah,
                        Harga,
                        Satuan,
                        Jumlah_Masuk,
                        Nilai_Barang,
                        Harga_Barang,
                        Satuan_Barang,
                        Total,
                        No_Penawaran,
                        Flag_Prepare,
                        Flag_Loading,
                        Flag_Timbang,
                        Flag_Refraksi,
                        Flag_Permintaan_Refraksi,
                        Harga_Refraksi,
                        Jumlah_Input,
                        Satuan_Input
                    )
                    SELECT
                        Kode_Perusahaan,
                        No_Faktur,
                        Kode_Stock_Owner,
                        Kode_Barang,
                        No_Urut,
                        Jumlah,
                        Harga,
                        Satuan,
                        Jumlah_Masuk,
                        Nilai_Barang,
                        Harga_Barang,
                        Satuan_Barang,
                        Total,
                        No_Penawaran,
                        Flag_Prepare,
                        Flag_Loading,
                        Flag_Timbang,
                        Flag_Refraksi,
                        Flag_Permintaan_Refraksi,
                        Harga_Refraksi,
                        Jumlah_Input,
                        Satuan_Input
                    FROM EMI_Pembelian_PO_Detail_Induk
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoPOInduk
                    "

                ExecuteTrans(SQL_Backup_PO_Detail_Induk)
            End If

            ' Insert ke Log
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@Log_UserID", UserID)
            Cmd.Parameters.AddWithValue("@Log_Tanggal", Date.Now())
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

            Dim SQL_Log As String = "
             INSERT INTO Emi_Pembelian_PO_Det_Induk_Log (
                Kode_Perusahaan,
                No_Faktur,
                Kode_Stock_Owner,
                Kode_Barang,
                No_Urut,
                Jumlah,
                Satuan,
                Harga,
                Nilai_Barang,
                Satuan_Barang,
                Harga_Barang,
                Total,
                No_Penawaran,
                No_Urut_PR,
                Flag_Selesai,
                Jumlah_Input,
                Satuan_Input,
                Log_UserID,
                Log_Tanggal
            )
            SELECT
                Kode_Perusahaan,
                No_Faktur,
                Kode_Stock_Owner,
                Kode_Barang,
                No_Urut,
                Jumlah,
                Satuan,
                Harga,
                Nilai_Barang,
                Satuan_Barang,
                Harga_Barang,
                Total,
                No_Penawaran,
                No_Urut_PR,
                Flag_Selesai,
                Jumlah_Input,
                Satuan_Input,
                @Log_UserID,
                @Log_Tanggal
            FROM emi_pembelian_po_det_induk
            WHERE 
                Kode_Perusahaan = @KodePerusahaan 
                AND No_Faktur = @NoPOInduk 
                AND Kode_Barang = @KodeBarang
                AND No_Urut = @NoUrut
        "
            ExecuteTrans(SQL_Log)

            ' UPDATE 1: Cek data sebelum update emi_pembelian_po_det_induk
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM emi_pembelian_po_det_induk
            WHERE 
                Kode_Perusahaan = @KodePerusahaan
                AND No_Faktur = @NoPOInduk
                AND Kode_Barang = @KodeBarang
                AND No_Urut = @NoUrut
        "
            Dim cekDetInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())

            If cekDetInduk = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
                MessageBox.Show($"Data detail PO Induk tidak ditemukan untuk barang {KodeBarang} No {NoUrut}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Update Detail PO Induk
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@QtySubPO", QtySubPO)
            Cmd.Parameters.AddWithValue("@NilaiBarangBaru", NilaiBarangBaru)
            Cmd.Parameters.AddWithValue("@JumlahInputBaru", JumlahInputBaru)
            Cmd.Parameters.AddWithValue("@TotalBaru", TotalBaru)
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

            Dim SQL_Update_Det As String = "
        UPDATE emi_pembelian_po_det_induk SET
            Flag_Selesai = 'Y',
            Jumlah = @QtySubPO,
            Nilai_Barang = @NilaiBarangBaru,   
            Total = @TotalBaru,
            Jumlah_Input = @JumlahInputBaru
        WHERE 
            Kode_Perusahaan = @KodePerusahaan
            AND No_Faktur = @NoPOInduk
            AND Kode_Barang = @KodeBarang
            AND No_Urut = @NoUrut
    "
            ExecuteTrans(SQL_Update_Det)

            ' UPDATE 2: Cek data sebelum update emi_pembelian_po_detail_induk
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)

            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM emi_pembelian_po_detail_induk
            WHERE 
                Kode_Perusahaan = @KodePerusahaan
                AND No_Faktur = @NoPOInduk
                AND Kode_Barang = @KodeBarang
        "
            Dim cekDetailInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())

            If cekDetailInduk = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
                MessageBox.Show($"Data detail induk PO tidak ditemukan untuk barang {KodeBarang}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Update PO Detail Induk (Aggregation)
            Dim SQL_Agg As String = "
        UPDATE emi_pembelian_po_detail_induk SET 
            Jumlah = (
                SELECT SUM(Jumlah) 
                FROM emi_pembelian_po_det_induk
                WHERE Kode_Perusahaan = @KodePerusahaan
                AND No_Faktur = @NoPOInduk
                AND Kode_Barang = @KodeBarang
            ),
            Nilai_Barang = (
                SELECT SUM(Nilai_Barang) 
                FROM emi_pembelian_po_det_induk 
                WHERE Kode_Perusahaan = @KodePerusahaan
                AND No_Faktur = @NoPOInduk
                AND Kode_Barang = @KodeBarang
            ),
            Total = (
                SELECT SUM(Total) 
                FROM emi_pembelian_po_det_induk 
                WHERE Kode_Perusahaan = @KodePerusahaan
                AND No_Faktur = @NoPOInduk
                AND Kode_Barang = @KodeBarang
            ),
            Jumlah_Input = (
                SELECT SUM(Jumlah_Input) 
                FROM emi_pembelian_po_det_induk 
                WHERE Kode_Perusahaan = @KodePerusahaan
                AND No_Faktur = @NoPOInduk
                AND Kode_Barang = @KodeBarang
            )
        WHERE 
            Kode_Perusahaan = @KodePerusahaan
            AND No_Faktur = @NoPOInduk
            AND Kode_Barang = @KodeBarang
    "
            ExecuteTrans(SQL_Agg)

            ' Get Grand Total & Pajak
            Cmd.CommandText = "
        SELECT ISNULL(SUM(epd.Total), 0) AS Total, epo.Mata_Uang, epo.PPN, epo.Kurs, epo.Total_MUA FROM emi_pembelian_po_detail_Induk epd 
        INNER JOIN Emi_Pembelian_PO_Induk epo ON epd.No_Faktur = epo.No_Faktur 
        WHERE epd.Kode_Perusahaan = @KodePerusahaan AND epd.No_Faktur = @NoPOInduk 
        GROUP BY Mata_Uang, PPN, Kurs, Total_MUA
    "
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)

            Dim grandTotal As Decimal = 0
            Dim persentasePPH As Decimal = 0
            Dim mataUang As String = ""
            Dim PPN As Decimal = 0
            Dim TotalMUA As Decimal = 0
            Dim Kurs As Decimal = 0

            Using reader = Cmd.ExecuteReader()
                If reader.Read() Then
                    grandTotal = Convert.ToDecimal(reader("Total"))
                    mataUang = reader("Mata_Uang").ToString()
                    PPN = Convert.ToDecimal(reader("PPN"))
                    Kurs = Convert.ToDecimal(reader("Kurs"))
                    TotalMUA = Convert.ToDecimal(reader("Total_MUA"))
                End If
            End Using

            Cmd.CommandText = "SELECT ISNULL(SUM(persentase), 0) AS Persentase_PPH FROM EMI_Detail_PPH_PO_Induk WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoPOInduk"
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)

            Using reader = Cmd.ExecuteReader()
                If reader.Read() Then
                    persentasePPH = CDec(reader("Persentase_PPH"))
                End If
            End Using

            Dim grandTotalSetelahPPN As Decimal = grandTotal + (grandTotal / 100 * PPN)
            Dim nilaiPPH As Decimal = (grandTotalSetelahPPN / 100 * persentasePPH)
            Dim grandTotalSetelahPPH As Decimal = grandTotalSetelahPPN - nilaiPPH
            Dim Total_MUA As Decimal = grandTotalSetelahPPH * Kurs

            ' ============================================
            ' 2. CEK APAKAH SEMUA BARANG SUDAH SELESAI
            ' ============================================
            Dim semuaBarangSelesai As Boolean = True
            Dim flagSelesaiSubPO As String = "NULL"

            ' Query untuk ambil semua barang di PO Induk beserta Qty PO Induk dan Qty SubPO
            Cmd.CommandText = "
        ;WITH SubPO AS (
            SELECT
                spo_d.Kode_Perusahaan,
                spo_d.No_FakInduk,
                spo_d.No_Urut_PR,
                spo_d.Kode_Barang,
                spo_d.Kode_Stock_Owner,
                SUM(spo_d.Jumlah) AS Total_Sub_PO
            FROM EMI_Pembelian_PO_Det spo_d
            INNER JOIN EMI_Pembelian_PO spo 
                ON spo.No_Faktur = spo_d.No_Faktur
                AND spo.Status IS NULL
                AND spo.Flag_Release = 'Y'
                AND spo.Kode_Perusahaan = spo_d.Kode_Perusahaan
            GROUP BY spo_d.Kode_Perusahaan, spo_d.No_FakInduk, spo_d.No_Urut_PR, spo_d.Kode_Barang, spo_d.Kode_Stock_Owner
        )
        SELECT 
            epd.Jumlah AS Qty_POInduk,
            ISNULL(s.Total_Sub_PO, 0) AS Qty_SubPO
        FROM emi_pembelian_po_det_induk epd
        LEFT JOIN SubPO s 
            ON s.No_FakInduk = epd.No_Faktur
            AND s.No_Urut_PR = epd.No_Urut_PR
            AND s.Kode_Barang = epd.Kode_Barang
            AND s.Kode_Perusahaan = epd.Kode_Perusahaan
        WHERE 
            epd.Kode_Perusahaan = @KodePerusahaan 
            AND epd.No_Faktur = @NoPOInduk
    "
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)

            Using reader = Cmd.ExecuteReader()
                While reader.Read()
                    Dim qtyPOIndukPerItem As Decimal = CDec(reader("Qty_POInduk"))
                    Dim qtySubPOPerItem As Decimal = CDec(reader("Qty_SubPO"))

                    ' Jika ada 1 item saja yang Qty_POInduk <> Qty_SubPO, berarti belum semua selesai
                    If Math.Round(qtyPOIndukPerItem, 2) <> Math.Round(qtySubPOPerItem, 2) Then
                        semuaBarangSelesai = False
                        Exit While
                    End If
                End While
            End Using

            ' Set flag berdasarkan hasil pengecekan
            If semuaBarangSelesai Then
                flagSelesaiSubPO = "'Y'"
            End If

            ' UPDATE 3: Cek data sebelum update Emi_Pembelian_PO_Induk
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)

            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM Emi_Pembelian_PO_Induk
            WHERE 
                Kode_Perusahaan = @KodePerusahaan 
                AND No_Faktur = @NoPOInduk
        "
            Dim cekPOIndukHeader As Integer = Convert.ToInt32(Cmd.ExecuteScalar())

            If cekPOIndukHeader = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
                MessageBox.Show($"Data PO Induk {NoPOInduk} tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Update Header PO Induk
            Cmd.Parameters.AddWithValue("@GrandSebelumPPN", grandTotal)
            Cmd.Parameters.AddWithValue("@GrandSetelahPPN", grandTotalSetelahPPN)
            Cmd.Parameters.AddWithValue("@GrandSetelahPPH", grandTotalSetelahPPH)
            Cmd.Parameters.AddWithValue("@TotalMUA", Total_MUA)
            Cmd.Parameters.AddWithValue("@GrandTotalTerbilang", General_Class.SayMUA(Math.Round(grandTotalSetelahPPH, 0), mataUang))

            Dim SQL_Update_Header As String = "
        UPDATE Emi_Pembelian_PO_Induk 
        SET 
            Flag_Selesai_SubPO = " & flagSelesaiSubPO & ",
            Total_MUA = @TotalMUA,
            Total_IDR = @GrandSebelumPPN,
            Grand_Sebelum_PPN = @GrandSebelumPPN,
            Grand = @GrandSetelahPPN,
            Grand_PPH = @GrandSetelahPPH,
            Grand_Total_Terbilang = @GrandTotalTerbilang 
        WHERE 
            Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoPOInduk
    "
            ExecuteTrans(SQL_Update_Header)

            ' UPDATE 4: Cek data sebelum update Emi_Purchase_Requisition_Detail
            If Not String.IsNullOrEmpty(NoPR) AndAlso Not String.IsNullOrEmpty(NoUrutPR) Then
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPR", NoPR)
                Cmd.Parameters.AddWithValue("@NoUrutPR", NoUrutPR)

                Cmd.CommandText = "
                SELECT COUNT(*) 
                FROM Emi_Purchase_Requisition_Detail
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan 
                    AND No_Faktur = @NoPR 
                    AND No_Urut = @NoUrutPR
            "
                Dim cekPR As Integer = Convert.ToInt32(Cmd.ExecuteScalar())

                If cekPR = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data PR tidak ditemukan untuk No. {NoPR} Urut {NoUrutPR}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                ' Update PR Detail
                Dim SQL_Update_PR As String = "UPDATE Emi_Purchase_Requisition_Detail SET Flag_Sudah_PO = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoPR AND No_Urut = @NoUrutPR"
                ExecuteTrans(SQL_Update_PR)
            End If

            Cmd.Transaction.Commit()

            CloseConn()
            Fetch_PO_Outstanding()
            If semuaBarangSelesai Then
                MessageBox.Show("Barang PO Induk berhasil diselesaikan." & vbCrLf & "Semua item di PO Induk ini sudah selesai.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Barang PO Induk berhasil diselesaikan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            If Cmd.Transaction IsNot Nothing Then
                Cmd.Transaction.Rollback()
            End If
            CloseTrans()
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SelesaikanPOInduk_Click(sender As Object, e As EventArgs) Handles SelesaikanPOInduk.Click
        If Guna2DataGridView2.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih data PO Induk terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row As DataGridViewRow = Guna2DataGridView2.SelectedRows(0)
        Dim NoPOInduk As String = row.Cells("NoPOInduk").Value?.ToString()

        If String.IsNullOrEmpty(NoPOInduk) Then
            MessageBox.Show("Data PO Induk tidak ditemukan.", "Error")
            Exit Sub
        End If

        Dim result As DialogResult = MessageBox.Show(
        $"Yakin ingin menyelesaikan SELURUH barang dalam PO Induk {NoPOInduk} ?",
        "Konfirmasi",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If result <> DialogResult.Yes Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Selesai_PO_Induk") = "T" Then
                MsgBox("Anda tidak memiliki akses menyelesaikan PO Induk.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            ' 1. Cek dan Backup EMI_Pembelian_PO_Induk ke EMI_Pembelian_PO_Induk_Origin
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Induk_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoPOInduk
        "
            Dim CekPOInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPOInduk = 0 Then
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
                Dim SQL_Backup_PO_Induk As String = "
                    INSERT INTO EMI_Pembelian_PO_Induk_Origin (
                        Kode_Perusahaan,
                        No_Faktur,
                        No_Nota,
                        Status,
                        Tanggal,
                        Jam,
                        UserID,
                        Kode_Supplier,
                        Lokasi,
                        Jenis_Pembayaran,
                        Cara_Bayar,
                        Tgl_Jatuh_Tempo,
                        Total_MUA,
                        Mata_Uang,
                        Kurs,
                        Total_IDR,
                        Grand_Sebelum_PPN,
                        PPN,
                        Grand,
                        ETD_Simulasi,
                        No_Prepare_Bahan,
                        Flag_Lokasi_Tujuan,
                        Flag_ETD,
                        ETD,
                        Flag_Loading,
                        Flag_Timbang,
                        Flag_QC,
                        Flag_BM,
                        Flag_Val_BM,
                        Flag_Selisih_BM,
                        Flag_Val_Selisih_BM,
                        Flag_Pembelian,
                        Selesai,
                        Ekspedisi,
                        Biaya,
                        Flag_Release,
                        Tanggal_Release,
                        Jam_Release,
                        User_Release,
                        flag_sudah_pindah,
                        Jumlah_Print,
                        Flag_Import,
                        Tempo_Pembayaran,
                        Flag_Selesai_PO,
                        Flag_Biaya,
                        Lama_Pembayaran,
                        Grand_Total_Terbilang,
                        Flag_Selesai_SubPO,
                        Grand_PPH,
                        UserID_Batal,
                        Tanggal_Batal,
                        Jam_Batal
                        --UserID_Selesai,
                        --Tanggal_Selesai,
                        --Jam_Selesai
                    )
                    SELECT
                        Kode_Perusahaan,
                        No_Faktur,
                        No_Nota,
                        Status,
                        Tanggal,
                        Jam,
                        UserID,
                        Kode_Supplier,
                        Lokasi,
                        Jenis_Pembayaran,
                        Cara_Bayar,
                        Tgl_Jatuh_Tempo,
                        Total_MUA,
                        Mata_Uang,
                        Kurs,
                        Total_IDR,
                        Grand_Sebelum_PPN,
                        PPN,
                        Grand,
                        ETD_Simulasi,
                        No_Prepare_Bahan,
                        Flag_Lokasi_Tujuan,
                        Flag_ETD,
                        ETD,
                        Flag_Loading,
                        Flag_Timbang,
                        Flag_QC,
                        Flag_BM,
                        Flag_Val_BM,
                        Flag_Selisih_BM,
                        Flag_Val_Selisih_BM,
                        Flag_Pembelian,
                        Selesai,
                        Ekspedisi,
                        Biaya,
                        Flag_Release,
                        Tanggal_Release,
                        Jam_Release,
                        User_Release,
                        flag_sudah_pindah,
                        Jumlah_Print,
                        Flag_Import,
                        Tempo_Pembayaran,
                        Flag_Selesai_PO,
                        Flag_Biaya,
                        Lama_Pembayaran,
                        Grand_Total_Terbilang,
                        Flag_Selesai_SubPO,
                        Grand_PPH,
                        UserID_Batal,
                        Tanggal_Batal,
                        Jam_Batal
                        --UserID_Selesai,
                        --Tanggal_Selesai,
                        --Jam_Selesai
                    FROM EMI_Pembelian_PO_Induk
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoPOInduk
                    "

                ExecuteTrans(SQL_Backup_PO_Induk)
            End If

            ' 2. Cek dan Backup EMI_Pembelian_PO_Det_Induk ke EMI_Pembelian_PO_Det_Induk_Origin
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Det_Induk_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoPOInduk
        "
            Dim CekPODetInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODetInduk = 0 Then
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
                Dim SQL_Backup_PO_Det_Induk As String = "
                    INSERT INTO EMI_Pembelian_PO_Det_Induk_Origin (
                        Kode_Perusahaan,
                        No_Faktur,
                        Kode_Stock_Owner,
                        Kode_Barang,
                        No_Urut,
                        Jumlah,
                        Satuan,
                        Harga,
                        Nilai_Barang,
                        Satuan_Barang,
                        Harga_Barang,
                        Total,
                        No_Penawaran,
                        No_Urut_PR,
                        Flag_Selesai,
                        Jumlah_Input,
                        Satuan_Input
                    )
                    SELECT
                        Kode_Perusahaan,
                        No_Faktur,
                        Kode_Stock_Owner,
                        Kode_Barang,
                        No_Urut,
                        Jumlah,
                        Satuan,
                        Harga,
                        Nilai_Barang,
                        Satuan_Barang,
                        Harga_Barang,
                        Total,
                        No_Penawaran,
                        No_Urut_PR,
                        Flag_Selesai,
                        Jumlah_Input,
                        Satuan_Input
                    FROM EMI_Pembelian_PO_Det_Induk
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoPOInduk
                    "

                ExecuteTrans(SQL_Backup_PO_Det_Induk)
            End If

            ' 3. Cek dan Backup EMI_Pembelian_PO_Detail_Induk ke EMI_Pembelian_PO_Detail_Induk_Origin
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Detail_Induk_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoPOInduk
        "
            Dim CekPODetailInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODetailInduk = 0 Then
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
                Dim SQL_Backup_PO_Detail_Induk As String = "
                    INSERT INTO EMI_Pembelian_PO_Detail_Induk_Origin (
                        Kode_Perusahaan,
                        No_Faktur,
                        Kode_Stock_Owner,
                        Kode_Barang,
                        No_Urut,
                        Jumlah,
                        Harga,
                        Satuan,
                        Jumlah_Masuk,
                        Nilai_Barang,
                        Harga_Barang,
                        Satuan_Barang,
                        Total,
                        No_Penawaran,
                        Flag_Prepare,
                        Flag_Loading,
                        Flag_Timbang,
                        Flag_Refraksi,
                        Flag_Permintaan_Refraksi,
                        Harga_Refraksi,
                        Jumlah_Input,
                        Satuan_Input
                    )
                    SELECT
                        Kode_Perusahaan,
                        No_Faktur,
                        Kode_Stock_Owner,
                        Kode_Barang,
                        No_Urut,
                        Jumlah,
                        Harga,
                        Satuan,
                        Jumlah_Masuk,
                        Nilai_Barang,
                        Harga_Barang,
                        Satuan_Barang,
                        Total,
                        No_Penawaran,
                        Flag_Prepare,
                        Flag_Loading,
                        Flag_Timbang,
                        Flag_Refraksi,
                        Flag_Permintaan_Refraksi,
                        Harga_Refraksi,
                        Jumlah_Input,
                        Satuan_Input
                    FROM EMI_Pembelian_PO_Detail_Induk
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoPOInduk
                    "

                ExecuteTrans(SQL_Backup_PO_Detail_Induk)
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)

            Dim SQL As String = "
            ;WITH SubPO AS (
                SELECT
                    spo_d.Kode_Perusahaan,
                    spo_d.No_FakInduk,
                    spo_d.No_Urut_PR,
                    spo_d.Kode_Barang,
                    spo_d.Kode_Stock_Owner,
                    SUM(spo_d.Jumlah) AS Total_Sub_PO
                FROM EMI_Pembelian_PO_Det spo_d
                INNER JOIN EMI_Pembelian_PO spo 
                    ON spo.No_Faktur = spo_d.No_Faktur
                    AND spo.Status IS NULL
                    AND spo.Flag_Release = 'Y'
                    AND spo.Kode_Perusahaan = spo_d.Kode_Perusahaan
                GROUP BY spo_d.Kode_Perusahaan, spo_d.No_FakInduk, spo_d.No_Urut_PR, spo_d.Kode_Barang, spo_d.Kode_Stock_Owner
            )
            SELECT 
                epd.Kode_Barang,
                epd.No_Urut,
                epd.Jumlah AS Qty_POInduk,
                ISNULL(s.Total_Sub_PO, 0) AS Qty_SubPO,
                epd.Nilai_Barang,
                epd.Harga_Barang,
                epd.Jumlah_Input,
                epd.No_Penawaran,
                epd.No_Urut_PR,
                pr.No_Faktur AS NoPR
            FROM emi_pembelian_po_det_induk epd
            LEFT JOIN SubPO s 
                ON s.No_FakInduk = epd.No_Faktur
                AND s.No_Urut_PR = epd.No_Urut_PR
                AND s.Kode_Barang = epd.Kode_Barang
                AND s.Kode_Perusahaan = epd.Kode_Perusahaan
            LEFT JOIN Emi_Purchase_Requisition_Detail pr
                ON pr.No_Urut = epd.No_Urut_PR
                AND pr.Kode_Barang = epd.Kode_Barang
            WHERE 
                epd.Kode_Perusahaan = @KodePerusahaan 
                AND epd.No_Faktur = @NoPOInduk
        "

            Cmd.CommandText = SQL
            Dim dtDetail As New DataTable
            Using adapter = New SqlDataAdapter(Cmd)
                adapter.Fill(dtDetail)
            End Using

            For Each detRow As DataRow In dtDetail.Rows
                Dim KodeBarang As String = detRow("Kode_Barang").ToString()
                Dim NoUrut As String = detRow("No_Urut").ToString()
                Dim QtyPOInduk As Decimal = CDec(detRow("Qty_POInduk"))
                Dim QtySubPO As Decimal = CDec(detRow("Qty_SubPO"))
                Dim NilaiBarang As Decimal = CDec(detRow("Nilai_Barang"))
                Dim HargaBarang As Decimal = CDec(detRow("Harga_Barang"))
                Dim JumlahInput As Decimal = CDec(detRow("Jumlah_Input"))
                Dim NoUrutPR As String = detRow("No_Urut_PR").ToString()
                Dim NoPR As String = detRow("NoPR").ToString()

                Dim NilaiBarangBaru As Decimal = (NilaiBarang / QtyPOInduk) * QtySubPO
                Dim JumlahInputBaru As Decimal = (JumlahInput / QtyPOInduk) * QtySubPO
                Dim TotalBaru As Decimal = QtySubPO * HargaBarang

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@Log_UserID", UserID)
                Cmd.Parameters.AddWithValue("@Log_Tanggal", Date.Now())
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

                SQL = "
                INSERT INTO Emi_Pembelian_PO_Det_Induk_Log (
                    Kode_Perusahaan,
                    No_Faktur,
                    Kode_Stock_Owner,
                    Kode_Barang,
                    No_Urut,
                    Jumlah,
                    Satuan,
                    Harga,
                    Nilai_Barang,
                    Satuan_Barang,
                    Harga_Barang,
                    Total,
                    No_Penawaran,
                    No_Urut_PR,
                    Flag_Selesai,
                    Jumlah_Input,
                    Satuan_Input,
                    Log_UserID,
                    Log_Tanggal
                )
                SELECT
                    Kode_Perusahaan,
                    No_Faktur,
                    Kode_Stock_Owner,
                    Kode_Barang,
                    No_Urut,
                    Jumlah,
                    Satuan,
                    Harga,
                    Nilai_Barang,
                    Satuan_Barang,
                    Harga_Barang,
                    Total,
                    No_Penawaran,
                    No_Urut_PR,
                    Flag_Selesai,
                    Jumlah_Input,
                    Satuan_Input,
                    @Log_UserID,
                    @Log_Tanggal
                FROM emi_pembelian_po_det_induk
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan 
                    AND No_Faktur = @NoPOInduk 
                    AND Kode_Barang = @KodeBarang
                    AND No_Urut = @NoUrut
            "
                ExecuteTrans(SQL)

                ' UPDATE 1: Cek data sebelum update emi_pembelian_po_det_induk
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

                Cmd.CommandText = "
                SELECT COUNT(*) 
                FROM emi_pembelian_po_det_induk
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoPOInduk
                    AND Kode_Barang = @KodeBarang
                    AND No_Urut = @NoUrut
            "
                Dim cekDetInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())

                If cekDetInduk = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data detail PO Induk tidak ditemukan untuk barang {KodeBarang} No {NoUrut}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@QtySubPO", QtySubPO)
                Cmd.Parameters.AddWithValue("@NilaiBarangBaru", NilaiBarangBaru)
                Cmd.Parameters.AddWithValue("@JumlahInputBaru", JumlahInputBaru)
                Cmd.Parameters.AddWithValue("@TotalBaru", TotalBaru)
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

                SQL = "
                UPDATE emi_pembelian_po_det_induk SET 
                    Flag_Selesai = 'Y',
                    Jumlah = @QtySubPO,
                    Nilai_Barang = @NilaiBarangBaru,   
                    Total = @TotalBaru,
                    Jumlah_Input = @JumlahInputBaru
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoPOInduk
                    AND Kode_Barang = @KodeBarang
                    AND No_Urut = @NoUrut
            "
                ExecuteTrans(SQL)

                'UPDATE 2: Cek data sebelum update emi_pembelian_po_detail_induk
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)

                Cmd.CommandText = "
                SELECT COUNT(*) 
                FROM emi_pembelian_po_detail_induk
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoPOInduk
                    AND Kode_Barang = @KodeBarang
            "
                Dim cekDetailInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())

                If cekDetailInduk = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data detail induk PO tidak ditemukan untuk barang {KodeBarang}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                SQL = "
                UPDATE emi_pembelian_po_detail_induk SET 
                    Jumlah = (
                        SELECT SUM(Jumlah) 
                        FROM emi_pembelian_po_det_induk 
                        WHERE Kode_Perusahaan = @KodePerusahaan
                        AND No_Faktur = @NoPOInduk
                        AND Kode_Barang = @KodeBarang
                    ),
                    Nilai_Barang = (
                        SELECT SUM(Nilai_Barang) 
                        FROM emi_pembelian_po_det_induk 
                        WHERE Kode_Perusahaan = @KodePerusahaan
                        AND No_Faktur = @NoPOInduk
                        AND Kode_Barang = @KodeBarang
                    ),
                    Total = (
                        SELECT SUM(Total) 
                        FROM emi_pembelian_po_det_induk 
                        WHERE Kode_Perusahaan = @KodePerusahaan
                        AND No_Faktur = @NoPOInduk
                        AND Kode_Barang = @KodeBarang
                    ),
                    Jumlah_Input = (
                        SELECT SUM(Jumlah_Input) 
                        FROM emi_pembelian_po_det_induk 
                        WHERE Kode_Perusahaan = @KodePerusahaan
                        AND No_Faktur = @NoPOInduk
                        AND Kode_Barang = @KodeBarang
                    )
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoPOInduk
                    AND Kode_Barang = @KodeBarang
            "
                ExecuteTrans(SQL)

                ' UPDATE 3: Cek data sebelum update Emi_Purchase_Requisition_Detail
                If Not String.IsNullOrEmpty(NoPR) AndAlso Not String.IsNullOrEmpty(NoUrutPR) Then
                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                    Cmd.Parameters.AddWithValue("@NoPR", NoPR)
                    Cmd.Parameters.AddWithValue("@NoUrutPR", NoUrutPR)

                    Cmd.CommandText = "
                    SELECT COUNT(*) 
                    FROM Emi_Purchase_Requisition_Detail
                    WHERE 
                        Kode_Perusahaan = @KodePerusahaan 
                        AND No_Faktur = @NoPR 
                        AND No_Urut = @NoUrutPR
                "
                    Dim cekPR As Integer = Convert.ToInt32(Cmd.ExecuteScalar())

                    If cekPR = 0 Then
                        Cmd.Transaction.Rollback()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Data PR tidak ditemukan untuk No PR {NoPR} No {NoUrutPR}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    SQL = "UPDATE Emi_Purchase_Requisition_Detail SET Flag_Sudah_PO = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoPR AND No_Urut = @NoUrutPR"
                    ExecuteTrans(SQL)
                End If
            Next

            Dim grandTotal As Decimal = 0
            Dim persentasePPH As Decimal = 0
            Dim mataUang As String = ""
            Dim PPN As Decimal = 0
            Dim Kurs As Decimal = 0
            Dim TotalMUA As Decimal = 0

            Cmd.CommandText = "
            SELECT ISNULL(SUM(epd.Total), 0) AS Total, 
                   epo.Mata_Uang, 
                   epo.PPN, 
                   epo.Kurs, 
                   epo.Total_MUA 
            FROM emi_pembelian_po_detail_induk epd 
            INNER JOIN Emi_Pembelian_PO_Induk epo ON epd.No_Faktur = epo.No_Faktur 
            WHERE epd.Kode_Perusahaan = @KodePerusahaan 
            AND epd.No_Faktur = @NoPOInduk 
            GROUP BY epo.Mata_Uang, epo.PPN, epo.Kurs, epo.Total_MUA
        "
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)

            Using reader = Cmd.ExecuteReader()
                If reader.Read() Then
                    grandTotal = Convert.ToDecimal(reader("Total"))
                    mataUang = reader("Mata_Uang").ToString()
                    PPN = Convert.ToDecimal(reader("PPN"))
                    Kurs = Convert.ToDecimal(reader("Kurs"))
                    TotalMUA = Convert.ToDecimal(reader("Total_MUA"))
                End If
            End Using

            Cmd.CommandText = "SELECT ISNULL(SUM(persentase), 0) AS Persentase_PPH FROM EMI_Detail_PPH_PO_Induk WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoPOInduk"
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)

            Using reader = Cmd.ExecuteReader()
                If reader.Read() Then
                    persentasePPH = CDec(reader("Persentase_PPH"))
                End If
            End Using

            Dim grandTotalSetelahPPN As Decimal = grandTotal + (grandTotal / 100 * PPN)
            Dim nilaiPPH As Decimal = (grandTotalSetelahPPN / 100 * persentasePPH)
            Dim grandTotalSetelahPPH As Decimal = grandTotalSetelahPPN - nilaiPPH
            Dim Total_MUA As Decimal = grandTotal * Kurs

            ' UPDATE 4: Cek data sebelum update Emi_Pembelian_PO_Induk
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoPOInduk", NoPOInduk)

            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM Emi_Pembelian_PO_Induk
            WHERE 
                Kode_Perusahaan = @KodePerusahaan 
                AND No_Faktur = @NoPOInduk
        "
            Dim cekPOIndukFinal As Integer = Convert.ToInt32(Cmd.ExecuteScalar())

            If cekPOIndukFinal = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
                MessageBox.Show($"Data PO Induk {NoPOInduk} tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.AddWithValue("@GrandSebelumPPN", grandTotal)
            Cmd.Parameters.AddWithValue("@GrandSetelahPPN", grandTotalSetelahPPN)
            Cmd.Parameters.AddWithValue("@GrandSetelahPPH", grandTotalSetelahPPH)
            Cmd.Parameters.AddWithValue("@TotalMUA", Total_MUA)
            Cmd.Parameters.AddWithValue("@GrandTotalTerbilang", General_Class.SayMUA(Math.Round(grandTotalSetelahPPH, 0), mataUang))

            SQL = "
            UPDATE Emi_Pembelian_PO_Induk 
            SET 
                Flag_Selesai_SubPO = 'Y',
                Total_MUA = @TotalMUA,
                Total_IDR = @GrandSebelumPPN,
                Grand_Sebelum_PPN = @GrandSebelumPPN,
                Grand = @GrandSetelahPPN,
                Grand_PPH = @GrandSetelahPPH,
                Grand_Total_Terbilang = @GrandTotalTerbilang 
            WHERE 
                Kode_Perusahaan = @KodePerusahaan 
                AND No_Faktur = @NoPOInduk
        "
            ExecuteTrans(SQL)
            Cmd.Transaction.Commit()
            CloseConn()
            Fetch_PO_Outstanding()
            MessageBox.Show($"PO Induk {NoPOInduk} berhasil diselesaikan untuk SELURUH barang.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
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