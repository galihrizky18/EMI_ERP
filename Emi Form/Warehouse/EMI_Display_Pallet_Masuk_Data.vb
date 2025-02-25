Imports System.CodeDom.Compiler
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports ZXing
Imports ZXing.Common
Imports ZXing.QrCode

Public Class EMI_Display_Pallet_Masuk_Data


    Dim Arr1, Arr2, Arr3, Arr4, arrAlreadyPrinted As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Dim tahunMulaiProduksi As String = ""

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

    Dim LvNoFaktur, LvNoPembLoading, LvIdNametagPallet, LvNoSJ, LvNoPlat, LvNmSupplier, LvTgl, LvJam, LvUserId, LvKodeSO, LvKdBrg, LvNmBrg, LvTglProd, LvTglExp, LvJumlah, LvJmlBags, LvSatuan, LvNilaiPengali, LvNilaiBrg, LvSatuanBrg, LvUrutOto As String

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
    End Sub
    Private Sub Display_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Private Sub kosong()

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
            Lv_BM_PerPallet.Columns.Add("User ID", 0, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("Lokasi", 0, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Tanggal Produksi", 150, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("Tanggal Expired", 150, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("Jumlah", 100, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Jumlah Bags", 100, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("Nilai Pengali", 0, HorizontalAlignment.Right)
            Lv_BM_PerPallet.Columns.Add("Nilai Barang", 0, HorizontalAlignment.Right)
            Lv_BM_PerPallet.Columns.Add("Satuan Barang", 0, HorizontalAlignment.Center)
            Lv_BM_PerPallet.Columns.Add("UrutOto", 0, HorizontalAlignment.Left)
            Lv_BM_PerPallet.Columns.Add("QR Code", 130, HorizontalAlignment.Left)
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

        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear()
            Cmb_Lokasi.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)

            'xSplit = CekKotaRole().Split(", ")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and kode_kota in( "
            'For i As Integer = 0 To xSplit.Count - 1
            '    SQL = SQL & "'" & xSplit(i).Trim & "', "
            'Next
            'SQL = Strings.Left(SQL, Len(SQL) - 2)

            'SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            'ComboBox1.Items.Add("Seluruh")
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using


            Cmb_Lokasi.Text = Lokasi

            'If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
            '    Cmb_Lokasi.Enabled = False
            'Else
            '    Cmb_Lokasi.Enabled = True
            'End If

            'ComboBox3.Items.Add("Y") : Arr4.Add("Y")
            'ComboBox3.Items.Add("T") : Arr4.Add("T")
            'ComboBox3.SelectedIndex = 1

            Cb_TransaksiHrIni.Checked = False : Cb_ParamTgl.Checked = False : Cb_ParamLain.Checked = False

            Cmb_ParamTgl.Items.Clear() : Arr1.Clear()
            Cmb_ParamTgl.Items.Add("Tanggal") : Arr1.Add("a.Tanggal")

            'TextBoxa.Text = "0" 
            Cmb_ParamTgl.Enabled = False : Cmb_ParamLain.Enabled = False
            Dtp_Awal.Enabled = False : Dtp_Akhir.Enabled = False
            Txt_ParamLain.Enabled = False

            Cmb_ParamLain.Items.Clear() : Cmb_ParamLain.Text = "" : Arr2.Clear()
            Cmb_ParamLain.Items.Add("No Faktur") : Arr2.Add("a.no_faktur")
            'ComboBox2.Items.Add("NO Nota") : Arr2.Add("a.no_nota")
            'ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.kode_supplier")

            Lbl_Title.Text = "Display - Barang Masuk Per Pallet"
            Cb_TransaksiHrIni.Text = Base_Language.Lang_Global_Hari_ini
            Cb_ParamTgl.Text = Base_Language.Lang_Global_Para_Tbl
            Cb_ParamLain.Text = Base_Language.Lang_Global_Para_lain
            Btn_Cari.Text = Base_Language.Lang_Global_Cari

            CloseConn()
        Catch ex As Exception
            Cmb_Lokasi.Items.Clear()
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

            SQL = "select a.qr_code + '-' + a.kode_unik_berjalan as Qr_Code ,a.No_Faktur, a.No_Pembelian_Loading, a.Id_Nametag_Pallet, a.No_SJ, a.No_Plat, c.Nama as nama_supplier, "
            SQL = SQL & "a.tanggal, a.jam, a.userid, b.kode_stock_owner, b.kode_barang, d.nama as nama_barang, b.tgl_produksi, b.tgl_expired, "
            SQL = SQL & "b.jumlah, b.jumlah_bags, b.satuan, b.nilai_pengali, b.nilai_barang, b.satuan_barang, b.urut_oto, a.sdh_cetak "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, EMI_Barang_Masuk_Perpallet_Detail b, Suppliers c, Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur and a.Kode_Supplier = c.Kode_Supplier "
            SQL = SQL & "and b.Kode_Barang = d.Kode_Barang and b.Kode_Stock_Owner = d.Kode_Stock_Owner "
            SQL = SQL & "and a.flag_angkut is null "
            'SQL = SQL & "and a.sdh_cetak is null "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.userid = '" & UserID & "' and a.lokasi = '" & Lokasi & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_BM_PerPallet.Items.Add(Dr("No_Faktur"))
                    lvw.SubItems.Add(Dr("No_Pembelian_Loading"))
                    lvw.SubItems.Add(Dr("Id_Nametag_Pallet"))
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

                    If General_Class.CekNULL(Dr("qr_code")) = "" Then
                        lvw.SubItems.Add("-")
                    Else
                        lvw.SubItems.Add(Dr("qr_code"))
                    End If
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

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBoxFilterData.Enter

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_TransaksiHrIni.CheckedChanged
        If Cb_TransaksiHrIni.Checked = True Then
            Cb_ParamTgl.Checked = False
            BtnBarangMasuk_Cari_Click(Cb_TransaksiHrIni, e)
        End If
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

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Try
            pertama = 1

            arrAlreadyPrinted.Clear()

            If Cb_ParamTgl.Checked = False And Cb_ParamLain.Checked = False And Cb_TransaksiHrIni.Checked = False Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Judul)
                Cb_ParamTgl.Focus() : Exit Sub
            End If

            If Cb_ParamTgl.Checked Then
                If Cmb_ParamTgl.SelectedIndex = -1 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Judul)
                    Cmb_ParamTgl.Focus() : Exit Sub
                ElseIf Dtp_Awal.Value > Dtp_Akhir.Value Then
                    MessageBox.Show("Periode I " & Base_Language.Lang_Global_TidakBolehLebihDari & " periode II!", Judul)
                    Dtp_Awal.Value = Now.Date : Dtp_Akhir.Value = Now.Date
                    Exit Sub
                End If
            ElseIf Cb_ParamLain.Checked Then
                If Cmb_ParamLain.SelectedIndex = -1 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain, Judul)
                    Cmb_ParamLain.Focus() : Exit Sub
                ElseIf Txt_ParamLain.Text.Trim.Length = 0 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Judul)
                    Txt_ParamLain.Focus() : Exit Sub
                End If
            End If

            OpenConn()

            Lv_BM_PerPallet.Items.Clear()

            'Lv_BMPerPalletDetail.Items.Clear()

            'SQL = "select a.No_Faktur,b.kode_supplier,b.Nama, a.tanggal, a.userid, a.keterangan "
            'SQL = SQL & "from EMI_Purchase_Requisition a, Suppliers b  "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            'SQL = SQL & "and a.Status is null "
            '''SQL = "select No_Faktur, tanggal, tanggal_release, keterangan, userid, Flag_Release "
            '''SQL = SQL & "from EMI_Purchase_Requisition   "
            '''SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            '''SQL = SQL & "and Status is null "

            '''-----------------------------------
            SQL = "select a.qr_code + '-' + a.kode_unik_Berjalan as QR_Code, a.sdh_cetak, a.No_Faktur, a.No_Pembelian_Loading, a.Id_Nametag_Pallet, a.No_SJ, a.No_Plat, c.Nama as nama_supplier, "
            SQL = SQL & "a.tanggal, a.jam, a.userid, b.kode_stock_owner, b.kode_barang, d.nama as nama_barang, a.tgl_produksi_real as Tgl_Produksi, a.Tgl_expired_real as tgl_expired, "
            SQL = SQL & "b.jumlah, b.jumlah_bags, b.satuan, b.nilai_pengali, b.nilai_barang, b.satuan_barang, b.urut_oto "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, EMI_Barang_Masuk_Perpallet_Detail b, Suppliers c, Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur and a.Kode_Supplier = c.Kode_Supplier "
            SQL = SQL & "and b.Kode_Barang = d.Kode_Barang and b.Kode_Stock_Owner = d.Kode_Stock_Owner "
            '   SQL = SQL & "and a.flag_angkut is null "
            'SQL = SQL & "and a.sdh_cetak is null "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.lokasi = '" & Lokasi & "' "

            If Cb_TransaksiHrIni.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "and "

                SQL = SQL & " a.tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If Cb_ParamTgl.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "and "

                SQL = SQL & Arr1.Item(Cmb_ParamTgl.SelectedIndex) & " between '"
                SQL = SQL & Format(Dtp_Awal.Value, "yyyy-MM-dd") & "' and '" & Format(Dtp_Akhir.Value, "yyyy-MM-dd") & "' "
            End If

            If Cb_ParamLain.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "and "

                SQL = SQL & Arr2.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
            End If

            ''If Cmb_Lokasi.SelectedIndex = 0 Then
            ''    SQL = SQL & " and Lokasi in("
            ''    Dim list_kota As String = ""
            ''    For x As Integer = 1 To Cmb_Lokasi.Items.Count - 1
            ''        list_kota = list_kota & "'" & Cmb_Lokasi.Items(x).ToString & "', "
            ''    Next

            ''    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            ''    SQL = SQL & list_kota & ")"
            ''Else
            ''    SQL = SQL & " and Lokasi = '" & Cmb_Lokasi.Text & "' "
            ''End If

            SQL = SQL & "order by tanggal , jam"

            'Dim Lvw As ListViewItem
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
                    If General_Class.CekNULL(Dr("sdh_cetak")) = "" Then
                        lvw.SubItems.Add("-")
                    Else
                        lvw.SubItems.Add(Dr("qr_code"))
                    End If

                    If General_Class.CekNULL(Dr("sdh_cetak")) = "Y" Then
                        lvw.BackColor = Color.Yellow
                        arrAlreadyPrinted.Add(Dr("No_Faktur"))
                    End If
                Loop
            End Using
            '''Using Ds = BindingTrans(SQL)
            '''    With Ds.Tables("MyTable")
            '''        If .Rows.Count <> 0 Then
            '''            For i As Integer = 0 To .Rows.Count - 1
            '''                Lvw = Lv_BM_PerPallet.Items.Add(.Rows(i).Item("no_faktur"))
            '''                'Lvw.SubItems.Add(.Rows(i).Item("kode_supplier"))
            '''                'Lvw.SubItems.Add(.Rows(i).Item("nama"))
            '''                Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
            '''                If General_Class.CekNULL(.Rows(i).Item("tanggal_release")) = "" Then
            '''                    Lvw.SubItems.Add("-")
            '''                Else
            '''                    Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_release"), "dd MMM yyyy"))
            '''                End If
            '''                Lvw.SubItems.Add(.Rows(i).Item("keterangan"))

            '''                If General_Class.CekNULL(.Rows(i).Item("Flag_Release")) = "Y" Then
            '''                    Lvw.SubItems.Add("SUBMITTED")
            '''                Else
            '''                    Lvw.SubItems.Add("UNSUBMITTED")
            '''                End If

            '''                Lvw.SubItems.Add(.Rows(i).Item("userid"))
            '''                ''Lvw = Lv_PR.Items.Add(.Rows(i).Item("no_faktur"))
            '''                ''Lvw.SubItems.Add(.Rows(i).Item("no_nota"))
            '''                ''Lvw.SubItems.Add(.Rows(i).Item("kode_supplier"))
            '''                ''Lvw.SubItems.Add(.Rows(i).Item("nama"))

            '''                ''If General_Class.CekNULL(.Rows(i).Item("jenis_pembayaran")) = "N" Then
            '''                ''    Lvw.SubItems.Add("Non Tunai")
            '''                ''Else
            '''                ''    Lvw.SubItems.Add("Tunai")
            '''                ''End If
            '''                ''Lvw.SubItems.Add(.Rows(i).Item("cara_bayar"))

            '''                ''If .Rows(i).Item("jenis_pembayaran") = "N" Then
            '''                ''    Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Jatuh_Tempo"), "dd MMM yyyy"))
            '''                ''Else
            '''                ''    Lvw.SubItems.Add("-")
            '''                ''End If

            '''                ''Lvw.SubItems.Add(.Rows(i).Item("mata_uang"))
            '''                ''Lvw.SubItems.Add(Format(.Rows(i).Item("total_mua"), "N2"))
            '''                ''Lvw.SubItems.Add(Format(.Rows(i).Item("total_idr"), "N2"))
            '''                ''Lvw.SubItems.Add(Format(.Rows(i).Item("ppn"), "N2"))
            '''                ''Lvw.SubItems.Add(Format(.Rows(i).Item("grand"), "N2"))
            '''                ''Lvw.SubItems.Add(Format(.Rows(i).Item("etd_simulasi"), "dd MMM yyyy"))


            '''                ''Lv_PR.Items(i).ForeColor = T

            '''                ''If General_Class.CekNULL(.Rows(i).Item("flag_release")) <> "Y" Then
            '''                ''    Lv_PR.Items(i).ForeColor = Batal
            '''                ''End If
            '''            Next
            '''        End If
            '''    End With
            '''End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
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

        get_jam()

        Dim kode_unik_print As String = ""

        Try
            OpenConn()

            Get_Isi_Listview(Lv_BM_PerPallet.FocusedItem.Index)

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



            SQL = "truncate table Cetak_Barang_Masuk_Perpallet "
            ExecuteTrans(SQL)

            SQL = "select Tahun_Mulai_Produksi from Init"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    tahunMulaiProduksi = If(General_Class.CekNULL(dr("Tahun_Mulai_Produksi")) = "", "0", dr("Tahun_Mulai_Produksi"))
                End If


            End Using

            Dim sudahCetak As Boolean = False

            SQL = "Select a.no_faktur, a.No_Pembelian_Loading, b.kode_stock_owner, b.Kode_Barang, c.Nama, a.tgl_produksi_real as Tgl_Produksi, a.Tgl_expired_real as tgl_expired, "
            SQL = SQL & "b.Jumlah, b.Satuan, b.Jumlah_Bags, b.Nilai_Pengali, b.Nilai_Barang, b.Satuan_Barang, b.urut_oto, "
            SQL = SQL & "a.no_sj, a.no_plat, b.Urut_Loading, a.kode_supplier, a.Sdh_Cetak, a.Metode_Timbang, a.Flag_Timbang, "
            SQL = SQL & "c.Metode_Pengeluaran_Stok, d.Tanggal as Tanggal_Masuk, a.Qr_Code, a.Kode_Unik_Berjalan "
            SQL = SQL & "From EMI_Barang_Masuk_Perpallet a,EMI_Barang_Masuk_Perpallet_Detail b, Barang c, EMI_Register_Kendaraan_BM d "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "And a.No_Faktur = b.No_Faktur And b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Pembelian_Loading =  d.No_Fak_Loading_Barang "
            SQL = SQL & "And b.Kode_Barang = c.Kode_Barang and a.no_faktur = '" & Lv_BM_PerPallet.FocusedItem.Text & "' "
            SQL = SQL & "and b.urut_oto = '" & LvUrutOto & "' "
            SQL = SQL & "and b.Kode_Barang = '" & LvKdBrg & "' "
            SQL = SQL & "order by urut_oto "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    'Cmd = New SqlClient.SqlCommand
                    'Cmd.Connection = Cn
                    'Cmd.CommandType = CommandType.Text

                    Dim batch As String = ""
                    Dim Qr As String = ""

                    For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
                        Dim kodeUnikBerjalan As String = ""
                        Dim kodeUnikAsal As String = ""
                        Dim Jumlah As String = ""
                        Dim satuan As String = ""
                        '======================================
                        '=       CEK APAKAH FLOORSCALE      =
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
                        SQL = SQL & "a.Kode_Supplier, a.Tanggal_Masuk, c.Tgl_expired_real as tgl_expired, b.Kode_Barang, c.Kode_Unik_Berjalan, sum(c.jumlah) as jumlah, c.satuan "
                        SQL = SQL & "from emi_pembelian_loading a, emi_pembelian_loading_detail b, EMI_Barang_Masuk_Perpallet c "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.No_Faktur = c.No_Pembelian_Loading "
                        SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                        SQL = SQL & "and a.Status is null "
                        SQL = SQL & "and c.No_Faktur='" & Ds.Tables("MyTable").Rows(i).Item("no_faktur") & "' "
                        SQL = SQL & "and b.Kode_Barang='" & Ds.Tables("MyTable").Rows(i).Item("Kode_Barang") & "' "
                        SQL = SQL & "and b.Urut_OTO='" & Ds.Tables("MyTable").Rows(i).Item("Urut_Loading") & "' "
                        SQL = SQL & "group by a.Kode_Supplier, a.Tanggal_Masuk, c.Tgl_expired_real, b.Kode_Barang, c.Kode_Unik_Berjalan, c.satuan "
                        Using Ds2 = BindingTrans(SQL)
                            With Ds2.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For j As Integer = 0 To .Rows.Count - 1
                                        Jumlah = .Rows(j).Item("jumlah")
                                        satuan = .Rows(j).Item("satuan")
                                        Dim expDate As String = ""
                                        Dim tanggalDatang As DateTime = .Rows(j).Item("Tanggal_Masuk")
                                        Dim SupplierKode As String = .Rows(j).Item("Kode_Supplier").ToString
                                        Dim tanggalMasuk As Integer = tanggalDatang.Day
                                        Dim bulanMasuk As Integer = tanggalDatang.Month
                                        Dim tahunMasuk As Integer = (tanggalDatang.Year - tahunMulaiProduksi) Mod 9

                                        kodeUnikBerjalan = Ds.Tables("MyTable").Rows(i).Item("Kode_Unik_Berjalan")
                                        Qr = Ds.Tables("MyTable").Rows(i).Item("Qr_Code")

                                        If tahunMasuk = 0 Then tahunMasuk = 9

                                        'Dim expDate As DateTime = Format(Ds2.Tables("MyTable").Rows(j).Item("Tanggal_Expired"), "yyy-MM-dd")
                                        Dim barangKode As String = .Rows(j).Item("Kode_Barang").ToString

                                        SQL = "select metode_pengeluaran_Stok from barang "
                                        SQL = SQL & "where kode_barang='" & .Rows(j).Item("Kode_Barang") & "' "
                                        SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
                                        SQL = SQL & "group by metode_pengeluaran_Stok"
                                        Using Dr = OpenTrans(SQL)
                                            Do While Dr.Read
                                                If General_Class.CekNULL(Dr("metode_pengeluaran_Stok")) = "FIFO" Then
                                                    expDate = "000000"
                                                Else
                                                    expDate = Format(.Rows(j).Item("tgl_expired"), "ddMMyy").ToString()
                                                End If
                                            Loop
                                        End Using


                                        If .Rows(i).Item("Batch_Masuk") = "0" Then

                                            kodeUnikBerjalan = Generate_Random_Kode(10).ToUpper
                                            kodeUnikAsal = kodeUnikBerjalan

                                            '==============================================
                                            '=       CEK SELURUH TRANSAKSI HARI INI       =
                                            '==============================================
                                            SQL = "select isnull(sum(Tot_Batch_Masuk),0) as Jmlh_Masuk_Hari_ini "
                                            SQL = SQL & "from emi_pembelian_loading a, emi_pembelian_loading_detail b "
                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                                            SQL = SQL & "and b.Kode_Barang='" & .Rows(j).Item("Kode_Barang") & "' "
                                            SQL = SQL & "and a.Tanggal_Masuk='" & Format(tgl_skg, "yyyy-MM-dd") & "' "
                                            SQL = SQL & "and a.Status is null "
                                            SQL = SQL & "and Tot_Batch_Masuk is not null "
                                            Using Ds3 = BindingTrans(SQL)

                                                If .Rows.Count <> 0 Then
                                                    For k As Integer = 0 To .Rows.Count - 1

                                                        '==========================
                                                        '=      UPDATE DATA       =
                                                        '==========================
                                                        SQL = "update emi_pembelian_loading_detail set Tot_Batch_Masuk=" & Ds3.Tables("MyTable").Rows(k).Item("Jmlh_Masuk_Hari_ini") & " + 1 "
                                                        SQL = SQL & "where No_Faktur='" & Ds.Tables("MyTable").Rows(i).Item("No_Pembelian_Loading") & "' and Urut_Oto='" & Ds.Tables("MyTable").Rows(i).Item("Urut_Loading") & "'"
                                                        ExecuteTrans(SQL)

                                                        Dim SupOrder As Integer = Val(Ds3.Tables("MyTable").Rows(k).Item("Jmlh_Masuk_Hari_ini")) + 1

                                                        batch = Generate_Batch_Bahan(SupplierKode, tanggalMasuk, bulanMasuk, tahunMasuk, SupOrder, expDate)
                                                        Qr = Generate_QR_Batch(barangKode, batch)


                                                    Next
                                                End If

                                            End Using
                                        Else
                                            If sudahCetak = True Then
                                                kodeUnikBerjalan = .Rows(j).Item("Kode_Unik_Berjalan")
                                                kodeUnikAsal = kodeUnikBerjalan
                                                Dim SupOrder As Integer = Val(.Rows(j).Item("Batch_Masuk"))

                                                batch = Generate_Batch_Bahan(SupplierKode, tanggalMasuk, bulanMasuk, tahunMasuk, SupOrder, expDate)
                                                Qr = Generate_QR_Batch(barangKode, batch)
                                            Else
                                                CloseConn()
                                                MessageBox.Show("Barcode Belum Pernah Cetak, Tidak Bisa Cetak ulang", "Cetak Ulang", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                        End If
                                    Next

                                Else
                                    CloseConn()
                                    MessageBox.Show("Data tidak Ditemukan", "Cetak Ulang", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using


                        PictureBoxKdBrg.Image = Generate_QR(Qr + "-" + kodeUnikBerjalan + "-" + Jumlah + "" + satuan)

                        Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, Ds.Tables("MyTable").Rows(i).Item("urut_oto") & "_barang1433.jpg")
                        'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
                        PictureBoxKdBrg.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
                        'End If

                        fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                        FileSize1 = fs1.Length
                        rawData1 = New Byte(FileSize1) {}
                        fs1.Read(rawData1, 0, FileSize1)
                        fs1.Close()
                        Cmd.Parameters.Add("@foto1" & Ds.Tables("MyTable").Rows(i).Item("urut_oto"), SqlDbType.Image).Value = rawData1

                        kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")


                        '=================================
                        '=      INSERT TABEL CETAK       =
                        '=================================
                        SQL = "insert into Cetak_barang_Masuk_Perpallet  (Kode_Perusahaan, No_Barang_Masuk_Per_Pallet, Kode_Barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, Tanggal_Cetak, "
                        SQL = SQL & "Kode_Unik_Print,tanggal_masuk,metode_pengeluaran_stok ) values "
                        SQL = SQL & "('" & KodePerusahaan & "', '" & Lv_BM_PerPallet.FocusedItem.Text & "', '" & Ds.Tables("MyTable").Rows(i).Item("Kode_Barang") & "', @foto1" & Ds.Tables("MyTable").Rows(i).Item("urut_oto") & ", "
                        SQL = SQL & "'" & Ds.Tables("MyTable").Rows(i).Item("Nama") & "', '" & Qr & "-" & kodeUnikBerjalan & "-" & Jumlah & "" & satuan & "', '" & Qr & "', '" & Format(Ds.Tables("MyTable").Rows(i).Item("Tgl_Expired"), "yyyy-MM-dd") & "', "
                        SQL = SQL & "'" & batch & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & kode_unik_print & "' ,'" & Ds.Tables("MyTable").Rows(i).Item("tanggal_masuk") & "',"
                        SQL = SQL & "'" & Ds.Tables("MyTable").Rows(i).Item("metode_pengeluaran_stok") & "' )"
                        ExecuteTrans(SQL)


                        'SQL = "insert into Cetak_Barang_Masuk_Perpallet(kode_perusahaan, no_barang_masuk_per_pallet, [" & kolom_1 & "], [" & kolom_1 & "a], "
                        'SQL = SQL & "[" & kolom_2 & "], [" & kolom_2 & "a], userid, Qr) values "
                        'SQL = SQL & "('" & KodePerusahaan & "', '" & Lv_BM_PerPallet.FocusedItem.Text & "', "
                        'SQL = SQL & "'" & batch & "', @foto1" & Ds.Tables("MyTable").Rows(i).Item("urut_oto") & ", "
                        ''SQL = SQL & "'" & batch & "', @foto2" & .Rows(i).Item("urut_oto") & ", "
                        'SQL = SQL & "null, null, "
                        'SQL = SQL & "'" & UserID & "', '" & Qr & "-" & kodeUnikBerjalan & "')"
                        'ExecuteTrans(SQL)

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
                    MessageBox.Show("Kendaraan Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()
            'Dim CrDoc As New Object

            SQL = "select kode_perusahaan from Cetak_Barang_Masuk_Perpallet "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_barang_masuk_per_pallet = '" & Lv_BM_PerPallet.FocusedItem.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then


                    Dim CrDoc As New BM_PerPallet

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{Cetak_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Barang_Masuk_Perpallet.no_barang_masuk_per_pallet} = '" & Lv_BM_PerPallet.FocusedItem.Text & "' and {Cetak_Barang_Masuk_Perpallet.Kode_Unik_Print} = '" & kode_unik_print & "' "

                    CrDoc.PrintOptions.PrinterName = PrinterBarcode

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    CrDoc.PrintToPrinter(1, False, 1, 2500)

                    '============================================================================================================================================
                    '============================================================================================================================================


                    'KODE LAMA
                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Barang_Masuk_Perpallet.no_barang_masuk_per_pallet} = '" & Lv_BM_PerPallet.FocusedItem.Text & "' and {Cetak_Barang_Masuk_Perpallet.Kode_Unik_Print} = '" & kode_unik_print & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
                    '    .Text = "Barang Masuk Per Pallet"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

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


    'Private Sub cetak()

    '    Dim tanya As String = MessageBox.Show("Yakin ingin mencetak data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '    If tanya = vbNo Then Exit Sub

    '    Try
    '        OpenConn()



    '        Dim kolom_1 As Integer = 1
    '        Dim kolom_2 As Integer = 2
    '        Dim sql1 As String = ""
    '        Dim sql2 As String = ""
    '        Dim sudah_execute As String = "belum"
    '        Dim X As String = ""



    '        SQL = "truncate table Cetak_Barang_Masuk_Perpallet "
    '        ExecuteTrans(SQL)

    '        Dim sudahCetak As Boolean = False

    '        SQL = "Select a.no_faktur,a.metode_timbang, b.kode_stock_owner, b.Kode_Barang, c.Nama, b.Tgl_Produksi, b.Tgl_Expired, "
    '        SQL = SQL & "b.Jumlah, b.Satuan,b.Urut_Loading, b.Jumlah_Bags, b.Nilai_Pengali, b.Nilai_Barang, b.Satuan_Barang, b.urut_oto "
    '        SQL = SQL & ",a.no_sj, a.no_plat, a.flag_timbang "
    '        SQL = SQL & "From EMI_Barang_Masuk_Perpallet a, EMI_Barang_Masuk_Perpallet_Detail b, Barang c "
    '        SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Perusahaan = c.Kode_Perusahaan "
    '        SQL = SQL & "And a.No_Faktur = b.No_Faktur And b.Kode_Stock_Owner = c.Kode_Stock_Owner "
    '        SQL = SQL & "And b.Kode_Barang = c.Kode_Barang and a.no_faktur = '" & Lv_BM_PerPallet.FocusedItem.Text & "' "
    '        'SQL = SQL & "and b.urut_oto in (" & X & ") order by urut_oto"
    '        SQL = SQL & "order by urut_oto "
    '        Using Ds = Binding(SQL)
    '            With Ds.Tables("MyTable")
    '                If .Rows.Count <> 0 Then
    '                    Cmd = New SqlClient.SqlCommand
    '                    Cmd.Connection = Cn
    '                    Cmd.CommandType = CommandType.Text
    '                    Dim batch As String = ""
    '                    Dim Qr As String = ""
    '                    For i As Integer = 0 To .Rows.Count - 1

    '                        Dim kodeUnikBerjalan As String = ""
    '                        Dim kodeUnikAsal As String = ""
    '                        '======================================
    '                        '=       CEK APAKAH FLOORSCALE      =
    '                        '======================================

    '                        If General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Metode_Timbang")) = "FLOOR SCALE" Then
    '                            If General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Flag_Timbang")) <> "Y" Then
    '                                CloseConn()
    '                                MessageBox.Show("Harap Timbang Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                                Exit Sub
    '                            End If

    '                        End If

    '                        '======================================
    '                        '=       CEK SUDAH PERNAH CETAK?      =
    '                        '======================================
    '                        If General_Class.CekNULL(Ds.Tables("MyTable").Rows(i).Item("Sdh_Cetak")) = "Y" Then
    '                            sudahCetak = True

    '                        End If



    '                        '==================================
    '                        '=       CEK PO LOADING DET       =
    '                        '==================================

    '                        SQL = "select  "
    '                        SQL = SQL & "ISNULL((sum(b.Tot_Batch_Masuk)), 0) as Batch_Masuk, "
    '                        SQL = SQL & "a.Kode_Supplier, a.Tanggal_Masuk, b.Tanggal_Expired, b.Kode_Barang, c.Kode_Unik_Berjalan "
    '                        SQL = SQL & "from emi_pembelian_loading a, emi_pembelian_loading_detail b, EMI_Barang_Masuk_Perpallet c "
    '                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.No_Faktur = c.No_Pembelian_Loading "
    '                        SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
    '                        SQL = SQL & "and a.Status is null "
    '                        SQL = SQL & "and c.No_Faktur='" & Ds.Tables("MyTable").Rows(i).Item("no_faktur") & "' "
    '                        SQL = SQL & "and b.Kode_Barang='" & Ds.Tables("MyTable").Rows(i).Item("Kode_Barang") & "' "
    '                        SQL = SQL & "and b.Urut_OTO='" & Ds.Tables("MyTable").Rows(i).Item("Urut_Loading") & "' "
    '                        SQL = SQL & "group by a.Kode_Supplier, a.Tanggal_Masuk, b.Tanggal_Expired, b.Kode_Barang, c.Kode_Unik_Berjalan "
    '                        Using Ds2 = BindingTrans(SQL)
    '                            With Ds2.Tables("MyTable")
    '                                If .Rows.Count <> 0 Then
    '                                    For j As Integer = 0 To .Rows.Count - 1

    '                                        Dim expDate As String = ""
    '                                        Dim tanggalDatang As DateTime = .Rows(j).Item("Tanggal_Masuk")
    '                                        Dim SupplierKode As String = .Rows(j).Item("Kode_Supplier").ToString
    '                                        Dim tanggalMasuk As Integer = tanggalDatang.Day
    '                                        Dim bulanMasuk As Integer = tanggalDatang.Month
    '                                        Dim tahunMasuk As Integer = (tanggalDatang.Year - tahunMulaiProduksi) Mod 9

    '                                        If tahunMasuk = 0 Then tahunMasuk = 9

    '                                        'Dim expDate As DateTime = Format(Ds2.Tables("MyTable").Rows(j).Item("Tanggal_Expired"), "yyy-MM-dd")
    '                                        Dim barangKode As String = .Rows(j).Item("Kode_Barang").ToString

    '                                        SQL = "select metode_pengeluaran_Stok from barang "
    '                                        SQL = SQL & "where kode_barang='" & .Rows(j).Item("Kode_Barang") & "' "
    '                                        SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' "
    '                                        SQL = SQL & "group by metode_pengeluaran_Stok"
    '                                        Using Dr = OpenTrans(SQL)
    '                                            Do While Dr.Read
    '                                                If General_Class.CekNULL(Dr("metode_pengeluaran_Stok")) = "FIFO" Then
    '                                                    expDate = "000000"
    '                                                Else
    '                                                    expDate = Format(.Rows(j).Item("Tanggal_Expired"), "ddMMyy").ToString()
    '                                                End If
    '                                            Loop
    '                                        End Using


    '                                        If .Rows(i).Item("Batch_Masuk") = "0" Then

    '                                            kodeUnikBerjalan = Generate_Random_Kode(10).ToUpper
    '                                            kodeUnikAsal = kodeUnikBerjalan

    '                                            '==============================================
    '                                            '=       CEK SELURUH TRANSAKSI HARI INI       =
    '                                            '==============================================
    '                                            SQL = "select isnull(sum(Tot_Batch_Masuk),0) as Jmlh_Masuk_Hari_ini "
    '                                            SQL = SQL & "from emi_pembelian_loading a, emi_pembelian_loading_detail b "
    '                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
    '                                            SQL = SQL & "and b.Kode_Barang='" & .Rows(j).Item("Kode_Barang") & "' "
    '                                            SQL = SQL & "and a.Tanggal_Masuk='" & Format(tgl_skg, "yyyy-MM-dd") & "' "
    '                                            SQL = SQL & "and a.Status is null "
    '                                            SQL = SQL & "and Tot_Batch_Masuk is not null "
    '                                            Using Ds3 = BindingTrans(SQL)

    '                                                If .Rows.Count <> 0 Then
    '                                                    For k As Integer = 0 To .Rows.Count - 1

    '                                                        '==========================
    '                                                        '=      UPDATE DATA       =
    '                                                        '==========================
    '                                                        SQL = "update emi_pembelian_loading_detail set Tot_Batch_Masuk=" & Ds3.Tables("MyTable").Rows(k).Item("Jmlh_Masuk_Hari_ini") & " + 1 "
    '                                                        SQL = SQL & "where No_Faktur='" & Ds.Tables("MyTable").Rows(i).Item("No_Pembelian_Loading") & "' and Urut_Oto='" & Ds.Tables("MyTable").Rows(i).Item("Urut_Loading") & "'"
    '                                                        ExecuteTrans(SQL)

    '                                                        Dim SupOrder As Integer = Val(Ds3.Tables("MyTable").Rows(k).Item("Jmlh_Masuk_Hari_ini")) + 1

    '                                                        batch = Generate_Batch_Bahan(SupplierKode, tanggalMasuk, bulanMasuk, tahunMasuk, SupOrder, expDate)
    '                                                        Qr = Generate_QR_Batch(barangKode, batch)


    '                                                    Next
    '                                                End If

    '                                            End Using
    '                                        Else
    '                                            If sudahCetak = True Then
    '                                                kodeUnikBerjalan = .Rows(j).Item("Kode_Unik_Berjalan")
    '                                                kodeUnikAsal = kodeUnikBerjalan
    '                                                Dim SupOrder As Integer = Val(.Rows(j).Item("Batch_Masuk"))

    '                                                batch = Generate_Batch_Bahan(SupplierKode, tanggalMasuk, bulanMasuk, tahunMasuk, SupOrder, expDate)
    '                                                Qr = Generate_QR_Batch(barangKode, batch)
    '                                            Else
    '                                                kodeUnikBerjalan = Generate_Random_Kode(10).ToUpper
    '                                                kodeUnikAsal = kodeUnikBerjalan
    '                                                Dim SupOrder As Integer = Val(.Rows(j).Item("Batch_Masuk"))

    '                                                batch = Generate_Batch_Bahan(SupplierKode, tanggalMasuk, bulanMasuk, tahunMasuk, SupOrder, expDate)
    '                                                Qr = Generate_QR_Batch(barangKode, batch)
    '                                            End If

    '                                        End If
    '                                    Next

    '                                Else
    '                                    Exit Sub
    '                                End If
    '                            End With
    '                        End Using



    '                        '1
    '                        Dim nama1 As String = .Rows(i).Item("kode_stock_owner") & "###" & .Rows(i).Item("no_faktur") & "###" & .Rows(i).Item("kode_barang") & "###" & .Rows(i).Item("nama")
    '                        Dim QR_Kode_Barang As String = ""



    '                        QR_Kode_Barang = .Rows(i).Item("kode_barang")
    '                        PictureBoxKdBrg.Image = Generate_QR_1(QR_Kode_Barang) 'CType(, Image)

    '                        Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, .Rows(i).Item("urut_oto") & "_barang.jpg")
    '                        If Not (System.IO.File.Exists(FileToSaveAs1)) Then
    '                            PictureBoxKdBrg.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
    '                        End If

    '                        fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
    '                        FileSize1 = fs1.Length
    '                        rawData1 = New Byte(FileSize1) {}
    '                        fs1.Read(rawData1, 0, FileSize1)
    '                        fs1.Close()
    '                        Cmd.Parameters.Add("@foto1" & .Rows(i).Item("urut_oto"), SqlDbType.Image).Value = rawData1

    '                        '2
    '                        Dim nama2 As String = .Rows(i).Item("kode_stock_owner") & "###" & .Rows(i).Item("no_sj") & "###" & .Rows(i).Item("no_plat")
    '                        Dim QR_Tracking_Barang As String = ""
    '                        QR_Tracking_Barang = .Rows(i).Item("no_sj") & " / " & .Rows(i).Item("no_plat")
    '                        PictureBoxTracking.Image = Generate_QR_2(QR_Tracking_Barang) 'CType(, Image)

    '                        Dim FileToSaveAs2 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, .Rows(i).Item("urut_oto") & "_tracking.jpg")
    '                        If Not (System.IO.File.Exists(FileToSaveAs2)) Then
    '                            PictureBoxTracking.Image.Save(FileToSaveAs2, System.Drawing.Imaging.ImageFormat.Jpeg)
    '                        End If

    '                        fs2 = New FileStream(FileToSaveAs2, FileMode.Open, FileAccess.Read)
    '                        FileSize2 = fs2.Length
    '                        rawData2 = New Byte(FileSize2) {}
    '                        fs2.Read(rawData2, 0, FileSize2)
    '                        fs2.Close()
    '                        Cmd.Parameters.Add("@foto2" & .Rows(i).Item("urut_oto"), SqlDbType.Image).Value = rawData2


    '                        '=================================
    '                        '=      INSERT TABEL CETAK       =
    '                        '=================================
    '                        SQL = "insert into Cetak_Barang_Masuk_Perpallet(kode_perusahaan, no_barang_masuk_per_pallet, [" & kolom_1 & "], [" & kolom_1 & "a], "
    '                        SQL = SQL & "[" & kolom_2 & "], [" & kolom_2 & "a], userid) values "
    '                        SQL = SQL & "('" & KodePerusahaan & "', '" & Lv_BM_PerPallet.FocusedItem.Text & "', "
    '                        SQL = SQL & "'" & nama1 & "', @foto1" & .Rows(i).Item("urut_oto") & ","
    '                        SQL = SQL & "'" & nama2 & "', @foto2" & .Rows(i).Item("urut_oto") & ", "
    '                        SQL = SQL & "'" & UserID & "')"
    '                        ExecuteTrans(SQL)

    '                        '''update
    '                        If Is2ndPrint = False Then
    '                            Dim kodeUnikBerjalan As String = Generate_Random_Kode(15)
    '                            Dim kodeUnikAsal As String = Generate_Random_Kode(15)

    '                            SQL = "update EMI_Barang_Masuk_Perpallet set Sdh_Cetak = 'Y', "
    '                            SQL = SQL & "batch_number='" & nama1 & "###" & kodeUnikBerjalan & "', "
    '                            SQL = SQL & "kode_unik_berjalan='" & kodeUnikBerjalan & "', kode_unik_asal='" & kodeUnikAsal & "' "
    '                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Lv_BM_PerPallet.FocusedItem.Text & "' "
    '                            'SQL = SQL & "and userid = '" & UserID & "' "
    '                            ExecuteTrans(SQL)
    '                        End If


    '                    Next

    '                Else
    '                    CloseConn()
    '                    MessageBox.Show("Data pembelian tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If

    '            End With
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try

    '    Try
    '        OpenConn()
    '        Dim CrDoc As New Object

    '        SQL = "select kode_perusahaan from Cetak_Barang_Masuk_Perpallet "
    '        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_barang_masuk_per_pallet = '" & Lv_BM_PerPallet.FocusedItem.Text & "' "
    '        SQL = SQL & "and userid = '" & UserID & "' "
    '        '''SQL = "select a.kode_perusahaan, a.userid, b.no_faktur, b.sdh_cetak "
    '        '''SQL = SQL & "from cetak_barang_masuk_Perpallet a, EMI_Barang_Masuk_Perpallet b "
    '        '''SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.no_barang_masuk_per_pallet = b.No_Faktur "
    '        '''SQL = SQL & "and b.Sdh_Cetak is null and a.no_barang_masuk_per_pallet = '" & Lv_BM_PerPallet.FocusedItem.Text & "'"
    '        '''SQL = SQL & "and a.userid = '" & UserID & "' "
    '        Using Ds = BindingTrans(SQL)
    '            If Ds.Tables("MyTable").Rows.Count <> 0 Then
    '                CrDoc = New BM_PerPallet
    '                With A_Place_For_Printing2
    '                    CrDoc.SetDataSource(Ds)
    '                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                    CrDoc.PrintOptions.PrinterName = ""
    '                    CrDoc.RecordSelectionFormula = "{EMI_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Barang_Masuk_Perpallet.No_Faktur} = '" & Lv_BM_PerPallet.FocusedItem.Text & "' and {EMI_Barang_Masuk_Perpallet.UserID} = '" & UserID & "' and IsNull({EMI_Barang_Masuk_Perpallet.Sdh_Cetak}) "
    '                    CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
    '                    .Text = "Barang Masuk Per Pallet"
    '                    .CrystalReportViewer1.ReportSource = CrDoc
    '                    .Refresh()
    '                    .Show()
    '                End With

    '                '''CrDoc.SetDataSource(Ds)
    '                '''CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                '''CrDoc.PrintOptions.PrinterName = ""
    '                '''Dim doctoprint As New System.Drawing.Printing.PrintDocument()
    '                '''doctoprint.PrinterSettings.PrinterName = ""
    '                '''A_Place_For_Printing2.CrystalReportViewer1.ReportSource = CrDoc
    '                '''A_Place_For_Printing2.Refresh()
    '                '''A_Place_For_Printing2.Show()
    '            End If
    '        End Using

    '        ''Using Ds = Binding("select * from EMI_Barang_Masuk_Perpallet where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Lv_BM_PerPallet.FocusedItem.Text & "'")
    '        ''    If Ds.Tables("MyTable").Rows.Count <> 0 Then
    '        ''        Dim CrDoc As New BM_PerPallet     'Nama file CR
    '        ''        With A_Place_For_Printing2
    '        ''            CrDoc.SetDataSource(Ds)
    '        ''            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '        ''            'CrDoc.PrintOptions.PrinterName = PrinterName
    '        ''            CrDoc.RecordSelectionFormula = "{EMI_Barang_Masuk_Perpallet.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Barang_Masuk_Perpallet.No_faktur} = '" & Lv_BM_PerPallet.FocusedItem.Text & "'"
    '        ''            CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
    '        ''            .Text = "Barang Masuk Per Pallet"
    '        ''            .CrystalReportViewer1.ReportSource = CrDoc
    '        ''            '.CrystalReportViewer1.DisplayGroupTree = False
    '        ''            .Refresh()
    '        ''            .Show()
    '        ''        End With
    '        ''    End If
    '        ''End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try

    '    'kosong()
    'End Sub

    Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If Lv_BM_PerPallet.Items.Count = 0 Or Lv_BM_PerPallet.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        'EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = Lv_BM_PerPallet.FocusedItem.Text
        'EMI_Barang_Masuk_Display_Rak.ShowDialog()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamTgl.CheckedChanged
        If Cb_ParamTgl.Checked Then
            Cmb_ParamTgl.Enabled = True : Dtp_Awal.Enabled = True : Dtp_Akhir.Enabled = True
            Cb_TransaksiHrIni.Checked = False
        Else
            Cmb_ParamTgl.Enabled = False : Dtp_Awal.Enabled = False : Dtp_Akhir.Enabled = False
            Cmb_ParamTgl.SelectedIndex = -1 : Dtp_Awal.Value = Now.Date : Dtp_Akhir.Value = Now.Date
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamLain.CheckedChanged
        If Cb_ParamLain.Checked Then
            Cmb_ParamLain.Enabled = True : Txt_ParamLain.Enabled = True
        Else
            Cmb_ParamLain.Enabled = False : Txt_ParamLain.Enabled = False
            Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamLain.Text = ""
        End If
    End Sub

    ''Dim arrcari As New ArrayList
    ''Dim Jenis = "Master_Jenis_Hewan"
    ''Private Sub kosong()
    ''    TextBox1.Text = ""
    ''    TextBox2.Text = ""

    ''    ComboBox1.Items.Clear() : arrcari.Clear()
    ''    ComboBox1.Items.Add(Base_Language.Lang_Jenis_Hewan_Kode) : arrcari.Add("kode_jenis_hewan")
    ''    ComboBox1.Items.Add(Base_Language.Lang_Jenis_Hewan_Keterangan) : arrcari.Add("keterangan")
    ''    TextBox3.Text = ""

    ''    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
    ''    Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
    ''    Btn_Cari.Text = Base_Language.Lang_Global_Cari
    ''    Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
    ''    Btn_Simpan.Tag = "&Simpan"
    ''    Btn_Hapus.Enabled = False

    ''End Sub

    ''Private Sub Cari(ByVal semua As String)
    ''    Try

    ''        OpenConn()

    ''        ListView1.Items.Clear()
    ''        SQL = "Select kode_jenis_hewan, keterangan From emi_jenis_hewan where kode_perusahaan = '" & KodePerusahaan & "' "
    ''        If semua = "T" Then
    ''            SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
    ''            SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
    ''        Else
    ''            SQL = SQL & "order by nama"
    ''        End If
    ''        Using dr = OpenTrans(SQL)
    ''            Do While dr.Read
    ''                Dim Lvw As ListViewItem
    ''                Lvw = ListView1.Items.Add(dr("kode_jenis_hewan"))
    ''                Lvw.SubItems.Add(dr("keterangan"))
    ''            Loop
    ''        End Using

    ''        CloseConn()

    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try
    ''End Sub
    ''Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    ''    My.Application.ChangeCulture("en-us")
    ''    My.Application.ChangeUICulture("en-us")
    ''End Sub

    ''Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ''    My.Application.ChangeCulture("en-us")
    ''    My.Application.ChangeUICulture("en-us")

    ''    Try
    ''        OpenConn()

    ''        Base_Language.Get_Languages_Global(Bahasa_Pilihan)

    ''        Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

    ''        Label1.Text = Base_Language.Lang_Jenis_Hewan_Judul
    ''        Label2.Text = Base_Language.Lang_Jenis_Hewan_Kode
    ''        Label3.Text = Base_Language.Lang_Jenis_Hewan_Keterangan
    ''        Label4.Text = Base_Language.Lang_Jenis_Hewan_Kolom

    ''        ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Kode, 150, HorizontalAlignment.Left)
    ''        ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Keterangan, 725, HorizontalAlignment.Left)
    ''        ListView1.View = View.Details

    ''        kosong()

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub

    ''    End Try


    ''End Sub

    ''Private Sub TextBox1_Leave(sender As Object, e As EventArgs)
    ''    If TextBox1.Text.Trim.Length = 0 Then Exit Sub

    ''    Try

    ''        OpenConn()

    ''        SQL = "Select kode_jenis_hewan, keterangan From emi_jenis_hewan Where "
    ''        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
    ''        SQL = SQL & "kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    ''        Using Dr = OpenTrans(SQL)
    ''            If Dr.Read Then
    ''                TextBox1.Text = Dr("kode_jenis_hewan")
    ''                TextBox2.Text = Dr("keterangan")

    ''                Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
    ''                Btn_Simpan.Tag = "&Update"
    ''            Else
    ''                TextBox2.Text = ""

    ''                Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = False
    ''                Btn_Simpan.Tag = "&Simpan"
    ''            End If
    ''        End Using

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try
    ''End Sub

    ''Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs)
    ''    If TextBox1.Text.Trim.Length = 0 Then
    ''        MessageBox.Show(Base_Language.Lang_Jenis_Hewan_Error_Kode, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        TextBox1.Focus() : Exit Sub
    ''    ElseIf TextBox2.Text.Trim.Length = 0 Then
    ''        MessageBox.Show(Base_Language.Lang_Jenis_Hewan_Error_Nama, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        TextBox2.Focus() : Exit Sub
    ''    End If

    ''    Try

    ''        OpenConn()

    ''        Cmd.Transaction = Cn.BeginTransaction

    ''        If Btn_Simpan.Tag = "&Simpan" Then
    ''            SQL = "Insert Into emi_jenis_hewan(Kode_Perusahaan, kode_jenis_hewan, keterangan) "
    ''            SQL = SQL & "Values('" & KodePerusahaan & "', "
    ''            SQL = SQL & "'" & TextBox1.Text.Trim & "', '" & TextBox2.Text.Trim & "')"
    ''            ExecuteTrans(SQL)
    ''        Else
    ''            SQL = "Update emi_jenis_hewan Set keterangan = '" & TextBox2.Text.Trim & "' "
    ''            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    ''            ExecuteTrans(SQL)
    ''        End If

    ''        Cmd.Transaction.Commit()

    ''        CloseConn()

    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try

    ''    kosong()
    ''    TextBox1.Focus()
    ''End Sub

    ''Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs)
    ''    Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    ''    If Hapus1 = vbYes Then

    ''        Try

    ''            OpenConn()

    ''            Cmd.Transaction = Cn.BeginTransaction

    ''            SQL = "Delete From emi_jenis_hewan where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    ''            ExecuteTrans(SQL)

    ''            Cmd.Transaction.Commit()

    ''            CloseConn()
    ''        Catch ex As Exception
    ''            CloseTrans()
    ''            CloseConn()
    ''            MessageBox.Show(ex.Message)
    ''            Exit Sub
    ''        End Try

    ''    Else
    ''        MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''    End If

    ''    kosong()
    ''    TextBox1.Focus()
    ''End Sub

    ''Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
    ''    kosong()
    ''End Sub

    ''Private Sub Btn_Cari_Click(sender As Object, e As EventArgs)
    ''    If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
    ''    If TextBox3.Text.Trim.Length = 0 Then Exit Sub

    ''    Cari("T")
    ''End Sub

    ''Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''    If e.KeyChar = Chr(13) Then TextBox2.Focus()
    ''End Sub

    ''Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''    If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    ''End Sub

    ''Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    ''End Sub

    ''Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''    If e.KeyChar = Chr(13) Then TextBox3.Focus()
    ''End Sub

    ''Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''    If e.KeyChar = Chr(13) Then Btn_Cari_Click(TextBox3, e)
    ''End Sub

    ''Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs)

    ''End Sub

    'FUNCTION UTILITY
    Private Function Generate_Random_Kode(ByVal length As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim result As New StringBuilder()

        For i As Integer = 1 To length
            Dim index As Integer = random.Next(0, chars.Length)
            result.Append(chars(index))
        Next

        Return result.ToString()
    End Function

End Class