Public Class SD_Detail_Batch

	Public noSplit As String = ""
	Public asal As String = ""

	Dim Lv_Batch, Lv_KdBarang, Lv_NmBarang, Lv_NilaiFormula, Lv_NilaiProduksi, Lv_Satuan, Lv_NoTransaksi As String

	Dim item_Batch As Integer = 0
	Dim item_KdBarang As Integer = 1
	Dim item_NmBarang As Integer = 2
	Dim item_NilaiFormula As Integer = 3
	Dim item_NilaiProduksi As Integer = 4
	Dim item_Satuan As Integer = 5
	Dim item_NoFaktur As Integer = 6

	Dim boleh_lihat_data As Boolean = False

	Dim FilterGR As New List(Of (Sql_FG As String, Sql_SCP As String, ValueCombo As String)) From {
		(OpsiSeluruh, OpsiSeluruh, OpsiSeluruh),
		("c.Tahap", "", "Batch"),
		("c.Batch_Number", "c.Batch_Number", "Batch Number"),
		("(c.Qr_Code+'-'+c.Kode_Unik_Berjalan)", "(c.Qr_Code+'-'+c.Kode_Unik_Berjalan)", "Barcode"),
		("b.Kode_Barang", "b.Kode_Barang_scrap", "Kode Barang")
	}

	Dim Color_Txt_Disable As Color = Color.FromArgb(235, 235, 235)

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Private lastIndex As Integer = -1
	Private originalColor As Color

	Private Sub SD_Detail_Batch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBuffer(Lv_GR_Detail_Pallet)
		EnableDoubleBuffer(Batch)
		EnableDoubleBuffer(LvDataRekap)
		EnableDoubleBuffer(Lv_DataDetail)

		EnableDoubleBufferDGV(Dgv_GR_Batch)

		Kosong()
	End Sub

	Public Sub Kosong()

		Txt_TotNilaiFormula.Text = ""
		Txt_TotNilaiPRoduksi.Text = ""

		If asal = "GI" Then
			Panel_GI.Visible = True
			Panel_GR.Visible = False

			Panel_GI.Location = New Point(13, 67)
			Panel_GR.Location = New Point(1000, 67)

			Try
				OpenConn()

				If CekButtonRole("Tampil_Detail_GI") = "Y" Then
					boleh_lihat_data = True
				End If

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
			'============================================================================
			'=     PANEL 1
			'============================================================================

			If boleh_lihat_data = True Then
				Lv_DataDetail.Columns.Clear() : Lv_DataDetail.Items.Clear()
				Lv_DataDetail.Columns.Add("", 0, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Batch", 70, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Jam", 90, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Kode Barang", 140, HorizontalAlignment.Left)
				Lv_DataDetail.Columns.Add("Nama", 350, HorizontalAlignment.Left)
				Lv_DataDetail.Columns.Add("Nilai Formula", 130, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Nilai Produksi", 130, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Nilai Produksi Tambahan", 130, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Selisih ", 130, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Selisih %", 120, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("no_Faktur", 0, HorizontalAlignment.Left)
			Else
				Lv_DataDetail.Columns.Clear() : Lv_DataDetail.Items.Clear()
				Lv_DataDetail.Columns.Add("", 0, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Batch", 120, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Tanggal", 180, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Jam", 160, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Kode Barang", 220, HorizontalAlignment.Left)
				Lv_DataDetail.Columns.Add("Nama", 0, HorizontalAlignment.Left)
				Lv_DataDetail.Columns.Add("Nilai Formula", 0, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Nilai Produksi", 240, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Nilai Produksi Tambahan", 130, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Satuan", 130, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Selisih ", 0, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Selisih %", 0, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("no_Faktur", 0, HorizontalAlignment.Left)
			End If

			Lv_DataDetail.View = View.Details

			'============================================================================
			LvDataRekap.Columns.Clear() : Lv_DataDetail.Items.Clear()
			LvDataRekap.Columns.Add("", 0, HorizontalAlignment.Right)
			LvDataRekap.Columns.Add("Batch", 130, HorizontalAlignment.Center)
			LvDataRekap.Columns.Add("Tanggal", 180, HorizontalAlignment.Center)
			LvDataRekap.Columns.Add("Jam", 180, HorizontalAlignment.Center)
			LvDataRekap.Columns.Add("Nilai Dosing (Kg)", 250, HorizontalAlignment.Right)
			LvDataRekap.Columns.Add("Selisih Dosing (%)", 200, HorizontalAlignment.Right)
			LvDataRekap.View = View.Details

			Try
				OpenConn()

				Cmb_Filter_Batch_Pn1.Items.Clear()

				SQL = "select a.Jumlah_Batch, a.satuan_batch, a.Kode_Barang, b.nama, Qty_Batch "
				SQL = SQL & "from Emi_Split_Production_Order a, Barang b  "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
				SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang  "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.Status is null  "
				SQL = SQL & "and a.No_Transaksi = '" & noSplit & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						TxtJumlahBatch.Text = Dr("Qty_Batch")
						TxtJumlahBatchVw.Text = Dr("Qty_Batch") & " " & Dr("satuan_batch")
						TxtBatch.Text = Dr("Jumlah_Batch")
						TxtNoSplit.Text = noSplit
						TxtNamaBarang.Text = Dr("nama")
					End If
				End Using

				SQL = "select isnull(max(e.Proses),0) as Proses "
				SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c, Emi_Production_Results d, Emi_Production_Results_HPP e, Barang f  "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan  "
				SQL = SQL & "and a.No_PO = b.No_Faktur  "
				SQL = SQL & "and a.No_Transaksi = d.No_Production_Order  "
				SQL = SQL & "and d.No_Transaksi = e.No_Transaksi  "
				SQL = SQL & "and b.Kode_Formula = c.No_Faktur  "
				SQL = SQL & "and c.Kode_Stock_Owner = f.Kode_Stock_Owner and c.Kode_Barang = f.Kode_Barang  "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.Status is null  "
				SQL = SQL & "and a.No_Transaksi = '" & noSplit & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dim JumlahBatch As Double = Val(HilangkanTanda(Dr("Proses")))
						Cmb_Filter_Batch_Pn1.Items.Add("--- SELURUH ---")
						For i As Integer = 1 To JumlahBatch
							Cmb_Filter_Batch_Pn1.Items.Add(i)
						Next

						Cmb_Filter_Batch_Pn1.SelectedIndex = 0
					End If
				End Using

				Cmb_KdBarang_Pn1.Items.Clear()
				SQL = "select distinct c.Kode_Barang "
				SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c, Emi_Production_Results d, Emi_Production_Results_HPP e, Barang f  "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan  "
				SQL = SQL & "and a.No_PO = b.No_Faktur  "
				SQL = SQL & "and a.No_Transaksi = d.No_Production_Order  "
				SQL = SQL & "and d.No_Transaksi = e.No_Transaksi  "
				SQL = SQL & "and b.Kode_Formula = c.No_Faktur  "
				SQL = SQL & "and c.Kode_Stock_Owner = f.Kode_Stock_Owner and c.Kode_Barang = f.Kode_Barang  "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.Status is null  "
				SQL = SQL & "and a.No_Transaksi = '" & noSplit & "' "
				Using Dr = OpenTrans(SQL)
					Cmb_KdBarang_Pn1.Items.Add("--- SELURUH ---")
					Do While Dr.Read
						Cmb_KdBarang_Pn1.Items.Add(Dr("Kode_Barang"))
					Loop
					Cmb_KdBarang_Pn1.SelectedIndex = 0
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			Btn_Cari_Pn1_Click(Btn_Cari_Pn1, New EventArgs)
		Else
			Panel_GI.Visible = False
			Panel_GR.Visible = True

			Panel_GR.Location = New Point(13, 67)
			Panel_GI.Location = New Point(1000, 67)

			If Cmb_Filter_GR.Items.Count = 0 Then
				Cmb_Filter_GR.Items.Clear()
				For Each item In FilterGR
					Cmb_Filter_GR.Items.Add(item.ValueCombo)
				Next
			End If

			Cmb_Filter_GR.SelectedIndex = 0

			Txt_Filter_GR.Text = ""
			Txt_Filter_GR.BackColor = Color_Txt_Disable

			Txt_GR_Rekap_PO.Text = ""
			Txt_GR_Rekap_Split.Text = ""
			Txt_GR_Rekap_Result.Text = ""
			Txt_GR_Rekap_Jumlah_Dosing.Text = ""
			Txt_GR_Rekap_Jumlah_Selesai.Text = ""
			Txt_GR_Rekap_Split.Text = ""

			Txt_GR_Rekap_Total.Text = ""
			Txt_GR_Rekap_Total_GR.Text = ""
			Txt_GR_Rekap_Total_Scrap.Text = ""

			Batch.Columns.Clear() : Batch.Items.Clear()
			Batch.Columns.Add("", 0, HorizontalAlignment.Center)
			Batch.Columns.Add("Batch Number", 150, HorizontalAlignment.Center)
			Batch.Columns.Add("Tanggal", 115, HorizontalAlignment.Center)
			Batch.Columns.Add("Lokasi", 180, HorizontalAlignment.Left)
			Batch.Columns.Add("Barcode", 300, HorizontalAlignment.Left)
			Batch.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
			Batch.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
			Batch.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
			Batch.Columns.Add("Batch", 80, HorizontalAlignment.Center).DisplayIndex = 1
			Batch.View = View.Details

			Lv_GR_Detail_Pallet.Columns.Clear()
			Lv_GR_Detail_Pallet.Columns.Add("Barcode", 280, HorizontalAlignment.Left)
			Lv_GR_Detail_Pallet.Columns.Add("Tgl Produksi", 100, HorizontalAlignment.Center)
			Lv_GR_Detail_Pallet.Columns.Add("Batch Input", 90, HorizontalAlignment.Center)
			Lv_GR_Detail_Pallet.Columns.Add("Troli", 100, HorizontalAlignment.Center)
			Lv_GR_Detail_Pallet.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
			Lv_GR_Detail_Pallet.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
			Lv_GR_Detail_Pallet.Columns.Add("Jumlah Terpakai", 130, HorizontalAlignment.Right)
			Lv_GR_Detail_Pallet.Columns.Add("Satuan Terpakai", 100, HorizontalAlignment.Center)
			Lv_GR_Detail_Pallet.View = View.Details

			Lv_GR_Detail_Scrap.Columns.Clear()
			Lv_GR_Detail_Scrap.Columns.Add("Barcode", 280, HorizontalAlignment.Left)
			Lv_GR_Detail_Scrap.Columns.Add("Tgl Produksi", 100, HorizontalAlignment.Center)
			Lv_GR_Detail_Scrap.Columns.Add("Batch Input", 90, HorizontalAlignment.Center)
			Lv_GR_Detail_Scrap.Columns.Add("Troli", 100, HorizontalAlignment.Center)
			Lv_GR_Detail_Scrap.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
			Lv_GR_Detail_Scrap.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
			Lv_GR_Detail_Scrap.Columns.Add("Jumlah Terpakai", 130, HorizontalAlignment.Right)
			Lv_GR_Detail_Scrap.Columns.Add("Satuan Terpakai", 100, HorizontalAlignment.Center)
			Lv_GR_Detail_Scrap.View = View.Details

			Lv_GR_Detail_Pallet.Size = New Point(1035, 140)
			Label23.Visible = True
			Panel10.Visible = True
			Lv_GR_Detail_Scrap.Visible = True

			'============================================================================
			'=     PANEL 2
			'============================================================================
			LoadDataGR(1)

		End If

	End Sub

	Private Sub LoadDataGR(ByVal JenisButton As Integer)

		SetButtonStyle(Btn_Batch_All, JenisButton = 1)
		SetButtonStyle(Btn_Batch_Sdh_Validasi, JenisButton = 2)
		SetButtonStyle(Btn_Batch_Blm_Validasi, JenisButton = 3)

		Dim FilterStatusBatch = ""
		If JenisButton = 2 Then
			FilterStatusBatch = "and c.Flag_Validasi = 'Y' "
		ElseIf JenisButton = 3 Then
			FilterStatusBatch = "and c.Flag_Validasi is null "
		Else
			FilterStatusBatch = ""
		End If

		Try
			OpenConn()

			'======================
			'=     LOAD BATCH     =
			'======================
			Dim Total_Dosing As Double = 0
			Dim Total_Selesai As Double = 0
			Dim Total_Selisih As Double = 0
			Dim Total_Satuan As String = ""
			Dgv_GR_Batch.Rows.Clear() : Lv_GR_Detail_Pallet.Items.Clear() : Lv_GR_Detail_Scrap.Items.Clear()
			SQL = $"
				select c.Proses, a.No_PO, a.No_Transaksi, b.No_Transaksi as No_Result,
					   sum(c.Jumlah_Dosing) as Jumlah_Dosing, sum(c.Jumlah_Terpakai) as Jumlah_Selesai,
					   (sum(c.Jumlah_Dosing) - sum(c.Jumlah_Terpakai)) as Selisih, c.Satuan, c.Flag_Validasi
				from Emi_Split_Production_Order a
					inner join Emi_Production_Results b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Production_Order
					inner join Emi_Production_Results_HPP c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Transaksi = c.No_Transaksi
				where a.Status is null
				and c.Tanggal is not null
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Transaksi = '{noSplit}'
				{FilterStatusBatch}
				group by a.No_Transaksi, a.No_PO, b.No_Transaksi, c.Satuan, c.Proses, c.Flag_Validasi
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						Dgv_GR_Batch.SuspendLayout()

						For i As Integer = 0 To .Rows.Count - 1

							Txt_GR_Rekap_PO.Text = .Rows(i).Item("No_PO")
							Txt_GR_Rekap_Split.Text = .Rows(i).Item("No_Transaksi")
							Txt_GR_Rekap_Result.Text = .Rows(i).Item("No_Result")

							Dim rowIndex As Integer = Dgv_GR_Batch.Rows.Add()
							Dgv_GR_Batch.Rows(rowIndex).HeaderCell.Value = $"{i + 1}"
							Dgv_GR_Batch.Rows(rowIndex).Cells(0).Value = .Rows(i).Item("Proses")

							Total_Dosing += Val(HilangkanTanda(.Rows(i).Item("Jumlah_Dosing")))
							Total_Selesai += Val(HilangkanTanda(.Rows(i).Item("Jumlah_Selesai")))
							Total_Selisih += Val(HilangkanTanda(.Rows(i).Item("Selisih")))
							Total_Satuan = .Rows(i).Item("Satuan")

							Dim FlagRelease As String = General_Class.CekNULL(.Rows(i).Item("Flag_Validasi"))

							If FlagRelease.ToUpper.Trim = "Y" Then
								Dgv_GR_Batch.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightGreen
							End If

						Next
						Dim scrapIndex As Integer = Dgv_GR_Batch.Rows.Add()
						Dgv_GR_Batch.Rows(scrapIndex).HeaderCell.Value = "Waste"
						Dgv_GR_Batch.Rows(scrapIndex).Cells(0).Value = "Waste"

						Dgv_GR_Batch.ResumeLayout()
					End If
				End With
			End Using

			Txt_GR_Rekap_Jumlah_Dosing.Text = $"{Format(Total_Dosing, "N4")} {Total_Satuan}"
			Txt_GR_Rekap_Jumlah_Selesai.Text = $"{Format(Total_Selesai, "N4")} {Total_Satuan}"
			Txt_GR_Rekap_Selisih.Text = $"{Format(Total_Selisih, "N4")} {Total_Satuan}"

			'==================
			'=     FILTER     =
			'==================
			Dim Filter_FG As String = ""
			Dim Filter_SCP As String = ""
			Dim IndexCmb As Integer = Cmb_Filter_GR.SelectedIndex

			If IndexCmb > 0 Then
				Dim filterText As String = Txt_Filter_GR.Text.Trim()
				Dim selectedFilter = FilterGR(IndexCmb)

				Filter_FG &= $" AND {selectedFilter.Sql_FG} like '%{filterText}%' "

				If IndexCmb = 1 Then
					If filterText = "0" Then
						Filter_SCP = $" AND 1=1 "
					Else
						Filter_SCP = " AND 1=2 "
					End If
				Else
					Filter_SCP = $" AND {selectedFilter.Sql_SCP} LIKE '%{filterText}%' "
				End If
			End If

			Batch.Items.Clear()

#Region "KODE LAMA"

			'SQL = "Select a.Kode_Perusahaan, e.No_PO, a.No_Production_Order as No_Split, a.No_Transaksi as No_Result, b.Tanggal, b.Jam, c.Batch_Number, c.Qr_Code+'-'+c.Kode_Unik_Berjalan as Barcode,  "
			'SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang, d.Nama as Barang, sum(c.Jumlah) as jumlah, b.Satuan, c.Tahap  "
			'SQL = SQL & "From Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, Emi_Production_Results_Detail_Pallet c, barang d, Emi_Split_Production_Order e "
			'SQL = SQL & "Where a.kode_perusahaan = b.kode_Perusahaan And b.kode_perusahaan = c.kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
			'SQL = SQL & "and a.No_Production_Order = e.No_Transaksi "
			'SQL = SQL & "And a.No_Transaksi = b.no_transaksi "
			'SQL = SQL & "And b.No_Transaksi = c.no_transaksi And b.Proses = c.Proses "
			'SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
			'SQL = SQL & "And a.status Is null "
			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
			'SQL = SQL & Filter_FG
			'SQL = SQL & "group by a.Kode_Perusahaan, e.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tanggal, b.Jam, c.Batch_Number, "
			'SQL = SQL & "(c.Qr_Code+'-'+c.Kode_Unik_Berjalan), c.Kode_Unik_Berjalan, b.Kode_Stock_Owner, b.Kode_Barang, d.Nama, b.Satuan, c.Tahap "

			'SQL = SQL & "union all "

			'SQL = SQL & "Select a.Kode_Perusahaan, e.No_PO, a.No_Production_Order as No_Split, a.No_Transaksi as No_Result, b.Tanggal, b.Jam, c.Batch_Number, c.Qr_Code+'-'+c.Kode_Unik_Berjalan as Barcode,  "
			'SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang_scrap as Kode_Barang, d.Nama as Barang, sum(c.Jumlah) as jumlah, b.Satuan_scrap as Satuan, 0 as Tahap "
			'SQL = SQL & "From Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, EMI_Production_Results_Detail_Scrap c, barang d, Emi_Split_Production_Order e "
			'SQL = SQL & "Where a.kode_perusahaan = b.kode_Perusahaan And b.kode_perusahaan = c.kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
			'SQL = SQL & "and a.No_Production_Order = e.No_Transaksi "
			'SQL = SQL & "And a.No_Transaksi = b.no_transaksi "
			'SQL = SQL & "And b.No_Transaksi = c.no_transaksi And b.Proses = c.Proses "
			'SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang_scrap = d.Kode_Barang "
			'SQL = SQL & "And a.status Is null "
			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
			'SQL = SQL & Filter_SCP
			'SQL = SQL & "group by a.Kode_Perusahaan, e.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tanggal, b.Jam, c.Batch_Number, "
			'SQL = SQL & "(c.Qr_Code+'-'+c.Kode_Unik_Berjalan), c.Kode_Unik_Berjalan, b.Kode_Stock_Owner, b.Kode_Barang_scrap, d.Nama, b.Satuan_scrap "
			'SQL = SQL & "oredr by Tahap "

#End Region

			SQL = $"
				SELECT * FROM (
								  Select a.Kode_Perusahaan, e.No_PO, a.No_Production_Order as No_Split, a.No_Transaksi as No_Result,
										 b.Tanggal, b.Jam, c.Batch_Number, c.Qr_Code + '-' + c.Kode_Unik_Berjalan as Barcode,
										 b.Kode_Stock_Owner, b.Kode_Barang, d.Nama as Barang, sum(c.Jumlah) as jumlah, b.Satuan, c.Tahap
								  From Emi_Production_Results a,
									   EMI_Production_Results_Detail_Barang b,
									   Emi_Production_Results_Detail_Pallet c,
									   barang d,
									   Emi_Split_Production_Order e
								  Where a.kode_perusahaan = b.kode_Perusahaan
									And b.kode_perusahaan = c.kode_Perusahaan
									and b.Kode_Perusahaan = d.Kode_Perusahaan
									and a.Kode_Perusahaan = e.Kode_Perusahaan
									and a.No_Production_Order = e.No_Transaksi
									And a.No_Transaksi = b.no_transaksi
									And b.No_Transaksi = c.no_transaksi
									And b.Proses = c.Proses
									and b.Kode_Stock_Owner = d.Kode_Stock_Owner
									and b.Kode_Barang = d.Kode_Barang
									And a.status Is null
									and a.Kode_Perusahaan = '{KodePerusahaan}'
									and a.No_Production_Order = '{noSplit}'
									{Filter_FG}
								  group by a.Kode_Perusahaan, e.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tanggal, b.Jam,
										   c.Batch_Number,
										   (c.Qr_Code + '-' + c.Kode_Unik_Berjalan), c.Kode_Unik_Berjalan, b.Kode_Stock_Owner,
										   b.Kode_Barang, d.Nama, b.Satuan, c.Tahap

								  union all

								  Select a.Kode_Perusahaan, e.No_PO, a.No_Production_Order as No_Split, a.No_Transaksi as No_Result,
										 b.Tanggal, b.Jam, c.Batch_Number, c.Qr_Code + '-' + c.Kode_Unik_Berjalan as Barcode,
										 b.Kode_Stock_Owner, b.Kode_Barang_scrap as Kode_Barang, d.Nama as Barang,
										 sum(c.Jumlah) as jumlah, b.Satuan_scrap as Satuan, 0 as Tahap
								  From Emi_Production_Results a,
									   EMI_Production_Results_Detail_Barang b,
									   EMI_Production_Results_Detail_Scrap c,
									   barang d,
									   Emi_Split_Production_Order e
								  Where a.kode_perusahaan = b.kode_Perusahaan
									And b.kode_perusahaan = c.kode_Perusahaan
									and b.Kode_Perusahaan = d.Kode_Perusahaan
									and a.Kode_Perusahaan = e.Kode_Perusahaan
									and a.No_Production_Order = e.No_Transaksi
									And a.No_Transaksi = b.no_transaksi
									And b.No_Transaksi = c.no_transaksi
									And b.Proses = c.Proses
									and b.Kode_Stock_Owner = d.Kode_Stock_Owner
									and b.Kode_Barang_scrap = d.Kode_Barang
									And a.status Is null
									and a.Kode_Perusahaan = '{KodePerusahaan}'
									and a.No_Production_Order = '{noSplit}'
									{Filter_SCP}
								  group by a.Kode_Perusahaan, e.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tanggal, b.Jam,
										   c.Batch_Number,
										   (c.Qr_Code + '-' + c.Kode_Unik_Berjalan), c.Kode_Unik_Berjalan, b.Kode_Stock_Owner,
										   b.Kode_Barang_scrap, d.Nama, b.Satuan_scrap
							  ) AS HasilUnion
				ORDER BY CASE WHEN Tahap = 0 THEN 1 ELSE 0 END, Tahap ASC
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Batch.Items.Add("")
					Lv.SubItems.Add(Dr("Batch_Number"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
					Lv.SubItems.Add(Dr("Barcode"))
					Lv.SubItems.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Format(Dr("jumlah"), "N4"))
					Lv.SubItems.Add(Dr("Satuan"))
					Lv.SubItems.Add(Dr("Tahap"))

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Dgv_GR_Batch.ClearSelection()

	End Sub

	Private Sub Btn_Cari_Pn1_Click(sender As Object, e As EventArgs) Handles Btn_Cari_Pn1.Click

		Dim boleh_lihat_data As Boolean = False
		Try
			OpenConn()

			If CekButtonRole("Tampil_Detail_GI") = "Y" Then
				boleh_lihat_data = True
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()
			Dim Total_Formula As Double = 0
			Dim Total_Produksi As Double = 0

			LvDataRekap.Items.Clear()
			SQL = "Select b.proses, b.Tanggal, b.Jam, "
			SQL = SQL & "isnull((select sum(y.Nilai_Barang) from Emi_Production_Results_Detail y where "
			SQL = SQL & "a.kode_perusahaan = y.kode_perusahaan And a.No_Transaksi = y.No_Transaksi And "
			SQL = SQL & "b.Proses = y.Proses ),0) As Total_Dosing "
			SQL = SQL & "From Emi_Production_Results a, Emi_Production_Results_HPP b Where "
			SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi  "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Status is null "
			SQL = SQL & "and a.no_production_order = '" & noSplit & "' "

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read()
					Dim Lv As ListViewItem
					Lv = LvDataRekap.Items.Add("")
					Lv.SubItems.Add(Dr("Proses"))
					If General_Class.CekNULL(Dr("Tanggal")) = "" Then
						Lv.SubItems.Add("-")
						Lv.SubItems.Add("-")
						Lv.BackColor = Color.White
					Else
						Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
						Lv.SubItems.Add(Dr("Jam"))
						Lv.BackColor = Color.LightGreen
					End If

					Lv.SubItems.Add(Format(Dr("Total_Dosing"), "N4"))
					Lv.SubItems.Add(Format(Val(HilangkanTanda((Val(HilangkanTanda(Dr("Total_Dosing"))) - Val(HilangkanTanda(TxtJumlahBatch.Text))) / Val(HilangkanTanda(TxtJumlahBatch.Text)) * 100)), "N4"))

				Loop
			End Using

			Lv_DataDetail.Items.Clear()

			Dim Filter As String = ""
			If Not Cmb_Filter_Batch_Pn1.SelectedIndex = 0 Then
				Filter = Filter & "and f.Proses = '" & Cmb_Filter_Batch_Pn1.Text & "' "
			End If

			If Not Cmb_KdBarang_Pn1.SelectedIndex = 0 Then
				Filter = Filter & "and c.Kode_Barang = '" & Cmb_KdBarang_Pn1.Text & "' "
			End If

#Region "Kode Lama"

			'SQL = "select a.No_Transaksi, e.Tanggal, e.Jam, e.Proses, c.Kode_Stock_Owner, c.Kode_Barang, f.nama, c.satuan, "
			'SQL = SQL & "round( "
			'SQL = SQL & "(c.Jumlah / (select z.Hasil from Emi_Transaksi_Formulator z "
			'SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur) "
			'SQL = SQL & ") "
			'SQL = SQL & " * "
			'SQL = SQL & "isnull(a.Qty_Batch,2),4) as Nilai_Formula, "
			'SQL = SQL & "ISNULL(( select z.Nilai_Produksi from Emi_Production_Results_Detail z  where z.Kode_Perusahaan = d.Kode_Perusahaan "
			'SQL = SQL & "and z.No_Transaksi = d.No_Transaksi and z.Kode_Barang = c.Kode_Barang "
			'SQL = SQL & "and z.Proses = e.Proses ), 0) as Nilai_Produksi "
			'SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c, Emi_Production_Results d, Emi_Production_Results_HPP e, Barang f "
			'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan "
			'SQL = SQL & "and a.No_PO = b.No_Faktur "
			'SQL = SQL & "and a.No_Transaksi = d.No_Production_Order "
			'SQL = SQL & "and d.No_Transaksi = e.No_Transaksi "
			'SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
			'SQL = SQL & "and c.Kode_Stock_Owner = f.Kode_Stock_Owner and c.Kode_Barang = f.Kode_Barang "
			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.Status is null "
			'SQL = SQL & "and a.No_Transaksi = '" & noSplit & "' "

			'If Not Cmb_Filter_Batch_Pn1.SelectedIndex = 0 Then
			'	SQL = SQL & "and e.Proses = '" & Cmb_Filter_Batch_Pn1.Text & "' "
			'End If

			'If Not Cmb_KdBarang_Pn1.SelectedIndex = 0 Then
			'	SQL = SQL & "and c.Kode_Barang = '" & Cmb_KdBarang_Pn1.Text & "' "
			'End If

			'SQL = SQL & "order by e.Proses, c.Kode_Barang "

#End Region

			SQL = $"
				;with Data_TF as (
									select f.Kode_Perusahaan, f.No_Faktur_Order, f.Flag_Tambah, e.Kode_Barang, d.Serial_Number,
										   sum(d.Jumlah) as Jumlah
									from Tf_Stock_Parent a
										 inner join Tf_Stock b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
										 inner join Tf_Stock_det c
													on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and
													   b.Urut_Oto = c.Urut_TF
										 inner join TF_Stock_Det2 d
													on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and
													   c.Urut_Oto = d.Urut_Det
										 inner join Emi_Material_Requisition_Det_Convert e on b.Kode_Perusahaan = e.Kode_Perusahaan and
																							  b.Urut_Material_Requisition_Convert =
																							  e.Urut_Oto
										 inner join Emi_Material_Requisition f
													on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Faktur = f.No_Faktur
									where a.Status is null
									  and f.Status is null
									  and a.Kode_Perusahaan = '{KodePerusahaan}'
									  and f.No_Faktur_Order = '{noSplit}'
									group by f.Kode_Perusahaan, f.No_Faktur_Order, f.Flag_Tambah, e.Kode_Barang, d.Serial_Number

									union all

									select a.Kode_Perusahaan, a.No_Faktur_Order, NULL as Flag_Tambah, b.Kode_Barang, b.SN_Baru,
										   sum(b.Jumlah) as Jumlah
									from N_EMI_Transaksi_Material_Requisition_QC a
										 inner join N_EMI_Transaksi_Material_Requisition_QC_Validasi b
													on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_RM
									where a.Status is null
									  and b.Status is null
									  and a.Kode_Perusahaan = '{KodePerusahaan}'
									  and a.No_Faktur_Order = '{noSplit}'
									group by a.Kode_Perusahaan, a.No_Faktur_Order, b.Kode_Barang, b.SN_Baru
								),
					 Detail_GI as (
									select a.Kode_Perusahaan, a.No_Transaksi, a.No_Production_Order, b.Kode_Barang,
										   SUM(
												   CASE
													   WHEN ISNULL(e.Flag_Tambah, 'N') <> 'Y'
														   THEN
														   CASE
															   WHEN d.Flag_Non_Barcode = 'Y'
																   THEN ISNULL(c.Nilai, 0)
															   ELSE ISNULL(e.Jumlah, 0)
														   END
													   ELSE 0
												   END
										   ) AS Nilai_Produksi,
										   SUM(
												   CASE
													   WHEN e.Flag_Tambah = 'Y'
														   THEN
														   CASE
															   WHEN d.Flag_Non_Barcode = 'Y'
																   THEN ISNULL(c.Nilai, 0)
															   ELSE ISNULL(e.Jumlah, 0)
														   END
													   ELSE 0
												   END
										   ) AS Nilai_Produksi_Tambahan
									from Emi_Production_Results a
										 inner join Emi_Production_Results_Detail b
													on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
										 inner join Emi_Production_Results_Det c
													on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Transaksi = c.No_Transaksi and
													   b.Urut = c.No_Urut_Detail
										 inner join barang d on b.Kode_Perusahaan = d.Kode_Perusahaan and
																b.Kode_Stock_Owner = d.Kode_Stock_Owner and
																b.Kode_Barang = d.Kode_Barang
										 left join Data_TF e
												   on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number = e.Serial_Number
									where a.Status is null
									  and a.Kode_Perusahaan = '{KodePerusahaan}'
									  and a.No_Production_Order = '{noSplit}'
									group by a.Kode_Perusahaan, a.No_Transaksi, a.No_Production_Order, b.Kode_Barang, b.Nilai_Produksi
								)

				select a.No_Transaksi, f.Tanggal, f.Jam, f.Proses, c.Kode_Stock_Owner, c.Kode_Barang, g.Nama, c.satuan,
					   round((c.Jumlah / d.Hasil) * isnull(a.Qty_Batch, 2), 4) as Nilai_Formula,
    					e.Nilai_Produksi, e.Nilai_Produksi_Tambahan
				from Emi_Split_Production_Order a
					 inner join EMI_Order_Produksi b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur
					 inner join EMI_Transaksi_Formulator_Detail_Bahan c
								on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Formula = c.No_Faktur
					 inner join Emi_Transaksi_Formulator d on c.Kode_Perusahaan = c.Kode_Perusahaan and c.No_Faktur = d.No_Faktur
					 inner join Detail_GI e on a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = e.No_Production_Order and c.Kode_Barang = e.Kode_Barang
					 inner join Emi_Production_Results_HPP f
								on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi
					 inner join Barang g on c.Kode_Perusahaan = g.Kode_Perusahaan and c.Kode_Stock_Owner = g.Kode_Stock_Owner and
											c.Kode_Barang = g.Kode_Barang
				where a.Status is null and b.Status is null and d.Status is null
				  and a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Transaksi = '{noSplit}'
				  {Filter}
				  order by f.proses, c.Kode_Barang
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read()
					Dim Lv As ListViewItem

					Dim NilaiFormula As Double = Val(HilangkanTanda(Dr("Nilai_Formula")))
					Dim NilaiProduksi As Double = Val(HilangkanTanda(Dr("Nilai_Produksi")))
					Dim NilaiProduksiTambahan As Double = Val(HilangkanTanda(Dr("Nilai_Produksi_Tambahan")))

					If boleh_lihat_data = True Then
						Lv = Lv_DataDetail.Items.Add("")
						Lv.SubItems.Add(Dr("Proses"))

						If General_Class.CekNULL(Dr("Tanggal")) = "" Then
							Lv.SubItems.Add("-")
						Else
							Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
						End If

						If General_Class.CekNULL(Dr("Jam")) = "" Then
							Lv.SubItems.Add("-")
						Else
							Lv.SubItems.Add(Dr("Jam"))
						End If

						Lv.SubItems.Add(Dr("Kode_Barang"))

						Lv.SubItems.Add(Dr("nama"))
						Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Formula"))), "N4"))
						Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Produksi"))), "N4"))
						Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Produksi_Tambahan"))), "N4"))
						Lv.SubItems.Add(Dr("satuan"))

						Lv.SubItems.Add(Format(Val(HilangkanTanda((NilaiProduksi + NilaiProduksiTambahan) - NilaiFormula)), "N4"))
						Lv.SubItems.Add(Format(Val(HilangkanTanda(((NilaiProduksi + NilaiProduksiTambahan) - NilaiFormula) / NilaiFormula * 100)), "N2"))

						Lv.SubItems.Add(Dr("No_Transaksi"))
					Else
						Lv = Lv_DataDetail.Items.Add("")
						Lv.SubItems.Add(Dr("Proses"))

						If General_Class.CekNULL(Dr("Tanggal")) = "" Then
							Lv.SubItems.Add("-")
						Else
							Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
						End If

						If General_Class.CekNULL(Dr("Jam")) = "" Then
							Lv.SubItems.Add("-")
						Else
							Lv.SubItems.Add(Dr("Jam"))
						End If

						Lv.SubItems.Add(Dr("Kode_Barang"))

						Lv.SubItems.Add("X")
						Lv.SubItems.Add("0")
						Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Produksi"))), "N4"))
						Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Produksi_Tambahan"))), "N4"))
						Lv.SubItems.Add(Dr("satuan"))
						Lv.SubItems.Add("0")

						Lv.SubItems.Add("0")

						Lv.SubItems.Add(Dr("No_Transaksi"))
					End If

					Total_Formula += NilaiFormula
					Total_Produksi += (NilaiProduksi + NilaiProduksiTambahan)
				Loop
			End Using

			Txt_TotNilaiFormula.Text = Format(Total_Formula, "N4")
			Txt_TotNilaiPRoduksi.Text = Format(Total_Produksi, "N4")

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Cari_GR_Click(sender As Object, e As EventArgs) Handles Btn_Cari_GR.Click
		If Cmb_Filter_GR.SelectedIndex > 0 Then
			If Txt_Filter_GR.Text.Trim.Length = 0 Then
				MessageBox.Show("Harap Isi Dahulu Filter GR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Txt_Filter_GR.Focus()
				Exit Sub
			End If
		End If

		LoadDataGR(1)

	End Sub

	Private Sub Btn_Cetak_GR_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_GR.Click
		If Batch.Items.Count = 0 Then
			MessageBox.Show("Lakukan Get Data Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Btn_Cari_GR.Focus()
			Exit Sub
		End If

		If MessageBox.Show("Yakin Ingin Cetak Detail Batch Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Handle_Cetak_Detail()
	End Sub

	Private Sub Handle_Cetak_Detail()

		Dim ds As DataSet = Nothing

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'==================
			'=     FILTER     =
			'==================
			Dim Filter_FG As String = ""
			Dim Filter_SCP As String = ""
			Dim IndexCmb As Integer = Cmb_Filter_GR.SelectedIndex

			If IndexCmb > 0 Then
				Dim filterText As String = Txt_Filter_GR.Text.Trim()
				Dim selectedFilter = FilterGR(IndexCmb)

				Filter_FG &= $" AND {selectedFilter.Sql_FG} like '%{filterText}%' "

				If IndexCmb = 1 Then
					If filterText = "0" Then
						Filter_SCP = $" AND 1=1 "
					Else
						Filter_SCP = " AND 1=2 "
					End If
				Else
					Filter_SCP = $" AND {selectedFilter.Sql_SCP} LIKE '%{filterText}%' "
				End If
			End If

			SQL = $"
				Select c.Tahap, c.Batch_Number, b.Tanggal, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, b.Kode_Barang,
					   {If(boleh_lihat_data, "d.Nama as Barang,", "")}
					   sum(c.Jumlah) as jumlah, b.Satuan
				From Emi_Production_Results a,
					 EMI_Production_Results_Detail_Barang b,
					 Emi_Production_Results_Detail_Pallet c,
					 barang d,
					 Emi_Split_Production_Order e
				Where a.kode_perusahaan = b.kode_Perusahaan
				  And b.kode_perusahaan = c.kode_Perusahaan
				  and b.Kode_Perusahaan = d.Kode_Perusahaan
				  and a.Kode_Perusahaan = e.Kode_Perusahaan
				  and a.No_Production_Order = e.No_Transaksi
				  And a.No_Transaksi = b.no_transaksi
				  And b.No_Transaksi = c.no_transaksi
				  And b.Proses = c.Proses
				  and b.Kode_Stock_Owner = d.Kode_Stock_Owner
				  and b.Kode_Barang = d.Kode_Barang
				  And a.status Is null
				  and a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Production_Order = '{noSplit}'
				  {Filter_FG}
				group by a.Kode_Perusahaan, e.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tanggal, b.Jam, c.Batch_Number,
						 (c.Qr_Code + '-' + c.Kode_Unik_Berjalan), c.Kode_Unik_Berjalan, b.Kode_Stock_Owner, b.Kode_Barang, d.Nama,
						 b.Satuan, c.Tahap

				union all

				Select 0 as Tahap, c.Batch_Number, b.Tanggal, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, b.Kode_Barang_scrap as Kode_Barang,
				       {If(boleh_lihat_data, "d.Nama as Barang,", "")}
					   sum(c.Jumlah) as jumlah,
					   b.Satuan_scrap as Satuan
				From Emi_Production_Results a,
					 EMI_Production_Results_Detail_Barang b,
					 EMI_Production_Results_Detail_Scrap c,
					 barang d,
					 Emi_Split_Production_Order e
				Where a.kode_perusahaan = b.kode_Perusahaan
				  And b.kode_perusahaan = c.kode_Perusahaan
				  and b.Kode_Perusahaan = d.Kode_Perusahaan
				  and a.Kode_Perusahaan = e.Kode_Perusahaan
				  and a.No_Production_Order = e.No_Transaksi
				  And a.No_Transaksi = b.no_transaksi
				  And b.No_Transaksi = c.no_transaksi
				  And b.Proses = c.Proses
				  and b.Kode_Stock_Owner = d.Kode_Stock_Owner
				  and b.Kode_Barang_scrap = d.Kode_Barang
				  And a.status Is null
				  and a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Production_Order = '{noSplit}'
				  {Filter_SCP}
				group by a.Kode_Perusahaan, e.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tanggal, b.Jam, c.Batch_Number,
						 (c.Qr_Code + '-' + c.Kode_Unik_Berjalan), c.Kode_Unik_Berjalan, b.Kode_Stock_Owner, b.Kode_Barang_scrap,
						 d.Nama, b.Satuan_scrap
			"
			ds = BindingTrans(SQL)

			If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Tidak ada data untuk di-export!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
				Exit Sub
			End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try

			'==============================================
			'=     SET DATA DARI DB MENJADI DATATABLE     =
			'==============================================
			Dim dt As DataTable = ds.Tables(0)

			'=========================
			'=     SETTING EXCEL     =
			'=========================
			Dim cultureID As New System.Globalization.CultureInfo("id-ID")
			Dim config As New ExcelExportHelper.ExportConfig() With {
				.FileName = $"Laporan_Detail_Batch_Split_{noSplit}_{Now.ToString("dd_MM_yyyy_HH_mm")}",
				.SheetName = $"Split {noSplit}",
				.FreezePanes = True,
				.FooterText = $"{Now.ToString("dd MMMM yyyy HH:mm:ss", cultureID)} | {UserID}"
			}

			'=============================
			'=     FORMAT KOLOM TEKS     =
			'=============================
			Dim textCols() As String = {"A", "B", "D", "E"}
			For Each idx As String In textCols
				config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(idx, "@", ExcelExportHelper.ExcelAlignment.Left, forceText:=True))
			Next

			config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat("G", "@", ExcelExportHelper.ExcelAlignment.Center, forceText:=True))

			'================================
			'=     FORMAT KOLOM TANGGAL     =
			'================================
			config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat("C", "dd MMMM yyyy", ExcelExportHelper.ExcelAlignment.Center, forceText:=False))

			'==============================
			'=     FORMAT KOLOM ANGKA     =
			'==============================
			config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat("F", "#,##0", ExcelExportHelper.ExcelAlignment.Right, forceNumber:=True))

			config.ConditionalRules.Add(New ExcelExportHelper.ConditionalRule(
				type:=ExcelExportHelper.ConditionalRule.RuleType.ColumnEquals,
				conditionColIdx:=6,
				conditionValue:="Pcs",
				targetColIdx:=5,
				numberFormat:="#,##0"
			))
			config.ConditionalRules.Add(New ExcelExportHelper.ConditionalRule(
				type:=ExcelExportHelper.ConditionalRule.RuleType.ColumnEquals,
				conditionColIdx:=6,
				conditionValue:="Kg",
				targetColIdx:=5,
				numberFormat:="#,##0.0000"
			))

			Dim titleText As String = $"LAPORAN DETAIL BATCH SPLIT {noSplit}"
			Dim headerRowTitle As New ExcelExportHelper.HeaderRow()
			headerRowTitle.AddCell(New ExcelExportHelper.HeaderCell(titleText, rowSpan:=1, colSpan:=dt.Columns.Count, backColor:=Color.WhiteSmoke))
			config.Headers.Add(headerRowTitle)

			Dim bgHeader As Color = Color.FromArgb(180, 198, 231)
			Dim headerRowData As New ExcelExportHelper.HeaderRow()
			For Each col As DataColumn In dt.Columns
				Dim caption As String = col.ColumnName.Replace("_", " ")
				caption = cultureID.TextInfo.ToTitleCase(caption.ToLower())
				headerRowData.AddCell(New ExcelExportHelper.HeaderCell(caption, backColor:=bgHeader))
			Next
			config.Headers.Add(headerRowData)

			ExcelExportHelper.ExportFromDataTable(dt, config)
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		End Try

	End Sub

	Private Sub Dgv_GR_Batch_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_GR_Batch.CellClick
		If Dgv_GR_Batch.Rows.Count = 0 Then Exit Sub
		If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim SelectedBatch As String = Dgv_GR_Batch.CurrentRow.Cells(0).Value.ToString.Trim

			Lv_GR_Detail_Pallet.Items.Clear()
			Lv_GR_Detail_Scrap.Items.Clear()
			Txt_GR_Rekap_Total.Text = ""
			Txt_GR_Rekap_Total_GR.Text = ""
			Txt_GR_Rekap_Total_Scrap.Text = ""
			Dim Total_Terpakai As Double = 0
			Dim Total_GR As Double = 0
			Dim Satuan_Terpakai As String = ""
			Dim Satuan_GR As String = ""
			Dim Sql_Det_Batch As String = ""

			Dim TotalScrap As Double = 0
			Dim SatuanScrap As String = ""

			If SelectedBatch.Trim = "Waste" Then
				Lv_GR_Detail_Pallet.Size = New Point(1035, 364)
				Label23.Visible = False
				Panel10.Visible = False
				Lv_GR_Detail_Scrap.Visible = False
				Sql_Det_Batch = $"
					select (c.Qr_Code+'-'+c.Kode_Unik_Berjalan) as Barcode, b.Tanggal as Tgl_Produksi, b.Proses as Batch_Sistem, isnull(c.Batch, 0) as Batch_Input, '-' as Troli,
						   sum(c.Jumlah) as Jumlah_Input, c.Satuan, sum(c.Jumlah) as Jumlah_Terpakai, c.Satuan as Satuan_Terpakai
					from Emi_Production_Results a
						inner join EMI_Production_Results_Detail_Barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
						inner join EMI_Production_Results_Detail_Scrap c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses
					where a.Status is null
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and c.Jumlah <> 0
					and a.No_Production_Order = '{noSplit}'
					group by (c.Qr_Code+'-'+c.Kode_Unik_Berjalan), b.Tanggal, b.Proses, c.Satuan, c.Batch
					order by b.Tanggal, c.Batch, (c.Qr_Code+'-'+c.Kode_Unik_Berjalan)
				"
			Else
				Lv_GR_Detail_Pallet.Size = New Point(1035, 140)
				Label23.Visible = True
				Panel10.Visible = True
				Lv_GR_Detail_Scrap.Visible = True
				Sql_Det_Batch = $"
					select (d.Qr_Code+'-'+d.Kode_Unik_Berjalan) as Barcode, d.Tgl_Produksi, c.Proses as Batch_Sistem, d.Tahap as Batch_Input, d.Troli, d.Jumlah as Jumlah_Input, d.Satuan,
						   ((d.Jumlah * e.Berat) / 1000) as Jumlah_Terpakai, 'KG' as Satuan_Terpakai
					from Emi_Production_Results a
						inner join EMI_Production_Results_Detail_Barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
						inner join Emi_Production_Results_HPP c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses
						inner join Emi_Production_Results_Detail_Pallet d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Transaksi = d.No_Transaksi and c.Urut = d.Urut_HPP
						inner join barang e on b.Kode_Perusahaan = e.Kode_Perusahaan and b.Kode_Stock_Owner = e.Kode_Stock_Owner and b.Kode_Barang = e.Kode_Barang
					where a.Status is null
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and d.jumlah <> 0
					and a.No_Production_Order = '{noSplit}'
					and d.Tahap = {SelectedBatch.Trim}
					order by d.Tgl_Produksi, (d.Qr_Code+'-'+d.Kode_Unik_Berjalan)
				"
			End If
			Using Dr = OpenTrans(Sql_Det_Batch)
				If Dr.Read Then
					Do
						Dim Lv As ListViewItem
						Lv = Lv_GR_Detail_Pallet.Items.Add(Dr("Barcode"))
						Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
						Lv.SubItems.Add(Dr("Batch_Input"))
						Lv.SubItems.Add(Dr("Troli"))
						Lv.SubItems.Add(Dr("Jumlah_Input"))
						Lv.SubItems.Add(Dr("Satuan"))
						Lv.SubItems.Add(Dr("Jumlah_Terpakai"))
						Lv.SubItems.Add(Dr("Satuan_Terpakai"))

						Total_Terpakai += Val(HilangkanTanda(Dr("Jumlah_Terpakai")))
						Total_GR += Val(HilangkanTanda(Dr("Jumlah_Input")))
						Satuan_Terpakai = Dr("Satuan_Terpakai")
						Satuan_GR = Dr("Satuan")

						TotalScrap += Val(HilangkanTanda(Dr("Jumlah_Input")))
						SatuanScrap = If(General_Class.CekNULL(Dr("Satuan")) = "", "", Dr("Satuan"))

					Loop While Dr.Read
				End If
			End Using

			'=================
			'=     SCRAP     =
			'=================
			If Not SelectedBatch.Trim = "Waste" Then
				TotalScrap = 0
				SatuanScrap = ""
				Lv_GR_Detail_Scrap.Items.Clear()
				SQL = $"
					select (c.Qr_Code+'-'+c.Kode_Unik_Berjalan) as Barcode, b.Tanggal as Tgl_Produksi, b.Proses as Batch_Sistem, isnull(c.Batch, 0) as Batch_Input, '-' as Troli,
						   sum(c.Jumlah) as Jumlah_Input, c.Satuan, sum(c.Jumlah) as Jumlah_Terpakai, c.Satuan as Satuan_Terpakai
					from Emi_Production_Results a
						inner join EMI_Production_Results_Detail_Barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
						inner join EMI_Production_Results_Detail_Scrap c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses
					where a.Status is null
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and c.Jumlah <> 0
					and a.No_Production_Order = '{noSplit}'
					and c.Batch = '{SelectedBatch.Trim}'
					group by (c.Qr_Code+'-'+c.Kode_Unik_Berjalan), b.Tanggal, b.Proses, c.Satuan, c.Batch
					order by b.Tanggal, c.Batch, (c.Qr_Code+'-'+c.Kode_Unik_Berjalan)
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Do
							Dim Lv As ListViewItem
							Lv = Lv_GR_Detail_Scrap.Items.Add(Dr("Barcode"))
							Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
							Lv.SubItems.Add(Dr("Batch_Input"))
							Lv.SubItems.Add(Dr("Troli"))
							Lv.SubItems.Add(Dr("Jumlah_Input"))
							Lv.SubItems.Add(Dr("Satuan"))
							Lv.SubItems.Add(Dr("Jumlah_Terpakai"))
							Lv.SubItems.Add(Dr("Satuan_Terpakai"))

							Total_Terpakai += Val(HilangkanTanda(Dr("Jumlah_Terpakai")))
							TotalScrap += Val(HilangkanTanda(Dr("Jumlah_Input")))
							SatuanScrap = If(General_Class.CekNULL(Dr("Satuan")) = "", "", Dr("Satuan"))
						Loop While Dr.Read

					End If
				End Using

			End If

			Txt_GR_Rekap_Total.Text = $"{Format(Total_Terpakai, "N4")} {Satuan_Terpakai}"
			Dim TotalGR As String = 0
			If String.Equals(Satuan_GR.Trim(), "Pcs", StringComparison.OrdinalIgnoreCase) Then
				TotalGR = Format(Total_GR, "N0")
			Else
				TotalGR = Format(Total_GR, "N4")
			End If
			Txt_GR_Rekap_Total_GR.Text = $"{If(SelectedBatch.Trim = "Waste", Format(0, "N2"), TotalGR)} {If(SelectedBatch.Trim = "Waste", "", Satuan_GR)}"
			Txt_GR_Rekap_Total_Scrap.Text = $"{Format(TotalScrap, "N4")} {SatuanScrap}"

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
		If Dgv_GR_Batch.Rows.Count = 0 Then Exit Sub

		Dim Batch As String = Dgv_GR_Batch.CurrentRow.Cells(0).Value.ToString.Trim
		Dim NoSplit As String = Txt_GR_Rekap_Split.Text.Trim
		If String.IsNullOrWhiteSpace(NoSplit) Then
			MessageBox.Show("Terjadi Kesalaham No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If (MessageBox.Show($"Yakin Ingin Melakukan Validasi Batch {Batch} Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim NoProductionResult As String = ""

			'===========================
			'=     CEK ROLE BUTTON     =
			'===========================
			If CekButtonRole("Validasi_GR_1_Batch") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Validasi GR Per Batch", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'========================================
			'=     CEK APAKAH SPLIT DI BATALKAN     =
			'========================================
			SQL = $"
				select a.No_Transaksi, a.Status, b.Flag_Validasi
				from Emi_Production_Results a
					inner join Emi_Production_Results_HPP b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Production_Order = '{NoSplit}'
				and b.Proses = '{Batch}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Production Result Terhadap Split {NoSplit} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					ElseIf General_Class.CekNULL(Dr("Flag_Validasi")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Batch {Batch} Sudah Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					NoProductionResult = Dr("No_Transaksi").ToString.Trim
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Batch {Batch} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================================
			'=     CEK APAKAH PO SUDAH SELESAI     =
			'=======================================
			SQL = $"
				select Flag_Hasil_Produksi_GI, Flag_Hasil_Produksi_GR
				from Emi_Split_Production_Order
				where Kode_Perusahaan = '{KodePerusahaan}'
				and no_transaksi = '{NoSplit}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Flag_Hasil_Produksi_GR")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("GR Sudah Selesai, Tidak Bisa Diselesaikan Lagi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					If General_Class.CekNULL(Dr("Flag_Hasil_Produksi_GI")) <> "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Validasi GI Terlebih Dahulu . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================
			'=     UPDATE FLAG     =
			'=======================
			If String.IsNullOrWhiteSpace(NoProductionResult) Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Terjadi Kesalahan, No Production Result Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			SQL = $"
				INSERT INTO N_EMI_LOG_Production_Results_Validation_Batch
					(Kode_Perusahaan, No_Transaksi, No_Production_Order, Batch, Jenis, UserID, Tanggal, Jam)
				VALUES
					('{KodePerusahaan}', '{NoProductionResult}', '{NoSplit}', '{Batch}', 'VALIDASI',
					'{UserID}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}')
			"
			ExecuteTrans(SQL)

			SQL = $"
				update Emi_Production_Results_HPP set Flag_Validasi = 'Y',
				UserID_Validasi = '{UserID}', Tanggal_Validasi = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi = '{Format(tgl_skg, "HH:mm:ss")}'
				where Kode_Perusahaan = '{KodePerusahaan}'
				and No_Transaksi = '{NoProductionResult}'
				and Proses = '{Batch}'
			"
			ExecuteTrans(SQL)

			'===================================================
			'=     CEK APAKAH SEMUA BATCH SUDAH DIVALIDASI     =
			'===================================================
			SQL = $"
				select a.No_Transaksi, a.Status, b.Flag_Validasi
				from Emi_Production_Results a
					 inner join Emi_Production_Results_HPP b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
				where a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.No_Production_Order = '{NoSplit}'
					and b.Flag_Validasi is null
			"
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					SQL = $"
						update Emi_Split_Production_Order
						set Flag_Hasil_Produksi_GR = 'Y',
							UserID_Selesai_GR      = '" & UserID & "',
							Tgl_Hasil_Produksi_GR  = '" & Format(tgl_skg, "yyyy-MM-dd") & "',
							Jam_Hasil_Produksi_GR  = '" & Format(tgl_skg, "HH:mm:ss") & "'
						where Kode_Perusahaan = '" & KodePerusahaan & "'
							and no_transaksi = '" & NoSplit & "'
					"
					ExecuteTrans(SQL)
				End If
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Batch {Batch} Berhasil Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		LoadDataGR(1)

	End Sub

	'================================================================================================================================================================
	'=     HANDLE KEY
	'================================================================================================================================================================

	Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
		If Dgv_GR_Batch.Rows.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If

		Dim mousePos As Point = Dgv_GR_Batch.PointToClient(Control.MousePosition)
		Dim info As DataGridView.HitTestInfo = Dgv_GR_Batch.HitTest(mousePos.X, mousePos.Y)

		If info.RowIndex < 0 OrElse info.ColumnIndex < 0 Then
			e.Cancel = True
			Exit Sub
		End If

		'=========================
		'=     CEK JENIS ROW     =
		'=========================
		Dim Jenis As String = Dgv_GR_Batch.Rows(info.RowIndex).Cells(0).Value.ToString.Trim

		If Jenis.ToUpper.Trim = "WASTE" Then
			e.Cancel = True
			Exit Sub
		End If

		Dgv_GR_Batch.ClearSelection() ' Opsional: bersihkan seleksi sebelumnya
		Dgv_GR_Batch.Rows(info.RowIndex).Selected = True

		Dgv_GR_Batch.CurrentCell = Dgv_GR_Batch.Rows(info.RowIndex).Cells(info.ColumnIndex)
	End Sub

	Private Sub Cmb_Filter_Batch_Pn1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter_Batch_Pn1.KeyPress
		If e.KeyChar = Chr(13) Then Cmb_KdBarang_Pn1.Focus()
	End Sub

	Private Sub Cmb_KdBarang_Pn1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KdBarang_Pn1.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari_Pn1.Focus()
	End Sub

	Private Sub Cmb_Filter_GR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter_GR.SelectedIndexChanged
		If Cmb_Filter_GR.Items.Count = 0 Then Exit Sub
		Txt_Filter_GR.Text = ""
		If Cmb_Filter_GR.SelectedIndex = 0 Then
			Txt_Filter_GR.Enabled = False
			Txt_Filter_GR.BackColor = Color_Txt_Disable
		Else
			Txt_Filter_GR.Enabled = True
			Txt_Filter_GR.BackColor = Color.White
		End If
	End Sub

	Private Sub Btn_Batch_All_Click(sender As Object, e As EventArgs) Handles Btn_Batch_All.Click
		LoadDataGR(1)
	End Sub

	Private Sub Btn_Batch_Sdh_Validasi_Click(sender As Object, e As EventArgs) Handles Btn_Batch_Sdh_Validasi.Click
		LoadDataGR(2)
	End Sub

	Private Sub Btn_Batch_Blm_Validasi_Click(sender As Object, e As EventArgs) Handles Btn_Batch_Blm_Validasi.Click
		LoadDataGR(3)
	End Sub

	Private Sub LvDataRekap_MouseMove(sender As Object, e As MouseEventArgs) Handles LvDataRekap.MouseMove, Lv_DataDetail.MouseMove, Batch.MouseMove, Lv_GR_Detail_Pallet.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	Private Sub Dgv_GR_Batch_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_GR_Batch.MouseMove
		HandleDataGridViewHover(sender, e)
	End Sub

	Private Sub EnableDoubleBuffer(lvw As ListView)
		Dim t As Type = lvw.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(lvw, True, Nothing)
	End Sub

	Private Sub EnableDoubleBufferDGV(dgv As DataGridView)
		Dim t As Type = dgv.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(dgv, True, Nothing)
	End Sub

	Private Sub HandleListViewHover(lvw As ListView, e As MouseEventArgs)
		Dim hit As ListViewHitTestInfo = lvw.HitTest(e.Location)

		lvw.Cursor = If(hit.Item IsNot Nothing, Cursors.Hand, Cursors.Default)

		If hit.Item IsNot lastHoverItem Then
			lvw.BeginUpdate()

			If lastHoverItem IsNot Nothing Then
				lastHoverItem.BackColor = originalItemColor
			End If

			If hit.Item IsNot Nothing AndAlso hit.Item.Tag Is Nothing Then
				lastHoverItem = hit.Item
				originalItemColor = lastHoverItem.BackColor

				Dim amt As Integer = 10
				lastHoverItem.BackColor = Color.FromArgb(
				Math.Max(0, originalItemColor.R - amt),
				Math.Max(0, originalItemColor.G - amt),
				Math.Max(0, originalItemColor.B - amt)
			)
			Else
				lastHoverItem = Nothing
			End If

			lvw.EndUpdate()
		End If
	End Sub

	Private Sub HandleDataGridViewHover(dgv As DataGridView, e As MouseEventArgs)
		Dim hit As DataGridView.HitTestInfo = dgv.HitTest(e.X, e.Y)

		dgv.Cursor = If(hit.Type = DataGridViewHitTestType.Cell, Cursors.Hand, Cursors.Default)

		If hit.RowIndex <> lastIndex Then

			If lastIndex >= 0 AndAlso lastIndex < dgv.Rows.Count Then
				For Each cell As DataGridViewCell In dgv.Rows(lastIndex).Cells
					If Not TypeOf cell Is DataGridViewButtonCell Then
						cell.Style.BackColor = Color.Empty '
					End If
				Next
			End If

			If hit.Type = DataGridViewHitTestType.Cell AndAlso hit.RowIndex >= 0 Then
				lastIndex = hit.RowIndex

				Dim currentRow = dgv.Rows(lastIndex)
				originalColor = currentRow.DefaultCellStyle.BackColor
				Dim displayColor As Color = currentRow.InheritedStyle.BackColor

				Dim amount As Integer = 23

				Dim hoverColor As Color = Color.FromArgb(
					Math.Max(0, displayColor.R - amount),
					Math.Max(0, displayColor.G - amount),
					Math.Max(0, displayColor.B - amount)
				)

				For Each cell As DataGridViewCell In currentRow.Cells
					If Not TypeOf cell Is DataGridViewButtonCell Then
						cell.Style.BackColor = hoverColor
					End If
				Next
			Else
				lastIndex = -1
			End If
		End If
	End Sub

	Private Sub SetButtonStyle(ByVal btn As RoundedButton, ByVal isActive As Boolean)
		If isActive Then
			btn.BackColor = Color.FromArgb(15, 86, 122)
			btn.ForeColor = Color.White
			btn.BorderColor = Color.FromArgb(13, 74, 105)

			btn.HoverBackColor = Color.FromArgb(15, 86, 122)
			btn.HoverForeColor = Color.White
			btn.HoverBorderColor = Color.FromArgb(15, 86, 122)
		Else
			btn.BackColor = Color.White
			btn.ForeColor = Color.Black
			btn.BorderColor = Color.FromArgb(215, 215, 215)

			btn.HoverBackColor = Color.FromArgb(240, 240, 240)
			btn.HoverForeColor = Color.White
			btn.HoverBorderColor = Color.FromArgb(215, 215, 215)
		End If
	End Sub

End Class