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

End Class