Public Class N_EMI_SD_Production_Tracker_Military_Sampling

    Public SelectedPO As String

    Dim arrSelectedBatch As New List(Of (no_transaksi As String, no_batch As String))
    Dim selectedCard As Panel = Nothing

    Dim SelectedSplit As String = ""

    Dim cell_KdBarang As Integer = 0
    Dim cell_NmBarang As Integer = 1
    Dim cell_NilaiFormula As Integer = 2
    Dim cell_NilaiProduksi As Integer = 3
    Dim cell_Satuan As Integer = 4

    Private Sub N_EMI_SD_Production_Tracker_Start_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FlPanel_Split.AutoSize = True

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Barcode", 300, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("No Keranjang", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tanggal Input", 140, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jumlah Per Keranjang", 180, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Jumlah Waste", 180, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Keterangan", 380, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("User", 140, HorizontalAlignment.Left)
        Lv_Data.View = View.Details

        Try
            OpenConn()

            Cmb_Satuan.Items.Clear()
            SQL = "select Satuan from emi_satuan where Kode_Perusahaan = '" & KodePerusahaan & "'"
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



        Txt_No_Transaksi.Text = ""
        Txt_No_Split.Text = ""
        Txt_Total_Waste_Persen.Text = ""
        Txt_Total_Waste.Text = ""
        Txt_User.Text = ""

        Cmb_Satuan.SelectedIndex = -1

        Dtp_Tgl.Value = Now.Date

        Lv_Data.Items.Clear()
        arrSelectedBatch.Clear()


        LoadSplit(SelectedPO)
    End Sub

    Private Sub LoadSplit(ByVal NoPO As String)

        Try
            OpenConn()

            FlPanel_Split.Controls.Clear() : arrSelectedBatch.Clear()
            SQL = "select d.No_Transaksi, d.No_Split, d.Tanggal, d.No_Batch, d.Userid, d.Flag_Military_Sampling, d.Flag_Ready_For_Packaging "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Emi_Split_Production_Order c, N_EMI_Military_Sampling d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.No_Production_Order = c.No_Transaksi "
            SQL = SQL & "and a.No_Production_Order = d.No_Split "
            SQL = SQL & "and b.Tahap = d.No_Batch "
            SQL = SQL & "and a.Status is null and c.Status is null and d.Status is null "
            SQL = SQL & "and d.No_GR = 1 "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.No_PO = '" & NoPO & "' "
            SQL = SQL & "order by d.No_Transaksi "

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
                            Dim Name As String = ""
                            If General_Class.CekNULL(.Rows(i).Item("Flag_Military_Sampling")) = "Y" And General_Class.CekNULL(.Rows(i).Item("Flag_Ready_For_Packaging")) = "Y" Then
                                Name = "READY"
                            ElseIf General_Class.CekNULL(.Rows(i).Item("Flag_Military_Sampling")) = "Y" And General_Class.CekNULL(.Rows(i).Item("Flag_Ready_For_Packaging")) = "" Then
                                Name = "HOLD"
                            Else
                                Name = "UNDEFINED"
                            End If
                            card.Name = Name
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

                            arrSelectedBatch.Add((.Rows(i).Item("No_Transaksi"), .Rows(i).Item("No_Batch")))

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

                            ' Label No Faktur
                            Dim Lbl_Split As New Label()
                            Lbl_Split.Text = .Rows(i).Item("No_Split")
                            Lbl_Split.Font = New Font("Work Sans", 8, FontStyle.Regular)
                            Lbl_Split.Location = New Point(5, 26)
                            Lbl_Split.AutoSize = True
                            Lbl_Split.BackColor = Color.Transparent
                            card.Controls.Add(Lbl_Split)
                            AddHandler Lbl_Split.Click, AddressOf HandleSplitItemClick


                            ' Label Tanggal
                            Dim lblTgl As New Label()
                            lblTgl.Text = Char.ConvertFromUtf32(&H1F4C5) & " Tanggal Split: " & If(General_Class.CekNULL(.Rows(i).Item("Tanggal")) = "", "-", Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            lblTgl.AutoSize = True
                            lblTgl.MaximumSize = New Size(200, 0)
                            lblTgl.Location = New Point(5, 45)
                            lblTgl.Font = New Font("Work Sans", 7, FontStyle.Bold)
                            lblTgl.BackColor = Color.Transparent
                            AddHandler lblTgl.Click, AddressOf HandleSplitItemClick
                            card.Controls.Add(lblTgl)

                            ' Label Batch
                            Dim Lbl_Batch As New Label()
                            Lbl_Batch.Text = Char.ConvertFromUtf32(&H1F4E6) & " Batch : " & If(General_Class.CekNULL(.Rows(i).Item("No_Batch")) = "", "-", .Rows(i).Item("No_Batch"))
                            Lbl_Batch.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            Lbl_Batch.Location = New Point(5, 60)
                            Lbl_Batch.AutoSize = True
                            Lbl_Batch.BackColor = Color.Transparent
                            card.Controls.Add(Lbl_Batch)
                            AddHandler Lbl_Batch.Click, AddressOf HandleSplitItemClick

                            ' Label Batch
                            Dim Lbl_User As New Label()
                            Lbl_User.Text = Char.ConvertFromUtf32(&H1F464) & " User : " & If(General_Class.CekNULL(.Rows(i).Item("Userid")) = "", "-", .Rows(i).Item("Userid"))
                            Lbl_User.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            Lbl_User.Location = New Point(5, 75)
                            Lbl_User.AutoSize = True
                            Lbl_User.BackColor = Color.Transparent
                            card.Controls.Add(Lbl_User)
                            AddHandler Lbl_User.Click, AddressOf HandleSplitItemClick



                            If General_Class.CekNULL(.Rows(i).Item("Flag_Military_Sampling")) = "Y" And General_Class.CekNULL(.Rows(i).Item("Flag_Ready_For_Packaging")) = "Y" Then
                                card.BackColor = Color.LightGreen
                            ElseIf General_Class.CekNULL(.Rows(i).Item("Flag_Military_Sampling")) = "Y" And General_Class.CekNULL(.Rows(i).Item("Flag_Ready_For_Packaging")) = "" Then
                                card.BackColor = Color.LightYellow
                            Else
                                card.BackColor = Color.White
                            End If


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
                If CType(ctrl, Panel).Name = "READY" Then
                    CType(ctrl, Panel).BackColor = Color.LightGreen
                ElseIf CType(ctrl, Panel).Name = "HOLD" Then
                    CType(ctrl, Panel).BackColor = Color.LightYellow
                Else
                    CType(ctrl, Panel).BackColor = Color.White
                End If
            End If
        Next

        clickedCard.BackColor = Color.FromArgb(193, 245, 193)

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedCard.Tag.ToString()
        Dim Batch As String = arrSelectedBatch.Find(Function(x) x.no_transaksi = NoFaktur).no_batch
        SelectedSplit = NoFaktur
        LoadDataDetail(NoFaktur, Batch)


    End Sub

    Private Sub HandleSplitItemClick(sender As Object, e As EventArgs)
        Dim panelInduk As Panel = TryCast(sender.Parent, Panel)
        Dim clickedCard As Panel = CType(panelInduk, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FlPanel_Split.Controls
            If CType(ctrl, Panel).Name = "READY" Then
                CType(ctrl, Panel).BackColor = Color.LightGreen
            ElseIf CType(ctrl, Panel).Name = "HOLD" Then
                CType(ctrl, Panel).BackColor = Color.LightYellow
            Else
                CType(ctrl, Panel).BackColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.FromArgb(193, 245, 193)

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedCard.Tag.ToString()
        Dim Batch As String = arrSelectedBatch.Find(Function(x) x.no_transaksi = NoFaktur).no_batch
        SelectedSplit = NoFaktur
        LoadDataDetail(NoFaktur, Batch)
    End Sub


    Private Sub LoadDataDetail(ByVal NoTransaksi As String, ByVal Batch As String)

        Try
            OpenConn()



            SQL = "select a.No_Transaksi, a.No_Split, a.Tanggal, a.Persentase_Waste, a.Total_Waste, b.Satuan, a.Userid "
            SQL = SQL & "from N_EMI_Military_Sampling a, N_EMI_Military_Sampling_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_GR = 1 "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & NoTransaksi & "' "
            SQL = SQL & "and a.No_Batch = '" & Batch & "' "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_No_Transaksi.Text = Dr("No_Transaksi")
                    Txt_No_Split.Text = Dr("No_Split")
                    Txt_Total_Waste_Persen.Text = If(General_Class.CekNULL(Dr("Persentase_Waste")) = "", "0", Format(Dr("Persentase_Waste"), "N0")) & " %"
                    Txt_Total_Waste.Text = If(General_Class.CekNULL(Dr("Total_Waste")) = "", "0", Format(Dr("Total_Waste"), "N0"))
                    Txt_User.Text = If(General_Class.CekNULL(Dr("Userid")) = "", "-", Dr("Userid"))
                    If General_Class.CekNULL(Dr("Satuan")) = "" Then
                        Cmb_Satuan.SelectedIndex = -1
                    Else
                        Cmb_Satuan.SelectedItem = Dr("Satuan")
                    End If
                    Dtp_Tgl.Value = If(General_Class.CekNULL(Dr("Tanggal")) = "", New DateTime(1900, 1, 1), Dr("Tanggal"))
                Else
                    Txt_No_Transaksi.Text = ""
                    Txt_No_Split.Text = ""
                    Txt_Total_Waste_Persen.Text = ""
                    Txt_Total_Waste.Text = ""
                    Txt_User.Text = ""
                    Cmb_Satuan.SelectedIndex = -1
                    Dtp_Tgl.Value = New DateTime(1900, 1, 1)
                End If
            End Using

            Lv_Data.Items.Clear()
            SQL = "select(b.Qr_Code + '-' + b.Kode_Unik_Berjalan) as Barcode, b.No_Keranjang, b.Tanggal_Input_Waste, b.Jam_Input_Waste, b.jumlah as Jumlah_Per_Keranjang, b.Jumlah_Waste, b.Keterangan, b.UserId_Input_Waste "
            SQL = SQL & "from N_EMI_Military_Sampling a, N_EMI_Military_Sampling_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_GR = 1 "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & NoTransaksi & "' "
            SQL = SQL & "and a.No_Batch = '" & Batch & "' "
            SQL = SQL & "order by b.No_Keranjang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(If(General_Class.CekNULL(Dr("Barcode")) = "", "-", Dr("Barcode")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("No_Keranjang")) = "", "-", Dr("No_Keranjang")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Input_Waste")) = "", Format(New Date(1900, 1, 1), "dd MMM yyyy"), Format(Dr("Tanggal_Input_Waste"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam_Input_Waste")) = "", "-", Dr("Jam_Input_Waste")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jumlah_Per_Keranjang")) = "", "-", Format(Dr("Jumlah_Per_Keranjang"), "N0")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jumlah_Waste")) = "", "-", Format(Dr("Jumlah_Waste"), "N0")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan")) = "", "-", Dr("Keterangan")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("UserId_Input_Waste")) = "", "-", Dr("UserId_Input_Waste")))
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

    Private Sub Lv_Batch_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        'If Lv_Batch.Rows.Count = 0 Then Exit Sub

        'Try
        '    OpenConn()

        '    Dim SelectedBatch As String = Lv_Batch.CurrentRow.Cells(0).Value
        '    Dim SelectedSample As String = Lv_Batch.CurrentRow.Cells(1).Value
        '    Dim SelectedNoSplit As String = Lv_Batch.CurrentRow.Cells(2).Value


        '    SQL = "select distinct a.No_Sampel, a.Tanggal, a.Flag_Ok, a.Id_User "
        '    SQL = SQL & "from N_EMI_LAB_Hasil_Uji_Validasi_Final a, N_EMI_LAB_Hasil_Uji_Validasi_Detail_Final b, N_EMI_LAB_Jenis_Analisa c "
        '    SQL = SQL & "where a.No_Sampel = b.No_Sampel "
        '    SQL = SQL & "and b.Id_Jenis_Analisa = c.id "
        '    SQL = SQL & "and a.No_Split_Po = '" & SelectedNoSplit & "' "
        '    SQL = SQL & "and a.No_Sampel = '" & SelectedSample & "' "
        '    SQL = SQL & "and a.No_Batch = '" & SelectedBatch & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            Txt_No_Sampel.Text = Dr("No_Sampel")
        '            Dtp_Sampel.Value = Dr("Tanggal")

        '            If General_Class.CekNULL(Dr("Flag_Ok")) = "Y" Then
        '                Status_Sampel.Text = "LOLOS"
        '                Status_Sampel.BackColor = Color.LightGreen
        '            ElseIf General_Class.CekNULL(Dr("Flag_Ok")) = "T" Then
        '                Status_Sampel.Text = "DITOLAK"
        '                Status_Sampel.BackColor = Color.DarkRed
        '                Status_Sampel.ForeColor = Color.White
        '            Else
        '                Status_Sampel.Text = "BELUM DI VALIDASI"
        '                Status_Sampel.BackColor = Color.White
        '                Status_Sampel.ForeColor = Color.Black
        '            End If
        '            Status_Sampel.Font = New Font(Status_Sampel.Font, FontStyle.Bold)
        '            User_Sampel.Text = Dr("Id_User")
        '        Else
        '            Txt_No_Sampel.Text = ""
        '            Dtp_Sampel.Value = New DateTime(1900, 1, 1)
        '            User_Sampel.Text = ""

        '            Status_Sampel.Text = ""
        '            Status_Sampel.BackColor = Color.White
        '            Status_Sampel.ForeColor = Color.Black
        '        End If
        '    End Using



        '    Lv_Data.Items.Clear()
        '    SQL = "select DISTINCT b.No_Sub_Sampel, b.Id_Jenis_Analisa, c.Jenis_Analisa, b.Tanggal, b.Jam, b.Flag_Layak, b.Flag_Resampling, b.Id_User "
        '    SQL = SQL & "from N_EMI_LAB_Hasil_Uji_Validasi_Final a, N_EMI_LAB_Hasil_Uji_Validasi_Detail_Final b, N_EMI_LAB_Jenis_Analisa c "
        '    SQL = SQL & "where a.No_Sampel = b.No_Sampel "
        '    SQL = SQL & "and b.Id_Jenis_Analisa = c.id "
        '    SQL = SQL & "and a.No_Split_Po = '" & SelectedNoSplit & "' "
        '    SQL = SQL & "and a.No_Sampel = '" & SelectedSample & "' "
        '    SQL = SQL & "and a.No_Batch = '" & SelectedBatch & "' "
        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read
        '            Dim Lv As ListViewItem
        '            Lv = Lv_Data.Items.Add(Dr("No_Sub_Sampel"))
        '            Lv.SubItems.Add(Dr("Jenis_Analisa"))
        '            Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
        '            Lv.SubItems.Add(Dr("Jam"))

        '            If General_Class.CekNULL(Dr("Flag_Layak")) = "Y" Then
        '                Lv.SubItems.Add("LAYAK")
        '                Lv.BackColor = Color.LightGreen
        '            ElseIf General_Class.CekNULL(Dr("Flag_Layak")) = "T" Then
        '                Lv.SubItems.Add("TIDAK LAYAK")
        '                Lv.BackColor = Color.DarkRed
        '                Lv.ForeColor = Color.White
        '            ElseIf General_Class.CekNULL(Dr("Flag_Layak")) = "" Then
        '                Lv.SubItems.Add("BELUM DIVALIDASI")
        '                Lv.BackColor = Color.White
        '                Lv.ForeColor = Color.Black
        '            End If

        '            If General_Class.CekNULL(Dr("Flag_Resampling")) = "Y" Then
        '                Lv.SubItems.Add("RESAMPLING")
        '            ElseIf General_Class.CekNULL(Dr("Flag_Resampling")) = "" Then
        '                Lv.SubItems.Add("MAIN")
        '            End If

        '            Lv.SubItems.Add(Dr("Id_User"))
        '        Loop
        '    End Using



        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub Panel_Isi_Paint(sender As Object, e As PaintEventArgs) Handles Panel_Isi.Paint
        'DrawPanelBorder(Panel_Isi, e, Color.LightGray, 1)
    End Sub

End Class