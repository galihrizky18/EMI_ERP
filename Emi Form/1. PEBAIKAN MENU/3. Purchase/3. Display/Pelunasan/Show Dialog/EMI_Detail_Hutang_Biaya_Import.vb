Public Class EMI_Detail_Hutang_Biaya_Import

    Dim selectedCard As Panel = Nothing

    Private Sub EMI_Detail_Hutang_Biaya_Import_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_DetailHutang.Columns.Clear() : Lv_DetailHutang.Items.Clear()
        Lv_DetailHutang.Columns.Add("No Pengajuan", 120, HorizontalAlignment.Left) '0
        Lv_DetailHutang.Columns.Add("Tanggal Bayar", 110, HorizontalAlignment.Center) '1
        Lv_DetailHutang.Columns.Add("Dibayar", 130, HorizontalAlignment.Right) '2
        Lv_DetailHutang.Columns.Add("Kurs Lama", 130, HorizontalAlignment.Right) '3
        Lv_DetailHutang.Columns.Add("Kurs Baru", 130, HorizontalAlignment.Right) '4
        Lv_DetailHutang.Columns.Add("Total Kurs Lama", 130, HorizontalAlignment.Right) '5
        Lv_DetailHutang.Columns.Add("Total Kurs Baru", 130, HorizontalAlignment.Right) '6
        Lv_DetailHutang.Columns.Add("Bank Tujuan", 100, HorizontalAlignment.Left) '7
        Lv_DetailHutang.Columns.Add("Rekening Tujuan", 120, HorizontalAlignment.Left) '8
        Lv_DetailHutang.Columns.Add("Penerima", 150, HorizontalAlignment.Left) '9
        Lv_DetailHutang.View = View.Details

        Try
            OpenConn()

            Cmb_Mata_Uang.Items.Clear()
            SQL = "select kode_Mata_Uang from Mata_Uang where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Mata_Uang.Items.Add(Dr("kode_Mata_Uang"))
                    Cmb_Mata_Uang_Pelunasan.Items.Add(Dr("kode_Mata_Uang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Load_Data_Pengajuan()

        Kosong()
    End Sub
    Public Sub Kosong()

        'Txt_NoPO.Text = ""
        'Txt_TanggalPO.Text = ""
        'Txt_Keterangan.Text = ""
        'Txt_Kategori.Text = ""
        'Txt_Perusahaan.Text = ""
        'Txt_TanggalJatuhTempo.Text = ""
        'Txt_KdPerusahaanBiayaImport.Text = ""
        'Txt_KdMasterKategori.Text = ""
        'Txt_TotalHutang.Text = "0"
        'Txt_TotalBayar.Text = "0"
        'Txt_Sisa.Text = "0"

        Panel_Isi.Visible = False

        Load_Data_Pengajuan()
        Load_Lv()
    End Sub

    Private Sub Load_Data_Pengajuan()

        Try
            OpenConn()

            FLP_Pengajuan.Controls.Clear()
            SQL = "select isnull(a.no_pengajuan, '-') as no_pengajuan, isnull(a.no_val, '-') as no_pelunasan, b.Tanggal_Bayar, isnull(b.Byr, 0) as Byr, isnull(b.Total_Bayar_Kurs_Baru, 0) as Kurs_Baru, isnull(b.Mata_Uang, '-') as Mata_Uang, "
            SQL = SQL & "isnull(( "
            SQL = SQL & "select "
            SQL = SQL & "case when x.Flag_Pengajuan is null and x.Flag_pelunasan is null then 'BELUM VALIDASI' "
            SQL = SQL & "when x.Flag_Pengajuan = 'Y' and x.Flag_pelunasan is null then 'PENGAJUAN' "
            SQL = SQL & "when x.Flag_Pengajuan = 'Y' and x.Flag_pelunasan = 'Y' then 'PELUNASAN' "
            SQL = SQL & "else 'UNDEFINED' "
            SQL = SQL & "end "
            SQL = SQL & "from Pengajuan_Temp z, Detail_Pengajuan_Temp x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Pengajuan = x.No_Pengajuan "
            SQL = SQL & "and z.No_Pengajuan = a.No_Pengajuan "
            SQL = SQL & "and x.Urut_Pelunasan = b.Urut "
            SQL = SQL & "), 'UNDEFINED') as Status_Proses "
            SQL = SQL & "from EMI_Pelunasan a, EMI_Pelunasan_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Val = b.No_Val "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & Txt_NoPO.Text & "' "
            SQL = SQL & "and b.Kode_Perusahaan_Biaya_Import = '" & Txt_KdPerusahaanBiayaImport.Text & "' "
            SQL = SQL & "and b.Kode_Master_Kategori_Biaya_Import = '" & Txt_KdMasterKategori.Text & "' "
            SQL = SQL & "order by b.Urut "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim PanelParent_Height As Double = FLP_Pengajuan.Height

                        For i As Integer = 0 To .Rows.Count - 1

                            Dim card As New Panel()
                            card.Tag = .Rows(i).Item("no_pelunasan")

                            If .Rows(i).Item("Status_Proses") = "BELUM VALIDASI" Then
                                card.Name = "BELUM VALIDASI"
                            ElseIf .Rows(i).Item("Status_Proses") = "PENGAJUAN" Then
                                card.Name = "PENGAJUAN"
                            ElseIf .Rows(i).Item("Status_Proses") = "PELUNASAN" Then
                                card.Name = "PELUNASAN"
                            Else
                                card.Name = "UNDEFINED"
                            End If
                            card.AutoSize = True
                            card.Height = PanelParent_Height
                            card.BackColor = Color.White
                            card.BorderStyle = BorderStyle.FixedSingle
                            card.Cursor = Cursors.Hand
                            card.BackgroundImageLayout = ImageLayout.Stretch
                            card.Dock = DockStyle.Top
                            'card.Margin = New Padding(5)
                            card.Padding = New Padding(0, 0, 0, 10)
                            card.Margin = New Padding(5, 3, 5, 3)
                            AddHandler card.Click, AddressOf HandleCardClick

                            '=====================================
                            '=     TAMBAH KOMPONEN PADA CARD     =
                            '=====================================
                            ' Label No Faktur
                            Dim lblNoFaktur As New Label()
                            lblNoFaktur.Text = .Rows(i).Item("no_pelunasan")
                            lblNoFaktur.Font = New Font("Work Sans", 9, FontStyle.Bold)
                            lblNoFaktur.Location = New Point(5, 7)
                            lblNoFaktur.AutoSize = True
                            lblNoFaktur.BackColor = Color.Transparent
                            card.Controls.Add(lblNoFaktur)
                            AddHandler lblNoFaktur.Click, AddressOf HandleCardItemClick


                            ' Label Tanggal
                            Dim lblTgl As New Label()
                            lblTgl.Text = Char.ConvertFromUtf32(&H1F4C5) & " Tanggal Validasi: " & If(General_Class.CekNULL(.Rows(i).Item("Tanggal_Bayar")) = "", "-", Format(.Rows(i).Item("Tanggal_Bayar"), "dd MMM yyyy"))
                            lblTgl.AutoSize = True
                            lblTgl.MaximumSize = New Size(200, 0)
                            lblTgl.Location = New Point(5, 30)
                            lblTgl.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            lblTgl.BackColor = Color.Transparent
                            AddHandler lblTgl.Click, AddressOf HandleCardItemClick
                            card.Controls.Add(lblTgl)

                            ' Label Perusahaan BIaya Import
                            Dim PerusahaanBiaya As New Label()
                            PerusahaanBiaya.Text = Char.ConvertFromUtf32(&H1F464) & " Total Hutang : " & If(General_Class.CekNULL(.Rows(i).Item("Mata_Uang")) = "", "-", .Rows(i).Item("Mata_Uang")) & " " & If(General_Class.CekNULL(.Rows(i).Item("Kurs_Baru")) = "", "-", Format(.Rows(i).Item("Kurs_Baru"), "N0"))
                            PerusahaanBiaya.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            PerusahaanBiaya.Location = New Point(5, 48)
                            PerusahaanBiaya.AutoSize = True
                            PerusahaanBiaya.BackColor = Color.Transparent
                            AddHandler PerusahaanBiaya.Click, AddressOf HandleCardItemClick
                            card.Controls.Add(PerusahaanBiaya)

                            ' Label Perusahaan BIaya Import
                            Dim KategoriBiaya As New Label()
                            KategoriBiaya.Text = Char.ConvertFromUtf32(&H1F464) & " Total Bayar : " & If(General_Class.CekNULL(.Rows(i).Item("Mata_Uang")) = "", "-", .Rows(i).Item("Mata_Uang")) & " " & If(General_Class.CekNULL(.Rows(i).Item("Byr")) = "", "-", Format(.Rows(i).Item("Byr"), "N0"))
                            KategoriBiaya.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            KategoriBiaya.Location = New Point(5, 60)
                            KategoriBiaya.AutoSize = True
                            KategoriBiaya.BackColor = Color.Transparent
                            AddHandler KategoriBiaya.Click, AddressOf HandleCardItemClick
                            card.Controls.Add(KategoriBiaya)

                            If .Rows(i).Item("Status_Proses") = "PELUNASAN" Then
                                card.BackColor = Color.LightGreen
                            ElseIf .Rows(i).Item("Status_Proses") = "PENGAJUAN" Then
                                card.BackColor = Color.LightYellow
                            End If

                            FLP_Pengajuan.Controls.Add(card)
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


    Private Sub HandleCardClick(sender As Object, e As EventArgs)
        Dim clickedCard As Panel = CType(sender, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FLP_Pengajuan.Controls
            If TypeOf ctrl Is Panel Then
                If CType(ctrl, Panel).Name = "PELUNASAN" Then
                    CType(ctrl, Panel).BackColor = Color.LightGreen
                ElseIf CType(ctrl, Panel).Name = "PENGAJUAN" Then
                    CType(ctrl, Panel).BackColor = Color.LightYellow
                Else
                    CType(ctrl, Panel).BackColor = Color.White
                End If
            End If
        Next

        clickedCard.BackColor = Color.LightBlue

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NO_Pelunasan As String = selectedCard.Tag.ToString()
        LoadDetail(NO_Pelunasan)

    End Sub

    Private Sub HandleCardItemClick(sender As Object, e As EventArgs)
        Dim panelInduk As Panel = TryCast(sender.Parent, Panel)
        Dim clickedCard As Panel = CType(panelInduk, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FLP_Pengajuan.Controls
            If TypeOf ctrl Is Panel Then

                If CType(ctrl, Panel).Name = "PELUNASAN" Then
                    CType(ctrl, Panel).BackColor = Color.LightGreen
                ElseIf CType(ctrl, Panel).Name = "PENGAJUAN" Then
                    CType(ctrl, Panel).BackColor = Color.LightYellow
                Else
                    CType(ctrl, Panel).BackColor = Color.White
                End If


            End If
        Next

        clickedCard.BackColor = Color.LightBlue

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NO_Pelunasan As String = selectedCard.Tag.ToString()
        LoadDetail(NO_Pelunasan)
    End Sub

    Private Sub LoadDetail(ByVal NoPelunasan As String)
        If NoPelunasan.Trim.Length = 0 Then Exit Sub

        Txt_No_Pengajuan.Text = ""
        Txt_Kd_Bank.Text = ""
        Txt_No_Rekening.Text = ""
        Txt_Nm_Penerima.Text = ""
        Txt_Persen_PPN.Text = ""
        Txt_Nilai_PPN.Text = ""
        Txt_Persen_PPH.Text = ""
        Txt_NIlai_PPH.Text = ""
        Txt_Kurs_Baru.Text = ""
        Txt_Kurs_Tot_Baru.Text = ""
        Txt_Kurs_Lama.Text = ""
        Txt_Kurs_Tot_Lama.Text = ""
        Txt_Jumlah_Bayar.Text = ""

        Txt_Tgl_Pengajuan.Text = ""
        Txt_Tgl_Pelunasan.Text = ""

        Pnl_Indikator_Pengajuan.BackColor = Color.White
        Pnl_Indikator_Pengeluaran.BackColor = Color.White

        Dtp_Bayar.Value = Now.Date
        Cmb_Mata_Uang.SelectedIndex = -1
        Cmb_Mata_Uang_Pelunasan.SelectedIndex = -1


        Try
            OpenConn()

            SQL = "select a.no_pengajuan, b.Kode_Bank_Tujuan, b.No_Rek_Tujuan, b.Nama_Penerima, "
            SQL = SQL & "b.Persen_PPN, b.Nilai_PPN, b.Persen_PPH, b.Nilai_PPH, b.Kurs_Baru, b.Total_Bayar_Kurs_Baru, b.Kurs_Lama, b.Total_Bayar_Kurs_Lama, "
            SQL = SQL & "b.Tanggal_Bayar, b.Byr, b.Mata_Uang, "

            SQL = SQL & "isnull(( "
            SQL = SQL & "select "
            SQL = SQL & "case when x.Flag_Pengajuan is null and x.Flag_pelunasan is null then 'BELUM VALIDASI' "
            SQL = SQL & "when x.Flag_Pengajuan = 'Y' and x.Flag_pelunasan is null then 'PENGAJUAN' "
            SQL = SQL & "when x.Flag_Pengajuan = 'Y' and x.Flag_pelunasan = 'Y' then 'PELUNASAN' "
            SQL = SQL & "else 'UNDEFINED' "
            SQL = SQL & "end "
            SQL = SQL & "from Pengajuan_Temp z, Detail_Pengajuan_Temp x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Pengajuan = x.No_Pengajuan "
            SQL = SQL & "and z.No_Pengajuan = a.No_Pengajuan "
            SQL = SQL & "and x.Urut_Pelunasan = b.Urut "
            SQL = SQL & "), 'UNDEFINED') as Status_Proses, "

            SQL = SQL & "isnull(( "
            SQL = SQL & "select top 1 y.no_transaksi from EMI_Pelunasan_Detail_DP z, EMI_Transaksi_Pembayaran_Dimuka_Detail x, EMI_Transaksi_Pembayaran_Dimuka y "
            SQL = SQL & "where z.kode_perusahaan = a.kode_perusahaan and z.kode_perusahaan = x.kode_perusahaan and x.kode_perusahaan = y.kode_perusahaan "
            SQL = SQL & "and z.no_Val = a.no_val "
            SQL = SQL & "and z.Urut_DP = x.No_Urut "
            SQL = SQL & "and x.no_transaksi = y.no_transaksi ), '-') as No_DP "

            SQL = SQL & "from EMI_Pelunasan a, EMI_Pelunasan_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Val = b.No_Val "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_val = '" & NoPelunasan & "' "
            SQL = SQL & "order by b.Urut "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("no_pengajuan")) = "" Or General_Class.CekNULL(Dr("no_pengajuan")) = "-" Then
                        Txt_Jenis_Pelunasan.Text = "DP"
                        Txt_No_Pengajuan.Text = If(General_Class.CekNULL(Dr("No_DP")) = "", "-", Dr("No_DP"))
                    Else
                        Txt_Jenis_Pelunasan.Text = "Pelunasan"
                        Txt_No_Pengajuan.Text = If(General_Class.CekNULL(Dr("no_pengajuan")) = "", "-", Dr("no_pengajuan"))
                    End If

                    Txt_Kd_Bank.Text = If(General_Class.CekNULL(Dr("Kode_Bank_Tujuan")) = "", "-", Dr("Kode_Bank_Tujuan"))
                    Txt_No_Rekening.Text = If(General_Class.CekNULL(Dr("No_Rek_Tujuan")) = "", "-", Dr("No_Rek_Tujuan"))
                    Txt_Nm_Penerima.Text = If(General_Class.CekNULL(Dr("Nama_Penerima")) = "", "-", Dr("Nama_Penerima"))
                    Txt_Persen_PPN.Text = If(General_Class.CekNULL(Dr("Persen_PPN")) = "", 0, Format(Dr("Persen_PPN"), "N0"))
                    Txt_Nilai_PPN.Text = If(General_Class.CekNULL(Dr("Nilai_PPN")) = "", 0, Format(Dr("Nilai_PPN"), "N0"))
                    Txt_Persen_PPH.Text = If(General_Class.CekNULL(Dr("Persen_PPH")) = "", 0, Format(Dr("Persen_PPH"), "N0"))
                    Txt_NIlai_PPH.Text = If(General_Class.CekNULL(Dr("Nilai_PPH")) = "", 0, Format(Dr("Nilai_PPH"), "N0"))
                    Txt_Kurs_Baru.Text = If(General_Class.CekNULL(Dr("Kurs_Baru")) = "", 0, Format(Dr("Kurs_Baru"), "N0"))
                    Txt_Kurs_Tot_Baru.Text = If(General_Class.CekNULL(Dr("Total_Bayar_Kurs_Baru")) = "", 0, Format(Dr("Total_Bayar_Kurs_Baru"), "N0"))
                    Txt_Kurs_Lama.Text = If(General_Class.CekNULL(Dr("Kurs_Lama")) = "", 0, Format(Dr("Kurs_Lama"), "N0"))
                    Txt_Kurs_Tot_Lama.Text = If(General_Class.CekNULL(Dr("Total_Bayar_Kurs_Lama")) = "", 0, Format(Dr("Total_Bayar_Kurs_Lama"), "N0"))
                    Txt_Jumlah_Bayar.Text = If(General_Class.CekNULL(Dr("Byr")) = "", 0, Format(Dr("Byr"), "N0"))

                    If Not General_Class.CekNULL(Dr("Tanggal_Bayar")) = "" Then
                        Dtp_Bayar.Value = Dr("Tanggal_Bayar")
                    Else
                        Dtp_Bayar.Value = Now.Date
                    End If

                    If Not General_Class.CekNULL(Dr("Mata_Uang")) = "" Then
                        Cmb_Mata_Uang.SelectedItem = Dr("Mata_Uang")
                    Else
                        Cmb_Mata_Uang.SelectedIndex = -1
                    End If

                    If Dr("Status_Proses") = "BELUM VALIDASI" Then
                        Pnl_Indikator_Pengajuan.BackColor = Color.White : Txt_Tgl_Pengajuan.Text = ""
                        Pnl_Indikator_Pengeluaran.BackColor = Color.White : Txt_Tgl_Pelunasan.Text = ""
                    ElseIf Dr("Status_Proses") = "PENGAJUAN" Then
                        Pnl_Indikator_Pengajuan.BackColor = Color.LightGreen : Txt_Tgl_Pengajuan.Text = Format(Dr("Tanggal_Bayar"), "dd MMM yyyy")
                        Pnl_Indikator_Pengeluaran.BackColor = Color.White : Txt_Tgl_Pelunasan.Text = ""
                    ElseIf Dr("Status_Proses") = "PELUNASAN" Then
                        Pnl_Indikator_Pengajuan.BackColor = Color.LightGreen : Txt_Tgl_Pengajuan.Text = ""
                        Pnl_Indikator_Pengeluaran.BackColor = Color.LightGreen : Txt_Tgl_Pelunasan.Text = Format(Dr("Tanggal_Bayar"), "dd MMM yyyy")
                    Else
                        Pnl_Indikator_Pengajuan.BackColor = Color.White : Txt_Tgl_Pengajuan.Text = ""
                        Pnl_Indikator_Pengeluaran.BackColor = Color.White : Txt_Tgl_Pelunasan.Text = ""
                    End If

                    Panel_Isi.Visible = True

                Else
                    Panel_Isi.Visible = False


                End If
            End Using

            SQL = "select isnull(sum(a.PelunasanHutangIDR2),0) as PelunasanIDR, a.mata_uang "
            SQL = SQL & "from View_EMI_Pelunasan a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_po = '" & Txt_NoPO.Text & "' "
            SQL = SQL & "and a.Kode_Perusahaan_Biaya_Import = '" & Txt_Perusahaan.Text & "' "
            SQL = SQL & "and a.Kode_Master_Kategori_Biaya_Import = '" & Txt_Kategori.Text & "' "
            SQL = SQL & "group by a.mata_uang "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_Jumlah_Pelunasan.Text = Dr("PelunasanIDR")
                    Cmb_Mata_Uang_Pelunasan.SelectedItem = Dr("mata_uang")
                Else
                    Txt_Jumlah_Pelunasan.Text = ""
                    Cmb_Mata_Uang_Pelunasan.SelectedIndex = -1
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Public Sub Load_Lv()

        'Try
        '    OpenConn()

        '    Lv_DetailHutang.Items.Clear()

        '    SQL = "select a.no_pengajuan, b.Tanggal_Bayar, b.Byr, b.Kurs_Lama, b. Kurs_Baru, b.Total_Bayar_Kurs_Lama, b.Total_Bayar_Kurs_Baru, b.Kode_Bank_Tujuan, b.No_Rek_Tujuan, b.Nama_Penerima "
        '    SQL = SQL & "from EMI_Pelunasan a, EMI_Pelunasan_Detail b "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
        '    SQL = SQL & "and a.No_Val = b.No_Val "
        '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.Status is null "
        '    SQL = SQL & "and b.No_Faktur = '" & Txt_NoPO.Text & "' "
        '    SQL = SQL & "and b.Kode_Perusahaan_Biaya_Import = '" & Txt_KdPerusahaanBiayaImport.Text & "' "
        '    SQL = SQL & "and b.Kode_Master_Kategori_Biaya_Import = '" & Txt_KdMasterKategori.Text & "' "
        '    SQL = SQL & "order by b.Urut "
        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read

        '            Dim Lv As ListViewItem
        '            Lv = Lv_DetailHutang.Items.Add(If(General_Class.CekNULL(Dr("no_pengajuan")) = "", "-", Dr("no_pengajuan")))
        '            Lv.SubItems.Add(Format(Dr("Tanggal_Bayar"), "dd MMM yyyy"))
        '            Lv.SubItems.Add(Format(Dr("Byr"), "N2"))
        '            Lv.SubItems.Add(Format(Dr("Kurs_Lama"), "N2"))
        '            Lv.SubItems.Add(Format(Dr("Kurs_Baru"), "N2"))
        '            Lv.SubItems.Add(Format(Dr("Total_Bayar_Kurs_Lama"), "N2"))
        '            Lv.SubItems.Add(Format(Dr("Total_Bayar_Kurs_Baru"), "N2"))
        '            Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Kode_Bank_Tujuan")) = "", "-", Dr("Kode_Bank_Tujuan")))
        '            Lv.SubItems.Add(If(General_Class.CekNULL(Dr("No_Rek_Tujuan")) = "", "-", Dr("No_Rek_Tujuan")))
        '            Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Nama_Penerima")) = "", "-", Dr("Nama_Penerima")))

        '        Loop

        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'Hitung()

    End Sub

    Private Sub Hitung()

        'If Lv_DetailHutang.Items.Count = 0 Then
        '    Txt_Sisa.Text = Format(Val(HilangkanTanda(Txt_TotalHutang.Text)), "N2")
        '    Exit Sub
        'End If

        'Dim TotBayar As Double = 0
        'Dim Sisa As Double = 0

        'For i As Integer = 0 To Lv_DetailHutang.Items.Count - 1

        '    Dim Dibayar As Double = Val(HilangkanTanda(Lv_DetailHutang.Items(i).SubItems(2).Text))

        '    TotBayar = TotBayar + Dibayar

        'Next

        'Sisa = Val(HilangkanTanda(Txt_TotalHutang.Text)) - TotBayar

        'Txt_TotalBayar.Text = Format(TotBayar, "N2")
        'Txt_Sisa.Text = Format(Sisa, "N2")

    End Sub

End Class