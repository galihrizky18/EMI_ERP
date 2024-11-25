<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Emi_Transfer_Quality_Production
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Lbl_Supplier = New System.Windows.Forms.Label()
        Me.Txt_NoSplit = New System.Windows.Forms.TextBox()
        Me.Txt_NoPO = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Stock = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Txt_Satuan = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtNo_Transaksi = New System.Windows.Forms.TextBox()
        Me.DGV_Data_TF = New System.Windows.Forms.DataGridView()
        Me.no_transaksi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_warehouse = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rak = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jenis = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jumlah_besar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan_besar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jenis_fix = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.jumlah_fix = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sample = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nilai_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan_kecil = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.proses = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.urut_oto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.qr_code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.batch_number = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.CmbSO = New System.Windows.Forms.ComboBox()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DGV_Data_TF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1151, 51)
        Me.Panel1.TabIndex = 25
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1151, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(247, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Transfer Quality Stock"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(20, 50)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 40
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Location = New System.Drawing.Point(0, 51)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 498)
        Me.Panel3.TabIndex = 39
        Me.Panel3.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(12, 137)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(924, 12)
        Me.Panel6.TabIndex = 232
        Me.Panel6.Visible = False
        '
        'Lbl_Supplier
        '
        Me.Lbl_Supplier.AutoSize = True
        Me.Lbl_Supplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Lbl_Supplier.Location = New System.Drawing.Point(26, 126)
        Me.Lbl_Supplier.Name = "Lbl_Supplier"
        Me.Lbl_Supplier.Size = New System.Drawing.Size(57, 17)
        Me.Lbl_Supplier.TabIndex = 433
        Me.Lbl_Supplier.Text = "No Split"
        '
        'Txt_NoSplit
        '
        Me.Txt_NoSplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoSplit.Enabled = False
        Me.Txt_NoSplit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_NoSplit.Location = New System.Drawing.Point(139, 124)
        Me.Txt_NoSplit.MaxLength = 50
        Me.Txt_NoSplit.Name = "Txt_NoSplit"
        Me.Txt_NoSplit.Size = New System.Drawing.Size(252, 21)
        Me.Txt_NoSplit.TabIndex = 432
        '
        'Txt_NoPO
        '
        Me.Txt_NoPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoPO.Enabled = False
        Me.Txt_NoPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_NoPO.Location = New System.Drawing.Point(139, 151)
        Me.Txt_NoPO.MaxLength = 50
        Me.Txt_NoPO.Name = "Txt_NoPO"
        Me.Txt_NoPO.Size = New System.Drawing.Size(252, 21)
        Me.Txt_NoPO.TabIndex = 432
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(26, 153)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 17)
        Me.Label1.TabIndex = 433
        Me.Label1.Text = "No PO"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(26, 99)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 17)
        Me.Label2.TabIndex = 433
        Me.Label2.Text = "Lokasi"
        '
        'Txt_Stock
        '
        Me.Txt_Stock.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Stock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Stock.Enabled = False
        Me.Txt_Stock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Stock.Location = New System.Drawing.Point(643, 214)
        Me.Txt_Stock.MaxLength = 50
        Me.Txt_Stock.Name = "Txt_Stock"
        Me.Txt_Stock.Size = New System.Drawing.Size(92, 21)
        Me.Txt_Stock.TabIndex = 478
        Me.Txt_Stock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Location = New System.Drawing.Point(643, 188)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 21)
        Me.Label9.TabIndex = 477
        Me.Label9.Text = "Stock"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Txt_Satuan
        '
        Me.Txt_Satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Satuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Satuan.Enabled = False
        Me.Txt_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Satuan.Location = New System.Drawing.Point(739, 214)
        Me.Txt_Satuan.MaxLength = 50
        Me.Txt_Satuan.Name = "Txt_Satuan"
        Me.Txt_Satuan.Size = New System.Drawing.Size(93, 21)
        Me.Txt_Satuan.TabIndex = 476
        Me.Txt_Satuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(739, 188)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(93, 21)
        Me.Label8.TabIndex = 475
        Me.Label8.Text = "Satuan"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmBarang.Enabled = False
        Me.Txt_NmBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_NmBarang.Location = New System.Drawing.Point(197, 214)
        Me.Txt_NmBarang.MaxLength = 50
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.ReadOnly = True
        Me.Txt_NmBarang.Size = New System.Drawing.Size(440, 21)
        Me.Txt_NmBarang.TabIndex = 474
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Enabled = False
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_KdBarang.Location = New System.Drawing.Point(29, 214)
        Me.Txt_KdBarang.MaxLength = 50
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(164, 21)
        Me.Txt_KdBarang.TabIndex = 473
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(29, 188)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(164, 21)
        Me.Label6.TabIndex = 471
        Me.Label6.Text = "Kode Barang"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(197, 188)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(440, 21)
        Me.Label7.TabIndex = 472
        Me.Label7.Text = "Nama Barang"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtNo_Transaksi
        '
        Me.TxtNo_Transaksi.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtNo_Transaksi.Enabled = False
        Me.TxtNo_Transaksi.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNo_Transaksi.Location = New System.Drawing.Point(19, 62)
        Me.TxtNo_Transaksi.MaxLength = 50
        Me.TxtNo_Transaksi.Name = "TxtNo_Transaksi"
        Me.TxtNo_Transaksi.Size = New System.Drawing.Size(210, 21)
        Me.TxtNo_Transaksi.TabIndex = 479
        '
        'DGV_Data_TF
        '
        Me.DGV_Data_TF.AllowUserToAddRows = False
        Me.DGV_Data_TF.AllowUserToDeleteRows = False
        Me.DGV_Data_TF.AllowUserToResizeColumns = False
        Me.DGV_Data_TF.AllowUserToResizeRows = False
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Data_TF.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV_Data_TF.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DGV_Data_TF.BackgroundColor = System.Drawing.Color.White
        Me.DGV_Data_TF.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Data_TF.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGV_Data_TF.ColumnHeadersHeight = 35
        Me.DGV_Data_TF.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.no_transaksi, Me.id_warehouse, Me.rak, Me.jenis, Me.jumlah_besar, Me.satuan_besar, Me.jenis_fix, Me.jumlah_fix, Me.sample, Me.nilai_barang, Me.satuan_kecil, Me.proses, Me.urut_oto, Me.qr_code, Me.batch_number, Me.sn})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Data_TF.DefaultCellStyle = DataGridViewCellStyle9
        Me.DGV_Data_TF.Location = New System.Drawing.Point(29, 251)
        Me.DGV_Data_TF.MultiSelect = False
        Me.DGV_Data_TF.Name = "DGV_Data_TF"
        Me.DGV_Data_TF.RowHeadersWidth = 21
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Data_TF.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.DGV_Data_TF.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGV_Data_TF.Size = New System.Drawing.Size(1105, 372)
        Me.DGV_Data_TF.TabIndex = 480
        '
        'no_transaksi
        '
        Me.no_transaksi.HeaderText = "no_transaksi"
        Me.no_transaksi.Name = "no_transaksi"
        Me.no_transaksi.Visible = False
        '
        'id_warehouse
        '
        Me.id_warehouse.HeaderText = "ID warehouse"
        Me.id_warehouse.Name = "id_warehouse"
        Me.id_warehouse.Visible = False
        '
        'rak
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.rak.DefaultCellStyle = DataGridViewCellStyle3
        Me.rak.HeaderText = "Rak"
        Me.rak.Name = "rak"
        Me.rak.ReadOnly = True
        Me.rak.Width = 280
        '
        'jenis
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.jenis.DefaultCellStyle = DataGridViewCellStyle4
        Me.jenis.HeaderText = "Jenis"
        Me.jenis.Name = "jenis"
        Me.jenis.ReadOnly = True
        Me.jenis.Width = 200
        '
        'jumlah_besar
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.jumlah_besar.DefaultCellStyle = DataGridViewCellStyle5
        Me.jumlah_besar.HeaderText = "Jumlah"
        Me.jumlah_besar.Name = "jumlah_besar"
        Me.jumlah_besar.ReadOnly = True
        '
        'satuan_besar
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.satuan_besar.DefaultCellStyle = DataGridViewCellStyle6
        Me.satuan_besar.HeaderText = "Satuan"
        Me.satuan_besar.Name = "satuan_besar"
        Me.satuan_besar.ReadOnly = True
        '
        'jenis_fix
        '
        Me.jenis_fix.HeaderText = "Jenis Berubah"
        Me.jenis_fix.Name = "jenis_fix"
        Me.jenis_fix.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.jenis_fix.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.jenis_fix.Width = 200
        '
        'jumlah_fix
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.jumlah_fix.DefaultCellStyle = DataGridViewCellStyle7
        Me.jumlah_fix.HeaderText = "Jumlah Berubah"
        Me.jumlah_fix.Name = "jumlah_fix"
        '
        'sample
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.sample.DefaultCellStyle = DataGridViewCellStyle8
        Me.sample.HeaderText = "Sample"
        Me.sample.Name = "sample"
        '
        'nilai_barang
        '
        Me.nilai_barang.HeaderText = "Nilai Barang"
        Me.nilai_barang.Name = "nilai_barang"
        Me.nilai_barang.Visible = False
        '
        'satuan_kecil
        '
        Me.satuan_kecil.HeaderText = "Satuan Kecil"
        Me.satuan_kecil.Name = "satuan_kecil"
        Me.satuan_kecil.Visible = False
        '
        'proses
        '
        Me.proses.HeaderText = "Proses"
        Me.proses.Name = "proses"
        Me.proses.Visible = False
        '
        'urut_oto
        '
        Me.urut_oto.HeaderText = "Urut OTo"
        Me.urut_oto.Name = "urut_oto"
        Me.urut_oto.ReadOnly = True
        Me.urut_oto.Visible = False
        '
        'qr_code
        '
        Me.qr_code.HeaderText = "QR code"
        Me.qr_code.Name = "qr_code"
        Me.qr_code.Visible = False
        '
        'batch_number
        '
        Me.batch_number.HeaderText = "Batch Number"
        Me.batch_number.Name = "batch_number"
        Me.batch_number.Visible = False
        '
        'sn
        '
        Me.sn.HeaderText = "Sn"
        Me.sn.Name = "sn"
        Me.sn.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Location = New System.Drawing.Point(1131, 188)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 498)
        Me.Panel4.TabIndex = 39
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(12, 137)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(924, 12)
        Me.Panel5.TabIndex = 232
        Me.Panel5.Visible = False
        '
        'CmbSO
        '
        Me.CmbSO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSO.Enabled = False
        Me.CmbSO.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSO.FormattingEnabled = True
        Me.CmbSO.Location = New System.Drawing.Point(139, 96)
        Me.CmbSO.Name = "CmbSO"
        Me.CmbSO.Size = New System.Drawing.Size(252, 24)
        Me.CmbSO.TabIndex = 481
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(29, 643)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(118, 36)
        Me.Btn_Simpan.TabIndex = 484
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(29, 680)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(942, 12)
        Me.Panel8.TabIndex = 482
        Me.Panel8.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(29, 630)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 12)
        Me.Panel7.TabIndex = 483
        Me.Panel7.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(149, 643)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(118, 36)
        Me.Btn_Refresh.TabIndex = 484
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Emi_Transfer_Quality_Production
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1151, 694)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.CmbSO)
        Me.Controls.Add(Me.DGV_Data_TF)
        Me.Controls.Add(Me.TxtNo_Transaksi)
        Me.Controls.Add(Me.Txt_Stock)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Txt_Satuan)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Txt_NmBarang)
        Me.Controls.Add(Me.Txt_KdBarang)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Lbl_Supplier)
        Me.Controls.Add(Me.Txt_NoPO)
        Me.Controls.Add(Me.Txt_NoSplit)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Emi_Transfer_Quality_Production"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.DGV_Data_TF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Lbl_Supplier As Label
    Friend WithEvents Txt_NoSplit As TextBox
    Friend WithEvents Txt_NoPO As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Stock As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Txt_Satuan As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtNo_Transaksi As TextBox
    Friend WithEvents DGV_Data_TF As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents CmbSO As ComboBox
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents no_transaksi As DataGridViewTextBoxColumn
    Friend WithEvents id_warehouse As DataGridViewTextBoxColumn
    Friend WithEvents rak As DataGridViewTextBoxColumn
    Friend WithEvents jenis As DataGridViewTextBoxColumn
    Friend WithEvents jumlah_besar As DataGridViewTextBoxColumn
    Friend WithEvents satuan_besar As DataGridViewTextBoxColumn
    Friend WithEvents jenis_fix As DataGridViewComboBoxColumn
    Friend WithEvents jumlah_fix As DataGridViewTextBoxColumn
    Friend WithEvents sample As DataGridViewTextBoxColumn
    Friend WithEvents nilai_barang As DataGridViewTextBoxColumn
    Friend WithEvents satuan_kecil As DataGridViewTextBoxColumn
    Friend WithEvents proses As DataGridViewTextBoxColumn
    Friend WithEvents urut_oto As DataGridViewTextBoxColumn
    Friend WithEvents qr_code As DataGridViewTextBoxColumn
    Friend WithEvents batch_number As DataGridViewTextBoxColumn
    Friend WithEvents sn As DataGridViewTextBoxColumn
    Friend WithEvents Btn_Refresh As Button
End Class
