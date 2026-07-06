Public Class Master_Quality_Control_Backup
	Dim arrcari, arrJenisQC, arrFlg_Option As New ArrayList
	Dim Jenis = "Master_Quality_Control"
	Dim id_qc, Flg_option As String

	Dim Lv1_Kode As String
	Dim Lv1_Ket As String
	Dim Lv1_Target As String
	Dim Lv1_Unit As String
	Dim Lv1_Jns As String
	Dim Lv1_Tampil_For As String
	Dim Lv1_Tampil_Bahan As String
	Dim Lv1_Id_QC As String
	Dim Lv1_Id_Kom As String
	Dim Lv1_Flag As String

	Dim Lv2_Ket As String
	Dim Lv2_Id_QC As String
	Dim Lv2_Id_Switch As String

	Private Sub Get_Isi_LvDataQC(ByVal No_Index As Integer)
		Lv1_Kode = Lv_DataQC.Items(No_Index).Text
		Lv1_Ket = Lv_DataQC.Items(No_Index).SubItems(1).Text
		Lv1_Target = Lv_DataQC.Items(No_Index).SubItems(2).Text
		Lv1_Unit = Lv_DataQC.Items(No_Index).SubItems(3).Text
		Lv1_Jns = Lv_DataQC.Items(No_Index).SubItems(4).Text
		Lv1_Tampil_For = Lv_DataQC.Items(No_Index).SubItems(5).Text
		Lv1_Tampil_Bahan = Lv_DataQC.Items(No_Index).SubItems(6).Text
		Lv1_Id_QC = Lv_DataQC.Items(No_Index).SubItems(7).Text
		Lv1_Id_Kom = Lv_DataQC.Items(No_Index).SubItems(8).Text
		Lv1_Flag = Lv_DataQC.Items(No_Index).SubItems(9).Text
	End Sub

	Private Sub Get_Isi_LV2(ByVal No_Index As Integer)
		Lv2_Ket = Lv_Switch.Items(No_Index).Text
		Lv2_Id_QC = Lv_Switch.Items(No_Index).SubItems(1).Text
		Lv2_Id_Switch = Lv_Switch.Items(No_Index).SubItems(2).Text
	End Sub

	Private Sub kosong()
		Txt_Kode.Text = ""
		Txt_Keterangan.Text = ""

		Txt_Kode.Focus()
		Txt_Kode.Enabled = True

		Txt_Switch.Text = ""
		Lv_Switch.Items.Clear()
		Txt_Switch.Visible = False
		Lv_Switch.Visible = False
		Rb_LapanganYa.Checked = False
		Rb_LapanganTdk.Checked = False
		Rb_LabYa.Checked = False
		Rb_LabTdk.Checked = False
		GrBox_Range.Visible = False
		Txt_RangeAwal.Text = ""
		Txt_RangeAkhir.Text = ""

		Tb_Lapangan.Checked = False : Rb_Lab.Checked = False

		Txt_FilterValue.Text = ""
		Cmb_FilterValue.Items.Clear() : arrcari.Clear()
		Cmb_FilterValue.Items.Add(Base_Language.Lang_Quality_Control_Kode) : arrcari.Add("a.kode_Uji")
		Cmb_FilterValue.Items.Add(Base_Language.Lang_Quality_Control_Keterangan) : arrcari.Add("a.keterangan")
		'''ComboBox1.Items.Add(Base_Language.Lang_Quality_Control_Target) : arrcari.Add("a.Target")
		Cmb_FilterValue.Items.Add(Base_Language.Lang_Quality_Control_Satuan) : arrcari.Add("a.Satuan")
		Cmb_FilterValue.SelectedIndex = -1

		'CmbJenis.Items.Clear() : arrJenisQC.Clear()
		'CmbJenis.Items.Add("Input") : arrJenisQC.Add("Input")
		'CmbJenis.Items.Add("Check") : arrJenisQC.Add("Check")

		Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
		Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
		Btn_Cari.Text = Base_Language.Lang_Global_Cari
		Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
		Btn_Simpan.Tag = "&Simpan"
		Btn_Hapus.Enabled = False

		Try
			OpenConn()

			Lv_DataQC.Items.Clear()
			SQL = "Select a.Id_QC_Formula,a.Kode_Perusahaan,a.Kode_Uji,a.Keterangan,a.Satuan,a.Target,"
			SQL = SQL & "a.Flag_Tampil_Formula,a.Flag_Tampil_Bahan,a.Id_Kategori_Komponen,b.Keterangan as Ket_kategori,b.Flag_Option, flag_tampil_dekstop,flag_tampil_android "
			SQL = SQL & "From EMI_Quality_Control a, EMI_Kategori_Komponen b where a.kode_perusahaan = b.kode_perusahaan and "
			SQL = SQL & "a.Id_Kategori_Komponen  = b.Id_Kategori_Komponen and "
			SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' order by a.kode_uji "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Dim Lvw As ListViewItem
					Lvw = Lv_DataQC.Items.Add(dr("Kode_Uji"))
					Lvw.SubItems.Add(If(General_Class.CekNULL(dr("Keterangan")) = "", "-", dr("Keterangan")))
					Lvw.SubItems.Add("")
					Lvw.SubItems.Add(If(General_Class.CekNULL(dr("Satuan")) = "", "-", dr("Satuan")))
					Lvw.SubItems.Add(If(General_Class.CekNULL(dr("Ket_kategori")) = "", "-", dr("Ket_kategori")))
					If General_Class.CekNULL(dr("Flag_Tampil_Formula")) = "Y" Then
						Lvw.SubItems.Add(dr("Flag_Tampil_Formula"))
					Else
						Lvw.SubItems.Add("T")
					End If
					If General_Class.CekNULL(dr("Flag_Tampil_Bahan")) = "Y" Then
						Lvw.SubItems.Add(dr("Flag_Tampil_Bahan"))
					Else
						Lvw.SubItems.Add("T")
					End If
					Lvw.SubItems.Add(dr("Id_QC_Formula"))
					Lvw.SubItems.Add(dr("Id_Kategori_Komponen"))
					Lvw.SubItems.Add(dr("Flag_Option"))
					If General_Class.CekNULL(dr("flag_tampil_dekstop")) = "Y" Then
						Lvw.SubItems.Add(dr("flag_tampil_dekstop"))
					Else
						Lvw.SubItems.Add("T")
					End If
					If General_Class.CekNULL(dr("flag_tampil_android")) = "Y" Then
						Lvw.SubItems.Add(dr("flag_tampil_android"))
					Else
						Lvw.SubItems.Add("T")
					End If
				Loop
			End Using

			Cmb_Satuan.Items.Clear()
			SQL = "select Satuan from EMI_Satuan where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & " and flag_tampil_formulator='Y' "
			SQL = SQL & " order by Satuan"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Cmb_Satuan.Items.Add(dr("Satuan"))
				Loop
			End Using
			Cmb_Satuan.SelectedIndex = -1

			Cmb_Jenis.Items.Clear() : arrJenisQC.Clear() : arrFlg_Option.Clear()
			SQL = "select Id_Kategori_Komponen,Keterangan,Flag_Option from EMI_Kategori_Komponen "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Cmb_Jenis.Items.Add(dr("Keterangan")) : arrJenisQC.Add(dr("Id_Kategori_Komponen")) : arrFlg_Option.Add(dr("Flag_Option"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Cari(ByVal semua As String)
		Try
			OpenConn()

			Lv_DataQC.Items.Clear()
			SQL = "Select a.Id_QC_Formula,a.Kode_Perusahaan,a.Kode_Uji,a.Keterangan,a.Satuan,Target,"
			SQL = SQL & "a.Flag_Tampil_Formula,a.Flag_Tampil_Bahan,a.Id_Kategori_Komponen,b.Keterangan as Ket_kategori,b.Flag_option  "
			SQL = SQL & "From EMI_Quality_Control a, EMI_Kategori_Komponen b where a.kode_perusahaan = b.kode_perusahaan and "
			SQL = SQL & "a.Id_Kategori_Komponen  = b.Id_Kategori_Komponen and "
			SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
			If semua = "T" Then
				SQL = SQL & "And " & arrcari.Item(Cmb_FilterValue.SelectedIndex) & " Like '%" & Txt_FilterValue.Text & "%' "
				SQL = SQL & "order by " & arrcari.Item(Cmb_FilterValue.SelectedIndex) & " "
			Else
				SQL = SQL & "order by a.Kode_Uji "
			End If
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Dim Lvw As ListViewItem
					Lvw = Lv_DataQC.Items.Add(dr("Kode_Uji"))
					Lvw.SubItems.Add(dr("Keterangan"))
					Lvw.SubItems.Add("")
					Lvw.SubItems.Add(dr("Satuan"))
					Lvw.SubItems.Add(dr("Ket_kategori"))
					If General_Class.CekNULL(dr("Flag_Tampil_Formula")) = "Y" Then
						Lvw.SubItems.Add(dr("Flag_Tampil_Formula"))
					Else
						Lvw.SubItems.Add("T")
					End If
					If General_Class.CekNULL(dr("Flag_Tampil_Bahan")) = "Y" Then
						Lvw.SubItems.Add(dr("Flag_Tampil_Bahan"))
					Else
						Lvw.SubItems.Add("T")
					End If
					Lvw.SubItems.Add(dr("Id_QC_Formula"))
					Lvw.SubItems.Add(dr("Id_Kategori_Komponen"))
					Lvw.SubItems.Add(dr("Flag_Option"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Try
			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			Label1.Text = Base_Language.Lang_Quality_Control_Judul
			Label2.Text = Base_Language.Lang_Quality_Control_Kode
			Label3.Text = Base_Language.Lang_Quality_Control_Keterangan
			Label4.Text = Base_Language.Lang_Quality_Control_Kolom
			Label6.Text = Base_Language.Lang_Global_Jenis
			Label11.Text = Base_Language.Lang_Quality_Control_Satuan

			Lv_DataQC.Columns.Add(Base_Language.Lang_Quality_Control_Kode, 150, HorizontalAlignment.Left)
			Lv_DataQC.Columns.Add(Base_Language.Lang_Quality_Control_Keterangan, 350, HorizontalAlignment.Left)
			Lv_DataQC.Columns.Add(Base_Language.Lang_Quality_Control_Target, 0, HorizontalAlignment.Left)
			Lv_DataQC.Columns.Add(Base_Language.Lang_Quality_Control_Satuan, 190, HorizontalAlignment.Left)
			Lv_DataQC.Columns.Add(Base_Language.Lang_Global_Jenis, 190, HorizontalAlignment.Left)
			Lv_DataQC.Columns.Add(Base_Language.Lang_Quality_Control_Tampil_Formula, 0, HorizontalAlignment.Left)
			Lv_DataQC.Columns.Add(Base_Language.Lang_Quality_Control_Tampil_Bahan, 0, HorizontalAlignment.Left)

			Lv_DataQC.Columns.Add("Id_qc", 0, HorizontalAlignment.Left)
			Lv_DataQC.Columns.Add("Id_Kom", 0, HorizontalAlignment.Left)
			Lv_DataQC.Columns.Add("flag", 0, HorizontalAlignment.Left)
			Lv_DataQC.Columns.Add("Flag Tampil Android", 0, HorizontalAlignment.Left)
			Lv_DataQC.Columns.Add("Flag Tampil Dekstop", 0, HorizontalAlignment.Left)
			Lv_DataQC.View = View.Details

			Lv_Switch.Columns.Add(Base_Language.Lang_Quality_Control_Keterangan & " Switch", 300, HorizontalAlignment.Left)
			Lv_Switch.Columns.Add("Id_qc", 0, HorizontalAlignment.Left)
			Lv_Switch.Columns.Add("urut", 0, HorizontalAlignment.Left)
			Lv_Switch.View = View.Details

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		kosong()
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Cmb_FilterValue.Text.Trim.Length = 0 Then Exit Sub
		If Txt_FilterValue.Text.Trim.Length = 0 Then Exit Sub

		Cari("T")
	End Sub

	Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kode.KeyPress
		If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
	End Sub

	Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
		If e.KeyChar = Chr(13) Then Cmb_Satuan.Focus()
	End Sub

	Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Satuan.KeyPress
		If e.KeyChar = Chr(13) Then Cmb_Jenis.Focus()
	End Sub

	Private Sub RadioButton4_KeyPress(sender As Object, e As KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
	End Sub

	Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_FilterValue.KeyPress
		If e.KeyChar = Chr(13) Then Txt_FilterValue.Focus()
	End Sub

	Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_FilterValue.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
	End Sub

	Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DataQC.DoubleClick
		Txt_Kode.Text = Lv_DataQC.FocusedItem.Text
		TextBox1_Leave(Lv_DataQC, e)
	End Sub

	Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles Txt_Kode.Leave
		If Txt_Kode.Text.Trim.Length = 0 Then Exit Sub
		Try
			OpenConn()

			SQL = "Select a.Id_QC_Formula,a.Kode_Perusahaan,a.Kode_Uji,a.Keterangan,a.Satuan,Target, a.flag_Tampil_Dekstop, a.flag_tampil_android,"
			SQL = SQL & "a.Id_Kategori_Komponen,b.Keterangan as Ket_kategori,b.Flag_Option,a.range_awal,a.range_akhir "
			SQL = SQL & "From EMI_Quality_Control a, EMI_Kategori_Komponen b where a.kode_perusahaan = b.kode_perusahaan and "
			SQL = SQL & "a.Id_Kategori_Komponen  = b.Id_Kategori_Komponen and "
			SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and kode_uji = '" & Txt_Kode.Text & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Txt_Keterangan.Text = Dr("Keterangan")
					Cmb_Satuan.Text = Dr("Satuan")
					id_qc = Dr("Id_QC_Formula")
					Txt_Kode.Enabled = False
					Cmb_Jenis.Text = Dr("Ket_kategori")

					If General_Class.CekNULL(Dr("range_awal")) = "" Then
						Txt_RangeAwal.Text = ""
					Else
						Txt_RangeAwal.Text = Dr("range_awal")
					End If

					If General_Class.CekNULL(Dr("range_akhir")) = "" Then
						Txt_RangeAkhir.Text = ""
					Else
						Txt_RangeAkhir.Text = Dr("range_akhir")
					End If

					'''If General_Class.CekNULL(Dr("Flag_Tampil_Formula")) = "Y" Then
					'''    RadioButton1.Checked = True
					'''    RadioButton2.Checked = False
					'''Else
					'''    RadioButton1.Checked = False
					'''    RadioButton2.Checked = True
					'''End If

					'''If General_Class.CekNULL(Dr("Flag_Tampil_Bahan")) = "Y" Then
					'''    RadioButton3.Checked = True
					'''    RadioButton4.Checked = False
					'''Else
					'''    RadioButton3.Checked = False
					'''    RadioButton4.Checked = True
					'''End If

					'If General_Class.CekNULL(Dr("flag_tampil_android")) = "Y" Then
					'    Rb_LapanganYa.Checked = True
					'    Rb_LapanganTdk.Checked = False
					'Else
					'    Rb_LapanganYa.Checked = False
					'    Rb_LapanganTdk.Checked = True
					'End If

					'If General_Class.CekNULL(Dr("flag_tampil_dekstop")) = "Y" Then
					'    Rb_LabYa.Checked = True
					'    Rb_LabTdk.Checked = False
					'Else
					'    Rb_LabYa.Checked = False
					'    Rb_LabTdk.Checked = True
					'End If

					If General_Class.CekNULL(Dr("flag_tampil_android")) = "Y" Then
						Tb_Lapangan.Checked = True
						Rb_Lab.Checked = False
					Else
						Tb_Lapangan.Checked = False
						Rb_Lab.Checked = True
					End If

					'For index = 0 To arrJenisQC.Count - 1
					'    If Dr("Jenis_Input") = arrJenisQC.Item(index) Then
					'        CmbJenis.SelectedIndex = index
					'    End If
					'Next

					Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
					Btn_Simpan.Tag = "&Update"
					Flg_option = Dr("Flag_Option")

					'''Get_Isi_LvDataQC(Lv_DataQC.FocusedItem.Index)
					If Dr("Flag_Option") = "Y" Then
						Dr.Close()

						Lv_Switch.Items.Clear()
						SQL = "select Id_Switch,Id_QC_Formula,Flag_Default,Keterangan from EMI_Switch where Kode_Perusahaan = '" & KodePerusahaan & "' "
						SQL = SQL & "and Id_QC_Formula = '" & id_qc & "' "
						Using ds = BindingTrans(SQL)
							With ds.Tables("MyTable")
								For i As Integer = 0 To .Rows.Count - 1
									Dim lv As New ListViewItem
									lv = Lv_Switch.Items.Add(.Rows(i).Item("Keterangan"))
									lv.SubItems.Add(.Rows(i).Item("Id_QC_Formula"))
									lv.SubItems.Add(.Rows(i).Item("Id_Switch"))
									If General_Class.CekNULL(.Rows(i).Item("Flag_Default")) <> "" Then
										Lv_Switch.Items(i).Checked = True
									Else
										Lv_Switch.Items(i).Checked = False
									End If
								Next
							End With
						End Using
					Else
						Lv_Switch.Items.Clear()
					End If
				Else
					'''RadioButton1.Checked = False
					'''RadioButton2.Checked = False
					'''RadioButton3.Checked = False
					'''RadioButton4.Checked = False

					Txt_Keterangan.Text = ""

					Cmb_Satuan.SelectedIndex = -1
					Cmb_Jenis.SelectedIndex = -1
					Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = False
					Btn_Simpan.Tag = "&Simpan"
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		kosong()
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Txt_Kode.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kode, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kode.Focus() : Exit Sub
		ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Keterangan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Keterangan.Focus() : Exit Sub
		ElseIf Cmb_Satuan.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Satuan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Satuan.Focus() : Exit Sub
		ElseIf Cmb_Jenis.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Jenis & " " & Base_Language.Lang_Global_Belum_Diisi & " . .  ! !", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Jenis.Focus() : Exit Sub
		End If

		If Cmb_Jenis.Text.Trim.ToUpper = "SWITCH" Then
			If Lv_Switch.Items.Count = 0 Then
				MessageBox.Show("Data" & " " & Base_Language.Lang_Global_Jenis & " " & Base_Language.Lang_Global_Belum_Diisi & " . .  ! !", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Lv_Switch.Focus() : Exit Sub
			End If
		ElseIf Cmb_Jenis.Text.Trim.ToUpper = "SLIDER" Then
			If Txt_RangeAwal.Text = "" Then
				MessageBox.Show("Range Awal" & " " & Base_Language.Lang_Global_Belum_Diisi & " . .  ! !", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_RangeAwal.Focus() : Exit Sub
			ElseIf Txt_RangeAkhir.Text = "" Then
				MessageBox.Show("Range Akhir" & " " & Base_Language.Lang_Global_Belum_Diisi & " . .  ! !", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_RangeAkhir.Focus() : Exit Sub
			End If
		End If

		Dim F_Dekstop As String = ""
		Dim F_Android As String = ""

		'If Rb_LapanganYa.Checked = True Then
		'    F_Android = "'Y'"
		'ElseIf Rb_LapanganTdk.Checked = True Then
		'    F_Android = "NULL"
		'End If

		'If Rb_LabYa.Checked = True Then
		'    F_Dekstop = "'Y'"
		'ElseIf Rb_LabTdk.Checked = True Then
		'    F_Dekstop = "NULL"
		'End If

		If Tb_Lapangan.Checked = True Then
			F_Android = "'Y'"
			F_Dekstop = "NULL"
		Else
			F_Android = "NULL"
			F_Dekstop = "'Y'"
		End If

		Dim Range_Awal As String = ""
		Dim Range_Akhir As String = ""

		If Txt_RangeAwal.Text = "" Then
			Range_Awal = "NULL"
		Else
			Range_Awal = Txt_RangeAwal.Text
		End If

		If Txt_RangeAkhir.Text = "" Then
			Range_Akhir = "NULL"
		Else
			Range_Akhir = Txt_RangeAkhir.Text
		End If

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If arrFlg_Option.Item(Cmb_Jenis.SelectedIndex) = "Y" Then
				If Lv_Switch.Items.Count = 0 Then
					MessageBox.Show("Opsi belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_Switch.Focus() : Exit Sub
				ElseIf Lv_Switch.Items.Count > 0 Then
					'Dim fl_d As String = "T"
					Dim b As Integer = 0
					For a As Integer = 0 To Lv_Switch.Items.Count - 1
						'Get_Isi_LV2(a)
						If Lv_Switch.Items(a).Checked = True Then
							b = b + 1
						End If
					Next
					If b > 1 Then
						MessageBox.Show("Flag Default tidak boleh lebih dari 1", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					ElseIf b < 1 Then
						MessageBox.Show("Flag Default belum dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End If
			End If

			If Btn_Simpan.Tag = "&Simpan" Then

				SQL = "Insert Into EMI_Quality_Control(Kode_Perusahaan,Kode_Uji,Keterangan,"
				SQL = SQL & "Satuan,Target,Id_Kategori_Komponen,flag_tampil_dekstop,flag_tampil_android,range_awal,range_akhir, Flag_Tampil_Formula, Flag_Tampil_Bahan) Values("
				SQL = SQL & "'" & KodePerusahaan & "','" & Txt_Kode.Text & "',"
				SQL = SQL & "'" & Txt_Keterangan.Text & "','" & Cmb_Satuan.Text & "',"
				SQL = SQL & "NULL,'" & arrJenisQC.Item(Cmb_Jenis.SelectedIndex) & "', "
				SQL = SQL & "" & F_Dekstop & ", " & F_Android & ", "
				SQL = SQL & "" & Range_Awal & ", " & Range_Akhir & ", 'Y', 'Y' )"
				ExecuteTrans(SQL)

				If arrFlg_Option.Item(Cmb_Jenis.SelectedIndex) = "Y" Then
					Dim x_no_Id_QC As Integer = 0
					SQL = "select IDENT_CURRENT('EMI_Quality_Control') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							x_no_Id_QC = Dr("urutan")
						End If
					End Using

					For a As Integer = 0 To Lv_Switch.Items.Count - 1
						Get_Isi_LV2(a)
						SQL = "insert into EMI_Switch(Kode_Perusahaan,Id_QC_Formula"
						If Lv_Switch.Items(a).Checked = True Then
							SQL = SQL & ",Flag_Default"
						Else
							SQL = SQL & ""
						End If
						SQL = SQL & ",Keterangan) values('" & KodePerusahaan & "','" & x_no_Id_QC & "'"
						If Lv_Switch.Items(a).Checked = True Then
							SQL = SQL & ",'Y'"
						Else
							SQL = SQL & ""
						End If
						SQL = SQL & ",'" & Lv2_Ket & "')"
						ExecuteTrans(SQL)
					Next

				End If
			Else
				SQL = "select Kode_Uji from EMI_Pembelian_QC_Bahan_Det where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Uji = '" & Txt_Kode.Text & "'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show(Base_Language.Lang_Quality_Control_Error1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = "select Kode_Uji from EMI_Transaksi_Formulator_QC_Det where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Uji = '" & Txt_Kode.Text & "'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show(Base_Language.Lang_Quality_Control_Error1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = "Select b.Keterangan as Ket_kategori,b.Flag_Option "
				SQL = SQL & "From EMI_Quality_Control a, EMI_Kategori_Komponen b where a.kode_perusahaan = b.kode_perusahaan and "
				SQL = SQL & "a.Id_Kategori_Komponen  = b.Id_Kategori_Komponen and "
				SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and kode_uji = '" & Txt_Kode.Text & "'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						If Dr("Flag_Option") = "Y" And arrFlg_Option.Item(Cmb_Jenis.SelectedIndex) <> "Y" Then
							Dr.Close()
							SQL = "delete from EMI_Switch where kode_perusahaan = '" & KodePerusahaan & "' and "
							SQL = SQL & "Id_QC_Formula = '" & id_qc & "'"
							ExecuteTrans(SQL)

							'For b As Integer = 0 To ListView2.Items.Count - 1
							'    Get_Isi_LV2(b)
							'    If Lv2_Id_Switch = "" Then

							'    End If
							'Next
						End If
					End If
				End Using

				SQL = "Update EMI_Quality_Control Set Keterangan = '" & Txt_Keterangan.Text & "',"
				SQL = SQL & "Satuan = '" & Cmb_Satuan.Text & "',"
				SQL = SQL & "Id_Kategori_Komponen = '" & arrJenisQC.Item(Cmb_Jenis.SelectedIndex) & "',"
				SQL = SQL & "Target = NULL,"
				SQL = SQL & "flag_tampil_dekstop = " & F_Dekstop & ", "
				SQL = SQL & "flag_tampil_android = " & F_Android & ", "
				SQL = SQL & "range_awal = " & Range_Awal & ", "
				SQL = SQL & "range_akhir = " & Range_Akhir & " "
				'''SQL = SQL & "Flag_Tampil_Formula = " & F_Formula & ","
				'''SQL = SQL & "Flag_Tampil_Bahan = " & F_Bahan & ","
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "Kode_Uji = '" & Txt_Kode.Text & "' "
				ExecuteTrans(SQL)

				If arrFlg_Option.Item(Cmb_Jenis.SelectedIndex) = "Y" Then
					For a As Integer = 0 To Lv_Switch.Items.Count - 1
						Get_Isi_LV2(a)
						If Lv2_Id_Switch = "" Then
							SQL = "insert into EMI_Switch(Kode_Perusahaan,Id_QC_Formula"
							If Lv_Switch.Items(a).Checked = True Then
								SQL = SQL & ",Flag_Default"
							Else
								SQL = SQL & ""
							End If
							SQL = SQL & ",Keterangan) values('" & KodePerusahaan & "','" & id_qc & "'"
							If Lv_Switch.Items(a).Checked = True Then
								SQL = SQL & ",'Y'"
							Else
								SQL = SQL & ""
							End If
							SQL = SQL & ",'" & Lv2_Ket & "')"
							ExecuteTrans(SQL)
						Else
							SQL = "update EMI_Switch set Keterangan = '" & Lv2_Ket & "' "
							If Lv_Switch.Items(a).Checked = True Then
								SQL = SQL & ",Flag_Default = 'Y' "
							Else
								SQL = SQL & ",Flag_Default = NULL "
							End If
							SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and Id_Switch = '" & Lv2_Id_Switch & "' "
							ExecuteTrans(SQL)
						End If

					Next
				End If
			End If

			Cmd.Transaction.Commit()
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		kosong()
	End Sub

	Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
		If Txt_Kode.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kode, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Kode.Focus() : Exit Sub
		End If
		Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If Hapus1 = vbYes Then
			Try
				OpenConn()
				Cmd.Transaction = Cn.BeginTransaction

				SQL = "select Kode_Uji from EMI_Pembelian_QC_Bahan_Det where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Uji = '" & Txt_Kode.Text & "'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show(Base_Language.Lang_Quality_Control_Error1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = "select Kode_Uji from EMI_Transaksi_Formulator_QC_Det where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Uji = '" & Txt_Kode.Text & "'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show(Base_Language.Lang_Quality_Control_Error1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = "Select b.Keterangan as Ket_kategori,b.Flag_Option "
				SQL = SQL & "From EMI_Quality_Control a, EMI_Kategori_Komponen b where a.kode_perusahaan = b.kode_perusahaan and "
				SQL = SQL & "a.Id_Kategori_Komponen  = b.Id_Kategori_Komponen and "
				SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and kode_uji = '" & Txt_Kode.Text & "'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						If Dr("Flag_Option") = "Y" Then
							Dr.Close()
							SQL = "delete from EMI_Switch where kode_perusahaan = '" & KodePerusahaan & "' and "
							SQL = SQL & "Id_QC_Formula = '" & id_qc & "'"
							ExecuteTrans(SQL)
						End If
					End If
				End Using

				SQL = "Delete From EMI_Quality_Control where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "Kode_Uji = '" & Txt_Kode.Text & "'"
				ExecuteTrans(SQL)

				Cmd.Transaction.Commit()
				CloseConn()
			Catch ex As Exception
				CloseConn()
				If ex.Message.Contains("REFERENCE constraint") Or ex.Message.Contains("FK_") Then
					MessageBox.Show("Data ini sedang digunakan dan tidak bisa dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Else
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End If
				Exit Sub
			End Try
		Else
			MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If
		kosong()
	End Sub

	Private Sub CmbJenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis.KeyPress
		If Cmb_Jenis.Text.Trim.ToUpper = "SWITCH" Then
			If e.KeyChar = Chr(13) Then Txt_Switch.Focus()
			'''Else
			'''    If e.KeyChar = Chr(13) Then RadioButton1.Focus()
		End If

	End Sub

	Private Sub ListView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Switch.SelectedIndexChanged

	End Sub

	Private Sub ListView2_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Switch.KeyDown
		If e.KeyCode = Keys.Delete Then
			Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If Hapus1 = vbYes Then
				Try
					For i As Integer = Lv_Switch.SelectedItems.Count - 1 To 0 Step -1
						Lv_Switch.Items.Remove(Lv_Switch.SelectedItems(i))
					Next
				Catch ex As Exception
					MessageBox.Show("Error saat menghapus: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End If
		End If
	End Sub

	Private Sub Txt_RangeAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_RangeAwal.KeyPress
		'If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
		'    e.Handled = True
		'End If

	End Sub

	Private Sub Txt_RangeAkhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_RangeAkhir.KeyPress
		'If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
		'    e.Handled = True
		'End If

	End Sub

	Private Sub Txt_RangeAwal_Leave(sender As Object, e As EventArgs) Handles Txt_RangeAwal.Leave
		If Not IsNumeric(Txt_RangeAwal.Text) Then
			Txt_RangeAwal.Text = ""
		End If
	End Sub

	Private Sub Txt_RangeAkhir_Leave(sender As Object, e As EventArgs) Handles Txt_RangeAkhir.Leave
		If Not IsNumeric(Txt_RangeAkhir.Text) Then
			Txt_RangeAkhir.Text = ""
		End If
	End Sub

	Private Sub CmbJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis.SelectedIndexChanged
		If Cmb_Jenis.Text.Trim.ToUpper = "SWITCH" Then
			Txt_Switch.Text = ""
			Txt_Switch.Visible = True
			Lv_Switch.Visible = True
			GrBox_Range.Visible = False
			Txt_RangeAwal.Text = ""
			Txt_RangeAkhir.Text = ""
		ElseIf Cmb_Jenis.Text.Trim.ToUpper = "SLIDER" Then
			Txt_Switch.Text = ""
			Lv_Switch.Items.Clear()
			Txt_Switch.Visible = False
			Lv_Switch.Visible = False
			GrBox_Range.Visible = True
		Else
			Txt_Switch.Text = ""
			Lv_Switch.Items.Clear()
			Txt_Switch.Visible = False
			Lv_Switch.Visible = False
			GrBox_Range.Visible = False
		End If
	End Sub

	Private Sub ListView2_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles Lv_Switch.ItemChecked
		'''If ListView2.Items.Count = 0 Then
		'''    Exit Sub
		'''ElseIf ListView2.Items.Count > 0 Then
		'''    'Dim fl_d As String = "T"
		'''    Dim b As Integer = 0
		'''    For a As Integer = 0 To ListView2.Items.Count - 1
		'''        'Get_Isi_LV2(a)
		'''        If ListView2.Items(a).Checked = True Then
		'''            b = b + 1
		'''        End If
		'''    Next
		'''    If b > 1 Then
		'''        MessageBox.Show("Flag Default tidak boleh lebih dari 1", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'''        Exit Sub
		'''    End If
		'''End If
		'''
		If Lv_Switch.Items.Count = 0 Then Exit Sub

		If e.Item.Checked Then
			For i As Integer = 0 To Lv_Switch.Items.Count - 1
				If Lv_Switch.Items(i) IsNot e.Item Then
					Lv_Switch.Items(i).Checked = False
				End If
			Next
		End If
	End Sub

	Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Switch.KeyPress
		If e.KeyChar = Chr(13) Then
			'If ListView2.Items.Count = 0 Then
			'    MessageBox.Show("Opsi belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TextBox4.Focus() : Exit Sub
			'End If
			For a As Integer = 0 To Lv_Switch.Items.Count - 1
				Get_Isi_LV2(a)
				If Lv2_Ket.ToUpper = Txt_Switch.Text.ToUpper Then
					MessageBox.Show("Opsi sudah ada pada list!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_Switch.Focus()
					Exit Sub
				ElseIf Txt_Switch.Text.Trim.Length = 0 Then
					MessageBox.Show("Opsi belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_Switch.Focus()
					Exit Sub
				End If
			Next
			Dim Lvw As ListViewItem
			Lvw = Lv_Switch.Items.Add(Txt_Switch.Text)
			Lvw.SubItems.Add("")
			Lvw.SubItems.Add("")

			Dim Tanya1 As String = MessageBox.Show("Mau Masukan opsi lain", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If Tanya1 = vbYes Then
				Txt_Switch.Text = ""
				Txt_Switch.Focus()
			Else
				If Lv_Switch.Items.Count = 0 Then
					MessageBox.Show("Opsi belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_Switch.Focus() : Exit Sub
				End If
				Txt_Switch.Text = ""
				'''RadioButton1.Focus()
			End If
		End If

	End Sub

End Class