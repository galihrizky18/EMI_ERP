Imports System.Drawing.Drawing2D
Imports MS.Internal
'Imports System.Windows.Forms.VisualStyles.VisualStyleElement
'Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_SD_Detail_Production_Schedule

    Public TanggalDariKalender As DateTime


    Public mustUpdate As Boolean = False
    Public kodeBarang As String
    Public namaBarang As String
    Public id As String


    Public jumlahSekarang As Double
    Public jumlahInginProduksi As Double
    Public bulanTahunSkrng As String
    Public no_urut As Integer
    Public noRv As String
    Public noRvSchedule As String
    Public jumlahSudahTerpakai As Double

    Dim sudahPO As String
    Dim statusRelease As String

    Dim FlagbtnShow As Boolean = False

    Dim bulan As String
    Dim tanggal As String

    Public Class RoundedLabel
        Inherits Label

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim path As New System.Drawing.Drawing2D.GraphicsPath()
            Dim radius As Integer = 10

            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
            path.CloseFigure()

            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.FillPath(New SolidBrush(BackColor), path)
            e.Graphics.DrawString(Text, Font, New SolidBrush(ForeColor),
            New RectangleF(0, 0, Width, Height),
            New StringFormat With {
                .Alignment = StringAlignment.Center,
                .LineAlignment = StringAlignment.Center
            })
        End Sub
    End Class

    Dim warnaShow As Color = Color.FromArgb(232, 241, 251)
    Dim warnaHide As Color = Color.FromArgb(240, 240, 240)
    Dim warnaBatal As Color = Color.FromArgb(200, 80, 80)
    Dim warnaDefault As Color = Color.FromArgb(15, 86, 122)


    Private Sub SetRoundedButton(btn As Button, radius As Integer)
        Dim path As New Drawing2D.GraphicsPath()

        path.AddArc(btn.ClientRectangle.X, btn.ClientRectangle.Y, radius, radius, 180, 90)
        path.AddArc(btn.ClientRectangle.Right - radius, btn.ClientRectangle.Y, radius, radius, 270, 90)
        path.AddArc(btn.ClientRectangle.Right - radius, btn.ClientRectangle.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(btn.ClientRectangle.X, btn.ClientRectangle.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()

        btn.Region = New Region(path)
    End Sub

    Private Sub N_EMI_SD_Edit_Jumlah_Production_Plan_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        TabControl1.SelectedIndex = 0

        Lv_Data.Columns.Clear()

        Lv_Data.Columns.Add("No PO", 120, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah PO", 120, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("User", 100, HorizontalAlignment.Center)

        Lv_Data.View = View.Details

        '   Btn_Simpan.Location = New Point(206, 481)
        Panel1.BackColor = Color.FromArgb(232, 238, 246)  ' #E8EEF6 - biru muda
        TxtTanggal.ForeColor = Color.FromArgb(100, 116, 139)   ' abu-abu biru untuk teks
        kosong()
    End Sub



    Sub panggil_data()
        Try
            OpenConn()


            SQL = "select day(Tanggal_Full) as Tanggal,a.kode_stock_owner, b.Bulan,b.Tahun,a.Kode_Barang,c.Nama,a.Jumlah,a.flag_sudah_production_order, f.flag_release, "
            SQL = SQL & "a.satuan,cast(a.rv as bigint) as rvx_Schedule,"
            SQL = SQL & "cast(x.rv as bigint) as rvx_planing, f.no_faktur, "
            SQL = SQL & "a.No_Urut , e.Id_Jenis_Produk,  e.Keterangan as Jenis_produk "
            SQL = SQL & ", x.Nilai_PPIC, h.nilai , a.tanggal_start, a.tanggal_end,a.keterangan, i.jumlah_po From N_EMI_Production_Plan_Schedule_Detail a "
            SQL = SQL & "inner join N_EMI_Production_Plan_Schedule b on "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi =b.No_Transaksi "
            SQL = SQL & "inner join barang c on "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Barang = c.Kode_Barang and a.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "inner join EMI_Transaksi_Sales_Forecasting_Detail x on  "
            SQL = SQL & "x.Kode_Perusahaan = a.Kode_Perusahaan and x.urut = a.Urut_Production_Plan "
            SQL = SQL & "inner join EMI_Transaksi_Sales_Forecasting y on  "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "


            SQL = SQL & "outer apply ( "
            SQL = SQL & "select sum(b.Jumlah) as nilai from N_EMI_Production_Plan_Schedule_Detail b,  "
            SQL = SQL & "n_emi_production_plan_schedule m "
            SQL = SQL & "where b.Urut_Production_Plan = x.urut "
            SQL = SQL & "and b.no_transaksi = m.no_transaksi and b.kode_perusahaan = m.kode_perusahaan "
            SQL = SQL & ") h "


            SQL = SQL & "outer apply ( "
            SQL = SQL & "select isnull(sum(xy.Jumlah),0) as jumlah_po from emi_order_produksi xy  "
            SQL = SQL & " where xy.Urut_Production_Schedule  = a.No_Urut  "
            SQL = SQL & " and xy.Kode_Perusahaan  = a.kode_perusahaan and xy.Status is null "
            SQL = SQL & ") i "

            SQL = SQL & " inner join EMI_Varian d on  c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Varian = d.Id_Varian "
            SQL = SQL & "inner join EMI_Jenis_Produk e on d.Kode_Perusahaan = e.Kode_Perusahaan and e.Id_Jenis_Produk = d.Id_Jenis_Produk "

            SQL = SQL & " left join EMI_Order_Produksi f on f.Kode_Perusahaan = a.Kode_Perusahaan and f.urut_production_schedule = a.No_Urut "
            SQL = SQL & " and f.status is null  "


            SQL = SQL & "where b.Status is null and No_Urut = " & id & " and y.Status is null "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    ' === isi dari DataReader
                    txtKdBrng.Text = Dr("kode_barang").ToString()
                    txtNamaBarang.Text = Dr("nama").ToString()

                    txtKdBrng2.Text = Dr("kode_barang").ToString()
                    txtNamaBarang2.Text = Dr("nama").ToString()

                    txtJumlahSkrng.Text = Format(Val(Dr("nilai_ppic")), "N2")
                    TextBox1.Text = Format(Val(Dr("jumlah")), "N2")

                    txtJmlhInginProduksi_Tab2.Text = Format(Val(Dr("jumlah")), "N2")
                    txtSudahPo.Text = Format(Dr("jumlah_po"), "N2")
                    JmlhSdhPO_TabKedua.Text = Format(Dr("jumlah_po"), "N2")
                    Dim namaBulan As String = New DateTime(2000, CInt(Dr("bulan")), 1).ToString("MMM", New Globalization.CultureInfo("id-ID"))
                    TxtTanggal.Text = Dr("tanggal").ToString() & " " & namaBulan & " " & Dr("tahun").ToString()

                    txt_NoUrut.Text = Dr("No_Urut").ToString()
                    txtNoRv.Text = Dr("rvx_planing").ToString()
                    txtNoRvSch.Text = Dr("rvx_Schedule").ToString()

                    txtJumlahTerpakai.Text = Format(Val(Dr("nilai")), "N2")

                    If General_Class.CekNULL(Dr("keterangan")) = "" Then
                        txtKet.Text = "-"
                    Else
                        txtKet.Text = Dr("keterangan")
                    End If

                    txtTanggalStart.Text = Format(Dr("tanggal_start"), "dd MMM yyyy")
                    txtTanggalEnd.Text = Format(Dr("tanggal_end"), "dd MMM yyyy")
                    txt_JamStart.Text = Format(Dr("tanggal_start"), "HH:ss:mm")
                    txtJamEnd.Text = Format(Dr("tanggal_end"), "HH:ss:mm")


                    txtKdStockOwner.Text = Dr("kode_stock_owner")
                    txtIdJenisProduk.Text = Dr("id_jenis_produk")
                    txtKetJenisProduk.Text = Dr("Jenis_produk")
                    txtJumlahSdhPO.Text = Format(Dr("jumlah_po"), "N2")

                    txtSatuan.Text = Dr("satuan")

                    If General_Class.CekNULL(Dr("no_faktur")) = "" Then
                        txtNoFakturPO.Text = Nothing
                    Else
                        txtNoFakturPO.Text = Dr("no_faktur")
                    End If



                    If General_Class.CekNULL(Dr("flag_sudah_production_order")) = "" Then
                        sudahPO = "T"
                    Else
                        sudahPO = "Y"
                    End If

                    If General_Class.CekNULL(Dr("flag_release")) = "" Then
                        statusRelease = "T"
                    Else
                        statusRelease = "Y"
                    End If


                Else
                    CloseConn()
                    MessageBox.Show("Belum ada data yang ingin di produksi pada tanggal ini..", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using







            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    'Sub HideData()
    '    txtTanggalStart.ReadOnly = True
    '    txtTanggalStart.BackColor = warnaHide
    '    txtTanggalStart.Enabled = False

    '    txtTanggalEnd.ReadOnly = True
    '    txtTanggalEnd.BackColor = warnaHide
    '    txtTanggalEnd.Enabled = False

    '    FlagbtnShow = False

    '    BtnEdit.Text = "Ubah Jadwal"
    '    BtnEdit.Tag = "edit"
    '    BtnEdit.BackColor = warnaDefault

    '    txt_JamStart.ReadOnly = True
    '    txt_JamStart.BackColor = warnaHide
    '    txt_JamStart.Enabled = False

    '    txtJamEnd.ReadOnly = True
    '    txtJamEnd.BackColor = warnaHide
    '    txtJamEnd.Enabled = False

    '    txtKet.ReadOnly = True
    '    txtKet.BackColor = warnaHide
    '    txtKet.Enabled = False
    'End Sub
    '' Deklarasi di level Form
    'Dim dtpTanggalStart As New DateTimePicker
    'Dim dtpTanggalEnd As New DateTimePicker
    'Dim dtp_JamStart As New DateTimePicker
    'Dim dtpJamEnd As New DateTimePicker
    'Sub ShowData1()
    '    txtTanggalStart.ReadOnly = False
    '    txtTanggalStart.BackColor = warnaHide
    '    txtTanggalStart.Enabled = True

    '    txtTanggalEnd.ReadOnly = False
    '    txtTanggalEnd.BackColor = warnaHide
    '    txtTanggalEnd.Enabled = True

    '    BtnEdit.Text = "Batal"
    '    BtnEdit.Tag = "batal"
    '    BtnEdit.BackColor = warnaBatal

    '    FlagbtnShow = True

    '    txt_JamStart.ReadOnly = False
    '    txt_JamStart.BackColor = warnaShow
    '    txt_JamStart.Enabled = True

    '    txtJamEnd.ReadOnly = False
    '    txtJamEnd.BackColor = warnaShow
    '    txtJamEnd.Enabled = True



    '    txtKet.ReadOnly = False
    '    txtKet.BackColor = warnaShow
    '    txtKet.Enabled = True
    'End Sub


    ' Deklarasi di level Form
    Dim dtpTanggalStart As New DateTimePicker
    Dim dtpTanggalEnd As New DateTimePicker
    Dim dtp_JamStart As New DateTimePicker
    Dim dtpJamEnd As New DateTimePicker

    Sub ShowData1()
        ' === Tanggal Start ===

        btnSimpanEdit.Visible = True
        dtpTanggalStart.Format = DateTimePickerFormat.Short
        dtpTanggalStart.Size = txtTanggalStart.Size
        dtpTanggalStart.Location = txtTanggalStart.Location
        dtpTanggalStart.Enabled = True
        dtpTanggalStart.BackColor = warnaShow
        dtpTanggalStart.Visible = True
        If Not String.IsNullOrEmpty(txtTanggalStart.Text) Then
            Dim tglStart As DateTime
            If DateTime.TryParseExact(txtTanggalStart.Text, "dd MMM yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, tglStart) Then
                dtpTanggalStart.Value = tglStart
            End If
        End If
        txtTanggalStart.Visible = False
        If Not Me.Controls.Contains(dtpTanggalStart) Then
            txtTanggalStart.Parent.Controls.Add(dtpTanggalStart)
        End If

        ' === Tanggal End ===
        dtpTanggalEnd.Format = DateTimePickerFormat.Short
        dtpTanggalEnd.Size = txtTanggalEnd.Size
        dtpTanggalEnd.Location = txtTanggalEnd.Location
        dtpTanggalEnd.Enabled = True
        dtpTanggalEnd.BackColor = warnaShow
        dtpTanggalEnd.Visible = True
        If Not String.IsNullOrEmpty(txtTanggalEnd.Text) Then
            Dim tglEnd As DateTime
            If DateTime.TryParseExact(txtTanggalEnd.Text, "dd MMM yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, tglEnd) Then
                dtpTanggalEnd.Value = tglEnd
            End If
        End If
        txtTanggalEnd.Visible = False
        If Not Me.Controls.Contains(dtpTanggalEnd) Then
            txtTanggalEnd.Parent.Controls.Add(dtpTanggalEnd)
        End If

        ' === Tanggal Start ===
        dtpTanggalStart.Format = DateTimePickerFormat.Custom
        dtpTanggalStart.CustomFormat = "dd MMM yyyy"

        ' === Tanggal End ===
        dtpTanggalEnd.Format = DateTimePickerFormat.Custom
        dtpTanggalEnd.CustomFormat = "dd MMM yyyy"

        ' === Jam Start ===
        dtp_JamStart.Format = DateTimePickerFormat.Custom
        dtp_JamStart.ShowUpDown = True
        dtp_JamStart.CustomFormat = "HH:mm:ss"
        dtp_JamStart.Size = txt_JamStart.Size
        dtp_JamStart.Location = txt_JamStart.Location
        dtp_JamStart.Enabled = True
        dtp_JamStart.BackColor = warnaShow
        dtp_JamStart.Visible = True
        If Not String.IsNullOrEmpty(txt_JamStart.Text) Then
            Dim jamStart As DateTime
            If DateTime.TryParseExact(txt_JamStart.Text, "HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, jamStart) Then
                dtp_JamStart.Value = DateTime.Today.Add(jamStart.TimeOfDay)
            End If
        End If
        txt_JamStart.Visible = False
        If Not Me.Controls.Contains(dtp_JamStart) Then
            txt_JamStart.Parent.Controls.Add(dtp_JamStart)
        End If

        ' === Jam End ===
        dtpJamEnd.Format = DateTimePickerFormat.Custom
        dtpJamEnd.ShowUpDown = True
        dtpJamEnd.Size = txtJamEnd.Size
        dtpJamEnd.CustomFormat = "HH:mm:ss"
        dtpJamEnd.Location = txtJamEnd.Location
        dtpJamEnd.Enabled = True
        dtpJamEnd.BackColor = warnaShow
        dtpJamEnd.Visible = True
        If Not String.IsNullOrEmpty(txtJamEnd.Text) Then
            Dim jamEnd As DateTime
            If DateTime.TryParseExact(txtJamEnd.Text, "HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, jamEnd) Then
                dtpJamEnd.Value = DateTime.Today.Add(jamEnd.TimeOfDay)
            End If
        End If
        txtJamEnd.Visible = False
        If Not Me.Controls.Contains(dtpJamEnd) Then
            txtJamEnd.Parent.Controls.Add(dtpJamEnd)
        End If

        ' === Lainnya ===
        BtnEdit.Text = "Batal"
        BtnEdit.Tag = "batal"
        BtnEdit.BackColor = warnaBatal
        FlagbtnShow = True

        txtKet.ReadOnly = False
        txtKet.BackColor = warnaShow
        txtKet.Enabled = True


        Button1.Visible = False
        btnBatal.Visible = False
        Btn_Simpan.Visible = False
    End Sub

    Sub HideData()

        btnSimpanEdit.Visible = False
        ' Sembunyikan DTP, tampilkan balik TextBox
        dtpTanggalStart.Visible = False
        txtTanggalStart.Visible = True
        txtTanggalStart.ReadOnly = True
        txtTanggalStart.BackColor = warnaHide
        txtTanggalStart.Enabled = False

        dtpTanggalEnd.Visible = False
        txtTanggalEnd.Visible = True
        txtTanggalEnd.ReadOnly = True
        txtTanggalEnd.BackColor = warnaHide
        txtTanggalEnd.Enabled = False

        BtnEdit.Text = "Ubah Jadwal"
        BtnEdit.Tag = "edit"
        BtnEdit.BackColor = warnaDefault

        FlagbtnShow = False

        dtp_JamStart.Visible = False
        txt_JamStart.Visible = True
        txt_JamStart.ReadOnly = True
        txt_JamStart.BackColor = warnaHide
        txt_JamStart.Enabled = False

        dtpJamEnd.Visible = False
        txtJamEnd.Visible = True
        txtJamEnd.ReadOnly = True
        txtJamEnd.BackColor = warnaHide
        txtJamEnd.Enabled = False

        txtKet.ReadOnly = True
        txtKet.BackColor = warnaHide
        txtKet.Enabled = False

        Button1.Visible = True


        If sudahPO = "Y" Then
            ' btnBatal.Visible = True

            ' Btn_Simpan.Location = New Point(300, 543)
        Else
            ' btnBatal.Visible = False
            ' Btn_Simpan.Location = New Point(206, 481)
        End If



        Btn_Simpan.Visible = True
    End Sub


    Sub kosong()


        panggil_data()

        HideData()

        'If sudahPO = "Y" Then

        '    BadgeLabel1.Text = "Production Order"

        '    btnBatal.Enabled = True

        '    FlagbtnShow = False

        '    btnBatal.Location = New Point(101, 483)

        '    ' btnBatal.Location = New Point(307, 507)

        '    ' Btn_Simpan.Location = New Point(271, 483)

        '    badgeReleaser.Visible = True

        '    Button1.Visible = False
        '    BtnEdit.Visible = False

        '    If statusRelease = "Y" Then
        '        badgeReleaser.Text = "Sudah di Release"
        '    Else
        '        badgeReleaser.Text = "Belum di Release"
        '    End If

        'Else
        '    BadgeLabel1.Text = "Belum Production Order"
        '    badgeReleaser.Visible = False

        '    ' btnBatal.Visible = False

        '    ' Btn_Simpan.Location = New Point(206, 481)
        '    Button1.Visible = True
        '    BtnEdit.Visible = True

        '    ' btnBatal.Location = New Point(101, 483)

        'End If

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click


        'Dim jumlahInput As String = InputBox("Masukkan jumlah yang ingin di produksi : ")

        'Dim nilai As Decimal
        'If jumlahInput = "" Then
        '    Exit Sub

        'ElseIf Not Decimal.TryParse(jumlahInput, nilai) Then

        '    MessageBox.Show("Input hanya boleh angka dan titik!", Judul,
        '            MessageBoxButtons.OK,
        '            MessageBoxIcon.Exclamation)
        '    Exit Sub

        'ElseIf nilai < 1 Then

        '    MessageBox.Show("Jumlah input tidak boleh kurang dari satu!", Judul,
        '            MessageBoxButtons.OK,
        '            MessageBoxIcon.Exclamation)
        '    Exit Sub

        'End If







        'EMI_Production_Order.parentForm = "schedule"

        'EMI_Production_Order.LvData.Columns.Clear()
        'EMI_Production_Order.LvData.Clear()

        'EMI_Production_Order.LvOrder.Columns.Clear()
        'EMI_Production_Order.LvOrder.Clear()

        'EMI_Production_Order.LvBahan.Columns.Clear()
        'EMI_Production_Order.LvBahan.Clear()

        'EMI_Production_Order.LvBahanNew.Columns.Clear()
        'EMI_Production_Order.LvBahanNew.Clear()


        'EMI_Production_Order.LvPackaging.Columns.Clear()
        'EMI_Production_Order.LvPackaging.Clear()



        'EMI_Production_Order.LvPackagingNew.Columns.Clear()
        'EMI_Production_Order.LvPackagingNew.Clear()




        'If sudahPO = "Y" Then
        '    EMI_Production_Order.NoFakturFromSchedule = txtNoFakturPO.Text
        '    EMI_Production_Order.flagSudahPOSchedule = sudahPO



        'Else
        '    Dim lvw As New ListViewItem("PS")
        '    lvw.SubItems.Add(txtKdStockOwner.Text) '1
        '    lvw.SubItems.Add("-") '2
        '    lvw.SubItems.Add("-") '3
        '    lvw.SubItems.Add(txtKdBrng.Text) '4
        '    lvw.SubItems.Add(txtNamaBarang.Text) '5
        '    lvw.SubItems.Add(jumlahInput) '6 Barang
        '    lvw.SubItems.Add(Format(0, "N2")) '7 Barang
        '    lvw.SubItems.Add(txtSatuan.Text) '8
        '    lvw.SubItems.Add(txtKetJenisProduk.Text) '9
        '    lvw.SubItems.Add(txt_NoUrut.Text) '10
        '    lvw.SubItems.Add(txtIdJenisProduk.Text) '11
        '    lvw.SubItems.Add(txtKet.Text) '12
        '    lvw.SubItems.Add("Produksi")
        '    lvw.SubItems.Add("")




        '    EMI_Production_Order.LvData.Items.Add(lvw)

        '    EMI_Production_Order.RV_Schedule = txtNoRvSch.Text
        '    EMI_Production_Order.UrutOtoSchedule = txt_NoUrut.Text

        '    EMI_Production_Order.NoFakturFromSchedule = Nothing
        '    EMI_Production_Order.flagSudahPOSchedule = "T"

        '    EMI_Production_Order.CekPackingSetFromSchedule(txtKdBrng.Text)

        'End If

        Try
            OpenConn()

            SQL = "select isnull(a.Jumlah,0) as Jumlah_Schedule, "
            SQL = SQL & "isnull((select sum(x.Jumlah) from emi_order_produksi x where x.Kode_Perusahaan = a.Kode_Perusahaan  "
            SQL = SQL & "and x.Urut_Production_Schedule = a.No_Urut and x.Status is null),0) as Jumlah_PO "
            SQL = SQL & "From N_EMI_Production_Plan_Schedule_Detail a, N_EMI_Production_Plan_Schedule b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Urut = '" & id & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Val(Dr("Jumlah_Schedule")) = Val(Dr("jumlah_po")) Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Tidak bisa melanjutkan, Jumlah PO sudah memenuhi jumlah schedule ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Schedule tidak ditemukan atau sudah dibatalkan sebelumnya. silah kan refresh dan coba lagi. ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        N_EMI_SD_Input_Jumlah_PO.txtKodeStockOwner1 = txtKdStockOwner.Text
        N_EMI_SD_Input_Jumlah_PO.txtKdBrng1 = txtKdBrng.Text
        N_EMI_SD_Input_Jumlah_PO.txtNamaBarang1 = txtNamaBarang.Text
        N_EMI_SD_Input_Jumlah_PO.txtSatuan1 = txtSatuan.Text
        N_EMI_SD_Input_Jumlah_PO.txtKetJenisProduk1 = txtKetJenisProduk.Text
        N_EMI_SD_Input_Jumlah_PO.txt_NoUrut1 = txt_NoUrut.Text
        N_EMI_SD_Input_Jumlah_PO.txtIdJenisProduk1 = txtIdJenisProduk.Text
        N_EMI_SD_Input_Jumlah_PO.txtKet1 = txtKet.Text
        N_EMI_SD_Input_Jumlah_PO.txtNoRvSch1 = txtNoRvSch.Text
        N_EMI_SD_Input_Jumlah_PO.txtJmlhSchedule1 = Val(HilangkanTanda(txtJmlhInginProduksi_Tab2.Text))
        N_EMI_SD_Input_Jumlah_PO.txtJmlhSdhPO1 = Val(HilangkanTanda(txtJumlahSdhPO.Text))
        N_EMI_SD_Input_Jumlah_PO.ShowDialog()
        ' EMI_Production_Order.ShowDialog()


        If EMI_Production_Order.mustUpdate Then
            kosong()
            panggil_data_po()
        End If

    End Sub



    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            OpenConn()
            ' Ambil baris yang tadi diklik kanan


            Dim ada_akses_untuk_ubah_Jumlah As Boolean = False

            If CekButtonRole("Production_Plan_Schedule_PPIC") = "T" Then
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses untuk edit!!")
                Exit Sub
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Dim pertanyaan As String = MessageBox.Show("Yakin Ingin membatalkan Schedule ini ? ", "Production Schedule", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If pertanyaan = vbNo Then Exit Sub



        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            Dim bulanSchedule As String = ""
            Dim tahunSchedule As String = ""

            SQL = "select b.Status, b.bulan,b.tahun ,"
            SQL = SQL & " isnull((select top(1) 'Y' from emi_order_produksi x where x.kode_perusahaan = a.kode_perusahaan "
            SQL = SQL & "and x.Urut_Production_Schedule = a.no_urut and x.status is null "
            SQL = SQL & "), 'T') as Sudah_production  "
            SQL = SQL & "From N_EMI_Production_Plan_Schedule_Detail a, N_EMI_Production_Plan_Schedule B  "
            SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.No_Urut = '" & txt_NoUrut.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("status")) <> "" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Production Schedule ini sudah dibatalkan sebelumnya..", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If General_Class.CekNULL(Dr("Sudah_production")) = "Y" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Tidak bisa membatalkan schedule ini,schedule sudah di buat producion order!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    bulanSchedule = Dr("bulan")
                    tahunSchedule = Dr("tahun")


                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Production Schedule tidak ditemukan...", "Production Schedule", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using




            SQL = "update N_EMI_Production_Plan_Schedule_Detail set "
            SQL = SQL & "Jumlah = '0' where No_Urut = '" & txt_NoUrut.Text & "' "
            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "'"
            ExecuteTrans(SQL)



            Dim listFilter As New List(Of String)

            listFilter.Add("(a.bulan = " & bulanSchedule & " AND a.tahun = " & tahunSchedule & ")")
            Dim filterBulanTahun As String = String.Join(" OR ", listFilter)

            RefreshForecastingSemiFG(
                    KodePerusahaan,
                    filterBulanTahun,
                    UserID,
                "Batal Schedule"
            )



            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Production Schedule berhasil dibatalkan.", "Batal Schedule", MessageBoxButtons.OK, MessageBoxIcon.Information)
            mustUpdate = True
            Me.Close()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        If FlagbtnShow = False Then
            ShowData1()
        Else

            HideData()
        End If
    End Sub

    Private Sub btnSimpanEdit_Click(sender As Object, e As EventArgs) Handles btnSimpanEdit.Click

        Dim pertanyaan As String = MessageBox.Show("Yakin Ingin merubah data ini ? ", "Production Schedule", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If pertanyaan = vbNo Then Exit Sub
        Try
            OpenConn()

            Dim bulanSchedule As String = ""
            Dim tahunSchedule As String = ""

            SQL = "select b.Status, b.bulan,b.tahun ,"
            SQL = SQL & " isnull((select top(1) 'Y' from emi_order_produksi x where x.kode_perusahaan = a.kode_perusahaan "
            SQL = SQL & "and x.Urut_Production_Schedule = a.no_urut and x.status is null "
            SQL = SQL & "), 'T') as Sudah_production  "
            SQL = SQL & "From N_EMI_Production_Plan_Schedule_Detail a, N_EMI_Production_Plan_Schedule B  "
            SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.No_Urut = '" & txt_NoUrut.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("status")) <> "" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Production Schedule ini sudah dibatalkan sebelumnya..", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If General_Class.CekNULL(Dr("Sudah_production")) = "Y" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Tidak bisa mengubah schedule ini,schedule sudah di buat producion order!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    bulanSchedule = Dr("bulan")
                    tahunSchedule = Dr("tahun")


                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Production Schedule tidak ditemukan...", "Production Schedule", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            Dim bulanInput As String = dtpTanggalStart.Value.ToString("MM")
            Dim tahunInput As String = dtpTanggalStart.Value.ToString("yyyy")

            ' Bandingkan
            If bulanInput <> bulanSchedule OrElse tahunInput <> tahunSchedule Then
                CloseConn()
                MessageBox.Show("Tanggal tidak sesuai dengan bulan schedule!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If



            SQL = "update N_EMI_Production_Plan_Schedule_Detail set "
            SQL = SQL & "Tanggal_Start = '" & dtpTanggalStart.Value.ToString("yyyy-MM-dd") & " " & dtp_JamStart.Value.ToString("HH:mm:ss") & "', "
            SQL = SQL & "Tanggal_End = '" & dtpTanggalEnd.Value.ToString("yyyy-MM-dd") & " " & dtpJamEnd.Value.ToString("HH:mm:ss") & "'"
            SQL = SQL & ",Keterangan = '" & txtKet.Text.Trim & "', "
            SQL = SQL & "tanggal_full = '" & dtpTanggalStart.Value.ToString("yyyy-MM-dd") & "', "
            SQL = SQL & "tanggal = '" & dtpTanggalStart.Value.ToString("dd") & "' "

            SQL = SQL & " where No_Urut = '" & txt_NoUrut.Text & "' "
            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "'"
            ExecuteTrans(SQL)



            SQL = "INSERT INTO N_EMI_Production_Plan_Schedule_log ("
            SQL = SQL & "Kode_Perusahaan, No_Transaksi, User_Id, Tanggal, Tanggal_Perhari, "
            SQL = SQL & "Bulan, Tahun, Kode_Formula, Jumlah, Satuan, No_Urut_Detail, jam, kode_Stock_owner,kode_barang ,"
            SQL = SQL & "tanggal_start, tanggal_end, keterangan"
            SQL = SQL & ")"

            SQL = SQL & "select   "
            SQL = SQL & "Kode_Perusahaan, No_Transaksi, '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', day(tanggal_full), "
            SQL = SQL & " month(tanggal_full) , year(tanggal_full), kode_formula,jumlah,satuan,no_urut, "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', kode_stock_owner,kode_barang, tanggal_start, "
            SQL = SQL & "tanggal_end, keterangan "
            SQL = SQL & "From N_EMI_Production_Plan_Schedule_detail where no_urut = '" & txt_NoUrut.Text & "'"
            ExecuteTrans(SQL)



            kosong()
            mustUpdate = True



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub




    Private Sub panggil_data_po()
        Try
            OpenConn()
            DGV_Data.Rows.Clear()
            'SQL = "select a.No_Transaksi,c.No_Faktur as NO_PO, c.Tanggal,c.Jam,c.UserId, c.Jumlah,c.Satuan From N_EMI_Production_Plan_Schedule_Detail a "
            'SQL = SQL & "inner join  N_EMI_Production_Plan_Schedule b  on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            'SQL = SQL & "left join  EMI_Order_Produksi c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Urut = c.Urut_Production_Schedule "
            'SQL = SQL & "and c.Status is null "
            'SQL = SQL & "where b.Status is null  and  a.No_Urut = '" & id & "' "
            'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "

            SQL = "select c.Urut_Production_Schedule, c.flag_release,c.No_Faktur as NO_PO, c.Tanggal,c.Jam,c.UserId, c.Jumlah,c.Satuan  "
            SQL = SQL & "from emi_order_produksi c where c.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.status is null "
            SQL = SQL & "and c.Urut_Production_Schedule = '" & id & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            DGV_Data.Rows.Add(1)

                            DGV_Data.Rows(i).Cells(2).Value = .Rows(i).Item("NO_PO")
                            DGV_Data.Rows(i).Cells(3).Value = Format(Val(.Rows(i).Item("Jumlah")), "N2")
                            DGV_Data.Rows(i).Cells(4).Value = .Rows(i).Item("satuan")
                            DGV_Data.Rows(i).Cells(5).Value = Format(.Rows(i).Item("tanggal"), "dd MMM yy")
                            DGV_Data.Rows(i).Cells(6).Value = .Rows(i).Item("jam")
                            DGV_Data.Rows(i).Cells(7).Value = .Rows(i).Item("userid")
                            DGV_Data.Rows(i).Cells(8).Value = .Rows(i).Item("Urut_Production_Schedule")
                            If General_Class.CekNULL(.Rows(i).Item("flag_release")) <> "" Then
                                DGV_Data.Rows(i).DefaultCellStyle.BackColor = Color.LightYellow

                            End If
                        Next
                    End If
                End With
            End Using




            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim lvw As ListViewItem
            '        lvw = Lv_Data.Items.Add(Dr("no_po"))
            '        lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
            '        lvw.SubItems.Add(Dr("satuan"))
            '        lvw.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
            '        lvw.SubItems.Add(Dr("jam"))
            '        lvw.SubItems.Add(Dr("userid"))

            '        If General_Class.CekNULL(Dr("flag_release")) <> "" Then
            '            lvw.BackColor = Color.Green
            '            lvw.ForeColor = Color.White
            '        End If

            '    Loop
            'End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 1 Then


            panggil_data_po()


        End If
    End Sub

    Private Sub DGV_Data_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        Dim no_po As String =
                DGV_Data.Rows(e.RowIndex).Cells(2).Value.ToString()

        Dim id_det_schedule As String =
                DGV_Data.Rows(e.RowIndex).Cells(8).Value.ToString()


        If e.ColumnIndex = 0 Then

            get_jam()

            Dim pertanyaan As String = MessageBox.Show("Yakin Ingin membatalkan Production Order dengan nomor " & no_po & " ?", "Production Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If pertanyaan = vbNo Then Exit Sub



            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction


                SQL = "select top(1) flag_release,status,no_faktur From EMI_Order_Produksi where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & no_po & "'  "
                SQL = SQL & "and flag_release = 'Y' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Production Order dengan nomor " & no_po & " sudah pernah direlease, Production order tidak bisa dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using




                SQL = "update EMI_Order_Produksi set status = 'Y' ,"
                        SQL = SQL & "Tanggal_Batal_PO = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "jam_batal_po = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "userid_batal_po = '" & UserID & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_faktur = '" & no_po & "'"
                ExecuteTrans(SQL)


                'cek masih ada po apa gk kalau gk ada po flag_pernah_po dan di null


                SQL = "select isnull(sum(a.Jumlah),0) as Jumlah_PO, isnull(b.jumlah,0) as Jumlah From emi_order_produksi a, N_EMI_Production_Plan_Schedule_Detail b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Production_Schedule = b.No_Urut   "
                SQL = SQL & "and a.Status is null and a.kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.No_Urut = '" & txt_NoUrut.Text & "' "
                SQL = SQL & "group by b.Jumlah"
                Using Dr = OpenTrans(SQL)

                    If Dr.Read Then
                        If Val(Dr("jumlah_po")) = Val(Dr("jumlah")) Then

                            Dr.Close()
                            SQL = "update N_EMI_Production_Plan_Schedule_Detail set Flag_Sudah_Production_Order = null ,"
                            SQL = SQL & "flag_pernah_production_order = 'Y' "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "No_Urut = '" & txt_NoUrut.Text & "' "
                            ExecuteTrans(SQL)
                        ElseIf Val(Dr("jumlah_po")) < Val(Dr("jumlah")) Then
                            Dr.Close()

                            SQL = "update N_EMI_Production_Plan_Schedule_Detail set Flag_Sudah_Production_Order = null ,"
                            SQL = SQL & "flag_pernah_production_order = 'Y' "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "No_Urut = '" & txt_NoUrut.Text & "' "
                            ExecuteTrans(SQL)

                        End If
                    Else

                        Dr.Close()

                        SQL = "update N_EMI_Production_Plan_Schedule_Detail set Flag_Sudah_Production_Order = null ,"
                        SQL = SQL & "flag_pernah_production_order = null "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Urut = '" & txt_NoUrut.Text & "' "
                        ExecuteTrans(SQL)

                    End If

                End Using


                'SQL = "select top(1) kode_perusahaan from emi_order_produksi where kode_perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and status is null and Urut_Production_Schedule = " & txt_NoUrut.Text & " "
                'Using Dr = OpenTrans(SQL)

                '    If Not Dr.Read Then
                '        Dr.Close()

                '        SQL = "update N_EMI_Production_Plan_Schedule_Detail set Flag_Sudah_Production_Order = null ,"
                '        SQL = SQL & "flag_pernah_production_order = null "
                '        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                '        SQL = SQL & "No_Urut = '" & txt_NoUrut.Text & "' "
                '        ExecuteTrans(SQL)
                '    End If

                'End Using







                'SQL = "update N_EMI_Production_Plan_Schedule_Detail set Flag_Sudah_Production_Order = null "
                'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "No_Urut = '" & txt_NoUrut.Text & "' "
                'ExecuteTrans(SQL)


                Cmd.Transaction.Commit()
                CloseConn()

                kosong()
                panggil_data_po()
                panggil_data()
                MessageBox.Show("Production order berhasil dibatalkan. ", "Berhasil batal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        End If

        ' tombol release
        If e.ColumnIndex = 1 Then


            EMI_Production_Order.parentForm = "schedule"

            EMI_Production_Order.LvData.Columns.Clear()
            EMI_Production_Order.LvData.Clear()

            EMI_Production_Order.LvOrder.Columns.Clear()
            EMI_Production_Order.LvOrder.Clear()

            EMI_Production_Order.LvBahan.Columns.Clear()
            EMI_Production_Order.LvBahan.Clear()

            EMI_Production_Order.LvBahanNew.Columns.Clear()
            EMI_Production_Order.LvBahanNew.Clear()


            EMI_Production_Order.LvPackaging.Columns.Clear()
            EMI_Production_Order.LvPackaging.Clear()



            EMI_Production_Order.LvPackagingNew.Columns.Clear()
            EMI_Production_Order.LvPackagingNew.Clear()


            EMI_Production_Order.NoFakturFromSchedule = no_po
            EMI_Production_Order.flagSudahPOSchedule = "Y"

            EMI_Production_Order.cmb_routing.Enabled = False
            EMI_Production_Order.Cmb_Jenis.Enabled = False

            EMI_Production_Order.ShowDialog()


            If EMI_Production_Order.mustUpdate Then
                kosong()
                panggil_data_po()
            End If

        End If
    End Sub

    Private Sub btnSimpanEdit_Click_1(sender As Object, e As EventArgs) Handles btnSimpanEdit.Click

    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click

    End Sub
End Class