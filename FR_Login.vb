Imports System.Data.SqlClient

Public Class FR_Login

    Private Sub FR_Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        ResetTextBox()
    End Sub
    Private Sub FR_Login_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ResetTextBox()
    End Sub

    Private Sub FR_Login_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        ResetTextBox()
    End Sub

    Private Sub ResetTextBox()
        Tb_Username.Text = String.Empty
        Tb_Password.Text = String.Empty
    End Sub

    Private Sub Tb_Username_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_Username.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then Tb_Password.Focus() : e.Handled = True
    End Sub
    Private Sub Tb_Password_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_Password.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then Btn_Login.Focus() : e.Handled = True
    End Sub



    Private Sub Btn_Login_Click(sender As Object, e As EventArgs) Handles Btn_Login.Click
        Dim username As String = Tb_Username.Text.Trim()
        Dim password As String = Tb_Password.Text.Trim()

        Dim userCheck As New DataTable

        If username.Length = 0 Then
            MessageBox.Show("Username Harus Di Isi", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf password.Length = 0 Then
            MessageBox.Show("Password Harus Di Isi", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim Sql As String = "SELECT TOP 1 users.UserID, users.UserName "
            Sql = Sql & "FROM users "
            Sql = Sql & "WHERE UserName = @Username "
            Sql = Sql & "AND Pass = @Password"

            Using cmd As New SqlCommand(Sql, Cn)

                'Pencegahan sql injection
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Password", password)

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    If dr.HasRows Then
                        userCheck.Load(dr)
                        UserID = userCheck.Rows(0).Item("UserID")
                        username = userCheck.Rows(0).Item("UserName")
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("User Tidak Ditemukan")
                        Exit Sub
                    End If
                End Using
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


        If userCheck.Rows.Count > 0 Then
            Me.Hide()
            Dim MenuForm As New Main_MenuDev()
            MenuForm.Show()
            MenuForm.Focus()
        Else
            MessageBox.Show("Username atau Password Salah", "Failed Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        '_UserID = "AYUNG"
        '_UserName = "AYUNG"

        'Dim MenuForm As New Main_Menu()
        'MenuForm.Show()
        'MenuForm.Focus()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CB_ShowPassword.CheckedChanged
        If CB_ShowPassword.Checked Then
            Tb_Password.PasswordChar = ""
        Else
            Tb_Password.PasswordChar = "*"
        End If
    End Sub
End Class