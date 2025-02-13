<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Display_Tracking_Kendaraan
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Dgv_Tracking = New System.Windows.Forms.DataGridView()
        Me.no_faktur = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lokasi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.no_sj = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tanggal_sampai = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tglSampai = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ekspedisi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.driver = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.noPlat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.eta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_DetBahan = New System.Windows.Forms.ListView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Tb_TanggalBerangkat = New System.Windows.Forms.TextBox()
        Me.Tb_Ekspedisi = New System.Windows.Forms.TextBox()
        Me.Tb_Driver = New System.Windows.Forms.TextBox()
        Me.Tb_NoPlat = New System.Windows.Forms.TextBox()
        Me.Tb_ETA = New System.Windows.Forms.TextBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Dgv_Tracking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1128, 51)
        Me.Panel1.TabIndex = 23
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 49)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1128, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(256, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Track - Kamar Timbang"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(19, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1216, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 50)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 695)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(538, 63)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(111, 28)
        Me.Btn_Cari.TabIndex = 343
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(26, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 20)
        Me.Label4.TabIndex = 341
        Me.Label4.Text = "Tanggal"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Dgv_Tracking)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(20, 93)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1088, 309)
        Me.GroupBox1.TabIndex = 345
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Position"
        '
        'Dgv_Tracking
        '
        Me.Dgv_Tracking.AllowUserToAddRows = False
        Me.Dgv_Tracking.AllowUserToDeleteRows = False
        Me.Dgv_Tracking.AllowUserToResizeColumns = False
        Me.Dgv_Tracking.AllowUserToResizeRows = False
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Tracking.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Dgv_Tracking.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Dgv_Tracking.BackgroundColor = System.Drawing.Color.White
        Me.Dgv_Tracking.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Tracking.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Dgv_Tracking.ColumnHeadersHeight = 45
        Me.Dgv_Tracking.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.no_faktur, Me.lokasi, Me.supplier, Me.no_sj, Me.tanggal_sampai, Me.status, Me.tglSampai, Me.ekspedisi, Me.driver, Me.noPlat, Me.eta})
        Me.Dgv_Tracking.Location = New System.Drawing.Point(6, 19)
        Me.Dgv_Tracking.Name = "Dgv_Tracking"
        Me.Dgv_Tracking.ReadOnly = True
        Me.Dgv_Tracking.RowHeadersWidth = 10
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Tracking.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.Dgv_Tracking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Tracking.Size = New System.Drawing.Size(1073, 284)
        Me.Dgv_Tracking.TabIndex = 23
        '
        'no_faktur
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.no_faktur.DefaultCellStyle = DataGridViewCellStyle3
        Me.no_faktur.HeaderText = "No Faktur"
        Me.no_faktur.Name = "no_faktur"
        Me.no_faktur.ReadOnly = True
        Me.no_faktur.Width = 160
        '
        'lokasi
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.lokasi.DefaultCellStyle = DataGridViewCellStyle4
        Me.lokasi.HeaderText = "Lokasi"
        Me.lokasi.Name = "lokasi"
        Me.lokasi.ReadOnly = True
        Me.lokasi.Width = 150
        '
        'supplier
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.supplier.DefaultCellStyle = DataGridViewCellStyle5
        Me.supplier.HeaderText = "Supplier"
        Me.supplier.Name = "supplier"
        Me.supplier.ReadOnly = True
        Me.supplier.Width = 150
        '
        'no_sj
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.no_sj.DefaultCellStyle = DataGridViewCellStyle6
        Me.no_sj.HeaderText = "No Surat Jalan"
        Me.no_sj.Name = "no_sj"
        Me.no_sj.ReadOnly = True
        Me.no_sj.Width = 150
        '
        'tanggal_sampai
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.tanggal_sampai.DefaultCellStyle = DataGridViewCellStyle7
        Me.tanggal_sampai.HeaderText = "Tanggal Sampai"
        Me.tanggal_sampai.Name = "tanggal_sampai"
        Me.tanggal_sampai.ReadOnly = True
        Me.tanggal_sampai.Width = 150
        '
        'status
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.status.DefaultCellStyle = DataGridViewCellStyle8
        Me.status.HeaderText = "Status"
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        Me.status.Width = 300
        '
        'tglSampai
        '
        Me.tglSampai.HeaderText = "tgl_Sampai"
        Me.tglSampai.Name = "tglSampai"
        Me.tglSampai.ReadOnly = True
        Me.tglSampai.Visible = False
        Me.tglSampai.Width = 5
        '
        'ekspedisi
        '
        Me.ekspedisi.HeaderText = "ekspedisi"
        Me.ekspedisi.Name = "ekspedisi"
        Me.ekspedisi.ReadOnly = True
        Me.ekspedisi.Visible = False
        Me.ekspedisi.Width = 5
        '
        'driver
        '
        Me.driver.HeaderText = "driver"
        Me.driver.Name = "driver"
        Me.driver.ReadOnly = True
        Me.driver.Visible = False
        Me.driver.Width = 5
        '
        'noPlat
        '
        Me.noPlat.HeaderText = "noPlat"
        Me.noPlat.Name = "noPlat"
        Me.noPlat.ReadOnly = True
        Me.noPlat.Visible = False
        Me.noPlat.Width = 5
        '
        'eta
        '
        Me.eta.HeaderText = "eta"
        Me.eta.Name = "eta"
        Me.eta.ReadOnly = True
        Me.eta.Visible = False
        Me.eta.Width = 5
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1111, 71)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 695)
        Me.Panel4.TabIndex = 36
        Me.Panel4.Visible = False
        '
        'Lv_DetBahan
        '
        Me.Lv_DetBahan.HideSelection = False
        Me.Lv_DetBahan.Location = New System.Drawing.Point(6, 19)
        Me.Lv_DetBahan.Name = "Lv_DetBahan"
        Me.Lv_DetBahan.Size = New System.Drawing.Size(676, 181)
        Me.Lv_DetBahan.TabIndex = 0
        Me.Lv_DetBahan.UseCompatibleStateImageBehavior = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_DetBahan)
        Me.GroupBox2.Location = New System.Drawing.Point(416, 414)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(692, 200)
        Me.GroupBox2.TabIndex = 347
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail Bahan"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(25, 405)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1216, 12)
        Me.Panel5.TabIndex = 35
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(36, 615)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1216, 12)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(33, 426)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(152, 18)
        Me.Label2.TabIndex = 348
        Me.Label2.Text = "Tanggal Keberangkatan"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(33, 587)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 18)
        Me.Label3.TabIndex = 348
        Me.Label3.Text = "Ekspedisi"
        Me.Label3.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(33, 454)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 18)
        Me.Label6.TabIndex = 348
        Me.Label6.Text = "Driver"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(33, 482)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(124, 18)
        Me.Label7.TabIndex = 348
        Me.Label7.Text = "No Plat Kendaraan"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(33, 510)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 18)
        Me.Label8.TabIndex = 348
        Me.Label8.Text = "ETA"
        '
        'Tb_TanggalBerangkat
        '
        Me.Tb_TanggalBerangkat.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Tb_TanggalBerangkat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tb_TanggalBerangkat.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Tb_TanggalBerangkat.Location = New System.Drawing.Point(196, 426)
        Me.Tb_TanggalBerangkat.MaxLength = 50
        Me.Tb_TanggalBerangkat.Name = "Tb_TanggalBerangkat"
        Me.Tb_TanggalBerangkat.ReadOnly = True
        Me.Tb_TanggalBerangkat.Size = New System.Drawing.Size(189, 22)
        Me.Tb_TanggalBerangkat.TabIndex = 0
        '
        'Tb_Ekspedisi
        '
        Me.Tb_Ekspedisi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Tb_Ekspedisi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tb_Ekspedisi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Tb_Ekspedisi.Location = New System.Drawing.Point(196, 586)
        Me.Tb_Ekspedisi.MaxLength = 50
        Me.Tb_Ekspedisi.Name = "Tb_Ekspedisi"
        Me.Tb_Ekspedisi.ReadOnly = True
        Me.Tb_Ekspedisi.Size = New System.Drawing.Size(189, 22)
        Me.Tb_Ekspedisi.TabIndex = 1
        Me.Tb_Ekspedisi.Visible = False
        '
        'Tb_Driver
        '
        Me.Tb_Driver.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Tb_Driver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tb_Driver.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Tb_Driver.Location = New System.Drawing.Point(196, 453)
        Me.Tb_Driver.MaxLength = 50
        Me.Tb_Driver.Name = "Tb_Driver"
        Me.Tb_Driver.ReadOnly = True
        Me.Tb_Driver.Size = New System.Drawing.Size(189, 22)
        Me.Tb_Driver.TabIndex = 2
        '
        'Tb_NoPlat
        '
        Me.Tb_NoPlat.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Tb_NoPlat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tb_NoPlat.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Tb_NoPlat.Location = New System.Drawing.Point(196, 481)
        Me.Tb_NoPlat.MaxLength = 50
        Me.Tb_NoPlat.Name = "Tb_NoPlat"
        Me.Tb_NoPlat.ReadOnly = True
        Me.Tb_NoPlat.Size = New System.Drawing.Size(189, 22)
        Me.Tb_NoPlat.TabIndex = 3
        '
        'Tb_ETA
        '
        Me.Tb_ETA.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Tb_ETA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tb_ETA.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Tb_ETA.Location = New System.Drawing.Point(196, 509)
        Me.Tb_ETA.MaxLength = 50
        Me.Tb_ETA.Name = "Tb_ETA"
        Me.Tb_ETA.ReadOnly = True
        Me.Tb_ETA.Size = New System.Drawing.Size(189, 22)
        Me.Tb_ETA.TabIndex = 4
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(335, 67)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(184, 20)
        Me.DateTimePicker2.TabIndex = 350
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(299, 69)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(25, 16)
        Me.Label5.TabIndex = 351
        Me.Label5.Text = "s/d"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(105, 67)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(184, 20)
        Me.DateTimePicker1.TabIndex = 349
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(654, 63)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(111, 28)
        Me.Btn_Refresh.TabIndex = 343
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Display_Tracking_Kendaraan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1128, 622)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Tb_Ekspedisi)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Tb_ETA)
        Me.Controls.Add(Me.Tb_NoPlat)
        Me.Controls.Add(Me.Tb_Driver)
        Me.Controls.Add(Me.Tb_TanggalBerangkat)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Display_Tracking_Kendaraan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.Dgv_Tracking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Dgv_Tracking As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_DetBahan As ListView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Tb_TanggalBerangkat As TextBox
    Friend WithEvents Tb_Ekspedisi As TextBox
    Friend WithEvents Tb_Driver As TextBox
    Friend WithEvents Tb_NoPlat As TextBox
    Friend WithEvents Tb_ETA As TextBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents no_faktur As DataGridViewTextBoxColumn
    Friend WithEvents lokasi As DataGridViewTextBoxColumn
    Friend WithEvents supplier As DataGridViewTextBoxColumn
    Friend WithEvents no_sj As DataGridViewTextBoxColumn
    Friend WithEvents tanggal_sampai As DataGridViewTextBoxColumn
    Friend WithEvents status As DataGridViewTextBoxColumn
    Friend WithEvents tglSampai As DataGridViewTextBoxColumn
    Friend WithEvents ekspedisi As DataGridViewTextBoxColumn
    Friend WithEvents driver As DataGridViewTextBoxColumn
    Friend WithEvents noPlat As DataGridViewTextBoxColumn
    Friend WithEvents eta As DataGridViewTextBoxColumn
End Class
