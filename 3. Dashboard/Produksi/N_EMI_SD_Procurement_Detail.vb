Public Class N_EMI_SD_Procurement_Detail
    Public Property NoPR As String
    Public Property KodeBarang As String
    Public Property NamaBarang As String
    Public Property TanggalRelease As String
    Public Property User As String
    Public Property QtyPR As Decimal
    Public Property QtyDO As Decimal

    Private NoUrutPR As String = ""
    Private POIndukList As New List(Of String)
    Private SubPOList As New List(Of String)

    Private Sub SD_DGV_Dashboard_Procurment_Produksi_Load(sender As Object, e As EventArgs) Handles Me.Load
        With Guna2DataGridView4
            .Rows.Clear()
            .Columns.Clear()

            .Columns.Add("Key", "Key")
            .Columns.Add("Value", "Value")

            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .CellBorderStyle = DataGridViewCellBorderStyle.None
            .GridColor = Color.White
            .EnableHeadersVisualStyles = False
            .BackgroundColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.White
            .DefaultCellStyle.SelectionForeColor = Color.Black

            .Columns(0).Width = 150
            .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            .Columns(0).DefaultCellStyle.Font = New Font(.Font, FontStyle.Bold)

            .Rows.Add("No PR", NoPR)
            .Rows.Add("Kode Barang", KodeBarang)
            .Rows.Add("Nama Barang", NamaBarang)
            .Rows.Add("Tanggal Release", TanggalRelease)
            .Rows.Add("User", User)
            .Rows.Add("Qty Permintaan", QtyPR.ToString("N2"))
            .Rows.Add("Qty Pemenuhan", QtyDO.ToString("N2"))

            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToDeleteRows = False
        End With

        Try
            OpenConn()

            Dim Query = $"
                SELECT No_Urut 
                FROM EMI_Purchase_Requisition_Detail 
                WHERE No_Faktur = '{NoPR}' 
                    AND Kode_Barang = '{KodeBarang}'
                    AND Kode_Perusahaan = '{KodePerusahaan}'
            "

            Using Dr = OpenTrans(Query)
                If Dr.Read() Then
                    NoUrutPR = Dr("No_Urut").ToString()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error mendapatkan No Urut PR: " & ex.Message, MsgBoxStyle.Critical)
            Return
        End Try

        Fetch_PO_Induk()
        Fetch_Sub_PO()
        Fetch_DO()
    End Sub

    Private Sub Fetch_PO_Induk()
        Try
            OpenConn()

            Guna2DataGridView1.Rows.Clear()
            Guna2DataGridView1.RowHeadersVisible = True
            Guna2DataGridView1.RowHeadersWidth = 30
            Guna2DataGridView1.AllowUserToResizeRows = False
            Guna2DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            POIndukList.Clear()

            Dim Query = $"
                SELECT
                    poi.No_Faktur AS No_PO_Induk,
                    FORMAT(poi.Tanggal_Release, 'dd MMM yyyy') AS Waktu,
                    poi.Kode_Supplier,
                    s.Nama AS Nama_Supplier,
                    pod.Jumlah AS Qty
                FROM EMI_Pembelian_PO_Det_Induk pod
                INNER JOIN EMI_Pembelian_PO_Induk poi
                    ON pod.No_Faktur = poi.No_Faktur
                    AND pod.Kode_Perusahaan = poi.Kode_Perusahaan
                INNER JOIN Suppliers s
                    ON poi.Kode_Supplier = s.Kode_Supplier
                WHERE pod.Kode_Barang = '{KodeBarang}'
                    AND pod.No_Urut_PR = '{NoUrutPR}'
                    AND pod.Kode_Perusahaan = '{KodePerusahaan}'
                    AND poi.Status IS NULL
                    AND poi.Flag_Release = 'Y'
                ORDER BY poi.Tanggal_Release DESC;
            "

            Using Dr = OpenTrans(Query)
                While Dr.Read()
                    Guna2DataGridView1.Rows.Add(
                        Dr("No_PO_Induk"),
                        Dr("Waktu"),
                        Dr("Kode_Supplier"),
                        Dr("Nama_Supplier"),
                        Dr("Qty")
                    )

                    POIndukList.Add(Dr("No_PO_Induk").ToString())
                End While
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error Fetch PO Induk: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_Sub_PO()
        Try
            OpenConn()

            Guna2DataGridView2.Rows.Clear()
            Guna2DataGridView2.RowHeadersVisible = True
            Guna2DataGridView2.RowHeadersWidth = 30
            Guna2DataGridView2.AllowUserToResizeRows = False
            Guna2DataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            SubPOList.Clear()

            If POIndukList.Count = 0 Then
                CloseConn()
                Return
            End If

            Dim POIndukFilter As String = String.Join(",", POIndukList.Select(Function(x) $"'{x}'"))

            Dim Query = $"
                WITH SubPO_Data AS (
                    SELECT
                        spd.No_Faktur AS No_Sub_PO,
                        spd.No_FakInduk AS No_PO_Induk,
                        sp.Tanggal_Release,
                        FORMAT(sp.Tanggal_Release, 'dd MMM yyyy') AS Waktu,
                        spd.Jumlah AS Qty_Mentah,
                        ROW_NUMBER() OVER (ORDER BY sp.Tanggal_Release, spd.No_Faktur) AS RN
                    FROM EMI_Pembelian_PO_Det spd
                    INNER JOIN EMI_Pembelian_PO sp
                        ON spd.No_Faktur = sp.No_Faktur
                        AND spd.Kode_Perusahaan = sp.Kode_Perusahaan
                    WHERE spd.Kode_Barang = '{KodeBarang}'
                        AND spd.No_FakInduk IN ({POIndukFilter})
                        AND spd.No_Urut_PR = '{NoUrutPR}'
                        AND spd.Kode_Perusahaan = '{KodePerusahaan}'
                        AND sp.Status IS NULL
                        AND sp.Flag_Release = 'Y'
                ),
                SubPO_Cumulative AS (
                    SELECT *,
                        SUM(Qty_Mentah) OVER (ORDER BY RN ROWS UNBOUNDED PRECEDING) AS Cum_SubPO,
                        ISNULL(SUM(Qty_Mentah) OVER (ORDER BY RN ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING), 0) AS Prev_Cum_SubPO
                    FROM SubPO_Data
                ),
                PR_Total AS (
                    SELECT pr_d.Jumlah AS Total_Qty_PR
                    FROM EMI_Purchase_Requisition_Detail pr_d
                    WHERE pr_d.No_Urut = '{NoUrutPR}'
                        AND pr_d.Kode_Barang = '{KodeBarang}'
                        AND pr_d.Kode_Perusahaan = '{KodePerusahaan}'
                )
                SELECT 
                    No_Sub_PO,
                    No_PO_Induk,
                    Waktu,
                    Qty_Mentah,
                    CASE
                        WHEN (SELECT Total_Qty_PR FROM PR_Total) IS NULL THEN 0
                        WHEN (SELECT Total_Qty_PR FROM PR_Total) = 0 THEN 0
                        WHEN Prev_Cum_SubPO >= (SELECT Total_Qty_PR FROM PR_Total) THEN 0
                        WHEN Cum_SubPO <= (SELECT Total_Qty_PR FROM PR_Total) THEN Qty_Mentah
                        WHEN Prev_Cum_SubPO < (SELECT Total_Qty_PR FROM PR_Total) 
                             AND Cum_SubPO > (SELECT Total_Qty_PR FROM PR_Total) 
                             THEN (SELECT Total_Qty_PR FROM PR_Total) - Prev_Cum_SubPO
                        ELSE 0
                    END AS Qty_Distribusi
                FROM SubPO_Cumulative
                ORDER BY RN;
            "

            Using Dr = OpenTrans(Query)
                While Dr.Read()
                    Dim qtyDistribusi As Decimal = If(IsDBNull(Dr("Qty_Distribusi")), 0, Convert.ToDecimal(Dr("Qty_Distribusi")))
                    Dim qtyMentah As Decimal = If(IsDBNull(Dr("Qty_Mentah")), 0, Convert.ToDecimal(Dr("Qty_Mentah")))

                    Guna2DataGridView2.Rows.Add(
                        Dr("No_Sub_PO"),
                        Dr("No_PO_Induk"),
                        Dr("Waktu"),
                        qtyDistribusi
                    )

                    If qtyDistribusi > 0 Then
                        SubPOList.Add(Dr("No_Sub_PO").ToString())
                    End If

                    Dim idx As Integer = Guna2DataGridView2.Rows.Count - 1
                    If qtyDistribusi <> qtyMentah Then
                        Guna2DataGridView2.Rows(idx).Cells("P2_Qty").Style.BackColor = Color.FromArgb(255, 204, 0)
                        Guna2DataGridView2.Rows(idx).Cells("P2_Qty").Style.ForeColor = Color.FromArgb(57, 18, 13)
                        Guna2DataGridView2.Rows(idx).Cells("P2_Qty").ToolTipText = $"Qty Sub PO: {qtyMentah}{vbCrLf}Qty untuk PR ini: {qtyDistribusi}"
                    End If
                End While
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error Fetch Sub PO: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Fetch_DO()
        Try
            OpenConn()
            Guna2DataGridView3.Rows.Clear()
            Guna2DataGridView3.RowHeadersVisible = True
            Guna2DataGridView3.RowHeadersWidth = 30
            Guna2DataGridView3.AllowUserToResizeRows = False
            Guna2DataGridView3.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            If SubPOList.Count = 0 Then
                CloseConn()
                Return
            End If

            Dim SubPOFilter As String = String.Join(",", SubPOList.Select(Function(x) $"'{x}'"))

            Dim Query = $"
                WITH 
                SubPO_All_PRs AS (
                    SELECT 
                        poi_d.No_Urut_PR,
                        spo_d.Kode_Barang,
                        spo_d.No_Faktur AS No_Sub_PO,
                        spo_d.Jumlah AS Qty_Sub_PO,
                        poi_d.Jumlah AS Qty_PR_in_SubPO,
                        pr_d.Tanggal_Delivery,
                        pr.No_Faktur AS No_PR
                    FROM EMI_Pembelian_PO_Det spo_d
                    JOIN EMI_Pembelian_PO spo ON spo.No_Faktur = spo_d.No_Faktur
                        AND spo.Kode_Perusahaan = spo_d.Kode_Perusahaan
                    JOIN EMI_Pembelian_PO_Det_Induk poi_d ON spo_d.No_FakInduk = poi_d.No_Faktur 
                        AND spo_d.No_Urut_PR = poi_d.No_Urut_PR
                        AND spo_d.Kode_Perusahaan = poi_d.Kode_Perusahaan
                        AND spo_d.Urut_Det_Induk = poi_d.No_Urut
                    JOIN EMI_Purchase_Requisition_Detail pr_d ON pr_d.No_Urut = poi_d.No_Urut_PR
                        AND pr_d.Kode_Perusahaan = poi_d.Kode_Perusahaan
                    JOIN EMI_Purchase_Requisition pr ON pr.No_Faktur = pr_d.No_Faktur
                        AND pr.Kode_Perusahaan = pr_d.Kode_Perusahaan
                    WHERE spo.Status IS NULL 
                        AND spo.Flag_Release = 'Y'
                        AND spo.Kode_Perusahaan = '{KodePerusahaan}'
                        AND spo_d.Kode_Barang = '{KodeBarang}'
                        AND pr.Status IS NULL 
                        AND pr.Flag_Release = 'Y'
                ),
                SubPO_Current_PR AS (
                    SELECT *
                    FROM SubPO_All_PRs
                    WHERE No_Urut_PR = '{NoUrutPR}'
                ),
                SubPO_Total_Qty AS (
                    SELECT 
                        No_Sub_PO,
                        Kode_Barang,
                        SUM(Qty_PR_in_SubPO) AS Total_Qty_PR_in_SubPO
                    FROM SubPO_All_PRs
                    GROUP BY No_Sub_PO, Kode_Barang
                ),
                SubPO_With_Proportion AS (
                    SELECT 
                        sp.*,
                        spt.Total_Qty_PR_in_SubPO,
                        CAST(sp.Qty_PR_in_SubPO AS FLOAT) / NULLIF(spt.Total_Qty_PR_in_SubPO, 0) AS Proportion
                    FROM SubPO_Current_PR sp
                    JOIN SubPO_Total_Qty spt ON sp.No_Sub_PO = spt.No_Sub_PO 
                        AND sp.Kode_Barang = spt.Kode_Barang
                ),
                DO_All AS (
                    SELECT 
                        ld.No_Faktur AS No_DO,
                        ldd.No_PO AS No_Sub_PO,
                        ldd.Kode_Barang,
                        ldd.Jumlah AS Qty_DO,
                        FORMAT(ld.Tanggal_OTW, 'dd MMM yyyy HH:mm') AS OTW,
                        FORMAT(ld.ETA, 'dd MMM yyyy HH:mm') AS ETA,
                        FORMAT(ld.Tanggal_Masuk, 'dd MMM yyyy HH:mm') AS Tiba,
                        CASE
                            WHEN ld.Tanggal_Masuk IS NULL AND ld.Tanggal_OTW IS NULL THEN 'Menunggu Pengiriman'
                            WHEN ld.Tanggal_Masuk IS NULL AND ld.Tanggal_OTW IS NOT NULL THEN 'Dalam Perjalanan'
                            WHEN ld.Tanggal_Masuk IS NOT NULL AND ldd.Flag_Pembelian = 'Y' THEN 'Barang Telah Masuk'
                            WHEN ld.Tanggal_Masuk IS NOT NULL AND ldd.Flag_Timbang_Keluar = 'Y' THEN 'Timbang Keluar'
                            WHEN ld.Tanggal_Masuk IS NOT NULL AND ldd.Flag_QC = 'Y' THEN 'QC2 Passed'
                            WHEN ld.Tanggal_Masuk IS NOT NULL AND ldd.Flag_Timbang_Masuk = 'Y' THEN 'Timbang Masuk'
                            WHEN ld.Tanggal_Masuk IS NOT NULL AND ldd.Flag_QC_Pertama = 'Y' THEN 'QC1 Passed'
                            WHEN ld.Tanggal_Masuk IS NOT NULL THEN 'Tiba'
                            ELSE '-'
                        END AS Status
                    FROM EMI_Pembelian_Loading_Detail ldd
                    JOIN EMI_Pembelian_Loading ld ON ld.No_Faktur = ldd.No_Faktur
                        AND ld.Kode_Perusahaan = ldd.Kode_Perusahaan
                    WHERE ldd.Kode_Barang = '{KodeBarang}'
                        AND ld.Kode_Perusahaan = '{KodePerusahaan}'
                        AND ld.Status IS NULL
                        AND ld.Flag_Retur IS NULL
                        AND ldd.No_PO IN (SELECT DISTINCT No_Sub_PO FROM SubPO_With_Proportion)
                ),
                DO_With_Alokasi AS (
                    SELECT 
                        d.No_DO,
                        d.No_Sub_PO,
                        d.OTW,
                        d.ETA,
                        d.Tiba,
                        d.Status,
                        d.Qty_DO,
                        sp.No_Urut_PR,
                        sp.Qty_PR_in_SubPO,
                        sp.Total_Qty_PR_in_SubPO,
                        sp.Proportion,
                        ROUND(d.Qty_DO * sp.Proportion, 2) AS Qty_Alokasi
                    FROM DO_All d
                    INNER JOIN SubPO_With_Proportion sp 
                        ON d.No_Sub_PO = sp.No_Sub_PO 
                        AND d.Kode_Barang = sp.Kode_Barang
                )
                SELECT 
                    No_DO,
                    No_Sub_PO,
                    OTW,
                    ETA,
                    Tiba,
                    Status,
                    Qty_Alokasi,
                    Qty_DO
                FROM DO_With_Alokasi
                ORDER BY No_DO;
            "

            Using Dr = OpenTrans(Query)
                While Dr.Read()
                    Dim qtyAlokasi As Decimal = If(IsDBNull(Dr("Qty_Alokasi")), 0, Convert.ToDecimal(Dr("Qty_Alokasi")))
                    Dim qtyDO As Decimal = If(IsDBNull(Dr("Qty_DO")), 0, Convert.ToDecimal(Dr("Qty_DO")))

                    Guna2DataGridView3.Rows.Add(
                        Dr("No_DO"),
                        Dr("No_Sub_PO"),
                        If(IsDBNull(Dr("OTW")), "-", Dr("OTW")),
                        If(IsDBNull(Dr("ETA")), "-", Dr("ETA")),
                        If(IsDBNull(Dr("Tiba")), "-", Dr("Tiba")),
                        Dr("Status"),
                        qtyDO,
                        qtyAlokasi
                    )

                    Dim idx As Integer = Guna2DataGridView3.Rows.Count - 1
                    Dim status As String = Dr("Status").ToString()

                    Select Case status
                        Case "Barang Telah Masuk"
                            Guna2DataGridView3.Rows(idx).DefaultCellStyle.BackColor = Color.FromArgb(17, 139, 80)
                            Guna2DataGridView3.Rows(idx).DefaultCellStyle.ForeColor = Color.White
                        Case "Dalam Perjalanan"
                            Guna2DataGridView3.Rows(idx).DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 0)
                            Guna2DataGridView3.Rows(idx).DefaultCellStyle.ForeColor = Color.FromArgb(57, 18, 13)
                        Case "Menunggu Pengiriman"
                            Guna2DataGridView3.Rows(idx).DefaultCellStyle.BackColor = Color.FromArgb(34, 40, 49)
                            Guna2DataGridView3.Rows(idx).DefaultCellStyle.ForeColor = Color.White
                    End Select
                End While
            End Using

            Guna2DataGridView3.ClearSelection()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error Fetch DO: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class