Imports System.CodeDom.Compiler
Imports System.IO
Imports System.Reflection.Emit
Imports System.Text
Imports ZXing.QrCode

Public Class Emi_Production_Barcode

    Private random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Dim Lv_NoSplit, Lv_NoPO, Lv_Lokasi, Lv_Tanggal, Lv_Jam, Lv_KdSo, Lv_KdBarang, Lv_NamaBarang, Lv_Jmlh, Lv_Satuan, Lv_Catatan, Lv_Routing As String
    Dim Lv_TglSelesaiProduksi, Lv_TglExpired, Lv_PrefixCode As String

    Dim Tahun_MulaiProduksi As String

    Dim item_NoSplit As Integer = 0
    Dim item_NoPO As Integer = 1
    Dim item_Lokasi As Integer = 2
    Dim item_Tanggal As Integer = 3
    Dim item_Jam As Integer = 4
    Dim item_KdSo As Integer = 5
    Dim item_KdBarang As Integer = 6
    Dim item_NamaBarang As Integer = 7
    Dim item_Jmlh As Integer = 8
    Dim item_Satuan As Integer = 9
    Dim item_Catatan As Integer = 10
    Dim item_Routing As Integer = 11
    Dim item_TglSelesaiProduksi As Integer = 12
    Dim item_TglExpired As Integer = 13
    Dim item_PrefixCode As Integer = 14

    Private Sub Emi_Barcode_FinishGood_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Private Sub kosong()

        Txt_NoSplit.Text = String.Empty
        Txt_KdBarang.Text = String.Empty
        Txt_NamaBarang.Text = String.Empty
        Txt_Jumlah.Text = String.Empty
        Txt_HasilProduksi.Text = String.Empty

        Cmb_Satuan.Items.Clear()
        Cmb_Satuan.Items.Add("KG")
        Cmb_Satuan.Items.Add("Gram")
        Cmb_Satuan.SelectedIndex = 0

        Chk_FullPallet.Checked = False

        Initial_Listview()
        Lv_Data.Items.Clear()

        Load_Data()

        'GET TAHUN MULAI PRODUKSI
        Try
            OpenConn()

            SQL = "select Tahun_Mulai_Produksi from Init"
            Using dr = OpenTrans(SQL)

                Do While dr.Read
                    Tahun_MulaiProduksi = If(General_Class.CekNULL(dr("Tahun_Mulai_Produksi")) = "", "0", dr("Tahun_Mulai_Produksi"))
                Loop

            End Using

            SQL = "select Kode_Stock_Owner from Stock_Owner where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("Kode_Stock_Owner"))
                Loop
                Cmb_Lokasi.SelectedIndex = 0
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Initial_Listview()

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Split PO", 170, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("No PO", 170, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Lokasi", 160, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Stock Owner", 170, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang", 140, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Catatan", 250, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Routing", 120, HorizontalAlignment.Center)

        'HIDE
        Lv_Data.Columns.Add("Tgl_SelesaiProksi", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tgl_Expired", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("PrefixCode", 0, HorizontalAlignment.Left)

        Lv_Data.View = View.Details

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Load_Data()

        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select a.No_Transaksi, a.No_PO, a.Lokasi, a.Tanggal, a.Jam, a.UserID, a.Kode_Stock_Owner, "
            SQL = SQL & "a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.Catatan, c.Id_Routing, d.Keterangan as ket_routing, "
            SQL = SQL & "a.Tgl_Selesai_Produksi, d.prefix_code, a.tgl_expired, a.Jam_Selesai_Produksi, a.UserID "
            SQL = SQL & "from Emi_Split_Production_Order a, Barang b, EMI_Order_Produksi c, EMI_Master_Routing d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.No_PO = c.No_Faktur "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Routing = d.Id_Routing and c.Status is null "
            SQL = SQL & "and c.Flag_Release = 'Y' and a.Flag_Produksi = 'Y' and a.Flag_Selesai_Produksi = 'Y' "
            SQL = SQL & "and a.Flag_Hasil_Produksi is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("No_PO"))
                    Lv.SubItems.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Dr("Jumlah"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Catatan"))
                    Lv.SubItems.Add(Dr("ket_routing"))

                    'HIDE
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Tgl_Selesai_Produksi")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("tgl_expired")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("prefix_code")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)
        Lv_NoSplit = Lv_Data.Items(index).SubItems(item_NoSplit).Text
        Lv_NoPO = Lv_Data.Items(index).SubItems(item_NoPO).Text
        Lv_Lokasi = Lv_Data.Items(index).SubItems(item_Lokasi).Text
        Lv_Tanggal = Lv_Data.Items(index).SubItems(item_Tanggal).Text
        Lv_Jam = Lv_Data.Items(index).SubItems(item_Jam).Text
        Lv_KdSo = Lv_Data.Items(index).SubItems(item_KdSo).Text
        Lv_KdBarang = Lv_Data.Items(index).SubItems(item_KdBarang).Text
        Lv_NamaBarang = Lv_Data.Items(index).SubItems(item_NamaBarang).Text
        Lv_Jmlh = Lv_Data.Items(index).SubItems(item_Jmlh).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_Catatan = Lv_Data.Items(index).SubItems(item_Catatan).Text
        Lv_Routing = Lv_Data.Items(index).SubItems(item_Routing).Text
        Lv_TglSelesaiProduksi = Lv_Data.Items(index).SubItems(item_TglSelesaiProduksi).Text
        Lv_TglExpired = Lv_Data.Items(index).SubItems(item_TglExpired).Text
        Lv_PrefixCode = Lv_Data.Items(index).SubItems(item_PrefixCode).Text
    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Then Exit Sub

        Get_Data_Lv(Lv_Data.FocusedItem.Index)

        Txt_NoSplit.Text = Lv_NoSplit
        Txt_KdBarang.Text = Lv_KdBarang
        Txt_NamaBarang.Text = Lv_NamaBarang
        Txt_HasilProduksi.Text = Lv_Jmlh

        Chk_FullPallet_CheckedChanged(sender, e)

    End Sub

    Private Sub Chk_FullPallet_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_FullPallet.CheckedChanged

        If Lv_Data.Items.Count = 0 Or Txt_NoSplit.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "select kode_barang, total, Satuan_Jumlah from barang_detail_susunan where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and flag_default = 'Y' and kode_barang ='" & Txt_KdBarang.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Cmb_Satuan.Items.Clear()

                    If Chk_FullPallet.Checked = True Then
                        Txt_Jumlah.Enabled = False
                        Txt_Jumlah.Text = String.Empty
                        Txt_Jumlah.Text = Dr("total")
                    Else
                        Txt_Jumlah.Enabled = True
                        Txt_Jumlah.Text = String.Empty
                    End If

                    Cmb_Satuan.Items.Add(Dr("Satuan_Jumlah"))
                    Cmb_Satuan.SelectedItem = (Dr("Satuan_Jumlah"))

                    'If Val(Txt_HasilProduksi.Text) > Val(Dr("total")) Then

                    'ElseIf Val(Txt_HasilProduksi.Text) < Val(Dr("total")) Then
                    'Txt_Jumlah.Text = Txt_HasilProduksi.Text
                    'End If
                Else
                    Dim tanya As String = MessageBox.Show("Tidak Ada Data Barang di Pallet, Tetap Input?", "Generate Barcode", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If tanya = vbYes Then
                        Txt_Jumlah.Enabled = True
                        Txt_Jumlah.Text = 0
                        Cmb_Satuan.SelectedIndex = 0
                    Else
                        Txt_Jumlah.Enabled = False
                        Txt_Jumlah.Text = String.Empty
                        Cmb_Satuan.SelectedIndex = 0

                    End If

                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Lv_Data.Items.Count = 0 Or Txt_NoSplit.Text.Trim.Length = 0 Or Txt_Jumlah.Text.Trim.Length = 0 Or Not IsNumeric(Txt_Jumlah.Text) Then Exit Sub
        If String.IsNullOrEmpty(Lv_TglSelesaiProduksi) Or String.IsNullOrEmpty(Lv_PrefixCode) Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            'Dim newBatch As String = Generate_Batch_Bahan(Lv_TglSelesaiProduksi, Lv_PrefixCode, Lv_TglExpired)
            'Dim newQrCode As String = Generate_QR(Txt_KdBarang.Text)

            Dim newBatch As String = ""
            Dim newQrCode As String = ""
            Dim Kode_Berjalan As String = Generate_Random_Kode(10)
            Dim Kode_Asal As String = Generate_Random_Kode(10)

            '=========================
            '=      INSERT DATA      =
            '=========================

            SQL = "insert into Emi_Produksi_Hasil_Perpallet (Kode_Perusahaan, No_Split, Lokasi, Tanggal, Jam, UserID, Kode_Stock_Owner, Kode_Barang, Jumlah, Satuan, Batch_Number, "
            SQL = SQL & "Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoSplit.Text & "', '" & Cmb_Lokasi.SelectedItem & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
            SQL = SQL & "'" & Lv_KdSo & "', '" & Txt_KdBarang.Text & "', '" & Lv_Jmlh & "', '" & Cmb_Satuan.SelectedItem & "', '" & newBatch & "', '" & newQrCode & "', '" & Kode_Berjalan & "', '" & Kode_Asal & "') "
            ExecuteTrans(SQL)

            '==================================
            '=      GENERATE NEW BARCODE      =
            '==================================

            'HAPUS TABEL SEMENTARA
            SQL = "truncate table Cetak_Finish_Good "
            ExecuteTrans(SQL)

            Dim fullNewQr As String = newQrCode & "-" & Kode_Berjalan

            Barcode.Image = Generate_QR(fullNewQr)

            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")
            'If Not (System.IO.File.Exists(FileToSaveAs1)) Then

            'End If

            Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)

            fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
            FileSize1 = fs1.Length
            rawData1 = New Byte(FileSize1) {}
            fs1.Read(rawData1, 0, FileSize1)
            fs1.Close()
            Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1

            'INSERT TABEL CETAK QR
            SQL = "insert into Cetak_Finish_Good (Kode_Perusahaan, Kode_Barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_KdBarang.Text & "', @newBarcode, '" & Txt_NamaBarang.Text & "', "
            SQL = SQL & "'" & fullNewQr & "', '" & newQrCode & "', '" & Format(Date.Parse(Lv_TglExpired), "yyyy-MM-dd") & "', '" & newBatch & "') "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '===========================
        '=      CETAK BARCODE      =
        '===========================

        Try
            OpenConn()
            Dim CrDoc As New Object

            SQL = "select Kode_Perusahaan from Cetak_Finish_Good where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & Txt_KdBarang.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New NewBarcodeFinishGood
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & Txt_KdBarang.Text & "' "
                        CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                        .Text = "New Barcode Finish Good"
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



    Private Sub Txt_Jumlah_Leave(sender As Object, e As EventArgs) Handles Txt_Jumlah.Leave
        If Not IsNumeric(Txt_Jumlah.Text) Then Txt_Jumlah.Text = String.Empty : Exit Sub
    End Sub

End Class