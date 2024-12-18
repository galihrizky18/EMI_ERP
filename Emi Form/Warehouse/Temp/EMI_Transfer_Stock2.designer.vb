<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Transfer_Stock2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.TxtNo_Transaksi = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtKd_Barang = New System.Windows.Forms.TextBox()
        Me.TxtNm_Barang = New System.Windows.Forms.TextBox()
        Me.TxtJumlah = New System.Windows.Forms.TextBox()
        Me.TxtKeterangan = New System.Windows.Forms.TextBox()
        Me.CmbSO_Asal = New System.Windows.Forms.ComboBox()
        Me.CmbSo_Tujuan = New System.Windows.Forms.ComboBox()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.Lbl_Supplier = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbJnsTransfer = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DGV_Data_TF = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.DGV_Data_TF, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(948, 51)
        Me.Panel1.TabIndex = 23
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
        Me.PanelGradient1.Size = New System.Drawing.Size(948, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(167, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Transfer Stock"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 498)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(9, 227)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(924, 12)
        Me.Panel6.TabIndex = 232
        Me.Panel6.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(110, 715)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 235
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(20, 715)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 233
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(14, 696)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(938, 19)
        Me.Panel7.TabIndex = 236
        Me.Panel7.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(669, 228)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 515)
        Me.Panel5.TabIndex = 344
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(-237, 751)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 345
        Me.Panel4.Visible = False
        '
        'TxtNo_Transaksi
        '
        Me.TxtNo_Transaksi.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtNo_Transaksi.Enabled = False
        Me.TxtNo_Transaksi.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNo_Transaksi.Location = New System.Drawing.Point(20, 63)
        Me.TxtNo_Transaksi.MaxLength = 50
        Me.TxtNo_Transaksi.Name = "TxtNo_Transaksi"
        Me.TxtNo_Transaksi.Size = New System.Drawing.Size(210, 21)
        Me.TxtNo_Transaksi.TabIndex = 347
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(25, 242)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(183, 21)
        Me.Label6.TabIndex = 349
        Me.Label6.Text = "Kode Barang"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(210, 242)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(448, 21)
        Me.Label7.TabIndex = 350
        Me.Label7.Text = "Nama Barang"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtKd_Barang
        '
        Me.TxtKd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKd_Barang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKd_Barang.Location = New System.Drawing.Point(25, 268)
        Me.TxtKd_Barang.MaxLength = 50
        Me.TxtKd_Barang.Name = "TxtKd_Barang"
        Me.TxtKd_Barang.Size = New System.Drawing.Size(183, 22)
        Me.TxtKd_Barang.TabIndex = 352
        '
        'TxtNm_Barang
        '
        Me.TxtNm_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNm_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNm_Barang.Enabled = False
        Me.TxtNm_Barang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtNm_Barang.Location = New System.Drawing.Point(210, 268)
        Me.TxtNm_Barang.MaxLength = 50
        Me.TxtNm_Barang.Name = "TxtNm_Barang"
        Me.TxtNm_Barang.Size = New System.Drawing.Size(448, 22)
        Me.TxtNm_Barang.TabIndex = 353
        '
        'TxtJumlah
        '
        Me.TxtJumlah.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJumlah.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtJumlah.Location = New System.Drawing.Point(519, 715)
        Me.TxtJumlah.MaxLength = 50
        Me.TxtJumlah.Name = "TxtJumlah"
        Me.TxtJumlah.Size = New System.Drawing.Size(150, 22)
        Me.TxtJumlah.TabIndex = 355
        '
        'TxtKeterangan
        '
        Me.TxtKeterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKeterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKeterangan.Location = New System.Drawing.Point(149, 106)
        Me.TxtKeterangan.MaxLength = 50
        Me.TxtKeterangan.Name = "TxtKeterangan"
        Me.TxtKeterangan.Size = New System.Drawing.Size(340, 22)
        Me.TxtKeterangan.TabIndex = 361
        '
        'CmbSO_Asal
        '
        Me.CmbSO_Asal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSO_Asal.FormattingEnabled = True
        Me.CmbSO_Asal.Location = New System.Drawing.Point(149, 161)
        Me.CmbSO_Asal.Name = "CmbSO_Asal"
        Me.CmbSO_Asal.Size = New System.Drawing.Size(163, 21)
        Me.CmbSO_Asal.TabIndex = 392
        '
        'CmbSo_Tujuan
        '
        Me.CmbSo_Tujuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSo_Tujuan.FormattingEnabled = True
        Me.CmbSo_Tujuan.Location = New System.Drawing.Point(149, 188)
        Me.CmbSo_Tujuan.Name = "CmbSo_Tujuan"
        Me.CmbSo_Tujuan.Size = New System.Drawing.Size(163, 21)
        Me.CmbSo_Tujuan.TabIndex = 393
        '
        'ListView2
        '
        Me.ListView2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(695, 322)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(633, 146)
        Me.ListView2.TabIndex = 394
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'Lbl_Supplier
        '
        Me.Lbl_Supplier.AutoSize = True
        Me.Lbl_Supplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Lbl_Supplier.Location = New System.Drawing.Point(22, 108)
        Me.Lbl_Supplier.Name = "Lbl_Supplier"
        Me.Lbl_Supplier.Size = New System.Drawing.Size(82, 17)
        Me.Lbl_Supplier.TabIndex = 422
        Me.Lbl_Supplier.Text = "Keterangan"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(22, 135)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 17)
        Me.Label1.TabIndex = 423
        Me.Label1.Text = "Jenis Transfer"
        '
        'CmbJnsTransfer
        '
        Me.CmbJnsTransfer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJnsTransfer.FormattingEnabled = True
        Me.CmbJnsTransfer.Location = New System.Drawing.Point(149, 134)
        Me.CmbJnsTransfer.Name = "CmbJnsTransfer"
        Me.CmbJnsTransfer.Size = New System.Drawing.Size(340, 21)
        Me.CmbJnsTransfer.TabIndex = 424
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(22, 162)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 17)
        Me.Label3.TabIndex = 425
        Me.Label3.Text = "Lokasi Awal"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(22, 189)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 17)
        Me.Label4.TabIndex = 426
        Me.Label4.Text = "Lokasi Tujuan"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(473, 717)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 17)
        Me.Label5.TabIndex = 427
        Me.Label5.Text = "Total"
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
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Work Sans", 8.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Data_TF.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGV_Data_TF.ColumnHeadersHeight = 45
        Me.DGV_Data_TF.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Work Sans", 8.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Data_TF.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGV_Data_TF.Location = New System.Drawing.Point(25, 296)
        Me.DGV_Data_TF.MultiSelect = False
        Me.DGV_Data_TF.Name = "DGV_Data_TF"
        Me.DGV_Data_TF.RowHeadersWidth = 21
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Data_TF.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DGV_Data_TF.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGV_Data_TF.Size = New System.Drawing.Size(633, 393)
        Me.DGV_Data_TF.TabIndex = 460
        '
        'Column1
        '
        Me.Column1.HeaderText = "Lokasi"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "kode Barang"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Nama"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "ID Warehouse"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Kode Rak"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "ID Pallet"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "Rak Tujuan"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Column8
        '
        Me.Column8.HeaderText = "ID Warehouse Tujuan"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Column9
        '
        Me.Column9.HeaderText = ""
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'EMI_Transfer_Stock2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(948, 766)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.DGV_Data_TF)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CmbJnsTransfer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Lbl_Supplier)
        Me.Controls.Add(Me.CmbSo_Tujuan)
        Me.Controls.Add(Me.CmbSO_Asal)
        Me.Controls.Add(Me.TxtKeterangan)
        Me.Controls.Add(Me.TxtJumlah)
        Me.Controls.Add(Me.TxtNm_Barang)
        Me.Controls.Add(Me.TxtKd_Barang)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtNo_Transaksi)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "EMI_Transfer_Stock2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DGV_Data_TF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents TxtNo_Transaksi As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtKd_Barang As TextBox
    Friend WithEvents TxtNm_Barang As TextBox
    Friend WithEvents TxtJumlah As TextBox
    Friend WithEvents TxtKeterangan As TextBox
    Friend WithEvents CmbSO_Asal As ComboBox
    Friend WithEvents CmbSo_Tujuan As ComboBox
    Friend WithEvents ListView2 As ListView
    Friend WithEvents Lbl_Supplier As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents CmbJnsTransfer As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents DGV_Data_TF As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewComboBoxColumn
    Friend WithEvents Column8 As DataGridViewComboBoxColumn
    Friend WithEvents Column9 As DataGridViewCheckBoxColumn
End Class
