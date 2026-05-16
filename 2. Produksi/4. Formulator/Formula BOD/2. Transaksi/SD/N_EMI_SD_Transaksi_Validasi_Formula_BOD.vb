Imports System.IO
Imports System.Net

Public Class N_EMI_SD_Transaksi_Validasi_Formula_BOD
	Public Property No_Faktur As String

	Dim lvNo As String
	Dim lvTipe As String
	Dim lvKdBarang As String
	Dim lvNama As String
	Dim lvQty As String
	Dim lvSatuan As String
	Dim lvPengali As String
	Dim lvSatuanBarang As String
	Dim lvNilaiBarang As String
	Dim lvPersentase As String
	Dim lvKet As String
	Dim lvQty_SatHasil As String
	Dim lvSatHasil As String
	Dim lvEstHPP As String
	Dim lvEstHPPPcs As String

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

	Dim cellNo As Integer = 0
	Public cellKdBarang As Integer = 1
	Public cellNama As Integer = 2
	Public Property cellQty As Integer = 3
	Public cellSatuan As Integer = 4
	Public cellPengali As Integer = 5
	Public cellSatuanBarang As Integer = 6
	Public cellNilaiBarang As Integer = 7
	Public Property cellPersentase As Integer = 8
	Dim cellKet As Integer = 9
	Public cellQty_SatHasil As Integer = 10
	Dim cellSatHasil As Integer = 11
	Dim cellEstHPP As Integer = 12
	Dim cellEstHPPPcs As Integer = 13

	Private Sub N_EMI_SD_Transaksi_Validasi_Formula_BOD_Load(sender As Object, e As EventArgs) Handles Me.Load
		TxtFormulator_NoFaktur.Text = No_Faktur

		If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show("Gagal Memuat No Faktur", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
			Exit Sub
		Else
		End If

		Load_Detail()
	End Sub

	Private Sub Load_Detail()
		Try
			OpenConn()

			SQL = $"
                SELECT a.Nama, a.Id_Karyawan
                FROM Emi_Karyawan a
                INNER JOIN Emi_Transaksi_Formulator b ON a.Id_Karyawan = b.Penanggung_Jawab
                WHERE b.Kode_Perusahaan = '{KodePerusahaan}' AND b.No_Faktur = '{No_Faktur}'
            "

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					CmbFormulator_PenanggungJawab.Items.Clear()
					CmbFormulator_PenanggungJawab.Items.Add(Dr("Nama"))
					CmbFormulator_PenanggungJawab.SelectedIndex = 0
				End If
			End Using

			Dim Nomor As Integer = 1
			SQL = $"
                SELECT
                    a.No_Faktur, a.Tanggal, a.Lokasi, a.Kode_Stock_Owner, a.Kode_Barang,
                    b.Nama as Nama_Barang, a.UserID, a.Hasil, a.Satuan_Hasil, a.Penanggung_Jawab,
                    c.Kode_Barang as Kode_Bahan, d.Nama as Nama_Bahan, c.Jumlah, c.satuan,
                    c.Nilai_Pengali, c.Satuan_barang, c.Nilai_Barang, c.Persentase,
                    CASE WHEN EXISTS (
                        SELECT 1 FROM Barang_SN z
                        WHERE c.kode_barang = z.kode_barang AND z.blok_sn IS NULL AND dbo.get_hpp(z.serial_number) <> 0
                    ) THEN (
                        SELECT TOP 1 dbo.get_hpp(z.serial_number) FROM Barang_SN z
                        WHERE c.kode_barang = z.kode_barang AND z.blok_sn IS NULL AND dbo.get_hpp(z.serial_number) <> 0
                        ORDER BY z.tgl_masuk DESC
                    ) ELSE d.estimasi_harga END AS Est_HPP,
                    CASE WHEN EXISTS (
                        SELECT 1 FROM Barang_SN z
                        WHERE c.kode_barang = z.kode_barang AND z.blok_sn IS NULL AND dbo.get_hpp(z.serial_number) <> 0
                    ) THEN ISNULL(
                        dbo.ubah_satuan(a.kode_perusahaan, 'masa', c.kode_barang, 'gram', d.satuan, (b.berat * (c.persentase / 100)))
                        * (SELECT TOP 1 dbo.get_hpp(z.serial_number) FROM Barang_SN z
                           WHERE c.kode_barang = z.kode_barang AND z.blok_sn IS NULL AND dbo.get_hpp(z.serial_number) <> 0
                           ORDER BY z.tgl_masuk DESC), 0)
                    ELSE ISNULL(
                        dbo.ubah_satuan(a.kode_perusahaan, 'masa', c.kode_barang, 'gram', d.satuan, (b.berat * (c.persentase / 100)))
                        * d.estimasi_harga, 0)
                    END AS Est_HPP_Pcs
                FROM Emi_Transaksi_Formulator a
                INNER JOIN Barang b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Kode_Stock_Owner = b.Kode_Stock_Owner AND a.Kode_Barang = b.Kode_Barang_inq
                INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c ON a.Kode_Perusahaan = c.Kode_Perusahaan AND a.No_Faktur = c.No_Faktur
                INNER JOIN Barang d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Stock_Owner = d.Kode_Stock_Owner AND c.Kode_Barang = d.Kode_Barang
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
                AND a.Status IS NULL
                AND a.No_Faktur = '{No_Faktur}'
                ORDER BY d.nama
            "

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count = 0 Then
						CloseConn()
						MessageBox.Show("Detail Bahan Formula Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Me.Close()
						Exit Sub
					End If

					For i As Integer = 0 To .Rows.Count - 1
						Dim row = .Rows(i)

						DtpFormulator_Tanggal.Value = row("Tanggal")
						TxtFormulator_KodeBarang.Text = row("Kode_Barang")
						TxtFormulator_NamaBarang.Text = row("Nama_Barang")
						TxtFormulator_Hasil.Text = Format(row("Hasil"), "N4")

						Dim lokasi As String = row("Lokasi").ToString.Trim
						Dim lokasiBarang As String = row("Kode_Stock_Owner").ToString.Trim
						Dim satuan As String = row("Satuan_Hasil").ToString.Trim

						If Not CmbFormulator_LokasiInquiry.Items.Contains(lokasi) Then
							CmbFormulator_LokasiInquiry.Items.Add(lokasi)
						End If

						If Not CmbFormulator_LokasiBarang.Items.Contains(lokasiBarang) Then
							CmbFormulator_LokasiBarang.Items.Add(lokasiBarang)
						End If

						If Not CmbFormulator_SatuanHasil.Items.Contains(satuan) Then
							CmbFormulator_SatuanHasil.Items.Add(satuan)
						End If

						CmbFormulator_LokasiInquiry.SelectedItem = lokasi
						CmbFormulator_LokasiBarang.SelectedItem = lokasiBarang
						CmbFormulator_SatuanHasil.SelectedItem = satuan

						DgvFormulator_StepFormulator.Rows.Add(1)
						With DgvFormulator_StepFormulator.Rows(i).Cells
							.Item(cellNo).Value = Nomor
							.Item(cellKdBarang).Value = row("Kode_Bahan")
							.Item(cellNama).Value = row("Nama_Bahan")
							.Item(cellQty).Value = Format(row("Jumlah"), "N4")
							.Item(cellSatuan).Value = row("satuan")
							.Item(cellPengali).Value = Format(row("Nilai_Pengali"), "N4")
							.Item(cellSatuanBarang).Value = row("Satuan_barang")
							.Item(cellNilaiBarang).Value = Format(row("Nilai_Barang"), "N4")
							.Item(cellPersentase).Value = Format(row("Persentase"), "N2")
							.Item(cellEstHPP).Value = If(General_Class.CekNULL(row("Est_HPP")) = "", 0, Format(row("Est_HPP"), "N2"))
							.Item(cellEstHPPPcs).Value = If(General_Class.CekNULL(row("Est_HPP_Pcs")) = "", 0, Format(row("Est_HPP_Pcs"), "N2"))
						End With

						Nomor += 1
					Next
				End With
			End Using

			Dgv_Moisture_Content.Rows.Clear()
			SQL = $"
                SELECT
                    b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab,
                    '-' AS Value_Combobox, a.Range_Awal, a.Range_Akhir
                FROM N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
                INNER JOIN N_EMI_LAB_Jenis_Analisa b ON a.Id_Jenis_Analisa = b.id
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}' AND a.No_Formula = '{No_Faktur}'
                UNION ALL
                SELECT
                    b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab,
                    c.label_keterangan AS Value_Combobox, '' AS Range_Awal, '' AS Range_Akhir
                FROM N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
                INNER JOIN N_EMI_LAB_Jenis_Analisa b ON a.Id_Jenis_Analisa = b.id
                INNER JOIN EMI_Switch c ON a.Kode_Perusahaan = c.kode_perusahaan AND a.nilai_kriteria = c.keterangan
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}' AND a.No_Formula = '{No_Faktur}'
            "

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						Dim row = .Rows(i)
						Dim flagPerhitungan As String = If(General_Class.CekNULL(row("Flag_Perhitungan")) = "", "T", row("Flag_Perhitungan"))

						Dgv_Moisture_Content.Rows.Add()

						Dim cmb As DataGridViewComboBoxCell = CType(Dgv_Moisture_Content.Rows(i).Cells(6), DataGridViewComboBoxCell)

						cmb.Items.Clear()
						cmb.Items.Add("-")

						If row("Value_Combobox").ToString.Trim <> "-" Then
							cmb.Items.Add(row("Value_Combobox").ToString.Trim)
						End If

						With Dgv_Moisture_Content.Rows(i).Cells
							.Item(Item_Moisture_ID).Value = row("Id")
							.Item(Item_Moisture_Kode_Analisa).Value = row("Kode_Analisa")
							.Item(Item_Moisture_Jenis_Analisa).Value = row("Jenis_Analisa")
							.Item(Item_Moisture_Flag_Perhitungan).Value = flagPerhitungan
							.Item(Item_Moisture_Kode_Aktivitas).Value = row("Kode_Aktivitas_Lab")
							.Item(Item_Moisture_Kategori).Value = If(flagPerhitungan.Trim = "Y", "Perhitungan", "Non Perhitungan")

							Dim val As String = row("Value_Combobox").ToString.Trim
							If cmb.Items.Contains(val) Then
								cmb.Value = val
							Else
								cmb.Value = "-"
							End If

							.Item(Item_Moisture_Range_Awal).Value = row("Range_Awal")
							.Item(Item_Moisture_Range_Akhir).Value = row("Range_Akhir")
						End With
					Next
				End With
			End Using

			RTBCookingStep.Clear()
			SQL = $"
                SELECT TOP 1 Cooking_Step
                FROM Emi_Transaksi_Formulator_Cooking_Steps
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                AND No_Faktur = '{No_Faktur}'
                AND Status IS NULL
                ORDER BY Tanggal DESC, Jam DESC
            "

			Using Dr = OpenTrans(SQL)
				If Dr.Read AndAlso General_Class.CekNULL(Dr("Cooking_Step")) <> "" Then
					RTBCookingStep.Rtf = Dr("Cooking_Step")
				End If
			End Using

			CloseConn()

			Total()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal mendapatkan data: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try
	End Sub

	Private Sub Total()
		Dim Total As Double = 0
		Dim TotalPersen As Double = 0
		Dim Total_Hpp As Double = 0
		Dim Total_Hpp_Pcs As Double = 0

		For index = 0 To DgvFormulator_StepFormulator.Rows.Count - 1
			Get_Isi_Listview(index)

			If IsNumeric(lvQty) = True Then
				Total += Val(HilangkanTanda(lvQty_SatHasil))
				TotalPersen += Val(HilangkanTanda(lvPersentase))
				Total_Hpp += Val(HilangkanTanda(lvEstHPP))
				Total_Hpp_Pcs += Val(HilangkanTanda(lvEstHPPPcs))
			End If
		Next

		TxtFormulator_TotalPersen.Text = Format(TotalPersen, "N2")
		Txt_Total_Hpp_Pcs.Text = Format(Total_Hpp_Pcs, "N2")
	End Sub

	Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
		lvNo = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellNo).Value)
		lvKdBarang = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellKdBarang).Value)
		lvNama = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellNama).Value)
		lvQty = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellQty).Value)
		lvSatuan = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellSatuan).Value)
		lvPengali = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellPengali).Value)
		lvSatuanBarang = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellSatuanBarang).Value)
		lvNilaiBarang = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellNilaiBarang).Value)
		lvPersentase = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellPersentase).Value)
		lvKet = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellKet).Value)
		lvQty_SatHasil = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellQty_SatHasil).Value)
		lvEstHPP = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellEstHPP).Value)
		lvEstHPPPcs = CekNothing(DgvFormulator_StepFormulator.Rows(No_Index).Cells(cellEstHPPPcs).Value)
	End Sub

	Private Function CekNothing(ByVal str As String) As String
		Dim hasil As String = ""

		If str Is Nothing Then
			hasil = ""
		Else
			hasil = str
		End If

		Return hasil
	End Function

	Private Sub Btn_Validasi_Click(sender As Object, e As EventArgs) Handles Btn_Validasi.Click
		Try
			OpenConn()

			If CekButtonRole("Validasi_Formula_BOD") = "T" Then
				MessageBox.Show("Anda tidak memiliki akses untuk memvalidasi formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal memvalidasi role button: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

		Dim result As DialogResult = MessageBox.Show(
			"Apakah Anda yakin ingin memvalidasi formula ini?",
			Judul,
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question
		)

		If result <> DialogResult.Yes Then
			Exit Sub
		End If

		Dim listSatuan As New List(Of String)
		Try
			OpenConn()

			listSatuan.Clear()
			SQL = "select Satuan from EMI_Satuan where Flag_Tampil_Berat='Y' "
			SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					listSatuan.Add(dr("Satuan").ToString())
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		With N_EMI_SD_Transaksi_Validasi_Formula_Hierarki_BOD
			.Txt_NoFormula.Text = TxtFormulator_NoFaktur.Text.Trim
			.Txt_Kd_Barang.Text = TxtFormulator_KodeBarang.Text.Trim
			.Txt_NmBarang.Text = TxtFormulator_NamaBarang.Text.Trim
			.Txt_Hasil.Text = Format(Val(HilangkanTanda(TxtFormulator_Hasil.Text.Trim)), "N4")
			.Cmb_Satuan.Items.Clear()
			.Cmb_Satuan.Items.AddRange(listSatuan.ToArray)
			.Cmb_Satuan.SelectedItem = CmbFormulator_SatuanHasil.Text.Trim
			.ShowDialog()
		End With

		'N_EMI_Dashboard_Formula.Kosong()
		Me.Close()

	End Sub

	Private Sub Btn_Tolak_Click(sender As Object, e As EventArgs) Handles Btn_Tolak.Click
		Try
			OpenConn()

			If CekButtonRole("Tolak_Formula_BOD") = "T" Then
				MessageBox.Show("Anda tidak memiliki akses untuk menolak formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal memvalidasi role button: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

		Dim result As DialogResult = MessageBox.Show(
			"Apakah Anda yakin ingin menolak formula ini?",
			Judul,
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question
		)

		If result <> DialogResult.Yes Then
			Exit Sub
		End If

		get_jam()

		Try
			OpenConn()

			SQL = $"
                UPDATE Emi_Transaksi_Formulator
                SET Flag_Validasi_Formula_Produksi_BOD = 'T', Tanggal_Validasi_Formula_Produksi_BOD = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi_Formula_Produksi_BOD = '{Format(tgl_skg, "HH:mm:ss")}', UserID_Validasi_Formula_Produksi_BOD = '{UserID}'
                WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Faktur = '{No_Faktur}'
            "

			ExecuteTrans(SQL)

			CloseConn()

			MessageBox.Show("Berhasil menolak formula", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

			'Me.Close()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal menolak formula: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

		'N_EMI_Dashboard_Formula.Kosong()
		Me.Close()
	End Sub

	Public Function GetPdfStream(url As String, no_split As String) As MemoryStream
		' ----------------------------------------------------------------
		' 1. Query bahan & hpp dari DB
		' ----------------------------------------------------------------
		Dim bahan As New List(Of Dictionary(Of String, Object))
		Dim hpp As New Dictionary(Of String, Object)
		Dim namaFormula As String = ""
		Dim kategoriProduk As String = ""
		Dim tanggalUji As String = ""

		Try
			OpenConn()

			SQL = $"SELECT TOP 1 kode_formula, nama_produk, FORMAT(tanggal, 'dd MMM yyyy') AS tanggal_uji FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					namaFormula = Dr("kode_formula").ToString()
					kategoriProduk = Dr("nama_produk").ToString()
					tanggalUji = Dr("tanggal_uji").ToString()
				End If
			End Using

			' Query bahan
			SQL = $"SELECT Nama_bahan, Jumlah, Persentase FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}' GROUP BY kode_formula, nama_produk, tanggal, No_Transaksi, Nama_bahan, Jumlah, Persentase"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read()
					bahan.Add(New Dictionary(Of String, Object) From {
					{"Nama_bahan", Dr("Nama_bahan")},
					{"Jumlah", Dr("Jumlah")},
					{"Persentase", Dr("Persentase")}
				})
				Loop
			End Using

			' Query hpp
			SQL = $"SELECT SUM(Est_HPP_Per_Pcs) AS hpp_bahan_baku, HPP_Packaging, HPP_produksi, 'Per ' + satuan AS satuan FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}' GROUP BY kode_formula, nama_produk, tanggal, No_Transaksi, satuan, HPP_Produksi, HPP_Packaging"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					hpp("hpp_bahan_baku") = Dr("hpp_bahan_baku")
					hpp("hpp_packaging") = Dr("hpp_packaging")
					hpp("hpp_produksi") = Dr("hpp_produksi")
					hpp("satuan") = Dr("satuan")
				End If
			End Using
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Return Nothing
		End Try

		' ----------------------------------------------------------------
		' 2. Serialize ke JSON & kirim ke API
		' ----------------------------------------------------------------
		Dim payload As New Dictionary(Of String, Object) From {
			{"no_split", no_split},
			{"nama_formula", namaFormula},
			{"kategori_produk", kategoriProduk},
			{"tanggal_uji", tanggalUji},
			{"bahan", bahan},
			{"hpp", hpp}
		}

		Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
		Dim signature As String = GenerateHmac(json, Secret_Api_Laporan_Formulator)

		Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
		request.Method = "POST"
		request.ContentType = "application/json"
		request.Accept = "*/*"
		request.UserAgent = "Mozilla/5.0"
		request.Headers.Add("X-Signature", signature)

		Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(json)
		request.ContentLength = bytes.Length
		Using stream = request.GetRequestStream()
			stream.Write(bytes, 0, bytes.Length)
		End Using

		Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
		Dim ms As New MemoryStream()
		Using responseStream = response.GetResponseStream()
			responseStream.CopyTo(ms)
		End Using
		ms.Position = 0
		Return ms
	End Function

End Class