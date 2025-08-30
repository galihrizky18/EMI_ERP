<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Display_Split_Stock
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Lv_Parent = New System.Windows.Forms.ListView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtnMasuk_Cari = New System.Windows.Forms.Button()
        Me.Cmb_1 = New System.Windows.Forms.ComboBox()
        Me.Chk_1 = New System.Windows.Forms.CheckBox()
        Me.Txt_ParamLainValue = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_3 = New System.Windows.Forms.ComboBox()
        Me.Chk_3 = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Chk_2 = New System.Windows.Forms.CheckBox()
        Me.Cmb_2 = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Child = New System.Windows.Forms.ListView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Lv_DetailPallet = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CetakToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Barcode = New System.Windows.Forms.PictureBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SalinNoTransaksiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatalSplitStockToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1054, 45)
        Me.Panel1.TabIndex = 27
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 8)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(216, 29)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Display - Split Stock"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 46)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 37
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 65)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 38
        Me.Panel3.Visible = False
        '
        'Lv_Parent
        '
        Me.Lv_Parent.ContextMenuStrip = Me.ContextMenuStrip2
        Me.Lv_Parent.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_Parent.FullRowSelect = True
        Me.Lv_Parent.GridLines = True
        Me.Lv_Parent.HideSelection = False
        Me.Lv_Parent.Location = New System.Drawing.Point(20, 79)
        Me.Lv_Parent.Name = "Lv_Parent"
        Me.Lv_Parent.Size = New System.Drawing.Size(1016, 170)
        Me.Lv_Parent.TabIndex = 1
        Me.Lv_Parent.UseCompatibleStateImageBehavior = False
        Me.Lv_Parent.View = System.Windows.Forms.View.Details
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1037, 46)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 601)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BtnMasuk_Cari)
        Me.GroupBox1.Controls.Add(Me.Cmb_1)
        Me.GroupBox1.Controls.Add(Me.Chk_1)
        Me.GroupBox1.Controls.Add(Me.Txt_ParamLainValue)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Cmb_3)
        Me.GroupBox1.Controls.Add(Me.Chk_3)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Chk_2)
        Me.GroupBox1.Controls.Add(Me.Cmb_2)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 465)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(700, 131)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter Data"
        '
        'BtnMasuk_Cari
        '
        Me.BtnMasuk_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnMasuk_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMasuk_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnMasuk_Cari.Location = New System.Drawing.Point(605, 101)
        Me.BtnMasuk_Cari.Name = "BtnMasuk_Cari"
        Me.BtnMasuk_Cari.Size = New System.Drawing.Size(81, 27)
        Me.BtnMasuk_Cari.TabIndex = 9
        Me.BtnMasuk_Cari.Text = "&Cari"
        Me.BtnMasuk_Cari.UseVisualStyleBackColor = False
        '
        'Cmb_1
        '
        Me.Cmb_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_1.FormattingEnabled = True
        Me.Cmb_1.Location = New System.Drawing.Point(8, 21)
        Me.Cmb_1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_1.Name = "Cmb_1"
        Me.Cmb_1.Size = New System.Drawing.Size(209, 24)
        Me.Cmb_1.TabIndex = 0
        '
        'Chk_1
        '
        Me.Chk_1.AutoSize = True
        Me.Chk_1.Location = New System.Drawing.Point(8, 50)
        Me.Chk_1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_1.Name = "Chk_1"
        Me.Chk_1.Size = New System.Drawing.Size(118, 20)
        Me.Chk_1.TabIndex = 1
        Me.Chk_1.Text = "Transaksi Hari Ini"
        Me.Chk_1.UseVisualStyleBackColor = True
        '
        'Txt_ParamLainValue
        '
        Me.Txt_ParamLainValue.Enabled = False
        Me.Txt_ParamLainValue.Location = New System.Drawing.Point(329, 104)
        Me.Txt_ParamLainValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_ParamLainValue.Name = "Txt_ParamLainValue"
        Me.Txt_ParamLainValue.Size = New System.Drawing.Size(271, 20)
        Me.Txt_ParamLainValue.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(283, 105)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Value"
        '
        'Cmb_3
        '
        Me.Cmb_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_3.Enabled = False
        Me.Cmb_3.FormattingEnabled = True
        Me.Cmb_3.Location = New System.Drawing.Point(141, 100)
        Me.Cmb_3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_3.Name = "Cmb_3"
        Me.Cmb_3.Size = New System.Drawing.Size(137, 24)
        Me.Cmb_3.TabIndex = 7
        '
        'Chk_3
        '
        Me.Chk_3.AutoSize = True
        Me.Chk_3.Location = New System.Drawing.Point(8, 100)
        Me.Chk_3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_3.Name = "Chk_3"
        Me.Chk_3.Size = New System.Drawing.Size(107, 20)
        Me.Chk_3.TabIndex = 6
        Me.Chk_3.Text = "Parameter Lain"
        Me.Chk_3.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Enabled = False
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(505, 74)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(177, 20)
        Me.DateTimePicker2.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(470, 77)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(284, 74)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(177, 20)
        Me.DateTimePicker1.TabIndex = 4
        '
        'Chk_2
        '
        Me.Chk_2.AutoSize = True
        Me.Chk_2.Location = New System.Drawing.Point(8, 74)
        Me.Chk_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_2.Name = "Chk_2"
        Me.Chk_2.Size = New System.Drawing.Size(124, 20)
        Me.Chk_2.TabIndex = 2
        Me.Chk_2.Text = "Parameter Tanggal"
        Me.Chk_2.UseVisualStyleBackColor = True
        '
        'Cmb_2
        '
        Me.Cmb_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_2.Enabled = False
        Me.Cmb_2.FormattingEnabled = True
        Me.Cmb_2.Location = New System.Drawing.Point(141, 72)
        Me.Cmb_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_2.Name = "Cmb_2"
        Me.Cmb_2.Size = New System.Drawing.Size(137, 24)
        Me.Cmb_2.TabIndex = 3
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(12, 595)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1338, 15)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Lv_Child
        '
        Me.Lv_Child.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_Child.FullRowSelect = True
        Me.Lv_Child.GridLines = True
        Me.Lv_Child.HideSelection = False
        Me.Lv_Child.Location = New System.Drawing.Point(7, 22)
        Me.Lv_Child.Name = "Lv_Child"
        Me.Lv_Child.Size = New System.Drawing.Size(488, 162)
        Me.Lv_Child.TabIndex = 0
        Me.Lv_Child.UseCompatibleStateImageBehavior = False
        Me.Lv_Child.View = System.Windows.Forms.View.Details
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Child)
        Me.GroupBox2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 260)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(500, 190)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Lv_DetailPallet)
        Me.GroupBox3.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox3.Location = New System.Drawing.Point(536, 260)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(500, 190)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Detail Pallet"
        '
        'Lv_DetailPallet
        '
        Me.Lv_DetailPallet.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_DetailPallet.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_DetailPallet.FullRowSelect = True
        Me.Lv_DetailPallet.GridLines = True
        Me.Lv_DetailPallet.HideSelection = False
        Me.Lv_DetailPallet.Location = New System.Drawing.Point(7, 22)
        Me.Lv_DetailPallet.Name = "Lv_DetailPallet"
        Me.Lv_DetailPallet.Size = New System.Drawing.Size(488, 162)
        Me.Lv_DetailPallet.TabIndex = 0
        Me.Lv_DetailPallet.UseCompatibleStateImageBehavior = False
        Me.Lv_DetailPallet.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CetakToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(151, 26)
        '
        'CetakToolStripMenuItem
        '
        Me.CetakToolStripMenuItem.Name = "CetakToolStripMenuItem"
        Me.CetakToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.CetakToolStripMenuItem.Text = "Cetak Barcode"
        '
        'Barcode
        '
        Me.Barcode.Location = New System.Drawing.Point(951, 556)
        Me.Barcode.Name = "Barcode"
        Me.Barcode.Size = New System.Drawing.Size(79, 32)
        Me.Barcode.TabIndex = 480
        Me.Barcode.TabStop = False
        Me.Barcode.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(11, 451)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1251, 12)
        Me.Panel6.TabIndex = 37
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(523, 182)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(12, 601)
        Me.Panel7.TabIndex = 38
        Me.Panel7.Visible = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(25, 248)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(942, 12)
        Me.Panel8.TabIndex = 37
        Me.Panel8.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(960, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 18)
        Me.Label8.TabIndex = 482
        Me.Label8.Text = "Dibatalkan"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.DarkRed
        Me.Panel9.Location = New System.Drawing.Point(941, 55)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(15, 15)
        Me.Panel9.TabIndex = 481
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalinNoTransaksiToolStripMenuItem, Me.BatalSplitStockToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(175, 48)
        '
        'SalinNoTransaksiToolStripMenuItem
        '
        Me.SalinNoTransaksiToolStripMenuItem.Name = "SalinNoTransaksiToolStripMenuItem"
        Me.SalinNoTransaksiToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.SalinNoTransaksiToolStripMenuItem.Text = "Salin Kode Transfer"
        '
        'BatalSplitStockToolStripMenuItem
        '
        Me.BatalSplitStockToolStripMenuItem.Name = "BatalSplitStockToolStripMenuItem"
        Me.BatalSplitStockToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.BatalSplitStockToolStripMenuItem.Text = "Batal Split Stock"
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1054, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Display_Split_Stock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1054, 611)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Barcode)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Lv_Parent)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "EMI_Display_Split_Stock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Lv_Parent As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Cmb_1 As ComboBox
    Friend WithEvents Chk_1 As CheckBox
    Friend WithEvents Txt_ParamLainValue As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_3 As ComboBox
    Friend WithEvents Chk_3 As CheckBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Chk_2 As CheckBox
    Friend WithEvents Cmb_2 As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_Child As ListView
    Friend WithEvents BtnMasuk_Cari As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Lv_DetailPallet As ListView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CetakToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Barcode As PictureBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents SalinNoTransaksiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BatalSplitStockToolStripMenuItem As ToolStripMenuItem
End Class
