Public Class N_EMI_SD_Production_Tracker_GR_1

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

        Lv_Detail_Packaging.Columns.Clear()
        Lv_Detail_Packaging.Columns.Add("Kode Barang", 180, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Barang", 300, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Nilai Formula", 180, HorizontalAlignment.Right)
        Lv_Detail_Packaging.Columns.Add("Nilai Produksi", 180, HorizontalAlignment.Right)
        Lv_Detail_Packaging.Columns.Add("Satuan", 130, HorizontalAlignment.Left)
        Lv_Detail_Packaging.View = View.Details

        Lv_Detail_Scrap.Columns.Clear()
        Lv_Detail_Scrap.Columns.Add("Kode Barang", 180, HorizontalAlignment.Left)
        Lv_Detail_Scrap.Columns.Add("Barang", 300, HorizontalAlignment.Left)
        Lv_Detail_Scrap.Columns.Add("Barcode", 300, HorizontalAlignment.Left)
        Lv_Detail_Scrap.Columns.Add("Jumlah", 180, HorizontalAlignment.Right)
        Lv_Detail_Scrap.Columns.Add("Satuan", 130, HorizontalAlignment.Left)
        Lv_Detail_Scrap.View = View.Details





        Kosong()
    End Sub

    Public Sub Kosong()



        Txt_NoTransaksi.Text = ""
        Txt_JumlahBatch.Text = ""
        Txt_NoSplit.Text = ""
        Txt_KdBarang.Text = ""
        Txt_NmBarang.Text = ""
        Txt_HasilProduksi.Text = ""
        Txt_Good_Stock.Text = ""
        Txt_Bad_Stock.Text = ""
        Txt_Scrap.Text = ""



        Dtp_Tgl.Value = Now.Date

        Lv_Batch.Rows.Clear()
        Lv_Detail_Packaging.Items.Clear()
        Lv_Detail_Scrap.Items.Clear()




        LoadSplit(SelectedPO)
    End Sub

    Private Sub LoadSplit(ByVal NoPO As String)

        Try
            OpenConn()

            FlPanel_Split.Controls.Clear()
            SQL = "select distinct a.No_Transaksi, a.No_Production_Order, a.Tanggal, a.UserID "
            SQL = SQL & "from Emi_Production_Results a, emi_production_results_detail_barang b, Emi_Split_Production_Order c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Production_Order = c.No_Transaksi "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null and b.tanggal is not null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.No_PO = '" & NoPO & "' "
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
                            lblTgl.Text = Char.ConvertFromUtf32(&H1F4C5) & " Tanggal Split: " & If(General_Class.CekNULL(.Rows(i).Item("Tanggal")) = "", "-", Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
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

#Region "Input Keterangan"
                            '' Label Keterangan
                            'Dim Keterangan As New Label()
                            'Keterangan.Text = Char.ConvertFromUtf32(&H1F4DD) & " Keterangan : "
                            'Keterangan.AutoSize = True
                            'Keterangan.MinimumSize = New Size(185, 0)
                            'Keterangan.MaximumSize = New Size(185, 0)
                            'Keterangan.Location = New Point(5, 68)
                            'Keterangan.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            'Keterangan.BackColor = Color.Transparent
                            'AddHandler Keterangan.Click, AddressOf HandleSplitItemClick
                            'card.Controls.Add(Keterangan)

                            '' Label Keterangan
                            'Dim Keterangan2 As New Label()
                            'Dim originalText As String = If(General_Class.CekNULL(.Rows(i).Item("keterangan")) = "", "-", .Rows(i).Item("keterangan"))

                            'Keterangan2.TextAlign = ContentAlignment.TopLeft
                            'Keterangan2.AutoSize = True
                            'Keterangan2.MinimumSize = New Size(185, 0)
                            'Keterangan2.MaximumSize = New Size(185, 0)
                            'Keterangan2.Location = New Point(22, 83)
                            'Keterangan2.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            'Keterangan2.BackColor = Color.Transparent

                            'Dim oneLineHeight As Integer = TextRenderer.MeasureText("A", Keterangan2.Font).Height
                            'Dim maxHeight As Integer = oneLineHeight * 3
                            'Dim size As Size = TextRenderer.MeasureText(originalText, Keterangan2.Font, New Size(Keterangan2.Width, 0), TextFormatFlags.WordBreak)

                            'If size.Height > maxHeight Then
                            '    ' Cari titik potong teks agar pas 3 baris
                            '    Dim textToShow As String = originalText
                            '    While TextRenderer.MeasureText(textToShow & "...", Keterangan2.Font, New Size(Keterangan2.Width, 0), TextFormatFlags.WordBreak).Height > maxHeight AndAlso textToShow.Length > 0
                            '        textToShow = textToShow.Substring(0, textToShow.Length - 1)
                            '    End While
                            '    Keterangan2.Text = textToShow & "..."
                            'Else
                            '    Keterangan2.Text = originalText
                            'End If
                            'AddHandler Keterangan2.Click, AddressOf HandleSplitItemClick
                            'card.Controls.Add(Keterangan2)

#End Region





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


    Private Sub LoadDataStep(ByVal NoResult As String)

        Try
            OpenConn()

            SQL = "select top 1 a.No_Transaksi, a.No_Production_Order, b.Tanggal, b.Jam, a.UserID, b.Proses "
            SQL = SQL & "from Emi_Production_Results a, emi_production_results_detail_barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.status is null and b.tanggal is not null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & NoResult & "' "
            SQL = SQL & "order by b.Proses DESC"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_NoTransaksi.Text = Dr("No_Transaksi")
                    Txt_NoSplit.Text = Dr("No_Production_Order")
                    Txt_JumlahBatch.Text = Format(Dr("Proses"), "N0")
                    Dtp_Tgl.Value = If(General_Class.CekNULL(Dr("Tanggal")) = "", New DateTime(1900, 1, 1), Dr("Tanggal"))
                Else
                    Txt_NoTransaksi.Text = ""
                    Txt_NoSplit.Text = ""
                    Txt_JumlahBatch.Text = ""
                    Dtp_Tgl.Value = Now.Date
                End If
            End Using

            Lv_Batch.Rows.Clear() : Lv_Detail_Packaging.Items.Clear() : Lv_Detail_Scrap.Items.Clear()
            SQL = "select No_Transaksi, Proses from emi_production_results_detail_barang where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & NoResult & "' and tanggal is not null and status is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Lv_Batch.Rows.Add(1)
                            Lv_Batch.Rows(i).Cells(0).Value = .Rows(i).Item("Proses")
                            Lv_Batch.Rows(i).Cells(1).Value = .Rows(i).Item("No_Transaksi")

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

    End Sub





    '=======================================================================================================================================================================================================================
    '=     UTIL
    '=======================================================================================================================================================================================================================
    Private Sub DrawPanelBorder(pnl As Panel, e As PaintEventArgs, warna As Color, tebal As Integer)
        Using p As New Pen(warna, tebal)
            e.Graphics.DrawRectangle(p, 0, 0, pnl.Width - 1, pnl.Height - 1)
        End Using
    End Sub

    Private Sub Lv_Batch_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Lv_Batch.CellClick
        If Lv_Batch.Rows.Count = 0 Then Exit Sub

        Try
            OpenConn()

            Dim SelectedProses As String = Lv_Batch.CurrentRow.Cells(0).Value
            Dim SelectedFaktur As String = Lv_Batch.CurrentRow.Cells(1).Value


            SQL = "select b.Kode_Barang, c.Nama As Nama_Barang, b.Qty_Hasil_Produksi, b.Qty_Good_Stock, b.Qty_Bad_Stock, b.Qty_Scrap, b.Satuan, b.Satuan_Scrap "
            SQL = SQL & "from Emi_Production_Results a, emi_production_results_detail_barang b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & SelectedFaktur & "' "
            SQL = SQL & "and b.Proses = '" & SelectedProses & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_KdBarang.Text = Dr("Kode_Barang")
                    Txt_NmBarang.Text = Dr("Nama_Barang")
                    Txt_HasilProduksi.Text = $"{Format(Dr("Qty_Hasil_Produksi"), "N0")}  {Dr("Satuan")}"
                    Txt_Good_Stock.Text = $"{Format(Dr("Qty_Good_Stock"), "N0")}  {Dr("Satuan")}"
                    Txt_Bad_Stock.Text = $"{Format(Dr("Qty_Bad_Stock"), "N0")}  {Dr("Satuan")}"
                    Txt_Scrap.Text = $"{Format(Dr("Qty_Scrap"), "N0")}  {Dr("Satuan_Scrap")}"
                Else
                    Txt_KdBarang.Text = ""
                    Txt_NmBarang.Text = ""
                    Txt_HasilProduksi.Text = ""
                    Txt_Good_Stock.Text = ""
                    Txt_Bad_Stock.Text = ""
                    Txt_Scrap.Text = ""
                End If
            End Using



            Lv_Detail_Packaging.Items.Clear()
            SQL = "select b.Kode_Barang, c.Nama As Nama_Barang, b.Nilai_Formula, b.Nilai_Produksi, b.Satuan "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Packaging_Detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & SelectedFaktur & "' "
            SQL = SQL & "and b.Proses = '" & SelectedProses & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Packaging.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Nilai_Formula"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Nilai_Produksi"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            Lv_Detail_Scrap.Items.Clear()
            SQL = "select d.Kode_Barang, d.Nama As Nama_Barang, b.Jumlah, b.Satuan, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Scrap b, Barang_SN c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Serial_Number = c.Serial_Number "
            SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & SelectedFaktur & "' "
            SQL = SQL & "and b.Proses = '" & SelectedProses & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Scrap.Items.Add(Dr("Kode_Barang"))
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

    Private Sub Panel_Isi_Paint(sender As Object, e As PaintEventArgs) Handles Panel_Isi.Paint
        'DrawPanelBorder(Panel_Isi, e, Color.LightGray, 1)
    End Sub

End Class