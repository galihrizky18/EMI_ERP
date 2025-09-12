Imports System.Drawing.Printing
Imports System.IO

Public Class TESTING_PRINT


    Private random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Private random2 As New Random()
    Private imageBytes2 As Byte = Nothing
    Private FileSize2 As UInt32
    Private rawData2() As Byte
    Private fs2 As FileStream



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim CrDoc, CrDoc2 As New Object
        Dim kertas As String = ""
        Dim kertasBarcodeBesar As String = "BarcodeFG"
        Dim kertasBarcodeKecil As String = "BarcodeQC"


        CrDoc = New TESPRINTQC
        'CrDoc.SetDataSource(Ds)
        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        'CrDoc.RecordSelectionFormula = ""


        CrDoc.PrintOptions.PrinterName = PrinterBarcode

        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        doctoprint.PrinterSettings.PrinterName = PrinterBarcode
        Dim rawKind As Integer = -1 ' Default jika kertas tidak ditemukan

        ' Loop mencari ukuran kertas yang cocok
        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertasBarcodeKecil Then
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
            MessageBox.Show("Ukuran kertas tidak ditemukan, menggunakan default.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        CrDoc.PrintToPrinter(1, False, 1, 2500)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            Dim SF As String = ""

            SQL = "select kode_perusahaan from N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = 'RQC0725-00002' "
            SQL = SQL & "and urut_detail = 3 "

            SF = "{N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed.No_Faktur} = 'RQC0725-00002'"
            SF = SF & "and {N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed.urut_detail} = 3"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New N_EMI_CR_Transaksi_Request_Material_QC_Premix_Label
                    kertas = "Faktur"
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = SF
                        CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
                        .Text = "Faktur Premix Label"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim CrDoc, CrDoc2 As New Object
        Dim kertas As String = "BarcodeAsset"


        CrDoc = New BarcodeAsset
        'CrDoc.SetDataSource(Ds)
        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        'CrDoc.RecordSelectionFormula = ""

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
            MessageBox.Show("Ukuran kertas tidak ditemukan, menggunakan default.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        CrDoc.PrintToPrinter(1, False, 1, 2500)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            OpenConn()

            Dim CrDoc, CrDoc2 As New Object
            Dim kertas As String = "BarcodeAsset"

            SQL = "select Kode_Perusahaan from N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Unik_Print='071206560000250'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New N_EMI_CR_Transaksi_Request_Material_QC_Barcode

                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.RecordSelectionFormula = "{N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Unik_Print} = '071206560000250' "

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
                    '    .Text = "Faktur Premix Label"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    'CrDoc.PrintOptions.PrinterName = PrinterBarcode

                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterBarcode

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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim CrDoc, CrDoc2 As New Object
        Dim kertas As String = ""
        Dim kertasBarcodeBesar As String = "BarcodeFG"
        Dim kertasBarcodeKecil As String = "BarcodeQC"


        CrDoc = New TESPRINTQC
        'CrDoc.SetDataSource(Ds)
        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        'CrDoc.RecordSelectionFormula = ""


        CrDoc.PrintOptions.PrinterName = PrinterBarcode

        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        doctoprint.PrinterSettings.PrinterName = PrinterBarcode
        Dim rawKind As Integer = -1 ' Default jika kertas tidak ditemukan

        ' Loop mencari ukuran kertas yang cocok
        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertasBarcodeBesar Then
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
            MessageBox.Show("Ukuran kertas tidak ditemukan, menggunakan default.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        CrDoc.PrintToPrinter(1, False, 1, 2500)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Try
            OpenConn()
            Dim CrDoc As New Object

            Dim KertasBesar As String = "BarcodeFG"
            Dim KertasKecil As String = "BarcodeQC"

            Dim kode_unik_print As String = "010100000005893"

            SQL = "select Kode_Perusahaan from Cetak_Finish_Good where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Unik_Print = '" & kode_unik_print & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    '==========================
                    '=     BARCODEE BESAR     =
                    '==========================
                    Dim printerDitemukan As Boolean = False
                    For Each printer As String In PrinterSettings.InstalledPrinters
                        If printer.ToLower() = PrinterBarcode.ToLower() Then
                            printerDitemukan = True
                            Exit For
                        End If
                    Next

                    If printerDitemukan Then

                        CrDoc = New NewBarcodeFinishGood

                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                        '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                        '    .Text = "New Barcode Finish Good"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        '=====================================================

                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                        'CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        'doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        'Dim rawKind As Integer
                        'Dim foundPaper As Boolean = False
                        'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        '        CrDoc.PrintOptions.PaperSize = rawKind
                        '        foundPaper = True
                        '        Exit For
                        '    End If
                        'Next

                        'If Not foundPaper Then
                        '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        '    MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        'End If

                        'CrDoc.PrintToPrinter(1, False, 1, 2500)

                    Else
                        MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                    printerDitemukan = False


                    '==========================
                    '=     BARCODEE KECIL     =
                    '==========================
                    'For Each printer As String In PrinterSettings.InstalledPrinters
                    '    If printer.ToLower() = PrinterBarcodeQC.ToLower() Then
                    '        printerDitemukan = True
                    '        Exit For
                    '    End If
                    'Next

                    'If printerDitemukan Then
                    'CrDoc = New NewBarcodeFinishGoodKecil

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                    '    .Text = "New Barcode Finish Good"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '    '=======================================================

                    '    Dim doctoprint2 As New System.Drawing.Printing.PrintDocument()
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                    '    CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC

                    '    doctoprint2.PrinterSettings.PrinterName = PrinterBarcodeQC

                    '    Dim rawKind2 As Integer
                    '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    '    For i = 0 To doctoprint2.PrinterSettings.PaperSizes.Count - 1
                    '        If doctoprint2.PrinterSettings.PaperSizes(i).PaperName = KertasKecil Then
                    '            rawKind2 = CInt(doctoprint2.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint2.PrinterSettings.PaperSizes(i)))
                    '            CrDoc.PrintOptions.PaperSize = rawKind2
                    '            Exit For
                    '        End If
                    '    Next

                    '    CrDoc.PrintToPrinter(1, False, 1, 2500)
                    'End If

                    printerDitemukan = False

                Else
                    MessageBox.Show("Printer QC Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        get_jam()
        Try
            OpenConn()
            Dim CrDoc As New Object

            Dim KertasBesar As String = "BarcodeFG"
            Dim KertasKecil As String = "BarcodeQC"

            Dim kode_unik_print As String = "010100000005893"

            Dim fullNewQrScrap As String = "Testing :)"


            SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_1 "
            ExecuteTrans(SQL)

            Barcode.Image = Generate_QR_NoPadding(fullNewQrScrap)
            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")
            Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)

            fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
            FileSize1 = fs1.Length
            rawData1 = New Byte(FileSize1) {}
            fs1.Read(rawData1, 0, FileSize1)
            fs1.Close()
            Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1


            SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_1 (kode_perusahaan, no_split, Kode_barang, Barcode, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, Proses, Tahap, Jumlah, Satuan, Troli, Nomor, id_routing, routing, Kode_unik_print) "
            SQL = SQL & "values ('001', 'PRD0525-00001-2', 'BRG08240005', @newBarcode, 'LIFE CAT 85GR TUNA KITTEN', 'Testing', 'Testing', "
            SQL = SQL & "'2025-07-05', ''JAM, '1', '1', '10', 'Pcs', '2', '1', '14', 'CHUNK IN CAN', '" & kode_unik_print & "')"
            ExecuteTrans(SQL)


            SQL = "select Kode_Perusahaan from N_EMI_Barcode_Label_Barcode_GR_1 where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Unik_Print = '" & kode_unik_print & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    '==========================
                    '=     BARCODEE BESAR     =
                    '==========================
                    Dim printerDitemukan As Boolean = False
                    For Each printer As String In PrinterSettings.InstalledPrinters
                        If printer.ToLower() = PrinterBarcode.ToLower() Then
                            printerDitemukan = True
                            Exit For
                        End If
                    Next

                    If printerDitemukan Then

                        CrDoc = New N_EMI_Label_Barcode_GR_1

                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_1.kode_perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Unik_Print} = '" & kode_unik_print & "' "
                            CrDoc.SummaryInfo.ReportTitle = "Testting"
                            .Text = "Testing"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With

                        '=====================================================

                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                        'CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        'doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        'Dim rawKind As Integer
                        'Dim foundPaper As Boolean = False
                        'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        '        CrDoc.PrintOptions.PaperSize = rawKind
                        '        foundPaper = True
                        '        Exit For
                        '    End If
                        'Next

                        'If Not foundPaper Then
                        '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        '    MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        'End If

                        'CrDoc.PrintToPrinter(1, False, 1, 2500)

                    Else
                        MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                    printerDitemukan = False



                Else
                    MessageBox.Show("Printer QC Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        get_jam()
        Try
            OpenConn()
            Dim CrDoc As New Object


            Dim rnd As New Random()
            Dim kode_unik_print As String = rnd.Next(100000, 999999).ToString()

            Dim fullNewQrScrap As String = "Testing2 :)"

            Dim KertasBesar As String = "BarcodeFG"


            SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_2 "
            ExecuteTrans(SQL)

            Barcode.Image = Nothing : Barcode.Refresh()
            Barcode.Image = Generate_QR_NoPadding(fullNewQrScrap)
            Dim FileToSaveAs2 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")
            Barcode.Image.Save(FileToSaveAs2, System.Drawing.Imaging.ImageFormat.Jpeg)

            fs2 = New FileStream(FileToSaveAs2, FileMode.Open, FileAccess.Read)
            FileSize2 = fs2.Length
            rawData2 = New Byte(FileSize2) {}
            fs2.Read(rawData2, 0, FileSize2)
            fs2.Close()
            Cmd.Parameters.Add("@newBarcode2", SqlDbType.Image).Value = rawData2



            SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_2 (kode_perusahaan, no_split, kode_barang, barcode, nama_barang, batch_number, qrutuh, qr, tgl_produksi, tgl_expired, jumlah, satuan, jenis, kode_unik_print) "
            SQL = SQL & "values ('001', 'PRD0525-00001-1', 'BRG08240005', @newBarcode2, 'LIFE CAT 85GR TUNA KITTEN', 'Testing', 'Testing', 'Testing', "
            SQL = SQL & "'2025-07-05', '2077-07-05', '315', 'Pcs', 'GOOD STOCK', '" & kode_unik_print & "') "
            ExecuteTrans(SQL)

            SQL = "select Kode_Perusahaan from N_EMI_Barcode_Label_Barcode_GR_2 where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Unik_Print = '" & kode_unik_print & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    '==========================
                    '=     BARCODEE BESAR     =
                    '==========================
                    Dim printerDitemukan As Boolean = False
                    'For Each printer As String In PrinterSettings.InstalledPrinters
                    '    If printer.ToLower() = PrinterBarcode.ToLower() Then
                    '        printerDitemukan = True
                    '        Exit For
                    '    End If
                    'Next

                    printerDitemukan = True

                    If printerDitemukan Then

                        CrDoc = New N_EMI_Label_Barcode_GR_2

                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.kode_perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & kode_unik_print & "' "
                        '    CrDoc.SummaryInfo.ReportTitle = "Testting"
                        '    .Text = "Testing"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        '=====================================================

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_2.kode_perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_2.Kode_Unik_Print} = '" & kode_unik_print & "' "
                        CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        Dim rawKind As Integer
                        Dim foundPaper As Boolean = False
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                foundPaper = True
                                Exit For
                            End If
                        Next

                        If Not foundPaper Then
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        End If

                        CrDoc.PrintToPrinter(1, False, 1, 2500)

                    Else
                        MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                    printerDitemukan = False



                Else
                    MessageBox.Show("Printer QC Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Function Generate_QR_NoPadding(ByVal isi As String)

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

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            OpenConn()




            SQL = "select Keterangan from N_EMI_Transaksi_Bypass_Military_Sampling"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("Keterangan")
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class