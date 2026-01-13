<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Display_Barcode_Merge
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
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Cb_Hari_Ini = New System.Windows.Forms.CheckBox()
        Me.Txt_Param_Lain = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_Param_Lain = New System.Windows.Forms.ComboBox()
        Me.Cb_Param_Lain = New System.Windows.Forms.CheckBox()
        Me.Tgl_2 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Tgl_1 = New System.Windows.Forms.DateTimePicker()
        Me.Cb_Tanggal = New System.Windows.Forms.CheckBox()
        Me.Cmb_Tanggal = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Barcode_Merge = New System.Windows.Forms.ListView()
        Me.CM_Lv_2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CetakUlangBarcodeMergeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatalkanBarcodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lv_Parent = New System.Windows.Forms.ListView()
        Me.CM_Lv_1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SalinNoFakturToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatalkanTransaksiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Lv_Detail_Barcode_Merge = New System.Windows.Forms.ListView()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.CM_Lv_2.SuspendLayout()
        Me.CM_Lv_1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1184, 45)
        Me.Panel1.TabIndex = 88
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
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(20, 7)
        Me.Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(333, 29)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Display - Penggabungan Barcode"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 49)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 618)
        Me.Panel3.TabIndex = 281
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(-4, 44)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1355, 12)
        Me.Panel2.TabIndex = 282
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(16, 596)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1225, 15)
        Me.Panel4.TabIndex = 282
        Me.Panel4.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Btn_Cari)
        Me.GroupBox3.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBox3.Controls.Add(Me.Cb_Hari_Ini)
        Me.GroupBox3.Controls.Add(Me.Txt_Param_Lain)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Cmb_Param_Lain)
        Me.GroupBox3.Controls.Add(Me.Cb_Param_Lain)
        Me.GroupBox3.Controls.Add(Me.Tgl_2)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Tgl_1)
        Me.GroupBox3.Controls.Add(Me.Cb_Tanggal)
        Me.GroupBox3.Controls.Add(Me.Cmb_Tanggal)
        Me.GroupBox3.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox3.Location = New System.Drawing.Point(20, 451)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(632, 141)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(542, 102)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(81, 29)
        Me.Btn_Cari.TabIndex = 4
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(11, 21)
        Me.Cmb_Lokasi.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(209, 24)
        Me.Cmb_Lokasi.TabIndex = 0
        '
        'Cb_Hari_Ini
        '
        Me.Cb_Hari_Ini.AutoSize = True
        Me.Cb_Hari_Ini.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cb_Hari_Ini.Location = New System.Drawing.Point(10, 55)
        Me.Cb_Hari_Ini.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_Hari_Ini.Name = "Cb_Hari_Ini"
        Me.Cb_Hari_Ini.Size = New System.Drawing.Size(118, 20)
        Me.Cb_Hari_Ini.TabIndex = 1
        Me.Cb_Hari_Ini.Text = "Transaksi Hari Ini"
        Me.Cb_Hari_Ini.UseVisualStyleBackColor = True
        '
        'Txt_Param_Lain
        '
        Me.Txt_Param_Lain.Enabled = False
        Me.Txt_Param_Lain.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Param_Lain.Location = New System.Drawing.Point(320, 109)
        Me.Txt_Param_Lain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Param_Lain.Name = "Txt_Param_Lain"
        Me.Txt_Param_Lain.Size = New System.Drawing.Size(217, 20)
        Me.Txt_Param_Lain.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label6.Location = New System.Drawing.Point(270, 112)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Value"
        '
        'Cmb_Param_Lain
        '
        Me.Cmb_Param_Lain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Param_Lain.Enabled = False
        Me.Cmb_Param_Lain.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Param_Lain.FormattingEnabled = True
        Me.Cmb_Param_Lain.Location = New System.Drawing.Point(138, 109)
        Me.Cmb_Param_Lain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Param_Lain.Name = "Cmb_Param_Lain"
        Me.Cmb_Param_Lain.Size = New System.Drawing.Size(123, 24)
        Me.Cmb_Param_Lain.TabIndex = 7
        '
        'Cb_Param_Lain
        '
        Me.Cb_Param_Lain.AutoSize = True
        Me.Cb_Param_Lain.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cb_Param_Lain.Location = New System.Drawing.Point(10, 108)
        Me.Cb_Param_Lain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_Param_Lain.Name = "Cb_Param_Lain"
        Me.Cb_Param_Lain.Size = New System.Drawing.Size(107, 20)
        Me.Cb_Param_Lain.TabIndex = 3
        Me.Cb_Param_Lain.Text = "Parameter Lain"
        Me.Cb_Param_Lain.UseVisualStyleBackColor = True
        '
        'Tgl_2
        '
        Me.Tgl_2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl_2.Enabled = False
        Me.Tgl_2.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Tgl_2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl_2.Location = New System.Drawing.Point(465, 82)
        Me.Tgl_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Tgl_2.Name = "Tgl_2"
        Me.Tgl_2.Size = New System.Drawing.Size(158, 20)
        Me.Tgl_2.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label7.Location = New System.Drawing.Point(431, 83)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        '
        'Tgl_1
        '
        Me.Tgl_1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl_1.Enabled = False
        Me.Tgl_1.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Tgl_1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl_1.Location = New System.Drawing.Point(265, 82)
        Me.Tgl_1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Tgl_1.Name = "Tgl_1"
        Me.Tgl_1.Size = New System.Drawing.Size(158, 20)
        Me.Tgl_1.TabIndex = 4
        '
        'Cb_Tanggal
        '
        Me.Cb_Tanggal.AutoSize = True
        Me.Cb_Tanggal.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cb_Tanggal.Location = New System.Drawing.Point(10, 81)
        Me.Cb_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_Tanggal.Name = "Cb_Tanggal"
        Me.Cb_Tanggal.Size = New System.Drawing.Size(124, 20)
        Me.Cb_Tanggal.TabIndex = 2
        Me.Cb_Tanggal.Text = "Parameter Tanggal"
        Me.Cb_Tanggal.UseVisualStyleBackColor = True
        '
        'Cmb_Tanggal
        '
        Me.Cmb_Tanggal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Tanggal.Enabled = False
        Me.Cmb_Tanggal.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Tanggal.FormattingEnabled = True
        Me.Cmb_Tanggal.Location = New System.Drawing.Point(138, 79)
        Me.Cmb_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Tanggal.Name = "Cmb_Tanggal"
        Me.Cmb_Tanggal.Size = New System.Drawing.Size(123, 24)
        Me.Cmb_Tanggal.TabIndex = 3
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(16, 440)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1355, 12)
        Me.Panel5.TabIndex = 282
        Me.Panel5.Visible = False
        '
        'Lv_Barcode_Merge
        '
        Me.Lv_Barcode_Merge.ContextMenuStrip = Me.CM_Lv_2
        Me.Lv_Barcode_Merge.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Lv_Barcode_Merge.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_Barcode_Merge.FullRowSelect = True
        Me.Lv_Barcode_Merge.GridLines = True
        Me.Lv_Barcode_Merge.HideSelection = False
        Me.Lv_Barcode_Merge.Location = New System.Drawing.Point(3, 16)
        Me.Lv_Barcode_Merge.Name = "Lv_Barcode_Merge"
        Me.Lv_Barcode_Merge.Size = New System.Drawing.Size(559, 173)
        Me.Lv_Barcode_Merge.TabIndex = 354
        Me.Lv_Barcode_Merge.UseCompatibleStateImageBehavior = False
        Me.Lv_Barcode_Merge.View = System.Windows.Forms.View.Details
        '
        'CM_Lv_2
        '
        Me.CM_Lv_2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CetakUlangBarcodeMergeToolStripMenuItem, Me.BatalkanBarcodeToolStripMenuItem})
        Me.CM_Lv_2.Name = "CM_Lv_2"
        Me.CM_Lv_2.Size = New System.Drawing.Size(222, 70)
        '
        'CetakUlangBarcodeMergeToolStripMenuItem
        '
        Me.CetakUlangBarcodeMergeToolStripMenuItem.Name = "CetakUlangBarcodeMergeToolStripMenuItem"
        Me.CetakUlangBarcodeMergeToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.CetakUlangBarcodeMergeToolStripMenuItem.Text = "Cetak Ulang Barcode Merge"
        '
        'BatalkanBarcodeToolStripMenuItem
        '
        Me.BatalkanBarcodeToolStripMenuItem.Name = "BatalkanBarcodeToolStripMenuItem"
        Me.BatalkanBarcodeToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.BatalkanBarcodeToolStripMenuItem.Text = "Batalkan Barcode"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(1165, 62)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(19, 618)
        Me.Panel6.TabIndex = 281
        Me.Panel6.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1100, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 16)
        Me.Label8.TabIndex = 356
        Me.Label8.Text = "Dibatalkan"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.DarkRed
        Me.Panel9.Location = New System.Drawing.Point(1084, 51)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(12, 12)
        Me.Panel9.TabIndex = 355
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(18, 243)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1355, 12)
        Me.Panel7.TabIndex = 282
        Me.Panel7.Visible = False
        '
        'Lv_Parent
        '
        Me.Lv_Parent.ContextMenuStrip = Me.CM_Lv_1
        Me.Lv_Parent.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Lv_Parent.FullRowSelect = True
        Me.Lv_Parent.GridLines = True
        Me.Lv_Parent.HideSelection = False
        Me.Lv_Parent.Location = New System.Drawing.Point(20, 68)
        Me.Lv_Parent.Name = "Lv_Parent"
        Me.Lv_Parent.Size = New System.Drawing.Size(1144, 177)
        Me.Lv_Parent.TabIndex = 354
        Me.Lv_Parent.UseCompatibleStateImageBehavior = False
        Me.Lv_Parent.View = System.Windows.Forms.View.Details
        '
        'CM_Lv_1
        '
        Me.CM_Lv_1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalinNoFakturToolStripMenuItem, Me.BatalkanTransaksiToolStripMenuItem})
        Me.CM_Lv_1.Name = "CM_Lv_1"
        Me.CM_Lv_1.Size = New System.Drawing.Size(171, 48)
        '
        'SalinNoFakturToolStripMenuItem
        '
        Me.SalinNoFakturToolStripMenuItem.Name = "SalinNoFakturToolStripMenuItem"
        Me.SalinNoFakturToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.SalinNoFakturToolStripMenuItem.Text = "Salin No Faktur"
        '
        'BatalkanTransaksiToolStripMenuItem
        '
        Me.BatalkanTransaksiToolStripMenuItem.Name = "BatalkanTransaksiToolStripMenuItem"
        Me.BatalkanTransaksiToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.BatalkanTransaksiToolStripMenuItem.Text = "Batalkan Transaksi"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(587, 261)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(12, 618)
        Me.Panel8.TabIndex = 281
        Me.Panel8.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_Barcode_Merge)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 249)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(565, 192)
        Me.GroupBox1.TabIndex = 358
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Barcode Merge"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Detail_Barcode_Merge)
        Me.GroupBox2.Location = New System.Drawing.Point(599, 249)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(565, 192)
        Me.GroupBox2.TabIndex = 358
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail Barcode Merge"
        '
        'Lv_Detail_Barcode_Merge
        '
        Me.Lv_Detail_Barcode_Merge.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Lv_Detail_Barcode_Merge.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_Detail_Barcode_Merge.FullRowSelect = True
        Me.Lv_Detail_Barcode_Merge.GridLines = True
        Me.Lv_Detail_Barcode_Merge.HideSelection = False
        Me.Lv_Detail_Barcode_Merge.Location = New System.Drawing.Point(3, 16)
        Me.Lv_Detail_Barcode_Merge.Name = "Lv_Detail_Barcode_Merge"
        Me.Lv_Detail_Barcode_Merge.Size = New System.Drawing.Size(559, 173)
        Me.Lv_Detail_Barcode_Merge.TabIndex = 354
        Me.Lv_Detail_Barcode_Merge.UseCompatibleStateImageBehavior = False
        Me.Lv_Detail_Barcode_Merge.View = System.Windows.Forms.View.Details
        '
        'N_EMI_Display_Barcode_Merge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 611)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Lv_Parent)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Display_Barcode_Merge"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.CM_Lv_2.ResumeLayout(False)
        Me.CM_Lv_1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Cb_Hari_Ini As CheckBox
    Friend WithEvents Txt_Param_Lain As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_Param_Lain As ComboBox
    Friend WithEvents Cb_Param_Lain As CheckBox
    Friend WithEvents Tgl_2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents Tgl_1 As DateTimePicker
    Friend WithEvents Cb_Tanggal As CheckBox
    Friend WithEvents Cmb_Tanggal As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_Barcode_Merge As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_Parent As ListView
    Friend WithEvents CM_Lv_1 As ContextMenuStrip
    Friend WithEvents SalinNoFakturToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BatalkanTransaksiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel8 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Lv_Detail_Barcode_Merge As ListView
    Friend WithEvents CM_Lv_2 As ContextMenuStrip
    Friend WithEvents CetakUlangBarcodeMergeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BatalkanBarcodeToolStripMenuItem As ToolStripMenuItem
End Class
