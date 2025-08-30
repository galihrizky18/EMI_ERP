Public Class EMI_Pengeluaran_Barang_Lain_Roles
    Dim Lv As ListViewItem
    Dim LKolom As New ArrayList

    Private Sub Pengeluaran_Barang_Roles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As New Point(102, 101)
        LvUser.Location = x

        Kosong()
        TxtUserID.Focus()
    End Sub

    Private Sub Kosong()
        LvDisplay.Columns(0).Width = 210
        LvDisplay.Columns(1).Width = 210

        TxtUserID.Text = ""

        LvRoles.Items.Clear()
        LvRoles.Items.Add("ATK")
        LvRoles.Items.Add("Asset")
        LvRoles.Items.Add("Sparepart")
        LvRoles.Items.Add("Packaging")
        LvRoles.Items.Add("Raw Material")
        LvRoles.Items.Add("Finished Good")
        LvRoles.Items.Add("Sample")
        LvRoles.Items.Add("Semi FG")
        LvRoles.Items.Add("Scrap")
        LvRoles.Items.Add("Bahan Bakar")
        LvRoles.Items.Add("Peralatan")

        LvDisplay.Items.Clear()

        CmbKolom.Items.Clear() : LKolom.Clear()
        CmbKolom.Items.Add("User ID") : LKolom.Add("UserID")
        CmbKolom.Items.Add("Nama Role") : LKolom.Add("Nama_Role")
        TxtValue.Text = ""
        BtnHapus.Enabled = False
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Kosong()
        TxtUserID.Focus()
    End Sub

    Private Sub TxtUserID_TextChanged(sender As Object, e As EventArgs) Handles TxtUserID.TextChanged
        If TxtUserID.Text.Trim.Length = 0 Then
            LvUser.Visible = False : Exit Sub
        Else
            LvUser.Visible = True
        End If

        Try
            OpenConn()

            LvUser.Items.Clear()

            SQL = "Select userid From users "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND userid LIKE '" & TxtUserID.Text & "%' "
            SQL = SQL & "ORDER BY userid"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = LvUser.Items.Add(Dr("userid"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub TxtUserID_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtUserID.KeyDown
        If e.KeyCode = Keys.Down Then LvUser.Focus()
    End Sub

    Private Sub TxtUserID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUserID.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtUserID.Text.Trim.Length = 0 Then LvRoles.Focus()
            TxtUserID_Leave(TxtUserID, e)
            LvUser.Visible = False
        End If
    End Sub

    Private Sub TxtUserID_Leave(sender As Object, e As EventArgs) Handles TxtUserID.Leave
        If TxtUserID.Text.Trim.Length = 0 Then Exit Sub
        If LvUser.Focused = True Then Exit Sub

        Try
            OpenConn()

            Dim Ketemu As Boolean = False

            SQL = "Select userid From users "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND userid = '" & TxtUserID.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtUserID.Text = Dr("userid")
                    Ketemu = True
                    BtnHapus.Enabled = True : LvRoles.Focus()
                Else
                    MessageBox.Show("User ID tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TxtUserID.Text = "" : TxtUserID.Focus() : BtnHapus.Enabled = False
                End If
                LvUser.Visible = False
            End Using

            For i As Integer = 0 To LvRoles.Items.Count - 1
                LvRoles.Items(i).Checked = False
            Next

            If Ketemu Then
                OpenConn()

                SQL = "Select nama_role From EMI_Pengeluaran_Barang_Lain_Roles "
                SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND userid = '" & TxtUserID.Text & "' "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        For i As Integer = 0 To LvRoles.Items.Count - 1
                            If Dr("nama_role").ToString.Trim.ToUpper = LvRoles.Items(i).Text.Trim.ToUpper Then
                                LvRoles.Items(i).Checked = True
                                Exit For
                            End If
                        Next
                    Loop
                End Using
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub LvRoles_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LvRoles.KeyPress
        If e.KeyChar = Chr(13) Then BtnSimpan.Focus()
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        If TxtUserID.Text.Trim.Length = 0 Then
            MessageBox.Show("User ID harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtUserID.Focus() : Exit Sub
        End If

        Dim Cek As String = ""
        For i As Integer = 0 To LvRoles.Items.Count - 1
            If LvRoles.Items(i).Checked Then
                Cek += "1"
            Else
                Cek += "0"
            End If
        Next
        If InStr(Cek, "1", vbTextCompare) = 0 Then
            MessageBox.Show("Roles harus dipilih salah satu!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LvRoles.Focus() : Exit Sub
        End If

        Dim simpan As String = MessageBox.Show("Anda yakin akan simpan data ini?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If simpan = vbNo Then Exit Sub

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            ExecuteTrans("Delete from EMI_Pengeluaran_Barang_Lain_Roles Where kode_perusahaan = '" & KodePerusahaan & "' and userid = '" & TxtUserID.Text & "'")

            For i As Integer = 0 To LvRoles.Items.Count - 1
                If LvRoles.Items(i).Checked Then
                    ExecuteTrans("Insert into EMI_Pengeluaran_Barang_Lain_Roles(kode_perusahaan,userid,nama_role) values('" & KodePerusahaan & "','" & TxtUserID.Text & "','" & LvRoles.Items(i).Text & "')")
                End If
            Next

            Cmd.Transaction.Commit()

            CloseConn()

            Kosong()
            TxtUserID.Focus()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles BtnHapus.Click
        If TxtUserID.Text.Trim.Length = 0 Then
            MessageBox.Show("User ID harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtUserID.Focus() : Exit Sub
        End If

        Dim hapus As String = MessageBox.Show("Anda yakin akan hapus data ini?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If hapus = vbNo Then Exit Sub

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            ExecuteTrans("Delete from EMI_Pengeluaran_Barang_Lain_Roles Where kode_perusahaan = '" & KodePerusahaan & "' and userid = '" & TxtUserID.Text & "'")

            Cmd.Transaction.Commit()

            CloseConn()

            Kosong()
            TxtUserID.Focus()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvUser_DoubleClick(sender As Object, e As EventArgs) Handles LvUser.DoubleClick
        Dim Kode As String = LvUser.FocusedItem.Text

        TxtUserID.Text = Kode

        LvUser.Visible = False
        TxtUserID_Leave(LvUser, e)
        LvRoles.Focus()
    End Sub

    Private Sub LvUser_KeyDown(sender As Object, e As KeyEventArgs) Handles LvUser.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvUser_DoubleClick(LvUser, e)
        End If
    End Sub

    Private Sub CmbKolom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKolom.KeyPress
        If e.KeyChar = Chr(13) Then TxtValue.Focus()
    End Sub

    Private Sub TxtValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtValue.KeyPress
        If e.KeyChar = Chr(13) Then BtnCari.Focus()
    End Sub

    Private Sub BtnCari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click
        If CmbKolom.SelectedIndex = -1 Then
            MessageBox.Show("Kolom pencarian harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbKolom.Focus() : Exit Sub
        ElseIf TxtValue.Text.Trim.Length = 0 Then
            MessageBox.Show("Value pencarian harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtValue.Focus() : Exit Sub
        End If

        LvDisplay.Items.Clear()

        Try
            OpenConn()

            SQL = "select UserID,Nama_Role from EMI_Pengeluaran_Barang_Lain_Roles "
            SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & LKolom(CmbKolom.SelectedIndex) & " like '%" & TxtValue.Text & "%' "
            SQL = SQL & "Order by userid,nama_role"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Lv = LvDisplay.Items.Add(dr("userid"))
                    Lv.SubItems.Add(dr("nama_role"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvDisplay_DoubleClick(sender As Object, e As EventArgs) Handles LvDisplay.DoubleClick
        If LvDisplay.Items.Count = 0 Then Exit Sub
        TxtUserID.Text = LvDisplay.FocusedItem.Text
        TxtUserID_Leave(LvDisplay, e)
    End Sub
End Class