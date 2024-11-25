Imports System.CodeDom.Compiler
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq.Expressions
Imports System.Net.NetworkInformation
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports ZXing
Imports ZXing.Common
Imports ZXing.QrCode

Public Class EMI_Display_Barang_Masuk_Per_Pallet2222

    Dim Arr1, Arr2, Arr3, Arr4, arrAlreadyPrinted As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Private random As New Random()
    Private Is2ndPrint As Boolean = False

    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Private imageBytes2 As Byte = Nothing
    Private FileSize2 As UInt32
    Private rawData2() As Byte
    Private fs2 As FileStream

    Dim LvNoFaktur, LvNoPembLoading, LvIdNametagPallet, LvNoSJ, LvNoPlat, LvNmSupplier, LvTgl, LvJam, LvUserId, LvKodeSO, LvKdBrg, LvNmBrg, LvTglProd, LvTglExp, LvJumlah, LvJmlBags, LvSatuan, LvNilaiPengali, LvNilaiBrg, LvSatuanBrg, LvUrutOto, LvMetodeTimbang As String

    Dim tahunMulaiProduksi As String = ""

    Dim itemNoFaktur As Integer = 0
    Dim itemNoPembLoading As Integer = 1
    Dim itemIdNametagPallet As Integer = 2
    Dim itemNoSJ As Integer = 3
    Dim itemNoPlat As Integer = 4
    Dim itemNmSupplier As Integer = 5
    Dim itemTgl As Integer = 6
    Dim itemJam As Integer = 7
    Dim itemUserId As Integer = 8
    Dim itemKodeSO As Integer = 9
    Dim itemKdBrg As Integer = 10
    Dim itemNmBrg As Integer = 11
    Dim itemTglProd As Integer = 12
    Dim itemTglExp As Integer = 13
    Dim itemJumlah As Integer = 14
    Dim itemJmlBags As Integer = 15
    Dim itemSatuan As Integer = 16
    Dim itemNilaiPengali As Integer = 17
    Dim itemNilaiBrg As Integer = 18
    Dim itemSatuanBrg As Integer = 19
    Dim itemUrutOto As Integer = 20
    Dim itemMetodeTimbang As Integer = 21

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If Lv_BM_PerPallet.Items.Count = 0 Or Lv_BM_PerPallet.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_BM_PerPallet.FocusedItem.Text)
    End Sub

    Public Function Generate_QR_1(ByVal isi As String)
        Dim options As New QrCodeEncodingOptions

        options.DisableECI = True
        options.CharacterSet = "UTF-8"
        'options.Width = 80
        'options.Height = 80

        Dim qr As New ZXing.BarcodeWriter()
        'qr.Options = options
        qr.Options.Width = 80
        qr.Options.Height = 80

        qr.Format = ZXing.BarcodeFormat.QR_CODE

        Dim result As New Bitmap(qr.Write(isi))
        'result.SetResolution(50, 50)

        Return result
    End Function

    Public Function Generate_QR_2(ByVal isi As String)
        Dim options As New QrCodeEncodingOptions

        options.DisableECI = True
        options.CharacterSet = "UTF-8"
        'options.Width = 80
        'options.Height = 80

        Dim qr As New ZXing.BarcodeWriter()
        'qr.Options = options
        qr.Options.Width = 80
        qr.Options.Height = 80

        qr.Format = ZXing.BarcodeFormat.QR_CODE

        Dim result As New Bitmap(qr.Write(isi))
        'result.SetResolution(50, 50)

        Return result
    End Function

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvNoFaktur = Lv_BM_PerPallet.Items(No_Index).Text
        LvNoPembLoading = Lv_BM_PerPallet.Items(No_Index).SubItems(itemNoPembLoading).Text
        LvIdNametagPallet = Lv_BM_PerPallet.Items(No_Index).SubItems(itemIdNametagPallet).Text
        LvNoSJ = Lv_BM_PerPallet.Items(No_Index).SubItems(itemNoSJ).Text
        LvNoPlat = Lv_BM_PerPallet.Items(No_Index).SubItems(itemNoPlat).Text
        LvNmSupplier = Lv_BM_PerPallet.Items(No_Index).SubItems(itemNmSupplier).Text
        LvTgl = Lv_BM_PerPallet.Items(No_Index).SubItems(itemTgl).Text
        LvJam = Lv_BM_PerPallet.Items(No_Index).SubItems(itemJam).Text
        LvUserId = Lv_BM_PerPallet.Items(No_Index).SubItems(itemUserId).Text
        LvKodeSO = Lv_BM_PerPallet.Items(No_Index).SubItems(itemKodeSO).Text
        LvKdBrg = Lv_BM_PerPallet.Items(No_Index).SubItems(itemKdBrg).Text
        LvNmBrg = Lv_BM_PerPallet.Items(No_Index).SubItems(itemNmBrg).Text
        LvTglProd = Lv_BM_PerPallet.Items(No_Index).SubItems(itemTglProd).Text
        LvTglExp = Lv_BM_PerPallet.Items(No_Index).SubItems(itemTglExp).Text
        LvJumlah = Lv_BM_PerPallet.Items(No_Index).SubItems(itemJumlah).Text
        LvJmlBags = Lv_BM_PerPallet.Items(No_Index).SubItems(itemJmlBags).Text
        LvSatuan = Lv_BM_PerPallet.Items(No_Index).SubItems(itemSatuan).Text
        LvNilaiPengali = Lv_BM_PerPallet.Items(No_Index).SubItems(itemNilaiPengali).Text
        LvNilaiBrg = Lv_BM_PerPallet.Items(No_Index).SubItems(itemNilaiBrg).Text
        LvSatuanBrg = Lv_BM_PerPallet.Items(No_Index).SubItems(itemSatuanBrg).Text
        LvUrutOto = Lv_BM_PerPallet.Items(No_Index).SubItems(itemUrutOto).Text
        LvMetodeTimbang = Lv_BM_PerPallet.Items(No_Index).SubItems(itemMetodeTimbang).Text
    End Sub
    Private Sub Display_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Public Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Display_Barang_Masuk")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")

            Lv_BM_PerPallet.Items.Clear()
            Lv_BM_PerPallet.Columns.Clear()
            Lv_BM_PerPallet.Columns.Add(Base_Language.Lang_Global_NoFaktur, 150, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("No Pembelian Loading", 0, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Id Nametag Pallet", 0, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("No SJ", 100, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("No Plat", 100, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Nama Supplier", 200, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("Jam", 80, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("User ID", 80, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Tanggal Produksi", 150, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("Tanggal Expired", 150, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("Jumlah", 100, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Jumlah Bags", 100, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("Nilai Pengali", 100, HorizontalAlignment.Right)
            Lv_BM_PerPallet.Columns.Add("Nilai Barang", 100, HorizontalAlignment.Right)
            Lv_BM_PerPallet.Columns.Add("Satuan Barang", 100, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("UrutOto", 0, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Metode Timbang", 100, HorizontalAlignment.Left)
            Lv_BM_PerPallet.View = View.Details

            Lv_BMPerPalletDetail.Items.Clear()
            Lv_BMPerPalletDetail.Columns.Add("No PO", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Nama Barang", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Tgl Produksi", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Tgl Expired", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Jumlah", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Satuan", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Jumlah Bags", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Nilai Pengali", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Nilai Barang", 150, HorizontalAlignment.Left)
            Lv_BMPerPalletDetail.Columns.Add("Satuan Barang", 150, HorizontalAlignment.Left)
            'Lv_BMPerPalletDetail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
            'Lv_BMPerPalletDetail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 200, HorizontalAlignment.Left)
            'Lv_BMPerPalletDetail.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
            'Lv_BMPerPalletDetail.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center)
            'Lv_BMPerPalletDetail.Columns.Add("Jumlah PO", 110, HorizontalAlignment.Center)
            'Lv_BMPerPalletDetail.Columns.Add("Sisa", 110, HorizontalAlignment.Center)
            'Lv_BMPerPalletDetail.Columns.Add("%Complete", 110, HorizontalAlignment.Center)
            'Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Harga, 110, HorizontalAlignment.Right)
            'Lv_PRDetail.Columns.Add("Jumlah Masuk", 110, HorizontalAlignment.Right)
            'Lv_PRDetail.Columns.Add("Sisa", 110, HorizontalAlignment.Right)
            'Lv_PRDetail.Columns.Add("%Complete", 110, HorizontalAlignment.Right)
            'Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Total, 140, HorizontalAlignment.Right)
            'ListView2.Columns.Add(Base_Language.Lang_Pmb_Barang_Masuk_Tanggal_produksi, 120, HorizontalAlignment.Center)
            'ListView2.Columns.Add(Base_Language.Lang_Pmb_Barang_Masuk_Tanggal_Expire, 120, HorizontalAlignment.Center)
            Lv_BMPerPalletDetail.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        Data_BM_PerPallet()
    End Sub

    Private Sub Data_BM_PerPallet()
        Try
            OpenConn()

            arrAlreadyPrinted.Clear()

            SQL = "select a.No_Faktur, a.No_Pembelian_Loading, a.Id_Nametag_Pallet, a.No_SJ, a.No_Plat, c.Nama as nama_supplier, "
            SQL = SQL & "a.tanggal, a.jam, a.userid, b.kode_stock_owner, b.kode_barang, d.nama as nama_barang, b.tgl_produksi, b.tgl_expired, "
            SQL = SQL & "b.jumlah, b.jumlah_bags, b.satuan, b.nilai_pengali, b.nilai_barang, b.satuan_barang, b.urut_oto, a.sdh_cetak, a.metode_Timbang "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, EMI_Barang_Masuk_Perpallet_Detail b, Suppliers c, Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur and a.Kode_Supplier = c.Kode_Supplier "
            SQL = SQL & "and b.Kode_Barang = d.Kode_Barang and b.Kode_Stock_Owner = d.Kode_Stock_Owner "
            SQL = SQL & "and a.flag_angkut is null "
            'SQL = SQL & "and a.sdh_cetak is null "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.lokasi = '" & Lokasi & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_BM_PerPallet.Items.Add(Dr("No_Faktur"))
                    lvw.SubItems.Add(Dr("No_Pembelian_Loading"))
                    lvw.SubItems.Add("")
                    lvw.SubItems.Add(Dr("No_SJ"))
                    lvw.SubItems.Add(Dr("No_Plat"))
                    lvw.SubItems.Add(Dr("nama_supplier"))
                    lvw.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Dr("jam"))
                    lvw.SubItems.Add(Dr("userid"))
                    lvw.SubItems.Add(Dr("kode_stock_owner"))
                    lvw.SubItems.Add(Dr("kode_barang"))
                    lvw.SubItems.Add(Dr("nama_barang"))
                    lvw.SubItems.Add(Format(Dr("tgl_produksi"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Format(Dr("tgl_expired"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    If General_Class.CekNULL(Dr("jumlah_bags")) = "" Then
                        lvw.SubItems.Add("-")
                    Else
                        lvw.SubItems.Add(Dr("jumlah_bags"))
                    End If
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(Format(Dr("nilai_pengali"), "N2"))
                    lvw.SubItems.Add(Format(Dr("nilai_barang"), "N2"))
                    lvw.SubItems.Add(Dr("satuan_barang"))
                    lvw.SubItems.Add(Dr("urut_oto"))
                    lvw.SubItems.Add(Dr("metode_timbang"))

                    If General_Class.CekNULL(Dr("sdh_cetak")) = "Y" Then
                        lvw.BackColor = Color.Yellow
                        arrAlreadyPrinted.Add(Dr("No_Faktur"))
                    End If
                Loop
            End Using

            '''Using Dr = OpenTrans(SQL)
            '''    Do While Dr.Read
            '''        Dim lvw As ListViewItem
            '''        lvw = Lv_BMPerPalletDetail.Items.Add(Dr("No_PO"))
            '''        lvw.SubItems.Add(Dr("Kode_Stock_Owner"))
            '''        lvw.SubItems.Add(Dr("Kode_Barang"))
            '''        lvw.SubItems.Add(Dr("Nama"))
            '''        lvw.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
            '''        lvw.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
            '''        lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
            '''        lvw.SubItems.Add(Dr("Satuan"))
            '''        If General_Class.CekNULL(Dr("Jumlah_Bags")) = "" Then
            '''            lvw.SubItems.Add("-")
            '''        Else
            '''            lvw.SubItems.Add(Dr("Jumlah_Bags"))
            '''        End If
            '''        lvw.SubItems.Add(Format(Dr("Nilai_Pengali"), "N2"))
            '''        lvw.SubItems.Add(Format(Dr("Nilai_Barang"), "N2"))
            '''        lvw.SubItems.Add(Dr("Satuan_Barang"))
            '''    Loop
            '''End Using

            '''SQL = "select a.No_Faktur, a.No_Pembelian_Loading, a.Id_Nametag_Pallet, a.No_SJ, a.No_Plat, a.Kode_Supplier, b.Nama as nama_supplier, "
            '''SQL = SQL & "a.Lokasi, a.Tanggal, a.Jam, a.Userid "
            '''SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, Suppliers b "
            '''SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' "
            '''SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier and a.lokasi = '" & Lokasi & "' "
            '''Using Dr = OpenTrans(SQL)
            '''    Do While Dr.Read
            '''        Dim lvw As ListViewItem
            '''        lvw = Lv_BM_PerPallet.Items.Add(Dr("No_Faktur"))
            '''        lvw.SubItems.Add(Dr("No_Pembelian_Loading"))
            '''        lvw.SubItems.Add(Dr("Id_Nametag_Pallet"))
            '''        lvw.SubItems.Add(Dr("No_SJ"))
            '''        lvw.SubItems.Add(Dr("No_Plat"))
            '''        lvw.SubItems.Add(Dr("Kode_Supplier"))
            '''        lvw.SubItems.Add(Dr("nama_supplier"))
            '''        lvw.SubItems.Add(Dr("lokasi"))
            '''        lvw.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
            '''        lvw.SubItems.Add(Dr("jam"))
            '''        lvw.SubItems.Add(Dr("userid"))
            '''    Loop
            '''End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub




    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_BM_PerPallet.SelectedIndexChanged
        ''Dim getNoFak As String = Lv_BM_PerPallet.FocusedItem.Text

        ''Try
        ''    OpenConn()
        ''    Lv_BMPerPalletDetail.Items.Clear()

        ''    SQL = "select b.No_PO, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Tgl_Produksi, b.Tgl_Expired, "
        ''    SQL = SQL & "b.Jumlah, b.Satuan, b.Jumlah_Bags, b.Nilai_Pengali, b.Nilai_Barang, b.Satuan_Barang "
        ''    SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, EMI_Barang_Masuk_Perpallet_Detail b, Barang c "
        ''    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' "
        ''    SQL = SQL & "and a.No_Faktur = b.No_Faktur and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
        ''    SQL = SQL & "and b.Kode_Barang = c.Kode_Barang and b.no_faktur = '" & getNoFak & "' "
        ''    Using Dr = OpenTrans(SQL)
        ''        Do While Dr.Read
        ''            Dim lvw As ListViewItem
        ''            lvw = Lv_BMPerPalletDetail.Items.Add(Dr("No_PO"))
        ''            lvw.SubItems.Add(Dr("Kode_Stock_Owner"))
        ''            lvw.SubItems.Add(Dr("Kode_Barang"))
        ''            lvw.SubItems.Add(Dr("Nama"))
        ''            lvw.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
        ''            lvw.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
        ''            lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
        ''            lvw.SubItems.Add(Dr("Satuan"))
        ''            If General_Class.CekNULL(Dr("Jumlah_Bags")) = "" Then
        ''                lvw.SubItems.Add("-")
        ''            Else
        ''                lvw.SubItems.Add(Dr("Jumlah_Bags"))
        ''            End If
        ''            lvw.SubItems.Add(Format(Dr("Nilai_Pengali"), "N2"))
        ''            lvw.SubItems.Add(Format(Dr("Nilai_Barang"), "N2"))
        ''            lvw.SubItems.Add(Dr("Satuan_Barang"))
        ''        Loop
        ''    End Using

        ''    CloseConn()
        ''Catch ex As Exception
        ''    CloseConn()
        ''    MessageBox.Show(ex.Message)
        ''    Exit Sub
        ''End Try
    End Sub


    Private Sub CetakToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakToolStripMenuItem.Click
        If Lv_BM_PerPallet.Items.Count = 0 Or Lv_BM_PerPallet.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        'If Lv_BM_PerPallet.CheckedItems.Count = 0 Then
        '    MessageBox.Show("Pilih dahulu item yang akan di cetak labelnya . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        If arrAlreadyPrinted.Contains(Lv_BM_PerPallet.FocusedItem.Text) Then
            Dim result As DialogResult = MessageBox.Show("Barcode Sudah Pernah diCetak, Apakah Ingin Cetak Lagi?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Is2ndPrint = True
                cetak()
                Exit Sub
            Else
                Exit Sub
            End If
        Else
            Is2ndPrint = False
            cetak()
            Exit Sub
        End If



    End Sub

    Private Sub cetak()

        Dim tanya As String = MessageBox.Show("Yakin ingin mencetak data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbNo Then Exit Sub

        Try
            OpenConn()

            '''Using Ds = Binding("select * from EMI_Barang_Masuk_Perpallet where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Lv_BM_PerPallet.FocusedItem.Text & "'")
            '''    If Ds.Tables("MyTable").Rows.Count <> 0 Then
            '''        Dim CrDoc As New BM_PerPallet     'Nama file CR
            '''        With A_Place_For_Printing2
            '''            CrDoc.SetDataSource(Ds)
            '''            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '''            'CrDoc.PrintOptions.PrinterName = PrinterName
            '''            CrDoc.RecordSelectionFormula = "{EMI_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Barang_Masuk_Perpallet.No_faktur} = '" & Lv_BM_PerPallet.FocusedItem.Text & "'"
            '''            CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
            '''            .Text = "Barang Masuk Per Pallet"
            '''            .CrystalReportViewer1.ReportSource = CrDoc
            '''            '.CrystalReportViewer1.DisplayGroupTree = False
            '''            .Refresh()
            '''            .Show()
            '''        End With
            '''    End If
            '''End Using

            Dim kolom_1 As Integer = 1
            Dim kolom_2 As Integer = 2
            Dim sql1 As String = ""
            Dim sql2 As String = ""
            Dim sudah_execute As String = "belum"
            Dim X As String = ""

            'For i As Integer = 0 To Lv_BM_PerPallet.Items.Count - 1
            '    If Lv_BM_PerPallet.Items(i).Checked = True Then
            '        X = X & "'" & Lv_BM_PerPallet.Items(i).SubItems(itemUrutOto).Text & "',"
            '        'If i <> ListView2.CheckedItems.Count - 1 Then
            '        '    X = X & ","
            '        'End If
            '    End If
            'Next

            'X = Strings.Left(X, Len(X) - 1)

            SQL = "truncate table Cetak_Barang_Masuk_Perpallet "
            '''SQL = "delete from cetak_barang_masuk_perpallet "
            '''SQL = SQL & "where no_barang_masuk_per_pallet = '" & Lv_BM_PerPallet.FocusedItem.Text & "' and userid = '" & UserID & "' "
            ExecuteTrans(SQL)

            SQL = "select Tahun_Mulai_Produksi from Init"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    tahunMulaiProduksi = If(General_Class.CekNULL(dr("Tahun_Mulai_Produksi")) = "", "0", dr("Tahun_Mulai_Produksi"))
                Loop
            End Using

            Dim sudahCetak As Boolean = False

            SQL = "Select a.no_faktur, a.No_Pembelian_Loading, b.kode_stock_owner, b.Kode_Barang, c.Nama, b.Tgl_Produksi, b.Tgl_Expired, "
            SQL = SQL & "b.Jumlah, b.Satuan, b.Jumlah_Bags, b.Nilai_Pengali, b.Nilai_Barang, b.Satuan_Barang, b.urut_oto, "
            SQL = SQL & "a.no_sj, a.no_plat, b.Urut_Loading, a.kode_supplier, a.Sdh_Cetak, a.Metode_Timbang, a.Flag_Timbang "
            SQL = SQL & "From EMI_Barang_Masuk_Perpallet a, EMI_Barang_Masuk_Perpallet_Detail b, Barang c "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "And a.No_Faktur = b.No_Faktur And b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "And b.Kode_Barang = c.Kode_Barang and a.no_faktur = '" & Lv_BM_PerPallet.FocusedItem.Text & "' "
            'SQL = SQL & "and b.urut_oto in (" & X & ") order by urut_oto"
            SQL = SQL & "order by urut_oto "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Cmd = New SqlClient.SqlCommand
                        Cmd.Connection = Cn
                        Cmd.CommandType = CommandType.Text

                        Dim batch As String = ""
                        Dim Qr As String = ""

                        For i As Integer = 0 To .Rows.Count - 1
                            Dim kodeUnikBerjalan As String = Generate_Random_Kode(10).ToUpper
                            Dim kodeUnikAsal As String = Generate_Random_Kode(10).ToUpper

                            '======================================
                            '=       CEK APAKAH FORSCALE      =
                            '======================================

                            If General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Metode_Timbang")) = "FLOOR SCALE" Then
                                If General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Flag_Timbang")) <> "Y" Then
                                    CloseConn()
                                    MessageBox.Show("Harap Timbang Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                            End If


                            '======================================
                            '=       CEK SUDAH PERNAH CETAK?      =
                            '======================================
                            If General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Sdh_Cetak")) = "Y" Then
                                sudahCetak = True
                            End If

                            '==================================
                            '=       CEK PO LOADING DET       =
                            '==================================
                            SQL = "select  "
                            SQL = SQL & "ISNULL((sum(b.Tot_Batch_Masuk)), 0) as Batch_Masuk, "
                            SQL = SQL & "a.Kode_Supplier, a.Tanggal_Masuk, b.Tanggal_Expired, b.Kode_Barang "
                            SQL = SQL & "from emi_pembelian_loading a, emi_pembelian_loading_detail b, EMI_Barang_Masuk_Perpallet c "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.No_Faktur = c.No_Pembelian_Loading "
                            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "and a.Status is null "
                            SQL = SQL & "and c.No_Faktur='" & Ds.Tables("MyTable").Rows(i).Item("no_faktur") & "' "
                            SQL = SQL & "and b.Kode_Barang='" & Ds.Tables("MyTable").Rows(i).Item("Kode_Barang") & "' "
                            SQL = SQL & "and a.Tanggal_Masuk='" & Format(DateTime.Now, "yyyy-MM-dd") & "' "
                            'SQL = SQL & "and b.Urut_PO='" & Ds.Tables("MyTable").Rows(i).Item("Urut_Loading") & "' "
                            SQL = SQL & "and b.Urut_OTO='" & Ds.Tables("MyTable").Rows(i).Item("Urut_Loading") & "' "
                            SQL = SQL & "group by a.Kode_Supplier, a.Tanggal_Masuk, b.Tanggal_Expired, b.Kode_Barang "
                            Using Ds2 = BindingTrans(SQL)
                                With Ds2.Tables("MyTable")
                                    If .Rows.Count <> 0 Then

                                        For j As Integer = 0 To .Rows.Count - 1

                                            Dim expDate As String = ""
                                            Dim tanggalDatang As DateTime = Ds2.Tables("MyTable").Rows(j).Item("Tanggal_Masuk")
                                            Dim SupplierKode As String = Ds2.Tables("MyTable").Rows(j).Item("Kode_Supplier").ToString
                                            Dim tanggalMasuk As Integer = tanggalDatang.Day
                                            Dim bulanMasuk As Integer = tanggalDatang.Month
                                            Dim tahunMasuk As Integer = (tanggalDatang.Year - tahunMulaiProduksi) Mod 9
                                            'Dim expDate As DateTime = Format(Ds2.Tables("MyTable").Rows(j).Item("Tanggal_Expired"), "yyy-MM-dd")
                                            Dim barangKode As String = Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang").ToString

                                            SQL = "select metode_pengeluaran_Stok from barang "
                                            SQL = SQL & "where kode_barang='" & Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang") & "' "
                                            SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                            SQL = SQL & "group by metode_pengeluaran_Stok"
                                            Using Dr = OpenTrans(SQL)
                                                Do While Dr.Read
                                                    If General_Class.CekNULL(Dr("metode_pengeluaran_Stok")) = "FIFO" Then
                                                        expDate = "000000"
                                                    Else
                                                        expDate = Format(Ds2.Tables("MyTable").Rows(j).Item("Tanggal_Expired"), "ddMMyy").ToString()
                                                    End If
                                                Loop
                                            End Using


                                            If Ds2.Tables("MyTable").Rows(i).Item("Batch_Masuk") = "0" And sudahCetak = False Then

                                                '==============================================
                                                '=       CEK SELURUH TRANSAKSI HARI INI       =
                                                '==============================================
                                                SQL = "select sum(Tot_Batch_Masuk) as Jmlh_Masuk_Hari_ini "
                                                SQL = SQL & "from emi_pembelian_loading a, emi_pembelian_loading_detail b "
                                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                                                SQL = SQL & "and b.Kode_Barang='" & Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang") & "' "
                                                SQL = SQL & "and a.Tanggal_Masuk='" & Format(DateTime.Now, "yyyy-MM-dd") & "' "
                                                SQL = SQL & "and a.Status is null "
                                                SQL = SQL & "and Tot_Batch_Masuk is not null "
                                                Using Ds3 = BindingTrans(SQL)
                                                    With Ds3.Tables("MyTable")
                                                        If .Rows.Count <> 0 Then
                                                            For k As Integer = 0 To .Rows.Count - 1

                                                                '==========================
                                                                '=      UPDATE DATA       =
                                                                '==========================
                                                                SQL = "update emi_pembelian_loading_detail set Tot_Batch_Masuk=" & Ds3.Tables("MyTable").Rows(k).Item("Jmlh_Masuk_Hari_ini") & " + 1 "
                                                                SQL = SQL & "where No_Faktur='" & Ds.Tables("MyTable").Rows(i).Item("No_Pembelian_Loading") & "' and Urut_Oto='" & Ds.Tables("MyTable").Rows(i).Item("Urut_Loading") & "'"
                                                                ExecuteTrans(SQL)

                                                                Dim SupOrder As Integer = Val(Ds3.Tables("MyTable").Rows(k).Item("Jmlh_Masuk_Hari_ini")) + 1

                                                                batch = Generate_Batch(SupplierKode, tanggalMasuk, bulanMasuk, tahunMasuk, SupOrder, expDate)
                                                                Qr = Generate_QR(barangKode, batch)


                                                            Next
                                                        End If
                                                    End With
                                                End Using
                                            Else



                                                Dim SupOrder As Integer = Val(Ds2.Tables("MyTable").Rows(j).Item("Batch_Masuk"))

                                                batch = Generate_Batch(SupplierKode, tanggalMasuk, bulanMasuk, tahunMasuk, SupOrder, expDate)
                                                Qr = Generate_QR(barangKode, batch)
                                            End If
                                        Next

                                    Else
                                        Exit Sub
                                    End If
                                End With
                            End Using



                            '1
                            'Dim nama1 As String = .Rows(i).Item("kode_stock_owner") & "###" & .Rows(i).Item("no_faktur") & "###" & .Rows(i).Item("kode_barang") & "###" & .Rows(i).Item("nama")
                            'Dim QR_Kode_Barang As String = ""
                            'QR_Kode_Barang = .Rows(i).Item("kode_barang")
                            'PictureBoxKdBrg.Image = Generate_QR_1(nama1 & "-" & kodeUnikBerjalan) 'CType(, Image)
                            PictureBoxKdBrg.Image = Generate_QR_1(Qr & "-" & kodeUnikBerjalan)

                            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, .Rows(i).Item("urut_oto") & "_barang1433.jpg")
                            'If Not (System.IO.File.Exists(FileToSaveAs1)) Then

                            'End If

                            PictureBoxKdBrg.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)

                            fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                            FileSize1 = fs1.Length
                            rawData1 = New Byte(FileSize1) {}
                            fs1.Read(rawData1, 0, FileSize1)
                            fs1.Close()
                            Cmd.Parameters.Add("@foto1" & .Rows(i).Item("urut_oto"), SqlDbType.Image).Value = rawData1

                            '2
                            'Dim nama2 As String = .Rows(i).Item("kode_stock_owner") & "###" & .Rows(i).Item("no_sj") & "###" & .Rows(i).Item("no_plat")
                            'Dim QR_Tracking_Barang As String = ""
                            'QR_Tracking_Barang = .Rows(i).Item("no_sj") & " / " & .Rows(i).Item("no_plat")
                            'PictureBoxTracking.Image = Generate_QR_2(nama2) 'CType(, Image)

                            'Dim FileToSaveAs2 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, .Rows(i).Item("urut_oto") & "_tracking.jpg")
                            'If Not (System.IO.File.Exists(FileToSaveAs2)) Then
                            '    PictureBoxTracking.Image.Save(FileToSaveAs2, System.Drawing.Imaging.ImageFormat.Jpeg)
                            'End If

                            'fs2 = New FileStream(FileToSaveAs2, FileMode.Open, FileAccess.Read)
                            'FileSize2 = fs2.Length
                            'rawData2 = New Byte(FileSize2) {}
                            'fs2.Read(rawData2, 0, FileSize2)
                            'fs2.Close()
                            'Cmd.Parameters.Add("@foto2" & .Rows(i).Item("urut_oto"), SqlDbType.Image).Value = rawData2



                            '''
                            SQL = "insert into Cetak_Barang_Masuk_Perpallet(kode_perusahaan, no_barang_masuk_per_pallet, [" & kolom_1 & "], [" & kolom_1 & "a], "
                            SQL = SQL & "[" & kolom_2 & "], [" & kolom_2 & "a], userid, Qr) values "
                            SQL = SQL & "('" & KodePerusahaan & "', '" & Lv_BM_PerPallet.FocusedItem.Text & "', "
                            SQL = SQL & "'" & batch & "', @foto1" & .Rows(i).Item("urut_oto") & ", "
                            'SQL = SQL & "'" & batch & "', @foto2" & .Rows(i).Item("urut_oto") & ", "
                            SQL = SQL & "null, null, "
                            SQL = SQL & "'" & UserID & "', '" & Qr & "')"
                            ExecuteTrans(SQL)


                            ''''update
                            If Is2ndPrint = False Then
                                SQL = "update EMI_Barang_Masuk_Perpallet set Sdh_Cetak = 'Y', "
                                SQL = SQL & "batch_number='" & batch & "', QR_Code='" & Qr & "', "
                                SQL = SQL & "kode_unik_berjalan='" & kodeUnikBerjalan & "', kode_unik_asal='" & kodeUnikAsal & "' "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Lv_BM_PerPallet.FocusedItem.Text & "' "
                                'SQL = SQL & "and userid = '" & UserID & "' "
                                ExecuteTrans(SQL)
                            End If
                        Next

                    Else
                        CloseConn()
                        MessageBox.Show("Data pembelian tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()
            Dim CrDoc As New Object

            SQL = "select kode_perusahaan from Cetak_Barang_Masuk_Perpallet "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_barang_masuk_per_pallet = '" & Lv_BM_PerPallet.FocusedItem.Text & "' "
            SQL = SQL & "and userid = '" & UserID & "' "
            '''SQL = "select a.kode_perusahaan, a.userid, b.no_faktur, b.sdh_cetak "
            '''SQL = SQL & "from cetak_barang_masuk_Perpallet a, EMI_Barang_Masuk_Perpallet b "
            '''SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.no_barang_masuk_per_pallet = b.No_Faktur "
            '''SQL = SQL & "and b.Sdh_Cetak is null and a.no_barang_masuk_per_pallet = '" & Lv_BM_PerPallet.FocusedItem.Text & "'"
            '''SQL = SQL & "and a.userid = '" & UserID & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New BM_PerPallet
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = "{EMI_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Barang_Masuk_Perpallet.No_Faktur} = '" & Lv_BM_PerPallet.FocusedItem.Text & "' and {EMI_Barang_Masuk_Perpallet.UserID} = '" & UserID & "' and IsNull({EMI_Barang_Masuk_Perpallet.Sdh_Cetak}) "
                        CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
                        .Text = "Barang Masuk Per Pallet"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With

                    '''CrDoc.SetDataSource(Ds)
                    '''CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '''CrDoc.PrintOptions.PrinterName = ""
                    '''Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    '''doctoprint.PrinterSettings.PrinterName = ""
                    '''A_Place_For_Printing2.CrystalReportViewer1.ReportSource = CrDoc
                    '''A_Place_For_Printing2.Refresh()
                    '''A_Place_For_Printing2.Show()
                End If
            End Using

            'Using Ds = Binding("select * from EMI_Barang_Masuk_Perpallet where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Lv_BM_PerPallet.FocusedItem.Text & "'")
            '    If Ds.Tables("MyTable").Rows.Count <> 0 Then
            '        Dim CrDoc As New BM_PerPallet     'Nama file CR
            '        With A_Place_For_Printing2
            '            CrDoc.SetDataSource(Ds)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            'CrDoc.PrintOptions.PrinterName = PrinterName
            '            CrDoc.RecordSelectionFormula = "{EMI_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Barang_Masuk_Perpallet.No_faktur} = '" & Lv_BM_PerPallet.FocusedItem.Text & "'"
            '            CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
            '            .Text = "Barang Masuk Per Pallet"
            '            .CrystalReportViewer1.ReportSource = CrDoc
            '            '.CrystalReportViewer1.DisplayGroupTree = False
            '            .Refresh()
            '            .Show()
            '        End With
            '    End If
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'kosong()
    End Sub

    Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If Lv_BM_PerPallet.Items.Count = 0 Or Lv_BM_PerPallet.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        'EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = Lv_BM_PerPallet.FocusedItem.Text
        'EMI_Barang_Masuk_Display_Rak.ShowDialog()
    End Sub

    Private Function Generate_Random_Kode(ByVal length As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim result As New StringBuilder()

        For i As Integer = 1 To length
            Dim index As Integer = random.Next(0, chars.Length)
            result.Append(chars(index))
        Next

        Return result.ToString()
    End Function

    Private Sub Lv_BM_PerPallet_DoubleClick(sender As Object, e As EventArgs) Handles Lv_BM_PerPallet.DoubleClick
        If Lv_BM_PerPallet.SelectedItems.Count = 0 Or Lv_BM_PerPallet.Items.Count = 0 Then
            Exit Sub
        End If
        Get_Isi_Listview(Lv_BM_PerPallet.FocusedItem.Index)

        If LvMetodeTimbang.Trim.ToUpper <> "FLOOR SCALE" Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select Flag_Timbang from EMI_Barang_Masuk_Perpallet where No_Faktur='" & LvNoFaktur.ToString & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    If General_Class.CekNULL(dr("Flag_Timbang")) = "Y" Then
                        CloseConn()
                        'MessageBox.Show("Data Sudah DiTimbang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        EMI_Timbang_Floor_Scale.kosong()
        'EMI_Timbang_Floor_Scale.CmbJenisTimbang.Text = "BARANG MASUK"
        EMI_Timbang_Floor_Scale.txtKodeTransfer.Text = LvNoFaktur
        EMI_Timbang_Floor_Scale.UNIX.Text = LvNoFaktur
        EMI_Timbang_Floor_Scale.txt_lokasi.Text = LvKodeSO
        EMI_Timbang_Floor_Scale.txt_barang.Text = LvNmBrg
        EMI_Timbang_Floor_Scale.TxtKdBarang.Text = LvKdBrg
        EMI_Timbang_Floor_Scale.txt_Jml_Estimasi.Text = 0


        EMI_Timbang_Floor_Scale.ShowDialog()
    End Sub


    Private Function Generate_Batch(ByVal SupCode As String, ByVal tgl_kedatangan As Integer, ByVal BulanKedatangan As Integer, ByVal tahunKedatangan As Integer,
        ByVal supplierBatchOrder As Integer, ByVal exp As String) As String

        BulanKedatangan = BulanKedatangan + 1
        supplierBatchOrder = supplierBatchOrder + 1


        Dim NumberToChar As New ArrayList From {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                        "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        Dim finalBatch As String = ""
        finalBatch = SupCode & tgl_kedatangan & NumberToChar((BulanKedatangan - 1) Mod 26) & tahunKedatangan & NumberToChar((supplierBatchOrder - 1) Mod 26) & exp

        Return finalBatch
    End Function

    Private Function Generate_QR(ByVal MaterialCode As String, ByVal BatchCode As String) As String

        'Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        'Dim UnixCode As New StringBuilder()

        'For i As Integer = 1 To 10
        '    Dim index As Integer = random.Next(0, chars.Length)
        '    UnixCode.Append(chars(index))
        'Next

        Dim Qr As String = ""
        Qr = MaterialCode & "-" & BatchCode

        Return Qr
    End Function




End Class