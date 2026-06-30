Public Class N_EMI_Transaksi_Binding_Department

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim ItemDepartment_KdKategoriGudang As Integer = 0
	Dim ItemDepartment_KdStockOwnerGudang As Integer = 1
	Dim ItemDepartment_JenisGudang As Integer = 2
	Dim ItemDepartment_Keterangan As Integer = 3
	Dim ItemDepartment_StatusBinding As Integer = 4
	Dim ItemDepartment_ID As Integer = 5

	Dim DataFilter As New List(Of (ValueCombo As String, Sql As String)) From {
		(OpsiSeluruh, OpsiSeluruh),
		("Kode Binding", "a.Kode_Binding"),
		("Keterangan", "a.Keterangan")
	}

	Dim IsUpdate As Boolean = False

	Private Sub N_EMI_Transaksi_Binding_Department_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub N_EMI_Transaksi_Binding_Departement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		EnableDoubleBuffer(Lv_Department)
		EnableDoubleBuffer(Lv_Display_Parent)
		EnableDoubleBuffer(Lv_Display_Detail)

		Lv_Department.Columns.Clear()
		Lv_Department.Columns.Add("Kode Kategori Gudang", 180, HorizontalAlignment.Left)
		Lv_Department.Columns.Add("Lokasi Gudang", 180, HorizontalAlignment.Left)
		Lv_Department.Columns.Add("Jenis Gudang", 120, HorizontalAlignment.Center)
		Lv_Department.Columns.Add("Keterangan", 240, HorizontalAlignment.Left)
		Lv_Department.Columns.Add("Status Binding", 130, HorizontalAlignment.Center)
		Lv_Department.Columns.Add("ID", 0, HorizontalAlignment.Center)
		Lv_Department.View = View.Details

		Lv_Display_Parent.Columns.Clear()
		Lv_Display_Parent.Columns.Add("Kode Binding", 150, HorizontalAlignment.Left)
		Lv_Display_Parent.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
		Lv_Display_Parent.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
		Lv_Display_Parent.View = View.Details

		Lv_Display_Detail.Columns.Add("Kode Kategori Gudang", 150, HorizontalAlignment.Left)
		Lv_Display_Detail.Columns.Add("Lokasi Gudang", 150, HorizontalAlignment.Left)
		Lv_Display_Detail.Columns.Add("Jenis Gudang", 130, HorizontalAlignment.Left)
		Lv_Display_Detail.View = View.Details

		Cmb_Filter.Items.Clear()

		For Each item In DataFilter
			Cmb_Filter.Items.Add(item.ValueCombo)
		Next
		Cmb_Filter.SelectedIndex = 0

		Kosong()

	End Sub

	Private Sub Kosong()

		Lv_Department.Items.Clear()
		Lv_Display_Parent.Items.Clear()
		Lv_Display_Detail.Items.Clear()
		Txt_KodeBinding.Text = ""
		Txt_Keterangan.Text = ""

		Cmb_Filter.SelectedIndex = 0

		Txt_KodeBinding.Enabled = True
		Txt_KodeBinding.BackColor = Color.White

		IsUpdate = False

		Btn_Simpan.Tag = "SIMPAN"
		Btn_Simpan.Text = "&Simpan"

		TabControl1.SelectedIndex = 0

		LoadDisplay()
	End Sub

	Private Sub Btn_Get_Data_Click(sender As Object, e As EventArgs) Handles Btn_Get_Data.Click
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim ListKdkategori As New List(Of String)
			If IsUpdate And Txt_KodeBinding.Text.Trim.Length > 0 Then
				SQL = $"
					select b.Kode_Kategori_Gudang
					from N_EMI_Binding_Department a
						inner join N_EMI_Binding_Department_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
					where a.Status is null
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.Kode_Binding = '{Txt_KodeBinding.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						ListKdkategori.Add(Dr("Kode_Kategori_Gudang").ToString.Trim)
					Loop
				End Using
			End If

			Lv_Department.Items.Clear()
			SQL = $"
				select a.Kode_Kategori_Gudang, a.Kode_Stock_Owner_Gudang, a.Jenis_Gudang,
						a.Keterangan, isnulL(b.HasBinding, 'T') as Status_Binding, a.Urut_Oto
					from N_EMI_Master_Kategori_Gudang_Barang_Lain a
						left join (
							select a.Kode_Perusahaan, b.Kode_Kategori_Gudang, 'Y' as HasBinding
							from N_EMI_Binding_Department a
							inner join N_EMI_Binding_Department_Detail b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
							where a.Status is null
						)	b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Kategori_Gudang = b.Kode_Kategori_Gudang
					where a.Status is null
					and a.Jenis_Gudang = 'Department'
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					order by a.Kode_Stock_Owner_Gudang
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Lv As ListViewItem
						Lv = Lv_Department.Items.Add(Dr("Kode_Kategori_Gudang"))
						Lv.SubItems.Add(Dr("Kode_Stock_Owner_Gudang"))
						Lv.SubItems.Add(Dr("Jenis_Gudang"))
						Lv.SubItems.Add(Dr("Keterangan"))
						If General_Class.CekNULL(Dr("Status_Binding")) = "Y" Then
							Lv.SubItems.Add("Binding")
						Else
							Lv.SubItems.Add("Belum Binding")
						End If

						If IsUpdate And ListKdkategori.Count > 0 Then
							If ListKdkategori.Contains(Dr("Kode_Kategori_Gudang").ToString.Trim) Then
								Lv.Checked = True
							End If
						End If
						Lv.SubItems.Add(Dr("Urut_Oto"))
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan, Data Kategori Gudang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub LoadDisplay()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Filter As String = ""
			If Cmb_Filter.SelectedIndex > 0 Then
				Filter &= $"and {DataFilter(Cmb_Filter.SelectedIndex).Sql} like '%{Txt_filter.Text.Trim}%' "

			End If

			Lv_Display_Parent.Items.Clear() : Lv_Display_Detail.Items.Clear()
			SQL = $"
				select a.Kode_Binding, a.Keterangan, a.Tanggal
				from N_EMI_Binding_Department a
				where a.Status is null
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				order by a.Tanggal, a.Jam, a.Kode_Binding
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Lv As ListViewItem
						Lv = Lv_Display_Parent.Items.Add(Dr("Kode_Binding"))
						Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
						Lv.SubItems.Add(Dr("Keterangan"))
					Loop While Dr.Read
				End If
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Lv_Display_Parent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Display_Parent.SelectedIndexChanged
		If Lv_Display_Parent.Items.Count = 0 Or Lv_Display_Parent.FocusedItem Is Nothing Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim KodeBinding As String = Lv_Display_Parent.FocusedItem.SubItems(0).Text

			Lv_Display_Detail.Items.Clear()
			SQL = $"
				select Kode_Kategori_Gudang, Kode_Stock_Owner_Gudang, Jenis_Gudang
				from N_EMI_Binding_Department_Detail
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Binding = '{KodeBinding.Trim}'
				order by Kode_Kategori_Gudang, Jenis_Gudang
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Lv As ListViewItem
						Lv = Lv_Display_Detail.Items.Add(Dr("Kode_Kategori_Gudang"))
						Lv.SubItems.Add(Dr("Kode_Stock_Owner_Gudang"))
						Lv.SubItems.Add(Dr("Jenis_Gudang"))
					Loop While Dr.Read
				End If
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Txt_KodeBinding.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Binding Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KodeBinding.Focus()
			Exit Sub
		ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Keterangan.Focus()
			Exit Sub
		End If

		If Lv_Department.Items.Count = 0 Then
			MessageBox.Show("Terjadi Kesalahan, Data Department Tidak Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Btn_Get_Data.Focus()
			Exit Sub
		Else
			Dim HasChecked As Boolean = (Lv_Department.CheckedItems.Count > 0)
			If Not HasChecked Then
				MessageBox.Show("Harus Ada Minimal 1 Deparment Yang Dicheck", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Lv_Department.Focus()
				Exit Sub
			End If
		End If

		Dim Action As String = StrConv(Btn_Simpan.Tag.Trim, VbStrConv.ProperCase)
		If (MessageBox.Show($"Yakin Ingin {Action} Binding Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If Btn_Simpan.Tag.ToString.ToUpper.Trim = "SIMPAN" Then

				'=================================================
				'=     CEK APAKAH ADA KODE BINDING YANG SAMA     =
				'=================================================
				SQL = $"
					select top 1 1
					from N_EMI_Binding_Department
					where Status is null
					and Kode_Perusahaan = '{KodePerusahaan}'
					AND Kode_Binding COLLATE SQL_Latin1_General_CP1_CI_AS = '{Txt_KodeBinding.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Kode Binding Sudah Ada. Harap Ganti Kode Binding dan Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     SUSUN QUERY     =
				'=======================
				Dim SqlInsertList As New List(Of String)
				Dim ListGudang As New List(Of String)
				Dim HasData As Boolean = False
				For Each item As ListViewItem In Lv_Department.CheckedItems
					HasData = True

					Dim Sql As String = $"('{KodePerusahaan}', '{Txt_KodeBinding.Text.Trim}', '{item.SubItems(ItemDepartment_KdKategoriGudang).Text.Trim}',
										'{item.SubItems(ItemDepartment_KdStockOwnerGudang).Text.Trim}', '{item.SubItems(ItemDepartment_JenisGudang).Text.Trim}',
										'{item.SubItems(ItemDepartment_ID).Text.Trim}')"

					SqlInsertList.Add(Sql)
					ListGudang.Add($"'{item.SubItems(ItemDepartment_KdKategoriGudang).Text.Trim}'")
				Next

				If Not HasData Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan, Tidak Ada Data Detail yang Diinsert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				'================================================================
				'=     CEK APAKAH KODE KATEGORI GUDANG SUDAH PERNAH DIINPUT     =
				'================================================================
				Dim sqlLsitGudang As String = String.Join(", ", ListGudang)
				SQL = $"
					select Kode_Kategori_Gudang
					from N_EMI_Binding_Department a
						inner join N_EMI_Binding_Department_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
					where a.Status is null
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and b.Kode_Kategori_Gudang in ({sqlLsitGudang})
				"
				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						Dim Kdkategori As String = Dr("Kode_Kategori_Gudang")
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Kode kategori {Kdkategori} Sudah Diinput Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					Loop
				End Using

				'=======================
				'=     INSERT DATA     =
				'=======================
				Dim SqlDetail As String = String.Join(", ", SqlInsertList)
				SQL = $"
					insert into N_EMI_Binding_Department (Kode_Perusahaan, Kode_Binding, Keterangan, Tanggal, Jam, UserID)
					values ('{KodePerusahaan}', '{Txt_KodeBinding.Text.Trim}', '{Txt_Keterangan.Text.Trim}',
					'{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{UserID.Trim}')
				"
				ExecuteTrans(SQL)

				SQL = $"
					insert into N_EMI_Binding_Department_Detail (kode_perusahaan, kode_binding, kode_kategori_gudang, kode_stock_owner_gudang, jenis_gudang, ID_Kode_Kategori_Gudang)
					values {SqlDetail}
				"
				ExecuteTrans(SQL)

				Action = "simpan"
			ElseIf Btn_Simpan.Tag.ToString.ToUpper.Trim = "UPDATE" Then

				'===============================
				'=     CEK APAKAH ADA DATA     =
				'===============================
				SQL = $"
					select 1
					from N_EMI_Binding_Department
					where status is null
					and Kode_Perusahaan = '{KodePerusahaan}'
					and Kode_Binding = '{Txt_KodeBinding.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Kode Binding {Txt_KodeBinding.Text.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     UPDATE DATA     =
				'=======================
				SQL = $"
					update N_EMI_Binding_Department set Keterangan = '{Txt_Keterangan.Text.Trim}'
					where status is null
					and Kode_Perusahaan = '{KodePerusahaan}'
					and Kode_Binding = '{Txt_KodeBinding.Text.Trim}'
				"
				ExecuteTrans(SQL)

				SQL = $"
					delete from N_EMI_Binding_Department_Detail
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Kode_Binding = '{Txt_KodeBinding.Text.Trim}'
				"
				ExecuteTrans(SQL)

				'=======================
				'=     SUSUN QUERY     =
				'=======================
				Dim SqlInsertList As New List(Of String)
				Dim ListGudang As New List(Of String)
				Dim HasData As Boolean = False
				For Each item As ListViewItem In Lv_Department.CheckedItems
					HasData = True

					Dim Sql As String = $"('{KodePerusahaan}', '{Txt_KodeBinding.Text.Trim}', '{item.SubItems(ItemDepartment_KdKategoriGudang).Text.Trim}',
										'{item.SubItems(ItemDepartment_KdStockOwnerGudang).Text.Trim}', '{item.SubItems(ItemDepartment_JenisGudang).Text.Trim}',
										'{item.SubItems(ItemDepartment_ID).Text.Trim}')"

					SqlInsertList.Add(Sql)
					ListGudang.Add($" '{item.SubItems(ItemDepartment_KdKategoriGudang).Text.Trim}'")
				Next

				If Not HasData Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan, Tidak Ada Data Detail yang Diinsert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				'================================================================
				'=     CEK APAKAH KODE KATEGORI GUDANG SUDAH PERNAH DIINPUT     =
				'================================================================
				Dim sqlLsitGudang As String = String.Join(", ", ListGudang)
				SQL = $"
					select Kode_Kategori_Gudang
					from N_EMI_Binding_Department a
						inner join N_EMI_Binding_Department_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
					where a.Status is null
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and b.Kode_Kategori_Gudang in ({sqlLsitGudang})
				"
				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						Dim Kdkategori As String = Dr("Kode_Kategori_Gudang")
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Kode kategori {Kdkategori} Sudah Diinput Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					Loop
				End Using

				'==============================
				'=     INSERT DATA DETAIL     =
				'==============================
				Dim SqlDetail As String = String.Join(", ", SqlInsertList)

				SQL = $"
					insert into N_EMI_Binding_Department_Detail (kode_perusahaan, kode_binding, kode_kategori_gudang, kode_stock_owner_gudang, jenis_gudang, ID_Kode_Kategori_Gudang)
					values {SqlDetail}
				"
				ExecuteTrans(SQL)

				Action = "update"
			Else
				CloseTrans()
				CloseConn()
				MessageBox.Show("Terjadi kesalahan, Action Button Tidak Diketahui", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Data Berhasil Di{Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()

	End Sub

	Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click
		If Txt_KodeBinding.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Binding Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KodeBinding.Focus()
			Exit Sub
		End If

		If MessageBox.Show($"Yakin Ingin Mengahapus Data {Txt_KodeBinding.Text.Trim} ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===============================
			'=     CEK APAKAH ADA DATA     =
			'===============================
			SQL = $"
					select 1
					from N_EMI_Binding_Department
					where status is null
					and Kode_Perusahaan = '{KodePerusahaan}'
					and Kode_Binding = '{Txt_KodeBinding.Text.Trim}'
				"
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Kode Binding {Txt_KodeBinding.Text.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=================================================================
			'=     CEK APAKAH BINDING SUDAH DIPAKAI PADA BUDGET PLANNING     =
			'=================================================================

			SQL = $"
				select 1 from N_EMI_Transaksi_Budget_Planning
				where Status is null
				and Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Binding = '{Txt_KodeBinding.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Kode Binding {Txt_KodeBinding.Text.Trim} Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'======================
			'=     HAPUS DATA     =
			'======================
			SQL = $"
					delete from N_EMI_Binding_Department_Detail
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Kode_Binding = '{Txt_KodeBinding.Text.Trim}'
				"
			ExecuteTrans(SQL)

			SQL = $"
				delete from N_EMI_Binding_Department
				where status is null
				and Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Binding = '{Txt_KodeBinding.Text.Trim}'
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

	Private Sub Txt_KodeBinding_Leave(sender As Object, e As EventArgs) Handles Txt_KodeBinding.Leave
		If Txt_KodeBinding.Text.Trim.Length = 0 Then Exit Sub

		Dim hasData As Boolean = False
		Try
			OpenConn()

			SQL = $"
				select Kode_Binding, Keterangan
				from N_EMI_Binding_Department
				where Status is null
				and Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Binding = '{Txt_KodeBinding.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Txt_KodeBinding.Text = Dr("Kode_Binding")
					Txt_Keterangan.Text = Dr("Keterangan")

					Txt_KodeBinding.Enabled = False
					Txt_KodeBinding.BackColor = Color.FromArgb(235, 235, 235)

					Btn_Simpan.Tag = "UPDATE"
					Btn_Simpan.Text = "&Update"

					IsUpdate = True
					hasData = True
				Else
					IsUpdate = False
					hasData = False
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If hasData Then
			Btn_Get_Data_Click(sender, e)
		End If

	End Sub

	Private Sub Lv_Display_Parent_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Display_Parent.DoubleClick
		If Lv_Display_Parent.Items.Count = 0 Or Lv_Display_Parent.FocusedItem Is Nothing Then Exit Sub

		Txt_KodeBinding.Text = Lv_Display_Parent.FocusedItem.SubItems(0).Text.Trim

		Txt_KodeBinding_Leave(sender, e)
		TabControl1.SelectedIndex = 0

	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Cmb_Filter.SelectedIndex < 0 Then Exit Sub

		If Cmb_Filter.SelectedIndex > 1 Then
			If Txt_filter.Text.Trim.Length = 0 Then
				MessageBox.Show("Harap Isi Dahulu Value Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Txt_filter.Focus()
				Exit Sub
			End If
		End If

		LoadDisplay()

	End Sub

	Private Sub Txt_KodeBinding_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KodeBinding.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			Txt_Keterangan.Focus()
		End If
	End Sub

	Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Get_Data.Focus()
	End Sub

	Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
		If Cmb_Filter.Items.Count = 0 Then Exit Sub
		If Cmb_Filter.SelectedIndex = 0 Then
			Txt_filter.Enabled = False
			Txt_filter.BackColor = Color.FromArgb(235, 235, 235)
		Else
			Txt_filter.Enabled = True
			Txt_filter.BackColor = Color.White
		End If
		Txt_filter.Text = ""
	End Sub

	Private Sub Cmb_Filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Filter.SelectedIndex > 0 Then
				Txt_filter.Focus()
			End If
		End If
	End Sub

	Private Sub Txt_filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_filter.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
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

	Private Sub Lv_Department_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Department.MouseMove, Lv_Display_Parent.MouseMove, Lv_Display_Detail.MouseMove
		HandleListViewHover(sender, e)
	End Sub

End Class