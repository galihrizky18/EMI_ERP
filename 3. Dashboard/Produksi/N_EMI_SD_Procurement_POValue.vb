Public Class N_EMI_SD_Procurement_POValue
    Private searchTimer As Timer
    Private Const DEBOUNCE_DELAY As Integer = 750
    Private Const MIN_SEARCH_LENGTH As Integer = 3

    Private Sub SD_Card_Purchase_Value_Outstanding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        searchTimer = New Timer With {.Interval = DEBOUNCE_DELAY}
        AddHandler searchTimer.Tick, AddressOf OnSearchTimerTick
        AddHandler TB_Search.TextChanged, AddressOf TB_Search_TextChanged

        Fetch_Purchase_Value_Outstanding()
    End Sub

    Private Sub TB_Search_TextChanged(sender As Object, e As EventArgs)
        searchTimer.Stop()
        searchTimer.Start()
    End Sub

    Private Sub OnSearchTimerTick(sender As Object, e As EventArgs)
        searchTimer.Stop()
        Fetch_Purchase_Value_Outstanding()
    End Sub

    Private Sub Fetch_Purchase_Value_Outstanding()
        Try
            If Guna2DataGridView1 Is Nothing OrElse Guna2DataGridView1.Columns.Count = 0 Then Return

            OpenConn()
            Guna2DataGridView1.Rows.Clear()
            Guna2DataGridView1.RowHeadersVisible = True
            Guna2DataGridView1.RowHeadersWidth = 30
            Guna2DataGridView1.AllowUserToResizeRows = False
            Guna2DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            SQL = $"
                SELECT 
                    p.No_Faktur AS No_PO,
                    FORMAT(p.Tanggal_Release, 'dd MMM yyyy', 'id-ID') AS Tgl_PO,
                    d.Kode_Barang,
                    b.Nama AS Nama_Barang,
                    d.Satuan,
                    d.Jumlah,
                    d.Total
                FROM emi_pembelian_po p
                INNER JOIN emi_pembelian_po_det d 
                    ON p.Kode_Perusahaan = d.Kode_Perusahaan
                    AND p.No_Faktur = d.No_Faktur
                LEFT JOIN Barang b 
                    ON d.Kode_Perusahaan = b.Kode_Perusahaan
                    AND d.Kode_Barang = b.Kode_Barang
                    AND d.Kode_Stock_Owner = b.Kode_Stock_Owner
                WHERE 
                    p.flag_release = 'Y'
                    AND p.Tanggal_Release >= '2025-08-01'
                    AND p.Kode_Perusahaan = '{KodePerusahaan}'
                    AND p.Status IS NULL
                    AND (
                        p.No_Faktur LIKE '%{TB_Search.Text}%' OR
                        d.Kode_Barang LIKE '%{TB_Search.Text}%' OR
                        b.Nama LIKE '%{TB_Search.Text}%'
                    )
                ORDER BY p.No_Faktur, d.Kode_Barang;
            "


            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim idx As Integer = Guna2DataGridView1.Rows.Add(
                        Dr("No_PO"),
                        Dr("Tgl_PO"),
                        Dr("Kode_Barang"),
                        Dr("Nama_Barang"),
                        Dr("Satuan"),
                        Dr("Jumlah"),
                        Dr("Total")
                    )
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
End Class
