<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Transfer_Stock_4
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
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbJnsTransfer = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Lbl_Supplier = New System.Windows.Forms.Label()
        Me.CmbSo_Tujuan = New System.Windows.Forms.ComboBox()
        Me.CmbSO_Asal = New System.Windows.Forms.ComboBox()
        Me.TxtKeterangan = New System.Windows.Forms.TextBox()
        Me.TxtNo_Transaksi = New System.Windows.Forms.TextBox()
        Me.TxtNm_Barang = New System.Windows.Forms.TextBox()
        Me.TxtKd_Barang = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DGV_Data_TF = New System.Windows.Forms.DataGridView()
        Me.lokasi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kode_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.barang_sn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nama_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_warehouse = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kode_rak = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_pallet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.good_stock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Chk_TF = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.jumlah_transfer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rak_tujuan = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.id_warehouse_tujuan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtTotalTransfer = New System.Windows.Forms.TextBox()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Lv_DetBarang = New System.Windows.Forms.ListView()
        Me.Btn_GetData = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_SO = New System.Windows.Forms.TextBox()
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
        Me.Panel1.Size = New System.Drawing.Size(812, 51)
        Me.Panel1.TabIndex = 24
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
        Me.PanelGradient1.Size = New System.Drawing.Size(812, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(166, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Transfer Stock"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Location = New System.Drawing.Point(0, 53)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 498)
        Me.Panel3.TabIndex = 37
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
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(20, 52)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 38
        Me.Panel2.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(20, 172)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 17)
        Me.Label4.TabIndex = 435
        Me.Label4.Text = "Lokasi Tujuan"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(20, 145)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 17)
        Me.Label3.TabIndex = 434
        Me.Label3.Text = "Lokasi Awal"
        '
        'CmbJnsTransfer
        '
        Me.CmbJnsTransfer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJnsTransfer.FormattingEnabled = True
        Me.CmbJnsTransfer.Location = New System.Drawing.Point(147, 117)
        Me.CmbJnsTransfer.Name = "CmbJnsTransfer"
        Me.CmbJnsTransfer.Size = New System.Drawing.Size(340, 21)
        Me.CmbJnsTransfer.TabIndex = 433
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(20, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 17)
        Me.Label1.TabIndex = 432
        Me.Label1.Text = "Jenis Transfer"
        '
        'Lbl_Supplier
        '
        Me.Lbl_Supplier.AutoSize = True
        Me.Lbl_Supplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Lbl_Supplier.Location = New System.Drawing.Point(20, 91)
        Me.Lbl_Supplier.Name = "Lbl_Supplier"
        Me.Lbl_Supplier.Size = New System.Drawing.Size(82, 17)
        Me.Lbl_Supplier.TabIndex = 431
        Me.Lbl_Supplier.Text = "Keterangan"
        '
        'CmbSo_Tujuan
        '
        Me.CmbSo_Tujuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSo_Tujuan.Enabled = False
        Me.CmbSo_Tujuan.FormattingEnabled = True
        Me.CmbSo_Tujuan.Location = New System.Drawing.Point(147, 171)
        Me.CmbSo_Tujuan.Name = "CmbSo_Tujuan"
        Me.CmbSo_Tujuan.Size = New System.Drawing.Size(163, 21)
        Me.CmbSo_Tujuan.TabIndex = 430
        '
        'CmbSO_Asal
        '
        Me.CmbSO_Asal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSO_Asal.Enabled = False
        Me.CmbSO_Asal.FormattingEnabled = True
        Me.CmbSO_Asal.Location = New System.Drawing.Point(147, 144)
        Me.CmbSO_Asal.Name = "CmbSO_Asal"
        Me.CmbSO_Asal.Size = New System.Drawing.Size(163, 21)
        Me.CmbSO_Asal.TabIndex = 429
        '
        'TxtKeterangan
        '
        Me.TxtKeterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKeterangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtKeterangan.Location = New System.Drawing.Point(147, 89)
        Me.TxtKeterangan.MaxLength = 50
        Me.TxtKeterangan.Name = "TxtKeterangan"
        Me.TxtKeterangan.Size = New System.Drawing.Size(340, 21)
        Me.TxtKeterangan.TabIndex = 428
        '
        'TxtNo_Transaksi
        '
        Me.TxtNo_Transaksi.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtNo_Transaksi.Enabled = False
        Me.TxtNo_Transaksi.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNo_Transaksi.Location = New System.Drawing.Point(20, 62)
        Me.TxtNo_Transaksi.MaxLength = 50
        Me.TxtNo_Transaksi.Name = "TxtNo_Transaksi"
        Me.TxtNo_Transaksi.Size = New System.Drawing.Size(210, 21)
        Me.TxtNo_Transaksi.TabIndex = 427
        '
        'TxtNm_Barang
        '
        Me.TxtNm_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNm_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNm_Barang.Enabled = False
        Me.TxtNm_Barang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtNm_Barang.Location = New System.Drawing.Point(342, 232)
        Me.TxtNm_Barang.MaxLength = 50
        Me.TxtNm_Barang.Name = "TxtNm_Barang"
        Me.TxtNm_Barang.ReadOnly = True
        Me.TxtNm_Barang.Size = New System.Drawing.Size(389, 21)
        Me.TxtNm_Barang.TabIndex = 439
        '
        'TxtKd_Barang
        '
        Me.TxtKd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKd_Barang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtKd_Barang.Location = New System.Drawing.Point(22, 232)
        Me.TxtKd_Barang.MaxLength = 50
        Me.TxtKd_Barang.Name = "TxtKd_Barang"
        Me.TxtKd_Barang.Size = New System.Drawing.Size(126, 21)
        Me.TxtKd_Barang.TabIndex = 438
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(22, 206)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(126, 21)
        Me.Label6.TabIndex = 436
        Me.Label6.Text = "Kode Barang"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(342, 206)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(389, 21)
        Me.Label7.TabIndex = 437
        Me.Label7.Text = "Nama Barang"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.DGV_Data_TF.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.lokasi, Me.kode_barang, Me.barang_sn, Me.nama_barang, Me.id_warehouse, Me.kode_rak, Me.id_pallet, Me.good_stock, Me.satuan, Me.Chk_TF, Me.jumlah_transfer, Me.rak_tujuan, Me.id_warehouse_tujuan})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Data_TF.DefaultCellStyle = DataGridViewCellStyle12
        Me.DGV_Data_TF.Location = New System.Drawing.Point(21, 259)
        Me.DGV_Data_TF.MultiSelect = False
        Me.DGV_Data_TF.Name = "DGV_Data_TF"
        Me.DGV_Data_TF.RowHeadersWidth = 21
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Data_TF.RowsDefaultCellStyle = DataGridViewCellStyle13
        Me.DGV_Data_TF.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGV_Data_TF.Size = New System.Drawing.Size(772, 375)
        Me.DGV_Data_TF.TabIndex = 461
        '
        'lokasi
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.lokasi.DefaultCellStyle = DataGridViewCellStyle3
        Me.lokasi.HeaderText = "Lokasi"
        Me.lokasi.Name = "lokasi"
        Me.lokasi.ReadOnly = True
        Me.lokasi.Width = 150
        '
        'kode_barang
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.kode_barang.DefaultCellStyle = DataGridViewCellStyle4
        Me.kode_barang.HeaderText = "kode Barang"
        Me.kode_barang.Name = "kode_barang"
        Me.kode_barang.ReadOnly = True
        Me.kode_barang.Width = 150
        '
        'barang_sn
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.barang_sn.DefaultCellStyle = DataGridViewCellStyle5
        Me.barang_sn.HeaderText = "Serial Number"
        Me.barang_sn.Name = "barang_sn"
        Me.barang_sn.Width = 250
        '
        'nama_barang
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.nama_barang.DefaultCellStyle = DataGridViewCellStyle6
        Me.nama_barang.HeaderText = "Nama"
        Me.nama_barang.Name = "nama_barang"
        Me.nama_barang.ReadOnly = True
        Me.nama_barang.Width = 150
        '
        'id_warehouse
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.id_warehouse.DefaultCellStyle = DataGridViewCellStyle7
        Me.id_warehouse.HeaderText = "ID Warehouse"
        Me.id_warehouse.Name = "id_warehouse"
        Me.id_warehouse.ReadOnly = True
        Me.id_warehouse.Width = 150
        '
        'kode_rak
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.kode_rak.DefaultCellStyle = DataGridViewCellStyle8
        Me.kode_rak.HeaderText = "Kode Rak"
        Me.kode_rak.Name = "kode_rak"
        Me.kode_rak.ReadOnly = True
        Me.kode_rak.Width = 150
        '
        'id_pallet
        '
        Me.id_pallet.HeaderText = "ID Pallet"
        Me.id_pallet.Name = "id_pallet"
        Me.id_pallet.ReadOnly = True
        Me.id_pallet.Width = 150
        '
        'good_stock
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.good_stock.DefaultCellStyle = DataGridViewCellStyle9
        Me.good_stock.HeaderText = "Good Stock"
        Me.good_stock.Name = "good_stock"
        '
        'satuan
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.satuan.DefaultCellStyle = DataGridViewCellStyle10
        Me.satuan.HeaderText = "Satuan"
        Me.satuan.Name = "satuan"
        Me.satuan.Width = 60
        '
        'Chk_TF
        '
        Me.Chk_TF.HeaderText = ""
        Me.Chk_TF.Name = "Chk_TF"
        Me.Chk_TF.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Chk_TF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Chk_TF.Width = 30
        '
        'jumlah_transfer
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.jumlah_transfer.DefaultCellStyle = DataGridViewCellStyle11
        Me.jumlah_transfer.HeaderText = "Jumlah"
        Me.jumlah_transfer.Name = "jumlah_transfer"
        '
        'rak_tujuan
        '
        Me.rak_tujuan.HeaderText = "Rak Tujuan"
        Me.rak_tujuan.Name = "rak_tujuan"
        Me.rak_tujuan.Width = 150
        '
        'id_warehouse_tujuan
        '
        Me.id_warehouse_tujuan.HeaderText = "ID Warehose Tujuan"
        Me.id_warehouse_tujuan.Name = "id_warehouse_tujuan"
        Me.id_warehouse_tujuan.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Location = New System.Drawing.Point(795, 72)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 498)
        Me.Panel4.TabIndex = 37
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
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(20, 638)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 12)
        Me.Panel7.TabIndex = 38
        Me.Panel7.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(597, 661)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 17)
        Me.Label5.TabIndex = 465
        Me.Label5.Text = "Total"
        '
        'TxtTotalTransfer
        '
        Me.TxtTotalTransfer.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtTotalTransfer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTotalTransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtTotalTransfer.Location = New System.Drawing.Point(643, 659)
        Me.TxtTotalTransfer.MaxLength = 50
        Me.TxtTotalTransfer.Name = "TxtTotalTransfer"
        Me.TxtTotalTransfer.Size = New System.Drawing.Size(150, 21)
        Me.TxtTotalTransfer.TabIndex = 464
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(110, 651)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 463
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(20, 651)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 462
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(20, 688)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(942, 12)
        Me.Panel8.TabIndex = 38
        Me.Panel8.Visible = False
        '
        'Lv_DetBarang
        '
        Me.Lv_DetBarang.FullRowSelect = True
        Me.Lv_DetBarang.HideSelection = False
        Me.Lv_DetBarang.Location = New System.Drawing.Point(803, 258)
        Me.Lv_DetBarang.Name = "Lv_DetBarang"
        Me.Lv_DetBarang.Size = New System.Drawing.Size(635, 250)
        Me.Lv_DetBarang.TabIndex = 466
        Me.Lv_DetBarang.UseCompatibleStateImageBehavior = False
        Me.Lv_DetBarang.Visible = False
        '
        'Btn_GetData
        '
        Me.Btn_GetData.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_GetData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_GetData.ForeColor = System.Drawing.Color.White
        Me.Btn_GetData.Location = New System.Drawing.Point(738, 203)
        Me.Btn_GetData.Name = "Btn_GetData"
        Me.Btn_GetData.Size = New System.Drawing.Size(56, 51)
        Me.Btn_GetData.TabIndex = 462
        Me.Btn_GetData.Text = "&Get"
        Me.Btn_GetData.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(153, 206)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(183, 21)
        Me.Label2.TabIndex = 436
        Me.Label2.Text = "Stock Owner"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Txt_SO
        '
        Me.Txt_SO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SO.Enabled = False
        Me.Txt_SO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_SO.Location = New System.Drawing.Point(153, 232)
        Me.Txt_SO.MaxLength = 50
        Me.Txt_SO.Name = "Txt_SO"
        Me.Txt_SO.ReadOnly = True
        Me.Txt_SO.Size = New System.Drawing.Size(183, 21)
        Me.Txt_SO.TabIndex = 438
        '
        'Transfer_Stock_3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(812, 700)
        Me.Controls.Add(Me.Lv_DetBarang)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtTotalTransfer)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_GetData)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.DGV_Data_TF)
        Me.Controls.Add(Me.TxtNm_Barang)
        Me.Controls.Add(Me.Txt_SO)
        Me.Controls.Add(Me.TxtKd_Barang)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CmbJnsTransfer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Lbl_Supplier)
        Me.Controls.Add(Me.CmbSo_Tujuan)
        Me.Controls.Add(Me.CmbSO_Asal)
        Me.Controls.Add(Me.TxtKeterangan)
        Me.Controls.Add(Me.TxtNo_Transaksi)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Transfer_Stock_3"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer_Stock_3"
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
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents CmbJnsTransfer As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Lbl_Supplier As Label
    Friend WithEvents CmbSo_Tujuan As ComboBox
    Friend WithEvents CmbSO_Asal As ComboBox
    Friend WithEvents TxtKeterangan As TextBox
    Friend WithEvents TxtNo_Transaksi As TextBox
    Friend WithEvents TxtNm_Barang As TextBox
    Friend WithEvents TxtKd_Barang As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents DGV_Data_TF As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtTotalTransfer As TextBox
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Lv_DetBarang As ListView
    Friend WithEvents Btn_GetData As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_SO As TextBox
    Friend WithEvents lokasi As DataGridViewTextBoxColumn
    Friend WithEvents kode_barang As DataGridViewTextBoxColumn
    Friend WithEvents barang_sn As DataGridViewTextBoxColumn
    Friend WithEvents nama_barang As DataGridViewTextBoxColumn
    Friend WithEvents id_warehouse As DataGridViewTextBoxColumn
    Friend WithEvents kode_rak As DataGridViewTextBoxColumn
    Friend WithEvents id_pallet As DataGridViewTextBoxColumn
    Friend WithEvents good_stock As DataGridViewTextBoxColumn
    Friend WithEvents satuan As DataGridViewTextBoxColumn
    Friend WithEvents Chk_TF As DataGridViewCheckBoxColumn
    Friend WithEvents jumlah_transfer As DataGridViewTextBoxColumn
    Friend WithEvents rak_tujuan As DataGridViewComboBoxColumn
    Friend WithEvents id_warehouse_tujuan As DataGridViewTextBoxColumn
End Class
