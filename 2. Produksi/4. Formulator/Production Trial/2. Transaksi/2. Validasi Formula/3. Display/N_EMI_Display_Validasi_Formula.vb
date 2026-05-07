Public Class N_EMI_Display_Validasi_Formula

	Dim ArrTanggal, ArrParamLainLama As New ArrayList

	Dim Dgv_Parent_NoFaktur, Dgv_Parent_Tanggal, Dgv_Parent_KodeProduk, Dgv_Parent_Produk, Dgv_Parent_Hasil, Dgv_Parent_Satuan, Dgv_Parent_PenanggungJawab As String

	Dim Cell_Parent_NoFaktur As Integer = 0
	Dim Cell_Parent_Tanggal As Integer = 1
	Dim Cell_Parent_KodeProduk As Integer = 2
	Dim Cell_Parent_Produk As Integer = 3
	Dim Cell_Parent_Hasil As Integer = 4
	Dim Cell_Parent_Satuan As Integer = 5
	Dim Cell_Parent_PenanggungJawab As Integer = 6

	Dim Cell_Bahan_KodeBahan As Integer = 0
	Dim Cell_Bahan_Bahan As Integer = 1
	Dim Cell_Bahan_Persentase As Integer = 2
	Dim Cell_Bahan_Jumlah As Integer = 3
	Dim Cell_Bahan_Satuan As Integer = 4
	Dim Cell_Bahan_EstHarga As Integer = 5
	Dim Cell_Bahan_EstHppPcs As Integer = 6

	Dim Cell_Moisture_KodeAnalisa As Integer = 0
	Dim Cell_Moisture_JenisAnalisa As Integer = 1
	Dim Cell_Moisture_Kategori As Integer = 2
	Dim Cell_Moisture_Value As Integer = 3

	Private Sub N_EMI_Display_Validasi_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBufferDGV(Dgv_Parent)
		EnableDoubleBufferDGV(Dgv_Detail_Bahan)
		EnableDoubleBufferDGV(Dgv_Detail_Mositure)

		Cmb_ParamTgl.Items.Clear() : ArrTanggal.Clear()
		Cmb_ParamTgl.Items.Add("Tanggal") : ArrTanggal.Add("a.Tanggal")

		Cmb_ParamLain.Items.Clear() : ArrParamLainLama.Clear()
		Cmb_ParamLain.Items.Add("No Faktur") : ArrParamLainLama.Add("a.No_Faktur")
		Cmb_ParamLain.Items.Add("Kode Produk") : ArrParamLainLama.Add("a.Kode_Barang")
		Cmb_ParamLain.Items.Add("Nama Produk") : ArrParamLainLama.Add("b.nama")
		Cmb_ParamLain.Items.Add("Penanggung Jawab") : ArrParamLainLama.Add("c.Nama")

		Kosong()
	End Sub

	Private Sub Kosong()

		Dgv_Detail_Bahan.Rows.Clear()
		Dgv_Detail_Mositure.Rows.Clear()

		Cmb_ParamTgl.SelectedIndex = -1
		DateTimePicker1.Value = Date.Now
		DateTimePicker2.Value = Date.Now

		Cmb_ParamLain.SelectedIndex = -1
		Txt_ParamValue.Text = ""

		Chk_TransaksiHrIni.Checked = True

		LoadData()
	End Sub

	Private Sub Get_Data_Parent(ByVal index As Integer)
		Dgv_Parent_NoFaktur = Dgv_Parent.Rows(index).Cells(Cell_Parent_NoFaktur).Value
		Dgv_Parent_Tanggal = Dgv_Parent.Rows(index).Cells(Cell_Parent_Tanggal).Value
		Dgv_Parent_KodeProduk = Dgv_Parent.Rows(index).Cells(Cell_Parent_KodeProduk).Value
		Dgv_Parent_Produk = Dgv_Parent.Rows(index).Cells(Cell_Parent_Produk).Value
		Dgv_Parent_Hasil = Dgv_Parent.Rows(index).Cells(Cell_Parent_Hasil).Value
		Dgv_Parent_Satuan = Dgv_Parent.Rows(index).Cells(Cell_Parent_Satuan).Value
		Dgv_Parent_PenanggungJawab = Dgv_Parent.Rows(index).Cells(Cell_Parent_PenanggungJawab).Value

	End Sub

	Private Sub LoadData()
		get_jam()

		Try
			OpenConn()

			Dim Filter As String = " "
			If Cmb_Lokasi.SelectedIndex > 0 Then
				'Filter &= "AND b.lokasi = '" & Cmb_Lokasi.Text & "' "
			End If

			If Chk_TransaksiHrIni.Checked Then
				Filter &= "AND Tanggal between '" & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now.AddDays(1), "yyyy-MM-dd") & "' "
			End If

			If Chk_ParamTgl.Checked Then
				Filter &= "AND " & ArrTanggal(Cmb_ParamTgl.SelectedIndex) & " between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value.AddDays(1), "yyyy-MM-dd") & "' "
			End If

			If Chk_ParamLain.Checked Then
				Filter &= "AND " & ArrParamLainLama(Cmb_ParamLain.SelectedIndex) & " like '%" & Txt_ParamValue.Text & "%' "
			End If

			Dgv_Parent.Rows.Clear()
			Dgv_Detail_Bahan.Rows.Clear()
			Dgv_Detail_Mositure.Rows.Clear()
			SQL = $"
				select a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Barang as Kode_Produk, b.nama as Nama_Produk,
					a.Hasil, a.Satuan_Hasil, c.Nama as Penanggung_Jawab,
					a.Status, a.Flag_Validasi, a.Flag_Validasi_Main
				from Emi_Transaksi_Formulator a
					inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang_Inq
					inner join Emi_Karyawan c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Penanggung_Jawab = c.Id_Karyawan
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				order by a.Tanggal, a.Jam
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					With Dgv_Parent.Rows(Dgv_Parent.Rows.Add())
						.Cells(Cell_Parent_NoFaktur).Value = Dr("No_Faktur")
						.Cells(Cell_Parent_Tanggal).Value = Format(Dr("Tanggal"), "dd MMM yyyy")
						.Cells(Cell_Parent_KodeProduk).Value = Dr("Kode_Produk")
						.Cells(Cell_Parent_Produk).Value = Dr("Nama_Produk")
						.Cells(Cell_Parent_Hasil).Value = Format(Dr("Hasil"), "N4")
						.Cells(Cell_Parent_Satuan).Value = Dr("Satuan_Hasil")
						.Cells(Cell_Parent_PenanggungJawab).Value = Dr("Penanggung_Jawab")
						.DefaultCellStyle.Padding = New Padding(2, 0, 2, 0)

						If General_Class.CekNULL(Dr("Status")) = "Y" Then
							.DefaultCellStyle.BackColor = Color.DarkRed
							.DefaultCellStyle.ForeColor = Color.White
						ElseIf General_Class.CekNULL(Dr("Flag_Validasi")) = "Y" Then
							.DefaultCellStyle.BackColor = Color.LightGreen
							.DefaultCellStyle.ForeColor = Color.Black
						End If
					End With
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Dgv_Parent_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellClick
		If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim SelectedFaktur As String = Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoFaktur).Value

			Dgv_Detail_Bahan.Rows.Clear()
			SQL = $"
				select a.No_Faktur, b.Kode_Barang as Kode_Bahan, c.Nama as Bahan, b.Persentase, b.Jumlah, b.satuan,
					b.Est_HPP, b.Est_HPP_Per_Pcs
				from Emi_Transaksi_Formulator a
					inner join EMI_Transaksi_Formulator_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Faktur = '{SelectedFaktur.Trim}'
				order by b.Kode_Barang
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					With Dgv_Detail_Bahan.Rows(Dgv_Detail_Bahan.Rows.Add())
						.Cells(Cell_Bahan_KodeBahan).Value = Dr("Kode_Bahan")
						.Cells(Cell_Bahan_Bahan).Value = Dr("Bahan")
						.Cells(Cell_Bahan_Persentase).Value = Format(Dr("Persentase"), "N2")
						.Cells(Cell_Bahan_Jumlah).Value = Format(Dr("Jumlah"), "N4")
						.Cells(Cell_Bahan_Satuan).Value = Dr("satuan")
						.Cells(Cell_Bahan_EstHarga).Value = If(General_Class.CekNULL(Dr("Est_HPP")) = "", "-", Format(Dr("Est_HPP"), "N2"))
						.Cells(Cell_Bahan_EstHppPcs).Value = If(General_Class.CekNULL(Dr("Est_HPP_Per_Pcs")) = "", "-", Format(Dr("Est_HPP_Per_Pcs"), "N2"))
						.DefaultCellStyle.Padding = New Padding(2, 0, 2, 0)
					End With
				Loop

			End Using

			Dgv_Detail_Mositure.Rows.Clear()
			SQL = $"
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, '-' as Value_Combobox, a.Range_Awal, a.Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{SelectedFaktur.Trim}'
				union all
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, c.label_keterangan as Value_Combobox, '' as Range_Awal, '' as Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				inner join EMI_Switch c on a.Kode_Perusahaan = c.kode_perusahaan and a.nilai_kriteria = c.keterangan
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{SelectedFaktur.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim Flag_Perhitungan_Analisa As String = If(General_Class.CekNULL(Dr("Flag_Perhitungan")) = "", "T", Dr("Flag_Perhitungan"))

					With Dgv_Detail_Mositure.Rows(Dgv_Detail_Mositure.Rows.Add())
						.Cells(Cell_Moisture_KodeAnalisa).Value = Dr("Kode_Analisa")
						.Cells(Cell_Moisture_JenisAnalisa).Value = Dr("Jenis_Analisa")
						.Cells(Cell_Moisture_Kategori).Value = If(Flag_Perhitungan_Analisa.Trim = "Y", "Perhitungan", "Non Perhitungan")
						.DefaultCellStyle.Padding = New Padding(2, 0, 2, 0)

						If Dr("Value_Combobox").ToString.Trim = "-" Then
							Dim Text As String = $"{ Dr("Range_Awal")} Sampai { Dr("Range_Akhir")}"
							.Cells(Cell_Moisture_Value).Value = Text
						Else
							.Cells(Cell_Moisture_Value).Value = Dr("Value_Combobox")
						End If

					End With
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub BatalkanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanToolStripMenuItem.Click
		If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then Exit Sub

		Dim SelectedFaktur As String = Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoFaktur).Value

		If MessageBox.Show($"Yakin Ingin Membatalkan Tranaksi {SelectedFaktur}??", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===========================
			'=     CEK ROLE BUTTON     =
			'===========================
			If CekButtonRole("Batal_Validasi_Formula_Trial") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Pembatalkan Validasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'==================================================
			'=     CEK APAKAH NO FORMULA SUDAH DIBATALKAN     =
			'==================================================
			SQL = $"
				select Status, Flag_Validasi, Flag_Validasi_Main
				from Emi_Transaksi_Formulator
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur = '{SelectedFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Pembatalan Tidak Dapat Dilakukan Karena No Formula {SelectedFaktur} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					If General_Class.CekNULL(Dr("Flag_Validasi")) = "" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Pembatalan Tidak Dapat Dilakukan Karena No Formula {SelectedFaktur} Belum Melakukan Validasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					If General_Class.CekNULL(Dr("Flag_Validasi_Main")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Pembatalan Tidak Dapat Dilakukan Karena No Formula {SelectedFaktur} Sudah Dilakukan Validasi Main", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Pembatalan Tidak Dapat Dilakukan Karena No Formula {SelectedFaktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=============================
			'=     PROSES PEMBATALAN     =
			'=============================

			SQL = "delete N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Formula = '{SelectedFaktur}' "
			ExecuteTrans(SQL)

			SQL = "delete N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Formula = '{SelectedFaktur}' "
			ExecuteTrans(SQL)

			SQL = $"
				update Emi_Transaksi_Formulator set Flag_Validasi = NULL,
					Tanggal_Validasi = NULL, Jam_Validasi = NULL, User_Validasi = NULL
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Faktur = '{SelectedFaktur}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"No Faktur {SelectedFaktur} Berhasil Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Chk_TransaksiHrIni.Checked = True
		Btn_Cari.PerformClick()
	End Sub

	'==================================================================================================================================
	'=     HANDLE FILTER
	'==================================================================================================================================
	Private Sub Chk_TransaksiHrIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_TransaksiHrIni.CheckedChanged
		If Chk_TransaksiHrIni.Checked = True Then
			Chk_ParamTgl.Checked = False
			Btn_Cari_Click(Chk_TransaksiHrIni, e)
		End If
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Not Chk_TransaksiHrIni.Checked And Not Chk_ParamTgl.Checked And Not Chk_ParamLain.Checked Then
			MessageBox.Show("Pilih Dahulu Parameter Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Chk_TransaksiHrIni.Focus()
			Exit Sub
		End If

		If Chk_ParamTgl.Checked Then
			If Cmb_ParamTgl.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Tanggal Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_ParamTgl.DroppedDown = True : Cmb_ParamTgl.Focus()
				Exit Sub
			End If
		End If

		If Chk_ParamLain.Checked Then
			If Cmb_ParamLain.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_ParamLain.DroppedDown = True : Cmb_ParamLain.Focus()
				Exit Sub
			Else
				If Txt_ParamValue.Text.Trim.Length = 0 Then
					MessageBox.Show("Value Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_ParamValue.Focus()
					Exit Sub
				End If
			End If
		End If

		LoadData()
	End Sub

	Private Sub Chk_ParamTgl_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamTgl.CheckedChanged
		If Chk_ParamTgl.Checked Then
			Cmb_ParamTgl.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
			Chk_TransaksiHrIni.Checked = False
		Else
			Cmb_ParamTgl.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
			Cmb_ParamTgl.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
		End If
	End Sub

	Private Sub Chk_ParamLain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamLain.CheckedChanged
		If Chk_ParamLain.Checked Then
			Cmb_ParamLain.Enabled = True : Txt_ParamValue.Enabled = True
		Else
			Cmb_ParamLain.Enabled = False : Txt_ParamValue.Enabled = False
			Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamValue.Text = ""
		End If
	End Sub

	Private Sub Cmb_ParamTgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamTgl.KeyPress
		If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
	End Sub

	Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
		If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
	End Sub

	Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
		If e.KeyChar = Chr(13) Then Chk_ParamLain.Focus()
	End Sub

	Private Sub Cmb_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_ParamLain.KeyPress
		If e.KeyChar = Chr(13) Then Txt_ParamValue.Focus()
	End Sub

	Private Sub Txt_ParamValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamValue.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
	End Sub

	Private Sub Chk_ParamTgl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_ParamTgl.KeyPress
		If e.KeyChar = Chr(13) Then
			If Chk_ParamTgl.Checked Then
				Cmb_ParamTgl.DroppedDown = True
				Cmb_ParamTgl.Focus()
			End If
		End If
	End Sub

	Private Sub Chk_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_ParamLain.KeyPress
		If e.KeyChar = Chr(13) Then
			If Chk_ParamLain.Checked Then
				Cmb_ParamLain.DroppedDown = True
				Cmb_ParamLain.Focus()
			End If
		End If
	End Sub

	'===================================================================================================================================================
	'=     HELPER
	'===================================================================================================================================================
	Private Sub Dgv_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Parent.MouseMove
		HandleDataGridViewHover(sender, e)
	End Sub

	Private Sub Dgv_Detail_Bahan_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Detail_Bahan.MouseMove
		HandleDataGridViewHover(sender, e)
	End Sub

	Private Sub Dgv_Detail_Mositure_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Detail_Mositure.MouseMove
		HandleDataGridViewHover(sender, e)
	End Sub

	Private Sub SalinNoFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFormulaToolStripMenuItem.Click
		If Dgv_Parent.Rows.Count = 0 Or Dgv_Parent.CurrentRow Is Nothing Then
			MessageBox.Show("Pilih Dahulu Transaksi Yang Ingin Disalin", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Dgv_Parent.CurrentRow.Cells(Cell_Parent_NoFaktur).Value)
	End Sub

	'===================================================================================================================================================
	'=     UTILITY
	'===================================================================================================================================================

	Protected Overrides Sub WndProc(ByRef m As Message)
		If m.Msg = &HA3 Then
			Return
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub Dgv_Parent_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_Parent.CellFormatting
		Try
			If e.RowIndex < 0 OrElse e.Value Is Nothing Then Exit Sub

			' Logika untuk Kolom Qty
			If e.ColumnIndex = Cell_Parent_Hasil Then

				Dim jumlah As Double = Val(HilangkanTanda(e.Value.ToString()))
				Dim satuan As String = Dgv_Parent.Rows(e.RowIndex).Cells(Cell_Parent_Satuan).Value?.ToString()

				e.Value = $"{Format(jumlah, "N4")} {satuan}"
				e.FormattingApplied = True

			End If
		Catch ex As Exception
			Debug.WriteLine("Error di CellFormatting " & ex.Message)
		End Try
	End Sub

	Private Sub Dgv_Detail_Bahan_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_Detail_Bahan.CellFormatting
		Try
			If e.RowIndex < 0 OrElse e.Value Is Nothing Then Exit Sub

			' Logika untuk Kolom Qty
			If e.ColumnIndex = Cell_Bahan_Jumlah Then

				Dim jumlah As Double = Val(HilangkanTanda(e.Value.ToString()))
				Dim satuan As String = Dgv_Detail_Bahan.Rows(e.RowIndex).Cells(Cell_Bahan_Satuan).Value?.ToString()

				e.Value = $"{Format(jumlah, "N4")} {satuan}"
				e.FormattingApplied = True
			ElseIf e.ColumnIndex = Cell_Bahan_Persentase Then

				Dim Nilai As Double = Val(HilangkanTanda(e.Value.ToString()))

				e.Value = $"{Format(Nilai, "N2")} %"
				e.FormattingApplied = True

			End If
		Catch ex As Exception
			Debug.WriteLine("Error di CellFormatting " & ex.Message)
		End Try
	End Sub

	Private Sub EnableDoubleBufferDGV(dgv As DataGridView)
		Dim t As Type = dgv.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(dgv, True, Nothing)
	End Sub

	Private Sub HandleDataGridViewHover(dgv As DataGridView, e As MouseEventArgs)

		Dim hit As DataGridView.HitTestInfo = dgv.HitTest(e.X, e.Y)

		' 👉 Cursor hanya jika benar di CELL
		If hit.Type = DataGridViewHitTestType.Cell Then
			dgv.Cursor = Cursors.Hand
		Else
			dgv.Cursor = Cursors.Default
		End If

		' 👉 Jika bukan cell, stop di sini
		If hit.Type <> DataGridViewHitTestType.Cell Then Exit Sub

		Dim rowIndex As Integer = hit.RowIndex
		If rowIndex < 0 Then Exit Sub

		Dim lastRowIndex As Integer = -1
		If dgv.Tag IsNot Nothing Then
			lastRowIndex = CInt(dgv.Tag)
		End If

		Dim currentRow = dgv.Rows(rowIndex)

		If currentRow.DefaultCellStyle.BackColor <> Color.White AndAlso
	   currentRow.DefaultCellStyle.BackColor <> Color.Empty Then
			Exit Sub
		End If

		If lastRowIndex <> rowIndex Then

			If lastRowIndex >= 0 AndAlso lastRowIndex < dgv.Rows.Count Then
				Dim lastRow = dgv.Rows(lastRowIndex)

				If lastRow.DefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235) Then
					lastRow.DefaultCellStyle.BackColor = Color.White
				End If
			End If

			currentRow.DefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235)

			dgv.Tag = rowIndex
		End If
	End Sub

	Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

		If Dgv_Parent.Rows.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If

		Dim mousePos As Point = Dgv_Parent.PointToClient(Control.MousePosition)

		Dim info As DataGridView.HitTestInfo = Dgv_Parent.HitTest(mousePos.X, mousePos.Y)

		If info.RowIndex < 0 Then
			e.Cancel = True
			Exit Sub
		End If

		Dgv_Parent.ClearSelection()
		Dgv_Parent.Rows(info.RowIndex).Selected = True

		Dgv_Parent.CurrentCell = Dgv_Parent.Rows(info.RowIndex).Cells(info.ColumnIndex)
	End Sub

End Class