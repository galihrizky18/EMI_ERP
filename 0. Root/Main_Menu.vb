Imports System.Net

Public Class Main_Menu

    Private Sub FR_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DataMenu As New DataTable

        DataMenu = GetLoadMenuFromDB(UserID)

        LoadMenu(DataMenu)

    End Sub


    Private Function GetLoadMenuFromDB(ByVal UserID As String) As DataTable
        Dim data As New DataTable

        Try
            OpenConn()

            'SQL = "select MainMenu.ImagePath, MainMenu.Title, RoleMainMenus.UserID, RoleMainMenus.MainMenuID from RoleMainMenus left join MainMenu "
            'SQL = SQL & "On RoleMainMenus.MainMenuID = MainMenu.MainMenuID where RoleMainMenus.UserID ='" & UserID & "' order by urut"

            SQL = "select a.MainMenuID, a.ImagePath, a.Title, "
            SQL = SQL & "ISNULL((select 'Y' from RoleMainMenus z where a.MainMenuID = z.MainMenuID and z.UserID = '" & UserID & "'), 'T') as Akses "
            SQL = SQL & "from mainmenu a "
            SQL = SQL & "order by akses desc, urut "
            Using dr = OpenTrans(Sql)
                If dr.HasRows Then
                    data.Load(dr)
                Else
                    MessageBox.Show("Menu Tidak Ditemukan", "Failed Get Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try

        Return data
    End Function


    Private Sub LoadMenu(ByVal DataMenu As DataTable)
        Dim ParentPanel As Panel = FlowLayoutMenu_Main

        If DataMenu.Rows.Count > 0 Then

            For Each data As DataRow In DataMenu.Rows

                Dim MainMenuId As String = data("MainMenuID")

                '=======================================
                '=     CEK APAKAH USER BOLEH AKSES     =
                '=======================================
                Dim isAccess As Boolean = False
                If data("Akses") = "Y" Then
                    isAccess = True
                Else
                    isAccess = False
                End If

                'Dim appDirectory As String = AppDomain.CurrentDomain.BaseDirectory
                'Dim imagePath As String = Path.Combine(appDirectory, "Assets", data("ImagePath"))

                'CreateNewPanelMenu(ParentPanel, data("ImagePath").ToString(), data("Title"), data("UserID").ToString(), data("MainMenuID").ToString(), isAccess)
                CreateNewPanelMenu(ParentPanel, data("ImagePath").ToString(), data("Title"), UserID, data("MainMenuID").ToString(), isAccess)
            Next

        End If

    End Sub

    Private Sub CreateNewPanelMenu(ByVal ParentPanel As Panel, ByVal ImagePath As String, ByVal Title As String, ByVal UserID As String, ByVal MainMenuID As String, ByVal isAccess As Boolean)

        Dim newPanel As New Panel()
        newPanel.BorderStyle = BorderStyle.None
        newPanel.Width = 100
        newPanel.Height = 110
        newPanel.Margin = New Padding(7)


        Dim picBox As New PictureBox()
        Dim imageUrl = ImagePath

        If Not String.IsNullOrWhiteSpace(imageUrl) Then
            Try
                Dim request As WebRequest = WebRequest.Create(imageUrl)
                Using response As WebResponse = request.GetResponse()
                    Using stream As IO.Stream = response.GetResponseStream()
                        picBox.Image = Image.FromStream(stream)
                    End Using
                End Using
            Catch ex As Exception

                Dim whiteBitmap As New Bitmap(70, 70)
                Using g As Graphics = Graphics.FromImage(whiteBitmap)
                    g.Clear(Color.White)
                End Using

                picBox.Image = whiteBitmap
            End Try
        Else
            Dim whiteBitmap As New Bitmap(70, 70)
            Using g As Graphics = Graphics.FromImage(whiteBitmap)
                g.Clear(Color.White)
            End Using

            picBox.Image = whiteBitmap

        End If

        picBox.SizeMode = PictureBoxSizeMode.StretchImage
        picBox.Width = 70
        picBox.Height = 70
        picBox.Top = 10
        picBox.Left = (newPanel.Width - picBox.Width) / 2

        Dim titleLabel As New Label()
        titleLabel.Text = Title
        titleLabel.Font = New Font("Tahoma", 8, FontStyle.Regular)
        titleLabel.ForeColor = Color.Black
        titleLabel.Top = picBox.Bottom + 5
        titleLabel.AutoSize = False
        titleLabel.Width = TextRenderer.MeasureText(Title, titleLabel.Font).Width
        titleLabel.Height = 20
        titleLabel.Top = picBox.Bottom + 5
        titleLabel.Left = (newPanel.Width - titleLabel.Width) / 2

        newPanel.Controls.Add(picBox)
        newPanel.Controls.Add(titleLabel)

        If isAccess Then

            newPanel.Cursor = Cursors.Hand

            AddHandler picBox.Click, Sub(sender, e) ShowForm(IsFormExist(FMenu, UserID, MainMenuID))
            AddHandler titleLabel.Click, Sub(sender, e) ShowForm(IsFormExist(FMenu, UserID, MainMenuID))

            AddHandler newPanel.MouseEnter, AddressOf MouseEntered_Panel
            AddHandler newPanel.MouseLeave, AddressOf MouseLeft_Panel
            AddHandler picBox.MouseEnter, AddressOf MouseEntered_Panel
            AddHandler picBox.MouseLeave, AddressOf MouseLeft_Panel
            AddHandler titleLabel.MouseEnter, AddressOf MouseEntered_Panel
            AddHandler titleLabel.MouseLeave, AddressOf MouseLeft_Panel
        Else
            newPanel.BackColor = Color.FromArgb(231, 231, 231)

        End If



        ParentPanel.Controls.Add(newPanel)

    End Sub

    Private Function IsFormExist(ByVal formCheck As Form, ByVal _UserID As String, ByVal _MainMenuID As String) As Form
        ' Cek apakah DiscusForm sudah ada
        'Dim existingForm As Form = Nothing

        '' Loop untuk mencari form yang sudah terbuka
        'For Each form As Form In Application.OpenForms
        '    If form.GetType() Is formCheck Then
        '        existingForm = form
        '        Exit For
        '    End If
        'Next
        UserID = _UserID
        MainMenuID = _MainMenuID

        Return formCheck


        '' Jika form tidak ditemukan, buat instance baru dengan konstruktor yang memiliki parameter
        'If existingForm Is Nothing Then
        '    'Dim newForm As Form = CType(Activator.CreateInstance(formCheck, Role, MainMenuID), Form) 'convert object baru ke form

        '    UserID = _UserID
        '    MainMenuID = _MainMenuID

        '    Dim newForm As New FMenu()
        '    newForm.Show()
        '    Return newForm
        'Else
        '    ' Jika form sudah ada buka formnya
        '    existingForm.BringToFront()
        '    existingForm.Focus()
        '    Return existingForm
        'End If
    End Function


    Private Sub ShowForm(ByVal form As Form)

        form.StartPosition = FormStartPosition.CenterScreen
        form.Show()
        form.Focus()
        Me.Hide()
    End Sub

    Private Sub MouseEntered_Panel(sender As Object, e As EventArgs)
        Dim panel As Panel

        ' Cek apakah sender adalah Panel, jika iya, langsung cast
        If TypeOf sender Is Panel Then
            panel = CType(sender, Panel)
        ElseIf TypeOf sender Is PictureBox OrElse TypeOf sender Is Label Then
            ' Jika sender adalah PictureBox atau Label, gunakan Parent untuk mengakses Panel
            panel = CType(CType(sender, Control).Parent, Panel)
        End If

        ' Ubah warna background panel jika panel ditemukan
        If panel IsNot Nothing Then
            panel.BackColor = Color.FromArgb(231, 231, 231)
        End If
    End Sub

    Private Sub MouseLeft_Panel(sender As Object, e As EventArgs)
        Dim panel As Panel

        ' Cek apakah sender adalah Panel, jika iya, langsung cast
        If TypeOf sender Is Panel Then
            panel = CType(sender, Panel)
        ElseIf TypeOf sender Is PictureBox OrElse TypeOf sender Is Label Then
            ' Jika sender adalah PictureBox atau Label, gunakan Parent untuk mengakses Panel
            panel = CType(CType(sender, Control).Parent, Panel)
        End If

        ' Kembalikan warna background panel ke warna asli jika panel ditemukan
        If panel IsNot Nothing Then
            panel.BackColor = SystemColors.Control
        End If
    End Sub


    Private Sub Main_Menu_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

        FMenu.Show()
        FMenu.Focus()
        Me.Hide()
    End Sub
End Class