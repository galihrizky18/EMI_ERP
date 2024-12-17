Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock
Imports System.Windows.Markup

Public Class Master_Role

    Dim _UserID As String
    Dim KodePerusahaan As String = "001"

    Dim arrUser, arrMainMenu, arrMenu, arrSubMenu, arrSubMenuLv1, arrSubMenuLv2, arrSubMenuLv3 As New ArrayList
    Dim tmp_mainmenu, tmp_menu, tmp_submenu, tmp_submenu1, tmp_submenu2, tmp_submenu3 As New ArrayList
    Dim LvMainMenuID, LvMenuID, LvSubMenuID, LvSubMenuLv1ID, LvSubMenuLv2ID, LvSubMenuLv3ID As String

    Dim itemMainMenuIDRole As Integer = 7
    Dim itemMenuIDRole As Integer = 8
    Dim itemSubMenuIDRole As Integer = 9
    Dim itemSubMenuLv1IDRole As Integer = 10
    Dim itemSubMenuLv2IDRole As Integer = 11
    Dim itemSubMenuLv3IDRole As Integer = 12

    Private Sub Master_Role_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        kosong()
        kosongUser()
        kosongArr()
        LoadLvRole()

    End Sub


    'HANDLE RESET
    Private Sub kosong()
        Lv_Role.Items.Clear()
    End Sub

    Private Sub kosongUser()
        Cb_Users.Items.Clear()
        Tb_UserName.Text = String.Empty
        Tb_UserName.Enabled = False

        Try
            OpenConn()

            arrUser.Clear()

            SQL = "select UserID, UserName, UserLevel from users"

            Using dr = OpenTrans(SQL)
                If dr.HasRows Then

                    Do While dr.Read
                        Cb_Users.Items.Add(dr("UserName")) : arrUser.Add(dr("UserID"))
                    Loop
                    CloseTrans()
                Else
                    dr.Close()
                    CloseTrans()
                    MessageBox.Show("Failed Get Users...")
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show("Failed Connect...")
        End Try

    End Sub

    Private Sub kosongArr()
        arrMainMenu.Clear()
        arrMenu.Clear()
        arrSubMenu.Clear()
        arrSubMenuLv1.Clear()
        arrSubMenuLv2.Clear()
        arrSubMenuLv3.Clear()

    End Sub

    Private Sub LoadLvRole()
        'SHOW TO LV
        Lv_Role.Columns.Add("", 30, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("Title", 150, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("Menu Name", 150, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("SubMenu Name", 150, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("SubMenuLv1 Name", 150, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("SubMenuLv2 Name", 150, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("SubMenuLv3 Name", 150, HorizontalAlignment.Center)

        'HIDDEN COLUMNS
        Lv_Role.Columns.Add("MainMenuID", 0, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("MenuID", 0, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("SubMenuID", 0, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("SubMenuLv1ID", 0, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("SubMenuLv2ID", 0, HorizontalAlignment.Center)
        Lv_Role.Columns.Add("SubMenuLv3ID", 0, HorizontalAlignment.Center)
        Lv_Role.View = View.Details


    End Sub


    'HANDLE BUTTON CLICK
    Private Sub Btn_Search_Click(sender As Object, e As EventArgs) Handles Btn_Search.Click
        If Cb_Users.SelectedIndex = -1 Then
            Exit Sub
        End If

        kosong()

        If Not String.IsNullOrWhiteSpace(arrUser.Item(Cb_Users.SelectedIndex)) Then
            LoadAllRoleMenus()
        End If

    End Sub

    Private Sub Btn_Save_Click(sender As Object, e As EventArgs) Handles Btn_Save.Click
        If String.IsNullOrWhiteSpace(KodePerusahaan) Then Exit Sub
        Get_Data_Lv_Checked()

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            SQL = "delete from RoleMainMenus where userid='" & _UserID & "'"
            ExecuteTrans(SQL)

            SQL = "delete from RoleMenus where userid='" & _UserID & "'"
            ExecuteTrans(SQL)

            SQL = "delete from RoleSubMenu where userid='" & _UserID & "'"
            ExecuteTrans(SQL)

            SQL = "delete from RoleSubMenuLv1 where userid='" & _UserID & "'"
            ExecuteTrans(SQL)

            SQL = "delete from RoleSubMenuLv2 where userid='" & _UserID & "'"
            ExecuteTrans(SQL)

            SQL = "delete from RoleSubMenuLv3 where userid='" & _UserID & "'"
            ExecuteTrans(SQL)

            tmp_mainmenu.Clear()
            tmp_menu.Clear()
            tmp_submenu.Clear()
            tmp_submenu1.Clear()
            tmp_submenu2.Clear()
            tmp_submenu3.Clear()

            For i As Integer = 0 To Lv_Role.Items.Count - 1
                If Lv_Role.Items(i).Checked = True Then

                    If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuLv3IDRole).Text) Then
                        Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuLv3IDRole).Text

                        Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
                            DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
                            DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

                        If Not tmp_submenu3.Contains(Data) Then
                            SQL = "insert into RoleSubMenuLv3 (RoleSubMenulv3ID, UserID, Kode_Perusahaan, SubMenuLv3ID) "
                            SQL = SQL & "values ('RoleSL3_" & uniqueID & "', '" & _UserID & "', "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

                            ExecuteTrans(SQL)

                            tmp_mainmenu.Add(Data)
                        End If

                    End If

                    If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuLv2IDRole).Text) Then
                        Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuLv2IDRole).Text

                        Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
                            DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
                            DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

                        If Not tmp_submenu2.Contains(Data) Then
                            SQL = "insert into RoleSubMenuLv2 (RoleSubMenulv2ID, UserID, Kode_Perusahaan, SubMenuLv2ID) "
                            SQL = SQL & "values ('RoleSL2_" & uniqueID & "', '" & _UserID & "', "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

                            ExecuteTrans(SQL)

                            tmp_submenu2.Add(Data)

                        End If

                    End If

                    If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuLv1IDRole).Text) Then
                        Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuLv1IDRole).Text

                        Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
                            DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
                            DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

                        If Not tmp_submenu1.Contains(Data) Then
                            SQL = "insert into RoleSubMenuLv1 (RoleSubMenulv1ID, UserID, Kode_Perusahaan, SubMenuLv1ID) "
                            SQL = SQL & "values ('RoleSL1_" & uniqueID & "', '" & _UserID & "', "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

                            ExecuteTrans(SQL)

                            tmp_submenu1.Add(Data)

                        End If

                    End If

                    If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuIDRole).Text) Then
                        Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuIDRole).Text

                        Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
                            DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
                            DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

                        If Not tmp_submenu.Contains(Data) Then

                            SQL = "insert into RoleSubMenu (RoleSubMenuID, UserID, Kode_Perusahaan, SubMenuID) "
                            SQL = SQL & "values ('RoleS_" & uniqueID & "', '" & _UserID & "', "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

                            ExecuteTrans(SQL)

                            tmp_submenu.Add(Data)

                        End If

                    End If

                    If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemMenuIDRole).Text) Then
                        Dim Data As String = Lv_Role.Items(i).SubItems(itemMenuIDRole).Text

                        Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
                            DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
                            DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

                        If Not tmp_menu.Contains(Data) Then
                            SQL = "insert into RoleMenus (RoleMenuID, UserID, Kode_Perusahaan, MenuID) "
                            SQL = SQL & "values ('RoleM_" & uniqueID & "', '" & _UserID & "', "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

                            ExecuteTrans(SQL)

                            tmp_menu.Add(Data)

                        End If

                    End If

                    If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text) Then
                        Dim Data As String = Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text

                        Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
                            DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
                            DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

                        If Not tmp_mainmenu.Contains(Data) Then
                            SQL = "insert into RoleMainMenus (RoleMainMenuID, UserID, Kode_Perusahaan, MainMenuID) "
                            SQL = SQL & "values ('RoleMM_" & uniqueID & "', '" & _UserID & "', "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

                            ExecuteTrans(SQL)

                            tmp_mainmenu.Add(Data)
                        End If

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

        Refresh()

    End Sub


    'HANDLE LOAD ROLE MENUS
    Private Sub LoadAllRoleMenus()
        If String.IsNullOrWhiteSpace(_UserID) Then Exit Sub

        Lv_Role.Items.Clear()

        Try
            OpenConn()

            Dim SQL As String
            SQL = "WITH CTE_A AS ( "
            SQL = SQL & "SELECT a.MainMenuID, a.TItle, b.MenuID, b.MenuName, c.SubMenuID, c.SubMenuName, d.SubMenuLv1ID, d.SubMenuLv1Name, e.SubMenuLv2ID, e.SubMenuLv2Name, f.SubMenuLv3ID, f.SubMenuLv3Name "
            SQL = SQL & "FROM MainMenu a "
            SQL = SQL & "LEFT JOIN Menus b ON a.MainMenuID = b.MainMenuID "
            SQL = SQL & "LEFT JOIN SubMenus c ON b.MenuID = c.MenuID "
            SQL = SQL & "LEFT JOIN SubMenuLv1 d ON d.SubMenuID = c.SubMenuID "
            SQL = SQL & "LEFT JOIN SubMenuLv2 e ON e.SubMenuLv1ID = d.SubMenuLv1ID "
            SQL = SQL & "LEFT JOIN SubMenuLv3 f ON f.SubMenuLv2ID = e.SubMenuLv2ID "
            SQL = SQL & ") "
            SQL = SQL & "SELECT a.MainMenuID, a.TItle, a.MenuID, a.MenuName, a.SubMenuID, a.SubMenuName, a.SubMenuLv1ID, a.SubMenuLv1Name, a.SubMenuLv2ID, a.SubMenuLv2Name, a.SubMenuLv3ID, a.SubMenuLv3Name, "
            SQL = SQL & "CASE "
            SQL = SQL & "WHEN a.SubMenuLv3ID IS NOT NULL THEN CASE WHEN g.RoleSubMenuLv3ID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
            SQL = SQL & "WHEN a.SubMenuLv2ID IS NOT NULL THEN CASE WHEN f.RoleSubMenuLv2ID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
            SQL = SQL & "WHEN a.SubMenuLv1ID IS NOT NULL THEN CASE WHEN e.RoleSubMenuLv1ID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
            SQL = SQL & "WHEN a.SubMenuID IS NOT NULL THEN CASE WHEN d.RoleSubMenuID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
            SQL = SQL & "WHEN a.MenuID IS NOT NULL THEN CASE WHEN c.RoleMenuID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
            SQL = SQL & "ELSE CASE WHEN b.RoleMainMenuID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
            SQL = SQL & "END AS StatusAccess "
            SQL = SQL & "FROM CTE_A a "
            SQL = SQL & "LEFT JOIN RoleMainMenus b ON a.MainMenuID = b.MainMenuID AND b.UserID = '" & _UserID & "' "
            SQL = SQL & "LEFT JOIN RoleMenus c ON a.MenuID = c.MenuID AND c.UserID = '" & _UserID & "' "
            SQL = SQL & "LEFT JOIN RoleSubMenu d ON a.SubMenuID = d.SubMenuID AND d.UserID = '" & _UserID & "' "
            SQL = SQL & "LEFT JOIN RoleSubMenuLv1 e ON a.SubMenuLv1ID = e.SubMenuLv1ID AND e.UserID = '" & _UserID & "' "
            SQL = SQL & "LEFT JOIN RoleSubMenuLv2 f ON a.SubMenuLv2ID = f.SubMenuLv2ID AND f.UserID = '" & _UserID & "' "
            SQL = SQL & "LEFT JOIN RoleSubMenuLv3 g ON a.SubMenuLv3ID = g.SubMenuLv3ID AND g.UserID = '" & _UserID & "' "
            SQL = SQL & "ORDER BY a.MainMenuID, a.MenuID, a.SubMenuID, a.SubMenuLv1ID, a.SubMenuLv2ID, a.SubMenuLv3ID"

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim LV As New ListViewItem()

                    'LV SHOW
                    LV = Lv_Role.Items.Add("")
                    LV.SubItems.Add(General_Class.CekNULL(dr("TItle")))
                    LV.SubItems.Add(General_Class.CekNULL(dr("MenuName")))
                    LV.SubItems.Add(General_Class.CekNULL(dr("SubMenuName")))
                    LV.SubItems.Add(General_Class.CekNULL(dr("SubMenuLv1Name")))
                    LV.SubItems.Add(General_Class.CekNULL(dr("SubMenuLv2Name")))
                    LV.SubItems.Add(General_Class.CekNULL(dr("SubMenuLv3Name")))

                    'LV HIDE
                    LV.SubItems.Add(General_Class.CekNULL(dr("MainMenuID")))
                    LV.SubItems.Add(General_Class.CekNULL(dr("MenuID")))
                    LV.SubItems.Add(General_Class.CekNULL(dr("SubMenuID")))
                    LV.SubItems.Add(General_Class.CekNULL(dr("SubMenuLv1ID")))
                    LV.SubItems.Add(General_Class.CekNULL(dr("SubMenuLv2ID")))
                    LV.SubItems.Add(General_Class.CekNULL(dr("SubMenuLv3ID")))

                    If dr("StatusAccess").ToString.ToUpper = "ACCESS" Then
                        LV.Checked = True
                    End If

                Loop

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    'HANDLE SELECTED INDEX COMBO BOX
    Private Sub Cb_Users_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_Users.SelectedIndexChanged
        If Cb_Users.SelectedIndex = -1 Then
            Exit Sub
        End If

        kosong()


        Try
            OpenConn()

            SQL = "select TOP 1 username, UserID from users where userID='" & arrUser.Item(Cb_Users.SelectedIndex).ToString & "'"
            Using dr = OpenTrans(SQL)
                If dr.HasRows Then
                    dr.Read()
                    Tb_UserName.Text = dr("username").ToString
                    _UserID = arrUser.Item(Cb_Users.SelectedIndex).ToString
                Else
                    dr.Close()
                    CloseTrans()
                    Tb_UserName.Text = ""
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    'UTILITY FUNCTION
    Private Sub Refresh()
        If Cb_Users.SelectedIndex = -1 Then
            Exit Sub
        End If

        kosong()

        If Not String.IsNullOrWhiteSpace(arrUser.Item(Cb_Users.SelectedIndex)) Then
            LoadAllRoleMenus()
        End If
    End Sub

    Private Sub Get_Data_Lv_Checked()
        Dim tampung As New ArrayList
        kosongArr()

        Dim a As Integer
        For i As Integer = 0 To Lv_Role.Items.Count - 1
            If Lv_Role.Items(i).Checked Then
                If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuLv3IDRole).Text) Then
                    Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuLv3IDRole).Text
                    Dim submenuLv2id As String = Lv_Role.Items(i).SubItems(itemSubMenuLv2IDRole).Text
                    Dim submenuLv1id As String = Lv_Role.Items(i).SubItems(itemSubMenuLv1IDRole).Text
                    Dim submenuid As String = Lv_Role.Items(i).SubItems(itemSubMenuIDRole).Text
                    Dim menuid As String = Lv_Role.Items(i).SubItems(itemMenuIDRole).Text
                    Dim mainmenuid As String = Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text
                    arrMainMenu.Add(mainmenuid)
                    arrMenu.Add(menuid)
                    arrSubMenu.Add(submenuid)
                    arrSubMenuLv1.Add(submenuLv1id)
                    arrSubMenuLv2.Add(submenuLv2id)
                    arrSubMenuLv3.Add(Data)

                ElseIf Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuLv2IDRole).Text) Then
                    Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuLv2IDRole).Text
                    Dim submenuLv1id As String = Lv_Role.Items(i).SubItems(itemSubMenuLv1IDRole).Text
                    Dim submenuid As String = Lv_Role.Items(i).SubItems(itemSubMenuIDRole).Text
                    Dim menuid As String = Lv_Role.Items(i).SubItems(itemMenuIDRole).Text
                    Dim mainmenuid As String = Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text
                    arrMainMenu.Add(mainmenuid)
                    arrMenu.Add(menuid)
                    arrSubMenu.Add(submenuid)
                    arrSubMenuLv1.Add(submenuLv1id)
                    arrSubMenuLv2.Add(Data)

                ElseIf Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuLv1IDRole).Text) Then
                    Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuLv1IDRole).Text
                    Dim submenuid As String = Lv_Role.Items(i).SubItems(itemSubMenuIDRole).Text
                    Dim menuid As String = Lv_Role.Items(i).SubItems(itemMenuIDRole).Text
                    Dim mainmenuid As String = Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text
                    arrMainMenu.Add(mainmenuid)
                    arrMenu.Add(menuid)
                    arrSubMenu.Add(submenuid)
                    arrSubMenuLv1.Add(Data)

                ElseIf Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuIDRole).Text) Then
                    Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuIDRole).Text
                    Dim menuid As String = Lv_Role.Items(i).SubItems(itemMenuIDRole).Text
                    Dim mainmenuid As String = Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text
                    arrMainMenu.Add(mainmenuid)
                    arrMenu.Add(menuid)
                    arrSubMenu.Add(Data)

                ElseIf Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemMenuIDRole).Text) Then
                    Dim Data As String = Lv_Role.Items(i).SubItems(itemMenuIDRole).Text
                    Dim mainmenuid As String = Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text
                    arrMainMenu.Add(mainmenuid)
                    arrMenu.Add(Data)

                ElseIf Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text) Then
                    Dim Data As String = Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text
                    arrMainMenu.Add(Data)

                End If

            End If
        Next

    End Sub


    'DRAW LISTVIEW
    Private Sub Lv_Role_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Role.DrawColumnHeader
        If e.ColumnIndex = 0 Then
            e.DrawBackground()
            Dim value As Boolean = False

            Try
                value = Convert.ToBoolean(e.Header.Tag)
            Catch ex As Exception
            End Try

            CheckBoxRenderer.DrawCheckBox(e.Graphics, New Point(e.Bounds.Left + 4, e.Bounds.Top + 4),
                                      If(value, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal,
                                         System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal))
        Else
            e.DrawDefault = True
        End If
    End Sub

    Private Sub Lv_Role_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles Lv_Role.ColumnClick
        If e.Column = 0 Then
            Dim value As Boolean = False

            Try
                value = Convert.ToBoolean(Lv_Role.Columns(e.Column).Tag)
            Catch ex As Exception

            End Try

            Dim newValue As Boolean = Not value
            Lv_Role.Columns(e.Column).Tag = newValue

            For Each item As ListViewItem In Lv_Role.Items
                item.Checked = newValue
            Next

            Lv_Role.Invalidate()
        End If
    End Sub

    Private Sub Lv_Role_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles Lv_Role.DrawItem
        e.DrawDefault = True
    End Sub

    Private Sub Lv_Role_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Role.DrawSubItem
        e.DrawDefault = True
    End Sub


End Class