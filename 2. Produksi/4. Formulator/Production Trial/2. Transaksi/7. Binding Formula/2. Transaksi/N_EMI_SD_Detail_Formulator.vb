Public Class N_EMI_SD_Detail_Formulator
	Property NoFaktur As String

	Private Sub N_EMI_SD_Detail_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		With LvDetailBahan
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
			.Columns.Add("Est. HPP Per Pcs", 100, HorizontalAlignment.Right)
		End With

		'With LvDetailKandungan
		'    .View = View.Details
		'    .FullRowSelect = True
		'    .GridLines = True
		'    .MultiSelect = False
		'    .HideSelection = False
		'    .Columns.Clear()

		'    .Columns.Add("Kode Uji", 100)
		'    .Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		'    .Columns.Add("Hasil", 100, HorizontalAlignment.Right)
		'End With

		'With LvDetailStep
		'    .View = View.Details
		'    .FullRowSelect = True
		'    .GridLines = True
		'    .MultiSelect = False
		'    .HideSelection = False
		'    .Columns.Clear()

		'    .Columns.Add("No Step", 100)
		'    .Columns.Add("Kode", 80)
		'    .Columns.Add("Deskripsi", 300)
		'    .Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
		'    .Columns.Add("Persentase", 100, HorizontalAlignment.Right)
		'End With

		Lv_Moisture_Content.Columns.Clear()
		Lv_Moisture_Content.Columns.Add("Kode Analisa", 130, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Jenis Analisa", 230, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Kategori", 180, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Kriteria", 180, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Range Awal", 150, HorizontalAlignment.Right)
		Lv_Moisture_Content.Columns.Add("Range Akhir", 150, HorizontalAlignment.Right)
		Lv_Moisture_Content.View = View.Details

		Fetch_DetailFormula()
		Fetch_LvDetailBahan()
		Fetch_LvDetailKandungan()
		Fetch_LvDetailStep()
		Fetch_LvDetailMoistureContent()

		Dim totalEstHPP As Decimal = 0
		Dim totalEstHPPPerPcs As Decimal = 0

		For Each item As ListViewItem In LvDetailBahan.Items
			totalEstHPP += Convert.ToDecimal(item.SubItems(5).Text)
		Next

		For Each item As ListViewItem In LvDetailBahan.Items
			totalEstHPPPerPcs += Convert.ToDecimal(item.SubItems(6).Text)
		Next

		'LbEstHPP.Text = "Est. HPP: " & FormatNumber(totalEstHPP, 4) & " | " & "Est. HPP Per Pcs: " & FormatNumber(totalEstHPPPerPcs, 4)
		LbEstHPP.Text = "Est. HPP Per Pcs: " & FormatNumber(totalEstHPPPerPcs, 4)
	End Sub

	Private Sub Fetch_DetailFormula()
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
					TlpDetailFormulator.Controls.Add(New Label With {.Text = "No Formula", .AutoSize = True}, 0, 0)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 0)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("No_Faktur")), "", Dr("No_Faktur").ToString), .AutoSize = True}, 2, 0)

					TlpDetailFormulator.Controls.Add(New Label With {.Text = "Kode Barang", .AutoSize = True}, 0, 1)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 1)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Kode_Barang")), "", Dr("Kode_Barang").ToString), .AutoSize = True}, 2, 1)

					TlpDetailFormulator.Controls.Add(New Label With {.Text = "Nama Barang", .AutoSize = True}, 0, 2)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 2)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Nama_Barang")), "", Dr("Nama_Barang").ToString), .AutoSize = True}, 2, 2)

					TlpDetailFormulator.Controls.Add(New Label With {.Text = "Hasil", .AutoSize = True}, 0, 3)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 3)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Hasil")), "", Dr("Hasil").ToString) & " " & If(IsDBNull(Dr("Satuan_Hasil")), "", Dr("Satuan_Hasil").ToString), .AutoSize = True}, 2, 3)

					TlpDetailFormulator.Controls.Add(New Label With {.Text = "Tanggal Dibuat", .AutoSize = True}, 4, 0)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 5, 0)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Tanggal")), "", Convert.ToDateTime(Dr("Tanggal")).ToString("dd MMM yyyy")), .AutoSize = True}, 6, 0)

					TlpDetailFormulator.Controls.Add(New Label With {.Text = "Jam Dibuat", .AutoSize = True}, 4, 1)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 5, 1)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Jam")), "", Dr("Jam").ToString), .AutoSize = True}, 6, 1)

					TlpDetailFormulator.Controls.Add(New Label With {.Text = "User", .AutoSize = True}, 4, 2)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 5, 2)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("UserID")), "", Dr("UserID").ToString), .AutoSize = True}, 6, 2)

					TlpDetailFormulator.Controls.Add(New Label With {.Text = "Tanggal Validasi", .AutoSize = True}, 8, 0)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 9, 0)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Tanggal_Validasi")), "", Convert.ToDateTime(Dr("Tanggal_Validasi")).ToString("dd MMM yyyy")), .AutoSize = True}, 10, 0)

					TlpDetailFormulator.Controls.Add(New Label With {.Text = "Jam Validasi", .AutoSize = True}, 8, 1)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 9, 1)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Jam_Validasi")), "", Dr("Jam_Validasi").ToString), .AutoSize = True}, 10, 1)

					TlpDetailFormulator.Controls.Add(New Label With {.Text = "User", .AutoSize = True}, 8, 2)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 9, 2)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("User_Validasi")), "", Dr("User_Validasi").ToString), .AutoSize = True}, 10, 2)
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Fetch_LvDetailBahan()
		Try
			OpenConn()

			LvDetailBahan.Items.Clear()

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

					LvDetailBahan.Items.Add(item)
				End While
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub Fetch_LvDetailKandungan()
		Try
			OpenConn()

			'LvDetailKandungan.Items.Clear()

			'SQL = $"
			'             SELECT Kode_Uji, Satuan, Hasil
			'             FROM EMI_Transaksi_Formulator_Detail_Kandungan
			'             WHERE Kode_Perusahaan = '{KodePerusahaan}'
			'             AND No_Faktur = '{NoFaktur}'
			'         "

			'Using Dr = OpenTrans(SQL)
			'	While Dr.Read
			'		Dim item As New ListViewItem(Dr("Kode_Uji").ToString)

			'		item.SubItems.Add(If(IsDBNull(Dr("Satuan")), "", Dr("Satuan").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Hasil")), "", Dr("Hasil").ToString))

			'		LvDetailKandungan.Items.Add(item)
			'	End While
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub Fetch_LvDetailStep()
		Try
			OpenConn()

			'LvDetailStep.Items.Clear()

			'SQL = $"
			'             SELECT No_Step, Kode, Deskripsi, Jumlah, Satuan, Persentase
			'             FROM EMI_Transaksi_Formulator_Detail_Step
			'             WHERE Kode_Perusahaan = '{KodePerusahaan}'
			'             AND No_Faktur = '{NoFaktur}'
			'         "

			'Using Dr = OpenTrans(SQL)
			'	While Dr.Read
			'		Dim item As New ListViewItem(Dr("No_Step").ToString)

			'		item.SubItems.Add(If(IsDBNull(Dr("Kode")), "", Dr("Kode").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Deskripsi")), "", Dr("Deskripsi").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Jumlah")), "0", FormatNumber(Dr("Jumlah"), 2)))
			'		item.SubItems.Add(If(IsDBNull(Dr("Persentase")), "0", FormatNumber(Dr("Persentase"), 2)))

			'		LvDetailStep.Items.Add(item)
			'	End While
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub Fetch_LvDetailMoistureContent()
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
			Exit Sub
		End Try
	End Sub

	Private Sub BtnSimpanSudahBinding_Click(sender As Object, e As EventArgs) Handles BtnSimpanSudahBinding.Click
		Me.Close()
	End Sub

End Class