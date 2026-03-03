<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SD_Tambah_PR
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
        Me.TxtTiba = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
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
        Me.Panel1.Size = New System.Drawing.Size(576, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(576, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'LblPilihBarang_Judul
        '
        Me.LblPilihBarang_Judul.AutoSize = True
        Me.LblPilihBarang_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPilihBarang_Judul.Location = New System.Drawing.Point(15, 11)
        Me.LblPilihBarang_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblPilihBarang_Judul.Name = "LblPilihBarang_Judul"
        Me.LblPilihBarang_Judul.Size = New System.Drawing.Size(193, 25)
        Me.LblPilihBarang_Judul.TabIndex = 0
        Me.LblPilihBarang_Judul.Text = "SD - Pilih Barang"
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
        Me.Panel5.Location = New System.Drawing.Point(559, 63)
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
        Me.CmbPilihBarang_Lokasi.Location = New System.Drawing.Point(235, 194)
        Me.CmbPilihBarang_Lokasi.Name = "CmbPilihBarang_Lokasi"
        Me.CmbPilihBarang_Lokasi.Size = New System.Drawing.Size(228, 21)
        Me.CmbPilihBarang_Lokasi.TabIndex = 6
        Me.CmbPilihBarang_Lokasi.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 236)
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
        Me.BtnPilihBarang_Refresh.Location = New System.Drawing.Point(113, 200)
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
        Me.BtnPilihBarang_Simpan.Location = New System.Drawing.Point(23, 200)
        Me.BtnPilihBarang_Simpan.Name = "BtnPilihBarang_Simpan"
        Me.BtnPilihBarang_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.BtnPilihBarang_Simpan.TabIndex = 7
        Me.BtnPilihBarang_Simpan.Text = "&Simpan"
        Me.BtnPilihBarang_Simpan.UseVisualStyleBackColor = False
        '
        'TxtPilihBarang_NamaBarang
        '
        Me.TxtPilihBarang_NamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPilihBarang_NamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPilihBarang_NamaBarang.Enabled = False
        Me.TxtPilihBarang_NamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtPilihBarang_NamaBarang.Location = New System.Drawing.Point(139, 101)
        Me.TxtPilihBarang_NamaBarang.MaxLength = 100
        Me.TxtPilihBarang_NamaBarang.Name = "TxtPilihBarang_NamaBarang"
        Me.TxtPilihBarang_NamaBarang.Size = New System.Drawing.Size(344, 21)
        Me.TxtPilihBarang_NamaBarang.TabIndex = 2
        '
        'LblPilihBarang_NamaBarang
        '
        Me.LblPilihBarang_NamaBarang.AutoSize = True
        Me.LblPilihBarang_NamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblPilihBarang_NamaBarang.Location = New System.Drawing.Point(24, 101)
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
        Me.TxtPilihBarang_KodeBarang.Location = New System.Drawing.Point(139, 74)
        Me.TxtPilihBarang_KodeBarang.MaxLength = 50
        Me.TxtPilihBarang_KodeBarang.Name = "TxtPilihBarang_KodeBarang"
        Me.TxtPilihBarang_KodeBarang.Size = New System.Drawing.Size(228, 21)
        Me.TxtPilihBarang_KodeBarang.TabIndex = 0
        '
        'LblPilihBarang_KodeBarang
        '
        Me.LblPilihBarang_KodeBarang.AutoSize = True
        Me.LblPilihBarang_KodeBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblPilihBarang_KodeBarang.Location = New System.Drawing.Point(24, 76)
        Me.LblPilihBarang_KodeBarang.Name = "LblPilihBarang_KodeBarang"
        Me.LblPilihBarang_KodeBarang.Size = New System.Drawing.Size(100, 17)
        Me.LblPilihBarang_KodeBarang.TabIndex = 349
        Me.LblPilihBarang_KodeBarang.Text = "Kode BarangX"
        '
        'LblPilihBarang_Satuan
        '
        Me.LblPilihBarang_Satuan.AutoSize = True
        Me.LblPilihBarang_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblPilihBarang_Satuan.Location = New System.Drawing.Point(24, 130)
        Me.LblPilihBarang_Satuan.Name = "LblPilihBarang_Satuan"
        Me.LblPilihBarang_Satuan.Size = New System.Drawing.Size(62, 17)
        Me.LblPilihBarang_Satuan.TabIndex = 356
        Me.LblPilihBarang_Satuan.Text = "SatuanX"
        '
        'LvPilihBarang_DataBarang
        '
        Me.LvPilihBarang_DataBarang.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9})
        Me.LvPilihBarang_DataBarang.FullRowSelect = True
        Me.LvPilihBarang_DataBarang.GridLines = True
        Me.LvPilihBarang_DataBarang.HideSelection = False
        Me.LvPilihBarang_DataBarang.Location = New System.Drawing.Point(502, 70)
        Me.LvPilihBarang_DataBarang.Name = "LvPilihBarang_DataBarang"
        Me.LvPilihBarang_DataBarang.Size = New System.Drawing.Size(414, 130)
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
        Me.ColumnHeader8.Width = 230
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Satuan"
        Me.ColumnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader9.Width = 50
        '
        'LblPilihBarang_Lokasi
        '
        Me.LblPilihBarang_Lokasi.AutoSize = True
        Me.LblPilihBarang_Lokasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblPilihBarang_Lokasi.Location = New System.Drawing.Point(449, 214)
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
        Me.CmbPilihBarang_Satuan.Location = New System.Drawing.Point(139, 129)
        Me.CmbPilihBarang_Satuan.Name = "CmbPilihBarang_Satuan"
        Me.CmbPilihBarang_Satuan.Size = New System.Drawing.Size(144, 21)
        Me.CmbPilihBarang_Satuan.TabIndex = 4
        '
        'TxtPilihBarang_Satuan
        '
        Me.TxtPilihBarang_Satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPilihBarang_Satuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPilihBarang_Satuan.Enabled = False
        Me.TxtPilihBarang_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtPilihBarang_Satuan.Location = New System.Drawing.Point(373, 74)
        Me.TxtPilihBarang_Satuan.MaxLength = 50
        Me.TxtPilihBarang_Satuan.Name = "TxtPilihBarang_Satuan"
        Me.TxtPilihBarang_Satuan.Size = New System.Drawing.Size(110, 21)
        Me.TxtPilihBarang_Satuan.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(24, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 17)
        Me.Label1.TabIndex = 363
        Me.Label1.Text = "Tanggal Delivery"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(139, 158)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(213, 20)
        Me.DateTimePicker1.TabIndex = 5
        '
        'Lbl_Jumlah
        '
        Me.Lbl_Jumlah.AutoSize = True
        Me.Lbl_Jumlah.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Lbl_Jumlah.Location = New System.Drawing.Point(245, 211)
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
        Me.txtJumlah.Location = New System.Drawing.Point(306, 209)
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
        Me.Label3.Location = New System.Drawing.Point(357, 165)
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
        Me.txtKeterangan.Location = New System.Drawing.Point(445, 164)
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
        Me.Lbl_Sisa.Location = New System.Drawing.Point(60, 378)
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
        Me.Txt_Sisa.Location = New System.Drawing.Point(199, 378)
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
        Me.Txt_Order.Location = New System.Drawing.Point(433, 346)
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
        Me.Lbl_Order.Location = New System.Drawing.Point(382, 348)
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
        Me.Txt_PR.Location = New System.Drawing.Point(199, 346)
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
        Me.Lbl_PR.Location = New System.Drawing.Point(60, 348)
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
        Me.Lbl_GetKdBrg.Location = New System.Drawing.Point(598, 316)
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
        Me.Txt_KDSo.Location = New System.Drawing.Point(526, 214)
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
        Me.Label2.Location = New System.Drawing.Point(289, 130)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 17)
        Me.Label2.TabIndex = 375
        Me.Label2.Text = "Est Tiba"
        '
        'TxtTiba
        '
        Me.TxtTiba.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtTiba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTiba.Enabled = False
        Me.TxtTiba.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtTiba.Location = New System.Drawing.Point(353, 128)
        Me.TxtTiba.MaxLength = 50
        Me.TxtTiba.Name = "TxtTiba"
        Me.TxtTiba.Size = New System.Drawing.Size(91, 21)
        Me.TxtTiba.TabIndex = 376
        Me.TxtTiba.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(450, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 17)
        Me.Label4.TabIndex = 377
        Me.Label4.Text = "Day"
        '
        'SD_Tambah_PR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(576, 251)
        Me.Controls.Add(Me.LvPilihBarang_DataBarang)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtTiba)
        Me.Controls.Add(Me.Label2)
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
        Me.Controls.Add(Me.TxtPilihBarang_Satuan)
        Me.Controls.Add(Me.LblPilihBarang_Lokasi)
        Me.Controls.Add(Me.LblPilihBarang_Satuan)
        Me.Controls.Add(Me.BtnPilihBarang_Refresh)
        Me.Controls.Add(Me.BtnPilihBarang_Simpan)
        Me.Controls.Add(Me.TxtPilihBarang_NamaBarang)
        Me.Controls.Add(Me.LblPilihBarang_NamaBarang)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.TxtPilihBarang_KodeBarang)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.LblPilihBarang_KodeBarang)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CmbPilihBarang_Satuan)
        Me.Controls.Add(Me.txtJumlah)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SD_Tambah_PR"
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
    Friend WithEvents TxtTiba As TextBox
    Friend WithEvents Label4 As Label
End Class
