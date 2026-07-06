Public Class N_EMI_Master_Jenis_Barang

	Dim menuItems As String() = {
		"Jenis Makanan", "Jenis Kemasan", "Jenis Kualitas"
	}

	Private activeLine As Panel = Nothing

	Dim Arr_Filter_Jenis_Makanan, Arr_Filter_Jenis_Kemasan, Arr_Filter_Jenis_Kualitas As New ArrayList

	Dim Lv_Makanan_ID, Lv_Makanan_Prefix, Lv_Makanan_KodeJenisMakanan, Lv_Makanan_Keterangan As String

	Dim Item_Makanan_ID As Integer = 0
	Dim Item_Makanan_Prefix As Integer = 1
	Dim Item_Makanan_KodeJenisMakanan As Integer = 2
	Dim Item_Makanan_Keterangan As Integer = 3

	Dim Lv_Kemasan_ID, Lv_Kemasan_Prefix, Lv_Kemasan_KodeJenisKemasan, Lv_Kemasan_Keterangan As String

	Dim Item_Kemasan_ID As Integer = 0
	Dim Item_Kemasan_Prefix As Integer = 1
	Dim Item_Kemasan_KodeJenisKemasan As Integer = 2
	Dim Item_Kemasan_Keterangan As Integer = 3

	Dim Lv_Kualitas_ID, Lv_Kualitas_Prefix, Lv_Kualitas_KodeJenisKualitas, Lv_Kualitas_Keterangan As String

	Dim Item_Kualitas_ID As Integer = 0
	Dim Item_Kualitas_Prefix As Integer = 1
	Dim Item_Kualitas_KodeJenisKualitas As Integer = 2
	Dim Item_Kualitas_Keterangan As Integer = 3

	Private Sub N_EMI_Master_Jenis_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		EnableDoubleBuffer(Lv_Jenis_Makanan)
		EnableDoubleBuffer(Lv_Jenis_Kemasan)
		EnableDoubleBuffer(Lv_Jenis_Kualitas)

		Try
			OpenConn()

			'=========================================================================================================================
			'=     MENU JENIS MAKANAN
			'=========================================================================================================================
			Lv_Jenis_Makanan.Columns.Clear()
			Lv_Jenis_Makanan.Columns.Add("Id", 0, HorizontalAlignment.Left)
			Lv_Jenis_Makanan.Columns.Add("Prefix", 90, HorizontalAlignment.Center)
			Lv_Jenis_Makanan.Columns.Add("Kode Jenis Makanan", 300, HorizontalAlignment.Left)
			Lv_Jenis_Makanan.Columns.Add("Keterangan", 520, HorizontalAlignment.Left)
			Lv_Jenis_Makanan.View = View.Details

			Cmb_Makanan_Kolom.Items.Clear() : Arr_Filter_Jenis_Makanan.Clear()
			Cmb_Makanan_Kolom.Items.Add("Prefix") : Arr_Filter_Jenis_Makanan.Add("Prefix_Jenis_Makanan")
			Cmb_Makanan_Kolom.Items.Add("Kode Jenis Makanan") : Arr_Filter_Jenis_Makanan.Add("Kode_Jenis_Makanan")
			Cmb_Makanan_Kolom.Items.Add("Keterangan") : Arr_Filter_Jenis_Makanan.Add("Keterangan")

			'=========================================================================================================================
			'=     MENU JENIS KEMASAN
			'=========================================================================================================================
			Lv_Jenis_Kemasan.Columns.Clear()
			Lv_Jenis_Kemasan.Columns.Add("Id", 0, HorizontalAlignment.Left)
			Lv_Jenis_Kemasan.Columns.Add("Prefix", 90, HorizontalAlignment.Center)
			Lv_Jenis_Kemasan.Columns.Add("Kode Jenis Kemasan", 300, HorizontalAlignment.Left)
			Lv_Jenis_Kemasan.Columns.Add("Keterangan", 520, HorizontalAlignment.Left)
			Lv_Jenis_Kemasan.View = View.Details

			Cmb_Kemasan_Kolom.Items.Clear() : Arr_Filter_Jenis_Kemasan.Clear()
			Cmb_Kemasan_Kolom.Items.Add("Prefix") : Arr_Filter_Jenis_Kemasan.Add("Prefix_Jenis_Kemasan")
			Cmb_Kemasan_Kolom.Items.Add("Kode Jenis Makanan") : Arr_Filter_Jenis_Kemasan.Add("Kode_Jenis_Kemasan")
			Cmb_Kemasan_Kolom.Items.Add("Keterangan") : Arr_Filter_Jenis_Kemasan.Add("Keterangan")

			'=========================================================================================================================
			'=     MENU JENIS KUALITAS
			'=========================================================================================================================

			Lv_Jenis_Kualitas.Columns.Clear()
			Lv_Jenis_Kualitas.Columns.Add("Id", 0, HorizontalAlignment.Left)
			Lv_Jenis_Kualitas.Columns.Add("Prefix", 90, HorizontalAlignment.Center)
			Lv_Jenis_Kualitas.Columns.Add("Kode Jenis Kualitas", 300, HorizontalAlignment.Left)
			Lv_Jenis_Kualitas.Columns.Add("Keterangan", 520, HorizontalAlignment.Left)
			Lv_Jenis_Kualitas.View = View.Details

			Cmb_Kualitas_Kolom.Items.Clear() : Arr_Filter_Jenis_Kualitas.Clear()
			Cmb_Kualitas_Kolom.Items.Add("Prefix") : Arr_Filter_Jenis_Kualitas.Add("Prefix_Jenis_Kualitas")
			Cmb_Kualitas_Kolom.Items.Add("Kode Jenis Kualitas") : Arr_Filter_Jenis_Kualitas.Add("Kode_Jenis_Kualitas")
			Cmb_Kualitas_Kolom.Items.Add("Keterangan") : Arr_Filter_Jenis_Kualitas.Add("Keterangan")

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try

		LoadTabMenu()
		Kosong()
	End Sub

	Private Sub Kosong()

	End Sub

	Private Sub HandleClickMenu(ByVal MenuTag As String)
		If MenuTag Is Nothing Then Exit Sub

		Select Case MenuTag
			Case "Jenis Makanan"
				Tab_Control.SelectedIndex = 0
				Kosong_Jenis_Makanan()
			Case "Jenis Kemasan"
				Tab_Control.SelectedIndex = 1
				Kosong_Jenis_Kemasan()
			Case "Jenis Kualitas"
				Tab_Control.SelectedIndex = 2
				Kosong_Jenis_Kualitas()
		End Select
	End Sub

	'=========================================================================================================================
	'=     MENU JENIS MAKANAN
	'=========================================================================================================================

#Region "JENIS MAKANAN"

	Private Sub Kosong_Jenis_Makanan()

		Txt_Makanan_Kode.Text = ""
		Txt_Makanan_Keterangan.Text = ""
		Txt_Makanan_Prefix.Text = ""
		Txt_Makanan_SelectedID.Text = ""

		Btn_Makanan_Simpan.Tag = "SIMPAN"
		Btn_Makanan_Simpan.Text = "&Simpan"

		Txt_Makanan_Kode.Enabled = True

		Cmb_Makanan_Kolom.SelectedIndex = -1
		Txt_Makanan_Value.Text = ""

		Try
			OpenConn()

			GetCurrentPrefix_Makanan()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		LoadData_JenisMakanan()
	End Sub

	Private Sub GetCurrentPrefix_Makanan()
		Txt_Makanan_Prefix.Text = ""
		SQL = $"
				select isnull(max(Prefix_Jenis_Makanan), 0) as Prefix
				from N_EMI_Master_Jenis_Makanan
				where Kode_Perusahaan = '{KodePerusahaan}'
			"
		Using Dr = OpenTrans(SQL)
			If Dr.Read Then
				Txt_Makanan_Prefix.Text = Val(HilangkanTanda(Dr("Prefix"))) + 1
			End If
		End Using
	End Sub

	Private Sub GetData_Makanan(ByVal index As Integer)
		Lv_Makanan_ID = Lv_Jenis_Makanan.Items(index).SubItems(Item_Makanan_ID).Text
		Lv_Makanan_Prefix = Lv_Jenis_Makanan.Items(index).SubItems(Item_Makanan_Prefix).Text
		Lv_Makanan_KodeJenisMakanan = Lv_Jenis_Makanan.Items(index).SubItems(Item_Makanan_KodeJenisMakanan).Text
		Lv_Makanan_Keterangan = Lv_Jenis_Makanan.Items(index).SubItems(Item_Makanan_Keterangan).Text
	End Sub

	Private Sub LoadData_JenisMakanan()
		Try
			OpenConn()

			Dim Filter As String = ""
			If Cmb_Makanan_Kolom.SelectedIndex <> -1 Then
				Filter &= $"and {Arr_Filter_Jenis_Makanan(Cmb_Makanan_Kolom.SelectedIndex)} like '%{Txt_Makanan_Value.Text.Trim}%' "
			End If

			Lv_Jenis_Makanan.Items.Clear()
			SQL = $"
				select Id_Jenis_Makanan, Kode_Jenis_Makanan, Prefix_Jenis_Makanan, Keterangan
				from N_EMI_Master_Jenis_Makanan
				where Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				order by Id_Jenis_Makanan
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Jenis_Makanan.Items.Add(Dr("Id_Jenis_Makanan"))
					Lv.SubItems.Add(Dr("Prefix_Jenis_Makanan"))
					Lv.SubItems.Add(Dr("Kode_Jenis_Makanan"))
					Lv.SubItems.Add(Dr("Keterangan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Makanan_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Makanan_Simpan.Click
		If Txt_Makanan_Kode.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Jenis Makanan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Makanan_Kode.Focus()
			Exit Sub
		ElseIf Txt_Makanan_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Jenis Makanan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Makanan_Keterangan.Focus()
			Exit Sub
		ElseIf Txt_Makanan_Prefix.Text.Trim.Length = 0 Then
			MessageBox.Show("Prefix Jenis Makanan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Makanan_Prefix.Focus()
			Exit Sub
		End If

		If (MessageBox.Show("Yakin Ingin Melakuakn Simpan Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Action As String = ""

			If Btn_Makanan_Simpan.Tag = "SIMPAN" Then

				GetCurrentPrefix_Makanan()

				'===========================================
				'=     CEK APAKAH ADA PREFIX YANG SAMA     =
				'===========================================
				SQL = $"
					select 1
					from N_EMI_Master_Jenis_Makanan
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Prefix_Jenis_Makanan = '{Txt_Makanan_Prefix.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Data jenis Makanan Dengan Prefix {Txt_Makanan_Prefix.Text.Trim} Sudah Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     INSERT DATA     =
				'=======================
				SQL = $"
					insert into N_EMI_Master_Jenis_Makanan (Kode_Perusahaan, Kode_Jenis_Makanan, Prefix_Jenis_Makanan, Keterangan)
					values ('{KodePerusahaan}', '{Txt_Makanan_Kode.Text.Trim}', '{Txt_Makanan_Prefix.Text.Trim}', '{Txt_Makanan_Keterangan.Text.Trim}')
				"
				ExecuteTrans(SQL)

				Action = "simpan"

			ElseIf Btn_Makanan_Simpan.Tag = "UPDATE" Then

				If Txt_Makanan_SelectedID.Text.Trim.Length = 0 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Harap Pilih Data Dahulu yang Ingin Diupdate", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				'===============================
				'=     CEK APAKAH ADA DATA     =
				'===============================
				SQL = $"
					select 1
					from N_EMI_Master_Jenis_Makanan
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Jenis_Makanan = '{Txt_Makanan_SelectedID.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     UDPATE DATA     =
				'=======================
				SQL = $"
					update N_EMI_Master_Jenis_Makanan set keterangan = '{Txt_Makanan_Keterangan.Text.Trim}'
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Jenis_Makanan = '{Txt_Makanan_SelectedID.Text.Trim}'
				"
				ExecuteTrans(SQL)

				Action = "update"
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

		Kosong_Jenis_Makanan()

	End Sub

	Private Sub Btn_Makanan_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Makanan_Refresh.Click
		Kosong_Jenis_Makanan()
	End Sub

	Private Sub Btn_Makanan_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Makanan_Hapus.Click
		If Txt_Makanan_SelectedID.Text.Trim.Length = 0 Then
			MessageBox.Show("Pilih Dahulu Data yang Ingin Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If (MessageBox.Show("Yakin Ingin Menghapus Data Ini???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===============================
			'=     CEK APAKAH DATA ADA     =
			'===============================
			SQL = $"
					select 1
					from N_EMI_Master_Jenis_Makanan
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Jenis_Makanan = '{Txt_Makanan_SelectedID.Text.Trim}'
				"
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'===================================================
			'=     CEK APAKAH DATA SUDAH DI PAKAI DIBARANG     =
			'===================================================
			SQL = $"
				select top 1 1
				from Barang
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Id_Jenis_Makanan = '{Txt_Makanan_SelectedID.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Tidak Bisa Dihapus Karena Sudah Dipakai pada Data Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'======================
			'=     HAPUS DATA     =
			'======================
			SQL = $"
				delete from N_EMI_Master_Jenis_Makanan
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Id_Jenis_Makanan = '{Txt_Makanan_SelectedID.Text.Trim}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong_Jenis_Makanan()
	End Sub

	Private Sub Cmb_Makanan_Kolom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Makanan_Kolom.SelectedIndexChanged
		If Cmb_Makanan_Kolom.SelectedIndex = -1 Then
			Txt_Makanan_Value.Enabled = False
		Else
			Txt_Makanan_Value.Enabled = True
		End If

		Txt_Makanan_Value.Text = ""
	End Sub

	Private Sub Btn_Makanan_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Makanan_Cari.Click
		LoadData_JenisMakanan()
	End Sub

	Private Sub Lv_Jenis_Makanan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Jenis_Makanan.DoubleClick
		If Lv_Jenis_Makanan.Items.Count = 0 Then Exit Sub

		GetData_Makanan(Lv_Jenis_Makanan.FocusedItem.Index)

		Txt_Makanan_Kode.Text = Lv_Makanan_KodeJenisMakanan.Trim
		Txt_Makanan_Keterangan.Text = Lv_Makanan_Keterangan.Trim
		Txt_Makanan_Prefix.Text = Lv_Makanan_Prefix.Trim
		Txt_Makanan_SelectedID.Text = Lv_Makanan_ID.Trim

		Txt_Makanan_Kode.Enabled = False

		Btn_Makanan_Simpan.Tag = "UPDATE"
		Btn_Makanan_Simpan.Text = "&Update"
	End Sub

#End Region

	'=========================================================================================================================
	'=     MENU JENIS KEMASAN
	'=========================================================================================================================

#Region "KEMASAN"

	Private Sub Kosong_Jenis_Kemasan()

		Txt_Kemasan_Kode.Text = ""
		Txt_Kemasan_Keterangan.Text = ""
		Txt_Kemasan_Prefix.Text = ""
		Txt_Kemasan_SelectedID.Text = ""

		Btn_Kemasan_Simpan.Tag = "SIMPAN"
		Btn_Kemasan_Simpan.Text = "&Simpan"

		Txt_Kemasan_Kode.Enabled = True

		Cmb_Kemasan_Kolom.SelectedIndex = -1
		Txt_Kemasan_Value.Text = ""

		Try
			OpenConn()

			GetCurrentPrefix_Kemasan()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		LoadData_JenisKemasan()
	End Sub

	Private Sub GetCurrentPrefix_Kemasan()
		Txt_Makanan_Prefix.Text = ""
		SQL = $"
				select isnull(max(Prefix_Jenis_Kemasan), 0) as Prefix
				from N_EMI_Master_Jenis_Kemasan
				where Kode_Perusahaan = '{KodePerusahaan}'
			"
		Using Dr = OpenTrans(SQL)
			If Dr.Read Then
				Txt_Kemasan_Prefix.Text = Val(HilangkanTanda(Dr("Prefix"))) + 1
			End If
		End Using
	End Sub

	Private Sub GetData_Kemasan(ByVal index As Integer)
		Lv_Kemasan_ID = Lv_Jenis_Kemasan.Items(index).SubItems(Item_Kemasan_ID).Text
		Lv_Kemasan_Prefix = Lv_Jenis_Kemasan.Items(index).SubItems(Item_Kemasan_Prefix).Text
		Lv_Kemasan_KodeJenisKemasan = Lv_Jenis_Kemasan.Items(index).SubItems(Item_Kemasan_KodeJenisKemasan).Text
		Lv_Kemasan_Keterangan = Lv_Jenis_Kemasan.Items(index).SubItems(Item_Kemasan_Keterangan).Text
	End Sub

	Private Sub LoadData_JenisKemasan()
		Try
			OpenConn()

			Dim Filter As String = ""
			If Cmb_Kemasan_Kolom.SelectedIndex <> -1 Then
				Filter &= $"and {Arr_Filter_Jenis_Kemasan(Cmb_Kemasan_Kolom.SelectedIndex)} like '%{Txt_Kemasan_Value.Text.Trim}%' "
			End If

			Lv_Jenis_Kemasan.Items.Clear()
			SQL = $"
				select Id_Jenis_Kemasan, Kode_Jenis_Kemasan, Prefix_Jenis_Kemasan, Keterangan
				from N_EMI_Master_Jenis_Kemasan
				where Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				order by Id_Jenis_Kemasan
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Jenis_Kemasan.Items.Add(Dr("Id_Jenis_Kemasan"))
					Lv.SubItems.Add(Dr("Prefix_Jenis_Kemasan"))
					Lv.SubItems.Add(Dr("Kode_Jenis_Kemasan"))
					Lv.SubItems.Add(Dr("Keterangan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Kemasan_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Kemasan_Simpan.Click
		If Txt_Kemasan_Kode.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Jenis Kemasan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kemasan_Kode.Focus()
			Exit Sub
		ElseIf Txt_Kemasan_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Jenis Kemasan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kemasan_Keterangan.Focus()
			Exit Sub
		ElseIf Txt_Kemasan_Prefix.Text.Trim.Length = 0 Then
			MessageBox.Show("Prefix Jenis Kemasan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kemasan_Prefix.Focus()
			Exit Sub
		End If

		If (MessageBox.Show("Yakin Ingin Melakuakn Simpan Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Action As String = ""

			If Btn_Kemasan_Simpan.Tag = "SIMPAN" Then

				GetCurrentPrefix_Kemasan()

				'===========================================
				'=     CEK APAKAH ADA PREFIX YANG SAMA     =
				'===========================================
				SQL = $"
					select 1
					from N_EMI_Master_Jenis_Kemasan
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Prefix_Jenis_Kemasan = '{Txt_Kemasan_Prefix.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Data jenis Kemasan Dengan Prefix {Txt_Kemasan_Prefix.Text.Trim} Sudah Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     INSERT DATA     =
				'=======================
				SQL = $"
					insert into N_EMI_Master_Jenis_Kemasan (Kode_Perusahaan, Kode_Jenis_Kemasan, Prefix_Jenis_Kemasan, Keterangan)
					values ('{KodePerusahaan}', '{Txt_Kemasan_Kode.Text.Trim}', '{Txt_Kemasan_Prefix.Text.Trim}', '{Txt_Kemasan_Keterangan.Text.Trim}')
				"
				ExecuteTrans(SQL)

				Action = "simpan"

			ElseIf Btn_Kemasan_Simpan.Tag = "UPDATE" Then

				If Txt_Kemasan_SelectedID.Text.Trim.Length = 0 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Harap Pilih Data Dahulu yang Ingin Diupdate", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				'===============================
				'=     CEK APAKAH ADA DATA     =
				'===============================
				SQL = $"
					select 1
					from N_EMI_Master_Jenis_Kemasan
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Jenis_Kemasan = '{Txt_Kemasan_SelectedID.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     UDPATE DATA     =
				'=======================
				SQL = $"
					update N_EMI_Master_Jenis_Kemasan set keterangan = '{Txt_Kemasan_Keterangan.Text.Trim}'
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Jenis_Kemasan = '{Txt_Kemasan_SelectedID.Text.Trim}'
				"
				ExecuteTrans(SQL)

				Action = "update"
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

		Kosong_Jenis_Kemasan()

	End Sub

	Private Sub Btn_Kemasan_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Kemasan_Refresh.Click
		Kosong_Jenis_Kemasan()
	End Sub

	Private Sub Btn_Kemasan_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Kemasan_Hapus.Click
		If Txt_Kemasan_SelectedID.Text.Trim.Length = 0 Then
			MessageBox.Show("Pilih Dahulu Data yang Ingin Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If (MessageBox.Show("Yakin Ingin Menghapus Data Ini???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===============================
			'=     CEK APAKAH DATA ADA     =
			'===============================
			SQL = $"
					select 1
					from N_EMI_Master_Jenis_Kemasan
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Jenis_Kemasan = '{Txt_Kemasan_SelectedID.Text.Trim}'
				"
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'===================================================
			'=     CEK APAKAH DATA SUDAH DI PAKAI DIBARANG     =
			'===================================================
			SQL = $"
				select top 1 1
				from Barang
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Id_Jenis_Kemasan = '{Txt_Kemasan_SelectedID.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Tidak Bisa Dihapus Karena Sudah Dipakai pada Data Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'======================
			'=     HAPUS DATA     =
			'======================
			SQL = $"
				delete from N_EMI_Master_Jenis_Kemasan
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Id_Jenis_Kemasan = '{Txt_Kemasan_SelectedID.Text.Trim}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong_Jenis_Kemasan()
	End Sub

	Private Sub Cmb_Kemasan_Kolom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kemasan_Kolom.SelectedIndexChanged
		If Cmb_Kemasan_Kolom.SelectedIndex = -1 Then
			Txt_Kemasan_Value.Enabled = False
		Else
			Txt_Kemasan_Value.Enabled = True
		End If

		Txt_Kemasan_Value.Text = ""
	End Sub

	Private Sub Btn_Kemasan_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Kemasan_Cari.Click
		LoadData_JenisKemasan()
	End Sub

	Private Sub Lv_Jenis_Kemasan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Jenis_Kemasan.DoubleClick
		If Lv_Jenis_Kemasan.Items.Count = 0 Then Exit Sub

		GetData_Kemasan(Lv_Jenis_Kemasan.FocusedItem.Index)

		Txt_Kemasan_Kode.Text = Lv_Kemasan_KodeJenisKemasan.Trim
		Txt_Kemasan_Keterangan.Text = Lv_Kemasan_Keterangan.Trim
		Txt_Kemasan_Prefix.Text = Lv_Kemasan_Prefix.Trim
		Txt_Kemasan_SelectedID.Text = Lv_Kemasan_ID.Trim

		Txt_Kemasan_Kode.Enabled = False

		Btn_Kemasan_Simpan.Tag = "UPDATE"
		Btn_Kemasan_Simpan.Text = "&Update"
	End Sub

#End Region

	'=========================================================================================================================
	'=     MENU JENIS KUALITAS
	'=========================================================================================================================

#Region "Kualitas"

	Private Sub Kosong_Jenis_Kualitas()

		Txt_Kualitas_Kode.Text = ""
		Txt_Kualitas_Keterangan.Text = ""
		Txt_Kualitas_Prefix.Text = ""
		Txt_Kualitas_SelectedID.Text = ""

		Btn_Kualitas_Simpan.Tag = "SIMPAN"
		Btn_Kualitas_Simpan.Text = "&Simpan"

		Txt_Kualitas_Kode.Enabled = True

		Cmb_Kualitas_Kolom.SelectedIndex = -1
		Txt_Kualitas_Value.Text = ""

		Try
			OpenConn()

			GetCurrentPrefix_Kualitas()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		LoadData_JenisKualitas()
	End Sub

	Private Sub GetCurrentPrefix_Kualitas()
		Txt_Makanan_Prefix.Text = ""
		SQL = $"
				select isnull(max(Prefix_Jenis_Kualitas), 0) as Prefix
				from N_EMI_Master_Jenis_Kualitas
				where Kode_Perusahaan = '{KodePerusahaan}'
			"
		Using Dr = OpenTrans(SQL)
			If Dr.Read Then
				Txt_Kualitas_Prefix.Text = Val(HilangkanTanda(Dr("Prefix"))) + 1
			End If
		End Using
	End Sub

	Private Sub GetData_Kualitas(ByVal index As Integer)
		Lv_Kualitas_ID = Lv_Jenis_Kualitas.Items(index).SubItems(Item_Kualitas_ID).Text
		Lv_Kualitas_Prefix = Lv_Jenis_Kualitas.Items(index).SubItems(Item_Kualitas_Prefix).Text
		Lv_Kualitas_KodeJenisKualitas = Lv_Jenis_Kualitas.Items(index).SubItems(Item_Kualitas_KodeJenisKualitas).Text
		Lv_Kualitas_Keterangan = Lv_Jenis_Kualitas.Items(index).SubItems(Item_Kualitas_Keterangan).Text
	End Sub

	Private Sub LoadData_JenisKualitas()
		Try
			OpenConn()

			Dim Filter As String = ""
			If Cmb_Kualitas_Kolom.SelectedIndex <> -1 Then
				Filter &= $"and {Arr_Filter_Jenis_Kualitas(Cmb_Kualitas_Kolom.SelectedIndex)} like '%{Txt_Kualitas_Value.Text.Trim}%' "
			End If

			Lv_Jenis_Kualitas.Items.Clear()
			SQL = $"
				select Id_Jenis_Kualitas, Kode_Jenis_Kualitas, Prefix_Jenis_Kualitas, Keterangan
				from N_EMI_Master_Jenis_Kualitas
				where Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				order by Id_Jenis_Kualitas
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Jenis_Kualitas.Items.Add(Dr("Id_Jenis_Kualitas"))
					Lv.SubItems.Add(Dr("Prefix_Jenis_Kualitas"))
					Lv.SubItems.Add(Dr("Kode_Jenis_Kualitas"))
					Lv.SubItems.Add(Dr("Keterangan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Kualitas_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Kualitas_Simpan.Click
		If Txt_Kualitas_Kode.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Jenis Kualitas Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kualitas_Kode.Focus()
			Exit Sub
		ElseIf Txt_Kualitas_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Jenis Kualitas Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kualitas_Keterangan.Focus()
			Exit Sub
		ElseIf Txt_Kualitas_Prefix.Text.Trim.Length = 0 Then
			MessageBox.Show("Prefix Jenis Kualitas Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kualitas_Prefix.Focus()
			Exit Sub
		End If

		If (MessageBox.Show("Yakin Ingin Melakuakn Simpan Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Action As String = ""

			If Btn_Kualitas_Simpan.Tag = "SIMPAN" Then

				GetCurrentPrefix_Kualitas()

				'===========================================
				'=     CEK APAKAH ADA PREFIX YANG SAMA     =
				'===========================================
				SQL = $"
					select 1
					from N_EMI_Master_Jenis_Kualitas
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Prefix_Jenis_Kualitas = '{Txt_Kualitas_Prefix.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Data jenis Kualitas Dengan Prefix {Txt_Kualitas_Prefix.Text.Trim} Sudah Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     INSERT DATA     =
				'=======================
				SQL = $"
					insert into N_EMI_Master_Jenis_Kualitas (Kode_Perusahaan, Kode_Jenis_Kualitas, Prefix_Jenis_Kualitas, Keterangan)
					values ('{KodePerusahaan}', '{Txt_Kualitas_Kode.Text.Trim}', '{Txt_Kualitas_Prefix.Text.Trim}', '{Txt_Kualitas_Keterangan.Text.Trim}')
				"
				ExecuteTrans(SQL)

				Action = "simpan"

			ElseIf Btn_Kualitas_Simpan.Tag = "UPDATE" Then

				If Txt_Kualitas_SelectedID.Text.Trim.Length = 0 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Harap Pilih Data Dahulu yang Ingin Diupdate", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				'===============================
				'=     CEK APAKAH ADA DATA     =
				'===============================
				SQL = $"
					select 1
					from N_EMI_Master_Jenis_Kualitas
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Jenis_Kualitas = '{Txt_Kualitas_SelectedID.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     UDPATE DATA     =
				'=======================
				SQL = $"
					update N_EMI_Master_Jenis_Kualitas set keterangan = '{Txt_Kualitas_Keterangan.Text.Trim}'
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Jenis_Kualitas = '{Txt_Kualitas_SelectedID.Text.Trim}'
				"
				ExecuteTrans(SQL)

				Action = "update"
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

		Kosong_Jenis_Kualitas()

	End Sub

	Private Sub Btn_Kualitas_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Kualitas_Refresh.Click
		Kosong_Jenis_Kualitas()
	End Sub

	Private Sub Btn_Kualitas_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Kualitas_Hapus.Click
		If Txt_Kualitas_SelectedID.Text.Trim.Length = 0 Then
			MessageBox.Show("Pilih Dahulu Data yang Ingin Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If (MessageBox.Show("Yakin Ingin Menghapus Data Ini???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===============================
			'=     CEK APAKAH DATA ADA     =
			'===============================
			SQL = $"
					select 1
					from N_EMI_Master_Jenis_Kualitas
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Jenis_Kualitas = '{Txt_Kualitas_SelectedID.Text.Trim}'
				"
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'===================================================
			'=     CEK APAKAH DATA SUDAH DI PAKAI DIBARANG     =
			'===================================================
			SQL = $"
				select top 1 1
				from Barang
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Id_Jenis_Kualitas = '{Txt_Kualitas_SelectedID.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Tidak Bisa Dihapus Karena Sudah Dipakai pada Data Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'======================
			'=     HAPUS DATA     =
			'======================
			SQL = $"
				delete from N_EMI_Master_Jenis_Kualitas
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Id_Jenis_Kualitas = '{Txt_Kualitas_SelectedID.Text.Trim}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong_Jenis_Kualitas()
	End Sub

	Private Sub Cmb_Kualitas_Kolom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kualitas_Kolom.SelectedIndexChanged
		If Cmb_Kualitas_Kolom.SelectedIndex = -1 Then
			Txt_Kualitas_Value.Enabled = False
		Else
			Txt_Kualitas_Value.Enabled = True
		End If

		Txt_Kualitas_Value.Text = ""
	End Sub

	Private Sub Btn_Kualitas_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Kualitas_Cari.Click
		LoadData_JenisKualitas()
	End Sub

	Private Sub Lv_Jenis_Kualitas_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Jenis_Kualitas.DoubleClick
		If Lv_Jenis_Kualitas.Items.Count = 0 Then Exit Sub

		GetData_Kualitas(Lv_Jenis_Kualitas.FocusedItem.Index)

		Txt_Kualitas_Kode.Text = Lv_Kualitas_KodeJenisKualitas.Trim
		Txt_Kualitas_Keterangan.Text = Lv_Kualitas_Keterangan.Trim
		Txt_Kualitas_Prefix.Text = Lv_Kualitas_Prefix.Trim
		Txt_Kualitas_SelectedID.Text = Lv_Kualitas_ID.Trim

		Txt_Kualitas_Kode.Enabled = False

		Btn_Kualitas_Simpan.Tag = "UPDATE"
		Btn_Kualitas_Simpan.Text = "&Update"
	End Sub

#End Region

	'=========================================================================================================================
	'=     HELPER
	'=========================================================================================================================

#Region "HELPER"

	Private Sub LoadTabMenu()

		For Each tabName In menuItems

			Dim container As New FlowLayoutPanel With {
				.Tag = tabName,
				.FlowDirection = FlowDirection.TopDown,
				.AutoSize = True,
				.Margin = New Padding(2, 5, 2, 0),
				.Cursor = Cursors.Hand
			}

			Dim lbl As New Label With {
				.Text = tabName,
				.AutoSize = True,
				.Font = New Font("Work Sans SemiBold", 10, FontStyle.Bold),
				.ForeColor = Color.DimGray,
				.Padding = New Padding(0, 0, 0, 2)
			}

			Dim line As New Panel With {
				.Tag = lbl,
				.Height = 3,
				.Dock = DockStyle.Bottom,
				.BackColor = Color.Transparent
			}

			AddHandler lbl.Click, Sub(s, ev) SetActiveTab(line, lbl, tabName)
			AddHandler line.Click, Sub(s, ev) SetActiveTab(line, lbl, tabName)
			AddHandler container.Click, Sub(s, ev) SetActiveTab(line, lbl, tabName)

			container.Controls.Add(lbl)
			container.Controls.Add(line)
			FLPanel_Tab_Menu.Controls.Add(container)

			'Set Default Tab
			If tabName = "Jenis Makanan" Then SetActiveTab(line, lbl, tabName)
		Next

	End Sub

	Private Sub SetActiveTab(line As Panel, lbl As Label, tag As String)

		If activeLine IsNot Nothing Then
			activeLine.BackColor = Color.Transparent
			DirectCast(activeLine.Tag, Label).ForeColor = Color.DimGray
		End If

		line.BackColor = Color.FromArgb(40, 80, 145)
		lbl.ForeColor = Color.FromArgb(40, 80, 145)

		activeLine = line
		HandleClickMenu(tag)

	End Sub

#End Region

	'=========================================================================================================================
	'=     UTILITY
	'=========================================================================================================================

#Region "UTILITY"

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub Lv_Jenis_Makanan_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Jenis_Makanan.MouseMove
		HandleListViewHover(Lv_Jenis_Makanan, e)
	End Sub

	Private Sub Lv_Jenis_Kemasan_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Jenis_Kemasan.MouseMove
		HandleListViewHover(Lv_Jenis_Kemasan, e)
	End Sub

	Private Sub Lv_Jenis_Kualitas_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Jenis_Kualitas.MouseMove
		HandleListViewHover(Lv_Jenis_Kualitas, e)
	End Sub

	Private Sub EnableDoubleBuffer(lvw As ListView)
		Dim t As Type = lvw.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(lvw, True, Nothing)
	End Sub

	Private Sub HandleListViewHover(lvw As ListView, e As MouseEventArgs)

		Dim hit As ListViewHitTestInfo = lvw.HitTest(e.Location)
		Dim lastItem As ListViewItem = TryCast(lvw.Tag, ListViewItem)

		If hit.Item IsNot Nothing Then
			lvw.Cursor = Cursors.Hand
		Else
			lvw.Cursor = Cursors.Default
		End If

		If hit.Item Is Nothing Then

			If lastItem IsNot Nothing Then
				If lastItem.BackColor = Color.FromArgb(235, 235, 235) Then
					lastItem.BackColor = Color.White
				End If

				lvw.Tag = Nothing
			End If

			Exit Sub
		End If

		Dim currentItem = hit.Item

		If currentItem.Tag IsNot Nothing Then
			Exit Sub
		End If

		If lastItem IsNot currentItem Then

			lvw.BeginUpdate()

			If lastItem IsNot Nothing Then
				If lastItem.BackColor = Color.FromArgb(235, 235, 235) Then
					lastItem.BackColor = Color.White
				End If
			End If

			currentItem.BackColor = Color.FromArgb(235, 235, 235)

			lvw.Tag = currentItem

			lvw.EndUpdate()
		End If
	End Sub

#End Region

End Class