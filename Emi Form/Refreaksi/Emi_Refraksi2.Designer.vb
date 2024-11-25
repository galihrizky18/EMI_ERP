<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Emi_Refraksi2
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Dgv_DataDetailPO = New System.Windows.Forms.DataGridView()
        Me.no_po = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kd_so = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kd_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nama_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jumlah = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.warna = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.harga = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.harga_berubah = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.urut_po = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TxtNoLoading = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txt_NmSupplier = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Txt_NmSupir = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtNoPlat = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Txt_KdSupplier = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.Dgv_DataDetailPO, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(1062, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1062, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(99, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Refraksi"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(18, 52)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(961, 12)
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
        Me.Panel3.Size = New System.Drawing.Size(19, 720)
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
        'Dgv_DataDetailPO
        '
        Me.Dgv_DataDetailPO.AllowUserToAddRows = False
        Me.Dgv_DataDetailPO.AllowUserToDeleteRows = False
        Me.Dgv_DataDetailPO.AllowUserToResizeColumns = False
        Me.Dgv_DataDetailPO.AllowUserToResizeRows = False
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_DataDetailPO.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Dgv_DataDetailPO.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Dgv_DataDetailPO.BackgroundColor = System.Drawing.Color.White
        Me.Dgv_DataDetailPO.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_DataDetailPO.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Dgv_DataDetailPO.ColumnHeadersHeight = 35
        Me.Dgv_DataDetailPO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.no_po, Me.kd_so, Me.kd_barang, Me.nama_barang, Me.jumlah, Me.satuan, Me.warna, Me.harga, Me.harga_berubah, Me.urut_po, Me.satuan_barang})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_DataDetailPO.DefaultCellStyle = DataGridViewCellStyle8
        Me.Dgv_DataDetailPO.Location = New System.Drawing.Point(20, 138)
        Me.Dgv_DataDetailPO.MultiSelect = False
        Me.Dgv_DataDetailPO.Name = "Dgv_DataDetailPO"
        Me.Dgv_DataDetailPO.RowHeadersWidth = 21
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_DataDetailPO.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.Dgv_DataDetailPO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Dgv_DataDetailPO.Size = New System.Drawing.Size(1012, 491)
        Me.Dgv_DataDetailPO.TabIndex = 481
        '
        'no_po
        '
        Me.no_po.HeaderText = "No PO"
        Me.no_po.Name = "no_po"
        Me.no_po.ReadOnly = True
        Me.no_po.Width = 150
        '
        'kd_so
        '
        Me.kd_so.HeaderText = "Lokasi"
        Me.kd_so.Name = "kd_so"
        Me.kd_so.ReadOnly = True
        Me.kd_so.Width = 150
        '
        'kd_barang
        '
        Me.kd_barang.HeaderText = "Kode Barang"
        Me.kd_barang.Name = "kd_barang"
        Me.kd_barang.ReadOnly = True
        Me.kd_barang.Width = 130
        '
        'nama_barang
        '
        Me.nama_barang.HeaderText = "Nama Barang"
        Me.nama_barang.Name = "nama_barang"
        Me.nama_barang.ReadOnly = True
        Me.nama_barang.Width = 300
        '
        'jumlah
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.jumlah.DefaultCellStyle = DataGridViewCellStyle3
        Me.jumlah.HeaderText = "Jumlah"
        Me.jumlah.Name = "jumlah"
        Me.jumlah.ReadOnly = True
        '
        'satuan
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.satuan.DefaultCellStyle = DataGridViewCellStyle4
        Me.satuan.HeaderText = "Satuan"
        Me.satuan.Name = "satuan"
        Me.satuan.ReadOnly = True
        '
        'warna
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.warna.DefaultCellStyle = DataGridViewCellStyle5
        Me.warna.HeaderText = "Warna"
        Me.warna.Name = "warna"
        Me.warna.ReadOnly = True
        '
        'harga
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.harga.DefaultCellStyle = DataGridViewCellStyle6
        Me.harga.HeaderText = "Harga"
        Me.harga.Name = "harga"
        Me.harga.ReadOnly = True
        Me.harga.Width = 150
        '
        'harga_berubah
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.harga_berubah.DefaultCellStyle = DataGridViewCellStyle7
        Me.harga_berubah.HeaderText = "Harga Berubah"
        Me.harga_berubah.Name = "harga_berubah"
        Me.harga_berubah.Width = 150
        '
        'urut_po
        '
        Me.urut_po.HeaderText = "Urut PO"
        Me.urut_po.Name = "urut_po"
        Me.urut_po.Visible = False
        '
        'satuan_barang
        '
        Me.satuan_barang.HeaderText = "Satuan Barang"
        Me.satuan_barang.Name = "satuan_barang"
        Me.satuan_barang.Visible = False
        '
        'TxtNoLoading
        '
        Me.TxtNoLoading.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNoLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNoLoading.Enabled = False
        Me.TxtNoLoading.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtNoLoading.Location = New System.Drawing.Point(133, 68)
        Me.TxtNoLoading.MaxLength = 50
        Me.TxtNoLoading.Name = "TxtNoLoading"
        Me.TxtNoLoading.Size = New System.Drawing.Size(212, 22)
        Me.TxtNoLoading.TabIndex = 482
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(22, 68)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 20)
        Me.Label8.TabIndex = 483
        Me.Label8.Text = "No Loading"
        '
        'Txt_NmSupplier
        '
        Me.Txt_NmSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmSupplier.Enabled = False
        Me.Txt_NmSupplier.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NmSupplier.Location = New System.Drawing.Point(597, 67)
        Me.Txt_NmSupplier.MaxLength = 100
        Me.Txt_NmSupplier.Name = "Txt_NmSupplier"
        Me.Txt_NmSupplier.Size = New System.Drawing.Size(212, 22)
        Me.Txt_NmSupplier.TabIndex = 486
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(416, 68)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(111, 20)
        Me.Label10.TabIndex = 487
        Me.Label10.Text = "Nama Supplier"
        '
        'Txt_NmSupir
        '
        Me.Txt_NmSupir.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmSupir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmSupir.Enabled = False
        Me.Txt_NmSupir.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NmSupir.Location = New System.Drawing.Point(533, 96)
        Me.Txt_NmSupir.MaxLength = 100
        Me.Txt_NmSupir.Name = "Txt_NmSupir"
        Me.Txt_NmSupir.Size = New System.Drawing.Size(276, 22)
        Me.Txt_NmSupir.TabIndex = 484
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(416, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 20)
        Me.Label3.TabIndex = 485
        Me.Label3.Text = "Supir"
        '
        'TxtNoPlat
        '
        Me.TxtNoPlat.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNoPlat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNoPlat.Enabled = False
        Me.TxtNoPlat.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtNoPlat.Location = New System.Drawing.Point(133, 96)
        Me.TxtNoPlat.MaxLength = 50
        Me.TxtNoPlat.Name = "TxtNoPlat"
        Me.TxtNoPlat.Size = New System.Drawing.Size(212, 22)
        Me.TxtNoPlat.TabIndex = 488
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(23, 96)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 20)
        Me.Label9.TabIndex = 489
        Me.Label9.Text = "No Plat"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Location = New System.Drawing.Point(1037, 59)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 720)
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
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(20, 650)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 490
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(20, 636)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(961, 12)
        Me.Panel7.TabIndex = 40
        Me.Panel7.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(104, 650)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 491
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(23, 684)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(961, 12)
        Me.Panel8.TabIndex = 40
        Me.Panel8.Visible = False
        '
        'Txt_KdSupplier
        '
        Me.Txt_KdSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdSupplier.Enabled = False
        Me.Txt_KdSupplier.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_KdSupplier.Location = New System.Drawing.Point(533, 67)
        Me.Txt_KdSupplier.MaxLength = 100
        Me.Txt_KdSupplier.Name = "Txt_KdSupplier"
        Me.Txt_KdSupplier.Size = New System.Drawing.Size(58, 22)
        Me.Txt_KdSupplier.TabIndex = 486
        '
        'Emi_Refraksi2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1062, 695)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.TxtNoPlat)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Txt_KdSupplier)
        Me.Controls.Add(Me.Txt_NmSupplier)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Txt_NmSupir)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtNoLoading)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Dgv_DataDetailPO)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Refraksi2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.Dgv_DataDetailPO, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Dgv_DataDetailPO As DataGridView
    Friend WithEvents TxtNoLoading As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Txt_NmSupplier As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Txt_NmSupir As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtNoPlat As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Txt_KdSupplier As TextBox
    Friend WithEvents no_po As DataGridViewTextBoxColumn
    Friend WithEvents kd_so As DataGridViewTextBoxColumn
    Friend WithEvents kd_barang As DataGridViewTextBoxColumn
    Friend WithEvents nama_barang As DataGridViewTextBoxColumn
    Friend WithEvents jumlah As DataGridViewTextBoxColumn
    Friend WithEvents satuan As DataGridViewTextBoxColumn
    Friend WithEvents warna As DataGridViewTextBoxColumn
    Friend WithEvents harga As DataGridViewTextBoxColumn
    Friend WithEvents harga_berubah As DataGridViewTextBoxColumn
    Friend WithEvents urut_po As DataGridViewTextBoxColumn
    Friend WithEvents satuan_barang As DataGridViewTextBoxColumn
End Class
