Public Class N_EMI_Master_Area_Barang_Lain

    Dim arrFilter As New ArrayList
    Dim JudulForm As String = "Master Area Barang Asset"


    Private Sub N_EMI_Master_Area_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lv_DataArea.Columns.Clear() : Lv_DataArea.Items.Clear()
        Lv_DataArea.Columns.Add("ID_Area", 0, HorizontalAlignment.Left)
        Lv_DataArea.Columns.Add("ID Gedung", 0, HorizontalAlignment.Left)
        Lv_DataArea.Columns.Add("Gedung", 200, HorizontalAlignment.Left)
        Lv_DataArea.Columns.Add("Kode Area", 200, HorizontalAlignment.Left)
        Lv_DataArea.Columns.Add("Keterangan", 230, HorizontalAlignment.Left)
        Lv_DataArea.View = View.Details


        Lv_Gedung.Columns.Clear() : Lv_Gedung.Items.Clear()
        Lv_Gedung.Columns.Add("ID Gedung", 0, HorizontalAlignment.Left)
        Lv_Gedung.Columns.Add("Kode Gedung", 150, HorizontalAlignment.Left)
        Lv_Gedung.Columns.Add("Keterangan", 230, HorizontalAlignment.Left)
        Lv_Gedung.View = View.Details

        kosong()
    End Sub

    Private Sub kosong()
        Txt_Gedung.Text = ""
        Txt_NmArea.Text = ""
        Txt_Keterangan.Text = ""
        Txt_SelectedID.Text = ""
        Txt_IdGedung.Text = ""

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add("Kode Area") : arrFilter.Add("a.Kode_Area")
        Cmb_Filter.Items.Add("Keterangan") : arrFilter.Add("a.Keterangan")

        Txt_ValueFilter.Text = ""

        Btn_Simpan.Tag = "SIMPAN"
        Btn_Simpan.Text = "&Simpan"

        Txt_NmArea.Text = ""

        LoadDataArea(False)


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

        LoadDataArea(True)


    End Sub

    Private Sub LoadDataArea(ByVal filter As Boolean)

        Try
            OpenConn()

            Lv_DataArea.Items.Clear()
            SQL = "select a.ID_Area, b.ID_Gedung, b.Keterangan As Nama_Gedung, a.Kode_Area, a.keterangan "
            SQL = SQL & "from N_EMI_Master_Area_Barang_Lain a, N_EMI_Master_Gedung_Barang_Lain b  "
            SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.ID_Gedung = b.ID_Gedung "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If filter Then
                SQL = SQL & "and " & arrFilter(Cmb_Filter.SelectedIndex) & " like '%" & Txt_ValueFilter.Text & "%' "
            End If
            SQL = SQL & "order by b.ID_Gedung, a.ID_Area "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DataArea.Items.Add(Dr("ID_Area"))
                    Lv.SubItems.Add(Dr("ID_Gedung"))
                    Lv.SubItems.Add(Dr("Nama_Gedung"))
                    Lv.SubItems.Add(Dr("Kode_Area"))
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

    Private Sub Txt_Gedung_Leave(sender As Object, e As EventArgs) Handles Txt_Gedung.Leave
        If Txt_Gedung.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Gedung.Focused = True Then Exit Sub

        Try
            OpenConn()
            SQL = "select ID_Gedung, Kode_Gedung, Keterangan from N_EMI_Master_Gedung_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and ID_Gedung = '" & Txt_IdGedung.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_IdGedung.Text = Dr("ID_Gedung")
                    Txt_Gedung.Text = Dr("Keterangan")
                    Txt_NmArea.Focus()
                Else
                    MessageBox.Show("Gedung Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_Gedung.Text = "" : Txt_IdGedung.Text = ""
                    Txt_Gedung.Focus() : Exit Sub
                End If

                Lv_Gedung.Location = New Point(700, 75)
                Lv_Gedung.Visible = False
            End Using

            Try
                OpenConn()

                '==============================
                '=     GENERATE KODE AREA     =
                '==============================
                Dim LastIdentity As Long = 0

                SQL = "select Top(1) Kode_Area "
                SQL = SQL & "from N_EMI_Master_Area_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and ID_Gedung = '" & Txt_IdGedung.Text & "' "
                SQL = SQL & "Order by cast(Kode_Area as integer) desc "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        LastIdentity = Dr("Kode_Area")
                    End If
                End Using

                LastIdentity += 1

                Dim KodeArea As String = LastIdentity.ToString("D3")

                Txt_NmArea.Text = KodeArea

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

        If Txt_Gedung.Text.Trim.Length = 0 Then
            MessageBox.Show("Gedung Harus Disi . . ! !", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Gedung.Focus() : Exit Sub
        ElseIf Txt_NmArea.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Area Harus Diisi . . ! !", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NmArea.Focus() : Exit Sub
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

                '=====================================
                '=     CEK APAKAH AREA DITEMUKAN     =
                '=====================================
                SQL = "select Kode_Perusahaan from N_EMI_Master_Gedung_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and ID_Gedung = '" & Txt_IdGedung.Text.Trim & "' "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        MessageBox.Show("Gedung Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Gedung.Text = "" : Txt_IdGedung.Text = ""
                        Txt_Gedung.Focus() : Exit Sub
                    End If
                End Using

                '=====================================================
                '=     CEK APAKAH ADA DATA DENGAN NAMA YANG SAMA     =
                '=====================================================
                SQL = "select Kode_Area "
                SQL = SQL & "from N_EMI_Master_Area_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and ID_Gedung = '" & Txt_IdGedung.Text & "' "
                SQL = SQL & "and Kode_Area = '" & Txt_NmArea.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Silakan Ulangi Transaksi ini . . ! !", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                '==============================
                '=     GENERATE KODE AREA     =
                '==============================
                Dim LastIdentity As Long = 0

                SQL = "select Top(1) Kode_Area "
                SQL = SQL & "from N_EMI_Master_Area_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and ID_Gedung = '" & Txt_IdGedung.Text & "' "
                SQL = SQL & "Order by cast(Kode_Area as integer) desc "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        LastIdentity = Dr("Kode_Area")
                    End If
                End Using

                LastIdentity += 1

                Dim KodeArea As String = LastIdentity.ToString("D3")


                '===========================
                '=     INSERT KELOMPOK     =
                '===========================
                SQL = "insert into N_EMI_Master_Area_Barang_Lain (Kode_Perusahaan, ID_Gedung, Kode_Area, Keterangan) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_IdGedung.Text & "', '" & KodeArea & "', '" & Txt_Keterangan.Text & "')  "
                ExecuteTrans(SQL)

                Action = "SIMPAN"


            ElseIf Btn_Simpan.Tag = "UPDATE" Then

                If MessageBox.Show("Yakin ingin Update Data Ini ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

                '===========================
                '=     UPDATE KATEGORI     =
                '===========================
                SQL = "select Kode_Area "
                SQL = SQL & "from N_EMI_Master_Area_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and ID_Area = '" & Txt_SelectedID.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()

                        SQL = "update N_EMI_Master_Area_Barang_Lain set ID_Gedung = '" & Txt_IdGedung.Text & "', Keterangan = '" & Txt_Keterangan.Text & "', Kode_Area = '" & Txt_NmArea.Text & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and ID_Area = '" & Txt_SelectedID.Text & "' "
                        ExecuteTrans(SQL)


                        Action = "UPDATE"

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Area tidak ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            End If


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            If Action = "SIMPAN" Then
                MessageBox.Show("Area Berhasil Ditambahkan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf Action = "UPDATE" Then
                MessageBox.Show("Area Berhasil Diupdate", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            MessageBox.Show("Pilih Dahulu Area yang Ingin Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_DataArea.Focus() : Exit Sub
        End If

        If MessageBox.Show("Yakin ingin Hapus Area " & Txt_NmArea.Text & " ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        Try
            OpenConn()

            '===============================================
            '=     CEK APAKAH KELOMPOK SUDAH DIGUNAKAN     =
            '===============================================
            SQL = "select top 1 Kode_Perusahaan from Barang_Lain_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and ID_Area = '" & Txt_SelectedID.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Area Tidak Bisa Dihapus Karena Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==========================
            '=     HAPUS KATEGORI     =
            '==========================
            SQL = "select Kode_Area "
            SQL = SQL & "from N_EMI_Master_Area_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and ID_Area = '" & Txt_SelectedID.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = "Delete N_EMI_Master_Area_Barang_Lain  "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and ID_Area = '" & Txt_SelectedID.Text & "' "
                    ExecuteTrans(SQL)

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Area tidak ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
            MessageBox.Show("Area Berhasil Dihapus", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()

    End Sub

    Private Sub Txt_Gedung_TextChanged(sender As Object, e As EventArgs) Handles Txt_Gedung.TextChanged
        If Txt_Gedung.Text.Trim.Length = 0 Then
            Lv_Gedung.Items.Clear()
            Lv_Gedung.Location = New Point(700, 75)
            Lv_Gedung.Visible = False
            Exit Sub
        Else
            Lv_Gedung.Items.Clear()
            Lv_Gedung.Location = New Point(130, 75)
            Lv_Gedung.Visible = True
        End If

        Try
            OpenConn()

            Lv_Gedung.Items.Clear()
            SQL = "select ID_Gedung, Kode_Gedung, Keterangan "
            SQL = SQL & "from N_EMI_Master_Gedung_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Keterangan like '%" & Txt_Gedung.Text & "%' "
            SQL = SQL & "order by ID_Gedung "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Gedung.Items.Add(Dr("ID_Gedung"))
                    Lv.SubItems.Add(Dr("Kode_Gedung"))
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

    Private Sub Lv_Kategori_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Gedung.DoubleClick
        If Lv_Gedung.Items.Count = 0 Or Lv_Gedung.FocusedItem.Index = -1 Then Exit Sub

        Dim IdGedung As String = Lv_Gedung.FocusedItem.SubItems(0).Text
        Dim NmGedung As String = Lv_Gedung.FocusedItem.SubItems(2).Text

        Txt_IdGedung.Text = IdGedung
        Txt_Gedung.Text = NmGedung

        Lv_Gedung.Location = New Point(700, 75)
        Lv_Gedung.Visible = False

        Txt_NmArea.Focus()
    End Sub

    Private Sub Lv_DataKelompok_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DataArea.DoubleClick

        If Lv_DataArea.Items.Count = 0 Or Lv_DataArea.FocusedItem Is Nothing Then Exit Sub

        Dim IdGedung As String = Lv_DataArea.FocusedItem.SubItems(1).Text
        Dim NmGedung As String = Lv_DataArea.FocusedItem.SubItems(2).Text

        Txt_SelectedID.Text = Lv_DataArea.FocusedItem.Text
        Txt_IdGedung.Text = IdGedung
        Txt_Gedung.Text = NmGedung
        Txt_NmArea.Text = Lv_DataArea.FocusedItem.SubItems(3).Text
        Txt_Keterangan.Text = Lv_DataArea.FocusedItem.SubItems(4).Text

        Btn_Simpan.Tag = "UPDATE"
        Btn_Simpan.Text = "&Update"

        Lv_Gedung.Location = New Point(700, 75)
        Lv_Gedung.Visible = False

        Txt_NmArea.Focus()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Txt_Gedung_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Gedung.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Gedung.Text.Trim.Length = 0 Then Txt_Gedung.Focus()
            Txt_Gedung_Leave(Txt_Gedung, e)

            Lv_Gedung.Location = New Point(700, 75)
            Lv_Gedung.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Gedung_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Gedung.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Gedung.Focus()
    End Sub

    Private Sub Lv_Kategori_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Gedung.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Kategori_DoubleClick(Lv_Gedung, e)
        End If
    End Sub

    Private Sub Txt_NmKelompok_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmArea.KeyPress
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