Imports System.Drawing.Text
Imports System.IO
Imports System.Net

Public Class Main_Menu

    Private Sub FR_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DataMenu As New DataTable

        DataMenu = GetLoadMenuFromDB(UserID)

        If DataMenu IsNot Nothing AndAlso DataMenu.Rows.Count > 0 Then
            LoadMenu(DataMenu)
        End If


    End Sub


    Private Function GetLoadMenuFromDB(ByVal UserID As String) As DataTable
        Dim data As New DataTable

        Try
            OpenConn()

            SQL = "select MainMenu.ImagePath, MainMenu.Title, RoleMainMenus.UserID, RoleMainMenus.MainMenuID from RoleMainMenus left join MainMenu "
            SQL = SQL & "On RoleMainMenus.MainMenuID = MainMenu.MainMenuID where RoleMainMenus.UserID ='" & UserID & "' "
            SQL = SQL & "order by MainMenuID"

            Using dr = OpenTrans(Sql)
                If dr.HasRows Then
                    data.Load(dr)
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Menu Tidak Ditemukan", "Failed Get Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Function
                End If
            End Using

            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Function
        End Try

        Return data
    End Function


    Private Sub LoadMenu(ByVal DataMenu As DataTable)
        Dim ParentPanel As Panel = FlowLayoutMenu_Main

        If DataMenu.Rows.Count > 0 Then

            For Each data As DataRow In DataMenu.Rows

                CreateNewPanelMenu(ParentPanel, data("ImagePath").ToString(), data("Title"), data("UserID").ToString(), data("MainMenuID").ToString())
            Next

        End If

    End Sub

    Private Sub CreateNewPanelMenu(ByVal ParentPanel As Panel, ByVal ImagePath As String, ByVal Title As String, ByVal UserID As String, ByVal MainMenuID As String)

        Dim newPanel As New Panel()
        newPanel.BorderStyle = BorderStyle.None
        newPanel.Width = 100
        newPanel.Height = 110
        newPanel.Margin = New Padding(7)
        newPanel.Cursor = Cursors.Hand

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

        'AddHandler picBox.Click, Sub(sender, e) ShowForm(IsFormExist(GetType(FMenu), UserID, MainMenuID))
        'AddHandler titleLabel.Click, Sub(sender, e) ShowForm(IsFormExist(GetType(FMenu), UserID, MainMenuID))

        AddHandler picBox.Click, Sub(sender, e) ShowForm(IsFormExist(FMenu, UserID, MainMenuID))
        AddHandler titleLabel.Click, Sub(sender, e) ShowForm(IsFormExist(FMenu, UserID, MainMenuID))


        ParentPanel.Controls.Add(newPanel)

    End Sub

    Private Function IsFormExist(ByVal formCheck As Form, ByVal _UserID As String, ByVal _MainMenuID As String) As Form

        Try
            OpenConn()

            SQL = "select * from menus a, RoleMenus b "
            SQL = SQL & "where a.MenuID=b.MenuID "
            SQL = SQL & "and b.UserID='" & _UserID & "' "
            SQL = SQL & "and a.MainMenuID='" & _MainMenuID & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.HasRows Then
                    UserID = _UserID
                    MainMenuID = _MainMenuID

                    Return formCheck
                Else
                    Return Nothing
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try

    End Function


    Private Sub ShowForm(ByVal form As Form)

        If form Is Nothing Then Exit Sub

        Me.Hide()
        form.StartPosition = FormStartPosition.CenterScreen
        form.Show()
        form.Focus()

    End Sub

    'Private Sub ShowForm(ByVal form As Form, ByVal a As String, ByVal b As String)

    '    Me.Hide()

    '    If Not String.IsNullOrWhiteSpace(a) AndAlso Not String.IsNullOrWhiteSpace(b) Then
    '        Dim propertyInfo = form.GetType().GetProperty(a)
    '        If propertyInfo IsNot Nothing AndAlso propertyInfo.CanWrite Then
    '            propertyInfo.SetValue(form, b)
    '        End If
    '    End If

    '    form.StartPosition = FormStartPosition.CenterScreen
    '    form.Show()
    '    form.Focus()
    'End Sub

    'Private Sub MouseEntered_Panel(sender As Object, e As EventArgs)
    '    Dim panel As Panel

    '    ' Cek apakah sender adalah Panel, jika iya, langsung cast
    '    If TypeOf sender Is Panel Then
    '        panel = CType(sender, Panel)
    '    ElseIf TypeOf sender Is PictureBox OrElse TypeOf sender Is Label Then
    '        ' Jika sender adalah PictureBox atau Label, gunakan Parent untuk mengakses Panel
    '        panel = CType(CType(sender, Control).Parent, Panel)
    '    End If

    '    ' Ubah warna background panel jika panel ditemukan
    '    If panel IsNot Nothing Then
    '        panel.BackColor = Color.FromArgb(231, 231, 231)
    '    End If
    'End Sub

    'Private Sub MouseLeft_Panel(sender As Object, e As EventArgs)
    '    Dim panel As Panel

    '    ' Cek apakah sender adalah Panel, jika iya, langsung cast
    '    If TypeOf sender Is Panel Then
    '        panel = CType(sender, Panel)
    '    ElseIf TypeOf sender Is PictureBox OrElse TypeOf sender Is Label Then
    '        ' Jika sender adalah PictureBox atau Label, gunakan Parent untuk mengakses Panel
    '        panel = CType(CType(sender, Control).Parent, Panel)
    '    End If

    '    ' Kembalikan warna background panel ke warna asli jika panel ditemukan
    '    If panel IsNot Nothing Then
    '        panel.BackColor = SystemColors.Control
    '    End If
    'End Sub


    Private Sub Main_Menu_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        End
    End Sub
End Class