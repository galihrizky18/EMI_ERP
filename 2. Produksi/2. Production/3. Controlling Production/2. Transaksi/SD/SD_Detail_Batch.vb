Public Class SD_Detail_Batch

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

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

	Private Sub SD_Detail_Batch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBuffer(Lv_DataGr)
		EnableDoubleBuffer(Batch)
		EnableDoubleBuffer(LvDataRekap)
		EnableDoubleBuffer(Lv_DataDetail)

		Panel_GI.Location = New Point(20, 57)
		Panel_GR.Location = New Point(20, 57)

		If asal = "GI" Then
			Panel_GI.Visible = True
			Panel_GR.Visible = False

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
			'============================================================================
			'=     PANEL 1
			'============================================================================

			If boleh_lihat_data = True Then
				Lv_DataDetail.Columns.Clear() : Lv_DataDetail.Items.Clear()
				Lv_DataDetail.Columns.Add("", 0, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Batch", 70, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Jam", 90, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Kode Barang", 140, HorizontalAlignment.Left)
				Lv_DataDetail.Columns.Add("Nama", 220, HorizontalAlignment.Left)
				Lv_DataDetail.Columns.Add("Nilai Produksi", 150, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Nilai Formula", 150, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Selisih ", 150, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Selisih %", 120, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("no_Faktur", 0, HorizontalAlignment.Left)
				Lv_DataDetail.Columns.Add("Material Tambahan (KG)", 150, HorizontalAlignment.Right)
			Else
				Lv_DataDetail.Columns.Clear() : Lv_DataDetail.Items.Clear()
				Lv_DataDetail.Columns.Add("", 0, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Batch", 70, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Tanggal", 180, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Jam", 160, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Kode Barang", 220, HorizontalAlignment.Left)
				Lv_DataDetail.Columns.Add("Nama", 0, HorizontalAlignment.Left)
				Lv_DataDetail.Columns.Add("Nilai Produksi", 240, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Nilai Formula", 0, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Satuan", 130, HorizontalAlignment.Center)
				Lv_DataDetail.Columns.Add("Selisih ", 0, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("Selisih %", 0, HorizontalAlignment.Right)
				Lv_DataDetail.Columns.Add("no_Faktur", 0, HorizontalAlignment.Left)
				Lv_DataDetail.Columns.Add("Material Tambahan (KG)", 150, HorizontalAlignment.Right)
			End If

			Lv_DataDetail.View = View.Details

			'============================================================================
			LvDataRekap.Columns.Clear() : Lv_DataDetail.Items.Clear()
			LvDataRekap.Columns.Add("", 0, HorizontalAlignment.Right)
			LvDataRekap.Columns.Add("Batch", 130, HorizontalAlignment.Center)
			LvDataRekap.Columns.Add("Tanggal", 180, HorizontalAlignment.Center)
			LvDataRekap.Columns.Add("Jam", 180, HorizontalAlignment.Center)
			LvDataRekap.Columns.Add("Nilai Dosing (KG)", 180, HorizontalAlignment.Right)
			LvDataRekap.Columns.Add("Selisih Dosing (%)", 180, HorizontalAlignment.Right)
			LvDataRekap.Columns.Add("Material Tambah(KG)", 180, HorizontalAlignment.Right)
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

			Lv_DataGr.Columns.Clear() : Lv_DataGr.Items.Clear()
			Lv_DataGr.Columns.Add("", 0, HorizontalAlignment.Center)
			Lv_DataGr.Columns.Add("Batch", 120, HorizontalAlignment.Center)
			Lv_DataGr.Columns.Add("Jumlah Dosing", 245, HorizontalAlignment.Right)
			Lv_DataGr.Columns.Add("Jumlah Selesai", 245, HorizontalAlignment.Right)
			Lv_DataGr.Columns.Add("Selisih", 245, HorizontalAlignment.Right)
			Lv_DataGr.Columns.Add("Satuan", 120, HorizontalAlignment.Center)
			'Hide
			Lv_DataGr.Columns.Add("Lv_NoResult", 0, HorizontalAlignment.Left)
			Lv_DataGr.View = View.Details

			Batch.Columns.Clear() : Batch.Items.Clear()
			Batch.Columns.Add("", 0, HorizontalAlignment.Center)
			Batch.Columns.Add("Batch Number", 150, HorizontalAlignment.Center)
			Batch.Columns.Add("Tanggal", 115, HorizontalAlignment.Center)
			Batch.Columns.Add("Lokasi", 180, HorizontalAlignment.Left)
			Batch.Columns.Add("Barcode", 300, HorizontalAlignment.Left)
			Batch.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
			Batch.Columns.Add("Jumlah", 180, HorizontalAlignment.Right)
			Batch.Columns.Add("Satuan", 120, HorizontalAlignment.Center)
			Batch.View = View.Details

		End If

		Kosong()
	End Sub

	Public Sub Kosong()

		Txt_TotNilaiFormula.Text = ""
		Txt_TotNilaiPRoduksi.Text = ""

		If asal = "GI" Then
			Btn_Cari_Pn1_Click(Btn_Cari_Pn1, New EventArgs)
		Else
			LoadDataGR()
		End If

	End Sub

	Private Sub LoadDataGR()
		'============================================================================
		'=     PANEL 2
		'============================================================================

		Try
			OpenConn()

			Lv_DataGr.Items.Clear()
			SQL = "select a.No_Transaksi, a.No_PO, b.No_Transaksi as No_Result, c.Proses as Batch_Number, c.Jumlah_Dosing, c.Jumlah_Terpakai as Jumlah_Selesai, "
			SQL = SQL & "(c.Jumlah_Dosing - c.Jumlah_Terpakai) as Selisih, c.Satuan "
			SQL = SQL & "from Emi_Split_Production_Order a , Emi_Production_Results b, Emi_Production_Results_HPP c  "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan  "
			SQL = SQL & "and a.No_Transaksi = No_Production_Order  "
			SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
			SQL = SQL & "and a.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and c.Tanggal is not null "
			SQL = SQL & "and a.No_Transaksi = '" & noSplit & "' "
			SQL = SQL & "order by c.Proses "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_DataGr.Items.Add("")
					Lv.SubItems.Add(Dr("Batch_Number"))
					Lv.SubItems.Add(Format(Dr("Jumlah_Dosing"), "N4"))
					Lv.SubItems.Add(Format(Dr("Jumlah_Selesai"), "N4"))
					Lv.SubItems.Add(Format(Dr("Selisih"), "N4"))
					Lv.SubItems.Add(Dr("Satuan"))
					'Hdide
					Lv.SubItems.Add(Dr("No_Result"))
				Loop
			End Using

			Batch.Items.Clear()
			SQL = "Select a.Kode_Perusahaan, e.No_PO, a.No_Production_Order as No_Split, a.No_Transaksi as No_Result, b.Tanggal, b.Jam, c.Batch_Number, c.Qr_Code+'-'+c.Kode_Unik_Berjalan as Barcode,  "
			SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang, d.Nama as Barang, sum(c.Jumlah) as jumlah, b.Satuan  "
			SQL = SQL & "From Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, Emi_Production_Results_Detail_Pallet c, barang d, Emi_Split_Production_Order e "
			SQL = SQL & "Where a.kode_perusahaan = b.kode_Perusahaan And b.kode_perusahaan = c.kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and a.No_Production_Order = e.No_Transaksi "
			SQL = SQL & "And a.No_Transaksi = b.no_transaksi "
			SQL = SQL & "And b.No_Transaksi = c.no_transaksi And b.Proses = c.Proses "
			SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
			SQL = SQL & "And a.status Is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
			SQL = SQL & "group by a.Kode_Perusahaan, e.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tanggal, b.Jam, c.Batch_Number, "
			SQL = SQL & "(c.Qr_Code+'-'+c.Kode_Unik_Berjalan), c.Kode_Unik_Berjalan, b.Kode_Stock_Owner, b.Kode_Barang, d.Nama, b.Satuan "

			SQL = SQL & "union all "

			SQL = SQL & "Select a.Kode_Perusahaan, e.No_PO, a.No_Production_Order as No_Split, a.No_Transaksi as No_Result, b.Tanggal, b.Jam, c.Batch_Number, c.Qr_Code+'-'+c.Kode_Unik_Berjalan as Barcode,  "
			SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang_scrap as Kode_Barang, d.Nama as Barang, sum(c.Jumlah) as jumlah, b.Satuan_scrap as Satuan "
			SQL = SQL & "From Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, EMI_Production_Results_Detail_Scrap c, barang d, Emi_Split_Production_Order e "
			SQL = SQL & "Where a.kode_perusahaan = b.kode_Perusahaan And b.kode_perusahaan = c.kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and a.No_Production_Order = e.No_Transaksi "
			SQL = SQL & "And a.No_Transaksi = b.no_transaksi "
			SQL = SQL & "And b.No_Transaksi = c.no_transaksi And b.Proses = c.Proses "
			SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang_scrap = d.Kode_Barang "
			SQL = SQL & "And a.status Is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
			SQL = SQL & "group by a.Kode_Perusahaan, e.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tanggal, b.Jam, c.Batch_Number, "
			SQL = SQL & "(c.Qr_Code+'-'+c.Kode_Unik_Berjalan), c.Kode_Unik_Berjalan, b.Kode_Stock_Owner, b.Kode_Barang_scrap, d.Nama, b.Satuan_scrap "
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

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
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

#Region "Kode Lama 15 Juni 2026"

			'SQL = "Select b.proses, b.Tanggal, b.Jam, "
			'SQL = SQL & "isnull((select sum(y.Nilai_Barang) from Emi_Production_Results_Detail y where "
			'SQL = SQL & "a.kode_perusahaan = y.kode_perusahaan And a.No_Transaksi = y.No_Transaksi And "
			'SQL = SQL & "b.Proses = y.Proses ),0) As Total_Dosing "
			'SQL = SQL & "From Emi_Production_Results a, Emi_Production_Results_HPP b Where "
			'SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi  "
			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.Status is null "
			'SQL = SQL & "and a.no_production_order = '" & noSplit & "' "

#End Region

			SQL = $"
				select a.No_Production_Order, b.proses, b.Tanggal, b.Jam, isnull(c.Nilai_Barang, 0) as Total_Dosing, isnull(d.Jumlah, 0) as Jumlah_RM_Tambah
				from Emi_Production_Results a
					inner join Emi_Production_Results_HPP b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi
					left join (
								  select sum(y.Nilai_Barang) as Nilai_Barang, y.Kode_Perusahaan, y.No_Transaksi, y.Proses
								  from Emi_Production_Results_Detail y
								  where y.status is null
								  group by y.Kode_Perusahaan, y.No_Transaksi, y.Proses
							  )	 c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses
					left join (
							select z.Kode_Perusahaan, z.No_Faktur_Order, sum(p.Jumlah) as Jumlah, z.batch
							from Emi_Material_Requisition z
								 inner join Emi_Material_Requisition_Det_Convert x
											on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
								 left join (
											   select q.Kode_Perusahaan, q.Urut_Material_Requisition_Convert, sum(q.Jumlah) as Jumlah
											   from (
														select y.Kode_Perusahaan, m.Jumlah, k.Urut_Material_Requisition_Convert
														from Tf_Stock_Parent y
															 inner join Tf_Stock k
																		on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur
															 inner join Tf_Stock_det l on k.Kode_Perusahaan = l.Kode_Perusahaan and
																						  k.No_Faktur = l.No_Faktur and
																						  k.Urut_Oto = l.Urut_TF
															 inner join TF_Stock_Det2 m on l.Kode_Perusahaan = m.Kode_Perusahaan and
																						   l.No_Faktur = m.No_Faktur and
																						   l.Urut_Oto = m.Urut_Det
														where y.Status is null

														union all

														select y.Kode_Perusahaan, m.Jumlah, k.Urut_Material_Requisition_Convert
														from N_EMI_Transaksi_Transfer_Stock_Sementara y
															 inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail k
																		on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur
															 inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det l
																		on k.Kode_Perusahaan = l.Kode_Perusahaan and
																		   k.No_Faktur = l.No_Faktur and k.Urut_Oto = l.Urut_TF
															 inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 m
																		on l.Kode_Perusahaan = m.Kode_Perusahaan and
																		   l.No_Faktur = m.No_Faktur and l.Urut_Oto = m.Urut_Det
														where y.Status is null
													) q
											   group by q.Kode_Perusahaan, q.Urut_Material_Requisition_Convert
										   ) p on x.Kode_Perusahaan = p.Kode_Perusahaan and x.Urut_Oto = p.Urut_Material_Requisition_Convert
							where z.Status is null
							  and z.Flag_Tambah = 'Y'
							group by z.Kode_Perusahaan, z.No_Faktur_Order, z.batch
						) d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Production_Order = d.No_Faktur_Order and b.proses = d.batch
				where a.Status is null
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Production_Order = '{noSplit}'
			"
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
					Lv.SubItems.Add(Format(Val(HilangkanTanda((Val(TxtJumlahBatch.Text) - Dr("Total_Dosing")) / Val(TxtJumlahBatch.Text) * 100)), "N4"))
					Lv.SubItems.Add(Format(Dr("Jumlah_RM_Tambah"), "N4"))

				Loop
			End Using

			Lv_DataDetail.Items.Clear()

			Dim Filter As String = ""
			If Not Cmb_Filter_Batch_Pn1.SelectedIndex = 0 Then
				Filter &= "and e.Proses = '" & Cmb_Filter_Batch_Pn1.Text & "' "
			End If
			If Not Cmb_KdBarang_Pn1.SelectedIndex = 0 Then
				Filter &= "and c.Kode_Barang = '" & Cmb_KdBarang_Pn1.Text & "' "
			End If

#Region "Kode Lama 15 Juni 2026"

			'SQL = "select a.No_Transaksi, e.Tanggal, e.Jam, e.Proses, c.Kode_Stock_Owner, c.Kode_Barang, f.nama, c.satuan, "
			'SQL = SQL & " "
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
				select a.No_Transaksi, e.Tanggal, e.Jam, e.Proses, c.Kode_Stock_Owner, c.Kode_Barang, f.nama, c.satuan,
					   round((c.Jumlah / (Frm.Hasil)) * isnull(a.Qty_Batch, 2), 4) as Nilai_Formula,
					   ISNULL((prs.Nilai_Produksi), 0) as Nilai_Produksi,
					   isnull(g.Jumlah, 0) as Jumlah_RM_Tambah
				from Emi_Split_Production_Order a
					inner join EMI_Order_Produksi b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur
					inner join EMI_Transaksi_Formulator_Detail_Bahan c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Formula = c.No_Faktur
					inner join Emi_Production_Results d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Production_Order
					inner join Emi_Production_Results_HPP e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.No_Transaksi = e.No_Transaksi
					inner join Barang f on c.Kode_Perusahaan = f.Kode_Perusahaan and c.Kode_Stock_Owner = f.Kode_Stock_Owner and c.Kode_Barang = f.Kode_Barang
					inner join Emi_Transaksi_Formulator Frm on c.Kode_Perusahaan = Frm.Kode_Perusahaan and c.No_Faktur = Frm.No_Faktur
					left join Emi_Production_Results_Detail prs on d.Kode_Perusahaan = prs.Kode_Perusahaan and d.No_Transaksi = prs.No_Transaksi and c.Kode_Barang = prs.Kode_Barang and e.Proses = prs.Proses
					left join (
						select z.Kode_Perusahaan, z.No_Faktur_Order, sum(p.Jumlah) as Jumlah, x.Kode_Barang, z.Batch
						from Emi_Material_Requisition z
							 inner join Emi_Material_Requisition_Det_Convert x
										on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
							 left join (
										   select q.Kode_Perusahaan, q.Urut_Material_Requisition_Convert, sum(q.Jumlah) as Jumlah
										   from (
													select y.Kode_Perusahaan, m.Jumlah, k.Urut_Material_Requisition_Convert
													from Tf_Stock_Parent y
														 inner join Tf_Stock k
																	on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur
														 inner join Tf_Stock_det l on k.Kode_Perusahaan = l.Kode_Perusahaan and
																					  k.No_Faktur = l.No_Faktur and
																					  k.Urut_Oto = l.Urut_TF
														 inner join TF_Stock_Det2 m on l.Kode_Perusahaan = m.Kode_Perusahaan and
																					   l.No_Faktur = m.No_Faktur and
																					   l.Urut_Oto = m.Urut_Det
													where y.Status is null

													union all

													select y.Kode_Perusahaan, m.Jumlah, k.Urut_Material_Requisition_Convert
													from N_EMI_Transaksi_Transfer_Stock_Sementara y
														 inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail k
																	on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur
														 inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det l
																	on k.Kode_Perusahaan = l.Kode_Perusahaan and
																	   k.No_Faktur = l.No_Faktur and k.Urut_Oto = l.Urut_TF
														 inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 m
																	on l.Kode_Perusahaan = m.Kode_Perusahaan and
																	   l.No_Faktur = m.No_Faktur and l.Urut_Oto = m.Urut_Det
													where y.Status is null
												) q
										   group by q.Kode_Perusahaan, q.Urut_Material_Requisition_Convert
									   ) p on x.Kode_Perusahaan = p.Kode_Perusahaan and x.Urut_Oto = p.Urut_Material_Requisition_Convert
						where z.Status is null
						  and z.Flag_Tambah = 'Y'
						group by z.Kode_Perusahaan, z.No_Faktur_Order, x.Kode_Barang, z.Batch
					) g on a.Kode_Perusahaan = g.Kode_Perusahaan and a.No_Transaksi = g.No_Faktur_Order and c.Kode_Barang = g.Kode_Barang and e.Proses = g.Batch
				where a.Status is null and b.Status is null and d.Status is null
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Transaksi = '{noSplit}'
				{Filter}
				order by e.Proses, c.Kode_Barang
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read()
					Dim Lv As ListViewItem
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
						Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Produksi"))), "N4"))
						Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Formula"))), "N4"))
						Lv.SubItems.Add(Dr("satuan"))
						Lv.SubItems.Add(Format(Val(HilangkanTanda((Dr("Nilai_Formula") - Dr("Nilai_Produksi")))), "N4"))

						Lv.SubItems.Add(Format(Val(HilangkanTanda((Dr("Nilai_Formula") - Dr("Nilai_Produksi")) / Dr("Nilai_Formula") * 100)), "N4"))

						Lv.SubItems.Add(Dr("No_Transaksi"))
						Lv.SubItems.Add(Format(Dr("Jumlah_RM_Tambah"), "N4"))
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
						Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Produksi"))), "N4"))
						Lv.SubItems.Add("0")
						Lv.SubItems.Add(Dr("satuan"))
						Lv.SubItems.Add("0")

						Lv.SubItems.Add("0")

						Lv.SubItems.Add(Dr("No_Transaksi"))
						Lv.SubItems.Add(Format(Dr("Jumlah_RM_Tambah"), "N4"))
					End If

					Total_Formula += Dr("Nilai_Formula")
					Total_Produksi += Dr("Nilai_Produksi")
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

	'================================================================================================================================================================
	'=     HANDLE KEY
	'================================================================================================================================================================

	Private Sub Cmb_Filter_Batch_Pn1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter_Batch_Pn1.KeyPress
		If e.KeyChar = Chr(13) Then Cmb_KdBarang_Pn1.Focus()
	End Sub

	Private Sub Cmb_KdBarang_Pn1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KdBarang_Pn1.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari_Pn1.Focus()
	End Sub

	Private Sub EnableDoubleBuffer(lvw As ListView)
		Dim t As Type = lvw.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(lvw, True, Nothing)
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

	Private Sub Lv_DataGr_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_DataGr.MouseMove, Batch.MouseMove, LvDataRekap.MouseMove, Lv_DataDetail.MouseMove
		HandleListViewHover(sender, e)
	End Sub

End Class