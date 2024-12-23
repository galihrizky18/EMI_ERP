<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Transaksi_Work_Center2
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnCari = New System.Windows.Forms.Button()
        Me.CmbLokasi = New System.Windows.Forms.ComboBox()
        Me.TxtBarangMasuk_NoFaktur = New System.Windows.Forms.TextBox()
        Me.CmbTahun = New System.Windows.Forms.ComboBox()
        Me.CmbBulan = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.dgv_routing = New System.Windows.Forms.DataGridView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.id_routing = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.routing = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.checklist = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.kode_routing = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prefix_code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_jenis_produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgv_workcenter = New System.Windows.Forms.DataGridView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.BtnSimpan = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.id_routingWork = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_routing_workcenter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.routing_workcenter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mesin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.dgv_routing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_workcenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1285, 63)
        Me.Panel1.TabIndex = 24
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 61)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1285, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(18, 14)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(248, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Transaksi Work Center"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 80)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(15, 515)
        Me.Panel3.TabIndex = 39
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(1, 64)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1179, 15)
        Me.Panel2.TabIndex = 38
        Me.Panel2.Visible = False
        '
        'BtnCari
        '
        Me.BtnCari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCari.ForeColor = System.Drawing.Color.White
        Me.BtnCari.Location = New System.Drawing.Point(445, 101)
        Me.BtnCari.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnCari.Name = "BtnCari"
        Me.BtnCari.Size = New System.Drawing.Size(98, 32)
        Me.BtnCari.TabIndex = 409
        Me.BtnCari.Text = "&Cari"
        Me.BtnCari.UseVisualStyleBackColor = False
        '
        'CmbLokasi
        '
        Me.CmbLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLokasi.FormattingEnabled = True
        Me.CmbLokasi.Location = New System.Drawing.Point(1093, 77)
        Me.CmbLokasi.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbLokasi.Name = "CmbLokasi"
        Me.CmbLokasi.Size = New System.Drawing.Size(176, 24)
        Me.CmbLokasi.TabIndex = 408
        '
        'TxtBarangMasuk_NoFaktur
        '
        Me.TxtBarangMasuk_NoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtBarangMasuk_NoFaktur.Enabled = False
        Me.TxtBarangMasuk_NoFaktur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBarangMasuk_NoFaktur.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtBarangMasuk_NoFaktur.Location = New System.Drawing.Point(17, 80)
        Me.TxtBarangMasuk_NoFaktur.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtBarangMasuk_NoFaktur.MaxLength = 20
        Me.TxtBarangMasuk_NoFaktur.Name = "TxtBarangMasuk_NoFaktur"
        Me.TxtBarangMasuk_NoFaktur.Size = New System.Drawing.Size(245, 21)
        Me.TxtBarangMasuk_NoFaktur.TabIndex = 407
        '
        'CmbTahun
        '
        Me.CmbTahun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTahun.FormattingEnabled = True
        Me.CmbTahun.Location = New System.Drawing.Point(291, 106)
        Me.CmbTahun.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbTahun.Name = "CmbTahun"
        Me.CmbTahun.Size = New System.Drawing.Size(146, 24)
        Me.CmbTahun.TabIndex = 406
        '
        'CmbBulan
        '
        Me.CmbBulan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbBulan.FormattingEnabled = True
        Me.CmbBulan.Location = New System.Drawing.Point(69, 106)
        Me.CmbBulan.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbBulan.Name = "CmbBulan"
        Me.CmbBulan.Size = New System.Drawing.Size(146, 24)
        Me.CmbBulan.TabIndex = 405
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(233, 108)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 17)
        Me.Label6.TabIndex = 404
        Me.Label6.Text = "Tahun"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(17, 108)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 17)
        Me.Label4.TabIndex = 403
        Me.Label4.Text = "Bulan"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(19, 130)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1179, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'dgv_routing
        '
        Me.dgv_routing.AllowUserToAddRows = False
        Me.dgv_routing.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_routing.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_routing.ColumnHeadersHeight = 40
        Me.dgv_routing.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_routing, Me.routing, Me.checklist, Me.kode_routing, Me.prefix_code, Me.id_jenis_produk})
        Me.dgv_routing.Location = New System.Drawing.Point(17, 147)
        Me.dgv_routing.Margin = New System.Windows.Forms.Padding(4)
        Me.dgv_routing.Name = "dgv_routing"
        Me.dgv_routing.RowHeadersWidth = 10
        Me.dgv_routing.RowTemplate.Height = 21
        Me.dgv_routing.Size = New System.Drawing.Size(208, 424)
        Me.dgv_routing.TabIndex = 400
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(224, 147)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(15, 515)
        Me.Panel5.TabIndex = 410
        Me.Panel5.Visible = False
        '
        'id_routing
        '
        Me.id_routing.HeaderText = "ID Routing"
        Me.id_routing.Name = "id_routing"
        Me.id_routing.ReadOnly = True
        Me.id_routing.Visible = False
        '
        'routing
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.routing.DefaultCellStyle = DataGridViewCellStyle2
        Me.routing.HeaderText = "Routing"
        Me.routing.Name = "routing"
        Me.routing.ReadOnly = True
        Me.routing.Width = 140
        '
        'checklist
        '
        Me.checklist.HeaderText = ""
        Me.checklist.Name = "checklist"
        Me.checklist.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.checklist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.checklist.Width = 50
        '
        'kode_routing
        '
        Me.kode_routing.HeaderText = "Kode Routing"
        Me.kode_routing.Name = "kode_routing"
        Me.kode_routing.ReadOnly = True
        Me.kode_routing.Visible = False
        '
        'prefix_code
        '
        Me.prefix_code.HeaderText = "Prefix Code"
        Me.prefix_code.Name = "prefix_code"
        Me.prefix_code.ReadOnly = True
        Me.prefix_code.Visible = False
        '
        'id_jenis_produk
        '
        Me.id_jenis_produk.HeaderText = "ID Jenis Produk"
        Me.id_jenis_produk.Name = "id_jenis_produk"
        Me.id_jenis_produk.ReadOnly = True
        Me.id_jenis_produk.Visible = False
        '
        'dgv_workcenter
        '
        Me.dgv_workcenter.AllowUserToAddRows = False
        Me.dgv_workcenter.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_workcenter.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_workcenter.ColumnHeadersHeight = 40
        Me.dgv_workcenter.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_routingWork, Me.id_routing_workcenter, Me.routing_workcenter, Me.mesin})
        Me.dgv_workcenter.Location = New System.Drawing.Point(238, 147)
        Me.dgv_workcenter.Margin = New System.Windows.Forms.Padding(4)
        Me.dgv_workcenter.Name = "dgv_workcenter"
        Me.dgv_workcenter.RowHeadersWidth = 10
        Me.dgv_workcenter.RowTemplate.Height = 21
        Me.dgv_workcenter.Size = New System.Drawing.Size(1034, 424)
        Me.dgv_workcenter.TabIndex = 411
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(1270, 63)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(15, 515)
        Me.Panel6.TabIndex = 410
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(17, 610)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1179, 15)
        Me.Panel7.TabIndex = 38
        Me.Panel7.Visible = False
        '
        'BtnSimpan
        '
        Me.BtnSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnSimpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnSimpan.ForeColor = System.Drawing.Color.White
        Me.BtnSimpan.Location = New System.Drawing.Point(238, 579)
        Me.BtnSimpan.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnSimpan.Name = "BtnSimpan"
        Me.BtnSimpan.Size = New System.Drawing.Size(114, 32)
        Me.BtnSimpan.TabIndex = 412
        Me.BtnSimpan.Text = "&Simpan"
        Me.BtnSimpan.UseVisualStyleBackColor = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(19, 567)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1179, 15)
        Me.Panel8.TabIndex = 38
        Me.Panel8.Visible = False
        '
        'BtnRefresh
        '
        Me.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnRefresh.ForeColor = System.Drawing.Color.White
        Me.BtnRefresh.Location = New System.Drawing.Point(355, 579)
        Me.BtnRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(98, 32)
        Me.BtnRefresh.TabIndex = 413
        Me.BtnRefresh.Text = "&Refresh"
        Me.BtnRefresh.UseVisualStyleBackColor = False
        '
        'id_routingWork
        '
        Me.id_routingWork.Frozen = True
        Me.id_routingWork.HeaderText = "ID Routing"
        Me.id_routingWork.Name = "id_routingWork"
        Me.id_routingWork.ReadOnly = True
        Me.id_routingWork.Visible = False
        '
        'id_routing_workcenter
        '
        Me.id_routing_workcenter.Frozen = True
        Me.id_routing_workcenter.HeaderText = "ID_WorkCenter"
        Me.id_routing_workcenter.Name = "id_routing_workcenter"
        Me.id_routing_workcenter.ReadOnly = True
        Me.id_routing_workcenter.Visible = False
        '
        'routing_workcenter
        '
        Me.routing_workcenter.Frozen = True
        Me.routing_workcenter.HeaderText = "Routing"
        Me.routing_workcenter.Name = "routing_workcenter"
        Me.routing_workcenter.ReadOnly = True
        Me.routing_workcenter.Width = 180
        '
        'mesin
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.mesin.DefaultCellStyle = DataGridViewCellStyle4
        Me.mesin.Frozen = True
        Me.mesin.HeaderText = "Mesin"
        Me.mesin.Name = "mesin"
        Me.mesin.ReadOnly = True
        Me.mesin.Width = 180
        '
        'EMI_Transaksi_Work_Center2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1285, 626)
        Me.Controls.Add(Me.BtnRefresh)
        Me.Controls.Add(Me.BtnSimpan)
        Me.Controls.Add(Me.dgv_workcenter)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.dgv_routing)
        Me.Controls.Add(Me.BtnCari)
        Me.Controls.Add(Me.CmbLokasi)
        Me.Controls.Add(Me.TxtBarangMasuk_NoFaktur)
        Me.Controls.Add(Me.CmbTahun)
        Me.Controls.Add(Me.CmbBulan)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "EMI_Transaksi_Work_Center2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgv_routing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_workcenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BtnCari As Button
    Friend WithEvents CmbLokasi As ComboBox
    Friend WithEvents TxtBarangMasuk_NoFaktur As TextBox
    Friend WithEvents CmbTahun As ComboBox
    Friend WithEvents CmbBulan As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents dgv_routing As DataGridView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents id_routing As DataGridViewTextBoxColumn
    Friend WithEvents routing As DataGridViewTextBoxColumn
    Friend WithEvents checklist As DataGridViewCheckBoxColumn
    Friend WithEvents kode_routing As DataGridViewTextBoxColumn
    Friend WithEvents prefix_code As DataGridViewTextBoxColumn
    Friend WithEvents id_jenis_produk As DataGridViewTextBoxColumn
    Friend WithEvents dgv_workcenter As DataGridView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents BtnSimpan As Button
    Friend WithEvents Panel8 As Panel
    Friend WithEvents BtnRefresh As Button
    Friend WithEvents id_routingWork As DataGridViewTextBoxColumn
    Friend WithEvents id_routing_workcenter As DataGridViewTextBoxColumn
    Friend WithEvents routing_workcenter As DataGridViewTextBoxColumn
    Friend WithEvents mesin As DataGridViewTextBoxColumn
End Class
