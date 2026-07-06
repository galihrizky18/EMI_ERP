Public Class N_EMI_Laporan_Penambahan_Budget
	Dim arrBinding As New ArrayList
	Dim switchAutoComplete As Boolean = False

	Private Sub N_EMI_Laporan_Penambahan_Budget_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Lv_Jenis.Columns.Clear()
		Lv_Jenis.Columns.Add("Id_Kategori_jenis", 0, HorizontalAlignment.Left)
		Lv_Jenis.Columns.Add("Keterangan", 350, HorizontalAlignment.Left)
		Lv_Jenis.View = View.Details
		Lv_Jenis.FullRowSelect = True
		Lv_Jenis.GridLines = True
		Lv_Jenis.HideSelection = False

		Lv_Sub.Columns.Clear()
		Lv_Sub.Columns.Add("Id_Sub_Kategori_Jenis_1", 0, HorizontalAlignment.Left)
		Lv_Sub.Columns.Add("Keterangan", 350, HorizontalAlignment.Left)
		Lv_Sub.View = View.Details
		Lv_Sub.FullRowSelect = True
		Lv_Sub.GridLines = True
		Lv_Sub.HideSelection = False

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 220, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 180, HorizontalAlignment.Left)
		Lv_Barang.View = View.Details
		Lv_Barang.FullRowSelect = True
		Lv_Barang.GridLines = True
		Lv_Barang.HideSelection = False

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Cmb_Binding.Items.Clear()
			arrBinding.Clear()

			Cmb_Binding.Items.Add(OpsiSeluruh)
			arrBinding.Add(OpsiSeluruh)

			SQL = "SELECT Kode_Binding, Keterangan "
			SQL &= "FROM N_EMI_Binding_Department "
			SQL &= $"WHERE Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= "ORDER BY Kode_Binding "

			Using Dr = OpenTrans(SQL)
				While Dr.Read
					Cmb_Binding.Items.Add(Dr("Kode_Binding").ToString)
					arrBinding.Add(Dr("Keterangan").ToString)
				End While
			End Using

			Cmb_Binding.SelectedIndex = 0

			Cmb_Tgl.Items.Clear()
			Cmb_Tgl.Items.Add(OpsiSeluruh)
			Cmb_Tgl.Items.Add("Tanggal PR")
			Cmb_Tgl.Items.Add("Tanggal Validasi")
			Cmb_Tgl.SelectedIndex = 0

			Cmd.Transaction.Commit()

			Cmb_Lain.Items.Clear()
			Cmb_Lain.Items.Add(OpsiSeluruh)
			Cmb_Lain.Items.Add("No Faktur")
			Cmb_Lain.Items.Add("UserId")
			Cmb_Lain.SelectedIndex = 0

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

	Private Sub N_EMI_Laporan_Penambahan_Budget_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Kosong()
		Tgl1.Value = Date.Today
		Tgl2.Value = Date.Today

		Cmb_Tgl.SelectedIndex = 0
		Cmb_Binding.SelectedIndex = 0
		Cmb_Lain.SelectedIndex = 0

		switchAutoComplete = True

		Txt_Jenis.Text = OpsiSeluruh
		Txt_Jenis.Tag = Nothing
		Txt_Sub.Text = OpsiSeluruh
		Txt_Sub.Tag = Nothing
		Txt_KdBarang.Text = OpsiSeluruh
		Txt_NmBarang.Text = OpsiSeluruh

		switchAutoComplete = False

		Cmb_Tgl.Focus()
	End Sub

	Private Sub Txt_Jenis_TextChanged(sender As Object, e As EventArgs) Handles Txt_Jenis.TextChanged
		If switchAutoComplete Then Exit Sub

		'ukuran Lv_Jenis sebelum mengetik
		If Txt_Jenis.Text.Trim.Length = 0 Then
			Me.Size = New Size(657, 304)
			Lv_Jenis.Visible = False
			Lv_Jenis.Location = New Point(158, 144)
			Txt_Jenis.Text = ""
			Exit Sub
		Else
			'Ukuran Lv_Jenis setelah mengetik
			Me.Size = New Size(657, 304)
			Lv_Jenis.Visible = True
			Lv_Jenis.Location = New Point(158, 130)

			If Lv_Jenis.Items.Count > 2 Then

				Lv_Jenis.Items(2).Selected = True
			End If
		End If
		Try
			OpenConn()
			Lv_Jenis.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Jenis.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Cmd.Transaction = Cn.BeginTransaction

			SQL = "SELECT Id_Kategori_jenis, Keterangan " &
			  "FROM N_EMI_Master_Kategori_Jenis " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
			  $"AND (Keterangan LIKE '%{Txt_Jenis.Text.Trim}%' " &
			  $"OR Id_Kategori_jenis LIKE '%{Txt_Jenis.Text.Trim}%') " &
			  "ORDER BY Keterangan "

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Item As New ListViewItem(Dr("Id_Kategori_jenis").ToString)
						Item.SubItems.Add(Dr("Keterangan").ToString)
						Lv_Jenis.Items.Add(Item)
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseConn()
					Exit Sub
				End If
			End Using
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Jenis_Leave(sender As Object, e As EventArgs) Handles Txt_Jenis.Leave
		If Txt_Jenis.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Jenis.Focused = True Then Exit Sub
		If Txt_Jenis.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then
			Exit Sub
		End If
		If Txt_Jenis.Tag IsNot Nothing AndAlso Txt_Jenis.Tag.ToString.Trim.Length > 0 Then Exit Sub
		Try
			OpenConn()

			If Not Txt_Jenis.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				SQL = "SELECT Id_Kategori_jenis, Keterangan " &
			  "FROM N_EMI_Master_Kategori_Jenis " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
			  $"AND (Keterangan LIKE '%{Txt_Jenis.Text.Trim}%' " &
			  $"OR Id_Kategori_jenis LIKE '%{Txt_Jenis.Text.Trim}%') "

				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Do
							Dim Item As New ListViewItem(Dr("Id_Kategori_jenis").ToString)
							Item.SubItems.Add(Dr("Keterangan").ToString)
							Txt_Sub.Focus()
						Loop While Dr.Read
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Group Jenis tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_Jenis.Text = ""
						Txt_Jenis.Tag = Nothing
						Txt_Jenis.Focus()
						Exit Sub
					End If
				End Using
			Else
				Txt_Sub.Focus()
			End If
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Lv_Jenis_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Jenis.DoubleClick
		If Lv_Jenis.FocusedItem Is Nothing OrElse Lv_Jenis.FocusedItem.Index = -1 Then Exit Sub

		Dim idJenis As String = Lv_Jenis.FocusedItem.Text
		Dim keterangan As String = Lv_Jenis.FocusedItem.SubItems(1).Text

		switchAutoComplete = True
		Txt_Jenis.Text = keterangan
		Txt_Jenis.Tag = idJenis
		switchAutoComplete = False

		'Lv_jenis Setelah doubleclik
		Me.Size = New Size(657, 304)
		Lv_Jenis.Visible = False
		Lv_Jenis.Location = New Point(172, 196)

		Txt_Sub.Focus()
	End Sub

	Private Sub Txt_Sub_TextChanged(sender As Object, e As EventArgs) Handles Txt_Sub.TextChanged
		If switchAutoComplete Then Exit Sub

		If Txt_Sub.Text.Trim.Length = 0 Then
			Me.Size = New Size(657, 304)
			Lv_Sub.Visible = False
			Lv_Sub.Location = New Point(158, 170)
			Txt_Sub.Text = ""
			Txt_Sub.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(657, 304)
			Lv_Sub.Visible = True
			Lv_Sub.Location = New Point(158, 156)
		End If

		Try
			OpenConn()

			Lv_Sub.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Sub.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "SELECT Id_Sub_Kategori_Jenis_1, Keterangan " &
			  "FROM N_EMI_Master_Sub_Kategori_Jenis_1 " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
			  $"AND (Keterangan LIKE '%{Txt_Sub.Text.Trim}%' " &
			  $"OR Id_Sub_Kategori_Jenis_1 LIKE '%{Txt_Sub.Text.Trim}%') " &
			  "ORDER BY Keterangan "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Item As New ListViewItem(Dr("Id_Sub_Kategori_Jenis_1").ToString)
					Item.SubItems.Add(Dr("Keterangan").ToString)
					Lv_Sub.Items.Add(Item)
				Loop
			End Using
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Sub_Leave(sender As Object, e As EventArgs) Handles Txt_Sub.Leave
		If Txt_Sub.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Sub.Focused = True Then Exit Sub
		If Txt_Sub.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then
			Exit Sub
		End If
		If Txt_Sub.Tag IsNot Nothing AndAlso Txt_Sub.Tag.ToString.Trim.Length > 0 Then Exit Sub
		Try
			OpenConn()

			If Not Txt_Sub.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				SQL = "SELECT Id_Sub_Kategori_Jenis, Keterangan " &
			  "FROM N_EMI_Master_Sub_Kategori_Jenis_1 " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
			  $"AND (Keterangan LIKE '%{Txt_Sub.Text.Trim}%' " &
			  $"OR Id_Sub_Kategori_Jenis LIKE '%{Txt_Sub.Text.Trim}%') "

				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Do
							Dim Item As New ListViewItem(Dr("Id_Sub_Kategori_jenis").ToString)
							Item.SubItems.Add(Dr("Keterangan").ToString)
							Txt_Sub.Focus()
						Loop While Dr.Read
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Group Jenis tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_Sub.Text = ""
						Txt_Sub.Tag = Nothing
						Txt_Sub.Focus()
						Exit Sub
					End If
				End Using
			Else
				Txt_KdBarang.Focus()
			End If
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Lv_Sub_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Sub.DoubleClick
		If Lv_Sub.FocusedItem Is Nothing OrElse Lv_Sub.FocusedItem.Index = -1 Then Exit Sub

		Dim idSub As String = Lv_Sub.FocusedItem.Text
		Dim keterangan As String = Lv_Sub.FocusedItem.SubItems(1).Text

		switchAutoComplete = True
		Txt_Sub.Text = keterangan
		Txt_Sub.Tag = idSub
		switchAutoComplete = False

		'Lv_jenis Setelah doubleclik
		Me.Size = New Size(657, 304)
		Lv_Sub.Visible = False
		Lv_Sub.Location = New Point(172, 222)

		Txt_KdBarang.Focus()
	End Sub

	Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
		If switchAutoComplete Then Exit Sub
		'Lv_Barang Sebelum mengetik
		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(657, 304)
			Lv_Barang.Visible = False
			Exit Sub
		Else
			'Lv_Barang Setelah mengetik
			Me.Size = New Size(657, 349)
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(158, 182)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "SELECT DISTINCT Kode_Barang, Nama " &
			  "FROM Barang_Lain " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
			  $"AND (Kode_Barang LIKE '%{Txt_KdBarang.Text.Trim}%' " &
			  $"OR Nama LIKE '%{Txt_KdBarang.Text.Trim}%') " &
			  "ORDER BY Kode_Barang "

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Lv = Lv_Barang.Items.Add(Dr("Kode_Barang").ToString)
						Lv.SubItems.Add(Dr("Nama").ToString)
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					'MessageBox.Show("Kode barang Tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using
			CloseConn()
		Catch ex As Exception

			CloseConn()
			MessageBox.Show(ex.Message)

		End Try
	End Sub

	Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
		If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Barang.Focused = True Then Exit Sub
		If Lv_Barang.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then
			Exit Sub
		End If
		Try
			OpenConn()

			If Not Txt_KdBarang.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				SQL = "SELECT Kode_Barang, Nama "
				SQL &= "FROM Barang_Lain "
				SQL &= $"WHERE Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"AND (Kode_Barang = '{Txt_KdBarang.Text.Trim}' "
				SQL &= $"Or Nama Like '%{Txt_KdBarang.Text.Trim}%') "

				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Do
							switchAutoComplete = True
							Txt_KdBarang.Text = Dr("Kode_Barang").ToString
							Txt_NmBarang.Text = Dr("Nama").ToString
							switchAutoComplete = False
							Txt_lain.Focus()
						Loop While Dr.Read
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()

						MessageBox.Show("Barang tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_KdBarang.Text = ""
						Txt_NmBarang.Text = ""
						Txt_KdBarang.Focus()
						Exit Sub
					End If
				End Using
			Else
				switchAutoComplete = True
				Txt_KdBarang.Text = OpsiSeluruh
				Txt_NmBarang.Text = OpsiSeluruh
				switchAutoComplete = False
				Cmb_Lain.Focus()

			End If
			CloseConn()
		Catch ex As Exception
			CloseConn()

			MessageBox.Show(ex.Message)
			Exit Sub

		End Try
	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Then Exit Sub
		If Lv_Barang.FocusedItem Is Nothing Then Exit Sub

		Dim KodeBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim NamaBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

		switchAutoComplete = True

		Txt_KdBarang.Text = KodeBarang
		Txt_NmBarang.Text = NamaBarang

		switchAutoComplete = False
		'Lv_Barang Setelah double klik
		Me.Size = New Size(657, 304)
		Lv_Barang.Visible = False
		Lv_Barang.Location = New Point(635, 161)

		Cmb_Lain.Focus()
		Cmb_Lain.DroppedDown = True
	End Sub

	Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
		If switchAutoComplete Then Exit Sub
		'Lv_Barang Sebelum mengetik
		If Txt_NmBarang.Text.Trim.Length = 0 Then
			Me.Size = New Size(657, 304)
			Lv_Barang.Visible = False
			Exit Sub
		Else
			'Lv_Barang Setelah mengetik
			Me.Size = New Size(657, 349)
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(158, 182)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "SELECT DISTINCT Kode_Barang, Nama " &
			  "FROM Barang_Lain " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' " &
			  $"AND (Nama LIKE '%{Txt_NmBarang.Text.Trim}%' " &
			  $"OR Nama LIKE '%{Txt_NmBarang.Text.Trim}%') " &
			  "ORDER BY Nama "

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Lv = Lv_Barang.Items.Add(Dr("Kode_Barang").ToString)
						Lv.SubItems.Add(Dr("Nama").ToString)
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Nama Barang Tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception

			CloseConn()
			MessageBox.Show(ex.Message)

		End Try
	End Sub

	Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Txt_Jenis.Text.Trim.Length = 0 Then
			MessageBox.Show("Kategori Jenis harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Jenis.Focus() : Exit Sub
		End If
		If Txt_Sub.Text.Trim.Length = 0 Then
			MessageBox.Show("Sub Kategori 1 harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Sub.Focus() : Exit Sub
		End If
		If Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		End If
		If Cmb_Lain.SelectedIndex <> 0 AndAlso Txt_lain.Text.Trim.Length = 0 Then
			MessageBox.Show("Lain harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Lain.Focus()
			Cmb_Lain.DroppedDown = True
			Exit Sub
		End If

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction
			Dim SF As String = ""

			SQL = "SELECT Kode_Perusahaan " &
			  "FROM N_EMI_View_Laporan_Penambahan_Budget " &
			  $"WHERE Kode_Perusahaan = '{KodePerusahaan}' "

			If Cmb_Tgl.SelectedIndex = 1 Then

				SQL &= $"AND Tanggal_Pra_Release >= '{Format(Tgl1.Value, "yyyy-MM-dd")}' "
				SQL &= $"AND Tanggal_Pra_Release <= '{Format(Tgl2.Value, "yyyy-MM-dd")}' "

				SF = "{N_EMI_View_Laporan_Penambahan_Budget.Kode_Perusahaan} = '" & KodePerusahaan & "' "
				SF &= "And {N_EMI_View_Laporan_Penambahan_Budget.Tanggal_Pra_Release} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# "
				SF &= "And {N_EMI_View_Laporan_Penambahan_Budget.Tanggal_Pra_Release} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

			ElseIf Cmb_Tgl.SelectedIndex = 2 Then

				SQL &= $"AND Tgl_Validasi_Tambah_Pengajuan_Budget >= '{Format(Tgl1.Value, "yyyy-MM-dd")}' "
				SQL &= $"AND Tgl_Validasi_Tambah_Pengajuan_Budget <= '{Format(Tgl2.Value, "yyyy-MM-dd")}' "

				SF = "{N_EMI_View_Laporan_Penambahan_Budget.Kode_Perusahaan} = '" & KodePerusahaan & "' "
				SF &= "And {N_EMI_View_Laporan_Penambahan_Budget.Tgl_Validasi_Tambah_Pengajuan_Budget} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# "
				SF &= "And {N_EMI_View_Laporan_Penambahan_Budget.Tgl_Validasi_Tambah_Pengajuan_Budget} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "
			Else
				' Opsi Seluruh
				SF = "{N_EMI_View_Laporan_Penambahan_Budget.Kode_Perusahaan} = '" & KodePerusahaan & "' "
			End If

			If Cmb_Binding.SelectedIndex <> 0 Then
				SQL &= $"AND Kode_Binding = '{Cmb_Binding.Text}' "
				SF &= "And {N_EMI_View_Laporan_Penambahan_Budget.Kode_Binding} = '" & Cmb_Binding.Text & "' "
			End If
			If Not Txt_Jenis.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
				Dim idJenis As String = If(Txt_Jenis.Tag IsNot Nothing, Txt_Jenis.Tag.ToString.Trim, "")
				If idJenis.Length > 0 Then
					SQL &= "AND Id_Kategori_Jenis = " & idJenis & " "
					SF &= "And {N_EMI_View_Laporan_Penambahan_Budget.Id_Kategori_Jenis} = " & idJenis & " "
				End If
			End If
			If Not Txt_Sub.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
				Dim idSub As String = If(Txt_Sub.Tag IsNot Nothing, Txt_Sub.Tag.ToString.Trim, "")
				If idSub.Length > 0 Then
					SQL &= "AND Id_Sub_Kategori_Jenis_1 = " & idSub & " "
					SF &= "And {N_EMI_View_Laporan_Penambahan_Budget.Id_Sub_Kategori_Jenis_1} = " & idSub & " "
				End If
			End If
			If Not Txt_KdBarang.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then
				SQL &= $"AND Kode_Barang = '{Txt_KdBarang.Text.Trim}' "
				SF &= "And {N_EMI_View_Laporan_Penambahan_Budget.Kode_Barang} = '" & Txt_KdBarang.Text.Trim & "' "
			End If
			If Txt_lain.Text.Trim.Length > 0 Then
				If Cmb_Lain.SelectedIndex = 1 Then
					SQL &= "AND No_Faktur LIKE '%" & Txt_lain.Text.Trim & "%' "
					SF &= "And {N_EMI_View_Laporan_Penambahan_Budget.No_Faktur} Like '*" & Txt_lain.Text.Trim & "*' "
				ElseIf Cmb_Lain.SelectedIndex = 2 Then
					SQL &= "AND UserId LIKE '%" & Txt_lain.Text.Trim & "%' "
					SF &= "And {N_EMI_View_Laporan_Penambahan_Budget.UserId} Like '*" & Txt_lain.Text.Trim & "*' "
				End If
			End If

			Using DS = BindingTrans(SQL)
				With DS.Tables("MyTable")
					If .Rows.Count <> 0 Then

						Dim CrDoc As New N_EMI_CR_Laporan_Penambahan_Budget

						CrDoc.SetDataSource(DS)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						' Mengatur Judul Periode secara dinamis berdasarkan pilihan ComboBox
						Select Case Cmb_Tgl.SelectedIndex
							Case 0
								CrDoc.SummaryInfo.ReportTitle = "Periode : Seluruh Penambahan Budget"

							Case 1
								CrDoc.SummaryInfo.ReportTitle = "Periode Tanggal PR : " &
										Format(Tgl1.Value, "dd/MMM/yyyy") &
										" s/d " &
										Format(Tgl2.Value, "dd/MMM/yyyy")

							Case 2
								CrDoc.SummaryInfo.ReportTitle = "Periode Tanggal Validasi : " &
										Format(Tgl1.Value, "dd/MMM/yyyy") &
										" s/d " &
										Format(Tgl2.Value, "dd/MMM/yyyy")
						End Select
						CrDoc.RecordSelectionFormula = SF

						With A_Place_For_Printing2
							.Text = "Laporan Penambahan Budget"
							.CrystalReportViewer1.ReportSource = CrDoc
							.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
							.Refresh()
							.Show()
						End With
					Else

						CloseConn()
						MessageBox.Show("Budget Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub

					End If
				End With
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

	Private Sub Cmb_Tgl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Tgl.SelectedIndexChanged
		If Cmb_Tgl.Items.Count = 0 Then Exit Sub
		If Cmb_Tgl.SelectedIndex = 0 Then
			Tgl1.Enabled = False
			Tgl2.Enabled = False
		Else
			Tgl1.Enabled = True
			Tgl2.Enabled = True
		End If
	End Sub

	Private Sub Cmb_Tgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tgl.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			If Cmb_Tgl.SelectedIndex = 0 Then
				Cmb_Binding.Focus()
				Cmb_Binding.DroppedDown = True
			Else
				Tgl1.Focus()
			End If
		End If
	End Sub

	Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
		If e.KeyChar = Chr(13) Then
			Tgl2.Focus()
		End If
	End Sub

	Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Binding.Focus()
			Cmb_Binding.DroppedDown = True
		End If
	End Sub

	Private Sub Cmb_Binding_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Binding.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_Jenis.Focus()
		End If
	End Sub

	Private Sub Txt_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Jenis.KeyDown
		If e.KeyCode = Keys.Down Then
			e.Handled = True
			Lv_Jenis.Focus()
		End If
	End Sub

	Private Sub Txt_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Jenis.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			Txt_Sub.Focus()
		End If
	End Sub

	Private Sub Lv_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Jenis.KeyDown
		If e.KeyCode = Keys.Enter Then
			e.Handled = True
			Lv_Jenis_DoubleClick(sender, e)
		End If
	End Sub

	Private Sub Txt_Sub_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Sub.KeyDown
		If e.KeyCode = Keys.Down Then
			e.Handled = True
			Lv_Sub.Focus()
		End If
	End Sub

	Private Sub Txt_Sub_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Sub.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			Txt_KdBarang.Focus()
			Exit Sub
		End If
	End Sub

	Private Sub Lv_Sub_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Sub.KeyDown
		If e.KeyCode = Keys.Enter Then
			e.Handled = True
			Lv_Sub_DoubleClick(sender, e)
		End If
	End Sub

	Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			Cmb_Lain.Focus()
			Exit Sub
		End If
	End Sub

	Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
		If e.KeyCode = Keys.Down Then
			e.Handled = True
			Lv_Barang.Focus()
		End If
	End Sub

	Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			Cmb_Lain.Focus()
			Exit Sub
		End If
	End Sub

	Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
		If e.KeyCode = Keys.Down Then
			e.Handled = True
			Lv_Barang.Focus()
		End If
	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			e.Handled = True
			Lv_Barang_DoubleClick(sender, e)
		End If
	End Sub

	Private Sub Cmb_Lain_DropDownClosed(sender As Object, e As EventArgs) Handles Cmb_Lain.DropDownClosed
		If Cmb_Lain.SelectedIndex = 0 Then
			Txt_lain.Enabled = False
			Txt_lain.Text = ""
		Else
			Txt_lain.Enabled = True
			Txt_lain.Focus()
		End If
	End Sub

	Private Sub Cmb_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lain.KeyPress
		If e.KeyChar = Chr(13) Then
			e.Handled = True
			If Cmb_Lain.SelectedIndex = 0 Then
				BtnCetak.Focus()
			Else
				Txt_lain.Focus()
			End If
		End If
	End Sub

	Private Sub Txt_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_lain.KeyPress
		If e.KeyChar = Chr(13) Then
			BtnCetak.Focus()
		End If
	End Sub

	Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
		Me.Close()
	End Sub

End Class