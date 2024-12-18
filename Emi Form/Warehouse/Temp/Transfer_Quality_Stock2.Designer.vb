<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Transfer_Quality_Stock2
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
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Btn_GetData = New System.Windows.Forms.Button()
        Me.TxtNamaBarang = New System.Windows.Forms.TextBox()
        Me.TxtSo = New System.Windows.Forms.TextBox()
        Me.TxtGoodStock = New System.Windows.Forms.TextBox()
        Me.TxtKd_Barang = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_StockOwner = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Dgv_Stock = New System.Windows.Forms.DataGridView()
        Me.kode_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.stock_owner = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.barang_sn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nama = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.position = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.good_stock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.warning_stock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bad_stock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan_display = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.checkbox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.jumlah_transfer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cmb_QualityTo = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Cmb_QualityFrom = New System.Windows.Forms.ComboBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.Dgv_Stock, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(1201, 51)
        Me.Panel1.TabIndex = 25
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(267, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Transfer Quality Stock 2"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(18, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 10)
        Me.Panel2.TabIndex = 40
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 52)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 498)
        Me.Panel3.TabIndex = 39
        Me.Panel3.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Btn_GetData)
        Me.GroupBox1.Controls.Add(Me.TxtNamaBarang)
        Me.GroupBox1.Controls.Add(Me.TxtSo)
        Me.GroupBox1.Controls.Add(Me.TxtGoodStock)
        Me.GroupBox1.Controls.Add(Me.TxtKd_Barang)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Cmb_StockOwner)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(20, 61)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1157, 120)
        Me.GroupBox1.TabIndex = 41
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Barang"
        '
        'Btn_GetData
        '
        Me.Btn_GetData.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_GetData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_GetData.ForeColor = System.Drawing.Color.White
        Me.Btn_GetData.Location = New System.Drawing.Point(812, 51)
        Me.Btn_GetData.Name = "Btn_GetData"
        Me.Btn_GetData.Size = New System.Drawing.Size(60, 51)
        Me.Btn_GetData.TabIndex = 463
        Me.Btn_GetData.Text = "&Get"
        Me.Btn_GetData.UseVisualStyleBackColor = False
        '
        'TxtNamaBarang
        '
        Me.TxtNamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNamaBarang.Enabled = False
        Me.TxtNamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtNamaBarang.Location = New System.Drawing.Point(344, 81)
        Me.TxtNamaBarang.MaxLength = 50
        Me.TxtNamaBarang.Name = "TxtNamaBarang"
        Me.TxtNamaBarang.Size = New System.Drawing.Size(352, 21)
        Me.TxtNamaBarang.TabIndex = 440
        '
        'TxtSo
        '
        Me.TxtSo.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSo.Enabled = False
        Me.TxtSo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtSo.Location = New System.Drawing.Point(152, 81)
        Me.TxtSo.MaxLength = 50
        Me.TxtSo.Name = "TxtSo"
        Me.TxtSo.Size = New System.Drawing.Size(186, 21)
        Me.TxtSo.TabIndex = 440
        '
        'TxtGoodStock
        '
        Me.TxtGoodStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtGoodStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGoodStock.Enabled = False
        Me.TxtGoodStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtGoodStock.Location = New System.Drawing.Point(702, 81)
        Me.TxtGoodStock.MaxLength = 50
        Me.TxtGoodStock.Name = "TxtGoodStock"
        Me.TxtGoodStock.Size = New System.Drawing.Size(104, 21)
        Me.TxtGoodStock.TabIndex = 440
        '
        'TxtKd_Barang
        '
        Me.TxtKd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKd_Barang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtKd_Barang.Location = New System.Drawing.Point(6, 81)
        Me.TxtKd_Barang.MaxLength = 50
        Me.TxtKd_Barang.Name = "TxtKd_Barang"
        Me.TxtKd_Barang.Size = New System.Drawing.Size(140, 21)
        Me.TxtKd_Barang.TabIndex = 440
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(344, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(352, 21)
        Me.Label4.TabIndex = 439
        Me.Label4.Text = "Nama Barang"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(152, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(186, 21)
        Me.Label2.TabIndex = 439
        Me.Label2.Text = "Kode Stock Owner"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(702, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 21)
        Me.Label5.TabIndex = 439
        Me.Label5.Text = "Good Stock"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(6, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(140, 21)
        Me.Label6.TabIndex = 439
        Me.Label6.Text = "Kode Barang"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Cmb_StockOwner
        '
        Me.Cmb_StockOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_StockOwner.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_StockOwner.FormattingEnabled = True
        Me.Cmb_StockOwner.Location = New System.Drawing.Point(121, 19)
        Me.Cmb_StockOwner.Name = "Cmb_StockOwner"
        Me.Cmb_StockOwner.Size = New System.Drawing.Size(173, 23)
        Me.Cmb_StockOwner.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Stock Owner"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Dgv_Stock)
        Me.GroupBox3.Controls.Add(Me.Cmb_QualityTo)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Cmb_QualityFrom)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(23, 196)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1154, 399)
        Me.GroupBox3.TabIndex = 42
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Stock"
        '
        'Dgv_Stock
        '
        Me.Dgv_Stock.AllowUserToAddRows = False
        Me.Dgv_Stock.AllowUserToDeleteRows = False
        Me.Dgv_Stock.AllowUserToResizeColumns = False
        Me.Dgv_Stock.AllowUserToResizeRows = False
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Stock.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Dgv_Stock.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Dgv_Stock.BackgroundColor = System.Drawing.Color.White
        Me.Dgv_Stock.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Stock.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Dgv_Stock.ColumnHeadersHeight = 35
        Me.Dgv_Stock.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.kode_barang, Me.stock_owner, Me.barang_sn, Me.nama, Me.position, Me.good_stock, Me.warning_stock, Me.bad_stock, Me.satuan_display, Me.satuan_barang, Me.checkbox, Me.jumlah_transfer})
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Stock.DefaultCellStyle = DataGridViewCellStyle13
        Me.Dgv_Stock.Location = New System.Drawing.Point(16, 45)
        Me.Dgv_Stock.MultiSelect = False
        Me.Dgv_Stock.Name = "Dgv_Stock"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Stock.RowHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.Dgv_Stock.RowHeadersWidth = 21
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Stock.RowsDefaultCellStyle = DataGridViewCellStyle15
        Me.Dgv_Stock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Dgv_Stock.Size = New System.Drawing.Size(1123, 348)
        Me.Dgv_Stock.TabIndex = 462
        '
        'kode_barang
        '
        Me.kode_barang.HeaderText = "Kode Barang"
        Me.kode_barang.Name = "kode_barang"
        Me.kode_barang.ReadOnly = True
        Me.kode_barang.Visible = False
        Me.kode_barang.Width = 150
        '
        'stock_owner
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.stock_owner.DefaultCellStyle = DataGridViewCellStyle3
        Me.stock_owner.HeaderText = "Lokasi"
        Me.stock_owner.Name = "stock_owner"
        Me.stock_owner.ReadOnly = True
        Me.stock_owner.Visible = False
        Me.stock_owner.Width = 150
        '
        'barang_sn
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.barang_sn.DefaultCellStyle = DataGridViewCellStyle4
        Me.barang_sn.HeaderText = "Serial Number"
        Me.barang_sn.Name = "barang_sn"
        Me.barang_sn.ReadOnly = True
        Me.barang_sn.Width = 200
        '
        'nama
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.nama.DefaultCellStyle = DataGridViewCellStyle5
        Me.nama.HeaderText = "Nama Barang"
        Me.nama.Name = "nama"
        Me.nama.ReadOnly = True
        Me.nama.Width = 200
        '
        'position
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.position.DefaultCellStyle = DataGridViewCellStyle6
        Me.position.HeaderText = "Posisi"
        Me.position.Name = "position"
        Me.position.ReadOnly = True
        Me.position.Width = 150
        '
        'good_stock
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.good_stock.DefaultCellStyle = DataGridViewCellStyle7
        Me.good_stock.HeaderText = "Good Stock"
        Me.good_stock.Name = "good_stock"
        Me.good_stock.ReadOnly = True
        Me.good_stock.Width = 105
        '
        'warning_stock
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.warning_stock.DefaultCellStyle = DataGridViewCellStyle8
        Me.warning_stock.HeaderText = "Warning Stock"
        Me.warning_stock.Name = "warning_stock"
        Me.warning_stock.ReadOnly = True
        Me.warning_stock.Width = 105
        '
        'bad_stock
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.bad_stock.DefaultCellStyle = DataGridViewCellStyle9
        Me.bad_stock.HeaderText = "Bad Stock"
        Me.bad_stock.Name = "bad_stock"
        Me.bad_stock.ReadOnly = True
        Me.bad_stock.Width = 105
        '
        'satuan_display
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.satuan_display.DefaultCellStyle = DataGridViewCellStyle10
        Me.satuan_display.HeaderText = "Satuan"
        Me.satuan_display.Name = "satuan_display"
        Me.satuan_display.ReadOnly = True
        Me.satuan_display.Width = 70
        '
        'satuan_barang
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.satuan_barang.DefaultCellStyle = DataGridViewCellStyle11
        Me.satuan_barang.HeaderText = "SatuanBarang"
        Me.satuan_barang.Name = "satuan_barang"
        Me.satuan_barang.ReadOnly = True
        Me.satuan_barang.Visible = False
        '
        'checkbox
        '
        Me.checkbox.HeaderText = ""
        Me.checkbox.Name = "checkbox"
        Me.checkbox.Width = 40
        '
        'jumlah_transfer
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.jumlah_transfer.DefaultCellStyle = DataGridViewCellStyle12
        Me.jumlah_transfer.HeaderText = "Jumlah Transfer"
        Me.jumlah_transfer.Name = "jumlah_transfer"
        Me.jumlah_transfer.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.jumlah_transfer.Width = 120
        '
        'Cmb_QualityTo
        '
        Me.Cmb_QualityTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_QualityTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_QualityTo.FormattingEnabled = True
        Me.Cmb_QualityTo.Location = New System.Drawing.Point(333, 16)
        Me.Cmb_QualityTo.Name = "Cmb_QualityTo"
        Me.Cmb_QualityTo.Size = New System.Drawing.Size(173, 23)
        Me.Cmb_QualityTo.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(304, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 15)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "To"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(13, 19)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 15)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Initial Quality"
        '
        'Cmb_QualityFrom
        '
        Me.Cmb_QualityFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_QualityFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_QualityFrom.FormattingEnabled = True
        Me.Cmb_QualityFrom.Location = New System.Drawing.Point(123, 16)
        Me.Cmb_QualityFrom.Name = "Cmb_QualityFrom"
        Me.Cmb_QualityFrom.Size = New System.Drawing.Size(173, 23)
        Me.Cmb_QualityFrom.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(24, 184)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 10)
        Me.Panel4.TabIndex = 40
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1180, 97)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 498)
        Me.Panel5.TabIndex = 39
        Me.Panel5.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(186, 605)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(161, 36)
        Me.Btn_Refresh.TabIndex = 466
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(21, 605)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(161, 36)
        Me.Btn_Simpan.TabIndex = 465
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(25, 643)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(942, 10)
        Me.Panel8.TabIndex = 463
        Me.Panel8.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(23, 595)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 10)
        Me.Panel7.TabIndex = 464
        Me.Panel7.Visible = False
        '
        'ListView1
        '
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(1190, 166)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(800, 197)
        Me.ListView1.TabIndex = 464
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.Visible = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1201, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Transfer_Quality_Stock2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1201, 654)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Transfer_Quality_Stock2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.Dgv_Stock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Cmb_StockOwner As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtKd_Barang As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtSo As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Cmb_QualityTo As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Cmb_QualityFrom As ComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents TxtNamaBarang As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtGoodStock As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn_GetData As Button
    Friend WithEvents Dgv_Stock As DataGridView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents kode_barang As DataGridViewTextBoxColumn
    Friend WithEvents stock_owner As DataGridViewTextBoxColumn
    Friend WithEvents barang_sn As DataGridViewTextBoxColumn
    Friend WithEvents nama As DataGridViewTextBoxColumn
    Friend WithEvents position As DataGridViewTextBoxColumn
    Friend WithEvents good_stock As DataGridViewTextBoxColumn
    Friend WithEvents warning_stock As DataGridViewTextBoxColumn
    Friend WithEvents bad_stock As DataGridViewTextBoxColumn
    Friend WithEvents satuan_display As DataGridViewTextBoxColumn
    Friend WithEvents satuan_barang As DataGridViewTextBoxColumn
    Friend WithEvents checkbox As DataGridViewCheckBoxColumn
    Friend WithEvents jumlah_transfer As DataGridViewTextBoxColumn
End Class
