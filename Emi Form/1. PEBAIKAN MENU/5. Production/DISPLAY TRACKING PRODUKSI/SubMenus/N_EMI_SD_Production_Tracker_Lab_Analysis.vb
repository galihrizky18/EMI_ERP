Public Class N_EMI_SD_Production_Tracker_Lab_Analysis

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

        Lv_Data_Sampel.Columns.Clear()
        Lv_Data_Sampel.Columns.Add("No Sub Sampel", 150, HorizontalAlignment.Left)
        Lv_Data_Sampel.Columns.Add("Jenis Analisa", 450, HorizontalAlignment.Left)
        Lv_Data_Sampel.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_Data_Sampel.Columns.Add("Jam", 120, HorizontalAlignment.Center)
        Lv_Data_Sampel.Columns.Add("Status", 130, HorizontalAlignment.Center)
        Lv_Data_Sampel.Columns.Add("Jenis", 130, HorizontalAlignment.Left)
        Lv_Data_Sampel.Columns.Add("User", 150, HorizontalAlignment.Left)
        Lv_Data_Sampel.View = View.Details

        Kosong()
    End Sub

    Public Sub Kosong()



        Txt_No_Split.Text = ""
        Txt_Kd_Barang.Text = ""
        Txt_Nm_Barang.Text = ""
        Txt_Tot_Batch.Text = ""

        Dtp_Tgl.Value = Now.Date
        Lv_Batch.Rows.Clear()

        Txt_No_Sampel.Text = ""
        Dtp_Sampel.Value = Now.Date
        Status_Sampel.Text = ""
        User_Sampel.Text = ""
        Status_Sampel.BackColor = Color.White
        Status_Sampel.ForeColor = Color.Black
        Lv_Data_Sampel.Items.Clear()


        LoadSplit(SelectedPO)
    End Sub

    Private Sub LoadSplit(ByVal NoPO As String)

        Try
            OpenConn()

            FlPanel_Split.Controls.Clear()
            SQL = "select a.No_Transaksi, a.Tanggal, a.UserID "
            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi in ( "
            SQL = SQL & "select distinct z.No_Split_Po from N_EMI_LAB_Hasil_Uji_Validasi_Final z "
            SQL = SQL & "where a.No_Transaksi = z.No_Split_Po and z.No_Po = '" & NoPO & "' ) "
            SQL = SQL & "order by a.No_Transaksi "
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
        LoadDataDetail(NoFaktur)


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
        LoadDataDetail(NoFaktur)
    End Sub


    Private Sub LoadDataDetail(ByVal NoSplit As String)

        Try
            OpenConn()


            SQL = "select Top 1 a.No_Production_Order, d.Tgl_Produksi, d.Kode_Barang, e.Nama as Nama_Barang, c.No_Batch "
            SQL = SQL & "from Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, N_EMI_LAB_Hasil_Uji_Validasi_Final c, Emi_Split_Production_Order d, barang e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.No_Production_Order = c.No_Split_Po "
            SQL = SQL & "and a.No_Production_Order = d.No_Transaksi "
            SQL = SQL & "and b.Proses = c.No_Batch "
            SQL = SQL & "and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and a.Status is null and b.status is null and d.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & NoSplit & "' "
            SQL = SQL & "order by c.No_Batch DESC "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_No_Split.Text = Dr("No_Production_Order")
                    Txt_Kd_Barang.Text = Dr("Kode_Barang")
                    Txt_Nm_Barang.Text = Dr("Nama_Barang")
                    Txt_Tot_Batch.Text = Format(Dr("No_Batch"), "N0")
                    Dtp_Tgl.Value = If(General_Class.CekNULL(Dr("Tgl_Produksi")) = "", New DateTime(1900, 1, 1), Dr("Tgl_Produksi"))
                Else
                    Txt_No_Split.Text = ""
                    Txt_Kd_Barang.Text = ""
                    Txt_Nm_Barang.Text = ""
                    Txt_Tot_Batch.Text = ""
                    Dtp_Tgl.Value = New DateTime(1900, 1, 1)
                End If
            End Using

            Lv_Batch.Rows.Clear() : Lv_Data_Sampel.Items.Clear()
            SQL = "select distinct a.No_Batch, a.No_Sampel, a.No_Split_Po "
            SQL = SQL & "from N_EMI_LAB_Hasil_Uji_Validasi_Final a, N_EMI_LAB_Hasil_Uji_Validasi_Detail_Final b "
            SQL = SQL & "where a.No_Sampel = b.No_Sampel "
            SQL = SQL & "and a.No_Split_Po = '" & NoSplit & "' "
            SQL = SQL & "order by a.No_Batch "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Lv_Batch.Rows.Add(1)
                            Lv_Batch.Rows(i).Cells(0).Value = .Rows(i).Item("No_Batch")
                            Lv_Batch.Rows(i).Cells(1).Value = .Rows(i).Item("No_Sampel")
                            Lv_Batch.Rows(i).Cells(2).Value = .Rows(i).Item("No_Split_Po")

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

            Dim SelectedBatch As String = Lv_Batch.CurrentRow.Cells(0).Value
            Dim SelectedSample As String = Lv_Batch.CurrentRow.Cells(1).Value
            Dim SelectedNoSplit As String = Lv_Batch.CurrentRow.Cells(2).Value


            SQL = "select distinct a.No_Sampel, a.Tanggal, a.Flag_Ok, a.Id_User "
            SQL = SQL & "from N_EMI_LAB_Hasil_Uji_Validasi_Final a, N_EMI_LAB_Hasil_Uji_Validasi_Detail_Final b, N_EMI_LAB_Jenis_Analisa c "
            SQL = SQL & "where a.No_Sampel = b.No_Sampel "
            SQL = SQL & "and b.Id_Jenis_Analisa = c.id "
            SQL = SQL & "and a.No_Split_Po = '" & SelectedNoSplit & "' "
            SQL = SQL & "and a.No_Sampel = '" & SelectedSample & "' "
            SQL = SQL & "and a.No_Batch = '" & SelectedBatch & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_No_Sampel.Text = Dr("No_Sampel")
                    Dtp_Sampel.Value = Dr("Tanggal")

                    If General_Class.CekNULL(Dr("Flag_Ok")) = "Y" Then
                        Status_Sampel.Text = "LOLOS"
                        Status_Sampel.BackColor = Color.LightGreen
                    ElseIf General_Class.CekNULL(Dr("Flag_Ok")) = "T" Then
                        Status_Sampel.Text = "DITOLAK"
                        Status_Sampel.BackColor = Color.DarkRed
                        Status_Sampel.ForeColor = Color.White
                    Else
                        Status_Sampel.Text = "BELUM DI VALIDASI"
                        Status_Sampel.BackColor = Color.White
                        Status_Sampel.ForeColor = Color.Black
                    End If
                    Status_Sampel.Font = New Font(Status_Sampel.Font, FontStyle.Bold)
                    User_Sampel.Text = Dr("Id_User")
                Else
                    Txt_No_Sampel.Text = ""
                    Dtp_Sampel.Value = New DateTime(1900, 1, 1)
                    User_Sampel.Text = ""

                    Status_Sampel.Text = ""
                    Status_Sampel.BackColor = Color.White
                    Status_Sampel.ForeColor = Color.Black
                End If
            End Using



            Lv_Data_Sampel.Items.Clear()
            SQL = "select DISTINCT b.No_Sub_Sampel, b.Id_Jenis_Analisa, c.Jenis_Analisa, b.Tanggal, b.Jam, b.Flag_Layak, b.Flag_Resampling, b.Id_User "
            SQL = SQL & "from N_EMI_LAB_Hasil_Uji_Validasi_Final a, N_EMI_LAB_Hasil_Uji_Validasi_Detail_Final b, N_EMI_LAB_Jenis_Analisa c "
            SQL = SQL & "where a.No_Sampel = b.No_Sampel "
            SQL = SQL & "and b.Id_Jenis_Analisa = c.id "
            SQL = SQL & "and a.No_Split_Po = '" & SelectedNoSplit & "' "
            SQL = SQL & "and a.No_Sampel = '" & SelectedSample & "' "
            SQL = SQL & "and a.No_Batch = '" & SelectedBatch & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data_Sampel.Items.Add(Dr("No_Sub_Sampel"))
                    Lv.SubItems.Add(Dr("Jenis_Analisa"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))

                    If General_Class.CekNULL(Dr("Flag_Layak")) = "Y" Then
                        Lv.SubItems.Add("LAYAK")
                        Lv.BackColor = Color.LightGreen
                    ElseIf General_Class.CekNULL(Dr("Flag_Layak")) = "T" Then
                        Lv.SubItems.Add("TIDAK LAYAK")
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
                    ElseIf General_Class.CekNULL(Dr("Flag_Layak")) = "" Then
                        Lv.SubItems.Add("BELUM DIVALIDASI")
                        Lv.BackColor = Color.White
                        Lv.ForeColor = Color.Black
                    End If

                    If General_Class.CekNULL(Dr("Flag_Resampling")) = "Y" Then
                        Lv.SubItems.Add("RESAMPLING")
                    ElseIf General_Class.CekNULL(Dr("Flag_Resampling")) = "" Then
                        Lv.SubItems.Add("MAIN")
                    End If

                    Lv.SubItems.Add(Dr("Id_User"))
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