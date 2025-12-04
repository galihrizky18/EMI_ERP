Imports System.IO

Public Class EMI_Display_Split_Stock

    Dim arrFilterLokasi, arrFilterTgl, arrFilterLain As New ArrayList

    Dim LvP_KdTransfer, LvP_JnsTransfer, LvP_LokasiAwal, LvP_LokasiTujuan, LvP_Keterangan, LvP_Tgl, LvP_Jam, LvP_User As String

    Dim itemP_KdTransfer As Integer = 0
    Dim itemP_JnsTransfer As Integer = 1
    Dim itemP_LokasiAwal As Integer = 2
    Dim itemP_LokasiAkhir As Integer = 3
    Dim itemP_Keterangan As Integer = 4
    Dim itemP_Tgl As Integer = 5
    Dim itemP_Jam As Integer = 6
    Dim itemP_User As Integer = 7

    Dim LvC_RakAwal, LvC_RakTujuan, LvC_KdBarang, LvC_NmBarang, LvC_Total, LvC_TotBags, LvC_BeratBagi, LvC_Satuan, LvC_IdWmsAwal, LvC_IdWmsTujuan, LvC_Urut As String

    Dim itemC_RakAwal As Integer = 0
    Dim itemC_RakTujuan As Integer = 1
    Dim itemC_KdBarang As Integer = 2
    Dim itemC_NmBarang As Integer = 3
    Dim itemC_Total As Integer = 4
    Dim itemC_TotBags As Integer = 5
    Dim itemC_BeratBagi As Integer = 6
    Dim itemC_Satuan As Integer = 7
    Dim itemC_IdWmsAwal As Integer = 8
    Dim itemC_IdWmsTujuan As Integer = 9
    Dim itemC_Urut As Integer = 10

    Dim LvDP_QrAkhir, LvDP_NoPallet, LvDP_Jumlah, LvDP_Satuan, LvDP_ID As String

    Dim itemDP_QrAkhir As Integer = 0
    Dim itemDP_NoPallet As Integer = 1
    Dim itemDP_Jumlah As Integer = 2
    Dim itemDP_Satuan As Integer = 3
    Dim itemDP_ID As Integer = 4


    Dim Random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Private Sub CetakToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakToolStripMenuItem.Click
        If Lv_DetailPallet.Items.Count = 0 Or Lv_DetailPallet.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim Kode_unik_print As String

        Dim selectedIndex As Integer = Lv_DetailPallet.FocusedItem.Index
        GetDataLvDP(selectedIndex)

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '=============================================
            '=     CEK APAKAH DATA SUDAH DI BATALKAN     =
            '=============================================
            SQL = "select a.Kode_Perusahaan from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c, Tf_Stock_QC_det2 d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            SQL = SQL & "and a.Status ='Y' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and d.Urut_Oto = '" & LvDP_ID & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Cetak Ulang Tidak Dapat Dilakukan karena No Transaksi Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim kodeBarang As String
            Dim QrLama As String
            Dim newKodeUnikBerjalan As String
            Dim namaBarang As String
            Dim expDate As String
            Dim batchLama As String
            Dim Urut As String = "1"
            Kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
            SQL = "select a.No_Faktur, b.kode_barang,  "

            SQL = SQL & "isnull(( select z.Qr_Code from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Tujuan = z.Kode_Stock_Owner and d.Serial_Number = z.Serial_Number ), '-') as QR_Akhir, "

            SQL = SQL & "isnull(( select z.Kode_Unik_Berjalan from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Tujuan = z.Kode_Stock_Owner and d.Serial_Number = z.Serial_Number ), '-') as Kode_Unik_Berjalan, "

            SQL = SQL & "isnull(( select z.Qr_Code from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Tujuan = z.Kode_Stock_Owner and d.Serial_Number = z.Serial_Number ), '-') as QR_Akhir, "

            SQL = SQL & "isnull(( select z.Tgl_Expired from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Tujuan = z.Kode_Stock_Owner and d.Serial_Number = z.Serial_Number ), NULL) as Tgl_Expired, "

            SQL = SQL & "isnull(( select z.Tgl_Produksi from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Tujuan = z.Kode_Stock_Owner and d.Serial_Number = z.Serial_Number ), NULL) as Tgl_Produksi, "

            SQL = SQL & "isnull(( select z.Batch_Number from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Tujuan = z.Kode_Stock_Owner and d.Serial_Number = z.Serial_Number ), '-') as Batch_Number, "

            SQL = SQL & "isnull(( select top(1) z.nama from Barang z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Tujuan = z.Kode_Stock_Owner and b.kode_barang = z.kode_barang ), '-') as nama "

            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c, Tf_Stock_QC_det2 d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and d.Urut_Oto = '" & LvDP_ID & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    kodeBarang = dr("kode_barang")
                    QrLama = dr("QR_Akhir")
                    newKodeUnikBerjalan = dr("Kode_Unik_Berjalan")
                    namaBarang = dr("nama")
                    expDate = Format(dr("Tgl_Expired"), "yyyy-MM-dd")
                    batchLama = dr("Batch_Number")
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data Kosong")
                    Exit Sub
                End If
            End Using

            '=====================================
            '=       GENERATE BARCODE BARU       =
            '=====================================

            Dim fullNewQr As String = QrLama & "-" & newKodeUnikBerjalan

            Barcode.Image = Generate_QR(fullNewQr)

            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStockQC" & Kode_unik_print & ".jpg")
            'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
            Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
            'End If

            fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
            FileSize1 = fs1.Length
            rawData1 = New Byte(FileSize1) {}
            fs1.Read(rawData1, 0, FileSize1)
            fs1.Close()
            Cmd.Parameters.Add($"@newBarcode{Urut}", SqlDbType.Image).Value = rawData1

            '===================================
            '=       INSERT BARCODE BARU       =
            ''===================================
            Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)
            SQL = "delete from Cetak_TransferStock_QC where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            ExecuteTrans(SQL)

            SQL = "insert into Cetak_TransferStock_QC (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & kodeBarang & "', @newBarcode" & Urut & ", '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
            SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Kode_unik_print & "' ) "
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

        '=================
        '=     CETAK     =
        '=================
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            '=========================
            '=     CETAK BARCODE     =
            '=========================
            Dim kertasBarcodeBesar As String = "BarcodeFG"
            SQL = "select Kode_Perusahaan, QrUtuh from Cetak_TransferStock_QC where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & Kode_unik_print & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1

                        '------------------------
                        CrDoc = New NewBarcodeTransferStockQC
                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{Cetak_TransferStock_QC.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock_QC.kode_unik_print} = '" & Kode_unik_print & "' and {Cetak_TransferStock_QC.QrUtuh} = '" & Ds.Tables("MyTable").Rows(i).Item("QrUtuh") & "'"
                        CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        Dim rawKind As Integer
                        Dim isPaperFound As Boolean = False
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcodeBesar Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                isPaperFound = True
                                Exit For
                            End If
                        Next

                        If Not isPaperFound Then
                            'CloseConn()
                            MessageBox.Show("Kertas Tidak DiTemukan, Kertas di set ke default", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            'Exit Sub
                        End If

                        CrDoc.PrintToPrinter(1, False, 1, 2500)
                    Next

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub EMI_Display_Split_Barang_BackgroundImageChanged(sender As Object, e As EventArgs) Handles Me.BackgroundImageChanged
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Display_Split_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        InitialListView()
        Kosong()

    End Sub

    Private Sub Kosong()

        Try
            OpenConn()

            Chk_1.Checked = False
            Chk_2.Checked = False
            Chk_3.Checked = False

            Txt_ParamLainValue.Text = ""
            DateTimePicker1.Value = Date.Now
            DateTimePicker2.Value = Date.Now

            Lv_Parent.Items.Clear()
            Lv_Child.Items.Clear()
            Lv_DetailPallet.Items.Clear()

            Cmb_1.Items.Clear() : arrFilterLokasi.Clear()
            SQL = "select kode_stock_owner, keterangan from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Cmb_1.Items.Add("--- Seluruh ---") : arrFilterLokasi.Add("SELURUH")
                Do While Dr.Read
                    Cmb_1.Items.Add(Dr("keterangan")) : arrFilterLokasi.Add(Dr("kode_stock_owner"))
                Loop
                Cmb_1.SelectedIndex = 0
            End Using

            Cmb_2.Items.Clear() : arrFilterTgl.Clear()
            Cmb_2.Items.Add("Tanggal") : arrFilterTgl.Add("a.Tanggal")

            Cmb_3.Items.Clear() : arrFilterLain.Clear()
            Cmb_3.Items.Add("No Faktur") : arrFilterLain.Add("a.No_Faktur")
            Cmb_3.Items.Add("Jenis Transfer") : arrFilterLain.Add("a.Jenis_Transfer")
            Cmb_3.Items.Add("Lokasi Awal") : arrFilterLain.Add("a.SO_Awal")
            Cmb_3.Items.Add("Lokasi Akhir") : arrFilterLain.Add("a.SO_Tujuan")
            Cmb_3.Items.Add("User") : arrFilterLain.Add("a.UserID")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_1.Focus()

    End Sub

    Private Sub InitialListView()

        Lv_Parent.Columns.Clear()
        Lv_Parent.Columns.Add("Kode Transfer", 150, HorizontalAlignment.Left)
        Lv_Parent.Columns.Add("Jenis Transfer", 120, HorizontalAlignment.Left)
        Lv_Parent.Columns.Add("Lokasi Awal", 160, HorizontalAlignment.Center)
        Lv_Parent.Columns.Add("Lokasi Akhir", 160, HorizontalAlignment.Center)
        Lv_Parent.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Parent.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        Lv_Parent.Columns.Add("Jam", 80, HorizontalAlignment.Center)
        Lv_Parent.Columns.Add("User", 100, HorizontalAlignment.Center)
        Lv_Parent.View = View.Details

        Lv_Child.Columns.Clear()
        Lv_Child.Columns.Add("Rak Awal", 120, HorizontalAlignment.Left)
        Lv_Child.Columns.Add("Rak Tujuan", 120, HorizontalAlignment.Left)
        Lv_Child.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
        Lv_Child.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_Child.Columns.Add("Total", 100, HorizontalAlignment.Right)
        Lv_Child.Columns.Add("Total Bags", 100, HorizontalAlignment.Right)
        Lv_Child.Columns.Add("Berat Bagi", 100, HorizontalAlignment.Right)
        Lv_Child.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        'Hide
        Lv_Child.Columns.Add("id_wmsAwal", 0, HorizontalAlignment.Center)
        Lv_Child.Columns.Add("id_wmsTujuan", 0, HorizontalAlignment.Center)
        Lv_Child.Columns.Add("urut  ", 0, HorizontalAlignment.Center)
        Lv_Child.View = View.Details

        Lv_DetailPallet.Columns.Clear()
        Lv_DetailPallet.Columns.Add("QR Akhir", 150, HorizontalAlignment.Left)
        Lv_DetailPallet.Columns.Add("No Pallet", 100, HorizontalAlignment.Center)
        Lv_DetailPallet.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
        Lv_DetailPallet.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_DetailPallet.Columns.Add("ID", 0, HorizontalAlignment.Left)
        Lv_DetailPallet.View = View.Details

    End Sub

    Private Sub GetDataLv_Parent(ByVal index As Integer)
        LvP_KdTransfer = Lv_Parent.Items(index).SubItems(itemP_KdTransfer).Text
        LvP_JnsTransfer = Lv_Parent.Items(index).SubItems(itemP_JnsTransfer).Text
        LvP_LokasiAwal = Lv_Parent.Items(index).SubItems(itemP_LokasiAwal).Text
        LvP_LokasiTujuan = Lv_Parent.Items(index).SubItems(itemP_LokasiAkhir).Text
        LvP_Keterangan = Lv_Parent.Items(index).SubItems(itemP_Keterangan).Text
        LvP_Tgl = Lv_Parent.Items(index).SubItems(itemP_Tgl).Text
        LvP_Jam = Lv_Parent.Items(index).SubItems(itemP_Jam).Text
        LvP_User = Lv_Parent.Items(index).SubItems(itemP_User).Text
    End Sub

    Private Sub GetDataLvChild(ByVal index As Integer)

        LvC_RakAwal = Lv_Child.Items(index).SubItems(itemC_RakAwal).Text
        LvC_RakTujuan = Lv_Child.Items(index).SubItems(itemC_RakTujuan).Text
        LvC_KdBarang = Lv_Child.Items(index).SubItems(itemC_KdBarang).Text
        LvC_NmBarang = Lv_Child.Items(index).SubItems(itemC_NmBarang).Text
        LvC_Total = Lv_Child.Items(index).SubItems(itemC_Total).Text
        LvC_TotBags = Lv_Child.Items(index).SubItems(itemC_TotBags).Text
        LvC_BeratBagi = Lv_Child.Items(index).SubItems(itemC_BeratBagi).Text
        LvC_Satuan = Lv_Child.Items(index).SubItems(itemC_Satuan).Text
        LvC_IdWmsAwal = Lv_Child.Items(index).SubItems(itemC_IdWmsAwal).Text
        LvC_IdWmsTujuan = Lv_Child.Items(index).SubItems(itemC_IdWmsTujuan).Text
        LvC_Urut = Lv_Child.Items(index).SubItems(itemC_Urut).Text

    End Sub

    Private Sub GetDataLvDP(ByVal index As Integer)
        LvDP_QrAkhir = Lv_DetailPallet.Items(index).SubItems(itemDP_QrAkhir).Text
        LvDP_NoPallet = Lv_DetailPallet.Items(index).SubItems(itemDP_NoPallet).Text
        LvDP_Jumlah = Lv_DetailPallet.Items(index).SubItems(itemDP_Jumlah).Text
        LvDP_Satuan = Lv_DetailPallet.Items(index).SubItems(itemDP_Satuan).Text
        LvDP_ID = Lv_DetailPallet.Items(index).SubItems(itemDP_ID).Text

    End Sub

    Private Sub Chk_1_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_1.CheckedChanged
        If Chk_1.Checked = True Then
            Chk_2.Checked = False
            Cmb_2.SelectedIndex = -1 : Cmb_2.Text = ""
            DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
            Cmb_2.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            BtnMasuk_Cari_Click(Chk_2, e)
        Else
            Cmb_2.SelectedIndex = -1 : Cmb_2.Text = ""
            DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
            Cmb_2.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
        End If
    End Sub

    Private Sub Chk_2_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_2.CheckedChanged
        If Chk_2.Checked Then
            Chk_1.Checked = False
            Cmb_2.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Cmb_2.SelectedIndex = -1 : Cmb_2.Text = ""
            DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
        Else
            Cmb_2.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_2.SelectedIndex = -1 : Cmb_2.Text = ""
            DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
        End If
    End Sub

    Private Sub Chk_3_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_3.CheckedChanged
        If Chk_3.Checked = True Then
            Cmb_3.Enabled = True : Txt_ParamLainValue.Enabled = True
            Cmb_3.SelectedIndex = -1 : Cmb_3.Text = "" : Txt_ParamLainValue.Text = ""
        Else
            Cmb_3.Enabled = False : Txt_ParamLainValue.Enabled = False
            Cmb_3.SelectedIndex = -1 : Cmb_3.Text = "" : Txt_ParamLainValue.Text = ""
        End If
    End Sub

    Private Sub BtnMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnMasuk_Cari.Click
        If Chk_1.Checked = False And Chk_2.Checked = False And Chk_3.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            Chk_1.Focus() : Exit Sub
        ElseIf Cmb_1.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi Harus harus diisi!", Judul)
            Cmb_1.Focus() : Exit Sub
        End If

        If Chk_2.Checked = True Then
            If Not Cmb_2.SelectedIndex = -1 Then
                If DateTimePicker1.Value > DateTimePicker2.Value Then
                    MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                    DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                    Exit Sub
                End If
            Else
                MessageBox.Show("Pilih Dahulu Tanggal yang akan Di Filter!", Judul)
                Cmb_2.Focus() : Exit Sub
            End If
        End If

        If Chk_3.Checked = True Then
            If Cmb_3.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                Cmb_3.Focus() : Exit Sub
            ElseIf Txt_ParamLainValue.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                Txt_ParamLainValue.Focus() : Exit Sub
            End If

        End If

        Try
            OpenConn()

            Lv_Child.Items.Clear() : Lv_DetailPallet.Items.Clear()

            Lv_Parent.Items.Clear()
            SQL = "select a.No_Faktur, a.Jenis_Transfer, a.SO_Awal, a.SO_Tujuan, a.Keterangan, a.Tanggal, a.Jam, a.UserID, a.status "
            SQL = SQL & "from Tf_Stock_QC a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.status is null "
            If Cmb_1.SelectedIndex <> 0 Then
                SQL = SQL & "and a.lokasi = '" & arrFilterLokasi(Cmb_1.SelectedIndex) & "' "
            End If
            If Chk_1.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal Between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(DateAdd(DateInterval.Day, 1, Now), "yyyy-MM-dd") & "' "
            End If
            If Chk_2.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilterTgl(Cmb_2.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If
            If Chk_3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilterLain(Cmb_3.SelectedIndex) & " like '%" & Trim(Txt_ParamLainValue.Text) & "%' "
            End If
            SQL = SQL & "order by a.Tanggal, a.Jam, a.SO_Awal, a.Jenis_Transfer "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Parent.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Jenis_Transfer"))
                    Lv.SubItems.Add(Dr("SO_Awal"))
                    Lv.SubItems.Add(Dr("SO_Tujuan"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("UserID"))

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
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

    Private Sub Lv_Parent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Parent.SelectedIndexChanged

        If Lv_Parent.Items.Count = 0 OrElse Lv_Parent.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()

            Dim SelectedIndex As Integer = Lv_Parent.FocusedItem.Index
            GetDataLv_Parent(SelectedIndex)

            Lv_Child.Items.Clear()
            SQL = "select b.Kode_Barang, d.Nama as NamaBarang, b.Total, b.Satuan, b.Total_Bags,  "
            SQL = SQL & "(dbo.Ubah_Satuan(a.Kode_Perusahaan, 'masa', b.Kode_Barang, c.Satuan_Barang, b.Satuan, c.Berat_Bagi)) as Berat_Bagi, "
            SQL = SQL & "b.Urut_Oto, c.Id_Wms_Awal, e.Keterangan as Warehouse_Awal, c.No_Pallet_Awal, c.Id_Wms_Tujuan, f.Keterangan as Warehouse_Tujuan, c.No_Pallet_Tujuan "
            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c, barang d, View_Warehouse_Position e, View_Warehouse_Position f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan  "
            SQL = SQL & "and c.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.SO_Awal = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Id_Wms_Awal = e.Id_WMS_Warehouse_Position "
            SQL = SQL & "and c.Id_Wms_Tujuan = f.Id_WMS_Warehouse_Position "
            'SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & LvP_KdTransfer.Trim & "'"
            SQL = SQL & "order by b.Kode_Barang, b.Urut_Oto "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Child.Items.Add(Dr("Warehouse_Awal"))
                    Lv.SubItems.Add(Dr("Warehouse_Tujuan"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("NamaBarang"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Total"))), "N4"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Total_Bags"))), "N2"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Berat_Bagi"))), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Id_Wms_Awal"))
                    Lv.SubItems.Add(Dr("Id_Wms_Tujuan"))
                    Lv.SubItems.Add(Dr("Urut_Oto"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Child_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Child.SelectedIndexChanged
        If Lv_Child.Items.Count = 0 OrElse Lv_Child.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()

            Dim selectedIndex As Integer = Lv_Child.FocusedItem.Index
            GetDataLvChild(selectedIndex)

            Lv_DetailPallet.Items.Clear()
            SQL = "select a.No_Faktur, d.urut_oto,  "
            SQL = SQL & "isnull(( select z.Qr_Code + '-' + z.Kode_Unik_Berjalan from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Awal = z.Kode_Stock_Owner and c.Serial_Number_Awal= z.Serial_Number ), '-') as QR_Awal, "

            SQL = SQL & "isnull(( select z.Qr_Code + '-' + z.Kode_Unik_Berjalan from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Tujuan = z.Kode_Stock_Owner and d.Serial_Number = z.Serial_Number ), '-') as QR_Akhir, "

            SQL = SQL & "d.No_Pallet, b.Satuan, "
            SQL = SQL & "(dbo.Ubah_Satuan(a.Kode_Perusahaan, 'masa', b.Kode_Barang, c.Satuan_Barang, b.Satuan, d.Jumlah)) as Jumlah "

            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c, Tf_Stock_QC_det2 d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            'SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & LvP_KdTransfer & "' "
            SQL = SQL & "and b.Urut_Oto = '" & LvC_Urut & "' "
            SQL = SQL & "order by d.Urut_Oto "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DetailPallet.Items.Add(Dr("QR_Akhir"))
                    Lv.SubItems.Add(Dr("No_Pallet"))
                    Lv.SubItems.Add(Dr("Jumlah"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("urut_oto"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    '===============================================================================================================================================
    '=     MENU STRIP
    '===============================================================================================================================================

    Private Sub SalinNoTransaksiToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles SalinNoTransaksiToolStripMenuItem.Click
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.SelectedItems.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no transfer yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Parent.FocusedItem.Text)
    End Sub
    Private Sub BatalSplitStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalSplitStockToolStripMenuItem.Click
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem.Index = -1 Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Split Stock"
            Dim NoTransfer As String = Lv_Parent.FocusedItem.Text

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Split_Stock") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk " & JudulNotif, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan No Transfer Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If


            '===================================================
            '=     CEK APAKAH NO TRANSFER SUDAH DIBATALKAN     =
            '===================================================
            SQL = "select Kode_Perusahaan from Tf_Stock_QC  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoTransfer & "' and status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(JudulNotif & " Tidak dapat Dilakukan karena No Transfer ini Sudah Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '========================================================
            '=     CEK APAKAH DATA SUDAH DI VALIDASI SELURUHNYA     =
            '========================================================
            SQL = "select a.Kode_Perusahaan from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and c.Selesai is null "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(JudulNotif & " Tidak dapat Dilakukan karena Barang Pada No Faktur Ini Belum Di Validasi Sepenuhnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '============================================
            '=     CEK APAKAH BARANG SUDAH DI PAKAI     =
            '============================================
            SQL = "select a.Kode_Perusahaan, a.No_Faktur, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang, d.Jumlah, "
            SQL = SQL & "ISNULL(( select z.Jumlah from Barang_SN z where d.kode_perusahaan = z.kode_perusahaan and d.Serial_Number = z.Serial_Number ), NULL) as Jumlah_SN "
            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c, Tf_Stock_QC_det2 d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            If .Rows(i).Item("Jumlah") <> .Rows(i).Item("Jumlah_SN") Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show(JudulNotif & " tidak dapat dilakukan karena Kode Barang : " & .Rows(i).Item("Kode_Barang") & " Sudah Digunakan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Split Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using


            '========================================
            '=     CEK APAKAH SUDAH TUTUP SALDO     =
            '========================================
            Dim HasData As Boolean = False
            Dim TglSplit As DateTime
            SQL = "select Tanggal from Tf_Stock_QC where status is null and Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoTransfer & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    HasData = True
                    TglSplit = Dr("Tanggal")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Split Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If HasData Then
                If CekSudahTutupSaldo(TglSplit) = "Y" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(JudulNotif & " tidak dapat dilakukan karena No Transaksi Sudah Tutup Saldo", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If

            '=====================
            '=     CEK BULAN     =
            '=====================
            If Not tgl_skg.Month = TglSplit.Month Then
                CloseTrans()
                CloseConn()
                MessageBox.Show(JudulNotif & " tidak dapat dilakukan karena Sudah Melewati Bulan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '====================================
            '=    CEK APAKAH DATA SEIMBANG     =
            '====================================
            SQL = "select a.No_Faktur, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang "
            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & .Rows(i).Item("SO_Tujuan") & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan, Data Tidak Seimbang . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Transfer Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '=========================
            '=     ROLLBACK DATA     =
            '=========================
            SQL = "select a.No_Faktur, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang, c.Serial_Number_Awal, c.Kode_Voucher, c.Urut_Oto, c.Jumlah_Bags "
            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan  "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.Status is null and c.Selesai = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim UrutDet As String = .Rows(i).Item("Urut_Oto")
                            Dim JumlahRollBack As Double = 0
                            Dim JumlahRollBackBags As Double = .Rows(i).Item("Jumlah_Bags")

                            '===============================
                            '=     ROLLBACK DATA DET 2     =
                            '===============================
                            SQL = "select a.No_Faktur, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang, c.Serial_Number_Awal, d.Serial_Number as Serial_Number_Tujuan, d.Jumlah "
                            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c, Tf_Stock_QC_det2 d "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
                            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                            SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
                            SQL = SQL & "and a.Status is null and c.Selesai = 'Y' "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
                            SQL = SQL & "and d.Urut_Det = '" & UrutDet & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1


                                        Dim Jumlah_Kecil As Double = Ds1.Tables("MyTable").Rows(j).Item("Jumlah")
                                        Dim KdSo_Awal As String = Ds1.Tables("MyTable").Rows(j).Item("SO_Awal")
                                        Dim KdSo_Tujuan As String = Ds1.Tables("MyTable").Rows(j).Item("SO_Tujuan")
                                        Dim KdBarang As String = Ds1.Tables("MyTable").Rows(j).Item("Kode_Barang")
                                        Dim SN_Awal As String = Ds1.Tables("MyTable").Rows(j).Item("Serial_Number_Awal")
                                        Dim SN_Tujuan As String = Ds1.Tables("MyTable").Rows(j).Item("Serial_Number_Tujuan")

                                        JumlahRollBack += Jumlah_Kecil
                                        'JumlahRollBackBags += 1

                                        '=============================
                                        '=     PENGURANGAN STOCK     =
                                        '=============================

                                        'PENGURANGAN BARANG SN
                                        SQL = "select Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdSo_Tujuan & "' "
                                        SQL = SQL & "and Kode_Barang = '" & KdBarang & "' and Serial_Number = '" & SN_Tujuan & "' "
                                        Using Ds2 = BindingTrans(SQL)
                                            If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                                If Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("Jumlah"))) < Jumlah_Kecil Then
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Terjadi Kesalahan Saat Rollback Data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                Else

                                                    SQL = "update Barang_SN set Jumlah = jumlah - " & Jumlah_Kecil & ", Jumlah_Bags = Jumlah_Bags - 1 "
                                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdSo_Tujuan & "' and Kode_Barang = '" & KdBarang & "' and Serial_Number = '" & SN_Tujuan & "' "
                                                    ExecuteTrans(SQL)

                                                End If
                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End Using

                                        'PENGURANGAN BARANG
                                        SQL = "select round(good_stock, 4) as good_stock from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & KdSo_Tujuan & "'  and kode_barang = '" & KdBarang & "' "
                                        Using Ds2 = BindingTrans(SQL)
                                            If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                                If Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("good_stock"))) < Jumlah_Kecil Then
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Terjadi Kesalahan Saat Rollback Data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                Else

                                                    SQL = "update Barang set good_stock = good_stock - " & Jumlah_Kecil & ", Jumlah_Bags = Jumlah_Bags - 1 "
                                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & KdSo_Tujuan & "'  and kode_barang = '" & KdBarang & "' "
                                                    ExecuteTrans(SQL)

                                                End If
                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End Using

                                        '====================================
                                        '=       CEK KESESUAIAN STOCK       =
                                        '====================================
                                        SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                        SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                        SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                        SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                        SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                        SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                        SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo_Tujuan & "' "
                                        SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                        SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                        Using Ds2 = BindingTrans(SQL)
                                            If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                                If Ds2.Tables("MyTable").Rows(0).Item("good_stock") <> Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End Using


                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Split Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            '=============================
                            '=     PENAMBAHAN STOCK     =
                            '=============================

                            'PENAMBAHAN BARANG SN
                            SQL = "select Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & .Rows(i).Item("SO_Awal") & "' "
                            SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "update Barang_SN set Jumlah = jumlah + " & JumlahRollBack & ", Jumlah_Bags = Jumlah_Bags + " & JumlahRollBackBags & " "
                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & .Rows(i).Item("SO_Awal") & "' "
                                    SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "' "
                                    ExecuteTrans(SQL)

                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            'PENAMBAHAN BARANG
                            SQL = "select good_stock from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & .Rows(i).Item("SO_Awal") & "'  and kode_barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "update Barang set good_stock = good_stock + " & JumlahRollBack & ", Jumlah_Bags = Jumlah_Bags + " & JumlahRollBackBags & " "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & .Rows(i).Item("SO_Awal") & "'  and kode_barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                    ExecuteTrans(SQL)

                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & .Rows(i).Item("SO_Awal") & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds2.Tables("MyTable").Rows(0).Item("good_stock") <> Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using



                            '===========================
                            '=     ROLLBACK JURNAL     =
                            '===========================
                            SQL = "select Kode_Perusahaan from Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & .Rows(i).Item("Kode_Voucher") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "delete Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & .Rows(i).Item("Kode_Voucher") & "' "
                                    ExecuteTrans(SQL)
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Jurnal Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                        Next

                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Split Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using


            '========================
            '=     UPDATE SPLIT     =
            '========================
            SQL = "select Kode_Perusahaan from Tf_Stock_QC where Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and No_Faktur = '" & NoTransfer & "' and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = "update Tf_Stock_QC set Status = 'Y', UserID_Batal = '" & UserID & "', Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoTransfer & "' and status is null"
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Transfer Stock Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Split Stock Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnMasuk_Cari_Click(e, New EventArgs)

    End Sub


    '===============================================================================================================================================
    '=     HANDLE KEYPRESS
    '===============================================================================================================================================
    Private Sub Cmb_1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_1.KeyPress
        If e.KeyChar = Chr(13) Then Chk_1.Focus()
    End Sub
    Private Sub Chk_1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_1.KeyPress
        If e.KeyChar = Chr(13) Then Chk_2.Focus()
    End Sub
    Private Sub Chk_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_2.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_2.Checked Then
                Cmb_2.DroppedDown = True
                Cmb_2.Focus()
            Else
                Chk_3.Focus()
            End If
        End If
    End Sub
    Private Sub Cmb_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_2.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub
    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub
    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Chk_3.Focus()
    End Sub
    Private Sub Chk_3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_3.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_3.Checked Then
                Cmb_3.DroppedDown = True
                Cmb_3.Focus()
            Else
                BtnMasuk_Cari.Focus()
            End If
        End If
    End Sub
    Private Sub Cmb_3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_3.KeyPress
        If e.KeyChar = Chr(13) Then Txt_ParamLainValue.Focus()
    End Sub
    Private Sub Txt_ParamLainValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamLainValue.KeyPress
        If e.KeyChar = Chr(13) Then BtnMasuk_Cari.Focus()
    End Sub



End Class