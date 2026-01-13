Imports System.IO

Public Class N_EMI_Transaksi_Cetak_Barcode_Stock_Per_Gudang

    Dim arrSo As New ArrayList
    Dim switchAutoComplete As Boolean = False

    Dim kode_unik_print As String

    Dim Random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Private Sub N_EMI_Transaksi_Cetak_Barcode_Stock_Per_Gudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            OpenConn()

            CmbLokasi.Items.Clear() : arrSo.Clear()
            SQL = $"
                select kode_Stock_owner, keterangan from stock_owner_gudang
                where kode_perusahaan = '{KodePerusahaan}'
                order by kode_stock_owner
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbLokasi.Items.Add(Dr("keterangan")) : arrSo.Add(Dr("kode_Stock_owner"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 350, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Kode Stock Owner", 120, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Jumlah", 80, HorizontalAlignment.Right) '3
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Left) '4
        Lv_Data.Columns.Add("Barcode", 150, HorizontalAlignment.Left) '5
        Lv_Data.View = View.Details

        Kosong()

    End Sub

    Private Sub Kosong()

        CmbLokasi.SelectedIndex = -1

        switchAutoComplete = True
        Txt_KdBarang.Text = String.Empty
        Txt_NmBarang.Text = String.Empty
        switchAutoComplete = False

        Lv_Data.Items.Clear()

    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1000, 130)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(122, 130)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()


            Lv_Barang.Items.Clear()
            Dim Lv As ListViewItem
            SQL = $"
                select Distinct a.Kode_Barang, a.Nama
                from Barang a
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Kode_Barang like '%{Txt_KdBarang.Text.Trim}%'
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
        If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()


            SQL = $"
                    select Distinct a.Kode_Barang, a.Nama
                    from Barang a
                    where a.Kode_Perusahaan = '{KodePerusahaan}'
                    and a.Kode_Barang = '{Txt_KdBarang.Text.Trim}'
                "
            Using Dr = Open(SQL)
                If Dr.Read Then
                    Txt_KdBarang.Text = Dr("Kode_Barang")
                    Txt_NmBarang.Text = Dr("Nama")
                    Btn_Set.Focus()
                Else
                    MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_KdBarang.Text = ""
                    Txt_NmBarang.Text = ""
                    Txt_KdBarang.Focus()
                End If

                Lv_Barang.Visible = False
                Lv_Barang.Location = New Point(1000, 130)
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1000, 130)

        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1000, 130)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(122, 130)
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            Dim Lv As ListViewItem
            SQL = $"
                    select Distinct a.Kode_Barang, a.Nama
                    from Barang a
                    where a.Kode_Perusahaan = '{KodePerusahaan}'
                    and a.Nama LIKE '%{Txt_NmBarang.Text.Trim}%'
                "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1000, 130)


        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmKdBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmKdBarang

        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(1000, 130)

    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    Private Sub Btn_Set_Click(sender As Object, e As EventArgs) Handles Btn_Set.Click

        If CmbLokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbLokasi.DroppedDown = True
            CmbLokasi.Focus()

        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang Harus Diisi Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus()
        End If

        Try
            OpenConn()

            Lv_Data.Items.Clear()
            Dim Lv As ListViewItem
            SQL = $"
                select b.Kode_Stock_Owner, b.Kode_Barang, a.Nama as Nama_Barang, a.Satuan, Round(b.jumlah, 4) as jumlah, (b.Qr_Code+'-'+b.Kode_Unik_Berjalan) as Barcode, Flag_Cetak_Barcode
                from barang a
	                inner join barang_sn b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Kode_Stock_Owner = '{arrSo(CmbLokasi.SelectedIndex)}'
                and a.Kode_Barang = '{Txt_KdBarang.Text.Trim}'
                and Round(b.jumlah, 4) <> 0
                order by Round(b.jumlah, 4) ASC
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Data.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("jumlah"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Barcode"))

                    If General_Class.CekNULL(Dr("Flag_Cetak_Barcode")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
                    Else
                        Lv.BackColor = Color.White
                    End If

                Loop
            End Using




            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Clear_Click(sender As Object, e As EventArgs) Handles Btn_Clear.Click
        Kosong()
    End Sub

    Private Sub Btn_Cetak_Click(sender As Object, e As EventArgs) Handles Btn_Cetak.Click

        Dim arrSelectedBarcode As New ArrayList

        For i As Integer = 0 To Lv_Data.Items.Count - 1
            Dim lv As ListViewItem = Lv_Data.Items(i)
            If lv.Checked Then
                arrSelectedBarcode.Add(Lv_Data.Items(i).SubItems(5).Text)
            End If
        Next

        get_jam()

        Try
            OpenConn()

            Dim Barcodes As String = "'" & String.Join("', '", arrSelectedBarcode.ToArray()) & "'"

            SQL = $"
                select a.Kode_Perusahaan, a.Kode_Barang, a.Nama as Nama_Barang, (b.Qr_Code+'-'+b.Kode_Unik_Berjalan) as Qr_Utuh, b.Qr_Code, b.Tgl_Expired, b.Batch_Number, b.Tgl_Masuk, a.Metode_Pengeluaran_Stok, b.Serial_Number,
                    a.kode_stock_owner
                from barang a
	                inner join barang_sn b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Kode_Stock_Owner = '{arrSo(CmbLokasi.SelectedIndex)}'
                and a.Kode_Barang = '{Txt_KdBarang.Text.Trim}'
                and (Qr_Code+'-'+Kode_Unik_Berjalan) in ({Barcodes})
                and Round(b.jumlah, 4) <> 0
                order by Round(b.jumlah, 4) ASC
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Barcode.Image = Nothing


                            '=====================================
                            '=       GENERATE BARCODE BARU       =
                            '=====================================
                            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
                            Dim fullNewQr As String = .Rows(i).Item("Qr_Utuh")

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
                            Cmd.Parameters.Add($"@newBarcode{i}", SqlDbType.Image).Value = rawData1


                            SQL = "Delete from Cetak_TransferStock where Flag_Cetak_Gudang = 'Y'"
                            ExecuteTrans(SQL)

                            SQL = "insert into Cetak_TransferStock (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print,tanggal_masuk,metode_pengeluaran_stok, Flag_Cetak_Gudang) values "
                            SQL = SQL & "('" & KodePerusahaan & "', '" & .Rows(i).Item("Kode_Barang") & "', @newBarcode" & i & ", '" & .Rows(i).Item("Nama_Barang") & "', '" & fullNewQr & "', '" & .Rows(i).Item("Qr_Code") & "', "
                            SQL = SQL & "'" & Format(.Rows(i).Item("Tgl_Expired"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("Batch_Number").ToString.Trim & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "' , "
                            SQL = SQL & "'" & Format(.Rows(i).Item("Tgl_Masuk"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("Metode_Pengeluaran_Stok") & "', 'Y') "
                            ExecuteTrans(SQL)


                            '=================================
                            '=     CETAK FAKTUR TF STOCK     =
                            '=================================
                            Dim CrDoc As New Object
                            '=========================
                            '=     CETAK BARCODE     =
                            '=========================
                            Dim kertasBarcode As String = ""
                            SQL = "select Kode_Perusahaan from Cetak_TransferStock where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & kode_unik_print & "' and Flag_Cetak_Gudang = 'Y'"
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                    CrDoc = New NewBarcodeTransferStock
                                    kertasBarcode = "BarcodeFG"


                                    Dim Batchh As String = .Rows(i).Item("Batch_Number").ToString.Trim
                                    With A_Place_For_Printing2
                                        CrDoc.SetDataSource(Ds2)
                                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                                        CrDoc.PrintOptions.PrinterName = ""
                                        CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unik_print & "' and {Cetak_TransferStock.batch} = '" & Batchh & "' "
                                        CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Stock"
                                        .Text = "New Barcode Transfer Stock"
                                        .CrystalReportViewer1.ReportSource = CrDoc
                                        .Refresh()
                                        .Show()
                                    End With

                                    '=================================================================================================================================================================

                                    'CrDoc.SetDataSource(Ds2)
                                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                                    'CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unik_print & "' and {Cetak_TransferStock.batch} = '" & .Rows(i).Item("Batch_Number").ToString.Trim & "'"

                                    'CrDoc.PrintOptions.PrinterName = PrinterBarcode

                                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                                    'doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                                    'Dim rawKind As Integer
                                    'Dim isPaperFound As Boolean = False
                                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                                    'For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                    '    If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcode Then
                                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                    '        CrDoc.PrintOptions.PaperSize = rawKind
                                    '        isPaperFound = True
                                    '        Exit For
                                    '    End If
                                    'Next

                                    'If Not isPaperFound Then
                                    '    MessageBox.Show("Kertas Tidak DiTemukan, Kertas di set ke default", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                                    'End If

                                    'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                                    'CrDoc.PrintToPrinter(1, False, 1, 99)


                                End If
                            End Using

                            SQL = $"
                             update Barang_SN set Flag_Cetak_Barcode = 'Y' where kode_perusahaan = '{KodePerusahaan}' 
                             and Kode_Stock_Owner = '{ .Rows(i).Item("kode_stock_owner")}' and Kode_Barang = '{ .Rows(i).Item("Kode_Barang")}' and Serial_Number = '{ .Rows(i).Item("Serial_Number")}'
                            "
                            ExecuteTrans(SQL)


                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show("Berhasil Cetak Barcode", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Kosong()





    End Sub
End Class