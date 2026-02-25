<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Emi_Request_Material_Display
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Data = New System.Windows.Forms.DataGridView()
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.no_faktur = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lokasi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kd_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nm_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tgl_produksi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jam_produksi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jumlah = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.user = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelesaiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        CType(Me.Lv_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1028, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1028, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(286, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display - Request Material"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(6, 52)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 38
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 72)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 416)
        Me.Panel3.TabIndex = 39
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1008, 72)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 416)
        Me.Panel4.TabIndex = 39
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(22, 537)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 38
        Me.Panel5.Visible = False
        '
        'Lv_Data
        '
        Me.Lv_Data.AllowUserToAddRows = False
        Me.Lv_Data.AllowUserToDeleteRows = False
        Me.Lv_Data.AllowUserToResizeColumns = False
        Me.Lv_Data.AllowUserToResizeRows = False
        Me.Lv_Data.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Lv_Data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Lv_Data.ColumnHeadersHeight = 40
        Me.Lv_Data.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.status, Me.no_faktur, Me.lokasi, Me.kd_barang, Me.nm_barang, Me.tgl_produksi, Me.jam_produksi, Me.jumlah, Me.satuan, Me.user})
        Me.Lv_Data.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_Data.Location = New System.Drawing.Point(20, 66)
        Me.Lv_Data.MultiSelect = False
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.RowHeadersWidth = 21
        Me.Lv_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Lv_Data.Size = New System.Drawing.Size(990, 471)
        Me.Lv_Data.TabIndex = 286
        '
        'status
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.status.DefaultCellStyle = DataGridViewCellStyle2
        Me.status.Frozen = True
        Me.status.HeaderText = "Status"
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        Me.status.Width = 120
        '
        'no_faktur
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft
        Me.no_faktur.DefaultCellStyle = DataGridViewCellStyle3
        Me.no_faktur.HeaderText = "No Faktur"
        Me.no_faktur.Name = "no_faktur"
        Me.no_faktur.ReadOnly = True
        Me.no_faktur.Width = 145
        '
        'lokasi
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.lokasi.DefaultCellStyle = DataGridViewCellStyle4
        Me.lokasi.HeaderText = "Lokasi"
        Me.lokasi.Name = "lokasi"
        Me.lokasi.ReadOnly = True
        Me.lokasi.Width = 145
        '
        'kd_barang
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.kd_barang.DefaultCellStyle = DataGridViewCellStyle5
        Me.kd_barang.HeaderText = "Kode Barang"
        Me.kd_barang.Name = "kd_barang"
        Me.kd_barang.ReadOnly = True
        Me.kd_barang.Width = 145
        '
        'nm_barang
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.nm_barang.DefaultCellStyle = DataGridViewCellStyle6
        Me.nm_barang.HeaderText = "Nama Barang"
        Me.nm_barang.Name = "nm_barang"
        Me.nm_barang.ReadOnly = True
        Me.nm_barang.Width = 270
        '
        'tgl_produksi
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.tgl_produksi.DefaultCellStyle = DataGridViewCellStyle7
        Me.tgl_produksi.HeaderText = "Tanggal Produksi"
        Me.tgl_produksi.Name = "tgl_produksi"
        Me.tgl_produksi.ReadOnly = True
        Me.tgl_produksi.Width = 125
        '
        'jam_produksi
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.jam_produksi.DefaultCellStyle = DataGridViewCellStyle8
        Me.jam_produksi.HeaderText = "Jam Produksi"
        Me.jam_produksi.Name = "jam_produksi"
        Me.jam_produksi.ReadOnly = True
        '
        'jumlah
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.jumlah.DefaultCellStyle = DataGridViewCellStyle9
        Me.jumlah.HeaderText = "Jumlah"
        Me.jumlah.Name = "jumlah"
        Me.jumlah.ReadOnly = True
        Me.jumlah.Width = 80
        '
        'satuan
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.satuan.DefaultCellStyle = DataGridViewCellStyle10
        Me.satuan.HeaderText = "Satuan"
        Me.satuan.Name = "satuan"
        Me.satuan.ReadOnly = True
        Me.satuan.Width = 80
        '
        'user
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.user.DefaultCellStyle = DataGridViewCellStyle11
        Me.user.HeaderText = "User"
        Me.user.Name = "user"
        Me.user.ReadOnly = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelesaiToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(110, 26)
        '
        'SelesaiToolStripMenuItem
        '
        Me.SelesaiToolStripMenuItem.Name = "SelesaiToolStripMenuItem"
        Me.SelesaiToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.SelesaiToolStripMenuItem.Text = "Selesai"
        '
        'Emi_Request_Material_Display
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1028, 549)
        Me.Controls.Add(Me.Lv_Data)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Request_Material_Display"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Lv_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_Data As DataGridView
    Friend WithEvents status As DataGridViewTextBoxColumn
    Friend WithEvents no_faktur As DataGridViewTextBoxColumn
    Friend WithEvents lokasi As DataGridViewTextBoxColumn
    Friend WithEvents kd_barang As DataGridViewTextBoxColumn
    Friend WithEvents nm_barang As DataGridViewTextBoxColumn
    Friend WithEvents tgl_produksi As DataGridViewTextBoxColumn
    Friend WithEvents jam_produksi As DataGridViewTextBoxColumn
    Friend WithEvents jumlah As DataGridViewTextBoxColumn
    Friend WithEvents satuan As DataGridViewTextBoxColumn
    Friend WithEvents user As DataGridViewTextBoxColumn
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SelesaiToolStripMenuItem As ToolStripMenuItem
End Class
