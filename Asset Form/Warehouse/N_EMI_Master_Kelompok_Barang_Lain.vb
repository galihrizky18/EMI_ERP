Public Class N_EMI_Master_Kelompok_Barang_Lain

    Dim arrFilter As New ArrayList
    Dim JudulForm As String = "Master Kelompok Barang Asset"

    Private Sub N_EMI_Master_Kelompok_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_DataKelompok.Columns.Clear() : Lv_DataKelompok.Items.Clear()
        Lv_DataKelompok.Columns.Add("ID_Kelompok", 0, HorizontalAlignment.Left)
        Lv_DataKelompok.Columns.Add("ID Kategori", 0, HorizontalAlignment.Left)
        Lv_DataKelompok.Columns.Add("Nama Kategori", 200, HorizontalAlignment.Left)
        Lv_DataKelompok.Columns.Add("Kode Kelompok", 200, HorizontalAlignment.Left)
        Lv_DataKelompok.Columns.Add("Keterangan", 230, HorizontalAlignment.Left)
        Lv_DataKelompok.View = View.Details


        Lv_Kategori.Columns.Clear() : Lv_Kategori.Items.Clear()
        Lv_Kategori.Columns.Add("ID_Kategori", 0, HorizontalAlignment.Left)
        Lv_Kategori.Columns.Add("Kode Kategori", 150, HorizontalAlignment.Left)
        Lv_Kategori.Columns.Add("Keterangan", 230, HorizontalAlignment.Left)
        Lv_Kategori.View = View.Details

        kosong()
    End Sub

    Private Sub kosong()

        Txt_Kategori.Text = ""
        Txt_NmKelompok.Text = ""
        Txt_Keterangan.Text = ""
        Txt_SelectedID.Text = ""
        Txt_IdKategori.Text = ""

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add("Kode Kelompok") : arrFilter.Add("a.Kode_Kelompok")
        Cmb_Filter.Items.Add("Keterangan") : arrFilter.Add("a.Keterangan")

        Txt_ValueFilter.Text = ""

        Btn_Simpan.Tag = "SIMPAN"
        Btn_Simpan.Text = "&Simpan"

        Txt_NmKelompok.Text = ""
        LoadDataKelompok(False)


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

        LoadDataKelompok(True)

    End Sub

    Private Sub LoadDataKelompok(ByVal filter As Boolean)

        Try
            OpenConn()

            Lv_DataKelompok.Items.Clear()
            SQL = "select a.ID_Kelompok, b.ID_Kategori, b.Keterangan As Nama_Kategori, a.Kode_Kelompok, a.keterangan "
            SQL = SQL & "from N_EMI_Master_Kelompok_Barang_Lain a, N_EMI_Master_Kategori_Barang_Lain b  "
            SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.ID_Kategori = b.ID_Kategori "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If filter Then
                SQL = SQL & "and " & arrFilter(Cmb_Filter.SelectedIndex) & " like '%" & Txt_ValueFilter.Text & "%' "
            End If
            SQL = SQL & "order by b.ID_Kategori, a.ID_Kelompok "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DataKelompok.Items.Add(Dr("ID_Kelompok"))
                    Lv.SubItems.Add(Dr("ID_Kategori"))
                    Lv.SubItems.Add(Dr("Nama_Kategori"))
                    Lv.SubItems.Add(Dr("Kode_Kelompok"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Txt_Kategori_Leave(sender As Object, e As EventArgs) Handles Txt_Kategori.Leave
        If Txt_Kategori.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Kategori.Focused = True Then Exit Sub

        Try
            OpenConn()
            SQL = "select ID_Kategori, Kode_Kategori, Keterangan from N_EMI_Master_Kategori_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and ID_Kategori = '" & Txt_IdKategori.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_IdKategori.Text = Dr("ID_Kategori")
                    Txt_Kategori.Text = Dr("Keterangan")
                    Txt_NmKelompok.Focus()
                Else
                    MessageBox.Show("Kategori Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_Kategori.Text = "" : Txt_IdKategori.Text = ""
                    Txt_Kategori.Focus() : Exit Sub
                End If

                Lv_Kategori.Location = New Point(700, 75)
                Lv_Kategori.Visible = False
            End Using

            Try
                OpenConn()

                '==================================
                '=     GENERATE KODE KELOMPOK     =
                '==================================
                Dim LastIdentity As Long = 0
                SQL = "select Top(1) Kode_Kelompok "
                SQL = SQL & "from N_EMI_Master_Kelompok_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and ID_Kategori = '" & Txt_IdKategori.Text & "' "
                SQL = SQL & "Order by cast(Kode_Kelompok as integer) desc "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        LastIdentity = Dr("Kode_Kelompok")
                    End If
                End Using

                LastIdentity += 1

                Dim KodeKelompok As String = LastIdentity.ToString("D3")

                Txt_NmKelompok.Text = KodeKelompok

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try

    End Sub



    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Txt_Kategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Kategori Harus Disi . . ! !", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kategori.Focus() : Exit Sub
        ElseIf Txt_NmKelompok.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Kelompok Harus Diisi . . ! !", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NmKelompok.Focus() : Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Harus Diisi . . ! !", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus() : Exit Sub
        End If

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim Action As String = ""

            If Btn_Simpan.Tag = "SIMPAN" Then

                '=========================================
                '=     CEK APAKAH KATEGORI DITEMUKAN     =
                '=========================================
                SQL = "select Kode_Perusahaan from N_EMI_Master_Kategori_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and ID_Kategori = '" & Txt_IdKategori.Text.Trim & "' "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        MessageBox.Show("Kategori Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Kategori.Text = "" : Txt_IdKategori.Text = ""
                        Txt_Kategori.Focus() : Exit Sub
                    End If
                End Using

                '=====================================================
                '=     CEK APAKAH ADA DATA DENGAN NAMA YANG SAMA     =
                '=====================================================
                SQL = "select Top(1) Kode_Kelompok "
                SQL = SQL & "from N_EMI_Master_Kelompok_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Kelompok = '" & Txt_NmKelompok.Text & "' "
                SQL = SQL & "and ID_Kategori = '" & Txt_IdKategori.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Silakan Ulangi Transaksi ini . . ! !", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                '==================================
                '=     GENERATE KODE KELOMPOK     =
                '==================================
                Dim LastIdentity As Long = 0
                SQL = "select Kode_Kelompok "
                SQL = SQL & "from N_EMI_Master_Kelompok_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and ID_Kategori = '" & Txt_IdKategori.Text & "' "
                SQL = SQL & "Order by cast(Kode_Kelompok as integer) desc "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        LastIdentity = Dr("Kode_Kelompok")
                    End If
                End Using

                LastIdentity += 1

                Dim KodeKelompok As String = LastIdentity.ToString("D3")


                '===========================
                '=     INSERT KELOMPOK     =
                '===========================
                SQL = "insert into N_EMI_Master_Kelompok_Barang_Lain (Kode_Perusahaan, ID_Kategori, Kode_Kelompok, Keterangan ) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_IdKategori.Text & "', '" & KodeKelompok & "', '" & Txt_Keterangan.Text & "')  "
                ExecuteTrans(SQL)

                Action = "SIMPAN"


            ElseIf Btn_Simpan.Tag = "UPDATE" Then

                If MessageBox.Show("Yakin ingin Update Data Ini ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

                '===========================
                '=     UPDATE KATEGORI     =
                '===========================
                SQL = "select Kode_Kelompok "
                SQL = SQL & "from N_EMI_Master_Kelompok_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and ID_Kelompok = '" & Txt_SelectedID.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        SQL = "update N_EMI_Master_Kelompok_Barang_Lain set ID_Kategori = '" & Txt_IdKategori.Text & "', Keterangan = '" & Txt_Keterangan.Text & "', Kode_Kelompok = '" & Txt_NmKelompok.Text & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and ID_Kelompok = '" & Txt_SelectedID.Text & "' "
                        ExecuteTrans(SQL)

                        Action = "UPDATE"

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Kelompok tidak ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            End If


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            If Action = "SIMPAN" Then
                MessageBox.Show("Kelompok Berhasil Ditambahkan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf Action = "UPDATE" Then
                MessageBox.Show("Kelompok Berhasil Diupdate", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()

    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click

        If Txt_SelectedID.Text.Trim.Length = 0 Then
            MessageBox.Show("Pilih Dahulu Kelompok yang Ingin Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_DataKelompok.Focus() : Exit Sub
        End If

        If MessageBox.Show("Yakin ingin Hapus Kelompok " & Txt_NmKelompok.Text & " ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        Try
            OpenConn()

            '===============================================
            '=     CEK APAKAH KELOMPOK SUDAH DIGUNAKAN     =
            '===============================================
            SQL = "select top 1 Kode_Perusahaan from Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and ID_Kelompok = '" & Txt_SelectedID.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kelompok Tidak Bisa Dihapus Karena Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==========================
            '=     HAPUS KATEGORI     =
            '==========================
            SQL = "select Kode_Kelompok "
            SQL = SQL & "from N_EMI_Master_Kelompok_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and ID_Kelompok = '" & Txt_SelectedID.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = "Delete N_EMI_Master_Kelompok_Barang_Lain  "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and ID_Kelompok = '" & Txt_SelectedID.Text & "' "
                    ExecuteTrans(SQL)

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kelompok tidak ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
            MessageBox.Show("Kelompok Berhasil Dihapus", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()

    End Sub

    Private Sub Txt_Kategori_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kategori.TextChanged
        If Txt_Kategori.Text.Trim.Length = 0 Then
            Lv_Kategori.Items.Clear()
            Lv_Kategori.Location = New Point(700, 75)
            Lv_Kategori.Visible = False
            Exit Sub
        Else
            Lv_Kategori.Items.Clear()
            Lv_Kategori.Location = New Point(130, 75)
            Lv_Kategori.Visible = True
        End If

        Try
            OpenConn()

            Lv_Kategori.Items.Clear()
            SQL = "select ID_Kategori, Kode_Kategori, Keterangan "
            SQL = SQL & "from N_EMI_Master_Kategori_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Keterangan like '%" & Txt_Kategori.Text & "%' "
            SQL = SQL & "order by ID_Kategori "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Kategori.Items.Add(Dr("ID_Kategori"))
                    Lv.SubItems.Add(Dr("Kode_Kategori"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_Kategori_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Kategori.DoubleClick
        If Lv_Kategori.Items.Count = 0 Or Lv_Kategori.FocusedItem.Index = -1 Then Exit Sub

        Dim IdKategori As String = Lv_Kategori.FocusedItem.SubItems(0).Text
        Dim NmKategori As String = Lv_Kategori.FocusedItem.SubItems(2).Text

        Txt_IdKategori.Text = IdKategori
        Txt_Kategori.Text = NmKategori

        Lv_Kategori.Location = New Point(700, 75)
        Lv_Kategori.Visible = False

        Txt_NmKelompok.Focus()
    End Sub

    Private Sub Lv_DataKelompok_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DataKelompok.DoubleClick

        If Lv_DataKelompok.Items.Count = 0 Or Lv_DataKelompok.FocusedItem Is Nothing Then Exit Sub

        Dim IdKategori As String = Lv_DataKelompok.FocusedItem.SubItems(1).Text
        Dim NmKategori As String = Lv_DataKelompok.FocusedItem.SubItems(2).Text

        Txt_SelectedID.Text = Lv_DataKelompok.FocusedItem.Text
        Txt_IdKategori.Text = IdKategori
        Txt_Kategori.Text = NmKategori
        Txt_NmKelompok.Text = Lv_DataKelompok.FocusedItem.SubItems(3).Text
        Txt_Keterangan.Text = Lv_DataKelompok.FocusedItem.SubItems(4).Text

        Btn_Simpan.Tag = "UPDATE"
        Btn_Simpan.Text = "&Update"

        Lv_Kategori.Location = New Point(700, 75)
        Lv_Kategori.Visible = False

        Txt_NmKelompok.Focus()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Txt_Kategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kategori.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kategori.Text.Trim.Length = 0 Then Txt_Kategori.Focus()
            Txt_Kategori_Leave(Txt_Kategori, e)

            Lv_Kategori.Location = New Point(700, 75)
            Lv_Kategori.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Kategori_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kategori.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Kategori.Focus()
    End Sub

    Private Sub Lv_Kategori_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Kategori.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Kategori_DoubleClick(Lv_Kategori, e)
        End If
    End Sub

    Private Sub Txt_NmKelompok_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmKelompok.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Cmb_Filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter.KeyPress
        If e.KeyChar = Chr(13) Then Txt_ValueFilter.Focus()
    End Sub

    Private Sub Txt_ValueFilter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ValueFilter.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub
End Class

