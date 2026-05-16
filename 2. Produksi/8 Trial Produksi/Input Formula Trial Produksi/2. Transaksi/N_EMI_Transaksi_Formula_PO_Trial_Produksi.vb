Public Class N_EMI_Transaksi_Formula_PO_Trial_Produksi

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Private lastIndex As Integer = -1
	Private originalColor As Color

	Dim FormulaChecked As String = ""
	Private isProcessing_Check As Boolean = False

	Dim ArrFilter As New ArrayList

	Dim Lv_Parent_NoFaktur, Lv_Parent_Tanggal, Lv_Parent_KodeBarang, Lv_Parent_NamaBarang, Lv_Parent_PenanggungJawab, Lv_Parent_Jumlah, Lv_Parent_Satuan As String

	Dim Item_Parent_NoFaktur As Integer = 0
	Dim Item_Parent_Tanggal As Integer = 1
	Dim Item_Parent_KodeBarang As Integer = 2
	Dim Item_Parent_NamaBarang As Integer = 3
	Dim Item_Parent_PenanggungJawab As Integer = 4
	Dim Item_Parent_Jumlah As Integer = 5
	Dim Item_Parent_Satuan As Integer = 6

	Dim cellNo As Integer = 0
	Dim cellKdBarang As Integer = 1
	Dim cellNama As Integer = 2
	Dim cellQty As Integer = 3
	Dim cellSatuan As Integer = 4
	Dim cellPengali As Integer = 5
	Dim cellSatuanBarang As Integer = 6
	Dim cellNilaiBarang As Integer = 7
	Dim cellPersentase As Integer = 8
	Dim cellKet As Integer = 9
	Dim cellQty_SatHasil As Integer = 10
	Dim cellSatHasil As Integer = 11
	Dim cellEstHPP As Integer = 12
	Dim cellEstHPPPcs As Integer = 13

	Dim Item_Moisture_ID As Integer = 0
	Dim Item_Moisture_Kode_Analisa As Integer = 1
	Dim Item_Moisture_Jenis_Analisa As Integer = 2
	Dim Item_Moisture_Flag_Perhitungan As Integer = 3
	Dim Item_Moisture_Kode_Aktivitas As Integer = 4
	Dim Item_Moisture_Kategori As Integer = 5
	Dim Item_Moisture_Combobox As Integer = 6
	Dim Item_Moisture_Range_Awal As Integer = 7
	Dim Item_Moisture_Range_Akhir As Integer = 8
	Dim Item_Moisture_Value As Integer = 9

	Private Sub N_EMI_Transaksi_Formula_PO_Trial_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBuffer(Lv_Data_Formula)
		EnableDoubleBufferDGV(DgvFormulator_StepFormulator)
		EnableDoubleBufferDGV(Dgv_Moisture_Content)

		Lv_Data_Formula.Columns.Clear()
		Lv_Data_Formula.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
		Lv_Data_Formula.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
		Lv_Data_Formula.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
		Lv_Data_Formula.Columns.Add("Nama Barang", 380, HorizontalAlignment.Left)
		Lv_Data_Formula.Columns.Add("Penanggung Jawab", 150, HorizontalAlignment.Left)
		Lv_Data_Formula.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
		Lv_Data_Formula.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_Data_Formula.View = View.Details

		Cmb_Filter.Items.Clear() : ArrFilter.Clear()
		Cmb_Filter.Items.Add("No Faktur") : ArrFilter.Add("a.No_Faktur")
		Cmb_Filter.Items.Add("Kode Barang") : ArrFilter.Add("a.Kode_Barang")
		Cmb_Filter.Items.Add("Nama Barang") : ArrFilter.Add("b.Nama")
		Cmb_Filter.Items.Add("Penanggung Jawab") : ArrFilter.Add("c.Nama")

		Kosong()

	End Sub

	Private Sub GetNoFaktur()
		Dim FPro_Trial As String = "POTP"
		Txt_NoFaktur.Text = FPro_Trial & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("N_EMI_Transaksi_PO_Trial_Produksi", "No_Transaksi", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(No_Transaksi, 1, " & Len(FPro_Trial) + 4 & ")", FPro_Trial & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub Kosong()

		FormulaChecked = ""

		Cmb_Filter.SelectedIndex = -1
		Txt_Filter.Text = ""

		Txt_Keterangan.Text = ""

		get_jam()

		Try
			OpenConn()

			GetNoFaktur()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Load_Data_Parent()
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Get_Lv_Parent_Data(ByVal index As Integer)
		Lv_Parent_NoFaktur = Lv_Data_Formula.Items(index).SubItems(Item_Parent_NoFaktur).Text
		Lv_Parent_Tanggal = Lv_Data_Formula.Items(index).SubItems(Item_Parent_Tanggal).Text
		Lv_Parent_KodeBarang = Lv_Data_Formula.Items(index).SubItems(Item_Parent_KodeBarang).Text
		Lv_Parent_NamaBarang = Lv_Data_Formula.Items(index).SubItems(Item_Parent_NamaBarang).Text
		Lv_Parent_PenanggungJawab = Lv_Data_Formula.Items(index).SubItems(Item_Parent_PenanggungJawab).Text
		Lv_Parent_Jumlah = Lv_Data_Formula.Items(index).SubItems(Item_Parent_Jumlah).Text
		Lv_Parent_Satuan = Lv_Data_Formula.Items(index).SubItems(Item_Parent_Satuan).Text
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Cmb_Filter.SelectedIndex = -1 Then
			MessageBox.Show("Jenis Filter Harap Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Cmb_Filter.DroppedDown = True
			Cmb_Filter.Focus()
			Exit Sub
		End If

		Load_Data_Parent()
	End Sub

	Private Sub Load_Data_Parent()
		Try
			OpenConn()

			Dim Filter As String = ""
			If Cmb_Filter.SelectedIndex <> -1 Then
				If Txt_Filter.Text.Trim.Length > 0 Then
					Filter &= $"and {ArrFilter(Cmb_Filter.SelectedIndex)} like '%{Txt_Filter.Text.Trim}%' "
				End If
			End If

			isProcessing_Check = True
			Lv_Data_Formula.Items.Clear() : DgvFormulator_StepFormulator.Rows.Clear() : Dgv_Moisture_Content.Rows.Clear()
			SQL = $"
				select a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Barang, b.Nama as Nama_Barang, c.Nama as Penanggung_Jawab,
					a.Hasil, a.Satuan_Hasil
				from Emi_Transaksi_Formulator a
					inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_inq
					inner join Emi_Karyawan c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Penanggung_Jawab = c.Id_Karyawan
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Status is NULL
				and a.Flag_Validasi = 'Y'
				and a.flag_lanjut_trial_produksi = 'Y'
				and a.Flag_Input_Trial_Produksi is null
				{Filter}
				order by a.Tanggal, a.Jam
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Data_Formula.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama_Barang"))
					Lv.SubItems.Add(Dr("Penanggung_Jawab"))
					Lv.SubItems.Add(Format(Dr("Hasil"), "N4"))
					Lv.SubItems.Add(Dr("Satuan_Hasil"))

					If Not String.IsNullOrWhiteSpace(FormulaChecked) Then
						If FormulaChecked.Trim = Dr("No_Faktur").ToString.Trim Then
							Lv.Checked = True
						End If
					End If
				Loop
			End Using
			isProcessing_Check = False

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub LoadDetailFormula(ByVal No_Faktur As String)
		If Lv_Data_Formula.Items.Count = 0 Then Exit Sub

		If String.IsNullOrWhiteSpace(No_Faktur) Then
			MessageBox.Show("Harap Pilih Dahulu Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		Try
			OpenConn()

			DgvFormulator_StepFormulator.Rows.Clear()
			Dim Nomor As Integer = 1
			SQL = "select a.No_Faktur, a.Tanggal, a.Lokasi, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, a.UserID, a.Hasil, a.Satuan_Hasil, a.Penanggung_Jawab, "
			SQL &= $"c.Kode_Barang as Kode_Bahan, d.Nama as Nama_Bahan, c.Jumlah, c.satuan, c.Nilai_Pengali, c.Satuan_barang, c.Nilai_Barang, c.Persentase, "
			SQL &= $"case when exists( "
			SQL &= $"select 1 from Barang_SN z where c.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0 "
			SQL &= $") then ( "
			SQL &= $"select top 1 dbo.get_hpp(z.serial_number) from Barang_SN z where c.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0 "
			SQL &= $"order by z.tgl_masuk DESC) "
			SQL &= $"else d.estimasi_harga end Est_HPP, "

			SQL &= $"CASE WHEN EXISTS ( SELECT 1 FROM Barang_SN z WHERE  c.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0 "
			SQL &= $") THEN ISNULL( dbo.ubah_satuan(a.kode_perusahaan, 'masa', c.kode_barang, 'gram', d.satuan, (b.berat * (c.persentase / 100)))  "
			SQL &= $"* (SELECT TOP 1 dbo.get_hpp(z.serial_number)  "
			SQL &= $"FROM Barang_SN z  "
			SQL &= $"WHERE c.kode_barang = z.kode_barang  AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0 ORDER BY z.tgl_masuk DESC), 0) "
			SQL &= $"ELSE ISNULL(dbo.ubah_satuan(a.kode_perusahaan, 'masa', c.kode_barang, 'gram', d.satuan, (b.berat * (c.persentase / 100))) * d.estimasi_harga, 0) "
			SQL &= $"END AS Est_HPP_Pcs "

			SQL &= $"from Emi_Transaksi_Formulator a "
			SQL &= $"inner join Barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_inq "
			SQL &= $"inner join EMI_Transaksi_Formulator_Detail_Bahan c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Faktur "
			SQL &= $"inner join barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Status is NULL "
			SQL &= $"and a.No_Faktur = '{No_Faktur}' "
			SQL &= $"order by d.nama "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							DgvFormulator_StepFormulator.Rows.Add(1)
							DgvFormulator_StepFormulator.Rows(i).Cells(cellNo).Value = Nomor
							DgvFormulator_StepFormulator.Rows(i).Cells(cellKdBarang).Value = .Rows(i).Item("Kode_Bahan")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellNama).Value = .Rows(i).Item("Nama_Bahan")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellQty).Value = Format(.Rows(i).Item("Jumlah"), "N4")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellSatuan).Value = .Rows(i).Item("satuan")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellPengali).Value = Format(.Rows(i).Item("Nilai_Pengali"), "N4")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellSatuanBarang).Value = .Rows(i).Item("Satuan_barang")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellNilaiBarang).Value = Format(.Rows(i).Item("Nilai_Barang"), "N4")
							DgvFormulator_StepFormulator.Rows(i).Cells(cellPersentase).Value = Format(.Rows(i).Item("Persentase"), "N2")

							If General_Class.CekNULL(.Rows(i).Item("Est_HPP")) = "" Then
								DgvFormulator_StepFormulator.Rows(i).Cells(cellEstHPP).Value = 0
							Else
								DgvFormulator_StepFormulator.Rows(i).Cells(cellEstHPP).Value = Format(.Rows(i).Item("Est_HPP"), "N2")
							End If

							If General_Class.CekNULL(.Rows(i).Item("Est_HPP_Pcs")) = "" Then
								DgvFormulator_StepFormulator.Rows(i).Cells(cellEstHPPPcs).Value = 0
							Else
								DgvFormulator_StepFormulator.Rows(i).Cells(cellEstHPPPcs).Value = Format(.Rows(i).Item("Est_HPP_Pcs"), "N2")
							End If

							Nomor += 1

						Next
					Else
						CloseConn()
						MessageBox.Show($"Detail Bbahan Formula Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			'======================================
			'=     LOAD DATA MOISTURE CONTENT     =
			'======================================
			Dgv_Moisture_Content.Rows.Clear()
			SQL = "select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, '-' as Value_Combobox, a.Range_Awal, a.Range_Akhir "
			SQL &= $"from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a "
			SQL &= $"inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.No_Formula = '{No_Faktur}' "
			SQL &= $"union all "
			SQL &= $"select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, c.label_keterangan as Value_Combobox, '' as Range_Awal, '' as Range_Akhir "
			SQL &= $"from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a "
			SQL &= $"inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id "
			SQL &= $"inner join EMI_Switch c on a.Kode_Perusahaan = c.kode_perusahaan and a.nilai_kriteria = c.keterangan "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.No_Formula = '{No_Faktur}' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dgv_Moisture_Content.Rows.Add()

							Dim Flag_Perhitungan_Analisa As String = If(General_Class.CekNULL(.Rows(i).Item("Flag_Perhitungan")) = "", "T", .Rows(i).Item("Flag_Perhitungan"))

							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_ID).Value = .Rows(i).Item("Id")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Kode_Analisa).Value = .Rows(i).Item("Kode_Analisa")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Jenis_Analisa).Value = .Rows(i).Item("Jenis_Analisa")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Flag_Perhitungan).Value = Flag_Perhitungan_Analisa
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Kode_Aktivitas).Value = .Rows(i).Item("Kode_Aktivitas_Lab")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Kategori).Value = If(Flag_Perhitungan_Analisa.Trim = "Y", "Perhitungan", "Non Perhitungan")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Combobox).Value = .Rows(i).Item("Value_Combobox")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Range_Awal).Value = .Rows(i).Item("Range_Awal")
							Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Range_Akhir).Value = .Rows(i).Item("Range_Akhir")

							If .Rows(i).Item("Value_Combobox").ToString.Trim = "-" Then
								Dim Text As String = $"{ .Rows(i).Item("Range_Awal")} Sampai { .Rows(i).Item("Range_Akhir")}"
								Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Value).Value = Text
							Else
								Dgv_Moisture_Content.Rows(i).Cells(Item_Moisture_Value).Value = .Rows(i).Item("Value_Combobox")
							End If

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

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Lv_Data_Formula.Items.Count = 0 Then
			MessageBox.Show("Data Pada Listview Tidak Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Lv_Data_Formula.CheckedItems.Count = 0 Then
			MessageBox.Show("Harap Pilih Dahulu Formula Yang Ingin Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Lv_Data_Formula.Focus()
			Exit Sub
		ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Keterangan.Focus()
			Exit Sub
		ElseIf FormulaChecked.Trim.Length = 0 Then
			MessageBox.Show("Terjadi Kesalaham, Harap Ulangi Centang No Formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Lv_Data_Formula.Focus()
			Exit Sub
		End If

		Dim RowChecked As Integer = 0
		For Each index As Integer In Lv_Data_Formula.CheckedIndices
			Get_Lv_Parent_Data(index)
			RowChecked += 1
		Next

		If RowChecked > 1 Then
			MessageBox.Show("Terjadi Kesalaham Terdapat Lebih dari 1 Data Dicentang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If MessageBox.Show($"Yakin Ingin Simpan Formula {Lv_Parent_NoFaktur} Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			GetNoFaktur()

			'===========================
			'=     CEK ROLE BUTTON     =
			'===========================
			If CekButtonRole("Simpan_Formula_PO_Trial_Produksi") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Simpan Formula Trial Produksi Ini", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'=======================
			'=     CEK FORMULA     =
			'=======================
			SQL = $"
				select Status
				from Emi_Transaksi_Formulator
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur = '{Lv_Parent_NoFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan No Faktur {Lv_Parent_NoFaktur} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan No Faktur {Lv_Parent_NoFaktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'==============================================
			'=     CEK APAKAH ADA NO FAKTUR YANG SAMA     =
			'==============================================
			SQL = $"
				select top 1 1
				from N_EMI_Transaksi_PO_Trial_Produksi
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Transaksi = '{Txt_NoFaktur.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan, No Faktur Sudah Ada Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================
			'=     SIMPAN DATA     =
			'=======================
			SQL = $"
				insert into N_EMI_Transaksi_PO_Trial_Produksi (Kode_Perusahaan, No_Transaksi, No_Faktur_Formula, Keterangan, Kode_Barang, Nama_Barang, Tanggal, Jam, UserID)
				values ('{KodePerusahaan}', '{Txt_NoFaktur.Text.Trim}', '{Lv_Parent_NoFaktur.Trim}', '{Txt_Keterangan.Text.Trim}',
				'{Lv_Parent_KodeBarang.Trim}', '{Lv_Parent_NamaBarang.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{UserID}')
			"
			ExecuteTrans(SQL)

			SQL = $"
				update Emi_Transaksi_Formulator set Flag_Input_Trial_Produksi = 'Y'
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Status is null
				and No_Faktur = '{Lv_Parent_NoFaktur}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Formula {Lv_Parent_NoFaktur} Berhasil Disimpan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()

	End Sub

	'================================================================================================================================================================
	'=     HELPER
	'================================================================================================================================================================
	Private Sub Lv_Data_Formula_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles Lv_Data_Formula.ItemChecked
		If isProcessing_Check Then Return

		If Lv_Data_Formula.CheckedItems.Count = 0 Then
			DgvFormulator_StepFormulator.Rows.Clear()
			Dgv_Moisture_Content.Rows.Clear()
		Else
			If e.Item.Checked Then
				LoadDetailFormula(e.Item.SubItems(Item_Parent_NoFaktur).Text)
			End If
		End If
	End Sub

	Private Sub Lv_Data_Formula_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Lv_Data_Formula.ItemCheck
		If isProcessing_Check Then Return

		If e.NewValue = CheckState.Checked Then
			isProcessing_Check = True

			FormulaChecked = Lv_Data_Formula.Items(e.Index).Text
			For Each item As ListViewItem In Lv_Data_Formula.CheckedItems
				If item.Index <> e.Index Then
					item.Checked = False
				End If
			Next

			isProcessing_Check = False
		Else
			FormulaChecked = ""
		End If
	End Sub

	Private Sub Lv_Data_Formula_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data_Formula.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	Private Sub DgvFormulator_StepFormulator_MouseMove(sender As Object, e As MouseEventArgs) Handles DgvFormulator_StepFormulator.MouseMove, Dgv_Moisture_Content.MouseMove
		HandleDataGridViewHover(sender, e)
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

	Private Sub HandleDataGridViewHover(dgv As DataGridView, e As MouseEventArgs)
		Dim hit As DataGridView.HitTestInfo = dgv.HitTest(e.X, e.Y)

		dgv.Cursor = If(hit.Type = DataGridViewHitTestType.Cell, Cursors.Hand, Cursors.Default)

		If hit.RowIndex <> lastIndex Then

			If lastIndex >= 0 AndAlso lastIndex < dgv.Rows.Count Then
				dgv.Rows(lastIndex).DefaultCellStyle.BackColor = originalColor
			End If

			If hit.Type = DataGridViewHitTestType.Cell AndAlso hit.RowIndex >= 0 Then
				lastIndex = hit.RowIndex

				Dim currentRow = dgv.Rows(lastIndex)

				originalColor = currentRow.DefaultCellStyle.BackColor

				Dim displayColor As Color = currentRow.InheritedStyle.BackColor

				Dim amount As Integer = 23
				currentRow.DefaultCellStyle.BackColor = Color.FromArgb(
					Math.Max(0, displayColor.R - amount),
					Math.Max(0, displayColor.G - amount),
					Math.Max(0, displayColor.B - amount)
				)
			Else
				lastIndex = -1
			End If
		End If
	End Sub

	'================================================================================================================================================================
	'=     UTILITY
	'================================================================================================================================================================
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

	Private Sub EnableDoubleBufferDGV(dgv As DataGridView)
		Dim t As Type = dgv.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(dgv, True, Nothing)
	End Sub

End Class