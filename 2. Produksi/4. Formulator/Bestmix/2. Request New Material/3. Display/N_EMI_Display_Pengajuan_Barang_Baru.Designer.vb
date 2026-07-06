<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Display_Pengajuan_Barang_Baru
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
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Lv_Data = New System.Windows.Forms.ListView()
		Me.Panel9 = New System.Windows.Forms.Panel()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Btn_Cari = New System.Windows.Forms.Button()
		Me.Cmb_Stock_Owner = New System.Windows.Forms.ComboBox()
		Me.Chk_HariIni = New System.Windows.Forms.CheckBox()
		Me.Txt_ParamLain = New System.Windows.Forms.TextBox()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Cmb_ParamLain = New System.Windows.Forms.ComboBox()
		Me.Chk_ParamLain = New System.Windows.Forms.CheckBox()
		Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
		Me.Chk_Tanggal = New System.Windows.Forms.CheckBox()
		Me.Cmb_Tanggal = New System.Windows.Forms.ComboBox()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.Txt_Nm_Barang = New System.Windows.Forms.TextBox()
		Me.Txt_User_Validasi = New System.Windows.Forms.TextBox()
		Me.Txt_Jam_Validasi = New System.Windows.Forms.TextBox()
		Me.Txt_Tgl_Validasi = New System.Windows.Forms.TextBox()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.Txt_Status_Transaksi = New System.Windows.Forms.TextBox()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.Txt_Klasifikasi_Bahan_2 = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Txt_Klasifikasi_Bahan = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Txt_Group_Jenis = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Txt_Kd_Barang = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Panel1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Label1)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(1034, 45)
		Me.Panel1.TabIndex = 346
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
		Me.PanelGradient1.Size = New System.Drawing.Size(1034, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(21, 7)
		Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(332, 30)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Display Pengajuan Barang Baru"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(22, 45)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1029, 12)
		Me.Panel2.TabIndex = 347
		Me.Panel2.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 54)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 491)
		Me.Panel3.TabIndex = 348
		Me.Panel3.Visible = False
		'
		'Lv_Data
		'
		Me.Lv_Data.FullRowSelect = True
		Me.Lv_Data.GridLines = True
		Me.Lv_Data.HideSelection = False
		Me.Lv_Data.Location = New System.Drawing.Point(20, 70)
		Me.Lv_Data.Name = "Lv_Data"
		Me.Lv_Data.Size = New System.Drawing.Size(995, 165)
		Me.Lv_Data.TabIndex = 349
		Me.Lv_Data.UseCompatibleStateImageBehavior = False
		Me.Lv_Data.View = System.Windows.Forms.View.Details
		'
		'Panel9
		'
		Me.Panel9.BackColor = System.Drawing.Color.DarkRed
		Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel9.Location = New System.Drawing.Point(932, 52)
		Me.Panel9.Name = "Panel9"
		Me.Panel9.Size = New System.Drawing.Size(12, 12)
		Me.Panel9.TabIndex = 493
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(948, 50)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(64, 16)
		Me.Label8.TabIndex = 494
		Me.Label8.Text = "Dibatalkan"
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(1015, 70)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(19, 491)
		Me.Panel4.TabIndex = 348
		Me.Panel4.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(25, 596)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(1029, 15)
		Me.Panel5.TabIndex = 347
		Me.Panel5.Visible = False
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Btn_Cari)
		Me.GroupBox1.Controls.Add(Me.Cmb_Stock_Owner)
		Me.GroupBox1.Controls.Add(Me.Chk_HariIni)
		Me.GroupBox1.Controls.Add(Me.Txt_ParamLain)
		Me.GroupBox1.Controls.Add(Me.Label6)
		Me.GroupBox1.Controls.Add(Me.Cmb_ParamLain)
		Me.GroupBox1.Controls.Add(Me.Chk_ParamLain)
		Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
		Me.GroupBox1.Controls.Add(Me.Label7)
		Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
		Me.GroupBox1.Controls.Add(Me.Chk_Tanggal)
		Me.GroupBox1.Controls.Add(Me.Cmb_Tanggal)
		Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.GroupBox1.Location = New System.Drawing.Point(20, 457)
		Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.GroupBox1.Size = New System.Drawing.Size(711, 139)
		Me.GroupBox1.TabIndex = 495
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Filter Data"
		'
		'Btn_Cari
		'
		Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari.Location = New System.Drawing.Point(616, 106)
		Me.Btn_Cari.Name = "Btn_Cari"
		Me.Btn_Cari.Size = New System.Drawing.Size(81, 27)
		Me.Btn_Cari.TabIndex = 343
		Me.Btn_Cari.Text = "&Cari"
		Me.Btn_Cari.UseVisualStyleBackColor = False
		'
		'Cmb_Stock_Owner
		'
		Me.Cmb_Stock_Owner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Stock_Owner.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Stock_Owner.FormattingEnabled = True
		Me.Cmb_Stock_Owner.Location = New System.Drawing.Point(8, 21)
		Me.Cmb_Stock_Owner.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Cmb_Stock_Owner.Name = "Cmb_Stock_Owner"
		Me.Cmb_Stock_Owner.Size = New System.Drawing.Size(209, 24)
		Me.Cmb_Stock_Owner.TabIndex = 342
		'
		'Chk_HariIni
		'
		Me.Chk_HariIni.AutoSize = True
		Me.Chk_HariIni.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Chk_HariIni.Location = New System.Drawing.Point(8, 51)
		Me.Chk_HariIni.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Chk_HariIni.Name = "Chk_HariIni"
		Me.Chk_HariIni.Size = New System.Drawing.Size(127, 21)
		Me.Chk_HariIni.TabIndex = 9
		Me.Chk_HariIni.Text = "Transaksi Hari Ini"
		Me.Chk_HariIni.UseVisualStyleBackColor = True
		'
		'Txt_ParamLain
		'
		Me.Txt_ParamLain.BackColor = System.Drawing.Color.White
		Me.Txt_ParamLain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_ParamLain.Enabled = False
		Me.Txt_ParamLain.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_ParamLain.Location = New System.Drawing.Point(364, 108)
		Me.Txt_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Txt_ParamLain.Name = "Txt_ParamLain"
		Me.Txt_ParamLain.Size = New System.Drawing.Size(247, 20)
		Me.Txt_ParamLain.TabIndex = 7
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label6.Location = New System.Drawing.Point(317, 109)
		Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(40, 17)
		Me.Label6.TabIndex = 8
		Me.Label6.Text = "Value"
		'
		'Cmb_ParamLain
		'
		Me.Cmb_ParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_ParamLain.Enabled = False
		Me.Cmb_ParamLain.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_ParamLain.FormattingEnabled = True
		Me.Cmb_ParamLain.Location = New System.Drawing.Point(152, 105)
		Me.Cmb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Cmb_ParamLain.Name = "Cmb_ParamLain"
		Me.Cmb_ParamLain.Size = New System.Drawing.Size(159, 24)
		Me.Cmb_ParamLain.TabIndex = 6
		'
		'Chk_ParamLain
		'
		Me.Chk_ParamLain.AutoSize = True
		Me.Chk_ParamLain.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Chk_ParamLain.Location = New System.Drawing.Point(8, 105)
		Me.Chk_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Chk_ParamLain.Name = "Chk_ParamLain"
		Me.Chk_ParamLain.Size = New System.Drawing.Size(116, 21)
		Me.Chk_ParamLain.TabIndex = 5
		Me.Chk_ParamLain.Text = "Parameter Lain"
		Me.Chk_ParamLain.UseVisualStyleBackColor = True
		'
		'DateTimePicker2
		'
		Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
		Me.DateTimePicker2.Enabled = False
		Me.DateTimePicker2.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.DateTimePicker2.Location = New System.Drawing.Point(531, 80)
		Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.DateTimePicker2.Name = "DateTimePicker2"
		Me.DateTimePicker2.Size = New System.Drawing.Size(166, 20)
		Me.DateTimePicker2.TabIndex = 4
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(500, 82)
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
		Me.DateTimePicker1.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.DateTimePicker1.Location = New System.Drawing.Point(329, 80)
		Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.DateTimePicker1.Name = "DateTimePicker1"
		Me.DateTimePicker1.Size = New System.Drawing.Size(166, 20)
		Me.DateTimePicker1.TabIndex = 3
		'
		'Chk_Tanggal
		'
		Me.Chk_Tanggal.AutoSize = True
		Me.Chk_Tanggal.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Chk_Tanggal.Location = New System.Drawing.Point(8, 78)
		Me.Chk_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Chk_Tanggal.Name = "Chk_Tanggal"
		Me.Chk_Tanggal.Size = New System.Drawing.Size(135, 21)
		Me.Chk_Tanggal.TabIndex = 1
		Me.Chk_Tanggal.Text = "Parameter Tanggal"
		Me.Chk_Tanggal.UseVisualStyleBackColor = True
		'
		'Cmb_Tanggal
		'
		Me.Cmb_Tanggal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Tanggal.Enabled = False
		Me.Cmb_Tanggal.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Tanggal.FormattingEnabled = True
		Me.Cmb_Tanggal.Location = New System.Drawing.Point(152, 76)
		Me.Cmb_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.Cmb_Tanggal.Name = "Cmb_Tanggal"
		Me.Cmb_Tanggal.Size = New System.Drawing.Size(159, 24)
		Me.Cmb_Tanggal.TabIndex = 2
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(22, 443)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(1029, 12)
		Me.Panel6.TabIndex = 347
		Me.Panel6.Visible = False
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.Txt_Nm_Barang)
		Me.GroupBox2.Controls.Add(Me.Txt_User_Validasi)
		Me.GroupBox2.Controls.Add(Me.Txt_Jam_Validasi)
		Me.GroupBox2.Controls.Add(Me.Txt_Tgl_Validasi)
		Me.GroupBox2.Controls.Add(Me.Label10)
		Me.GroupBox2.Controls.Add(Me.Txt_Status_Transaksi)
		Me.GroupBox2.Controls.Add(Me.Label9)
		Me.GroupBox2.Controls.Add(Me.Txt_Klasifikasi_Bahan_2)
		Me.GroupBox2.Controls.Add(Me.Label5)
		Me.GroupBox2.Controls.Add(Me.Txt_Klasifikasi_Bahan)
		Me.GroupBox2.Controls.Add(Me.Label4)
		Me.GroupBox2.Controls.Add(Me.Txt_Group_Jenis)
		Me.GroupBox2.Controls.Add(Me.Label3)
		Me.GroupBox2.Controls.Add(Me.Txt_Kd_Barang)
		Me.GroupBox2.Controls.Add(Me.Label2)
		Me.GroupBox2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox2.Location = New System.Drawing.Point(22, 249)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(990, 193)
		Me.GroupBox2.TabIndex = 496
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Detail"
		'
		'Txt_Nm_Barang
		'
		Me.Txt_Nm_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Nm_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Nm_Barang.Enabled = False
		Me.Txt_Nm_Barang.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Nm_Barang.Location = New System.Drawing.Point(257, 23)
		Me.Txt_Nm_Barang.MaxLength = 40
		Me.Txt_Nm_Barang.Name = "Txt_Nm_Barang"
		Me.Txt_Nm_Barang.Size = New System.Drawing.Size(287, 20)
		Me.Txt_Nm_Barang.TabIndex = 491
		'
		'Txt_User_Validasi
		'
		Me.Txt_User_Validasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_User_Validasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_User_Validasi.Enabled = False
		Me.Txt_User_Validasi.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_User_Validasi.Location = New System.Drawing.Point(378, 155)
		Me.Txt_User_Validasi.MaxLength = 40
		Me.Txt_User_Validasi.Name = "Txt_User_Validasi"
		Me.Txt_User_Validasi.Size = New System.Drawing.Size(166, 20)
		Me.Txt_User_Validasi.TabIndex = 491
		Me.Txt_User_Validasi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Txt_Jam_Validasi
		'
		Me.Txt_Jam_Validasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Jam_Validasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Jam_Validasi.Enabled = False
		Me.Txt_Jam_Validasi.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Jam_Validasi.Location = New System.Drawing.Point(255, 155)
		Me.Txt_Jam_Validasi.MaxLength = 40
		Me.Txt_Jam_Validasi.Name = "Txt_Jam_Validasi"
		Me.Txt_Jam_Validasi.Size = New System.Drawing.Size(117, 20)
		Me.Txt_Jam_Validasi.TabIndex = 491
		Me.Txt_Jam_Validasi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Txt_Tgl_Validasi
		'
		Me.Txt_Tgl_Validasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Tgl_Validasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Tgl_Validasi.Enabled = False
		Me.Txt_Tgl_Validasi.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Tgl_Validasi.Location = New System.Drawing.Point(134, 155)
		Me.Txt_Tgl_Validasi.MaxLength = 40
		Me.Txt_Tgl_Validasi.Name = "Txt_Tgl_Validasi"
		Me.Txt_Tgl_Validasi.Size = New System.Drawing.Size(117, 20)
		Me.Txt_Tgl_Validasi.TabIndex = 491
		Me.Txt_Tgl_Validasi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label10.Location = New System.Drawing.Point(11, 155)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(92, 17)
		Me.Label10.TabIndex = 490
		Me.Label10.Text = "Detail Validasi"
		'
		'Txt_Status_Transaksi
		'
		Me.Txt_Status_Transaksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Status_Transaksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Status_Transaksi.Enabled = False
		Me.Txt_Status_Transaksi.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Txt_Status_Transaksi.Location = New System.Drawing.Point(134, 127)
		Me.Txt_Status_Transaksi.MaxLength = 40
		Me.Txt_Status_Transaksi.Name = "Txt_Status_Transaksi"
		Me.Txt_Status_Transaksi.Size = New System.Drawing.Size(304, 22)
		Me.Txt_Status_Transaksi.TabIndex = 491
		Me.Txt_Status_Transaksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label9.Location = New System.Drawing.Point(11, 127)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(104, 17)
		Me.Label9.TabIndex = 490
		Me.Label9.Text = "Status Transaksi"
		'
		'Txt_Klasifikasi_Bahan_2
		'
		Me.Txt_Klasifikasi_Bahan_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Klasifikasi_Bahan_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Klasifikasi_Bahan_2.Enabled = False
		Me.Txt_Klasifikasi_Bahan_2.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Klasifikasi_Bahan_2.Location = New System.Drawing.Point(134, 101)
		Me.Txt_Klasifikasi_Bahan_2.MaxLength = 40
		Me.Txt_Klasifikasi_Bahan_2.Name = "Txt_Klasifikasi_Bahan_2"
		Me.Txt_Klasifikasi_Bahan_2.Size = New System.Drawing.Size(304, 20)
		Me.Txt_Klasifikasi_Bahan_2.TabIndex = 491
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label5.Location = New System.Drawing.Point(11, 101)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(118, 17)
		Me.Label5.TabIndex = 490
		Me.Label5.Text = "Klasifikasi Bahan 2"
		'
		'Txt_Klasifikasi_Bahan
		'
		Me.Txt_Klasifikasi_Bahan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Klasifikasi_Bahan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Klasifikasi_Bahan.Enabled = False
		Me.Txt_Klasifikasi_Bahan.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Klasifikasi_Bahan.Location = New System.Drawing.Point(134, 75)
		Me.Txt_Klasifikasi_Bahan.MaxLength = 40
		Me.Txt_Klasifikasi_Bahan.Name = "Txt_Klasifikasi_Bahan"
		Me.Txt_Klasifikasi_Bahan.Size = New System.Drawing.Size(304, 20)
		Me.Txt_Klasifikasi_Bahan.TabIndex = 491
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label4.Location = New System.Drawing.Point(11, 75)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(107, 17)
		Me.Label4.TabIndex = 490
		Me.Label4.Text = "Klasifikasi Bahan"
		'
		'Txt_Group_Jenis
		'
		Me.Txt_Group_Jenis.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Group_Jenis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Group_Jenis.Enabled = False
		Me.Txt_Group_Jenis.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Group_Jenis.Location = New System.Drawing.Point(134, 49)
		Me.Txt_Group_Jenis.MaxLength = 40
		Me.Txt_Group_Jenis.Name = "Txt_Group_Jenis"
		Me.Txt_Group_Jenis.Size = New System.Drawing.Size(304, 20)
		Me.Txt_Group_Jenis.TabIndex = 491
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label3.Location = New System.Drawing.Point(11, 49)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(78, 17)
		Me.Label3.TabIndex = 490
		Me.Label3.Text = "Group Jenis"
		'
		'Txt_Kd_Barang
		'
		Me.Txt_Kd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Kd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Kd_Barang.Enabled = False
		Me.Txt_Kd_Barang.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Kd_Barang.Location = New System.Drawing.Point(134, 23)
		Me.Txt_Kd_Barang.MaxLength = 40
		Me.Txt_Kd_Barang.Name = "Txt_Kd_Barang"
		Me.Txt_Kd_Barang.Size = New System.Drawing.Size(117, 20)
		Me.Txt_Kd_Barang.TabIndex = 491
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(11, 23)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(48, 17)
		Me.Label2.TabIndex = 490
		Me.Label2.Text = "Barang"
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(16, 235)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1029, 12)
		Me.Panel7.TabIndex = 347
		Me.Panel7.Visible = False
		'
		'N_EMI_Display_Pengajuan_Barang_Baru
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1034, 611)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Panel9)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.Lv_Data)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_Display_Pengajuan_Barang_Baru"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Cmb_Stock_Owner As ComboBox
    Friend WithEvents Chk_HariIni As CheckBox
    Friend WithEvents Txt_ParamLain As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_ParamLain As ComboBox
    Friend WithEvents Chk_ParamLain As CheckBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Chk_Tanggal As CheckBox
    Friend WithEvents Cmb_Tanggal As ComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Txt_Kd_Barang As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Nm_Barang As TextBox
    Friend WithEvents Txt_Group_Jenis As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_Klasifikasi_Bahan_2 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_Klasifikasi_Bahan As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_User_Validasi As TextBox
    Friend WithEvents Txt_Jam_Validasi As TextBox
    Friend WithEvents Txt_Tgl_Validasi As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Txt_Status_Transaksi As TextBox
    Friend WithEvents Label9 As Label
End Class
