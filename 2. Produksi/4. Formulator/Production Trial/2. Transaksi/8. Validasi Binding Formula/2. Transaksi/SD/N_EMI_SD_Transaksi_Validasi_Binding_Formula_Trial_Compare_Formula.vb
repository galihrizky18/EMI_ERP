Public Class N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula

	Dim arrNoFakturFormula As New ArrayList

	Private Sub N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		If Txt_Faktur.Text.Trim.Length = 0 Then
			MessageBox.Show("Terjadi Kesalahan, No Faktur Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
			Exit Sub
		End If

		FRM1_Lv_Bahan.Columns.Clear()
		FRM1_Lv_Bahan.Columns.Add("Kode Bahan", 120, HorizontalAlignment.Left)
		FRM1_Lv_Bahan.Columns.Add("Bahan", 200, HorizontalAlignment.Left)
		FRM1_Lv_Bahan.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
		FRM1_Lv_Bahan.Columns.Add("Persentase", 90, HorizontalAlignment.Center)
		FRM1_Lv_Bahan.Columns.Add("Harga", 120, HorizontalAlignment.Right)
		FRM1_Lv_Bahan.Columns.Add("Hpp Pcs", 120, HorizontalAlignment.Right)
		FRM1_Lv_Bahan.View = View.Details

		FRM1_Lv_Moisture_Content.Columns.Clear()
		FRM1_Lv_Moisture_Content.Columns.Add("Kode Analisa", 100, HorizontalAlignment.Left)
		FRM1_Lv_Moisture_Content.Columns.Add("Jenis Analisa", 150, HorizontalAlignment.Left)
		FRM1_Lv_Moisture_Content.Columns.Add("Kategori", 130, HorizontalAlignment.Left)
		FRM1_Lv_Moisture_Content.Columns.Add("Kriteria", 150, HorizontalAlignment.Left)
		FRM1_Lv_Moisture_Content.Columns.Add("Range Awal", 130, HorizontalAlignment.Right)
		FRM1_Lv_Moisture_Content.Columns.Add("Range Akhir", 130, HorizontalAlignment.Right)
		FRM1_Lv_Moisture_Content.View = View.Details

		FRM2_Lv_Bahan.Columns.Clear()
		FRM2_Lv_Bahan.Columns.Add("Kode Bahan", 120, HorizontalAlignment.Left)
		FRM2_Lv_Bahan.Columns.Add("Bahan", 200, HorizontalAlignment.Left)
		FRM2_Lv_Bahan.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
		FRM2_Lv_Bahan.Columns.Add("Persentase", 90, HorizontalAlignment.Center)
		FRM2_Lv_Bahan.Columns.Add("Harga", 120, HorizontalAlignment.Right)
		FRM2_Lv_Bahan.Columns.Add("Hpp Pcs", 120, HorizontalAlignment.Right)
		FRM2_Lv_Bahan.View = View.Details

		FRM2_Lv_Moisture_Content.Columns.Clear()
		FRM2_Lv_Moisture_Content.Columns.Add("Kode Analisa", 100, HorizontalAlignment.Left)
		FRM2_Lv_Moisture_Content.Columns.Add("Jenis Analisa", 150, HorizontalAlignment.Left)
		FRM2_Lv_Moisture_Content.Columns.Add("Kategori", 130, HorizontalAlignment.Left)
		FRM2_Lv_Moisture_Content.Columns.Add("Kriteria", 150, HorizontalAlignment.Left)
		FRM2_Lv_Moisture_Content.Columns.Add("Range Awal", 130, HorizontalAlignment.Right)
		FRM2_Lv_Moisture_Content.Columns.Add("Range Akhir", 130, HorizontalAlignment.Right)
		FRM2_Lv_Moisture_Content.View = View.Details

		Try
			OpenConn()

			'=======================
			'=     GET FORMULA     =
			'=======================
			Cmb_Formula_1.Items.Clear() : Cmb_Formula_2.Items.Clear() : arrNoFakturFormula.Clear()
			SQL = $"
				select a.No_Faktur, c.No_Faktur as No_Faktur_Formula, b.Keterangan
				from N_EMI_Transaksi_Formulator_Binding a
					inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join Emi_Transaksi_Formulator c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Formulator = c.No_Faktur and c.Status is null
				where a.Status is NULL
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{Txt_Faktur.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Show As String = $"{Dr("No_Faktur_Formula")} - {Dr("Keterangan")}"
					Cmb_Formula_1.Items.Add(Show) : Cmb_Formula_2.Items.Add(Show)
					arrNoFakturFormula.Add(Dr("No_Faktur_Formula"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()

	End Sub

	Public Sub Kosong()

		'Cmb_Formula_1.SelectedIndex = -1 : Cmb_Formula_2.SelectedIndex = -1

		'Kosong_Formula_1()
		'Kosong_Formula_2()

		Btn_Clear_Click(Txt_Faktur, New EventArgs)
	End Sub

	Private Sub Btn_Clear_Click(sender As Object, e As EventArgs) Handles Btn_Clear.Click
		Cmb_Formula_1.SelectedIndex = -1 : Cmb_Formula_2.SelectedIndex = -1

		Kosong_Formula_1()
		Kosong_Formula_2()
	End Sub

	Private Sub Kosong_Formula_1()

		FRM1_TL.Controls.Clear()
		FRM1_Lv_Bahan.Items.Clear()
		FRM1_Lv_Moisture_Content.Items.Clear()
		FRM1_Txt_Est_HPP_Pcs.Text = ""

	End Sub

	Private Sub Kosong_Formula_2()
		FRM2_TL.Controls.Clear()
		FRM2_Lv_Bahan.Items.Clear()
		FRM2_Lv_Moisture_Content.Items.Clear()
		FRM2_Txt_Est_HPP_Pcs.Text = ""
	End Sub

	Private Sub Cmb_Formula_1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Formula_1.SelectedIndexChanged
		If Cmb_Formula_1.Items.Count = 0 Or Cmb_Formula_1.SelectedIndex = -1 Then Exit Sub

		'=================================
		'=     CEK APAKAH INDEX SAMA     =
		'=================================
		If Cmb_Formula_2.SelectedIndex <> -1 Then
			If arrNoFakturFormula(Cmb_Formula_1.SelectedIndex).ToString.ToUpper.Trim = arrNoFakturFormula(Cmb_Formula_2.SelectedIndex).ToString.ToUpper.Trim Then
				MessageBox.Show("Formula 1 tidak boleh sama dengan formula 2", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_Formula_1.SelectedIndex = -1
				Cmb_Formula_1.DroppedDown = True
				Kosong_Formula_1()
				Exit Sub
			End If
		End If

		Load_Formula_1(arrNoFakturFormula(Cmb_Formula_1.SelectedIndex))
	End Sub

	Private Sub Cmb_Formula_2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Formula_2.SelectedIndexChanged
		If Cmb_Formula_2.Items.Count = 0 Or Cmb_Formula_2.SelectedIndex = -1 Then Exit Sub

		'=================================
		'=     CEK APAKAH INDEX SAMA     =
		'=================================
		If Cmb_Formula_1.SelectedIndex <> -1 Then
			If arrNoFakturFormula(Cmb_Formula_1.SelectedIndex).ToString.ToUpper.Trim = arrNoFakturFormula(Cmb_Formula_2.SelectedIndex).ToString.ToUpper.Trim Then
				MessageBox.Show("Formula 2 tidak boleh sama dengan formula 1", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_Formula_2.SelectedIndex = -1
				Cmb_Formula_2.DroppedDown = True
				Kosong_Formula_2()
				Exit Sub
			End If
		End If

		Load_Formula_2(arrNoFakturFormula(Cmb_Formula_2.SelectedIndex))
	End Sub

	Private Sub Load_Formula_1(ByVal No_Formula As String)

		Kosong_Formula_1()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim TotHppPcs As Double = 0

			'===========================
			'=     GET DETAIL DATA     =
			'===========================
			SQL = $"
				select a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Barang, b.Nama, a.Hasil, a.Satuan_Hasil
				from Emi_Transaksi_Formulator a
					inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang
				where a.Status is null
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{No_Formula}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					Dim NoFormula As String = Dr("No_Faktur")
					Dim Tanggal As String = $"{Format(Dr("Tanggal"), "dd MMM yyyy")}, {Dr("Jam")}"
					Dim Barang As String = $"{Dr("Kode_Barang")}, {Dr("Nama")}"
					Dim Hasil As String = $"{Format(Dr("Hasil"), "N4")} {Dr("Satuan_Hasil")}"

					FRM1_TL.Controls.Add(New Label With {.Text = "No Formula", .AutoSize = True}, 0, 0)
					FRM1_TL.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 0)
					FRM1_TL.Controls.Add(New Label With {.Text = NoFormula, .AutoSize = True}, 2, 0)

					FRM1_TL.Controls.Add(New Label With {.Text = "Tanggal", .AutoSize = True}, 0, 1)
					FRM1_TL.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 1)
					FRM1_TL.Controls.Add(New Label With {.Text = Tanggal, .AutoSize = True}, 2, 1)

					FRM1_TL.Controls.Add(New Label With {.Text = "Barang", .AutoSize = True}, 0, 2)
					FRM1_TL.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 2)
					FRM1_TL.Controls.Add(New Label With {.Text = Barang, .AutoSize = True}, 2, 2)

					FRM1_TL.Controls.Add(New Label With {.Text = "Hasil", .AutoSize = True}, 0, 3)
					FRM1_TL.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 3)
					FRM1_TL.Controls.Add(New Label With {.Text = Hasil, .AutoSize = True}, 2, 3)
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"No Formula {No_Formula} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=================================
			'=     GET DETAIL BAHAN DATA     =
			'=================================
			FRM1_Lv_Bahan.Items.Clear()
			SQL = $"
				select a.No_Faktur, a.Tanggal, a.Lokasi, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, a.UserID, a.Hasil, a.Satuan_Hasil, a.Penanggung_Jawab,
					c.Kode_Barang as Kode_Bahan, d.Nama as Nama_Bahan, c.Jumlah, c.satuan, c.Nilai_Pengali, c.Satuan_barang, c.Nilai_Barang, c.Persentase,
					case when exists(
					select 1 from Barang_SN z where c.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0
					) then (
					select top 1 dbo.get_hpp(z.serial_number) from Barang_SN z where c.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0
					order by z.tgl_masuk DESC)
					else isnull(d.estimasi_harga, 0) end Est_HPP,

					CASE WHEN EXISTS ( SELECT 1 FROM Barang_SN z WHERE  c.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0
					) THEN ISNULL( dbo.ubah_satuan(a.kode_perusahaan, 'masa', c.kode_barang, 'gram', d.satuan, (b.berat * (c.persentase / 100)))
					* (SELECT TOP 1 dbo.get_hpp(z.serial_number)
					FROM Barang_SN z
					WHERE c.kode_barang = z.kode_barang  AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0 ORDER BY z.tgl_masuk DESC), 0)
					ELSE ISNULL(dbo.ubah_satuan(a.kode_perusahaan, 'masa', c.kode_barang, 'gram', d.satuan, (b.berat * (c.persentase / 100))) * isnull(d.estimasi_harga, 0), 0)
					END AS Est_HPP_Pcs

				from Emi_Transaksi_Formulator a
				inner join Barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_inq
				inner join EMI_Transaksi_Formulator_Detail_Bahan c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Faktur
				inner join barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Status is NULL
				and a.No_Faktur = '{No_Formula}'
				order by d.nama
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = FRM1_Lv_Bahan.Items.Add(Dr("Kode_Bahan"))
					Lv.SubItems.Add(Dr("Nama_Bahan"))
					Lv.SubItems.Add($"{Format(Dr("Jumlah"), "N4")} {Dr("satuan")}")
					Lv.SubItems.Add($"{Dr("Persentase")} %")
					Lv.SubItems.Add(Format(Dr("Est_HPP"), "N2"))
					Lv.SubItems.Add(Format(Dr("Est_HPP_Pcs"), "N2"))

					TotHppPcs += Val(HilangkanTanda(Dr("Est_HPP_Pcs")))
				Loop
			End Using

			'=======================================
			'=     GET DETAIL MOISTURE CONTENT     =
			'=======================================
			FRM1_Lv_Moisture_Content.Items.Clear()
			SQL = $"
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, '-' as Value_Combobox, a.Range_Awal, a.Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{No_Formula}'
				union all
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, c.label_keterangan as Value_Combobox, '' as Range_Awal, '' as Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				inner join EMI_Switch c on a.Kode_Perusahaan = c.kode_perusahaan and a.nilai_kriteria = c.keterangan
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{No_Formula}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim Flag_Perhitungan_Analisa As String = If(General_Class.CekNULL(Dr("Flag_Perhitungan")) = "", "T", Dr("Flag_Perhitungan"))

					Dim Lv As ListViewItem
					Lv = FRM1_Lv_Moisture_Content.Items.Add(Dr("Kode_Analisa"))
					Lv.SubItems.Add(Dr("Jenis_Analisa"))
					Lv.SubItems.Add(If(Flag_Perhitungan_Analisa.Trim = "Y", "Perhitungan", "Non Perhitungan"))
					Lv.SubItems.Add(Dr("Value_Combobox"))
					Lv.SubItems.Add(Dr("Range_Awal"))
					Lv.SubItems.Add(Dr("Range_Akhir"))
				Loop
			End Using

			FRM1_Txt_Est_HPP_Pcs.Text = Format(Val(HilangkanTanda(TotHppPcs)), "N2")

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

	Private Sub Load_Formula_2(ByVal No_Formula As String)
		Kosong_Formula_2()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim TotHppPcs As Double = 0

			'===========================
			'=     GET DETAIL DATA     =
			'===========================
			SQL = $"
				select a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Barang, b.Nama, a.Hasil, a.Satuan_Hasil
				from Emi_Transaksi_Formulator a
					inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang
				where a.Status is null
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{No_Formula}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					Dim NoFormula As String = Dr("No_Faktur")
					Dim Tanggal As String = $"{Format(Dr("Tanggal"), "dd MMM yyyy")}, {Dr("Jam")}"
					Dim Barang As String = $"{Dr("Kode_Barang")}, {Dr("Nama")}"
					Dim Hasil As String = $"{Format(Dr("Hasil"), "N4")} {Dr("Satuan_Hasil")}"

					FRM2_TL.Controls.Add(New Label With {.Text = "No Formula", .AutoSize = True}, 0, 0)
					FRM2_TL.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 0)
					FRM2_TL.Controls.Add(New Label With {.Text = NoFormula, .AutoSize = True}, 2, 0)

					FRM2_TL.Controls.Add(New Label With {.Text = "Tanggal", .AutoSize = True}, 0, 1)
					FRM2_TL.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 1)
					FRM2_TL.Controls.Add(New Label With {.Text = Tanggal, .AutoSize = True}, 2, 1)

					FRM2_TL.Controls.Add(New Label With {.Text = "Barang", .AutoSize = True}, 0, 2)
					FRM2_TL.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 2)
					FRM2_TL.Controls.Add(New Label With {.Text = Barang, .AutoSize = True}, 2, 2)

					FRM2_TL.Controls.Add(New Label With {.Text = "Hasil", .AutoSize = True}, 0, 3)
					FRM2_TL.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 3)
					FRM2_TL.Controls.Add(New Label With {.Text = Hasil, .AutoSize = True}, 2, 3)
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"No Formula {No_Formula} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=================================
			'=     GET DETAIL BAHAN DATA     =
			'=================================
			FRM2_Lv_Bahan.Items.Clear()
			SQL = $"
				select a.No_Faktur, a.Tanggal, a.Lokasi, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, a.UserID, a.Hasil, a.Satuan_Hasil, a.Penanggung_Jawab,
					c.Kode_Barang as Kode_Bahan, d.Nama as Nama_Bahan, c.Jumlah, c.satuan, c.Nilai_Pengali, c.Satuan_barang, c.Nilai_Barang, c.Persentase,
					case when exists(
					select 1 from Barang_SN z where c.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0
					) then (
					select top 1 dbo.get_hpp(z.serial_number) from Barang_SN z where c.kode_barang = z.kode_barang and z.blok_sn is null and dbo.get_hpp(z.serial_number) <> 0
					order by z.tgl_masuk DESC)
					else isnull(d.estimasi_harga, 0) end Est_HPP,

					CASE WHEN EXISTS ( SELECT 1 FROM Barang_SN z WHERE  c.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0
					) THEN ISNULL( dbo.ubah_satuan(a.kode_perusahaan, 'masa', c.kode_barang, 'gram', d.satuan, (b.berat * (c.persentase / 100)))
					* (SELECT TOP 1 dbo.get_hpp(z.serial_number)
					FROM Barang_SN z
					WHERE c.kode_barang = z.kode_barang  AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0 ORDER BY z.tgl_masuk DESC), 0)
					ELSE ISNULL(dbo.ubah_satuan(a.kode_perusahaan, 'masa', c.kode_barang, 'gram', d.satuan, (b.berat * (c.persentase / 100))) * isnull(d.estimasi_harga, 0), 0)
					END AS Est_HPP_Pcs

				from Emi_Transaksi_Formulator a
				inner join Barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_inq
				inner join EMI_Transaksi_Formulator_Detail_Bahan c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Faktur
				inner join barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Status is NULL
				and a.No_Faktur = '{No_Formula}'
				order by d.nama
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = FRM2_Lv_Bahan.Items.Add(Dr("Kode_Bahan"))
					Lv.SubItems.Add(Dr("Nama_Bahan"))
					Lv.SubItems.Add($"{Format(Dr("Jumlah"), "N4")} {Dr("satuan")}")
					Lv.SubItems.Add($"{Dr("Persentase")} %")
					Lv.SubItems.Add(Format(Dr("Est_HPP"), "N2"))
					Lv.SubItems.Add(Format(Dr("Est_HPP_Pcs"), "N2"))

					TotHppPcs += Val(HilangkanTanda(Dr("Est_HPP_Pcs")))
				Loop
			End Using

			'=======================================
			'=     GET DETAIL MOISTURE CONTENT     =
			'=======================================
			FRM2_Lv_Moisture_Content.Items.Clear()
			SQL = $"
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, '-' as Value_Combobox, a.Range_Awal, a.Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{No_Formula}'
				union all
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, c.label_keterangan as Value_Combobox, '' as Range_Awal, '' as Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				inner join EMI_Switch c on a.Kode_Perusahaan = c.kode_perusahaan and a.nilai_kriteria = c.keterangan
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{No_Formula}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim Flag_Perhitungan_Analisa As String = If(General_Class.CekNULL(Dr("Flag_Perhitungan")) = "", "T", Dr("Flag_Perhitungan"))

					Dim Lv As ListViewItem
					Lv = FRM2_Lv_Moisture_Content.Items.Add(Dr("Kode_Analisa"))
					Lv.SubItems.Add(Dr("Jenis_Analisa"))
					Lv.SubItems.Add(If(Flag_Perhitungan_Analisa.Trim = "Y", "Perhitungan", "Non Perhitungan"))
					Lv.SubItems.Add(Dr("Value_Combobox"))
					Lv.SubItems.Add(Dr("Range_Awal"))
					Lv.SubItems.Add(Dr("Range_Akhir"))
				Loop
			End Using

			FRM2_Txt_Est_HPP_Pcs.Text = Format(Val(HilangkanTanda(TotHppPcs)), "N2")

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

	'======================================================================================================================================================
	'=     UTILITY
	'======================================================================================================================================================

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

End Class