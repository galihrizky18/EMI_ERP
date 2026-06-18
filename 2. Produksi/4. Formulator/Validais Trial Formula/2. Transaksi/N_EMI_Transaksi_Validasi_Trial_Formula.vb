Imports System.IO
Imports System.Net
Imports DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing
Public Class N_EMI_Transaksi_Validasi_Trial_Formula
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
	Dim Cell_BtnValidasi As Integer = 8

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

			cmb_Formulator.Items.Add("No Split") : arrcari.Add("no_transaksi")
			cmb_Formulator.Items.Add("Kode Formula") : arrcari.Add("Kode_Formula")
			cmb_Formulator.Items.Add("Tanggal Formula") : arrcari.Add("TANGGAL_FORMULA")
			cmb_Formulator.Items.Add("Kode Barang") : arrcari.Add("kode_barang")
			cmb_Formulator.Items.Add("Nama Produk") : arrcari.Add("Nama_Produk")

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
		cmb_Formulator.SelectedIndex = 0

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

			SQL = "WITH CTE AS ("
			SQL = SQL & "  SELECT U.No_Po_Sampel, "
			SQL = SQL & "  CASE WHEN SUM(CASE WHEN U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) > 0 THEN 1 ELSE 0 END as has_validasi, "
			SQL = SQL & "  CASE "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			SQL = SQL & "  END as status_lock_view, "
			SQL = SQL & "  CASE "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			SQL = SQL & "  END as status_analisa_lab, "
			SQL = SQL & "  CASE "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = 0 THEN 'TIDAK ADA' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'TERKUNCI' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'LCKV' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU LOCK VIEW' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' THEN 1 ELSE 0 END) > SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'ANL' AND U.Flag_Approval IS NOT NULL THEN 1 ELSE 0 END) THEN 'MENUNGGU HASIL UJI LAB' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'T' THEN 1 ELSE 0 END) > 0 THEN 'DITOLAK' "
			SQL = SQL & "    WHEN SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' THEN 1 ELSE 0 END) = SUM(CASE WHEN B.Kode_Aktivitas_Lab = 'PLT' AND U.Flag_Approval = 'Y' THEN 1 ELSE 0 END) THEN 'DISETUJUI' "
			SQL = SQL & "    ELSE 'MENUNGGU VALIDASI' "
			SQL = SQL & "  END as status_palatabilitas "
			SQL = SQL & "  FROM N_EMI_LIMS_Uji_Sampel as U "
			SQL = SQL & "  JOIN N_EMI_LAB_Jenis_Analisa as A ON U.Id_Jenis_Analisa = A.id "
			SQL = SQL & "  JOIN N_EMI_LIMS_Klasifikasi_Aktivitas_Lab as B ON A.Kode_Aktivitas_Lab = B.Kode_Aktivitas_Lab "
			SQL = SQL & "  LEFT JOIN N_EMI_LIMS_Uji_Pra_Final as UPF ON U.No_Po_Sampel = UPF.No_Sampel "
			SQL = SQL & " WHERE "
			SQL = SQL & "     u.Flag_Approval = 'Y' "
			SQL = SQL & "     AND EXISTS ( "
			SQL = SQL & "         SELECT 1 FROM N_EMI_LIMS_Uji_Pra_Final x "
			SQL = SQL & "         WHERE x.No_Sampel = u.No_Po_Sampel "
			SQL = SQL & "     ) "
			SQL = SQL & "     AND u.Flag_Selesai = 'Y' "
			SQL = SQL & "     AND u.Flag_Resampling IS NULL "
			SQL = SQL & "     AND u.Status IS NULL "

			SQL = SQL & "  GROUP BY U.No_Po_Sampel "
			SQL = SQL & "), Final_Status AS ( "
			SQL = SQL & "  SELECT b.Kode_Perusahaan, b.No_Transaksi, CTE.status_lock_view, CTE.status_analisa_lab, "
			SQL = SQL & "  c.Kode_Formula, d.Tanggal, d.Jam, d.Kode_Barang, e.Nama AS Nama_Produk, d.Hasil, d.Satuan_Hasil, "
			SQL = SQL & "  SUM(CASE WHEN ISNULL(cte.status_lock_view, '') <> 'DISETUJUI' OR ISNULL(Cte.status_analisa_lab, '') <> 'DISETUJUI' THEN 1 ELSE 0 END) "
			SQL = SQL & "  OVER(PARTITION BY a.No_Split_Po) as Tidak_Disetujui, b.Jumlah as Jumlah_Split, b.Satuan as Satuan_Split, b.tanggal as Tanggal_Split "
			SQL = SQL & "  FROM CTE "
			SQL = SQL & "  JOIN N_LIMS_PO_Sampel a ON a.No_Sampel = CTE.No_Po_Sampel "
			SQL = SQL & "  JOIN N_EMI_Transaksi_Trial_Split_Production_Order b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Split_Po = b.No_Transaksi "
			SQL = SQL & "  JOIN N_EMI_Transaksi_Trial_Order_Produksi c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_PO = c.No_Faktur "
			SQL = SQL & "  JOIN Emi_Transaksi_Formulator d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.Kode_Formula = d.No_Faktur "
			SQL = SQL & "  JOIN barang e ON e.Kode_Perusahaan = d.Kode_Perusahaan AND e.Kode_Stock_Owner = d.Kode_Stock_Owner AND e.Kode_Barang_inq = d.Kode_Barang "
			SQL = SQL & "  WHERE a.Status IS NULL AND b.Status IS NULL AND b.Flag_Validasi IS NULL AND c.Status IS NULL AND d.Status IS NULL "
			SQL = SQL & ") "
			SQL = SQL & "SELECT Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, Jumlah_Split, Satuan_Split, Tanggal_Split "
			SQL = SQL & "FROM Final_Status WHERE Tidak_Disetujui = 0 "
			If cari = "Y" Then

				If arrcari(cmb_Formulator.SelectedIndex).ToString.Trim = "TANGGAL_FORMULA" Then

					SQL = SQL & "and Tanggal between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
				Else
					SQL = SQL & "and  " & arrcari.Item(cmb_Formulator.SelectedIndex) & " like '%" & TxtValPencarian.Text.Trim & "%' "
				End If

			End If
			SQL = SQL & "GROUP BY Kode_Perusahaan, No_Transaksi, status_lock_view, status_analisa_lab, Kode_Formula, Tanggal, Jam, Kode_Barang, Nama_Produk, Hasil, Satuan_Hasil, Jumlah_Split, Satuan_Split, Tanggal_Split "

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					With Dgv_Parent.Rows(Dgv_Parent.Rows.Add)
						.Cells(Cell_NoSplit).Value = Dr("No_Transaksi")
						.Cells(Cell_KodeBarang).Value = Dr("Kode_Barang")
						.Cells(Cell_NamaBarang).Value = Dr("Nama_Produk")
						.Cells(Cell_KodeFormula).Value = Dr("Kode_Formula")
						.Cells(Cell_Hasil).Value = Dr("Jumlah_Split")
						.Cells(Cell_Satuan).Value = Dr("Satuan_Split")
						.Cells(Cell_TanggalFormula).Value = Format(Dr("Tanggal"), "dd MMM yyyy")
						.Cells(Cell_TanggalSplit).Value = Format(Dr("Tanggal_Split"), "dd MMM yyyy")
						.Cells(Cell_BtnValidasi).Value = "Validasi"
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

		'Me.SuspendLayout()

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

		'Me.ResumeLayout()

		TxtValPencarian.Clear()
		DateTimePicker1.Value = Date.Now
		DateTimePicker2.Value = Date.Now

	End Sub

	Private Sub Dgv_Formula_Parent_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		If e.ColumnIndex = Cell_BtnValidasi Then

			Get_Isi_ListView(Dgv_Parent.CurrentRow.Index)

			If (MessageBox.Show($"Yakin ingin Melakukan Validasi Split {Dgv_NoSPlit} ini ???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

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

			With N_EMI_SD_Transaksi_Validasi_Trial_Formula
				.NoSplit = Dgv_NoSPlit
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

		Dim no_formula As String = Dgv_Parent.Rows(e.RowIndex).Cells(Cell_KodeFormula).Value.ToString()
		Dim no_split As String = Dgv_Parent.Rows(e.RowIndex).Cells(Cell_NoSplit).Value.ToString()
		Dim bahan As New List(Of Dictionary(Of String, Object))
		Dim hpp As New Dictionary(Of String, Object)
		Dim namaFormula As String = ""
		Dim kategoriProduk As String = ""
		Dim tanggalUji As String = ""
		Dim tanggalValidasi As String = ""
		Dim ketFormula As String = ""
		Dim cookingStep As String = ""

		'Get data untuk laporan
		Try
			OpenConn()

			SQL = $"
                SELECT TOP 1 Cooking_Step 
                FROM Emi_Transaksi_Formulator_Cooking_Steps
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                  AND No_Faktur = '{no_formula}'
                  AND Status IS NULL
                  AND (
                        (
                            Flag_Trial_Kitchen = 'Y'
                            AND Flag_Trial_Produksi IS NULL
                        )
                        OR
                        (
                            Flag_Trial_Kitchen IS NULL
                            AND Flag_Trial_Produksi IS NULL
                        )
                      )
                ORDER BY Urut_Oto DESC
            "
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					cookingStep = Dr("Cooking_Step").ToString()
				End If
			End Using

			SQL = $"SELECT Keterangan FROM N_EMI_Transaksi_Trial_Split_Production_Order WHERE No_Transaksi = '{no_split}'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					ketFormula = Dr("Keterangan").ToString()
				End If
			End Using

			SQL = $"SELECT TOP 1 a.kode_formula, a.nama_produk, FORMAT(b.tanggal, 'dd MMM yyyy') AS tanggal_uji, FORMAT(CONVERT(DATETIME, CONVERT(VARCHAR(10), c.tanggal_selesai_trial_kitchen, 120) + ' ' + c.jam_selesai_trial_kitchen, 120), 'dd MMMM yyyy, HH:mm') AS tanggal_validasi
                    FROM N_EMI_View_Laporan_Formula_Rpt a 
                    JOIN N_LIMS_PO_Sampel b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Split_Po
                    JOIN EMI_Transaksi_Formulator c ON c.Kode_Perusahaan = a.Kode_Perusahaan AND c.No_Faktur = a.Kode_Formula
                    WHERE a.No_Transaksi = '{no_split}'
            "
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					namaFormula = Dr("kode_formula").ToString()
					kategoriProduk = Dr("nama_produk").ToString()
					tanggalUji = Dr("tanggal_uji").ToString()
					tanggalValidasi = Dr("tanggal_validasi").ToString()
				End If
			End Using

			SQL = $"SELECT Nama_bahan, Jumlah, Persentase, Est_HPP, Est_HPP_Per_Pcs FROM N_EMI_View_Laporan_Formula_Rpt WHERE No_Transaksi = '{no_split}' GROUP BY kode_formula, nama_produk, tanggal, No_Transaksi, Nama_bahan, Jumlah, Persentase, Est_HPP_Per_Pcs, Est_HPP"
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
			Dim pdfStream As MemoryStream = Helper_API.CallAPIFile(Url_Api_Laporan_Formulator, "POST", payload, headers)

			If pdfStream Is Nothing OrElse pdfStream.Length = 0 Then
				Throw New Exception("PDF stream kosong")
			End If

			Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

			Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
				pdfStream.CopyTo(file)
			End Using

			Dim frm As New N_EMI_PDF_Viewer(tempPath, "Laporan Formula Trial Kitchen")
			frm.ShowDialog()
		Catch ex As Exception
			Me.Cursor = Cursors.Default
			MessageBox.Show($"Gagal mendapatkan laporan dengan No. Split {no_split}: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Private Sub Dgv_Formula_Parent_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_Parent.CellFormatting
		Try
			If e.RowIndex < 0 OrElse e.Value Is Nothing Then Exit Sub

			' Logika untuk Kolom Qty
			If e.ColumnIndex = Cell_Hasil Then

				Dim jumlah As Double = Val(HilangkanTanda(e.Value.ToString()))
				Dim satuan As String = Dgv_Parent.Rows(e.RowIndex).Cells(Cell_Satuan).Value?.ToString()

				e.Value = $"{Format(jumlah, "N0")} {satuan}"
				e.FormattingApplied = True

			End If
		Catch ex As Exception
			Debug.WriteLine("Error di CellFormatting " & ex.Message)
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