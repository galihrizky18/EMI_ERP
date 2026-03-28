<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Quality_Control
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Kode = New System.Windows.Forms.TextBox()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Lv_DataQC = New System.Windows.Forms.ListView()
        Me.Txt_FilterValue = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Cmb_FilterValue = New System.Windows.Forms.ComboBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.Cmb_Jenis = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Lv_Switch = New System.Windows.Forms.ListView()
        Me.Txt_Switch = New System.Windows.Forms.TextBox()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Rb_LapanganYa = New System.Windows.Forms.RadioButton()
        Me.Rb_LapanganTdk = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Rb_LabYa = New System.Windows.Forms.RadioButton()
        Me.Rb_LabTdk = New System.Windows.Forms.RadioButton()
        Me.Txt_RangeAwal = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Txt_RangeAkhir = New System.Windows.Forms.TextBox()
        Me.GrBox_Range = New System.Windows.Forms.GroupBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Tb_Lapangan = New System.Windows.Forms.RadioButton()
        Me.Rb_Lab = New System.Windows.Forms.RadioButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.GrBox_Range.SuspendLayout()
        Me.Panel8.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(942, 51)
        Me.Panel1.TabIndex = 22
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
        Me.PanelGradient1.Size = New System.Drawing.Size(942, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(320, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Master Data - Quality Control"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 486)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(922, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 486)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(-8, 518)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1462, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(23, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 20)
        Me.Label2.TabIndex = 227
        Me.Label2.Text = "xKode"
        '
        'Txt_Kode
        '
        Me.Txt_Kode.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kode.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Kode.Location = New System.Drawing.Point(211, 63)
        Me.Txt_Kode.MaxLength = 50
        Me.Txt_Kode.Name = "Txt_Kode"
        Me.Txt_Kode.Size = New System.Drawing.Size(228, 22)
        Me.Txt_Kode.TabIndex = 0
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(211, 89)
        Me.Txt_Keterangan.MaxLength = 100
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(389, 22)
        Me.Txt_Keterangan.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(23, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 20)
        Me.Label3.TabIndex = 229
        Me.Label3.Text = "xKeterangan"
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(211, 228)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 4
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(301, 228)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Hapus.TabIndex = 5
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(391, 228)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 6
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Lv_DataQC
        '
        Me.Lv_DataQC.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_DataQC.FullRowSelect = True
        Me.Lv_DataQC.GridLines = True
        Me.Lv_DataQC.HideSelection = False
        Me.Lv_DataQC.Location = New System.Drawing.Point(21, 317)
        Me.Lv_DataQC.Name = "Lv_DataQC"
        Me.Lv_DataQC.Size = New System.Drawing.Size(900, 200)
        Me.Lv_DataQC.TabIndex = 234
        Me.Lv_DataQC.UseCompatibleStateImageBehavior = False
        Me.Lv_DataQC.View = System.Windows.Forms.View.Details
        '
        'Txt_FilterValue
        '
        Me.Txt_FilterValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_FilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_FilterValue.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_FilterValue.Location = New System.Drawing.Point(362, 287)
        Me.Txt_FilterValue.MaxLength = 50
        Me.Txt_FilterValue.Name = "Txt_FilterValue"
        Me.Txt_FilterValue.Size = New System.Drawing.Size(189, 22)
        Me.Txt_FilterValue.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(27, 289)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 20)
        Me.Label4.TabIndex = 236
        Me.Label4.Text = "Kolom"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(310, 287)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 20)
        Me.Label5.TabIndex = 337
        Me.Label5.Text = "Value"
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(556, 283)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Cari.TabIndex = 9
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Cmb_FilterValue
        '
        Me.Cmb_FilterValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_FilterValue.DropDownWidth = 150
        Me.Cmb_FilterValue.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.Cmb_FilterValue.FormattingEnabled = True
        Me.Cmb_FilterValue.Location = New System.Drawing.Point(104, 286)
        Me.Cmb_FilterValue.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_FilterValue.Name = "Cmb_FilterValue"
        Me.Cmb_FilterValue.Size = New System.Drawing.Size(195, 25)
        Me.Cmb_FilterValue.TabIndex = 7
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(9, 217)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(595, 12)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(4, 265)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(938, 19)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(23, 119)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 20)
        Me.Label11.TabIndex = 345
        Me.Label11.Text = "XSatuan"
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Location = New System.Drawing.Point(211, 116)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(228, 24)
        Me.Cmb_Satuan.TabIndex = 2
        '
        'Cmb_Jenis
        '
        Me.Cmb_Jenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Jenis.FormattingEnabled = True
        Me.Cmb_Jenis.Location = New System.Drawing.Point(211, 144)
        Me.Cmb_Jenis.Name = "Cmb_Jenis"
        Me.Cmb_Jenis.Size = New System.Drawing.Size(228, 24)
        Me.Cmb_Jenis.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(23, 147)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 20)
        Me.Label6.TabIndex = 356
        Me.Label6.Text = "XSatuan"
        '
        'Lv_Switch
        '
        Me.Lv_Switch.CheckBoxes = True
        Me.Lv_Switch.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_Switch.FullRowSelect = True
        Me.Lv_Switch.GridLines = True
        Me.Lv_Switch.HideSelection = False
        Me.Lv_Switch.Location = New System.Drawing.Point(606, 63)
        Me.Lv_Switch.Name = "Lv_Switch"
        Me.Lv_Switch.Size = New System.Drawing.Size(315, 221)
        Me.Lv_Switch.TabIndex = 357
        Me.Lv_Switch.UseCompatibleStateImageBehavior = False
        Me.Lv_Switch.View = System.Windows.Forms.View.Details
        '
        'Txt_Switch
        '
        Me.Txt_Switch.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Switch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Switch.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Switch.Location = New System.Drawing.Point(445, 146)
        Me.Txt_Switch.MaxLength = 50
        Me.Txt_Switch.Name = "Txt_Switch"
        Me.Txt_Switch.Size = New System.Drawing.Size(155, 22)
        Me.Txt_Switch.TabIndex = 358
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.Rb_LapanganYa)
        Me.Panel10.Controls.Add(Me.Rb_LapanganTdk)
        Me.Panel10.Location = New System.Drawing.Point(721, 173)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(113, 28)
        Me.Panel10.TabIndex = 351
        Me.Panel10.Visible = False
        '
        'Rb_LapanganYa
        '
        Me.Rb_LapanganYa.AutoSize = True
        Me.Rb_LapanganYa.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Rb_LapanganYa.Location = New System.Drawing.Point(3, 3)
        Me.Rb_LapanganYa.Name = "Rb_LapanganYa"
        Me.Rb_LapanganYa.Size = New System.Drawing.Size(39, 21)
        Me.Rb_LapanganYa.TabIndex = 1
        Me.Rb_LapanganYa.TabStop = True
        Me.Rb_LapanganYa.Text = "Ya"
        Me.Rb_LapanganYa.UseVisualStyleBackColor = True
        '
        'Rb_LapanganTdk
        '
        Me.Rb_LapanganTdk.AutoSize = True
        Me.Rb_LapanganTdk.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Rb_LapanganTdk.Location = New System.Drawing.Point(48, 4)
        Me.Rb_LapanganTdk.Name = "Rb_LapanganTdk"
        Me.Rb_LapanganTdk.Size = New System.Drawing.Size(58, 21)
        Me.Rb_LapanganTdk.TabIndex = 0
        Me.Rb_LapanganTdk.TabStop = True
        Me.Rb_LapanganTdk.Text = "Tidak"
        Me.Rb_LapanganTdk.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(590, 179)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(125, 20)
        Me.Label7.TabIndex = 352
        Me.Label7.Text = "Tampil Lapangan"
        Me.Label7.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(685, 212)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 20)
        Me.Label8.TabIndex = 359
        Me.Label8.Text = "Tampil Lab"
        Me.Label8.Visible = False
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Rb_LabYa)
        Me.Panel11.Controls.Add(Me.Rb_LabTdk)
        Me.Panel11.Location = New System.Drawing.Point(775, 208)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(113, 28)
        Me.Panel11.TabIndex = 352
        Me.Panel11.Visible = False
        '
        'Rb_LabYa
        '
        Me.Rb_LabYa.AutoSize = True
        Me.Rb_LabYa.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Rb_LabYa.Location = New System.Drawing.Point(3, 5)
        Me.Rb_LabYa.Name = "Rb_LabYa"
        Me.Rb_LabYa.Size = New System.Drawing.Size(39, 21)
        Me.Rb_LabYa.TabIndex = 1
        Me.Rb_LabYa.TabStop = True
        Me.Rb_LabYa.Text = "Ya"
        Me.Rb_LabYa.UseVisualStyleBackColor = True
        '
        'Rb_LabTdk
        '
        Me.Rb_LabTdk.AutoSize = True
        Me.Rb_LabTdk.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Rb_LabTdk.Location = New System.Drawing.Point(48, 4)
        Me.Rb_LabTdk.Name = "Rb_LabTdk"
        Me.Rb_LabTdk.Size = New System.Drawing.Size(58, 21)
        Me.Rb_LabTdk.TabIndex = 0
        Me.Rb_LabTdk.TabStop = True
        Me.Rb_LabTdk.Text = "Tidak"
        Me.Rb_LabTdk.UseVisualStyleBackColor = True
        '
        'Txt_RangeAwal
        '
        Me.Txt_RangeAwal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_RangeAwal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_RangeAwal.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_RangeAwal.Location = New System.Drawing.Point(102, 9)
        Me.Txt_RangeAwal.MaxLength = 50
        Me.Txt_RangeAwal.Name = "Txt_RangeAwal"
        Me.Txt_RangeAwal.Size = New System.Drawing.Size(120, 22)
        Me.Txt_RangeAwal.TabIndex = 360
        Me.Txt_RangeAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(11, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 20)
        Me.Label9.TabIndex = 361
        Me.Label9.Text = "Range Awal"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(242, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(90, 20)
        Me.Label10.TabIndex = 363
        Me.Label10.Text = "Range Akhir"
        '
        'Txt_RangeAkhir
        '
        Me.Txt_RangeAkhir.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_RangeAkhir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_RangeAkhir.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_RangeAkhir.Location = New System.Drawing.Point(336, 9)
        Me.Txt_RangeAkhir.MaxLength = 50
        Me.Txt_RangeAkhir.Name = "Txt_RangeAkhir"
        Me.Txt_RangeAkhir.Size = New System.Drawing.Size(120, 22)
        Me.Txt_RangeAkhir.TabIndex = 362
        Me.Txt_RangeAkhir.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GrBox_Range
        '
        Me.GrBox_Range.Controls.Add(Me.Txt_RangeAwal)
        Me.GrBox_Range.Controls.Add(Me.Label10)
        Me.GrBox_Range.Controls.Add(Me.Label9)
        Me.GrBox_Range.Controls.Add(Me.Txt_RangeAkhir)
        Me.GrBox_Range.Location = New System.Drawing.Point(455, 136)
        Me.GrBox_Range.Name = "GrBox_Range"
        Me.GrBox_Range.Size = New System.Drawing.Size(460, 35)
        Me.GrBox_Range.TabIndex = 364
        Me.GrBox_Range.TabStop = False
        Me.GrBox_Range.Visible = False
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Tb_Lapangan)
        Me.Panel8.Controls.Add(Me.Rb_Lab)
        Me.Panel8.Location = New System.Drawing.Point(211, 174)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(183, 28)
        Me.Panel8.TabIndex = 352
        '
        'Tb_Lapangan
        '
        Me.Tb_Lapangan.AutoSize = True
        Me.Tb_Lapangan.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Tb_Lapangan.Location = New System.Drawing.Point(3, 5)
        Me.Tb_Lapangan.Name = "Tb_Lapangan"
        Me.Tb_Lapangan.Size = New System.Drawing.Size(82, 21)
        Me.Tb_Lapangan.TabIndex = 1
        Me.Tb_Lapangan.TabStop = True
        Me.Tb_Lapangan.Text = "Lapangan"
        Me.Tb_Lapangan.UseVisualStyleBackColor = True
        '
        'Rb_Lab
        '
        Me.Rb_Lab.AutoSize = True
        Me.Rb_Lab.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Rb_Lab.Location = New System.Drawing.Point(103, 5)
        Me.Rb_Lab.Name = "Rb_Lab"
        Me.Rb_Lab.Size = New System.Drawing.Size(48, 21)
        Me.Rb_Lab.TabIndex = 0
        Me.Rb_Lab.TabStop = True
        Me.Rb_Lab.Text = "Lab"
        Me.Rb_Lab.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label12.Location = New System.Drawing.Point(23, 182)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 20)
        Me.Label12.TabIndex = 352
        Me.Label12.Text = "Tampil "
        '
        'Master_Quality_Control
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(942, 535)
        Me.Controls.Add(Me.GrBox_Range)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel11)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Txt_Switch)
        Me.Controls.Add(Me.Lv_Switch)
        Me.Controls.Add(Me.Cmb_Jenis)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Cmb_Satuan)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Cmb_FilterValue)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Txt_FilterValue)
        Me.Controls.Add(Me.Lv_DataQC)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Txt_Kode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Master_Quality_Control"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.GrBox_Range.ResumeLayout(False)
        Me.GrBox_Range.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Kode As TextBox
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Lv_DataQC As ListView
    Friend WithEvents Txt_FilterValue As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Cmb_FilterValue As ComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents Cmb_Jenis As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Lv_Switch As ListView
    Friend WithEvents Txt_Switch As TextBox
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Rb_LapanganYa As RadioButton
    Friend WithEvents Rb_LapanganTdk As RadioButton
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Rb_LabYa As RadioButton
    Friend WithEvents Rb_LabTdk As RadioButton
    Friend WithEvents Txt_RangeAwal As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Txt_RangeAkhir As TextBox
    Friend WithEvents GrBox_Range As GroupBox
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Tb_Lapangan As RadioButton
    Friend WithEvents Rb_Lab As RadioButton
    Friend WithEvents Label12 As Label
End Class
