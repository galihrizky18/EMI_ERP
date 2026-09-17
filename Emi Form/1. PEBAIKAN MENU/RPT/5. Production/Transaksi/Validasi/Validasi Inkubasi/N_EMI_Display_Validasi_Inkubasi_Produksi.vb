

Public Class N_EMI_Display_Validasi_Inkubasi_Produksi

    Private selectedCard As Panel = Nothing

    Dim arrFilter As New ArrayList

    Dim SelectedFaktur, SelectedBatch As String

    Dim Lv_KdBarang, Lv_NmBarang, Lv_Barcode, Lv_Jumlah, Lv_Satuan, Lv_Jenis, Lv_Status As String

    Dim itemDetail_KdBarang As Integer = 0
    Dim itemDetail_NmBarang As Integer = 1
    Dim itemDetail_Barcode As Integer = 2
    Dim itemDetail_Jumlah As Integer = 3
    Dim itemDetail_Satuan As Integer = 4
    Dim itemDetail_Jenis As Integer = 5
    Dim itemDetail_Status As Integer = 6



    Private Sub N_EMI_Display_Validasi_Inkubasi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        FLPanel_Data.FlowDirection = FlowDirection.TopDown
        FLPanel_Data.WrapContents = False
        FLPanel_Data.AutoScroll = True

        FPNL_Batch.FlowDirection = FlowDirection.LeftToRight
        FPNL_Batch.WrapContents = False
        FPNL_Batch.AutoScroll = True

        Lv_DataDestail.Columns.Clear()
        Lv_DataDestail.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '0
        Lv_DataDestail.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left) '1
        Lv_DataDestail.Columns.Add("Barocde", 200, HorizontalAlignment.Left) '2
        Lv_DataDestail.Columns.Add("Jumlah", 130, HorizontalAlignment.Right) '3
        Lv_DataDestail.Columns.Add("Satuan", 70, HorizontalAlignment.Center) '4
        Lv_DataDestail.Columns.Add("Jenis", 150, HorizontalAlignment.Center) '5
        Lv_DataDestail.Columns.Add("Status Military Sampling", 150, HorizontalAlignment.Center) '6
        Lv_DataDestail.View = View.Details




        kosong()

        Cmb_Filter.Focus()
    End Sub


    Private Sub kosong()

        FLPanel_Data.Controls.Clear()

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add(OpsiSeluruh) : arrFilter.Add(OpsiSeluruh)
        Cmb_Filter.Items.Add("No Transaksi") : arrFilter.Add("a.No_Transaksi")
        Cmb_Filter.Items.Add("No Split") : arrFilter.Add("b.No_Transaksi")
        Cmb_Filter.Items.Add("Keterangan") : arrFilter.Add("a.Keterangan")
        Cmb_Filter.SelectedIndex = 0

        SelectedFaktur = ""
        SelectedBatch = ""


        LoadDataParent()

    End Sub

    Private Sub get_no_faktur()
        Dim FValidasiGR As String = "VGR2-"
        Txt_NoFaktur.Text = FValidasiGR & Format(tgl_skg, "MMyy") & "-" &
                            General_Class.Get_Last_Number2("N_EMI_Transaksi_Validasi_Masa_Inkubasi", "No_Transaksi", 5,
                            "Kode_perusahaan", KodePerusahaan,
                            "And", "substring(No_Transaksi, 1, " & Len(FValidasiGR) + 4 & ")", FValidasiGR & Format(tgl_skg, "MMyy"))

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter.SelectedIndex <> 0 Then
            If Txt_FilterValue.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Harus Diisi Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_FilterValue.Focus() : Exit Sub
            End If
        End If

        KosongPanelDetail()
        LoadDataParent()

    End Sub
    Private Sub Btn_Refresh_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Refresh_Cari.Click

        ReloadForm()

    End Sub



    Private Sub ReloadForm()

        Cmb_Filter.SelectedIndex = 0 : Txt_FilterValue.Text = "" : Txt_FilterValue.Enabled = False

        KosongPanelDetail()
        LoadDataParent()

    End Sub

    Private Sub KosongPanelDetail()
        Txt_Keterangan.Text = ""
        Txt_NoFaktur.Text = ""
        Txt_SoAwal.Text = ""
        Txt_SOTujuan.Text = ""
        SelectedFaktur = ""
        SelectedBatch = ""
        DTP_Tanggal_Produksi.Value = Date.Now
        Lv_DataDestail.Items.Clear()
        FPNL_Batch.Controls.Clear()


    End Sub

    Private Sub GetDataLvDetail(ByVal index As Integer)

        Lv_KdBarang = Lv_DataDestail.Items(index).SubItems(itemDetail_KdBarang).Text
        Lv_NmBarang = Lv_DataDestail.Items(index).SubItems(itemDetail_NmBarang).Text
        Lv_Barcode = Lv_DataDestail.Items(index).SubItems(itemDetail_Barcode).Text
        Lv_Jumlah = Lv_DataDestail.Items(index).SubItems(itemDetail_Jumlah).Text
        Lv_Satuan = Lv_DataDestail.Items(index).SubItems(itemDetail_Satuan).Text
        Lv_Jenis = Lv_DataDestail.Items(index).SubItems(itemDetail_Jenis).Text
        Lv_Status = Lv_DataDestail.Items(index).SubItems(itemDetail_Status).Text

    End Sub

    Private Sub LoadDataParent()

        Try
            OpenConn()

            Dim PanelSizeWidth As Double = FLPanel_Data.Width - 30

            FLPanel_Data.Controls.Clear()
            'SQL = "select a.Kode_Perusahaan, a.No_Transaksi, b.No_Transaksi as No_Split, a.Keterangan, "
            'SQL = SQL & "a.Tanggal as Tanggal_GR2, a.Jam as Jam_GR2, b.Tanggal as Tangal_Split, b.Jam as Jam_Split, a.UserID "
            'SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Split_Production_Order b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            ''SQL = SQL & "and b.Flag_Hasil_Produksi_GR2 = 'Y' "
            'SQL = SQL & "and a.Flag_Validasi_Inkubasi is null "
            'SQL = SQL & "and a.Status is null and b.Status is null "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            SQL = "select a.Kode_Perusahaan, a.No_Transaksi, b.No_Transaksi as No_Split, a.Keterangan, "
            SQL = SQL & "a.Tanggal as Tanggal_GR2, a.Jam as Jam_GR2, b.Tanggal as Tangal_Split, b.Jam as Jam_Split, a.UserID "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Split_Production_Order b, Emi_Production_Results_Validation_Detail c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and a.No_Transaksi = c.No_Transaksi "
            SQL = SQL & "and a.Flag_Validasi_Inkubasi is null "
            SQL = SQL & "and a.Status is null and b.Status is null "
            SQL = SQL & "and c.Jenis = 'Finished Good' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If Cmb_Filter.SelectedIndex > 0 Then
                SQL = SQL & "and " & arrFilter(Cmb_Filter.SelectedIndex) & " like '%" & Txt_FilterValue.Text & "%' "
            End If
            SQL = SQL & "group by a.Kode_Perusahaan, a.No_Transaksi, b.No_Transaksi, a.Keterangan,a.Tanggal, a.Jam, b.Tanggal, b.Jam, a.UserID "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim card As New Panel
                    card.AutoSize = True
                    'card.Size = New Size(100, 100)
                    'card.AutoSizeMode = AutoSizeMode.GrowAndShrink
                    'card.Width = FLPanel_Data.ClientSize.Width
                    card.Size = New Size(PanelSizeWidth, 100)
                    card.Tag = Dr("No_Transaksi")
                    card.BackColor = Color.White
                    card.BorderStyle = BorderStyle.FixedSingle
                    card.Cursor = Cursors.Hand
                    card.BackgroundImageLayout = ImageLayout.Stretch
                    card.Dock = DockStyle.Top
                    card.Padding = New Padding(0, 0, 0, 10)
                    card.Margin = New Padding(5, 6, 5, 6)
                    AddHandler card.Click, AddressOf Handle_Card_Click

                    '=====================================
                    '=     TAMBAH KOMPONEN PADA CARD     =
                    '=====================================
                    ' Label No Faktur
                    Dim lblNoFaktur As New Label()
                    lblNoFaktur.Text = Dr("No_Transaksi")
                    lblNoFaktur.Font = New Font("Work Sans", 9, FontStyle.Bold)
                    lblNoFaktur.Location = New Point(5, 5)
                    lblNoFaktur.AutoSize = True
                    lblNoFaktur.BackColor = Color.Transparent
                    AddHandler lblNoFaktur.Click, AddressOf Handle_Item_Card_Click
                    card.Controls.Add(lblNoFaktur)


                    ' Label No Split
                    Dim lblNoSplit As New Label()
                    lblNoSplit.Text = Dr("No_Split")
                    lblNoSplit.Font = New Font("Work Sans", 7, FontStyle.Regular)
                    lblNoSplit.Location = New Point(5, 25)
                    lblNoSplit.AutoSize = True
                    lblNoSplit.BackColor = Color.Transparent
                    AddHandler lblNoSplit.Click, AddressOf Handle_Item_Card_Click
                    card.Controls.Add(lblNoSplit)

                    ' Label Tanggal
                    Dim lblTgl As New Label()
                    lblTgl.Text = Char.ConvertFromUtf32(&H1F4C5) & " Tanggal Validasi: " & Format(Dr("Tanggal_GR2"), "dd MMM yyyy")
                    lblTgl.AutoSize = True
                    lblTgl.MaximumSize = New Size(200, 0)
                    lblTgl.Location = New Point(5, 45)
                    lblTgl.Font = New Font("Work Sans", 7, FontStyle.Regular)
                    lblTgl.BackColor = Color.Transparent
                    AddHandler lblTgl.Click, AddressOf Handle_Item_Card_Click
                    card.Controls.Add(lblTgl)

                    ' Icon Keterangan
                    Dim lblKeterangan As New Label()
                    lblKeterangan.Text = Char.ConvertFromUtf32(&H1F4DD) & " Keterangan : "
                    lblKeterangan.AutoSize = True
                    lblKeterangan.Location = New Point(5, 65)
                    lblKeterangan.Font = New Font("Work Sans", 7, FontStyle.Regular)
                    lblKeterangan.BackColor = Color.Transparent
                    AddHandler lblKeterangan.Click, AddressOf Handle_Item_Card_Click
                    card.Controls.Add(lblKeterangan)

                    ' Label Keterangan
                    Dim Keterangan As New Label()
                    Keterangan.Text = Dr("Keterangan")
                    Keterangan.AutoSize = True
                    Keterangan.MinimumSize = New Size(185, 0)
                    Keterangan.MaximumSize = New Size(185, 0)
                    Keterangan.Location = New Point(20, 82)
                    Keterangan.Font = New Font("Work Sans", 7, FontStyle.Regular)
                    Keterangan.BackColor = Color.Transparent
                    AddHandler Keterangan.Click, AddressOf Handle_Item_Card_Click
                    card.Controls.Add(Keterangan)

                    FLPanel_Data.Controls.Add(card)
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        FLPanel_Data.Focus()

    End Sub

    Private Sub Handle_Card_Click(sender As Object, e As EventArgs)
        Dim clickedCard As Panel = CType(sender, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FLPanel_Data.Controls
            If TypeOf ctrl Is Panel Then
                CType(ctrl, Panel).BackColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.LightBlue

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedCard.Tag.ToString()
        LoadDetail_Batch(NoFaktur)
    End Sub

    Private Sub Handle_Item_Card_Click(sender As Object, e As EventArgs)

        Dim panelInduk As Panel = TryCast(sender.Parent, Panel)
        Dim clickedCard As Panel = CType(panelInduk, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FLPanel_Data.Controls
            If TypeOf ctrl Is Panel Then
                CType(ctrl, Panel).BackColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.LightBlue

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedCard.Tag.ToString()
        LoadDetail_Batch(NoFaktur)
    End Sub

    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
        If Cmb_Filter.SelectedIndex = 0 Then
            Txt_FilterValue.Enabled = False
        Else
            Txt_FilterValue.Enabled = True
        End If
        Txt_FilterValue.Text = ""
    End Sub

    Private Sub LoadDetail_Batch(ByVal Faktur As String)

        Try
            OpenConn()
            get_no_faktur()


            Dim PanelSizeHeight As Double = FPNL_Batch.Height - 15

            FPNL_Batch.Controls.Clear()
            SQL = "select distinct a.Kode_Perusahaan, a.No_Transaksi, b.Tahap "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Flag_Validasi_Inkubasi is null "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Faktur & "' "
            SQL = SQL & "AND b.Tahap NOT IN ( "
            SQL = SQL & "select Tahap from N_EMI_Transaksi_Validasi_Masa_Inkubasi z "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Faktur_Penerimaan_Barang = a.No_Transaksi and a.Status is null and z.status is null ) "
            SQL = SQL & "order by b.Tahap "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim card As New Panel
                    'card.AutoSize = True
                    'card.Size = New Size(100, 100)
                    'card.AutoSizeMode = AutoSizeMode.GrowAndShrink
                    'card.Width = FLPanel_Data.ClientSize.Width
                    card.Size = New Size(50, PanelSizeHeight)
                    card.Tag = Dr("Tahap")
                    card.BackColor = Color.White
                    card.BorderStyle = BorderStyle.FixedSingle
                    card.Cursor = Cursors.Hand
                    card.BackgroundImageLayout = ImageLayout.Stretch
                    card.Dock = DockStyle.Top
                    card.Padding = New Padding(0, 0, 0, 0)
                    card.Margin = New Padding(10, 7, 0, 0)
                    AddHandler card.Click, AddressOf Handle_Batch_Click

                    '=====================================
                    '=     TAMBAH KOMPONEN PADA CARD     =
                    '=====================================
                    ' Label No Faktur
                    Dim lblNoFaktur As New Label()
                    lblNoFaktur.Text = Dr("Tahap")
                    lblNoFaktur.Font = New Font("Work Sans", 12, FontStyle.Bold)
                    lblNoFaktur.AutoSize = True
                    lblNoFaktur.BackColor = Color.Transparent

                    ' Hitung posisi tengah (horizontal dan vertical)
                    Dim x As Integer = (card.Width - lblNoFaktur.PreferredWidth) \ 2
                    Dim y As Integer = (card.Height - lblNoFaktur.PreferredHeight) \ 2
                    lblNoFaktur.Location = New Point(x, y)

                    AddHandler lblNoFaktur.Click, AddressOf Handle_Item_Batch_Click
                    card.Controls.Add(lblNoFaktur)



                    FPNL_Batch.Controls.Add(card)
                Loop
            End Using



            Lv_DataDestail.Items.Clear()
            SelectedFaktur = Faktur
            Txt_SoAwal.Text = ""
            Txt_SOTujuan.Text = ""
            DTP_Tanggal_Produksi.Value = Now.Date





            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Handle_Batch_Click(sender As Object, e As EventArgs)
        Dim clickedCard As Panel = CType(sender, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FPNL_Batch.Controls
            If TypeOf ctrl Is Panel Then
                Dim pnl As Panel = CType(ctrl, Panel)
                pnl.BackColor = Color.White

                For Each inner As Control In pnl.Controls
                    If TypeOf inner Is Label Then
                        CType(inner, Label).ForeColor = Color.Black
                    End If
                Next
            End If
        Next

        For Each ctrl2 As Control In clickedCard.Controls
            If TypeOf ctrl2 Is Label Then
                CType(ctrl2, Label).ForeColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.FromArgb(15, 86, 122)

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoBatch As String = selectedCard.Tag.ToString()
        SelectedBatch = NoBatch
        LoadDetail(SelectedFaktur, NoBatch)
    End Sub

    Private Sub Handle_Item_Batch_Click(sender As Object, e As EventArgs)

        Dim panelInduk As Panel = TryCast(sender.Parent, Panel)
        Dim clickedCard As Panel = CType(panelInduk, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FPNL_Batch.Controls
            If TypeOf ctrl Is Panel Then
                Dim pnl As Panel = CType(ctrl, Panel)
                pnl.BackColor = Color.White

                ' Telusuri semua kontrol dalam panel
                For Each inner As Control In pnl.Controls
                    If TypeOf inner Is Label Then
                        CType(inner, Label).ForeColor = Color.Black
                    End If
                Next
            End If
        Next

        For Each ctrl2 As Control In clickedCard.Controls
            If TypeOf ctrl2 Is Label Then
                CType(ctrl2, Label).ForeColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.FromArgb(15, 86, 122)

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoBatch As String = selectedCard.Tag.ToString()
        SelectedBatch = NoBatch
        LoadDetail(SelectedFaktur, NoBatch)
    End Sub



    Private Sub LoadDetail(ByVal Faktur As String, ByVal Batch As String)

        Try
            OpenConn()
            get_no_faktur()

            Dim SoAwal As String = ""
            Dim SoTujuan As String = ""
            Dim TglProduksi As Date = Nothing
            Lv_DataDestail.Items.Clear()
            SQL = "select a.No_Transaksi, a.No_Production_Order, b.Tanggal as Tanggal_Produksi, b.Jam as Jam_Produksi, b.Kode_Barang as Barang_Produksi, "
            SQL = SQL & "c.Kode_Stock_Owner_Awal, c.Kode_Stock_Owner_Tujuan, c.Kode_Barang, d.Nama, c.Serial_Number_Tujuan, c.Warna, c.Jumlah, c.Satuan, c.Jenis, "

            SQL = SQL & "CASE "
            SQL = SQL & "WHEN c.Jenis = 'Finished Good' THEN "
            SQL = SQL & "ISNULL(( SELECT z.qr_code FROM Barang_SN_Sementara z WHERE z.Kode_Perusahaan = c.Kode_Perusahaan AND z.Serial_Number = c.Serial_Number_Tujuan ), '-') "
            SQL = SQL & "ELSE "
            SQL = SQL & "ISNULL(( SELECT z.qr_code FROM Barang_SN z WHERE z.Kode_Perusahaan = c.Kode_Perusahaan AND z.Serial_Number = c.Serial_Number_Tujuan ), '-') "
            SQL = SQL & "END AS Barcode, "

            SQL = SQL & "case "
            SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = a.No_Production_Order and z.No_Batch = c.Tahap), 'U') = 'T' then 'DITOLAK' "
            SQL = SQL & "when isnull((select z.Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Split_Po = a.No_Production_Order and z.No_Batch = c.Tahap), 'U') = 'Y' "
            SQL = SQL & "then 'DITERIMA' "
            SQL = SQL & "when isnull(( select top 1 z.Flag_Ready_For_Packaging from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = a.No_Production_Order "
            SQL = SQL & "and z.No_Batch = c.Tahap and z.Flag_Ready_For_Packaging = 'Y' and z.No_GR = '2' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' "
            SQL = SQL & "then 'READY FOR PACKING' "
            SQL = SQL & "when isnull(( select top 1 z.Kode_Perusahaan from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Split = a.No_Production_Order "
            SQL = SQL & "and z.No_Batch = c.Tahap and z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging is null and z.No_GR = '2' order by z.Tahap_Military_Sampling DESC), 'U') = 'Y' "
            SQL = SQL & "then 'HOLD' else 'NO DATA' end as Status_Split "

            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Split_Production_Order b, Emi_Production_Results_Validation_Detail c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and a.No_Transaksi = c.No_Transaksi "
            SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Status is null and b.Status is null "
            'SQL = SQL & "and b.Flag_Hasil_Produksi_GR2 = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Faktur & "' "
            SQL = SQL & "and c.Tahap = " & Batch & " "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    SoAwal = Dr("Kode_Stock_Owner_Awal")
                    SoTujuan = Dr("Kode_Stock_Owner_Tujuan")
                    TglProduksi = Dr("Tanggal_Produksi")

                    Dim Lv As ListViewItem
                    Lv = Lv_DataDestail.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Dr("Barcode"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Jenis"))
                    Lv.SubItems.Add(Dr("Status_Split"))

                    If General_Class.CekNULL(Dr("Status_Split")).ToUpper = "DITOLAK" Then
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
                    ElseIf General_Class.CekNULL(Dr("Status_Split")).ToUpper = "DITERIMA" Then
                        Lv.BackColor = Color.LightGreen
                    ElseIf General_Class.CekNULL(Dr("Status_Split")).ToUpper = "READY FOR PACKING" Then
                        Lv.BackColor = Color.LightGreen
                    ElseIf General_Class.CekNULL(Dr("Status_Split")).ToUpper = "HOLD" Then
                        Lv.BackColor = Color.LightYellow
                    Else
                        Lv.BackColor = Color.White
                    End If

                Loop
            End Using

            'Txt_Selected_Transaksi.Text = Faktur
            Txt_SoAwal.Text = SoAwal
            Txt_SOTujuan.Text = SoTujuan
            DTP_Tanggal_Produksi.Value = TglProduksi



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        KosongPanelDetail()

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FLPanel_Data.Controls
            If TypeOf ctrl Is Panel Then
                CType(ctrl, Panel).BackColor = Color.White
            End If
        Next

        FLPanel_Data.Focus()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If SelectedFaktur.Trim.Length = 0 Then
            MessageBox.Show("No Faktur Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Lv_DataDestail.Items.Count = 0 Then
            MessageBox.Show("Tidak Ada Data yang Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If MessageBox.Show("Yakin Ingin Validasi Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

        If Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus() : Exit Sub
        End If

        get_jam()


        '==============================================
        '=     CEK APAKAH ADA DATA HOLD / DITOLAK     =
        '==============================================

        For i As Integer = 0 To Lv_DataDestail.Items.Count
            GetDataLvDetail(i)

            If Not Lv_Status.ToUpper = "DITERIMA" Then
                MessageBox.Show("Terdapat Data yang Ditolak atau Dihold", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

        Next

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            get_no_faktur()

            '=================================================
            '=     CEK APAKAH DATA SUDAH MASUK STEP GR 2     =
            '=================================================
            Dim NoSplit As String = ""
            SQL = "select a.No_Transaksi, a.no_Production_Order, a.Kode_Perusahaan, b.Flag_Hasil_Produksi_GR2 "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Split_Production_Order b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and a.Status is null and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & SelectedFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Hasil_Produksi_GR2")) = "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No Faktur Belum Melalui Proses Validasi Penerimaan Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    NoSplit = Dr("no_Production_Order")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Faktur Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================
            '=     INSERT RECORD     =
            '=========================
            SQL = "insert into N_EMI_Transaksi_Validasi_Masa_Inkubasi (Kode_Perusahaan, No_Transaksi, No_Faktur_Penerimaan_Barang, No_Split, Tanggal, Jam, Keterangan, UserID, Tahap) "
            SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & SelectedFaktur & "', '" & NoSplit & "',  "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & Txt_Keterangan.Text & "', '" & UserID & "', " & SelectedBatch & ")"
            ExecuteTrans(SQL)

            '==================================
            '=     UPDATE FLAG BLOK STOCK     =
            '==================================
            SQL = "select a.Kode_Perusahaan, a.No_Transaksi, b.Flag_Hasil_Produksi_GR2, c.Kode_Stock_Owner_Awal, c.Kode_Stock_Owner_Tujuan, c.Kode_Barang, "
            SQL = SQL & "c.Serial_Number_Awal, c.Serial_Number_Tujuan, c.Warna, c.Jumlah, c.Satuan, c.Jenis, c.Batch_Number, c.tahap "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Split_Production_Order b, Emi_Production_Results_Validation_Detail c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and a.No_Transaksi = c.No_Transaksi "
            'SQL = SQL & "and b.Flag_Hasil_Produksi_GR2 = 'Y' "
            SQL = SQL & "and a.Status is null and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & SelectedFaktur & "' "
            SQL = SQL & "and c.tahap = " & SelectedBatch & " "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            If .Rows(i).Item("Jenis").ToString.ToUpper = "FINISHED GOOD" Then

                                '===================================
                                '=     CEK APAKAH SN DITEMUKAN     =
                                '===================================
                                SQL = "select Kode_Perusahaan from barang_sn_sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' "
                                SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and Serial_Number = '" & .Rows(i).Item("Serial_Number_Tujuan") & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Not Dr.Read Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '==========================
                                '=     UPDATE FLAG SN     =
                                '==========================
                                SQL = "select Kode_Perusahaan from barang_sn_sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' "
                                SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and Serial_Number = '" & .Rows(i).Item("Serial_Number_Tujuan") & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dr.Close()
                                        SQL = "update barang_sn_sementara set Blok_SN = NULL where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' "
                                        SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and Serial_Number = '" & .Rows(i).Item("Serial_Number_Tujuan") & "' "
                                        ExecuteTrans(SQL)

                                    End If
                                End Using

                            Else

                                '===================================
                                '=     CEK APAKAH SN DITEMUKAN     =
                                '===================================
                                SQL = "select Kode_Perusahaan from barang_sn where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' "
                                SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and Serial_Number = '" & .Rows(i).Item("Serial_Number_Tujuan") & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Not Dr.Read Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '==========================
                                '=     UPDATE FLAG SN     =
                                '==========================
                                SQL = "select Kode_Perusahaan from barang_sn where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' "
                                SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and Serial_Number = '" & .Rows(i).Item("Serial_Number_Tujuan") & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dr.Close()
                                        SQL = "update barang_sn set Blok_SN = NULL where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "' "
                                        SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and Serial_Number = '" & .Rows(i).Item("Serial_Number_Tujuan") & "' "
                                        ExecuteTrans(SQL)

                                    End If
                                End Using

                            End If

                            '================================
                            '=     INSERT RECORD DETAIL     =
                            '================================
                            SQL = "insert into N_EMI_Transaksi_Validasi_Masa_Inkubasi_Detail (Kode_perusahaan, No_Transaksi, Kode_Stock_Owner_Awal, Kode_Stock_Owner_Tujuan, Kode_Barang, Serial_Number_Awal, "
                            SQL = SQL & "Serial_Number_tujuan, Batch_Number, Warna, Jumlah, Satuan, Jenis, Tahap) "
                            SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & .Rows(i).Item("Kode_Stock_Owner_Awal") & "', '" & .Rows(i).Item("Kode_Stock_Owner_Tujuan") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Serial_Number_Awal") & "', '" & .Rows(i).Item("Serial_Number_Tujuan") & "', '" & .Rows(i).Item("Batch_Number") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("Warna") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', '" & .Rows(i).Item("Jenis") & "', " & .Rows(i).Item("Tahap") & ") "
                            ExecuteTrans(SQL)


                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            SQL = "select distinct a.Kode_Perusahaan, a.No_Transaksi, b.Tahap "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Flag_Validasi_Inkubasi is null "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & SelectedFaktur & "' "
            SQL = SQL & "AND b.Tahap NOT IN ( "
            SQL = SQL & "select Tahap from N_EMI_Transaksi_Validasi_Masa_Inkubasi z "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Faktur_Penerimaan_Barang = a.No_Transaksi and a.Status is null and z.status is null ) "
            SQL = SQL & "order by b.Tahap "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    SQL = "update Emi_Production_Results_Validation set Flag_Validasi_Inkubasi = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Transaksi = '" & SelectedFaktur & "' "
                    ExecuteTrans(SQL)
                End If
            End Using

            'SQL = "select Kode_Perusahaan from Emi_Production_Results_Validation where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Transaksi = '" & SelectedFaktur & "'"
            'Using dr = OpenTrans(SQL)
            '    If dr.Read Then
            '        dr.Close()
            '        SQL = "update Emi_Production_Results_Validation set Flag_Validasi_Inkubasi = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Transaksi = '" & SelectedFaktur & "' "
            '        ExecuteTrans(SQL)
            '    Else
            '        dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Data Penerimaan Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ReloadForm()

    End Sub


    '=======================================================================================================================================================
    '=     HANDLE KEYPRESS
    '=======================================================================================================================================================
    Private Sub Cmb_Filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter.KeyPress
        If e.KeyChar = Chr(13) Then Txt_FilterValue.Focus() : Exit Sub
    End Sub

    Private Sub Txt_FilterValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_FilterValue.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus() : Exit Sub
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus() : Exit Sub
    End Sub



End Class

