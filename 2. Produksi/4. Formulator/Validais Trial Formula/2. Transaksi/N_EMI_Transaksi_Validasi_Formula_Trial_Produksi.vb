Imports System.IO
Imports System.Net

Public Class N_EMI_Transaksi_Validasi_Formula_Trial_Produksi
	Dim arrcari As New ArrayList
	Dim Jenis = "ETA"

	Dim ValueBarcode As String = ""
	Public Property filter_tambahan As String
	Public Property asal As String

	Dim Dgv_NoSPlit, Dgv_KodeBarang, Dgv_NamaBarang, Dgv_KodeFormula, Dgv_Hasil, Dgv_Satuan, Dgv_TanggalFormula, Dgv_Validasi

	Dim Cell_NoSplit As Integer = 0
	Dim Cell_KodeFormula As Integer = 1
	Dim Cell_KodeBarang As Integer = 2
	Dim Cell_NamaBarang As Integer = 3
	Dim Cell_Hasil As Integer = 4
	Dim Cell_Satuan As Integer = 5
	Dim Cell_TanggalFormula As Integer = 6
	Dim Cell_TanggalSplit As Integer = 7
	Dim Cell_Statusfinalisasi As Integer = 8
	Dim Cell_BtnValidasi As Integer = 9

	Private Sub Popup_Timbang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Popup_Timbang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Try
			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			cmb_Formulator.Items.Add("No Split") : arrcari.Add("No Split")
			cmb_Formulator.Items.Add("Kode Formula") : arrcari.Add("Kode Formula")
			cmb_Formulator.Items.Add("Tanggal Formula") : arrcari.Add("Tanggal Formula")
			cmb_Formulator.Items.Add("Kode Barang") : arrcari.Add("Kode Barang")
			cmb_Formulator.Items.Add("Nama Produk") : arrcari.Add("Nama Produk")

			'Menangkap semua inputan dari keyboard
			Me.KeyPreview = True

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Pnl_Filter_Date.Location = New Point(223, 67)

		kosong()

	End Sub

	Public Sub kosong()
		TxtValPencarian.Clear()
		cmb_Formulator.SelectedIndex = -1

		Btn_Scan.Location = New Point(420, 64)
		Btn_Refresh.Location = New Point(520, 64)

		TxtValPencarian.Visible = True
		Pnl_Filter_Date.Visible = False

		DateTimePicker1.Value = Date.Now
		DateTimePicker2.Value = Date.Now

		get_data_formulator("T")
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		kosong()
	End Sub

	Private Sub get_data_formulator(ByRef cari As String)

		Try
			OpenConn()

			Dgv_Parent.Rows.Clear()

			SQL = "
				SELECT 
					a.No_Faktur AS Kode_Formula,
					a.Kode_Barang,
					d.Nama AS Nama_Produk,
					a.Hasil,
					a.Satuan_Hasil,
					FORMAT(a.Tanggal, 'dd MMM yyyy') AS Tanggal_Formula,
					c.No_Transaksi AS No_Split,
					FORMAT(c.Tanggal, 'dd MMM yyyy') AS Tanggal_Split,

					CASE 
						WHEN e.Total_Data > 0 
							THEN 'Sudah Finalisasi'
						ELSE 'Belum Finalisasi'
					END AS Status_Finalisasi,

					'Validasi' AS Validasi

				FROM EMI_Transaksi_Formulator a

				JOIN EMI_Order_Produksi b 
					ON a.Kode_Perusahaan = b.Kode_Perusahaan 
					AND a.No_Faktur = b.Kode_Formula

				OUTER APPLY (
					SELECT TOP 1 *
					FROM Emi_Split_Production_Order x
					WHERE x.Kode_Perusahaan = b.Kode_Perusahaan
					  AND x.No_PO = b.No_Faktur
					ORDER BY x.Tanggal DESC, x.Jam DESC
				) c

				JOIN Barang d 
					ON d.Kode_Barang_Inq = a.Kode_Barang 
					AND d.Kode_Stock_Owner = a.Kode_Stock_Owner

				OUTER APPLY (
					select count(*) as Total_Data from N_EMI_LAB_Hasil_Uji_Validasi_Final z
					join N_EMI_LAB_PO_Sampel y on y.No_Po = z.No_Po
					where z.No_Split_Po = c.No_Transaksi and y.Kode_Perusahaan = a.Kode_Perusahaan and y.Flag_Selesai = 'Y'
				) e

				WHERE 
					a.Flag_Lanjut_Trial_Produksi = 'Y'
					AND a.Flag_Selesai_Trial_Produksi IS NULL
					AND a.Status IS NULL
					AND a.Status IS NULL
					AND c.No_Transaksi IS NOT NULL
			"

			If cari = "Y" Then

				If arrcari(cmb_Formulator.SelectedIndex).ToString.Trim = "Tanggal Formula" Then

					SQL &= $"
					AND a.Tanggal BETWEEN 
						'{Format(DateTimePicker1.Value, "yyyy-MM-dd")}'
						AND '{Format(DateTimePicker2.Value, "yyyy-MM-dd")}'
				"

				Else

					Dim fieldCari As String = arrcari.Item(cmb_Formulator.SelectedIndex)

					Select Case fieldCari

						Case "No Split"
							SQL &= $" AND c.No_Transaksi LIKE '%{TxtValPencarian.Text.Trim}%' "

						Case "Kode Formula"
							SQL &= $" AND a.No_Faktur LIKE '%{TxtValPencarian.Text.Trim}%' "

						Case "Kode Barang"
							SQL &= $" AND a.Kode_Barang LIKE '%{TxtValPencarian.Text.Trim}%' "

						Case "Nama Produk"
							SQL &= $" AND d.Nama LIKE '%{TxtValPencarian.Text.Trim}%' "

						Case "Status Finalisasi"
							SQL &= $"
								AND (
									CASE 
										WHEN e.Flag_Selesai = 'Y' THEN 'Sudah Finalisasi' 
										ELSE 'Belum Finalisasi' 
									END
								) LIKE '%{TxtValPencarian.Text.Trim}%'
							"
					End Select

				End If

			End If

			SQL &= "
				ORDER BY 
					c.Tanggal DESC,
					c.Jam DESC
			"

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					With Dgv_Parent.Rows(Dgv_Parent.Rows.Add)

						.Cells(Cell_NoSplit).Value = Dr("No_Split")
						.Cells(Cell_KodeFormula).Value = Dr("Kode_Formula")
						.Cells(Cell_KodeBarang).Value = Dr("Kode_Barang")
						.Cells(Cell_NamaBarang).Value = Dr("Nama_Produk")
						.Cells(Cell_Hasil).Value = Dr("Hasil")
						.Cells(Cell_Satuan).Value = Dr("Satuan_Hasil")
						.Cells(Cell_TanggalFormula).Value = Dr("Tanggal_Formula")
						.Cells(Cell_TanggalSplit).Value = Dr("Tanggal_Split")
						.Cells(Cell_Statusfinalisasi).Value = Dr("Status_Finalisasi")
						.Cells(Cell_BtnValidasi).Value = Dr("Validasi")
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

	Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click

		If cmb_Formulator.SelectedIndex = -1 Then
			MessageBox.Show("Silahkan pilih parameter terlebih dahulu")
			cmb_Formulator.SelectedIndex = 0
			Exit Sub

		End If

		If arrcari(cmb_Formulator.SelectedIndex).ToString.Trim = "TANGGAL_FORMULA" Then
			If DateTimePicker1.Value.Date > DateTimePicker2.Value.Date Then
				MessageBox.Show("Tanggal awal tidak boleh melewati tanggal akhir!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				DateTimePicker1.Value = Date.Now
				DateTimePicker2.Value = Date.Now
				Exit Sub
			End If
		Else
			If TxtValPencarian.Text.Trim.Length = 0 Then
				MessageBox.Show("Harap Isi Value Filter Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				TxtValPencarian.Focus()
				Exit Sub
			End If
		End If

		get_data_formulator("Y")
	End Sub

	Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
		Dgv_NoSPlit = Dgv_Parent.Rows(NoIndex).Cells(Cell_NoSplit).Value
		Dgv_KodeBarang = Dgv_Parent.Rows(NoIndex).Cells(Cell_KodeBarang).Value
		Dgv_NamaBarang = Dgv_Parent.Rows(NoIndex).Cells(Cell_NamaBarang).Value
		Dgv_KodeFormula = Dgv_Parent.Rows(NoIndex).Cells(Cell_KodeFormula).Value
		Dgv_Hasil = Dgv_Parent.Rows(NoIndex).Cells(Cell_Hasil).Value
		Dgv_Satuan = Dgv_Parent.Rows(NoIndex).Cells(Cell_Satuan).Value
		Dgv_TanggalFormula = Dgv_Parent.Rows(NoIndex).Cells(Cell_TanggalFormula).Value
		Dgv_Validasi = Dgv_Parent.Rows(NoIndex).Cells(Cell_BtnValidasi).Value
	End Sub

	Private Sub cmb_Formulator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Formulator.SelectedIndexChanged
		If cmb_Formulator.Items.Count = 0 Or cmb_Formulator.SelectedIndex = -1 Then Exit Sub

		If arrcari(cmb_Formulator.SelectedIndex).ToString.Trim = "TANGGAL_FORMULA" Then
			Btn_Scan.Location = New Point(666, 64)
			Btn_Refresh.Location = New Point(766, 64)

			TxtValPencarian.Visible = False
			Pnl_Filter_Date.Visible = True
		Else

			Btn_Scan.Location = New Point(420, 64)
			Btn_Refresh.Location = New Point(520, 64)

			TxtValPencarian.Visible = True
			Pnl_Filter_Date.Visible = False
		End If

		TxtValPencarian.Clear()
		DateTimePicker1.Value = Date.Now
		DateTimePicker2.Value = Date.Now
	End Sub

	Private Sub Dgv_Formula_Parent_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		If e.ColumnIndex = Cell_BtnValidasi Then

			If Dgv_Parent.CurrentRow.Cells(Cell_Statusfinalisasi).Value.ToString <> "Sudah Finalisasi" Then
				MessageBox.Show("Validasi hanya dapat dilakukan jika data sudah finalisasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End If

			Get_Isi_ListView(Dgv_Parent.CurrentRow.Index)

			If (MessageBox.Show($"Yakin ingin Melakukan Validasi Split {Dgv_NoSPlit} ini ???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

			get_jam()

			Dim listSatuan As New List(Of String)

			Try
				OpenConn()

				listSatuan.Clear()

				SQL = "select Satuan from EMI_Satuan where Flag_Tampil_Berat='Y' "
				SQL &= "and kode_perusahaan = '" & KodePerusahaan & "' "

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

			With N_EMI_SD_Transaksi_Validasi_Formula_Trial_Produksi
				.Txt_NoFormula.Text = Dgv_KodeFormula
				.Txt_Kd_Barang.Text = Dgv_KodeBarang
				.Txt_NmBarang.Text = Dgv_NamaBarang
				.Txt_Hasil.Text = Format(Val(HilangkanTanda(Dgv_Hasil)), "N4")

				.Cmb_Satuan.Items.Clear()
				.Cmb_Satuan.Items.AddRange(listSatuan.ToArray)
				.Cmb_Satuan.SelectedItem = Dgv_Satuan

				.ShowDialog()
			End With

			kosong()
		End If
	End Sub

	Private Sub Dgv_Formula_Parent_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellDoubleClick
		If e.RowIndex < 0 Then Exit Sub

		Dim no_split As String = Dgv_Parent.Rows(e.RowIndex).Cells(Cell_NoSplit).Value.ToString()
		Dim no_formula As String = Dgv_Parent.Rows(e.RowIndex).Cells(Cell_KodeFormula).Value.ToString()
		Dim namaFormula As String = ""
		Dim kategoriProduk As String = ""
		Dim tanggalUji As String = ""
		Dim tanggalValidasi As String = ""
		Dim ketFormula As String = ""
		Dim cookingStep As String = ""
		Dim bahan As New List(Of Dictionary(Of String, Object))
		Dim hpp As New Dictionary(Of String, Object)

		'Get data untuk laporan
		Try
			OpenConn()

			SQL = $"
                SELECT TOP 1 Cooking_Step 
                FROM Emi_Transaksi_Formulator_Cooking_Steps
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                  AND No_Faktur = '{no_formula}'
                  AND Status IS NULL
                  AND Flag_Trial_Produksi = 'Y'
                  AND Flag_Trial_Kitchen IS NULL
                ORDER BY Urut_Oto DESC
            "
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					cookingStep = Dr("Cooking_Step").ToString()
				End If
			End Using

			SQL = $"SELECT Keterangan FROM Emi_Split_Production_Order WHERE No_Transaksi = '{no_split}'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					ketFormula = Dr("Keterangan").ToString()
				End If
			End Using

			SQL = $"select c.No_Faktur as kode_formula, b.Nama as nama_produk, format(a.Tanggal, 'dd MMM yyyy') as tanggal_uji, FORMAT(CONVERT(DATETIME, CONVERT(VARCHAR(10), c.Tanggal_Selesai_Trial_Produksi, 120) + ' ' + c.Jam_Selesai_Trial_Produksi, 120), 'dd MMMM yyyy, HH:mm') AS tanggal_validasi
				from N_EMI_LAB_PO_Sampel a
				cross apply (
					select top 1 *
					from Barang b
					where a.Kode_Perusahaan = b.Kode_Perusahaan
					and a.Kode_Barang = b.Kode_Barang
					order by b.Kode_Stock_Owner
				) b
				join Emi_Transaksi_Formulator c 
					on c.Kode_Perusahaan = a.Kode_Perusahaan 
					and c.Kode_Barang = b.Kode_Barang_Inq
					and c.Status is null
					and c.No_Faktur = '{no_formula}'
				where a.No_Split_Po = '{no_split}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					namaFormula = Dr("kode_formula").ToString()
					kategoriProduk = Dr("nama_produk").ToString()
					tanggalUji = Dr("tanggal_uji").ToString()
					tanggalValidasi = Dr("tanggal_validasi").ToString()
				End If
			End Using

			SQL = $"
                ;WITH cte_hpp_sn AS (
					SELECT 
						kode_barang,
						dbo.get_hpp(serial_number) AS hpp_val,
						ROW_NUMBER() OVER (
							PARTITION BY kode_barang 
							ORDER BY Tgl_masuk DESC
						) AS rn
					FROM barang_sn
					WHERE blok_sn IS NULL
					  AND dbo.get_hpp(serial_number) <> 0
				),
				cte_bahan AS (
					SELECT 
						a.Kode_Barang,
						a.Kode_Bahan,
						a.Jumlah_Barang,
						ISNULL(h.hpp_val, b.estimasi_harga) 
							/ NULLIF(a.Jumlah_Barang, 0) AS hpp
					FROM Barang_Detail_Bahan_Penolong a
					INNER JOIN Barang b
						ON a.Kode_Bahan = b.Kode_Barang
					LEFT JOIN cte_hpp_sn h
						ON h.kode_barang = a.kode_bahan
					   AND h.rn = 1
					GROUP BY 
						a.Kode_Bahan, 
						a.Kode_Barang, 
						a.Jumlah_Barang, 
						b.Estimasi_Harga,
						h.hpp_val
				),
				cte_faktur_ranked AS (
					SELECT 
						Kode_Perusahaan,
						Jenis_Biaya,
						No_Faktur,
						ROW_NUMBER() OVER (
							PARTITION BY Kode_Perusahaan, Jenis_Biaya 
							ORDER BY Id DESC
						) AS rn
					FROM Emi_Transaksi_Work_Center
					WHERE Status IS NULL
				),
				cte_wc AS (
					SELECT 
						a.Kode_Perusahaan,
						a.Id_Jenis_Biaya_Produksi,
						a.Kode_Jenis_Biaya_Produksi,
						f.No_Faktur AS Faktur_WC
					FROM Emi_Jenis_Biaya_Produksi a
					LEFT JOIN cte_faktur_ranked f
						ON f.Kode_Perusahaan = a.Kode_Perusahaan
					   AND f.Jenis_Biaya      = a.Kode_Jenis_Biaya_Produksi
					   AND f.rn = 1
				),
				cte_produksi AS (
					SELECT 
						c.Id_Routing,
						a.Kode_Jenis_Biaya_Produksi,
						c.Id_Work_Center,
						MAX(c.Nilai_Per_Pcs) AS Nilai_Per_Kg
					FROM cte_wc a
					JOIN Emi_Transaksi_Work_Center b
						ON a.Kode_Perusahaan = b.Kode_Perusahaan
					   AND a.Faktur_WC       = b.No_Faktur
					JOIN Emi_Transaksi_Work_Center_Detail c
						ON b.Kode_Perusahaan = c.Kode_Perusahaan
					   AND b.No_Faktur       = c.No_Faktur
					GROUP BY 
						c.Id_Routing,
						c.Id_Work_Center,
						a.Kode_Jenis_Biaya_Produksi
				),
				cte_packaging_agg AS (
					SELECT 
						Kode_Barang,
						SUM(hpp) AS total_hpp
					FROM cte_bahan
					GROUP BY Kode_Barang
				),
				cte_produksi_agg AS (
					SELECT 
						Id_Routing,
						SUM(Nilai_Per_Kg) AS total_nilai
					FROM cte_produksi
					GROUP BY Id_Routing
				)
				SELECT 
					SUM(ISNULL(c.Est_HPP_Per_Pcs, 0))              AS HPP_Bahan_Baku,
					ISNULL(pkg.total_hpp, 0)                        AS HPP_Packaging,
					ISNULL(prod.total_nilai / 1000.0 * d.Berat, 0) AS HPP_Produksi,
					'Per ' + d.Satuan                               AS Satuan
				FROM Emi_Transaksi_Formulator b
				INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c
					ON b.Kode_Perusahaan = c.Kode_Perusahaan
				   AND b.No_Faktur       = c.No_Faktur
				INNER JOIN Barang d
					ON b.Kode_Perusahaan  = d.Kode_Perusahaan
				   AND b.Kode_Stock_Owner = d.Kode_Stock_Owner
				   AND b.Kode_Barang      = d.Kode_Barang_inq
				LEFT JOIN cte_packaging_agg pkg
					ON pkg.Kode_Barang = b.Kode_Barang
				LEFT JOIN cte_produksi_agg prod
					ON prod.Id_Routing = d.Id_Routing
				WHERE b.Kode_Perusahaan = '{KodePerusahaan}'
				  AND b.No_Faktur       = '{no_formula}'
				GROUP BY 
					b.No_Faktur,
					d.Nama,
					b.Tanggal,
					d.Satuan,
					d.Berat,
					d.Id_Routing,
					b.Kode_Barang,
					pkg.total_hpp,
					prod.total_nilai;
            "
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					hpp("hpp_bahan_baku") = Dr("HPP_Bahan_Baku")
					hpp("hpp_packaging") = Dr("HPP_Packaging")
					hpp("hpp_produksi") = Dr("HPP_Produksi")
					hpp("satuan") = Dr("Satuan")
				End If
			End Using

			SQL = $"
                    SELECT
                        e.Nama AS Nama_Bahan,
                        c.Jumlah,
                        c.Persentase,
                        ISNULL(c.Est_HPP, 0) AS Est_HPP,
                        ISNULL(c.Est_HPP_Per_Pcs, 0) AS Est_HPP_Per_Pcs

                    FROM Emi_Transaksi_Formulator b 

                    INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                        AND b.No_Faktur = c.No_Faktur

                    INNER JOIN Barang d
                        ON b.Kode_Perusahaan = d.Kode_Perusahaan
                        AND b.Kode_Stock_Owner = d.Kode_Stock_Owner
                        AND b.Kode_Barang = d.Kode_Barang_Inq

                    INNER JOIN Barang e
                        ON c.Kode_Perusahaan = e.Kode_Perusahaan
                        AND c.Kode_Stock_Owner = e.Kode_Stock_Owner
                        AND c.Kode_Barang = e.Kode_Barang

                    WHERE
                        b.Status IS NULL
                        AND b.Kode_Perusahaan = '{KodePerusahaan}'
                        AND b.No_Faktur = '{no_formula}'
            "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read()
					bahan.Add(New Dictionary(Of String, Object) From {
					{"Nama_bahan", Dr("Nama_bahan")},
					{"Jumlah", Dr("Jumlah")},
					{"Persentase", Dr("Persentase")},
					{"Est_HPP_Per_Pcs", Dr("Est_HPP_Per_Pcs")},
					{"Est_HPP", Dr("Est_HPP")}
				})
				Loop
			End Using

			CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show($"Gagal mendapatkan data laporan dengan No. Split {no_split}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

		'Hit API untuk mendapatkan file PDF laporan
		Try
			Me.Cursor = Cursors.WaitCursor
			Application.DoEvents()

			Dim payload As New Dictionary(Of String, Object) From {
				{"no_split", no_split},
				{"nama_formula", namaFormula},
				{"kategori_produk", kategoriProduk},
				{"tanggal_uji", tanggalUji},
				{"tanggal_validasi", tanggalValidasi},
				{"keterangan", ketFormula},
				{"cooking_step", cookingStep},
				{"bahan", bahan},
				{"hpp", hpp}
			}

			Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
			Dim headers As New Dictionary(Of String, String) From {{"X-Signature", GenerateHmac(json, Secret_Api_Laporan_Formulator)}}
			Dim pdfStream As MemoryStream = Helper_API.CallAPIFile(Url_Api_Laporan_Formulator_Trial_Produksi, "POST", payload, headers)

			If pdfStream Is Nothing OrElse pdfStream.Length = 0 Then
				Throw New Exception("PDF stream kosong")
			End If

			Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

			Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
				pdfStream.CopyTo(file)
			End Using

			Dim frm As New N_EMI_PDF_Viewer(tempPath, "Laporan Formula Trial Produksi")
			frm.ShowDialog()

			Me.Cursor = Cursors.Default
		Catch ex As Exception
			Me.Cursor = Cursors.Default
			MessageBox.Show(
				$"Gagal mendapatkan laporan dengan No. Split {no_split}" & Environment.NewLine &
				ex.Message,
				Judul,
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning
			)
		End Try
	End Sub

	Private Sub Dgv_Formula_Parent_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_Parent.CellFormatting
		Try
			If e.RowIndex < 0 OrElse e.Value Is Nothing Then Exit Sub

			If e.ColumnIndex = Cell_Hasil Then

				Dim jumlah As Double = Val(HilangkanTanda(e.Value.ToString()))
				Dim satuan As String = Dgv_Parent.Rows(e.RowIndex).Cells(Cell_Satuan).Value?.ToString()

				e.Value = $"{Format(jumlah, "N0")} {satuan}"
				e.FormattingApplied = True

			End If
		Catch ex As Exception
			MessageBox.Show("Error di CellFormatting " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
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

	Private Sub Dgv_Formula_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Parent.MouseMove
		Dim info As DataGridView.HitTestInfo = Dgv_Parent.HitTest(e.X, e.Y)

		If info.RowIndex >= 0 AndAlso info.ColumnIndex >= 0 Then
			Dgv_Parent.Cursor = Cursors.Hand
		Else
			Dgv_Parent.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Dgv_Formula_Parent_MouseLeave(sender As Object, e As EventArgs) Handles Dgv_Parent.MouseLeave
		Dgv_Parent.Cursor = Cursors.Default
	End Sub
End Class