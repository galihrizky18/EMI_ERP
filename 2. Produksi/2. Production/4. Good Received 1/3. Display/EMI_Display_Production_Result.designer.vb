<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Display_Production_Result
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
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Lv_ProductionResult = New System.Windows.Forms.ListView()
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.CopyNoTransaksiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.CetakToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.LaporanGIGRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.LaporanGIGRDetailToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.BatalkanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Lv_DetailFinishedGood = New System.Windows.Forms.ListView()
		Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.CetakUlangBarcodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.CetakUlangBarcodeQCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.GroupBox3 = New System.Windows.Forms.GroupBox()
		Me.Btn_Cari = New System.Windows.Forms.Button()
		Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
		Me.Cb_TransaksiHrIni = New System.Windows.Forms.CheckBox()
		Me.Txt_ParamValue = New System.Windows.Forms.TextBox()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Cmb_ParamLain = New System.Windows.Forms.ComboBox()
		Me.Cb_ParamLain = New System.Windows.Forms.CheckBox()
		Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
		Me.Cb_ParamTgl = New System.Windows.Forms.CheckBox()
		Me.Cmb_ParamTgl = New System.Windows.Forms.ComboBox()
		Me.TabControl1 = New System.Windows.Forms.TabControl()
		Me.TabFinishedGood = New System.Windows.Forms.TabPage()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.Lv_DetailPackaging = New System.Windows.Forms.ListView()
		Me.TabScrap = New System.Windows.Forms.TabPage()
		Me.Lv_DetailScrap = New System.Windows.Forms.ListView()
		Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.CetakUlangBarcodeFGToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.Lv_DetailRawMaterial = New System.Windows.Forms.ListView()
		Me.Barcode = New System.Windows.Forms.PictureBox()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Panel1.SuspendLayout()
		Me.ContextMenuStrip1.SuspendLayout()
		Me.ContextMenuStrip2.SuspendLayout()
		Me.GroupBox3.SuspendLayout()
		Me.TabControl1.SuspendLayout()
		Me.TabFinishedGood.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.TabScrap.SuspendLayout()
		Me.ContextMenuStrip3.SuspendLayout()
		CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
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
		Me.Panel1.Size = New System.Drawing.Size(1051, 45)
		Me.Panel1.TabIndex = 22
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold)
		Me.Label1.Location = New System.Drawing.Point(20, 7)
		Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(268, 29)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Display Production Result"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(0, 45)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1052, 12)
		Me.Panel2.TabIndex = 34
		Me.Panel2.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(1, 63)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 687)
		Me.Panel3.TabIndex = 35
		Me.Panel3.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(1033, 63)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(19, 694)
		Me.Panel5.TabIndex = 37
		Me.Panel5.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(20, 451)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(1436, 12)
		Me.Panel4.TabIndex = 38
		Me.Panel4.Visible = False
		'
		'Lv_ProductionResult
		'
		Me.Lv_ProductionResult.ContextMenuStrip = Me.ContextMenuStrip1
		Me.Lv_ProductionResult.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_ProductionResult.FullRowSelect = True
		Me.Lv_ProductionResult.GridLines = True
		Me.Lv_ProductionResult.HideSelection = False
		Me.Lv_ProductionResult.Location = New System.Drawing.Point(21, 55)
		Me.Lv_ProductionResult.Name = "Lv_ProductionResult"
		Me.Lv_ProductionResult.Size = New System.Drawing.Size(1012, 172)
		Me.Lv_ProductionResult.TabIndex = 234
		Me.Lv_ProductionResult.UseCompatibleStateImageBehavior = False
		Me.Lv_ProductionResult.View = System.Windows.Forms.View.Details
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyNoTransaksiToolStripMenuItem, Me.CetakToolStripMenuItem, Me.BatalkanToolStripMenuItem})
		Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
		Me.ContextMenuStrip1.Size = New System.Drawing.Size(217, 70)
		'
		'CopyNoTransaksiToolStripMenuItem
		'
		Me.CopyNoTransaksiToolStripMenuItem.Name = "CopyNoTransaksiToolStripMenuItem"
		Me.CopyNoTransaksiToolStripMenuItem.Size = New System.Drawing.Size(216, 22)
		Me.CopyNoTransaksiToolStripMenuItem.Text = "Copy No Transaksi"
		'
		'CetakToolStripMenuItem
		'
		Me.CetakToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LaporanGIGRToolStripMenuItem, Me.LaporanGIGRDetailToolStripMenuItem})
		Me.CetakToolStripMenuItem.Name = "CetakToolStripMenuItem"
		Me.CetakToolStripMenuItem.Size = New System.Drawing.Size(216, 22)
		Me.CetakToolStripMenuItem.Text = "Cetak"
		'
		'LaporanGIGRToolStripMenuItem
		'
		Me.LaporanGIGRToolStripMenuItem.Name = "LaporanGIGRToolStripMenuItem"
		Me.LaporanGIGRToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
		Me.LaporanGIGRToolStripMenuItem.Text = "Laporan GI & GR"
		'
		'LaporanGIGRDetailToolStripMenuItem
		'
		Me.LaporanGIGRDetailToolStripMenuItem.Name = "LaporanGIGRDetailToolStripMenuItem"
		Me.LaporanGIGRDetailToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
		Me.LaporanGIGRDetailToolStripMenuItem.Text = "Laporan Detail GI & GR"
		'
		'BatalkanToolStripMenuItem
		'
		Me.BatalkanToolStripMenuItem.Name = "BatalkanToolStripMenuItem"
		Me.BatalkanToolStripMenuItem.Size = New System.Drawing.Size(216, 22)
		Me.BatalkanToolStripMenuItem.Text = "Batalkan Production Result"
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(0, 597)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(1456, 15)
		Me.Panel6.TabIndex = 39
		Me.Panel6.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(21, 228)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1436, 12)
		Me.Panel7.TabIndex = 39
		Me.Panel7.Visible = False
		'
		'Lv_DetailFinishedGood
		'
		Me.Lv_DetailFinishedGood.ContextMenuStrip = Me.ContextMenuStrip2
		Me.Lv_DetailFinishedGood.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_DetailFinishedGood.FullRowSelect = True
		Me.Lv_DetailFinishedGood.GridLines = True
		Me.Lv_DetailFinishedGood.HideSelection = False
		Me.Lv_DetailFinishedGood.Location = New System.Drawing.Point(6, 6)
		Me.Lv_DetailFinishedGood.Name = "Lv_DetailFinishedGood"
		Me.Lv_DetailFinishedGood.Size = New System.Drawing.Size(995, 175)
		Me.Lv_DetailFinishedGood.TabIndex = 341
		Me.Lv_DetailFinishedGood.UseCompatibleStateImageBehavior = False
		Me.Lv_DetailFinishedGood.View = System.Windows.Forms.View.Details
		'
		'ContextMenuStrip2
		'
		Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CetakUlangBarcodeToolStripMenuItem, Me.CetakUlangBarcodeQCToolStripMenuItem})
		Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
		Me.ContextMenuStrip2.Size = New System.Drawing.Size(205, 48)
		'
		'CetakUlangBarcodeToolStripMenuItem
		'
		Me.CetakUlangBarcodeToolStripMenuItem.Name = "CetakUlangBarcodeToolStripMenuItem"
		Me.CetakUlangBarcodeToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
		Me.CetakUlangBarcodeToolStripMenuItem.Text = "Cetak Ulang Barcode FG"
		'
		'CetakUlangBarcodeQCToolStripMenuItem
		'
		Me.CetakUlangBarcodeQCToolStripMenuItem.Name = "CetakUlangBarcodeQCToolStripMenuItem"
		Me.CetakUlangBarcodeQCToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
		Me.CetakUlangBarcodeQCToolStripMenuItem.Text = "Cetak Ulang Barcode QC"
		'
		'GroupBox3
		'
		Me.GroupBox3.Controls.Add(Me.Btn_Cari)
		Me.GroupBox3.Controls.Add(Me.Cmb_Lokasi)
		Me.GroupBox3.Controls.Add(Me.Cb_TransaksiHrIni)
		Me.GroupBox3.Controls.Add(Me.Txt_ParamValue)
		Me.GroupBox3.Controls.Add(Me.Label6)
		Me.GroupBox3.Controls.Add(Me.Cmb_ParamLain)
		Me.GroupBox3.Controls.Add(Me.Cb_ParamLain)
		Me.GroupBox3.Controls.Add(Me.DateTimePicker2)
		Me.GroupBox3.Controls.Add(Me.Label7)
		Me.GroupBox3.Controls.Add(Me.DateTimePicker1)
		Me.GroupBox3.Controls.Add(Me.Cb_ParamTgl)
		Me.GroupBox3.Controls.Add(Me.Cmb_ParamTgl)
		Me.GroupBox3.Location = New System.Drawing.Point(21, 465)
		Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.GroupBox3.Name = "GroupBox3"
		Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.GroupBox3.Size = New System.Drawing.Size(643, 125)
		Me.GroupBox3.TabIndex = 342
		Me.GroupBox3.TabStop = False
		Me.GroupBox3.Text = "Filter Data"
		'
		'Btn_Cari
		'
		Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Btn_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari.Location = New System.Drawing.Point(523, 91)
		Me.Btn_Cari.Name = "Btn_Cari"
		Me.Btn_Cari.Size = New System.Drawing.Size(108, 27)
		Me.Btn_Cari.TabIndex = 343
		Me.Btn_Cari.Text = "&Cari"
		Me.Btn_Cari.UseVisualStyleBackColor = False
		'
		'Cmb_Lokasi
		'
		Me.Cmb_Lokasi.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Lokasi.Enabled = False
		Me.Cmb_Lokasi.FormattingEnabled = True
		Me.Cmb_Lokasi.Location = New System.Drawing.Point(8, 16)
		Me.Cmb_Lokasi.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
		Me.Cmb_Lokasi.Size = New System.Drawing.Size(232, 24)
		Me.Cmb_Lokasi.TabIndex = 342
		'
		'Cb_TransaksiHrIni
		'
		Me.Cb_TransaksiHrIni.AutoSize = True
		Me.Cb_TransaksiHrIni.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cb_TransaksiHrIni.Location = New System.Drawing.Point(8, 44)
		Me.Cb_TransaksiHrIni.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Cb_TransaksiHrIni.Name = "Cb_TransaksiHrIni"
		Me.Cb_TransaksiHrIni.Size = New System.Drawing.Size(118, 20)
		Me.Cb_TransaksiHrIni.TabIndex = 9
		Me.Cb_TransaksiHrIni.Text = "Transaksi Hari Ini"
		Me.Cb_TransaksiHrIni.UseVisualStyleBackColor = True
		'
		'Txt_ParamValue
		'
		Me.Txt_ParamValue.Location = New System.Drawing.Point(283, 95)
		Me.Txt_ParamValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Txt_ParamValue.Name = "Txt_ParamValue"
		Me.Txt_ParamValue.Size = New System.Drawing.Size(235, 20)
		Me.Txt_ParamValue.TabIndex = 7
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(244, 97)
		Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(35, 16)
		Me.Label6.TabIndex = 8
		Me.Label6.Text = "Value"
		'
		'Cmb_ParamLain
		'
		Me.Cmb_ParamLain.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_ParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_ParamLain.FormattingEnabled = True
		Me.Cmb_ParamLain.Location = New System.Drawing.Point(136, 93)
		Me.Cmb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Cmb_ParamLain.Name = "Cmb_ParamLain"
		Me.Cmb_ParamLain.Size = New System.Drawing.Size(104, 24)
		Me.Cmb_ParamLain.TabIndex = 6
		'
		'Cb_ParamLain
		'
		Me.Cb_ParamLain.AutoSize = True
		Me.Cb_ParamLain.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cb_ParamLain.Location = New System.Drawing.Point(8, 95)
		Me.Cb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Cb_ParamLain.Name = "Cb_ParamLain"
		Me.Cb_ParamLain.Size = New System.Drawing.Size(107, 20)
		Me.Cb_ParamLain.TabIndex = 5
		Me.Cb_ParamLain.Text = "Parameter Lain"
		Me.Cb_ParamLain.UseVisualStyleBackColor = True
		'
		'DateTimePicker2
		'
		Me.DateTimePicker2.Cursor = System.Windows.Forms.Cursors.Hand
		Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
		Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.DateTimePicker2.Location = New System.Drawing.Point(475, 66)
		Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.DateTimePicker2.Name = "DateTimePicker2"
		Me.DateTimePicker2.Size = New System.Drawing.Size(156, 20)
		Me.DateTimePicker2.TabIndex = 4
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(446, 68)
		Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(25, 16)
		Me.Label7.TabIndex = 4
		Me.Label7.Text = "s/d"
		'
		'DateTimePicker1
		'
		Me.DateTimePicker1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
		Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.DateTimePicker1.Location = New System.Drawing.Point(284, 67)
		Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.DateTimePicker1.Name = "DateTimePicker1"
		Me.DateTimePicker1.Size = New System.Drawing.Size(158, 20)
		Me.DateTimePicker1.TabIndex = 3
		'
		'Cb_ParamTgl
		'
		Me.Cb_ParamTgl.AutoSize = True
		Me.Cb_ParamTgl.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cb_ParamTgl.Location = New System.Drawing.Point(8, 67)
		Me.Cb_ParamTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Cb_ParamTgl.Name = "Cb_ParamTgl"
		Me.Cb_ParamTgl.Size = New System.Drawing.Size(124, 20)
		Me.Cb_ParamTgl.TabIndex = 1
		Me.Cb_ParamTgl.Text = "Parameter Tanggal"
		Me.Cb_ParamTgl.UseVisualStyleBackColor = True
		'
		'Cmb_ParamTgl
		'
		Me.Cmb_ParamTgl.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_ParamTgl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_ParamTgl.FormattingEnabled = True
		Me.Cmb_ParamTgl.Location = New System.Drawing.Point(136, 65)
		Me.Cmb_ParamTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Cmb_ParamTgl.Name = "Cmb_ParamTgl"
		Me.Cmb_ParamTgl.Size = New System.Drawing.Size(104, 24)
		Me.Cmb_ParamTgl.TabIndex = 2
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.TabFinishedGood)
		Me.TabControl1.Controls.Add(Me.TabPage1)
		Me.TabControl1.Controls.Add(Me.TabScrap)
		Me.TabControl1.Location = New System.Drawing.Point(21, 240)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(1012, 215)
		Me.TabControl1.TabIndex = 343
		'
		'TabFinishedGood
		'
		Me.TabFinishedGood.Controls.Add(Me.Lv_DetailFinishedGood)
		Me.TabFinishedGood.Location = New System.Drawing.Point(4, 25)
		Me.TabFinishedGood.Name = "TabFinishedGood"
		Me.TabFinishedGood.Padding = New System.Windows.Forms.Padding(3)
		Me.TabFinishedGood.Size = New System.Drawing.Size(1004, 186)
		Me.TabFinishedGood.TabIndex = 0
		Me.TabFinishedGood.Text = "Finished Good"
		Me.TabFinishedGood.UseVisualStyleBackColor = True
		'
		'TabPage1
		'
		Me.TabPage1.Controls.Add(Me.Lv_DetailPackaging)
		Me.TabPage1.Location = New System.Drawing.Point(4, 25)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage1.Size = New System.Drawing.Size(1004, 186)
		Me.TabPage1.TabIndex = 2
		Me.TabPage1.Text = "Packaging"
		Me.TabPage1.UseVisualStyleBackColor = True
		'
		'Lv_DetailPackaging
		'
		Me.Lv_DetailPackaging.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_DetailPackaging.FullRowSelect = True
		Me.Lv_DetailPackaging.GridLines = True
		Me.Lv_DetailPackaging.HideSelection = False
		Me.Lv_DetailPackaging.Location = New System.Drawing.Point(6, 6)
		Me.Lv_DetailPackaging.Name = "Lv_DetailPackaging"
		Me.Lv_DetailPackaging.Size = New System.Drawing.Size(995, 175)
		Me.Lv_DetailPackaging.TabIndex = 343
		Me.Lv_DetailPackaging.UseCompatibleStateImageBehavior = False
		Me.Lv_DetailPackaging.View = System.Windows.Forms.View.Details
		'
		'TabScrap
		'
		Me.TabScrap.Controls.Add(Me.Lv_DetailScrap)
		Me.TabScrap.Location = New System.Drawing.Point(4, 25)
		Me.TabScrap.Name = "TabScrap"
		Me.TabScrap.Size = New System.Drawing.Size(1004, 186)
		Me.TabScrap.TabIndex = 3
		Me.TabScrap.Text = "Scrap"
		Me.TabScrap.UseVisualStyleBackColor = True
		'
		'Lv_DetailScrap
		'
		Me.Lv_DetailScrap.ContextMenuStrip = Me.ContextMenuStrip3
		Me.Lv_DetailScrap.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_DetailScrap.FullRowSelect = True
		Me.Lv_DetailScrap.GridLines = True
		Me.Lv_DetailScrap.HideSelection = False
		Me.Lv_DetailScrap.Location = New System.Drawing.Point(6, 6)
		Me.Lv_DetailScrap.Name = "Lv_DetailScrap"
		Me.Lv_DetailScrap.Size = New System.Drawing.Size(995, 175)
		Me.Lv_DetailScrap.TabIndex = 344
		Me.Lv_DetailScrap.UseCompatibleStateImageBehavior = False
		Me.Lv_DetailScrap.View = System.Windows.Forms.View.Details
		'
		'ContextMenuStrip3
		'
		Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CetakUlangBarcodeFGToolStripMenuItem})
		Me.ContextMenuStrip3.Name = "ContextMenuStrip3"
		Me.ContextMenuStrip3.Size = New System.Drawing.Size(202, 26)
		'
		'CetakUlangBarcodeFGToolStripMenuItem
		'
		Me.CetakUlangBarcodeFGToolStripMenuItem.Name = "CetakUlangBarcodeFGToolStripMenuItem"
		Me.CetakUlangBarcodeFGToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
		Me.CetakUlangBarcodeFGToolStripMenuItem.Text = "Cetak Ulang Barcode FG"
		'
		'Lv_DetailRawMaterial
		'
		Me.Lv_DetailRawMaterial.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_DetailRawMaterial.FullRowSelect = True
		Me.Lv_DetailRawMaterial.GridLines = True
		Me.Lv_DetailRawMaterial.HideSelection = False
		Me.Lv_DetailRawMaterial.Location = New System.Drawing.Point(29, 619)
		Me.Lv_DetailRawMaterial.Name = "Lv_DetailRawMaterial"
		Me.Lv_DetailRawMaterial.Size = New System.Drawing.Size(995, 175)
		Me.Lv_DetailRawMaterial.TabIndex = 342
		Me.Lv_DetailRawMaterial.UseCompatibleStateImageBehavior = False
		Me.Lv_DetailRawMaterial.View = System.Windows.Forms.View.Details
		Me.Lv_DetailRawMaterial.Visible = False
		'
		'Barcode
		'
		Me.Barcode.Location = New System.Drawing.Point(1054, 55)
		Me.Barcode.Name = "Barcode"
		Me.Barcode.Size = New System.Drawing.Size(100, 50)
		Me.Barcode.TabIndex = 378
		Me.Barcode.TabStop = False
		Me.Barcode.Visible = False
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
		Me.PanelGradient1.Size = New System.Drawing.Size(1051, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'EMI_Display_Production_Result
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1051, 610)
		Me.Controls.Add(Me.Lv_DetailRawMaterial)
		Me.Controls.Add(Me.Barcode)
		Me.Controls.Add(Me.TabControl1)
		Me.Controls.Add(Me.GroupBox3)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Lv_ProductionResult)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "EMI_Display_Production_Result"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ContextMenuStrip1.ResumeLayout(False)
		Me.ContextMenuStrip2.ResumeLayout(False)
		Me.GroupBox3.ResumeLayout(False)
		Me.GroupBox3.PerformLayout()
		Me.TabControl1.ResumeLayout(False)
		Me.TabFinishedGood.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabScrap.ResumeLayout(False)
		Me.ContextMenuStrip3.ResumeLayout(False)
		CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_ProductionResult As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_DetailFinishedGood As ListView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Cb_TransaksiHrIni As CheckBox
    Friend WithEvents Txt_ParamValue As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_ParamLain As ComboBox
    Friend WithEvents Cb_ParamLain As CheckBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Cb_ParamTgl As CheckBox
    Friend WithEvents Cmb_ParamTgl As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabFinishedGood As TabPage
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Lv_DetailRawMaterial As ListView
    Friend WithEvents Lv_DetailPackaging As ListView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CopyNoTransaksiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabScrap As TabPage
    Friend WithEvents Lv_DetailScrap As ListView
    Friend WithEvents CetakToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LaporanGIGRToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LaporanGIGRDetailToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents CetakUlangBarcodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Barcode As PictureBox
    Friend WithEvents CetakUlangBarcodeQCToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BatalkanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip3 As ContextMenuStrip
    Friend WithEvents CetakUlangBarcodeFGToolStripMenuItem As ToolStripMenuItem
End Class
