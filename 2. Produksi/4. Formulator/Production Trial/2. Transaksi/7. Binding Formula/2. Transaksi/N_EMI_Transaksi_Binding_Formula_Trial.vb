Public Class N_EMI_Transaksi_Binding_Formula_Trial
    Dim changeNotSaved As Boolean = False
    Dim selectedKodeBarang As String = ""
    Dim selectedFakturSusunan As String = ""
    Private lblValueMap As New Dictionary(Of String, Label)

    Private Sub N_EMI_Transaksi_Binding_Formula_Trial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GB_InformasiSusunanFormula.Visible = True
        GB_BuatSusunanFormula.Visible = False

        Load_DGV_Barang()

        With TLP_InformasiBarang
            .SuspendLayout()
            .AutoScroll = True
            .AutoSize = False
            .Dock = DockStyle.Fill
            .ColumnCount = 1
            .ColumnStyles.Clear()
            .ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
            .HorizontalScroll.Enabled = False
            .HorizontalScroll.Visible = False
            .ResumeLayout()
        End With

        Init_TLPInformasiBarang()
        AddField("Kode Barang")
        AddField("Nama Barang", maxLines:=2)
        AddField("No Faktur Susunan")
        AddField("No Formula")
        AddField("Hasil")
        AddField("Keterangan")
        AddField("Tanggal Validasi")
    End Sub

    Private Sub Load_DGV_Barang()
        DGV_Barang.Rows.Clear()
        DGV_SusunanFormula.Rows.Clear()
        DGV_DetailSusunanFormula.Rows.Clear()
        DGV_FormulaTersedia.Rows.Clear()
        DGV_FormulaTerbinding.Rows.Clear()

        selectedKodeBarang = ""
        selectedFakturSusunan = ""

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim Keyword As String = TxtCariBarang.Text.Trim

            SQL = $"
                WITH Data AS (
                    SELECT
                        a.Kode_Barang,
                        a.Kode_Barang_Inq,
                        a.Nama,
                        COUNT(b.No_Faktur) AS Total_Formula,
                        lb.No_Faktur,
                        CASE
                            WHEN lb.No_Faktur IS NULL THEN
                                'BELUM ADA BINDING'

                            WHEN lb.Flag_Validasi_Main = 'Y'
                                 AND EXISTS (
                                     SELECT 1
                                     FROM N_EMI_Transaksi_Formulator_Binding_Detail d
                                     INNER JOIN EMI_Transaksi_Formulator f
                                         ON f.Kode_Perusahaan = d.Kode_Perusahaan
                                        AND f.No_Faktur = d.No_Formulator
                                     WHERE d.Kode_Perusahaan = lb.Kode_Perusahaan
                                       AND d.No_Faktur = lb.No_Faktur
                                       AND f.Status IS NULL
                                       AND f.Flag_Deprecated_Binding IS NULL
                                       AND (
                                           f.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                                           OR f.Flag_Validasi_Formula_Produksi = 'Y'
                                           OR f.Flag_Validasi_Main = 'Y'
                                       )
                                 )
                            THEN
                                'BINDING AKTIF'
                            WHEN lb.Flag_Validasi_Main = 'Y' THEN
                                'BINDING TIDAK VALID'
                            WHEN lb.Flag_Validasi_Main IS NULL THEN
                                'MENUNGGU VALIDASI'
                            WHEN lb.Flag_Validasi_Main = 'T'
                                 AND EXISTS (
                                     SELECT 1
                                     FROM N_EMI_Transaksi_Formulator_Binding x
                                     WHERE x.Kode_Perusahaan = lb.Kode_Perusahaan
                                       AND x.Kode_Barang = lb.Kode_Barang
                                       AND x.Status IS NULL
                                       AND x.Flag_Validasi_Main = 'Y'
                                       AND x.No_Faktur < lb.No_Faktur
                                 )
                            THEN
                                'BINDING AKTIF (PENGAJUAN TERBARU DITOLAK)'
                            WHEN lb.Flag_Validasi_Main = 'T' THEN
                                'DITOLAK'
                            ELSE
                                'UNKNOWN'
                        END AS Status_Binding
                    FROM Barang a
                    INNER JOIN EMI_Group_Jenis c
                        ON c.Kode_Perusahaan = a.Kode_Perusahaan
                       AND c.Id_Group_Jenis = a.Id_Group_Jenis
                       AND (
                           c.Flag_Finished_Good = 'Y'
                           OR c.Flag_Semi_FG = 'Y'
                       )
                    LEFT JOIN EMI_Transaksi_Formulator b
                        ON b.Kode_Perusahaan = a.Kode_Perusahaan
                       AND b.Kode_Barang = a.Kode_Barang_Inq
                       AND b.Kode_Stock_Owner = a.Kode_Stock_Owner
                       AND b.Status IS NULL
                       AND b.Flag_Deprecated_Binding IS NULL
                       AND (
                           b.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                           OR b.Flag_Validasi_Formula_Produksi = 'Y'
                           OR b.Flag_Validasi_Main = 'Y'
                       )
                    OUTER APPLY (
                        SELECT TOP 1
                            *
                        FROM N_EMI_Transaksi_Formulator_Binding lb
                        WHERE lb.Kode_Perusahaan = a.Kode_Perusahaan
                          AND lb.Kode_Barang = a.Kode_Barang_Inq
                          AND lb.Status IS NULL
                        ORDER BY
                            lb.Tanggal DESC,
                            lb.Jam DESC
                    ) lb
                    WHERE a.Kode_Perusahaan = '{KodePerusahaan}' AND (a.Kode_Barang_Inq LIKE '%{Keyword}%' OR a.Nama LIKE '%{Keyword}%')
                    GROUP BY
                        a.Kode_Barang,
                        a.Kode_Barang_Inq,
                        a.Nama,
                        lb.No_Faktur,
                        lb.Flag_Validasi_Main,
                        lb.Kode_Perusahaan,
                        lb.Kode_Barang
                )
                SELECT *
                FROM Data
                ORDER BY
                    CASE Status_Binding
                        WHEN 'MENUNGGU VALIDASI' THEN 1
                        WHEN 'BINDING AKTIF' THEN 2
                        WHEN 'BINDING AKTIF (PENGAJUAN TERBARU DITOLAK)' THEN 2
                        WHEN 'BINDING TIDAK VALID' THEN 3
                        WHEN 'BELUM ADA BINDING' THEN 3
                        WHEN 'DITOLAK' THEN 4
                        ELSE 5
                    END,
                    Kode_Barang_Inq;
			"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DGV_Barang.Rows.Add()
                            DGV_Barang.Rows(i).Cells(0).Value = If(General_Class.CekNULL(.Rows(i).Item("kode_barang_inq")) = "", "", .Rows(i).Item("kode_barang_inq").ToString())
                            DGV_Barang.Rows(i).Cells(1).Value = If(General_Class.CekNULL(.Rows(i).Item("nama")) = "", "", .Rows(i).Item("nama").ToString())
                            DGV_Barang.Rows(i).Cells(2).Value = Format(If(General_Class.CekNULL(.Rows(i).Item("total_formula")) = "", 0, .Rows(i).Item("total_formula")), "N0")

                            Dim status As String = If(General_Class.CekNULL(.Rows(i).Item("status_binding")) = "", "", .Rows(i).Item("status_binding").ToString())

                            Select Case status
                                Case "BINDING AKTIF"
                                    DGV_Barang.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                                Case "BINDING AKTIF (PENGAJUAN TERBARU DITOLAK)"
                                    DGV_Barang.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                                Case "MENUNGGU VALIDASI"
                                    DGV_Barang.Rows(i).DefaultCellStyle.BackColor = Color.Khaki

                                Case "DITOLAK"
                                    DGV_Barang.Rows(i).DefaultCellStyle.BackColor = Color.LightCoral

                                Case "BINDING TIDAK VALID"
                                    DGV_Barang.Rows(i).DefaultCellStyle.BackColor = Color.DarkGray

                                Case "BELUM ADA BINDING"
                                    DGV_Barang.Rows(i).DefaultCellStyle.BackColor = Color.DarkGray

                                Case Else
                                    DGV_Barang.Rows(i).DefaultCellStyle.BackColor = Color.White
                            End Select
                        Next
                    Else
                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            DGV_Barang.ClearSelection()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show("Gagal load data barang: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub Init_TLPInformasiBarang()
        TLP_InformasiBarang.RowCount = 0
        TLP_InformasiBarang.RowStyles.Clear()
        TLP_InformasiBarang.Controls.Clear()
        lblValueMap.Clear()
    End Sub

    Private Sub AddField(title As String, Optional maxLines As Integer = 1)
        If TLP_InformasiBarang.RowCount > 0 Then
            Dim rowSep As Integer = TLP_InformasiBarang.RowCount
            TLP_InformasiBarang.RowCount += 1
            TLP_InformasiBarang.RowStyles.Add(New RowStyle(SizeType.Absolute, 1))

            Dim pnlSep As New Panel()
            pnlSep.Dock = DockStyle.Fill
            pnlSep.BackColor = Color.FromArgb(220, 220, 220)
            pnlSep.Height = 1
            TLP_InformasiBarang.Controls.Add(pnlSep, 0, rowSep)
        End If

        Dim rowTitle As Integer = TLP_InformasiBarang.RowCount
        TLP_InformasiBarang.RowCount += 1
        TLP_InformasiBarang.RowStyles.Add(New RowStyle(SizeType.Absolute, 18))

        Dim lblTitle As New Label()
        lblTitle.Text = title
        lblTitle.Font = New Font("Work Sans", 8, FontStyle.Bold)
        lblTitle.Dock = DockStyle.Fill
        lblTitle.TextAlign = ContentAlignment.BottomLeft
        lblTitle.AutoSize = False
        lblTitle.Padding = New Padding(0, 4, 0, 0)
        TLP_InformasiBarang.Controls.Add(lblTitle, 0, rowTitle)

        Dim rowHeight As Integer = If(maxLines > 1, 20 * maxLines, 20)
        Dim rowValue As Integer = TLP_InformasiBarang.RowCount
        TLP_InformasiBarang.RowCount += 1
        TLP_InformasiBarang.RowStyles.Add(New RowStyle(SizeType.Absolute, rowHeight))

        Dim lblValue As New Label()
        lblValue.Text = "-"
        lblValue.Font = New Font("Work Sans", 8, FontStyle.Regular)
        lblValue.Dock = DockStyle.Fill
        lblValue.AutoSize = False
        lblValue.TextAlign = ContentAlignment.TopLeft

        If maxLines > 1 Then
            lblValue.AutoEllipsis = True
            lblValue.MaximumSize = New Size(0, rowHeight)
        End If

        lblValueMap(title) = lblValue
        TLP_InformasiBarang.Controls.Add(lblValue, 0, rowValue)
    End Sub

    Private Sub SetValue(title As String, value As String)
        If lblValueMap.ContainsKey(title) Then
            lblValueMap(title).Text = value
        End If
    End Sub

    Private Sub Load_SusunanFormula()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = $"
                WITH BindingData AS (
                    SELECT
                        a.No_Faktur,
                        a.Tanggal,
                        a.Jam,
                        a.UserID,
                        a.Flag_Validasi_Main,
                        COUNT(*) AS Total_Formula,
                        ROW_NUMBER() OVER (
                            PARTITION BY a.Kode_Barang
                            ORDER BY
                                CASE
                                    WHEN a.Flag_Validasi_Main = 'Y' THEN 0
                                    ELSE 1
                                END,
                                a.Tanggal DESC,
                                a.Jam DESC
                        ) AS RN_Valid
                    FROM N_EMI_Transaksi_Formulator_Binding a
                    JOIN N_EMI_Transaksi_Formulator_Binding_Detail b
                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                       AND a.No_Faktur = b.No_Faktur
                    LEFT JOIN EMI_Transaksi_Formulator f
                        ON f.Kode_Perusahaan = b.Kode_Perusahaan
                       AND f.No_Faktur = b.No_Formulator
                       AND a.Kode_Barang = f.Kode_Barang
                    WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
                      AND a.Kode_Barang = '{selectedKodeBarang}'
                      AND a.Status IS NULL
                    GROUP BY
                        a.No_Faktur,
                        a.Tanggal,
                        a.Jam,
                        a.UserID,
                        a.Flag_Validasi_Main,
                        a.Kode_Barang
                    HAVING SUM(
                        CASE
                            WHEN f.Status IS NULL AND f.Flag_Deprecated_Binding IS NULL
                             AND (
                                    f.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                                 OR f.Flag_Validasi_Formula_Produksi = 'Y'
                                 OR f.Flag_Validasi_Main = 'Y'
                             )
                            THEN 1
                            ELSE 0
                        END
                    ) > 0
                )
                SELECT
                    No_Faktur,
                    Total_Formula,
                    FORMAT(Tanggal, 'dd MMM yyyy') AS Tanggal_Display,
                    UserID,
                    RN_Valid,
                    CASE
                        WHEN Flag_Validasi_Main = 'Y' AND RN_Valid = 1 THEN 'AKTIF'
                        WHEN Flag_Validasi_Main = 'Y' THEN 'TIDAK AKTIF'
                        WHEN Flag_Validasi_Main IS NULL THEN 'MENUNGGU VALIDASI'
                        WHEN Flag_Validasi_Main = 'T' THEN 'DITOLAK'
                    END AS status_binding
                FROM BindingData
                ORDER BY Tanggal Desc, Jam desc;
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DGV_SusunanFormula.Rows.Add()
                            DGV_SusunanFormula.Rows(i).Cells(0).Value = If(General_Class.CekNULL(.Rows(i).Item("No_Faktur")) = "", "", .Rows(i).Item("No_Faktur").ToString())
                            DGV_SusunanFormula.Rows(i).Cells(1).Value = Format(If(General_Class.CekNULL(.Rows(i).Item("Total_Formula")) = "", 0, .Rows(i).Item("Total_Formula")), "N0")
                            DGV_SusunanFormula.Rows(i).Cells(2).Value = If(General_Class.CekNULL(.Rows(i).Item("Tanggal_Display")) = "", "", .Rows(i).Item("Tanggal_Display").ToString())
                            DGV_SusunanFormula.Rows(i).Cells(3).Value = If(General_Class.CekNULL(.Rows(i).Item("UserID")) = "", "", .Rows(i).Item("UserID").ToString())
                            DGV_SusunanFormula.Rows(i).Cells(4).Value = If(General_Class.CekNULL(.Rows(i).Item("status_binding")) = "", "", .Rows(i).Item("status_binding").ToString())

                            Dim status As String = If(General_Class.CekNULL(.Rows(i).Item("status_binding")) = "", "", .Rows(i).Item("status_binding").ToString())

                            Select Case status
                                Case "AKTIF"
                                    DGV_SusunanFormula.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                                Case "TIDAK AKTIF"
                                    DGV_SusunanFormula.Rows(i).DefaultCellStyle.BackColor = Color.DarkGray

                                Case "MENUNGGU VALIDASI"
                                    DGV_SusunanFormula.Rows(i).DefaultCellStyle.BackColor = Color.Khaki

                                Case "DITOLAK"
                                    DGV_SusunanFormula.Rows(i).DefaultCellStyle.BackColor = Color.LightCoral

                                Case Else
                                    DGV_SusunanFormula.Rows(i).DefaultCellStyle.BackColor = Color.White
                            End Select
                        Next
                    Else
                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            DGV_SusunanFormula.ClearSelection()
            DGV_DetailSusunanFormula.ClearSelection()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show("Gagal load susunan formula: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

    End Sub

    Private Sub Load_BuatSusunanFormula()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = $"
                WITH BindingAktif AS (
                    SELECT TOP 1
                        No_Faktur
                    FROM N_EMI_Transaksi_Formulator_Binding
                    WHERE
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{selectedKodeBarang}'
                        AND Flag_Validasi_Main = 'Y'
                        AND Status IS NULL
                    ORDER BY Tanggal DESC, Jam DESC
                )
                SELECT
                    F.No_Faktur,
                    F.Kode_Hierarki
                FROM EMI_Transaksi_Formulator F
                WHERE
                    F.Kode_Perusahaan = '{KodePerusahaan}'
                    AND F.Kode_Barang = '{selectedKodeBarang}'
                    AND F.Status IS NULL
                    AND F.Flag_Deprecated_Binding IS NULL
                    AND (
                        F.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                        OR F.Flag_Validasi_Formula_Produksi = 'Y'
                        OR F.Flag_Validasi_Main = 'Y'
                    )
                    AND NOT EXISTS (
                        SELECT 1
                        FROM BindingAktif BA
                        INNER JOIN N_EMI_Transaksi_Formulator_Binding_Detail D
                            ON D.No_Faktur = BA.No_Faktur
                        WHERE D.No_Formulator = F.No_Faktur
                    )
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DGV_FormulaTersedia.Rows.Add()
                            DGV_FormulaTersedia.Rows(i).Cells(0).Value = If(General_Class.CekNULL(.Rows(i).Item("No_Faktur")) = "", "", .Rows(i).Item("No_Faktur").ToString())
                            DGV_FormulaTersedia.Rows(i).Cells(1).Value = If(General_Class.CekNULL(.Rows(i).Item("Kode_Hierarki")) = "", "", .Rows(i).Item("Kode_Hierarki").ToString())
                            DGV_FormulaTersedia.Rows(i).Cells(2).Value = ""
                            DGV_FormulaTersedia.Rows(i).Cells(3).Value = "➕"
                        Next
                    Else
                    End If
                End With
            End Using

            SQL = $"
                WITH BindingAktif AS (
                    SELECT TOP 1
                        No_Faktur
                    FROM N_EMI_Transaksi_Formulator_Binding
                    WHERE
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{selectedKodeBarang}'
                        AND Flag_Validasi_Main = 'Y'
                        AND Status IS NULL
                    ORDER BY Tanggal DESC, Jam DESC
                )
                SELECT
                    F.No_Faktur,
                    F.Kode_Hierarki,
                    D.Keterangan
                FROM BindingAktif BA
                INNER JOIN N_EMI_Transaksi_Formulator_Binding_Detail D
                    ON D.No_Faktur = BA.No_Faktur
                INNER JOIN EMI_Transaksi_Formulator F
                    ON F.Kode_Perusahaan = D.Kode_Perusahaan
                    AND F.No_Faktur = D.No_Formulator
                WHERE
                    F.Kode_Perusahaan = '{KodePerusahaan}'
                    AND F.Kode_Barang = '{selectedKodeBarang}'
                    AND F.Status IS NULL
                    AND F.Flag_Deprecated_Binding IS NULL
                ORDER BY D.No_Prioritas
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DGV_FormulaTerbinding.Rows.Add()
                            DGV_FormulaTerbinding.Rows(i).Cells(0).Value = If(General_Class.CekNULL(.Rows(i).Item("No_Faktur")) = "", "", .Rows(i).Item("No_Faktur").ToString())
                            DGV_FormulaTerbinding.Rows(i).Cells(1).Value = If(General_Class.CekNULL(.Rows(i).Item("Kode_Hierarki")) = "", "", .Rows(i).Item("Kode_Hierarki").ToString())
                            DGV_FormulaTerbinding.Rows(i).Cells(2).Value = If(General_Class.CekNULL(.Rows(i).Item("Keterangan")) = "", "", .Rows(i).Item("Keterangan").ToString())
                            DGV_FormulaTerbinding.Rows(i).Cells(3).Value = "▲"
                            DGV_FormulaTerbinding.Rows(i).Cells(4).Value = "▼"
                            DGV_FormulaTerbinding.Rows(i).Cells(5).Value = "✖"
                        Next
                    Else
                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            UpdateButtonState()

            DGV_FormulaTersedia.ClearSelection()
            DGV_FormulaTerbinding.ClearSelection()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show("Gagal load buat susunan formula: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub DGV_Barang_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Barang.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If changeNotSaved Then
            Dim result = MessageBox.Show(
                "Perubahan belum disimpan. Lanjutkan?",
                Judul,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )
            If result = DialogResult.No Then Exit Sub
            changeNotSaved = False
        End If

        DGV_SusunanFormula.Rows.Clear()
        DGV_DetailSusunanFormula.Rows.Clear()
        DGV_FormulaTersedia.Rows.Clear()
        DGV_FormulaTerbinding.Rows.Clear()
        Dim kodeBarang As String = DGV_Barang.Rows(e.RowIndex).Cells(0).Value.ToString()
        Dim namaBarang As String = DGV_Barang.Rows(e.RowIndex).Cells(1).Value.ToString()
        selectedKodeBarang = kodeBarang

        Show_InformasiBarang(kodeBarang, namaBarang)

        If GB_InformasiSusunanFormula.Visible Then
            Load_SusunanFormula()
        ElseIf GB_BuatSusunanFormula.Visible Then
            Load_BuatSusunanFormula()
        End If
    End Sub

    Private Sub DGV_SusunanFormula_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_SusunanFormula.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        DGV_DetailSusunanFormula.Rows.Clear()
        Dim kodeBinding As String = DGV_SusunanFormula.Rows(e.RowIndex).Cells(0).Value.ToString()
        Dim statusParent As String = DGV_SusunanFormula.Rows(e.RowIndex).Cells(4).Value.ToString()

        selectedFakturSusunan = kodeBinding

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = $"
                SELECT b.No_Prioritas, f.No_Faktur, f.Kode_Hierarki, b.Keterangan, f.Hasil, f.Satuan_Hasil, b.Flag_Invalid FROM N_EMI_Transaksi_Formulator_Binding a
                JOIN N_EMI_Transaksi_Formulator_Binding_Detail b
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan
                   AND a.No_Faktur = b.No_Faktur
                JOIN EMI_Transaksi_Formulator f
                    ON f.Kode_Perusahaan = b.Kode_Perusahaan
                   AND f.No_Faktur = b.No_Formulator
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
                  AND a.No_Faktur = '{kodeBinding}'
                  AND a.Status IS NULL
                  AND f.Status IS NULL
                  AND f.Flag_Deprecated_Binding IS NULL
                   AND (
                       f.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                       OR f.Flag_Validasi_Formula_Produksi = 'Y'
                       OR f.Flag_Validasi_Main = 'Y'
                   )
                ORDER BY b.No_Prioritas
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DGV_DetailSusunanFormula.Rows.Add()
                            DGV_DetailSusunanFormula.Rows(i).Cells(0).Value = If(General_Class.CekNULL(.Rows(i).Item("No_Faktur")) = "", "", .Rows(i).Item("No_Faktur").ToString())
                            DGV_DetailSusunanFormula.Rows(i).Cells(1).Value = If(General_Class.CekNULL(.Rows(i).Item("Kode_Hierarki")) = "", "", .Rows(i).Item("Kode_Hierarki").ToString())
                            DGV_DetailSusunanFormula.Rows(i).Cells(2).Value = If(General_Class.CekNULL(.Rows(i).Item("Keterangan")) = "", "", .Rows(i).Item("Keterangan").ToString())
                            DGV_DetailSusunanFormula.Rows(i).Cells(3).Value = Format(If(General_Class.CekNULL(.Rows(i).Item("Hasil")) = "", 0, .Rows(i).Item("Hasil")), "N4")
                            DGV_DetailSusunanFormula.Rows(i).Cells(4).Value = If(General_Class.CekNULL(.Rows(i).Item("Satuan_Hasil")) = "", "", .Rows(i).Item("Satuan_Hasil").ToString())

                            Dim noPrioritas As String = If(General_Class.CekNULL(.Rows(i).Item("No_Prioritas")) = "", "", .Rows(i).Item("No_Prioritas").ToString())
                            Dim isInvalid As String = If(General_Class.CekNULL(.Rows(i).Item("Flag_Invalid")) = "", "", .Rows(i).Item("Flag_Invalid").ToString())
                            If noPrioritas = "1" AndAlso statusParent = "AKTIF" AndAlso isInvalid = "" Then
                                DGV_DetailSusunanFormula.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                            Else
                                DGV_DetailSusunanFormula.Rows(i).DefaultCellStyle.BackColor = Color.White
                            End If
                        Next
                    Else
                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            DGV_DetailSusunanFormula.ClearSelection()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show("Gagal load detail susunan formula: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

    End Sub

    Private Sub DGV_FormulaTerbinding_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_FormulaTerbinding.CellMouseEnter
        If e.RowIndex < 0 Then Exit Sub

        If e.ColumnIndex = 3 OrElse e.ColumnIndex = 4 OrElse e.ColumnIndex = 5 Then
            DGV_FormulaTerbinding.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub DGV_FormulaTerbinding_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_FormulaTerbinding.CellMouseLeave
        DGV_FormulaTerbinding.Cursor = Cursors.Default
    End Sub

    Private Sub DGV_FormulaTerbinding_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_FormulaTerbinding.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        If e.ColumnIndex = 3 Then
            MoveRow(e.RowIndex, -1)

            changeNotSaved = True
        ElseIf e.ColumnIndex = 4 Then
            MoveRow(e.RowIndex, 1)

            changeNotSaved = True
        ElseIf e.ColumnIndex = 5 Then
            Dim noFormula As String = DGV_FormulaTerbinding.Rows(e.RowIndex).Cells(0).Value.ToString()
            Dim keterangan As String = DGV_FormulaTerbinding.Rows(e.RowIndex).Cells(2).Value.ToString()

            Dim result = MessageBox.Show(
                $"Hapus Formula No {noFormula}?",
                Judul,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )

            If result = DialogResult.Yes Then

                Try
                    OpenConn()
                    Cmd.Transaction = Cn.BeginTransaction

                    SQL = $"UPDATE Emi_Transaksi_Formulator SET Flag_Deprecated_Binding = 'Y' WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Faktur = '{noFormula}'"
                    ExecuteTrans(SQL)

                    Cmd.Transaction.Commit()
                    CloseTrans()
                    CloseConn()
                Catch ex As Exception
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                'Dim idx = DGV_FormulaTersedia.Rows.Add()

                'DGV_FormulaTersedia.Rows(idx).Cells(0).Value = DGV_FormulaTerbinding.Rows(e.RowIndex).Cells(0).Value
                'DGV_FormulaTersedia.Rows(idx).Cells(1).Value = DGV_FormulaTerbinding.Rows(e.RowIndex).Cells(1).Value
                'DGV_FormulaTersedia.Rows(idx).Cells(2).Value = DGV_FormulaTerbinding.Rows(e.RowIndex).Cells(2).Value
                'DGV_FormulaTersedia.Rows(idx).Cells(3).Value = "➕"
                DGV_FormulaTerbinding.Rows.RemoveAt(e.RowIndex)
                UpdateButtonState()
            End If
        End If
    End Sub

    Private Sub MoveRow(index As Integer, direction As Integer)
        Dim targetIndex As Integer = index + direction

        If targetIndex < 0 OrElse targetIndex >= DGV_FormulaTerbinding.Rows.Count Then Exit Sub

        Dim row As DataGridViewRow = DGV_FormulaTerbinding.Rows(index)

        DGV_FormulaTerbinding.Rows.RemoveAt(index)
        DGV_FormulaTerbinding.Rows.Insert(targetIndex, row)

        DGV_FormulaTerbinding.ClearSelection()
        DGV_FormulaTerbinding.Rows(targetIndex).Selected = True
        DGV_FormulaTerbinding.CurrentCell = DGV_FormulaTerbinding.Rows(targetIndex).Cells(0)

        UpdateButtonState()
    End Sub

    Private Sub UpdateButtonState()
        Dim lastIndex As Integer = DGV_FormulaTerbinding.Rows.Count - 1
        Dim upColor As Color = Color.FromArgb(15, 86, 122)
        Dim downColor As Color = Color.FromArgb(15, 86, 122)
        Dim deleteColor As Color = Color.FromArgb(15, 86, 122)

        For i As Integer = 0 To lastIndex

            Dim row = DGV_FormulaTerbinding.Rows(i)

            SetButton(row.Cells(3), "▲", upColor, Color.White)

            If i = 0 Then
                SetButton(row.Cells(3), "", Color.White, Color.White)
            End If

            SetButton(row.Cells(4), "▼", downColor, Color.White)

            If i = lastIndex Then
                SetButton(row.Cells(4), "", Color.White, Color.White)
            End If

            SetButton(row.Cells(5), "✖", deleteColor, Color.White)
        Next
    End Sub

    Private Sub SetButton(cell As DataGridViewCell, text As String, back As Color, fore As Color)
        cell.Value = text
        cell.Style.BackColor = back
        cell.Style.ForeColor = fore
    End Sub

    Private Sub DGV_FormulaTersedia_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_FormulaTersedia.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        If DGV_FormulaTersedia.Columns(e.ColumnIndex).Index <> 3 Then Exit Sub

        Dim noFaktur As String = DGV_FormulaTersedia.Rows(e.RowIndex).Cells(0).Value.ToString()
        Dim hierarki As String = DGV_FormulaTersedia.Rows(e.RowIndex).Cells(1).Value.ToString()
        Dim keterangan As String = DGV_FormulaTersedia.Rows(e.RowIndex).Cells(2).Value.ToString()

        Dim result = MessageBox.Show(
            $"Tambahkan formula {noFaktur} ke posisi tertentu?",
            "Konfirmasi",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question
        )

        If result = DialogResult.Cancel Then Exit Sub

        If result = DialogResult.Yes Then

            Dim ListNoFakturBinding As New List(Of String)
            Dim ListHierarki As New List(Of String)
            Dim ListKeterangan As New List(Of String)

            For Each row As DataGridViewRow In DGV_FormulaTerbinding.Rows
                If row.IsNewRow Then Continue For

                ListNoFakturBinding.Add(row.Cells(0).Value?.ToString())
                ListHierarki.Add(row.Cells(1).Value?.ToString())
                ListKeterangan.Add(row.Cells(2).Value?.ToString())
            Next

            Dim SD As New N_EMI_SD_Compare_Formulator With {
                .NoFaktur = noFaktur,
                .ArrNoFaktur = ListNoFakturBinding,
                .ArrHierarki = ListHierarki,
                .ArrKeterangan = ListKeterangan
            }

            If SD.ShowDialog() = DialogResult.OK Then

                DGV_FormulaTerbinding.Rows.Clear()

                For i As Integer = 0 To SD.ArrNoFaktur.Count - 1

                    Dim idx = DGV_FormulaTerbinding.Rows.Add()

                    DGV_FormulaTerbinding.Rows(idx).Cells(0).Value = SD.ArrNoFaktur(i)
                    DGV_FormulaTerbinding.Rows(idx).Cells(1).Value = SD.ArrHierarki(i)
                    DGV_FormulaTerbinding.Rows(idx).Cells(2).Value = SD.ArrKeterangan(i)

                    DGV_FormulaTerbinding.Rows(idx).Cells(3).Value = "▲"
                    DGV_FormulaTerbinding.Rows(idx).Cells(4).Value = "▼"
                    DGV_FormulaTerbinding.Rows(idx).Cells(5).Value = "✖"

                Next

                DGV_FormulaTersedia.Rows.RemoveAt(e.RowIndex)

            End If

        Else
            Dim idx = DGV_FormulaTerbinding.Rows.Add()

            DGV_FormulaTerbinding.Rows(idx).Cells(0).Value = noFaktur
            DGV_FormulaTerbinding.Rows(idx).Cells(1).Value = hierarki
            DGV_FormulaTerbinding.Rows(idx).Cells(2).Value = keterangan

            DGV_FormulaTerbinding.Rows(idx).Cells(3).Value = "▲"
            DGV_FormulaTerbinding.Rows(idx).Cells(4).Value = "▼"
            DGV_FormulaTerbinding.Rows(idx).Cells(5).Value = "✖"

            DGV_FormulaTersedia.Rows.RemoveAt(e.RowIndex)
        End If

        changeNotSaved = True
        UpdateButtonState()
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        If changeNotSaved Then
            Dim result = MessageBox.Show(
                "Perubahan belum disimpan. Refresh akan membatalkan perubahan. Lanjutkan?",
                Judul,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )
            If result = DialogResult.No Then Exit Sub
            changeNotSaved = False
        End If

        TxtCariBarang.Text = ""
        DGV_SusunanFormula.Rows.Clear()
        DGV_DetailSusunanFormula.Rows.Clear()
        DGV_FormulaTersedia.Rows.Clear()
        DGV_FormulaTerbinding.Rows.Clear()
        Load_DGV_Barang()
        SetValue("Kode Barang", "-")
        SetValue("Nama Barang", "-")
        SetValue("No Faktur Susunan", "-")
        SetValue("No Formula", "-")
        SetValue("Hasil", "-")
        SetValue("Keterangan", "-")
        SetValue("Tanggal Validasi", "-")
    End Sub

    Private Function get_no_faktur() As String
        Dim NoFaktur = fBindingFormula & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("N_EMI_Transaksi_Formulator_Binding", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(fBindingFormula) + 4 & ")", fBindingFormula & Format(tgl_skg, "MMyy"))

        Return NoFaktur
    End Function

    Private Sub BtnSimpanBinding_Click(sender As Object, e As EventArgs) Handles BtnSimpanBinding.Click
        Dim TotalFormula As Integer = DGV_FormulaTerbinding.Rows.Cast(Of DataGridViewRow).Count(Function(r) Not r.IsNewRow)

        If TotalFormula = 0 Then
            MessageBox.Show("Minimal harus ada satu formula yang di binding.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If String.IsNullOrEmpty(selectedKodeBarang) Then
            MessageBox.Show("Pilih barang terlebih dahulu.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()

        Try

            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim NoFaktur As String = get_no_faktur()
            Dim KodeBarang As String = selectedKodeBarang
            Dim Tanggal As String = Format(tgl_skg, "yyyy-MM-dd")
            Dim Jam As String = Format(tgl_skg, "HH:mm:ss")

            SQL = $"
                SELECT TOP 1
                    Kode_Perusahaan
                FROM N_EMI_Transaksi_Formulator_Binding
                WHERE
                    Kode_Perusahaan = '{KodePerusahaan}'
                    AND Kode_Barang = '{KodeBarang}'
                    AND Status IS NULL
                    AND Flag_Validasi_Main IS NULL
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dr.Close()

                    Cmd.Transaction.Rollback()

                    CloseTrans()
                    CloseConn()

                    MessageBox.Show(
                        "Binding sudah diinput, silahkan menunggu validasi.",
                        Judul,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    )
                    Exit Sub
                End If
            End Using

            SQL = $"
                INSERT INTO N_EMI_Transaksi_Formulator_Binding
                (
                    Kode_Perusahaan,
                    No_Faktur,
                    Kode_Barang,
                    Tanggal,
                    Jam,
                    UserID,
                    Status
                )
                VALUES
                (
                    '{KodePerusahaan}',
                    '{NoFaktur}',
                    '{KodeBarang}',
                    '{Tanggal}',
                    '{Jam}',
                    '{UserID}',
                    NULL
                )
            "

            ExecuteTrans(SQL)
            Dim Prioritas As Integer = 1

            For Each row As DataGridViewRow In DGV_FormulaTerbinding.Rows
                If row.IsNewRow Then Continue For

                Dim NoFormulator As String =
                If(row.Cells(0).Value Is Nothing, "", row.Cells(0).Value.ToString())

                Dim KodeHierarki As String =
                If(row.Cells(1).Value Is Nothing, "", row.Cells(1).Value.ToString())

                Dim Keterangan As String =
                If(row.Cells(2).Value Is Nothing, "", row.Cells(2).Value.ToString())

                SQL = $"
                    INSERT INTO N_EMI_Transaksi_Formulator_Binding_Detail
                    (
                        Kode_Perusahaan,
                        No_Faktur,
                        No_Formulator,
                        No_Prioritas,
                        Kode_Hierarki,
                        Tanggal,
                        Jam,
                        UserID,
                        Keterangan
                    )
                    VALUES
                    (
                        '{KodePerusahaan}',
                        '{NoFaktur}',
                        '{NoFormulator}',
                        '{Prioritas}',
                        '{KodeHierarki}',
                        '{Tanggal}',
                        '{Jam}',
                        '{UserID}',
                        '{Keterangan}'
                    )
                "

                ExecuteTrans(SQL)

                Prioritas += 1
            Next

            Cmd.Transaction.Commit()

            CloseTrans()
            CloseConn()

            DGV_FormulaTerbinding.Rows.Clear()
            DGV_FormulaTersedia.Rows.Clear()

            Load_DGV_Barang()
            changeNotSaved = False

            SetValue("Kode Barang", "-")
            SetValue("Nama Barang", "-")
            SetValue("No Faktur Susunan", "-")
            SetValue("No Formula", "-")
            SetValue("Hasil", "-")
            SetValue("Keterangan", "-")
            SetValue("Tanggal Validasi", "-")

            MessageBox.Show(
                "Data binding berhasil disimpan.",
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub BtnCariBarang_Click(sender As Object, e As EventArgs) Handles BtnCariBarang.Click
        Load_DGV_Barang()
        TxtCariBarang.Focus()
    End Sub

    Private Sub DGV_FormulaTersedia_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_FormulaTersedia.CellDoubleClick
        If e.RowIndex < 0 OrElse e.ColumnIndex = 2 Then Exit Sub

        Dim noFaktur As String = DGV_FormulaTersedia.Rows(e.RowIndex).Cells(0).Value.ToString()

        Dim SD As New N_EMI_SD_Detail_Formulator With {
            .NoFaktur = noFaktur
        }

        SD.ShowDialog()
    End Sub

    Private Sub DGV_FormulaTerbinding_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_FormulaTerbinding.CellDoubleClick
        If e.RowIndex < 0 OrElse e.ColumnIndex = 3 OrElse e.ColumnIndex = 4 OrElse e.ColumnIndex = 5 Then Exit Sub

        Dim noFaktur As String = DGV_FormulaTerbinding.Rows(e.RowIndex).Cells(0).Value.ToString()

        Dim SD As New N_EMI_SD_Detail_Formulator With {
            .NoFaktur = noFaktur
        }

        SD.ShowDialog()
    End Sub

    Private Sub DGV_DetailSusunanFormula_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_DetailSusunanFormula.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        Dim noFaktur As String = DGV_DetailSusunanFormula.Rows(e.RowIndex).Cells(0).Value.ToString()

        Dim SD As New N_EMI_SD_Detail_Formulator With {
            .NoFaktur = noFaktur
        }

        SD.ShowDialog()
    End Sub

    Private Sub TxtCariBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtCariBarang.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnCariBarang.Focus()
        End If
    End Sub

    Private Sub Show_InformasiBarang(KodeBarang As String, NamaBarang As String)
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = $"
                WITH LatestBinding AS (
                    SELECT
                        a.No_Faktur,
                        a.Kode_Barang,
                        a.Tanggal,
                        a.Jam,
                        ROW_NUMBER() OVER (
                            PARTITION BY a.Kode_Barang
                            ORDER BY a.Tanggal DESC, a.Jam DESC
                        ) AS rn
                    FROM N_EMI_Transaksi_Formulator_Binding a
                    WHERE
                        a.Kode_Perusahaan = '{KodePerusahaan}'
                        AND a.Kode_Barang = '{KodeBarang}'
                        AND a.Status IS NULL
                        AND a.Flag_Validasi_Main = 'Y'
                )
                SELECT TOP 1
                    lb.No_Faktur AS No_Faktur_Binding,
                    d.No_Formulator AS No_Formula,
                    f.Hasil,
                    f.Satuan_Hasil,
                    d.Keterangan,
                    FORMAT(
                        COALESCE(
                            f.Tanggal_Validasi_Formula_Produksi_BOD,
                            f.Tanggal_Validasi_Main
                        ),
                        'dd MMM yy'
                    ) AS Tanggal_Validasi
                FROM LatestBinding lb
                JOIN N_EMI_Transaksi_Formulator_Binding_Detail d
                    ON lb.No_Faktur = d.No_Faktur
                JOIN EMI_Transaksi_Formulator f
                    ON f.Kode_Perusahaan = d.Kode_Perusahaan
                   AND f.No_Faktur = d.No_Formulator
                WHERE
                    lb.rn = 1
                    AND d.No_Prioritas = 1
                    AND d.Flag_Invalid IS NULL
                    AND f.Status IS NULL
                    AND f.Flag_Deprecated_Binding IS NULL
                    AND (
                           f.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                        OR f.Flag_Validasi_Formula_Produksi = 'Y'
                        OR f.Flag_Validasi_Main = 'Y'
                    );
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    SetValue("Kode Barang", KodeBarang)
                    SetValue("Nama Barang", NamaBarang)
                    SetValue("No Faktur Susunan", If(General_Class.CekNULL(Dr("No_Faktur_Binding")).Trim() = "", "-", General_Class.CekNULL(Dr("No_Faktur_Binding")).Trim()))
                    SetValue("No Formula", If(General_Class.CekNULL(Dr("No_Formula")).Trim() = "", "-", General_Class.CekNULL(Dr("No_Formula")).Trim()))
                    Dim hasil As String = General_Class.CekNULL(Dr("Hasil")).Trim()
                    Dim satuan As String = General_Class.CekNULL(Dr("Satuan_Hasil")).Trim()
                    SetValue("Hasil", If(hasil = "", "-", hasil & If(satuan = "", "", " " & satuan)))
                    SetValue("Keterangan", If(General_Class.CekNULL(Dr("Keterangan")).Trim() = "", "-", General_Class.CekNULL(Dr("Keterangan")).Trim()))
                    SetValue("Tanggal Validasi", If(General_Class.CekNULL(Dr("Tanggal_Validasi")).Trim() = "", "-", General_Class.CekNULL(Dr("Tanggal_Validasi")).Trim()))
                Else
                    SetValue("Kode Barang", KodeBarang)
                    SetValue("Nama Barang", NamaBarang)
                    SetValue("No Faktur Susunan", "-")
                    SetValue("No Formula", "-")
                    SetValue("Hasil", "-")
                    SetValue("Keterangan", "-")
                    SetValue("Tanggal Validasi", "-")
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show($"Gagal load informasi barang {KodeBarang}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub BTN_LihatSusunanFormula_Click(sender As Object, e As EventArgs) Handles BTN_LihatSusunanFormula.Click
        If changeNotSaved Then
            Dim result = MessageBox.Show(
                "Perubahan belum disimpan. Lanjutkan?",
                Judul,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )
            If result = DialogResult.No Then Exit Sub
            changeNotSaved = False
        End If

        GB_BuatSusunanFormula.Visible = False
        GB_InformasiSusunanFormula.Visible = True
        DGV_SusunanFormula.Rows.Clear()
        DGV_DetailSusunanFormula.Rows.Clear()
        PNL_FormulaAktif.Visible = True
        LB_FormulaAktif.Visible = True

        Load_SusunanFormula()
    End Sub

    Private Sub BTN_BuatSusunanFormula_Click(sender As Object, e As EventArgs) Handles BTN_BuatSusunanFormula.Click
        GB_BuatSusunanFormula.Visible = True
        GB_InformasiSusunanFormula.Visible = False
        DGV_FormulaTerbinding.Rows.Clear()
        DGV_FormulaTersedia.Rows.Clear()
        PNL_FormulaAktif.Visible = False
        LB_FormulaAktif.Visible = False

        Load_BuatSusunanFormula()
    End Sub

    Private Sub NonaktifkanFormulaDefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NonaktifkanFormulaDefaultToolStripMenuItem.Click
        If DGV_DetailSusunanFormula.CurrentRow Is Nothing Then Exit Sub

        Dim noFormula As String = DGV_DetailSusunanFormula.CurrentRow.Cells(0).Value.ToString()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = $"Select * From N_EMI_Transaksi_Formulator_Binding_Detail WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Prioritas = 1 AND No_Faktur = '{selectedFakturSusunan}' AND No_Formulator = '{noFormula}'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Formula ini bukan FORMULA UTAMA untuk faktur susunan '{selectedFakturSusunan}'", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = $"UPDATE N_EMI_Transaksi_Formulator_Binding_Detail SET Flag_Invalid = 'Y' WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Prioritas = 1 AND No_Faktur = '{selectedFakturSusunan}' AND No_Formulator = '{noFormula}'"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            DGV_DetailSusunanFormula.Rows(0).DefaultCellStyle.BackColor = Color.White

            MessageBox.Show($"Berhasil menonaktifkan formula default untuk faktur susunan '{selectedFakturSusunan}'", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show($"Gagal menonaktifkan formula default untuk faktur susunan '{selectedFakturSusunan}': " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub
End Class