Imports System.Data.SqlClient

Public Class N_EMI_Master_Bundle_Packing_Set
    Private IsFillBarang As Boolean = False
    Private IsFillPackingSet As Boolean = False

    Dim LV_CariBarang_Show_Point As New Point(116, 115)
    Dim LV_CariPackingSet_Show_Point As New Point(410, 176)

    Private Sub Me_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub TB_KodeBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles TB_KodeBarang.KeyDown
        If Not LV_CariBarang.Visible Then Exit Sub
        If LV_CariBarang.Items.Count = 0 Then Exit Sub

        Select Case e.KeyCode
            Case Keys.Down
                e.SuppressKeyPress = True

                Dim Index As Integer = 0

                If LV_CariBarang.SelectedItems.Count > 0 Then
                    Index = LV_CariBarang.SelectedItems(0).Index + 1
                End If

                If Index >= LV_CariBarang.Items.Count Then
                    Index = LV_CariBarang.Items.Count - 1
                End If

                LV_CariBarang.SelectedItems.Clear()
                LV_CariBarang.Items(Index).Selected = True
                LV_CariBarang.Items(Index).Focused = True
                LV_CariBarang.EnsureVisible(Index)
            Case Keys.Up
                e.SuppressKeyPress = True

                Dim Index As Integer = 0

                If LV_CariBarang.SelectedItems.Count > 0 Then
                    Index = LV_CariBarang.SelectedItems(0).Index - 1
                End If

                If Index < 0 Then
                    Index = 0
                End If

                LV_CariBarang.SelectedItems.Clear()
                LV_CariBarang.Items(Index).Selected = True
                LV_CariBarang.Items(Index).Focused = True
                LV_CariBarang.EnsureVisible(Index)
            Case Keys.Enter
                e.SuppressKeyPress = True

                If LV_CariBarang.SelectedItems.Count > 0 Then
                    PilihBarang()
                End If
            Case Keys.Escape
                e.SuppressKeyPress = True
                LV_CariBarang.Visible = False
        End Select
    End Sub

    Private Sub TB_KodeBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TB_KodeBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            CMB_PackingSet.Focus()
        End If

        If e.KeyChar = "'" Then
            e.KeyChar = Chr(0)
        End If
    End Sub

    Private Sub TB_KodeBarang_TextChanged(sender As Object, e As EventArgs) Handles TB_KodeBarang.TextChanged
        If IsFillBarang Then Exit Sub

        Try
            Dim Keyword As String = TB_KodeBarang.Text.Trim

            If Keyword = "" Then
                LV_CariBarang.Visible = False
                LV_CariBarang.Items.Clear()
                Exit Sub
            End If

            If Keyword.Length < 1 Then
                LV_CariBarang.Visible = False
                LV_CariBarang.Items.Clear()
                Exit Sub
            End If

            OpenConn()

            LV_CariBarang.Items.Clear()

            SQL = $"
                SELECT TOP(50) kode_barang, kode_barang_inq, nama, satuan
                FROM barang
                WHERE kode_perusahaan = '{KodePerusahaan}'
                AND nama LIKE '%{Keyword.Replace("'", "''")}%' or kode_barang LIKE '%{Keyword.Replace("'", "''")}%' or kode_barang_inq LIKE '%{Keyword.Replace("'", "''")}%'
                GROUP BY kode_Barang, kode_barang_inq, nama, satuan ORDER BY kode_barang
            "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count = 0 Then
                        LV_CariBarang.Visible = False
                        CloseConn()
                        Exit Sub
                    End If

                    For i As Integer = 0 To .Rows.Count - 1

                        Dim NamaBarang As String =
                            If(General_Class.CekNULL(.Rows(i).Item("nama")) = "",
                               "-",
                               .Rows(i).Item("nama").ToString)

                        Dim Satuan As String =
                            If(General_Class.CekNULL(.Rows(i).Item("satuan")) = "",
                               "-",
                               .Rows(i).Item("satuan").ToString)

                        Dim LVI As ListViewItem

                        LVI = LV_CariBarang.Items.Add(.Rows(i).Item("kode_barang").ToString)
                        LVI.SubItems.Add(.Rows(i).Item("kode_barang_inq").ToString)
                        LVI.SubItems.Add(NamaBarang)
                        LVI.SubItems.Add(Satuan)
                    Next

                    If LV_CariBarang.Items.Count > 0 Then
                        LV_CariBarang.Items(0).Selected = True
                    End If
                End With
            End Using

            CloseConn()

            LV_CariBarang.Visible = (LV_CariBarang.Items.Count > 0)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal mencari data barang : " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub PilihBarang()
        Try
            If LV_CariBarang.SelectedItems.Count = 0 Then Exit Sub

            IsFillBarang = True

            TB_KodeBarang.Text = LV_CariBarang.SelectedItems(0).SubItems(0).Text
            TB_KodeBarangInq.Text = LV_CariBarang.SelectedItems(0).SubItems(1).Text
            TB_NamaBarang.Text = LV_CariBarang.SelectedItems(0).SubItems(2).Text

            LV_CariBarang.Visible = False

            LV_BundlePackingSet.SelectedItems.Clear()
            TB_Deskripsi.Clear()
            TB_IdPackingSet.Clear()

            Load_Bundle_Packing_Set()
            Load_Packing_Set()

        Catch ex As Exception
            MessageBox.Show("Gagal memilih barang : " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

        IsFillBarang = False
    End Sub

    Private Sub N_EMI_Master_Bundle_Packing_Set_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        LV_CariBarang.Visible = False
        LV_CariBarang.Location = LV_CariBarang_Show_Point
        LV_CariBarang.MultiSelect = False
        LV_CariBarang.FullRowSelect = True
    End Sub

    Private Sub LV_CariBarang_DoubleClick(sender As Object, e As EventArgs) Handles LV_CariBarang.DoubleClick
        PilihBarang()
    End Sub

    Private Sub Load_Bundle_Packing_Set()
        BTN_Simpan.Text = "&Simpan"
        DGV_ListPackingSet.Rows.Clear()
        TB_Deskripsi.Clear()

        Try
            OpenConn()

            SQL = $"
                SELECT a.Urut_Oto, a.Deskripsi, COUNT(b.Urut_Oto) AS total_packing_set FROM N_EMI_Bundle_Packing_Set a
                JOIN N_EMI_Bundle_Packing_Set_Detail b ON a.Urut_Oto = b.Id_Parent AND a.Kode_Perusahaan = b.Kode_Perusahaan
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}' AND a.Kode_Barang = '{TB_KodeBarangInq.Text.Trim}' AND a.Flag_Aktif = 'Y'
                GROUP BY a.Urut_Oto, a.Deskripsi
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    LV_BundlePackingSet.Items.Clear()

                    For i As Integer = 0 To .Rows.Count - 1
                        Dim LVI As ListViewItem
                        LVI = LV_BundlePackingSet.Items.Add(.Rows(i).Item("urut_oto").ToString)
                        LVI.SubItems.Add(.Rows(i).Item("deskripsi"))
                        LVI.SubItems.Add(.Rows(i).Item("total_packing_set"))
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal load data bundle packing set : " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub BTN_ClearBahan_Click(sender As Object, e As EventArgs) Handles BTN_ClearBahan.Click
        CMB_PackingSet.SelectedIndex = -1
        TB_IdPackingSet.Clear()
    End Sub

    Private Sub BTN_SimpanBahan_Click(sender As Object, e As EventArgs) Handles BTN_SimpanBahan.Click
        If TB_IdPackingSet.Text.Trim = "" Then
            MessageBox.Show("Packing set tidak valid.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CMB_PackingSet.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = $"
                select 
                    a.Urut_Oto as Id_Parent,
                    b.Urut_Oto as Id, 
                    a.Deskripsi, 
                    b.Jenis, 
                    b.Kode_Bahan
                from N_EMI_Detail_Barang_Detail_Bahan_Penolong a
                join N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail b 
                    on a.Kode_Perusahaan = b.Kode_Perusahaan 
                    and a.Urut_Oto = b.Id_Parent
                where a.urut_oto = '{TB_IdPackingSet.Text.Trim}'
                and a.Kode_Perusahaan = '{KodePerusahaan}'
            "

            Dim Dt As DataTable = BindingTrans(SQL).Tables("MyTable")

            ' =========================
            ' VALIDASI 1
            ' Tidak boleh ada kombinasi
            ' Kode_Bahan + Jenis yang sama
            ' di packing set yang akan masuk
            ' =========================
            Dim DuplicateInternal = Dt.AsEnumerable().
            GroupBy(Function(x) New With {
                Key .Kode_Bahan = x("Kode_Bahan").ToString.Trim,
                Key .Jenis = x("Jenis").ToString.Trim
            }).
            Any(Function(g) g.Count > 1)

            If DuplicateInternal Then
                CloseConn()
                MessageBox.Show(
                    "Terdapat kombinasi Kode Bahan dan Jenis yang sama pada packing set.",
                    Judul,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                Exit Sub
            End If

            ' =========================
            ' VALIDASI
            ' Tidak boleh masukkan
            ' packing set yang sudah ada
            ' =========================
            For Each DataBaru As DataRow In Dt.Rows
                Dim idBaru As String = DataBaru("Id").ToString.Trim

                For Each Row As DataGridViewRow In DGV_ListPackingSet.Rows
                    If Row.IsNewRow Then Continue For

                    Dim existingId As String = Row.Cells(0).Value.ToString.Trim

                    If existingId = idBaru Then

                        CloseConn()

                        MessageBox.Show(
                                $"Packing set '{DataBaru("Deskripsi")}' sudah ada di daftar.",
                                Judul,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            )
                        Exit Sub
                    End If
                Next
            Next

            ' =========================
            ' VALIDASI 3
            ' Jika ada Kode_Bahan sama
            ' tapi Jenis berbeda
            ' maka tidak boleh
            ' =========================
            For Each DataBaru As DataRow In Dt.Rows

                Dim KodeBahanBaru = DataBaru("Kode_Bahan").ToString.Trim
                Dim JenisBaru = DataBaru("Jenis").ToString.Trim

                For Each Row As DataGridViewRow In DGV_ListPackingSet.Rows
                    If Row.IsNewRow Then Continue For

                    Dim JenisExisting = Row.Cells(2).Value.ToString.Trim
                    Dim KodeBahanExisting = Row.Cells(4).Value.ToString.Trim

                    If KodeBahanBaru = KodeBahanExisting AndAlso JenisBaru <> JenisExisting Then
                        CloseConn()
                        MessageBox.Show(
                            $"Kode bahan '{KodeBahanBaru}' sudah ada dengan jenis berbeda.",
                            Judul,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        )
                        Exit Sub
                    End If
                Next
            Next

            ' =========================
            ' INSERT KE DGV
            ' =========================
            For Each Dr As DataRow In Dt.Rows
                DGV_ListPackingSet.Rows.Add(
                    Dr("Id"),
                    Dr("Deskripsi"),
                    Dr("Jenis"),
                    0,
                    Dr("Kode_Bahan"),
                    Dr("Id_Parent")
                )
            Next

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(
                "Gagal load data packing set : " & ex.Message,
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            Exit Sub
        End Try

        CMB_PackingSet.SelectedIndex = -1
        TB_IdPackingSet.Clear()
        CMB_PackingSet.Focus()
    End Sub

    Private Sub BTN_Simpan_Click(sender As Object, e As EventArgs) Handles BTN_Simpan.Click

        get_jam()

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            ' =========================
            ' VALIDASI HEADER
            ' =========================
            If TB_Deskripsi.Text.Trim = "" Then
                CloseTrans()
                CloseConn()

                MessageBox.Show("Deskripsi tidak boleh kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                TB_Deskripsi.Focus()
                Exit Sub
            End If

            If DGV_ListPackingSet.Rows.Count = 0 Then
                CloseTrans()
                CloseConn()

                MessageBox.Show("List packing set kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' =========================
            ' VALIDASI KONFLIK
            ' KODE BAHAN & JENIS
            ' =========================
            Dim dict As New Dictionary(Of String, String)

            For Each Row As DataGridViewRow In DGV_ListPackingSet.Rows

                If Row.IsNewRow Then Continue For

                Dim kodeBahan As String = Row.Cells(4).Value.ToString.Trim
                Dim jenis As String = Row.Cells(2).Value.ToString.Trim

                If dict.ContainsKey(kodeBahan) Then

                    If dict(kodeBahan) <> jenis Then

                        CloseTrans()
                        CloseConn()

                        MessageBox.Show(
                            $"Kode bahan '{kodeBahan}' memiliki jenis berbeda.",
                            Judul,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        )

                        Exit Sub
                    End If

                Else
                    dict.Add(kodeBahan, jenis)
                End If

            Next

            Dim idParent As String = ""
            Dim isEdit As Boolean = LV_BundlePackingSet.SelectedItems.Count > 0

            ' =========================
            ' CEK DUPLIKAT DESKRIPSI
            ' =========================
            If isEdit Then

                idParent = LV_BundlePackingSet.SelectedItems(0).SubItems(0).Text

                SQL = $"
                    SELECT urut_oto
                    FROM N_EMI_Bundle_Packing_Set
                    WHERE
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{TB_KodeBarangInq.Text.Trim}'
                        AND Deskripsi = '{TB_Deskripsi.Text.Trim}'
                        AND urut_oto <> '{idParent}'
                "

            Else

                SQL = $"
                    SELECT urut_oto
                    FROM N_EMI_Bundle_Packing_Set
                    WHERE
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{TB_KodeBarangInq.Text.Trim}'
                        AND Deskripsi = '{TB_Deskripsi.Text.Trim}'
                "

            End If

            Using Dr = OpenTrans(SQL)

                If Dr.Read Then

                    Dr.Close()

                    CloseTrans()
                    CloseConn()

                    MessageBox.Show(
                        "Deskripsi sudah dipakai.",
                        Judul,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )

                    Exit Sub

                End If

            End Using

            ' =========================
            ' UPDATE
            ' =========================
            If isEdit Then

                idParent = LV_BundlePackingSet.SelectedItems(0).SubItems(0).Text

                Dim urutLog As Integer = 1

                SQL = $"
                    SELECT ISNULL(MAX(Urut_Log), 0) + 1 AS urut_log
                    FROM N_EMI_Bundle_Packing_Set_Detail_Log
                    WHERE Id_Parent = '{idParent}'
                "

                Using Dr = OpenTrans(SQL)

                    If Dr.Read Then
                        urutLog = Val(Dr("urut_log"))
                    End If

                End Using

                ' =========================
                ' BACKUP DETAIL
                ' =========================
                SQL = $"
                    INSERT INTO N_EMI_Bundle_Packing_Set_Detail_Log
                    (
                        Urut_Log,
                        Id_Parent,
                        Id_Packing_Set,
                        Persentase,
                        Jenis,
                        Tanggal,
                        Jam,
                        UserID,
                        Kode_Perusahaan
                    )
                    SELECT
                        '{urutLog}',
                        Id_Parent,
                        Id_Packing_Set,
                        Persentase,
                        Jenis,
                        '{Format(tgl_skg, "yyyy-MM-dd")}',
                        '{Format(tgl_skg, "HH:mm:ss")}',
                        '{UserID}',
                        Kode_Perusahaan
                    FROM N_EMI_Bundle_Packing_Set_Detail
                    WHERE Id_Parent = '{idParent}'
                "

                ExecuteTrans(SQL)

                ' =========================
                ' UPDATE HEADER
                ' =========================
                SQL = $"
                    UPDATE N_EMI_Bundle_Packing_Set
                    SET
                        Deskripsi = '{TB_Deskripsi.Text.Trim}'
                    WHERE urut_oto = '{idParent}'
                "

                ExecuteTrans(SQL)

                ' =========================
                ' HAPUS DETAIL LAMA
                ' =========================
                SQL = $"
                    DELETE FROM N_EMI_Bundle_Packing_Set_Detail
                    WHERE Id_Parent = '{idParent}'
                "

                ExecuteTrans(SQL)

            Else

                ' =========================
                ' INSERT HEADER
                ' =========================
                SQL = $"
                    INSERT INTO N_EMI_Bundle_Packing_Set
                    (
                        Kode_Barang,
                        Deskripsi,
                        Tanggal,
                        Jam,
                        UserID,
                        Kode_Perusahaan,
                        Flag_Aktif
                    )
                    VALUES
                    (
                        '{TB_KodeBarangInq.Text.Trim}',
                        '{TB_Deskripsi.Text.Trim}',
                        '{Format(tgl_skg, "yyyy-MM-dd")}',
                        '{Format(tgl_skg, "HH:mm:ss")}',
                        '{UserID}',
                        '{KodePerusahaan}',
                        'Y'
                    )
                "

                ExecuteTrans(SQL)

                SQL = $"
                    SELECT TOP 1 urut_oto
                    FROM N_EMI_Bundle_Packing_Set
                    WHERE
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{TB_KodeBarangInq.Text.Trim}'
                    ORDER BY urut_oto DESC
                "

                Using Dr = OpenTrans(SQL)

                    If Dr.Read Then
                        idParent = Dr("urut_oto").ToString
                    End If

                End Using

            End If

            ' =========================
            ' VALIDASI PERSENTASE
            ' =========================
            For Each Row As DataGridViewRow In DGV_ListPackingSet.Rows

                If Row.IsNewRow Then Continue For

                Dim persen As Double = Val(Row.Cells(3).Value)

                If persen < 0 Then

                    CloseTrans()
                    CloseConn()

                    MessageBox.Show(
                        "Persentase tidak boleh angka negatif",
                        Judul,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )

                    DGV_ListPackingSet.CurrentCell = Row.Cells(3)

                    Exit Sub
                End If
            Next

            ' =========================
            ' INSERT DETAIL DARI DGV
            ' =========================
            For Each Row As DataGridViewRow In DGV_ListPackingSet.Rows

                If Row.IsNewRow Then Continue For

                SQL = $"
                    INSERT INTO N_EMI_Bundle_Packing_Set_Detail
                    (
                        Id_Parent,
                        Id_Packing_Set,
                        Persentase,
                        Jenis,
                        Tanggal,
                        Jam,
                        UserID,
                        Kode_Perusahaan
                    )
                    VALUES
                    (
                        '{idParent}',
                        '{Row.Cells(5).Value}',
                        '{Val(Row.Cells(3).Value)}',
                        '{Row.Cells(2).Value}',
                        '{Format(tgl_skg, "yyyy-MM-dd")}',
                        '{Format(tgl_skg, "HH:mm:ss")}',
                        '{UserID}',
                        '{KodePerusahaan}'
                    )
                "

                ExecuteTrans(SQL)

            Next

            Cmd.Transaction.Commit()

            CloseTrans()
            CloseConn()

            MessageBox.Show(
                "Data berhasil disimpan.",
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

            LV_BundlePackingSet.Items.Clear()
            DGV_ListPackingSet.Rows.Clear()
            TB_Deskripsi.Clear()
            CMB_PackingSet.SelectedIndex = -1
            TB_IdPackingSet.Clear()

            Load_Bundle_Packing_Set()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub BTN_Refresh_Click(sender As Object, e As EventArgs) Handles BTN_Refresh.Click
        BTN_Simpan.Text = "&Simpan"
        DGV_ListPackingSet.Rows.Clear()

        LV_BundlePackingSet.SelectedItems.Clear()
        TB_Deskripsi.Clear()

        CMB_PackingSet.SelectedIndex = -1
        TB_IdPackingSet.Clear()
    End Sub

    Private Sub LV_PackingSet_DoubleClick(sender As Object, e As EventArgs) Handles LV_BundlePackingSet.DoubleClick
        If LV_BundlePackingSet.SelectedItems.Count = 0 Then Exit Sub

        TB_Deskripsi.Text = LV_BundlePackingSet.SelectedItems(0).SubItems(1).Text
        BTN_Simpan.Text = "&Update"
        DGV_ListPackingSet.Rows.Clear()

        Try
            OpenConn()

            SQL = $"
                SELECT 
                    c.Urut_Oto as ID_Parent,
                    d.Urut_Oto AS ID,
                    c.Deskripsi AS Packing_Set,
                    b.Jenis,
                    b.Persentase,
                    d.Kode_Bahan
                FROM N_EMI_Bundle_Packing_Set a

                JOIN N_EMI_Bundle_Packing_Set_Detail b 
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan 
                    AND a.Urut_Oto = b.Id_Parent

                JOIN N_EMI_Detail_Barang_Detail_Bahan_Penolong c 
                    ON b.Kode_Perusahaan = c.Kode_Perusahaan 
                    AND b.Id_Packing_Set = c.Urut_Oto

                JOIN N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail d
                    ON c.Kode_Perusahaan = d.Kode_Perusahaan
                    AND c.Urut_Oto = d.Id_Parent
                    AND d.Jenis = b.Jenis

                WHERE 
                    a.Kode_Perusahaan = '{KodePerusahaan}'
                    AND a.Urut_Oto = '{LV_BundlePackingSet.SelectedItems(0).SubItems(0).Text}'

                ORDER BY 
                    c.Deskripsi
            "

            Dim Ds As DataSet = BindingTrans(SQL)

            With Ds.Tables("MyTable")
                If .Rows.Count > 0 Then
                    For i As Integer = 0 To .Rows.Count - 1
                        DGV_ListPackingSet.Rows.Add(
                            .Rows(i)("ID"),
                            .Rows(i)("Packing_Set"),
                            .Rows(i)("Jenis"),
                            .Rows(i)("Persentase"),
                            .Rows(i)("Kode_Bahan"),
                            .Rows(i)("ID_Parent")
                        )
                    Next
                End If
            End With

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(
                $"Gagal load list packing set {LV_BundlePackingSet.SelectedItems(0).SubItems(1).Text}: " & ex.Message,
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            Exit Sub
        End Try

    End Sub

    Private Sub BatalkanPackingDefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanPackingDefaultToolStripMenuItem.Click
        If LV_BundlePackingSet.SelectedItems.Count = 0 Then Exit Sub

        Dim idParent As String = LV_BundlePackingSet.SelectedItems(0).SubItems(0).Text

        get_jam()
        Try

            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            ' BATALKAN DEFAULT
            SQL = $"
                UPDATE N_EMI_Bundle_Packing_Set
                SET
                    flag_aktif = NULL
                WHERE urut_oto = '{idParent}'
            "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            MessageBox.Show("Berhasil menonaktifkan bundle packing set", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Load_Bundle_Packing_Set()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HapusBahanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusBahanToolStripMenuItem.Click
        If DGV_ListPackingSet.CurrentRow Is Nothing Then Exit Sub

        Dim packingSet As String = DGV_ListPackingSet.CurrentRow.Cells(1).Value.ToString.Trim

        If packingSet = "" Then Exit Sub

        If MessageBox.Show(
            $"Hapus semua data packing set '{packingSet}' ?",
            Judul,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        ) = DialogResult.No Then
            Exit Sub
        End If

        For i As Integer = DGV_ListPackingSet.Rows.Count - 1 To 0 Step -1
            If DGV_ListPackingSet.Rows(i).IsNewRow Then Continue For
            Dim packingSetRow As String = DGV_ListPackingSet.Rows(i).Cells(1).Value.ToString.Trim
            If packingSetRow = packingSet Then
                DGV_ListPackingSet.Rows.RemoveAt(i)
            End If
        Next
    End Sub

    Private Sub Load_Packing_Set()
        CMB_PackingSet.Items.Clear()

        Try
            OpenConn()

            SQL = $"
                SELECT Deskripsi FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong
                WHERE Kode_Barang = '{TB_KodeBarangInq.Text.Trim}' AND Kode_Perusahaan = '{KodePerusahaan}' AND Status IS NULL AND Flag_Aktif = 'Y'
                ORDER BY Deskripsi DESC
            "
            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    CMB_PackingSet.Items.Add(Dr("Deskripsi").ToString())
                End While
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal load packing set: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub CMB_PackingSet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMB_PackingSet.SelectedIndexChanged
        Try
            OpenConn()

            SQL = $"
                SELECT Urut_Oto FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong
                WHERE Kode_Barang = '{TB_KodeBarangInq.Text.Trim}' AND Kode_Perusahaan = '{KodePerusahaan}' AND Status IS NULL AND Flag_Aktif = 'Y' AND Deskripsi = '{CMB_PackingSet.Text.Trim}'
                ORDER BY Deskripsi DESC
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    TB_IdPackingSet.Text = Dr("Urut_Oto").ToString()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal load packing set: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub
End Class