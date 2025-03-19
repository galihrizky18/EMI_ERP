<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Master_Routing
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Txt_Kd = New System.Windows.Forms.TextBox()
        Me.Lbl_Kd = New System.Windows.Forms.Label()
        Me.Lbl_Keterangan = New System.Windows.Forms.Label()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Cmb_Kolom = New System.Windows.Forms.ComboBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Lbl_Value = New System.Windows.Forms.Label()
        Me.Lbl_Kolom = New System.Windows.Forms.Label()
        Me.Txt_Value = New System.Windows.Forms.TextBox()
        Me.Lv_Routing = New System.Windows.Forms.ListView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lbl_IdRouting = New System.Windows.Forms.Label()
        Me.Lv_RoutingDetail = New System.Windows.Forms.ListView()
        Me.Cmb_DataWorkCenter = New System.Windows.Forms.ComboBox()
        Me.Btn_WorkCenter = New System.Windows.Forms.Button()
        Me.Lv_DataWorkCenter = New System.Windows.Forms.ListView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_PrefixCode = New System.Windows.Forms.TextBox()
        Me.Btn_PilihBarColor = New System.Windows.Forms.Button()
        Me.Txt_Barcolor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Btn_DisplayWarna = New System.Windows.Forms.Button()
        Me.Btn_DisplayWarnaBackColor = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_Backcolor = New System.Windows.Forms.TextBox()
        Me.Btn_PilihBackColor = New System.Windows.Forms.Button()
        Me.Cmb_JnsProduk = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_Qty_PerBatch = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_SatuanBatch = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(651, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(651, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(242, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master Data - Routing"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 789)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Txt_Kd
        '
        Me.Txt_Kd.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kd.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Kd.Location = New System.Drawing.Point(126, 66)
        Me.Txt_Kd.MaxLength = 50
        Me.Txt_Kd.Name = "Txt_Kd"
        Me.Txt_Kd.Size = New System.Drawing.Size(498, 22)
        Me.Txt_Kd.TabIndex = 0
        '
        'Lbl_Kd
        '
        Me.Lbl_Kd.AutoSize = True
        Me.Lbl_Kd.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kd.Location = New System.Drawing.Point(16, 66)
        Me.Lbl_Kd.Name = "Lbl_Kd"
        Me.Lbl_Kd.Size = New System.Drawing.Size(42, 20)
        Me.Lbl_Kd.TabIndex = 229
        Me.Lbl_Kd.Text = "Kode"
        '
        'Lbl_Keterangan
        '
        Me.Lbl_Keterangan.AutoSize = True
        Me.Lbl_Keterangan.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Keterangan.Location = New System.Drawing.Point(16, 99)
        Me.Lbl_Keterangan.Name = "Lbl_Keterangan"
        Me.Lbl_Keterangan.Size = New System.Drawing.Size(86, 20)
        Me.Lbl_Keterangan.TabIndex = 230
        Me.Lbl_Keterangan.Text = "Keterangan"
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(126, 98)
        Me.Txt_Keterangan.MaxLength = 50
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(498, 22)
        Me.Txt_Keterangan.TabIndex = 1
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(18, 471)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(924, 12)
        Me.Panel6.TabIndex = 232
        Me.Panel6.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(198, 485)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 9
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(108, 485)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Hapus.TabIndex = 10
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(18, 485)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 8
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(10, 523)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(938, 19)
        Me.Panel7.TabIndex = 236
        Me.Panel7.Visible = False
        '
        'Cmb_Kolom
        '
        Me.Cmb_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Kolom.DropDownWidth = 150
        Me.Cmb_Kolom.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.Cmb_Kolom.FormattingEnabled = True
        Me.Cmb_Kolom.Location = New System.Drawing.Point(61, 13)
        Me.Cmb_Kolom.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_Kolom.Name = "Cmb_Kolom"
        Me.Cmb_Kolom.Size = New System.Drawing.Size(195, 25)
        Me.Cmb_Kolom.TabIndex = 11
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(508, 10)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Cari.TabIndex = 13
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Lbl_Value
        '
        Me.Lbl_Value.AutoSize = True
        Me.Lbl_Value.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Value.Location = New System.Drawing.Point(261, 15)
        Me.Lbl_Value.Name = "Lbl_Value"
        Me.Lbl_Value.Size = New System.Drawing.Size(46, 20)
        Me.Lbl_Value.TabIndex = 342
        Me.Lbl_Value.Text = "Value"
        '
        'Lbl_Kolom
        '
        Me.Lbl_Kolom.AutoSize = True
        Me.Lbl_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kolom.Location = New System.Drawing.Point(6, 15)
        Me.Lbl_Kolom.Name = "Lbl_Kolom"
        Me.Lbl_Kolom.Size = New System.Drawing.Size(50, 20)
        Me.Lbl_Kolom.TabIndex = 341
        Me.Lbl_Kolom.Text = "Kolom"
        '
        'Txt_Value
        '
        Me.Txt_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Value.Location = New System.Drawing.Point(313, 14)
        Me.Txt_Value.MaxLength = 50
        Me.Txt_Value.Name = "Txt_Value"
        Me.Txt_Value.Size = New System.Drawing.Size(189, 22)
        Me.Txt_Value.TabIndex = 12
        '
        'Lv_Routing
        '
        Me.Lv_Routing.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_Routing.FullRowSelect = True
        Me.Lv_Routing.GridLines = True
        Me.Lv_Routing.HideSelection = False
        Me.Lv_Routing.Location = New System.Drawing.Point(7, 43)
        Me.Lv_Routing.Name = "Lv_Routing"
        Me.Lv_Routing.OwnerDraw = True
        Me.Lv_Routing.Size = New System.Drawing.Size(341, 233)
        Me.Lv_Routing.TabIndex = 343
        Me.Lv_Routing.UseCompatibleStateImageBehavior = False
        Me.Lv_Routing.View = System.Windows.Forms.View.Details
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(631, 83)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 766)
        Me.Panel5.TabIndex = 344
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(-247, 827)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 345
        Me.Panel4.Visible = False
        '
        'Lbl_IdRouting
        '
        Me.Lbl_IdRouting.AutoSize = True
        Me.Lbl_IdRouting.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_IdRouting.Location = New System.Drawing.Point(652, 67)
        Me.Lbl_IdRouting.Name = "Lbl_IdRouting"
        Me.Lbl_IdRouting.Size = New System.Drawing.Size(79, 20)
        Me.Lbl_IdRouting.TabIndex = 346
        Me.Lbl_IdRouting.Text = "Id Routing"
        Me.Lbl_IdRouting.Visible = False
        '
        'Lv_RoutingDetail
        '
        Me.Lv_RoutingDetail.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_RoutingDetail.FullRowSelect = True
        Me.Lv_RoutingDetail.GridLines = True
        Me.Lv_RoutingDetail.HideSelection = False
        Me.Lv_RoutingDetail.Location = New System.Drawing.Point(354, 43)
        Me.Lv_RoutingDetail.Name = "Lv_RoutingDetail"
        Me.Lv_RoutingDetail.Size = New System.Drawing.Size(244, 233)
        Me.Lv_RoutingDetail.TabIndex = 347
        Me.Lv_RoutingDetail.UseCompatibleStateImageBehavior = False
        Me.Lv_RoutingDetail.View = System.Windows.Forms.View.Details
        '
        'Cmb_DataWorkCenter
        '
        Me.Cmb_DataWorkCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_DataWorkCenter.DropDownWidth = 150
        Me.Cmb_DataWorkCenter.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.Cmb_DataWorkCenter.FormattingEnabled = True
        Me.Cmb_DataWorkCenter.Location = New System.Drawing.Point(126, 255)
        Me.Cmb_DataWorkCenter.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_DataWorkCenter.Name = "Cmb_DataWorkCenter"
        Me.Cmb_DataWorkCenter.Size = New System.Drawing.Size(492, 25)
        Me.Cmb_DataWorkCenter.TabIndex = 6
        '
        'Btn_WorkCenter
        '
        Me.Btn_WorkCenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_WorkCenter.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_WorkCenter.ForeColor = System.Drawing.Color.White
        Me.Btn_WorkCenter.Location = New System.Drawing.Point(446, 286)
        Me.Btn_WorkCenter.Name = "Btn_WorkCenter"
        Me.Btn_WorkCenter.Size = New System.Drawing.Size(104, 28)
        Me.Btn_WorkCenter.TabIndex = 7
        Me.Btn_WorkCenter.Text = "OK"
        Me.Btn_WorkCenter.UseVisualStyleBackColor = False
        '
        'Lv_DataWorkCenter
        '
        Me.Lv_DataWorkCenter.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_DataWorkCenter.FullRowSelect = True
        Me.Lv_DataWorkCenter.GridLines = True
        Me.Lv_DataWorkCenter.HideSelection = False
        Me.Lv_DataWorkCenter.Location = New System.Drawing.Point(8, 19)
        Me.Lv_DataWorkCenter.Name = "Lv_DataWorkCenter"
        Me.Lv_DataWorkCenter.Size = New System.Drawing.Size(590, 132)
        Me.Lv_DataWorkCenter.TabIndex = 350
        Me.Lv_DataWorkCenter.UseCompatibleStateImageBehavior = False
        Me.Lv_DataWorkCenter.View = System.Windows.Forms.View.Details
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(16, 256)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 20)
        Me.Label3.TabIndex = 353
        Me.Label3.Text = "Work Center"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cmb_Kolom)
        Me.GroupBox1.Controls.Add(Me.Txt_Value)
        Me.GroupBox1.Controls.Add(Me.Lbl_Kolom)
        Me.GroupBox1.Controls.Add(Me.Lbl_Value)
        Me.GroupBox1.Controls.Add(Me.Btn_Cari)
        Me.GroupBox1.Controls.Add(Me.Lv_Routing)
        Me.GroupBox1.Controls.Add(Me.Lv_RoutingDetail)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 544)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(604, 288)
        Me.GroupBox1.TabIndex = 354
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Display"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_DataWorkCenter)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 323)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(604, 160)
        Me.GroupBox2.TabIndex = 355
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Data Work Center"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(16, 130)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 20)
        Me.Label1.TabIndex = 356
        Me.Label1.Text = "Prefix Code"
        '
        'Txt_PrefixCode
        '
        Me.Txt_PrefixCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_PrefixCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_PrefixCode.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_PrefixCode.Location = New System.Drawing.Point(126, 129)
        Me.Txt_PrefixCode.MaxLength = 2
        Me.Txt_PrefixCode.Name = "Txt_PrefixCode"
        Me.Txt_PrefixCode.Size = New System.Drawing.Size(498, 22)
        Me.Txt_PrefixCode.TabIndex = 2
        '
        'Btn_PilihBarColor
        '
        Me.Btn_PilihBarColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_PilihBarColor.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_PilihBarColor.ForeColor = System.Drawing.Color.White
        Me.Btn_PilihBarColor.Location = New System.Drawing.Point(446, 186)
        Me.Btn_PilihBarColor.Name = "Btn_PilihBarColor"
        Me.Btn_PilihBarColor.Size = New System.Drawing.Size(178, 28)
        Me.Btn_PilihBarColor.TabIndex = 4
        Me.Btn_PilihBarColor.Text = "Pilih Bar Color"
        Me.Btn_PilihBarColor.UseVisualStyleBackColor = False
        '
        'Txt_Barcolor
        '
        Me.Txt_Barcolor.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Barcolor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Barcolor.Enabled = False
        Me.Txt_Barcolor.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Barcolor.Location = New System.Drawing.Point(126, 188)
        Me.Txt_Barcolor.MaxLength = 50
        Me.Txt_Barcolor.Name = "Txt_Barcolor"
        Me.Txt_Barcolor.Size = New System.Drawing.Size(242, 22)
        Me.Txt_Barcolor.TabIndex = 358
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(16, 189)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 20)
        Me.Label2.TabIndex = 359
        Me.Label2.Text = "Bar Color"
        '
        'Btn_DisplayWarna
        '
        Me.Btn_DisplayWarna.Enabled = False
        Me.Btn_DisplayWarna.Location = New System.Drawing.Point(374, 188)
        Me.Btn_DisplayWarna.Name = "Btn_DisplayWarna"
        Me.Btn_DisplayWarna.Size = New System.Drawing.Size(66, 23)
        Me.Btn_DisplayWarna.TabIndex = 360
        Me.Btn_DisplayWarna.UseVisualStyleBackColor = True
        '
        'Btn_DisplayWarnaBackColor
        '
        Me.Btn_DisplayWarnaBackColor.Enabled = False
        Me.Btn_DisplayWarnaBackColor.Location = New System.Drawing.Point(374, 222)
        Me.Btn_DisplayWarnaBackColor.Name = "Btn_DisplayWarnaBackColor"
        Me.Btn_DisplayWarnaBackColor.Size = New System.Drawing.Size(66, 23)
        Me.Btn_DisplayWarnaBackColor.TabIndex = 364
        Me.Btn_DisplayWarnaBackColor.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(16, 223)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 20)
        Me.Label4.TabIndex = 363
        Me.Label4.Text = "Back Color"
        '
        'Txt_Backcolor
        '
        Me.Txt_Backcolor.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Backcolor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Backcolor.Enabled = False
        Me.Txt_Backcolor.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Backcolor.Location = New System.Drawing.Point(126, 222)
        Me.Txt_Backcolor.MaxLength = 50
        Me.Txt_Backcolor.Name = "Txt_Backcolor"
        Me.Txt_Backcolor.Size = New System.Drawing.Size(242, 22)
        Me.Txt_Backcolor.TabIndex = 362
        '
        'Btn_PilihBackColor
        '
        Me.Btn_PilihBackColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_PilihBackColor.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_PilihBackColor.ForeColor = System.Drawing.Color.White
        Me.Btn_PilihBackColor.Location = New System.Drawing.Point(446, 220)
        Me.Btn_PilihBackColor.Name = "Btn_PilihBackColor"
        Me.Btn_PilihBackColor.Size = New System.Drawing.Size(178, 28)
        Me.Btn_PilihBackColor.TabIndex = 5
        Me.Btn_PilihBackColor.Text = "Pilih Back Color"
        Me.Btn_PilihBackColor.UseVisualStyleBackColor = False
        '
        'Cmb_JnsProduk
        '
        Me.Cmb_JnsProduk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_JnsProduk.DropDownWidth = 150
        Me.Cmb_JnsProduk.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.Cmb_JnsProduk.FormattingEnabled = True
        Me.Cmb_JnsProduk.Location = New System.Drawing.Point(125, 156)
        Me.Cmb_JnsProduk.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_JnsProduk.Name = "Cmb_JnsProduk"
        Me.Cmb_JnsProduk.Size = New System.Drawing.Size(499, 25)
        Me.Cmb_JnsProduk.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(16, 158)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 20)
        Me.Label5.TabIndex = 366
        Me.Label5.Text = "Jenis Produk"
        '
        'Txt_Qty_PerBatch
        '
        Me.Txt_Qty_PerBatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Qty_PerBatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Qty_PerBatch.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Qty_PerBatch.Location = New System.Drawing.Point(126, 288)
        Me.Txt_Qty_PerBatch.MaxLength = 100
        Me.Txt_Qty_PerBatch.Name = "Txt_Qty_PerBatch"
        Me.Txt_Qty_PerBatch.Size = New System.Drawing.Size(242, 22)
        Me.Txt_Qty_PerBatch.TabIndex = 2
        Me.Txt_Qty_PerBatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(16, 289)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 20)
        Me.Label6.TabIndex = 356
        Me.Label6.Text = "Qty Per Batch"
        '
        'Cmb_SatuanBatch
        '
        Me.Cmb_SatuanBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_SatuanBatch.DropDownWidth = 150
        Me.Cmb_SatuanBatch.Enabled = False
        Me.Cmb_SatuanBatch.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.Cmb_SatuanBatch.FormattingEnabled = True
        Me.Cmb_SatuanBatch.Location = New System.Drawing.Point(373, 287)
        Me.Cmb_SatuanBatch.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_SatuanBatch.Name = "Cmb_SatuanBatch"
        Me.Cmb_SatuanBatch.Size = New System.Drawing.Size(68, 25)
        Me.Cmb_SatuanBatch.TabIndex = 6
        '
        'Master_Routing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(651, 844)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Lbl_Keterangan)
        Me.Controls.Add(Me.Lbl_Kd)
        Me.Controls.Add(Me.Cmb_JnsProduk)
        Me.Controls.Add(Me.Btn_DisplayWarnaBackColor)
        Me.Controls.Add(Me.Txt_Backcolor)
        Me.Controls.Add(Me.Btn_PilihBackColor)
        Me.Controls.Add(Me.Btn_DisplayWarna)
        Me.Controls.Add(Me.Txt_Barcolor)
        Me.Controls.Add(Me.Btn_PilihBarColor)
        Me.Controls.Add(Me.Txt_Qty_PerBatch)
        Me.Controls.Add(Me.Txt_PrefixCode)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Btn_WorkCenter)
        Me.Controls.Add(Me.Cmb_SatuanBatch)
        Me.Controls.Add(Me.Cmb_DataWorkCenter)
        Me.Controls.Add(Me.Lbl_IdRouting)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.Txt_Kd)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Master_Routing"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Txt_Kd As TextBox
    Friend WithEvents Lbl_Kd As Label
    Friend WithEvents Lbl_Keterangan As Label
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Cmb_Kolom As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Lbl_Value As Label
    Friend WithEvents Lbl_Kolom As Label
    Friend WithEvents Txt_Value As TextBox
    Friend WithEvents Lv_Routing As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lbl_IdRouting As Label
    Friend WithEvents Lv_RoutingDetail As ListView
    Friend WithEvents Cmb_DataWorkCenter As ComboBox
    Friend WithEvents Btn_WorkCenter As Button
    Friend WithEvents Lv_DataWorkCenter As ListView
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_PrefixCode As TextBox
    Friend WithEvents Btn_PilihBarColor As Button
    Friend WithEvents Txt_Barcolor As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Btn_DisplayWarna As Button
    Friend WithEvents Btn_DisplayWarnaBackColor As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_Backcolor As TextBox
    Friend WithEvents Btn_PilihBackColor As Button
    Friend WithEvents Cmb_JnsProduk As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_Qty_PerBatch As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_SatuanBatch As ComboBox
End Class
