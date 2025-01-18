Imports System.Net.NetworkInformation
Imports System.Runtime.Remoting.Metadata.W3cXsd2001
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Master_Menu2

    Dim arrMainMenu, arrMenu, arrSubMenu, arrSubMenuLv1, arrSubMenuLv2, arrSubMenuLv3 As New ArrayList

    Private Sub Master_Menu1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Initial_LvMenu()
        kosong()

    End Sub

    Private Sub kosong()

        Load_All_Menu()

        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True

        Tb_MenuName.BackColor = Color.White
        Tb_MenuOrder.BackColor = Color.White

        Tb_ImagePath.Enabled = False
        Tb_MenuForm.Enabled = False
        Tb_Var1.Enabled = False
        Tb_IsiVariabel1.Enabled = False
        Tb_Var2.Enabled = False
        Tb_IsiVariabel2.Enabled = False
        Tb_Var3.Enabled = False
        Tb_IsiVariabel3.Enabled = False

        Try
            OpenConn()

            'LOAD MAINMENU
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
        Lv_hierarki.View = View.Details

    End Sub

    Private Sub Load_All_Menu()

        Try
            OpenConn()
            Lv_hierarki.Items.Clear()

            SQL = "select MainMenuID, MenuID, SubMenuID, SubMenuLv1ID, SubMenuLv2ID, SubMenuLv3ID, "
            SQL = SQL & "Title as MainMenu, MenuName, SubMenuName, SubMenuLv1Name, SubMenuLv2Name, SubMenuLv3Name, Form "
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
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Cb_MainMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_MainMenu.SelectedIndexChanged
        If Cb_MainMenu.SelectedIndex = -1 Then Exit Sub

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

        Tb_ImagePath.Enabled = True
        Tb_MenuName.Enabled = True
        Tb_MenuOrder.Enabled = True

        Tb_ImagePath.BackColor = Color.White




    End Sub


    Private Sub LoadMenu()



    End Sub




















End Class