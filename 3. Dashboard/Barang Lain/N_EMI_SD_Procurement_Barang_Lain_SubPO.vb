Imports System.Data.SqlClient
Imports System.Web
Imports Guna.UI2.WinForms

Public Class N_EMI_SD_Procurement_Barang_Lain_SubPO
    Public Property FilterKodeBarang As String

    Private searchTimer As Timer
    Private Const DEBOUNCE_DELAY As Integer = 750
    Private Const MIN_SEARCH_LENGTH As Integer = 3

    Private hasETDValue As Boolean = False
    Private hasETAValue As Boolean = False

    Private Sub SD_Card_Sub_PO_Outstanding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()
            If CekButtonRole("Ubah_ETD_ETA_Sub_PO_Barang_Lain") = "T" Then
                Button_Simpan.Enabled = False
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        searchTimer = New Timer With {.Interval = DEBOUNCE_DELAY}
        AddHandler searchTimer.Tick, AddressOf OnSearchTimerTick
        AddHandler TB_Search.TextChanged, AddressOf TB_Search_TextChanged
        AddHandler CB_Status.SelectedIndexChanged, AddressOf CB_Status_SelectedIndexChanged

        AddHandler Guna2DateTimePicker2.ValueChanged, AddressOf Guna2DateTimePicker2_ValueChanged
        AddHandler Guna2DateTimePicker2.DropDown, AddressOf Guna2DateTimePicker2_DropDown

        Guna2TextBox1.Enabled = False

        Guna2DateTimePicker1.Format = DateTimePickerFormat.Long
        Guna2DateTimePicker2.Format = DateTimePickerFormat.Custom
        Guna2DateTimePicker2.CustomFormat = " "

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
        If Not hasETAValue Then
            Guna2DateTimePicker2.Format = DateTimePickerFormat.Long
            Guna2DateTimePicker2.Value = DateTime.Now.Date
        End If
    End Sub

    Private Sub Guna2DateTimePicker2_ValueChanged(sender As Object, e As EventArgs)
        If Guna2DateTimePicker2.Format <> DateTimePickerFormat.Custom Then
            hasETAValue = True
        End If
    End Sub

    Private Sub ClearFormInputs()
        Guna2Panel4.Visible = False
        Guna2TextBox1.Text = ""

        Guna2DateTimePicker1.Format = DateTimePickerFormat.Long
        Guna2DateTimePicker1.Value = DateTime.Now.Date
        hasETDValue = True

        Guna2DateTimePicker2.Format = DateTimePickerFormat.Custom
        Guna2DateTimePicker2.CustomFormat = " "
        hasETAValue = False
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

                Guna2TextBox1.Text = If(IsDBNull(row.Cells("NoSubPO").Value), "", row.Cells("NoSubPO").Value.ToString())
                Guna2Panel4.Visible = True

                If e.ColumnIndex = 10 Then
                    Guna2HtmlLabel5.Text = "Riwayat Perubahan ETD"
                    Fetch_ETD_Log()
                End If

                If e.ColumnIndex = 12 Then
                    Guna2HtmlLabel5.Text = "Riwayat Perubahan ETA"
                    Fetch_ETA_Log()
                End If

                Dim noPO As String = Guna2TextBox1.Text
                If Not String.IsNullOrEmpty(noPO) Then
                    Try
                        OpenConn()
                        SQL = $"SELECT ETD_Simulasi, ETA_Simulasi FROM EMI_Pembelian_PO_Barang_Lain WHERE No_Faktur = '{noPO}'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read() Then
                                SetDateTimePickerValue(Guna2DateTimePicker1, hasETDValue, Dr("ETD_Simulasi"), True)

                                SetDateTimePickerValue(Guna2DateTimePicker2, hasETAValue, Dr("ETA_Simulasi"), False)
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
                Case Else
                    statusFilter = ""
            End Select

            If FilterKodeBarang = "" Then
                CloseConn()
                Exit Sub
            End If

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
                        MAX(CASE WHEN rn_asc = 1 THEN ETD END) AS ETD_Ori_Log
                    FROM(
                        SELECT
                            Kode_Perusahaan, No_Faktur, ETD, No_Urut,
                            ROW_NUMBER() OVER (PARTITION BY Kode_Perusahaan, No_Faktur ORDER BY No_Urut ASC) rn_asc
                        FROM N_EMI_LOG_PO_SUB_ETA_ETD_Barang_lain
                        WHERE ETD IS NOT NULL
                    ) x
                    GROUP BY Kode_Perusahaan, No_Faktur
                ),
                ETA_Log AS (
                    SELECT
                        Kode_Perusahaan,
                        No_Faktur,
                        MAX(CASE WHEN rn_asc = 1 THEN ETA END) AS ETA_Ori_Log
                    FROM(
                        SELECT
                            Kode_Perusahaan, No_Faktur, ETA, No_Urut,
                            ROW_NUMBER() OVER (PARTITION BY Kode_Perusahaan, No_Faktur ORDER BY No_Urut ASC) rn_asc
                        FROM N_EMI_LOG_PO_SUB_ETA_ETD_Barang_lain
                        WHERE ETA IS NOT NULL
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
                        CASE
                            WHEN etd.ETD_Ori_Log IS NULL THEN NULL
                            ELSE FORMAT(spo.ETD_Simulasi, 'dd MMM yyyy')
                        END,
                        '-'
                    ) AS ETD_Update,
                    ISNULL(
                        FORMAT(
                            COALESCE(eta.ETA_Ori_Log, spo.ETA_Simulasi),
                            'dd MMM yyyy'
                        ),
                        '-'
                    ) AS ETA_Ori,
                    ISNULL(
                        CASE
                            WHEN eta.ETA_Ori_Log IS NULL THEN NULL
                            ELSE FORMAT(spo.ETA_Simulasi, 'dd MMM yyyy')
                        END,
                        '-'
                    ) AS ETA_Update,
                    spo_d.No_Urut,
                    spo_d.No_FakInduk,
                    spo_d.Nilai_Barang,
                    spo_d.Harga_Barang,
                    spo_d.urut_det_induk
                FROM EMI_Pembelian_PO_Barang_Lain spo
                INNER JOIN EMI_Pembelian_PO_Det_Barang_Lain spo_d 
                    ON spo_d.No_Faktur = spo.No_Faktur
                    AND spo_d.Kode_Perusahaan = spo.Kode_Perusahaan
                INNER JOIN EMI_Pembelian_PO_Induk_Barang_Lain poi
                    ON poi.No_Faktur = spo_d.No_FakInduk
                    AND poi.Kode_Perusahaan = spo.Kode_Perusahaan
                INNER JOIN PR pr 
                    ON pr.No_Urut = spo_d.No_Urut_PR
                    AND pr.Kode_Barang = spo_d.Kode_Barang
                    AND pr.Kode_Perusahaan = spo_d.Kode_Perusahaan
                LEFT JOIN DO_Sum ds 
                    ON ds.No_PO = spo.No_Faktur 
                    AND ds.Kode_Barang = spo_d.Kode_Barang
                LEFT JOIN Barang_Lain b 
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
                    AND (
                        spo.No_Faktur LIKE '%{TB_Search.Text}%'
                        OR pr.No_Faktur LIKE '%{TB_Search.Text}%'
                        OR b.Kode_Barang LIKE '%{TB_Search.Text}%'
                        OR b.Nama LIKE '%{TB_Search.Text}%'
                    )
                    {statusFilter}
                    AND spo_d.Kode_Barang IN ({FilterKodeBarang})
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
            If CekButtonRole("Ubah_ETD_ETA_Sub_PO_Barang_Lain") = "T" Then
                MsgBox("Anda tidak memiliki akses untuk melakukan tindakan ini!")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        Try
            If String.IsNullOrEmpty(Guna2TextBox1.Text) Then
                MessageBox.Show("Pilih Sub PO terlebih dahulu dari data grid!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim noPO As String = Guna2TextBox1.Text
            Dim newETD As DateTime = Guna2DateTimePicker1.Value.Date
            Dim newETA As DateTime? = Nothing

            If hasETAValue Then
                newETA = Guna2DateTimePicker2.Value.Date
            End If

            If hasETAValue Then
                If newETA.Value < newETD Then
                    MessageBox.Show("ETA tidak boleh lebih kecil dari ETD!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            OpenConn()

            Dim oldETD As DateTime? = Nothing
            Dim oldETA As DateTime? = Nothing

            SQL = $"SELECT ETD_Simulasi, ETA_Simulasi FROM EMI_Pembelian_PO_Barang_Lain WHERE No_Faktur = '{noPO}'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    If Not IsDBNull(Dr("ETD_Simulasi")) AndAlso Dr("ETD_Simulasi") IsNot Nothing Then
                        oldETD = Convert.ToDateTime(Dr("ETD_Simulasi")).Date
                    End If
                    If Not IsDBNull(Dr("ETA_Simulasi")) AndAlso Dr("ETA_Simulasi") IsNot Nothing Then
                        oldETA = Convert.ToDateTime(Dr("ETA_Simulasi")).Date
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

            SQL = $"SELECT DISTINCT No_FakInduk FROM EMI_Pembelian_PO_Det_Barang_Lain WHERE No_Faktur = '{noPO}'"
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
                SQL = $"SELECT MIN(ETD_Simulasi) as MinETD FROM EMI_Pembelian_PO_Barang_Lain WHERE No_Faktur IN ('{inClause}')"

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

            If hasETAValue Then
                SQL = $"UPDATE EMI_Pembelian_PO_Barang_Lain SET ETD_Simulasi = '{newETD:yyyy-MM-dd}', ETA_Simulasi = '{newETA.Value:yyyy-MM-dd}' WHERE No_Faktur = '{noPO}'"
            Else
                SQL = $"UPDATE EMI_Pembelian_PO_Barang_Lain SET ETD_Simulasi = '{newETD:yyyy-MM-dd}', ETA_Simulasi = NULL WHERE No_Faktur = '{noPO}'"
            End If
            ExecuteTrans(SQL)

            If hasETDChanged OrElse hasETAChanged Then
                Dim currentDate As DateTime = DateTime.Now
                Dim currentTime As String = currentDate.ToString("HH:mm:ss")

                Dim etdForLog As String = "NULL"
                If hasETDChanged AndAlso oldETD.HasValue Then
                    etdForLog = $"'{oldETD.Value:yyyy-MM-dd}'"
                End If

                Dim etaForLog As String = "NULL"
                If hasETAChanged AndAlso oldETA.HasValue Then
                    etaForLog = $"'{oldETA.Value:yyyy-MM-dd}'"
                End If

                SQL = $"INSERT INTO N_EMI_LOG_PO_SUB_ETA_ETD_Barang_lain 
                (Kode_Perusahaan, Tanggal, Jam, UserID, No_Faktur, ETD, ETA) 
                VALUES 
                ('{KodePerusahaan}', '{currentDate:yyyy-MM-dd HH:mm:ss}', '{currentTime}', '{UserID}', '{noPO}', {etdForLog}, {etaForLog})"
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
                l1.ETD,
                l1.Userid, 
                l1.Tanggal,
                l1.No_Urut,
                LEAD(l1.ETD) OVER (ORDER BY l1.No_Urut) AS NextETD,
                p.ETD_Simulasi
            FROM N_EMI_LOG_PO_SUB_ETA_ETD_Barang_lain l1
            LEFT JOIN EMI_Pembelian_PO_Barang_Lain p ON l1.No_Faktur = p.No_Faktur
            WHERE l1.No_Faktur = '{Guna2TextBox1.Text}'
              AND l1.ETD IS NOT NULL
            ORDER BY l1.No_Urut DESC"

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
                    Dim nextETD As Object = Dr("NextETD")
                    Dim etdSimulasi As Object = Dr("ETD_Simulasi")
                    Dim userId As String = Dr("Userid")

                    Dim etdDisplay As String = "N/A"
                    If nextETD IsNot Nothing AndAlso Not IsDBNull(nextETD) Then
                        etdDisplay = CDate(nextETD).ToString("dd MMM yy")
                    ElseIf etdSimulasi IsNot Nothing AndAlso Not IsDBNull(etdSimulasi) Then
                        etdDisplay = CDate(etdSimulasi).ToString("dd MMM yy")
                    End If

                    ListView1.Columns.Add(
                        "Diubah Pada " & tanggal.ToString("dd MMM yy"),
                        180,
                        HorizontalAlignment.Center
                    )

                    Dim cellText As String =
                $"ETD: {etdDisplay} | User: {userId}"

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
                l1.ETA,
                l1.Userid, 
                l1.Tanggal,
                l1.No_Urut,
                LEAD(l1.ETA) OVER (ORDER BY l1.No_Urut) AS NextETA,
                p.ETA_Simulasi
            FROM N_EMI_LOG_PO_SUB_ETA_ETD_Barang_lain l1
            LEFT JOIN EMI_Pembelian_PO_Barang_Lain p ON l1.No_Faktur = p.No_Faktur
            WHERE l1.No_Faktur = '{Guna2TextBox1.Text}'
              AND l1.ETA IS NOT NULL
            ORDER BY l1.No_Urut DESC"

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
                    Dim nextETA As Object = Dr("NextETA")
                    Dim etaSimulasi As Object = Dr("ETA_Simulasi")
                    Dim userId As String = Dr("Userid")

                    Dim etaDisplay As String = "N/A"
                    If nextETA IsNot Nothing AndAlso Not IsDBNull(nextETA) Then
                        etaDisplay = CDate(nextETA).ToString("dd MMM yy")
                    ElseIf etaSimulasi IsNot Nothing AndAlso Not IsDBNull(etaSimulasi) Then
                        etaDisplay = CDate(etaSimulasi).ToString("dd MMM yy")
                    End If

                    ListView1.Columns.Add(
                        "Diubah Pada " & tanggal.ToString("dd MMM yy"),
                        180,
                        HorizontalAlignment.Center
                    )

                    Dim cellText As String =
                $"ETA: {etaDisplay} | User: {userId}"

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
        Dim nama_file As String = "Sub_PO_Outstanding_Barang_Lain_" & format_akhir & ".xlsx"
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("Sheet1")
        xlApp.ScreenUpdating = False
        xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

        Try
            For colIndex As Integer = 0 To Guna2DataGridView2.Columns.Count - 1
                xlWorkSheet.Cells(1, colIndex + 1).Value = Guna2DataGridView2.Columns(colIndex).HeaderText
            Next

            Dim rowIndex As Integer = 2
            Dim rows = Guna2DataGridView2.Rows.Count
            Dim cols = Guna2DataGridView2.Columns.Count

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

            Dim dataRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(rowIndex - 1, Guna2DataGridView2.Columns.Count))
            With dataRange.Borders
                .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
            End With

            Dim headerRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, Guna2DataGridView2.Columns.Count))
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
                MsgBox("Sub PO Outstanding Barang Lain berhasil di-export!", MsgBoxStyle.Information)
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

    Private Sub Guna2DataGridView2_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Guna2DataGridView2.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            Guna2DataGridView2.ClearSelection()
            Guna2DataGridView2.Rows(e.RowIndex).Selected = True
            Guna2DataGridView2.CurrentCell = Guna2DataGridView2.Rows(e.RowIndex).Cells(0)

            Guna2DataGridView2.ContextMenuStrip = ContextMenuStrip_DGV_Sub_PO_Outstanding
        End If
    End Sub

    Private Sub CMS_TSMI_Batalkan_Sub_PO_Click(sender As Object, e As EventArgs) Handles CMS_TSMI_Batalkan_Sub_PO.Click
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
        '    Try
        '        OpenConn()

        '        Dim SQL As String = "
        '    ;WITH DO_Sum AS (
        '        SELECT 
        '            do_d.No_PO,
        '            do_d.Kode_Barang,
        '            SUM(do_d.Jumlah) AS Total_DO
        '        FROM EMI_Pembelian_Loading_Detail_Barang_Lain do_d
        '        WHERE EXISTS (
        '            SELECT 1 
        '            FROM EMI_Pembelian_Loading_Barang_Lain do 
        '            WHERE 
        '                do.No_Faktur = do_d.No_Faktur
        '                AND do.Kode_Perusahaan = @KodePerusahaan
        '                AND do.Status IS NULL
        '                AND do.Flag_Retur IS NULL
        '        )
        '        GROUP BY do_d.No_PO, do_d.Kode_Barang
        '    )
        '    SELECT 
        '        epd.Kode_Barang,
        '        epd.No_Urut,
        '        epd.Jumlah AS Qty_SubPO,
        '        ISNULL(ds.Total_DO, 0) AS Qty_DO,
        '        epd.Nilai_Barang,
        '        epd.Harga_Barang,
        '        epd.No_FakInduk,
        '        epd.urut_det_induk AS NoUrutDetInduk
        '    FROM emi_pembelian_po_det_Barang_Lain epd
        '    LEFT JOIN DO_Sum ds 
        '        ON ds.No_PO = epd.No_Faktur 
        '        AND ds.Kode_Barang = epd.Kode_Barang
        '    WHERE 
        '        epd.Kode_Perusahaan = @KodePerusahaan 
        '        AND epd.No_Faktur = @NoSubPO
        '"

        '        Cmd.Parameters.Clear()
        '        Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
        '        Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
        '        Cmd.CommandText = SQL

        '        Dim dtDetail As New DataTable
        '        Using adapter = New SqlDataAdapter(Cmd)
        '            adapter.Fill(dtDetail)
        '        End Using

        '        Dim adaBarangDenganQtyDO As Boolean = False

        '        ' Cek satu-satu setiap barang
        '        For Each detRow As DataRow In dtDetail.Rows
        '            Dim qtyDO As Decimal = CDec(detRow("Qty_DO"))

        '            ' Jika ada 1 barang saja yang Qty_DO > 0, stop proses
        '            If qtyDO > 0 Then
        '                adaBarangDenganQtyDO = True
        '                Exit For
        '            End If
        '        Next

        '        ' Jika ada barang dengan QtyDO > 0, batalkan proses
        '        If adaBarangDenganQtyDO Then
        '            MessageBox.Show("Pembatalan tidak bisa dilakukan karena barang sudah dalam pengiriman.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            CloseConn()
        '            Exit Sub
        '        End If

        '        CloseConn()

        '    Catch ex As Exception
        '        CloseConn()
        '        MessageBox.Show("Error saat validasi: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Sub
        '    End Try

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
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Batal_Sub_PO_Barang_Lain") = "T" Then
                MsgBox("Anda tidak memiliki akses membatalkan Sub PO.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            ' ============================================
            ' UPDATE 1: Status Sub PO di Database Utama
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

            Dim cekDataUtama As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekDataUtama = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
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
            Dim SQL_Batalkan As String = "UPDATE Emi_Pembelian_PO_Barang_Lain SET Status = 'Y', Flag_Selesai_PO = NULL, UserID_Batal = @UserID, Tanggal_Batal = @TanggalBatal, Jam_Batal = @JamBatal WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"
            ExecuteTrans(SQL_Batalkan)

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
        FROM emi_pembelian_po_det_Barang_Lain
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
                Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Det_Induk_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"

                Dim cekDetInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekDetInduk = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data Detail Induk tidak ditemukan untuk No Faktur={NoFakIndukDetail} dan No={NoUrutDetIndukDetail}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                ' Lakukan update
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakIndukDetail)
                Cmd.Parameters.AddWithValue("@NoUrutDetInduk", NoUrutDetIndukDetail)
                Dim SQL_UpdateDetInduk As String = "UPDATE Emi_Pembelian_PO_Det_Induk_Barang_Lain SET Flag_Selesai = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"
                ExecuteTrans(SQL_UpdateDetInduk)
            Next

            ' ============================================
            ' UPDATE 5: Update flag di header induk
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Induk_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"

            Dim cekInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekInduk = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data Induk tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Lakukan update
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Dim SQL_UpdateInduk As String = "UPDATE Emi_Pembelian_PO_Induk_Barang_Lain SET Flag_Selesai_SubPO = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"
            ExecuteTrans(SQL_UpdateInduk)

            ' Batalkan DO dari sub PO ini jika ada
            Try
                ' Step 1: Ambil semua No_Faktur dari detail berdasarkan No_PO
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)

                Cmd.CommandText = "SELECT DISTINCT No_Faktur FROM Emi_Pembelian_Loading_Detail_Barang_Lain " &
                      "WHERE Kode_Perusahaan = @KodePerusahaan AND No_PO = @NoSubPO"

                Dim noFakturs As New List(Of String)()
                Dim dr As SqlDataReader = Cmd.ExecuteReader()

                While dr.Read()
                    If Not IsDBNull(dr("No_Faktur")) Then
                        noFakturs.Add(dr("No_Faktur").ToString())
                    End If
                End While

                dr.Close()

                ' Step 2: Jika ada No_Faktur, update status di tabel header
                If noFakturs.Count > 0 Then
                    For Each noFaktur As String In noFakturs
                        Cmd.Parameters.Clear()
                        Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                        Cmd.Parameters.AddWithValue("@NoFaktur", noFaktur)
                        Cmd.Parameters.AddWithValue("@UserID", UserID)
                        Cmd.Parameters.AddWithValue("@TanggalBatal", Date.Now)
                        Cmd.Parameters.AddWithValue("@JamBatal", Date.Now.ToString("HH:mm:ss"))

                        Dim SQL_Batalkan_DO As String = "UPDATE Emi_Pembelian_Loading_Barang_Lain " &
                                            "SET Status = 'Y', UserID_Batal = @UserID, Tanggal_Batal = @TanggalBatal, Jam_Batal = @JamBatal " &
                                            "WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFaktur"

                        ExecuteTrans(SQL_Batalkan_DO)
                    Next
                End If
            Catch ex As Exception
                CloseConn()
                MessageBox.Show("Error saat pembatalan DO: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Cmd.Transaction.Commit()

            CloseConn()

            Fetch_Sub_PO_Outstanding()
            MessageBox.Show("Sub PO berhasil dibatalkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            If Cmd.Transaction IsNot Nothing Then
                Cmd.Transaction.Rollback()
            End If

            CloseTrans()
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CMS_TSMI_Selesaikan_Sub_PO_Click(sender As Object, e As EventArgs) Handles CMS_TSMI_Selesaikan_Sub_PO.Click
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
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Selesai_Sub_PO_Barang_Lain") = "T" Then
                MsgBox("Anda tidak memiliki akses menyelesaikan Sub PO.", MsgBoxStyle.Critical)
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
            FROM EMI_Pembelian_PO_Barang_Lain_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPO = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePO = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO As String = "
                INSERT INTO EMI_Pembelian_PO_Barang_Lain_Origin (
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
                    UserID_Batal,
                    Tanggal_Batal,
                    Jam_Batal,
                    UserID_Selesai,
                    Tanggal_Selesai,
                    Jam_Selesai,
                    ETA_Simulasi
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
                    UserID_Batal,
                    Tanggal_Batal,
                    Jam_Batal,
                    UserID_Selesai,
                    Tanggal_Selesai,
                    Jam_Selesai,
                    ETA_Simulasi
                FROM EMI_Pembelian_PO_Barang_Lain
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
            FROM EMI_Pembelian_PO_Det_Barang_Lain_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODet = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO_Det_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePODet = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO_Det As String = "
                    INSERT INTO EMI_Pembelian_PO_Det_Barang_Lain_Origin (
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
                        Jumlah_MasukX,
                        No_FakInduk,
                        Urut_Det_Induk,
                        Kategori_Gudang
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
                        Jumlah_MasukX,
                        No_FakInduk,
                        Urut_Det_Induk,
                        Kategori_Gudang
                    FROM EMI_Pembelian_PO_Det_Barang_Lain
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
            FROM EMI_Pembelian_PO_Detail_Barang_Lain_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODetail = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO_Detail_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePODetail = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO_Detail As String = "
                INSERT INTO EMI_Pembelian_PO_Detail_Barang_Lain_Origin (
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
                    Kategori_Gudang
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
                    Kategori_Gudang
                FROM EMI_Pembelian_PO_Detail_Barang_Lain
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
                FROM EMI_Pembelian_Loading_Detail_Barang_Lain do_d
                WHERE EXISTS (
                    SELECT 1 
                    FROM EMI_Pembelian_Loading_Barang_Lain do 
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
                epd.No_FakInduk,
                epd.urut_det_induk AS NoUrutDetInduk
            FROM emi_pembelian_po_det_Barang_Lain epd
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
                Dim NoFakIndukDetail As String = detRow("No_FakInduk").ToString()
                Dim NoUrutDetIndukDetail As String = detRow("NoUrutDetInduk").ToString()

                Dim NilaiBarangBaru As Decimal = (NilaiBarang / QtySubPO) * QtyDO
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
                Cmd.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_det_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang AND No_Urut = @NoUrut"

                Dim CekSubPODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If CekSubPODet = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
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
                INSERT INTO Emi_Pembelian_PO_Det_Barang_Lain_Log (
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
                    @Log_UserID,
                    @Log_Tanggal
                FROM emi_pembelian_po_det_Barang_Lain
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
                Cmd.Parameters.AddWithValue("@TotalBaru", TotalBaru)
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

                SQL = "
                UPDATE emi_pembelian_po_det_Barang_Lain SET 
                    Jumlah = @QtyDO,
                    Nilai_Barang = @NilaiBarangBaru,   
                    Total = @TotalBaru
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
                    ISNULL(SUM(Total), 0) AS Total_Total
                FROM emi_pembelian_po_det_Barang_Lain
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

                If drSumDetail.Read() Then
                    totalJumlah = CDec(drSumDetail("Total_Jumlah"))
                    totalNilai = CDec(drSumDetail("Total_Nilai"))
                    totalTotal = CDec(drSumDetail("Total_Total"))
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
                Cmd.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_detail_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang"

                Dim CekSubPODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If CekSubPODetail = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data Sub PO tidak ditemukan untuk Barang={KodeBarang}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@Jumlah", totalJumlah)
                Cmd.Parameters.AddWithValue("@NilaiBarang", totalNilai)
                Cmd.Parameters.AddWithValue("@Total", totalTotal)
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
                SQL = "
                UPDATE emi_pembelian_po_detail_Barang_Lain SET 
                    Jumlah = @Jumlah,
                    Nilai_Barang = @NilaiBarang,
                    Total = @Total
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoSubPO
                    AND Kode_Barang = @KodeBarang
            "
                ExecuteTrans(SQL)
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
            FROM emi_pembelian_po_detail_Barang_Lain epd 
            INNER JOIN Emi_Pembelian_PO_Barang_Lain epo ON epd.No_Faktur = epo.No_Faktur 
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

            Cmd.CommandText = "SELECT ISNULL(SUM(persentase), 0) AS Persentase_PPH FROM EMI_Detail_PPH_PO_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"
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
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

            Dim cekEmiPO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekEmiPO = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
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
            UPDATE Emi_Pembelian_PO_Barang_Lain 
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
                Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Det_Induk_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"

                Dim cekDetInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekDetInduk = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data PO Induk tidak ditemukan untuk No_Faktur={NoFakIndukDetail} dan No={NoUrutDetIndukDetail}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakIndukDetail)
                Cmd.Parameters.AddWithValue("@NoUrutDetInduk", NoUrutDetIndukDetail)

                SQL = "UPDATE Emi_Pembelian_PO_Det_Induk_Barang_Lain SET Flag_Selesai = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"
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
                Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Induk_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"

                Dim cekInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekInduk = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data PO Induk tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)

                SQL = "
                UPDATE Emi_Pembelian_PO_Induk_Barang_Lain SET 
                    Flag_Selesai_SubPO = NULL 
                WHERE Kode_Perusahaan = @KodePerusahaan 
                AND No_Faktur = @NoFakInduk
            "
                ExecuteTrans(SQL)
            End If

            'Import
            Dim Flag_Import As String = ""
            SQL = "Select isnull(Flag_Import,'T') as Flag_Import from emi_pembelian_po_barang_lain a where "
            SQL = SQL & "a.kode_Perusahaan ='" & KodePerusahaan & "' and a.No_Faktur='" & NoSubPO & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Flag_Import = Dr("Flag_Import")

                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(" Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

            SQL = "Select isnull(Flag_Selesai_PO,'T') as Selesai_PO, "
            SQL = SQL & "isnull((select top(1) 'T' from EMI_Pembelian_Loading_Detail_Barang_Lain x, EMI_Pembelian_Loading_Barang_Lain y where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.no_faktur = y.no_faktur And y.status Is null "
            SQL = SQL & "And x.Kode_Perusahaan=a.Kode_Perusahaan And x.No_PO=a.No_Faktur And y.Flag_Selisih_BM Is null),'Y') as Selesai_loading "
            SQL = SQL & "From EMI_Pembelian_po_barang_lain a Where a.nO_faktur ='" & NoSubPO & "' and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If Dr("Selesai_PO") = "Y" And Dr("Selesai_loading") = "Y" Then

                        SQL = "Update EMI_Pembelian_po_barang_lain set flag_selisih_bm = 'Y' "
                        SQL = SQL & "where No_faktur = '" & NoSubPO & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                        Dr.Close()
                        ExecuteTrans(SQL)

                        If Flag_Import = "Y" Then
                            SQL = "Update EMI_Pembelian_po_barang_lain set Flag_Biaya = 'Y' "
                            SQL = SQL & "where No_faktur = '" & NoSubPO & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                            Dr.Close()
                            ExecuteTrans(SQL)
                        End If

                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(" Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseConn()

            Fetch_Sub_PO_Outstanding()

            MessageBox.Show($"Sub PO {NoSubPO} berhasil diselesaikan untuk SELURUH barang.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            If Cmd.Transaction IsNot Nothing Then
                Cmd.Transaction.Rollback()
            End If

            CloseTrans()
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CMS_TSMI_Selesaikan_Sub_PO_Item_Click(sender As Object, e As EventArgs) Handles CMS_TSMI_Selesaikan_Sub_PO_Item.Click
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

            If CekButtonRole("Selesai_Barang_Sub_PO_Barang_Lain") = "T" Then
                MsgBox("Anda tidak memiliki akses menyelesaikan sebagian barang di Sub PO.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim SQL As String = "
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
                    AND do.Kode_Perusahaan = @KodePerusahaan
                    AND do.Status IS NULL
                    AND do.Flag_Retur IS NULL
            )
            GROUP BY do_d.No_PO, do_d.Kode_Barang
        )
        SELECT 
            ISNULL(ds.Total_DO, 0) AS Qty_DO
        FROM emi_pembelian_po_det_Barang_Lain epd
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
            Cmd.Transaction = Cn.BeginTransaction

            ' ============================================
            ' BACKUP 1: EMI_Pembelian_PO ke EMI_Pembelian_PO_Origin
            ' ============================================
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.CommandText = "
            SELECT COUNT(*) 
            FROM EMI_Pembelian_PO_Barang_Lain_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPO = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePO = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO As String = "
                INSERT INTO EMI_Pembelian_PO_Barang_Lain_Origin (
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
                    UserID_Batal,
                    Tanggal_Batal,
                    Jam_Batal,
                    UserID_Selesai,
                    Tanggal_Selesai,
                    Jam_Selesai,
                    ETA_Simulasi
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
                    UserID_Batal,
                    Tanggal_Batal,
                    Jam_Batal,
                    UserID_Selesai,
                    Tanggal_Selesai,
                    Jam_Selesai,
                    ETA_Simulasi
                FROM EMI_Pembelian_PO_Barang_Lain
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
            FROM EMI_Pembelian_PO_Det_Barang_Lain_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODet = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO_Det_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePODet = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO_Det As String = "
                INSERT INTO EMI_Pembelian_PO_Det_Barang_Lain_Origin (
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
                    Jumlah_MasukX,
                    No_FakInduk,
                    Urut_Det_Induk,
                    Kategori_Gudang
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
                    Jumlah_MasukX,
                    No_FakInduk,
                    Urut_Det_Induk,
                    Kategori_Gudang
                FROM EMI_Pembelian_PO_Det_Barang_Lain
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
            FROM EMI_Pembelian_PO_Detail_Barang_Lain_Origin
            WHERE Kode_Perusahaan = @KodePerusahaan 
            AND No_Faktur = @NoSubPO
        "
            Dim CekPODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekPODetail = 0 Then
                ' Cek keberadaan data di source
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Cmd.CommandText = "SELECT COUNT(*) FROM EMI_Pembelian_PO_Detail_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

                Dim cekSourcePODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
                If cekSourcePODetail = 0 Then
                    Cmd.Transaction.Rollback()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Sub PO tidak ditemukan untuk backup!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
                Dim SQL_Backup_PO_Detail As String = "
                INSERT INTO EMI_Pembelian_PO_Detail_Barang_Lain_Origin (
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
                    Kategori_Gudang
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
                    Kategori_Gudang
                FROM EMI_Pembelian_PO_Detail_Barang_Lain
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
            Cmd.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_det_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang AND No_Urut = @NoUrut"

            Dim CekSubPODet As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekSubPODet = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
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
        INSERT INTO Emi_Pembelian_PO_Det_Barang_Lain_Log (
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
            @Log_UserID,
            @Log_Tanggal
        FROM emi_pembelian_po_det_Barang_Lain
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
            Cmd.Parameters.AddWithValue("@TotalBaru", TotalBaru)
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.Parameters.AddWithValue("@NoUrut", NoUrut)

            Dim SQL_Update_Det As String = "
        UPDATE emi_pembelian_po_det_Barang_Lain SET 
            Jumlah = @QtyDO,
            Nilai_Barang = @NilaiBarangBaru,   
            Total = @TotalBaru
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
                    ISNULL(SUM(Total), 0) AS Total_Total
                FROM emi_pembelian_po_det_Barang_Lain
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

            If drSumDetail.Read() Then
                totalJumlah = CDec(drSumDetail("Total_Jumlah"))
                totalNilai = CDec(drSumDetail("Total_Nilai"))
                totalTotal = CDec(drSumDetail("Total_Total"))
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
            Cmd.CommandText = "SELECT COUNT(*) FROM emi_pembelian_po_detail_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO AND Kode_Barang = @KodeBarang"

            Dim CekSubPODetail As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If CekSubPODetail = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
                MessageBox.Show($"Data Sub PO tidak ditemukan untuk Barang={KodeBarang}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@Jumlah", totalJumlah)
            Cmd.Parameters.AddWithValue("@NilaiBarang", totalNilai)
            Cmd.Parameters.AddWithValue("@Total", totalTotal)
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoSubPO", NoSubPO)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            SQL = "
                UPDATE emi_pembelian_po_detail_Barang_Lain SET 
                    Jumlah = @Jumlah,
                    Nilai_Barang = @NilaiBarang,
                    Total = @Total
                WHERE 
                    Kode_Perusahaan = @KodePerusahaan
                    AND No_Faktur = @NoSubPO
                    AND Kode_Barang = @KodeBarang
            "
            ExecuteTrans(SQL)

            ' ============================================
            ' Hitung Grand Total & Pajak
            ' ============================================
            Cmd.CommandText = "
        SELECT ISNULL(SUM(epd.Total), 0) AS Total, epo.Mata_Uang, epo.PPN, epo.Kurs, epo.Total_MUA FROM emi_pembelian_po_detail_Barang_Lain epd 
        INNER JOIN Emi_Pembelian_PO_Barang_Lain epo ON epd.No_Faktur = epo.No_Faktur 
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

            Cmd.CommandText = "SELECT ISNULL(SUM(persentase), 0) AS Persentase_PPH FROM EMI_Detail_PPH_PO_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"
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
            FROM EMI_Pembelian_Loading_Detail_Barang_Lain do_d
            WHERE EXISTS (
                SELECT 1 
                FROM EMI_Pembelian_Loading_Barang_Lain do 
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
        FROM emi_pembelian_po_det_Barang_Lain epd
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
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoSubPO"

            Dim cekEmiPO As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekEmiPO = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
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
        UPDATE Emi_Pembelian_PO_Barang_Lain 
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
            ' UPDATE 6: Emi_Pembelian_PO_Det_Induk
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Cmd.Parameters.AddWithValue("@NoUrutDetInduk", NoUrutDetInduk)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Det_Induk_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"

            Dim cekDetInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekDetInduk = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
                MessageBox.Show($"Data PO Induk tidak ditemukan untuk No_Faktur={NoFakInduk} dan No_Urut={NoUrutDetInduk}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Cmd.Parameters.AddWithValue("@NoUrutDetInduk", NoUrutDetInduk)
            Dim SQL_Update_Induk As String = "UPDATE Emi_Pembelian_PO_Det_Induk_Barang_Lain SET Flag_Selesai = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk AND No_Urut = @NoUrutDetInduk"
            ExecuteTrans(SQL_Update_Induk)

            ' ============================================
            ' UPDATE 7: Emi_Pembelian_PO_Induk
            ' ============================================
            ' Cek keberadaan data terlebih dahulu
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Cmd.CommandText = "SELECT COUNT(*) FROM Emi_Pembelian_PO_Induk_Barang_Lain WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"

            Dim cekInduk As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            If cekInduk = 0 Then
                Cmd.Transaction.Rollback()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data PO Induk tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFakInduk", NoFakInduk)
            Dim SQL_Update_Induk_PO As String = "UPDATE Emi_Pembelian_PO_Induk_Barang_Lain SET Flag_Selesai_SubPO = NULL WHERE Kode_Perusahaan = @KodePerusahaan AND No_Faktur = @NoFakInduk"
            ExecuteTrans(SQL_Update_Induk_PO)

            'Import
            Dim Flag_Import As String = ""
            SQL = "Select isnull(Flag_Import,'T') as Flag_Import from emi_pembelian_po_barang_lain a where "
            SQL = SQL & "a.kode_Perusahaan ='" & KodePerusahaan & "' and a.No_Faktur='" & NoSubPO & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Flag_Import = Dr("Flag_Import")

                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(" Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

            SQL = "Select isnull(Flag_Selesai_PO,'T') as Selesai_PO, "
            SQL = SQL & "isnull((select top(1) 'T' from EMI_Pembelian_Loading_Detail_Barang_Lain x, EMI_Pembelian_Loading_Barang_Lain y where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.no_faktur = y.no_faktur And y.status Is null "
            SQL = SQL & "And x.Kode_Perusahaan=a.Kode_Perusahaan And x.No_PO=a.No_Faktur And y.Flag_Selisih_BM Is null),'Y') as Selesai_loading "
            SQL = SQL & "From EMI_Pembelian_po_barang_lain a Where a.nO_faktur ='" & NoSubPO & "' and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If Dr("Selesai_PO") = "Y" And Dr("Selesai_loading") = "Y" Then

                        SQL = "Update EMI_Pembelian_po_barang_lain set flag_selisih_bm = 'Y' "
                        SQL = SQL & "where No_faktur = '" & NoSubPO & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                        Dr.Close()
                        ExecuteTrans(SQL)

                        If Flag_Import = "Y" Then
                            SQL = "Update EMI_Pembelian_po_barang_lain set Flag_Biaya = 'Y' "
                            SQL = SQL & "where No_faktur = '" & NoSubPO & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                            Dr.Close()
                            ExecuteTrans(SQL)
                        End If

                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(" Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseConn()

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

            CloseTrans()
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class