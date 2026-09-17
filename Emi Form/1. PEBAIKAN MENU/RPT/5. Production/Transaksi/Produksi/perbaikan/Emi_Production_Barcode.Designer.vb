<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Emi_Production_Barcode
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
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle36 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.CmbSisaProduksi = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.CmbSatScrap = New System.Windows.Forms.ComboBox()
        Me.TxtJmlScrap = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Cmb_Lokasi_Gudang_Sisa = New System.Windows.Forms.ComboBox()
        Me.Dgv_Packaging = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Txt_Jumlah = New System.Windows.Forms.TextBox()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.Chk_FullPallet = New System.Windows.Forms.CheckBox()
        Me.CmbJenis = New System.Windows.Forms.ComboBox()
        Me.DtpExpired = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DtpProduksi = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_Troli = New System.Windows.Forms.TextBox()
        Me.Cmb_LokasiSimpan = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TxtJumlahKeranjang = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtGroupJenis = New System.Windows.Forms.TextBox()
        Me.Cmb_SatuanProduksi = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtJam = New System.Windows.Forms.TextBox()
        Me.Txt_NamaBarang = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_HasilProduksi = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_NoSplit = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtSatuanKecil = New System.Windows.Forms.TextBox()
        Me.TxtSatScrapKecil = New System.Windows.Forms.TextBox()
        Me.TxtLifeTime = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TxtFormulator_NoFaktur = New System.Windows.Forms.TextBox()
        Me.Barcode = New System.Windows.Forms.PictureBox()
        Me.Cmb_Tahapan = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.Dgv_Packaging, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(678, 43)
        Me.Panel1.TabIndex = 23
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 41)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(678, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(344, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Transaksi - Penerimaan Barang"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(3, 41)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Location = New System.Drawing.Point(3, 56)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(20, 485)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(942, 12)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.Dgv_Packaging)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.TxtJumlahKeranjang)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.TxtGroupJenis)
        Me.GroupBox1.Controls.Add(Me.Cmb_SatuanProduksi)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.TxtJam)
        Me.GroupBox1.Controls.Add(Me.Txt_NamaBarang)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Txt_HasilProduksi)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_NoSplit)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 75)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(634, 569)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.CmbSisaProduksi)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.CmbSatScrap)
        Me.GroupBox4.Controls.Add(Me.TxtJmlScrap)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.Cmb_Lokasi_Gudang_Sisa)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox4.Location = New System.Drawing.Point(26, 333)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(580, 103)
        Me.GroupBox4.TabIndex = 431
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Input Scrap"
        '
        'CmbSisaProduksi
        '
        Me.CmbSisaProduksi.BackColor = System.Drawing.SystemColors.Window
        Me.CmbSisaProduksi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSisaProduksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSisaProduksi.FormattingEnabled = True
        Me.CmbSisaProduksi.Location = New System.Drawing.Point(154, 31)
        Me.CmbSisaProduksi.Name = "CmbSisaProduksi"
        Me.CmbSisaProduksi.Size = New System.Drawing.Size(165, 23)
        Me.CmbSisaProduksi.TabIndex = 424
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label14.Location = New System.Drawing.Point(6, 65)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(128, 16)
        Me.Label14.TabIndex = 413
        Me.Label14.Text = "Lokasi Gudang Sisa"
        '
        'CmbSatScrap
        '
        Me.CmbSatScrap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSatScrap.Enabled = False
        Me.CmbSatScrap.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSatScrap.FormattingEnabled = True
        Me.CmbSatScrap.Location = New System.Drawing.Point(491, 29)
        Me.CmbSatScrap.Name = "CmbSatScrap"
        Me.CmbSatScrap.Size = New System.Drawing.Size(60, 24)
        Me.CmbSatScrap.TabIndex = 419
        '
        'TxtJmlScrap
        '
        Me.TxtJmlScrap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJmlScrap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJmlScrap.Enabled = False
        Me.TxtJmlScrap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtJmlScrap.Location = New System.Drawing.Point(326, 31)
        Me.TxtJmlScrap.MaxLength = 50
        Me.TxtJmlScrap.Name = "TxtJmlScrap"
        Me.TxtJmlScrap.Size = New System.Drawing.Size(160, 21)
        Me.TxtJmlScrap.TabIndex = 410
        Me.TxtJmlScrap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label13.Location = New System.Drawing.Point(7, 33)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(90, 16)
        Me.Label13.TabIndex = 413
        Me.Label13.Text = "Sisa Produksi"
        '
        'Cmb_Lokasi_Gudang_Sisa
        '
        Me.Cmb_Lokasi_Gudang_Sisa.BackColor = System.Drawing.SystemColors.Window
        Me.Cmb_Lokasi_Gudang_Sisa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi_Gudang_Sisa.Enabled = False
        Me.Cmb_Lokasi_Gudang_Sisa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Lokasi_Gudang_Sisa.FormattingEnabled = True
        Me.Cmb_Lokasi_Gudang_Sisa.Location = New System.Drawing.Point(154, 60)
        Me.Cmb_Lokasi_Gudang_Sisa.Name = "Cmb_Lokasi_Gudang_Sisa"
        Me.Cmb_Lokasi_Gudang_Sisa.Size = New System.Drawing.Size(165, 23)
        Me.Cmb_Lokasi_Gudang_Sisa.TabIndex = 424
        '
        'Dgv_Packaging
        '
        Me.Dgv_Packaging.AllowUserToAddRows = False
        Me.Dgv_Packaging.AllowUserToResizeColumns = False
        Me.Dgv_Packaging.AllowUserToResizeRows = False
        Me.Dgv_Packaging.BackgroundColor = System.Drawing.Color.White
        Me.Dgv_Packaging.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle33.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle33.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        DataGridViewCellStyle33.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Packaging.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle33
        Me.Dgv_Packaging.ColumnHeadersHeight = 35
        Me.Dgv_Packaging.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column3, Me.Column2})
        Me.Dgv_Packaging.Location = New System.Drawing.Point(25, 442)
        Me.Dgv_Packaging.Name = "Dgv_Packaging"
        Me.Dgv_Packaging.RowHeadersWidth = 21
        DataGridViewCellStyle36.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dgv_Packaging.RowsDefaultCellStyle = DataGridViewCellStyle36
        Me.Dgv_Packaging.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Dgv_Packaging.Size = New System.Drawing.Size(581, 112)
        Me.Dgv_Packaging.TabIndex = 425
        '
        'Column1
        '
        DataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle34
        Me.Column1.HeaderText = "Kode Bahan Packaging"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 180
        '
        'Column3
        '
        Me.Column3.HeaderText = "Nama"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 232
        '
        'Column2
        '
        DataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle35
        Me.Column2.HeaderText = "Qty"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 147
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Txt_Jumlah)
        Me.GroupBox2.Controls.Add(Me.Cmb_Satuan)
        Me.GroupBox2.Controls.Add(Me.Chk_FullPallet)
        Me.GroupBox2.Controls.Add(Me.CmbJenis)
        Me.GroupBox2.Controls.Add(Me.DtpExpired)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Cmb_Tahapan)
        Me.GroupBox2.Controls.Add(Me.DtpProduksi)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Txt_Troli)
        Me.GroupBox2.Controls.Add(Me.Cmb_LokasiSimpan)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox2.Location = New System.Drawing.Point(25, 155)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(581, 175)
        Me.GroupBox2.TabIndex = 430
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Input"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(11, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 16)
        Me.Label10.TabIndex = 382
        Me.Label10.Text = "Tgl Produksi"
        '
        'Txt_Jumlah
        '
        Me.Txt_Jumlah.BackColor = System.Drawing.Color.White
        Me.Txt_Jumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Jumlah.Enabled = False
        Me.Txt_Jumlah.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Jumlah.Location = New System.Drawing.Point(155, 81)
        Me.Txt_Jumlah.Name = "Txt_Jumlah"
        Me.Txt_Jumlah.Size = New System.Drawing.Size(275, 22)
        Me.Txt_Jumlah.TabIndex = 375
        Me.Txt_Jumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.Enabled = False
        Me.Cmb_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Location = New System.Drawing.Point(434, 81)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(116, 24)
        Me.Cmb_Satuan.TabIndex = 376
        '
        'Chk_FullPallet
        '
        Me.Chk_FullPallet.AutoSize = True
        Me.Chk_FullPallet.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Chk_FullPallet.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chk_FullPallet.Location = New System.Drawing.Point(12, 81)
        Me.Chk_FullPallet.Name = "Chk_FullPallet"
        Me.Chk_FullPallet.Size = New System.Drawing.Size(100, 20)
        Me.Chk_FullPallet.TabIndex = 374
        Me.Chk_FullPallet.Text = "Jumlah Input"
        Me.Chk_FullPallet.UseVisualStyleBackColor = True
        '
        'CmbJenis
        '
        Me.CmbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJenis.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbJenis.FormattingEnabled = True
        Me.CmbJenis.Location = New System.Drawing.Point(155, 111)
        Me.CmbJenis.Name = "CmbJenis"
        Me.CmbJenis.Size = New System.Drawing.Size(395, 24)
        Me.CmbJenis.TabIndex = 377
        '
        'DtpExpired
        '
        Me.DtpExpired.CustomFormat = "dd MMMM yyyy"
        Me.DtpExpired.Enabled = False
        Me.DtpExpired.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpExpired.Location = New System.Drawing.Point(394, 51)
        Me.DtpExpired.Name = "DtpExpired"
        Me.DtpExpired.Size = New System.Drawing.Size(156, 21)
        Me.DtpExpired.TabIndex = 379
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(312, 51)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 16)
        Me.Label9.TabIndex = 380
        Me.Label9.Text = "Tgl Expired"
        '
        'DtpProduksi
        '
        Me.DtpProduksi.CustomFormat = "dd MMMM yyyy"
        Me.DtpProduksi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpProduksi.Location = New System.Drawing.Point(155, 51)
        Me.DtpProduksi.Name = "DtpProduksi"
        Me.DtpProduksi.Size = New System.Drawing.Size(151, 21)
        Me.DtpProduksi.TabIndex = 381
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 114)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 16)
        Me.Label8.TabIndex = 423
        Me.Label8.Text = "Jenis Quality"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 16)
        Me.Label5.TabIndex = 415
        Me.Label5.Text = "Lokasi Gudang"
        '
        'Txt_Troli
        '
        Me.Txt_Troli.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Troli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Troli.Enabled = False
        Me.Txt_Troli.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Troli.Location = New System.Drawing.Point(387, 143)
        Me.Txt_Troli.MaxLength = 50
        Me.Txt_Troli.Name = "Txt_Troli"
        Me.Txt_Troli.Size = New System.Drawing.Size(163, 21)
        Me.Txt_Troli.TabIndex = 410
        Me.Txt_Troli.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Cmb_LokasiSimpan
        '
        Me.Cmb_LokasiSimpan.BackColor = System.Drawing.SystemColors.Window
        Me.Cmb_LokasiSimpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_LokasiSimpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_LokasiSimpan.FormattingEnabled = True
        Me.Cmb_LokasiSimpan.Location = New System.Drawing.Point(155, 21)
        Me.Cmb_LokasiSimpan.Name = "Cmb_LokasiSimpan"
        Me.Cmb_LokasiSimpan.Size = New System.Drawing.Size(396, 23)
        Me.Cmb_LokasiSimpan.TabIndex = 414
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label15.Location = New System.Drawing.Point(12, 147)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(41, 16)
        Me.Label15.TabIndex = 413
        Me.Label15.Text = "Batch"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label16.Location = New System.Drawing.Point(334, 145)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(41, 16)
        Me.Label16.TabIndex = 413
        Me.Label16.Text = "Trolly"
        '
        'TxtJumlahKeranjang
        '
        Me.TxtJumlahKeranjang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJumlahKeranjang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJumlahKeranjang.Enabled = False
        Me.TxtJumlahKeranjang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJumlahKeranjang.Location = New System.Drawing.Point(499, 17)
        Me.TxtJumlahKeranjang.Name = "TxtJumlahKeranjang"
        Me.TxtJumlahKeranjang.Size = New System.Drawing.Size(106, 22)
        Me.TxtJumlahKeranjang.TabIndex = 429
        Me.TxtJumlahKeranjang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(379, 19)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(114, 16)
        Me.Label17.TabIndex = 428
        Me.Label17.Text = "Jumlah Keranjang"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(364, 75)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 16)
        Me.Label12.TabIndex = 427
        Me.Label12.Text = "Kategori"
        '
        'TxtGroupJenis
        '
        Me.TxtGroupJenis.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtGroupJenis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGroupJenis.Enabled = False
        Me.TxtGroupJenis.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtGroupJenis.Location = New System.Drawing.Point(427, 73)
        Me.TxtGroupJenis.MaxLength = 50
        Me.TxtGroupJenis.Name = "TxtGroupJenis"
        Me.TxtGroupJenis.Size = New System.Drawing.Size(178, 21)
        Me.TxtGroupJenis.TabIndex = 426
        '
        'Cmb_SatuanProduksi
        '
        Me.Cmb_SatuanProduksi.Enabled = False
        Me.Cmb_SatuanProduksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_SatuanProduksi.FormattingEnabled = True
        Me.Cmb_SatuanProduksi.Location = New System.Drawing.Point(480, 130)
        Me.Cmb_SatuanProduksi.Name = "Cmb_SatuanProduksi"
        Me.Cmb_SatuanProduksi.Size = New System.Drawing.Size(126, 24)
        Me.Cmb_SatuanProduksi.TabIndex = 422
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMM yyyy"
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(182, 46)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(191, 20)
        Me.DateTimePicker1.TabIndex = 416
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(24, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(114, 16)
        Me.Label6.TabIndex = 418
        Me.Label6.Text = "Tanggal Produksi"
        '
        'TxtJam
        '
        Me.TxtJam.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJam.Enabled = False
        Me.TxtJam.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtJam.Location = New System.Drawing.Point(382, 45)
        Me.TxtJam.MaxLength = 50
        Me.TxtJam.Name = "TxtJam"
        Me.TxtJam.Size = New System.Drawing.Size(223, 21)
        Me.TxtJam.TabIndex = 417
        '
        'Txt_NamaBarang
        '
        Me.Txt_NamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NamaBarang.Enabled = False
        Me.Txt_NamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NamaBarang.Location = New System.Drawing.Point(182, 101)
        Me.Txt_NamaBarang.Name = "Txt_NamaBarang"
        Me.Txt_NamaBarang.Size = New System.Drawing.Size(423, 22)
        Me.Txt_NamaBarang.TabIndex = 378
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 16)
        Me.Label4.TabIndex = 377
        Me.Label4.Text = "Nama Barang"
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Enabled = False
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_KdBarang.Location = New System.Drawing.Point(182, 73)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(176, 22)
        Me.Txt_KdBarang.TabIndex = 375
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Kode Barang"
        '
        'Txt_HasilProduksi
        '
        Me.Txt_HasilProduksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_HasilProduksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_HasilProduksi.Enabled = False
        Me.Txt_HasilProduksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_HasilProduksi.Location = New System.Drawing.Point(182, 130)
        Me.Txt_HasilProduksi.Name = "Txt_HasilProduksi"
        Me.Txt_HasilProduksi.Size = New System.Drawing.Size(292, 22)
        Me.Txt_HasilProduksi.TabIndex = 375
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "No Split"
        '
        'Txt_NoSplit
        '
        Me.Txt_NoSplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoSplit.Enabled = False
        Me.Txt_NoSplit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoSplit.Location = New System.Drawing.Point(182, 17)
        Me.Txt_NoSplit.Name = "Txt_NoSplit"
        Me.Txt_NoSplit.Size = New System.Drawing.Size(191, 22)
        Me.Txt_NoSplit.TabIndex = 375
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(24, 132)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 16)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Jumlah Produksi"
        '
        'TxtSatuanKecil
        '
        Me.TxtSatuanKecil.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSatuanKecil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSatuanKecil.Enabled = False
        Me.TxtSatuanKecil.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSatuanKecil.Location = New System.Drawing.Point(762, 283)
        Me.TxtSatuanKecil.Name = "TxtSatuanKecil"
        Me.TxtSatuanKecil.Size = New System.Drawing.Size(72, 22)
        Me.TxtSatuanKecil.TabIndex = 421
        Me.TxtSatuanKecil.Visible = False
        '
        'TxtSatScrapKecil
        '
        Me.TxtSatScrapKecil.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSatScrapKecil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSatScrapKecil.Enabled = False
        Me.TxtSatScrapKecil.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSatScrapKecil.Location = New System.Drawing.Point(684, 283)
        Me.TxtSatScrapKecil.Name = "TxtSatScrapKecil"
        Me.TxtSatScrapKecil.Size = New System.Drawing.Size(72, 22)
        Me.TxtSatScrapKecil.TabIndex = 420
        Me.TxtSatScrapKecil.Visible = False
        '
        'TxtLifeTime
        '
        Me.TxtLifeTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtLifeTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtLifeTime.Enabled = False
        Me.TxtLifeTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLifeTime.Location = New System.Drawing.Point(750, 340)
        Me.TxtLifeTime.Name = "TxtLifeTime"
        Me.TxtLifeTime.Size = New System.Drawing.Size(122, 22)
        Me.TxtLifeTime.TabIndex = 380
        Me.TxtLifeTime.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(682, 342)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 16)
        Me.Label11.TabIndex = 379
        Me.Label11.Text = "Life Time"
        Me.Label11.Visible = False
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.Enabled = False
        Me.Cmb_Lokasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(441, 51)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(214, 24)
        Me.Cmb_Lokasi.TabIndex = 376
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(194, 12)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(126, 35)
        Me.Btn_Refresh.TabIndex = 373
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Enabled = False
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(459, 11)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(156, 35)
        Me.Btn_Simpan.TabIndex = 373
        Me.Btn_Simpan.Text = "&Finished Goods"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        Me.Btn_Simpan.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(658, 61)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 601)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(685, 561)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 12)
        Me.Panel7.TabIndex = 35
        Me.Panel7.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Button1)
        Me.GroupBox3.Controls.Add(Me.Btn_Simpan)
        Me.GroupBox3.Controls.Add(Me.Btn_Refresh)
        Me.GroupBox3.Location = New System.Drawing.Point(23, 647)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(634, 53)
        Me.GroupBox3.TabIndex = 379
        Me.GroupBox3.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(6, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(187, 35)
        Me.Button1.TabIndex = 373
        Me.Button1.Text = "&Primary Packaging only"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TxtFormulator_NoFaktur
        '
        Me.TxtFormulator_NoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtFormulator_NoFaktur.Enabled = False
        Me.TxtFormulator_NoFaktur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFormulator_NoFaktur.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtFormulator_NoFaktur.Location = New System.Drawing.Point(23, 54)
        Me.TxtFormulator_NoFaktur.MaxLength = 30
        Me.TxtFormulator_NoFaktur.Name = "TxtFormulator_NoFaktur"
        Me.TxtFormulator_NoFaktur.ReadOnly = True
        Me.TxtFormulator_NoFaktur.Size = New System.Drawing.Size(227, 21)
        Me.TxtFormulator_NoFaktur.TabIndex = 380
        Me.TxtFormulator_NoFaktur.Visible = False
        '
        'Barcode
        '
        Me.Barcode.Location = New System.Drawing.Point(684, 64)
        Me.Barcode.Name = "Barcode"
        Me.Barcode.Size = New System.Drawing.Size(100, 50)
        Me.Barcode.TabIndex = 377
        Me.Barcode.TabStop = False
        Me.Barcode.Visible = False
        '
        'Cmb_Tahapan
        '
        Me.Cmb_Tahapan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Tahapan.Enabled = False
        Me.Cmb_Tahapan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Tahapan.FormattingEnabled = True
        Me.Cmb_Tahapan.Location = New System.Drawing.Point(155, 143)
        Me.Cmb_Tahapan.Name = "Cmb_Tahapan"
        Me.Cmb_Tahapan.Size = New System.Drawing.Size(165, 24)
        Me.Cmb_Tahapan.TabIndex = 422
        '
        'Emi_Production_Barcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(678, 710)
        Me.Controls.Add(Me.TxtFormulator_NoFaktur)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.TxtSatuanKecil)
        Me.Controls.Add(Me.TxtSatScrapKecil)
        Me.Controls.Add(Me.Barcode)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Cmb_Lokasi)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtLifeTime)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Production_Barcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.Dgv_Packaging, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Chk_FullPallet As CheckBox
    Friend WithEvents Txt_NoSplit As TextBox
    Friend WithEvents Txt_Jumlah As TextBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Txt_HasilProduksi As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Barcode As PictureBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents CmbJenis As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents DtpExpired As DateTimePicker
    Friend WithEvents Txt_NamaBarang As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents DtpProduksi As DateTimePicker
    Friend WithEvents TxtLifeTime As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtFormulator_NoFaktur As TextBox
    Friend WithEvents Cmb_LokasiSimpan As ComboBox
    Friend WithEvents TxtJmlScrap As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtJam As TextBox
    Friend WithEvents CmbSatScrap As ComboBox
    Friend WithEvents Cmb_SatuanProduksi As ComboBox
    Friend WithEvents TxtSatuanKecil As TextBox
    Friend WithEvents TxtSatScrapKecil As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents CmbSisaProduksi As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Dgv_Packaging As DataGridView
    Friend WithEvents TxtGroupJenis As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Cmb_Lokasi_Gudang_Sisa As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Txt_Troli As TextBox
    Friend WithEvents TxtJumlahKeranjang As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Cmb_Tahapan As ComboBox
End Class
