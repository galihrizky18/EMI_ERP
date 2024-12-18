Public Class Master_Menu

    Dim arrMainMenu, arrMenu, arrSubMenu, arrSubMenuLv1, arrSubMenuLv2, arrSubMenuLv3 As New ArrayList
    Dim mainmenuid, menuid, submenuid, submenulv1id, submenulv2id, submenulv3id As String



    Private Sub Master_Menu_Testing_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        kosong()
        LoadMainMenu()

        Tb_MenuOrder.Enabled = False
        Tb_Form.Enabled = False
        Tb_Var1.Enabled = False
        Tb_Var2.Enabled = False
        Tb_Var3.Enabled = False
        Tb_IsiVar1.Enabled = False
        Tb_IsiVar2.Enabled = False
        Tb_IsiVar3.Enabled = False
    End Sub


    'HANDLE LOAD MENU
    Private Sub LoadMainMenu()

        Try
            OpenConn()

            SQL = "select MainMenuId, Title from MainMenu"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cb_MainMenu.Items.Add(dr("Title")) : arrMainMenu.Add(dr("MainMenuId"))
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
        Cb_Menu.Items.Clear()
        arrMenu.Clear()
        Cb_SubMenu.Items.Clear()
        arrSubMenu.Clear()
        Cb_SubMenuLv1.Items.Clear()
        arrSubMenuLv1.Clear()
        Cb_SubMenuLv2.Items.Clear()
        arrSubMenuLv2.Clear()
        Cb_SubMenuLv3.Items.Clear()
        arrSubMenuLv3.Clear()

        Cb_Menu.Text = String.Empty
        Cb_SubMenu.Text = String.Empty
        Cb_SubMenuLv1.Text = String.Empty
        Cb_SubMenuLv2.Text = String.Empty
        Cb_SubMenuLv3.Text = String.Empty

        mainmenuid = arrMainMenu(Cb_MainMenu.SelectedIndex)
        Tb_MenuOrder.Enabled = True

        If Not Cb_MainMenu.SelectedIndex = -1 Then
            loadMenu(mainmenuid)
            Tb_Form.Enabled = False
            Tb_Var1.Enabled = False
            Tb_Var2.Enabled = False
            Tb_Var3.Enabled = False
            Tb_IsiVar1.Enabled = False
            Tb_IsiVar2.Enabled = False
            Tb_IsiVar3.Enabled = False
        End If
    End Sub

    Private Sub Cb_MainMenu_TextChanged(sender As Object, e As EventArgs) Handles Cb_MainMenu.TextChanged
        If Cb_MainMenu.Text.Trim = "" AndAlso Cb_MainMenu.SelectedIndex = -1 Then
            mainmenuid = String.Empty

            Tb_ImagePath.Enabled = True

            Tb_MenuOrder.Enabled = False
            Tb_Form.Enabled = False
            Tb_Var1.Enabled = False
            Tb_Var2.Enabled = False
            Tb_Var3.Enabled = False
            Tb_IsiVar1.Enabled = False
            Tb_IsiVar2.Enabled = False
            Tb_IsiVar3.Enabled = False

            Cb_Menu.Items.Clear()
            arrMenu.Clear()
            Cb_SubMenu.Items.Clear()
            arrSubMenu.Clear()
            Cb_SubMenuLv1.Items.Clear()
            arrSubMenuLv1.Clear()
            Cb_SubMenuLv2.Items.Clear()
            arrSubMenuLv2.Clear()
            Cb_SubMenuLv3.Items.Clear()
            arrSubMenuLv3.Clear()

            Cb_Menu.Text = String.Empty
            Cb_SubMenu.Text = String.Empty
            Cb_SubMenuLv1.Text = String.Empty
            Cb_SubMenuLv2.Text = String.Empty
            Cb_SubMenuLv3.Text = String.Empty
        Else
            Tb_ImagePath.Enabled = False
        End If
    End Sub

    Private Sub loadMenu(ByVal mainmenuid As String)
        If Cb_MainMenu.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select menuid, menuname from menus where mainmenuid='" & mainmenuid & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cb_Menu.Items.Add(dr("menuname")) : arrMenu.Add(dr("menuid"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Cb_Menu_TextChanged(sender As Object, e As EventArgs) Handles Cb_Menu.TextChanged
        If Cb_Menu.Text.Trim = "" AndAlso Cb_Menu.SelectedIndex = -1 Then
            menuid = String.Empty

            Tb_Form.Enabled = False
            Tb_Var1.Enabled = False
            Tb_Var2.Enabled = False
            Tb_Var3.Enabled = False
            Tb_IsiVar1.Enabled = False
            Tb_IsiVar2.Enabled = False
            Tb_IsiVar3.Enabled = False

            Cb_SubMenu.Items.Clear()
            arrSubMenu.Clear()
            Cb_SubMenuLv1.Items.Clear()
            arrSubMenuLv1.Clear()
            Cb_SubMenuLv2.Items.Clear()
            arrSubMenuLv2.Clear()
            Cb_SubMenuLv3.Items.Clear()
            arrSubMenuLv3.Clear()

            Cb_SubMenu.Text = String.Empty
            Cb_SubMenuLv1.Text = String.Empty
            Cb_SubMenuLv2.Text = String.Empty
            Cb_SubMenuLv3.Text = String.Empty
        Else
            Tb_Form.Enabled = True
            Tb_Var1.Enabled = True
            Tb_Var2.Enabled = True
            Tb_Var3.Enabled = True
            Tb_IsiVar1.Enabled = True
            Tb_IsiVar2.Enabled = True
            Tb_IsiVar3.Enabled = True
        End If
    End Sub

    Private Sub Cb_Menu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_Menu.SelectedIndexChanged
        Cb_SubMenu.Items.Clear()
        arrSubMenu.Clear()
        Cb_SubMenuLv1.Items.Clear()
        arrSubMenuLv1.Clear()
        Cb_SubMenuLv2.Items.Clear()
        arrSubMenuLv2.Clear()
        Cb_SubMenuLv3.Items.Clear()
        arrSubMenuLv3.Clear()

        Cb_SubMenu.Text = String.Empty
        Cb_SubMenuLv1.Text = String.Empty
        Cb_SubMenuLv2.Text = String.Empty
        Cb_SubMenuLv3.Text = String.Empty

        menuid = arrMenu(Cb_Menu.SelectedIndex)

        If Not Cb_Menu.SelectedIndex = -1 Then
            loadSubMenu(menuid)
        End If
    End Sub

    Private Sub loadSubMenu(ByVal menuid As String)
        If Cb_Menu.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select SubMenuID, SubMenuName from SubMenus where MenuID='" & menuid & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cb_SubMenu.Items.Add(dr("SubMenuName")) : arrSubMenu.Add(dr("SubMenuID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cb_SubMenu_TextChanged(sender As Object, e As EventArgs) Handles Cb_SubMenu.TextChanged
        If Cb_SubMenu.Text.Trim = "" AndAlso Cb_SubMenu.SelectedIndex = -1 Then
            submenuid = String.Empty

            Cb_SubMenuLv1.Items.Clear()
            arrSubMenuLv1.Clear()
            Cb_SubMenuLv2.Items.Clear()
            arrSubMenuLv2.Clear()
            Cb_SubMenuLv3.Items.Clear()
            arrSubMenuLv3.Clear()

            Cb_SubMenuLv1.Text = String.Empty
            Cb_SubMenuLv2.Text = String.Empty
            Cb_SubMenuLv3.Text = String.Empty

        End If
    End Sub

    Private Sub Cb_SubMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_SubMenu.SelectedIndexChanged
        Cb_SubMenuLv1.Items.Clear()
        arrSubMenuLv1.Clear()
        Cb_SubMenuLv2.Items.Clear()
        arrSubMenuLv2.Clear()
        Cb_SubMenuLv3.Items.Clear()
        arrSubMenuLv3.Clear()

        Cb_SubMenuLv1.Text = String.Empty
        Cb_SubMenuLv2.Text = String.Empty
        Cb_SubMenuLv3.Text = String.Empty

        submenuid = arrSubMenu(Cb_SubMenu.SelectedIndex)

        If Not Cb_SubMenu.SelectedIndex = -1 Then
            loadSubMenuLv1(submenuid)
        End If
    End Sub

    Private Sub loadSubMenuLv1(ByVal submenuid As String)
        If Cb_SubMenu.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select SubMenuLv1ID, SubMenuLv1Name from SubMenuLv1 a, SubMenus b where a.SubMenuID=b.SubMenuID "
            SQL = SQL & "and b.form is null and b.SubMenuID='" & submenuid & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cb_SubMenuLv1.Items.Add(dr("SubMenuLv1Name")) : arrSubMenuLv1.Add(dr("SubMenuLv1ID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cb_SubMenuLv1_TextChanged(sender As Object, e As EventArgs) Handles Cb_SubMenuLv1.TextChanged
        If Cb_SubMenuLv1.Text.Trim = "" AndAlso Cb_SubMenuLv1.SelectedIndex = -1 Then
            submenulv1id = String.Empty

            Cb_SubMenuLv2.Items.Clear()
            arrSubMenuLv2.Clear()
            Cb_SubMenuLv3.Items.Clear()
            arrSubMenuLv3.Clear()

            Cb_SubMenuLv2.Text = String.Empty
            Cb_SubMenuLv3.Text = String.Empty
        End If
    End Sub

    Private Sub Tb_MenuOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_MenuOrder.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Cb_SubMenuLv1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_SubMenuLv1.SelectedIndexChanged
        Cb_SubMenuLv2.Items.Clear()
        arrSubMenuLv2.Clear()
        Cb_SubMenuLv3.Items.Clear()
        arrSubMenuLv3.Clear()

        Cb_SubMenuLv2.Text = String.Empty
        Cb_SubMenuLv3.Text = String.Empty

        submenulv1id = arrSubMenuLv1(Cb_SubMenuLv1.SelectedIndex)

        If Not Cb_SubMenuLv1.SelectedIndex = -1 Then
            loadSubMenuLv2(submenulv1id)
        End If
    End Sub

    Private Sub loadSubMenuLv2(ByVal submenulv1id As String)
        If Cb_SubMenuLv1.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select SubMenuLv2ID, SubMenuLv2Name from SubMenuLv2 a, SubMenuLv1 b where a.SubMenuLv1ID=b.SubMenuLv1ID "
            SQL = SQL & "and b.form is null and b.SubMenuLv1ID='" & submenulv1id & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cb_SubMenuLv2.Items.Add(dr("SubMenuLv2Name")) : arrSubMenuLv2.Add(dr("SubMenuLv2ID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cb_SubMenuLv2_TextChanged(sender As Object, e As EventArgs) Handles Cb_SubMenuLv2.TextChanged
        If Cb_SubMenuLv2.Text.Trim = "" AndAlso Cb_SubMenuLv2.SelectedIndex = -1 Then
            submenulv2id = String.Empty

            Cb_SubMenuLv3.Items.Clear()
            arrSubMenuLv3.Clear()

            Cb_SubMenuLv3.Text = String.Empty
        End If
    End Sub

    Private Sub Cb_SubMenuLv2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cb_SubMenuLv2.SelectedIndexChanged
        Cb_SubMenuLv3.Items.Clear()
        arrSubMenuLv3.Clear()

        Cb_SubMenuLv3.Text = String.Empty

        submenulv2id = arrSubMenuLv2(Cb_SubMenuLv2.SelectedIndex)

        If Not Cb_SubMenuLv2.SelectedIndex = -1 Then
            loadSubMenuLv3(submenulv2id)
        End If
    End Sub

    Private Sub loadSubMenuLv3(ByVal submenulv2id As String)
        If Cb_SubMenuLv2.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select SubMenuLv3ID, SubMenuLv3Name from SubMenuLv3 a, SubMenuLv2 b where a.SubMenuLv2ID=b.SubMenuLv2ID "
            SQL = SQL & "and b.form is null and b.SubMenuLv2ID='" & submenulv2id & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cb_SubMenuLv3.Items.Add(dr("SubMenuLv3Name")) : arrSubMenuLv3.Add(dr("SubMenuLv3ID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'HANDLE BUTTON
    Private Sub Btn_Save_Click(sender As Object, e As EventArgs) Handles Btn_Save.Click

        If Tb_MenuName.Text.Trim.Length = 0 Then
            MessageBox.Show("Menu Name Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Tb_MenuOrder.Text.Trim.Length = 0 Then
            MessageBox.Show("Menu Order Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim newMenuName = Tb_MenuName.Text.Trim
        Dim newMenuOrder = Tb_MenuOrder.Text.Trim
        Dim newMenuForm = Tb_Form.Text.Trim
        Dim newImagePath = Tb_ImagePath.Text.Trim
        Dim newMenuVar1 = Tb_Var1.Text.Trim
        Dim newMenuVar2 = Tb_Var2.Text.Trim
        Dim newMenuVar3 = Tb_Var3.Text.Trim
        Dim newMenuIsiVar1 = Tb_IsiVar1.Text.Trim
        Dim newMenuIsiVar2 = Tb_IsiVar2.Text.Trim
        Dim newMenuIsiVar3 = Tb_IsiVar3.Text.Trim
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Not newMenuName = "" Then
                If Not mainmenuid = "" Then
                    If Not menuid = "" Then
                        If Not submenuid = "" Then
                            If Not submenulv1id = "" Then
                                If Not submenulv2id = "" Then
                                    If Not submenulv3id = "" Then

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
                        SQL = "insert into MainMenu(MainMenuId, ImagePath, Title) values "
                        SQL = SQL & "('MainMenu_" & getUniqueID() & "', '" & newImagePath & "', '" & newMenuName & "')"
                        ExecuteTrans(SQL)
                        MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("ImagePath Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If

            End If

            Cmd.Transaction.Commit()
            CloseConn()
            kosong()
            LoadMainMenu()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click

        Try
            OpenConn()

            If submenulv3id = "" Then
                If submenulv2id = "" Then
                    If submenulv1id = "" Then
                        If submenuid = "" Then
                            If menuid = "" Then
                                If mainmenuid = "" Then

                                Else
                                    SQL = "delete from MainMenu where MainMenuID='" & mainmenuid & "'"
                                    ExecuteTrans(SQL)
                                    MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Else
                                SQL = "delete from menus where MenuID='" & menuid & "'"
                                ExecuteTrans(SQL)
                                MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            SQL = "delete from SubMenus where SubMenuID='" & submenuid & "'"
                            ExecuteTrans(SQL)
                            MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        SQL = "delete from SubMenuLv1 where SubMenuLv1ID='" & submenulv1id & "'"
                        ExecuteTrans(SQL)
                        MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    SQL = "delete from SubMenuLv2 where SubMenuLv2ID='" & submenulv2id & "'"
                    ExecuteTrans(SQL)
                    MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                SQL = "delete from SubMenuLv3 where SubMenuLv3ID='" & submenulv3id & "'"
                ExecuteTrans(SQL)
                MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    'FUNCTION UTILITY
    Private Sub kosong()
        arrMainMenu.Clear() : arrMenu.Clear()
        arrSubMenu.Clear() : arrSubMenuLv1.Clear() : arrSubMenuLv2.Clear() : arrSubMenuLv3.Clear()

        mainmenuid = String.Empty : menuid = String.Empty : submenuid = String.Empty
        submenulv1id = String.Empty : submenulv2id = String.Empty : submenulv3id = String.Empty

        Tb_MenuName.Text = String.Empty : Tb_MenuOrder.Text = String.Empty : Tb_ImagePath.Text = String.Empty
        Tb_Form.Text = String.Empty : Tb_Var1.Text = String.Empty : Tb_IsiVar1.Text = String.Empty
        Tb_Var2.Text = String.Empty : Tb_IsiVar2.Text = String.Empty : Tb_Var3.Text = String.Empty
        Tb_IsiVar3.Text = String.Empty

        Cb_MainMenu.Text = String.Empty
        Cb_Menu.Text = String.Empty
        Cb_SubMenu.Text = String.Empty
        Cb_SubMenuLv1.Text = String.Empty
        Cb_SubMenuLv2.Text = String.Empty
        Cb_SubMenuLv3.Text = String.Empty

        Cb_MainMenu.Items.Clear()
        Cb_Menu.Items.Clear()
        Cb_SubMenu.Items.Clear()
        Cb_SubMenuLv1.Items.Clear()
        Cb_SubMenuLv2.Items.Clear()
        Cb_SubMenuLv3.Items.Clear()

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
End Class