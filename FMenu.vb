Imports System.Reflection

Public Class FMenu

	Private Sub FMenu_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Async Sub FMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		' 1. Monitor Setup (Multimonitor support)
		If Screen.AllScreens.Length > 1 Then
			Me.StartPosition = FormStartPosition.Manual
			Dim secondaryScreen As Screen = Screen.AllScreens(1)
			Me.WindowState = FormWindowState.Normal
			Me.Location = secondaryScreen.Bounds.Location
			Me.WindowState = FormWindowState.Maximized
		Else
			Me.StartPosition = FormStartPosition.CenterScreen
			Me.WindowState = FormWindowState.Maximized
		End If

		' Update Status Information
		ToolStripStatusLabel1.Text = "Login : " & UserID
		ToolStripStatusLabel4.Text = "Lokasi : " & Lokasi

		' Styling MDI Client Area
		For Each C As Control In Me.Controls
			If TypeOf C Is MdiClient Then
				C.BackColor = Color.FromArgb(248, 250, 252) '#F8FAFC ERP Background
				Exit For
			End If
		Next

		' Apply Professional Modern ERP Menu Renderer
		MenuStrip1.Renderer = New ErpMenuRenderer()

		' 2. Load Waktu Server secara Async
		Await GetServerTimeAsync()

		' 3. Load Menu secara ASYNC dengan High-Performance In-Memory Bulk Processing
		Await LoadMenuStripAsync()

		Timer1.Start()
	End Sub

	Private Async Function GetServerTimeAsync() As Task
		Try
			Dim jamServer As String = ""
			Await Task.Run(Sub()
							   OpenConn()
							   Using Dr = OpenTrans("SELECT DATEADD(hh, " & selisihjam & ", GETDATE()) as Jam")
								   If Dr.Read Then
									   jamServer = Format(Dr("jam"), "yyyy-MM-dd HH:mm:ss")
								   End If
							   End Using
							   CloseConn()
						   End Sub)

			If Not String.IsNullOrEmpty(jamServer) Then
				ToolStripStatusLabel3.Text = jamServer
			End If
		Catch ex As Exception
			CloseConn()
		End Try
	End Function

	'=======================================================================================================================================
	'= HELPER: FILTER SAFE DATA TABLE (MENCEGAH ERROR System.Data.EvaluateException: Cannot find column)
	'=======================================================================================================================================
	Private Function SafeSelectDataTable(ByVal dt As DataTable, ByVal filterExpr As String) As DataRow()
		If dt IsNot Nothing AndAlso dt.Columns.Count > 0 AndAlso dt.Rows.Count > 0 Then
			Try
				Dim colName As String = filterExpr.Split("="c)(0).Trim()
				If dt.Columns.Contains(colName) Then
					Return dt.Select(filterExpr)
				End If
			Catch ex As Exception
				Return New DataRow() {}
			End Try
		End If
		Return New DataRow() {}
	End Function

	'=======================================================================================================================================
	'= PERFORMA TINGGI: MEMUAT SELURUH MENU DALAM 1 KONEKSI DATABASE & IN-MEMORY FILTERING (< 50ms)
	'=======================================================================================================================================
	Private Async Function LoadMenuStripAsync() As Task
		Dim dtMenus As New DataTable()
		Dim dtSubMenus As New DataTable()
		Dim dtSubLv1 As New DataTable()
		Dim dtSubLv2 As New DataTable()
		Dim dtSubLv3 As New DataTable()
		Dim moduleTitleName As String = "ERP System"

		' Fetch ALL DataTables in 1 Single Async Thread execution
		Await Task.Run(Sub()
						   Try
							   OpenConn()

							   ' 0. Get MainMenu Title Name
							   Using drTitle = OpenTrans($"SELECT Title FROM mainmenu WHERE MainMenuID = '{MainMenuID}'")
								   If drTitle.Read Then
									   moduleTitleName = General_Class.CekNULL(drTitle("Title")).ToString().Trim()
								   End If
							   End Using

							   ' 1. Menus
							   Dim sqlM As String = $"SELECT Menus.MenuID, Menus.MenuName FROM RoleMenus INNER JOIN Menus ON RoleMenus.MenuID=Menus.MenuID WHERE RoleMenus.UserID='{UserID}' AND Menus.MainMenuID='{MainMenuID}' ORDER BY MenuOrder"
							   Using dr = OpenTrans(sqlM)
								   dtMenus.Load(dr)
							   End Using

							   ' 2. SubMenus
							   Dim sqlS As String = "SELECT MAX(SubMenuLv1.SubMenuID) AS SubMenuIDLv1, " &
													"SubMenus.SubMenuName, SubMenus.MenuID, SubMenus.SubMenuID, SubMenus.Form, " &
													"SubMenus.Variabel, SubMenus.Isi_Variabel, " &
													"SubMenus.Variabel2, SubMenus.Isi_Variabel2, " &
													"SubMenus.Variabel3, SubMenus.Isi_Variabel3 " &
													"FROM RoleSubMenu " &
													"INNER JOIN SubMenus ON RoleSubMenu.SubMenuID = SubMenus.SubMenuID " &
													"LEFT JOIN SubMenuLv1 ON SubMenus.SubMenuID = SubMenuLv1.SubMenuID " &
													$"WHERE RoleSubMenu.UserID = '{UserID}' " &
													"GROUP BY SubMenus.SubMenuName, SubMenus.MenuID, SubMenus.SubMenuID, SubMenus.Form, SubMenus.Variabel, SubMenus.Isi_Variabel, SubMenus.Variabel2, SubMenus.Isi_Variabel2, SubMenus.Variabel3, SubMenus.Isi_Variabel3 " &
													"ORDER BY MAX(SubMenus.SubMenuOrder);"
							   Using dr = OpenTrans(sqlS)
								   dtSubMenus.Load(dr)
							   End Using

							   ' 3. SubMenuLv1
							   Dim sqlLv1 As String = "SELECT MAX(SubMenuLv2.SubMenuLv1ID) AS SubMenuLv1IDFromSubMenuLv2, " &
													  "SubMenuLv1.SubMenuLv1Name, SubMenuLv1.SubMenuID, SubMenuLv1.SubMenuLv1ID, SubMenuLv1.Form, " &
													  "SubMenuLv1.Variabel, SubMenuLv1.Isi_Variabel, " &
													  "SubMenuLv1.Variabel2, SubMenuLv1.Isi_Variabel2, " &
													  "SubMenuLv1.Variabel3, SubMenuLv1.Isi_Variabel3 " &
													  "FROM RoleSubMenuLv1 " &
													  "INNER JOIN SubMenuLv1 ON RoleSubMenuLv1.SubMenuLv1ID = SubMenuLv1.SubMenuLv1ID " &
													  "LEFT JOIN SubMenuLv2 ON SubMenuLv1.SubMenuLv1ID = SubMenuLv2.SubMenuLv1ID " &
													  $"WHERE RoleSubMenuLv1.UserID = '{UserID}' " &
													  "GROUP BY SubMenuLv1.SubMenuLv1Name, SubMenuLv1.SubMenuID, SubMenuLv1.SubMenuLv1ID, SubMenuLv1.Form, SubMenuLv1.Variabel, SubMenuLv1.Isi_Variabel, SubMenuLv1.Variabel2, SubMenuLv1.Isi_Variabel2, SubMenuLv1.Variabel3, SubMenuLv1.Isi_Variabel3 " &
													  "ORDER BY MAX(SubMenuLv1.SubMenuLv1Order);"
							   Using dr = OpenTrans(sqlLv1)
								   dtSubLv1.Load(dr)
							   End Using

							   ' 4. SubMenuLv2
							   Dim sqlLv2 As String = "SELECT MAX(SubMenuLv3.SubMenuLv2ID) AS SubMenuLv2IDFromSubLv3, " &
													  "SubMenuLv2.SubMenuLv2Name, SubMenuLv2.SubMenuLv1ID, SubMenuLv2.SubMenuLv2ID, SubMenuLv2.Form, " &
													  "SubMenuLv2.Variabel, SubMenuLv2.Isi_Variabel, " &
													  "SubMenuLv2.Variabel2, SubMenuLv2.Isi_Variabel2, " &
													  "SubMenuLv2.Variabel3, SubMenuLv2.Isi_Variabel3 " &
													  "FROM RoleSubMenuLv2 " &
													  "INNER JOIN SubMenuLv2 ON RoleSubMenuLv2.SubMenuLv2ID = SubMenuLv2.SubMenuLv2ID " &
													  "LEFT JOIN SubMenuLv3 ON SubMenuLv2.SubMenuLv2ID = SubMenuLv3.SubMenuLv2ID " &
													  $"WHERE RoleSubMenuLv2.UserID = '{UserID}' " &
													  "GROUP BY SubMenuLv2.SubMenuLv2Name, SubMenuLv2.SubMenuLv1ID, SubMenuLv2.SubMenuLv2ID, SubMenuLv2.Form, SubMenuLv2.Variabel, SubMenuLv2.Isi_Variabel, SubMenuLv2.Variabel2, SubMenuLv2.Isi_Variabel2, SubMenuLv2.Variabel3, SubMenuLv2.Isi_Variabel3 " &
													  "ORDER BY MAX(SubMenuLv2.SubMenuLv2Order);"
							   Using dr = OpenTrans(sqlLv2)
								   dtSubLv2.Load(dr)
							   End Using

							   ' 5. SubMenuLv3
							   Dim sqlLv3 As String = "SELECT SubMenuLv3.SubMenuLv3Name, SubMenuLv3.SubMenuLv2ID, SubMenuLv3.Form, " &
													  "SubMenuLv3.Variabel, SubMenuLv3.Isi_Variabel, " &
													  "SubMenuLv3.Variabel2, SubMenuLv3.Isi_Variabel2, " &
													  "SubMenuLv3.Variabel3, SubMenuLv3.Isi_Variabel3 " &
													  "FROM RoleSubMenuLv3 " &
													  "INNER JOIN SubMenuLv3 ON RoleSubMenuLv3.SubMenuLv3ID = SubMenuLv3.SubMenuLv3ID " &
													  $"WHERE RoleSubMenuLv3.UserID = '{UserID}' " &
													  "GROUP BY SubMenuLv3.SubMenuLv3Name, SubMenuLv3.SubMenuLv2ID, SubMenuLv3.Form, SubMenuLv3.Variabel, SubMenuLv3.Isi_Variabel, SubMenuLv3.Variabel2, SubMenuLv3.Isi_Variabel2, SubMenuLv3.Variabel3, SubMenuLv3.Isi_Variabel3 " &
													  "ORDER BY MAX(SubMenuLv3.SubMenuLv3Order);"
							   Using dr = OpenTrans(sqlLv3)
								   dtSubLv3.Load(dr)
							   End Using

							   CloseConn()
						   Catch ex As Exception
							   CloseConn()
						   End Try
					   End Sub)

		Me.Text = $"ERP System - {moduleTitleName}"

		' Build MenuStrip UI Items in RAM Memory Fast!
		MenuStrip1.Items.Clear()

		If dtMenus.Rows.Count > 0 Then
			For Each rowM As DataRow In dtMenus.Rows
				Dim menuID As String = rowM("MenuID").ToString()
				Dim menuText As String = rowM("MenuName").ToString()
				Dim mainMenu As New ToolStripMenuItem(menuText) With {
					.ForeColor = Color.FromArgb(15, 23, 42),
					.Font = New Font("Work Sans", 9.0!, FontStyle.Regular)
				}

				' Filter SubMenus for this MenuID safely in-memory
				Dim subRows = SafeSelectDataTable(dtSubMenus, $"MenuID = '{menuID}'")
				For Each rowS As DataRow In subRows
					Dim subMenuID As String = rowS("SubMenuID").ToString()
					Dim subText As String = rowS("SubMenuName").ToString()
					Dim menuItemSub As New ToolStripMenuItem(subText) With {
						.ForeColor = Color.FromArgb(15, 23, 42),
						.Font = New Font("Work Sans", 8.5!, FontStyle.Regular)
					}

					' Check SubMenuLv1 items for this SubMenuID safely
					Dim lv1Rows = SafeSelectDataTable(dtSubLv1, $"SubMenuID = '{subMenuID}'")
					If lv1Rows.Length > 0 Then
						For Each rowLv1 As DataRow In lv1Rows
							Dim lv1ID As String = rowLv1("SubMenuLv1ID").ToString()
							Dim lv1Text As String = rowLv1("SubMenuLv1Name").ToString()
							Dim menuItemLv1 As New ToolStripMenuItem(lv1Text) With {
								.ForeColor = Color.FromArgb(15, 23, 42),
								.Font = New Font("Work Sans", 8.5!, FontStyle.Regular)
							}

							' Check SubMenuLv2 items for this SubMenuLv1ID safely
							Dim lv2Rows = SafeSelectDataTable(dtSubLv2, $"SubMenuLv1ID = '{lv1ID}'")
							If lv2Rows.Length > 0 Then
								For Each rowLv2 As DataRow In lv2Rows
									Dim lv2ID As String = rowLv2("SubMenuLv2ID").ToString()
									Dim lv2Text As String = rowLv2("SubMenuLv2Name").ToString()
									Dim menuItemLv2 As New ToolStripMenuItem(lv2Text) With {
										.ForeColor = Color.FromArgb(15, 23, 42),
										.Font = New Font("Work Sans", 8.5!, FontStyle.Regular)
									}

									' Check SubMenuLv3 items for this SubMenuLv2ID safely
									Dim lv3Rows = SafeSelectDataTable(dtSubLv3, $"SubMenuLv2ID = '{lv2ID}'")
									If lv3Rows.Length > 0 Then
										For Each rowLv3 As DataRow In lv3Rows
											Dim lv3Text As String = rowLv3("SubMenuLv3Name").ToString()
											Dim menuItemLv3 As New ToolStripMenuItem(lv3Text) With {
												.ForeColor = Color.FromArgb(15, 23, 42),
												.Font = New Font("Work Sans", 8.5!, FontStyle.Regular)
											}
											BindFormClick(menuItemLv3, rowLv3)
											menuItemLv2.DropDownItems.Add(menuItemLv3)
										Next
									Else
										BindFormClick(menuItemLv2, rowLv2)
									End If

									menuItemLv1.DropDownItems.Add(menuItemLv2)
								Next
							Else
								BindFormClick(menuItemLv1, rowLv1)
							End If

							menuItemSub.DropDownItems.Add(menuItemLv1)
						Next
					Else
						BindFormClick(menuItemSub, rowS)
					End If

					mainMenu.DropDownItems.Add(menuItemSub)
				Next

				MenuStrip1.Items.Add(mainMenu)
			Next
		End If
	End Function

	Private Sub BindFormClick(ByVal menuItem As ToolStripMenuItem, ByVal row As DataRow)
		If row("Form") Is DBNull.Value OrElse String.IsNullOrWhiteSpace(row("Form").ToString()) Then Exit Sub

		Dim formName As String = row("Form").ToString().Trim()
		Dim v1 As String = If(row("Variabel") Is DBNull.Value, "", row("Variabel").ToString())
		Dim iv1 As String = If(row("Isi_Variabel") Is DBNull.Value, "", row("Isi_Variabel").ToString())
		Dim v2 As String = If(row("Variabel2") Is DBNull.Value, "", row("Variabel2").ToString())
		Dim iv2 As String = If(row("Isi_Variabel2") Is DBNull.Value, "", row("Isi_Variabel2").ToString())
		Dim v3 As String = If(row("Variabel3") Is DBNull.Value, "", row("Variabel3").ToString())
		Dim iv3 As String = If(row("Isi_Variabel3") Is DBNull.Value, "", row("Isi_Variabel3").ToString())

		AddHandler menuItem.Click, Sub(sender As Object, e As EventArgs)
									   Dim targetForm As Form = convertStringToForm(formName)
									   HandlerClickMenu(targetForm, v1, iv1, v2, iv2, v3, iv3)
								   End Sub
	End Sub

	Private Sub HandlerClickMenu(ByVal formToOpen As Form, Optional ByVal V1 As String = "", Optional ByVal IV1 As String = "", Optional ByVal V2 As String = "", Optional ByVal IV2 As String = "", Optional ByVal V3 As String = "", Optional ByVal IV3 As String = "")

		If formToOpen Is Nothing Then
			Exit Sub
		End If

		Dim isFormOpen As Boolean = False

		For Each frm As Form In Application.OpenForms
			If frm.GetType() Is formToOpen.GetType() Then
				frm.Focus()
				isFormOpen = True
				Exit For
			End If
		Next

		If Not isFormOpen Then
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

	Private Function convertStringToForm(ByVal formToOpen As String) As Form
		Try
			Dim formProperty As PropertyInfo = My.Forms.GetType().GetProperty(formToOpen)

			If formProperty IsNot Nothing Then
				Return DirectCast(formProperty.GetValue(My.Forms, Nothing), Form)
			Else
				Return Nothing
			End If
		Catch ex As Exception
			Return Nothing
		End Try
	End Function

	Private Sub F_Menu_Closed(sender As Object, e As EventArgs) Handles Me.Closed
		Me.Dispose()
		Main_Menu.Show()
		Main_Menu.Focus()
	End Sub

	Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
		Try
			Dim currentDt As DateTime
			If DateTime.TryParse(ToolStripStatusLabel3.Text, currentDt) Then
				ToolStripStatusLabel3.Text = currentDt.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss")
			End If
		Catch ex As Exception
		End Try
	End Sub

	Private Async Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
		Await GetServerTimeAsync()
	End Sub

End Class

'=======================================================================================================================================
'= MODERN PURE WHITE ERP MENU RENDERER (Clean Regular Text Font)
'=======================================================================================================================================
Public Class ErpMenuRenderer
	Inherits ToolStripProfessionalRenderer

	Public Sub New()
		MyBase.New(New ErpColorTable())
	End Sub

	Protected Overrides Sub OnRenderItemText(ByVal e As ToolStripItemTextRenderEventArgs)
		If e.Item.Selected Then
			' Saat item di-hover, gunakan teks biru gelap (#0F567A) di atas background Light Ice Blue (#E0F2FE)
			e.TextColor = Color.FromArgb(15, 86, 122) '#0F567A Corporate Blue
		Else
			' Saat normal, gunakan teks Dark Slate (#0F172A)
			e.TextColor = Color.FromArgb(15, 23, 42) '#0F172A Dark Slate
		End If
		MyBase.OnRenderItemText(e)
	End Sub

End Class

Public Class ErpColorTable
	Inherits ProfessionalColorTable

	' Top MenuBar Background (Pure White)
	Public Overrides ReadOnly Property MenuStripGradientBegin As Color
		Get
			Return Color.White
		End Get
	End Property

	Public Overrides ReadOnly Property MenuStripGradientEnd As Color
		Get
			Return Color.White
		End Get
	End Property

	' Menu Item Hover / Selected Background (Light Ice Blue #E0F2FE)
	Public Overrides ReadOnly Property MenuItemSelected As Color
		Get
			Return Color.FromArgb(224, 242, 254) '#E0F2FE Light Ice Blue
		End Get
	End Property

	Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
		Get
			Return Color.FromArgb(224, 242, 254)
		End Get
	End Property

	Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
		Get
			Return Color.FromArgb(224, 242, 254)
		End Get
	End Property

	Public Overrides ReadOnly Property MenuItemPressedGradientBegin As Color
		Get
			Return Color.FromArgb(224, 242, 254)
		End Get
	End Property

	Public Overrides ReadOnly Property MenuItemPressedGradientEnd As Color
		Get
			Return Color.FromArgb(224, 242, 254)
		End Get
	End Property

	Public Overrides ReadOnly Property ToolStripDropDownBackground As Color
		Get
			Return Color.White
		End Get
	End Property

	Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
		Get
			Return Color.White
		End Get
	End Property

	Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color
		Get
			Return Color.White
		End Get
	End Property

	Public Overrides ReadOnly Property ImageMarginGradientEnd As Color
		Get
			Return Color.White
		End Get
	End Property

	Public Overrides ReadOnly Property MenuBorder As Color
		Get
			Return Color.FromArgb(203, 213, 225)
		End Get
	End Property

	Public Overrides ReadOnly Property MenuItemBorder As Color
		Get
			Return Color.FromArgb(186, 230, 253) '#BAE6FD Border Halus
		End Get
	End Property

End Class