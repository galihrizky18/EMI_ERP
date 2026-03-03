Imports System.Data.SqlClient
Imports Guna.UI2.WinForms
Public Class N_EMI_SD_Procurement_SubPO
    Public Property IDGroupJenis As Integer

    'Waktu debounce, searching state dan minimal panjang karakter untuk pencarian
    Private Const SEARCH_DEBOUNCE As Integer = 250
    Private Const SEARCH_MIN_LENGTH As Integer = 1
    Private IS_SEARCHING As Boolean = False

    'Timer untuk debounce pencarian
    Private WithEvents SEARCH_TIMER As New Timer With {.Interval = SEARCH_DEBOUNCE}

    Private HAS_ETD_VALUE As Boolean = False
    Private HAS_ETA_VALUE As Boolean = False

    Private Sub SD_Card_Sub_PO_Outstanding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()
            If CekButtonRole("Ubah_ETD_ETA_Sub_PO") = "T" Then
                Button_Simpan.Enabled = False
            End If
        Catch ex As Exception
            MsgBox($"Error: {ex.Message}", MsgBoxStyle.Critical)
        End Try


        AddHandler TB_Search.TextChanged, AddressOf TB_Search_TextChanged
        AddHandler CB_Status.SelectedIndexChanged, AddressOf CB_Status_SelectedIndexChanged

        AddHandler Guna2DateTimePicker2.ValueChanged, AddressOf Guna2DateTimePicker2_ValueChanged
        AddHandler Guna2DateTimePicker2.DropDown, AddressOf Guna2DateTimePicker2_DropDown

        ClearFormInputs()

        Fetch_Sub_PO_Outstanding()

        With ListView1
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .Columns.Clear()
        End With
    End Sub

    Private Sub Guna2DateTimePicker2_DropDown(sender As Object, e As EventArgs)
        If Not HAS_ETA_VALUE Then
            Guna2DateTimePicker2.Format = DateTimePickerFormat.Long
            Guna2DateTimePicker2.Value = DateTime.Now.Date
        End If
    End Sub

    Private Sub Guna2DateTimePicker2_ValueChanged(sender As Object, e As EventArgs)
        If Guna2DateTimePicker2.Format <> DateTimePickerFormat.Custom Then
            HAS_ETA_VALUE = True
        End If
    End Sub

    Private Sub ClearFormInputs()
        Guna2Panel4.Visible = False
        No_Sub_PO.Text = ""

        Guna2DateTimePicker1.Format = DateTimePickerFormat.Long
        Guna2DateTimePicker1.Value = DateTime.Now.Date
        HAS_ETD_VALUE = True

        Guna2DateTimePicker2.Format = DateTimePickerFormat.Custom
        Guna2DateTimePicker2.CustomFormat = " "
        HAS_ETA_VALUE = False
    End Sub

    Private Sub SetDateTimePickerValue(dtp As Guna2DateTimePicker, ByRef hasValue As Boolean, value As Object, isETD As Boolean)
        If IsDBNull(value) OrElse value Is Nothing Then
            If isETD Then
                dtp.Format = DateTimePickerFormat.Long
                dtp.Value = DateTime.Now.Date
                hasValue = True
            Else
                dtp.Format = DateTimePickerFormat.Custom
                dtp.CustomFormat = " "
                hasValue = False
            End If
        Else
            dtp.Format = DateTimePickerFormat.Long
            dtp.Value = Convert.ToDateTime(value)
            hasValue = True
        End If
    End Sub

    Private Sub Guna2DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView2.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim row = Guna2DataGridView2.Rows(e.RowIndex)

                No_Sub_PO.Text = If(IsDBNull(row.Cells("NoSubPO").Value), "", row.Cells("NoSubPO").Value.ToString())
                Guna2Panel4.Visible = True

                If e.ColumnIndex = 10 Then
                    Guna2HtmlLabel5.Text = "Riwayat Perubahan ETD"
                    Fetch_ETD_Log()
                End If

                If e.ColumnIndex = 12 Then
                    Guna2HtmlLabel5.Text = "Riwayat Perubahan ETA"
                    Fetch_ETA_Log()
                End If

                Dim noPO As String = No_Sub_PO.Text
                If Not String.IsNullOrEmpty(noPO) Then
                    Try
                        OpenConn()
                        SQL = $"SELECT ETD_Simulasi, ETA FROM EMI_Pembelian_PO WHERE No_Faktur = '{noPO}'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read() Then
                                SetDateTimePickerValue(Guna2DateTimePicker1, HAS_ETD_VALUE, Dr("ETD_Simulasi"), True)

                                SetDateTimePickerValue(Guna2DateTimePicker2, HAS_ETA_VALUE, Dr("ETA"), False)
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
        SEARCH_TIMER.Stop()
        SEARCH_TIMER.Start()
    End Sub

    Private Sub SearchTimer_Tick(sender As Object, e As EventArgs) Handles SEARCH_TIMER.Tick
        SEARCH_TIMER.Stop()
        Fetch_Sub_PO_Outstanding()
    End Sub

    Private Sub CB_Status_SelectedIndexChanged(sender As Object, e As EventArgs)
        Fetch_Sub_PO_Outstanding()
    End Sub

    Private Sub Fetch_Sub_PO_Outstanding()
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
                    statusFilter = "AND DATEDIFF(DAY, GETDATE(), spo.ETD_Simulasi) < 0"
                Case "ON SCHEDULE"
                    statusFilter = "AND DATEDIFF(DAY, GETDATE(), spo.ETD_Simulasi) >= 0"
            End Select

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
                        AND pr_d.Kode_Perusahaan = pr.Kode_Perusahaan
                    WHERE 
                        pr.Status IS NULL
                        AND pr.Flag_Release = 'Y'
                        AND pr.Kode_Perusahaan = '{KodePerusahaan}'
                    GROUP BY pr_d.Kode_Perusahaan, pr_d.No_Faktur, pr_d.No_Urut, pr_d.Kode_Barang, pr_d.Kode_Stock_Owner
                ),
                ETD_Log AS (
                    SELECT
                        Kode_Perusahaan,
                        No_Faktur,
                        MAX(CASE WHEN rn_asc = 1 THEN COALESCE(ETD_Lama, ETD_Baru) END) AS ETD_Ori_Log,
                        MAX(CASE WHEN rn_desc = 1 AND ETD_Lama IS NOT NULL THEN ETD_Baru END) AS ETD_Update_Log
                    FROM (
                        SELECT
                            Kode_Perusahaan, No_Faktur, ETD_Lama, ETD_Baru, No_Urut,
                            ROW_NUMBER() OVER (PARTITION BY Kode_Perusahaan, No_Faktur ORDER BY No_Urut ASC) rn_asc,
                            ROW_NUMBER() OVER (PARTITION BY Kode_Perusahaan, No_Faktur ORDER BY No_Urut DESC) rn_desc
                        FROM N_EMI_LOG_ETD_ETA_Pembelian_PO
                        WHERE ETD_Baru IS NOT NULL
                    ) x
                    GROUP BY Kode_Perusahaan, No_Faktur
                ),
                ETA_Log AS (
                    SELECT
                        Kode_Perusahaan,
                        No_Faktur,
                        MAX(CASE WHEN rn_asc = 1 THEN COALESCE(ETA_Lama, ETA_Baru) END) AS ETA_Ori_Log,
                        MAX(CASE WHEN rn_desc = 1 AND ETA_Lama IS NOT NULL THEN ETA_Baru END) AS ETA_Update_Log
                    FROM (
                        SELECT
                            Kode_Perusahaan, No_Faktur, ETA_Lama, ETA_Baru, No_Urut,
                            ROW_NUMBER() OVER (PARTITION BY Kode_Perusahaan, No_Faktur ORDER BY No_Urut ASC) rn_asc,
                            ROW_NUMBER() OVER (PARTITION BY Kode_Perusahaan, No_Faktur ORDER BY No_Urut DESC) rn_desc
                        FROM N_EMI_LOG_ETD_ETA_Pembelian_PO
                        WHERE ETA_Baru IS NOT NULL
                    ) x
                    GROUP BY Kode_Perusahaan, No_Faktur
                )
                SELECT 
                    pr.No_Faktur AS No_PR,
                    spo.No_Faktur AS No_Sub_PO,
                    spo.User_Release,
                    FORMAT(spo.Tanggal_Release, 'dd MMM yyyy') AS Tanggal_Sub_PO,
                    CASE 
                        WHEN spo.ETD_Simulasi >= CAST(GETDATE() AS DATE)
                            THEN CAST(DATEDIFF(DAY, CAST(GETDATE() AS DATE), spo.ETD_Simulasi) AS VARCHAR) + ' hari lagi'
                        ELSE 
                            'Telat ' + CAST(DATEDIFF(DAY, spo.ETD_Simulasi, CAST(GETDATE() AS DATE)) AS VARCHAR) + ' hari'
                    END AS Sisa_Waktu_Sub_PO,
                    spo_d.Kode_Barang,
                    spo_d.Satuan,
                    b.Nama AS Nama_Barang,
                    spo_d.Jumlah AS Qty_Sub_PO,
                    ISNULL(ds.Total_DO, 0) AS Qty_Sudah_DO,
                    spo_d.Jumlah - ISNULL(ds.Total_DO, 0) AS Qty_Outstanding,
                    ISNULL(
                        FORMAT(
                            COALESCE(etd.ETD_Ori_Log, spo.ETD_Simulasi),
                            'dd MMM yyyy'
                        ),
                        '-'
                    ) AS ETD_Ori,
                    ISNULL(
                        FORMAT(
                            COALESCE(etd.ETD_Update_Log, NULL),
                            'dd MMM yyyy'
                        ),
                        '-'
                    ) AS ETD_Update,
                    ISNULL(
                        FORMAT(
                            COALESCE(eta.ETA_Ori_Log, spo.ETA),
                            'dd MMM yyyy'
                        ),
                        '-'
                    ) AS ETA_Ori,
                    ISNULL(
                        FORMAT(
                            COALESCE(eta.ETA_Update_Log, NULL),
                            'dd MMM yyyy'
                        ),
                        '-'
                    ) AS ETA_Update,
                    spo_d.No_Urut,
                    spo_d.No_FakInduk,
                    spo_d.Nilai_Barang,
                    spo_d.Harga_Barang,
                    spo_d.Jumlah_Input,
                    spo_d.urut_det_induk
                FROM EMI_Pembelian_PO spo
                INNER JOIN EMI_Pembelian_PO_Det spo_d 
                    ON spo_d.No_Faktur = spo.No_Faktur
                    AND spo_d.Kode_Perusahaan = spo.Kode_Perusahaan
                INNER JOIN EMI_Pembelian_PO_Induk poi
                    ON poi.No_Faktur = spo_d.No_FakInduk
                    AND poi.Kode_Perusahaan = spo.Kode_Perusahaan
                INNER JOIN PR pr 
                    ON pr.No_Urut = spo_d.No_Urut_PR
                    AND pr.Kode_Barang = spo_d.Kode_Barang
                    AND pr.Kode_Perusahaan = spo_d.Kode_Perusahaan
                LEFT JOIN DO_Sum ds 
                    ON ds.No_PO = spo.No_Faktur 
                    AND ds.Kode_Barang = spo_d.Kode_Barang
                LEFT JOIN Barang b 
                    ON b.Kode_Barang = spo_d.Kode_Barang 
                    AND b.Kode_Stock_Owner = spo_d.Kode_Stock_Owner
                LEFT JOIN ETD_Log etd 
                    ON etd.Kode_Perusahaan = spo.Kode_Perusahaan
                    AND etd.No_Faktur = spo.No_Faktur
                LEFT JOIN ETA_Log eta 
                    ON eta.Kode_Perusahaan = spo.Kode_Perusahaan
                    AND eta.No_Faktur = spo.No_Faktur
                WHERE spo.Kode_Perusahaan = '{KodePerusahaan}'
                    AND poi.Status IS NULL
                    AND poi.Flag_Release = 'Y'
                    AND spo.Status IS NULL
                    AND spo.Flag_Release = 'Y'
                    AND spo.Flag_selesai_PO IS NULL
                    AND spo_d.Jumlah - ISNULL(ds.Total_DO, 0) > 0
                    {If(IDGroupJenis <> 0, $" AND b.Id_Group_Jenis = {IDGroupJenis} ", "")}
                    AND (
                        spo.No_Faktur LIKE '%{TB_Search.Text}%'
                        OR pr.No_Faktur LIKE '%{TB_Search.Text}%'
                        OR b.Kode_Barang LIKE '%{TB_Search.Text}%'
                        OR b.Nama LIKE '%{TB_Search.Text}%'
                    )
                    {statusFilter}
                ORDER BY spo.No_Faktur, spo_d.Kode_Barang;
            "

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim idx As Integer = Guna2DataGridView2.Rows.Add(
                        Dr("No_PR"),
                        Dr("No_Sub_PO"),
                        Dr("Tanggal_Sub_PO"),
                        Dr("Kode_Barang"),
                        Dr("Nama_Barang"),
                        Dr("Satuan"),
                        Dr("Qty_Sub_PO"),
                        Dr("Qty_Sudah_DO"),
                        Dr("Qty_Outstanding"),
                        Dr("ETD_Ori"),
                        Dr("ETD_Update"),
                        Dr("ETA_Ori"),
                        Dr("ETA_Update"),
                        Dr("Sisa_Waktu_Sub_PO"),
                        Dr("User_Release"),
                        Dr("No_Urut"),
                        Dr("No_FakInduk"),
                        Dr("Nilai_Barang"),
                        Dr("Harga_Barang"),
                        Dr("Jumlah_Input"),
                        Dr("urut_det_induk")
                    )

                    Dim sisaWaktu As String = Dr("Sisa_Waktu_Sub_PO").ToString()
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

    Private Sub Button_Simpan_Click(sender As Object, e As EventArgs) Handles Button_Simpan.Click
        Try
            OpenConn()
            If CekButtonRole("Ubah_ETD_ETA_Sub_PO") = "T" Then
                MsgBox("Anda tidak memiliki akses untuk melakukan tindakan ini!")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        Try
            If String.IsNullOrEmpty(No_Sub_PO.Text) Then
                MessageBox.Show("Pilih Sub PO terlebih dahulu dari data grid!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim noPO As String = No_Sub_PO.Text
            Dim newETD As DateTime = Guna2DateTimePicker1.Value.Date
            Dim newETA As DateTime? = Nothing

            If HAS_ETA_VALUE Then
                newETA = Guna2DateTimePicker2.Value.Date
            End If

            If HAS_ETA_VALUE Then
                If newETA.Value < newETD Then
                    MessageBox.Show("ETA tidak boleh lebih kecil dari ETD!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            OpenConn()

            Dim oldETD As DateTime? = Nothing
            Dim oldETA As DateTime? = Nothing

            SQL = $"SELECT ETD_Simulasi, ETA FROM EMI_Pembelian_PO WHERE No_Faktur = '{noPO}'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    If Not IsDBNull(Dr("ETD_Simulasi")) AndAlso Dr("ETD_Simulasi") IsNot Nothing Then
                        oldETD = Convert.ToDateTime(Dr("ETD_Simulasi")).Date
                    End If
                    If Not IsDBNull(Dr("ETA")) AndAlso Dr("ETA") IsNot Nothing Then
                        oldETA = Convert.ToDateTime(Dr("ETA")).Date
                    End If
                End If
            End Using

            Dim hasETDChanged As Boolean = False
            Dim hasETAChanged As Boolean = False

            If oldETD.HasValue Then
                hasETDChanged = (oldETD.Value <> newETD)
            Else
                hasETDChanged = True
            End If

            If oldETA.HasValue AndAlso newETA.HasValue Then
                hasETAChanged = (oldETA.Value <> newETA.Value)
            ElseIf oldETA.HasValue AndAlso Not newETA.HasValue Then
                hasETAChanged = True
            ElseIf Not oldETA.HasValue AndAlso newETA.HasValue Then
                hasETAChanged = True
            End If

            If Not hasETDChanged AndAlso Not hasETAChanged Then
                CloseConn()
                Fetch_Sub_PO_Outstanding()
                ClearFormInputs()
                MessageBox.Show("Data berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            SQL = $"SELECT DISTINCT No_FakInduk FROM EMI_Pembelian_PO_Det WHERE No_Faktur = '{noPO}'"
            Dim listNoFakInduk As New List(Of String)

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    If Not IsDBNull(Dr("No_FakInduk")) AndAlso Not String.IsNullOrEmpty(Dr("No_FakInduk").ToString()) Then
                        listNoFakInduk.Add(Dr("No_FakInduk").ToString())
                    End If
                End While
            End Using

            If listNoFakInduk.Count > 0 Then
                Dim inClause As String = String.Join("','", listNoFakInduk)
                SQL = $"SELECT MIN(ETD_Simulasi) as MinETD FROM EMI_Pembelian_PO WHERE No_Faktur IN ('{inClause}')"

                Using Dr = OpenTrans(SQL)
                    If Dr.Read() Then
                        If Not IsDBNull(Dr("MinETD")) AndAlso Dr("MinETD") IsNot Nothing Then
                            Dim minETDInduk As DateTime = Convert.ToDateTime(Dr("MinETD")).Date

                            If newETD < minETDInduk Then
                                CloseConn()
                                MessageBox.Show($"ETD Sub PO tidak boleh lebih kecil dari ETD terkecil PO Induk!{vbCrLf}" &
                                          $"ETD terkecil PO Induk: {minETDInduk:dd MMM yyyy}{vbCrLf}" &
                                          $"ETD yang Anda input: {newETD:dd MMM yyyy}",
                                          "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            End If
                        End If
                    End If
                End Using
            End If

            If HAS_ETA_VALUE Then
                SQL = $"UPDATE EMI_Pembelian_PO SET ETD_Simulasi = '{newETD:yyyy-MM-dd}', ETA = '{newETA.Value:yyyy-MM-dd}' WHERE No_Faktur = '{noPO}'"
            Else
                SQL = $"UPDATE EMI_Pembelian_PO SET ETD_Simulasi = '{newETD:yyyy-MM-dd}', ETA = NULL WHERE No_Faktur = '{noPO}'"
            End If
            ExecuteTrans(SQL)

            If hasETDChanged OrElse hasETAChanged Then
                Dim currentDate As DateTime = DateTime.Now
                Dim currentTime As String = currentDate.ToString("HH:mm:ss")

                Dim etdLamaStr As String = "NULL"
                Dim etaLamaStr As String = "NULL"
                Dim etdBaruStr As String = "NULL"
                Dim etaBaruStr As String = "NULL"

                If hasETDChanged Then
                    If oldETD.HasValue Then
                        etdLamaStr = $"'{oldETD.Value:yyyy-MM-dd}'"
                    Else
                        etdLamaStr = "NULL"
                    End If
                    etdBaruStr = $"'{newETD:yyyy-MM-dd}'"
                End If

                If hasETAChanged Then
                    If oldETA.HasValue Then
                        etaLamaStr = $"'{oldETA.Value:yyyy-MM-dd}'"
                    Else
                        etaLamaStr = "NULL"
                    End If

                    If newETA.HasValue Then
                        etaBaruStr = $"'{newETA.Value:yyyy-MM-dd}'"
                    Else
                        etaBaruStr = "NULL"
                    End If
                End If

                SQL = $"INSERT INTO N_EMI_LOG_ETD_ETA_Pembelian_PO 
                        (Kode_Perusahaan, Tanggal, Jam, UserID, No_Faktur, ETD_Lama, ETA_Lama, ETD_Baru, ETA_Baru) 
                        VALUES 
                        ('{KodePerusahaan}', '{currentDate:yyyy-MM-dd HH:mm:ss}', '{currentTime}', '{UserID}', '{noPO}', 
                         {etdLamaStr}, {etaLamaStr}, {etdBaruStr}, {etaBaruStr})"
                ExecuteTrans(SQL)
            End If

            CloseConn()

            Fetch_Sub_PO_Outstanding()
            ClearFormInputs()

            MessageBox.Show("Data berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error saat menyimpan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fetch_ETD_Log()
        Try
            OpenConn()

            SQL = $"SELECT 
                    ETD_Baru, 
                    UserID, 
                    Tanggal
                FROM N_EMI_LOG_ETD_ETA_Pembelian_PO
                WHERE No_Faktur = '{No_Sub_PO.Text}'
                  AND ETD_Baru IS NOT NULL
                  AND ETD_Lama IS NOT NULL
                ORDER BY No_Urut DESC"

            Using Dr = OpenTrans(SQL)

                ListView1.Items.Clear()
                ListView1.Columns.Clear()

                ListView1.View = View.Details
                ListView1.FullRowSelect = True
                ListView1.GridLines = True

                Dim rowItem As New ListViewItem()
                Dim colIndex As Integer = 0

                While Dr.Read()
                    Dim tanggal As Date = Dr("Tanggal")
                    Dim etd As Date = Dr("ETD_Baru")
                    Dim userId As String = Dr("UserID")

                    ListView1.Columns.Add(
                        "Diubah Pada " & tanggal.ToString("dd MMM yy"),
                        180,
                        HorizontalAlignment.Center
                    )

                    Dim cellText As String =
                    $"ETD: {etd:dd MMM yy} | User: {userId}"

                    If colIndex = 0 Then
                        rowItem.Text = cellText
                    Else
                        rowItem.SubItems.Add(cellText)
                    End If

                    colIndex += 1
                End While

                If colIndex > 0 Then
                    ListView1.Items.Add(rowItem)
                End If

            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(
                "Error mengambil data: " & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try
    End Sub

    Private Sub Fetch_ETA_Log()
        Try
            OpenConn()

            SQL = $"SELECT 
                    ETA_Baru, 
                    UserID, 
                    Tanggal
                FROM N_EMI_LOG_ETD_ETA_Pembelian_PO
                WHERE No_Faktur = '{No_Sub_PO.Text}'
                  AND ETA_Baru IS NOT NULL
                  AND ETA_Lama IS NOT NULL
                ORDER BY No_Urut DESC"

            Using Dr = OpenTrans(SQL)

                ListView1.Items.Clear()
                ListView1.Columns.Clear()

                ListView1.View = View.Details
                ListView1.FullRowSelect = True
                ListView1.GridLines = True

                Dim rowItem As New ListViewItem()
                Dim colIndex As Integer = 0

                While Dr.Read()
                    Dim tanggal As Date = Dr("Tanggal")
                    Dim etd As Date = Dr("ETA_Baru")
                    Dim userId As String = Dr("UserID")

                    ListView1.Columns.Add(
                        "Diubah Pada " & tanggal.ToString("dd MMM yy"),
                        180,
                        HorizontalAlignment.Center
                    )

                    Dim cellText As String =
                    $"ETA: {etd:dd MMM yy} | User: {userId}"

                    If colIndex = 0 Then
                        rowItem.Text = cellText
                    Else
                        rowItem.SubItems.Add(cellText)
                    End If

                    colIndex += 1
                End While

                If colIndex > 0 Then
                    ListView1.Items.Add(rowItem)
                End If

            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(
                "Error mengambil data: " & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
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
        Dim nama_file As String = "Sub_PO_Outstanding_" & format_akhir & ".xlsx"
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("Sheet1")
        xlApp.ScreenUpdating = False
        xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

        Try
            For colIndex As Integer = 0 To Guna2DataGridView2.Columns.Count - 7
                xlWorkSheet.Cells(1, colIndex + 1).Value = Guna2DataGridView2.Columns(colIndex).HeaderText
            Next

            Dim rowIndex As Integer = 2
            Dim rows = Guna2DataGridView2.Rows.Count
            Dim cols = Guna2DataGridView2.Columns.Count - 6

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
                    "D2:F" & lastRow & ";" &
                    "N2:N" & lastRow

                xlWorkSheet.Range(rangeText).NumberFormat = "@"
                xlWorkSheet.Range(rangeText).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft


                Dim rangeDate As String =
                    "C2:C" & lastRow & ";" &
                    "J2:M" & lastRow

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

            Dim dataRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(rowIndex - 1, Guna2DataGridView2.Columns.Count - 6))
            With dataRange.Borders
                .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
            End With

            Dim headerRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, Guna2DataGridView2.Columns.Count - 6))
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
                MsgBox("Sub PO Outstanding berhasil di-export!", MsgBoxStyle.Information)
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

    Private Sub DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) _
    Handles Guna2DataGridView2.CellMouseDown

        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            Guna2DataGridView2.ClearSelection()
            Guna2DataGridView2.Rows(e.RowIndex).Selected = True
            Guna2DataGridView2.CurrentCell = Guna2DataGridView2.Rows(e.RowIndex).Cells(0)

            Guna2DataGridView2.ContextMenuStrip = ContextMenuStrip_DGV_Sub_PO_Outstanding
        End If
    End Sub

    Private Sub SelesaikanSubPO_Click(sender As Object, e As EventArgs) Handles CMS_TSMI_Selesaikan_Sub_PO.Click
        If Guna2DataGridView2.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih data Sub PO terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row As DataGridViewRow = Guna2DataGridView2.SelectedRows(0)
        Dim NoSubPO As String = row.Cells("NoSubPO").Value?.ToString()
        Dim NoFakInduk As String = row.Cells("NoPOInduk").Value?.ToString()

        If String.IsNullOrEmpty(NoSubPO) Then
            MessageBox.Show("Data Sub PO tidak ditemukan.", "Error")
            Exit Sub
        End If

        Dim result As DialogResult = MessageBox.Show(
        $"Yakin ingin menyelesaikan SELURUH barang dalam Sub PO {NoSubPO} ?",
        "Konfirmasi",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If result <> DialogResult.Yes Then Exit Sub

        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            If CekButtonRole("Selesai_Sub_PO") = "T" Then
                CloseTransB2B()
                CloseConnB2B()
                MsgBox("Anda tidak memiliki akses menyelesaikan Sub PO.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            ''Cek apakah Sub PO ada di B2B
            Dim SQL_CheckB2B_DO As String = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO 
            WHERE Kode_Perusahaan = @KodePerusahaan 
              AND No_Faktur = @NoSubPO
        "

            CmdB2B.Parameters.Clear()
            CmdB2B.CommandText = SQL_CheckB2B_DO
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)

            Dim B2B_Sub_PO As Integer = Convert.ToInt32(CmdB2B.ExecuteScalar())

            If B2B_Sub_PO = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data tidak ditemukan!")
                Exit Sub
            End If

            ' ============================================
            ' BACKUP 1: EMI_Pembelian_PO ke EMI_Pembelian_PO_Origin
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPO = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePO = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO As String = "
                    INSERT INTO EMI_Pembelian_PO_Origin (
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
                        No_Faktur_Induk,
                        Grand_PPH,
                        --UserID_Selesai,
                        --Tanggal_Selesai,
                        --Jam_Selesai,
                        ETA,
                        UserID_Batal,
                        Tanggal_Batal,
                        Jam_Batal
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
                        No_Faktur_Induk,
                        Grand_PPH,
                        --UserID_Selesai,
                        --Tanggal_Selesai,
                        --Jam_Selesai,
                        ETA,
                        UserID_Batal,
                        Tanggal_Batal,
                        Jam_Batal
                    FROM EMI_Pembelian_PO
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoSubPO
                    "

                ExecuteTrans(SQL_Backup_PO)
            End If

            ' ============================================
            ' BACKUP 2: EMI_Pembelian_PO_Det ke EMI_Pembelian_PO_Det_Origin
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Det_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODet = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO_Det WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePODet = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO_Det As String = "
                    INSERT INTO EMI_Pembelian_PO_Det_Origin (
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
                        No_FakInduk,
                        urut_det_induk,
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
                        No_FakInduk,
                        urut_det_induk,
                        Jumlah_Input,
                        Satuan_Input
                    FROM EMI_Pembelian_PO_Det
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoSubPO
                    "

                ExecuteTrans(SQL_Backup_PO_Det)
            End If

            ' ============================================
            ' BACKUP 3: EMI_Pembelian_PO_Detail ke EMI_Pembelian_PO_Detail_Origin
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Detail_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODetail = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO_Detail WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePODetail = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO_Detail As String = "
                    INSERT INTO EMI_Pembelian_PO_Detail_Origin (
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
                    FROM EMI_Pembelian_PO_Detail
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoSubPO
                    "

                ExecuteTrans(SQL_Backup_PO_Detail)
            End If

            ' ============================================
            ' Ambil data detail untuk proses update
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Dim SQL As String = "
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
                        AND do.Kode_Perusahaan = @KodePerusahaan
                        AND do.Status IS NULL
                        AND do.Flag_Retur IS NULL
                )
                GROUP BY do_d.No_PO, do_d.Kode_Barang
            )
            SELECT 
                epd.Kode_Barang,
                epd.No_Urut,
                epd.Jumlah AS Qty_SubPO,
                ISNULL(ds.Total_DO, 0) AS Qty_DO,
                epd.Nilai_Barang,
                epd.Harga_Barang,
                epd.Jumlah_Input,
                epd.No_FakInduk,
                epd.urut_det_induk AS NoUrutDetInduk
            FROM emi_pembelian_po_det epd
            LEFT JOIN DO_Sum ds 
                ON ds.No_PO = epd.No_Faktur 
                AND ds.Kode_Barang = epd.Kode_Barang
            WHERE 
                epd.Kode_Perusahaan = @KodePerusahaan 
                AND epd.No_Faktur = @NoSubPO
        "

            Cmd.CommandText = SQL
            Dim dtDetail As New DataTable
            Using adapter = New SqlDataAdapter(Cmd)
                adapter.Fill(dtDetail)
            End Using

            ' ============================================
            ' UPDATE Detail Items: emi_pembelian_po_det, emi_pembelian_po_detail, dan B2B
            ' ============================================
            For Each detRow As DataRow In dtDetail.Rows
                Dim KodeBarang As String = detRow("Kode_Barang").ToString()
                Dim NoUrut As String = detRow("No_Urut").ToString()
                Dim QtySubPO As Decimal = CDec(detRow("Qty_SubPO"))
                Dim QtyDO As Decimal = CDec(detRow("Qty_DO"))
                Dim NilaiBarang As Decimal = CDec(detRow("Nilai_Barang"))
                Dim HargaBarang As Decimal = CDec(detRow("Harga_Barang"))
                Dim JumlahInput As Decimal = CDec(detRow("Jumlah_Input"))
                Dim NoFakIndukDetail As String = detRow("No_FakInduk").ToString()
                Dim NoUrutDetIndukDetail As String = detRow("NoUrutDetInduk").ToString()

                Dim NilaiBarangBaru As Decimal = (NilaiBarang / QtySubPO) * QtyDO
                Dim JumlahInputBaru As Decimal = (JumlahInput / QtySubPO) * QtyDO
                Dim TotalBaru As Decimal = QtyDO * HargaBarang

                ' ============================================
                ' INSERT Log: Emi_Pembelian_PO_Det_Log
                ' ============================================
                ' Cek keberadaan data terlebih dahulu
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)
                Cmd.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_det WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang AND No_Urut = @NoUrut"

                Dim CekSubPODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If CekSubPODet = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show($"Data Sub PO tidak ditemukan untuk Barang={KodeBarang} dan No={NoUrut}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@Log_UserID", UserID)
                Cmd.Parameters.AddWithValue("@Log_Tanggal", Date.Now())
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

                SQL = "
                INSERT INTO Emi_Pembelian_PO_Det_Log (
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
                    No_FakInduk,
                    urut_det_induk,
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
                    No_FakInduk,
                    urut_det_induk,
                    Jumlah_Input,
                    Satuan_Input,
                    @Log_UserID,
                    @Log_Tanggal
                FROM emi_pembelian_po_det
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan 
                    AND No_Faktur = @NoSubPO 
                    AND Kode_Barang = @KodeBarang
                    AND No_Urut = @NoUrut
            "
                ExecuteTrans(SQL)

                ' ============================================
                ' UPDATE 1: emi_pembelian_po_det
                ' ============================================
                ' Data sudah dicek di atas, langsung update
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@QtyDO", QtyDO)
                Cmd.Parameters.AddWithValue("@NilaiBarangBaru", NilaiBarangBaru)
                Cmd.Parameters.AddWithValue("@JumlahInputBaru", JumlahInputBaru)
                Cmd.Parameters.AddWithValue("@TotalBaru", TotalBaru)
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

                SQL = "
                UPDATE emi_pembelian_po_det SET 
                    Jumlah = @QtyDO,
                    Nilai_Barang = @NilaiBarangBaru,   
                    Total = @TotalBaru,
                    Jumlah_Input = @JumlahInputBaru
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoSubPO
                    AND Kode_Barang = @KodeBarang
                    AND No_Urut = @NoUrut
            "
                ExecuteTrans(SQL)

                ' ============================================
                ' Hitung Summary emi_pembelian_po_detail
                ' ============================================
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                SQL = "
                SELECT
                    ISNULL(SUM(Jumlah), 0) AS Total_Jumlah,
                    ISNULL(SUM(Nilai_Barang), 0) AS Total_Nilai,
                    ISNULL(SUM(Total), 0) AS Total_Total,
                    ISNULL(SUM(Jumlah_Input), 0) AS Total_JumlahInput
                FROM emi_pembelian_po_det
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoSubPO
                    AND Kode_Barang = @KodeBarang
            "
                Cmd.CommandText = SQL
                Dim drSumDetail As SqlDataReader = Cmd.ExecuteReader()

                Dim totalJumlah As Decimal = 0
                Dim totalNilai As Decimal = 0
                Dim totalTotal As Decimal = 0
                Dim totalJumlahInput As Decimal = 0

                If drSumDetail.Read() Then
                    totalJumlah = CDec(drSumDetail("Total_Jumlah"))
                    totalNilai = CDec(drSumDetail("Total_Nilai"))
                    totalTotal = CDec(drSumDetail("Total_Total"))
                    totalJumlahInput = CDec(drSumDetail("Total_JumlahInput"))
                End If
                drSumDetail.Close()

                ' ============================================
                ' UPDATE 2: emi_pembelian_po_detail
                ' ============================================
                ' Cek keberadaan data terlebih dahulu
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                Cmd.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_detail WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang"

                Dim CekSubPODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If CekSubPODetail = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show($"Data Sub PO tidak ditemukan untuk Barang={KodeBarang}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@Jumlah", totalJumlah)
                Cmd.Parameters.AddWithValue("@NilaiBarang", totalNilai)
                Cmd.Parameters.AddWithValue("@Total", totalTotal)
                Cmd.Parameters.AddWithValue("@JumlahInput", totalJumlahInput)
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                SQL = "
                UPDATE emi_pembelian_po_detail SET 
                    Jumlah = @Jumlah,
                    Nilai_Barang = @NilaiBarang,
                    Total = @Total,
                    Jumlah_Input = @JumlahInput
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoSubPO
                    AND Kode_Barang = @KodeBarang
            "
                ExecuteTrans(SQL)

                ' ============================================
                ' UPDATE 3: Sub PO detail di B2B
                ' ============================================
                ' Cek keberadaan data terlebih dahulu
                CmdB2B.Parameters.Clear()
                CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                CmdB2B.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                CmdB2B.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_detail WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang"

                Dim cekB2BDetail As Integer = Convert.ToInt32(CmdB2B.ExecuteScalar())
                If cekB2BDetail = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show($"Data B2B Sub PO tidak ditemukan untuk Barang={KodeBarang}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                CmdB2B.Parameters.Clear()
                CmdB2B.Parameters.AddWithValue("@Jumlah", totalJumlah)
                CmdB2B.Parameters.AddWithValue("@NilaiBarang", totalNilai)
                CmdB2B.Parameters.AddWithValue("@Total", totalTotal)
                CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                CmdB2B.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                SQL = "
                UPDATE emi_pembelian_po_detail SET 
                    Jumlah = @Jumlah,
                    Nilai_Barang = @NilaiBarang,
                    Total = @Total,
                    Flag_Sudah_Kirim = 'Y'
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoSubPO
                    AND Kode_Barang = @KodeBarang
            "
                ExecuteTransB2B(SQL)
            Next

            ' ============================================
            ' Hitung Grand Total dan Update Header
            ' ============================================
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
            FROM emi_pembelian_po_detail epd 
            INNER JOIN Emi_Pembelian_PO epo ON epd.No_Faktur = epo.No_Faktur 
            WHERE epd.Kode_Perusahaan = @KodePerusahaan 
            AND epd.No_Faktur = @NoSubPO 
            GROUP BY epo.Mata_Uang, epo.PPN, epo.Kurs, epo.Total_MUA
        "
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)

            Using reader = Cmd.ExecuteReader()
                If reader.Read() Then
                    grandTotal = Convert.ToDecimal(reader("Total"))
                    mataUang = reader("Mata_Uang").ToString()
                    PPN = Convert.ToDecimal(reader("PPN"))
                    Kurs = Convert.ToDecimal(reader("Kurs"))
                    TotalMUA = Convert.ToDecimal(reader("Total_MUA"))
                End If
            End Using

            Cmd.CommandText = "SELECT ISNULL(SUM(persentase), 0) AS Persentase_PPH FROM EMI_Detail_PPH_PO WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)

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
            ' UPDATE 4: Emi_Pembelian_PO (Database Utama)
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

            Dim cekEmiPO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekEmiPO = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data Sub PO tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@GrandSebelumPPN", grandTotal)
            Cmd.Parameters.AddWithValue("@GrandSetelahPPN", grandTotalSetelahPPN)
            Cmd.Parameters.AddWithValue("@GrandSetelahPPH", grandTotalSetelahPPH)
            Cmd.Parameters.AddWithValue("@TotalMUA", Total_MUA)
            Cmd.Parameters.AddWithValue("@GrandTotalTerbilang", General_Class.SayMUA(Math.Round(grandTotalSetelahPPH, 0), mataUang))
            SQL = "
            UPDATE Emi_Pembelian_PO 
            SET 
                Flag_Selesai_PO = 'Y',
                Total_MUA = @TotalMUA,
                Total_IDR = @GrandSebelumPPN,
                Grand_Sebelum_PPN = @GrandSebelumPPN,
                Grand = @GrandSetelahPPN,
                Grand_PPH = @GrandSetelahPPH,
                Grand_Total_Terbilang = @GrandTotalTerbilang 
            WHERE 
                Kode_Perusahaan = @KodePerusahaan 
                AND No_Faktur = @NoSubPO
        "
            ExecuteTrans(SQL)

            ' ============================================
            ' UPDATE 5: Emi_Pembelian_PO (B2B)
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            CmdB2B.Parameters.Clear()
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            CmdB2B.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

            Dim cekB2BEmiPO As Integer = Convert.ToInt32(CmdB2B.ExecuteScalar())
            If cekB2BEmiPO = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data B2B Sub PO tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            CmdB2B.Parameters.Clear()
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            CmdB2B.Parameters.AddWithValue("@GrandSebelumPPN", grandTotal)
            CmdB2B.Parameters.AddWithValue("@GrandSetelahPPN", grandTotalSetelahPPN)
            CmdB2B.Parameters.AddWithValue("@TotalMUA", Total_MUA)
            SQL = "
            UPDATE Emi_Pembelian_PO 
            SET 
                Flag_Selesai_PO = 'Y',
                Total_MUA = @TotalMUA,
                Total_IDR = @GrandSebelumPPN,
                Grand_Sebelum_PPN = @GrandSebelumPPN,
                Grand = @GrandSetelahPPN
            WHERE 
                Kode_Perusahaan = @KodePerusahaan 
                AND No_Faktur = @NoSubPO
        "
            ExecuteTransB2B(SQL)

            ' ============================================
            ' UPDATE 6: Emi_Pembelian_PO_Det_Induk
            ' ============================================
            For Each detRow As DataRow In dtDetail.Rows
                Dim NoFakIndukDetail As String = detRow("No_FakInduk").ToString()
                Dim NoUrutDetIndukDetail As String = detRow("NoUrutDetInduk").ToString()

                ' Cek keberadaan data terlebih dahulu
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakIndukDetail)
                Cmd.Parameters.AddWithValue("@NoUrutDetInduk", NoUrutDetIndukDetail)
                Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Det_Induk WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"

                Dim cekDetInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekDetInduk = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show($"Data PO Induk tidak ditemukan untuk No_Faktur={NoFakIndukDetail} dan No={NoUrutDetIndukDetail}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakIndukDetail)
                Cmd.Parameters.AddWithValue("@NoUrutDetInduk", NoUrutDetIndukDetail)

                SQL = "UPDATE Emi_Pembelian_PO_Det_Induk SET Flag_Selesai = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"
                ExecuteTrans(SQL)
            Next

            ' ============================================
            ' UPDATE 7: Emi_Pembelian_PO_Induk
            ' ============================================
            If Not String.IsNullOrEmpty(NoFakInduk) Then
                ' Cek keberadaan data terlebih dahulu
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
                Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Induk WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"

                Dim cekInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekInduk = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show("Data PO Induk tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)

                SQL = "
                UPDATE Emi_Pembelian_PO_Induk SET 
                    Flag_Selesai_SubPO = NULL 
                WHERE Kode_Perusahaan = @KodePerusahaan 
                AND No_Faktur = @NoFakInduk
            "
                ExecuteTrans(SQL)
            End If

            'Import
            Dim Flag_Import As String = ""
            SQL = "Select isnull(Flag_Import,'T') as Flag_Import from emi_pembelian_po a where "
            SQL = SQL & "a.kode_Perusahaan ='" & KodePerusahaan & "' and a.No_Faktur='" & NoSubPO & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Flag_Import = Dr("Flag_Import")

                Else
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show(" Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

            SQL = "Select isnull(Flag_Selesai_PO,'T') as Selesai_PO, "
            SQL = SQL & "isnull((select top(1) 'T' from EMI_Pembelian_Loading_Detail x, EMI_Pembelian_Loading y where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.no_faktur = y.no_faktur And y.status Is null "
            SQL = SQL & "And x.Kode_Perusahaan=a.Kode_Perusahaan And x.No_PO=a.No_Faktur And y.Flag_Selisih_BM Is null),'Y') as Selesai_loading "
            SQL = SQL & "From EMI_Pembelian_po a Where a.nO_faktur ='" & NoSubPO & "' and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If Dr("Selesai_PO") = "Y" And Dr("Selesai_loading") = "Y" Then

                        SQL = "Update EMI_Pembelian_po set flag_selisih_bm = 'Y' "
                        SQL = SQL & "where No_faktur = '" & NoSubPO & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                        Dr.Close()
                        ExecuteTrans(SQL)

                        If Flag_Import = "Y" Then
                            SQL = "Update EMI_Pembelian_po set Flag_Biaya = 'Y' "
                            SQL = SQL & "where No_faktur = '" & NoSubPO & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                            Dr.Close()
                            ExecuteTrans(SQL)
                        End If

                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show(" Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()

            Fetch_Sub_PO_Outstanding()

            MessageBox.Show($"Sub PO {NoSubPO} berhasil diselesaikan untuk SELURUH barang.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            If Cmd.Transaction IsNot Nothing Then
                Cmd.Transaction.Rollback()
            End If

            If CmdB2B.Transaction IsNot Nothing Then
                CmdB2B.Transaction.Rollback()
            End If

            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SelesaikanSubPOBarangIni_Click(sender As Object, e As EventArgs) Handles CMS_TSMI_Selesaikan_Sub_PO_Item.Click
        If Guna2DataGridView2.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih data Sub PO terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        Dim row As DataGridViewRow = Guna2DataGridView2.SelectedRows(0)
        Dim NoFakInduk As String = row.Cells("NoPOInduk").Value?.ToString()
        Dim NoSubPO As String = row.Cells("NoSubPO").Value?.ToString()
        Dim NoUrut As String = row.Cells("No_Urut").Value?.ToString()
        Dim NoUrutDetInduk As String = row.Cells("NoUrutDetInduk").Value?.ToString()
        Dim KodeBarang As String = row.Cells("KodeBarang").Value?.ToString()
        Dim NamaBarang As String = row.Cells("NamaBarang").Value?.ToString()
        Dim QtySubPO As Decimal = CDec(row.Cells("Qty_SubPO").Value)
        Dim NilaiBarang As Decimal = CDec(row.Cells("Nilai_Barang").Value)
        Dim HargaBarang As Decimal = CDec(row.Cells("Harga_Barang").Value)
        Dim JumlahInput As Decimal = CDec(row.Cells("Jumlah_Input").Value)

        If String.IsNullOrEmpty(NoSubPO) OrElse String.IsNullOrEmpty(KodeBarang) OrElse String.IsNullOrEmpty(NoUrut) Then
            MessageBox.Show("Data tidak lengkap.", "Error")
            Exit Sub
        End If

        ' ============================================
        ' 1. AMBIL QtyDO DARI DATABASE
        ' ============================================
        Dim QtyDO As Decimal = 0

        Try
            OpenConn()

            If CekButtonRole("Selesai_Barang_Sub_PO") = "T" Then
                MsgBox("Anda tidak memiliki akses menyelesaikan sebagian barang di Sub PO.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim SQL As String = "
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
                    AND do.Kode_Perusahaan = @KodePerusahaan
                    AND do.Status IS NULL
                    AND do.Flag_Retur IS NULL
            )
            GROUP BY do_d.No_PO, do_d.Kode_Barang
        )
        SELECT 
            ISNULL(ds.Total_DO, 0) AS Qty_DO
        FROM emi_pembelian_po_det epd
        LEFT JOIN DO_Sum ds 
            ON ds.No_PO = epd.No_Faktur 
            AND ds.Kode_Barang = epd.Kode_Barang
        WHERE 
            epd.Kode_Perusahaan = @KodePerusahaan 
            AND epd.No_Faktur = @NoSubPO
            AND epd.Kode_Barang = @KodeBarang
            AND epd.No_Urut = @NoUrut
    "

            Cmd.CommandText = SQL
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

            Dim objResult As Object = Cmd.ExecuteScalar()
            If objResult IsNot Nothing AndAlso Not IsDBNull(objResult) Then
                QtyDO = CDec(objResult)
            End If

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error mengambil QtyDO dari database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim NilaiBarangBaru As Decimal = (NilaiBarang / QtySubPO) * QtyDO
        Dim JumlahInputBaru As Decimal = (JumlahInput / QtySubPO) * QtyDO
        Dim TotalBaru As Decimal = QtyDO * HargaBarang

        Dim result As DialogResult = MessageBox.Show(
            $"Yakin ingin menyelesaikan item {KodeBarang}-{NamaBarang} sejumlah {QtySubPO:N2} menjadi {QtyDO:N2} di Sub PO {NoSubPO} ?",
            "Konfirmasi",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        )

        If result <> DialogResult.Yes Then Exit Sub

        Try
            OpenConn()
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            'Cek apakah Sub PO ada di B2B
            Dim SQL_CheckB2B_DO As String = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO 
            WHERE Kode_Perusahaan = @KodePerusahaan 
              AND No_Faktur = @NoSubPO
        "

            CmdB2B.Parameters.Clear()
            CmdB2B.CommandText = SQL_CheckB2B_DO
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)

            Dim B2B_Sub_PO As Integer = Convert.ToInt32(CmdB2B.ExecuteScalar())

            If B2B_Sub_PO = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data tidak ditemukan!")
                Exit Sub
            End If

            ' ============================================
            ' BACKUP 1: EMI_Pembelian_PO ke EMI_Pembelian_PO_Origin
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPO = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePO = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO As String = "
                    INSERT INTO EMI_Pembelian_PO_Origin (
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
                        No_Faktur_Induk,
                        Grand_PPH,
                        --UserID_Selesai,
                        --Tanggal_Selesai,
                        --Jam_Selesai,
                        ETA,
                        UserID_Batal,
                        Tanggal_Batal,
                        Jam_Batal
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
                        No_Faktur_Induk,
                        Grand_PPH,
                        --UserID_Selesai,
                        --Tanggal_Selesai,
                        --Jam_Selesai,
                        ETA,
                        UserID_Batal,
                        Tanggal_Batal,
                        Jam_Batal
                    FROM EMI_Pembelian_PO
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoSubPO
                    "

                ExecuteTrans(SQL_Backup_PO)
            End If

            ' ============================================
            ' BACKUP 2: EMI_Pembelian_PO_Det ke EMI_Pembelian_PO_Det_Origin
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Det_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODet = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO_Det WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePODet = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO_Det As String = "
                    INSERT INTO EMI_Pembelian_PO_Det_Origin (
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
                        No_FakInduk,
                        urut_det_induk,
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
                        No_FakInduk,
                        urut_det_induk,
                        Jumlah_Input,
                        Satuan_Input
                    FROM EMI_Pembelian_PO_Det
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoSubPO
                    "

                ExecuteTrans(SQL_Backup_PO_Det)
            End If

            ' ============================================
            ' BACKUP 3: EMI_Pembelian_PO_Detail ke EMI_Pembelian_PO_Detail_Origin
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Detail_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODetail = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO_Detail WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePODetail = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO_Detail As String = "
                    INSERT INTO EMI_Pembelian_PO_Detail_Origin (
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
                    FROM EMI_Pembelian_PO_Detail
                    WHERE Kode_Perusahaan = @KodePerusahaan
                      AND No_Faktur = @NoSubPO
                    "

                ExecuteTrans(SQL_Backup_PO_Detail)
            End If

            ' ============================================
            ' INSERT Log: Emi_Pembelian_PO_Det_Log
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)
            Cmd.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_det WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang AND No_Urut = @NoUrut"

            Dim CekSubPODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekSubPODet = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show($"Data Sub PO tidak ditemukan untuk Barang={KodeBarang} dan No={NoUrut}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@Log_UserID", UserID)
            Cmd.Parameters.AddWithValue("@Log_Tanggal", Date.Now())
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

            Dim SQL_Log As String = "
        INSERT INTO Emi_Pembelian_PO_Det_Log (
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
            No_FakInduk,
            urut_det_induk,
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
            No_FakInduk,
            urut_det_induk,
            Jumlah_Input,
            Satuan_Input,
            @Log_UserID,
            @Log_Tanggal
        FROM emi_pembelian_po_det
        WHERE 
            Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO 
            AND Kode_Barang = @KodeBarang
            AND No_Urut = @NoUrut
    "
            ExecuteTrans(SQL_Log)

            ' ============================================
            ' UPDATE 1: emi_pembelian_po_det
            ' ============================================
            ' Data sudah dicek di atas, langsung update
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@QtyDO", QtyDO)
            Cmd.Parameters.AddWithValue("@NilaiBarangBaru", NilaiBarangBaru)
            Cmd.Parameters.AddWithValue("@JumlahInputBaru", JumlahInputBaru)
            Cmd.Parameters.AddWithValue("@TotalBaru", TotalBaru)
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

            Dim SQL_Update_Det As String = "
        UPDATE emi_pembelian_po_det SET 
            Jumlah = @QtyDO,
            Nilai_Barang = @NilaiBarangBaru,   
            Total = @TotalBaru,
            Jumlah_Input = @JumlahInputBaru
        WHERE 
            Kode_Perusahaan = @KodePerusahaan
            AND No_Faktur = @NoSubPO
            AND Kode_Barang = @KodeBarang
            AND No_Urut = @NoUrut
    "
            ExecuteTrans(SQL_Update_Det)

            ' ============================================
            ' Hitung Summary emi_pembelian_po_detail
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Dim SQL As String = "
                SELECT
                    ISNULL(SUM(Jumlah), 0) AS Total_Jumlah,
                    ISNULL(SUM(Nilai_Barang), 0) AS Total_Nilai,
                    ISNULL(SUM(Total), 0) AS Total_Total,
                    ISNULL(SUM(Jumlah_Input), 0) AS Total_JumlahInput
                FROM emi_pembelian_po_det
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoSubPO
                    AND Kode_Barang = @KodeBarang
            "
            Cmd.CommandText = SQL
            Dim drSumDetail As SqlDataReader = Cmd.ExecuteReader()

            Dim totalJumlah As Decimal = 0
            Dim totalNilai As Decimal = 0
            Dim totalTotal As Decimal = 0
            Dim totalJumlahInput As Decimal = 0

            If drSumDetail.Read() Then
                totalJumlah = CDec(drSumDetail("Total_Jumlah"))
                totalNilai = CDec(drSumDetail("Total_Nilai"))
                totalTotal = CDec(drSumDetail("Total_Total"))
                totalJumlahInput = CDec(drSumDetail("Total_JumlahInput"))
            End If
            drSumDetail.Close()

            ' ============================================
            ' UPDATE 2: emi_pembelian_po_detail
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_detail WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang"

            Dim CekSubPODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekSubPODetail = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show($"Data Sub PO tidak ditemukan untuk Barang={KodeBarang}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@Jumlah", totalJumlah)
            Cmd.Parameters.AddWithValue("@NilaiBarang", totalNilai)
            Cmd.Parameters.AddWithValue("@Total", totalTotal)
            Cmd.Parameters.AddWithValue("@JumlahInput", totalJumlahInput)
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            SQL = "
                UPDATE emi_pembelian_po_detail SET 
                    Jumlah = @Jumlah,
                    Nilai_Barang = @NilaiBarang,
                    Total = @Total,
                    Jumlah_Input = @JumlahInput
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoSubPO
                    AND Kode_Barang = @KodeBarang
            "
            ExecuteTrans(SQL)

            ' ============================================
            ' UPDATE 3: Sub PO detail di B2B
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            CmdB2B.Parameters.Clear()
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            CmdB2B.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            CmdB2B.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_detail WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang"

            Dim cekB2BDetail As Integer = Convert.ToInt32(CmdB2B.ExecuteScalar())
            If cekB2BDetail = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show($"Data B2B Sub PO tidak ditemukan untuk Barang={KodeBarang}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            CmdB2B.Parameters.Clear()
            CmdB2B.Parameters.AddWithValue("@Jumlah", totalJumlah)
            CmdB2B.Parameters.AddWithValue("@NilaiBarang", totalNilai)
            CmdB2B.Parameters.AddWithValue("@Total", totalTotal)
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            CmdB2B.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            SQL = "
                UPDATE emi_pembelian_po_detail SET 
                    Jumlah = @Jumlah,
                    Nilai_Barang = @NilaiBarang,
                    Total = @Total,
                    Flag_Sudah_Kirim = 'Y'
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoSubPO
                    AND Kode_Barang = @KodeBarang
            "
            ExecuteTransB2B(SQL)

            ' ============================================
            ' Hitung Grand Total & Pajak
            ' ============================================
            Cmd.CommandText = "
        SELECT ISNULL(SUM(epd.Total), 0) AS Total, epo.Mata_Uang, epo.PPN, epo.Kurs, epo.Total_MUA FROM emi_pembelian_po_detail epd 
        INNER JOIN Emi_Pembelian_PO epo ON epd.No_Faktur = epo.No_Faktur 
        WHERE epd.Kode_Perusahaan = @KodePerusahaan AND epd.No_Faktur = @NoSubPO 
        GROUP BY Mata_Uang, PPN, Kurs, Total_MUA
    "
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)

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

            Cmd.CommandText = "SELECT ISNULL(SUM(persentase), 0) AS Persentase_PPH FROM EMI_Detail_PPH_PO WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)

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
            ' CEK APAKAH SEMUA BARANG SUDAH SELESAI
            ' ============================================
            Dim semuaBarangSelesai As Boolean = True
            Dim flagSelesaiPO As String = "NULL"

            ' Query untuk ambil semua barang di Sub PO beserta Qty DO nya
            Cmd.CommandText = "
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
                    AND do.Kode_Perusahaan = @KodePerusahaan
                    AND do.Status IS NULL
                    AND do.Flag_Retur IS NULL
            )
            GROUP BY do_d.No_PO, do_d.Kode_Barang
        )
        SELECT 
            epd.Kode_Barang,
            epd.Jumlah AS Qty_SubPO,
            ISNULL(ds.Total_DO, 0) AS Qty_DO
        FROM emi_pembelian_po_det epd
        LEFT JOIN DO_Sum ds 
            ON ds.No_PO = epd.No_Faktur 
            AND ds.Kode_Barang = epd.Kode_Barang
        WHERE 
            epd.Kode_Perusahaan = @KodePerusahaan 
            AND epd.No_Faktur = @NoSubPO
    "
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)

            Using reader = Cmd.ExecuteReader()
                While reader.Read()
                    Dim QtySubPOPerItem As Decimal = CDec(reader("Qty_SubPO"))
                    Dim QtyDOPerItem As Decimal = CDec(reader("Qty_DO"))

                    ' Jika ada 1 item saja yang Qty_SubPO <> Qty_DO, berarti belum semua selesai
                    If QtySubPOPerItem <> QtyDOPerItem Then
                        semuaBarangSelesai = False
                        Exit While
                    End If
                End While
            End Using

            ' Set flag berdasarkan hasil pengecekan
            If semuaBarangSelesai Then
                flagSelesaiPO = "'Y'"
            End If

            ' ============================================
            ' UPDATE 4: Emi_Pembelian_PO (Database Utama)
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

            Dim cekEmiPO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekEmiPO = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data Sub PO tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@GrandSebelumPPN", grandTotal)
            Cmd.Parameters.AddWithValue("@GrandSetelahPPN", grandTotalSetelahPPN)
            Cmd.Parameters.AddWithValue("@GrandSetelahPPH", grandTotalSetelahPPH)
            Cmd.Parameters.AddWithValue("@TotalMUA", Total_MUA)
            Cmd.Parameters.AddWithValue("@GrandTotalTerbilang", General_Class.SayMUA(Math.Round(grandTotalSetelahPPH, 0), mataUang))
            Dim SQL_Update_Header As String = "
        UPDATE Emi_Pembelian_PO 
        SET 
            Flag_Selesai_PO = " & flagSelesaiPO & ",
            Total_MUA = @TotalMUA,
            Total_IDR = @GrandSebelumPPN,
            Grand_Sebelum_PPN = @GrandSebelumPPN,
            Grand = @GrandSetelahPPN,
            Grand_PPH = @GrandSetelahPPH,
            Grand_Total_Terbilang = @GrandTotalTerbilang 
        WHERE 
            Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
    "
            ExecuteTrans(SQL_Update_Header)

            ' ============================================
            ' UPDATE 5: Emi_Pembelian_PO (B2B)
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            CmdB2B.Parameters.Clear()
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            CmdB2B.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

            Dim cekB2BEmiPO As Integer = Convert.ToInt32(CmdB2B.ExecuteScalar())
            If cekB2BEmiPO = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data B2B Sub PO tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            CmdB2B.Parameters.Clear()
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            CmdB2B.Parameters.AddWithValue("@GrandSebelumPPN", grandTotal)
            CmdB2B.Parameters.AddWithValue("@GrandSetelahPPN", grandTotalSetelahPPN)
            CmdB2B.Parameters.AddWithValue("@TotalMUA", Total_MUA)
            Dim SQL_Update_Header_B2B As String = "
            UPDATE Emi_Pembelian_PO 
            SET 
                Flag_Selesai_PO = " & flagSelesaiPO & ",
                Total_MUA = @TotalMUA,
                Total_IDR = @GrandSebelumPPN,
                Grand_Sebelum_PPN = @GrandSebelumPPN,
                Grand = @GrandSetelahPPN
            WHERE 
                Kode_Perusahaan = @KodePerusahaan 
                AND No_Faktur = @NoSubPO
        "
            ExecuteTransB2B(SQL_Update_Header_B2B)

            ' ============================================
            ' UPDATE 6: Emi_Pembelian_PO_Det_Induk
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Cmd.Parameters.AddWithValue("@NoUrutDetInduk", NoUrutDetInduk)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Det_Induk WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"

            Dim cekDetInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekDetInduk = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show($"Data PO Induk tidak ditemukan untuk No_Faktur={NoFakInduk} dan No_Urut={NoUrutDetInduk}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Cmd.Parameters.AddWithValue("@NoUrutDetInduk", NoUrutDetInduk)
            Dim SQL_Update_Induk As String = "UPDATE Emi_Pembelian_PO_Det_Induk SET Flag_Selesai = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"
            ExecuteTrans(SQL_Update_Induk)

            ' ============================================
            ' UPDATE 7: Emi_Pembelian_PO_Induk
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Induk WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"

            Dim cekInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekInduk = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data PO Induk tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Dim SQL_Update_Induk_PO As String = "UPDATE Emi_Pembelian_PO_Induk SET Flag_Selesai_SubPO = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"
            ExecuteTrans(SQL_Update_Induk_PO)

            'Import
            Dim Flag_Import As String = ""
            SQL = "Select isnull(Flag_Import,'T') as Flag_Import from emi_pembelian_po a where "
            SQL = SQL & "a.kode_Perusahaan ='" & KodePerusahaan & "' and a.No_Faktur='" & NoSubPO & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Flag_Import = Dr("Flag_Import")

                Else
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show(" Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

            SQL = "Select isnull(Flag_Selesai_PO,'T') as Selesai_PO, "
            SQL = SQL & "isnull((select top(1) 'T' from EMI_Pembelian_Loading_Detail x, EMI_Pembelian_Loading y where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.no_faktur = y.no_faktur And y.status Is null "
            SQL = SQL & "And x.Kode_Perusahaan=a.Kode_Perusahaan And x.No_PO=a.No_Faktur And y.Flag_Selisih_BM Is null),'Y') as Selesai_loading "
            SQL = SQL & "From EMI_Pembelian_po a Where a.nO_faktur ='" & NoSubPO & "' and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If Dr("Selesai_PO") = "Y" And Dr("Selesai_loading") = "Y" Then

                        SQL = "Update EMI_Pembelian_po set flag_selisih_bm = 'Y' "
                        SQL = SQL & "where No_faktur = '" & NoSubPO & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                        Dr.Close()
                        ExecuteTrans(SQL)

                        If Flag_Import = "Y" Then
                            SQL = "Update EMI_Pembelian_po set Flag_Biaya = 'Y' "
                            SQL = SQL & "where No_faktur = '" & NoSubPO & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                            Dr.Close()
                            ExecuteTrans(SQL)
                        End If

                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show(" Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()
            CloseConn()
            CloseConnB2B()

            Fetch_Sub_PO_Outstanding()

            If semuaBarangSelesai Then
                MessageBox.Show("Barang Sub PO berhasil diselesaikan." & vbCrLf & "Semua item di Sub PO ini sudah selesai.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Barang Sub PO berhasil diselesaikan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            If Cmd.Transaction IsNot Nothing Then
                Cmd.Transaction.Rollback()
            End If

            If CmdB2B.Transaction IsNot Nothing Then
                CmdB2B.Transaction.Rollback()
            End If

            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BatalkanSubPO_Click(sender As Object, e As EventArgs) Handles CMS_TSMI_Batalkan_Sub_PO.Click
        If Guna2DataGridView2.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih data Sub PO terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row As DataGridViewRow = Guna2DataGridView2.SelectedRows(0)
        Dim NoSubPO As String = row.Cells("NoSubPO").Value?.ToString()
        Dim NoFakInduk As String = row.Cells("NoPOInduk").Value?.ToString()

        If String.IsNullOrEmpty(NoSubPO) Or String.IsNullOrEmpty(NoFakInduk) Then
            MessageBox.Show("Data tidak lengkap.", "Error")
            Exit Sub
        End If

        ' ============================================
        ' VALIDASI: CEK APAKAH ADA BARANG DENGAN QtyDO
        ' ============================================
        Try
            OpenConn()

            If CekButtonRole("Batal_Sub_PO") = "T" Then
                MsgBox("Anda tidak memiliki akses membatalkan Sub PO.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim SQL As String = "
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
                            AND do.Kode_Perusahaan = @KodePerusahaan
                            AND do.Status IS NULL
                            AND do.Flag_Retur IS NULL
                    )
                    GROUP BY do_d.No_PO, do_d.Kode_Barang
                )
                SELECT 
                    epd.Kode_Barang,
                    epd.No_Urut,
                    epd.Jumlah AS Qty_SubPO,
                    ISNULL(ds.Total_DO, 0) AS Qty_DO,
                    epd.Nilai_Barang,
                    epd.Harga_Barang,
                    epd.Jumlah_Input,
                    epd.No_FakInduk,
                    epd.urut_det_induk AS NoUrutDetInduk
                FROM emi_pembelian_po_det epd
                LEFT JOIN DO_Sum ds 
                    ON ds.No_PO = epd.No_Faktur 
                    AND ds.Kode_Barang = epd.Kode_Barang
                WHERE 
                    epd.Kode_Perusahaan = @KodePerusahaan 
                    AND epd.No_Faktur = @NoSubPO
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = SQL

            Dim dtDetail As New DataTable
            Using adapter = New SqlDataAdapter(Cmd)
                adapter.Fill(dtDetail)
            End Using

            Dim adaBarangDenganQtyDO As Boolean = False

            ' Cek satu-satu setiap barang
            For Each detRow As DataRow In dtDetail.Rows
                Dim qtyDO As Decimal = CDec(detRow("Qty_DO"))

                ' Jika ada 1 barang saja yang Qty_DO > 0, stop proses
                If qtyDO > 0 Then
                    adaBarangDenganQtyDO = True
                    Exit For
                End If
            Next

            ' Jika ada barang dengan QtyDO > 0, batalkan proses
            If adaBarangDenganQtyDO Then
                CloseConn()
                MessageBox.Show("Pembatalan tidak bisa dilakukan karena barang sudah dalam pengiriman.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error saat validasi: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        ' ============================================
        ' KONFIRMASI PEMBATALAN
        ' ============================================
        Dim result As DialogResult = MessageBox.Show(
            $"Yakin ingin membatalkan Sub PO {NoSubPO} ?",
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
            OpenConnB2B()
            Cmd.Transaction = Cn.BeginTransaction
            CmdB2B.Transaction = CnB2B.BeginTransaction

            'Cek apakah Sub PO ada di B2B
            Dim SQL_CheckB2B_DO As String = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO 
            WHERE Kode_Perusahaan = @KodePerusahaan 
              AND No_Faktur = @NoSubPO
        "

            CmdB2B.Parameters.Clear()
            CmdB2B.CommandText = SQL_CheckB2B_DO
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)

            Dim B2B_Sub_PO As Integer = Convert.ToInt32(CmdB2B.ExecuteScalar())

            If B2B_Sub_PO = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data tidak ditemukan!")
                Exit Sub
            End If

            ' ============================================
            ' UPDATE 1: Status Sub PO di Database Utama
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

            Dim cekDataUtama As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekDataUtama = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data Sub PO tidak ditemukan di database utama!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Lakukan update
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@UserID", UserID)
            Cmd.Parameters.AddWithValue("@TanggalBatal", Date.Now)
            Cmd.Parameters.AddWithValue("@JamBatal", Date.Now.ToString("HH:mm:ss"))
            Dim SQL_Batalkan As String = "UPDATE Emi_Pembelian_PO SET Status = 'Y', Flag_Selesai_PO = NULL, UserID_Batal = @UserID, Tanggal_Batal = @TanggalBatal, Jam_Batal = @JamBatal WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"
            ExecuteTrans(SQL_Batalkan)

            ' ============================================
            ' UPDATE 2: Sub PO di B2B
            ' ============================================
            ' Cek keberadaan data terlebih dahulu (sudah dicek di atas, tapi untuk konsistensi)
            CmdB2B.Parameters.Clear()
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            CmdB2B.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

            Dim cekDataB2B As Integer = Convert.ToInt32(CmdB2B.ExecuteScalar())
            If cekDataB2B = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data Sub PO tidak ditemukan di B2B!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Lakukan update
            CmdB2B.Parameters.Clear()
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Dim SQL_Batalkan_B2B As String = "UPDATE Emi_Pembelian_PO SET Status = 'Y', Flag_Selesai_PO = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"
            ExecuteTransB2B(SQL_Batalkan_B2B)

            ' ============================================
            ' UPDATE 3: Sub PO Detail di B2B
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            CmdB2B.Parameters.Clear()
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            CmdB2B.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_detail WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

            Dim cekDetailB2B As Integer = Convert.ToInt32(CmdB2B.ExecuteScalar())
            If cekDetailB2B = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data Detail Sub PO tidak ditemukan di B2B!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Lakukan update
            CmdB2B.Parameters.Clear()
            CmdB2B.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            CmdB2B.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Dim SQL_Batalkan_Detail_B2B As String = "UPDATE emi_pembelian_po_detail SET Flag_Sudah_Kirim = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"
            ExecuteTransB2B(SQL_Batalkan_Detail_B2B)

            ' ============================================
            ' Ambil data induk untuk update flag
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Dim SQL_GetInduk As String = "
        SELECT DISTINCT
            No_FakInduk,
            urut_det_induk AS NoUrutDetInduk
        FROM emi_pembelian_po_det
        WHERE Kode_Perusahaan = @KodePerusahaan
        AND No_Faktur = @NoSubPO
    "
            Cmd.CommandText = SQL_GetInduk
            Dim dtInduk As New DataTable
            Using adapter = New SqlDataAdapter(Cmd)
                adapter.Fill(dtInduk)
            End Using

            ' ============================================
            ' UPDATE 4: Update flag di detail induk satu-satu
            ' ============================================
            For Each detRow As DataRow In dtInduk.Rows
                Dim NoFakIndukDetail As String = detRow("No_FakInduk").ToString()
                Dim NoUrutDetIndukDetail As String = detRow("NoUrutDetInduk").ToString()

                ' Cek keberadaan data terlebih dahulu
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakIndukDetail)
                Cmd.Parameters.AddWithValue("@NoUrutDetInduk", NoUrutDetIndukDetail)
                Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Det_Induk WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"

                Dim cekDetInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekDetInduk = 0 Then
                    Cmd.Transaction.Rollback()
                    CmdB2B.Transaction.Rollback()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show($"Data Detail Induk tidak ditemukan untuk No Faktur={NoFakIndukDetail} dan No={NoUrutDetIndukDetail}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                ' Lakukan update
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakIndukDetail)
                Cmd.Parameters.AddWithValue("@NoUrutDetInduk", NoUrutDetIndukDetail)
                Dim SQL_UpdateDetInduk As String = "UPDATE Emi_Pembelian_PO_Det_Induk SET Flag_Selesai = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"
                ExecuteTrans(SQL_UpdateDetInduk)
            Next

            ' ============================================
            ' UPDATE 5: Update flag di header induk
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Induk WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"

            Dim cekInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekInduk = 0 Then
                Cmd.Transaction.Rollback()
                CmdB2B.Transaction.Rollback()
                CloseTrans()
                CloseTransB2B()
                CloseConn()
                CloseConnB2B()
                MessageBox.Show("Data Induk tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Lakukan update
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Dim SQL_UpdateInduk As String = "UPDATE Emi_Pembelian_PO_Induk SET Flag_Selesai_SubPO = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"
            ExecuteTrans(SQL_UpdateInduk)

            Cmd.Transaction.Commit()
            CmdB2B.Transaction.Commit()

            CloseConn()
            CloseConnB2B()

            Fetch_Sub_PO_Outstanding()
            MessageBox.Show("Sub PO berhasil dibatalkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            If Cmd.Transaction IsNot Nothing Then
                Cmd.Transaction.Rollback()
            End If

            If CmdB2B.Transaction IsNot Nothing Then
                CmdB2B.Transaction.Rollback()
            End If

            CloseTrans()
            CloseTransB2B()
            CloseConn()
            CloseConnB2B()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class