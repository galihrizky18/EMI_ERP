Public Class N_EMI_Master_Kategori_Barang_Lain


    Dim arrFilter As New ArrayList
    Dim JudulForm As String = "Master Kategori Barang Asset"


    Private Sub N_EMI_Master_Kategori_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_DataKategori.Columns.Clear() : Lv_DataKategori.Items.Clear()
        Lv_DataKategori.Columns.Add("idKategori", 0, HorizontalAlignment.Left)
        Lv_DataKategori.Columns.Add("Kode Kategori", 200, HorizontalAlignment.Left)
        Lv_DataKategori.Columns.Add("Keterangan", 400, HorizontalAlignment.Left)
        Lv_DataKategori.View = View.Details

        kosong()

    End Sub

    Private Sub kosong()

        Txt_NmKategori.Text = ""
        Txt_Keterangan.Text = ""
        Txt_SelectedID.Text = ""

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add("Nama Kategori") : arrFilter.Add("Nama")

        Txt_ValueFilter.Text = ""


        Btn_Simpan.Tag = "SIMPAN"
        Btn_Simpan.Text = "&Simpan"

        LoadDataKategori(False)

        Try
            OpenConn()


            '==================================
            '=     GENERATE KODE KATEGORI     =
            '==================================
            Dim LastIdentity As Long = 0
            Dim HasData As Boolean = False

            SQL = "select top(1) Kode_kategori "
            SQL = SQL & "from N_EMI_Master_Kategori_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "Order by cast(Kode_kategori as integer) desc"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    LastIdentity = Dr("Kode_kategori")
                End If
            End Using


            LastIdentity += 1


            Dim KodeKategori As String = LastIdentity.ToString("D2")

            Txt_NmKategori.Text = KodeKategori

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

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

        LoadDataKategori(True)

    End Sub

    Private Sub LoadDataKategori(ByVal filter As Boolean)

        Try
            OpenConn()

            Lv_DataKategori.Items.Clear()
            SQL = "select ID_Kategori, kode_kategori, Keterangan "
            SQL = SQL & "from N_EMI_Master_Kategori_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            If filter Then
                SQL = SQL & "and " & arrFilter(Cmb_Filter.SelectedIndex) & " like '%" & Txt_ValueFilter.Text & "%' "
            End If
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DataKategori.Items.Add(Dr("ID_Kategori"))
                    Lv.SubItems.Add(Dr("kode_kategori"))
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

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Txt_NmKategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Kategori Harus Diisi . . ! !", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NmKategori.Focus() : Exit Sub
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
                '=====================================================
                '=     CEK APAKAH ADA DATA DENGAN NAMA YANG SAMA     =
                '=====================================================
                SQL = "select top(1) kode_perusahaan "
                SQL = SQL & "from N_EMI_Master_Kategori_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_kategori = '" & Txt_NmKategori.Text & "' "
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
                '=     GENERATE KODE KATEGORI     =
                '==================================
                Dim LastIdentity As Long = 0
                SQL = "select top(1) Kode_kategori "
                SQL = SQL & "from N_EMI_Master_Kategori_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "Order by cast(Kode_kategori as integer) desc "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        LastIdentity = Dr("Kode_kategori")
                    End If
                End Using


                LastIdentity += 1


                Dim KodeKategori As String = LastIdentity.ToString("D2")

                '===========================
                '=     INSERT KATEGORI     =
                '===========================
                SQL = "insert into N_EMI_Master_Kategori_Barang_Lain (Kode_Perusahaan, Kode_Kategori, Keterangan) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NmKategori.Text & "', '" & Txt_Keterangan.Text & "'  "
                SQL = SQL & ")"
                ExecuteTrans(SQL)

                Action = "SIMPAN"


            ElseIf Btn_Simpan.Tag = "UPDATE" Then

                If MessageBox.Show("Yakin ingin Update Data Ini ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

                '===========================
                '=     UPDATE KATEGORI     =
                '===========================
                SQL = "select ID_Kategori, Keterangan "
                SQL = SQL & "from N_EMI_Master_Kategori_Barang_Lain "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and ID_Kategori = '" & Txt_SelectedID.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        Dr.Close()
                        SQL = "update N_EMI_Master_Kategori_Barang_Lain set Keterangan = '" & Txt_Keterangan.Text & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and ID_Kategori = '" & Txt_SelectedID.Text & "' "
                        ExecuteTrans(SQL)

                        Action = "UPDATE"

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Kategori tidak ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            End If



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            If Action = "SIMPAN" Then
                MessageBox.Show("Kategori Berhasil Ditambahkan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf Action = "UPDATE" Then
                MessageBox.Show("Kategori Berhasil Diupdate", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            MessageBox.Show("Pilih Dahulu Kategori yang Ingin Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_DataKategori.Focus() : Exit Sub
        End If

        If MessageBox.Show("Yakin ingin Hapus Kategori " & Txt_NmKategori.Text & " ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '===============================================
            '=     CEK APAKAH KATEGORI SUDAH DIGUNAKAN     =
            '===============================================
            SQL = "select top 1 Kode_Perusahaan from Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and ID_Kategori = '" & Txt_SelectedID.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kategori Tidak Bisa Dihapus Karena Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==========================
            '=     HAPUS KATEGORI     =
            '==========================
            SQL = "select ID_Kategori,  Keterangan "
            SQL = SQL & "from N_EMI_Master_Kategori_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and ID_Kategori = '" & Txt_SelectedID.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = "Delete N_EMI_Master_Kelompok_Barang_Lain "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and ID_Kategori = '" & Txt_SelectedID.Text & "' "
                    ExecuteTrans(SQL)

                    SQL = "Delete N_EMI_Master_Kategori_Barang_Lain  "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and ID_Kategori = '" & Txt_SelectedID.Text & "' "
                    ExecuteTrans(SQL)


                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kategori tidak ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Kategori Berhasil Dihapus", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()


    End Sub


    Private Sub Lv_DataKategori_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DataKategori.DoubleClick

        If Lv_DataKategori.Items.Count = 0 Or Lv_DataKategori.FocusedItem Is Nothing Then Exit Sub


        Txt_SelectedID.Text = Lv_DataKategori.FocusedItem.Text
        Txt_NmKategori.Text = Lv_DataKategori.FocusedItem.SubItems(1).Text
        Txt_Keterangan.Text = Lv_DataKategori.FocusedItem.SubItems(2).Text

        Btn_Simpan.Tag = "UPDATE"
        Btn_Simpan.Text = "&Update"

        Txt_NmKategori.Focus()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Txt_NmKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmKategori.KeyPress
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

    Private Sub Txt_NmKategori_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmKategori.TextChanged

    End Sub

End Class
