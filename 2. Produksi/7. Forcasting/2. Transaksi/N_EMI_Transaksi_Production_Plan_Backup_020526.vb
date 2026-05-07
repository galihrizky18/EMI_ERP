Imports Microsoft.Office.Interop

Public Class N_EMI_Transaksi_Production_Plan_Backup_020526
	Public arrBulan, arrBulanMM As New ArrayList
	Public fRef, fValidasi As String
	Dim Jenis = "Transaksi_Sales_Forecasting"

	Private isLoading As Boolean = True

	Private isFromGrid As Boolean = False
	Private isFromHeader As Boolean = False

	Dim initBulanOtomatis As Integer = 0

	Public fTransSalesForcasting As String = "SF"

	Dim arrCellInputSales, arrCellInputPPIC As New ArrayList

	Dim tahun_awal As Integer
	Dim tahun_akhir As Integer

	Dim lvChkBox As String
	Dim lvKdBrg As String
	Dim lvNmBrg As String
	Dim lvAvg3Bulan As String
	Dim lvForecastCurrentMonth As String
	Dim lvActualCurrentMonth As String
	Dim lvPersenCurrentMonth As String
	Dim lvSalesForecastBln1 As String
	Dim lvPPICForecastBln1 As String
	Dim LvUrut_1 As String
	Dim LvRV_1 As String
	Dim lvSpace1 As String
	Dim lvSalesForecastBln2 As String
	Dim lvPPICForecastBln2 As String
	Dim LvUrut_2 As String
	Dim LvRV_2 As String
	Dim lvSpace2 As String
	Dim lvSalesForecastBln3 As String
	Dim lvPPICForecastBln3 As String
	Dim LvUrut_3 As String
	Dim LvRV_3 As String
	Dim lvSpace3 As String
	Dim lvSalesForecastBln4 As String
	Dim lvPPICForecastBln4 As String
	Dim LvUrut_4 As String
	Dim LvRV_4 As String
	Dim lvSpace4 As String
	Dim lvSalesForecastBln5 As String
	Dim lvPPICForecastBln5 As String
	Dim LvUrut_5 As String
	Dim LvRV_5 As String
	Dim lvSpace5 As String
	Dim lvSalesForecastBln6 As String
	Dim lvPPICForecastBln6 As String
	Dim LvUrut_6 As String
	Dim LvRV_6 As String
	Dim lvSpace6 As String
	Dim lvStatus As String
	Dim lvSatuan As String

	Dim CellChkBox As Integer = 0
	Dim CellKdBrg As Integer = 1
	Dim CellNmBrg As Integer = 2
	Dim CellAvg3Bulan As Integer = 3
	Dim CellForecastCurrentMonth As Integer = 4
	Dim CellActualCurrentMonth As Integer = 5
	Dim CellPersenCurrentMonth As Integer = 6

	Dim CellPersenStockGR1 As Integer = 7
	Dim CellPersenStockGR2 As Integer = 8
	Dim CellJumlahDO As Integer = 9

	Public CellSalesForecastBln1 As Integer = 10
	Public CellPPICForecastBln1 As Integer = 11
	Public CellUrut_1 As Integer = 12
	Public CellRV_1 As Integer = 13
	Public CellSpace1 As Integer = 14

	Public CellSalesForecastBln2 As Integer = 15
	Public CellPPICForecastBln2 As Integer = 16
	Public CellUrut_2 As Integer = 17
	Public CellRV_2 As Integer = 18
	Public CellSpace2 As Integer = 19

	Public CellSalesForecastBln3 As Integer = 20
	Public CellPPICForecastBln3 As Integer = 21
	Public CellUrut_3 As Integer = 22
	Public CellRV_3 As Integer = 23
	Public CellSpace3 As Integer = 24

	Public CellSalesForecastBln4 As Integer = 25
	Public CellPPICForecastBln4 As Integer = 26
	Public CellUrut_4 As Integer = 27
	Public CellRV_4 As Integer = 28
	Public CellSpace4 As Integer = 29

	Public CellSalesForecastBln5 As Integer = 30
	Public CellPPICForecastBln5 As Integer = 31
	Public CellUrut_5 As Integer = 32
	Public CellRV_5 As Integer = 33
	Public CellSpace5 As Integer = 34

	Public CellSalesForecastBln6 As Integer = 35
	Public CellPPICForecastBln6 As Integer = 36
	Public CellUrut_6 As Integer = 37
	Public CellRV_6 As Integer = 38
	Public CellSpace6 As Integer = 39

	Public CellStatus As Integer = 40
	Public CellSatuan As Integer = 41

	Public Property fStatus As String = ""

	Public Arrbarang As New ArrayList
	Public Arrlokasi As New ArrayList
	Public ArrNama As New ArrayList

	Private Class UpdateDetailItem
		Public Urut As String
		Public NoFaktur As String
		Public NilaiSalesBaru As String   ' sudah HilangkanTanda
		Public NilaiPPICBaru As String    ' sudah HilangkanTanda
		Public NilaiSalesLama As Double
		Public NilaiPPICLama As Double
	End Class

	Private Sub get_no_faktur()
		Txt_NoFaktur.Text = fTransSalesForcasting & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("EMI_Transaksi_Sales_Forecasting", "No_Faktur", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(no_Faktur, 1, " & Len(fTransSalesForcasting) + 4 & ")", fTransSalesForcasting & Format(tgl_skg, "MMyy"))
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

	Private Sub get_data()
		DataGridView1.Columns(CellChkBox).HeaderText = "#"
		DataGridView1.Columns(CellKdBrg).HeaderText = "Kode Barang"
		DataGridView1.Columns(CellNmBrg).HeaderText = "Nama Barang"
		DataGridView1.Columns(CellAvg3Bulan).HeaderText = "Avg 3 Bulan (Pcs)"
		DataGridView1.Columns(CellForecastCurrentMonth).HeaderText = "Forecast Current Month"
		DataGridView1.Columns(CellActualCurrentMonth).HeaderText = "Actual Current Month"
		DataGridView1.Columns(CellPersenCurrentMonth).HeaderText = "% Current Month"

		DataGridView1.Columns(CellPersenCurrentMonth).DisplayIndex = 11
		DataGridView1.Columns(CellActualCurrentMonth).DisplayIndex = 10
		DataGridView1.Columns(CellForecastCurrentMonth).DisplayIndex = 10

		Dim a As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
		Dim fthn As Integer = Val(Cmb_Tahun.Text)
		Dim panggil_databulan As String = ""
		Dim panggil_datatahun As String = ""
		If a = 12 Then
			a = 1
			fthn = fthn + 1
		Else
			a = a + 1
		End If

		Dim b As String = ""
		For index = 0 To arrBulan.Count - 1
			If arrBulan.Item(index) = a Then
				b = Cmb_Bulan.Items(index)
				panggil_databulan = arrBulanMM.Item(index)
			End If
		Next

		panggil_datatahun = fthn

		DataGridView1.Columns(CellSalesForecastBln1).HeaderText = "Sales - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellPPICForecastBln1).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellUrut_1).HeaderText = "1"
		DataGridView1.Columns(CellSpace1).HeaderText = ""

		If a = 12 Then
			a = 1
			fthn = fthn + 1
		Else
			a = a + 1
		End If

		For index = 0 To arrBulan.Count - 1
			If arrBulan.Item(index) = a Then
				b = Cmb_Bulan.Items(index)
			End If
		Next
		DataGridView1.Columns(CellSalesForecastBln2).HeaderText = "Sales - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellPPICForecastBln2).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellUrut_2).HeaderText = "2"
		DataGridView1.Columns(CellSpace2).HeaderText = ""

		If a = 12 Then
			a = 1
			fthn = fthn + 1
		Else
			a = a + 1
		End If

		For index = 0 To arrBulan.Count - 1
			If arrBulan.Item(index) = a Then
				b = Cmb_Bulan.Items(index)
			End If
		Next
		DataGridView1.Columns(CellSalesForecastBln3).HeaderText = "Sales - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellPPICForecastBln3).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellUrut_3).HeaderText = "3"
		DataGridView1.Columns(CellSpace3).HeaderText = ""

		If a = 12 Then
			a = 1
			fthn = fthn + 1
		Else
			a = a + 1
		End If

		For index = 0 To arrBulan.Count - 1
			If arrBulan.Item(index) = a Then
				b = Cmb_Bulan.Items(index)
			End If
		Next
		DataGridView1.Columns(CellSalesForecastBln4).HeaderText = "Sales - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellPPICForecastBln4).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellUrut_4).HeaderText = "4"
		DataGridView1.Columns(CellSpace4).HeaderText = ""

		If a = 12 Then
			a = 1
			fthn = fthn + 1
		Else
			a = a + 1
		End If

		For index = 0 To arrBulan.Count - 1
			If arrBulan.Item(index) = a Then
				b = Cmb_Bulan.Items(index)
			End If
		Next
		DataGridView1.Columns(CellSalesForecastBln5).HeaderText = "Sales - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellPPICForecastBln5).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellUrut_5).HeaderText = "5"
		DataGridView1.Columns(CellSpace5).HeaderText = ""

		If a = 12 Then
			a = 1
			fthn = fthn + 1
		Else
			a = a + 1
		End If

		For index = 0 To arrBulan.Count - 1
			If arrBulan.Item(index) = a Then
				b = Cmb_Bulan.Items(index)
			End If
		Next
		DataGridView1.Columns(CellSalesForecastBln6).HeaderText = "Sales - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellPPICForecastBln6).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellUrut_6).HeaderText = "6"
		DataGridView1.Columns(CellSpace6).HeaderText = ""

		DataGridView1.Columns(CellStatus).HeaderText = "Status"

		Dim fLoad As Boolean = False
		Dim aksesUbahSales As String = ""

		Try
			OpenConn()

			If CekButtonRole("EMI_Transaksi_ForecastOrder_Sales") = "Y" Then
				aksesUbahSales = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			SQL = "select No_Faktur from EMI_Transaksi_Sales_Forecasting where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "Lokasi = '" & Cmb_Lokasi.Text & "' and Bulan = '" & arrBulanMM.Item(Cmb_Bulan.SelectedIndex) & "' and Tahun = '" & Cmb_Tahun.Text & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Txt_NoFaktur.Text = Dr("No_Faktur")
					fLoad = True
				Else
					fLoad = False
				End If
			End Using

			'SQL = "select Flag_Validasi from EMI_Transaksi_Sales_Forecasting where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'SQL = SQL & "Lokasi = '" & Cmb_Lokasi.Text & "' and Bulan = '" & panggil_databulan & "' and Tahun = '" & panggil_datatahun & "'"
			'Using Dr = OpenTrans(SQL)
			'    If Dr.Read Then
			'        If fStatus = "Transaksi_ForecastOrder_Sales" Then
			'            If aksesUbahSales = "Y" Then
			'                If General_Class.CekNULL(Dr("Flag_Validasi")) = "Y" Then
			'                    btn_TambahBarang.Enabled = False
			'                Else
			'                    btn_TambahBarang.Enabled = True
			'                End If
			'            Else
			'                btn_TambahBarang.Enabled = False
			'            End If
			'        Else
			'            btn_TambahBarang.Enabled = False
			'        End If
			'    Else
			'        If fStatus = "Transaksi_ForecastOrder_Sales" Then
			'            If aksesUbahSales = "Y" Then
			'                btn_TambahBarang.Enabled = True
			'            Else
			'                btn_TambahBarang.Enabled = False
			'            End If
			'        Else
			'            btn_TambahBarang.Enabled = False
			'        End If
			'    End If
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If fLoad = True Then
			Txt_NoFaktur_Leave(Cmb_Tahun, Nothing)
		Else
			DataGridView1.Rows.Clear()
		End If
	End Sub

	Public Sub Get_Barang()
		Dim aksesUbahSales As String = ""
		Dim aksesUbahPPIC As String = ""

		Try
			OpenConn()

			If CekButtonRole("EMI_Transaksi_ForecastOrder_PPIC") = "Y" Then
				aksesUbahPPIC = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			If CekButtonRole("EMI_Transaksi_ForecastOrder_Sales") = "Y" Then
				aksesUbahSales = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			For i = 0 To Arrbarang.Count - 1
				DataGridView1.Rows.Add(1)
				Dim ind As Integer = DataGridView1.Rows.Count - 1
				DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellSpace1).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellSpace2).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellSalesForecastBln3).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellSpace3).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellSalesForecastBln4).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellSpace4).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellSalesForecastBln5).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellSpace5).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellSpace6).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellStatus).Style.BackColor = Color.Yellow

				DataGridView1.Rows(ind).Cells(CellKdBrg).Value = Arrbarang.Item(i)
				DataGridView1.Rows(ind).Cells(CellNmBrg).Value = ArrNama.Item(i)
				DataGridView1.Rows(ind).Cells(CellAvg3Bulan).Value = Format(0, "N2")

				SQL = "Select top(1) satuan from barang a where kode_barang='" & Arrbarang.Item(i) & "' "
				Using dr2 = OpenTrans(SQL)
					If dr2.Read Then
						DataGridView1.Rows(ind).Cells(CellSatuan).Value = dr2("satuan")
					Else
						dr2.Close()
						CloseConn()
						MessageBox.Show("barang tidak ditemukan")
						Exit Sub
					End If
				End Using

				Dim a As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
				Dim fthn As Integer = Val(Cmb_Tahun.Text)
				Dim b As String = ""
				Dim FValidasi = "Y"
				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						'ComboBox1.SelectedIndex = index
						b = arrBulanMM.Item(index)
					End If
				Next

				'Current Month
				SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Sales,Urut, cast(rv as bigint) as rvx from EMI_Transaksi_Sales_Forecasting_Detail where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(i) & "' and Kode_Barang = '" & Arrbarang.Item(i) & "'"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")
						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellForecastCurrentMonth).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
							DataGridView1.Rows(ind).Cells(CellActualCurrentMonth).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellPersenCurrentMonth).Value = Format(0, "N2")
						Else
							DataGridView1.Rows(ind).Cells(CellForecastCurrentMonth).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellActualCurrentMonth).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellPersenCurrentMonth).Value = Format(0, "N2")
						End If
					End With
				End Using

				SQL = $"
					select Kode_Perusahaan, Kode_Barang, Stock_GR1, Stock_GR2
					from N_EMI_View_Transaksi_Production_Plan_Stock_GR
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Kode_Barang = '{Arrbarang.Item(i)}'
				"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")
						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellPersenStockGR1).Value = Format(Val(HilangkanTanda(.Rows(0).Item("Stock_GR1"))), "N2")
							DataGridView1.Rows(ind).Cells(CellPersenStockGR2).Value = Format(Val(HilangkanTanda(.Rows(0).Item("Stock_GR2"))), "N2")
						Else
							DataGridView1.Rows(ind).Cells(CellPersenStockGR1).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellPersenStockGR2).Value = Format(0, "N2")
						End If
					End With
				End Using

				Dim BulanTahunDO As String = $"{arrBulanMM(Cmb_Bulan.SelectedIndex)}{Cmb_Tahun.Text.Trim}"
				SQL = $"
					select Kode_Perusahaan, Kode_Barang, BulanTahun, StockDO
					from N_EMI_View_Transaksi_Production_Plan_Stock_DO
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Kode_Barang = '{Arrbarang.Item(i)}'
					and BulanTahun = '{BulanTahunDO}'
				"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")
						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellJumlahDO).Value = Format(Val(HilangkanTanda(.Rows(0).Item("StockDO"))), "N2")
						Else
							DataGridView1.Rows(ind).Cells(CellJumlahDO).Value = Format(0, "N2")
						End If
					End With
				End Using

				'1
				If a = 12 Then
					a = 1
					fthn = fthn + 1
				Else
					a = a + 1
				End If

				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						'ComboBox1.SelectedIndex = index
						b = arrBulanMM.Item(index)
					End If
				Next

				SQL = "Select Status_Data from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
				SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						DataGridView1.Rows(ind).Cells(CellStatus).Value = dr("Status_Data")
					Else
						DataGridView1.Rows(ind).Cells(CellStatus).Value = "NEW"
					End If
				End Using

				If fStatus = "Transaksi_ForecastOrder_PPIC" Then

					If aksesUbahPPIC = "Y" Then
						SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and Flag_validasi_PPIC='Y' "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								FValidasi = "Y"
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).ReadOnly = True
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).Style.BackColor = Color.LightCyan
							Else

								dr.Close()
								SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
								SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and Flag_validasi='Y' "
								Using dr2 = OpenTrans(SQL)
									If dr2.Read Then
										FValidasi = ""
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).ReadOnly = False
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).Style.BackColor = Color.LightCyan
									Else
										FValidasi = "Y"
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).ReadOnly = True
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).Style.BackColor = Color.LightCyan
									End If
								End Using
							End If
						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).Style.BackColor = Color.LightCyan
					End If

				ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
					If aksesUbahSales = "Y" Then
						'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and Flag_validasi='Y' "
						'Using dr = OpenTrans(SQL)
						'    If dr.Read Then
						'        FValidasi = "Y"
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).ReadOnly = True
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).Style.BackColor = Color.FromArgb(245, 245, 220)
						'    Else
						'        FValidasi = ""
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).ReadOnly = False
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).Style.BackColor = Color.LightYellow
						'    End If
						'End Using

						SQL = "Select a.no_faktur , b.flag_validasi from EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_detail b where a.bulan='" & b & "' and a.tahun ='" & fthn & "' "
						SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' "
						SQL = SQL & "and a.no_faktur = b.no_faktur and a.kode_perusahaan = b.kode_perusahaan "
						Using dr = OpenTrans(SQL)
							Do While dr.Read
								If General_Class.CekNULL(dr("flag_validasi")) = "" Then
									FValidasi = ""
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = False
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.LightYellow
								Else
									FValidasi = "Y"
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = True
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.FromArgb(245, 245, 220)

								End If
							Loop
						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).Style.BackColor = Color.FromArgb(245, 245, 220)
					End If
				End If

				SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Sales,Urut, cast(rv as bigint) as rvx from EMI_Transaksi_Sales_Forecasting_Detail where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(i) & "' and Kode_Barang = '" & Arrbarang.Item(i) & "'"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")
						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).Value = Format(.Rows(0).Item("Nilai_Sales"), "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_1).Value = .Rows(0).Item("Urut")
							DataGridView1.Rows(ind).Cells(CellRV_1).Value = .Rows(0).Item("rvx")
							DataGridView1.Rows(ind).Cells(CellSpace1).Value = ""
						Else
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_1).Value = ""
							DataGridView1.Rows(ind).Cells(CellRV_1).Value = ""
							DataGridView1.Rows(ind).Cells(CellSpace1).Value = ""
						End If
					End With
				End Using

				'2
				If a = 12 Then
					a = 1
					fthn = fthn + 1
				Else
					a = a + 1
				End If

				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						'ComboBox1.SelectedIndex = index
						b = arrBulanMM.Item(index)
					End If
				Next

				If fStatus = "Transaksi_ForecastOrder_PPIC" Then

					If aksesUbahPPIC = "Y" Then
						SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								FValidasi = "Y"
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).ReadOnly = True
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).Style.BackColor = Color.LightCyan
							Else

								dr.Close()
								SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
								SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
								Using dr2 = OpenTrans(SQL)
									If dr2.Read Then
										FValidasi = ""
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).ReadOnly = False
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).Style.BackColor = Color.LightCyan
									Else
										FValidasi = "Y"
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).ReadOnly = True
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).Style.BackColor = Color.LightCyan
									End If
								End Using
							End If
						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).Style.BackColor = Color.LightCyan
					End If

				ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
					If aksesUbahSales = "Y" Then
						'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
						'Using dr = OpenTrans(SQL)
						'    If dr.Read Then
						'        FValidasi = "Y"
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).ReadOnly = True
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).Style.BackColor = Color.FromArgb(245, 245, 220)
						'    Else
						'        FValidasi = ""
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).ReadOnly = False
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).Style.BackColor = Color.LightYellow
						'    End If
						'End Using
						SQL = "Select a.no_faktur , b.flag_validasi from EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_detail b where a.bulan='" & b & "' and a.tahun ='" & fthn & "' "
						SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' "
						SQL = SQL & "and a.no_faktur = b.no_faktur and a.kode_perusahaan = b.kode_perusahaan "
						Using dr = OpenTrans(SQL)
							Do While dr.Read
								If General_Class.CekNULL(dr("flag_validasi")) = "" Then
									FValidasi = ""
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = False
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.LightYellow
								Else
									FValidasi = "Y"
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = True
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.FromArgb(245, 245, 220)

								End If
							Loop

						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).Style.BackColor = Color.FromArgb(245, 245, 220)
					End If
				End If

				SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Sales,Urut, cast(rv as bigint) as rvx from EMI_Transaksi_Sales_Forecasting_Detail where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(i) & "' and Kode_Barang = '" & Arrbarang.Item(i) & "'"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")

						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).Value = Format(.Rows(0).Item("Nilai_Sales"), "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_2).Value = .Rows(0).Item("Urut")
							DataGridView1.Rows(ind).Cells(CellRV_2).Value = .Rows(0).Item("rvx")
							DataGridView1.Rows(ind).Cells(CellSpace2).Value = ""
						Else
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_2).Value = ""
							DataGridView1.Rows(ind).Cells(CellRV_2).Value = ""
							DataGridView1.Rows(ind).Cells(CellSpace2).Value = ""
						End If
					End With
				End Using
				'3
				If a = 12 Then
					a = 1
					fthn = fthn + 1
				Else
					a = a + 1
				End If

				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						'ComboBox1.SelectedIndex = index
						b = arrBulanMM.Item(index)
					End If
				Next

				If fStatus = "Transaksi_ForecastOrder_PPIC" Then

					If aksesUbahPPIC = "Y" Then
						SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								FValidasi = "Y"
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).ReadOnly = True
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).Style.BackColor = Color.LightCyan
							Else

								dr.Close()
								SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
								SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
								Using dr2 = OpenTrans(SQL)
									If dr2.Read Then
										FValidasi = ""
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).ReadOnly = False
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).Style.BackColor = Color.LightCyan
									Else
										FValidasi = "Y"
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).ReadOnly = True
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).Style.BackColor = Color.LightCyan
									End If
								End Using
							End If
						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).Style.BackColor = Color.LightCyan
					End If

				ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
					If aksesUbahSales = "Y" Then
						'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
						'Using dr = OpenTrans(SQL)
						'    If dr.Read Then
						'        FValidasi = "Y"
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln3).ReadOnly = True
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln3).Style.BackColor = Color.FromArgb(245, 245, 220)
						'    Else
						'        FValidasi = ""
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln3).ReadOnly = False
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln3).Style.BackColor = Color.LightYellow
						'    End If
						'End Using

						SQL = "Select a.no_faktur , b.flag_validasi from EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_detail b where a.bulan='" & b & "' and a.tahun ='" & fthn & "' "
						SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' "
						SQL = SQL & "and a.no_faktur = b.no_faktur and a.kode_perusahaan = b.kode_perusahaan "
						Using dr = OpenTrans(SQL)
							Do While dr.Read
								If General_Class.CekNULL(dr("flag_validasi")) = "" Then
									FValidasi = ""
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = False
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.LightYellow
								Else
									FValidasi = "Y"
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = True
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.FromArgb(245, 245, 220)

								End If
							Loop

						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln3).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln3).Style.BackColor = Color.FromArgb(245, 245, 220)
					End If
				End If

				SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Sales,Urut, cast(rv as bigint) as rvx from EMI_Transaksi_Sales_Forecasting_Detail where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(i) & "' and Kode_Barang = '" & Arrbarang.Item(i) & "'"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")

						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln3).Value = Format(.Rows(0).Item("Nilai_Sales"), "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_3).Value = .Rows(0).Item("Urut")
							DataGridView1.Rows(ind).Cells(CellRV_3).Value = .Rows(0).Item("rvx")
							DataGridView1.Rows(ind).Cells(CellSpace3).Value = ""
						Else
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln3).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_3).Value = ""
							DataGridView1.Rows(ind).Cells(CellRV_3).Value = ""
							DataGridView1.Rows(ind).Cells(CellSpace3).Value = ""
						End If
					End With
				End Using
				'4
				If a = 12 Then
					a = 1
					fthn = fthn + 1
				Else
					a = a + 1
				End If

				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						'ComboBox1.SelectedIndex = index
						b = arrBulanMM.Item(index)
					End If
				Next

				If fStatus = "Transaksi_ForecastOrder_PPIC" Then

					If aksesUbahPPIC = "Y" Then
						SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								FValidasi = "Y"
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).ReadOnly = True
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).Style.BackColor = Color.LightCyan
							Else

								dr.Close()
								SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
								SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
								Using dr2 = OpenTrans(SQL)
									If dr2.Read Then
										FValidasi = ""
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).ReadOnly = False
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).Style.BackColor = Color.LightCyan
									Else
										FValidasi = "Y"
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).ReadOnly = True
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).Style.BackColor = Color.LightCyan
									End If
								End Using
							End If
						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).Style.BackColor = Color.LightCyan
					End If

				ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
					If aksesUbahSales = "Y" Then
						'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
						'Using dr = OpenTrans(SQL)
						'    If dr.Read Then
						'        FValidasi = "Y"
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln4).ReadOnly = True
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln4).Style.BackColor = Color.FromArgb(245, 245, 220)
						'    Else
						'        FValidasi = ""
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln4).ReadOnly = False
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln4).Style.BackColor = Color.LightYellow
						'    End If
						'End Using
						SQL = "Select a.no_faktur , b.flag_validasi from EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_detail b where a.bulan='" & b & "' and a.tahun ='" & fthn & "' "
						SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' "
						SQL = SQL & "and a.no_faktur = b.no_faktur and a.kode_perusahaan = b.kode_perusahaan "
						Using dr = OpenTrans(SQL)
							Do While dr.Read
								If General_Class.CekNULL(dr("flag_validasi")) = "" Then
									FValidasi = ""
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = False
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.LightYellow
								Else
									FValidasi = "Y"
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = True
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.FromArgb(245, 245, 220)

								End If
							Loop

						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln4).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln4).Style.BackColor = Color.FromArgb(245, 245, 220)
					End If
				End If

				SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Sales,Urut, cast(rv as bigint) as rvx from EMI_Transaksi_Sales_Forecasting_Detail where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(i) & "' and Kode_Barang = '" & Arrbarang.Item(i) & "'"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")

						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln4).Value = Format(.Rows(0).Item("Nilai_Sales"), "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_4).Value = .Rows(0).Item("Urut")
							DataGridView1.Rows(ind).Cells(CellRV_4).Value = .Rows(0).Item("rvx")
							DataGridView1.Rows(ind).Cells(CellSpace4).Value = ""
						Else
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln4).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_4).Value = ""
							DataGridView1.Rows(ind).Cells(CellRV_4).Value = ""
							DataGridView1.Rows(ind).Cells(CellSpace4).Value = ""
						End If
					End With
				End Using
				'5
				If a = 12 Then
					a = 1
					fthn = fthn + 1
				Else
					a = a + 1
				End If

				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						'ComboBox1.SelectedIndex = index
						b = arrBulanMM.Item(index)
					End If
				Next

				If fStatus = "Transaksi_ForecastOrder_PPIC" Then

					If aksesUbahPPIC = "Y" Then
						SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								FValidasi = "Y"
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).ReadOnly = True
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).Style.BackColor = Color.LightCyan
							Else

								dr.Close()
								SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
								SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
								Using dr2 = OpenTrans(SQL)
									If dr2.Read Then
										FValidasi = ""
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).ReadOnly = False
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).Style.BackColor = Color.LightCyan
									Else
										FValidasi = "Y"
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).ReadOnly = True
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).Style.BackColor = Color.LightCyan
									End If
								End Using
							End If
						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).Style.BackColor = Color.LightCyan
					End If

				ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
					If aksesUbahSales = "Y" Then
						'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
						'Using dr = OpenTrans(SQL)
						'    If dr.Read Then
						'        FValidasi = "Y"
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln5).ReadOnly = True
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln5).Style.BackColor = Color.FromArgb(245, 245, 220)
						'    Else
						'        FValidasi = ""
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln5).ReadOnly = False
						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln5).Style.BackColor = Color.LightYellow
						'    End If
						'End Using

						SQL = "Select a.no_faktur , b.flag_validasi from EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_detail b where a.bulan='" & b & "' and a.tahun ='" & fthn & "' "
						SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' "
						SQL = SQL & "and a.no_faktur = b.no_faktur and a.kode_perusahaan = b.kode_perusahaan "
						Using dr = OpenTrans(SQL)
							Do While dr.Read
								If General_Class.CekNULL(dr("flag_validasi")) = "" Then
									FValidasi = ""
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = False
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.LightYellow
								Else
									FValidasi = "Y"
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = True
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.FromArgb(245, 245, 220)

								End If
							Loop

						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln5).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln5).Style.BackColor = Color.FromArgb(245, 245, 220)
					End If
				End If

				SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Sales,Urut, cast(rv as bigint) as rvx from EMI_Transaksi_Sales_Forecasting_Detail where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(i) & "' and Kode_Barang = '" & Arrbarang.Item(i) & "'"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")

						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln5).Value = Format(.Rows(0).Item("Nilai_Sales"), "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_5).Value = .Rows(0).Item("Urut")
							DataGridView1.Rows(ind).Cells(CellRV_5).Value = .Rows(0).Item("rvx")
							DataGridView1.Rows(ind).Cells(CellSpace5).Value = ""
						Else
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln5).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_5).Value = ""
							DataGridView1.Rows(ind).Cells(CellRV_5).Value = ""
							DataGridView1.Rows(ind).Cells(CellSpace5).Value = ""
						End If
					End With
				End Using
				'6
				If a = 12 Then
					a = 1
					fthn = fthn + 1
				Else
					a = a + 1
				End If

				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						'ComboBox1.SelectedIndex = index
						b = arrBulanMM.Item(index)
					End If
				Next

				If fStatus = "Transaksi_ForecastOrder_PPIC" Then

					If aksesUbahPPIC = "Y" Then
						SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
						SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								FValidasi = "Y"
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).ReadOnly = True
								DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).Style.BackColor = Color.LightCyan
							Else

								dr.Close()
								SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
								SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
								Using dr2 = OpenTrans(SQL)
									If dr2.Read Then
										FValidasi = ""
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).ReadOnly = False
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).Style.BackColor = Color.LightCyan
									Else
										FValidasi = "Y"
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).ReadOnly = True
										DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).Style.BackColor = Color.LightCyan
									End If
								End Using
							End If
						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).Style.BackColor = Color.LightCyan
					End If

				ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
					If aksesUbahSales = "Y" Then
						SQL = "Select a.no_faktur , b.flag_validasi from EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_detail b where a.bulan='" & b & "' and a.tahun ='" & fthn & "' "
						SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' "
						SQL = SQL & "and a.no_faktur = b.no_faktur and a.kode_perusahaan = b.kode_perusahaan "
						Using dr = OpenTrans(SQL)
							Do While dr.Read
								If General_Class.CekNULL(dr("flag_validasi")) = "" Then
									FValidasi = ""
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = False
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.LightYellow
								Else
									FValidasi = "Y"
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = True
									DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.FromArgb(245, 245, 220)

								End If
							Loop
							'If dr.Read Then
							'    FValidasi = "Y"
							'    DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = True
							'    DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.FromArgb(245, 245, 220)
							'Else
							'    FValidasi = ""
							'    DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = False
							'    DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.LightYellow
							'End If
						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.FromArgb(245, 245, 220)
					End If
				End If

				SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Sales,Urut, cast(rv as bigint) as rvx from EMI_Transaksi_Sales_Forecasting_Detail where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(i) & "' and Kode_Barang = '" & Arrbarang.Item(i) & "'"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")

						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Value = Format(.Rows(0).Item("Nilai_Sales"), "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_6).Value = .Rows(0).Item("Urut")
							DataGridView1.Rows(ind).Cells(CellRV_6).Value = .Rows(0).Item("rvx")
							DataGridView1.Rows(ind).Cells(CellSpace6).Value = ""
						Else
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).Value = Format(0, "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_6).Value = ""
							DataGridView1.Rows(ind).Cells(CellRV_6).Value = ""
							DataGridView1.Rows(ind).Cells(CellSpace6).Value = ""
						End If
					End With
				End Using

			Next

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
		'lvChkBox = DataGridView1.Rows(No_Index).Cells(CellChkBox).Value
		lvKdBrg = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKdBrg).Value)
		lvNmBrg = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNmBrg).Value)
		lvAvg3Bulan = CekNothing(DataGridView1.Rows(No_Index).Cells(CellAvg3Bulan).Value)
		lvForecastCurrentMonth = CekNothing(DataGridView1.Rows(No_Index).Cells(CellForecastCurrentMonth).Value)
		lvActualCurrentMonth = CekNothing(DataGridView1.Rows(No_Index).Cells(CellActualCurrentMonth).Value)
		lvPersenCurrentMonth = CekNothing(DataGridView1.Rows(No_Index).Cells(CellPersenCurrentMonth).Value)

		lvSalesForecastBln1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSalesForecastBln1).Value)
		lvPPICForecastBln1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellPPICForecastBln1).Value)
		LvUrut_1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_1).Value)
		LvRV_1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellRV_1).Value)
		lvSpace1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSpace1).Value)

		lvSalesForecastBln2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSalesForecastBln2).Value)
		lvPPICForecastBln2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellPPICForecastBln2).Value)
		LvUrut_2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_2).Value)
		LvRV_2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellRV_2).Value)
		lvSpace2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSpace2).Value)

		lvSalesForecastBln3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSalesForecastBln3).Value)
		lvPPICForecastBln3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellPPICForecastBln3).Value)
		LvUrut_3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_3).Value)
		LvRV_3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellRV_3).Value)
		lvSpace3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSpace3).Value)

		lvSalesForecastBln4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSalesForecastBln4).Value)
		lvPPICForecastBln4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellPPICForecastBln4).Value)
		LvUrut_4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_4).Value)
		LvRV_4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellRV_4).Value)
		lvSpace4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSpace4).Value)

		lvSalesForecastBln5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSalesForecastBln5).Value)
		lvPPICForecastBln5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellPPICForecastBln5).Value)
		LvUrut_5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_5).Value)
		LvRV_5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellRV_5).Value)
		lvSpace5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSpace5).Value)

		lvSalesForecastBln6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSalesForecastBln6).Value)
		lvPPICForecastBln6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellPPICForecastBln6).Value)
		LvUrut_6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_6).Value)
		LvRV_6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellRV_6).Value)
		lvSpace6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSpace6).Value)

		lvStatus = CekNothing(DataGridView1.Rows(No_Index).Cells(CellStatus).Value)
		lvSatuan = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSatuan).Value)
	End Sub

	Private Sub kosong(Optional ByVal isRefresh As String = "")
		'Btn_Realese.Visible = False
		Btn_Realese.Enabled = False

		Dim aksesUbahSales As String = ""
		Dim aksesUbahPPIC As String = ""
		Dim AksesRealeaseSales As String = ""
		Dim AksesRealeasePPIC As String = ""
		Dim AksesUNRealeaseSales As String = ""
		Dim AksesUNRealeasePPIC As String = ""

		isLoading = True

		Try
			OpenConn()

			If CekButtonRole("EMI_Transaksi_ForecastOrder_PPIC") = "Y" Then
				aksesUbahPPIC = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			If CekButtonRole("EMI_Transaksi_ForecastOrder_Sales") = "Y" Then
				aksesUbahSales = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			If CekButtonRole("Realease_Forecast_Sales") = "Y" Then
				AksesRealeaseSales = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'
		Try
			OpenConn()

			If CekButtonRole("Realease_Forecast_PPIC") = "Y" Then
				AksesRealeasePPIC = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			If CekButtonRole("Unrealease_Forecast_Sales") = "Y" Then
				AksesUNRealeaseSales = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'
		Try
			OpenConn()

			If CekButtonRole("Unrealease_Forecast_PPIC") = "Y" Then
				AksesUNRealeasePPIC = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()
			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			'Label1.Text = Base_Language.Lang_Transaksi_Sales_Forecasting
			Label1.Text = "Production Plan"
			'  Label2.Text = Base_Language.Lang_Global_NoFaktur
			Label5.Text = Base_Language.lang_global_keterangan
			'  Label3.Text = Base_Language.Lang_Global_Tanggal
			Label7.Text = Base_Language.Lang_Global_Lokasi
			btn_TambahBarang.Text = Base_Language.Lang_Global_TambahBarang
			CB_PilihSeluruh.Text = Base_Language.Lang_Global_PilihSeluruh
			Label4.Text = Base_Language.Lang_Global_Bulan
			Label6.Text = Base_Language.Lang_Global_Tahun

			Cmb_Bulan.Enabled = False
			Cmb_Tahun.Enabled = False

			get_jam()

			If Not isRefresh = "REFRESH" Then

				'   DateTimePicker1.Value = tgl_skg

				Dim selectedDate As Date = tgl_skg
				Dim selectedMonthName As String = selectedDate.ToString("MMMM", New Globalization.CultureInfo("id-ID"))
				Dim selectedYear As Integer = selectedDate.Year

				Cmb_Bulan.Items.Clear() : arrBulan.Clear() : arrBulanMM.Clear()
				Cmb_Bulan.Items.Add("Januari") : arrBulan.Add("1") : arrBulanMM.Add("01")
				Cmb_Bulan.Items.Add("Februari") : arrBulan.Add("2") : arrBulanMM.Add("02")
				Cmb_Bulan.Items.Add("Maret") : arrBulan.Add("3") : arrBulanMM.Add("03")
				Cmb_Bulan.Items.Add("April") : arrBulan.Add("4") : arrBulanMM.Add("04")
				Cmb_Bulan.Items.Add("Mei") : arrBulan.Add("5") : arrBulanMM.Add("05")
				Cmb_Bulan.Items.Add("Juni") : arrBulan.Add("6") : arrBulanMM.Add("06")
				Cmb_Bulan.Items.Add("Juli") : arrBulan.Add("7") : arrBulanMM.Add("07")
				Cmb_Bulan.Items.Add("Agustus") : arrBulan.Add("8") : arrBulanMM.Add("08")
				Cmb_Bulan.Items.Add("September") : arrBulan.Add("9") : arrBulanMM.Add("09")
				Cmb_Bulan.Items.Add("Oktober") : arrBulan.Add("10") : arrBulanMM.Add("10")
				Cmb_Bulan.Items.Add("November") : arrBulan.Add("11") : arrBulanMM.Add("11")
				Cmb_Bulan.Items.Add("Desember") : arrBulan.Add("12") : arrBulanMM.Add("12")

				'    Cmb_Bulan.Enabled = True

				Cmb_Tahun.Items.Clear()
				tahun_awal = Date.Now.Year - 2
				tahun_akhir = Date.Now.Year + 2
				For a As Integer = tahun_awal To tahun_akhir
					Cmb_Tahun.Items.Add(a)
				Next

				'  Cmb_Tahun.Enabled = True
				CB_PilihSeluruh.Checked = False

				Cmb_Lokasi.Items.Clear()
				SQL = "select Kode_Stock_Owner from Stock_Owner where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Stock_Owner"
				Using dr = OpenTrans(SQL)
					Do While dr.Read
						Cmb_Lokasi.Items.Add(dr("Kode_Stock_Owner"))
					Loop
				End Using
				Cmb_Lokasi.SelectedIndex = -1
				Cmb_Lokasi.Enabled = True

				Cmb_Bulan.SelectedItem = selectedMonthName
				Cmb_Tahun.SelectedItem = selectedYear

			End If

			Btn_Simpan.Tag = "&Simpan"
			OpenConn()
			'get_no_faktur()
			Txt_NoFaktur.Enabled = True

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Cek Validasi 'Y'
		''If fValidasi = "Y" Then
		''    MessageBox.Show("Data ini sudah divalidasi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		''    Display_Transaksi_ForecastOrder.fStatus = ""
		''End If

		'PPIC
		Try
			OpenConn()

			If fStatus = "Transaksi_ForecastOrder_PPIC" Then
				If aksesUbahPPIC = "Y" Then
					DataGridView1.Columns(CellChkBox).ReadOnly = False
					DataGridView1.Columns(CellKdBrg).ReadOnly = True
					DataGridView1.Columns(CellNmBrg).ReadOnly = True
					DataGridView1.Columns(CellAvg3Bulan).ReadOnly = True
					DataGridView1.Columns(CellForecastCurrentMonth).ReadOnly = True
					DataGridView1.Columns(CellActualCurrentMonth).ReadOnly = True
					DataGridView1.Columns(CellPersenCurrentMonth).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln1).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln1).ReadOnly = False
					DataGridView1.Columns(CellUrut_1).ReadOnly = True
					DataGridView1.Columns(CellSpace1).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln2).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln2).ReadOnly = False
					DataGridView1.Columns(CellUrut_2).ReadOnly = True
					DataGridView1.Columns(CellSpace2).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln3).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln3).ReadOnly = False
					DataGridView1.Columns(CellUrut_3).ReadOnly = True
					DataGridView1.Columns(CellSpace3).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln4).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln4).ReadOnly = False
					DataGridView1.Columns(CellUrut_4).ReadOnly = True
					DataGridView1.Columns(CellSpace4).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln5).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln5).ReadOnly = False
					DataGridView1.Columns(CellUrut_5).ReadOnly = True
					DataGridView1.Columns(CellSpace5).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln6).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln6).ReadOnly = False
					DataGridView1.Columns(CellUrut_6).ReadOnly = True
					DataGridView1.Columns(CellSpace6).ReadOnly = True
					DataGridView1.Columns(CellStatus).ReadOnly = True
					Cb_Referensi.Enabled = False
					btn_TambahBarang.Enabled = False
					Btn_Simpan.Enabled = True
					Btn_Simpan.Tag = "&Simpan"
				Else
					DataGridView1.Columns(CellChkBox).ReadOnly = True
					DataGridView1.Columns(CellKdBrg).ReadOnly = True
					DataGridView1.Columns(CellNmBrg).ReadOnly = True
					DataGridView1.Columns(CellAvg3Bulan).ReadOnly = True
					DataGridView1.Columns(CellForecastCurrentMonth).ReadOnly = True
					DataGridView1.Columns(CellActualCurrentMonth).ReadOnly = True
					DataGridView1.Columns(CellPersenCurrentMonth).ReadOnly = True

					DataGridView1.Columns(CellSalesForecastBln1).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln1).ReadOnly = True
					DataGridView1.Columns(CellUrut_1).ReadOnly = True
					DataGridView1.Columns(CellSpace1).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln2).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln2).ReadOnly = True
					DataGridView1.Columns(CellUrut_2).ReadOnly = True
					DataGridView1.Columns(CellSpace2).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln3).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln3).ReadOnly = True
					DataGridView1.Columns(CellUrut_3).ReadOnly = True
					DataGridView1.Columns(CellSpace3).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln4).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln4).ReadOnly = True
					DataGridView1.Columns(CellUrut_4).ReadOnly = True
					DataGridView1.Columns(CellSpace4).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln5).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln5).ReadOnly = True
					DataGridView1.Columns(CellUrut_5).ReadOnly = True
					DataGridView1.Columns(CellSpace5).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln6).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln6).ReadOnly = True
					DataGridView1.Columns(CellUrut_6).ReadOnly = True
					DataGridView1.Columns(CellSpace6).ReadOnly = True
					DataGridView1.Columns(CellStatus).ReadOnly = True

					btn_TambahBarang.Enabled = False
					Btn_Simpan.Enabled = False

				End If

				If AksesRealeasePPIC = "Y" Then
					Btn_Realese.Enabled = True
				Else
					Btn_Realese.Enabled = False
				End If

				If AksesUNRealeasePPIC = "Y" Then
					Btn_Unrealese.Enabled = True
				Else
					Btn_Unrealese.Enabled = False
				End If

			ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then

				If aksesUbahSales = "Y" Then
					DataGridView1.Columns(CellChkBox).ReadOnly = False
					DataGridView1.Columns(CellKdBrg).ReadOnly = True
					DataGridView1.Columns(CellNmBrg).ReadOnly = True
					DataGridView1.Columns(CellAvg3Bulan).ReadOnly = True
					DataGridView1.Columns(CellForecastCurrentMonth).ReadOnly = True
					DataGridView1.Columns(CellActualCurrentMonth).ReadOnly = True
					DataGridView1.Columns(CellPersenCurrentMonth).ReadOnly = True

					DataGridView1.Columns(CellSalesForecastBln1).ReadOnly = False
					DataGridView1.Columns(CellPPICForecastBln1).ReadOnly = True
					DataGridView1.Columns(CellUrut_1).ReadOnly = True
					DataGridView1.Columns(CellSpace1).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln2).ReadOnly = False
					DataGridView1.Columns(CellPPICForecastBln2).ReadOnly = True
					DataGridView1.Columns(CellUrut_2).ReadOnly = True
					DataGridView1.Columns(CellSpace2).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln3).ReadOnly = False
					DataGridView1.Columns(CellPPICForecastBln3).ReadOnly = True
					DataGridView1.Columns(CellUrut_3).ReadOnly = True
					DataGridView1.Columns(CellSpace3).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln4).ReadOnly = False
					DataGridView1.Columns(CellPPICForecastBln4).ReadOnly = True
					DataGridView1.Columns(CellUrut_4).ReadOnly = True
					DataGridView1.Columns(CellSpace4).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln5).ReadOnly = False
					DataGridView1.Columns(CellPPICForecastBln5).ReadOnly = True
					DataGridView1.Columns(CellUrut_5).ReadOnly = True
					DataGridView1.Columns(CellSpace5).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln6).ReadOnly = False
					DataGridView1.Columns(CellPPICForecastBln6).ReadOnly = True
					DataGridView1.Columns(CellUrut_6).ReadOnly = True
					DataGridView1.Columns(CellSpace6).ReadOnly = True
					Cb_Referensi.Enabled = True
					btn_TambahBarang.Enabled = True
					Btn_Simpan.Enabled = True
				Else
					DataGridView1.Columns(CellChkBox).ReadOnly = True
					DataGridView1.Columns(CellKdBrg).ReadOnly = True
					DataGridView1.Columns(CellNmBrg).ReadOnly = True
					DataGridView1.Columns(CellAvg3Bulan).ReadOnly = True
					DataGridView1.Columns(CellForecastCurrentMonth).ReadOnly = True
					DataGridView1.Columns(CellActualCurrentMonth).ReadOnly = True
					DataGridView1.Columns(CellPersenCurrentMonth).ReadOnly = True

					DataGridView1.Columns(CellSalesForecastBln1).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln1).ReadOnly = True
					DataGridView1.Columns(CellUrut_1).ReadOnly = True
					DataGridView1.Columns(CellSpace1).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln2).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln2).ReadOnly = True
					DataGridView1.Columns(CellUrut_2).ReadOnly = True
					DataGridView1.Columns(CellSpace2).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln3).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln3).ReadOnly = True
					DataGridView1.Columns(CellUrut_3).ReadOnly = True
					DataGridView1.Columns(CellSpace3).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln4).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln4).ReadOnly = True
					DataGridView1.Columns(CellUrut_4).ReadOnly = True
					DataGridView1.Columns(CellSpace4).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln5).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln5).ReadOnly = True
					DataGridView1.Columns(CellUrut_5).ReadOnly = True
					DataGridView1.Columns(CellSpace5).ReadOnly = True
					DataGridView1.Columns(CellSalesForecastBln6).ReadOnly = True
					DataGridView1.Columns(CellPPICForecastBln6).ReadOnly = True
					DataGridView1.Columns(CellUrut_6).ReadOnly = True
					DataGridView1.Columns(CellSpace6).ReadOnly = True
					DataGridView1.Columns(CellStatus).ReadOnly = True

					btn_TambahBarang.Enabled = False
					Btn_Simpan.Enabled = False
				End If

				If AksesRealeaseSales = "Y" Then
					Btn_Realese.Enabled = True
				Else
					Btn_Realese.Enabled = False
				End If

				If AksesUNRealeaseSales = "Y" Then
					Btn_Unrealese.Enabled = True
				Else
					Btn_Unrealese.Enabled = False
				End If

			ElseIf fStatus = "" Then

			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Cmb_Lokasi.SelectedIndex = -1
		'Cmb_Bulan.SelectedIndex = -1
		'Cmb_Tahun.SelectedIndex = -1
		'Cmb_Lokasi.SelectedIndex = -1

		DataGridView1.Rows.Clear()

		CheckBox1.Checked = False
		If isRefresh = "REFRESH" Then

			Cmb_Tahun_SelectedIndexChanged(Cmb_Tahun, EventArgs.Empty)
		Else
			Cmb_Lokasi.SelectedIndex = 0

		End If

	End Sub

	Private Sub EMI_Transaksi_ForecastOrder_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
		kosong()
	End Sub

	Private Sub EMI_Transaksi_ForecastOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Txt_NoFaktur.Focus()

		Me.Dock = DockStyle.Fill

		For Each col As DataGridViewColumn In DataGridView1.Columns
			col.SortMode = DataGridViewColumnSortMode.NotSortable
		Next

		kosong()
		DataGridView1.Columns(CellSatuan).DisplayIndex = 3

		DataGridView1.Columns(CellChkBox).HeaderText = "#"
		DataGridView1.Columns(CellKdBrg).HeaderText = "Kode Barang"
		DataGridView1.Columns(CellNmBrg).HeaderText = "Nama Barang"
		DataGridView1.Columns(CellAvg3Bulan).HeaderText = "Avg 3 Bulan (Pcs)"
		DataGridView1.Columns(CellForecastCurrentMonth).HeaderText = "Forecast Current Month"
		DataGridView1.Columns(CellActualCurrentMonth).HeaderText = "Actual Current Month"
		DataGridView1.Columns(CellPersenCurrentMonth).HeaderText = "% Current Month"
		DataGridView1.Columns(CellSalesForecastBln1).HeaderText = "Sales - Production Plan "
		DataGridView1.Columns(CellPPICForecastBln1).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_1).HeaderText = "1"
		DataGridView1.Columns(CellSpace1).HeaderText = ""
		DataGridView1.Columns(CellSpace1).HeaderCell.Style.BackColor = Color.LightGray
		DataGridView1.Columns(CellSalesForecastBln2).HeaderText = "Sales - Production Plan "
		DataGridView1.Columns(CellPPICForecastBln2).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_2).HeaderText = "2"
		DataGridView1.Columns(CellSpace2).HeaderText = ""
		DataGridView1.Columns(CellSpace2).HeaderCell.Style.BackColor = Color.LightGray
		DataGridView1.Columns(CellSalesForecastBln3).HeaderText = "Sales - Production Plan "
		DataGridView1.Columns(CellPPICForecastBln3).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_3).HeaderText = "3"
		DataGridView1.Columns(CellSpace3).HeaderText = ""
		DataGridView1.Columns(CellSpace3).HeaderCell.Style.BackColor = Color.LightGray
		DataGridView1.Columns(CellSalesForecastBln4).HeaderText = "Sales - Production Plan "
		DataGridView1.Columns(CellPPICForecastBln4).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_4).HeaderText = "4"
		DataGridView1.Columns(CellSpace4).HeaderText = ""
		DataGridView1.Columns(CellSpace4).HeaderCell.Style.BackColor = Color.LightGray
		DataGridView1.Columns(CellSalesForecastBln5).HeaderText = "Sales - Production Plan "
		DataGridView1.Columns(CellPPICForecastBln5).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_5).HeaderText = "5"
		DataGridView1.Columns(CellSpace5).HeaderText = ""
		DataGridView1.Columns(CellSpace5).HeaderCell.Style.BackColor = Color.LightGray
		DataGridView1.Columns(CellSalesForecastBln6).HeaderText = "Sales - Production Plan "
		DataGridView1.Columns(CellPPICForecastBln6).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_6).HeaderText = "6"
		DataGridView1.Columns(CellSpace6).HeaderText = ""
		DataGridView1.Columns(CellSpace6).HeaderCell.Style.BackColor = Color.LightGray
		DataGridView1.Columns(CellStatus).HeaderText = "Status"

		DataGridView1.Columns(CellChkBox).ReadOnly = False

		'DataGridView1.Columns(CellActualCurrentMonth).DisplayIndex = 4
		'DataGridView1.Columns(CellForecastCurrentMonth).DisplayIndex = 5
		'DataGridView1.Columns(CellPersenCurrentMonth).DisplayIndex = 6

		DataGridView1.Columns(4).DisplayIndex = 4
		DataGridView1.Columns(9).DisplayIndex = 5
		DataGridView1.Columns(6).DisplayIndex = 6

		' 1. Definisikan array yang berisi variabel-variabel kolommu
		Dim colList() As Integer = {
			CellSalesForecastBln3, CellPPICForecastBln3, CellUrut_3, CellSpace3,
		CellSalesForecastBln4, CellPPICForecastBln4, CellUrut_4, CellSpace4,
		CellSalesForecastBln5, CellPPICForecastBln5, CellUrut_5, CellSpace5,
		CellSalesForecastBln6, CellPPICForecastBln6, CellUrut_6, CellSpace6
}

		' 2. Gunakan For i untuk looping berdasarkan jumlah item di array
		For i As Integer = 0 To colList.Length - 1
			DataGridView1.Columns(colList(i)).Visible = False
		Next

		'If Display_Transaksi_ForecastOrder.asal = "isi_lv" Then
		'    Txt_NoFaktur_Leave(Txt_NoFaktur, e)
		'ElseIf Display_Transaksi_ForecastOrder.asal = "Ref" Then
		'Try
		'        OpenConn()

		'        get_no_faktur()
		'        SQL = "select a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner from EMI_Transaksi_Sales_Forecasting_Detail a,"
		'        SQL = SQL & "Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and "
		'        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Display_Transaksi_ForecastOrder.nfak & "' "
		'        SQL = SQL & "group by a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner "
		'        Using Ds = BindingTrans(SQL)
		'            With Ds.Tables("MyTable")
		'                For i As Integer = 0 To .Rows.Count - 1
		'                    DataGridView1.Rows.Add(1)

		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln1).Style.BackColor = Color.LightYellow
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln1).Style.BackColor = Color.LightCyan
		'                    DataGridView1.Rows(i).Cells(CellSpace1).Style.BackColor = Color.LightGray
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln2).Style.BackColor = Color.LightYellow
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln2).Style.BackColor = Color.LightCyan
		'                    DataGridView1.Rows(i).Cells(CellSpace2).Style.BackColor = Color.LightGray
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln3).Style.BackColor = Color.LightYellow
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln3).Style.BackColor = Color.LightCyan
		'                    DataGridView1.Rows(i).Cells(CellSpace3).Style.BackColor = Color.LightGray
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln4).Style.BackColor = Color.LightYellow
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln4).Style.BackColor = Color.LightCyan
		'                    DataGridView1.Rows(i).Cells(CellSpace4).Style.BackColor = Color.LightGray
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln5).Style.BackColor = Color.LightYellow
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln5).Style.BackColor = Color.LightCyan
		'                    DataGridView1.Rows(i).Cells(CellSpace5).Style.BackColor = Color.LightGray
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln6).Style.BackColor = Color.LightYellow
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln6).Style.BackColor = Color.LightCyan
		'                    DataGridView1.Rows(i).Cells(CellSpace6).Style.BackColor = Color.LightGray
		'                    DataGridView1.Rows(i).Cells(CellStatus).Style.BackColor = Color.Yellow

		'                    DataGridView1.Rows(i).Cells(CellKdBrg).Value = .Rows(i).Item("Kode_Barang")
		'                    DataGridView1.Rows(i).Cells(CellNmBrg).Value = .Rows(i).Item("Nama")
		'                    DataGridView1.Rows(i).Cells(CellAvg3Bulan).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellForecastCurrentMonth).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellActualCurrentMonth).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellPersenCurrentMonth).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln1).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln1).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellUrut_1).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellRV_1).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellSpace1).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln2).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln2).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellUrut_2).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellRV_2).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellSpace2).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln3).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln3).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellUrut_3).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellRV_3).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellSpace3).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln4).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln4).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellUrut_4).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellRV_4).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellSpace4).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln5).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln5).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellUrut_5).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellRV_5).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellSpace5).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellSalesForecastBln6).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellPPICForecastBln6).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellUrut_6).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellRV_6).Value = "0"
		'                    DataGridView1.Rows(i).Cells(CellSpace6).Value = ""
		'                    DataGridView1.Rows(i).Cells(CellStatus).Value = "NEW"
		'                    Btn_Simpan.Tag = "&Simpan"
		'                Next
		'            End With
		'        End Using

		'        CloseConn()
		'    Catch ex As Exception
		'        CloseConn()
		'        MessageBox.Show(ex.Message)
		'        Exit Sub
		'    End Try
		'Else
		Try
			OpenConn()

			get_no_faktur()

			'================================================================================
			'============== ambil berapa bulan yang ingin direlease secara dinamis===========
			'================================================================================

			'SQL = "Select Jumlah_Bulan_Production_Plan from init "
			'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'	If dr.Read Then
			'		initBulanOtomatis = dr("Jumlah_Bulan_Production_Plan")
			'	Else

			'		dr.Close()
			'		CloseConn()
			'		MessageBox.Show("Production Plan tidak ditemukan...", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'		Exit Sub
			'	End If
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		'End If

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_TambahBarang.Click
		If Cmb_Bulan.Text.Trim.Length = 0 Then
			MessageBox.Show("Bulan Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Bulan.Focus() : Exit Sub
		ElseIf Cmb_Tahun.Text.Trim.Length = 0 Then
			MessageBox.Show("Tahun Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Tahun.Focus() : Exit Sub
		ElseIf Cmb_Lokasi.Text.Trim.Length = 0 Then
			MessageBox.Show("Lokasi Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Lokasi.Focus() : Exit Sub
		End If

		'buka lagi

		'Cmb_Lokasi.Enabled = False
		'Cmb_Bulan.Enabled = False
		'Cmb_Tahun.Enabled = False
		SD_Pilih_Produk_Forecasting.urutcmb = Cmb_Bulan.SelectedIndex
		SD_Pilih_Produk_Forecasting.ShowDialog()
	End Sub

	Private Sub Cmb_Tahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Tahun.SelectedIndexChanged
		If Cmb_Bulan.SelectedIndex = -1 Then
			Exit Sub
		ElseIf Cmb_Tahun.SelectedIndex = -1 Then
			Exit Sub
		ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
			Exit Sub
		End If

		'DateTimePicker1.Enabled = True
		'Txt_Keterangan.Enabled = True
		isLoading = True
		CheckBox1.Checked = False
		Start_Loading(Me)

		Get_Data_Rix()
		End_Loading(Me)

		isLoading = False

	End Sub

	Public Sub Txt_NoFaktur_Leave(sender As Object, e As EventArgs)
		If Txt_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_NoFaktur.Focus() : Exit Sub
		End If
		'UserID
		Dim ada_data As Boolean = True
		Try
			OpenConn()

			SQL = "select No_Faktur,Tanggal,Keterangan,Lokasi,Bulan,Tahun from EMI_Transaksi_Sales_Forecasting where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Faktur = '" & Txt_NoFaktur.Text & "'"
			Using dr = OpenTrans(SQL)
				If dr.Read Then

					Cmb_Lokasi.Text = dr("Lokasi")
					For index = 0 To arrBulanMM.Count - 1
						If arrBulanMM.Item(index) = dr("Bulan") Then
							Cmb_Bulan.SelectedIndex = index
						End If
					Next

					Cmb_Bulan.Text = dr("Bulan")
					Cmb_Tahun.Text = dr("Tahun")
					'DateTimePicker1.Enabled = False
					'Cmb_Bulan.Enabled = False
					'Cmb_Tahun.Enabled = False
					Cmb_Lokasi.Enabled = True
					Btn_Simpan.Tag = "&Refresh"

					ada_data = True
				Else
					dr.Close()
					get_no_faktur()

					' Cmb_Lokasi.SelectedIndex = -1
					'   Cmb_Bulan.SelectedIndex = -1
					'  Cmb_Tahun.SelectedIndex = -1

					'Cmb_Bulan.Enabled = False
					'Cmb_Tahun.Enabled = False
					'  Cmb_Lokasi.Enabled = True
					Btn_Simpan.Tag = "&Simpan"
					'Btn_Realese.Visible = False
					Btn_Realese.Enabled = False
				End If
			End Using

			DataGridView1.Rows.Clear()
			Arrbarang.Clear()
			Arrlokasi.Clear()
			ArrNama.Clear()

			'If fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "select a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner from EMI_Transaksi_Sales_Forecasting_Detail a,"
			'    SQL = SQL & "Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and "
			'    SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
			'    SQL = SQL & "group by a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner "

			'ElseIf fStatus = "Transaksi_ForecastOrder_PPIC" Then

			'    SQL = "select a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner from EMI_Transaksi_Sales_Forecasting_Detail a,"
			'    SQL = SQL & "Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and "
			'    SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
			'    SQL = SQL & "and a.flag_validasi = 'Y' "
			'    SQL = SQL & "group by a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner "

			'End If
			OpenConn()
			Dim gudangDefault As String = ""
			SQL = "select Kode_Stock_Owner_Gudang From Binding_Lokasi_Gudang where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Gudang_Default = 'Y' and Kode_Stock_Owner = '" & Cmb_Lokasi.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					gudangDefault = Dr("Kode_Stock_Owner_Gudang")
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show("Terjadi kesalahan , gudang default tidak ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "select distinct a.Kode_Barang,a.Nama From barang a "
			SQL = SQL & "inner join EMI_Group_Jenis b on "
			SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.id_group_jenis = b.id_group_jenis "
			SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Flag_Finished_Good = 'Y' "
			SQL = SQL & "and a.Aktif = 'Y' and a.Flag_Tampil_Production_Plan = 'Y' "
			SQL = SQL & "order by Nama "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						Arrbarang.Add(.Rows(i).Item("Kode_Barang"))
						Arrlokasi.Add(gudangDefault)
						ArrNama.Add(.Rows(i).Item("Nama"))
					Next
				End With
			End Using
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If ada_data = True Then
			'Get_Barang()
			Get_Barang_Rix()
		End If

	End Sub

	'Versi Optimasi pertama
	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		'Validasi input
		If Txt_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_NoFaktur.Focus() : Exit Sub
		ElseIf Cmb_Lokasi.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Error_Lokasi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Lokasi.Focus() : Exit Sub
		ElseIf Cmb_Bulan.Text.Trim.Length = 0 Then
			MessageBox.Show("Bulan Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Bulan.Focus() : Exit Sub
		ElseIf Cmb_Tahun.Text.Trim.Length = 0 Then
			MessageBox.Show("Tahun Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Tahun.Focus() : Exit Sub
		End If

		'Cek akses
		Try
			OpenConn()
			If fStatus = "Transaksi_ForecastOrder_PPIC" Then
				If CekButtonRole("EMI_Transaksi_ForecastOrder_PPIC") = "T" Then
					CloseConn() : MessageBox.Show("Anda tidak memiliki akses ! !") : Exit Sub
				End If
			ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
				If CekButtonRole("EMI_Transaksi_ForecastOrder_Sales") = "T" Then
					CloseConn() : MessageBox.Show("Anda tidak memiliki akses ! !") : Exit Sub
				End If
			End If
			CloseConn()
		Catch ex As Exception
			CloseConn() : MessageBox.Show(ex.Message) : Exit Sub
		End Try

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'Cache mapping bulan integer - nama bulan string
			Dim dictBulanMM As New Dictionary(Of Integer, String)
			For idx = 0 To arrBulan.Count - 1
				If Not dictBulanMM.ContainsKey(arrBulan(idx)) Then
					dictBulanMM(arrBulan(idx)) = arrBulanMM(idx)
				End If
			Next

			'Hitung nama + tahun untuk 3 bulan yang diproses
			Dim listBulanProses As New List(Of String)
			Dim listTahunProses As New List(Of Integer)
			Dim tempA As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
			Dim tempThn As Integer = Val(Cmb_Tahun.Text)

			For jj As Integer = 0 To 2
				listBulanProses.Add(dictBulanMM(tempA))
				listTahunProses.Add(tempThn)
				If tempA = 12 Then : tempA = 1 : tempThn += 1
				Else : tempA += 1
				End If
			Next

			Dim bulanSQL As String = String.Join(",", listBulanProses.Select(Function(x) "'" & x & "'").Distinct())
			Dim tahunSQL As String = String.Join(",", listTahunProses.Distinct())

			'Pre-load gudang default
			Dim fSO As String = ""
			SQL = "select Kode_Stock_Owner_Gudang From Binding_Lokasi_Gudang " &
			  "where Kode_Perusahaan = '" & KodePerusahaan & "' " &
			  "and Gudang_Default = 'Y' and Kode_Stock_Owner = '" & Cmb_Lokasi.Text & "'"
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					fSO = dr("Kode_Stock_Owner_Gudang")
				Else
					dr.Close() : CloseTrans() : CloseConn()
					MessageBox.Show("Terjadi kesalahan , gudang default tidak ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'Pre-load semua header yang sudah ada untuk 3 bulan
			'Menggantikan SELECT header per bulan di dalam loop
			Dim dictHeader As New Dictionary(Of String, String) ' key: bulan|tahun -> no_faktur

			SQL = "SELECT bulan, tahun, no_faktur FROM EMI_Transaksi_Sales_Forecasting " &
			  "WHERE kode_perusahaan = '" & KodePerusahaan & "' " &
			  "AND bulan IN (" & bulanSQL & ") " &
			  "AND tahun IN (" & tahunSQL & ") " &
			  "AND status IS NULL"
			Using ds = BindingTrans(SQL)
				For Each row As DataRow In ds.Tables("MyTable").Rows
					dictHeader(row("bulan") & "|" & row("tahun")) = row("no_faktur").ToString()
				Next
			End Using

			'Insert header yang belum ada (maks 3 INSERT, bukan per barang)
			For jj As Integer = 0 To 2
				Dim bNama As String = listBulanProses(jj)
				Dim bThn As Integer = listTahunProses(jj)
				Dim hKey As String = bNama & "|" & bThn

				If Not dictHeader.ContainsKey(hKey) Then
					get_no_faktur()
					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting" &
					  "(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_referensi,Status_Data) VALUES(" &
					  "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "'," &
					  "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & Cmb_Lokasi.Text & "'," &
					  "'" & bNama & "','" & bThn & "'," &
					  If(Cb_Referensi.Checked, "'Y'", "NULL") & ",'UNSUBMITTED')"
					ExecuteTrans(SQL)
					dictHeader(hKey) = Txt_NoFaktur.Text
				End If
			Next

			'Pre-load semua detail yang sudah ada
			'Menggantikan: SELECT detail + SELECT rv+nilai per barang per bulan
			'Sekaligus menyimpan rv dan nilai lama untuk keperluan UPDATE
			Dim dictDetail As New Dictionary(Of String, DataRow) ' key: bulan|tahun|kode_barang|kode_stock_owner

			SQL = "SELECT bulan, tahun, kode_barang, kode_stock_owner, " &
			  "urut, cast(rv as bigint) as rvx, nilai_sales, nilai_ppic " &
			  "FROM EMI_Transaksi_Sales_Forecasting_Detail " &
			  "WHERE kode_perusahaan = '" & KodePerusahaan & "' " &
			  "AND bulan IN (" & bulanSQL & ") " &
			  "AND tahun IN (" & tahunSQL & ")"
			Using ds = BindingTrans(SQL)
				For Each row As DataRow In ds.Tables("MyTable").Rows
					Dim key As String = row("bulan") & "|" & row("tahun") & "|" &
								row("kode_barang") & "|" & row("kode_stock_owner")
					dictDetail(key) = row
				Next
			End Using

			Dim listInsertDetailValues As New List(Of String)  ' VALUES untuk batch INSERT detail
			Dim listUpdateItems As New List(Of UpdateDetailItem)  ' data untuk batch UPDATE
			Dim listUpdateLogValues As New List(Of String)     ' VALUES untuk batch log UPDATE

			Dim bindingFlags As Reflection.BindingFlags =
			Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance

			For c As Integer = 0 To DataGridView1.Rows.Count - 1
				Get_Isi_Listview(c)

				' Ambil nilai per bulan SETELAH Get_Isi_Listview karena fungsi ini
				' mengisi ulang variabel LvUrut_1/2, lvSalesForecastBln1/2, dll per baris
				Dim cachedLvUrut(2) As String
				Dim cachedLvRV(2) As String
				Dim cachedLvSales(2) As String
				Dim cachedLvPPIC(2) As String
				For j As Integer = 1 To 2
					cachedLvUrut(j) = CType(Me.GetType().GetField("LvUrut_" & j, bindingFlags).GetValue(Me), String)
					cachedLvRV(j) = CType(Me.GetType().GetField("LvRV_" & j, bindingFlags).GetValue(Me), String)
					cachedLvSales(j) = CType(Me.GetType().GetField("lvSalesForecastBln" & j, bindingFlags).GetValue(Me), String)
					cachedLvPPIC(j) = CType(Me.GetType().GetField("lvPPICForecastBln" & j, bindingFlags).GetValue(Me), String)
				Next

				' Bulan ke-0: pastikan record detail bulan berjalan ada
				Dim b0 As String = listBulanProses(0)
				Dim thn0 As Integer = listTahunProses(0)
				Dim noFaktur0 As String = dictHeader(b0 & "|" & thn0)
				Dim detailKey0 As String = b0 & "|" & thn0 & "|" & lvKdBrg & "|" & fSO

				If Not dictDetail.ContainsKey(detailKey0) Then
					listInsertDetailValues.Add(
					"('" & KodePerusahaan & "','" & noFaktur0 & "','" & b0 & "','" & thn0 & "'," &
					"'" & fSO & "','" & lvKdBrg & "','0','0','" & lvSatuan & "')")
					' Tandai sudah diproses agar barang berikutnya tidak dobel insert
					dictDetail(detailKey0) = Nothing
				End If

				' Bulan ke-1 dan ke-2: INSERT baru atau UPDATE yang sudah ada
				For j As Integer = 1 To 2
					Dim bNama As String = listBulanProses(j)
					Dim bThn As Integer = listTahunProses(j)
					Dim noFakturJ As String = dictHeader(bNama & "|" & bThn)
					Dim detailKey As String = bNama & "|" & bThn & "|" & lvKdBrg & "|" & fSO

					Dim lvUrut As String = cachedLvUrut(j)
					Dim lvRV As String = cachedLvRV(j)
					Dim lvSalesBaru As String = HilangkanTanda(cachedLvSales(j))
					Dim lvPPICBaru As String = HilangkanTanda(cachedLvPPIC(j))

					If lvUrut = "" Then
						'Belum ada → tambahkan ke batch INSERT
						listInsertDetailValues.Add(
						"('" & KodePerusahaan & "','" & noFakturJ & "','" & bNama & "','" & bThn & "'," &
						"'" & fSO & "','" & lvKdBrg & "','" & lvSalesBaru & "','" & lvPPICBaru & "','" & lvSatuan & "')")
						dictDetail(detailKey) = Nothing ' tandai sudah diproses
					Else
						'Sudah ada -> cek RV dari dictionary, tidak perlu query ke DB
						If dictDetail.ContainsKey(detailKey) AndAlso dictDetail(detailKey) IsNot Nothing Then
							Dim existingRow As DataRow = dictDetail(detailKey)

							' Optimistic concurrency check
							If existingRow("rvx").ToString() <> lvRV Then
								CloseTrans() : CloseConn()
								MessageBox.Show("Data ini sudah diubah sebelumnya! Barang: " & lvKdBrg & " Bulan: " & bNama,
											Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							Dim NSalesLama As Double = existingRow("nilai_sales")
							Dim NPPICLama As Double = existingRow("nilai_ppic")
							Dim NSalesBaru As Double = Val(lvSalesBaru)
							Dim NPPICBaru As Double = Val(lvPPICBaru)

							' Tambahkan ke batch UPDATE
							listUpdateItems.Add(New UpdateDetailItem With {
							.Urut = lvUrut,
							.NoFaktur = noFakturJ,
							.NilaiSalesBaru = lvSalesBaru,
							.NilaiPPICBaru = lvPPICBaru,
							.NilaiSalesLama = NSalesLama,
							.NilaiPPICLama = NPPICLama
						})

							' Siapkan log UPDATE hanya jika nilai berubah
							If NSalesLama <> NSalesBaru Or NPPICLama <> NPPICBaru Then
								listUpdateLogValues.Add(
								"('" & KodePerusahaan & "','" & noFakturJ & "','" & lvUrut & "'," &
								"'" & NPPICLama & "','" & NSalesLama & "','UPDATE'," &
								"'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')")
							End If
						End If
					End If
				Next j
			Next c

			Dim batchSize As Integer = 500
			Dim listInsertLogValues As New List(Of String)

			' OUTPUT INSERTED menggantikan SELECT IDENT_CURRENT per INSERT
			' Semua urut baru didapat sekaligus tanpa query tambahan
			If listInsertDetailValues.Count > 0 Then
				For offset As Integer = 0 To listInsertDetailValues.Count - 1 Step batchSize
					Dim batch = listInsertDetailValues.Skip(offset).Take(batchSize).ToList()

					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Detail" &
					  "(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Sales,Nilai_PPIC,satuan) " &
					  "OUTPUT INSERTED.urut, INSERTED.No_Faktur " &
					  "VALUES " & String.Join(",", batch)

					Using ds = BindingTrans(SQL)
						For Each row As DataRow In ds.Tables("MyTable").Rows
							listInsertLogValues.Add(
							"('" & KodePerusahaan & "','" & row("No_Faktur") & "','" & row("urut") & "',0,0,'INSERT'," &
							"'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')")
						Next
					End Using
				Next
			End If

			'Batch INSERT log untuk record baru
			If listInsertLogValues.Count > 0 Then
				For offset As Integer = 0 To listInsertLogValues.Count - 1 Step batchSize
					Dim batch = listInsertLogValues.Skip(offset).Take(batchSize).ToList()
					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log" &
					  "(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,Tanggal,Jam) " &
					  "VALUES " & String.Join(",", batch)
					ExecuteTrans(SQL)
				Next
			End If

			'Batch INSERT log untuk record yang diupdate
			If listUpdateLogValues.Count > 0 Then
				For offset As Integer = 0 To listUpdateLogValues.Count - 1 Step batchSize
					Dim batch = listUpdateLogValues.Skip(offset).Take(batchSize).ToList()
					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log" &
					  "(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,Tanggal,Jam) " &
					  "VALUES " & String.Join(",", batch)
					ExecuteTrans(SQL)
				Next
			End If

			' Menggantikan: UPDATE per baris -> 1 UPDATE untuk semua baris sekaligus
			' Contoh hasil SQL:
			'   UPDATE ... SET
			'     Nilai_Sales = CASE urut WHEN '1' THEN '100' WHEN '2' THEN '200' ... END,
			'     Nilai_PPIC  = CASE urut WHEN '1' THEN '90'  WHEN '2' THEN '180' ... END
			'   WHERE urut IN ('1','2',...)
			If listUpdateItems.Count > 0 Then
				For offset As Integer = 0 To listUpdateItems.Count - 1 Step batchSize
					Dim batch = listUpdateItems.Skip(offset).Take(batchSize).ToList()

					Dim sbSales As New System.Text.StringBuilder("CASE urut ")
					Dim sbPPIC As New System.Text.StringBuilder("CASE urut ")
					Dim listUrut As New List(Of String)

					For Each item As UpdateDetailItem In batch
						sbSales.Append("WHEN '" & item.Urut & "' THEN '" & item.NilaiSalesBaru & "' ")
						sbPPIC.Append("WHEN '" & item.Urut & "' THEN '" & item.NilaiPPICBaru & "' ")
						listUrut.Add("'" & item.Urut & "'")
					Next

					sbSales.Append("ELSE Nilai_Sales END")
					sbPPIC.Append("ELSE Nilai_PPIC END")

					SQL = "UPDATE EMI_Transaksi_Sales_Forecasting_Detail SET " &
					  "Nilai_Sales = " & sbSales.ToString() & ", " &
					  "Nilai_PPIC = " & sbPPIC.ToString() & " " &
					  "WHERE urut IN (" & String.Join(",", listUrut) & ")"
					ExecuteTrans(SQL)
				Next
			End If

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			Try : CloseTrans() : Catch : End Try
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		kosong("REFRESH")

	End Sub

	'Versi awal
	'Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
	'	If Txt_NoFaktur.Text.Trim.Length = 0 Then
	'		MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'		Txt_NoFaktur.Focus() : Exit Sub

	'	ElseIf Cmb_Lokasi.Text.Trim.Length = 0 Then
	'		MessageBox.Show(Base_Language.Lang_Global_Error_Lokasi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'		Cmb_Lokasi.Focus() : Exit Sub
	'	ElseIf Cmb_Bulan.Text.Trim.Length = 0 Then
	'		MessageBox.Show("Bulan Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'		Cmb_Bulan.Focus() : Exit Sub
	'	ElseIf Cmb_Tahun.Text.Trim.Length = 0 Then
	'		MessageBox.Show("Tahun Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'		Cmb_Tahun.Focus() : Exit Sub
	'	End If

	'	'Dim fSimpan As Boolean = False
	'	'For a As Integer = 0 To DataGridView1.Rows.Count - 1
	'	'    If DataGridView1.Rows.Item(a).Cells(CellChkBox).Value = True Then
	'	'        fSimpan = True
	'	'        Exit For
	'	'    Else
	'	'        fSimpan = False
	'	'    End If
	'	'Next

	'	'If fSimpan = False Then
	'	'    MessageBox.Show("Pilih dahulu data yang mau disimpan....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'	'    Exit Sub
	'	'End If

	'	get_jam()

	'	Try
	'		OpenConn()
	'		Cmd.Transaction = Cn.BeginTransaction

	'		If fStatus = "Transaksi_ForecastOrder_PPIC" Then
	'			If CekButtonRole("EMI_Transaksi_ForecastOrder_PPIC") = "T" Then
	'				CloseTrans()
	'				CloseConn()
	'				MessageBox.Show("anda tidak memiliki akses ! !")
	'				Exit Sub
	'			End If

	'		ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
	'			If CekButtonRole("EMI_Transaksi_ForecastOrder_Sales") = "T" Then
	'				CloseTrans()
	'				CloseConn()
	'				MessageBox.Show("anda tidak memiliki akses ! !")
	'				Exit Sub
	'			End If
	'		End If

	'		''''If Btn_Simpan.Tag = "&Simpan" Then
	'		''''    get_no_faktur()

	'		''''    SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_referensi) VALUES("
	'		''''    SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
	'		''''    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & Txt_Keterangan.Text & "','" & Cmb_Lokasi.Text & "',"
	'		''''    SQL = SQL & "'" & arrBulanMM.Item(Cmb_Bulan.SelectedIndex) & "','" & Cmb_Tahun.Text & "' "
	'		''''    If Cb_Referensi.Checked = True Then
	'		''''        SQL = SQL & " ,'Y' ) "
	'		''''    Else
	'		''''        SQL = SQL & " ,NULL ) "
	'		''''    End If
	'		''''    ExecuteTrans(SQL)
	'		''''Else
	'		''''    SQL = "UPDATE EMI_Transaksi_Sales_Forecasting SET "
	'		''''    If Cb_Referensi.Checked = True Then
	'		''''        SQL = SQL & "flag_Referensi = 'Y' "
	'		''''    Else
	'		''''        SQL = SQL & "flag_Referensi = NULL "
	'		''''    End If
	'		''''    SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur.Text & "'"
	'		''''    ExecuteTrans(SQL)
	'		''''End If

	'		For c As Integer = 0 To DataGridView1.Rows.Count - 1
	'			' If DataGridView1.Rows.Item(c).Cells(CellChkBox).Value = True Then
	'			Get_Isi_Listview(c)

	'			Dim fSO As String = ""
	'			'SQL = "select Top(1)a.Lokasi_Gudang from EMI_Kategori_Gudang_PerLokasi a,Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
	'			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Kategori_Gudang = b.Id_Kategori_Gudang and "
	'			'SQL = SQL & "a.Kode_Stock_Owner = '" & Cmb_Lokasi.Text & "' and b.Kode_Barang = '" & lvKdBrg & "'"
	'			'Using Ds = BindingTrans(SQL)
	'			'    With Ds.Tables("MyTable")
	'			'        If .Rows.Count <> 0 Then
	'			'            For i As Integer = 0 To .Rows.Count - 1
	'			'                fSO = .Rows(i).Item("Lokasi_Gudang")
	'			'            Next
	'			'        End If
	'			'    End With
	'			'End Using

	'			SQL = "select Kode_Stock_Owner_Gudang From Binding_Lokasi_Gudang where "
	'			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Gudang_Default = 'Y' and Kode_Stock_Owner = '" & Cmb_Lokasi.Text & "' "
	'			Using Dr = OpenTrans(SQL)
	'				If Dr.Read Then
	'					fSO = Dr("Kode_Stock_Owner_Gudang")
	'				Else
	'					Dr.Close()
	'					CloseConn()
	'					MessageBox.Show("Terjadi kesalahan , gudang default tidak ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'					Exit Sub
	'				End If
	'			End Using

	'			Dim a As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
	'			Dim fthn As Integer = Val(Cmb_Tahun.Text)
	'			Dim b As String = ""
	'			Dim x_no_urut_det As String = 0
	'			Dim NSales_Lama As Double = 0
	'			Dim NPPIC_Lama As Double = 0

	'			For index = 0 To arrBulan.Count - 1
	'				If arrBulan.Item(index) = a Then
	'					b = arrBulanMM.Item(index)
	'				End If
	'			Next

	'			'BULAN YANG DIPILIH
	'			SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
	'			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
	'			Using dr = OpenTrans(SQL)
	'				If dr.Read Then
	'					Txt_NoFaktur.Text = dr("no_faktur")
	'				Else
	'					dr.Close()
	'					get_no_faktur()

	'					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_referensi, Status_Data) VALUES("
	'					SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
	'					SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & Cmb_Lokasi.Text & "',"
	'					SQL = SQL & "'" & b & "','" & fthn & "' "
	'					If Cb_Referensi.Checked = True Then
	'						SQL = SQL & " ,'Y', "
	'					Else
	'						SQL = SQL & " ,NULL, "
	'					End If
	'					SQL = SQL & "'UNSUBMITTED')"
	'					ExecuteTrans(SQL)

	'				End If
	'			End Using

	'			SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting_Detail a where no_faktur='" & Txt_NoFaktur.Text & "' "
	'			SQL = SQL & "And a.kode_barang='" & lvKdBrg & "' and kode_stock_owner='" & fSO & "'  And kode_perusahaan='" & KodePerusahaan & "' "
	'			Using dr4 = OpenTrans(SQL)
	'				If Not dr4.Read Then
	'					dr4.Close()
	'					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Sales,Nilai_PPIC, satuan) "
	'					SQL = SQL & "VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & b & "','" & fthn & "',"
	'					SQL = SQL & "'" & fSO & "','" & lvKdBrg & "','" & 0 & "','" & 0 & "', '" & lvSatuan & "')"
	'					ExecuteTrans(SQL)

	'					SQL = "select IDENT_CURRENT('EMI_Transaksi_Sales_Forecasting_Detail') as urutan"
	'					Using Dr = OpenTrans(SQL)
	'						If Dr.Read Then
	'							x_no_urut_det = "" & Dr("urutan") & ""
	'						End If
	'					End Using

	'					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'					SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
	'					SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'					ExecuteTrans(SQL)

	'				End If
	'			End Using

	'			'--- 1
	'			If a = 12 Then
	'				a = 1
	'				fthn = fthn + 1
	'			Else
	'				a = a + 1
	'			End If
	'			For index = 0 To arrBulan.Count - 1
	'				If arrBulan.Item(index) = a Then
	'					b = arrBulanMM.Item(index)
	'				End If
	'			Next

	'			SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
	'			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
	'			Using dr = OpenTrans(SQL)
	'				If dr.Read Then
	'					Txt_NoFaktur.Text = dr("no_faktur")
	'				Else
	'					dr.Close()
	'					get_no_faktur()

	'					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_referensi, Status_Data) VALUES("
	'					SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
	'					SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & Cmb_Lokasi.Text & "',"
	'					SQL = SQL & "'" & b & "','" & fthn & "' "
	'					If Cb_Referensi.Checked = True Then
	'						SQL = SQL & " ,'Y', "
	'					Else
	'						SQL = SQL & " ,NULL, "
	'					End If
	'					SQL = SQL & "'UNSUBMITTED')"
	'					ExecuteTrans(SQL)

	'				End If
	'			End Using

	'			If LvUrut_1 = "" Then

	'				SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Sales,Nilai_PPIC,satuan) "
	'				SQL = SQL & "VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & b & "','" & fthn & "',"
	'				SQL = SQL & "'" & fSO & "','" & lvKdBrg & "','" & HilangkanTanda(lvSalesForecastBln1) & "','" & HilangkanTanda(lvPPICForecastBln1) & "','" & lvSatuan & "')"
	'				ExecuteTrans(SQL)

	'				SQL = "select IDENT_CURRENT('EMI_Transaksi_Sales_Forecasting_Detail') as urutan"
	'				Using Dr = OpenTrans(SQL)
	'					If Dr.Read Then
	'						x_no_urut_det = "" & Dr("urutan") & ""
	'					End If
	'				End Using

	'				SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'				SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
	'				SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'				ExecuteTrans(SQL)
	'			Else
	'				NSales_Lama = 0
	'				NPPIC_Lama = 0

	'				'cek rv 1
	'				SQL = "select cast(rv as bigint) as rvx, urut "
	'				SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_Detail "
	'				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and  urut = '" & LvUrut_1 & "' "
	'				Using dr = OpenTrans(SQL)
	'					If dr.Read Then
	'						If dr("rvx") <> LvRV_1 Then
	'							dr.Close()
	'							CloseTrans()
	'							CloseConn()
	'							MessageBox.Show("Data ini sudah diubah sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'							Exit Sub
	'						End If
	'					End If
	'				End Using

	'				SQL = "select Nilai_Sales,Nilai_PPIC,Urut from EMI_Transaksi_Sales_Forecasting_Detail where Urut = '" & LvUrut_1 & "' "
	'				Using dr = OpenTrans(SQL)
	'					If dr.Read Then
	'						'If dr("Nilai_Sales") <> Val(HilangkanTanda(lvSalesForecastBln1)) Then
	'						NSales_Lama = dr("Nilai_Sales")
	'						'ElseIf dr("Nilai_PPIC") <> Val(HilangkanTanda(lvPPICForecastBln1)) Then
	'						NPPIC_Lama = dr("Nilai_PPIC")
	'						'End If
	'					End If
	'				End Using

	'				If NSales_Lama <> Val(HilangkanTanda(lvSalesForecastBln1)) Or NPPIC_Lama <> Val(HilangkanTanda(lvPPICForecastBln1)) Then
	'					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'					SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & LvUrut_1 & "',"
	'					SQL = SQL & "'" & NPPIC_Lama & "','" & NSales_Lama & "','UPDATE',"
	'					SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'					ExecuteTrans(SQL)
	'				End If

	'				SQL = "UPDATE EMI_Transaksi_Sales_Forecasting_Detail SET Nilai_Sales = '" & HilangkanTanda(lvSalesForecastBln1) & "' "
	'				SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(lvPPICForecastBln1) & "' WHERE Urut = '" & LvUrut_1 & "'"
	'				ExecuteTrans(SQL)
	'			End If

	'			'2
	'			If a = 12 Then
	'				a = 1
	'				fthn = fthn + 1
	'			Else
	'				a = a + 1
	'			End If

	'			For index = 0 To arrBulan.Count - 1
	'				If arrBulan.Item(index) = a Then
	'					b = arrBulanMM.Item(index)
	'				End If
	'			Next

	'			SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
	'			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
	'			Using dr = OpenTrans(SQL)
	'				If dr.Read Then
	'					Txt_NoFaktur.Text = dr("no_faktur")
	'				Else
	'					dr.Close()
	'					get_no_faktur()

	'					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_referensi, Status_Data) VALUES("
	'					SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
	'					SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & Cmb_Lokasi.Text & "',"
	'					SQL = SQL & "'" & b & "','" & fthn & "' "
	'					If Cb_Referensi.Checked = True Then
	'						SQL = SQL & " ,'Y', "
	'					Else
	'						SQL = SQL & " ,NULL, "
	'					End If
	'					SQL = SQL & "'UNSUBMITTED')"
	'					ExecuteTrans(SQL)

	'				End If
	'			End Using

	'			If LvUrut_2 = "" Then
	'				SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Sales,Nilai_PPIC,satuan) "
	'				SQL = SQL & "VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & b & "','" & fthn & "',"
	'				SQL = SQL & "'" & fSO & "','" & lvKdBrg & "','" & HilangkanTanda(lvSalesForecastBln2) & "','" & HilangkanTanda(lvPPICForecastBln2) & "','" & lvSatuan & "')"
	'				ExecuteTrans(SQL)

	'				SQL = "select IDENT_CURRENT('EMI_Transaksi_Sales_Forecasting_Detail') as urutan"
	'				Using Dr = OpenTrans(SQL)
	'					If Dr.Read Then
	'						x_no_urut_det = "" & Dr("urutan") & ""
	'					End If
	'				End Using

	'				SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'				SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
	'				SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'				ExecuteTrans(SQL)
	'			Else
	'				NSales_Lama = 0
	'				NPPIC_Lama = 0

	'				'cek rv 2
	'				SQL = "select cast(rv as bigint) as rvx, urut "
	'				SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_Detail "
	'				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and  urut = '" & LvUrut_2 & "' "
	'				Using dr = OpenTrans(SQL)
	'					If dr.Read Then
	'						If dr("rvx") <> LvRV_2 Then
	'							dr.Close()
	'							CloseTrans()
	'							CloseConn()
	'							MessageBox.Show("Data ini sudah diubah sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'							Exit Sub
	'						End If
	'					End If
	'				End Using

	'				SQL = "select Nilai_Sales,Nilai_PPIC,Urut from EMI_Transaksi_Sales_Forecasting_Detail where Urut = '" & LvUrut_2 & "' "
	'				Using dr = OpenTrans(SQL)
	'					If dr.Read Then
	'						'If dr("Nilai_Sales") <> Val(HilangkanTanda(lvSalesForecastBln2)) Then
	'						NSales_Lama = dr("Nilai_Sales")
	'						'ElseIf dr("Nilai_PPIC") <> Val(HilangkanTanda(lvPPICForecastBln2)) Then
	'						NPPIC_Lama = dr("Nilai_PPIC")
	'						' End If
	'					End If
	'				End Using

	'				If NSales_Lama <> Val(HilangkanTanda(lvSalesForecastBln2)) Or NPPIC_Lama <> Val(HilangkanTanda(lvPPICForecastBln2)) Then
	'					SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'					SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & LvUrut_2 & "',"
	'					SQL = SQL & "'" & NPPIC_Lama & "','" & NSales_Lama & "','UPDATE',"
	'					SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'					ExecuteTrans(SQL)
	'				End If

	'				SQL = "UPDATE EMI_Transaksi_Sales_Forecasting_Detail SET Nilai_Sales = '" & HilangkanTanda(lvSalesForecastBln2) & "' "
	'				SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(lvPPICForecastBln2) & "' WHERE Urut = '" & LvUrut_2 & "'"
	'				ExecuteTrans(SQL)
	'			End If

	'			'    '3
	'			'    If a = 12 Then
	'			'        a = 1
	'			'        fthn = fthn + 1
	'			'    Else
	'			'        a = a + 1
	'			'    End If

	'			'    For index = 0 To arrBulan.Count - 1
	'			'        If arrBulan.Item(index) = a Then
	'			'            b = arrBulanMM.Item(index)
	'			'        End If
	'			'    Next

	'			'    SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
	'			'    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
	'			'    Using dr = OpenTrans(SQL)
	'			'        If dr.Read Then
	'			'            Txt_NoFaktur.Text = dr("no_faktur")
	'			'        Else
	'			'            dr.Close()
	'			'            get_no_faktur()

	'			'            SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_referensi, Status_Data) VALUES("
	'			'            SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
	'			'            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & Cmb_Lokasi.Text & "',"
	'			'            SQL = SQL & "'" & b & "','" & fthn & "' "
	'			'            If Cb_Referensi.Checked = True Then
	'			'                SQL = SQL & " ,'Y', "
	'			'            Else
	'			'                SQL = SQL & " ,NULL, "
	'			'            End If
	'			'            SQL = SQL & "'UNSUBMITTED')"
	'			'            ExecuteTrans(SQL)

	'			'        End If
	'			'    End Using

	'			'    If LvUrut_3 = "" Then
	'			'        SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Sales,Nilai_PPIC,satuan) "
	'			'        SQL = SQL & "VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & b & "','" & fthn & "',"
	'			'        SQL = SQL & "'" & fSO & "','" & lvKdBrg & "','" & HilangkanTanda(lvSalesForecastBln3) & "','" & HilangkanTanda(lvPPICForecastBln3) & "','" & lvSatuan & "')"
	'			'        ExecuteTrans(SQL)

	'			'        SQL = "select IDENT_CURRENT('EMI_Transaksi_Sales_Forecasting_Detail') as urutan"
	'			'        Using Dr = OpenTrans(SQL)
	'			'            If Dr.Read Then
	'			'                x_no_urut_det = "" & Dr("urutan") & ""
	'			'            End If
	'			'        End Using

	'			'        SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'			'        SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
	'			'        SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'			'        ExecuteTrans(SQL)
	'			'    Else
	'			'        NSales_Lama = 0
	'			'        NPPIC_Lama = 0

	'			'        'cek rv 3
	'			'        SQL = "select cast(rv as bigint) as rvx, urut "
	'			'        SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_Detail "
	'			'        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and  urut = '" & LvUrut_3 & "' "
	'			'        Using dr = OpenTrans(SQL)
	'			'            If dr.Read Then
	'			'                If dr("rvx") <> LvRV_3 Then
	'			'                    dr.Close()
	'			'                    CloseTrans()
	'			'                    CloseConn()
	'			'                    MessageBox.Show("Data ini sudah diubah sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'			'                    Exit Sub
	'			'                End If
	'			'            End If
	'			'        End Using

	'			'        SQL = "select Nilai_Sales,Nilai_PPIC,Urut from EMI_Transaksi_Sales_Forecasting_Detail where Urut = '" & LvUrut_3 & "' "
	'			'        Using dr = OpenTrans(SQL)
	'			'            If dr.Read Then
	'			'                'If dr("Nilai_Sales") <> Val(HilangkanTanda(lvSalesForecastBln3)) Then
	'			'                NSales_Lama = dr("Nilai_Sales")
	'			'                'ElseIf dr("Nilai_PPIC") <> Val(HilangkanTanda(lvPPICForecastBln3)) Then
	'			'                NPPIC_Lama = dr("Nilai_PPIC")
	'			'                'End If
	'			'            End If
	'			'        End Using

	'			'        If NSales_Lama <> Val(HilangkanTanda(lvSalesForecastBln3)) Or NPPIC_Lama <> Val(HilangkanTanda(lvPPICForecastBln3)) Then
	'			'            SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'			'            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & LvUrut_3 & "',"
	'			'            SQL = SQL & "'" & NPPIC_Lama & "','" & NSales_Lama & "','UPDATE',"
	'			'            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'			'            ExecuteTrans(SQL)
	'			'        End If

	'			'        SQL = "UPDATE EMI_Transaksi_Sales_Forecasting_Detail SET Nilai_Sales = '" & HilangkanTanda(lvSalesForecastBln3) & "' "
	'			'        SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(lvPPICForecastBln3) & "' WHERE Urut = '" & LvUrut_3 & "'"
	'			'        ExecuteTrans(SQL)
	'			'    End If

	'			'    '4
	'			'    If a = 12 Then
	'			'        a = 1
	'			'        fthn = fthn + 1
	'			'    Else
	'			'        a = a + 1
	'			'    End If

	'			'    For index = 0 To arrBulan.Count - 1
	'			'        If arrBulan.Item(index) = a Then
	'			'            b = arrBulanMM.Item(index)
	'			'        End If
	'			'    Next

	'			'    SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
	'			'    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
	'			'    Using dr = OpenTrans(SQL)
	'			'        If dr.Read Then
	'			'            Txt_NoFaktur.Text = dr("no_faktur")
	'			'        Else
	'			'            dr.Close()
	'			'            get_no_faktur()

	'			'            SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_referensi, Status_Data) VALUES("
	'			'            SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
	'			'            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & Cmb_Lokasi.Text & "',"
	'			'            SQL = SQL & "'" & b & "','" & fthn & "' "
	'			'            If Cb_Referensi.Checked = True Then
	'			'                SQL = SQL & " ,'Y', "
	'			'            Else
	'			'                SQL = SQL & " ,NULL, "
	'			'            End If
	'			'            SQL = SQL & "'UNSUBMITTED')"
	'			'            ExecuteTrans(SQL)

	'			'        End If
	'			'    End Using

	'			'    If LvUrut_4 = "" Then
	'			'        SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Sales,Nilai_PPIC,satuan) "
	'			'        SQL = SQL & "VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & b & "','" & fthn & "',"
	'			'        SQL = SQL & "'" & fSO & "','" & lvKdBrg & "','" & HilangkanTanda(lvSalesForecastBln4) & "','" & HilangkanTanda(lvPPICForecastBln4) & "','" & lvSatuan & "')"
	'			'        ExecuteTrans(SQL)

	'			'        SQL = "select IDENT_CURRENT('EMI_Transaksi_Sales_Forecasting_Detail') as urutan"
	'			'        Using Dr = OpenTrans(SQL)
	'			'            If Dr.Read Then
	'			'                x_no_urut_det = "" & Dr("urutan") & ""
	'			'            End If
	'			'        End Using

	'			'        SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'			'        SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
	'			'        SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'			'        ExecuteTrans(SQL)
	'			'    Else
	'			'        NSales_Lama = 0
	'			'        NPPIC_Lama = 0

	'			'        'cek rv 4
	'			'        SQL = "select cast(rv as bigint) as rvx, urut "
	'			'        SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_Detail "
	'			'        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and  urut = '" & LvUrut_4 & "' "
	'			'        Using dr = OpenTrans(SQL)
	'			'            If dr.Read Then
	'			'                If dr("rvx") <> LvRV_4 Then
	'			'                    dr.Close()
	'			'                    CloseTrans()
	'			'                    CloseConn()
	'			'                    MessageBox.Show("Data ini sudah diubah sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'			'                    Exit Sub
	'			'                End If
	'			'            End If
	'			'        End Using

	'			'        SQL = "select Nilai_Sales,Nilai_PPIC,Urut from EMI_Transaksi_Sales_Forecasting_Detail where Urut = '" & LvUrut_4 & "' "
	'			'        Using dr = OpenTrans(SQL)
	'			'            If dr.Read Then
	'			'                'If dr("Nilai_Sales") <> Val(HilangkanTanda(lvSalesForecastBln4)) Then
	'			'                NSales_Lama = dr("Nilai_Sales")
	'			'                ' ElseIf dr("Nilai_PPIC") <> Val(HilangkanTanda(lvPPICForecastBln4)) Then
	'			'                NPPIC_Lama = dr("Nilai_PPIC")
	'			'                ' End If
	'			'            End If
	'			'        End Using

	'			'        If NSales_Lama <> Val(HilangkanTanda(lvSalesForecastBln4)) Or NPPIC_Lama <> Val(HilangkanTanda(lvPPICForecastBln4)) Then
	'			'            SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'			'            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & LvUrut_4 & "',"
	'			'            SQL = SQL & "'" & NPPIC_Lama & "','" & NSales_Lama & "','UPDATE',"
	'			'            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'			'            ExecuteTrans(SQL)
	'			'        End If

	'			'        SQL = "UPDATE EMI_Transaksi_Sales_Forecasting_Detail SET Nilai_Sales = '" & HilangkanTanda(lvSalesForecastBln4) & "' "
	'			'        SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(lvPPICForecastBln4) & "' WHERE Urut = '" & LvUrut_4 & "'"
	'			'        ExecuteTrans(SQL)
	'			'    End If
	'			'    '5
	'			'    If a = 12 Then
	'			'        a = 1
	'			'        fthn = fthn + 1
	'			'    Else
	'			'        a = a + 1
	'			'    End If

	'			'    For index = 0 To arrBulan.Count - 1
	'			'        If arrBulan.Item(index) = a Then
	'			'            b = arrBulanMM.Item(index)
	'			'        End If
	'			'    Next

	'			'    SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
	'			'    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
	'			'    Using dr = OpenTrans(SQL)
	'			'        If dr.Read Then
	'			'            Txt_NoFaktur.Text = dr("no_faktur")
	'			'        Else
	'			'            dr.Close()
	'			'            get_no_faktur()

	'			'            SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_referensi, Status_Data) VALUES("
	'			'            SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
	'			'            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & Cmb_Lokasi.Text & "',"
	'			'            SQL = SQL & "'" & b & "','" & fthn & "' "
	'			'            If Cb_Referensi.Checked = True Then
	'			'                SQL = SQL & " ,'Y', "
	'			'            Else
	'			'                SQL = SQL & " ,NULL, "
	'			'            End If
	'			'            SQL = SQL & "'UNSUBMITTED')"
	'			'            ExecuteTrans(SQL)

	'			'        End If
	'			'    End Using

	'			'    If LvUrut_5 = "" Then
	'			'        SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Sales,Nilai_PPIC,satuan) "
	'			'        SQL = SQL & "VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & b & "','" & fthn & "',"
	'			'        SQL = SQL & "'" & fSO & "','" & lvKdBrg & "','" & HilangkanTanda(lvSalesForecastBln5) & "','" & HilangkanTanda(lvPPICForecastBln5) & "','" & lvSatuan & "')"
	'			'        ExecuteTrans(SQL)

	'			'        SQL = "select IDENT_CURRENT('EMI_Transaksi_Sales_Forecasting_Detail') as urutan"
	'			'        Using Dr = OpenTrans(SQL)
	'			'            If Dr.Read Then
	'			'                x_no_urut_det = "" & Dr("urutan") & ""
	'			'            End If
	'			'        End Using

	'			'        SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'			'        SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
	'			'        SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'			'        ExecuteTrans(SQL)
	'			'    Else
	'			'        NSales_Lama = 0
	'			'        NPPIC_Lama = 0

	'			'        'cek rv 5
	'			'        SQL = "select cast(rv as bigint) as rvx, urut "
	'			'        SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_Detail "
	'			'        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and  urut = '" & LvUrut_5 & "' "
	'			'        Using dr = OpenTrans(SQL)
	'			'            If dr.Read Then
	'			'                If dr("rvx") <> LvRV_5 Then
	'			'                    dr.Close()
	'			'                    CloseTrans()
	'			'                    CloseConn()
	'			'                    MessageBox.Show("Data ini sudah diubah sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'			'                    Exit Sub
	'			'                End If
	'			'            End If
	'			'        End Using

	'			'        SQL = "select Nilai_Sales,Nilai_PPIC,Urut from EMI_Transaksi_Sales_Forecasting_Detail where Urut = '" & LvUrut_5 & "' "
	'			'        Using dr = OpenTrans(SQL)
	'			'            If dr.Read Then
	'			'                'If dr("Nilai_Sales") <> Val(HilangkanTanda(lvSalesForecastBln5)) Then
	'			'                NSales_Lama = dr("Nilai_Sales")
	'			'                'ElseIf dr("Nilai_PPIC") <> Val(HilangkanTanda(lvPPICForecastBln5)) Then
	'			'                NPPIC_Lama = dr("Nilai_PPIC")
	'			'                ' End If
	'			'            End If
	'			'        End Using

	'			'        If NSales_Lama <> Val(HilangkanTanda(lvSalesForecastBln5)) Or NPPIC_Lama <> Val(HilangkanTanda(lvPPICForecastBln5)) Then
	'			'            SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'			'            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & LvUrut_5 & "',"
	'			'            SQL = SQL & "'" & NPPIC_Lama & "','" & NSales_Lama & "','UPDATE',"
	'			'            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'			'            ExecuteTrans(SQL)
	'			'        End If

	'			'        SQL = "UPDATE EMI_Transaksi_Sales_Forecasting_Detail SET Nilai_Sales = '" & HilangkanTanda(lvSalesForecastBln5) & "' "
	'			'        SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(lvPPICForecastBln5) & "' WHERE Urut = '" & LvUrut_5 & "'"
	'			'        ExecuteTrans(SQL)

	'			'    End If
	'			'    '6
	'			'    If a = 12 Then
	'			'        a = 1
	'			'        fthn = fthn + 1
	'			'    Else
	'			'        a = a + 1
	'			'    End If

	'			'    For index = 0 To arrBulan.Count - 1
	'			'        If arrBulan.Item(index) = a Then
	'			'            b = arrBulanMM.Item(index)
	'			'        End If
	'			'    Next

	'			'    SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
	'			'    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
	'			'    Using dr = OpenTrans(SQL)
	'			'        If dr.Read Then
	'			'            Txt_NoFaktur.Text = dr("no_faktur")
	'			'        Else
	'			'            dr.Close()
	'			'            get_no_faktur()

	'			'            SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_referensi, Status_Data) VALUES("
	'			'            SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
	'			'            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & Cmb_Lokasi.Text & "',"
	'			'            SQL = SQL & "'" & b & "','" & fthn & "' "
	'			'            If Cb_Referensi.Checked = True Then
	'			'                SQL = SQL & " ,'Y', "
	'			'            Else
	'			'                SQL = SQL & " ,NULL, "
	'			'            End If
	'			'            SQL = SQL & "'UNSUBMITTED')"
	'			'            ExecuteTrans(SQL)

	'			'        End If
	'			'    End Using

	'			'    If LvUrut_6 = "" Then
	'			'        SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Sales,Nilai_PPIC,satuan) "
	'			'        SQL = SQL & "VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & b & "','" & fthn & "',"
	'			'        SQL = SQL & "'" & fSO & "','" & lvKdBrg & "','" & HilangkanTanda(lvSalesForecastBln6) & "','" & HilangkanTanda(lvPPICForecastBln6) & "','" & lvSatuan & "')"
	'			'        ExecuteTrans(SQL)

	'			'        SQL = "select IDENT_CURRENT('EMI_Transaksi_Sales_Forecasting_Detail') as urutan"
	'			'        Using Dr = OpenTrans(SQL)
	'			'            If Dr.Read Then
	'			'                x_no_urut_det = "" & Dr("urutan") & ""
	'			'            End If
	'			'        End Using

	'			'        SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'			'        SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
	'			'        SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'			'        ExecuteTrans(SQL)
	'			'    Else
	'			'        NSales_Lama = 0
	'			'        NPPIC_Lama = 0

	'			'        'cek rv 6
	'			'        SQL = "select cast(rv as bigint) as rvx, urut "
	'			'        SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_Detail "
	'			'        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and  urut = '" & LvUrut_6 & "' "
	'			'        Using dr = OpenTrans(SQL)
	'			'            If dr.Read Then
	'			'                If dr("rvx") <> LvRV_6 Then
	'			'                    dr.Close()
	'			'                    CloseTrans()
	'			'                    CloseConn()
	'			'                    MessageBox.Show("Data ini sudah diubah sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'			'                    Exit Sub
	'			'                End If
	'			'            End If
	'			'        End Using

	'			'        SQL = "select Nilai_Sales,Nilai_PPIC,Urut from EMI_Transaksi_Sales_Forecasting_Detail where Urut = '" & LvUrut_6 & "' "
	'			'        Using dr = OpenTrans(SQL)
	'			'            If dr.Read Then
	'			'                'If dr("Nilai_Sales") <> Val(HilangkanTanda(lvSalesForecastBln6)) Then
	'			'                NSales_Lama = dr("Nilai_Sales")
	'			'                'ElseIf dr("Nilai_PPIC") <> Val(HilangkanTanda(lvPPICForecastBln6)) Then
	'			'                NPPIC_Lama = dr("Nilai_PPIC")
	'			'                ' End If
	'			'            End If
	'			'        End Using

	'			'        If NSales_Lama <> Val(HilangkanTanda(lvSalesForecastBln6)) Or NPPIC_Lama <> Val(HilangkanTanda(lvPPICForecastBln6)) Then
	'			'            SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
	'			'            SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "','" & LvUrut_6 & "',"
	'			'            SQL = SQL & "'" & NPPIC_Lama & "','" & NSales_Lama & "','UPDATE',"
	'			'            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
	'			'            ExecuteTrans(SQL)
	'			'        End If

	'			'        SQL = "UPDATE EMI_Transaksi_Sales_Forecasting_Detail SET Nilai_Sales = '" & HilangkanTanda(lvSalesForecastBln6) & "' "
	'			'        SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(lvPPICForecastBln6) & "' WHERE Urut = '" & LvUrut_6 & "'"
	'			'        ExecuteTrans(SQL)
	'			'    End If

	'			'    'End If
	'		Next

	'		Cmd.Transaction.Commit()
	'		CloseConn()
	'		MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
	'	Catch ex As Exception
	'		CloseConn()
	'		MessageBox.Show(ex.Message)
	'		Exit Sub
	'	End Try

	'	kosong("REFRESH")
	'	'Display_Transaksi_ForecastOrder.Btn_Refresh_Click(Btn_Simpan, e)
	'	'Me.Close()
	'End Sub

	Private Sub CB_PilihSeluruh_CheckedChanged(sender As Object, e As EventArgs) Handles CB_PilihSeluruh.CheckedChanged

		If CB_PilihSeluruh.Checked = True Then
			For a As Integer = 0 To DataGridView1.Rows.Count - 1
				DataGridView1.Rows(a).Cells(0).Value = True
			Next
		Else
			For a As Integer = 0 To DataGridView1.Rows.Count - 1
				DataGridView1.Rows(a).Cells(0).Value = False
			Next
		End If

	End Sub

	Private Sub Cmb_Bulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Bulan.SelectedIndexChanged
		If Cmb_Bulan.SelectedIndex = -1 Then
			Exit Sub
		ElseIf Cmb_Tahun.SelectedIndex = -1 Then
			Exit Sub
		ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
			Exit Sub
		End If

		'DateTimePicker1.Enabled = True
		'Txt_Keterangan.Enabled = True

		Start_Loading(Me)
		'get_data()
		Get_Data_Rix()
		End_Loading(Me)
	End Sub

	Private Sub Btn_Realese_Click(sender As Object, e As EventArgs) Handles Btn_Realese.Click
		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If fStatus = "Transaksi_ForecastOrder_PPIC" Then
				If CekButtonRole("Realease_Forecast_PPIC") = "T" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("anda tidak memiliki akses ! !")
					Exit Sub
				End If

			ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
				If CekButtonRole("Realease_Forecast_Sales") = "T" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("anda tidak memiliki akses ! !")
					Exit Sub
				End If
			End If

			Dim a As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
			Dim fthn As Integer = Val(Cmb_Tahun.Text)
			Dim b As String = ""

			Dim daftarProductionPlantDiRelease As String = ""
			For i As Integer = 0 To DataGridView1.Rows.Count - 1
				Dim isChecked As Boolean = Convert.ToBoolean(DataGridView1.Rows(i).Cells(CellChkBox).Value)

				If isChecked Then
					' Ambil nilai kode barang
					Dim kodeBarang As String = DataGridView1.Rows(i).Cells(CellKdBrg).Value.ToString()

					' Gabungkan dengan format tanda petik untuk SQL
					If daftarProductionPlantDiRelease = "" Then
						' Hasil: 'BRG2410001'
						daftarProductionPlantDiRelease = "'" & kodeBarang & "'"
					Else
						' Hasil: 'BRG2410001','BRG2410002'
						daftarProductionPlantDiRelease &= ",'" & kodeBarang & "'"
					End If
				End If
			Next

			If daftarProductionPlantDiRelease.Trim.Length = 0 Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Silahkan pilih data barang yang ingin di release.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'--- 1
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If

			For i As Integer = 1 To 2

				If a = 12 Then
					a = 1
					fthn = fthn + 1
				Else
					a = a + 1
				End If

				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						b = arrBulanMM.Item(index)

						Exit For
					End If
				Next

				'Cek jika data terakhir maka harus di cek apakah sudah pernah di release apa belum .
				'jika belum maka boleh di release

				Dim no_faktur As String = ""
				SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
				SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						no_faktur = dr("no_faktur")
					Else

						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Production Plan tidak ditemukan...", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
				SQL = SQL & "Tanggal,Jam) "
				SQL = SQL & " select a.Kode_Perusahaan, a.No_Faktur,a.urut,a.Nilai_Sales,a.Nilai_Sales,'RELEASE', '" & UserID & "' "
				SQL = SQL & ", '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
				SQL = SQL & "From  EMI_Transaksi_Sales_Forecasting_Detail a "
				SQL = SQL & "inner join	EMI_Transaksi_Sales_Forecasting b on a.Kode_Perusahaan  =b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
				SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Bulan = '" & b & "' and b.tahun = '" & fthn & "' and status is null "
				SQL = SQL & "and a.Kode_Barang in (" & daftarProductionPlantDiRelease & ") "
				ExecuteTrans(SQL)

				'dari sini

				If fStatus = "Transaksi_ForecastOrder_PPIC" Then

					If i = 2 Then

						SQL = "select flag_validasi_PPIC,flag_validasi,b.nama  From  EMI_Transaksi_Sales_Forecasting_Detail a, barang b where "
						SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
						SQL = SQL & "a.kode_barang = b.kode_barang and a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner "
						SQL = SQL & "and a.bulan='" & b & "' and a.tahun ='" & fthn & "' "
						SQL = SQL & "and a.kode_barang in (" & daftarProductionPlantDiRelease & ")"

						Using Ds = BindingTrans(SQL)
							With Ds.Tables("MyTable")
								If .Rows.Count <> 0 Then
									For indexCekin As Integer = 0 To .Rows.Count - 1

										If General_Class.CekNULL(.Rows(indexCekin).Item("flag_validasi")) = "" Then

											CloseTrans()
											CloseConn()
											MessageBox.Show("Gagal Release untuk barang " & .Rows(indexCekin).Item("nama") & ". barang belum di release oleh marketing.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If

										If General_Class.CekNULL(.Rows(indexCekin).Item("flag_validasi_PPIC")) = "Y" Then

											CloseTrans()
											CloseConn()
											MessageBox.Show("Production Plan sudah pernah di release untuk barang " & .Rows(indexCekin).Item("nama"), Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									Next
								Else

									CloseTrans()
									CloseConn()
									MessageBox.Show("Production Plan sudah pernah di release", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End With
						End Using

						'Using Dr = OpenTrans(SQL)
						'    Do While Dr.Read
						'        If General_Class.CekNULL(Dr("flag_validasi")) = "" Then
						'            Dr.Close()
						'            CloseTrans()
						'            CloseConn()
						'            MessageBox.Show("Gagal Release untuk barang " &   , Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'            Exit Sub
						'        End If

						'        If General_Class.CekNULL(Dr("flag_validasi_PPIC")) = "Y" Then
						'            Dr.Close()
						'            CloseTrans()
						'            CloseConn()
						'            MessageBox.Show("Production Plan sudah pernah di release", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'            Exit Sub
						'        End If
						'    Loop

						'End Using

					End If

					SQL = "update EMI_Transaksi_Sales_Forecasting_Detail set "
					SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
					SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
					SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
					SQL = SQL & "Status_Data = 'VERIFIED' "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & " bulan='" & b & "' and tahun ='" & fthn & "' "
					SQL = SQL & "and kode_barang in (" & daftarProductionPlantDiRelease & ")"
					ExecuteTrans(SQL)

					SQL = "Select b.kode_barang_inq as Kode_Barang, a.urut,b.nama from EMI_Transaksi_Sales_Forecasting_detail a, barang b "
					SQL = SQL & "where a.no_faktur ='" & no_faktur & "' "
					SQL = SQL & "And a.kode_perusahaan='" & KodePerusahaan & "' "
					SQL = SQL & "and a.kode_Barang=b.Kode_Barang and a.Kode_stock_owner=b.Kode_stock_owner "
					SQL = SQL & "and a.kode_Perusahaan=b.kode_Perusahaan "
					SQL = SQL & "and a.kode_barang in (" & daftarProductionPlantDiRelease & ")"
					Using ds = BindingTrans(SQL)
						With ds.Tables("MyTable")
							For index = 0 To .Rows.Count - 1

								Dim kode_formula As String = ""
								Dim namaBarang As String = .Rows(index).Item("nama")
								SQL = "select top(1) c.No_Faktur
										from N_EMI_Transaksi_Formulator_Binding a
										inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
										inner join Emi_Transaksi_Formulator c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Formulator = c.No_Faktur and c.Status is null
										where a.Status is NULL and a.Flag_Validasi_Main = 'Y' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Barang ='" & .Rows(index).Item("Kode_Barang") & "'
										order by a.Tanggal DESC, a.Jam DESC, b.No_Prioritas ASC

									"

								Using dr = OpenTrans(SQL)
									If dr.Read Then
										kode_formula = dr("No_Faktur")
									Else
										dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show(namaBarang & " belum di binding ke formula. silahkan binding terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								End Using

								SQL = "update EMI_Transaksi_Sales_Forecasting_detail set "
								SQL = SQL & "Kode_Formula = '" & kode_formula & "' "
								SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
								SQL = SQL & "urut='" & .Rows(index).Item("urut") & "' "
								ExecuteTrans(SQL)

							Next
						End With
					End Using

					' insert barang semi finishgood

				ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
					If i = 2 Then

						SQL = "select flag_validasi_PPIC,flag_validasi,b.nama  From  EMI_Transaksi_Sales_Forecasting_Detail a, barang b where "
						SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
						SQL = SQL & "a.kode_barang = b.kode_barang and a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner "
						SQL = SQL & "and bulan='" & b & "' and tahun ='" & fthn & "' "
						SQL = SQL & "and a.kode_barang in (" & daftarProductionPlantDiRelease & ")"

						Using Ds = BindingTrans(SQL)
							With Ds.Tables("MyTable")
								If .Rows.Count <> 0 Then
									For indexCekin As Integer = 0 To .Rows.Count - 1

										If General_Class.CekNULL(.Rows(indexCekin).Item("flag_validasi_PPIC")) = "Y" Then

											CloseTrans()
											CloseConn()
											MessageBox.Show("Production Plan sudah pernah di release untuk barang " & .Rows(indexCekin).Item("nama"), Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									Next
								Else

									CloseTrans()
									CloseConn()
									MessageBox.Show("Production Plan sudah pernah di release", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End With
						End Using

						SQL = "select flag_validasi From  EMI_Transaksi_Sales_Forecasting_Detail where "
						SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
						SQL = SQL & "and bulan='" & b & "' and tahun ='" & fthn & "' "
						SQL = SQL & "and kode_barang in (" & daftarProductionPlantDiRelease & ")"
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								If General_Class.CekNULL(Dr("flag_validasi")) = "Y" Then
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Production Plan sudah pernah di release", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							Else
								Dr.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("Production Plan tidak ditemukan...", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End Using

					End If

					SQL = "update EMI_Transaksi_Sales_Forecasting_Detail set "
					SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
					SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
					SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
					SQL = SQL & "Status_Data = 'SUBMITTED' "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
					SQL = SQL & "and kode_barang in (" & daftarProductionPlantDiRelease & ")"
					ExecuteTrans(SQL)
				End If

			Next

			If fStatus = "Transaksi_ForecastOrder_PPIC" Then
				a = arrBulan.Item(Cmb_Bulan.SelectedIndex)
				fthn = Val(Cmb_Tahun.Text)
				b = ""

				Dim listFilter As New List(Of String)
				Dim listFilterDelete As New List(Of String)

				For i As Integer = 0 To 2

					If a = 12 Then
						a = 1
						fthn += 1
					Else
						If i <> 0 Then a += 1
					End If

					For index = 0 To arrBulan.Count - 1
						If arrBulan.Item(index) = a Then
							b = arrBulanMM.Item(index)

							' 🔥 penting: tanpa tanda kutip kalau INT
							listFilter.Add("(a.bulan = " & CInt(b) & " AND a.tahun = " & fthn & ")")

							Exit For
						End If
					Next
				Next

				Dim filterBulanTahun As String = String.Join(" OR ", listFilter)

				RefreshForecastingSemiFG(
					KodePerusahaan,
					filterBulanTahun,
					"Release Insert",
					UserID
					)

				'SQL = "DELETE FROM EMI_Transaksi_Sales_Forecasting_Detail "
				'SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
				'SQL = SQL & "AND (" & filterBulanTahundelete & ") "
				'SQL = SQL & "AND flag_semi_fg = 'Y' "

				'ExecuteTrans(SQL)

				'' ================== TEMP TABLE ==================
				'SQL = "DECLARE @Inserted TABLE ("
				'SQL = SQL & " Urut_Detail INT,"
				'SQL = SQL & " No_Faktur VARCHAR(50),"
				'SQL = SQL & " Nilai_PPIC float,"
				'SQL = SQL & " Nilai_Sales float"

				'SQL = SQL & ") "

				'' ================== CTE ==================
				'SQL = SQL & " ;WITH cte AS ( "
				'SQL = SQL & " SELECT DISTINCT "
				'SQL = SQL & " ISNULL(( "
				'SQL = SQL & " SELECT TOP 1 c.No_Faktur "
				'SQL = SQL & " FROM N_EMI_Transaksi_Formulator_Binding a "
				'SQL = SQL & " INNER JOIN N_EMI_Transaksi_Formulator_Binding_Detail b "
				'SQL = SQL & " ON a.Kode_Perusahaan=b.Kode_Perusahaan AND a.No_Faktur=b.No_Faktur "
				'SQL = SQL & " INNER JOIN Emi_Transaksi_Formulator c "
				'SQL = SQL & " ON b.Kode_Perusahaan=c.Kode_Perusahaan AND b.No_Formulator=c.No_Faktur AND c.Status IS NULL "
				'SQL = SQL & " WHERE a.Status IS NULL "
				'SQL = SQL & " AND a.Flag_Validasi_Main='Y' "
				'SQL = SQL & " AND a.Kode_Perusahaan=x.kode_perusahaan "
				'SQL = SQL & " AND a.Kode_Barang=x.kode_barang_inq "
				'SQL = SQL & " ORDER BY a.Tanggal DESC,a.Jam DESC,b.No_Prioritas ASC "
				'SQL = SQL & " ),'') AS kode_formula, x.kode_barang_inq,x.kode_perusahaan "
				'SQL = SQL & " FROM barang x "
				'SQL = SQL & " INNER JOIN emI_group_jenis y ON x.kode_perusahaan=y.kode_perusahaan AND x.id_group_jenis=y.id_group_jenis "
				'SQL = SQL & " WHERE (y.Flag_Finished_Good='Y' OR y.Flag_Semi_FG='Y') "
				'SQL = SQL & "), "

				'' ================== CTE_B ==================
				'SQL = SQL & "cte_b AS ( "

				'' ===== SELECT 1 =====
				'SQL = SQL & " SELECT a.Kode_Perusahaan as Kp,a.No_Faktur,b.Kode_Stock_Owner,a.bulan,a.tahun,"
				'SQL = SQL & " b.kode_Barang as kd_fg,d.kode_barang,d.Nilai_Barang,d.satuan_barang,"
				'SQL = SQL & " b.Kode_Formula,"
				'SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic-total_qty) AS nilai_ppic,"
				'SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil) AS nilai_Formula,"
				'SQL = SQL & " z.Kode_Perusahaan,z.id_group_jenis "

				'SQL = SQL & " FROM emi_transaksi_sales_forecasting a "
				'SQL = SQL & " INNER JOIN emi_transaksi_sales_forecasting_detail b ON a.Kode_Perusahaan=b.kode_Perusahaan AND a.no_faktur=b.no_faktur "
				'SQL = SQL & " INNER JOIN barang bb ON b.kode_perusahaan=bb.kode_perusahaan AND b.kode_barang=bb.kode_barang AND b.kode_stock_owner=bb.kode_stock_owner "
				'SQL = SQL & " INNER JOIN cte bc ON bb.kode_perusahaan=bc.kode_perusahaan AND bb.kode_barang_inq=bc.kode_barang_inq "
				'SQL = SQL & " INNER JOIN Emi_Transaksi_Formulator c ON c.Kode_Perusahaan=bc.Kode_Perusahaan AND c.No_Faktur=bc.Kode_Formula "
				'SQL = SQL & " INNER JOIN emi_transaksi_formulator_detail_Bahan d ON c.Kode_Perusahaan=d.Kode_Perusahaan AND c.no_faktur=d.no_faktur "
				'SQL = SQL & " INNER JOIN barang z ON d.Kode_Perusahaan=z.Kode_Perusahaan AND d.Kode_Barang=z.Kode_Barang AND d.Kode_Stock_Owner=z.Kode_Stock_Owner "
				'SQL = SQL & " INNER JOIN init e ON a.kode_Perusahaan=e.kode_Perusahaan "

				'SQL = SQL & " OUTER APPLY (SELECT ISNULL(SUM(f.Jumlah),0) total_qty FROM N_EMI_Production_Plan_Schedule_Detail f "
				'SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule g ON f.No_Transaksi=g.No_Transaksi AND f.Kode_Perusahaan=g.Kode_Perusahaan "
				'SQL = SQL & " WHERE g.Status IS NULL AND f.Kode_Perusahaan=b.Kode_Perusahaan AND f.Urut_Production_Plan=b.urut) f_sum "

				'SQL = SQL & " WHERE a.status IS NULL "
				'SQL = SQL & " AND b.Kode_Perusahaan='" & KodePerusahaan & "' "
				'SQL = SQL & " AND (" & filterBulanTahun & ") "
				'SQL = SQL & " AND b.flag_validasi='Y' AND b.flag_validasi_PPIC='Y' AND c.status IS NULL "

				'SQL = SQL & " UNION ALL "

				'' ===== SELECT 2 =====
				'SQL = SQL & " SELECT a.Kode_Perusahaan,a.No_Faktur,b.Kode_Stock_Owner,a.bulan,a.tahun,"
				'SQL = SQL & "  b.kode_Barang as kd_fg,d.kode_barang,d.Nilai_Barang,d.satuan_barang,"
				'SQL = SQL & " f.Kode_Formula,"
				'SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,f.Jumlah) AS nilai_ppic,"
				'SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil) AS nilai_Formula,"
				'SQL = SQL & " z.Kode_Perusahaan,z.id_group_jenis "

				'SQL = SQL & " FROM emi_transaksi_sales_forecasting a "
				'SQL = SQL & " INNER JOIN emi_transaksi_sales_forecasting_detail b ON a.Kode_Perusahaan=b.kode_Perusahaan AND a.no_faktur=b.no_faktur "
				'SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule_Detail f ON f.Kode_Perusahaan=b.Kode_Perusahaan AND f.Urut_Production_Plan=b.urut "
				'SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule g ON f.Kode_Perusahaan=g.Kode_Perusahaan AND f.No_Transaksi=g.No_Transaksi "
				'SQL = SQL & " INNER JOIN barang bb ON f.kode_perusahaan=bb.kode_perusahaan AND f.kode_barang=bb.kode_barang AND f.kode_stock_owner=bb.kode_stock_owner "
				'SQL = SQL & " INNER JOIN cte bc ON bb.kode_perusahaan=bc.kode_perusahaan AND bb.kode_barang_inq=bc.kode_barang_inq "
				'SQL = SQL & " INNER JOIN emi_transaksi_formulator c ON f.Kode_Perusahaan=c.Kode_Perusahaan AND f.Kode_Formula=c.No_Faktur "
				'SQL = SQL & " INNER JOIN emi_transaksi_formulator_detail_Bahan d ON c.Kode_Perusahaan=d.Kode_Perusahaan AND c.no_faktur=d.no_faktur "
				'SQL = SQL & " INNER JOIN barang z ON d.Kode_Perusahaan=z.Kode_Perusahaan AND d.Kode_Barang=z.Kode_Barang AND d.Kode_Stock_Owner=z.Kode_Stock_Owner "
				'SQL = SQL & " INNER JOIN init e ON a.kode_Perusahaan=e.kode_Perusahaan "

				'SQL = SQL & " WHERE a.status IS NULL "
				'SQL = SQL & " AND b.Kode_Perusahaan='" & KodePerusahaan & "' "
				'SQL = SQL & " AND (" & filterBulanTahun & ") "
				'SQL = SQL & " AND b.flag_validasi='Y' AND b.flag_validasi_PPIC='Y' AND c.status IS NULL AND g.Status IS NULL "

				'SQL = SQL & ") "

				'' ================== INSERT + OUTPUT ==================
				'SQL = SQL & "INSERT INTO EMI_Transaksi_Sales_Forecasting_Detail "
				'SQL = SQL & "(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Bulan,Tahun,Nilai_Sales,Nilai_PPIC,Flag_Validasi,Flag_Validasi_PPIC, flag_semi_fg) "

				'SQL = SQL & "OUTPUT INSERTED.urut, INSERTED.No_Faktur, INSERTED.Nilai_PPIC, INSERTED.Nilai_Sales  INTO @Inserted "

				'SQL = SQL & "SELECT a.kp,a.no_faktur,a.Kode_Stock_Owner,a.kode_barang,a.bulan,a.tahun,"
				'SQL = SQL & " ISNULL(SUM(ROUND(a.Nilai_Barang*(a.nilai_ppic/NULLIF(a.nilai_Formula,0)),2)),0),"
				'SQL = SQL & " ISNULL(SUM(ROUND(a.Nilai_Barang*(a.nilai_ppic/NULLIF(a.nilai_Formula,0)),2)),0),'Y','Y', 'Y' "

				'SQL = SQL & "FROM cte_b a "
				'SQL = SQL & "INNER JOIN emi_group_jenis m ON a.kode_perusahaan=m.kode_perusahaan AND a.id_group_jenis=m.id_group_jenis "
				'SQL = SQL & "WHERE m.flag_semi_fg='Y' "
				'SQL = SQL & "GROUP BY a.kp,a.no_faktur,a.Kode_Stock_Owner,a.kode_barang,a.bulan,a.tahun,a.kode_perusahaan "

				'' ================== INSERT LOG ==================
				'SQL = SQL & "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log "
				'SQL = SQL & "(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,Tanggal,Jam) "

				'SQL = SQL & "SELECT '" & KodePerusahaan & "',No_Faktur,Urut_Detail,Nilai_PPIC,Nilai_Sales,'INSERT','" & UserID & "',"
				'SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "' FROM @Inserted "

				'ExecuteTrans(SQL)

			End If

			'Dim no_faktur As String = ""
			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Production Plan tidak ditemukan...", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			''dari sini

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'VERIFICATION' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'    SQL = "Select b.kode_barang_inq as Kode_Barang, a.urut from EMI_Transaksi_Sales_Forecasting_detail a, barang b "
			'    SQL = SQL & "where a.no_faktur ='" & no_faktur & "' "
			'    SQL = SQL & "And a.kode_perusahaan='" & KodePerusahaan & "' "
			'    SQL = SQL & "and a.kode_Barang=b.Kode_Barang and a.Kode_stock_owner=b.Kode_stock_owner "
			'    SQL = SQL & "and a.kode_Perusahaan=b.kode_Perusahaan "
			'    Using ds = BindingTrans(SQL)
			'        With ds.Tables("MyTable")
			'            For index = 0 To .Rows.Count - 1

			'                Dim kode_formula As String = ""
			'                SQL = "Select Kode_formula from EMI_Transaksi_Formulator_Binding where "
			'                SQL = SQL & "kode_Barang='" & .Rows(index).Item("Kode_Barang") & "' and Aktif='Y' "
			'                SQL = SQL & " And kode_perusahaan ='" & KodePerusahaan & "' "
			'                Using dr = OpenTrans(SQL)
			'                    If dr.Read Then
			'                        kode_formula = dr("Kode_formula")
			'                    Else
			'                        dr.Close()
			'                        CloseConn()
			'                        MessageBox.Show("Barang tidak di temukan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "update EMI_Transaksi_Sales_Forecasting_detail set "
			'                SQL = SQL & "Kode_Formula = '" & kode_formula & "' "
			'                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'                SQL = SQL & "urut='" & .Rows(index).Item("urut") & "' "
			'                ExecuteTrans(SQL)

			'            Next
			'        End With
			'    End Using

			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'SUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			''--- 2
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If
			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)
			'    End If
			'Next

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'VERIFICATION' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'    SQL = "Select b.kode_barang_inq as Kode_Barang, a.urut from EMI_Transaksi_Sales_Forecasting_detail a, barang b "
			'    SQL = SQL & "where a.no_faktur ='" & no_faktur & "' "
			'    SQL = SQL & "And a.kode_perusahaan='" & KodePerusahaan & "' "
			'    SQL = SQL & "and a.kode_Barang=b.Kode_Barang and a.Kode_stock_owner=b.Kode_stock_owner "
			'    SQL = SQL & "and a.kode_Perusahaan=b.kode_Perusahaan "
			'    Using ds = BindingTrans(SQL)
			'        With ds.Tables("MyTable")
			'            For index = 0 To .Rows.Count - 1

			'                Dim kode_formula As String = ""
			'                SQL = "Select Kode_formula from EMI_Transaksi_Formulator_Binding where "
			'                SQL = SQL & "kode_Barang='" & .Rows(index).Item("Kode_Barang") & "' and Aktif='Y' "
			'                SQL = SQL & " And kode_perusahaan ='" & KodePerusahaan & "' "
			'                Using dr = OpenTrans(SQL)
			'                    If dr.Read Then
			'                        kode_formula = dr("Kode_formula")
			'                    Else
			'                        dr.Close()
			'                        CloseConn()
			'                        MessageBox.Show("Barang tidak di temukan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "update EMI_Transaksi_Sales_Forecasting_detail set "
			'                SQL = SQL & "Kode_Formula = '" & kode_formula & "' "
			'                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'                SQL = SQL & "urut='" & .Rows(index).Item("urut") & "' "
			'                ExecuteTrans(SQL)

			'            Next
			'        End With
			'    End Using
			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'SUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			'--- 3
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If
			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)
			'    End If
			'Next

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'VERIFICATION' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'    SQL = "Select b.kode_barang_inq as Kode_Barang, a.urut from EMI_Transaksi_Sales_Forecasting_detail a, barang b "
			'    SQL = SQL & "where a.no_faktur ='" & no_faktur & "' "
			'    SQL = SQL & "And a.kode_perusahaan='" & KodePerusahaan & "' "
			'    SQL = SQL & "and a.kode_Barang=b.Kode_Barang and a.Kode_stock_owner=b.Kode_stock_owner "
			'    SQL = SQL & "and a.kode_Perusahaan=b.kode_Perusahaan "
			'    Using ds = BindingTrans(SQL)
			'        With ds.Tables("MyTable")
			'            For index = 0 To .Rows.Count - 1

			'                Dim kode_formula As String = ""
			'                SQL = "Select Kode_formula from EMI_Transaksi_Formulator_Binding where "
			'                SQL = SQL & "kode_Barang='" & .Rows(index).Item("Kode_Barang") & "' and Aktif='Y' "
			'                SQL = SQL & " And kode_perusahaan ='" & KodePerusahaan & "' "
			'                Using dr = OpenTrans(SQL)
			'                    If dr.Read Then
			'                        kode_formula = dr("Kode_formula")
			'                    Else
			'                        dr.Close()
			'                        CloseConn()
			'                        MessageBox.Show("Barang tidak di temukan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "update EMI_Transaksi_Sales_Forecasting_detail set "
			'                SQL = SQL & "Kode_Formula = '" & kode_formula & "' "
			'                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'                SQL = SQL & "urut='" & .Rows(index).Item("urut") & "' "
			'                ExecuteTrans(SQL)

			'            Next
			'        End With
			'    End Using
			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'SUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			''--- 4
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If
			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)
			'    End If
			'Next

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'VERIFICATION' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'    SQL = "Select b.kode_barang_inq as Kode_Barang, a.urut from EMI_Transaksi_Sales_Forecasting_detail a, barang b "
			'    SQL = SQL & "where a.no_faktur ='" & no_faktur & "' "
			'    SQL = SQL & "And a.kode_perusahaan='" & KodePerusahaan & "' "
			'    SQL = SQL & "and a.kode_Barang=b.Kode_Barang and a.Kode_stock_owner=b.Kode_stock_owner "
			'    SQL = SQL & "and a.kode_Perusahaan=b.kode_Perusahaan "
			'    Using ds = BindingTrans(SQL)
			'        With ds.Tables("MyTable")
			'            For index = 0 To .Rows.Count - 1

			'                Dim kode_formula As String = ""
			'                SQL = "Select Kode_formula from EMI_Transaksi_Formulator_Binding where "
			'                SQL = SQL & "kode_Barang='" & .Rows(index).Item("Kode_Barang") & "' and Aktif='Y' "
			'                SQL = SQL & " And kode_perusahaan ='" & KodePerusahaan & "' "
			'                Using dr = OpenTrans(SQL)
			'                    If dr.Read Then
			'                        kode_formula = dr("Kode_formula")
			'                    Else
			'                        dr.Close()
			'                        CloseConn()
			'                        MessageBox.Show("Barang tidak di temukan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "update EMI_Transaksi_Sales_Forecasting_detail set "
			'                SQL = SQL & "Kode_Formula = '" & kode_formula & "' "
			'                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'                SQL = SQL & "urut='" & .Rows(index).Item("urut") & "' "
			'                ExecuteTrans(SQL)

			'            Next
			'        End With
			'    End Using
			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'SUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			''--- 5
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If
			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)
			'    End If
			'Next

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'VERIFICATION' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'    SQL = "Select b.kode_barang_inq as Kode_Barang, a.urut from EMI_Transaksi_Sales_Forecasting_detail a, barang b "
			'    SQL = SQL & "where a.no_faktur ='" & no_faktur & "' "
			'    SQL = SQL & "And a.kode_perusahaan='" & KodePerusahaan & "' "
			'    SQL = SQL & "and a.kode_Barang=b.Kode_Barang and a.Kode_stock_owner=b.Kode_stock_owner "
			'    SQL = SQL & "and a.kode_Perusahaan=b.kode_Perusahaan "
			'    Using ds = BindingTrans(SQL)
			'        With ds.Tables("MyTable")
			'            For index = 0 To .Rows.Count - 1

			'                Dim kode_formula As String = ""
			'                SQL = "Select Kode_formula from EMI_Transaksi_Formulator_Binding where "
			'                SQL = SQL & "kode_Barang='" & .Rows(index).Item("Kode_Barang") & "' and Aktif='Y' "
			'                SQL = SQL & " And kode_perusahaan ='" & KodePerusahaan & "' "
			'                Using dr = OpenTrans(SQL)
			'                    If dr.Read Then
			'                        kode_formula = dr("Kode_formula")
			'                    Else
			'                        dr.Close()
			'                        CloseConn()
			'                        MessageBox.Show("Barang tidak di temukan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "update EMI_Transaksi_Sales_Forecasting_detail set "
			'                SQL = SQL & "Kode_Formula = '" & kode_formula & "' "
			'                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'                SQL = SQL & "urut='" & .Rows(index).Item("urut") & "' "
			'                ExecuteTrans(SQL)

			'            Next
			'        End With
			'    End Using
			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'SUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			''--- 6
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If
			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)
			'    End If
			'Next

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'VERIFICATION' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'    SQL = "Select b.kode_barang_inq as Kode_Barang, a.urut from EMI_Transaksi_Sales_Forecasting_detail a, barang b "
			'    SQL = SQL & "where a.no_faktur ='" & no_faktur & "' "
			'    SQL = SQL & "And a.kode_perusahaan='" & KodePerusahaan & "' "
			'    SQL = SQL & "and a.kode_Barang=b.Kode_Barang and a.Kode_stock_owner=b.Kode_stock_owner "
			'    SQL = SQL & "and a.kode_Perusahaan=b.kode_Perusahaan "
			'    Using ds = BindingTrans(SQL)
			'        With ds.Tables("MyTable")
			'            For index = 0 To .Rows.Count - 1

			'                Dim kode_formula As String = ""
			'                SQL = "Select Kode_formula from EMI_Transaksi_Formulator_Binding where "
			'                SQL = SQL & "kode_Barang='" & .Rows(index).Item("Kode_Barang") & "' and Aktif='Y' "
			'                SQL = SQL & " And kode_perusahaan ='" & KodePerusahaan & "' "
			'                Using dr = OpenTrans(SQL)
			'                    If dr.Read Then
			'                        kode_formula = dr("Kode_formula")
			'                    Else
			'                        dr.Close()
			'                        CloseConn()
			'                        MessageBox.Show("Barang tidak di temukan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "update EMI_Transaksi_Sales_Forecasting_detail set "
			'                SQL = SQL & "Kode_Formula = '" & kode_formula & "' "
			'                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'                SQL = SQL & "urut='" & .Rows(index).Item("urut") & "' "
			'                ExecuteTrans(SQL)

			'            Next
			'        End With
			'    End Using

			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
			'    SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
			'    SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
			'    SQL = SQL & "Status_Data = 'SUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show("Data berhasil divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		kosong("REFRESH")
	End Sub

	Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
		kosong()
	End Sub

	Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
		Get_Isi_Listview(DataGridView1.CurrentRow.Index)

		arrCellInputSales.AddRange(New Object() {
						CellSalesForecastBln1, CellSalesForecastBln2, CellSalesForecastBln3, CellSalesForecastBln4,
						CellSalesForecastBln5, CellSalesForecastBln6
		})

		arrCellInputPPIC.AddRange(New Object() {
						CellPPICForecastBln1, CellPPICForecastBln2, CellPPICForecastBln3, CellPPICForecastBln4,
						CellPPICForecastBln5, CellPPICForecastBln6
		})

		If IsNumeric(lvSalesForecastBln1) = False Or Val(lvSalesForecastBln1) < 0 Then
			DataGridView1.CurrentRow.Cells(CellSalesForecastBln1).Value = 0
		ElseIf IsNumeric(lvPPICForecastBln1) = False Or Val(lvPPICForecastBln1) < 0 Then
			DataGridView1.CurrentRow.Cells(CellPPICForecastBln1).Value = 0
		ElseIf IsNumeric(lvSalesForecastBln2) = False Or Val(lvSalesForecastBln2) < 0 Then
			DataGridView1.CurrentRow.Cells(CellSalesForecastBln2).Value = 0
		ElseIf IsNumeric(lvPPICForecastBln2) = False Or Val(lvPPICForecastBln2) < 0 Then
			DataGridView1.CurrentRow.Cells(CellPPICForecastBln2).Value = 0
		ElseIf IsNumeric(lvSalesForecastBln3) = False Or Val(lvSalesForecastBln3) < 0 Then
			DataGridView1.CurrentRow.Cells(CellSalesForecastBln3).Value = 0
		ElseIf IsNumeric(lvPPICForecastBln3) = False Or Val(lvPPICForecastBln3) < 0 Then
			DataGridView1.CurrentRow.Cells(CellPPICForecastBln3).Value = 0
		ElseIf IsNumeric(lvSalesForecastBln4) = False Or Val(lvSalesForecastBln4) < 0 Then
			DataGridView1.CurrentRow.Cells(CellSalesForecastBln4).Value = 0
		ElseIf IsNumeric(lvPPICForecastBln4) = False Or Val(lvPPICForecastBln4) < 0 Then
			DataGridView1.CurrentRow.Cells(CellPPICForecastBln4).Value = 0
		ElseIf IsNumeric(lvSalesForecastBln5) = False Or Val(lvSalesForecastBln5) < 0 Then
			DataGridView1.CurrentRow.Cells(CellSalesForecastBln5).Value = 0
		ElseIf IsNumeric(lvPPICForecastBln5) = False Or Val(lvPPICForecastBln5) < 0 Then
			DataGridView1.CurrentRow.Cells(CellPPICForecastBln5).Value = 0
		ElseIf IsNumeric(lvSalesForecastBln6) = False Or Val(lvSalesForecastBln6) < 0 Then
			DataGridView1.CurrentRow.Cells(CellSalesForecastBln6).Value = 0
		ElseIf IsNumeric(lvPPICForecastBln6) = False Or Val(lvPPICForecastBln6) < 0 Then
			DataGridView1.CurrentRow.Cells(CellPPICForecastBln6).Value = 0
		End If

		'======================
		'=     SET FORMAT     =
		'======================
		If arrCellInputSales.Contains(DataGridView1.CurrentCell.ColumnIndex) Or arrCellInputPPIC.Contains(DataGridView1.CurrentCell.ColumnIndex) Then

			Dim cellKuantity As String = DataGridView1.CurrentCell.Value

			If cellKuantity.Contains(",") Then
				MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				DataGridView1.CurrentCell.Value = 0
				Exit Sub
			End If

			Dim nilai As Decimal = Decimal.Parse(cellKuantity)
			Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

			DataGridView1.CurrentCell.Value = formattedValue
		End If

	End Sub

	Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter

		arrCellInputSales.AddRange(New Object() {
						CellSalesForecastBln1, CellSalesForecastBln2, CellSalesForecastBln3, CellSalesForecastBln4,
						CellSalesForecastBln5, CellSalesForecastBln6
		})

		arrCellInputPPIC.AddRange(New Object() {
						CellPPICForecastBln1, CellPPICForecastBln2, CellPPICForecastBln3, CellPPICForecastBln4,
						CellPPICForecastBln5, CellPPICForecastBln6
		})

		If arrCellInputSales.Contains(DataGridView1.CurrentCell.ColumnIndex) Or arrCellInputPPIC.Contains(DataGridView1.CurrentCell.ColumnIndex) Then
			Dim cellKuantity As String = DataGridView1.CurrentCell.Value

			If cellKuantity = "" Then
				Exit Sub
			End If

			Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
			Dim nilai As Decimal = Decimal.Parse(cleanedStr)

			DataGridView1.CurrentCell.Value = nilai
		End If

	End Sub

	Private Sub DataGridView1_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellLeave

		arrCellInputSales.AddRange(New Object() {
						CellSalesForecastBln1, CellSalesForecastBln2, CellSalesForecastBln3, CellSalesForecastBln4,
						CellSalesForecastBln5, CellSalesForecastBln6
		})

		arrCellInputPPIC.AddRange(New Object() {
						CellPPICForecastBln1, CellPPICForecastBln2, CellPPICForecastBln3, CellPPICForecastBln4,
						CellPPICForecastBln5, CellPPICForecastBln6
		})

		If arrCellInputSales.Contains(DataGridView1.CurrentCell.ColumnIndex) Or arrCellInputPPIC.Contains(DataGridView1.CurrentCell.ColumnIndex) Then
			Dim cellKuantity As String = DataGridView1.CurrentCell.Value

			If cellKuantity = "" Then
				Exit Sub
			End If

			'Dim nilai As Decimal = Decimal.Parse(cellKuantity)
			'Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

			DataGridView1.CurrentCell.Value = Format(Val(HilangkanTanda(cellKuantity)), "N2")

		End If
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Btn_Unrealese.Click
		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If fStatus = "Transaksi_ForecastOrder_PPIC" Then
				If CekButtonRole("Unrealease_Forecast_PPIC") = "T" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("anda tidak memiliki akses ! !")
					Exit Sub
				End If

			ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
				If CekButtonRole("Unrealease_Forecast_Sales") = "T" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("anda tidak memiliki akses ! !")
					Exit Sub
				End If
			End If

			Dim a As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
			Dim fthn As Integer = Val(Cmb_Tahun.Text)
			Dim b As String = ""

			Dim daftarProductionPlantDiRelease As String = ""
			For i As Integer = 0 To DataGridView1.Rows.Count - 1
				Dim isChecked As Boolean = Convert.ToBoolean(DataGridView1.Rows(i).Cells(CellChkBox).Value)

				If isChecked Then
					' Ambil nilai kode barang
					Dim kodeBarang As String = DataGridView1.Rows(i).Cells(CellKdBrg).Value.ToString()

					' Gabungkan dengan format tanda petik untuk SQL
					If daftarProductionPlantDiRelease = "" Then
						' Hasil: 'BRG2410001'
						daftarProductionPlantDiRelease = "'" & kodeBarang & "'"
					Else
						' Hasil: 'BRG2410001','BRG2410002'
						daftarProductionPlantDiRelease &= ",'" & kodeBarang & "'"
					End If
				End If
			Next

			If daftarProductionPlantDiRelease.Trim.Length = 0 Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Silahkan pilih data barang yang ingin di release.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			For i As Integer = 1 To 2

				If a = 12 Then
					a = 1
					fthn = fthn + 1
				Else
					a = a + 1
				End If

				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						b = arrBulanMM.Item(index)

					End If
				Next
				Dim no_faktur As String = ""
				SQL = "Select a.no_faktur,a.flag_validasi,a.flag_validasi_ppic from EMI_Transaksi_Sales_Forecasting_detail a, EMI_Transaksi_Sales_Forecasting b where b.bulan='" & b & "' and b.tahun ='" & fthn & "' "
				SQL = SQL & "And b.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' "
				SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur "
				SQL = SQL & "and a.Kode_Barang in (" & daftarProductionPlantDiRelease & ") "
				Using dr = OpenTrans(SQL)
					If dr.Read Then

						If fStatus = "Transaksi_ForecastOrder_Sales" Then
							If General_Class.CekNULL(dr("flag_validasi")) = "" Then
								dr.Close()
								CloseTrans()

								CloseConn()
								MessageBox.Show("Data belum pernah di submit!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							If General_Class.CekNULL(dr("flag_validasi_ppic")) = "Y" Then
								dr.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("Data yang telah di realease PPIC Tidak bisa di Batalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End If

						If fStatus = "Transaksi_ForecastOrder_PPIC" Then
							If General_Class.CekNULL(dr("flag_validasi_ppic")) = "" Then
								dr.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("Data belum pernah di submit!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End If

						no_faktur = dr("no_faktur")
					Else

						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = "select a.Kode_Perusahaan,  "

				SQL = SQL & "isnull((select top(1) 'Y' from N_EMI_Production_Plan_Schedule_Detail x, N_EMI_Production_Plan_Schedule y "
				SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Transaksi = y.No_Transaksi and y.Status is null "
				SQL = SQL & "), 'T') as Sudah_Schedule, "

				SQL = SQL & "isnull((select top(1) x.jumlah from N_EMI_Production_Plan_Schedule_Detail x, N_EMI_Production_Plan_Schedule y "
				SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Transaksi = y.No_Transaksi and y.Status is null "
				SQL = SQL & "), 0) as jumlah_schedule "

				SQL = SQL & ", c.nama "
				SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_Detail a, EMI_Transaksi_Sales_Forecasting b, Barang c "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur  "
				SQL = SQL & "and a.Kode_Perusahaan =c.Kode_Perusahaan and a.Kode_Barang = c.Kode_Barang and a.Kode_Stock_Owner = c.Kode_Stock_Owner "
				SQL = SQL & "and b.Status is null  and a.Bulan= '" & b & "' and a.Tahun = '" & fthn & "' "

				SQL = SQL & "and a.Kode_Barang in (" & daftarProductionPlantDiRelease & ") "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dim nama As String = Dr("nama")
						If General_Class.CekNULL(Dr("sudah_schedule")) = "Y" And Dr("jumlah_schedule") <> 0 Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show(nama & " sudah ada di schedule , tidak bisa unrelease data!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					Else
						Dr.Close()
					End If
				End Using

				SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
				SQL = SQL & "Tanggal,Jam) "
				SQL = SQL & " select a.Kode_Perusahaan, a.No_Faktur,a.urut,a.Nilai_Sales,a.Nilai_Sales,'RELEASE', '" & UserID & "' "
				SQL = SQL & ", '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
				SQL = SQL & "From  EMI_Transaksi_Sales_Forecasting_Detail a "
				SQL = SQL & "inner join	EMI_Transaksi_Sales_Forecasting b on a.Kode_Perusahaan  =b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
				SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Bulan = '" & b & "' and b.tahun = '" & fthn & "' and status is null "
				SQL = SQL & "and a.Kode_Barang in (" & daftarProductionPlantDiRelease & ") "
				ExecuteTrans(SQL)

				If fStatus = "Transaksi_ForecastOrder_PPIC" Then
					SQL = "update EMI_Transaksi_Sales_Forecasting_detail  set "
					SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
					SQL = SQL & "Tanggal_Validasi_PPIC = null,"
					SQL = SQL & "Jam_Validasi_PPIC = null, "
					SQL = SQL & "Status_Data = 'UNSUBMITED' "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
					SQL = SQL & "and (Kode_Barang in (" & daftarProductionPlantDiRelease & ") or flag_semi_fg = 'Y') "
					ExecuteTrans(SQL)

				ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
					SQL = "update EMI_Transaksi_Sales_Forecasting_detail  set "
					SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
					SQL = SQL & "Tanggal_Validasi = null,"
					SQL = SQL & "Jam_Validasi = null, "
					SQL = SQL & "Status_Data = 'UNSUBMITTED' "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
					SQL = SQL & "and Kode_Barang in (" & daftarProductionPlantDiRelease & ") "
					ExecuteTrans(SQL)
				End If

			Next

			'--- 1

			''--- 1
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If

			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)

			'    End If
			'Next
			'Dim no_faktur As String = ""
			'SQL = "Select no_faktur,flag_validasi,flag_validasi_ppic from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then

			'        If fStatus = "Transaksi_ForecastOrder_Sales" Then
			'            If General_Class.CekNULL(dr("flag_validasi")) = "" Then
			'                dr.Close()
			'                CloseConn()
			'                MessageBox.Show("Data belum pernah di submit!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                Exit Sub
			'            End If

			'            If General_Class.CekNULL(dr("flag_validasi_ppic")) = "Y" Then
			'                dr.Close()
			'                CloseConn()
			'                MessageBox.Show("Data yang telah di realease PPIC Tidak bisa di Batalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                Exit Sub
			'            End If
			'        End If

			'        If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'            If General_Class.CekNULL(dr("flag_validasi_ppic")) = "" Then
			'                dr.Close()
			'                CloseConn()
			'                MessageBox.Show("Data belum pernah di submit!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                Exit Sub
			'            End If
			'        End If

			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = null,"
			'    SQL = SQL & "Jam_Validasi_PPIC = null, "
			'    SQL = SQL & "Status_Data = 'SUBMITED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
			'    SQL = SQL & "Tanggal_Validasi = null,"
			'    SQL = SQL & "Jam_Validasi = null, "
			'    SQL = SQL & "Status_Data = 'UNSUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			''--- 2
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If
			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)
			'    End If
			'Next

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = null,"
			'    SQL = SQL & "Jam_Validasi_PPIC = null, "
			'    SQL = SQL & "Status_Data = 'SUBMITED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
			'    SQL = SQL & "Tanggal_Validasi = null,"
			'    SQL = SQL & "Jam_Validasi = null, "
			'    SQL = SQL & "Status_Data = 'UNSUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			'--- 3
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If
			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)
			'    End If
			'Next

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = null,"
			'    SQL = SQL & "Jam_Validasi_PPIC = null, "
			'    SQL = SQL & "Status_Data = 'SUBMITED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
			'    SQL = SQL & "Tanggal_Validasi = null,"
			'    SQL = SQL & "Jam_Validasi = null, "
			'    SQL = SQL & "Status_Data = 'UNSUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			''--- 4
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If
			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)
			'    End If
			'Next

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = null,"
			'    SQL = SQL & "Jam_Validasi_PPIC = null, "
			'    SQL = SQL & "Status_Data = 'SUBMITED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
			'    SQL = SQL & "Tanggal_Validasi = null,"
			'    SQL = SQL & "Jam_Validasi = null, "
			'    SQL = SQL & "Status_Data = 'UNSUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			''--- 5
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If
			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)
			'    End If
			'Next

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = null,"
			'    SQL = SQL & "Jam_Validasi_PPIC = null, "
			'    SQL = SQL & "Status_Data = 'SUBMITED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
			'    SQL = SQL & "Tanggal_Validasi = null,"
			'    SQL = SQL & "Jam_Validasi = null, "
			'    SQL = SQL & "Status_Data = 'UNSUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			''--- 6
			'If a = 12 Then
			'    a = 1
			'    fthn = fthn + 1
			'Else
			'    a = a + 1
			'End If
			'For index = 0 To arrBulan.Count - 1
			'    If arrBulan.Item(index) = a Then
			'        b = arrBulanMM.Item(index)
			'    End If
			'Next

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			'Using dr = OpenTrans(SQL)
			'    If dr.Read Then
			'        no_faktur = dr("no_faktur")
			'    Else

			'        dr.Close()
			'        CloseConn()
			'        MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = null,"
			'    SQL = SQL & "Jam_Validasi_PPIC = null, "
			'    SQL = SQL & "Status_Data = 'SUBMITED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
			'    SQL = "update EMI_Transaksi_Sales_Forecasting set "
			'    SQL = SQL & "Flag_Validasi = null,User_Validasi = null ,"
			'    SQL = SQL & "Tanggal_Validasi = null,"
			'    SQL = SQL & "Jam_Validasi = null , "
			'    SQL = SQL & "Status_Data = 'UNSUBMITTED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)
			'End If

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show("Data berhasil dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		kosong("REFRESH")
	End Sub

	Private Sub Cmb_Lokasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lokasi.SelectedIndexChanged
		'If Cmb_Bulan.SelectedIndex = -1 Then
		'    Exit Sub
		'ElseIf Cmb_Tahun.SelectedIndex = -1 Then
		'    Exit Sub
		'ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
		'    Exit Sub
		'End If

		''DateTimePicker1.Enabled = True
		''Txt_Keterangan.Enabled = True

		'Start_Loading(Me)
		''get_data()
		'Get_Data_Rix()
		'End_Loading(Me)
	End Sub

	Private Sub DateTimePicker1_CloseUp(sender As Object, e As EventArgs)
		'If DateTimePicker1.Value = Nothing Then Exit Sub
		''If Cmb_Lokasi.SelectedIndex = -1 Then Exit Sub

		get_jam()

		Dim selectedDate As Date = Format(tgl_skg, "yyyy-MM-dd")
		Dim selectedMonthName As String = selectedDate.ToString("MMMM", New Globalization.CultureInfo("id-ID"))
		Dim selectedYear As Integer = selectedDate.Year

		'If selectedYear < tahun_awal Or selectedYear > tahun_akhir Then
		'    MessageBox.Show("Tahun harus dalam rentang dua tahun dari tahun ini.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    Exit Sub
		'End If

		Cmb_Bulan.SelectedItem = selectedMonthName
		Cmb_Tahun.SelectedItem = selectedYear
	End Sub

	Private Sub EMI_Transaksi_ForecastOrder_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
		'Display_Transaksi_ForecastOrder.Btn_Refresh_Click(Btn_Simpan, e)
	End Sub

	Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
		If Cmb_Bulan.SelectedIndex = -1 Then
			Exit Sub
		ElseIf Cmb_Tahun.SelectedIndex = -1 Then
			Exit Sub
		ElseIf Cmb_Lokasi.SelectedIndex = -1 Then
			Exit Sub
		End If

		'DateTimePicker1.Enabled = True
		'Txt_Keterangan.Enabled = True
		isLoading = True
		CheckBox1.Checked = False
		Start_Loading(Me)

		Get_Data_Rix()
		End_Loading(Me)

		isLoading = False
	End Sub

	Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

		If isFromGrid Then Exit Sub ' <-- PENTING BANGET

		isFromHeader = True

		For i As Integer = 0 To DataGridView1.Rows.Count - 1
			If Not DataGridView1.Rows(i).IsNewRow Then
				DataGridView1.Rows(i).Cells(CellChkBox).Value = CheckBox1.Checked
			End If
		Next

		isFromHeader = False

	End Sub

	Private Sub DataGridView1_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
		If DataGridView1.IsCurrentCellDirty Then
			DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
		End If
	End Sub

	'Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

	'	If isFromHeader Then Exit Sub

	'	If e.ColumnIndex = DataGridView1.Columns(CellChkBox).Index Then

	'		isFromGrid = True ' tandain ini dari grid

	'		Dim allChecked As Boolean = True

	'		For i As Integer = 0 To DataGridView1.Rows.Count - 1
	'			If Not DataGridView1.Rows(i).IsNewRow Then
	'				Dim val = DataGridView1.Rows(i).Cells(CellChkBox).Value

	'				If val Is Nothing OrElse val = False Then
	'					allChecked = False
	'					Exit For
	'				End If
	'			End If
	'		Next

	'		CheckBox1.Checked = allChecked

	'		isFromGrid = False

	'	End If
	'End Sub

	Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

		' 🔥 ini penting
		If isLoading OrElse isFromHeader Then Exit Sub

		If e.ColumnIndex = DataGridView1.Columns(CellChkBox).Index Then

			isFromGrid = True

			Dim allChecked As Boolean = True

			For i As Integer = 0 To DataGridView1.Rows.Count - 1
				If Not DataGridView1.Rows(i).IsNewRow Then

					Dim val = DataGridView1.Rows(i).Cells(CellChkBox).Value

					If val Is Nothing OrElse val = False Then
						allChecked = False
						Exit For
					End If

				End If
			Next

			CheckBox1.Checked = allChecked

			isFromGrid = False

		End If
	End Sub

	Private Sub ExportToExcel()

		If fStatus <> "Transaksi_ForecastOrder_PPIC" AndAlso fStatus <> "Transaksi_ForecastOrder_Sales" Then
			MessageBox.Show("Anda tidak memilik akses untuk melakukan export data!", "Peringatan!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Dim xlApp As New Excel.Application
		Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add()
		Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Sheets(1)

		xlApp.ScreenUpdating = False

		' mapping kolom
		Dim mapping As Integer() = {
			CellKdBrg,                    ' Kode Barang
			CellNmBrg,                    ' Nama Barang
			CellSatuan,                   ' Satuan
			CellPersenStockGR1,           ' Stock GR 1
			CellPersenStockGR2,           ' Stock GR 2
			CellJumlahDO,                 ' Delivery Order Current Month
			CellPersenCurrentMonth,       ' % Current Month
			CellForecastCurrentMonth,     ' Production Plan Current Month
			CellSalesForecastBln1,        ' Sales Mei
			CellPPICForecastBln1,         ' PPIC Mei
			CellSalesForecastBln2,        ' Sales Juni
			CellPPICForecastBln2,         ' PPIC Juni
			CellStatus                    ' Status
		}

		Dim colCount As Integer = mapping.Length
		Dim rowCount As Integer = DataGridView1.Rows.Count

		' 🔥 HEADER
		For j As Integer = 0 To colCount - 1
			xlWorkSheet.Cells(1, j + 1) = DataGridView1.Columns(mapping(j)).HeaderText
		Next

		' 🔥 HITUNG ROW VALID
		Dim validRowCount As Integer = 0
		For i As Integer = 0 To rowCount - 1
			If Not DataGridView1.Rows(i).IsNewRow Then
				validRowCount += 1
			End If
		Next

		If validRowCount = 0 Then Exit Sub

		' 🔥 ARRAY 2D
		Dim data(validRowCount - 1, colCount - 1) As Object

		Dim rowIndex As Integer = 0

		For i As Integer = 0 To rowCount - 1
			If Not DataGridView1.Rows(i).IsNewRow Then

				For j As Integer = 0 To colCount - 1
					data(rowIndex, j) = DataGridView1.Item(mapping(j), i).Value
				Next

				rowIndex += 1
			End If
		Next

		' 🔥 BULK WRITE (INI YANG NGEBOOST SPEED)
		Dim startCell = xlWorkSheet.Cells(2, 1)
		Dim endCell = xlWorkSheet.Cells(validRowCount + 1, colCount)

		xlWorkSheet.Range(startCell, endCell).Value = data

		' format
		xlWorkSheet.Columns(1).NumberFormat = "@"
		xlWorkSheet.Columns.AutoFit()

		' SAVE
		Dim saveFileDialog As New SaveFileDialog
		saveFileDialog.Filter = "Excel Files|*.xlsx"

		Dim berhasil As Boolean = False

		If saveFileDialog.ShowDialog = DialogResult.OK Then
			xlWorkBook.SaveAs(saveFileDialog.FileName)
			' CLEANUP
			xlWorkBook.Close(False)
			xlApp.Quit()

			berhasil = True

		End If

		If berhasil Then

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)

			MessageBox.Show("Export berhasil!", "Sukses Simpan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Else
			MessageBox.Show("Export dibatalkan", "Batal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		End If

	End Sub

	Private Sub releaseObject(ByVal obj As Object)
		Try
			System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
			obj = Nothing
		Catch
			obj = Nothing
		Finally
			GC.Collect()
		End Try
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		ExportToExcel()
	End Sub

	Private Sub ImportFromExcel()

		If fStatus <> "Transaksi_ForecastOrder_PPIC" AndAlso fStatus <> "Transaksi_ForecastOrder_Sales" Then
			MessageBox.Show("Anda tidak memilik akses untuk melakukan import data!", "Peringatan!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Dim openFileDialog As New OpenFileDialog
		openFileDialog.Filter = "Excel Files|*.xlsx;*.xls"

		If openFileDialog.ShowDialog <> DialogResult.OK Then Exit Sub

		Dim xlApp As New Excel.Application
		Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Open(openFileDialog.FileName)
		Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Sheets(1)

		Try
			Dim rowExcel As Integer = 2

			While xlWorkSheet.Cells(rowExcel, 1).Value IsNot Nothing

				Dim kodeExcel As String = xlWorkSheet.Cells(rowExcel, 1).Value.ToString()

				' 🔥 CARI ROW DI GRID
				Dim foundRow As DataGridViewRow = Nothing

				For Each dgRow As DataGridViewRow In DataGridView1.Rows
					If Not dgRow.IsNewRow AndAlso dgRow.Cells(CellKdBrg).Value.ToString() = kodeExcel Then
						foundRow = dgRow
						Exit For
					End If
				Next

				If foundRow Is Nothing Then
					Throw New Exception("Kode Barang tidak ditemukan: " & kodeExcel)
				End If

				' ================== AMBIL NILAI ==================
				Dim valSales1 = xlWorkSheet.Cells(rowExcel, 9).Value
				Dim valPPIC1 = xlWorkSheet.Cells(rowExcel, 10).Value

				Dim valSales2 = xlWorkSheet.Cells(rowExcel, 11).Value
				Dim valPPIC2 = xlWorkSheet.Cells(rowExcel, 12).Value

				' ================== VALIDASI ==================

				If fStatus = "Transaksi_ForecastOrder_PPIC" Then

					If Not IsNothing(valSales1) AndAlso valSales1.ToString() <> "" Then
						Throw New Exception("Baris " & rowExcel & " : PPIC tidak boleh isi SALES")
					End If

					If Not IsNothing(valSales2) AndAlso valSales2.ToString() <> "" Then
						Throw New Exception("Baris " & rowExcel & " : PPIC tidak boleh isi SALES")
					End If

					' 🔥 UPDATE GRID
					foundRow.Cells(CellPPICForecastBln1).Value = valPPIC1
					foundRow.Cells(CellPPICForecastBln2).Value = valPPIC2

				ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then

					If Not IsNothing(valPPIC1) AndAlso valPPIC1.ToString() <> "" Then
						Throw New Exception("Baris " & rowExcel & " : SALES tidak boleh isi PPIC")
					End If

					If Not IsNothing(valPPIC2) AndAlso valPPIC2.ToString() <> "" Then
						Throw New Exception("Baris " & rowExcel & " : SALES tidak boleh isi PPIC")
					End If

					' 🔥 UPDATE GRID
					foundRow.Cells(CellSalesForecastBln1).Value = valSales1
					foundRow.Cells(CellSalesForecastBln2).Value = valSales2
				Else

				End If

				rowExcel += 1

			End While

			MessageBox.Show("Import update berhasil!")
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		Finally
			xlWorkBook.Close(False)
			xlApp.Quit()

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)
		End Try

	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		ImportFromExcel()
	End Sub

	'======================================================================================================
	'======================================================================================================

	Private Sub Get_Data_Rix()

		DataGridView1.Columns(CellChkBox).HeaderText = "#"
		DataGridView1.Columns(CellKdBrg).HeaderText = "Kode Barang"
		DataGridView1.Columns(CellNmBrg).HeaderText = "Nama Barang"
		DataGridView1.Columns(CellAvg3Bulan).HeaderText = "Avg 3 Bulan (Pcs)"
		DataGridView1.Columns(CellForecastCurrentMonth).HeaderText = "Production Plan Current Month"
		DataGridView1.Columns(CellActualCurrentMonth).HeaderText = "Actual Current Month"
		DataGridView1.Columns(CellPersenCurrentMonth).HeaderText = "% Current Month"
		Dim a As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
		Dim fthn As Integer = Val(Cmb_Tahun.Text)
		Dim panggil_databulan As String = ""
		Dim panggil_datatahun As String = ""

		For i As Integer = 1 To 2
			' Perbarui nilai bulan dan tahun
			If a = 12 Then
				a = 1
				fthn = fthn + 1
			Else
				a = a + 1
			End If

			Dim b As String = ""

			' Temukan nama bulan yang sesuai
			For index = 0 To arrBulan.Count - 1
				If arrBulan.Item(index) = a Then
					b = Cmb_Bulan.Items(index)
					If i = 1 Then ' Hanya sekali untuk panggil_databulan di iterasi pertama
						panggil_databulan = arrBulanMM.Item(index)
						panggil_datatahun = fthn
					End If
				End If
			Next

			Dim CellSalesForecastBln As Integer = CType(Me.GetType().GetField("CellSalesForecastBln" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), Integer)
			Dim CellPPICForecastBln As Integer = CType(Me.GetType().GetField("CellPPICForecastBln" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), Integer)
			Dim CellUrut As Integer = CType(Me.GetType().GetField("CellUrut_" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), Integer)
			Dim CellRV As Integer = CType(Me.GetType().GetField("CellRV_" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), Integer)
			Dim CellSpace As Integer = CType(Me.GetType().GetField("CellSpace" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), Integer)

			DataGridView1.Columns(CellSalesForecastBln).HeaderText = "Sales - Production Plan " & b & " - " & fthn
			DataGridView1.Columns(CellPPICForecastBln).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
			DataGridView1.Columns(CellUrut).HeaderText = i
			DataGridView1.Columns(CellSpace).HeaderText = ""
		Next

		DataGridView1.Columns(CellStatus).HeaderText = "Status"

		Dim fLoad As Boolean = False
		Dim aksesUbahSales As String = ""

		Try
			OpenConn()

			If CekButtonRole("EMI_Transaksi_ForecastOrder_Sales") = "Y" Then
				aksesUbahSales = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			SQL = "select No_Faktur from EMI_Transaksi_Sales_Forecasting where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "Lokasi = '" & Cmb_Lokasi.Text & "' and Bulan = '" & arrBulanMM.Item(Cmb_Bulan.SelectedIndex) & "' and Tahun = '" & Cmb_Tahun.Text & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Txt_NoFaktur.Text = Dr("No_Faktur")
					fLoad = True
				Else
					fLoad = False
				End If
			End Using

			'SQL = "select Flag_Validasi from EMI_Transaksi_Sales_Forecasting where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'SQL = SQL & "Lokasi = '" & Cmb_Lokasi.Text & "' and Bulan = '" & panggil_databulan & "' and Tahun = '" & panggil_datatahun & "'"
			'Using Dr = OpenTrans(SQL)
			'    If Dr.Read Then
			'        If fStatus = "Transaksi_ForecastOrder_Sales" Then
			'            If aksesUbahSales = "Y" Then
			'                If General_Class.CekNULL(Dr("Flag_Validasi")) = "Y" Then
			'                    btn_TambahBarang.Enabled = False
			'                Else
			'                    btn_TambahBarang.Enabled = True
			'                End If
			'            Else
			'                btn_TambahBarang.Enabled = False
			'            End If
			'        Else
			'            btn_TambahBarang.Enabled = False
			'        End If
			'    Else
			'        If fStatus = "Transaksi_ForecastOrder_Sales" Then
			'            If aksesUbahSales = "Y" Then
			'                btn_TambahBarang.Enabled = True
			'            Else
			'                btn_TambahBarang.Enabled = False
			'            End If
			'        Else
			'            btn_TambahBarang.Enabled = False
			'        End If
			'    End If
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Txt_NoFaktur_Leave(Cmb_Tahun, Nothing)
		'If fLoad = True Then
		'    Txt_NoFaktur_Leave(Cmb_Tahun, Nothing)
		'Else
		'    DataGridView1.Rows.Clear()
		'End If

	End Sub

	'Optimasi Pertama
	'Public Sub Get_Barang_Rix()
	'	Dim aksesUbahSales As String = ""
	'	Dim aksesUbahPPIC As String = ""

	'	' Cek hak akses user untuk role PPIC dan Sales dalam 1 koneksi sekaligus
	'	Try
	'		OpenConn()
	'		If CekButtonRole("EMI_Transaksi_ForecastOrder_PPIC") = "Y" Then
	'			aksesUbahPPIC = "Y"
	'		End If
	'		If CekButtonRole("EMI_Transaksi_ForecastOrder_Sales") = "Y" Then
	'			aksesUbahSales = "Y"
	'		End If
	'		CloseConn()
	'	Catch ex As Exception
	'		CloseConn()
	'		MessageBox.Show(ex.Message)
	'		Exit Sub
	'	End Try

	'	' Ambil semua data yang dibutuhkan sekaligus sebelum mengisi grid
	'	Try
	'		OpenConn()

	'		DataGridView1.SuspendLayout()
	'		DataGridView1.Rows.Clear()

	'		' Ambil bulan dan tahun yang dipilih user dari combobox
	'		Dim a1 As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
	'		Dim fthn1 As Integer = Val(Cmb_Tahun.Text)

	'		Dim listBulan As New List(Of String)
	'		Dim listTahun As New List(Of Integer)
	'		Dim tempBulan As Integer = a1
	'		Dim tempTahun As Integer = fthn1

	'		' Buat daftar 3 bulan berurutan mulai dari bulan yang dipilih
	'		For j As Integer = 0 To 2
	'			Dim bulanStr As String = ""
	'			For idx = 0 To arrBulan.Count - 1
	'				If arrBulan(idx) = tempBulan Then bulanStr = arrBulanMM(idx)
	'			Next
	'			listBulan.Add("'" & bulanStr & "'")
	'			listTahun.Add(tempTahun)
	'			' Jika bulan Desember, pindah ke Januari tahun berikutnya
	'			If tempBulan = 12 Then : tempBulan = 1 : tempTahun += 1
	'			Else : tempBulan += 1
	'			End If
	'		Next

	'		' Gabungkan daftar bulan dan tahun menjadi string untuk dipakai di query SQL
	'		Dim bulanSQL As String = String.Join(",", listBulan.Distinct())
	'		Dim tahunSQL As String = String.Join(",", listTahun.Distinct())

	'		' Siapkan dictionary untuk menyimpan status data per barang per bulan per tahun
	'		Dim dictStatus As New Dictionary(Of String, String)
	'		Dim dictValidasi As New Dictionary(Of String, String)
	'		Dim dictValidasi_ppic As New Dictionary(Of String, String)

	'		' Ambil status data dari tabel forecasting header dan detail sekaligus untuk semua bulan
	'		SQL = "select a.kode_barang, b.bulan, b.tahun, a.Status_Data " &
	'			  "from EMI_Transaksi_Sales_Forecasting_detail a " &
	'			  "join EMI_Transaksi_Sales_Forecasting b " &
	'			  "  on a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur " &
	'			  "where a.kode_perusahaan = '" & KodePerusahaan & "' " &
	'			  "and b.bulan in (" & bulanSQL & ") " &
	'			  "and b.tahun in (" & tahunSQL & ") " &
	'			  "and b.status is null"

	'		Using ds = BindingTrans(SQL)
	'			For Each row As DataRow In ds.Tables("MyTable").Rows
	'				' Kalau status kosong, tandai sebagai UNSUBMITED
	'				Dim statusValue As String = If(General_Class.CekNULL(row("status_data")) = "", "UNSUBMITED", row("Status_Data").ToString())
	'				' Kunci dictionary gabungan kode barang, bulan, dan tahun
	'				Dim key As String = row("kode_barang") & "|" & row("bulan") & "|" & row("tahun")
	'				dictStatus(key) = statusValue
	'			Next
	'		End Using

	'		' Ambil flag validasi Sales dan PPIC untuk semua barang dan bulan sekaligus
	'		SQL = "select kode_barang, bulan, tahun, flag_validasi, flag_validasi_ppic " &
	'			  "from EMI_Transaksi_Sales_Forecasting_detail " &
	'			  "where kode_perusahaan = '" & KodePerusahaan & "' " &
	'			  "and bulan in (" & bulanSQL & ") " &
	'			  "and tahun in (" & tahunSQL & ")"

	'		Using ds = BindingTrans(SQL)
	'			For Each row As DataRow In ds.Tables("MyTable").Rows
	'				Dim key As String = row("kode_barang") & "|" & row("bulan") & "|" & row("tahun")
	'				' Simpan flag validasi Sales dan PPIC ke masing-masing dictionary
	'				dictValidasi(key) = If(General_Class.CekNULL(row("flag_validasi")) = "", "", row("flag_validasi").ToString())
	'				dictValidasi_ppic(key) = If(General_Class.CekNULL(row("flag_validasi_ppic")) = "", "", row("flag_validasi_ppic").ToString())
	'			Next
	'		End Using

	'		' Ambil semua data forecast Sales dan PPIC untuk 3 bulan sekaligus
	'		' Disimpan di dictionary agar tidak perlu query ulang di dalam loop barang
	'		Dim dictForecast As New Dictionary(Of String, DataRow)

	'		SQL = "select Bulan,Tahun,Kode_Barang,Kode_Stock_Owner, " &
	'			  "Nilai_PPIC,Nilai_Sales,Urut,cast(rv as bigint) as rvx " &
	'			  "from EMI_Transaksi_Sales_Forecasting_Detail " &
	'			  "where Kode_Perusahaan = '" & KodePerusahaan & "' " &
	'			  "and Bulan in (" & bulanSQL & ") " &
	'			  "and Tahun in (" & tahunSQL & ")"

	'		Using ds = BindingTrans(SQL)
	'			For Each row As DataRow In ds.Tables("MyTable").Rows
	'				' Kunci dictionary gabungan kode barang, bulan, tahun, dan lokasi stock owner
	'				Dim dictKey As String = row("Kode_Barang") & "|" & row("Bulan") & "|" &
	'								row("Tahun") & "|" & row("Kode_Stock_Owner")
	'				dictForecast(dictKey) = row
	'			Next
	'		End Using

	'		' Ambil satuan semua barang sekaligus dalam 1 query, bukan 1 query per barang
	'		Dim dictSatuan As New Dictionary(Of String, String)
	'		Dim listBarangSQL As String = String.Join("','",
	'			Arrbarang.Cast(Of Object).Select(Function(x) x.ToString().Trim()))

	'		SQL = "select kode_barang, satuan from barang " &
	'			  "where kode_perusahaan = '" & KodePerusahaan & "' " &
	'			  "and kode_barang in ('" & listBarangSQL & "')"

	'		Using ds = BindingTrans(SQL)
	'			For Each row As DataRow In ds.Tables("MyTable").Rows
	'				dictSatuan(row("kode_barang").ToString()) = row("satuan").ToString()
	'			Next
	'		End Using

	'		' Ambil data forecast bulan berjalan yang sudah divalidasi Sales
	'		' Dipakai untuk kolom Forecast Current Month di grid
	'		Dim bCurrentMonth As String = arrBulanMM(Cmb_Bulan.SelectedIndex)
	'		Dim fThnCurrent As Integer = Val(Cmb_Tahun.Text)
	'		Dim dictCurrentMonth As New Dictionary(Of String, String)

	'		SQL = "select Kode_Barang, Kode_Stock_Owner, Nilai_PPIC " &
	'			  "from EMI_Transaksi_Sales_Forecasting_Detail " &
	'			  "where Kode_Perusahaan = '" & KodePerusahaan & "' " &
	'			  "and Bulan = '" & bCurrentMonth & "' and Tahun = '" & fThnCurrent & "' " &
	'			  "and flag_validasi = 'Y'"

	'		Using ds = BindingTrans(SQL)
	'			For Each row As DataRow In ds.Tables("MyTable").Rows
	'				' Kunci dictionary gabungan kode barang dan lokasi stock owner
	'				Dim key As String = row("Kode_Barang") & "|" & row("Kode_Stock_Owner")
	'				dictCurrentMonth(key) = Format(row("Nilai_PPIC"), "N2")
	'			Next
	'		End Using

	'		' Ambil data stok barang di GR untuk semua barang sekaligus
	'		Dim dictStockGR As New Dictionary(Of String, DataRow)

	'		SQL = "select Kode_Barang, Stock_GR1, Stock_GR2 " &
	'			  "from N_EMI_View_Transaksi_Production_Plan_Stock_GR " &
	'			  "where Kode_Perusahaan = '" & KodePerusahaan & "'"

	'		Using ds = BindingTrans(SQL)
	'			For Each row As DataRow In ds.Tables("MyTable").Rows
	'				dictStockGR(row("Kode_Barang").ToString()) = row
	'			Next
	'		End Using

	'		' Ambil data stok DO bulan berjalan untuk semua barang sekaligus
	'		Dim dictStockDO As New Dictionary(Of String, String)
	'		Dim BulanTahunDO As String = $"{arrBulanMM(Cmb_Bulan.SelectedIndex)}{Cmb_Tahun.Text.Trim}"

	'		SQL = "select Kode_Barang, StockDO " &
	'			  "from N_EMI_View_Transaksi_Production_Plan_Stock_DO " &
	'			  "where Kode_Perusahaan = '" & KodePerusahaan & "' " &
	'			  "and BulanTahun = '" & BulanTahunDO & "'"

	'		Using ds = BindingTrans(SQL)
	'			For Each row As DataRow In ds.Tables("MyTable").Rows
	'				dictStockDO(row("Kode_Barang").ToString()) = Format(Val(HilangkanTanda(row("StockDO"))), "N2")
	'			Next
	'		End Using

	'		' Loop setiap barang untuk mengisi baris di grid
	'		' Di sini tidak ada query ke database, semua data diambil dari dictionary
	'		For i = 0 To Arrbarang.Count - 1

	'			DataGridView1.Rows.Add(1)
	'			Dim ind As Integer = DataGridView1.Rows.Count - 1

	'			' Isi kolom kode barang, nama barang, dan nilai rata-rata 3 bulan
	'			DataGridView1.Rows(ind).Cells(CellKdBrg).Value = Arrbarang.Item(i)
	'			DataGridView1.Rows(ind).Cells(CellNmBrg).Value = ArrNama.Item(i)
	'			DataGridView1.Rows(ind).Cells(CellAvg3Bulan).Value = Format(0, "N2")

	'			' Isi kolom satuan dari dictionary, hentikan proses jika barang tidak ditemukan
	'			If dictSatuan.ContainsKey(Arrbarang(i)) Then
	'				DataGridView1.Rows(ind).Cells(CellSatuan).Value = dictSatuan(Arrbarang(i))
	'			Else
	'				MessageBox.Show("barang tidak ditemukan: " & Arrbarang(i))
	'				Exit Sub
	'			End If

	'			' Isi kolom forecast, actual, dan persen untuk bulan berjalan
	'			Dim keyCurrentMonth As String = Arrbarang(i) & "|" & Arrlokasi(i)
	'			Dim nilaiPPICCurrent As String = If(dictCurrentMonth.ContainsKey(keyCurrentMonth),
	'										dictCurrentMonth(keyCurrentMonth),
	'										Format(0, "N2"))
	'			DataGridView1.Rows(ind).Cells(CellForecastCurrentMonth).Value = nilaiPPICCurrent
	'			DataGridView1.Rows(ind).Cells(CellActualCurrentMonth).Value = Format(0, "N2")
	'			If nilaiPPICCurrent <> 0 Then
	'				DataGridView1.Rows(ind).Cells(CellPersenCurrentMonth).Value = Format(If(dictStockDO.ContainsKey(Arrbarang(i)), dictStockDO(Arrbarang(i)), Format(0, "N2")) / nilaiPPICCurrent * 100, "N2")
	'			Else
	'				DataGridView1.Rows(ind).Cells(CellPersenCurrentMonth).Value = Format(0, "N2")
	'			End If

	'			' Isi kolom stok GR1 dan GR2 dari dictionary
	'			If dictStockGR.ContainsKey(Arrbarang(i)) Then
	'				Dim grRow = dictStockGR(Arrbarang(i))
	'				DataGridView1.Rows(ind).Cells(CellPersenStockGR1).Value = Format(Val(HilangkanTanda(grRow("Stock_GR1"))), "N2")
	'				DataGridView1.Rows(ind).Cells(CellPersenStockGR2).Value = Format(Val(HilangkanTanda(grRow("Stock_GR2"))), "N2")
	'			Else
	'				DataGridView1.Rows(ind).Cells(CellPersenStockGR1).Value = Format(0, "N2")
	'				DataGridView1.Rows(ind).Cells(CellPersenStockGR2).Value = Format(0, "N2")
	'			End If

	'			' Isi kolom jumlah DO dari dictionary, isi 0 jika tidak ada data
	'			DataGridView1.Rows(ind).Cells(CellJumlahDO).Value =
	'		If(dictStockDO.ContainsKey(Arrbarang(i)), dictStockDO(Arrbarang(i)), Format(0, "N2"))

	'			' Mulai dari bulan yang dipilih, lalu loop untuk 2 bulan berikutnya
	'			Dim a As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
	'			Dim fthn As Integer = Val(Cmb_Tahun.Text)

	'			For j As Integer = 1 To 2

	'				' Geser ke bulan berikutnya, jika Desember maka pindah ke Januari tahun depan
	'				If a = 12 Then : a = 1 : fthn += 1
	'				Else : a += 1
	'				End If

	'				' Cari nama bulan dalam format string sesuai indeks
	'				Dim b As String = ""
	'				For idx = 0 To arrBulan.Count - 1
	'					If arrBulan.Item(idx) = a Then b = arrBulanMM.Item(idx)
	'				Next

	'				' Tampilkan status data bulan ini, jika tidak ada di dictionary berarti data baru
	'				Dim key As String = Arrbarang(i) & "|" & b & "|" & fthn
	'				DataGridView1.Rows(ind).Cells(CellStatus).Value =
	'			If(dictStatus.ContainsKey(key), dictStatus(key), "NEW")

	'				' Ambil nomor kolom grid secara dinamis berdasarkan urutan bulan ke-j
	'				' Menggunakan reflection karena nama field mengandung angka urutan bulan
	'				Dim CellSalesForecastBln As Integer = CType(
	'			Me.GetType().GetField("CellSalesForecastBln" & j,
	'				Reflection.BindingFlags.NonPublic Or
	'				Reflection.BindingFlags.Public Or
	'				Reflection.BindingFlags.Instance).GetValue(Me), Integer)

	'				Dim CellPPICForecastBln As Integer = CType(
	'			Me.GetType().GetField("CellPPICForecastBln" & j,
	'				Reflection.BindingFlags.NonPublic Or
	'				Reflection.BindingFlags.Public Or
	'				Reflection.BindingFlags.Instance).GetValue(Me), Integer)

	'				Dim CellUrut As Integer = CType(
	'			Me.GetType().GetField("CellUrut_" & j,
	'				Reflection.BindingFlags.NonPublic Or
	'				Reflection.BindingFlags.Public Or
	'				Reflection.BindingFlags.Instance).GetValue(Me), Integer)

	'				Dim CellRV As Integer = CType(
	'			Me.GetType().GetField("CellRV_" & j,
	'				Reflection.BindingFlags.NonPublic Or
	'				Reflection.BindingFlags.Public Or
	'				Reflection.BindingFlags.Instance).GetValue(Me), Integer)

	'				Dim CellSpace As Integer = CType(
	'			Me.GetType().GetField("CellSpace" & j,
	'				Reflection.BindingFlags.NonPublic Or
	'				Reflection.BindingFlags.Public Or
	'				Reflection.BindingFlags.Instance).GetValue(Me), Integer)

	'				' Isi nilai forecast Sales dan PPIC dari dictionary
	'				' Jika data tidak ada, isi semua kolom dengan nilai kosong atau nol
	'				Dim keyForecast As String = Arrbarang(i) & "|" & b & "|" & fthn & "|" & Arrlokasi(i)

	'				If dictForecast.ContainsKey(keyForecast) Then
	'					Dim row = dictForecast(keyForecast)
	'					DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Value = Format(row("Nilai_Sales"), "N2")
	'					DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Value = Format(row("Nilai_PPIC"), "N2")
	'					DataGridView1.Rows(ind).Cells(CellUrut).Value = row("Urut")
	'					DataGridView1.Rows(ind).Cells(CellRV).Value = row("rvx")
	'					DataGridView1.Rows(ind).Cells(CellSpace).Value = ""
	'				Else
	'					DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Value = Format(0, "N2")
	'					DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Value = Format(0, "N2")
	'					DataGridView1.Rows(ind).Cells(CellUrut).Value = ""
	'					DataGridView1.Rows(ind).Cells(CellRV).Value = ""
	'					DataGridView1.Rows(ind).Cells(CellSpace).Value = ""
	'				End If

	'				' Cek apakah Sales dan PPIC sudah melakukan validasi untuk bulan ini
	'				Dim isValidSales As Boolean = dictValidasi.ContainsKey(key) AndAlso dictValidasi(key) = "Y"
	'				Dim isValidPPIC As Boolean = dictValidasi_ppic.ContainsKey(key) AndAlso dictValidasi_ppic(key) = "Y"

	'				' Atur warna dan status bisa edit atau tidak untuk mode Sales
	'				If fStatus = "Transaksi_ForecastOrder_Sales" Then

	'					' Kolom PPIC selalu tidak bisa diedit oleh Sales
	'					' Warna lavender jika sudah divalidasi PPIC, biru muda jika belum
	'					DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor =
	'				If(isValidPPIC, Color.Lavender, Color.LightCyan)
	'					DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True

	'					' Kolom Sales bisa diedit jika belum divalidasi dan user punya akses
	'					If isValidSales Then
	'						' Sudah divalidasi, kunci dan warna lavender
	'						DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.Lavender
	'						DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True
	'					ElseIf aksesUbahSales = "Y" Then
	'						' Belum divalidasi dan punya akses, boleh diedit, warna kuning
	'						DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.LightYellow
	'						DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = False
	'					Else
	'						' Belum divalidasi tapi tidak punya akses, kunci dan warna biru muda
	'						DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.LightCyan
	'						DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True
	'					End If

	'					' Atur warna dan status bisa edit atau tidak untuk mode PPIC
	'				ElseIf fStatus = "Transaksi_ForecastOrder_PPIC" Then

	'					' Kolom Sales selalu tidak bisa diedit oleh PPIC
	'					' Warna lavender jika sudah divalidasi Sales, biru muda jika belum
	'					DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor =
	'				If(isValidSales, Color.Lavender, Color.LightCyan)
	'					DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True

	'					' Kolom PPIC hanya bisa diedit jika Sales sudah validasi dan PPIC belum validasi
	'					If isValidPPIC Then
	'						' PPIC sudah validasi, kunci dan warna lavender
	'						DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.Lavender
	'						DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True
	'					ElseIf aksesUbahPPIC = "Y" Then
	'						' PPIC belum validasi dan punya akses, boleh diedit jika Sales sudah validasi
	'						DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.LightYellow
	'						' Hanya bisa diedit jika Sales sudah validasi terlebih dahulu
	'						DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = Not isValidSales
	'					Else
	'						' Tidak punya akses, kunci dan warna biru muda
	'						DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.LightCyan
	'						DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True
	'					End If

	'				End If

	'			Next j  ' selesai loop bulan
	'		Next i      ' selesai loop barang

	'		DataGridView1.ResumeLayout()
	'		CloseConn()

	'	Catch ex As Exception
	'		CloseConn()
	'		MessageBox.Show(ex.Message)
	'		Exit Sub
	'	End Try

	'End Sub

	'Oprimasi versi 2
	Public Sub Get_Barang_Rix()
		Dim aksesUbahSales As String = ""
		Dim aksesUbahPPIC As String = ""

		' Cek hak akses user untuk role PPIC dan Sales dalam 1 koneksi sekaligus
		Try
			OpenConn()
			If CekButtonRole("EMI_Transaksi_ForecastOrder_PPIC") = "Y" Then
				aksesUbahPPIC = "Y"
			End If
			OpenConn()

			If CekButtonRole("EMI_Transaksi_ForecastOrder_Sales") = "Y" Then
				aksesUbahSales = "Y"
			End If
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			' ── OPTIMASI #5: Tahan rendering grid sepenuhnya ──────────────────────────
			CType(DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			DataGridView1.SuspendLayout()
			DataGridView1.Rows.Clear()

			' Ambil bulan dan tahun yang dipilih user dari combobox
			Dim a1 As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
			Dim fthn1 As Integer = Val(Cmb_Tahun.Text)

			Dim listBulan As New List(Of String)
			Dim listTahun As New List(Of Integer)
			Dim tempBulan As Integer = a1
			Dim tempTahun As Integer = fthn1

			' Buat daftar 3 bulan berurutan mulai dari bulan yang dipilih
			For j As Integer = 0 To 2
				Dim bulanStr As String = ""
				For idx = 0 To arrBulan.Count - 1
					If arrBulan(idx) = tempBulan Then bulanStr = arrBulanMM(idx)
				Next
				listBulan.Add("'" & bulanStr & "'")
				listTahun.Add(tempTahun)
				If tempBulan = 12 Then : tempBulan = 1 : tempTahun += 1
				Else : tempBulan += 1
				End If
			Next

			Dim bulanSQL As String = String.Join(",", listBulan.Distinct())
			Dim tahunSQL As String = String.Join(",", listTahun.Distinct())

			' ── OPTIMASI #3: Cache mapping bulan → nama bulan MM ──────────────────────
			' Dibuat sekali di sini, dipakai di dalam loop tanpa nested For
			Dim dictBulanMM As New Dictionary(Of Integer, String)
			For idx = 0 To arrBulan.Count - 1
				If Not dictBulanMM.ContainsKey(arrBulan(idx)) Then
					dictBulanMM(arrBulan(idx)) = arrBulanMM(idx)
				End If
			Next

			' ── OPTIMASI #1: Gabung query status + validasi jadi 1 query ──────────────
			' Sebelumnya 2 query terpisah ke tabel yang sama, sekarang cukup 1 round-trip
			Dim dictStatus As New Dictionary(Of String, String)
			Dim dictValidasi As New Dictionary(Of String, String)
			Dim dictValidasi_ppic As New Dictionary(Of String, String)

			SQL = "select a.kode_barang, b.bulan, b.tahun, a.Status_Data, " &
			  "       a.flag_validasi, a.flag_validasi_ppic " &
			  "from EMI_Transaksi_Sales_Forecasting_detail a " &
			  "join EMI_Transaksi_Sales_Forecasting b " &
			  "  on a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur " &
			  "where a.kode_perusahaan = '" & KodePerusahaan & "' " &
			  "and b.bulan in (" & bulanSQL & ") " &
			  "and b.tahun in (" & tahunSQL & ") " &
			  "and b.status is null"

			Using ds = BindingTrans(SQL)
				For Each row As DataRow In ds.Tables("MyTable").Rows
					Dim key As String = row("kode_barang") & "|" & row("bulan") & "|" & row("tahun")
					' Status: kosong berarti UNSUBMITED
					dictStatus(key) = If(General_Class.CekNULL(row("status_data")) = "", "UNSUBMITED", row("Status_Data").ToString())
					' Flag validasi Sales dan PPIC diisi sekaligus dari baris yang sama
					dictValidasi(key) = If(General_Class.CekNULL(row("flag_validasi")) = "", "", row("flag_validasi").ToString())
					dictValidasi_ppic(key) = If(General_Class.CekNULL(row("flag_validasi_ppic")) = "", "", row("flag_validasi_ppic").ToString())
				Next
			End Using

			' Ambil semua data forecast Sales dan PPIC untuk 3 bulan sekaligus
			Dim dictForecast As New Dictionary(Of String, DataRow)

			SQL = "select Bulan, Tahun, Kode_Barang, Kode_Stock_Owner, " &
			  "Nilai_PPIC, Nilai_Sales, Urut, cast(rv as bigint) as rvx " &
			  "from EMI_Transaksi_Sales_Forecasting_Detail " &
			  "where Kode_Perusahaan = '" & KodePerusahaan & "' " &
			  "and Bulan in (" & bulanSQL & ") " &
			  "and Tahun in (" & tahunSQL & ")"

			Using ds = BindingTrans(SQL)
				For Each row As DataRow In ds.Tables("MyTable").Rows
					Dim dictKey As String = row("Kode_Barang") & "|" & row("Bulan") & "|" &
								row("Tahun") & "|" & row("Kode_Stock_Owner")
					dictForecast(dictKey) = row
				Next
			End Using

			' Ambil satuan semua barang sekaligus dalam 1 query
			Dim dictSatuan As New Dictionary(Of String, String)
			Dim listBarangSQL As String = String.Join("','",
			Arrbarang.Cast(Of Object).Select(Function(x) x.ToString().Trim()))

			SQL = "select kode_barang, satuan from barang " &
			  "where kode_perusahaan = '" & KodePerusahaan & "' " &
			  "and kode_barang in ('" & listBarangSQL & "')"

			Using ds = BindingTrans(SQL)
				For Each row As DataRow In ds.Tables("MyTable").Rows
					dictSatuan(row("kode_barang").ToString()) = row("satuan").ToString()
				Next
			End Using

			' ── OPTIMASI #2: Hapus query dictCurrentMonth yang terpisah ───────────────
			' Data current month sudah ada di dictForecast (bulan berjalan termasuk dalam bulanSQL)
			' dan flag_validasi sudah ada di dictValidasi, jadi tidak perlu query tambahan.
			' nilaiPPICCurrent diambil langsung dari dictForecast di dalam loop barang.
			Dim bCurrentMonth As String = arrBulanMM(Cmb_Bulan.SelectedIndex)
			Dim fThnCurrent As Integer = Val(Cmb_Tahun.Text)

			' Ambil data stok barang di GR untuk semua barang sekaligus
			Dim dictStockGR As New Dictionary(Of String, DataRow)

			SQL = "select Kode_Barang, Stock_GR1, Stock_GR2 " &
			  "from N_EMI_View_Transaksi_Production_Plan_Stock_GR " &
			  "where Kode_Perusahaan = '" & KodePerusahaan & "'"

			Using ds = BindingTrans(SQL)
				For Each row As DataRow In ds.Tables("MyTable").Rows
					dictStockGR(row("Kode_Barang").ToString()) = row
				Next
			End Using

			' Ambil data stok DO bulan berjalan untuk semua barang sekaligus
			Dim dictStockDO As New Dictionary(Of String, String)
			Dim BulanTahunDO As String = $"{arrBulanMM(Cmb_Bulan.SelectedIndex)}{Cmb_Tahun.Text.Trim}"

			SQL = "select Kode_Barang, StockDO " &
			  "from N_EMI_View_Transaksi_Production_Plan_Stock_DO " &
			  "where Kode_Perusahaan = '" & KodePerusahaan & "' " &
			  "and BulanTahun = '" & BulanTahunDO & "'"

			Using ds = BindingTrans(SQL)
				For Each row As DataRow In ds.Tables("MyTable").Rows
					dictStockDO(row("Kode_Barang").ToString()) = Format(Val(HilangkanTanda(row("StockDO"))), "N2")
				Next
			End Using

			' ── OPTIMASI #4: Cache hasil reflection di luar loop barang ───────────────
			' Reflection itu mahal — jangan panggil ulang setiap iterasi barang × bulan.
			' Hitung sekali untuk j=1 dan j=2, simpan di array, pakai langsung di loop.
			Dim cachedCellSalesForecast(2) As Integer
			Dim cachedCellPPICForecast(2) As Integer
			Dim cachedCellUrut(2) As Integer
			Dim cachedCellRV(2) As Integer
			Dim cachedCellSpace(2) As Integer
			Dim bindingFlags As Reflection.BindingFlags =
			Reflection.BindingFlags.NonPublic Or
			Reflection.BindingFlags.Public Or
			Reflection.BindingFlags.Instance

			For j As Integer = 1 To 2
				cachedCellSalesForecast(j) = CType(Me.GetType().GetField("CellSalesForecastBln" & j, bindingFlags).GetValue(Me), Integer)
				cachedCellPPICForecast(j) = CType(Me.GetType().GetField("CellPPICForecastBln" & j, bindingFlags).GetValue(Me), Integer)
				cachedCellUrut(j) = CType(Me.GetType().GetField("CellUrut_" & j, bindingFlags).GetValue(Me), Integer)
				cachedCellRV(j) = CType(Me.GetType().GetField("CellRV_" & j, bindingFlags).GetValue(Me), Integer)
				cachedCellSpace(j) = CType(Me.GetType().GetField("CellSpace" & j, bindingFlags).GetValue(Me), Integer)
			Next

			' ── Loop utama: isi grid per barang ───────────────────────────────────────
			' Tidak ada query ke database di sini — semua data sudah di dictionary
			For i = 0 To Arrbarang.Count - 1

				DataGridView1.Rows.Add(1)
				Dim ind As Integer = DataGridView1.Rows.Count - 1

				DataGridView1.Rows(ind).Cells(CellKdBrg).Value = Arrbarang.Item(i)
				DataGridView1.Rows(ind).Cells(CellNmBrg).Value = ArrNama.Item(i)
				DataGridView1.Rows(ind).Cells(CellAvg3Bulan).Value = Format(0, "N2")

				' Isi satuan — keluar jika barang tidak ditemukan
				If dictSatuan.ContainsKey(Arrbarang(i)) Then
					DataGridView1.Rows(ind).Cells(CellSatuan).Value = dictSatuan(Arrbarang(i))
				Else
					MessageBox.Show("barang tidak ditemukan: " & Arrbarang(i))
					Exit Sub
				End If

				' ── OPTIMASI #2 (lanjutan): Ambil nilai current month dari dictForecast ──
				' Cek flag validasi dari dictValidasi yang sudah digabung di query #1
				Dim keyCurrentMonthForecast As String = Arrbarang(i) & "|" & bCurrentMonth & "|" & fThnCurrent & "|" & Arrlokasi(i)
				Dim keyCurrentMonthValidasi As String = Arrbarang(i) & "|" & bCurrentMonth & "|" & fThnCurrent
				Dim nilaiPPICCurrent As String = Format(0, "N2")

				If dictForecast.ContainsKey(keyCurrentMonthForecast) AndAlso
			   dictValidasi.ContainsKey(keyCurrentMonthValidasi) AndAlso
			   dictValidasi(keyCurrentMonthValidasi) = "Y" Then
					nilaiPPICCurrent = Format(dictForecast(keyCurrentMonthForecast)("Nilai_PPIC"), "N2")
				End If

				DataGridView1.Rows(ind).Cells(CellForecastCurrentMonth).Value = nilaiPPICCurrent
				DataGridView1.Rows(ind).Cells(CellActualCurrentMonth).Value = Format(0, "N2")
				If nilaiPPICCurrent <> 0 Then
					DataGridView1.Rows(ind).Cells(CellPersenCurrentMonth).Value = Format(
					If(dictStockDO.ContainsKey(Arrbarang(i)), dictStockDO(Arrbarang(i)), Format(0, "N2")) /
					nilaiPPICCurrent * 100, "N2")
				Else
					DataGridView1.Rows(ind).Cells(CellPersenCurrentMonth).Value = Format(0, "N2")
				End If

				' Isi kolom stok GR1 dan GR2
				If dictStockGR.ContainsKey(Arrbarang(i)) Then
					Dim grRow = dictStockGR(Arrbarang(i))
					DataGridView1.Rows(ind).Cells(CellPersenStockGR1).Value = Format(Val(HilangkanTanda(grRow("Stock_GR1"))), "N2")
					DataGridView1.Rows(ind).Cells(CellPersenStockGR2).Value = Format(Val(HilangkanTanda(grRow("Stock_GR2"))), "N2")
				Else
					DataGridView1.Rows(ind).Cells(CellPersenStockGR1).Value = Format(0, "N2")
					DataGridView1.Rows(ind).Cells(CellPersenStockGR2).Value = Format(0, "N2")
				End If

				DataGridView1.Rows(ind).Cells(CellJumlahDO).Value =
				If(dictStockDO.ContainsKey(Arrbarang(i)), dictStockDO(Arrbarang(i)), Format(0, "N2"))

				' Loop 2 bulan berikutnya setelah bulan yang dipilih
				Dim a As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
				Dim fthn As Integer = Val(Cmb_Tahun.Text)

				For j As Integer = 1 To 2

					' Geser ke bulan berikutnya
					If a = 12 Then : a = 1 : fthn += 1
					Else : a += 1
					End If

					' ── OPTIMASI #3 (lanjutan): Pakai dictionary, bukan nested For loop ──
					Dim b As String = dictBulanMM(a)

					Dim key As String = Arrbarang(i) & "|" & b & "|" & fthn
					DataGridView1.Rows(ind).Cells(CellStatus).Value =
					If(dictStatus.ContainsKey(key), dictStatus(key), "NEW")

					' ── OPTIMASI #4 (lanjutan): Pakai cached cell index, bukan reflection ──
					Dim CellSalesForecastBln As Integer = cachedCellSalesForecast(j)
					Dim CellPPICForecastBln As Integer = cachedCellPPICForecast(j)
					Dim CellUrut As Integer = cachedCellUrut(j)
					Dim CellRV As Integer = cachedCellRV(j)
					Dim CellSpace As Integer = cachedCellSpace(j)

					' Isi nilai forecast dari dictionary
					Dim keyForecast As String = Arrbarang(i) & "|" & b & "|" & fthn & "|" & Arrlokasi(i)

					If dictForecast.ContainsKey(keyForecast) Then
						Dim row = dictForecast(keyForecast)
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Value = Format(row("Nilai_Sales"), "N2")
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Value = Format(row("Nilai_PPIC"), "N2")
						DataGridView1.Rows(ind).Cells(CellUrut).Value = row("Urut")
						DataGridView1.Rows(ind).Cells(CellRV).Value = row("rvx")
						DataGridView1.Rows(ind).Cells(CellSpace).Value = ""
					Else
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Value = Format(0, "N2")
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Value = Format(0, "N2")
						DataGridView1.Rows(ind).Cells(CellUrut).Value = ""
						DataGridView1.Rows(ind).Cells(CellRV).Value = ""
						DataGridView1.Rows(ind).Cells(CellSpace).Value = ""
					End If

					' Cek status validasi dari dictionary yang sudah digabung
					Dim isValidSales As Boolean = dictValidasi.ContainsKey(key) AndAlso dictValidasi(key) = "Y"
					Dim isValidPPIC As Boolean = dictValidasi_ppic.ContainsKey(key) AndAlso dictValidasi_ppic(key) = "Y"

					' Atur warna dan editability sesuai mode (Sales / PPIC)
					If fStatus = "Transaksi_ForecastOrder_Sales" Then

						DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor =
						If(isValidPPIC, Color.Lavender, Color.LightCyan)
						DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True

						If isValidSales Then
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.Lavender
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True
						ElseIf aksesUbahSales = "Y" Then
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.LightYellow
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = False
						Else
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.LightCyan
							DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True
						End If

					ElseIf fStatus = "Transaksi_ForecastOrder_PPIC" Then

						DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor =
						If(isValidSales, Color.Lavender, Color.LightCyan)
						DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True

						If isValidPPIC Then
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.Lavender
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True
						ElseIf aksesUbahPPIC = "Y" Then
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.LightYellow
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = Not isValidSales
						Else
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.LightCyan
							DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True
						End If

					End If

				Next j  ' selesai loop bulan
			Next i      ' selesai loop barang

			DataGridView1.ResumeLayout()
			CType(DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
			CloseConn()
		Catch ex As Exception
			CloseConn()
			' Pastikan grid kembali normal walau terjadi error
			Try
				DataGridView1.ResumeLayout()
				CType(DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Catch
			End Try
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	'Versi pertama
	'Public Sub Get_Barang_Rix()
	'	Dim aksesUbahSales As String = ""
	'	Dim aksesUbahPPIC As String = ""

	'	Try
	'		OpenConn()

	'		If CekButtonRole("EMI_Transaksi_ForecastOrder_PPIC") = "Y" Then
	'			aksesUbahPPIC = "Y"
	'		End If

	'		CloseConn()
	'	Catch ex As Exception
	'		CloseConn()
	'		MessageBox.Show(ex.Message)
	'		Exit Sub
	'	End Try

	'	Try
	'		OpenConn()

	'		If CekButtonRole("EMI_Transaksi_ForecastOrder_Sales") = "Y" Then
	'			aksesUbahSales = "Y"
	'		End If

	'		CloseConn()
	'	Catch ex As Exception
	'		CloseConn()
	'		MessageBox.Show(ex.Message)
	'		Exit Sub
	'	End Try

	'	Try
	'		OpenConn()

	'		DataGridView1.SuspendLayout()
	'		DataGridView1.Rows.Clear()

	'		Dim a1 As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
	'		Dim fthn1 As Integer = Val(Cmb_Tahun.Text)

	'		Dim listBulan As New List(Of String)
	'		Dim listTahun As New List(Of Integer)

	'		Dim tempBulan As Integer = a1
	'		Dim tempTahun As Integer = fthn1

	'		For j As Integer = 0 To 2

	'			Dim bulanStr As String = ""

	'			For idx = 0 To arrBulan.Count - 1
	'				If arrBulan(idx) = tempBulan Then
	'					bulanStr = arrBulanMM(idx)
	'				End If
	'			Next

	'			listBulan.Add("'" & bulanStr & "'")
	'			listTahun.Add(tempTahun)

	'			' next bulan
	'			If tempBulan = 12 Then
	'				tempBulan = 1
	'				tempTahun += 1
	'			Else
	'				tempBulan += 1
	'			End If

	'		Next

	'		Dim bulanSQL As String = String.Join(",", listBulan.Distinct())
	'		Dim tahunSQL As String = String.Join(",", listTahun.Distinct())

	'		' ================== TAMBAHAN STEP 1 ==================
	'		Dim dictStatus As New Dictionary(Of String, String)
	'		Dim dictValidasi As New Dictionary(Of String, String)
	'		Dim dictValidasi_ppic As New Dictionary(Of String, String)

	'		' LOAD STATUS
	'		SQL = "select a.kode_barang, b.bulan, b.tahun, a.Status_Data "
	'		SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_detail a "
	'		SQL = SQL & "join EMI_Transaksi_Sales_Forecasting b "
	'		SQL = SQL & "on a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur "
	'		SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
	'		SQL = SQL & "and b.bulan in (" & bulanSQL & ") "
	'		SQL = SQL & "and b.tahun in (" & tahunSQL & ") "
	'		SQL = SQL & "and b.status is null "

	'		Using ds = BindingTrans(SQL)

	'			With ds.Tables("MyTable")
	'				If .Rows.Count <> 0 Then

	'					For i As Integer = 0 To .Rows.Count - 1
	'						Dim statusValue As String = ""

	'						If General_Class.CekNULL(.Rows(i).Item("status_data")) = "" Then
	'							statusValue = "UNSUBMITED"
	'						Else
	'							statusValue = .Rows(i)("Status_Data").ToString()
	'						End If

	'						Dim key As String =
	'						   .Rows(i)("kode_barang").ToString() & "|" &
	'						   .Rows(i)("bulan").ToString() & "|" &
	'						   .Rows(i)("tahun").ToString()

	'						dictStatus(key) = statusValue

	'					Next

	'				End If
	'			End With

	'		End Using

	'		SQL = "select kode_barang, bulan, tahun, flag_validasi, flag_validasi_ppic "
	'		SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_detail "
	'		SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
	'		SQL = SQL & "and bulan in (" & bulanSQL & ") "
	'		SQL = SQL & "and tahun in (" & tahunSQL & ") "

	'		Using ds = BindingTrans(SQL)

	'			With ds.Tables("MyTable")
	'				If .Rows.Count <> 0 Then
	'					For i As Integer = 0 To .Rows.Count - 1

	'						Dim validasiValue As String = ""

	'						If General_Class.CekNULL(.Rows(i).Item("flag_validasi")) = "" Then
	'							validasiValue = ""
	'						Else
	'							validasiValue = .Rows(i)("flag_validasi").ToString()
	'						End If

	'						Dim validasiValue_ppic As String = ""

	'						If General_Class.CekNULL(.Rows(i).Item("flag_validasi_ppic")) = "" Then
	'							validasiValue_ppic = ""
	'						Else
	'							validasiValue_ppic = .Rows(i)("flag_validasi_ppic").ToString()
	'						End If

	'						Dim key As String =
	'						.Rows(i)("kode_barang").ToString() & "|" &
	'						.Rows(i)("bulan").ToString() & "|" &
	'						.Rows(i)("tahun").ToString()

	'						dictValidasi(key) = validasiValue
	'						dictValidasi_ppic(key) = validasiValue_ppic

	'					Next
	'				End If
	'			End With

	'		End Using
	'		' ====================================================

	'		For i = 0 To Arrbarang.Count - 1

	'			DataGridView1.Rows.Add(1)
	'			Dim ind As Integer = DataGridView1.Rows.Count - 1

	'			' ================== LOAD SEMUA DATA SEKALI ==================
	'			Dim dictForecast As New Dictionary(Of String, DataRow)
	'			Dim dictSatuan As New Dictionary(Of String, String)

	'			Dim dictKey As String
	'			' Ambil semua forecast
	'			SQL = "select Bulan,Tahun,Kode_Barang,Kode_Stock_Owner, "
	'			SQL = SQL & "Nilai_PPIC,Nilai_Sales,Urut,cast(rv as bigint) as rvx "
	'			SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_Detail "
	'			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
	'			SQL = SQL & "and Bulan in (" & bulanSQL & ") "
	'			SQL = SQL & "and Tahun in (" & tahunSQL & ") "
	'			Using Ds = BindingTrans(SQL)
	'				With Ds.Tables("MyTable")
	'					If .Rows.Count <> 0 Then
	'						For indexForecast As Integer = 0 To .Rows.Count - 1
	'							dictKey =
	'							   .Rows(indexForecast)("Kode_Barang").ToString() & "|" &
	'							   .Rows(indexForecast)("Bulan").ToString() & "|" &
	'							   .Rows(indexForecast)("Tahun").ToString() & "|" &
	'							   .Rows(indexForecast)("Kode_Stock_Owner").ToString()

	'							dictForecast(dictKey) = .Rows(indexForecast)
	'						Next
	'					End If
	'				End With
	'			End Using

	'			'Dim listBarang = String.Join("','", Arrbarang.Cast(Of Object).Select(Function(x) x.ToString().Trim()))

	'			'SQL = "select distinct kode_barang, satuan "
	'			'SQL = SQL & "from barang "
	'			'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
	'			'SQL = SQL & "and kode_barang in (" & listBarang & ")"
	'			'Using ds = BindingTrans(SQL)
	'			'    With ds.Tables("MyTable")

	'			'        For indexSatuan As Integer = 0 To .Rows.Count - 1

	'			'            dictSatuan(.Rows(indexSatuan)("kode_barang").ToString()) =
	'			'            .Rows(indexSatuan)("satuan").ToString()

	'			'        Next
	'			'    End With

	'			'End Using

	'			'SQL = ""

	'			'If fStatus = "Transaksi_ForecastOrder_PPIC" Then
	'			'    DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).Style.BackColor = Color.LightCyan
	'			'    DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).Style.BackColor = Color.LightYellow
	'			'    DataGridView1.Rows(ind).Cells(CellSpace1).Style.BackColor = Color.LightGray
	'			'    DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).Style.BackColor = Color.LightCyan
	'			'    DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).Style.BackColor = Color.LightYellow
	'			'    DataGridView1.Rows(ind).Cells(CellSpace2).Style.BackColor = Color.LightGray
	'			'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then
	'			'    DataGridView1.Rows(ind).Cells(CellSalesForecastBln1).Style.BackColor = Color.LightYellow
	'			'    DataGridView1.Rows(ind).Cells(CellPPICForecastBln1).Style.BackColor = Color.LightCyan
	'			'    DataGridView1.Rows(ind).Cells(CellSpace1).Style.BackColor = Color.LightGray
	'			'    DataGridView1.Rows(ind).Cells(CellSalesForecastBln2).Style.BackColor = Color.LightYellow
	'			'    DataGridView1.Rows(ind).Cells(CellPPICForecastBln2).Style.BackColor = Color.LightCyan
	'			'    DataGridView1.Rows(ind).Cells(CellSpace2).Style.BackColor = Color.LightGray
	'			'End If

	'			'DataGridView1.Rows(ind).Cells(CellSalesForecastBln3).Style.BackColor = Color.LightYellow
	'			'DataGridView1.Rows(ind).Cells(CellPPICForecastBln3).Style.BackColor = Color.LightCyan
	'			'DataGridView1.Rows(ind).Cells(CellSpace3).Style.BackColor = Color.LightGray
	'			'DataGridView1.Rows(ind).Cells(CellSalesForecastBln4).Style.BackColor = Color.LightYellow
	'			'DataGridView1.Rows(ind).Cells(CellPPICForecastBln4).Style.BackColor = Color.LightCyan
	'			'DataGridView1.Rows(ind).Cells(CellSpace4).Style.BackColor = Color.LightGray
	'			'DataGridView1.Rows(ind).Cells(CellSalesForecastBln5).Style.BackColor = Color.LightYellow
	'			'DataGridView1.Rows(ind).Cells(CellPPICForecastBln5).Style.BackColor = Color.LightCyan
	'			'DataGridView1.Rows(ind).Cells(CellSpace5).Style.BackColor = Color.LightGray
	'			'DataGridView1.Rows(ind).Cells(CellSalesForecastBln6).Style.BackColor = Color.LightYellow
	'			'DataGridView1.Rows(ind).Cells(CellPPICForecastBln6).Style.BackColor = Color.LightCyan
	'			'DataGridView1.Rows(ind).Cells(CellSpace6).Style.BackColor = Color.LightGray
	'			'DataGridView1.Rows(ind).Cells(CellStatus).Style.BackColor = Color.Yellow

	'			DataGridView1.Rows(ind).Cells(CellKdBrg).Value = Arrbarang.Item(i)
	'			DataGridView1.Rows(ind).Cells(CellNmBrg).Value = ArrNama.Item(i)
	'			DataGridView1.Rows(ind).Cells(CellAvg3Bulan).Value = Format(0, "N2")

	'			'If dictSatuan.ContainsKey(Arrbarang(i)) Then
	'			'    DataGridView1.Rows(ind).Cells(CellSatuan).Value = dictSatuan(Arrbarang(i))
	'			'End If

	'			SQL = "Select top(1) satuan from barang a where kode_barang='" & Arrbarang.Item(i) & "' "
	'			Using dr2 = OpenTrans(SQL)
	'				If dr2.Read Then
	'					DataGridView1.Rows(ind).Cells(CellSatuan).Value = dr2("satuan")
	'				Else
	'					dr2.Close()
	'					CloseConn()
	'					MessageBox.Show("barang tidak ditemukan")
	'					Exit Sub
	'				End If
	'			End Using

	'			Dim a As Integer = arrBulan.Item(Cmb_Bulan.SelectedIndex)
	'			Dim fthn As Integer = Val(Cmb_Tahun.Text)
	'			Dim b As String = ""
	'			Dim FValidasi = "Y"
	'			For index = 0 To arrBulan.Count - 1
	'				If arrBulan.Item(index) = a Then
	'					'ComboBox1.SelectedIndex = index
	'					b = arrBulanMM.Item(index)
	'				End If
	'			Next

	'			'Current Month
	'			SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Sales,Urut, cast(rv as bigint) as rvx from EMI_Transaksi_Sales_Forecasting_Detail where "
	'			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
	'			SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(i) & "' and Kode_Barang = '" & Arrbarang.Item(i) & "' "
	'			SQL = SQL & "and flag_validasi = 'Y' "
	'			Using Ds2 = BindingTrans(SQL)
	'				With Ds2.Tables("MyTable")
	'					If .Rows.Count <> 0 Then
	'						DataGridView1.Rows(ind).Cells(CellForecastCurrentMonth).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
	'						DataGridView1.Rows(ind).Cells(CellActualCurrentMonth).Value = Format(0, "N2")
	'						DataGridView1.Rows(ind).Cells(CellPersenCurrentMonth).Value = Format(0, "N2")
	'					Else
	'						DataGridView1.Rows(ind).Cells(CellForecastCurrentMonth).Value = Format(0, "N2")
	'						DataGridView1.Rows(ind).Cells(CellActualCurrentMonth).Value = Format(0, "N2")
	'						DataGridView1.Rows(ind).Cells(CellPersenCurrentMonth).Value = Format(0, "N2")
	'					End If
	'				End With
	'			End Using

	'			SQL = $"
	'				select Kode_Perusahaan, Kode_Barang, Stock_GR1, Stock_GR2
	'				from N_EMI_View_Transaksi_Production_Plan_Stock_GR
	'				where Kode_Perusahaan = '{KodePerusahaan}'
	'				and Kode_Barang = '{Arrbarang.Item(i)}'
	'			"
	'			Using Ds2 = BindingTrans(SQL)
	'				With Ds2.Tables("MyTable")
	'					If .Rows.Count <> 0 Then
	'						DataGridView1.Rows(ind).Cells(CellPersenStockGR1).Value = Format(Val(HilangkanTanda(.Rows(0).Item("Stock_GR1"))), "N2")
	'						DataGridView1.Rows(ind).Cells(CellPersenStockGR2).Value = Format(Val(HilangkanTanda(.Rows(0).Item("Stock_GR2"))), "N2")
	'					Else
	'						DataGridView1.Rows(ind).Cells(CellPersenStockGR1).Value = Format(0, "N2")
	'						DataGridView1.Rows(ind).Cells(CellPersenStockGR2).Value = Format(0, "N2")
	'					End If
	'				End With
	'			End Using

	'			Dim BulanTahunDO As String = $"{arrBulanMM(Cmb_Bulan.SelectedIndex)}{Cmb_Tahun.Text.Trim}"
	'			SQL = $"
	'				select Kode_Perusahaan, Kode_Barang, BulanTahun, StockDO
	'				from N_EMI_View_Transaksi_Production_Plan_Stock_DO
	'				where Kode_Perusahaan = '{KodePerusahaan}'
	'				and Kode_Barang = '{Arrbarang.Item(i)}'
	'				and BulanTahun = '{BulanTahunDO}'
	'			"
	'			Using Ds2 = BindingTrans(SQL)
	'				With Ds2.Tables("MyTable")
	'					If .Rows.Count <> 0 Then
	'						DataGridView1.Rows(ind).Cells(CellJumlahDO).Value = Format(Val(HilangkanTanda(.Rows(0).Item("StockDO"))), "N2")
	'					Else
	'						DataGridView1.Rows(ind).Cells(CellJumlahDO).Value = Format(0, "N2")
	'					End If
	'				End With
	'			End Using

	'			dictKey = Arrbarang(i) & "|" & b & "|" & fthn & "|" & Arrlokasi(i)

	'			For j As Integer = 1 To 2
	'				' Perbarui nilai bulan dan tahun
	'				If a = 12 Then
	'					a = 1
	'					fthn = fthn + 1
	'				Else
	'					a = a + 1
	'				End If

	'				' Temukan nama bulan yang sesuai
	'				For index = 0 To arrBulan.Count - 1
	'					If arrBulan.Item(index) = a Then
	'						b = arrBulanMM.Item(index)

	'						' Load_Data_Perbulan(b, fthn, aksesUbahPPIC, aksesUbahSales, ind, j, i)

	'						' ================= STEP 3 =================
	'						Dim key = Arrbarang(i) & "|" & b & "|" & fthn

	'						' STATUS
	'						If dictStatus.ContainsKey(key) Then
	'							DataGridView1.Rows(ind).Cells(CellStatus).Value = dictStatus(key)
	'						Else
	'							DataGridView1.Rows(ind).Cells(CellStatus).Value = "NEW"
	'						End If

	'						' ================= FIX 3 (DATA) =================

	'						Dim CellSalesForecastBln As Integer =
	'							CType(Me.GetType().GetField("CellSalesForecastBln" & j,
	'							Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), Integer)

	'						Dim CellPPICForecastBln As Integer =
	'							CType(Me.GetType().GetField("CellPPICForecastBln" & j,
	'							Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), Integer)

	'						Dim CellUrut As Integer = CType(Me.GetType().GetField("CellUrut_" & j, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), String)
	'						Dim CellRV As Integer = CType(Me.GetType().GetField("CellRV_" & j, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), String)
	'						Dim CellSpace As Integer = CType(Me.GetType().GetField("CellSpace" & j, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), String)

	'						Dim keyForecast = Arrbarang(i) & "|" & b & "|" & fthn & "|" & Arrlokasi(i)

	'						If dictForecast.ContainsKey(keyForecast) Then
	'							Dim row = dictForecast(keyForecast)

	'							DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Value = Format(row("Nilai_Sales"), "N2")
	'							DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Value = Format(row("Nilai_PPIC"), "N2")

	'							DataGridView1.Rows(ind).Cells(CellUrut).Value = row("Urut")
	'							DataGridView1.Rows(ind).Cells(CellRV).Value = row("rvx")
	'							DataGridView1.Rows(ind).Cells(CellSpace).Value = ""
	'						Else
	'							DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Value = Format(0, "N2")
	'							DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Value = Format(0, "N2")

	'							DataGridView1.Rows(ind).Cells(CellUrut).Value = ""
	'							DataGridView1.Rows(ind).Cells(CellRV).Value = ""
	'							DataGridView1.Rows(ind).Cells(CellSpace).Value = ""
	'						End If

	'						Dim isValidSales As Boolean = dictValidasi.ContainsKey(key) AndAlso dictValidasi(key) = "Y"
	'						Dim isValidPPIC As Boolean = dictValidasi_ppic.ContainsKey(key) AndAlso dictValidasi_ppic(key) = "Y"

	'						If fStatus = "Transaksi_ForecastOrder_Sales" Then

	'							' PPIC
	'							If isValidPPIC Then
	'								DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.Lavender
	'							Else
	'								DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.LightCyan
	'							End If

	'							DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True

	'							' SALES
	'							If isValidSales Then
	'								DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.Lavender
	'								DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True
	'							Else
	'								If aksesUbahSales = "Y" Then
	'									DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.LightYellow
	'									DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = False
	'								Else
	'									DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.LightCyan
	'									DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True
	'								End If
	'							End If

	'						End If
	'						If fStatus = "Transaksi_ForecastOrder_PPIC" Then

	'							' SALES
	'							If isValidSales Then
	'								DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.Lavender
	'							Else
	'								DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.LightCyan
	'							End If
	'							DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True

	'							' PPIC
	'							If isValidPPIC Then
	'								' kalau dia sendiri sudah validasi → lavender + lock
	'								DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.Lavender
	'								DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True
	'							Else
	'								If aksesUbahPPIC = "Y" Then
	'									DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.LightYellow
	'									DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = False
	'								Else
	'									DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.LightCyan
	'									DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True
	'								End If
	'							End If

	'							If isValidSales Then
	'								If isValidPPIC Then
	'									DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True
	'								Else
	'									DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = False
	'								End If
	'							Else
	'								DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True
	'							End If

	'						End If

	'						'If fStatus = "Transaksi_ForecastOrder_PPIC" Then

	'						'    ' SALES (read only kalau sudah validasi)
	'						'    If dictValidasi.ContainsKey(key) AndAlso dictValidasi(key) = "Y" Then
	'						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.Lavender
	'						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True
	'						'    Else
	'						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.LightCyan
	'						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True
	'						'    End If

	'						'    ' PPIC (SELALU BOLEH EDIT kalau dia punya akses)
	'						'    If aksesUbahPPIC = "Y" Then
	'						'        DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.LightYellow
	'						'        DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = False
	'						'    Else
	'						'        DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.LightCyan
	'						'        DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True
	'						'    End If

	'						'ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then

	'						'    ' PPIC selalu read only
	'						'    DataGridView1.Rows(ind).Cells(CellPPICForecastBln).Style.BackColor = Color.LightCyan
	'						'    DataGridView1.Rows(ind).Cells(CellPPICForecastBln).ReadOnly = True

	'						'    ' SALES
	'						'    If aksesUbahSales = "Y" Then
	'						'        If dictValidasi.ContainsKey(key) AndAlso dictValidasi(key) = "Y" Then
	'						'            DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.Lavender
	'						'            DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True
	'						'        Else
	'						'            DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.LightYellow
	'						'            DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = False
	'						'        End If
	'						'    Else
	'						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln).Style.BackColor = Color.LightCyan
	'						'        DataGridView1.Rows(ind).Cells(CellSalesForecastBln).ReadOnly = True
	'						'    End If

	'						'End If
	'					End If
	'				Next

	'			Next
	'		Next
	'		DataGridView1.ResumeLayout()

	'		CloseConn()
	'	Catch ex As Exception
	'		CloseConn()
	'		MessageBox.Show(ex.Message)
	'		Exit Sub
	'	End Try

	'End Sub

	Private Sub Load_Data_Perbulan(ByVal Bln As String, ByVal Thn As String, ByVal aksesUbahPPIC As String, ByVal aksesUbahSales As String, ByVal RowIndex As Integer, ByVal bulanke As Integer, ByVal indexBarang As Integer)

		Dim CellPPICForecastBln As Integer = CType(Me.GetType().GetField("CellPPICForecastBln" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), String)
		Dim CellSalesForecastBln As Integer = CType(Me.GetType().GetField("CellSalesForecastBln" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), String)
		Dim CellUrut As Integer = CType(Me.GetType().GetField("CellUrut_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), String)
		Dim CellRV As Integer = CType(Me.GetType().GetField("CellRV_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), String)
		Dim CellSpace As Integer = CType(Me.GetType().GetField("CellSpace" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance).GetValue(Me), String)

		SQL = "Select a.Status_Data from EMI_Transaksi_Sales_Forecasting_detail a,EMI_Transaksi_Sales_Forecasting b  where a.bulan='" & Bln & "' and b.tahun ='" & Thn & "' "
		SQL = SQL & "And b.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' and "
		SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur "

		SQL = SQL & "and kode_barang = '" & Arrbarang.Item(indexBarang) & "' "
		Using dr = OpenTrans(SQL)
			If dr.Read Then
				If General_Class.CekNULL(dr("status_data")) = "" Then
					DataGridView1.Rows(RowIndex).Cells(CellStatus).Value = "UNSUBMITED"
				Else
					DataGridView1.Rows(RowIndex).Cells(CellStatus).Value = dr("Status_Data")
				End If
			Else
				DataGridView1.Rows(RowIndex).Cells(CellStatus).Value = "NEW"
			End If
		End Using

		If fStatus = "Transaksi_ForecastOrder_PPIC" Then

			SQL = "Select a.no_faktur, b.flag_validasi from EMI_Transaksi_Sales_Forecasting a,EMI_Transaksi_Sales_Forecasting_detail b  where a.bulan='" & Bln & "' and a.tahun ='" & Thn & "' "
			'      SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' and b.Flag_validasi_PPIC='Y' and kode_barang = '" & Arrbarang.Item(indexBarang) & "' "
			SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' and b.flag_validasi = 'Y'  "
			SQL = SQL & "and kode_barang = '" & Arrbarang.Item(indexBarang) & "' "
			SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur "

			Using dr = OpenTrans(SQL)
				If dr.Read Then

					DataGridView1.Rows(RowIndex).Cells(CellSalesForecastBln).Style.BackColor = Color.Lavender
				Else
					DataGridView1.Rows(RowIndex).Cells(CellSalesForecastBln).Style.BackColor = Color.LightYellow

				End If
			End Using

			If aksesUbahPPIC = "Y" Then
				'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & Bln & "' and tahun ='" & Thn & "' "
				'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and Flag_validasi_PPIC='Y' "
				SQL = "Select a.no_faktur, b.flag_validasi from EMI_Transaksi_Sales_Forecasting a,EMI_Transaksi_Sales_Forecasting_detail b  where a.bulan='" & Bln & "' and a.tahun ='" & Thn & "' "
				SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' and b.Flag_validasi_PPIC='Y' and kode_barang = '" & Arrbarang.Item(indexBarang) & "' "
				SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' and b.Flag_validasi_PPIC='Y'  "
				SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur "

				Using dr = OpenTrans(SQL)
					If dr.Read Then
						fValidasi = "Y"

						DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).ReadOnly = True
						DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).Style.BackColor = Color.Lavender
					Else

						dr.Close()
						SQL = "Select a.no_faktur, b.flag_validasi from EMI_Transaksi_Sales_Forecasting a,EMI_Transaksi_Sales_Forecasting_detail b  where a.bulan='" & Bln & "' and a.tahun ='" & Thn & "' "
						SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' and b.Flag_validasi='Y' "
						SQL = SQL & "and kode_barang = '" & Arrbarang.Item(indexBarang) & "' "
						SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur "

						Using dr2 = OpenTrans(SQL)
							If dr2.Read Then
								fValidasi = ""
								DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).ReadOnly = False
								DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).Style.BackColor = Color.Lavender
							Else
								fValidasi = "Y"
								DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).ReadOnly = True
								DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).Style.BackColor = Color.LightYellow
							End If
						End Using
					End If
				End Using
			Else
				fValidasi = ""
				DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).ReadOnly = True
				DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).Style.BackColor = Color.LightCyan
			End If

		ElseIf fStatus = "Transaksi_ForecastOrder_Sales" Then

			SQL = "Select a.no_faktur, b.flag_validasi from EMI_Transaksi_Sales_Forecasting a,EMI_Transaksi_Sales_Forecasting_detail b  where a.bulan='" & Bln & "' and a.tahun ='" & Thn & "' "
			'      SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' and b.Flag_validasi_PPIC='Y' and kode_barang = '" & Arrbarang.Item(indexBarang) & "' "
			SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' and b.Flag_validasi_PPIC = 'Y'  "
			SQL = SQL & "and kode_barang = '" & Arrbarang.Item(indexBarang) & "' "
			SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur "

			Using dr = OpenTrans(SQL)
				If dr.Read Then

					DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).Style.BackColor = Color.Lavender
				Else
					DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).Style.BackColor = Color.LightYellow

				End If
			End Using

			If aksesUbahSales = "Y" Then
				'SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & Bln & "' and tahun ='" & Thn & "' "
				'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and Flag_validasi='Y' "
				SQL = "Select a.no_faktur from EMI_Transaksi_Sales_Forecasting a,EMI_Transaksi_Sales_Forecasting_detail b  where a.bulan='" & Bln & "' and a.tahun ='" & Thn & "' "
				SQL = SQL & "And a.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' and b.Flag_validasi='Y' and kode_barang = '" & Arrbarang.Item(indexBarang) & "' "
				SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur "

				Using dr = OpenTrans(SQL)
					If dr.Read Then
						fValidasi = "Y"
						DataGridView1.Rows(RowIndex).Cells(CellSalesForecastBln).ReadOnly = True
						DataGridView1.Rows(RowIndex).Cells(CellSalesForecastBln).Style.BackColor = Color.Lavender
					Else
						fValidasi = ""
						DataGridView1.Rows(RowIndex).Cells(CellSalesForecastBln).ReadOnly = False
						DataGridView1.Rows(RowIndex).Cells(CellSalesForecastBln).Style.BackColor = Color.LightYellow
					End If
				End Using
			Else
				fValidasi = ""
				DataGridView1.Rows(RowIndex).Cells(CellSalesForecastBln).ReadOnly = True
				DataGridView1.Rows(RowIndex).Cells(CellSalesForecastBln).Style.BackColor = Color.FromArgb(245, 245, 220)
			End If
		End If

		SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Sales,Urut, cast(rv as bigint) as rvx from EMI_Transaksi_Sales_Forecasting_Detail where "
		SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & Bln & "' and tahun = '" & Thn & "' and "
		SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexBarang) & "' and Kode_Barang = '" & Arrbarang.Item(indexBarang) & "' "

		Using Ds2 = BindingTrans(SQL)
			With Ds2.Tables("MyTable")
				If .Rows.Count <> 0 Then
					DataGridView1.Rows(RowIndex).Cells(CellSalesForecastBln).Value = Format(.Rows(0).Item("Nilai_Sales"), "N2")
					DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
					DataGridView1.Rows(RowIndex).Cells(CellUrut).Value = .Rows(0).Item("Urut")
					DataGridView1.Rows(RowIndex).Cells(CellRV).Value = .Rows(0).Item("rvx")
					DataGridView1.Rows(RowIndex).Cells(CellSpace).Value = ""
				Else
					DataGridView1.Rows(RowIndex).Cells(CellSalesForecastBln).Value = Format(0, "N2")
					DataGridView1.Rows(RowIndex).Cells(CellPPICForecastBln).Value = Format(0, "N2")
					DataGridView1.Rows(RowIndex).Cells(CellUrut).Value = ""
					DataGridView1.Rows(RowIndex).Cells(CellRV).Value = ""
					DataGridView1.Rows(RowIndex).Cells(CellSpace).Value = ""
				End If
			End With
		End Using
	End Sub

End Class