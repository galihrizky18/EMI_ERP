Imports System.Runtime.InteropServices

Public Class N_EMI_Master_Approval_Hierarchy_Waste

	Private WithEvents TypingTimer As New Timer()

	Dim Lv_Review_Username, Lv_Review_Peran, Lv_Review_Jabatan, Lv_Review_No_HP, Lv_Review_Id_User, Lv_Review_Id_User_Koneksi As String

	Dim Item_Review_Username As Integer = 0
	Dim Item_Review_Peran As Integer = 1
	Dim item_Review_Jabatan As Integer = 2
	Dim item_Review_No_HP As Integer = 3
	Dim item_Review_Id_User As Integer = 4
	Dim item_Review_Id_User_Koneksi As Integer = 5

	Dim arrJenisApproval, arrLokasi, arrPeran, arrId_User_Koneksi As New ArrayList

	Dim switchAutoComplete As Boolean = False
	Dim SelectedUserID As String = ""

	Private Sub N_EMI_Master_Approval_Hierarchy_Waste_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		'===================================================
		'=     MENERAPKAN KONSEP DEBOUNCE UNTUK FILTER     =
		'===================================================
		TypingTimer.Interval = 500

		SetCueBanner(Txt_Filter_Lokasi_Pengajuan, "Filter Lokasi")
		SetCueBanner(Txt_Filter_Lokasi_Pemindahan, "Filter Lokasi")

		Lv_User.Columns.Clear()
		Lv_User.Columns.Add("UserID", 0, HorizontalAlignment.Left)
		Lv_User.Columns.Add("Username", 350, HorizontalAlignment.Left)
		Lv_User.View = View.Details

		Cmb_Jenis_Approval.Items.Clear() : arrJenisApproval.Clear()
		Cmb_Jenis_Approval.Items.Add("Pengajuan Pemusnahan Waste") : arrJenisApproval.Add("Waste_Process")
		Cmb_Jenis_Approval.Items.Add("Pemindahan Waste") : arrJenisApproval.Add("Waste_Produk")

		Lv_DetaiL_Pengajuan.Columns.Clear()
		Lv_DetaiL_Pengajuan.Columns.Add("UserID", 0, HorizontalAlignment.Left) ' Hide
		Lv_DetaiL_Pengajuan.Columns.Add("Level", 70, HorizontalAlignment.Center)
		Lv_DetaiL_Pengajuan.Columns.Add("Peran", 100, HorizontalAlignment.Center)
		Lv_DetaiL_Pengajuan.Columns.Add("Jabatan", 170, HorizontalAlignment.Left)
		Lv_DetaiL_Pengajuan.Columns.Add("Username", 150, HorizontalAlignment.Left)
		Lv_DetaiL_Pengajuan.Columns.Add("No HP", 150, HorizontalAlignment.Left)
		Lv_DetaiL_Pengajuan.Columns.Add("Username Koneksi", 150, HorizontalAlignment.Left)
		Lv_DetaiL_Pengajuan.View = View.Details

		Lv_DetaiL_Pemindahan.Columns.Clear()
		Lv_DetaiL_Pemindahan.Columns.Add("UserID", 0, HorizontalAlignment.Left) ' Hide
		Lv_DetaiL_Pemindahan.Columns.Add("Level", 70, HorizontalAlignment.Center)
		Lv_DetaiL_Pemindahan.Columns.Add("Peran", 100, HorizontalAlignment.Center)
		Lv_DetaiL_Pemindahan.Columns.Add("Jabatan", 170, HorizontalAlignment.Left)
		Lv_DetaiL_Pemindahan.Columns.Add("Username", 150, HorizontalAlignment.Left)
		Lv_DetaiL_Pemindahan.Columns.Add("No HP", 150, HorizontalAlignment.Left)
		Lv_DetaiL_Pemindahan.Columns.Add("Username Koneksi", 150, HorizontalAlignment.Left)
		Lv_DetaiL_Pemindahan.View = View.Details

		Lv_Review.Columns.Clear()
		Lv_Review.Columns.Add("Username", 130, HorizontalAlignment.Left)
		Lv_Review.Columns.Add("Peran", 170, HorizontalAlignment.Left)
		Lv_Review.Columns.Add("Jabatan", 180, HorizontalAlignment.Left)
		Lv_Review.Columns.Add("No HP", 180, HorizontalAlignment.Left)
		'Hide
		Lv_Review.Columns.Add("Id_User", 0, HorizontalAlignment.Left)
		Lv_Review.Columns.Add("Id_User_Koneksi", 0, HorizontalAlignment.Left)
		Lv_Review.View = View.Details

		Try
			OpenConn()

			Cmb_Lokasi.Items.Clear() : arrLokasi.Clear()
			SQL = $"select Kode_Stock_Owner from stock_owner_gudang where kode_perusahaan = '{KodePerusahaan}' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Lokasi.Items.Add(Dr("Kode_Stock_Owner")) : arrLokasi.Add(Dr("Kode_Stock_Owner"))
				Loop
			End Using

			Cmb_Peran.Items.Clear() : arrPeran.Clear()
			SQL = $"select id, peran from N_EMI_Master_Peran_Approval where kode_perusahaan = '" & KodePerusahaan & "' order by id "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Peran.Items.Add(Dr("peran")) : arrPeran.Add(Dr("id"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()
		Kosong_Tab_Review()

		TabControl1.SelectedIndex = 0

	End Sub

	Private Sub Get_Data_Review(ByVal index As Integer)
		Lv_Review_Username = Lv_Review.Items(index).SubItems(Item_Review_Username).Text
		Lv_Review_Peran = Lv_Review.Items(index).SubItems(Item_Review_Peran).Text
		Lv_Review_Jabatan = Lv_Review.Items(index).SubItems(item_Review_Jabatan).Text
		Lv_Review_No_HP = Lv_Review.Items(index).SubItems(item_Review_No_HP).Text
		Lv_Review_Id_User = Lv_Review.Items(index).SubItems(item_Review_Id_User).Text
		Lv_Review_Id_User_Koneksi = Lv_Review.Items(index).SubItems(item_Review_Id_User_Koneksi).Text
	End Sub

	Private Sub Kosong()

		Cmb_Jenis_Approval.SelectedIndex = -1
		Cmb_Approval_Level.SelectedIndex = -1
		Cmb_Lokasi.SelectedIndex = -1
		Cmb_Peran.SelectedIndex = -1

		Cmb_User_Koneksi.Items.Clear()
		Cmb_User_Koneksi.SelectedIndex = -1

		Cmb_Approval_Level.Text = ""
		Cmb_Lokasi.Text = ""
		Cmb_Peran.Text = ""

		switchAutoComplete = True
		Txt_Username.Text = ""
		Txt_Selected_UserID.Text = ""
		Txt_Selected_Approval_Level.Text = ""
		Txt_Jabatan.Text = ""
		Txt_No_HP.Text = ""
		SelectedUserID = ""
		Txt_Update_User_ID.Text = ""
		switchAutoComplete = False

		Cmb_Jenis_Approval.Enabled = True
		Cmb_Lokasi.Enabled = False
		Cmb_Approval_Level.Enabled = False
		Txt_Username.Enabled = False
		Txt_Selected_UserID.Enabled = False
		Cmb_Peran.Enabled = False
		Txt_Jabatan.Enabled = False
		Txt_No_HP.Enabled = False

		Cb_Flag_Koneksi.Checked = False

		Btn_Delete.Visible = False

		arrId_User_Koneksi.Clear()

		Btn_Tambah.Tag = "TAMBAH"
		Btn_Tambah.Text = "&Tambah"

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click

		If Lv_Review.Items.Count <> 0 Then
			Disable_Panel_Input()
		Else
			Kosong()
		End If

	End Sub

	Private Sub Btn_Refresh_Panel_Review_Click(sender As Object, e As EventArgs) Handles Btn_Refresh_Panel_Review.Click
		Kosong_Tab_Review()
		Kosong()
	End Sub

	Private Sub Cmb_Jenis_Approval_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Approval.SelectedIndexChanged
		If Cmb_Jenis_Approval.SelectedIndex <> -1 Then
			Cmb_Lokasi.Enabled = True
			Cmb_Lokasi.Focus()
		Else
			Cmb_Lokasi.Enabled = False
			Cmb_Lokasi.Focus()
		End If
	End Sub

	Private Sub Cmb_Lokasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lokasi.SelectedIndexChanged

		If Cmb_Lokasi.SelectedIndex <> -1 Then

			Dim MaxApproveLevel As Double = 0
			Try
				OpenConn()

				SQL = "SELECT max(Approval_Level) as Max "
				SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste "
				SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
				SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
				SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						If General_Class.CekNULL(Dr("Max")) <> "" Then
							MaxApproveLevel = Val(HilangkanTanda(Dr("Max")))
						End If
					End If
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			Cmb_Approval_Level.Items.Clear()
			For i As Integer = 0 To MaxApproveLevel
				Cmb_Approval_Level.Items.Add(i + 1)
			Next

			Cmb_Approval_Level.SelectedIndex = MaxApproveLevel

			Cmb_Approval_Level.Enabled = True
			Txt_Username.Enabled = True
			Txt_Username.Text = ""
			Txt_Username.BackColor = Color.White
		Else
			Cmb_Approval_Level.Enabled = False
			Txt_Username.Enabled = False
			Txt_Username.Text = ""
			Txt_Username.BackColor = Color.FromArgb(235, 235, 235)

		End If

	End Sub

	Private Sub Txt_Username_TextChanged(sender As Object, e As EventArgs) Handles Txt_Username.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_Username.Text.Trim.Length = 0 Then
			Lv_User.Visible = False
			Lv_User.Location = New Point(750, 171)
			Txt_Username.Text = ""
			Txt_Selected_UserID.Text = ""
			Exit Sub
		Else
			Lv_User.Location = New Point(136, 171)
			Lv_User.Visible = True
		End If

		Try
			OpenConn()

			Lv_User.Items.Clear()

			If Cmb_Approval_Level.Text = "1" Then
				SQL = "select UserID as UserID, UserName from Users "
			Else
				SQL = "select id as UserID, username from Emi_Users "
			End If
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and username like '%{Txt_Username.Text}%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_User.Items.Add(Dr("UserID"))
					Lv.SubItems.Add(Dr("username"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Username_Leave(sender As Object, e As EventArgs) Handles Txt_Username.Leave
		If Txt_Username.Text.Trim.Length = 0 Then Exit Sub
		If Lv_User.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Cmb_Approval_Level.Text = "1" Then
				SQL = "select UserID as UserID, UserName from Users "
			Else
				SQL = "select id as UserID, username from Emi_Users "
			End If
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and username = '{Txt_Username.Text}' "
			Using Dr = Open(SQL)
				If Dr.Read Then
					Txt_Username.Text = Dr("UserName")
					Txt_Selected_UserID.Text = Dr("UserID")
					Cmb_Peran.Enabled = True
					Cmb_Peran.Focus()
				Else
					MessageBox.Show("Data User tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_Username.Text = ""
					Txt_Selected_UserID.Text = ""
					Txt_Username.Focus()
				End If

				Lv_User.Visible = False
				Lv_User.Location = New Point(750, 171)
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Username_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Username.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_Username.Text.Trim.Length = 0 Then Txt_Username.Focus()
			Txt_Username_Leave(Txt_Username, e)

			Lv_User.Visible = False
			Lv_User.Location = New Point(750, 171)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_Username_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Username.KeyDown
		If e.KeyCode = Keys.Down Then Lv_User.Focus()
	End Sub

	Private Sub Lv_User_DoubleClick(sender As Object, e As EventArgs) Handles Lv_User.DoubleClick
		If Lv_User.Items.Count = 0 Or Lv_User.FocusedItem.Index = -1 Then Exit Sub

		Dim UserID As String = Lv_User.FocusedItem.SubItems(0).Text
		Dim Username As String = Lv_User.FocusedItem.SubItems(1).Text

		switchAutoComplete = True
		Txt_Selected_UserID.Text = UserID
		Txt_Username.Text = Username
		switchAutoComplete = False

		Lv_User.Visible = False
		Lv_User.Location = New Point(750, 171)

		Cmb_Peran.Enabled = True
		'Cmb_Peran.SelectedIndex = -1
		Cmb_Peran.Focus()

		Txt_Username.Focus()
	End Sub

	Private Sub Lv_User_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_User.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_User_DoubleClick(Lv_User, e)
		End If
	End Sub

	Private Sub Cmb_Peran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Peran.SelectedIndexChanged
		If Cmb_Peran.SelectedIndex <> -1 Then
			Txt_Jabatan.Enabled = True
			Txt_No_HP.Enabled = True
			Txt_Jabatan.Focus()
			Txt_Jabatan.BackColor = Color.White
			Txt_No_HP.BackColor = Color.White
		Else
			Txt_Jabatan.Enabled = False
			Txt_No_HP.Enabled = False
			Txt_Jabatan.Focus()
			Txt_Jabatan.BackColor = Color.FromArgb(235, 235, 235)
			Txt_No_HP.BackColor = Color.FromArgb(235, 235, 235)
		End If
	End Sub

	Private Sub Kosong_Tab_Review()
		Lv_Review.Items.Clear()
	End Sub

	Private Sub Kosong_Tab_1()
		Txt_Filter_Lokasi_Pengajuan.Text = ""

		Load_Data_Tab1()
	End Sub

	Private Sub Kosong_Tab_2()
		Txt_Filter_Lokasi_Pemindahan.Text = ""

		Load_Data_Tab2()
	End Sub

	Private Sub Load_Data_Tab1()

		'Txt_Filter_Lokasi_Pengajuan.Clear()
		Txt_Jenis_Approval_Pengajuan.Text = ""
		Txt_Lokasi_Pengajuan.Text = ""
		Lv_DetaiL_Pengajuan.Items.Clear()

		Try
			OpenConn()

			Lv_Data_Pengajuan.Rows.Clear()
			SQL = "select distinct Kode_Stock_Owner "
			SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and Jenis_Approval = 'Waste_Process' "
			If Txt_Filter_Lokasi_Pengajuan.Text.Trim.Length <> 0 Then
				SQL &= $"and Kode_Stock_Owner like '%{Txt_Filter_Lokasi_Pengajuan.Text}%' "
			End If
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Lv_Data_Pengajuan.Rows.Add(1)
							Lv_Data_Pengajuan.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Stock_Owner")
						Next

						Lv_Data_Pengajuan.ClearSelection()
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

	Private Sub Lv_Data_Pengajuan_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Lv_Data_Pengajuan.CellClick
		If Lv_Data_Pengajuan.Rows.Count = 0 Then Exit Sub

		Try
			OpenConn()

			Dim SelectedLokasi As String = Lv_Data_Pengajuan.CurrentCell.Value

			Lv_DetaiL_Pengajuan.Items.Clear()
			SQL = "SELECT a.Jenis_Approval, a.Kode_Stock_Owner, a.Approval_Level, b.Peran, b.Jabatan, b.No_HP, "
			SQL &= $"b.ID_User_Desktop, b.ID_User_Android, isnull(c.UserName, '-') as Username_Desktop, b.ID_User_Android, isnull(d.username, '-') as Username_Android, "
			SQL &= $"isnull(b.ID_User_Koneksi, '-') as ID_User_Koneksi, isnull(e.UserName, '-')  as Username_Koneksi "
			SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
			SQL &= $"INNER JOIN N_EMI_Master_Hierarchy_Approval_Waste_user b ON a.Kode_Perusahaan = b.Kode_Perusahaan  "
			SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Approval_Level = b.Approval_Level "
			SQL &= $"LEFT JOIN users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.ID_User_Desktop = c.UserID "
			SQL &= $"LEFT JOIN Emi_Users d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.ID_User_Android = d.id "
			SQL &= $"LEFT JOIN users e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.ID_User_Koneksi = e.UserID "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Jenis_Approval = 'Waste_Process' "
			SQL &= $"and a.kode_stock_owner = '{SelectedLokasi}' "
			SQL &= $"order by a.Approval_Level, b.ID_User_Koneksi "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Txt_Jenis_Approval_Pengajuan.Text = "Pengajuan Pemusnahan Waste"
							Txt_Lokasi_Pengajuan.Text = .Rows(i).Item("Kode_Stock_Owner").ToString.Trim

							Dim UserID As String = ""
							Dim Username As String = ""
							If General_Class.CekNULL(.Rows(i).Item("ID_User_Desktop")) <> "0" Then
								UserID = .Rows(i).Item("ID_User_Desktop")
								Username = .Rows(i).Item("Username_Desktop")
							Else
								UserID = .Rows(i).Item("ID_User_Android")
								Username = .Rows(i).Item("Username_Android")
							End If
							Dim Lv As ListViewItem
							Lv = Lv_DetaiL_Pengajuan.Items.Add(UserID)
							Lv.SubItems.Add(.Rows(i).Item("Approval_Level"))
							Lv.SubItems.Add(.Rows(i).Item("Peran"))
							Lv.SubItems.Add(.Rows(i).Item("Jabatan"))
							Lv.SubItems.Add(Username)
							Lv.SubItems.Add(.Rows(i).Item("No_HP"))
							Lv.SubItems.Add(.Rows(i).Item("Username_Koneksi"))

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

	Private Sub Lv_Data_Pemindahan_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Lv_Data_Pemindahan.CellClick
		If Lv_Data_Pemindahan.Rows.Count = 0 Then Exit Sub

		Try
			OpenConn()

			Dim SelectedLokasi As String = Lv_Data_Pemindahan.CurrentCell.Value

			Lv_DetaiL_Pemindahan.Items.Clear()
			'SQL = "select a.Jenis_Approval, a.Kode_Stock_Owner, "
			'SQL &= $"a.Approval_Level, a.Peran, a.Jabatan, a.No_HP, a.ID_User_Desktop, b.UserName as Username_Desktop, a.ID_User_Android, c.username as Username_Android "
			'SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste a "
			'SQL &= $"left join users b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_User_Desktop = b.UserID and a.ID_User_Desktop is not NULL "
			'SQL &= $"left join Emi_Users c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_User_Android = c.id and a.ID_User_Android is not null "
			'SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			'SQL &= $"and a.Jenis_Approval = 'Waste_Produk' "
			'SQL &= $"and a.Kode_Stock_Owner = '{SelectedLokasi}' "
			'SQL &= $"order by a.Approval_Level "

			SQL = "SELECT a.Jenis_Approval, a.Kode_Stock_Owner, a.Approval_Level, b.Peran, b.Jabatan, b.No_HP, "
			SQL &= $"b.ID_User_Desktop, b.ID_User_Android, isnull(c.UserName, '-') as Username_Desktop, b.ID_User_Android, isnull(d.username, '-') as Username_Android, "
			SQL &= $"isnull(b.ID_User_Koneksi, '-') as ID_User_Koneksi, isnull(e.UserName, '-')  as Username_Koneksi "
			SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
			SQL &= $"INNER JOIN N_EMI_Master_Hierarchy_Approval_Waste_user b ON a.Kode_Perusahaan = b.Kode_Perusahaan  "
			SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Approval_Level = b.Approval_Level "
			SQL &= $"LEFT JOIN users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.ID_User_Desktop = c.UserID "
			SQL &= $"LEFT JOIN Emi_Users d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.ID_User_Android = d.id "
			SQL &= $"LEFT JOIN users e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.ID_User_Koneksi = e.UserID "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Jenis_Approval = 'Waste_Produk' "
			SQL &= $"and a.kode_stock_owner = '{SelectedLokasi}' "
			SQL &= $"order by a.Approval_Level, b.ID_User_Koneksi "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Txt_Jenis_Approval_Pemindahan.Text = "Pemindahan Pemusnahan Waste"
							Txt_Lokasi_Pemindahan.Text = .Rows(i).Item("Kode_Stock_Owner").ToString.Trim

							Dim UserID As String = ""
							Dim Username As String = ""
							If General_Class.CekNULL(.Rows(i).Item("ID_User_Desktop")) <> "0" Then
								UserID = .Rows(i).Item("ID_User_Desktop")
								Username = .Rows(i).Item("Username_Desktop")
							Else
								UserID = .Rows(i).Item("ID_User_Android")
								Username = .Rows(i).Item("Username_Android")
							End If
							Dim Lv As ListViewItem
							Lv = Lv_DetaiL_Pemindahan.Items.Add(UserID)
							Lv.SubItems.Add(.Rows(i).Item("Approval_Level"))
							Lv.SubItems.Add(.Rows(i).Item("Peran"))
							Lv.SubItems.Add(.Rows(i).Item("Jabatan"))
							Lv.SubItems.Add(Username)
							Lv.SubItems.Add(.Rows(i).Item("No_HP"))
							Lv.SubItems.Add(.Rows(i).Item("Username_Koneksi"))

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

	Private Sub Lv_DetaiL_Pengajuan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetaiL_Pengajuan.DoubleClick
		If Lv_DetaiL_Pengajuan.Items.Count = 0 Then Exit Sub

		Try
			OpenConn()

			Dim SelectedJenisApproval As String = "Waste_Process"
			Dim SelectedLokasi As String = Txt_Lokasi_Pengajuan.Text.Trim
			Dim SelectedApprovalLevel As String = Lv_DetaiL_Pengajuan.FocusedItem.SubItems(1).Text
			Dim UserID As String = Lv_DetaiL_Pengajuan.FocusedItem.SubItems(0).Text

			Cmb_Peran.Items.Clear() : arrPeran.Clear()
			SQL = $"select id, peran from N_EMI_Master_Peran_Approval where kode_perusahaan = '" & KodePerusahaan & "' order by id "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Peran.Items.Add(Dr("peran")) : arrPeran.Add(Dr("id"))
				Loop
			End Using

			'SQL = "select a.Jenis_Approval, a.Kode_Stock_Owner, "
			'SQL &= $"a.Approval_Level, d.ID as Id_Peran, a.Peran, a.Jabatan, a.No_HP, a.ID_User_Desktop, b.UserName as Username_Desktop, a.ID_User_Android, c.username as Username_Android "
			'SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste a "
			'SQL &= $"left join users b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_User_Desktop = b.UserID and a.ID_User_Desktop is not NULL "
			'SQL &= $"left join Emi_Users c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_User_Android = c.id and a.ID_User_Android is not null "
			'SQL &= $"inner join N_EMI_Master_Peran_Approval d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.Peran = d.Peran "
			'SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			'SQL &= $"and a.Jenis_Approval = '{SelectedJenisApproval}' "
			'SQL &= $"and a.Kode_Stock_Owner = '{SelectedLokasi}' "
			'SQL &= $"and a.Approval_Level = '{SelectedApprovalLevel}' "

			SQL = "SELECT a.Jenis_Approval, a.Kode_Stock_Owner, a.Approval_Level, d.ID as Id_Peran, b.Peran, b.Jabatan, b.No_HP, b.ID_User_Desktop, isnull(c.UserName, '-') as Username_Desktop, "
			SQL &= $"b.ID_User_Android, isnull(d.username, '-') as Username_Android, b.ID_User_Koneksi, "
			SQL &= $"isnull(b.ID_User_Koneksi, '-') as ID_User_Koneksi, isnull(e.UserName, '-')  as Username_Koneksi "
			SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
			SQL &= $"INNER JOIN N_EMI_Master_Hierarchy_Approval_Waste_user b ON a.Kode_Perusahaan = b.Kode_Perusahaan  "
			SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Approval_Level = b.Approval_Level "
			SQL &= $"LEFT JOIN users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.ID_User_Desktop = c.UserID "
			SQL &= $"LEFT JOIN Emi_Users d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.ID_User_Android = d.id "
			SQL &= $"LEFT JOIN users e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.ID_User_Koneksi = e.UserID "
			SQL &= $"inner join N_EMI_Master_Peran_Approval f on b.Kode_Perusahaan = f.Kode_Perusahaan and b.Peran = f.Peran "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Jenis_Approval = '{SelectedJenisApproval}' "
			SQL &= $"and a.kode_stock_owner = '{SelectedLokasi}' "
			SQL &= $"and a.Approval_Level = '{SelectedApprovalLevel}'"
			If SelectedApprovalLevel = 1 Then
				SQL &= $"and b.ID_User_Desktop = '{UserID}'"
			Else
				SQL &= $"and b.ID_User_Android = '{UserID}'"
			End If

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Cmb_Jenis_Approval.SelectedIndex = 0
					Cmb_Lokasi.SelectedIndex = arrLokasi.IndexOf(Dr("Kode_Stock_Owner"))
					Cmb_Approval_Level.SelectedItem = Dr("Approval_Level")
					Txt_Selected_Approval_Level.Text = Dr("Approval_Level")

					switchAutoComplete = True
					If SelectedApprovalLevel = 1 Then
						Txt_Username.Text = Dr("Username_Desktop")
						Txt_Selected_UserID.Text = Dr("ID_User_Desktop")
						SelectedUserID = Dr("ID_User_Desktop")
						Txt_Update_User_ID.Text = Dr("ID_User_Desktop")
					Else
						Txt_Username.Text = Dr("Username_Android")
						Txt_Selected_UserID.Text = Dr("ID_User_Android")
						SelectedUserID = Dr("ID_User_Android")
						Txt_Update_User_ID.Text = Dr("ID_User_Android")
					End If

					'If General_Class.CekNULL(Dr("ID_User_Desktop")) <> "" Then
					'    Txt_Username.Text = Dr("Username_Desktop")
					'    Txt_Selected_UserID.Text = Dr("ID_User_Desktop")
					'    SelectedUserID = Dr("ID_User_Desktop")
					'Else
					'    Txt_Username.Text = Dr("Username_Android")
					'    Txt_Selected_UserID.Text = Dr("ID_User_Android")
					'    SelectedUserID = Dr("ID_User_Android")
					'End If
					switchAutoComplete = False

					Cmb_Peran.SelectedItem = Dr("Peran")
					Txt_Jabatan.Text = Dr("Jabatan").ToString.Trim
					Txt_No_HP.Text = Dr("No_HP").ToString.Trim

					If Dr("ID_User_Koneksi") <> "-" Then
						Cb_Flag_Koneksi.Checked = True

						Dim UernameKoneksi As String = Dr("Username_Koneksi")

						Cmb_User_Koneksi.Items.Clear() : arrId_User_Koneksi.Clear()
						SQL = "SELECT b.ID_User_Desktop, c.UserName "
						SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
						SQL &= $"inner join N_EMI_Master_Hierarchy_Approval_Waste_user b on a.Kode_Perusahaan = b.Kode_Perusahaan  "
						SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Approval_Level = b.Approval_Level and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
						SQL &= $"inner join Users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.ID_User_Desktop = c.UserID "
						SQL &= $"where a.Approval_Level = '1' "
						SQL &= $"and a.Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
						SQL &= $"order by c.UserName "
						Dr.Close()
						Using Ds = BindingTrans(SQL)
							If Ds.Tables("MyTable").Rows.Count <> 0 Then
								For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
									Cmb_User_Koneksi.Items.Add(Ds.Tables("MyTable").Rows(i).Item("UserName")) : arrId_User_Koneksi.Add(Ds.Tables("MyTable").Rows(i).Item("ID_User_Desktop"))
								Next
							End If

						End Using
						Cmb_User_Koneksi.SelectedItem = UernameKoneksi
					End If

					Cmb_Jenis_Approval.Enabled = False
					Txt_Username.Enabled = False
					Cmb_Lokasi.Enabled = False
					Cmb_Peran.Enabled = True
					Txt_Jabatan.Enabled = True
					Txt_No_HP.Enabled = True

					Btn_Tambah.Tag = "UPDATE"
					Btn_Tambah.Text = "&Update"

					Btn_Delete.Visible = True

					Label8.Focus()

				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Lv_DetaiL_Pemindahan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetaiL_Pemindahan.DoubleClick
		If Lv_DetaiL_Pemindahan.Items.Count = 0 Then Exit Sub

		Try
			OpenConn()

			Dim SelectedJenisApproval As String = "Waste_Produk"
			Dim SelectedLokasi As String = Txt_Lokasi_Pemindahan.Text.Trim
			Dim SelectedApprovalLevel As String = Lv_DetaiL_Pemindahan.FocusedItem.SubItems(1).Text
			Dim UserID As String = Lv_DetaiL_Pemindahan.FocusedItem.SubItems(0).Text

			Cmb_Peran.Items.Clear() : arrPeran.Clear()
			SQL = $"select id, peran from N_EMI_Master_Peran_Approval where kode_perusahaan = '" & KodePerusahaan & "' order by id "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Peran.Items.Add(Dr("peran")) : arrPeran.Add(Dr("id"))
				Loop
			End Using

			'SQL = "select a.Jenis_Approval, a.Kode_Stock_Owner, "
			'SQL &= $"a.Approval_Level, d.ID as Id_Peran, a.Peran, a.Jabatan, a.No_HP, a.ID_User_Desktop, b.UserName as Username_Desktop, a.ID_User_Android, c.username as Username_Android "
			'SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste a "
			'SQL &= $"left join users b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_User_Desktop = b.UserID and a.ID_User_Desktop is not NULL "
			'SQL &= $"left join Emi_Users c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_User_Android = c.id and a.ID_User_Android is not null "
			'SQL &= $"inner join N_EMI_Master_Peran_Approval d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.Peran = d.Peran "
			'SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			'SQL &= $"and a.Jenis_Approval = '{SelectedJenisApproval}' "
			'SQL &= $"and a.Kode_Stock_Owner = '{SelectedLokasi}' "
			'SQL &= $"and a.Approval_Level = '{SelectedApprovalLevel}' "

			SQL = "SELECT a.Jenis_Approval, a.Kode_Stock_Owner, a.Approval_Level, d.ID as Id_Peran, b.Peran, b.Jabatan, b.No_HP, b.ID_User_Desktop, isnull(c.UserName, '-') as Username_Desktop, "
			SQL &= $"b.ID_User_Android, isnull(d.username, '-') as Username_Android, b.ID_User_Koneksi, "
			SQL &= $"isnull(b.ID_User_Koneksi, '-') as ID_User_Koneksi, isnull(e.UserName, '-')  as Username_Koneksi "
			SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
			SQL &= $"INNER JOIN N_EMI_Master_Hierarchy_Approval_Waste_user b ON a.Kode_Perusahaan = b.Kode_Perusahaan  "
			SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Approval_Level = b.Approval_Level "
			SQL &= $"LEFT JOIN users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.ID_User_Desktop = c.UserID "
			SQL &= $"LEFT JOIN Emi_Users d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.ID_User_Android = d.id "
			SQL &= $"LEFT JOIN users e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.ID_User_Koneksi = e.UserID "
			SQL &= $"inner join N_EMI_Master_Peran_Approval f on b.Kode_Perusahaan = f.Kode_Perusahaan and b.Peran = f.Peran "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Jenis_Approval = '{SelectedJenisApproval}' "
			SQL &= $"and a.kode_stock_owner = '{SelectedLokasi}' "
			SQL &= $"and a.Approval_Level = '{SelectedApprovalLevel}'"
			If SelectedApprovalLevel = 1 Then
				SQL &= $"and b.ID_User_Desktop = '{UserID}'"
			Else
				SQL &= $"and b.ID_User_Android = '{UserID}'"
			End If
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Cmb_Jenis_Approval.SelectedIndex = 1
					Cmb_Lokasi.SelectedIndex = arrLokasi.IndexOf(Dr("Kode_Stock_Owner"))
					Cmb_Approval_Level.SelectedItem = Dr("Approval_Level")
					Txt_Selected_Approval_Level.Text = Dr("Approval_Level")

					switchAutoComplete = True
					If SelectedApprovalLevel = 1 Then
						Txt_Username.Text = Dr("Username_Desktop")
						Txt_Selected_UserID.Text = Dr("ID_User_Desktop")
						SelectedUserID = Dr("ID_User_Desktop")
						Txt_Update_User_ID.Text = Dr("ID_User_Desktop")
					Else
						Txt_Username.Text = Dr("Username_Android")
						Txt_Selected_UserID.Text = Dr("ID_User_Android")
						SelectedUserID = Dr("ID_User_Android")
						Txt_Update_User_ID.Text = Dr("ID_User_Android")
					End If

					'If General_Class.CekNULL(Dr("ID_User_Desktop")) <> "" Then
					'    Txt_Username.Text = Dr("Username_Desktop")
					'    Txt_Selected_UserID.Text = Dr("ID_User_Desktop")
					'    SelectedUserID = Dr("ID_User_Desktop")
					'Else
					'    Txt_Username.Text = Dr("Username_Android")
					'    Txt_Selected_UserID.Text = Dr("ID_User_Android")
					'    SelectedUserID = Dr("ID_User_Android")
					'End If
					switchAutoComplete = False

					'Cmb_Peran.SelectedIndex = arrPeran.IndexOf(Dr("Id_Peran"))
					Cmb_Peran.SelectedItem = Dr("Peran")
					Txt_Jabatan.Text = Dr("Jabatan").ToString.Trim
					Txt_No_HP.Text = Dr("No_HP").ToString.Trim

					If Dr("ID_User_Koneksi") <> "-" Then
						Cb_Flag_Koneksi.Checked = True

						Dim UernameKoneksi As String = Dr("Username_Koneksi")

						Cmb_User_Koneksi.Items.Clear() : arrId_User_Koneksi.Clear()
						SQL = "SELECT b.ID_User_Desktop, c.UserName "
						SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
						SQL &= $"inner join N_EMI_Master_Hierarchy_Approval_Waste_user b on a.Kode_Perusahaan = b.Kode_Perusahaan  "
						SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Approval_Level = b.Approval_Level and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
						SQL &= $"inner join Users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.ID_User_Desktop = c.UserID "
						SQL &= $"where a.Approval_Level = '1' "
						SQL &= $"and a.Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
						SQL &= $"order by c.UserName "
						Dr.Close()
						Using Ds = BindingTrans(SQL)
							If Ds.Tables("MyTable").Rows.Count <> 0 Then
								For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
									Cmb_User_Koneksi.Items.Add(Ds.Tables("MyTable").Rows(i).Item("UserName")) : arrId_User_Koneksi.Add(Ds.Tables("MyTable").Rows(i).Item("ID_User_Desktop"))
								Next
							End If

						End Using
						Cmb_User_Koneksi.SelectedItem = UernameKoneksi
					End If

					Cmb_Jenis_Approval.Enabled = False
					Txt_Username.Enabled = False
					Cmb_Lokasi.Enabled = False
					Cmb_Peran.Enabled = True
					Txt_Jabatan.Enabled = True
					Txt_No_HP.Enabled = True
					Txt_Username.Enabled = True

					Btn_Tambah.Tag = "UPDATE"
					Btn_Tambah.Text = "&Update"

					Btn_Delete.Visible = True

					Label8.Focus()

				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Lv_Review.Items.Count = 0 Then
			MessageBox.Show("Tidak Ada Data Yang Bisa Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Action As String = ""

			'===========================
			'=     CEK BUTTON ROLE     =
			'===========================
			If CekButtonRole("Simpan_Approval_Level_Waste") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Simpan Approval Level Waste", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim IsFlagConnection As String = If(Cb_Flag_Koneksi.Checked, "'Y'", "NULL")

			'=========================
			'=     INSERT INNDUK     =
			'=========================
			SQL = "select 1 "
			SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
			SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
			SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					'Dr.Close()
					'CloseTrans()
					'CloseConn()
					'MessageBox.Show("Terjadi Kesalahan, Data Gudang Sudah Pernah Diinput", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					'Exit Sub
				Else
					Dr.Close()

					SQL = "insert into N_EMI_Master_Hierarchy_Approval_Waste(Kode_Perusahaan, Jenis_Approval, Approval_Level, Kode_Stock_Owner, Flag_Koneksi, Jumlah_Approval) "
					SQL &= $"values ('{KodePerusahaan}', '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}', '{Cmb_Approval_Level.Text.Trim}', '{arrLokasi(Cmb_Lokasi.SelectedIndex)}', "
					SQL &= $"{IsFlagConnection}, NULL)"
					ExecuteTrans(SQL)

				End If
			End Using

			Dim TotalUser As Integer = 0

			For i As Integer = 0 To Lv_Review.Items.Count - 1
				Get_Data_Review(i)

				'=====================================
				'=     CEK APAKAH DATA SUDAH ADA     =
				'=====================================
				SQL = "SELECT 1 "
				SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
				SQL &= $"inner join N_EMI_Master_Hierarchy_Approval_Waste_user b on a.Kode_Perusahaan = b.Kode_Perusahaan "
				SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Approval_Level = b.Approval_Level and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
				SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and a.Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
				SQL &= $"and a.Approval_Level = '{Cmb_Approval_Level.Text.Trim}' "
				SQL &= $"and a.Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
				If Cmb_Approval_Level.Text = "1" Then
					SQL &= $"and b.ID_User_Desktop = '{Lv_Review_Id_User.Trim}' "
				Else
					SQL &= $"and b.ID_User_Android = '{Lv_Review_Id_User.Trim}' "
				End If
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terjadi Kesalahan, Data User Sudah Pernah Diinput", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=====================================
				'=     CEK APAKAH DATA SUDAH ADA     =
				'=====================================
				SQL = "SELECT 1 "
				SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
				SQL &= $"inner join N_EMI_Master_Hierarchy_Approval_Waste_user b on a.Kode_Perusahaan = b.Kode_Perusahaan "
				SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Approval_Level = b.Approval_Level and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
				SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and a.Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
				SQL &= $"and b.ID_User_Koneksi = '{Lv_Review_Id_User_Koneksi.Trim}' "
				SQL &= $"and a.Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
				If Cmb_Approval_Level.Text = "1" Then
					SQL &= $"and b.ID_User_Desktop = '{Lv_Review_Id_User.Trim}' "
				Else
					SQL &= $"and b.ID_User_Android = '{Lv_Review_Id_User.Trim}' "
				End If
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data User Sudah Pernah Diinput", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=====================================================
				'=     CEK APAKAH USER DIBAWAHNYA SUDAH DI INPUT     =
				'=====================================================
				If Cmb_Approval_Level.Text.Trim <> 1 Then
					SQL = "SELECT b.Approval_Level "
					SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
					SQL &= $"INNER JOIN N_EMI_Master_Hierarchy_Approval_Waste_user b ON a.Kode_Perusahaan = b.Kode_Perusahaan  "
					SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Approval_Level = b.Approval_Level "
					SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
					SQL &= $"and a.Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
					SQL &= $"and a.kode_stock_owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
					If Cmb_Approval_Level.Text.Trim > 2 Then
						SQL &= $"and b.ID_User_Koneksi = '{Lv_Review_Id_User_Koneksi.Trim}' "
					End If
					SQL &= $"and b.Approval_Level = {Val(HilangkanTanda(Cmb_Approval_Level.Text)) - 1} "
					Using Dr = OpenTrans(SQL)
						If Not Dr.Read Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show($"Terjadi Kesalahan, User dengan Level {Val(HilangkanTanda(Cmb_Approval_Level.Text)) - 1} Belum Diinput", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End Using

				End If

				Dim Id_User_Desktop As String = ""
				Dim Id_User_Android As String = ""
				If Cmb_Approval_Level.Text = "1" Then
					Id_User_Desktop = $"'{Lv_Review_Id_User.Trim}'"
					Id_User_Android = "0"
				Else
					Id_User_Desktop = "0"
					Id_User_Android = $"'{Lv_Review_Id_User.Trim}'"
				End If

				'=========================
				'=     INSERT DETAIL     =
				'=========================
				SQL = "insert into N_EMI_Master_Hierarchy_Approval_Waste_user (Kode_Perusahaan, Jenis_Approval, Approval_Level, "
				SQL &= $"Kode_Stock_Owner, ID_User_Android, ID_User_Desktop, Peran, Jabatan, isActive, No_HP, ID_User_Koneksi) "
				SQL &= $"values ('{KodePerusahaan}', '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}', '{Cmb_Approval_Level.Text.Trim}', "
				SQL &= $"'{arrLokasi(Cmb_Lokasi.SelectedIndex)}', {Id_User_Android}, {Id_User_Desktop}, '{Lv_Review_Peran.Trim}', '{Lv_Review_Jabatan.Trim}', "
				SQL &= $"'Y', '{Lv_Review_No_HP.Trim}', '{Lv_Review_Id_User_Koneksi.Trim}') "
				ExecuteTrans(SQL)

				TotalUser += 1

			Next

			'=========================
			'=     UPDATE PARENT     =
			'=========================
			SQL = $"Update N_EMI_Master_Hierarchy_Approval_Waste Set Jumlah_Approval = '{TotalUser}' "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
			SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
			SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
			ExecuteTrans(SQL)

			Action = "Disimpan"

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show($"Data Berhasil {Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

#Region "Kode Lama"

		'Try
		'    OpenConn()
		'    Cmd.Transaction = Cn.BeginTransaction

		'    Dim Action As String = ""

		'    '===========================
		'    '=     CEK BUTTON ROLE     =
		'    '===========================
		'    If CekButtonRole("Simpan_Approval_Level_Waste") = "T" Then
		'        CloseConn()
		'        MessageBox.Show("Anda Tidak Memiliki Akses Untuk Simpan Approval Level Waste", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'        Exit Sub
		'    End If

		'    If Btn_Simpan.Tag = "SIMPAN" Then
		'        '=====================================
		'        '=     CEK APAKAH DATA SUDAH ADA     =
		'        '=====================================
		'        SQL = "select 1 "
		'        SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
		'        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
		'        SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
		'        SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
		'        SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
		'        Using Dr = OpenTrans(SQL)
		'            If Dr.Read Then
		'                Dr.Close()
		'                CloseTrans()
		'                CloseConn()
		'                MessageBox.Show("Terjadi Kesalahan, Data sudah ada di database", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                Exit Sub
		'            End If
		'        End Using

		'        '=========================================================
		'        '=     CEK APAKAH USER SUDAH ADA DALAM DATA APPROVAL     =
		'        '=========================================================
		'        SQL = "select 1 "
		'        SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
		'        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
		'        SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
		'        SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
		'        If Cmb_Approval_Level.SelectedIndex = 0 Then
		'            SQL &= $"and ID_User_Desktop = '{Txt_Selected_UserID.Text}' "
		'        Else
		'            SQL &= $"and ID_User_Android = '{Txt_Selected_UserID.Text}'"
		'        End If
		'        Using Dr = OpenTrans(SQL)
		'            If Dr.Read Then
		'                Dr.Close()
		'                CloseTrans()
		'                CloseConn()
		'                MessageBox.Show("Terjadi Kesalahan, User sudah ada di dalam data approval", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                Exit Sub
		'            End If
		'        End Using

		'        Dim Id_User_Desktop As String = ""
		'        Dim Id_User_Android As String = ""

		'        If Cmb_Approval_Level.Text = "1" Then
		'            Id_User_Desktop = $"'{Txt_Selected_UserID.Text}'"
		'            Id_User_Android = "0"
		'        Else
		'            Id_User_Desktop = "NULL"
		'            Id_User_Android = $"'{Txt_Selected_UserID.Text}'"
		'        End If

		'        SQL = "insert into N_EMI_Master_Hierarchy_Approval_Waste (Kode_Perusahaan, ID_User_Android, "
		'        SQL &= $"ID_User_Desktop, Jenis_Approval, Approval_Level, Peran, Jabatan, isActive, No_HP, Kode_Stock_Owner) "
		'        SQL &= $"values ('{KodePerusahaan}', {Id_User_Android}, {Id_User_Desktop}, '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}', "
		'        SQL &= $"'{Cmb_Approval_Level.Text}', '{Cmb_Peran.Text}', '{Txt_Jabatan.Text}', 'Y', '{Txt_No_HP.Text.Trim}', '{arrLokasi(Cmb_Lokasi.SelectedIndex)}') "
		'        ExecuteTrans(SQL)

		'        SQL = "select 1 "
		'        SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
		'        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
		'        SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
		'        SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
		'        SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
		'        Using Dr = OpenTrans(SQL)
		'            If Not Dr.Read Then
		'                Dr.Close()
		'                CloseTrans()
		'                CloseConn()
		'                MessageBox.Show("Terjadi Kesalaha, Data Tidak Berhasil Disimpan, Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                Exit Sub
		'            End If
		'        End Using

		'        Action = "Disimpan"

		'    ElseIf Btn_Simpan.Tag = "UPDATE" Then

		'        '=====================================
		'        '=     CEK APAKAH DATA SUDAH ADA     =
		'        '=====================================
		'        SQL = "select 1 "
		'        SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
		'        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
		'        SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
		'        SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
		'        Using Dr = OpenTrans(SQL)
		'            If Not Dr.Read Then
		'                Dr.Close()
		'                CloseTrans()
		'                CloseConn()
		'                MessageBox.Show("Terjadi Kesalaha, Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                Exit Sub
		'            End If
		'        End Using

		'        '=========================================================
		'        '=     CEK APAKAH USER SUDAH ADA DALAM DATA APPROVAL     =
		'        '=========================================================
		'        'SQL = "select ID_User_Android, ID_User_Desktop "
		'        'SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
		'        'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
		'        'SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
		'        'SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
		'        'SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
		'        ''SQL &= $"and ID_User_Android = '{Txt_Selected_UserID.Text}' OR ID_User_Desktop = '{Txt_Selected_UserID.Text}' "
		'        'Using Dr = OpenTrans(SQL)
		'        '    If Dr.Read Then

		'        '        Dr.Close()
		'        '        SQL = "select ID_User_Android, ID_User_Desktop "
		'        '        SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
		'        '        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
		'        '        SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
		'        '        SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
		'        '        SQL &= $"and ID_User_Android = '{Txt_Selected_UserID.Text}' OR ID_User_Desktop = '{Txt_Selected_UserID.Text}' "
		'        '        Using Dr2 = OpenTrans(SQL)
		'        '            If Dr2.Read Then
		'        '                Dr2.Close()
		'        '                CloseTrans()
		'        '                CloseConn()
		'        '                MessageBox.Show("Terjadi Kesalahan, User sudah ada di dalam data approval", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'        '                Exit Sub
		'        '            End If
		'        '        End Using
		'        '    End If
		'        'End Using

		'        If Txt_Selected_Approval_Level.Text <> Cmb_Approval_Level.Text Then
		'            SQL = "select ID_User_Android, ID_User_Desktop "
		'            SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
		'            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
		'            SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
		'            SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
		'            SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
		'            Using Dr2 = OpenTrans(SQL)
		'                If Dr2.Read Then
		'                    Dr2.Close()
		'                    CloseTrans()
		'                    CloseConn()
		'                    MessageBox.Show("Terjadi Kesalahan, Approval Level Sudah Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                    Exit Sub
		'                End If
		'            End Using
		'        End If

		'        If SelectedUserID <> Txt_Selected_UserID.Text Then
		'            SQL = "select ID_User_Android, ID_User_Desktop "
		'            SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
		'            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
		'            SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
		'            SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
		'            If Cmb_Approval_Level.SelectedIndex = 0 Then
		'                SQL &= $"and ID_User_Desktop = '{Txt_Selected_UserID.Text}' "
		'            Else
		'                SQL &= $"and ID_User_Android = '{Txt_Selected_UserID.Text}'"
		'            End If
		'            Using Dr2 = OpenTrans(SQL)
		'                If Dr2.Read Then
		'                    Dr2.Close()
		'                    CloseTrans()
		'                    CloseConn()
		'                    MessageBox.Show("Terjadi Kesalahan, User sudah ada di dalam data approval", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                    Exit Sub
		'                End If
		'            End Using
		'        End If

		'        Dim Id_User_Desktop As String = ""
		'        Dim Id_User_Android As String = ""

		'        If Cmb_Approval_Level.Text = "1" Then
		'            Id_User_Desktop = $"'{Txt_Selected_UserID.Text}'"
		'            Id_User_Android = "0"
		'        Else
		'            Id_User_Desktop = "NULL"
		'            Id_User_Android = $"'{Txt_Selected_UserID.Text}'"
		'        End If

		'        SQL = "update N_EMI_Master_Hierarchy_Approval_Waste "
		'        SQL &= $"set Approval_Level = '{Cmb_Approval_Level.Text}', ID_User_Android = {Id_User_Android}, "
		'        SQL &= $"ID_User_Desktop = {Id_User_Desktop}, Peran = '{Cmb_Peran.Text}', "
		'        SQL &= $"Jabatan = '{Txt_Jabatan.Text}', No_HP = '{Txt_No_HP.Text}' "
		'        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
		'        SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
		'        SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
		'        SQL &= $"and Approval_Level = '{Txt_Selected_Approval_Level.Text}' "
		'        ExecuteTrans(SQL)

		'        Action = "Diupdate"

		'    End If

		'    Cmd.Transaction.Commit()
		'    CloseConn()
		'    MessageBox.Show($"Data Berhasil {Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		'Catch ex As Exception
		'    CloseTrans()
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

#End Region

		Kosong()
		Kosong_Tab_Review()
	End Sub

	Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click
		If Cmb_Jenis_Approval.SelectedIndex = -1 Then
			MessageBox.Show("Jenis Approval Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Jenis_Approval.DroppedDown = True
			Cmb_Jenis_Approval.Focus()
			Exit Sub
		ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
			MessageBox.Show("Lokasi Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Lokasi.DroppedDown = True
			Cmb_Lokasi.Focus()
			Exit Sub
		ElseIf Cmb_Approval_Level.SelectedIndex = -1 Then
			MessageBox.Show("Approval Level Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Approval_Level.DroppedDown = True
			Cmb_Approval_Level.Focus()
			Exit Sub
		ElseIf Txt_Username.Text.Trim.Length = 0 Then
			MessageBox.Show("Users Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Username.Focus()
			Exit Sub
		ElseIf Cmb_Peran.SelectedIndex = -1 Then
			MessageBox.Show("Peran Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Peran.DroppedDown = True
			Cmb_Peran.Focus()
			Exit Sub
		ElseIf Txt_Jabatan.Text.Trim.Length = 0 Then
			MessageBox.Show("Jabatan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Jabatan.Focus()
			Exit Sub
		ElseIf Txt_No_HP.Text.Trim.Length = 0 Then
			MessageBox.Show("No HP Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_No_HP.Focus()
			Exit Sub

			'ElseIf Cb_Flag_Koneksi.Checked Then
			'    If Cmb_User_Koneksi.SelectedIndex = -1 Then
			'        MessageBox.Show("User Koneksi Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Cmb_User_Koneksi.DroppedDown = True
			'        Cmb_User_Koneksi.Focus()
			'        Exit Sub
			'    End If
		End If

		If MessageBox.Show("Yakin ingin Menghapus Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===========================
			'=     CEK BUTTON ROLE     =
			'===========================
			If CekButtonRole("Hapus_Approval_Level_Waste") = "T" Then
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Hapus Approval Level Waste", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			''==================================
			''=     CEK APAKAH ADA DATANYA     =
			''==================================
			'SQL = "select 1 "
			'SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
			'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			'SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
			'SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
			'SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
			'Using Dr = OpenTrans(SQL)
			'    If Not Dr.Read Then
			'        Dr.Close()
			'        CloseTrans()
			'        CloseConn()
			'        MessageBox.Show("Terjadi Kesalahan, Data Tidak ada di database", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'=====================================
			'=     CEK APAKAH DATA SUDAH ADA     =
			'=====================================

			SQL = "SELECT 1 "
			SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
			SQL &= $"inner join N_EMI_Master_Hierarchy_Approval_Waste_user b on a.Kode_Perusahaan = b.Kode_Perusahaan  "
			SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Approval_Level = b.Approval_Level and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and b.Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
			SQL &= $"and b.Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
			SQL &= $"and b.Approval_Level = '{Cmb_Approval_Level.Text.Trim}' "
			If Cmb_Approval_Level.Text.Trim = 1 Then
				SQL &= $"and b.ID_User_Desktop = '{Txt_Update_User_ID.Text.Trim}'"
			Else
				SQL &= $"and b.ID_User_Android = '{Txt_Update_User_ID.Text.Trim}'"
			End If
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan, Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = $"delete N_EMI_Master_Hierarchy_Approval_Waste_user "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
			SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
			SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
			If Cmb_Approval_Level.Text.Trim = 1 Then
				SQL &= $"and ID_User_Desktop = '{Txt_Update_User_ID.Text.Trim}'"
			Else
				SQL &= $"and ID_User_Android = '{Txt_Update_User_ID.Text.Trim}'"
			End If
			ExecuteTrans(SQL)

			SQL = "update N_EMI_Master_Hierarchy_Approval_Waste set Jumlah_Approval -= 1 "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and jenis_approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
			SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
			SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
			ExecuteTrans(SQL)

			SQL = $"Select 1 from N_EMI_Master_Hierarchy_Approval_Waste_user "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
			SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
			SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					SQL = "delete N_EMI_Master_Hierarchy_Approval_Waste  "
					SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
					SQL &= $"and jenis_approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
					SQL &= $"and Approval_Level = '{Cmb_Approval_Level.Text}' "
					SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
					ExecuteTrans(SQL)
				End If
			End Using

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show($"Data Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		TabControl1_SelectedIndexChanged(e, New EventArgs)
		Kosong()
	End Sub

	Private Sub Btn_Tambah_Click(sender As Object, e As EventArgs) Handles Btn_Tambah.Click
		If Cmb_Jenis_Approval.SelectedIndex = -1 Then
			MessageBox.Show("Jenis Approval Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Jenis_Approval.DroppedDown = True
			Cmb_Jenis_Approval.Focus()
			Exit Sub
		ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
			MessageBox.Show("Lokasi Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Lokasi.DroppedDown = True
			Cmb_Lokasi.Focus()
			Exit Sub
		ElseIf Cmb_Approval_Level.SelectedIndex = -1 Then
			MessageBox.Show("Approval Level Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Approval_Level.DroppedDown = True
			Cmb_Approval_Level.Focus()
			Exit Sub
		ElseIf Txt_Username.Text.Trim.Length = 0 Then
			MessageBox.Show("Users Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Username.Focus()
			Exit Sub
		ElseIf Cmb_Peran.SelectedIndex = -1 Then
			MessageBox.Show("Peran Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Peran.DroppedDown = True
			Cmb_Peran.Focus()
			Exit Sub
		ElseIf Txt_Jabatan.Text.Trim.Length = 0 Then
			MessageBox.Show("Jabatan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Jabatan.Focus()
			Exit Sub
		ElseIf Txt_No_HP.Text.Trim.Length = 0 Then
			MessageBox.Show("No HP Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_No_HP.Focus()
			Exit Sub

		ElseIf Cb_Flag_Koneksi.Checked Then
			If Cmb_User_Koneksi.SelectedIndex = -1 Then
				MessageBox.Show("User Koneksi Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_User_Koneksi.DroppedDown = True
				Cmb_User_Koneksi.Focus()
				Exit Sub
			End If
		End If

		'Btn_Tambah.Tag = "TAMBAH"
		'Btn_Tambah.Text = "&Tambah"

		If Btn_Tambah.Tag = "TAMBAH" Then
			'==================================================
			'=     CEK APAKAH SUDAH ADA DI DALAM LISTVIEW     =
			'==================================================
			For i As Integer = 0 To Lv_Review.Items.Count - 1
				Get_Data_Review(i)

				If Lv_Review_Id_User.Trim = Txt_Selected_UserID.Text.Trim Then
					MessageBox.Show("Username Sudah Ada Di Dalam List", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				ElseIf Lv_Review_No_HP.Trim = Txt_No_HP.Text.Trim Then
					MessageBox.Show("Nomor HP Sudah Ada Di Dalam List", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

			Next

			Dim Id_User_Koeneksi As String = "0"
			If Cb_Flag_Koneksi.Checked Then
				Id_User_Koeneksi = arrId_User_Koneksi(Cmb_User_Koneksi.SelectedIndex)
			End If

			Dim Lv As ListViewItem
			Lv = Lv_Review.Items.Add(Txt_Username.Text.Trim)
			Lv.SubItems.Add(Cmb_Peran.Text.Trim)
			Lv.SubItems.Add(Txt_Jabatan.Text.Trim)
			Lv.SubItems.Add(Txt_No_HP.Text.Trim)
			Lv.SubItems.Add(Txt_Selected_UserID.Text.Trim)
			Lv.SubItems.Add(Id_User_Koeneksi)

			Disable_Panel_Input()

		ElseIf Btn_Tambah.Tag = "UPDATE" Then

			Try
				OpenConn()
				Cmd.Transaction = Cn.BeginTransaction

				'=====================================
				'=     CEK APAKAH DATA SUDAH ADA     =
				'=====================================

				SQL = "SELECT 1 "
				SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
				SQL &= $"inner join N_EMI_Master_Hierarchy_Approval_Waste_user b on a.Kode_Perusahaan = b.Kode_Perusahaan  "
				SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Approval_Level = b.Approval_Level and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
				SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and b.Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
				SQL &= $"and b.Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
				'SQL &= $"and b.Approval_Level = '{Cmb_Approval_Level.Text.Trim}' "
				If Cmb_Approval_Level.Text.Trim = 1 Then
					SQL &= $"and b.ID_User_Desktop = '{Txt_Update_User_ID.Text.Trim}'"
				Else
					SQL &= $"and b.ID_User_Android = '{Txt_Update_User_ID.Text.Trim}'"
				End If
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terjadi Kesalahan, Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				If Txt_Selected_Approval_Level.Text <> Cmb_Approval_Level.Text Then
					SQL = "SELECT 1 "
					SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
					SQL &= $"inner join N_EMI_Master_Hierarchy_Approval_Waste_user b on a.Kode_Perusahaan = b.Kode_Perusahaan  "
					SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Approval_Level = b.Approval_Level and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
					SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
					SQL &= $"and b.Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
					SQL &= $"and b.Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
					SQL &= $"and b.Approval_Level = '{Txt_Selected_Approval_Level.Text.Trim}' "
					If Cmb_Approval_Level.Text.Trim = 1 Then
						SQL &= $"and b.ID_User_Desktop = '{Txt_Update_User_ID.Text.Trim}'"
					Else
						SQL &= $"and b.ID_User_Android = '{Txt_Update_User_ID.Text.Trim}'"
					End If

					Using Dr2 = OpenTrans(SQL)
						If Dr2.Read Then
							Dr2.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Terjadi Kesalahan, Approval Level Sudah Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End Using
				End If

				'If SelectedUserID <> Txt_Selected_UserID.Text Then
				'    'SQL = "select ID_User_Android, ID_User_Desktop "
				'    'SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
				'    'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
				'    'SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
				'    'SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
				'    'If Cmb_Approval_Level.SelectedIndex = 0 Then
				'    '    SQL &= $"and ID_User_Desktop = '{Txt_Selected_UserID.Text}' "
				'    'Else
				'    '    SQL &= $"and ID_User_Android = '{Txt_Selected_UserID.Text}'"
				'    'End If

				'    SQL = "SELECT 1 "
				'    SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
				'    SQL &= $"inner join N_EMI_Master_Hierarchy_Approval_Waste_user b on a.Kode_Perusahaan = b.Kode_Perusahaan  "
				'    SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Approval_Level = b.Approval_Level and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
				'    SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
				'    SQL &= $"and b.Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
				'    SQL &= $"and b.Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
				'    'SQL &= $"and b.Approval_Level = '{Txt_Selected_Approval_Level.Text.Trim}' "
				'    If Cmb_Approval_Level.Text.Trim = 1 Then
				'        SQL &= $"and b.ID_User_Desktop = '{Txt_Selected_UserID.Text.Trim}'"
				'    Else
				'        SQL &= $"and b.ID_User_Android = '{Txt_Selected_UserID.Text.Trim}'"
				'    End If

				'    Using Dr2 = OpenTrans(SQL)
				'        If Dr2.Read Then
				'            Dr2.Close()
				'            CloseTrans()
				'            CloseConn()
				'            MessageBox.Show("Terjadi Kesalahan, User sudah ada di dalam data approval", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'            Exit Sub
				'        End If
				'    End Using
				'End If

				Dim Id_User_Desktop As String = ""
				Dim Id_User_Android As String = ""

				If Cmb_Approval_Level.Text = "1" Then
					Id_User_Desktop = $"'{Txt_Selected_UserID.Text}'"
					Id_User_Android = "0"
				Else
					Id_User_Desktop = "0"
					Id_User_Android = $"'{Txt_Selected_UserID.Text}'"
				End If

				SQL = "update N_EMI_Master_Hierarchy_Approval_Waste_user "
				SQL &= $"set Approval_Level = '{Cmb_Approval_Level.Text}', "
				SQL &= $"Peran = '{Cmb_Peran.Text}', "
				SQL &= $"Jabatan = '{Txt_Jabatan.Text}', No_HP = '{Txt_No_HP.Text}' "
				SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and Jenis_Approval = '{arrJenisApproval(Cmb_Jenis_Approval.SelectedIndex)}' "
				SQL &= $"and Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
				SQL &= $"and Approval_Level = '{Txt_Selected_Approval_Level.Text}' "
				If Cmb_Approval_Level.Text.Trim = 1 Then
					SQL &= $"and ID_User_Desktop = '{Txt_Update_User_ID.Text.Trim}'"
				Else
					SQL &= $"and ID_User_Android = '{Txt_Update_User_ID.Text.Trim}'"
				End If
				ExecuteTrans(SQL)

				Cmd.Transaction.Commit()
				CloseConn()
				MessageBox.Show("Data Behasil DI Update", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			Catch ex As Exception
				CloseTrans()
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			TabControl1_SelectedIndexChanged(e, New EventArgs)
			Kosong()

		End If

	End Sub

	Private Sub Disable_Panel_Input()
		Cmb_Jenis_Approval.Enabled = False
		Cmb_Lokasi.Enabled = False
		Cmb_Approval_Level.Enabled = False

		Cb_Flag_Koneksi.Enabled = False
		Cmb_User_Koneksi.Enabled = False

		Cmb_Peran.SelectedIndex = -1

		Cmb_Peran.Text = ""

		switchAutoComplete = True
		Txt_Username.Text = ""
		Txt_Selected_UserID.Text = ""
		Txt_Selected_Approval_Level.Text = ""
		Txt_Jabatan.Text = ""
		Txt_No_HP.Text = ""
		SelectedUserID = ""
		switchAutoComplete = False

		Txt_Username.Enabled = True
		Txt_Selected_UserID.Enabled = False
		Cmb_Peran.Enabled = False
		Txt_Jabatan.Enabled = False
		Txt_No_HP.Enabled = False

		Btn_Delete.Visible = False

	End Sub

	Private Sub Enabled_Panel_Input()
		Cmb_Jenis_Approval.Enabled = True
		Cmb_Lokasi.Enabled = True
		Cmb_Approval_Level.Enabled = True

		Cb_Flag_Koneksi.Enabled = False
		Cmb_User_Koneksi.Enabled = False

		Cmb_Peran.SelectedIndex = -1

		Cmb_Peran.Text = ""

		switchAutoComplete = True
		Txt_Username.Text = ""
		Txt_Selected_UserID.Text = ""
		Txt_Selected_Approval_Level.Text = ""
		Txt_Jabatan.Text = ""
		Txt_No_HP.Text = ""
		SelectedUserID = ""
		switchAutoComplete = False

		Txt_Username.Enabled = False
		Txt_Selected_UserID.Enabled = False
		Cmb_Peran.Enabled = False
		Txt_Jabatan.Enabled = False
		Txt_No_HP.Enabled = False

		Btn_Delete.Visible = False

	End Sub

	Private Sub Load_Data_Tab2()
		Txt_Jenis_Approval_Pemindahan.Text = ""
		Txt_Lokasi_Pemindahan.Text = ""
		Lv_DetaiL_Pemindahan.Items.Clear()

		Try
			OpenConn()

			Lv_Data_Pemindahan.Rows.Clear()
			SQL = "select distinct Kode_Stock_Owner "
			SQL &= $"from N_EMI_Master_Hierarchy_Approval_Waste "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and Jenis_Approval = 'Waste_Produk' "
			If Txt_Filter_Lokasi_Pemindahan.Text.Trim.Length <> 0 Then
				SQL &= $"and Kode_Stock_Owner like '%{Txt_Filter_Lokasi_Pemindahan.Text}%' "
			End If
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Lv_Data_Pemindahan.Rows.Add(1)
							Lv_Data_Pemindahan.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Stock_Owner")
						Next

						Lv_Data_Pemindahan.ClearSelection()
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

	Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
		If TabControl1.SelectedIndex = 0 Then
			Kosong_Tab_Review()
		ElseIf TabControl1.SelectedIndex = 1 Then
			Kosong_Tab_1()
		ElseIf TabControl1.SelectedIndex = 2 Then
			Kosong_Tab_2()
		End If

		Cmb_Jenis_Approval.SelectedIndex = -1
		Cmb_Approval_Level.SelectedIndex = -1
		Cmb_Lokasi.SelectedIndex = -1
		Cmb_Peran.SelectedIndex = -1

		Cmb_Approval_Level.Text = ""
		Cmb_Lokasi.Text = ""
		Cmb_Peran.Text = ""

		switchAutoComplete = True
		Txt_Username.Text = ""
		Txt_Selected_UserID.Text = ""
		Txt_Jabatan.Text = ""
		Txt_No_HP.Text = ""
		switchAutoComplete = False

		Cmb_Jenis_Approval.Enabled = True
		Cmb_Lokasi.Enabled = False
		Cmb_Approval_Level.Enabled = False
		Txt_Username.Enabled = False
		Txt_Selected_UserID.Enabled = False
		Cmb_Peran.Enabled = False
		Txt_Jabatan.Enabled = False
		Txt_No_HP.Enabled = False

		Btn_Tambah.Tag = "TAMBAH"
		Btn_Tambah.Text = "&Tambah"

	End Sub

	Private Sub Lv_Review_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Review.KeyDown
		If e.KeyCode = Keys.Delete Then
			If Lv_Review.SelectedItems.Count > 0 Then
				For Each item As ListViewItem In Lv_Review.SelectedItems
					Lv_Review.Items.Remove(item)
				Next
			End If
		End If
	End Sub

	Private Sub Cmb_Approval_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Approval_Level.SelectedIndexChanged
		If Cmb_Approval_Level.SelectedIndex = 0 Then
			Cb_Flag_Koneksi.Checked = False
			Cb_Flag_Koneksi.Enabled = False
			arrId_User_Koneksi.Clear()
			Cmb_User_Koneksi.Items.Clear()
			Cmb_User_Koneksi.SelectedIndex = -1
		Else
			Cb_Flag_Koneksi.Checked = False
			Cb_Flag_Koneksi.Enabled = True
			Try
				OpenConn()

				Cmb_User_Koneksi.Items.Clear() : arrId_User_Koneksi.Clear()
				SQL = "SELECT b.ID_User_Desktop, c.UserName "
				SQL &= $"FROM N_EMI_Master_Hierarchy_Approval_Waste a "
				SQL &= $"inner join N_EMI_Master_Hierarchy_Approval_Waste_user b on a.Kode_Perusahaan = b.Kode_Perusahaan  "
				SQL &= $"and a.Jenis_Approval = b.Jenis_Approval and a.Approval_Level = b.Approval_Level and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
				SQL &= $"inner join Users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.ID_User_Desktop = c.UserID "
				SQL &= $"where a.Approval_Level = '1' "
				SQL &= $"and a.Kode_Stock_Owner = '{arrLokasi(Cmb_Lokasi.SelectedIndex)}' "
				SQL &= $"order by c.UserName "
				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						Cmb_User_Koneksi.Items.Add(Dr("UserName")) : arrId_User_Koneksi.Add(Dr("ID_User_Desktop"))
					Loop
				End Using

				Cmb_User_Koneksi.SelectedIndex = -1

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		End If
	End Sub

	Private Sub Cb_Flag_Koneksi_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Flag_Koneksi.CheckedChanged
		If Cb_Flag_Koneksi.Checked Then
			Cmb_User_Koneksi.Enabled = True
		Else
			Cmb_User_Koneksi.Enabled = False
		End If

		Cmb_User_Koneksi.SelectedIndex = -1

	End Sub

	'========================================================================================================================================
	'=     HANDLE KEYPRESS
	'========================================================================================================================================
	Private Sub Txt_No_HP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_HP.KeyPress
		If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
			e.Handled = True
		End If

	End Sub

	'========================================================================================================================================
	'=     UTILITY
	'========================================================================================================================================
	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

	'======================================
	'=     MENERAPKAN KONSEP DEBOUNCE     =
	'======================================
	Private Sub Txt_Filter_Lokasi_Pengajuan_TextChanged(sender As Object, e As EventArgs) Handles Txt_Filter_Lokasi_Pengajuan.TextChanged
		' Reset timer setiap ada ketikan
		TypingTimer.Stop()
		TypingTimer.Start()
	End Sub

	Private Sub Txt_Filter_Lokasi_Pemindahan_TextChanged(sender As Object, e As EventArgs) Handles Txt_Filter_Lokasi_Pemindahan.TextChanged
		' Reset timer setiap ada ketikan
		TypingTimer.Stop()
		TypingTimer.Start()
	End Sub

	Private Sub TypingTimer_Tick(sender As Object, e As EventArgs) Handles TypingTimer.Tick
		TypingTimer.Stop()

		'Dim keyword As String = Txt_Filter_Lokasi_Pengajuan.Text.Trim()

		'==============================================
		'=     FUNGSI RELOAD TAMPILKAN DATA ULANG     =
		'==============================================
		Load_Data_Tab1()
		Load_Data_Tab2()

	End Sub

	'====================================
	'=     FUNGSI UNTUK PLACEHOLDER     =
	'====================================

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)>
	Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As String) As IntPtr
	End Function

	Private Const EM_SETCUEBANNER As Integer = &H1501

	Private Sub SetCueBanner(tb As TextBox, text As String)
		SendMessage(tb.Handle, EM_SETCUEBANNER, CType(1, IntPtr), text)
	End Sub

End Class