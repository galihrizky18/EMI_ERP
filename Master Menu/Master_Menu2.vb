Public Class Master_Menu2

    Dim arrMainMenu, arrMenu, arrSubMenu, arrSubMenuLv1, arrSubMenuLv2, arrSubMenuLv3 As New ArrayList
    Dim mainmenuid, menuid, submenuid, submenulv1id, submenulv2id, submenulv3id As String

    Dim Lv_MainMenuId, Lv_MenuId, Lv_SubMenuId, Lv_SubMenuLv1Id, Lv_SubMenuLv2Id, Lv_SubMenuLv3Id, Lv_Form, Lv_MainMenuName, Lv_MenuName, Lv_SubMenuName, Lv_SubMenuLv1Name, Lv_SubMenuLv2Name, Lv_SubMenuLv3Name As String
    Dim Lv_MainMenuOrder, Lv_MenuOrder, Lv_SubMenuOrder, Lv_SubMenuLv1Order, Lv_SubMenuLv2Order, Lv_SubMenuLv3Order, Lv_ImagePath As String

    Dim Item_MainMenuId As Integer = 0
    Dim Item_MenuId As Integer = 1
    Dim Item_SubMenuId As Integer = 2
    Dim Item_SubMenuLv1Id As Integer = 3
    Dim Item_SubMenuLv2Id As Integer = 4
    Dim Item_Form As Integer = 6
    Dim Item_SubMenuLv3Id As Integer = 5
    Dim Item_MainMenuName As Integer = 7
    Dim Item_MenuName As Integer = 8
    Dim Item_SubMenuName As Integer = 9
    Dim Item_SubMenuLv1Name As Integer = 10
    Dim Item_SubMenuLv2Name As Integer = 11
    Dim Item_SubmenuLv3Name As Integer = 12
    Dim Item_FormTpl As Integer = 13
    Dim Item_MainMenuOrder As Integer = 14
    Dim Item_MenuOrder As Integer = 15
    Dim Item_SubMenuOrder As Integer = 16
    Dim Item_SubmenuLv1Order As Integer = 17

    Private Sub Master_Menu2_Load(sender As Object, e As EventArgs)
        Dim ok As String = "asda"

        ok = "Rix"

    End Sub

    Dim Item_SubMenuLv2Order As Integer = 18
    Dim Item_SubMenuLv3Order As Integer = 19
    Dim Item_ImagePath As Integer = 20

    Private Sub Master_Menu1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Initial_LvMenu()
        kosong()

    End Sub

    Private Sub kosong()

        Load_All_Menu()

        Cb_MainMenu.Items.Clear()
        Cb_Menu.Items.Clear()
        Cb_SubMenu.Items.Clear()
        Cb_SubMenuLv1.Items.Clear()
        Cb_SubMenuLv2.Items.Clear()
        Cb_SubMenuLv3.Items.Clear()

        Cb_MainMenu.Text = ""
        Cb_Menu.Text = ""
        Cb_SubMenu.Text = ""
        Cb_SubMenuLv1.Text = ""
        Cb_SubMenuLv2.Text = ""
        Cb_SubMenuLv3.Text = ""

        Tb_ImagePath.Enabled = True
        Tb_UrutMainMenu.Enabled = True
        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True
        Tb_MenuForm.Enabled = False
        Tb_Var1.Enabled = False
        Tb_IsiVariabel1.Enabled = False
        Tb_Var2.Enabled = False
        Tb_IsiVariabel2.Enabled = False
        Tb_Var3.Enabled = False
        Tb_IsiVariabel3.Enabled = False

        Tb_ImagePath.Text = ""
        Tb_UrutMainMenu.Text = ""
        Tb_MenuName.Text = ""
        Tb_MenuOrder.Text = ""
        Tb_MenuForm.Text = ""
        Tb_Var1.Text = ""
        Tb_IsiVariabel1.Text = ""
        Tb_Var2.Text = ""
        Tb_IsiVariabel2.Text = ""
        Tb_Var3.Text = ""
        Tb_IsiVariabel3.Text = ""

        Tb_ImagePath.BackColor = Color.White
        Tb_UrutMainMenu.BackColor = Color.White
        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White
        Tb_MenuForm.BackColor = Color.LightGray
        Tb_Var1.BackColor = Color.LightGray
        Tb_IsiVariabel1.BackColor = Color.LightGray
        Tb_Var2.BackColor = Color.LightGray
        Tb_IsiVariabel2.BackColor = Color.LightGray
        Tb_Var3.BackColor = Color.LightGray
        Tb_IsiVariabel3.BackColor = Color.LightGray

        Load_MainMenu()

    End Sub

    Private Sub Initial_LvMenu()

        Lv_hierarki.Columns.Clear()

        'HIDE
        Lv_hierarki.Columns.Add("MainMenuID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("MenuID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv1ID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv2ID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv3ID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Form", 0, HorizontalAlignment.Center)

        'SHOW
        Lv_hierarki.Columns.Add("MainMenu", 140, HorizontalAlignment.Left)
        Lv_hierarki.Columns.Add("Menu", 140, HorizontalAlignment.Left)
        Lv_hierarki.Columns.Add("Sub Menu", 190, HorizontalAlignment.Left)
        Lv_hierarki.Columns.Add("Sub Menu Lv 1", 190, HorizontalAlignment.Left)
        Lv_hierarki.Columns.Add("Sub Menu Lv 2", 190, HorizontalAlignment.Left)
        Lv_hierarki.Columns.Add("Sub Menu Lv 3", 190, HorizontalAlignment.Left)
        Lv_hierarki.Columns.Add("Form", 230, HorizontalAlignment.Left)

        'HIDE
        Lv_hierarki.Columns.Add("MainMenuOrder", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("MenuOrder", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuOrder", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv1Order", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv2Order", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv3Order", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("ImagePath", 0, HorizontalAlignment.Center)

        Lv_hierarki.View = View.Details

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Load_All_Menu()

        Try
            OpenConn()
            Lv_hierarki.Items.Clear()

            SQL = "select MainMenuID, MenuID, SubMenuID, SubMenuLv1ID, SubMenuLv2ID, SubMenuLv3ID, "
            SQL = SQL & "Title as MainMenu, MenuName, SubMenuName, SubMenuLv1Name, SubMenuLv2Name, SubMenuLv3Name, Form, "
            SQL = SQL & "urut, MenuOrder, SubMenuOrder, SubMenuLv1Order, SubMenuLv2Order, SubMenuLv3Order, imagePath "
            SQL = SQL & "from vw_MenuHierarchy "
            SQL = SQL & "order by urut, MenuOrder, SubMenuOrder, SubMenuLv1Order, SubMenuLv2Order, SubMenuLv3Order "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_hierarki.Items.Add(Dr("MainMenuID"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("MenuID")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuID")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuLv1ID")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuLv2ID")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuLv3ID")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Form")))
                    'SHOW
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("MainMenu")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("MenuName")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuName")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuLv1Name")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuLv2Name")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuLv3Name")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Form")))
                    'HIDE
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("urut")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("MenuOrder")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuOrder")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuLv1Order")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuLv2Order")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuLv3Order")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("imagePath")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Get_Lv_MenuHierarchy(ByVal index As Integer)

        Lv_MainMenuId = Lv_hierarki.Items(index).SubItems(Item_MainMenuId).Text
        Lv_MenuId = Lv_hierarki.Items(index).SubItems(Item_MenuId).Text
        Lv_SubMenuId = Lv_hierarki.Items(index).SubItems(Item_SubMenuId).Text
        Lv_SubMenuLv1Id = Lv_hierarki.Items(index).SubItems(Item_SubMenuLv1Id).Text
        Lv_SubMenuLv2Id = Lv_hierarki.Items(index).SubItems(Item_SubMenuLv2Id).Text
        Lv_SubMenuLv3Id = Lv_hierarki.Items(index).SubItems(Item_SubMenuLv3Id).Text
        Lv_Form = Lv_hierarki.Items(index).SubItems(Item_Form).Text
        'Show
        Lv_MainMenuName = Lv_hierarki.Items(index).SubItems(Item_MainMenuName).Text
        Lv_MenuName = Lv_hierarki.Items(index).SubItems(Item_MenuName).Text
        Lv_SubMenuName = Lv_hierarki.Items(index).SubItems(Item_SubMenuName).Text
        Lv_SubMenuLv1Name = Lv_hierarki.Items(index).SubItems(Item_SubMenuLv1Name).Text
        Lv_SubMenuLv2Name = Lv_hierarki.Items(index).SubItems(Item_SubMenuLv2Name).Text
        Lv_SubMenuLv3Name = Lv_hierarki.Items(index).SubItems(Item_SubmenuLv3Name).Text
        'Hide
        Lv_MainMenuOrder = Lv_hierarki.Items(index).SubItems(Item_MainMenuOrder).Text
        Lv_MenuOrder = Lv_hierarki.Items(index).SubItems(Item_MenuOrder).Text
        Lv_SubMenuOrder = Lv_hierarki.Items(index).SubItems(Item_SubMenuOrder).Text
        Lv_SubMenuLv1Order = Lv_hierarki.Items(index).SubItems(Item_SubmenuLv1Order).Text
        Lv_SubMenuLv2Order = Lv_hierarki.Items(index).SubItems(Item_SubMenuLv2Order).Text
        Lv_SubMenuLv3Order = Lv_hierarki.Items(index).SubItems(Item_SubMenuLv3Order).Text
        Lv_ImagePath = Lv_hierarki.Items(index).SubItems(Item_ImagePath).Text

    End Sub

    Private Sub Cb_MainMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_MainMenu.SelectedIndexChanged
        If Cb_MainMenu.SelectedIndex = -1 Then Exit Sub

        mainmenuid = arrMainMenu(Cb_MainMenu.SelectedIndex)
        menuid = ""
        submenuid = ""
        submenulv1id = ""
        submenulv2id = ""
        submenulv3id = ""

        Cb_Menu.Items.Clear() : arrMenu.Clear()
        Cb_SubMenu.Items.Clear() : arrSubMenu.Clear()
        Cb_SubMenuLv1.Items.Clear() : arrSubMenuLv1.Clear()
        Cb_SubMenuLv2.Items.Clear() : arrSubMenuLv2.Clear()
        Cb_SubMenuLv3.Items.Clear() : arrSubMenuLv3.Clear()

        Tb_ImagePath.Text = ""
        Tb_MenuName.Text = ""
        Tb_MenuOrder.Text = ""
        Tb_MenuForm.Text = ""
        Tb_Var1.Text = ""
        Tb_IsiVariabel1.Text = ""
        Tb_Var2.Text = ""
        Tb_IsiVariabel2.Text = ""
        Tb_Var3.Text = ""
        Tb_IsiVariabel3.Text = ""

        Tb_ImagePath.Enabled = False
        Tb_UrutMainMenu.Enabled = False
        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True
        Tb_MenuForm.Enabled = True
        Tb_Var1.Enabled = True
        Tb_IsiVariabel1.Enabled = True
        Tb_Var2.Enabled = True
        Tb_IsiVariabel2.Enabled = True
        Tb_Var3.Enabled = True
        Tb_IsiVariabel3.Enabled = True

        Tb_ImagePath.BackColor = Color.LightGray
        Tb_UrutMainMenu.BackColor = Color.LightGray
        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White
        Tb_MenuForm.BackColor = Color.White
        Tb_Var1.BackColor = Color.White
        Tb_IsiVariabel1.BackColor = Color.White
        Tb_Var2.BackColor = Color.White
        Tb_IsiVariabel2.BackColor = Color.White
        Tb_Var3.BackColor = Color.White
        Tb_IsiVariabel3.BackColor = Color.White

        LoadMenu()

    End Sub

    Private Sub Cb_Menu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_Menu.SelectedIndexChanged

        If Cb_Menu.SelectedIndex = -1 Then Exit Sub

        menuid = arrMenu(Cb_Menu.SelectedIndex)

        submenuid = ""
        submenulv1id = ""
        submenulv2id = ""
        submenulv3id = ""

        Cb_SubMenu.Items.Clear() : arrSubMenu.Clear()
        Cb_SubMenuLv1.Items.Clear() : arrSubMenuLv1.Clear()
        Cb_SubMenuLv2.Items.Clear() : arrSubMenuLv2.Clear()
        Cb_SubMenuLv3.Items.Clear() : arrSubMenuLv3.Clear()

        Tb_ImagePath.Text = ""
        Tb_MenuName.Text = ""
        Tb_MenuOrder.Text = ""
        Tb_MenuForm.Text = ""
        Tb_Var1.Text = ""
        Tb_IsiVariabel1.Text = ""
        Tb_Var2.Text = ""
        Tb_IsiVariabel2.Text = ""
        Tb_Var3.Text = ""
        Tb_IsiVariabel3.Text = ""

        Tb_ImagePath.Enabled = False
        Tb_UrutMainMenu.Enabled = False
        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True
        Tb_MenuForm.Enabled = True
        Tb_Var1.Enabled = True
        Tb_IsiVariabel1.Enabled = True
        Tb_Var2.Enabled = True
        Tb_IsiVariabel2.Enabled = True
        Tb_Var3.Enabled = True
        Tb_IsiVariabel3.Enabled = True

        Tb_ImagePath.BackColor = Color.LightGray
        Tb_UrutMainMenu.BackColor = Color.LightGray
        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White
        Tb_MenuForm.BackColor = Color.White
        Tb_Var1.BackColor = Color.White
        Tb_IsiVariabel1.BackColor = Color.White
        Tb_Var2.BackColor = Color.White
        Tb_IsiVariabel2.BackColor = Color.White
        Tb_Var3.BackColor = Color.White
        Tb_IsiVariabel3.BackColor = Color.White

        LoadSubMenu()

    End Sub

    Private Sub Cb_SubMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_SubMenu.SelectedIndexChanged
        If Cb_SubMenu.SelectedIndex = -1 Then Exit Sub

        submenuid = arrSubMenu(Cb_SubMenu.SelectedIndex)

        submenulv1id = ""
        submenulv2id = ""
        submenulv3id = ""

        Cb_SubMenuLv1.Items.Clear() : arrSubMenuLv1.Clear()
        Cb_SubMenuLv2.Items.Clear() : arrSubMenuLv2.Clear()
        Cb_SubMenuLv3.Items.Clear() : arrSubMenuLv3.Clear()

        Tb_ImagePath.Text = ""
        Tb_MenuName.Text = ""
        Tb_MenuOrder.Text = ""
        Tb_MenuForm.Text = ""
        Tb_Var1.Text = ""
        Tb_IsiVariabel1.Text = ""
        Tb_Var2.Text = ""
        Tb_IsiVariabel2.Text = ""
        Tb_Var3.Text = ""
        Tb_IsiVariabel3.Text = ""

        Tb_ImagePath.Enabled = False
        Tb_UrutMainMenu.Enabled = False
        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True
        Tb_MenuForm.Enabled = True
        Tb_Var1.Enabled = True
        Tb_IsiVariabel1.Enabled = True
        Tb_Var2.Enabled = True
        Tb_IsiVariabel2.Enabled = True
        Tb_Var3.Enabled = True
        Tb_IsiVariabel3.Enabled = True

        Tb_ImagePath.BackColor = Color.LightGray
        Tb_UrutMainMenu.BackColor = Color.LightGray
        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White
        Tb_MenuForm.BackColor = Color.White
        Tb_Var1.BackColor = Color.White
        Tb_IsiVariabel1.BackColor = Color.White
        Tb_Var2.BackColor = Color.White
        Tb_IsiVariabel2.BackColor = Color.White
        Tb_Var3.BackColor = Color.White
        Tb_IsiVariabel3.BackColor = Color.White

        LoadSubMenuLv1()

    End Sub

    Private Sub Cb_SubMenuLv1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_SubMenuLv1.SelectedIndexChanged
        If Cb_SubMenuLv1.SelectedIndex = -1 Then Exit Sub

        submenulv1id = arrSubMenuLv1(Cb_SubMenuLv1.SelectedIndex)

        submenulv2id = ""
        submenulv3id = ""

        Cb_SubMenuLv2.Items.Clear() : arrSubMenuLv2.Clear()
        Cb_SubMenuLv3.Items.Clear() : arrSubMenuLv3.Clear()

        Tb_ImagePath.Text = ""
        Tb_MenuName.Text = ""
        Tb_MenuOrder.Text = ""
        Tb_MenuForm.Text = ""
        Tb_Var1.Text = ""
        Tb_IsiVariabel1.Text = ""
        Tb_Var2.Text = ""
        Tb_IsiVariabel2.Text = ""
        Tb_Var3.Text = ""
        Tb_IsiVariabel3.Text = ""

        Tb_ImagePath.Enabled = False
        Tb_UrutMainMenu.Enabled = False
        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True
        Tb_MenuForm.Enabled = True
        Tb_Var1.Enabled = True
        Tb_IsiVariabel1.Enabled = True
        Tb_Var2.Enabled = True
        Tb_IsiVariabel2.Enabled = True
        Tb_Var3.Enabled = True
        Tb_IsiVariabel3.Enabled = True

        Tb_ImagePath.BackColor = Color.LightGray
        Tb_UrutMainMenu.BackColor = Color.LightGray
        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White
        Tb_MenuForm.BackColor = Color.White
        Tb_Var1.BackColor = Color.White
        Tb_IsiVariabel1.BackColor = Color.White
        Tb_Var2.BackColor = Color.White
        Tb_IsiVariabel2.BackColor = Color.White
        Tb_Var3.BackColor = Color.White
        Tb_IsiVariabel3.BackColor = Color.White

        LoadSubMenuLv2()

    End Sub

    Private Sub Cb_SubMenuLv2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_SubMenuLv2.SelectedIndexChanged
        If Cb_SubMenuLv2.SelectedIndex = -1 Then Exit Sub

        submenulv2id = arrSubMenuLv2(Cb_SubMenuLv2.SelectedIndex)

        submenulv3id = ""

        Cb_SubMenuLv3.Items.Clear() : arrSubMenuLv3.Clear()

        Tb_ImagePath.Text = ""
        Tb_MenuName.Text = ""
        Tb_MenuOrder.Text = ""
        Tb_MenuForm.Text = ""
        Tb_Var1.Text = ""
        Tb_IsiVariabel1.Text = ""
        Tb_Var2.Text = ""
        Tb_IsiVariabel2.Text = ""
        Tb_Var3.Text = ""
        Tb_IsiVariabel3.Text = ""

        Tb_ImagePath.Enabled = False
        Tb_UrutMainMenu.Enabled = False
        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True
        Tb_MenuForm.Enabled = True
        Tb_Var1.Enabled = True
        Tb_IsiVariabel1.Enabled = True
        Tb_Var2.Enabled = True
        Tb_IsiVariabel2.Enabled = True
        Tb_Var3.Enabled = True
        Tb_IsiVariabel3.Enabled = True

        Tb_ImagePath.BackColor = Color.LightGray
        Tb_UrutMainMenu.BackColor = Color.LightGray
        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White
        Tb_MenuForm.BackColor = Color.White
        Tb_Var1.BackColor = Color.White
        Tb_IsiVariabel1.BackColor = Color.White
        Tb_Var2.BackColor = Color.White
        Tb_IsiVariabel2.BackColor = Color.White
        Tb_Var3.BackColor = Color.White
        Tb_IsiVariabel3.BackColor = Color.White

        LoadSubMenuLv3()

    End Sub

    Private Sub Cb_SubMenuLv3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_SubMenuLv3.SelectedIndexChanged
        If Cb_SubMenuLv3.SelectedIndex = -1 Then Exit Sub

        submenulv3id = arrSubMenuLv3(Cb_SubMenuLv3.SelectedIndex)

        Tb_ImagePath.Text = ""
        Tb_MenuName.Text = ""
        Tb_MenuOrder.Text = ""
        Tb_MenuForm.Text = ""
        Tb_Var1.Text = ""
        Tb_IsiVariabel1.Text = ""
        Tb_Var2.Text = ""
        Tb_IsiVariabel2.Text = ""
        Tb_Var3.Text = ""
        Tb_IsiVariabel3.Text = ""

        Tb_ImagePath.Enabled = False
        Tb_UrutMainMenu.Enabled = False
        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True
        Tb_MenuForm.Enabled = True
        Tb_Var1.Enabled = True
        Tb_IsiVariabel1.Enabled = True
        Tb_Var2.Enabled = True
        Tb_IsiVariabel2.Enabled = True
        Tb_Var3.Enabled = True
        Tb_IsiVariabel3.Enabled = True

        Tb_ImagePath.BackColor = Color.LightGray
        Tb_UrutMainMenu.BackColor = Color.LightGray
        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White
        Tb_MenuForm.BackColor = Color.White
        Tb_Var1.BackColor = Color.White
        Tb_IsiVariabel1.BackColor = Color.White
        Tb_Var2.BackColor = Color.White
        Tb_IsiVariabel2.BackColor = Color.White
        Tb_Var3.BackColor = Color.White
        Tb_IsiVariabel3.BackColor = Color.White
    End Sub

    '=============== LOAD MENUS ==============='
    Private Sub Load_MainMenu()
        Try
            OpenConn()

            'LOAD MAINMENU
            Cb_MainMenu.Items.Clear() : arrMainMenu.Clear()
            SQL = "select MainMenuID, TItle from MainMenu order by urut"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cb_MainMenu.Items.Add(Dr("TItle")) : arrMainMenu.Add(Dr("MainMenuID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LoadMenu()

        If Cb_MainMenu.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Cb_Menu.Items.Clear() : arrMenu.Clear()
            SQL = "select MenuID , MenuName from menus where MainMenuID = '" & arrMainMenu(Cb_MainMenu.SelectedIndex) & "' order by MenuOrder "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cb_Menu.Items.Add(Dr("MenuName")) : arrMenu.Add(Dr("MenuID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub LoadSubMenu()

        If Cb_Menu.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Cb_SubMenu.Items.Clear() : arrSubMenu.Clear()
            SQL = "select SubMenuID, SubMenuName from submenus where MenuID = '" & arrMenu(Cb_Menu.SelectedIndex) & "' order by SubMenuOrder "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cb_SubMenu.Items.Add(Dr("SubMenuName")) : arrSubMenu.Add(Dr("SubMenuID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub LoadSubMenuLv1()

        If Cb_SubMenu.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Cb_SubMenuLv1.Items.Clear() : arrSubMenuLv1.Clear()
            SQL = "select SubMenuLv1ID, SubMenuLv1Name from SubMenuLv1 where SubMenuID = '" & arrSubMenu(Cb_SubMenu.SelectedIndex) & "' order by SubMenuLv1Order"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cb_SubMenuLv1.Items.Add(Dr("SubMenuLv1Name")) : arrSubMenuLv1.Add(Dr("SubMenuLv1ID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub LoadSubMenuLv2()

        If Cb_SubMenuLv1.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Cb_SubMenuLv2.Items.Clear() : arrSubMenuLv2.Clear()
            SQL = "select SubMenuLv2ID, SubMenuLv2Name from SubMenuLv2 where SubMenuLv1ID = '" & arrSubMenuLv1(Cb_SubMenuLv1.SelectedIndex) & "' order by SubMenuLv2Order"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cb_SubMenuLv2.Items.Add(Dr("SubMenuLv2Name")) : arrSubMenuLv2.Add(Dr("SubMenuLv2ID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub LoadSubMenuLv3()

        If Cb_SubMenuLv2.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Cb_SubMenuLv3.Items.Clear() : arrSubMenuLv3.Clear()
            SQL = "select SubMenuLv3ID, SubMenuLv3Name from SubMenuLv3 where SubMenuLv2ID = '" & arrSubMenuLv2(Cb_SubMenuLv2.SelectedIndex) & "' order by SubMenuLv3Order"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cb_SubMenuLv3.Items.Add(Dr("SubMenuLv3Name")) : arrSubMenuLv3.Add(Dr("SubMenuLv3ID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    '============ HANDLE BUTTON ============'
    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Tb_MenuName.Text.Trim.Length = 0 Then
            MessageBox.Show("Menu Name Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Tb_MenuOrder.Text.Trim.Length = 0 Then
            MessageBox.Show("Menu Order Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim newImagePath = Tb_ImagePath.Text.Trim
        Dim newUrutMainMenu = Tb_UrutMainMenu.Text
        Dim newMenuName = Tb_MenuName.Text.Trim
        Dim newMenuOrder = Tb_MenuOrder.Text.Trim
        Dim newMenuForm = Tb_MenuForm.Text.Trim
        Dim newMenuVar1 = Tb_Var1.Text.Trim
        Dim newMenuVar2 = Tb_Var2.Text.Trim
        Dim newMenuVar3 = Tb_Var3.Text.Trim
        Dim newMenuIsiVar1 = Tb_IsiVariabel1.Text.Trim
        Dim newMenuIsiVar2 = Tb_IsiVariabel2.Text.Trim
        Dim newMenuIsiVar3 = Tb_IsiVariabel3.Text.Trim
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Not newMenuName = "" Then
                If Not Cb_MainMenu.Text = "" Then
                    If Not Cb_Menu.Text = "" Then
                        If Not Cb_SubMenu.Text = "" Then
                            If Not Cb_SubMenuLv1.Text = "" Then
                                If Not Cb_SubMenuLv2.Text = "" Then
                                    If Not Cb_SubMenuLv3.Text = "" Then
                                    Else
                                        'ADD NEW SUBMENU LV3
                                        If Not newMenuName = "" AndAlso Not newMenuOrder = "" Then
                                            SQL = "insert into SubMenuLv3(SubMenuLv3ID, SubMenuLv2ID, SubMenuLv3Name, SubMenuLv3Order, Form, Variabel, Isi_Variabel, "
                                            SQL = SQL & "Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                            SQL = SQL & "(SubMenuLv3_'" & getUniqueID() & "', '" & submenulv2id & "', '" & newMenuName & "', " & newMenuOrder & ", "
                                            SQL = SQL & "" & cekEmptyString(newMenuForm) & ", " & cekEmptyString(newMenuVar1) & ", " & cekEmptyString(newMenuIsiVar1) & ", "
                                            SQL = SQL & "" & cekEmptyString(newMenuVar2) & ", " & cekEmptyString(newMenuIsiVar2) & ", " & cekEmptyString(newMenuVar3) & ", "
                                            SQL = SQL & "" & cekEmptyString(newMenuIsiVar3) & ")"
                                            ExecuteTrans(SQL)
                                            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Else
                                            MessageBox.Show("MenuName dan MenuOrder Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        End If
                                    End If
                                Else
                                    'ADD NEW SUBMENULV2
                                    If Not newMenuName = "" AndAlso Not newMenuOrder = "" Then
                                        SQL = "insert into SubMenuLv2(SubMenuLv2ID, SubMenuLv1ID, SubMenuLv2Name, SubMenuLv2Order, Form, Variabel, Isi_Variabel, "
                                        SQL = SQL & "Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                        SQL = SQL & "('SubMenuLv2_" & getUniqueID() & "', '" & submenulv1id & "', '" & newMenuName & "', " & newMenuOrder & ", "
                                        SQL = SQL & "" & cekEmptyString(newMenuForm) & ", " & cekEmptyString(newMenuVar1) & ", " & cekEmptyString(newMenuIsiVar1) & ", "
                                        SQL = SQL & "" & cekEmptyString(newMenuVar2) & ", " & cekEmptyString(newMenuIsiVar2) & ", " & cekEmptyString(newMenuVar3) & ", "
                                        SQL = SQL & "" & cekEmptyString(newMenuIsiVar3) & ")"
                                        ExecuteTrans(SQL)
                                        MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Else
                                        MessageBox.Show("MenuName dan MenuOrder Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    End If
                                End If
                            Else
                                'ADD NEW SUBMENULV1
                                If Not newMenuName = "" AndAlso Not newMenuOrder = "" Then
                                    SQL = "insert into SubMenuLv1(SubMenuLv1ID, SubMenuID, SubMenuLv1Name, SubMenuLv1Order, Form, Variabel, Isi_Variabel, "
                                    SQL = SQL & "Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                    SQL = SQL & "('SubMenuLv1ID_" & getUniqueID() & "', '" & submenuid & "', '" & newMenuName & "', " & newMenuOrder & ", " & cekEmptyString(newMenuForm) & ", "
                                    SQL = SQL & "" & cekEmptyString(newMenuVar1) & ", " & cekEmptyString(newMenuIsiVar1) & ", " & cekEmptyString(newMenuVar2) & ", "
                                    SQL = SQL & "" & cekEmptyString(newMenuIsiVar2) & ", " & cekEmptyString(newMenuVar3) & ", " & cekEmptyString(newMenuIsiVar3) & ")"
                                    ExecuteTrans(SQL)
                                    MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Else
                                    MessageBox.Show("MenuName dan MenuOrder Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If
                            End If
                        Else
                            'ADD NEW SUBMENU
                            If Not newMenuName = "" AndAlso Not newMenuOrder = "" Then
                                SQL = "insert into SubMenus(SubMenuID, SubMenuName, MenuID, SubMenuOrder, Form, Variabel, Isi_Variabel, "
                                SQL = SQL & "Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                SQL = SQL & "('SubMenu_" & getUniqueID() & "', '" & newMenuName & "', '" & menuid & "', " & newMenuOrder & ", " & cekEmptyString(newMenuForm) & " "
                                SQL = SQL & ", " & cekEmptyString(newMenuVar1) & ", " & cekEmptyString(newMenuIsiVar1) & ", " & cekEmptyString(newMenuVar2) & ", "
                                SQL = SQL & "" & cekEmptyString(newMenuIsiVar2) & ", " & cekEmptyString(newMenuVar3) & ", " & cekEmptyString(newMenuIsiVar3) & ")"
                                ExecuteTrans(SQL)
                                MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                MessageBox.Show("MenuName dan MenuOrder Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If
                        End If
                    Else
                        'ADD NEW MENU
                        If Not newMenuName = "" AndAlso Not newMenuOrder = "" Then
                            SQL = "insert into menus (MenuId, MainMenuID, MenuName, MenuOrder, MenuParent) values "
                            SQL = SQL & "('Menu_" & getUniqueID() & "', '" & mainmenuid & "', '" & newMenuName & "', " & newMenuOrder & " , NULL)"
                            ExecuteTrans(SQL)
                            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("MenuName dan MenuOrder Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If
                Else
                    'ADD NEW MAINMENU
                    If Not newImagePath = "" Then
                        SQL = "insert into MainMenu(MainMenuId, ImagePath, Title, urut) values "
                        SQL = SQL & "('MainMenu_" & getUniqueID() & "', '" & newImagePath & "', '" & newMenuName & "', '" & newUrutMainMenu & "')"
                        ExecuteTrans(SQL)
                        MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("ImagePath Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If

            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()

    End Sub

    Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If submenulv3id = "" Then
                If submenulv2id = "" Then
                    If submenulv1id = "" Then
                        If submenuid = "" Then
                            If menuid = "" Then
                                If mainmenuid = "" Then
                                Else
                                    SQL = "delete RoleMainMenus where MainMenuID = '" & mainmenuid & "'"
                                    ExecuteTrans(SQL)

                                    SQL = "delete from MainMenu where MainMenuID='" & mainmenuid & "'"
                                    ExecuteTrans(SQL)

                                    MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Else
                                SQL = "delete RoleMenus where MenuID = '" & menuid & "'"
                                ExecuteTrans(SQL)

                                SQL = "delete from menus where MenuID='" & menuid & "'"
                                ExecuteTrans(SQL)

                                MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            SQL = "delete RoleSubMenu where SubMenuID = '" & submenuid & "'"
                            ExecuteTrans(SQL)

                            SQL = "delete from SubMenus where SubMenuID='" & submenuid & "'"
                            ExecuteTrans(SQL)

                            MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        SQL = "delete RoleSubMenuLv1 where SubMenuLv1ID = '" & submenulv1id & "'"
                        ExecuteTrans(SQL)

                        SQL = "delete from SubMenuLv1 where SubMenuLv1ID='" & submenulv1id & "'"
                        ExecuteTrans(SQL)

                        MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    SQL = "delete RoleSubMenuLv2 where SubMenuLv2ID = '" & submenulv2id & "'"
                    ExecuteTrans(SQL)

                    SQL = "delete from SubMenuLv2 where SubMenuLv2ID='" & submenulv2id & "'"
                    ExecuteTrans(SQL)

                    MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                SQL = "delete RoleSubMenuLv3 where SubMenuLv3ID = '" & submenulv3id & "'"
                ExecuteTrans(SQL)

                SQL = "delete from SubMenuLv3 where SubMenuLv3ID='" & submenulv3id & "'"
                ExecuteTrans(SQL)

                MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()

    End Sub

    Private Sub Lv_hierarki_DoubleClick(sender As Object, e As EventArgs) Handles Lv_hierarki.DoubleClick
        If Lv_hierarki.Items.Count = 0 Then Exit Sub

        Get_Lv_MenuHierarchy(Lv_hierarki.FocusedItem.Index)

        If Not String.IsNullOrEmpty(Lv_MainMenuId) Then
            Cb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Lv_MainMenuId)
            Tb_ImagePath.Text = Lv_ImagePath
            Tb_UrutMainMenu.Text = Lv_MainMenuOrder
            Tb_MenuOrder.Text = Lv_MainMenuOrder
            Tb_MenuName.Text = Lv_MainMenuName
            Tb_MenuForm.Text = Lv_Form

            If Not String.IsNullOrEmpty(Lv_MenuId) Then
                Cb_Menu.SelectedIndex = arrMenu.IndexOf(Lv_MenuId)
                Tb_ImagePath.Text = Lv_ImagePath
                Tb_UrutMainMenu.Text = Lv_MainMenuOrder
                Tb_MenuOrder.Text = Lv_MenuOrder
                Tb_MenuName.Text = Lv_MenuName
                Tb_MenuForm.Text = Lv_Form

                If Not String.IsNullOrEmpty(Lv_SubMenuId) Then
                    Cb_SubMenu.SelectedIndex = arrSubMenu.IndexOf(Lv_SubMenuId)
                    Tb_ImagePath.Text = Lv_ImagePath
                    Tb_UrutMainMenu.Text = Lv_MainMenuOrder
                    Tb_MenuOrder.Text = Lv_SubMenuOrder
                    Tb_MenuName.Text = Lv_SubMenuName
                    Tb_MenuForm.Text = Lv_Form

                    If Not String.IsNullOrEmpty(Lv_SubMenuLv1Id) Then
                        Cb_SubMenuLv1.SelectedIndex = arrSubMenuLv1.IndexOf(Lv_SubMenuLv1Id)
                        Tb_ImagePath.Text = Lv_ImagePath
                        Tb_UrutMainMenu.Text = Lv_MainMenuOrder
                        Tb_MenuOrder.Text = Lv_SubMenuLv1Order
                        Tb_MenuName.Text = Lv_SubMenuLv1Name
                        Tb_MenuForm.Text = Lv_Form

                        If Not String.IsNullOrEmpty(Lv_SubMenuLv2Id) Then
                            Cb_SubMenuLv2.SelectedIndex = arrSubMenuLv2.IndexOf(Lv_SubMenuLv2Id)
                            Tb_ImagePath.Text = Lv_ImagePath
                            Tb_UrutMainMenu.Text = Lv_MainMenuOrder
                            Tb_MenuOrder.Text = Lv_SubMenuLv2Order
                            Tb_MenuName.Text = Lv_SubMenuLv2Name
                            Tb_MenuForm.Text = Lv_Form

                            If Not String.IsNullOrEmpty(Lv_SubMenuLv3Id) Then
                                Cb_SubMenuLv3.SelectedIndex = arrSubMenuLv3.IndexOf(Lv_SubMenuLv3Id)
                                Tb_ImagePath.Text = Lv_ImagePath
                                Tb_UrutMainMenu.Text = Lv_MainMenuOrder
                                Tb_MenuOrder.Text = Lv_SubMenuLv3Order
                                Tb_MenuName.Text = Lv_SubMenuLv3Name
                                Tb_MenuForm.Text = Lv_Form
                            End If
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Function getUniqueID() As String
        Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
                            DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
                            DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")
        Return uniqueID
    End Function

    Private Function cekEmptyString(ByVal str As String) As String

        If str Is Nothing OrElse str = "" Then
            Return "NULL"
        Else
            Return "'" & str & "'"
        End If

    End Function

    Private Sub Tb_MenuOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_MenuOrder.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Tb_UrutMainMenu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_UrutMainMenu.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

End Class