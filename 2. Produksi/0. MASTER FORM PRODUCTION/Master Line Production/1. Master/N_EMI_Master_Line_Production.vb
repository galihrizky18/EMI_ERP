Public Class N_EMI_Master_Line_Production

	Dim SelectedIDLine As String

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim arrFilterDisplay, arrSelectedPackingSetLine As New ArrayList

	Dim Lv1_Packing_Set_ID, Lv1_Packing_Set_Paket, Lv1_Packing_Set_Tanggal, Lv1_Packing_Set_Total_Bahan As String

	Dim Item1_Packing_Set_Paket As Integer = 0
	Dim Item1_Packing_Set_Tanggal As Integer = 1
	Dim Item1_Packing_Set_Total_Bahan As Integer = 2
	Dim Item1_Packing_Set_ID As Integer = 3

	Dim Item2_Line_Production_ID As Integer = 0
	Dim Item2_Line_Production_Line As Integer = 1
	Dim Item2_Line_Production_Status As Integer = 2
	Dim Item2_Line_Production_Tanggal As Integer = 3
	Dim Item2_Line_Production_KdBarang As Integer = 4
	Dim Item2_Line_Production_NmBarang As Integer = 5
	Dim Item2_Line_Production_User As Integer = 6

	Dim Switch_Auto_Complete, UpdateFromDisplay As Boolean

	Private Sub N_EMI_Master_Line_Production_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBuffer(Lv1_Barang)
		EnableDoubleBuffer(Lv1_Packing_Set)
		EnableDoubleBuffer(Lv1_Packing_Set_Detail)
		EnableDoubleBuffer(Lv2_Display_Line_Production)
		EnableDoubleBuffer(Lv2_Detail_Packing_Set)

		Lv1_Packing_Set.Columns.Clear()
		Lv1_Packing_Set.Columns.Add("Paket", 250, HorizontalAlignment.Left)
		Lv1_Packing_Set.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
		Lv1_Packing_Set.Columns.Add("Total Bahan", 90, HorizontalAlignment.Center)
		Lv1_Packing_Set.Columns.Add("ID", 0, HorizontalAlignment.Left)
		Lv1_Packing_Set.View = View.Details

		Lv1_Packing_Set_Detail.Columns.Clear()
		Lv1_Packing_Set_Detail.Columns.Add("Kode Bahan", 110, HorizontalAlignment.Left)
		Lv1_Packing_Set_Detail.Columns.Add("Nama Bahan", 230, HorizontalAlignment.Left)
		Lv1_Packing_Set_Detail.Columns.Add("Jenis", 130, HorizontalAlignment.Center)
		Lv1_Packing_Set_Detail.Columns.Add("Jumlah Barang", 110, HorizontalAlignment.Right)
		Lv1_Packing_Set_Detail.Columns.Add("Jumlah Bahan", 110, HorizontalAlignment.Right)
		Lv1_Packing_Set_Detail.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv1_Packing_Set_Detail.View = View.Details

		Lv1_Barang.Columns.Clear()
		Lv1_Barang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
		Lv1_Barang.Columns.Add("Nama Barang", 260, HorizontalAlignment.Left)
		Lv1_Barang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv1_Barang.View = View.Details

		Lv1_Barang.Location = New Point(162, 112)
		Lv1_Barang.Visible = False

		Lv2_Display_Line_Production.Columns.Clear()
		Lv2_Display_Line_Production.Columns.Add("ID", 0, HorizontalAlignment.Left)
		Lv2_Display_Line_Production.Columns.Add("Line", 190, HorizontalAlignment.Left)
		Lv2_Display_Line_Production.Columns.Add("Status", 100, HorizontalAlignment.Center)
		Lv2_Display_Line_Production.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
		Lv2_Display_Line_Production.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
		Lv2_Display_Line_Production.Columns.Add("Nama Barang", 370, HorizontalAlignment.Left)
		Lv2_Display_Line_Production.Columns.Add("User Create", 120, HorizontalAlignment.Left)
		Lv2_Display_Line_Production.View = View.Details

		Lv2_Detail_Packing_Set.Columns.Clear()
		Lv2_Detail_Packing_Set.Columns.Add("ID", 0, HorizontalAlignment.Left)
		Lv2_Detail_Packing_Set.Columns.Add("Paket", 140, HorizontalAlignment.Left)
		Lv2_Detail_Packing_Set.Columns.Add("Kode Bahan", 130, HorizontalAlignment.Left)
		Lv2_Detail_Packing_Set.Columns.Add("Nama Bahan", 280, HorizontalAlignment.Left)
		Lv2_Detail_Packing_Set.Columns.Add("Jenis", 140, HorizontalAlignment.Center)
		Lv2_Detail_Packing_Set.Columns.Add("Jumlah Barang", 110, HorizontalAlignment.Right)
		Lv2_Detail_Packing_Set.Columns.Add("Jumlah Bahan", 110, HorizontalAlignment.Right)
		Lv2_Detail_Packing_Set.Columns.Add("Satuan", 80, HorizontalAlignment.Center)

		Cmb2_Filter.Items.Clear() : arrFilterDisplay.Clear()
		Cmb2_Filter.Items.Add(OpsiSeluruh) : arrFilterDisplay.Add(OpsiSeluruh)
		Cmb2_Filter.Items.Add("Line") : arrFilterDisplay.Add("a.Keterangan")
		Cmb2_Filter.Items.Add("Kode Barang") : arrFilterDisplay.Add("a.Kode_Barang")
		Cmb2_Filter.Items.Add("Nama Barang") : arrFilterDisplay.Add("c.Nama")
		Cmb2_Filter.Items.Add("User Create") : arrFilterDisplay.Add("a.UserID")

		UpdateFromDisplay = False

		TabControl1.SelectedIndex = 0
		TabControl1_SelectedIndexChanged(sender, e)

	End Sub

	Private Sub Kosong_Tab_1()

		SelectedIDLine = ""
		arrSelectedPackingSetLine.Clear()

		Switch_Auto_Complete = True
		Txt1_KdBarang.Text = ""
		Txt1_NmBarang.Text = ""
		Switch_Auto_Complete = False

		Txt1_Keterangan.Enabled = False
		Txt1_Keterangan.BackColor = Color.FromArgb(235, 235, 235)
		Txt1_Keterangan.Text = ""

		Txt1_KdBarang.Enabled = True
		Txt1_NmBarang.Enabled = True

		Lv1_Packing_Set.Items.Clear()
		Lv1_Packing_Set_Detail.Items.Clear()

		Btn1_Simpan.Tag = "SIMPAN"
		Btn1_Simpan.Text = "&Simpan"

		Txt1_KdBarang.Focus()

	End Sub

	Private Sub Get_Data_Lv_Packing_Set(ByVal index As Integer)
		Lv1_Packing_Set_ID = Lv1_Packing_Set.Items(index).SubItems(Item1_Packing_Set_ID).Text
		Lv1_Packing_Set_Paket = Lv1_Packing_Set.Items(index).SubItems(Item1_Packing_Set_Paket).Text
		Lv1_Packing_Set_Tanggal = Lv1_Packing_Set.Items(index).SubItems(Item1_Packing_Set_Tanggal).Text
		Lv1_Packing_Set_Total_Bahan = Lv1_Packing_Set.Items(index).SubItems(Item1_Packing_Set_Total_Bahan).Text
	End Sub

	Private Sub Kosong_Tab_2()

		Cmb2_Filter.SelectedIndex = 0

		Load_Display()
	End Sub

	Private Sub Load_Display()

		Try
			OpenConn()

			Dim Filter As String = ""
			If Cmb2_Filter.SelectedIndex >= 1 Then
				Filter &= $"and {arrFilterDisplay(Cmb2_Filter.SelectedIndex)} like '%{Txt2_Filter.Text.Trim}%' "
			End If

			Lv2_Display_Line_Production.BeginUpdate()
			Lv2_Display_Line_Production.Items.Clear() : Lv2_Detail_Packing_Set.Items.Clear()
			SQL = $"
				select a.ID_Line, a.status, a.Tanggal, a.Jam, a.Kode_Barang, c.Nama, a.Keterangan, a.UserID
				from N_EMI_Master_Line_Production a
					inner join (
						select z.Kode_Perusahaan, z.Kode_Barang, z.nama, z.Satuan
						from barang z
						group by z.Kode_Perusahaan, z.Kode_Barang, z.nama, z.Satuan
					) c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Barang = c.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				order by a.Tanggal, a.Jam
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv2_Display_Line_Production.Items.Add(Dr("ID_Line").ToString())
					Lv.SubItems.Add(Dr("Keterangan").ToString())
					If General_Class.CekNULL(Dr("status")) = "Y" Then
						Lv.SubItems.Add("Non Aktif")
						Lv.BackColor = Color.Tan
					Else
						Lv.SubItems.Add("Aktif")
					End If
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Kode_Barang").ToString())
					Lv.SubItems.Add(Dr("Nama").ToString())
					Lv.SubItems.Add(Dr("UserID").ToString())

				Loop
			End Using
			Lv2_Display_Line_Production.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
		If TabControl1.SelectedIndex = 0 Then
			If Not UpdateFromDisplay Then
				Kosong_Tab_1()
			End If
		ElseIf TabControl1.SelectedIndex = 1 Then
			Kosong_Tab_2()
		End If

	End Sub

	Private Sub Btn1_Get_Packing_Set_Click(sender As Object, e As EventArgs) Handles Btn1_Get_Packing_Set.Click
		If Txt1_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Harap Pilih Dahulu Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Txt1_KdBarang.Focus()
			Exit Sub
		End If

		Try
			OpenConn()

			Dim KdBarangInq As String = ""
			'===============================
			'=     GET KODE BARANG INQ     =
			'===============================
			SQL = $"
				select Kode_Barang, Kode_Barang_Inq
				from Barang
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Barang = '{Txt1_KdBarang.Text.Trim}'
				group by Kode_Barang, Kode_Barang_Inq
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					KdBarangInq = Dr("Kode_Barang_Inq")
				End If
			End Using

			'=====================================
			'=     GET PACKING SET BY BARANG     =
			'=====================================
			Lv1_Packing_Set.Items.Clear() : Lv1_Packing_Set_Detail.Items.Clear()
			SQL = $"
				select a.Urut_Oto, a.Deskripsi, a.Tanggal, count(distinct b.Kode_Bahan) as JumlahBahan
				from N_EMI_Detail_Barang_Detail_Bahan_Penolong a
					left join N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Oto = b.Id_Parent
				where a.Status is null
				and a.Flag_Aktif = 'Y'
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and (a.Kode_Barang = '{Txt1_KdBarang.Text.Trim}' or a.Kode_Barang = '{KdBarangInq.Trim}')
				group by a.Urut_Oto, a.Deskripsi, a.Tanggal
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					Do
						Dim Lv As ListViewItem
						Lv = Lv1_Packing_Set.Items.Add(Dr("Deskripsi"))
						Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
						Lv.SubItems.Add(Dr("JumlahBahan"))
						Lv.SubItems.Add(Dr("Urut_Oto"))

						If arrSelectedPackingSetLine.Contains(Dr("Urut_Oto").ToString.Trim) Then
							Lv.Checked = True
						Else
							Lv.Checked = False
						End If

					Loop While Dr.Read
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show($"Kode Barang {Txt1_KdBarang.Text.Trim} Belum Memiliki Packing Set", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Lv1_Packing_Set_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv1_Packing_Set.SelectedIndexChanged
		If Lv1_Packing_Set.Items.Count = 0 Or Lv1_Packing_Set.FocusedItem.Index = -1 Then Exit Sub

		Try
			OpenConn()

			Dim ID_Packing_Set As String = Lv1_Packing_Set.FocusedItem.SubItems(Item1_Packing_Set_ID).Text
			SQL = $"
				select a.Kode_Bahan, b.nama, a.jenis, a.Jumlah_Barang, a.Jumlah_Bahan, b.Satuan
				from N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail a
					inner join (
						select z.Kode_Perusahaan, z.Kode_Barang, z.Nama, z.Satuan
						from barang z
						group by z.Kode_Perusahaan, z.Kode_Barang, z.Nama, z.Satuan
					) b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Bahan = b.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Id_Parent = '{ID_Packing_Set}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					Lv1_Packing_Set_Detail.Items.Clear()
					Lv1_Packing_Set_Detail.BeginUpdate()

					Do
						Dim Lv As ListViewItem
						Lv = Lv1_Packing_Set_Detail.Items.Add(Dr("Kode_Bahan").ToString())
						Lv.SubItems.Add(Dr("nama").ToString())
						Lv.SubItems.Add(Dr("jenis").ToString())
						Lv.SubItems.Add(Format(Dr("Jumlah_Barang"), "N4"))
						Lv.SubItems.Add(Format(Dr("Jumlah_Bahan"), "N4"))
						Lv.SubItems.Add(Dr("Satuan").ToString())
					Loop While Dr.Read()

					Lv1_Packing_Set_Detail.EndUpdate()
				Else
					Lv1_Packing_Set_Detail.Items.Clear()
				End If

			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn1_Refresh_Click(sender As Object, e As EventArgs) Handles Btn1_Refresh.Click
		UpdateFromDisplay = False
		Kosong_Tab_1()
	End Sub

	Private Sub Btn1_Simpan_Click(sender As Object, e As EventArgs) Handles Btn1_Simpan.Click
		If Txt1_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Harap Pilih Barang Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Txt1_KdBarang.Focus()
			Exit Sub
		ElseIf Txt1_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Txt1_Keterangan.Focus()
			Exit Sub
		End If

		If Lv1_Packing_Set.CheckedItems.Count = 0 Then
			MessageBox.Show("Minimal Pilih 1 Packing Set", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Lv1_Packing_Set.Focus()
			Exit Sub
		End If

		Dim Action As String = ""
		If Btn1_Simpan.Tag = "SIMPAN" Then
			Action = "Simpan"
		ElseIf Btn1_Simpan.Tag = "UPDATE" Then
			Action = "Update"
		End If

		If MessageBox.Show($"Yakin Ingin Melakukan {Action} Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If Btn1_Simpan.Tag.ToString.ToUpper.Trim = "SIMPAN" Then

				'=========================
				'=     INSERT PARENT     =
				'=========================
				SQL = $"
					insert into N_EMI_Master_Line_Production
						(Kode_Perusahaan, Tanggal, Jam, Kode_Barang, Keterangan, UserID)
					values
						('{KodePerusahaan}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}',
						'{Txt1_KdBarang.Text.Trim}', '{Txt1_Keterangan.Text.Trim}', '{UserID}')
				"
				ExecuteTrans(SQL)

				Dim ident_current As Integer = 0
				SQL = "select IDENT_CURRENT('N_EMI_Master_Line_Production') as urutan "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						ident_current = Dr("urutan")
					End If
				End Using

				'=========================
				'=     INSERT DETAIL     =
				'=========================
				For i As Integer = 0 To Lv1_Packing_Set.Items.Count - 1
					If Not Lv1_Packing_Set.Items(i).Checked Then Continue For

					Get_Data_Lv_Packing_Set(i)

					SQL = $"
						insert into N_EMI_Master_Line_Production_Detail_Packaging
							(Kode_Perusahaan, ID_Line, ID_Packing_Set, Kode_Barang, Deskripsi)
						values
							('{KodePerusahaan}', '{ident_current}', '{Lv1_Packing_Set_ID}', '{Txt1_KdBarang.Text.Trim}', '{Lv1_Packing_Set_Paket}')
					"
					ExecuteTrans(SQL)

				Next

				Action = "Disimpan"

			ElseIf Btn1_Simpan.Tag.ToString.ToUpper.Trim = "UPDATE" Then

				If SelectedIDLine.Trim.Length = 0 Then
					MessageBox.Show("Data Tidak Ditemukan . . ! !. Harap Pilih Dahulu Line Yang Ingin Diupdate", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				'====================================
				'=     CEK APAKAH DATA LINE ADA     =
				'====================================
				SQL = $"
					select status
					from N_EMI_Master_Line_Production
					where Kode_Perusahaan = '{KodePerusahaan}'
					and ID_Line = '{SelectedIDLine.Trim}'
				"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then

							If General_Class.CekNULL(.Rows(0).Item("status")) = "Y" Then
								CloseConn()
								MessageBox.Show($"Data Tidak Dapat Diupdate Karena Line Sudah Dibatalkan / Tidak Aktif", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							'=========================
							'=     UPDATE PARENT     =
							'=========================
							SQL = $"
								update N_EMI_Master_Line_Production
								set Keterangan = '{Txt1_Keterangan.Text.Trim}'
								where Kode_Perusahaan = '{KodePerusahaan}'
								and ID_Line = '{SelectedIDLine.Trim}'
							"
							ExecuteTrans(SQL)

							'=========================
							'=     DELETE DETAIL     =
							'=========================
							SQL = $"
								delete from N_EMI_Master_Line_Production_Detail_Packaging
								where Kode_Perusahaan = '{KodePerusahaan}'
								and ID_Line = '{SelectedIDLine.Trim}'
							"
							ExecuteTrans(SQL)
							'=========================
							'=     INSERT DETAIL     =
							'=========================
							For i As Integer = 0 To Lv1_Packing_Set.Items.Count - 1
								If Not Lv1_Packing_Set.Items(i).Checked Then Continue For
								Get_Data_Lv_Packing_Set(i)
								SQL = $"
									insert into N_EMI_Master_Line_Production_Detail_Packaging
										(Kode_Perusahaan, ID_Line, ID_Packing_Set, Kode_Barang, Deskripsi)
									values
										('{KodePerusahaan}', '{SelectedIDLine.Trim}', '{Lv1_Packing_Set_ID}', '{Txt1_KdBarang.Text.Trim}', '{Lv1_Packing_Set_Paket}')
								"
								ExecuteTrans(SQL)
							Next
						Else
							CloseConn()
							MessageBox.Show("Data Line Production Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End With
				End Using

				Action = "Diupdate"
			End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Data Berhasil {Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Btn1_Refresh.PerformClick()

	End Sub

	Private Sub Btn1_Hapus_Click(sender As Object, e As EventArgs) Handles Btn1_Hapus.Click
		If SelectedIDLine.Trim.Length = 0 Then
			MessageBox.Show("Harap Pilih Dahulu Line yang Ingin Dihapus Melalui Tab Display", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt1_KdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show("Terdapat Data yang Tidak Lengkap, Kode Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt1_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Terdapat Data yang Tidak Lengkap, Keterangan Line Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show($"Yakin Ingin Mengahpus Line Production Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Dim IDSelecedLine As String = SelectedIDLine.Trim

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'=========================
			'=     CEK DATA LINE     =
			'=========================
			SQL = $"
				select Status
				from N_EMI_Master_Line_Production
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ID_Line = '{IDSelecedLine}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Line Production Sudah Dibatalkan Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Line Production Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'==============================================================
			'=     CEK APAKAH DATA LINE SUDAH DI PAKAI PADA STEP LAIN     =
			'==============================================================
			' CEK STEP GR 1
			SQL = $"
				select top 1 1
				from Emi_Produksi_Hasil_Perpallet
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ID_Line = '{IDSelecedLine}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Line Production Tidak Dapat Dihapus, Karent Line Productoin Sudah Digunakan Pada GR 1", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=========================
			'=     BACKUP KE LOG     =
			'=========================
			SQL = $"
				insert into N_EMI_Master_Line_Production_Log
					(Kode_Perusahaan, ID_Line, Status, Tanggal, Jam, Kode_Barang, Keterangan, UserID, Tanggal_Hapus,
					 Jam_Hapus, User_Hapus)
				select kode_perusahaan, id_line, status, tanggal, jam, kode_barang, keterangan, userid,
    					'{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{UserID}'
				from N_EMI_Master_Line_Production
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ID_Line = '{IDSelecedLine}'
			"
			ExecuteTrans(SQL)

			SQL = $"
				insert into N_EMI_Master_Line_Production_Detail_Packaging_Log
					(Kode_Perusahaan, ID_Line, ID_Packing_Set, Kode_Barang, Deskripsi, Tanggal_Hapus, Jam_Hapus, User_Hapus)
				select kode_perusahaan, id_line, id_packing_set, kode_barang, deskripsi,
					   '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{UserID}'
				from N_EMI_Master_Line_Production_Detail_Packaging
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ID_Line = '{IDSelecedLine}'
			"
			ExecuteTrans(SQL)

			'======================
			'=     HAPUS DATA     =
			'======================
			' HAPUS DETAIL
			SQL = $"
				delete from N_EMI_Master_Line_Production_Detail_Packaging
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ID_Line = '{IDSelecedLine}'
			"
			ExecuteTrans(SQL)

			'HAPUS PARENG
			SQL = $"
				delete from N_EMI_Master_Line_Production
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ID_Line = '{IDSelecedLine}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Line {Txt1_Keterangan.Text.Trim} Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Btn1_Refresh.PerformClick()

	End Sub

	Private Sub Lv2_Display_Line_Production_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv2_Display_Line_Production.SelectedIndexChanged
		If Lv2_Display_Line_Production.Items.Count = 0 Or Lv2_Display_Line_Production.FocusedItem.Index = -1 Then Exit Sub

		Try
			OpenConn()

			Dim IDLine As String = Lv2_Display_Line_Production.FocusedItem.SubItems(Item2_Line_Production_ID).Text

			Lv2_Detail_Packing_Set.BeginUpdate()
			Lv2_Detail_Packing_Set.Items.Clear()
			SQL = $"
				select c.Urut_Oto, c.Deskripsi, d.Kode_Bahan, e.Nama, d.Jenis, d.Jumlah_Barang, d.Jumlah_Bahan, e.Satuan
				from N_EMI_Master_Line_Production a
					inner join N_EMI_Master_Line_Production_Detail_Packaging b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_Line = b.ID_Line
					inner join N_EMI_Detail_Barang_Detail_Bahan_Penolong c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.ID_Packing_Set = c.Urut_Oto
					inner join N_EMI_Detail_Barang_Detail_Bahan_Penolong_Detail d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Urut_Oto = d.Id_Parent
					inner join (
						select z.Kode_Perusahaan, z.Kode_Barang, z.nama, z.Satuan
						from barang z
						group by z.Kode_Perusahaan, z.Kode_Barang, z.nama, z.Satuan
					) e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.Kode_Bahan = e.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.ID_Line = '{IDLine}'
				order by c.Urut_Oto, d.Jenis, d.Kode_Bahan
			"
			Using Dr = OpenTrans(SQL)
				Dim lastPaket As String = ""
				Dim useAlternativeColor As Boolean = False
				Dim color1 As Color = Color.White
				Dim color2 As Color = Color.FromArgb(240, 244, 248)

				Do While Dr.Read

					Dim currentPaket As String = Dr("Urut_Oto").ToString()

					If currentPaket <> lastPaket Then
						If lastPaket <> "" Then
							useAlternativeColor = Not useAlternativeColor
						End If
						lastPaket = currentPaket
					End If

					Dim Lv As ListViewItem
					Lv = Lv2_Detail_Packing_Set.Items.Add(Dr("Urut_Oto"))
					Lv.SubItems.Add(Dr("Deskripsi").ToString())
					Lv.SubItems.Add(Dr("Kode_Bahan").ToString())
					Lv.SubItems.Add(Dr("Nama").ToString())
					Lv.SubItems.Add(Dr("Jenis").ToString())
					Lv.SubItems.Add(Format(Dr("Jumlah_Barang"), "N4"))
					Lv.SubItems.Add(Format(Dr("Jumlah_Bahan"), "N4"))
					Lv.SubItems.Add(Dr("Satuan").ToString())

					If useAlternativeColor Then
						Lv.BackColor = color1
					Else
						Lv.BackColor = color2
					End If

					Lv.UseItemStyleForSubItems = True

				Loop
			End Using
			Lv2_Detail_Packing_Set.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn2_Cari_Click(sender As Object, e As EventArgs) Handles Btn2_Cari.Click
		If Cmb2_Filter.Items.Count = 0 Then Exit Sub

		If Cmb2_Filter.SelectedIndex = -1 Then
			MessageBox.Show("Harap Pilih Dahulu Data yang Ingin Difilter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Cmb2_Filter.DroppedDown = True
			Cmb2_Filter.Focus()
			Exit Sub
		End If

		Load_Display()
	End Sub

	Private Sub Lv2_Display_Line_Production_DoubleClick(sender As Object, e As EventArgs) Handles Lv2_Display_Line_Production.DoubleClick
		If Lv2_Display_Line_Production.Items.Count = 0 Or Lv2_Display_Line_Production.FocusedItem Is Nothing Then Exit Sub

		Dim SelectedId As String = Lv2_Display_Line_Production.FocusedItem.SubItems(Item2_Line_Production_ID).Text
		Dim HasData As Boolean = False

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'====================================
			'=     GET DATA LINE PRODUCTION     =
			'====================================
			SQL = $"
				select a.ID_Line, a.Kode_Barang, b.Nama, a.Keterangan, a.status
				from N_EMI_Master_Line_Production a
					inner join (
						select z.Kode_Perusahaan, z.Kode_Barang, z.nama, z.Satuan
						from barang z
						group by z.Kode_Perusahaan, z.Kode_Barang, z.nama, z.Satuan
					) b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.ID_Line = '{SelectedId}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Line Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					SelectedIDLine = SelectedId.Trim

					HasData = True

					Switch_Auto_Complete = True
					Txt1_KdBarang.Text = Dr("Kode_Barang")
					Txt1_NmBarang.Text = Dr("Nama")
					Txt1_Keterangan.Text = Dr("Keterangan")
					Switch_Auto_Complete = False

					Txt1_KdBarang.Enabled = False
					Txt1_NmBarang.Enabled = False
					Txt1_Keterangan.Enabled = True
					Txt1_Keterangan.BackColor = Color.White

					Btn1_Simpan.Tag = "UPDATE"
					Btn1_Simpan.Text = "&Update"
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Data Line Production Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'================================
			'=     GET DATA PACKING SET     =
			'================================
			arrSelectedPackingSetLine.Clear()
			SQL = $"
				select distinct  ID_Packing_Set
				from N_EMI_Master_Line_Production_Detail_Packaging
				where Kode_Perusahaan = '{KodePerusahaan}'
				and ID_Line = '{SelectedId}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					arrSelectedPackingSetLine.Add(Dr("ID_Packing_Set").ToString.Trim)
				Loop
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

		If HasData Then
			UpdateFromDisplay = True
		Else
			UpdateFromDisplay = False
		End If

		TabControl1.SelectedIndex = 0
		Btn1_Get_Packing_Set_Click(sender, e)
		Txt1_Keterangan.Focus()

	End Sub

	'================================================================================================================================================
	'=     HANDLE KEPRESS
	'================================================================================================================================================

#Region "HANDLE KEYPRESS TAB 1"

	Private Sub Txt1_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt1_KdBarang.TextChanged
		If Switch_Auto_Complete Then Exit Sub

		If Txt1_KdBarang.Text.Trim.Length = 0 Then
			Lv1_Barang.Visible = False
			Txt1_KdBarang.Text = ""
			Txt1_NmBarang.Text = ""
			Exit Sub
		Else
			Lv1_Barang.Visible = True
		End If

		Try
			OpenConn()

			Lv1_Barang.BeginUpdate()
			Lv1_Barang.Items.Clear()
			SQL = $"
				select c.Lokasi_Gudang, a.Kode_Barang, a.Nama, a.Satuan
				from barang a
				inner join EMI_Group_Jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis
				inner join EMI_Kategori_Gudang_PerLokasi c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang
				where (b.Flag_Finished_Good = 'Y' or b.Flag_Semi_FG = 'Y')
				and a.aktif = 'Y'
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Kode_Barang like '%{Txt1_KdBarang.Text.Trim}%'
				group by c.Lokasi_Gudang, a.Kode_Barang, a.Nama, a.Satuan
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv1_Barang.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
					Lv.SubItems.Add(Dr("Satuan"))
				Loop
			End Using
			Lv1_Barang.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt1_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt1_KdBarang.Leave
		If Txt1_KdBarang.Text.Trim.Length = 0 Then Exit Sub
		If Lv1_Barang.Focused = True Then Exit Sub

		Try
			OpenConn()

			SQL = $"
					select c.Lokasi_Gudang, a.Kode_Barang, a.Nama, a.Satuan
					from barang a
					inner join EMI_Group_Jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis
					inner join EMI_Kategori_Gudang_PerLokasi c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang
					where (b.Flag_Finished_Good = 'Y' or b.Flag_Semi_FG = 'Y')
					and a.aktif = 'Y'
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.Kode_Barang = '{Txt1_KdBarang.Text.Trim}'
					group by c.Lokasi_Gudang, a.Kode_Barang, a.Nama, a.Satuan
				"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then

						''===============================================================
						''=     CEK APAKAH KODE BARANG SUDAH ADA DI LINE PRODUCTION     =
						''===============================================================
						'SQL = $"
						'	select ID_Line, Keterangan
						'	from N_EMI_Master_Line_Production
						'	where Status is null
						'	and Kode_Perusahaan = '{KodePerusahaan}'
						'	and Kode_Barang = '{Txt1_KdBarang.Text.Trim}'
						'"
						'Using Dr = OpenTrans(SQL)
						'	If Dr.Read Then
						'		HasData = True
						'		SelectedIDLine = Dr("ID_Line")
						'		Txt1_Keterangan.Text = Dr("Keterangan").ToString.Trim
						'		Txt1_Keterangan.Enabled = False
						'		Txt1_Keterangan.BackColor = Color.FromArgb(235, 235, 235)
						'	Else
						'		HasData = False
						'		SelectedIDLine = ""
						'		Txt1_Keterangan.Text = ""
						'		Txt1_Keterangan.Enabled = True
						'		Txt1_Keterangan.BackColor = Color.White
						'	End If
						'End Using

						Txt1_KdBarang.Text = .Rows(0).Item("Kode_Barang")
						Txt1_NmBarang.Text = .Rows(0).Item("Nama")

						Txt1_Keterangan.Text = ""
						Txt1_Keterangan.Enabled = True
						Txt1_Keterangan.BackColor = Color.White

						Txt1_Keterangan.Focus()
					Else
						MessageBox.Show("Barang Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt1_KdBarang.Text = ""
						Txt1_NmBarang.Text = ""
						Txt1_Keterangan.Text = ""
						Txt1_Keterangan.Enabled = False
						Txt1_Keterangan.BackColor = Color.FromArgb(235, 235, 235)
						Txt1_KdBarang.Focus()
					End If
				End With
			End Using

			Lv1_Barang.Visible = False

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt1_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt1_NmBarang.TextChanged
		If Switch_Auto_Complete Then Exit Sub

		If Txt1_NmBarang.Text.Trim.Length = 0 Then
			Lv1_Barang.Visible = False
			Txt1_KdBarang.Text = ""
			Txt1_NmBarang.Text = ""
			Exit Sub
		Else
			Lv1_Barang.Visible = True
		End If

		Try
			OpenConn()

			Lv1_Barang.BeginUpdate()
			Lv1_Barang.Items.Clear()
			SQL = $"
				select c.Lokasi_Gudang, a.Kode_Barang, a.Nama, a.Satuan
				from barang a
				inner join EMI_Group_Jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis
				inner join EMI_Kategori_Gudang_PerLokasi c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang
				where (b.Flag_Finished_Good = 'Y' or b.Flag_Semi_FG = 'Y')
				and a.aktif = 'Y'
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Nama like '%{Txt1_NmBarang.Text.Trim}%'
				group by c.Lokasi_Gudang, a.Kode_Barang, a.Nama, a.Satuan
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv1_Barang.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
					Lv.SubItems.Add(Dr("Satuan"))
				Loop
			End Using
			Lv1_Barang.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt1_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt1_KdBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt1_KdBarang.Text.Trim.Length = 0 Then Txt1_KdBarang.Focus()
			Txt1_KdBarang_Leave(Txt1_KdBarang, e)

			Lv1_Barang.Visible = False

		End If
	End Sub

	Private Sub Txt1_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt1_KdBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv1_Barang.Focus()
	End Sub

	Private Sub Txt1_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt1_NmBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt1_NmBarang.Text.Trim.Length = 0 Then Txt1_NmBarang.Focus()
			Txt1_KdBarang_Leave(Txt1_NmBarang, e)

			Lv1_Barang.Visible = False

		End If
	End Sub

	Private Sub Txt1_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt1_NmBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv1_Barang.Focus()
	End Sub

	Private Sub Lv1_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv1_Barang.DoubleClick
		If Lv1_Barang.Items.Count = 0 Or Lv1_Barang.FocusedItem.Index = -1 Then Exit Sub

		Dim NoFaktur As String = Lv1_Barang.FocusedItem.SubItems(0).Text
		Dim Ket As String = Lv1_Barang.FocusedItem.SubItems(1).Text

		Switch_Auto_Complete = True
		Txt1_KdBarang.Text = NoFaktur
		Txt1_NmBarang.Text = Ket
		Switch_Auto_Complete = False

		Lv1_Barang.Visible = False

		If Not String.IsNullOrWhiteSpace(NoFaktur) Then
			Txt1_KdBarang_Leave(sender, e)
		End If

	End Sub

	Private Sub Lv1_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv1_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv1_Barang_DoubleClick(Lv1_Barang, e)
		End If
	End Sub

	Private Sub Txt1_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt1_Keterangan.KeyPress
		If e.KeyChar = Chr(13) Then Btn1_Get_Packing_Set.Focus()
	End Sub

	Private Sub Txt2_Filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt2_Filter.KeyPress
		If e.KeyChar = Chr(13) Then Btn2_Cari.Focus()
	End Sub

	Private Sub Cmb2_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb2_Filter.SelectedIndexChanged
		If Cmb2_Filter.Items.Count = 0 Then Exit Sub

		If Cmb2_Filter.SelectedIndex = 0 Then
			Txt2_Filter.Enabled = False
			Txt2_Filter.BackColor = Color.FromArgb(235, 235, 235)
			Lv2_Detail_Packing_Set.Focus()
		ElseIf Cmb2_Filter.SelectedIndex >= 1 Then
			Txt2_Filter.Enabled = True
			Txt2_Filter.BackColor = Color.White
			Txt2_Filter.Focus()
		End If

		Txt2_Filter.Text = ""

	End Sub

#End Region

	'================================================================================================================================================
	'=     UTILITY
	'================================================================================================================================================

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

	Private Sub Lv1_Packing_Set_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv1_Packing_Set.MouseMove, Lv1_Packing_Set_Detail.MouseMove,
		Lv2_Display_Line_Production.MouseMove, Lv2_Detail_Packing_Set.MouseMove, Lv1_Barang.MouseMove

		HandleListViewHover(sender, e)
	End Sub

End Class