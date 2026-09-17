Public Class N_EMI_SD_Production_Tracker_Start

    Public SelectedPO As String

    Dim selectedCard As Panel = Nothing

    Dim SelectedSplit As String = ""

    Private Sub N_EMI_SD_Production_Tracker_Start_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FlPanel_Split.AutoSize = True

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang", 180, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Barang", 450, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah", 200, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Data.View = View.Details


        Try
            OpenConn()

            Cmb_Satuan.Items.Clear()
            SQL = "select Satuan from EMI_Satuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Satuan.Items.Add(Dr("Satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
    End Sub

    Public Sub Kosong()



        Txt_NoSplit.Text = ""
        Txt_KdBarang.Text = ""
        Txt_NmBarang.Text = ""
        Txt_Jumlah.Text = ""
        Txt_Operator.Text = ""

        Dtp_Tgl.Value = Now.Date

        Lv_Data.Items.Clear()



        LoadSplit(SelectedPO)
    End Sub

    Private Sub LoadSplit(ByVal NoPO As String)

        Try
            OpenConn()

            FlPanel_Split.Controls.Clear()
            SQL = "select a.No_PO, a.No_Transaksi, a.Tanggal, a.Jam, a.No_Batch, "
            SQL = SQL & "isnull(( select ISNULL(z.Nama, '-') from Emi_Karyawan z where a.Kode_Perusahaan = z.Kode_Perusahaan and z.Id_Karyawan = a.Operator "
            SQL = SQL & "), '-') as Operator "
            SQL = SQL & "from Emi_Split_Production_Order a, emi_order_produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and b.Status is null and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and b.No_Faktur = '" & NoPO & "' "
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

                            ' Label Tanggal
                            Dim lblTgl As New Label()
                            lblTgl.Text = Char.ConvertFromUtf32(&H1F4C5) & " Tanggal Split: " & If(General_Class.CekNULL(.Rows(i).Item("Tanggal")) = "", "-", Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            lblTgl.AutoSize = True
                            lblTgl.MaximumSize = New Size(200, 0)
                            lblTgl.Location = New Point(5, 35)
                            lblTgl.Font = New Font("Work Sans", 7, FontStyle.Bold)
                            lblTgl.BackColor = Color.Transparent
                            AddHandler lblTgl.Click, AddressOf HandleSplitItemClick
                            card.Controls.Add(lblTgl)

                            ' Label Batch
                            Dim LblUser As New Label()
                            LblUser.Text = Char.ConvertFromUtf32(&H26D1) & " Operator : " & If(General_Class.CekNULL(.Rows(i).Item("Operator")) = "", "-", .Rows(i).Item("Operator"))
                            LblUser.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            LblUser.Location = New Point(5, 53)
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


    Private Sub LoadDataStep(ByVal NoSPlit As String)

        Try
            OpenConn()

            SQL = "select a.No_Transaksi, a.Tanggal, a.Kode_Barang, b.Nama as Nama_Barang, a.Jumlah, a.Satuan, c.Nama As Operator "
            SQL = SQL & "from Emi_Split_Production_Order a, Barang b, Emi_Karyawan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Operator = c.Id_Karyawan "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & NoSPlit & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_NoSplit.Text = Dr("No_Transaksi")
                    Txt_KdBarang.Text = Dr("Kode_Barang")
                    Txt_NmBarang.Text = Dr("Nama_Barang")
                    Txt_Jumlah.Text = Format(Dr("Jumlah"), "N0")
                    Dtp_Tgl.Value = Dr("Tanggal")
                    Cmb_Satuan.SelectedItem = Dr("Satuan")
                    Txt_Operator.Text = Dr("Operator")
                End If
            End Using

            Lv_Data.Items.Clear()
            SQL = "select b.kode_Stock_owner, b.Kode_Barang, c.Nama, b.Jumlah, c.Satuan "
            SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Bahan b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Faktur "
            SQL = SQL & "and b.kode_stock_owner = c.kode_stock_owner and b.kode_barang = c.kode_barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & NoSPlit & "' "
            SQL = SQL & "union all "
            SQL = SQL & "select b.kode_Stock_owner, b.Kode_Barang, c.Nama, b.Jumlah, c.Satuan "
            SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Faktur "
            SQL = SQL & "and b.kode_stock_owner = c.kode_stock_owner and b.kode_barang = c.kode_barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & NoSPlit & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("kode_Stock_owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
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