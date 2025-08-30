<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Controlling_Produksi
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
        Me.Txt_FilterValue = New System.Windows.Forms.TextBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lv_GR = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DetailToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValidasiGRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Lv_GI = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DetailToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValidasiGIToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1284, 54)
        Me.Panel1.TabIndex = 25
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 14)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(346, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Display - Production Controlling"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1179, 12)
        Me.Panel2.TabIndex = 39
        Me.Panel2.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 64)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(12, 515)
        Me.Panel5.TabIndex = 411
        Me.Panel5.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(16, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 20)
        Me.Label3.TabIndex = 413
        Me.Label3.Text = "Filter"
        '
        'Cmb_Filter
        '
        Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter.FormattingEnabled = True
        Me.Cmb_Filter.Location = New System.Drawing.Point(92, 69)
        Me.Cmb_Filter.Name = "Cmb_Filter"
        Me.Cmb_Filter.Size = New System.Drawing.Size(127, 26)
        Me.Cmb_Filter.TabIndex = 412
        '
        'Txt_FilterValue
        '
        Me.Txt_FilterValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_FilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_FilterValue.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Txt_FilterValue.Location = New System.Drawing.Point(222, 70)
        Me.Txt_FilterValue.MaxLength = 50
        Me.Txt_FilterValue.Name = "Txt_FilterValue"
        Me.Txt_FilterValue.Size = New System.Drawing.Size(304, 23)
        Me.Txt_FilterValue.TabIndex = 414
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(529, 66)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 32)
        Me.Btn_Cari.TabIndex = 373
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(20, 97)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1179, 12)
        Me.Panel3.TabIndex = 39
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1272, 136)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 515)
        Me.Panel4.TabIndex = 411
        Me.Panel4.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(20, 691)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1179, 12)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_GR)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 401)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1252, 288)
        Me.GroupBox1.TabIndex = 416
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Good Received"
        '
        'Lv_GR
        '
        Me.Lv_GR.ContextMenuStrip = Me.ContextMenuStrip2
        Me.Lv_GR.FullRowSelect = True
        Me.Lv_GR.GridLines = True
        Me.Lv_GR.HideSelection = False
        Me.Lv_GR.Location = New System.Drawing.Point(6, 22)
        Me.Lv_GR.Name = "Lv_GR"
        Me.Lv_GR.Size = New System.Drawing.Size(1240, 228)
        Me.Lv_GR.TabIndex = 415
        Me.Lv_GR.UseCompatibleStateImageBehavior = False
        Me.Lv_GR.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DetailToolStripMenuItem1, Me.ValidasiGRToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(181, 70)
        '
        'DetailToolStripMenuItem1
        '
        Me.DetailToolStripMenuItem1.Name = "DetailToolStripMenuItem1"
        Me.DetailToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.DetailToolStripMenuItem1.Text = "Detail"
        '
        'ValidasiGRToolStripMenuItem
        '
        Me.ValidasiGRToolStripMenuItem.Name = "ValidasiGRToolStripMenuItem"
        Me.ValidasiGRToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ValidasiGRToolStripMenuItem.Text = "Validasi GR"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(10, 258)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(280, 20)
        Me.Label8.TabIndex = 413
        Me.Label8.Text = "*Double Klik untuk Penerimaan Barang"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Panel9)
        Me.GroupBox2.Controls.Add(Me.Panel8)
        Me.GroupBox2.Controls.Add(Me.Panel7)
        Me.GroupBox2.Controls.Add(Me.Lv_GI)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 107)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1252, 288)
        Me.GroupBox2.TabIndex = 416
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Good Issue"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1008, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 16)
        Me.Label7.TabIndex = 417
        Me.Label7.Text = "Selesai"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(925, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 16)
        Me.Label5.TabIndex = 417
        Me.Label5.Text = "Berjalan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(805, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 16)
        Me.Label2.TabIndex = 417
        Me.Label2.Text = "Belum Berjalan"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.LightGreen
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.Label6)
        Me.Panel9.Enabled = False
        Me.Panel9.Location = New System.Drawing.Point(982, 15)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(20, 20)
        Me.Panel9.TabIndex = 416
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(0, 18)
        Me.Label6.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.LightYellow
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.Label4)
        Me.Panel8.Enabled = False
        Me.Panel8.Location = New System.Drawing.Point(899, 15)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(20, 20)
        Me.Panel8.TabIndex = 416
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 18)
        Me.Label4.TabIndex = 0
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.LightGray
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Label1)
        Me.Panel7.Enabled = False
        Me.Panel7.Location = New System.Drawing.Point(779, 15)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(20, 20)
        Me.Panel7.TabIndex = 416
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 18)
        Me.Label1.TabIndex = 0
        '
        'Lv_GI
        '
        Me.Lv_GI.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_GI.FullRowSelect = True
        Me.Lv_GI.GridLines = True
        Me.Lv_GI.HideSelection = False
        Me.Lv_GI.Location = New System.Drawing.Point(6, 41)
        Me.Lv_GI.Name = "Lv_GI"
        Me.Lv_GI.Size = New System.Drawing.Size(1240, 241)
        Me.Lv_GI.TabIndex = 415
        Me.Lv_GI.UseCompatibleStateImageBehavior = False
        Me.Lv_GI.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DetailToolStripMenuItem, Me.ValidasiGIToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(128, 48)
        '
        'DetailToolStripMenuItem
        '
        Me.DetailToolStripMenuItem.Name = "DetailToolStripMenuItem"
        Me.DetailToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
        Me.DetailToolStripMenuItem.Text = "Detail"
        '
        'ValidasiGIToolStripMenuItem
        '
        Me.ValidasiGIToolStripMenuItem.Name = "ValidasiGIToolStripMenuItem"
        Me.ValidasiGIToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
        Me.ValidasiGIToolStripMenuItem.Text = "Validasi GI"
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(610, 66)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(102, 32)
        Me.Btn_Refresh.TabIndex = 373
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(1162, 64)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 32)
        Me.Button1.TabIndex = 417
        Me.Button1.Text = "&History"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 52)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1284, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Controlling_Produksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1284, 701)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Txt_FilterValue)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Cmb_Filter)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Controlling_Produksi"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Txt_FilterValue As TextBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Lv_GR As ListView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Lv_GI As ListView
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents DetailToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label8 As Label
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents DetailToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ValidasiGRToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ValidasiGIToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button1 As Button
End Class
