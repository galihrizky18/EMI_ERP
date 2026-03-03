Imports Guna.Charts.WinForms
Imports Guna.UI2.WinForms

Public Class N_EMI_Dashboard_Procurment_Barang_Lain
    Private RoleButtonTampilSemuaBarangLain As Boolean = True
    Private KodeKategoriGudang As String = ""
    Private KodeBarang As String = ""
    Private CutOffDate As String = "2025-08-01"

    Private Const DEBOUNCE_DELAY As Integer = 300
    Private Const MIN_SEARCH_LENGTH As Integer = 3
    Private SearchTimer As Timer
    Private LastSearchData As String = ""

    Private MountStartIndex As Integer = 0
    Private MountEndIndex As Integer = 11
    Private MonthCTE As String = "
        WITH BulanCTE AS (
            SELECT 0 AS OffsetMonth, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1) AS BulanAwal
            UNION ALL
            SELECT OffsetMonth + 1, DATEADD(MONTH, -(OffsetMonth + 1), DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
            FROM BulanCTE
            WHERE OffsetMonth + 1 < 12
        )
    "

    Private Sub N_EMI_Dashboard_Procurment_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Form full screen
        Me.Dock = DockStyle.Fill

        'Set filter tanggal
        DtpTanggalAkhir.Value = DateTime.Today

        'Set interval debaunce search
        SearchTimer = New Timer With {
            .Interval = DEBOUNCE_DELAY
        }

        'Handler event text changed dengan debounce
        AddHandler SearchTimer.Tick, AddressOf OnSearchTimerTick
        AddHandler TbPermintaanBarang.TextChanged, AddressOf TbPermintaanBarang_TextChanged
        AddHandler TbLeadTime.TextChanged, AddressOf TbLeadTime_TextChanged
        AddHandler TbSelisihPenerimaan.TextChanged, AddressOf TbSelisihPenerimaan_TextChanged

        'Fetch Role Button
        Fetch_RoleButton()

        'Get Kategori Gudang
        Get_KodeKategoriGudang()

        'Set kode kategori gudang ke global variable ketika form load
        CbKodeKategoriGudang.SelectedIndex = 0
    End Sub

    'Handle PR item search text changed dengan debounce
    Private Sub TbPermintaanBarang_TextChanged(sender As Object, e As EventArgs)
        SearchTimer.Stop()

        Dim searchText As String = TbPermintaanBarang.Text.Trim()

        If String.IsNullOrEmpty(searchText) Then
            Get_DashboardData()
            Return
        End If

        If searchText.Length < MIN_SEARCH_LENGTH Then
            Return
        End If

        LastSearchData = "RequestedItems"
        SearchTimer.Start()
    End Sub

    'Handle lead time search text changed dengan debounce
    Private Sub TbLeadTime_TextChanged(sender As Object, e As EventArgs)
        SearchTimer.Stop()

        Dim searchText As String = TbPermintaanBarang.Text.Trim()

        If String.IsNullOrEmpty(searchText) Then
            Get_DashboardData()
            Return
        End If

        If searchText.Length < MIN_SEARCH_LENGTH Then
            Return
        End If

        LastSearchData = "LeadTime"
        SearchTimer.Start()
    End Sub

    'Handle selisih penerimaan search text changed dengan debounce
    Private Sub TbSelisihPenerimaan_TextChanged(sender As Object, e As EventArgs)
        SearchTimer.Stop()

        Dim searchText As String = TbPermintaanBarang.Text.Trim()

        If String.IsNullOrEmpty(searchText) Then
            Get_DashboardData()
            Return
        End If

        If searchText.Length < MIN_SEARCH_LENGTH Then
            Return
        End If

        LastSearchData = "SelisihPenerimaan"
        SearchTimer.Start()
    End Sub

    'Handle text changed untuk search requested items dengan debounce
    Private Sub OnSearchTimerTick(sender As Object, e As EventArgs)
        SearchTimer.Stop()

        Get_DashboardData()
    End Sub

    'Cek akses user
    Private Sub Fetch_RoleButton()
        Try
            OpenConn()
            If CekButtonRole("Tampil_Offer_Dashboard_Procurment_Barang_Lain") = "T" Then
                CardOfferOutstanding.Visible = False
            End If

            OpenConn()
            If CekButtonRole("Tampil_Purchase_Value_Dashboard_Procurment_Barang_Lain") = "T" Then
                CardPurchaseValue.Visible = False

                Guna2Panel9.Visible = False

                Guna2Panel7.Size = New Size(1123, 293)
                Guna2Panel7.Location = New Point(6, 6)
            End If

            OpenConn()
            If CekButtonRole("Tampil_Semua_Barang_Lain") = "T" Then
                RoleButtonTampilSemuaBarangLain = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Untuk mendapatkan list kategori gudang
    Private Sub Get_KodeKategoriGudang()
        Try
            OpenConn()

            If RoleButtonTampilSemuaBarangLain = False Then
                SQL = $"Select a.Kode_Kategori_Gudang, a.Keterangan From N_EMI_Master_Kategori_Gudang_Barang_Lain a, N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain b
                        Where a.kode_perusahaan=b.kode_perusahaan and a.Kode_Kategori_Gudang=b.Kode_Kategori_Gudang and
                        a.Kode_Perusahaan = '{KodePerusahaan}' and b.user_id='{UserID}' and a.flag_data_lama is null Order By a.Kode_Kategori_Gudang ASC "
            Else
                SQL = $"Select Kode_Kategori_Gudang, Keterangan From N_EMI_Master_Kategori_Gudang_Barang_Lain Where Kode_Perusahaan = '{KodePerusahaan}'  and flag_data_lama is null and Jenis_Gudang='Warehouse' Order By Kode_Kategori_Gudang ASC"
            End If

            Dim Ds As DataSet = BindingTrans(SQL)
            Dim Dt As DataTable

            If Ds IsNot Nothing AndAlso Ds.Tables.Count > 0 Then
                Dt = Ds.Tables(0)
            Else
                Dt = New DataTable()
                Dt.Columns.Add("Keterangan")
                Dt.Columns.Add("Kode_Kategori_Gudang")
            End If

            Dim dr As DataRow = Dt.NewRow()
            dr("Keterangan") = "-- SEMUA DATA --"
            dr("Kode_Kategori_Gudang") = ""
            Dt.Rows.InsertAt(dr, 0)

            CbKodeKategoriGudang.DataSource = Dt
            CbKodeKategoriGudang.DisplayMember = "Keterangan"
            CbKodeKategoriGudang.ValueMember = "Kode_Kategori_Gudang"
            CbKodeKategoriGudang.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Jika user memiliki akses untuk melihat semua barang maka tidak akan difilter berdasarkan user id
    'Untuk mendapatkan kode barang berdasarkan kategori gudang yang dipilih, jika tidak ada kategori gudang yang dipilih maka akan menampilkan semua barang
    Private Function Get_KodeBarang(KodePerusahaan As String, UserID As String) As String
        Dim ResultKodeBarang As String = ""
        Try
            OpenConn()

            SQL = $"
                SELECT a.Kode_Barang
                FROM Barang_Lain a
                JOIN EMI_Group_Jenis_Lain b 
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan
                    AND a.Id_Group_Jenis = b.Id_Group_Jenis
                JOIN View_Kategori_Turunan d 
                    ON a.Kode_Perusahaan = d.Kode_Perusahaan
                    AND a.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3
                JOIN N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain e 
                    ON d.Id_Kategori_Jenis = e.Id_Kategori_Jenis
                    AND d.Id_Sub_Kategori_Jenis = e.Id_Sub_Kategori_Jenis
                JOIN EMI_Kategori_Gudang_PerLokasi_Barang_Lain c 
                    ON a.Kode_Perusahaan = c.Kode_Perusahaan
                    AND a.Id_Kategori_Gudang = c.ID_Kategori_Gudang
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
                    AND a.Flag_Barang_Lama IS NULL
                    AND a.aktif = 'Y'
                    {If(RoleButtonTampilSemuaBarangLain, "", $"AND e.User_ID = '{UserID}'")}
                    {If(KodeKategoriGudang <> "", $"AND e.Kode_Kategori_Gudang = '{KodeKategoriGudang}'", "")}
                GROUP BY a.Kode_Barang
                ORDER BY a.Kode_Barang
            "

            Using Dr = OpenTrans(SQL)
                Dim ListKodeBarang As New List(Of String)

                While Dr.Read()
                    If Not IsDBNull(Dr("Kode_Barang")) Then
                        ListKodeBarang.Add("'" & Dr("Kode_Barang").ToString().Trim() & "'")
                    End If
                End While

                If ListKodeBarang.Count > 0 Then
                    ResultKodeBarang = String.Join(",", ListKodeBarang)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            ResultKodeBarang = ""
        End Try

        Return ResultKodeBarang
    End Function

    'Get card pr outstanding
    Private Sub Get_Card_PR_Outstanding()
        Try
            OpenConn()

            LB_PR_Total_Outstanding.Text = 0
            LB_PR_Info_Outstanding.Text = $"
                <span style='color:#19a635;'>OS: 0</span>
                <span style='color:#000;'>|</span> 
                <span style='color:#eb2121;'>LS: 0</span>
            "

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
                    FROM EMI_Purchase_Requisition_Barang_Lain pr
                    JOIN EMI_Purchase_Requisition_Barang_Lain_Detail pr_d 
                        ON pr_d.No_Faktur = pr.No_Faktur
                        AND pr_d.Kode_Perusahaan = pr.Kode_Perusahaan
                    JOIN Barang_Lain b
                        ON b.Kode_Barang = pr_d.Kode_Barang
                        AND b.Kode_Stock_Owner = pr_d.Kode_Stock_Owner
                        AND b.Kode_Perusahaan = pr_d.Kode_Perusahaan
                    LEFT JOIN EMI_Pembelian_PO_Det_Induk_Barang_Lain poi_d 
                        ON poi_d.No_Urut_PR = pr_d.No_Urut 
                        AND poi_d.Kode_Barang = pr_d.Kode_Barang
                        AND poi_d.Kode_Perusahaan = pr.Kode_Perusahaan
                    LEFT JOIN EMI_Pembelian_PO_Induk_Barang_Lain poi 
                        ON poi.No_Faktur = poi_d.No_Faktur
                        AND poi.Kode_Perusahaan = pr.Kode_Perusahaan
                    WHERE
                        pr.Kode_Perusahaan = '{KodePerusahaan}'
                        AND pr.Status IS NULL
                        AND pr.Flag_Release = 'Y'
                        AND pr_d.Flag_Sudah_po is null
                        AND pr_d.Kode_Barang IN ({KodeBarang})
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get card po outstanding
    Private Sub Get_Card_PO_Outstanding()
        Try
            OpenConn()

            LB_PO_Total_Outstanding.Text = 0
            LB_PO_Info_Outstanding.Text = $"
                <span style='color:#19a635;'>OS: 0</span>
                <span style='color:#000;'>|</span> 
                <span style='color:#eb2121;'>LS: 0</span>
            "

            SQL = $"
                ;WITH SubPO AS (
                    SELECT 
                        spo_d.No_FakInduk,
                        spo_d.No_Urut_PR,
                        spo_d.Kode_Barang,
                        SUM(spo_d.Jumlah) AS Total_Sub_PO
                    FROM EMI_Pembelian_PO_Det_Barang_Lain spo_d
                    JOIN EMI_Pembelian_PO_Barang_Lain spo ON spo.No_Faktur = spo_d.No_Faktur
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
                    FROM EMI_Purchase_Requisition_Barang_Lain_Detail pr_d
                    INNER JOIN EMI_Purchase_Requisition_Barang_Lain pr 
                        ON pr_d.No_Faktur = pr.No_Faktur
                        AND pr.Status IS NULL
                        AND pr.Flag_Release = 'Y'
                    GROUP BY pr_d.Kode_Perusahaan, pr_d.No_Faktur, pr_d.No_Urut, pr_d.Kode_Barang, pr_d.Kode_Stock_Owner
                )
                SELECT 
                    COUNT(CASE WHEN poi.ETD_Simulasi >= CAST(GETDATE() AS DATE) THEN 1 END) AS Total_Item_On_Schedule,
                    COUNT(CASE WHEN poi.ETD_Simulasi < CAST(GETDATE() AS DATE) THEN 1 END) AS Total_Item_Telat
                FROM EMI_Pembelian_PO_Induk_Barang_Lain poi
                JOIN EMI_Pembelian_PO_Det_Induk_Barang_Lain poi_d 
                    ON poi_d.No_Faktur = poi.No_Faktur
                INNER JOIN PR pr 
                    ON pr.No_Urut = poi_d.No_Urut_PR
                    AND pr.Kode_Barang = poi_d.Kode_Barang
                    AND pr.Kode_Perusahaan = poi.Kode_Perusahaan
                JOIN Barang_Lain b
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
                    AND poi_d.Jumlah - ISNULL(s.Total_Sub_PO, 0) > 0
                    AND pr.Kode_Barang IN ({KodeBarang});
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get card sub po outstanding
    Private Sub Get_Card_Sub_PO_Outstanding()
        Try
            OpenConn()

            LB_Sub_PO_Total_Outstanding.Text = 0
            LB_Sub_PO_Info_Outstanding.Text = $"
                <span style='color:#19a635;'>OS: 0</span>
                <span style='color:#000;'>|</span> 
                <span style='color:#eb2121;'>LS: 0</span>
            "

            SQL = $"
                ;WITH DO_Sum AS (
                    SELECT 
                        do_d.No_PO,
                        do_d.Kode_Barang,
                        SUM(do_d.Jumlah) AS Total_DO
                    FROM EMI_Pembelian_Loading_Detail_Barang_Lain do_d
                    WHERE EXISTS (
                        SELECT 1 
                        FROM EMI_Pembelian_Loading_Barang_Lain do 
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
                    FROM EMI_Purchase_Requisition_Barang_Lain_Detail pr_d
                    INNER JOIN EMI_Purchase_Requisition_Barang_Lain pr 
                        ON pr_d.No_Faktur = pr.No_Faktur
                        AND pr.Status IS NULL
                        AND pr.Flag_Release = 'Y'
                    GROUP BY pr_d.Kode_Perusahaan, pr_d.No_Faktur, pr_d.No_Urut, pr_d.Kode_Barang, pr_d.Kode_Stock_Owner
                )
                SELECT 
                    COUNT(CASE WHEN spo.ETD_Simulasi >= CAST(GETDATE() AS DATE) THEN 1 END) AS Total_Item_On_Schedule,
                    COUNT(CASE WHEN spo.ETD_Simulasi < CAST(GETDATE() AS DATE) THEN 1 END) AS Total_Item_Telat
                FROM EMI_Pembelian_PO_Barang_Lain spo
                JOIN EMI_Pembelian_PO_Det_Barang_Lain spo_d 
                    ON spo_d.No_Faktur = spo.No_Faktur
                    AND spo_d.Kode_Perusahaan = spo.Kode_Perusahaan
                JOIN EMI_Pembelian_PO_Induk_Barang_Lain poi 
                    ON poi.No_Faktur = spo_d.No_FakInduk
                    AND poi.Kode_Perusahaan = spo_d.Kode_Perusahaan
                INNER JOIN PR pr 
                    ON pr.No_Urut = spo_d.No_Urut_PR
                    AND pr.Kode_Barang = spo_d.Kode_Barang
                    AND pr.Kode_Perusahaan = spo.Kode_Perusahaan
                JOIN Barang_Lain b
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
                    AND spo_d.Jumlah - ISNULL(ds.Total_DO, 0) > 0
                    AND pr.Kode_Barang IN ({KodeBarang});
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get card do outstanding
    Private Sub Get_Card_DO_Outstanding()
        Try
            OpenConn()

            LB_DO_Total_Outstanding.Text = 0
            LB_DO_Info_Outstanding.Text = $"
                <span style='color:#19a635;'>OS: 0</span>
                <span style='color:#000;'>|</span> 
                <span style='color:#eb2121;'>LS: 0</span>
            "

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
                    FROM EMI_Pembelian_Loading_Barang_Lain do
                    JOIN EMI_Pembelian_Loading_Detail_Barang_Lain do_d
                        ON do_d.Kode_Perusahaan = do.Kode_Perusahaan
                        AND do_d.No_Faktur = do.No_Faktur
                    JOIN EMI_Pembelian_PO_Barang_Lain spo
                        ON spo.Kode_Perusahaan = do_d.Kode_Perusahaan
                        AND spo.No_Faktur = do_d.No_PO
                    JOIN Barang_Lain b
                        ON b.Kode_Perusahaan = do_d.Kode_Perusahaan
                        AND b.Kode_Barang = do_d.Kode_Barang
                        AND b.Kode_Stock_Owner = do_d.Kode_Stock_Owner
                    WHERE
                        do.Kode_Perusahaan = '{KodePerusahaan}'
                        AND do.Status IS NULL
                        AND do_d.Flag_Pembelian IS NULL
                        AND spo.Status IS NULL
                        AND do.Flag_Retur IS NULL
                        AND do_d.Kode_Barang IN ({KodeBarang})
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get card offer outstanding
    Private Sub Get_Card_Offer_Outstanding()
        Try
            OpenConn()

            Guna2HtmlLabel5.Text = $"Total Barang: 0"
            Guna2HtmlLabel7.Text = 0

            SQL = $"
                WITH OutstandingPenawaran AS (
                    SELECT 
                        p.No_Faktur,
                        p.Kode_Supplier,
                        p.Periode_Akhir_Penawaran,
                        p.flag_release,
                        p.Selesai
                    FROM EMI_Master_Penawaran_Barang_Lain p
                    JOIN EMI_Master_Penawaran_Detail_Barang_Lain d
                        ON p.Kode_Perusahaan = d.Kode_Perusahaan
                        AND p.No_Faktur = d.No_Faktur
                    WHERE 
                        p.flag_release = 'Y'
                        AND p.Kode_Perusahaan = '{KodePerusahaan}'
                        AND p.Status IS NULL
                        AND CAST(GETDATE() AS DATE) between p.Tgl_Penawaran_Hrg and p.Periode_Akhir_Penawaran
                        AND d.Kode_Barang IN ({KodeBarang})
                )
                SELECT 
                    (SELECT COUNT(*) FROM OutstandingPenawaran) AS Total_Penawaran_Outstanding,
                    (
                        SELECT COUNT(DISTINCT d.Kode_Barang)
                        FROM EMI_Master_Penawaran_Detail_Barang_Lain d
                        INNER JOIN OutstandingPenawaran o
                            ON o.No_Faktur = d.No_Faktur
                        WHERE
                            d.Kode_Perusahaan = '{KodePerusahaan}'
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get card purchase value outstanding
    Private Sub Get_Card_Purchase_Value_Outstanding()
        Try
            OpenConn()

            SQL = $"
                SELECT 
                    CAST(ISNULL(SUM(t.Grand_Sebelum_PPN), 0) AS DECIMAL(18,2)) AS Total_Pembelian
                FROM EMI_Pembelian_PO_Barang_Lain t
                WHERE 
                    t.Status IS NULL
                    AND t.Kode_Perusahaan = '{KodePerusahaan}'
                    AND t.Tanggal_Release >= '{CutOffDate}';
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get PR item outstanding
    Private Sub Get_DGV_PR_Item_Outstanding()
        Try
            If DgvPermintaanBarang Is Nothing OrElse DgvPermintaanBarang.Columns.Count = 0 Then Return
            OpenConn()
            DgvPermintaanBarang.Rows.Clear()
            DgvPermintaanBarang.RowHeadersVisible = True
            DgvPermintaanBarang.RowHeadersWidth = 30
            DgvPermintaanBarang.AllowUserToResizeRows = False
            DgvPermintaanBarang.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            Dim statusPRParam As String = "NULL"
            Dim statusProsesParam As String = "NULL"

            Select Case CbPermintaanBarang.Text
                Case "Selesai"
                    statusPRParam = "'Selesai'"
                    statusProsesParam = "'Selesai'"
                Case Else
                    statusProsesParam = $"'{CbPermintaanBarang.Text}'"
            End Select

            Dim searchParam As String = "NULL"
            If Not String.IsNullOrEmpty(TbPermintaanBarang.Text.Trim()) Then
                searchParam = $"'{TbPermintaanBarang.Text.Trim().Replace("'", "''")}'"
            End If

            Dim tanggalDari As String = $"'{DtpTanggalAwal.Value.ToString("yyyy-MM-dd")}'"
            Dim tanggalSampai As String = $"'{DtpTanggalAkhir.Value.ToString("yyyy-MM-dd")}'"

            Dim kodeBarangParam As String = $"'{KodeBarang.Replace("'", "")}'"

            SQL = $"EXEC N_EMI_SP_Tracking_Purchase_Requisition_Barang_Lain
            @Kode_Perusahaan = '{KodePerusahaan}',
            @Status_PR = {statusPRParam},
            @Status_Proses = {statusProsesParam},
            @Tanggal_Release_Dari = {tanggalDari},
            @Tanggal_Release_Sampai = {tanggalSampai},
            @SearchText = {searchParam},
            @Kode_Barang = {kodeBarangParam}"

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim idx As Integer = DgvPermintaanBarang.Rows.Add(
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
                        With DgvPermintaanBarang.Rows(idx)
                            .DefaultCellStyle.BackColor = Color.FromArgb(17, 139, 80)
                            .DefaultCellStyle.ForeColor = Color.White
                        End With
                    ElseIf statusProses <> "Selesai" AndAlso sisaWaktu.StartsWith("Telat") Then
                        With DgvPermintaanBarang.Rows(idx)
                            .DefaultCellStyle.BackColor = Color.FromArgb(215, 53, 53)
                            .DefaultCellStyle.ForeColor = Color.White
                        End With
                    End If
                End While
            End Using

            DgvPermintaanBarang.ClearSelection()
            DGV_Empty_Placeholder1.Visible = (DgvPermintaanBarang.Rows.Count = 0)
            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get PR item lead time
    Private Sub Get_DGV_PR_Item_LeadTime()
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
                    FROM EMI_Purchase_Requisition_Barang_Lain pr
                    JOIN EMI_Purchase_Requisition_Barang_Lain_Detail pr_d 
                        ON pr_d.No_Faktur = pr.No_Faktur
                    LEFT JOIN EMI_Pembelian_PO_Det_Induk_Barang_Lain poi_d 
                        ON poi_d.No_Urut_PR = pr_d.No_Urut 
                        AND poi_d.Kode_Barang = pr_d.Kode_Barang
                    LEFT JOIN EMI_Pembelian_PO_Induk_Barang_Lain poi 
                        ON poi.No_Faktur = poi_d.No_Faktur
                    LEFT JOIN EMI_Pembelian_PO_Det_Barang_Lain spo_d 
                        ON spo_d.No_FakInduk = poi_d.No_Faktur 
                        AND spo_d.No_Urut_PR = pr_d.No_Urut 
                        AND spo_d.Kode_Barang = pr_d.Kode_Barang
                    LEFT JOIN EMI_Pembelian_PO_Barang_Lain spo 
                        ON spo.No_Faktur = spo_d.No_Faktur
                    LEFT JOIN EMI_Pembelian_Loading_Detail_Barang_Lain do_d 
                        ON do_d.No_PO = spo.No_Faktur 
                        AND do_d.Kode_Barang = pr_d.Kode_Barang
                    LEFT JOIN EMI_Pembelian_Loading_Barang_Lain do 
                        ON do.No_Faktur = do_d.No_Faktur
                    JOIN Barang_Lain b 
                        ON b.Kode_Barang = pr_d.Kode_Barang 
                        AND b.Kode_Stock_Owner = pr_d.Kode_Stock_Owner
                    WHERE
                        pr.Kode_Perusahaan = '001'
                        AND pr.Status IS NULL
                        AND pr.Flag_Release = 'Y'
                        AND poi.Status IS NULL
                        AND poi.Flag_Release = 'Y'
                        AND spo.Status IS NULL
                        AND spo.Flag_Release = 'Y'
                        AND pr.Tanggal_Release >= '{CutOffDate}'
                        AND pr_d.Kode_Barang IN ({KodeBarang})
                )
                SELECT
                    No_PR,
                    FORMAT(MIN(Tgl_PR), 'dd MMM yyyy', 'id-ID') AS Waktu_PR,
                    Kode_Barang,
                    Nama_Barang,
                    Satuan,
                    Qty_PR,
                    CASE 
                        WHEN SUM(Qty_PO_Induk) = SUM(Qty_PR) 
                            THEN DATEDIFF(DAY, MIN(Tgl_PR), MAX(Tgl_PO))
                        WHEN SUM(Qty_PO_Induk) < SUM(Qty_PR)
                            THEN DATEDIFF(DAY, MIN(Tgl_PR), GETDATE())
                        ELSE NULL
                    END AS Waktu_PR_PO,
                    CASE 
                        WHEN SUM(Qty_Sub_PO) = SUM(Qty_PO_Induk)
                            THEN DATEDIFF(DAY, MIN(Tgl_PO), MAX(Tgl_Sub_PO))
                        WHEN SUM(Qty_Sub_PO) < SUM(Qty_PO_Induk)
                            THEN DATEDIFF(DAY, MIN(Tgl_PO), GETDATE())
                        ELSE NULL
                    END AS Waktu_PO_SubPO,
                    CASE 
                        WHEN SUM(Qty_DO) = SUM(Qty_Sub_PO)
                            THEN DATEDIFF(DAY, MIN(Tgl_Sub_PO), MAX(Tgl_Masuk_DO))
                        WHEN SUM(Qty_DO) < SUM(Qty_Sub_PO)
                            THEN DATEDIFF(DAY, MIN(Tgl_Sub_PO), GETDATE())
                        ELSE NULL
                    END AS Waktu_SubPO_DO
                FROM Data
                WHERE
                    No_PR LIKE '%{TbLeadTime.Text}%'
                    OR Kode_Barang LIKE '%{TbLeadTime.Text}%'
                    OR Nama_Barang LIKE '%{TbLeadTime.Text}%'
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get PR item selisih penerimaan
    Private Sub Get_DGV_PR_Item_SelisihPenerimaan()
        Try
            If Guna2DataGridView1 Is Nothing OrElse Guna2DataGridView1.Columns.Count = 0 Then Return
            OpenConn()
            Guna2DataGridView1.Rows.Clear()
            Guna2DataGridView1.RowHeadersVisible = True
            Guna2DataGridView1.RowHeadersWidth = 30
            Guna2DataGridView1.AllowUserToResizeRows = False
            Guna2DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

            Dim statusFilter As String = ""
            Select Case CbSelisihPenerimaan.Text
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
                FROM EMI_Pembelian_Loading_Barang_Lain a
                INNER JOIN EMI_Pembelian_Loading_Detail_Barang_Lain b 
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan
                    AND a.No_Faktur = b.No_Faktur
                INNER JOIN Barang_Lain c 
                    ON b.Kode_Perusahaan = c.Kode_Perusahaan
                    AND b.Kode_Stock_Owner = c.Kode_Stock_Owner
                    AND b.Kode_Barang = c.Kode_Barang
                INNER JOIN EMI_Pembelian_PO_Detail_Barang_Lain d 
                    ON b.Kode_Perusahaan = d.Kode_Perusahaan
                    AND b.Urut_PO = d.No_Urut
                JOIN Suppliers s
                    ON a.Kode_Perusahaan = s.Kode_Perusahaan
                    AND a.Kode_Supplier = s.Kode_Supplier
                WHERE 
                    a.Kode_Perusahaan = '{KodePerusahaan}'
                    AND a.Status IS NULL
                    AND a.Tanggal_Masuk IS NOT NULL 
                    AND a.Flag_Timbang_Keluar = 'Y' 
                    AND (b.Jumlah_Barang - b.Jumlah_Masuk) <> 0
                    {statusFilter}
                    AND b.Kode_Barang IN ({KodeBarang})
                    AND (
                        b.No_Faktur LIKE '%{TbSelisihPenerimaan.Text}%'
                        OR c.Nama LIKE '%{TbSelisihPenerimaan.Text}%'
                        OR b.Kode_Barang LIKE '%{TbSelisihPenerimaan.Text}%'
                    )
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get grafik procurment
    Private Sub Get_ProcurmentChart_Data()
        Try
            OpenConn()

            Dim GetProcurmentHistoricalQuery = Function(table As String, additionalCondition As String) $"
                {MonthCTE}
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
                WHERE b.OffsetMonth BETWEEN {MountStartIndex} AND {MountEndIndex}
                GROUP BY b.OffsetMonth, b.BulanAwal
                ORDER BY b.OffsetMonth DESC;
            "

            Dim GetDOHistoricalQuery = $"
                {MonthCTE}
                SELECT 
                    FORMAT(b.BulanAwal, 'MMM') + ' ' + CAST(YEAR(b.BulanAwal) AS VARCHAR) AS Label,
                    ISNULL(COUNT(t.No_Faktur), 0) AS Value
                FROM BulanCTE b
                LEFT JOIN EMI_Pembelian_Loading_Barang_Lain t
                    ON YEAR(t.Tanggal_OTW) = YEAR(b.BulanAwal)
                    AND MONTH(t.Tanggal_OTW) = MONTH(b.BulanAwal)
                    AND t.Kode_Perusahaan = '{KodePerusahaan}'
                    AND t.Status IS NULL
                    AND t.Tanggal_Masuk IS NULL
                WHERE b.OffsetMonth BETWEEN {MountStartIndex} AND {MountEndIndex}
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

            Using Dr = OpenTrans(GetProcurmentHistoricalQuery("EMI_Purchase_Requisition_Barang_Lain", ""))
                While Dr.Read()
                    PRSplineDataset.DataPoints.Add(Dr("Label").ToString(), Convert.ToDecimal(Dr("Value")))
                End While
            End Using

            Using Dr = OpenTrans(GetProcurmentHistoricalQuery("EMI_Pembelian_PO_Induk_Barang_Lain", ""))
                While Dr.Read()
                    POSplineDataset.DataPoints.Add(Dr("Label").ToString(), Convert.ToDecimal(Dr("Value")))
                End While
            End Using

            Using Dr = OpenTrans(GetProcurmentHistoricalQuery("EMI_Pembelian_PO_Barang_Lain", ""))
                While Dr.Read()
                    SubPOSplineDataset.DataPoints.Add(Dr("Label").ToString(), Convert.ToDecimal(Dr("Value")))
                End While
            End Using

            Using Dr = OpenTrans(GetDOHistoricalQuery)
                While Dr.Read()
                    DOSplineDataset.DataPoints.Add(Dr("Label").ToString(), Convert.ToDecimal(Dr("Value")))
                End While
            End Using

            Using Dr = OpenTrans(GetProcurmentHistoricalQuery("EMI_Master_Penawaran_Barang_Lain", ""))
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get grafik purchase value
    Private Sub Get_PurchaseValueChart_Data()
        Try
            OpenConn()

            Dim GetPOValueHistoricalQuery = $"
                {MonthCTE}
                SELECT 
                    FORMAT(b.BulanAwal, 'MMM') + ' ' + CAST(YEAR(b.BulanAwal) AS VARCHAR) AS Label,
                    CAST(ISNULL(SUM(t.Grand_Sebelum_PPN), 0) AS DECIMAL(18,2)) AS Value
                FROM BulanCTE b
                LEFT JOIN EMI_Pembelian_PO_Barang_Lain t
                    ON YEAR(t.Tanggal_Release) = YEAR(b.BulanAwal)
                    AND MONTH(t.Tanggal_Release) = MONTH(b.BulanAwal)
                    AND t.Status IS NULL
                    AND t.Kode_Perusahaan = '{KodePerusahaan}'
                WHERE b.OffsetMonth BETWEEN {MountStartIndex} AND {MountEndIndex}
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get top 10 requested item
    Private Sub Get_DGV_Top10RequestedItem()
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

            Dim Query As String = $"
            SELECT TOP 10
                prd.Kode_Barang,
                b.Nama AS Nama_Barang,
                SUM(prd.Jumlah) AS Qty
            FROM EMI_Purchase_Requisition_Barang_Lain pr
            INNER JOIN EMI_Purchase_Requisition_Barang_Lain_Detail prd
                ON pr.No_Faktur = prd.No_Faktur
            INNER JOIN Barang_Lain b
                ON prd.Kode_Barang = b.Kode_Barang
                AND prd.Kode_Stock_Owner = b.Kode_Stock_Owner
            WHERE pr.Status IS NULL
                AND pr.Kode_Perusahaan = '{KodePerusahaan}'
                AND pr.Flag_Release = 'Y'
                AND pr.Tanggal_Release >= '{CutOffDate}'
                AND prd.Kode_Barang IN ({KodeBarang})
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Get top supplier
    Private Sub Get_DGV_Top10Suppliers()
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
                FROM EMI_Pembelian_Loading_Barang_Lain AS a
                INNER JOIN suppliers AS b
                    ON a.kode_supplier = b.kode_supplier
                WHERE a.tanggal_otw IS NOT NULL
                    AND a.Kode_Perusahaan = '{KodePerusahaan}'
                    AND a.tanggal_masuk IS NOT NULL
                    AND a.Status IS NULL
                    AND a.Tanggal >= '{CutOffDate}'
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
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Untuk mendapatkan data dashboard berdasarkan filter yang dipilih
    Private Sub Get_DashboardData()
        If KodeBarang = "" Then
            Exit Sub
        End If

        'Get card outstanding data
        Get_Card_PR_Outstanding()
        Get_Card_PO_Outstanding()
        Get_Card_Sub_PO_Outstanding()
        Get_Card_DO_Outstanding()
        Get_Card_Offer_Outstanding()
        Get_Card_Purchase_Value_Outstanding()

        'Get data tab control planning
        TcPlanning_GetData()
    End Sub

    'Untuk handle tab control planning ketika tab berubah agar data yang ditampilkan sesuai dengan tab yang dipilih
    Private Sub TcPlanning_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TcPlanning.SelectedIndexChanged
        TcPlanning_GetData()
    End Sub

    'Untuk handle tab control main ketika tab berubah
    Private Sub TcMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TcMain.SelectedIndexChanged
        TcMain_GetData()
    End Sub

    'Hanlder filter status PR item
    Private Sub CbPermintaanBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbPermintaanBarang.SelectedIndexChanged
        Get_DashboardData()
    End Sub

    'Handle filter selisih penerimaan
    Private Sub CbSelisihPenerimaan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbSelisihPenerimaan.SelectedIndexChanged
        Get_DashboardData()
    End Sub

    'Get ulang kode barang ketika kategori gudang berubah dan refresh data dashboard
    Private Sub CbKodeKategoriGudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbKodeKategoriGudang.SelectedIndexChanged
        CbKodeKategoriGudang.Enabled = False

        KodeBarang = Get_KodeBarang(KodePerusahaan, UserID)

        Get_DashboardData()

        CbKodeKategoriGudang.Enabled = True
    End Sub

    'Handle cellclick di dgv pr item outstanding untuk membuka detail pr
    Private Sub DgvPermintaanBarang_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPermintaanBarang.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DgvPermintaanBarang.Rows(e.RowIndex)
            Dim NoPR = selectedRow.Cells("Tab1_No_PR").Value.ToString()
            Dim KodeBarang = selectedRow.Cells("Tab1_Kode_Barang").Value.ToString()
            Dim NamaBarang = selectedRow.Cells("Tab1_Nama_Barang").Value.ToString()
            Dim QtyPR = selectedRow.Cells("Tab1_Qty_PR").Value.ToString()
            Dim QtyDO = selectedRow.Cells("Tab1_Qty_DO").Value.ToString()
            Dim TanggalRelease = selectedRow.Cells("Tab1_Waktu").Value.ToString()
            Dim User = selectedRow.Cells("User").Value.ToString()

            Dim SD As New N_EMI_SD_Procurement_Barang_Lain_Detail With {
                .NoPR = NoPR,
                .KodeBarang = KodeBarang,
                .NamaBarang = NamaBarang,
                .QtyPR = QtyPR,
                .QtyDO = QtyDO,
                .TanggalRelease = TanggalRelease,
                .User = User,
                .StartPosition = FormStartPosition.CenterScreen
            }

            SD.ShowDialog()
        End If
    End Sub

    'Handle card pr outstanding click
    Private Sub CardPROutstanding_Click(sender As Object, e As EventArgs) Handles CardPROutstanding.Click
        Dim SD As New N_EMI_SD_Procurement_Barang_Lain_PR With {
            .StartPosition = FormStartPosition.CenterScreen,
            .FilterKodeBarang = KodeBarang
        }

        SD.ShowDialog()
    End Sub

    'Handle card po outstanding click
    Private Sub CardPOOutstanding_Click(sender As Object, e As EventArgs) Handles CardPOOutstanding.Click
        Dim SD As New N_EMI_SD_Procurement_Barang_Lain_PO With {
            .StartPosition = FormStartPosition.CenterScreen,
            .FilterKodeBarang = KodeBarang
        }

        SD.ShowDialog()
    End Sub

    'Handle card sub po outstanding click
    Private Sub CardSubPOOutstanding_Click(sender As Object, e As EventArgs) Handles CardSubPOOutstanding.Click
        Dim SD As New N_EMI_SD_Procurement_Barang_Lain_SubPO With {
            .StartPosition = FormStartPosition.CenterScreen,
            .FilterKodeBarang = KodeBarang
        }

        SD.ShowDialog()
    End Sub

    'Handle card do outstanding click
    Private Sub CardDOOutstanding_Click(sender As Object, e As EventArgs) Handles CardDOOutstanding.Click
        Dim SD As New N_EMI_SD_Procurement_Barang_Lain_Loading With {
            .StartPosition = FormStartPosition.CenterScreen,
            .FilterKodeBarang = KodeBarang
        }

        SD.ShowDialog()
    End Sub

    'Handle card offer outstanding click
    Private Sub CardOfferOutstanding_Click(sender As Object, e As EventArgs) Handles CardOfferOutstanding.Click
        Dim SD As New N_EMI_SD_Procurement_Barang_Lain_Penawaran With {
            .StartPosition = FormStartPosition.CenterScreen,
            .FilterKodeBarang = KodeBarang
        }

        SD.ShowDialog()
    End Sub

    'Handle card purchase value outstanding click
    Private Sub CardPurchaseValue_Click(sender As Object, e As EventArgs) Handles CardPurchaseValue.Click
        Dim SD As New N_EMI_SD_Procurement_Barang_Lain_POValue With {
            .StartPosition = FormStartPosition.CenterScreen
        }

        SD.ShowDialog()
    End Sub

    'Handle get data tab control planning sesuai tab
    Private Sub TcPlanning_GetData()
        Select Case TcPlanning.SelectedIndex
            Case 0
                Get_DGV_PR_Item_Outstanding()
            Case 1
                Get_DGV_PR_Item_LeadTime()
            Case 2
                Get_DGV_PR_Item_SelisihPenerimaan()
        End Select
    End Sub

    'Handle get data tab control main sesuai tab
    Private Sub TcMain_GetData()
        Select Case TcMain.SelectedIndex
            Case 1
                Get_ProcurmentChart_Data()
                Get_PurchaseValueChart_Data()
            Case 2
                Get_DGV_Top10RequestedItem()
                Get_DGV_Top10Suppliers()
        End Select
    End Sub

    'Untuk format angka menjadi format pendek dengan satuan (Juta, Miliar, Triliun)
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

    'Handle filter tanggal awal
    Private Sub DtpTanggalAwal_ValueChanged(sender As Object, e As EventArgs) Handles DtpTanggalAwal.ValueChanged
        If DtpTanggalAwal.Value > DtpTanggalAkhir.Value Then
            DtpTanggalAkhir.Value = DtpTanggalAwal.Value
        End If

        TcPlanning_GetData()
    End Sub

    'Handle filter tanggal akhir
    Private Sub DtpTanggalAkhir_ValueChanged(sender As Object, e As EventArgs) Handles DtpTanggalAkhir.ValueChanged
        If DtpTanggalAkhir.Value < DtpTanggalAwal.Value Then
            DtpTanggalAwal.Value = DtpTanggalAkhir.Value
        End If

        TcPlanning_GetData()
    End Sub

    'Handle export dgv pr item outstanding ke excel
    Private Sub BtnExportExcelPermintaanBarang_Click(sender As Object, e As EventArgs) Handles BtnExportExcelPermintaanBarang.Click
        If DgvPermintaanBarang.Rows.Count = 0 Then
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
        Dim nama_file As String = "PR_Outstanding_Summary_Barang_Lain_" & format_akhir & ".xlsx"
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("Sheet1")
        xlApp.ScreenUpdating = False
        xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

        Try
            For colIndex As Integer = 0 To DgvPermintaanBarang.Columns.Count - 1
                xlWorkSheet.Cells(1, colIndex + 1).Value = DgvPermintaanBarang.Columns(colIndex).HeaderText
            Next

            Dim rowIndex As Integer = 2
            Dim rows = DgvPermintaanBarang.Rows.Count
            Dim cols = DgvPermintaanBarang.Columns.Count

            If rows > 0 Then
                Dim dataArr(rows - 1, cols - 1) As Object

                For r As Integer = 0 To rows - 1
                    For c As Integer = 0 To cols - 1
                        Dim value = DgvPermintaanBarang.Rows(r).Cells(c).Value
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

            Dim dataRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(rowIndex - 1, DgvPermintaanBarang.Columns.Count))
            With dataRange.Borders
                .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
            End With

            Dim headerRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, DgvPermintaanBarang.Columns.Count))
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
                MsgBox("PR Outstanding Summary Barang Lain berhasil di-export!", MsgBoxStyle.Information)
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
End Class