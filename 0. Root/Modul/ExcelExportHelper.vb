Imports System.Globalization
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop

''' <summary>
''' Helper class untuk export data ke Excel dengan fitur lengkap:
''' - Sumber data: DataTable, DataGridView, ListView
''' - Custom nama file
''' - Multi-level header dengan row/col span dan background color
''' - Format cell per kolom atau per cell (conditional formatting)
''' - Auto-detect regional separator (decimal & thousand)
''' - Proteksi format teks (kode barang berawalan 0 tidak hilang)
''' - Auto-fit column width (min = lebar header, max = lebar konten)
''' - Fast Column Color Formatting (mewarnai cell per block/range kolom sangat cepat)
''' </summary>
Public Class ExcelExportHelper

#Region "=== DATA STRUCTURES ==="

	''' <summary>Definisi satu cell header. Bisa span beberapa baris/kolom.</summary>
	Public Class HeaderCell
		Public Property Caption As String = ""
		Public Property RowSpan As Integer = 1
		Public Property ColSpan As Integer = 1
		Public Property BackColor As Color? = Nothing
		Public Property ForeColor As Color? = Nothing
		Public Property Alignment As ExcelAlignment = ExcelAlignment.Center
		Public Property Bold As Boolean = True

		Public Sub New(caption As String,
					   Optional rowSpan As Integer = 1,
					   Optional colSpan As Integer = 1,
					   Optional backColor As Color? = Nothing,
					   Optional foreColor As Color? = Nothing,
					   Optional alignment As ExcelAlignment = ExcelAlignment.Center)
			Me.Caption = caption
			Me.RowSpan = rowSpan
			Me.ColSpan = colSpan
			Me.BackColor = backColor
			Me.ForeColor = foreColor
			Me.Alignment = alignment
		End Sub

	End Class

	''' <summary>Satu baris header, terdiri dari kumpulan <see cref="HeaderCell"/>.</summary>
	Public Class HeaderRow
		Public Property Cells As New List(Of HeaderCell)

		Public Sub New(ParamArray cells() As HeaderCell)
			Me.Cells.AddRange(cells)
		End Sub

		Public Sub AddCell(cell As HeaderCell)
			Me.Cells.Add(cell)
		End Sub

	End Class

	''' <summary>Definisi format untuk satu kolom data (number format, alignment, backcolor dll).</summary>
	Public Class ColumnFormat
		Public Property ColumnIndex As Integer
		Public Property NumberFormat As String = "@"
		Public Property Alignment As ExcelAlignment = ExcelAlignment.Left
		Public Property ForceText As Boolean = False
		Public Property ForceNumber As Boolean = False
		Public Property BackColor As Color? = Nothing

		''' <summary>
		''' <paramref name="numberFormat"/> mendukung shortcut "N0", "N1", "N2", dst
		''' yang otomatis dikonversi ke format Excel (mis. "N2" -> "#,##0.00").
		''' Format Excel standar lain ("@", "dd/mm/yyyy", dll) tetap dipakai apa adanya.
		''' </summary>
		Public Sub New(columnName As String,
			   Optional numberFormat As String = "@",
			   Optional alignment As ExcelAlignment = ExcelAlignment.Left,
			   Optional forceText As Boolean = False,
			   Optional forceNumber As Boolean = False,
			   Optional backColor As Color? = Nothing)

			Me.ColumnIndex = ColumnNameToIndex(columnName)
			Me.NumberFormat = ConvertNumberFormat(numberFormat)
			Me.Alignment = alignment
			Me.ForceText = forceText
			Me.ForceNumber = forceNumber
			Me.BackColor = backColor
		End Sub

		Public Function ColumnNameToIndex(columnName As String) As Integer
			columnName = columnName.ToUpper().Trim()
			Dim result As Integer = 0
			For Each c As Char In columnName
				result = result * 26 + (Asc(c) - Asc("A"c) + 1)
			Next
			Return result - 1
		End Function

		Private Function ConvertNumberFormat(format As String) As String
			If format.StartsWith("N") Then
				Dim decimalPlaces As Integer
				If Integer.TryParse(format.Substring(1), decimalPlaces) Then
					If decimalPlaces = 0 Then
						Return "#,##0"
					Else
						Return "#,##0." & New String("0"c, decimalPlaces)
					End If
				End If
			End If
			Return format
		End Function

	End Class

	''' <summary>
	''' Definisi formula Excel untuk satu kolom, diterapkan ke seluruh baris data.
	''' Gunakan {ROW} sebagai placeholder nomor baris Excel, misal "=A{ROW}*B{ROW}".
	''' NumberFormat dan alignment tetap diambil dari ColumnFormat jika ada.
	''' </summary>
	Public Class ColumnFormula
		Public Property ColumnIndex As Integer
		Public Property Formula As String = ""

		''' <summary>
		''' <paramref name="columnName"/> adalah nama kolom Excel (A, B, C, ..., AA, dst).
		''' <paramref name="formula"/> gunakan {ROW} sebagai placeholder baris,
		''' misal "=A{ROW}*B{ROW}" atau "=IF(C{ROW}>0,D{ROW}/C{ROW},0)".
		''' </summary>
		Public Sub New(columnName As String, formula As String)
			Me.ColumnIndex = ColumnNameToIndex(columnName)
			Me.Formula = formula
		End Sub

		Private Function ColumnNameToIndex(columnName As String) As Integer
			columnName = columnName.ToUpper().Trim()
			Dim result As Integer = 0
			For Each c As Char In columnName
				result = result * 26 + (Asc(c) - Asc("A"c) + 1)
			Next
			Return result - 1
		End Function

	End Class

	''' <summary>
	''' Rule conditional formatting yang dievaluasi per-baris data.
	''' </summary>
	Public Class ConditionalRule

		Public Enum RuleType
			ColumnEquals
			ColumnGreaterThan
			ColumnLessThan
			ColumnContains
			ColumnNotEmpty
			AlwaysApply
		End Enum

		Public Property Type As RuleType = RuleType.AlwaysApply
		Public Property ConditionColumnIndex As Integer = 0
		Public Property ConditionValue As String = ""

		''' <summary>Index kolom target (0-based) yang akan diformat. -1 = semua kolom di baris tersebut.</summary>
		Public Property TargetColumnIndex As Integer = -1

		Public Property BackColor As Color? = Nothing
		Public Property ForeColor As Color? = Nothing
		Public Property Bold As Boolean? = Nothing
		Public Property NumberFormat As String = ""
		Public Property Alignment As ExcelAlignment? = Nothing

		Public Sub New(type As RuleType,
					   conditionColIdx As Integer,
					   conditionValue As String,
					   targetColIdx As Integer,
					   Optional backColor As Color? = Nothing,
					   Optional foreColor As Color? = Nothing,
					   Optional bold As Boolean? = Nothing,
					   Optional numberFormat As String = "",
					   Optional alignment As ExcelAlignment? = Nothing)
			Me.Type = type
			Me.ConditionColumnIndex = conditionColIdx
			Me.ConditionValue = conditionValue
			Me.TargetColumnIndex = targetColIdx
			Me.BackColor = backColor
			Me.ForeColor = foreColor
			Me.Bold = bold
			Me.NumberFormat = numberFormat
			Me.Alignment = alignment
		End Sub

	End Class

	''' <summary>Konfigurasi lengkap untuk satu kali proses export.</summary>
	Public Class ExportConfig
		Public Property FileName As String = ""
		Public Property SheetName As String = "Sheet1"
		Public Property Headers As New List(Of HeaderRow)
		Public Property ColumnFormats As New List(Of ColumnFormat)
		Public Property ConditionalRules As New List(Of ConditionalRule)
		Public Property ShowBorder As Boolean = True
		Public Property DefaultHeaderBackColor As Color = Color.FromArgb(180, 198, 231)
		Public Property FreezePanes As Boolean = True
		Public Property AutoFilter As Boolean = False
		Public Property FontName As String = "Calibri"
		Public Property FontSize As Single = 10
		Public Property HeaderFontSize As Single = 10
		Public Property ColumnFormulas As New List(Of ColumnFormula)
		Public Property FooterText As String = ""
	End Class

	Public Enum ExcelAlignment
		Left
		Center
		Right
	End Enum

#End Region

#Region "=== MAIN EXPORT METHODS ==="

	''' <summary>
	''' Export isi <paramref name="dt"/> ke file Excel (.xlsx) sesuai <paramref name="config"/>.
	''' </summary>
	Public Shared Sub ExportFromDataTable(dt As DataTable, config As ExportConfig)
		Dim xlApp As New Excel.Application()
		If Not ValidateExcel(xlApp) Then Return
		Try
			PerformExport(xlApp, config, Function() GetRowsFromDataTable(dt), dt.Columns.Count)
		Catch ex As Exception
			ShowError(ex)
			SafeQuit(xlApp)
		End Try
	End Sub

	''' <summary>
	''' Export isi <paramref name="dgv"/> ke file Excel (.xlsx) sesuai <paramref name="config"/>.
	''' Jika <paramref name="exportOnlyVisible"/> = True, kolom yang Visible = False
	''' diabaikan dan urutan kolom mengikuti DisplayIndex.
	''' </summary>
	Public Shared Sub ExportFromDataGridView(dgv As DataGridView, config As ExportConfig,
											  Optional exportOnlyVisible As Boolean = True)
		Dim xlApp As New Excel.Application()
		If Not ValidateExcel(xlApp) Then Return
		Try
			Dim cols = dgv.Columns.Cast(Of DataGridViewColumn)().
					   Where(Function(c) Not exportOnlyVisible OrElse c.Visible).ToList()
			PerformExport(xlApp, config,
				Function() GetRowsFromDataGridView(dgv, exportOnlyVisible),
				cols.Count)
		Catch ex As Exception
			ShowError(ex)
			SafeQuit(xlApp)
		End Try
	End Sub

	''' <summary>
	''' Export isi <paramref name="lv"/> (Details view) ke file Excel (.xlsx) sesuai <paramref name="config"/>.
	''' </summary>
	Public Shared Sub ExportFromListView(lv As ListView, config As ExportConfig)
		Dim xlApp As New Excel.Application()
		If Not ValidateExcel(xlApp) Then Return
		Try
			PerformExport(xlApp, config,
				Function() GetRowsFromListView(lv),
				lv.Columns.Count)
		Catch ex As Exception
			ShowError(ex)
			SafeQuit(xlApp)
		End Try
	End Sub

#End Region

#Region "=== CORE ENGINE ==="

	Private Shared Sub PerformExport(xlApp As Excel.Application,
									  config As ExportConfig,
									  getRows As Func(Of List(Of String())),
									  colCount As Integer)

		Dim xlWorkBook As Excel.Workbook = Nothing
		Dim xlWorkSheet As Excel.Worksheet = Nothing

		Try
			xlApp.ScreenUpdating = False
			xlApp.DisplayAlerts = False

			Dim sysDecSep As String = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

			xlWorkBook = xlApp.Workbooks.Add()
			xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
			xlWorkSheet.Name = config.SheetName

			xlApp.Calculation = Excel.XlCalculation.xlCalculationManual

			Dim allRows As List(Of String()) = getRows()
			Dim rowCount As Integer = allRows.Count

			Dim headerRowCount As Integer = WriteHeaders(xlWorkSheet, config, colCount)
			Dim dataStartRow As Integer = headerRowCount + 1

			If rowCount > 0 Then
				Dim colFormatMap = BuildColumnFormatMap(config)
				Dim colFormulaMap = BuildColumnFormulaMap(config)

				WriteDataRows(xlWorkSheet, allRows, dataStartRow, colCount, colFormatMap, colFormulaMap, config, sysDecSep)

				If config.ConditionalRules.Count > 0 Then
					ApplyConditionalFormatting(xlWorkSheet, allRows, dataStartRow, config.ConditionalRules, colCount)
				End If
			End If

			If config.ShowBorder Then
				Dim lastDataRow As Integer = Math.Max(dataStartRow + rowCount - 1, dataStartRow)
				Dim tableRange = xlWorkSheet.Range(
					CType(xlWorkSheet.Cells(1, 1), Excel.Range),
					CType(xlWorkSheet.Cells(lastDataRow, colCount), Excel.Range))
				With tableRange.Borders
					.LineStyle = Excel.XlLineStyle.xlContinuous
					.Weight = Excel.XlBorderWeight.xlThin
				End With
			End If

			If Not String.IsNullOrEmpty(config.FooterText) Then
				Dim footerRowIdx As Integer = dataStartRow + rowCount + 1
				Dim footerCell = CType(xlWorkSheet.Cells(footerRowIdx, 1), Excel.Range)
				footerCell.Value = config.FooterText
				footerCell.Font.Italic = True
				footerCell.Font.Bold = True
			End If

			AutoFitColumns(xlWorkSheet, colCount, headerRowCount, dataStartRow + rowCount - 1)

			If config.FreezePanes AndAlso headerRowCount > 0 Then
				Try
					Dim freezeCell = CType(xlWorkSheet.Cells(headerRowCount + 1, 1), Excel.Range)
					freezeCell.Select()
					xlApp.ActiveWindow.FreezePanes = True
				Catch
					' Non-kritikal, abaikan jika gagal.
				End Try
			End If

			If config.AutoFilter AndAlso headerRowCount > 0 Then
				Try
					Dim filterRange = xlWorkSheet.Range(
						CType(xlWorkSheet.Cells(headerRowCount, 1), Excel.Range),
						CType(xlWorkSheet.Cells(headerRowCount, colCount), Excel.Range))
					filterRange.AutoFilter()
				Catch
					' Non-kritikal, abaikan jika gagal.
				End Try
			End If

			xlApp.ScreenUpdating = True
			xlApp.Calculation = Excel.XlCalculation.xlCalculationAutomatic
			xlApp.DisplayAlerts = True

			Dim namaFile As String = If(String.IsNullOrEmpty(config.FileName),
				"Export_" & Now.ToString("dd_MM_yyyy_HH_mm") & ".xlsx",
				config.FileName)

			Dim saveDialog As New SaveFileDialog()
			saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx"
			saveDialog.FileName = namaFile

			If saveDialog.ShowDialog() = DialogResult.OK Then
				xlWorkBook.SaveAs(saveDialog.FileName, Excel.XlFileFormat.xlOpenXMLWorkbook,
								  Type.Missing, Type.Missing, False, False,
								  Excel.XlSaveAsAccessMode.xlNoChange,
								  Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
				MsgBox("Export berhasil!" & vbNewLine & saveDialog.FileName,
					   MsgBoxStyle.Information, "Export Selesai")
				xlWorkBook.Close(False)
				xlApp.Quit()
			Else
				xlWorkBook.Close(False)
				xlApp.Quit()
			End If
		Catch ex As Exception
			Try : xlApp.ScreenUpdating = True : Catch : End Try
			Try : xlApp.Calculation = Excel.XlCalculation.xlCalculationAutomatic : Catch : End Try
			Try : xlApp.DisplayAlerts = True : Catch : End Try
			Throw
		Finally
			ReleaseObj(xlWorkSheet)
			ReleaseObj(xlWorkBook)
			ReleaseObj(xlApp)
		End Try
	End Sub

#End Region

#Region "=== HEADER WRITER ==="

	Private Shared Function WriteHeaders(ws As Excel.Worksheet,
										  config As ExportConfig,
										  colCount As Integer) As Integer

		If config.Headers Is Nothing OrElse config.Headers.Count = 0 Then Return 0

		Dim totalHeaderRows As Integer = config.Headers.Count

		For rowIdx As Integer = 0 To totalHeaderRows - 1
			Dim hRow As HeaderRow = config.Headers(rowIdx)
			Dim excelRow As Integer = rowIdx + 1
			Dim excelCol As Integer = 1

			For Each hCell As HeaderCell In hRow.Cells

				Do While IsCellMergedFromAbove(ws, excelRow, excelCol, rowIdx)
					excelCol += 1
				Loop

				Dim bgColor As Color = If(hCell.BackColor.HasValue,
					hCell.BackColor.Value, config.DefaultHeaderBackColor)
				Dim fgColor As Color = If(hCell.ForeColor.HasValue,
					hCell.ForeColor.Value, Color.Black)

				If hCell.RowSpan > 1 OrElse hCell.ColSpan > 1 Then
					Dim mergeRange = ws.Range(
						CType(ws.Cells(excelRow, excelCol), Excel.Range),
						CType(ws.Cells(excelRow + hCell.RowSpan - 1,
									   excelCol + hCell.ColSpan - 1), Excel.Range))
					mergeRange.Merge()
					ApplyHeaderStyle(mergeRange, hCell, bgColor, fgColor, config)
					mergeRange.Value = hCell.Caption
				Else
					Dim cell = CType(ws.Cells(excelRow, excelCol), Excel.Range)
					ApplyHeaderStyle(cell, hCell, bgColor, fgColor, config)
					cell.Value = hCell.Caption
				End If

				excelCol += hCell.ColSpan
			Next
		Next

		For r As Integer = 1 To totalHeaderRows
			CType(ws.Rows(r), Excel.Range).RowHeight = 30
		Next

		Return totalHeaderRows
	End Function

	Private Shared Function IsCellMergedFromAbove(ws As Excel.Worksheet,
													currentRow As Integer,
													currentCol As Integer,
													currentHeaderRowIdx As Integer) As Boolean
		If currentHeaderRowIdx = 0 Then Return False
		Try
			Dim cell = CType(ws.Cells(currentRow, currentCol), Excel.Range)
			If cell.MergeCells Then
				Dim mergeArea = cell.MergeArea
				If mergeArea.Row < currentRow Then Return True
			End If
		Catch
		End Try
		Return False
	End Function

	Private Shared Sub ApplyHeaderStyle(rng As Excel.Range,
										 hCell As HeaderCell,
										 bgColor As Color,
										 fgColor As Color,
										 config As ExportConfig)
		With rng
			.Font.Name = config.FontName
			.Font.Size = config.HeaderFontSize
			.Font.Bold = hCell.Bold
			.Font.Color = RGB(fgColor.R, fgColor.G, fgColor.B)
			.Interior.Color = RGB(bgColor.R, bgColor.G, bgColor.B)
			.HorizontalAlignment = ToExcelHAlign(hCell.Alignment)
			.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
			.WrapText = True
		End With
	End Sub

#End Region

#Region "=== DATA WRITER ==="

	Private Shared Sub WriteDataRows(ws As Excel.Worksheet,
								  allRows As List(Of String()),
								  dataStartRow As Integer,
								  colCount As Integer,
								  colFormatMap As Dictionary(Of Integer, ColumnFormat),
								  colFormulaMap As Dictionary(Of Integer, ColumnFormula),
								  config As ExportConfig,
								  sysDecSep As String)

		Dim rowCount As Integer = allRows.Count
		If rowCount = 0 Then Return

		' STEP 1: format kolom (NumberFormat, alignment, font, BACKCOLOR CEPAT)
		For c As Integer = 0 To colCount - 1
			Dim colRange = ws.Range(
				CType(ws.Cells(dataStartRow, c + 1), Excel.Range),
				CType(ws.Cells(dataStartRow + rowCount - 1, c + 1), Excel.Range))

			Dim fmt As String = "@"
			Dim align As ExcelAlignment = ExcelAlignment.Left

			If colFormatMap.ContainsKey(c) Then
				fmt = colFormatMap(c).NumberFormat
				align = colFormatMap(c).Alignment

				' Format warna kolom secara bulk
				If colFormatMap(c).BackColor.HasValue Then
					Dim bc As Color = colFormatMap(c).BackColor.Value
					colRange.Interior.Color = RGB(bc.R, bc.G, bc.B)
				End If
			End If

			colRange.NumberFormat = fmt
			colRange.HorizontalAlignment = ToExcelHAlign(align)
			colRange.Font.Name = config.FontName
			colRange.Font.Size = config.FontSize
		Next

		' STEP 2: isi nilai sel atau formula.
		For r As Integer = 0 To rowCount - 1
			Dim rowData As String() = allRows(r)
			Dim excelRow As Integer = dataStartRow + r

			For c As Integer = 0 To colCount - 1
				Dim cell = CType(ws.Cells(excelRow, c + 1), Excel.Range)

				If colFormulaMap.ContainsKey(c) Then
					Dim formulaStr As String = colFormulaMap(c).Formula.Replace("{ROW}", excelRow.ToString())
					cell.Formula = formulaStr
					Continue For
				End If

				Dim rawVal As String = If(c < rowData.Length, rowData(c), "")

				If colFormatMap.ContainsKey(c) Then
					Dim cf As ColumnFormat = colFormatMap(c)
					If cf.ForceText Then
						cell.Value2 = rawVal
					ElseIf cf.ForceNumber Then
						Dim numVal As Double
						If TryParseNumber(rawVal, numVal) Then
							cell.Value2 = numVal
						Else
							cell.Value2 = rawVal
						End If
					Else
						If cf.NumberFormat = "@" Then
							cell.Value2 = rawVal
						Else
							Dim numVal As Double
							If TryParseNumber(rawVal, numVal) Then
								cell.Value2 = numVal
							Else
								cell.Value2 = rawVal
							End If
						End If
					End If
				Else
					cell.Value2 = rawVal
				End If
			Next
		Next
	End Sub

	Private Shared Function TryParseNumber(value As String, ByRef result As Double) As Boolean
		If String.IsNullOrWhiteSpace(value) Then Return False

		Dim s As String = value.Trim()
		Dim dotCount As Integer = s.Count(Function(ch) ch = "."c)
		Dim commaCount As Integer = s.Count(Function(ch) ch = ","c)

		If commaCount = 1 AndAlso dotCount = 0 Then
			s = s.Replace(",", ".")
		ElseIf commaCount = 1 AndAlso dotCount >= 1 Then
			s = s.Replace(".", "").Replace(",", ".")
		ElseIf commaCount = 0 AndAlso dotCount > 1 Then
			s = s.Replace(".", "")
		End If
		Return Double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, result)
	End Function

#End Region

#Region "=== CONDITIONAL FORMATTING ==="

	Private Shared Sub ApplyConditionalFormatting(ws As Excel.Worksheet,
													allRows As List(Of String()),
													dataStartRow As Integer,
													rules As List(Of ConditionalRule),
													colCount As Integer)
		For r As Integer = 0 To allRows.Count - 1
			Dim rowData As String() = allRows(r)
			Dim excelRow As Integer = dataStartRow + r

			For Each rule As ConditionalRule In rules
				Dim condVal As String = If(rule.ConditionColumnIndex < rowData.Length,
					rowData(rule.ConditionColumnIndex), "")

				If Not EvaluateCondition(rule, condVal) Then Continue For

				Dim colStart As Integer = If(rule.TargetColumnIndex = -1, 1, rule.TargetColumnIndex + 1)
				Dim colEnd As Integer = If(rule.TargetColumnIndex = -1, colCount, rule.TargetColumnIndex + 1)

				Dim rng = ws.Range(
					CType(ws.Cells(excelRow, colStart), Excel.Range),
					CType(ws.Cells(excelRow, colEnd), Excel.Range))

				If rule.BackColor.HasValue Then
					Dim bc As Color = rule.BackColor.Value
					rng.Interior.Color = RGB(bc.R, bc.G, bc.B)
				End If
				If rule.ForeColor.HasValue Then
					Dim fc As Color = rule.ForeColor.Value
					rng.Font.Color = RGB(fc.R, fc.G, fc.B)
				End If
				If rule.Bold.HasValue Then
					rng.Font.Bold = rule.Bold.Value
				End If
				If Not String.IsNullOrEmpty(rule.NumberFormat) Then
					rng.NumberFormat = rule.NumberFormat
				End If
				If rule.Alignment.HasValue Then
					rng.HorizontalAlignment = ToExcelHAlign(rule.Alignment.Value)
				End If
			Next
		Next
	End Sub

	Private Shared Function EvaluateCondition(rule As ConditionalRule, cellValue As String) As Boolean
		Select Case rule.Type
			Case ConditionalRule.RuleType.ColumnEquals
				Return String.Equals(cellValue, rule.ConditionValue, StringComparison.OrdinalIgnoreCase)
			Case ConditionalRule.RuleType.ColumnGreaterThan
				Dim a As Double, b As Double
				Return Double.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, a) AndAlso
					   Double.TryParse(rule.ConditionValue, NumberStyles.Any, CultureInfo.InvariantCulture, b) AndAlso
					   a > b
			Case ConditionalRule.RuleType.ColumnLessThan
				Dim a As Double, b As Double
				Return Double.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, a) AndAlso
					   Double.TryParse(rule.ConditionValue, NumberStyles.Any, CultureInfo.InvariantCulture, b) AndAlso
					   a < b
			Case ConditionalRule.RuleType.ColumnContains
				Return cellValue.IndexOf(rule.ConditionValue, StringComparison.OrdinalIgnoreCase) >= 0
			Case ConditionalRule.RuleType.ColumnNotEmpty
				Return Not String.IsNullOrEmpty(cellValue)
			Case ConditionalRule.RuleType.AlwaysApply
				Return True
			Case Else
				Return False
		End Select
	End Function

#End Region

#Region "=== DATA SOURCE READERS ==="

	Private Shared Function GetRowsFromDataTable(dt As DataTable) As List(Of String())
		Dim result As New List(Of String())
		For Each row As DataRow In dt.Rows
			Dim vals(dt.Columns.Count - 1) As String
			For c As Integer = 0 To dt.Columns.Count - 1
				vals(c) = If(IsDBNull(row(c)) OrElse row(c) Is Nothing, "", row(c).ToString())
			Next
			result.Add(vals)
		Next
		Return result
	End Function

	Private Shared Function GetRowsFromDataGridView(dgv As DataGridView,
													 onlyVisible As Boolean) As List(Of String())
		Dim result As New List(Of String())
		Dim visibleCols = dgv.Columns.Cast(Of DataGridViewColumn)().
			Where(Function(c) Not onlyVisible OrElse c.Visible).
			OrderBy(Function(c) c.DisplayIndex).ToList()

		For Each row As DataGridViewRow In dgv.Rows
			If row.IsNewRow Then Continue For
			Dim vals(visibleCols.Count - 1) As String
			For i As Integer = 0 To visibleCols.Count - 1
				Dim cv = row.Cells(visibleCols(i).Index).Value
				vals(i) = If(cv Is Nothing, "", cv.ToString())
			Next
			result.Add(vals)
		Next
		Return result
	End Function

	Private Shared Function GetRowsFromListView(lv As ListView) As List(Of String())
		Dim result As New List(Of String())
		For Each item As ListViewItem In lv.Items
			Dim vals(lv.Columns.Count - 1) As String
			vals(0) = item.Text
			For s As Integer = 1 To lv.Columns.Count - 1
				vals(s) = If(s < item.SubItems.Count, item.SubItems(s).Text, "")
			Next
			result.Add(vals)
		Next
		Return result
	End Function

#End Region

#Region "=== AUTO-FIT COLUMN WIDTH ==="

	Private Shared Sub AutoFitColumns(ws As Excel.Worksheet,
									   colCount As Integer,
									   headerRows As Integer,
									   lastDataRow As Integer)
		Try
			If headerRows > 0 Then
				Dim hRange = ws.Range(
					CType(ws.Cells(1, 1), Excel.Range),
					CType(ws.Cells(headerRows, colCount), Excel.Range))
				hRange.WrapText = False
			End If

			CType(ws.Cells, Excel.Range).EntireColumn.AutoFit()

			Dim headerWidths(colCount - 1) As Double
			For c As Integer = 1 To colCount
				headerWidths(c - 1) = CType(ws.Columns(c), Excel.Range).ColumnWidth
			Next

			If headerRows > 0 Then
				Dim hRange = ws.Range(
					CType(ws.Cells(1, 1), Excel.Range),
					CType(ws.Cells(headerRows, colCount), Excel.Range))
				hRange.WrapText = True
			End If

			If lastDataRow >= headerRows + 1 Then
				For c As Integer = 1 To colCount
					Dim dataCol = ws.Range(
						CType(ws.Cells(headerRows + 1, c), Excel.Range),
						CType(ws.Cells(lastDataRow, c), Excel.Range))
					dataCol.EntireColumn.AutoFit()

					Dim dataWidth As Double = CType(ws.Columns(c), Excel.Range).ColumnWidth
					Dim finalWidth As Double = Math.Min(Math.Max(headerWidths(c - 1), dataWidth) + 1, 60)
					CType(ws.Columns(c), Excel.Range).ColumnWidth = finalWidth
				Next
			Else
				For c As Integer = 1 To colCount
					CType(ws.Columns(c), Excel.Range).ColumnWidth =
						Math.Min(headerWidths(c - 1) + 1, 60)
				Next
			End If
		Catch
			' Non-kritikal, abaikan jika gagal.
		End Try
	End Sub

#End Region

#Region "=== UTILITIES ==="

	Private Shared Function BuildColumnFormatMap(config As ExportConfig) As Dictionary(Of Integer, ColumnFormat)
		Dim map As New Dictionary(Of Integer, ColumnFormat)
		For Each cf As ColumnFormat In config.ColumnFormats
			map(cf.ColumnIndex) = cf
		Next
		Return map
	End Function

	Private Shared Function ToExcelHAlign(alignment As ExcelAlignment) As Excel.XlHAlign
		Select Case alignment
			Case ExcelAlignment.Center : Return Excel.XlHAlign.xlHAlignCenter
			Case ExcelAlignment.Right : Return Excel.XlHAlign.xlHAlignRight
			Case Else : Return Excel.XlHAlign.xlHAlignLeft
		End Select
	End Function

	Private Shared Function ValidateExcel(xlApp As Excel.Application) As Boolean
		If xlApp Is Nothing Then
			MsgBox("Excel tidak terinstall dengan benar!", MsgBoxStyle.Critical, "Error")
			Return False
		End If
		Return True
	End Function

	Private Shared Sub ShowError(ex As Exception)
		MsgBox("Export gagal: " & ex.Message, MsgBoxStyle.Critical, "Error Export")
	End Sub

	Private Shared Sub SafeQuit(xlApp As Excel.Application)
		Try : xlApp.Quit() : Catch : End Try
		ReleaseObj(xlApp)
	End Sub

	Private Shared Sub ReleaseObj(obj As Object)
		Try
			If obj IsNot Nothing Then Marshal.ReleaseComObject(obj)
		Catch
		End Try
	End Sub

	Private Shared Function BuildColumnFormulaMap(config As ExportConfig) As Dictionary(Of Integer, ColumnFormula)
		Dim map As New Dictionary(Of Integer, ColumnFormula)
		For Each cf As ColumnFormula In config.ColumnFormulas
			map(cf.ColumnIndex) = cf
		Next
		Return map
	End Function

#End Region

	Public Shared Sub ExportFromDataTableDebug(dt As DataTable, config As ExportConfig)
		Dim xlApp As New Excel.Application()
		If Not ValidateExcel(xlApp) Then Return
		PerformExport(xlApp, config, Function() GetRowsFromDataTable(dt), dt.Columns.Count)
	End Sub

End Class