
Imports System.IO
Imports Microsoft.Office.Interop.Excel
Imports excel = Microsoft.Office.Interop.Excel
Imports Forms = System.Windows.Forms

Public Class Tes_Export_Excel_EPPLUS




    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

        If xlApp Is Nothing Then
            MessageBox.Show("Excel is not properly installed!!")
            Return
        End If

        Dim xlWorkBook As excel.Workbook
        Dim xlWorkSheet As excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value

        Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name
        If System.IO.Directory.Exists(lokasi_file) = False Then
            System.IO.Directory.CreateDirectory(lokasi_file)
        End If
        Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
        Dim nama_file As String = "Data_Gudang_ " & format_akhir & ".xlsx"

        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("Sheet1")

        SQL = "exec SP_Pivot_Gudang '" & KodePerusahaan & "', ''"
        ' SQL = SQL & " from Detail_Transaksi_Blok where kode_perusahaan = '" & KodePerusahaan & "' "

        Using Ds = BindingTrans(SQL)
            Dim data As Integer = Ds.Tables("MyTable").Rows.Count
            If data <> 0 Then
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    For j = 0 To Ds.Tables("MyTable").Columns.Count - 1

                        If i = 0 Then 'buat judul
                            xlWorkSheet.Cells(i + 1, j + 1) = Ds.Tables("MyTable").Columns(j).ColumnName
                        End If
                        xlWorkSheet.Cells(i + 2, j + 1) = Ds.Tables("MyTable").Rows(i).Item(j)

                    Next
                Next
            Else
                MessageBox.Show("Tidak Ada Data!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        xlWorkSheet.SaveAs(lokasi_file & "\" & nama_file)
        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlWorkSheet)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Membuat instance Excel Application
        Dim xlApp As New Application

        ' Menonaktifkan peringatan Excel
        xlApp.DisplayAlerts = False

        ' Membuat workbook baru
        Dim xlWorkBook As Workbook = xlApp.Workbooks.Add()
        Dim xlWorkSheet As Worksheet = CType(xlWorkBook.Sheets(1), Worksheet)

        ' Menulis data ke worksheet
        xlWorkSheet.Cells(1, 1).Value = "Nama"
        xlWorkSheet.Cells(1, 2).Value = "Usia"
        xlWorkSheet.Cells(2, 1).Value = "John"
        xlWorkSheet.Cells(2, 2).Value = 25
        xlWorkSheet.Cells(3, 1).Value = "Alice"
        xlWorkSheet.Cells(3, 2).Value = 30

        ' Menggabungkan sel A1 hingga C1
        xlWorkSheet.Range("A1:C1").Merge()
        xlWorkSheet.Cells(1, 1).Value = "Judul Utama"
        xlWorkSheet.Cells(1, 1).HorizontalAlignment = XlHAlign.xlHAlignCenter
        xlWorkSheet.Cells(1, 1).VerticalAlignment = XlVAlign.xlVAlignCenter

        ' Memberi warna latar belakang kuning pada sel A1 setelah digabung
        xlWorkSheet.Cells(1, 1).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)

        ' Memberi warna teks merah pada sel B1
        xlWorkSheet.Cells(1, 2).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)

        ' Mengatur ukuran font di A1 dan membuat font tebal (bold)
        xlWorkSheet.Cells(1, 1).Font.Size = 14
        xlWorkSheet.Cells(1, 1).Font.Bold = True

        ' Mengatur ukuran font di B2 dan membuat font italic
        xlWorkSheet.Cells(2, 2).Font.Size = 12
        xlWorkSheet.Cells(2, 2).Font.Italic = True

        ' Memberi warna latar belakang hijau pada sel B2 dan C2
        xlWorkSheet.Range("B2:C2").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)

        ' Mengatur ukuran kolom A dan B
        xlWorkSheet.Columns("A:B").AutoFit()

        ' Menyusun nama file dengan format waktu
        Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
        Dim nama_file As String = "Testing_Excel_" & format_akhir & ".xlsx"

        ' Tentukan folder tujuan dan buat folder jika belum ada
        Dim folderPath As String = "D:\PEKERJAAN\1. PROJECT\ERP_EMI\bin\Debug\LAPTOP-LA66NMEU"
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If

        ' Menggabungkan folder path dengan nama file untuk mendapatkan path lengkap
        Dim filePath As String = Path.Combine(folderPath, nama_file)

        ' Pastikan path valid sebelum menyimpan file
        If String.IsNullOrWhiteSpace(filePath) OrElse Not Directory.Exists(folderPath) Then
            MessageBox.Show("Path tidak valid atau folder tidak ditemukan.")
            xlApp.Quit()
            Return
        End If

        Try
            ' Menyimpan workbook ke file dengan format Excel (xlsx)
            xlWorkBook.SaveAs(filePath, XlFileFormat.xlOpenXMLWorkbook)
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
            xlApp.Quit()
            Return
        End Try

        ' Menutup aplikasi Excel
        xlApp.Quit()

        ' Memberi pesan bahwa file Excel berhasil dibuat
        MessageBox.Show("File Excel berhasil dibuat di " & filePath)

    End Sub




    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

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

            Dim JudulLaporan As String = "LAPORAN FINAL GI GR"

            Dim xlWorkBook As excel.Workbook
            Dim xlWorkSheet As excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

            If System.IO.Directory.Exists(lokasi_file) = False Then
                System.IO.Directory.CreateDirectory(lokasi_file)
            End If
            Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
            Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("Sheet1")

            '==================================
            '=     DEFINISIKAN NAMA KOLOM     =
            '==================================
#Region "Generate Coloms"

            Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Keterangan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jumlah"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Satuan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Batch"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Berat (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}}, 'Mulai GR 1
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Total (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Good Received GR (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}}, 'Mulai Val
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Good Received GR (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Total (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Good Received Rejected GR (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}}, 'Mulai Rejected
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Good Received Rejected GR (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received GR (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}}, 'Mulai FINAL GR
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received GR (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Loss (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}}
            }

            Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
                {"Default", New Dictionary(Of String, Object) From {
                    {"Default", "Default"},
                    {"Kolom", 0}
                }}
            }

            For i As Integer = 0 To dataKoloms.Count - 1
                Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

                If kolom("Identifier") = "Main" Then

                    xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()
                    xlWorkSheet.Cells(3, i + 1).WrapText = True

                    'BORDER
                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                        .LineStyle = XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "GR1" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()
                    xlWorkSheet.Cells(4, i + 1).WrapText = True

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Line Production (Good Received I)"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Val" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()
                    xlWorkSheet.Cells(4, i + 1).WrapText = True

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Quality Inspection (Good Received II)"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Rejected" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()
                    xlWorkSheet.Cells(4, i + 1).WrapText = True

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Good Received Rejected"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Final" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()
                    xlWorkSheet.Cells(4, i + 1).WrapText = True

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Final Good Received"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                End If

            Next

#End Region


            '=========================
            '=     GENERATE BODY     =
            '=========================

            Dim stringCenter As New List(Of Integer) From {3, 4, 9, 10, 18, 24, 28}
            Dim defaultRowIndex As Integer = 5
            Try
                OpenConn()

                Dim row As Integer = 0
                SQL = "select No_PO, no_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, satuan, batch, Berat_GI, Jumlah_Dosing, NilaiGR1_Pcs, NilaiGR1_KG, ScrapGR1_KG, TotalGR1_KG, Loss_Production, Loss_Production_Persen, Persen_WasteGR1, WaktuGR1, "
                SQL = SQL & "NilaiGR2_Pcs, NilaiGR2_KG, ScrapGR2_KG, TotalGR2_KG, Persen_WasteGR2, WaktuGR2, NilaiAfterGR_Pcs, NilaiAfterGR_KG, Persen_WasteGR3, WaktuGR3, NilaiGRFinal_Pcs, NilaiGRFinal_KG, ScrapGRFinal_KG, Loss_Production_Final_GR, Loss_Production_Final_GR_Persen, Total_Waste "
                SQL = SQL & "from Laporan_Akhir_GIGR "
                SQL = SQL & "where Kode_Perusahaan = '001' "
                SQL = SQL & "and Tgl_Produksi between '2022-12-20 00:00:00.000' and '2030-12-20 00:00:00.000' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")

                        For i As Integer = 0 To .Rows.Count - 1

                            For colIndex As Integer = 0 To .Columns.Count - 1
                                Dim cell = xlWorkSheet.Cells(i + defaultRowIndex, colIndex + 1)
                                cell.Value = General_Class.CekNULL(.Rows(i).Item(colIndex))
                                cell.VerticalAlignment = XlVAlign.xlVAlignCenter

                                ' Format numerik (N2)
                                If .Columns(colIndex).DataType.Name = "Double" Or .Columns(colIndex).DataType.Name = "Decimal" Then
                                    cell.NumberFormat = "#,##0.00"
                                End If

                                '== ATUR ALIGMENT CELL =='
                                Select Case .Columns(colIndex).DataType.Name
                                    Case "String"
                                        cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), XlHAlign.xlHAlignCenter, XlHAlign.xlHAlignLeft)
                                    Case "DateTime"
                                        cell.HorizontalAlignment = XlHAlign.xlHAlignCenter
                                        cell.Value = Format(CDate(.Rows(i).Item(colIndex)), "dd MMM yyyy")
                                    Case "Int32", "Double"
                                        cell.HorizontalAlignment = XlHAlign.xlHAlignRight
                                        cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), XlHAlign.xlHAlignCenter, XlHAlign.xlHAlignRight)
                                End Select

                                ' BORDER
                                With cell.Borders
                                    .LineStyle = XlLineStyle.xlContinuous
                                    .ColorIndex = 0
                                    .Weight = XlBorderWeight.xlThin
                                End With

                                ' BG COLOR
                                Select Case colIndex
                                    Case 13 To 20
                                        If .Columns(colIndex).ColumnName = "WaktuGR1" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(252, 105, 108))
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                                        End If
                                    Case 21 To 26
                                        If .Columns(colIndex).ColumnName = "WaktuGR2" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(181, 230, 162))
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                        End If
                                    Case 27 To 30
                                        If .Columns(colIndex).ColumnName = "WaktuGR3" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                                        End If
                                    Case 31 To 36
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
                                End Select

                                xlWorkSheet.Cells(1, 1).Interior.TintAndShade = 0.2

                            Next

                            row += 1

                        Next

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
                xlWorkSheet.Cells(1, 1).HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, 1).VerticalAlignment = XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()

                '==========================
                '=     FOOTER LAPORAN     =
                '==========================
                Dim jumlahRows As Integer = row + defaultRowIndex

                Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

                xlWorkSheet.Cells(jumlahRows + 1, 1).Value = Footer
                xlWorkSheet.Cells(jumlahRows + 1, 1).HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(jumlahRows + 1, 1).VerticalAlignment = XlVAlign.xlVAlignCenter
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


            'Tampilkan Show Dialog Save as
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim filePath As String = saveFileDialog.FileName

                    xlWorkBook.SaveAs(filePath, XlFileFormat.xlOpenXMLWorkbook)

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


    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub







End Class