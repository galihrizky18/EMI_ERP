Imports System.Data.SqlClient

Public Class Master_Role

	Dim arrUserReference, arrFilterRole As New ArrayList

	Dim _UserID As String
	Dim KodePerusahaan As String = "001"

	Dim isLoading As Boolean = False
	Dim FocusedForm As String
	Dim CurrentButtonName As New List(Of String)

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim arrUser, arrMainMenu, arrMenu, arrSubMenu, arrSubMenuLv1, arrSubMenuLv2, arrSubMenuLv3 As New ArrayList
	Dim tmp_mainmenu, tmp_menu, tmp_submenu, tmp_submenu1, tmp_submenu2, tmp_submenu3 As New ArrayList
	Dim LvMainMenuID, LvMenuID, LvSubMenuID, LvSubMenuLv1ID, LvSubMenuLv2ID, LvSubMenuLv3ID As String

	Dim itemMainMenuIDRole As Integer = 7
	Dim itemMenuIDRole As Integer = 8
	Dim itemSubMenuIDRole As Integer = 9
	Dim itemSubMenuLv1IDRole As Integer = 10
	Dim itemSubMenuLv2IDRole As Integer = 11
	Dim itemSubMenuLv3IDRole As Integer = 12

	Dim LvRoleButton_Form, LvRoleButton_Button, LvRoleButton_Kategori, LvRoleButton_Keterangan As String

	Dim ItemRoleButton_Kategori As Integer = 0
	Dim ItemRoleButton_Button As Integer = 1
	Dim ItemRoleButton_Form As Integer = 2
	Dim ItemRoleButton_Keterangan As Integer = 3

	Dim LvMenu_Kosong, LvMenu_Title, LvMenu_MenuName, LvMenu_SubMenuName, LvMenu_SubMenuLv1Name, LvMenu_SubMenuLv2Name, LvMenu_SubMenuLv3Name As String
	Dim LvMenu_MainMenuID, LvMenu_MenuID, LvMenu_SubMenuID, LvMenu_SubMenuLv1ID, LvMenu_SubMenuLv2ID, LvMenu_SubMenuLv3ID, LvMenu_Form As String

	Dim itemMenu_Kosong As Integer = 0
	Dim itemMenu_Title As Integer = 1
	Dim itemMenu_MenuName As Integer = 2
	Dim itemMenu_SubMenuName As Integer = 3
	Dim itemMenu_SubMenuName1 As Integer = 4
	Dim itemMenu_SubMenuName2 As Integer = 5
	Dim itemMenu_SubMenuName3 As Integer = 6
	Dim itemMenu_MainMenuID As Integer = 7
	Dim itemMenu_MenuID As Integer = 8
	Dim itemMenu_SubMenuID As Integer = 9
	Dim itemMenu_SubMenuLv1ID As Integer = 10
	Dim itemMenu_SubMenuLv2ID As Integer = 11
	Dim itemMenu_SubMenuLv3ID As Integer = 12
	Dim itemMenu_Form As Integer = 13

	Private Sub Master_Role_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBuffer(Lv_Role)
		EnableDoubleBuffer(Lv_RoleButton)

		kosong()
		kosongArr()
		LoadLvRole()

		Cmb_Filter_RoleButton.Items.Clear() : arrFilterRole.Clear()
		Cmb_Filter_RoleButton.Items.Add("Kategori") : arrFilterRole.Add("a.Kategori")
		Cmb_Filter_RoleButton.Items.Add("Button") : arrFilterRole.Add("a.ButtonName")

	End Sub

	'HANDLE RESET
	Private Sub kosong()

		Lv_Role.Items.Clear()
		Cmb_Reference.Items.Clear() : arrUserReference.Clear()

		Cb_Users.Items.Clear()
		Tb_UserName.Text = String.Empty
		Tb_UserName.Enabled = False
		FocusedForm = ""

		Chk_All_RoleButton.Checked = False
		Cmb_Filter_RoleButton.SelectedIndex = -1
		Lv_RoleButton.Items.Clear() : Txt_Filter_RoleButton.Text = ""

		Try
			OpenConn()

			'====================
			'=     GET USER     =
			'====================
			arrUser.Clear()
			SQL = "select UserID, UserName, UserLevel from users"
			Using dr = OpenTrans(SQL)
				If dr.HasRows Then
					Do While dr.Read
						Cb_Users.Items.Add(dr("UserName")) : arrUser.Add(dr("UserID"))
						Cmb_Reference.Items.Add(dr("UserName")) : arrUserReference.Add(dr("UserID"))
					Loop
				Else
					dr.Close()
					CloseTrans()
					MessageBox.Show("Failed Get Users...")
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Failed Connect...")
			Exit Sub
		End Try

	End Sub

	Private Sub KosongSebagian()

		Cmb_Reference.SelectedIndex = -1 : Cmb_Reference.Text = ""

		If Not String.IsNullOrWhiteSpace(arrUser.Item(Cb_Users.SelectedIndex)) Then
			LoadAllRoleMenus()
		End If

		Chk_All_RoleButton.Checked = False
		Cmb_Filter_RoleButton.SelectedIndex = -1
		Txt_Filter_RoleButton.Text = ""
		Lv_RoleButton.Items.Clear()

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

		Lv_Role.Columns.Clear()
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
		Lv_Role.Columns.Add("Form", 0, HorizontalAlignment.Center)
		Lv_Role.View = View.Details

		Lv_RoleButton.Columns.Clear()
		Lv_RoleButton.Columns.Add("Kategori", 140, HorizontalAlignment.Left)
		Lv_RoleButton.Columns.Add("Button", 200, HorizontalAlignment.Left)
		Lv_RoleButton.Columns.Add("Form", 0, HorizontalAlignment.Left)
		Lv_RoleButton.Columns.Add("Keterangan", 0, HorizontalAlignment.Left)
		Lv_RoleButton.Columns.Add("IsAcess", 0, HorizontalAlignment.Left)
		Lv_RoleButton.View = View.Details

	End Sub

	Private Sub GetDataLvMenu(ByVal index As Integer)
		LvMenu_Kosong = Lv_Role.Items(index).SubItems(itemMenu_Kosong).Text
		LvMenu_Title = Lv_Role.Items(index).SubItems(itemMenu_Title).Text
		LvMenu_MenuName = Lv_Role.Items(index).SubItems(itemMenu_MenuName).Text
		LvMenu_SubMenuName = Lv_Role.Items(index).SubItems(itemMenu_SubMenuName).Text
		LvMenu_SubMenuLv1Name = Lv_Role.Items(index).SubItems(itemMenu_SubMenuName1).Text
		LvMenu_SubMenuLv2Name = Lv_Role.Items(index).SubItems(itemMenu_SubMenuName2).Text
		LvMenu_SubMenuLv3Name = Lv_Role.Items(index).SubItems(itemMenu_SubMenuName3).Text
		LvMenu_MainMenuID = Lv_Role.Items(index).SubItems(itemMenu_MainMenuID).Text
		LvMenu_MenuID = Lv_Role.Items(index).SubItems(itemMenu_MenuID).Text
		LvMenu_SubMenuID = Lv_Role.Items(index).SubItems(itemMenu_SubMenuID).Text
		LvMenu_SubMenuLv1ID = Lv_Role.Items(index).SubItems(itemMenu_SubMenuLv1ID).Text
		LvMenu_SubMenuLv2ID = Lv_Role.Items(index).SubItems(itemMenu_SubMenuLv2ID).Text
		LvMenu_SubMenuLv3ID = Lv_Role.Items(index).SubItems(itemMenu_SubMenuLv3ID).Text
		LvMenu_Form = Lv_Role.Items(index).SubItems(itemMenu_Form).Text

	End Sub

	Private Sub GetDataRoleButton(ByVal index As Integer)
		LvRoleButton_Form = Lv_RoleButton.Items(index).SubItems(ItemRoleButton_Form).Text
		LvRoleButton_Button = Lv_RoleButton.Items(index).SubItems(ItemRoleButton_Button).Text
		LvRoleButton_Kategori = Lv_RoleButton.Items(index).SubItems(ItemRoleButton_Kategori).Text
		LvRoleButton_Keterangan = Lv_RoleButton.Items(index).SubItems(ItemRoleButton_Keterangan).Text
	End Sub

	Private Sub Lv_Role_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Role.SelectedIndexChanged
		If Lv_Role.Items.Count = 0 Then Exit Sub

		GetDataLvMenu(Lv_Role.FocusedItem.Index)

		Dim SelectedForm As String = LvMenu_Form
		FocusedForm = LvMenu_Form

		Try
			OpenConn()

			Cmb_Filter_RoleButton.SelectedIndex = -1
			Txt_Filter_RoleButton.Text = ""

			Chk_All_RoleButton.Checked = False
			isLoading = True
			Lv_RoleButton.BeginUpdate()
			Lv_RoleButton.Items.Clear() : CurrentButtonName.Clear()
			SQL = "select b.Form, a.ButtonName, a.Kategori, a.Keterangan, ISNULL(c.IsAccess, 'T') AS IsAccess "
			SQL = SQL & "from N_EMI_Master_Role_Button a "
			SQL = SQL & "inner join N_EMI_Master_Role_Button_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ButtonName = b.ButtonName "
			SQL = SQL & "left join ( "
			SQL = SQL & "select distinct ButtonName, 'Y' as IsAccess "
			SQL = SQL & "from Role_Button "
			SQL = SQL & "where UserID = '" & _UserID.Trim & "' "
			SQL = SQL & ") c ON a.ButtonName = c.ButtonName "
			SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and b.form = '" & SelectedForm & "' "
			SQL = SQL & "order by b.Form, a.Kategori, a.ButtonName "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_RoleButton.Items.Add(Dr("Kategori"))
					Lv.SubItems.Add(Dr("ButtonName"))
					Lv.SubItems.Add(Dr("Form"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan")) = "", "-", Dr("Keterangan")))

					If Dr("IsAccess") = "Y" Then
						Lv.Checked = True
						Lv.SubItems.Add("Y")
					Else
						Lv.SubItems.Add("T")
					End If

					CurrentButtonName.Add(Dr("ButtonName"))
				Loop
			End Using
			Lv_RoleButton.EndUpdate()
			isLoading = False

			If Lv_RoleButton.Items.Count > 0 Then
				Chk_All_RoleButton.Checked = (Lv_RoleButton.CheckedItems.Count = Lv_RoleButton.Items.Count)
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	'HANDLE BUTTON CLICK
	Private Sub Btn_Search_Click(sender As Object, e As EventArgs) Handles Btn_Search.Click
		If Cb_Users.SelectedIndex = -1 Then
			Exit Sub
		End If

		If Not String.IsNullOrWhiteSpace(arrUser.Item(Cb_Users.SelectedIndex)) Then
			LoadAllRoleMenus()

			Cmb_Reference.SelectedIndex = -1 : Cmb_Reference.Text = ""
		End If

	End Sub

	Private Sub Btn_GetReference_Click(sender As Object, e As EventArgs) Handles Btn_GetReference.Click

		If Cb_Users.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu User sebelum memilih Referensi", "Role Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Reference.SelectedIndex = -1 : Cmb_Reference.Text = ""
			Exit Sub
		End If

		If Cmb_Reference.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu Referensi", "Role Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		LoadAllRoleMenus(arrUserReference(Cmb_Reference.SelectedIndex))

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

#Region "METODE LAMA"

			'For i As Integer = 0 To Lv_Role.Items.Count - 1
			'	If Lv_Role.Items(i).Checked = True Then

			'		If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuLv3IDRole).Text) Then
			'			Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuLv3IDRole).Text

			'			'Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
			'			'	DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
			'			'	DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

			'			Dim uniqueID As String = GenerateUniqueID(i, 6)

			'			If Not tmp_submenu3.Contains(Data) Then
			'				SQL = "insert into RoleSubMenuLv3 (RoleSubMenulv3ID, UserID, Kode_Perusahaan, SubMenuLv3ID) "
			'				SQL = SQL & "values ('RoleSL3_" & uniqueID & "', '" & _UserID & "', "
			'				SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

			'				ExecuteTrans(SQL)

			'				tmp_mainmenu.Add(Data)
			'			End If

			'		End If

			'		If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuLv2IDRole).Text) Then
			'			Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuLv2IDRole).Text

			'			'Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
			'			'	DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
			'			'	DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

			'			Dim uniqueID As String = GenerateUniqueID(i, 5)

			'			If Not tmp_submenu2.Contains(Data) Then
			'				SQL = "insert into RoleSubMenuLv2 (RoleSubMenulv2ID, UserID, Kode_Perusahaan, SubMenuLv2ID) "
			'				SQL = SQL & "values ('RoleSL2_" & uniqueID & "', '" & _UserID & "', "
			'				SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

			'				ExecuteTrans(SQL)

			'				tmp_submenu2.Add(Data)

			'			End If

			'		End If

			'		If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuLv1IDRole).Text) Then
			'			Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuLv1IDRole).Text

			'			'Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
			'			'	DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
			'			'	DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

			'			Dim uniqueID As String = GenerateUniqueID(i, 4)

			'			If Not tmp_submenu1.Contains(Data) Then
			'				SQL = "insert into RoleSubMenuLv1 (RoleSubMenulv1ID, UserID, Kode_Perusahaan, SubMenuLv1ID) "
			'				SQL = SQL & "values ('RoleSL1_" & uniqueID & "', '" & _UserID & "', "
			'				SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

			'				ExecuteTrans(SQL)

			'				tmp_submenu1.Add(Data)

			'			End If

			'		End If

			'		If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemSubMenuIDRole).Text) Then
			'			Dim Data As String = Lv_Role.Items(i).SubItems(itemSubMenuIDRole).Text

			'			'Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
			'			'	DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
			'			'	DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

			'			Dim uniqueID As String = GenerateUniqueID(i, 3)

			'			If Not tmp_submenu.Contains(Data) Then

			'				SQL = "insert into RoleSubMenu (RoleSubMenuID, UserID, Kode_Perusahaan, SubMenuID) "
			'				SQL = SQL & "values ('RoleS_" & uniqueID & "', '" & _UserID & "', "
			'				SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

			'				ExecuteTrans(SQL)

			'				tmp_submenu.Add(Data)

			'			End If

			'		End If

			'		If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemMenuIDRole).Text) Then
			'			Dim Data As String = Lv_Role.Items(i).SubItems(itemMenuIDRole).Text

			'			'Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
			'			'	DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
			'			'	DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

			'			Dim uniqueID As String = GenerateUniqueID(i, 2)

			'			If Not tmp_menu.Contains(Data) Then
			'				SQL = "insert into RoleMenus (RoleMenuID, UserID, Kode_Perusahaan, MenuID) "
			'				SQL = SQL & "values ('RoleM_" & uniqueID & "', '" & _UserID & "', "
			'				SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

			'				ExecuteTrans(SQL)

			'				tmp_menu.Add(Data)

			'			End If

			'		End If

			'		If Not String.IsNullOrWhiteSpace(Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text) Then
			'			Dim Data As String = Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text

			'			'Dim uniqueID As String = DateTime.Now.Millisecond.ToString("D3") &
			'			'	DateTime.Now.Second.ToString("D2") & DateTime.Now.Hour.ToString("D2") &
			'			'	DateTime.Now.Day.ToString("D2") & DateTime.Now.Month.ToString("D1")

			'			Dim uniqueID As String = GenerateUniqueID(i, 1)

			'			If Not tmp_mainmenu.Contains(Data) Then
			'				SQL = "insert into RoleMainMenus (RoleMainMenuID, UserID, Kode_Perusahaan, MainMenuID) "
			'				SQL = SQL & "values ('RoleMM_" & uniqueID & "', '" & _UserID & "', "
			'				SQL = SQL & "'" & KodePerusahaan & "', '" & Data & "')"

			'				ExecuteTrans(SQL)

			'				tmp_mainmenu.Add(Data)
			'			End If

			'		End If

			'	End If
			'Next

#End Region

#Region "METODE BARU DENGAN BULK INSERT"

			'===================================================
			'=     MENYIAPKAN DATATABLE MASING MASING ROLE     =
			'===================================================
			Dim dtSL3 As New DataTable : dtSL3.Columns.Add("RoleSubMenuLv3ID") : dtSL3.Columns.Add("UserID") : dtSL3.Columns.Add("Kode_Perusahaan") : dtSL3.Columns.Add("SubMenuLv3ID")
			Dim dtSL2 As New DataTable : dtSL2.Columns.Add("RoleSubMenuLv2ID") : dtSL2.Columns.Add("UserID") : dtSL2.Columns.Add("Kode_Perusahaan") : dtSL2.Columns.Add("SubMenuLv2ID")
			Dim dtSL1 As New DataTable : dtSL1.Columns.Add("RoleSubMenuLv1ID") : dtSL1.Columns.Add("UserID") : dtSL1.Columns.Add("Kode_Perusahaan") : dtSL1.Columns.Add("SubMenuLv1ID")
			Dim dtS As New DataTable : dtS.Columns.Add("RoleSubMenuID") : dtS.Columns.Add("UserID") : dtS.Columns.Add("Kode_Perusahaan") : dtS.Columns.Add("SubMenuID")
			Dim dtM As New DataTable : dtM.Columns.Add("RoleMenuID") : dtM.Columns.Add("UserID") : dtM.Columns.Add("Kode_Perusahaan") : dtM.Columns.Add("MenuID")
			Dim dtMM As New DataTable : dtMM.Columns.Add("RoleMainMenuID") : dtMM.Columns.Add("UserID") : dtMM.Columns.Add("Kode_Perusahaan") : dtMM.Columns.Add("MainMenuID")

			Dim listMenuUnCheck As New List(Of String)

			'==============================
			'=     LOOP ISI DATATABLE     =
			'==============================
			For i As Integer = 0 To Lv_Role.Items.Count - 1
				If Lv_Role.Items(i).Checked Then

					Dim dataL3 As String = Lv_Role.Items(i).SubItems(itemSubMenuLv3IDRole).Text
					If Not String.IsNullOrWhiteSpace(dataL3) AndAlso Not tmp_submenu3.Contains(dataL3) Then
						dtSL3.Rows.Add("RoleSL3_" & GenerateUniqueID(i, 6, 1), _UserID, KodePerusahaan, dataL3)
						tmp_submenu3.Add(dataL3)
					End If

					Dim dataL2 As String = Lv_Role.Items(i).SubItems(itemSubMenuLv2IDRole).Text
					If Not String.IsNullOrWhiteSpace(dataL2) AndAlso Not tmp_submenu2.Contains(dataL2) Then
						dtSL2.Rows.Add("RoleSL2_" & GenerateUniqueID(i, 5, 1), _UserID, KodePerusahaan, dataL2)
						tmp_submenu2.Add(dataL2)
					End If

					Dim dataL1 As String = Lv_Role.Items(i).SubItems(itemSubMenuLv1IDRole).Text
					If Not String.IsNullOrWhiteSpace(dataL1) AndAlso Not tmp_submenu1.Contains(dataL1) Then
						dtSL1.Rows.Add("RoleSL1_" & GenerateUniqueID(i, 4, 1), _UserID, KodePerusahaan, dataL1)
						tmp_submenu1.Add(dataL1)
					End If

					Dim dataSub As String = Lv_Role.Items(i).SubItems(itemSubMenuIDRole).Text
					If Not String.IsNullOrWhiteSpace(dataSub) AndAlso Not tmp_submenu.Contains(dataSub) Then
						dtS.Rows.Add("RoleS_" & GenerateUniqueID(i, 3, 1), _UserID, KodePerusahaan, dataSub)
						tmp_submenu.Add(dataSub)
					End If

					Dim dataMenu As String = Lv_Role.Items(i).SubItems(itemMenuIDRole).Text
					If Not String.IsNullOrWhiteSpace(dataMenu) AndAlso Not tmp_menu.Contains(dataMenu) Then
						dtM.Rows.Add("RoleM_" & GenerateUniqueID(i, 2, 1), _UserID, KodePerusahaan, dataMenu)
						tmp_menu.Add(dataMenu)
					End If

					Dim dataMainMenu As String = Lv_Role.Items(i).SubItems(itemMainMenuIDRole).Text
					If Not String.IsNullOrWhiteSpace(dataMainMenu) AndAlso Not tmp_mainmenu.Contains(dataMainMenu) Then
						dtMM.Rows.Add("RoleMM_" & GenerateUniqueID(i, 1, 1), _UserID, KodePerusahaan, dataMainMenu)
						tmp_mainmenu.Add(dataMainMenu)
					End If
				Else
					listMenuUnCheck.Add(Lv_Role.Items(i).SubItems(itemMenu_Form).Text)
				End If
			Next

			'========================================================
			'=     HAPUS ROLE BUTTON UNTUK MENU YANG DI UNCHECK     =
			'========================================================
			If listMenuUnCheck.Count <> 0 Then
				Dim MenusToDelete As String = $"'{String.Join("', '", listMenuUnCheck)}'"

				Dim ListMenuButton As New List(Of String)
				SQL = "select distinct ButtonName from N_EMI_Master_Role_Button_Detail "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Form in (" & MenusToDelete & ") "
				Using dr = OpenTrans(SQL)
					Do While dr.Read
						ListMenuButton.Add(dr("ButtonName"))
					Loop
				End Using

				Dim MenuButtonToDelete As String = $"'{String.Join("', '", ListMenuButton)}'"
				SQL = "delete from Role_Button "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and UserID = '" & _UserID & "' "
				SQL = SQL & "and ButtonName in (" & MenuButtonToDelete & ") "
				ExecuteTrans(SQL)
			End If

			''=========================
			''=     EKSEKUSI BULK     =
			''=========================
			ExecuteBulkInsert(dtMM, "RoleMainMenus", Cn, Cmd.Transaction)
			ExecuteBulkInsert(dtM, "RoleMenus", Cn, Cmd.Transaction)
			ExecuteBulkInsert(dtS, "RoleSubMenu", Cn, Cmd.Transaction)
			ExecuteBulkInsert(dtSL1, "RoleSubMenuLv1", Cn, Cmd.Transaction)
			ExecuteBulkInsert(dtSL2, "RoleSubMenuLv2", Cn, Cmd.Transaction)
			ExecuteBulkInsert(dtSL3, "RoleSubMenuLv3", Cn, Cmd.Transaction)

#End Region

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Role Berhasil Disimpan", "Role Menu", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		kosong()

	End Sub

	Private Sub Btn_SaveRoleButton_Click(sender As Object, e As EventArgs) Handles Btn_SaveRoleButton.Click
		If Lv_RoleButton.Items.Count = 0 Or String.IsNullOrEmpty(FocusedForm) Then
			MessageBox.Show("Tidak ada Role Button yang dapat disimpan", "Role Button", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If CurrentButtonName.Count = 0 Then
			MessageBox.Show("Terjadi kesalahan pada data Button", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show("Apakah Anda yakin ingin menyimpan Role Button untuk Form " & FocusedForm & "?", "Role Button", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
			Exit Sub
		End If

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim CurrentButton As String = $"'{String.Join("', '", CurrentButtonName)}'"

			'==============================
			'=     DELETE ROLE BUTTON     =
			'==============================
			SQL = "delete from Role_Button "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and UserID = '" & _UserID & "' "
			SQL = SQL & "and ButtonName in (" & CurrentButton & ") "
			ExecuteTrans(SQL)

			'=====================================================
			'=     REINSERT BUTTON DENGAN METODE BULK INSERT     =
			'=====================================================
			Dim Dt_Bulk As New DataTable
			Dt_Bulk.Columns.Add("Kode_Perusahaan", GetType(String))
			Dt_Bulk.Columns.Add("UserID", GetType(String))
			Dt_Bulk.Columns.Add("ButtonName", GetType(String))

			For Each Data As ListViewItem In Lv_RoleButton.Items
				If Data.Checked = False Then Continue For

				Dim DataRow As DataRow = Dt_Bulk.NewRow
				DataRow("Kode_Perusahaan") = KodePerusahaan
				DataRow("UserID") = _UserID
				DataRow("ButtonName") = Data.SubItems(ItemRoleButton_Button).Text
				Dt_Bulk.Rows.Add(DataRow)
			Next

			ExecuteBulkInsert(Dt_Bulk, "Role_Button", Cn, Cmd.Transaction)

			'If True Then
			'	CloseTrans()
			'	CloseConn()
			'	Exit Sub
			'End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Role Button Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If Lv_Role.Items.Count > 0 Then
			Lv_Role.Items(Lv_Role.FocusedItem.Index).Selected = True
			Lv_Role.Select()
		End If

	End Sub

	'HANDLE LOAD ROLE MENUS
	Private Sub LoadAllRoleMenus(ByVal Optional UserIDReference As String = "")
		If String.IsNullOrWhiteSpace(_UserID) Then Exit Sub

		Try
			OpenConn()

			FocusedForm = ""

			Dim SQL As String
			Lv_Role.Items.Clear()
			SQL = "WITH CTE_A AS ( "
			SQL = SQL & "SELECT a.MainMenuID, a.TItle, b.MenuID, b.MenuName, c.SubMenuID, c.SubMenuName, d.SubMenuLv1ID, d.SubMenuLv1Name, e.SubMenuLv2ID, e.SubMenuLv2Name, f.SubMenuLv3ID, f.SubMenuLv3Name, "
			SQL = SQL & "a.urut, b.MenuOrder, c.SubMenuOrder, d.SubMenuLv1Order, e.SubMenuLv2Order, f.SubMenuLv3Order, "
			SQL = SQL & "CASE "
			SQL = SQL & "WHEN f.SubMenuLv3Name IS NOT NULL THEN f.form "
			SQL = SQL & "WHEN e.SubMenuLv2Name IS NOT NULL THEN e.form "
			SQL = SQL & "WHEN d.SubMenuLv1Name IS NOT NULL THEN d.form "
			SQL = SQL & "WHEN c.SubMenuName IS NOT NULL THEN c.form "
			SQL = SQL & "ELSE '-' END AS form "
			SQL = SQL & "FROM MainMenu a "
			SQL = SQL & "LEFT JOIN Menus b ON a.MainMenuID = b.MainMenuID "
			SQL = SQL & "LEFT JOIN SubMenus c ON b.MenuID = c.MenuID "
			SQL = SQL & "LEFT JOIN SubMenuLv1 d ON d.SubMenuID = c.SubMenuID "
			SQL = SQL & "LEFT JOIN SubMenuLv2 e ON e.SubMenuLv1ID = d.SubMenuLv1ID "
			SQL = SQL & "LEFT JOIN SubMenuLv3 f ON f.SubMenuLv2ID = e.SubMenuLv2ID "
			SQL = SQL & ") "
			SQL = SQL & "SELECT a.MainMenuID, a.TItle, a.MenuID, a.MenuName, a.SubMenuID, a.SubMenuName, a.SubMenuLv1ID, a.SubMenuLv1Name, a.SubMenuLv2ID, a.SubMenuLv2Name, a.SubMenuLv3ID, a.SubMenuLv3Name, "
			SQL = SQL & "a.urut, a.MenuOrder, a.SubMenuOrder, a.SubMenuLv1Order, a.SubMenuLv2Order, a.SubMenuLv3Order, "
			SQL = SQL & "CASE "
			SQL = SQL & "WHEN a.SubMenuLv3ID IS NOT NULL THEN CASE WHEN g.RoleSubMenuLv3ID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
			SQL = SQL & "WHEN a.SubMenuLv2ID IS NOT NULL THEN CASE WHEN f.RoleSubMenuLv2ID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
			SQL = SQL & "WHEN a.SubMenuLv1ID IS NOT NULL THEN CASE WHEN e.RoleSubMenuLv1ID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
			SQL = SQL & "WHEN a.SubMenuID IS NOT NULL THEN CASE WHEN d.RoleSubMenuID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
			SQL = SQL & "WHEN a.MenuID IS NOT NULL THEN CASE WHEN c.RoleMenuID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
			SQL = SQL & "ELSE CASE WHEN b.RoleMainMenuID IS NOT NULL THEN 'Access' ELSE 'Not Access' END "
			SQL = SQL & "END AS StatusAccess, form "
			SQL = SQL & "FROM CTE_A a "
			SQL = SQL & "LEFT JOIN RoleMainMenus b ON a.MainMenuID = b.MainMenuID AND b.UserID = '" & If(UserIDReference = "", _UserID, UserIDReference) & "' "
			SQL = SQL & "LEFT JOIN RoleMenus c ON a.MenuID = c.MenuID AND c.UserID = '" & If(UserIDReference = "", _UserID, UserIDReference) & "' "
			SQL = SQL & "LEFT JOIN RoleSubMenu d ON a.SubMenuID = d.SubMenuID AND d.UserID = '" & If(UserIDReference = "", _UserID, UserIDReference) & "' "
			SQL = SQL & "LEFT JOIN RoleSubMenuLv1 e ON a.SubMenuLv1ID = e.SubMenuLv1ID AND e.UserID = '" & If(UserIDReference = "", _UserID, UserIDReference) & "' "
			SQL = SQL & "LEFT JOIN RoleSubMenuLv2 f ON a.SubMenuLv2ID = f.SubMenuLv2ID AND f.UserID = '" & If(UserIDReference = "", _UserID, UserIDReference) & "' "
			SQL = SQL & "LEFT JOIN RoleSubMenuLv3 g ON a.SubMenuLv3ID = g.SubMenuLv3ID AND g.UserID = '" & If(UserIDReference = "", _UserID, UserIDReference) & "' "
			SQL = SQL & "order by a.title, a.MenuID, a.MenuOrder, a.submenuname, a.submenulv1name, a.submenulv2name, a.submenulv3name "
			'SQL = SQL & "ORDER BY a.MainMenuID, a.urut, a.MenuID, a.MenuOrder, a.SubMenuID, a.SubMenuOrder, a.SubMenuLv1ID, a.SubMenuLv1Order, a.SubMenuLv2ID, a.SubMenuLv2Order, a.SubMenuLv3ID, a.SubMenuLv3Order "
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
					LV.SubItems.Add(General_Class.CekNULL(dr("form")))

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

		Try
			OpenConn()

			isLoading = True
			Cmb_Filter_RoleButton.SelectedIndex = -1 : Txt_Filter_RoleButton.Text = ""
			Chk_All_RoleButton.Checked = False
			Lv_RoleButton.Items.Clear() : CurrentButtonName.Clear()
			isLoading = False

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

	Private Sub Get_Data_Lv_Checked()
		Dim tampung As New ArrayList
		kosongArr()
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

	Private Sub Btn_Cari_RoleButton_Click(sender As Object, e As EventArgs) Handles Btn_Cari_RoleButton.Click
		'If Lv_RoleButton.Items.Count = 0 Then Exit Sub

		If Cmb_Filter_RoleButton.SelectedIndex = -1 Then
			MessageBox.Show("Pilih dahulu kategori filter", "Filter Role Button", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Filter_RoleButton.DroppedDown = True
			Cmb_Filter_RoleButton.Focus()
			Exit Sub
		Else
			'If Txt_Filter_RoleButton.Text.Trim.Length = 0 Then
			'	MessageBox.Show("Value filter tidak boleh kosong", "Filter Role Button", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'	Txt_Filter_RoleButton.Focus()
			'	Exit Sub
			'End If
		End If

		GetDataLvMenu(Lv_Role.FocusedItem.Index)
		Dim SelectedForm As String = LvMenu_Form
		If String.IsNullOrWhiteSpace(SelectedForm) Then
			Exit Sub
		End If

		Try
			OpenConn()

			Chk_All_RoleButton.Checked = False
			isLoading = True
			Lv_RoleButton.BeginUpdate()
			Lv_RoleButton.Items.Clear() : CurrentButtonName.Clear()
			SQL = "select b.Form, a.ButtonName, a.Kategori, a.Keterangan, ISNULL(c.IsAccess, 'T') AS IsAccess "
			SQL = SQL & "from N_EMI_Master_Role_Button a "
			SQL = SQL & "inner join N_EMI_Master_Role_Button_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ButtonName = b.ButtonName "
			SQL = SQL & "left join ( "
			SQL = SQL & "select distinct ButtonName, 'Y' as IsAccess "
			SQL = SQL & "from Role_Button "
			SQL = SQL & "where UserID = '" & _UserID.Trim & "' "
			SQL = SQL & ") c ON a.ButtonName = c.ButtonName "
			SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and b.form = '" & SelectedForm & "' "

			If Cmb_Filter_RoleButton.SelectedIndex <> -1 And Txt_Filter_RoleButton.Text.Trim.Length > 0 Then
				SQL = SQL & "and " & arrFilterRole(Cmb_Filter_RoleButton.SelectedIndex) & " like '%" & Txt_Filter_RoleButton.Text.Trim & "%' "
			End If

			SQL = SQL & "order by b.Form, a.Kategori, a.ButtonName "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_RoleButton.Items.Add(Dr("Kategori"))
					Lv.SubItems.Add(Dr("ButtonName"))
					Lv.SubItems.Add(Dr("Form"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan")) = "", "-", Dr("Keterangan")))

					If Dr("IsAccess") = "Y" Then
						Lv.Checked = True
						Lv.SubItems.Add("Y")
					Else
						Lv.SubItems.Add("T")
					End If

					CurrentButtonName.Add(Dr("ButtonName"))
				Loop
			End Using
			Lv_RoleButton.EndUpdate()
			isLoading = False

			If Lv_RoleButton.Items.Count > 0 Then
				Chk_All_RoleButton.Checked = (Lv_RoleButton.CheckedItems.Count = Lv_RoleButton.Items.Count)
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
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

	Private Sub Chk_All_RoleButton_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_All_RoleButton.CheckedChanged
		If isLoading Then Exit Sub

		'===========================================
		'=     CEK APAKAH MENU SEDANG DI CHECK     =
		'===========================================
		If Lv_Role.FocusedItem Is Nothing OrElse Not Lv_Role.FocusedItem.Checked Then
			Exit Sub
		End If

		isLoading = True
		Lv_RoleButton.BeginUpdate()
		For Each item As ListViewItem In Lv_RoleButton.Items
			item.Checked = Chk_All_RoleButton.Checked
		Next
		Lv_RoleButton.EndUpdate()
		isLoading = False
	End Sub

	Private Sub Lv_RoleButton_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles Lv_RoleButton.ItemChecked
		If isLoading Then Exit Sub

		'===========================================
		'=     CEK APAKAH MENU SEDANG DI CHECK     =
		'===========================================
		If Lv_Role.FocusedItem Is Nothing OrElse Not Lv_Role.FocusedItem.Checked Then
			isLoading = True
			e.Item.Checked = False
			isLoading = False
			Return
		End If

		isLoading = True
		Chk_All_RoleButton.Checked = (Lv_RoleButton.CheckedItems.Count = Lv_RoleButton.Items.Count)
		isLoading = False
	End Sub

	Private Sub Lv_Role_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles Lv_Role.DrawItem
		e.DrawDefault = True
	End Sub

	Private Sub Lv_Role_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Role.DrawSubItem
		e.DrawDefault = True
	End Sub

	Public Function GenerateUniqueID(ByVal UrutMenu As Integer, ByVal LevelMenu As Integer, ByVal indexUser As Integer) As String
		Return DateTime.Now.ToString("ddHHmm") & UrutMenu & LevelMenu & indexUser
	End Function

	Public Sub ExecuteBulkInsert(dtSource As DataTable, destinationTable As String, conn As SqlConnection, trans As SqlTransaction)

		If dtSource Is Nothing OrElse dtSource.Rows.Count = 0 Then
			Exit Sub
			'Throw New Exception("Terjadi kesalahan, Data yang akan disimpan tidak ada")
		End If

		Using bulkCopy As New SqlBulkCopy(conn, SqlBulkCopyOptions.Default, trans)
			bulkCopy.DestinationTableName = destinationTable
			bulkCopy.BatchSize = 1000 ' limit data per batch untuk menghindari timeout. 1 kali execute hanya 1000 data yang dikirim ke SQL Server

			'Mapping kolom secara otomatis berdasarkan nama kolom (Penting: Nama kolom di DataTable harus SAMA dengan di Tabel SQL)
			For Each col As DataColumn In dtSource.Columns
				bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName)
			Next

			bulkCopy.WriteToServer(dtSource)
		End Using
	End Sub

	Private Sub Lv_Users_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Role.MouseMove, Lv_RoleButton.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	Private Sub EnableDoubleBuffer(lvw As ListView)
		Dim t As Type = lvw.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(lvw, True, Nothing)
	End Sub

	Private Sub HandleListViewHover(lvw As ListView, e As MouseEventArgs)
		Dim hit As ListViewHitTestInfo = lvw.HitTest(e.Location)

		lvw.Cursor = If(hit.Item IsNot Nothing, Cursors.Hand, Cursors.Default)

		If hit.Item IsNot lastHoverItem Then
			lvw.BeginUpdate()
			If lastHoverItem IsNot Nothing Then
				lastHoverItem.BackColor = originalItemColor
			End If

			If hit.Item IsNot Nothing AndAlso hit.Item.Tag Is Nothing Then
				lastHoverItem = hit.Item
				originalItemColor = lastHoverItem.BackColor

				Dim amt As Integer = 15
				lastHoverItem.BackColor = Color.FromArgb(
				Math.Max(0, originalItemColor.R - amt),
				Math.Max(0, originalItemColor.G - amt),
				Math.Max(0, originalItemColor.B - amt)
			)
			Else
				lastHoverItem = Nothing
			End If

			lvw.EndUpdate()
		End If

	End Sub

End Class