Public Class N_EMI_SD_Master_Menu

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim Lv_Kategori, Lv_NamaRole, Lv_Keterangan As String

	Dim Item_Kategori As Integer = 0
	Dim Item_NamaRole As Integer = 1
	Dim Item_Keterangan As Integer = 2

	Dim SelectedUpdateRole, SelectedUpdateKategori As String

	Private Sub N_EMI_SD_Master_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBuffer(Lv_Data_Role)

		Lv_Data_Role.Columns.Clear()
		Lv_Data_Role.Columns.Add("Kategori", 120, HorizontalAlignment.Left)
		Lv_Data_Role.Columns.Add("Nama Role", 180, HorizontalAlignment.Left)
		Lv_Data_Role.Columns.Add("Keterangan", 270, HorizontalAlignment.Left)
		Lv_Data_Role.View = View.Details

		Kosong()

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Kosong()

		Btn_Simpan.Text = "&Simpan"
		Btn_Simpan.Tag = "SIMPAN"

		SelectedUpdateRole = ""
		SelectedUpdateKategori = ""

		Txt_Kategori.Text = ""
		Txt_NamaRole.Text = ""
		Txt_Keterangan.Text = ""

		Txt_Kategori.Focus()

		LoadDataRole()
	End Sub

	Private Sub GetDataLv(ByVal index As Integer)
		Lv_Kategori = Lv_Data_Role.Items(index).SubItems(Item_Kategori).Text
		Lv_NamaRole = Lv_Data_Role.Items(index).SubItems(Item_NamaRole).Text
		Lv_Keterangan = Lv_Data_Role.Items(index).SubItems(Item_Keterangan).Text
	End Sub

	Private Sub LoadDataRole()
		Try
			OpenConn()

			Lv_Data_Role.Items.Clear()
			SQL = $"
				select b.Form, a.ButtonName, a.Kategori, a.Keterangan
				from N_EMI_Master_Role_Button a
					inner join N_EMI_Master_Role_Button_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ButtonName = b.ButtonName
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and b.Form = '{Txt_CurrentForm.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Data_Role.Items.Add(Dr("Kategori"))
					Lv.SubItems.Add(Dr("ButtonName"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan")) = "", "", Dr("Keterangan")))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Txt_CurrentForm.Text.Trim.Length = 0 Then
			MessageBox.Show("Terjadi Kesalahan, Nama Form Tidak terselect", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt_Kategori.Text.Trim.Length = 0 Then
			MessageBox.Show("Kategori Harus Diisi Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kategori.Focus()
			Exit Sub
		ElseIf Txt_NamaRole.Text.Trim.Length = 0 Then
			MessageBox.Show("Nama Role Harus Diisi Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_NamaRole.Focus()
			Exit Sub
		End If

		If MessageBox.Show("Yakin Ingin Melakukan Simpan Role Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim ValueKeterangan As String = If(Txt_Keterangan.Text.Trim.Length = 0, "NULL", $"'{Txt_Keterangan.Text.Trim}'")

			If Btn_Simpan.Tag = "SIMPAN" Then

				'==============================================
				'=     CEK APAKAH ADA NAMA ROLE YANG SAMA     =
				'==============================================
				SQL = $"
					select top 1 1
					from N_EMI_Master_Role_Button
					WHERE Kode_Perusahaan = '{KodePerusahaan}'
					AND ButtonName = '{Txt_NamaRole.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Nama Role {Txt_NamaRole.Text.Trim} Sudah Ada. Harap Ganti Nama Role", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=========================
				'=     INSERT PARENT     =
				'=========================
				SQL = $"
					insert into N_EMI_Master_Role_Button (Kode_Perusahaan, ButtonName, Kategori, Keterangan)
					values('{KodePerusahaan}', '{Txt_NamaRole.Text.Trim}', '{Txt_Kategori.Text.Trim}', {ValueKeterangan})
				"
				ExecuteTrans(SQL)

				'=========================
				'=     INSERT DETAIL     =
				'=========================
				SQL = $"
					insert into N_EMI_Master_Role_Button_Detail (Kode_Perusahaan, ButtonName, Form)
					values('{KodePerusahaan}', '{Txt_NamaRole.Text.Trim}', '{Txt_CurrentForm.Text.Trim}')
				"
				ExecuteTrans(SQL)

			ElseIf Btn_Simpan.Tag = "UPDATE" Then

				If String.IsNullOrWhiteSpace(SelectedUpdateRole) Or String.IsNullOrWhiteSpace(SelectedUpdateRole) Then
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Role Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				SQL = $"
					select 1
					from N_EMI_Master_Role_Button
					where Kode_Perusahaan = '{KodePerusahaan}'
					and ButtonName = '{SelectedUpdateRole}'
					and Kategori = '{SelectedUpdateKategori}'
				"
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Nama Role {SelectedUpdateRole} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'===========================================
				'=     CEK APAKAH ROLE SUDAH DIGUNAKAN     =
				'===========================================
				SQL = $"
					select 1
					from Role_Button
					where Kode_Perusahaan = '{KodePerusahaan}'
					and ButtonName = '{SelectedUpdateRole}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Role {SelectedUpdateRole} Sudah Digunakann", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				If Txt_NamaRole.Text.Trim <> SelectedUpdateRole Then
					SQL = $"
						update N_EMI_Master_Role_Button_Detail set ButtonName = '{Txt_NamaRole.Text.Trim}'
						where Kode_Perusahaan = '{KodePerusahaan}'
						and ButtonName = '{SelectedUpdateRole}'
						and Form = '{Txt_CurrentForm.Text.Trim}'
					"
					ExecuteTrans(SQL)
				End If

				SQL = $"
					update N_EMI_Master_Role_Button set Kategori = '{Txt_Kategori.Text.Trim}',
					ButtonName = '{Txt_NamaRole.Text.Trim}', Keterangan = '{Txt_Keterangan.Text.Trim}'
					where Kode_Perusahaan = '{KodePerusahaan}'
					and ButtonName = '{SelectedUpdateRole}'
					and Kategori = '{SelectedUpdateKategori}'
				"
				ExecuteTrans(SQL)

			End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Diinput", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()
	End Sub

	Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click

		If String.IsNullOrEmpty(SelectedUpdateRole) Then
			MessageBox.Show("Terjadi kesalahan, harap pilih dahulu role yang ingin di delete", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show($"Yakin Ingin Hapus Role {SelectedUpdateRole} Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===========================================
			'=     CEK APAKAH ROLE SUDAH DIGUNAKAN     =
			'===========================================
			SQL = $"
				select 1
				from Role_Button
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ButtonName = '{SelectedUpdateRole}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Role {SelectedUpdateRole} Sudah Digunakann", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=========================
			'=     CEK DATA ROLE     =
			'=========================
			SQL = $"
				select 1
				from N_EMI_Master_Role_Button
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ButtonName = '{SelectedUpdateRole}'
				and Kategori = '{SelectedUpdateKategori}'
			"
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Nama Role {SelectedUpdateRole} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================
			'=     DELETE DATA     =
			'=======================
			SQL = $"
				select 1
				from N_EMI_Master_Role_Button_Detail
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ButtonName = '{SelectedUpdateRole}'
				and Form = '{Txt_CurrentForm.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()

					SQL = $"
						delete from N_EMI_Master_Role_Button_Detail
						where Kode_Perusahaan = '{KodePerusahaan}'
						and ButtonName = '{SelectedUpdateRole}'
						and Form = '{Txt_CurrentForm}'
					"
					ExecuteTrans(SQL)
				Else

					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Nama Role {SelectedUpdateRole} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = $"
				delete from N_EMI_Master_Role_Button
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ButtonName = '{SelectedUpdateRole}'
				and Kategori = '{SelectedUpdateKategori}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()
	End Sub

	Private Sub Lv_Data_Role_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data_Role.DoubleClick
		If Lv_Data_Role.Items.Count = 9 Then Exit Sub

		GetDataLv(Lv_Data_Role.FocusedItem.Index)

		Txt_Kategori.Text = Lv_Kategori
		Txt_NamaRole.Text = Lv_NamaRole
		Txt_Keterangan.Text = Lv_Keterangan

		SelectedUpdateRole = Lv_NamaRole
		SelectedUpdateKategori = Lv_Kategori

		Btn_Simpan.Text = "&Update"
		Btn_Simpan.Tag = "UPDATE"

	End Sub

	Private Sub Lv_Data_Role_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data_Role.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	'=======================================================================================================================================
	'=     HELPER
	'=======================================================================================================================================
	Protected Overrides Sub WndProc(ByRef m As Message)
		If m.Msg = &HA3 Then
			Return
		End If

		MyBase.WndProc(m)
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

				Dim amt As Integer = 10
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