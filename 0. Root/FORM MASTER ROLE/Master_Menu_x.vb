Imports System.Text.RegularExpressions

Public Class Master_Menu_x

    Dim arrFilter As New ArrayList

    Dim arrMainMenu, arrMenu, arrSubMenu, arrSubMenuLv1, arrSubMenuLv2, arrSubMenuLv3 As New ArrayList
    Dim mainmenuid, menuid, submenuid, submenulv1id, submenulv2id, submenulv3id As String

    Dim Lv_MainMenuId, Lv_MenuId, Lv_SubMenuId, Lv_SubMenuLv1Id, Lv_SubMenuLv2Id, Lv_SubMenuLv3Id, Lv_Form, Lv_MainMenuName, Lv_MenuName, Lv_SubMenuName, Lv_SubMenuLv1Name, Lv_SubMenuLv2Name, Lv_SubMenuLv3Name As String
    Dim Lv_MainMenuOrder, Lv_MenuOrder, Lv_SubMenuOrder, Lv_SubMenuLv1Order, Lv_SubMenuLv2Order, Lv_SubMenuLv3Order, Lv_ImagePath As String

    Dim Dgv_MenuID, Dgv_MenuName, Dgv_CurrentOrder, Dgv_NewOrder As String

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
    Dim Item_SubMenuLv2Order As Integer = 18
    Dim Item_SubMenuLv3Order As Integer = 19
    Dim Item_ImagePath As Integer = 20

    Dim itemDgv_MenuID As Integer = 0
    Dim itemDgv_MenuName As Integer = 1
    Dim itemDgv_CurrentOrder As Integer = 2
    Dim itemDgv_NewOrder As Integer = 3

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

        Btn_Simpan.Tag = "SAVE"
        Btn_Simpan.Text = "&Save"

        Cb_MainMenu.Text = ""
        Cb_Menu.Text = ""
        Cb_SubMenu.Text = ""
        Cb_SubMenuLv1.Text = ""
        Cb_SubMenuLv2.Text = ""
        Cb_SubMenuLv3.Text = ""

        Tb_ImagePath.Enabled = True
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
        Tb_MenuName.Text = ""
        Tb_MenuOrder.Text = ""
        Tb_MenuForm.Text = ""
        Tb_Var1.Text = ""
        Tb_IsiVariabel1.Text = ""
        Tb_Var2.Text = ""
        Tb_IsiVariabel2.Text = ""
        Tb_Var3.Text = ""
        Tb_IsiVariabel3.Text = ""
        Txt_SelectedMenu.Text = "MAINMENU"

        Tb_ImagePath.BackColor = Color.White
        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White
        Tb_MenuForm.BackColor = Color.LightGray
        Tb_Var1.BackColor = Color.LightGray
        Tb_IsiVariabel1.BackColor = Color.LightGray
        Tb_Var2.BackColor = Color.LightGray
        Tb_IsiVariabel2.BackColor = Color.LightGray
        Tb_Var3.BackColor = Color.LightGray
        Tb_IsiVariabel3.BackColor = Color.LightGray

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add("Main Menu") : arrFilter.Add("title")
        Cmb_Filter.Items.Add("Menu") : arrFilter.Add("MenuName")
        Cmb_Filter.Items.Add("Sub Menu") : arrFilter.Add("SubMenuName")
        Cmb_Filter.Items.Add("Sub Menu Lv 1") : arrFilter.Add("SubMenuLv1Name")
        Cmb_Filter.Items.Add("Sub Menu Lv 2") : arrFilter.Add("SubMenuLv2Name")
        Cmb_Filter.Items.Add("Sub Menu Lv 3") : arrFilter.Add("SubMenuLv4Name")
        Cmb_Filter.SelectedIndex = -1 : Txt_Filter.Text = ""

        Dgv_Order.Rows.Clear()

        Load_MainMenu()
        Load_Order_MainMenu()

    End Sub

    Private Sub kosongSebagian()
        Cb_MainMenu.Items.Clear()
        Cb_Menu.Items.Clear()
        Cb_SubMenu.Items.Clear()
        Cb_SubMenuLv1.Items.Clear()
        Cb_SubMenuLv2.Items.Clear()
        Cb_SubMenuLv3.Items.Clear()

        Btn_Simpan.Tag = "SAVE"
        Btn_Simpan.Text = "&Save"

        Cb_MainMenu.Text = ""
        Cb_Menu.Text = ""
        Cb_SubMenu.Text = ""
        Cb_SubMenuLv1.Text = ""
        Cb_SubMenuLv2.Text = ""
        Cb_SubMenuLv3.Text = ""

        Tb_ImagePath.Enabled = True
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
        Tb_MenuName.Text = ""
        Tb_MenuOrder.Text = ""
        Tb_MenuForm.Text = ""
        Tb_Var1.Text = ""
        Tb_IsiVariabel1.Text = ""
        Tb_Var2.Text = ""
        Tb_IsiVariabel2.Text = ""
        Tb_Var3.Text = ""
        Tb_IsiVariabel3.Text = ""
        Txt_SelectedMenu.Text = "MAINMENU"

        Tb_ImagePath.BackColor = Color.White
        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White
        Tb_MenuForm.BackColor = Color.LightGray
        Tb_Var1.BackColor = Color.LightGray
        Tb_IsiVariabel1.BackColor = Color.LightGray
        Tb_Var2.BackColor = Color.LightGray
        Tb_IsiVariabel2.BackColor = Color.LightGray
        Tb_Var3.BackColor = Color.LightGray
        Tb_IsiVariabel3.BackColor = Color.LightGray

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add("Main Menu") : arrFilter.Add("title")
        Cmb_Filter.Items.Add("Menu") : arrFilter.Add("MenuName")
        Cmb_Filter.Items.Add("Sub Menu") : arrFilter.Add("SubMenuName")
        Cmb_Filter.Items.Add("Sub Menu Lv 1") : arrFilter.Add("SubMenuLv1Name")
        Cmb_Filter.Items.Add("Sub Menu Lv 2") : arrFilter.Add("SubMenuLv2Name")
        Cmb_Filter.Items.Add("Sub Menu Lv 3") : arrFilter.Add("SubMenuLv4Name")
        Cmb_Filter.SelectedIndex = -1 : Txt_Filter.Text = ""

        Dgv_Order.Rows.Clear()

        Load_MainMenu()
        Load_Order_MainMenu()
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

        Lv_hierarki.Columns.Add("var1", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("isivar1", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("var2", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("isivar2", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("var3", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("isivar3", 0, HorizontalAlignment.Center)

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

            If Not Cmb_Filter.SelectedIndex = -1 Then
                SQL &= If(Not UCase(SQL).Contains("WHERE"), "WHERE ", "AND ")

                'If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilter.Item(Cmb_Filter.SelectedIndex) & " like '%" & Trim(Txt_Filter.Text) & "%' "
            End If

            SQL = SQL & "order by MenuID, MenuOrder, SubMenuOrder, SubMenuLv1Order, SubMenuLv2Order, SubMenuLv3Order "
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

    Private Sub GetItem_DGV(ByVal index As Integer)
        Dgv_MenuID = Dgv_Order.Rows(index).Cells(itemDgv_MenuID).Value.ToString
        Dgv_MenuName = Dgv_Order.Rows(index).Cells(itemDgv_MenuName).Value.ToString
        Dgv_CurrentOrder = Dgv_Order.Rows(index).Cells(itemDgv_CurrentOrder).Value.ToString
        Dgv_NewOrder = Dgv_Order.Rows(index).Cells(itemDgv_NewOrder).Value.ToString
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

        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True
        Tb_MenuForm.Enabled = False
        Tb_Var1.Enabled = False
        Tb_IsiVariabel1.Enabled = False
        Tb_Var2.Enabled = False
        Tb_IsiVariabel2.Enabled = False
        Tb_Var3.Enabled = False
        Tb_IsiVariabel3.Enabled = False

        If Btn_Simpan.Tag = "UPDATE" Then
            Tb_ImagePath.Enabled = True
            Tb_ImagePath.BackColor = Color.White
        Else
            Tb_ImagePath.Enabled = False
            Tb_ImagePath.BackColor = Color.LightGray

        End If

        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White
        Tb_MenuForm.BackColor = Color.LightGray
        Tb_Var1.BackColor = Color.LightGray
        Tb_IsiVariabel1.BackColor = Color.LightGray
        Tb_Var2.BackColor = Color.LightGray
        Tb_IsiVariabel2.BackColor = Color.LightGray
        Tb_Var3.BackColor = Color.LightGray
        Tb_IsiVariabel3.BackColor = Color.LightGray

        LoadMenu()
        Load_Order_Menu()

        Txt_SelectedMenu.Text = "MENU"

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
        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True

        If Btn_Simpan.Tag = "UPDATE" Then
            Tb_MenuForm.Enabled = False
            Tb_Var1.Enabled = False
            Tb_IsiVariabel1.Enabled = False
            Tb_Var2.Enabled = False
            Tb_IsiVariabel2.Enabled = False
            Tb_Var3.Enabled = False
            Tb_IsiVariabel3.Enabled = False

            Tb_MenuForm.BackColor = Color.LightGray
            Tb_Var1.BackColor = Color.LightGray
            Tb_IsiVariabel1.BackColor = Color.LightGray
            Tb_Var2.BackColor = Color.LightGray
            Tb_IsiVariabel2.BackColor = Color.LightGray
            Tb_Var3.BackColor = Color.LightGray
            Tb_IsiVariabel3.BackColor = Color.LightGray
        Else
            Tb_MenuForm.Enabled = True
            Tb_Var1.Enabled = True
            Tb_IsiVariabel1.Enabled = True
            Tb_Var2.Enabled = True
            Tb_IsiVariabel2.Enabled = True
            Tb_Var3.Enabled = True
            Tb_IsiVariabel3.Enabled = True

            Tb_MenuForm.BackColor = Color.White
            Tb_Var1.BackColor = Color.White
            Tb_IsiVariabel1.BackColor = Color.White
            Tb_Var2.BackColor = Color.White
            Tb_IsiVariabel2.BackColor = Color.White
            Tb_Var3.BackColor = Color.White
            Tb_IsiVariabel3.BackColor = Color.White
        End If

        Tb_ImagePath.BackColor = Color.LightGray
        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White

        LoadSubMenu()
        Load_Order_Submenu()

        Txt_SelectedMenu.Text = "SUBMENU"

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
        Load_Order_SubMenuLv1()

        Txt_SelectedMenu.Text = "SUBMENULV1"

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
        Load_Order_SubMenuLv2()

        Txt_SelectedMenu.Text = "SUBMENULV2"

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
        Load_Order_Submenulv3()

        Txt_SelectedMenu.Text = "SUBMENULV3"

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

    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
        If Cmb_Filter.SelectedIndex = -1 Then Exit Sub
        Txt_Filter.Text = ""
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

    '============ LOAD ORDER ============'

    Private Sub Load_Order_MainMenu()

        Try
            OpenConn()

            Dgv_Order.Rows.Clear()
            SQL = "select mainmenuid, TItle, urut from MainMenu "
            SQL = SQL & "order by urut "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Order.Rows.Add(1)
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuID).Value = .Rows(i).Item("mainmenuid")
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuName).Value = .Rows(i).Item("TItle")
                            Dgv_Order.Rows(i).Cells(itemDgv_CurrentOrder).Value = .Rows(i).Item("urut")
                            Dgv_Order.Rows(i).Cells(itemDgv_NewOrder).Value = ""
                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Order_Menu()
        Try
            OpenConn()

            Dgv_Order.Rows.Clear()
            SQL = "select MenuID, MenuName, MenuOrder from Menus "
            SQL = SQL & "where MainMenuID = '" & arrMainMenu(Cb_MainMenu.SelectedIndex) & "' "
            SQL = SQL & "order by MenuOrder "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Order.Rows.Add(1)
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuID).Value = .Rows(i).Item("MenuID")
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuName).Value = .Rows(i).Item("MenuName")
                            Dgv_Order.Rows(i).Cells(itemDgv_CurrentOrder).Value = .Rows(i).Item("MenuOrder")
                            Dgv_Order.Rows(i).Cells(itemDgv_NewOrder).Value = ""
                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Order_Submenu()
        Try
            OpenConn()

            Dgv_Order.Rows.Clear()
            SQL = "select SubMenuID, SubMenuName, SubMenuOrder from SubMenus "
            SQL = SQL & "where MenuID = '" & arrMenu(Cb_Menu.SelectedIndex) & "' "
            SQL = SQL & "order by SubMenuOrder "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Order.Rows.Add(1)
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuID).Value = .Rows(i).Item("SubMenuID")
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuName).Value = .Rows(i).Item("SubMenuName")
                            Dgv_Order.Rows(i).Cells(itemDgv_CurrentOrder).Value = .Rows(i).Item("SubMenuOrder")
                            Dgv_Order.Rows(i).Cells(itemDgv_NewOrder).Value = ""
                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Order_SubMenuLv1()
        Try
            OpenConn()

            Dgv_Order.Rows.Clear()
            SQL = "select SubMenuLv1ID, SubMenuLv1Name, SubMenuLv1Order from SubMenuLv1 "
            SQL = SQL & "where SubMenuID = '" & arrSubMenu(Cb_SubMenu.SelectedIndex) & "' "
            SQL = SQL & "order by SubMenuLv1Order "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Order.Rows.Add(1)
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuID).Value = .Rows(i).Item("SubMenuLv1ID")
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuName).Value = .Rows(i).Item("SubMenuLv1Name")
                            Dgv_Order.Rows(i).Cells(itemDgv_CurrentOrder).Value = .Rows(i).Item("SubMenuLv1Order")
                            Dgv_Order.Rows(i).Cells(itemDgv_NewOrder).Value = ""
                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Order_SubMenuLv2()
        Try
            OpenConn()

            Dgv_Order.Rows.Clear()
            SQL = "select SubMenuLv2ID, SubMenuLv2Name, SubMenuLv2Order from SubMenuLv2 "
            SQL = SQL & "where SubMenuLv1ID = '" & arrSubMenuLv1(Cb_SubMenuLv1.SelectedIndex) & "' "
            SQL = SQL & "order by SubMenuLv2Order "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Order.Rows.Add(1)
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuID).Value = .Rows(i).Item("SubMenuLv2ID")
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuName).Value = .Rows(i).Item("SubMenuLv2Name")
                            Dgv_Order.Rows(i).Cells(itemDgv_CurrentOrder).Value = .Rows(i).Item("SubMenuLv2Order")
                            Dgv_Order.Rows(i).Cells(itemDgv_NewOrder).Value = ""
                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Order_Submenulv3()
        Try
            OpenConn()

            Dgv_Order.Rows.Clear()
            SQL = "select SubMenuLv3ID, SubMenuLv3Name, SubMenuLv3Order from SubMenuLv3 "
            SQL = SQL & "where SubMenuLv2ID =  '" & arrSubMenuLv2(Cb_SubMenuLv2.SelectedIndex) & "' "
            SQL = SQL & "order by SubMenuLv3Order "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dgv_Order.Rows.Add(1)
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuID).Value = .Rows(i).Item("SubMenuLv3ID")
                            Dgv_Order.Rows(i).Cells(itemDgv_MenuName).Value = .Rows(i).Item("SubMenuLv3Name")
                            Dgv_Order.Rows(i).Cells(itemDgv_CurrentOrder).Value = .Rows(i).Item("SubMenuLv3Order")
                            Dgv_Order.Rows(i).Cells(itemDgv_NewOrder).Value = ""
                        Next
                    End If
                End With
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

        Dim newImagePath = cekEmptyString(Tb_ImagePath.Text.Trim)
        Dim newMenuName = cekEmptyString(Tb_MenuName.Text.Trim)
        Dim newMenuOrder = cekEmptyString(Tb_MenuOrder.Text.Trim)
        Dim newMenuForm = cekEmptyString(Tb_MenuForm.Text.Trim)
        Dim newMenuVar1 = cekEmptyString(Tb_Var1.Text.Trim)
        Dim newMenuVar2 = cekEmptyString(Tb_Var2.Text.Trim)
        Dim newMenuVar3 = cekEmptyString(Tb_Var3.Text.Trim)
        Dim newMenuIsiVar1 = cekEmptyString(Tb_IsiVariabel1.Text.Trim)
        Dim newMenuIsiVar2 = cekEmptyString(Tb_IsiVariabel2.Text.Trim)
        Dim newMenuIsiVar3 = cekEmptyString(Tb_IsiVariabel3.Text.Trim)

        Dim action As String = ""
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag = "SAVE" Then

                If Not newMenuName = "" Then
                    If Not Cb_MainMenu.Text = "" Then
                        If Not Cb_Menu.Text = "" Then
                            If Not Cb_SubMenu.Text = "" Then
                                If Not Cb_SubMenuLv1.Text = "" Then
                                    If Not Cb_SubMenuLv2.Text = "" Then
                                        If Not Cb_SubMenuLv3.Text = "" Then
                                        Else
                                            'Cek SubMenu2
                                            SQL = "select form from SubMenuLv2 where SubMenuLv2ID = '" & submenulv2id & "' "
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    If Not General_Class.CekNULL(Dr("form")) = "" Then
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Menu Tidak bisa tambah anak", "Master Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End Using

                                            'ADD NEW SUBMENU LV3
                                            If Not newMenuName = "" AndAlso Not newMenuOrder = "" Then
                                                SQL = "insert into SubMenuLv3(SubMenuLv3ID, SubMenuLv2ID, SubMenuLv3Name, SubMenuLv3Order, Form, Variabel, Isi_Variabel, "
                                                SQL = SQL & "Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                                SQL = SQL & "(SubMenuLv3_'" & getUniqueID() & "', '" & submenulv2id & "', " & newMenuName & ", " & newMenuOrder & ", "
                                                SQL = SQL & "" & newMenuForm & ", " & newMenuVar1 & ", " & newMenuIsiVar1 & ", "
                                                SQL = SQL & "" & newMenuVar2 & ", " & newMenuIsiVar2 & ", " & newMenuVar3 & ", "
                                                SQL = SQL & "" & newMenuIsiVar3 & ")"
                                                ExecuteTrans(SQL)
                                            Else
                                                MessageBox.Show("MenuName dan MenuOrder Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            End If
                                        End If
                                    Else
                                        'Cek SubMenu1
                                        SQL = "select form from SubMenuLv1 where SubMenuLv1ID = '" & submenulv1id & "' "
                                        Using Dr = OpenTrans(SQL)
                                            If Dr.Read Then
                                                If Not General_Class.CekNULL(Dr("form")) = "" Then
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Menu Tidak bisa tambah anak", "Master Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                        End Using

                                        'ADD NEW SUBMENULV2
                                        If Not newMenuName = "" AndAlso Not newMenuOrder = "" Then
                                            SQL = "insert into SubMenuLv2(SubMenuLv2ID, SubMenuLv1ID, SubMenuLv2Name, SubMenuLv2Order, Form, Variabel, Isi_Variabel, "
                                            SQL = SQL & "Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                            SQL = SQL & "('SubMenuLv2_" & getUniqueID() & "', '" & submenulv1id & "', " & newMenuName & ", " & newMenuOrder & ", "
                                            SQL = SQL & "" & newMenuForm & ", " & newMenuVar1 & ", " & newMenuIsiVar1 & ", "
                                            SQL = SQL & "" & newMenuVar2 & ", " & newMenuIsiVar2 & ", " & newMenuVar3 & ", "
                                            SQL = SQL & "" & newMenuIsiVar3 & ")"
                                            ExecuteTrans(SQL)
                                        Else
                                            MessageBox.Show("MenuName dan MenuOrder Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        End If
                                    End If
                                Else
                                    'Cek SubMenus
                                    SQL = "select form from SubMenus where SubMenuID = '" & submenuid & "' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            If Not General_Class.CekNULL(Dr("form")) = "" Then
                                                Dr.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Menu Tidak bisa tambah anak", "Master Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End Using

                                    'ADD NEW SUBMENULV1
                                    If Not newMenuName = "" AndAlso Not newMenuOrder = "" Then
                                        SQL = "insert into SubMenuLv1(SubMenuLv1ID, SubMenuID, SubMenuLv1Name, SubMenuLv1Order, Form, Variabel, Isi_Variabel, "
                                        SQL = SQL & "Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                        SQL = SQL & "('SubMenuLv1ID_" & getUniqueID() & "', '" & submenuid & "', " & newMenuName & ", " & newMenuOrder & ", " & newMenuForm & ", "
                                        SQL = SQL & "" & newMenuVar1 & ", " & newMenuIsiVar1 & ", " & newMenuVar2 & ", "
                                        SQL = SQL & "" & newMenuIsiVar2 & ", " & newMenuVar3 & ", " & newMenuIsiVar3 & ")"
                                        ExecuteTrans(SQL)
                                    Else
                                        MessageBox.Show("MenuName dan MenuOrder Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    End If
                                End If
                            Else

                                'ADD NEW SUBMENU
                                If Not newMenuName = "" AndAlso Not newMenuOrder = "" Then
                                    SQL = "insert into SubMenus(SubMenuID, SubMenuName, MenuID, SubMenuOrder, Form, Variabel, Isi_Variabel, "
                                    SQL = SQL & "Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3) values "
                                    SQL = SQL & "('SubMenu_" & getUniqueID() & "', " & newMenuName & ", '" & menuid & "', " & newMenuOrder & ", " & newMenuForm & " "
                                    SQL = SQL & ", " & newMenuVar1 & ", " & newMenuIsiVar1 & ", " & newMenuVar2 & ", "
                                    SQL = SQL & "" & newMenuIsiVar2 & ", " & newMenuVar3 & ", " & newMenuIsiVar3 & ")"
                                    ExecuteTrans(SQL)
                                Else
                                    MessageBox.Show("MenuName dan MenuOrder Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If
                            End If
                        Else
                            'ADD NEW MENU
                            If Not newMenuName = "" AndAlso Not newMenuOrder = "" Then
                                SQL = "insert into menus (MenuId, MainMenuID, MenuName, MenuOrder, MenuParent) values "
                                SQL = SQL & "('Menu_" & getUniqueID() & "', '" & mainmenuid & "', " & newMenuName & ", " & newMenuOrder & " , NULL)"
                                ExecuteTrans(SQL)
                            Else
                                MessageBox.Show("MenuName dan MenuOrder Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If
                        End If
                    Else
                        'ADD NEW MAINMENU
                        If Not newImagePath = "" Then
                            SQL = "insert into MainMenu(MainMenuId, ImagePath, Title, urut) values "
                            SQL = SQL & "('MainMenu_" & getUniqueID() & "', " & newImagePath & ", " & newMenuName & ", " & newMenuOrder & ")"
                            ExecuteTrans(SQL)
                        Else
                            MessageBox.Show("ImagePath Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If

                End If

                action = "Simpan"
            ElseIf Btn_Simpan.Tag = "UPDATE" Then

                Dim pertanyaan As String = MessageBox.Show("Yakin Ingin Update?", "Master Menu", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If pertanyaan = vbNo Then
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If

                If submenulv3id = "" Then
                    If submenulv2id = "" Then
                        If submenulv1id = "" Then
                            If submenuid = "" Then
                                If menuid = "" Then
                                    If mainmenuid = "" Then
                                    Else
                                        SQL = "update MainMenu set ImagePath = " & newImagePath & ", TItle = " & newMenuName & ", urut = " & newMenuOrder & " "
                                        SQL = SQL & "where MainMenuID = '" & mainmenuid & "' "
                                        ExecuteTrans(SQL)

                                    End If
                                Else
                                    SQL = "update Menus set MenuName = " & newMenuName & ", MenuOrder = " & newMenuOrder & " "
                                    SQL = SQL & "where MenuID = '" & menuid & "' "
                                    ExecuteTrans(SQL)

                                End If
                            Else
                                SQL = "update submenus set SubMenuName = " & newMenuName & ", SubMenuOrder = " & newMenuOrder & ", form = " & newMenuForm & ", "
                                SQL = SQL & "Variabel = " & newMenuVar1 & ", Isi_Variabel = " & newMenuIsiVar1 & ", "
                                SQL = SQL & "Variabel2 = " & newMenuVar2 & ", Isi_Variabel2 = " & newMenuIsiVar2 & ", "
                                SQL = SQL & "Variabel3 = " & newMenuVar3 & ", Isi_Variabel3 = " & newMenuIsiVar3 & " "
                                SQL = SQL & "where SubMenuID = '" & submenuid & "' "
                                ExecuteTrans(SQL)

                            End If
                        Else
                            SQL = "update SubMenuLv1 set SubMenuLv1Name = " & newMenuName & ", SubMenuLv1Order = " & newMenuOrder & ", form = " & newMenuForm & ", "
                            SQL = SQL & "Variabel = " & newMenuVar1 & ", Isi_Variabel = " & newMenuIsiVar1 & ", "
                            SQL = SQL & "Variabel2 = " & newMenuVar2 & ", Isi_Variabel2 = " & newMenuIsiVar2 & ", "
                            SQL = SQL & "Variabel3 = " & newMenuVar3 & ", Isi_Variabel3 = " & newMenuIsiVar3 & " "
                            SQL = SQL & "where SubMenuLv1ID = '" & submenulv1id & "' "
                            ExecuteTrans(SQL)

                        End If
                    Else
                        SQL = "update SubMenuLv2 set SubMenuLv2Name = " & newMenuName & ", SubMenuLv2Order = " & newMenuOrder & ", form = " & newMenuForm & ", "
                        SQL = SQL & "Variabel = " & newMenuVar1 & ", Isi_Variabel = " & newMenuIsiVar1 & ", "
                        SQL = SQL & "Variabel2 = " & newMenuVar2 & ", Isi_Variabel2 = " & newMenuIsiVar2 & ", "
                        SQL = SQL & "Variabel3 = " & newMenuVar3 & ", Isi_Variabel3 = " & newMenuIsiVar3 & " "
                        SQL = SQL & "where SubMenuLv2ID = '" & submenulv2id & "' "
                        ExecuteTrans(SQL)

                    End If
                Else
                    SQL = "update SubMenuLv3 set SubMenuLv3Name = " & newMenuName & ", SubMenuLv3Order = " & newMenuOrder & ", form = " & newMenuForm & ", "
                    SQL = SQL & "Variabel = " & newMenuVar1 & ", Isi_Variabel = " & newMenuIsiVar1 & ", "
                    SQL = SQL & "Variabel2 = " & newMenuVar2 & ", Isi_Variabel2 = " & newMenuIsiVar2 & ", "
                    SQL = SQL & "Variabel3 = " & newMenuVar3 & ", Isi_Variabel3 = " & newMenuIsiVar3 & " "
                    SQL = SQL & "where SubMenuLv3ID = '" & submenulv2id & "' "
                    ExecuteTrans(SQL)

                End If

                action = "Update"
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

        MessageBox.Show($"Data Berhasil di {action}", "Master Menu", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'kosongSebagian()

    End Sub

    Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click

        Dim pertanyaan As String = MessageBox.Show("Yakin Ingin Hapus?", "Master Menu", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If pertanyaan = vbNo Then Exit Sub

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

    Private Sub Btn_Simpan_Order_Click_1(sender As Object, e As EventArgs) Handles Btn_Simpan_Order.Click
        If Dgv_Order.Rows.Count = 0 Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            For i As Integer = 0 To Dgv_Order.Rows.Count - 1

                GetItem_DGV(i)

                If Not Dgv_NewOrder = "" And Dgv_CurrentOrder <> Dgv_NewOrder Then

                    Select Case Txt_SelectedMenu.Text.ToUpper()
                        Case "MAINMENU"
                            SQL = "update MainMenu set urut = '" & Dgv_NewOrder & "' where MainMenuID = '" & Dgv_MenuID & "'"
                        Case "MENU"
                            SQL = "update Menus set MenuOrder = '" & Dgv_NewOrder & "' where MenuID = '" & Dgv_MenuID & "'"
                        Case "SUBMENU"
                            SQL = "update SubMenus set SubMenuOrder = '" & Dgv_NewOrder & "' where SubMenuID = '" & Dgv_MenuID & "'"
                        Case "SUBMENULV1"
                            SQL = "update SubMenuLv1 set SubMenuLv1Order = '" & Dgv_NewOrder & "' where SubMenuLv1ID = '" & Dgv_MenuID & "'"
                        Case "SUBMENULV2"
                            SQL = "update SubMenuLv2 set SubMenuLv2Order = '" & Dgv_NewOrder & "' where SubMenuLv2ID = '" & Dgv_MenuID & "'"
                        Case "SUBMENULV3"
                            SQL = "update SubMenuLv3 set SubMenuLv3Order = '" & Dgv_NewOrder & "' where SubMenuLv3IDS = '" & Dgv_MenuID & "'"
                    End Select

                    If Not SQL = "" Then
                        ExecuteTrans(SQL)
                    End If

                End If

            Next

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'Select Case Txt_SelectedMenu.Text.ToUpper()
        '    Case "MAINMENU"
        '        Load_Order_MainMenu()
        '    Case "MENU"
        '        Load_Order_Menu()
        '    Case "SUBMENU"
        '        Load_Order_Submenu()
        '    Case "SUBMENULV1"
        '        Load_Order_SubMenuLv1()
        '    Case "SUBMENULV2"
        '        Load_Order_SubMenuLv2()
        '    Case "SUBMENULV3"
        '        Load_Order_Submenulv3()
        'End Select

        MessageBox.Show("Berhasil Update Order", "Input Menu", MessageBoxButtons.OK, MessageBoxIcon.Information)
        kosong()
    End Sub

    Private Sub Lv_hierarki_DoubleClick(sender As Object, e As EventArgs) Handles Lv_hierarki.DoubleClick
        If Lv_hierarki.Items.Count = 0 Then Exit Sub

        Get_Lv_MenuHierarchy(Lv_hierarki.FocusedItem.Index)

        Btn_Simpan.Tag = "UPDATE"
        Btn_Simpan.Text = "&Update"

        Try

            If Not String.IsNullOrEmpty(Lv_SubMenuLv3Id) Then

                Cb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Lv_MainMenuId)
                Cb_Menu.SelectedIndex = arrMenu.IndexOf(Lv_MenuId)
                Cb_SubMenu.SelectedIndex = arrSubMenu.IndexOf(Lv_SubMenuId)
                Cb_SubMenuLv1.SelectedIndex = arrSubMenuLv1.IndexOf(Lv_SubMenuLv1Id)
                Cb_SubMenuLv2.SelectedIndex = arrSubMenuLv2.IndexOf(Lv_SubMenuLv2Id)
                Cb_SubMenuLv3.SelectedIndex = arrSubMenuLv3.IndexOf(Lv_SubMenuLv3Id)
                Tb_ImagePath.Text = Lv_ImagePath
                Tb_MenuOrder.Text = Lv_SubMenuLv3Order
                Tb_MenuName.Text = Lv_SubMenuLv3Name
                Tb_MenuForm.Text = Lv_Form

                OpenConn()
                SQL = "select Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3 "
                SQL = SQL & "from SubMenuLv3 "
                SQL = SQL & "where SubMenuLv3ID = '" & arrSubMenuLv3(arrSubMenuLv3.IndexOf(Lv_SubMenuLv3Id)) & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Tb_Var1.Text = General_Class.CekNULL(Dr("Variabel"))
                        Tb_IsiVariabel1.Text = General_Class.CekNULL(Dr("Isi_Variabel"))
                        Tb_Var2.Text = General_Class.CekNULL(Dr("Variabel2"))
                        Tb_IsiVariabel2.Text = General_Class.CekNULL(Dr("Isi_Variabel2"))
                        Tb_Var3.Text = General_Class.CekNULL(Dr("Variabel3"))
                        Tb_IsiVariabel3.Text = General_Class.CekNULL(Dr("Isi_Variabel3"))
                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Menu Tidak Ditemukan", "Master Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            ElseIf Not String.IsNullOrEmpty(Lv_SubMenuLv2Id) Then
                Cb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Lv_MainMenuId)
                Cb_Menu.SelectedIndex = arrMenu.IndexOf(Lv_MenuId)
                Cb_SubMenu.SelectedIndex = arrSubMenu.IndexOf(Lv_SubMenuId)
                Cb_SubMenuLv1.SelectedIndex = arrSubMenuLv1.IndexOf(Lv_SubMenuLv1Id)
                Cb_SubMenuLv2.SelectedIndex = arrSubMenuLv2.IndexOf(Lv_SubMenuLv2Id)
                Tb_ImagePath.Text = Lv_ImagePath
                Tb_MenuOrder.Text = Lv_SubMenuLv2Order
                Tb_MenuName.Text = Lv_SubMenuLv2Name
                Tb_MenuForm.Text = Lv_Form

                OpenConn()
                SQL = "select Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3 "
                SQL = SQL & "from SubMenuLv2 "
                SQL = SQL & "where SubMenuLv2ID = '" & arrSubMenuLv2(arrSubMenuLv2.IndexOf(Lv_SubMenuLv2Id)) & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Tb_Var1.Text = General_Class.CekNULL(Dr("Variabel"))
                        Tb_IsiVariabel1.Text = General_Class.CekNULL(Dr("Isi_Variabel"))
                        Tb_Var2.Text = General_Class.CekNULL(Dr("Variabel2"))
                        Tb_IsiVariabel2.Text = General_Class.CekNULL(Dr("Isi_Variabel2"))
                        Tb_Var3.Text = General_Class.CekNULL(Dr("Variabel3"))
                        Tb_IsiVariabel3.Text = General_Class.CekNULL(Dr("Isi_Variabel3"))
                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Menu Tidak Ditemukan", "Master Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            ElseIf Not String.IsNullOrEmpty(Lv_SubMenuLv1Id) Then

                Cb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Lv_MainMenuId)
                Cb_Menu.SelectedIndex = arrMenu.IndexOf(Lv_MenuId)
                Cb_SubMenu.SelectedIndex = arrSubMenu.IndexOf(Lv_SubMenuId)
                Cb_SubMenuLv1.SelectedIndex = arrSubMenuLv1.IndexOf(Lv_SubMenuLv1Id)
                Tb_ImagePath.Text = Lv_ImagePath
                Tb_MenuOrder.Text = Lv_SubMenuLv1Order
                Tb_MenuName.Text = Lv_SubMenuLv1Name
                Tb_MenuForm.Text = Lv_Form

                OpenConn()
                SQL = "select Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3 "
                SQL = SQL & "from SubMenuLv1 "
                SQL = SQL & "where SubMenuLv1ID = '" & arrSubMenuLv1(arrSubMenuLv1.IndexOf(Lv_SubMenuLv1Id)) & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Tb_Var1.Text = General_Class.CekNULL(Dr("Variabel"))
                        Tb_IsiVariabel1.Text = General_Class.CekNULL(Dr("Isi_Variabel"))
                        Tb_Var2.Text = General_Class.CekNULL(Dr("Variabel2"))
                        Tb_IsiVariabel2.Text = General_Class.CekNULL(Dr("Isi_Variabel2"))
                        Tb_Var3.Text = General_Class.CekNULL(Dr("Variabel3"))
                        Tb_IsiVariabel3.Text = General_Class.CekNULL(Dr("Isi_Variabel3"))
                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Menu Tidak Ditemukan", "Master Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            ElseIf Not String.IsNullOrEmpty(Lv_SubMenuId) Then
                Cb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Lv_MainMenuId)
                Cb_Menu.SelectedIndex = arrMenu.IndexOf(Lv_MenuId)
                Cb_SubMenu.SelectedIndex = arrSubMenu.IndexOf(Lv_SubMenuId)
                Tb_ImagePath.Text = Lv_ImagePath
                Tb_MenuOrder.Text = Lv_SubMenuOrder
                Tb_MenuName.Text = Lv_SubMenuName
                Tb_MenuForm.Text = Lv_Form

                OpenConn()
                SQL = "select Variabel, Isi_Variabel, Variabel2, Isi_Variabel2, Variabel3, Isi_Variabel3 "
                SQL = SQL & "from SubMenus "
                SQL = SQL & "where SubMenuID = '" & arrSubMenu(arrSubMenu.IndexOf(Lv_SubMenuId)) & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Tb_Var1.Text = General_Class.CekNULL(Dr("Variabel"))
                        Tb_IsiVariabel1.Text = General_Class.CekNULL(Dr("Isi_Variabel"))
                        Tb_Var2.Text = General_Class.CekNULL(Dr("Variabel2"))
                        Tb_IsiVariabel2.Text = General_Class.CekNULL(Dr("Isi_Variabel2"))
                        Tb_Var3.Text = General_Class.CekNULL(Dr("Variabel3"))
                        Tb_IsiVariabel3.Text = General_Class.CekNULL(Dr("Isi_Variabel3"))
                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Menu Tidak Ditemukan", "Master Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            ElseIf Not String.IsNullOrEmpty(Lv_MenuId) Then
                Cb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Lv_MainMenuId)
                Cb_Menu.SelectedIndex = arrMenu.IndexOf(Lv_MenuId)
                Tb_ImagePath.Text = Lv_ImagePath
                Tb_MenuOrder.Text = Lv_MenuOrder
                Tb_MenuName.Text = Lv_MenuName

            ElseIf Not String.IsNullOrEmpty(Lv_MainMenuId) Then
                Cb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Lv_MainMenuId)
                Tb_ImagePath.Text = Lv_ImagePath
                Tb_MenuOrder.Text = Lv_MainMenuOrder
                Tb_MenuName.Text = Lv_MainMenuName

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

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

    Private Sub Tb_UrutMainMenu_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Filter diPililh Dahulu", "Input Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Filter.Focus() : Exit Sub
        ElseIf Txt_Filter.Text = "" Then
            MessageBox.Show("Filter Value Tidak Boleh Kosong", "Input Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Filter.Focus() : Exit Sub
        End If

        Load_All_Menu()
        kosongSebagian()

    End Sub

    Private Sub Dgv_Order_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Order.CellEndEdit

        If Dgv_Order.CurrentCell IsNot Nothing AndAlso Dgv_Order.CurrentCell.ColumnIndex = 3 Then

            Dim data As String = Dgv_Order.CurrentCell.Value

            Dim allowedPattern As String = "^[0-9]+$"  ' hanya memperbolehkan satu atau lebih digit angka

            If Not Regex.IsMatch(data, allowedPattern) Then
                Dgv_Order.CurrentCell.Value = ""
            End If

        End If

    End Sub



    '======================
    '=    HANDLE CLICK    =
    '======================
    Private Sub Tb_ImagePath_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_ImagePath.KeyPress
        If e.KeyChar = Chr(13) Then Tb_MenuName.Focus()
    End Sub

    Private Sub Tb_MenuName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_MenuName.KeyPress
        If e.KeyChar = Chr(13) Then Tb_MenuOrder.Focus()
    End Sub

    Private Sub Tb_MenuOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_MenuOrder.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
        If e.KeyChar = Chr(13) Then Tb_MenuForm.Focus()
    End Sub

    Private Sub Tb_MenuForm_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_MenuForm.KeyPress
        If e.KeyChar = Chr(13) Then Tb_Var1.Focus()
    End Sub

    Private Sub Tb_Var1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_Var1.KeyPress
        If e.KeyChar = Chr(13) Then Tb_IsiVariabel1.Focus()
    End Sub

    Private Sub Tb_IsiVariabel1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_IsiVariabel1.KeyPress
        If e.KeyChar = Chr(13) Then Tb_Var2.Focus()
    End Sub

    Private Sub Tb_Var2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_Var2.KeyPress
        If e.KeyChar = Chr(13) Then Tb_IsiVariabel2.Focus()
    End Sub

    Private Sub Tb_IsiVariabel2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_IsiVariabel2.KeyPress
        If e.KeyChar = Chr(13) Then Tb_Var3.Focus()
    End Sub

    Private Sub Tb_Var3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_Var3.KeyPress
        If e.KeyChar = Chr(13) Then Tb_IsiVariabel3.Focus()
    End Sub

    Private Sub Tb_IsiVariabel3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_IsiVariabel3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

End Class