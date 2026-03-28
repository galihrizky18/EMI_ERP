<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Transaksi_Validasi_Binding_Formula_Trial
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
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Lbl_Judul = New System.Windows.Forms.Label()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Btn_Cari = New System.Windows.Forms.Button()
		Me.Txt_Filter = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
		Me.Btn_Refresh = New System.Windows.Forms.Button()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Lv_Formula_Parent = New System.Windows.Forms.ListView()
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.ValidasiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.TolakToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Lv_Formula_Order = New System.Windows.Forms.ListView()
		Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.CompareFormulaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.Lv_Detail_Bahan = New System.Windows.Forms.ListView()
		Me.Panel9 = New System.Windows.Forms.Panel()
		Me.Panel_FIlter_Tanggal = New System.Windows.Forms.Panel()
		Me.Filter_Tgl_2 = New System.Windows.Forms.DateTimePicker()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Filter_Tgl_1 = New System.Windows.Forms.DateTimePicker()
		Me.Panel1.SuspendLayout()
		Me.Panel5.SuspendLayout()
		Me.ContextMenuStrip1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.ContextMenuStrip2.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.Panel_FIlter_Tanggal.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(1184, 45)
		Me.Panel1.TabIndex = 30
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 43)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Lbl_Judul
		'
		Me.Lbl_Judul.AutoSize = True
		Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lbl_Judul.Location = New System.Drawing.Point(22, 10)
		Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Lbl_Judul.Name = "Lbl_Judul"
		Me.Lbl_Judul.Size = New System.Drawing.Size(265, 25)
		Me.Lbl_Judul.TabIndex = 0
		Me.Lbl_Judul.Text = "Validasi - Binding Formula"
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 53)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 637)
		Me.Panel3.TabIndex = 495
		Me.Panel3.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Controls.Add(Me.Panel6)
		Me.Panel5.Location = New System.Drawing.Point(-2, 45)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(1348, 12)
		Me.Panel5.TabIndex = 502
		Me.Panel5.Visible = False
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(1167, 8)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(19, 637)
		Me.Panel6.TabIndex = 495
		Me.Panel6.Visible = False
		'
		'Btn_Cari
		'
		Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari.Location = New System.Drawing.Point(637, 55)
		Me.Btn_Cari.Margin = New System.Windows.Forms.Padding(2)
		Me.Btn_Cari.Name = "Btn_Cari"
		Me.Btn_Cari.Size = New System.Drawing.Size(89, 30)
		Me.Btn_Cari.TabIndex = 5
		Me.Btn_Cari.Text = "&Cari"
		Me.Btn_Cari.UseVisualStyleBackColor = False
		'
		'Txt_Filter
		'
		Me.Txt_Filter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Filter.Enabled = False
		Me.Txt_Filter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Txt_Filter.Location = New System.Drawing.Point(221, 60)
		Me.Txt_Filter.MaxLength = 100
		Me.Txt_Filter.Name = "Txt_Filter"
		Me.Txt_Filter.Size = New System.Drawing.Size(411, 21)
		Me.Txt_Filter.TabIndex = 4
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label1.Location = New System.Drawing.Point(26, 61)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(39, 17)
		Me.Label1.TabIndex = 517
		Me.Label1.Text = "Filter"
		'
		'Cmb_Filter
		'
		Me.Cmb_Filter.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Filter.FormattingEnabled = True
		Me.Cmb_Filter.Location = New System.Drawing.Point(94, 59)
		Me.Cmb_Filter.Name = "Cmb_Filter"
		Me.Cmb_Filter.Size = New System.Drawing.Size(121, 24)
		Me.Cmb_Filter.TabIndex = 3
		'
		'Btn_Refresh
		'
		Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Refresh.Location = New System.Drawing.Point(730, 54)
		Me.Btn_Refresh.Margin = New System.Windows.Forms.Padding(2)
		Me.Btn_Refresh.Name = "Btn_Refresh"
		Me.Btn_Refresh.Size = New System.Drawing.Size(89, 30)
		Me.Btn_Refresh.TabIndex = 6
		Me.Btn_Refresh.Text = "&Refresh"
		Me.Btn_Refresh.UseVisualStyleBackColor = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(15, 84)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1348, 12)
		Me.Panel2.TabIndex = 502
		Me.Panel2.Visible = False
		'
		'Lv_Formula_Parent
		'
		Me.Lv_Formula_Parent.ContextMenuStrip = Me.ContextMenuStrip1
		Me.Lv_Formula_Parent.FullRowSelect = True
		Me.Lv_Formula_Parent.GridLines = True
		Me.Lv_Formula_Parent.HideSelection = False
		Me.Lv_Formula_Parent.Location = New System.Drawing.Point(20, 96)
		Me.Lv_Formula_Parent.Name = "Lv_Formula_Parent"
		Me.Lv_Formula_Parent.OwnerDraw = True
		Me.Lv_Formula_Parent.Size = New System.Drawing.Size(1144, 204)
		Me.Lv_Formula_Parent.TabIndex = 0
		Me.Lv_Formula_Parent.UseCompatibleStateImageBehavior = False
		Me.Lv_Formula_Parent.View = System.Windows.Forms.View.Details
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ValidasiToolStripMenuItem, Me.TolakToolStripMenuItem})
		Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
		Me.ContextMenuStrip1.Size = New System.Drawing.Size(114, 48)
		'
		'ValidasiToolStripMenuItem
		'
		Me.ValidasiToolStripMenuItem.Name = "ValidasiToolStripMenuItem"
		Me.ValidasiToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
		Me.ValidasiToolStripMenuItem.Text = "Validasi"
		'
		'TolakToolStripMenuItem
		'
		Me.TolakToolStripMenuItem.Name = "TolakToolStripMenuItem"
		Me.TolakToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
		Me.TolakToolStripMenuItem.Text = "Tolak"
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(19, 596)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(1348, 15)
		Me.Panel4.TabIndex = 502
		Me.Panel4.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(16, 301)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1348, 12)
		Me.Panel7.TabIndex = 502
		Me.Panel7.Visible = False
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Lv_Formula_Order)
		Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox1.Location = New System.Drawing.Point(20, 315)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(465, 278)
		Me.GroupBox1.TabIndex = 1
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Formula Order"
		'
		'Lv_Formula_Order
		'
		Me.Lv_Formula_Order.ContextMenuStrip = Me.ContextMenuStrip2
		Me.Lv_Formula_Order.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Lv_Formula_Order.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_Formula_Order.FullRowSelect = True
		Me.Lv_Formula_Order.GridLines = True
		Me.Lv_Formula_Order.HideSelection = False
		Me.Lv_Formula_Order.Location = New System.Drawing.Point(3, 18)
		Me.Lv_Formula_Order.Name = "Lv_Formula_Order"
		Me.Lv_Formula_Order.OwnerDraw = True
		Me.Lv_Formula_Order.Size = New System.Drawing.Size(459, 257)
		Me.Lv_Formula_Order.TabIndex = 0
		Me.Lv_Formula_Order.UseCompatibleStateImageBehavior = False
		Me.Lv_Formula_Order.View = System.Windows.Forms.View.Details
		'
		'ContextMenuStrip2
		'
		Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CompareFormulaToolStripMenuItem})
		Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
		Me.ContextMenuStrip2.Size = New System.Drawing.Size(181, 48)
		'
		'CompareFormulaToolStripMenuItem
		'
		Me.CompareFormulaToolStripMenuItem.Name = "CompareFormulaToolStripMenuItem"
		Me.CompareFormulaToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
		Me.CompareFormulaToolStripMenuItem.Text = "Compare Formula"
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.Red
		Me.Panel8.Location = New System.Drawing.Point(486, 332)
		Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Size = New System.Drawing.Size(12, 637)
		Me.Panel8.TabIndex = 495
		Me.Panel8.Visible = False
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.Lv_Detail_Bahan)
		Me.GroupBox2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox2.Location = New System.Drawing.Point(500, 315)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(664, 278)
		Me.GroupBox2.TabIndex = 2
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Detail Bahan"
		'
		'Lv_Detail_Bahan
		'
		Me.Lv_Detail_Bahan.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Lv_Detail_Bahan.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_Detail_Bahan.FullRowSelect = True
		Me.Lv_Detail_Bahan.GridLines = True
		Me.Lv_Detail_Bahan.HideSelection = False
		Me.Lv_Detail_Bahan.Location = New System.Drawing.Point(3, 18)
		Me.Lv_Detail_Bahan.Name = "Lv_Detail_Bahan"
		Me.Lv_Detail_Bahan.OwnerDraw = True
		Me.Lv_Detail_Bahan.Size = New System.Drawing.Size(658, 257)
		Me.Lv_Detail_Bahan.TabIndex = 0
		Me.Lv_Detail_Bahan.UseCompatibleStateImageBehavior = False
		Me.Lv_Detail_Bahan.View = System.Windows.Forms.View.Details
		'
		'Panel9
		'
		Me.Panel9.BackColor = System.Drawing.Color.Red
		Me.Panel9.Location = New System.Drawing.Point(1165, 39)
		Me.Panel9.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel9.Name = "Panel9"
		Me.Panel9.Size = New System.Drawing.Size(19, 637)
		Me.Panel9.TabIndex = 495
		Me.Panel9.Visible = False
		'
		'Panel_FIlter_Tanggal
		'
		Me.Panel_FIlter_Tanggal.Controls.Add(Me.Filter_Tgl_2)
		Me.Panel_FIlter_Tanggal.Controls.Add(Me.Label2)
		Me.Panel_FIlter_Tanggal.Controls.Add(Me.Filter_Tgl_1)
		Me.Panel_FIlter_Tanggal.Location = New System.Drawing.Point(900, 54)
		Me.Panel_FIlter_Tanggal.Name = "Panel_FIlter_Tanggal"
		Me.Panel_FIlter_Tanggal.Size = New System.Drawing.Size(490, 37)
		Me.Panel_FIlter_Tanggal.TabIndex = 519
		'
		'Filter_Tgl_2
		'
		Me.Filter_Tgl_2.Location = New System.Drawing.Point(268, 7)
		Me.Filter_Tgl_2.Name = "Filter_Tgl_2"
		Me.Filter_Tgl_2.Size = New System.Drawing.Size(217, 20)
		Me.Filter_Tgl_2.TabIndex = 519
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(231, 8)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(30, 17)
		Me.Label2.TabIndex = 518
		Me.Label2.Text = "S/D"
		'
		'Filter_Tgl_1
		'
		Me.Filter_Tgl_1.Location = New System.Drawing.Point(5, 7)
		Me.Filter_Tgl_1.Name = "Filter_Tgl_1"
		Me.Filter_Tgl_1.Size = New System.Drawing.Size(217, 20)
		Me.Filter_Tgl_1.TabIndex = 0
		'
		'N_EMI_Transaksi_Validasi_Binding_Formula_Trial
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1184, 611)
		Me.Controls.Add(Me.Panel_FIlter_Tanggal)
		Me.Controls.Add(Me.Btn_Cari)
		Me.Controls.Add(Me.Btn_Refresh)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Lv_Formula_Parent)
		Me.Controls.Add(Me.Cmb_Filter)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Txt_Filter)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel8)
		Me.Controls.Add(Me.Panel9)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_Transaksi_Validasi_Binding_Formula_Trial"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel5.ResumeLayout(False)
		Me.ContextMenuStrip1.ResumeLayout(False)
		Me.GroupBox1.ResumeLayout(False)
		Me.ContextMenuStrip2.ResumeLayout(False)
		Me.GroupBox2.ResumeLayout(False)
		Me.Panel_FIlter_Tanggal.ResumeLayout(False)
		Me.Panel_FIlter_Tanggal.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Lbl_Judul As Label
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Btn_Cari As Button
	Friend WithEvents Txt_Filter As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Cmb_Filter As ComboBox
	Friend WithEvents Btn_Refresh As Button
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Lv_Formula_Parent As ListView
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Panel6 As Panel
	Friend WithEvents Panel7 As Panel
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Panel8 As Panel
	Friend WithEvents Lv_Formula_Order As ListView
	Friend WithEvents GroupBox2 As GroupBox
	Friend WithEvents Lv_Detail_Bahan As ListView
	Friend WithEvents Panel9 As Panel
	Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
	Friend WithEvents ValidasiToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents TolakToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents Panel_FIlter_Tanggal As Panel
	Friend WithEvents Filter_Tgl_2 As DateTimePicker
	Friend WithEvents Label2 As Label
	Friend WithEvents Filter_Tgl_1 As DateTimePicker
	Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
	Friend WithEvents CompareFormulaToolStripMenuItem As ToolStripMenuItem
End Class
