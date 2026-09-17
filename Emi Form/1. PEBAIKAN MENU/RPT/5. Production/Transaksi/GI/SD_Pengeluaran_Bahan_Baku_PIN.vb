Public Class SD_Pengeluaran_Bahan_Baku_PIN
    Private Sub SD_Pengeluaran_Bahan_Baku_PIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ComboBox1.Items.Clear()
            Accounts.Clear()

            OpenConn()

            SQL = $"
                SELECT 
                    a.kode_perusahaan AS KodePerusahaan,
                    a.Nama AS NamaPerusahaan,
                    b.userid AS UserID,
                    b.username AS UserName,
                    b.userlevel AS UserLevel
                FROM perusahaan a, users b
                WHERE a.kode_perusahaan = b.kode_perusahaan
                AND a.kode_perusahaan = '{KodePerusahaan}'
                AND b.userid <> '{UserID}'
            "

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim acc As New AccountInfo With {
                        .KodePerusahaan = Dr("KodePerusahaan").ToString(),
                        .NamaPerusahaan = Dr("NamaPerusahaan").ToString(),
                        .UserID = Dr("UserID").ToString(),
                        .UserName = Dr("UserName").ToString(),
                        .UserLevel = Dr("UserLevel").ToString(),
                        .IsDefaultUser = False
                    }
                    Accounts.Add(acc)
                End While
            End Using

            CloseConn()

            Dim defaultAcc As New AccountInfo With {
                .KodePerusahaan = KodePerusahaan,
                .NamaPerusahaan = NamaPerusahaan,
                .UserID = UserID,
                .UserName = UserName,
                .UserLevel = UserLevel,
                .IsDefaultUser = True
            }
            Accounts.Add(defaultAcc)

            For Each acc As AccountInfo In Accounts
                Dim displayText As String = $"{acc.UserID} - {acc.UserName}"
                ComboBox1.Items.Add(displayText)
            Next

            Dim selectedText As String = $"{UserID} - {UserName}"
            Dim index As Integer = ComboBox1.Items.IndexOf(selectedText)
            Label4.Text = UserID
            If index >= 0 Then ComboBox1.SelectedIndex = index

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        TextBox2.PasswordChar = "*"c
        AddHandler Keypad_PIN1.VisibilityToggled, AddressOf KeypadPIN_VisibilityToggled
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            If ComboBox1.SelectedIndex < 0 Then Exit Sub

            Dim selectedText As String = ComboBox1.SelectedItem.ToString()
            Dim parts = selectedText.Split("-"c)
            If parts.Length < 2 Then Exit Sub

            Dim selectedUserID As String = parts(0).Trim()
            Dim selectedUserName As String = parts(1).Trim()

            Dim acc = Accounts.FirstOrDefault(Function(a) a.UserID = selectedUserID)
            If acc.UserID Is Nothing Then Exit Sub

            UserName = acc.UserName
            UserLevel = acc.UserLevel
            KodePerusahaan = acc.KodePerusahaan
            NamaPerusahaan = acc.NamaPerusahaan
            UserID = acc.UserID

            Label4.Text = UserID
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Try
            Dim inputPin As String = TextBox2.Text.Trim()
            If String.IsNullOrEmpty(inputPin) Then
                MessageBox.Show("Silakan masukkan PIN terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            OpenConn()

            SQL = $"
                SELECT UserID 
                FROM Users 
                WHERE UserID = '{UserID}'
                AND Access_PIN = HASHBYTES('MD5', '{inputPin}')
            "

            Dim isValid As Boolean = False
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    isValid = True
                End If
            End Using

            CloseConn()

            If isValid Then
                DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("PIN salah. Silakan coba lagi.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox2.Clear()
                Keypad_PIN1.Reset()
                TextBox2.Focus()
            End If

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        AddHandler Keypad_PIN1.ValueChanged, AddressOf KeypadPIN_ValueChanged
        Keypad_PIN1.IsPasswordVisible = (TextBox2.PasswordChar = CChar(vbNullChar))
        Keypad_PIN1.Btn_Toggle_Visibility.Enabled = True
    End Sub

    Private Sub KeypadPIN_ValueChanged(ByVal newValue As String)
        If TextBox2 IsNot Nothing Then
            TextBox2.Text = newValue
            TextBox2.SelectionStart = TextBox2.Text.Length
        End If
    End Sub

    Private Sub KeypadPIN_VisibilityToggled(ByVal isVisible As Boolean)
        If TextBox2 IsNot Nothing Then
            If isVisible Then
                TextBox2.PasswordChar = CChar(vbNullChar)
            Else
                TextBox2.PasswordChar = "*"c
            End If
        End If
    End Sub

    Private Sub SD_Pengeluaran_Bahan_Baku_PIN_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Try
            Dim acc = Accounts.FirstOrDefault(Function(a) a.IsDefaultUser)

            If acc.UserID Is Nothing OrElse acc.UserID = "" Then
                MessageBox.Show("User default tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            UserName = acc.UserName
            UserLevel = acc.UserLevel
            KodePerusahaan = acc.KodePerusahaan
            NamaPerusahaan = acc.NamaPerusahaan
            UserID = acc.UserID
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
