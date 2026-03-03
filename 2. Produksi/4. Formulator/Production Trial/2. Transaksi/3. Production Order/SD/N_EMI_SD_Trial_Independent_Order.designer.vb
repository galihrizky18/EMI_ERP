<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_SD_Trial_Independent_Order
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.BtnPilihBarang_Refresh = New System.Windows.Forms.Button()
        Me.BtnPilihBarang_Simpan = New System.Windows.Forms.Button()
        Me.LblPilihBarang_KodeBarang = New System.Windows.Forms.Label()
        Me.LvPilihBarang_DataBarang = New System.Windows.Forms.ListView()
        Me.LblPilihBarang_Lokasi = New System.Windows.Forms.Label()
        Me.cmb_Lokasi_Init_Faktur = New System.Windows.Forms.ComboBox()
        Me.TxtPilihBarang_Satuan = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtPilihBarang_KodeBarang = New System.Windows.Forms.TextBox()
        Me.txt_no_faktur = New System.Windows.Forms.TextBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtLokasi_Gudang = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.TxtNamaBarang = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtJenisProduk = New System.Windows.Forms.TextBox()
        Me.TxtKso = New System.Windows.Forms.TextBox()
        Me.txtIdJenisProduk = New System.Windows.Forms.TextBox()
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
        Me.Panel1.Size = New System.Drawing.Size(853, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(853, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'LblPilihBarang_Judul
        '
        Me.LblPilihBarang_Judul.AutoSize = True
        Me.LblPilihBarang_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPilihBarang_Judul.Location = New System.Drawing.Point(15, 11)
        Me.LblPilihBarang_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblPilihBarang_Judul.Name = "LblPilihBarang_Judul"
        Me.LblPilihBarang_Judul.Size = New System.Drawing.Size(453, 25)
        Me.LblPilihBarang_Judul.TabIndex = 0
        Me.LblPilihBarang_Judul.Text = "Transaksi - Independent Order Formulator"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 582)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(833, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 461)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 512)
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
        Me.BtnPilihBarang_Refresh.Location = New System.Drawing.Point(111, 479)
        Me.BtnPilihBarang_Refresh.Name = "BtnPilihBarang_Refresh"
        Me.BtnPilihBarang_Refresh.Size = New System.Drawing.Size(75, 30)
        Me.BtnPilihBarang_Refresh.TabIndex = 355
        Me.BtnPilihBarang_Refresh.Text = "&Refresh"
        Me.BtnPilihBarang_Refresh.UseVisualStyleBackColor = False
        '
        'BtnPilihBarang_Simpan
        '
        Me.BtnPilihBarang_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnPilihBarang_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnPilihBarang_Simpan.ForeColor = System.Drawing.Color.White
        Me.BtnPilihBarang_Simpan.Location = New System.Drawing.Point(30, 479)
        Me.BtnPilihBarang_Simpan.Name = "BtnPilihBarang_Simpan"
        Me.BtnPilihBarang_Simpan.Size = New System.Drawing.Size(75, 30)
        Me.BtnPilihBarang_Simpan.TabIndex = 353
        Me.BtnPilihBarang_Simpan.Text = "&Simpan"
        Me.BtnPilihBarang_Simpan.UseVisualStyleBackColor = False
        '
        'LblPilihBarang_KodeBarang
        '
        Me.LblPilihBarang_KodeBarang.AutoSize = True
        Me.LblPilihBarang_KodeBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblPilihBarang_KodeBarang.Location = New System.Drawing.Point(27, 93)
        Me.LblPilihBarang_KodeBarang.Name = "LblPilihBarang_KodeBarang"
        Me.LblPilihBarang_KodeBarang.Size = New System.Drawing.Size(100, 17)
        Me.LblPilihBarang_KodeBarang.TabIndex = 349
        Me.LblPilihBarang_KodeBarang.Text = "Kode BarangX"
        '
        'LvPilihBarang_DataBarang
        '
        Me.LvPilihBarang_DataBarang.FullRowSelect = True
        Me.LvPilihBarang_DataBarang.GridLines = True
        Me.LvPilihBarang_DataBarang.HideSelection = False
        Me.LvPilihBarang_DataBarang.Location = New System.Drawing.Point(277, 484)
        Me.LvPilihBarang_DataBarang.Name = "LvPilihBarang_DataBarang"
        Me.LvPilihBarang_DataBarang.Size = New System.Drawing.Size(440, 130)
        Me.LvPilihBarang_DataBarang.TabIndex = 358
        Me.LvPilihBarang_DataBarang.UseCompatibleStateImageBehavior = False
        Me.LvPilihBarang_DataBarang.View = System.Windows.Forms.View.Details
        '
        'LblPilihBarang_Lokasi
        '
        Me.LblPilihBarang_Lokasi.AutoSize = True
        Me.LblPilihBarang_Lokasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblPilihBarang_Lokasi.Location = New System.Drawing.Point(912, 97)
        Me.LblPilihBarang_Lokasi.Name = "LblPilihBarang_Lokasi"
        Me.LblPilihBarang_Lokasi.Size = New System.Drawing.Size(58, 17)
        Me.LblPilihBarang_Lokasi.TabIndex = 359
        Me.LblPilihBarang_Lokasi.Text = "LokasiX"
        Me.LblPilihBarang_Lokasi.Visible = False
        '
        'cmb_Lokasi_Init_Faktur
        '
        Me.cmb_Lokasi_Init_Faktur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Lokasi_Init_Faktur.FormattingEnabled = True
        Me.cmb_Lokasi_Init_Faktur.Items.AddRange(New Object() {"HEAD OFFICE"})
        Me.cmb_Lokasi_Init_Faktur.Location = New System.Drawing.Point(855, 72)
        Me.cmb_Lokasi_Init_Faktur.Name = "cmb_Lokasi_Init_Faktur"
        Me.cmb_Lokasi_Init_Faktur.Size = New System.Drawing.Size(122, 21)
        Me.cmb_Lokasi_Init_Faktur.TabIndex = 360
        Me.cmb_Lokasi_Init_Faktur.Visible = False
        '
        'TxtPilihBarang_Satuan
        '
        Me.TxtPilihBarang_Satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPilihBarang_Satuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPilihBarang_Satuan.Enabled = False
        Me.TxtPilihBarang_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtPilihBarang_Satuan.Location = New System.Drawing.Point(251, 146)
        Me.TxtPilihBarang_Satuan.MaxLength = 50
        Me.TxtPilihBarang_Satuan.Name = "TxtPilihBarang_Satuan"
        Me.TxtPilihBarang_Satuan.Size = New System.Drawing.Size(116, 21)
        Me.TxtPilihBarang_Satuan.TabIndex = 362
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TextBox1.Location = New System.Drawing.Point(156, 146)
        Me.TextBox1.MaxLength = 50
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(94, 21)
        Me.TextBox1.TabIndex = 366
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(27, 148)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 17)
        Me.Label2.TabIndex = 365
        Me.Label2.Text = "Jumlah"
        '
        'TxtPilihBarang_KodeBarang
        '
        Me.TxtPilihBarang_KodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPilihBarang_KodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPilihBarang_KodeBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtPilihBarang_KodeBarang.Location = New System.Drawing.Point(156, 91)
        Me.TxtPilihBarang_KodeBarang.MaxLength = 50
        Me.TxtPilihBarang_KodeBarang.Name = "TxtPilihBarang_KodeBarang"
        Me.TxtPilihBarang_KodeBarang.Size = New System.Drawing.Size(161, 21)
        Me.TxtPilihBarang_KodeBarang.TabIndex = 350
        '
        'txt_no_faktur
        '
        Me.txt_no_faktur.BackColor = System.Drawing.Color.Goldenrod
        Me.txt_no_faktur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_no_faktur.ForeColor = System.Drawing.SystemColors.Window
        Me.txt_no_faktur.Location = New System.Drawing.Point(20, 63)
        Me.txt_no_faktur.MaxLength = 30
        Me.txt_no_faktur.Name = "txt_no_faktur"
        Me.txt_no_faktur.ReadOnly = True
        Me.txt_no_faktur.Size = New System.Drawing.Size(227, 21)
        Me.txt_no_faktur.TabIndex = 390
        '
        'ListView1
        '
        Me.ListView1.CheckBoxes = True
        Me.ListView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(23, 177)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(809, 245)
        Me.ListView1.TabIndex = 391
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(370, 143)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(84, 28)
        Me.Button1.TabIndex = 392
        Me.Button1.Text = "Pilih"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TextBox2.Location = New System.Drawing.Point(156, 441)
        Me.TextBox2.MaxLength = 100
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(439, 21)
        Me.TextBox2.TabIndex = 393
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(27, 443)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 17)
        Me.Label3.TabIndex = 394
        Me.Label3.Text = "Keterangan"
        '
        'txtLokasi_Gudang
        '
        Me.txtLokasi_Gudang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtLokasi_Gudang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLokasi_Gudang.Enabled = False
        Me.txtLokasi_Gudang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txtLokasi_Gudang.Location = New System.Drawing.Point(870, 117)
        Me.txtLokasi_Gudang.MaxLength = 50
        Me.txtLokasi_Gudang.Name = "txtLokasi_Gudang"
        Me.txtLokasi_Gudang.ReadOnly = True
        Me.txtLokasi_Gudang.Size = New System.Drawing.Size(111, 21)
        Me.txtLokasi_Gudang.TabIndex = 396
        Me.txtLokasi_Gudang.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(16, 464)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1028, 15)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(11, 424)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1028, 15)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'TxtNamaBarang
        '
        Me.TxtNamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtNamaBarang.Location = New System.Drawing.Point(318, 91)
        Me.TxtNamaBarang.MaxLength = 50
        Me.TxtNamaBarang.Name = "TxtNamaBarang"
        Me.TxtNamaBarang.Size = New System.Drawing.Size(277, 21)
        Me.TxtNamaBarang.TabIndex = 398
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(27, 121)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 17)
        Me.Label1.TabIndex = 400
        Me.Label1.Text = "Jenis"
        '
        'TxtJenisProduk
        '
        Me.TxtJenisProduk.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJenisProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJenisProduk.Enabled = False
        Me.TxtJenisProduk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtJenisProduk.Location = New System.Drawing.Point(368, 119)
        Me.TxtJenisProduk.MaxLength = 50
        Me.TxtJenisProduk.Name = "TxtJenisProduk"
        Me.TxtJenisProduk.ReadOnly = True
        Me.TxtJenisProduk.Size = New System.Drawing.Size(227, 21)
        Me.TxtJenisProduk.TabIndex = 399
        '
        'TxtKso
        '
        Me.TxtKso.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKso.Enabled = False
        Me.TxtKso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtKso.Location = New System.Drawing.Point(156, 119)
        Me.TxtKso.MaxLength = 50
        Me.TxtKso.Name = "TxtKso"
        Me.TxtKso.Size = New System.Drawing.Size(211, 21)
        Me.TxtKso.TabIndex = 401
        '
        'txtIdJenisProduk
        '
        Me.txtIdJenisProduk.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtIdJenisProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdJenisProduk.Enabled = False
        Me.txtIdJenisProduk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txtIdJenisProduk.Location = New System.Drawing.Point(156, 119)
        Me.txtIdJenisProduk.MaxLength = 50
        Me.txtIdJenisProduk.Name = "txtIdJenisProduk"
        Me.txtIdJenisProduk.ReadOnly = True
        Me.txtIdJenisProduk.Size = New System.Drawing.Size(211, 21)
        Me.txtIdJenisProduk.TabIndex = 402
        Me.txtIdJenisProduk.Visible = False
        '
        'N_EMI_SD_Trial_Independent_Order
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(853, 526)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtJenisProduk)
        Me.Controls.Add(Me.TxtKso)
        Me.Controls.Add(Me.TxtNamaBarang)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.txtLokasi_Gudang)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.LvPilihBarang_DataBarang)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.txt_no_faktur)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtPilihBarang_Satuan)
        Me.Controls.Add(Me.cmb_Lokasi_Init_Faktur)
        Me.Controls.Add(Me.LblPilihBarang_Lokasi)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.BtnPilihBarang_Refresh)
        Me.Controls.Add(Me.BtnPilihBarang_Simpan)
        Me.Controls.Add(Me.TxtPilihBarang_KodeBarang)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.LblPilihBarang_KodeBarang)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.txtIdJenisProduk)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_SD_Trial_Independent_Order"
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
    Friend WithEvents LblPilihBarang_KodeBarang As Label
    Friend WithEvents LvPilihBarang_DataBarang As ListView
    Friend WithEvents LblPilihBarang_Lokasi As Label
    Friend WithEvents cmb_Lokasi_Init_Faktur As ComboBox
    Friend WithEvents TxtPilihBarang_Satuan As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtPilihBarang_KodeBarang As TextBox
    Friend WithEvents txt_no_faktur As TextBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtLokasi_Gudang As TextBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents TxtNamaBarang As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtJenisProduk As TextBox
    Friend WithEvents TxtKso As TextBox
    Friend WithEvents txtIdJenisProduk As TextBox
End Class
