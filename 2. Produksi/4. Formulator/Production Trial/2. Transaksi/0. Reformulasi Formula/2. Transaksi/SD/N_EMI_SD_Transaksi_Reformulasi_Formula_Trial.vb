Public Class N_EMI_SD_Transaksi_Reformulasi_Formula_Trial

	Private WithEvents TypingTimer As New Timer()

	Dim arrFilter As New ArrayList

	Dim LvParent_Lokasi, LvParent_NoFaktur, LvParent_Tanggal, LvParent_Jam, LvParent_KodeBarang, LvParent_NamaBarang, LvParent_Hasil, LvParent_Satuan As String

	Dim itemParent_Lokasi As Integer = 0
	Dim itemParent_NoFaktur As Integer = 1
	Dim itemParent_Tanggal As Integer = 2
	Dim itemParent_Jam As Integer = 3
	Dim itemParent_KodeBarang As Integer = 4
	Dim itemParent_NamaBarang As Integer = 5
	Dim itemParent_Hasil As Integer = 6
	Dim itemParent_Satuan As Integer = 7

	Private Sub N_EMI_SD_Transaksi_Reformulasi_Formula_Trial_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		TypingTimer.Interval = 500
		TypingTimer.Enabled = False

		Lv_Parent.Columns.Clear()
		Lv_Parent.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
		Lv_Parent.Columns.Add("No Faktur", 120, HorizontalAlignment.Left)
		Lv_Parent.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
		Lv_Parent.Columns.Add("Jam", 90, HorizontalAlignment.Center)
		Lv_Parent.Columns.Add("Kode Barang", 140, HorizontalAlignment.Left)
		Lv_Parent.Columns.Add("Nama Barang", 380, HorizontalAlignment.Left)
		Lv_Parent.Columns.Add("Hasil", 120, HorizontalAlignment.Right)
		Lv_Parent.Columns.Add("Satuan", 70, HorizontalAlignment.Center)
		Lv_Parent.View = View.Details

		Lv_Detail_Bahan.Columns.Clear()
		Lv_Detail_Bahan.Columns.Add("Kode Bahan", 90, HorizontalAlignment.Left)
		Lv_Detail_Bahan.Columns.Add("Nama Bahan", 175, HorizontalAlignment.Left)
		Lv_Detail_Bahan.Columns.Add("Persentase", 80, HorizontalAlignment.Center)
		Lv_Detail_Bahan.Columns.Add("Jumlah", 110, HorizontalAlignment.Right)
		Lv_Detail_Bahan.Columns.Add("Est. Hpp Pcs", 100, HorizontalAlignment.Right)
		Lv_Detail_Bahan.View = View.Details

		Lv_Detail_MoistureContent.Columns.Clear()
		Lv_Detail_MoistureContent.Columns.Add("Id Jenis", 0, HorizontalAlignment.Left)
		Lv_Detail_MoistureContent.Columns.Add("Kode Analisa", 90, HorizontalAlignment.Left)
		Lv_Detail_MoistureContent.Columns.Add("Jenis Analisa", 180, HorizontalAlignment.Left)
		Lv_Detail_MoistureContent.Columns.Add("Kategori", 115, HorizontalAlignment.Center)
		Lv_Detail_MoistureContent.Columns.Add("Value", 170, HorizontalAlignment.Left)
		Lv_Detail_MoistureContent.View = View.Details

		Cmb_Filter.Items.Clear() : arrFilter.Clear()
		Cmb_Filter.Items.Add("Lokasi") : arrFilter.Add("a.Lokasi")
		Cmb_Filter.Items.Add("No Faktur") : arrFilter.Add("a.No_Faktur")
		Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("a.Kode_Barang")
		Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("b.Nama")

		Kosong()

	End Sub

	Private Sub Kosong()

		Cmb_Filter.SelectedIndex = -1
		Txt_Filter.Text = ""

		Lv_Parent.Items.Clear()
		Lv_Detail_Bahan.Items.Clear()
		Lv_Detail_MoistureContent.Items.Clear()

		Txt_Tot_Persen.Text = ""
		Txt_Tot_HPP.Text = ""

		LoadDataParent()

	End Sub

	Private Sub GetDataParent(ByVal index As Integer)
		LvParent_Lokasi = Lv_Parent.Items(index).SubItems(itemParent_Lokasi).Text
		LvParent_NoFaktur = Lv_Parent.Items(index).SubItems(itemParent_NoFaktur).Text
		LvParent_Tanggal = Lv_Parent.Items(index).SubItems(itemParent_Tanggal).Text
		LvParent_Jam = Lv_Parent.Items(index).SubItems(itemParent_Jam).Text
		LvParent_KodeBarang = Lv_Parent.Items(index).SubItems(itemParent_KodeBarang).Text
		LvParent_NamaBarang = Lv_Parent.Items(index).SubItems(itemParent_NamaBarang).Text
		LvParent_Hasil = Lv_Parent.Items(index).SubItems(itemParent_Hasil).Text
		LvParent_Satuan = Lv_Parent.Items(index).SubItems(itemParent_Satuan).Text
	End Sub

	Private Sub LoadDataParent()
		Try
			OpenConn()

			Dim Filter As String = ""
			If Cmb_Filter.SelectedIndex <> -1 Then
				Filter = $"and {arrFilter(Cmb_Filter.SelectedIndex)} like '%{Txt_Filter.Text.Trim}%' "
			Else
				Filter = ""
			End If

			Lv_Detail_Bahan.Items.Clear() : Lv_Detail_MoistureContent.Items.Clear()

			Lv_Parent.BeginUpdate()
			Lv_Parent.Items.Clear()
			SQL = $"
				select a.Lokasi, a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Barang, b.Nama as Nama_Barang, a.Hasil, a.Satuan_Hasil
				from Emi_Transaksi_Formulator a
					inner join Barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Status is NULL
				{Filter}
				order by a.Tanggal, a.Jam
			"
			Using Dr = OpenTrans(SQL) '=
				Do While Dr.Read
					Dim lv As ListViewItem
					lv = Lv_Parent.Items.Add(Dr("Lokasi"))
					lv.SubItems.Add(Dr("No_Faktur"))
					lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					lv.SubItems.Add(Dr("Jam"))
					lv.SubItems.Add(Dr("Kode_Barang"))
					lv.SubItems.Add(Dr("Nama_Barang"))
					lv.SubItems.Add(Format(Dr("Hasil"), "N4"))
					lv.SubItems.Add(Dr("Satuan_Hasil"))
				Loop
			End Using
			Lv_Parent.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Filter_TextChanged(sender As Object, e As EventArgs) Handles Txt_Filter.TextChanged
		If Txt_Filter.Text.Trim.Length = 0 Then
			LoadDataParent()
			Exit Sub
		End If
		TypingTimer.Stop()
		TypingTimer.Start()
	End Sub

	Private Sub Lv_Parent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Parent.SelectedIndexChanged
		If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim SelectedFomula As String = Lv_Parent.FocusedItem.SubItems(itemParent_NoFaktur).Text

			'=============================
			'=     LOAD DETAIL BAHAN     =
			'=============================
			Lv_Detail_Bahan.BeginUpdate()
			Lv_Detail_Bahan.Items.Clear()

			SQL = $"
				select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Persentase, isnull(b.Jumlah, 0) as Jumlah, b.satuan, isnull(b.Est_HPP, 0) as Est_HPP, isnull(b.Est_HPP_Per_Pcs, 0) as Est_HPP_Per_Pcs
				from Emi_Transaksi_Formulator a
					inner join EMI_Transaksi_Formulator_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Status is NULL
				and a.No_Faktur = '{SelectedFomula.Trim}'
				order by a.Tanggal, a.Jam
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Detail_Bahan.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
					Lv.SubItems.Add($"{Dr("Persentase")} %")
					Lv.SubItems.Add($"{Format(Dr("Jumlah"), "N4")} {Dr("satuan")}")
					Lv.SubItems.Add(Format(Dr("Est_HPP_Per_Pcs"), "N2"))
				Loop
			End Using
			Lv_Detail_Bahan.EndUpdate()

			'=============================
			'=     MOISTURE CONTENT     =
			'=============================
			Lv_Detail_MoistureContent.BeginUpdate()
			Lv_Detail_MoistureContent.Items.Clear()
			SQL = $"
				;with cte as (
				select a.kode_perusahaan, 'Perhitungan' as Kategori, a.No_Faktur, c.Id_Jenis_Analisa, d.Kode_Analisa, d.Jenis_Analisa, '-' as DataCombo, c.Range_Awal, c.Range_Akhir
				from Emi_Transaksi_Formulator a
					inner join N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Formula
					inner join N_EMI_LAB_Jenis_Analisa d on c.Id_Jenis_Analisa = d.id
				where a.Status is NULL

				union all

				select a.kode_perusahaan, 'Non Perhitungan' as Kategori, a.No_Faktur, c.Id_Jenis_Analisa, d.Kode_Analisa, d.Jenis_Analisa, e.Label_Keterangan as DataCombo, '' as Range_Awal, '' as Range_Akhir
				from Emi_Transaksi_Formulator a
					inner join N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Formula
					inner join N_EMI_LAB_Jenis_Analisa d on c.Id_Jenis_Analisa = d.id
					inner join EMI_Switch e on c.Kode_Perusahaan = e.kode_perusahaan and c.nilai_kriteria = e.keterangan
				where a.Status is NULL

				)
				select kode_perusahaan, Kategori, No_Faktur, Id_Jenis_Analisa, Kode_Analisa, Jenis_Analisa, DataCombo, Range_Awal, Range_Akhir
				from cte
				where kode_perusahaan = '{KodePerusahaan}'
				and No_Faktur = '{SelectedFomula.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Detail_MoistureContent.Items.Add(Dr("Id_Jenis_Analisa"))
					Lv.SubItems.Add(Dr("Kode_Analisa"))
					Lv.SubItems.Add(Dr("Jenis_Analisa"))
					Lv.SubItems.Add(Dr("Kategori"))
					If Dr("DataCombo") = "-" Then
						Lv.SubItems.Add($"{Dr("Range_Awal")} Sampai {Dr("Range_Akhir")}")
					Else
						Lv.SubItems.Add($"{Dr("DataCombo")}")
					End If
				Loop
			End Using
			Lv_Detail_MoistureContent.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		HitungRekapDetailBahan()
	End Sub

	Private Sub Lv_Parent_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Parent.DoubleClick
		If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then Exit Sub

		Dim SelectedFormula As String = Lv_Parent.FocusedItem.SubItems(itemParent_NoFaktur).Text

		N_EMI_Transaksi_Reformulasi_Formula_Trial.LoadDataDetailBahanByFormula(SelectedFormula.Trim)
		Me.Close()

		'' Jalankan proses berat di thread terpisah (Background)
		'Task.Run(Sub()
		'			 ' Jalankan proses berat di sini
		'			 N_EMI_Transaksi_Reformulasi_Formula_Trial.LoadDataDetailBahanByFormula(SelectedFormula)
		'		 End Sub)

		'' Langsung tutup form tanpa menunggu
		'Me.Close()

	End Sub

	Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
		If Cmb_Filter.SelectedIndex = -1 Then
			Txt_Filter.Enabled = False
			Txt_Filter.BackColor = Color.FromArgb(235, 235, 235)
		Else
			Txt_Filter.Enabled = True
			Txt_Filter.BackColor = Color.White
		End If

		Txt_Filter.Text = ""

	End Sub

	Private Sub Btn_Reset_Filter_Click(sender As Object, e As EventArgs) Handles Btn_Reset_Filter.Click
		Cmb_Filter.SelectedIndex = -1
		Txt_Filter.Text = ""
		LoadDataParent()
	End Sub

	'================================================================================================================================================================
	'=     UTILITY
	'================================================================================================================================================================
	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub HitungRekapDetailBahan()
		If Lv_Detail_Bahan.Items.Count = 0 Then Exit Sub

		Dim TotPersen As Double = 0
		Dim TotHpp As Double = 0

		For i As Integer = 0 To Lv_Detail_Bahan.Items.Count - 1
			TotPersen += Val(HilangkanTanda(Lv_Detail_Bahan.Items(i).SubItems(2).Text.Replace("%", "").Trim()))
			TotHpp += Val(HilangkanTanda(Lv_Detail_Bahan.Items(i).SubItems(4).Text.Trim))
		Next
		Txt_Tot_Persen.Text = Format(TotPersen, "N2")
		Txt_Tot_HPP.Text = Format(TotHpp, "N2")

	End Sub

	Private Sub Lv_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Parent.MouseMove
		Dim info As ListViewHitTestInfo = Lv_Parent.HitTest(e.Location)

		If info.Item IsNot Nothing Then
			' Mouse sedang berada di atas row
			Lv_Parent.Cursor = Cursors.Hand
		Else
			' Mouse tidak mengenai row
			Lv_Parent.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Lv_Parent_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Parent.MouseLeave
		Lv_Parent.Cursor = Cursors.Default
	End Sub

	Private Sub TypingTimer_Tick(sender As Object, e As EventArgs) Handles TypingTimer.Tick
		TypingTimer.Stop()

		Dim keyword As String = Txt_Filter.Text.Trim()

		'==============================================
		'=     FUNGSI RELOAD TAMPILKAN DATA ULANG     =
		'==============================================
		LoadDataParent()

	End Sub

End Class