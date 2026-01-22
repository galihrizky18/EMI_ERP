<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Display_Retur_Packaging
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_Parent = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SalinNoTransaksiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatalkanTransaksiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CetakBarcodeScrapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
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
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Btn_Show_RM = New System.Windows.Forms.Button()
        Me.Txt_Detail_Jumlah_RM = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Lv_Detail_RM = New System.Windows.Forms.ListView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Detail_Jumlah_Retur_convert = New System.Windows.Forms.TextBox()
        Me.Txt_Detail_Jumlah_Retur = New System.Windows.Forms.TextBox()
        Me.Txt_Detail_Kd_Barang_Tujuan = New System.Windows.Forms.TextBox()
        Me.Txt_Detail_Kd_Barang_Awal = New System.Windows.Forms.TextBox()
        Me.Cmb_Detail_Satuan_Retur_Convert = New System.Windows.Forms.ComboBox()
        Me.Txt_Detail_Nm_Barang_Tujuan = New System.Windows.Forms.TextBox()
        Me.Cmb_Detail_Satuan_Retur = New System.Windows.Forms.ComboBox()
        Me.Txt_Detail_Nm_Barang_Awal = New System.Windows.Forms.TextBox()
        Me.Txt_Detail_Lokasi = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1184, 43)
        Me.Panel1.TabIndex = 25
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 41)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(295, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display - Packaging Waste"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(-5, 42)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1355, 12)
        Me.Panel2.TabIndex = 283
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 58)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 618)
        Me.Panel3.TabIndex = 284
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(16, 596)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1355, 15)
        Me.Panel4.TabIndex = 283
        Me.Panel4.Visible = False
        '
        'Lv_Parent
        '
        Me.Lv_Parent.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_Parent.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Lv_Parent.FullRowSelect = True
        Me.Lv_Parent.GridLines = True
        Me.Lv_Parent.HideSelection = False
        Me.Lv_Parent.Location = New System.Drawing.Point(20, 67)
        Me.Lv_Parent.Name = "Lv_Parent"
        Me.Lv_Parent.OwnerDraw = True
        Me.Lv_Parent.Size = New System.Drawing.Size(1144, 224)
        Me.Lv_Parent.TabIndex = 355
        Me.Lv_Parent.UseCompatibleStateImageBehavior = False
        Me.Lv_Parent.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalinNoTransaksiToolStripMenuItem, Me.BatalkanTransaksiToolStripMenuItem, Me.CetakBarcodeScrapToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(217, 70)
        '
        'SalinNoTransaksiToolStripMenuItem
        '
        Me.SalinNoTransaksiToolStripMenuItem.Name = "SalinNoTransaksiToolStripMenuItem"
        Me.SalinNoTransaksiToolStripMenuItem.Size = New System.Drawing.Size(216, 22)
        Me.SalinNoTransaksiToolStripMenuItem.Text = "Salin No Transaksi"
        '
        'BatalkanTransaksiToolStripMenuItem
        '
        Me.BatalkanTransaksiToolStripMenuItem.Name = "BatalkanTransaksiToolStripMenuItem"
        Me.BatalkanTransaksiToolStripMenuItem.Size = New System.Drawing.Size(216, 22)
        Me.BatalkanTransaksiToolStripMenuItem.Text = "Batalkan Transaksi"
        '
        'CetakBarcodeScrapToolStripMenuItem
        '
        Me.CetakBarcodeScrapToolStripMenuItem.Name = "CetakBarcodeScrapToolStripMenuItem"
        Me.CetakBarcodeScrapToolStripMenuItem.Size = New System.Drawing.Size(216, 22)
        Me.CetakBarcodeScrapToolStripMenuItem.Text = "Cetak Ulang Barcode Scrap"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1165, 58)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 618)
        Me.Panel5.TabIndex = 284
        Me.Panel5.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Btn_Cari)
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
        Me.GroupBox3.Location = New System.Drawing.Point(20, 483)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(632, 111)
        Me.GroupBox3.TabIndex = 356
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(542, 73)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(81, 29)
        Me.Btn_Cari.TabIndex = 4
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Cb_Hari_Ini
        '
        Me.Cb_Hari_Ini.AutoSize = True
        Me.Cb_Hari_Ini.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cb_Hari_Ini.Location = New System.Drawing.Point(10, 26)
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
        Me.Txt_Param_Lain.Location = New System.Drawing.Point(320, 80)
        Me.Txt_Param_Lain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Param_Lain.Name = "Txt_Param_Lain"
        Me.Txt_Param_Lain.Size = New System.Drawing.Size(217, 20)
        Me.Txt_Param_Lain.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label6.Location = New System.Drawing.Point(270, 83)
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
        Me.Cmb_Param_Lain.Location = New System.Drawing.Point(138, 80)
        Me.Cmb_Param_Lain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Param_Lain.Name = "Cmb_Param_Lain"
        Me.Cmb_Param_Lain.Size = New System.Drawing.Size(123, 24)
        Me.Cmb_Param_Lain.TabIndex = 7
        '
        'Cb_Param_Lain
        '
        Me.Cb_Param_Lain.AutoSize = True
        Me.Cb_Param_Lain.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cb_Param_Lain.Location = New System.Drawing.Point(10, 79)
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
        Me.Tgl_2.Location = New System.Drawing.Point(465, 53)
        Me.Tgl_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Tgl_2.Name = "Tgl_2"
        Me.Tgl_2.Size = New System.Drawing.Size(158, 20)
        Me.Tgl_2.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label7.Location = New System.Drawing.Point(431, 54)
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
        Me.Tgl_1.Location = New System.Drawing.Point(265, 53)
        Me.Tgl_1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Tgl_1.Name = "Tgl_1"
        Me.Tgl_1.Size = New System.Drawing.Size(158, 20)
        Me.Tgl_1.TabIndex = 4
        '
        'Cb_Tanggal
        '
        Me.Cb_Tanggal.AutoSize = True
        Me.Cb_Tanggal.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cb_Tanggal.Location = New System.Drawing.Point(10, 52)
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
        Me.Cmb_Tanggal.Location = New System.Drawing.Point(138, 50)
        Me.Cmb_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Tanggal.Name = "Cmb_Tanggal"
        Me.Cmb_Tanggal.Size = New System.Drawing.Size(123, 24)
        Me.Cmb_Tanggal.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1098, 47)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 16)
        Me.Label8.TabIndex = 358
        Me.Label8.Text = "Dibatalkan"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.DarkRed
        Me.Panel9.Location = New System.Drawing.Point(1082, 49)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(12, 12)
        Me.Panel9.TabIndex = 357
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(25, 291)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1355, 12)
        Me.Panel6.TabIndex = 283
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(14, 470)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1355, 12)
        Me.Panel7.TabIndex = 283
        Me.Panel7.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Btn_Show_RM)
        Me.GroupBox1.Controls.Add(Me.Txt_Detail_Jumlah_RM)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Lv_Detail_RM)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_Detail_Jumlah_Retur_convert)
        Me.GroupBox1.Controls.Add(Me.Txt_Detail_Jumlah_Retur)
        Me.GroupBox1.Controls.Add(Me.Txt_Detail_Kd_Barang_Tujuan)
        Me.GroupBox1.Controls.Add(Me.Txt_Detail_Kd_Barang_Awal)
        Me.GroupBox1.Controls.Add(Me.Cmb_Detail_Satuan_Retur_Convert)
        Me.GroupBox1.Controls.Add(Me.Txt_Detail_Nm_Barang_Tujuan)
        Me.GroupBox1.Controls.Add(Me.Cmb_Detail_Satuan_Retur)
        Me.GroupBox1.Controls.Add(Me.Txt_Detail_Nm_Barang_Awal)
        Me.GroupBox1.Controls.Add(Me.Txt_Detail_Lokasi)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 304)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1144, 167)
        Me.GroupBox1.TabIndex = 359
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detail Retur"
        '
        'Btn_Show_RM
        '
        Me.Btn_Show_RM.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Show_RM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Show_RM.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Show_RM.ForeColor = System.Drawing.Color.White
        Me.Btn_Show_RM.Location = New System.Drawing.Point(869, 24)
        Me.Btn_Show_RM.Name = "Btn_Show_RM"
        Me.Btn_Show_RM.Size = New System.Drawing.Size(166, 29)
        Me.Btn_Show_RM.TabIndex = 4
        Me.Btn_Show_RM.Text = "&Show Request Material"
        Me.Btn_Show_RM.UseVisualStyleBackColor = False
        Me.Btn_Show_RM.Visible = False
        '
        'Txt_Detail_Jumlah_RM
        '
        Me.Txt_Detail_Jumlah_RM.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Txt_Detail_Jumlah_RM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Detail_Jumlah_RM.Enabled = False
        Me.Txt_Detail_Jumlah_RM.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Detail_Jumlah_RM.Location = New System.Drawing.Point(647, 29)
        Me.Txt_Detail_Jumlah_RM.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Detail_Jumlah_RM.Name = "Txt_Detail_Jumlah_RM"
        Me.Txt_Detail_Jumlah_RM.Size = New System.Drawing.Size(217, 20)
        Me.Txt_Detail_Jumlah_RM.TabIndex = 8
        Me.Txt_Detail_Jumlah_RM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(529, 29)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(103, 17)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Jumlah Request"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(11, 133)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 17)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Jumlah Convert"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(11, 107)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 17)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Jumlah Retur"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(11, 81)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Barang Tujuan"
        '
        'Lv_Detail_RM
        '
        Me.Lv_Detail_RM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Lv_Detail_RM.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_Detail_RM.FullRowSelect = True
        Me.Lv_Detail_RM.GridLines = True
        Me.Lv_Detail_RM.HideSelection = False
        Me.Lv_Detail_RM.Location = New System.Drawing.Point(531, 55)
        Me.Lv_Detail_RM.Name = "Lv_Detail_RM"
        Me.Lv_Detail_RM.OwnerDraw = True
        Me.Lv_Detail_RM.Size = New System.Drawing.Size(607, 100)
        Me.Lv_Detail_RM.TabIndex = 355
        Me.Lv_Detail_RM.UseCompatibleStateImageBehavior = False
        Me.Lv_Detail_RM.View = System.Windows.Forms.View.Details
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(11, 55)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Barang Awal"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(11, 29)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 17)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Lokasi"
        '
        'Txt_Detail_Jumlah_Retur_convert
        '
        Me.Txt_Detail_Jumlah_Retur_convert.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Txt_Detail_Jumlah_Retur_convert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Detail_Jumlah_Retur_convert.Enabled = False
        Me.Txt_Detail_Jumlah_Retur_convert.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Detail_Jumlah_Retur_convert.Location = New System.Drawing.Point(129, 133)
        Me.Txt_Detail_Jumlah_Retur_convert.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Detail_Jumlah_Retur_convert.Name = "Txt_Detail_Jumlah_Retur_convert"
        Me.Txt_Detail_Jumlah_Retur_convert.Size = New System.Drawing.Size(231, 20)
        Me.Txt_Detail_Jumlah_Retur_convert.TabIndex = 8
        Me.Txt_Detail_Jumlah_Retur_convert.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_Detail_Jumlah_Retur
        '
        Me.Txt_Detail_Jumlah_Retur.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Txt_Detail_Jumlah_Retur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Detail_Jumlah_Retur.Enabled = False
        Me.Txt_Detail_Jumlah_Retur.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Detail_Jumlah_Retur.Location = New System.Drawing.Point(129, 107)
        Me.Txt_Detail_Jumlah_Retur.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Detail_Jumlah_Retur.Name = "Txt_Detail_Jumlah_Retur"
        Me.Txt_Detail_Jumlah_Retur.Size = New System.Drawing.Size(231, 20)
        Me.Txt_Detail_Jumlah_Retur.TabIndex = 8
        Me.Txt_Detail_Jumlah_Retur.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_Detail_Kd_Barang_Tujuan
        '
        Me.Txt_Detail_Kd_Barang_Tujuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Txt_Detail_Kd_Barang_Tujuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Detail_Kd_Barang_Tujuan.Enabled = False
        Me.Txt_Detail_Kd_Barang_Tujuan.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Detail_Kd_Barang_Tujuan.Location = New System.Drawing.Point(129, 81)
        Me.Txt_Detail_Kd_Barang_Tujuan.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Detail_Kd_Barang_Tujuan.Name = "Txt_Detail_Kd_Barang_Tujuan"
        Me.Txt_Detail_Kd_Barang_Tujuan.Size = New System.Drawing.Size(101, 20)
        Me.Txt_Detail_Kd_Barang_Tujuan.TabIndex = 8
        '
        'Txt_Detail_Kd_Barang_Awal
        '
        Me.Txt_Detail_Kd_Barang_Awal.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Txt_Detail_Kd_Barang_Awal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Detail_Kd_Barang_Awal.Enabled = False
        Me.Txt_Detail_Kd_Barang_Awal.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Detail_Kd_Barang_Awal.Location = New System.Drawing.Point(129, 55)
        Me.Txt_Detail_Kd_Barang_Awal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Detail_Kd_Barang_Awal.Name = "Txt_Detail_Kd_Barang_Awal"
        Me.Txt_Detail_Kd_Barang_Awal.Size = New System.Drawing.Size(101, 20)
        Me.Txt_Detail_Kd_Barang_Awal.TabIndex = 8
        '
        'Cmb_Detail_Satuan_Retur_Convert
        '
        Me.Cmb_Detail_Satuan_Retur_Convert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Detail_Satuan_Retur_Convert.Enabled = False
        Me.Cmb_Detail_Satuan_Retur_Convert.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Detail_Satuan_Retur_Convert.FormattingEnabled = True
        Me.Cmb_Detail_Satuan_Retur_Convert.Location = New System.Drawing.Point(364, 131)
        Me.Cmb_Detail_Satuan_Retur_Convert.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Detail_Satuan_Retur_Convert.Name = "Cmb_Detail_Satuan_Retur_Convert"
        Me.Cmb_Detail_Satuan_Retur_Convert.Size = New System.Drawing.Size(123, 24)
        Me.Cmb_Detail_Satuan_Retur_Convert.TabIndex = 3
        '
        'Txt_Detail_Nm_Barang_Tujuan
        '
        Me.Txt_Detail_Nm_Barang_Tujuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Txt_Detail_Nm_Barang_Tujuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Detail_Nm_Barang_Tujuan.Enabled = False
        Me.Txt_Detail_Nm_Barang_Tujuan.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Detail_Nm_Barang_Tujuan.Location = New System.Drawing.Point(234, 81)
        Me.Txt_Detail_Nm_Barang_Tujuan.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Detail_Nm_Barang_Tujuan.Name = "Txt_Detail_Nm_Barang_Tujuan"
        Me.Txt_Detail_Nm_Barang_Tujuan.Size = New System.Drawing.Size(253, 20)
        Me.Txt_Detail_Nm_Barang_Tujuan.TabIndex = 8
        '
        'Cmb_Detail_Satuan_Retur
        '
        Me.Cmb_Detail_Satuan_Retur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Detail_Satuan_Retur.Enabled = False
        Me.Cmb_Detail_Satuan_Retur.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Detail_Satuan_Retur.FormattingEnabled = True
        Me.Cmb_Detail_Satuan_Retur.Location = New System.Drawing.Point(364, 105)
        Me.Cmb_Detail_Satuan_Retur.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Detail_Satuan_Retur.Name = "Cmb_Detail_Satuan_Retur"
        Me.Cmb_Detail_Satuan_Retur.Size = New System.Drawing.Size(123, 24)
        Me.Cmb_Detail_Satuan_Retur.TabIndex = 3
        '
        'Txt_Detail_Nm_Barang_Awal
        '
        Me.Txt_Detail_Nm_Barang_Awal.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Txt_Detail_Nm_Barang_Awal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Detail_Nm_Barang_Awal.Enabled = False
        Me.Txt_Detail_Nm_Barang_Awal.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Detail_Nm_Barang_Awal.Location = New System.Drawing.Point(234, 55)
        Me.Txt_Detail_Nm_Barang_Awal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Detail_Nm_Barang_Awal.Name = "Txt_Detail_Nm_Barang_Awal"
        Me.Txt_Detail_Nm_Barang_Awal.Size = New System.Drawing.Size(253, 20)
        Me.Txt_Detail_Nm_Barang_Awal.TabIndex = 8
        '
        'Txt_Detail_Lokasi
        '
        Me.Txt_Detail_Lokasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Txt_Detail_Lokasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Detail_Lokasi.Enabled = False
        Me.Txt_Detail_Lokasi.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Detail_Lokasi.Location = New System.Drawing.Point(129, 29)
        Me.Txt_Detail_Lokasi.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Detail_Lokasi.Name = "Txt_Detail_Lokasi"
        Me.Txt_Detail_Lokasi.Size = New System.Drawing.Size(358, 20)
        Me.Txt_Detail_Lokasi.TabIndex = 8
        Me.Txt_Detail_Lokasi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'N_EMI_Display_Retur_Packaging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 611)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Lv_Parent)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Display_Retur_Packaging"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_Parent As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Btn_Cari As Button
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
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Detail_Lokasi As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_Detail_Jumlah_RM As TextBox
    Friend WithEvents Txt_Detail_Jumlah_Retur As TextBox
    Friend WithEvents Txt_Detail_Kd_Barang_Tujuan As TextBox
    Friend WithEvents Txt_Detail_Kd_Barang_Awal As TextBox
    Friend WithEvents Lv_Detail_RM As ListView
    Friend WithEvents Btn_Show_RM As Button
    Friend WithEvents Txt_Detail_Nm_Barang_Tujuan As TextBox
    Friend WithEvents Cmb_Detail_Satuan_Retur As ComboBox
    Friend WithEvents Txt_Detail_Nm_Barang_Awal As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Txt_Detail_Jumlah_Retur_convert As TextBox
    Friend WithEvents Cmb_Detail_Satuan_Retur_Convert As ComboBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SalinNoTransaksiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BatalkanTransaksiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CetakBarcodeScrapToolStripMenuItem As ToolStripMenuItem
End Class
