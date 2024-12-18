<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Flever
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_NamaBarangAwal = New System.Windows.Forms.TextBox()
        Me.Lbl_KdBrg = New System.Windows.Forms.Label()
        Me.Txt_KdBarangAwal = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_NamaBarangAkhir = New System.Windows.Forms.TextBox()
        Me.Txt_KdBarangAkhir = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Txt_HslMaterialAkhir = New System.Windows.Forms.TextBox()
        Me.Txt_JmlhMaterialAwal = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_DetailBarang = New System.Windows.Forms.ListView()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Cmb_Kolom = New System.Windows.Forms.ComboBox()
        Me.Txt_Value = New System.Windows.Forms.TextBox()
        Me.Lbl_Kolom = New System.Windows.Forms.Label()
        Me.Lbl_Value = New System.Windows.Forms.Label()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Lbl_IdFlever = New System.Windows.Forms.Label()
        Me.Txt_IdFlever = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1050, 51)
        Me.Panel1.TabIndex = 318
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1050, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(314, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Master - Material to Material"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(19, 50)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1334, 12)
        Me.Panel2.TabIndex = 319
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 51)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 673)
        Me.Panel3.TabIndex = 320
        Me.Panel3.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Txt_NamaBarangAwal)
        Me.GroupBox1.Controls.Add(Me.Lbl_KdBrg)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarangAwal)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 8.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(562, 79)
        Me.GroupBox1.TabIndex = 321
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Barang Awal"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, -3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 17)
        Me.Label5.TabIndex = 447
        Me.Label5.Text = "Barang Awal"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(6, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 20)
        Me.Label3.TabIndex = 447
        Me.Label3.Text = "Nama Barang"
        '
        'Txt_NamaBarangAwal
        '
        Me.Txt_NamaBarangAwal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NamaBarangAwal.Enabled = False
        Me.Txt_NamaBarangAwal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NamaBarangAwal.Location = New System.Drawing.Point(188, 47)
        Me.Txt_NamaBarangAwal.MaxLength = 50
        Me.Txt_NamaBarangAwal.Name = "Txt_NamaBarangAwal"
        Me.Txt_NamaBarangAwal.Size = New System.Drawing.Size(336, 22)
        Me.Txt_NamaBarangAwal.TabIndex = 2
        '
        'Lbl_KdBrg
        '
        Me.Lbl_KdBrg.AutoSize = True
        Me.Lbl_KdBrg.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_KdBrg.Location = New System.Drawing.Point(6, 21)
        Me.Lbl_KdBrg.Name = "Lbl_KdBrg"
        Me.Lbl_KdBrg.Size = New System.Drawing.Size(94, 20)
        Me.Lbl_KdBrg.TabIndex = 446
        Me.Lbl_KdBrg.Text = "Kode Barang"
        '
        'Txt_KdBarangAwal
        '
        Me.Txt_KdBarangAwal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarangAwal.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_KdBarangAwal.Location = New System.Drawing.Point(188, 19)
        Me.Txt_KdBarangAwal.MaxLength = 50
        Me.Txt_KdBarangAwal.Name = "Txt_KdBarangAwal"
        Me.Txt_KdBarangAwal.Size = New System.Drawing.Size(336, 22)
        Me.Txt_KdBarangAwal.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Txt_NamaBarangAkhir)
        Me.GroupBox2.Controls.Add(Me.Txt_KdBarangAkhir)
        Me.GroupBox2.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(6, 94)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(562, 78)
        Me.GroupBox2.TabIndex = 321
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Barang Akhir"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(6, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 20)
        Me.Label4.TabIndex = 448
        Me.Label4.Text = "Nama Barang"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(6, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 20)
        Me.Label2.TabIndex = 447
        Me.Label2.Text = "Kode Barang"
        '
        'Txt_NamaBarangAkhir
        '
        Me.Txt_NamaBarangAkhir.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NamaBarangAkhir.Enabled = False
        Me.Txt_NamaBarangAkhir.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NamaBarangAkhir.Location = New System.Drawing.Point(188, 45)
        Me.Txt_NamaBarangAkhir.MaxLength = 50
        Me.Txt_NamaBarangAkhir.Name = "Txt_NamaBarangAkhir"
        Me.Txt_NamaBarangAkhir.Size = New System.Drawing.Size(336, 22)
        Me.Txt_NamaBarangAkhir.TabIndex = 2
        '
        'Txt_KdBarangAkhir
        '
        Me.Txt_KdBarangAkhir.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarangAkhir.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_KdBarangAkhir.Location = New System.Drawing.Point(188, 17)
        Me.Txt_KdBarangAkhir.MaxLength = 50
        Me.Txt_KdBarangAkhir.Name = "Txt_KdBarangAkhir"
        Me.Txt_KdBarangAkhir.Size = New System.Drawing.Size(336, 22)
        Me.Txt_KdBarangAkhir.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1030, 59)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 653)
        Me.Panel4.TabIndex = 320
        Me.Panel4.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Btn_Hapus)
        Me.GroupBox3.Controls.Add(Me.Btn_Refresh)
        Me.GroupBox3.Controls.Add(Me.Btn_Simpan)
        Me.GroupBox3.Location = New System.Drawing.Point(20, 334)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(599, 52)
        Me.GroupBox3.TabIndex = 322
        Me.GroupBox3.TabStop = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(201, 12)
        Me.Btn_Hapus.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(184, 33)
        Me.Btn_Hapus.TabIndex = 11
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(399, 12)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(184, 33)
        Me.Btn_Refresh.TabIndex = 3
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(6, 12)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(184, 33)
        Me.Btn_Simpan.TabIndex = 2
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Txt_HslMaterialAkhir
        '
        Me.Txt_HslMaterialAkhir.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_HslMaterialAkhir.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_HslMaterialAkhir.Location = New System.Drawing.Point(194, 206)
        Me.Txt_HslMaterialAkhir.Name = "Txt_HslMaterialAkhir"
        Me.Txt_HslMaterialAkhir.Size = New System.Drawing.Size(132, 22)
        Me.Txt_HslMaterialAkhir.TabIndex = 1
        '
        'Txt_JmlhMaterialAwal
        '
        Me.Txt_JmlhMaterialAwal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_JmlhMaterialAwal.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_JmlhMaterialAwal.Location = New System.Drawing.Point(194, 178)
        Me.Txt_JmlhMaterialAwal.Name = "Txt_JmlhMaterialAwal"
        Me.Txt_JmlhMaterialAwal.Size = New System.Drawing.Size(132, 22)
        Me.Txt_JmlhMaterialAwal.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(12, 208)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(143, 20)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Hasil Material Akhir"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(12, 180)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(158, 20)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Jumlah Material Awal"
        '
        'Lv_Data
        '
        Me.Lv_Data.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(7, 49)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(996, 254)
        Me.Lv_Data.TabIndex = 0
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(22, 712)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1334, 12)
        Me.Panel5.TabIndex = 319
        Me.Panel5.Visible = False
        '
        'Lv_DetailBarang
        '
        Me.Lv_DetailBarang.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_DetailBarang.FullRowSelect = True
        Me.Lv_DetailBarang.GridLines = True
        Me.Lv_DetailBarang.HideSelection = False
        Me.Lv_DetailBarang.Location = New System.Drawing.Point(1051, 194)
        Me.Lv_DetailBarang.Name = "Lv_DetailBarang"
        Me.Lv_DetailBarang.Size = New System.Drawing.Size(486, 206)
        Me.Lv_DetailBarang.TabIndex = 325
        Me.Lv_DetailBarang.UseCompatibleStateImageBehavior = False
        Me.Lv_DetailBarang.View = System.Windows.Forms.View.Details
        Me.Lv_DetailBarang.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Cmb_Satuan)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.GroupBox1)
        Me.GroupBox4.Controls.Add(Me.GroupBox2)
        Me.GroupBox4.Controls.Add(Me.Txt_HslMaterialAkhir)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.Txt_JmlhMaterialAwal)
        Me.GroupBox4.Location = New System.Drawing.Point(21, 55)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(598, 273)
        Me.GroupBox4.TabIndex = 446
        Me.GroupBox4.TabStop = False
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Location = New System.Drawing.Point(194, 234)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(132, 26)
        Me.Cmb_Satuan.TabIndex = 450
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(12, 237)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 20)
        Me.Label6.TabIndex = 444
        Me.Label6.Text = "Satuan"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(4, 330)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(835, 12)
        Me.Panel6.TabIndex = 447
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(3, 387)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(836, 12)
        Me.Panel7.TabIndex = 448
        Me.Panel7.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Cmb_Kolom)
        Me.GroupBox5.Controls.Add(Me.Txt_Value)
        Me.GroupBox5.Controls.Add(Me.Lbl_Kolom)
        Me.GroupBox5.Controls.Add(Me.Lbl_Value)
        Me.GroupBox5.Controls.Add(Me.Btn_Cari)
        Me.GroupBox5.Controls.Add(Me.Lv_Data)
        Me.GroupBox5.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(20, 396)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(1009, 309)
        Me.GroupBox5.TabIndex = 449
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Display"
        '
        'Cmb_Kolom
        '
        Me.Cmb_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Kolom.DropDownWidth = 150
        Me.Cmb_Kolom.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.Cmb_Kolom.FormattingEnabled = True
        Me.Cmb_Kolom.Location = New System.Drawing.Point(62, 18)
        Me.Cmb_Kolom.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_Kolom.Name = "Cmb_Kolom"
        Me.Cmb_Kolom.Size = New System.Drawing.Size(195, 25)
        Me.Cmb_Kolom.TabIndex = 343
        '
        'Txt_Value
        '
        Me.Txt_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Value.Location = New System.Drawing.Point(314, 19)
        Me.Txt_Value.MaxLength = 50
        Me.Txt_Value.Name = "Txt_Value"
        Me.Txt_Value.Size = New System.Drawing.Size(189, 22)
        Me.Txt_Value.TabIndex = 344
        '
        'Lbl_Kolom
        '
        Me.Lbl_Kolom.AutoSize = True
        Me.Lbl_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kolom.Location = New System.Drawing.Point(7, 20)
        Me.Lbl_Kolom.Name = "Lbl_Kolom"
        Me.Lbl_Kolom.Size = New System.Drawing.Size(50, 20)
        Me.Lbl_Kolom.TabIndex = 346
        Me.Lbl_Kolom.Text = "Kolom"
        '
        'Lbl_Value
        '
        Me.Lbl_Value.AutoSize = True
        Me.Lbl_Value.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Value.Location = New System.Drawing.Point(262, 20)
        Me.Lbl_Value.Name = "Lbl_Value"
        Me.Lbl_Value.Size = New System.Drawing.Size(46, 20)
        Me.Lbl_Value.TabIndex = 347
        Me.Lbl_Value.Text = "Value"
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(509, 15)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Cari.TabIndex = 345
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.Enabled = False
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(858, 63)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(171, 26)
        Me.Cmb_Lokasi.TabIndex = 451
        Me.Cmb_Lokasi.Visible = False
        '
        'Lbl_IdFlever
        '
        Me.Lbl_IdFlever.AutoSize = True
        Me.Lbl_IdFlever.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_IdFlever.Location = New System.Drawing.Point(807, 118)
        Me.Lbl_IdFlever.Name = "Lbl_IdFlever"
        Me.Lbl_IdFlever.Size = New System.Drawing.Size(69, 20)
        Me.Lbl_IdFlever.TabIndex = 452
        Me.Lbl_IdFlever.Text = "Id Flever"
        Me.Lbl_IdFlever.Visible = False
        '
        'Txt_IdFlever
        '
        Me.Txt_IdFlever.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_IdFlever.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_IdFlever.Location = New System.Drawing.Point(882, 116)
        Me.Txt_IdFlever.MaxLength = 50
        Me.Txt_IdFlever.Name = "Txt_IdFlever"
        Me.Txt_IdFlever.Size = New System.Drawing.Size(147, 22)
        Me.Txt_IdFlever.TabIndex = 453
        Me.Txt_IdFlever.Visible = False
        '
        'Master_Material_to_Material
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1050, 725)
        Me.Controls.Add(Me.Txt_IdFlever)
        Me.Controls.Add(Me.Lbl_IdFlever)
        Me.Controls.Add(Me.Lv_DetailBarang)
        Me.Controls.Add(Me.Cmb_Lokasi)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox5)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Master_Material_to_Material"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_KdBarangAwal As TextBox
    Friend WithEvents Txt_NamaBarangAwal As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Txt_NamaBarangAkhir As TextBox
    Friend WithEvents Txt_KdBarangAkhir As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Txt_HslMaterialAkhir As TextBox
    Friend WithEvents Txt_JmlhMaterialAwal As TextBox
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_DetailBarang As ListView
    Friend WithEvents Lbl_KdBrg As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents Cmb_Kolom As ComboBox
    Friend WithEvents Txt_Value As TextBox
    Friend WithEvents Lbl_Kolom As Label
    Friend WithEvents Lbl_Value As Label
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Lbl_IdFlever As Label
    Friend WithEvents Txt_IdFlever As TextBox
    Friend WithEvents Btn_Simpan As Button
End Class
