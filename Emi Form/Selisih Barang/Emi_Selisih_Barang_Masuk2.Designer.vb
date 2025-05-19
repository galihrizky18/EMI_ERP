<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Selisih_Barang_Masuk2
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_FakturPO = New System.Windows.Forms.TextBox()
        Me.Cmb_LokasiPO = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Txt_JamMasuk = New System.Windows.Forms.TextBox()
        Me.Txt_TglMasuk = New System.Windows.Forms.TextBox()
        Me.Txt_TglBerangkat = New System.Windows.Forms.TextBox()
        Me.Txt_Driver = New System.Windows.Forms.TextBox()
        Me.Txt_Plat = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_NoSJ = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_Supplier = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Dgv_DetailBarang = New System.Windows.Forms.DataGridView()
        Me.lokasi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kode_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nama = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jumlah_pl = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jumlah_bm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.selisih = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.harga_pcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tgl_produksi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tgl_expired = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.JmlhPlHitung = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jmlhBMHitung = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hargabarang_hitung = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jumlah_utang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Cmb_JenisSelisih = New System.Windows.Forms.ComboBox()
        Me.lblJenisSelisih = New System.Windows.Forms.Label()
        Me.LblSelisihBrgMsk_TotalHarga = New System.Windows.Forms.Label()
        Me.Txt_TotSelisihRP = New System.Windows.Forms.TextBox()
        Me.LblSelisihBrgMsk_TotalQty = New System.Windows.Forms.Label()
        Me.Txt_TotSelisihQTY = New System.Windows.Forms.TextBox()
        Me.BtnSelisihBrgMsk_Refresh = New System.Windows.Forms.Button()
        Me.BtnSelisihBrgMsk_Simpan = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Txt_NoFaktur = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Dgv_DetailBarang, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1070, 51)
        Me.Panel1.TabIndex = 86
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1070, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(14, 9)
        Me.Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(241, 25)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Selisih Barang Masuk"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 58)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(16, 618)
        Me.Panel3.TabIndex = 279
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 48)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1031, 12)
        Me.Panel2.TabIndex = 278
        Me.Panel2.Visible = False
        '
        'Txt_FakturPO
        '
        Me.Txt_FakturPO.BackColor = System.Drawing.Color.Goldenrod
        Me.Txt_FakturPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_FakturPO.ForeColor = System.Drawing.SystemColors.Window
        Me.Txt_FakturPO.Location = New System.Drawing.Point(570, 60)
        Me.Txt_FakturPO.MaxLength = 10
        Me.Txt_FakturPO.Name = "Txt_FakturPO"
        Me.Txt_FakturPO.ReadOnly = True
        Me.Txt_FakturPO.Size = New System.Drawing.Size(211, 21)
        Me.Txt_FakturPO.TabIndex = 280
        Me.Txt_FakturPO.Visible = False
        '
        'Cmb_LokasiPO
        '
        Me.Cmb_LokasiPO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_LokasiPO.Enabled = False
        Me.Cmb_LokasiPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_LokasiPO.FormattingEnabled = True
        Me.Cmb_LokasiPO.Location = New System.Drawing.Point(232, 60)
        Me.Cmb_LokasiPO.Name = "Cmb_LokasiPO"
        Me.Cmb_LokasiPO.Size = New System.Drawing.Size(178, 23)
        Me.Cmb_LokasiPO.TabIndex = 281
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Txt_JamMasuk)
        Me.GroupBox1.Controls.Add(Me.Txt_TglMasuk)
        Me.GroupBox1.Controls.Add(Me.Txt_TglBerangkat)
        Me.GroupBox1.Controls.Add(Me.Txt_Driver)
        Me.GroupBox1.Controls.Add(Me.Txt_Plat)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Txt_NoSJ)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Txt_Supplier)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 91)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1032, 156)
        Me.GroupBox1.TabIndex = 282
        Me.GroupBox1.TabStop = False
        '
        'Txt_JamMasuk
        '
        Me.Txt_JamMasuk.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_JamMasuk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_JamMasuk.Enabled = False
        Me.Txt_JamMasuk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_JamMasuk.Location = New System.Drawing.Point(609, 81)
        Me.Txt_JamMasuk.Name = "Txt_JamMasuk"
        Me.Txt_JamMasuk.Size = New System.Drawing.Size(273, 22)
        Me.Txt_JamMasuk.TabIndex = 282
        '
        'Txt_TglMasuk
        '
        Me.Txt_TglMasuk.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TglMasuk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TglMasuk.Enabled = False
        Me.Txt_TglMasuk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_TglMasuk.Location = New System.Drawing.Point(609, 52)
        Me.Txt_TglMasuk.Name = "Txt_TglMasuk"
        Me.Txt_TglMasuk.Size = New System.Drawing.Size(273, 22)
        Me.Txt_TglMasuk.TabIndex = 282
        '
        'Txt_TglBerangkat
        '
        Me.Txt_TglBerangkat.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TglBerangkat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TglBerangkat.Enabled = False
        Me.Txt_TglBerangkat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_TglBerangkat.Location = New System.Drawing.Point(609, 23)
        Me.Txt_TglBerangkat.Name = "Txt_TglBerangkat"
        Me.Txt_TglBerangkat.Size = New System.Drawing.Size(273, 22)
        Me.Txt_TglBerangkat.TabIndex = 282
        '
        'Txt_Driver
        '
        Me.Txt_Driver.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Driver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Driver.Enabled = False
        Me.Txt_Driver.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Driver.Location = New System.Drawing.Point(150, 111)
        Me.Txt_Driver.Name = "Txt_Driver"
        Me.Txt_Driver.Size = New System.Drawing.Size(273, 22)
        Me.Txt_Driver.TabIndex = 282
        '
        'Txt_Plat
        '
        Me.Txt_Plat.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Plat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Plat.Enabled = False
        Me.Txt_Plat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Plat.Location = New System.Drawing.Point(150, 82)
        Me.Txt_Plat.Name = "Txt_Plat"
        Me.Txt_Plat.Size = New System.Drawing.Size(273, 22)
        Me.Txt_Plat.TabIndex = 282
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(454, 83)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 16)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Jam Masuk"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Driver"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(454, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 16)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Tanggal Masuk"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "No Surat Jalan"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(454, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Tanggal Berangkat"
        '
        'Txt_NoSJ
        '
        Me.Txt_NoSJ.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoSJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoSJ.Enabled = False
        Me.Txt_NoSJ.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoSJ.Location = New System.Drawing.Point(150, 53)
        Me.Txt_NoSJ.Name = "Txt_NoSJ"
        Me.Txt_NoSJ.Size = New System.Drawing.Size(273, 22)
        Me.Txt_NoSJ.TabIndex = 282
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Plat Kendaraan"
        '
        'Txt_Supplier
        '
        Me.Txt_Supplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Supplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Supplier.Enabled = False
        Me.Txt_Supplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Supplier.Location = New System.Drawing.Point(150, 24)
        Me.Txt_Supplier.Name = "Txt_Supplier"
        Me.Txt_Supplier.Size = New System.Drawing.Size(273, 22)
        Me.Txt_Supplier.TabIndex = 282
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Supplier"
        '
        'Dgv_DetailBarang
        '
        Me.Dgv_DetailBarang.AllowUserToAddRows = False
        Me.Dgv_DetailBarang.AllowUserToDeleteRows = False
        Me.Dgv_DetailBarang.AllowUserToResizeColumns = False
        Me.Dgv_DetailBarang.AllowUserToResizeRows = False
        Me.Dgv_DetailBarang.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_DetailBarang.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Dgv_DetailBarang.ColumnHeadersHeight = 45
        Me.Dgv_DetailBarang.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.lokasi, Me.kode_barang, Me.nama, Me.jumlah_pl, Me.jumlah_bm, Me.satuan, Me.selisih, Me.Column2, Me.harga_pcs, Me.tgl_produksi, Me.tgl_expired, Me.JmlhPlHitung, Me.jmlhBMHitung, Me.hargabarang_hitung, Me.Column1, Me.jumlah_utang})
        Me.Dgv_DetailBarang.Location = New System.Drawing.Point(16, 264)
        Me.Dgv_DetailBarang.MultiSelect = False
        Me.Dgv_DetailBarang.Name = "Dgv_DetailBarang"
        Me.Dgv_DetailBarang.RowHeadersWidth = 21
        Me.Dgv_DetailBarang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Dgv_DetailBarang.Size = New System.Drawing.Size(1035, 311)
        Me.Dgv_DetailBarang.TabIndex = 283
        '
        'lokasi
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.lokasi.DefaultCellStyle = DataGridViewCellStyle2
        Me.lokasi.HeaderText = "Lokasi"
        Me.lokasi.Name = "lokasi"
        Me.lokasi.ReadOnly = True
        Me.lokasi.Visible = False
        Me.lokasi.Width = 125
        '
        'kode_barang
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.kode_barang.DefaultCellStyle = DataGridViewCellStyle3
        Me.kode_barang.HeaderText = "Kode Barang"
        Me.kode_barang.Name = "kode_barang"
        Me.kode_barang.ReadOnly = True
        Me.kode_barang.Width = 130
        '
        'nama
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.nama.DefaultCellStyle = DataGridViewCellStyle4
        Me.nama.HeaderText = "Nama"
        Me.nama.Name = "nama"
        Me.nama.ReadOnly = True
        Me.nama.Width = 250
        '
        'jumlah_pl
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.jumlah_pl.DefaultCellStyle = DataGridViewCellStyle5
        Me.jumlah_pl.HeaderText = "Jumlah PL"
        Me.jumlah_pl.Name = "jumlah_pl"
        Me.jumlah_pl.ReadOnly = True
        Me.jumlah_pl.Width = 110
        '
        'jumlah_bm
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.jumlah_bm.DefaultCellStyle = DataGridViewCellStyle6
        Me.jumlah_bm.HeaderText = "Jumlah BM"
        Me.jumlah_bm.Name = "jumlah_bm"
        Me.jumlah_bm.ReadOnly = True
        Me.jumlah_bm.Width = 110
        '
        'satuan
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.satuan.DefaultCellStyle = DataGridViewCellStyle7
        Me.satuan.HeaderText = "Satuan"
        Me.satuan.Name = "satuan"
        Me.satuan.ReadOnly = True
        Me.satuan.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.satuan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'selisih
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.selisih.DefaultCellStyle = DataGridViewCellStyle8
        Me.selisih.HeaderText = "Selisih"
        Me.selisih.Name = "selisih"
        Me.selisih.ReadOnly = True
        Me.selisih.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.selisih.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.selisih.Width = 125
        '
        'Column2
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle9
        Me.Column2.HeaderText = "Selisih Harga"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        Me.Column2.Width = 120
        '
        'harga_pcs
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.harga_pcs.DefaultCellStyle = DataGridViewCellStyle10
        Me.harga_pcs.HeaderText = "Harga Barang"
        Me.harga_pcs.Name = "harga_pcs"
        Me.harga_pcs.ReadOnly = True
        Me.harga_pcs.Visible = False
        Me.harga_pcs.Width = 125
        '
        'tgl_produksi
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.tgl_produksi.DefaultCellStyle = DataGridViewCellStyle11
        Me.tgl_produksi.HeaderText = "Tgl Produksi"
        Me.tgl_produksi.Name = "tgl_produksi"
        Me.tgl_produksi.ReadOnly = True
        Me.tgl_produksi.Width = 120
        '
        'tgl_expired
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.tgl_expired.DefaultCellStyle = DataGridViewCellStyle12
        Me.tgl_expired.HeaderText = "Tgl Expired"
        Me.tgl_expired.Name = "tgl_expired"
        Me.tgl_expired.ReadOnly = True
        Me.tgl_expired.Width = 120
        '
        'JmlhPlHitung
        '
        Me.JmlhPlHitung.HeaderText = "JmlhPL"
        Me.JmlhPlHitung.Name = "JmlhPlHitung"
        Me.JmlhPlHitung.Visible = False
        '
        'jmlhBMHitung
        '
        Me.jmlhBMHitung.HeaderText = "JmlhBM"
        Me.jmlhBMHitung.Name = "jmlhBMHitung"
        Me.jmlhBMHitung.Visible = False
        '
        'hargabarang_hitung
        '
        Me.hargabarang_hitung.HeaderText = "Harga Barang Hitung"
        Me.hargabarang_hitung.Name = "hargabarang_hitung"
        Me.hargabarang_hitung.Visible = False
        '
        'Column1
        '
        Me.Column1.HeaderText = "Urut"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'jumlah_utang
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight
        Me.jumlah_utang.DefaultCellStyle = DataGridViewCellStyle13
        Me.jumlah_utang.HeaderText = "Jumlah Utang"
        Me.jumlah_utang.Name = "jumlah_utang"
        Me.jumlah_utang.Width = 110
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 248)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1031, 12)
        Me.Panel4.TabIndex = 278
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(20, 580)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1031, 12)
        Me.Panel5.TabIndex = 278
        Me.Panel5.Visible = False
        '
        'Cmb_JenisSelisih
        '
        Me.Cmb_JenisSelisih.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_JenisSelisih.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_JenisSelisih.FormattingEnabled = True
        Me.Cmb_JenisSelisih.Location = New System.Drawing.Point(139, 591)
        Me.Cmb_JenisSelisih.Name = "Cmb_JenisSelisih"
        Me.Cmb_JenisSelisih.Size = New System.Drawing.Size(280, 23)
        Me.Cmb_JenisSelisih.TabIndex = 289
        '
        'lblJenisSelisih
        '
        Me.lblJenisSelisih.AutoSize = True
        Me.lblJenisSelisih.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJenisSelisih.Location = New System.Drawing.Point(22, 595)
        Me.lblJenisSelisih.Name = "lblJenisSelisih"
        Me.lblJenisSelisih.Size = New System.Drawing.Size(82, 16)
        Me.lblJenisSelisih.TabIndex = 288
        Me.lblJenisSelisih.Text = "Jenis Selisih"
        '
        'LblSelisihBrgMsk_TotalHarga
        '
        Me.LblSelisihBrgMsk_TotalHarga.AutoSize = True
        Me.LblSelisihBrgMsk_TotalHarga.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSelisihBrgMsk_TotalHarga.Location = New System.Drawing.Point(662, 636)
        Me.LblSelisihBrgMsk_TotalHarga.Name = "LblSelisihBrgMsk_TotalHarga"
        Me.LblSelisihBrgMsk_TotalHarga.Size = New System.Drawing.Size(128, 16)
        Me.LblSelisihBrgMsk_TotalHarga.TabIndex = 287
        Me.LblSelisihBrgMsk_TotalHarga.Text = "Total Selisih (Rp)"
        Me.LblSelisihBrgMsk_TotalHarga.Visible = False
        '
        'Txt_TotSelisihRP
        '
        Me.Txt_TotSelisihRP.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TotSelisihRP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotSelisihRP.Enabled = False
        Me.Txt_TotSelisihRP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_TotSelisihRP.Location = New System.Drawing.Point(818, 632)
        Me.Txt_TotSelisihRP.Name = "Txt_TotSelisihRP"
        Me.Txt_TotSelisihRP.Size = New System.Drawing.Size(168, 21)
        Me.Txt_TotSelisihRP.TabIndex = 286
        Me.Txt_TotSelisihRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Txt_TotSelisihRP.Visible = False
        '
        'LblSelisihBrgMsk_TotalQty
        '
        Me.LblSelisihBrgMsk_TotalQty.AutoSize = True
        Me.LblSelisihBrgMsk_TotalQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSelisihBrgMsk_TotalQty.Location = New System.Drawing.Point(718, 604)
        Me.LblSelisihBrgMsk_TotalQty.Name = "LblSelisihBrgMsk_TotalQty"
        Me.LblSelisihBrgMsk_TotalQty.Size = New System.Drawing.Size(94, 16)
        Me.LblSelisihBrgMsk_TotalQty.TabIndex = 285
        Me.LblSelisihBrgMsk_TotalQty.Text = "Total Selisih"
        '
        'Txt_TotSelisihQTY
        '
        Me.Txt_TotSelisihQTY.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TotSelisihQTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotSelisihQTY.Enabled = False
        Me.Txt_TotSelisihQTY.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_TotSelisihQTY.Location = New System.Drawing.Point(818, 602)
        Me.Txt_TotSelisihQTY.Name = "Txt_TotSelisihQTY"
        Me.Txt_TotSelisihQTY.Size = New System.Drawing.Size(231, 21)
        Me.Txt_TotSelisihQTY.TabIndex = 284
        Me.Txt_TotSelisihQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BtnSelisihBrgMsk_Refresh
        '
        Me.BtnSelisihBrgMsk_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnSelisihBrgMsk_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelisihBrgMsk_Refresh.ForeColor = System.Drawing.Color.White
        Me.BtnSelisihBrgMsk_Refresh.Location = New System.Drawing.Point(182, 626)
        Me.BtnSelisihBrgMsk_Refresh.Name = "BtnSelisihBrgMsk_Refresh"
        Me.BtnSelisihBrgMsk_Refresh.Size = New System.Drawing.Size(156, 35)
        Me.BtnSelisihBrgMsk_Refresh.TabIndex = 290
        Me.BtnSelisihBrgMsk_Refresh.Text = "&Refresh"
        Me.BtnSelisihBrgMsk_Refresh.UseVisualStyleBackColor = False
        '
        'BtnSelisihBrgMsk_Simpan
        '
        Me.BtnSelisihBrgMsk_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnSelisihBrgMsk_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelisihBrgMsk_Simpan.ForeColor = System.Drawing.Color.White
        Me.BtnSelisihBrgMsk_Simpan.Location = New System.Drawing.Point(25, 626)
        Me.BtnSelisihBrgMsk_Simpan.Name = "BtnSelisihBrgMsk_Simpan"
        Me.BtnSelisihBrgMsk_Simpan.Size = New System.Drawing.Size(156, 35)
        Me.BtnSelisihBrgMsk_Simpan.TabIndex = 291
        Me.BtnSelisihBrgMsk_Simpan.Text = "&Simpan"
        Me.BtnSelisihBrgMsk_Simpan.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(22, 667)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1031, 12)
        Me.Panel6.TabIndex = 278
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(1055, 91)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(16, 618)
        Me.Panel7.TabIndex = 279
        Me.Panel7.Visible = False
        '
        'Txt_NoFaktur
        '
        Me.Txt_NoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.Txt_NoFaktur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoFaktur.ForeColor = System.Drawing.SystemColors.Window
        Me.Txt_NoFaktur.Location = New System.Drawing.Point(16, 61)
        Me.Txt_NoFaktur.MaxLength = 10
        Me.Txt_NoFaktur.Name = "Txt_NoFaktur"
        Me.Txt_NoFaktur.ReadOnly = True
        Me.Txt_NoFaktur.Size = New System.Drawing.Size(211, 21)
        Me.Txt_NoFaktur.TabIndex = 280
        '
        'EMI_Selisih_Barang_Masuk2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1070, 676)
        Me.Controls.Add(Me.BtnSelisihBrgMsk_Refresh)
        Me.Controls.Add(Me.BtnSelisihBrgMsk_Simpan)
        Me.Controls.Add(Me.Cmb_JenisSelisih)
        Me.Controls.Add(Me.lblJenisSelisih)
        Me.Controls.Add(Me.LblSelisihBrgMsk_TotalHarga)
        Me.Controls.Add(Me.Txt_TotSelisihRP)
        Me.Controls.Add(Me.LblSelisihBrgMsk_TotalQty)
        Me.Controls.Add(Me.Txt_TotSelisihQTY)
        Me.Controls.Add(Me.Dgv_DetailBarang)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Cmb_LokasiPO)
        Me.Controls.Add(Me.Txt_NoFaktur)
        Me.Controls.Add(Me.Txt_FakturPO)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Selisih_Barang_Masuk2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Dgv_DetailBarang, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Txt_FakturPO As TextBox
    Friend WithEvents Cmb_LokasiPO As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Supplier As TextBox
    Friend WithEvents Txt_TglBerangkat As TextBox
    Friend WithEvents Txt_Plat As TextBox
    Friend WithEvents Txt_NoSJ As TextBox
    Friend WithEvents Dgv_DetailBarang As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Cmb_JenisSelisih As ComboBox
    Friend WithEvents lblJenisSelisih As Label
    Friend WithEvents LblSelisihBrgMsk_TotalHarga As Label
    Friend WithEvents Txt_TotSelisihRP As TextBox
    Friend WithEvents LblSelisihBrgMsk_TotalQty As Label
    Friend WithEvents Txt_TotSelisihQTY As TextBox
    Friend WithEvents BtnSelisihBrgMsk_Refresh As Button
    Friend WithEvents BtnSelisihBrgMsk_Simpan As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Txt_JamMasuk As TextBox
    Friend WithEvents Txt_TglMasuk As TextBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Txt_Driver As TextBox
    Friend WithEvents Txt_NoFaktur As TextBox
    Friend WithEvents lokasi As DataGridViewTextBoxColumn
    Friend WithEvents kode_barang As DataGridViewTextBoxColumn
    Friend WithEvents nama As DataGridViewTextBoxColumn
    Friend WithEvents jumlah_pl As DataGridViewTextBoxColumn
    Friend WithEvents jumlah_bm As DataGridViewTextBoxColumn
    Friend WithEvents satuan As DataGridViewTextBoxColumn
    Friend WithEvents selisih As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents harga_pcs As DataGridViewTextBoxColumn
    Friend WithEvents tgl_produksi As DataGridViewTextBoxColumn
    Friend WithEvents tgl_expired As DataGridViewTextBoxColumn
    Friend WithEvents JmlhPlHitung As DataGridViewTextBoxColumn
    Friend WithEvents jmlhBMHitung As DataGridViewTextBoxColumn
    Friend WithEvents hargabarang_hitung As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents jumlah_utang As DataGridViewTextBoxColumn
End Class
