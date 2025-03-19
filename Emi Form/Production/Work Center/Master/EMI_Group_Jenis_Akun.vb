
Public Class EMI_Group_Jenis_Akun
    Dim arrid_group_jenis As New ArrayList
    Private Sub EMI_Group_Jenis_Akun_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView1.Columns.Add("Lokasi Gudang", 200, HorizontalAlignment.Center)
        ListView1.Columns.Add("Akun Persediaan", 200)
        ListView1.View = View.Details

        ListView2.Columns.Add("Kode Account", 150, HorizontalAlignment.Center)
        ListView2.Columns.Add("Keterangan", 250)
        ListView2.View = View.Details
        Dim x As New Point(174, 111)
        ListView2.Location = x
        ListView2.Visible = False

        Kosong()
    End Sub
    Private Sub Kosong()
        Try
            OpenConn()

            ComboBox1.Items.Clear() : arrid_group_jenis.Clear()
            SQL = "select Id_Group_Jenis,Kode_Group_Jenis from EMI_Group_Jenis where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Group_Jenis "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox1.Items.Add(Dr("Kode_Group_Jenis"))
                    arrid_group_jenis.Add(Dr("Id_Group_Jenis"))
                Loop
            End Using

            ListView1.Items.Clear()
            'SQL = "select Kode_Stock_Owner from Stock_Owner_Gudang where Kode_Perusahaan = '" & KodePerusahaan & "' "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim lvi As ListViewItem
            '        lvi = ListView1.Items.Add(Dr("Kode_Stock_Owner"))
            '        lvi.SubItems.Add("")
            '    Loop
            'End Using

            TextBox3.Text = ""
            TextBox4.Text = ""
            CheckBox1.Checked = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub EMI_Group_Jenis_Akun_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Down Then
            ListView2.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox3.Text.Trim.Length = 0 Then
                ListView2.Visible = False : TextBox4.Focus() : Exit Sub
            End If
            TextBox3_Leave(TextBox3, e)
        End If
    End Sub

    Private Sub TextBox3_Leave(sender As Object, e As EventArgs) Handles TextBox3.Leave
        If TextBox3.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If
        If ListView2.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "select Kode_Account as Kode_Account, Keterangan, Posisi from "
            SQL = SQL & "detail_account where kode_perusahaan = '" & KodePerusahaan & "' and right(kode_detail_acc,3) <> '000' and "
            SQL = SQL & "Kode_Account = '" & TextBox3.Text.Trim & "' "
            SQL = SQL & "order by keterangan"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox3.Text = Dr("Kode_Account")
                    TextBox4.Text = Dr("Keterangan")
                    CheckBox1.Focus()
                Else
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    TextBox3.Focus()
                End If

                ListView2.Visible = False
            End Using

            For a As Integer = 0 To ListView1.Items.Count - 1
                SQL = "select Kode_Stock_Owner from EMI_Group_Jenis_Akun where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Group_Jenis = '" & arrid_group_jenis.Item(ComboBox1.SelectedIndex) & "' "
                SQL = SQL & "and Akun_Persediaan = '" & TextBox3.Text & "' and Kode_Stock_Owner = '" & ListView1.Items(a).Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        ListView1.Items(a).Checked = True
                    Else
                        ListView1.Items(a).Checked = False
                    End If
                End Using
            Next

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If


        Try
            OpenConn()

            ListView2.Items.Clear()
            SQL = "select top(75) Kode_Account as Kode_Account, Keterangan, Posisi from "
            SQL = SQL & "detail_account where kode_perusahaan = '" & KodePerusahaan & "' and right(kode_detail_acc,3) <> '000' and "
            SQL = SQL & "keterangan like '%" & TextBox3.Text & "%' "
            SQL = SQL & "order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView2.Items.Add(Dr("Kode_Account"))
                    Lvw.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub
        Dim kode As String = ListView2.FocusedItem.Text
        Dim nama As String = ListView2.FocusedItem.SubItems(1).Text
        TextBox3.Text = kode
        TextBox4.Text = nama
        ListView2.Visible = False
        CheckBox1.Focus()

        Try
            OpenConn()

            For a As Integer = 0 To ListView1.Items.Count - 1
                SQL = "select Kode_Stock_Owner from EMI_Group_Jenis_Akun where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Group_Jenis = '" & arrid_group_jenis.Item(ComboBox1.SelectedIndex) & "' "
                SQL = SQL & "and Akun_Persediaan = '" & TextBox3.Text & "' and Kode_Stock_Owner = '" & ListView1.Items(a).Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        ListView1.Items(a).Checked = True
                    Else
                        ListView1.Items(a).Checked = False
                    End If
                End Using
            Next

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            For a As Integer = 0 To ListView1.Items.Count - 1
                ListView1.Items(a).Checked = True
            Next
        Else
            For a As Integer = 0 To ListView1.Items.Count - 1
                ListView1.Items(a).Checked = False
            Next
        End If
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then ListView1.Focus()
    End Sub

    Private Sub ListView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ListView1.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

    Private Sub Button1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Button1.KeyPress
        If e.KeyChar = Chr(13) Then Button3.Focus()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Group jenis harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus() : Exit Sub
        ElseIf TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Akun Persediaan harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus() : Exit Sub
        End If

        Dim fsimpan As String = "T"
        For a As Integer = 0 To ListView1.Items.Count - 1
            If ListView1.Items(a).Checked = True Then
                fsimpan = "Y"
                Exit For
            End If
        Next

        If fsimpan = "T" Then
            MessageBox.Show("Lokasi gudang belum dipilih . . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            'SQL = "delete from EMI_Group_Jenis_Akun where Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and Id_Group_Jenis = '" & arrid_group_jenis.Item(ComboBox1.SelectedIndex) & "' "
            'SQL = SQL & "and Akun_Persediaan = '" & TextBox3.Text & "' "
            'ExecuteTrans(SQL)

            For a As Integer = 0 To ListView1.Items.Count - 1
                If ListView1.Items(a).Checked = True Then
                    If ListView1.Items(a).SubItems(1).Text <> TextBox3.Text And ListView1.Items(a).SubItems(1).Text <> "-" Then
                        SQL = "update EMI_Group_Jenis_Akun set Akun_Persediaan = '" & TextBox3.Text & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "Id_Group_Jenis = '" & arrid_group_jenis.Item(ComboBox1.SelectedIndex) & "' and "
                        SQL = SQL & "Kode_Stock_Owner = '" & ListView1.Items(a).Text & "' "
                        ExecuteTrans(SQL)
                    ElseIf ListView1.Items(a).SubItems(1).Text <> TextBox3.Text And ListView1.Items(a).SubItems(1).Text = "-" Then
                        SQL = "insert into EMI_Group_Jenis_Akun(Kode_Perusahaan,Id_Group_Jenis,Kode_Stock_Owner,Akun_Persediaan) values"
                        SQL = SQL & "('" & KodePerusahaan & "','" & arrid_group_jenis.Item(ComboBox1.SelectedIndex) & "',"
                        SQL = SQL & "'" & ListView1.Items(a).Text & "','" & TextBox3.Text & "')"
                        ExecuteTrans(SQL)
                    ElseIf ListView1.Items(a).SubItems(1).Text = TextBox3.Text Then
                        SQL = "update EMI_Group_Jenis_Akun set Akun_Persediaan = '" & TextBox3.Text & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "Id_Group_Jenis = '" & arrid_group_jenis.Item(ComboBox1.SelectedIndex) & "' and "
                        SQL = SQL & "Kode_Stock_Owner = '" & ListView1.Items(a).Text & "' "
                        ExecuteTrans(SQL)
                    End If
                End If
            Next

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Kosong()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.Kode_Stock_Owner, "
            SQL = SQL & "ISNULL((select b.Akun_Persediaan from EMI_Group_Jenis_Akun b where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner and "
            SQL = SQL & "b.Id_Group_Jenis = '" & arrid_group_jenis.Item(ComboBox1.SelectedIndex) & "' ),'-') as Akun_Persedian "
            SQL = SQL & "from Stock_Owner_Gudang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvi As ListViewItem
                    lvi = ListView1.Items.Add(Dr("Kode_Stock_Owner"))
                    lvi.SubItems.Add(Dr("Akun_Persedian"))
                Loop
            End Using

            CheckBox1.Checked = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class