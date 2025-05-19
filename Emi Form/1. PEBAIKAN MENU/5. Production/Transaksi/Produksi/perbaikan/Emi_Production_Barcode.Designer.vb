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
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CmbSisaProduksi = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Cmb_SatuanProduksi = New System.Windows.Forms.ComboBox()
        Me.CmbSatScrap = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtJam = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Cmb_LokasiSimpan = New System.Windows.Forms.ComboBox()
        Me.TxtJmlScrap = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DtpProduksi = New System.Windows.Forms.DateTimePicker()
        Me.Txt_NamaBarang = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DtpExpired = New System.Windows.Forms.DateTimePicker()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.CmbJenis = New System.Windows.Forms.ComboBox()
        Me.Chk_FullPallet = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_HasilProduksi = New System.Windows.Forms.TextBox()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Jumlah = New System.Windows.Forms.TextBox()
        Me.Txt_NoSplit = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtSatuanKecil = New System.Windows.Forms.TextBox()
        Me.TxtSatScrapKecil = New System.Windows.Forms.TextBox()
        Me.TxtLifeTime = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Barcode = New System.Windows.Forms.PictureBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TxtFormulator_NoFaktur = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(739, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(739, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(344, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Transaksi - Penerimaan Barang"
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
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Location = New System.Drawing.Point(3, 66)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 36
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CmbSisaProduksi)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Cmb_SatuanProduksi)
        Me.GroupBox1.Controls.Add(Me.CmbSatScrap)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.TxtJam)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Cmb_LokasiSimpan)
        Me.GroupBox1.Controls.Add(Me.TxtJmlScrap)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.DtpProduksi)
        Me.GroupBox1.Controls.Add(Me.Txt_NamaBarang)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.DtpExpired)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.CmbJenis)
        Me.GroupBox1.Controls.Add(Me.Chk_FullPallet)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Txt_HasilProduksi)
        Me.GroupBox1.Controls.Add(Me.Cmb_Satuan)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_Jumlah)
        Me.GroupBox1.Controls.Add(Me.Txt_NoSplit)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 92)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(690, 359)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        '
        'CmbSisaProduksi
        '
        Me.CmbSisaProduksi.BackColor = System.Drawing.SystemColors.Window
        Me.CmbSisaProduksi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSisaProduksi.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSisaProduksi.FormattingEnabled = True
        Me.CmbSisaProduksi.Location = New System.Drawing.Point(184, 310)
        Me.CmbSisaProduksi.Name = "CmbSisaProduksi"
        Me.CmbSisaProduksi.Size = New System.Drawing.Size(186, 25)
        Me.CmbSisaProduksi.TabIndex = 424
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(24, 252)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 16)
        Me.Label8.TabIndex = 423
        Me.Label8.Text = "Jenis Quality"
        '
        'Cmb_SatuanProduksi
        '
        Me.Cmb_SatuanProduksi.Enabled = False
        Me.Cmb_SatuanProduksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_SatuanProduksi.FormattingEnabled = True
        Me.Cmb_SatuanProduksi.Location = New System.Drawing.Point(463, 136)
        Me.Cmb_SatuanProduksi.Name = "Cmb_SatuanProduksi"
        Me.Cmb_SatuanProduksi.Size = New System.Drawing.Size(116, 24)
        Me.Cmb_SatuanProduksi.TabIndex = 422
        '
        'CmbSatScrap
        '
        Me.CmbSatScrap.Enabled = False
        Me.CmbSatScrap.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSatScrap.FormattingEnabled = True
        Me.CmbSatScrap.Location = New System.Drawing.Point(519, 310)
        Me.CmbSatScrap.Name = "CmbSatScrap"
        Me.CmbSatScrap.Size = New System.Drawing.Size(60, 24)
        Me.CmbSatScrap.TabIndex = 419
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMM yyyy"
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(182, 53)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(201, 20)
        Me.DateTimePicker1.TabIndex = 416
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(23, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(124, 20)
        Me.Label6.TabIndex = 418
        Me.Label6.Text = "Tanggal Produksi"
        '
        'TxtJam
        '
        Me.TxtJam.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJam.Enabled = False
        Me.TxtJam.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtJam.Location = New System.Drawing.Point(388, 52)
        Me.TxtJam.MaxLength = 50
        Me.TxtJam.Name = "TxtJam"
        Me.TxtJam.Size = New System.Drawing.Size(191, 22)
        Me.TxtJam.TabIndex = 417
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(24, 283)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 16)
        Me.Label5.TabIndex = 415
        Me.Label5.Text = "Lokasi Gudang"
        '
        'Cmb_LokasiSimpan
        '
        Me.Cmb_LokasiSimpan.BackColor = System.Drawing.SystemColors.Window
        Me.Cmb_LokasiSimpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_LokasiSimpan.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_LokasiSimpan.FormattingEnabled = True
        Me.Cmb_LokasiSimpan.Location = New System.Drawing.Point(184, 280)
        Me.Cmb_LokasiSimpan.Name = "Cmb_LokasiSimpan"
        Me.Cmb_LokasiSimpan.Size = New System.Drawing.Size(395, 25)
        Me.Cmb_LokasiSimpan.TabIndex = 414
        '
        'TxtJmlScrap
        '
        Me.TxtJmlScrap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJmlScrap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJmlScrap.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtJmlScrap.Location = New System.Drawing.Point(376, 311)
        Me.TxtJmlScrap.MaxLength = 50
        Me.TxtJmlScrap.Name = "TxtJmlScrap"
        Me.TxtJmlScrap.Size = New System.Drawing.Size(140, 22)
        Me.TxtJmlScrap.TabIndex = 410
        Me.TxtJmlScrap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label13.Location = New System.Drawing.Point(23, 312)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(102, 20)
        Me.Label13.TabIndex = 413
        Me.Label13.Text = "Sisa Produksi"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(24, 166)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 16)
        Me.Label10.TabIndex = 382
        Me.Label10.Text = "Tgl Produksi"
        '
        'DtpProduksi
        '
        Me.DtpProduksi.CustomFormat = "dd MMMM yyyy"
        Me.DtpProduksi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpProduksi.Location = New System.Drawing.Point(184, 166)
        Me.DtpProduksi.Name = "DtpProduksi"
        Me.DtpProduksi.Size = New System.Drawing.Size(234, 20)
        Me.DtpProduksi.TabIndex = 381
        '
        'Txt_NamaBarang
        '
        Me.Txt_NamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NamaBarang.Enabled = False
        Me.Txt_NamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NamaBarang.Location = New System.Drawing.Point(184, 108)
        Me.Txt_NamaBarang.Name = "Txt_NamaBarang"
        Me.Txt_NamaBarang.Size = New System.Drawing.Size(395, 22)
        Me.Txt_NamaBarang.TabIndex = 378
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(24, 194)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 16)
        Me.Label9.TabIndex = 380
        Me.Label9.Text = "Tgl Expired"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 110)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 16)
        Me.Label4.TabIndex = 377
        Me.Label4.Text = "Nama Barang"
        '
        'DtpExpired
        '
        Me.DtpExpired.CustomFormat = "dd MMMM yyyy"
        Me.DtpExpired.Enabled = False
        Me.DtpExpired.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpExpired.Location = New System.Drawing.Point(184, 193)
        Me.DtpExpired.Name = "DtpExpired"
        Me.DtpExpired.Size = New System.Drawing.Size(234, 20)
        Me.DtpExpired.TabIndex = 379
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Enabled = False
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_KdBarang.Location = New System.Drawing.Point(184, 80)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(395, 22)
        Me.Txt_KdBarang.TabIndex = 375
        '
        'CmbJenis
        '
        Me.CmbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJenis.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbJenis.FormattingEnabled = True
        Me.CmbJenis.Location = New System.Drawing.Point(184, 249)
        Me.CmbJenis.Name = "CmbJenis"
        Me.CmbJenis.Size = New System.Drawing.Size(395, 24)
        Me.CmbJenis.TabIndex = 377
        '
        'Chk_FullPallet
        '
        Me.Chk_FullPallet.AutoSize = True
        Me.Chk_FullPallet.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Chk_FullPallet.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chk_FullPallet.Location = New System.Drawing.Point(24, 220)
        Me.Chk_FullPallet.Name = "Chk_FullPallet"
        Me.Chk_FullPallet.Size = New System.Drawing.Size(100, 20)
        Me.Chk_FullPallet.TabIndex = 374
        Me.Chk_FullPallet.Text = "Jumlah Input"
        Me.Chk_FullPallet.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Kode Barang"
        '
        'Txt_HasilProduksi
        '
        Me.Txt_HasilProduksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_HasilProduksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_HasilProduksi.Enabled = False
        Me.Txt_HasilProduksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_HasilProduksi.Location = New System.Drawing.Point(184, 137)
        Me.Txt_HasilProduksi.Name = "Txt_HasilProduksi"
        Me.Txt_HasilProduksi.Size = New System.Drawing.Size(275, 22)
        Me.Txt_HasilProduksi.TabIndex = 375
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.Enabled = False
        Me.Cmb_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Location = New System.Drawing.Point(463, 218)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(116, 24)
        Me.Cmb_Satuan.TabIndex = 376
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "No Split"
        '
        'Txt_Jumlah
        '
        Me.Txt_Jumlah.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Jumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Jumlah.Enabled = False
        Me.Txt_Jumlah.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Jumlah.Location = New System.Drawing.Point(184, 220)
        Me.Txt_Jumlah.Name = "Txt_Jumlah"
        Me.Txt_Jumlah.Size = New System.Drawing.Size(275, 22)
        Me.Txt_Jumlah.TabIndex = 375
        Me.Txt_Jumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_NoSplit
        '
        Me.Txt_NoSplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoSplit.Enabled = False
        Me.Txt_NoSplit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoSplit.Location = New System.Drawing.Point(182, 24)
        Me.Txt_NoSplit.Name = "Txt_NoSplit"
        Me.Txt_NoSplit.Size = New System.Drawing.Size(165, 22)
        Me.Txt_NoSplit.TabIndex = 375
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(24, 139)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 16)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Jumlah Produksi"
        '
        'TxtSatuanKecil
        '
        Me.TxtSatuanKecil.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSatuanKecil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSatuanKecil.Enabled = False
        Me.TxtSatuanKecil.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSatuanKecil.Location = New System.Drawing.Point(873, 256)
        Me.TxtSatuanKecil.Name = "TxtSatuanKecil"
        Me.TxtSatuanKecil.Size = New System.Drawing.Size(72, 22)
        Me.TxtSatuanKecil.TabIndex = 421
        Me.TxtSatuanKecil.Visible = False
        '
        'TxtSatScrapKecil
        '
        Me.TxtSatScrapKecil.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSatScrapKecil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSatScrapKecil.Enabled = False
        Me.TxtSatScrapKecil.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSatScrapKecil.Location = New System.Drawing.Point(795, 256)
        Me.TxtSatScrapKecil.Name = "TxtSatScrapKecil"
        Me.TxtSatScrapKecil.Size = New System.Drawing.Size(72, 22)
        Me.TxtSatScrapKecil.TabIndex = 420
        Me.TxtSatScrapKecil.Visible = False
        '
        'TxtLifeTime
        '
        Me.TxtLifeTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtLifeTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtLifeTime.Enabled = False
        Me.TxtLifeTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLifeTime.Location = New System.Drawing.Point(861, 313)
        Me.TxtLifeTime.Name = "TxtLifeTime"
        Me.TxtLifeTime.Size = New System.Drawing.Size(122, 22)
        Me.TxtLifeTime.TabIndex = 380
        Me.TxtLifeTime.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(793, 315)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 16)
        Me.Label11.TabIndex = 379
        Me.Label11.Text = "Life Time"
        Me.Label11.Visible = False
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.Enabled = False
        Me.Cmb_Lokasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(499, 66)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(214, 24)
        Me.Cmb_Lokasi.TabIndex = 376
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(257, 17)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(126, 56)
        Me.Btn_Refresh.TabIndex = 373
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(6, 17)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(127, 56)
        Me.Btn_Simpan.TabIndex = 373
        Me.Btn_Simpan.Text = "&Finished Goods"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(720, 64)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 601)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
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
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(796, 534)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 12)
        Me.Panel7.TabIndex = 35
        Me.Panel7.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Button1)
        Me.GroupBox3.Controls.Add(Me.Btn_Simpan)
        Me.GroupBox3.Controls.Add(Me.Btn_Refresh)
        Me.GroupBox3.Location = New System.Drawing.Point(23, 457)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(690, 83)
        Me.GroupBox3.TabIndex = 379
        Me.GroupBox3.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(135, 17)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(119, 56)
        Me.Button1.TabIndex = 373
        Me.Button1.Text = "&Primary Packaging only"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TxtFormulator_NoFaktur
        '
        Me.TxtFormulator_NoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtFormulator_NoFaktur.Enabled = False
        Me.TxtFormulator_NoFaktur.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFormulator_NoFaktur.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtFormulator_NoFaktur.Location = New System.Drawing.Point(23, 64)
        Me.TxtFormulator_NoFaktur.MaxLength = 30
        Me.TxtFormulator_NoFaktur.Name = "TxtFormulator_NoFaktur"
        Me.TxtFormulator_NoFaktur.ReadOnly = True
        Me.TxtFormulator_NoFaktur.Size = New System.Drawing.Size(227, 22)
        Me.TxtFormulator_NoFaktur.TabIndex = 380
        Me.TxtFormulator_NoFaktur.Visible = False
        '
        'Emi_Production_Barcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(739, 547)
        Me.Controls.Add(Me.TxtFormulator_NoFaktur)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.TxtSatuanKecil)
        Me.Controls.Add(Me.TxtSatScrapKecil)
        Me.Controls.Add(Me.Barcode)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Cmb_Lokasi)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtLifeTime)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Production_Barcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Chk_FullPallet As CheckBox
    Friend WithEvents Txt_NoSplit As TextBox
    Friend WithEvents Txt_Jumlah As TextBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Txt_HasilProduksi As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Barcode As PictureBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents CmbJenis As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents DtpExpired As DateTimePicker
    Friend WithEvents Txt_NamaBarang As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents DtpProduksi As DateTimePicker
    Friend WithEvents TxtLifeTime As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtFormulator_NoFaktur As TextBox
    Friend WithEvents Cmb_LokasiSimpan As ComboBox
    Friend WithEvents TxtJmlScrap As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtJam As TextBox
    Friend WithEvents CmbSatScrap As ComboBox
    Friend WithEvents Cmb_SatuanProduksi As ComboBox
    Friend WithEvents TxtSatuanKecil As TextBox
    Friend WithEvents TxtSatScrapKecil As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents CmbSisaProduksi As ComboBox
    Friend WithEvents Button1 As Button
End Class
