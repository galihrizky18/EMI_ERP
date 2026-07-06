Imports System.Reflection

Public Class FMenu

	'Private Property UserID As String
	'Private Property MainMenuID As String
	Private Sub FMenu_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub FMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Try
			OpenConn()

			Using Dr = OpenTrans("select dateadd(hh, " & selisihjam & ", getdate()) as Jam")
				If Dr.Read Then
					ToolStripStatusLabel3.Text = Format(Dr("jam"), "yyyy-MM-dd HH:mm:ss")
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		ToolStripStatusLabel1.Text = "Login : " & UserID
		ToolStripStatusLabel4.Text = "Lokasi : " & Lokasi

		Timer1_Tick(Me, Nothing)

		Dim C As Control

		For Each C In Me.Controls
			If TypeOf C Is MdiClient Then
				C.BackColor = Color.White
				Exit For
			End If
		Next

		C = Nothing

		LoadMenuStrip()
	End Sub

	'Public  Sub New(ByVal UserID As String, ByVal MainMenuID As String)

	'    '' This call is required by the designer.
	'    'InitializeComponent()

	'    '' Add any initialization after the InitializeComponent() call.
	'    'Me.UserID = UserID
	'    'Me.MainMenuID = MainMenuID

	'End Sub

	Private Sub LoadMenuStrip()

		Dim menuStrip As New MenuStrip()
		Dim DataMenu As New DataTable

		Try
			OpenConn()
			Dim Sql As String

			Sql = "SELECT Menus.MenuName, RoleMenus.MenuID FROM RoleMenus INNER JOIN Menus ON RoleMenus.MenuID=Menus.MenuID "
			Sql = Sql & "WHERE RoleMenus.UserID='" & UserID & "' AND Menus.MainMenuID='" & MainMenuID & "' ORDER BY MenuOrder"

			Using dr = OpenTrans(Sql)
				If dr.HasRows Then
					DataMenu.Load(dr)
				End If

			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)

		End Try

		If DataMenu.Rows.Count > 0 Then
			For Each data As DataRow In DataMenu.Rows
				'Main Menu
				Dim menuText As String = If(data("MenuName") IsNot DBNull.Value, data("MenuName").ToString(), "Default Text")
				Dim mainMenu As New ToolStripMenuItem(menuText)

				'Try Get SubMenu By MenuIDz
				Dim DataSubMenu As New DataTable
				Try
					OpenConn()
					Dim Sql As String

					Sql = "SELECT MAX(SubMenuLv1.SubMenuID) AS SubMenuIDLv1, "
					Sql = Sql & "MAX(RoleSubMenuLv1.SubMenuLv1ID) AS Lv1IDFromRole, "
					Sql = Sql & "SubMenus.SubMenuName AS SubMenuName, "
					Sql = Sql & "MAX(SubMenus.MenuID) AS MenuIDFromSubMenus, "
					Sql = Sql & "MAX(SubMenus.SubMenuID) AS SubMenuIDFromSubMenus, "
					Sql = Sql & "MAX(SubMenus.Form) AS Form, "
					Sql = Sql & "MAX(SubMenus.Variabel) AS Variabel, MAX(SubMenus.Isi_Variabel) AS Isi_Variabel, "
					Sql = Sql & "MAX(SubMenus.Variabel2) AS Variabel2, MAX(SubMenus.Isi_Variabel2) AS Isi_Variabel2, "
					Sql = Sql & "MAX(SubMenus.Variabel3) AS Variabel3, MAX(SubMenus.Isi_Variabel3) AS Isi_Variabel3 "
					Sql = Sql & "FROM RoleSubMenu "
					Sql = Sql & "INNER JOIN SubMenus ON RoleSubMenu.SubMenuID = SubMenus.SubMenuID "
					Sql = Sql & "LEFT JOIN SubMenuLv1 ON SubMenus.SubMenuID = SubMenuLv1.SubMenuID "
					Sql = Sql & "LEFT JOIN RoleSubMenuLv1 on RoleSubMenuLv1.SubMenuLv1ID= SubMenuLv1.SubMenuLv1ID "
					Sql = Sql & "WHERE RoleSubMenu.UserID = '" & UserID & "' "
					Sql = Sql & "AND SubMenus.MenuID = '" & data("MenuID") & "' "
					Sql = Sql & "GROUP BY SubMenus.SubMenuName "
					Sql = Sql & "ORDER BY MAX(SubMenus.SubMenuOrder);"

					Using dr = OpenTrans(Sql)
						If dr.HasRows Then
							DataSubMenu.Load(dr)
						End If

					End Using

					CloseConn()
				Catch ex As Exception
					CloseConn()
					MessageBox.Show(ex.Message)

				End Try

				'Add Sub Menu
				If DataSubMenu.Rows.Count > 0 Then
					For Each dataSub As DataRow In DataSubMenu.Rows

						If data("MenuID") = dataSub("MenuIDFromSubMenus") Then
							Dim SubMenuText As String = If(dataSub("SubMenuName") IsNot DBNull.Value, dataSub("SubMenuName").ToString(), "Default Text")

							Dim SubMenu As New ToolStripMenuItem(SubMenuText)

							If dataSub("SubMenuIDLv1") Is DBNull.Value Then
								If Not dataSub("Form") Is DBNull.Value Then
									'add handler
									Dim formName As String = dataSub("form") ' TIdak ada data dataSub("form")
									If dataSub("Variabel") IsNot DBNull.Value Or dataSub("Isi_Variabel") IsNot DBNull.Value Then

										If dataSub("Variabel2") IsNot DBNull.Value Or dataSub("Isi_Variabel2") IsNot DBNull.Value Then

											If dataSub("Variabel3") IsNot DBNull.Value Or dataSub("Isi_Variabel3") IsNot DBNull.Value Then

												AddHandler SubMenu.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), dataSub("Variabel"), dataSub("Isi_Variabel"), dataSub("Variabel2"), dataSub("Isi_Variabel2"), dataSub("Variabel3"), dataSub("Isi_Variabel3"))
											End If

											AddHandler SubMenu.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), dataSub("Variabel"), dataSub("Isi_Variabel"), dataSub("Variabel2"), dataSub("Isi_Variabel2"))

										End If

										AddHandler SubMenu.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), dataSub("Variabel"), dataSub("Isi_Variabel"))
									Else
										AddHandler SubMenu.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName))
									End If

									'ElseIf dataSub("Lv1IDFromRole") Is DBNull.Value Then
									'    AddHandler SubMenu.Click, Sub(sender, e) MessageBox.Show("Nothing To Display")

								End If

							End If

							'add SubMenuLv1
							'Try Get SubMenu By MenuID
							Dim DataSubMenuLv1 As New DataTable
							Try
								OpenConn()
								Dim Sql As String

								'Sql = " SELECT SubMenuLv1Name, SubMenuID, SubMenuLv1ID Form FROM SubMenuLv1 WHERE "
								'Sql = Sql & "SubMenuID='" & dataSub("SubMenuID") & "'ORDER BY SubMenuLv1Order"

								Sql = "SELECT MAX(SubMenuLv2.SubMenuLv1ID) AS SubMenuLv1IDFromSubMenuLv2, "
								Sql = Sql & "MAX(RoleSubMenuLv2.SubMenuLv2ID) AS Lv2IDFromRole, "
								Sql = Sql & "SubMenuLv1.SubMenuLv1Name AS SubMenuLv1Name, "
								Sql = Sql & "MAX(SubMenuLv1.SubMenuID) AS SubMenuIDFromSubMenuLv1, "
								Sql = Sql & "MAX(SubMenuLv1.SubMenuLv1ID) AS SubMenuLv1IDFromSubMenuLv1, "
								Sql = Sql & "MAX(SubMenuLv1.Form) AS Form, "
								Sql = Sql & "MAX(SubMenuLv1.Variabel) AS Variabel, MAX(SubMenuLv1.Isi_Variabel) AS Isi_Variabel, "
								Sql = Sql & "MAX(SubMenuLv1.Variabel2) AS Variabel2, MAX(SubMenuLv1.Isi_Variabel2) AS Isi_Variabel2, "
								Sql = Sql & "MAX(SubMenuLv1.Variabel3) AS Variabel3, MAX(SubMenuLv1.Isi_Variabel3) AS Isi_Variabel3 "
								Sql = Sql & "FROM RoleSubMenuLv1 "
								Sql = Sql & "INNER JOIN SubMenuLv1 ON RoleSubMenuLv1.SubMenuLv1ID = SubMenuLv1.SubMenuLv1ID "
								Sql = Sql & "LEFT JOIN SubMenuLv2 ON SubMenuLv1.SubMenuLv1ID = SubMenuLv2.SubMenuLv1ID "
								Sql = Sql & "LEFT JOIN RoleSubMenuLv2 on RoleSubMenuLv2.SubMenuLv2ID=SubMenuLv2.SubMenuLv2ID "
								Sql = Sql & "WHERE RoleSubMenuLv1.UserID = '" & UserID & "' "
								Sql = Sql & "AND SubMenuLv1.SubMenuID = '" & dataSub("SubMenuIDFromSubMenus") & "' "
								Sql = Sql & "GROUP BY SubMenuLv1.SubMenuLv1Name "
								Sql = Sql & "ORDER BY MAX(SubMenuLv1.SubMenuLv1Order);"

								Using dr = OpenTrans(Sql)
									If dr.HasRows Then
										DataSubMenuLv1.Load(dr)
									End If

								End Using

								CloseConn()
							Catch ex As Exception
								CloseConn()
								MessageBox.Show(ex.Message)

							End Try

							If DataSubMenuLv1.Rows.Count > 0 Then
								For Each DataSubLv1 As DataRow In DataSubMenuLv1.Rows
									If dataSub("SubMenuIDFromSubMenus") = DataSubLv1("SubMenuIDFromSubMenuLv1") Then
										Dim SubMenuLv1Text As String = If(DataSubLv1("SubMenuLv1Name") IsNot DBNull.Value, DataSubLv1("SubMenuLv1Name").ToString(), "Default Text")

										Dim SubMenuLv1 As New ToolStripMenuItem(SubMenuLv1Text)

										If DataSubLv1("SubMenuLv1IDFromSubMenuLv2") Is DBNull.Value Then
											If Not DataSubLv1("Form") Is DBNull.Value Then
												'add handlerb
												Dim formName As String = DataSubLv1("Form") ' TIdak ada data dataSub("form")

												If DataSubLv1("Variabel") IsNot DBNull.Value Or DataSubLv1("Isi_Variabel") IsNot DBNull.Value Then
													If DataSubLv1("Variabel2") IsNot DBNull.Value Or DataSubLv1("Isi_Variabel2") IsNot DBNull.Value Then

														If DataSubLv1("Variabel3") IsNot DBNull.Value Or DataSubLv1("Isi_Variabel3") IsNot DBNull.Value Then

															AddHandler SubMenuLv1.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), DataSubLv1("Variabel"), DataSubLv1("Isi_Variabel"), DataSubLv1("Variabel2"), DataSubLv1("Isi_Variabel2"), DataSubLv1("Variabel3"), DataSubLv1("Isi_Variabel3"))
														End If

														AddHandler SubMenuLv1.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), DataSubLv1("Variabel"), DataSubLv1("Isi_Variabel"), DataSubLv1("Variabel2"), DataSubLv1("Isi_Variabel2"))

													End If

													AddHandler SubMenuLv1.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), DataSubLv1("Variabel"), DataSubLv1("Isi_Variabel"))
												Else

													AddHandler SubMenuLv1.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName))
													'AddHandler SubMenuLv1.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName))
												End If
												'ElseIf DataSubLv1("Lv2IDFromRole") Is DBNull.Value Then
												'    AddHandler SubMenu.Click, Sub(sender, e) MessageBox.Show("Nothing To Display")
											End If

										End If

										'add SubMenuLv2
										'Try Get SubMenu By MenuID
										Dim DataSubMenuLv2 As New DataTable
										Try
											OpenConn()
											Dim Sql As String

											Sql = "SELECT MAX(SubMenuLv3.SubMenuLv2ID) AS SubMenuLv2IDFromSubLv3, "
											Sql = Sql & "MAX(RoleSubMenuLv3.SubMenuLv3ID) AS Lv3IDFromRole, "
											Sql = Sql & "SubMenuLv2.SubMenuLv2Name AS SubMenuLv2Name, "
											Sql = Sql & "MAX(SubMenuLv2.SubMenuLv1ID) AS SubMenuLv1IDFromSubMenuLv2, "
											Sql = Sql & "MAX(SubMenuLv2.SubMenuLv2ID) AS SubMenuLv2IDFromSubMenuLv2, "
											Sql = Sql & "MAX(SubMenuLv2.Form) AS Form, "
											Sql = Sql & "MAX(SubMenuLv2.Variabel) AS Variabel, MAX(SubMenuLv2.Isi_Variabel) AS Isi_Variabel, "
											Sql = Sql & "MAX(SubMenuLv2.Variabel2) AS Variabel2, MAX(SubMenuLv2.Isi_Variabel2) AS Isi_Variabel2, "
											Sql = Sql & "MAX(SubMenuLv2.Variabel3) AS Variabel3, MAX(SubMenuLv2.Isi_Variabel3) AS Isi_Variabel3 "
											Sql = Sql & "FROM RoleSubMenuLv2 "
											Sql = Sql & "INNER JOIN SubMenuLv2 ON RoleSubMenuLv2.SubMenuLv2ID = SubMenuLv2.SubMenuLv2ID "
											Sql = Sql & "LEFT JOIN SubMenuLv3 ON SubMenuLv2.SubMenuLv2ID = SubMenuLv3.SubMenuLv2ID "
											Sql = Sql & "LEFT JOIN RoleSubMenuLv3 on RoleSubMenuLv3.SubMenuLv3ID=SubMenuLv3.SubMenuLv3ID "
											Sql = Sql & "WHERE RoleSubMenuLv2.UserID = '" & UserID & "' "
											Sql = Sql & "AND SubMenuLv2.SubMenuLv1ID = '" & DataSubLv1("SubMenuLv1IDFromSubMenuLv1") & "' "
											Sql = Sql & "GROUP BY SubMenuLv2.SubMenuLv2Name "
											Sql = Sql & "ORDER BY MAX(SubMenuLv2.SubMenuLv2Order);"

											Using dr = OpenTrans(Sql)
												If dr.HasRows Then
													DataSubMenuLv2.Load(dr)
												End If

											End Using

											CloseConn()
										Catch ex As Exception
											CloseConn()
											MessageBox.Show(ex.Message)

										End Try

										If DataSubMenuLv2.Rows.Count > 0 Then
											For Each DataSubLv2 As DataRow In DataSubMenuLv2.Rows
												If DataSubLv1("SubMenuLv1IDFromSubMenuLv1") = DataSubLv2("SubMenuLv1IDFromSubMenuLv2") Then
													Dim SubMenuLv2Text As String = If(DataSubLv2("SubMenuLv2Name") IsNot DBNull.Value, DataSubLv2("SubMenuLv2Name").ToString(), "Default Text")

													Dim SubMenuLv2 As New ToolStripMenuItem(SubMenuLv2Text)

													'Jika ada Level Dibawahnya
													If DataSubLv2("SubMenuLv2IDFromSubLv3") Is DBNull.Value Then
														If Not DataSubLv2("Form") Is DBNull.Value Then
															'add handler
															Dim formName As String = DataSubLv2("Form") ' TIdak ada data dataSub("form")

															If DataSubLv2("Variabel") IsNot DBNull.Value Or DataSubLv2("Isi_Variabel") IsNot DBNull.Value Then

																If DataSubLv2("Variabel2") IsNot DBNull.Value Or DataSubLv2("Isi_Variabel2") IsNot DBNull.Value Then

																	If DataSubLv2("Variabel3") IsNot DBNull.Value Or DataSubLv2("Isi_Variabel3") IsNot DBNull.Value Then

																		AddHandler SubMenuLv2.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), DataSubLv2("Variabel"), DataSubLv2("Isi_Variabel"), DataSubLv2("Variabel2"), DataSubLv2("Isi_Variabel2"), DataSubLv2("Variabel3"), DataSubLv2("Isi_Variabel3"))
																	End If

																	AddHandler SubMenuLv2.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), DataSubLv2("Variabel"), DataSubLv2("Isi_Variabel"), DataSubLv2("Variabel2"), DataSubLv2("Isi_Variabel2"))

																End If

																AddHandler SubMenuLv2.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), DataSubLv2("Variabel"), DataSubLv2("Isi_Variabel"))
															Else
																AddHandler SubMenuLv2.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName))
															End If

															'ElseIf DataSubLv2("Lv3IDFromRole") Is DBNull.Value Then
															'    AddHandler SubMenu.Click, Sub(sender, e) MessageBox.Show("Nothing To Display")
														End If

													End If

													'add SubMenuLv3
													'Try Get SubMenu By MenuID
													Dim DataSubMenuLv3 As New DataTable
													Try
														OpenConn()
														Dim Sql As String

														'Sql = "SELECT MAX(submenulv2.SubMenuLv2ID) AS SubMenuLv2ID,
														Sql = "SELECT SubMenuLv3.SubMenuLv3Name AS SubMenuLv3Name, "
														Sql = Sql & "MAX(SubMenuLv3.SubMenuLv2ID) AS SubMenuLv2IDFromSubMenuLv3, "
														Sql = Sql & "MAX(SubMenuLv3.Form) AS Form, "
														Sql = Sql & "MAX(SubMenuLv3.Variabel) AS Variabel, MAX(SubMenuLv3.Isi_Variabel) AS Isi_Variabel, "
														Sql = Sql & "MAX(SubMenuLv3.Variabel2) AS Variabel2, MAX(SubMenuLv3.Isi_Variabel2) AS Isi_Variabel2, "
														Sql = Sql & "MAX(SubMenuLv3.Variabel3) AS Variabel3, MAX(SubMenuLv3.Isi_Variabel3) AS Isi_Variabel3 "
														Sql = Sql & "FROM RoleSubMenuLv3 "
														Sql = Sql & "INNER JOIN SubMenuLv3 ON RoleSubMenuLv3.SubMenuLv3ID = SubMenuLv3.SubMenuLv3ID "
														Sql = Sql & "WHERE RoleSubMenuLv3.UserID = '" & UserID & "' "
														Sql = Sql & "AND SubMenuLv3.SubMenuLv2ID = '" & DataSubLv2("SubMenuLv2IDFromSubMenuLv2") & "' "
														Sql = Sql & "GROUP BY SubMenuLv3.SubMenuLv3Name "
														Sql = Sql & "ORDER BY MAX(SubMenuLv3.SubMenuLv3Order);"

														Using dr = OpenTrans(Sql)
															If dr.HasRows Then
																DataSubMenuLv3.Load(dr)
															End If

														End Using

														CloseConn()
													Catch ex As Exception
														CloseConn()
														MessageBox.Show(ex.Message)

													End Try

													If DataSubMenuLv3.Rows.Count > 0 Then
														For Each DataSubLv3 As DataRow In DataSubMenuLv3.Rows
															If DataSubLv2("SubMenuLv2IDFromSubMenuLv2") = DataSubLv3("SubMenuLv2IDFromSubMenuLv3") Then

																Dim SubMenuLv3Text As String = If(DataSubLv3("SubMenuLv3Name") IsNot DBNull.Value, DataSubLv3("SubMenuLv3Name").ToString(), "Default Text")

																Dim SubMenuLv3 As New ToolStripMenuItem(SubMenuLv3Text)

																'Jika ada Level Dibawahnya
																'If DataSubLv3("SubMenuLv2IDFromSubMenuLv3") Is DBNull.Value Then
																'    'add handler
																'    Dim formName As String = DataSubLv2("Form") ' TIdak ada data dataSub("form")
																'    AddHandler SubMenuLv3.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName))

																'End If

																'TAMBAHKAN TRY DAN LOOPING LEVEL SELANJUTANYA DISINI

																If Not DataSubLv3("Form") Is DBNull.Value Then
																	'add handler
																	Dim formName As String = DataSubLv3("Form") ' TIdak ada data dataSub("form")

																	If DataSubLv3("Variabel") IsNot DBNull.Value Or DataSubLv3("Isi_Variabel") IsNot DBNull.Value Then

																		If DataSubLv3("Variabel2") IsNot DBNull.Value Or DataSubLv3("Isi_Variabel2") IsNot DBNull.Value Then

																			If DataSubLv3("Variabel3") IsNot DBNull.Value Or DataSubLv3("Isi_Variabel3") IsNot DBNull.Value Then

																				AddHandler SubMenuLv2.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), DataSubLv3("Variabel"), DataSubLv3("Isi_Variabel"), DataSubLv3("Variabel2"), DataSubLv3("Isi_Variabel2"), DataSubLv3("Variabel3"), DataSubLv3("Isi_Variabel3"))
																			End If

																			AddHandler SubMenuLv3.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), DataSubLv3("Variabel"), DataSubLv3("Isi_Variabel"), DataSubLv3("Variabel2"), DataSubLv3("Isi_Variabel2"))

																		End If

																		AddHandler SubMenuLv3.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName), DataSubLv3("Variabel"), DataSubLv3("Isi_Variabel"))
																	Else
																		AddHandler SubMenuLv3.Click, Sub(sender, e) HandlerClickMenu(convertStringToForm(formName))
																	End If

																End If

																'add to Sub Menu Lv 2
																SubMenuLv2.DropDownItems.Add(SubMenuLv3)
															End If
														Next
													End If

													'add to Sub Menu Lv 1
													SubMenuLv1.DropDownItems.Add(SubMenuLv2)
												End If
											Next
										End If

										'add to Sub Menu
										SubMenu.DropDownItems.Add(SubMenuLv1)
									End If
								Next
							End If

							'add to Main Menu
							mainMenu.DropDownItems.Add(SubMenu)
						End If
					Next
				End If

				menuStrip.Items.Add(mainMenu)
				'DataSubMenu.Clear()
			Next

		End If

		Me.Controls.Add(menuStrip)
	End Sub

	Private Sub HandlerClickMenu(ByVal formToOpen As Form, Optional ByVal V1 As String = "", Optional ByVal IV1 As String = "", Optional ByVal V2 As String = "", Optional ByVal IV2 As String = "", Optional ByVal V3 As String = "", Optional ByVal IV3 As String = "")

		If formToOpen Is Nothing Then
			Exit Sub
		End If

		Dim isFormOpen As Boolean = False

		'For Each frm As Form In Me.MdiChildren
		For Each frm As Form In Application.OpenForms
			If frm.GetType() Is formToOpen.GetType() Then
				frm.Focus()
				isFormOpen = True
				Exit For
			End If
		Next

		If Not isFormOpen Then

			'Dim newForm As Form = CType(Activator.CreateInstance(formToOpen.GetType), Form)

			formToOpen.MdiParent = Me

			If Not String.IsNullOrWhiteSpace(V1) AndAlso Not String.IsNullOrWhiteSpace(IV1) Then
				Dim propertyInfo = formToOpen.GetType().GetProperty(V1)
				If propertyInfo IsNot Nothing AndAlso propertyInfo.CanWrite Then
					propertyInfo.SetValue(formToOpen, IV1)
				End If
			End If

			If Not String.IsNullOrWhiteSpace(V2) AndAlso Not String.IsNullOrWhiteSpace(IV2) Then
				Dim propertyInfo = formToOpen.GetType().GetProperty(V2)
				If propertyInfo IsNot Nothing AndAlso propertyInfo.CanWrite Then
					propertyInfo.SetValue(formToOpen, IV2)
				End If
			End If

			If Not String.IsNullOrWhiteSpace(V3) AndAlso Not String.IsNullOrWhiteSpace(IV3) Then
				Dim propertyInfo = formToOpen.GetType().GetProperty(V3)
				If propertyInfo IsNot Nothing AndAlso propertyInfo.CanWrite Then
					propertyInfo.SetValue(formToOpen, IV3)
				End If
			End If

			formToOpen.StartPosition = FormStartPosition.CenterScreen
			formToOpen.Show()
			formToOpen.Focus()
		End If
	End Sub

	'Private Function convertStringToForm(formName As String) As Form

	'    If String.IsNullOrWhiteSpace(formName) Then
	'        Exit Function
	'    End If

	'    'menggunakan Refleksi
	'    Try
	'        Dim existingForm As Form = Nothing

	'        'Mencari formtype dengan nama sesuai parameter / mencari apakah form ada atau tidak
	'        Dim formType As Type = Type.GetType(Me.GetType().Namespace & "." & formName)

	'        If formType IsNot Nothing Then

	'            ' Loop untuk mencari form yang sudah terbuka
	'            For Each form As Form In Application.OpenForms
	'                If form.GetType() Is formType Then
	'                    existingForm = form
	'                    Exit For
	'                End If
	'            Next

	'            ' Jika form tidak ditemukan, buat instance baru dengan konstruktor yang memiliki parameter
	'            If existingForm Is Nothing Then
	'                Dim newForm As Form = CType(Activator.CreateInstance(formType), Form) 'convert object baru ke form
	'                Return newForm
	'            Else
	'                ' Jika form sudah terbuka
	'                Return existingForm
	'            End If

	'        Else
	'            MessageBox.Show("Form dengan nama " & formName & " tidak ditemukan.")
	'            Exit Function
	'        End If
	'    Catch ex As Exception
	'        MessageBox.Show("Terjadi kesalahan: " & ex.Message)
	'    End Try
	'End Function

	Private Sub F_Menu_Closed(sender As Object, e As EventArgs) Handles Me.Closed
		Me.Dispose()
		Main_Menu.Show()
		Main_Menu.Focus()
	End Sub

	Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
		ToolStripStatusLabel3.Text = Format(DateAdd(DateInterval.Second, 1, CDate(ToolStripStatusLabel3.Text)), "yyyy-MM-dd HH:mm:ss")
	End Sub

	Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
		Try
			OpenConn()

			Using Dr = OpenTrans("select dateadd(hh, " & selisihjam & ", getdate()) as Jam")
				If Dr.Read Then
					'ToolStripStatusLabel3.Text = Format(Dr("jam"), "dd MMM yyyy HH:mm:ss")
					ToolStripStatusLabel3.Text = Format(Dr("jam"), "yyyy-MM-dd HH:mm:ss")
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Function convertStringToForm(ByVal formToOpen As String) As Form

		Try
			' Mengakses default instance form melalui My.Forms menggunakan Reflection
			Dim formProperty As System.Reflection.PropertyInfo = My.Forms.GetType().GetProperty(formToOpen)

			If formProperty IsNot Nothing Then
				Return DirectCast(formProperty.GetValue(My.Forms, Nothing), Form)
			Else
				Return Nothing
			End If
		Catch ex As Exception
			Return Nothing
		End Try

		'If Forms.ContainsKey(formToOpen) Then
		'    Return Forms(formToOpen)
		'Else
		'    'MessageBox.Show("Form tidak ditemukan: " & formToOpen, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		'    Return Nothing
		'End If

#Region "Kode Benar"

		'Select Case formToOpen

		'	Case "Purchase_Requisition"
		'		Return Purchase_Requisition

		'	Case "EMI_Display_Log_ForecastOrder"
		'		Return EMI_Display_Log_ForecastOrder
		'	Case "EMI_Pengeluaran_Stock"
		'		Return EMI_Pengeluaran_Stock

		'	Case "N_EMI_Display_Barcode_Merge"
		'		Return N_EMI_Display_Barcode_Merge

		'	'Case "Acc_Bongkar"
		'	'    Return Acc_Bongkar

		'	'Case "Acc_Ke_PO"
		'	'    Return Acc_Ke_PO

		'	'Case "Account_Master_Kategori_Biaya_Import"
		'	'    Return Account_Master_Kategori_Biaya_Import

		'	'Case "Barang_Masuk_New"
		'	'    Return Barang_Masuk_New

		'	'Case "Biaya_Import_Per_PO"
		'	'    Return Biaya_Import_Per_PO

		'	'Case "Biaya_Import_Per_PO_Validasi"
		'	'    Return Biaya_Import_Per_PO_Validasi

		'	Case "Display_Barang"
		'		Return Display_Barang

		'	'Case "Display_Data_Penawaran"
		'	'    Return Display_Data_Penawaran

		'	'Case "Display_HPP_Barang_Masuk"
		'	'    Return Display_HPP_Barang_Masuk

		'	'Case "Display_Inquiry"
		'	'    Return Display_Inquiry

		'	Case "Display_Kurs"
		'		Return Display_Kurs

		'	'Case "Display_selisih_Barang_Masuk"
		'	'    Return Display_selisih_Barang_Masuk

		'	Case "Display_Transaksi_MaterialRequsition"
		'		Return Display_Transaksi_MaterialRequsition

		'	'Case "Display_Validasi_Detail_Biaya_Import"
		'	'    Return Display_Validasi_Detail_Biaya_Import

		'	'Case "Display_Validasi_Selisih_Barang_Masuk"
		'	'    Return Display_Validasi_Selisih_Barang_Masuk

		'	'Case "DO_Reseller_New"
		'	'    Return DO_Reseller_New

		'	'Case "EMI_Barang_Masuk_Display_data"
		'	'    Return EMI_Barang_Masuk_Display_data

		'	'Case "EMI_Barang_Masuk_Validasi"
		'	'    Return EMI_Barang_Masuk_Validasi

		'	'Case "EMI_Display_Log_MaterialRequisition"
		'	'    Return EMI_Display_Log_MaterialRequisition

		'	Case "EMI_Display_Timbang"
		'		Return EMI_Display_Timbang

		'	Case "N_EMI_Transaksi_Request_Material_QC_Validasi"
		'		Return N_EMI_Transaksi_Request_Material_QC_Validasi

		'	Case "EMI_Pembelian_PO_Summary_Data"
		'		Return EMI_Pembelian_PO_Summary_Data

		'	Case "Emi_Display_Transfer_Stock"
		'		Return Emi_Display_Transfer_Stock

		'	Case "Global_Setting"
		'		Return Global_Setting

		'	Case "EMI_PO_Pembelian_Display"
		'		Return EMI_PO_Pembelian_Display

		'	Case "EMI_PO_Pembelian_Display2"
		'		Return EMI_PO_Pembelian_Display2

		'	Case "EMI_Production_Order"
		'		Return EMI_Production_Order

		'	Case "EMI_Production_Order_Summary_Data"
		'		Return EMI_Production_Order_Summary_Data

		'	Case "N_EMI_Transaksi_Request_Material_QC_Validasi"
		'		Return N_EMI_Transaksi_Request_Material_QC_Validasi

		'	'Case "EMI_Schedule"
		'	'    Return EMI_Schedule

		'	Case "EMI_Selisih_Barang_Masuk_Validasi"
		'		Return EMI_Selisih_Barang_Masuk_Validasi

		'	Case "EMI_Technical_Complete"
		'		Return EMI_Technical_Complete

		'	Case "EMI_Transaksi_ForecastOrder"
		'		Return EMI_Transaksi_ForecastOrder

		'	'Case "EMI_Transaksi_MaterialRequsition"
		'	'    Return EMI_Transaksi_MaterialRequsition

		'	'Case "Form_Input_Barang_Per_Kontainer"
		'	'    Return Form_Input_Barang_Per_Kontainer

		'	Case "Hitung_HPP_Import"
		'		Return Hitung_HPP_Import

		'	'Case "Hitung_Total_billing"
		'	'    Return Hitung_Total_billing

		'	'Case "HPP_Simulasi_Display"
		'	'    Return HPP_Simulasi_Display

		'	'Case "HPP_Simulasi_Pilih_Barang"
		'	'    Return HPP_Simulasi_Pilih_Barang

		'	'Case "HR_Validasi_Biaya_DO"
		'	'    Return HR_Validasi_Biaya_DO

		'	'Case "Jf_BarangPerKontainer"
		'	'    Return Jf_BarangPerKontainer

		'	'Case "Jf_Display_Rencana_Order1"
		'	'    Return Jf_Display_Rencana_Order1

		'	'Case "Jf_Insentif_Kasbon_Kerajinan"
		'	'    Return Jf_Insentif_Kasbon_Kerajinan

		'	'Case "Jf_Master_Asuransi"
		'	'    Return Jf_Master_Asuransi

		'	'Case "Jf_Master_Attendance"
		'	'    Return Jf_Master_Attendance

		'	'Case "Jf_Master_Attendance_Laporan"
		'	'    Return Jf_Master_Attendance_Laporan

		'	'Case "Jf_Master_Attendance_Update"
		'	'    Return Jf_Master_Attendance_Update

		'	'Case "Jf_Master_Divisi"
		'	'    Return Jf_Master_Divisi

		'	'Case "Jf_Master_Golongan_Level_Jabatan"
		'	'    Return Jf_Master_Golongan_Level_Jabatan

		'	'Case "Jf_Master_Grouping_Penggajian"
		'	'    Return Jf_Master_Grouping_Penggajian

		'	'Case "Jf_Master_Karyawan2"
		'	'    Return Jf_Master_Karyawan2

		'	Case "Jf_Master_Karyawan2_Display"
		'		Return Jf_Master_Karyawan2_Display

		'	'Case "Jf_Master_Komponen_Gaji"
		'	'    Return Jf_Master_Komponen_Gaji

		'	'Case "Jf_Master_Uang_Makan"
		'	'    Return Jf_Master_Uang_Makan

		'	'Case "Jf_Pengecekan_Absen"
		'	'    Return Jf_Pengecekan_Absen

		'	'Case "Jf_Penggajian"
		'	'    Return Jf_Penggajian

		'	'Case "Jf_PO_Pembelian_import"
		'	'    Return Jf_PO_Pembelian_import

		'	'Case "Jf_Simulasi_Gaji"
		'	'    Return Jf_Simulasi_Gaji

		'	'Case "Jf_Transaksi_Approval_Dialog"
		'	'    Return Jf_Transaksi_Approval_Dialog

		'	'Case "Jf_Transaksi_Approval_Final"
		'	'    Return Jf_Transaksi_Approval_Final

		'	'Case "Jf_Transaksi_Asuransi"
		'	'    Return Jf_Transaksi_Asuransi

		'	'Case "Jf_Transaksi_Divisi"
		'	'    Return Jf_Transaksi_Divisi

		'	'Case "Jf_Transaksi_Gaji"
		'	'    Return Jf_Transaksi_Gaji

		'	'Case "Jf_Transaksi_Jabatan"
		'	'    Return Jf_Transaksi_Jabatan

		'	'Case "Jf_Transaksi_Roles_Approval"
		'	'    Return Jf_Transaksi_Roles_Approval

		'	'Case "Jf_Transaksi_Status"
		'	'    Return Jf_Transaksi_Status

		'	Case "Kategori_Biaya_Import"
		'		Return Kategori_Biaya_Import

		'	'Case "Kategori_Perusahaan_Biaya_Import"
		'	'    Return Kategori_Perusahaan_Biaya_Import

		'	Case "Loading_Barang_Import"
		'		Return Loading_Barang_Import

		'	'Case "Lokasi_Tujuan_Per_Container"
		'	'    Return Lokasi_Tujuan_Per_Container

		'	'Case "Master_Bahan"
		'	'    Return Master_Bahan

		'	'Case "Master_Barang_Import"
		'	'    Return Master_Barang_Import

		'	Case "Master_Barang_New"
		'		Return Master_Barang_New

		'	Case "Master_Barang_Susunan"
		'		Return Master_Barang_Susunan

		'	'Case "Master_Biaya"
		'	'    Return Master_Biaya

		'	'Case "Master_Biaya_Import"
		'	'    Return Master_Biaya_Import

		'	'Case "Master_Bundling_Promo"
		'	'    Return Master_Bundling_Promo

		'	'Case "Master_Customer"
		'	'    Return Master_Customer

		'	'Case "Master_Detail_Biaya_Import"
		'	'    Return Master_Detail_Biaya_Import

		'	'Case "Master_Detail_Biaya_Import2"
		'	'    Return Master_Detail_Biaya_Import2

		'	'Case "Master_Detail_Biaya_Import3"
		'	'    Return Master_Detail_Biaya_Import3

		'	'Case "Master_Ekspedisi"
		'	'    Return Master_Ekspedisi

		'	'Case "Master_Freight_Suppliers"
		'	'    Return Master_Freight_Suppliers

		'	'Case "Master_Gudang"
		'	'    Return Master_Gudang

		'	'Case "Master_Hewan"
		'	'    Return Master_Hewan

		'	'Case "Master_HS_Code"
		'	'    Return Master_HS_Code

		'	'Case "Master_Kategori_Harga_Detail"
		'	'    Return Master_Kategori_Harga_Detail

		'	'Case "Master_Kategori_Harga"
		'	'    Return Master_Kategori_Harga

		'	'Case "Master_Karyawan"
		'	'    Return Master_Karyawan

		'	'Case "Master_Kategori_Biaya_Import_New"
		'	'    Return Master_Kategori_Biaya_Import_New

		'	'Case "Master_Gudang_Kategori"
		'	'    Return Master_Gudang_Kategori

		'	'Case "Master_Kategori_PO"
		'	'    Return Master_Kategori_PO

		'	'Case "Master_Quality_Control_Kategori"
		'	'    Return Master_Quality_Control_Kategori

		'	'Case "Master_Kemasan"
		'	'    Return Master_Kemasan

		'	Case "Master_Klasifikasi_Bahan"
		'		Return Master_Klasifikasi_Bahan

		'	'Case "Master_Komposisi_Barang_Jadi"
		'	'    Return Master_Komposisi_Barang_Jadi

		'	'Case "Master_Mata_Uang"
		'	'    Return Master_Mata_Uang

		'	'Case "Master_Media_Kirim"
		'	'    Return Master_Media_Kirim

		'	Case "Master_Mesin"
		'		Return Master_Mesin

		'	'Case "Master_Pelabuhan"
		'	'    Return Master_Pelabuhan

		'	'Case "Master_Pelabuhan_Supplier"
		'	'    Return Master_Pelabuhan_Supplier

		'	'Case "Master_Pelayaran"
		'	'    Return Master_Pelayaran

		'	Case "Transaksi_Penawaran"
		'		Return Transaksi_Penawaran

		'	'Case "Master_Persentase_Rencana_Order"
		'	'    Return Master_Persentase_Rencana_Order

		'	'Case "Master_Perusahaan_Biaya_Import"
		'	'    Return Master_Perusahaan_Biaya_Import

		'	'Case "Master_Produk"
		'	'    Return Master_Produk

		'	Case "Master_Quality_Control"
		'		Return Master_Quality_Control

		'	Case "Master_Quality_Control_Barang"
		'		Return Master_Quality_Control_Barang

		'	Case "Master_Quality_Control_Kategori_Detail"
		'		Return Master_Quality_Control_Kategori_Detail

		'	'Case "Master_Quality_Control_Kendaraan"
		'	'    Return Master_Quality_Control_Kendaraan

		'	'Case "Master_Rekening_Perusahaan_Biaya_Import"
		'	'    Return Master_Rekening_Perusahaan_Biaya_Import

		'	'Case "Master_Rekening_Suppliers"
		'	'    Return Master_Rekening_Suppliers

		'	'Case "Master_Kategori_PO_Role"
		'	'    Return Master_Kategori_PO_Role

		'	Case "Master_Routing"
		'		Return Master_Routing

		'	'Case "Master_Satuan"
		'	'    Return Master_Satuan

		'	'Case "Master_Satuan_Perhitungan"
		'	'    Return Master_Satuan_Perhitungan

		'	'Case "Master_Shipper"
		'	'    Return Master_Shipper

		'	'Case "Master_Storage"
		'	'    Return Master_Storage

		'	Case "Master_Suppliers"
		'		Return Master_Suppliers

		'	Case "Master_Work_Center"
		'		Return Master_Work_Center

		'	Case "Master_Cost_Center"
		'		Return Master_Cost_Center

		'	'Case "Pelayaran"
		'	'    Return Pelayaran

		'	'Case "Pembelian_Import"
		'	'    Return Pembelian_Import

		'	'Case "Pembelian_New3"
		'	'    Return Pembelian_New3

		'	'Case "Penjualan_New"
		'	'    Return Penjualan_New

		'	'Case "PO_Import_Grouping"
		'	'    Return PO_Import_Grouping

		'	'Case "PO_Import_Grouping_dinamis"
		'	'    Return PO_Import_Grouping_dinamis

		'	'Case "Rencana_Order_Gabungan"
		'	'    Return Rencana_Order_Gabungan

		'	Case "EMI_Display_Barcode"
		'		Return EMI_Display_Barcode

		'	Case "EMI_Independent_Order"
		'		Return EMI_Independent_Order

		'	'Case "Submit_PO_Import"
		'	'    Return Submit_PO_Import

		'	Case "Transaksi_Biaya_import"
		'		Return Transaksi_Biaya_import

		'	Case "Transaksi_Biaya_import3"
		'		Return Transaksi_Biaya_import3

		'	'Case "Display_Penawaran_Berakhir"
		'	'    Return Display_Penawaran_Berakhir

		'	Case "Transaksi_Formula"
		'		Return Transaksi_Formula

		'	Case "Transaksi_Formula_Binding"
		'		Return Transaksi_Formula_Binding

		'	Case "Display_Formula"
		'		Return Display_Formula

		'	Case "EMI_Transaksi_MaterialRequisition"
		'		Return EMI_Transaksi_MaterialRequisition

		'	Case "Display_Formula_Binding"
		'		Return Display_Formula_Binding

		'	Case "EMI_Transfer_Stock"
		'		Return EMI_Transfer_Stock

		'	Case "EMI_Display_Data_Terima_Transfer"
		'		Return EMI_Display_Data_Terima_Transfer

		'	Case "EMI_Adjustment_Dist"
		'		Return EMI_Adjustment_Dist

		'	Case "Emi_Transaksi_Cost_Center"
		'		Return EMI_Transaksi_Cost_Center

		'	Case "Emi_Transaksi_Work_Center"
		'		Return EMI_Transaksi_Work_Center

		'	Case "EMI_Display_Quality_Control"
		'		Return Emi_Display_Quality_Control

		'	Case "Master_Menu"
		'		Return Master_Menu

		'	Case "Master_Role"
		'		Return Master_Role

		'	'Case "Master_Jenis_Muatan"
		'	'    Return Master_Jenis_Muatan

		'	'Case "Master_Packing"
		'	'    Return Master_Packing

		'	Case "EMI_Display_Hasil_Produksi"
		'		Return EMI_Display_Hasil_Produksi

		'	Case "EMI_Split_Production"
		'		Return EMI_Split_Production

		'	Case "Laporan_Purchase_Requisition"
		'		Return Laporan_Purchase_Requisition

		'	'Case "Laporan_Purchase_Order"
		'	'    Return Laporan_Purchase_Order

		'	Case "EMI_Transfer_Stock2"
		'		Return EMI_Transfer_Stock2

		'	Case "EMI_Display_Pallet_Masuk"
		'		Return EMI_Display_Pallet_Masuk

		'	Case "Server_Sinkronasi_B2B"
		'		Return Server_Sinkronasi_B2B

		'	Case "Transfer_Stock_3"
		'		Return Transfer_Stock_3

		'	'Case "EMI_Pembelian_PR_Summary_Data"
		'	'    Return EMI_Pembelian_PR_Summary_Data

		'	Case "Master_Klasifikasi_Bahan2"
		'		Return Master_Klasifikasi_Bahan2

		'	Case "Display_Kendaraan_Masuk_Tidak_Sesuai" : Return Display_Kendaraan_Masuk_Tidak_Sesuai

		'	Case = "EMI_Transfer_Stock_Display"
		'		Return EMI_Transfer_Stock_Display

		'	'Case = "SD_Pilih_Produk_New"
		'	'    Return SD_Pilih_Produk_New

		'	Case "Master_Cost_Center" : Return Master_Cost_Center
		'	Case "Master_Work_Center" : Return Master_Work_Center
		'	Case "Master_Routing" : Return Master_Routing
		'	Case "Master_Mesin" : Return Master_Mesin
		'	Case "EMI_Production_Order_Summary_Data" : Return EMI_Production_Order_Summary_Data
		'	Case "EMI_Display_Split_Production_Order" : Return EMI_Display_Split_Production_Order
		'	Case "EMI_Production_Order" : Return EMI_Production_Order
		'	Case "EMI_Independent_Order" : Return EMI_Independent_Order
		'	'Case "EMI_Schedule" : Return EMI_Schedule
		'	Case "EMI_Split_Production" : Return EMI_Split_Production
		'	Case "EMI_Display_Mulai_Produksi" : Return EMI_Display_Mulai_Produksi
		'	Case "EMI_Display_Selesai_Produksi" : Return EMI_Display_Selesai_Produksi
		'	Case "Emi_Production_Barcode" : Return Emi_Production_Barcode
		'	Case "EMI_Display_Hasil_Produksi" : Return EMI_Display_Hasil_Produksi
		'	Case "EMI_Display_Hasil_HPP" : Return EMI_Display_Hasil_HPP
		'	Case "EMI_Transaksi_Cost_Center" : Return EMI_Transaksi_Cost_Center
		'	Case "EMI_Transaksi_Work_Center" : Return EMI_Transaksi_Work_Center

		'	'Case "Master_Gudang" : Return Master_Gudang
		'	'Case "Master_Gudang_Kategori" : Return Master_Gudang_Kategori
		'	'Case "Master_IP_Cam" : Return Master_IP_Cam
		'	'Case "Master_Jenis_Muatan" : Return Master_Jenis_Muatan
		'	Case "Transfer_Stock_3" : Return Transfer_Stock_3
		'	Case "Emi_Display_Transfer" : Return EMI_Display_Transfer
		'	Case "Display_Kendaraan_Masuk_Tidak_Sesuai" : Return Display_Kendaraan_Masuk_Tidak_Sesuai
		'	Case "Emi_Display_Timbang" : Return EMI_Display_Timbang
		'	Case "Emi_Display_Quality_Control" : Return Emi_Display_Quality_Control
		'	Case "Emi_Display_Timbang" : Return EMI_Display_Timbang
		'	Case "Emi_Display_Pallet_Masuk" : Return EMI_Display_Pallet_Masuk
		'	Case "Display_Hasil_Quality_Control" : Return Display_Hasil_Quality_Control
		'	Case "Display_Tracking_Kendaraan" : Return Display_Tracking_Kendaraan
		'	Case "Emi_Display_Pallet_Masuk_Data" : Return EMI_Display_Pallet_Masuk_Data

		'	'Case "Display_Data_Penawaran" : Return Display_Data_Penawaran
		'	'Case "Display_Penawaran_Berakhir" : Return Display_Penawaran_Berakhir
		'	Case "Emi_Pembelian_PO_Summary_Data" : Return EMI_Pembelian_PO_Summary_Data
		'	'Case "Emi_Pembelian_PR_Summary_Data" : Return EMI_Pembelian_PR_Summary_Data
		'	'Case "Laporan_Purchase_Order" : Return Laporan_Purchase_Order
		'	Case "Laporan_Purchase_Requisition" : Return Laporan_Purchase_Requisition
		'	Case "Transaksi_Penawaran" : Return Transaksi_Penawaran
		'	Case "Emi_PO_Pembelian_Display" : Return EMI_PO_Pembelian_Display
		'	Case "Purchase_Requisition" : Return Purchase_Requisition

		'	Case "Master_Quality_Control" : Return Master_Quality_Control
		'	'Case "Master_Quality_Control_Kategori" : Return Master_Quality_Control_Kategori
		'	Case "Master_Quality_Control_Kategori_Detail" : Return Master_Quality_Control_Kategori_Detail
		'	Case "Master_Quality_Control_Barang" : Return Master_Quality_Control_Barang
		'	'Case "Master_Quality_Control_Kendaraan" : Return Master_Quality_Control_Kendaraan
		'	Case "Display_Formula" : Return Display_Formula
		'	Case "Display_Formula_Binding" : Return Display_Formula_Binding
		'	'Case "Emi_Display_Log_MaterialRequisition" : Return Emi_Display_Log_MaterialRequisition
		'	Case "Display_Hasil_Quality_Control" : Return Display_Hasil_Quality_Control
		'	Case "Emi_Display_Quality_Control" : Return Emi_Display_Quality_Control
		'	Case "Emi_Transaksi_MaterialRequisition" : Return EMI_Transaksi_MaterialRequisition
		'	Case "Transaksi_Formula" : Return Transaksi_Formula
		'	Case "Transaksi_Formula_Binding" : Return Transaksi_Formula_Binding
		'	Case "EMI_Display_Hasil_ProduksiFG" : Return EMI_Display_Hasil_ProduksiFG
		'	Case "EMI_Display_Pengeluaran_Bahan_Baku" : Return EMI_Display_Pengeluaran_Bahan_Baku
		'	Case "EMI_Display_Production_Result" : Return EMI_Display_Production_Result

		'		'Case "Master_Kategori_Supplier" : Return Master_Kategori_Supplier
		'		'Case "Master_Perhitungan_Jatuh_Tempo" : Return Master_Perhitungan_Jatuh_Tempo

		'		'Case "Jf_Pengajuan_Biaya" : Return Jf_Pengajuan_Biaya
		'		'Case "Jf_Display_Validasi_Pengajuan_Biaya" : Return Jf_Display_Validasi_Pengajuan_Biaya
		'		'Case "Jf_Uang_Masuk_Global" : Return Jf_Uang_Masuk_Global
		'		'Case "Pengajuan_Cheque_Backup" : Return Pengajuan_Cheque_Backup
		'		'Case "Jf_Laba_Rugi_Lama" : Return Jf_Laba_Rugi_Lama
		'		'Case "Jf_Master_Bank" : Return Jf_Master_Bank
		'		'Case "Jf_Master_Rekening" : Return Jf_Master_Rekening
		'		'Case "Jf_Display_Validasi_Pengajuan_Cheque" : Return Jf_Display_Validasi_Pengajuan_Cheque
		'		'Case "Rekon_Saldo_New" : Return Rekon_Saldo_New
		'		'Case "Jf_Master_Cheque" : Return Jf_Master_Cheque
		'		'Case "Jf_Master_Bank_Tujuan" : Return Jf_Master_Bank_Tujuan
		'		'Case "Master_Bank_Di_Pengajuan" : Return Master_Bank_Di_Pengajuan
		'		'Case "Display_Print_Cheque_Slip" : Return Display_Print_Cheque_Slip
		'		'Case "Master_Jenis_Slip" : Return Master_Jenis_Slip
		'		'Case "Master_Rekening_Tujuan" : Return Master_Rekening_Tujuan
		'		'Case "Display_Print_Slip" : Return Display_Print_Slip
		'		'Case "Display_Jurnal_Pengajuan" : Return Display_Jurnal_Pengajuan
		'		'Case "Account_Jurnal_Pengajuan" : Return Account_Jurnal_Pengajuan
		'		'Case "Display_Validasi_Selesai_Cheque" : Return Display_Validasi_Selesai_Cheque
		'		'Case "Master_Rekening_Pengirim" : Return Master_Rekening_Pengirim
		'		'Case "Display_History_Validasi_Selesai_Cheque" : Return Display_History_Validasi_Selesai_Cheque
		'		'Case "Display_History_Jurnal_Pengajuan" : Return Display_History_Jurnal_Pengajuan
		'		'Case "Jf_Pengajuan_Token" : Return Jf_Pengajuan_Token
		'		'Case "Jf_Display_Validasi_Pengajuan_Token" : Return Jf_Display_Validasi_Pengajuan_Token
		'		'Case "Display_Cheque" : Return Display_Cheque
		'		'Case "Display_Validasi_Slip_New" : Return Display_Validasi_Slip_New
		'		'Case "Display_Jurnal_Pengajuan_token" : Return Display_Jurnal_Pengajuan_token
		'		'Case "Display_History_Jurnal_Token" : Return Display_History_Jurnal_Token
		'		'Case "Jf_Pengajuan_Biaya_Sementara" : Return Jf_Pengajuan_Biaya_Sementara
		'		'Case "Master_Penanda_Tangan" : Return Master_Penanda_Tangan
		'		'Case "Display_Validasi_Slip" : Return Display_Validasi_Slip
		'		'Case "Jf_Validasi_Buku_Bank_Pengajuan" : Return Jf_Validasi_Buku_Bank_Pengajuan
		'		'Case "Laporan_Rekon_Saldo" : Return Laporan_Rekon_Saldo

		'End Select

#End Region

	End Function

	'Private Function convertStringToForm(ByVal formToOpen As String) As Form

	'    ' Mendapatkan tipe form berdasarkan nama
	'    Dim namaForm As String = $"{Me.GetType().Namespace}.{formToOpen}"
	'    'Dim formType As Type = Type.GetType("ERP_EMI.EMI_Display_Timbang")
	'    Dim formType As Type = Type.GetType(namaForm)

	'    ' Periksa apakah tipe form ditemukan
	'    If formType IsNot Nothing Then
	'        ' Mendapatkan instance default dari form menggunakan reflection
	'        'Dim instanceDefault As Form = DirectCast(formType.GetProperty("DefaultInstance").GetValue(Nothing, Nothing), Form)
	'        Dim instanceDefault As Form = CType(Activator.CreateInstance(formType), Form)
	'        'Dim instanceDefault As Form = DirectCast(Activator.CreateInstance(formType), Form)

	'        ' Menampilkan form secara instance default
	'        If instanceDefault.Visible Then
	'            instanceDefault.BringToFront()
	'            Exit Function
	'        Else
	'            'instanceDefault.Show()
	'            Return instanceDefault
	'        End If
	'    Else
	'        MessageBox.Show($"Form dengan nama '{formToOpen}' tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
	'    End If

	'    Return Nothing

	'End Function

End Class