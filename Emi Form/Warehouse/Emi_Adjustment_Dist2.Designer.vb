<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Emi_Adjustment_Dist2
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Cmb_So = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lv_PilihBarang = New System.Windows.Forms.ListView()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Dtp_Expred = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Dtp_Produksi = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Id_Position = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Txt_AdjustBags = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Txt_JmlhAdj = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Txt_Position = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Txt_HPP = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Txt_SisaBags = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Txt_SisaStock = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Txt_NmBarangAdj = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Txt_SN = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txt_KdBarangAdj = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_BarangSNPallet = New System.Windows.Forms.ListView()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Txt_SatuanAkhir = New System.Windows.Forms.TextBox()
        Me.txt_satuan = New System.Windows.Forms.TextBox()
        Me.Txt_SO = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1281, 52)
        Me.Panel1.TabIndex = 319
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 50)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1281, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(18, 11)
        Me.Label13.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(258, 30)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Adjustment - Per Pallet"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 50)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1556, 15)
        Me.Panel2.TabIndex = 320
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 65)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(22, 602)
        Me.Panel3.TabIndex = 321
        Me.Panel3.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox1.Location = New System.Drawing.Point(420, 66)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.MaxLength = 11
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(190, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(286, 67)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 20)
        Me.Label3.TabIndex = 325
        Me.Label3.Text = "Kode Adjustment"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(95, 66)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(172, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(28, 66)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 20)
        Me.Label2.TabIndex = 322
        Me.Label2.Text = "Tanggal"
        '
        'Cmb_So
        '
        Me.Cmb_So.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_So.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_So.FormattingEnabled = True
        Me.Cmb_So.Location = New System.Drawing.Point(144, 17)
        Me.Cmb_So.Margin = New System.Windows.Forms.Padding(4)
        Me.Cmb_So.Name = "Cmb_So"
        Me.Cmb_So.Size = New System.Drawing.Size(245, 26)
        Me.Cmb_So.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 20)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(125, 18)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Kode Stock Owner"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_PilihBarang)
        Me.GroupBox1.Controls.Add(Me.Btn_Refresh)
        Me.GroupBox1.Controls.Add(Me.Btn_Simpan)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Panel6)
        Me.GroupBox1.Controls.Add(Me.Panel5)
        Me.GroupBox1.Controls.Add(Me.Lv_BarangSNPallet)
        Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
        Me.GroupBox1.Controls.Add(Me.Txt_SO)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Cmb_So)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 92)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1235, 550)
        Me.GroupBox1.TabIndex = 326
        Me.GroupBox1.TabStop = False
        '
        'Lv_PilihBarang
        '
        Me.Lv_PilihBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_PilihBarang.FullRowSelect = True
        Me.Lv_PilihBarang.HideSelection = False
        Me.Lv_PilihBarang.Location = New System.Drawing.Point(1250, 108)
        Me.Lv_PilihBarang.Name = "Lv_PilihBarang"
        Me.Lv_PilihBarang.Size = New System.Drawing.Size(605, 198)
        Me.Lv_PilihBarang.TabIndex = 447
        Me.Lv_PilihBarang.UseCompatibleStateImageBehavior = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(152, 504)
        Me.Btn_Refresh.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(135, 35)
        Me.Btn_Refresh.TabIndex = 6
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(14, 505)
        Me.Btn_Simpan.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(135, 35)
        Me.Btn_Simpan.TabIndex = 5
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Dtp_Expred)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Dtp_Produksi)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Txt_Keterangan)
        Me.GroupBox2.Controls.Add(Me.Id_Position)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Txt_AdjustBags)
        Me.GroupBox2.Controls.Add(Me.Txt_SatuanAkhir)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.txt_satuan)
        Me.GroupBox2.Controls.Add(Me.Txt_JmlhAdj)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Txt_Position)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Txt_HPP)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Txt_SisaBags)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.Txt_SisaStock)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Txt_NmBarangAdj)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Txt_SN)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Txt_KdBarangAdj)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Panel8)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 111)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(605, 386)
        Me.GroupBox2.TabIndex = 446
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "AdjustStock"
        '
        'Dtp_Expred
        '
        Me.Dtp_Expred.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtp_Expred.Location = New System.Drawing.Point(150, 346)
        Me.Dtp_Expred.Name = "Dtp_Expred"
        Me.Dtp_Expred.Size = New System.Drawing.Size(240, 23)
        Me.Dtp_Expred.TabIndex = 11
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(13, 353)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 18)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Tgl Expired"
        '
        'Dtp_Produksi
        '
        Me.Dtp_Produksi.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtp_Produksi.Location = New System.Drawing.Point(150, 316)
        Me.Dtp_Produksi.Name = "Dtp_Produksi"
        Me.Dtp_Produksi.Size = New System.Drawing.Size(240, 23)
        Me.Dtp_Produksi.TabIndex = 10
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(13, 323)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(84, 18)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Tgl Produksi"
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Keterangan.Location = New System.Drawing.Point(149, 286)
        Me.Txt_Keterangan.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_Keterangan.MaxLength = 40
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(308, 23)
        Me.Txt_Keterangan.TabIndex = 9
        '
        'Id_Position
        '
        Me.Id_Position.AutoSize = True
        Me.Id_Position.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Id_Position.Location = New System.Drawing.Point(522, 11)
        Me.Id_Position.Name = "Id_Position"
        Me.Id_Position.Size = New System.Drawing.Size(77, 18)
        Me.Id_Position.TabIndex = 0
        Me.Id_Position.Text = "Id_Position"
        Me.Id_Position.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(13, 289)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(79, 18)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Keterangan"
        '
        'Txt_AdjustBags
        '
        Me.Txt_AdjustBags.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_AdjustBags.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_AdjustBags.Location = New System.Drawing.Point(149, 258)
        Me.Txt_AdjustBags.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_AdjustBags.MaxLength = 40
        Me.Txt_AdjustBags.Name = "Txt_AdjustBags"
        Me.Txt_AdjustBags.Size = New System.Drawing.Size(308, 23)
        Me.Txt_AdjustBags.TabIndex = 8
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(13, 261)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(130, 18)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Jumlah Adjust Bags"
        '
        'Txt_JmlhAdj
        '
        Me.Txt_JmlhAdj.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_JmlhAdj.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_JmlhAdj.Location = New System.Drawing.Point(149, 230)
        Me.Txt_JmlhAdj.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_JmlhAdj.MaxLength = 40
        Me.Txt_JmlhAdj.Name = "Txt_JmlhAdj"
        Me.Txt_JmlhAdj.Size = New System.Drawing.Size(308, 23)
        Me.Txt_JmlhAdj.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(13, 233)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(97, 18)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Jumlah Adjust"
        '
        'Txt_Position
        '
        Me.Txt_Position.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Position.Enabled = False
        Me.Txt_Position.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Position.Location = New System.Drawing.Point(150, 81)
        Me.Txt_Position.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_Position.MaxLength = 40
        Me.Txt_Position.Name = "Txt_Position"
        Me.Txt_Position.Size = New System.Drawing.Size(308, 23)
        Me.Txt_Position.TabIndex = 2
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(13, 85)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 17)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Position"
        '
        'Txt_HPP
        '
        Me.Txt_HPP.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_HPP.Enabled = False
        Me.Txt_HPP.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_HPP.Location = New System.Drawing.Point(149, 201)
        Me.Txt_HPP.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_HPP.MaxLength = 40
        Me.Txt_HPP.Name = "Txt_HPP"
        Me.Txt_HPP.Size = New System.Drawing.Size(308, 23)
        Me.Txt_HPP.TabIndex = 6
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(14, 205)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(31, 17)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "HPP"
        '
        'Txt_SisaBags
        '
        Me.Txt_SisaBags.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SisaBags.Enabled = False
        Me.Txt_SisaBags.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_SisaBags.Location = New System.Drawing.Point(149, 170)
        Me.Txt_SisaBags.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_SisaBags.MaxLength = 40
        Me.Txt_SisaBags.Name = "Txt_SisaBags"
        Me.Txt_SisaBags.Size = New System.Drawing.Size(308, 23)
        Me.Txt_SisaBags.TabIndex = 5
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(13, 173)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(66, 18)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Sisa Bags"
        '
        'Txt_SisaStock
        '
        Me.Txt_SisaStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SisaStock.Enabled = False
        Me.Txt_SisaStock.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_SisaStock.Location = New System.Drawing.Point(149, 140)
        Me.Txt_SisaStock.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_SisaStock.MaxLength = 40
        Me.Txt_SisaStock.Name = "Txt_SisaStock"
        Me.Txt_SisaStock.Size = New System.Drawing.Size(308, 23)
        Me.Txt_SisaStock.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 143)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 18)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Sisa Stock"
        '
        'Txt_NmBarangAdj
        '
        Me.Txt_NmBarangAdj.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmBarangAdj.Enabled = False
        Me.Txt_NmBarangAdj.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NmBarangAdj.Location = New System.Drawing.Point(149, 110)
        Me.Txt_NmBarangAdj.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_NmBarangAdj.MaxLength = 40
        Me.Txt_NmBarangAdj.Name = "Txt_NmBarangAdj"
        Me.Txt_NmBarangAdj.Size = New System.Drawing.Size(308, 23)
        Me.Txt_NmBarangAdj.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(13, 113)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 18)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Nama Barang"
        '
        'Txt_SN
        '
        Me.Txt_SN.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SN.Enabled = False
        Me.Txt_SN.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_SN.Location = New System.Drawing.Point(150, 52)
        Me.Txt_SN.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_SN.MaxLength = 40
        Me.Txt_SN.Name = "Txt_SN"
        Me.Txt_SN.Size = New System.Drawing.Size(308, 23)
        Me.Txt_SN.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(14, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 18)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Serial Number"
        '
        'Txt_KdBarangAdj
        '
        Me.Txt_KdBarangAdj.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarangAdj.Enabled = False
        Me.Txt_KdBarangAdj.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_KdBarangAdj.Location = New System.Drawing.Point(150, 22)
        Me.Txt_KdBarangAdj.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_KdBarangAdj.MaxLength = 40
        Me.Txt_KdBarangAdj.Name = "Txt_KdBarangAdj"
        Me.Txt_KdBarangAdj.Size = New System.Drawing.Size(308, 23)
        Me.Txt_KdBarangAdj.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(14, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 18)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Kode Barang"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(2, 16)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(10, 353)
        Me.Panel8.TabIndex = 321
        Me.Panel8.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(1225, 18)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(10, 353)
        Me.Panel6.TabIndex = 321
        Me.Panel6.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 21)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(10, 353)
        Me.Panel5.TabIndex = 321
        Me.Panel5.Visible = False
        '
        'Lv_BarangSNPallet
        '
        Me.Lv_BarangSNPallet.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_BarangSNPallet.FullRowSelect = True
        Me.Lv_BarangSNPallet.HideSelection = False
        Me.Lv_BarangSNPallet.Location = New System.Drawing.Point(632, 56)
        Me.Lv_BarangSNPallet.Name = "Lv_BarangSNPallet"
        Me.Lv_BarangSNPallet.Size = New System.Drawing.Size(592, 441)
        Me.Lv_BarangSNPallet.TabIndex = 4
        Me.Lv_BarangSNPallet.UseCompatibleStateImageBehavior = False
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmBarang.Enabled = False
        Me.Txt_NmBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NmBarang.Location = New System.Drawing.Point(306, 82)
        Me.Txt_NmBarang.MaxLength = 50
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.ReadOnly = True
        Me.Txt_NmBarang.Size = New System.Drawing.Size(311, 22)
        Me.Txt_NmBarang.TabIndex = 3
        '
        'Txt_SatuanAkhir
        '
        Me.Txt_SatuanAkhir.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SatuanAkhir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SatuanAkhir.Enabled = False
        Me.Txt_SatuanAkhir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_SatuanAkhir.Location = New System.Drawing.Point(525, 53)
        Me.Txt_SatuanAkhir.MaxLength = 50
        Me.Txt_SatuanAkhir.Name = "Txt_SatuanAkhir"
        Me.Txt_SatuanAkhir.ReadOnly = True
        Me.Txt_SatuanAkhir.Size = New System.Drawing.Size(74, 22)
        Me.Txt_SatuanAkhir.TabIndex = 2
        Me.Txt_SatuanAkhir.Visible = False
        '
        'txt_satuan
        '
        Me.txt_satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_satuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_satuan.Enabled = False
        Me.txt_satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_satuan.Location = New System.Drawing.Point(525, 29)
        Me.txt_satuan.MaxLength = 50
        Me.txt_satuan.Name = "txt_satuan"
        Me.txt_satuan.ReadOnly = True
        Me.txt_satuan.Size = New System.Drawing.Size(74, 22)
        Me.txt_satuan.TabIndex = 2
        Me.txt_satuan.Visible = False
        '
        'Txt_SO
        '
        Me.Txt_SO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SO.Enabled = False
        Me.Txt_SO.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_SO.Location = New System.Drawing.Point(130, 82)
        Me.Txt_SO.MaxLength = 50
        Me.Txt_SO.Name = "Txt_SO"
        Me.Txt_SO.ReadOnly = True
        Me.Txt_SO.Size = New System.Drawing.Size(170, 22)
        Me.Txt_SO.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(130, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 21)
        Me.Label1.TabIndex = 441
        Me.Label1.Text = "Stock Owner"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(306, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(311, 21)
        Me.Label7.TabIndex = 442
        Me.Label7.Text = "Nama Barang"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_KdBarang.Location = New System.Drawing.Point(12, 82)
        Me.Txt_KdBarang.MaxLength = 50
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(112, 22)
        Me.Txt_KdBarang.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 21)
        Me.Label6.TabIndex = 439
        Me.Label6.Text = "Kode Barang"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1259, 67)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(22, 602)
        Me.Panel4.TabIndex = 321
        Me.Panel4.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(37, 640)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1556, 15)
        Me.Panel7.TabIndex = 320
        Me.Panel7.Visible = False
        '
        'Emi_Adjustment_Dist2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1281, 658)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Adjustment_Dist2"
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
    Friend WithEvents Label13 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Cmb_So As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Txt_SO As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Lv_BarangSNPallet As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_KdBarangAdj As TextBox
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Txt_NmBarangAdj As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Txt_SN As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Txt_SisaStock As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Txt_HPP As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Txt_JmlhAdj As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Dtp_Produksi As DateTimePicker
    Friend WithEvents Label15 As Label
    Friend WithEvents Dtp_Expred As DateTimePicker
    Friend WithEvents Label16 As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Lv_PilihBarang As ListView
    Friend WithEvents Txt_Position As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Id_Position As Label
    Friend WithEvents txt_satuan As TextBox
    Friend WithEvents Txt_AdjustBags As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Txt_SisaBags As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Txt_SatuanAkhir As TextBox
End Class
