Public Class N_EMI_Formulator_Binding
    Private SelectedKodeBarang As String = ""
    Private Sub N_EMI_Formulator_Binding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With LvBarang
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .MultiSelect = False
            .HideSelection = False
            .Columns.Clear()

            .Columns.Add("Kode Barang", 100)
            .Columns.Add("Nama Barang", 300)
            .Columns.Add("Total Formula", 80, HorizontalAlignment.Center)
        End With

        With LvFormulaTersedia
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .MultiSelect = False
            .HideSelection = False
            .Columns.Clear()

            .Columns.Add("No Formula", 100)
            .Columns.Add("Tanggal Validasi", 100, HorizontalAlignment.Center)
            .Columns.Add("User Validasi", 100, HorizontalAlignment.Center)
        End With

        With LvBindingFormula
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .MultiSelect = False
            .HideSelection = False
            .Columns.Clear()

            .Columns.Add("No Formula", 100)
            .Columns.Add("Posisi", 100)
            .Columns.Add("Posisi Sebelumnya", 100)
            .Columns.Add("Keterangan", 250)
        End With

        RbAktif.Checked = True

        Fetch_LvBarang()
    End Sub

    Private Sub Fetch_LvBarang()

        Try
            OpenConn()

            Dim filterBinding As String = ""
            Dim filterCari As String = ""

            If RbSudahBinding.Checked Then
                filterBinding = "AND B.Kode_Barang IS NOT NULL"
            ElseIf RbBelumBinding.Checked Then
                filterBinding = "AND B.Kode_Barang IS NULL"
            End If

            If TxtCariBarang.Text.Trim <> "" Then
                filterCari = $"AND (Brg.Kode_Barang_Inq LIKE '%{TxtCariBarang.Text.Trim}%' 
                   OR Brg.Nama_Inq LIKE '%{TxtCariBarang.Text.Trim}%')"
            End If

            SQL = $"
                WITH BarangFG AS (
                    SELECT 
                        b.Kode_Perusahaan,
                        b.Kode_Barang_Inq,
                        MAX(b.Nama_Inq) AS Nama_Inq
                    FROM Barang b
                    JOIN EMI_Group_Jenis g 
                        ON g.Kode_Perusahaan = b.Kode_Perusahaan
                        AND g.Id_Group_Jenis = b.Id_Group_Jenis
                        AND g.Kode_Group_Jenis = 'FINISHED GOODS'
                    WHERE b.Kode_Perusahaan = '{KodePerusahaan}'
                    GROUP BY 
                        b.Kode_Perusahaan,
                        b.Kode_Barang_Inq
                )

                SELECT 
                    Brg.Kode_Barang_Inq,
                    Brg.Nama_Inq AS Nama_Barang,
                    COUNT(DISTINCT F.No_Faktur) AS Total_Formula,
                    CASE 
                        WHEN B.Kode_Barang IS NULL THEN 0
                        ELSE 1
                    END AS IsBinding
                FROM BarangFG Brg
                LEFT JOIN Emi_Transaksi_Formulator F
                    ON F.Kode_Perusahaan = Brg.Kode_Perusahaan
                    AND F.Kode_Barang = Brg.Kode_Barang_Inq
                    AND F.Flag_Validasi = 'Y'
                    AND F.Status IS NULL
                LEFT JOIN N_EMI_Transaksi_Formulator_Binding B
                    ON B.Kode_Perusahaan = Brg.Kode_Perusahaan
                    AND B.Kode_Barang = Brg.Kode_Barang_Inq
                    AND B.Status IS NULL
                WHERE 1=1
                    {filterBinding}
                    {filterCari}
                GROUP BY 
                    Brg.Kode_Barang_Inq,
                    Brg.Nama_Inq,
                    B.Kode_Barang
                ORDER BY 
                    IsBinding DESC,
                    COUNT(F.No_Faktur) DESC
             "

            LvBarang.BeginUpdate()
            LvBarang.Items.Clear()

            Using Dr = OpenTrans(SQL)
                While Dr.Read()

                    Dim kode As String = If(IsDBNull(Dr("Kode_Barang_Inq")), "", Dr("Kode_Barang_Inq").ToString)
                    Dim nama As String = If(IsDBNull(Dr("Nama_Barang")), "", Dr("Nama_Barang").ToString)
                    Dim total As Integer = If(IsDBNull(Dr("Total_Formula")), 0, Convert.ToInt32(Dr("Total_Formula")))
                    Dim isBinding As Integer = If(IsDBNull(Dr("IsBinding")), 0, Convert.ToInt32(Dr("IsBinding")))

                    Dim item As New ListViewItem(kode)
                    item.SubItems.Add(nama)
                    item.SubItems.Add(total.ToString("N0"))

                    If isBinding = 1 Then
                        item.BackColor = Color.LightGreen
                    Else
                        item.BackColor = Color.LightCoral
                    End If

                    LvBarang.Items.Add(item)

                End While
            End Using

            LvBarang.EndUpdate()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Fetch_LvFormulaTersedia(kodeBarang As String)
        If RbAktif.Checked Then
            Try
                OpenConn()
                SQL = $"
                SELECT 
                    F.No_Faktur,
                    F.Tanggal_Validasi,
                    F.Jam_Validasi,
                    F.User_Validasi
                FROM Emi_Transaksi_Formulator F
                WHERE 
                    F.Kode_Perusahaan = '{KodePerusahaan}'
                    AND F.Kode_Barang = '{kodeBarang}'
                    AND F.Flag_Validasi = 'Y'
                    AND F.Status IS NULL
                    AND NOT EXISTS (
                        SELECT 1
                        FROM N_EMI_Transaksi_Formulator_Binding B
                        INNER JOIN N_EMI_Transaksi_Formulator_Binding_Detail D
                            ON D.Kode_Perusahaan = B.Kode_Perusahaan
                            AND D.No_Faktur = B.No_Faktur
                        WHERE 
                            B.Kode_Perusahaan = F.Kode_Perusahaan
                            AND B.Kode_Barang = F.Kode_Barang
                            AND B.Status IS NULL
                            AND D.No_Formulator = F.No_Faktur
                            AND B.No_Faktur = (
                                SELECT TOP 1 No_Faktur
                                FROM N_EMI_Transaksi_Formulator_Binding
                                WHERE 
                                    Kode_Perusahaan = '{KodePerusahaan}'
                                    AND Kode_Barang = '{kodeBarang}'
                                    AND Status IS NULL
                                ORDER BY Tanggal DESC, Jam DESC
                            )
                    )
                ORDER BY 
                    F.Tanggal DESC,
                    F.Jam DESC,
                    F.Tanggal_Validasi DESC,
                    F.Jam_Validasi DESC
            "
                LvFormulaTersedia.BeginUpdate()
                LvFormulaTersedia.Items.Clear()

                Using Dr = OpenTrans(SQL)
                    While Dr.Read()
                        Dim noFaktur As String = If(IsDBNull(Dr("No_Faktur")), "", Dr("No_Faktur").ToString)
                        Dim tanggal As String = If(IsDBNull(Dr("Tanggal_Validasi")), "", Convert.ToDateTime(Dr("Tanggal_Validasi")).ToString("dd MMM yy"))
                        Dim user As String = If(IsDBNull(Dr("User_Validasi")), "", Dr("User_Validasi").ToString)
                        Dim item As New ListViewItem(noFaktur)
                        item.SubItems.Add(tanggal)
                        item.SubItems.Add(user)
                        LvFormulaTersedia.Items.Add(item)
                    End While
                End Using

                LvFormulaTersedia.EndUpdate()
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

        If RbRiwayat.Checked Then
            Try
                OpenConn()
                SQL = $"
                    SELECT 
                        B.No_Faktur,
                        B.Tanggal,
                        B.Jam,
                        B.UserID
                    FROM N_EMI_Transaksi_Formulator_Binding B
                    WHERE 
                        B.Kode_Perusahaan = '{KodePerusahaan}'
                        AND B.Kode_Barang = '{kodeBarang}'
                        AND B.Status IS NULL
                    ORDER BY 
                        B.Tanggal DESC,
                        B.Jam DESC
                "

                LvFormulaTersedia.BeginUpdate()
                LvFormulaTersedia.Items.Clear()

                Dim index As Integer = 0

                Using Dr = OpenTrans(SQL)
                    While Dr.Read()

                        Dim noFaktur As String = If(IsDBNull(Dr("No_Faktur")), "", Dr("No_Faktur").ToString)
                        Dim tanggal As String = If(IsDBNull(Dr("Tanggal")), "", Convert.ToDateTime(Dr("Tanggal")).ToString("dd MMM yy"))
                        Dim user As String = If(IsDBNull(Dr("UserID")), "", Dr("UserID").ToString)

                        Dim item As New ListViewItem(noFaktur)
                        item.SubItems.Add(tanggal)
                        item.SubItems.Add(user)

                        If index = 0 Then
                            item.BackColor = Color.LightGreen
                        Else
                            item.BackColor = Color.Gainsboro
                        End If

                        LvFormulaTersedia.Items.Add(item)

                        index += 1
                    End While
                End Using

                LvFormulaTersedia.EndUpdate()
                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub Fetch_LvBindingFormula(FilterID As String)
        If RbAktif.Checked Then
            Try
                OpenConn()

                SQL = $"
            SELECT 
                D.No_Formulator,
                D.No_Prioritas,
                D.Keterangan
            FROM N_EMI_Transaksi_Formulator_Binding_Detail D
            WHERE 
                D.Kode_Perusahaan = '{KodePerusahaan}'
                AND D.No_Faktur = (
                    SELECT TOP 1 No_Faktur
                    FROM N_EMI_Transaksi_Formulator_Binding
                    WHERE 
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{FilterID}'
                        AND Status IS NULL
                    ORDER BY Tanggal DESC, Jam DESC
                )
            ORDER BY D.No_Prioritas
        "

                LvBindingFormula.BeginUpdate()
                LvBindingFormula.Items.Clear()

                Using Dr = OpenTrans(SQL)
                    While Dr.Read()

                        Dim noFormula As String = If(IsDBNull(Dr("No_Formulator")), "", Dr("No_Formulator").ToString)
                        Dim prioritas As Integer = If(IsDBNull(Dr("No_Prioritas")), 0, Convert.ToInt32(Dr("No_Prioritas")))
                        Dim posisi As String

                        If prioritas = 1 Then
                            posisi = "Formula Utama"
                        Else
                            posisi = "Cadangan " & (prioritas - 1)
                        End If

                        Dim keterangan As String = If(IsDBNull(Dr("Keterangan")), "", Dr("Keterangan").ToString)

                        Dim item As New ListViewItem(noFormula)
                        item.SubItems.Add(posisi)
                        item.SubItems.Add(posisi)
                        item.SubItems.Add(keterangan)

                        LvBindingFormula.Items.Add(item)

                    End While
                End Using

                LvBindingFormula.EndUpdate()
                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

        If RbRiwayat.Checked Then
            Try
                OpenConn()

                SQL = $"
                    SELECT 
                        D.No_Formulator,
                        D.No_Prioritas,
                        D.Keterangan
                    FROM N_EMI_Transaksi_Formulator_Binding_Detail D
                    WHERE 
                        D.Kode_Perusahaan = '{KodePerusahaan}'
                        AND D.No_Faktur = '{FilterID}'
                    ORDER BY D.No_Prioritas
                "

                LvBindingFormula.BeginUpdate()
                LvBindingFormula.Items.Clear()

                Using Dr = OpenTrans(SQL)
                    While Dr.Read()

                        Dim noFormula As String = If(IsDBNull(Dr("No_Formulator")), "", Dr("No_Formulator").ToString)
                        Dim prioritas As Integer = If(IsDBNull(Dr("No_Prioritas")), 0, Convert.ToInt32(Dr("No_Prioritas")))
                        Dim posisi As String

                        If prioritas = 1 Then
                            posisi = "Formula Utama"
                        Else
                            posisi = "Cadangan " & (prioritas - 1)
                        End If

                        Dim keterangan As String = If(IsDBNull(Dr("Keterangan")), "", Dr("Keterangan").ToString)

                        Dim item As New ListViewItem(noFormula)
                        item.SubItems.Add(posisi)
                        item.SubItems.Add(keterangan)

                        LvBindingFormula.Items.Add(item)

                    End While
                End Using

                LvBindingFormula.EndUpdate()
                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub LvBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvBarang.SelectedIndexChanged
        If LvBarang.SelectedItems.Count > 0 Then
            SelectedKodeBarang = LvBarang.SelectedItems(0).Text

            Fetch_LvFormulaTersedia(SelectedKodeBarang)
            Fetch_LvBindingFormula(SelectedKodeBarang)
        End If
    End Sub

    Private Sub LvFormulaTersedia_MouseDown(sender As Object, e As MouseEventArgs) Handles LvFormulaTersedia.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim item As ListViewItem = LvFormulaTersedia.GetItemAt(e.X, e.Y)

            If item IsNot Nothing Then
                item.Selected = True
            End If
        End If
    End Sub

    Private Sub UbahPosisiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UbahPosisiToolStripMenuItem.Click
        Dim ListNoFakturBinding As New List(Of String)
        Dim ListKeterangan As New List(Of String)
        Dim oldPosisi As New Dictionary(Of String, String)

        For Each item As ListViewItem In LvBindingFormula.Items
            ListNoFakturBinding.Add(item.Text)
            ListKeterangan.Add(item.SubItems(3).Text)

            oldPosisi(item.Text) = item.SubItems(1).Text
        Next

        Dim SD As New N_EMI_SD_Compare_Formulator With {
            .NoFaktur = LvBindingFormula.SelectedItems(0).Text,
            .ArrNoFaktur = ListNoFakturBinding,
            .ArrKeterangan = ListKeterangan
        }

        If SD.ShowDialog() = DialogResult.OK Then

            LvBindingFormula.Items.Clear()

            For i As Integer = 0 To SD.ArrNoFaktur.Count - 1

                Dim posisiText As String

                If i = 0 Then
                    posisiText = "Formula Utama"
                Else
                    posisiText = "Cadangan " & i
                End If

                Dim item As New ListViewItem(SD.ArrNoFaktur(i))

                item.SubItems.Add(posisiText)

                Dim posisiSebelumnya As String = "-"
                If oldPosisi.ContainsKey(SD.ArrNoFaktur(i)) Then
                    posisiSebelumnya = oldPosisi(SD.ArrNoFaktur(i))
                End If

                item.SubItems.Add(posisiSebelumnya)
                item.SubItems.Add(SD.ArrKeterangan(i))

                LvBindingFormula.Items.Add(item)

            Next

        End If
    End Sub

    Private Sub BindingFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BindingFormulaToolStripMenuItem.Click

        Dim ListNoFakturBinding As New List(Of String)
        Dim ListKeterangan As New List(Of String)
        Dim oldPosisi As New Dictionary(Of String, String)

        For Each item As ListViewItem In LvBindingFormula.Items
            ListNoFakturBinding.Add(item.Text)
            ListKeterangan.Add(item.SubItems(3).Text)

            oldPosisi(item.Text) = item.SubItems(1).Text
        Next

        Dim SD As New N_EMI_SD_Compare_Formulator With {
        .NoFaktur = LvFormulaTersedia.SelectedItems(0).Text,
        .ArrNoFaktur = ListNoFakturBinding,
        .ArrKeterangan = ListKeterangan
    }

        If SD.ShowDialog() = DialogResult.OK Then

            LvBindingFormula.Items.Clear()

            For i As Integer = 0 To SD.ArrNoFaktur.Count - 1

                Dim posisiText As String

                If i = 0 Then
                    posisiText = "Formula Utama"
                Else
                    posisiText = "Cadangan " & i
                End If

                Dim item As New ListViewItem(SD.ArrNoFaktur(i))

                item.SubItems.Add(posisiText)

                Dim posisiSebelumnya As String = "-"
                If oldPosisi.ContainsKey(SD.ArrNoFaktur(i)) Then
                    posisiSebelumnya = oldPosisi(SD.ArrNoFaktur(i))
                End If

                item.SubItems.Add(posisiSebelumnya)
                item.SubItems.Add(SD.ArrKeterangan(i))

                LvBindingFormula.Items.Add(item)

            Next

        End If

    End Sub

    Private Sub DetailFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailFormulaToolStripMenuItem.Click
        Dim SD As New N_EMI_SD_Detail_Formulator With {
            .NoFaktur = LvFormulaTersedia.SelectedItems(0).Text
        }
        SD.ShowDialog()
    End Sub

    Private Sub RbAktif_CheckedChanged(sender As Object, e As EventArgs) Handles RbAktif.CheckedChanged
        If RbAktif.Checked Then
            PnlBindingAktif.Visible = False
            LbBindingAktif.Visible = False
            PnlBindingTidakAktif.Visible = False
            LbBindingTidakAktif.Visible = False

            RbRiwayat.Checked = False
            BtnSimpanBinding.Enabled = True

            GbFormulaTersedia.Text = "Formula Tersedia"

            LvFormulaTersedia.Clear()
            LvBindingFormula.Items.Clear()
            LvFormulaTersedia.ContextMenuStrip = CmsFormulaTersedia

            LvBindingFormula.Clear()

            With LvFormulaTersedia
                .View = View.Details
                .FullRowSelect = True
                .GridLines = True
                .MultiSelect = False
                .HideSelection = False
                .Columns.Clear()

                .Columns.Add("No Formula", 100)
                .Columns.Add("Tanggal Validasi", 100, HorizontalAlignment.Center)
                .Columns.Add("User Validasi", 100, HorizontalAlignment.Center)
            End With

            With LvBindingFormula
                .View = View.Details
                .FullRowSelect = True
                .GridLines = True
                .MultiSelect = False
                .HideSelection = False
                .Columns.Clear()

                .Columns.Add("No Formula", 100)
                .Columns.Add("Posisi", 100)
                .Columns.Add("Posisi Sebelumnya", 100)
                .Columns.Add("Keterangan", 250)
            End With

            If String.IsNullOrEmpty(SelectedKodeBarang) Then
                Fetch_LvFormulaTersedia(SelectedKodeBarang)
                Fetch_LvBindingFormula(SelectedKodeBarang)
            End If
        End If
    End Sub

    Private Sub RbRiwayat_CheckedChanged(sender As Object, e As EventArgs) Handles RbRiwayat.CheckedChanged
        If RbRiwayat.Checked Then
            PnlBindingAktif.Visible = True
            LbBindingAktif.Visible = True
            PnlBindingTidakAktif.Visible = True
            LbBindingTidakAktif.Visible = True

            RbAktif.Checked = False
            BtnSimpanBinding.Enabled = False

            GbFormulaTersedia.Text = "Formula Binding"

            LvFormulaTersedia.Clear()
            LvBindingFormula.Items.Clear()
            LvFormulaTersedia.ContextMenuStrip = Nothing

            LvBindingFormula.Clear()

            With LvFormulaTersedia
                .View = View.Details
                .FullRowSelect = True
                .GridLines = True
                .MultiSelect = False
                .HideSelection = False
                .Columns.Clear()

                .Columns.Add("No Binding", 100)
                .Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
                .Columns.Add("User", 100, HorizontalAlignment.Center)
            End With

            With LvBindingFormula
                .View = View.Details
                .FullRowSelect = True
                .GridLines = True
                .MultiSelect = False
                .HideSelection = False
                .Columns.Clear()

                .Columns.Add("No Formula", 100)
                .Columns.Add("Posisi", 100)
                .Columns.Add("Keterangan", 250)
            End With

            If String.IsNullOrEmpty(SelectedKodeBarang) Then
                Fetch_LvFormulaTersedia(SelectedKodeBarang)
            End If
        End If
    End Sub

    Private Sub RbBelumBinding_CheckedChanged(sender As Object, e As EventArgs) Handles RbBelumBinding.CheckedChanged
        If RbBelumBinding.Checked Then
            RbSudahBinding.Checked = False
        End If

        Fetch_LvBarang()

        LvFormulaTersedia.Items.Clear()
        LvBindingFormula.Items.Clear()
    End Sub

    Private Sub RbSudahBinding_CheckedChanged(sender As Object, e As EventArgs) Handles RbSudahBinding.CheckedChanged
        If RbSudahBinding.Checked Then
            RbBelumBinding.Checked = False
        End If

        Fetch_LvBarang()

        LvFormulaTersedia.Items.Clear()
        LvBindingFormula.Items.Clear()
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        RbSudahBinding.Checked = False
        RbBelumBinding.Checked = False

        TxtCariBarang.Text = ""
        SelectedKodeBarang = ""

        LvFormulaTersedia.Items.Clear()
        LvBindingFormula.Items.Clear()

        Fetch_LvBarang()
    End Sub

    Private Sub BtnCariBarang_Click(sender As Object, e As EventArgs) Handles BtnCariBarang.Click
        Fetch_LvBarang()
    End Sub

    Private Function get_no_faktur() As String
        Dim PrefixFaktur = "FRMB"
        Dim NoFaktur = PrefixFaktur & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("N_EMI_Transaksi_Formulator_Binding", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(PrefixFaktur) + 4 & ")", PrefixFaktur & Format(tgl_skg, "MMyy"))

        Return NoFaktur
    End Function

    Private Sub BtnSimpanBinding_Click(sender As Object, e As EventArgs) Handles BtnSimpanBinding.Click
        If LvBindingFormula.Items.Count = 0 Then
            MessageBox.Show("Minimal harus ada satu formula yang di binding.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If String.IsNullOrEmpty(SelectedKodeBarang) Then
            MessageBox.Show("Pilih barang terlebih dahulu.", "Peringatan")
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim NoFaktur = get_no_faktur()
            Dim KodeBarang = SelectedKodeBarang
            Dim Tanggal = Format(tgl_skg, " yyyy-MM-dd")
            Dim Jam = Format(tgl_skg, "HH:mm:ss")

            SQL = "
                INSERT INTO N_EMI_Transaksi_Formulator_Binding
                (Kode_Perusahaan, No_Faktur, Kode_Barang, Tanggal, Jam, UserID, Status)
                VALUES
                (@KodePerusahaan, @NoFaktur, @KodeBarang, @Tanggal, @Jam, @UserID, NULL)
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@NoFaktur", NoFaktur)
            Cmd.Parameters.AddWithValue("@KodeBarang", KodeBarang)
            Cmd.Parameters.AddWithValue("@Tanggal", Tanggal)
            Cmd.Parameters.AddWithValue("@Jam", Jam)
            Cmd.Parameters.AddWithValue("@UserID", UserID)
            ExecuteTrans(SQL)

            SQL = "
                INSERT INTO N_EMI_Transaksi_Formulator_Binding_Detail
                (Kode_Perusahaan, No_Faktur, No_Formulator, No_Prioritas, Tanggal, Jam, UserID, Keterangan)
                VALUES
                (@KodePerusahaan, @NoFaktur, @NoFormulator, @Prioritas, @Tanggal, @Jam, @UserID, @Keterangan)
            "

            For i = 0 To LvBindingFormula.Items.Count - 1
                Dim item = LvBindingFormula.Items(i)
                Dim NoFormulator = item.SubItems(0).Text
                Dim Keterangan = item.SubItems(3).Text
                Dim Prioritas = i + 1

                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                Cmd.Parameters.AddWithValue("@NoFaktur", NoFaktur)
                Cmd.Parameters.AddWithValue("@NoFormulator", NoFormulator)
                Cmd.Parameters.AddWithValue("@Prioritas", Prioritas)
                Cmd.Parameters.AddWithValue("@Tanggal", Tanggal)
                Cmd.Parameters.AddWithValue("@Jam", Jam)
                Cmd.Parameters.AddWithValue("@UserID", UserID)
                Cmd.Parameters.AddWithValue("@Keterangan", Keterangan)
                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            Fetch_LvBarang()

            MessageBox.Show("Data binding berhasil disimpan.", "Informasi")
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub DetailFormulaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DetailFormulaToolStripMenuItem1.Click
        Dim SD As New N_EMI_SD_Detail_Formulator With {
            .NoFaktur = LvBindingFormula.SelectedItems(0).Text
        }
        SD.ShowDialog()
    End Sub

    Private Sub BatalkanFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanFormulaToolStripMenuItem.Click
        If LvBindingFormula.SelectedItems.Count = 0 Then Exit Sub
        Dim item = LvBindingFormula.SelectedItems(0)
        Dim noFaktur As String = item.SubItems(0).Text

        LvBindingFormula.Items.Remove(item)

        ReorderPosisiBinding()

        Try
            OpenConn()

            SQL = $"
                SELECT 
                    No_Faktur,
                    Tanggal_Validasi,
                    User_Validasi
                FROM Emi_Transaksi_Formulator
                WHERE 
                    Kode_Perusahaan = '{KodePerusahaan}'
                    AND No_Faktur = '{noFaktur}'
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then

                    Dim tanggal As String = If(IsDBNull(Dr("Tanggal_Validasi")), "", Convert.ToDateTime(Dr("Tanggal_Validasi")).ToString("dd MMM yy"))
                    Dim user As String = If(IsDBNull(Dr("User_Validasi")), "", Dr("User_Validasi").ToString)

                    Dim newItem As New ListViewItem(noFaktur)
                    newItem.SubItems.Add(tanggal)
                    newItem.SubItems.Add(user)

                    LvFormulaTersedia.Items.Add(newItem)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub LvFormulaTersedia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvFormulaTersedia.SelectedIndexChanged
        If RbRiwayat.Checked Then
            If LvFormulaTersedia.SelectedItems.Count > 0 Then
                Fetch_LvBindingFormula(LvFormulaTersedia.SelectedItems(0).Text)
            End If
        End If
    End Sub

    Private Sub ReorderPosisiBinding()
        For i As Integer = 0 To LvBindingFormula.Items.Count - 1

            Dim posisiText As String

            If i = 0 Then
                posisiText = "Formula Utama"
            Else
                posisiText = "Cadangan " & i
            End If

            LvBindingFormula.Items(i).SubItems(1).Text = posisiText
        Next
    End Sub
End Class