Public Class N_EMI_SD_Compare_Formulator
	Property NoFaktur As String
	Property ArrNoFaktur As List(Of String)
	Property ArrKeterangan As List(Of String)

	Private Sub N_EMI_SD_Compare_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		With LvF1DetailBahan
			.View = View.Details
			.FullRowSelect = True
			.GridLines = True
			.MultiSelect = False
			.HideSelection = False
			.Columns.Clear()

			.Columns.Add("Kode Barang", 100)
			.Columns.Add("Nama Barang", 300)
			.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
			.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
			.Columns.Add("Persentase", 100, HorizontalAlignment.Right)
			.Columns.Add("Harga", 100, HorizontalAlignment.Right)
			.Columns.Add("Est. HPP %", 100, HorizontalAlignment.Right)
		End With

		'With LvF1DetailKandungan
		'	.View = View.Details
		'	.FullRowSelect = True
		'	.GridLines = True
		'	.MultiSelect = False
		'	.HideSelection = False
		'	.Columns.Clear()

		'	.Columns.Add("Kode Uji", 100)
		'	.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		'	.Columns.Add("Hasil", 100, HorizontalAlignment.Right)
		'End With

		'With LvF1DetailStep
		'	.View = View.Details
		'	.FullRowSelect = True
		'	.GridLines = True
		'	.MultiSelect = False
		'	.HideSelection = False
		'	.Columns.Clear()

		'	.Columns.Add("No Step", 100)
		'	.Columns.Add("Kode", 80)
		'	.Columns.Add("Deskripsi", 250)
		'	.Columns.Add("Jumlah", 80, HorizontalAlignment.Right)
		'	.Columns.Add("Persentase", 80, HorizontalAlignment.Right)
		'End With

		With LvF2DetailBahan
			.View = View.Details
			.FullRowSelect = True
			.GridLines = True
			.MultiSelect = False
			.HideSelection = False
			.Columns.Clear()

			.Columns.Add("Kode Barang", 100)
			.Columns.Add("Nama Barang", 300)
			.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
			.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
			.Columns.Add("Persentase", 100, HorizontalAlignment.Right)
			.Columns.Add("Est. HPP", 100, HorizontalAlignment.Right)
			.Columns.Add("Est. HPP %", 100, HorizontalAlignment.Right)
		End With

		'With LvF2DetailKandungan
		'	.View = View.Details
		'	.FullRowSelect = True
		'	.GridLines = True
		'	.MultiSelect = False
		'	.HideSelection = False
		'	.Columns.Clear()

		'	.Columns.Add("Kode Uji", 100)
		'	.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		'	.Columns.Add("Hasil", 100, HorizontalAlignment.Right)
		'End With

		'With LvF2DetailStep
		'	.View = View.Details
		'	.FullRowSelect = True
		'	.GridLines = True
		'	.MultiSelect = False
		'	.HideSelection = False
		'	.Columns.Clear()

		'	.Columns.Add("No Step", 100)
		'	.Columns.Add("Kode", 80)
		'	.Columns.Add("Deskripsi", 250)
		'	.Columns.Add("Jumlah", 80, HorizontalAlignment.Right)
		'	.Columns.Add("Persentase", 80, HorizontalAlignment.Right)
		'End With

		Lv_Moisture_Content.Columns.Clear()
		Lv_Moisture_Content.Columns.Add("Kode Analisa", 130, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Jenis Analisa", 230, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Kategori", 180, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Kriteria", 180, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Range Awal", 150, HorizontalAlignment.Right)
		Lv_Moisture_Content.Columns.Add("Range Akhir", 150, HorizontalAlignment.Right)
		Lv_Moisture_Content.View = View.Details

		Lv_Moisture_Content_2.Columns.Clear()
		Lv_Moisture_Content_2.Columns.Add("Kode Analisa", 130, HorizontalAlignment.Left)
		Lv_Moisture_Content_2.Columns.Add("Jenis Analisa", 230, HorizontalAlignment.Left)
		Lv_Moisture_Content_2.Columns.Add("Kategori", 180, HorizontalAlignment.Left)
		Lv_Moisture_Content_2.Columns.Add("Kriteria", 180, HorizontalAlignment.Left)
		Lv_Moisture_Content_2.Columns.Add("Range Awal", 150, HorizontalAlignment.Right)
		Lv_Moisture_Content_2.Columns.Add("Range Akhir", 150, HorizontalAlignment.Right)
		Lv_Moisture_Content_2.View = View.Details

		Fetch_DetailF1()
		Fetch_LvF1DetailBahan()
		Fetch_LvF1DetailKandungan()
		Fetch_LvF1DetailStep()
		Fetch_Moisture_Content_1()

		If ArrNoFaktur.Contains(NoFaktur) Then
			TbPosisiSekarang.Text = (ArrNoFaktur.IndexOf(NoFaktur) + 1).ToString()
			TbKeterangan.Text = ArrKeterangan(ArrNoFaktur.IndexOf(NoFaktur))
		End If

		If ArrNoFaktur.Count = 0 Then
			TbPosisiTujuan.Text = "1"
			TbPosisiTujuan.Enabled = False
			TbKeterangan.Focus()
		End If

		Dim totalEstHPP As Decimal = 0
		Dim totalEstHPPPerPcs As Decimal = 0

		For Each item As ListViewItem In LvF1DetailBahan.Items
			totalEstHPP += Convert.ToDecimal(item.SubItems(5).Text)
		Next

		For Each item As ListViewItem In LvF1DetailBahan.Items
			totalEstHPPPerPcs += Convert.ToDecimal(item.SubItems(6).Text)
		Next

		'LbF1EstHpp.Text = "Est. HPP: " & FormatNumber(totalEstHPP, 4) & " | " & "Est. HPP Per Pcs: " & FormatNumber(totalEstHPPPerPcs, 4)
		LbF1EstHpp.Text = "Est. HPP Per Pcs: " & FormatNumber(totalEstHPPPerPcs, 4)
	End Sub

	Private Sub Fetch_Compare()
		Fetch_DetailF2(ArrNoFaktur(TbPosisiTujuan.Text - 1))
		Fetch_LvF2DetailBahan(ArrNoFaktur(TbPosisiTujuan.Text - 1))
		Fetch_LvF2DetailKandungan(ArrNoFaktur(TbPosisiTujuan.Text - 1))
		Fetch_LvF2DetailStep(ArrNoFaktur(TbPosisiTujuan.Text - 1))
		Fetch_Moisture_Content_2(ArrNoFaktur(TbPosisiTujuan.Text - 1))

		Dim totalEstHPP As Decimal = 0
		Dim totalEstHPPPerPcs As Decimal = 0

		For Each item As ListViewItem In LvF2DetailBahan.Items
			totalEstHPP += Convert.ToDecimal(item.SubItems(5).Text)
		Next

		For Each item As ListViewItem In LvF2DetailBahan.Items
			totalEstHPPPerPcs += Convert.ToDecimal(item.SubItems(6).Text)
		Next

		'LbF2EstHpp.Text = "Est. HPP: " & FormatNumber(totalEstHPP, 4) & " | " & "Est. HPP Per Pcs: " & FormatNumber(totalEstHPPPerPcs, 4)
		LbF2EstHpp.Text = "Est. HPP Per Pcs: " & FormatNumber(totalEstHPPPerPcs, 4)
	End Sub

	Private Sub Fetch_DetailF1()
		Try
			OpenConn()

			SQL = $"
				SELECT a.*, b.Nama AS Nama_Barang
				FROM Emi_Transaksi_Formulator a
				JOIN Barang b ON b.Kode_Perusahaan = a.Kode_Perusahaan AND b.Kode_Barang = a.Kode_Barang AND b.Kode_Stock_Owner = a.Kode_Stock_Owner
				WHERE a.Kode_Perusahaan = '{KodePerusahaan}' AND a.No_Faktur = '{NoFaktur}'

			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					TlpF1.Controls.Add(New Label With {.Text = "No Formula", .AutoSize = True}, 0, 0)
					TlpF1.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 0)
					TlpF1.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("No_Faktur")), "", Dr("No_Faktur").ToString), .AutoSize = True}, 2, 0)

					TlpF1.Controls.Add(New Label With {.Text = "Kode Barang", .AutoSize = True}, 0, 1)
					TlpF1.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 1)
					TlpF1.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Kode_Barang")), "", Dr("Kode_Barang").ToString), .AutoSize = True}, 2, 1)

					TlpF1.Controls.Add(New Label With {.Text = "Nama Barang", .AutoSize = True}, 0, 2)
					TlpF1.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 2)
					TlpF1.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Nama_Barang")), "", Dr("Nama_Barang").ToString), .AutoSize = True}, 2, 2)

					TlpF1.Controls.Add(New Label With {.Text = "Hasil", .AutoSize = True}, 0, 3)
					TlpF1.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 3)
					TlpF1.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Hasil")), "", Dr("Hasil").ToString) & " " & If(IsDBNull(Dr("Satuan_Hasil")), "", Dr("Satuan_Hasil").ToString), .AutoSize = True}, 2, 3)
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Fetch_LvF1DetailBahan()
		Try
			OpenConn()

			LvF1DetailBahan.Items.Clear()

			SQL = $"
			SELECT a.Kode_Barang, b.Nama AS Nama_Barang, a.Satuan,
				   a.Jumlah, a.Persentase, a.Est_HPP, a.Est_HPP_Per_Pcs
			FROM EMI_Transaksi_Formulator_Detail_Bahan a
			JOIN Barang b
				ON b.Kode_Perusahaan = a.Kode_Perusahaan
				AND b.Kode_Barang = a.Kode_Barang
				AND b.Kode_Stock_Owner = a.Kode_Stock_Owner
			WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
			AND a.No_Faktur = '{NoFaktur}'
		"

			Using Dr = OpenTrans(SQL)
				While Dr.Read
					Dim item As New ListViewItem(Dr("Kode_Barang").ToString)

					item.SubItems.Add(If(IsDBNull(Dr("Nama_Barang")), "", Dr("Nama_Barang").ToString))
					item.SubItems.Add(If(IsDBNull(Dr("Satuan")), "", Dr("Satuan").ToString))

					item.SubItems.Add(If(IsDBNull(Dr("Jumlah")), "0", FormatNumber(Dr("Jumlah"), 2)))
					item.SubItems.Add(If(IsDBNull(Dr("Persentase")), "0", FormatNumber(Dr("Persentase"), 2)))

					item.SubItems.Add(If(IsDBNull(Dr("Est_HPP")), "0", FormatNumber(Dr("Est_HPP"), 4)))
					item.SubItems.Add(If(IsDBNull(Dr("Est_HPP_Per_Pcs")), "0", FormatNumber(Dr("Est_HPP_Per_Pcs"), 4)))

					LvF1DetailBahan.Items.Add(item)
				End While
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub Fetch_LvF1DetailKandungan()
		Try
			OpenConn()

			'LvF1DetailKandungan.Items.Clear()

			'SQL = $"
			'	SELECT Kode_Uji, Satuan, Hasil
			'	FROM EMI_Transaksi_Formulator_Detail_Kandungan
			'	WHERE Kode_Perusahaan = '{KodePerusahaan}'
			'	AND No_Faktur = '{NoFaktur}'
			'"

			'Using Dr = OpenTrans(SQL)
			'	While Dr.Read
			'		Dim item As New ListViewItem(Dr("Kode_Uji").ToString)

			'		item.SubItems.Add(If(IsDBNull(Dr("Satuan")), "", Dr("Satuan").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Hasil")), "", Dr("Hasil").ToString))

			'		LvF1DetailKandungan.Items.Add(item)
			'	End While
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub Fetch_LvF1DetailStep()
		Try
			OpenConn()

			'LvF1DetailStep.Items.Clear()

			'SQL = $"
			'	SELECT No_Step, Kode, Deskripsi, Jumlah, Satuan, Persentase
			'	FROM EMI_Transaksi_Formulator_Detail_Step
			'	WHERE Kode_Perusahaan = '{KodePerusahaan}'
			'	AND No_Faktur = '{NoFaktur}'
			'"

			'Using Dr = OpenTrans(SQL)
			'	While Dr.Read
			'		Dim item As New ListViewItem(Dr("No_Step").ToString)

			'		item.SubItems.Add(If(IsDBNull(Dr("Kode")), "", Dr("Kode").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Deskripsi")), "", Dr("Deskripsi").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Jumlah")), "0", FormatNumber(Dr("Jumlah"), 2)))
			'		item.SubItems.Add(If(IsDBNull(Dr("Persentase")), "0", FormatNumber(Dr("Persentase"), 2)))

			'		LvF1DetailStep.Items.Add(item)
			'	End While
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub Fetch_Moisture_Content_1()
		Try
			OpenConn()

			Lv_Moisture_Content.Items.Clear()
			SQL = $"
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, '-' as Value_Combobox, a.Range_Awal, a.Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{NoFaktur}'
				union all
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, c.label_keterangan as Value_Combobox, '' as Range_Awal, '' as Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				inner join EMI_Switch c on a.Kode_Perusahaan = c.kode_perusahaan and a.nilai_kriteria = c.keterangan
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{NoFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim Flag_Perhitungan_Analisa As String = If(General_Class.CekNULL(Dr("Flag_Perhitungan")) = "", "T", Dr("Flag_Perhitungan"))

					Dim Lv As ListViewItem
					Lv = Lv_Moisture_Content.Items.Add(Dr("Kode_Analisa"))
					Lv.SubItems.Add(Dr("Jenis_Analisa"))
					Lv.SubItems.Add(If(Flag_Perhitungan_Analisa.Trim = "Y", "Perhitungan", "Non Perhitungan"))
					Lv.SubItems.Add(Dr("Value_Combobox"))
					Lv.SubItems.Add(Dr("Range_Awal"))
					Lv.SubItems.Add(Dr("Range_Akhir"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub TbPosisiTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TbPosisiTujuan.KeyPress
		Dim txt As TextBox = CType(sender, TextBox)

		If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
			e.Handled = True
			Exit Sub
		End If

		If Char.IsDigit(e.KeyChar) Then

			Dim newValue As String = txt.Text & e.KeyChar

			If IsNumeric(newValue) Then

				Dim value As Integer = CInt(newValue)
				Dim maxValue As Integer = ArrNoFaktur.Count + 1

				If value = 0 Then
					e.Handled = True
					Exit Sub
				End If

				If value > maxValue Then
					MessageBox.Show("Posisi formula tidak boleh lebih dari " & maxValue)

					txt.Text = maxValue.ToString()
					txt.SelectionStart = txt.Text.Length

					e.Handled = True
				End If
			End If
		End If
	End Sub

	Private Sub TbPosisiTujuan_TextChanged(sender As Object, e As EventArgs) Handles TbPosisiTujuan.TextChanged
		LvF2DetailBahan.Items.Clear()
		'LvF2DetailKandungan.Items.Clear()
		'LvF2DetailStep.Items.Clear()
		Lv_Moisture_Content_2.Items.Clear()

		TlpF2.Controls.Clear()

		If TbPosisiTujuan.Text = "" Then Exit Sub
		If Not IsNumeric(TbPosisiTujuan.Text) Then Exit Sub

		Dim posisi As Integer = CInt(TbPosisiTujuan.Text)

		If posisi <= ArrNoFaktur.Count Then
			Fetch_Compare()
		End If
	End Sub

	Private Sub Fetch_DetailF2(NoFaktur As String)
		Try
			OpenConn()

			SQL = $"
				SELECT a.*, b.Nama AS Nama_Barang
				FROM Emi_Transaksi_Formulator a
				JOIN Barang b ON b.Kode_Perusahaan = a.Kode_Perusahaan AND b.Kode_Barang = a.Kode_Barang AND b.Kode_Stock_Owner = a.Kode_Stock_Owner
				WHERE a.Kode_Perusahaan = '{KodePerusahaan}' AND a.No_Faktur = '{NoFaktur}'

			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					TlpF2.Controls.Add(New Label With {.Text = "No Formula", .AutoSize = True}, 0, 0)
					TlpF2.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 0)
					TlpF2.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("No_Faktur")), "", Dr("No_Faktur").ToString), .AutoSize = True}, 2, 0)

					TlpF2.Controls.Add(New Label With {.Text = "Kode Barang", .AutoSize = True}, 0, 1)
					TlpF2.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 1)
					TlpF2.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Kode_Barang")), "", Dr("Kode_Barang").ToString), .AutoSize = True}, 2, 1)

					TlpF2.Controls.Add(New Label With {.Text = "Nama Barang", .AutoSize = True}, 0, 2)
					TlpF2.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 2)
					TlpF2.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Nama_Barang")), "", Dr("Nama_Barang").ToString), .AutoSize = True}, 2, 2)

					TlpF2.Controls.Add(New Label With {.Text = "Hasil", .AutoSize = True}, 0, 3)
					TlpF2.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 3)
					TlpF2.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Hasil")), "", Dr("Hasil").ToString) & " " & If(IsDBNull(Dr("Satuan_Hasil")), "", Dr("Satuan_Hasil").ToString), .AutoSize = True}, 2, 3)
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Fetch_LvF2DetailBahan(NoFaktur As String)
		Try
			OpenConn()

			LvF2DetailBahan.Items.Clear()

			SQL = $"
			SELECT a.Kode_Barang, b.Nama AS Nama_Barang, a.Satuan,
				   a.Jumlah, a.Persentase, a.Est_HPP, a.Est_HPP_Per_Pcs
			FROM EMI_Transaksi_Formulator_Detail_Bahan a
			JOIN Barang b
				ON b.Kode_Perusahaan = a.Kode_Perusahaan
				AND b.Kode_Barang = a.Kode_Barang
				AND b.Kode_Stock_Owner = a.Kode_Stock_Owner
			WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
			AND a.No_Faktur = '{NoFaktur}'
		"

			Using Dr = OpenTrans(SQL)
				While Dr.Read
					Dim item As New ListViewItem(Dr("Kode_Barang").ToString)

					item.SubItems.Add(If(IsDBNull(Dr("Nama_Barang")), "", Dr("Nama_Barang").ToString))
					item.SubItems.Add(If(IsDBNull(Dr("Satuan")), "", Dr("Satuan").ToString))

					item.SubItems.Add(If(IsDBNull(Dr("Jumlah")), "0", FormatNumber(Dr("Jumlah"), 2)))
					item.SubItems.Add(If(IsDBNull(Dr("Persentase")), "0", FormatNumber(Dr("Persentase"), 2)))

					item.SubItems.Add(If(IsDBNull(Dr("Est_HPP")), "0", FormatNumber(Dr("Est_HPP"), 4)))
					item.SubItems.Add(If(IsDBNull(Dr("Est_HPP_Per_Pcs")), "0", FormatNumber(Dr("Est_HPP_Per_Pcs"), 4)))

					LvF2DetailBahan.Items.Add(item)
				End While
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub Fetch_LvF2DetailKandungan(NoFaktur As String)
		Try
			OpenConn()

			'LvF2DetailKandungan.Items.Clear()

			'SQL = $"
			'	SELECT Kode_Uji, Satuan, Hasil
			'	FROM EMI_Transaksi_Formulator_Detail_Kandungan
			'	WHERE Kode_Perusahaan = '{KodePerusahaan}'
			'	AND No_Faktur = '{NoFaktur}'
			'"

			'Using Dr = OpenTrans(SQL)
			'	While Dr.Read
			'		Dim item As New ListViewItem(Dr("Kode_Uji").ToString)

			'		item.SubItems.Add(If(IsDBNull(Dr("Satuan")), "", Dr("Satuan").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Hasil")), "", Dr("Hasil").ToString))

			'		LvF2DetailKandungan.Items.Add(item)
			'	End While
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub Fetch_LvF2DetailStep(NoFaktur As String)
		Try
			OpenConn()

			'LvF2DetailStep.Items.Clear()

			'SQL = $"
			'	SELECT No_Step, Kode, Deskripsi, Jumlah, Satuan, Persentase
			'	FROM EMI_Transaksi_Formulator_Detail_Step
			'	WHERE Kode_Perusahaan = '{KodePerusahaan}'
			'	AND No_Faktur = '{NoFaktur}'
			'"

			'Using Dr = OpenTrans(SQL)
			'	While Dr.Read
			'		Dim item As New ListViewItem(Dr("No_Step").ToString)

			'		item.SubItems.Add(If(IsDBNull(Dr("Kode")), "", Dr("Kode").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Deskripsi")), "", Dr("Deskripsi").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Jumlah")), "0", FormatNumber(Dr("Jumlah"), 2)))
			'		item.SubItems.Add(If(IsDBNull(Dr("Persentase")), "0", FormatNumber(Dr("Persentase"), 2)))

			'		LvF2DetailStep.Items.Add(item)
			'	End While
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub Fetch_Moisture_Content_2(NoFaktur As String)
		Try
			OpenConn()

			Lv_Moisture_Content_2.Items.Clear()
			SQL = $"
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, '-' as Value_Combobox, a.Range_Awal, a.Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{NoFaktur}'
				union all
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, c.label_keterangan as Value_Combobox, '' as Range_Awal, '' as Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				inner join EMI_Switch c on a.Kode_Perusahaan = c.kode_perusahaan and a.nilai_kriteria = c.keterangan
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{NoFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim Flag_Perhitungan_Analisa As String = If(General_Class.CekNULL(Dr("Flag_Perhitungan")) = "", "T", Dr("Flag_Perhitungan"))

					Dim Lv As ListViewItem
					Lv = Lv_Moisture_Content_2.Items.Add(Dr("Kode_Analisa"))
					Lv.SubItems.Add(Dr("Jenis_Analisa"))
					Lv.SubItems.Add(If(Flag_Perhitungan_Analisa.Trim = "Y", "Perhitungan", "Non Perhitungan"))
					Lv.SubItems.Add(Dr("Value_Combobox"))
					Lv.SubItems.Add(Dr("Range_Awal"))
					Lv.SubItems.Add(Dr("Range_Akhir"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
		Try

			If TbPosisiTujuan.Text = "" OrElse Not IsNumeric(TbPosisiTujuan.Text) Then
				MessageBox.Show("Posisi tujuan tidak valid")
				Exit Sub
			End If

			If TbKeterangan.Text = "" Then
				MessageBox.Show("Keterangan tidak boleh kosong")
				Exit Sub
			End If

			Dim posisiTujuan As Integer = CInt(TbPosisiTujuan.Text)
			Dim posisiSekarang As Integer = 0

			If TbPosisiSekarang.Text <> "-" AndAlso IsNumeric(TbPosisiSekarang.Text) Then
				posisiSekarang = CInt(TbPosisiSekarang.Text)
			End If

			Dim maxPosisi As Integer = ArrNoFaktur.Count + 1

			If posisiTujuan < 1 Or posisiTujuan > maxPosisi Then
				MessageBox.Show("Posisi tujuan tidak valid")
				Exit Sub
			End If

			If posisiTujuan <= ArrNoFaktur.Count Then

				Dim result = MessageBox.Show(
				"Posisi tersebut sudah ditempati formula lain." & vbCrLf &
				"Formula setelahnya akan bergeser." & vbCrLf &
				"Lanjutkan?",
				"Konfirmasi Perubahan Posisi",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question)

				If result = DialogResult.No Then Exit Sub

			End If

			If posisiSekarang > 0 And posisiSekarang <= ArrNoFaktur.Count Then

				ArrNoFaktur.RemoveAt(posisiSekarang - 1)
				ArrKeterangan.RemoveAt(posisiSekarang - 1)

			End If
			ArrNoFaktur.Insert(posisiTujuan - 1, NoFaktur)
			ArrKeterangan.Insert(posisiTujuan - 1, TbKeterangan.Text)

			Me.DialogResult = DialogResult.OK
			Me.Close()
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub TbKeterangan_TextChanged(sender As Object, e As EventArgs) Handles TbKeterangan.TextChanged

	End Sub

End Class