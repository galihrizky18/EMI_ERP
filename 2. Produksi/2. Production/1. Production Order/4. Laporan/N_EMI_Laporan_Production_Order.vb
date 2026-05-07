Public Class N_EMI_Laporan_Production_Order

	Dim arrTanggal, arrLokasi, arrStatusPO, arrStatusSplit, arrLain As New ArrayList

	Dim SwitchAutoComplete As Boolean = False

	Private Sub N_EMI_Laporan_Production_Order_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Cmb_Tanggal.Items.Clear() : arrTanggal.Clear()
		Cmb_Tanggal.Items.Add("Tanggal Transaksi") : arrTanggal.Add("Tanggal")
		Cmb_Tanggal.Items.Add("Tanggal Release") : arrTanggal.Add("Tanggal_Release")

		Cmb_Lokasi.Items.Clear() : arrLokasi.Clear()
		Cmb_Lokasi.Items.Add(OpsiSeluruh) : arrLokasi.Add(OpsiSeluruh)
		Cmb_Lokasi.Items.Add("HEAD OFFICE") : arrLokasi.Add("HEAD OFFICE")

		Cmb_Status_PO.Items.Clear() : arrStatusPO.Clear()
		Cmb_Status_PO.Items.Add(OpsiSeluruh) : arrStatusPO.Add(OpsiSeluruh)
		Cmb_Status_PO.Items.Add("ON PROCESS") : arrStatusPO.Add("ON PROCESS")
		Cmb_Status_PO.Items.Add("RELEASED") : arrStatusPO.Add("RELEASED")

		Cmb_Status_Split.Items.Clear() : arrStatusSplit.Clear()
		Cmb_Status_Split.Items.Add(OpsiSeluruh) : arrStatusSplit.Add(OpsiSeluruh)
		Cmb_Status_Split.Items.Add("ON PROCESS") : arrStatusSplit.Add("ON PROCESS")
		Cmb_Status_Split.Items.Add("FINISHED") : arrStatusSplit.Add("FINISHED")

		Cmb_Lain.Items.Clear() : arrLain.Clear()
		Cmb_Lain.Items.Add(OpsiSeluruh) : arrLain.Add(OpsiSeluruh)
		Cmb_Lain.Items.Add("Routing") : arrLain.Add("Routing")
		Cmb_Lain.Items.Add("Jenis Produk") : arrLain.Add("Jenis_Produk")
		Cmb_Lain.Items.Add("Keterangan") : arrLain.Add("Keterangan")
		Cmb_Lain.Items.Add("User Input") : arrLain.Add("UserId")
		Cmb_Lain.Items.Add("User Release") : arrLain.Add("UserId_Release")

		Lv_NoFaktur.Location = New Point(133, 235)
		Lv_Formula.Location = New Point(133, 262)
		Lv_Barang.Location = New Point(133, 367)

		Lv_NoFaktur.Columns.Clear()
		Lv_NoFaktur.Columns.Add("No Faktur", 120, HorizontalAlignment.Left)
		Lv_NoFaktur.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
		Lv_NoFaktur.Columns.Add("Jam", 90, HorizontalAlignment.Center)
		Lv_NoFaktur.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
		Lv_NoFaktur.View = View.Details

		Lv_Formula.Columns.Clear()
		Lv_Formula.Columns.Add("No Transaksi", 120, HorizontalAlignment.Left)
		Lv_Formula.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
		Lv_Formula.Columns.Add("Barang", 250, HorizontalAlignment.Left)
		Lv_Formula.View = View.Details

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 350, HorizontalAlignment.Left)
		Lv_Barang.View = View.Details

		Kosong()

	End Sub

	Private Sub Kosong()

		Me.Size = New Point(598, 505)

		Cmb_Tanggal.SelectedIndex = 0
		Cmb_Lokasi.SelectedIndex = 0
		Cmb_Status_PO.SelectedIndex = 0
		Cmb_Status_Split.SelectedIndex = 0
		Cmb_Lain.SelectedIndex = 0

		SwitchAutoComplete = True
		Txt_No_Faktur.Text = OpsiSeluruh
		Txt_No_Formula.Text = OpsiSeluruh
		Txt_KdBarang.Text = OpsiSeluruh
		Txt_NmBarang.Text = OpsiSeluruh
		Txt_Lain.Text = ""
		SwitchAutoComplete = False

		Lv_NoFaktur.Visible = False
		Lv_Formula.Visible = False
		Lv_Barang.Visible = False

	End Sub

	'============================================================================================================================================
	'=     HANDLE AUTOCOMPLETE
	'============================================================================================================================================
	Private Sub Txt_No_Faktur_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Faktur.TextChanged
		If SwitchAutoComplete Then Exit Sub

		If Txt_No_Faktur.Text.Trim.Length = 0 Then
			Lv_NoFaktur.Visible = False
			Txt_No_Faktur.Text = ""
			Exit Sub
		Else
			Lv_NoFaktur.Visible = True
		End If

		Try
			OpenConn()

			Lv_NoFaktur.Items.Clear()

			Lv_NoFaktur.BeginUpdate()
			Dim Lv As ListViewItem
			Lv = Lv_NoFaktur.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			SQL = $"
				select No_Faktur, Tanggal, Jam, Keterangan
				from EMI_Order_Produksi
				where Status is null
				and Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur like '%{Txt_No_Faktur.Text.Trim}%'
				order by tanggal, jam
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_NoFaktur.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Jam"))
					Lv.SubItems.Add(Dr("Keterangan"))
				Loop
			End Using

			Lv_NoFaktur.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_No_Faktur_Leave(sender As Object, e As EventArgs) Handles Txt_No_Faktur.Leave
		If Txt_No_Faktur.Text.Trim.Length = 0 Then Exit Sub
		If Lv_NoFaktur.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_No_Faktur.Text = OpsiSeluruh Then

				SQL = $"
					select No_Faktur, Tanggal, Jam, Keterangan
					from EMI_Order_Produksi
					where Status is null
					and Kode_Perusahaan = '{KodePerusahaan}'
					and No_Faktur = '{Txt_No_Faktur.Text.Trim}'
				"
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_No_Faktur.Text = Dr("No_Faktur")
						Txt_No_Formula.Focus()
					Else
						MessageBox.Show("No Faktur Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_No_Faktur.Text = ""
						Txt_No_Faktur.Focus()
					End If

					Lv_NoFaktur.Visible = False
				End Using
			Else
				Txt_No_Formula.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_No_Faktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Faktur.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_No_Faktur.Text.Trim.Length = 0 Then Txt_No_Faktur.Focus()
			Txt_No_Faktur_Leave(Txt_No_Faktur, e)

			Lv_NoFaktur.Visible = False

		End If
	End Sub

	Private Sub Txt_No_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Faktur.KeyDown
		If e.KeyCode = Keys.Down Then Lv_NoFaktur.Focus()
	End Sub

	Private Sub Lv_NoFaktur_DoubleClick(sender As Object, e As EventArgs) Handles Lv_NoFaktur.DoubleClick
		If Lv_NoFaktur.Items.Count = 0 Or Lv_NoFaktur.FocusedItem.Index = -1 Then Exit Sub

		Dim No_Faktur As String = Lv_NoFaktur.FocusedItem.SubItems(0).Text

		SwitchAutoComplete = True
		Txt_No_Faktur.Text = No_Faktur
		SwitchAutoComplete = False

		Lv_NoFaktur.Visible = False

		Txt_No_Formula.Focus()
	End Sub

	Private Sub Lv_NoFaktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_NoFaktur.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_NoFaktur_DoubleClick(Lv_NoFaktur, e)
		End If
	End Sub

	'=====================================================================================================================================================
	'=====================================================================================================================================================

	Private Sub Txt_No_Formula_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Formula.TextChanged
		If SwitchAutoComplete Then Exit Sub

		If Txt_No_Formula.Text.Trim.Length = 0 Then
			Lv_Formula.Visible = False
			Txt_No_Formula.Text = ""
			Exit Sub
		Else
			Lv_Formula.Visible = True
		End If

		Try
			OpenConn()

			Lv_Formula.Items.Clear()

			Lv_Formula.BeginUpdate()
			Dim Lv As ListViewItem
			Lv = Lv_Formula.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			SQL = $"
				select a.Kode_Perusahaan, a.No_Faktur, a.Kode_Barang, b.Nama
				from Emi_Transaksi_Formulator a
					inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_inq
				where a.Status is null
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur like '%{Txt_No_Formula.Text.Trim}%'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Formula.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			Lv_Formula.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_No_Formula_Leave(sender As Object, e As EventArgs) Handles Txt_No_Formula.Leave
		If Txt_No_Formula.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Formula.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_No_Formula.Text = OpsiSeluruh Then

				SQL = $"
					select a.Kode_Perusahaan, a.No_Faktur, a.Kode_Barang, b.Nama
					from Emi_Transaksi_Formulator a
						inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_inq
					where a.Status is null
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.No_Faktur = '{Txt_No_Formula.Text.Trim}'
				"
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_No_Formula.Text = Dr("No_Faktur")
						Cmb_Status_PO.DroppedDown = True
						Cmb_Status_PO.Focus()
					Else
						MessageBox.Show("No Transaksi Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_No_Formula.Text = ""
						Txt_No_Formula.Focus()
					End If

					Lv_Formula.Visible = False
				End Using
			Else
				Cmb_Status_PO.DroppedDown = True
				Cmb_Status_PO.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_No_Formula_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Formula.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_No_Formula.Text.Trim.Length = 0 Then Txt_No_Formula.Focus()
			Txt_No_Formula_Leave(Txt_No_Formula, e)

			Lv_NoFaktur.Visible = False

		End If
	End Sub

	Private Sub Txt_No_Formula_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Formula.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Formula.Focus()
	End Sub

	Private Sub Lv_Formula_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Formula.DoubleClick
		If Lv_Formula.Items.Count = 0 Or Lv_Formula.FocusedItem.Index = -1 Then Exit Sub

		Dim No_Faktur As String = Lv_Formula.FocusedItem.SubItems(0).Text

		SwitchAutoComplete = True
		Txt_No_Formula.Text = No_Faktur
		SwitchAutoComplete = False

		Lv_Formula.Visible = False

		Txt_No_Formula.Focus()
	End Sub

	Private Sub Lv_Formula_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Formula.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Formula_DoubleClick(Lv_Formula, e)
		End If
	End Sub

	'=====================================================================================================================================================
	'=====================================================================================================================================================
	Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
		If SwitchAutoComplete Then Exit Sub

		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Me.Size = New Point(598, 505)
			Exit Sub
		Else
			Lv_Barang.Visible = True
			Me.Size = New Point(598, 585)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Lv_Barang.BeginUpdate()
			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = $"
				select Distinct a.Kode_Barang, a.Nama
				from barang a, EMI_Group_Jenis b
				where a.Kode_Perusahaan = b.Kode_Perusahaan
				and a.Id_Group_Jenis = b.Id_Group_Jenis
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and b.Kode_Group_Jenis = 'FINISHED GOODS'
				and a.Kode_Barang like '%{Txt_KdBarang.Text.Trim}%'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using
			Lv_Barang.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
		If SwitchAutoComplete Then Exit Sub

		If Txt_NmBarang.Text.Trim.Length = 0 Then
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Me.Size = New Point(598, 505)
			Exit Sub
		Else
			Lv_Barang.Visible = True
			Me.Size = New Point(598, 585)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = $"
				select Distinct a.Kode_Barang, a.Nama
				from barang a, EMI_Group_Jenis b
				where a.Kode_Perusahaan = b.Kode_Perusahaan
				and a.Id_Group_Jenis = b.Id_Group_Jenis
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and b.Kode_Group_Jenis = 'FINISHED GOODS'
				and a.Nama like '%{Txt_NmBarang.Text.Trim}%'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
		If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Barang.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_KdBarang.Text = OpsiSeluruh Then

				SQL = $"
					select Distinct a.Kode_Barang, a.Nama
					from barang a, EMI_Group_Jenis b
					where a.Kode_Perusahaan = b.Kode_Perusahaan
					and a.Id_Group_Jenis = b.Id_Group_Jenis
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and b.Kode_Group_Jenis = 'FINISHED GOODS'
					and a.Kode_Barang = '{Txt_KdBarang.Text.Trim}'
				"
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_KdBarang.Text = Dr("Kode_Barang")
						Txt_NmBarang.Text = Dr("Nama")
						Cmb_Lain.DroppedDown = True
						Cmb_Lain.Focus()
					Else
						MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_KdBarang.Text = ""
						Txt_NmBarang.Text = ""
						Txt_KdBarang.Focus()
					End If

					Lv_Barang.Visible = False
					Me.Size = New Point(598, 505)
				End Using
			Else
				Cmb_Lain.DroppedDown = True
				Cmb_Lain.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
			Txt_KdBarang_Leave(Txt_KdBarang, e)

			Lv_Barang.Visible = False
			Me.Size = New Point(598, 505)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_KdBarang_Leave(Txt_NmBarang, e)

			Lv_Barang.Visible = False
			Me.Size = New Point(598, 505)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

		Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim NmKdBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

		SwitchAutoComplete = True
		Txt_KdBarang.Text = KdBarang
		Txt_NmBarang.Text = NmKdBarang
		SwitchAutoComplete = False

		Lv_Barang.Visible = False
		Me.Size = New Point(598, 505)

		BtnCetak.Focus()
	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Barang_DoubleClick(Lv_Barang, e)
		End If
	End Sub

	'============================================================================================================================================
	'=     HANDLE BUTTON
	'============================================================================================================================================
	Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
		Me.Close()
	End Sub

	Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
		If Tgl1.Value.Date > Tgl2.Value.Date Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
			MessageBox.Show("Lokasi Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Lokasi.Focus() : Exit Sub
		ElseIf Txt_No_Faktur.Text.Trim.Length = 0 Then
			MessageBox.Show("No Faktur Tidak Boleh Kosong!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_No_Faktur.Focus() : Exit Sub
		ElseIf Txt_No_Formula.Text.Trim.Length = 0 Then
			MessageBox.Show("No Formula Tidak Boleh Kosong!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_No_Formula.Focus() : Exit Sub
		ElseIf Cmb_Status_PO.SelectedIndex = -1 Then
			MessageBox.Show("Status PO Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Status_PO.Focus() : Exit Sub
		ElseIf Cmb_Status_Split.SelectedIndex = -1 Then
			MessageBox.Show("Status Split Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Status_Split.Focus() : Exit Sub
		ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Barang Tidak Boleh Kosong!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_KdBarang.Focus() : Exit Sub
		ElseIf Txt_NmBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Barang Tidak Boleh Kosong!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_NmBarang.Focus() : Exit Sub
		End If

		If Cmb_Lain.SelectedIndex <> 0 Then
			If Txt_Lain.Text.Trim.Length = 0 Then
				MessageBox.Show("Value Filter harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_Lain.Focus() : Exit Sub
			End If
		End If

		If Cmb_Tanggal.SelectedIndex = -1 Then
			MessageBox.Show("Harap pilih tanggal Dahulu!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Tanggal.Focus() : Exit Sub
		End If

		Try
			OpenConn()

			Dim kertas As String = "A4"
			Dim Filter As String = ""
			Dim SF As String = ""

			SF = "{N_EMI_View_Laporan_Production_Order.Kode_Perusahaan} = '" & KodePerusahaan & "' "
			SF &= "and {N_EMI_View_Laporan_Production_Order." & arrTanggal(Cmb_Tanggal.SelectedIndex) & "} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
			SF &= "{N_EMI_View_Laporan_Production_Order." & arrTanggal(Cmb_Tanggal.SelectedIndex) & "} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

			If Cmb_Lokasi.SelectedIndex <> 0 Then
				Filter &= "and lokasi like '%" & arrLokasi(Cmb_Lokasi.SelectedIndex) & "%' "
				SF &= "And {N_EMI_View_Laporan_Production_Order.lokasi} LIKE '*" & arrLokasi(Cmb_Lokasi.SelectedIndex) & "*' "
			End If

			If Not Txt_No_Faktur.Text.ToUpper = OpsiSeluruh.ToUpper Then
				Filter &= "and No_Faktur like '%" & Txt_No_Faktur.Text.Trim & "%' "
				SF &= "And {N_EMI_View_Laporan_Production_Order.No_Faktur} LIKE '*" & Txt_No_Faktur.Text.Trim & "*' "
			End If

			If Not Txt_No_Formula.Text.ToUpper = OpsiSeluruh.ToUpper Then
				Filter &= "and Kode_Formula like '%" & Txt_No_Formula.Text.Trim & "%' "
				SF &= "And {N_EMI_View_Laporan_Production_Order.Kode_Formula} LIKE '*" & Txt_No_Formula.Text.Trim & "*' "
			End If

			If Cmb_Status_PO.SelectedIndex <> 0 Then
				Filter &= "and Status_PO like '%" & arrStatusPO(Cmb_Status_PO.SelectedIndex) & "%' "
				SF &= "And {N_EMI_View_Laporan_Production_Order.Status_PO} LIKE '*" & arrStatusPO(Cmb_Status_PO.SelectedIndex) & "*' "
			End If

			If Cmb_Status_Split.SelectedIndex <> 0 Then
				Filter &= "and Status_Selesai_Split like '%" & arrStatusSplit(Cmb_Status_Split.SelectedIndex) & "%' "
				SF &= "And {N_EMI_View_Laporan_Production_Order.Status_Selesai_Split} LIKE '*" & arrStatusSplit(Cmb_Status_Split.SelectedIndex) & "*' "
			End If

			If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
				Filter &= "and Kode_Produk = '" & Txt_KdBarang.Text.Trim & "' "
				SF &= "And {N_EMI_View_Laporan_Production_Order.Kode_Produk} = '" & Txt_KdBarang.Text.Trim & "'"
			End If

			If Cmb_Lain.SelectedIndex <> 0 Then
				If Not Txt_Lain.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
					Filter &= "and " & arrLain(Cmb_Lain.SelectedIndex) & " = '" & Txt_Lain.Text.Trim & "' "
					SF &= "And {N_EMI_View_Laporan_Production_Order." & arrLain(Cmb_Lain.SelectedIndex) & "} = '" & Txt_Lain.Text.Trim & "'"
				End If
			End If

			SQL = $"
				select Kode_Perusahaan
				from N_EMI_View_Laporan_Production_Order
				where Kode_Perusahaan = '{KodePerusahaan}'
				and {arrTanggal(Cmb_Tanggal.SelectedIndex)} BETWEEN '{Format(Tgl1.Value, "yyyy-MM-dd")}' and '{Format(Tgl2.Value, "yyyy-MM-dd")}'
				{Filter}
			"
			Using DS = BindingTrans(SQL)
				With DS.Tables("MyTable")
					If .Rows.Count <> 0 Then

						Dim CrDoc As New N_EMI_CR_Laporan_Production_Order

						CrDoc.SetDataSource(DS)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
																			Format(Tgl2.Value, "dd/MMM/yyyy")
						CrDoc.RecordSelectionFormula = SF

						With A_Place_For_Printing2
							.Text = "Laporan Production Order"
							.CrystalReportViewer1.ReportSource = CrDoc
							.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
							.Refresh()
							.Show()
						End With
					Else

						CloseConn()
						MessageBox.Show("Data Production Order Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub

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

	'============================================================================================================================================
	'=     HANDLE KEYPRESS
	'============================================================================================================================================
	Private Sub Cmb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Tanggal.SelectedIndex <> -1 Then
				Tgl1.Focus()
			End If
		End If
	End Sub

	Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
		If e.KeyChar = Chr(13) Then Tgl2.Focus()
	End Sub

	Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Lokasi.DroppedDown = True
			Cmb_Lokasi.Focus()
		End If
	End Sub

	Private Sub Cmb_Lokasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Lokasi.SelectedIndex <> -1 Then
				Txt_No_Faktur.Focus()
			End If
		End If
	End Sub

	Private Sub Cmb_Status_PO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Status_PO.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Status_PO.SelectedIndex <> -1 Then
				Cmb_Status_Split.DroppedDown = True
				Cmb_Status_Split.Focus()
			End If
		End If
	End Sub

	Private Sub Cmb_Status_Split_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Status_Split.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Status_Split.SelectedIndex <> -1 Then
				Txt_KdBarang.Focus()
			End If
		End If
	End Sub

	Private Sub Cmb_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lain.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cmb_Lain.SelectedIndex = 0 Then
				BtnCetak.Focus()
				Txt_Lain.Enabled = False
				Txt_Lain.BackColor = Color.FromArgb(235, 235, 235)
			Else
				Txt_Lain.Enabled = True
				Txt_Lain.BackColor = Color.White
			End If
		End If

		Txt_Lain.Text = ""
	End Sub

	Private Sub Cmb_Lain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lain.SelectedIndexChanged
		If Cmb_Lain.SelectedIndex = 0 Then
			BtnCetak.Focus()
			Txt_Lain.Enabled = False
			Txt_Lain.BackColor = Color.FromArgb(235, 235, 235)
		Else
			Txt_Lain.Enabled = True
			Txt_Lain.BackColor = Color.White
		End If
		Txt_Lain.Text = ""
	End Sub

	'============================================================================================================================================
	'=     UTILITY
	'============================================================================================================================================
	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

End Class