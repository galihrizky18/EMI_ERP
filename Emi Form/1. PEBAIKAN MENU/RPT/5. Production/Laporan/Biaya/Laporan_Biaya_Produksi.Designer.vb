<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Laporan_Biaya_Produksi
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Chk_NoSplit = New System.Windows.Forms.CheckBox()
        Me.Chk_Batch = New System.Windows.Forms.CheckBox()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Txt_NmLokasiTujuan = New System.Windows.Forms.TextBox()
        Me.Txt_NmLokasiAwal = New System.Windows.Forms.TextBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Txt_KdSoTujuan = New System.Windows.Forms.TextBox()
        Me.Txt_Batch = New System.Windows.Forms.TextBox()
        Me.Txt_KdSoAwal = New System.Windows.Forms.TextBox()
        Me.Txt_NoSplit = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Cmb_Laporan = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CmbJenisBiaya = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.Lv_Lokasi = New System.Windows.Forms.ListView()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.Lv_Lokasi2 = New System.Windows.Forms.ListView()
        Me.Panel_Packaging_Sekunder = New System.Windows.Forms.Panel()
        Me.Txt_Pack_Sekunder_NmBahan = New System.Windows.Forms.TextBox()
        Me.Txt_Pack_Sekunder_NmBarang = New System.Windows.Forms.TextBox()
        Me.Txt_Pack_Sekunder_NoSplit = New System.Windows.Forms.TextBox()
        Me.Txt_Pack_Sekunder_KdBahan = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Txt_Pack_Sekunder_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Txt_Pack_Sekunder_NoTransaksi = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Lv_Pack_Sekunder_NoTransaksi = New System.Windows.Forms.ListView()
        Me.Lv_Pack_Sekunder_NoSplit = New System.Windows.Forms.ListView()
        Me.Lv_Pack_Sekunder_Barang = New System.Windows.Forms.ListView()
        Me.Lv_Pack_Sekunder_Bahan = New System.Windows.Forms.ListView()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel_Packaging_Sekunder.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(591, 51)
        Me.Panel1.TabIndex = 24
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(196, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan Produksi"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Chk_NoSplit)
        Me.GroupBox1.Controls.Add(Me.Chk_Batch)
        Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
        Me.GroupBox1.Controls.Add(Me.Txt_NmLokasiTujuan)
        Me.GroupBox1.Controls.Add(Me.Txt_NmLokasiAwal)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Txt_KdSoTujuan)
        Me.GroupBox1.Controls.Add(Me.Txt_Batch)
        Me.GroupBox1.Controls.Add(Me.Txt_KdSoAwal)
        Me.GroupBox1.Controls.Add(Me.Txt_NoSplit)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Cmb_Laporan)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.CmbJenisBiaya)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 51)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(569, 221)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        '
        'Chk_NoSplit
        '
        Me.Chk_NoSplit.AutoSize = True
        Me.Chk_NoSplit.Location = New System.Drawing.Point(121, 82)
        Me.Chk_NoSplit.Name = "Chk_NoSplit"
        Me.Chk_NoSplit.Size = New System.Drawing.Size(15, 14)
        Me.Chk_NoSplit.TabIndex = 48
        Me.Chk_NoSplit.UseVisualStyleBackColor = True
        Me.Chk_NoSplit.Visible = False
        '
        'Chk_Batch
        '
        Me.Chk_Batch.AutoSize = True
        Me.Chk_Batch.Location = New System.Drawing.Point(121, 109)
        Me.Chk_Batch.Name = "Chk_Batch"
        Me.Chk_Batch.Size = New System.Drawing.Size(15, 14)
        Me.Chk_Batch.TabIndex = 48
        Me.Chk_Batch.UseVisualStyleBackColor = True
        Me.Chk_Batch.Visible = False
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.Location = New System.Drawing.Point(255, 182)
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(231, 20)
        Me.Txt_NmBarang.TabIndex = 9
        Me.Txt_NmBarang.Visible = False
        '
        'Txt_NmLokasiTujuan
        '
        Me.Txt_NmLokasiTujuan.Location = New System.Drawing.Point(255, 156)
        Me.Txt_NmLokasiTujuan.Name = "Txt_NmLokasiTujuan"
        Me.Txt_NmLokasiTujuan.Size = New System.Drawing.Size(231, 20)
        Me.Txt_NmLokasiTujuan.TabIndex = 9
        Me.Txt_NmLokasiTujuan.Visible = False
        '
        'Txt_NmLokasiAwal
        '
        Me.Txt_NmLokasiAwal.Location = New System.Drawing.Point(255, 130)
        Me.Txt_NmLokasiAwal.Name = "Txt_NmLokasiAwal"
        Me.Txt_NmLokasiAwal.Size = New System.Drawing.Size(231, 20)
        Me.Txt_NmLokasiAwal.TabIndex = 9
        Me.Txt_NmLokasiAwal.Visible = False
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.Location = New System.Drawing.Point(121, 182)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(130, 20)
        Me.Txt_KdBarang.TabIndex = 9
        Me.Txt_KdBarang.Visible = False
        '
        'Txt_KdSoTujuan
        '
        Me.Txt_KdSoTujuan.Location = New System.Drawing.Point(121, 156)
        Me.Txt_KdSoTujuan.Name = "Txt_KdSoTujuan"
        Me.Txt_KdSoTujuan.Size = New System.Drawing.Size(130, 20)
        Me.Txt_KdSoTujuan.TabIndex = 9
        Me.Txt_KdSoTujuan.Visible = False
        '
        'Txt_Batch
        '
        Me.Txt_Batch.Enabled = False
        Me.Txt_Batch.Location = New System.Drawing.Point(142, 104)
        Me.Txt_Batch.Name = "Txt_Batch"
        Me.Txt_Batch.Size = New System.Drawing.Size(344, 20)
        Me.Txt_Batch.TabIndex = 9
        Me.Txt_Batch.Visible = False
        '
        'Txt_KdSoAwal
        '
        Me.Txt_KdSoAwal.Location = New System.Drawing.Point(121, 130)
        Me.Txt_KdSoAwal.Name = "Txt_KdSoAwal"
        Me.Txt_KdSoAwal.Size = New System.Drawing.Size(130, 20)
        Me.Txt_KdSoAwal.TabIndex = 9
        Me.Txt_KdSoAwal.Visible = False
        '
        'Txt_NoSplit
        '
        Me.Txt_NoSplit.Enabled = False
        Me.Txt_NoSplit.Location = New System.Drawing.Point(142, 78)
        Me.Txt_NoSplit.Name = "Txt_NoSplit"
        Me.Txt_NoSplit.Size = New System.Drawing.Size(344, 20)
        Me.Txt_NoSplit.TabIndex = 9
        Me.Txt_NoSplit.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 184)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 16)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Barang"
        '
        'Cmb_Laporan
        '
        Me.Cmb_Laporan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Laporan.FormattingEnabled = True
        Me.Cmb_Laporan.Location = New System.Drawing.Point(121, 48)
        Me.Cmb_Laporan.Name = "Cmb_Laporan"
        Me.Cmb_Laporan.Size = New System.Drawing.Size(365, 24)
        Me.Cmb_Laporan.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 158)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 16)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Lokasi Tujuan"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(16, 106)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 16)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Batch Number"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Laporan"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 132)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Lokasi Awal"
        '
        'CmbJenisBiaya
        '
        Me.CmbJenisBiaya.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJenisBiaya.FormattingEnabled = True
        Me.CmbJenisBiaya.Location = New System.Drawing.Point(121, 78)
        Me.CmbJenisBiaya.Name = "CmbJenisBiaya"
        Me.CmbJenisBiaya.Size = New System.Drawing.Size(365, 24)
        Me.CmbJenisBiaya.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 16)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Jenis Biaya"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(323, 22)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 20)
        Me.Tgl2.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(290, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(121, 22)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 20)
        Me.Tgl1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Periode"
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(496, 278)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 27
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(413, 278)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
        Me.BtnCetak.TabIndex = 26
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'Lv_Lokasi
        '
        Me.Lv_Lokasi.BackColor = System.Drawing.Color.White
        Me.Lv_Lokasi.FullRowSelect = True
        Me.Lv_Lokasi.GridLines = True
        Me.Lv_Lokasi.HideSelection = False
        Me.Lv_Lokasi.Location = New System.Drawing.Point(600, 204)
        Me.Lv_Lokasi.Name = "Lv_Lokasi"
        Me.Lv_Lokasi.Size = New System.Drawing.Size(365, 159)
        Me.Lv_Lokasi.TabIndex = 47
        Me.Lv_Lokasi.UseCompatibleStateImageBehavior = False
        Me.Lv_Lokasi.View = System.Windows.Forms.View.Details
        Me.Lv_Lokasi.Visible = False
        '
        'Lv_Barang
        '
        Me.Lv_Barang.BackColor = System.Drawing.Color.White
        Me.Lv_Barang.FullRowSelect = True
        Me.Lv_Barang.GridLines = True
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(600, 247)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(365, 159)
        Me.Lv_Barang.TabIndex = 47
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        Me.Lv_Barang.Visible = False
        '
        'Lv_Lokasi2
        '
        Me.Lv_Lokasi2.BackColor = System.Drawing.Color.White
        Me.Lv_Lokasi2.FullRowSelect = True
        Me.Lv_Lokasi2.GridLines = True
        Me.Lv_Lokasi2.HideSelection = False
        Me.Lv_Lokasi2.Location = New System.Drawing.Point(600, 224)
        Me.Lv_Lokasi2.Name = "Lv_Lokasi2"
        Me.Lv_Lokasi2.Size = New System.Drawing.Size(365, 159)
        Me.Lv_Lokasi2.TabIndex = 47
        Me.Lv_Lokasi2.UseCompatibleStateImageBehavior = False
        Me.Lv_Lokasi2.View = System.Windows.Forms.View.Details
        Me.Lv_Lokasi2.Visible = False
        '
        'Panel_Packaging_Sekunder
        '
        Me.Panel_Packaging_Sekunder.Controls.Add(Me.Txt_Pack_Sekunder_NmBahan)
        Me.Panel_Packaging_Sekunder.Controls.Add(Me.Txt_Pack_Sekunder_NmBarang)
        Me.Panel_Packaging_Sekunder.Controls.Add(Me.Txt_Pack_Sekunder_NoSplit)
        Me.Panel_Packaging_Sekunder.Controls.Add(Me.Txt_Pack_Sekunder_KdBahan)
        Me.Panel_Packaging_Sekunder.Controls.Add(Me.Label13)
        Me.Panel_Packaging_Sekunder.Controls.Add(Me.Txt_Pack_Sekunder_KdBarang)
        Me.Panel_Packaging_Sekunder.Controls.Add(Me.Label12)
        Me.Panel_Packaging_Sekunder.Controls.Add(Me.Label11)
        Me.Panel_Packaging_Sekunder.Controls.Add(Me.Txt_Pack_Sekunder_NoTransaksi)
        Me.Panel_Packaging_Sekunder.Controls.Add(Me.Label10)
        Me.Panel_Packaging_Sekunder.Location = New System.Drawing.Point(600, 127)
        Me.Panel_Packaging_Sekunder.Name = "Panel_Packaging_Sekunder"
        Me.Panel_Packaging_Sekunder.Size = New System.Drawing.Size(513, 139)
        Me.Panel_Packaging_Sekunder.TabIndex = 48
        Me.Panel_Packaging_Sekunder.Visible = False
        '
        'Txt_Pack_Sekunder_NmBahan
        '
        Me.Txt_Pack_Sekunder_NmBahan.Location = New System.Drawing.Point(262, 84)
        Me.Txt_Pack_Sekunder_NmBahan.Name = "Txt_Pack_Sekunder_NmBahan"
        Me.Txt_Pack_Sekunder_NmBahan.Size = New System.Drawing.Size(231, 20)
        Me.Txt_Pack_Sekunder_NmBahan.TabIndex = 51
        '
        'Txt_Pack_Sekunder_NmBarang
        '
        Me.Txt_Pack_Sekunder_NmBarang.Location = New System.Drawing.Point(260, 58)
        Me.Txt_Pack_Sekunder_NmBarang.Name = "Txt_Pack_Sekunder_NmBarang"
        Me.Txt_Pack_Sekunder_NmBarang.Size = New System.Drawing.Size(231, 20)
        Me.Txt_Pack_Sekunder_NmBarang.TabIndex = 51
        '
        'Txt_Pack_Sekunder_NoSplit
        '
        Me.Txt_Pack_Sekunder_NoSplit.Location = New System.Drawing.Point(115, 32)
        Me.Txt_Pack_Sekunder_NoSplit.Name = "Txt_Pack_Sekunder_NoSplit"
        Me.Txt_Pack_Sekunder_NoSplit.Size = New System.Drawing.Size(141, 20)
        Me.Txt_Pack_Sekunder_NoSplit.TabIndex = 50
        '
        'Txt_Pack_Sekunder_KdBahan
        '
        Me.Txt_Pack_Sekunder_KdBahan.Location = New System.Drawing.Point(115, 84)
        Me.Txt_Pack_Sekunder_KdBahan.Name = "Txt_Pack_Sekunder_KdBahan"
        Me.Txt_Pack_Sekunder_KdBahan.Size = New System.Drawing.Size(141, 20)
        Me.Txt_Pack_Sekunder_KdBahan.TabIndex = 50
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(10, 86)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 16)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = "Bahan"
        '
        'Txt_Pack_Sekunder_KdBarang
        '
        Me.Txt_Pack_Sekunder_KdBarang.Location = New System.Drawing.Point(115, 58)
        Me.Txt_Pack_Sekunder_KdBarang.Name = "Txt_Pack_Sekunder_KdBarang"
        Me.Txt_Pack_Sekunder_KdBarang.Size = New System.Drawing.Size(141, 20)
        Me.Txt_Pack_Sekunder_KdBarang.TabIndex = 50
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(10, 60)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(43, 16)
        Me.Label12.TabIndex = 49
        Me.Label12.Text = "Barang"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(11, 32)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 16)
        Me.Label11.TabIndex = 49
        Me.Label11.Text = "No Split"
        '
        'Txt_Pack_Sekunder_NoTransaksi
        '
        Me.Txt_Pack_Sekunder_NoTransaksi.Location = New System.Drawing.Point(115, 6)
        Me.Txt_Pack_Sekunder_NoTransaksi.Name = "Txt_Pack_Sekunder_NoTransaksi"
        Me.Txt_Pack_Sekunder_NoTransaksi.Size = New System.Drawing.Size(141, 20)
        Me.Txt_Pack_Sekunder_NoTransaksi.TabIndex = 50
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 16)
        Me.Label10.TabIndex = 49
        Me.Label10.Text = "No Transaksi"
        '
        'Lv_Pack_Sekunder_NoTransaksi
        '
        Me.Lv_Pack_Sekunder_NoTransaksi.FullRowSelect = True
        Me.Lv_Pack_Sekunder_NoTransaksi.GridLines = True
        Me.Lv_Pack_Sekunder_NoTransaksi.HideSelection = False
        Me.Lv_Pack_Sekunder_NoTransaksi.Location = New System.Drawing.Point(1120, 155)
        Me.Lv_Pack_Sekunder_NoTransaksi.Name = "Lv_Pack_Sekunder_NoTransaksi"
        Me.Lv_Pack_Sekunder_NoTransaksi.Size = New System.Drawing.Size(376, 134)
        Me.Lv_Pack_Sekunder_NoTransaksi.TabIndex = 49
        Me.Lv_Pack_Sekunder_NoTransaksi.UseCompatibleStateImageBehavior = False
        Me.Lv_Pack_Sekunder_NoTransaksi.View = System.Windows.Forms.View.Details
        '
        'Lv_Pack_Sekunder_NoSplit
        '
        Me.Lv_Pack_Sekunder_NoSplit.FullRowSelect = True
        Me.Lv_Pack_Sekunder_NoSplit.GridLines = True
        Me.Lv_Pack_Sekunder_NoSplit.HideSelection = False
        Me.Lv_Pack_Sekunder_NoSplit.Location = New System.Drawing.Point(1120, 181)
        Me.Lv_Pack_Sekunder_NoSplit.Name = "Lv_Pack_Sekunder_NoSplit"
        Me.Lv_Pack_Sekunder_NoSplit.Size = New System.Drawing.Size(376, 134)
        Me.Lv_Pack_Sekunder_NoSplit.TabIndex = 49
        Me.Lv_Pack_Sekunder_NoSplit.UseCompatibleStateImageBehavior = False
        Me.Lv_Pack_Sekunder_NoSplit.View = System.Windows.Forms.View.Details
        '
        'Lv_Pack_Sekunder_Barang
        '
        Me.Lv_Pack_Sekunder_Barang.FullRowSelect = True
        Me.Lv_Pack_Sekunder_Barang.GridLines = True
        Me.Lv_Pack_Sekunder_Barang.HideSelection = False
        Me.Lv_Pack_Sekunder_Barang.Location = New System.Drawing.Point(1120, 207)
        Me.Lv_Pack_Sekunder_Barang.Name = "Lv_Pack_Sekunder_Barang"
        Me.Lv_Pack_Sekunder_Barang.Size = New System.Drawing.Size(376, 134)
        Me.Lv_Pack_Sekunder_Barang.TabIndex = 49
        Me.Lv_Pack_Sekunder_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Pack_Sekunder_Barang.View = System.Windows.Forms.View.Details
        '
        'Lv_Pack_Sekunder_Bahan
        '
        Me.Lv_Pack_Sekunder_Bahan.FullRowSelect = True
        Me.Lv_Pack_Sekunder_Bahan.GridLines = True
        Me.Lv_Pack_Sekunder_Bahan.HideSelection = False
        Me.Lv_Pack_Sekunder_Bahan.Location = New System.Drawing.Point(1120, 233)
        Me.Lv_Pack_Sekunder_Bahan.Name = "Lv_Pack_Sekunder_Bahan"
        Me.Lv_Pack_Sekunder_Bahan.Size = New System.Drawing.Size(376, 134)
        Me.Lv_Pack_Sekunder_Bahan.TabIndex = 49
        Me.Lv_Pack_Sekunder_Bahan.UseCompatibleStateImageBehavior = False
        Me.Lv_Pack_Sekunder_Bahan.View = System.Windows.Forms.View.Details
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 49)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(591, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Laporan_Biaya_Produksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(591, 321)
        Me.Controls.Add(Me.Lv_Pack_Sekunder_Bahan)
        Me.Controls.Add(Me.Lv_Pack_Sekunder_Barang)
        Me.Controls.Add(Me.Lv_Pack_Sekunder_NoSplit)
        Me.Controls.Add(Me.Lv_Pack_Sekunder_NoTransaksi)
        Me.Controls.Add(Me.Panel_Packaging_Sekunder)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.Lv_Lokasi2)
        Me.Controls.Add(Me.Lv_Lokasi)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Laporan_Biaya_Produksi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel_Packaging_Sekunder.ResumeLayout(False)
        Me.Panel_Packaging_Sekunder.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CmbJenisBiaya As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnExit As Button
    Friend WithEvents BtnCetak As Button
    Friend WithEvents Cmb_Laporan As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_NoSplit As TextBox
    Friend WithEvents Txt_KdSoAwal As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Txt_KdSoTujuan As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Txt_NmLokasiTujuan As TextBox
    Friend WithEvents Txt_NmLokasiAwal As TextBox
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Txt_Batch As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Lv_Lokasi As ListView
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Lv_Lokasi2 As ListView
    Friend WithEvents Chk_Batch As CheckBox
    Friend WithEvents Chk_NoSplit As CheckBox
    Friend WithEvents Panel_Packaging_Sekunder As Panel
    Friend WithEvents Txt_Pack_Sekunder_NoTransaksi As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Txt_Pack_Sekunder_NoSplit As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Txt_Pack_Sekunder_NmBarang As TextBox
    Friend WithEvents Txt_Pack_Sekunder_KdBarang As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Txt_Pack_Sekunder_NmBahan As TextBox
    Friend WithEvents Txt_Pack_Sekunder_KdBahan As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Lv_Pack_Sekunder_NoTransaksi As ListView
    Friend WithEvents Lv_Pack_Sekunder_NoSplit As ListView
    Friend WithEvents Lv_Pack_Sekunder_Barang As ListView
    Friend WithEvents Lv_Pack_Sekunder_Bahan As ListView
End Class
