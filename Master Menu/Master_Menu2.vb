Imports System.Net.NetworkInformation
Imports System.Runtime.Remoting.Metadata.W3cXsd2001
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Master_Menu2

    Private Sub Master_Menu1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Initial_LvMenu()

        Lv_hierarki.Columns.Clear()
        Lv_hierarki.Columns.Add("MainMenu", 150, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Menu", 150, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Sub Menu", 150, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Sub Menu Lv 1", 150, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Sub Menu Lv 2", 150, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Sub Menu Lv 3", 150, HorizontalAlignment.Center)

        'HIDE
        Lv_hierarki.Columns.Add("MainMenuID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("MenuID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv1ID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv1ID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("SubMenuLv1ID", 0, HorizontalAlignment.Center)
        Lv_hierarki.Columns.Add("Form", 0, HorizontalAlignment.Center)

        Lv_hierarki.View = View.Details

    End Sub

    Private Sub Load_All_Menu()

        Try
            OpenConn()
            Lv_hierarki.Items.Clear()

            SQL = "SELECT mm.Title, m.MenuName, sm.SubMenuName, sl1.SubMenuLv1Name, sl2.SubMenuLv2Name, sl3.SubMenuLv3Name, "


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub
























End Class