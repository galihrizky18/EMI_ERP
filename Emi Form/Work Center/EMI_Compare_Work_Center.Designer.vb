<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Compare_Work_Center
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dgv_biaya = New System.Windows.Forms.DataGridView()
        Me.kode_biaya = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.biaya = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.checklist = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.dgv_workcenter = New System.Windows.Forms.DataGridView()
        Me.kd_so = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kd_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nm_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mesin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.BtnCari = New System.Windows.Forms.Button()
        Me.CmbTahun = New System.Windows.Forms.ComboBox()
        Me.CmbBulan = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        CType(Me.dgv_biaya, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(1240, 63)
        Me.Panel1.TabIndex = 24
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(18, 14)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(259, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Work Center - Compare"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 62)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1179, 15)
        Me.Panel2.TabIndex = 36
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 78)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(15, 515)
        Me.Panel3.TabIndex = 37
        Me.Panel3.Visible = False
        '
        'dgv_biaya
        '
        Me.dgv_biaya.AllowUserToAddRows = False
        Me.dgv_biaya.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_biaya.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_biaya.ColumnHeadersHeight = 40
        Me.dgv_biaya.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.kode_biaya, Me.biaya, Me.checklist})
        Me.dgv_biaya.Location = New System.Drawing.Point(13, 120)
        Me.dgv_biaya.Margin = New System.Windows.Forms.Padding(4)
        Me.dgv_biaya.Name = "dgv_biaya"
        Me.dgv_biaya.RowHeadersWidth = 10
        Me.dgv_biaya.RowTemplate.Height = 21
        Me.dgv_biaya.Size = New System.Drawing.Size(208, 463)
        Me.dgv_biaya.TabIndex = 399
        '
        'kode_biaya
        '
        Me.kode_biaya.HeaderText = "Kode Biaya"
        Me.kode_biaya.Name = "kode_biaya"
        Me.kode_biaya.ReadOnly = True
        Me.kode_biaya.Visible = False
        '
        'biaya
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.biaya.DefaultCellStyle = DataGridViewCellStyle2
        Me.biaya.HeaderText = "Biaya"
        Me.biaya.Name = "biaya"
        Me.biaya.ReadOnly = True
        Me.biaya.Width = 120
        '
        'checklist
        '
        Me.checklist.HeaderText = ""
        Me.checklist.Name = "checklist"
        Me.checklist.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.checklist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.checklist.Width = 70
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(221, 121)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(15, 515)
        Me.Panel4.TabIndex = 37
        Me.Panel4.Visible = False
        '
        'dgv_workcenter
        '
        Me.dgv_workcenter.AllowUserToAddRows = False
        Me.dgv_workcenter.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_workcenter.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_workcenter.ColumnHeadersHeight = 60
        Me.dgv_workcenter.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.kd_so, Me.kd_barang, Me.nm_barang, Me.mesin})
        Me.dgv_workcenter.Location = New System.Drawing.Point(238, 120)
        Me.dgv_workcenter.Margin = New System.Windows.Forms.Padding(4)
        Me.dgv_workcenter.Name = "dgv_workcenter"
        Me.dgv_workcenter.RowHeadersWidth = 10
        Me.dgv_workcenter.RowTemplate.Height = 21
        Me.dgv_workcenter.Size = New System.Drawing.Size(989, 463)
        Me.dgv_workcenter.TabIndex = 400
        '
        'kd_so
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.kd_so.DefaultCellStyle = DataGridViewCellStyle4
        Me.kd_so.Frozen = True
        Me.kd_so.HeaderText = "Kode SO"
        Me.kd_so.Name = "kd_so"
        Me.kd_so.ReadOnly = True
        Me.kd_so.Width = 150
        '
        'kd_barang
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.kd_barang.DefaultCellStyle = DataGridViewCellStyle5
        Me.kd_barang.Frozen = True
        Me.kd_barang.HeaderText = "Kode Barang"
        Me.kd_barang.Name = "kd_barang"
        Me.kd_barang.ReadOnly = True
        Me.kd_barang.Width = 120
        '
        'nm_barang
        '
        Me.nm_barang.Frozen = True
        Me.nm_barang.HeaderText = "Nama Barang"
        Me.nm_barang.Name = "nm_barang"
        Me.nm_barang.ReadOnly = True
        Me.nm_barang.Width = 200
        '
        'mesin
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.mesin.DefaultCellStyle = DataGridViewCellStyle6
        Me.mesin.Frozen = True
        Me.mesin.HeaderText = "Mesin"
        Me.mesin.Name = "mesin"
        Me.mesin.ReadOnly = True
        Me.mesin.Width = 120
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1225, 79)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(15, 515)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(23, 581)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1179, 15)
        Me.Panel6.TabIndex = 36
        Me.Panel6.Visible = False
        '
        'BtnCari
        '
        Me.BtnCari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCari.ForeColor = System.Drawing.Color.White
        Me.BtnCari.Location = New System.Drawing.Point(441, 77)
        Me.BtnCari.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnCari.Name = "BtnCari"
        Me.BtnCari.Size = New System.Drawing.Size(98, 32)
        Me.BtnCari.TabIndex = 407
        Me.BtnCari.Text = "&Cari"
        Me.BtnCari.UseVisualStyleBackColor = False
        '
        'CmbTahun
        '
        Me.CmbTahun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTahun.FormattingEnabled = True
        Me.CmbTahun.Location = New System.Drawing.Point(287, 80)
        Me.CmbTahun.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbTahun.Name = "CmbTahun"
        Me.CmbTahun.Size = New System.Drawing.Size(146, 24)
        Me.CmbTahun.TabIndex = 406
        '
        'CmbBulan
        '
        Me.CmbBulan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbBulan.FormattingEnabled = True
        Me.CmbBulan.Location = New System.Drawing.Point(65, 80)
        Me.CmbBulan.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbBulan.Name = "CmbBulan"
        Me.CmbBulan.Size = New System.Drawing.Size(146, 24)
        Me.CmbBulan.TabIndex = 405
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(229, 82)
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
        Me.Label4.Location = New System.Drawing.Point(13, 82)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 17)
        Me.Label4.TabIndex = 403
        Me.Label4.Text = "Bulan"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(16, 104)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1179, 15)
        Me.Panel7.TabIndex = 37
        Me.Panel7.Visible = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1240, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Compare_Work_Center
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1240, 595)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.BtnCari)
        Me.Controls.Add(Me.CmbTahun)
        Me.Controls.Add(Me.CmbBulan)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dgv_workcenter)
        Me.Controls.Add(Me.dgv_biaya)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Compare_Work_Center"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgv_biaya, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_workcenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents dgv_biaya As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents dgv_workcenter As DataGridView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents kode_biaya As DataGridViewTextBoxColumn
    Friend WithEvents biaya As DataGridViewTextBoxColumn
    Friend WithEvents checklist As DataGridViewCheckBoxColumn
    Friend WithEvents kd_so As DataGridViewTextBoxColumn
    Friend WithEvents kd_barang As DataGridViewTextBoxColumn
    Friend WithEvents nm_barang As DataGridViewTextBoxColumn
    Friend WithEvents mesin As DataGridViewTextBoxColumn
    Friend WithEvents BtnCari As Button
    Friend WithEvents CmbTahun As ComboBox
    Friend WithEvents CmbBulan As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel7 As Panel
End Class
