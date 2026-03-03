Imports Guna.Charts.WinForms

Public Class N_EMI_Dashboard_Procurment
    Private startDate As New Date(Date.Today.Year, Date.Today.Month, 1)
    Private endDate As Date = startDate.AddMonths(1).AddDays(-1)
    Private mountStartIndex As Integer = 0
    Private mountEndIndex As Integer = 11

    Private IDGroupJenis As Integer = 0

    Private searchTimer As Timer
    Private Const DEBOUNCE_DELAY As Integer = 300
    Private Const MIN_SEARCH_LENGTH As Integer = 3

    Private Const CUT_OFF_DATE As String = "2025-08-01"

    Private lastSearchType As String = ""
    Private monthCTE As String = "
        WITH BulanCTE AS (
            SELECT 0 AS OffsetMonth, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1) AS BulanAwal
            UNION ALL
            SELECT OffsetMonth + 1, DATEADD(MONTH, -(OffsetMonth + 1), DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
            FROM BulanCTE
            WHERE OffsetMonth + 1 < 12
        )
    "
    Private Sub PR_Outstanding_Click(sender As Object, e As EventArgs) Handles PR_Outstanding.Click
        Dim SD As New N_EMI_SD_Procurement_PR With {
            .StartPosition = FormStartPosition.CenterScreen,
            .IDGroupJenis = IDGroupJenis
        }

        SD.ShowDialog()
    End Sub

    Private Sub Fetch_ProcurmentChart()
        Try
            OpenConn()

            Dim GetProcurmentHistoricalQuery = Function(table As String, additionalCondition As String) $"
                {monthCTE}
                SELECT 
                    FORMAT(b.BulanAwal, 'MMM') + ' ' + CAST(YEAR(b.BulanAwal) AS VARCHAR) AS Label,
                    ISNULL(COUNT(t.No_Faktur), 0) AS Value
                FROM BulanCTE b
                LEFT JOIN {table} t
                    ON YEAR(t.Tanggal_Release) = YEAR(b.BulanAwal)
                    AND MONTH(t.Tanggal_Release) = MONTH(b.BulanAwal)
                    AND t.Kode_Perusahaan = '{KodePerusahaan}'
                    AND t.Status IS NULL
                    AND t.Flag_Release = 'Y'
                    {additionalCondition}
                WHERE b.OffsetMonth BETWEEN {mountStartIndex} AND {mountEndIndex}
                GROUP BY b.OffsetMonth, b.BulanAwal
                ORDER BY b.OffsetMonth DESC;
            "

            Dim GetDOHistoricalQuery = $"
                {monthCTE}
                SELECT 
                    FORMAT(b.BulanAwal, 'MMM') + ' ' + CAST(YEAR(b.BulanAwal) AS VARCHAR) AS Label,
                    ISNULL(COUNT(t.No_Faktur), 0) AS Value
                FROM BulanCTE b
                LEFT JOIN EMI_Pembelian_Loading t
                    ON YEAR(t.Tanggal_OTW) = YEAR(b.BulanAwal)
                    AND MONTH(t.Tanggal_OTW) = MONTH(b.BulanAwal)
                    AND t.Kode_Perusahaan = '{KodePerusahaan}'
                    AND t.Status IS NULL
                WHERE b.OffsetMonth BETWEEN {mountStartIndex} AND {mountEndIndex}
                GROUP BY b.OffsetMonth, b.BulanAwal
                ORDER BY b.OffsetMonth DESC;
            "

            GunaChart1.Datasets.Clear()
            GunaChart1.Legend.Display = True

            With GunaChart1
                .XAxes.GridLines.Display = True
                .YAxes.GridLines.Display = True
                .Legend.Position = Guna.Charts.WinForms.LegendPosition.Top
            End With

            Dim PRSplineDataset = New GunaSplineDataset With {
                .Label = "PR",
                .BorderWidth = 2,
                .PointBorderWidth = 1,
                .PointRadius = 4,
                .BorderColor = Color.FromArgb(75, 192, 192),
                .FillColor = Color.FromArgb(30, 75, 192, 192),
                .PointStyle = Guna.Charts.WinForms.PointStyle.Circle
            }

            Dim POSplineDataset = New GunaSplineDataset With {
                .Label = "PO",
                .BorderWidth = 2,
                .PointBorderWidth = 1,
                .PointRadius = 4,
                .BorderColor = Color.FromArgb(255, 99, 132),
                .FillColor = Color.FromArgb(30, 255, 99, 132),
                .PointStyle = Guna.Charts.WinForms.PointStyle.Circle
            }

            Dim SubPOSplineDataset = New GunaSplineDataset With {
                .Label = "Sub PO",
                .BorderWidth = 2,
                .PointBorderWidth = 1,
                .PointRadius = 4,
                .BorderColor = Color.FromArgb(54, 162, 235),
                .FillColor = Color.FromArgb(30, 54, 162, 235),
                .PointStyle = Guna.Charts.WinForms.PointStyle.Circle
            }

            Dim DOSplineDataset = New GunaSplineDataset With {
                .Label = "DO",
                .BorderWidth = 2,
                .PointBorderWidth = 1,
                .PointRadius = 4,
                .BorderColor = Color.FromArgb(255, 159, 64),
                .FillColor = Color.FromArgb(30, 255, 159, 64),
                .PointStyle = Guna.Charts.WinForms.PointStyle.Circle
            }

            Dim OfferSplineDataset = New GunaSplineDataset With {
                .Label = "Offer",
                .BorderWidth = 2,
                .PointBorderWidth = 1,
                .PointRadius = 4,
                .BorderColor = Color.FromArgb(255, 206, 86),
                .FillColor = Color.FromArgb(30, 255, 206, 86),
                .PointStyle = Guna.Charts.WinForms.PointStyle.Circle
            }

            PRSplineDataset.DataPoints.Clear()
            POSplineDataset.DataPoints.Clear()
            SubPOSplineDataset.DataPoints.Clear()
            DOSplineDataset.DataPoints.Clear()
            OfferSplineDataset.DataPoints.Clear()

            Using Dr = OpenTrans(GetProcurmentHistoricalQuery("EMI_Purchase_Requisition", ""))
                While Dr.Read()
                    PRSplineDataset.DataPoints.Add(Dr("Label").ToString(), Convert.ToDecimal(Dr("Value")))
                End While
            End Using

            Using Dr = OpenTrans(GetProcurmentHistoricalQuery("EMI_Pembelian_PO_Induk", ""))
                While Dr.Read()
                    POSplineDataset.DataPoints.Add(Dr("Label").ToString(), Convert.ToDecimal(Dr("Value")))
                End While
            End Using

            Using Dr = OpenTrans(GetProcurmentHistoricalQuery("EMI_Pembelian_PO", ""))
                While Dr.Read()
                    SubPOSplineDataset.DataPoints.Add(Dr("Label").ToString(), Convert.ToDecimal(Dr("Value")))
                End While
            End Using

            Using Dr = OpenTrans(GetDOHistoricalQuery)
                While Dr.Read()
                    DOSplineDataset.DataPoints.Add(Dr("Label").ToString(), Convert.ToDecimal(Dr("Value")))
                End While
            End Using

            Using Dr = OpenTrans(GetProcurmentHistoricalQuery("EMI_Master_Penawaran", ""))
                While Dr.Read()
                    OfferSplineDataset.DataPoints.Add(Dr("Label").ToString(), Convert.ToDecimal(Dr("Value")))
                End While
            End Using

            GunaChart1.Datasets.Add(PRSplineDataset)
            GunaChart1.Datasets.Add(POSplineDataset)
            GunaChart1.Datasets.Add(SubPOSplineDataset)
            GunaChart1.Datasets.Add(DOSplineDataset)
            GunaChart1.Datasets.Add(OfferSplineDataset)
            GunaChart1.Update()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_PurchaseValueChart()
        Try
            OpenConn()

            Dim GetPOValueHistoricalQuery = $"
                {monthCTE}
                SELECT 
                    FORMAT(b.BulanAwal, 'MMM') + ' ' + CAST(YEAR(b.BulanAwal) AS VARCHAR) AS Label,
                    CAST(ISNULL(SUM(t.Grand_Sebelum_PPN), 0) AS DECIMAL(18,2)) AS Value
                FROM BulanCTE b
                LEFT JOIN EMI_Pembelian_PO t
                    ON YEAR(t.Tanggal_Release) = YEAR(b.BulanAwal)
                    AND MONTH(t.Tanggal_Release) = MONTH(b.BulanAwal)
                    AND t.Status IS NULL
                    AND t.Kode_Perusahaan = '{KodePerusahaan}'
                WHERE b.OffsetMonth BETWEEN {mountStartIndex} AND {mountEndIndex}
                GROUP BY b.OffsetMonth, b.BulanAwal
                ORDER BY b.OffsetMonth DESC;
            "

            GunaChart2.Datasets.Clear()
            GunaChart2.Legend.Display = True

            With GunaChart2
                .XAxes.GridLines.Display = True
                .YAxes.GridLines.Display = True
                .Legend.Position = Guna.Charts.WinForms.LegendPosition.Top
            End With

            Dim POValueSplineDataset = New GunaSplineAreaDataset With {
                .Label = "Nilai Pembelian",
                .BorderWidth = 2,
                .PointBorderWidth = 1,
                .PointRadius = 4,
                .BorderColor = Color.FromArgb(255, 159, 64),
                .FillColor = Color.FromArgb(30, 255, 159, 64),
                .PointStyle = Guna.Charts.WinForms.PointStyle.Circle,
                .YFormat = "'Rp' #,##0"
            }

            POValueSplineDataset.DataPoints.Clear()

            Using Dr = OpenTrans(GetPOValueHistoricalQuery)
                While Dr.Read()
                    POValueSplineDataset.DataPoints.Add(Dr("Label").ToString(), Convert.ToDecimal(Dr("Value")))
                End While
            End Using

            GunaChart2.Datasets.Add(POValueSplineDataset)
            GunaChart2.Update()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub N_EMI_Dashboard_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill

        searchTimer = New Timer With {
            .Interval = DEBOUNCE_DELAY
        }

        AddHandler searchTimer.Tick, AddressOf OnSearchTimerTick
        AddHandler TB_SearchRequestedItems.TextChanged, AddressOf TB_SearchRequestedItems_TextChanged
        AddHandler TB_SearchLeadTime.TextChanged, AddressOf TB_SearchLeadTime_TextChanged
        AddHandler Guna2TextBox1.TextChanged, AddressOf Guna2TextBox1_TextChanged

        Guna2DateTimePicker2.Value = DateTime.Today

        Try
            OpenConn()
            If CekButtonRole("Tampil_Offer_Dashboard_Procurment") = "T" Then
                Guna2Panel2.Visible = False
            End If

            OpenConn()
            If CekButtonRole("Tampil_Purchase_Value_Dashboard_Procurment") = "T" Then
                Guna2Panel4.Visible = False

                Guna2Panel9.Visible = False

                Guna2Panel7.Size = New Size(1123, 293)
                Guna2Panel7.Location = New Point(6, 6)
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        Fetch_Group_Jenis()
        Fetch_PR_Outstanding()
        Fetch_PO_Outstanding()
        Fetch_Sub_PO_Outstanding()
        Fetch_DO_Outstanding()
        Fetch_Offer_Outstanding()
        Fetch_Purchase_Value_Outstanding()
        Fetch_ProcurmentChart()
        Fetch_PurchaseValueChart()
        Fetch_Requested_Items()
        Fetch_Selisih_Penerimaan()
        Fetch_LeadTime()
        Fetch_Top10RequestedItems()
        Fetch_Top10Suppliers()
    End Sub

    Private Sub Fetch_Group_Jenis()
        Try
            OpenConn()

            SQL = $"Select Kode_Group_Jenis, Id_Group_Jenis From EMI_Group_Jenis Where Kode_Perusahaan = '{KodePerusahaan}' and Flag_PO='Y' Order By Kode_Group_Jenis ASC"


            Dim Ds As DataSet = BindingTrans(SQL)

            Dim dt As DataTable

            If Ds IsNot Nothing AndAlso Ds.Tables.Count > 0 Then
                dt = Ds.Tables(0)
            Else
                dt = New DataTable()
                dt.Columns.Add("Kode_Group_Jenis")
                dt.Columns.Add("Id_Group_Jenis")
            End If

            Dim dr As DataRow = dt.NewRow()
            dr("Kode_Group_Jenis") = "-- SEMUA DATA --"
            dr("Id_Group_Jenis") = 0
            dt.Rows.InsertAt(dr, 0)

            CbGroupJenis.DataSource = dt
            CbGroupJenis.DisplayMember = "Kode_Group_Jenis"
            CbGroupJenis.ValueMember = "Id_Group_Jenis"
            CbGroupJenis.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error Fetch_Grup_Jenis: " & ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Private Sub CbGrupJenis_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CbGroupJenis.SelectionChangeCommitted
        If CbGroupJenis.SelectedValue IsNot Nothing Then
            Dim newID As Integer = CInt(CbGroupJenis.SelectedValue)

            If IDGroupJenis <> newID Then
                CbGroupJenis.Enabled = False

                IDGroupJenis = newID

                Fetch_Requested_Items()
                Fetch_Top10RequestedItems()
                Fetch_Selisih_Penerimaan()
                Fetch_LeadTime()
                Fetch_PR_Outstanding()
                Fetch_PO_Outstanding()
                Fetch_Sub_PO_Outstanding()
                Fetch_DO_Outstanding()
                Fetch_Offer_Outstanding()

                CbGroupJenis.Enabled = True
            End If
        End If
    End Sub

    Private Sub TB_SearchRequestedItems_TextChanged(sender As Object, e As EventArgs)
        searchTimer.Stop()

        Dim searchText As String = TB_SearchRequestedItems.Text.Trim()

        If String.IsNullOrEmpty(searchText) Then
            Fetch_Requested_Items()
            Return
        End If

        If searchText.Length < MIN_SEARCH_LENGTH Then
            Return
        End If

        lastSearchType = "RequestedItems"
        searchTimer.Start()
    End Sub

    Private Sub TB_SearchLeadTime_TextChanged(sender As Object, e As EventArgs)
        searchTimer.Stop()

        Dim searchText As String = TB_SearchLeadTime.Text.Trim()

        If String.IsNullOrEmpty(searchText) Then
            Fetch_LeadTime()
            Return
        End If

        If searchText.Length < MIN_SEARCH_LENGTH Then
            Return
        End If

        lastSearchType = "LeadTime"
        searchTimer.Start()
    End Sub


    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs)
        searchTimer.Stop()

        Dim searchText As String = TB_SearchRequestedItems.Text.Trim()

        If String.IsNullOrEmpty(searchText) Then
            Fetch_Selisih_Penerimaan()
            Return
        End If

        If searchText.Length < MIN_SEARCH_LENGTH Then
            Return
        End If

        lastSearchType = "SelisihPenerimaan"
        searchTimer.Start()
    End Sub

    Private Sub OnSearchTimerTick(sender As Object, e As EventArgs)
        searchTimer.Stop()

        Select Case lastSearchType
            Case "RequestedItems"
                Fetch_Requested_Items()
            Case "SelisihPenerimaan"
                Fetch_Selisih_Penerimaan()
            Case "LeadTime"
                Fetch_LeadTime()
        End Select
    End Sub

    Private Sub Fetch_Top10RequestedItems()
        Try
            If Guna2DataGridView3 Is Nothing OrElse Guna2DataGridView3.Columns.Count = 0 Then
                Return
            End If

            OpenConn()

            Guna2DataGridView3.Rows.Clear()
            Guna2DataGridView3.RowHeadersVisible = True
            Guna2DataGridView3.RowHeadersWidth = 30
            Guna2DataGridView3.AllowUserToResizeRows = False
            Guna2DataGridView3.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            Dim Query = $"
                SELECT TOP 10
                    prd.Kode_Barang,
                    b.Nama AS Nama_Barang,
                    SUM(prd.Jumlah) AS Qty
                FROM EMI_Purchase_Requisition pr
                INNER JOIN EMI_Purchase_Requisition_Detail prd
                    ON pr.No_Faktur = prd.No_Faktur
                INNER JOIN Barang b
                    ON prd.Kode_Barang = b.Kode_Barang
                    AND prd.Kode_Stock_Owner = b.Kode_Stock_Owner
                WHERE pr.Status IS NULL
                    AND pr.Kode_Perusahaan = '{KodePerusahaan}'
                    AND pr.Flag_Release = 'Y'
                    AND pr.Tanggal_Release >= '{CUT_OFF_DATE}'
                    {If(IDGroupJenis <> 0, $"AND b.Id_Group_Jenis = {IDGroupJenis}", "")}
                GROUP BY
                    prd.Kode_Barang,
                    b.Nama
                ORDER BY
                    Qty DESC;
            "

            Using Dr = OpenTrans(Query)
                While Dr.Read()
                    Guna2DataGridView3.Rows.Add(
                        Dr("Kode_Barang"),
                        Dr("Nama_Barang"),
                        Dr("Qty")
                    )
                End While
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_Top10Suppliers()
        Try
            If Guna2DataGridView4 Is Nothing OrElse Guna2DataGridView4.Columns.Count = 0 Then
                Return
            End If

            OpenConn()

            Guna2DataGridView4.Rows.Clear()
            Guna2DataGridView4.RowHeadersVisible = True
            Guna2DataGridView4.RowHeadersWidth = 30
            Guna2DataGridView4.AllowUserToResizeRows = False
            Guna2DataGridView4.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            Dim Query = $"
                SELECT TOP 10
                    b.Kode_Supplier,
                    b.Nama,
                    AVG(DATEDIFF(DAY, a.tanggal_otw, a.tanggal_masuk)) AS Avg_DO
                FROM emi_pembelian_loading AS a
                INNER JOIN suppliers AS b
                    ON a.kode_supplier = b.kode_supplier
                WHERE a.tanggal_otw IS NOT NULL
                    AND a.Kode_Perusahaan = '{KodePerusahaan}'
                    AND a.tanggal_masuk IS NOT NULL
                    AND a.Status IS NULL
                    AND a.Flag_Retur IS NULL
                    AND a.Tanggal >= '{CUT_OFF_DATE}'
                GROUP BY
                    b.kode_supplier,
                    b.nama
                ORDER BY
                    Avg_DO ASC;

            "

            Using Dr = OpenTrans(Query)
                While Dr.Read()
                    Guna2DataGridView4.Rows.Add(
                        Dr("Kode_Supplier"),
                        Dr("Nama"),
                        Dr("Avg_DO")
                    )
                End While
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Guna2DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles Guna2DateTimePicker1.ValueChanged
        If Guna2DateTimePicker1.Value > Guna2DateTimePicker2.Value Then
            Guna2DateTimePicker2.Value = Guna2DateTimePicker1.Value
        End If
        Fetch_Requested_Items()
    End Sub

    Private Sub Guna2DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles Guna2DateTimePicker2.ValueChanged
        If Guna2DateTimePicker2.Value < Guna2DateTimePicker1.Value Then
            Guna2DateTimePicker1.Value = Guna2DateTimePicker2.Value
        End If
        Fetch_Requested_Items()
    End Sub

    Private Sub Fetch_Selisih_Penerimaan()
        Try
            If Guna2DataGridView1 Is Nothing OrElse Guna2DataGridView1.Columns.Count = 0 Then Return
            OpenConn()
            Guna2DataGridView1.Rows.Clear()
            Guna2DataGridView1.RowHeadersVisible = True
            Guna2DataGridView1.RowHeadersWidth = 30
            Guna2DataGridView1.AllowUserToResizeRows = False
            Guna2DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            Dim statusFilter As String = ""
            Select Case Guna2ComboBox1.Text
                Case "Semua"
                    statusFilter = ""
                Case "Selesai"
                    statusFilter = "AND a.Flag_Selisih_BM = 'Y'"
                Case "Belum Selesai"
                    statusFilter = "AND a.Flag_Selisih_BM IS NULL"
                Case Else
                    statusFilter = "AND a.Flag_Selisih_BM IS NULL"
            End Select

            SQL = $"
                SELECT 
                    a.No_Faktur,
                    FORMAT(a.Tanggal_Masuk, 'dd MMM yyy') as Tanggal_Masuk,
                    FORMAT(a.Tanggal_OTW, 'dd MMM yyy') as Tanggal_OTW,
                    FORMAT(a.ETA, 'dd MMM yyy') as Tanggal_ETA,
                    b.Kode_Stock_Owner,
                    b.Tanggal_Produksi,
                    b.Tanggal_Expired,
                    b.Kode_Barang,
                    c.Nama,
                    b.Jumlah_Barang AS Jmlh_Pl_Hitung,
                    b.Jumlah AS Jmlh_Pl,
                    b.Jumlah_Masuk AS Jmlh_BM,
                    CASE 
                        WHEN (b.Jumlah_Barang - b.Jumlah_Masuk) > 0 
                            THEN '-' + FORMAT((b.Jumlah_Barang - b.Jumlah_Masuk), 'N0')
                        WHEN (b.Jumlah_Barang - b.Jumlah_Masuk) < 0 
                            THEN '+' + FORMAT(ABS(b.Jumlah_Barang - b.Jumlah_Masuk), 'N0')
                        ELSE '0'
                    END AS Jmlh_Selisih,
                    CASE 
                        WHEN a.Flag_Selisih_BM IS NOT NULL THEN 'Sudah Validasi'
                        ELSE 'Belum Validasi'
                    END AS Status_Validasi,
                    b.Satuan,
                    b.Satuan_Barang,
                    d.Harga_Barang,
                    d.Harga,
                    ISNULL(b.Jumlah_Barang * d.Harga_Barang, 0) AS Harga_Pl,
                    ISNULL(b.Jumlah_Masuk * d.Harga_Barang, 0) AS Harga_BM,
                    b.Urut_Oto,
                    s.Nama AS Nama_Supplier,
                    a.No_Plat + ' - ' + a.Driver AS Plat_Driver
                FROM EMI_Pembelian_Loading a
                INNER JOIN EMI_Pembelian_Loading_Detail b 
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan
                    AND a.No_Faktur = b.No_Faktur
                INNER JOIN Barang c 
                    ON b.Kode_Perusahaan = c.Kode_Perusahaan
                    AND b.Kode_Stock_Owner = c.Kode_Stock_Owner
                    AND b.Kode_Barang = c.Kode_Barang
                INNER JOIN EMI_Pembelian_PO_Detail d 
                    ON b.Kode_Perusahaan = d.Kode_Perusahaan
                    AND b.Urut_PO = d.No_Urut
                JOIN Suppliers s
                    ON a.Kode_Perusahaan = s.Kode_Perusahaan
                    AND a.Kode_Supplier = s.Kode_Supplier
                WHERE 
                    a.Kode_Perusahaan = '{KodePerusahaan}'
                    AND a.Status IS NULL
                    AND a.Flag_Retur IS NULL
                    AND a.Tanggal_Masuk IS NOT NULL 
                    AND a.Flag_Timbang_Keluar = 'Y' 
                    AND (b.Jumlah_Barang - b.Jumlah_Masuk) <> 0
                    {statusFilter}
                    AND (
                        b.No_Faktur LIKE '%{Guna2TextBox1.Text}%'
                        OR c.Nama LIKE '%{Guna2TextBox1.Text}%'
                        OR b.Kode_Barang LIKE '%{Guna2TextBox1.Text}%'
                    )
                    {If(IDGroupJenis <> 0, $"AND c.Id_Group_Jenis = {IDGroupJenis}", "")}
                ORDER BY CASE WHEN a.Flag_Selisih_BM IS NULL THEN 0 ELSE 1 END
            "

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim selisih As Decimal = If(IsDBNull(Dr("Jmlh_Pl_Hitung")), 0, Dr("Jmlh_Pl_Hitung")) - If(IsDBNull(Dr("Jmlh_BM")), 0, Dr("Jmlh_BM"))

                    Dim idx As Integer = Guna2DataGridView1.Rows.Add(
                        Dr("No_Faktur"),
                        Dr("Kode_Barang"),
                        Dr("Nama"),
                        Dr("Nama_Supplier"),
                        Dr("Plat_Driver"),
                        Dr("Tanggal_Masuk"),
                        Dr("Satuan"),
                        Dr("Jmlh_Pl"),
                        Dr("Jmlh_BM"),
                        Dr("Jmlh_Selisih"),
                        Dr("Status_Validasi")
                    )

                    If Dr("Status_Validasi") = "Belum Validasi" Then
                        With Guna2DataGridView1.Rows(idx)
                            .DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 0)
                            .DefaultCellStyle.ForeColor = Color.FromArgb(57, 18, 13)
                        End With
                    End If
                End While
            End Using

            Guna2DataGridView1.ClearSelection()

            Guna2HtmlLabel28.Visible = (Guna2DataGridView1.Rows.Count = 0)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_Requested_Items()
        Try
            If DGV_Requested_Items Is Nothing OrElse DGV_Requested_Items.Columns.Count = 0 Then Return
            OpenConn()
            DGV_Requested_Items.Rows.Clear()
            DGV_Requested_Items.RowHeadersVisible = True
            DGV_Requested_Items.RowHeadersWidth = 30
            DGV_Requested_Items.AllowUserToResizeRows = False
            DGV_Requested_Items.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            Dim statusPRParam As String = "NULL"
            Dim statusProsesParam As String = "NULL"

            Select Case CB_Requested_Item.Text
                Case "Selesai"
                    statusPRParam = "'Selesai'"
                    statusProsesParam = "'Selesai'"
                Case Else
                    statusProsesParam = $"'{CB_Requested_Item.Text}'"
            End Select

            Dim searchParam As String = "NULL"
            If Not String.IsNullOrEmpty(TB_SearchRequestedItems.Text.Trim()) Then
                searchParam = $"'{TB_SearchRequestedItems.Text.Trim().Replace("'", "''")}'"
            End If

            Dim tanggalDari As String = $"'{Guna2DateTimePicker1.Value.ToString("yyyy-MM-dd")}'"
            Dim tanggalSampai As String = $"'{Guna2DateTimePicker2.Value.ToString("yyyy-MM-dd")}'"

            SQL = $"EXEC N_EMI_SP_Tracking_Purchase_Requisition
            @Kode_Perusahaan = '{KodePerusahaan}',
            @Status_PR = {statusPRParam},
            @Status_Proses = {statusProsesParam},
            @Tanggal_Release_Dari = {tanggalDari},
            @Tanggal_Release_Sampai = {tanggalSampai},
            @SearchText = {searchParam},
            @ID_Group_Jenis = {IDGroupJenis}"

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim listPOInduk As String = Dr("List_PO_Induk").ToString()
                    Dim listSubPO As String = Dr("List_Sub_PO").ToString()

                    Dim formattedPOInduk As String = ""
                    Dim formattedSubPO As String = ""

                    If Not String.IsNullOrEmpty(listPOInduk) Then
                        Dim poParts As String() = listPOInduk.Split(";"c)
                        Dim poList As New List(Of String)
                        For Each po In poParts
                            Dim trimmedPO As String = po.Trim()
                            If Not String.IsNullOrEmpty(trimmedPO) Then
                                poList.Add("* " & trimmedPO)
                            End If
                        Next
                        formattedPOInduk = String.Join(Environment.NewLine, poList)
                    End If

                    If Not String.IsNullOrEmpty(listSubPO) Then
                        Dim spoParts As String() = listSubPO.Split(";"c)
                        Dim spoList As New List(Of String)
                        For Each spo In spoParts
                            Dim trimmedSPO As String = spo.Trim()
                            If Not String.IsNullOrEmpty(trimmedSPO) Then
                                spoList.Add("* " & trimmedSPO)
                            End If
                        Next
                        formattedSubPO = String.Join(Environment.NewLine, spoList)
                    End If

                    Dim idx As Integer = DGV_Requested_Items.Rows.Add(
                                            Dr("No_PR"),
                                            Dr("Kode_Barang"),
                                            Dr("Nama_Barang"),
                                            Dr("Waktu_PR"),
                                            Dr("Tgl_Kebutuhan"),
                                            Dr("Satuan"),
                                            Dr("Qty_PR"),
                                            Dr("Qty_PO"),
                                            Dr("Qty_Sub_PO"),
                                            Dr("Qty_DO"),
                                            Dr("Status_PR"),
                                            Dr("Status_Proses"),
                                            Dr("Persentase_Pemenuhan"),
                                            Dr("Sisa_Waktu"),
                                            Dr("User_Release")
                                        )

                    Dim sisaWaktu As String = Dr("Sisa_Waktu").ToString()
                    Dim statusProses As String = Dr("Status_Proses").ToString()

                    If statusProses = "Selesai" Then
                        With DGV_Requested_Items.Rows(idx)
                            .DefaultCellStyle.BackColor = Color.FromArgb(17, 139, 80)
                            .DefaultCellStyle.ForeColor = Color.White
                        End With
                    ElseIf statusProses <> "Selesai" AndAlso sisaWaktu.StartsWith("Telat") Then
                        With DGV_Requested_Items.Rows(idx)
                            .DefaultCellStyle.BackColor = Color.FromArgb(215, 53, 53)
                            .DefaultCellStyle.ForeColor = Color.White
                        End With
                    End If
                End While
            End Using

            DGV_Requested_Items.ClearSelection()
            DGV_Empty_Placeholder1.Visible = (DGV_Requested_Items.Rows.Count = 0)
            CloseConn()

        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_LeadTime()
        Try
            If Guna2DataGridView2 Is Nothing OrElse Guna2DataGridView2.Columns.Count = 0 Then Return

            OpenConn()
            Guna2DataGridView2.Rows.Clear()
            Guna2DataGridView2.RowHeadersVisible = True
            Guna2DataGridView2.RowHeadersWidth = 30
            Guna2DataGridView2.AllowUserToResizeRows = False
            Guna2DataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            SQL = $"
                WITH Data AS (
                    SELECT
                        pr.No_Faktur AS No_PR,
                        pr.Tanggal_Release AS Tgl_PR,
                        pr_d.Kode_Barang,
                        b.Nama AS Nama_Barang,
                        pr_d.Satuan,
                        pr_d.Jumlah AS Qty_PR,
                        COALESCE(poi_d.Jumlah, 0) AS Qty_PO_Induk,
                        COALESCE(spo_d.Jumlah, 0) AS Qty_Sub_PO,
                        COALESCE(do_d.Jumlah, 0) AS Qty_DO,
                        poi.Tanggal_Release AS Tgl_PO,
                        spo.Tanggal_Release AS Tgl_Sub_PO,
                        do.Tanggal_Masuk AS Tgl_Masuk_DO
                    FROM EMI_Purchase_Requisition pr
                    JOIN EMI_Purchase_Requisition_Detail pr_d 
                        ON pr_d.No_Faktur = pr.No_Faktur
                        AND pr_d.Kode_Perusahaan = pr.Kode_Perusahaan
                    LEFT JOIN EMI_Pembelian_PO_Det_Induk poi_d 
                        ON poi_d.No_Urut_PR = pr_d.No_Urut 
                        AND poi_d.Kode_Barang = pr_d.Kode_Barang
                        AND poi_d.Kode_Perusahaan = pr_d.Kode_Perusahaan
                    LEFT JOIN EMI_Pembelian_PO_Induk poi 
                        ON poi.No_Faktur = poi_d.No_Faktur
                        AND poi.Kode_Perusahaan = poi_d.Kode_Perusahaan
                    LEFT JOIN EMI_Pembelian_PO_Det spo_d 
                        ON spo_d.No_FakInduk = poi_d.No_Faktur 
                        AND spo_d.No_Urut_PR = pr_d.No_Urut 
                        AND spo_d.Kode_Barang = pr_d.Kode_Barang
                        AND spo_d.Kode_Perusahaan = pr_d.Kode_Perusahaan
                    LEFT JOIN EMI_Pembelian_PO spo 
                        ON spo.No_Faktur = spo_d.No_Faktur
                        AND spo.Kode_Perusahaan = spo_d.Kode_Perusahaan
                    LEFT JOIN EMI_Pembelian_Loading_Detail do_d 
                        ON do_d.No_PO = spo.No_Faktur 
                        AND do_d.Kode_Barang = pr_d.Kode_Barang
                        AND do_d.Kode_Perusahaan = pr_d.Kode_Perusahaan
                    LEFT JOIN EMI_Pembelian_Loading do 
                        ON do.No_Faktur = do_d.No_Faktur
                        AND do.Kode_Perusahaan = do_d.Kode_Perusahaan
                    JOIN Barang b 
                        ON b.Kode_Barang = pr_d.Kode_Barang 
                        AND b.Kode_Stock_Owner = pr_d.Kode_Stock_Owner
                        AND b.Kode_Perusahaan = pr_d.Kode_Perusahaan
                    WHERE
                        pr.Kode_Perusahaan = '001'
                        AND pr.Status IS NULL
                        AND pr.Flag_Release = 'Y'
                        AND poi.Status IS NULL
                        AND poi.Flag_Release = 'Y'
                        AND spo.Status IS NULL
                        AND spo.Flag_Release = 'Y'
                        AND do.Status IS NULL
                        AND do.Flag_Retur IS NULL
                        AND pr.Tanggal_Release >= '{CUT_OFF_DATE}'
                        {If(IDGroupJenis <> 0, $" AND b.Id_Group_Jenis = {IDGroupJenis} ", "")}
                )
                SELECT
                    No_PR,
                    FORMAT(MIN(Tgl_PR), 'dd MMM yyyy', 'id-ID') AS Waktu_PR,
                    Kode_Barang,
                    Nama_Barang,
                    Satuan,
                    Qty_PR,
                    COALESCE(
                        CAST(
                            CASE 
                                WHEN SUM(Qty_PO_Induk) = SUM(Qty_PR)
                                    THEN DATEDIFF(DAY, MIN(Tgl_PR), MAX(Tgl_PO))
                                WHEN SUM(Qty_PO_Induk) < SUM(Qty_PR)
                                    THEN DATEDIFF(DAY, MIN(Tgl_PR), GETDATE())
                            END AS VARCHAR(10)
                        ),
                        '-'
                    ) AS Waktu_PR_PO,
                    COALESCE(
                        CAST(
                            CASE 
                                WHEN SUM(Qty_Sub_PO) = SUM(Qty_PO_Induk)
                                    THEN DATEDIFF(DAY, MIN(Tgl_PO), MAX(Tgl_Sub_PO))
                                WHEN SUM(Qty_Sub_PO) < SUM(Qty_PO_Induk)
                                    THEN DATEDIFF(DAY, MIN(Tgl_PO), GETDATE())
                            END AS VARCHAR(10)
                        ),
                        '-'
                    ) AS Waktu_PO_SubPO,
                    COALESCE(
                        CAST(
                            CASE 
                                WHEN SUM(Qty_DO) = SUM(Qty_Sub_PO)
                                    THEN DATEDIFF(DAY, MIN(Tgl_Sub_PO), MAX(Tgl_Masuk_DO))
                                WHEN SUM(Qty_DO) < SUM(Qty_Sub_PO)
                                    THEN DATEDIFF(DAY, MIN(Tgl_Sub_PO), GETDATE())
                            END AS VARCHAR(10)
                        ),
                        '-'
                    ) AS Waktu_SubPO_DO
                FROM Data
                WHERE
                    No_PR LIKE '%{TB_SearchLeadTime.Text}%'
                    OR Kode_Barang LIKE '%{TB_SearchLeadTime.Text}%'
                    OR Nama_Barang LIKE '%{TB_SearchLeadTime.Text}%'
                GROUP BY 
                    No_PR, Kode_Barang, Nama_Barang, Satuan, Qty_PR
                ORDER BY 
                    No_PR, Kode_Barang;
            "

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim idx As Integer = Guna2DataGridView2.Rows.Add(
                        Dr("No_PR"),
                        Dr("Waktu_PR"),
                        Dr("Kode_Barang"),
                        Dr("Nama_Barang"),
                        Dr("Satuan"),
                        Dr("Qty_PR"),
                        Dr("Waktu_PR_PO"),
                        Dr("Waktu_PO_SubPO"),
                        Dr("Waktu_SubPO_DO")
                    )
                End While
            End Using

            Guna2DataGridView2.ClearSelection()

            Guna2HtmlLabel11.Visible = (Guna2DataGridView2.Rows.Count = 0)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_PR_Outstanding()
        Try
            OpenConn()

            SQL = $"
                SELECT 
                    COUNT(CASE 
                        WHEN Tanggal_Delivery >= CAST(GETDATE() AS DATE) 
                        THEN 1 
                    END) as Total_Item_On_Schedule,
                    COUNT(CASE 
                        WHEN Tanggal_Delivery < CAST(GETDATE() AS DATE) 
                        THEN 1 
                    END) as Total_Item_Telat
                FROM (
                    SELECT 
                        pr_d.No_Urut,
                        pr_d.Kode_Barang,
                        pr_d.Tanggal_Delivery,
                        pr_d.Jumlah,
                        ISNULL(SUM(CASE 
                            WHEN poi.Status IS NULL AND poi.Flag_Release = 'Y' 
                            THEN poi_d.Jumlah 
                            ELSE 0 
                        END), 0) as Qty_Sudah_PO
                    FROM EMI_Purchase_Requisition pr
                    JOIN EMI_Purchase_Requisition_Detail pr_d 
                        ON pr_d.No_Faktur = pr.No_Faktur
                        AND pr_d.Kode_Perusahaan = pr.Kode_Perusahaan
                    JOIN Barang b
                        ON b.Kode_Barang = pr_d.Kode_Barang
                        AND b.Kode_Stock_Owner = pr_d.Kode_Stock_Owner
                        AND b.Kode_Perusahaan = pr_d.Kode_Perusahaan
                    LEFT JOIN EMI_Pembelian_PO_Det_Induk poi_d 
                        ON poi_d.No_Urut_PR = pr_d.No_Urut 
                        AND poi_d.Kode_Barang = pr_d.Kode_Barang
                        AND poi_d.Kode_Perusahaan = pr.Kode_Perusahaan
                    LEFT JOIN EMI_Pembelian_PO_Induk poi 
                        ON poi.No_Faktur = poi_d.No_Faktur
                        AND poi.Kode_Perusahaan = pr.Kode_Perusahaan
                    WHERE
                        pr.Kode_Perusahaan = '{KodePerusahaan}'
                        AND pr.Status IS NULL
                        AND pr.Flag_Release = 'Y'
                        AND pr_d.Flag_Sudah_PO is null
                        {If(IDGroupJenis <> 0, $" AND b.Id_Group_Jenis = {IDGroupJenis} ", "")}
                    GROUP BY
                        pr_d.No_Urut,
                        pr_d.Kode_Barang,
                        pr_d.Tanggal_Delivery,
                        pr_d.Jumlah
                    HAVING 
                        pr_d.Jumlah - ISNULL(SUM(CASE 
                            WHEN poi.Status IS NULL AND poi.Flag_Release = 'Y' 
                            THEN poi_d.Jumlah 
                            ELSE 0 
                        END), 0) > 0
                ) outstanding_items
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dim totalDalamProses As Integer = If(IsDBNull(Dr("Total_Item_On_Schedule")), 0, Convert.ToInt32(Dr("Total_Item_On_Schedule")))
                    Dim totalTelat As Integer = If(IsDBNull(Dr("Total_Item_Telat")), 0, Convert.ToInt32(Dr("Total_Item_Telat")))

                    LB_PR_Info_Outstanding.Text = $"
                        <span style='color:#19a635;'>OS: {totalDalamProses:N0}</span>
                        <span style='color:#000;'>|</span> 
                        <span style='color:#eb2121;'>LS: {totalTelat:N0}</span>
                    "

                    LB_PR_Total_Outstanding.Text = (totalDalamProses + totalTelat).ToString("N0")
                Else
                    LB_PR_Info_Outstanding.Text = "<span style='color:#19a635;'>OS: 0</span> | <span style='color:#eb2121;'>LS: 0</span>"
                    LB_PR_Total_Outstanding.Text = "0"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_PO_Outstanding()
        Try
            OpenConn()

            SQL = $"
                ;WITH SubPO AS (
                    SELECT 
                        spo_d.No_FakInduk,
                        spo_d.No_Urut_PR,
                        spo_d.Kode_Barang,
                        SUM(spo_d.Jumlah) AS Total_Sub_PO
                    FROM EMI_Pembelian_PO_Det spo_d
                    JOIN EMI_Pembelian_PO spo ON spo.No_Faktur = spo_d.No_Faktur
                        AND spo.Status IS NULL
                        AND spo.Flag_Release = 'Y'
                    GROUP BY spo_d.No_FakInduk, spo_d.No_Urut_PR, spo_d.Kode_Barang
                ),
                PR AS (
                    SELECT
                        pr_d.Kode_Perusahaan,
                        pr_d.No_Faktur,
                        pr_d.No_Urut,
                        pr_d.Kode_Barang,
                        pr_d.Kode_Stock_Owner
                    FROM EMI_Purchase_Requisition_Detail pr_d
                    INNER JOIN EMI_Purchase_Requisition pr 
                        ON pr_d.No_Faktur = pr.No_Faktur
                        AND pr.Status IS NULL
                        AND pr.Flag_Release = 'Y'
                    GROUP BY pr_d.Kode_Perusahaan, pr_d.No_Faktur, pr_d.No_Urut, pr_d.Kode_Barang, pr_d.Kode_Stock_Owner
                )
                SELECT 
                    COUNT(CASE WHEN poi.ETD_Simulasi >= CAST(GETDATE() AS DATE) THEN 1 END) AS Total_Item_On_Schedule,
                    COUNT(CASE WHEN poi.ETD_Simulasi < CAST(GETDATE() AS DATE) THEN 1 END) AS Total_Item_Telat
                FROM EMI_Pembelian_PO_Induk poi
                JOIN EMI_Pembelian_PO_Det_Induk poi_d 
                    ON poi_d.No_Faktur = poi.No_Faktur
                INNER JOIN PR pr 
                    ON pr.No_Urut = poi_d.No_Urut_PR
                    AND pr.Kode_Barang = poi_d.Kode_Barang
                    AND pr.Kode_Perusahaan = poi.Kode_Perusahaan
                JOIN Barang b
                    ON b.Kode_Barang = pr.Kode_Barang
                    AND b.Kode_Perusahaan = pr.Kode_Perusahaan
                    AND b.Kode_Stock_Owner = pr.Kode_Stock_Owner
                LEFT JOIN SubPO s 
                    ON s.No_FakInduk = poi_d.No_Faktur
                    AND s.No_Urut_PR = poi_d.No_Urut_PR
                    AND s.Kode_Barang = poi_d.Kode_Barang
                WHERE 
                    poi.Kode_Perusahaan = '{KodePerusahaan}'
                    AND poi.Status IS NULL
                    AND poi.Flag_Release = 'Y'
                    AND poi.Flag_Selesai_SubPO IS NULL
                    {If(IDGroupJenis <> 0, $" AND b.Id_Group_Jenis = {IDGroupJenis} ", "")}
                    AND poi_d.Jumlah - ISNULL(s.Total_Sub_PO, 0) > 0;
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dim totalDalamProses As Integer = If(IsDBNull(Dr("Total_Item_On_Schedule")), 0, Convert.ToInt32(Dr("Total_Item_On_Schedule")))
                    Dim totalTelat As Integer = If(IsDBNull(Dr("Total_Item_Telat")), 0, Convert.ToInt32(Dr("Total_Item_Telat")))

                    LB_PO_Info_Outstanding.Text = $"
                        <span style='color:#19a635;'>OS: {totalDalamProses:N0}</span>
                        <span style='color:#000;'>|</span> 
                        <span style='color:#eb2121;'>LS: {totalTelat:N0}</span>
                    "

                    LB_PO_Total_Outstanding.Text = (totalDalamProses + totalTelat).ToString("N0")
                Else
                    LB_PO_Info_Outstanding.Text = "<span style='color:#19a635;'>OS: 0</span> | <span style='color:#eb2121;'>LS: 0</span>"
                    LB_PO_Total_Outstanding.Text = "0"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_Sub_PO_Outstanding()
        Try
            OpenConn()

            SQL = $"
                ;WITH DO_Sum AS (
                    SELECT 
                        do_d.No_PO,
                        do_d.Kode_Barang,
                        SUM(do_d.Jumlah) AS Total_DO
                    FROM EMI_Pembelian_Loading_Detail do_d
                    WHERE EXISTS (
                        SELECT 1 
                        FROM EMI_Pembelian_Loading do 
                        WHERE 
                            do.No_Faktur = do_d.No_Faktur
                            AND do.Kode_Perusahaan = '{KodePerusahaan}'
                            AND do.Status IS NULL
                            AND do.Flag_Retur IS NULL
                    )
                    GROUP BY do_d.No_PO, do_d.Kode_Barang
                ),
                PR AS (
                    SELECT
                        pr_d.Kode_Perusahaan,
                        pr_d.No_Faktur,
                        pr_d.No_Urut,
                        pr_d.Kode_Barang,
                        pr_d.Kode_Stock_Owner
                    FROM EMI_Purchase_Requisition_Detail pr_d
                    INNER JOIN EMI_Purchase_Requisition pr 
                        ON pr_d.No_Faktur = pr.No_Faktur
                        AND pr.Status IS NULL
                        AND pr.Flag_Release = 'Y'
                    GROUP BY pr_d.Kode_Perusahaan, pr_d.No_Faktur, pr_d.No_Urut, pr_d.Kode_Barang, pr_d.Kode_Stock_Owner
                )
                SELECT 
                    COUNT(CASE WHEN spo.ETD_Simulasi >= CAST(GETDATE() AS DATE) THEN 1 END) AS Total_Item_On_Schedule,
                    COUNT(CASE WHEN spo.ETD_Simulasi < CAST(GETDATE() AS DATE) THEN 1 END) AS Total_Item_Telat
                FROM EMI_Pembelian_PO spo
                JOIN EMI_Pembelian_PO_Det spo_d 
                    ON spo_d.No_Faktur = spo.No_Faktur
                    AND spo_d.Kode_Perusahaan = spo.Kode_Perusahaan
                JOIN EMI_Pembelian_PO_Induk poi 
                    ON poi.No_Faktur = spo_d.No_FakInduk
                    AND poi.Kode_Perusahaan = spo_d.Kode_Perusahaan
                INNER JOIN PR pr 
                    ON pr.No_Urut = spo_d.No_Urut_PR
                    AND pr.Kode_Barang = spo_d.Kode_Barang
                    AND pr.Kode_Perusahaan = spo.Kode_Perusahaan
                JOIN Barang b
                    ON b.Kode_Barang = pr.Kode_Barang
                    AND b.Kode_Perusahaan = pr.Kode_Perusahaan
                    AND b.Kode_Stock_Owner = pr.Kode_Stock_Owner
                LEFT JOIN DO_Sum ds ON ds.No_PO = spo.No_Faktur 
                    AND ds.Kode_Barang = spo_d.Kode_Barang
                WHERE
                    spo.Kode_Perusahaan = '{KodePerusahaan}'
                    AND spo.Status IS NULL
                    AND poi.Status IS NULL
                    AND poi.Flag_Release = 'Y'
                    AND spo.Flag_Release = 'Y'
                    AND spo.Flag_selesai_PO IS NULL
                    {If(IDGroupJenis <> 0, $" AND b.Id_Group_Jenis = {IDGroupJenis} ", "")}
                    AND spo_d.Jumlah - ISNULL(ds.Total_DO, 0) > 0;
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dim totalDalamProses As Integer = If(IsDBNull(Dr("Total_Item_On_Schedule")), 0, Convert.ToInt32(Dr("Total_Item_On_Schedule")))
                    Dim totalTelat As Integer = If(IsDBNull(Dr("Total_Item_Telat")), 0, Convert.ToInt32(Dr("Total_Item_Telat")))

                    LB_Sub_PO_Info_Outstanding.Text = $"
                        <span style='color:#19a635;'>OS: {totalDalamProses:N0}</span>
                        <span style='color:#000;'>|</span> 
                        <span style='color:#eb2121;'>LS: {totalTelat:N0}</span>
                    "

                    LB_Sub_PO_Total_Outstanding.Text = (totalDalamProses + totalTelat).ToString("N0")
                Else
                    LB_Sub_PO_Info_Outstanding.Text = "<span style='color:#19a635;'>OS: 0</span> | <span style='color:#eb2121;'>LS: 0</span>"
                    LB_Sub_PO_Total_Outstanding.Text = "0"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_DO_Outstanding()
        Try
            OpenConn()

            SQL = $"
                ;WITH DO_Items AS (
                    SELECT
                        do.No_Faktur,
                        TRY_CAST(do.ETD AS DATE)     AS ETD_date,
                        TRY_CAST(do.ETA AS DATE)     AS ETA_date,
                        TRY_CAST(do.Tanggal_OTW AS DATE)  AS OTW_date,
                        TRY_CAST(do.Tanggal_Masuk AS DATE) AS Masuk_date,
                        do_d.Kode_Barang,
                        do_d.Kode_Stock_Owner
                    FROM EMI_Pembelian_Loading do
                    JOIN EMI_Pembelian_Loading_Detail do_d
                        ON do_d.No_Faktur = do.No_Faktur
                    JOIN EMI_Pembelian_PO spo
                        ON spo.Kode_Perusahaan = do_d.Kode_Perusahaan
                        AND spo.No_Faktur = do_d.No_PO
                    JOIN Barang b
                        ON b.Kode_Perusahaan = do_d.Kode_Perusahaan
                        AND b.Kode_Barang = do_d.Kode_Barang
                        AND b.Kode_Stock_Owner = do_d.Kode_Stock_Owner
                    WHERE
                        do.Kode_Perusahaan = '{KodePerusahaan}'
                        AND do.status IS NULL
                        AND do.Flag_Retur IS NULL
                        AND do_d.Flag_Pembelian IS NULL
                        AND spo.Status IS NULL
                        AND spo.Flag_Release = 'Y'
                        {If(IDGroupJenis <> 0, $" AND b.Id_Group_Jenis = {IDGroupJenis} ", "")}
                )
                SELECT
                    SUM(CASE 
                        WHEN (OTW_date IS NOT NULL AND Masuk_date IS NULL AND ETA_date >= CAST(GETDATE() AS DATE))
                          OR (OTW_date IS NULL AND ETD_date >= CAST(GETDATE() AS DATE))
                        THEN 1 ELSE 0 END) AS Total_Item_On_Schedule,

                    COUNT(*) 
                      - SUM(CASE 
                            WHEN (OTW_date IS NOT NULL AND Masuk_date IS NULL AND ETA_date >= CAST(GETDATE() AS DATE))
                              OR (OTW_date IS NULL AND ETD_date >= CAST(GETDATE() AS DATE))
                            THEN 1 ELSE 0 END)
                    AS Total_Item_Telat
                FROM DO_Items;
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dim totalDalamProses As Integer = If(IsDBNull(Dr("Total_Item_On_Schedule")), 0, Convert.ToInt32(Dr("Total_Item_On_Schedule")))
                    Dim totalTelat As Integer = If(IsDBNull(Dr("Total_Item_Telat")), 0, Convert.ToInt32(Dr("Total_Item_Telat")))

                    LB_DO_Info_Outstanding.Text = $"
                        <span style='color:#19a635;'>OS: {totalDalamProses:N0}</span>
                        <span style='color:#000;'>|</span> 
                        <span style='color:#eb2121;'>LS: {totalTelat:N0}</span>
                    "

                    LB_DO_Total_Outstanding.Text = (totalDalamProses + totalTelat).ToString("N0")
                Else
                    LB_DO_Info_Outstanding.Text = "<span style='color:#19a635;'>OS: 0</span> | <span style='color:#eb2121;'>LS: 0</span>"
                    LB_DO_Total_Outstanding.Text = "0"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_Offer_Outstanding()
        Try
            OpenConn()

            SQL = $"
                WITH OutstandingPenawaran AS (
                    SELECT DISTINCT
                        p.No_Faktur,
                        p.Kode_Supplier,
                        p.Periode_Akhir_Penawaran,
                        p.flag_release,
                        p.Selesai
                    FROM EMI_Master_Penawaran p
                    JOIN EMI_Master_Penawaran_Detail pd
                        ON p.Kode_Perusahaan = pd.Kode_Perusahaan
                        AND p.No_Faktur = pd.No_Faktur
                    CROSS APPLY (
                        SELECT TOP 1 brg.Kode_Barang
                        FROM Barang brg
                        WHERE brg.Kode_Barang = pd.Kode_Barang
                          AND brg.Kode_Perusahaan = p.Kode_Perusahaan
                          {If(IDGroupJenis <> 0, $"AND brg.Id_Group_Jenis = '{IDGroupJenis}'", "")}
                        ORDER BY brg.Kode_Stock_Owner
                    ) brg
                    WHERE 
                        p.flag_release = 'Y'
                        AND p.Kode_Perusahaan = '{KodePerusahaan}'
                        AND p.Status IS NULL
                        AND CAST(GETDATE() AS DATE) 
                            BETWEEN p.Tgl_Penawaran_Hrg 
                            AND p.Periode_Akhir_Penawaran
                )
                SELECT 
                    (SELECT COUNT(*) FROM OutstandingPenawaran) 
                        AS Total_Penawaran_Outstanding,
                    (
                        SELECT COUNT(DISTINCT pd.Kode_Barang)
                        FROM EMI_Master_Penawaran_Detail pd
                        INNER JOIN OutstandingPenawaran o
                            ON o.No_Faktur = pd.No_Faktur
                    ) AS Total_Barang_Unik;
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dim totalPenawaran As Integer = If(IsDBNull(Dr("Total_Penawaran_Outstanding")), 0, Convert.ToInt32(Dr("Total_Penawaran_Outstanding")))
                    Dim totalBarangUnik As Integer = If(IsDBNull(Dr("Total_Barang_Unik")), 0, Convert.ToInt32(Dr("Total_Barang_Unik")))

                    Guna2HtmlLabel5.Text = $"Total Barang: {totalBarangUnik:N0}"
                    Guna2HtmlLabel7.Text = (totalPenawaran).ToString("N0")
                Else
                    Guna2HtmlLabel5.Text = "Total Barang: 0"
                    Guna2HtmlLabel7.Text = "0"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_Purchase_Value_Outstanding()
        Try
            OpenConn()

            SQL = $"
                SELECT 
                    CAST(ISNULL(SUM(t.Grand_Sebelum_PPN), 0) AS DECIMAL(18,2)) AS Total_Pembelian
                FROM EMI_Pembelian_PO t
                WHERE 
                    t.Status IS NULL
                    AND t.Kode_Perusahaan = '{KodePerusahaan}'
                    AND t.Flag_Release = 'Y'
                    AND t.Tanggal_Release >= '{CUT_OFF_DATE}';
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dim Total_Pembelian As Decimal = If(IsDBNull(Dr("Total_Pembelian")), 0D, Convert.ToDecimal(Dr("Total_Pembelian")))

                    Guna2HtmlLabel9.Text = "~"
                    Guna2HtmlLabel10.Text = FormatShortNumber(Total_Pembelian)
                Else
                    Guna2HtmlLabel9.Text = "~"
                    Guna2HtmlLabel10.Text = "0"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Function FormatShortNumber(ByVal value As Decimal) As String
        If value >= 1000000000000D Then
            Return (value / 1000000000000D).ToString("N2") & " T"
        ElseIf value >= 1000000000D Then
            Return (value / 1000000000D).ToString("N2") & " M"
        ElseIf value >= 1000000D Then
            Return (value / 1000000D).ToString("N2") & " JT"
        Else
            Return value.ToString("N0")
        End If
    End Function


    Private Sub PO_Outstanding_Click(sender As Object, e As EventArgs) Handles PO_Outstanding.Click
        Dim SD As New N_EMI_SD_Procurement_PO With {
            .StartPosition = FormStartPosition.CenterScreen,
            .IDGroupJenis = IDGroupJenis
        }

        SD.ShowDialog()
    End Sub

    Private Sub Sub_PO_Outstanding_Click(sender As Object, e As EventArgs) Handles Sub_PO_Outstanding.Click
        Dim SD As New N_EMI_SD_Procurement_SubPO With {
            .StartPosition = FormStartPosition.CenterScreen,
            .IDGroupJenis = IDGroupJenis
        }

        SD.ShowDialog()
    End Sub

    Private Sub Guna2Panel3_Click(sender As Object, e As EventArgs) Handles Guna2Panel3.Click
        Dim SD As New N_EMI_SD_Procurement_Loading With {
            .StartPosition = FormStartPosition.CenterScreen,
            .IDGroupJenis = IDGroupJenis
        }

        SD.ShowDialog()
    End Sub

    Private Sub CB_Requested_Item_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Requested_Item.SelectedIndexChanged
        Fetch_Requested_Items()
    End Sub

    Private Sub Guna2Panel2_Click(sender As Object, e As EventArgs) Handles Guna2Panel2.Click
        Dim SD As New N_EMI_SD_Procurement_Penawaran With {
            .StartPosition = FormStartPosition.CenterScreen
        }

        SD.ShowDialog()
    End Sub

    Private Sub Guna2Panel4_Click(sender As Object, e As EventArgs) Handles Guna2Panel4.Click
        Dim SD As New N_EMI_SD_Procurement_POValue With {
            .StartPosition = FormStartPosition.CenterScreen
        }

        SD.ShowDialog()
    End Sub

    Private Sub DGV_Requested_Items_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Requested_Items.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DGV_Requested_Items.Rows(e.RowIndex)
            Dim NoPR = selectedRow.Cells("Tab1_No_PR").Value.ToString()
            Dim KodeBarang = selectedRow.Cells("Tab1_Kode_Barang").Value.ToString()
            Dim NamaBarang = selectedRow.Cells("Tab1_Nama_Barang").Value.ToString()
            Dim TanggalRelease = selectedRow.Cells("Tab1_Waktu").Value.ToString()
            Dim User = selectedRow.Cells("Tab1_User_Release").Value.ToString()
            Dim QtyPR = selectedRow.Cells("Tab1_Qty_PR").Value.ToString()
            Dim QtyDO = selectedRow.Cells("Tab1_Qty_DO").Value.ToString()

            Dim SD As New N_EMI_SD_Procurement_Detail With {
                .NoPR = NoPR,
                .KodeBarang = KodeBarang,
                .NamaBarang = NamaBarang,
                .TanggalRelease = TanggalRelease,
                .User = User,
                .QtyPR = QtyPR,
                .QtyDO = QtyDO,
                .StartPosition = FormStartPosition.CenterScreen
            }

            SD.ShowDialog()
        End If
    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged
        Fetch_Selisih_Penerimaan()
    End Sub

    Private Sub Button_Export_Excel_Click(sender As Object, e As EventArgs) Handles Button_Export_Excel.Click
        If DGV_Requested_Items.Rows.Count = 0 Then
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
        Dim nama_file As String = "PR_Outstanding_Summary_" & format_akhir & ".xlsx"
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("Sheet1")
        xlApp.ScreenUpdating = False
        xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

        Try
            For colIndex As Integer = 0 To DGV_Requested_Items.Columns.Count - 1
                xlWorkSheet.Cells(1, colIndex + 1).Value = DGV_Requested_Items.Columns(colIndex).HeaderText
            Next

            Dim rowIndex As Integer = 2
            Dim rows = DGV_Requested_Items.Rows.Count
            Dim cols = DGV_Requested_Items.Columns.Count

            If rows > 0 Then
                Dim dataArr(rows - 1, cols - 1) As Object

                For r As Integer = 0 To rows - 1
                    For c As Integer = 0 To cols - 1
                        Dim value = DGV_Requested_Items.Rows(r).Cells(c).Value
                        Dim cellValue As String = If(value IsNot Nothing AndAlso Not IsDBNull(value), value.ToString(), "")
                        dataArr(r, c) = cellValue
                    Next
                Next

                Dim startCell = xlWorkSheet.Cells(2, 1)
                Dim endCell = xlWorkSheet.Cells(rows + 1, cols)
                Dim writeRange = xlWorkSheet.Range(startCell, endCell)
                Dim lastRow As Integer = rows + 1

                Dim rangeText As String =
                    "A2:C" & lastRow & ";" &
                    "F2:F" & lastRow & ";" &
                    "K2:L" & lastRow & ";" &
                    "N2:N" & lastRow

                xlWorkSheet.Range(rangeText).NumberFormat = "@"
                xlWorkSheet.Range(rangeText).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft


                Dim rangeDate As String =
                    "D2:E" & lastRow

                xlWorkSheet.Range(rangeDate).NumberFormat = "dd mmm yyyy"
                xlWorkSheet.Range(rangeDate).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter

                Dim rangeNumber As String =
                    "G2:I" & lastRow & ";" &
                    "J2:J" & lastRow & ";" &
                    "M2:M" & lastRow

                xlWorkSheet.Range(rangeNumber).NumberFormat = "#,##0.00"
                xlWorkSheet.Range(rangeNumber).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

                writeRange.Value = dataArr
                rowIndex = rows + 2
            End If

            xlWorkSheet.Cells.EntireColumn.AutoFit()

            Dim dataRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(rowIndex - 1, DGV_Requested_Items.Columns.Count))
            With dataRange.Borders
                .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
            End With

            Dim headerRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, DGV_Requested_Items.Columns.Count))
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
                MsgBox("PR Outstanding Summary berhasil di-export!", MsgBoxStyle.Information)
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