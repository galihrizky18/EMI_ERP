Imports System.Web
Imports Guna.UI2.WinForms

Public Class N_EMI_SD_Procurement_Barang_Lain_Penawaran
    Public Property FilterKodeBarang As String

    Private searchTimer As Timer
    Private Const DEBOUNCE_DELAY As Integer = 750
    Private Const MIN_SEARCH_LENGTH As Integer = 3

    Private Sub SD_Card_Offer_Outstanding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        searchTimer = New Timer With {.Interval = DEBOUNCE_DELAY}
        AddHandler searchTimer.Tick, AddressOf OnSearchTimerTick
        AddHandler TB_Search.TextChanged, AddressOf TB_Search_TextChanged

        Fetch_Offer_Outstanding()
    End Sub

    Private Sub TB_Search_TextChanged(sender As Object, e As EventArgs)
        searchTimer.Stop()
        searchTimer.Start()
    End Sub

    Private Sub OnSearchTimerTick(sender As Object, e As EventArgs)
        searchTimer.Stop()
        Fetch_Offer_Outstanding()
    End Sub

    Private Sub Fetch_Offer_Outstanding()
        Try
            If Guna2DataGridView1 Is Nothing OrElse Guna2DataGridView1.Columns.Count = 0 Then Return

            OpenConn()
            Guna2DataGridView1.Rows.Clear()
            Guna2DataGridView1.RowHeadersVisible = True
            Guna2DataGridView1.RowHeadersWidth = 30
            Guna2DataGridView1.AllowUserToResizeRows = False
            Guna2DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            If FilterKodeBarang = "" Then
                CloseConn()
                Exit Sub
            End If

            SQL = $"
                SELECT
                    p.No_Faktur AS No_Offer,
                    FORMAT(p.Tgl_Penawaran_Hrg, 'dd MMM yyyy', 'id-ID') AS Tgl_Penawaran,
                    FORMAT(p.Periode_Akhir_Penawaran, 'dd MMM yyyy', 'id-ID') AS Priode_Akhir,
                    d.Kode_Barang,
                    p.Kode_Supplier,
                    s.Nama AS Nama_Supplier,
                    b.Nama AS Nama_Barang,
                    d.Satuan,
                    d.Min_Order AS Jumlah,
                    d.Mata_Uang,
                    d.Harga_Satuan
                FROM EMI_Master_Penawaran_Barang_Lain p
                INNER JOIN EMI_Master_Penawaran_Detail_Barang_Lain d 
                    ON p.No_Faktur = d.No_Faktur
                    AND d.Kode_Perusahaan = p.Kode_Perusahaan
                LEFT JOIN (
                    SELECT DISTINCT Kode_Barang, Kode_Perusahaan, Nama
                    FROM Barang_Lain
                ) b
                    ON d.Kode_Barang = b.Kode_Barang
                    AND b.Kode_Perusahaan = p.Kode_Perusahaan
                LEFT JOIN Suppliers s
                    ON s.Kode_Supplier = p.Kode_Supplier
                WHERE 
                    p.flag_release = 'Y'
                    AND p.Status IS NULL
                    AND p.Kode_Perusahaan = '{KodePerusahaan}'
                    AND CAST(GETDATE() AS DATE) between p.Tgl_Penawaran_Hrg and p.Periode_Akhir_Penawaran
                    AND (
                        s.Nama LIKE '%{TB_Search.Text}%' OR
                        p.Kode_Supplier LIKE '%{TB_Search.Text}%' OR
                        p.No_Faktur LIKE '%{TB_Search.Text}%' OR
                        d.Kode_Barang LIKE '%{TB_Search.Text}%' OR
                        b.Nama LIKE '%{TB_Search.Text}%'
                    )
                    AND d.Kode_Barang IN ({FilterKodeBarang})
                ORDER BY p.No_Faktur, d.Kode_Barang, p.Tgl_Penawaran_Hrg, p.Periode_Akhir_Penawaran;
            "

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim idx As Integer = Guna2DataGridView1.Rows.Add(
                        Dr("No_Offer"),
                        Dr("Tgl_Penawaran"),
                        Dr("Priode_Akhir"),
                        Dr("Kode_Supplier"),
                        Dr("Nama_Supplier"),
                        Dr("Kode_Barang"),
                        Dr("Nama_Barang"),
                        Dr("Satuan"),
                        Dr("Jumlah"),
                        Dr("Mata_Uang"),
                        Dr("Harga_Satuan")
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
