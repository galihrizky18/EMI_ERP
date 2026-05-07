<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_Production_Order
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
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Cmb_Lain = New System.Windows.Forms.ComboBox()
		Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
		Me.Txt_No_Faktur = New System.Windows.Forms.TextBox()
		Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
		Me.Txt_Lain = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
		Me.BtnCetak = New System.Windows.Forms.Button()
		Me.BtnExit = New System.Windows.Forms.Button()
		Me.Cmb_Tanggal = New System.Windows.Forms.ComboBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Txt_No_Formula = New System.Windows.Forms.TextBox()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Cmb_Status_PO = New System.Windows.Forms.ComboBox()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.Cmb_Status_Split = New System.Windows.Forms.ComboBox()
		Me.Panel18 = New System.Windows.Forms.Panel()
		Me.Panel19 = New System.Windows.Forms.Panel()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Label12 = New System.Windows.Forms.Label()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.Panel9 = New System.Windows.Forms.Panel()
		Me.Panel10 = New System.Windows.Forms.Panel()
		Me.Label13 = New System.Windows.Forms.Label()
		Me.Panel11 = New System.Windows.Forms.Panel()
		Me.Panel12 = New System.Windows.Forms.Panel()
		Me.Lv_NoFaktur = New System.Windows.Forms.ListView()
		Me.Lv_Formula = New System.Windows.Forms.ListView()
		Me.Lv_Barang = New System.Windows.Forms.ListView()
		Me.Panel1.SuspendLayout()
		Me.Panel18.SuspendLayout()
		Me.Panel19.SuspendLayout()
		Me.Panel2.SuspendLayout()
		Me.Panel4.SuspendLayout()
		Me.Panel6.SuspendLayout()
		Me.Panel7.SuspendLayout()
		Me.Panel8.SuspendLayout()
		Me.Panel9.SuspendLayout()
		Me.Panel10.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(582, 45)
		Me.Panel1.TabIndex = 91
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
		Me.PanelGradient1.Size = New System.Drawing.Size(582, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label11.Location = New System.Drawing.Point(20, 7)
		Me.Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(268, 29)
		Me.Label11.TabIndex = 0
		Me.Label11.Text = "Laporan Production Order"
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(-2, 45)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(942, 12)
		Me.Panel5.TabIndex = 92
		Me.Panel5.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 65)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 601)
		Me.Panel3.TabIndex = 93
		Me.Panel3.Visible = False
		'
		'Cmb_Lain
		'
		Me.Cmb_Lain.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Lain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Lain.FormattingEnabled = True
		Me.Cmb_Lain.Location = New System.Drawing.Point(111, 45)
		Me.Cmb_Lain.Name = "Cmb_Lain"
		Me.Cmb_Lain.Size = New System.Drawing.Size(131, 24)
		Me.Cmb_Lain.TabIndex = 2
		'
		'Cmb_Lokasi
		'
		Me.Cmb_Lokasi.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Lokasi.FormattingEnabled = True
		Me.Cmb_Lokasi.Location = New System.Drawing.Point(111, 13)
		Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
		Me.Cmb_Lokasi.Size = New System.Drawing.Size(163, 24)
		Me.Cmb_Lokasi.TabIndex = 2
		'
		'Txt_No_Faktur
		'
		Me.Txt_No_Faktur.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Txt_No_Faktur.Location = New System.Drawing.Point(111, 43)
		Me.Txt_No_Faktur.Name = "Txt_No_Faktur"
		Me.Txt_No_Faktur.Size = New System.Drawing.Size(163, 20)
		Me.Txt_No_Faktur.TabIndex = 3
		'
		'Txt_KdBarang
		'
		Me.Txt_KdBarang.Location = New System.Drawing.Point(111, 19)
		Me.Txt_KdBarang.Name = "Txt_KdBarang"
		Me.Txt_KdBarang.Size = New System.Drawing.Size(131, 20)
		Me.Txt_KdBarang.TabIndex = 4
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(4, 46)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(60, 16)
		Me.Label1.TabIndex = 4
		Me.Label1.Text = "No Faktur"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(5, 52)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(49, 16)
		Me.Label5.TabIndex = 4
		Me.Label5.Text = "Lainnya"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(5, 16)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(42, 16)
		Me.Label4.TabIndex = 4
		Me.Label4.Text = "Lokasi"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(5, 22)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(43, 16)
		Me.Label6.TabIndex = 4
		Me.Label6.Text = "Barang"
		'
		'Tgl2
		'
		Me.Tgl2.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Tgl2.CustomFormat = "dd MMMM yyyy"
		Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Tgl2.Location = New System.Drawing.Point(280, 43)
		Me.Tgl2.Name = "Tgl2"
		Me.Tgl2.Size = New System.Drawing.Size(163, 20)
		Me.Tgl2.TabIndex = 1
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(7, 43)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(90, 16)
		Me.Label3.TabIndex = 2
		Me.Label3.Text = "Sampai Dengan"
		'
		'Tgl1
		'
		Me.Tgl1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Tgl1.CustomFormat = "dd MMMM yyyy"
		Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Tgl1.Location = New System.Drawing.Point(111, 43)
		Me.Tgl1.Name = "Tgl1"
		Me.Tgl1.Size = New System.Drawing.Size(163, 20)
		Me.Tgl1.TabIndex = 0
		'
		'Txt_Lain
		'
		Me.Txt_Lain.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Lain.Enabled = False
		Me.Txt_Lain.Location = New System.Drawing.Point(248, 49)
		Me.Txt_Lain.Name = "Txt_Lain"
		Me.Txt_Lain.Size = New System.Drawing.Size(282, 20)
		Me.Txt_Lain.TabIndex = 6
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(7, 16)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(47, 16)
		Me.Label2.TabIndex = 0
		Me.Label2.Text = "Periode"
		'
		'Txt_NmBarang
		'
		Me.Txt_NmBarang.Location = New System.Drawing.Point(248, 19)
		Me.Txt_NmBarang.Name = "Txt_NmBarang"
		Me.Txt_NmBarang.Size = New System.Drawing.Size(282, 20)
		Me.Txt_NmBarang.TabIndex = 6
		'
		'BtnCetak
		'
		Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
		Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.BtnCetak.ForeColor = System.Drawing.Color.White
		Me.BtnCetak.Location = New System.Drawing.Point(349, 419)
		Me.BtnCetak.Name = "BtnCetak"
		Me.BtnCetak.Size = New System.Drawing.Size(105, 33)
		Me.BtnCetak.TabIndex = 96
		Me.BtnCetak.Text = "&Cetak"
		Me.BtnCetak.UseVisualStyleBackColor = False
		'
		'BtnExit
		'
		Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
		Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.BtnExit.ForeColor = System.Drawing.Color.White
		Me.BtnExit.Location = New System.Drawing.Point(458, 419)
		Me.BtnExit.Name = "BtnExit"
		Me.BtnExit.Size = New System.Drawing.Size(105, 33)
		Me.BtnExit.TabIndex = 97
		Me.BtnExit.Text = "&Keluar"
		Me.BtnExit.UseVisualStyleBackColor = False
		'
		'Cmb_Tanggal
		'
		Me.Cmb_Tanggal.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Tanggal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Tanggal.FormattingEnabled = True
		Me.Cmb_Tanggal.Location = New System.Drawing.Point(111, 13)
		Me.Cmb_Tanggal.Name = "Cmb_Tanggal"
		Me.Cmb_Tanggal.Size = New System.Drawing.Size(332, 24)
		Me.Cmb_Tanggal.TabIndex = 2
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(4, 72)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(69, 16)
		Me.Label7.TabIndex = 4
		Me.Label7.Text = "No Formula"
		'
		'Txt_No_Formula
		'
		Me.Txt_No_Formula.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Txt_No_Formula.Location = New System.Drawing.Point(111, 69)
		Me.Txt_No_Formula.Name = "Txt_No_Formula"
		Me.Txt_No_Formula.Size = New System.Drawing.Size(163, 20)
		Me.Txt_No_Formula.TabIndex = 3
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(5, 98)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(62, 16)
		Me.Label8.TabIndex = 4
		Me.Label8.Text = "Status PO"
		'
		'Cmb_Status_PO
		'
		Me.Cmb_Status_PO.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Status_PO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Status_PO.FormattingEnabled = True
		Me.Cmb_Status_PO.Location = New System.Drawing.Point(111, 95)
		Me.Cmb_Status_PO.Name = "Cmb_Status_PO"
		Me.Cmb_Status_PO.Size = New System.Drawing.Size(163, 24)
		Me.Cmb_Status_PO.TabIndex = 2
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Location = New System.Drawing.Point(284, 99)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(72, 16)
		Me.Label9.TabIndex = 4
		Me.Label9.Text = "Status Split"
		'
		'Cmb_Status_Split
		'
		Me.Cmb_Status_Split.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Status_Split.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Status_Split.FormattingEnabled = True
		Me.Cmb_Status_Split.Location = New System.Drawing.Point(367, 95)
		Me.Cmb_Status_Split.Name = "Cmb_Status_Split"
		Me.Cmb_Status_Split.Size = New System.Drawing.Size(163, 24)
		Me.Cmb_Status_Split.TabIndex = 2
		'
		'Panel18
		'
		Me.Panel18.BackColor = System.Drawing.Color.Gainsboro
		Me.Panel18.Controls.Add(Me.Panel19)
		Me.Panel18.Location = New System.Drawing.Point(20, 69)
		Me.Panel18.Name = "Panel18"
		Me.Panel18.Padding = New System.Windows.Forms.Padding(2)
		Me.Panel18.Size = New System.Drawing.Size(543, 80)
		Me.Panel18.TabIndex = 459
		'
		'Panel19
		'
		Me.Panel19.BackColor = System.Drawing.Color.White
		Me.Panel19.Controls.Add(Me.Cmb_Tanggal)
		Me.Panel19.Controls.Add(Me.Label2)
		Me.Panel19.Controls.Add(Me.Tgl2)
		Me.Panel19.Controls.Add(Me.Tgl1)
		Me.Panel19.Controls.Add(Me.Label3)
		Me.Panel19.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel19.Location = New System.Drawing.Point(2, 2)
		Me.Panel19.Name = "Panel19"
		Me.Panel19.Size = New System.Drawing.Size(539, 76)
		Me.Panel19.TabIndex = 0
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.White
		Me.Panel2.Controls.Add(Me.Label10)
		Me.Panel2.Location = New System.Drawing.Point(27, 58)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(118, 21)
		Me.Panel2.TabIndex = 460
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label10.Font = New System.Drawing.Font("Work Sans SemiBold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label10.Location = New System.Drawing.Point(0, 0)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(119, 18)
		Me.Label10.TabIndex = 0
		Me.Label10.Text = "Parameter Waktu"
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Gainsboro
		Me.Panel4.Controls.Add(Me.Panel6)
		Me.Panel4.Location = New System.Drawing.Point(20, 325)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Padding = New System.Windows.Forms.Padding(2)
		Me.Panel4.Size = New System.Drawing.Size(543, 88)
		Me.Panel4.TabIndex = 459
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.White
		Me.Panel6.Controls.Add(Me.Cmb_Lain)
		Me.Panel6.Controls.Add(Me.Txt_KdBarang)
		Me.Panel6.Controls.Add(Me.Label6)
		Me.Panel6.Controls.Add(Me.Txt_NmBarang)
		Me.Panel6.Controls.Add(Me.Txt_Lain)
		Me.Panel6.Controls.Add(Me.Label5)
		Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel6.Location = New System.Drawing.Point(2, 2)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(539, 84)
		Me.Panel6.TabIndex = 0
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.White
		Me.Panel7.Controls.Add(Me.Label12)
		Me.Panel7.Location = New System.Drawing.Point(27, 314)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(175, 21)
		Me.Panel7.TabIndex = 460
		'
		'Label12
		'
		Me.Label12.AutoSize = True
		Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label12.Font = New System.Drawing.Font("Work Sans SemiBold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label12.Location = New System.Drawing.Point(0, 0)
		Me.Label12.Name = "Label12"
		Me.Label12.Size = New System.Drawing.Size(172, 18)
		Me.Label12.TabIndex = 0
		Me.Label12.Text = "Filter Produk dan Lainnya"
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.Gainsboro
		Me.Panel8.Controls.Add(Me.Panel9)
		Me.Panel8.Location = New System.Drawing.Point(20, 169)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Padding = New System.Windows.Forms.Padding(2)
		Me.Panel8.Size = New System.Drawing.Size(543, 136)
		Me.Panel8.TabIndex = 459
		'
		'Panel9
		'
		Me.Panel9.BackColor = System.Drawing.Color.White
		Me.Panel9.Controls.Add(Me.Label4)
		Me.Panel9.Controls.Add(Me.Cmb_Lokasi)
		Me.Panel9.Controls.Add(Me.Label7)
		Me.Panel9.Controls.Add(Me.Cmb_Status_Split)
		Me.Panel9.Controls.Add(Me.Label1)
		Me.Panel9.Controls.Add(Me.Txt_No_Faktur)
		Me.Panel9.Controls.Add(Me.Txt_No_Formula)
		Me.Panel9.Controls.Add(Me.Cmb_Status_PO)
		Me.Panel9.Controls.Add(Me.Label8)
		Me.Panel9.Controls.Add(Me.Label9)
		Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel9.Location = New System.Drawing.Point(2, 2)
		Me.Panel9.Name = "Panel9"
		Me.Panel9.Size = New System.Drawing.Size(539, 132)
		Me.Panel9.TabIndex = 0
		'
		'Panel10
		'
		Me.Panel10.BackColor = System.Drawing.Color.White
		Me.Panel10.Controls.Add(Me.Label13)
		Me.Panel10.Location = New System.Drawing.Point(27, 158)
		Me.Panel10.Name = "Panel10"
		Me.Panel10.Size = New System.Drawing.Size(194, 21)
		Me.Panel10.TabIndex = 460
		'
		'Label13
		'
		Me.Label13.AutoSize = True
		Me.Label13.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label13.Font = New System.Drawing.Font("Work Sans SemiBold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label13.Location = New System.Drawing.Point(0, 0)
		Me.Label13.Name = "Label13"
		Me.Label13.Size = New System.Drawing.Size(193, 18)
		Me.Label13.TabIndex = 0
		Me.Label13.Text = "Filter Identifikasi dan Status"
		'
		'Panel11
		'
		Me.Panel11.BackColor = System.Drawing.Color.Red
		Me.Panel11.Location = New System.Drawing.Point(563, 59)
		Me.Panel11.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel11.Name = "Panel11"
		Me.Panel11.Size = New System.Drawing.Size(19, 601)
		Me.Panel11.TabIndex = 93
		Me.Panel11.Visible = False
		'
		'Panel12
		'
		Me.Panel12.BackColor = System.Drawing.Color.Red
		Me.Panel12.Location = New System.Drawing.Point(15, 452)
		Me.Panel12.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel12.Name = "Panel12"
		Me.Panel12.Size = New System.Drawing.Size(942, 15)
		Me.Panel12.TabIndex = 92
		Me.Panel12.Visible = False
		'
		'Lv_NoFaktur
		'
		Me.Lv_NoFaktur.BackColor = System.Drawing.Color.White
		Me.Lv_NoFaktur.FullRowSelect = True
		Me.Lv_NoFaktur.GridLines = True
		Me.Lv_NoFaktur.HideSelection = False
		Me.Lv_NoFaktur.Location = New System.Drawing.Point(590, 235)
		Me.Lv_NoFaktur.Name = "Lv_NoFaktur"
		Me.Lv_NoFaktur.Size = New System.Drawing.Size(423, 161)
		Me.Lv_NoFaktur.TabIndex = 461
		Me.Lv_NoFaktur.UseCompatibleStateImageBehavior = False
		Me.Lv_NoFaktur.View = System.Windows.Forms.View.Details
		Me.Lv_NoFaktur.Visible = False
		'
		'Lv_Formula
		'
		Me.Lv_Formula.BackColor = System.Drawing.Color.White
		Me.Lv_Formula.FullRowSelect = True
		Me.Lv_Formula.GridLines = True
		Me.Lv_Formula.HideSelection = False
		Me.Lv_Formula.Location = New System.Drawing.Point(590, 262)
		Me.Lv_Formula.Name = "Lv_Formula"
		Me.Lv_Formula.Size = New System.Drawing.Size(423, 161)
		Me.Lv_Formula.TabIndex = 461
		Me.Lv_Formula.UseCompatibleStateImageBehavior = False
		Me.Lv_Formula.View = System.Windows.Forms.View.Details
		Me.Lv_Formula.Visible = False
		'
		'Lv_Barang
		'
		Me.Lv_Barang.BackColor = System.Drawing.Color.White
		Me.Lv_Barang.FullRowSelect = True
		Me.Lv_Barang.GridLines = True
		Me.Lv_Barang.HideSelection = False
		Me.Lv_Barang.Location = New System.Drawing.Point(590, 367)
		Me.Lv_Barang.Name = "Lv_Barang"
		Me.Lv_Barang.Size = New System.Drawing.Size(423, 161)
		Me.Lv_Barang.TabIndex = 461
		Me.Lv_Barang.UseCompatibleStateImageBehavior = False
		Me.Lv_Barang.View = System.Windows.Forms.View.Details
		Me.Lv_Barang.Visible = False
		'
		'N_EMI_Laporan_Production_Order
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(582, 466)
		Me.Controls.Add(Me.Lv_Barang)
		Me.Controls.Add(Me.Lv_Formula)
		Me.Controls.Add(Me.Lv_NoFaktur)
		Me.Controls.Add(Me.Panel10)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel8)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel18)
		Me.Controls.Add(Me.BtnCetak)
		Me.Controls.Add(Me.BtnExit)
		Me.Controls.Add(Me.Panel11)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel12)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_Laporan_Production_Order"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel18.ResumeLayout(False)
		Me.Panel19.ResumeLayout(False)
		Me.Panel19.PerformLayout()
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.Panel4.ResumeLayout(False)
		Me.Panel6.ResumeLayout(False)
		Me.Panel6.PerformLayout()
		Me.Panel7.ResumeLayout(False)
		Me.Panel7.PerformLayout()
		Me.Panel8.ResumeLayout(False)
		Me.Panel9.ResumeLayout(False)
		Me.Panel9.PerformLayout()
		Me.Panel10.ResumeLayout(False)
		Me.Panel10.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Label11 As Label
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Cmb_Lain As ComboBox
	Friend WithEvents Cmb_Lokasi As ComboBox
	Friend WithEvents Txt_No_Faktur As TextBox
	Friend WithEvents Txt_KdBarang As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Label5 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents Label6 As Label
	Friend WithEvents Tgl2 As DateTimePicker
	Friend WithEvents Label3 As Label
	Friend WithEvents Tgl1 As DateTimePicker
	Friend WithEvents Txt_Lain As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents Txt_NmBarang As TextBox
	Friend WithEvents BtnCetak As Button
	Friend WithEvents BtnExit As Button
	Friend WithEvents Cmb_Tanggal As ComboBox
	Friend WithEvents Cmb_Status_Split As ComboBox
	Friend WithEvents Cmb_Status_PO As ComboBox
	Friend WithEvents Txt_No_Formula As TextBox
	Friend WithEvents Label7 As Label
	Friend WithEvents Label9 As Label
	Friend WithEvents Label8 As Label
	Friend WithEvents Panel18 As Panel
	Friend WithEvents Panel19 As Panel
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Label10 As Label
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Panel6 As Panel
	Friend WithEvents Panel7 As Panel
	Friend WithEvents Label12 As Label
	Friend WithEvents Panel8 As Panel
	Friend WithEvents Panel9 As Panel
	Friend WithEvents Panel10 As Panel
	Friend WithEvents Label13 As Label
	Friend WithEvents Panel11 As Panel
	Friend WithEvents Panel12 As Panel
	Friend WithEvents Lv_NoFaktur As ListView
	Friend WithEvents Lv_Formula As ListView
	Friend WithEvents Lv_Barang As ListView
End Class
