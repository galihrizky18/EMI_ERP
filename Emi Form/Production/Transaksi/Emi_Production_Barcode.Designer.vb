<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Emi_Production_Barcode
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
        Me.TxtLifeTime = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Txt_NamaBarang = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_NoSplit = New System.Windows.Forms.TextBox()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.Txt_Jumlah = New System.Windows.Forms.TextBox()
        Me.Txt_HasilProduksi = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Chk_FullPallet = New System.Windows.Forms.CheckBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Barcode = New System.Windows.Forms.PictureBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DtpProduksi = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DtpExpired = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CmbJenis = New System.Windows.Forms.ComboBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TxtSatuanHasil = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1119, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1119, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(395, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Transaksi - Generate Pallet Barcode"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(3, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(3, 66)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TxtLifeTime)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Txt_NamaBarang)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_NoSplit)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 411)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(535, 166)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detail"
        '
        'TxtLifeTime
        '
        Me.TxtLifeTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtLifeTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtLifeTime.Enabled = False
        Me.TxtLifeTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLifeTime.Location = New System.Drawing.Point(134, 123)
        Me.TxtLifeTime.Name = "TxtLifeTime"
        Me.TxtLifeTime.Size = New System.Drawing.Size(74, 22)
        Me.TxtLifeTime.TabIndex = 380
        Me.TxtLifeTime.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(16, 125)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 16)
        Me.Label11.TabIndex = 379
        Me.Label11.Text = "Life Time"
        Me.Label11.Visible = False
        '
        'Txt_NamaBarang
        '
        Me.Txt_NamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NamaBarang.Enabled = False
        Me.Txt_NamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NamaBarang.Location = New System.Drawing.Point(134, 99)
        Me.Txt_NamaBarang.Name = "Txt_NamaBarang"
        Me.Txt_NamaBarang.Size = New System.Drawing.Size(395, 22)
        Me.Txt_NamaBarang.TabIndex = 378
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 16)
        Me.Label4.TabIndex = 377
        Me.Label4.Text = "Nama Barang"
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.Enabled = False
        Me.Cmb_Lokasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(134, 16)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(395, 24)
        Me.Cmb_Lokasi.TabIndex = 376
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Enabled = False
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_KdBarang.Location = New System.Drawing.Point(134, 73)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(395, 22)
        Me.Txt_KdBarang.TabIndex = 375
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Lokasi"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Kode Barang"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "No Faktur"
        '
        'Txt_NoSplit
        '
        Me.Txt_NoSplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoSplit.Enabled = False
        Me.Txt_NoSplit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoSplit.Location = New System.Drawing.Point(134, 45)
        Me.Txt_NoSplit.Name = "Txt_NoSplit"
        Me.Txt_NoSplit.Size = New System.Drawing.Size(395, 22)
        Me.Txt_NoSplit.TabIndex = 375
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.Enabled = False
        Me.Cmb_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Location = New System.Drawing.Point(300, 140)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(62, 24)
        Me.Cmb_Satuan.TabIndex = 376
        '
        'Txt_Jumlah
        '
        Me.Txt_Jumlah.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Jumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Jumlah.Enabled = False
        Me.Txt_Jumlah.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Jumlah.Location = New System.Drawing.Point(124, 141)
        Me.Txt_Jumlah.Name = "Txt_Jumlah"
        Me.Txt_Jumlah.Size = New System.Drawing.Size(170, 22)
        Me.Txt_Jumlah.TabIndex = 375
        Me.Txt_Jumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_HasilProduksi
        '
        Me.Txt_HasilProduksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_HasilProduksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_HasilProduksi.Enabled = False
        Me.Txt_HasilProduksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_HasilProduksi.Location = New System.Drawing.Point(124, 10)
        Me.Txt_HasilProduksi.Name = "Txt_HasilProduksi"
        Me.Txt_HasilProduksi.Size = New System.Drawing.Size(131, 22)
        Me.Txt_HasilProduksi.TabIndex = 375
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 143)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 16)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Jumlah"
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(144, 9)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(126, 33)
        Me.Btn_Refresh.TabIndex = 373
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 16)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Jumlah Produksi"
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(11, 9)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(127, 33)
        Me.Btn_Simpan.TabIndex = 373
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Chk_FullPallet
        '
        Me.Chk_FullPallet.AutoSize = True
        Me.Chk_FullPallet.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chk_FullPallet.Location = New System.Drawing.Point(124, 40)
        Me.Chk_FullPallet.Name = "Chk_FullPallet"
        Me.Chk_FullPallet.Size = New System.Drawing.Size(84, 20)
        Me.Chk_FullPallet.TabIndex = 374
        Me.Chk_FullPallet.Text = "Full Pallet"
        Me.Chk_FullPallet.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1097, 54)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 601)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'Lv_Data
        '
        Me.Lv_Data.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(24, 66)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(1070, 329)
        Me.Lv_Data.TabIndex = 235
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(34, 636)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(942, 12)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'Barcode
        '
        Me.Barcode.Location = New System.Drawing.Point(1109, 51)
        Me.Barcode.Name = "Barcode"
        Me.Barcode.Size = New System.Drawing.Size(100, 50)
        Me.Barcode.TabIndex = 377
        Me.Barcode.TabStop = False
        Me.Barcode.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(23, 397)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 12)
        Me.Panel4.TabIndex = 35
        Me.Panel4.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TxtSatuanHasil)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.DtpProduksi)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.DtpExpired)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.CmbJenis)
        Me.GroupBox2.Controls.Add(Me.Chk_FullPallet)
        Me.GroupBox2.Controls.Add(Me.Txt_HasilProduksi)
        Me.GroupBox2.Controls.Add(Me.Cmb_Satuan)
        Me.GroupBox2.Controls.Add(Me.Txt_Jumlah)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(564, 410)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(397, 168)
        Me.GroupBox2.TabIndex = 378
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Input"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 62)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 16)
        Me.Label10.TabIndex = 382
        Me.Label10.Text = "Tgl Produksi"
        '
        'DtpProduksi
        '
        Me.DtpProduksi.CustomFormat = "dd MMMM yyyy"
        Me.DtpProduksi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpProduksi.Location = New System.Drawing.Point(124, 61)
        Me.DtpProduksi.Name = "DtpProduksi"
        Me.DtpProduksi.Size = New System.Drawing.Size(170, 20)
        Me.DtpProduksi.TabIndex = 381
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 87)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 16)
        Me.Label9.TabIndex = 380
        Me.Label9.Text = "Tgl Expired"
        '
        'DtpExpired
        '
        Me.DtpExpired.CustomFormat = "dd MMMM yyyy"
        Me.DtpExpired.Enabled = False
        Me.DtpExpired.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpExpired.Location = New System.Drawing.Point(124, 86)
        Me.DtpExpired.Name = "DtpExpired"
        Me.DtpExpired.Size = New System.Drawing.Size(170, 20)
        Me.DtpExpired.TabIndex = 379
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 114)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 16)
        Me.Label8.TabIndex = 378
        Me.Label8.Text = "Jenis"
        '
        'CmbJenis
        '
        Me.CmbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJenis.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbJenis.FormattingEnabled = True
        Me.CmbJenis.Location = New System.Drawing.Point(124, 112)
        Me.CmbJenis.Name = "CmbJenis"
        Me.CmbJenis.Size = New System.Drawing.Size(170, 24)
        Me.CmbJenis.TabIndex = 377
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(27, 579)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 12)
        Me.Panel7.TabIndex = 35
        Me.Panel7.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Btn_Simpan)
        Me.GroupBox3.Controls.Add(Me.Btn_Refresh)
        Me.GroupBox3.Location = New System.Drawing.Point(23, 591)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(938, 42)
        Me.GroupBox3.TabIndex = 379
        Me.GroupBox3.TabStop = False
        '
        'TxtSatuanHasil
        '
        Me.TxtSatuanHasil.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSatuanHasil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSatuanHasil.Enabled = False
        Me.TxtSatuanHasil.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSatuanHasil.Location = New System.Drawing.Point(261, 10)
        Me.TxtSatuanHasil.Name = "TxtSatuanHasil"
        Me.TxtSatuanHasil.Size = New System.Drawing.Size(101, 22)
        Me.TxtSatuanHasil.TabIndex = 383
        '
        'Emi_Production_Barcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1119, 649)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Barcode)
        Me.Controls.Add(Me.Lv_Data)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Production_Barcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Chk_FullPallet As CheckBox
    Friend WithEvents Txt_NoSplit As TextBox
    Friend WithEvents Txt_Jumlah As TextBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Txt_HasilProduksi As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Barcode As PictureBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents CmbJenis As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents DtpExpired As DateTimePicker
    Friend WithEvents Txt_NamaBarang As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents DtpProduksi As DateTimePicker
    Friend WithEvents TxtLifeTime As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtSatuanHasil As TextBox
End Class
