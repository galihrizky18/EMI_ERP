Imports System.IO

Public Class N_EMI_Dashboard_Formula
    Private ActiveMenuKomponen As Label
    Private ActiveMenuTrackingProgress As Label

    Dim CellParent_NoFormula As Integer = 0
    Dim CellParent_TanggalFormula As Integer = 1
    Dim CellParent_KdBarang As Integer = 2
    Dim CellParent_NmBarang As Integer = 3
    Dim CellParent_HPPMin As Integer = 4
    Dim CellParent_HPPMax As Integer = 5
    Dim CellParent_Jumlah As Integer = 6
    Dim CellParent_Satuan As Integer = 7
    Dim CellParent_JenisFormula As Integer = 8
    Dim CellParent_PosisiBinding As Integer = 9
    Dim CellParent_StatusFormula As Integer = 10
    Dim CellParent_Deskripsi As Integer = 11
    Dim CellParent_BtnValidasi As Integer = 12
    Dim CellParent_StatusBypass As Integer = 13

    Dim UserPosition As String = ""

    Dim Status_HeadDept As String() = {"Belum Diproses", "Selesai Trial Kitchen", "Selesai Trial Produksi", "Proses Trial Produksi"}
    Dim Status_BOD As String() = {"Menunggu Validasi BOD", "Proses Produksi Komersial"}

    Dim DgvParent_NoFormula, DgvParent_TanggalFormula, DgvParent_KdBarang, DgvParent_NmBarang, DgvParent_HPPMin, DgvParent_HPPMax, DgvParent_Jumlah, DgvParent_Satuan,
        DgvParent_JenisFormula, DgvParent_PosisiBinding, DgvParent_StatusFormula, DgvParent_Deskripsi, DgvParent_BtnValidasi, DgvParent_StatusBypass As String

    Dim list As New List(Of PalatRow)
    Dim headerSet As New HashSet(Of String)

    Private _activeRTB As RichTextBox = Nothing
    Private WithEvents _tsBold As ToolStripButton
    Private WithEvents _tsItalic As ToolStripButton
    Private WithEvents _tsUnderline As ToolStripButton
    Private WithEvents _tsBullet As ToolStripButton

    Private Sub Me_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub N_EMI_Dashboard_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Me.Dock = DockStyle.Fill

        Try
            OpenConn()

            If CekButtonRole("User_Formula_Position_Staff") = "Y" Then
                UserPosition = "STAFF"

                Cmb_Filter_Status.Items.Remove("Formula Outstanding")

                CloseTrans()
                CloseConn()
                Exit Try
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal cek role user: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("User_Formula_Position_HeadDept") = "Y" Then
                UserPosition = "HEADDEPT"
                CloseTrans()
                CloseConn()
                Exit Try
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal cek role user: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("User_Formula_Position_CLevel") = "Y" Then
                UserPosition = "CLEVEL"
                CloseTrans()
                CloseConn()
                Exit Try
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal cek role user: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

        If String.IsNullOrWhiteSpace(UserPosition) Then
            MessageBox.Show("Terjadi Kesalahan, Posisi User Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
        End If

        Try
            OpenConn()

            If CekButtonRole("Tampil_HPP_Min_Max") = "T" Then
                DGV_Formula.Columns("HPP_Min").Visible = False
                DGV_Formula.Columns("HPP_Max").Visible = False
            Else
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal cek akses HPP: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        TB_NoFormula.Clear()
        TB_NoSplitFormula.Clear()
        TB_SplitType.Clear()
        TB_NoSplitKomponen.Clear()
        TB_SplitTypeKomponen.Clear()

        LB_Persen.Visible = False
        TB_Persen.Visible = False
        LB_HPP_Pcs.Visible = False
        TB_HPP_Pcs.Visible = False

        DGV_Komponen.Height = 246
        TC_CookingStep.Visible = False
        BTN_SimpanCookingStep.Visible = False
        PNL_Komponen.BackColor = Color.Silver

        Cmb_Filter_Status.SelectedIndex = 0

        _activeRTB = RTB_TrialKitchen

        Init_Komponen_Menu()
        Init_Tracking_Progress_Menu()
    End Sub

    Private Sub Init_Komponen_Menu()
        TLP_Komponen.Controls.Clear()

        Dim menus() As String = {
            "HPP Sementara",
            "Bahan Material",
            "Moisture Content",
            "Cooking Step",
            "Daftar Split"
        }

        Try
            OpenConn()

            If CekButtonRole("Tampil_Semua_Komponen_Formula") = "T" Then
                Dim menuList As New List(Of String)(menus)

                menuList.Remove("HPP Sementara")
                menuList.Remove("Bahan Material")
                menuList.Remove("Cooking Step")

                menus = menuList.ToArray()
            Else
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal cek akses HPP: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim rowPercent As Single = If(menus.Length < 5, 20, 100 / menus.Length)

        TLP_Komponen.ColumnCount = 1
        TLP_Komponen.RowCount = 5
        TLP_Komponen.RowStyles.Clear()
        TLP_Komponen.ColumnStyles.Clear()
        TLP_Komponen.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

        For i As Integer = 0 To 4
            TLP_Komponen.RowStyles.Add(New RowStyle(SizeType.Percent, 20))
        Next

        For i As Integer = 0 To menus.Length - 1
            TLP_Komponen.RowStyles.Add(New RowStyle(SizeType.Percent, rowPercent))

            Dim lbl As New Label()

            With lbl
                .Text = menus(i)
                .Dock = DockStyle.Fill
                .TextAlign = ContentAlignment.MiddleLeft
                .Padding = New Padding(10, 0, 0, 0)
                .Cursor = Cursors.Hand
                .Margin = New Padding(1)
                .Font = New Font("Work Sans", 8, FontStyle.Regular)
                .BackColor = Color.White
                .ForeColor = Color.Black
            End With

            AddHandler lbl.Click, AddressOf Komponen_Menu_Click

            TLP_Komponen.Controls.Add(lbl, 0, i)

            If i = 0 Then
                Set_Active_Menu_Komponen(lbl)

                If lbl.Text = "HPP Sementara" Then
                    Load_Komponen_HPP_Sementara()
                ElseIf lbl.Text = "Bahan Material" Then
                    Load_Komponen_Bahan_Material()
                ElseIf lbl.Text = "Moisture Content" Then
                    Load_Komponen_Moisture_Content()
                ElseIf lbl.Text = "Cooking Step" Then
                    Load_Komponen_Cooking_Step()
                ElseIf lbl.Text = "Daftar Split" Then
                    Load_Komponen_Daftar_Split()
                End If
            End If
        Next
    End Sub

    Private Sub Init_Tracking_Progress_Menu()
        TLP_TrackingProgress.Controls.Clear()

        Dim menus() As String = {
            "Look View",
            "Analisa Lab",
            "Palatabilitas"
        }

        TLP_TrackingProgress.ColumnCount = 1
        TLP_TrackingProgress.RowCount = 5
        TLP_TrackingProgress.RowStyles.Clear()
        TLP_TrackingProgress.ColumnStyles.Clear()
        TLP_TrackingProgress.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

        For i As Integer = 0 To 4
            TLP_TrackingProgress.RowStyles.Add(New RowStyle(SizeType.Percent, 20))
        Next

        For i As Integer = 0 To menus.Length - 1
            Dim lbl As New Label()

            With lbl
                .Text = menus(i)
                .Dock = DockStyle.Fill
                .TextAlign = ContentAlignment.MiddleLeft
                .Padding = New Padding(10, 0, 0, 0)
                .Cursor = Cursors.Hand
                .Margin = New Padding(1)
                .Font = New Font("Work Sans", 8, FontStyle.Regular)
                .BackColor = Color.White
                .ForeColor = Color.Black
            End With

            AddHandler lbl.Click, AddressOf Tracking_Progress_Menu_Click
            TLP_TrackingProgress.Controls.Add(lbl, 0, i)
        Next

        TLP_TrackingProgress.Enabled = False
    End Sub

    Private Sub Init_Filter()
    End Sub

    Private Sub Komponen_Menu_Click(sender As Object, e As EventArgs)
        If TB_NoFormula.Text.Trim = "" Then
            MessageBox.Show("Silahkan pilih formula terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim lbl As Label = CType(sender, Label)

        If lbl.Text = "HPP Sementara" Then
            Load_Komponen_HPP_Sementara()
        ElseIf lbl.Text = "Bahan Material" Then
            Load_Komponen_Bahan_Material()
        ElseIf lbl.Text = "Moisture Content" Then
            Load_Komponen_Moisture_Content()
        ElseIf lbl.Text = "Cooking Step" Then
            Load_Komponen_Cooking_Step()
        ElseIf lbl.Text = "Daftar Split" Then
            Load_Komponen_Daftar_Split()
        End If

        If lbl.Text <> "Daftar Split" Then
            Reset_Active_Menu_Tracking_Progress()
            DGV_Detail_Pengujian.Rows.Clear()
        Else
        End If

        If lbl.Text <> "Bahan Material" Then
            LB_Persen.Visible = False
            TB_Persen.Visible = False
            LB_HPP_Pcs.Visible = False
            TB_HPP_Pcs.Visible = False

            DGV_Komponen.Height = 246
        Else
            LB_Persen.Visible = True
            TB_Persen.Visible = True
            LB_HPP_Pcs.Visible = True
            TB_HPP_Pcs.Visible = True

            DGV_Komponen.Height = 217
        End If

        Set_Active_Menu_Komponen(lbl)
    End Sub


    Private Sub Set_Active_Menu_Komponen(menuLabel As Label)
        For Each ctrl As Control In TLP_Komponen.Controls
            If TypeOf ctrl Is Label Then
                Dim lbl As Label = CType(ctrl, Label)

                lbl.BackColor = Color.White
                lbl.ForeColor = Color.Black
                lbl.Font = New Font("Work Sans", 8, FontStyle.Regular)
            End If
        Next

        menuLabel.BackColor = Color.LightBlue
        menuLabel.ForeColor = Color.Black
        menuLabel.Font = New Font("Work Sans", 8, FontStyle.Bold)

        ActiveMenuKomponen = menuLabel
    End Sub

    Private Sub Reset_Active_Menu_Komponen()
        For Each ctrl As Control In TLP_Komponen.Controls
            If TypeOf ctrl Is Label Then
                Dim lbl As Label = CType(ctrl, Label)

                lbl.BackColor = Color.White
                lbl.ForeColor = Color.Black
                lbl.Font = New Font("Work Sans", 8, FontStyle.Regular)
            End If
        Next

        ActiveMenuKomponen = Nothing
    End Sub

    Private Sub Tracking_Progress_Menu_Click(sender As Object, e As EventArgs)
        Dim lbl As Label = CType(sender, Label)

        If lbl.Text = "Look View" Then
            Load_Tracking_Progress_Look_View()
        ElseIf lbl.Text = "Analisa Lab" Then
            Load_Tracking_Progress_Analisa_Lab()
        ElseIf lbl.Text = "Palatabilitas" Then
            Load_Tracking_Progress_Palatabilitas()
        End If

        Set_Active_Menu_Tracking_Progress(lbl)
    End Sub

    Private Sub Set_Active_Menu_Tracking_Progress(menuLabel As Label)
        For Each ctrl As Control In TLP_TrackingProgress.Controls
            If TypeOf ctrl Is Label Then
                Dim lbl As Label = CType(ctrl, Label)

                lbl.BackColor = Color.White
                lbl.ForeColor = Color.Black
                lbl.Font = New Font("Work Sans", 8, FontStyle.Regular)
            End If
        Next

        menuLabel.BackColor = Color.LightBlue
        menuLabel.ForeColor = Color.Black
        menuLabel.Font = New Font("Work Sans", 8, FontStyle.Bold)

        ActiveMenuTrackingProgress = menuLabel
    End Sub
    Private Sub Reset_Active_Menu_Tracking_Progress()
        For Each ctrl As Control In TLP_TrackingProgress.Controls
            If TypeOf ctrl Is Label Then
                Dim lbl As Label = CType(ctrl, Label)

                lbl.BackColor = Color.White
                lbl.ForeColor = Color.Black
                lbl.Font = New Font("Work Sans", 8, FontStyle.Regular)
            End If
        Next

        ActiveMenuTrackingProgress = Nothing
    End Sub

    Public Sub Load_Formula()
        Try
            OpenConn()

            Dim filterStatus As String = ""
            Dim filterParameter As String = ""
            Dim valueCari As String = Tb_Value.Text.Trim.Replace("'", "''")
            Dim filterTanggal As String = ""
            Dim filterTanggalParameter As String = ""

            If Cb_Transaksi_Hari_Ini.Checked Then
                filterTanggal = " AND CONVERT(DATE, a.Tanggal_Validasi) = CONVERT(DATE, GETDATE()) "
            End If

            Select Case Cmb_Filter_Status.Text.Trim()
                Case "Formula Outstanding"
                    Dim filterByUserPosition As String = ""
                    If UserPosition.Trim = "HEADDEPT" Then
                        filterByUserPosition = "
                            (
                                Flag_Lanjut_Trial_Kitchen IS NULL
                                AND Flag_Selesai_Trial_Kitchen IS NULL
                                AND Flag_Lanjut_Trial_Produksi IS NULL
                                AND Flag_Selesai_Trial_Produksi IS NULL
                                AND Flag_Lanjut_Produksi IS NULL
                                AND Flag_Validasi_Formula_Produksi_BOD IS NULL
                            )
                            OR
                            (
                                Flag_Selesai_Trial_Kitchen = 'Y'
                                AND Flag_Lanjut_Trial_Produksi IS NULL
                                AND Flag_Lanjut_Produksi IS NULL
                            )
                            OR
                            (
                                Flag_Selesai_Trial_Produksi = 'Y'
                                AND Flag_Lanjut_Produksi IS NULL
                            )
                            OR
                            (
                                Flag_Lanjut_Trial_Produksi = 'Y'
                                AND Flag_Lanjut_Produksi IS NULL
                            )
                        "
                    ElseIf UserPosition.Trim = "CLEVEL" Then
                        filterByUserPosition = "
                            (
                                Flag_Lanjut_Produksi = 'Y'
                                AND Flag_Validasi_Formula_Produksi_BOD IS NULL
                            )
                        "
                    End If

                    filterStatus = $"
                        AND ISNULL(a.Flag_Lanjut_Trial_Kitchen, '') <> 'T'
                        AND ISNULL(a.Flag_Lanjut_Trial_Produksi, '') <> 'T'
                        AND ISNULL(a.Flag_Lanjut_Produksi, '') <> 'T'
                        AND ISNULL(a.Flag_Validasi_Formula_Produksi_BOD, '') <> 'T'
                        AND (
                            {filterByUserPosition}
                        )
                    "
                Case "Formula Sedang Diproses"
                    filterStatus = "
                        AND ISNULL(a.Flag_Lanjut_Trial_Kitchen, '') <> 'T'
                        AND ISNULL(a.Flag_Lanjut_Trial_Produksi, '') <> 'T'
                        AND ISNULL(a.Flag_Lanjut_Produksi, '') <> 'T'
                        AND ISNULL(a.Flag_Validasi_Formula_Produksi_BOD, '') <> 'T'

                        AND (
                            (a.Flag_Lanjut_Trial_Kitchen = 'Y' AND a.Flag_Selesai_Trial_Kitchen IS NULL)
                            OR (a.Flag_Lanjut_Trial_Produksi = 'Y' AND a.Flag_Selesai_Trial_Produksi IS NULL)
                            OR (a.FLag_Lanjut_Produksi = 'Y' AND a.Flag_Validasi_Formula_Produksi_BOD IS NULL)
                        )
                    "
                Case "Formula Selesai"
                    filterStatus = "
                        AND ISNULL(a.Flag_Lanjut_Trial_Kitchen, '') <> 'T'
                        AND ISNULL(a.Flag_Lanjut_Trial_Produksi, '') <> 'T'
                        AND ISNULL(a.Flag_Lanjut_Produksi, '') <> 'T'
                        AND ISNULL(a.Flag_Validasi_Formula_Produksi_BOD, '') <> 'T'

                        AND a.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                    "
                Case "Formula Ditolak"
                    filterStatus = "
                        AND (
                            a.Flag_Lanjut_Trial_Kitchen = 'T'
                            OR a.Flag_Lanjut_Trial_Produksi = 'T'
                            OR a.Flag_Lanjut_Produksi = 'T' 
                            OR a.Flag_Validasi_Formula_Produksi_BOD = 'T'   
                        )
                    "
                Case Else
                    filterStatus = ""
            End Select

            If Cmb_Parameter_Lain.Text.Trim <> "" Then

                If valueCari = "" Then
                    MessageBox.Show("Silahkan isi value pencarian", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Tb_Value.Focus()
                    Exit Sub
                End If

                Select Case Cmb_Parameter_Lain.Text.Trim()

                    Case "No. Formula"
                        filterParameter = $" AND a.No_Faktur LIKE '%{valueCari}%' "

                    Case "Kode Barang"
                        filterParameter = $" AND b.Kode_Barang_Inq LIKE '%{valueCari}%' "

                    Case "Nama Barang"
                        filterParameter = $" AND b.Nama LIKE '%{valueCari}%' "

                    Case "Jenis Formula"
                        filterParameter = $" AND ISNULL(a.Kode_Hierarki, '') LIKE '%{valueCari}%' "

                    Case "Posisi Binding"
                        filterParameter = $"
                            AND EXISTS (
                                SELECT 1
                                FROM N_EMI_Transaksi_Formulator_Binding x
                                JOIN N_EMI_Transaksi_Formulator_Binding_Detail y 
                                    ON y.Kode_Perusahaan = x.Kode_Perusahaan
                                    AND y.No_Faktur = x.No_Faktur
                                WHERE y.No_Formulator = a.No_Faktur
                                    AND x.Status IS NULL
                                    AND x.Flag_Validasi_Main = 'Y'
                                    AND CAST(y.No_Prioritas AS VARCHAR) LIKE '%{valueCari}%'
                            )
                        "
                End Select
            End If

            If Cb_Parameter_Tanggal.Checked Then

                If DTP_Start.Value.Date > DTP_End.Value.Date Then
                    MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal akhir.",
                        Judul,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)

                    DTP_Start.Focus()
                    Exit Sub
                End If

                Select Case Cmb_Paramater_Tanggal.Text.Trim()

                    Case "Tanggal Formula"
                        filterTanggalParameter = $"
                        AND CAST(a.Tanggal_Validasi AS DATE)
                        BETWEEN '{Format(DTP_Start.Value, "yyyy-MM-dd")}'
                        AND '{Format(DTP_End.Value, "yyyy-MM-dd")}'
                    "

                End Select
            End If

            Dim SQL_LoadFormula As String = $"
                SELECT 
                    a.No_Faktur,
                    FORMAT(a.Tanggal_Validasi, 'dd MMM yyyy') AS Tanggal_Validasi,
                    b.Kode_Barang_Inq, 
                    b.Nama, 
                    0 AS HPP_Min, 
                    0 AS HPP_Max, 
                    a.Hasil,
                    a.Satuan_Hasil,
                    a.Kode_Hierarki,  
                    ISNULL(fb.No_Prioritas, NULL) AS Posisi_Binding,
                    CASE 
                        WHEN a.Flag_Validasi_Formula_Produksi_BOD = 'T'
                        THEN 'Ditolak Untuk Produksi'
                        WHEN a.Flag_Lanjut_Produksi = 'T'
                        THEN 'Ditolak Lanjut Produksi'
                        WHEN a.Flag_Lanjut_Trial_Produksi = 'T'
                        THEN 'Ditolak Lanjut Trial Produksi'
                        WHEN a.Flag_Lanjut_Trial_Kitchen = 'T'
                        THEN 'Ditolak Lanjut Trial Kitchen'

                        WHEN a.Flag_Lanjut_Produksi = 'Y' AND a.Flag_Validasi_Formula_Produksi_BOD = 'Y' THEN 'Produksi'
                        WHEN a.Flag_Lanjut_Produksi = 'Y' AND a.Flag_Validasi_Formula_Produksi_BOD IS NULL THEN 'Menunggu Validasi BOD'
                        WHEN a.Flag_Selesai_Produksi = 'Y' THEN 'Selesai Produksi Komersial'
                        WHEN a.Flag_Lanjut_Produksi = 'Y' THEN 'Proses Produksi Komersial'
                        WHEN a.Flag_Selesai_Trial_Produksi = 'Y' THEN 'Selesai Trial Produksi'
                        WHEN a.Flag_Lanjut_Trial_Produksi = 'Y' THEN 'Proses Trial Produksi'
                        WHEN a.Flag_Selesai_Trial_Kitchen = 'Y' THEN 'Selesai Trial Kitchen'
                        WHEN a.Flag_Lanjut_Trial_Kitchen = 'Y' THEN 'Proses Trial Kitchen'
                        WHEN a.Flag_Validasi = 'Y' THEN 'Belum Diproses'
                        WHEN a.Flag_Validasi IS NULL THEN 'Belum Validasi Tahap 1'
                        ELSE '-'
                    END AS Status_Formula,
                    CASE
                        WHEN a.Keterangan_Bypass_trial IS NOT NULL THEN a.Keterangan_Bypass_trial
                        WHEN a.Keterangan_Bypass_Trial_Produksi_On_Process IS NOT NULL THEN a.Keterangan_Bypass_Trial_Produksi_On_Process
                        WHEN a.Flag_Lanjut_Produksi = 'Y'
                             OR a.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                        THEN p.Deskripsi
                        WHEN a.Flag_Lanjut_Trial_Produksi = 'Y'
                             OR a.Flag_Selesai_Trial_Produksi = 'Y'
                        THEN tp.Deskripsi
                        WHEN a.Flag_Lanjut_Trial_Kitchen = 'Y'
                             OR a.Flag_Selesai_Trial_Kitchen = 'Y'
                        THEN tk.Deskripsi
                        ELSE ''
                    END AS Deskripsi,
                    'Validasi' AS Validasi,
                    CASE 
                        WHEN a.Keterangan_Bypass_trial IS NOT NULL THEN 'BYPASS_TRIAL'
                        WHEN a.Keterangan_Bypass_Trial_Produksi_On_Process IS NOT NULL THEN 'BYPASS_TRIAL_PRODUKSI_ON_PROCESS'
                        ELSE 'NORMAL'
                    END AS Status_Bypass
                FROM EMI_Transaksi_Formulator a
                JOIN Barang b ON b.Kode_Perusahaan = a.Kode_Perusahaan AND b.Kode_Barang_Inq = a.Kode_Barang AND b.Kode_Stock_Owner = a.Kode_Stock_Owner
                OUTER APPLY (
                    SELECT TOP 1 y.Deskripsi
                    FROM N_EMI_Transaksi_Trial_Order_Produksi x
                    JOIN N_EMI_Transaksi_Trial_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                    WHERE x.Kode_Formula = a.No_Faktur
                    ORDER BY y.Tanggal DESC, y.Jam DESC
                ) tk
                OUTER APPLY (
                    SELECT TOP 1 y.Deskripsi
                    FROM EMI_Order_Produksi x
                    JOIN Emi_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                    WHERE x.Kode_Formula = a.No_Faktur AND x.Flag_Trial_Produksi = 'Y'
                    ORDER BY y.Tanggal DESC, y.Jam DESC
                ) tp
                OUTER APPLY (
                    SELECT TOP 1 y.Deskripsi
                    FROM EMI_Order_Produksi x
                    JOIN Emi_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                    WHERE x.Kode_Formula = a.No_Faktur
                    ORDER BY y.Tanggal DESC, y.Jam DESC
                ) p
                OUTER APPLY (
                    SELECT TOP 1 y.No_Prioritas
                    FROM N_EMI_Transaksi_Formulator_Binding x
                    JOIN N_EMI_Transaksi_Formulator_Binding_Detail y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_Faktur = x.No_Faktur
                    WHERE y.No_Formulator = a.No_Faktur AND x.Status IS NULL AND x.Flag_Validasi_Main = 'Y'
                    ORDER BY y.Tanggal DESC, y.Jam DESC
                ) fb
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}' 
                    AND a.Flag_Validasi = 'Y' 
                    AND a.Status IS NULL
                    {filterStatus}
                    {filterParameter}
                    {filterTanggal}
                    {filterTanggalParameter}
                ORDER BY a.Tanggal_Validasi DESC
            "

            DGV_Formula.Rows.Clear()

            Using Ds = BindingTrans(SQL_LoadFormula)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DGV_Formula.Rows.Add()

                            Dim statusFormula As String = .Rows(i).Item("Status_Formula").ToString()
                            Dim statusBypass As String = .Rows(i).Item("Status_Bypass").ToString()
                            Dim rowColor As Color = GetRowColor(statusFormula, statusBypass)
                            Dim posisiBinding As String = If(IsDBNull(.Rows(i).Item("Posisi_Binding")), "", .Rows(i).Item("Posisi_Binding").ToString())

                            If posisiBinding = "1" Then
                                posisiBinding = "FORMULA UTAMA"
                            ElseIf IsNumeric(posisiBinding) AndAlso CInt(posisiBinding) > 1 Then
                                posisiBinding = $"CADANGAN {CInt(posisiBinding) - 1}"
                            Else
                                posisiBinding = "-"
                            End If

                            DGV_Formula.Rows(i).Cells(CellParent_NoFormula).Value = .Rows(i).Item("No_Faktur")
                            DGV_Formula.Rows(i).Cells(CellParent_TanggalFormula).Value = .Rows(i).Item("Tanggal_Validasi")
                            DGV_Formula.Rows(i).Cells(CellParent_KdBarang).Value = .Rows(i).Item("Kode_Barang_Inq")
                            DGV_Formula.Rows(i).Cells(CellParent_NmBarang).Value = .Rows(i).Item("Nama").ToString().Trim
                            DGV_Formula.Rows(i).Cells(CellParent_HPPMin).Value = Format(.Rows(i).Item("HPP_Min"), "N2")
                            DGV_Formula.Rows(i).Cells(CellParent_HPPMax).Value = Format(.Rows(i).Item("HPP_Max"), "N2")
                            DGV_Formula.Rows(i).Cells(CellParent_Jumlah).Value = Format(.Rows(i).Item("Hasil"), "N4")
                            DGV_Formula.Rows(i).Cells(CellParent_Satuan).Value = .Rows(i).Item("Satuan_Hasil")
                            DGV_Formula.Rows(i).Cells(CellParent_JenisFormula).Value = If(General_Class.CekNULL(.Rows(i).Item("Kode_Hierarki")) = "", "-", .Rows(i).Item("Kode_Hierarki"))

                            DGV_Formula.Rows(i).Cells(CellParent_PosisiBinding).Value = posisiBinding

                            DGV_Formula.Rows(i).Cells(CellParent_StatusFormula).Value = statusFormula
                            DGV_Formula.Rows(i).Cells(CellParent_Deskripsi).Value = .Rows(i).Item("Deskripsi").ToString().Trim
                            DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = .Rows(i).Item("Validasi")
                            DGV_Formula.Rows(i).Cells(CellParent_StatusBypass).Value = .Rows(i).Item("Status_Bypass")
                            DGV_Formula.Rows(i).DefaultCellStyle.BackColor = rowColor

                            If UserPosition.Trim = "STAFF" Then
                                DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi) = New DataGridViewTextBoxCell()
                                DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style.ForeColor = Color.Black
                                DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = ""
                                DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).ReadOnly = True
                                DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style.BackColor = rowColor
                            Else
                                Dim btnCell As New DataGridViewButtonCell()
                                btnCell.FlatStyle = FlatStyle.Flat

                                Dim currentStatus As String = .Rows(i).Item("Status_Formula").ToString.Trim

                                Dim isButton As Boolean = False
                                Dim userPos As String = UserPosition.Trim

                                If userPos = "HEADDEPT" Then
                                    isButton = Status_HeadDept.Contains(currentStatus)
                                ElseIf userPos = "CLEVEL" Then
                                    isButton = Status_BOD.Contains(currentStatus)
                                End If

                                If isButton Then
                                    DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi) = btnCell
                                    DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = .Rows(i).Item("Validasi")

                                    With DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style
                                        .BackColor = Color.FromArgb(15, 86, 122)
                                        .ForeColor = Color.White
                                    End With
                                    DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).ReadOnly = False
                                Else
                                    DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi) = New DataGridViewTextBoxCell()
                                    DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style.BackColor = rowColor

                                    DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = ""
                                    DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).ReadOnly = True
                                End If

                            End If
                        Next
                    Else
                        TB_NoFormula.Clear()
                        TB_NoSplitFormula.Clear()
                        TB_SplitType.Clear()
                        TB_NoSplitKomponen.Clear()
                        TB_SplitTypeKomponen.Clear()
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal loading formula: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Komponen_HPP_Sementara()
        DGV_Komponen.Visible = True
        TC_CookingStep.Visible = False
        BTN_SimpanCookingStep.Visible = False
        PNL_Komponen.BackColor = Color.Silver

        With DGV_Komponen
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(CreateTextColumn("HS_Komponen", "Komponen", autoSizeMode:="fill"))
            .Columns.Add(CreateTextColumn("HS_Nilai", "Nilai", format:="N2", minWidth:=200))
            .Columns.Add(CreateTextColumn("HS_Satuan", "Satuan", alignment:="center", minWidth:=100))
        End With

        If TB_NoFormula.Text.Trim = "" Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = $"
                WITH cte_bahan AS (
                    SELECT 
                        a.Kode_Barang,
                        a.Kode_Bahan,
                        a.Jumlah_Barang,

                        (
                            ISNULL(
                                (
                                    SELECT TOP (1)
                                        dbo.get_hpp(x.serial_number)
                                    FROM barang_sn x
                                    WHERE x.kode_barang = a.kode_bahan
                                      AND x.blok_sn IS NULL
                                      AND dbo.get_hpp(x.serial_number) <> 0
                                    ORDER BY x.Tgl_masuk DESC
                                ),
                                b.estimasi_harga
                            )
                        ) / NULLIF(a.Jumlah_Barang, 0) AS hpp

                    FROM Barang_Detail_Bahan_Penolong a

                    INNER JOIN Barang b
                        ON a.Kode_Bahan = b.Kode_Barang

                    GROUP BY a.Kode_Bahan, a.Kode_Barang, a.Jumlah_Barang, b.Estimasi_Harga
                ),

                cte_wc AS (
                    SELECT 
                        a.Kode_Perusahaan,
                        a.Id_Jenis_Biaya_Produksi,
                        a.Kode_Jenis_Biaya_Produksi,

                        (
                            SELECT TOP (1) x.No_Faktur
                            FROM Emi_Transaksi_Work_Center x
                            WHERE x.Status IS NULL
                              AND x.Kode_Perusahaan = a.Kode_Perusahaan
                              AND x.Jenis_Biaya = a.Kode_Jenis_Biaya_Produksi
                            ORDER BY x.Id DESC
                        ) AS Faktur_WC

                    FROM Emi_Jenis_Biaya_Produksi a
                ),

                cte_produksi AS (
                    SELECT 
                        c.Id_Routing,
                        a.Kode_Jenis_Biaya_Produksi,
                        c.Id_Work_Center,
                        MAX(c.Nilai_Per_Pcs) AS Nilai_Per_Kg

                    FROM cte_wc a

                    JOIN Emi_Transaksi_Work_Center b
                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                       AND a.Faktur_WC = b.No_Faktur

                    JOIN Emi_Transaksi_Work_Center_Detail c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                       AND b.No_Faktur = c.No_Faktur

                    GROUP BY 
                        c.Id_Routing,
                        c.Id_Work_Center,
                        a.Kode_Jenis_Biaya_Produksi
                )

                SELECT 
                    SUM(ISNULL(c.Est_HPP_Per_Pcs, 0)) AS HPP_Bahan_Baku,

                    ISNULL((
                        SELECT SUM(x.hpp)
                        FROM cte_bahan x
                        WHERE x.Kode_Barang = b.Kode_Barang
                    ), 0) AS HPP_Packaging,

                    ISNULL((
                        SELECT SUM(x.Nilai_Per_Kg) / 1000 * d.Berat
                        FROM cte_produksi x
                        WHERE d.Id_Routing = x.Id_Routing
                    ), 0) AS HPP_Produksi,

                    'Per ' + d.Satuan AS Satuan

                FROM Emi_Transaksi_Formulator b

                INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c
                    ON b.Kode_Perusahaan = c.Kode_Perusahaan
                   AND b.No_Faktur = c.No_Faktur

                INNER JOIN Barang d
                    ON b.Kode_Perusahaan = d.Kode_Perusahaan
                   AND b.Kode_Stock_Owner = d.Kode_Stock_Owner
                   AND b.Kode_Barang = d.Kode_Barang_inq

                WHERE b.Kode_Perusahaan = '{KodePerusahaan}' AND
                    b.No_Faktur = '{TB_NoFormula.Text.Trim}'

                GROUP BY 
                    b.No_Faktur,
                    d.Nama,
                    b.Tanggal,
                    d.Satuan,
                    d.Berat,
                    d.Id_Routing,
                    b.Kode_Barang
            "

            Using Dr = OpenTrans(SQL)
                DGV_Komponen.Rows.Clear()

                If Dr.Read() Then
                    Dim hppBahanBaku As Decimal = If(IsDBNull(Dr("HPP_Bahan_Baku")), 0, CDec(Dr("HPP_Bahan_Baku")))
                    Dim hppPackaging As Decimal = If(IsDBNull(Dr("HPP_Packaging")), 0, CDec(Dr("HPP_Packaging")))
                    Dim hppProduksi As Decimal = If(IsDBNull(Dr("HPP_Produksi")), 0, CDec(Dr("HPP_Produksi")))
                    Dim satuan As String = If(IsDBNull(Dr("Satuan")), "", Dr("Satuan").ToString())

                    Dim totalHPP As Decimal = hppBahanBaku + hppPackaging + hppProduksi

                    DGV_Komponen.Rows.Add("HPP Bahan Baku", "Rp " & Format(hppBahanBaku, "N0") & ",-", satuan)
                    DGV_Komponen.Rows.Add("HPP Packaging", "Rp " & Format(hppPackaging, "N0") & ",-", satuan)
                    DGV_Komponen.Rows.Add("HPP Produksi", "Rp " & Format(hppProduksi, "N0") & ",-", satuan)
                    DGV_Komponen.Rows.Add("Total HPP Sementara", "Rp " & Format(totalHPP, "N0") & ",-", satuan)
                Else
                    DGV_Komponen.Rows.Add("HPP Bahan Baku", "-", "-")
                    DGV_Komponen.Rows.Add("HPP Packaging", "-", "-")
                    DGV_Komponen.Rows.Add("HPP Produksi", "-", "-")
                    DGV_Komponen.Rows.Add("Total HPP Sementara", "-", "-")
                End If

                DGV_Komponen.Rows(DGV_Komponen.Rows.Count - 1).DefaultCellStyle.Font = New Font(DGV_Komponen.Font, FontStyle.Bold)
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Gagal load hpp sementara untuk formula {TB_NoFormula.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Komponen_Bahan_Material()
        TC_CookingStep.Visible = False
        BTN_SimpanCookingStep.Visible = False
        PNL_Komponen.BackColor = Color.Silver
        DGV_Komponen.Visible = True

        With DGV_Komponen
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(CreateTextColumn("BM_Nomor", "No", frozen:=True, autoSizeMode:="allcells", alignment:="center", minWidth:=10))
            .Columns.Add(CreateTextColumn("BM_Kode_Barang", "Kode Barang", frozen:=True, autoSizeMode:="allcells", minWidth:=100))
            .Columns.Add(CreateTextColumn("BM_Nama_Barang", "Deskripsi", minWidth:=200, autoSizeMode:="fill"))
            .Columns.Add(CreateTextColumn("BM_Jumlah", "Jumlah (Kg)", autoSizeMode:="allcells", format:="N2"))
            .Columns.Add(CreateTextColumn("BM_Persentase", "Persentase (%)", autoSizeMode:="allcells", format:="N2"))
            .Columns.Add(CreateTextColumn("BM_Harga", "Harga", autoSizeMode:="allcells", format:="N2"))
            .Columns.Add(CreateTextColumn("BM_Est_HPP_Pcs", "Est. HPP Pcs", autoSizeMode:="allcells", format:="N2"))
        End With

        TB_Persen.Text = $""
        TB_HPP_Pcs.Text = $""

        If TB_NoFormula.Text.Trim = "" Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = $"
                    SELECT
                        c.Kode_Barang AS Kode_Bahan,
                        e.Nama AS Nama_Bahan,
                        c.Jumlah,
                        c.Persentase,
                        ISNULL(c.Est_HPP, 0) AS Est_HPP,
                        ISNULL(c.Est_HPP_Per_Pcs, 0) AS Est_HPP_Per_Pcs,
                        d.Satuan

                    FROM Emi_Transaksi_Formulator b 

                    INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                        AND b.No_Faktur = c.No_Faktur

                    INNER JOIN Barang d
                        ON b.Kode_Perusahaan = d.Kode_Perusahaan
                        AND b.Kode_Stock_Owner = d.Kode_Stock_Owner
                        AND b.Kode_Barang = d.Kode_Barang_Inq

                    INNER JOIN Barang e
                        ON c.Kode_Perusahaan = e.Kode_Perusahaan
                        AND c.Kode_Stock_Owner = e.Kode_Stock_Owner
                        AND c.Kode_Barang = e.Kode_Barang

                    WHERE
                        b.Status IS NULL
                        AND b.Kode_Perusahaan = '{KodePerusahaan}'
                        AND b.No_Faktur = '{TB_NoFormula.Text.Trim}'
            "

            Dim totalPersentase As Decimal = 0
            Dim totalEstHPPPcs As Decimal = 0

            Using Ds = BindingTrans(SQL)
                DGV_Komponen.Rows.Clear()

                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dim jumlah As Decimal = Val(.Rows(i).Item("Jumlah"))
                            Dim persentase As Decimal = Val(.Rows(i).Item("Persentase"))
                            Dim esthpp As Decimal = Val(.Rows(i).Item("Est_HPP"))
                            Dim esthpppcs As Decimal = Val(.Rows(i).Item("Est_HPP_Per_Pcs"))

                            totalPersentase += persentase
                            totalEstHPPPcs += esthpppcs

                            DGV_Komponen.Rows.Add()
                            DGV_Komponen.Rows(i).Cells(0).Value = i + 1
                            DGV_Komponen.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Bahan")
                            DGV_Komponen.Rows(i).Cells(2).Value = .Rows(i).Item("Nama_Bahan")
                            DGV_Komponen.Rows(i).Cells(3).Value = $"{jumlah:N2} Kg"
                            DGV_Komponen.Rows(i).Cells(4).Value = $"{persentase:N2} %"
                            DGV_Komponen.Rows(i).Cells(5).Value = $"{esthpp:N2}"
                            DGV_Komponen.Rows(i).Cells(6).Value = $"{esthpppcs:N2}"
                        Next
                    End If
                End With
            End Using

            TB_Persen.Text = $"{totalPersentase:N2}"
            TB_HPP_Pcs.Text = $"{totalEstHPPPcs:N2}"

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Gagal load bahan material formula {TB_NoFormula.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Komponen_Moisture_Content()
        TC_CookingStep.Visible = False
        BTN_SimpanCookingStep.Visible = False
        PNL_Komponen.BackColor = Color.Silver
        DGV_Komponen.Visible = True

        With DGV_Komponen
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(CreateTextColumn("MC_Kode_Analisa", "Kode Analisa", minWidth:=100, alignment:="center", frozen:=True))
            .Columns.Add(CreateTextColumn("MC_Jenis_Analisa", "Jenis Analisa", minWidth:=180, autoSizeMode:="fill"))
            .Columns.Add(CreateTextColumn("MC_Kategori", "Kategori", minWidth:=110, alignment:="center"))
            .Columns.Add(CreateTextColumn("MC_Kriteria", "Kriteria", minWidth:=110, alignment:="center"))
            .Columns.Add(CreateTextColumn("MC_Range_Awal", "Range Awal", format:="N2"))
            .Columns.Add(CreateTextColumn("MC_Range_Akhir", "Range Akhir", format:="N2"))
        End With

        If TB_NoFormula.Text.Trim = "" Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = $"
	             SELECT
		             b.Kode_Analisa,
		             b.Jenis_Analisa,
		             CASE
			             WHEN ISNULL(b.Flag_Perhitungan, 'T') = 'Y'
                 THEN 'Perhitungan'
			             ELSE 'Non Perhitungan'
		             END AS Kategori,
		             '-' AS Kriteria,
		             a.Range_Awal,
		             a.Range_Akhir
	             FROM N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
	             INNER JOIN N_EMI_LAB_Jenis_Analisa b
		             ON a.Id_Jenis_Analisa = b.id
	             WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
	             AND a.No_Formula = '{TB_NoFormula.Text.Trim}'
             
	             UNION ALL
             
	             SELECT
		             b.Kode_Analisa,
		             b.Jenis_Analisa,
		             'Non Perhitungan' AS Kategori,
		             c.Label_Keterangan AS Kriteria,
		             '' AS Range_Awal,
		             '' AS Range_Akhir
	             FROM N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
	             INNER JOIN N_EMI_LAB_Jenis_Analisa b
		             ON a.Id_Jenis_Analisa = b.id
	             INNER JOIN EMI_Switch c
		             ON a.Kode_Perusahaan = c.kode_perusahaan
		             AND a.nilai_kriteria = c.keterangan
	             WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
	             AND a.No_Formula = '{TB_NoFormula.Text.Trim}'
             
	             ORDER BY Kode_Analisa, Jenis_Analisa
             "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DGV_Komponen.Rows.Add()
                            DGV_Komponen.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Analisa")
                            DGV_Komponen.Rows(i).Cells(1).Value = .Rows(i).Item("Jenis_Analisa")
                            DGV_Komponen.Rows(i).Cells(2).Value = .Rows(i).Item("Kategori")
                            DGV_Komponen.Rows(i).Cells(3).Value = .Rows(i).Item("Kriteria")
                            DGV_Komponen.Rows(i).Cells(4).Value = .Rows(i).Item("Range_Awal")
                            DGV_Komponen.Rows(i).Cells(5).Value = .Rows(i).Item("Range_Akhir")
                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Gagal load moisture content untuk formula {TB_NoFormula.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Komponen_Cooking_Step()
        DGV_Komponen.Visible = False
        TC_CookingStep.Visible = True
        BTN_SimpanCookingStep.Visible = True
        PNL_Komponen.BackColor = Color.Gainsboro

        If TB_NoFormula.Text.Trim = "" Then
            Exit Sub
        End If

        RTB_TrialKitchen.Clear()
        RTB_TrialProduksi.Clear()

        RemoveRTBToolbar(RTB_TrialKitchen)
        RemoveRTBToolbar(RTB_TrialProduksi)

        Dim hasKitchenData As Boolean = False
        Dim hasProduksiData As Boolean = False

        Try
            OpenConn()

            ' =========================
            ' LOAD TRIAL KITCHEN
            ' =========================
            Dim SQL_TrialKitchen As String = $"
                SELECT TOP 1 Cooking_Step 
                FROM Emi_Transaksi_Formulator_Cooking_Steps
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                  AND No_Faktur = '{TB_NoFormula.Text.Trim}'
                  AND Status IS NULL
                  AND (
                        (
                            Flag_Trial_Kitchen = 'Y'
                            AND Flag_Trial_Produksi IS NULL
                        )
                        OR
                        (
                            Flag_Trial_Kitchen IS NULL
                            AND Flag_Trial_Produksi IS NULL
                        )
                      )
                ORDER BY Urut_Oto DESC
            "

            Using Dr = OpenTrans(SQL_TrialKitchen)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Cooking_Step")) <> "" Then
                        RTB_TrialKitchen.Rtf = Dr("Cooking_Step")
                        hasKitchenData = True
                    End If
                End If
            End Using

            If hasKitchenData Then
                RTB_TrialKitchen.ReadOnly = True
                RTB_TrialKitchen.Enabled = False
                RemoveRTBToolbar(RTB_TrialKitchen)
            Else
                RTB_TrialKitchen.ReadOnly = False
                RTB_TrialKitchen.Enabled = True
                InitRTBToolbar(RTB_TrialKitchen)
            End If

            ' =========================
            ' LOAD TRIAL PRODUKSI
            ' =========================
            Dim SQL_TrialProduksi As String = $"
                SELECT TOP 1 Cooking_Step 
                FROM Emi_Transaksi_Formulator_Cooking_Steps
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                  AND No_Faktur = '{TB_NoFormula.Text.Trim}'
                  AND Status IS NULL
                  AND Flag_Trial_Produksi = 'Y'
                  AND Flag_Trial_Kitchen IS NULL
                ORDER BY Urut_Oto DESC
            "

            Using Dr = OpenTrans(SQL_TrialProduksi)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Cooking_Step")) <> "" Then
                        RTB_TrialProduksi.Rtf = Dr("Cooking_Step")
                        hasProduksiData = True
                    End If
                End If
            End Using

            If hasProduksiData Then
                RTB_TrialProduksi.ReadOnly = True
                RTB_TrialProduksi.Enabled = False
                RemoveRTBToolbar(RTB_TrialProduksi)
            Else
                RTB_TrialProduksi.ReadOnly = False
                RTB_TrialProduksi.Enabled = True
                InitRTBToolbar(RTB_TrialProduksi)
            End If

            ' =========================
            ' BUTTON SIMPAN
            ' =========================
            BTN_SimpanCookingStep.Enabled =
            Not (hasKitchenData AndAlso hasProduksiData)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(
                $"Gagal load cooking step untuk formula {TB_NoFormula.Text.Trim}: " & ex.Message,
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Komponen_Daftar_Split()
        TC_CookingStep.Visible = False
        BTN_SimpanCookingStep.Visible = False
        PNL_Komponen.BackColor = Color.Silver
        DGV_Komponen.Visible = True

        With DGV_Komponen
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(CreateTextColumn("DS_No_Split", "No. Split", minWidth:=100, frozen:=True))
            .Columns.Add(CreateTextColumn("DS_Tanggal_Split", "Tanggal Split", minWidth:=100, alignment:="center"))
            .Columns.Add(CreateTextColumn("DS_Tanggal_Validasi", "Tanggal Validasi", minWidth:=100, alignment:="center"))
            .Columns.Add(CreateTextColumn("DS_Jumlah", "Jumlah", minWidth:=100, format:="N2"))
            .Columns.Add(CreateTextColumn("DS_Satuan", "Satuan", minWidth:=80, alignment:="center"))
            .Columns.Add(CreateTextColumn("DS_Status_Split", "Status Split", minWidth:=100, autoSizeMode:="fill", alignment:="center"))
            .Columns.Add(CreateButtonColumn("DS_Cetak", "Cetak", "Cetak Laporan", minWidth:=80))
        End With

        If TB_NoFormula.Text.Trim = "" Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = $"
                SELECT *
                    FROM (
                        SELECT TOP 1 
                            y.No_Transaksi,
                            FORMAT(y.Tanggal, 'dd MMM yyyy') AS Tanggal,
                            FORMAT(y.Tanggal_Validasi, 'dd MMM yyyy') AS Tanggal_Validasi,
                            y.Jumlah,
                            y.Satuan,
                            'Trial Kitchen' AS Status
                        FROM N_EMI_Transaksi_Trial_Order_Produksi x
                        JOIN N_EMI_Transaksi_Trial_Split_Production_Order y 
                            ON y.Kode_Perusahaan = x.Kode_Perusahaan 
                            AND y.No_PO = x.No_Faktur
                        WHERE x.Kode_Perusahaan = '{KodePerusahaan}' AND x.Kode_Formula = '{TB_NoFormula.Text.Trim}'
                        ORDER BY y.Tanggal DESC, y.Jam DESC
                    ) a

                    UNION ALL

                    SELECT *
                    FROM (
                        SELECT TOP 1 
                            y.No_Transaksi,
                            FORMAT(y.Tanggal, 'dd MMM yyyy') AS Tanggal,
                            FORMAT(y.Tanggal, 'dd MMM yyyy') AS Tanggal_Validasi,
                            y.Jumlah,
                            y.Satuan,
                            'Trial Produksi' AS Status
                        FROM EMI_Order_Produksi x
                        JOIN Emi_Split_Production_Order y 
                            ON y.Kode_Perusahaan = x.Kode_Perusahaan 
                            AND y.No_PO = x.No_Faktur
                        WHERE x.Flag_Trial_Produksi = 'Y' AND x.Kode_Perusahaan = '{KodePerusahaan}' AND x.Kode_Formula = '{TB_NoFormula.Text.Trim}'
                        ORDER BY y.Tanggal DESC, y.Jam DESC
                    ) b
            "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DGV_Komponen.Rows.Add()
                            DGV_Komponen.Rows(i).Cells(0).Value = .Rows(i).Item("No_Transaksi")
                            DGV_Komponen.Rows(i).Cells(1).Value = .Rows(i).Item("Tanggal")
                            DGV_Komponen.Rows(i).Cells(2).Value = .Rows(i).Item("Tanggal_Validasi")
                            DGV_Komponen.Rows(i).Cells(3).Value = Format(.Rows(i).Item("Jumlah"), "N2")
                            DGV_Komponen.Rows(i).Cells(4).Value = .Rows(i).Item("Satuan")
                            DGV_Komponen.Rows(i).Cells(5).Value = .Rows(i).Item("Status")
                            DGV_Komponen.Rows(i).Cells(6).Value = "Cetak Laporan"
                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Gagal load daftar split untuk formula {TB_NoFormula.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Tracking_Progress_Look_View()
        DGV_Detail_Pengujian.Rows.Clear()

        With DGV_Detail_Pengujian
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(CreateTextColumn("PLT_Nama_Mesin", "Nama Mesin", minWidth:=100, frozen:=True))
            .Columns.Add(CreateTextColumn("PLT_Jenis_Analisa", "Jenis Mesin", minWidth:=200, autoSizeMode:="fill"))
            .Columns.Add(CreateTextColumn("PLT_Standar_Min", "Standar Min", minWidth:=100, alignment:="center"))
            .Columns.Add(CreateTextColumn("PLT_Standar_Max", "Standar Max", minWidth:=100, alignment:="center"))
            .Columns.Add(CreateTextColumn("PLT_Avg_Hasil", "Avg Hasil", minWidth:=100, alignment:="center"))
            .Columns.Add(CreateTextColumn("PLT_Hasil_Uji", "Hasil", minWidth:=100, alignment:="center"))
        End With

        If TB_NoSplitKomponen.Text.Trim = "" Then
            Exit Sub
        End If

        If TB_SplitTypeKomponen.Text = "TRIAL_KITCHEN" Then
            Try
                OpenConn()

                SQL = $"
                    WITH cte AS (
                        SELECT
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN CAST(ROUND(AVG(b.Hasil), 2) AS VARCHAR(50))
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS keterangan_kriteria,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '0')
                                ELSE '-'
                            END AS Std_Min,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '0')
                                ELSE '-'
                            END AS Std_Max,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND ROUND(AVG(b.Hasil), 2)
                                         BETWEEN TRY_CAST(d.Range_Awal AS FLOAT)
                                         AND TRY_CAST(d.Range_Akhir AS FLOAT)
                                    THEN 'Lulus'

                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND (
                                            ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT)
                                            OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)
                                         )
                                    THEN 'Tidak Lulus'

                                ELSE
                                    CASE
                                        WHEN e.Flag_Layak = 'Y'
                                            THEN 'Lulus'
                                        ELSE 'Tidak Lulus'
                                    END
                            END AS Hasil_Uji,

                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'

                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_lock_view_split,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'

                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_analisa_lab_split

                        FROM N_LIMS_PO_Sampel a

                        JOIN N_EMI_LIMS_Uji_Sampel b
                            ON a.No_Sampel = b.No_Po_Sampel
                            AND b.Flag_Resampling IS NULL

                        JOIN N_EMI_LAB_Jenis_Analisa c
                            ON b.Id_Jenis_Analisa = c.id

                        LEFT JOIN N_EMI_LIMS_Uji_Pra_Final upf
                            ON b.No_Po_Sampel = upf.No_Sampel

                        LEFT JOIN N_EMI_LAB_Standar_Rentang d
                            ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan = 'Y'

                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e
                            ON e.Nilai_Kriteria = b.Hasil
                            AND b.Flag_Perhitungan IS NULL
                            AND e.Kode_Role = '{KODE_ROLE_FLM}'

                        JOIN EMI_Master_Mesin f
                            ON a.Kode_Perusahaan = f.Kode_Perusahaan
                            AND a.Id_Mesin = f.Id_Master_Mesin

                        WHERE
                            b.Flag_Approval = 'Y'
                            AND a.Status IS NULL
                            AND b.Flag_Selesai = 'Y'
                            AND b.Status IS NULL

                        GROUP BY
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,
                            d.Range_Awal,
                            d.Range_Akhir,
                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,
                            b.Flag_Perhitungan,
                            e.Keterangan_Kriteria,
                            e.Flag_Layak
                    )

                    SELECT *
                    FROM cte

                    WHERE
                        status_lock_view_split = 'DISETUJUI'
                        AND status_analisa_lab_split = 'DISETUJUI'
                        AND No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                        AND Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'

                    ORDER BY
                        Kode_Aktivitas_Lab
                "

                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        With DGV_Detail_Pengujian.Rows(DGV_Detail_Pengujian.Rows.Add)
                            .Cells(0).Value = Dr("Nama_Mesin")
                            .Cells(1).Value = Dr("Jenis_Analisa")
                            .Cells(2).Value = Dr("Std_Min")
                            .Cells(3).Value = Dr("Std_Max")
                            .Cells(4).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                            .Cells(4).Value = Dr("keterangan_kriteria")
                            .Cells(5).Value = Dr("Hasil_Uji")
                        End With
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show($"Gagal load tracking progress look view untuk formula {TB_NoFormula.Text.Trim} dengan No. Split {TB_NoSplitKomponen.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try
        ElseIf TB_SplitTypeKomponen.Text = "TRIAL_PRODUKSI" Or TB_SplitTypeKomponen.Text.Trim = "PRODUKSI" Then
            Dim splitTrialProduksi As String = ""
            If TB_SplitTypeKomponen.Text.Trim = "TRIAL_PRODUKSI" Then
                splitTrialProduksi = "AND a.Flag_Trial_Produksi = 'Y'"
            Else
                splitTrialProduksi = "AND a.Flag_Trial_Produksi IS NULL"
            End If

            Try
                OpenConn()

                SQL = $"
                    WITH cte AS (
                        SELECT
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN CAST(ROUND(AVG(b.Hasil), 2) AS VARCHAR(50))
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS keterangan_kriteria,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '0')
                                ELSE '-'
                            END AS Std_Min,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '0')
                                ELSE '-'
                            END AS Std_Max,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND ROUND(AVG(b.Hasil), 2)
                                         BETWEEN TRY_CAST(d.Range_Awal AS FLOAT)
                                         AND TRY_CAST(d.Range_Akhir AS FLOAT)
                                    THEN 'Lulus'

                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND (
                                            ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT)
                                            OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)
                                         )
                                    THEN 'Tidak Lulus'

                                ELSE
                                    CASE
                                        WHEN e.Flag_Layak = 'Y'
                                            THEN 'Lulus'
                                        ELSE 'Tidak Lulus'
                                    END
                            END AS Hasil_Uji,

                            b.Status,
                            b.Flag_Final,

                            CASE
   
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'

                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_lock_view_split,

                            CASE

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'

                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_analisa_lab_split

                        FROM N_EMI_LAB_PO_Sampel a

                        JOIN N_EMI_LAB_Uji_Sampel b
                            ON a.No_Sampel = b.No_Po_Sampel
                            AND b.Flag_Resampling IS NULL

                        JOIN N_EMI_LAB_Jenis_Analisa c
                            ON b.Id_Jenis_Analisa = c.id

                        LEFT JOIN N_EMI_LAB_Standar_Rentang d
                            ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan = 'Y'

                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e
                            ON e.Nilai_Kriteria = b.Hasil
                            AND b.Flag_Perhitungan IS NULL
                            AND e.Kode_Role = '{KODE_ROLE_LAB}'

                        JOIN EMI_Master_Mesin f
                            ON a.Kode_Perusahaan = f.Kode_Perusahaan
                            AND a.Id_Mesin = f.Id_Master_Mesin

                        WHERE
                            a.Status IS NULL
                            AND b.Status IS NULL
                            {splitTrialProduksi}

                        GROUP BY
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,
                            d.Range_Awal,
                            d.Range_Akhir,
                            b.Status,
                            b.Flag_Final,
                            b.Flag_Perhitungan,
                            e.Keterangan_Kriteria,
                            e.Flag_Layak
                    )

                    SELECT *
                    FROM cte

                    WHERE
                        status_lock_view_split = 'DISETUJUI'
                        AND status_analisa_lab_split = 'DISETUJUI'
                        AND No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                        AND Kode_Aktivitas_Lab = '{KODE_ANALISA_LOOK_VIEW}'

                    ORDER BY
                        Kode_Aktivitas_Lab
                "

                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        With DGV_Detail_Pengujian.Rows(DGV_Detail_Pengujian.Rows.Add)
                            .Cells(0).Value = Dr("Nama_Mesin")
                            .Cells(1).Value = Dr("Jenis_Analisa")
                            .Cells(2).Value = Dr("Std_Min")
                            .Cells(3).Value = Dr("Std_Max")
                            .Cells(4).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                            .Cells(4).Value = Dr("keterangan_kriteria")
                            .Cells(5).Value = Dr("Hasil_Uji")
                        End With
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show($"Gagal load tracking progress look view untuk formula {TB_NoFormula.Text.Trim} dengan No. Split {TB_NoSplitKomponen.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub Load_Tracking_Progress_Analisa_Lab()
        DGV_Detail_Pengujian.Rows.Clear()

        With DGV_Detail_Pengujian
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(CreateTextColumn("PLT_Nama_Mesin", "Nama Mesin", minWidth:=100, frozen:=True))
            .Columns.Add(CreateTextColumn("PLT_Jenis_Analisa", "Jenis Mesin", minWidth:=200, autoSizeMode:="fill"))
            .Columns.Add(CreateTextColumn("PLT_Standar_Min", "Standar Min", minWidth:=100, alignment:="center"))
            .Columns.Add(CreateTextColumn("PLT_Standar_Max", "Standar Max", minWidth:=100, alignment:="center"))
            .Columns.Add(CreateTextColumn("PLT_Avg_Hasil", "Avg Hasil", minWidth:=100, alignment:="center"))
            .Columns.Add(CreateTextColumn("PLT_Hasil_Uji", "Hasil", minWidth:=100, alignment:="center"))
        End With

        If TB_NoSplitKomponen.Text.Trim = "" Then
            Exit Sub
        End If

        If TB_SplitTypeKomponen.Text = "TRIAL_KITCHEN" Then
            Try
                OpenConn()

                SQL = $"
                    WITH cte AS (
                        SELECT
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN CAST(ROUND(AVG(b.Hasil), 2) AS VARCHAR(50))
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS keterangan_kriteria,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '-')
                                ELSE '-'
                            END AS Std_Min,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '-')
                                ELSE '-'
                            END AS Std_Max,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND ROUND(AVG(b.Hasil), 2)
                                         BETWEEN TRY_CAST(d.Range_Awal AS FLOAT)
                                         AND TRY_CAST(d.Range_Akhir AS FLOAT)
                                    THEN 'Lulus'

                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND (
                                            ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT)
                                            OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)
                                         )
                                    THEN 'Tidak Lulus'

                                ELSE
                                    CASE
                                        WHEN e.Flag_Layak = 'Y'
                                            THEN 'Lulus'
                                        ELSE 'Tidak Lulus'
                                    END
                            END AS Hasil_Uji,

                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'

                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_lock_view_split,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'

                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_analisa_lab_split

                        FROM N_LIMS_PO_Sampel a

                        JOIN N_EMI_LIMS_Uji_Sampel b
                            ON a.No_Sampel = b.No_Po_Sampel
                            AND b.Flag_Resampling IS NULL

                        JOIN N_EMI_LAB_Jenis_Analisa c
                            ON b.Id_Jenis_Analisa = c.id

                        LEFT JOIN N_EMI_LIMS_Uji_Pra_Final upf
                            ON b.No_Po_Sampel = upf.No_Sampel

                        LEFT JOIN N_EMI_LAB_Standar_Rentang d
                            ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan = 'Y'
                            AND a.Kode_Barang = d.Kode_Barang

                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e
                            ON e.Nilai_Kriteria = b.Hasil
                            AND b.Flag_Perhitungan IS NULL
                            AND e.Kode_Role = '{KODE_ROLE_FLM}'

                        JOIN EMI_Master_Mesin f
                            ON a.Kode_Perusahaan = f.Kode_Perusahaan
                            AND a.Id_Mesin = f.Id_Master_Mesin

                        WHERE
                            b.Flag_Approval = 'Y'
                            AND a.Status IS NULL
                            AND b.Flag_Selesai = 'Y'
                            AND b.Status IS NULL

                        GROUP BY
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,
                            d.Range_Awal,
                            d.Range_Akhir,
                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,
                            b.Flag_Perhitungan,
                            e.Keterangan_Kriteria,
                            e.Flag_Layak
                    )

                    SELECT *
                    FROM cte

                    WHERE
                        status_lock_view_split = 'DISETUJUI'
                        AND status_analisa_lab_split = 'DISETUJUI'
                        AND No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                        AND Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'

                    ORDER BY
                        Kode_Aktivitas_Lab
                "

                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        With DGV_Detail_Pengujian.Rows(DGV_Detail_Pengujian.Rows.Add)
                            .Cells(0).Value = Dr("Nama_Mesin")
                            .Cells(1).Value = Dr("Jenis_Analisa")
                            .Cells(2).Value = Dr("Std_Min")
                            .Cells(3).Value = Dr("Std_Max")
                            .Cells(4).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                            .Cells(4).Value = Dr("keterangan_kriteria")
                            .Cells(5).Value = Dr("Hasil_Uji")
                        End With
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show($"Gagal load tracking progress look view untuk formula {TB_NoFormula.Text.Trim} dengan No. Split {TB_NoSplitKomponen.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try
        ElseIf TB_SplitTypeKomponen.Text = "TRIAL_PRODUKSI" Or TB_SplitTypeKomponen.Text.Trim = "PRODUKSI" Then
            Dim splitTrialProduksi As String = ""
            If TB_SplitTypeKomponen.Text.Trim = "TRIAL_PRODUKSI" Then
                splitTrialProduksi = "AND a.Flag_Trial_Produksi = 'Y'"
            Else
                splitTrialProduksi = "AND a.Flag_Trial_Produksi IS NULL"
            End If

            Try
                OpenConn()

                SQL = $"
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY parameter, no_sample) AS no,
                        x.*
                    FROM (
                        SELECT
                            d.Jenis_Analisa AS parameter,
                            a.No_Sampel AS no_sample,
                            b.Nama_Mesin AS mesin,
                            CASE
                                WHEN e.Keterangan_Kriteria IS NULL THEN '-'
                                ELSE e.Keterangan_Kriteria
                            END AS hasil,
                            '-' AS std_min,  
                            '-' AS std_max,  
                            CASE
                                WHEN e.Flag_Layak = 'Y' THEN 'Lulus'
                                ELSE 'Tidak Lulus'
                            END AS status              
                        FROM N_EMI_LAB_PO_Sampel a
                        JOIN EMI_Master_Mesin b 
                            ON a.Id_Mesin = b.Id_Master_Mesin
                        JOIN N_EMI_LAB_Uji_Sampel c 
                            ON c.No_Po_Sampel = a.No_Sampel
                            AND c.Flag_Resampling IS NULL
                        JOIN N_EMI_LAB_Jenis_Analisa d 
                            ON d.id = c.Id_Jenis_Analisa
                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e 
                            ON e.Nilai_Kriteria = c.Hasil
                            AND e.Kode_Role = '{KODE_ROLE_LAB}'

                        WHERE a.No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}' 
                            AND a.Flag_Trial_Produksi = 'Y' 
                            AND c.Flag_Perhitungan IS NULL
                            AND d.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                            AND c.Flag_Resampling IS NULL

                        UNION 

                        SELECT
                            d.Jenis_Analisa AS parameter,
                            a.No_Sampel AS no_sample,
                            b.Nama_Mesin AS mesin,
                            ISNULL(CAST(ROUND(AVG(c.Hasil), 2) AS VARCHAR(30)), '-') AS hasil,
                            ISNULL(CAST(e.Range_Awal AS VARCHAR(30)), '-') AS std_min,                        
                            ISNULL(CAST(e.Range_Akhir AS VARCHAR(30)), '-') AS std_max,

                            CASE
                                WHEN e.Id_Jenis_Analisa IS NULL
                                THEN 'Lulus'

                                WHEN d.Flag_Perhitungan = 'Y'
                                    AND ROUND(AVG(c.Hasil), 2)
                                        BETWEEN TRY_CAST(e.Range_Awal AS FLOAT)
                                        AND TRY_CAST(e.Range_Akhir AS FLOAT)
                                THEN 'Lulus'

                                WHEN d.Flag_Perhitungan = 'Y'
                                    AND (
                                            ROUND(AVG(c.Hasil), 2) < TRY_CAST(e.Range_Awal AS FLOAT)
                                        OR ROUND(AVG(c.Hasil), 2) > TRY_CAST(e.Range_Akhir AS FLOAT)
                                        )
                                THEN 'Tidak Lulus'

                                ELSE 'Tidak Lulus'
                            END AS status

                        FROM N_EMI_LAB_PO_Sampel a
                        JOIN EMI_Master_Mesin b 
                            ON a.Id_Mesin = b.Id_Master_Mesin
                        JOIN N_EMI_LAB_Uji_Sampel c 
                            ON c.No_Po_Sampel = a.No_Sampel
                            AND c.Flag_Resampling IS NULL
                        JOIN N_EMI_LAB_Jenis_Analisa d 
                            ON d.id = c.Id_Jenis_Analisa
                        LEFT JOIN N_EMI_LAB_Standar_Rentang e 
                            ON e.Id_Jenis_Analisa = c.Id_Jenis_Analisa
                            AND e.Kode_Barang = a.Kode_Barang
                            AND e.Kode_Role = '{KODE_ROLE_LAB}'
                        WHERE a.No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}' 
                            AND a.Flag_Trial_Produksi = 'Y' 
                            AND c.Flag_Perhitungan = 'Y'
                            AND d.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                            AND c.Flag_Resampling IS NULL

                        GROUP BY
                            d.Jenis_Analisa,
                            a.No_Sampel,
                            b.Nama_Mesin,
                            e.Range_Awal,
                            e.Range_Akhir,
                            d.Flag_Perhitungan,
                            e.Id_Jenis_Analisa
                    ) x
                "

                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        With DGV_Detail_Pengujian.Rows(DGV_Detail_Pengujian.Rows.Add)
                            .Cells(0).Value = Dr("mesin")
                            .Cells(1).Value = Dr("parameter")
                            .Cells(2).Value = Dr("std_min")
                            .Cells(3).Value = Dr("std_max")
                            .Cells(4).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                            .Cells(4).Value = Dr("hasil")
                            .Cells(5).Value = Dr("status")
                        End With
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show($"Gagal load tracking progress look view untuk formula {TB_NoFormula.Text.Trim} dengan No. Split {TB_NoSplitKomponen.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub Load_Tracking_Progress_Palatabilitas()
        If TB_SplitTypeKomponen.Text = "TRIAL_KITCHEN" Then
            With DGV_Detail_Pengujian
                .AutoGenerateColumns = False
                .Columns.Clear()
                .Columns.Add(CreateTextColumn("PLT_Nama_Mesin", "Nama Mesin", minWidth:=100, frozen:=True))
                .Columns.Add(CreateTextColumn("PLT_Jenis_Analisa", "Jenis Mesin", minWidth:=200, autoSizeMode:="fill"))
                .Columns.Add(CreateTextColumn("PLT_Standar_Min", "Standar Min", minWidth:=100, alignment:="center"))
                .Columns.Add(CreateTextColumn("PLT_Standar_Max", "Standar Max", minWidth:=100, alignment:="center"))
                .Columns.Add(CreateTextColumn("PLT_Avg_Hasil", "Avg Hasil", minWidth:=100, alignment:="center"))
                .Columns.Add(CreateTextColumn("PLT_Hasil_Uji", "Hasil", minWidth:=100, alignment:="center"))
            End With

            If TB_NoSplitKomponen.Text.Trim = "" Then
                Exit Sub
            End If

            Try
                OpenConn()

                SQL = $"
                    WITH cte AS (
                        SELECT
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN CAST(ROUND(AVG(b.Hasil), 2) AS VARCHAR(50))
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS keterangan_kriteria,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '0')
                                ELSE '-'
                            END AS Std_Min,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '0')
                                ELSE '-'
                            END AS Std_Max,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND ROUND(AVG(b.Hasil), 2)
                                         BETWEEN TRY_CAST(d.Range_Awal AS FLOAT)
                                         AND TRY_CAST(d.Range_Akhir AS FLOAT)
                                    THEN 'Lulus'

                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND (
                                            ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT)
                                            OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)
                                         )
                                    THEN 'Tidak Lulus'

                                ELSE
                                    CASE
                                        WHEN e.Flag_Layak = 'Y'
                                            THEN 'Lulus'
                                        ELSE 'Tidak Lulus'
                                    END
                            END AS Hasil_Uji,

                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_PALATABILITAS}'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_PALATABILITAS}'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_PALATABILITAS}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'

                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_lock_view_split,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_PALATABILITAS}'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_PALATABILITAS}'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_PALATABILITAS}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'

                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_analisa_lab_split

                        FROM N_LIMS_PO_Sampel a

                        JOIN N_EMI_LIMS_Uji_Sampel b
                            ON a.No_Sampel = b.No_Po_Sampel
                            AND b.Flag_Resampling IS NULL

                        JOIN N_EMI_LAB_Jenis_Analisa c
                            ON b.Id_Jenis_Analisa = c.id

                        LEFT JOIN N_EMI_LIMS_Uji_Pra_Final upf
                            ON b.No_Po_Sampel = upf.No_Sampel

                        LEFT JOIN N_EMI_LAB_Standar_Rentang d
                            ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan = 'Y'

                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e
                            ON e.Nilai_Kriteria = b.Hasil
                            AND b.Flag_Perhitungan IS NULL
                            AND e.Kode_Role = '{KODE_ROLE_FLM}'

                        JOIN EMI_Master_Mesin f
                            ON a.Kode_Perusahaan = f.Kode_Perusahaan
                            AND a.Id_Mesin = f.Id_Master_Mesin

                        WHERE
                            b.Flag_Approval = 'Y'
                            AND a.Status IS NULL
                            AND b.Flag_Selesai = 'Y'
                            AND b.Status IS NULL

                        GROUP BY
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,
                            d.Range_Awal,
                            d.Range_Akhir,
                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,
                            b.Flag_Perhitungan,
                            e.Keterangan_Kriteria,
                            e.Flag_Layak
                    )

                    SELECT *
                    FROM cte

                    WHERE
                        status_lock_view_split = 'DISETUJUI'
                        AND status_analisa_lab_split = 'DISETUJUI'
                        AND No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                        AND Kode_Aktivitas_Lab = '{KODE_ANALISA_PALATABILITAS}'

                    ORDER BY
                        Kode_Aktivitas_Lab
                "

                Using Dr = OpenTrans(SQL)
                    DGV_Detail_Pengujian.Rows.Clear()

                    Do While Dr.Read
                        With DGV_Detail_Pengujian.Rows(DGV_Detail_Pengujian.Rows.Add)
                            .Cells(0).Value = Dr("Nama_Mesin")
                            .Cells(1).Value = Dr("Jenis_Analisa")
                            .Cells(2).Value = Dr("Std_Min")
                            .Cells(3).Value = Dr("Std_Max")
                            .Cells(4).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                            .Cells(4).Value = Dr("keterangan_kriteria")
                            .Cells(5).Value = Dr("Hasil_Uji")
                        End With
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show($"Gagal load tracking progress palatabilitas untuk formula {TB_NoFormula.Text.Trim} dengan No. Split {TB_NoSplitKomponen.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try
        ElseIf TB_SplitTypeKomponen.Text = "TRIAL_PRODUKSI" Or TB_SplitTypeKomponen.Text = "PRODUKSI" Then
            DGV_Detail_Pengujian.Rows.Clear()
            DGV_Detail_Pengujian.Columns.Clear()

            Dim splitTrialProduksi As String = ""
            If TB_SplitTypeKomponen.Text.Trim = "TRIAL_PRODUKSI" Then
                splitTrialProduksi = "AND a.Flag_Trial_Produksi = 'Y'"
            Else
                splitTrialProduksi = "AND a.Flag_Trial_Produksi IS NULL"
            End If

            Try
                OpenConn()

                Dim SQL As String = $"
                    WITH CTE AS (
                        SELECT 
                            d.Jenis_Analisa AS Metric, 
                            a.No_Sampel, 
                            f.Nama_Mesin AS Mesin, 
                            e.Keterangan AS Header, 
                            FORMAT(c.Value_Parameter, '0.################') AS Header_Value,
                            b.No_Faktur,
                            e.Id_QC_Formula,
                            ISNULL(p.Nama_Pembanding, '-') AS Nama_Pembanding,
                            ROW_NUMBER() OVER (
                                PARTITION BY d.Jenis_Analisa, a.No_Sampel, f.Nama_Mesin, e.Keterangan, c.Value_Parameter, b.No_Faktur, p.Nama_Pembanding 
                                ORDER BY d.Jenis_Analisa, a.No_Sampel, f.Nama_Mesin, e.Keterangan, c.Value_Parameter, b.No_Faktur, p.Nama_Pembanding
                            ) AS rn
                        FROM N_EMI_LAB_PO_Sampel a
                        JOIN N_EMI_LAB_Uji_Sampel b 
                            ON b.Kode_Perusahaan = a.Kode_Perusahaan 
                            AND a.No_Sampel = b.No_Po_Sampel
                            AND b.Flag_Resampling IS NULL
                        LEFT JOIN N_EMI_LAB_Palatabilitas_Session s ON b.Id_Session = s.Id_Session AND b.Kode_Perusahaan = s.Kode_Perusahaan
                        LEFT JOIN N_EMI_LAB_Palatabilitas_Pembanding p
                            ON p.Kode_Perusahaan = b.Kode_Perusahaan
                            AND p.Id_Pembanding = b.Id_Pembanding
                        LEFT JOIN N_EMI_LAB_Uji_Sampel_Detail c 
                            ON c.Kode_Perusahaan = b.Kode_Perusahaan 
                            AND b.No_Faktur = c.No_Faktur_Uji_Sample
                        JOIN N_EMI_LAB_Jenis_Analisa d 
                            ON d.id = b.Id_Jenis_Analisa 
                            AND d.Kode_Aktivitas_Lab = '{KODE_ANALISA_PALATABILITAS}'
                        JOIN EMI_Quality_Control e 
                            ON e.Id_QC_Formula = c.Id_Quality_Control
                        JOIN EMI_Master_Mesin f 
                            ON f.Kode_Perusahaan = a.Kode_Perusahaan 
                            AND f.Id_Master_Mesin = a.Id_Mesin
                        WHERE 
                            a.No_Split_Po = '{TB_NoSplitKomponen.Text.Trim}'
                            {splitTrialProduksi}
                    )
                    SELECT Metric, No_Sampel, Mesin, Header, Header_Value, No_Faktur, Nama_Pembanding, Id_QC_Formula
                    FROM CTE
                    WHERE rn = 1
                    GROUP BY Metric, No_Sampel, Mesin, Header, Header_Value, No_Faktur, Nama_Pembanding, Id_QC_Formula
                    ORDER BY Nama_Pembanding, Metric, Id_QC_Formula
                "

                Dim list As New List(Of PalatRow)
                Dim headerSet As New HashSet(Of String)

                Using Dr = OpenTrans(SQL)

                    While Dr.Read()

                        Dim metric = Dr("Metric").ToString()
                        Dim noSample = Dr("No_Sampel").ToString()
                        Dim mesin = Dr("Mesin").ToString()
                        Dim header = Dr("Header").ToString()
                        Dim value = Dr("Header_Value").ToString()
                        Dim nofaktur = Dr("No_Faktur").ToString()
                        Dim namaPembanding = Dr("Nama_Pembanding").ToString()

                        headerSet.Add(header)

                        Dim key = metric & "||" & noSample & "||" & mesin & "||" & nofaktur & "||" & namaPembanding

                        Dim row = list.FirstOrDefault(Function(x) x.Metric & "||" & x.No_Sample & "||" & x.Mesin & "||" & x.No_Faktur & "||" & x.Nama_Pembanding = key)

                        If row Is Nothing Then
                            row = New PalatRow With {
                                .No_Faktur = nofaktur,
                                .Metric = metric,
                                .No_Sample = noSample,
                                .Mesin = mesin,
                                .Nama_Pembanding = namaPembanding,
                                .Headers = New Dictionary(Of String, String)
                            }

                            list.Add(row)
                        End If

                        If Not row.Headers.ContainsKey(header) Then
                            row.Headers(header) = value
                        End If

                    End While

                End Using

                CloseConn()

                DGV_Detail_Pengujian.Columns.Add(CreateTextColumn("No_Faktur", "No Faktur", minWidth:=100, frozen:=True))
                DGV_Detail_Pengujian.Columns.Add(CreateTextColumn("Metric", "Metric", minWidth:=150, frozen:=True))
                DGV_Detail_Pengujian.Columns.Add(CreateTextColumn("No_Sample", "No Sample", minWidth:=100, autoSizeMode:="fill"))
                DGV_Detail_Pengujian.Columns.Add(CreateTextColumn("Mesin", "Mesin", minWidth:=100))
                DGV_Detail_Pengujian.Columns.Add(CreateTextColumn("Nama_Pembanding", "Nama Kontrol", minWidth:=120))

                For Each h In headerSet
                    DGV_Detail_Pengujian.Columns.Add(h, h)
                Next

                For Each item In list
                    Dim idx = DGV_Detail_Pengujian.Rows.Add()
                    Dim r = DGV_Detail_Pengujian.Rows(idx)

                    r.Cells("No_Faktur").Value = item.No_Faktur
                    r.Cells("Metric").Value = item.Metric
                    r.Cells("No_Sample").Value = item.No_Sample
                    r.Cells("Mesin").Value = item.Mesin
                    r.Cells("Nama_Pembanding").Value = item.Nama_Pembanding

                    For Each h In headerSet

                        Dim val As String = "-"

                        If item.Headers.ContainsKey(h) Then
                            val = item.Headers(h)
                        End If

                        r.Cells(h).Value = If(General_Class.CekNULL(val) = "", "-", val)
                        r.Cells(h).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    Next
                Next
            Catch ex As Exception
                CloseConn()
                MessageBox.Show($"Gagal load tracking progress palatabilitas untuk formula {TB_NoFormula.Text.Trim} dengan No. Split {TB_NoSplitKomponen.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub Load_Laporan_Trial_Kitchen()
        Dim no_formula As String = TB_NoFormula.Text.Trim
        Dim no_split As String = TB_NoSplitKomponen.Text.Trim

        If no_formula = "" Then
            MessageBox.Show("Pilih No. Formula terlebih dahulu.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If no_split = "" Then
            MessageBox.Show("Pilih No. Split terlebih dahulu.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim bahan As New List(Of Dictionary(Of String, Object))
        Dim hpp As New Dictionary(Of String, Object)
        Dim namaFormula As String = ""
        Dim kategoriProduk As String = ""
        Dim tanggalUji As String = ""
        Dim tanggalValidasi As String = ""
        Dim ketFormula As String = ""
        Dim cookingStep As String = ""

        'Get data untuk laporan
        Try
            OpenConn()

            SQL = $"
                SELECT TOP 1 Cooking_Step 
                FROM Emi_Transaksi_Formulator_Cooking_Steps
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                  AND No_Faktur = '{no_formula}'
                  AND Status IS NULL
                  AND (
                        (
                            Flag_Trial_Kitchen = 'Y'
                            AND Flag_Trial_Produksi IS NULL
                        )
                        OR
                        (
                            Flag_Trial_Kitchen IS NULL
                            AND Flag_Trial_Produksi IS NULL
                        )
                      )
                ORDER BY Urut_Oto DESC
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    cookingStep = Dr("Cooking_Step").ToString()
                End If
            End Using

            SQL = $"SELECT Keterangan FROM N_EMI_Transaksi_Trial_Split_Production_Order WHERE No_Transaksi = '{no_split}'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    ketFormula = Dr("Keterangan").ToString()
                End If
            End Using

            SQL = $"SELECT TOP 1 a.kode_formula, a.nama_produk, FORMAT(b.tanggal, 'dd MMM yyyy') AS tanggal_uji, FORMAT(CONVERT(DATETIME, CONVERT(VARCHAR(10), c.tanggal_selesai_trial_kitchen, 120) + ' ' + c.jam_selesai_trial_kitchen, 120), 'dd MMMM yyyy, HH:mm') AS tanggal_validasi
                    FROM N_EMI_View_Laporan_Formula_Rpt a 
                    JOIN N_LIMS_PO_Sampel b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Split_Po
                    JOIN EMI_Transaksi_Formulator c ON c.Kode_Perusahaan = a.Kode_Perusahaan AND c.No_Faktur = a.Kode_Formula
                    WHERE a.No_Transaksi = '{no_split}'
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    namaFormula = Dr("kode_formula").ToString()
                    kategoriProduk = Dr("nama_produk").ToString()
                    tanggalUji = Dr("tanggal_uji").ToString()
                    tanggalValidasi = Dr("tanggal_validasi").ToString()
                End If
            End Using

            SQL = $"SELECT Nama_bahan, Jumlah, Persentase, Est_HPP, Est_HPP_Per_Pcs FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}' GROUP BY kode_formula, nama_produk, tanggal, No_Transaksi, Nama_bahan, Jumlah, Persentase, Est_HPP_Per_Pcs, Est_HPP"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    bahan.Add(New Dictionary(Of String, Object) From {
                    {"Nama_bahan", Dr("Nama_bahan")},
                    {"Jumlah", Dr("Jumlah")},
                    {"Persentase", Dr("Persentase")},
                    {"Est_HPP_Per_Pcs", Dr("Est_HPP_Per_Pcs")},
                    {"Est_HPP", Dr("Est_HPP")}
                })
                Loop
            End Using

            SQL = $"SELECT SUM(Est_HPP_Per_Pcs) AS hpp_bahan_baku, HPP_Packaging, HPP_produksi, 'Per ' + satuan AS satuan FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}' GROUP BY kode_formula, nama_produk, tanggal, No_Transaksi, satuan, HPP_Produksi, HPP_Packaging"
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    hpp("hpp_bahan_baku") = Dr("hpp_bahan_baku")
                    hpp("hpp_packaging") = Dr("hpp_packaging")
                    hpp("hpp_produksi") = Dr("hpp_produksi")
                    hpp("satuan") = Dr("satuan")
                End If
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Gagal mendapatkan data laporan dengan No. Split {no_split}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

        'Hit API untuk mendapatkan file PDF laporan
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim payload As New Dictionary(Of String, Object) From {
                {"no_split", no_split},
                {"nama_formula", namaFormula},
                {"kategori_produk", kategoriProduk},
                {"tanggal_uji", tanggalUji},
                {"tanggal_validasi", tanggalValidasi},
                {"keterangan", ketFormula},
                {"cooking_step", cookingStep},
                {"bahan", bahan},
                {"hpp", hpp}
            }

            Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
            Dim headers As New Dictionary(Of String, String) From {{"X-Signature", GenerateHmac(json, Secret_Api_Laporan_Formulator)}}
            Dim pdfStream As MemoryStream = Helper_API.CallAPIFile(Url_Api_Laporan_Formulator, "POST", payload, headers)

            If pdfStream Is Nothing OrElse pdfStream.Length = 0 Then
                Throw New Exception("PDF stream kosong")
            End If

            Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

            Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
                pdfStream.CopyTo(file)
            End Using

            Dim frm As New N_EMI_PDF_Viewer(tempPath, "Laporan Formula Trial Kitchen")
            frm.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show($"Gagal mendapatkan laporan dengan No. Split {no_split}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Laporan_Trial_Produksi()
        Dim no_formula As String = TB_NoFormula.Text.Trim
        Dim no_split As String = TB_NoSplitKomponen.Text.Trim

        If no_formula = "" Then
            MessageBox.Show("Pilih No. Formula terlebih dahulu.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If no_split = "" Then
            MessageBox.Show("Pilih No. Split terlebih dahulu.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim namaFormula As String = ""
        Dim ketFormula As String = ""
        Dim kategoriProduk As String = ""
        Dim tanggalUji As String = ""
        Dim tanggalValidasi As String = ""
        Dim cookingStep As String = ""
        Dim bahan As New List(Of Dictionary(Of String, Object))
        Dim hpp As New Dictionary(Of String, Object)

        'Get data untuk laporan
        Try
            OpenConn()

            SQL = $"
                SELECT TOP 1 Cooking_Step 
                FROM Emi_Transaksi_Formulator_Cooking_Steps
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                  AND No_Faktur = '{no_formula}'
                  AND Status IS NULL
                  AND Flag_Trial_Produksi = 'Y'
                  AND Flag_Trial_Kitchen IS NULL
                ORDER BY Urut_Oto DESC
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    cookingStep = Dr("Cooking_Step").ToString()
                End If
            End Using

            SQL = $"SELECT Keterangan FROM Emi_Split_Production_Order WHERE No_Transaksi = '{no_split}'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    ketFormula = Dr("Keterangan").ToString()
                End If
            End Using

            SQL = $"select c.No_Faktur as kode_formula, b.Nama as nama_produk, format(a.Tanggal, 'dd MMM yyyy') as tanggal_uji, FORMAT(CONVERT(DATETIME, CONVERT(VARCHAR(10), c.Tanggal_Selesai_Trial_Produksi, 120) + ' ' + c.Jam_Selesai_Trial_Produksi, 120), 'dd MMMM yyyy, HH:mm') AS tanggal_validasi
				from N_EMI_LAB_PO_Sampel a
				cross apply (
					select top 1 *
					from Barang b
					where a.Kode_Perusahaan = b.Kode_Perusahaan
					and a.Kode_Barang = b.Kode_Barang
					order by b.Kode_Stock_Owner
				) b
				join Emi_Transaksi_Formulator c 
					on c.Kode_Perusahaan = a.Kode_Perusahaan 
					and c.Kode_Barang = b.Kode_Barang_Inq
					and c.Status is null
					and c.No_Faktur = '{no_formula}'
				where a.No_Split_Po = '{no_split}' and a.Flag_Trial_Produksi = 'Y'
			"
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    namaFormula = Dr("kode_formula").ToString()
                    kategoriProduk = Dr("nama_produk").ToString()
                    tanggalUji = Dr("tanggal_uji").ToString()
                    tanggalValidasi = Dr("tanggal_validasi").ToString()
                End If
            End Using

            SQL = $"
                WITH cte_bahan AS (
                    SELECT 
                        a.Kode_Barang,
                        a.Kode_Bahan,
                        a.Jumlah_Barang,

                        (
                            ISNULL(
                                (
                                    SELECT TOP (1)
                                        dbo.get_hpp(x.serial_number)
                                    FROM barang_sn x
                                    WHERE x.kode_barang = a.kode_bahan
                                      AND x.blok_sn IS NULL
                                      AND dbo.get_hpp(x.serial_number) <> 0
                                    ORDER BY x.Tgl_masuk DESC
                                ),
                                b.estimasi_harga
                            )
                        ) / NULLIF(a.Jumlah_Barang, 0) AS hpp

                    FROM Barang_Detail_Bahan_Penolong a

                    INNER JOIN Barang b
                        ON a.Kode_Bahan = b.Kode_Barang

                    GROUP BY a.Kode_Bahan, a.Kode_Barang, a.Jumlah_Barang, b.Estimasi_Harga
                ),

                cte_wc AS (
                    SELECT 
                        a.Kode_Perusahaan,
                        a.Kode_Jenis_Biaya_Produksi,

                        (
                            SELECT TOP (1) x.no_faktur
                            FROM Emi_Transaksi_Work_Center x
                            WHERE x.status IS NULL
                              AND x.Kode_Perusahaan = a.Kode_Perusahaan
                              AND x.jenis_biaya = a.Kode_Jenis_Biaya_Produksi
                            ORDER BY x.id DESC
                        ) AS Faktur_WC

                    FROM Emi_Jenis_Biaya_Produksi a
                ),

                cte_produksi AS (
                    SELECT 
                        c.Id_Routing,
                        c.id_work_center,
                        MAX(c.Nilai_Per_pcs) AS Nilai_Per_Kg

                    FROM cte_wc a

                    JOIN Emi_Transaksi_Work_Center b
                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                       AND a.Faktur_WC = b.No_Faktur

                    JOIN Emi_Transaksi_Work_Center_detail c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                       AND b.No_Faktur = c.No_Faktur

                    GROUP BY 
                        c.Id_Routing,
                        c.id_work_center,
                        a.Kode_Jenis_Biaya_Produksi
                )

                SELECT 
                    d.Satuan,

                    ISNULL((
                        SELECT SUM(x.hpp)
                        FROM cte_bahan x
                        WHERE x.Kode_Barang = b.Kode_Barang
                    ), 0) AS HPP_Packaging,

                    ISNULL((
                        SELECT SUM(x.Nilai_Per_Kg) / 1000 * d.Berat
                        FROM cte_produksi x
                        WHERE d.Id_Routing = x.Id_Routing
                    ), 0) AS HPP_Produksi,

                    ISNULL((
                        SELECT SUM(ISNULL(x.Est_HPP_Per_Pcs, 0))
                        FROM EMI_Transaksi_Formulator_Detail_Bahan x
                        WHERE x.Kode_Perusahaan = b.Kode_Perusahaan
                          AND x.No_Faktur = b.No_Faktur
                    ), 0) AS HPP_Bahan_Baku

                FROM Emi_Transaksi_Formulator b

                JOIN Barang d
                    ON b.Kode_Perusahaan = d.Kode_Perusahaan
                    AND b.Kode_Barang = d.Kode_Barang_inq
                    AND b.Kode_Stock_Owner = d.Kode_Stock_Owner

                WHERE b.No_Faktur = '{TB_NoFormula.Text.Trim}'

                GROUP BY 
                    b.No_Faktur,
                    b.Kode_Perusahaan,
                    b.Kode_Barang,
                    d.Nama,
                    d.Satuan,
                    d.Berat,
                    d.Id_Routing
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    hpp("hpp_bahan_baku") = Dr("HPP_Bahan_Baku")
                    hpp("hpp_packaging") = Dr("HPP_Packaging")
                    hpp("hpp_produksi") = Dr("HPP_Produksi")
                    hpp("satuan") = Dr("Satuan")
                End If
            End Using

            SQL = $"
                    SELECT
                        e.Nama AS Nama_Bahan,
                        c.Jumlah,
                        c.Persentase,
                        ISNULL(c.Est_HPP, 0) AS Est_HPP,
                        ISNULL(c.Est_HPP_Per_Pcs, 0) AS Est_HPP_Per_Pcs

                    FROM Emi_Transaksi_Formulator b 

                    INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                        AND b.No_Faktur = c.No_Faktur

                    INNER JOIN Barang d
                        ON b.Kode_Perusahaan = d.Kode_Perusahaan
                        AND b.Kode_Stock_Owner = d.Kode_Stock_Owner
                        AND b.Kode_Barang = d.Kode_Barang_Inq

                    INNER JOIN Barang e
                        ON c.Kode_Perusahaan = e.Kode_Perusahaan
                        AND c.Kode_Stock_Owner = e.Kode_Stock_Owner
                        AND c.Kode_Barang = e.Kode_Barang

                    WHERE
                        b.Status IS NULL
                        AND b.Kode_Perusahaan = '{KodePerusahaan}'
                        AND b.No_Faktur = '{TB_NoFormula.Text.Trim}'
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    bahan.Add(New Dictionary(Of String, Object) From {
                    {"Nama_bahan", Dr("Nama_bahan")},
                    {"Jumlah", Dr("Jumlah")},
                    {"Persentase", Dr("Persentase")},
                    {"Est_HPP_Per_Pcs", Dr("Est_HPP_Per_Pcs")},
                    {"Est_HPP", Dr("Est_HPP")}
                })
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Gagal mendapatkan data laporan dengan No. Split {no_split}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

        'Hit API untuk mendapatkan file PDF laporan
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim payload As New Dictionary(Of String, Object) From {
                {"no_split", no_split},
                {"nama_formula", namaFormula},
                {"kategori_produk", kategoriProduk},
                {"tanggal_uji", tanggalUji},
                {"tanggal_validasi", tanggalValidasi},
                {"keterangan", ketFormula},
                {"cooking_step", cookingStep},
                {"bahan", bahan},
                {"hpp", hpp}
            }

            Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
            Dim headers As New Dictionary(Of String, String) From {{"X-Signature", GenerateHmac(json, Secret_Api_Laporan_Formulator)}}
            Dim pdfStream As MemoryStream = Helper_API.CallAPIFile(Url_Api_Laporan_Formulator_Trial_Produksi, "POST", payload, headers)

            If pdfStream Is Nothing OrElse pdfStream.Length = 0 Then
                Throw New Exception("PDF stream kosong")
            End If

            Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

            Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
                pdfStream.CopyTo(file)
            End Using

            Dim frm As New N_EMI_PDF_Viewer(tempPath, "Laporan Formula Trial Produksi")
            frm.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show($"Gagal mendapatkan laporan dengan No. Split {no_split}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub DGV_Formula_SelectionChanged(sender As Object, e As EventArgs) Handles DGV_Formula.SelectionChanged
        If DGV_Formula.CurrentRow Is Nothing Then Exit Sub
        If DGV_Formula.CurrentRow.Index < 0 Then Exit Sub

        TB_NoFormula.Text = DGV_Formula.CurrentRow.Cells(0).Value?.ToString()
        TB_NoSplitKomponen.Text = ""
        TB_SplitTypeKomponen.Text = ""

        TLP_TrackingProgress.Enabled = False
        Reset_Active_Menu_Tracking_Progress()
        DGV_Detail_Pengujian.Columns.Clear()

        Try
            OpenConn()

            Dim SQL_DGV_Formula As String = $"
                    SELECT
                        a.No_Faktur,
                        tk.No_Transaksi AS No_Split_Trial_Kitchen,
                        tp.No_Transaksi AS No_Split_Trial_Produksi,
                        p.No_Transaksi AS No_Split_Produksi
                    FROM EMI_Transaksi_Formulator a
                    OUTER APPLY (
                        SELECT TOP 1 y.No_Transaksi
                        FROM N_EMI_Transaksi_Trial_Order_Produksi x
                        JOIN N_EMI_Transaksi_Trial_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                        WHERE x.Kode_Formula = a.No_Faktur
                        ORDER BY y.Tanggal DESC, y.Jam DESC
                    ) tk
                    OUTER APPLY (
                        SELECT TOP 1 y.No_Transaksi
                        FROM EMI_Order_Produksi x
                        JOIN Emi_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                        WHERE x.Kode_Formula = a.No_Faktur AND x.Flag_Trial_Produksi = 'Y'
                        ORDER BY y.Tanggal DESC, y.Jam DESC
                    ) tp
                    OUTER APPLY (
                        SELECT TOP 1 y.No_Transaksi
                        FROM EMI_Order_Produksi x
                        JOIN Emi_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                        WHERE x.Kode_Formula = a.No_Faktur AND x.Flag_Trial_Produksi IS NULL
                        ORDER BY y.Tanggal DESC, y.Jam DESC
                    ) p
                    WHERE a.No_Faktur = '{TB_NoFormula.Text.Trim}'
                "

            Using Dr = OpenTrans(SQL_DGV_Formula)
                If Dr.Read() Then
                    Dim noSplitProduksi As String = If(General_Class.CekNULL(Dr("No_Split_Produksi")) = "", "", Dr("No_Split_Produksi").ToString())
                    Dim noSplitTrialProduksi As String = If(General_Class.CekNULL(Dr("NO_Split_Trial_Produksi")) = "", "", Dr("NO_Split_Trial_Produksi").ToString())
                    Dim noSplitTrialKitchen As String = If(General_Class.CekNULL(Dr("No_Split_Trial_Kitchen")) = "", "", Dr("No_Split_Trial_Kitchen").ToString())

                    If noSplitProduksi <> "" Then
                        TB_NoSplitFormula.Text = noSplitProduksi
                        TB_SplitType.Text = "PRODUKSI"
                    ElseIf noSplitTrialProduksi <> "" Then
                        TB_NoSplitFormula.Text = noSplitTrialProduksi
                        TB_SplitType.Text = "TRIAL_PRODUKSI"
                    ElseIf noSplitTrialKitchen <> "" Then
                        TB_NoSplitFormula.Text = noSplitTrialKitchen
                        TB_SplitType.Text = "TRIAL_KITCHEN"
                    Else
                        TB_NoSplitFormula.Text = ""
                        TB_SplitType.Text = ""
                    End If
                Else
                    TB_NoSplitFormula.Text = ""
                    TB_SplitType.Text = ""
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal load no split: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

        If ActiveMenuKomponen IsNot Nothing Then
            If ActiveMenuKomponen.Text = "HPP Sementara" Then
                Load_Komponen_HPP_Sementara()
            ElseIf ActiveMenuKomponen.Text = "Bahan Material" Then
                Load_Komponen_Bahan_Material()
            ElseIf ActiveMenuKomponen.Text = "Moisture Content" Then
                Load_Komponen_Moisture_Content()
            ElseIf ActiveMenuKomponen.Text = "Cooking Step" Then
                Load_Komponen_Cooking_Step()
            ElseIf ActiveMenuKomponen.Text = "Daftar Split" Then
                Load_Komponen_Daftar_Split()
            End If
        Else
            Dim firstMenu As Label = TryCast(TLP_Komponen.GetControlFromPosition(0, 0), Label)

            If firstMenu IsNot Nothing Then
                Set_Active_Menu_Komponen(firstMenu)
                If firstMenu.Text = "HPP Sementara" Then
                    Load_Komponen_HPP_Sementara()
                ElseIf firstMenu.Text = "Bahan Material" Then
                    Load_Komponen_Bahan_Material()
                ElseIf firstMenu.Text = "Moisture Content" Then
                    Load_Komponen_Moisture_Content()
                ElseIf firstMenu.Text = "Cooking Step" Then
                    Load_Komponen_Cooking_Step()
                ElseIf firstMenu.Text = "Daftar Split" Then
                    Load_Komponen_Daftar_Split()
                End If
            End If
        End If
    End Sub

    Private Sub BTN_SimpanCookingStep_Click(sender As Object, e As EventArgs) Handles BTN_SimpanCookingStep.Click
        Dim rtb As RichTextBox = Nothing
        Dim flagTrialKitchen As String = "NULL"
        Dim flagTrialProduksi As String = "NULL"
        Dim jenis As String = ""

        If TC_CookingStep.SelectedTab Is TP_CS_TrialKitchen Then
            rtb = RTB_TrialKitchen
            flagTrialKitchen = "'Y'"
            jenis = "Trial Kitchen"
        ElseIf TC_CookingStep.SelectedTab Is TP_CS_TrialProduksi Then
            rtb = RTB_TrialProduksi
            flagTrialProduksi = "'Y'"
            jenis = "Trial Produksi"
        Else
            MessageBox.Show(
                "Tab cooking step tidak valid.",
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            Exit Sub
        End If

        If rtb Is Nothing OrElse
        String.IsNullOrWhiteSpace(rtb.Text.Trim) Then
            MessageBox.Show(
                "Cooking step tidak boleh kosong.",
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            rtb.Focus()
            Exit Sub
        End If

        get_jam()
        Try
            OpenConn()

            SQL = $"
                INSERT INTO Emi_Transaksi_Formulator_Cooking_Steps
                (
                    Kode_Perusahaan,
                    No_Faktur,
                    Status,
                    UserID,
                    Tanggal,
                    Jam,
                    Cooking_Step,
                    Flag_Trial_Kitchen,
                    Flag_Trial_Produksi
                )
                VALUES
                (
                    '{KodePerusahaan}',
                    '{TB_NoFormula.Text.Trim}',
                    NULL,
                    '{UserID}',
                    '{Format(tgl_skg, "yyyy-MM-dd")}',
                    '{Format(tgl_skg, "HH:mm:ss")}',
                    '{rtb.Rtf.Replace("'", "''")}',
                    {flagTrialKitchen},
                    {flagTrialProduksi}
                )
            "

            ExecuteTrans(SQL)

            CloseConn()

            Load_Komponen_Cooking_Step()

            MessageBox.Show(
                $"Cooking step {jenis} berhasil disimpan.",
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(
                "Gagal menyimpan cooking step: " & ex.Message,
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
        End Try
    End Sub

    Private Sub DGV_Komponen_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Komponen.CellClick
        If e.RowIndex < 0 Then Exit Sub

        If ActiveMenuKomponen.Text = "Daftar Split" Then
            TB_NoSplitKomponen.Text = DGV_Komponen.Rows(e.RowIndex).Cells(0).Value?.ToString()
            TB_SplitTypeKomponen.Text = DGV_Komponen.Rows(e.RowIndex).Cells(5).Value?.ToString().ToUpper().Replace(" ", "_")
            TLP_TrackingProgress.Enabled = True

            If DGV_Komponen.Columns(e.ColumnIndex).Name = "DS_Cetak" Then
                If TB_SplitTypeKomponen.Text = "TRIAL_KITCHEN" Then
                    Load_Laporan_Trial_Kitchen()
                ElseIf TB_SplitTypeKomponen.Text = "TRIAL_PRODUKSI" Then
                    Load_Laporan_Trial_Produksi()
                End If
            Else
                If ActiveMenuTrackingProgress IsNot Nothing Then
                    If ActiveMenuTrackingProgress.Text = "Look View" Then
                        Load_Tracking_Progress_Look_View()
                    ElseIf ActiveMenuTrackingProgress.Text = "Analisa Lab" Then
                        Load_Tracking_Progress_Analisa_Lab()
                    ElseIf ActiveMenuTrackingProgress.Text = "Palatabilitas" Then
                        Load_Tracking_Progress_Palatabilitas()
                    End If
                Else
                    Dim firstMenu As Label = TryCast(TLP_TrackingProgress.GetControlFromPosition(0, 0), Label)

                    If firstMenu IsNot Nothing Then
                        Set_Active_Menu_Tracking_Progress(firstMenu)
                        Load_Tracking_Progress_Look_View()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Get_Data_DGV_Parent(ByVal index As Integer)
        DgvParent_NoFormula = DGV_Formula.Rows(index).Cells(CellParent_NoFormula).Value
        DgvParent_TanggalFormula = DGV_Formula.Rows(index).Cells(CellParent_TanggalFormula).Value
        DgvParent_KdBarang = DGV_Formula.Rows(index).Cells(CellParent_KdBarang).Value
        DgvParent_NmBarang = DGV_Formula.Rows(index).Cells(CellParent_NmBarang).Value
        DgvParent_HPPMin = DGV_Formula.Rows(index).Cells(CellParent_HPPMin).Value
        DgvParent_HPPMax = DGV_Formula.Rows(index).Cells(CellParent_HPPMax).Value
        DgvParent_Jumlah = DGV_Formula.Rows(index).Cells(CellParent_Jumlah).Value
        DgvParent_Satuan = DGV_Formula.Rows(index).Cells(CellParent_Satuan).Value
        DgvParent_JenisFormula = DGV_Formula.Rows(index).Cells(CellParent_JenisFormula).Value
        DgvParent_PosisiBinding = DGV_Formula.Rows(index).Cells(CellParent_PosisiBinding).Value
        DgvParent_StatusFormula = DGV_Formula.Rows(index).Cells(CellParent_StatusFormula).Value
        DgvParent_Deskripsi = DGV_Formula.Rows(index).Cells(CellParent_Deskripsi).Value
        DgvParent_BtnValidasi = DGV_Formula.Rows(index).Cells(CellParent_BtnValidasi).Value
        DgvParent_StatusBypass = DGV_Formula.Rows(index).Cells(CellParent_StatusBypass).Value
    End Sub

    Private Function Cek_HPP_Formula(NoFormula As String) As Boolean
        Try
            OpenConn()

            SQL = $"
                WITH cte_bahan AS (
                    SELECT 
                        a.Kode_Barang,
                        a.Kode_Bahan,
                        a.Jumlah_Barang,

                        (
                            ISNULL(
                                (
                                    SELECT TOP (1)
                                        dbo.get_hpp(x.serial_number)
                                    FROM barang_sn x
                                    WHERE x.kode_barang = a.kode_bahan
                                      AND x.blok_sn IS NULL
                                      AND dbo.get_hpp(x.serial_number) <> 0
                                    ORDER BY x.Tgl_masuk DESC
                                ),
                                b.estimasi_harga
                            )
                        ) / NULLIF(a.Jumlah_Barang, 0) AS hpp

                    FROM Barang_Detail_Bahan_Penolong a

                    INNER JOIN Barang b
                        ON a.Kode_Bahan = b.Kode_Barang

                    GROUP BY a.Kode_Bahan, a.Kode_Barang, a.Jumlah_Barang, b.Estimasi_Harga
                ),

                cte_wc AS (
                    SELECT 
                        a.Kode_Perusahaan,
                        a.Id_Jenis_Biaya_Produksi,
                        a.Kode_Jenis_Biaya_Produksi,

                        (
                            SELECT TOP (1) x.No_Faktur
                            FROM Emi_Transaksi_Work_Center x
                            WHERE x.Status IS NULL
                              AND x.Kode_Perusahaan = a.Kode_Perusahaan
                              AND x.Jenis_Biaya = a.Kode_Jenis_Biaya_Produksi
                            ORDER BY x.Id DESC
                        ) AS Faktur_WC

                    FROM Emi_Jenis_Biaya_Produksi a
                ),

                cte_produksi AS (
                    SELECT 
                        c.Id_Routing,
                        a.Kode_Jenis_Biaya_Produksi,
                        c.Id_Work_Center,
                        MAX(c.Nilai_Per_Pcs) AS Nilai_Per_Kg

                    FROM cte_wc a

                    JOIN Emi_Transaksi_Work_Center b
                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                       AND a.Faktur_WC = b.No_Faktur

                    JOIN Emi_Transaksi_Work_Center_Detail c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                       AND b.No_Faktur = c.No_Faktur

                    GROUP BY 
                        c.Id_Routing,
                        a.Kode_Jenis_Biaya_Produksi,
                        c.Id_Work_Center
                )

                SELECT 
                    SUM(ISNULL(c.Est_HPP_Per_Pcs, 0)) AS HPP_Bahan_Baku,

                    ISNULL((
                        SELECT SUM(x.hpp)
                        FROM cte_bahan x
                        WHERE x.Kode_Barang = b.Kode_Barang
                    ), 0) AS HPP_Packaging,

                    ISNULL((
                        SELECT SUM(x.Nilai_Per_Kg) / 1000 * d.Berat
                        FROM cte_produksi x
                        WHERE d.Id_Routing = x.Id_Routing
                    ), 0) AS HPP_Produksi

                FROM Emi_Transaksi_Formulator b

                INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c
                    ON b.Kode_Perusahaan = c.Kode_Perusahaan
                   AND b.No_Faktur = c.No_Faktur

                INNER JOIN Barang d
                    ON b.Kode_Perusahaan = d.Kode_Perusahaan
                   AND b.Kode_Stock_Owner = d.Kode_Stock_Owner
                   AND b.Kode_Barang = d.Kode_Barang_inq

                WHERE 
                    b.No_Faktur = '{NoFormula}'

                GROUP BY 
                    b.No_Faktur,
                    d.Nama,
                    b.Tanggal,
                    d.Satuan,
                    d.Berat,
                    d.Id_Routing,
                    b.Kode_Barang
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then

                    Dim hppBahanBaku As Decimal = Val(Dr("HPP_Bahan_Baku"))
                    Dim hppPackaging As Decimal = Val(Dr("HPP_Packaging"))
                    Dim hppProduksi As Decimal = Val(Dr("HPP_Produksi"))

                    Dim listError As New List(Of String)

                    If hppBahanBaku <= 0 Then
                        listError.Add("HPP Bahan Baku")
                    End If

                    If hppPackaging <= 0 Then
                        listError.Add("HPP Packaging")
                    End If

                    If hppProduksi <= 0 Then
                        listError.Add("HPP Produksi")
                    End If

                    If listError.Count > 0 Then
                        CloseConn()

                        MessageBox.Show(
                            $"Tidak bisa validasi formula karena {String.Join(", ", listError)} bernilai 0.",
                            Judul,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        )
                        Return False
                    End If

                    CloseConn()
                    Return True
                Else
                    CloseConn()

                    MessageBox.Show(
                        $"Data formula {NoFormula} tidak ditemukan.",
                        Judul,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )
                    Return False
                End If
            End Using
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(
                $"Gagal cek HPP formula {NoFormula}: {ex.Message}",
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            Return False
        End Try
    End Function

    Private Sub DGV_Formula_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Formula.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        If e.ColumnIndex = CellParent_BtnValidasi Then
            Get_Data_DGV_Parent(e.RowIndex)

            Dim noformula As String = DgvParent_NoFormula.Trim
            If Not Cek_HPP_Formula(noformula) Then
                Exit Sub
            End If

            Dim currentStatus As String = DgvParent_StatusFormula.Trim

            Dim userPos As String = UserPosition.Trim

            If userPos = "HEADDEPT" Then
                If Status_HeadDept.Contains(currentStatus) Then

                    ShowFormHeadDept(DgvParent_NoFormula, DgvParent_StatusFormula)

                End If
            ElseIf userPos = "CLEVEL" Then
                If Status_BOD.Contains(currentStatus) Then
                    ShowFormBOD(DgvParent_NoFormula, DgvParent_StatusBypass, DgvParent_Deskripsi)
                End If
            End If
        End If
    End Sub

    Private Sub ShowFormHeadDept(ByVal NoFaktur As String, ByVal Status As String)
        N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.Kosong()
        N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.TxtFormulator_NoFaktur.Text = NoFaktur
        N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.StatusDariList = Status
        N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.TxtFormulator_NoFaktur_Leave(DGV_Formula, New EventArgs)
        N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.ShowDialog()

        DGV_Formula.Rows.Clear()

        Reset_Active_Menu_Komponen()
        LB_Persen.Visible = False
        TB_Persen.Visible = False
        LB_HPP_Pcs.Visible = False
        TB_HPP_Pcs.Visible = False

        DGV_Komponen.Height = 246

        RTB_TrialKitchen.Rtf = ""
        RTB_TrialProduksi.Rtf = ""
        DGV_Detail_Pengujian.Columns.Clear()
        DGV_Komponen.Columns.Clear()

        TC_CookingStep.SelectedTab = TP_CS_TrialKitchen

        Load_Formula()
    End Sub

    Private Sub ShowFormBOD(ByVal NoFaktur As String, statusBypass As String, keteranganBypass As String)
        Dim frm As New N_EMI_SD_Transaksi_Validasi_Formula_BOD With {
            .No_Faktur = NoFaktur,
            .StatusBypass = statusBypass,
            .KeteranganBypass = keteranganBypass
        }
        frm.ShowDialog()

        DGV_Formula.Rows.Clear()

        Reset_Active_Menu_Komponen()
        LB_Persen.Visible = False
        TB_Persen.Visible = False
        LB_HPP_Pcs.Visible = False
        TB_HPP_Pcs.Visible = False

        DGV_Komponen.Height = 246

        RTB_TrialKitchen.Rtf = ""
        RTB_TrialProduksi.Rtf = ""
        DGV_Detail_Pengujian.Columns.Clear()
        DGV_Komponen.Columns.Clear()

        TC_CookingStep.SelectedTab = TP_CS_TrialKitchen

        Load_Formula()
    End Sub

    Private Sub Cmb_Filter_Status_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter_Status.SelectedIndexChanged
        DGV_Formula.Rows.Clear()

        Reset_Active_Menu_Komponen()
        LB_Persen.Visible = False
        TB_Persen.Visible = False
        LB_HPP_Pcs.Visible = False
        TB_HPP_Pcs.Visible = False

        DGV_Komponen.Height = 246

        RTB_TrialKitchen.Rtf = ""
        RTB_TrialProduksi.Rtf = ""
        DGV_Detail_Pengujian.Columns.Clear()
        DGV_Komponen.Columns.Clear()

        TC_CookingStep.SelectedTab = TP_CS_TrialKitchen

        Load_Formula()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Reset_Active_Menu_Komponen()
        LB_Persen.Visible = False
        TB_Persen.Visible = False
        LB_HPP_Pcs.Visible = False
        TB_HPP_Pcs.Visible = False
        DGV_Komponen.Height = 246

        RTB_TrialKitchen.Rtf = ""
        RTB_TrialProduksi.Rtf = ""
        Reset_Active_Menu_Tracking_Progress()
        DGV_Komponen.Rows.Clear()
        DGV_Detail_Pengujian.Rows.Clear()
        Cb_Parameter_Tanggal.Checked = False
        Cb_Parameter_Lain.Checked = False
        Cmb_Parameter_Lain.SelectedIndex = -1
        Cmb_Paramater_Tanggal.SelectedIndex = -1
        Tb_Value.Clear()

        TC_CookingStep.SelectedTab = TP_CS_TrialKitchen

        Load_Formula()
    End Sub

    Private Sub Cb_Parameter_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Parameter_Tanggal.CheckedChanged
        If Cb_Parameter_Tanggal.Checked Then
            Cb_Transaksi_Hari_Ini.Checked = False
        Else
        End If
    End Sub

    Private Sub Cb_Transaksi_Hari_Ini_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Transaksi_Hari_Ini.CheckedChanged
        If Cb_Transaksi_Hari_Ini.Checked Then
            Cb_Parameter_Tanggal.Checked = False

            Reset_Active_Menu_Komponen()
            Reset_Active_Menu_Tracking_Progress()
            DGV_Komponen.Columns.Clear()
            DGV_Detail_Pengujian.Columns.Clear()
            RTB_TrialKitchen.Rtf = ""
            RTB_TrialProduksi.Rtf = ""
            LB_Persen.Visible = False
            TB_Persen.Visible = False
            LB_HPP_Pcs.Visible = False
            TB_HPP_Pcs.Visible = False

            DGV_Komponen.Height = 246
        Else
        End If

        Load_Formula()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Load_Formula()
    End Sub

    'Utils untuk membuat kolom DataGridView dengan cepat
    Private Function CreateTextColumn(
        name As String,
        header As String,
        Optional autoSizeMode As String = "allcells",
        Optional minWidth As Integer = 50,
        Optional frozen As Boolean = False,
        Optional readOnlyCol As Boolean = True,
        Optional visible As Boolean = True,
        Optional alignment As String = "left",
        Optional format As String = Nothing
    ) As DataGridViewTextBoxColumn
        Dim cellAlignment As DataGridViewContentAlignment =
        DataGridViewContentAlignment.MiddleLeft

        If Not String.IsNullOrWhiteSpace(format) AndAlso format.StartsWith("N", StringComparison.OrdinalIgnoreCase) Then
            alignment = "right"
        End If

        Select Case alignment.ToLower.Trim
            Case "center"
                cellAlignment = DataGridViewContentAlignment.MiddleCenter

            Case "right"
                cellAlignment = DataGridViewContentAlignment.MiddleRight

            Case Else
                cellAlignment = DataGridViewContentAlignment.MiddleLeft
        End Select

        Dim style As New DataGridViewCellStyle With {
            .Alignment = cellAlignment,
            .WrapMode = DataGridViewTriState.True
        }

        If Not String.IsNullOrWhiteSpace(format) Then
            style.Format = format
        End If

        Dim col As New DataGridViewTextBoxColumn With {
            .Name = name,
            .HeaderText = header,
            .MinimumWidth = minWidth,
            .Frozen = frozen,
            .ReadOnly = readOnlyCol,
            .Visible = visible,
            .SortMode = DataGridViewColumnSortMode.NotSortable,
            .DefaultCellStyle = style
        }

        Select Case autoSizeMode.ToLower.Trim

            Case "fill"
                If frozen Then
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                Else
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If

            Case Else
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        End Select

        Return col
    End Function

    'Utils untuk membuat kolom button di DataGridView dengan cepat
    Private Function CreateButtonColumn(
        name As String,
        header As String,
        text As String,
        Optional minWidth As Integer = 50,
        Optional frozen As Boolean = False,
        Optional visible As Boolean = True
    ) As DataGridViewButtonColumn

        Dim col As New DataGridViewButtonColumn With {
            .Name = name,
            .HeaderText = header,
            .Text = text,
            .UseColumnTextForButtonValue = True,
            .MinimumWidth = minWidth,
            .Frozen = frozen,
            .Visible = visible,
            .SortMode = DataGridViewColumnSortMode.NotSortable,
            .FlatStyle = FlatStyle.Flat,
            .DefaultCellStyle = New DataGridViewCellStyle With {
                .BackColor = Color.FromArgb(15, 86, 122),
                .ForeColor = Color.White,
                .SelectionBackColor = Color.FromArgb(15, 86, 122),
                .SelectionForeColor = Color.White,
                .Alignment = DataGridViewContentAlignment.MiddleCenter
            }
        }

        Return col
    End Function

    Private Sub InitRTBToolbar(rtb As RichTextBox)
        For Each ctrl As Control In rtb.Parent.Controls

            If TypeOf ctrl Is ToolStrip AndAlso
           ctrl.Tag IsNot Nothing AndAlso
           ctrl.Tag Is rtb Then
                Return
            End If

        Next

        Dim ts As New ToolStrip()

        ts.GripStyle = ToolStripGripStyle.Hidden
        ts.BackColor = Color.WhiteSmoke
        ts.Tag = rtb

        Dim tsBold As New ToolStripButton("B") With {
            .Font = New Font("Georgia", 10, FontStyle.Bold),
            .ToolTipText = "Bold (Ctrl+B)",
            .CheckOnClick = True,
            .AutoSize = False,
            .Width = 30,
            .Height = 26,
            .Tag = rtb
        }

        Dim tsItalic As New ToolStripButton("I") With {
            .Font = New Font("Georgia", 10, FontStyle.Italic),
            .ToolTipText = "Italic (Ctrl+I)",
            .CheckOnClick = True,
            .AutoSize = False,
            .Width = 30,
            .Height = 26,
            .Tag = rtb
        }

        Dim tsUnderline As New ToolStripButton("U") With {
            .Font = New Font("Georgia", 10, FontStyle.Underline),
            .ToolTipText = "Underline (Ctrl+U)",
            .CheckOnClick = True,
            .AutoSize = False,
            .Width = 30,
            .Height = 26,
            .Tag = rtb
        }

        Dim tsBullet As New ToolStripButton("• List") With {
            .ToolTipText = "Bullet List",
            .CheckOnClick = True,
            .AutoSize = True,
            .Height = 26,
            .Tag = rtb
        }

        Dim btnTab As New ToolStripButton("⇥ Tab") With {
            .ToolTipText = "Indent (Tab)",
            .AutoSize = True,
            .Height = 26,
            .Tag = rtb
        }

        AddHandler tsBold.Click,
        Sub()
            ToggleBold(rtb)
            rtb.Focus()
        End Sub

        AddHandler tsItalic.Click,
        Sub()
            ToggleItalic(rtb)
            rtb.Focus()
        End Sub

        AddHandler tsUnderline.Click,
        Sub()
            ToggleUnderline(rtb)
            rtb.Focus()
        End Sub

        AddHandler tsBullet.Click,
        Sub()
            ToggleBullet(rtb)
            rtb.Focus()
        End Sub

        AddHandler btnTab.Click,
        Sub()
            IndentRTB(rtb, True)
            rtb.Focus()
        End Sub

        ts.Items.AddRange({
            tsBold,
            tsItalic,
            tsUnderline,
            New ToolStripSeparator(),
            tsBullet,
            btnTab
        })

        Dim parent = rtb.Parent

        Dim rtbTop As Integer = rtb.Top
        Dim rtbLeft As Integer = rtb.Left
        Dim rtbWidth As Integer = rtb.Width
        Dim rtbHeight As Integer = rtb.Height

        Const TS_HEIGHT As Integer = 28

        ts.Location = New Point(rtbLeft, rtbTop - TS_HEIGHT)
        ts.Width = rtbWidth
        ts.Height = TS_HEIGHT

        rtb.Top = rtbTop
        rtb.Height = rtbHeight - TS_HEIGHT

        parent.Controls.Add(ts)

        ts.BringToFront()

        rtb.Dock = DockStyle.Bottom
    End Sub

    Private Sub RemoveRTBToolbar(rtb As RichTextBox)
        Dim parent = rtb.Parent
        Dim toRemove As New List(Of Control)

        For Each ctrl As Control In parent.Controls

            If TypeOf ctrl Is ToolStrip Then

                Dim ts = DirectCast(ctrl, ToolStrip)

                If ts.Tag IsNot Nothing AndAlso ts.Tag Is rtb Then
                    toRemove.Add(ts)
                End If

            End If

        Next

        For Each ts In toRemove
            parent.Controls.Remove(ts)
            ts.Dispose()
        Next

        rtb.Dock = DockStyle.Fill
    End Sub

    Private Sub UpdateRTBState(rtb As RichTextBox, hasData As Boolean)
        If hasData Then
            rtb.ReadOnly = True
            rtb.Enabled = False
            RemoveRTBToolbar(rtb)
        Else
            rtb.ReadOnly = False
            rtb.Enabled = True
            RemoveRTBToolbar(rtb)
            InitRTBToolbar(rtb)
        End If
    End Sub

    Private Function GetActiveRTB() As RichTextBox
        If TC_CookingStep.SelectedTab Is Nothing Then Return Nothing

        Select Case TC_CookingStep.SelectedTab.Name

            Case TP_CS_TrialKitchen.Name
                If RTB_TrialKitchen.Enabled Then
                    Return RTB_TrialKitchen
                Else
                    Return Nothing
                End If
            Case TP_CS_TrialProduksi.Name
                If RTB_TrialProduksi.Enabled Then
                    Return RTB_TrialProduksi
                Else
                    Return Nothing
                End If
        End Select

        Return Nothing
    End Function

    Private Sub TC_CookingStep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TC_CookingStep.SelectedIndexChanged
        _activeRTB = GetActiveRTB()
        UpdateToolbarState(_activeRTB)

        If _activeRTB Is Nothing OrElse Not _activeRTB.Enabled Then
            BTN_SimpanCookingStep.Enabled = False
        Else
            BTN_SimpanCookingStep.Enabled = True
        End If
    End Sub

    Private Sub CompareFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompareFormulaToolStripMenuItem.Click
        If DGV_Formula.CurrentRow Is Nothing Then Exit Sub

        Dim KodeBarang As String = DGV_Formula.CurrentRow.Cells(CellParent_KdBarang).Value.ToString()

        Dim frm As New N_EMI_Display_Compare_Formula With {
            .KodeBarang = KodeBarang
        }

        frm.ShowDialog()
    End Sub

    Private Sub RTB_KeyDown(sender As Object, e As KeyEventArgs) _
        Handles RTB_TrialKitchen.KeyDown,
                RTB_TrialProduksi.KeyDown

        Dim rtb As RichTextBox = DirectCast(sender, RichTextBox)

        _activeRTB = rtb

        If e.Control AndAlso e.KeyCode = Keys.B Then
            ToggleBold(rtb)
            e.SuppressKeyPress = True
            Return
        End If

        If e.Control AndAlso e.KeyCode = Keys.I Then
            ToggleItalic(rtb)
            e.SuppressKeyPress = True
            Return
        End If

        If e.Control AndAlso e.KeyCode = Keys.U Then
            ToggleUnderline(rtb)
            e.SuppressKeyPress = True
            Return
        End If

        If e.KeyCode = Keys.Tab AndAlso Not e.Shift Then
            IndentRTB(rtb, True)
            e.SuppressKeyPress = True
            Return
        End If

        If e.KeyCode = Keys.Back Then

            Dim lineIndex = rtb.GetLineFromCharIndex(rtb.SelectionStart)
            Dim lineStart = rtb.GetFirstCharIndexFromLine(lineIndex)
            Dim cursorPos = rtb.SelectionStart

            If cursorPos = lineStart AndAlso rtb.SelectionIndent > 0 Then
                IndentRTB(rtb, False)
                e.SuppressKeyPress = True
                Return
            End If

        End If

    End Sub

    Private Sub RTB_SelectionChanged(sender As Object, e As EventArgs) _
        Handles RTB_TrialKitchen.SelectionChanged,
                RTB_TrialProduksi.SelectionChanged

        Dim rtb As RichTextBox = DirectCast(sender, RichTextBox)

        _activeRTB = rtb

        UpdateToolbarState(rtb)

    End Sub

    Private Sub UpdateToolbarState(rtb As RichTextBox)
        If rtb Is Nothing Then Return
        If rtb.SelectionFont Is Nothing Then Return

        If _tsBold IsNot Nothing Then
            _tsBold.Checked = rtb.SelectionFont.Bold
        End If

        If _tsItalic IsNot Nothing Then
            _tsItalic.Checked = rtb.SelectionFont.Italic
        End If

        If _tsUnderline IsNot Nothing Then
            _tsUnderline.Checked = rtb.SelectionFont.Underline
        End If

        If _tsBullet IsNot Nothing Then

            Dim lineIdx As Integer =
                rtb.GetLineFromCharIndex(rtb.SelectionStart)

            If lineIdx >= 0 AndAlso lineIdx < rtb.Lines.Length Then
                _tsBullet.Checked =
                    rtb.Lines(lineIdx).StartsWith("• ")
            End If

        End If

    End Sub

    Private Sub TS_Bold_Click(sender As Object, e As EventArgs) Handles _tsBold.Click
        ToggleBold(GetActiveRTB())
    End Sub

    Private Sub TS_Italic_Click(sender As Object, e As EventArgs) Handles _tsItalic.Click
        ToggleItalic(GetActiveRTB())
    End Sub

    Private Sub TS_Underline_Click(sender As Object, e As EventArgs) Handles _tsUnderline.Click
        ToggleUnderline(GetActiveRTB())
    End Sub

    Private Sub TS_Bullet_Click(sender As Object, e As EventArgs) Handles _tsBullet.Click
        ToggleBullet(GetActiveRTB())
    End Sub

    Private Sub ToggleBold(rtb As RichTextBox)

        If rtb Is Nothing Then Return
        If rtb.SelectionFont Is Nothing Then Return

        Dim s = rtb.SelectionFont.Style

        rtb.SelectionFont = New Font(
            rtb.SelectionFont,
            If(
                (s And FontStyle.Bold) = FontStyle.Bold,
                s And Not FontStyle.Bold,
                s Or FontStyle.Bold
            )
        )

        rtb.Focus()

    End Sub

    Private Sub ToggleItalic(rtb As RichTextBox)

        If rtb Is Nothing Then Return
        If rtb.SelectionFont Is Nothing Then Return

        Dim s = rtb.SelectionFont.Style

        rtb.SelectionFont = New Font(
            rtb.SelectionFont,
            If(
                (s And FontStyle.Italic) = FontStyle.Italic,
                s And Not FontStyle.Italic,
                s Or FontStyle.Italic
            )
        )

        rtb.Focus()

    End Sub

    Private Sub ToggleUnderline(rtb As RichTextBox)

        If rtb Is Nothing Then Return
        If rtb.SelectionFont Is Nothing Then Return

        Dim s = rtb.SelectionFont.Style

        rtb.SelectionFont = New Font(
            rtb.SelectionFont,
            If(
                (s And FontStyle.Underline) = FontStyle.Underline,
                s And Not FontStyle.Underline,
                s Or FontStyle.Underline
            )
        )

        rtb.Focus()

    End Sub

    Private Sub IndentRTB(rtb As RichTextBox, addIndent As Boolean)

        If rtb Is Nothing Then Return

        Const INDENT As Integer = 40

        Dim firstLine =
            rtb.GetLineFromCharIndex(rtb.SelectionStart)

        Dim lastLine =
            rtb.GetLineFromCharIndex(
                rtb.SelectionStart + rtb.SelectionLength
            )

        Dim savedStart = rtb.SelectionStart
        Dim savedLen = rtb.SelectionLength

        For i = firstLine To lastLine

            rtb.SelectionStart =
                rtb.GetFirstCharIndexFromLine(i)

            rtb.SelectionLength = 0

            rtb.SelectionIndent =
                Math.Max(
                    0,
                    rtb.SelectionIndent +
                    If(addIndent, INDENT, -INDENT)
                )

        Next

        rtb.SelectionStart = savedStart
        rtb.SelectionLength = savedLen

        rtb.Focus()

    End Sub

    Private Sub ToggleBullet(rtb As RichTextBox)

        If rtb Is Nothing Then Return

        Dim selStart As Integer = rtb.SelectionStart
        Dim selLen As Integer = rtb.SelectionLength

        Dim firstLine As Integer =
            rtb.GetLineFromCharIndex(selStart)

        Dim lastLine As Integer =
            rtb.GetLineFromCharIndex(selStart + selLen)

        Dim firstLineText As String = ""

        If firstLine < rtb.Lines.Length Then
            firstLineText = rtb.Lines(firstLine)
        End If

        Dim sudahBullet As Boolean =
            firstLineText.StartsWith("• ")

        rtb.SuspendLayout()

        For i As Integer = firstLine To lastLine

            Dim lineStart As Integer =
                rtb.GetFirstCharIndexFromLine(i)

            If lineStart < 0 Then Continue For

            Dim lineText As String = ""

            If i < rtb.Lines.Length Then
                lineText = rtb.Lines(i)
            End If

            If sudahBullet Then

                If lineText.StartsWith("• ") Then

                    rtb.SelectionStart = lineStart
                    rtb.SelectionLength = 2
                    rtb.SelectedText = ""

                    rtb.SelectionStart =
                        rtb.GetFirstCharIndexFromLine(i)

                    rtb.SelectionLength = 0
                    rtb.SelectionIndent = 0
                    rtb.SelectionHangingIndent = 0

                End If

            Else

                rtb.SelectionStart = lineStart
                rtb.SelectionLength = 0

                Dim f As Font =
                    If(rtb.SelectionFont, rtb.Font)

                rtb.SelectedText = "• "

                rtb.SelectionStart = lineStart
                rtb.SelectionLength = 2
                rtb.SelectionFont = f

                rtb.SelectionIndent = 10
                rtb.SelectionHangingIndent = 0

                rtb.SelectionStart = lineStart + 2
                rtb.SelectionLength = 0

            End If

        Next

        If firstLine <> lastLine Then

            Dim lastLineStart As Integer =
                rtb.GetFirstCharIndexFromLine(lastLine)

            If lastLineStart >= 0 AndAlso
               lastLine < rtb.Lines.Length Then

                rtb.SelectionStart =
                    lastLineStart +
                    rtb.Lines(lastLine).Length

                rtb.SelectionLength = 0

            End If

        End If

        rtb.ResumeLayout()

        If Not sudahBullet AndAlso firstLine = lastLine Then

            Dim pos As Integer =
                rtb.GetFirstCharIndexFromLine(firstLine)

            If pos >= 0 Then
                rtb.SelectionStart = pos + 2
                rtb.SelectionLength = 0
            End If

        End If

        If _tsBullet IsNot Nothing Then
            _tsBullet.Checked = Not sudahBullet
        End If

        rtb.Focus()
    End Sub

    Private Function GetRowColor(statusFormula As String, statusBypass As String) As Color
        If statusBypass.Trim.ToUpper <> "NORMAL" Then
            Return Color.SandyBrown
        End If

        Select Case statusFormula
            Case "Produksi",
                 "Menunggu Validasi BOD",
                 "Selesai Produksi Komersial",
                 "Proses Produksi Komersial"

                Return Color.LightGreen
            Case "Selesai Trial Produksi",
                 "Proses Trial Produksi"

                Return Color.LightBlue
            Case "Selesai Trial Kitchen",
                 "Proses Trial Kitchen"

                Return Color.LightYellow
            Case Else
                If statusFormula.ToLower.Contains("ditolak") Then
                    Return Color.LightCoral
                End If

                Return Color.White
        End Select
    End Function
End Class

Public Class PalatRow
    Public Property No_Faktur As String
    Public Property Metric As String
    Public Property No_Sample As String
    Public Property Mesin As String
    Public Property Headers As Dictionary(Of String, String)
    Public Property Nama_Pembanding As String
End Class