Public Class N_EMI_Display_Barang_Masuk_Asset


    Dim arrLokasi, arrFilterTanggal, arrFilterLain As New ArrayList

    Dim Lv_NoFaktur, Lv_Supplier, Lv_Tanggal, Lv_Jam, Lv_TanggalMasuk, Lv_JamMasuk, Lv_UserID, Lv_FlagTimbang, Lv_FlagTimbangKeluar, Lv_FlagSudahBongkarAndroid, Lv_FlagSudahBarangMasuk As String
    Dim LvDetail_Lokasi, LvDetail_KdBarang, LvDetail_Barang, LvDetail_TglProduksi, LvDetail_TglExpired, LvDetail_JmlhLoading, LvDetail_JmlhMasuk, LvDetail_Selisih, LvDetail_Satuan As String

    Dim item_NoFaktur As Integer = 0
    Dim item_Supplier As Integer = 1
    Dim item_Tanggal As Integer = 2
    Dim item_Jam As Integer = 3
    Dim item_TglMasuk As Integer = 4
    Dim item_JamMasuk As Integer = 5
    Dim item_UserID As Integer = 6
    Dim item_FlagTimbang As Integer = 7
    Dim item_FlagTimbangKeluar As Integer = 8
    Dim item_FlagSudahBongkarAndroid As Integer = 9
    Dim item_FlagSudahBarangMasuk As Integer = 10


    Dim itemDetail_Lokasi As Integer = 0
    Dim itemDetail_KdBarang As Integer = 1
    Dim itemDetail_Barang As Integer = 2
    Dim itemDetail_TglProduksi As Integer = 3
    Dim itemDetail_TglExpired As Integer = 4
    Dim itemDetail_Jmlhloading As Integer = 5
    Dim itemDetail_JmlhMasuk As Integer = 6
    Dim itemDetail_Selisih As Integer = 7
    Dim itemDetail_Satuan As Integer = 8
    Dim itemDetail_NOFak As Integer = 9



    Private Sub N_EMI_Display_Barang_Masuk_Asset_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_PembelianLoading.Columns.Clear()
        Lv_PembelianLoading.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
        Lv_PembelianLoading.Columns.Add("Supplier", 400, HorizontalAlignment.Left)
        Lv_PembelianLoading.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_PembelianLoading.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_PembelianLoading.Columns.Add("Tanggal Masuk", 120, HorizontalAlignment.Center)
        Lv_PembelianLoading.Columns.Add("Jam Masuk", 100, HorizontalAlignment.Center)
        Lv_PembelianLoading.Columns.Add("User", 130, HorizontalAlignment.Center)
        'Hide
        Lv_PembelianLoading.Columns.Add("FT", 0, HorizontalAlignment.Left)
        Lv_PembelianLoading.Columns.Add("FTK", 0, HorizontalAlignment.Left)
        Lv_PembelianLoading.Columns.Add("FSBA", 0, HorizontalAlignment.Left)
        Lv_PembelianLoading.Columns.Add("FSBM", 0, HorizontalAlignment.Left)
        Lv_PembelianLoading.View = View.Details


        Lv_PODetail.Columns.Clear()
        Lv_PODetail.Columns.Add("Lokasi", 120, HorizontalAlignment.Left)
        Lv_PODetail.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_PODetail.Columns.Add("Barang", 200, HorizontalAlignment.Left)
        Lv_PODetail.Columns.Add("Tanggal Produksi", 100, HorizontalAlignment.Center)
        Lv_PODetail.Columns.Add("Tanggal Expired", 100, HorizontalAlignment.Center)
        Lv_PODetail.Columns.Add("Jumlah Loading", 130, HorizontalAlignment.Right)
        Lv_PODetail.Columns.Add("Jumlah Masuk", 130, HorizontalAlignment.Right)
        Lv_PODetail.Columns.Add("Selisih", 130, HorizontalAlignment.Left)
        Lv_PODetail.Columns.Add("Satuan ", 80, HorizontalAlignment.Center)
        'Hide
        Lv_PembelianLoading.Columns.Add("FKTR", 0, HorizontalAlignment.Left)
        Lv_PODetail.View = View.Details

        Lv_DetailPallet.Columns.Clear()
        Lv_DetailPallet.Columns.Add("Barcode", 180, HorizontalAlignment.Left)
        Lv_DetailPallet.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_DetailPallet.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_DetailPallet.Columns.Add("Rak", 150, HorizontalAlignment.Center)
        Lv_DetailPallet.Columns.Add("Gedung", 150, HorizontalAlignment.Center)
        Lv_DetailPallet.Columns.Add("Cost Center", 150, HorizontalAlignment.Center)
        Lv_DetailPallet.View = View.Details


        Kosong()
    End Sub



    Private Sub Kosong()


        Lv_PembelianLoading.Items.Clear()
        Lv_PODetail.Items.Clear()
        Lv_DetailPallet.Items.Clear()

        Txt_JumlahMasuk.Text = ""
        Txt_JumlahBlmMasuk.Text = ""
        Txt_PalletMasuk.Text = ""
        Txt_PalletBlmMasuk.Text = ""

        '===================
        '=     FILTER      =
        '===================
        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear() : arrLokasi.Clear()
            SQL = "select kode_Stock_Owner, Keterangan from stock_owner where aktif = 'Y' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("Keterangan")) : arrLokasi.Add(Dr("kode_Stock_Owner"))
                Loop
                If arrLokasi.Contains("HEAD OFFICE") Then
                    Cmb_Lokasi.SelectedIndex = arrLokasi.IndexOf("HEAD OFFICE")
                Else
                    Cmb_Lokasi.SelectedIndex = 0
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Chk_HariIni.Checked = False : Chk_Tanggal.Checked = False : Chk_Lain.Checked = False
        Tgl_1.Value = Now.Date : Tgl_2.Value = Now.Date
        Txt_ValueLain.Text = ""

        Cmb_Tanggal.Items.Clear() : arrFilterTanggal.Clear()
        Cmb_Tanggal.Items.Add("Tanggal Loading") : arrFilterTanggal.Add("a.Tanggal")
        Cmb_Tanggal.Items.Add("Tanggal Masuk") : arrFilterTanggal.Add("a.Tanggal_Masuk")

        Cmb_Lain.Items.Clear() : arrFilterLain.Clear()
        Cmb_Lain.Items.Add("No Faktur") : arrFilterLain.Add("a.No_Faktur")
        Cmb_Lain.Items.Add("Supplier") : arrFilterLain.Add("b.Nama_Supplier")
        Cmb_Lain.Items.Add("User") : arrFilterLain.Add("a.UseriD")

        LoadData()

    End Sub



    Private Sub GetData_Parent(ByVal Index As Integer)
        Lv_NoFaktur = Lv_PembelianLoading.Items(Index).SubItems(item_NoFaktur).Text
        Lv_Supplier = Lv_PembelianLoading.Items(Index).SubItems(item_Supplier).Text
        Lv_Tanggal = Lv_PembelianLoading.Items(Index).SubItems(item_Tanggal).Text
        Lv_Jam = Lv_PembelianLoading.Items(Index).SubItems(item_Jam).Text
        Lv_TanggalMasuk = Lv_PembelianLoading.Items(Index).SubItems(item_TglMasuk).Text
        Lv_JamMasuk = Lv_PembelianLoading.Items(Index).SubItems(item_JamMasuk).Text
        Lv_UserID = Lv_PembelianLoading.Items(Index).SubItems(item_UserID).Text
        Lv_FlagTimbang = Lv_PembelianLoading.Items(Index).SubItems(item_FlagTimbang).Text
        Lv_FlagTimbangKeluar = Lv_PembelianLoading.Items(Index).SubItems(item_FlagTimbangKeluar).Text
        Lv_FlagSudahBongkarAndroid = Lv_PembelianLoading.Items(Index).SubItems(item_FlagSudahBongkarAndroid).Text
        Lv_FlagSudahBarangMasuk = Lv_PembelianLoading.Items(Index).SubItems(item_FlagSudahBarangMasuk).Text
    End Sub



    Private Sub GetData_Detail(ByVal Index As Integer)
        LvDetail_Lokasi = Lv_PODetail.Items(Index).SubItems(itemDetail_Lokasi).Text
        LvDetail_KdBarang = Lv_PODetail.Items(Index).SubItems(itemDetail_KdBarang).Text
        LvDetail_Barang = Lv_PODetail.Items(Index).SubItems(itemDetail_Barang).Text
        LvDetail_TglProduksi = Lv_PODetail.Items(Index).SubItems(itemDetail_TglProduksi).Text
        LvDetail_TglExpired = Lv_PODetail.Items(Index).SubItems(itemDetail_TglExpired).Text
        LvDetail_JmlhLoading = Lv_PODetail.Items(Index).SubItems(itemDetail_Jmlhloading).Text
        LvDetail_JmlhMasuk = Lv_PODetail.Items(Index).SubItems(itemDetail_JmlhMasuk).Text
        LvDetail_Selisih = Lv_PODetail.Items(Index).SubItems(itemDetail_Selisih).Text
        LvDetail_Satuan = Lv_PODetail.Items(Index).SubItems(itemDetail_Satuan).Text

    End Sub

    Private Sub LoadData()
        If Chk_Tanggal.Checked Then
            If Cmb_Tanggal.SelectedIndex = -1 Then
                MessageBox.Show("Jenis Tanggal Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Tanggal.DroppedDown = True : Cmb_Tanggal.Focus() : Exit Sub
            End If
        End If
        If Chk_Lain.Checked Then
            If Cmb_Lain.SelectedIndex = -1 Then
                MessageBox.Show("Opsi Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Lain.DroppedDown = True : Cmb_Lain.Focus() : Exit Sub
            Else
                If Txt_ValueLain.Text.Trim.Length = 0 Then
                    MessageBox.Show("Value Filter Tidak boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_ValueLain.Focus() : Exit Sub
                End If
            End If
        End If

        get_jam()

        Try
            OpenConn()


            Lv_PembelianLoading.Items.Clear() : Lv_PODetail.Items.Clear() : Lv_DetailPallet.Items.Clear()
            Txt_JumlahMasuk.Text = "" : Txt_PalletMasuk.Text = ""
            Txt_JumlahBlmMasuk.Text = "" : Txt_PalletBlmMasuk.Text = ""

            SQL = "Select a.Kode_Perusahaan, a.No_Faktur, a.Kode_Supplier, b.Nama_Supplier as Supplier, a.Lokasi, a.Tanggal, a.Jam, a.Tanggal_Masuk, a.Jam_Masuk, a.UseriD, "
            SQL = SQL & "a.Flag_Timbang, a.Flag_Timbang_Keluar, a.Flag_Sudah_Bongkar_Android, a.Flag_Selisih_BM "
            SQL = SQL & "from EMI_Pembelian_Loading_Barang_Lain a, Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            If Cmb_Lokasi.SelectedIndex >= 0 Then
                SQL = SQL & "and a.Lokasi = '" & arrLokasi(Cmb_Lokasi.SelectedIndex) & "'  "
            End If

            If Chk_HariIni.Checked Then
                SQL = SQL & "and a.tanggal between '" & Format(tgl_skg, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            End If

            If Chk_Tanggal.Checked Then
                SQL = SQL & "and " & arrFilterTanggal(Cmb_Tanggal.SelectedIndex) & " between '" & Format(Tgl_1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl_2.Value, "yyyy-MM-dd") & "' "
            End If

            If Chk_Lain.Checked Then
                SQL = SQL & "and " & arrFilterLain(Cmb_Lain.SelectedIndex) & " like '%" & Txt_ValueLain.Text & "%' "
            End If

            SQL = SQL & "order by a.Tanggal DESC "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    Lv = Lv_PembelianLoading.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Supplier")) = "", "-", Dr("Supplier")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal")) = "", "-", Format(Dr("Tanggal"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam")) = "", "-'", Dr("Jam")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Masuk")) = "", "-", Format(Dr("Tanggal_Masuk"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam_Masuk")) = "", "-", Dr("Jam_Masuk")))
                    Lv.SubItems.Add(Dr("UseriD"))
                    'Hide
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Flag_Timbang")) = "", "-", Dr("Flag_Timbang")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Flag_Timbang_Keluar")) = "", "-", Dr("Flag_Timbang_Keluar")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Flag_Sudah_Bongkar_Android")) = "", "-", Dr("Flag_Sudah_Bongkar_Android")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Flag_Selisih_BM")) = "", "-", Dr("Flag_Selisih_BM")))

                    If General_Class.CekNULL(Dr("Flag_Timbang_Keluar")) = "" Or General_Class.CekNULL(Dr("Flag_Sudah_Bongkar_Android")) = "" Then
                        Lv.BackColor = Color.LightYellow
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

    Private Sub Lv_PembelianLoading_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PembelianLoading.SelectedIndexChanged
        If Lv_PembelianLoading.Items.Count = 0 OrElse Lv_PembelianLoading.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_PembelianLoading.FocusedItem.Text

            Lv_PODetail.Items.Clear() : Lv_DetailPallet.Items.Clear()
            Txt_JumlahMasuk.Text = "" : Txt_PalletMasuk.Text = ""
            Txt_JumlahBlmMasuk.Text = "" : Txt_PalletBlmMasuk.Text = ""

            SQL = "select a.No_Faktur, b.No_PO, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama as Nama_Barang, b.Tanggal_Produksi, b.Tanggal_Expired, "
            SQL = SQL & "isnull(b.Jumlah, 0) as Jumlah, isnull(b.Jumlah_Masuk, 0) as Jumlah_Masuk, isnull((b.Jumlah_Masuk - b.Jumlah),0) as Selisih, b.Satuan, "

            SQL = SQL & "isnull(( Select top 1 z.Flag_angkut from EMI_Barang_Masuk_Perpallet_Barang_Lain z  "
            SQL = SQL & "where z.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and z.No_Pembelian_Loading = b.No_Faktur and z.Kode_Barang = b.Kode_Barang and z.Flag_angkut = 'Y' and z.Status is null "
            SQL = SQL & "), 'T') as Flag_Angkut, "
            SQL = SQL & "isnull((Select top 1 z.Selesai from EMI_Barang_Masuk_Perpallet_Barang_Lain z where z.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and z.No_Pembelian_Loading = b.No_Faktur and z.Kode_Barang = b.Kode_Barang and z.Selesai = 'Y' and z.Status is null ), 'T') as Flag_Selesai "
            SQL = SQL & "from EMI_Pembelian_Loading_Barang_Lain a,EMI_Pembelian_Loading_Detail_Barang_Lain b, barang_lain c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' "
            SQL = SQL & "order by b.No_PO "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_PODetail.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Produksi")) = "", "-", Format(Dr("Tanggal_Produksi"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Expired")) = "", "-", Format(Dr("Tanggal_Expired"), "dd MMM yyyy")))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Masuk"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Selisih"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    'Hide
                    Lv.SubItems.Add(Dr("No_Faktur"))

                    If General_Class.CekNULL(Dr("Flag_Angkut")) = "Y" Then
                        Lv.BackColor = Color.LightYellow
                    End If

                    If General_Class.CekNULL(Dr("Flag_Angkut")) = "Y" And General_Class.CekNULL(Dr("Flag_Selesai")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
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

    Private Sub Lv_PODetail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PODetail.SelectedIndexChanged
        If Lv_PODetail.Items.Count = 0 OrElse Lv_PODetail.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_PODetail.FocusedItem.SubItems(itemDetail_NOFak).Text
            Dim SelectedKdBarang As String = Lv_PODetail.FocusedItem.SubItems(itemDetail_KdBarang).Text

            Dim TotMasuk As Double = 0
            Dim TotBelumMasuk As Double = 0
            Dim TotPalletMasuk As Double = 0
            Dim TotPalletBelumMasuk As Double = 0

            Lv_DetailPallet.Items.Clear()
            Txt_JumlahMasuk.Text = "" : Txt_PalletMasuk.Text = ""
            Txt_JumlahBlmMasuk.Text = "" : Txt_PalletBlmMasuk.Text = ""

            SQL = "select (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, c.jumlah, c.satuan, c.Flag_angkut, c.Selesai, c.id_warehouse, d.keterangan as Rak, "
            SQL = SQL & "isnull(( select z.Keterangan from N_EMI_Master_Gedung_Barang_Lain z where z.Kode_Perusahaan = e.Kode_Perusahaan and z.ID_Gedung = e.ID_Gedung "
            SQL = SQL & "), '-') as Gedung, "
            SQL = SQL & "isnull(( select z.Keterangan from EMI_Master_Cost_Center z where z.Kode_Perusahaan = e.Kode_Perusahaan and z.Id_Cost_Center = e.ID_Cost_Center "
            SQL = SQL & "), '-') as CostCenter "
            SQL = SQL & "from EMI_Pembelian_Loading_Barang_Lain a,EMI_Pembelian_Loading_Detail_Barang_Lain b, EMI_Barang_Masuk_Perpallet_Barang_Lain c, View_Warehouse_Position d, Barang_Lain_SN e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Pembelian_Loading and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and c.Id_Warehouse = d.Id_WMS_Warehouse_Position "
            SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and c.Kode_Barang = e.Kode_Barang and c.Serial_Number = e.Serial_Number "
            SQL = SQL & "and a.status is null and c.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.No_Pembelian_Loading = '" & SelectedFaktur & "' "
            SQL = SQL & "and c.Kode_Barang = '" & SelectedKdBarang & "' "
            SQL = SQL & "order by c.Kode_Supplier, c.Tanggal, c.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = Lv_DetailPallet.Items.Add(Dr("Barcode"))
                    lv.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Dr("Rak"))
                    lv.SubItems.Add(Dr("Gedung"))
                    lv.SubItems.Add(Dr("CostCenter"))

                    If General_Class.CekNULL(Dr("Flag_angkut")) = "Y" And General_Class.CekNULL(Dr("Selesai")) = "Y" Then
                        TotMasuk += Val(HilangkanTanda(Dr("jumlah")))
                        TotPalletMasuk += 1
                    Else
                        TotBelumMasuk += Val(HilangkanTanda(Dr("jumlah")))
                        TotPalletBelumMasuk += 1
                    End If

                Loop
            End Using

            Txt_JumlahMasuk.Text = Format(TotMasuk, "N0")
            Txt_PalletMasuk.Text = Format(TotPalletMasuk, "N0")
            Txt_JumlahBlmMasuk.Text = Format(TotBelumMasuk, "N0")
            Txt_PalletBlmMasuk.Text = Format(TotPalletBelumMasuk, "N0")



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        LoadData()
    End Sub

    '==============================================================================================================================================================================================
    '=     HDNALE TOOLSTRIP
    '==============================================================================================================================================================================================

    Private Sub SaliinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaliinNoFakturToolStripMenuItem.Click
        If Lv_PembelianLoading.Items.Count = 0 Or Lv_PembelianLoading.SelectedItems.Count = 0 Or Lv_PembelianLoading.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_PembelianLoading.FocusedItem.Text)
    End Sub
    Private Sub BatalBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalBarangMasukToolStripMenuItem.Click
        'If Lv_PembelianLoading.Items.Count = 0 Or Lv_PembelianLoading.FocusedItem.Index = -1 Then Exit Sub


        'Try
        '    OpenConn()
        '    Cmd.Transaction = Cn.BeginTransaction

        '    Dim JudulNotif As String = "Pembatalan Barang Masuk"

        '    '====================
        '    '=     CEK ROLE     =
        '    '====================
        '    If CekButtonRole("Batal_Barang_Masuk_Asset") = "T" Then
        '        CloseTrans()
        '        CloseConn()
        '        MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If

        '    Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Barang Masuk Ini?", JudulNotif, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '    If tanya = vbNo Then
        '        CloseTrans()
        '        CloseConn()
        '        Exit Sub
        '    End If

        '    Dim NoLoading As String = Lv_PembelianLoading.FocusedItem.Text

        '    '=========================================
        '    '=     CEK APAKAH LOADING DIBATALKAN     =
        '    '=========================================
        '    SQL = "select Status from EMI_Pembelian_Loading_Barang_Lain "
        '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and No_Faktur = '" & NoLoading & "' and status is null "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then

        '            If General_Class.CekNULL(Dr("Status")).ToString.ToUpper = "Y" Then
        '                Dr.Close()
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If

        '        Else
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using


        '    '===============================================================
        '    '=     CEK APAKAH DATA SUDAH BERADA PADA STEP BARANG MASUK     =
        '    '===============================================================

        '    SQL = "select Flag_Timbang_Keluar, Flag_Sudah_Bongkar_Android from EMI_Pembelian_Loading_Detail_Barang_Lain "
        '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
        '    SQL = SQL & "and (Flag_Sudah_Bongkar_Android is null or Flag_Timbang_Keluar is null) "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Belum Melakukan Pembongkaran", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using



        '    '=========================================================
        '    '=     CEK APAKAH ADA DATA ID BARANG MASUK PERPALLET     =
        '    '=========================================================
        '    SQL = "select Kode_Perusahaan from EMI_Barang_Masuk_Perpallet_Barang_Lain "
        '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and No_Pembelian_Loading = '" & NoLoading & "' "
        '    SQL = SQL & "and status is null "
        '    Using Dr = OpenTrans(SQL)
        '        If Not Dr.Read Then
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Belum pada Step Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using

        '    '======================================================
        '    '=     CEK APAKAH ADA DATA SUDAH VALIDASI SELISIH     =
        '    '======================================================
        '    SQL = "select Flag_Selisih_BM from EMI_Pembelian_Loading_Barang_Lain a "
        '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and status is null "
        '    SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            If General_Class.CekNULL(Dr("Flag_Selisih_BM")).ToString.ToUpper = "Y" Then
        '                Dr.Close()
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Sudah Melalui Proses Validasi Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        End If
        '    End Using

        '    '=========================
        '    '=     ROLLBACK DATA     =
        '    '=========================
        '    Dim JumlahRollback As Double = 0
        '    Dim totalRollBack As New List(Of (Kode_Barang As String, Jumlah As Double))
        '    SQL = "select a.no_Faktur, c.Kode_Stock_Owner, c.Kode_Stock_Owner_Tujuan, c.Kode_Barang, c.Jumlah, c.Jumlah_Bags, c.satuan, c.Serial_Number, c.Serial_Number_Awal, c.Flag_angkut, c.Selesai "
        '    SQL = SQL & "from EMI_Pembelian_Loading_Barang_Lain a, EMI_Pembelian_Loading_Detail_Barang_Lain b, EMI_Barang_Masuk_Perpallet_Barang_Lain c "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
        '    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
        '    SQL = SQL & "and b.No_Faktur = c.No_Pembelian_Loading and b.Kode_Barang = c.Kode_Barang "
        '    SQL = SQL & "and a.Flag_Timbang_Keluar = 'Y' and a.Flag_Sudah_Bongkar_Android = 'Y' and b.Flag_Sudah_Bongkar_Android = 'Y' "
        '    SQL = SQL & "and a.Status is null and c.Status is null "
        '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.No_Faktur = '" & NoLoading & "' "
        '    Using Ds = BindingTrans(SQL)
        '        With Ds.Tables("MyTable")
        '            If .Rows.Count <> 0 Then
        '                For i As Integer = 0 To .Rows.Count - 1

        '                    Dim idx = totalRollBack.FindIndex(Function(x) x.Kode_Barang = .Rows(i).Item("Kode_Barang"))

        '                    If idx >= 0 Then
        '                        totalRollBack(idx) = (.Rows(i).Item("Kode_Barang"), totalRollBack(idx).Jumlah + Val(.Rows(i).Item("Jumlah")))
        '                    Else
        '                        totalRollBack.Add((.Rows(i).Item("Kode_Barang"), Val(.Rows(i).Item("Jumlah"))))
        '                    End If


        '                    If General_Class.CekNULL(.Rows(i).Item("Selesai")) = "Y" Then
        '                        SQL = "select Jumlah, Jumlah_Bags "
        '                        SQL = SQL & "from Barang_Lain_sn where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                        SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
        '                        SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
        '                        Using Ds2 = BindingTrans(SQL)
        '                            If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) <> 0 Then

        '                                If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) >= .Rows(i).Item("Jumlah") Then
        '                                    SQL = "update Barang_Lain_sn set Jumlah = Jumlah - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
        '                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                                    SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
        '                                    SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
        '                                    ExecuteTrans(SQL)
        '                                Else
        '                                    CloseTrans()
        '                                    CloseConn()
        '                                    MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                                    Exit Sub
        '                                End If

        '                            End If
        '                        End Using

        '                        SQL = "select Good_Stock, Jumlah_Bags "
        '                        SQL = SQL & "from Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                        SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
        '                        Using Ds2 = BindingTrans(SQL)
        '                            If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) <> 0 Then

        '                                If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) >= .Rows(i).Item("Jumlah") Then
        '                                    SQL = "update Barang_Lain set Good_Stock = Good_Stock - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
        '                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                                    SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
        '                                    ExecuteTrans(SQL)
        '                                Else
        '                                    CloseTrans()
        '                                    CloseConn()
        '                                    MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                                    Exit Sub
        '                                End If

        '                            End If
        '                        End Using

        '                    Else
        '                        If General_Class.CekNULL(.Rows(i).Item("Flag_angkut")) = "Y" Then
        '                            SQL = "select Jumlah, Jumlah_Bags "
        '                            SQL = SQL & "from barang_lain_sn_sementara where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                            SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
        '                            SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
        '                            Using Ds2 = BindingTrans(SQL)
        '                                If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) <> 0 Then

        '                                    If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) >= .Rows(i).Item("Jumlah") Then
        '                                        SQL = "update barang_lain_sn_sementara set Jumlah = Jumlah - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
        '                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                                        SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
        '                                        SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
        '                                        ExecuteTrans(SQL)
        '                                    Else
        '                                        CloseTrans()
        '                                        CloseConn()
        '                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                                        Exit Sub
        '                                    End If

        '                                End If
        '                            End Using

        '                        Else
        '                            SQL = "select Jumlah, Jumlah_Bags "
        '                            SQL = SQL & "from Barang_Lain_sn where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                            SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
        '                            SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "'"
        '                            Using Ds2 = BindingTrans(SQL)
        '                                If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) <> 0 Then

        '                                    If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) >= .Rows(i).Item("Jumlah") Then
        '                                        SQL = "update Barang_Lain_sn set Jumlah = Jumlah - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
        '                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                                        SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
        '                                        SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "'"
        '                                        ExecuteTrans(SQL)
        '                                    Else
        '                                        CloseTrans()
        '                                        CloseConn()
        '                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                                        Exit Sub
        '                                    End If

        '                                End If
        '                            End Using

        '                            SQL = "select Good_Stock, Jumlah_Bags "
        '                            SQL = SQL & "from Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                            SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
        '                            Using Ds2 = BindingTrans(SQL)
        '                                If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) <> 0 Then

        '                                    If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) >= .Rows(i).Item("Jumlah") Then
        '                                        SQL = "update Barang_Lain set Good_Stock = Good_Stock - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
        '                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '                                        SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
        '                                        ExecuteTrans(SQL)
        '                                    Else
        '                                        CloseTrans()
        '                                        CloseConn()
        '                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                                        Exit Sub
        '                                    End If

        '                                End If
        '                            End Using
        '                        End If
        '                    End If

        '                Next
        '            End If
        '        End With
        '    End Using

        '    SQL = "select Kode_Perusahaan, Flag_Sudah_Bongkar_Android, Urut_PO, Kode_Barang from EMI_Pembelian_Loading_Detail_Barang_Lain  "
        '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and Flag_Sudah_Bongkar_Android = 'Y' "
        '    Using Ds = BindingTrans(SQL)
        '        With Ds.Tables("MyTable")
        '            If .Rows.Count <> 0 Then
        '                For i As Integer = 0 To .Rows.Count - 1

        '                    Dim JumlahPotong As Double = totalRollBack.Find(Function(x) x.Kode_Barang = .Rows(i).Item("Kode_Barang")).Jumlah

        '                    SQL = "update EMI_Pembelian_Loading_Detail_Barang_Lain set Flag_Sudah_Bongkar_Android = NULL, Flag_Timbang_Keluar = NULL, Jumlah_Masuk = Jumlah_Masuk - " & JumlahPotong & " "
        '                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and Flag_Sudah_Bongkar_Android = 'Y' and Urut_PO = '" & .Rows(i).Item("Urut_PO") & "' "
        '                    ExecuteTrans(SQL)
        '                Next
        '            End If
        '        End With
        '    End Using

        '    SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading_Barang_Lain "
        '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and status is null and Flag_Sudah_Bongkar_Android = 'Y' "
        '    Using Ds = BindingTrans(SQL)
        '        With Ds.Tables("MyTable")
        '            If .Rows.Count <> 0 Then
        '                For i As Integer = 0 To .Rows.Count - 1
        '                    SQL = "update EMI_Pembelian_Loading_Barang_Lain set Flag_Sudah_Bongkar_Android = NULL, Flag_Timbang_Keluar = NULL "
        '                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and status is null and Flag_Sudah_Bongkar_Android = 'Y' "
        '                    ExecuteTrans(SQL)
        '                Next
        '            End If
        '        End With
        '    End Using


        '    SQL = "select Serial_Number, Serial_Number_Awal, Flag_angkut, Selesai from EMI_Barang_Masuk_Perpallet_Barang_Lain "
        '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
        '    SQL = SQL & " and Status is null"
        '    Using Ds = BindingTrans(SQL)
        '        With Ds.Tables("MyTable")
        '            If .Rows.Count <> 0 Then
        '                For i As Integer = 0 To .Rows.Count - 1

        '                    SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
        '                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
        '                    SQL = SQL & " and Status is null "
        '                    ExecuteTrans(SQL)

        '                    'If General_Class.CekNULL(.Rows(i).Item("Selesai")) = "Y" Then
        '                    '    SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
        '                    '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
        '                    '    SQL = SQL & " and Status is null and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "' "
        '                    '    ExecuteTrans(SQL)

        '                    'Else
        '                    '    If General_Class.CekNULL(.Rows(i).Item("Flag_angkut")) = "Y" Then
        '                    '        SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
        '                    '        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
        '                    '        SQL = SQL & " and Status is null and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "' "
        '                    '        ExecuteTrans(SQL)

        '                    '    Else

        '                    '        SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
        '                    '        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
        '                    '        SQL = SQL & " and Status is null and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "' "
        '                    '        ExecuteTrans(SQL)

        '                    '    End If

        '                    'End If


        '                Next
        '            End If
        '        End With
        '    End Using



        '    Cmd.Transaction.Commit()
        '    CloseTrans()
        '    CloseConn()
        '    MessageBox.Show("Data Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Catch ex As Exception
        '    CloseTrans()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'LoadData()


    End Sub

    Private Sub BatalBarangMasukToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BatalBarangMasukToolStripMenuItem1.Click
        If Lv_PODetail.Items.Count = 0 Or Lv_PODetail.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Barang Masuk"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Barang_Masuk_Asset") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Barang Masuk Ini?", JudulNotif, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If


            Dim NoLoading As String = Lv_PODetail.FocusedItem.SubItems(itemDetail_NOFak).Text
            Dim SelectedKdBarang As String = Lv_PODetail.FocusedItem.SubItems(itemDetail_KdBarang).Text

            '=========================================
            '=     CEK APAKAH LOADING DIBATALKAN     =
            '=========================================
            SQL = "select Status from EMI_Pembelian_Loading_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")).ToString.ToUpper = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '===============================================================
            '=     CEK APAKAH DATA SUDAH BERADA PADA STEP BARANG MASUK     =
            '===============================================================

            SQL = "select Flag_Timbang_Keluar, Flag_Sudah_Bongkar_Android from EMI_Pembelian_Loading_Detail_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
            SQL = SQL & "and Kode_Barang = '" & SelectedKdBarang & "' "
            SQL = SQL & "and (Flag_Sudah_Bongkar_Android is null or Flag_Timbang_Keluar is null) "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Belum Melakukan Pembongkaran", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub

                Else
                    Dr.Close()
                    SQL = "select Flag_Timbang_Keluar, Flag_Sudah_Bongkar_Android from EMI_Pembelian_Loading_Detail_Barang_Lain "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
                    SQL = SQL & "and Kode_Barang = '" & SelectedKdBarang & "' "
                    SQL = SQL & "and (Flag_Sudah_Bongkar_Android is null or Flag_Timbang_Keluar is null) "
                    Using Dr2 = OpenTrans(SQL)
                        If Dr2.Read Then
                            Dr2.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Belum Melakukan Pembongkaran", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using
                End If
            End Using



            '=========================================================
            '=     CEK APAKAH ADA DATA ID BARANG MASUK PERPALLET     =
            '=========================================================
            SQL = "select Kode_Perusahaan from EMI_Barang_Masuk_Perpallet_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Pembelian_Loading = '" & NoLoading & "' and Kode_Barang = '" & SelectedKdBarang & "' and selesai is null "
            SQL = SQL & "and status is null "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Belum pada Step Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '======================================================
            '=     CEK APAKAH ADA DATA SUDAH VALIDASI SELISIH     =
            '======================================================
            SQL = "select Flag_Selisih_BM from EMI_Pembelian_Loading_Barang_Lain a "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and status is null "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Selisih_BM")).ToString.ToUpper = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Sudah Melalui Proses Validasi Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            '=========================
            '=     ROLLBACK DATA     =
            '=========================
            Dim JumlahRollback As Double = 0
            Dim totalRollBack As New List(Of (Kode_Barang As String, Jumlah As Double))
            SQL = "select a.no_Faktur, c.Kode_Stock_Owner, c.Kode_Stock_Owner_Tujuan, c.Kode_Barang, c.Jumlah, c.Jumlah_Bags, c.satuan, c.Serial_Number, c.Serial_Number_Awal, c.Flag_angkut, c.Selesai "
            SQL = SQL & "from EMI_Pembelian_Loading_Barang_Lain a, EMI_Pembelian_Loading_Detail_Barang_Lain b, EMI_Barang_Masuk_Perpallet_Barang_Lain c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Pembelian_Loading and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Flag_Timbang_Keluar = 'Y' and a.Flag_Sudah_Bongkar_Android = 'Y' and b.Flag_Sudah_Bongkar_Android = 'Y' "
            SQL = SQL & "and a.Status is null and c.Status is null and c.selesai is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoLoading & "' and c.Kode_Barang = '" & SelectedKdBarang & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim idx = totalRollBack.FindIndex(Function(x) x.Kode_Barang = .Rows(i).Item("Kode_Barang"))

                            If idx >= 0 Then
                                totalRollBack(idx) = (.Rows(i).Item("Kode_Barang"), totalRollBack(idx).Jumlah + Val(.Rows(i).Item("Jumlah")))
                            Else
                                totalRollBack.Add((.Rows(i).Item("Kode_Barang"), Val(.Rows(i).Item("Jumlah"))))
                            End If


                            If General_Class.CekNULL(.Rows(i).Item("Selesai")) = "Y" Then
                                SQL = "select Jumlah, Jumlah_Bags "
                                SQL = SQL & "from Barang_Lain_sn where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
                                Using Ds2 = BindingTrans(SQL)
                                    If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) <> 0 Then

                                        If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) >= .Rows(i).Item("Jumlah") Then
                                            SQL = "update Barang_Lain_sn set Jumlah = Jumlah - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
                                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                            SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                            SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
                                            ExecuteTrans(SQL)
                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                    End If
                                End Using

                                SQL = "select Good_Stock, Jumlah_Bags "
                                SQL = SQL & "from Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                Using Ds2 = BindingTrans(SQL)
                                    If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) <> 0 Then

                                        If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) >= .Rows(i).Item("Jumlah") Then
                                            SQL = "update Barang_Lain set Good_Stock = Good_Stock - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
                                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                            SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                            ExecuteTrans(SQL)
                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                    End If
                                End Using

                            Else
                                If General_Class.CekNULL(.Rows(i).Item("Flag_angkut")) = "Y" Then
                                    SQL = "select Jumlah, Jumlah_Bags "
                                    SQL = SQL & "from barang_lain_sn_sementara where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                    SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
                                    Using Ds2 = BindingTrans(SQL)
                                        If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) <> 0 Then

                                            If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) >= .Rows(i).Item("Jumlah") Then
                                                SQL = "update barang_lain_sn_sementara set Jumlah = Jumlah - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
                                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                                SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
                                                ExecuteTrans(SQL)
                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                        End If
                                    End Using

                                Else
                                    SQL = "select Jumlah, Jumlah_Bags "
                                    SQL = SQL & "from Barang_Lain_sn where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                    SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "'"
                                    Using Ds2 = BindingTrans(SQL)
                                        If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) <> 0 Then

                                            If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) >= .Rows(i).Item("Jumlah") Then
                                                SQL = "update Barang_Lain_sn set Jumlah = Jumlah - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
                                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                                SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "'"
                                                ExecuteTrans(SQL)
                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                        End If
                                    End Using

                                    SQL = "select Good_Stock, Jumlah_Bags "
                                    SQL = SQL & "from Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                    Using Ds2 = BindingTrans(SQL)
                                        If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) <> 0 Then

                                            If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) >= .Rows(i).Item("Jumlah") Then
                                                SQL = "update Barang_Lain set Good_Stock = Good_Stock - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
                                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                                ExecuteTrans(SQL)
                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                        End If
                                    End Using
                                End If
                            End If

                        Next
                    End If
                End With
            End Using

            SQL = "select Kode_Perusahaan, Flag_Sudah_Bongkar_Android, Urut_PO, Kode_Barang from EMI_Pembelian_Loading_Detail_Barang_Lain  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and Flag_Sudah_Bongkar_Android = 'Y' and Kode_Barang = '" & SelectedKdBarang & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim JumlahPotong As Double = totalRollBack.Find(Function(x) x.Kode_Barang = .Rows(i).Item("Kode_Barang")).Jumlah

                            SQL = "update EMI_Pembelian_Loading_Detail_Barang_Lain set Flag_Sudah_Bongkar_Android = NULL, Flag_Timbang_Keluar = NULL, Jumlah_Masuk = Jumlah_Masuk - " & JumlahPotong & " "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and Flag_Sudah_Bongkar_Android = 'Y' and Urut_PO = '" & .Rows(i).Item("Urut_PO") & "' and Kode_Barang = '" & SelectedKdBarang & "' "
                            ExecuteTrans(SQL)
                        Next
                    End If
                End With
            End Using

            'SQL = "select a.Kode_Perusahaan, b.Flag_Sudah_Bongkar_Android, b.* from EMI_Pembelian_Loading_Barang_Lain a, EMI_Pembelian_Loading_Detail_Barang_Lain b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            'SQL = SQL & "and b.Flag_Sudah_Bongkar_Android ='Y' "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur = '" & NoLoading & "' "

            SQL = "select a.Kode_Perusahaan, b.Flag_Sudah_Bongkar_Android, b.* from EMI_Pembelian_Loading_Barang_Lain a, EMI_Pembelian_Loading_Detail_Barang_Lain b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            'SQL = SQL & "and b.Flag_Sudah_Bongkar_Android ='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoLoading & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    'If .Rows.Count = 0 Then
                    If .Rows.Count <> 0 Then
                        SQL = "update EMI_Pembelian_Loading_Barang_Lain set Flag_Sudah_Bongkar_Android = NULL, Flag_Timbang_Keluar = NULL "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and status is null and Flag_Sudah_Bongkar_Android = 'Y' "
                        ExecuteTrans(SQL)
                    End If
                End With
            End Using


            SQL = "select Serial_Number, Serial_Number_Awal, Flag_angkut, Selesai from EMI_Barang_Masuk_Perpallet_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "' and Kode_Barang = '" & SelectedKdBarang & "' and selesai is null "
            SQL = SQL & " and Status is null"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'  and Kode_Barang = '" & SelectedKdBarang & "' and selesai is null "
                            SQL = SQL & "and Status is null "
                            ExecuteTrans(SQL)

                            'If General_Class.CekNULL(.Rows(i).Item("Selesai")) = "Y" Then
                            '    SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
                            '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
                            '    SQL = SQL & " and Status is null and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "' "
                            '    ExecuteTrans(SQL)

                            'Else
                            '    If General_Class.CekNULL(.Rows(i).Item("Flag_angkut")) = "Y" Then
                            '        SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
                            '        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
                            '        SQL = SQL & " and Status is null and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "' "
                            '        ExecuteTrans(SQL)

                            '    Else

                            '        SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
                            '        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
                            '        SQL = SQL & " and Status is null and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "' "
                            '        ExecuteTrans(SQL)

                            '    End If

                            'End If


                        Next
                    End If
                End With
            End Using



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        LoadData()

    End Sub

    Private Sub BatalValidasiBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalValidasiBarangMasukToolStripMenuItem.Click
        If Lv_PODetail.Items.Count = 0 Or Lv_PODetail.FocusedItem.Index = -1 Then Exit Sub

        'MASIH ADA KESALAHAN
        Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Barang Masuk"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Barang_Masuk_Asset") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Barang Masuk Ini?", JudulNotif, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If


            Dim NoLoading As String = Lv_PODetail.FocusedItem.SubItems(itemDetail_NOFak).Text
            Dim SelectedKdBarang As String = Lv_PODetail.FocusedItem.SubItems(itemDetail_KdBarang).Text

            '=========================================
            '=     CEK APAKAH LOADING DIBATALKAN     =
            '=========================================
            SQL = "select Status from EMI_Pembelian_Loading_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")).ToString.ToUpper = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '===============================================================
            '=     CEK APAKAH DATA SUDAH BERADA PADA STEP BARANG MASUK     =
            '===============================================================

            SQL = "select Flag_Timbang_Keluar, Flag_Sudah_Bongkar_Android from EMI_Pembelian_Loading_Detail_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
            SQL = SQL & "and Kode_Barang = '" & SelectedKdBarang & "' "
            SQL = SQL & "and (Flag_Sudah_Bongkar_Android is null or Flag_Timbang_Keluar is null) "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Belum Melakukan Pembongkaran", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub

                Else
                    Dr.Close()
                    SQL = "select Flag_Timbang_Keluar, Flag_Sudah_Bongkar_Android from EMI_Pembelian_Loading_Detail_Barang_Lain "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
                    SQL = SQL & "and Kode_Barang = '" & SelectedKdBarang & "' "
                    SQL = SQL & "and (Flag_Sudah_Bongkar_Android is null or Flag_Timbang_Keluar is null) "
                    Using Dr2 = OpenTrans(SQL)
                        If Dr2.Read Then
                            Dr2.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Belum Melakukan Pembongkaran", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using
                End If
            End Using

            '=========================================================
            '=     CEK APAKAH ADA DATA ID BARANG MASUK PERPALLET     =
            '=========================================================
            SQL = "select Kode_Perusahaan, selesai from EMI_Barang_Masuk_Perpallet_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Pembelian_Loading = '" & NoLoading & "' and Kode_Barang = '" & SelectedKdBarang & "'  "
            SQL = SQL & "and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("selesai")) = "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Belum pada Step Validasi Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else

                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Belum pada Step Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================================================
            '=     CEK APAKAH ADA DATA ID BARANG MASUK PERPALLET     =
            '=========================================================
            SQL = "select Kode_Perusahaan, selesai from EMI_Barang_Masuk_Perpallet_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Pembelian_Loading = '" & NoLoading & "' and Kode_Barang = '" & SelectedKdBarang & "' And selesai = 'Y' "
            SQL = SQL & "and status is null "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Belum pada Step Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            '======================================================
            '=     CEK APAKAH ADA DATA SUDAH VALIDASI SELISIH     =
            '======================================================
            SQL = "select Flag_Selisih_BM from EMI_Pembelian_Loading_Barang_Lain a "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and status is null "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Selisih_BM")).ToString.ToUpper = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Tidak Bisa Melanjutkan Pembatalan karena No Loading Sudah Melalui Proses Validasi Barang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            '=========================
            '=     ROLLBACK DATA     =
            '=========================
            Dim JumlahRollback As Double = 0
            Dim totalRollBack As New List(Of (Kode_Barang As String, Jumlah As Double))
            SQL = "select a.no_Faktur, c.Kode_Stock_Owner, c.Kode_Stock_Owner_Tujuan, c.Kode_Barang, c.Jumlah, c.Jumlah_Bags, c.satuan, c.Serial_Number, c.Serial_Number_Awal, c.Flag_angkut, c.Selesai "
            SQL = SQL & "from EMI_Pembelian_Loading_Barang_Lain a, EMI_Pembelian_Loading_Detail_Barang_Lain b, EMI_Barang_Masuk_Perpallet_Barang_Lain c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Pembelian_Loading and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Flag_Timbang_Keluar = 'Y' and a.Flag_Sudah_Bongkar_Android = 'Y' and b.Flag_Sudah_Bongkar_Android = 'Y' "
            SQL = SQL & "and a.Status is null and c.Status is null And c.selesai = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoLoading & "' and c.Kode_Barang = '" & SelectedKdBarang & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim idx = totalRollBack.FindIndex(Function(x) x.Kode_Barang = .Rows(i).Item("Kode_Barang"))

                            If idx >= 0 Then
                                totalRollBack(idx) = (.Rows(i).Item("Kode_Barang"), totalRollBack(idx).Jumlah + Val(.Rows(i).Item("Jumlah")))
                            Else
                                totalRollBack.Add((.Rows(i).Item("Kode_Barang"), Val(.Rows(i).Item("Jumlah"))))
                            End If


                            If General_Class.CekNULL(.Rows(i).Item("Selesai")) = "Y" Then
                                SQL = "select Jumlah, Jumlah_Bags "
                                SQL = SQL & "from Barang_Lain_sn where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
                                Using Ds2 = BindingTrans(SQL)
                                    If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) <> 0 Then

                                        If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) >= .Rows(i).Item("Jumlah") Then
                                            SQL = "update Barang_Lain_sn set Jumlah = Jumlah - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
                                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                            SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                            SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
                                            ExecuteTrans(SQL)
                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                    End If
                                End Using

                                SQL = "select Good_Stock, Jumlah_Bags "
                                SQL = SQL & "from Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                Using Ds2 = BindingTrans(SQL)
                                    If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) <> 0 Then

                                        If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) >= .Rows(i).Item("Jumlah") Then
                                            SQL = "update Barang_Lain set Good_Stock = Good_Stock - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
                                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                            SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                            ExecuteTrans(SQL)
                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                    End If
                                End Using


                                '==================
                                '=     TAMBAH     =
                                '==================
                                SQL = "select Jumlah, Jumlah_Bags "
                                SQL = SQL & "from Barang_Lain_sn where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "'"
                                Using Ds2 = BindingTrans(SQL)

                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                        SQL = "update Barang_Lain_sn set Jumlah = Jumlah + " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags + " & .Rows(i).Item("Jumlah_Bags") & " "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                        SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "'"
                                        ExecuteTrans(SQL)

                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub

                                    End If
                                End Using

                                SQL = "select Good_Stock, Jumlah_Bags "
                                SQL = SQL & "from Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                        SQL = "update Barang_Lain set Good_Stock = Good_Stock + " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags + " & .Rows(i).Item("Jumlah_Bags") & " "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                        ExecuteTrans(SQL)

                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub

                                    End If
                                End Using

                            Else
                                'If General_Class.CekNULL(.Rows(i).Item("Flag_angkut")) = "Y" Then
                                '    SQL = "select Jumlah, Jumlah_Bags "
                                '    SQL = SQL & "from barang_lain_sn_sementara where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '    SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                '    SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
                                '    Using Ds2 = BindingTrans(SQL)
                                '        If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) <> 0 Then

                                '            If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) >= .Rows(i).Item("Jumlah") Then
                                '                SQL = "update barang_lain_sn_sementara set Jumlah = Jumlah - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
                                '                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                '                SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "'"
                                '                ExecuteTrans(SQL)
                                '            Else
                                '                CloseTrans()
                                '                CloseConn()
                                '                MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                Exit Sub
                                '            End If

                                '        End If
                                '    End Using

                                'Else
                                '    SQL = "select Jumlah, Jumlah_Bags "
                                '    SQL = SQL & "from Barang_Lain_sn where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '    SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                '    SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "'"
                                '    Using Ds2 = BindingTrans(SQL)
                                '        If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) <> 0 Then

                                '            If Val(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")) >= .Rows(i).Item("Jumlah") Then
                                '                SQL = "update Barang_Lain_sn set Jumlah = Jumlah - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
                                '                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                '                SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "'"
                                '                ExecuteTrans(SQL)
                                '            Else
                                '                CloseTrans()
                                '                CloseConn()
                                '                MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                Exit Sub
                                '            End If

                                '        End If
                                '    End Using

                                '    SQL = "select Good_Stock, Jumlah_Bags "
                                '    SQL = SQL & "from Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '    SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                '    Using Ds2 = BindingTrans(SQL)
                                '        If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) <> 0 Then

                                '            If Val(Ds2.Tables("MyTable").Rows(0).Item("Good_Stock")) >= .Rows(i).Item("Jumlah") Then
                                '                SQL = "update Barang_Lain set Good_Stock = Good_Stock - " & .Rows(i).Item("Jumlah") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(i).Item("Jumlah_Bags") & " "
                                '                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                                '                ExecuteTrans(SQL)
                                '            Else
                                '                CloseTrans()
                                '                CloseConn()
                                '                MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                Exit Sub
                                '            End If

                                '        End If
                                '    End Using
                                'End If
                            End If

                        Next
                    End If
                End With
            End Using

            'SQL = "select Kode_Perusahaan, Flag_Sudah_Bongkar_Android, Urut_PO, Kode_Barang from EMI_Pembelian_Loading_Detail_Barang_Lain  "
            'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and Flag_Sudah_Bongkar_Android = 'Y' and Kode_Barang = '" & SelectedKdBarang & "' "
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        If .Rows.Count <> 0 Then
            '            For i As Integer = 0 To .Rows.Count - 1

            '                Dim JumlahPotong As Double = totalRollBack.Find(Function(x) x.Kode_Barang = .Rows(i).Item("Kode_Barang")).Jumlah

            '                SQL = "update EMI_Pembelian_Loading_Detail_Barang_Lain set Flag_Sudah_Bongkar_Android = NULL, Flag_Timbang_Keluar = NULL, Jumlah_Masuk = Jumlah_Masuk - " & JumlahPotong & " "
            '                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and Flag_Sudah_Bongkar_Android = 'Y' and Urut_PO = '" & .Rows(i).Item("Urut_PO") & "' and Kode_Barang = '" & SelectedKdBarang & "' "
            '                ExecuteTrans(SQL)
            '            Next
            '        End If
            '    End With
            'End Using

            'SQL = "select a.Kode_Perusahaan, b.Flag_Sudah_Bongkar_Android, b.* from EMI_Pembelian_Loading_Barang_Lain a, EMI_Pembelian_Loading_Detail_Barang_Lain b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            ''SQL = SQL & "and b.Flag_Sudah_Bongkar_Android ='Y' "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur = '" & NoLoading & "' "
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        'If .Rows.Count = 0 Then
            '        If .Rows.Count <> 0 Then
            '            SQL = "update EMI_Pembelian_Loading_Barang_Lain set Flag_Sudah_Bongkar_Android = NULL, Flag_Timbang_Keluar = NULL "
            '            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and status is null and Flag_Sudah_Bongkar_Android = 'Y' "
            '            ExecuteTrans(SQL)
            '        End If
            '    End With
            'End Using


            SQL = "select Serial_Number, Serial_Number_Awal, Flag_angkut, Selesai from EMI_Barang_Masuk_Perpallet_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "' and Kode_Barang = '" & SelectedKdBarang & "' And selesai = 'Y' "
            SQL = SQL & " and Status is null"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1


                            SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Selesai = NULL, Id_Warehouse = NULL, Userid_val_stok = NULL, Tanggal_Val_Stok = NULL, jam_val_stok = NULL, Kode_Stock_Owner_Tujuan = NULL "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'  and Kode_Barang = '" & SelectedKdBarang & "' And selesai = 'Y' "
                            SQL = SQL & "and Status is null "
                            ExecuteTrans(SQL)

                            'If General_Class.CekNULL(.Rows(i).Item("Selesai")) = "Y" Then
                            '    SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
                            '    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
                            '    SQL = SQL & " and Status is null and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "' "
                            '    ExecuteTrans(SQL)

                            'Else
                            '    If General_Class.CekNULL(.Rows(i).Item("Flag_angkut")) = "Y" Then
                            '        SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
                            '        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
                            '        SQL = SQL & " and Status is null and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "' "
                            '        ExecuteTrans(SQL)

                            '    Else

                            '        SQL = "update EMI_Barang_Masuk_Perpallet_Barang_Lain set Status = 'Y' "
                            '        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Pembelian_Loading = '" & NoLoading & "'"
                            '        SQL = SQL & " and Status is null and Serial_Number = '" & .Rows(i).Item("Serial_Number_Awal") & "' "
                            '        ExecuteTrans(SQL)

                            '    End If

                            'End If


                        Next
                    End If
                End With
            End Using



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        LoadData()
    End Sub


    '==============================================================================================================================================================================================
    '=     HDNALE KEYPRESS
    '==============================================================================================================================================================================================

    Private Sub Cmb_Lokasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
        If e.KeyChar = Chr(13) Then Chk_HariIni.Focus()
    End Sub
    Private Sub Chk_HariIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_HariIni.CheckedChanged
        If Chk_HariIni.Checked = True Then
            Chk_Tanggal.Checked = False
            Btn_Cari_Click(Chk_HariIni, e)
        End If
    End Sub
    Private Sub Chk_HariIni_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_HariIni.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Tanggal.Focus()
    End Sub
    Private Sub Chk_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Tanggal.CheckedChanged
        If Chk_Tanggal.Checked Then
            Cmb_Tanggal.Enabled = True : Tgl_1.Enabled = True : Tgl_2.Enabled = True
            Chk_HariIni.Checked = False
        Else
            Cmb_Tanggal.Enabled = False : Tgl_1.Enabled = False : Tgl_1.Enabled = False
            Cmb_Tanggal.SelectedIndex = -1 : Tgl_1.Value = Now.Date : Tgl_1.Value = Now.Date
        End If
    End Sub
    Private Sub Chk_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_Tanggal.Checked Then
                Cmb_Tanggal.DroppedDown = True
                Cmb_Tanggal.Focus()
            Else
                Chk_Lain.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then Tgl_1.Focus()
    End Sub
    Private Sub Tgl_1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl_2.Focus()
    End Sub
    Private Sub Tgl_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_2.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Lain.Focus()
    End Sub
    Private Sub Chk_Lain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Lain.CheckedChanged
        If Chk_Lain.Checked Then
            Cmb_Lain.Enabled = True : Txt_ValueLain.Enabled = True
        Else
            Cmb_Lain.Enabled = False : Txt_ValueLain.Enabled = False
            Cmb_Lain.SelectedIndex = -1 : Txt_ValueLain.Text = ""
        End If
    End Sub
    Private Sub Chk_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Lain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_Lain.Checked Then
                Cmb_Lain.DroppedDown = True
                Cmb_Lain.Focus()
            Else
                Btn_Cari.Focus()
            End If
        End If
    End Sub
    Private Sub Cmb_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lain.KeyPress
        If e.KeyChar = Chr(13) Then Txt_ValueLain.Focus()
    End Sub
    Private Sub Txt_ValueLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ValueLain.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

End Class