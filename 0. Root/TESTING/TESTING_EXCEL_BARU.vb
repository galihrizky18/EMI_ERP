Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.ConditionalFormatting.Contracts
Imports OfficeOpenXml.Style
Imports excel = Microsoft.Office.Interop.Excel

Public Class TESTING_EXCEL_BARU

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Generate_Excel_Rekap3()
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Generate_Excel_Rekap3_EPPlus()
	End Sub

	Private Sub Generate_Excel_Rekap3()
		Try

			get_jam()

			Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

			'=======================================
			'=     CEK APAKAH EXCEL TERINSTALL     =
			'=======================================
			If xlApp Is Nothing Then
				MessageBox.Show("Excel is not properly installed!!")
				Return
			End If

			Dim isShowScrapPackaging As Boolean = True

			Dim JudulLaporan As String = "LAPORAN FINAL GI GR"

			Dim xlWorkBook As excel.Workbook
			Dim xlWorkSheet As excel.Worksheet
			Dim misValue As Object = System.Reflection.Missing.Value

			Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
			Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("Sheet1")

			xlApp.ScreenUpdating = False
			xlApp.Calculation = excel.XlCalculation.xlCalculationManual
			xlApp.EnableEvents = False

			'==================================
			'=     DEFINISIKAN NAMA KOLOM     =
			'==================================

			Dim DigitDecimal As String = ""
			DigitDecimal = "KG"

#Region "Generate Coloms"

			Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Good Issue"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Good Issue"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Keterangan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "PRO-RQ (" & DigitDecimal & ")"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Berat (Gram)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "PRO-LINE Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 200, 101))}}, 'Mulai Reject
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "QC-LINE Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 200, 101))}},
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "Warehouse-LINE Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 200, 101))}},
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "Total Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 200, 101))}},
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "PRO-LINE (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}}, 'Mulai GR 1
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "QC-LINE (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "Warehouse-LINE (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "Total (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "PRO-LINE (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}}, 'Mulai Val
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "QC-LINE (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "Warehouse-Line (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "Total (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Loss"}, {"Kolom", "LOSS (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
				New Dictionary(Of String, String) From {{"Identifier", "Inspection"}, {"Kolom", "Good Received Inspection (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(233, 69, 69))}}, 'Mulai Inspection
				New Dictionary(Of String, String) From {{"Identifier", "Inspection"}, {"Kolom", "Good Received Inspection (" & DigitDecimal & ")"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(233, 69, 69))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final_GR"}, {"Kolom", "Good Received Rejected (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final_GR"}, {"Kolom", "Good Received Rejected (" & DigitDecimal & ")"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}}, 'Mulai Rejected
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Reject (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Scrap (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received Inspection (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste_Packaging"}, {"Kolom", "Waste Packaging (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(244, 208, 111))}},
				New Dictionary(Of String, String) From {{"Identifier", "Flag_Preservative"}, {"Kolom", "Flag Preservative"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(188, 158, 130))}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Status"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}}
			}

			'Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
			'    {"Default", New Dictionary(Of String, Object) From {
			'        {"Default", "Default"},
			'        {"Kolom", 0}
			'    }}
			'}

			'====================================
			'=     HANDLE FILTER ROLE AKSES     =
			'====================================
			If Not isShowScrapPackaging Then
				dataKoloms = dataKoloms.Where(Function(k) k("Identifier") <> "Waste_Packaging").ToList()
			End If

			Dim tot_kolom As Integer = dataKoloms.Count
			Dim headerRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(3, 1), xlWorkSheet.Cells(4, tot_kolom))

			Dim headerValues(1, tot_kolom - 1) As Object
			Dim groupInfo As New Dictionary(Of String, Tuple(Of Integer, Integer, String))

			For i As Integer = 0 To tot_kolom - 1
				Dim kolom = dataKoloms(i)
				Dim currentIdentifier = kolom("Identifier").ToString()

				If currentIdentifier = "Main" OrElse currentIdentifier = "Loss" OrElse currentIdentifier = "Waste_Packaging" OrElse currentIdentifier = "Flag_Preservative" Then
					headerValues(0, i) = kolom("Kolom").ToString()
				Else
					headerValues(1, i) = kolom("Kolom").ToString()
				End If

				If Not groupInfo.ContainsKey(currentIdentifier) Then
					Dim groupTitle As String = If(currentIdentifier = "Main" OrElse currentIdentifier = "Loss" OrElse currentIdentifier = "Waste_Packaging" OrElse currentIdentifier = "Flag_Preservative", "", currentIdentifier)
					If currentIdentifier = "Inspection" Then groupTitle = "Inspection Good Received"
					If currentIdentifier = "Final_GR" Then groupTitle = "Good Received"
					If currentIdentifier = "Final" Then groupTitle = "Final Report"
					groupInfo.Add(currentIdentifier, Tuple.Create(i + 1, i + 1, groupTitle))
				Else
					Dim currentTuple = groupInfo(currentIdentifier)
					groupInfo(currentIdentifier) = Tuple.Create(currentTuple.Item1, i + 1, currentTuple.Item3)
				End If
			Next

			'Set Judul Kolom
			For Each kvp In groupInfo
				If Not String.IsNullOrEmpty(kvp.Value.Item3) Then
					headerValues(0, kvp.Value.Item1 - 1) = kvp.Value.Item3
				End If
			Next

			headerRange.Value = headerValues

			With headerRange
				.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				.VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				.Font.Bold = True
				.Borders.LineStyle = excel.XlLineStyle.xlContinuous
				.Borders.Weight = excel.XlBorderWeight.xlThin
			End With

			For Each kvp In groupInfo
				Dim identifier = kvp.Key
				Dim startCol = kvp.Value.Item1
				Dim endCol = kvp.Value.Item2
				' Mengambil warna dan melakukan casting dari Object ke Integer
				Dim color = CInt(dataKoloms.First(Function(k) k("Identifier").ToString() = identifier)("Warna"))

				If identifier = "Main" OrElse identifier = "Loss" OrElse identifier = "Waste_Packaging" OrElse identifier = "Flag_Preservative" Then
					For c As Integer = startCol To endCol
						If dataKoloms(c - 1)("Identifier").ToString() = "Main" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Loss" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Waste_Packaging" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Flag_Preservative" Then
							With xlWorkSheet.Range(xlWorkSheet.Cells(3, c), xlWorkSheet.Cells(4, c))
								.Merge()
								.Interior.Color = color
							End With
						End If
					Next
				Else
					Dim topRowRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(3, startCol), xlWorkSheet.Cells(3, endCol))
					Dim bottomRowRange As excel.Range = xlWorkSheet.Range(xlWorkSheet.Cells(4, startCol), xlWorkSheet.Cells(4, endCol))
					topRowRange.Merge()
					topRowRange.Interior.Color = color
					bottomRowRange.Interior.Color = color
				End If
			Next

#Region "Kode Lama"

			'For i As Integer = 0 To dataKoloms.Count - 1
			'    Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

			'    If kolom("Identifier") = "Main" Then

			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

			'        xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        'BORDER
			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Reject" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Reject"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Scrap" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Scrap"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Waste" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Waste"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Loss" Then

			'        'Menambah nilai Range
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

			'        xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).ColumnWidth = 25

			'        'BORDER
			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Inspection" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Inspection Good Received"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Final_GR" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Good Received"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    ElseIf kolom("Identifier") = "Final" Then

			'        'Menambah nilai Range
			'        If rangeKolom.ContainsKey(kolom("Identifier")) Then
			'            rangeKolom(kolom("Identifier"))("akhir") = i + 1
			'        Else

			'            Dim innerData As New Dictionary(Of String, Object)
			'            innerData.Add("awal", i + 1)
			'            innerData.Add("akhir", i + 1)

			'            rangeKolom.Add(kolom("Identifier"), innerData)

			'        End If

			'        xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
			'        xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
			'        xlWorkSheet.Columns(i + 1).AutoFit()

			'        Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
			'        Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
			'        xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
			'        xlWorkSheet.Cells(3, indexAwal).Value = "Final Report"
			'        xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
			'        xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

			'        'BORDER
			'        With xlWorkSheet.Cells(4, i + 1).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
			'            .LineStyle = excel.XlLineStyle.xlContinuous
			'            .ColorIndex = 0
			'            .TintAndShade = 0
			'            .Weight = excel.XlBorderWeight.xlThin
			'        End With

			'        'BG COLOR
			'        xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
			'        xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

			'        'FONT
			'        xlWorkSheet.Cells(3, i + 1).Font.Bold = True
			'        xlWorkSheet.Cells(4, i + 1).Font.Bold = True

			'    End If

			'Next

#End Region

#End Region

			'=========================
			'=     GENERATE BODY     =
			'=========================

			Dim stringCenter As New List(Of Integer) From {2, 3, 4, 5, 6, 26, 37, 38}

			Dim numberColumn As New List(Of Integer) From {8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36}

			Dim DecimalColumn As New List(Of Integer) From {8, 25, 27}

			Dim NumberN0 As New List(Of Integer) From {8, 9, 21, 25, 27}

			Dim defaultRowIndex As Integer = 5

			Try
				OpenConn()

				' Ambil format sesuai culture
				Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
				xlApp.UseSystemSeparators = True

				'==  AMBIL SEPARATOR DARI EXCEL =='
				Dim decimalSep As String = xlApp.DecimalSeparator
				Dim groupSep As String = xlApp.ThousandsSeparator

				'==  AMBIL SEPARATOR DARI SISTEM =='

				If decimalSep = "," Then
					decimalSep = "."
				ElseIf decimalSep = "." Then
					decimalSep = ","
				End If

				If groupSep = "." Then
					groupSep = ","
				ElseIf groupSep = "," Then
					groupSep = "."
				End If

				Dim templateFormat As String = "#GROUP##0DEC0000"
				Dim excelFormat As String = templateFormat _
					.Replace("GROUP", groupSep) _
					.Replace("DEC", decimalSep)

				Dim templateFormatN0 As String = "#GROUP##0"
				Dim excelFormatN0 As String = templateFormatN0 _
					.Replace("GROUP", groupSep)

				Dim jumlahRows As Integer = 0

				Dim row As Integer = 0
				SQL = "select no_po, No_split, Tgl_Produksi, Jam_Produksi, Tanggal_GI, Jam_GI, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Berat_GI, Jumlah_Dosing, "
				SQL = SQL & "Pro_Reject_KG, Qc_Reject_KG, Warehouse_Reject_KG, Tot_Reject_KG, " ' Reject LINE
				SQL = SQL & "ScrapGR1_KG, ScrapGR2_KG, ScrapGR3_KG, ScrapTotal_KG, "
				SQL = SQL & "WasteGR1_KG, WasteGR2_KG, WasteGR3_KG, WasteTotal_KG, Loss_Production_Final_GR, "
				SQL = SQL & "NilaiGRFinal_KG, NilaiGRFinal_Pcs, "
				SQL = SQL & "Reject_Final_Persen, ScrapTotal_Persen, WasteTotal_Persen, Loss_Production_Final_GR_Persen, NilaiGRFinal_Persen, GR_Inspection_KG, "
				SQL = SQL & "GR_Inspection_PCS, GR_Inspection_Persen, "
				If isShowScrapPackaging Then
					SQL = SQL & "Jumlah_Scrap_Packaging, "
				End If
				SQL = SQL & "Flag_Preservative, Status "
				SQL = SQL & "from Laporan_Akhir_GIGR_Rekap2 "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Tgl_Produksi between '2024-01-01' and '2030-01-01' "

				SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")

						If .Rows.Count = 0 Then
							CloseConn()
							MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If

						Dim rowCount As Integer = .Rows.Count
						Dim colCount As Integer = .Columns.Count
						jumlahRows = rowCount

						Dim startCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex, 1)
						Dim endCell As excel.Range = xlWorkSheet.Cells(defaultRowIndex + rowCount - 1, colCount)
						Dim dataRange As excel.Range = xlWorkSheet.Range(startCell, endCell)

						Dim dataArray(rowCount - 1, colCount - 1) As Object

						For r As Integer = 0 To rowCount - 1
							For c As Integer = 0 To colCount - 1
								Dim currentValue As Object = .Rows(r)(c)
								If TypeOf currentValue Is Date Then
									dataArray(r, c) = CDate(currentValue).ToString("dd MMM yyyy")
								Else
									dataArray(r, c) = General_Class.CekNULL(currentValue)
								End If
							Next
						Next

						dataRange.Value = dataArray

						dataRange.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

						With dataRange.Borders
							.LineStyle = excel.XlLineStyle.xlContinuous
							.ColorIndex = 0
							.Weight = excel.XlBorderWeight.xlThin
						End With

						' Atur warna, format, dan style per celnya
						For c As Integer = 1 To colCount
							Dim colIndex As Integer = c - 1 ' Index berbasis 0
							Dim currentColumn As excel.Range = dataRange.Columns(c)

							If colIndex = 6 Then
								currentColumn.NumberFormat = "@"
							End If

							' Alignment untuk kolom String (Text)
							If stringCenter.Contains(colIndex) Then
								currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
							Else
								currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignLeft
							End If

							' Format kolom numerik (N4 atau N0)
							If numberColumn.Contains(colIndex) Then
								If NumberN0.Contains(colIndex) Then
									currentColumn.NumberFormat = excelFormatN0
								Else
									currentColumn.NumberFormat = excelFormat
								End If
								currentColumn.HorizontalAlignment = excel.XlHAlign.xlHAlignRight

							End If
						Next

						xlWorkSheet.Range(dataRange.Columns(14), dataRange.Columns(17)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 217, 153))
						xlWorkSheet.Range(dataRange.Columns(18), dataRange.Columns(21)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
						xlWorkSheet.Range(dataRange.Columns(22), dataRange.Columns(25)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
						dataRange.Columns(26).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
						xlWorkSheet.Range(dataRange.Columns(27), dataRange.Columns(28)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCoral)
						xlWorkSheet.Range(dataRange.Columns(29), dataRange.Columns(30)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
						xlWorkSheet.Range(dataRange.Columns(31), dataRange.Columns(36)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCyan)

						If isShowScrapPackaging Then
							dataRange.Columns(37).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(249, 230, 117))
							dataRange.Columns(38).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(217, 200, 185))
						Else
							'dataRange.Columns(37).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(249, 230, 117))
							dataRange.Columns(37).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(217, 200, 185))
						End If

						Dim targetColorRange As excel.Range = dataRange.Columns(39) ' Kolom yang akan diwarnai

						' Hapus format kondisi lama jika ada
						targetColorRange.FormatConditions.Delete()

						' Kondisi 1: PRODUCTION
						Dim fc1 As excel.FormatCondition = targetColorRange.FormatConditions.Add(
						Type:=excel.XlFormatConditionType.xlExpression,
						Formula1:="=$AI" & defaultRowIndex & "=""PRODUCTION""")
						fc1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)

						' Kondisi 2: INSPECTION
						Dim fc2 As excel.FormatCondition = targetColorRange.FormatConditions.Add(
						Type:=excel.XlFormatConditionType.xlExpression,
						Formula1:="=$AI" & defaultRowIndex & "=""INSPECTION""")
						fc2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)

						' Kondisi 3: WAREHOUSE
						Dim fc3 As excel.FormatCondition = targetColorRange.FormatConditions.Add(
						Type:=excel.XlFormatConditionType.xlExpression,
						Formula1:="=$AI" & defaultRowIndex & "=""WAREHOUSE""")
						fc3.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

						'dataRange.Columns.AutoFit()

					End With
				End Using

				' AutoFit kolom setelah semua data dimasukkan
				xlWorkSheet.Columns.AutoFit()

				'==========================
				'=     HEADER LAPORAN     =
				'==========================
				Dim panjangKolom As Integer = dataKoloms.Count

				xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

				xlWorkSheet.Cells(1, 1).Value = JudulLaporan
				xlWorkSheet.Cells(1, 1).Font.Size = 14
				xlWorkSheet.Cells(1, 1).Font.Bold = True
				xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				xlWorkSheet.Columns(1).AutoFit()

				'==========================
				'=     FOOTER LAPORAN     =
				'==========================

				Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).Value = Footer
				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
				xlWorkSheet.Cells((jumlahRows + defaultRowIndex) + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
				xlWorkSheet.Columns(1).AutoFit()

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			'=====================
			'=     SAVE FILE     =
			'=====================
			Dim saveFileDialog As New SaveFileDialog()

			' Set File Filter
			saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
			saveFileDialog.Title = "Save As"

			xlApp.ScreenUpdating = True
			xlApp.Calculation = excel.XlCalculation.xlCalculationAutomatic
			xlApp.EnableEvents = True

			'Tampilkan Show Dialog Save as
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					Dim filePath As String = saveFileDialog.FileName

					xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

					'MessageBox.Show("File berhasil disimpan di: " & filePath)

					' Menutup workbook dan aplikasi Excel
					xlWorkBook.Close()
					xlApp.Quit()

					' Membebaskan objek Excel
					releaseObject(xlWorkSheet)
					releaseObject(xlWorkBook)
					releaseObject(xlApp)
				Catch ex As Exception
					MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
				End Try
			End If
		Catch ex As Exception
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Generate_Excel_Rekap3_EPPlus()
		' INI KODE UNUTK GUNAKNA NON COMMERCIAL
		'OfficeOpenXml.ExcelPackage.License.SetNonCommercialOrganization("Evo")

		Try
			get_jam()

			Dim isShowScrapPackaging As Boolean = True
			Dim JudulLaporan As String = "LAPORAN FINAL GI GR"
			Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
			Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

			Dim DigitDecimal As String = "KG"

#Region "Generate Coloms"

			' Dictionary Anda tetap sama persis
			Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Produksi"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Good Issue"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Good Issue"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Keterangan"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "PRO-RQ (" & DigitDecimal & ")"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Berat (Gram)"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", ColorTranslator.ToOle(Color.White)}},
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "PRO-LINE Reject (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(253, 200, 101))}},
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "QC-LINE Reject (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(253, 200, 101))}},
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "Warehouse-LINE Reject (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(253, 200, 101))}},
				New Dictionary(Of String, String) From {{"Identifier", "Reject"}, {"Kolom", "Total Reject (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(253, 200, 101))}},
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "PRO-LINE (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "QC-LINE (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "Warehouse-LINE (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "Total (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(253, 255, 167))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "PRO-LINE (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "QC-LINE (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "Warehouse-Line (PCS)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste"}, {"Kolom", "Total (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(131, 197, 217))}},
				New Dictionary(Of String, String) From {{"Identifier", "Loss"}, {"Kolom", "LOSS (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(89, 229, 89))}},
				New Dictionary(Of String, String) From {{"Identifier", "Inspection"}, {"Kolom", "Good Received Inspection (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(233, 69, 69))}},
				New Dictionary(Of String, String) From {{"Identifier", "Inspection"}, {"Kolom", "Good Received Inspection (" & DigitDecimal & ")"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(233, 69, 69))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final_GR"}, {"Kolom", "Good Received Rejected (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final_GR"}, {"Kolom", "Good Received Rejected (" & DigitDecimal & ")"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(174, 174, 174))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Reject (%)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Scrap (%)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Waste (%)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Loss (%)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received Inspection (%)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received (%)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(159, 255, 255))}},
				New Dictionary(Of String, String) From {{"Identifier", "Waste_Packaging"}, {"Kolom", "Waste Packaging (KG)"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(244, 208, 111))}},
				New Dictionary(Of String, String) From {{"Identifier", "Flag_Preservative"}, {"Kolom", "Flag Preservative"}, {"Warna", ColorTranslator.ToOle(Color.FromArgb(188, 158, 130))}},
				New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Status"}, {"Warna", ColorTranslator.ToOle(Color.White)}}
			}

#End Region

			If Not isShowScrapPackaging Then
				dataKoloms = dataKoloms.Where(Function(k) k("Identifier") <> "Waste_Packaging").ToList()
			End If

			Dim tot_kolom As Integer = dataKoloms.Count
			Dim groupInfo As New Dictionary(Of String, Tuple(Of Integer, Integer, String))

			Dim stringCenter As New List(Of Integer) From {2, 3, 4, 5, 6, 26, 37, 38}
			Dim numberColumn As New List(Of Integer) From {10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36}
			Dim NumberN0 As New List(Of Integer) From {23, 27}
			Dim defaultRowIndex As Integer = 5
			Dim jumlahRows As Integer = 0

			Using package As New ExcelPackage()
				Dim ws As ExcelWorksheet = package.Workbook.Worksheets.Add("Sheet1")

				' --- HEADER SETUP ---
				Dim headerValues(1, tot_kolom - 1) As Object
				For i As Integer = 0 To tot_kolom - 1
					Dim kolom = dataKoloms(i)
					Dim currentIdentifier = kolom("Identifier").ToString()

					If currentIdentifier = "Main" OrElse currentIdentifier = "Loss" OrElse currentIdentifier = "Waste_Packaging" OrElse currentIdentifier = "Flag_Preservative" Then
						headerValues(0, i) = kolom("Kolom").ToString()
					Else
						headerValues(1, i) = kolom("Kolom").ToString()
					End If

					If Not groupInfo.ContainsKey(currentIdentifier) Then
						Dim groupTitle As String = If(currentIdentifier = "Main" OrElse currentIdentifier = "Loss" OrElse currentIdentifier = "Waste_Packaging" OrElse currentIdentifier = "Flag_Preservative", "", currentIdentifier)
						If currentIdentifier = "Inspection" Then groupTitle = "Inspection Good Received"
						If currentIdentifier = "Final_GR" Then groupTitle = "Good Received"
						If currentIdentifier = "Final" Then groupTitle = "Final Report"
						groupInfo.Add(currentIdentifier, Tuple.Create(i + 1, i + 1, groupTitle))
					Else
						Dim currentTuple = groupInfo(currentIdentifier)
						groupInfo(currentIdentifier) = Tuple.Create(currentTuple.Item1, i + 1, currentTuple.Item3)
					End If
				Next

				For Each kvp In groupInfo
					If Not String.IsNullOrEmpty(kvp.Value.Item3) Then
						headerValues(0, kvp.Value.Item1 - 1) = kvp.Value.Item3
					End If
				Next

				Dim headerRange As ExcelRange = ws.Cells(3, 1, 4, tot_kolom)
				headerRange.LoadFromArrays({
					Enumerable.Range(0, tot_kolom).Select(Function(x) headerValues(0, x)).ToArray(),
					Enumerable.Range(0, tot_kolom).Select(Function(x) headerValues(1, x)).ToArray()
				})

				With headerRange.Style
					.HorizontalAlignment = ExcelHorizontalAlignment.Center
					.VerticalAlignment = ExcelVerticalAlignment.Center
					.Font.Bold = True
					.Border.Top.Style = ExcelBorderStyle.Thin
					.Border.Bottom.Style = ExcelBorderStyle.Thin
					.Border.Left.Style = ExcelBorderStyle.Thin
					.Border.Right.Style = ExcelBorderStyle.Thin
				End With

				For Each kvp In groupInfo
					Dim identifier = kvp.Key
					Dim startCol = kvp.Value.Item1
					Dim endCol = kvp.Value.Item2
					Dim oColor As Integer = CInt(dataKoloms.First(Function(k) k("Identifier").ToString() = identifier)("Warna"))
					Dim rgbColor As Color = ColorTranslator.FromOle(oColor) ' Convert OLE back to RGB

					If identifier = "Main" OrElse identifier = "Loss" OrElse identifier = "Waste_Packaging" OrElse identifier = "Flag_Preservative" Then
						For c As Integer = startCol To endCol
							If dataKoloms(c - 1)("Identifier").ToString() = "Main" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Loss" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Waste_Packaging" OrElse dataKoloms(c - 1)("Identifier").ToString() = "Flag_Preservative" Then
								ws.Cells(3, c, 4, c).Merge = True
								ws.Cells(3, c, 4, c).Style.Fill.PatternType = ExcelFillStyle.Solid
								ws.Cells(3, c, 4, c).Style.Fill.BackgroundColor.SetColor(rgbColor)
							End If
						Next
					Else
						ws.Cells(3, startCol, 3, endCol).Merge = True
						ws.Cells(3, startCol, 4, endCol).Style.Fill.PatternType = ExcelFillStyle.Solid
						ws.Cells(3, startCol, 4, endCol).Style.Fill.BackgroundColor.SetColor(rgbColor)
					End If
				Next

				' --- DATA LOAD ---
				OpenConn()
				SQL = "select no_po, No_split, Tgl_Produksi, Jam_Produksi, Tanggal_GI, Jam_GI, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Berat_GI, Jumlah_Dosing, "
				SQL &= "Pro_Reject_KG, Qc_Reject_KG, Warehouse_Reject_KG, Tot_Reject_KG, "
				SQL &= "ScrapGR1_KG, ScrapGR2_KG, ScrapGR3_KG, ScrapTotal_KG, "
				SQL &= "WasteGR1_KG, WasteGR2_KG, WasteGR3_KG, WasteTotal_KG, Loss_Production_Final_GR, "
				SQL &= "NilaiGRFinal_KG, NilaiGRFinal_Pcs, "
				SQL &= "Reject_Final_Persen, ScrapTotal_Persen, WasteTotal_Persen, Loss_Production_Final_GR_Persen, NilaiGRFinal_Persen, GR_Inspection_KG, "
				SQL &= "GR_Inspection_PCS, GR_Inspection_Persen, "
				If isShowScrapPackaging Then
					SQL &= "Jumlah_Scrap_Packaging, "
				End If
				SQL &= "Flag_Preservative, Status "
				SQL &= "from Laporan_Akhir_GIGR_Rekap2 "
				SQL &= "where kode_perusahaan = '" & KodePerusahaan & "' "
				SQL &= "and Tgl_Produksi between '2024-01-01' and '2030-01-01' "
				SQL &= "order by no_split, Tgl_Produksi, Jam_Produksi "

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count = 0 Then
							CloseConn()
							MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If

						jumlahRows = .Rows.Count
						Dim colCount As Integer = .Columns.Count

						' Prepare List for EPPlus bulk load
						Dim dataList As New List(Of Object())

						' Setup Number Formats & Alignments
						For Each idx In stringCenter
							ws.Cells(defaultRowIndex, idx + 1, defaultRowIndex + jumlahRows - 1, idx + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						Next

						For Each idx In numberColumn
							Dim colDataRange As ExcelRange = ws.Cells(defaultRowIndex, idx + 1, defaultRowIndex + jumlahRows - 1, idx + 1)

							colDataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right

							If NumberN0.Contains(idx) Then
								colDataRange.Style.Numberformat.Format = "#,##0"
							Else
								colDataRange.Style.Numberformat.Format = "#,##0.0000"
							End If
						Next

						For r As Integer = 0 To jumlahRows - 1
							Dim rowData(colCount - 1) As Object
							For c As Integer = 0 To colCount - 1
								Dim currentValue As Object = .Rows(r)(c)

								If TypeOf currentValue Is Date Then
									rowData(c) = CDate(currentValue).ToString("dd MMM yyyy")
								ElseIf numberColumn.Contains(c) Then
									Dim numVal As Decimal = 0
									Decimal.TryParse(General_Class.CekNULL(currentValue).ToString(), numVal)
									rowData(c) = numVal
								Else
									rowData(c) = General_Class.CekNULL(currentValue)
								End If
							Next
							dataList.Add(rowData)
						Next

						' Load Data
						ws.Cells(defaultRowIndex, 1).LoadFromArrays(dataList)

						Dim dataRange As ExcelRange = ws.Cells(defaultRowIndex, 1, defaultRowIndex + jumlahRows - 1, colCount)
						' Borders & Alignments default+
						With dataRange.Style
							.VerticalAlignment = ExcelVerticalAlignment.Center
							.HorizontalAlignment = ExcelHorizontalAlignment.Left ' Default rata kiri
							.Border.Top.Style = ExcelBorderStyle.Thin
							.Border.Bottom.Style = ExcelBorderStyle.Thin
							.Border.Left.Style = ExcelBorderStyle.Thin
							.Border.Right.Style = ExcelBorderStyle.Thin
						End With

						' Format Text Kolom 7
						ws.Cells(defaultRowIndex, 7, defaultRowIndex + jumlahRows - 1, 7).Style.Numberformat.Format = "@"
						For Each idx In stringCenter
							ws.Cells(defaultRowIndex, idx + 1, defaultRowIndex + jumlahRows - 1, idx + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						Next

						For Each idx In numberColumn
							Dim colDataRange As ExcelRange = ws.Cells(defaultRowIndex, idx + 1, defaultRowIndex + jumlahRows - 1, idx + 1)

							colDataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right ' Set rata kanan

							If NumberN0.Contains(idx) Then
								colDataRange.Style.Numberformat.Format = "#,##0"
							Else
								colDataRange.Style.Numberformat.Format = "#,##0.0000" ' 4 Digit Desimal
							End If
						Next

						' Column Background Colors (Data Body)
						Dim FillSolid As ExcelFillStyle = ExcelFillStyle.Solid
						ws.Cells(defaultRowIndex, 14, defaultRowIndex + jumlahRows - 1, 17).Style.Fill.PatternType = FillSolid
						ws.Cells(defaultRowIndex, 14, defaultRowIndex + jumlahRows - 1, 17).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(253, 217, 153))

						ws.Cells(defaultRowIndex, 18, defaultRowIndex + jumlahRows - 1, 21).Style.Fill.PatternType = FillSolid
						ws.Cells(defaultRowIndex, 18, defaultRowIndex + jumlahRows - 1, 21).Style.Fill.BackgroundColor.SetColor(Color.LightYellow)

						ws.Cells(defaultRowIndex, 22, defaultRowIndex + jumlahRows - 1, 25).Style.Fill.PatternType = FillSolid
						ws.Cells(defaultRowIndex, 22, defaultRowIndex + jumlahRows - 1, 25).Style.Fill.BackgroundColor.SetColor(Color.LightBlue)

						ws.Cells(defaultRowIndex, 26, defaultRowIndex + jumlahRows - 1, 26).Style.Fill.PatternType = FillSolid
						ws.Cells(defaultRowIndex, 26, defaultRowIndex + jumlahRows - 1, 26).Style.Fill.BackgroundColor.SetColor(Color.LightGreen)

						ws.Cells(defaultRowIndex, 27, defaultRowIndex + jumlahRows - 1, 28).Style.Fill.PatternType = FillSolid
						ws.Cells(defaultRowIndex, 27, defaultRowIndex + jumlahRows - 1, 28).Style.Fill.BackgroundColor.SetColor(Color.LightCoral)

						ws.Cells(defaultRowIndex, 29, defaultRowIndex + jumlahRows - 1, 30).Style.Fill.PatternType = FillSolid
						ws.Cells(defaultRowIndex, 29, defaultRowIndex + jumlahRows - 1, 30).Style.Fill.BackgroundColor.SetColor(Color.LightGray)

						ws.Cells(defaultRowIndex, 31, defaultRowIndex + jumlahRows - 1, 36).Style.Fill.PatternType = FillSolid
						ws.Cells(defaultRowIndex, 31, defaultRowIndex + jumlahRows - 1, 36).Style.Fill.BackgroundColor.SetColor(Color.LightCyan)

						If isShowScrapPackaging Then
							ws.Cells(defaultRowIndex, 37, defaultRowIndex + jumlahRows - 1, 37).Style.Fill.PatternType = FillSolid
							ws.Cells(defaultRowIndex, 37, defaultRowIndex + jumlahRows - 1, 37).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(249, 230, 117))

							ws.Cells(defaultRowIndex, 38, defaultRowIndex + jumlahRows - 1, 38).Style.Fill.PatternType = FillSolid
							ws.Cells(defaultRowIndex, 38, defaultRowIndex + jumlahRows - 1, 38).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 200, 185))
						Else
							ws.Cells(defaultRowIndex, 37, defaultRowIndex + jumlahRows - 1, 37).Style.Fill.PatternType = FillSolid
							ws.Cells(defaultRowIndex, 37, defaultRowIndex + jumlahRows - 1, 37).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 200, 185))
						End If

						' Conditional Formatting Kolom 39
						Dim condFormatRange As ExcelAddress = New ExcelAddress(defaultRowIndex, 39, defaultRowIndex + jumlahRows - 1, 39)

						Dim cfProd As IExcelConditionalFormattingExpression = ws.ConditionalFormatting.AddExpression(condFormatRange)
						cfProd.Formula = "=$AI" & defaultRowIndex & "=""PRODUCTION"""
						cfProd.Style.Fill.BackgroundColor.Color = Color.LightYellow

						Dim cfInsp As IExcelConditionalFormattingExpression = ws.ConditionalFormatting.AddExpression(condFormatRange)
						cfInsp.Formula = "=$AI" & defaultRowIndex & "=""INSPECTION"""
						cfInsp.Style.Fill.BackgroundColor.Color = Color.LightBlue

						Dim cfWare As IExcelConditionalFormattingExpression = ws.ConditionalFormatting.AddExpression(condFormatRange)
						cfWare.Formula = "=$AI" & defaultRowIndex & "=""WAREHOUSE"""
						cfWare.Style.Fill.BackgroundColor.Color = Color.LightGray

					End With
				End Using

				' AutoFit (Sangat cepat di EPPlus)
				ws.Cells.AutoFitColumns()

				' --- HEADER JUDUL ---
				ws.Cells(1, 1, 1, tot_kolom).Merge = True
				ws.Cells(1, 1).Value = JudulLaporan
				ws.Cells(1, 1).Style.Font.Size = 14
				ws.Cells(1, 1).Style.Font.Bold = True
				ws.Cells(1, 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
				ws.Cells(1, 1).Style.VerticalAlignment = ExcelVerticalAlignment.Center

				' --- FOOTER TANGGAL ---
				Dim FooterText As String = "| " & Format(Now, "dd MMM yyyy") & " | " & Format(Now, "HH:mm:ss")
				Dim footerRow As Integer = (jumlahRows + defaultRowIndex) + 1
				ws.Cells(footerRow, 1).Value = FooterText
				ws.Cells(footerRow, 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
				ws.Cells(footerRow, 1).Style.VerticalAlignment = ExcelVerticalAlignment.Center

				CloseConn()

				' --- SAVE FILE ---
				Dim saveFileDialog As New SaveFileDialog()
				saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
				saveFileDialog.Title = "Save As"
				saveFileDialog.FileName = nama_file

				If saveFileDialog.ShowDialog() = DialogResult.OK Then
					Dim fi As New FileInfo(saveFileDialog.FileName)
					package.SaveAs(fi)
					MessageBox.Show("File berhasil disimpan!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
				End If

			End Using
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

End Class