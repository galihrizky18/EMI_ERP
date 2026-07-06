Public Class N_EMI_SD_Transaksi_Production_Barcode_Waste_Sample

	Public arrCheckedSample As New List(Of (ID As String, NoSampel As String, Batch As Integer, JumlahSampelAwal As Double, JumlahSampelConvert As Double, Satuan_Awal As String, Satuan_Convert As String))

	Dim Lv_IdPOSampel, Lv_NoSampel, Lv_NoBatch, Lv_Mesin, Lv_Keterangan, Lv_JumlahSampel, Lv_Satuan, Lv_JmlhConvert As String

	Dim Item_NoSampel As Integer = 0
	Dim Item_NoBatch As Integer = 1
	Dim Item_Mesin As Integer = 2
	Dim Item_Keterangan As Integer = 3
	Dim Item_JumlahSampel As Integer = 4
	Dim Item_Satuan As Integer = 5
	Dim Item_IdPoSampel As Integer = 6
	Dim Item_JmlhConvert As Integer = 7

	Dim IsLoadData As Boolean = False

	Private Sub N_EMI_SD_Transaksi_Production_Barcode_Waste_Sample_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Lv_Data_Sampel.Columns.Clear()
		Lv_Data_Sampel.Columns.Add("No Sampel", 120, HorizontalAlignment.Left)
		Lv_Data_Sampel.Columns.Add("Batch", 50, HorizontalAlignment.Center)
		Lv_Data_Sampel.Columns.Add("Mesin", 150, HorizontalAlignment.Left)
		Lv_Data_Sampel.Columns.Add("Keterangan", 270, HorizontalAlignment.Left)
		Lv_Data_Sampel.Columns.Add("Jumlah Sampel", 130, HorizontalAlignment.Right)
		Lv_Data_Sampel.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		'Hide
		Lv_Data_Sampel.Columns.Add("IdPOSampel", 0, HorizontalAlignment.Left)
		Lv_Data_Sampel.Columns.Add("JmlhConvert", 0, HorizontalAlignment.Left)
		Lv_Data_Sampel.View = View.Details

		Kosong()
	End Sub

	Private Sub Kosong()

		Txt_Total_Pcs.Text = Format(0, "N4")
		Txt_Total_KG.Text = Format(0, "N4")
		Txt_Total_Final.Text = Format(0, "N4")

		IsLoadData = True
		'arrCheckedSample.Clear()
		Chk_All.Checked = False
		Lv_Data_Sampel.Items.Clear()

		Application.DoEvents()
		IsLoadData = False

		LoadDataSampel()
	End Sub

	Private Sub GetDataLv(ByVal index As Integer)
		Lv_NoSampel = Lv_Data_Sampel.Items(index).SubItems(Item_NoSampel).Text
		Lv_NoBatch = Lv_Data_Sampel.Items(index).SubItems(Item_NoBatch).Text
		Lv_Mesin = Lv_Data_Sampel.Items(index).SubItems(Item_Mesin).Text
		Lv_Keterangan = Lv_Data_Sampel.Items(index).SubItems(Item_Keterangan).Text
		Lv_JumlahSampel = Lv_Data_Sampel.Items(index).SubItems(Item_JumlahSampel).Text
		Lv_Satuan = Lv_Data_Sampel.Items(index).SubItems(Item_Satuan).Text
		Lv_IdPOSampel = Lv_Data_Sampel.Items(index).SubItems(Item_IdPoSampel).Text
		Lv_JmlhConvert = Lv_Data_Sampel.Items(index).SubItems(Item_JmlhConvert).Text
	End Sub

	Private Sub LoadDataSampel()

		If Txt_NoSplit.Text.Trim.Length = 0 Then MessageBox.Show("No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

		Try
			OpenConn()

			IsLoadData = True
			Lv_Data_Sampel.BeginUpdate()
			Lv_Data_Sampel.Items.Clear()
			SQL = $"
				select a.id as Id_PO_Sampel, a.No_Sampel, a.No_Batch, a.Id_Mesin, b.Nama_Mesin, a.Keterangan,
					case
						when b.flag_kg = 'Y' then isnull(a.Berat_Sampel, 0)
						else isnull(a.jumlah_pcs, 0)
					end as Jumlah_Sampel,
					case
						when b.flag_kg = 'Y' then 'KG'
						else 'Pcs'
					end as Satuan,
					case
						when b.flag_kg = 'Y' then isnull(a.Berat_Sampel, 0)
						else dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, 'PCS', 'KG', isnull(a.jumlah_pcs, 0) )
					end as Jumlah_Sampel_Convert
				from N_EMI_LAB_PO_Sampel a
					inner join EMI_Master_Mesin b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Mesin = b.Id_Master_Mesin
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Split_Po = '{Txt_NoSplit.Text.Trim}'
				and a.Flag_Input_Waste is null
				order by a.No_Batch, a.Id_Mesin
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					With Lv_Data_Sampel.Items.Add(Dr("No_Sampel"))
						.SubItems.Add(Dr("No_Batch"))
						.SubItems.Add(Dr("Nama_Mesin"))
						.SubItems.Add(Dr("Keterangan"))
						.SubItems.Add(Dr("Jumlah_Sampel"))
						.SubItems.Add(Dr("Satuan"))
						.SubItems.Add(Dr("Id_PO_Sampel"))
						.SubItems.Add(Val(HilangkanTanda(Dr("Jumlah_Sampel_Convert"))))

						If arrCheckedSample.Any(Function(x) x.ID = Dr("Id_PO_Sampel").ToString()) Then
							.Checked = True
						End If
					End With
				Loop
				IsLoadData = False
			End Using
			Lv_Data_Sampel.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Lv_Data_Sampel.Items.Count = 0 Then Exit Sub

		arrCheckedSample.Clear()
		Dim TotalWaste_KG As Double = 0

		For i As Integer = 0 To Lv_Data_Sampel.Items.Count - 1
			If Lv_Data_Sampel.Items(i).Checked AndAlso Val(HilangkanTanda(Lv_Data_Sampel.Items(i).SubItems(Item_JumlahSampel).Text)) <> 0 Then
				GetDataLv(i)
				arrCheckedSample.Add((Lv_IdPOSampel, Lv_NoSampel, Val(Lv_NoBatch), Val(Lv_JumlahSampel), Val(Lv_JmlhConvert), Lv_Satuan, "KG"))
				TotalWaste_KG += Val(Lv_JmlhConvert)
			End If
		Next

		If arrCheckedSample.Count = 0 Then
			MessageBox.Show("Pilih dahulu data sampel!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Emi_Production_Barcode.SelectedSampelWaste = New List(Of (ID As String, NoSampel As String, NoBatch As Integer, JumlahSampelAwal As Double, JumlahSampelConvert As Double, Satuan_Awal As String, Satuan_Convert As String))(arrCheckedSample)
		Emi_Production_Barcode.TxtJmlScrap.Enabled = False
		Emi_Production_Barcode.TxtJmlScrap.Text = Val(HilangkanTanda(TotalWaste_KG))

		Me.Close()

	End Sub

	Private Sub Btn_Keluar_Click(sender As Object, e As EventArgs) Handles Btn_Keluar.Click
		Me.Close()
	End Sub

	'===========================================================================================================================================================
	'=     HELPER
	'===========================================================================================================================================================
	Private Sub HitungTotalDataSampel()
		If Lv_Data_Sampel.Items.Count = 0 Then Exit Sub
		Dim DataPcs As Double = 0
		Dim DataKG As Double = 0
		Dim TotalFinal_KG As Double = 0
		Dim allChecked As Boolean = (Lv_Data_Sampel.Items.Count > 0)

		For i As Integer = 0 To Lv_Data_Sampel.Items.Count - 1
			If Lv_Data_Sampel.Items(i) Is Nothing Then Continue For

			If Not Lv_Data_Sampel.Items(i).Checked Then
				allChecked = False
				Continue For
			End If

			GetDataLv(i)

			Dim jumlah As Double = Val(HilangkanTanda(Lv_JumlahSampel))
			If Lv_Satuan.Trim.ToUpper = "PCS" Then
				DataPcs += jumlah
			Else
				DataKG += jumlah
			End If

			TotalFinal_KG += Val(Lv_Data_Sampel.Items(i).SubItems(Item_JmlhConvert).Text)
		Next

		' Update UI
		Txt_Total_Pcs.Text = DataPcs.ToString("N4")
		Txt_Total_KG.Text = DataKG.ToString("N4")
		Txt_Total_Final.Text = TotalFinal_KG.ToString("N4")

		IsLoadData = True
		Chk_All.Checked = allChecked
		IsLoadData = False

	End Sub

	Private Sub Lv_Data_Sampel_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles Lv_Data_Sampel.ItemChecked
		If IsLoadData Then Exit Sub

		If e.Item.Checked AndAlso Val(HilangkanTanda(e.Item.SubItems(Item_JumlahSampel).Text)) = 0 Then
			IsLoadData = True
			e.Item.Checked = False
			IsLoadData = False

			MessageBox.Show("Data dengan jumlah 0 tidak dapat dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		HitungTotalDataSampel()
	End Sub

	Private Sub Chk_All_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_All.CheckedChanged
		If IsLoadData OrElse Lv_Data_Sampel.Items.Count = 0 Then Exit Sub

		IsLoadData = True
		For i As Integer = 0 To Lv_Data_Sampel.Items.Count - 1
			Lv_Data_Sampel.Items(i).Checked = Chk_All.Checked
		Next
		IsLoadData = False

		HitungTotalDataSampel()
	End Sub

	'===========================================================================================================================================================
	'=     UTILITY
	'===========================================================================================================================================================
	Protected Overrides Sub WndProc(ByRef m As Message)
		If m.Msg = &HA3 Then
			Return
		End If

		MyBase.WndProc(m)
	End Sub

End Class