Imports System.ComponentModel
Imports ERP_EMI.Devices.RFID.HW_VX6346KL

Public Class N_EMI_Pairing_RFID_Touchscreen_BackUp
    'RFID Reader class
    Private RFIDReader As HW_VX6346KL_Reader

    'Buffer untuk menyimpan data scan dari scanner manual
    Private ScanBuffer As String = ""
    Private ScannedTags As New HashSet(Of String)

    'Pagination variables
    Private CurrentPage As Integer = 1
    Private PageSize As Integer = 19
    Private TotalData As Integer = 0
    Private TotalPage As Integer = 0

    'Flag untuk menentukan metode scan
    Private FlagScanManual As Boolean = False

    'IP RFID Reader
    Private RFIDReaderIP As String = "0.0.0.0"

    'No Transaksi
    Private SelectedNoFaktur As String = ""

    Private Sub N_EMI_Pairing_RFID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        'Setup Lv RFID Tags
        With Lv_RFID_Tags
            .Clear()
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True

            .Columns.Add("RFID Tag", 250, HorizontalAlignment.Left)
        End With

        Try
            OpenConn()

            SQL = "SELECT IP_Address From N_EMI_Master_Data_RFID_Readers WHERE Urut_Oto = 1"
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    RFIDReaderIP = Dr("IP_Address").ToString()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'Load Tab Menunggu Pairing
        TabControl1.SelectedIndex = 0
        Fetch_Menunggu_Pairing_RFID()

        'RFID Reader Setup
        RFIDReader = New HW_VX6346KL_Reader(RFIDReaderIP, 6000)
        AddHandler RFIDReader.Connected, AddressOf RFID_Connected
        AddHandler RFIDReader.Disconnected, AddressOf RFID_Disconnected
        AddHandler RFIDReader.TagDetected, AddressOf RFID_TagDetected
    End Sub

    'Listen scanner manual
    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If Not FlagScanManual Then Return
        If e.KeyChar = ChrW(Keys.Enter) Then
            If ScanBuffer <> "" Then
                Dim tag As String = ScanBuffer.Trim()
                If ScannedTags.Contains(tag) Then
                    ScanBuffer = ""
                    Return
                End If
                If Not IsRFIDTagCanBeUsed(tag) Then
                    ScanBuffer = ""
                    Return
                End If
                ScannedTags.Add(tag)
                Lv_RFID_Tags.Items.Add(New ListViewItem(tag))
            End If
            ScanBuffer = ""
        Else
            ScanBuffer &= e.KeyChar
        End If
    End Sub

    Private Sub RFID_Connected()
        Btn_Disconnect_RFID.Visible = True
    End Sub

    Private Sub RFID_Disconnected()
        Btn_Disconnect_RFID.Visible = False
    End Sub

    'RFID Reader class ketika detect tag
    Private Sub RFID_TagDetected(tag As String)
        If Lv_RFID_Tags.InvokeRequired Then
            Lv_RFID_Tags.Invoke(New Action(Of String)(AddressOf RFID_TagDetected), tag)
            Return
        End If
        For Each itm As ListViewItem In Lv_RFID_Tags.Items
            If itm.Text = tag Then Return
        Next
        If Not IsRFIDTagCanBeUsed(tag) Then Return
        Lv_RFID_Tags.Items.Add(New ListViewItem(tag))
    End Sub

    Private Function IsRFIDTagCanBeUsed(rfidTag As String) As Boolean
        Try
            OpenConn()

            Dim sql As String = "
                SELECT 1
                WHERE EXISTS (
                    SELECT 1
                    FROM N_EMI_Master_Data_RFID_Tags
                    WHERE RFID_Tag = @RFID_Tag
                      AND Status IS NULL
                )
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@RFID_Tag", rfidTag)

            Using Dr = OpenTrans(sql)
                Return Dr.Read()
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True
        Finally
            CloseConn()
        End Try
    End Function


    Private Sub LoadTotalDataMenungguPairing()
        Dim sqlTotal As String = "
            SELECT COUNT(*) AS Total_Data
            FROM (
                SELECT a.No_Transaksi
                FROM Emi_Split_Production_Order a
                JOIN Emi_Material_Requisition b
                    ON b.Kode_Perusahaan = a.Kode_Perusahaan
                    AND b.No_Faktur_Order = a.No_Transaksi
                JOIN Emi_Material_Requisition_Det c
                    ON c.Kode_Perusahaan = b.Kode_Perusahaan
                    AND c.No_Faktur = b.No_Faktur
                JOIN Emi_Material_Requisition_Det_Convert d
                    ON d.Kode_Perusahaan = c.Kode_Perusahaan
                    AND d.No_Urut_Det = c.Urut_Oto
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM N_EMI_Pairing_RFID x
                    WHERE x.No_Split_Production_Order = a.No_Transaksi
                )
                AND a.Kode_Perusahaan = @KodePerusahaan
                AND a.Status IS NULL
                GROUP BY a.No_Transaksi
            ) AS Total_Data;
        "

        Cmd.Parameters.Clear()
        Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)

        Using Dr = OpenTrans(sqlTotal)
            If Dr.Read() Then
                TotalData = If(IsDBNull(Dr("Total_Data")), 0, Convert.ToInt32(Dr("Total_Data")))
            End If
            Dr.Close()
        End Using

        TotalPage = Math.Ceiling(TotalData / PageSize)
    End Sub

    Private Sub LoadTotalDataSudahPairing()
        Dim sqlTotal As String = "
            SELECT COUNT(*) AS Total_Data
            FROM Emi_Split_Production_Order a
            WHERE EXISTS (
                SELECT 1
                FROM N_EMI_Pairing_RFID b
                WHERE b.No_Split_Production_Order = a.No_Transaksi
            )
            AND a.Kode_Perusahaan = @KodePerusahaan
            AND a.Status IS NULL;
        "

        Cmd.Parameters.Clear()
        Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)

        Using Dr = OpenTrans(sqlTotal)
            If Dr.Read() Then
                TotalData = If(IsDBNull(Dr("Total_Data")), 0, Convert.ToInt32(Dr("Total_Data")))
            End If
            Dr.Close()
        End Using

        TotalPage = Math.Ceiling(TotalData / PageSize)
    End Sub

    Private Sub UpdatePaginationUI()
        Dim StartData As Integer = ((CurrentPage - 1) * PageSize) + 1
        Dim EndData As Integer = Math.Min(CurrentPage * PageSize, TotalData)
        Dim ShowingData As Integer = If(TabControl1.SelectedIndex = 0, Dgv_Menunggu_Pairing.Rows.Count, Dgv_Sudah_Pairing.Rows.Count)

        Lb_Pagination.Text =
        $"Halaman {CurrentPage} dari {TotalPage}, menampilkan {ShowingData} dari {TotalData} data"

        Btn_Prev.Enabled = CurrentPage > 1
        Btn_Next.Enabled = CurrentPage < TotalPage
    End Sub

    Private Sub Fetch_Menunggu_Pairing_RFID()
        Try
            OpenConn()
            Dgv_Menunggu_Pairing.Rows.Clear()

            LoadTotalDataMenungguPairing()

            SQL = $"
                SELECT 
                    a.No_Transaksi AS No_Split_PO,
                    a.Flag_Scan_Manual,
                    FORMAT(a.Tgl_Produksi, 'dd MMM yyy') AS Tanggal_Produksi,
                    a.Jam_Produksi,
                    a.No_Batch,
                    CASE
                        WHEN SUM(CASE WHEN d.Flag_Transfer = 'Y' THEN 1 ELSE 0 END) = COUNT(*) 
                            THEN 'SELESAI PROSES'
                        WHEN SUM(CASE WHEN d.Flag_Transfer = 'Y' THEN 1 ELSE 0 END) > 0 
                            THEN 'SEDANG DIPROSES'
                        ELSE 'BELUM DIPROSES'
                    END AS Status_Proses
                FROM Emi_Split_Production_Order a
                JOIN Emi_Material_Requisition b
                    ON b.Kode_Perusahaan = a.Kode_Perusahaan
                    AND b.No_Faktur_Order = a.No_Transaksi
                JOIN Emi_Material_Requisition_Det c
                    ON c.Kode_Perusahaan = b.Kode_Perusahaan
                    AND c.No_Faktur = b.No_Faktur
                JOIN Emi_Material_Requisition_Det_Convert d
                    ON d.Kode_Perusahaan = c.Kode_Perusahaan
                    AND d.No_Urut_Det = c.Urut_Oto
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM N_EMI_Pairing_RFID x
                    WHERE x.No_Split_Production_Order = a.No_Transaksi
                )
                AND a.Kode_Perusahaan = @KodePerusahaan
                AND a.Status IS NULL
                GROUP BY a.No_Transaksi, a.Tgl_Produksi, a.Jam_Produksi, a.No_Batch, a.Flag_Scan_Manual
                ORDER BY a.Tgl_Produksi DESC, a.Jam_Produksi DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
            "

            Dim Offset As Integer = (CurrentPage - 1) * PageSize

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@Offset", Offset)
            Cmd.Parameters.AddWithValue("@PageSize", PageSize)


            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim rowIndex As Integer = Dgv_Menunggu_Pairing.Rows.Add(
                        If(IsDBNull(Dr("No_Split_PO")), "", Dr("No_Split_PO").ToString()),
                        If(IsDBNull(Dr("Tanggal_Produksi")), "", Dr("Tanggal_Produksi").ToString()),
                        "Mulai Pairing",
                        If(IsDBNull(Dr("Flag_Scan_Manual")), "", Dr("Flag_Scan_Manual").ToString())
                    )

                    Dim status As String = If(IsDBNull(Dr("Status_Proses")), "", Dr("Status_Proses").ToString())

                    Select Case status
                        Case "SELESAI PROSES"
                            Dgv_Menunggu_Pairing.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightGreen

                        Case "SEDANG DIPROSES"
                            Dgv_Menunggu_Pairing.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightYellow

                        Case "BELUM DIPROSES"
                            Dgv_Menunggu_Pairing.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightCoral
                    End Select
                End While

                Dr.Close()
            End Using

            UpdatePaginationUI()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fetch_Sudah_Pairing_RFID()
        Try
            OpenConn()
            Dgv_Sudah_Pairing.Rows.Clear()

            LoadTotalDataSudahPairing()

            SQL = $"
                SELECT 
                    a.No_Transaksi AS No_Split_PO, a.Flag_Scan_Manual,
                    FORMAT(a.Tgl_Produksi, 'dd MMM yyy') AS Tanggal_Produksi, a.Jam_Produksi, a.No_Batch
                FROM Emi_Split_Production_Order a
                WHERE EXISTS (
                    SELECT 1
                    FROM N_EMI_Pairing_RFID b
                    WHERE b.No_Split_Production_Order = a.No_Transaksi
                )
                AND a.Kode_Perusahaan = @KodePerusahaan
                AND a.Status IS NULL
                ORDER BY a.Tgl_Produksi DESC, a.Jam_Produksi DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
            "

            Dim Offset As Integer = (CurrentPage - 1) * PageSize

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@Offset", Offset)
            Cmd.Parameters.AddWithValue("@PageSize", PageSize)

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim rowIndex As Integer = Dgv_Sudah_Pairing.Rows.Add(
                        If(IsDBNull(Dr("No_Split_PO")), "", Dr("No_Split_PO").ToString()),
                        If(IsDBNull(Dr("Tanggal_Produksi")), "", Dr("Tanggal_Produksi").ToString()),
                        "Pairing Ulang",
                        If(IsDBNull(Dr("Flag_Scan_Manual")), "", Dr("Flag_Scan_Manual").ToString())
                    )
                End While

                Dr.Close()
            End Using

            UpdatePaginationUI()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fetch_Dgv_Detail(NoFakturOrder)
        Select Case TabControl1.SelectedIndex
            Case 0
                If Dgv_Menunggu_Pairing.CurrentRow Is Nothing Then Exit Sub
            Case 1
                If Dgv_Sudah_Pairing.CurrentRow Is Nothing Then Exit Sub
        End Select

        Try
            OpenConn()
            Dgv_Detail.Rows.Clear()

            SQL = "
                SELECT
                    a.No_Faktur,
                    b.Kode_Barang,
                    c.Nama,
                    b.Jumlah AS Qty_MR,
                    COALESCE(d.Total, 0) AS Qty_TF,
                    b.Satuan,
                    CASE
                        WHEN COALESCE(d.Total, 0) = 0 THEN 'BELUM DIPROSES'
                        WHEN COALESCE(d.Total, 0) < b.Jumlah THEN 'SEDANG DIPROSES'
                        ELSE 'SELESAI PROSES'
                    END AS Status_Proses
                FROM Emi_Material_Requisition a
                JOIN Emi_Material_Requisition_Det_Convert b
                    ON  b.Kode_Perusahaan = a.Kode_Perusahaan
                    AND b.No_Faktur       = a.No_Faktur
                LEFT JOIN Tf_Stock d
                    ON  d.Kode_Perusahaan = b.Kode_Perusahaan
                    AND d.Urut_Material_Requisition_Convert = b.Urut_Oto
                JOIN Barang c
                    ON  c.Kode_Perusahaan  = b.Kode_Perusahaan
                    AND c.Kode_Barang      = b.Kode_Barang
                    AND c.Kode_Stock_Owner = b.Kode_Stock_Owner
                WHERE a.No_Faktur_Order = @NoFakturOrder
                  AND a.Kode_Perusahaan = @KodePerusahaan;
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("NoFakturOrder", NoFakturOrder)
            Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim rowIndex As Integer = Dgv_Detail.Rows.Add(
                        If(IsDBNull(Dr("No_Faktur")), "", Dr("No_Faktur")),
                        If(IsDBNull(Dr("Kode_Barang")), "", Dr("Kode_Barang")),
                        If(IsDBNull(Dr("Satuan")), "", Dr("Satuan")),
                        If(IsDBNull(Dr("Qty_MR")), 0, Convert.ToDecimal(Dr("Qty_MR"))),
                        If(IsDBNull(Dr("Qty_TF")), 0, Convert.ToDecimal(Dr("Qty_TF")))
                    )

                    Dim status As String = If(IsDBNull(Dr("Status_Proses")), "", Dr("Status_Proses").ToString())

                    Select Case status
                        Case "SELESAI PROSES"
                            Dgv_Detail.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightGreen

                        Case "SEDANG DIPROSES"
                            Dgv_Detail.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightYellow

                        Case "BELUM DIPROSES"
                            Dgv_Detail.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightCoral
                    End Select
                End While

                Dr.Close()
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        'Clear dgv dan lv
        Lv_RFID_Tags.Items.Clear()
        Dgv_Detail.Rows.Clear()

        'Reset pagination
        CurrentPage = 1
        PageSize = 19
        TotalData = 0
        TotalPage = 0

        FlagScanManual = False

        SelectedNoFaktur = ""

        Select Case TabControl1.SelectedIndex
            Case 0
                Fetch_Menunggu_Pairing_RFID()
                Btn_Simpan.Visible = True
                Btn_Update.Visible = False
            Case 1
                Fetch_Sudah_Pairing_RFID()
                Btn_Simpan.Visible = False
                Btn_Update.Visible = True
        End Select
    End Sub

    Private Sub Dgv_Menunggu_Pairing_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Menunggu_Pairing.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        Dim NoFakturOrder As String = If(IsDBNull(Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingRFIDNoFakturOrder").Value), "", Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingRFIDNoFakturOrder").Value.ToString())
        SelectedNoFaktur = NoFakturOrder
        FlagScanManual = Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingFlagScanManual").Value = "Y"

        If Dgv_Menunggu_Pairing.Columns(e.ColumnIndex).Name <> "BtnMenungguPairingRFID" Then Exit Sub

        RichTextBox1.Focus()

        ' Kalau mode manual, konfirmasi dulu
        If FlagScanManual Then
            Dim result = MessageBox.Show(
                "No Transaksi ini diperbolehkan untuk scan RFID manual." & vbCrLf &
                "Apakah anda ingin melanjutkan scan manual?",
                "Metode Pairing",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

            ' YES = manual → stop di sini
            If result = DialogResult.Yes Then Exit Sub

            ' NO = scan otomatis → lanjut connect RFID
            FlagScanManual = False
        End If

        ' Pastikan RFID siap
        If Not EnsureRFIDConnected() Then Exit Sub
    End Sub

    Private Sub Dgv_Sudah_Pairing_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Sudah_Pairing.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        Dim row = Dgv_Sudah_Pairing.Rows(e.RowIndex)
        Dim NoFakturOrder As String = If(IsDBNull(row.Cells("SudahPairingRFIDNoFakturOrder").Value), "", row.Cells("SudahPairingRFIDNoFakturOrder").Value.ToString())
        SelectedNoFaktur = NoFakturOrder
        FlagScanManual = row.Cells("SudahPairingFlagScanManual").Value = "Y"

        If Dgv_Sudah_Pairing.Columns(e.ColumnIndex).Name <> "BtnPairingUlangRFID" Then Exit Sub

        RichTextBox1.Focus()

        ' Kalau mode manual, konfirmasi dulu
        If FlagScanManual Then
            Dim result = MessageBox.Show(
                "No Transaksi ini diperbolehkan untuk scan RFID manual." & vbCrLf &
                "Apakah anda ingin melanjutkan scan manual?",
                "Metode Pairing",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

            ' YES = manual → stop di sini
            If result = DialogResult.Yes Then
                Lv_RFID_Tags.Items.Clear()
                Exit Sub
            End If

            ' NO = scan otomatis → lanjut connect RFID
            FlagScanManual = False
        End If

        ' Pastikan RFID siap
        If EnsureRFIDConnected() Then
            Lv_RFID_Tags.Items.Clear()
            Exit Sub
        End If
    End Sub

    Private Function EnsureRFIDConnected() As Boolean
        If RFIDReader.IsConnected Then Return True

        If Not PingHost(RFIDReaderIP) Then
            MessageBox.Show(
            "Reader RFID tidak terdeteksi di jaringan.",
            "Peringatan",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )
            Return False
        End If

        If Not RFIDReader.Connect(500) Then
            MessageBox.Show(
            "Gagal terhubung ke reader RFID.",
            "Peringatan",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )
            Return False
        End If

        Return True
    End Function

    Function PingHost(ip As String) As Boolean
        Try
            Dim ping As New Net.NetworkInformation.Ping()
            Dim reply = ping.Send(ip, 1000)
            Return reply.Status = Net.NetworkInformation.IPStatus.Success
        Catch
            Return False
        End Try
    End Function

    Private Sub Btn_Disconnect_RFID_Click(sender As Object, e As EventArgs) Handles Btn_Disconnect_RFID.Click
        If RFIDReader.IsConnected Then
            RFIDReader.Disconnect()
        End If
    End Sub

    Private Sub N_EMI_Pairing_RFID_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If RFIDReader.IsConnected Then
            RFIDReader.Disconnect()
        End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Lv_RFID_Tags.Items.Clear()
        Dgv_Detail.Rows.Clear()
        TabControl1.SelectedIndex = 0
        Fetch_Menunggu_Pairing_RFID()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Lv_RFID_Tags.Items.Count = 0 Then
            MessageBox.Show("Tidak ada data RFID Tag untuk disimpan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If RFIDReader.IsConnected Then
            RFIDReader.Disconnect()
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim noFaktur As String = SelectedNoFaktur
            If String.IsNullOrEmpty(noFaktur) Then
                MessageBox.Show("Silakan pilih No Transaksi terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            For Each tag As ListViewItem In Lv_RFID_Tags.Items
                Dim rfid_tag As String = tag.Text
                SQL = "
                    INSERT INTO N_EMI_Pairing_RFID
                    (Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag,
                     Tanggal_Pairing, Jam_Pairing, UserID_Pairing, Lokasi_Pairing, Flag_Scan_Manual)
                    VALUES
                    (@KodePerusahaan, @NoFaktur, @KodeStockOwner, @RFID_Tag,
                     @TanggalPairing, @JamPairing, @UserIDPairing, @LokasiPairing, @FlagScanManual)
                "

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)
                Cmd.Parameters.AddWithValue("KodeStockOwner", Lokasi)
                Cmd.Parameters.AddWithValue("LokasiPairing", Lokasi)
                Cmd.Parameters.AddWithValue("RFID_Tag", rfid_tag)
                Cmd.Parameters.AddWithValue("TanggalPairing", Date.Now)
                Cmd.Parameters.AddWithValue("JamPairing", Date.Now.ToString("HH:mm:ss"))
                Cmd.Parameters.AddWithValue("UserIDPairing", UserID)
                Cmd.Parameters.AddWithValue(
                    "@FlagScanManual",
                    If(FlagScanManual, "Y", CType(DBNull.Value, Object))
                )

                ExecuteTrans(SQL)

                SQL = "UPDATE N_EMI_Master_Data_RFID_Tags SET Status = 'Y' WHERE RFID_Tag = @RFID_Tag"
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("RFID_Tag", rfid_tag)

                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            Lv_RFID_Tags.Items.Clear()
            Dgv_Detail.Rows.Clear()

            FlagScanManual = False

            TabControl1.SelectedIndex = 0
            Fetch_Menunggu_Pairing_RFID()

            MessageBox.Show("Data pairing RFID Tag berhasil disimpan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            If Cmd.Transaction IsNot Nothing Then
                Cmd.Transaction.Rollback()
            End If
            CloseTrans()
            CloseConn()
            MessageBox.Show("Gagal menyimpan data binding: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Dgv_Menunggu_Pairing_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Menunggu_Pairing.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim NoFakturOrder As String = If(IsDBNull(Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingRFIDNoFakturOrder").Value), "", Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingRFIDNoFakturOrder").Value.ToString())
        Fetch_Dgv_Detail(NoFakturOrder)
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs)
        Select Case TabControl1.SelectedIndex
            Case 0
                Fetch_Menunggu_Pairing_RFID()
            Case 1
                Fetch_Sudah_Pairing_RFID()
        End Select
    End Sub

    Private Sub Dgv_Sudah_Pairing_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Sudah_Pairing.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim NoFakturOrder As String = If(IsDBNull(Dgv_Sudah_Pairing.Rows(e.RowIndex).Cells("SudahPairingRFIDNoFakturOrder").Value), "", Dgv_Sudah_Pairing.Rows(e.RowIndex).Cells("SudahPairingRFIDNoFakturOrder").Value.ToString())
        Fetch_Dgv_Detail(NoFakturOrder)
        Fetch_RFID_Pair(NoFakturOrder)
    End Sub

    Private Sub Fetch_RFID_Pair(NoFakturOrder)
        Try
            OpenConn()
            Lv_RFID_Tags.Items.Clear()

            SQL = "SELECT RFID_Tag FROM N_EMI_Pairing_RFID WHERE No_Split_Production_Order = @NoFakturOrder"

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("NoFakturOrder", NoFakturOrder)

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim rfidTag As String = ""
                    If Dr("RFID_Tag") IsNot DBNull.Value Then
                        rfidTag = Dr("RFID_Tag").ToString()
                    End If

                    Dim item As New ListViewItem(rfidTag)
                    Lv_RFID_Tags.Items.Add(item)
                End While

                Dr.Close()
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_Update_Click(sender As Object, e As EventArgs) Handles Btn_Update.Click
        If Lv_RFID_Tags.Items.Count = 0 Then
            MessageBox.Show("Tidak ada data RFID Tag untuk disimpan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If RFIDReader.IsConnected Then
            RFIDReader.Disconnect()
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim noFaktur As String = SelectedNoFaktur
            If String.IsNullOrEmpty(noFaktur) Then
                MessageBox.Show("Silakan pilih No Transaksi terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim urutPairingBaru As Integer = 1

            SQL = "
                SELECT ISNULL(MAX(Urut_Pairing), 0) + 1 AS UrutPairingBaru
                FROM N_EMI_Pairing_RFID
                WHERE Kode_Perusahaan = @KodePerusahaan
                  AND No_Split_Production_Order = @NoFaktur
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    urutPairingBaru = Convert.ToInt32(Dr("UrutPairingBaru"))
                End If
                Dr.Close()
            End Using

            SQL = "
                INSERT INTO N_EMI_Pairing_RFID_Log
                (Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag, 
                 Tanggal_Pairing, Jam_Pairing, UserID_Pairing, Flag_Pairing_Ulang, Urut_Pairing, Lokasi_Pairing)
                SELECT 
                    Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag,
                    Tanggal_Pairing, Jam_Pairing, UserID_Pairing, 'Y', Urut_Pairing, Lokasi_Pairing
                FROM N_EMI_Pairing_RFID
                WHERE Kode_Perusahaan = @KodePerusahaan
                  AND No_Split_Production_Order = @NoFaktur
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)
            ExecuteTrans(SQL)

            SQL = "SELECT RFID_Tag FROM N_EMI_Pairing_RFID WHERE Kode_Perusahaan = @KodePerusahaan AND No_Split_Production_Order = @NoFaktur"
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim rfidTag As String = Dr("RFID_Tag").ToString()
                    SQL = "UPDATE N_EMI_Master_Data_RFID_Tags SET Status = NULL WHERE RFID_Tag = @RFIDTag"
                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("RFIDTag", rfidTag)
                    ExecuteTrans(SQL)
                End While
                Dr.Close()
            End Using

            SQL = "
                DELETE FROM N_EMI_Pairing_RFID
                WHERE Kode_Perusahaan = @KodePerusahaan
                  AND No_Split_Production_Order = @NoFaktur
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)
            ExecuteTrans(SQL)

            For Each tag As ListViewItem In Lv_RFID_Tags.Items
                SQL = "
                    INSERT INTO N_EMI_Pairing_RFID
                    (Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner,
                     RFID_Tag, Urut_Pairing, Tanggal_Pairing, Jam_Pairing, UserID_Pairing, Lokasi_Pairing)
                    VALUES
                    (@KodePerusahaan, @NoFaktur, @KodeStockOwner,
                     @RFID_Tag, @UrutPairing, @TanggalPairing, @JamPairing, @UserIDPairing, @LokasiPairing)
                "

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)
                Cmd.Parameters.AddWithValue("KodeStockOwner", Lokasi)
                Cmd.Parameters.AddWithValue("RFID_Tag", tag.Text)
                Cmd.Parameters.AddWithValue("UrutPairing", urutPairingBaru)
                Cmd.Parameters.AddWithValue("TanggalPairing", Date.Now)
                Cmd.Parameters.AddWithValue("JamPairing", Date.Now.ToString("HH:mm:ss"))
                Cmd.Parameters.AddWithValue("UserIDPairing", UserID)
                Cmd.Parameters.AddWithValue("LokasiPairing", Lokasi)

                ExecuteTrans(SQL)

                SQL = "UPDATE N_EMI_Master_Data_RFID_Tags SET Status = 'Y' WHERE RFID_Tag = @RFIDTag"
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("RFID_Tag", tag.Text)

                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            FlagScanManual = False

            MessageBox.Show("Pairing ulang RFID Tag berhasil disimpan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            If Cmd.Transaction IsNot Nothing Then
                Cmd.Transaction.Rollback()
            End If
            CloseTrans()
            CloseConn()
            MessageBox.Show("Gagal menyimpan pairing ulang: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_Prev_Click(sender As Object, e As EventArgs) Handles Btn_Prev.Click
        If CurrentPage > 1 Then
            CurrentPage -= 1
            Fetch_Menunggu_Pairing_RFID()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Btn_Next.Click
        CurrentPage += 1
        Fetch_Menunggu_Pairing_RFID()
    End Sub
End Class