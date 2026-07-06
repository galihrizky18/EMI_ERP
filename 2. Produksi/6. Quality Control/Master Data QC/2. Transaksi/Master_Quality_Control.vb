Public Class Master_Quality_Control

	Dim arrJenis_Input As New List(Of (Id_Kategori_Komponen As String, Keterangan As String, Kode As String, isOption As Boolean, isSlider As Boolean, isInput As Boolean))
	Dim arrFilter As New ArrayList

	Dim Cell_Switch_KdUnikItem As Integer = 0
	Dim Cell_Switch_IsDefault As Integer = 1
	Dim Cell_Switch_TampilLims As Integer = 2
	Dim Cell_Switch_Keterangan As Integer = 3

	Dim Lv_IDQC, Lv_KodeUji, Lv_Keterangan, Lv_Satuan, Lv_Jenis As String

	Dim Item_IdQC As Integer = 0
	Dim Item_KodeUji As Integer = 1
	Dim Item_Keterangan As Integer = 2
	Dim Item_Satuan As Integer = 3
	Dim Item_Jenis As Integer = 4

	Dim SelectedIdQC As String = ""

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Private Sub Master_Quality_Control_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBuffer(Lv_Data)

		Try
			OpenConn()

			'============================
			'=     GET NILAI SATUAN     =
			'============================
			Cmb_Satuan.Items.Clear()
			SQL = $"
				select Satuan
				from EMI_Satuan
				where Kode_Perusahaan = '{KodePerusahaan}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Satuan.Items.Add(Dr("Satuan"))
				Loop
			End Using

			arrJenis_Input.Clear()
			SQL = $"
				select Flag_Option, Flag_Slider, Flag_Input, Id_Kategori_Komponen, Keterangan, UPPER(Keterangan) AS KodeKeterangan
				from EMI_Kategori_Komponen
				where Kode_Perusahaan = '{KodePerusahaan}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					arrJenis_Input.Add((Dr("Id_Kategori_Komponen"), Dr("Keterangan"), Dr("KodeKeterangan"),
									   If(General_Class.CekNULL(Dr("Flag_Option")) = "Y", True, False),
									   If(General_Class.CekNULL(Dr("Flag_Slider")) = "Y", True, False),
									   If(General_Class.CekNULL(Dr("Flag_Input")) = "Y", True, False)
									   ))
				Loop
			End Using

			Cmb_Jenis_Input.Items.Clear()
			For Each item In arrJenis_Input
				Cmb_Jenis_Input.Items.Add(item.Keterangan)
			Next

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Lv_Data.Columns.Clear()
		Lv_Data.Columns.Add("Id_QC", 0, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Kode Uji", 150, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Keterangan", 560, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Satuan", 120, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("Jenis", 120, HorizontalAlignment.Center)
		Lv_Data.View = View.Details

		Cmb_Filter.Items.Clear() : arrFilter.Clear()
		Cmb_Filter.Items.Add("Kode Uji") : arrFilter.Add("a.Kode_Uji")
		Cmb_Filter.Items.Add("Keterangan") : arrFilter.Add("a.Keterangan")
		Cmb_Filter.Items.Add("Satuan") : arrFilter.Add("a.Satuan")
		Cmb_Filter.Items.Add("Jenis") : arrFilter.Add("b.Keterangan")

		Kosong()

	End Sub

	Private Sub Kosong()

		Txt_Kode.Text = ""
		Txt_Keterangan.Text = ""
		Cmb_Satuan.SelectedIndex = -1
		Cmb_Filter.SelectedIndex = -1
		Txt_Value_Filter.Text = ""
		Rd_Lapangan.Checked = False
		Rd_Lab.Checked = False
		Rd_Kategori_Desktop.Checked = False
		Rd_Kategori_Lims.Checked = False
		Cmb_Jenis_Input.SelectedIndex = 0
		Lbl_Value_1.Text = "Range Awal"
		Lbl_Value_2.Text = "Range Akhir"
		Txt_Value_1.Text = ""
		Txt_Value_2.Text = ""
		Txt_Value_1.Enabled = False
		Txt_Value_2.Enabled = False

		Txt_Kode.Enabled = True
		Txt_Kode.BackColor = Color.White

		Btn_Insert.Visible = False
		Dgv_Detail_Switch.Rows.Clear()
		Dgv_Detail_Switch.Enabled = False

		Btn_Simpan.Tag = "SIMPAN"
		Btn_Simpan.Text = $"Simpan"

		SelectedIdQC = ""

		LoadData()

		Txt_Kode.Focus()

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub GetDataLv(ByVal index As Integer)
		Lv_IDQC = Lv_Data.Items(index).SubItems(Item_IdQC).Text
		Lv_KodeUji = Lv_Data.Items(index).SubItems(Item_KodeUji).Text
		Lv_Keterangan = Lv_Data.Items(index).SubItems(Item_Keterangan).Text
		Lv_Satuan = Lv_Data.Items(index).SubItems(Item_Satuan).Text
		Lv_Jenis = Lv_Data.Items(index).SubItems(Item_Jenis).Text
	End Sub

	Private Sub Cmb_Jenis_Input_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Input.SelectedIndexChanged
		If Cmb_Jenis_Input.Items.Count = 0 Or Cmb_Jenis_Input.SelectedIndex = -1 Then Exit Sub

		Lbl_Value_1.Text = "Range Awal"
		Lbl_Value_2.Text = "Range Akhir"
		Txt_Value_1.Text = ""
		Txt_Value_2.Text = ""
		Dgv_Detail_Switch.Rows.Clear()
		Dgv_Detail_Switch.Enabled = False
		Txt_Value_1.BackColor = Color.FromArgb(235, 235, 235)
		Txt_Value_2.BackColor = Color.FromArgb(235, 235, 235)
		Btn_Insert.Visible = False
		Txt_Value_1.TextAlign = HorizontalAlignment.Right
		If arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).isSlider Then
			Txt_Value_1.Enabled = True
			Txt_Value_2.Enabled = True
			Txt_Value_1.BackColor = Color.White
			Txt_Value_2.BackColor = Color.White
		ElseIf arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).isOption Then
			Txt_Value_1.Enabled = True
			Txt_Value_2.Enabled = False
			Txt_Value_1.BackColor = Color.White
			Lbl_Value_1.Text = "Keterangan"
			Txt_Value_1.TextAlign = HorizontalAlignment.Left
			Dgv_Detail_Switch.Enabled = True
			Btn_Insert.Visible = True
		ElseIf arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).isInput Then
			Txt_Value_1.Enabled = False
			Txt_Value_2.Enabled = False
		End If

	End Sub

	Private Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_Insert.Click
		If Not arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).isOption Then Exit Sub

		If Txt_Value_1.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Items Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Value_1.Focus()
			Exit Sub
		End If

		Try

			Dim KdUnikItem As String = Generate_KdUnik_items_Switch()

			With Dgv_Detail_Switch.Rows(Dgv_Detail_Switch.Rows.Add)
				.Cells(Cell_Switch_KdUnikItem).Value = KdUnikItem
				.Cells(Cell_Switch_IsDefault).Value = False
				.Cells(Cell_Switch_TampilLims).Value = False
				.Cells(Cell_Switch_Keterangan).Value = Txt_Value_1.Text.Trim
				.DefaultCellStyle.Padding = New Padding(2, 0, 2, 0)
			End With
		Catch ex As Exception
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Txt_Value_1.Text = ""
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Cmb_Filter.SelectedIndex <> -1 Then
			If Txt_Value_Filter.Text.Trim.Length = 0 Then
				MessageBox.Show("Nilai Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_Value_Filter.Focus()
				Exit Sub
			End If
		End If

		LoadData()
	End Sub

	Private Sub LoadData()
		Try
			OpenConn()

			Dim Filter As String = ""

			If Cmb_Filter.SelectedIndex <> -1 Then
				If Txt_Value_Filter.Text.Trim.Length <> 0 Then
					Filter &= $"and {arrFilter(Cmb_Filter.SelectedIndex)} like '%{Txt_Value_Filter.Text.Trim}%' "
				End If
			End If

			Lv_Data.BeginUpdate()
			Lv_Data.Items.Clear()
			SQL = $"
				select a.Id_QC_Formula, a.Kode_Uji, a.Keterangan, a.Satuan, a.Id_Kategori_Komponen, b.Keterangan as Jenis
				from EMI_Quality_Control a
					inner join EMI_Kategori_Komponen b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Komponen = b.Id_Kategori_Komponen
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				order by a.Kode_Uji
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Data.Items.Add(Dr("Id_QC_Formula"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Kode_Uji")) = "", "-", Dr("Kode_Uji")))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan")) = "", "-", Dr("Keterangan")))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Satuan")) = "", "-", Dr("Satuan")))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jenis")) = "", "-", Dr("Jenis")))
				Loop
			End Using
			Lv_Data.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Txt_Kode.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Uji Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kode.Focus()
			Exit Sub
		ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Keterangan.Focus()
			Exit Sub
		ElseIf Cmb_Satuan.SelectedIndex = -1 Then
			MessageBox.Show("Satuan Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Satuan.DroppedDown = True
			Cmb_Satuan.Focus()
			Exit Sub
		End If

		If Not Rd_Lapangan.Checked And Not Rd_Lab.Checked Then
			MessageBox.Show("Tampil QC Harus Minimal Dipilih 1", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Rd_Lapangan.Focus()
			Exit Sub
		End If

		If Not Rd_Kategori_Desktop.Checked And Not Rd_Kategori_Lims.Checked Then
			MessageBox.Show("Jenis Kategori Harus Minimal Dipilih 1", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Rd_Kategori_Desktop.Focus()
			Exit Sub
		End If

		If Cmb_Jenis_Input.SelectedIndex = -1 Then
			MessageBox.Show("Jenis Input Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Satuan.DroppedDown = True
			Cmb_Satuan.Focus()
			Exit Sub
		Else
			If arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).isOption Then
				If Dgv_Detail_Switch.Rows.Count = 0 Then
					MessageBox.Show("Isi Switch Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Dgv_Detail_Switch.Focus()
					Exit Sub
				End If
				Dim IsChecked As Boolean = False
				For Each row As DataGridViewRow In Dgv_Detail_Switch.Rows
					If Not row.IsNewRow Then
						If CBool(row.Cells(Cell_Switch_IsDefault).Value) Then
							IsChecked = True
							Exit For
						End If
					End If
				Next
				If Not IsChecked Then
					MessageBox.Show("Switch Harus Memiliki Nilai Default", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Dgv_Detail_Switch.Focus()
					Exit Sub
				End If
			ElseIf arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).isSlider Then
				If Txt_Value_1.Text.Trim.Length = 0 Then
					MessageBox.Show("Range Awal Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_Value_1.Focus()
					Exit Sub
				ElseIf Txt_Value_2.Text.Trim.Length = 0 Then
					MessageBox.Show("Range Akhir Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_Value_2.Focus()
					Exit Sub
				End If
			End If
		End If

		Dim F_Dekstop As String = If(Rd_Lapangan.Checked, "'Y'", "NULL")
		Dim F_Android As String = If(Rd_Lab.Checked, "'Y'", "NULL")
		Dim Range_Awal As String = If(Txt_Value_1.Text.Trim.Length = 0, "NULL", $"{Val(HilangkanTanda(Txt_Value_1.Text.Trim))}")
		Dim Range_Akhir As String = If(Txt_Value_2.Text.Trim.Length = 0, "NULL", $"{Val(HilangkanTanda(Txt_Value_2.Text.Trim))}")
		Dim Kategori_Value As String = "NULL"
		If Rd_Kategori_Desktop.Checked Then
			Kategori_Value = $"'DESKTOP'"
		ElseIf Rd_Kategori_Lims.Checked Then
			Kategori_Value = $"'LIMS'"
		End If

		If MessageBox.Show($"Yakin Ingin Melakukan {If(Btn_Simpan.Tag = "SIMPAN", "Simpan", "Update")} Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If Btn_Simpan.Tag = "SIMPAN" Then

				'=============================================
				'=     CEK APAKAH ADA KODE UJI YANG SAMA     =
				'=============================================
				SQL = $"
					select 1
					from EMI_Quality_Control
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Kode_Uji = '{Txt_Kode.Text}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						MessageBox.Show("Terjadi Kesalahan, Kode Uji Sudah Ada, Harap Input Kode Uji yang Berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Return
					End If
				End Using

				SQL = $"
					Insert Into EMI_Quality_Control(Kode_Perusahaan,Kode_Uji,Keterangan,
					Satuan,Target,Id_Kategori_Komponen,flag_tampil_dekstop,flag_tampil_android,range_awal,range_akhir, Flag_Tampil_Formula, Flag_Tampil_Bahan, Kategori_Value)
					Values('{KodePerusahaan}', '{Txt_Kode.Text}', '{Txt_Keterangan.Text.Trim}', '{Cmb_Satuan.Text.Trim}',NULL,
					'{arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).Id_Kategori_Komponen}', {F_Dekstop}, {F_Android},
					{Range_Awal}, {Range_Akhir}, 'Y', 'Y', {Kategori_Value})
				"
				ExecuteTrans(SQL)

				If arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).isOption Then
					Dim No_ID_QC As Integer = 0
					SQL = "select IDENT_CURRENT('EMI_Quality_Control') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							No_ID_QC = Dr("urutan")
						End If
					End Using

					For Each row As DataGridViewRow In Dgv_Detail_Switch.Rows
						If Not row.IsNewRow Then

							Dim Kode_Keterangan As String = ""
							If Rd_Kategori_Desktop.Checked Then
								Kode_Keterangan = row.Cells(Cell_Switch_Keterangan).Value
							ElseIf Rd_Kategori_Lims.Checked Then
								Kode_Keterangan = If(Convert.ToBoolean(row.Cells(Cell_Switch_TampilLims).Value), $"-{row.Cells(Cell_Switch_KdUnikItem).Value}", row.Cells(Cell_Switch_KdUnikItem).Value)
							Else
								MessageBox.Show("Terjadi Kesalahan, Kategori Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Return
							End If
							Dim Keterangan As String = row.Cells(Cell_Switch_Keterangan).Value
							Dim IsDefault As String = If(Convert.ToBoolean(row.Cells(Cell_Switch_IsDefault).Value), "'Y'", "NULL")
							Dim IsTampilLims As String = If(Convert.ToBoolean(row.Cells(Cell_Switch_TampilLims).Value), "'Y'", "NULL")

							'=================================
							'=     CEK KETERANGAN UNIQUE     =
							'=================================
							If Rd_Kategori_Lims.Checked Then
								SQL = $"
									select 1
									from EMI_Switch
									where Kode_Perusahaan = '{KodePerusahaan}'
									and Keterangan = '{Kode_Keterangan}'
								"
								Using Dr = OpenTrans(SQL)
									If Dr.Read Then
										Dr.Close()
										MessageBox.Show("Terjadi Kesalahan, Harap Ulangi Penginputan Item Switch", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Return
									End If
								End Using

							End If

							SQL = $"
								insert into EMI_Switch(Kode_Perusahaan, Id_QC_Formula, Keterangan, Label_Keterangan, Flag_Default, Flag_Tampil_Lims)
								Values('{KodePerusahaan}', '{No_ID_QC}',
								'{Kode_Keterangan}', '{Keterangan}', {IsDefault}, {IsTampilLims})
							"
							ExecuteTrans(SQL)

						End If
					Next

				End If

			ElseIf Btn_Simpan.Tag = "UPDATE" Then

				If SelectedIdQC.Trim.Length = 0 Then
					MessageBox.Show("Terjadi Kesalahan, Id Quality Control Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If

				SQL = "select Kode_Uji from EMI_Pembelian_QC_Bahan_Det where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Uji = '" & Txt_Kode.Text & "'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						MessageBox.Show("Terjadi Kesalahan Kode Uji Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Return
					End If
				End Using

				SQL = "select Kode_Uji from EMI_Transaksi_Formulator_QC_Det where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Uji = '" & Txt_Kode.Text & "'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						MessageBox.Show("Terjadi Kesalahan, Kode Uji Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Return
					End If
				End Using

				Dim IsLimsAlready As Boolean = False
				SQL = $"
					Select b.Keterangan as Ket_kategori,b.Flag_Option, a.Kategori_Value
					From EMI_Quality_Control a, EMI_Kategori_Komponen b where a.kode_perusahaan = b.kode_perusahaan and
					a.Id_Kategori_Komponen  = b.Id_Kategori_Komponen and
					a.kode_perusahaan = '{KodePerusahaan}'
					and a.kode_uji = '{Txt_Kode.Text.Trim}'
					and a.Id_QC_Formula = '{SelectedIdQC}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						IsLimsAlready = If(General_Class.CekNULL(Dr("Kategori_Value")) = "LIMS", True, False)
						Dim isOption As String = If(General_Class.CekNULL(Dr("Flag_Option")) = "", "T", Dr("Flag_Option"))
						Dr.Close()

						If isOption = "Y" Then
							SQL = $"
								delete from EMI_Switch
								where Kode_Perusahaan = '{KodePerusahaan}'
								and Id_QC_Formula = '{SelectedIdQC}'
							"
							ExecuteTrans(SQL)
						End If
					Else
						Dr.Close()
						MessageBox.Show("Terjadi Kesalahan, Kode Uji Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Return
					End If
				End Using

				'==========================
				'=     UPDATE DATA QC     =
				'==========================
				SQL = $"
					update EMI_Quality_Control set Keterangan = '{Txt_Keterangan.Text.Trim}', Satuan = '{Cmb_Satuan.Text.Trim}',
						Id_Kategori_Komponen= '{arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).Id_Kategori_Komponen}',
						flag_tampil_dekstop = {F_Dekstop}, flag_tampil_android = {F_Android}, range_awal = {Range_Awal}, range_akhir = {Range_Akhir},
						Kategori_Value = {Kategori_Value}
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_QC_Formula = '{SelectedIdQC}'
					and Kode_Uji = '{Txt_Kode.Text.Trim}'
				"
				ExecuteTrans(SQL)

				If arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).isOption Then

					For Each row As DataGridViewRow In Dgv_Detail_Switch.Rows
						If Not row.IsNewRow Then

							Dim Kode_Keterangan As String = ""
							If Rd_Kategori_Desktop.Checked Then
								Kode_Keterangan = row.Cells(Cell_Switch_Keterangan).Value
							ElseIf Rd_Kategori_Lims.Checked Then
								Kode_Keterangan = If(Convert.ToBoolean(row.Cells(Cell_Switch_TampilLims).Value), $"-{row.Cells(Cell_Switch_KdUnikItem).Value}", row.Cells(Cell_Switch_KdUnikItem).Value)
							Else
								MessageBox.Show("Terjadi Kesalahan, Kategori Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Return
							End If

							If Not IsLimsAlready Then
								Kode_Keterangan = If(Convert.ToBoolean(row.Cells(Cell_Switch_TampilLims).Value), $"-{Generate_KdUnik_items_Switch()}", Generate_KdUnik_items_Switch())
							End If

							Dim Keterangan As String = row.Cells(Cell_Switch_Keterangan).Value
							Dim IsDefault As String = If(Convert.ToBoolean(row.Cells(Cell_Switch_IsDefault).Value), "'Y'", "NULL")
							Dim IsTampilLims As String = If(Convert.ToBoolean(row.Cells(Cell_Switch_TampilLims).Value), "'Y'", "NULL")

							'=================================
							'=     CEK KETERANGAN UNIQUE     =
							'=================================
							If Rd_Kategori_Lims.Checked Then
								SQL = $"
									select 1
									from EMI_Switch
									where Kode_Perusahaan = '{KodePerusahaan}'
									and Keterangan = '{Kode_Keterangan}'
								"
								Using Dr = OpenTrans(SQL)
									If Dr.Read Then
										Dr.Close()
										MessageBox.Show("Terjadi Kesalahan, Harap Ulangi Penginputan Item Switch", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Return
									End If
								End Using

							End If

							SQL = $"
								insert into EMI_Switch(Kode_Perusahaan, Id_QC_Formula, Keterangan, Label_Keterangan, Flag_Default, Flag_Tampil_Lims)
								Values('{KodePerusahaan}', '{SelectedIdQC}',
								'{Kode_Keterangan}', '{Keterangan}', {IsDefault}, {IsTampilLims})
							"
							ExecuteTrans(SQL)

						End If
					Next

				End If

			End If

			Cmd.Transaction.Commit()
			MessageBox.Show("Data Berhasil Diinput", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		Finally
			CloseTrans()
			CloseConn()
		End Try

		Kosong()
	End Sub

	Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
		If SelectedIdQC.Trim.Length = 0 Or Txt_Kode.Text.Trim.Length = 0 Then
			MessageBox.Show("Pilih Dahulu Data yang Ingin Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show("Yakin Ingin Menghapus Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			SQL = "select Kode_Uji from EMI_Pembelian_QC_Bahan_Det where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Uji = '" & Txt_Kode.Text & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					MessageBox.Show("Terjadi Kesalahan Kode Uji Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If
			End Using

			SQL = "select Kode_Uji from EMI_Transaksi_Formulator_QC_Det where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Uji = '" & Txt_Kode.Text & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					MessageBox.Show("Terjadi Kesalahan Kode Uji Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If
			End Using

			SQL = "Select b.Keterangan as Ket_kategori,b.Flag_Option "
			SQL = SQL & "From EMI_Quality_Control a, EMI_Kategori_Komponen b where a.kode_perusahaan = b.kode_perusahaan and "
			SQL = SQL & "a.Id_Kategori_Komponen  = b.Id_Kategori_Komponen and "
			SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.kode_uji = '" & Txt_Kode.Text & "' "
			SQL = SQL & "and a.Id_QC_Formula = '" & SelectedIdQC & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If Dr("Flag_Option") = "Y" Then
						Dr.Close()
						SQL = "delete from EMI_Switch where kode_perusahaan = '" & KodePerusahaan & "' and "
						SQL = SQL & "Id_QC_Formula = '" & SelectedIdQC & "'"
						ExecuteTrans(SQL)
					End If
				Else
					Dr.Close()
					MessageBox.Show("Terjadi Kesalahan Kode Uji Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If
			End Using

			SQL = "Delete From EMI_Quality_Control where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "Kode_Uji = '" & Txt_Kode.Text & "' "
			SQL = SQL & "and Id_QC_Formula = '" & SelectedIdQC & "' "
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			MessageBox.Show($"Data Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		Finally
			CloseTrans()
			CloseConn()
		End Try

		Kosong()

	End Sub

	'======================================================================================================================================================
	'=     HELPER
	'======================================================================================================================================================

	Private Sub Dgv_Detail_Switch_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Detail_Switch.CellValueChanged
		If e.RowIndex < 0 OrElse e.ColumnIndex <> Cell_Switch_IsDefault Then Exit Sub

		Dim dgv = Dgv_Detail_Switch
		Dim isChecked As Boolean = CBool(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)

		If Not isChecked Then Exit Sub

		RemoveHandler dgv.CellValueChanged, AddressOf Dgv_Detail_Switch_CellValueChanged

		For Each row As DataGridViewRow In dgv.Rows
			If Not row.IsNewRow Then
				row.Cells(Cell_Switch_IsDefault).Value = (row.Index = e.RowIndex)
			End If
		Next

		AddHandler dgv.CellValueChanged, AddressOf Dgv_Detail_Switch_CellValueChanged
	End Sub

	Private Sub Dgv_Detail_Switch_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles Dgv_Detail_Switch.CurrentCellDirtyStateChanged
		If Dgv_Detail_Switch.IsCurrentCellDirty Then
			Dgv_Detail_Switch.CommitEdit(DataGridViewDataErrorContexts.Commit)
		End If
	End Sub

	Private Sub Txt_Value_1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value_1.KeyPress, Txt_Value_2.KeyPress
		'=============================
		'=     IZINKAN BACKSAPCE     =
		'=============================
		If e.KeyChar = Chr(8) Then Exit Sub

		'=============================
		'=     HANDLE ENTER     =
		'=============================
		If e.KeyChar = Chr(13) Then
			Btn_Simpan.PerformClick()
			e.Handled = True
			Exit Sub
		End If


		Dim txt As TextBox = DirectCast(sender, TextBox)

		'=============================
		'=     CEK APAKAH SLIDER     =
		'=============================
		If arrJenis_Input(Cmb_Jenis_Input.SelectedIndex).isSlider Then

			'================================
			'=     INPUT YANG DIIZINKAN     =
			'================================
			Dim allowedChars As String = "0123456789.-"
			If Not allowedChars.Contains(e.KeyChar) Then
				e.Handled = True
				Exit Sub
			End If

			'===============================================
			'=     HANDLE CEK TITIK HANYA BOLEH 1 KALI     =
			'===============================================
			If e.KeyChar = "."c AndAlso txt.Text.Contains(".") Then
				e.Handled = True
				Exit Sub
			End If

			'================================================
			'=     HANDLE CEK MINUS HANYA BOLEH DI AWAL     =
			'================================================
			If e.KeyChar = "-"c AndAlso txt.SelectionStart <> 0 Then
				e.Handled = True
				Exit Sub
			End If
		Else

			'==========================================
			'=     HANDLE JIKA SELAIN DARI SLIDER     =
			'==========================================
			'If Not Char.IsLetterOrDigit(e.KeyChar) Then
			'	e.Handled = True
			'	Exit Sub
			'End If
			Exit Sub
		End If

		'====================
		'=     GET TEXT     =
		'====================
		Dim futureText As String = txt.Text.Substring(0, txt.SelectionStart) & e.KeyChar & txt.Text.Substring(txt.SelectionStart + txt.SelectionLength)

		'===================================
		'=     HANDLE CEGAH 00 DI AWAL     =
		'===================================
		If futureText.Length > 1 AndAlso futureText.StartsWith("0") AndAlso Not futureText.StartsWith("0.") Then
			e.Handled = True
			Exit Sub
		End If

		'===========================================
		'=     CEK VALIDASI DAN RANGE MAKSIMAL     =
		'===========================================
		Dim value As Double
		If Double.TryParse(futureText, value) Then
			If value < -10000000000000 OrElse value > 10000000000000 Then
				e.Handled = True
				Exit Sub
			End If
		Else
			' Izinkan jika hanya berisi tanda minus sementara
			If futureText = "-" Then Exit Sub
			e.Handled = True
			Exit Sub
		End If

	End Sub

	Private Sub Txt_Kode_Leave(sender As Object, e As EventArgs) Handles Txt_Kode.Leave
		If Txt_Kode.Text.Trim.Length = 0 Then Exit Sub

		Try
			OpenConn()

			Dim kodeUji As String = Txt_Kode.Text.Trim
			Dim hasDataSwitch As Boolean = False
			SQL = $"
				select a.Id_QC_Formula, a.Kode_Uji, a.Keterangan, a.Satuan, a.Id_Kategori_Komponen, b.Keterangan,
					a.Flag_Tampil_Dekstop, a.Flag_Tampil_Android, a.Kategori_Value, a.Id_Kategori_Komponen, a.Range_Awal, a.Range_Akhir
				from EMI_Quality_Control a
					inner join EMI_Kategori_Komponen b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Komponen = b.Id_Kategori_Komponen
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Kode_Uji = '{kodeUji}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Txt_Kode.Enabled = False
					Txt_Kode.BackColor = Color.FromArgb(235, 235, 235)
					SelectedIdQC = Dr("Id_QC_Formula")
					Btn_Simpan.Text = "&Update"
					Btn_Simpan.Tag = "UPDATE"
					Txt_Keterangan.Text = Dr("Keterangan")
					Cmb_Satuan.SelectedItem = Dr("Satuan")
					Rd_Lapangan.Checked = If(General_Class.CekNULL(Dr("Flag_Tampil_Dekstop")) = "", False, True)
					Rd_Lab.Checked = If(General_Class.CekNULL(Dr("Flag_Tampil_Android")) = "", False, True)

					If General_Class.CekNULL(Dr("Kategori_Value")) = "DESKTOP" Then
						Rd_Kategori_Desktop.Checked = True
						Rd_Kategori_Lims.Checked = False
					ElseIf General_Class.CekNULL(Dr("Kategori_Value")) = "LIMS" Then
						Rd_Kategori_Desktop.Checked = False
						Rd_Kategori_Lims.Checked = True
					Else
						Rd_Kategori_Desktop.Checked = False
						Rd_Kategori_Lims.Checked = False
					End If
					Cmb_Jenis_Input.SelectedIndex = arrJenis_Input.FindIndex(Function(x) x.Id_Kategori_Komponen = Dr("Id_Kategori_Komponen"))
					Txt_Value_1.Text = If(General_Class.CekNULL(Dr("Range_Awal")) = "", "", Dr("Range_Awal"))
					Txt_Value_2.Text = If(General_Class.CekNULL(Dr("Range_Akhir")) = "", "", Dr("Range_Akhir"))

					hasDataSwitch = arrJenis_Input.Any(Function(x) x.Id_Kategori_Komponen = Dr("Id_Kategori_Komponen").ToString() AndAlso x.isOption = True)

				End If
			End Using

			If hasDataSwitch Then
				Dgv_Detail_Switch.Rows.Clear()
				SQL = $"
					select Keterangan, Flag_Default, Flag_Tampil_Lims, Label_Keterangan
					from EMI_Switch
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_QC_Formula = '{SelectedIdQC}'
				"
				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						With Dgv_Detail_Switch.Rows(Dgv_Detail_Switch.Rows.Add)
							.Cells(Cell_Switch_KdUnikItem).Value = Dr("Keterangan").ToString.Replace("-", "")
							.Cells(Cell_Switch_IsDefault).Value = If(General_Class.CekNULL(Dr("Flag_Default")) = "", False, True)
							.Cells(Cell_Switch_TampilLims).Value = If(General_Class.CekNULL(Dr("Flag_Tampil_Lims")) = "", False, True)
							.Cells(Cell_Switch_Keterangan).Value = Dr("Label_Keterangan").ToString.Trim
							.DefaultCellStyle.Padding = New Padding(2, 0, 2, 0)
						End With
					Loop
				End Using
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged

		If Cmb_Filter.SelectedIndex = -1 Then
			Txt_Value_Filter.Enabled = False
			Txt_Value_Filter.BackColor = Color.FromArgb(235, 235, 235)
		Else
			Txt_Value_Filter.Enabled = True
			Txt_Value_Filter.BackColor = Color.White
		End If
		Txt_Value_Filter.Text = ""
	End Sub

	Private Sub Lv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
		If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

		GetDataLv(Lv_Data.FocusedItem.Index)
		Txt_Kode.Text = Lv_KodeUji.Trim
		Txt_Kode_Leave(sender, e)

	End Sub

	'======================================================================================================================================================
	'=     UTILITY
	'======================================================================================================================================================
	Protected Overrides Sub WndProc(ByRef m As Message)
		If m.Msg = &HA3 Then
			Return
		End If

		MyBase.WndProc(m)
	End Sub

	Private Function Generate_KdUnik_items_Switch() As String
		Dim JumlahDigit As Integer = 9

		Dim rnd As New Random()
		Dim min As Integer = CInt(10 ^ (JumlahDigit - 1))
		Dim max As Integer = CInt(10 ^ JumlahDigit)
		Return rnd.Next(min, max).ToString()
	End Function

	Private Sub Dgv_Detail_Switch_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Dgv_Detail_Switch.CellPainting
		If e.RowIndex = -1 Then
			e.Paint(e.ClipBounds, DataGridViewPaintParts.Background Or DataGridViewPaintParts.ContentForeground Or DataGridViewPaintParts.SelectionBackground)

			Using p As New Pen(Color.Gray, 1)
				e.Graphics.DrawLine(p, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1)
			End Using

			e.Handled = True
		End If
	End Sub

	Private Sub EnableDoubleBuffer(lvw As ListView)
		Dim t As Type = lvw.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(lvw, True, Nothing)
	End Sub

	Private Sub HandleListViewHover(lvw As ListView, e As MouseEventArgs)

		Dim hit As ListViewHitTestInfo = lvw.HitTest(e.Location)
		Dim lastItem As ListViewItem = TryCast(lvw.Tag, ListViewItem)

		' =========================
		' 👉 CURSOR
		' =========================
		If hit.Item IsNot Nothing Then
			lvw.Cursor = Cursors.Hand
		Else
			lvw.Cursor = Cursors.Default
		End If

		' =========================
		' 👉 KELUAR AREA
		' =========================
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

		' =========================
		' 👉 SKIP ITEM CUSTOM (PAKAI TAG)
		' =========================
		If currentItem.Tag IsNot Nothing Then
			Exit Sub
		End If

		' =========================
		' 👉 HOVER LOGIC
		' =========================
		If lastItem IsNot currentItem Then

			lvw.BeginUpdate()

			' Reset sebelumnya
			If lastItem IsNot Nothing Then
				If lastItem.BackColor = Color.FromArgb(235, 235, 235) Then
					lastItem.BackColor = Color.White
				End If
			End If

			' Set hover
			currentItem.BackColor = Color.FromArgb(235, 235, 235)

			lvw.Tag = currentItem

			lvw.EndUpdate()
		End If
	End Sub

	'======================================================================================================================================================
	'=     HANDLE KEYPRESS
	'======================================================================================================================================================
	Private Sub Txt_Kode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kode.KeyPress
		If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
	End Sub

	Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Satuan.DroppedDown = True
			Cmb_Satuan.Focus()
		End If
	End Sub

	Private Sub Rd_Lapangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Rd_Lapangan.KeyPress, Rd_Lab.KeyPress
		If e.KeyChar = Chr(13) Then Rd_Kategori_Desktop.Focus()
	End Sub

	Private Sub Rd_Kategori_Desktop_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Rd_Kategori_Desktop.KeyPress

	End Sub

End Class