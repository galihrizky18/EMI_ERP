Imports System.IO
Imports iTextSharp.text.pdf

Public Class Testing_Cetak_Barcode


    Dim Random As New Random()

    'Private imageBytes3 As Byte = Nothing
    'Private FileSize3 As UInt32
    'Private rawData3() As Byte
    'Private fs3 As FileStream

    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Private Sub Btn_Cetak_Click(sender As Object, e As EventArgs) Handles Btn_Cetak.Click

        get_jam()

        Dim kode_unik_print As String = ""

        Try
            OpenConn()

            '=====================================
            '=       GENERATE BARCODE BARU       =
            '=====================================
            Dim rnd As New Random()
            Dim TextBarcodeBatch As String = ""

            For k As Integer = 1 To 10
                Dim randomChar As Char = Chr(rnd.Next(65, 91)) ' ASCII 65–90 = A–Z
                TextBarcodeBatch &= randomChar
            Next

            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
            Dim fullNewQr As String = "Testing"

            ' Generate gambar barcode
            Dim ImgBarcode As Image = Generate_QR_QC(fullNewQr)
            Barcode.Image = ImgBarcode   ' jika Anda tetap ingin menampilkan di PictureBox

            '=====================================
            '  CONVERT IMAGE TO BYTE ARRAY (TANPA SAVE FILE)
            '=====================================
            Dim ms As New MemoryStream()
            ImgBarcode.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim rawData3 As Byte() = ms.ToArray()
            ms.Close()

            '=====================================
            '  MASUKKAN BYTE ARRAY KE SQL
            '=====================================
            Cmd.Parameters.Add("@newBarcodeBatch" & kode_unik_print, SqlDbType.Image).Value = rawData3

            'asdasd






            'kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
            'Dim fullNewQr As String = "Testing"

            'Barcode.Image = Generate_QR_QC(fullNewQr)

            'Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeValidasiRMQC" & kode_unik_print & ".jpg")

            ''If Not (System.IO.File.Exists(FileToSaveAs1)) Then
            'Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
            ''End If

            'fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
            'FileSize1 = fs1.Length
            'rawData1 = New Byte(FileSize1) {}
            'fs1.Read(rawData1, 0, FileSize1)
            'fs1.Close()
            'Cmd.Parameters.Add("@newBarcodeBatch" & kode_unik_print, SqlDbType.Image).Value = rawData1




            SQL = "INSERT INTO N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak "
            SQL = SQL & "(Kode_Perusahaan, Barcode, Barcode_PSS, Kode_Bahan, NamaBahan, NamaBarang, Tgl_Input, Jam_Input, Batch_Number, Jumlah_Input, Satuan_Input, "
            SQL = SQL & "No, Dari, QrUtuh, Qr, Tanggal_Cetak, Kode_Unik_Print, No_Split, Batch) "
            SQL = SQL & "VALUES('" & KodePerusahaan & "', @newBarcodeBatch" & kode_unik_print & ", @newBarcodeBatch" & kode_unik_print & ",'Testing', 'Testing', "
            SQL = SQL & "'Testing', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'Testing', "
            SQL = SQL & "12345, 'Testing', 2, 0, "
            SQL = SQL & "'" & fullNewQr & "', 'Testing', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & kode_unik_print & "', "
            SQL = SQL & "'Testing', 'Testing'); "
            ExecuteTrans(SQL)


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '=================================
            '=     CETAK FAKTUR TF STOCK     =
            '=================================
            Dim CrDoc As New Object
            Dim kertas As String = ""

            '=================================
            '=     CETAK FAKTUR BARCODE     =
            '=================================
            SQL = "select Kode_Perusahaan from N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Unik_Print='" & kode_unik_print & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New N_EMI_CR_Transaksi_Request_Material_QC_Barcode

                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = "{N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Unik_Print} = '" & kode_unik_print & "' "
                        CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
                        .Text = "Faktur Premix Label"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With

                    '===================================================================================================================================
                    '===================================================================================================================================
                    '===================================================================================================================================

                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.RecordSelectionFormula = "{N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Unik_Print} = '" & kode_unik_print & "' "

                    'CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC

                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterBarcodeQC

                    'Dim rawKind As Integer
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcode Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        Exit For
                    '    End If
                    'Next

                    'If rawKind = Nothing Or rawKind = 0 Then
                    '    CloseConn()
                    '    MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Kertas Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If

                    'CrDoc.PrintToPrinter(1, False, 1, 2500)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Function Generate_QR_QC(ByVal isi As String)

        Dim options As New ZXing.QrCode.QrCodeEncodingOptions()

        options.DisableECI = True
        options.CharacterSet = "UTF-8"
        options.Width = 80
        options.Height = 80
        options.Margin = 0

        Dim qr As New ZXing.BarcodeWriter()
        qr.Format = ZXing.BarcodeFormat.QR_CODE
        qr.Options = options

        Dim result As New Bitmap(qr.Write(isi))
        Return result
    End Function

End Class