<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Display_Summary_POInduk
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Lv_PO = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CetakUlangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelesaiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Chk_Status = New System.Windows.Forms.CheckBox()
        Me.Txt_Filter_Lain = New System.Windows.Forms.TextBox()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Cmb_Filter_Status = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Chk1 = New System.Windows.Forms.CheckBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Cmb_Filter_Lain = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Chk3 = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Chk2 = New System.Windows.Forms.CheckBox()
        Me.Cmb_FIlter_Tanggal = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_PO_Detail = New System.Windows.Forms.ListView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Lv_DetSubPO = New System.Windows.Forms.ListView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1184, 55)
        Me.Panel1.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 12)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(425, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Summary Data - Purchase Order Parent"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 55)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(2221, 12)
        Me.Panel2.TabIndex = 39
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 90)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 982)
        Me.Panel3.TabIndex = 40
        Me.Panel3.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Panel7)
        Me.GroupBox1.Controls.Add(Me.Panel9)
        Me.GroupBox1.Controls.Add(Me.Lv_PO)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(11, 57)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1160, 241)
        Me.GroupBox1.TabIndex = 41
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data PO"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1058, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 18)
        Me.Label4.TabIndex = 346
        Me.Label4.Text = "PO di Batalkan"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.DarkRed
        Me.Panel9.Location = New System.Drawing.Point(1037, 15)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(15, 15)
        Me.Panel9.TabIndex = 345
        '
        'Lv_PO
        '
        Me.Lv_PO.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_PO.FullRowSelect = True
        Me.Lv_PO.GridLines = True
        Me.Lv_PO.HideSelection = False
        Me.Lv_PO.Location = New System.Drawing.Point(7, 33)
        Me.Lv_PO.Name = "Lv_PO"
        Me.Lv_PO.Size = New System.Drawing.Size(1153, 201)
        Me.Lv_PO.TabIndex = 0
        Me.Lv_PO.UseCompatibleStateImageBehavior = False
        Me.Lv_PO.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CetakUlangToolStripMenuItem, Me.SelesaiToolStripMenuItem, Me.BatalToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(139, 70)
        '
        'CetakUlangToolStripMenuItem
        '
        Me.CetakUlangToolStripMenuItem.Name = "CetakUlangToolStripMenuItem"
        Me.CetakUlangToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.CetakUlangToolStripMenuItem.Text = "Cetak Ulang"
        '
        'SelesaiToolStripMenuItem
        '
        Me.SelesaiToolStripMenuItem.Name = "SelesaiToolStripMenuItem"
        Me.SelesaiToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.SelesaiToolStripMenuItem.Text = "Selesai"
        '
        'BatalToolStripMenuItem
        '
        Me.BatalToolStripMenuItem.Name = "BatalToolStripMenuItem"
        Me.BatalToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.BatalToolStripMenuItem.Text = "Batal"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1170, 77)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 982)
        Me.Panel4.TabIndex = 40
        Me.Panel4.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Chk_Status)
        Me.GroupBox3.Controls.Add(Me.Txt_Filter_Lain)
        Me.GroupBox3.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBox3.Controls.Add(Me.Cmb_Filter_Status)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Chk1)
        Me.GroupBox3.Controls.Add(Me.Btn_Cari)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox3.Controls.Add(Me.Cmb_Filter_Lain)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Chk3)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox3.Controls.Add(Me.Chk2)
        Me.GroupBox3.Controls.Add(Me.Cmb_FIlter_Tanggal)
        Me.GroupBox3.Location = New System.Drawing.Point(11, 546)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(755, 143)
        Me.GroupBox3.TabIndex = 42
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'Chk_Status
        '
        Me.Chk_Status.AutoSize = True
        Me.Chk_Status.Location = New System.Drawing.Point(8, 55)
        Me.Chk_Status.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_Status.Name = "Chk_Status"
        Me.Chk_Status.Size = New System.Drawing.Size(62, 20)
        Me.Chk_Status.TabIndex = 87
        Me.Chk_Status.Text = "Status"
        Me.Chk_Status.UseVisualStyleBackColor = True
        '
        'Txt_Filter_Lain
        '
        Me.Txt_Filter_Lain.Location = New System.Drawing.Point(355, 114)
        Me.Txt_Filter_Lain.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Txt_Filter_Lain.Name = "Txt_Filter_Lain"
        Me.Txt_Filter_Lain.Size = New System.Drawing.Size(295, 20)
        Me.Txt_Filter_Lain.TabIndex = 84
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(593, 21)
        Me.Cmb_Lokasi.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(148, 24)
        Me.Cmb_Lokasi.TabIndex = 30
        '
        'Cmb_Filter_Status
        '
        Me.Cmb_Filter_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter_Status.Enabled = False
        Me.Cmb_Filter_Status.FormattingEnabled = True
        Me.Cmb_Filter_Status.Location = New System.Drawing.Point(165, 52)
        Me.Cmb_Filter_Status.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Filter_Status.Name = "Cmb_Filter_Status"
        Me.Cmb_Filter_Status.Size = New System.Drawing.Size(134, 24)
        Me.Cmb_Filter_Status.TabIndex = 88
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(308, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 16)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Value"
        '
        'Chk1
        '
        Me.Chk1.AutoSize = True
        Me.Chk1.Location = New System.Drawing.Point(8, 24)
        Me.Chk1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Chk1.Name = "Chk1"
        Me.Chk1.Size = New System.Drawing.Size(118, 20)
        Me.Chk1.TabIndex = 9
        Me.Chk1.Text = "Transaksi Hari Ini"
        Me.Chk1.UseVisualStyleBackColor = True
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(651, 107)
        Me.Btn_Cari.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(90, 32)
        Me.Btn_Cari.TabIndex = 8
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(546, 83)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(195, 20)
        Me.DateTimePicker2.TabIndex = 4
        '
        'Cmb_Filter_Lain
        '
        Me.Cmb_Filter_Lain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter_Lain.FormattingEnabled = True
        Me.Cmb_Filter_Lain.Location = New System.Drawing.Point(165, 112)
        Me.Cmb_Filter_Lain.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Cmb_Filter_Lain.Name = "Cmb_Filter_Lain"
        Me.Cmb_Filter_Lain.Size = New System.Drawing.Size(134, 24)
        Me.Cmb_Filter_Lain.TabIndex = 83
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(512, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "s/d"
        '
        'Chk3
        '
        Me.Chk3.AutoSize = True
        Me.Chk3.Location = New System.Drawing.Point(8, 113)
        Me.Chk3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Chk3.Name = "Chk3"
        Me.Chk3.Size = New System.Drawing.Size(107, 20)
        Me.Chk3.TabIndex = 82
        Me.Chk3.Text = "Parameter Lain"
        Me.Chk3.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(311, 84)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(195, 20)
        Me.DateTimePicker1.TabIndex = 3
        '
        'Chk2
        '
        Me.Chk2.AutoSize = True
        Me.Chk2.Location = New System.Drawing.Point(8, 85)
        Me.Chk2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Chk2.Name = "Chk2"
        Me.Chk2.Size = New System.Drawing.Size(124, 20)
        Me.Chk2.TabIndex = 1
        Me.Chk2.Text = "Parameter Tanggal"
        Me.Chk2.UseVisualStyleBackColor = True
        '
        'Cmb_FIlter_Tanggal
        '
        Me.Cmb_FIlter_Tanggal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_FIlter_Tanggal.FormattingEnabled = True
        Me.Cmb_FIlter_Tanggal.Location = New System.Drawing.Point(165, 82)
        Me.Cmb_FIlter_Tanggal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Cmb_FIlter_Tanggal.Name = "Cmb_FIlter_Tanggal"
        Me.Cmb_FIlter_Tanggal.Size = New System.Drawing.Size(134, 24)
        Me.Cmb_FIlter_Tanggal.TabIndex = 2
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(21, 720)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(2221, 12)
        Me.Panel5.TabIndex = 39
        Me.Panel5.Visible = False
        '
        'Lv_PO_Detail
        '
        Me.Lv_PO_Detail.FullRowSelect = True
        Me.Lv_PO_Detail.GridLines = True
        Me.Lv_PO_Detail.HideSelection = False
        Me.Lv_PO_Detail.Location = New System.Drawing.Point(6, 19)
        Me.Lv_PO_Detail.Name = "Lv_PO_Detail"
        Me.Lv_PO_Detail.Size = New System.Drawing.Size(563, 217)
        Me.Lv_PO_Detail.TabIndex = 0
        Me.Lv_PO_Detail.UseCompatibleStateImageBehavior = False
        Me.Lv_PO_Detail.View = System.Windows.Forms.View.Details
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(21, 688)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(2221, 12)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'Lv_DetSubPO
        '
        Me.Lv_DetSubPO.FullRowSelect = True
        Me.Lv_DetSubPO.GridLines = True
        Me.Lv_DetSubPO.HideSelection = False
        Me.Lv_DetSubPO.Location = New System.Drawing.Point(6, 17)
        Me.Lv_DetSubPO.Name = "Lv_DetSubPO"
        Me.Lv_DetSubPO.Size = New System.Drawing.Size(563, 217)
        Me.Lv_DetSubPO.TabIndex = 1
        Me.Lv_DetSubPO.UseCompatibleStateImageBehavior = False
        Me.Lv_DetSubPO.View = System.Windows.Forms.View.Details
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_PO_Detail)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 298)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(575, 242)
        Me.GroupBox2.TabIndex = 43
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Lv_DetSubPO)
        Me.GroupBox4.Location = New System.Drawing.Point(596, 299)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(575, 242)
        Me.GroupBox4.TabIndex = 43
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Detail Sub PO"
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 53)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Green
        Me.Panel7.Location = New System.Drawing.Point(898, 14)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(15, 15)
        Me.Panel7.TabIndex = 345
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(919, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 18)
        Me.Label5.TabIndex = 346
        Me.Label5.Text = "PO Telah Selesai"
        '
        'Display_Summary_POInduk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 701)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Display_Summary_POInduk"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Txt_Filter_Lain As TextBox
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Chk1 As CheckBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Cmb_Filter_Lain As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Chk3 As CheckBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Chk2 As CheckBox
    Friend WithEvents Cmb_FIlter_Tanggal As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_PO_Detail As ListView
    Friend WithEvents Chk_Status As CheckBox
    Friend WithEvents Cmb_Filter_Status As ComboBox
    Friend WithEvents Lv_PO As ListView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CetakUlangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents BatalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Lv_DetSubPO As ListView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents SelesaiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel7 As Panel
End Class
