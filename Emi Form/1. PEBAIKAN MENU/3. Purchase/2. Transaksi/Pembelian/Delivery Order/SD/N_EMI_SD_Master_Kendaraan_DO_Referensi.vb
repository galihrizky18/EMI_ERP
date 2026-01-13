Public Class N_EMI_SD_Master_Kendaraan_DO_Referensi
    Dim arrcari As New ArrayList

    Private Sub Master_Barang_Kategori_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListView1.Columns.Add("Plat", 220, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kapasitas", 150, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jenis", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Stnk Sendiri", 100, HorizontalAlignment.Center)
        ListView1.View = View.Details

        Kosong()
        TextBox1.Focus()
    End Sub

    Private Sub Kosong()
        Try

            OpenConn()

            ListView1.Items.Clear()
            SQL = "Select kode_mobil, kapasitas, jenis, Flag_STNK_Sendiri From kendaraan where kode_perusahaan = '" & KodePerusahaan & "' order by kode_mobil"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("kode_mobil"))
                    Lvw.SubItems.Add(dr("kapasitas"))
                    Lvw.SubItems.Add(dr("jenis"))
                    Lvw.SubItems.Add(dr("Flag_STNK_Sendiri"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add("Plat") : arrcari.Add("kode_mobil")
        ComboBox1.Items.Add("Kapasitas") : arrcari.Add("kapasitas")
        ComboBox1.Items.Add("Stnk Sendiri") : arrcari.Add("flag_stnk_sendiri")

        TextBox1.Text = "" : TextBox3.Text = "" : TextBox7.Text = ""

        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Sendiri")
        ComboBox2.Items.Add("Ekspedisi")

        cbStnkSendiri.Items.Clear()
        cbStnkSendiri.Items.Add("Y")
        cbStnkSendiri.Items.Add("T")

        Button1.Text = "&Simpan" : Button2.Enabled = False
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Kosong()
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
        If Not (e.KeyChar <> Chr(Asc("-")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        If TextBox1.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select Kode_Mobil, Kapasitas, Jenis, Flag_STNK_Sendiri From kendaraan Where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_mobil = '" & Trim(TextBox1.Text) & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("Kode_Mobil")
                    TextBox3.Text = Dr("Kapasitas")
                    ComboBox2.Text = Dr("Jenis")
                    cbStnkSendiri.Text = Dr("Flag_STNK_Sendiri")
                    Button1.Text = "&Update" : Button2.Enabled = True
                Else
                    TextBox3.Text = ""
                    ComboBox2.SelectedIndex = -1
                    cbStnkSendiri.SelectedIndex = -1
                    Button1.Text = "&Simpan" : Button2.Enabled = False
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Plat harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Kapasitas harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus() : Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Jml segel harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf cbStnkSendiri.SelectedIndex = -1 Then
            MessageBox.Show("Stnk Sendiri harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cbStnkSendiri.Focus() : Exit Sub
        End If

        Try

            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            If Button1.Text = "&Simpan" Then
                SQL = "Insert Into kendaraan(Kode_Perusahaan, kode_mobil, kapasitas, jenis, Flag_STNK_Sendiri) Values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & TextBox1.Text.Trim & "', "
                SQL = SQL & "'" & TextBox3.Text.Trim & "', '" & ComboBox2.Text.Trim & "', '" & cbStnkSendiri.Text.Trim & "')"
                ExecuteTrans(SQL)
            Else
                SQL = "Update kendaraan Set kapasitas = '" & TextBox3.Text.Trim & "', "
                SQL = SQL & "jenis = '" & ComboBox2.Text.Trim & "', Flag_STNK_Sendiri = '" & cbStnkSendiri.Text.Trim & "' where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_mobil = '" & TextBox1.Text.Trim & "'"
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then

            Try

                OpenConn()

                'Barang
                'SQL = "Select top 1 * from detail_pengiriman where kode_perusahaan = '" & KodePerusahaan & "' and kode_mobil = '" & TextBox1.Text.Trim & "'"
                'Using Dr1 = OpenTrans(SQL)
                '    If Dr1.Read Then
                '        MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        Kosong()
                '        TextBox1.Focus()
                '        CloseConn()
                '        Exit Sub
                '    End If
                'End Using

                'SQL = "Delete From kendaraan where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_mobil = '" & TextBox1.Text.Trim & "'"
                'ExecuteTrans(SQL)

                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        Kosong()
        TextBox1.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox2.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox7.Focus()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox7.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            ListView1.Items.Clear()
            Using Dr = OpenTrans("Select Kode_Mobil, Kapasitas, Jenis, Flag_STNK_Sendiri From kendaraan where kode_perusahaan = '" & KodePerusahaan & "' and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox7.Text & "%' order by kode_mobil")
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(Dr("Kode_Mobil"))
                    Lvw.SubItems.Add(Dr("Kapasitas"))
                    Lvw.SubItems.Add(Dr("Jenis"))
                    Lvw.SubItems.Add(Dr("Flag_STNK_Sendiri"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Chr(13) Then Button5_Click(TextBox7, e)
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub

        TextBox1.Text = ListView1.Items(ListView1.FocusedItem.Index).Text
        TextBox1_Leave(ListView1, e)
    End Sub

    Private Sub Master_Barang_Kategori_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

End Class