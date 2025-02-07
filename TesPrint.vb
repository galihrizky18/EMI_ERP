Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Text

Public Class TesPrint


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            Dim lokasi_file As String = Application.StartupPath & "\" & My.Computer.Name
            Dim password As String = "123"

            Dim format_akhir As String = Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "ddMMMyyyyHHmmss")
            Dim nama_file As String = "Tes_" & format_akhir

            If System.IO.Directory.Exists(lokasi_file) = False Then
                System.IO.Directory.CreateDirectory(lokasi_file)
            End If


            'Dim nama_file As String = Replace(ListView1.FocusedItem.SubItems(1).Text, "/", "") & "_" & format_akhir

            SQL = "select Kode_Perusahaan from Vw_Bukti_Timbang where No_Faktur = 'TK1224-00001'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New Rpt_Bukti_Timbang
                    kertas = "Faktur"

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{Vw_Bukti_Timbang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Bukti_Timbang.No_Faktur} = 'TK1224-00001'"
                    CrDoc.SummaryInfo.ReportTitle = "[DUPLIKAT]"

                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterName
                    'Dim rawKind As Integer
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Letter" Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        Exit For
                    '    End If
                    'Next
                    ''   export_inv("", "PDF", "Letter", Application.StartupPath & "\" & My.Computer.Name, "T")

                    'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)


                    '==================================
                    '=     EXPORT TO PDF PASSWORD     =
                    '==================================
                    Dim fileName As String = lokasi_file & "\" & nama_file & "_Raw.pdf"

                    CrDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileName)
                    Dim fileOutput As String = lokasi_file & "\" & nama_file & ".pdf"
                    AddPasswordToPdf(fileName, fileOutput, password)

                End If
            End Using

            CloseConn()

            'MessageBox.Show("Berhasil di Convert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub


    Public Sub AddPasswordToPdf(ByVal inputFile As String, ByVal outputFile As String, ByVal password As String)

        Try

            Dim IsSuccess As Boolean = False

            ' Membaca file PDF yang sudah ada
            Using reader As New PdfReader(inputFile)
                ' Menggunakan FileStream dengan FileShare.ReadWrite agar file bisa diakses oleh proses lain
                Using fs As New FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
                    ' Membuat PdfStamper untuk menambahkan password ke PDF
                    Dim stamper As New PdfStamper(reader, fs)

                    ' Menambahkan enkripsi dan password ke PDF
                    stamper.SetEncryption(
                        Encoding.UTF8.GetBytes(password),  ' Kata sandi pengguna
                        Encoding.UTF8.GetBytes(password),  ' Kata sandi pemilik
                        PdfWriter.ALLOW_PRINTING Or PdfWriter.ALLOW_COPY,          ' Hak akses yang diizinkan (Print dan ReadOnly)
                        PdfWriter.ENCRYPTION_AES_256       ' Jenis enkripsi
                    )

                    ' Tutup stamper untuk menyimpan perubahan
                    stamper.Close()

                    IsSuccess = True
                End Using
            End Using

            If IsSuccess Then
                File.Delete(inputFile)
            End If

            MessageBox.Show("PDF berhasil diproteksi dengan kata sandi!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            Dim PrinterBarcode As String = "TSC TE210"

            SQL = "select Kode_Perusahaan from Cetak_TransferStock where Kode_Perusahaan='001' and kode_unik_print='012016123108405'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New NewBarcodeTransferStock
                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unik_print & "' and {Cetak_TransferStock.batch} = '" & batchLama & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Stock"
                    '    .Text = "New Barcode Transfer Stock"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '001' and {Cetak_TransferStock.kode_unik_print} = '012016042604209' and {Cetak_TransferStock.batch} = '0120M9B311224' "
                    CrDoc.PrintOptions.PrinterName = PrinterBarcode


                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode
                    CrDoc.PrintToPrinter(1, False, 1, 2500)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        NewGenerateSn("COLD STORAGE", "1111031", "2", DateTime.Now)

    End Sub

    Private Function NewGenerateSn(ByVal KdSo As String, ByVal KdBarang As String, ByVal HPP As String, ByVal Tgl As String) As String

        Dim Alfabet As New ArrayList From {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                       "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        Dim SN As String = ""

        Try
            OpenConn()

            '==================
            '=     CEK SN     =
            '==================
            SQL = "select kode_barang, serial_number from barang_sn where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_stock_owner = '" & KdSo & "' and "
            SQL = SQL & "kode_barang = '" & KdBarang & "'"
            'SQL = SQL & "and serial_number = '" & SN & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

                            Dim FirstCode As String = GetFirstCode(.Rows(i).Item("serial_number"))

                            For j As Integer = 0 To Alfabet.Count - 1

                                If Not Alfabet(j) = FirstCode Then
                                    SN = Alfabet(j) & Tanda_SN & "01" & Tanda_SN & HPP & Tanda_SN & "02" & Tanda_SN & Format(Tgl, "yyyy-MM-dd")
                                    Exit For
                                End If

                            Next

                        Next

                    Else
                        SN = Alfabet(0) & Tanda_SN & "01" & Tanda_SN & HPP & Tanda_SN & "02" & Tanda_SN & Format(Tgl, "yyyy-MM-dd")
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try

        Return SN

    End Function

    Private Function GetFirstCode(ByVal SN As String) As String
        Dim hasil As String = SN.Split("#"c)(0)
        Return hasil
    End Function

End Class