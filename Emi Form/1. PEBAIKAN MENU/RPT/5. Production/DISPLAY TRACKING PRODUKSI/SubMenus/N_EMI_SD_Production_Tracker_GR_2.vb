Public Class N_EMI_SD_Production_Tracker_GR_2

    Public SelectedPO As String

    Dim selectedCard As Panel = Nothing

    Dim SelectedSplit As String = ""

    Dim cell_KdBarang As Integer = 0
    Dim cell_NmBarang As Integer = 1
    Dim cell_NilaiFormula As Integer = 2
    Dim cell_NilaiProduksi As Integer = 3
    Dim cell_Satuan As Integer = 4

    Private Sub N_EMI_SD_Production_Tracker_Start_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FlPanel_Split.AutoSize = True

        Lv_Detail_Data.Columns.Clear()
        Lv_Detail_Data.Columns.Add("Lokasi Awal", 140, HorizontalAlignment.Left)
        Lv_Detail_Data.Columns.Add("Lokasi Tujuan", 140, HorizontalAlignment.Left)
        Lv_Detail_Data.Columns.Add("Jenis", 140, HorizontalAlignment.Center)
        Lv_Detail_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Detail_Data.Columns.Add("Barang", 250, HorizontalAlignment.Left)
        Lv_Detail_Data.Columns.Add("Barcode Awal", 230, HorizontalAlignment.Left)
        Lv_Detail_Data.Columns.Add("Barcode Tujuan", 230, HorizontalAlignment.Left)
        Lv_Detail_Data.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        Lv_Detail_Data.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Detail_Data.View = View.Details

        Lv_Detail_Packaging.Columns.Clear()
        Lv_Detail_Packaging.Columns.Add("Kode Barang", 180, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Barang", 300, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Barcode", 300, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Jumlah", 180, HorizontalAlignment.Right)
        Lv_Detail_Packaging.Columns.Add("Satuan", 130, HorizontalAlignment.Center)
        Lv_Detail_Packaging.View = View.Details





        Kosong()
    End Sub

    Public Sub Kosong()



        Txt_NoTransaksi.Text = ""
        Txt_NoSplit.Text = ""
        Txt_Jam.Text = ""
        Txt_Batch.Text = ""
        Rtb_Keterangan.Text = ""
        Txt_Good_Stock.Text = ""
        Txt_Bad_Stock.Text = ""
        Txt_Scrap.Text = ""
        Txt_Tot_Packaging.Text = ""

        Dtp_Tgl.Value = Now.Date

        Lv_Detail_Data.Items.Clear()
        Lv_Detail_Packaging.Items.Clear()




        LoadSplit(SelectedPO)
    End Sub

    Private Sub LoadSplit(ByVal NoPO As String)

        Try
            OpenConn()

            FlPanel_Split.Controls.Clear()
            SQL = "select a.No_Transaksi, a.No_Production_Order, a.Tanggal, a.Jam, a.Keterangan, a.UserID "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Split_Production_Order b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.no_production_order = b.no_transaksi "
            SQL = SQL & "and a.status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_PO = '" & NoPO & "' "
            SQL = SQL & "order by a.No_Production_Order "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Panel_Isi.Visible = True

                        Dim PanelSizeHeight As Double = FlPanel_Split.Height

                        Dim card0 As New Panel()
                        card0.Tag = ""
                        'card.AutoSize = True
                        card0.Size = New Size(5, PanelSizeHeight)
                        card0.BackColor = Color.Transparent
                        card0.BorderStyle = BorderStyle.None
                        card0.BackgroundImageLayout = ImageLayout.Stretch
                        card0.Dock = DockStyle.Top
                        card0.Enabled = False
                        'card.Margin = New Padding(5)
                        card0.Padding = New Padding(0, 0, 0, 10)
                        card0.Margin = New Padding(3, 10, 3, 10)

                        FlPanel_Split.Controls.Add(card0)

                        For i As Integer = 0 To .Rows.Count - 1

                            Dim card As New Panel()
                            card.Tag = .Rows(i).Item("No_Transaksi")
                            card.AutoSize = True
                            card.Size = New Size(100, PanelSizeHeight)
                            card.BackColor = Color.White
                            card.BorderStyle = BorderStyle.FixedSingle
                            card.Cursor = Cursors.Hand
                            card.BackgroundImageLayout = ImageLayout.Stretch
                            card.Dock = DockStyle.Top
                            'card.Margin = New Padding(5)
                            card.Padding = New Padding(0, 0, 0, 10)
                            card.Margin = New Padding(4, 10, 4, 10)
                            AddHandler card.Click, AddressOf HandleSplitClick

                            '=====================================
                            '=     TAMBAH KOMPONEN PADA CARD     =
                            '=====================================
                            ' Label No Faktur
                            Dim lblNoFaktur As New Label()
                            lblNoFaktur.Text = .Rows(i).Item("No_Transaksi")
                            lblNoFaktur.Font = New Font("Work Sans", 9, FontStyle.Bold)
                            lblNoFaktur.Location = New Point(5, 10)
                            lblNoFaktur.AutoSize = True
                            lblNoFaktur.BackColor = Color.Transparent
                            card.Controls.Add(lblNoFaktur)
                            AddHandler lblNoFaktur.Click, AddressOf HandleSplitItemClick

                            ' Label No Split
                            Dim lblNoSplit As New Label()
                            lblNoSplit.Text = .Rows(i).Item("No_Production_Order")
                            lblNoSplit.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            lblNoSplit.Location = New Point(5, 28)
                            lblNoSplit.AutoSize = True
                            lblNoSplit.BackColor = Color.Transparent
                            card.Controls.Add(lblNoSplit)
                            AddHandler lblNoSplit.Click, AddressOf HandleSplitItemClick

                            ' Label Tanggal
                            Dim lblTgl As New Label()
                            lblTgl.Text = Char.ConvertFromUtf32(&H1F4C5) & " Tanggal: " & If(General_Class.CekNULL(.Rows(i).Item("Tanggal")) = "", "-", Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            lblTgl.AutoSize = True
                            lblTgl.MaximumSize = New Size(200, 0)
                            lblTgl.Location = New Point(5, 50)
                            lblTgl.Font = New Font("Work Sans", 7, FontStyle.Bold)
                            lblTgl.BackColor = Color.Transparent
                            AddHandler lblTgl.Click, AddressOf HandleSplitItemClick
                            card.Controls.Add(lblTgl)

                            ' Label Batch
                            Dim LblUser As New Label()
                            LblUser.Text = Char.ConvertFromUtf32(&H1F464) & " User ID : " & If(General_Class.CekNULL(.Rows(i).Item("UserID")) = "", "-", .Rows(i).Item("UserID"))
                            LblUser.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            LblUser.Location = New Point(5, 68)
                            LblUser.AutoSize = True
                            LblUser.BackColor = Color.Transparent
                            card.Controls.Add(LblUser)
                            AddHandler LblUser.Click, AddressOf HandleSplitItemClick





                            FlPanel_Split.Controls.Add(card)

                        Next
                    Else
                        Panel_Isi.Visible = False
                    End If
                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub HandleSplitClick(sender As Object, e As EventArgs)
        Dim clickedCard As Panel = CType(sender, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FlPanel_Split.Controls
            If TypeOf ctrl Is Panel Then
                CType(ctrl, Panel).BackColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.FromArgb(193, 245, 193)

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedCard.Tag.ToString()
        SelectedSplit = NoFaktur
        LoadDataStep(NoFaktur)


    End Sub

    Private Sub HandleSplitItemClick(sender As Object, e As EventArgs)
        Dim panelInduk As Panel = TryCast(sender.Parent, Panel)
        Dim clickedCard As Panel = CType(panelInduk, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FlPanel_Split.Controls
            If TypeOf ctrl Is Panel Then
                CType(ctrl, Panel).BackColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.FromArgb(193, 245, 193)

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedCard.Tag.ToString()
        SelectedSplit = NoFaktur
        LoadDataStep(NoFaktur)
    End Sub


    Private Sub LoadDataStep(ByVal No_Transaksi As String)


        Try
            OpenConn()

            SQL = "select top 1 a.No_Transaksi, a.No_Production_Order, a.Tanggal, a.Jam, a.Keterangan, a.UserID, b.Tahap "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "order by b.Tahap DESC "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_NoTransaksi.Text = Dr("No_Transaksi")
                    Txt_NoSplit.Text = Dr("No_Production_Order")
                    Dtp_Tgl.Value = If(General_Class.CekNULL(Dr("Tanggal")) = "", New DateTime(1900, 1, 1), Dr("Tanggal"))
                    Txt_Jam.Text = If(General_Class.CekNULL(Dr("Jam")) = "", "-", Dr("Jam"))
                    Txt_Batch.Text = Format(Dr("Tahap"), "N0")
                    Rtb_Keterangan.Text = If(General_Class.CekNULL(Dr("Keterangan")) = "", "-", Dr("Keterangan"))

                    '======================================
                    '=     GET SUM FG, REJECT, DSCRAP     =
                    '======================================
                    SQL = "SELECT a.No_Transaksi, "
                    SQL = SQL & "SUM( CASE WHEN b.Jenis IN ('Finished Good','FINISHED GOOD') THEN ROUND(dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',b.Kode_Barang, b.Satuan, 'PCS', b.Jumlah ), 0) "
                    SQL = SQL & "ELSE 0 END) AS Finish_Good, "
                    SQL = SQL & "SUM( CASE WHEN b.Jenis IN ('Rejected','REJECTED') THEN ROUND(dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',b.Kode_Barang, b.Satuan, 'PCS', b.Jumlah ), 0) "
                    SQL = SQL & "ELSE 0 END) AS Rejected, "
                    SQL = SQL & "SUM( "
                    SQL = SQL & "CASE WHEN b.Jenis NOT IN ('Rejected','REJECTED','Finished Good','FINISHED GOOD') THEN round(dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',b.Kode_Barang, b.Satuan, 'PCS', b.Jumlah ), 0) "
                    SQL = SQL & "ELSE 0 END) AS Scrap "
                    SQL = SQL & "FROM Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b "
                    SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "AND a.No_Transaksi = b.No_Transaksi "
                    SQL = SQL & "AND a.Status IS NULL "
                    SQL = SQL & "AND a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND a.No_Transaksi = '" & Dr("No_Transaksi") & "' "
                    SQL = SQL & "GROUP BY a.No_Transaksi; "
                    Dr.Close()
                    Using Dr1 = OpenTrans(SQL)
                        If Dr1.Read Then
                            Txt_Good_Stock.Text = Format(Dr1("Finish_Good"), "N0")
                            Txt_Bad_Stock.Text = Format(Dr1("Rejected"), "N0")
                            Txt_Scrap.Text = Format(Dr1("Scrap"), "N0")
                        Else
                            Txt_Good_Stock.Text = ""
                            Txt_Bad_Stock.Text = ""
                            Txt_Scrap.Text = ""
                        End If
                    End Using
                Else
                    Txt_NoTransaksi.Text = ""
                    Txt_NoSplit.Text = ""
                    Txt_Jam.Text = ""
                    Txt_Batch.Text = ""
                    Rtb_Keterangan.Text = ""
                    Dtp_Tgl.Value = Now.Date
                End If
            End Using


            '========================
            '=     LOAD DATA LV     =
            '========================
            Lv_Detail_Data.Items.Clear()
            SQL = "select b.Kode_Stock_Owner_Awal, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Nama as Nama_Barang, "
            SQL = SQL & "case when b.Jenis IN ('Finished Good','FINISHED GOOD') then 'FINISHED GOOD' "
            SQL = SQL & "when b.Jenis IN ('Rejected','REJECTED')  then 'REJECT' "
            SQL = SQL & "else 'SCRAP' end as Jenis, "
            SQL = SQL & "(d.qr_code + '-' + d.kode_Unik_Berjalan) as Barcode_Awal, "
            SQL = SQL & "(e.qr_code + '-' + e.kode_Unik_Berjalan) as Barcode_Tujuan, "
            SQL = SQL & "ROUND(dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',b.Kode_Barang, b.Satuan, 'PCS', b.Jumlah ), 0) as Jumlah_Pcs, 'PCS' as Satuan "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, barang c, Barang_SN d, Barang_SN e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Kode_Stock_Owner_Awal = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and b.Serial_Number_Awal = d.Serial_Number "
            SQL = SQL & "and b.Serial_Number_Tujuan = e.Serial_Number "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "order by b.Tahap DESC "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Data.Items.Add(Dr("Kode_Stock_Owner_Awal"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner_Tujuan"))
                    Lv.SubItems.Add(Dr("Jenis"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Barcode_Awal"))
                    Lv.SubItems.Add(Dr("Barcode_Tujuan"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Pcs"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using


            Dim Tot_Packaging As Double = 0
            Lv_Detail_Packaging.Items.Clear()
            SQL = "select d.Kode_Barang, e.nama as Nama_Barang, (f.Qr_Code + '-' + f.Kode_Unik_Berjalan) as Barcode, d.Jumlah, c.Satuan "
            SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Emi_Production_Results_Validation_Packaging_Detail c, Emi_Production_Results_Validation_Packaging_Det d, "
            SQL = SQL & "barang e, Barang_SN f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and d.kode_perusahaan = e.kode_perusahaan and d.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Urut = c.Urut_Detail "
            SQL = SQL & "and c.No_Transaksi = d.No_Transaksi and c.Urut = d.No_Urut_Detail "
            SQL = SQL & "and d.kode_stock_owner = e.kode_Stock_owner and d.kode_barang = e.kode_barang "
            SQL = SQL & "and d.Kode_Stock_Owner = f.Kode_Stock_Owner and d.Serial_Number = f.Serial_Number "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "order by b.Tahap DESC "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Packaging.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Barcode"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using





            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub





    '=======================================================================================================================================================================================================================
    '=     UTIL
    '=======================================================================================================================================================================================================================
    Private Sub DrawPanelBorder(pnl As Panel, e As PaintEventArgs, warna As Color, tebal As Integer)
        Using p As New Pen(warna, tebal)
            e.Graphics.DrawRectangle(p, 0, 0, pnl.Width - 1, pnl.Height - 1)
        End Using
    End Sub


    Private Sub Panel_Isi_Paint(sender As Object, e As PaintEventArgs) Handles Panel_Isi.Paint
        'DrawPanelBorder(Panel_Isi, e, Color.LightGray, 1)
    End Sub

End Class