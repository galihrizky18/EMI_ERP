Public Class N_EMI_Master_Packing
    Private IsFillBarang As Boolean = False
    Private IsFillBahan As Boolean = False

    Dim LV_CariBarang_Show_Point As New Point(116, 115)
    Dim LV_CariBahan_Show_Point As New Point(360, 175)

    Private Sub Me_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub N_EMI_Master_Packing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        LV_CariBarang.Visible = False
        LV_CariBarang.Location = LV_CariBarang_Show_Point
        LV_CariBarang.MultiSelect = False
        LV_CariBarang.FullRowSelect = True

        LV_CariBahan.Visible = False
        LV_CariBahan.Location = LV_CariBahan_Show_Point
        LV_CariBahan.MultiSelect = False
        LV_CariBahan.FullRowSelect = True

        Try
            OpenConn()

            CMB_Lokasi.Items.Clear()
            SQL = $"
                SELECT Kode_Stock_Owner_Gudang
                FROM binding_lokasi_gudang
                WHERE 
                    gudang_default = 'Y'
                    AND kode_stock_owner = '{Lokasi}'
                ORDER BY Kode_Stock_Owner_Gudang
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CMB_Lokasi.Items.Add(Dr("Kode_Stock_Owner_Gudang"))
                Loop
            End Using

            CMB_Lokasi.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal load lokasi: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
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
                AND (nama LIKE '%{Keyword.Replace("'", "''")}%' or kode_barang LIKE '%{Keyword.Replace("'", "''")}%' or kode_barang_inq LIKE '%{Keyword.Replace("'", "''")}%')
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

    Private Sub LV_CariBarang_DoubleClick(sender As Object, e As EventArgs) Handles LV_CariBarang.DoubleClick
        PilihBarang()
    End Sub

    Private Sub TB_KodeBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TB_KodeBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            TB_KodeBahan.Focus()
        End If

        If e.KeyChar = "'" Then
            e.KeyChar = Chr(0)
        End If
    End Sub

    Private Sub PilihBarang()
        Try
            If LV_CariBarang.SelectedItems.Count = 0 Then Exit Sub

            IsFillBarang = True

            TB_KodeBarang.Text = LV_CariBarang.SelectedItems(0).SubItems(0).Text
            TB_KodeBarangInq.Text = LV_CariBarang.SelectedItems(0).SubItems(1).Text
            TB_NamaBarang.Text = LV_CariBarang.SelectedItems(0).SubItems(2).Text

            LV_CariBarang.Visible = False

            TB_KodeBahan.Focus()

            Load_Packing_Set()

            LV_ListBahan.Items.Clear()
            LV_PackingSet.SelectedItems.Clear()
            TB_Deskripsi.Clear()
            CB_Default.Checked = False

            TB_KodeBahan.Clear()
            TB_NamaBahan.Clear()
            TB_QtyBarang.Clear()
            TB_QtyBahan.Clear()
            CMB_Level.SelectedIndex = -1
            CMB_Jenis.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show("Gagal memilih barang : " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

        IsFillBarang = False
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

    Private Sub Load_Packing_Set()
        Try
            OpenConn()

            SQL = $"
                SELECT a.urut_oto, a.deskripsi, a.flag_default, COUNT(b.Urut_Oto) AS total_bahan FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong a
                JOIN N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Urut_Oto = b.Id_Parent
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}' AND a.Kode_Barang = '{TB_KodeBarangInq.Text.Trim}' AND a.Flag_Aktif = 'Y'
                GROUP BY a.urut_oto, a.deskripsi, a.flag_default
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    LV_PackingSet.Items.Clear()

                    For i As Integer = 0 To .Rows.Count - 1
                        Dim LVI As ListViewItem
                        LVI = LV_PackingSet.Items.Add(.Rows(i).Item("urut_oto").ToString)
                        LVI.SubItems.Add(.Rows(i).Item("deskripsi"))
                        LVI.SubItems.Add(.Rows(i).Item("total_bahan"))
                        LVI.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("flag_default")) = "", "", .Rows(i).Item("flag_default")))

                        If .Rows(i).Item("flag_default").ToString.Trim.ToUpper = "Y" Then
                            LVI.BackColor = Color.LightGreen
                        End If
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Gagal load packing set untuk kode barang {TB_KodeBarang.Text.Trim}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub TB_KodeBahan_TextChanged(sender As Object, e As EventArgs) Handles TB_KodeBahan.TextChanged
        If IsFillBahan Then Exit Sub

        Try
            Dim Keyword As String = TB_KodeBahan.Text.Trim

            If Keyword = "" Then
                LV_CariBahan.Visible = False
                LV_CariBahan.Items.Clear()
                Exit Sub
            End If

            If Keyword.Length < 1 Then
                LV_CariBahan.Visible = False
                LV_CariBahan.Items.Clear()
                Exit Sub
            End If

            OpenConn()

            LV_CariBahan.Items.Clear()

            SQL = $"
                SELECT 
                    kode_barang, 
                    nama, 
                    satuan
                FROM Barang a, EMI_Group_Jenis b
                WHERE 
                    a.Kode_Perusahaan = b.Kode_Perusahaan 
                    AND a.Id_Group_Jenis = b.Id_Group_Jenis
                    AND Flag_Packaging = 'Y'
                    AND Kode_Stock_Owner = '{CMB_Lokasi.SelectedItem.ToString}'
                    AND (nama LIKE '%{TB_KodeBahan.Text.Trim}%' OR kode_barang LIKE '%{TB_KodeBahan.Text.Trim}%')
                GROUP BY 
                    kode_barang, 
                    nama, 
                    satuan
                ORDER BY 
                    nama
            "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim LVI As ListViewItem
                        LVI = LV_CariBahan.Items.Add(.Rows(i).Item("kode_barang").ToString)
                        LVI.SubItems.Add(.Rows(i).Item("nama").ToString)
                        LVI.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("satuan").ToString) = "", "-", .Rows(i).Item("satuan").ToString))
                    Next

                    If LV_CariBahan.Items.Count > 0 Then
                        LV_CariBahan.Items(0).Selected = True
                    End If
                End With
            End Using

            CloseConn()

            LV_CariBahan.Visible = (LV_CariBahan.Items.Count > 0)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Gagal mencari data bahan : " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub TB_KodeBahan_KeyDown(sender As Object, e As KeyEventArgs) Handles TB_KodeBahan.KeyDown
        If Not LV_CariBahan.Visible Then Exit Sub
        If LV_CariBahan.Items.Count = 0 Then Exit Sub

        Select Case e.KeyCode
            Case Keys.Down
                e.SuppressKeyPress = True

                Dim Index As Integer = 0

                If LV_CariBahan.SelectedItems.Count > 0 Then
                    Index = LV_CariBahan.SelectedItems(0).Index + 1
                End If

                If Index >= LV_CariBahan.Items.Count Then
                    Index = LV_CariBahan.Items.Count - 1
                End If

                LV_CariBahan.SelectedItems.Clear()
                LV_CariBahan.Items(Index).Selected = True
                LV_CariBahan.Items(Index).Focused = True
                LV_CariBahan.EnsureVisible(Index)
            Case Keys.Up
                e.SuppressKeyPress = True

                Dim Index As Integer = 0

                If LV_CariBahan.SelectedItems.Count > 0 Then
                    Index = LV_CariBahan.SelectedItems(0).Index - 1
                End If

                If Index < 0 Then
                    Index = 0
                End If

                LV_CariBahan.SelectedItems.Clear()
                LV_CariBahan.Items(Index).Selected = True
                LV_CariBahan.Items(Index).Focused = True
                LV_CariBahan.EnsureVisible(Index)
            Case Keys.Enter
                e.SuppressKeyPress = True

                If LV_CariBahan.SelectedItems.Count > 0 Then
                    PilihBahan()
                End If
            Case Keys.Escape
                e.SuppressKeyPress = True
                LV_CariBahan.Visible = False
        End Select
    End Sub

    Private Sub TB_KodeBahan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TB_KodeBahan.KeyPress
        If e.KeyChar = Chr(13) Then
            TB_QtyBahan.Focus()
        End If

        If e.KeyChar = "'" Then
            e.KeyChar = Chr(0)
        End If
    End Sub

    Private Sub PilihBahan()
        Try
            If LV_CariBahan.SelectedItems.Count = 0 Then Exit Sub

            IsFillBahan = True

            TB_KodeBahan.Text = LV_CariBahan.SelectedItems(0).SubItems(0).Text
            TB_NamaBahan.Text = LV_CariBahan.SelectedItems(0).SubItems(1).Text

            LV_CariBahan.Visible = False

            TB_QtyBarang.Focus()
        Catch ex As Exception
            MessageBox.Show("Gagal memilih bahan : " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

        IsFillBahan = False
    End Sub

    Private Sub LV_CariBahan_DoubleClick(sender As Object, e As EventArgs) Handles LV_CariBahan.DoubleClick
        PilihBahan()
    End Sub

    Private Sub LV_PackingSet_DoubleClick(sender As Object, e As EventArgs) Handles LV_PackingSet.DoubleClick
        If LV_PackingSet.SelectedItems.Count = 0 Then Exit Sub

        TB_Deskripsi.Text = LV_PackingSet.SelectedItems(0).SubItems(1).Text
        CB_Default.Checked = (LV_PackingSet.SelectedItems(0).SubItems(3).Text = "Y")

        BTN_Simpan.Text = "&Update"

        Try
            OpenConn()

            SQL = $"
                SELECT 
                    a.Jumlah_Barang AS qty_barang,
                    a.kode_bahan,

                    (
                        SELECT b2.nama
                        FROM barang b2, EMI_Group_Jenis egj
                        WHERE 
                            b2.kode_perusahaan = egj.Kode_Perusahaan
                            AND b2.Id_Group_Jenis = egj.Id_Group_Jenis
                            AND b2.Kode_Barang = a.kode_bahan
                            AND egj.Flag_Packaging = 'Y'
                            AND b2.Kode_Stock_Owner = '{CMB_Lokasi.SelectedItem.ToString}'
                    ) AS Nama_Bahan,

                    a.jumlah_bahan AS qty_bahan,
                    a.level,
                    a.jenis

                FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail a

                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
                    AND a.Id_Parent = '{LV_PackingSet.SelectedItems(0).SubItems(0).Text}'
                ORDER BY 
                    a.jenis,
                    a.level
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    LV_ListBahan.Items.Clear()

                    For i As Integer = 0 To .Rows.Count - 1
                        Dim LVI As ListViewItem
                        LVI = LV_ListBahan.Items.Add(.Rows(i).Item("kode_bahan").ToString)
                        LVI.SubItems.Add(.Rows(i).Item("nama_bahan").ToString)
                        LVI.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("qty_barang")) = "", 0, Format(.Rows(i).Item("qty_barang"), "N0")))
                        LVI.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("qty_bahan")) = "", 0, Format(.Rows(i).Item("qty_bahan"), "N0")))
                        LVI.SubItems.Add(.Rows(i).Item("level").ToString)
                        LVI.SubItems.Add(.Rows(i).Item("jenis").ToString)
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Gagal load list bahan untuk packing set {LV_PackingSet.SelectedItems(0).SubItems(1).Text}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub BTN_ClearBahan_Click(sender As Object, e As EventArgs) Handles BTN_ClearBahan.Click
        TB_KodeBahan.Clear()
        TB_NamaBahan.Clear()
        TB_QtyBarang.Clear()
        TB_QtyBahan.Clear()
        CMB_Level.SelectedIndex = -1
        CMB_Jenis.SelectedIndex = -1
    End Sub

    Private Sub BTN_Refresh_Click(sender As Object, e As EventArgs) Handles BTN_Refresh_Input_Bahan.Click
        BTN_Simpan.Text = "&Simpan"

        LV_PackingSet.SelectedItems.Clear()

        TB_KodeBahan.Clear()
        TB_NamaBahan.Clear()
        TB_QtyBarang.Clear()
        TB_QtyBahan.Clear()
        CMB_Level.SelectedIndex = -1
        CMB_Jenis.SelectedIndex = -1

        LV_ListBahan.Items.Clear()
        TB_Deskripsi.Clear()
        CB_Default.Checked = False
    End Sub

    Private Sub BTN_Simpan_Click(sender As Object, e As EventArgs) Handles BTN_Simpan.Click
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If TB_Deskripsi.Text.Trim = "" Then
                CloseTrans()
                CloseConn()

                MessageBox.Show("Deskripsi tidak boleh kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TB_Deskripsi.Focus()
                Exit Sub
            End If

            If LV_ListBahan.Items.Count = 0 Then
                CloseTrans()
                CloseConn()

                MessageBox.Show("Detail bahan kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim idParent As String = ""
            Dim flagDefault As String = If(CB_Default.Checked, "Y", "")
            Dim isEdit As Boolean = LV_PackingSet.SelectedItems.Count > 0

            ' =========================
            ' CEK DUPLIKAT DESKRIPSI
            ' =========================
            If isEdit Then
                idParent = LV_PackingSet.SelectedItems(0).SubItems(0).Text

                SQL = $"
                    SELECT urut_oto
                    FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong
                    WHERE
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{TB_KodeBarangInq.Text.Trim}'
                        AND deskripsi = '{TB_Deskripsi.Text.Trim}'
                        AND urut_oto <> '{idParent}'
                "
            Else
                SQL = $"
                    SELECT urut_oto
                    FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong
                    WHERE
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{TB_KodeBarangInq.Text.Trim}'
                        AND deskripsi = '{TB_Deskripsi.Text.Trim}'
                "
            End If

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()

                    MessageBox.Show("Deskripsi sudah dipakai.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

            ' =========================
            ' UPDATE
            ' =========================
            If isEdit Then
                idParent = LV_PackingSet.SelectedItems(0).SubItems(0).Text

                Dim flagDefaultLama As String = ""

                SQL = $"
                    SELECT flag_default
                    FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong
                    WHERE urut_oto = '{idParent}'
                "

                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        flagDefaultLama = Dr("flag_default").ToString.Trim.ToUpper
                    End If
                End Using

                ' LOG DEFAULT
                If flagDefaultLama <> flagDefault Then
                    Dim Id_Default_Sebelum As String = "NULL"
                    If flagDefaultLama = "Y" Then
                        Id_Default_Sebelum = idParent
                    End If

                    Dim Id_Default_Sesudah As String = "NULL"
                    If flagDefault = "Y" Then
                        Id_Default_Sesudah = idParent
                    End If

                    SQL = $"
                        INSERT INTO N_EMI_Detail_Barang_Detail_Bahan_Penolong_Log_Default
                        (
                            Id_Default_Sebelum,
                            Id_Default_Sesudah,
                            Tanggal,
                            Jam,
                            UserID,
                            Kode_Perusahaan
                        )
                        VALUES
                        (
                            {Id_Default_Sebelum},
                            {Id_Default_Sesudah},
                            '{Format(tgl_skg, "yyyy-MM-dd")}',
                            '{Format(tgl_skg, "HH:mm:ss")}',
                            '{UserID}',
                            '{KodePerusahaan}'
                        )
                    "
                    ExecuteTrans(SQL)
                End If

                Dim urutLog As Integer = 1

                SQL = $"
                    SELECT ISNULL(MAX(Urut_Log), 0) + 1 AS urut_log
                    FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail_Log
                    WHERE Id_Parent = '{idParent}'
                "

                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        urutLog = Val(Dr("urut_log"))
                    End If
                End Using

                ' BACKUP DETAIL
                SQL = $"
                    INSERT INTO N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail_Log
                    (
                        Id_Parent,
                        Urut_Log,
                        Kode_Bahan,
                        Jumlah_Barang,
                        Jumlah_Bahan,
                        Id_Kemasan,
                        Level,
                        Jenis,
                        Id_Stiker,
                        Jenis_Packing,
                        Tanggal,
                        Jam,
                        UserID,
                        Kode_Perusahaan
                    )
                    SELECT
                        Id_Parent,
                        '{urutLog}',
                        Kode_Bahan,
                        Jumlah_Barang,
                        Jumlah_Bahan,
                        Id_Kemasan,
                        Level,
                        Jenis,
                        Id_Stiker,
                        Jenis_Packing,
                        '{Format(tgl_skg, "yyyy-MM-dd")}',
                        '{Format(tgl_skg, "HH:mm:ss")}',
                        '{UserID}',
                        Kode_Perusahaan
                    FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail
                    WHERE Id_Parent = '{idParent}'
                "
                ExecuteTrans(SQL)

                ' UPDATE HEADER
                Dim flag_default As String = "NULL"
                If flagDefault <> "" Then
                    flag_default = $"'{flagDefault}'"
                End If
                SQL = $"
                    UPDATE N_EMI_Detail_Barang_Detail_Bahan_Penolong
                    SET
                        flag_default = {flag_default},
                        deskripsi = '{TB_Deskripsi.Text.Trim}',
                        userid = '{UserID}'
                    WHERE urut_oto = '{idParent}'
                "
                ExecuteTrans(SQL)

                ' HAPUS DETAIL LAMA
                SQL = $"
                    DELETE FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail
                    WHERE Id_Parent = '{idParent}'
                "
                ExecuteTrans(SQL)
            Else

                Dim totalPackingSet As Integer = 0

                SQL = $"
                    SELECT COUNT(urut_oto) AS total_data
                    FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong
                    WHERE
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{TB_KodeBarangInq.Text.Trim}'
                        AND Flag_Aktif = 'Y'
                "

                Using Dr = OpenTrans(SQL)

                    If Dr.Read Then
                        totalPackingSet = Val(Dr("total_data"))
                    End If

                End Using

                ' AUTO DEFAULT JIKA PERTAMA
                If totalPackingSet = 0 Then
                    flagDefault = "Y"
                    CB_Default.Checked = True
                End If

                Dim Flag_Default As String = "NULL"

                If flagDefault <> "" Then
                    Flag_Default = $"'{flagDefault}'"
                End If

                If Flag_Default <> "NULL" Then
                    SQL = $"
                        UPDATE N_EMI_Detail_Barang_Detail_Bahan_Penolong
                        SET flag_default = NULL
                        WHERE
                            Kode_Perusahaan = '{KodePerusahaan}'
                            AND Kode_Barang = '{TB_KodeBarangInq.Text.Trim}'
                    "
                    ExecuteTrans(SQL)
                End If

                ' =========================
                ' INSERT HEADER
                ' =========================
                SQL = $"
                    INSERT INTO N_EMI_Detail_Barang_Detail_Bahan_Penolong
                    (
                        Flag_Default,
                        Deskripsi,
                        Tanggal,
                        Jam,
                        UserID,
                        Kode_Barang,
                        Kode_Perusahaan,
                        Flag_Aktif
                    )
                    VALUES
                    (
                        {Flag_Default},
                        '{TB_Deskripsi.Text.Trim}',
                        '{Format(tgl_skg, "yyyy-MM-dd")}',
                        '{Format(tgl_skg, "HH:mm:ss")}',
                        '{UserID}',
                        '{TB_KodeBarangInq.Text.Trim}',
                        '{KodePerusahaan}',
                        'Y'
                    )
                "
                ExecuteTrans(SQL)

                SQL = $"
                    SELECT TOP 1 urut_oto
                    FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong
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
            ' UPDATE DEFAULT LAIN
            ' =========================
            If flagDefault = "Y" Then
                SQL = $"
                    UPDATE N_EMI_Detail_Barang_Detail_Bahan_Penolong
                    SET flag_default = NULL
                    WHERE
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{TB_KodeBarangInq.Text.Trim}'
                        AND urut_oto <> '{idParent}'
                "
                ExecuteTrans(SQL)

                SQL = "
                        DELETE FROM Barang_Detail_Bahan_Penolong
                        WHERE Flag_Default = 'Y'
                          AND Id_Parent IS NOT NULL
                "
                ExecuteTrans(SQL)
            End If

            ' =========================
            ' INSERT DETAIL
            ' =========================
            For Each item As ListViewItem In LV_ListBahan.Items
                SQL = $"
                    INSERT INTO N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail
                    (
                        Id_Parent,
                        Kode_Bahan,
                        Jumlah_Barang,
                        Jumlah_Bahan,
                        Id_Kemasan,
                        Level,
                        Jenis,
                        Id_Stiker,
                        Jenis_Packing,
                        Tanggal,
                        Jam,
                        UserID,
                        Kode_Perusahaan
                    )
                    VALUES
                    (
                        '{idParent}',
                        '{item.SubItems(0).Text}',
                        '{Val(item.SubItems(2).Text)}',
                        '{Val(item.SubItems(3).Text)}',
                        NULL,
                        '{Val(item.SubItems(4).Text)}',
                        '{item.SubItems(5).Text}',
                        NULL,
                        NULL,
                        '{Format(tgl_skg, "yyyy-MM-dd")}',
                        '{Format(tgl_skg, "HH:mm:ss")}',
                        '{UserID}',
                        '{KodePerusahaan}'
                    )
                "
                ExecuteTrans(SQL)

                If flagDefault = "Y" Then
                    SQL = $"
                        INSERT INTO Barang_Detail_Bahan_Penolong
                        (
                            Kode_Perusahaan,
                            Kode_Barang,
                            Kode_Bahan,
                            Jumlah_Barang,
                            Jumlah_Bahan,
                            Id_kemasan,
                            Level,
                            Jenis,
                            Id_Stiker,
                            Id_Parent,
                            Flag_Default,
                            Tanggal,
                            Jam,
                            UserID
                        )
                        VALUES
                        (
                            '{KodePerusahaan}',
                            '{TB_KodeBarangInq.Text.Trim}',
                            '{item.SubItems(0).Text}',
                            '{Val(item.SubItems(2).Text)}',
                            '{Val(item.SubItems(3).Text)}',
                            NULL,
                            '{Val(item.SubItems(4).Text)}',
                            '{item.SubItems(5).Text}',
                            NULL,
                            '{idParent}',
                            'Y',
                            '{Format(tgl_skg, "yyyy-MM-dd")}',
                            '{Format(tgl_skg, "HH:mm:ss")}',
                            '{UserID}'
                        )
                    "
                    ExecuteTrans(SQL)
                End If
            Next

            Cmd.Transaction.Commit()

            CloseTrans()
            CloseConn()

            MessageBox.Show("Data berhasil disimpan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

            LV_ListBahan.Items.Clear()
            LV_PackingSet.Items.Clear()

            TB_Deskripsi.Clear()

            CB_Default.Checked = False

            Load_Packing_Set()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BTN_SimpanBahan_Click(sender As Object, e As EventArgs) Handles BTN_SimpanBahan.Click
        If TB_KodeBahan.Text.Trim = "" Then
            MessageBox.Show("Kode bahan tidak boleh kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TB_KodeBahan.Focus()
            Exit Sub
        End If

        If TB_NamaBahan.Text.Trim = "" Then
            MessageBox.Show("Nama bahan tidak boleh kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TB_NamaBahan.Focus()
            Exit Sub
        End If

        If TB_QtyBarang.Text.Trim = "" Then
            MessageBox.Show("Qty barang tidak boleh kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TB_QtyBarang.Focus()
            Exit Sub
        End If

        If Not IsNumeric(TB_QtyBarang.Text) Then
            MessageBox.Show("Qty barang harus angka.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TB_QtyBarang.Focus()
            Exit Sub
        End If

        If TB_QtyBahan.Text.Trim = "" Then
            MessageBox.Show("Qty bahan tidak boleh kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TB_QtyBahan.Focus()
            Exit Sub
        End If

        If Not IsNumeric(TB_QtyBahan.Text) Then
            MessageBox.Show("Qty bahan harus angka.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TB_QtyBahan.Focus()
            Exit Sub
        End If

        If CMB_Jenis.SelectedIndex = -1 Then
            MessageBox.Show("Jenis tidak boleh kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CMB_Jenis.Focus()
            Exit Sub
        End If

        If CMB_Level.SelectedIndex = -1 Then
            MessageBox.Show("Level kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CMB_Level.Focus()
            Exit Sub
        End If

        For Each item As ListViewItem In LV_ListBahan.Items
            If item.SubItems(0).Text.Trim.ToUpper = TB_KodeBahan.Text.Trim.ToUpper Then
                MessageBox.Show("Bahan sudah ada di daftar.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Next

        Dim LVI As ListViewItem
        LVI = LV_ListBahan.Items.Add(TB_KodeBahan.Text.Trim)
        LVI.SubItems.Add(TB_NamaBahan.Text.Trim)
        LVI.SubItems.Add(TB_QtyBarang.Text.Trim)
        LVI.SubItems.Add(TB_QtyBahan.Text.Trim)
        LVI.SubItems.Add(CMB_Level.Text.Trim)
        LVI.SubItems.Add(CMB_Jenis.Text.Trim)

        TB_KodeBahan.Clear()
        TB_NamaBahan.Clear()
        TB_QtyBarang.Clear()
        TB_QtyBahan.Clear()
        CMB_Jenis.SelectedIndex = -1
        CMB_Level.SelectedIndex = -1
        TB_KodeBahan.Focus()
    End Sub

    Private Sub HapusBahanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusBahanToolStripMenuItem.Click
        If LV_ListBahan.SelectedItems.Count = 0 Then Exit Sub

        Dim jenis As String = LV_ListBahan.SelectedItems(0).SubItems(5).Text
        For i As Integer = LV_ListBahan.Items.Count - 1 To 0 Step -1
            If LV_ListBahan.Items(i).SubItems(5).Text = jenis Then
                LV_ListBahan.Items.RemoveAt(i)
            End If
        Next
    End Sub

    Private Sub JadikanPackingDefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JadikanPackingDefaultToolStripMenuItem.Click
        If LV_PackingSet.SelectedItems.Count = 0 Then Exit Sub

        Dim idParent As String = LV_PackingSet.SelectedItems(0).SubItems(0).Text

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim idDefaultSebelum As String = "NULL"

            ' CARI DEFAULT LAMA
            SQL = $"
                SELECT TOP 1 urut_oto
                FROM N_EMI_Detail_Barang_Detail_Bahan_Penolong
                WHERE
                    Kode_Perusahaan = '{KodePerusahaan}'
                    AND Kode_Barang = '{TB_KodeBarangInq.Text.Trim}'
                    AND flag_default = 'Y'
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    idDefaultSebelum = Dr("urut_oto").ToString
                End If
            End Using

            ' RESET SEMUA DEFAULT
            SQL = $"
                UPDATE N_EMI_Detail_Barang_Detail_Bahan_Penolong
                SET flag_default = NULL
                WHERE
                    Kode_Perusahaan = '{KodePerusahaan}'
                    AND Kode_Barang = '{TB_KodeBarangInq.Text.Trim}'
            "
            ExecuteTrans(SQL)

            ' SET DEFAULT BARU
            SQL = $"
                UPDATE N_EMI_Detail_Barang_Detail_Bahan_Penolong
                SET
                    flag_default = 'Y',
                    userid = '{UserID}'
                WHERE urut_oto = '{idParent}'
            "
            ExecuteTrans(SQL)

            ' INSERT LOG
            SQL = $"
                INSERT INTO N_EMI_Detail_Barang_Detail_Bahan_Penolong_Log_Default
                (
                    Id_Default_Sebelum,
                    Id_Default_Sesudah,
                    Tanggal,
                    Jam,
                    UserID,
                    Kode_Perusahaan
                )
                VALUES
                (
                    {idDefaultSebelum},
                    {idParent},
                    '{Format(tgl_skg, "yyyy-MM-dd")}',
                    '{Format(tgl_skg, "HH:mm:ss")}',
                    '{UserID}',
                    '{KodePerusahaan}'
                )
            "

            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()

            CloseTrans()
            CloseConn()

            MessageBox.Show("Berhasil mengubah default packing set", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Load_Packing_Set()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BatalkanPackingDefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanPackingDefaultToolStripMenuItem.Click
        If LV_PackingSet.SelectedItems.Count = 0 Then Exit Sub

        Dim idParent As String = LV_PackingSet.SelectedItems(0).SubItems(0).Text
        Dim flagDefault As String = LV_PackingSet.SelectedItems(0).SubItems(3).Text

        If flagDefault = "Y" Then
            MessageBox.Show("Tidak bisa menonaktifkan packing set default", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        get_jam()
        Try

            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            ' BATALKAN DEFAULT
            SQL = $"
                UPDATE N_EMI_Detail_Barang_Detail_Bahan_Penolong
                SET
                    flag_default = NULL,
                    flag_aktif = NULL
                WHERE urut_oto = '{idParent}'
            "
            ExecuteTrans(SQL)

            ' NONAKTIFKAN BUNDLE YANG MEMAKAI PACKING SET INI
            SQL = $"
                UPDATE a
                SET
                    a.Flag_Aktif = NULL
                FROM N_EMI_Bundle_Packing_Set a
                INNER JOIN N_EMI_Bundle_Packing_Set_Detail b
                    ON a.Urut_Oto = b.Id_Parent
                WHERE b.Id_Packing_Set = '{idParent}'
            "
            ExecuteTrans(SQL)

            ' LOG
            SQL = $"
                INSERT INTO N_EMI_Detail_Barang_Detail_Bahan_Penolong_Log_Default
                (
                    Id_Default_Sebelum,
                    Id_Default_Sesudah,
                    Tanggal,
                    Jam,
                    UserID,
                    Kode_Perusahaan
                )
                VALUES
                (
                    {idParent},
                    NULL,
                    '{Format(tgl_skg, "yyyy-MM-dd")}',
                    '{Format(tgl_skg, "HH:mm:ss")}',
                    '{UserID}',
                    '{KodePerusahaan}'
                )
            "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            MessageBox.Show("Berhasil menonaktifkan packing set", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Load_Packing_Set()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CMB_Jenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMB_Jenis.SelectedIndexChanged
        If CMB_Jenis.SelectedIndex = -1 Then Exit Sub

        Dim levelTerakhir As Integer = 0

        For Each item As ListViewItem In LV_ListBahan.Items

            Dim jenis As String = item.SubItems(5).Text.Trim

            If jenis.ToUpper = CMB_Jenis.Text.Trim.ToUpper Then

                Dim level As Integer = Val(item.SubItems(4).Text)

                If level > levelTerakhir Then
                    levelTerakhir = level
                End If

            End If

        Next

        Dim nextLevel As String = (levelTerakhir + 1).ToString

        If Not CMB_Level.Items.Contains(nextLevel) Then
            CMB_Level.Items.Add(nextLevel)
        End If

        CMB_Level.SelectedItem = nextLevel
    End Sub

    Private Sub BTN_Refresh_Click_1(sender As Object, e As EventArgs) Handles BTN_Refresh.Click
        BTN_Simpan.Text = "&Simpan"

        TB_KodeBarang.Clear()
        TB_NamaBarang.Clear()
        TB_KodeBarangInq.Clear()

        LV_PackingSet.SelectedItems.Clear()
        LV_PackingSet.Items.Clear()

        TB_KodeBahan.Clear()
        TB_NamaBahan.Clear()
        TB_QtyBarang.Clear()
        TB_QtyBahan.Clear()
        CMB_Level.SelectedIndex = -1
        CMB_Jenis.SelectedIndex = -1

        LV_ListBahan.Items.Clear()
        TB_Deskripsi.Clear()
        CB_Default.Checked = False
    End Sub
End Class