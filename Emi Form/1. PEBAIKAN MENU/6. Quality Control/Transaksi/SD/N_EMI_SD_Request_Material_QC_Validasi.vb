Public Class N_EMI_SD_Request_Material_QC_Validasi

    Private selectedCard As Panel = Nothing

    Dim DataParent As New List(Of (NoFaktur As String, NoSplit As String, Tanggal As Date, KodeBarang As String, Keterangan As String))


    Private Sub N_EMI_SD_Request_Material_QC_Validasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Lv_Data.Columns.Clear()
        'Lv_Data.Columns.Add("No Faktur", 120, HorizontalAlignment.Left)
        'Lv_Data.Columns.Add("No Split", 120, HorizontalAlignment.Left)
        'Lv_Data.Columns.Add("Gudang Request", 150, HorizontalAlignment.Left)
        'Lv_Data.Columns.Add("Gudang Tujuan", 150, HorizontalAlignment.Left)
        'Lv_Data.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        'Lv_Data.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
        'Lv_Data.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        'Lv_Data.Columns.Add("Jumlah Tambahan", 150, HorizontalAlignment.Right)
        'Lv_Data.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        'Lv_Data.View = View.Details

        'Lv_Data.Columns.Clear()
        'Lv_Data.Columns.Add("No Faktur", 150, HorizontalAlignment.Left) '0
        'Lv_Data.Columns.Add("No Split", 150, HorizontalAlignment.Left) '1
        'Lv_Data.Columns.Add("Tanggal", 120, HorizontalAlignment.Center) '2
        'Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left) '3
        'Lv_Data.Columns.Add("Keterangan", 330, HorizontalAlignment.Left) '4
        'Lv_Data.View = View.Details

        'Lv_Detail.Columns.Clear()
        'Lv_Detail.Columns.Add("", 0, HorizontalAlignment.Left) '0
        'Lv_Detail.Columns.Add("Batch", 78, HorizontalAlignment.Center) '1
        'Lv_Detail.Columns.Add("NoFaktur", 0, HorizontalAlignment.Left) '2
        'Lv_Detail.Columns.Add("UrutDetail", 0, HorizontalAlignment.Left) '3
        'Lv_Detail.View = View.Details

        Lv_Det.Columns.Clear()
        'Lv_Det.Columns.Add("Lokasi Barang", 130, HorizontalAlignment.Left) '0
        Lv_Det.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '1
        Lv_Det.Columns.Add("Nama Barang ", 230, HorizontalAlignment.Left) '2
        Lv_Det.Columns.Add("Jumlah Permintaan", 180, HorizontalAlignment.Right) '3
        Lv_Det.Columns.Add("Jumlah Transfer", 180, HorizontalAlignment.Right) '4
        Lv_Det.Columns.Add("Jumlah Transfer Bags", 0, HorizontalAlignment.Right) '5
        Lv_Det.Columns.Add("Satuan", 80, HorizontalAlignment.Center).DisplayIndex = 2
        Lv_Det.View = View.Details

        FlowLayoutPanelSidebar.BorderStyle = BorderStyle.None

        Kosong()
    End Sub

    Private Sub Kosong()

        FlowLayoutPanelSidebar.Controls.Clear()
        Lv_Detail.Rows.Clear()
        Txt_Keterangan.Text = ""
        Lv_Det.Items.Clear()

        LoadData()
        LoadSidebar()
    End Sub


    Private Sub LoadData()
        Try
            OpenConn()

            'Lv_Data.Items.Clear()
            'SQL = "select a.Kode_Perusahaan, a.No_Faktur, a.No_Faktur_Order, c.Kode_Stock_Owner as Kd_SO_Requester, c.Kode_Stock_Owner_Tujuan as Kd_SO_Destination,  "
            'SQL = SQL & "c.Kode_Barang, d.Nama as Nama_Barang, isnull(sum(c.Jumlah_Per_Batch), 0) as Jumlah_Per_Batch, isnull(sum(c.Jumlah_Tambah), 0) as Jumlah_Tambah, c.Satuan "
            'SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a,N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c, Barang d "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            'SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail "
            'SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and a.Status is null and b.Flag_Terpenuhi is null and c.Flag_Terpenuhi is null "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur, a.No_Faktur_Order, c.Kode_Stock_Owner, c.Kode_Stock_Owner_Tujuan, c.Kode_Barang, d.Nama, c.Satuan "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lv As ListViewItem
            '        Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
            '        Lv.SubItems.Add(Dr("No_Faktur_Order"))
            '        Lv.SubItems.Add(Dr("Kd_SO_Requester"))
            '        Lv.SubItems.Add(Dr("Kd_SO_Destination"))
            '        Lv.SubItems.Add(Dr("Kode_Barang"))
            '        Lv.SubItems.Add(Dr("Nama_Barang"))
            '        Lv.SubItems.Add(Format(Dr("Jumlah_Per_Batch"), "N4"))
            '        Lv.SubItems.Add(Format(Dr("Jumlah_Tambah"), "N4"))
            '        Lv.SubItems.Add(Dr("Satuan"))
            '    Loop
            'End Using

            'Lv_Data.Items.Clear() : Lv_Detail.Items.Clear() : Lv_Det.Items.Clear()
            'SQL = "select a.Kode_Perusahaan, a.No_Faktur, a.No_Faktur_Order, a.Tanggal, a.Jam, a.Kode_Barang, b.Nama as Nm_Barang, a.Keterangan, a.UserId, a.Status, a.Flag_Selesai "
            'SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, Barang b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            'SQL = SQL & "and a.Status is null and a.Flag_Selesai is null "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lv As ListViewItem
            '        Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
            '        Lv.SubItems.Add(Dr("No_Faktur_Order"))
            '        Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
            '        Lv.SubItems.Add(Dr("Kode_Barang"))
            '        'Lv.SubItems.Add(Dr("Nm_Barang"))
            '        Lv.SubItems.Add(Dr("Keterangan"))
            '    Loop
            'End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub






    '===============================================================================================================================
    '=     SIDEBAR     
    '===============================================================================================================================
    Private Sub LoadSidebar()

        Try
            OpenConn()

            '===================================
            '=     KOSONGKAN ISI FLOWPANEL     =
            '===================================
            FlowLayoutPanelSidebar.Controls.Clear()
            DataParent.Clear()
            SQL = "select a.Kode_Perusahaan, a.No_Faktur, a.No_Faktur_Order, a.Tanggal, a.Jam, a.Kode_Barang, b.Nama as Nm_Barang, a.Keterangan, a.UserId, a.Status, a.Flag_Selesai "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, Barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Status is null and a.Flag_Selesai is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    DataParent.Add((Dr("No_Faktur"), Dr("No_Faktur_Order"), Dr("Tanggal"), Dr("Kode_Barang"), Dr("Keterangan")))
                Loop
            End Using

            Dim PanelSizeWidth As Double = FlowLayoutPanelSidebar.Width - 30

            If DataParent.Count > 4 Then
                PanelSizeWidth = FlowLayoutPanelSidebar.Width - 30
            Else
                PanelSizeWidth = FlowLayoutPanelSidebar.Width - 10
            End If

            For Each Datas In DataParent

                Dim card As New Panel()
                card.Size = New Size(PanelSizeWidth, 100)
                card.BackColor = Color.White
                card.BorderStyle = BorderStyle.FixedSingle
                card.Cursor = Cursors.Hand
                card.BackgroundImageLayout = ImageLayout.Stretch
                card.Tag = Datas.NoFaktur
                AddHandler card.Click, AddressOf Card_Click
                card.Margin = New Padding(5)

                '=====================================
                '=     TAMBAH KOMPONEN PADA CARD     =
                '=====================================
                ' Label No Faktur
                Dim lblNoFaktur As New Label()
                lblNoFaktur.Text = Datas.NoFaktur
                lblNoFaktur.Font = New Font("Work Sans", 9, FontStyle.Bold)
                lblNoFaktur.Location = New Point(5, 10)
                lblNoFaktur.AutoSize = True
                lblNoFaktur.BackColor = Color.Transparent
                AddHandler lblNoFaktur.Click, AddressOf Item_Card_Click
                card.Controls.Add(lblNoFaktur)

                ' Label No Faktur
                Dim lblNoSplit As New Label()
                lblNoSplit.Text = Datas.NoSplit
                lblNoSplit.Font = New Font("Work Sans", 8, FontStyle.Regular)
                lblNoSplit.Location = New Point(5, 30)
                lblNoSplit.AutoSize = True
                lblNoSplit.BackColor = Color.Transparent
                AddHandler lblNoSplit.Click, AddressOf Item_Card_Click
                card.Controls.Add(lblNoSplit)

                ' Label Tanggal
                Dim lblTgl As New Label()
                lblTgl.Text = Char.ConvertFromUtf32(&H1F4C5) & " Tanggal Request: " & Format(Datas.Tanggal, "dd MMM yyyy")
                lblTgl.Font = New Font("Work Sans", 8, FontStyle.Regular)
                'lblTglRekrut.Location = New Point(10, 63)
                lblTgl.Location = New Point(5, 50)
                lblTgl.AutoSize = True
                lblTgl.BackColor = Color.Transparent
                AddHandler lblTgl.Click, AddressOf Item_Card_Click
                card.Controls.Add(lblTgl)


                ' Label Kode Barang
                Dim KodeBarang As New Label()
                KodeBarang.Text = Char.ConvertFromUtf32(&H1F4E6) & " Kode Barang : " & Datas.KodeBarang
                KodeBarang.Font = New Font("Work Sans", 8, FontStyle.Regular)
                KodeBarang.Location = New Point(5, 70)
                KodeBarang.AutoSize = True
                KodeBarang.BackColor = Color.Transparent
                AddHandler KodeBarang.Click, AddressOf Item_Card_Click
                card.Controls.Add(KodeBarang)


                '' Label Keterangn
                'Dim Keterangan As New Label()
                'Keterangan.Text = "Keterangan : " & Datas.KodeBarang
                'Keterangan.Font = New Font("Work Sans", 8, FontStyle.Regular)
                'Keterangan.Location = New Point(10, 90)
                'Keterangan.AutoSize = True
                'Keterangan.BackColor = Color.Transparent
                'card.Controls.Add(Keterangan)



                FlowLayoutPanelSidebar.Controls.Add(card)


            Next

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub

    '==============================================================================================================================
    '=     HANDLER
    '==============================================================================================================================

    Private Sub Card_Click(sender As Object, e As EventArgs)

        Dim clickedCard As Panel = CType(sender, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FlowLayoutPanelSidebar.Controls
            If TypeOf ctrl Is Panel Then
                CType(ctrl, Panel).BackColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.LightBlue

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedCard.Tag.ToString()
        LoadDetail(NoFaktur)
    End Sub

    Private Sub Item_Card_Click(sender As Object, e As EventArgs)

        Dim panelInduk As Panel = TryCast(sender.Parent, Panel)
        Dim clickedCard As Panel = CType(panelInduk, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FlowLayoutPanelSidebar.Controls
            If TypeOf ctrl Is Panel Then
                CType(ctrl, Panel).BackColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.LightBlue

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedCard.Tag.ToString()
        LoadDetail(NoFaktur)
    End Sub


    Private Sub LoadDetail(ByVal NoFaktur As String)
        Try
            OpenConn()

            Lv_Detail.Rows.Clear()
            Lv_Det.Items.Clear()
            SQL = "select a.No_Faktur, a.keterangan, b.Batch, b.Qty_PerBatch, b.Total_Tambah, b.Flag_Terpenuhi, b.Urut_Oto, "
            SQL = SQL & "isnull(( select top 1 Satuan from N_EMI_Transaksi_Material_Requisition_QC_Det z "
            SQL = SQL & "where b.Kode_Perusahaan = z.Kode_Perusahaan and b.No_Faktur = z.No_Faktur and z.Urut_Detail = b.Urut_Oto ), '-') as Satuan "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.no_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoFaktur & "' "
            SQL = SQL & "order by b.Batch"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Txt_Keterangan.Text = .Rows(i).Item("keterangan")


                            Lv_Detail.Rows.Add(1)
                            Lv_Detail.Rows(i).Cells(0).Value = Format(.Rows(i).Item("Batch"), "N0")
                            Lv_Detail.Rows(i).Cells(1).Value = .Rows(i).Item("No_Faktur")
                            Lv_Detail.Rows(i).Cells(2).Value = .Rows(i).Item("Urut_Oto")

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Terpenuhi")) = "Y" Then
                                Lv_Detail.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                                Lv_Detail.Rows(i).DefaultCellStyle.ForeColor = Color.Gray
                                Lv_Detail.Rows(i).Cells(3).Value = "Y"
                            Else
                                Lv_Detail.Rows(i).DefaultCellStyle.BackColor = Color.White
                                Lv_Detail.Rows(i).DefaultCellStyle.ForeColor = Color.Black
                                Lv_Detail.Rows(i).Cells(3).Value = "T"
                            End If
                        Next

                        Lv_Detail.ClearSelection()

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

    Private Sub Lv_Detail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Lv_Detail.CellClick
        If Lv_Detail.Rows.Count = 0 Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Detail.CurrentRow.Cells(1).Value
            Dim SelectedUrutDetail As String = Lv_Detail.CurrentRow.Cells(2).Value

            Lv_Det.Items.Clear()
            SQL = "select a.No_Faktur, c.Kode_Stock_Owner_Tujuan, c.Kode_Barang, d.Nama as Nama_Barang, c.Kebutuhan as Kebutuhan_Request, "
            SQL = SQL & "(isnull(sum(c.Jumlah_Per_Batch), 0) + isnull((sum(c.Jumlah_Tambah)), 0)) as Jumlah_Kebutuhan_Barang_PerBatch, "
            SQL = SQL & "isnull((select sum(z.Jumlah) from N_EMI_Transaksi_Material_Requisition_QC_Validasi z "
            SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan and z.No_Faktur_RM = c.No_Faktur and z.Urut_Det_RM = c.Urut_Oto and z.Flag_Retur is null "
            SQL = SQL & "), 0) as Jumlah_Transfer, "
            SQL = SQL & "isnull((select sum(z.Jumlah_Bags) from N_EMI_Transaksi_Material_Requisition_QC_Validasi z "
            SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan and z.No_Faktur_RM = c.No_Faktur and z.Urut_Det_RM = c.Urut_Oto and z.Flag_Retur is null "
            SQL = SQL & "), 0) as Jumlah_Transfer_Bags, c.Satuan, c.Flag_Terpenuhi "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c, Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail "
            SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & SelectedFaktur & "' and b.Urut_Oto = '" & SelectedUrutDetail & "' "
            SQL = SQL & "group by a.No_Faktur, c.Kode_Stock_Owner_Tujuan, c.Kode_Barang, d.Nama, c.Kebutuhan, c.Kode_Perusahaan, c.No_Faktur, c.Urut_Oto, c.Satuan, c.Flag_Terpenuhi "
            SQL = SQL & "order by c.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Det.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Kebutuhan_Barang_PerBatch"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Transfer"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Transfer_Bags"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))

                    If General_Class.CekNULL(Dr("Flag_Terpenuhi")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
                    Else
                        Lv.BackColor = Color.White
                    End If
                    Lv.ForeColor = Color.Black

                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    '==============================================================================================================================
    '==============================================================================================================================

    Private Sub Lv_Detail_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Lv_Detail.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim dgv As DataGridView = CType(sender, DataGridView)

            If dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected Then
                ' Ambil nilai Flag Terpenuhi
                Dim cellValue As String = dgv.Rows(e.RowIndex).Cells(3).Value?.ToString()

                ' Warna background seleksi berdasarkan nilai cell kolom ke-3
                Dim fillColor As Color = dgv.DefaultCellStyle.BackColor ' default

                If cellValue = "Y" Then
                    fillColor = Color.LightGreen
                End If

                ' Warnai background
                Using bgBrush As New SolidBrush(fillColor)
                    e.Graphics.FillRectangle(bgBrush, e.CellBounds)
                End Using

                ' Gambar isi teks
                e.PaintContent(e.ClipBounds)

                ' Border hitam
                Using pen As New Pen(Color.Black, 2)
                    Dim rect = e.CellBounds
                    rect.Width -= 1
                    rect.Height -= 1
                    e.Graphics.DrawRectangle(pen, rect)
                End Using

                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Lv_Detail_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Lv_Detail.CellMouseEnter
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Lv_Detail.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub Lv_Detail_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Lv_Detail.CellMouseLeave
        Lv_Detail.Cursor = Cursors.Default
    End Sub




End Class
