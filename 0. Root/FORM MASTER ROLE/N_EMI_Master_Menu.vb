Public Class N_EMI_Master_Menu

	Dim arrFilter As New ArrayList
	Dim arrMainMenu, arrMenu, arrSubMenu, arrSubMenuLv1, arrSubMenuLv2, arrSubMenuLv3 As New ArrayList

	Dim FocusedMenu As String

	Private Sub N_EMI_Master_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Cmb_Filter.Items.Clear() : arrFilter.Clear()
		Cmb_Filter.Items.Add("Main Menu") : arrFilter.Add("Title")
		Cmb_Filter.Items.Add("Menu") : arrFilter.Add("MenuName")
		Cmb_Filter.Items.Add("Sub Menu") : arrFilter.Add("SubMenuName")
		Cmb_Filter.Items.Add("Sub Menu Lv 1") : arrFilter.Add("SubMenuLv1Name")
		Cmb_Filter.Items.Add("Sub Menu Lv 2") : arrFilter.Add("SubMenuLv2Name")
		Cmb_Filter.Items.Add("Sub Menu Lv 3") : arrFilter.Add("SubMenuLv3Name")
		Cmb_Filter.Items.Add("Form") : arrFilter.Add("form")

		Kosong()
	End Sub

	Private Sub Kosong()

		FocusedMenu = ""
		Txt_MenuName.Enabled = True

		arrMainMenu.Clear()
		arrMenu.Clear()
		arrSubMenu.Clear()
		arrSubMenuLv1.Clear()
		arrSubMenuLv2.Clear()
		arrSubMenuLv3.Clear()

		Cmb_Filter.SelectedIndex = -1
		Txt_Filter.Text = ""

		Cmb_MainMenu.SelectedIndex = -1
		Cmb_Menu.Items.Clear()
		Cmb_SubMenu.Items.Clear()
		Cmb_SubMenu_Lv1.Items.Clear()
		Cmb_SubMenu_Lv2.Items.Clear()
		Cmb_SubMenu_Lv3.Items.Clear()

		Txt_ImagePath.Text = ""
		Txt_MenuName.Text = ""
		Txt_MenuOrder.Text = ""
		Txt_Form.Text = ""
		Txt_Var_1.Text = ""
		Txt_Var_2.Text = ""
		Txt_Var_3.Text = ""
		Txt_Isi_Var_1.Text = ""
		Txt_Isi_Var_2.Text = ""
		Txt_Isi_Var_3.Text = ""

		Txt_ImagePath.Enabled = True
		Txt_MenuName.Enabled = True
		Txt_MenuOrder.Enabled = True
		Txt_Form.Enabled = False
		Txt_Var_1.Enabled = False
		Txt_Var_2.Enabled = False
		Txt_Var_3.Enabled = False
		Txt_Isi_Var_1.Enabled = False
		Txt_Isi_Var_2.Enabled = False
		Txt_Isi_Var_3.Enabled = False

		Txt_ImagePath.BackColor = Color.White
		Txt_MenuName.BackColor = Color.White
		Txt_MenuOrder.BackColor = Color.White
		Txt_Form.BackColor = Color.LightGray
		Txt_Var_1.BackColor = Color.LightGray
		Txt_Var_2.BackColor = Color.LightGray
		Txt_Var_3.BackColor = Color.LightGray
		Txt_Isi_Var_1.BackColor = Color.LightGray
		Txt_Isi_Var_2.BackColor = Color.LightGray
		Txt_Isi_Var_3.BackColor = Color.LightGray

		Btn_Simpan.Tag = "SIMPAN"
		Btn_Simpan.Text = "&Simpan"

		LoadDataMenu()
		Load_MainMenu()
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Cmb_Filter.SelectedIndex > -1 Then
			'If Txt_Filter.Text.Trim.Length = 0 Then
			'	MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'	Exit Sub
			'End If
		End If

		LoadDataMenu()
	End Sub

	Private Sub LoadDataMenu()
		Try
			OpenConn()

			Dim Filter As String = ""
			If Cmb_Filter.SelectedIndex > -1 Then
				If Txt_Filter.Text.Trim.Length <> 0 Then
					Filter &= $"and {arrFilter(Cmb_Filter.SelectedIndex)} like '%{Txt_Filter.Text.Trim}%' "
				End If
			End If

			Tv_DataMenu.Nodes.Clear()
			SQL = $"
				select MainMenuID, Title, urut as UrutMainMenu, MenuID, MenuName, MenuOrder, SubMenuID, SubMenuName, SubMenuOrder,
					SubMenuLv1ID, SubMenuLv1Name, SubMenuLv1Order, SubMenuLv2ID, SubMenuLv2Name, SubMenuLv2Order, SubMenuLv3ID, SubMenuLv3Name, SubMenuLv3Order
				from vw_MenuHierarchy
				where 1=1
				{Filter}
				order by urut, Title, MenuOrder, MenuName, SubMenuOrder, SubMenuName, SubMenuLv1Order,
					SubMenuLv1Name, SubMenuLv2Order, SubMenuLv2Name, SubMenuLv3Order, SubMenuLv3Name
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					'====================
					'=     MAINMENU     =
					'====================
					Dim mainMenuKey As String = Dr("MainMenuID")
					Dim nodeMainMenu As TreeNode() = Tv_DataMenu.Nodes.Find(mainMenuKey, True)
					Dim currentMain As TreeNode

					If nodeMainMenu.Length = 0 Then
						Dim propertyNode As New propertyNode
						propertyNode.MenuID = Dr("MainMenuID")
						propertyNode.Level = 1

						currentMain = Tv_DataMenu.Nodes.Add(mainMenuKey, $"📁 {Dr("Title")}")
						currentMain.Tag = propertyNode
					Else
						currentMain = nodeMainMenu(0)
					End If

					'================
					'=     MENU     =
					'================
					If Not General_Class.CekNULL(Dr("MenuID")) = "" Then

						Dim menuKey As String = Dr("MenuID")
						Dim nodeMenu As TreeNode() = currentMain.Nodes.Find(menuKey, True)
						Dim currentMenu As TreeNode

						If nodeMenu.Length = 0 Then
							Dim propertyNode As New propertyNode
							propertyNode.MenuID = Dr("MenuID")
							propertyNode.Level = 2

							currentMenu = currentMain.Nodes.Add(menuKey, $"📁 {Dr("MenuName")}")
							currentMenu.Tag = propertyNode
						Else
							currentMenu = nodeMenu(0)
						End If

						'===================
						'=     SUBMENU     =
						'===================
						If Not General_Class.CekNULL(Dr("SubMenuID")) = "" Then
							Dim subKey As String = Dr("SubMenuID")
							Dim nodeSub As TreeNode() = currentMenu.Nodes.Find(subKey, True)
							Dim currentSub As TreeNode

							If nodeSub.Length = 0 Then
								Dim propertyNode As New propertyNode
								propertyNode.MenuID = Dr("SubMenuID")
								propertyNode.Level = 3

								currentSub = currentMenu.Nodes.Add(subKey, $"📁 {Dr("SubMenuName")}")
								currentSub.Tag = propertyNode
							Else
								currentSub = nodeSub(0)
							End If

							'========================
							'=     SUBMENU LV 1     =
							'========================
							If Not General_Class.CekNULL(Dr("SubMenuLv1ID")) = "" Then
								Dim subLv1Key As String = Dr("SubMenuLv1ID")
								Dim nodeSubLv1 As TreeNode() = currentSub.Nodes.Find(subLv1Key, True)
								Dim currentSubLV1 As TreeNode

								If nodeSubLv1.Length = 0 Then
									Dim propertyNode As New propertyNode
									propertyNode.MenuID = Dr("SubMenuLv1ID")
									propertyNode.Level = 4

									currentSubLV1 = currentSub.Nodes.Add(subLv1Key, $"📁 {Dr("SubMenuLv1Name")}")
									currentSubLV1.Tag = propertyNode
								Else
									currentSubLV1 = nodeSubLv1(0)
								End If

								'========================
								'=     SUBMENU LV 2     =
								'========================
								If Not General_Class.CekNULL(Dr("SubMenuLv2ID")) = "" Then
									Dim subLv2Key As String = Dr("SubMenuLv2ID")
									Dim nodeSubLv2 As TreeNode() = currentSubLV1.Nodes.Find(subLv2Key, True)
									Dim currentSubLV2 As TreeNode

									If nodeSubLv2.Length = 0 Then
										Dim propertyNode As New propertyNode
										propertyNode.MenuID = Dr("SubMenuLv2ID")
										propertyNode.Level = 5

										currentSubLV2 = currentSubLV1.Nodes.Add(subLv2Key, $"📁 {Dr("SubMenuLv2Name")}")
										currentSubLV2.Tag = propertyNode
									Else
										currentSubLV2 = nodeSubLv2(0)
									End If

									'========================
									'=     SUBMENU LV 3     =
									'========================
									If Not General_Class.CekNULL(Dr("SubMenuLv3ID")) = "" Then
										Dim subLv3Key As String = Dr("SubMenuLv3ID")
										Dim nodeSubLv3 As TreeNode() = currentSubLV2.Nodes.Find(subLv3Key, True)
										Dim currentSubLv3 As TreeNode

										If nodeSubLv3.Length = 0 Then
											Dim propertyNode As New propertyNode
											propertyNode.MenuID = Dr("SubMenuLv3ID")
											propertyNode.Level = 6

											currentSubLv3 = currentSubLV2.Nodes.Add(subLv3Key, $"📁 {Dr("SubMenuLv3Name")}")
											currentSubLv3.Tag = propertyNode
										Else
											currentSubLv3 = nodeSubLv3(0)
										End If
									End If

								End If

							End If

						End If

					End If
				Loop
			End Using

			If Cmb_Filter.SelectedIndex > -1 Then
				If Txt_Filter.Text.Trim.Length <> 0 Then

					Dim searchName As String = Txt_Filter.Text.Trim

					Tv_DataMenu.SelectedNode = Nothing
					ClearNodeBackColor(Tv_DataMenu.Nodes)

					If Cmb_Filter.SelectedItem <> "Form" Then

						Dim nodeFound As TreeNode = FindNodeByText(Tv_DataMenu.Nodes, searchName)

						If nodeFound IsNot Nothing Then
							Tv_DataMenu.SelectedNode = nodeFound

							nodeFound.EnsureVisible()

							'nodeFound.BackColor = Color.LightBlue

							Tv_DataMenu.Focus()
						Else
							MessageBox.Show("Menu tidak ditemukan.")
						End If
					End If
				End If
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Load_MainMenu()
		Try
			OpenConn()

			'LOAD MAINMENU
			Cmb_MainMenu.Items.Clear() : arrMainMenu.Clear()
			SQL = "select MainMenuID, TItle from MainMenu order by urut"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_MainMenu.Items.Add(Dr("TItle")) : arrMainMenu.Add(Dr("MainMenuID"))
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

		If Cmb_MainMenu.SelectedIndex = -1 Then Exit Sub

		Try
			OpenConn()

			Cmb_Menu.Items.Clear() : arrMenu.Clear()
			SQL = "select MenuID , MenuName from menus where MainMenuID = '" & arrMainMenu(Cmb_MainMenu.SelectedIndex) & "' order by MenuOrder "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Menu.Items.Add(Dr("MenuName")) : arrMenu.Add(Dr("MenuID"))
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

		If Cmb_Menu.SelectedIndex = -1 Then Exit Sub

		Try
			OpenConn()

			Cmb_SubMenu.Items.Clear() : arrSubMenu.Clear()
			SQL = "select SubMenuID, SubMenuName from submenus where MenuID = '" & arrMenu(Cmb_Menu.SelectedIndex) & "' order by SubMenuOrder "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_SubMenu.Items.Add(Dr("SubMenuName")) : arrSubMenu.Add(Dr("SubMenuID"))
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

		If Cmb_SubMenu.SelectedIndex = -1 Then Exit Sub

		Try
			OpenConn()

			Cmb_SubMenu_Lv1.Items.Clear() : arrSubMenuLv1.Clear()
			SQL = "select SubMenuLv1ID, SubMenuLv1Name from SubMenuLv1 where SubMenuID = '" & arrSubMenu(Cmb_SubMenu.SelectedIndex) & "' order by SubMenuLv1Order"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_SubMenu_Lv1.Items.Add(Dr("SubMenuLv1Name")) : arrSubMenuLv1.Add(Dr("SubMenuLv1ID"))
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

		If Cmb_SubMenu_Lv1.SelectedIndex = -1 Then Exit Sub

		Try
			OpenConn()

			Cmb_SubMenu_Lv2.Items.Clear() : arrSubMenuLv2.Clear()
			SQL = "select SubMenuLv2ID, SubMenuLv2Name from SubMenuLv2 where SubMenuLv1ID = '" & arrSubMenuLv1(Cmb_SubMenu_Lv1.SelectedIndex) & "' order by SubMenuLv2Order"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_SubMenu_Lv2.Items.Add(Dr("SubMenuLv2Name")) : arrSubMenuLv2.Add(Dr("SubMenuLv2ID"))
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

		If Cmb_SubMenu_Lv2.SelectedIndex = -1 Then Exit Sub

		Try
			OpenConn()

			Cmb_SubMenu_Lv3.Items.Clear() : arrSubMenuLv3.Clear()
			SQL = "select SubMenuLv3ID, SubMenuLv3Name from SubMenuLv3 where SubMenuLv2ID = '" & arrSubMenuLv2(Cmb_SubMenu_Lv2.SelectedIndex) & "' order by SubMenuLv3Order"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_SubMenu_Lv3.Items.Add(Dr("SubMenuLv3Name")) : arrSubMenuLv3.Add(Dr("SubMenuLv3ID"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Tv_DataMenu_MouseDown(sender As Object, e As MouseEventArgs) Handles Tv_DataMenu.MouseDown
		If e.Button = MouseButtons.Right Then
			Dim node As TreeNode = Tv_DataMenu.GetNodeAt(e.X, e.Y)

			If node IsNot Nothing Then
				Tv_DataMenu.SelectedNode = node
			End If
		End If
	End Sub

	Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
		If Tv_DataMenu.Nodes.Count = 0 Then Exit Sub

		Dim SelectedNode As TreeNode = Tv_DataMenu.SelectedNode

		If SelectedNode Is Nothing Then
			MessageBox.Show("Silakan pilih menu yang akan diedit terlebih dahulu.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		If SelectedNode.Tag IsNot Nothing AndAlso TypeOf SelectedNode.Tag Is propertyNode Then
			Dim dataNode As propertyNode = DirectCast(SelectedNode.Tag, propertyNode)

			Dim MenuID As String = dataNode.MenuID
			Dim LevelMenu As String = dataNode.Level

			Cmb_SubMenu_Lv3.Items.Clear()
			Cmb_SubMenu_Lv2.Items.Clear()
			Cmb_SubMenu_Lv1.Items.Clear()
			Cmb_SubMenu.Items.Clear()
			Cmb_Menu.Items.Clear()
			Cmb_MainMenu.SelectedIndex = -1

			Try
				OpenConn()

				If Not String.IsNullOrEmpty(LevelMenu) Then
					If LevelMenu = 1 Then

						SQL = $"
							select MainMenuID, ImagePath, TItle, urut
							from MainMenu
							where mainmenuid = '{MenuID}'
						"
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								Cmb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Dr("MainMenuID").ToString.Trim)
								Txt_ImagePath.Text = Dr("ImagePath")
								Txt_MenuName.Text = Dr("TItle")
								Txt_MenuOrder.Text = Dr("urut")
							End If
						End Using

					ElseIf LevelMenu = 2 Then

						SQL = $"
							select a.MainMenuID, a.ImagePath, a.TItle,
								b.MenuID, b.MenuName, b.MenuOrder
							from MainMenu a
								inner join Menus b on a.MainMenuID = b.MainMenuID
							where b.MenuID = '{MenuID}'
						"
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								Cmb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Dr("MainMenuID").ToString.Trim)
								Cmb_Menu.SelectedIndex = arrMenu.IndexOf(Dr("MenuID").ToString.Trim)
								Txt_ImagePath.Text = Dr("ImagePath")
								Txt_MenuName.Text = Dr("MenuName")
								Txt_MenuOrder.Text = Dr("MenuOrder")
							End If
						End Using

					ElseIf LevelMenu = 3 Then

						SQL = $"
							select a.MainMenuID, a.ImagePath, a.TItle, b.MenuID,
								c.SubMenuID, c.SubMenuName, c.SubMenuOrder,
								c.Variabel, c.Isi_Variabel, c.Variabel2, c.Isi_Variabel2, c.Variabel3, c.Isi_Variabel3, c.form
							from MainMenu a
								inner join Menus b on a.MainMenuID = b.MainMenuID
								inner join SubMenus c on b.MenuID = c.MenuID
							where c.SubMenuID = '{MenuID}'
						"
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								Cmb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Dr("MainMenuID").ToString.Trim)
								Cmb_Menu.SelectedIndex = arrMenu.IndexOf(Dr("MenuID").ToString.Trim)
								Cmb_SubMenu.SelectedIndex = arrSubMenu.IndexOf(Dr("SubMenuID").ToString.Trim)
								Txt_ImagePath.Text = Dr("ImagePath")
								Txt_MenuName.Text = Dr("SubMenuName")
								Txt_MenuOrder.Text = Dr("SubMenuOrder")
								Txt_Var_1.Text = If(General_Class.CekNULL(Dr("Variabel")) = "", "", Dr("Variabel"))
								Txt_Var_2.Text = If(General_Class.CekNULL(Dr("Variabel2")) = "", "", Dr("Variabel2"))
								Txt_Var_3.Text = If(General_Class.CekNULL(Dr("Variabel3")) = "", "", Dr("Variabel3"))
								Txt_Isi_Var_1.Text = If(General_Class.CekNULL(Dr("Isi_Variabel")) = "", "", Dr("Isi_Variabel"))
								Txt_Isi_Var_2.Text = If(General_Class.CekNULL(Dr("Isi_Variabel2")) = "", "", Dr("Isi_Variabel2"))
								Txt_Isi_Var_3.Text = If(General_Class.CekNULL(Dr("Isi_Variabel3")) = "", "", Dr("Isi_Variabel3"))
								Txt_Form.Text = If(General_Class.CekNULL(Dr("form")) = "", "", Dr("form"))
							End If
						End Using

					ElseIf LevelMenu = 4 Then

						SQL = $"
							select a.MainMenuID, a.ImagePath, a.TItle, b.MenuID, c.SubMenuID,
								d.SubMenuLv1ID, d.SubMenuLv1Name, d.SubMenuLv1Order,
								d.Variabel, d.Isi_Variabel, d.Variabel2, d.Isi_Variabel2, d.Variabel3, d.Isi_Variabel3, d.Form
							from MainMenu a
								inner join Menus b on a.MainMenuID = b.MainMenuID
								inner join SubMenus c on b.MenuID = c.MenuID
								inner join SubMenuLv1 d on c.SubMenuID = d.SubMenuID
							where d.SubMenuLv1ID = '{MenuID}'
						"
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								Cmb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Dr("MainMenuID").ToString.Trim)
								Cmb_Menu.SelectedIndex = arrMenu.IndexOf(Dr("MenuID").ToString.Trim)
								Cmb_SubMenu.SelectedIndex = arrSubMenu.IndexOf(Dr("SubMenuID").ToString.Trim)
								Cmb_SubMenu_Lv1.SelectedIndex = arrSubMenuLv1.IndexOf(Dr("SubMenuLv1ID").ToString.Trim)
								Txt_ImagePath.Text = Dr("ImagePath")
								Txt_MenuName.Text = Dr("SubMenuLv1Name")
								Txt_MenuOrder.Text = Dr("SubMenuLv1Order")
								Txt_Var_1.Text = If(General_Class.CekNULL(Dr("Variabel")) = "", "", Dr("Variabel"))
								Txt_Var_2.Text = If(General_Class.CekNULL(Dr("Variabel2")) = "", "", Dr("Variabel2"))
								Txt_Var_3.Text = If(General_Class.CekNULL(Dr("Variabel3")) = "", "", Dr("Variabel3"))
								Txt_Isi_Var_1.Text = If(General_Class.CekNULL(Dr("Isi_Variabel")) = "", "", Dr("Isi_Variabel"))
								Txt_Isi_Var_2.Text = If(General_Class.CekNULL(Dr("Isi_Variabel2")) = "", "", Dr("Isi_Variabel2"))
								Txt_Isi_Var_3.Text = If(General_Class.CekNULL(Dr("Isi_Variabel3")) = "", "", Dr("Isi_Variabel3"))
								Txt_Form.Text = If(General_Class.CekNULL(Dr("form")) = "", "", Dr("form"))
							End If
						End Using

					ElseIf LevelMenu = 5 Then

						SQL = $"
							select a.MainMenuID, a.ImagePath, a.TItle, b.MenuID, c.SubMenuID, d.SubMenuLv1ID,
								e.SubMenuLv2ID, e.SubMenuLv2Name, e.SubMenuLv2Order,
								e.Variabel, e.Isi_Variabel, e.Variabel2, e.Isi_Variabel2, e.Variabel3, e.Isi_Variabel3, e.Form
							from MainMenu a
								inner join Menus b on a.MainMenuID = b.MainMenuID
								inner join SubMenus c on b.MenuID = c.MenuID
								inner join SubMenuLv1 d on c.SubMenuID = d.SubMenuID
								inner join SubMenuLv2 e on d.SubMenuLv1ID = e.SubMenuLv1ID
							where e.SubMenuLv2ID = '{MenuID}'
						"
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								Cmb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Dr("MainMenuID").ToString.Trim)
								Cmb_Menu.SelectedIndex = arrMenu.IndexOf(Dr("MenuID").ToString.Trim)
								Cmb_SubMenu.SelectedIndex = arrSubMenu.IndexOf(Dr("SubMenuID").ToString.Trim)
								Cmb_SubMenu_Lv1.SelectedIndex = arrSubMenuLv1.IndexOf(Dr("SubMenuLv1ID").ToString.Trim)
								Cmb_SubMenu_Lv2.SelectedIndex = arrSubMenuLv2.IndexOf(Dr("SubMenuLv2ID").ToString.Trim)
								Txt_ImagePath.Text = Dr("ImagePath")
								Txt_MenuName.Text = Dr("SubMenuLv2Name")
								Txt_MenuOrder.Text = Dr("SubMenuLv2Order")
								Txt_Var_1.Text = If(General_Class.CekNULL(Dr("Variabel")) = "", "", Dr("Variabel"))
								Txt_Var_2.Text = If(General_Class.CekNULL(Dr("Variabel2")) = "", "", Dr("Variabel2"))
								Txt_Var_3.Text = If(General_Class.CekNULL(Dr("Variabel3")) = "", "", Dr("Variabel3"))
								Txt_Isi_Var_1.Text = If(General_Class.CekNULL(Dr("Isi_Variabel")) = "", "", Dr("Isi_Variabel"))
								Txt_Isi_Var_2.Text = If(General_Class.CekNULL(Dr("Isi_Variabel2")) = "", "", Dr("Isi_Variabel2"))
								Txt_Isi_Var_3.Text = If(General_Class.CekNULL(Dr("Isi_Variabel3")) = "", "", Dr("Isi_Variabel3"))
								Txt_Form.Text = If(General_Class.CekNULL(Dr("form")) = "", "", Dr("form"))
							End If
						End Using

					ElseIf LevelMenu = 6 Then

						SQL = $"
							select a.MainMenuID, a.ImagePath, a.TItle, b.MenuID, c.SubMenuID, d.SubMenuLv1ID, e.SubMenuLv2ID,
								f.SubMenuLv3ID, f.SubMenuLv3Name, f.SubMenuLv3Order,
								f.Variabel, f.Isi_Variabel, f.Variabel2, f.Isi_Variabel2, f.Variabel3, f.Isi_Variabel3, f.Form
							from MainMenu a
								inner join Menus b on a.MainMenuID = b.MainMenuID
								inner join SubMenus c on b.MenuID = c.MenuID
								inner join SubMenuLv1 d on c.SubMenuID = d.SubMenuID
								inner join SubMenuLv2 e on d.SubMenuLv1ID = e.SubMenuLv1ID
								inner join SubMenuLv3 f on e.SubMenuLv2ID = f.SubMenuLv2ID
							where f.SubMenuLv3ID = '{MenuID}'
						"
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								Cmb_MainMenu.SelectedIndex = arrMainMenu.IndexOf(Dr("MainMenuID").ToString.Trim)
								Cmb_Menu.SelectedIndex = arrMenu.IndexOf(Dr("MenuID").ToString.Trim)
								Cmb_SubMenu.SelectedIndex = arrSubMenu.IndexOf(Dr("SubMenuID").ToString.Trim)
								Cmb_SubMenu_Lv1.SelectedIndex = arrSubMenuLv1.IndexOf(Dr("SubMenuLv1ID").ToString.Trim)
								Cmb_SubMenu_Lv2.SelectedIndex = arrSubMenuLv2.IndexOf(Dr("SubMenuLv2ID").ToString.Trim)
								Cmb_SubMenu_Lv3.SelectedIndex = arrSubMenuLv2.IndexOf(Dr("SubMenuLv3ID").ToString.Trim)
								Txt_ImagePath.Text = Dr("ImagePath")
								Txt_MenuName.Text = Dr("SubMenuLv3Name")
								Txt_MenuOrder.Text = Dr("SubMenuLv3Order")
								Txt_Var_1.Text = If(General_Class.CekNULL(Dr("Variabel")) = "", "", Dr("Variabel"))
								Txt_Var_2.Text = If(General_Class.CekNULL(Dr("Variabel2")) = "", "", Dr("Variabel2"))
								Txt_Var_3.Text = If(General_Class.CekNULL(Dr("Variabel3")) = "", "", Dr("Variabel3"))
								Txt_Isi_Var_1.Text = If(General_Class.CekNULL(Dr("Isi_Variabel")) = "", "", Dr("Isi_Variabel"))
								Txt_Isi_Var_2.Text = If(General_Class.CekNULL(Dr("Isi_Variabel2")) = "", "", Dr("Isi_Variabel2"))
								Txt_Isi_Var_3.Text = If(General_Class.CekNULL(Dr("Isi_Variabel3")) = "", "", Dr("Isi_Variabel3"))
								Txt_Form.Text = If(General_Class.CekNULL(Dr("form")) = "", "", Dr("form"))
							End If
						End Using

					End If
				End If

				Btn_Simpan.Text = "&Update"
				Btn_Simpan.Tag = "UPDATE"

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		Else
			MessageBox.Show("Data identitas menu tidak ditemukan.")
		End If
	End Sub

	'=======================================================================================================================================
	'=     HANDLE SELECTED INDEX
	'=======================================================================================================================================
	Private Sub Cmb_MainMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_MainMenu.SelectedIndexChanged
		If Cmb_MainMenu.SelectedIndex = -1 Then Exit Sub

		Cmb_Menu.Items.Clear() : arrMenu.Clear()
		Cmb_SubMenu.Items.Clear() : arrSubMenu.Clear()
		Cmb_SubMenu_Lv1.Items.Clear() : arrSubMenuLv1.Clear()
		Cmb_SubMenu_Lv2.Items.Clear() : arrSubMenuLv2.Clear()
		Cmb_SubMenu_Lv3.Items.Clear() : arrSubMenuLv3.Clear()

		Txt_ImagePath.Text = ""
		Txt_MenuName.Text = ""
		Txt_MenuOrder.Text = ""
		Txt_Form.Text = ""
		Txt_Var_1.Text = ""
		Txt_Var_2.Text = ""
		Txt_Var_3.Text = ""
		Txt_Isi_Var_1.Text = ""
		Txt_Isi_Var_2.Text = ""
		Txt_Isi_Var_3.Text = ""

		Txt_MenuName.Enabled = True
		Txt_MenuOrder.Enabled = True
		Txt_Form.Enabled = False
		Txt_Var_1.Enabled = False
		Txt_Var_2.Enabled = False
		Txt_Var_3.Enabled = False
		Txt_Isi_Var_1.Enabled = False
		Txt_Isi_Var_2.Enabled = False
		Txt_Isi_Var_3.Enabled = False

		If Btn_Simpan.Tag = "UPDATE" Then
			Txt_ImagePath.Enabled = True
			Txt_ImagePath.BackColor = Color.White
		Else
			Txt_ImagePath.Enabled = False
			Txt_ImagePath.BackColor = Color.LightGray

		End If

		Txt_MenuName.BackColor = Color.White
		Txt_MenuOrder.BackColor = Color.White
		Txt_Form.BackColor = Color.LightGray
		Txt_Var_1.BackColor = Color.LightGray
		Txt_Var_2.BackColor = Color.LightGray
		Txt_Var_3.BackColor = Color.LightGray
		Txt_Isi_Var_1.BackColor = Color.LightGray
		Txt_Isi_Var_2.BackColor = Color.LightGray
		Txt_Isi_Var_3.BackColor = Color.LightGray

		LoadMenu()

		FocusedMenu = "MENU"
	End Sub

	Private Sub Cmb_Menu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Menu.SelectedIndexChanged
		If Cmb_Menu.SelectedIndex = -1 Then Exit Sub

		Cmb_SubMenu.Items.Clear() : arrSubMenu.Clear()
		Cmb_SubMenu_Lv1.Items.Clear() : arrSubMenuLv1.Clear()
		Cmb_SubMenu_Lv2.Items.Clear() : arrSubMenuLv2.Clear()
		Cmb_SubMenu_Lv3.Items.Clear() : arrSubMenuLv3.Clear()

		Txt_ImagePath.Text = ""
		Txt_MenuName.Text = ""
		Txt_MenuOrder.Text = ""
		Txt_Form.Text = ""
		Txt_Var_1.Text = ""
		Txt_Var_2.Text = ""
		Txt_Var_3.Text = ""
		Txt_Isi_Var_1.Text = ""
		Txt_Isi_Var_2.Text = ""
		Txt_Isi_Var_3.Text = ""

		Txt_ImagePath.Enabled = False
		Txt_MenuName.Enabled = True
		Txt_MenuOrder.Enabled = True

		If Btn_Simpan.Tag = "UPDATE" Then

			Txt_Form.Enabled = False
			Txt_Var_1.Enabled = False
			Txt_Var_2.Enabled = False
			Txt_Var_3.Enabled = False
			Txt_Isi_Var_1.Enabled = False
			Txt_Isi_Var_2.Enabled = False
			Txt_Isi_Var_3.Enabled = False

			Txt_MenuName.BackColor = Color.LightGray
			Txt_Var_1.BackColor = Color.LightGray
			Txt_Var_2.BackColor = Color.LightGray
			Txt_Var_3.BackColor = Color.LightGray
			Txt_Isi_Var_1.BackColor = Color.LightGray
			Txt_Isi_Var_2.BackColor = Color.LightGray
			Txt_Isi_Var_3.BackColor = Color.LightGray
		Else
			Txt_Form.Enabled = True
			Txt_Var_1.Enabled = True
			Txt_Var_2.Enabled = True
			Txt_Var_3.Enabled = True
			Txt_Isi_Var_1.Enabled = True
			Txt_Isi_Var_2.Enabled = True
			Txt_Isi_Var_3.Enabled = True

			Txt_Form.BackColor = Color.White
			Txt_Var_1.BackColor = Color.White
			Txt_Var_2.BackColor = Color.White
			Txt_Var_3.BackColor = Color.White
			Txt_Isi_Var_1.BackColor = Color.White
			Txt_Isi_Var_2.BackColor = Color.White
			Txt_Isi_Var_3.BackColor = Color.White
		End If

		Txt_ImagePath.BackColor = Color.LightGray
		Txt_MenuName.BackColor = Color.White
		Txt_MenuOrder.BackColor = Color.White

		LoadSubMenu()

		FocusedMenu = "SUBMENU"
	End Sub

	Private Sub Cmb_SubMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_SubMenu.SelectedIndexChanged
		If Cmb_SubMenu.SelectedIndex = -1 Then Exit Sub

		Cmb_SubMenu_Lv1.Items.Clear() : arrSubMenuLv1.Clear()
		Cmb_SubMenu_Lv2.Items.Clear() : arrSubMenuLv2.Clear()
		Cmb_SubMenu_Lv3.Items.Clear() : arrSubMenuLv3.Clear()

		Txt_ImagePath.Text = ""
		Txt_MenuName.Text = ""
		Txt_MenuOrder.Text = ""
		Txt_Form.Text = ""
		Txt_Var_1.Text = ""
		Txt_Var_2.Text = ""
		Txt_Var_3.Text = ""
		Txt_Isi_Var_1.Text = ""
		Txt_Isi_Var_2.Text = ""
		Txt_Isi_Var_3.Text = ""

		Txt_ImagePath.Enabled = False
		Txt_MenuName.Enabled = True
		Txt_MenuOrder.Enabled = True
		Txt_Form.Enabled = True
		Txt_Var_1.Enabled = True
		Txt_Var_2.Enabled = True
		Txt_Var_3.Enabled = True
		Txt_Isi_Var_1.Enabled = True
		Txt_Isi_Var_2.Enabled = True
		Txt_Isi_Var_3.Enabled = True

		Txt_ImagePath.BackColor = Color.LightGray
		Txt_MenuName.BackColor = Color.White
		Txt_MenuOrder.BackColor = Color.White
		Txt_Form.BackColor = Color.White
		Txt_Var_1.BackColor = Color.White
		Txt_Var_2.BackColor = Color.White
		Txt_Var_3.BackColor = Color.White
		Txt_Isi_Var_1.BackColor = Color.White
		Txt_Isi_Var_2.BackColor = Color.White
		Txt_Isi_Var_3.BackColor = Color.White

		LoadSubMenuLv1()

		FocusedMenu = "SUBMENULV1"
	End Sub

	Private Sub Cmb_SubMenu_Lv1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_SubMenu_Lv1.SelectedIndexChanged
		If Cmb_SubMenu_Lv1.SelectedIndex = -1 Then Exit Sub

		Cmb_SubMenu_Lv2.Items.Clear() : arrSubMenuLv2.Clear()
		Cmb_SubMenu_Lv3.Items.Clear() : arrSubMenuLv3.Clear()

		Txt_ImagePath.Text = ""
		Txt_MenuName.Text = ""
		Txt_MenuOrder.Text = ""
		Txt_Form.Text = ""
		Txt_Var_1.Text = ""
		Txt_Var_2.Text = ""
		Txt_Var_3.Text = ""
		Txt_Isi_Var_1.Text = ""
		Txt_Isi_Var_2.Text = ""
		Txt_Isi_Var_3.Text = ""

		Txt_ImagePath.Enabled = False
		Txt_MenuName.Enabled = True
		Txt_MenuOrder.Enabled = True
		Txt_Form.Enabled = True
		Txt_Var_1.Enabled = True
		Txt_Var_2.Enabled = True
		Txt_Var_3.Enabled = True
		Txt_Isi_Var_1.Enabled = True
		Txt_Isi_Var_2.Enabled = True
		Txt_Isi_Var_3.Enabled = True

		Txt_ImagePath.BackColor = Color.LightGray
		Txt_MenuName.BackColor = Color.White
		Txt_MenuOrder.BackColor = Color.White
		Txt_Form.BackColor = Color.White
		Txt_Var_1.BackColor = Color.White
		Txt_Var_2.BackColor = Color.White
		Txt_Var_3.BackColor = Color.White
		Txt_Isi_Var_1.BackColor = Color.White
		Txt_Isi_Var_2.BackColor = Color.White
		Txt_Isi_Var_3.BackColor = Color.White

		LoadSubMenuLv2()

		FocusedMenu = "SUBMENULV2"
	End Sub

	Private Sub Cmb_SubMenu_Lv2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_SubMenu_Lv2.SelectedIndexChanged
		If Cmb_SubMenu_Lv2.SelectedIndex = -1 Then Exit Sub

		Cmb_SubMenu_Lv3.Items.Clear() : arrSubMenuLv3.Clear()

		Txt_ImagePath.Text = ""
		Txt_MenuName.Text = ""
		Txt_MenuOrder.Text = ""
		Txt_Form.Text = ""
		Txt_Var_1.Text = ""
		Txt_Var_2.Text = ""
		Txt_Var_3.Text = ""
		Txt_Isi_Var_1.Text = ""
		Txt_Isi_Var_2.Text = ""
		Txt_Isi_Var_3.Text = ""

		Txt_ImagePath.Enabled = False
		Txt_MenuName.Enabled = True
		Txt_MenuOrder.Enabled = True
		Txt_Form.Enabled = True
		Txt_Var_1.Enabled = True
		Txt_Var_2.Enabled = True
		Txt_Var_3.Enabled = True
		Txt_Isi_Var_1.Enabled = True
		Txt_Isi_Var_2.Enabled = True
		Txt_Isi_Var_3.Enabled = True

		Txt_ImagePath.BackColor = Color.LightGray
		Txt_MenuName.BackColor = Color.White
		Txt_MenuOrder.BackColor = Color.White
		Txt_Form.BackColor = Color.White
		Txt_Var_1.BackColor = Color.White
		Txt_Var_2.BackColor = Color.White
		Txt_Var_3.BackColor = Color.White
		Txt_Isi_Var_1.BackColor = Color.White
		Txt_Isi_Var_2.BackColor = Color.White
		Txt_Isi_Var_3.BackColor = Color.White

		LoadSubMenuLv3()
		FocusedMenu = "SUBMENULV3"

	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Txt_MenuName.Text.Trim.Length = 0 Then
			MessageBox.Show("Menu Name Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt_MenuOrder.Text.Trim.Length = 0 Then
			MessageBox.Show("Menu Order Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Dim newImagePath = cekEmptyString(Txt_ImagePath.Text.Trim)
		Dim newMenuName = cekEmptyString(Txt_MenuName.Text.Trim)
		Dim newMenuOrder = cekEmptyString(Txt_MenuOrder.Text.Trim)
		Dim newMenuForm = cekEmptyString(Txt_Form.Text.Trim)
		Dim newMenuVar1 = cekEmptyString(Txt_Var_1.Text.Trim)
		Dim newMenuVar2 = cekEmptyString(Txt_Var_2.Text.Trim)
		Dim newMenuVar3 = cekEmptyString(Txt_Var_3.Text.Trim)
		Dim newMenuIsiVar1 = cekEmptyString(Txt_Isi_Var_1.Text.Trim)
		Dim newMenuIsiVar2 = cekEmptyString(Txt_Isi_Var_2.Text.Trim)
		Dim newMenuIsiVar3 = cekEmptyString(Txt_Isi_Var_3.Text.Trim)

		Dim action As String = ""
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If Btn_Simpan.Tag = "SIMPAN" Then

				If Not newMenuName = "" Then
					If Not Cmb_MainMenu.Text = "" Then
						If Not Cmb_Menu.Text = "" Then
							If Not Cmb_SubMenu.Text = "" Then
								If Not Cmb_SubMenu_Lv1.Text = "" Then
									If Not Cmb_SubMenu_Lv2.Text = "" Then
										If Not Cmb_SubMenu_Lv3.Text = "" Then
										Else
											'Cek SubMenu2
											SQL = "select form from SubMenuLv2 where SubMenuLv2ID = '" & arrSubMenuLv2(Cmb_SubMenu_Lv2.SelectedIndex) & "' "
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
												SQL = SQL & "(SubMenuLv3_'" & getUniqueID() & "', '" & arrSubMenuLv2(Cmb_SubMenu_Lv2.SelectedIndex) & "', " & newMenuName & ", " & newMenuOrder & ", "
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
										SQL = "select form from SubMenuLv1 where SubMenuLv1ID = '" & arrSubMenuLv1(Cmb_SubMenu_Lv1.SelectedIndex) & "' "
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
											SQL = SQL & "('SubMenuLv2_" & getUniqueID() & "', '" & arrSubMenuLv1(Cmb_SubMenu_Lv1.SelectedIndex) & "', " & newMenuName & ", " & newMenuOrder & ", "
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
									SQL = "select form from SubMenus where SubMenuID = '" & arrSubMenu(Cmb_SubMenu.SelectedIndex) & "' "
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
										SQL = SQL & "('SubMenuLv1ID_" & getUniqueID() & "', '" & arrSubMenu(Cmb_SubMenu.SelectedIndex) & "', " & newMenuName & ", " & newMenuOrder & ", " & newMenuForm & ", "
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
									SQL = SQL & "('SubMenu_" & getUniqueID() & "', " & newMenuName & ", '" & arrMenu(Cmb_Menu.SelectedIndex) & "', " & newMenuOrder & ", " & newMenuForm & " "
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
								SQL = SQL & "('Menu_" & getUniqueID() & "', '" & arrMainMenu(Cmb_MainMenu.SelectedIndex) & "', " & newMenuName & ", " & newMenuOrder & " , NULL)"
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

				If Cmb_SubMenu_Lv3.SelectedIndex = -1 Then
					If Cmb_SubMenu_Lv2.SelectedIndex = -1 Then
						If Cmb_SubMenu_Lv1.SelectedIndex = -1 Then
							If Cmb_SubMenu.SelectedIndex = -1 Then
								If Cmb_Menu.SelectedIndex = -1 Then
									If Cmb_MainMenu.SelectedIndex = -1 Then
									Else
										SQL = "update MainMenu set ImagePath = " & newImagePath & ", TItle = " & newMenuName & ", urut = " & newMenuOrder & " "
										SQL = SQL & "where MainMenuID = '" & arrMainMenu(Cmb_MainMenu.SelectedIndex) & "' "
										ExecuteTrans(SQL)

									End If
								Else
									SQL = "update Menus set MenuName = " & newMenuName & ", MenuOrder = " & newMenuOrder & " "
									SQL = SQL & "where MenuID = '" & arrMenu(Cmb_Menu.SelectedIndex) & "' "
									ExecuteTrans(SQL)

								End If
							Else
								SQL = "update submenus set SubMenuName = " & newMenuName & ", SubMenuOrder = " & newMenuOrder & ", form = " & newMenuForm & ", "
								SQL = SQL & "Variabel = " & newMenuVar1 & ", Isi_Variabel = " & newMenuIsiVar1 & ", "
								SQL = SQL & "Variabel2 = " & newMenuVar2 & ", Isi_Variabel2 = " & newMenuIsiVar2 & ", "
								SQL = SQL & "Variabel3 = " & newMenuVar3 & ", Isi_Variabel3 = " & newMenuIsiVar3 & " "
								SQL = SQL & "where SubMenuID = '" & arrSubMenu(Cmb_SubMenu.SelectedIndex) & "' "
								ExecuteTrans(SQL)

							End If
						Else
							SQL = "update SubMenuLv1 set SubMenuLv1Name = " & newMenuName & ", SubMenuLv1Order = " & newMenuOrder & ", form = " & newMenuForm & ", "
							SQL = SQL & "Variabel = " & newMenuVar1 & ", Isi_Variabel = " & newMenuIsiVar1 & ", "
							SQL = SQL & "Variabel2 = " & newMenuVar2 & ", Isi_Variabel2 = " & newMenuIsiVar2 & ", "
							SQL = SQL & "Variabel3 = " & newMenuVar3 & ", Isi_Variabel3 = " & newMenuIsiVar3 & " "
							SQL = SQL & "where SubMenuLv1ID = '" & arrSubMenuLv1(Cmb_SubMenu_Lv1.SelectedIndex) & "' "
							ExecuteTrans(SQL)

						End If
					Else
						SQL = "update SubMenuLv2 set SubMenuLv2Name = " & newMenuName & ", SubMenuLv2Order = " & newMenuOrder & ", form = " & newMenuForm & ", "
						SQL = SQL & "Variabel = " & newMenuVar1 & ", Isi_Variabel = " & newMenuIsiVar1 & ", "
						SQL = SQL & "Variabel2 = " & newMenuVar2 & ", Isi_Variabel2 = " & newMenuIsiVar2 & ", "
						SQL = SQL & "Variabel3 = " & newMenuVar3 & ", Isi_Variabel3 = " & newMenuIsiVar3 & " "
						SQL = SQL & "where SubMenuLv2ID = '" & arrSubMenuLv2(Cmb_SubMenu_Lv2.SelectedIndex) & "' "
						ExecuteTrans(SQL)

					End If
				Else
					SQL = "update SubMenuLv3 set SubMenuLv3Name = " & newMenuName & ", SubMenuLv3Order = " & newMenuOrder & ", form = " & newMenuForm & ", "
					SQL = SQL & "Variabel = " & newMenuVar1 & ", Isi_Variabel = " & newMenuIsiVar1 & ", "
					SQL = SQL & "Variabel2 = " & newMenuVar2 & ", Isi_Variabel2 = " & newMenuIsiVar2 & ", "
					SQL = SQL & "Variabel3 = " & newMenuVar3 & ", Isi_Variabel3 = " & newMenuIsiVar3 & " "
					SQL = SQL & "where SubMenuLv3ID = '" & arrSubMenuLv3(Cmb_SubMenu_Lv3.SelectedIndex) & "' "
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

		Kosong()
	End Sub

	Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click

		Dim pertanyaan As String = MessageBox.Show("Yakin Ingin Hapus?", "Master Menu", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If pertanyaan = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If Cmb_SubMenu_Lv3.SelectedIndex = -1 Then
				If Cmb_SubMenu_Lv2.SelectedIndex = -1 Then
					If Cmb_SubMenu_Lv1.SelectedIndex = -1 Then
						If Cmb_SubMenu.SelectedIndex = -1 Then
							If Cmb_Menu.SelectedIndex = -1 Then
								If Cmb_MainMenu.SelectedIndex = -1 Then
									MessageBox.Show("Tidak Ada Data yang Bisa Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
								Else
									SQL = "delete RoleMainMenus where MainMenuID = '" & arrMainMenu(Cmb_MainMenu.SelectedIndex) & "'"
									ExecuteTrans(SQL)

									SQL = "delete from MainMenu where MainMenuID='" & arrMainMenu(Cmb_MainMenu.SelectedIndex) & "'"
									ExecuteTrans(SQL)

									MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
								End If
							Else
								SQL = "delete RoleMenus where MenuID = '" & arrMenu(Cmb_Menu.SelectedIndex) & "'"
								ExecuteTrans(SQL)

								SQL = "delete from menus where MenuID='" & arrMenu(Cmb_Menu.SelectedIndex) & "'"
								ExecuteTrans(SQL)

								MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
							End If
						Else
							SQL = "delete RoleSubMenu where SubMenuID = '" & arrSubMenu(Cmb_SubMenu.SelectedIndex) & "'"
							ExecuteTrans(SQL)

							SQL = "delete from SubMenus where SubMenuID='" & arrSubMenu(Cmb_SubMenu.SelectedIndex) & "'"
							ExecuteTrans(SQL)

							MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
						End If
					Else
						SQL = "delete RoleSubMenuLv1 where SubMenuLv1ID = '" & arrSubMenuLv1(Cmb_SubMenu_Lv1.SelectedIndex) & "'"
						ExecuteTrans(SQL)

						SQL = "delete from SubMenuLv1 where SubMenuLv1ID='" & arrSubMenuLv1(Cmb_SubMenu_Lv1.SelectedIndex) & "'"
						ExecuteTrans(SQL)

						MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
					End If
				Else
					SQL = "delete RoleSubMenuLv2 where SubMenuLv2ID = '" & arrSubMenuLv2(Cmb_SubMenu_Lv2.SelectedIndex) & "'"
					ExecuteTrans(SQL)

					SQL = "delete from SubMenuLv2 where SubMenuLv2ID='" & arrSubMenuLv2(Cmb_SubMenu_Lv2.SelectedIndex) & "'"
					ExecuteTrans(SQL)

					MessageBox.Show("Berhasil DiHapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
				End If
			Else
				SQL = "delete RoleSubMenuLv3 where SubMenuLv3ID = '" & arrSubMenuLv3(Cmb_SubMenu_Lv3.SelectedIndex) & "'"
				ExecuteTrans(SQL)

				SQL = "delete from SubMenuLv3 where SubMenuLv3ID='" & arrSubMenuLv3(Cmb_SubMenu_Lv3.SelectedIndex) & "'"
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

		Kosong()
	End Sub

	Private Sub Cmb_SubMenu_Lv3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_SubMenu_Lv3.SelectedIndexChanged
		If Cmb_SubMenu_Lv3.SelectedIndex = -1 Then Exit Sub

		Txt_ImagePath.Text = ""
		Txt_MenuName.Text = ""
		Txt_MenuOrder.Text = ""
		Txt_Form.Text = ""
		Txt_Var_1.Text = ""
		Txt_Var_2.Text = ""
		Txt_Var_3.Text = ""
		Txt_Isi_Var_1.Text = ""
		Txt_Isi_Var_2.Text = ""
		Txt_Isi_Var_3.Text = ""

		Txt_ImagePath.Enabled = False
		Txt_MenuName.Enabled = True
		Txt_MenuOrder.Enabled = True
		Txt_Form.Enabled = True
		Txt_Var_1.Enabled = True
		Txt_Var_2.Enabled = True
		Txt_Var_3.Enabled = True
		Txt_Isi_Var_1.Enabled = True
		Txt_Isi_Var_2.Enabled = True
		Txt_Isi_Var_3.Enabled = True

		Txt_ImagePath.BackColor = Color.LightGray
		Txt_MenuName.BackColor = Color.White
		Txt_MenuOrder.BackColor = Color.White
		Txt_Form.BackColor = Color.White
		Txt_Var_1.BackColor = Color.White
		Txt_Var_2.BackColor = Color.White
		Txt_Var_3.BackColor = Color.White
		Txt_Isi_Var_1.BackColor = Color.White
		Txt_Isi_Var_2.BackColor = Color.White
		Txt_Isi_Var_3.BackColor = Color.White
	End Sub

	Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
		If Cmb_Filter.SelectedIndex = -1 Then
			Txt_Filter.Enabled = False
			Txt_Filter.BackColor = Color.FromArgb(235, 235, 235)
		Else
			Txt_Filter.Enabled = True
			Txt_Filter.BackColor = Color.White
		End If

		Txt_Filter.Text = ""

	End Sub

	Private Sub Btn_Input_Role_Button_Click(sender As Object, e As EventArgs) Handles Btn_Input_Role_Button.Click
		If Cmb_MainMenu.SelectedIndex = -1 Then
			MessageBox.Show("Harap Pilih Dahulu Main Menu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Cmb_Menu.SelectedIndex = -1 Then
			MessageBox.Show("Harap Pilih Dahulu Menu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt_Form.Text.Trim.Length = 0 Then
			MessageBox.Show("Form Harus Diisi Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Txt_MenuName.Enabled = False
		N_EMI_SD_Master_Menu.Txt_CurrentForm.Text = Txt_Form.Text.Trim
		N_EMI_SD_Master_Menu.ShowDialog()

	End Sub

	'=======================================================================================================================================
	'=     HELPER
	'=======================================================================================================================================

	Private Sub Tv_DataMenu_AfterExpand(sender As Object, e As TreeViewEventArgs) Handles Tv_DataMenu.AfterExpand
		If e.Node.Text.StartsWith("📁") Then
			e.Node.Text = e.Node.Text.Replace("📁", "📂")
		End If
	End Sub

	Private Sub Txt_MenuOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_MenuOrder.KeyPress
		If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
			e.Handled = True
		End If
	End Sub

	Private Sub Tv_DataMenu_AfterCollapse(sender As Object, e As TreeViewEventArgs) Handles Tv_DataMenu.AfterCollapse
		If e.Node.Text.StartsWith("📂") Then
			e.Node.Text = e.Node.Text.Replace("📂", "📁")
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

	Private Class propertyNode
		Public Property MenuID As String
		Public Property Level As String
	End Class

	Protected Overrides Sub WndProc(ByRef m As Message)
		If m.Msg = &HA3 Then
			Return
		End If

		MyBase.WndProc(m)
	End Sub

	Private Function FindNodeByText(ByVal nodes As TreeNodeCollection, ByVal searchText As String) As TreeNode
		For Each node As TreeNode In nodes
			If node.Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 Then
				Return node
			End If
			Dim foundChild As TreeNode = FindNodeByText(node.Nodes, searchText)
			If foundChild IsNot Nothing Then Return foundChild
		Next
		Return Nothing
	End Function

	Private Sub ClearNodeBackColor(ByVal nodes As TreeNodeCollection)
		For Each node As TreeNode In nodes
			node.BackColor = Color.White
			ClearNodeBackColor(node.Nodes)
		Next
	End Sub

End Class