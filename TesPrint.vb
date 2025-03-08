Imports System.IO
Imports System.Text
Imports iTextSharp.text.pdf

Public Class TesPrint
    Dim Random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream



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

            Dim PrinterBarcode As String = "TSC TE210 (LAN)"

            SQL = "select Kode_Perusahaan from Cetak_TransferStock where Kode_Perusahaan='001' and kode_unik_print='021408272904114'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New NewBarcodeTransferStock
                    kertas = "Barcode TSC"

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '001' and {Cetak_TransferStock.kode_unik_print} = '021408272904114' "
                    CrDoc.PrintOptions.PrinterName = PrinterBarcode

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    'SET KERTAS
                    Dim rawKind As Integer
                    Dim kertasDitemukan As Boolean = False
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            kertasDitemukan = True
                            Exit For
                        End If
                    Next

                    If Not kertasDitemukan Then
                        CloseConn()
                        MessageBox.Show("Kertas Tidak diTemukan", "Cetak", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 2500)



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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        get_jam()

        Try
            OpenConn()
            Dim CrDoc As New Object
            Dim kertas As String = ""

            Dim fullNewQr As String = "1111065-0112C1B280225-8FDGXPD94Y-450.5"

            '=====================================
            '=       GENERATE BARCODE BARU       =
            '=====================================
            Dim kode_unik_print As String = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")

            Barcode.Image = Generate_QR(fullNewQr)

            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")
            'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
            Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
            'End If

            fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
            FileSize1 = fs1.Length
            rawData1 = New Byte(FileSize1) {}
            fs1.Read(rawData1, 0, FileSize1)
            fs1.Close()
            Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1


            '===================================
            '=       INSERT BARCODE BARU       =
            '===================================
            Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

            SQL = "delete from Cetak_TransferStock where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            ExecuteTrans(SQL)

            Dim namaBarang As String = "IKAN PATIN UTUH FROZEN IKAN PATIN UTUH FROZEN"
            Dim QrLama As String = "1111065-0112C1B280225"
            Dim expDate As String = "2025-02-28"
            Dim batchLama As String = "0112C1B280225"
            Dim tglMsk As String = "2025-02-12 "
            Dim metodePengeluaranStock As String = "FEFO"
            Dim GetDataKdBrg As String = "1111065"

            SQL = "insert into Cetak_TransferStock (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print,tanggal_masuk,metode_pengeluaran_stok) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & GetDataKdBrg & "', @newBarcode, '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
            SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "' , "
            SQL = SQL & "'" & tglMsk & "', '" & metodePengeluaranStock & "' ) "
            ExecuteTrans(SQL)




            '=========================
            '=     CETAK BARCODE     =
            '=========================
            Dim PrinterBarcode As String = "TSC TE210"
            Dim kodeUnikPrint As String = "021408272904114"

            SQL = "select Kode_Perusahaan from Cetak_TransferStock where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & kode_unik_print & "'"
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
                    CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unik_print & "' "

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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            OpenConn()

            Dim CrDoc, CrDoc2 As New Object
            Dim kertas As String = ""

            Dim PrinterBarcodeKecil As String = "TSC TE210 (LAN)"
            Dim PrinterBarcodeBesar As String = "TSC TE210"

            Dim kertasBarcodeBesar As String = "BarcodeFG"
            Dim kertasBarcodeKecil As String = "BarcodeQC"

            SQL = "select Kode_Perusahaan from Cetak_Finish_Good where Kode_Perusahaan = '001' and Kode_Unik_Print = '030513122502848'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    '==========================
                    '=     BARCODEE BESAR     =
                    '==========================
                    CrDoc = New NewBarcodeFinishGood
                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '030513122502848' "
                    CrDoc.PrintOptions.PrinterName = PrinterBarcodeBesar

                    doctoprint.PrinterSettings.PrinterName = PrinterBarcodeBesar

                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertasBarcodeBesar Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintToPrinter(1, False, 1, 2500)


                    '==========================
                    '=     BARCODEE KECIL     =
                    '==========================
                    CrDoc2 = New NewBarcodeFinishGoodKecil

                    Dim doctoprint2 As New System.Drawing.Printing.PrintDocument()

                    CrDoc2.SetDataSource(Ds)
                    CrDoc2.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc2.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '030513122502848' "
                    CrDoc2.PrintOptions.PrinterName = PrinterBarcodeKecil

                    doctoprint2.PrinterSettings.PrinterName = PrinterBarcodeKecil

                    Dim rawKind2 As Integer
                    CrDoc2.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint2.PrinterSettings.PaperSizes(i).PaperName = kertasBarcodeKecil Then
                            rawKind2 = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint2.PrinterSettings.PaperSizes(i)))
                            CrDoc2.PrintOptions.PaperSize = rawKind2
                            Exit For
                        End If
                    Next

                    CrDoc2.PrintToPrinter(1, False, 1, 2500)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        '=================================
        '=     CETAK FAKTUR TF STOCK     =
        '=================================
        Dim CrDoc, CrDoc2 As New Object
        Dim kertas As String = ""
        Dim kertasBarcodeBesar As String = "BarcodeFG"
        Dim kertasBarcodeKecil As String = "BarcodeQC"

        SQL = "select a.Kode_Perusahaan "
        SQL = SQL & "from Vw_tf_stock_detail a "
        SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and a.No_Faktur = 'TS-RM-02/25-0001' "
        Using Ds = BindingTrans(SQL)
            If Ds.Tables("MyTable").Rows.Count <> 0 Then

                CrDoc = New Rpt_EMI_Faktur_Transfer_Stock_Detail
                kertas = "Faktur"

                'With A_Place_For_Printing2
                '    CrDoc.SetDataSource(Ds)
                '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                '    CrDoc.PrintOptions.PrinterName = ""
                '    CrDoc.RecordSelectionFormula = "{Vw_tf_stock_detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_tf_stock_detail.No_Faktur}='" & txtKodeTransfer.Text & "' "
                '    CrDoc.SummaryInfo.ReportTitle = "TF"
                '    .Text = "TF"
                '    .CrystalReportViewer1.ReportSource = CrDoc
                '    .Refresh()
                '    .Show()
                'End With

                '============================================================================================================================================
                '============================================================================================================================================
                CrDoc.SetDataSource(Ds)
                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                CrDoc.PrintOptions.PrinterName = PrinterNameTS
                CrDoc.RecordSelectionFormula = "{Vw_tf_stock_detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_tf_stock_detail.No_Faktur}='' "
                'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = PrinterNameTS
                doctoprint.DefaultPageSettings.Landscape = True
                Dim rawKind As Integer
                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        CrDoc.PrintOptions.PaperSize = rawKind
                        Exit For
                    End If
                Next

                CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                CrDoc.PrintToPrinter(1, False, 1, 99)

                MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


            End If
        End Using


        '=================================
        '=     CETAK FAKTUR BARCODE     =
        '=================================
        Dim PrinterBarcode As String = "TSC TE210"
        SQL = "select Kode_Perusahaan from Cetak_TransferStock where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='021408272904114'"
        Using Ds = BindingTrans(SQL)
            If Ds.Tables("MyTable").Rows.Count <> 0 Then

#Region "Kode Lama"

                'CrDoc = New NewBarcodeTransferStock
                'CrDoc.SetDataSource(Ds)
                'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                'CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '021408272904114' "

                'CrDoc.PrintOptions.PrinterName = PrinterBarcode

                'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                'doctoprint.PrinterSettings.PrinterName = PrinterBarcode
                'Dim rawKind As Integer
                'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertasBarcodeBesar Then
                '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                '        CrDoc.PrintOptions.PaperSize = rawKind
                '        Exit For
                '    End If
                'Next

                'CrDoc.PrintToPrinter(1, False, 1, 2500)

#End Region

#Region "Kode Baru"

                CrDoc = New NewBarcodeTransferStock
                CrDoc.SetDataSource(Ds)
                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '021408272904114' "

                CrDoc.PrintOptions.PrinterName = PrinterBarcode

                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = PrinterBarcode
                Dim rawKind As Integer = -1 ' Default jika kertas tidak ditemukan

                ' Loop mencari ukuran kertas yang cocok
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                        rawKind = doctoprint.PrinterSettings.PaperSizes(i).RawKind
                        Exit For
                    End If
                Next

                ' Jika kertas ditemukan, gunakan ukurannya
                If rawKind <> -1 Then
                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                Else
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    Debug.Print("Ukuran kertas tidak ditemukan, menggunakan default.")
                End If

                CrDoc.PrintToPrinter(1, False, 1, 2500)

#End Region

            End If
        End Using
    End Sub
End Class