<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Master_Line_Production
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
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.TabControl1 = New System.Windows.Forms.TabControl()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.Lv1_Packing_Set_Detail = New System.Windows.Forms.ListView()
		Me.Lv1_Packing_Set = New System.Windows.Forms.ListView()
		Me.Panel14 = New System.Windows.Forms.Panel()
		Me.Panel15 = New System.Windows.Forms.Panel()
		Me.Panel10 = New System.Windows.Forms.Panel()
		Me.Panel11 = New System.Windows.Forms.Panel()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.Panel9 = New System.Windows.Forms.Panel()
		Me.Btn1_Hapus = New System.Windows.Forms.Button()
		Me.Btn1_Refresh = New System.Windows.Forms.Button()
		Me.Btn1_Simpan = New System.Windows.Forms.Button()
		Me.Btn1_Get_Packing_Set = New System.Windows.Forms.Button()
		Me.Txt1_NmBarang = New System.Windows.Forms.TextBox()
		Me.Txt1_Keterangan = New System.Windows.Forms.TextBox()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Txt1_KdBarang = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.TabPage2 = New System.Windows.Forms.TabPage()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Panel16 = New System.Windows.Forms.Panel()
		Me.GroupBox3 = New System.Windows.Forms.GroupBox()
		Me.Lv2_Detail_Packing_Set = New System.Windows.Forms.ListView()
		Me.Btn2_Cari = New System.Windows.Forms.Button()
		Me.Cmb2_Filter = New System.Windows.Forms.ComboBox()
		Me.Txt2_Filter = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Lv2_Display_Line_Production = New System.Windows.Forms.ListView()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel12 = New System.Windows.Forms.Panel()
		Me.Panel13 = New System.Windows.Forms.Panel()
		Me.Lv1_Barang = New System.Windows.Forms.ListView()
		Me.Panel1.SuspendLayout()
		Me.Panel3.SuspendLayout()
		Me.TabControl1.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.Panel14.SuspendLayout()
		Me.Panel10.SuspendLayout()
		Me.Panel8.SuspendLayout()
		Me.TabPage2.SuspendLayout()
		Me.GroupBox3.SuspendLayout()
		Me.Panel12.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(1084, 45)
		Me.Panel1.TabIndex = 24
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
		Me.PanelGradient1.Size = New System.Drawing.Size(1084, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(20, 7)
		Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(256, 30)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Master Line Production"
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Controls.Add(Me.Panel6)
		Me.Panel3.Location = New System.Drawing.Point(0, 59)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 601)
		Me.Panel3.TabIndex = 38
		Me.Panel3.Visible = False
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(20, 485)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(942, 12)
		Me.Panel6.TabIndex = 35
		Me.Panel6.Visible = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(0, 44)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(942, 12)
		Me.Panel2.TabIndex = 37
		Me.Panel2.Visible = False
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.TabPage1)
		Me.TabControl1.Controls.Add(Me.TabPage2)
		Me.TabControl1.Location = New System.Drawing.Point(21, 56)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(1042, 461)
		Me.TabControl1.TabIndex = 39
		'
		'TabPage1
		'
		Me.TabPage1.BackColor = System.Drawing.Color.White
		Me.TabPage1.Controls.Add(Me.GroupBox1)
		Me.TabPage1.Controls.Add(Me.Panel10)
		Me.TabPage1.Controls.Add(Me.Panel8)
		Me.TabPage1.Controls.Add(Me.Btn1_Hapus)
		Me.TabPage1.Controls.Add(Me.Btn1_Refresh)
		Me.TabPage1.Controls.Add(Me.Btn1_Simpan)
		Me.TabPage1.Controls.Add(Me.Btn1_Get_Packing_Set)
		Me.TabPage1.Controls.Add(Me.Txt1_NmBarang)
		Me.TabPage1.Controls.Add(Me.Txt1_Keterangan)
		Me.TabPage1.Controls.Add(Me.Panel7)
		Me.TabPage1.Controls.Add(Me.Panel5)
		Me.TabPage1.Controls.Add(Me.Label2)
		Me.TabPage1.Controls.Add(Me.Txt1_KdBarang)
		Me.TabPage1.Controls.Add(Me.Label3)
		Me.TabPage1.Location = New System.Drawing.Point(4, 25)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage1.Size = New System.Drawing.Size(1034, 432)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Create Data"
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.GroupBox2)
		Me.GroupBox1.Controls.Add(Me.Lv1_Packing_Set)
		Me.GroupBox1.Controls.Add(Me.Panel14)
		Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox1.Location = New System.Drawing.Point(20, 97)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(998, 270)
		Me.GroupBox1.TabIndex = 427
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Packing Set"
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.Lv1_Packing_Set_Detail)
		Me.GroupBox2.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.GroupBox2.Location = New System.Drawing.Point(481, 13)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(513, 251)
		Me.GroupBox2.TabIndex = 1
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Detail Packing Set"
		'
		'Lv1_Packing_Set_Detail
		'
		Me.Lv1_Packing_Set_Detail.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Lv1_Packing_Set_Detail.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv1_Packing_Set_Detail.FullRowSelect = True
		Me.Lv1_Packing_Set_Detail.GridLines = True
		Me.Lv1_Packing_Set_Detail.HideSelection = False
		Me.Lv1_Packing_Set_Detail.Location = New System.Drawing.Point(3, 16)
		Me.Lv1_Packing_Set_Detail.Name = "Lv1_Packing_Set_Detail"
		Me.Lv1_Packing_Set_Detail.Size = New System.Drawing.Size(507, 232)
		Me.Lv1_Packing_Set_Detail.TabIndex = 1
		Me.Lv1_Packing_Set_Detail.UseCompatibleStateImageBehavior = False
		Me.Lv1_Packing_Set_Detail.View = System.Windows.Forms.View.Details
		'
		'Lv1_Packing_Set
		'
		Me.Lv1_Packing_Set.CheckBoxes = True
		Me.Lv1_Packing_Set.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv1_Packing_Set.FullRowSelect = True
		Me.Lv1_Packing_Set.GridLines = True
		Me.Lv1_Packing_Set.HideSelection = False
		Me.Lv1_Packing_Set.Location = New System.Drawing.Point(6, 21)
		Me.Lv1_Packing_Set.Name = "Lv1_Packing_Set"
		Me.Lv1_Packing_Set.Size = New System.Drawing.Size(460, 243)
		Me.Lv1_Packing_Set.TabIndex = 0
		Me.Lv1_Packing_Set.UseCompatibleStateImageBehavior = False
		Me.Lv1_Packing_Set.View = System.Windows.Forms.View.Details
		'
		'Panel14
		'
		Me.Panel14.BackColor = System.Drawing.Color.Red
		Me.Panel14.Controls.Add(Me.Panel15)
		Me.Panel14.Location = New System.Drawing.Point(466, 13)
		Me.Panel14.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel14.Name = "Panel14"
		Me.Panel14.Size = New System.Drawing.Size(12, 601)
		Me.Panel14.TabIndex = 38
		Me.Panel14.Visible = False
		'
		'Panel15
		'
		Me.Panel15.BackColor = System.Drawing.Color.Red
		Me.Panel15.Location = New System.Drawing.Point(20, 485)
		Me.Panel15.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel15.Name = "Panel15"
		Me.Panel15.Size = New System.Drawing.Size(942, 12)
		Me.Panel15.TabIndex = 35
		Me.Panel15.Visible = False
		'
		'Panel10
		'
		Me.Panel10.BackColor = System.Drawing.Color.Red
		Me.Panel10.Controls.Add(Me.Panel11)
		Me.Panel10.Location = New System.Drawing.Point(1020, 9)
		Me.Panel10.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel10.Name = "Panel10"
		Me.Panel10.Size = New System.Drawing.Size(12, 601)
		Me.Panel10.TabIndex = 38
		Me.Panel10.Visible = False
		'
		'Panel11
		'
		Me.Panel11.BackColor = System.Drawing.Color.Red
		Me.Panel11.Location = New System.Drawing.Point(20, 485)
		Me.Panel11.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel11.Name = "Panel11"
		Me.Panel11.Size = New System.Drawing.Size(942, 12)
		Me.Panel11.TabIndex = 35
		Me.Panel11.Visible = False
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.Red
		Me.Panel8.Controls.Add(Me.Panel9)
		Me.Panel8.Location = New System.Drawing.Point(2, 3)
		Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Size = New System.Drawing.Size(12, 601)
		Me.Panel8.TabIndex = 38
		Me.Panel8.Visible = False
		'
		'Panel9
		'
		Me.Panel9.BackColor = System.Drawing.Color.Red
		Me.Panel9.Location = New System.Drawing.Point(20, 485)
		Me.Panel9.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel9.Name = "Panel9"
		Me.Panel9.Size = New System.Drawing.Size(942, 12)
		Me.Panel9.TabIndex = 35
		Me.Panel9.Visible = False
		'
		'Btn1_Hapus
		'
		Me.Btn1_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn1_Hapus.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn1_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn1_Hapus.ForeColor = System.Drawing.Color.White
		Me.Btn1_Hapus.Location = New System.Drawing.Point(344, 381)
		Me.Btn1_Hapus.Name = "Btn1_Hapus"
		Me.Btn1_Hapus.Size = New System.Drawing.Size(158, 35)
		Me.Btn1_Hapus.TabIndex = 426
		Me.Btn1_Hapus.Text = "&Hapus"
		Me.Btn1_Hapus.UseVisualStyleBackColor = False
		'
		'Btn1_Refresh
		'
		Me.Btn1_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn1_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn1_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn1_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn1_Refresh.Location = New System.Drawing.Point(182, 381)
		Me.Btn1_Refresh.Name = "Btn1_Refresh"
		Me.Btn1_Refresh.Size = New System.Drawing.Size(158, 35)
		Me.Btn1_Refresh.TabIndex = 426
		Me.Btn1_Refresh.Text = "&Refresh"
		Me.Btn1_Refresh.UseVisualStyleBackColor = False
		'
		'Btn1_Simpan
		'
		Me.Btn1_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn1_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn1_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn1_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn1_Simpan.Location = New System.Drawing.Point(20, 381)
		Me.Btn1_Simpan.Name = "Btn1_Simpan"
		Me.Btn1_Simpan.Size = New System.Drawing.Size(158, 35)
		Me.Btn1_Simpan.TabIndex = 426
		Me.Btn1_Simpan.Text = "&Simpan"
		Me.Btn1_Simpan.UseVisualStyleBackColor = False
		'
		'Btn1_Get_Packing_Set
		'
		Me.Btn1_Get_Packing_Set.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn1_Get_Packing_Set.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn1_Get_Packing_Set.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn1_Get_Packing_Set.ForeColor = System.Drawing.Color.White
		Me.Btn1_Get_Packing_Set.Location = New System.Drawing.Point(135, 60)
		Me.Btn1_Get_Packing_Set.Name = "Btn1_Get_Packing_Set"
		Me.Btn1_Get_Packing_Set.Size = New System.Drawing.Size(158, 31)
		Me.Btn1_Get_Packing_Set.TabIndex = 426
		Me.Btn1_Get_Packing_Set.Text = "&Get Packing Set"
		Me.Btn1_Get_Packing_Set.UseVisualStyleBackColor = False
		'
		'Txt1_NmBarang
		'
		Me.Txt1_NmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt1_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt1_NmBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt1_NmBarang.Location = New System.Drawing.Point(297, 8)
		Me.Txt1_NmBarang.Name = "Txt1_NmBarang"
		Me.Txt1_NmBarang.Size = New System.Drawing.Size(323, 20)
		Me.Txt1_NmBarang.TabIndex = 377
		'
		'Txt1_Keterangan
		'
		Me.Txt1_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt1_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt1_Keterangan.Enabled = False
		Me.Txt1_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt1_Keterangan.Location = New System.Drawing.Point(137, 34)
		Me.Txt1_Keterangan.MaxLength = 255
		Me.Txt1_Keterangan.Name = "Txt1_Keterangan"
		Me.Txt1_Keterangan.Size = New System.Drawing.Size(483, 20)
		Me.Txt1_Keterangan.TabIndex = 377
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(14, 417)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(942, 12)
		Me.Panel7.TabIndex = 37
		Me.Panel7.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(12, 366)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(942, 12)
		Me.Panel5.TabIndex = 37
		Me.Panel5.Visible = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(17, 35)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(99, 17)
		Me.Label2.TabIndex = 376
		Me.Label2.Text = "Line Production"
		'
		'Txt1_KdBarang
		'
		Me.Txt1_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt1_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt1_KdBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt1_KdBarang.Location = New System.Drawing.Point(137, 8)
		Me.Txt1_KdBarang.Name = "Txt1_KdBarang"
		Me.Txt1_KdBarang.Size = New System.Drawing.Size(156, 20)
		Me.Txt1_KdBarang.TabIndex = 377
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label3.Location = New System.Drawing.Point(17, 9)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(48, 17)
		Me.Label3.TabIndex = 376
		Me.Label3.Text = "Barang"
		'
		'TabPage2
		'
		Me.TabPage2.BackColor = System.Drawing.Color.White
		Me.TabPage2.Controls.Add(Me.Label8)
		Me.TabPage2.Controls.Add(Me.Panel16)
		Me.TabPage2.Controls.Add(Me.GroupBox3)
		Me.TabPage2.Controls.Add(Me.Btn2_Cari)
		Me.TabPage2.Controls.Add(Me.Cmb2_Filter)
		Me.TabPage2.Controls.Add(Me.Txt2_Filter)
		Me.TabPage2.Controls.Add(Me.Label4)
		Me.TabPage2.Controls.Add(Me.Lv2_Display_Line_Production)
		Me.TabPage2.Location = New System.Drawing.Point(4, 25)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage2.Size = New System.Drawing.Size(1034, 432)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "Display Data"
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(970, 16)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(58, 16)
		Me.Label8.TabIndex = 541
		Me.Label8.Text = "Non Aktif"
		'
		'Panel16
		'
		Me.Panel16.BackColor = System.Drawing.Color.Tan
		Me.Panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel16.Location = New System.Drawing.Point(954, 19)
		Me.Panel16.Name = "Panel16"
		Me.Panel16.Size = New System.Drawing.Size(11, 11)
		Me.Panel16.TabIndex = 540
		'
		'GroupBox3
		'
		Me.GroupBox3.Controls.Add(Me.Lv2_Detail_Packing_Set)
		Me.GroupBox3.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox3.Location = New System.Drawing.Point(6, 229)
		Me.GroupBox3.Name = "GroupBox3"
		Me.GroupBox3.Size = New System.Drawing.Size(1022, 197)
		Me.GroupBox3.TabIndex = 428
		Me.GroupBox3.TabStop = False
		Me.GroupBox3.Text = "Packing Set"
		'
		'Lv2_Detail_Packing_Set
		'
		Me.Lv2_Detail_Packing_Set.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Lv2_Detail_Packing_Set.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv2_Detail_Packing_Set.FullRowSelect = True
		Me.Lv2_Detail_Packing_Set.GridLines = True
		Me.Lv2_Detail_Packing_Set.HideSelection = False
		Me.Lv2_Detail_Packing_Set.Location = New System.Drawing.Point(3, 18)
		Me.Lv2_Detail_Packing_Set.Name = "Lv2_Detail_Packing_Set"
		Me.Lv2_Detail_Packing_Set.Size = New System.Drawing.Size(1016, 176)
		Me.Lv2_Detail_Packing_Set.TabIndex = 429
		Me.Lv2_Detail_Packing_Set.UseCompatibleStateImageBehavior = False
		Me.Lv2_Detail_Packing_Set.View = System.Windows.Forms.View.Details
		'
		'Btn2_Cari
		'
		Me.Btn2_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn2_Cari.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn2_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn2_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn2_Cari.Location = New System.Drawing.Point(525, 4)
		Me.Btn2_Cari.Name = "Btn2_Cari"
		Me.Btn2_Cari.Size = New System.Drawing.Size(81, 28)
		Me.Btn2_Cari.TabIndex = 427
		Me.Btn2_Cari.Text = "&Cari"
		Me.Btn2_Cari.UseVisualStyleBackColor = False
		'
		'Cmb2_Filter
		'
		Me.Cmb2_Filter.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb2_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb2_Filter.FormattingEnabled = True
		Me.Cmb2_Filter.Location = New System.Drawing.Point(79, 6)
		Me.Cmb2_Filter.Name = "Cmb2_Filter"
		Me.Cmb2_Filter.Size = New System.Drawing.Size(172, 24)
		Me.Cmb2_Filter.TabIndex = 380
		'
		'Txt2_Filter
		'
		Me.Txt2_Filter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt2_Filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt2_Filter.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt2_Filter.Location = New System.Drawing.Point(255, 8)
		Me.Txt2_Filter.Name = "Txt2_Filter"
		Me.Txt2_Filter.Size = New System.Drawing.Size(265, 20)
		Me.Txt2_Filter.TabIndex = 379
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label4.Location = New System.Drawing.Point(17, 10)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(39, 17)
		Me.Label4.TabIndex = 378
		Me.Label4.Text = "Filter"
		'
		'Lv2_Display_Line_Production
		'
		Me.Lv2_Display_Line_Production.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv2_Display_Line_Production.FullRowSelect = True
		Me.Lv2_Display_Line_Production.GridLines = True
		Me.Lv2_Display_Line_Production.HideSelection = False
		Me.Lv2_Display_Line_Production.Location = New System.Drawing.Point(3, 38)
		Me.Lv2_Display_Line_Production.Name = "Lv2_Display_Line_Production"
		Me.Lv2_Display_Line_Production.Size = New System.Drawing.Size(1028, 185)
		Me.Lv2_Display_Line_Production.TabIndex = 2
		Me.Lv2_Display_Line_Production.UseCompatibleStateImageBehavior = False
		Me.Lv2_Display_Line_Production.View = System.Windows.Forms.View.Details
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(24, 518)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(942, 15)
		Me.Panel4.TabIndex = 37
		Me.Panel4.Visible = False
		'
		'Panel12
		'
		Me.Panel12.BackColor = System.Drawing.Color.Red
		Me.Panel12.Controls.Add(Me.Panel13)
		Me.Panel12.Location = New System.Drawing.Point(1065, 81)
		Me.Panel12.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel12.Name = "Panel12"
		Me.Panel12.Size = New System.Drawing.Size(19, 601)
		Me.Panel12.TabIndex = 38
		Me.Panel12.Visible = False
		'
		'Panel13
		'
		Me.Panel13.BackColor = System.Drawing.Color.Red
		Me.Panel13.Location = New System.Drawing.Point(20, 485)
		Me.Panel13.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel13.Name = "Panel13"
		Me.Panel13.Size = New System.Drawing.Size(942, 12)
		Me.Panel13.TabIndex = 35
		Me.Panel13.Visible = False
		'
		'Lv1_Barang
		'
		Me.Lv1_Barang.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv1_Barang.FullRowSelect = True
		Me.Lv1_Barang.GridLines = True
		Me.Lv1_Barang.HideSelection = False
		Me.Lv1_Barang.Location = New System.Drawing.Point(1070, 112)
		Me.Lv1_Barang.Name = "Lv1_Barang"
		Me.Lv1_Barang.Size = New System.Drawing.Size(483, 178)
		Me.Lv1_Barang.TabIndex = 428
		Me.Lv1_Barang.UseCompatibleStateImageBehavior = False
		Me.Lv1_Barang.View = System.Windows.Forms.View.Details
		'
		'N_EMI_Master_Line_Production
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1084, 534)
		Me.Controls.Add(Me.Lv1_Barang)
		Me.Controls.Add(Me.TabControl1)
		Me.Controls.Add(Me.Panel12)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel1)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel2)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.Name = "N_EMI_Master_Line_Production"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel3.ResumeLayout(False)
		Me.TabControl1.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabPage1.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox2.ResumeLayout(False)
		Me.Panel14.ResumeLayout(False)
		Me.Panel10.ResumeLayout(False)
		Me.Panel8.ResumeLayout(False)
		Me.TabPage2.ResumeLayout(False)
		Me.TabPage2.PerformLayout()
		Me.GroupBox3.ResumeLayout(False)
		Me.Panel12.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Label1 As Label
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel6 As Panel
	Friend WithEvents Panel2 As Panel
	Friend WithEvents TabControl1 As TabControl
	Friend WithEvents TabPage1 As TabPage
	Friend WithEvents TabPage2 As TabPage
	Friend WithEvents Txt1_KdBarang As TextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Txt1_NmBarang As TextBox
	Friend WithEvents Txt1_Keterangan As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents Btn1_Get_Packing_Set As Button
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Lv1_Packing_Set As ListView
	Friend WithEvents Btn1_Refresh As Button
	Friend WithEvents Btn1_Simpan As Button
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Btn1_Hapus As Button
	Friend WithEvents Panel7 As Panel
	Friend WithEvents Panel10 As Panel
	Friend WithEvents Panel11 As Panel
	Friend WithEvents Panel8 As Panel
	Friend WithEvents Panel9 As Panel
	Friend WithEvents Panel12 As Panel
	Friend WithEvents Panel13 As Panel
	Friend WithEvents GroupBox2 As GroupBox
	Friend WithEvents Lv1_Packing_Set_Detail As ListView
	Friend WithEvents Panel14 As Panel
	Friend WithEvents Panel15 As Panel
	Friend WithEvents Lv1_Barang As ListView
	Friend WithEvents Lv2_Display_Line_Production As ListView
	Friend WithEvents Btn2_Cari As Button
	Friend WithEvents Cmb2_Filter As ComboBox
	Friend WithEvents Txt2_Filter As TextBox
	Friend WithEvents Label4 As Label
	Friend WithEvents GroupBox3 As GroupBox
	Friend WithEvents Lv2_Detail_Packing_Set As ListView
	Friend WithEvents Label8 As Label
	Friend WithEvents Panel16 As Panel
End Class
