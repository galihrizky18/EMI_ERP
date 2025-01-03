Imports System.Net.NetworkInformation
Imports System.Runtime.Remoting.Metadata.W3cXsd2001
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Master_Menu2

    Private Sub Master_Menu1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Initial_LvMenu()

        Lv_hierarki.Columns.Clear()

        'HIDE
        Lv_hierarki.Columns.Add("MainMenuID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("MenuID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv1ID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv1ID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv1ID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Form", 0, HorizontalAlignment.Center)

        'OPEN
        Lv_hierarki.Columns.Add("MainMenu", 150, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Menu", 150, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Sub Menu", 150, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Sub Menu Lv 1", 150, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Sub Menu Lv 2", 150, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Sub Menu Lv 3", 150, HorizontalAlignment.Center)


        Lv_hierarki.View = View.Details

    End Sub

    Private Sub Load_All_Menu()

        Try
            OpenConn()
            Lv_hierarki.Items.Clear()

            SQL = "select MainMenuID, MenuID, SubMenuID, SubMenuLv1ID, SubMenuLv2ID, SubMenuLv3ID, "
            SQL = SQL & "Title as MainMenu, MenuName, SubMenuName, SubMenuLv1Name, SubMenuLv2Name, SubMenuLv3Name, Form "
            SQL = SQL & "from vw_MenuHierarchy"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_hierarki.Items.Add(Dr("MainMenuID"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("MenuID")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuID")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SubMenuLv1ID")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("")))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub
























End Class