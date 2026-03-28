Public Class N_EMI_Transaksi_Validasi_Binding_Formula_Trial

	Private ReadOnly BodyAlignments_Parent As New Dictionary(Of Integer, StringAlignment)
	Private ReadOnly BodyAlignments_Order As New Dictionary(Of Integer, StringAlignment)
	Private ReadOnly BodyAlignments_Bahan As New Dictionary(Of Integer, StringAlignment)

	Dim arrFilter As New ArrayList

	Dim Lv_Parent_NoFaktur, Lv_Parent_Tanggal, Lv_Parent_Jam, Lv_Parent_KodeBarang, Lv_Parent_NamaBarang As String

	Dim Item_Lv_Parent_NoFaktur As Integer = 0
	Dim Item_Lv_Parent_Tanggal As Integer = 1
	Dim Item_Lv_Parent_Jam As Integer = 2
	Dim Item_Lv_Parent_KodeBarang As Integer = 3
	Dim Item_Lv_Parent_NamaBarang As Integer = 4

	Dim Lv_Order_Order, Lv_Order_NoFormula, Lv_Order_Hasil, Lv_Order_Keterangan As String

	Dim Item_Lv_Order_Order As Integer = 1
	Dim Item_Lv_Order_NoFormula As Integer = 2
	Dim Item_Lv_Order_Hasil As Integer = 3
	Dim Item_Lv_Order_Keterangan As Integer = 4

	Private Sub N_EMI_Transaksi_Validasi_Binding_Formula_Trial_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Lv_Formula_Parent.Columns.Clear() : BodyAlignments_Parent.Clear()
		Lv_Formula_Parent.Columns.Add("No Faktur", 150) : BodyAlignments_Parent(0) = StringAlignment.Near
		Lv_Formula_Parent.Columns.Add("Tanggal", 130) : BodyAlignments_Parent(1) = StringAlignment.Center
		Lv_Formula_Parent.Columns.Add("Jam", 110) : BodyAlignments_Parent(2) = StringAlignment.Center
		Lv_Formula_Parent.Columns.Add("Kode Barang", 180) : BodyAlignments_Parent(3) = StringAlignment.Near
		Lv_Formula_Parent.Columns.Add("Nama Barang", 550) : BodyAlignments_Parent(4) = StringAlignment.Near
		Lv_Formula_Parent.View = View.Details

		Lv_Formula_Order.Columns.Clear() : BodyAlignments_Order.Clear()
		Lv_Formula_Order.Columns.Add("", 0) : BodyAlignments_Order(0) = StringAlignment.Near
		Lv_Formula_Order.Columns.Add("Order", 50) : BodyAlignments_Order(1) = StringAlignment.Center
		Lv_Formula_Order.Columns.Add("No Formula", 120) : BodyAlignments_Order(2) = StringAlignment.Near
		Lv_Formula_Order.Columns.Add("Hasil", 110) : BodyAlignments_Order(3) = StringAlignment.Far
		Lv_Formula_Order.Columns.Add("Keterangan", 170) : BodyAlignments_Order(4) = StringAlignment.Near
		Lv_Formula_Order.View = View.Details

		Lv_Detail_Bahan.Columns.Clear() : BodyAlignments_Bahan.Clear()
		Lv_Detail_Bahan.Columns.Add("Kode Bahan", 110) : BodyAlignments_Bahan(0) = StringAlignment.Near
		Lv_Detail_Bahan.Columns.Add("Bahan", 300) : BodyAlignments_Bahan(1) = StringAlignment.Near
		Lv_Detail_Bahan.Columns.Add("Persentase", 100) : BodyAlignments_Bahan(2) = StringAlignment.Center
		Lv_Detail_Bahan.Columns.Add("Jumlah", 140) : BodyAlignments_Bahan(3) = StringAlignment.Far
		Lv_Detail_Bahan.View = View.Details

		Cmb_Filter.Items.Clear() : arrFilter.Clear()
		Cmb_Filter.Items.Add("No Faktur") : arrFilter.Add("a.No_Faktur")
		Cmb_Filter.Items.Add("Tanggal") : arrFilter.Add("a.Tanggal")
		Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("a.Kode_Barang")
		Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("b.Nama")

		Kosong()

	End Sub

	Private Sub Kosong()

		Lv_Formula_Parent.Items.Clear()
		Lv_Formula_Order.Items.Clear()
		Lv_Detail_Bahan.Items.Clear()
		Cmb_Filter.SelectedIndex = -1

		Txt_Filter.Visible = True
		Btn_Cari.Location = New Point(637, 55)
		Btn_Refresh.Location = New Point(730, 55)

		Panel_FIlter_Tanggal.Visible = False
		Panel_FIlter_Tanggal.Location = New Point(221, 54)

		Txt_Filter.Text = ""
		Filter_Tgl_1.Value = Date.Today
		Filter_Tgl_2.Value = Date.Today

		LoadData()

	End Sub

	Private Sub LoadData()
		Try
			OpenConn()

			Dim SqlFilter As String = ""
			If Cmb_Filter.SelectedIndex <> -1 Then
				If Cmb_Filter.SelectedIndex = 1 Then

					If Filter_Tgl_1.Value.Date > Filter_Tgl_2.Value.Date Then
						MessageBox.Show("Tanggal awal tidak boleh lebih besar dari tanggal akhir!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
						Filter_Tgl_1.Value = Filter_Tgl_2.Value
						Exit Sub
					Else
						SqlFilter = $"and {arrFilter(Cmb_Filter.SelectedIndex)} between '{Format(Filter_Tgl_1.Value, "yyyy-MM-dd")}' and '{Format(Filter_Tgl_2.Value, "yyyy-MM-dd")}' "
					End If
				Else
					If Txt_Filter.Text.Trim.Length = 0 Then
						MessageBox.Show("Value FIlter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_Filter.Focus()
						Exit Sub
					Else
						SqlFilter = $"and {arrFilter(Cmb_Filter.SelectedIndex)} like '%{Txt_Filter.Text.Trim}%' "
					End If
				End If

			End If

			Lv_Formula_Parent.Items.Clear() : Lv_Formula_Order.Items.Clear() : Lv_Detail_Bahan.Items.Clear()
			SQL = $"
				select distinct a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Barang, b.Nama
				from N_EMI_Transaksi_Formulator_Binding a
					inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang_Inq
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Status is null
				and a.Flag_Validasi_Main is null
				{SqlFilter}
				order by a.Tanggal, a.Jam
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Formula_Parent.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Jam"))
					Lv.SubItems.Add(Dr("Kode_Barang"))
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

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		LoadData()
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
		If Cmb_Filter.SelectedIndex <> -1 Then
			Txt_Filter.Enabled = True
			Txt_Filter.BackColor = Color.White

			If Cmb_Filter.SelectedIndex = 1 Then
				Txt_Filter.Visible = False
				Btn_Cari.Location = New Point(713, 55)
				Btn_Refresh.Location = New Point(806, 55)
				Panel_FIlter_Tanggal.Visible = True
			Else
				Txt_Filter.Visible = True
				Btn_Cari.Location = New Point(637, 55)
				Btn_Refresh.Location = New Point(730, 55)
				Panel_FIlter_Tanggal.Visible = False
			End If
		Else
			Txt_Filter.Enabled = False
			Txt_Filter.BackColor = Color.FromArgb(235, 235, 235)

		End If

		Filter_Tgl_1.Value = Date.Today
		Filter_Tgl_2.Value = Date.Today
		Txt_Filter.Text = ""
	End Sub

	Private Sub Lv_Formula_Parent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Formula_Parent.SelectedIndexChanged
		If Lv_Formula_Parent.Items.Count = 0 Or Lv_Formula_Parent.FocusedItem Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim SelectedFaktur As String = Lv_Formula_Parent.FocusedItem.SubItems(Item_Lv_Parent_NoFaktur).Text

			Lv_Formula_Order.Items.Clear() : Lv_Detail_Bahan.Items.Clear()
			SQL = $"
				select b.No_Formulator, b.No_Prioritas, c.Hasil, c.Satuan_Hasil, b.Keterangan
				from N_EMI_Transaksi_Formulator_Binding a
					inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join Emi_Transaksi_Formulator c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Formulator = c.No_Faktur and c.Status is NULL
				where a.Status is null
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{SelectedFaktur}'
				order by b.No_Prioritas
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Formula_Order.Items.Add("")
					Lv.SubItems.Add(Dr("No_Prioritas"))
					Lv.SubItems.Add(Dr("No_Formulator"))
					Lv.SubItems.Add($"{Format(Dr("Hasil"), "N4")} {Dr("Satuan_Hasil")}")
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

	Private Sub Lv_Formula_Order_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Formula_Order.SelectedIndexChanged
		If Lv_Formula_Order.Items.Count = 0 Or Lv_Formula_Order.FocusedItem Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim SelectedFaktur As String = Lv_Formula_Parent.FocusedItem.SubItems(Item_Lv_Parent_NoFaktur).Text
			Dim SelectedFormula As String = Lv_Formula_Order.FocusedItem.SubItems(Item_Lv_Order_NoFormula).Text

			Lv_Detail_Bahan.Items.Clear()
			SQL = $"
				select a.No_Faktur, b.Kode_Barang, c.Nama, b.Persentase, b.Jumlah, b.satuan
				from Emi_Transaksi_Formulator a
					inner join EMI_Transaksi_Formulator_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{SelectedFormula}'
				and a.Status is NULL
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Detail_Bahan.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
					Lv.SubItems.Add($"{Format(Dr("Persentase"), "N2")} %")
					Lv.SubItems.Add($"{Format(Dr("Jumlah"), "N4")} {Dr("satuan")}")
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
		If Lv_Formula_Parent.Items.Count = 0 Or Lv_Formula_Parent.FocusedItem Is Nothing Then Exit Sub

		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===========================
			'=     CEK ROLE BUTTON     =
			'===========================
			If CekButtonRole("Validasi_Formula_Binding") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Validasi Formula Binding", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim SelectedFaktur As String = Lv_Formula_Parent.FocusedItem.SubItems(Item_Lv_Parent_NoFaktur).Text

			If MessageBox.Show($"Yakin Ingin Melakukan Validasi Faktur {SelectedFaktur}?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
				CloseTrans()
				CloseConn()
				Exit Sub
			End If

			'==============================
			'=     CEK DATA TRANSAKSI     =
			'==============================
			SQL = $"
				select a.Status, a.Flag_Validasi_Main, b.no_formulator
				from N_EMI_Transaksi_Formulator_Binding a
					inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.kode_perusahaan = b.Kode_Perusahaan and a.no_faktur = b.No_Faktur
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{SelectedFaktur}'
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							If General_Class.CekNULL(.Rows(i).Item("Flag_Validasi_Main")) = "Y" Then
								CloseTrans()
								CloseConn()
								MessageBox.Show($"Data Binding Formula {SelectedFaktur} Sudah Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							'============================
							'=     CEK DATA FORMULA     =
							'============================
							SQL = $"
								select Status
								from Emi_Transaksi_Formulator
								where Kode_Perusahaan = '{KodePerusahaan}'
								and No_Faktur = '{ .Rows(i).Item("no_formulator")}'
							"
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									If General_Class.CekNULL(Dr("Status")) = "Y" Then
										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show($"Data Nomor Formula { .Rows(i).Item("no_formulator")} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show($"Data Nomor Formula { .Rows(i).Item("no_formulator")} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Data Binding Formula {SelectedFaktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			'=======================
			'=     UPDATE FLAG     =
			'=======================
			SQL = $"
				update N_EMI_Transaksi_Formulator_Binding set Flag_Validasi_Main = 'Y',
					Tanggal_Validasi_Main = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi_Main = '{Format(tgl_skg, "HH:mm:ss")}', User_Validasi_Main = '{UserID}'
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur = '{SelectedFaktur}'
				and Flag_Validasi_Main is null
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil DiValidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()
	End Sub

	Private Sub TolakToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TolakToolStripMenuItem.Click
		If Lv_Formula_Parent.Items.Count = 0 Or Lv_Formula_Parent.FocusedItem Is Nothing Then Exit Sub

		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===========================
			'=     CEK ROLE BUTTON     =
			'===========================
			If CekButtonRole("Validasi_Formula_Binding") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Validasi Formula Binding", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim SelectedFaktur As String = Lv_Formula_Parent.FocusedItem.SubItems(Item_Lv_Parent_NoFaktur).Text

			If MessageBox.Show($"Yakin Ingin Melakukan Tolak Validasi Faktur {SelectedFaktur}?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
				CloseTrans()
				CloseConn()
				Exit Sub
			End If

			'==============================
			'=     CEK DATA TRANSAKSI     =
			'==============================
			SQL = $"
				select a.Status, a.Flag_Validasi_Main, b.no_formulator
				from N_EMI_Transaksi_Formulator_Binding a
					inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.kode_perusahaan = b.Kode_Perusahaan and a.no_faktur = b.No_Faktur
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{SelectedFaktur}'
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							If General_Class.CekNULL(.Rows(i).Item("Flag_Validasi_Main")) = "Y" Then
								CloseTrans()
								CloseConn()
								MessageBox.Show($"Data Binding Formula {SelectedFaktur} Sudah Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							'============================
							'=     CEK DATA FORMULA     =
							'============================
							SQL = $"
								select Status
								from Emi_Transaksi_Formulator
								where Kode_Perusahaan = '{KodePerusahaan}'
								and No_Faktur = '{ .Rows(i).Item("no_formulator")}'
							"
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									If General_Class.CekNULL(Dr("Status")) = "Y" Then
										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show($"Data Nomor Formula { .Rows(i).Item("no_formulator")} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show($"Data Nomor Formula { .Rows(i).Item("no_formulator")} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Data Binding Formula {SelectedFaktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			'=======================
			'=     UPDATE FLAG     =
			'=======================
			SQL = $"
				update N_EMI_Transaksi_Formulator_Binding set Flag_Validasi_Main = 'T',
					Tanggal_Validasi_Main = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi_Main = '{Format(tgl_skg, "HH:mm:ss")}', User_Validasi_Main = '{UserID}'
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur = '{SelectedFaktur}'
				and Flag_Validasi_Main is null
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Ditolak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()
	End Sub

	Private Sub CompareFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompareFormulaToolStripMenuItem.Click
		If Lv_Formula_Order.Items.Count = 0 Or Lv_Formula_Order.FocusedItem Is Nothing Then Exit Sub

		Dim NoFaktur As String = Lv_Formula_Parent.FocusedItem.SubItems(Item_Lv_Parent_NoFaktur).Text
		Dim KdBarang As String = Lv_Formula_Parent.FocusedItem.SubItems(Item_Lv_Parent_KodeBarang).Text
		Dim NamaBarang As String = Lv_Formula_Parent.FocusedItem.SubItems(Item_Lv_Parent_NamaBarang).Text

		N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.Txt_Faktur.Text = NoFaktur.Trim
		N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.Txt_Kd_Barang.Text = KdBarang.Trim
		N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.Txt_Nm_Barang.Text = NamaBarang.Trim
		N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.Kosong()
		N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.ShowDialog()

	End Sub

	'==========================================================================================================================================================================================================
	'=     UTILITY
	'==========================================================================================================================================================================================================
	Protected Overrides Sub WndProc(ByRef m As Message)
		If m.Msg = &HA3 Then
			Return
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub Lv_Formula_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Formula_Parent.MouseMove
		Dim info As ListViewHitTestInfo = Lv_Formula_Parent.HitTest(e.Location)

		If info.Item IsNot Nothing Then
			Lv_Formula_Parent.Cursor = Cursors.Hand
		Else
			Lv_Formula_Parent.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Lv_Formula_Parent_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Formula_Parent.MouseLeave
		Lv_Formula_Parent.Cursor = Cursors.Default
	End Sub

	Private Sub Lv_Formula_Order_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Formula_Order.MouseMove
		Dim info As ListViewHitTestInfo = Lv_Formula_Order.HitTest(e.Location)

		If info.Item IsNot Nothing Then
			Lv_Formula_Order.Cursor = Cursors.Hand
		Else
			Lv_Formula_Order.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Lv_Formula_Order_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Formula_Order.MouseLeave
		Lv_Formula_Order.Cursor = Cursors.Default
	End Sub

	Private Sub Lv_Formula_Parent_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Formula_Parent.DrawColumnHeader

		Using bgBrush As New Drawing2D.LinearGradientBrush(
			e.Bounds,
			Color.FromArgb(245, 245, 245),
			Color.FromArgb(220, 220, 220),
			Drawing2D.LinearGradientMode.Vertical)

			e.Graphics.FillRectangle(bgBrush, e.Bounds)
		End Using

		Using borderPen As New Pen(Color.FromArgb(180, 180, 180))
			e.Graphics.DrawLine(
				borderPen,
				e.Bounds.Left,
				e.Bounds.Bottom - 1,
				e.Bounds.Right,
				e.Bounds.Bottom - 1)
		End Using

		Using sf As New StringFormat()
			sf.Alignment = StringAlignment.Center
			sf.LineAlignment = StringAlignment.Center
			sf.Trimming = StringTrimming.EllipsisCharacter

			Dim textRect As Rectangle = Rectangle.Inflate(e.Bounds, -4, -2)

			e.Graphics.DrawString(
				e.Header.Text,
				Lv_Formula_Parent.Font,
				Brushes.Black,
				textRect,
				sf)
		End Using

	End Sub

	Private Sub Lv_Formula_Parent_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Formula_Parent.DrawSubItem
		Dim sf As New StringFormat()
		sf.LineAlignment = StringAlignment.Center

		If BodyAlignments_Parent.ContainsKey(e.ColumnIndex) Then
			sf.Alignment = BodyAlignments_Parent(e.ColumnIndex)
		Else
			sf.Alignment = StringAlignment.Near
		End If

		sf.Trimming = StringTrimming.EllipsisCharacter
		sf.FormatFlags = StringFormatFlags.NoWrap

		Dim status As String = CStr(e.Item.Tag)
		Dim bgBrush As Brush = Brushes.White
		Dim fgBrush As Brush = Brushes.Black

		If status = "Y" Then
			bgBrush = Brushes.DarkRed
			fgBrush = Brushes.White
		End If

		If e.Item.Selected Then
			bgBrush = SystemBrushes.Highlight
			fgBrush = SystemBrushes.HighlightText
		End If

		e.Graphics.FillRectangle(bgBrush, e.Bounds)

		Dim padding As Integer = 5
		Dim textBounds As Rectangle = e.Bounds
		textBounds.Inflate(-padding, 0)

		' 3. Gambar Teks di dalam textBounds yang sudah menyusut
		e.Graphics.DrawString(e.SubItem.Text, Lv_Formula_Parent.Font, fgBrush, textBounds, sf)

		sf.Dispose()
	End Sub

	Private Sub Lv_Formula_Order_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Formula_Order.DrawColumnHeader

		Using bgBrush As New Drawing2D.LinearGradientBrush(
			e.Bounds,
			Color.FromArgb(245, 245, 245),
			Color.FromArgb(220, 220, 220),
			Drawing2D.LinearGradientMode.Vertical)

			e.Graphics.FillRectangle(bgBrush, e.Bounds)
		End Using

		Using borderPen As New Pen(Color.FromArgb(180, 180, 180))
			e.Graphics.DrawLine(
				borderPen,
				e.Bounds.Left,
				e.Bounds.Bottom - 1,
				e.Bounds.Right,
				e.Bounds.Bottom - 1)
		End Using

		Using sf As New StringFormat()
			sf.Alignment = StringAlignment.Center
			sf.LineAlignment = StringAlignment.Center
			sf.Trimming = StringTrimming.EllipsisCharacter

			Dim textRect As Rectangle = Rectangle.Inflate(e.Bounds, -4, -2)

			e.Graphics.DrawString(
				e.Header.Text,
				Lv_Formula_Order.Font,
				Brushes.Black,
				textRect,
				sf)
		End Using
	End Sub

	Private Sub Lv_Formula_Order_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Formula_Order.DrawSubItem

		Dim sf As New StringFormat()
		sf.LineAlignment = StringAlignment.Center

		If BodyAlignments_Order.ContainsKey(e.ColumnIndex) Then
			sf.Alignment = BodyAlignments_Order(e.ColumnIndex)
		Else
			sf.Alignment = StringAlignment.Near
		End If

		sf.Trimming = StringTrimming.EllipsisCharacter
		sf.FormatFlags = StringFormatFlags.NoWrap

		Dim status As String = CStr(e.Item.Tag)
		Dim bgBrush As Brush = Brushes.White
		Dim fgBrush As Brush = Brushes.Black

		If status = "Y" Then
			bgBrush = Brushes.DarkRed
			fgBrush = Brushes.White
		End If

		If e.Item.Selected Then
			bgBrush = SystemBrushes.Highlight
			fgBrush = SystemBrushes.HighlightText
		End If

		e.Graphics.FillRectangle(bgBrush, e.Bounds)

		Dim padding As Integer = 5
		Dim textBounds As Rectangle = e.Bounds
		textBounds.Inflate(-padding, 0)

		e.Graphics.DrawString(e.SubItem.Text, Lv_Formula_Order.Font, fgBrush, textBounds, sf)

		sf.Dispose()
	End Sub

	Private Sub Lv_Detail_Bahan_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Detail_Bahan.DrawColumnHeader
		Using bgBrush As New Drawing2D.LinearGradientBrush(
			e.Bounds,
			Color.FromArgb(245, 245, 245),
			Color.FromArgb(220, 220, 220),
			Drawing2D.LinearGradientMode.Vertical)

			e.Graphics.FillRectangle(bgBrush, e.Bounds)
		End Using

		Using borderPen As New Pen(Color.FromArgb(180, 180, 180))
			e.Graphics.DrawLine(
				borderPen,
				e.Bounds.Left,
				e.Bounds.Bottom - 1,
				e.Bounds.Right,
				e.Bounds.Bottom - 1)
		End Using

		Using sf As New StringFormat()
			sf.Alignment = StringAlignment.Center
			sf.LineAlignment = StringAlignment.Center
			sf.Trimming = StringTrimming.EllipsisCharacter

			' Padding teks
			Dim textRect As Rectangle = Rectangle.Inflate(e.Bounds, -4, -2)

			e.Graphics.DrawString(
				e.Header.Text,
				Lv_Detail_Bahan.Font,
				Brushes.Black,
				textRect,
				sf)
		End Using
	End Sub

	Private Sub Lv_Detail_Bahan_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Detail_Bahan.DrawSubItem

		Dim sf As New StringFormat()
		sf.LineAlignment = StringAlignment.Center

		If BodyAlignments_Bahan.ContainsKey(e.ColumnIndex) Then
			sf.Alignment = BodyAlignments_Bahan(e.ColumnIndex)
		Else
			sf.Alignment = StringAlignment.Near
		End If

		sf.Trimming = StringTrimming.EllipsisCharacter
		sf.FormatFlags = StringFormatFlags.NoWrap

		Dim status As String = CStr(e.Item.Tag)
		Dim bgBrush As Brush = Brushes.White
		Dim fgBrush As Brush = Brushes.Black

		If status = "Y" Then
			bgBrush = Brushes.DarkRed
			fgBrush = Brushes.White
		End If

		If e.Item.Selected Then
			bgBrush = SystemBrushes.Highlight
			fgBrush = SystemBrushes.HighlightText
		End If

		e.Graphics.FillRectangle(bgBrush, e.Bounds)

		Dim padding As Integer = 5
		Dim textBounds As Rectangle = e.Bounds
		textBounds.Inflate(-padding, 0)

		' 3. Gambar Teks di dalam textBounds yang sudah menyusut
		e.Graphics.DrawString(e.SubItem.Text, Lv_Detail_Bahan.Font, fgBrush, textBounds, sf)

		sf.Dispose()
	End Sub

	Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
		If Lv_Formula_Parent.Items.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If

		'=========================================================
		'=     CEK APAKAH MOUSE BERADA DI ATAS ROWS LISTVIEW     =
		'=========================================================
		Dim mousePos As Point = Lv_Formula_Parent.PointToClient(Cursor.Position)
		Dim info As ListViewHitTestInfo = Lv_Formula_Parent.HitTest(mousePos)

		If info.Item Is Nothing Then
			e.Cancel = True
			Exit Sub
		End If

		Lv_Formula_Parent.FocusedItem = info.Item
		info.Item.Selected = True
	End Sub

	Private Sub ContextMenuStrip2_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip2.Opening
		If Lv_Formula_Order.Items.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If

		'=========================================================
		'=     CEK APAKAH MOUSE BERADA DI ATAS ROWS LISTVIEW     =
		'=========================================================
		Dim mousePos As Point = Lv_Formula_Order.PointToClient(Cursor.Position)
		Dim info As ListViewHitTestInfo = Lv_Formula_Order.HitTest(mousePos)

		If info.Item Is Nothing Then
			e.Cancel = True
			Exit Sub
		End If

		Lv_Formula_Order.FocusedItem = info.Item
		info.Item.Selected = True
	End Sub

End Class