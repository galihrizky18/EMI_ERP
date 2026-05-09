Public Class N_EMI_Transaksi_Validasi_Formula_Gabungan

	Dim ArrFilter As New ArrayList

	Dim Lv_NoFaktur, Lv_KdBarang, Lv_NmBarang, Lv_Jumlah, Lv_Satuan As String

	Dim Item_NoFaktur As Integer = 0
	Dim Item_KdBarang As Integer = 1
	Dim Item_NmBarang As Integer = 2
	Dim Item_Jumlah As Integer = 3
	Dim Item_Satuan As Integer = 4

	Private Sub N_EMI_Transaksi_Validasi_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Cmb_Kolom_Filter.Items.Clear() : ArrFilter.Clear()
		Cmb_Kolom_Filter.Items.Add("No Faktur") : ArrFilter.Add("a.No_Faktur")
		Cmb_Kolom_Filter.Items.Add("Kode Barang") : ArrFilter.Add("a.Kode_Barang")
		Cmb_Kolom_Filter.Items.Add("Nama Barang") : ArrFilter.Add("b.Nama")
		Cmb_Kolom_Filter.Items.Add("Status") : ArrFilter.Add("CASE " &
		"WHEN a.flag_selesai_produksi = 'Y' THEN 'SELESAI PRODUKSI' " &
		"WHEN a.flag_lanjut_produksi = 'Y' THEN 'PROSES PRODUKSI' " &
		"WHEN a.flag_selesai_trial_produksi = 'Y' THEN 'SELESAI TRIAL PRODUKSI' " &
		"WHEN a.flag_lanjut_trial_produksi = 'Y' THEN 'PROSES TRIAL PRODUKSI' " &
		"WHEN a.flag_selesai_trial_kitchen = 'Y' THEN 'SELESAI TRIAL KITCHEN' " &
		"WHEN a.flag_lanjut_trial_kitchen = 'Y' THEN 'PROSES TRIAL KITCHEN' " &
		"ELSE 'BELUM DIPROSES' END")

		Lv_Data.Columns.Clear()
		Lv_Data.Columns.Add("No Faktur", 100, HorizontalAlignment.Left) ' 0
		Lv_Data.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
		Lv_Data.Columns.Add("Nama Barang", 280, HorizontalAlignment.Left) '2
		Lv_Data.Columns.Add("Jumlah", 100, HorizontalAlignment.Right) '3
		Lv_Data.Columns.Add("Satuan", 50, HorizontalAlignment.Center) '4
		Lv_Data.Columns.Add("Status", 150, HorizontalAlignment.Center) '4
		Lv_Data.View = View.Details

		Kosong()
	End Sub

	Public Sub Kosong()

		Cmb_Kolom_Filter.SelectedIndex = -1
		Txt_Value_Filter.Text = ""

		LoadData()
	End Sub

	Private Sub GetDataLv(ByVal index As Integer)
		Lv_NoFaktur = Lv_Data.Items(index).SubItems(Item_NoFaktur).Text
		Lv_KdBarang = Lv_Data.Items(index).SubItems(Item_KdBarang).Text
		Lv_NmBarang = Lv_Data.Items(index).SubItems(Item_NmBarang).Text
		Lv_Jumlah = Lv_Data.Items(index).SubItems(Item_Jumlah).Text
		Lv_Satuan = Lv_Data.Items(index).SubItems(Item_Satuan).Text
	End Sub

	Private Sub LoadData()
		Try
			OpenConn()

			Lv_Data.Items.Clear()
			SQL = $"
				SELECT *
				FROM (
					SELECT 
						a.No_Faktur, a.Kode_Barang, b.Nama AS Nama_Barang, 
						a.Hasil, a.Satuan_Hasil,
						a.flag_lanjut_trial_kitchen, 
						a.flag_lanjut_trial_produksi, 
						a.flag_lanjut_produksi,
						a.Tanggal,
						CASE
							WHEN a.flag_selesai_produksi = 'Y' THEN 'SELESAI PRODUKSI'
							WHEN a.flag_lanjut_produksi = 'Y' THEN 'PROSES PRODUKSI'
							WHEN a.flag_selesai_trial_produksi = 'Y' THEN 'SELESAI TRIAL PRODUKSI'
							WHEN a.flag_lanjut_trial_produksi = 'Y' THEN 'PROSES TRIAL PRODUKSI'
							WHEN a.flag_selesai_trial_kitchen = 'Y' THEN 'SELESAI TRIAL KITCHEN'
							WHEN a.flag_lanjut_trial_kitchen = 'Y' THEN 'PROSES TRIAL KITCHEN'
							ELSE 'BELUM DIPROSES'
						END AS Status
					FROM Emi_Transaksi_Formulator a
					INNER JOIN barang b 
						ON a.Kode_Perusahaan = b.Kode_Perusahaan
						AND a.Kode_Stock_Owner = b.Kode_Stock_Owner
						AND a.Kode_Barang = b.Kode_Barang_Inq
					WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
					AND a.Status IS NULL
					AND a.Flag_Validasi = 'Y'
					{If(Cmb_Kolom_Filter.SelectedIndex <> -1 And Txt_Value_Filter.Text <> "", $"AND {ArrFilter(Cmb_Kolom_Filter.SelectedIndex)} LIKE '%{Txt_Value_Filter.Text}%'", "")}
				) x
				WHERE x.Status NOT LIKE 'PROSES%'
			"

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama_Barang"))
					Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Hasil"))), "N4"))
					Lv.SubItems.Add(Dr("Satuan_Hasil"))
					Lv.SubItems.Add(Dr("Status"))

					Dim status As String = Dr("Status").ToString()

					Select Case status
						Case "PROSES PRODUKSI", "SELESAI PRODUKSI"
							Lv.BackColor = Color.LightGreen

						Case "PROSES TRIAL PRODUKSI", "SELESAI TRIAL PRODUKSI"
							Lv.BackColor = Color.LightBlue

						Case "PROSES TRIAL KITCHEN", "SELESAI TRIAL KITCHEN"
							Lv.BackColor = Color.LightYellow

						Case Else
							Lv.BackColor = Color.LightGray
					End Select
				Loop
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

	Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
		If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

		Dim No_faktur As String = Lv_Data.FocusedItem.SubItems(Item_NoFaktur).Text
		Dim Status As String = Lv_Data.FocusedItem.SubItems(6).Text  ' kolom Status

		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.Kosong()
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.TxtFormulator_NoFaktur.Text = No_faktur
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.StatusDariList = Status
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.TxtFormulator_NoFaktur_Leave(sender, New EventArgs)
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.ShowDialog()

		LoadData()
	End Sub

	Private Sub Lv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data.MouseMove
		Dim info As ListViewHitTestInfo = Lv_Data.HitTest(e.Location)

		If info.Item IsNot Nothing Then
			Lv_Data.Cursor = Cursors.Hand
		Else
			Lv_Data.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub Lv_Data_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data.MouseLeave
		Lv_Data.Cursor = Cursors.Default
	End Sub
End Class