<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Emi_Display_Transfer_Stock
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Lv_Stock = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SalinNoFakturToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CetakUlangFakturToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Lv_Stock_Detail = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CetakUlangBarcodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.LvwAkhir = New System.Windows.Forms.ListView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.LvwAwal = New System.Windows.Forms.ListView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtnBarangMasuk_Cari = New System.Windows.Forms.Button()
        Me.CmbSO_Asal = New System.Windows.Forms.ComboBox()
        Me.Chk_Transaksi_HariIni = New System.Windows.Forms.CheckBox()
        Me.Txt_ParamLain = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_Filter_ParamLain = New System.Windows.Forms.ComboBox()
        Me.Chk_Param_Lain = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Chk_Tanggal = New System.Windows.Forms.CheckBox()
        Me.Cmb_FIlterTanggal = New System.Windows.Forms.ComboBox()
        Me.Barcode = New System.Windows.Forms.PictureBox()
        Me.BatalTransferStockToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(1184, 44)
        Me.Panel1.TabIndex = 26
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(21, 7)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(239, 28)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Display - Transfer Stock"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(8, 44)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 36
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 72)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 37
        Me.Panel3.Visible = False
        '
        'Lv_Stock
        '
        Me.Lv_Stock.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_Stock.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_Stock.FullRowSelect = True
        Me.Lv_Stock.GridLines = True
        Me.Lv_Stock.HideSelection = False
        Me.Lv_Stock.Location = New System.Drawing.Point(18, 56)
        Me.Lv_Stock.Name = "Lv_Stock"
        Me.Lv_Stock.Size = New System.Drawing.Size(1146, 189)
        Me.Lv_Stock.TabIndex = 0
        Me.Lv_Stock.UseCompatibleStateImageBehavior = False
        Me.Lv_Stock.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalinNoFakturToolStripMenuItem, Me.CetakUlangFakturToolStripMenuItem, Me.BatalTransferStockToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(178, 70)
        '
        'SalinNoFakturToolStripMenuItem
        '
        Me.SalinNoFakturToolStripMenuItem.Name = "SalinNoFakturToolStripMenuItem"
        Me.SalinNoFakturToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.SalinNoFakturToolStripMenuItem.Text = "Salin No Faktur"
        '
        'CetakUlangFakturToolStripMenuItem
        '
        Me.CetakUlangFakturToolStripMenuItem.Name = "CetakUlangFakturToolStripMenuItem"
        Me.CetakUlangFakturToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.CetakUlangFakturToolStripMenuItem.Text = "Cetak Ulang Faktur"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1165, 66)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 601)
        Me.Panel4.TabIndex = 37
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(46, 242)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(1291, 214)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1055, 208)
        Me.GroupBox2.TabIndex = 239
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail Transfer Stock"
        '
        'Lv_Stock_Detail
        '
        Me.Lv_Stock_Detail.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_Stock_Detail.FullRowSelect = True
        Me.Lv_Stock_Detail.GridLines = True
        Me.Lv_Stock_Detail.HideSelection = False
        Me.Lv_Stock_Detail.Location = New System.Drawing.Point(4, 3)
        Me.Lv_Stock_Detail.Name = "Lv_Stock_Detail"
        Me.Lv_Stock_Detail.Size = New System.Drawing.Size(1126, 154)
        Me.Lv_Stock_Detail.TabIndex = 234
        Me.Lv_Stock_Detail.UseCompatibleStateImageBehavior = False
        Me.Lv_Stock_Detail.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CetakUlangBarcodeToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(185, 26)
        '
        'CetakUlangBarcodeToolStripMenuItem
        '
        Me.CetakUlangBarcodeToolStripMenuItem.Name = "CetakUlangBarcodeToolStripMenuItem"
        Me.CetakUlangBarcodeToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.CetakUlangBarcodeToolStripMenuItem.Text = "Cetak Ulang Barcode"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(19, 446)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(942, 12)
        Me.Panel6.TabIndex = 36
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(20, 596)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 15)
        Me.Panel7.TabIndex = 36
        Me.Panel7.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(20, 254)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1144, 189)
        Me.TabControl1.TabIndex = 241
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Lv_Stock_Detail)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1136, 160)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Detail Barang"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1136, 160)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Lokasi"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.LvwAkhir)
        Me.GroupBox4.Location = New System.Drawing.Point(571, 5)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(565, 159)
        Me.GroupBox4.TabIndex = 238
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Lokasi Tujuan"
        '
        'LvwAkhir
        '
        Me.LvwAkhir.ContextMenuStrip = Me.ContextMenuStrip2
        Me.LvwAkhir.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.LvwAkhir.FullRowSelect = True
        Me.LvwAkhir.GridLines = True
        Me.LvwAkhir.HideSelection = False
        Me.LvwAkhir.Location = New System.Drawing.Point(3, 19)
        Me.LvwAkhir.Name = "LvwAkhir"
        Me.LvwAkhir.Size = New System.Drawing.Size(557, 124)
        Me.LvwAkhir.TabIndex = 0
        Me.LvwAkhir.UseCompatibleStateImageBehavior = False
        Me.LvwAkhir.View = System.Windows.Forms.View.Details
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.LvwAwal)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(565, 159)
        Me.GroupBox3.TabIndex = 237
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Lokasi Awal"
        '
        'LvwAwal
        '
        Me.LvwAwal.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.LvwAwal.FullRowSelect = True
        Me.LvwAwal.GridLines = True
        Me.LvwAwal.HideSelection = False
        Me.LvwAwal.Location = New System.Drawing.Point(2, 19)
        Me.LvwAwal.Name = "LvwAwal"
        Me.LvwAwal.Size = New System.Drawing.Size(557, 124)
        Me.LvwAwal.TabIndex = 0
        Me.LvwAwal.UseCompatibleStateImageBehavior = False
        Me.LvwAwal.View = System.Windows.Forms.View.Details
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BtnBarangMasuk_Cari)
        Me.GroupBox1.Controls.Add(Me.CmbSO_Asal)
        Me.GroupBox1.Controls.Add(Me.Chk_Transaksi_HariIni)
        Me.GroupBox1.Controls.Add(Me.Txt_ParamLain)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Cmb_Filter_ParamLain)
        Me.GroupBox1.Controls.Add(Me.Chk_Param_Lain)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Chk_Tanggal)
        Me.GroupBox1.Controls.Add(Me.Cmb_FIlterTanggal)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 458)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(670, 141)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter Data"
        '
        'BtnBarangMasuk_Cari
        '
        Me.BtnBarangMasuk_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnBarangMasuk_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBarangMasuk_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnBarangMasuk_Cari.Location = New System.Drawing.Point(562, 100)
        Me.BtnBarangMasuk_Cari.Name = "BtnBarangMasuk_Cari"
        Me.BtnBarangMasuk_Cari.Size = New System.Drawing.Size(81, 27)
        Me.BtnBarangMasuk_Cari.TabIndex = 9
        Me.BtnBarangMasuk_Cari.Text = "&Cari"
        Me.BtnBarangMasuk_Cari.UseVisualStyleBackColor = False
        '
        'CmbSO_Asal
        '
        Me.CmbSO_Asal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSO_Asal.FormattingEnabled = True
        Me.CmbSO_Asal.Location = New System.Drawing.Point(8, 21)
        Me.CmbSO_Asal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CmbSO_Asal.Name = "CmbSO_Asal"
        Me.CmbSO_Asal.Size = New System.Drawing.Size(209, 24)
        Me.CmbSO_Asal.TabIndex = 0
        '
        'Chk_Transaksi_HariIni
        '
        Me.Chk_Transaksi_HariIni.AutoSize = True
        Me.Chk_Transaksi_HariIni.Location = New System.Drawing.Point(8, 48)
        Me.Chk_Transaksi_HariIni.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_Transaksi_HariIni.Name = "Chk_Transaksi_HariIni"
        Me.Chk_Transaksi_HariIni.Size = New System.Drawing.Size(118, 20)
        Me.Chk_Transaksi_HariIni.TabIndex = 1
        Me.Chk_Transaksi_HariIni.Text = "Transaksi Hari Ini"
        Me.Chk_Transaksi_HariIni.UseVisualStyleBackColor = True
        '
        'Txt_ParamLain
        '
        Me.Txt_ParamLain.Location = New System.Drawing.Point(349, 105)
        Me.Txt_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_ParamLain.Name = "Txt_ParamLain"
        Me.Txt_ParamLain.Size = New System.Drawing.Size(208, 20)
        Me.Txt_ParamLain.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(310, 106)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Value"
        '
        'Cmb_Filter_ParamLain
        '
        Me.Cmb_Filter_ParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter_ParamLain.FormattingEnabled = True
        Me.Cmb_Filter_ParamLain.Location = New System.Drawing.Point(143, 102)
        Me.Cmb_Filter_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Filter_ParamLain.Name = "Cmb_Filter_ParamLain"
        Me.Cmb_Filter_ParamLain.Size = New System.Drawing.Size(159, 24)
        Me.Cmb_Filter_ParamLain.TabIndex = 7
        '
        'Chk_Param_Lain
        '
        Me.Chk_Param_Lain.AutoSize = True
        Me.Chk_Param_Lain.Location = New System.Drawing.Point(8, 102)
        Me.Chk_Param_Lain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_Param_Lain.Name = "Chk_Param_Lain"
        Me.Chk_Param_Lain.Size = New System.Drawing.Size(107, 20)
        Me.Chk_Param_Lain.TabIndex = 6
        Me.Chk_Param_Lain.Text = "Parameter Lain"
        Me.Chk_Param_Lain.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(496, 76)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(143, 20)
        Me.DateTimePicker2.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(464, 78)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(313, 76)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(143, 20)
        Me.DateTimePicker1.TabIndex = 4
        '
        'Chk_Tanggal
        '
        Me.Chk_Tanggal.AutoSize = True
        Me.Chk_Tanggal.Location = New System.Drawing.Point(8, 74)
        Me.Chk_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_Tanggal.Name = "Chk_Tanggal"
        Me.Chk_Tanggal.Size = New System.Drawing.Size(124, 20)
        Me.Chk_Tanggal.TabIndex = 2
        Me.Chk_Tanggal.Text = "Parameter Tanggal"
        Me.Chk_Tanggal.UseVisualStyleBackColor = True
        '
        'Cmb_FIlterTanggal
        '
        Me.Cmb_FIlterTanggal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_FIlterTanggal.FormattingEnabled = True
        Me.Cmb_FIlterTanggal.Location = New System.Drawing.Point(143, 72)
        Me.Cmb_FIlterTanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_FIlterTanggal.Name = "Cmb_FIlterTanggal"
        Me.Cmb_FIlterTanggal.Size = New System.Drawing.Size(159, 24)
        Me.Cmb_FIlterTanggal.TabIndex = 3
        '
        'Barcode
        '
        Me.Barcode.Location = New System.Drawing.Point(1291, 159)
        Me.Barcode.Name = "Barcode"
        Me.Barcode.Size = New System.Drawing.Size(79, 32)
        Me.Barcode.TabIndex = 480
        Me.Barcode.TabStop = False
        Me.Barcode.Visible = False
        '
        'BatalTransferStockToolStripMenuItem
        '
        Me.BatalTransferStockToolStripMenuItem.Name = "BatalTransferStockToolStripMenuItem"
        Me.BatalTransferStockToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.BatalTransferStockToolStripMenuItem.Text = "Batal Transfer Stock"
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 42)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Emi_Display_Transfer_Stock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 611)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Barcode)
        Me.Controls.Add(Me.Lv_Stock)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Display_Transfer_Stock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Lv_Stock As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Lv_Stock_Detail As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CetakUlangFakturToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents LvwAwal As ListView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BtnBarangMasuk_Cari As Button
    Friend WithEvents CmbSO_Asal As ComboBox
    Friend WithEvents Chk_Transaksi_HariIni As CheckBox
    Friend WithEvents Txt_ParamLain As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_Filter_ParamLain As ComboBox
    Friend WithEvents Chk_Param_Lain As CheckBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Chk_Tanggal As CheckBox
    Friend WithEvents Cmb_FIlterTanggal As ComboBox
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents CetakUlangBarcodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Barcode As PictureBox
    Friend WithEvents LvwAkhir As ListView
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents SalinNoFakturToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BatalTransferStockToolStripMenuItem As ToolStripMenuItem
End Class
