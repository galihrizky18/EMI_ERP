Imports System.IO

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
                Dim randomChar As Char = Chr(rnd.Next(65, 91))
                TextBarcodeBatch &= randomChar
            Next

            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
            Dim fullNewQr As String = "Testing Tes"

            '=====================================================
            '  CONVERT IMAGE TO BYTE ARRAY (TANPA SAVE FILE)     =
            '=====================================================
            Cmd.Parameters.Clear()
            Using ImgBarcode1 As Image = Generate_QR_QC(fullNewQr)
                Using ms1 As New MemoryStream()
                    ImgBarcode1.Save(ms1, Imaging.ImageFormat.Jpeg)
                    Dim rawData1 As Byte() = ms1.ToArray()

                    Dim param1 As String = "@newBarcodeBatch" & kode_unik_print
                    Cmd.Parameters.Add(param1, SqlDbType.Image).Value = rawData1
                End Using
            End Using

            Using ImgBarcode2 As Image = Generate_QR_QC(fullNewQr & "_PSS")
                Using ms2 As New MemoryStream()
                    ImgBarcode2.Save(ms2, Imaging.ImageFormat.Jpeg)
                    Dim rawData2 As Byte() = ms2.ToArray()

                    Dim param2 As String = "@newBarcodeBatchPSS" & kode_unik_print
                    Cmd.Parameters.Add(param2, SqlDbType.Image).Value = rawData2
                End Using
            End Using

            Dim barcode1 As String = "@newBarcodeBatch" & kode_unik_print
            Dim barcode2 As String = "@newBarcodeBatchPSS" & kode_unik_print

            SQL = "INSERT INTO N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak "
            SQL = SQL & "(Kode_Perusahaan, Barcode, Barcode_PSS, Kode_Bahan, NamaBahan, NamaBarang, Tgl_Input, Jam_Input, Batch_Number, Jumlah_Input, Satuan_Input, "
            SQL = SQL & "No, Dari, QrUtuh, Qr, Tanggal_Cetak, Kode_Unik_Print, No_Split, Batch) "
            SQL = SQL & "VALUES('" & KodePerusahaan & "', " & barcode1 & ", " & barcode2 & ",'Testing', 'Testing', "
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