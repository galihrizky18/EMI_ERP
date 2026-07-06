<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SD_Tambah_PR_Barang_Lain
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
		Me.LblPilihBarang_Judul = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.CmbPilihBarang_Lokasi = New System.Windows.Forms.ComboBox()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.BtnPilihBarang_Refresh = New System.Windows.Forms.Button()
		Me.BtnPilihBarang_Simpan = New System.Windows.Forms.Button()
		Me.TxtPilihBarang_NamaBarang = New System.Windows.Forms.TextBox()
		Me.LblPilihBarang_NamaBarang = New System.Windows.Forms.Label()
		Me.TxtPilihBarang_KodeBarang = New System.Windows.Forms.TextBox()
		Me.LblPilihBarang_KodeBarang = New System.Windows.Forms.Label()
		Me.LblPilihBarang_Satuan = New System.Windows.Forms.Label()
		Me.LvPilihBarang_DataBarang = New System.Windows.Forms.ListView()
		Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
		Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
		Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
		Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
		Me.LblPilihBarang_Lokasi = New System.Windows.Forms.Label()
		Me.CmbPilihBarang_Satuan = New System.Windows.Forms.ComboBox()
		Me.TxtPilihBarang_Satuan = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
		Me.Lbl_Jumlah = New System.Windows.Forms.Label()
		Me.txtJumlah = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.txtKeterangan = New System.Windows.Forms.TextBox()
		Me.Lbl_Sisa = New System.Windows.Forms.Label()
		Me.Txt_Sisa = New System.Windows.Forms.TextBox()
		Me.Txt_Order = New System.Windows.Forms.TextBox()
		Me.Lbl_Order = New System.Windows.Forms.Label()
		Me.Txt_PR = New System.Windows.Forms.TextBox()
		Me.Lbl_PR = New System.Windows.Forms.Label()
		Me.Lbl_GetKdBrg = New System.Windows.Forms.Label()
		Me.Txt_KDSo = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Txt_CostCenter = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Txt_KdGedung = New System.Windows.Forms.TextBox()
		Me.Txt_Gedung = New System.Windows.Forms.TextBox()
		Me.Lv_CostCenter = New System.Windows.Forms.ListView()
		Me.Lv_Gedung = New System.Windows.Forms.ListView()
		Me.Txt_IdGedung = New System.Windows.Forms.TextBox()
		Me.Txt_LokasiGudang = New System.Windows.Forms.TextBox()
		Me.Txt_Id_CostCenter = New System.Windows.Forms.TextBox()
		Me.Cmb_Stock = New System.Windows.Forms.ComboBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Txt_SisaStock = New System.Windows.Forms.TextBox()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.TxtTiba = New System.Windows.Forms.TextBox()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.TxtHarga = New System.Windows.Forms.TextBox()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.LblPilihBarang_Judul)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(575, 51)
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
		Me.PanelGradient1.Size = New System.Drawing.Size(575, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'LblPilihBarang_Judul
		'
		Me.LblPilihBarang_Judul.AutoSize = True
		Me.LblPilihBarang_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LblPilihBarang_Judul.Location = New System.Drawing.Point(15, 11)
		Me.LblPilihBarang_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.LblPilihBarang_Judul.Name = "LblPilihBarang_Judul"
		Me.LblPilihBarang_Judul.Size = New System.Drawing.Size(245, 25)
		Me.LblPilihBarang_Judul.TabIndex = 0
		Me.LblPilihBarang_Judul.Text = "SD - Pilih Barang Lain"
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
		Me.Panel3.Size = New System.Drawing.Size(19, 270)
		Me.Panel3.TabIndex = 35
		Me.Panel3.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(556, 86)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(19, 270)
		Me.Panel5.TabIndex = 37
		Me.Panel5.Visible = False
		'
		'CmbPilihBarang_Lokasi
		'
		Me.CmbPilihBarang_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CmbPilihBarang_Lokasi.Enabled = False
		Me.CmbPilihBarang_Lokasi.FormattingEnabled = True
		Me.CmbPilihBarang_Lokasi.Items.AddRange(New Object() {"HEAD OFFICE"})
		Me.CmbPilihBarang_Lokasi.Location = New System.Drawing.Point(180, 434)
		Me.CmbPilihBarang_Lokasi.Name = "CmbPilihBarang_Lokasi"
		Me.CmbPilihBarang_Lokasi.Size = New System.Drawing.Size(80, 21)
		Me.CmbPilihBarang_Lokasi.TabIndex = 6
		Me.CmbPilihBarang_Lokasi.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(25, 358)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(1028, 15)
		Me.Panel4.TabIndex = 38
		Me.Panel4.Visible = False
		'
		'BtnPilihBarang_Refresh
		'
		Me.BtnPilihBarang_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnPilihBarang_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
		Me.BtnPilihBarang_Refresh.ForeColor = System.Drawing.Color.White
		Me.BtnPilihBarang_Refresh.Location = New System.Drawing.Point(117, 323)
		Me.BtnPilihBarang_Refresh.Name = "BtnPilihBarang_Refresh"
		Me.BtnPilihBarang_Refresh.Size = New System.Drawing.Size(84, 36)
		Me.BtnPilihBarang_Refresh.TabIndex = 8
		Me.BtnPilihBarang_Refresh.Text = "&Refresh"
		Me.BtnPilihBarang_Refresh.UseVisualStyleBackColor = False
		'
		'BtnPilihBarang_Simpan
		'
		Me.BtnPilihBarang_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnPilihBarang_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
		Me.BtnPilihBarang_Simpan.ForeColor = System.Drawing.Color.White
		Me.BtnPilihBarang_Simpan.Location = New System.Drawing.Point(27, 323)
		Me.BtnPilihBarang_Simpan.Name = "BtnPilihBarang_Simpan"
		Me.BtnPilihBarang_Simpan.Size = New System.Drawing.Size(84, 36)
		Me.BtnPilihBarang_Simpan.TabIndex = 5
		Me.BtnPilihBarang_Simpan.Text = "&Simpan"
		Me.BtnPilihBarang_Simpan.UseVisualStyleBackColor = False
		'
		'TxtPilihBarang_NamaBarang
		'
		Me.TxtPilihBarang_NamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtPilihBarang_NamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtPilihBarang_NamaBarang.Enabled = False
		Me.TxtPilihBarang_NamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.TxtPilihBarang_NamaBarang.Location = New System.Drawing.Point(139, 113)
		Me.TxtPilihBarang_NamaBarang.MaxLength = 100
		Me.TxtPilihBarang_NamaBarang.Name = "TxtPilihBarang_NamaBarang"
		Me.TxtPilihBarang_NamaBarang.Size = New System.Drawing.Size(344, 21)
		Me.TxtPilihBarang_NamaBarang.TabIndex = 2
		'
		'LblPilihBarang_NamaBarang
		'
		Me.LblPilihBarang_NamaBarang.AutoSize = True
		Me.LblPilihBarang_NamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.LblPilihBarang_NamaBarang.Location = New System.Drawing.Point(24, 113)
		Me.LblPilihBarang_NamaBarang.Name = "LblPilihBarang_NamaBarang"
		Me.LblPilihBarang_NamaBarang.Size = New System.Drawing.Size(54, 17)
		Me.LblPilihBarang_NamaBarang.TabIndex = 351
		Me.LblPilihBarang_NamaBarang.Text = "NamaX"
		'
		'TxtPilihBarang_KodeBarang
		'
		Me.TxtPilihBarang_KodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtPilihBarang_KodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtPilihBarang_KodeBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.TxtPilihBarang_KodeBarang.Location = New System.Drawing.Point(139, 86)
		Me.TxtPilihBarang_KodeBarang.MaxLength = 50
		Me.TxtPilihBarang_KodeBarang.Name = "TxtPilihBarang_KodeBarang"
		Me.TxtPilihBarang_KodeBarang.Size = New System.Drawing.Size(246, 21)
		Me.TxtPilihBarang_KodeBarang.TabIndex = 0
		'
		'LblPilihBarang_KodeBarang
		'
		Me.LblPilihBarang_KodeBarang.AutoSize = True
		Me.LblPilihBarang_KodeBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.LblPilihBarang_KodeBarang.Location = New System.Drawing.Point(24, 88)
		Me.LblPilihBarang_KodeBarang.Name = "LblPilihBarang_KodeBarang"
		Me.LblPilihBarang_KodeBarang.Size = New System.Drawing.Size(100, 17)
		Me.LblPilihBarang_KodeBarang.TabIndex = 349
		Me.LblPilihBarang_KodeBarang.Text = "Kode BarangX"
		'
		'LblPilihBarang_Satuan
		'
		Me.LblPilihBarang_Satuan.AutoSize = True
		Me.LblPilihBarang_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.LblPilihBarang_Satuan.Location = New System.Drawing.Point(360, 141)
		Me.LblPilihBarang_Satuan.Name = "LblPilihBarang_Satuan"
		Me.LblPilihBarang_Satuan.Size = New System.Drawing.Size(53, 17)
		Me.LblPilihBarang_Satuan.TabIndex = 356
		Me.LblPilihBarang_Satuan.Text = "Satuan"
		'
		'LvPilihBarang_DataBarang
		'
		Me.LvPilihBarang_DataBarang.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9})
		Me.LvPilihBarang_DataBarang.FullRowSelect = True
		Me.LvPilihBarang_DataBarang.GridLines = True
		Me.LvPilihBarang_DataBarang.HideSelection = False
		Me.LvPilihBarang_DataBarang.Location = New System.Drawing.Point(757, 110)
		Me.LvPilihBarang_DataBarang.Name = "LvPilihBarang_DataBarang"
		Me.LvPilihBarang_DataBarang.Size = New System.Drawing.Size(400, 130)
		Me.LvPilihBarang_DataBarang.TabIndex = 358
		Me.LvPilihBarang_DataBarang.UseCompatibleStateImageBehavior = False
		Me.LvPilihBarang_DataBarang.View = System.Windows.Forms.View.Details
		'
		'ColumnHeader1
		'
		Me.ColumnHeader1.Text = "Lokasi"
		Me.ColumnHeader1.Width = 0
		'
		'ColumnHeader7
		'
		Me.ColumnHeader7.Text = "Kode Barang"
		Me.ColumnHeader7.Width = 110
		'
		'ColumnHeader8
		'
		Me.ColumnHeader8.Text = "Nama"
		Me.ColumnHeader8.Width = 210
		'
		'ColumnHeader9
		'
		Me.ColumnHeader9.Text = "Satuan"
		Me.ColumnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		Me.ColumnHeader9.Width = 70
		'
		'LblPilihBarang_Lokasi
		'
		Me.LblPilihBarang_Lokasi.AutoSize = True
		Me.LblPilihBarang_Lokasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.LblPilihBarang_Lokasi.Location = New System.Drawing.Point(51, 447)
		Me.LblPilihBarang_Lokasi.Name = "LblPilihBarang_Lokasi"
		Me.LblPilihBarang_Lokasi.Size = New System.Drawing.Size(58, 17)
		Me.LblPilihBarang_Lokasi.TabIndex = 359
		Me.LblPilihBarang_Lokasi.Text = "LokasiX"
		Me.LblPilihBarang_Lokasi.Visible = False
		'
		'CmbPilihBarang_Satuan
		'
		Me.CmbPilihBarang_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CmbPilihBarang_Satuan.FormattingEnabled = True
		Me.CmbPilihBarang_Satuan.Items.AddRange(New Object() {"KG", "TON"})
		Me.CmbPilihBarang_Satuan.Location = New System.Drawing.Point(419, 140)
		Me.CmbPilihBarang_Satuan.Name = "CmbPilihBarang_Satuan"
		Me.CmbPilihBarang_Satuan.Size = New System.Drawing.Size(64, 21)
		Me.CmbPilihBarang_Satuan.TabIndex = 4
		'
		'TxtPilihBarang_Satuan
		'
		Me.TxtPilihBarang_Satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtPilihBarang_Satuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtPilihBarang_Satuan.Enabled = False
		Me.TxtPilihBarang_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.TxtPilihBarang_Satuan.Location = New System.Drawing.Point(391, 86)
		Me.TxtPilihBarang_Satuan.MaxLength = 50
		Me.TxtPilihBarang_Satuan.Name = "TxtPilihBarang_Satuan"
		Me.TxtPilihBarang_Satuan.Size = New System.Drawing.Size(92, 21)
		Me.TxtPilihBarang_Satuan.TabIndex = 1
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label1.Location = New System.Drawing.Point(24, 250)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(101, 17)
		Me.Label1.TabIndex = 363
		Me.Label1.Text = "Tgl Kebutuhan"
		'
		'DateTimePicker1
		'
		Me.DateTimePicker1.Location = New System.Drawing.Point(139, 247)
		Me.DateTimePicker1.Name = "DateTimePicker1"
		Me.DateTimePicker1.Size = New System.Drawing.Size(213, 20)
		Me.DateTimePicker1.TabIndex = 4
		'
		'Lbl_Jumlah
		'
		Me.Lbl_Jumlah.AutoSize = True
		Me.Lbl_Jumlah.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Lbl_Jumlah.Location = New System.Drawing.Point(303, 399)
		Me.Lbl_Jumlah.Name = "Lbl_Jumlah"
		Me.Lbl_Jumlah.Size = New System.Drawing.Size(53, 17)
		Me.Lbl_Jumlah.TabIndex = 365
		Me.Lbl_Jumlah.Text = "Jumlah"
		Me.Lbl_Jumlah.Visible = False
		'
		'txtJumlah
		'
		Me.txtJumlah.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtJumlah.Enabled = False
		Me.txtJumlah.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.txtJumlah.Location = New System.Drawing.Point(364, 397)
		Me.txtJumlah.MaxLength = 50
		Me.txtJumlah.Name = "txtJumlah"
		Me.txtJumlah.Size = New System.Drawing.Size(123, 21)
		Me.txtJumlah.TabIndex = 3
		Me.txtJumlah.Visible = False
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label3.Location = New System.Drawing.Point(48, 424)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(82, 17)
		Me.Label3.TabIndex = 367
		Me.Label3.Text = "Keterangan"
		Me.Label3.Visible = False
		'
		'txtKeterangan
		'
		Me.txtKeterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtKeterangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.txtKeterangan.Location = New System.Drawing.Point(136, 425)
		Me.txtKeterangan.MaxLength = 50
		Me.txtKeterangan.Name = "txtKeterangan"
		Me.txtKeterangan.Size = New System.Drawing.Size(38, 21)
		Me.txtKeterangan.TabIndex = 366
		Me.txtKeterangan.Visible = False
		'
		'Lbl_Sisa
		'
		Me.Lbl_Sisa.AutoSize = True
		Me.Lbl_Sisa.BackColor = System.Drawing.Color.White
		Me.Lbl_Sisa.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Lbl_Sisa.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Lbl_Sisa.Location = New System.Drawing.Point(60, 469)
		Me.Lbl_Sisa.Name = "Lbl_Sisa"
		Me.Lbl_Sisa.Size = New System.Drawing.Size(35, 17)
		Me.Lbl_Sisa.TabIndex = 368
		Me.Lbl_Sisa.Text = "Sisa"
		Me.Lbl_Sisa.Visible = False
		'
		'Txt_Sisa
		'
		Me.Txt_Sisa.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Sisa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Sisa.Enabled = False
		Me.Txt_Sisa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_Sisa.Location = New System.Drawing.Point(199, 469)
		Me.Txt_Sisa.MaxLength = 50
		Me.Txt_Sisa.Name = "Txt_Sisa"
		Me.Txt_Sisa.Size = New System.Drawing.Size(110, 21)
		Me.Txt_Sisa.TabIndex = 369
		Me.Txt_Sisa.Visible = False
		'
		'Txt_Order
		'
		Me.Txt_Order.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Order.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Order.Enabled = False
		Me.Txt_Order.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_Order.Location = New System.Drawing.Point(430, 469)
		Me.Txt_Order.MaxLength = 50
		Me.Txt_Order.Name = "Txt_Order"
		Me.Txt_Order.Size = New System.Drawing.Size(110, 21)
		Me.Txt_Order.TabIndex = 371
		Me.Txt_Order.Visible = False
		'
		'Lbl_Order
		'
		Me.Lbl_Order.AutoSize = True
		Me.Lbl_Order.BackColor = System.Drawing.Color.White
		Me.Lbl_Order.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Lbl_Order.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Lbl_Order.Location = New System.Drawing.Point(382, 439)
		Me.Lbl_Order.Name = "Lbl_Order"
		Me.Lbl_Order.Size = New System.Drawing.Size(45, 17)
		Me.Lbl_Order.TabIndex = 370
		Me.Lbl_Order.Text = "Order"
		Me.Lbl_Order.Visible = False
		'
		'Txt_PR
		'
		Me.Txt_PR.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_PR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_PR.Enabled = False
		Me.Txt_PR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_PR.Location = New System.Drawing.Point(196, 469)
		Me.Txt_PR.MaxLength = 50
		Me.Txt_PR.Name = "Txt_PR"
		Me.Txt_PR.Size = New System.Drawing.Size(110, 21)
		Me.Txt_PR.TabIndex = 373
		Me.Txt_PR.Visible = False
		'
		'Lbl_PR
		'
		Me.Lbl_PR.AutoSize = True
		Me.Lbl_PR.BackColor = System.Drawing.Color.White
		Me.Lbl_PR.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Lbl_PR.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Lbl_PR.Location = New System.Drawing.Point(60, 439)
		Me.Lbl_PR.Name = "Lbl_PR"
		Me.Lbl_PR.Size = New System.Drawing.Size(27, 17)
		Me.Lbl_PR.TabIndex = 372
		Me.Lbl_PR.Text = "PR"
		Me.Lbl_PR.Visible = False
		'
		'Lbl_GetKdBrg
		'
		Me.Lbl_GetKdBrg.AutoSize = True
		Me.Lbl_GetKdBrg.BackColor = System.Drawing.Color.White
		Me.Lbl_GetKdBrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Lbl_GetKdBrg.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Lbl_GetKdBrg.Location = New System.Drawing.Point(326, 427)
		Me.Lbl_GetKdBrg.Name = "Lbl_GetKdBrg"
		Me.Lbl_GetKdBrg.Size = New System.Drawing.Size(70, 17)
		Me.Lbl_GetKdBrg.TabIndex = 374
		Me.Lbl_GetKdBrg.Text = "GetKdBrg"
		Me.Lbl_GetKdBrg.Visible = False
		'
		'Txt_KDSo
		'
		Me.Txt_KDSo.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_KDSo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_KDSo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_KDSo.Location = New System.Drawing.Point(116, 448)
		Me.Txt_KDSo.MaxLength = 50
		Me.Txt_KDSo.Name = "Txt_KDSo"
		Me.Txt_KDSo.Size = New System.Drawing.Size(38, 21)
		Me.Txt_KDSo.TabIndex = 366
		Me.Txt_KDSo.Visible = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label2.Location = New System.Drawing.Point(24, 168)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(82, 17)
		Me.Label2.TabIndex = 349
		Me.Label2.Text = "Cost Center"
		'
		'Txt_CostCenter
		'
		Me.Txt_CostCenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_CostCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_CostCenter.Enabled = False
		Me.Txt_CostCenter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_CostCenter.Location = New System.Drawing.Point(139, 166)
		Me.Txt_CostCenter.MaxLength = 50
		Me.Txt_CostCenter.Name = "Txt_CostCenter"
		Me.Txt_CostCenter.Size = New System.Drawing.Size(344, 21)
		Me.Txt_CostCenter.TabIndex = 2
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label4.Location = New System.Drawing.Point(24, 195)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(59, 17)
		Me.Label4.TabIndex = 349
		Me.Label4.Text = "Gedung"
		'
		'Txt_KdGedung
		'
		Me.Txt_KdGedung.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_KdGedung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_KdGedung.Enabled = False
		Me.Txt_KdGedung.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_KdGedung.Location = New System.Drawing.Point(139, 193)
		Me.Txt_KdGedung.MaxLength = 50
		Me.Txt_KdGedung.Name = "Txt_KdGedung"
		Me.Txt_KdGedung.Size = New System.Drawing.Size(60, 21)
		Me.Txt_KdGedung.TabIndex = 3
		'
		'Txt_Gedung
		'
		Me.Txt_Gedung.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Gedung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Gedung.Enabled = False
		Me.Txt_Gedung.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_Gedung.Location = New System.Drawing.Point(203, 193)
		Me.Txt_Gedung.MaxLength = 50
		Me.Txt_Gedung.Name = "Txt_Gedung"
		Me.Txt_Gedung.Size = New System.Drawing.Size(280, 21)
		Me.Txt_Gedung.TabIndex = 1
		'
		'Lv_CostCenter
		'
		Me.Lv_CostCenter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
		Me.Lv_CostCenter.FullRowSelect = True
		Me.Lv_CostCenter.GridLines = True
		Me.Lv_CostCenter.HideSelection = False
		Me.Lv_CostCenter.Location = New System.Drawing.Point(791, 190)
		Me.Lv_CostCenter.Name = "Lv_CostCenter"
		Me.Lv_CostCenter.Size = New System.Drawing.Size(394, 181)
		Me.Lv_CostCenter.TabIndex = 384
		Me.Lv_CostCenter.UseCompatibleStateImageBehavior = False
		Me.Lv_CostCenter.View = System.Windows.Forms.View.Details
		'
		'Lv_Gedung
		'
		Me.Lv_Gedung.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
		Me.Lv_Gedung.FullRowSelect = True
		Me.Lv_Gedung.GridLines = True
		Me.Lv_Gedung.HideSelection = False
		Me.Lv_Gedung.Location = New System.Drawing.Point(791, 216)
		Me.Lv_Gedung.Name = "Lv_Gedung"
		Me.Lv_Gedung.Size = New System.Drawing.Size(394, 181)
		Me.Lv_Gedung.TabIndex = 384
		Me.Lv_Gedung.UseCompatibleStateImageBehavior = False
		Me.Lv_Gedung.View = System.Windows.Forms.View.Details
		'
		'Txt_IdGedung
		'
		Me.Txt_IdGedung.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_IdGedung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_IdGedung.Enabled = False
		Me.Txt_IdGedung.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_IdGedung.Location = New System.Drawing.Point(489, 194)
		Me.Txt_IdGedung.MaxLength = 50
		Me.Txt_IdGedung.Name = "Txt_IdGedung"
		Me.Txt_IdGedung.Size = New System.Drawing.Size(60, 21)
		Me.Txt_IdGedung.TabIndex = 0
		Me.Txt_IdGedung.Visible = False
		'
		'Txt_LokasiGudang
		'
		Me.Txt_LokasiGudang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_LokasiGudang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_LokasiGudang.Enabled = False
		Me.Txt_LokasiGudang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_LokasiGudang.Location = New System.Drawing.Point(489, 220)
		Me.Txt_LokasiGudang.MaxLength = 50
		Me.Txt_LokasiGudang.Name = "Txt_LokasiGudang"
		Me.Txt_LokasiGudang.Size = New System.Drawing.Size(60, 21)
		Me.Txt_LokasiGudang.TabIndex = 0
		Me.Txt_LokasiGudang.Visible = False
		'
		'Txt_Id_CostCenter
		'
		Me.Txt_Id_CostCenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Id_CostCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Id_CostCenter.Enabled = False
		Me.Txt_Id_CostCenter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_Id_CostCenter.Location = New System.Drawing.Point(489, 167)
		Me.Txt_Id_CostCenter.MaxLength = 50
		Me.Txt_Id_CostCenter.Name = "Txt_Id_CostCenter"
		Me.Txt_Id_CostCenter.Size = New System.Drawing.Size(60, 21)
		Me.Txt_Id_CostCenter.TabIndex = 0
		Me.Txt_Id_CostCenter.Visible = False
		'
		'Cmb_Stock
		'
		Me.Cmb_Stock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Stock.FormattingEnabled = True
		Me.Cmb_Stock.Items.AddRange(New Object() {"KG", "TON"})
		Me.Cmb_Stock.Location = New System.Drawing.Point(423, 243)
		Me.Cmb_Stock.Name = "Cmb_Stock"
		Me.Cmb_Stock.Size = New System.Drawing.Size(60, 21)
		Me.Cmb_Stock.TabIndex = 4
		Me.Cmb_Stock.Visible = False
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label5.Location = New System.Drawing.Point(506, 247)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(43, 17)
		Me.Label5.TabIndex = 356
		Me.Label5.Text = "Stock"
		Me.Label5.Visible = False
		'
		'Txt_SisaStock
		'
		Me.Txt_SisaStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_SisaStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_SisaStock.Enabled = False
		Me.Txt_SisaStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_SisaStock.Location = New System.Drawing.Point(139, 139)
		Me.Txt_SisaStock.MaxLength = 50
		Me.Txt_SisaStock.Name = "Txt_SisaStock"
		Me.Txt_SisaStock.Size = New System.Drawing.Size(219, 21)
		Me.Txt_SisaStock.TabIndex = 0
		Me.Txt_SisaStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label6.Location = New System.Drawing.Point(24, 141)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(74, 17)
		Me.Label6.TabIndex = 351
		Me.Label6.Text = "Sisa Stock"
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label8.Location = New System.Drawing.Point(24, 275)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(34, 17)
		Me.Label8.TabIndex = 391
		Me.Label8.Text = "Link"
		'
		'TextBox1
		'
		Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.TextBox1.Location = New System.Drawing.Point(139, 273)
		Me.TextBox1.MaxLength = 300
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(344, 21)
		Me.TextBox1.TabIndex = 392
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label7.Location = New System.Drawing.Point(236, 222)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(33, 17)
		Me.Label7.TabIndex = 395
		Me.Label7.Text = "Day"
		'
		'TxtTiba
		'
		Me.TxtTiba.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtTiba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtTiba.Enabled = False
		Me.TxtTiba.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.TxtTiba.Location = New System.Drawing.Point(139, 220)
		Me.TxtTiba.MaxLength = 50
		Me.TxtTiba.Name = "TxtTiba"
		Me.TxtTiba.Size = New System.Drawing.Size(91, 21)
		Me.TxtTiba.TabIndex = 394
		Me.TxtTiba.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label9.Location = New System.Drawing.Point(22, 222)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(60, 17)
		Me.Label9.TabIndex = 393
		Me.Label9.Text = "Est Tiba"
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label10.Location = New System.Drawing.Point(24, 302)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(47, 17)
		Me.Label10.TabIndex = 396
		Me.Label10.Text = "Harga"
		'
		'TxtHarga
		'
		Me.TxtHarga.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtHarga.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtHarga.Enabled = False
		Me.TxtHarga.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.TxtHarga.Location = New System.Drawing.Point(139, 300)
		Me.TxtHarga.MaxLength = 300
		Me.TxtHarga.Name = "TxtHarga"
		Me.TxtHarga.Size = New System.Drawing.Size(344, 21)
		Me.TxtHarga.TabIndex = 397
		'
		'SD_Tambah_PR_Barang_Lain
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(575, 371)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.Lv_Gedung)
		Me.Controls.Add(Me.Lv_CostCenter)
		Me.Controls.Add(Me.LvPilihBarang_DataBarang)
		Me.Controls.Add(Me.CmbPilihBarang_Lokasi)
		Me.Controls.Add(Me.Lbl_GetKdBrg)
		Me.Controls.Add(Me.Txt_PR)
		Me.Controls.Add(Me.Lbl_PR)
		Me.Controls.Add(Me.Txt_Order)
		Me.Controls.Add(Me.Lbl_Order)
		Me.Controls.Add(Me.Txt_Sisa)
		Me.Controls.Add(Me.Lbl_Sisa)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Txt_KDSo)
		Me.Controls.Add(Me.txtKeterangan)
		Me.Controls.Add(Me.Lbl_Jumlah)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.DateTimePicker1)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Txt_Gedung)
		Me.Controls.Add(Me.TxtPilihBarang_Satuan)
		Me.Controls.Add(Me.LblPilihBarang_Lokasi)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.LblPilihBarang_Satuan)
		Me.Controls.Add(Me.BtnPilihBarang_Refresh)
		Me.Controls.Add(Me.BtnPilihBarang_Simpan)
		Me.Controls.Add(Me.TxtPilihBarang_NamaBarang)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.LblPilihBarang_NamaBarang)
		Me.Controls.Add(Me.Txt_SisaStock)
		Me.Controls.Add(Me.Txt_LokasiGudang)
		Me.Controls.Add(Me.Txt_Id_CostCenter)
		Me.Controls.Add(Me.Txt_IdGedung)
		Me.Controls.Add(Me.Txt_KdGedung)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Txt_CostCenter)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.TxtPilihBarang_KodeBarang)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.LblPilihBarang_KodeBarang)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.Controls.Add(Me.Cmb_Stock)
		Me.Controls.Add(Me.CmbPilihBarang_Satuan)
		Me.Controls.Add(Me.txtJumlah)
		Me.Controls.Add(Me.TextBox1)
		Me.Controls.Add(Me.Label7)
		Me.Controls.Add(Me.TxtTiba)
		Me.Controls.Add(Me.Label9)
		Me.Controls.Add(Me.TxtHarga)
		Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
		Me.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "SD_Tambah_PR_Barang_Lain"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents LblPilihBarang_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents BtnPilihBarang_Refresh As Button
    Friend WithEvents BtnPilihBarang_Simpan As Button
    Friend WithEvents TxtPilihBarang_NamaBarang As TextBox
    Friend WithEvents LblPilihBarang_NamaBarang As Label
    Friend WithEvents TxtPilihBarang_KodeBarang As TextBox
    Friend WithEvents LblPilihBarang_KodeBarang As Label
    Friend WithEvents LblPilihBarang_Satuan As Label
    Friend WithEvents LvPilihBarang_DataBarang As ListView
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents LblPilihBarang_Lokasi As Label
    Friend WithEvents CmbPilihBarang_Lokasi As ComboBox
    Friend WithEvents CmbPilihBarang_Satuan As ComboBox
    Friend WithEvents TxtPilihBarang_Satuan As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents Lbl_Jumlah As Label
    Friend WithEvents txtJumlah As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtKeterangan As TextBox
    Friend WithEvents Lbl_Sisa As Label
    Friend WithEvents Txt_Sisa As TextBox
    Friend WithEvents Txt_Order As TextBox
    Friend WithEvents Lbl_Order As Label
    Friend WithEvents Txt_PR As TextBox
    Friend WithEvents Lbl_PR As Label
    Friend WithEvents Lbl_GetKdBrg As Label
    Friend WithEvents Txt_KDSo As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_CostCenter As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_KdGedung As TextBox
    Friend WithEvents Txt_Gedung As TextBox
    Friend WithEvents Lv_CostCenter As ListView
    Friend WithEvents Lv_Gedung As ListView
    Friend WithEvents Txt_IdGedung As TextBox
    Friend WithEvents Txt_LokasiGudang As TextBox
    Friend WithEvents Txt_Id_CostCenter As TextBox
    Friend WithEvents Cmb_Stock As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_SisaStock As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtTiba As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents TxtHarga As TextBox
End Class
