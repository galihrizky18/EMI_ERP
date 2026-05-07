Public Class N_EMI_Transaksi_Validasi_Binding_Formula_Trial

	Private ReadOnly BodyAlignments_Parent As New Dictionary(Of Integer, StringAlignment)
	Private ReadOnly BodyAlignments_Order As New Dictionary(Of Integer, StringAlignment)
	Private ReadOnly BodyAlignments_Bahan As New Dictionary(Of Integer, StringAlignment)

	Dim arrFilter As New ArrayList

	Dim Lv_Parent_NoFaktur, Lv_Parent_Tanggal, Lv_Parent_Jam, Lv_Parent_KodeBarang, Lv_Parent_NamaBarang As String

	Dim Item_Lv_ChkBox As Integer = 0
	Dim Item_Lv_Parent_NoFaktur As Integer = 1
	Dim Item_Lv_Parent_Tanggal As Integer = 2
	Dim Item_Lv_Parent_Jam As Integer = 3
	Dim Item_Lv_Parent_KodeBarang As Integer = 4
	Dim Item_Lv_Parent_NamaBarang As Integer = 5

	Dim Lv_Order_Order, Lv_Order_NoFormula, Lv_Order_Hasil, Lv_Order_Keterangan As String

	Dim Item_Lv_Order_Order As Integer = 1
	Dim Item_Lv_Order_NoFormula As Integer = 2
	Dim Item_Lv_Order_Hasil As Integer = 3
	Dim Item_Lv_Order_Keterangan As Integer = 4

	Private Sub N_EMI_Transaksi_Validasi_Binding_Formula_Trial_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Cmb_Filter.Items.Clear() : arrFilter.Clear()
		Cmb_Filter.Items.Add("No Faktur") : arrFilter.Add("a.No_Faktur")
		Cmb_Filter.Items.Add("Tanggal") : arrFilter.Add("a.Tanggal")
		Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("a.Kode_Barang")
		Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("b.Nama")

		Kosong()

	End Sub

	Private Sub Kosong()

		Dgv_Formula_Parent.Rows.Clear()
		Dgv_Detail_Bahan.Rows.Clear()
		Dgv_Formula_Order.Rows.Clear()
		Cmb_Filter.SelectedIndex = -1

		FRM2_Txt_Est_HPP_Pcs.Text = Format(0, "N2")

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

			Dgv_Formula_Parent.Rows.Clear() : Dgv_Formula_Order.Rows.Clear() : Dgv_Detail_Bahan.Rows.Clear() : FRM2_Txt_Est_HPP_Pcs.Text = Format(0, "N2")
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
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Dgv_Formula_Parent.Rows.Add()
							Dgv_Formula_Parent.Rows(i).Cells(0).Value = False
							Dgv_Formula_Parent.Rows(i).Cells(1).Value = .Rows(i).Item("No_Faktur")
							Dgv_Formula_Parent.Rows(i).Cells(2).Value = Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy")
							Dgv_Formula_Parent.Rows(i).Cells(3).Value = .Rows(i).Item("Jam")
							Dgv_Formula_Parent.Rows(i).Cells(4).Value = .Rows(i).Item("Kode_Barang")
							Dgv_Formula_Parent.Rows(i).Cells(5).Value = .Rows(i).Item("Nama")

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

	Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
		If Dgv_Formula_Parent.Rows.Count = 0 Or Dgv_Formula_Parent.CurrentRow Is Nothing Then Exit Sub

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

			Dim SelectedFaktur As String = Dgv_Formula_Parent.CurrentRow.Cells(Item_Lv_Parent_NoFaktur).Value

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
		If Dgv_Formula_Parent.Rows.Count = 0 Or Dgv_Formula_Parent.CurrentRow Is Nothing Then Exit Sub

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

			Dim SelectedFaktur As String = Dgv_Formula_Parent.CurrentRow.Cells(Item_Lv_Parent_NoFaktur).Value

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

	Private Sub Dgv_Formula_Parent_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Formula_Parent.CellClick
		If Dgv_Formula_Parent.Rows.Count = 0 Or Dgv_Formula_Parent.CurrentRow Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim SelectedFaktur As String = Dgv_Formula_Parent.CurrentRow.Cells(Item_Lv_Parent_NoFaktur).Value

			Dgv_Formula_Order.Rows.Clear() : Dgv_Detail_Bahan.Rows.Clear() : FRM2_Txt_Est_HPP_Pcs.Text = Format(0, "N2")
			SQL = $"
				select b.No_Formulator, b.No_Prioritas, c.Hasil, c.Satuan_Hasil, b.Keterangan, b.Kode_Hierarki
				from N_EMI_Transaksi_Formulator_Binding a
					inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join Emi_Transaksi_Formulator c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Formulator = c.No_Faktur and c.Status is NULL
				where a.Status is null
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{SelectedFaktur}'
				order by b.No_Prioritas
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dim prioritas As Integer = If(IsDBNull(.Rows(i).Item("No_Prioritas")), 0, Convert.ToInt32(.Rows(i).Item("No_Prioritas")))
							Dim posisi As String

							If prioritas = 1 Then
								posisi = "Formula Utama"
							Else
								posisi = "Cadangan " & (prioritas - 1)
							End If

							Dgv_Formula_Order.Rows.Add()
							'Dgv_Formula_Order.Rows(i).Cells(0).Value = .Rows(i).Item("No_Prioritas")
							Dgv_Formula_Order.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Hierarki")
							Dgv_Formula_Order.Rows(i).Cells(1).Value = .Rows(i).Item("No_Formulator")
							Dgv_Formula_Order.Rows(i).Cells(2).Value = $"{ Format(.Rows(i).Item("Hasil"), "N4")} { .Rows(i).Item("Satuan_Hasil")}"
							Dgv_Formula_Order.Rows(i).Cells(3).Value = .Rows(i).Item("Keterangan")
							Dgv_Formula_Order.Rows(i).Cells(4).Value = "Compare"

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

	Private Sub Dgv_Formula_Order_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Formula_Order.CellContentClick

		If e.RowIndex < 0 Then Exit Sub

		' 2. Cek apakah yang diklik adalah kolom ke-4 (Index 4)
		If e.ColumnIndex = 4 Then

			Dim NoFaktur As String = Dgv_Formula_Parent.CurrentRow.Cells(Item_Lv_Parent_NoFaktur).Value
			Dim KdBarang As String = Dgv_Formula_Parent.CurrentRow.Cells(Item_Lv_Parent_KodeBarang).Value
			Dim NamaBarang As String = Dgv_Formula_Parent.CurrentRow.Cells(Item_Lv_Parent_NamaBarang).Value
			Dim SelectedFormula As String = Dgv_Formula_Order.CurrentRow.Cells(1).Value

			Dim SelectedFormula2 As String = ""
			If Dgv_Formula_Order.Rows.Count = 2 Then
				Dim index As Integer = Dgv_Formula_Order.CurrentRow.Index
				SelectedFormula2 = Dgv_Formula_Order.Rows(index + 1).Cells(1).Value
			End If

			N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.Txt_Faktur.Text = NoFaktur.Trim
			N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.Txt_Kd_Barang.Text = KdBarang.Trim
			N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.Txt_Nm_Barang.Text = NamaBarang.Trim
			N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.SelectedFormula_1 = ""
			N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.SelectedFormula_2 = ""
			N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.SelectedFormula_1 = SelectedFormula
			N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.SelectedFormula_2 = SelectedFormula2
			N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.Kosong()
			N_EMI_SD_Transaksi_Validasi_Binding_Formula_Trial_Compare_Formula.ShowDialog()
		End If

	End Sub

	Private Sub Dgv_Formula_Order_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Formula_Order.CellClick
		'If Lv_Formula_Order.Items.Count = 0 Or Lv_Formula_Order.FocusedItem Is Nothing Then Exit Sub
		If Dgv_Formula_Order.Rows.Count = 0 Then Exit Sub

		Try
			OpenConn()

			Dim SelectedFaktur As String = Dgv_Formula_Parent.CurrentRow.Cells(Item_Lv_Parent_NoFaktur).Value
			Dim SelectedFormula As String = Dgv_Formula_Order.CurrentRow.Cells(1).Value

			Dgv_Detail_Bahan.Rows.Clear() : FRM2_Txt_Est_HPP_Pcs.Text = Format(0, "N2")
			SQL = $"
				select a.No_Faktur, b.Kode_Barang, c.Nama, b.Persentase, b.Jumlah, b.satuan,
					CASE WHEN EXISTS (
						SELECT 1 FROM Barang_SN z WHERE b.kode_barang = z.kode_barang AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0
					) THEN
						ISNULL(
							dbo.ubah_satuan(a.kode_perusahaan, 'masa', b.kode_barang, 'gram', c.satuan, (d.berat * (b.persentase / 100)))
						* (SELECT TOP 1 dbo.get_hpp(z.serial_number)
						FROM Barang_SN z
						WHERE b.kode_barang = z.kode_barang  AND z.blok_sn IS NULL and dbo.get_hpp(z.serial_number) <> 0 ORDER BY z.tgl_masuk DESC), 0)
					ELSE ISNULL(dbo.ubah_satuan(a.kode_perusahaan, 'masa', b.kode_barang, 'gram', c.satuan, (d.berat * (b.persentase / 100))) * d.estimasi_harga, 0)
					END AS Est_HPP_Pcs
				from Emi_Transaksi_Formulator a
					inner join EMI_Transaksi_Formulator_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
					inner join barang d on a.kode_perusahaan = d.kode_perusahaan and a.kode_Stock_owner = d.kode_stock_owner and a.kode_barang = d.kode_barang_Inq
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{SelectedFormula}'
				and a.Status is NULL
				order by c.nama
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Dgv_Detail_Bahan.Rows.Add()
							Dgv_Detail_Bahan.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Barang")
							Dgv_Detail_Bahan.Rows(i).Cells(1).Value = .Rows(i).Item("Nama")
							Dgv_Detail_Bahan.Rows(i).Cells(2).Value = $"{ .Rows(i).Item("Persentase")} %"
							Dgv_Detail_Bahan.Rows(i).Cells(3).Value = $"{Format(.Rows(i).Item("Jumlah"), "N4")} { .Rows(i).Item("satuan")}"
							Dgv_Detail_Bahan.Rows(i).Cells(4).Value = $"{Format(.Rows(i).Item("Est_HPP_Pcs"), "N2")}"

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

		HitungEstHPPPcs()
	End Sub

	Private Sub Dgv_Formula_Parent_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Formula_Parent.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		Dim NoFaktur As String = Dgv_Formula_Parent.CurrentRow.Cells(Item_Lv_Parent_NoFaktur).Value

	End Sub

	Private Sub Btn_Validasi_Click(sender As Object, e As EventArgs) Handles Btn_Validasi.Click
		If Dgv_Formula_Parent.Rows.Count = 0 Then
			MessageBox.Show("Tidak Ada Data yang Bisa Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Dim hasData As Boolean = False
		For i As Integer = 0 To Dgv_Formula_Parent.Rows.Count - 1
			If Dgv_Formula_Parent.Rows(i).Cells(Item_Lv_ChkBox).Value = True Then
				hasData = True
			End If
		Next

		If Not hasData Then
			MessageBox.Show("Harap Pilih Minimal 1 Faktur Untuk Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

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

			If MessageBox.Show($"Yakin Ingin Melakukan Validasi Faktur yang Dipilih?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
				CloseTrans()
				CloseConn()
				Exit Sub
			End If

			For zzz As Integer = 0 To Dgv_Formula_Parent.Rows.Count - 1
				If Dgv_Formula_Parent.Rows(zzz).Cells(Item_Lv_ChkBox).Value = False Then Continue For

				Dim SelectedFaktur As String = Dgv_Formula_Parent.Rows(zzz).Cells(Item_Lv_Parent_NoFaktur).Value

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

			Next

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

	Private Sub Btn_Tolak_Click(sender As Object, e As EventArgs) Handles Btn_Tolak.Click
		If Dgv_Formula_Parent.Rows.Count = 0 Then
			MessageBox.Show("Tidak Ada Data yang Bisa Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Dim hasData As Boolean = False
		For i As Integer = 0 To Dgv_Formula_Parent.Rows.Count - 1
			If Dgv_Formula_Parent.Rows(i).Cells(Item_Lv_ChkBox).Value = True Then
				hasData = True
			End If
		Next

		If Not hasData Then
			MessageBox.Show("Harap Pilih Minimal 1 Faktur Untuk Ditolak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

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

			If MessageBox.Show($"Yakin Ingin Melakukan Tolak Faktur yang Dipilih?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
				CloseTrans()
				CloseConn()
				Exit Sub
			End If

			For zzz As Integer = 0 To Dgv_Formula_Parent.Rows.Count - 1
				If Dgv_Formula_Parent.Rows(zzz).Cells(Item_Lv_ChkBox).Value = False Then Continue For

				Dim SelectedFaktur As String = Dgv_Formula_Parent.Rows(zzz).Cells(Item_Lv_Parent_NoFaktur).Value

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

			Next

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

	Private Sub HitungEstHPPPcs()
		If Dgv_Detail_Bahan.Rows.Count = 0 Then
			FRM2_Txt_Est_HPP_Pcs.Text = Format(0, "N2")
		Else

			Dim totHPP As Double = 0
			For i As Integer = 0 To Dgv_Detail_Bahan.Rows.Count - 1
				totHPP += Val(HilangkanTanda(Dgv_Detail_Bahan.Rows(i).Cells(4).Value))
			Next

			FRM2_Txt_Est_HPP_Pcs.Text = Format(totHPP, "N2")
		End If

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

	Private Sub Dgv_Formula_Order_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Formula_Order.MouseMove
		Dim info As DataGridView.HitTestInfo = Dgv_Formula_Order.HitTest(e.X, e.Y)

		If info.RowIndex >= 0 AndAlso info.ColumnIndex >= 0 Then
			Dgv_Formula_Order.Cursor = Cursors.Hand
		Else
			Dgv_Formula_Order.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Dgv_Formula_Order_MouseLeave(sender As Object, e As EventArgs) Handles Dgv_Formula_Order.MouseLeave
		Dgv_Formula_Order.Cursor = Cursors.Default
	End Sub

	Private Sub Dgv_Formula_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Formula_Parent.MouseMove
		Dim info As DataGridView.HitTestInfo = Dgv_Formula_Parent.HitTest(e.X, e.Y)

		If info.RowIndex >= 0 AndAlso info.ColumnIndex >= 0 Then
			Dgv_Formula_Parent.Cursor = Cursors.Hand
		Else
			Dgv_Formula_Parent.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Dgv_Formula_Parent_MouseLeave(sender As Object, e As EventArgs) Handles Dgv_Formula_Parent.MouseLeave
		Dgv_Formula_Parent.Cursor = Cursors.Default
	End Sub

End Class