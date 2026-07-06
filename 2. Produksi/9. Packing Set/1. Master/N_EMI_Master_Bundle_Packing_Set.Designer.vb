<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Master_Bundle_Packing_Set
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PanelJudul = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GB_Barang = New System.Windows.Forms.GroupBox()
        Me.LB_Kode_Barang_Inq = New System.Windows.Forms.Label()
        Me.TB_KodeBarangInq = New System.Windows.Forms.TextBox()
        Me.TB_NamaBarang = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TB_KodeBarang = New System.Windows.Forms.TextBox()
        Me.GB_PackingSet = New System.Windows.Forms.GroupBox()
        Me.LV_BundlePackingSet = New System.Windows.Forms.ListView()
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CMS_BundlePackingSet = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BatalkanPackingDefaultToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GB_Bahan = New System.Windows.Forms.GroupBox()
        Me.CMB_PackingSet = New System.Windows.Forms.ComboBox()
        Me.TB_IdPackingSet = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.BTN_ClearBahan = New System.Windows.Forms.Button()
        Me.BTN_SimpanBahan = New System.Windows.Forms.Button()
        Me.Lbl_KdBrg = New System.Windows.Forms.Label()
        Me.GB_ListBahan = New System.Windows.Forms.GroupBox()
        Me.DGV_ListPackingSet = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CMS_ListPackingSet = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.HapusBahanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GB_Packing = New System.Windows.Forms.GroupBox()
        Me.TB_Deskripsi = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.BTN_Simpan = New System.Windows.Forms.Button()
        Me.BTN_Refresh = New System.Windows.Forms.Button()
        Me.LV_CariBarang = New System.Windows.Forms.ListView()
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PanelJudul.SuspendLayout()
        Me.GB_Barang.SuspendLayout()
        Me.GB_PackingSet.SuspendLayout()
        Me.CMS_BundlePackingSet.SuspendLayout()
        Me.GB_Bahan.SuspendLayout()
        Me.GB_ListBahan.SuspendLayout()
        CType(Me.DGV_ListPackingSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CMS_ListPackingSet.SuspendLayout()
        Me.GB_Packing.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelJudul
        '
        Me.PanelJudul.Controls.Add(Me.PanelGradient1)
        Me.PanelJudul.Controls.Add(Me.Lbl_Judul)
        Me.PanelJudul.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelJudul.Location = New System.Drawing.Point(0, 0)
        Me.PanelJudul.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelJudul.Name = "PanelJudul"
        Me.PanelJudul.Size = New System.Drawing.Size(934, 51)
        Me.PanelJudul.TabIndex = 412
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
        Me.PanelGradient1.Size = New System.Drawing.Size(934, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(346, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master Data Bundle Packing Set"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(-18, 50)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1400, 12)
        Me.Panel2.TabIndex = 414
        Me.Panel2.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 57)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 684)
        Me.Panel5.TabIndex = 415
        Me.Panel5.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(915, 59)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 960)
        Me.Panel3.TabIndex = 418
        Me.Panel3.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Red
        Me.Panel1.Location = New System.Drawing.Point(-145, 599)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1400, 12)
        Me.Panel1.TabIndex = 419
        Me.Panel1.Visible = False
        '
        'GB_Barang
        '
        Me.GB_Barang.Controls.Add(Me.LB_Kode_Barang_Inq)
        Me.GB_Barang.Controls.Add(Me.TB_KodeBarangInq)
        Me.GB_Barang.Controls.Add(Me.TB_NamaBarang)
        Me.GB_Barang.Controls.Add(Me.Label2)
        Me.GB_Barang.Controls.Add(Me.Label3)
        Me.GB_Barang.Controls.Add(Me.TB_KodeBarang)
        Me.GB_Barang.Location = New System.Drawing.Point(19, 69)
        Me.GB_Barang.Name = "GB_Barang"
        Me.GB_Barang.Size = New System.Drawing.Size(895, 53)
        Me.GB_Barang.TabIndex = 422
        Me.GB_Barang.TabStop = False
        Me.GB_Barang.Text = "Barang"
        '
        'LB_Kode_Barang_Inq
        '
        Me.LB_Kode_Barang_Inq.AutoSize = True
        Me.LB_Kode_Barang_Inq.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_Kode_Barang_Inq.Location = New System.Drawing.Point(618, 24)
        Me.LB_Kode_Barang_Inq.Name = "LB_Kode_Barang_Inq"
        Me.LB_Kode_Barang_Inq.Size = New System.Drawing.Size(95, 16)
        Me.LB_Kode_Barang_Inq.TabIndex = 11
        Me.LB_Kode_Barang_Inq.Text = "Kode Barang Inq"
        Me.LB_Kode_Barang_Inq.Visible = False
        '
        'TB_KodeBarangInq
        '
        Me.TB_KodeBarangInq.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TB_KodeBarangInq.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TB_KodeBarangInq.Location = New System.Drawing.Point(716, 18)
        Me.TB_KodeBarangInq.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TB_KodeBarangInq.MaxLength = 40
        Me.TB_KodeBarangInq.Name = "TB_KodeBarangInq"
        Me.TB_KodeBarangInq.Size = New System.Drawing.Size(144, 22)
        Me.TB_KodeBarangInq.TabIndex = 10
        Me.TB_KodeBarangInq.Visible = False
        '
        'TB_NamaBarang
        '
        Me.TB_NamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TB_NamaBarang.Enabled = False
        Me.TB_NamaBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TB_NamaBarang.Location = New System.Drawing.Point(290, 18)
        Me.TB_NamaBarang.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TB_NamaBarang.MaxLength = 60
        Me.TB_NamaBarang.Name = "TB_NamaBarang"
        Me.TB_NamaBarang.ReadOnly = True
        Me.TB_NamaBarang.Size = New System.Drawing.Size(316, 22)
        Me.TB_NamaBarang.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(247, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Nama"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Kode Barang"
        '
        'TB_KodeBarang
        '
        Me.TB_KodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TB_KodeBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TB_KodeBarang.Location = New System.Drawing.Point(90, 18)
        Me.TB_KodeBarang.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TB_KodeBarang.MaxLength = 40
        Me.TB_KodeBarang.Name = "TB_KodeBarang"
        Me.TB_KodeBarang.Size = New System.Drawing.Size(144, 22)
        Me.TB_KodeBarang.TabIndex = 3
        '
        'GB_PackingSet
        '
        Me.GB_PackingSet.Controls.Add(Me.LV_BundlePackingSet)
        Me.GB_PackingSet.Location = New System.Drawing.Point(19, 128)
        Me.GB_PackingSet.Name = "GB_PackingSet"
        Me.GB_PackingSet.Padding = New System.Windows.Forms.Padding(8, 3, 8, 8)
        Me.GB_PackingSet.Size = New System.Drawing.Size(301, 424)
        Me.GB_PackingSet.TabIndex = 423
        Me.GB_PackingSet.TabStop = False
        Me.GB_PackingSet.Text = "Bundle Packing Set"
        '
        'LV_BundlePackingSet
        '
        Me.LV_BundlePackingSet.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader15, Me.ColumnHeader1, Me.ColumnHeader2})
        Me.LV_BundlePackingSet.ContextMenuStrip = Me.CMS_BundlePackingSet
        Me.LV_BundlePackingSet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LV_BundlePackingSet.FullRowSelect = True
        Me.LV_BundlePackingSet.GridLines = True
        Me.LV_BundlePackingSet.HideSelection = False
        Me.LV_BundlePackingSet.Location = New System.Drawing.Point(8, 16)
        Me.LV_BundlePackingSet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LV_BundlePackingSet.Name = "LV_BundlePackingSet"
        Me.LV_BundlePackingSet.Size = New System.Drawing.Size(285, 400)
        Me.LV_BundlePackingSet.TabIndex = 19
        Me.LV_BundlePackingSet.UseCompatibleStateImageBehavior = False
        Me.LV_BundlePackingSet.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.DisplayIndex = 2
        Me.ColumnHeader15.Text = "ID"
        Me.ColumnHeader15.Width = 0
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.DisplayIndex = 0
        Me.ColumnHeader1.Text = "Deskripsi"
        Me.ColumnHeader1.Width = 170
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.DisplayIndex = 1
        Me.ColumnHeader2.Text = "Total Packing Set"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader2.Width = 100
        '
        'CMS_BundlePackingSet
        '
        Me.CMS_BundlePackingSet.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BatalkanPackingDefaultToolStripMenuItem})
        Me.CMS_BundlePackingSet.Name = "CMS_PackingSet"
        Me.CMS_BundlePackingSet.Size = New System.Drawing.Size(180, 26)
        '
        'BatalkanPackingDefaultToolStripMenuItem
        '
        Me.BatalkanPackingDefaultToolStripMenuItem.Name = "BatalkanPackingDefaultToolStripMenuItem"
        Me.BatalkanPackingDefaultToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.BatalkanPackingDefaultToolStripMenuItem.Text = "Nonaktifkan Bundle"
        '
        'GB_Bahan
        '
        Me.GB_Bahan.Controls.Add(Me.CMB_PackingSet)
        Me.GB_Bahan.Controls.Add(Me.TB_IdPackingSet)
        Me.GB_Bahan.Controls.Add(Me.Label5)
        Me.GB_Bahan.Controls.Add(Me.BTN_ClearBahan)
        Me.GB_Bahan.Controls.Add(Me.BTN_SimpanBahan)
        Me.GB_Bahan.Controls.Add(Me.Lbl_KdBrg)
        Me.GB_Bahan.Location = New System.Drawing.Point(326, 128)
        Me.GB_Bahan.Name = "GB_Bahan"
        Me.GB_Bahan.Size = New System.Drawing.Size(588, 97)
        Me.GB_Bahan.TabIndex = 424
        Me.GB_Bahan.TabStop = False
        Me.GB_Bahan.Text = "Packing Set"
        '
        'CMB_PackingSet
        '
        Me.CMB_PackingSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMB_PackingSet.FormattingEnabled = True
        Me.CMB_PackingSet.Location = New System.Drawing.Point(84, 21)
        Me.CMB_PackingSet.Name = "CMB_PackingSet"
        Me.CMB_PackingSet.Size = New System.Drawing.Size(144, 21)
        Me.CMB_PackingSet.TabIndex = 430
        '
        'TB_IdPackingSet
        '
        Me.TB_IdPackingSet.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TB_IdPackingSet.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TB_IdPackingSet.Location = New System.Drawing.Point(275, 20)
        Me.TB_IdPackingSet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TB_IdPackingSet.MaxLength = 60
        Me.TB_IdPackingSet.Name = "TB_IdPackingSet"
        Me.TB_IdPackingSet.Size = New System.Drawing.Size(66, 22)
        Me.TB_IdPackingSet.TabIndex = 425
        Me.TB_IdPackingSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TB_IdPackingSet.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(251, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(18, 16)
        Me.Label5.TabIndex = 424
        Me.Label5.Text = "ID"
        Me.Label5.Visible = False
        '
        'BTN_ClearBahan
        '
        Me.BTN_ClearBahan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_ClearBahan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BTN_ClearBahan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BTN_ClearBahan.ForeColor = System.Drawing.Color.White
        Me.BTN_ClearBahan.Location = New System.Drawing.Point(233, 56)
        Me.BTN_ClearBahan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BTN_ClearBahan.Name = "BTN_ClearBahan"
        Me.BTN_ClearBahan.Size = New System.Drawing.Size(142, 33)
        Me.BTN_ClearBahan.TabIndex = 423
        Me.BTN_ClearBahan.Text = "&Clear"
        Me.BTN_ClearBahan.UseVisualStyleBackColor = False
        '
        'BTN_SimpanBahan
        '
        Me.BTN_SimpanBahan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_SimpanBahan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BTN_SimpanBahan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BTN_SimpanBahan.ForeColor = System.Drawing.Color.White
        Me.BTN_SimpanBahan.Location = New System.Drawing.Point(83, 56)
        Me.BTN_SimpanBahan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BTN_SimpanBahan.Name = "BTN_SimpanBahan"
        Me.BTN_SimpanBahan.Size = New System.Drawing.Size(145, 33)
        Me.BTN_SimpanBahan.TabIndex = 422
        Me.BTN_SimpanBahan.Text = "&Simpan"
        Me.BTN_SimpanBahan.UseVisualStyleBackColor = False
        '
        'Lbl_KdBrg
        '
        Me.Lbl_KdBrg.AutoSize = True
        Me.Lbl_KdBrg.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_KdBrg.Location = New System.Drawing.Point(6, 26)
        Me.Lbl_KdBrg.Name = "Lbl_KdBrg"
        Me.Lbl_KdBrg.Size = New System.Drawing.Size(70, 16)
        Me.Lbl_KdBrg.TabIndex = 5
        Me.Lbl_KdBrg.Text = "Packing Set"
        '
        'GB_ListBahan
        '
        Me.GB_ListBahan.Controls.Add(Me.DGV_ListPackingSet)
        Me.GB_ListBahan.Location = New System.Drawing.Point(326, 231)
        Me.GB_ListBahan.Name = "GB_ListBahan"
        Me.GB_ListBahan.Padding = New System.Windows.Forms.Padding(8, 3, 8, 8)
        Me.GB_ListBahan.Size = New System.Drawing.Size(588, 265)
        Me.GB_ListBahan.TabIndex = 425
        Me.GB_ListBahan.TabStop = False
        Me.GB_ListBahan.Text = "List Packing Set"
        '
        'DGV_ListPackingSet
        '
        Me.DGV_ListPackingSet.AllowUserToAddRows = False
        Me.DGV_ListPackingSet.AllowUserToResizeColumns = False
        Me.DGV_ListPackingSet.AllowUserToResizeRows = False
        Me.DGV_ListPackingSet.BackgroundColor = System.Drawing.Color.White
        Me.DGV_ListPackingSet.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_ListPackingSet.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV_ListPackingSet.ColumnHeadersHeight = 30
        Me.DGV_ListPackingSet.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
        Me.DGV_ListPackingSet.ContextMenuStrip = Me.CMS_ListPackingSet
        Me.DGV_ListPackingSet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_ListPackingSet.Location = New System.Drawing.Point(8, 16)
        Me.DGV_ListPackingSet.Name = "DGV_ListPackingSet"
        Me.DGV_ListPackingSet.RowHeadersWidth = 21
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGV_ListPackingSet.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGV_ListPackingSet.RowTemplate.Height = 30
        Me.DGV_ListPackingSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGV_ListPackingSet.Size = New System.Drawing.Size(572, 241)
        Me.DGV_ListPackingSet.TabIndex = 19
        '
        'Column1
        '
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column2.HeaderText = "Packing Set"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Jenis"
        Me.Column3.MinimumWidth = 150
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 150
        '
        'Column4
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle2.Format = "N2"
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column4.HeaderText = "Persentase"
        Me.Column4.MinimumWidth = 100
        Me.Column4.Name = "Column4"
        '
        'Column5
        '
        Me.Column5.HeaderText = "Kode Bahan"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Visible = False
        '
        'Column6
        '
        Me.Column6.HeaderText = "ID_Parent"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Visible = False
        '
        'CMS_ListPackingSet
        '
        Me.CMS_ListPackingSet.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HapusBahanToolStripMenuItem})
        Me.CMS_ListPackingSet.Name = "CMS_ListBahan"
        Me.CMS_ListPackingSet.Size = New System.Drawing.Size(173, 26)
        '
        'HapusBahanToolStripMenuItem
        '
        Me.HapusBahanToolStripMenuItem.Name = "HapusBahanToolStripMenuItem"
        Me.HapusBahanToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.HapusBahanToolStripMenuItem.Text = "Hapus Packing Set"
        '
        'GB_Packing
        '
        Me.GB_Packing.Controls.Add(Me.TB_Deskripsi)
        Me.GB_Packing.Controls.Add(Me.Label4)
        Me.GB_Packing.Location = New System.Drawing.Point(326, 502)
        Me.GB_Packing.Name = "GB_Packing"
        Me.GB_Packing.Size = New System.Drawing.Size(588, 50)
        Me.GB_Packing.TabIndex = 425
        Me.GB_Packing.TabStop = False
        Me.GB_Packing.Text = "Informasi Bundle"
        '
        'TB_Deskripsi
        '
        Me.TB_Deskripsi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TB_Deskripsi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TB_Deskripsi.Location = New System.Drawing.Point(68, 17)
        Me.TB_Deskripsi.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TB_Deskripsi.MaxLength = 40
        Me.TB_Deskripsi.Name = "TB_Deskripsi"
        Me.TB_Deskripsi.Size = New System.Drawing.Size(236, 22)
        Me.TB_Deskripsi.TabIndex = 408
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 409
        Me.Label4.Text = "Deskripsi"
        '
        'BTN_Simpan
        '
        Me.BTN_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BTN_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BTN_Simpan.ForeColor = System.Drawing.Color.White
        Me.BTN_Simpan.Location = New System.Drawing.Point(19, 566)
        Me.BTN_Simpan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BTN_Simpan.Name = "BTN_Simpan"
        Me.BTN_Simpan.Size = New System.Drawing.Size(132, 33)
        Me.BTN_Simpan.TabIndex = 426
        Me.BTN_Simpan.Text = "&Simpan"
        Me.BTN_Simpan.UseVisualStyleBackColor = False
        '
        'BTN_Refresh
        '
        Me.BTN_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BTN_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BTN_Refresh.ForeColor = System.Drawing.Color.White
        Me.BTN_Refresh.Location = New System.Drawing.Point(157, 565)
        Me.BTN_Refresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BTN_Refresh.Name = "BTN_Refresh"
        Me.BTN_Refresh.Size = New System.Drawing.Size(132, 33)
        Me.BTN_Refresh.TabIndex = 427
        Me.BTN_Refresh.Text = "&Refresh"
        Me.BTN_Refresh.UseVisualStyleBackColor = False
        '
        'LV_CariBarang
        '
        Me.LV_CariBarang.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader9, Me.ColumnHeader6, Me.ColumnHeader10, Me.ColumnHeader11})
        Me.LV_CariBarang.FullRowSelect = True
        Me.LV_CariBarang.GridLines = True
        Me.LV_CariBarang.HideSelection = False
        Me.LV_CariBarang.Location = New System.Drawing.Point(950, 0)
        Me.LV_CariBarang.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LV_CariBarang.Name = "LV_CariBarang"
        Me.LV_CariBarang.Size = New System.Drawing.Size(520, 200)
        Me.LV_CariBarang.TabIndex = 428
        Me.LV_CariBarang.UseCompatibleStateImageBehavior = False
        Me.LV_CariBarang.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Kode Barang"
        Me.ColumnHeader9.Width = 100
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Kode Barang Inq"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Nama"
        Me.ColumnHeader10.Width = 260
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Satuan"
        '
        'N_EMI_Master_Bundle_Packing_Set
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(934, 611)
        Me.Controls.Add(Me.LV_CariBarang)
        Me.Controls.Add(Me.BTN_Refresh)
        Me.Controls.Add(Me.BTN_Simpan)
        Me.Controls.Add(Me.GB_Packing)
        Me.Controls.Add(Me.GB_ListBahan)
        Me.Controls.Add(Me.GB_Bahan)
        Me.Controls.Add(Me.GB_PackingSet)
        Me.Controls.Add(Me.GB_Barang)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.PanelJudul)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "N_EMI_Master_Bundle_Packing_Set"
        Me.PanelJudul.ResumeLayout(False)
        Me.PanelJudul.PerformLayout()
        Me.GB_Barang.ResumeLayout(False)
        Me.GB_Barang.PerformLayout()
        Me.GB_PackingSet.ResumeLayout(False)
        Me.CMS_BundlePackingSet.ResumeLayout(False)
        Me.GB_Bahan.ResumeLayout(False)
        Me.GB_Bahan.PerformLayout()
        Me.GB_ListBahan.ResumeLayout(False)
        CType(Me.DGV_ListPackingSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CMS_ListPackingSet.ResumeLayout(False)
        Me.GB_Packing.ResumeLayout(False)
        Me.GB_Packing.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelJudul As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GB_Barang As GroupBox
    Friend WithEvents TB_NamaBarang As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TB_KodeBarang As TextBox
    Friend WithEvents GB_PackingSet As GroupBox
    Friend WithEvents LV_BundlePackingSet As ListView
    Friend WithEvents ColumnHeader15 As ColumnHeader
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents GB_Bahan As GroupBox
    Friend WithEvents BTN_ClearBahan As Button
    Friend WithEvents BTN_SimpanBahan As Button
    Friend WithEvents Lbl_KdBrg As Label
    Friend WithEvents GB_ListBahan As GroupBox
    Friend WithEvents GB_Packing As GroupBox
    Friend WithEvents TB_Deskripsi As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents BTN_Simpan As Button
    Friend WithEvents BTN_Refresh As Button
    Friend WithEvents LV_CariBarang As ListView
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents ColumnHeader10 As ColumnHeader
    Friend WithEvents ColumnHeader11 As ColumnHeader
    Friend WithEvents LB_Kode_Barang_Inq As Label
    Friend WithEvents TB_KodeBarangInq As TextBox
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents CMS_ListPackingSet As ContextMenuStrip
    Friend WithEvents HapusBahanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CMS_BundlePackingSet As ContextMenuStrip
    Friend WithEvents BatalkanPackingDefaultToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DGV_ListPackingSet As DataGridView
    Friend WithEvents TB_IdPackingSet As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents CMB_PackingSet As ComboBox
End Class
