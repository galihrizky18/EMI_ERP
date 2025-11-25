Public Class N_EMI_Master_Persentase_Margin_Barang

    Dim arrFilter As New ArrayList
    Dim JudulForm As String = "Master Persentase Margin Barang"
    Private PersentaseStep As Integer = 1

    Private Sub N_EMI_Master_Persentase_Margin_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lv_DataPersentase.Columns.Clear() : Lv_DataPersentase.Items.Clear()
        Lv_DataPersentase.Columns.Add("Kode Barang", 200, HorizontalAlignment.Left)
        Lv_DataPersentase.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
        Lv_DataPersentase.Columns.Add("Persentase Margin (%)", 420, HorizontalAlignment.Left)
        Lv_DataPersentase.View = View.Details

        LvPilihBarang_DataBarang.Visible = False
        LvPilihBarang_DataBarang.Location = New Point(120, 77)

        Kosong()
    End Sub


    Private Sub Kosong()
        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("Kode_Barang")
        Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("Nama_Barang")

        Txt_ValueFilter.Text = ""

        Btn_Simpan.Tag = "SIMPAN"
        Btn_Simpan.Text = "&Simpan"
        TxtPilihBarang_KodeBarang.Text = ""
        TxtPilihBarang_NamaBarang.Text = ""
        TxtPilihBarang_Satuan.Text = ""
        TxtPersentase.Text = ""
        TxtPilihBarang_KodeBarang.Focus()

        LoadDataPersentaseMarginBarang(False)
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Harap Pilih Filter Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Filter.DroppedDown = True : Cmb_Filter.Focus() : Exit Sub
        Else
            If Txt_ValueFilter.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_ValueFilter.Focus() : Exit Sub
            End If
        End If

        LoadDataPersentaseMarginBarang(True)
    End Sub

    Private Sub LoadDataPersentaseMarginBarang(ByVal filter As Boolean)
        Try
            OpenConn()

            Lv_DataPersentase.Items.Clear()
            SQL = "select Kode_Barang, Nama_Barang, Persentase_Margin "
            SQL = SQL & "from N_EMI_Master_Persentase_Margin "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            If filter Then
                SQL = SQL & "and " & arrFilter(Cmb_Filter.SelectedIndex) & " like '%" & Txt_ValueFilter.Text & "%' "
            End If
            SQL = SQL & "order by Kode_Barang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DataPersentase.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Persentase_Margin"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If VIV(IVR_Required(TxtPilihBarang_KodeBarang.Text, "Kode Barang")) Then Exit Sub
        If VIV(IVR_Combine(TxtPersentase.Text, Function(v) IVR_Required(v, "Persentase"), Function(v) IVR_Percentage(v, "Persentase"))) Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim Action As String = ""

            '=====================================================
            '=     CEK APAKAH DATA SUDAH ADA                     =
            '=====================================================
            SQL = "select Kode_Perusahaan, Kode_Barang "
            SQL = SQL & "from N_EMI_Master_Persentase_Margin "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    '===========================
                    '=     UPDATE              =
                    '===========================
                    Dr.Close()
                    SQL = "update N_EMI_Master_Persentase_Margin set "
                    SQL = SQL & "Persentase_Margin = '" & TxtPersentase.Text & "' "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' "
                    ExecuteTrans(SQL)

                    Action = "UPDATE"
                Else
                    Dr.Close()
                    '===========================
                    '=     INSERT              =
                    '===========================
                    SQL = "insert into N_EMI_Master_Persentase_Margin "
                    SQL = SQL & "(Kode_Perusahaan, Kode_Barang, Nama_Barang, Persentase_Margin) values "
                    SQL = SQL & "('" & KodePerusahaan & "', "
                    SQL = SQL & "'" & TxtPilihBarang_KodeBarang.Text & "', "
                    SQL = SQL & "'" & TxtPilihBarang_NamaBarang.Text & "', "
                    SQL = SQL & "'" & TxtPersentase.Text & "') "
                    ExecuteTrans(SQL)

                    Action = "SIMPAN"
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            If Action = "SIMPAN" Then
                MessageBox.Show("Persentase berhasil ditambahkan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf Action = "UPDATE" Then
                MessageBox.Show("Persentase berhasil diupdate", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message, JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Kosong()

    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click

        If MessageBox.Show("Yakin ingin Hapus Data Persentase Margin " & TxtPilihBarang_NamaBarang.Text & " ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '==========================
            '=     HAPUS KATEGORI     =
            '==========================
            SQL = "select Kode_Perusahaan, Kode_Barang "
            SQL = SQL & "from N_EMI_Master_Persentase_Margin "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()

                    SQL = "Delete N_EMI_Master_Persentase_Margin  "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' "
                    ExecuteTrans(SQL)

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Persentase tidak ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Persentase Berhasil Dihapus", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()


    End Sub

    Private Sub Lv_DataPersentase_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DataPersentase.DoubleClick
        If Lv_DataPersentase.Items.Count = 0 Or Lv_DataPersentase.FocusedItem Is Nothing Then Exit Sub

        TxtPilihBarang_KodeBarang.Text = Lv_DataPersentase.FocusedItem.SubItems(0).Text
        TxtPersentase.Text = Lv_DataPersentase.FocusedItem.SubItems(2).Text
        TxtPilihBarang_KodeBarang.Focus() : TxtPersentase.Focus()

        Btn_Simpan.Tag = "UPDATE"
        Btn_Simpan.Text = "&Update"
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Txt_NmKategori_KeyPress(sender As Object, e As KeyPressEventArgs)
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Cmb_Filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter.KeyPress
        If e.KeyChar = Chr(13) Then Txt_ValueFilter.Focus()
    End Sub

    Private Sub Txt_ValueFilter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ValueFilter.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub TxtPilihBarang_KodeBarang_Leave(sender As Object, e As EventArgs) Handles TxtPilihBarang_KodeBarang.Leave
        If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then Exit Sub
        If LvPilihBarang_DataBarang.Focused = True Then Exit Sub

        Try
            OpenConn()

            '==============================
            '= CARI DATA BARANG           =
            '==============================
            SQL = "SELECT Kode_Barang, Nama, Satuan "
            SQL = SQL & "FROM barang "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "AND (Nama LIKE '%" & TxtPilihBarang_KodeBarang.Text & "%' "
            SQL = SQL & "     OR Kode_Barang LIKE '%" & TxtPilihBarang_KodeBarang.Text & "%') "
            SQL = SQL & "AND Aktif = 'Y' "
            SQL = SQL & "GROUP BY Kode_Barang, Nama, Satuan"

            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TxtPilihBarang_KodeBarang.Text = dr("Kode_Barang")
                    TxtPilihBarang_NamaBarang.Text = dr("Nama")
                    TxtPilihBarang_Satuan.Text = dr("Satuan")
                    TxtPersentase.Text = ""
                    dr.Close()

                    '=============================================
                    '= CEK DATA PERSENTASE DI TABEL MARGIN       =
                    '=============================================
                    SQL = "select Persentase_Margin "
                    SQL = SQL & "from N_EMI_Master_Persentase_Margin "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' "

                    Using drMargin = OpenTrans(SQL)
                        If drMargin.Read Then
                            TxtPersentase.Text = drMargin("Persentase_Margin")

                            Btn_Simpan.Tag = "UPDATE"
                            Btn_Simpan.Text = "&Update"
                        End If
                    End Using

                    LvPilihBarang_DataBarang.Visible = False
                    TxtPersentase.Focus()
                Else
                    TxtPilihBarang_KodeBarang.Text = ""
                    TxtPilihBarang_NamaBarang.Text = ""
                    TxtPilihBarang_Satuan.Text = ""
                    TxtPersentase.Text = ""
                    TxtPilihBarang_KodeBarang.Focus()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtPilihBarang_KodeBarang_TextChanged(sender As Object, e As EventArgs) Handles TxtPilihBarang_KodeBarang.TextChanged
        If TxtPilihBarang_KodeBarang.Text.Length >= 3 Then
            If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then
                LvPilihBarang_DataBarang.Visible = False : Exit Sub
            Else
                LvPilihBarang_DataBarang.Visible = True
            End If

            If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then
                LvPilihBarang_DataBarang.Visible = False : Exit Sub
            Else
                LvPilihBarang_DataBarang.Visible = True
            End If

            TxtPilihBarang_NamaBarang.Text = ""

            Try
                OpenConn()

                LvPilihBarang_DataBarang.Items.Clear()
                Dim Lvw As ListViewItem

                SQL = "SELECT Kode_Perusahaan, Kode_Barang, Nama, Satuan "
                SQL = SQL & "FROM barang "
                SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "AND (Nama LIKE '%" & TxtPilihBarang_KodeBarang.Text & "%' "
                SQL = SQL & "     OR Kode_Barang LIKE '%" & TxtPilihBarang_KodeBarang.Text & "%') "
                SQL = SQL & "AND Aktif = 'Y' "
                SQL = SQL & "GROUP BY Kode_Perusahaan, Kode_Barang, Nama, Satuan"
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        Lvw = LvPilihBarang_DataBarang.Items.Add(dr("Kode_Perusahaan"))
                        Lvw.SubItems.Add(dr("Kode_Barang"))
                        Lvw.SubItems.Add(dr("Nama"))
                        Lvw.SubItems.Add(dr("Satuan"))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            LvPilihBarang_DataBarang.Visible = False
        End If
    End Sub
    Private Sub LvPilihBarang_DataBarang_DoubleClick(sender As Object, e As EventArgs) Handles LvPilihBarang_DataBarang.DoubleClick
        If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub

        TxtPilihBarang_KodeBarang.Text = LvPilihBarang_DataBarang.FocusedItem.SubItems(1).Text
        TxtPilihBarang_KodeBarang.Focus() : TxtPersentase.Focus()
        LvPilihBarang_DataBarang.Visible = False

    End Sub
    Private Sub TxtKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPilihBarang_KodeBarang.KeyPress
        If e.KeyChar = Chr(13) Then TxtPersentase.Focus()

    End Sub
    Private Sub TxtKode_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPilihBarang_KodeBarang.KeyDown
        If e.KeyCode = Keys.Down Then
            If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub
            LvPilihBarang_DataBarang.Focus()
        End If
    End Sub

    Private Sub TxtPersentase_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPersentase.KeyPress
        KPR_Percentage(CType(sender, TextBox), e)
    End Sub

    Private Sub BtnPercentageControl_Click(sender As Object, e As EventArgs) Handles BtnPlus.Click, BtnMinus.Click
        If sender Is BtnPlus Then
            TxtPersentase.Text = IVP_Percentage(TxtPersentase.Text, +PersentaseStep).ToString()
        ElseIf sender Is BtnMinus Then
            TxtPersentase.Text = IVP_Percentage(TxtPersentase.Text, -PersentaseStep).ToString()
        End If
    End Sub

    Private Sub TxtPersentase_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPersentase.KeyDown
        If e.KeyCode = Keys.Up Then
            TxtPersentase.Text = IVP_Percentage(TxtPersentase.Text, +PersentaseStep).ToString()
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Down Then
            TxtPersentase.Text = IVP_Percentage(TxtPersentase.Text, -PersentaseStep).ToString()
            e.SuppressKeyPress = True
        End If
    End Sub
End Class