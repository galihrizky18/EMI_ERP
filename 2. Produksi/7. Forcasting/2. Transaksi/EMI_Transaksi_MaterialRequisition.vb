Imports System.Reflection
Imports Microsoft.Office.Interop

'Imports Microsoft.SqlServer.Server

Public Class EMI_Transaksi_MaterialRequisition
	Public arrBulan, arrBulanMM As New ArrayList
	Dim Jenis = "Master_Jenis_Hewan"
	Public fRef As String
	Private lastHeaderColumnIndex As Integer = -1
	Dim ToolTip1 As New ToolTip


	Dim arrCellInputPPIC As New ArrayList
	Dim ListEndingStock As New List(Of (Kode_Bahan As String, Bulan As String, Jenis As String, Jumlah As Double))

	Dim Lv0 As String
	Dim LVKd_Barang As String
	Dim LvNm_Barang As String
	Dim LvAvg_3Bln As String
	Dim LvStock_BB As String
	Dim LvOPRequesition As String
	Dim LvOPOrder As String
	Dim LvTotal As String
	Dim LvNBom_1 As String
	Dim LvNPPIC_1 As String
	Dim LvUrut_1 As String
	Dim LvKosong_1 As String
	Dim LvNBom_2 As String
	Dim LvNPPIC_2 As String
	Dim LvUrut_2 As String
	Dim LvKosong_2 As String
	Dim LvNBom_3 As String
	Dim LvNPPIC_3 As String
	Dim LvUrut_3 As String
	Dim LvKosong_3 As String
	Dim LvNBom_4 As String
	Dim LvNPPIC_4 As String
	Dim LvUrut_4 As String
	Dim LvKosong_4 As String
	Dim LvNBom_5 As String
	Dim LvNPPIC_5 As String
	Dim LvUrut_5 As String
	Dim LvKosong_5 As String
	Dim LvNBom_6 As String
	Dim LvNPPIC_6 As String
	Dim LvUrut_6 As String
	Dim LvKosong_6 As String

	'Dim LvReferensi As String
	Dim LvStatus As String

	Dim LvSatuanBarang As String
	Dim LvCurrentMonth As String

	Dim LvCarryOver As String
	Dim LvRequirementStok As String

	Dim LvOPRequesition_1 As String
	Dim LvOPOrder_1 As String

	Dim LvOPRequesition_2 As String
	Dim LvOPOrder_2 As String

	Dim LvOPRequesition_3 As String
	Dim LvOPOrder_3 As String

	Dim LvOPRequesition_4 As String
	Dim LvOPOrder_4 As String

	Dim LvOPRequesition_5 As String
	Dim LvOPOrder_5 As String

	Dim LvOPRequesition_6 As String
	Dim LvOPOrder_6 As String

	Dim LvRequireStok_1 As String
	Dim LvRequireStok_2 As String

	Dim LvRequireStok_3 As String
	Dim LvRequireStok_4 As String

	Dim LvRequireStok_5 As String
	Dim LvRequireStok_6 As String
	Dim LvEndingStokSebelumPRPO As String




	Dim Cell0 As Integer = 0
	Dim CellKd_Barang As Integer = 1
	Dim CellNm_Barang As Integer = 2
	Dim CellAvg_3Bln As Integer = 3
	Dim CellCurrentMonth As Integer = 4
	Dim CellStock_BB As Integer = 5
	Dim CellOPRequesition As Integer = 6
	Dim CellOPOrder As Integer = 7
	Dim CellTotal As Integer = 8

	Dim CellNBom_1 As Integer = 9
	Dim CellNPPIC_1 As Integer = 10
	Dim CellUrut_1 As Integer = 11
	Dim CellKosong_1 As Integer = 12

	Dim CellNBom_2 As Integer = 13
	Dim CellNPPIC_2 As Integer = 14
	Dim CellUrut_2 As Integer = 15
	Dim CellKosong_2 As Integer = 16

	Dim CellNBom_3 As Integer = 17
	Dim CellNPPIC_3 As Integer = 18
	Dim CellUrut_3 As Integer = 19
	Dim CellKosong_3 As Integer = 20

	Dim CellNBom_4 As Integer = 21
	Dim CellNPPIC_4 As Integer = 22
	Dim CellUrut_4 As Integer = 23
	Dim CellKosong_4 As Integer = 24

	Dim CellNBom_5 As Integer = 25
	Dim CellNPPIC_5 As Integer = 26
	Dim CellUrut_5 As Integer = 27
	Dim CellKosong_5 As Integer = 28

	Dim CellNBom_6 As Integer = 29
	Dim CellNPPIC_6 As Integer = 30
	Dim CellUrut_6 As Integer = 31
	Dim CellKosong_6 As Integer = 32

	Dim CellStatus As Integer = 33
	Dim CellSatuanBarang As Integer = 34

	Dim cellCarryOver As Integer = 35
	Dim cellRequirmentStok As Integer = 36

	Dim CellOPRequesition_1 As Integer = 37
	Dim CellOPOrder_1 As Integer = 38

	Dim CellOPRequesition_2 As Integer = 39
	Dim CellOPOrder_2 As Integer = 40

	Dim cellRequireStok_1 As Integer = 41
	Dim cellRequireStok_2 As Integer = 42

	Dim cellOutstandingPOPlan As Integer = 43
	Dim cellOutstandingPOOrder As Integer = 44
	Dim cellFinishedPO As Integer = 45


	Dim CellOPRequesition_3 As Integer = 46
	Dim CellOPOrder_3 As Integer = 47
	Dim cellRequireStok_3 As Integer = 48

	Dim CellOPRequesition_4 As Integer = 49
	Dim CellOPOrder_4 As Integer = 50
	Dim cellRequireStok_4 As Integer = 51

	Dim CellOPRequesition_5 As Integer = 52
	Dim CellOPOrder_5 As Integer = 53
	Dim cellRequireStok_5 As Integer = 54

	Dim CellOPRequesition_6 As Integer = 55
	Dim CellOPOrder_6 As Integer = 56
	Dim cellRequireStok_6 As Integer = 57

	Dim cellEndingStokSebelumPRPO As Integer = 58




	Private dictNBom As New Dictionary(Of Integer, Integer) From {
		{1, 9},
		{2, 13},
		{3, 17},
		{4, 21},
		{5, 25},
		{6, 29}
	}

	Private dictPPIC As New Dictionary(Of Integer, Integer) From {
		{1, 10},
		{2, 14},
		{3, 18},
		{4, 22},
		{5, 26},
		{6, 30}
	}

	Private dictUrut As New Dictionary(Of Integer, Integer) From {
		{1, 11},
		{2, 15},
		{3, 19},
		{4, 23},
		{5, 27},
		{6, 31}
	}

	Private dictKosong As New Dictionary(Of Integer, Integer) From {
		{1, 12},
		{2, 16},
		{3, 20},
		{4, 24},
		{5, 28},
		{6, 32}
	}

	Private dictOPRequesition As New Dictionary(Of Integer, Integer) From {
		{1, 37},
		{2, 39},
		{3, 46},
		{4, 49},
		{5, 52},
		{6, 55}
	}

	Private dictOPOrder As New Dictionary(Of Integer, Integer) From {
		{1, 38},
		{2, 40},
		{3, 47},
		{4, 50},
		{5, 53},
		{6, 56}
	}

	Private dictRequireStok As New Dictionary(Of Integer, Integer) From {
		{1, 41},
		{2, 42},
		{3, 48},
		{4, 51},
		{5, 54},
		{6, 57}
	}

	Dim estreq_awal As Double = 0

	Public Property fstatus As String = ""

	Public Arrbarang As New ArrayList
	Public Arrlokasi As New ArrayList
	Public ArrNama As New ArrayList
	' Public ArrJenis As New ArrayList

	Private Sub FunTotalJumlahCurrentMonth(ByVal jumlahProductionPlan As Double, ByVal stok As Double, ByVal Jumlah_out_pr As Double, ByVal Jumlah_out_po As Double, ByVal index As Integer, ByVal KdBarang As String)

		Dim EstJumlahStok As Double = 0
		Dim EstJumlahPlan As Double = 0
		Dim nilaiAkhir As Double = 0



		EstJumlahStok = (Val(stok) + Val(Jumlah_out_pr) + Val(Jumlah_out_po))



		EstJumlahPlan = jumlahProductionPlan

		'nilaiAkhir = Val(jumlahProductionPlan) - Val(EstJumlahStok)


		Dim estJmlhStokSebagian = (Val(stok) - Val(EstJumlahPlan))


		DataGridView1.Rows(index).Cells(cellEndingStokSebelumPRPO).Value = Format(estJmlhStokSebagian, "N2")

		If EstJumlahStok - (EstJumlahPlan) >= 0 Then
			DataGridView1.Rows(index).Cells(cellRequirmentStok).Style.BackColor = Color.FromArgb(26, 191, 98)
		Else

			DataGridView1.Rows(index).Cells(cellRequirmentStok).Style.BackColor = Color.FromArgb(255, 204, 0)
		End If

		If estJmlhStokSebagian >= 0 Then
			DataGridView1.Rows(index).Cells(cellEndingStokSebelumPRPO).Style.BackColor = Color.FromArgb(26, 191, 98)
		Else

			DataGridView1.Rows(index).Cells(cellEndingStokSebelumPRPO).Style.BackColor = Color.FromArgb(255, 204, 0)
		End If



		DataGridView1.Rows(index).Cells(cellRequirmentStok).Value = Format(EstJumlahStok - (EstJumlahPlan), "N2")
		ListEndingStock.Add((KdBarang, (Val(HilangkanTanda(btn_bln0.Tag)) + 1).ToString("00"), "ENDING STOCK CURRENT", Val(HilangkanTanda((EstJumlahStok - EstJumlahPlan)))))

		estreq_awal = EstJumlahStok - (EstJumlahPlan)

	End Sub

	Private Sub FunTotalJumlahLooping(ByVal jumlahProductionPlan As Double, ByVal jumlah_out_pr As Double, ByVal Jumlah_out_po As Double, ByVal index As Integer, ByVal cells As Integer, ByVal KdBarang As String)

		Dim EstJumlahStok As Double = 0

		Dim nilaiAkhir As Double = 0

		EstJumlahStok = Val(Jumlah_out_po) + Val(jumlah_out_pr) + estreq_awal

		'nilaiAkhir = Val(jumlahProductionPlan) - Val(EstJumlahStok)

		If EstJumlahStok - (jumlahProductionPlan) >= 0 Then
			DataGridView1.Rows(index).Cells(cells).Style.BackColor = Color.FromArgb(26, 191, 98)
		Else
			DataGridView1.Rows(index).Cells(cells).Style.BackColor = Color.FromArgb(255, 204, 0)
		End If

		DataGridView1.Rows(index).Cells(cells).Value = Format(EstJumlahStok - jumlahProductionPlan, "N2")

		If cells = cellRequireStok_1 Then
			ListEndingStock.Add((KdBarang, (Val(HilangkanTanda(btn_bln0.Tag)) + 2).ToString("00"), "ENDING STOCK M1", Val(HilangkanTanda(EstJumlahStok - jumlahProductionPlan))))
		ElseIf cells = cellRequireStok_2 Then
			ListEndingStock.Add((KdBarang, (Val(HilangkanTanda(btn_bln0.Tag)) + 3).ToString("00"), "ENDING STOCK M2", Val(HilangkanTanda(EstJumlahStok - jumlahProductionPlan))))
		ElseIf cells = cellRequireStok_3 Then
			ListEndingStock.Add((KdBarang, (Val(HilangkanTanda(btn_bln0.Tag)) + 4).ToString("00"), "ENDING STOCK M3", Val(HilangkanTanda(EstJumlahStok - jumlahProductionPlan))))
		ElseIf cells = cellRequireStok_4 Then
			ListEndingStock.Add((KdBarang, (Val(HilangkanTanda(btn_bln0.Tag)) + 5).ToString("00"), "ENDING STOCK M4", Val(HilangkanTanda(EstJumlahStok - jumlahProductionPlan))))
		ElseIf cells = cellRequireStok_5 Then
			ListEndingStock.Add((KdBarang, (Val(HilangkanTanda(btn_bln0.Tag)) + 6).ToString("00"), "ENDING STOCK M5", Val(HilangkanTanda(EstJumlahStok - jumlahProductionPlan))))
		ElseIf cells = cellRequireStok_6 Then
			ListEndingStock.Add((KdBarang, (Val(HilangkanTanda(btn_bln0.Tag)) + 7).ToString("00"), "ENDING STOCK M6", Val(HilangkanTanda(EstJumlahStok - jumlahProductionPlan))))
		End If

		estreq_awal = EstJumlahStok - (jumlahProductionPlan)

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

	Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
		'Lv0 = DataGridView1.Rows(No_Index).Cells(Cell0).Value.ToString
		LVKd_Barang = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKd_Barang).Value)
		LvNm_Barang = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNm_Barang).Value)
		LvAvg_3Bln = CekNothing(DataGridView1.Rows(No_Index).Cells(CellAvg_3Bln).Value)
		LvCurrentMonth = CekNothing(DataGridView1.Rows(No_Index).Cells(CellCurrentMonth).Value)
		LvStock_BB = CekNothing(DataGridView1.Rows(No_Index).Cells(CellStock_BB).Value)
		LvOPRequesition = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPRequesition).Value)
		LvOPOrder = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPOrder).Value)
		LvTotal = CekNothing(DataGridView1.Rows(No_Index).Cells(CellTotal).Value)
		LvNBom_1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNBom_1).Value)
		LvNPPIC_1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNPPIC_1).Value)
		LvUrut_1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_1).Value)
		LvKosong_1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKosong_1).Value)
		LvNBom_2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNBom_2).Value)
		LvNPPIC_2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNPPIC_2).Value)
		LvUrut_2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_2).Value)
		LvKosong_2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKosong_2).Value)
		LvNBom_3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNBom_3).Value)
		LvNPPIC_3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNPPIC_3).Value)
		LvUrut_3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_3).Value)
		LvKosong_3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKosong_3).Value)
		LvNBom_4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNBom_4).Value)
		LvNPPIC_4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNPPIC_4).Value)
		LvUrut_4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_4).Value)
		LvKosong_4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKosong_4).Value)
		LvNBom_5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNBom_5).Value)
		LvNPPIC_5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNPPIC_5).Value)
		LvUrut_5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_5).Value)
		LvKosong_5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKosong_5).Value)
		LvNBom_6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNBom_6).Value)
		LvNPPIC_6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNPPIC_6).Value)
		LvUrut_6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellUrut_6).Value)
		LvKosong_6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKosong_6).Value)
		'LvReferensi = DataGridView1.Rows(No_Index).Cells(CellReferensi).Value.ToString
		LvStatus = CekNothing(DataGridView1.Rows(No_Index).Cells(CellStatus).Value)
		LvSatuanBarang = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSatuanBarang).Value)

		LvCarryOver = CekNothing(DataGridView1.Rows(No_Index).Cells(cellCarryOver).Value)
		LvRequirementStok = CekNothing(DataGridView1.Rows(No_Index).Cells(cellRequirmentStok).Value)

		LvOPRequesition_1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPRequesition_1).Value)
		LvOPOrder_1 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPOrder_1).Value)
		LvRequireStok_1 = CekNothing(DataGridView1.Rows(No_Index).Cells(cellRequireStok_1).Value)

		LvOPRequesition_2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPRequesition_2).Value)
		LvOPOrder_2 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPOrder_2).Value)
		LvRequireStok_2 = CekNothing(DataGridView1.Rows(No_Index).Cells(cellRequireStok_2).Value)

		'3
		LvOPRequesition_3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPRequesition_3).Value)
		LvOPOrder_3 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPOrder_3).Value)
		LvRequireStok_3 = CekNothing(DataGridView1.Rows(No_Index).Cells(cellRequireStok_3).Value)

		'4
		LvOPRequesition_4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPRequesition_4).Value)
		LvOPOrder_4 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPOrder_4).Value)
		LvRequireStok_4 = CekNothing(DataGridView1.Rows(No_Index).Cells(cellRequireStok_4).Value)

		'5
		LvOPRequesition_5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPRequesition_5).Value)
		LvOPOrder_5 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPOrder_5).Value)
		LvRequireStok_5 = CekNothing(DataGridView1.Rows(No_Index).Cells(cellRequireStok_5).Value)

		'6
		LvOPRequesition_6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPRequesition_6).Value)
		LvOPOrder_6 = CekNothing(DataGridView1.Rows(No_Index).Cells(CellOPOrder_6).Value)
		LvRequireStok_6 = CekNothing(DataGridView1.Rows(No_Index).Cells(cellRequireStok_6).Value)

		LvEndingStokSebelumPRPO = CekNothing(DataGridView1.Rows(No_Index).Cells(cellEndingStokSebelumPRPO).Value)
	End Sub

	Private Sub getdata()

		DataGridView1.Rows.Clear()

		DataGridView1.Columns(Cell0).HeaderText = "#"
		DataGridView1.Columns(CellKd_Barang).HeaderText = "Kode Barang"
		DataGridView1.Columns(CellNm_Barang).HeaderText = "Nama Barang"
		DataGridView1.Columns(CellAvg_3Bln).HeaderText = "Avg 3 Bulan (Pcs)"
		DataGridView1.Columns(CellCurrentMonth).HeaderText = "Current Month"
		DataGridView1.Columns(CellStock_BB).HeaderText = "Stok Bahan Baku"
		DataGridView1.Columns(CellOPRequesition).HeaderText = "Open Purchase Requsition"
		DataGridView1.Columns(CellOPOrder).HeaderText = "Open Purchase Order"
		DataGridView1.Columns(CellTotal).HeaderText = "Total Stock + Open PR + Open PO"

		Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
		Dim fthn As Integer = Val(ComboBox2.Text)
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
				'ComboBox1.SelectedIndex = index
				b = ComboBox1.Items(index)
				panggil_databulan = arrBulanMM.Item(index)
			End If
		Next
		panggil_datatahun = fthn
		DataGridView1.Columns(CellNBom_1).HeaderText = "MRP - Production Plan " & b & " - " & fthn
		'   DataGridView1.Columns(CellNPPIC_1).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellUrut_1).HeaderText = "1"
		DataGridView1.Columns(CellKosong_1).HeaderText = ""

		If a = 12 Then
			a = 1
			fthn = fthn + 1
		Else
			a = a + 1
		End If

		For index = 0 To arrBulan.Count - 1
			If arrBulan.Item(index) = a Then
				'ComboBox1.SelectedIndex = index
				b = ComboBox1.Items(index)
			End If
		Next
		DataGridView1.Columns(CellNBom_2).HeaderText = "MRP - Production Plan " & b & " - " & fthn
		'   DataGridView1.Columns(CellNPPIC_2).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		DataGridView1.Columns(CellUrut_2).HeaderText = "2"
		DataGridView1.Columns(CellKosong_2).HeaderText = ""

		If a = 12 Then
			a = 1
			fthn = fthn + 1
		Else
			a = a + 1
		End If

		For index = 0 To arrBulan.Count - 1
			If arrBulan.Item(index) = a Then
				'ComboBox1.SelectedIndex = index
				b = ComboBox1.Items(index)
			End If
		Next
		'DataGridView1.Columns(CellNBom_3).HeaderText = "BoM - Production Plan " & b & " - " & fthn
		'DataGridView1.Columns(CellNPPIC_3).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		'DataGridView1.Columns(CellUrut_3).HeaderText = "3"
		'DataGridView1.Columns(CellKosong_3).HeaderText = ""
		'If a = 12 Then
		'    a = 1
		'    fthn = fthn + 1
		'Else
		'    a = a + 1
		'End If

		'For index = 0 To arrBulan.Count - 1
		'    If arrBulan.Item(index) = a Then
		'        'ComboBox1.SelectedIndex = index
		'        b = ComboBox1.Items(index)
		'    End If
		'Next
		'DataGridView1.Columns(CellNBom_4).HeaderText = "BoM - Production Plan " & b & " - " & fthn
		'DataGridView1.Columns(CellNPPIC_4).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		'DataGridView1.Columns(CellUrut_4).HeaderText = "4"
		'DataGridView1.Columns(CellKosong_4).HeaderText = ""
		'If a = 12 Then
		'    a = 1
		'    fthn = fthn + 1
		'Else
		'    a = a + 1
		'End If

		'For index = 0 To arrBulan.Count - 1
		'    If arrBulan.Item(index) = a Then
		'        'ComboBox1.SelectedIndex = index
		'        b = ComboBox1.Items(index)
		'    End If
		'Next
		'DataGridView1.Columns(CellNBom_5).HeaderText = "BoM - Production Plan " & b & " - " & fthn
		'DataGridView1.Columns(CellNPPIC_5).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		'DataGridView1.Columns(CellUrut_5).HeaderText = "5"
		'DataGridView1.Columns(CellKosong_5).HeaderText = ""
		'If a = 12 Then
		'    a = 1
		'    fthn = fthn + 1
		'Else
		'    a = a + 1
		'End If

		'For index = 0 To arrBulan.Count - 1
		'    If arrBulan.Item(index) = a Then
		'        'ComboBox1.SelectedIndex = index
		'        b = ComboBox1.Items(index)
		'    End If
		'Next
		'DataGridView1.Columns(CellNBom_6).HeaderText = "BoM - Production Plan " & b & " - " & fthn
		'DataGridView1.Columns(CellNPPIC_6).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
		'DataGridView1.Columns(CellUrut_6).HeaderText = "6"
		'DataGridView1.Columns(CellKosong_6).HeaderText = ""

		DataGridView1.Columns(CellStatus).HeaderText = "Status"

		Dim fLoad As Boolean = False
		Try
			OpenConn()

			SQL = "select No_Faktur from EMI_Transaksi_Material_Requsition where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "Lokasi = '" & ComboBox3.Text & "' and Bulan = '" & arrBulanMM.Item(ComboBox1.SelectedIndex) & "' and Tahun = '" & ComboBox2.Text & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					TxtBarangMasuk_NoFaktur.Text = Dr("No_Faktur")
					fLoad = True
					'Btn_Realese.Enabled = fa
				Else
					fLoad = False
					' Btn_Realese.Enabled = False
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If fLoad = True Then
			TxtBarangMasuk_NoFaktur_Leave(ComboBox2, Nothing)
		Else

			Try
				OpenConn()

				get_no_faktur()

				Arrbarang.Clear()
				Arrlokasi.Clear()
				ArrNama.Clear()
				'SQL = "select  e.Kode_Stock_Owner,e.Kode_Barang,c.Nama from EMI_Transaksi_Sales_Forecasting a,  "
				'SQL = SQL & "EMI_Transaksi_Sales_Forecasting_Detail b,barang c, Emi_Transaksi_Formulator d, EMI_Transaksi_Formulator_Detail_Bahan e "
				'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur  "
				'SQL = SQL & "and e.Kode_Perusahaan = c.Kode_Perusahaan and e.Kode_Stock_Owner = c.Kode_Stock_Owner and e.Kode_Barang = c.Kode_Barang "
				'SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Formula = d.No_Faktur "
				'SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.No_Faktur = e.No_Faktur "
				'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
				'SQL = SQL & "and a.lokasi = '" & ComboBox3.Text & "' "
				'SQL = SQL & "and b.bulan = '" & panggil_databulan & "' and b.Tahun = '" & panggil_datatahun & "' "
				'SQL = SQL & "and b.Flag_Validasi = 'Y' and b.Flag_Validasi_PPIC = 'Y'  "
				'SQL = SQL & "group by e.Kode_Stock_Owner,e.Kode_Barang,c.Nama "

				'SQL = SQL & "Union all "

				'SQL = SQL & "Select d.Kode_Stock_Owner,c.Kode_Bahan As Kode_Barang,d.Nama from "
				'SQL = SQL & "EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_Detail b, barang_detail_bahan_penolong c, barang d, barang e "
				'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And b.Kode_Perusahaan = c.Kode_Perusahaan "
				'SQL = SQL & "And b.Kode_Perusahaan=e.Kode_Perusahaan And b.Kode_barang=e.Kode_Barang And b.kode_stock_owner=e.kode_stock_owner "
				'SQL = SQL & "And e.Kode_Barang_inq = c.Kode_Barang And c.Kode_Perusahaan = d.Kode_Perusahaan "
				'SQL = SQL & " And c.Kode_Bahan = d.Kode_Barang And b.Kode_Stock_Owner = d.Kode_Stock_Owner And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'SQL = SQL & "And a.lokasi = '" & ComboBox3.Text & "' and b.bulan = '" & panggil_databulan & "' and b.Tahun = '" & panggil_datatahun & "' "
				'SQL = SQL & "and b.Flag_Validasi = 'Y' and b.Flag_Validasi_PPIC = 'Y'  "
				'SQL = SQL & "group by d.Kode_Stock_Owner, c.Kode_Bahan, d.Nama "

				'SQL = SQL & "order by nama "

				SQL = $"
					With cte As (

					  SELECT DISTINCT ISNULL(f.No_Faktur,'') AS kode_formula,

					x.kode_barang_inq,x.kode_perusahaan  FROM barang x  INNER JOIN emI_group_jenis y ON x.kode_perusahaan=y.kode_perusahaan AND x.id_group_jenis=y.id_group_jenis
					OUTER APPLY (
						SELECT TOP 1 c.No_Faktur
						FROM N_EMI_Transaksi_Formulator_Binding a
						INNER JOIN N_EMI_Transaksi_Formulator_Binding_Detail b
							ON a.Kode_Perusahaan=b.Kode_Perusahaan
							AND a.No_Faktur=b.No_Faktur
						INNER JOIN Emi_Transaksi_Formulator c
							ON b.Kode_Perusahaan=c.Kode_Perusahaan
							AND b.No_Formulator=c.No_Faktur
							AND c.Status IS NULL
						WHERE a.Status IS NULL
							AND a.Flag_Validasi_Main='Y'
							AND a.Kode_Perusahaan=x.kode_perusahaan
							AND a.Kode_Barang=x.kode_barang_inq
						ORDER BY a.Tanggal DESC,a.Jam DESC,b.No_Prioritas ASC
					) f
					WHERE (y.Flag_Finished_Good='Y' OR y.Flag_Semi_FG='Y')

					), cte_b as(

					Select Case e.Kode_Stock_Owner,e.Kode_Barang,c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis
					From EMI_Transaksi_Sales_Forecasting a
					INNER Join EMI_Transaksi_Sales_Forecasting_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur
					INNER Join barang bb ON b.kode_perusahaan=bb.kode_perusahaan And b.kode_barang=bb.kode_barang And b.kode_stock_owner=bb.kode_stock_owner
					INNER Join cte bc ON bb.kode_perusahaan=bc.kode_perusahaan And bb.kode_barang_inq=bc.kode_barang_inq
					INNER Join Emi_Transaksi_Formulator d ON d.Kode_Perusahaan = bc.Kode_Perusahaan And d.No_Faktur =  bc.Kode_Formula
					INNER Join EMI_Transaksi_Formulator_Detail_Bahan e ON d.Kode_Perusahaan = e.Kode_Perusahaan And d.No_Faktur = e.No_Faktur
					INNER Join barang c ON e.Kode_Perusahaan = c.Kode_Perusahaan And e.Kode_Stock_Owner = c.Kode_Stock_Owner And e.Kode_Barang = c.Kode_Barang
					WHERE a.Kode_Perusahaan ='{KodePerusahaan}' AND a.lokasi= '{ComboBox3.Text}' AND  b.bulan = '{panggil_databulan}' and b.Tahun = '{panggil_datatahun}'
					And b.Flag_Validasi ='Y'
					And b.Flag_Validasi_PPIC ='Y'
					GROUP BY e.Kode_Stock_Owner, e.Kode_Barang, c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis

					Union all

					Select Case e.Kode_Stock_Owner,e.Kode_Barang,c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis
					From EMI_Transaksi_Sales_Forecasting a
					INNER Join EMI_Transaksi_Sales_Forecasting_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur
					INNER Join Emi_Transaksi_Formulator d ON d.Kode_Perusahaan = b.Kode_Perusahaan
					INNER Join EMI_Transaksi_Formulator_Detail_Bahan e ON d.Kode_Perusahaan = e.Kode_Perusahaan And d.No_Faktur = e.No_Faktur
					INNER Join N_EMI_Production_Plan_Schedule_Detail f on b.kode_perusahaan =  f.kode_perusahaan And b.urut = f.urut_production_plan And f.kode_formula = d.no_faktur
					INNER Join N_EMI_Production_Plan_Schedule g on f.kode_perusahaan = g.kode_perusahaan And f.no_transaksi = g.no_transaksi And g.status Is null
					INNER Join barang c ON e.Kode_Perusahaan = c.Kode_Perusahaan And e.Kode_Stock_Owner = c.Kode_Stock_Owner And e.Kode_Barang = c.Kode_Barang
					WHERE a.Kode_Perusahaan ='{KodePerusahaan}' AND a.lokasi= '{ComboBox3.Text}' AND  b.bulan = '{panggil_databulan}' and b.Tahun = '{panggil_datatahun}'
					And b.Flag_Validasi ='Y'
					And b.Flag_Validasi_PPIC ='Y'
					GROUP BY e.Kode_Stock_Owner, e.Kode_Barang, c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis

					)
					Select distinct a.Kode_Stock_Owner,a.Kode_Barang,a.Nama from cte_b a, EMI_Group_Jenis b where a.kode_perusahaan=b.Kode_Perusahaan And a.id_group_jenis=b.Id_Group_Jenis
					And b.Flag_Raw_Material='Y'
"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows().Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								Arrbarang.Add(.Rows(i).Item("kode_barang"))
								Arrlokasi.Add(.Rows(i).Item("kode_stock_owner"))
								ArrNama.Add(.Rows(i).Item("nama"))
							Next
						Else
							CloseConn()
							MessageBox.Show("Tidak ada data forecasting pada bulan " & ComboBox1.Text & " " & ComboBox2.Text)
							Exit Sub
						End If
					End With
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			get_barang()

		End If
	End Sub

	Public Sub get_barang()

		Dim akses_ubah As String = "MAUK SINNI DAK"

		Try
			OpenConn()

			If CekButtonRole("MRP_PPIC") = "Y" Then
				akses_ubah = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()
			DataGridView1.Rows.Add(1)
			For indexxx = 0 To Arrbarang.Count - 1

				Dim ind As Integer = DataGridView1.Rows.Count - 1

				'DataGridView1.Rows(ind).Cells(CellNBom_1).Style.BackColor = Color.LightYellow
				'DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.LightCyan
				'DataGridView1.Rows(ind).Cells(CellKosong_1).Style.BackColor = Color.LightGray
				'DataGridView1.Rows(ind).Cells(CellNBom_2).Style.BackColor = Color.LightYellow
				'DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.LightCyan
				'DataGridView1.Rows(ind).Cells(CellKosong_2).Style.BackColor = Color.LightGray
				'DataGridView1.Rows(ind).Cells(CellNBom_3).Style.BackColor = Color.LightYellow
				'DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.LightCyan
				'DataGridView1.Rows(ind).Cells(CellKosong_3).Style.BackColor = Color.LightGray
				'DataGridView1.Rows(ind).Cells(CellNBom_4).Style.BackColor = Color.LightYellow
				'DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.LightCyan
				'DataGridView1.Rows(ind).Cells(CellKosong_4).Style.BackColor = Color.LightGray
				'DataGridView1.Rows(ind).Cells(CellNBom_5).Style.BackColor = Color.LightYellow
				'DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.LightCyan
				'DataGridView1.Rows(ind).Cells(CellKosong_5).Style.BackColor = Color.LightGray
				'DataGridView1.Rows(ind).Cells(CellNBom_6).Style.BackColor = Color.LightYellow
				'DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.LightCyan
				'DataGridView1.Rows(ind).Cells(CellKosong_6).Style.BackColor = Color.LightGray
				'DataGridView1.Rows(ind).Cells(CellStatus).Style.BackColor = Color.Yellow

				DataGridView1.Rows(ind).Cells(CellNBom_1).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNBom_2).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNBom_3).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNBom_4).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNBom_5).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNBom_6).ReadOnly = True

				DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = True

				Dim satuan_barang As String = ""
				Dim good_stock As Double = 0
				Dim Flag_Packaging As String = ""
				Dim Flag_Raw_Material As String = ""

				Dim ada_data As String = ""
				Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
				Dim fthn As Integer = Val(ComboBox2.Text)
				Dim b As String = ""
				Dim FValidasi As String = ""
				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						'ComboBox1.SelectedIndex = index
						b = arrBulanMM.Item(index)
					End If
				Next

				SQL = "select a.satuan, a.good_stock, b.Flag_raw_material, b.flag_packaging from barang a, emi_group_jenis b "
				SQL = SQL & "where a.kode_Barang='" & Arrbarang.Item(indexxx) & "' and kode_stock_owner='" & Arrlokasi.Item(indexxx) & "' "
				SQL = SQL & "And a.kode_Perusahaan ='" & KodePerusahaan & "' and a.id_group_jenis=b.id_group_jenis and a.Kode_Perusahaan=b.kode_perusahaan "
				Using dr5 = OpenTrans(SQL)
					If dr5.Read Then
						satuan_barang = dr5("satuan")
						good_stock = dr5("good_stock")
						Flag_Packaging = dr5("flag_packaging")
						Flag_Raw_Material = dr5("Flag_raw_material")
					Else
						dr5.Close()
						CloseConn()
						MessageBox.Show("data tidak ada")
						Exit Sub
					End If
				End Using

				'sedang di edit
				Dim convertKesatuanDisplay As String = ""
				Dim good_stock_tampil_display As Double = 0

				SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & Arrbarang.Item(indexxx) & "' "
				SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
				Using Dr3 = OpenTrans(SQL)
					If Dr3.Read Then
						convertKesatuanDisplay = Dr3("satuan")
						SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Arrbarang.Item(indexxx) & "',"
						SQL = SQL & "'" & satuan_barang & "','" & Dr3("satuan") & "',"
						SQL = SQL & "" & good_stock & ") as Hasil "
						Dr3.Close()

						Using dr4 = OpenTrans(SQL)
							If dr4.Read Then
								If General_Class.CekNULL(dr4("Hasil")) <> "" Then
									If dr4("Hasil") = 0 Then
										good_stock_tampil_display = 0
									Else
										good_stock_tampil_display = dr4("hasil")

									End If
								Else
									dr4.Close()
									CloseConn()
									MessageBox.Show("Satuan " & satuan_barang & " Ke " & convertKesatuanDisplay & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									kosong()
									Exit Sub
								End If
							End If
						End Using
					Else
						Dr3.Close()
						CloseConn()
						MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						kosong()
						Exit Sub
					End If
				End Using

				DataGridView1.Rows(ind).Cells(CellKd_Barang).Value = Arrbarang.Item(indexxx)
				DataGridView1.Rows(ind).Cells(CellNm_Barang).Value = ArrNama.Item(indexxx)
				DataGridView1.Rows(ind).Cells(CellAvg_3Bln).Value = "0"
				DataGridView1.Rows(ind).Cells(CellStock_BB).Value = Format(good_stock_tampil_display, "N2")
				DataGridView1.Rows(ind).Cells(CellSatuanBarang).Value = convertKesatuanDisplay

				'=============================== tampil data PR belum PO ==============================='

				Dim totalPrBelumPOPerbarang As Double = 0
				Dim convertSatuanDisplayPr As String = ""
				'---------select ke data pr berdasarkan kode_barang
				'SQL = "select b.Kode_Stock_Owner, b.Kode_Barang, b.Satuan, "

				'SQL = SQL & "b.jumlah-isnull((Select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
				'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur "
				'SQL = SQL & "And y.Kode_Perusahaan = b.Kode_Perusahaan And y.no_urut_pr = b.No_Urut And x.status Is null), "
				'SQL = SQL & "0) As jumlah "

				'SQL = SQL & "from EMI_Purchase_Requisition a, EMI_Purchase_Requisition_Detail b "
				'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
				'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null  "
				'SQL = SQL & "and b.kode_stock_owner = '" & Arrlokasi.Item(indexxx) & "' "
				'SQL = SQL & "and b.kode_barang = '" & Arrbarang.Item(indexxx) & "' "
				'SQL = SQL & "and MONTH(b.tanggal_delivery) = '" & b & "' "
				'SQL = SQL & "and YEAR(b.tanggal_delivery) = '" & fthn & "' "

				SQL = "select a.Kode_Perusahaan,b.kode_stock_owner,b.Kode_Barang,c.Nama,isnull(sum(b.Jumlah),0) as Jumlah, b.Satuan From EMI_Purchase_Requisition a "
				SQL = SQL & "inner join  EMI_Purchase_Requisition_Detail b  on "
				SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
				SQL = SQL & "inner join barang c on  "
				SQL = SQL & "b.kode_perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
				SQL = SQL & "where  a.Flag_Release = 'Y' and b.Flag_Sudah_PO is null and a.Status is null and "
				SQL = SQL & "(MONTH(Tanggal_Delivery) = '" & b & "' and YEAR(Tanggal_Delivery) = '" & fthn & "' or MONTH(Tanggal_Delivery) < " & b & " and YEAR(Tanggal_Delivery) < " & fthn & ") "
				SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "'  and b.kode_stock_owner = '" & Arrlokasi.Item(indexxx) & "' "
				SQL = SQL & "and b.kode_barang = '" & Arrbarang.Item(indexxx) & "' "
				SQL = SQL & "group by a.Kode_Perusahaan,b.Kode_Barang,c.Nama,b.Satuan "

				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For indexPR As Integer = 0 To .Rows.Count - 1

								'ambil satuan barang yang akan ditampilkan ke display
								SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexPR).Item("kode_barang") & "' "
								SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
								OpenConn()

								Using Dr3 = OpenTrans(SQL)
									If Dr3.Read Then
										convertSatuanDisplayPr = Dr3("satuan")
										SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexPR).Item("kode_barang") & "',"
										SQL = SQL & "'" & .Rows(indexPR).Item("satuan") & "','" & Dr3("satuan") & "',"
										SQL = SQL & "" & .Rows(indexPR).Item("jumlah") & ") as Hasil "
										Dr3.Close()

										Using dr4 = OpenTrans(SQL)
											If dr4.Read Then
												If General_Class.CekNULL(dr4("Hasil")) <> "" Then
													'If dr4("Hasil") = 0 Then
													'    MessageBox.Show("Satuan " & .Rows(indexPR).Item("satuan") & " Ke " & convertSatuanDisplayPr & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													'    kosong()
													'    Exit Sub
													'Else
													totalPrBelumPOPerbarang = totalPrBelumPOPerbarang + dr4("hasil")
													DataGridView1.Rows(ind).Cells(CellOPRequesition).Value = Format(totalPrBelumPOPerbarang, "N2")
													'End If
												Else
													dr4.Close()
													CloseConn()
													MessageBox.Show("Satuan " & .Rows(indexPR).Item("satuan") & " Ke " & convertSatuanDisplayPr & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													kosong()
													Exit Sub
												End If
											End If
										End Using
									Else
										Dr3.Close()
										CloseConn()
										MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										kosong()
										Exit Sub
									End If
								End Using

							Next
						Else
							'jika belum  ada pr maka 0
							DataGridView1.Rows(ind).Cells(CellOPRequesition).Value = 0
						End If
					End With
				End Using

				'=========================================== tampil PR sudah po =========================================='
				Dim totalPRSudahPO As Double = 0
				Dim convertSatuanPRSudahPO As String = ""
				'---------select ke data pr berdasarkan kode_barang
				SQL = "select b.Kode_Stock_Owner,b.Kode_Barang,b.Satuan,b.jumlah "
				SQL = SQL & "from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b "
				SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
				SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null  "
				SQL = SQL & "and b.kode_stock_owner = '" & Arrlokasi.Item(indexxx) & "' "
				SQL = SQL & "and b.kode_barang = '" & Arrbarang.Item(indexxx) & "' "
				SQL = SQL & "and MONTH(a.eta) = '" & b & "' "
				SQL = SQL & "and YEAR(a.eta) = '" & fthn & "' "
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For indexPR As Integer = 0 To .Rows.Count - 1

								'ambil satuan barang yang akan ditampilkan ke display
								SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & .Rows(indexPR).Item("kode_barang") & "' "
								SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
								OpenConn()
								Using Dr3 = OpenTrans(SQL)
									If Dr3.Read Then
										convertSatuanPRSudahPO = Dr3("satuan")
										SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(indexPR).Item("kode_barang") & "',"
										SQL = SQL & "'" & .Rows(indexPR).Item("satuan") & "','" & Dr3("satuan") & "',"
										SQL = SQL & "" & .Rows(indexPR).Item("jumlah") & ") as Hasil "
										Dr3.Close()

										Using dr4 = OpenTrans(SQL)
											If dr4.Read Then
												If General_Class.CekNULL(dr4("Hasil")) <> "" Then
													'If dr4("Hasil") = 0 Then
													'    MessageBox.Show("Satuan " & .Rows(indexPR).Item("satuan") & " Ke " & convertSatuanDisplayPr & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													'    kosong()
													'    Exit Sub
													'Else
													totalPRSudahPO = totalPRSudahPO + dr4("hasil")
													DataGridView1.Rows(ind).Cells(CellOPOrder).Value = Format(totalPRSudahPO, "N2")
													'End If
												Else
													dr4.Close()
													CloseConn()
													MessageBox.Show("Satuan " & .Rows(indexPR).Item("satuan") & " Ke " & convertSatuanDisplayPr & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													kosong()
													Exit Sub
												End If
											End If
										End Using
									Else
										Dr3.Close()
										CloseConn()
										MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										kosong()
										Exit Sub
									End If
								End Using

							Next
						Else
							'jika belum  ada pr maka 0
							DataGridView1.Rows(ind).Cells(CellOPOrder).Value = 0
						End If
					End With
				End Using

				'  DataGridView1.Rows(ind).Cells(CellOPOrder).Value = "0"
				DataGridView1.Rows(ind).Cells(CellTotal).Value = Format(totalPrBelumPOPerbarang + totalPRSudahPO + good_stock_tampil_display, "N2")

				'Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
				'Dim fthn As Integer = Val(ComboBox2.Text)
				'Dim b As String = ""
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

				'BULAN KE 1

				If fstatus = "MRP_PPIC" Then

					If akses_ubah = "Y" Then
						SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
						SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								FValidasi = "Y"
								DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = True
								DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.DarkCyan
							Else

								dr.Close()
								'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
								'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
								'Using dr2 = OpenTrans(SQL)
								'    If dr2.Read Then
								'        FValidasi = ""
								'        DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = False
								'        DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.LightCyan
								'    Else
								FValidasi = ""
								DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = False
								DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.LightCyan
								' End If
								'End Using
							End If
						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.DarkCyan
					End If

				ElseIf fstatus = "MRP_Formulator" Then
					SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
					SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							FValidasi = "Y"
							' DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = False
							DataGridView1.Rows(ind).Cells(CellNBom_1).Style.BackColor = Color.DarkGoldenrod
						Else
							FValidasi = ""
							'DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = False
							DataGridView1.Rows(ind).Cells(CellNBom_1).Style.BackColor = Color.LightYellow
						End If
					End Using
				End If

				ada_data = ""
				SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC, Nilai_Bom, Urut from EMI_Transaksi_Material_Requsition_Detail where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")
						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellNBom_1).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
							'    DataGridView1.Rows(ind).Cells(CellNPPIC_1).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_1).Value = .Rows(0).Item("Urut")
							DataGridView1.Rows(ind).Cells(CellKosong_1).Value = ""
							ada_data = "T"
						Else
							DataGridView1.Rows(ind).Cells(CellNBom_1).Value = 0
							'   DataGridView1.Rows(ind).Cells(CellNPPIC_1).Value = 0
							DataGridView1.Rows(ind).Cells(CellUrut_1).Value = ""
							DataGridView1.Rows(ind).Cells(CellKosong_1).Value = ""
							ada_data = "T"
						End If
					End With
				End Using

				'BULAN KE 1 ngitung bahan yang dibutuhkan
				'ambil barang untuk bulan dan tahun

				If ada_data = "T" Then

					If Flag_Raw_Material = "Y" Then
						SQL = ";with cte as ( "
						SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
						SQL = SQL & "b.nilai_ppic*bb.Berat/1000 as nilai_ppic, "
						SQL = SQL & "c.hasil as nilai_Formula, "
						SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
						SQL = SQL & "from "
						SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
						SQL = SQL & "barang bb, "
						SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
						SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
						SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
						SQL = SQL & "and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y'  "
						SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
						SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
						SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
						SQL = SQL & "b.kode_perusahaan=bb.kode_perusahaan and b.kode_barang=bb.kode_barang and b.kode_stock_owner=bb.kode_stock_owner "
						SQL = SQL & ") "
						SQL = SQL & "select Kode_Bahan, satuan_barang,isnull(sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)),0) as Nilai from cte "
						SQL = SQL & "group by Kode_Bahan, satuan_barang "

						Using ds3 = BindingTrans(SQL)

							If ds3.Tables("MyTable").Rows.Count <> 0 Then
								For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

									Dim jumlah As Double = 0

									jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

									Dim convertKeSatuanAsli As String = ""
									Dim jumlahBarangDibutuhkan As Double = 0

									SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
									SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
									Using Dr3 = OpenTrans(SQL)
										If Dr3.Read Then
											convertKeSatuanAsli = Dr3("satuan")
											SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
											SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
											SQL = SQL & "" & jumlah & ") as Hasil "
											Dr3.Close()

											Using dr4 = OpenTrans(SQL)
												If dr4.Read Then
													If General_Class.CekNULL(dr4("Hasil")) <> "" Then

														If dr4("hasil") = 0 Then
															DataGridView1.Rows(ind).Cells(CellNBom_1).Value = 0
														Else
															DataGridView1.Rows(ind).Cells(CellNBom_1).Value = Format(dr4("hasil"), "N2")
														End If
													Else
														dr4.Close()
														CloseConn()
														MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If
												End If
											End Using
										Else
											Dr3.Close()
											CloseConn()
											MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using
								Next
							Else
								DataGridView1.Rows(ind).Cells(CellNBom_1).Value = 0

							End If

						End Using

					ElseIf Flag_Packaging = "Y" Then
						SQL = ";with cte as ( "
						SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
						SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
						SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
						SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
						SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
						SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y' and "
						SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
						SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
						SQL = SQL & ") "
						SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
						SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
						Using ds3 = BindingTrans(SQL)

							If ds3.Tables("MyTable").Rows.Count <> 0 Then
								For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

									Dim jumlah As Double = 0

									jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

									Dim convertKeSatuanAsli As String = ""
									Dim jumlahBarangDibutuhkan As Double = 0

									SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
									SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
									Using Dr3 = OpenTrans(SQL)
										If Dr3.Read Then
											convertKeSatuanAsli = Dr3("satuan")
											SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
											SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
											SQL = SQL & "" & jumlah & ") as Hasil "
											Dr3.Close()

											Using dr4 = OpenTrans(SQL)
												If dr4.Read Then
													If General_Class.CekNULL(dr4("Hasil")) <> "" Then

														If dr4("hasil") = 0 Then
															DataGridView1.Rows(ind).Cells(CellNBom_1).Value = 0
														Else
															DataGridView1.Rows(ind).Cells(CellNBom_1).Value = Format(dr4("hasil"), "N2")
														End If
													Else
														dr4.Close()
														CloseConn()
														MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If
												End If
											End Using
										Else
											Dr3.Close()
											CloseConn()
											MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using
								Next
							Else
								DataGridView1.Rows(ind).Cells(CellNBom_1).Value = 0

							End If

						End Using
					End If

				End If

				'============ akhir bulan 1

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

				'BULAN KE 2

				If fstatus = "MRP_PPIC" Then

					If akses_ubah = "Y" Then
						SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
						SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								FValidasi = "Y"
								DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = True
								DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.DarkCyan
							Else

								dr.Close()
								'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
								'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
								'Using dr2 = OpenTrans(SQL)
								'    If dr2.Read Then
								'        FValidasi = ""
								'        DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = False
								'        DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.LightCyan
								'    Else
								FValidasi = ""
								DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = False
								DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.LightCyan
								'    End If
								'End Using
							End If
						End Using
					Else
						FValidasi = ""
						DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = True
						DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.DarkCyan
					End If

				ElseIf fstatus = "MRP_Formulator" Then
					SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
					SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							FValidasi = "Y"

							DataGridView1.Rows(ind).Cells(CellNBom_2).Style.BackColor = Color.DarkGoldenrod
						Else
							FValidasi = ""

							DataGridView1.Rows(ind).Cells(CellNBom_2).Style.BackColor = Color.LightYellow
						End If
					End Using
				End If

				ada_data = ""
				SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
				Using Ds2 = BindingTrans(SQL)
					With Ds2.Tables("MyTable")

						If .Rows.Count <> 0 Then
							DataGridView1.Rows(ind).Cells(CellNBom_2).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
							DataGridView1.Rows(ind).Cells(CellNPPIC_2).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
							DataGridView1.Rows(ind).Cells(CellUrut_2).Value = .Rows(0).Item("Urut")
							DataGridView1.Rows(ind).Cells(CellKosong_2).Value = ""
							ada_data = "T"
						Else
							'  DataGridView1.Rows(ind).Cells(CellNBom_2).Value = 0
							DataGridView1.Rows(ind).Cells(CellNPPIC_2).Value = 0
							DataGridView1.Rows(ind).Cells(CellUrut_2).Value = ""
							DataGridView1.Rows(ind).Cells(CellKosong_2).Value = ""
							ada_data = "T"
						End If
					End With
				End Using

				If ada_data = "T" Then

					If Flag_Raw_Material = "Y" Then
						SQL = ";with cte as ( "
						SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
						SQL = SQL & "b.nilai_ppic*bb.Berat/1000 as nilai_ppic, "
						SQL = SQL & "c.hasil as nilai_Formula, "
						SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
						SQL = SQL & "from "
						SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
						SQL = SQL & "barang bb, "
						SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
						SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
						SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
						SQL = SQL & "and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y'  "
						SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
						SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
						SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
						SQL = SQL & "b.kode_perusahaan=bb.kode_perusahaan and b.kode_barang=bb.kode_barang and b.kode_stock_owner=bb.kode_stock_owner "
						SQL = SQL & ") "
						SQL = SQL & "select Kode_Bahan, satuan_barang,isnull(sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)),0) as Nilai from cte "
						SQL = SQL & "group by Kode_Bahan, satuan_barang "

						Using ds3 = BindingTrans(SQL)

							If ds3.Tables("MyTable").Rows.Count <> 0 Then
								For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

									Dim jumlah As Double = 0

									jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

									Dim convertKeSatuanAsli As String = ""
									Dim jumlahBarangDibutuhkan As Double = 0

									SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
									SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
									Using Dr3 = OpenTrans(SQL)
										If Dr3.Read Then
											convertKeSatuanAsli = Dr3("satuan")
											SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
											SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
											SQL = SQL & "" & jumlah & ") as Hasil "
											Dr3.Close()

											Using dr4 = OpenTrans(SQL)
												If dr4.Read Then
													If General_Class.CekNULL(dr4("Hasil")) <> "" Then

														If dr4("hasil") = 0 Then
															DataGridView1.Rows(ind).Cells(CellNBom_2).Value = 0
														Else
															DataGridView1.Rows(ind).Cells(CellNBom_2).Value = Format(dr4("hasil"), "N2")
														End If
													Else
														dr4.Close()
														CloseConn()
														MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If
												End If
											End Using
										Else
											Dr3.Close()
											CloseConn()
											MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using
								Next
							Else
								DataGridView1.Rows(ind).Cells(CellNBom_2).Value = 0

							End If

						End Using

					ElseIf Flag_Packaging = "Y" Then
						SQL = ";with cte as ( "
						SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
						SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
						SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
						SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
						SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
						SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y' and "
						SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
						SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
						SQL = SQL & ") "
						SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
						SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
						Using ds3 = BindingTrans(SQL)

							If ds3.Tables("MyTable").Rows.Count <> 0 Then
								For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

									Dim jumlah As Double = 0

									jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

									Dim convertKeSatuanAsli As String = ""
									Dim jumlahBarangDibutuhkan As Double = 0

									SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
									SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
									Using Dr3 = OpenTrans(SQL)
										If Dr3.Read Then
											convertKeSatuanAsli = Dr3("satuan")
											SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
											SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
											SQL = SQL & "" & jumlah & ") as Hasil "
											Dr3.Close()

											Using dr4 = OpenTrans(SQL)
												If dr4.Read Then
													If General_Class.CekNULL(dr4("Hasil")) <> "" Then

														If dr4("hasil") = 0 Then
															DataGridView1.Rows(ind).Cells(CellNBom_2).Value = 0
														Else
															DataGridView1.Rows(ind).Cells(CellNBom_2).Value = Format(dr4("hasil"), "N2")
														End If
													Else
														dr4.Close()
														CloseConn()
														MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If
												End If
											End Using
										Else
											Dr3.Close()
											CloseConn()
											MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using
								Next
							Else
								DataGridView1.Rows(ind).Cells(CellNBom_2).Value = 0

							End If

						End Using
					End If

				End If

				'============ akhir bulan 2

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

				''BULAN KE 3

				'If fstatus = "MRP_PPIC" Then

				'    If akses_ubah = "Y" Then
				'        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
				'        Using dr = OpenTrans(SQL)
				'            If dr.Read Then
				'                FValidasi = "Y"
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = True
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.DarkCyan
				'            Else

				'                dr.Close()
				'                'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'                'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
				'                'Using dr2 = OpenTrans(SQL)
				'                '    If dr2.Read Then
				'                '        FValidasi = ""
				'                '        DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = False
				'                '        DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.LightCyan
				'                '    Else
				'                FValidasi = ""
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = False
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.LightCyan
				'                '    End If
				'                'End Using
				'            End If
				'        End Using
				'    Else
				'        FValidasi = ""
				'        DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = True
				'        DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.DarkCyan
				'    End If

				'ElseIf fstatus = "MRP_Formulator" Then
				'    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
				'    Using dr = OpenTrans(SQL)
				'        If dr.Read Then
				'            FValidasi = "Y"

				'            DataGridView1.Rows(ind).Cells(CellNBom_3).Style.BackColor = Color.DarkGoldenrod
				'        Else
				'            FValidasi = ""

				'            DataGridView1.Rows(ind).Cells(CellNBom_3).Style.BackColor = Color.LightYellow
				'        End If
				'    End Using
				'End If
				'ada_data = ""
				'SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
				'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				'SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
				'Using Ds2 = BindingTrans(SQL)
				'    With Ds2.Tables("MyTable")
				'        If .Rows.Count <> 0 Then
				'            DataGridView1.Rows(ind).Cells(CellNBom_3).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
				'            DataGridView1.Rows(ind).Cells(CellNPPIC_3).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
				'            DataGridView1.Rows(ind).Cells(CellUrut_3).Value = .Rows(0).Item("Urut")
				'            DataGridView1.Rows(ind).Cells(CellKosong_3).Value = ""
				'            ada_data = "T"
				'        Else
				'            '  DataGridView1.Rows(ind).Cells(CellNBom_3).Value = 0
				'            DataGridView1.Rows(ind).Cells(CellNPPIC_3).Value = 0
				'            DataGridView1.Rows(ind).Cells(CellUrut_3).Value = ""
				'            DataGridView1.Rows(ind).Cells(CellKosong_3).Value = ""
				'            ada_data = "T"
				'        End If
				'    End With
				'End Using

				'If ada_data = "T" Then

				'    If Flag_Raw_Material = "Y" Then
				'        SQL = ";with cte as ( "
				'        SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
				'        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
				'        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
				'        SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
				'        SQL = SQL & "from "
				'        SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
				'        SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
				'        SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
				'        SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
				'        SQL = SQL & "and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y'  "
				'        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
				'        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
				'        SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
				'        SQL = SQL & ") "
				'        SQL = SQL & "select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
				'        SQL = SQL & "group by Kode_Bahan, satuan_barang "
				'        Using ds3 = BindingTrans(SQL)

				'            If ds3.Tables("MyTable").Rows.Count <> 0 Then
				'                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

				'                    Dim jumlah As Double = 0

				'                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

				'                    Dim convertKeSatuanAsli As String = ""
				'                    Dim jumlahBarangDibutuhkan As Double = 0

				'                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
				'                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
				'                    Using Dr3 = OpenTrans(SQL)
				'                        If Dr3.Read Then
				'                            convertKeSatuanAsli = Dr3("satuan")
				'                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
				'                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
				'                            SQL = SQL & "" & jumlah & ") as Hasil "
				'                            Dr3.Close()

				'                            Using dr4 = OpenTrans(SQL)
				'                                If dr4.Read Then
				'                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then

				'                                        If dr4("hasil") = 0 Then
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_3).Value = 0
				'                                        Else
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_3).Value = Format(dr4("hasil"), "N2")
				'                                        End If

				'                                    Else
				'                                        dr4.Close()
				'                                        CloseConn()
				'                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                                        Exit Sub
				'                                    End If
				'                                End If
				'                            End Using
				'                        Else
				'                            Dr3.Close()
				'                            CloseConn()
				'                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using
				'                Next
				'            Else
				'                DataGridView1.Rows(ind).Cells(CellNBom_3).Value = 0

				'            End If

				'        End Using

				'    ElseIf Flag_Packaging = "Y" Then
				'        SQL = ";with cte as ( "
				'        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
				'        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
				'        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
				'        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
				'        SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y' and "
				'        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
				'        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
				'        SQL = SQL & ") "
				'        SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
				'        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
				'        Using ds3 = BindingTrans(SQL)

				'            If ds3.Tables("MyTable").Rows.Count <> 0 Then
				'                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

				'                    Dim jumlah As Double = 0

				'                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

				'                    Dim convertKeSatuanAsli As String = ""
				'                    Dim jumlahBarangDibutuhkan As Double = 0

				'                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
				'                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
				'                    Using Dr3 = OpenTrans(SQL)
				'                        If Dr3.Read Then
				'                            convertKeSatuanAsli = Dr3("satuan")
				'                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
				'                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
				'                            SQL = SQL & "" & jumlah & ") as Hasil "
				'                            Dr3.Close()

				'                            Using dr4 = OpenTrans(SQL)
				'                                If dr4.Read Then
				'                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then

				'                                        If dr4("hasil") = 0 Then
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_3).Value = 0
				'                                        Else
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_3).Value = Format(dr4("hasil"), "N2")
				'                                        End If

				'                                    Else
				'                                        dr4.Close()
				'                                        CloseConn()
				'                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                                        Exit Sub
				'                                    End If
				'                                End If
				'                            End Using
				'                        Else
				'                            Dr3.Close()
				'                            CloseConn()
				'                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using
				'                Next
				'            Else
				'                DataGridView1.Rows(ind).Cells(CellNBom_3).Value = 0

				'            End If

				'        End Using
				'    End If

				'End If

				''============ akhir bulan 3

				'If a = 12 Then
				'    a = 1
				'    fthn = fthn + 1
				'Else
				'    a = a + 1
				'End If

				'For index = 0 To arrBulan.Count - 1
				'    If arrBulan.Item(index) = a Then
				'        'ComboBox1.SelectedIndex = index
				'        b = arrBulanMM.Item(index)
				'    End If
				'Next
				''BULAN KE 4

				'If fstatus = "MRP_PPIC" Then

				'    If akses_ubah = "Y" Then
				'        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
				'        Using dr = OpenTrans(SQL)
				'            If dr.Read Then
				'                FValidasi = "Y"
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = True
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.DarkCyan
				'            Else

				'                dr.Close()
				'                'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'                'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
				'                'Using dr2 = OpenTrans(SQL)
				'                '    If dr2.Read Then
				'                '        FValidasi = ""
				'                '        DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = False
				'                '        DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.LightCyan
				'                '    Else
				'                FValidasi = ""
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = False
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.LightCyan
				'                '    End If
				'                'End Using
				'            End If
				'        End Using
				'    Else
				'        FValidasi = ""
				'        DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = True
				'        DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.DarkCyan
				'    End If

				'ElseIf fstatus = "MRP_Formulator" Then
				'    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
				'    Using dr = OpenTrans(SQL)
				'        If dr.Read Then
				'            FValidasi = "Y"

				'            DataGridView1.Rows(ind).Cells(CellNBom_4).Style.BackColor = Color.DarkGoldenrod
				'        Else
				'            FValidasi = ""

				'            DataGridView1.Rows(ind).Cells(CellNBom_4).Style.BackColor = Color.LightYellow
				'        End If
				'    End Using
				'End If

				'ada_data = ""
				'SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
				'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				'SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
				'Using Ds2 = BindingTrans(SQL)
				'    With Ds2.Tables("MyTable")
				'        If .Rows.Count <> 0 Then
				'            DataGridView1.Rows(ind).Cells(CellNBom_4).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
				'            DataGridView1.Rows(ind).Cells(CellNPPIC_4).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
				'            DataGridView1.Rows(ind).Cells(CellUrut_4).Value = .Rows(0).Item("Urut")
				'            DataGridView1.Rows(ind).Cells(CellKosong_4).Value = ""
				'            ada_data = "T"
				'        Else
				'            ' DataGridView1.Rows(ind).Cells(CellNBom_4).Value = 0
				'            DataGridView1.Rows(ind).Cells(CellNPPIC_4).Value = 0
				'            DataGridView1.Rows(ind).Cells(CellUrut_4).Value = ""
				'            DataGridView1.Rows(ind).Cells(CellKosong_4).Value = ""
				'            ada_data = "T"
				'        End If
				'    End With
				'End Using

				'If ada_data = "T" Then

				'    If Flag_Raw_Material = "Y" Then
				'        SQL = ";with cte as ( "
				'        SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
				'        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
				'        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
				'        SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
				'        SQL = SQL & "from "
				'        SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
				'        SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
				'        SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
				'        SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
				'        SQL = SQL & "and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y'  "
				'        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
				'        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
				'        SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
				'        SQL = SQL & ") "
				'        SQL = SQL & "select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
				'        SQL = SQL & "group by Kode_Bahan, satuan_barang "
				'        Using ds3 = BindingTrans(SQL)

				'            If ds3.Tables("MyTable").Rows.Count <> 0 Then
				'                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

				'                    Dim jumlah As Double = 0

				'                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

				'                    Dim convertKeSatuanAsli As String = ""
				'                    Dim jumlahBarangDibutuhkan As Double = 0

				'                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
				'                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
				'                    Using Dr3 = OpenTrans(SQL)
				'                        If Dr3.Read Then
				'                            convertKeSatuanAsli = Dr3("satuan")
				'                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
				'                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
				'                            SQL = SQL & "" & jumlah & ") as Hasil "
				'                            Dr3.Close()

				'                            Using dr4 = OpenTrans(SQL)
				'                                If dr4.Read Then
				'                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then

				'                                        If dr4("hasil") = 0 Then
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_4).Value = 0
				'                                        Else
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_4).Value = Format(dr4("hasil"), "N2")
				'                                        End If

				'                                    Else
				'                                        dr4.Close()
				'                                        CloseConn()
				'                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                                        Exit Sub
				'                                    End If
				'                                End If
				'                            End Using
				'                        Else
				'                            Dr3.Close()
				'                            CloseConn()
				'                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using
				'                Next
				'            Else
				'                DataGridView1.Rows(ind).Cells(CellNBom_4).Value = 0

				'            End If

				'        End Using

				'    ElseIf Flag_Packaging = "Y" Then
				'        SQL = ";with cte as ( "
				'        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
				'        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
				'        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
				'        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
				'        SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y' and "
				'        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
				'        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
				'        SQL = SQL & ") "
				'        SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
				'        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
				'        Using ds3 = BindingTrans(SQL)

				'            If ds3.Tables("MyTable").Rows.Count <> 0 Then
				'                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

				'                    Dim jumlah As Double = 0

				'                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

				'                    Dim convertKeSatuanAsli As String = ""
				'                    Dim jumlahBarangDibutuhkan As Double = 0

				'                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
				'                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
				'                    Using Dr3 = OpenTrans(SQL)
				'                        If Dr3.Read Then
				'                            convertKeSatuanAsli = Dr3("satuan")
				'                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
				'                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
				'                            SQL = SQL & "" & jumlah & ") as Hasil "
				'                            Dr3.Close()

				'                            Using dr4 = OpenTrans(SQL)
				'                                If dr4.Read Then
				'                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then

				'                                        If dr4("hasil") = 0 Then
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_4).Value = 0
				'                                        Else
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_4).Value = Format(dr4("hasil"), "N2")
				'                                        End If

				'                                    Else
				'                                        dr4.Close()
				'                                        CloseConn()
				'                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                                        Exit Sub
				'                                    End If
				'                                End If
				'                            End Using
				'                        Else
				'                            Dr3.Close()
				'                            CloseConn()
				'                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using
				'                Next
				'            Else
				'                DataGridView1.Rows(ind).Cells(CellNBom_4).Value = 0

				'            End If

				'        End Using
				'    End If

				'End If

				''============ akhir bulan 4

				'If a = 12 Then
				'    a = 1
				'    fthn = fthn + 1
				'Else
				'    a = a + 1
				'End If

				'For index = 0 To arrBulan.Count - 1
				'    If arrBulan.Item(index) = a Then
				'        'ComboBox1.SelectedIndex = index
				'        b = arrBulanMM.Item(index)
				'    End If
				'Next
				''BULAN  5

				'If fstatus = "MRP_PPIC" Then

				'    If akses_ubah = "Y" Then
				'        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
				'        Using dr = OpenTrans(SQL)
				'            If dr.Read Then
				'                FValidasi = "Y"
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = True
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.DarkCyan
				'            Else

				'                dr.Close()
				'                'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'                'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
				'                'Using dr2 = OpenTrans(SQL)
				'                '    If dr2.Read Then
				'                '        FValidasi = ""
				'                '        DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = False
				'                '        DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.LightCyan
				'                '    Else
				'                FValidasi = ""
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = False
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.LightCyan
				'                '    End If
				'                'End Using
				'            End If
				'        End Using
				'    Else
				'        FValidasi = ""
				'        DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = True
				'        DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.DarkCyan
				'    End If

				'ElseIf fstatus = "MRP_Formulator" Then
				'    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
				'    Using dr = OpenTrans(SQL)
				'        If dr.Read Then
				'            FValidasi = "Y"

				'            DataGridView1.Rows(ind).Cells(CellNBom_5).Style.BackColor = Color.DarkGoldenrod
				'        Else
				'            FValidasi = ""

				'            DataGridView1.Rows(ind).Cells(CellNBom_5).Style.BackColor = Color.LightYellow
				'        End If
				'    End Using
				'End If
				'ada_data = ""
				'SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
				'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				'SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
				'Using Ds2 = BindingTrans(SQL)
				'    With Ds2.Tables("MyTable")
				'        If .Rows.Count <> 0 Then
				'            DataGridView1.Rows(ind).Cells(CellNBom_5).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
				'            DataGridView1.Rows(ind).Cells(CellNPPIC_5).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
				'            DataGridView1.Rows(ind).Cells(CellUrut_5).Value = .Rows(0).Item("Urut")
				'            DataGridView1.Rows(ind).Cells(CellKosong_5).Value = ""
				'            ada_data = "T"
				'        Else
				'            '  DataGridView1.Rows(ind).Cells(CellNBom_5).Value = 0
				'            DataGridView1.Rows(ind).Cells(CellNPPIC_5).Value = 0
				'            DataGridView1.Rows(ind).Cells(CellUrut_5).Value = ""
				'            DataGridView1.Rows(ind).Cells(CellKosong_5).Value = ""
				'            ada_data = "T"
				'        End If
				'    End With
				'End Using

				'If ada_data = "T" Then

				'    If Flag_Raw_Material = "Y" Then
				'        SQL = ";with cte as ( "
				'        SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
				'        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
				'        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
				'        SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
				'        SQL = SQL & "from "
				'        SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
				'        SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
				'        SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
				'        SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
				'        SQL = SQL & "and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y'  "
				'        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
				'        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
				'        SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
				'        SQL = SQL & ") "
				'        SQL = SQL & "select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
				'        SQL = SQL & "group by Kode_Bahan, satuan_barang "
				'        Using ds3 = BindingTrans(SQL)

				'            If ds3.Tables("MyTable").Rows.Count <> 0 Then
				'                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

				'                    Dim jumlah As Double = 0

				'                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

				'                    Dim convertKeSatuanAsli As String = ""
				'                    Dim jumlahBarangDibutuhkan As Double = 0

				'                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
				'                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
				'                    Using Dr3 = OpenTrans(SQL)
				'                        If Dr3.Read Then
				'                            convertKeSatuanAsli = Dr3("satuan")
				'                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
				'                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
				'                            SQL = SQL & "" & jumlah & ") as Hasil "
				'                            Dr3.Close()

				'                            Using dr4 = OpenTrans(SQL)
				'                                If dr4.Read Then
				'                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then

				'                                        If dr4("hasil") = 0 Then
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_5).Value = 0
				'                                        Else
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_5).Value = Format(dr4("hasil"), "N2")
				'                                        End If

				'                                    Else
				'                                        dr4.Close()
				'                                        CloseConn()
				'                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                                        Exit Sub
				'                                    End If
				'                                End If
				'                            End Using
				'                        Else
				'                            Dr3.Close()
				'                            CloseConn()
				'                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using
				'                Next
				'            Else
				'                DataGridView1.Rows(ind).Cells(CellNBom_5).Value = 0

				'            End If

				'        End Using

				'    ElseIf Flag_Packaging = "Y" Then
				'        SQL = ";with cte as ( "
				'        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
				'        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
				'        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
				'        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
				'        SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y' and "
				'        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
				'        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
				'        SQL = SQL & ") "
				'        SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
				'        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
				'        Using ds3 = BindingTrans(SQL)

				'            If ds3.Tables("MyTable").Rows.Count <> 0 Then
				'                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

				'                    Dim jumlah As Double = 0

				'                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

				'                    Dim convertKeSatuanAsli As String = ""
				'                    Dim jumlahBarangDibutuhkan As Double = 0

				'                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
				'                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
				'                    Using Dr3 = OpenTrans(SQL)
				'                        If Dr3.Read Then
				'                            convertKeSatuanAsli = Dr3("satuan")
				'                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
				'                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
				'                            SQL = SQL & "" & jumlah & ") as Hasil "
				'                            Dr3.Close()

				'                            Using dr4 = OpenTrans(SQL)
				'                                If dr4.Read Then
				'                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then

				'                                        If dr4("hasil") = 0 Then
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_5).Value = 0
				'                                        Else
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_5).Value = Format(dr4("hasil"), "N2")
				'                                        End If

				'                                    Else
				'                                        dr4.Close()
				'                                        CloseConn()
				'                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                                        Exit Sub
				'                                    End If
				'                                End If
				'                            End Using
				'                        Else
				'                            Dr3.Close()
				'                            CloseConn()
				'                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using
				'                Next
				'            Else
				'                DataGridView1.Rows(ind).Cells(CellNBom_5).Value = 0

				'            End If

				'        End Using
				'    End If

				'End If

				''============ akhir bulan 5

				'If a = 12 Then
				'    a = 1
				'    fthn = fthn + 1
				'Else
				'    a = a + 1
				'End If

				'For index = 0 To arrBulan.Count - 1
				'    If arrBulan.Item(index) = a Then
				'        'ComboBox1.SelectedIndex = index
				'        b = arrBulanMM.Item(index)
				'    End If
				'Next
				''BULAN KE 6

				'If fstatus = "MRP_PPIC" Then

				'    If akses_ubah = "Y" Then
				'        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
				'        Using dr = OpenTrans(SQL)
				'            If dr.Read Then
				'                FValidasi = "Y"
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = True
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.DarkCyan
				'            Else

				'                dr.Close()
				'                'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'                'SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
				'                'Using dr2 = OpenTrans(SQL)
				'                '    If dr2.Read Then
				'                '        FValidasi = ""
				'                '        DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = False
				'                '        DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.LightCyan
				'                '    Else
				'                FValidasi = ""
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = False
				'                DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.LightCyan
				'                '    End If
				'                'End Using
				'            End If
				'        End Using
				'    Else
				'        FValidasi = ""
				'        DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = True
				'        DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.DarkCyan
				'    End If

				'ElseIf fstatus = "MRP_Formulator" Then
				'    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				'    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
				'    Using dr = OpenTrans(SQL)
				'        If dr.Read Then
				'            FValidasi = "Y"

				'            DataGridView1.Rows(ind).Cells(CellNBom_6).Style.BackColor = Color.DarkGoldenrod
				'        Else
				'            FValidasi = ""

				'            DataGridView1.Rows(ind).Cells(CellNBom_6).Style.BackColor = Color.LightYellow
				'        End If
				'    End Using
				'End If

				'ada_data = ""
				'SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
				'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & b & "' and tahun = '" & fthn & "' and "
				'SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(indexxx) & "' and Kode_Barang = '" & Arrbarang.Item(indexxx) & "'"
				'Using Ds2 = BindingTrans(SQL)
				'    With Ds2.Tables("MyTable")
				'        If .Rows.Count <> 0 Then
				'            DataGridView1.Rows(ind).Cells(CellNBom_6).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
				'            DataGridView1.Rows(ind).Cells(CellNPPIC_6).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
				'            DataGridView1.Rows(ind).Cells(CellUrut_6).Value = .Rows(0).Item("Urut")
				'            DataGridView1.Rows(ind).Cells(CellKosong_6).Value = ""
				'            ada_data = "T"
				'        Else
				'            '  DataGridView1.Rows(ind).Cells(CellNBom_6).Value = 0
				'            DataGridView1.Rows(ind).Cells(CellNPPIC_6).Value = 0
				'            DataGridView1.Rows(ind).Cells(CellUrut_6).Value = ""
				'            DataGridView1.Rows(ind).Cells(CellKosong_6).Value = ""
				'            ada_data = "T"
				'        End If
				'    End With
				'End Using

				'If ada_data = "T" Then

				'    If Flag_Raw_Material = "Y" Then
				'        SQL = ";with cte as ( "
				'        SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
				'        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
				'        SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
				'        SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
				'        SQL = SQL & "from "
				'        SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
				'        SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
				'        SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
				'        SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' "
				'        SQL = SQL & "and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y'  "
				'        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
				'        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
				'        SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(indexxx) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
				'        SQL = SQL & ") "
				'        SQL = SQL & "select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
				'        SQL = SQL & "group by Kode_Bahan, satuan_barang "
				'        Using ds3 = BindingTrans(SQL)

				'            If ds3.Tables("MyTable").Rows.Count <> 0 Then
				'                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

				'                    Dim jumlah As Double = 0

				'                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

				'                    Dim convertKeSatuanAsli As String = ""
				'                    Dim jumlahBarangDibutuhkan As Double = 0

				'                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
				'                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
				'                    Using Dr3 = OpenTrans(SQL)
				'                        If Dr3.Read Then
				'                            convertKeSatuanAsli = Dr3("satuan")
				'                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
				'                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
				'                            SQL = SQL & "" & jumlah & ") as Hasil "
				'                            Dr3.Close()

				'                            Using dr4 = OpenTrans(SQL)
				'                                If dr4.Read Then
				'                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then

				'                                        If dr4("hasil") = 0 Then
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_6).Value = 0
				'                                        Else
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_6).Value = Format(dr4("hasil"), "N2")
				'                                        End If

				'                                    Else
				'                                        dr4.Close()
				'                                        CloseConn()
				'                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                                        Exit Sub
				'                                    End If
				'                                End If
				'                            End Using
				'                        Else
				'                            Dr3.Close()
				'                            CloseConn()
				'                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using
				'                Next
				'            Else
				'                DataGridView1.Rows(ind).Cells(CellNBom_6).Value = 0

				'            End If

				'        End Using

				'    ElseIf Flag_Packaging = "Y" Then
				'        SQL = ";with cte as ( "
				'        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
				'        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
				'        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
				'        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
				'        SQL = SQL & "And a.Bulan = '" & b & "' and a.tahun = '" & fthn & "' and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y' and "
				'        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
				'        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(indexxx) & "' "
				'        SQL = SQL & ") "
				'        SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
				'        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
				'        Using ds3 = BindingTrans(SQL)

				'            If ds3.Tables("MyTable").Rows.Count <> 0 Then
				'                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

				'                    Dim jumlah As Double = 0

				'                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

				'                    Dim convertKeSatuanAsli As String = ""
				'                    Dim jumlahBarangDibutuhkan As Double = 0

				'                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
				'                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
				'                    Using Dr3 = OpenTrans(SQL)
				'                        If Dr3.Read Then
				'                            convertKeSatuanAsli = Dr3("satuan")
				'                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
				'                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
				'                            SQL = SQL & "" & jumlah & ") as Hasil "
				'                            Dr3.Close()

				'                            Using dr4 = OpenTrans(SQL)
				'                                If dr4.Read Then
				'                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then

				'                                        If dr4("hasil") = 0 Then
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_6).Value = 0
				'                                        Else
				'                                            DataGridView1.Rows(ind).Cells(CellNBom_6).Value = Format(dr4("hasil"), "N2")
				'                                        End If

				'                                    Else
				'                                        dr4.Close()
				'                                        CloseConn()
				'                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                                        Exit Sub
				'                                    End If
				'                                End If
				'                            End Using
				'                        Else
				'                            Dr3.Close()
				'                            CloseConn()
				'                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using
				'                Next
				'            Else
				'                DataGridView1.Rows(ind).Cells(CellNBom_6).Value = 0

				'            End If

				'        End Using
				'    End If

				'End If

				''============ akhir bulan 6

				DataGridView1.Rows(ind).Cells(CellStatus).Value = "SUBMITED"
			Next

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If ComboBox1.Text.Trim.Length = 0 Then
			MessageBox.Show("Bulan Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox1.Focus() : Exit Sub
		ElseIf ComboBox2.Text.Trim.Length = 0 Then
			MessageBox.Show("Tahun Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox2.Focus() : Exit Sub
		ElseIf ComboBox3.Text.Trim.Length = 0 Then
			MessageBox.Show("Lokasi Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox3.Focus() : Exit Sub
		End If

		'ComboBox1.Enabled = False
		'ComboBox2.Enabled = False
		'ComboBox3.Enabled = False
		'SD_Pilih_Produk.urutcmb = ComboBox1.SelectedIndex
		'SD_Pilih_Produk.ShowDialog()
	End Sub

	Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub kosong()
		ComboBox1.Items.Clear() : arrBulan.Clear() : arrBulanMM.Clear()
		ComboBox1.Items.Add("Januari") : arrBulan.Add("1") : arrBulanMM.Add("01")
		ComboBox1.Items.Add("Februari") : arrBulan.Add("2") : arrBulanMM.Add("02")
		ComboBox1.Items.Add("Maret") : arrBulan.Add("3") : arrBulanMM.Add("03")
		ComboBox1.Items.Add("April") : arrBulan.Add("4") : arrBulanMM.Add("04")
		ComboBox1.Items.Add("Mei") : arrBulan.Add("5") : arrBulanMM.Add("05")
		ComboBox1.Items.Add("Juni") : arrBulan.Add("6") : arrBulanMM.Add("06")
		ComboBox1.Items.Add("Juli") : arrBulan.Add("7") : arrBulanMM.Add("07")
		ComboBox1.Items.Add("Agustus") : arrBulan.Add("8") : arrBulanMM.Add("08")
		ComboBox1.Items.Add("September") : arrBulan.Add("9") : arrBulanMM.Add("09")
		ComboBox1.Items.Add("Oktober") : arrBulan.Add("10") : arrBulanMM.Add("10")
		ComboBox1.Items.Add("November") : arrBulan.Add("11") : arrBulanMM.Add("11")
		ComboBox1.Items.Add("Desember") : arrBulan.Add("12") : arrBulanMM.Add("12")
		ComboBox1.SelectedIndex = -1
		ComboBox1.Enabled = False

		btn_bln0.Visible = False
		btn_bln1.Visible = False
		btn_bln2.Visible = False
		BtnExport.Visible = False

		ComboBox2.Items.Clear()
		Dim tahun_awal As Integer = Date.Now.Year - 2
		Dim tahun_akhir As Integer = Date.Now.Year + 2
		For a As Integer = tahun_awal To tahun_akhir
			ComboBox2.Items.Add(a)
		Next
		ComboBox2.SelectedIndex = -1
		'ComboBox2.Enabled = True
		ComboBox2.Enabled = False
		Btn_Simpan.Tag = "&Simpan"

		get_jam()

		Dim akses_ubah As String = "Y"
		Dim akses_realease As String = "Y"
		Dim akses_unrealease As String = "Y"
		'Try
		'    OpenConn()

		'    If CekButtonRole("MRP_PPIC") = "Y" Then
		'        akses_ubah = "Y"
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		''
		'Try
		'    OpenConn()

		'    If CekButtonRole("MRP_Realease") = "Y" Then
		'        akses_realease = "Y"
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		'Try
		'    OpenConn()

		'    If CekButtonRole("MRP_Unrealease") = "Y" Then
		'        akses_unrealease = "Y"
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		Try
			OpenConn()

			ComboBox3.Items.Clear()
			SQL = "select Kode_Stock_Owner from Stock_Owner where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Stock_Owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ComboBox3.Items.Add(dr("Kode_Stock_Owner"))
				Loop
			End Using
			ComboBox3.SelectedIndex = -1
			ComboBox3.Enabled = True

			'get_no_faktur()
			TxtBarangMasuk_NoFaktur.Enabled = True

			If fstatus = "MRP_PPIC" Then
				If akses_ubah = "Y" Then
					DataGridView1.Columns(Cell0).ReadOnly = False
					DataGridView1.Columns(CellKd_Barang).ReadOnly = True
					DataGridView1.Columns(CellNm_Barang).ReadOnly = True
					DataGridView1.Columns(CellAvg_3Bln).ReadOnly = True
					DataGridView1.Columns(CellStock_BB).ReadOnly = True
					DataGridView1.Columns(CellOPRequesition).ReadOnly = True
					DataGridView1.Columns(CellOPOrder).ReadOnly = True
					DataGridView1.Columns(CellTotal).ReadOnly = True
					DataGridView1.Columns(CellNBom_1).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_1).ReadOnly = False
					DataGridView1.Columns(CellUrut_1).ReadOnly = True
					DataGridView1.Columns(CellKosong_1).ReadOnly = True
					DataGridView1.Columns(CellNBom_2).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_2).ReadOnly = False
					DataGridView1.Columns(CellUrut_2).ReadOnly = True
					DataGridView1.Columns(CellKosong_2).ReadOnly = True
					DataGridView1.Columns(CellNBom_3).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_3).ReadOnly = False
					DataGridView1.Columns(CellUrut_3).ReadOnly = True
					DataGridView1.Columns(CellKosong_3).ReadOnly = True
					DataGridView1.Columns(CellNBom_4).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_4).ReadOnly = False
					DataGridView1.Columns(CellUrut_4).ReadOnly = True
					DataGridView1.Columns(CellKosong_4).ReadOnly = True
					DataGridView1.Columns(CellNBom_5).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_5).ReadOnly = False
					DataGridView1.Columns(CellUrut_5).ReadOnly = True
					DataGridView1.Columns(CellKosong_5).ReadOnly = True
					DataGridView1.Columns(CellNBom_6).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_6).ReadOnly = False
					DataGridView1.Columns(CellUrut_6).ReadOnly = True
					DataGridView1.Columns(CellKosong_6).ReadOnly = True
					DataGridView1.Columns(CellStatus).ReadOnly = True
					DataGridView1.Columns(CellSatuanBarang).ReadOnly = True
					DataGridView1.Columns(CellSatuanBarang).DisplayIndex = 3
					DataGridView1.Columns(cellCarryOver).DisplayIndex = 10
					DataGridView1.Columns(cellRequirmentStok).DisplayIndex = 11

					DataGridView1.Columns(cellEndingStokSebelumPRPO).DisplayIndex = 7

					DataGridView1.Columns(CellOPRequesition_1).DisplayIndex = 14
					DataGridView1.Columns(CellOPOrder_1).DisplayIndex = 15
					DataGridView1.Columns(cellRequireStok_1).DisplayIndex = 16

					DataGridView1.Columns(CellNBom_2).DisplayIndex = 18
					DataGridView1.Columns(CellOPRequesition_2).DisplayIndex = 19
					DataGridView1.Columns(CellOPOrder_2).DisplayIndex = 20
					DataGridView1.Columns(cellRequireStok_2).DisplayIndex = 21


					'3
					DataGridView1.Columns(CellNBom_3).DisplayIndex = 24
					DataGridView1.Columns(CellOPRequesition_3).DisplayIndex = 25
					DataGridView1.Columns(CellOPOrder_3).DisplayIndex = 26
					DataGridView1.Columns(cellRequireStok_3).DisplayIndex = 27

					'4
					DataGridView1.Columns(CellNBom_4).DisplayIndex = 30
					DataGridView1.Columns(CellOPRequesition_4).DisplayIndex = 31
					DataGridView1.Columns(CellOPOrder_4).DisplayIndex = 32
					DataGridView1.Columns(cellRequireStok_4).DisplayIndex = 33

					'5
					DataGridView1.Columns(CellNBom_5).DisplayIndex = 36
					DataGridView1.Columns(CellOPRequesition_5).DisplayIndex = 37
					DataGridView1.Columns(CellOPOrder_5).DisplayIndex = 38
					DataGridView1.Columns(cellRequireStok_5).DisplayIndex = 39

					'6
					DataGridView1.Columns(CellNBom_6).DisplayIndex = 42
					DataGridView1.Columns(CellOPRequesition_6).DisplayIndex = 43
					DataGridView1.Columns(CellOPOrder_6).DisplayIndex = 44
					DataGridView1.Columns(cellRequireStok_6).DisplayIndex = 45




					CheckBox1.Enabled = False
					Button1.Enabled = False
					Btn_Simpan.Enabled = True
					Btn_Simpan.Tag = "&Simpan"
				Else
					DataGridView1.Columns(Cell0).ReadOnly = False
					DataGridView1.Columns(CellKd_Barang).ReadOnly = True
					DataGridView1.Columns(CellNm_Barang).ReadOnly = True
					DataGridView1.Columns(CellAvg_3Bln).ReadOnly = True
					DataGridView1.Columns(CellStock_BB).ReadOnly = True
					DataGridView1.Columns(CellOPRequesition).ReadOnly = True
					DataGridView1.Columns(CellOPOrder).ReadOnly = True
					DataGridView1.Columns(CellTotal).ReadOnly = True
					DataGridView1.Columns(CellNBom_1).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_1).ReadOnly = True
					DataGridView1.Columns(CellUrut_1).ReadOnly = True
					DataGridView1.Columns(CellKosong_1).ReadOnly = True
					DataGridView1.Columns(CellNBom_2).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_2).ReadOnly = True
					DataGridView1.Columns(CellUrut_2).ReadOnly = True
					DataGridView1.Columns(CellKosong_2).ReadOnly = True
					DataGridView1.Columns(CellNBom_3).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_3).ReadOnly = True
					DataGridView1.Columns(CellUrut_3).ReadOnly = True
					DataGridView1.Columns(CellKosong_3).ReadOnly = True
					DataGridView1.Columns(CellNBom_4).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_4).ReadOnly = True
					DataGridView1.Columns(CellUrut_4).ReadOnly = True
					DataGridView1.Columns(CellKosong_4).ReadOnly = True
					DataGridView1.Columns(CellNBom_5).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_5).ReadOnly = True
					DataGridView1.Columns(CellUrut_5).ReadOnly = True
					DataGridView1.Columns(CellKosong_5).ReadOnly = True
					DataGridView1.Columns(CellNBom_6).ReadOnly = True
					DataGridView1.Columns(CellNPPIC_6).ReadOnly = True
					DataGridView1.Columns(CellUrut_6).ReadOnly = True
					DataGridView1.Columns(CellKosong_6).ReadOnly = True
					DataGridView1.Columns(CellStatus).ReadOnly = True

					DataGridView1.Columns(CellSatuanBarang).ReadOnly = True
					DataGridView1.Columns(CellSatuanBarang).DisplayIndex = 3

					CheckBox1.Enabled = False
					Button1.Enabled = False
					Btn_Simpan.Enabled = False
				End If

				If akses_realease = "Y" Then
					Btn_Realese.Enabled = True
				Else
					Btn_Realese.Enabled = False
				End If

				If akses_unrealease = "Y" Then
					btnUnRelease.Enabled = True
				Else
					btnUnRelease.Enabled = False
				End If
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Try
		'    OpenConn()

		'If CekButtonRole("MRP_Formulator") = "Y" Then
		'    DataGridView1.Columns(Cell0).ReadOnly = False
		'    DataGridView1.Columns(CellKd_Barang).ReadOnly = True
		'    DataGridView1.Columns(CellNm_Barang).ReadOnly = True
		'    DataGridView1.Columns(CellAvg_3Bln).ReadOnly = True
		'    DataGridView1.Columns(CellStock_BB).ReadOnly = True
		'    DataGridView1.Columns(CellOPRequesition).ReadOnly = True
		'    DataGridView1.Columns(CellOPOrder).ReadOnly = True
		'    DataGridView1.Columns(CellTotal).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_1).ReadOnly = False
		'    DataGridView1.Columns(CellNPPIC_1).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_1).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_1).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_2).ReadOnly = False
		'    DataGridView1.Columns(CellNPPIC_2).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_2).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_2).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_3).ReadOnly = False
		'    DataGridView1.Columns(CellNPPIC_3).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_3).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_3).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_4).ReadOnly = False
		'    DataGridView1.Columns(CellNPPIC_4).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_4).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_4).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_5).ReadOnly = False
		'    DataGridView1.Columns(CellNPPIC_5).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_5).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_5).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_6).ReadOnly = False
		'    DataGridView1.Columns(CellNPPIC_6).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_6).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_6).ReadOnly = True
		'    DataGridView1.Columns(CellStatus).ReadOnly = True
		'    Button1.Enabled = True
		'    Btn_Refresh.Enabled = True
		'Else
		'    DataGridView1.Columns(Cell0).ReadOnly = False
		'    DataGridView1.Columns(CellKd_Barang).ReadOnly = True
		'    DataGridView1.Columns(CellNm_Barang).ReadOnly = True
		'    DataGridView1.Columns(CellAvg_3Bln).ReadOnly = True
		'    DataGridView1.Columns(CellStock_BB).ReadOnly = True
		'    DataGridView1.Columns(CellOPRequesition).ReadOnly = True
		'    DataGridView1.Columns(CellOPOrder).ReadOnly = True
		'    DataGridView1.Columns(CellTotal).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_1).ReadOnly = True
		'    DataGridView1.Columns(CellNPPIC_1).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_1).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_1).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_2).ReadOnly = True
		'    DataGridView1.Columns(CellNPPIC_2).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_2).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_2).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_3).ReadOnly = True
		'    DataGridView1.Columns(CellNPPIC_3).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_3).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_3).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_4).ReadOnly = True
		'    DataGridView1.Columns(CellNPPIC_4).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_4).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_4).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_5).ReadOnly = True
		'    DataGridView1.Columns(CellNPPIC_5).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_5).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_5).ReadOnly = True
		'    DataGridView1.Columns(CellNBom_6).ReadOnly = True
		'    DataGridView1.Columns(CellNPPIC_6).ReadOnly = True
		'    DataGridView1.Columns(CellUrut_6).ReadOnly = True
		'    DataGridView1.Columns(CellKosong_6).ReadOnly = True
		'    DataGridView1.Columns(CellStatus).ReadOnly = True
		'    Button1.Enabled = False
		'    Btn_Refresh.Enabled = False
		'End If

		'CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		'TxtBarangMasuk_NoFaktur.Text = "FO-24/08-0001"

		DateTimePicker1.Enabled = True

		DateTimePicker1.Value = tgl_skg
		DataGridView1.Rows.Clear()

		Dim selectedDate As Date = DateTimePicker1.Value
		Dim selectedMonthName As String = selectedDate.ToString("MMMM", New Globalization.CultureInfo("id-ID"))
		Dim selectedYear As Integer = selectedDate.Year

		ComboBox1.SelectedItem = selectedMonthName
		ComboBox2.SelectedItem = selectedYear

	End Sub

	Private Sub get_no_faktur()
		Dim FPro_Results As String = "TMR"
		TxtBarangMasuk_NoFaktur.Text = FPro_Results & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("EMI_Transaksi_Material_Requsition", "No_Faktur", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(No_Faktur, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & Format(tgl_skg, "MMyy"))
	End Sub
	Private Sub DataGridView1_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellMouseEnter
		' Cek jika mouse berada di HEADER kolom yang valid
		If e.RowIndex = -1 AndAlso e.ColumnIndex >= 0 Then

			If e.ColumnIndex <> lastHeaderColumnIndex Then
				lastHeaderColumnIndex = e.ColumnIndex

				Dim judulBalon As String = ""
				Dim isiBalon As String = ""

				' Ambil NAMA objek kolom
				Dim namaKolom As String = DataGridView1.Columns(e.ColumnIndex).Name

				Dim bulanTahun As String = ""
				Dim headerText As String = DataGridView1.Columns(e.ColumnIndex).HeaderText
				If headerText.Contains("-") Then
					Dim potongan() As String = headerText.Split("-"c)

					' Ambil tahun dari potongan paling terakhir
					Dim tahun As String = potongan(potongan.Length - 1).Trim()

					' Ambil bagian sebelum tahun, lalu pecah per spasi untuk ambil nama bulannya
					Dim bagianKiri As String = potongan(potongan.Length - 2).Trim()
					Dim kataBagianKiri() As String = bagianKiri.Split(" "c)
					Dim bulan As String = kataBagianKiri(kataBagianKiri.Length - 1).Trim()

					' Satukan jadi format: "Juni - 2026"
					bulanTahun = bulan & " - " & tahun
				End If

				' PAKAI SELECT CASE BERDASARKAN NAMA KOLOM
				Select Case namaKolom
					Case "Column46"
						judulBalon = "Finished PRD"
						isiBalon = "PRD yang telah di GOOD ISSUE"

					Case "Column45"
						judulBalon = "Outstanding PRD"
						isiBalon = "Production Plan yang sudah di buat Production Order"

					Case "Column44"
						judulBalon = "Outstanding Production Plan"
						isiBalon = "Production plan yang sudah di schedule tetapi belum di buat Production Order"

					Case "Column26"
						judulBalon = "MRP"
						isiBalon = "Jumlah Production Plan yang di butuhkan untuk bulan sekarang"

					Case "Column4"
						judulBalon = "Stok bahan Baku"
						isiBalon = "Stok bahan baku yang berjalan sekarang di Gudang Warehouse"

					Case "Column5"
						judulBalon = "Outstanding PR"
						isiBalon = "Outstanding Purchase Requisition berdasarkan Tanggal Delivery dibulan berjalan kebawah"

					Case "Column6"
						judulBalon = "Outstanding PO"
						isiBalon = "Outstanding Purchase Order berdasrkan ETA dibulan berjalan kebawah"

					Case "Column31"
						judulBalon = "Ending Stok"
						isiBalon = "Stok Akhir berdasrkan dari Stok Bahan Baku - Finished PRD - Oustanding PRD + Outstanding PR + Outstanding PO"

					Case "Column8"
						judulBalon = "PRODUCTION PLAN " & bulanTahun
						isiBalon = "Production plan di bulan " & bulanTahun

					Case "Column35"
						judulBalon = "PR Outstanding " & bulanTahun
						isiBalon = "Outstanding PR berdasarkan Tanggal Delivery di bulan " & bulanTahun

					Case "Column36"
						judulBalon = "PP Outstanding " & bulanTahun
						isiBalon = "Outstanding PO berdasarkan Tanggal ETA di bulan " & bulanTahun

					Case "Column40"
						judulBalon = "PR Outstanding " & bulanTahun
						isiBalon = "Outstanding PR berdasarkan Tanggal Delivery di bulan " & bulanTahun

					Case "Column41"
						judulBalon = "PO Outstanding " & bulanTahun
						isiBalon = "Outstanding PO berdasarkan Tanggal ETA di bulan " & bulanTahun

					Case "Column42"
						judulBalon = "Ending Stok dibulan  " & bulanTahun
						isiBalon = "ENDING Stok berdasrkan dari Stok Bahan Baku + Outstanding PR + Outstading PO di " & bulanTahun

					Case "Column43"
						judulBalon = "Ending Stok dibulan  " & bulanTahun
						isiBalon = "ENDING Stok berdasrkan dari Stok Bahan Baku + Outstanding PR + Outstading PO di " & bulanTahun
					Case "Column59"
						judulBalon = "Ending Stok Sementara"
						isiBalon = "Stok Akhir berdasrkan dari Stok Bahan Baku - Finished PRD - Oustanding PRD"
					Case Else
						judulBalon = DataGridView1.Columns(e.ColumnIndex).HeaderText
						isiBalon = "Data " & judulBalon
				End Select

				' ==========================================================
				' LOGIKA PEMBAGI KATA BIAR AUTO ENTER
				' ==========================================================
				Dim kata() As String = isiBalon.Split(" "c)
				Dim teksHasil As String = ""
				For i As Integer = 0 To kata.Length - 1
					teksHasil &= kata(i) & " "
					If (i + 1) Mod 6 = 0 Then
						teksHasil &= vbCrLf
					End If
				Next
				isiBalon = teksHasil.Trim()

				' Hancurkan ToolTip lama agar fresh
				If ToolTip1 IsNot Nothing Then
					ToolTip1.Hide(DataGridView1)
					ToolTip1.Dispose()
				End If

				' Buat ulang objek ToolTip baru
				ToolTip1 = New ToolTip()
				ToolTip1.ToolTipIcon = ToolTipIcon.Info
				ToolTip1.ToolTipTitle = judulBalon
				ToolTip1.AutoPopDelay = Int32.MaxValue

				' Ambil kotak area header kolom
				Dim headerRect As Rectangle = DataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False)

				Dim centerX As Integer = headerRect.Left + (headerRect.Width \ 2)
				Dim centerY As Integer = headerRect.Bottom - 5

				Dim sisaRuangKanan As Integer = DataGridView1.Width - headerRect.Left


				If sisaRuangKanan < 420 Then
					' --- UNTUK 3 KOLOM UJUNG KANAN ---
					ToolTip1.IsBalloon = False ' Ubah jadi KOTAK biasa biar GAK PUNYA EKOR (Anti Meleset)

					' Kita pakai centerX dan centerY biar posisinya ngunci pas di tengah bawah kolom!
					ToolTip1.Show(isiBalon, DataGridView1, centerX, centerY)
				Else
					' --- UNTUK KOLOM NORMAL (SISA KOLOM SEBELAH KIRI) ---
					ToolTip1.IsBalloon = True ' Tetap pakai BALON cakep

					ToolTip1.Show(isiBalon, DataGridView1, centerX, centerY)
				End If
			End If

		End If
	End Sub
	Private Sub DataGridView1_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellMouseLeave
		' JIKA mouse keluar dari area Header, langsung sembunyikan balonnya
		If e.RowIndex = -1 Then
			ToolTip1.Hide(DataGridView1)
			lastHeaderColumnIndex = -1
		End If
	End Sub
	Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
		kosong()
		For Each col As DataGridViewColumn In DataGridView1.Columns
			col.SortMode = DataGridViewColumnSortMode.NotSortable
		Next





		ToolTip1.IsBalloon = True

		ToolTip1.ToolTipIcon = ToolTipIcon.Info


		DataGridView1.ShowCellToolTips = False

		Dim dgvType As Type = DataGridView1.GetType()
		Dim pi As PropertyInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
		pi.SetValue(DataGridView1, True, Nothing)

		Me.Dock = DockStyle.Fill

		btn_bln0.Visible = False
		btn_bln1.Visible = False
		btn_bln1.Visible = False

		btn_bln0.Size = New Size(132, 23)
		btn_bln1.Size = New Size(132, 23)
		btn_bln2.Size = New Size(132, 23)
		btn_bln3.Size = New Size(132, 23)
		btn_bln4.Size = New Size(132, 23)
		btn_bln5.Size = New Size(132, 23)
		btn_bln5.Size = New Size(132, 23)

		DataGridView1.Columns(Cell0).HeaderText = "#"
		DataGridView1.Columns(CellKd_Barang).HeaderText = "Kode Barang"
		DataGridView1.Columns(CellNm_Barang).HeaderText = "Nama Barang"
		DataGridView1.Columns(CellAvg_3Bln).HeaderText = "Avg 3 Bulan (Pcs)"
		DataGridView1.Columns(CellStock_BB).HeaderText = "Stok Bahan Baku"
		DataGridView1.Columns(CellOPRequesition).HeaderText = "Outstanding PR"
		DataGridView1.Columns(CellOPOrder).HeaderText = "Outstanding PO"
		DataGridView1.Columns(CellTotal).HeaderText = "Total Stock + Open PR + Open PO"
		DataGridView1.Columns(CellNBom_1).HeaderText = "MRP - Production Plan "
		DataGridView1.Columns(CellNPPIC_1).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_1).HeaderText = "1"
		DataGridView1.Columns(CellKosong_1).HeaderText = ""
		DataGridView1.Columns(CellNBom_2).HeaderText = "MRP - Production Plan "
		DataGridView1.Columns(CellNPPIC_2).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_2).HeaderText = "2"
		DataGridView1.Columns(CellKosong_2).HeaderText = ""
		DataGridView1.Columns(CellNBom_3).HeaderText = "BoM - Production Plan "
		DataGridView1.Columns(CellNPPIC_3).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_3).HeaderText = "3"
		DataGridView1.Columns(CellKosong_3).HeaderText = ""
		DataGridView1.Columns(CellNBom_4).HeaderText = "BoM - Production Plan "
		DataGridView1.Columns(CellNPPIC_4).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_4).HeaderText = "4"
		DataGridView1.Columns(CellKosong_4).HeaderText = ""
		DataGridView1.Columns(CellNBom_5).HeaderText = "BoM - Production Plan "
		DataGridView1.Columns(CellNPPIC_5).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_5).HeaderText = "5"
		DataGridView1.Columns(CellKosong_5).HeaderText = ""
		DataGridView1.Columns(CellNBom_6).HeaderText = "BoM - Production Plan "
		DataGridView1.Columns(CellNPPIC_6).HeaderText = "PPIC - Production Plan "
		DataGridView1.Columns(CellUrut_6).HeaderText = "6"
		DataGridView1.Columns(CellKosong_6).HeaderText = ""
		'DataGridView1.Columns(CellReferensi).HeaderText = "Referensi"
		DataGridView1.Columns(CellStatus).HeaderText = "Status"

		DataGridView1.Columns(CellCurrentMonth).Visible = False

		DataGridView1.Columns(cellOutstandingPOOrder).DisplayIndex = 4
		DataGridView1.Columns(cellOutstandingPOPlan).DisplayIndex = 4
		DataGridView1.Columns(cellFinishedPO).DisplayIndex = 4
		'DataGridView1.Columns(Cell0).ReadOnly = False

		'If Display_Transaksi_MaterialRequsition.asal = "isi_lv" Then
		'    TxtBarangMasuk_NoFaktur_Leave(TxtBarangMasuk_NoFaktur, e)
		'ElseIf Display_Transaksi_MaterialRequsition.asal = "Ref" Then
		'    Try
		'        OpenConn()

		'        get_no_faktur()

		'        DataGridView1.Rows.Clear()
		'        SQL = "select a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner from EMI_Transaksi_Material_Requsition_Detail a,"
		'        SQL = SQL & "Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and "
		'        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Display_Transaksi_MaterialRequsition.nfak & "' "
		'        SQL = SQL & "group by a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner "
		'        Using Ds = BindingTrans(SQL)
		'            With Ds.Tables("MyTable")
		'                For i As Integer = 0 To .Rows.Count - 1
		'                    DataGridView1.Rows.Add(1)
		'                    DataGridView1.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Barang")
		'                    DataGridView1.Rows(i).Cells(2).Value = .Rows(i).Item("Nama")
		'                    DataGridView1.Rows(i).Cells(3).Value = "0"
		'                    DataGridView1.Rows(i).Cells(4).Value = "0"
		'                    DataGridView1.Rows(i).Cells(5).Value = "0"
		'                    DataGridView1.Rows(i).Cells(6).Value = "0"
		'                    DataGridView1.Rows(i).Cells(7).Value = "0"
		'                    DataGridView1.Rows(i).Cells(8).Value = "0"
		'                    DataGridView1.Rows(i).Cells(9).Value = "0"
		'                    DataGridView1.Rows(i).Cells(10).Value = "0"
		'                    DataGridView1.Rows(i).Cells(11).Value = ""
		'                    DataGridView1.Rows(i).Cells(12).Value = "0"
		'                    DataGridView1.Rows(i).Cells(13).Value = "0"
		'                    DataGridView1.Rows(i).Cells(14).Value = "0"
		'                    DataGridView1.Rows(i).Cells(15).Value = ""
		'                    DataGridView1.Rows(i).Cells(16).Value = "0"
		'                    DataGridView1.Rows(i).Cells(17).Value = "0"
		'                    DataGridView1.Rows(i).Cells(18).Value = "0"
		'                    DataGridView1.Rows(i).Cells(19).Value = ""
		'                    DataGridView1.Rows(i).Cells(20).Value = "0"
		'                    DataGridView1.Rows(i).Cells(21).Value = "0"
		'                    DataGridView1.Rows(i).Cells(22).Value = "0"
		'                    DataGridView1.Rows(i).Cells(23).Value = ""
		'                    DataGridView1.Rows(i).Cells(24).Value = "0"
		'                    DataGridView1.Rows(i).Cells(25).Value = "0"
		'                    DataGridView1.Rows(i).Cells(26).Value = "0"
		'                    DataGridView1.Rows(i).Cells(27).Value = ""
		'                    DataGridView1.Rows(i).Cells(28).Value = "0"
		'                    DataGridView1.Rows(i).Cells(29).Value = "0"
		'                    DataGridView1.Rows(i).Cells(30).Value = "0"
		'                    DataGridView1.Rows(i).Cells(31).Value = ""
		'                    DataGridView1.Rows(i).Cells(32).Value = "NEW"
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

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		'End If

		'Try
		'    OpenConn()

		'    Base_Language.Get_Languages_Global(Bahasa_Pilihan)

		'    Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

		'        Label1.Text = Base_Language.Lang_Jenis_Hewan_Judul
		'        Label2.Text = Base_Language.Lang_Jenis_Hewan_Kode
		'        Label3.Text = Base_Language.Lang_Jenis_Hewan_Keterangan
		'        Label4.Text = Base_Language.Lang_Jenis_Hewan_Kolom

		'        ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Kode, 150, HorizontalAlignment.Left)
		'        ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Keterangan, 725, HorizontalAlignment.Left)
		'        ListView1.View = View.Details

		'

		'        CloseConn()
		'    Catch ex As Exception
		'        CloseConn()
		'        MessageBox.Show(ex.Message)
		'        Exit Sub

		'    End Try
	End Sub

	Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
		If ComboBox1.SelectedIndex = -1 Then
			Exit Sub
		ElseIf ComboBox2.SelectedIndex = -1 Then
			Exit Sub
		ElseIf ComboBox3.SelectedIndex = -1 Then
			Exit Sub
		End If

		Start_Loading(Me)
		'getdata()
		GetDataRix()
		End_Loading(Me)

	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If TxtBarangMasuk_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtBarangMasuk_NoFaktur.Focus() : Exit Sub

		ElseIf ComboBox3.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Error_Lokasi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox3.Focus() : Exit Sub
		ElseIf ComboBox1.Text.Trim.Length = 0 Then
			MessageBox.Show("Bulan Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox1.Focus() : Exit Sub
		ElseIf ComboBox2.Text.Trim.Length = 0 Then
			MessageBox.Show("Tahun Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox2.Focus() : Exit Sub
		End If

		'For a As Integer = 0 To DataGridView1.Rows.Count - 1
		'    If DataGridView1.Rows.Item(a).Cells(Cell0).Value = True Then
		'        Get_Isi_Listview(a)
		'        If LvNBom_1 = 0 Then
		'            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNPPIC_1 = 0 Then
		'            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNBom_2 = 0 Then
		'            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNPPIC_2 = 0 Then
		'            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNBom_3 = 0 Then
		'            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNPPIC_3 = 0 Then
		'            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNBom_4 = 0 Then
		'            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNPPIC_4 = 0 Then
		'            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNBom_5 = 0 Then
		'            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNPPIC_5 = 0 Then
		'            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNBom_6 = 0 Then
		'            MessageBox.Show("Nilai BOM harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        ElseIf LvNPPIC_6 = 0 Then
		'            MessageBox.Show("Nilai PPIC harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            Exit Sub
		'        End If
		'    End If
		'Next

		'Dim fInsert As Boolean = False
		'For a As Integer = 0 To DataGridView1.Rows.Count - 1
		'    If DataGridView1.Rows.Item(a).Cells(Cell0).Value = True Then
		'        fInsert = True
		'        Exit For
		'    Else
		'        fInsert = False
		'    End If
		'Next

		'If fInsert = False Then
		'    MessageBox.Show("Pilih dahulu data yang mau disimpan....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    Exit Sub
		'End If

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If fstatus = "MRP_PPIC" Then
				If CekButtonRole("MRP_PPIC") = "T" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("anda tidak memiliki akses ! !")
					Exit Sub
				End If
			Else
				CloseTrans()
				CloseConn()
				MessageBox.Show("anda tidak memiliki akses ! !")
				Exit Sub

			End If

			'''''If Btn_Refresh.Tag = "&Simpan" Then
			'''''    get_no_faktur()

			'''''    SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
			'''''    SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
			'''''    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & TextBox2.Text & "','" & ComboBox3.Text & "',"
			'''''    SQL = SQL & "'" & arrBulanMM.Item(ComboBox1.SelectedIndex) & "','" & ComboBox2.Text & "'"
			'''''    If CheckBox1.Checked = True Then
			'''''        SQL = SQL & ",'Y')"
			'''''    Else
			'''''        SQL = SQL & ",NULL)"
			'''''    End If
			'''''    ExecuteTrans(SQL)
			'''''Else
			'''''    SQL = "UPDATE EMI_Transaksi_Material_Requsition SET "
			'''''    If CheckBox1.Checked = True Then
			'''''        SQL = SQL & "flag_Referensi = 'Y' "
			'''''    Else
			'''''        SQL = SQL & "flag_Referensi = NULL "
			'''''    End If
			'''''    SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtBarangMasuk_NoFaktur.Text & "'"
			'''''    ExecuteTrans(SQL)
			'''''End If

			'BULAN KE 1
			For c As Integer = 0 To DataGridView1.Rows.Count - 1
				' If DataGridView1.Rows.Item(c).Cells(Cell0).Value = True Then
				Get_Isi_Listview(c)

				Dim fSO As String = ""
				SQL = "select Top(1)a.Lokasi_Gudang from EMI_Kategori_Gudang_PerLokasi a,Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Kategori_Gudang = b.Id_Kategori_Gudang and "
				SQL = SQL & "a.Kode_Stock_Owner = '" & ComboBox3.Text & "' and b.Kode_Barang = '" & LVKd_Barang & "'"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								fSO = .Rows(i).Item("Lokasi_Gudang")
							Next
						Else
							CloseTrans()
							CloseConn()
							MessageBox.Show("Data Tidak ada . . ! !")
							Exit Sub
						End If
					End With
				End Using

				Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
				Dim fthn As Integer = Val(ComboBox2.Text)
				Dim b As String = ""
				Dim x_no_urut_det As String = 0
				Dim NBom_Lama As Double = 0
				Dim NPPIC_Lama As Double = 0

				For index = 0 To arrBulan.Count - 1
					If arrBulan.Item(index) = a Then
						'ComboBox1.SelectedIndex = index
						b = arrBulanMM.Item(index)
					End If
				Next

				'BULAN YANG DIPILIH
				SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
					Else
						dr.Close()
						get_no_faktur()

						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
						SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
						SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & ComboBox3.Text & "',"
						SQL = SQL & "'" & b & "','" & fthn & "'"
						If CheckBox1.Checked = True Then
							SQL = SQL & ",'Y')"
						Else
							SQL = SQL & ",NULL)"
						End If
						ExecuteTrans(SQL)

					End If
				End Using

				SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition_detail a where no_faktur='" & TxtBarangMasuk_NoFaktur.Text & "' "
				SQL = SQL & "And a.kode_barang='" & LVKd_Barang & "' and kode_stock_owner='" & fSO & "'  And kode_perusahaan='" & KodePerusahaan & "' "
				Using dr4 = OpenTrans(SQL)
					If Not dr4.Read Then
						dr4.Close()
						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC, satuan "
						SQL = SQL & ") VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
						SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & 0 & "','" & 0 & "', '" & LvSatuanBarang & "')"
						'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
						'    SQL = SQL & ",'Y')"
						'Else
						'    SQL = SQL & ",NULL)"
						'End If
						ExecuteTrans(SQL)

						SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								x_no_urut_det = "" & Dr("urutan") & ""
							End If
						End Using

						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
						SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
						SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
						ExecuteTrans(SQL)

					End If
				End Using

				'BULAN KE 1
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

				SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
					Else
						dr.Close()
						get_no_faktur()

						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
						SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
						SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & ComboBox3.Text & "',"
						SQL = SQL & "'" & b & "','" & fthn & "'"
						If CheckBox1.Checked = True Then
							SQL = SQL & ",'Y')"
						Else
							SQL = SQL & ",NULL)"
						End If
						ExecuteTrans(SQL)

					End If
				End Using

				If LvUrut_1 = "" Then
					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail (Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC,satuan "
					SQL = SQL & ") VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
					SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_1) & "','" & HilangkanTanda(LvNPPIC_1) & "' , '" & LvSatuanBarang & "' )"
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",'Y')"
					'Else
					'    SQL = SQL & ",NULL)"
					'End If
					ExecuteTrans(SQL)

					SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							x_no_urut_det = "" & Dr("urutan") & ""
						End If
					End Using

					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
					SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
					SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
					ExecuteTrans(SQL)
				Else
					NBom_Lama = 0
					NPPIC_Lama = 0
					SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_1 & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							NBom_Lama = dr("Nilai_Bom")
							NPPIC_Lama = dr("Nilai_PPIC")
						End If
					End Using

					If NBom_Lama <> HilangkanTanda(LvNBom_1) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_1) Then
						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
						SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_1 & "',"
						SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
						SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
						ExecuteTrans(SQL)
					End If

					SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_1) & "' "
					SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_1) & "' "
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",Flag_Referensi = 'Y' "
					'Else
					'    SQL = SQL & ",Flag_Referensi = NULL "
					'End If
					SQL = SQL & "WHERE Urut = '" & LvUrut_1 & "'"
					ExecuteTrans(SQL)
				End If

				'BULAN KE 2
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

				SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
					Else
						dr.Close()
						get_no_faktur()

						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
						SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
						SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & ComboBox3.Text & "',"
						SQL = SQL & "'" & b & "','" & fthn & "'"
						If CheckBox1.Checked = True Then
							SQL = SQL & ",'Y')"
						Else
							SQL = SQL & ",NULL)"
						End If
						ExecuteTrans(SQL)

					End If
				End Using

				If LvUrut_2 = "" Then
					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC, satuan) "
					SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
					SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_2) & "','" & HilangkanTanda(LvNPPIC_2) & "' , '" & LvSatuanBarang & "' )"
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",'Y')"
					'Else
					'    SQL = SQL & ",NULL)"
					'End If
					ExecuteTrans(SQL)

					SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							x_no_urut_det = "" & Dr("urutan") & ""
						End If
					End Using

					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
					SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
					SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
					ExecuteTrans(SQL)
				Else
					NBom_Lama = 0
					NPPIC_Lama = 0
					SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_2 & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							NBom_Lama = dr("Nilai_Bom")
							NPPIC_Lama = dr("Nilai_PPIC")
						End If
					End Using

					If NBom_Lama <> HilangkanTanda(LvNBom_2) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_2) Then
						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
						SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_2 & "',"
						SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
						SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
						ExecuteTrans(SQL)
					End If

					SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_2) & "' "
					SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_2) & "' "
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",Flag_Referensi = 'Y' "
					'Else
					'    SQL = SQL & ",Flag_Referensi = NULL "
					'End If
					SQL = SQL & "WHERE Urut = '" & LvUrut_2 & "'"
					ExecuteTrans(SQL)
				End If

				'BULAN KE 3
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

				SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
					Else
						dr.Close()
						get_no_faktur()

						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
						SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
						SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & ComboBox3.Text & "',"
						SQL = SQL & "'" & b & "','" & fthn & "'"
						If CheckBox1.Checked = True Then
							SQL = SQL & ",'Y')"
						Else
							SQL = SQL & ",NULL)"
						End If
						ExecuteTrans(SQL)

					End If
				End Using

				If LvUrut_3 = "" Then
					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC,satuan) "
					SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
					SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_3) & "','" & HilangkanTanda(LvNPPIC_3) & "', '" & LvSatuanBarang & "') "
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",'Y')"
					'Else
					'    SQL = SQL & ",NULL)"
					'End If
					ExecuteTrans(SQL)

					SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							x_no_urut_det = "" & Dr("urutan") & ""
						End If
					End Using

					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
					SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
					SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
					ExecuteTrans(SQL)
				Else
					NBom_Lama = 0
					NPPIC_Lama = 0
					SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_3 & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							NBom_Lama = dr("Nilai_Bom")
							NPPIC_Lama = dr("Nilai_PPIC")
						End If
					End Using

					If NBom_Lama <> HilangkanTanda(LvNBom_3) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_3) Then
						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
						SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_3 & "',"
						SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
						SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
						ExecuteTrans(SQL)
					End If

					SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_3) & "' "
					SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_3) & "' "
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",Flag_Referensi = 'Y' "
					'Else
					'    SQL = SQL & ",Flag_Referensi = NULL "
					'End If
					SQL = SQL & "WHERE Urut = '" & LvUrut_3 & "'"
					ExecuteTrans(SQL)
				End If

				'BULAN KE 4
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

				SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
					Else
						dr.Close()
						get_no_faktur()

						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
						SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
						SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & ComboBox3.Text & "',"
						SQL = SQL & "'" & b & "','" & fthn & "'"
						If CheckBox1.Checked = True Then
							SQL = SQL & ",'Y')"
						Else
							SQL = SQL & ",NULL)"
						End If
						ExecuteTrans(SQL)

					End If
				End Using

				If LvUrut_4 = "" Then
					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC,satuan) "
					SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
					SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_4) & "','" & HilangkanTanda(LvNPPIC_4) & "', '" & LvSatuanBarang & "')"
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",'Y')"
					'Else
					'    SQL = SQL & ",NULL)"
					'End If
					ExecuteTrans(SQL)

					SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							x_no_urut_det = "" & Dr("urutan") & ""
						End If
					End Using

					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
					SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
					SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
					ExecuteTrans(SQL)
				Else
					NBom_Lama = 0
					NPPIC_Lama = 0
					SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_4 & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							NBom_Lama = dr("Nilai_Bom")
							NPPIC_Lama = dr("Nilai_PPIC")
						End If
					End Using

					If NBom_Lama <> HilangkanTanda(LvNBom_4) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_4) Then
						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
						SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_4 & "',"
						SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
						SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
						ExecuteTrans(SQL)
					End If

					SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_4) & "' "
					SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_4) & "' "
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",Flag_Referensi = 'Y' "
					'Else
					'    SQL = SQL & ",Flag_Referensi = NULL "
					'End If
					SQL = SQL & "WHERE Urut = '" & LvUrut_4 & "'"
					ExecuteTrans(SQL)
				End If

				'BULAN KE 5
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

				SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
					Else
						dr.Close()
						get_no_faktur()

						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
						SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
						SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & ComboBox3.Text & "',"
						SQL = SQL & "'" & b & "','" & fthn & "'"
						If CheckBox1.Checked = True Then
							SQL = SQL & ",'Y')"
						Else
							SQL = SQL & ",NULL)"
						End If
						ExecuteTrans(SQL)

					End If
				End Using

				If LvUrut_5 = "" Then
					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC,satuan) "
					SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
					SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_5) & "','" & HilangkanTanda(LvNPPIC_5) & "', '" & LvSatuanBarang & "') "
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",'Y')"
					'Else
					'    SQL = SQL & ",NULL)"
					'End If
					ExecuteTrans(SQL)

					SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							x_no_urut_det = "" & Dr("urutan") & ""
						End If
					End Using

					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
					SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
					SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
					ExecuteTrans(SQL)
				Else
					NBom_Lama = 0
					NPPIC_Lama = 0
					SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_5 & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							NBom_Lama = dr("Nilai_Bom")
							NPPIC_Lama = dr("Nilai_PPIC")
						End If
					End Using

					If NBom_Lama <> HilangkanTanda(LvNBom_5) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_5) Then
						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
						SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_5 & "',"
						SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
						SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
						ExecuteTrans(SQL)
					End If

					SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_5) & "' "
					SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_5) & "' "
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",Flag_Referensi = 'Y' "
					'Else
					'    SQL = SQL & ",Flag_Referensi = NULL "
					'End If
					SQL = SQL & "WHERE Urut = '" & LvUrut_5 & "'"
					ExecuteTrans(SQL)
				End If

				'BULAN KE 6
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

				SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
				SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						TxtBarangMasuk_NoFaktur.Text = dr("no_faktur")
					Else
						dr.Close()
						get_no_faktur()

						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition(Kode_Perusahaan,No_Faktur,Tanggal,Jam,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi) VALUES("
						SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
						SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','-','" & ComboBox3.Text & "',"
						SQL = SQL & "'" & b & "','" & fthn & "'"
						If CheckBox1.Checked = True Then
							SQL = SQL & ",'Y')"
						Else
							SQL = SQL & ",NULL)"
						End If
						ExecuteTrans(SQL)

					End If
				End Using
				If LvUrut_6 = "" Then
					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Detail(Kode_Perusahaan,No_Faktur,Bulan,Tahun,Kode_Stock_Owner,Kode_Barang,Nilai_Bom,Nilai_PPIC,satuan) "
					SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & b & "','" & fthn & "',"
					SQL = SQL & "'" & fSO & "','" & LVKd_Barang & "','" & HilangkanTanda(LvNBom_6) & "','" & HilangkanTanda(LvNPPIC_6) & "', '" & LvSatuanBarang & "')"
					'If DataGridView1.Rows.Item(c).Cells(CellReferensi).Value = True Then
					'    SQL = SQL & ",'Y')"
					'Else
					'    SQL = SQL & ",NULL)"
					'End If
					ExecuteTrans(SQL)

					SQL = "select IDENT_CURRENT('EMI_Transaksi_Material_Requsition_Detail') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							x_no_urut_det = "" & Dr("urutan") & ""
						End If
					End Using

					SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
					SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & x_no_urut_det & "',0,0,'INSERT',"
					SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
					ExecuteTrans(SQL)
				Else
					NBom_Lama = 0
					NPPIC_Lama = 0
					SQL = "select Nilai_Bom,Nilai_PPIC,Urut from EMI_Transaksi_Material_Requsition_Detail where Urut = '" & LvUrut_6 & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							NBom_Lama = dr("Nilai_Bom")
							NPPIC_Lama = dr("Nilai_PPIC")
						End If
					End Using

					If NBom_Lama <> HilangkanTanda(LvNBom_6) Or NPPIC_Lama <> HilangkanTanda(LvNPPIC_6) Then
						SQL = "INSERT INTO EMI_Transaksi_Material_Requsition_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_BOM,Jenis,UserID,"
						SQL = SQL & "Tanggal,Jam) VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvUrut_6 & "',"
						SQL = SQL & "'" & NPPIC_Lama & "','" & NBom_Lama & "','UPDATE',"
						SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "')"
						ExecuteTrans(SQL)
					End If

					SQL = "UPDATE EMI_Transaksi_Material_Requsition_Detail SET Nilai_Bom = '" & HilangkanTanda(LvNBom_6) & "' "
					SQL = SQL & ",Nilai_PPIC = '" & HilangkanTanda(LvNPPIC_6) & "' WHERE Urut = '" & LvUrut_6 & "'"
					ExecuteTrans(SQL)
				End If

				'End If
			Next

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		kosong()
	End Sub

	Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit

		arrCellInputPPIC.AddRange(New Object() {
						CellNPPIC_1, CellNPPIC_2, CellNPPIC_3, CellNPPIC_4,
						CellNPPIC_5, CellNPPIC_6
		})

		Get_Isi_Listview(DataGridView1.CurrentRow.Index)
		If IsNumeric(LvNBom_1) = False Or Val(LvNBom_1) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNBom_1).Value = 0
		ElseIf IsNumeric(LvNPPIC_1) = False Or Val(LvNPPIC_1) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNPPIC_1).Value = 0
		ElseIf IsNumeric(LvNBom_2) = False Or Val(LvNBom_2) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNBom_2).Value = 0
		ElseIf IsNumeric(LvNPPIC_2) = False Or Val(LvNPPIC_2) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNPPIC_2).Value = 0
		ElseIf IsNumeric(LvNBom_3) = False Or Val(LvNBom_3) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNBom_3).Value = 0
		ElseIf IsNumeric(LvNPPIC_3) = False Or Val(LvNPPIC_3) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNPPIC_3).Value = 0
		ElseIf IsNumeric(LvNBom_4) = False Or Val(LvNBom_4) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNBom_4).Value = 0
		ElseIf IsNumeric(LvNPPIC_4) = False Or Val(LvNPPIC_4) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNPPIC_4).Value = 0
		ElseIf IsNumeric(LvNBom_5) = False Or Val(LvNBom_5) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNBom_5).Value = 0
		ElseIf IsNumeric(LvNPPIC_5) = False Or Val(LvNPPIC_5) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNPPIC_5).Value = 0
		ElseIf IsNumeric(LvNBom_6) = False Or Val(LvNBom_6) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNBom_6).Value = 0
		ElseIf IsNumeric(LvNPPIC_6) = False Or Val(LvNPPIC_6) < 0 Then
			DataGridView1.CurrentRow.Cells(CellNPPIC_6).Value = 0
		End If

		'======================
		'=     SET FORMAT     =
		'======================
		If arrCellInputPPIC.Contains(DataGridView1.CurrentCell.ColumnIndex) Then

			Dim cellKuantity As String = DataGridView1.CurrentCell.Value

			If cellKuantity.Contains(",") Then
				MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				DataGridView1.CurrentCell.Value = Format(0, "N2")
				Exit Sub
			End If

			Dim nilai As Decimal = Decimal.Parse(cellKuantity)
			Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

			DataGridView1.CurrentCell.Value = formattedValue
		End If

	End Sub

	Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter

		arrCellInputPPIC.AddRange(New Object() {
						CellNPPIC_1, CellNPPIC_2, CellNPPIC_3, CellNPPIC_4,
						CellNPPIC_5, CellNPPIC_6
		})

		If arrCellInputPPIC.Contains(DataGridView1.CurrentCell.ColumnIndex) Then
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

		arrCellInputPPIC.AddRange(New Object() {
						CellNPPIC_1, CellNPPIC_2, CellNPPIC_3, CellNPPIC_4,
						CellNPPIC_5, CellNPPIC_6
		})

		If arrCellInputPPIC.Contains(DataGridView1.CurrentCell.ColumnIndex) Then
			Dim cellKuantity As String = DataGridView1.CurrentCell.Value

			If cellKuantity = "" Then
				Exit Sub
			End If

			Dim nilai As Decimal = Decimal.Parse(cellKuantity)
			Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

			DataGridView1.CurrentCell.Value = formattedValue

		End If
	End Sub

	Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
		If ComboBox1.SelectedIndex = -1 Then
			Exit Sub
		ElseIf ComboBox2.SelectedIndex = -1 Then
			Exit Sub
		ElseIf ComboBox3.SelectedIndex = -1 Then
			Exit Sub
		End If

		Start_Loading(Me)
		'getdata()
		GetDataRix()
		End_Loading(Me)
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		kosong()
	End Sub

	Private Sub Btn_Realese_Click(sender As Object, e As EventArgs) Handles Btn_Realese.Click
		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If fstatus = "MRP_PPIC" Then
				If CekButtonRole("MRP_Realease") = "T" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("anda tidak memiliki akses ! !")
					Exit Sub
				End If
			Else
				CloseTrans()
				CloseConn()
				MessageBox.Show("anda tidak memiliki akses ! !")
				Exit Sub

			End If

			Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
			Dim fthn As Integer = Val(ComboBox2.Text)
			Dim b As String = ""

			'--- 1
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
			SQL = "Select no_faktur,flag_validasi,flag_validasi_ppic from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then

					If fstatus = "MRP_Formulator" Then
						If General_Class.CekNULL(dr("flag_validasi")) = "Y" Then
							dr.Close()
							CloseConn()
							MessageBox.Show("Data sudah pernah di submit sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End If

					If fstatus = "MRP_PPIC" Then
						If General_Class.CekNULL(dr("flag_validasi_ppic")) = "Y" Then
							dr.Close()
							CloseConn()
							MessageBox.Show("Data sudah pernah di submit sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End If

					no_faktur = dr("no_faktur")
				Else

					dr.Close()
					CloseConn()
					MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			If fstatus = "MRP_PPIC" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'VERIFICATION' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)

			ElseIf fstatus = "MRP_Formulator" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'SUBMITTED' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)
			End If

			'--- 2
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

			SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					no_faktur = dr("no_faktur")
				Else

					dr.Close()
					CloseConn()
					MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			If fstatus = "MRP_PPIC" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'VERIFICATION' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)

			ElseIf fstatus = "MRP_Formulator" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'SUBMITTED' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)
			End If

			'--- 3
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

			SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					no_faktur = dr("no_faktur")
				Else

					dr.Close()
					CloseConn()
					MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			If fstatus = "MRP_PPIC" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'VERIFICATION' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)

			ElseIf fstatus = "MRP_Formulator" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'SUBMITTED' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)
			End If

			'--- 4
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

			SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					no_faktur = dr("no_faktur")
				Else

					dr.Close()
					CloseConn()
					MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			If fstatus = "MRP_PPIC" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'VERIFICATION' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)

			ElseIf fstatus = "MRP_Formulator" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'SUBMITTED' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)
			End If

			'--- 5
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

			SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					no_faktur = dr("no_faktur")
				Else

					dr.Close()
					CloseConn()
					MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			If fstatus = "MRP_PPIC" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'VERIFICATION' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)

			ElseIf fstatus = "MRP_Formulator" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'SUBMITTED' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)
			End If

			'--- 6
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

			SQL = "Select no_faktur from EMI_Transaksi_Sales_Forecasting a where bulan='" & b & "' and tahun ='" & fthn & "' "
			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					no_faktur = dr("no_faktur")
				Else

					dr.Close()
					CloseConn()
					MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					no_faktur = dr("no_faktur")
				Else

					dr.Close()
					CloseConn()
					MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			If fstatus = "MRP_PPIC" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi_PPIC = 'Y',User_Validasi_PPIC = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi_PPIC = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi_PPIC = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'VERIFICATION' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)

			ElseIf fstatus = "MRP_Formulator" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi = 'Y',User_Validasi = '" & UserID & "',"
				SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
				SQL = SQL & "Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "', "
				SQL = SQL & "Status_Data = 'SUBMITTED' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)
			End If

			Cmd.Transaction.Commit()
			CloseConn()
			MessageBox.Show("Data berhasil divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		kosong()
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnUnRelease.Click
		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			If fstatus = "MRP_PPIC" Then
				If CekButtonRole("MRP_Unrealease") = "T" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("anda tidak memiliki akses ! !")
					Exit Sub
				End If
			Else
				CloseTrans()
				CloseConn()
				MessageBox.Show("anda tidak memiliki akses ! !")
				Exit Sub

			End If

			Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
			Dim fthn As Integer = Val(ComboBox2.Text)
			Dim b As String = ""

			'--- 1
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
			SQL = "Select no_faktur,flag_validasi,flag_validasi_ppic from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then

					If fstatus = "MRP_Formulator" Then
						If General_Class.CekNULL(dr("flag_validasi")) = "" Then
							dr.Close()
							CloseConn()
							MessageBox.Show("Data belum pernah di submit!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End If

					If fstatus = "MRP_PPIC" Then
						If General_Class.CekNULL(dr("flag_validasi_ppic")) = "" Then
							dr.Close()
							CloseConn()
							MessageBox.Show("Data belum pernah di submit!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End If

					no_faktur = dr("no_faktur")
				Else

					dr.Close()
					CloseConn()
					MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			If fstatus = "MRP_PPIC" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
				SQL = SQL & "Tanggal_Validasi_PPIC = null,"
				SQL = SQL & "Jam_Validasi_PPIC = null, "
				SQL = SQL & "Status_Data = 'SUBMITED' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)

			ElseIf fstatus = "MRP_Formulator" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
				SQL = SQL & "Tanggal_Validasi = null,"
				SQL = SQL & "Jam_Validasi = null, "
				SQL = SQL & "Status_Data = 'UNSUBMITTED' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)
			End If

			'--- 2
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

			SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
			SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					no_faktur = dr("no_faktur")
				Else

					dr.Close()
					CloseConn()
					MessageBox.Show("Terdapat Data Tidak Lengkap . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			If fstatus = "MRP_PPIC" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
				SQL = SQL & "Tanggal_Validasi_PPIC = null,"
				SQL = SQL & "Jam_Validasi_PPIC = null, "
				SQL = SQL & "Status_Data = 'SUBMITED' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)

			ElseIf fstatus = "MRP_Formulator" Then
				SQL = "update EMI_Transaksi_Material_Requsition set "
				SQL = SQL & "Flag_Validasi = null,User_Validasi = null,"
				SQL = SQL & "Tanggal_Validasi = null,"
				SQL = SQL & "Jam_Validasi = null, "
				SQL = SQL & "Status_Data = 'UNSUBMITTED' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
				ExecuteTrans(SQL)
			End If

			''--- 3
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

			'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
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

			'If fstatus = "MRP_PPIC" Then
			'    SQL = "update EMI_Transaksi_Material_Requsition set "
			'    SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = null,"
			'    SQL = SQL & "Jam_Validasi_PPIC = null, "
			'    SQL = SQL & "Status_Data = 'SUBMITED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'ElseIf fstatus = "MRP_Formulator" Then
			'    SQL = "update EMI_Transaksi_Material_Requsition set "
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

			'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
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

			'If fstatus = "MRP_PPIC" Then
			'    SQL = "update EMI_Transaksi_Material_Requsition set "
			'    SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = null,"
			'    SQL = SQL & "Jam_Validasi_PPIC = null, "
			'    SQL = SQL & "Status_Data = 'SUBMITED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'ElseIf fstatus = "MRP_Formulator" Then
			'    SQL = "update EMI_Transaksi_Material_Requsition set "
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

			'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
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

			'If fstatus = "MRP_PPIC" Then
			'    SQL = "update EMI_Transaksi_Material_Requsition set "
			'    SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = null,"
			'    SQL = SQL & "Jam_Validasi_PPIC = null, "
			'    SQL = SQL & "Status_Data = 'SUBMITED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'ElseIf fstatus = "MRP_Formulator" Then
			'    SQL = "update EMI_Transaksi_Material_Requsition set "
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

			'SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & b & "' and tahun ='" & fthn & "' "
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

			'If fstatus = "MRP_PPIC" Then
			'    SQL = "update EMI_Transaksi_Material_Requsition set "
			'    SQL = SQL & "Flag_Validasi_PPIC = null,User_Validasi_PPIC = null,"
			'    SQL = SQL & "Tanggal_Validasi_PPIC = null,"
			'    SQL = SQL & "Jam_Validasi_PPIC = null, "
			'    SQL = SQL & "Status_Data = 'SUBMITED' "
			'    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "bulan='" & b & "' and tahun ='" & fthn & "' "
			'    ExecuteTrans(SQL)

			'ElseIf fstatus = "MRP_Formulator" Then
			'    SQL = "update EMI_Transaksi_Material_Requsition set "
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
		kosong()
	End Sub

	Public Sub TxtBarangMasuk_NoFaktur_Leave(sender As Object, e As EventArgs) Handles TxtBarangMasuk_NoFaktur.Leave
		If TxtBarangMasuk_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtBarangMasuk_NoFaktur.Focus() : Exit Sub
		End If
		Dim ada_data As Boolean = False
		Try
			OpenConn()

			SQL = "select No_Faktur, Tanggal, Keterangan, Lokasi, Bulan, Tahun, flag_referensi, Flag_validasi_PPIC from EMI_Transaksi_Material_Requsition where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Faktur = '" & TxtBarangMasuk_NoFaktur.Text & "'"
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					ada_data = True
					'TextBox2.Text = dr("Keterangan")
					'DateTimePicker1.Value = Format(dr("Tanggal"), "dd-MMMM-yyyy")
					ComboBox3.Text = dr("Lokasi")
					For index = 0 To arrBulanMM.Count - 1
						If arrBulanMM.Item(index) = dr("Bulan") Then
							ComboBox1.SelectedIndex = index
						End If
					Next

					If General_Class.CekNULL(dr("Flag_referensi")) = "" Then
						CheckBox1.Checked = False
					Else
						CheckBox1.Checked = True
					End If

					'======================================================
					'=     DISABLE KETERANGAN JIKA SUDAH RELEASE SAJA     =
					'======================================================
					'If fstatus = "MRP_PPIC" Then

					'    If General_Class.CekNULL(dr("Flag_validasi_PPIC")) = "Y" Then
					'        TextBox2.Enabled = False
					'    Else
					'        TextBox2.Enabled = True
					'    End If

					'End If

					ComboBox1.Text = dr("Bulan")
					ComboBox2.Text = dr("Tahun")
					'DateTimePicker1.Enabled = False
					ComboBox1.Enabled = False
					ComboBox2.Enabled = False
					ComboBox3.Enabled = True
					Btn_Simpan.Tag = "&Refresh"
				Else
					dr.Close()
					get_no_faktur()
					'TextBox2.Text = ""
					DateTimePicker1.Value = Now
					ComboBox3.SelectedIndex = -1
					ComboBox1.SelectedIndex = -1
					ComboBox2.SelectedIndex = -1
					'DateTimePicker1.Enabled = True
					'TextBox2.Enabled = True
					ComboBox1.Enabled = False
					ComboBox2.Enabled = False
					ComboBox3.Enabled = True
					CheckBox1.Checked = False
					Btn_Simpan.Tag = "&Simpan"
				End If
			End Using

			DataGridView1.Rows.Clear()
			Arrbarang.Clear()
			Arrlokasi.Clear()
			ArrNama.Clear()
			SQL = "select a.No_Faktur,c.Bulan,c.Tahun,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner, b.good_stock,b.satuan "
			SQL = SQL & "from EMI_Transaksi_Material_Requsition_Detail a,Barang b , EMI_Transaksi_Material_Requsition c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang  "
			SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Faktur "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TxtBarangMasuk_NoFaktur.Text & "' "
			SQL = SQL & "group by a.No_Faktur,c.Bulan,c.Tahun,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner,b.good_stock, b.satuan "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						Arrbarang.Add(.Rows(i).Item("Kode_Barang"))
						Arrlokasi.Add(.Rows(i).Item("Kode_Stock_Owner"))
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
			get_barang()
		End If
	End Sub

	Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
		If ComboBox1.SelectedIndex = -1 Then
			Exit Sub
		ElseIf ComboBox2.SelectedIndex = -1 Then
			Exit Sub
		End If

		'Start_Loading(Me)
		''getdata()
		'GetDataRix()
		'End_Loading(Me)
	End Sub

	Private Sub DateTimePicker1_CloseUp(sender As Object, e As EventArgs) Handles DateTimePicker1.CloseUp
		If DateTimePicker1.Value = Nothing Then Exit Sub
		'If ComboBox3.SelectedIndex = -1 Then Exit Sub

		Dim selectedDate As Date = DateTimePicker1.Value
		Dim selectedMonthName As String = selectedDate.ToString("MMMM", New Globalization.CultureInfo("id-ID"))
		Dim selectedYear As Integer = selectedDate.Year

		'If selectedYear < tahun_awal Or selectedYear > tahun_akhir Then
		'    MessageBox.Show("Tahun harus dalam rentang dua tahun dari tahun ini.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    Exit Sub
		'End If

		ComboBox1.SelectedItem = selectedMonthName
		ComboBox2.SelectedItem = selectedYear
	End Sub

	'==============================================================================================================
	'==============================================================================================================
	'==============================================================================================================
	Private Sub GetDataRix()
		DataGridView1.Rows.Clear()

		DataGridView1.Columns(Cell0).HeaderText = "#"
		DataGridView1.Columns(CellKd_Barang).HeaderText = "Kode Barang"
		DataGridView1.Columns(CellNm_Barang).HeaderText = "Nama Barang"
		DataGridView1.Columns(CellAvg_3Bln).HeaderText = "Avg 3 Bulan (Pcs)"
		DataGridView1.Columns(CellStock_BB).HeaderText = "Stok Bahan Baku"
		DataGridView1.Columns(CellOPRequesition).HeaderText = "Outstanding PR"
		DataGridView1.Columns(CellOPOrder).HeaderText = "Outstanding PO"
		DataGridView1.Columns(CellTotal).HeaderText = "Total Stock + Open PR + Open PO"

		Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
		Dim fthn As Integer = Val(ComboBox2.Text)
		Dim panggil_databulan As String = ""
		Dim panggil_datatahun As String = ""

		Dim listBulan1 As New List(Of String)

		listBulan1.Add(a.ToString("00"))

		Dim b As String = ""

		Dim BulanAwal As String = ""
		Dim kondisi As New List(Of String)

		Dim kondisiRix As New List(Of String)

		kondisi.Add("(b.bulan='" & a.ToString("00") & "' AND b.Tahun='" & fthn & "')")

		kondisiRix.Add("(a.bulan='" & a.ToString("00") & "' AND a.Tahun='" & fthn & "')")

		BulanAwal = arrBulanMM.Item(arrBulan.Item(ComboBox1.SelectedIndex) - 1)

		' Temukan nama bulan yang sesuai
		'For index = 0 To arrBulan.Count - 1
		'	If arrBulan.Item(index) = a Then
		'		b = ComboBox1.Items(index)

		'		panggil_databulan = arrBulanMM.Item(index)
		'		panggil_datatahun = fthn

		'	End If
		'Next

		Dim index_Bulan As Integer = arrBulan.IndexOf(a.ToString) ' LANGSUNG GUNAKAN INDEX OF DARIPADA LOOP 1 1
		If index_Bulan <> -1 Then
			b = ComboBox1.Items(index_Bulan)
			panggil_databulan = arrBulanMM.Item(index_Bulan)
			panggil_datatahun = fthn
		End If

		btn_bln0.Text = "Detail " & b & " " & fthn
		btn_bln0.Tag = a.ToString("00") & "|" & fthn
		btn_bln0.Enabled = True

		For i As Integer = 1 To 6
			' Perbarui nilai bulan dan tahun
			If a = 12 Then
				a = 1
				fthn = fthn + 1
			Else
				a = a + 1

				'  a = a
			End If

			kondisi.Add("(b.bulan='" & a.ToString("00") & "' AND b.Tahun='" & fthn & "')")

			kondisiRix.Add("(a.bulan='" & a.ToString("00") & "' AND a.Tahun='" & fthn & "')")

			listBulan1.Add(a.ToString("00"))

			Dim btn As System.Windows.Forms.Button = CType(Me.Controls("btn_bln" & i), System.Windows.Forms.Button)



			Dim index_Bulan2 As Integer = arrBulan.IndexOf(a.ToString) ' LANGSUNG GUNAKAN INDEX OF DARIPADA LOOP 1 1
			If index_Bulan2 <> -1 Then
				b = ComboBox1.Items(index_Bulan2)
				If i = 1 Then
					panggil_databulan = arrBulanMM.Item(index_Bulan2)
					panggil_datatahun = fthn
				End If
			End If

			btn.Text = "Detail " & b & " " & fthn
			btn.Tag = a.ToString("00") & "|" & fthn
			btn.Enabled = True

			'Dim cellNBom As Integer = CType(Me.GetType().GetField("CellNBom_" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
			'Dim CellNPPIC As Integer = CType(Me.GetType().GetField("CellNPPIC_" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
			'Dim CellUrut As Integer = CType(Me.GetType().GetField("CellUrut_" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
			'Dim CellKosong As Integer = CType(Me.GetType().GetField("CellKosong_" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)

			'Dim cellPRoutStanding As Integer = CType(Me.GetType().GetField("CellOPRequesition_" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
			'Dim cellPOoutStanding As Integer = CType(Me.GetType().GetField("CellOPOrder_" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
			'Dim cellRequireStok As Integer = CType(Me.GetType().GetField("cellRequireStok_" & i, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)

			Dim cellNBom As Integer = dictNBom(i)
			Dim CellNPPIC As Integer = dictPPIC(i)
			Dim CellUrut As Integer = dictUrut(i)
			Dim CellKosong As Integer = dictKosong(i)

			Dim cellPRoutStanding As Integer = dictOPRequesition(i)
			Dim cellPOoutStanding As Integer = dictOPOrder(i)
			Dim cellRequireStok As Integer = dictRequireStok(i)

			' Set header kolom DataGridView
			DataGridView1.Columns(cellNBom).HeaderText = "MRP - Production Plan " & b & " - " & fthn
			'     DataGridView1.Columns(CellNPPIC).HeaderText = "PPIC - Production Plan " & b & " - " & fthn
			DataGridView1.Columns(CellUrut).HeaderText = i
			DataGridView1.Columns(CellKosong).HeaderText = ""

			DataGridView1.Columns(cellPRoutStanding).HeaderText = "Outstanding PR " & b & " - " & fthn
			DataGridView1.Columns(cellPOoutStanding).HeaderText = "Outstanding PO " & b & " - " & fthn
			DataGridView1.Columns(cellRequireStok).HeaderText = "Ending Stok " & b & " - " & fthn

			Try
				OpenConn()

				Dim listFilter As New List(Of String)

				listFilter.Add("(a.bulan = " & a.ToString("00") & " AND a.tahun = " & fthn & ")")
				Dim filterBulanTahun As String = String.Join(" OR ", listFilter)
				'1
				RefreshForecastingSemiFG(
						KodePerusahaan,
						filterBulanTahun,
						UserID,
						"GET MRP"
				)
				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

		Next

		Dim listBulan As String = "'" & String.Join("','", listBulan1) & "'"

		DataGridView1.Columns(CellStatus).HeaderText = "Status"
		Dim filterSQL As String = String.Join(" OR ", kondisi)

		Dim filterSQL2 As String = String.Join(" OR ", kondisiRix)
		Dim fLoad As Boolean = False

		Try
			OpenConn()

			get_no_faktur()

			Arrbarang.Clear()
			Arrlokasi.Clear()
			ArrNama.Clear()



			Dim pesan As String = ""
			Dim tidakAdaFormula As Boolean = False ' True jika ditemukan ada yang Flag_Invalid = "Y"



			Dim SQL As String = $"
		with cte as  (
		select distinct a.Kode_Perusahaan,kode_barang,nama,No_Faktur,Flag_Tampil_Production_Plan,  Flag_Raw_Material, Flag_Finished_Good, Flag_Invalid from Barang  a
		inner join emi_Group_jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis
		outer apply (
			select top(1)  z.no_faktur,y.Flag_Invalid from N_EMI_Transaksi_Formulator_Binding x
				inner join N_EMI_Transaksi_Formulator_Binding_Detail y  on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur
				inner join  EMI_Transaksi_Formulator z on y.Kode_Perusahaan = z.Kode_Perusahaan and y.No_Formulator = z.No_Faktur
							and z.Status is null
			where x.Status is null
			and x.Flag_Validasi_Main = 'Y'
			and x.Kode_Perusahaan = a.Kode_Perusahaan  and x.Kode_Barang = a.Kode_Barang_Inq
			and z.Flag_Validasi_Formula_Produksi_BOD = 'Y'
			and y.No_Prioritas = 1
			and a.Flag_Tampil_Production_Plan = 'Y'
			and z.Flag_Deprecated_Binding is null
			order by x.tanggal desc ,x.jam desc ,y.No_Prioritas asc
		)   c
		where a.Flag_Tampil_Production_Plan = 'Y'
		)
		select kode_perusahaan,kode_barang,nama,no_faktur,flag_tampil_production_plan,flag_invalid From cte a
		where (flag_raw_material = 'Y' or Flag_Finished_Good =  'Y')
		and kode_barang in (
			select a.Kode_Barang from  EMI_Transaksi_Sales_Forecasting_Detail a
					 inner join EMI_Transaksi_Sales_Forecasting b on a.Kode_Perusahaan = b.Kode_Perusahaan
					 and a.No_Faktur = b.No_Faktur and b.Status is null
			and a.Flag_Validasi_PPIC = 'Y'
			AND ({filterSQL}) and a.kode_perusahaan = '{KodePerusahaan}'
			)
	"

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read()
					If General_Class.CekNULL(Dr("flag_invalid")) = "Y" Then
						tidakAdaFormula = True

						pesan &= $"- {Dr("kode_barang")} ({Dr("nama")})" & Environment.NewLine
					End If
				Loop
			End Using


			If tidakAdaFormula = True Then
				CloseTrans()
				CloseConn()
				Dim pesanLengkap As String = "Gagal menampilkan karena ada beberapa barang yang belum ada formula default : :" & Environment.NewLine & Environment.NewLine & pesan
				MessageBox.Show(pesanLengkap, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub

			End If








			'Dim bolehlewat As Boolean = N_EMI_Fun_Cek_Formula_Default(filterSQL2, KodePerusahaan)

			'' Jika statusAman bernilai False (berarti ada yang invalid dan MessageBox sudah muncul)
			'If Not bolehlewat Then
			'	CloseTrans()
			'	CloseConn()
			'	Exit Sub ' Stop transaksi di sini
			'End If




			SQL = $"
					with cte as (

					select distinct 
					isnull((select top 1 c.No_Faktur
					from N_EMI_Transaksi_Formulator_Binding a
					inner join N_EMI_Transaksi_Formulator_Binding_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join Emi_Transaksi_Formulator c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Formulator = c.No_Faktur and c.Status is null
					where a.Status is NULL
					and a.Flag_Validasi_Main = 'Y'
					and b.no_prioritas = 1
					and a.Kode_Perusahaan = x.kode_perusahaan
					and a.Kode_Barang = x.kode_barang_inq
					order by a.Tanggal DESC, a.Jam DESC, b.No_Prioritas ASC),'') as kode_formula, x.kode_barang_inq, x.kode_perusahaan
					from barang x, emI_group_jenis y where x.kode_perusahaan=y.kode_perusahaan and x.id_group_jenis=y.id_group_jenis and (y.Flag_Finished_Good='Y' or y.Flag_Semi_FG='Y')
				  

					), cte_b as(

					SELECT e.Kode_Stock_Owner,e.Kode_Barang,c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis
					FROM EMI_Transaksi_Sales_Forecasting a
					INNER JOIN EMI_Transaksi_Sales_Forecasting_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur
					INNER JOIN barang bb ON b.kode_perusahaan=bb.kode_perusahaan and b.kode_barang=bb.kode_barang and b.kode_stock_owner=bb.kode_stock_owner
					INNER JOIN cte bc ON bb.kode_perusahaan=bc.kode_perusahaan and bb.kode_barang_inq=bc.kode_barang_inq
					INNER JOIN Emi_Transaksi_Formulator d ON d.Kode_Perusahaan = bc.Kode_Perusahaan AND d.No_Faktur =  bc.Kode_Formula
					INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan e ON d.Kode_Perusahaan = e.Kode_Perusahaan AND d.No_Faktur = e.No_Faktur
					INNER JOIN barang c ON e.Kode_Perusahaan = c.Kode_Perusahaan AND e.Kode_Stock_Owner = c.Kode_Stock_Owner AND e.Kode_Barang = c.Kode_Barang
					WHERE a.Kode_Perusahaan='{KodePerusahaan}' AND a.lokasi= '{ComboBox3.Text}' AND ({filterSQL})
					AND b.Flag_Validasi='Y'
					AND b.Flag_Validasi_PPIC='Y'
					
					AND a.status is null 
					and d.status is null
					GROUP BY e.Kode_Stock_Owner,e.Kode_Barang,c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis

					Union all

					SELECT e.Kode_Stock_Owner,e.Kode_Barang,c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis
					FROM EMI_Transaksi_Sales_Forecasting a
					INNER JOIN EMI_Transaksi_Sales_Forecasting_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur
					INNER JOIN Emi_Transaksi_Formulator d ON d.Kode_Perusahaan = b.Kode_Perusahaan
					INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan e ON d.Kode_Perusahaan = e.Kode_Perusahaan AND d.No_Faktur = e.No_Faktur


					INNER JOIN N_EMI_Production_Plan_Schedule_Detail f on b.kode_perusahaan =  f.kode_perusahaan and b.urut = f.urut_production_plan and f.kode_formula = d.no_faktur
					INNER JOIN N_EMI_Production_Plan_Schedule g on f.kode_perusahaan = g.kode_perusahaan and f.no_transaksi = g.no_transaksi and g.status is null



					INNER JOIN barang c ON e.Kode_Perusahaan = c.Kode_Perusahaan AND e.Kode_Stock_Owner = c.Kode_Stock_Owner AND e.Kode_Barang = c.Kode_Barang
					WHERE a.Kode_Perusahaan='{KodePerusahaan}' AND a.lokasi= '{ComboBox3.Text}' AND ({filterSQL})
					AND b.Flag_Validasi='Y'
					AND b.Flag_Validasi_PPIC='Y'
					and a.Status is null
					and d.Status is null 
					
					GROUP BY e.Kode_Stock_Owner,e.Kode_Barang,c.Nama, c.Kode_Perusahaan, c.Id_Group_Jenis

					)
					select distinct '1' as asal, a.Kode_Stock_Owner,a.Kode_Barang,a.Nama from cte_b a, EMI_Group_Jenis b where a.kode_perusahaan=b.Kode_Perusahaan and a.id_group_jenis=b.Id_Group_Jenis
					and b.Flag_Raw_Material='Y'

					Union all

					Select  '2' as asal, d.Kode_Stock_Owner,c.Kode_Bahan As Kode_Barang,d.Nama from
					EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_Detail b, barang_detail_bahan_penolong c, barang d, barang e
					where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And b.Kode_Perusahaan = c.Kode_Perusahaan
					And b.Kode_Perusahaan=e.Kode_Perusahaan And b.Kode_barang=e.Kode_Barang And b.kode_stock_owner=e.kode_stock_owner
					And e.Kode_Barang_inq = c.Kode_Barang And c.Kode_Perusahaan = d.Kode_Perusahaan
					And c.Kode_Bahan = d.Kode_Barang And b.Kode_Stock_Owner = d.Kode_Stock_Owner And
					a.Kode_Perusahaan='{KodePerusahaan}' AND a.lokasi= '{ComboBox3.Text}' AND ({filterSQL})
					and b.Flag_Validasi = 'Y' and b.Flag_Validasi_PPIC = 'Y'
					and a.status is null 
					group by d.Kode_Stock_Owner, c.Kode_Bahan, d.Nama

					order by asal,nama
				"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows().Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Arrbarang.Add(.Rows(i).Item("kode_barang"))
							Arrlokasi.Add(.Rows(i).Item("kode_stock_owner"))
							ArrNama.Add(.Rows(i).Item("nama"))
						Next
					Else
						CloseConn()
						MessageBox.Show("Tidak ada data forecasting pada bulan " & ComboBox1.Text & " " & ComboBox2.Text)
						Exit Sub
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

			End_Loading(Me)
		End Try

		Get_Barang_Rix(BulanAwal, panggil_datatahun, filterSQL2)

	End Sub

	Dim dictSatuanDisplay As New Dictionary(Of String, String)

	Public Sub Get_Barang_Rix(ByVal bulanSaatIni As String, ByVal tahunSaatIni As String, ByVal filterBulan As String)

		Dim akses_ubah As String = "Y"

		'Try
		'    OpenConn()

		'    If CekButtonRole("MRP_PPIC") = "Y" Then
		'        akses_ubah = "Y"
		'    End If

		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		Try
			OpenConn()

			'cek pointt terbaru dan bener

			Dim dictPRBulanan As New Dictionary(Of String, Double)
			Dim dictPOBulanan As New Dictionary(Of String, Double)
			Dim dictPRD As New Dictionary(Of String, Double)
			Dim dictOSPRD As New Dictionary(Of String, Double)

			Dim tglAwal As Date = New Date(tahunSaatIni, bulanSaatIni, 1)
			Dim tglAwalStr As String = Format(tglAwal, "yyyy-MM-dd")

			Dim batas As Integer = CInt(tahunSaatIni) * 100 + CInt(bulanSaatIni)



			SQL = "SELECT kode_barang, "
			SQL = SQL & "ISNULL(SUM(jumlah),0) AS jumlah "
			SQL = SQL & "FROM N_EMI_View_Pemakaian_bahan "
			SQL = SQL & "WHERE kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "AND (tahun * 100 + CAST(bulan AS INT)) = " & batas & " "
			SQL = SQL & "GROUP BY kode_barang"
			Using ds = BindingTrans(SQL)
				For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
					Dim kode As String = ds.Tables("MyTable").Rows(i)("kode_barang").ToString()

					dictPRD(kode) = Val(ds.Tables("MyTable").Rows(i)("jumlah"))
				Next
			End Using

			Dim awal As Integer = CInt(tahunSaatIni) * 100 + CInt(bulanSaatIni)

			Dim dtAkhir As Date = New Date(CInt(tahunSaatIni), CInt(bulanSaatIni), 1).AddMonths(3)
			Dim akhir As Integer = dtAkhir.Year * 100 + dtAkhir.Month



			SQL = "SELECT kode_barang, bulan, tahun, "
			SQL = SQL & "ISNULL(SUM(OutStanding_PR),0) AS total_pr "
			SQL = SQL & "FROM N_EMI_View_Material_Requirement_Planning "
			SQL = SQL & "WHERE kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "AND (tahun * 100 + CAST(bulan AS INT)) >= " & awal & " "
			SQL = SQL & "AND (tahun * 100 + CAST(bulan AS INT)) < " & akhir & " "
			SQL = SQL & "GROUP BY kode_barang, bulan, tahun "
			Using ds = BindingTrans(SQL)
				For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
					Dim kode = ds.Tables("MyTable").Rows(i)("kode_barang").ToString()
					Dim bln = ds.Tables("MyTable").Rows(i)("bulan").ToString()
					Dim thn = ds.Tables("MyTable").Rows(i)("tahun").ToString()

					Dim key = kode & "|" & bln & "|" & thn
					dictPRBulanan(key) = Val(ds.Tables("MyTable").Rows(i)("total_pr"))
				Next
			End Using

			' =========================
			' PO (ETD_simulasi)
			' =========================
			SQL = "SELECT kode_barang, bulan, tahun, "
			SQL = SQL & "SUM(Outstanding_PO) AS total_po "
			SQL = SQL & "FROM N_EMI_View_Material_Requirement_Planning "
			SQL = SQL & "WHERE kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "AND (tahun * 100 + CAST(bulan AS INT)) >= " & awal & " "
			SQL = SQL & "AND (tahun * 100 + CAST(bulan AS INT)) < " & akhir & " "
			SQL = SQL & "GROUP BY kode_barang, bulan, tahun "
			Using ds = BindingTrans(SQL)
				For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
					Dim kode = ds.Tables("MyTable").Rows(i)("kode_barang").ToString()
					Dim bln = ds.Tables("MyTable").Rows(i)("bulan").ToString()
					Dim thn = ds.Tables("MyTable").Rows(i)("tahun").ToString()

					Dim key = kode & "|" & bln & "|" & thn
					dictPOBulanan(key) = Val(ds.Tables("MyTable").Rows(i)("total_po"))
				Next
			End Using

			' =========================
			' 🔥 PRELOAD 1: PR & PO (GLOBAL) — sudah ada, tetap dipakai
			' =========================
			Dim dictPR As New Dictionary(Of String, Double)
			Dim dictPO As New Dictionary(Of String, Double)



			SQL = "SELECT kode_barang, "
			SQL = SQL & "ISNULL(SUM(OutStanding_PR),0) AS total_pr, "
			SQL = SQL & "ISNULL(SUM(Outstanding_PO),0) AS total_po "
			SQL = SQL & "FROM N_EMI_View_Material_Requirement_Planning "
			SQL = SQL & "WHERE kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "AND (tahun * 100 + CAST(bulan AS INT)) <= " & batas & " "
			SQL = SQL & "GROUP BY kode_barang"
			Using ds = BindingTrans(SQL)
				For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1

					Dim kode As String = ds.Tables("MyTable").Rows(i)("kode_barang").ToString()

					dictPR(kode) = Val(ds.Tables("MyTable").Rows(i)("total_pr"))
					dictPO(kode) = Val(ds.Tables("MyTable").Rows(i)("total_po"))

				Next
			End Using

			' =========================
			' 🔥 PRELOAD 2: Info barang (satuan, good_stock, flag) — SATU QUERY untuk semua barang
			' =========================
			Dim dictBarangInfo As New Dictionary(Of String, (satuan As String, good_stock As Double, flag_packaging As String, flag_raw_material As String))

			If Arrbarang.Count > 0 Then
				Dim inList As String = String.Join(",", Arrbarang.Cast(Of String).Select(Function(k) "'" & k & "'"))
				SQL = "SELECT distinct a.kode_barang, a.satuan, (Stock_Wharehouse+Stock_Unloading) as good_stock, (Gantungan_PO + Gantungan_Split1 + Gantungan_Split2) AS Keep_Stock, "
				SQL = SQL & "b.Flag_raw_material, b.flag_packaging "
				SQL = SQL & "FROM N_Emi_View_Stock_Gantung a "
				SQL = SQL & "INNER JOIN barang c ON a.kode_barang = c.kode_barang "
				SQL = SQL & "INNER JOIN emi_group_jenis b ON b.id_group_jenis = c.id_group_jenis "
				SQL = SQL & "AND b.kode_perusahaan = b.kode_perusahaan "
				SQL = SQL & "WHERE b.kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "AND a.kode_barang IN (" & inList & ")"
				Using ds = BindingTrans(SQL)
					For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
						Dim row = ds.Tables("MyTable").Rows(i)
						Dim key As String = row("kode_barang").ToString()
						dictBarangInfo(key) = (
						satuan:=row("satuan").ToString(),
						good_stock:=Val(row("good_stock")),
						flag_packaging:=row("flag_packaging").ToString(),
						flag_raw_material:=row("Flag_raw_material").ToString()
					)
						dictOSPRD(key) = Val(ds.Tables("MyTable").Rows(i)("Keep_Stock"))

					Next
				End Using
			End If

			' =========================
			' 🔥 PRELOAD 3: Satuan display — SATU QUERY untuk semua barang
			' =========================
			' dictSatuanDisplay diasumsikan sudah dideklarasi di level form
			' Kalau belum, deklarasi di sini:
			' Dim dictSatuanDisplay As New Dictionary(Of String, String)

			Dim listErrorSatuanCekBarang As New List(Of String)

			If Arrbarang.Count > 0 Then
				Dim inList As String = String.Join(",", Arrbarang.Cast(Of String).Select(Function(k) "'" & k & "'"))
				SQL = "SELECT kode_barang, satuan FROM Barang_Detail_Satuan "

				SQL = SQL & "WHERE kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "AND flag_tampil_display = 'Y' "
				SQL = SQL & "AND kode_barang IN (" & inList & ")"

				Using ds = BindingTrans(SQL)
					For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
						Dim row = ds.Tables("MyTable").Rows(i)

						Dim key As String = row("kode_barang").ToString()
						Dim satuanFix As String = row("satuan").ToString()

						If General_Class.CekNULL(row("satuan")) = "" Then
							listErrorSatuanCekBarang.Add(key)
						Else
							If Not dictSatuanDisplay.ContainsKey(key) Then
								dictSatuanDisplay(key) = satuanFix
							End If
						End If

						'If Not dictSatuanDisplay.ContainsKey(key) Then
						'    dictSatuanDisplay(key) = row("satuan").ToString()
						'End If
					Next
				End Using
			End If

			If listErrorSatuanCekBarang.Count > 0 Then
				Dim msg As String = "Satuan 1 kosong untuk kode barang: " & String.Join(", ", listErrorSatuanCekBarang)
				Throw New Exception(msg)
			End If

			' =========================
			' 🔥 PRELOAD 4: Ubah_Satuan good_stock — SATU QUERY untuk semua barang
			' Gabungkan good_stock conversion ke satu batch SQL
			' =========================
			Dim dictGoodStockDisplay As New Dictionary(Of String, Double)

			If Arrbarang.Count > 0 Then
				' Bangun UNION ALL untuk semua barang sekaligus
				Dim unionParts As New List(Of String)
				For i As Integer = 0 To Arrbarang.Count - 1
					Dim kb As String = Arrbarang.Item(i)
					If dictBarangInfo.ContainsKey(kb) AndAlso dictSatuanDisplay.ContainsKey(kb) Then
						Dim info = dictBarangInfo(kb)
						Dim satuanDisplay As String = dictSatuanDisplay(kb)
						unionParts.Add(
						"SELECT '" & kb & "' AS kode_barang, " &
						"" & info.good_stock & " AS hasil"
					)
					End If
				Next

				If unionParts.Count > 0 Then
					SQL = String.Join(" UNION ALL ", unionParts)
					Using ds = BindingTrans(SQL)
						For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
							Dim row = ds.Tables("MyTable").Rows(i)
							dictGoodStockDisplay(row("kode_barang").ToString()) = Val(row("hasil"))
						Next
					End Using
				End If
			End If

			Dim listBulan As New List(Of String)
			Dim listTahun As New List(Of String)

			For i As Integer = 0 To 6
				Dim dt As Date = New Date(CInt(tahunSaatIni), CInt(bulanSaatIni), 1).AddMonths(i)
				listBulan.Add(dt.Month.ToString("D2"))
				listTahun.Add(dt.Year.ToString())
			Next

			Dim strBulan As String = "'" & String.Join("','", listBulan) & "'"
			Dim strTahun As String = "'" & String.Join("','", listTahun.Distinct()) & "'"
			' =========================
			' 🔥 PRELOAD 5: NilaiCurrentMonth (CTE formulator) — SATU QUERY untuk SEMUA barang sekaligus
			' =========================

			Dim dictForecastRM As New Dictionary(Of String, Double)
			Dim dictForecastPKG As New Dictionary(Of String, Double)

			Dim listErrorSatuan As New List(Of String)



			SQL = $"

															WITH cte AS (SELECT DISTINCT ISNULL(f.No_Faktur, '') AS kode_formula,

																 x.kode_barang_inq,
																 x.kode_perusahaan
												 FROM barang x
														  INNER JOIN emI_group_jenis y
																	 ON x.kode_perusahaan = y.kode_perusahaan AND x.id_group_jenis = y.id_group_jenis
														  OUTER APPLY (SELECT TOP 1 c.No_Faktur, flag_invalid
																	   FROM N_EMI_Transaksi_Formulator_Binding a
																				INNER JOIN N_EMI_Transaksi_Formulator_Binding_Detail b
																						   ON a.Kode_Perusahaan = b.Kode_Perusahaan
																							   AND a.No_Faktur = b.No_Faktur
																				INNER JOIN Emi_Transaksi_Formulator c
																						   ON b.Kode_Perusahaan = c.Kode_Perusahaan
																							   AND b.No_Formulator = c.No_Faktur
																							   AND c.Status IS NULL
																	   WHERE a.Status IS NULL
																		 AND a.Flag_Validasi_Main = 'Y'
																		 AND a.Kode_Perusahaan = x.kode_perusahaan
																		 AND a.Kode_Barang = x.kode_barang_inq
																		 and c.Flag_Validasi_Formula_Produksi_BOD = 'Y'
																		 and no_prioritas = 1
																	   ORDER BY a.Tanggal DESC, a.Jam DESC, b.No_Prioritas ASC) f
												 WHERE (y.Flag_Finished_Good = 'Y' OR y.Flag_Semi_FG = 'Y')
												   and flag_invalid is null),
										 cte_b as (SELECT '1'           as dari,
														  b.Kode_Barang,
														  a.bulan,
														  a.tahun,
														  b.kode_Barang AS kode_fg,
														  d.kode_barang AS Kode_Bahan,
														  d.Nilai_Barang,
														  d.satuan_barang,
														  C.No_Faktur   as Kode_Formula,
														  (b.nilai_ppic - f_sum_beda_formula.total_qty_beda_formula - f_sum_sudah_po.total_sudah_po) *
														  bb.Berat /
														  1000          AS nilai_ppic,
														  c.hasil       AS nilai_Formula,
														  z.Kode_Perusahaan,
														  z.id_group_jenis

												   FROM emi_transaksi_sales_forecasting a
															INNER JOIN emi_transaksi_sales_forecasting_detail b
																	   ON a.Kode_Perusahaan = b.kode_Perusahaan AND a.no_faktur = b.no_faktur
															INNER JOIN barang bb
																	   ON b.kode_perusahaan = bb.kode_perusahaan and b.kode_barang = bb.kode_barang and
																		  b.kode_stock_owner = bb.kode_stock_owner
															INNER JOIN cte bc ON bb.kode_perusahaan = bc.kode_perusahaan and
																				 bb.kode_barang_inq = bc.kode_barang_inq
															INNER JOIN Emi_Transaksi_Formulator c
																	   ON c.Kode_Perusahaan = bc.Kode_Perusahaan AND c.No_Faktur = bc.Kode_Formula
															INNER JOIN emi_transaksi_formulator_detail_Bahan d
																	   ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.no_faktur = d.no_faktur
															INNER JOIN barang z
																	   ON d.Kode_Perusahaan = z.Kode_Perusahaan and d.Kode_Barang = z.Kode_Barang and
																		  d.Kode_Stock_Owner = z.Kode_Stock_Owner


														   OUTER APPLY (SELECT ISNULL(SUM(f.Jumlah - ISNULL(po.Jumlah_PO, 0)), 0) AS total_qty_beda_formula
                                     FROM N_EMI_Production_Plan_Schedule_Detail f
                                              INNER JOIN N_EMI_Production_Plan_Schedule g
                                                         ON f.No_Transaksi = g.No_Transaksi AND
                                                            f.Kode_Perusahaan = g.Kode_Perusahaan

															  LEFT JOIN (SELECT Urut_Production_Schedule,
																				Kode_Perusahaan,
																				SUM(Jumlah) AS Jumlah_PO
																		 FROM EMI_Order_Produksi
																		 WHERE Status IS NULL
																		 GROUP BY Urut_Production_Schedule, Kode_Perusahaan) po
																		ON po.Urut_Production_Schedule = f.No_Urut AND
																		   po.Kode_Perusahaan = f.Kode_Perusahaan

													 WHERE g.Status IS NULL
													  and  f.kode_formula is not null
													   AND f.Kode_Perusahaan = b.Kode_Perusahaan
													   AND f.Urut_Production_Plan = b.urut) f_sum_beda_formula

										OUTER APPLY (select isnull(sum(y.Jumlah), 0) as total_sudah_po
													 from N_EMI_Production_Plan_Schedule_Detail x,
														  emi_order_produksi y,
														  n_emi_production_plan_schedule z
													 where x.Urut_Production_Plan = b.urut
													   and x.Kode_Perusahaan = y.Kode_Perusahaan
													   and x.No_Urut = y.Urut_Production_Schedule
													   and y.Status is null
													   and x.Urut_Production_Plan = b.urut
													   and x.Kode_Perusahaan = z.Kode_Perusahaan
													   and x.No_Transaksi = z.No_Transaksi
													   and y.Status is null) f_sum_sudah_po

												   WHERE a.status IS NULL
													 and a.status IS NULL
											
													 AND b.Kode_Perusahaan = '{ KodePerusahaan }'
													 AND ({filterBulan})
													 AND b.flag_validasi = 'Y'
													 AND b.flag_validasi_PPIC = 'Y'
													 AND c.status IS NULL

												   UNION ALL

												   SELECT '2'                                                       as dari,
														  b.Kode_Barang,
														  a.bulan,
														  a.tahun,
														  b.kode_Barang                                             AS kode_fg,
														  d.kode_barang                                             AS Kode_Bahan,
														  d.Nilai_Barang,
														  d.satuan_barang,
														  f.Kode_Formula,
														  isnull((f.Jumlah - k.Qty_PO), f.Jumlah) * bb.Berat / 1000 AS nilai_ppic,
														  c.hasil                                                   AS nilai_Formula,
														  z.Kode_Perusahaan,
														  z.id_group_jenis

												   FROM emi_transaksi_sales_forecasting a
															INNER JOIN emi_transaksi_sales_forecasting_detail b
																	   ON a.Kode_Perusahaan = b.kode_Perusahaan AND a.no_faktur = b.no_faktur
															INNER JOIN barang bb
																	   ON b.kode_perusahaan = bb.kode_perusahaan and b.kode_barang = bb.kode_barang and
																		  b.kode_stock_owner = bb.kode_stock_owner
															INNER JOIN N_EMI_Production_Plan_Schedule_Detail f
																	   ON f.Kode_Perusahaan = b.Kode_Perusahaan AND f.Urut_Production_Plan = b.urut
															INNER JOIN N_EMI_Production_Plan_Schedule g
																	   ON f.Kode_Perusahaan = g.Kode_Perusahaan AND f.No_Transaksi = g.No_Transaksi
																		   and g.Status is null
															INNER JOIN emi_transaksi_formulator c
																	   ON f.Kode_Perusahaan = c.Kode_Perusahaan AND f.Kode_Formula = c.No_Faktur
																		   and c.Status is null
															INNER JOIN emi_transaksi_formulator_detail_Bahan d
																	   ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.no_faktur = d.no_faktur
															INNER JOIN barang z
																	   ON d.Kode_Perusahaan = z.Kode_Perusahaan and d.Kode_Barang = z.Kode_Barang and
																		  d.Kode_Stock_Owner = z.Kode_Stock_Owner
															INNER JOIN init e ON a.kode_Perusahaan = e.kode_Perusahaan
															outer apply (select sum(xyz.Jumlah) as Qty_PO
																		 from EMI_Order_Produksi xyz
																		 where xyz.Kode_Perusahaan = f.Kode_Perusahaan
																		   and xyz.Urut_Production_Schedule = f.No_Urut
																		   and xyz.Status is null) k

												   WHERE a.status IS NULL
													 and a.status IS NULL
													 AND b.Kode_Perusahaan = '{KodePerusahaan}'
													 AND ({filterBulan})
													 AND b.flag_validasi = 'Y'
													 AND b.flag_validasi_PPIC = 'Y'
													 AND c.status IS NULL
													 AND g.Status IS NULL),
										 hasil_grouping_formula as (SELECT a.Kode_Barang
																			,
																		   a.Kode_Bahan,
																		   a.satuan_barang,
																		   a.bulan,
																		   a.tahun,
																		   ISNULL(
																				   (ROUND(sum(a.Nilai_Barang * (a.nilai_ppic / a.nilai_Formula)), 2)),
																				   0)                                   AS Nilai,
																		   b.satuan                                     AS satuan_display,
																		   CASE WHEN b.satuan IS NULL THEN 1 ELSE 0 END AS flag_satuan_kosong

																	FROM cte_b a
																			 INNER JOIN emi_group_jenis m
																						ON a.kode_perusahaan = m.kode_perusahaan and
																						   a.id_group_jenis = m.id_group_jenis
																			 LEFT JOIN Barang_Detail_Satuan b
																					   ON b.kode_barang = a.Kode_Bahan
																						   AND b.kode_perusahaan = '{KodePerusahaan}' AND
																						  b.flag_tampil_display = 'Y'
																	where m.flag_raw_material = 'Y'


																	group by a.Kode_Bahan, a.satuan_barang, a.bulan, a.tahun, b.satuan,
																			 a.Kode_Barang, a.Kode_Formula)
									select Kode_Barang,
										   Kode_Bahan,
										   Satuan_barang,
										   Bulan,
										   a.tahun,
										   ROUND(sum(a.Nilai), 2) as Nilai,
										   satuan_display,
										   flag_satuan_kosong
									From hasil_grouping_formula a
									group by Kode_Barang, Kode_Bahan, Satuan_barang, Bulan,
											 a.tahun,
											 satuan_display, flag_satuan_kosong

									order by a.Bulan, a.tahun, a.Kode_Bahan

					"


			Dim listRM As New List(Of (kb As String, sb As String, sd As String, bln As String, thn As String, nilai As Double))

			Using ds = BindingTrans(SQL)
				For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
					Dim r = ds.Tables("MyTable").Rows(i)

					If r("flag_satuan_kosong").ToString() = "1" Then
						listErrorSatuan.Add(r("Kode_Bahan").ToString())
					End If

					listRM.Add((
						kb:=r("Kode_Bahan").ToString(),
						sb:=r("satuan_barang").ToString(),
						sd:=r("satuan_display").ToString(),
						bln:=r("bulan").ToString().PadLeft(2, "0"),
						thn:=r("tahun").ToString(),
						nilai:=Val(r("Nilai"))
					))
				Next
			End Using

			If listRM.Count > 0 Then
				Dim unionConv As New List(Of String)

				For i As Integer = 0 To listRM.Count - 1
					Dim r = listRM(i)

					unionConv.Add(
			"SELECT '" & r.kb & "' kode_bahan,'" & r.bln & "' bulan,'" & r.thn & "' tahun," &
			"" & r.nilai & " hasil"
		)
				Next

				SQL = String.Join(" UNION ALL ", unionConv)

				Using ds = BindingTrans(SQL)
					For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
						Dim row = ds.Tables("MyTable").Rows(i)

						Dim key = row("kode_bahan").ToString() & "|" &
					  row("bulan").ToString() & "|" &
					  row("tahun").ToString()

						If dictForecastRM.ContainsKey(key) Then
							dictForecastRM(key) += Val(row("hasil"))
						Else
							dictForecastRM(key) = Val(row("hasil"))
						End If
					Next
				End Using
			End If

			SQL = ";WITH cte AS ("

			SQL = SQL & " SELECT a.bulan,a.tahun, b.kode_Barang AS kode_fg, c.Kode_Bahan, c.Jumlah_Bahan AS Nilai_Barang,"
			SQL = SQL & " b.nilai_ppic AS nilai_ppic, c.jumlah_barang AS nilai_Formula, b.satuan AS satuan_barang"

			SQL = SQL & " FROM emi_transaksi_sales_forecasting a,"
			SQL = SQL & " emi_transaksi_sales_forecasting_detail b,"
			SQL = SQL & " barang_detail_bahan_penolong c, barang d"

			SQL = SQL & " WHERE a.Kode_Perusahaan = b.kode_Perusahaan AND a.no_faktur = b.no_faktur"
			SQL = SQL & " AND a.status IS NULL AND b.Kode_Perusahaan = '" & KodePerusahaan & "'"

			SQL = SQL & " AND b.Kode_Perusahaan=d.Kode_Perusahaan AND b.Kode_barang=d.Kode_Barang AND b.kode_stock_owner=d.kode_stock_owner "

			SQL = SQL & "AND (" & filterBulan & ") "

			'SQL = SQL & " AND ( (a.tahun='" & tahunSaatIni & "' AND a.bulan >= '" & bulanSaatIni & "') "
			'SQL = SQL & "    OR (a.tahun='" & (Val(tahunSaatIni) + 1) & "' AND a.bulan <= '02') )"

			SQL = SQL & " AND b.flag_validasi='Y' AND b.flag_validasi_PPIC='Y'"

			SQL = SQL & " AND d.Kode_Perusahaan = c.Kode_Perusahaan AND d.kode_barang_inq = c.kode_barang"

			SQL = SQL & " )"

			SQL = SQL & " SELECT cte.Kode_Bahan, cte.satuan_barang, cte.bulan, cte.tahun,"
			SQL = SQL & " ISNULL(SUM(ROUND(cte.Nilai_Barang*(cte.nilai_ppic/cte.nilai_Formula),2)),0) AS Nilai,"
			SQL = SQL & " bds.satuan AS satuan_display, "
			SQL = SQL & "CASE WHEN bds.satuan IS NULL THEN 1 ELSE 0 END AS flag_satuan_kosong "

			SQL = SQL & " FROM cte"

			SQL = SQL & " LEFT JOIN Barang_Detail_Satuan bds ON bds.kode_barang = cte.Kode_Bahan "
			SQL = SQL & " AND bds.kode_perusahaan = '" & KodePerusahaan & "' AND bds.flag_tampil_display = 'Y'"

			SQL = SQL & " GROUP BY cte.Kode_Bahan, cte.satuan_barang, cte.bulan, cte.tahun, bds.satuan"

			Dim listPKG As New List(Of (kb As String, sb As String, sd As String, bln As String, thn As String, nilai As Double))

			Using ds = BindingTrans(SQL)
				For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
					Dim r = ds.Tables("MyTable").Rows(i)

					If r("flag_satuan_kosong").ToString() = "1" Then
						listErrorSatuan.Add(r("Kode_Bahan").ToString())
					End If

					listPKG.Add((
							kb:=r("Kode_Bahan").ToString(),
							sb:=r("satuan_barang").ToString(),
							sd:=r("satuan_display").ToString(),
							bln:=r("bulan").ToString().PadLeft(2, "0"),
							thn:=r("tahun").ToString(),
							nilai:=Val(r("Nilai"))
						))
				Next
			End Using

			If listPKG.Count > 0 Then
				Dim unionConv As New List(Of String)

				For i As Integer = 0 To listPKG.Count - 1
					Dim r = listPKG(i)

					unionConv.Add(
							"SELECT '" & r.kb & "' kode_bahan,'" & r.bln & "' bulan,'" & r.thn & "' tahun," &
							"" & r.nilai & " hasil"
						)
				Next

				SQL = String.Join(" UNION ALL ", unionConv)

				Using ds = BindingTrans(SQL)
					For i As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
						Dim row = ds.Tables("MyTable").Rows(i)

						Dim key = row("kode_bahan").ToString() & "|" &
									  row("bulan").ToString() & "|" &
									  row("tahun").ToString()

						dictForecastPKG(key) = Val(row("hasil"))
					Next
				End Using
			End If

			If listErrorSatuan.Count > 0 Then

				' hapus duplikat (penting!)
				Dim uniqueError = listErrorSatuan.Distinct().ToList()

				MessageBox.Show("Satuan belum di set untuk barang: " & vbCrLf &
					String.Join(vbCrLf, uniqueError),
					"Warning",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning)

				Exit Sub
			End If
			For indexxx = 0 To Arrbarang.Count - 1
				DataGridView1.SuspendLayout()
				DataGridView1.Rows.Add(1)
				Dim ind As Integer = DataGridView1.Rows.Count - 1

				DataGridView1.Rows(ind).Cells(CellNBom_1).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellNPPIC_1).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellKosong_1).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellNBom_2).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellNPPIC_2).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellKosong_2).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellNBom_3).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellNPPIC_3).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellKosong_3).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellNBom_4).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellNPPIC_4).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellKosong_4).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellNBom_5).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellNPPIC_5).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellKosong_5).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellNBom_6).Style.BackColor = Color.LightYellow
				DataGridView1.Rows(ind).Cells(CellNPPIC_6).Style.BackColor = Color.LightCyan
				DataGridView1.Rows(ind).Cells(CellKosong_6).Style.BackColor = Color.LightGray
				DataGridView1.Rows(ind).Cells(CellStatus).Style.BackColor = Color.Yellow

				DataGridView1.Rows(ind).Cells(CellNBom_1).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNBom_2).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNBom_3).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNBom_4).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNBom_5).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNBom_6).ReadOnly = True

				DataGridView1.Rows(ind).Cells(CellNPPIC_1).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNPPIC_2).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNPPIC_3).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNPPIC_4).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNPPIC_5).ReadOnly = True
				DataGridView1.Rows(ind).Cells(CellNPPIC_6).ReadOnly = True

				Dim satuan_barang As String = ""
				Dim good_stock As Double = 0
				Dim Flag_Packaging As String = ""
				Dim Flag_Raw_Material As String = ""

				Dim ada_data As String = ""
				Dim FValidasi As String = ""

				Dim kodeBarang As String = Arrbarang.Item(indexxx)

				' Validasi data ada
				If Not dictBarangInfo.ContainsKey(kodeBarang) Then
					MessageBox.Show("Data tidak ada untuk: " & kodeBarang)
					Exit Sub
				End If
				If Not dictSatuanDisplay.ContainsKey(kodeBarang) Then
					MessageBox.Show("Barang detail satuan belum di set!")
					Exit Sub
				End If

				Dim info = dictBarangInfo(kodeBarang)
				Dim convertKesatuanDisplay As String = dictSatuanDisplay(kodeBarang)
				Dim good_stock_tampil_display As Double = If(dictGoodStockDisplay.ContainsKey(kodeBarang), dictGoodStockDisplay(kodeBarang), 0)

				'' Ambil currentMonth dari cache
				'Dim currentMonthVal As Double = 0
				'If info.flag_raw_material = "Y" Then
				'    If dictCurrentMonth.ContainsKey(kodeBarang) Then
				'        currentMonthVal = dictCurrentMonth(kodeBarang)
				'    End If
				'ElseIf info.flag_packaging = "Y" Then
				'    If dictCurrentMonthPkg.ContainsKey(kodeBarang) Then
				'        currentMonthVal = dictCurrentMonthPkg(kodeBarang)
				'    End If
				'End If
				Dim key = kodeBarang & "|" & bulanSaatIni & "|" & tahunSaatIni

				Dim nilai As Double = 0

				If info.flag_raw_material = "Y" Then
					If dictForecastRM.ContainsKey(key) Then
						nilai = dictForecastRM(key)
					End If
				ElseIf info.flag_packaging = "Y" Then
					If dictForecastPKG.ContainsKey(key) Then
						nilai = dictForecastPKG(key)
					End If
				End If

				' Assign ke grid
				DataGridView1.Rows(ind).Cells(CellCurrentMonth).Value = Format(nilai, "N2")
				DataGridView1.Rows(ind).Cells(CellKd_Barang).Value = kodeBarang
				DataGridView1.Rows(ind).Cells(CellNm_Barang).Value = ArrNama.Item(indexxx)
				DataGridView1.Rows(ind).Cells(CellAvg_3Bln).Value = "0"
				DataGridView1.Rows(ind).Cells(CellStock_BB).Value = Format(good_stock_tampil_display, "N2")
				DataGridView1.Rows(ind).Cells(CellSatuanBarang).Value = convertKesatuanDisplay

				ListEndingStock.Add((Arrbarang.Item(indexxx), Val(HilangkanTanda(btn_bln0.Tag)).ToString("00"), "STOCK BAHAN BAKU", Val(HilangkanTanda(good_stock_tampil_display))))

				' PR & PO
				Dim totalPrBelumPO As Double = If(dictPR.ContainsKey(kodeBarang), dictPR(kodeBarang), 0)
				Dim totalPRSudahPO As Double = If(dictPO.ContainsKey(kodeBarang), dictPO(kodeBarang), 0)
				Dim totalPRD As Double = If(dictPRD.ContainsKey(kodeBarang), dictPRD(kodeBarang), 0)
				Dim totalOSPRD As Double = If(dictOSPRD.ContainsKey(kodeBarang), dictOSPRD(kodeBarang), 0)

				DataGridView1.Rows(ind).Cells(CellOPRequesition).Value = Format(totalPrBelumPO, "N2")
				DataGridView1.Rows(ind).Cells(CellOPOrder).Value = Format(totalPRSudahPO, "N2")

				DataGridView1.Rows(ind).Cells(cellCarryOver).Value = Format(totalPRD, "N2")

				DataGridView1.Rows(ind).Cells(cellOutstandingPOPlan).Value = Format(nilai, "N2")
				DataGridView1.Rows(ind).Cells(cellOutstandingPOOrder).Value = Format(totalOSPRD, "N2")
				DataGridView1.Rows(ind).Cells(cellFinishedPO).Value = Format(totalPRD, "N2")

				Dim jumlahProductionPlan As Double = Val(HilangkanTanda(nilai + totalOSPRD))

				estreq_awal = 0

				FunTotalJumlahCurrentMonth(jumlahProductionPlan, good_stock_tampil_display, totalPrBelumPO, totalPRSudahPO, ind, Arrbarang.Item(indexxx))
				'  FunTotalJumlahCurrentMonth(jumlahProductionPlan, good_stock_tampil_display, totalPrBelumPOPerbarang, totalPRSudahPO, ind)

				'Load Data PerBulan
				Dim a As Integer = arrBulan.Item(ComboBox1.SelectedIndex)
				Dim fthn As Integer = Val(ComboBox2.Text)
				Dim b As String = ""

				For i As Integer = 1 To 6
					' Perbarui nilai bulan dan tahun
					If a = 12 Then
						a = 1
						fthn = fthn + 1
					Else
						a = a + 1
					End If

					' Temukan nama bulan yang sesuai
					For index = 0 To arrBulan.Count - 1
						If arrBulan.Item(index) = a Then
							b = arrBulanMM.Item(index)

							Dim asdasdas As String = Arrbarang.Item(indexxx)
							Load_Data_Perbulan1(info.flag_raw_material, info.flag_packaging, indexxx, b, fthn, ind, akses_ubah, "", i, dictSatuanDisplay, dictPRBulanan, dictPOBulanan, dictForecastRM, dictForecastPKG)
							' Load_Data_Perbulan(Flag_Raw_Material, Flag_Packaging, indexxx, b, fthn, ind, akses_ubah, FValidasi, i, dictSatuanDisplay)
						End If
					Next

				Next

			Next
			DataGridView1.ResumeLayout()
			BtnExport.Visible = True

			btn_bln0.Visible = True
			btn_bln1.Visible = True
			btn_bln2.Visible = True
			btn_bln3.Visible = True
			btn_bln4.Visible = True
			btn_bln5.Visible = True
			btn_bln6.Visible = True

			CloseConn()
		Catch ex As Exception
			BtnExport.Visible = False

			btn_bln0.Visible = False
			btn_bln1.Visible = False
			btn_bln2.Visible = False
			btn_bln3.Visible = False
			btn_bln4.Visible = False
			btn_bln5.Visible = False
			btn_bln6.Visible = False
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
		btn_bln0.Visible = False
		btn_bln1.Visible = False
		btn_bln2.Visible = False
		btn_bln3.Visible = False
		btn_bln4.Visible = False
		btn_bln5.Visible = False
		btn_bln6.Visible = False
		BtnExport.Visible = False

		ListEndingStock.Clear()
		Start_Loading(Me)

		GetDataRix()

		End_Loading(Me)

	End Sub

	Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
		'If DataGridView1.Rows.Count = 0 Then Exit Sub

		'If e.RowIndex >= 0 AndAlso e.ColumnIndex = CellCurrentMonth Then

		'    Dim kodeBarang As String = DataGridView1.Rows(e.RowIndex).Cells(CellKd_Barang).Value.ToString()

		'    get_jam()

		'    N_EMI_SD_Detail_Transaksi_MaterialRequisition.lokasi_mrp = ComboBox3.Text
		'    N_EMI_SD_Detail_Transaksi_MaterialRequisition.bulanSkrng = Format(tgl_skg, "MM")
		'    N_EMI_SD_Detail_Transaksi_MaterialRequisition.tahunSkrng = Format(tgl_skg, "yyyy")
		'    N_EMI_SD_Detail_Transaksi_MaterialRequisition.ShowDialog()

		'End If

	End Sub

	Private Sub DataGridView1_ColumnHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseDoubleClick
		If e.ColumnIndex = CellCurrentMonth Then

			MessageBox.Show("Message")

		End If
	End Sub

	Dim bolehlewat As Boolean = True
	Private Sub Cek_Barang(ByVal bulan As String, ByVal tahun As String)
		Dim harusCekAkses As Boolean = True
		Try
			OpenConn()

			If CekButtonRole("MRP_MINGGUAN") = "Y" Then
				bolehlewat = True
				harusCekAkses = False
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try


		If harusCekAkses Then
			Try
				OpenConn()
				Cmd.Transaction = Cn.BeginTransaction

				SQL = $"
							WITH cte AS (
						SELECT DISTINCT 
							a.Kode_Perusahaan, 
							a.Kode_Barang,
							b.Bulan,
							b.Tahun,
							DAY(a.Tanggal_Full) AS Hari
						FROM N_EMI_Production_Plan_Schedule_Detail a
						INNER JOIN N_EMI_Production_Plan_Schedule b 
							ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Transaksi
						WHERE b.Status IS NULL 
							AND a.Jumlah <> 0
							AND b.Kode_Perusahaan = '{KodePerusahaan}' 
							AND b.Bulan = '{bulan}' 
							AND b.Tahun = '{tahun}'
					) 
					SELECT 
						COUNT(DISTINCT CASE WHEN a.Hari BETWEEN 1 AND 7 THEN a.Kode_Barang END) AS Jml_M1,
						COUNT(DISTINCT CASE WHEN a.Hari BETWEEN 8 AND 14 THEN a.Kode_Barang END) AS Jml_M2,
						COUNT(DISTINCT CASE WHEN a.Hari BETWEEN 15 AND 21 THEN a.Kode_Barang END) AS Jml_M3,
						COUNT(DISTINCT CASE WHEN a.Hari >= 22 THEN a.Kode_Barang END) AS Jml_M4,
    
						ISNULL(b.Jumlah_Min_Release_Production_Plan, 0) AS Target_Min,

						-- PERBAIKAN LOGIKA: Boleh 0, tapi kalau diisi wajib >= Target_Min
						CASE 
							WHEN (COUNT(DISTINCT CASE WHEN a.Hari BETWEEN 1 AND 7 THEN a.Kode_Barang END) = 0 OR COUNT(DISTINCT CASE WHEN a.Hari BETWEEN 1 AND 7 THEN a.Kode_Barang END) >= ISNULL(b.Jumlah_Min_Release_Production_Plan, 0))
							 AND (COUNT(DISTINCT CASE WHEN a.Hari BETWEEN 8 AND 14 THEN a.Kode_Barang END) = 0 OR COUNT(DISTINCT CASE WHEN a.Hari BETWEEN 8 AND 14 THEN a.Kode_Barang END) >= ISNULL(b.Jumlah_Min_Release_Production_Plan, 0))
							 AND (COUNT(DISTINCT CASE WHEN a.Hari BETWEEN 15 AND 21 THEN a.Kode_Barang END) = 0 OR COUNT(DISTINCT CASE WHEN a.Hari BETWEEN 15 AND 21 THEN a.Kode_Barang END) >= ISNULL(b.Jumlah_Min_Release_Production_Plan, 0))
							 AND (COUNT(DISTINCT CASE WHEN a.Hari >= 22 THEN a.Kode_Barang END) = 0 OR COUNT(DISTINCT CASE WHEN a.Hari >= 22 THEN a.Kode_Barang END) >= ISNULL(b.Jumlah_Min_Release_Production_Plan, 0))
							THEN 'Y'
							ELSE 'T' 
						END AS Boleh_Lewat
					FROM Init b 
					LEFT JOIN cte a ON b.Kode_Perusahaan = a.Kode_Perusahaan
					WHERE b.Kode_Perusahaan = '{KodePerusahaan}'
					GROUP BY b.Jumlah_Min_Release_Production_Plan
			"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Do
							If General_Class.CekNULL(Dr("boleh_lewat")) = "T" Then
								Dr.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("Terdapat Data mingguan yang belum lengkap (kurang dari minimal tampil)", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								bolehlewat = False
								Exit Sub
							Else

								bolehlewat = True

							End If

						Loop While Dr.Read
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						bolehlewat = False
						MessageBox.Show("Terjadi kesalahan pada pengecekan init data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using





				Cmd.Transaction.Commit()
				CloseTrans()
				CloseConn()
			Catch ex As Exception
				CloseTrans()
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		End If


	End Sub



	Private Sub btn_bln0_Click(sender As Object, e As EventArgs) Handles btn_bln0.Click

		Dim btn As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)

		' ambil bulan & tahun dari Tag
		Dim arr() As String = btn.Tag.ToString().Split("|"c)
		Dim bulan As String = arr(0)
		Dim tahun As String = arr(1)



		Cek_Barang(bulan, tahun)
		If bolehlewat <> True Then
			Exit Sub
		End If

		' kirim parameter ke form
		With N_EMI_SD_Detail_Transaksi_MaterialRequisition
			.lokasi_mrp = ComboBox3.Text
			.bulanSkrng = bulan
			.tahunSkrng = tahun
			.BulanKe = 1
			.ListEndingStock = ListEndingStock
			.ShowDialog()
		End With
	End Sub

	Private Sub btn_bln1_Click(sender As Object, e As EventArgs) Handles btn_bln1.Click
		Dim btn As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)

		' ambil bulan & tahun dari Tag
		Dim arr() As String = btn.Tag.ToString().Split("|"c)
		Dim bulan As String = arr(0)
		Dim tahun As String = arr(1)

		Cek_Barang(bulan, tahun)
		If bolehlewat <> True Then
			Exit Sub
		End If


		' kirim parameter ke form
		With N_EMI_SD_Detail_Transaksi_MaterialRequisition
			.lokasi_mrp = ComboBox3.Text
			.bulanSkrng = bulan
			.tahunSkrng = tahun
			.BulanKe = 2
			.ListEndingStock = ListEndingStock
			.ShowDialog()
		End With
	End Sub

	Private Sub btn_bln2_Click(sender As Object, e As EventArgs) Handles btn_bln2.Click
		Dim btn As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)

		' ambil bulan & tahun dari Tag
		Dim arr() As String = btn.Tag.ToString().Split("|"c)
		Dim bulan As String = arr(0)
		Dim tahun As String = arr(1)


		Cek_Barang(bulan, tahun)
		If bolehlewat <> True Then
			Exit Sub
		End If

		' kirim parameter ke form
		With N_EMI_SD_Detail_Transaksi_MaterialRequisition
			.lokasi_mrp = ComboBox3.Text
			.bulanSkrng = bulan
			.tahunSkrng = tahun
			.BulanKe = 3
			.ListEndingStock = ListEndingStock
			.ShowDialog()
		End With
	End Sub

	Private Sub ExportToExcel()

		Dim xlApp As New Excel.Application
		Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add()
		Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Sheets(1)

		xlApp.ScreenUpdating = False

		' mapping kolom
		Dim mapping As Integer() = {
			CellKd_Barang,
			CellNm_Barang,
			CellSatuanBarang,
			cellFinishedPO,
			cellOutstandingPOPlan,
			cellOutstandingPOOrder,
			CellStock_BB,
			CellOPRequesition,
			CellOPOrder,
			cellRequirmentStok,
			CellNBom_1,
			CellOPRequesition_1,
			CellOPOrder_1,
			cellRequireStok_1,
			CellNBom_2,
			CellOPRequesition_2,
			CellOPOrder_2,
			cellRequireStok_2
		}

		Dim CellInteger As Integer() = {
			cellFinishedPO,
			cellOutstandingPOPlan,
			cellOutstandingPOOrder,
			CellStock_BB,
			CellOPRequesition,
			CellOPOrder,
			cellRequirmentStok,
			CellNBom_1,
			CellOPRequesition_1,
			CellOPOrder_1,
			cellRequireStok_1,
			CellNBom_2,
			CellOPRequesition_2,
			CellOPOrder_2,
			cellRequireStok_2
		}

		Dim CellCenter As Integer() = {
			CellSatuanBarang
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
		'xlWorkSheet.Columns(1).NumberFormat = "@"
		'xlWorkSheet.Columns.AutoFit()

		' 🔥 FORMATTING LOGIC
		' 1. Default: Semua Kolom Set ke Text (@) dan Rata Kiri
		Dim allDataRange = xlWorkSheet.Range(xlWorkSheet.Cells(2, 1), xlWorkSheet.Cells(validRowCount + 1, colCount))
		allDataRange.NumberFormat = "@"
		allDataRange.HorizontalAlignment = Excel.Constants.xlLeft

		' 🔥 1. FORMAT ACCOUNTING / NUMBER (Rata Kanan)
		Dim rangeNumeric As Excel.Range = Nothing

		For Each colIdx In CellInteger
			Dim targetColIndex As Integer = Array.IndexOf(mapping, colIdx) + 1
			If targetColIndex > 0 Then
				If rangeNumeric Is Nothing Then
					rangeNumeric = xlWorkSheet.Columns(targetColIndex)
				Else
					rangeNumeric = xlApp.Union(rangeNumeric, xlWorkSheet.Columns(targetColIndex))
				End If
			End If
		Next

		If rangeNumeric IsNot Nothing Then
			rangeNumeric.NumberFormat = "#,##0.00"
			rangeNumeric.HorizontalAlignment = Excel.Constants.xlRight
		End If

		' 🔥 2. FORMAT RATA TENGAH (Center)
		Dim rangeCenter As Excel.Range = Nothing

		For Each colIdx In CellCenter
			Dim targetColIndex As Integer = Array.IndexOf(mapping, colIdx) + 1
			If targetColIndex > 0 Then
				If rangeCenter Is Nothing Then
					rangeCenter = xlWorkSheet.Columns(targetColIndex)
				Else
					rangeCenter = xlApp.Union(rangeCenter, xlWorkSheet.Columns(targetColIndex))
				End If
			End If
		Next

		If rangeCenter IsNot Nothing Then
			rangeCenter.HorizontalAlignment = Excel.Constants.xlCenter
		End If

		' 🔥 3. CLEANUP RANGE OBJECTS
		If rangeNumeric IsNot Nothing Then releaseObject(rangeNumeric)
		If rangeCenter IsNot Nothing Then releaseObject(rangeCenter)

		' 3. Header Styling (Opsional: Bold & Center)
		Dim headerRange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, colCount))
		headerRange.Font.Bold = True
		headerRange.HorizontalAlignment = Excel.Constants.xlCenter

		xlWorkSheet.Columns.AutoFit()

		' SAVE
		Dim saveFileDialog As New SaveFileDialog
		saveFileDialog.Filter = "Excel Files|*.xlsx"

		If saveFileDialog.ShowDialog = DialogResult.OK Then
			xlWorkBook.SaveAs(saveFileDialog.FileName)
		End If

		' CLEANUP
		xlWorkBook.Close(False)
		xlApp.Quit()

		releaseObject(xlWorkSheet)
		releaseObject(xlWorkBook)
		releaseObject(xlApp)

		MessageBox.Show("Export berhasil!", "Sukses Simpan", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
		ExportToExcel()
	End Sub

	Private Sub btn_bln3_Click(sender As Object, e As EventArgs) Handles btn_bln3.Click
		Dim btn As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)

		' ambil bulan & tahun dari Tag
		Dim arr() As String = btn.Tag.ToString().Split("|"c)
		Dim bulan As String = arr(0)
		Dim tahun As String = arr(1)

		Cek_Barang(bulan, tahun)
		If bolehlewat <> True Then
			Exit Sub
		End If

		' kirim parameter ke form
		With N_EMI_SD_Detail_Transaksi_MaterialRequisition
			.lokasi_mrp = ComboBox3.Text
			.bulanSkrng = bulan
			.tahunSkrng = tahun
			.BulanKe = 4
			.ListEndingStock = ListEndingStock
			.ShowDialog()
		End With
	End Sub

	Private Sub btn_bln4_Click(sender As Object, e As EventArgs) Handles btn_bln4.Click
		Dim btn As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)

		' ambil bulan & tahun dari Tag
		Dim arr() As String = btn.Tag.ToString().Split("|"c)
		Dim bulan As String = arr(0)
		Dim tahun As String = arr(1)

		Cek_Barang(bulan, tahun)
		If bolehlewat <> True Then
			Exit Sub
		End If

		' kirim parameter ke form
		With N_EMI_SD_Detail_Transaksi_MaterialRequisition
			.lokasi_mrp = ComboBox3.Text
			.bulanSkrng = bulan
			.tahunSkrng = tahun
			.BulanKe = 5
			.ListEndingStock = ListEndingStock
			.ShowDialog()
		End With
	End Sub

	Private Sub btn_bln5_Click(sender As Object, e As EventArgs) Handles btn_bln5.Click

		Dim btn As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)

		' ambil bulan & tahun dari Tag
		Dim arr() As String = btn.Tag.ToString().Split("|"c)
		Dim bulan As String = arr(0)
		Dim tahun As String = arr(1)

		Cek_Barang(bulan, tahun)
		If bolehlewat <> True Then
			Exit Sub
		End If

		' kirim parameter ke form
		With N_EMI_SD_Detail_Transaksi_MaterialRequisition
			.lokasi_mrp = ComboBox3.Text
			.bulanSkrng = bulan
			.tahunSkrng = tahun
			.BulanKe = 6
			.ListEndingStock = ListEndingStock
			.ShowDialog()
		End With
	End Sub

	Private Sub btn_bln6_Click(sender As Object, e As EventArgs) Handles btn_bln6.Click
		Dim btn As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)

		' ambil bulan & tahun dari Tag
		Dim arr() As String = btn.Tag.ToString().Split("|"c)
		Dim bulan As String = arr(0)
		Dim tahun As String = arr(1)

		Cek_Barang(bulan, tahun)

		' kirim parameter ke form
		With N_EMI_SD_Detail_Transaksi_MaterialRequisition
			.lokasi_mrp = ComboBox3.Text
			.bulanSkrng = bulan
			.tahunSkrng = tahun
			.BulanKe = 7
			.ListEndingStock = ListEndingStock
			.ShowDialog()
		End With
	End Sub


	Public Sub Load_Data_Perbulan1(ByVal Flag_Raw_Material As String, ByVal Flag_Packaging As String, ByVal barangIndex As Integer, ByVal Bln As String, ByVal Thn As String, ByVal RowIndex As Integer, ByVal aksesUbah As String, ByVal FValidasi As String, ByVal bulanke As Integer, ByRef dictSatuanDisplay As Dictionary(Of String, String), ByRef dictPRBulanan As Dictionary(Of String, Double), ByRef dictPOBulanan As Dictionary(Of String, Double),
								   ByRef dictRM As Dictionary(Of String, Double), ByRef dictPkg As Dictionary(Of String, Double))

		Dim ada_data As String = ""

		'Dim CellNPPIC As Integer = CType(Me.GetType().GetField("CellNPPIC_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
		'Dim CellNBom As Integer = CType(Me.GetType().GetField("CellNBom_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
		'Dim CellUrut As Integer = CType(Me.GetType().GetField("CellUrut_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
		'Dim CellKosong As Integer = CType(Me.GetType().GetField("CellKosong_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)

		'Dim cellPRoutStanding As Integer = CType(Me.GetType().GetField("CellOPRequesition_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
		'Dim cellPOoutStanding As Integer = CType(Me.GetType().GetField("CellOPOrder_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
		'Dim cellRequireStok As Integer = CType(Me.GetType().GetField("cellRequireStok_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)

		Dim cellNBom As Integer = dictNBom(bulanke)
		Dim CellNPPIC As Integer = dictPPIC(bulanke)
		Dim CellUrut As Integer = dictUrut(bulanke)
		Dim CellKosong As Integer = dictKosong(bulanke)

		Dim cellPRoutStanding As Integer = dictOPRequesition(bulanke)
		Dim cellPOoutStanding As Integer = dictOPOrder(bulanke)
		Dim cellRequireStok As Integer = dictRequireStok(bulanke)

		'=============================== tampil data PR belum PO ==============================='

		'Dim totalPrBelumPOPerbarang As Double = 0
		'Dim convertSatuanDisplayPr As String = ""

		''---------select ke data pr berdasarkan kode_barang

		'SQL = "select a.Kode_Perusahaan,a.Kode_Barang,a.Nama,sum(PR_Outstanding) as Jumlah,a.Satuan from N_EMI_View_Material_Requirement_Planning a  "
		'SQL = SQL & "where MONTH(Tanggal_Delivery) = '" & Bln & "' and YEAR(Tanggal_Delivery) = '" & Thn & "' "
		'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "'  "
		'SQL = SQL & "and a.kode_barang = '" & Arrbarang.Item(barangIndex) & "' "

		'SQL = SQL & "group by a.Kode_Perusahaan,a.Kode_Barang,a.Nama,a.Satuan "

		'Using Ds2 = BindingTrans(SQL)
		'    With Ds2.Tables("MyTable")
		'        If .Rows.Count <> 0 Then
		'            For indexPR As Integer = 0 To .Rows.Count - 1

		'                Dim kodeBarangPR As String = .Rows(indexPR).Item("kode_barang").ToString()

		'                ' ambil dari cache (NO QUERY)
		'                convertSatuanDisplayPr = dictSatuanDisplay(kodeBarangPR)

		'                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarangPR & "'," &
		'                              "'" & .Rows(indexPR).Item("satuan") & "','" & convertSatuanDisplayPr & "'," &
		'                              .Rows(indexPR).Item("jumlah") & ") as Hasil"

		'                Using dr4 = OpenTrans(SQL)
		'                    If dr4.Read Then
		'                        If General_Class.CekNULL(dr4("Hasil")) <> "" Then
		'                            totalPrBelumPOPerbarang += dr4("hasil")
		'                        Else
		'                            MessageBox.Show("Satuan tidak ditemukan!")
		'                            Exit Sub
		'                        End If
		'                    End If
		'                End Using

		'            Next

		'            DataGridView1.Rows(RowIndex).Cells(cellPRoutStanding).Value = Format(totalPrBelumPOPerbarang, "N2")

		'        Else
		'            'jika belum  ada pr maka 0
		'            DataGridView1.Rows(RowIndex).Cells(cellPRoutStanding).Value = 0
		'        End If
		'    End With
		'End Using

		''=========================================== tampil PR sudah po =========================================='
		'Dim totalPRSudahPO As Double = 0
		'Dim convertSatuanPRSudahPO As String = ""

		'SQL = "select a.Kode_Perusahaan,a.Kode_Barang,a.Nama,sum(PO_Outstanding) as Jumlah,a.Satuan from N_EMI_View_Material_Requirement_Planning a  "
		'SQL = SQL & "where MONTH(etd_simulasi) = '" & Bln & "' and YEAR(etd_simulasi) = '" & Thn & "'  "
		'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "'  "
		'SQL = SQL & "and a.kode_barang = '" & Arrbarang.Item(barangIndex) & "' "

		'SQL = SQL & "group by a.Kode_Perusahaan,a.Kode_Barang,a.Nama,a.Satuan "
		'Using Ds2 = BindingTrans(SQL)
		'    With Ds2.Tables("MyTable")
		'        If .Rows.Count <> 0 Then
		'            For indexPR As Integer = 0 To .Rows.Count - 1

		'                Dim kodeBarangPO As String = .Rows(indexPR).Item("kode_barang").ToString()

		'                ' ambil dari cache (NO QUERY)
		'                convertSatuanPRSudahPO = dictSatuanDisplay(kodeBarangPO)

		'                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarangPO & "'," &
		'                              "'" & .Rows(indexPR).Item("satuan") & "','" & convertSatuanPRSudahPO & "'," &
		'                              .Rows(indexPR).Item("jumlah") & ") as Hasil"

		'                Using dr4 = OpenTrans(SQL)
		'                    If dr4.Read Then
		'                        If General_Class.CekNULL(dr4("Hasil")) <> "" Then
		'                            totalPRSudahPO += dr4("hasil")
		'                        Else
		'                            MessageBox.Show("Satuan tidak ditemukan!")
		'                            Exit Sub
		'                        End If
		'                    End If
		'                End Using

		'            Next

		'            DataGridView1.Rows(RowIndex).Cells(cellPOoutStanding).Value = Format(totalPRSudahPO, "N2")

		'        Else
		'            'jika belum  ada pr maka 0
		'            DataGridView1.Rows(RowIndex).Cells(cellPOoutStanding).Value = 0
		'        End If
		'    End With
		'End Using

		Dim kodeBarang As String = Arrbarang.Item(barangIndex)

		Dim key As String = kodeBarang & "|" & Bln & "|" & CInt(Thn)

		' =========================
		' PR BULANAN
		' =========================
		Dim totalPrBelumPOPerbarang As Double = 0

		If dictPRBulanan.ContainsKey(key) Then
			totalPrBelumPOPerbarang = dictPRBulanan(key)
		End If

		DataGridView1.Rows(RowIndex).Cells(cellPRoutStanding).Value = Format(totalPrBelumPOPerbarang, "N2")

		' =========================
		' PO BULANAN
		' =========================
		Dim totalPRSudahPO As Double = 0

		If dictPOBulanan.ContainsKey(key) Then
			totalPRSudahPO = dictPOBulanan(key)
		End If

		DataGridView1.Rows(RowIndex).Cells(cellPOoutStanding).Value = Format(totalPRSudahPO, "N2")

		'If fstatus = "MRP_PPIC" Then

		'    If aksesUbah = "Y" Then
		'        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & Bln & "' and tahun ='" & Thn & "' "
		'        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
		'        Using dr = OpenTrans(SQL)
		'            If dr.Read Then
		'                FValidasi = "Y"
		'                DataGridView1.Rows(RowIndex).Cells(CellNPPIC).ReadOnly = True
		'                DataGridView1.Rows(RowIndex).Cells(CellNPPIC).Style.BackColor = Color.DarkCyan
		'            Else

		'                dr.Close()

		'                FValidasi = ""
		'                DataGridView1.Rows(RowIndex).Cells(CellNPPIC).ReadOnly = False
		'                DataGridView1.Rows(RowIndex).Cells(CellNPPIC).Style.BackColor = Color.LightCyan

		'            End If
		'        End Using
		'    Else
		'        FValidasi = ""
		'        DataGridView1.Rows(RowIndex).Cells(CellNPPIC).ReadOnly = True
		'        DataGridView1.Rows(RowIndex).Cells(CellNPPIC).Style.BackColor = Color.DarkCyan
		'    End If

		'ElseIf fstatus = "MRP_Formulator" Then
		'    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & Bln & "' and tahun ='" & Thn & "' "
		'    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
		'    Using dr = OpenTrans(SQL)
		'        If dr.Read Then
		'            FValidasi = "Y"

		'            DataGridView1.Rows(RowIndex).Cells(CellNBom).Style.BackColor = Color.DarkGoldenrod
		'        Else
		'            FValidasi = ""

		'            DataGridView1.Rows(RowIndex).Cells(CellNBom).Style.BackColor = Color.LightYellow
		'        End If
		'    End Using
		'End If

		'ada_data = ""
		'SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
		'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & Bln & "' and tahun = '" & Thn & "' "
		''SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(barangIndex) & "' "
		'SQL = SQL & "and Kode_Barang = '" & Arrbarang.Item(barangIndex) & "'"
		'Using Ds2 = BindingTrans(SQL)
		'    With Ds2.Tables("MyTable")
		'        If .Rows.Count <> 0 Then
		'            DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
		'            DataGridView1.Rows(RowIndex).Cells(CellNPPIC).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
		'            DataGridView1.Rows(RowIndex).Cells(CellUrut).Value = .Rows(0).Item("Urut")
		'            DataGridView1.Rows(RowIndex).Cells(CellKosong).Value = ""
		'            ada_data = "T"
		'        Else
		'            DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = 0
		'            DataGridView1.Rows(RowIndex).Cells(CellNPPIC).Value = 0
		'            DataGridView1.Rows(RowIndex).Cells(CellUrut).Value = ""
		'            DataGridView1.Rows(RowIndex).Cells(CellKosong).Value = ""
		'            ada_data = "T"
		'        End If
		'    End With
		'End Using

		If ada_data = "T" Then

		End If
		DataGridView1.Rows(RowIndex).Cells(cellNBom).Value = 0

		Dim nilai As Double = 0

		Dim key1 = kodeBarang & "|" & Bln & "|" & Thn

		If Flag_Raw_Material = "Y" Then
			If dictRM.ContainsKey(key1) Then
				nilai = dictRM(key1)
			End If
		ElseIf Flag_Packaging = "Y" Then
			If dictPkg.ContainsKey(key1) Then
				nilai = dictPkg(key1)
			End If
		End If

		DataGridView1.Rows(RowIndex).Cells(cellNBom).Value = Format(nilai, "N2")
		'If Flag_Raw_Material = "Y" Then

		'    SQL = "WITH cte AS ( "

		'    SQL = SQL & " SELECT b.kode_Barang,b.Kode_Formula,e.satuan_berat,"
		'    SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic-total_qty) AS nilai_ppic,"
		'    SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil) AS nilai_Formula,"
		'    SQL = SQL & " d.kode_barang AS Kode_Bahan,d.Nilai_Barang,d.satuan_barang"
		'    SQL = SQL & " FROM emi_transaksi_sales_forecasting a"
		'    SQL = SQL & " INNER JOIN emi_transaksi_sales_forecasting_detail b ON a.Kode_Perusahaan=b.kode_Perusahaan AND a.no_faktur=b.no_faktur"
		'    SQL = SQL & " INNER JOIN emi_transaksi_formulator c ON b.Kode_Perusahaan=c.Kode_Perusahaan AND b.Kode_Formula=c.No_faktur"
		'    SQL = SQL & " INNER JOIN emi_transaksi_formulator_detail_Bahan d ON c.Kode_Perusahaan=d.Kode_Perusahaan AND c.no_faktur=d.no_faktur"
		'    SQL = SQL & " INNER JOIN init e ON a.kode_Perusahaan=e.kode_Perusahaan"
		'    SQL = SQL & " OUTER APPLY (SELECT SUM(f.Jumlah) AS total_qty FROM N_EMI_Production_Plan_Schedule_Detail f"
		'    SQL = SQL & " WHERE f.Kode_Perusahaan=b.Kode_Perusahaan AND f.Urut_Production_Plan=b.urut) f_sum"

		'    SQL = SQL & " WHERE a.status IS NULL"
		'    SQL = SQL & " AND b.Kode_Perusahaan='" & KodePerusahaan & "'"
		'    SQL = SQL & " AND a.Bulan='" & Bln & "'"
		'    SQL = SQL & " AND a.tahun='" & Thn & "'"
		'    SQL = SQL & " AND b.flag_validasi='Y'"
		'    SQL = SQL & " AND b.flag_validasi_PPIC='Y'"
		'    SQL = SQL & " AND c.status IS NULL"
		'    SQL = SQL & " AND d.kode_Barang='" & Arrbarang.Item(barangIndex) & "'"

		'    SQL = SQL & " UNION ALL "

		'    SQL = SQL & " SELECT b.kode_Barang,f.Kode_Formula,e.satuan_berat,"
		'    SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,f.Jumlah) AS nilai_ppic,"
		'    SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil) AS nilai_Formula,"
		'    SQL = SQL & " d.kode_barang AS Kode_Bahan,d.Nilai_Barang,d.satuan_barang"
		'    SQL = SQL & " FROM emi_transaksi_sales_forecasting a"
		'    SQL = SQL & " INNER JOIN emi_transaksi_sales_forecasting_detail b ON a.Kode_Perusahaan=b.kode_Perusahaan AND a.no_faktur=b.no_faktur"
		'    SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule_Detail f ON f.Kode_Perusahaan=b.Kode_Perusahaan AND f.Urut_Production_Plan=b.urut"
		'    SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule g ON f.Kode_Perusahaan=g.Kode_Perusahaan AND f.No_Transaksi=g.No_Transaksi"
		'    SQL = SQL & " INNER JOIN emi_transaksi_formulator c ON f.Kode_Perusahaan=c.Kode_Perusahaan AND f.Kode_Formula=c.No_Faktur"
		'    SQL = SQL & " INNER JOIN emi_transaksi_formulator_detail_Bahan d ON c.Kode_Perusahaan=d.Kode_Perusahaan AND c.no_faktur=d.no_faktur"
		'    SQL = SQL & " INNER JOIN init e ON a.kode_Perusahaan=e.kode_Perusahaan"

		'    SQL = SQL & " WHERE a.status IS NULL"
		'    SQL = SQL & " AND b.Kode_Perusahaan='" & KodePerusahaan & "'"
		'    SQL = SQL & " AND a.Bulan='" & Bln & "'"
		'    SQL = SQL & " AND a.tahun='" & Thn & "'"
		'    SQL = SQL & " AND b.flag_validasi='Y'"
		'    SQL = SQL & " AND b.flag_validasi_PPIC='Y'"
		'    SQL = SQL & " AND c.status IS NULL"
		'    SQL = SQL & " AND g.Status IS NULL"
		'    SQL = SQL & " AND d.kode_Barang='" & Arrbarang.Item(barangIndex) & "'"

		'    SQL = SQL & ")"

		'    SQL = SQL & " SELECT Kode_Bahan,satuan_barang, kode_formula ,"
		'    SQL = SQL & " SUM(ROUND(Nilai_Barang*(nilai_ppic/nilai_Formula),2)) AS Nilai"
		'    SQL = SQL & " FROM cte"
		'    SQL = SQL & " GROUP BY Kode_Bahan,satuan_barang, kode_formula "
		'    Using ds3 = BindingTrans(SQL)

		'        If ds3.Tables("MyTable").Rows.Count <> 0 Then
		'            For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

		'                Dim jumlah As Double = 0

		'                jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

		'                Dim convertKeSatuanAsli As String = ""
		'                Dim jumlahBarangDibutuhkan As Double = 0

		'                SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
		'                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
		'                Using Dr3 = OpenTrans(SQL)
		'                    If Dr3.Read Then
		'                        convertKeSatuanAsli = Dr3("satuan")

		'                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
		'                        SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
		'                        SQL = SQL & "" & jumlah & ") as Hasil "
		'                        Dr3.Close()

		'                        Using dr4 = OpenTrans(SQL)
		'                            If dr4.Read Then
		'                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then

		'                                    If dr4("hasil") = 0 Then
		'                                        DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = Format(Val(HilangkanTanda(DataGridView1.Rows(RowIndex).Cells(CellNBom).Value)) + 0, "N2")
		'                                    Else
		'                                        DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = Format(Val(HilangkanTanda(DataGridView1.Rows(RowIndex).Cells(CellNBom).Value) + dr4("hasil")), "N2")
		'                                    End If

		'                                Else
		'                                    dr4.Close()
		'                                    CloseConn()
		'                                    MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                                    Exit Sub
		'                                End If
		'                            End If
		'                        End Using
		'                    Else
		'                        Dr3.Close()
		'                        CloseConn()
		'                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                        Exit Sub
		'                    End If
		'                End Using
		'            Next
		'        Else
		'            DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = 0

		'        End If

		'    End Using

		'ElseIf Flag_Packaging = "Y" Then
		'    SQL = ";with cte as ( "
		'    SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
		'    SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
		'    SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
		'    SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
		'    SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
		'    SQL = SQL & "And a.Bulan = '" & Bln & "' and a.tahun = '" & Thn & "' and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y' and "
		'    SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
		'    SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(barangIndex) & "' "
		'    SQL = SQL & ") "
		'    SQL = SQL & "Select Kode_Bahan, satuan_barang,sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) As Nilai "
		'    SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
		'    Using ds3 = BindingTrans(SQL)

		'        If ds3.Tables("MyTable").Rows.Count <> 0 Then
		'            For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

		'                Dim jumlah As Double = 0

		'                jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

		'                Dim convertKeSatuanAsli As String = ""
		'                Dim jumlahBarangDibutuhkan As Double = 0

		'                SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
		'                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
		'                Using Dr3 = OpenTrans(SQL)
		'                    If Dr3.Read Then
		'                        convertKeSatuanAsli = Dr3("satuan")
		'                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
		'                        SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
		'                        SQL = SQL & "" & jumlah & ") as Hasil "
		'                        Dr3.Close()

		'                        Using dr4 = OpenTrans(SQL)
		'                            If dr4.Read Then
		'                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then

		'                                    If dr4("hasil") = 0 Then
		'                                        DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = 0
		'                                    Else
		'                                        DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = Format(dr4("hasil"), "N2")
		'                                    End If

		'                                Else
		'                                    dr4.Close()
		'                                    CloseConn()
		'                                    MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                                    Exit Sub
		'                                End If
		'                            End If
		'                        End Using
		'                    Else
		'                        Dr3.Close()
		'                        CloseConn()
		'                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                        Exit Sub
		'                    End If
		'                End Using
		'            Next
		'        Else
		'            DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = 0

		'        End If

		'    End Using
		'End If

		FunTotalJumlahLooping(HilangkanTanda(HilangkanTanda(DataGridView1.Rows(RowIndex).Cells(cellNBom).Value)), totalPrBelumPOPerbarang, totalPRSudahPO, RowIndex, cellRequireStok, kodeBarang)

	End Sub

	'Public Sub Load_Data_Perbulan(ByVal Flag_Raw_Material As String, ByVal Flag_Packaging As String, ByVal barangIndex As Integer, ByVal Bln As String, ByVal Thn As String, ByVal RowIndex As Integer, ByVal aksesUbah As String, ByVal FValidasi As String, ByVal bulanke As Integer, ByRef dictSatuanDisplay As Dictionary(Of String, String))

	'    Dim ada_data As String = ""

	'    Dim CellNPPIC As Integer = CType(Me.GetType().GetField("CellNPPIC_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
	'    Dim CellNBom As Integer = CType(Me.GetType().GetField("CellNBom_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
	'    Dim CellUrut As Integer = CType(Me.GetType().GetField("CellUrut_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
	'    Dim CellKosong As Integer = CType(Me.GetType().GetField("CellKosong_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)

	'    Dim cellPRoutStanding As Integer = CType(Me.GetType().GetField("CellOPRequesition_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
	'    Dim cellPOoutStanding As Integer = CType(Me.GetType().GetField("CellOPOrder_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
	'    Dim cellRequireStok As Integer = CType(Me.GetType().GetField("cellRequireStok_" & bulanke, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(Me), String)
	'    '=============================== tampil data PR belum PO ==============================='

	'    Dim totalPrBelumPOPerbarang As Double = 0
	'    Dim convertSatuanDisplayPr As String = ""

	'    '---------select ke data pr berdasarkan kode_barang

	'    SQL = "select a.Kode_Perusahaan,a.Kode_Barang,a.Nama,isnull(sum(PR_Outstanding),0) as Jumlah,a.Satuan from N_EMI_View_Material_Requirement_Planning a  "
	'    SQL = SQL & "where MONTH(Tanggal_Delivery) = '" & Bln & "' and YEAR(Tanggal_Delivery) = '" & Thn & "' "
	'    SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "'  "
	'    SQL = SQL & "and a.kode_barang = '" & Arrbarang.Item(barangIndex) & "' "

	'    SQL = SQL & "group by a.Kode_Perusahaan,a.Kode_Barang,a.Nama,a.Satuan "

	'    Using Ds2 = BindingTrans(SQL)
	'        With Ds2.Tables("MyTable")
	'            If .Rows.Count <> 0 Then
	'                For indexPR As Integer = 0 To .Rows.Count - 1

	'                    Dim kodeBarangPR As String = .Rows(indexPR).Item("kode_barang").ToString()

	'                    ' ambil dari cache (NO QUERY)
	'                    convertSatuanDisplayPr = dictSatuanDisplay(kodeBarangPR)

	'                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarangPR & "'," &
	'                                  "'" & .Rows(indexPR).Item("satuan") & "','" & convertSatuanDisplayPr & "'," &
	'                                  .Rows(indexPR).Item("jumlah") & ") as Hasil"

	'                    Using dr4 = OpenTrans(SQL)
	'                        If dr4.Read Then
	'                            If General_Class.CekNULL(dr4("Hasil")) <> "" Then
	'                                totalPrBelumPOPerbarang += dr4("hasil")
	'                            Else
	'                                MessageBox.Show("Satuan tidak ditemukan!")
	'                                Exit Sub
	'                            End If
	'                        End If
	'                    End Using

	'                Next

	'                DataGridView1.Rows(RowIndex).Cells(cellPRoutStanding).Value = Format(totalPrBelumPOPerbarang, "N2")

	'            Else
	'                'jika belum  ada pr maka 0
	'                DataGridView1.Rows(RowIndex).Cells(cellPRoutStanding).Value = 0
	'            End If
	'        End With
	'    End Using

	'    '=========================================== tampil PR sudah po =========================================='
	'    Dim totalPRSudahPO As Double = 0
	'    Dim convertSatuanPRSudahPO As String = ""
	'    '---------select ke data pr berdasarkan kode_barang
	'    'SQL = "select b.Kode_Stock_Owner,b.Kode_Barang,b.Satuan,b.jumlah "
	'    'SQL = SQL & "from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b "
	'    'SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
	'    'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null  "
	'    'SQL = SQL & "and b.kode_stock_owner = '" & Arrlokasi.Item(indexxx) & "' "
	'    'SQL = SQL & "and b.kode_barang = '" & Arrbarang.Item(indexxx) & "' "

	'    SQL = "select a.Kode_Perusahaan,a.Kode_Barang,a.Nama,isnull(sum(PO_Outstanding),0) as Jumlah,a.Satuan from N_EMI_View_Material_Requirement_Planning a  "
	'    SQL = SQL & "where MONTH(Tanggal_Delivery) = '" & Bln & "' and YEAR(Tanggal_Delivery) = '" & Thn & "'  "
	'    SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "'  "
	'    SQL = SQL & "and a.kode_barang = '" & Arrbarang.Item(barangIndex) & "' "

	'    SQL = SQL & "group by a.Kode_Perusahaan,a.Kode_Barang,a.Nama,a.Satuan "
	'    Using Ds2 = BindingTrans(SQL)
	'        With Ds2.Tables("MyTable")
	'            If .Rows.Count <> 0 Then
	'                For indexPR As Integer = 0 To .Rows.Count - 1

	'                    Dim kodeBarangPO As String = .Rows(indexPR).Item("kode_barang").ToString()

	'                    ' ambil dari cache (NO QUERY)
	'                    convertSatuanPRSudahPO = dictSatuanDisplay(kodeBarangPO)

	'                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kodeBarangPO & "'," &
	'                                  "'" & .Rows(indexPR).Item("satuan") & "','" & convertSatuanPRSudahPO & "'," &
	'                                  .Rows(indexPR).Item("jumlah") & ") as Hasil"

	'                    Using dr4 = OpenTrans(SQL)
	'                        If dr4.Read Then
	'                            If General_Class.CekNULL(dr4("Hasil")) <> "" Then
	'                                totalPRSudahPO += dr4("hasil")
	'                            Else
	'                                MessageBox.Show("Satuan tidak ditemukan!")
	'                                Exit Sub
	'                            End If
	'                        End If
	'                    End Using

	'                Next

	'                DataGridView1.Rows(RowIndex).Cells(cellPOoutStanding).Value = Format(totalPRSudahPO, "N2")

	'            Else
	'                'jika belum  ada pr maka 0
	'                DataGridView1.Rows(RowIndex).Cells(cellPOoutStanding).Value = 0
	'            End If
	'        End With
	'    End Using

	'    'If fstatus = "MRP_PPIC" Then

	'    '    If aksesUbah = "Y" Then
	'    '        SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & Bln & "' and tahun ='" & Thn & "' "
	'    '        SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi_PPIC='Y' "
	'    '        Using dr = OpenTrans(SQL)
	'    '            If dr.Read Then
	'    '                FValidasi = "Y"
	'    '                DataGridView1.Rows(RowIndex).Cells(CellNPPIC).ReadOnly = True
	'    '                DataGridView1.Rows(RowIndex).Cells(CellNPPIC).Style.BackColor = Color.DarkCyan
	'    '            Else

	'    '                dr.Close()

	'    '                FValidasi = ""
	'    '                DataGridView1.Rows(RowIndex).Cells(CellNPPIC).ReadOnly = False
	'    '                DataGridView1.Rows(RowIndex).Cells(CellNPPIC).Style.BackColor = Color.LightCyan

	'    '            End If
	'    '        End Using
	'    '    Else
	'    '        FValidasi = ""
	'    '        DataGridView1.Rows(RowIndex).Cells(CellNPPIC).ReadOnly = True
	'    '        DataGridView1.Rows(RowIndex).Cells(CellNPPIC).Style.BackColor = Color.DarkCyan
	'    '    End If

	'    'ElseIf fstatus = "MRP_Formulator" Then
	'    '    SQL = "Select no_faktur from EMI_Transaksi_Material_Requsition a where bulan='" & Bln & "' and tahun ='" & Thn & "' "
	'    '    SQL = SQL & "And status Is null And kode_perusahaan='" & KodePerusahaan & "' and  Flag_validasi='Y' "
	'    '    Using dr = OpenTrans(SQL)
	'    '        If dr.Read Then
	'    '            FValidasi = "Y"

	'    '            DataGridView1.Rows(RowIndex).Cells(CellNBom).Style.BackColor = Color.DarkGoldenrod
	'    '        Else
	'    '            FValidasi = ""

	'    '            DataGridView1.Rows(RowIndex).Cells(CellNBom).Style.BackColor = Color.LightYellow
	'    '        End If
	'    '    End Using
	'    'End If

	'    'ada_data = ""
	'    'SQL = "select Bulan,Tahun,Kode_Barang,Nilai_PPIC,Nilai_Bom,Urut from EMI_Transaksi_Material_Requsition_Detail where "
	'    'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Bulan = '" & Bln & "' and tahun = '" & Thn & "' "
	'    ''SQL = SQL & "Kode_Stock_Owner = '" & Arrlokasi.Item(barangIndex) & "' "
	'    'SQL = SQL & "and Kode_Barang = '" & Arrbarang.Item(barangIndex) & "'"
	'    'Using Ds2 = BindingTrans(SQL)
	'    '    With Ds2.Tables("MyTable")
	'    '        If .Rows.Count <> 0 Then
	'    '            DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = Format(.Rows(0).Item("Nilai_Bom"), "N2")
	'    '            DataGridView1.Rows(RowIndex).Cells(CellNPPIC).Value = Format(.Rows(0).Item("Nilai_PPIC"), "N2")
	'    '            DataGridView1.Rows(RowIndex).Cells(CellUrut).Value = .Rows(0).Item("Urut")
	'    '            DataGridView1.Rows(RowIndex).Cells(CellKosong).Value = ""
	'    '            ada_data = "T"
	'    '        Else
	'    '            DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = 0
	'    '            DataGridView1.Rows(RowIndex).Cells(CellNPPIC).Value = 0
	'    '            DataGridView1.Rows(RowIndex).Cells(CellUrut).Value = ""
	'    '            DataGridView1.Rows(RowIndex).Cells(CellKosong).Value = ""
	'    '            ada_data = "T"
	'    '        End If
	'    '    End With
	'    'End Using

	'    If ada_data = "T" Then

	'    End If
	'    DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = 0
	'    If Flag_Raw_Material = "Y" Then
	'        'SQL = ";with cte as ( "
	'        'SQL = SQL & "select b.kode_Barang, e.satuan_berat,  "
	'        'SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic ) as nilai_ppic, "
	'        'SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil ) as nilai_Formula, "
	'        'SQL = SQL & "d.kode_barang as Kode_Bahan, d.Nilai_Barang, d.satuan_barang "
	'        'SQL = SQL & "from "
	'        'SQL = SQL & "emi_transaksi_sales_forecasting a, emi_transaksi_sales_forecasting_detail b, "
	'        'SQL = SQL & "emi_transaksi_formulator c, emi_transaksi_formulator_detail_Bahan d, init e "
	'        'SQL = SQL & "where a.Kode_Perusahaan =b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
	'        'SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & Bln & "' and a.tahun = '" & Thn & "' "
	'        'SQL = SQL & "and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y'  "
	'        'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.KOde_Formula=c.No_faktur and c.status is null "
	'        'SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.no_faktur=d.no_faktur "
	'        'SQL = SQL & "and d.kode_Barang ='" & Arrbarang.Item(barangIndex) & "' and a.kode_Perusahaan=e.kode_Perusahaan "
	'        'SQL = SQL & ") "
	'        'SQL = SQL & "select Kode_Bahan, satuan_barang, sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)) as Nilai from cte "
	'        'SQL = SQL & "group by Kode_Bahan, satuan_barang "

	'        SQL = "WITH cte AS ( "

	'        SQL = SQL & " SELECT b.kode_Barang,b.Kode_Formula,e.satuan_berat,"
	'        SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,b.nilai_ppic-total_qty) AS nilai_ppic,"
	'        SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil) AS nilai_Formula,"
	'        SQL = SQL & " d.kode_barang AS Kode_Bahan,d.Nilai_Barang,d.satuan_barang"
	'        SQL = SQL & " FROM emi_transaksi_sales_forecasting a"
	'        SQL = SQL & " INNER JOIN emi_transaksi_sales_forecasting_detail b ON a.Kode_Perusahaan=b.kode_Perusahaan AND a.no_faktur=b.no_faktur"
	'        SQL = SQL & " INNER JOIN emi_transaksi_formulator c ON b.Kode_Perusahaan=c.Kode_Perusahaan AND b.Kode_Formula=c.No_faktur"
	'        SQL = SQL & " INNER JOIN emi_transaksi_formulator_detail_Bahan d ON c.Kode_Perusahaan=d.Kode_Perusahaan AND c.no_faktur=d.no_faktur"
	'        SQL = SQL & " INNER JOIN init e ON a.kode_Perusahaan=e.kode_Perusahaan"
	'        SQL = SQL & " OUTER APPLY (SELECT isnull( SUM(f.Jumlah),0) AS total_qty FROM N_EMI_Production_Plan_Schedule_Detail f"
	'        SQL = SQL & " WHERE f.Kode_Perusahaan=b.Kode_Perusahaan AND f.Urut_Production_Plan=b.urut) f_sum"

	'        SQL = SQL & " WHERE a.status IS NULL"
	'        SQL = SQL & " AND b.Kode_Perusahaan='" & KodePerusahaan & "'"
	'        SQL = SQL & " AND a.Bulan='" & Bln & "'"
	'        SQL = SQL & " AND a.tahun='" & Thn & "'"
	'        SQL = SQL & " AND b.flag_validasi='Y'"
	'        SQL = SQL & " AND b.flag_validasi_PPIC='Y'"
	'        SQL = SQL & " AND c.status IS NULL"
	'        SQL = SQL & " AND d.kode_Barang='" & Arrbarang.Item(barangIndex) & "'"

	'        SQL = SQL & " UNION ALL "

	'        SQL = SQL & " SELECT b.kode_Barang,f.Kode_Formula,e.satuan_berat,"
	'        SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,b.satuan,e.satuan_berat,f.Jumlah) AS nilai_ppic,"
	'        SQL = SQL & " dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',b.kode_Barang,c.satuan_hasil,e.satuan_berat,c.hasil) AS nilai_Formula,"
	'        SQL = SQL & " d.kode_barang AS Kode_Bahan,d.Nilai_Barang,d.satuan_barang"
	'        SQL = SQL & " FROM emi_transaksi_sales_forecasting a"
	'        SQL = SQL & " INNER JOIN emi_transaksi_sales_forecasting_detail b ON a.Kode_Perusahaan=b.kode_Perusahaan AND a.no_faktur=b.no_faktur"
	'        SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule_Detail f ON f.Kode_Perusahaan=b.Kode_Perusahaan AND f.Urut_Production_Plan=b.urut"
	'        SQL = SQL & " INNER JOIN N_EMI_Production_Plan_Schedule g ON f.Kode_Perusahaan=g.Kode_Perusahaan AND f.No_Transaksi=g.No_Transaksi"
	'        SQL = SQL & " INNER JOIN emi_transaksi_formulator c ON f.Kode_Perusahaan=c.Kode_Perusahaan AND f.Kode_Formula=c.No_Faktur"
	'        SQL = SQL & " INNER JOIN emi_transaksi_formulator_detail_Bahan d ON c.Kode_Perusahaan=d.Kode_Perusahaan AND c.no_faktur=d.no_faktur"
	'        SQL = SQL & " INNER JOIN init e ON a.kode_Perusahaan=e.kode_Perusahaan"

	'        SQL = SQL & " WHERE a.status IS NULL"
	'        SQL = SQL & " AND b.Kode_Perusahaan='" & KodePerusahaan & "'"
	'        SQL = SQL & " AND a.Bulan='" & Bln & "'"
	'        SQL = SQL & " AND a.tahun='" & Thn & "'"
	'        SQL = SQL & " AND b.flag_validasi='Y'"
	'        SQL = SQL & " AND b.flag_validasi_PPIC='Y'"
	'        SQL = SQL & " AND c.status IS NULL"
	'        SQL = SQL & " AND g.Status IS NULL"
	'        SQL = SQL & " AND d.kode_Barang='" & Arrbarang.Item(barangIndex) & "'"

	'        SQL = SQL & ")"

	'        SQL = SQL & " SELECT Kode_Bahan,satuan_barang, kode_formula ,"
	'        SQL = SQL & " isnull(SUM(ROUND(Nilai_Barang*(nilai_ppic/nilai_Formula),2)),0) AS Nilai"
	'        SQL = SQL & " FROM cte"
	'        SQL = SQL & " GROUP BY Kode_Bahan,satuan_barang, kode_formula "
	'        Using ds3 = BindingTrans(SQL)

	'            If ds3.Tables("MyTable").Rows.Count <> 0 Then
	'                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

	'                    Dim jumlah As Double = 0

	'                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

	'                    Dim convertKeSatuanAsli As String = ""
	'                    Dim jumlahBarangDibutuhkan As Double = 0

	'                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
	'                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
	'                    Using Dr3 = OpenTrans(SQL)
	'                        If Dr3.Read Then
	'                            convertKeSatuanAsli = Dr3("satuan")

	'                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
	'                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
	'                            SQL = SQL & "" & jumlah & ") as Hasil "
	'                            Dr3.Close()

	'                            Using dr4 = OpenTrans(SQL)
	'                                If dr4.Read Then
	'                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then

	'                                        If dr4("hasil") = 0 Then
	'                                            DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = Format(Val(HilangkanTanda(DataGridView1.Rows(RowIndex).Cells(CellNBom).Value)) + 0, "N2")
	'                                        Else
	'                                            DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = Format(Val(HilangkanTanda(DataGridView1.Rows(RowIndex).Cells(CellNBom).Value) + dr4("hasil")), "N2")
	'                                        End If

	'                                    Else
	'                                        dr4.Close()
	'                                        CloseConn()
	'                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'                                        Exit Sub
	'                                    End If
	'                                End If
	'                            End Using
	'                        Else
	'                            Dr3.Close()
	'                            CloseConn()
	'                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'                            Exit Sub
	'                        End If
	'                    End Using
	'                Next
	'            Else
	'                DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = 0

	'            End If

	'        End Using

	'    ElseIf Flag_Packaging = "Y" Then
	'        SQL = ";with cte as ( "
	'        SQL = SQL & "Select b.kode_Barang, b.nilai_ppic As nilai_ppic, c.jumlah_barang As nilai_Formula, "
	'        SQL = SQL & "c.Kode_Bahan, c.Jumlah_Bahan As Nilai_Barang, b.satuan As satuan_barang from emi_transaksi_sales_forecasting a, "
	'        SQL = SQL & "emi_transaksi_sales_forecasting_detail b, barang_detail_bahan_penolong c, barang d "
	'        SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan And a.no_faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan = '" & KodePerusahaan & "' "
	'        SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_barang=d.Kode_Barang And b.kode_stock_owner=d.kode_stock_owner "
	'        SQL = SQL & "And a.Bulan = '" & Bln & "' and a.tahun = '" & Thn & "' and b.flag_validasi='Y' and b.flag_validasi_PPIC='Y' and "
	'        SQL = SQL & "d.Kode_Perusahaan = c.Kode_Perusahaan And d.kode_barang_inq = c.kode_barang "
	'        SQL = SQL & "And c.kode_bahan ='" & Arrbarang.Item(barangIndex) & "' "
	'        SQL = SQL & ") "
	'        SQL = SQL & "Select Kode_Bahan, satuan_barang,isnull(sum(round(nilai_barang*(nilai_ppic/nilai_Formula),2)),0) As Nilai "
	'        SQL = SQL & "From cte Group By Kode_Bahan, satuan_barang "
	'        Using ds3 = BindingTrans(SQL)

	'            If ds3.Tables("MyTable").Rows.Count <> 0 Then
	'                For indexFormulator As Integer = 0 To ds3.Tables("MyTable").Rows.Count - 1

	'                    Dim jumlah As Double = 0

	'                    jumlah = ds3.Tables("MyTable").Rows(indexFormulator).Item("Nilai")

	'                    Dim convertKeSatuanAsli As String = ""
	'                    Dim jumlahBarangDibutuhkan As Double = 0

	'                    SQL = "select satuan From Barang_Detail_Satuan where Kode_barang = '" & ds3.Tables("MyTable").Rows(indexFormulator).Item("kode_bahan") & "' "
	'                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and flag_tampil_display = 'Y' "
	'                    Using Dr3 = OpenTrans(SQL)
	'                        If Dr3.Read Then
	'                            convertKeSatuanAsli = Dr3("satuan")
	'                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & ds3.Tables("MyTable").Rows(indexFormulator).Item("Kode_Bahan") & "',"
	'                            SQL = SQL & "'" & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & "','" & Dr3("satuan") & "',"
	'                            SQL = SQL & "" & jumlah & ") as Hasil "
	'                            Dr3.Close()

	'                            Using dr4 = OpenTrans(SQL)
	'                                If dr4.Read Then
	'                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then

	'                                        If dr4("hasil") = 0 Then
	'                                            DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = 0
	'                                        Else
	'                                            DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = Format(dr4("hasil"), "N2")
	'                                        End If

	'                                    Else
	'                                        dr4.Close()
	'                                        CloseConn()
	'                                        MessageBox.Show("Satuan " & ds3.Tables("MyTable").Rows(indexFormulator).Item("satuan_barang") & " Ke " & convertKeSatuanAsli & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'                                        Exit Sub
	'                                    End If
	'                                End If
	'                            End Using
	'                        Else
	'                            Dr3.Close()
	'                            CloseConn()
	'                            MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'                            Exit Sub
	'                        End If
	'                    End Using
	'                Next
	'            Else
	'                DataGridView1.Rows(RowIndex).Cells(CellNBom).Value = 0

	'            End If

	'        End Using
	'    End If

	'  FunTotalJumlahLooping(HilangkanTanda(HilangkanTanda(DataGridView1.Rows(RowIndex).Cells(CellNBom).Value)), totalPrBelumPOPerbarang, totalPRSudahPO, RowIndex, cellRequireStok)

	'End Sub

End Class