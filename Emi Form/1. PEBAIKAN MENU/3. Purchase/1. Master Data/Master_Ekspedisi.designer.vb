<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Ekspedisi
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lbl_Kode = New System.Windows.Forms.Label()
        Me.Txt_KodeEkspedisi = New System.Windows.Forms.TextBox()
        Me.Txt_NmEkspedisi = New System.Windows.Forms.TextBox()
        Me.Lbl_Nama = New System.Windows.Forms.Label()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Lbl_Kolom = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lbl_Alamat = New System.Windows.Forms.Label()
        Me.Txt_AlmtEkspedisi = New System.Windows.Forms.TextBox()
        Me.Txt_TelpEkspedisi = New System.Windows.Forms.TextBox()
        Me.Lbl_Telp = New System.Windows.Forms.Label()
        Me.Txt_PenanggungJawab = New System.Windows.Forms.TextBox()
        Me.Lbl_PIC = New System.Windows.Forms.Label()
        Me.Lbl_Pembayaran = New System.Windows.Forms.Label()
        Me.Cmb_Pembayaran = New System.Windows.Forms.ComboBox()
        Me.Lbl_GolPPH = New System.Windows.Forms.Label()
        Me.Cmb_GolPPH = New System.Windows.Forms.ComboBox()
        Me.Txt_NilaiPPH = New System.Windows.Forms.TextBox()
        Me.Lbl_NilaiPPH = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
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
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(260, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master Data - Ekspedisi"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 416)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(922, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 416)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(41, 786)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Lbl_Kode
        '
        Me.Lbl_Kode.AutoSize = True
        Me.Lbl_Kode.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kode.Location = New System.Drawing.Point(23, 70)
        Me.Lbl_Kode.Name = "Lbl_Kode"
        Me.Lbl_Kode.Size = New System.Drawing.Size(42, 20)
        Me.Lbl_Kode.TabIndex = 227
        Me.Lbl_Kode.Text = "Kode"
        '
        'Txt_KodeEkspedisi
        '
        Me.Txt_KodeEkspedisi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KodeEkspedisi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KodeEkspedisi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_KodeEkspedisi.Location = New System.Drawing.Point(211, 69)
        Me.Txt_KodeEkspedisi.MaxLength = 50
        Me.Txt_KodeEkspedisi.Name = "Txt_KodeEkspedisi"
        Me.Txt_KodeEkspedisi.Size = New System.Drawing.Size(350, 22)
        Me.Txt_KodeEkspedisi.TabIndex = 228
        '
        'Txt_NmEkspedisi
        '
        Me.Txt_NmEkspedisi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmEkspedisi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmEkspedisi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NmEkspedisi.Location = New System.Drawing.Point(211, 97)
        Me.Txt_NmEkspedisi.MaxLength = 100
        Me.Txt_NmEkspedisi.Name = "Txt_NmEkspedisi"
        Me.Txt_NmEkspedisi.Size = New System.Drawing.Size(350, 22)
        Me.Txt_NmEkspedisi.TabIndex = 230
        '
        'Lbl_Nama
        '
        Me.Lbl_Nama.AutoSize = True
        Me.Lbl_Nama.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Nama.Location = New System.Drawing.Point(23, 98)
        Me.Lbl_Nama.Name = "Lbl_Nama"
        Me.Lbl_Nama.Size = New System.Drawing.Size(118, 20)
        Me.Lbl_Nama.TabIndex = 229
        Me.Lbl_Nama.Text = "Nama Ekspedisi"
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(210, 366)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 231
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(300, 366)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Hapus.TabIndex = 232
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(390, 366)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 233
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'ListView1
        '
        Me.ListView1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(21, 453)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(900, 248)
        Me.ListView1.TabIndex = 234
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TextBox3.Location = New System.Drawing.Point(347, 424)
        Me.TextBox3.MaxLength = 50
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(189, 22)
        Me.TextBox3.TabIndex = 235
        '
        'Lbl_Kolom
        '
        Me.Lbl_Kolom.AutoSize = True
        Me.Lbl_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kolom.Location = New System.Drawing.Point(27, 425)
        Me.Lbl_Kolom.Name = "Lbl_Kolom"
        Me.Lbl_Kolom.Size = New System.Drawing.Size(50, 20)
        Me.Lbl_Kolom.TabIndex = 236
        Me.Lbl_Kolom.Text = "Kolom"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(293, 425)
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
        Me.Btn_Cari.Location = New System.Drawing.Point(539, 421)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Cari.TabIndex = 338
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.DropDownWidth = 150
        Me.ComboBox1.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(92, 424)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(195, 25)
        Me.ComboBox1.TabIndex = 339
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(210, 355)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(344, 12)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(20, 402)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(938, 19)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'Lbl_Alamat
        '
        Me.Lbl_Alamat.AutoSize = True
        Me.Lbl_Alamat.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Alamat.Location = New System.Drawing.Point(23, 126)
        Me.Lbl_Alamat.Name = "Lbl_Alamat"
        Me.Lbl_Alamat.Size = New System.Drawing.Size(127, 20)
        Me.Lbl_Alamat.TabIndex = 340
        Me.Lbl_Alamat.Text = "Alamat Ekspedisi"
        '
        'Txt_AlmtEkspedisi
        '
        Me.Txt_AlmtEkspedisi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_AlmtEkspedisi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_AlmtEkspedisi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_AlmtEkspedisi.Location = New System.Drawing.Point(211, 125)
        Me.Txt_AlmtEkspedisi.MaxLength = 50
        Me.Txt_AlmtEkspedisi.Multiline = True
        Me.Txt_AlmtEkspedisi.Name = "Txt_AlmtEkspedisi"
        Me.Txt_AlmtEkspedisi.Size = New System.Drawing.Size(350, 77)
        Me.Txt_AlmtEkspedisi.TabIndex = 355
        '
        'Txt_TelpEkspedisi
        '
        Me.Txt_TelpEkspedisi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TelpEkspedisi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TelpEkspedisi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_TelpEkspedisi.Location = New System.Drawing.Point(211, 208)
        Me.Txt_TelpEkspedisi.MaxLength = 50
        Me.Txt_TelpEkspedisi.Name = "Txt_TelpEkspedisi"
        Me.Txt_TelpEkspedisi.Size = New System.Drawing.Size(350, 22)
        Me.Txt_TelpEkspedisi.TabIndex = 375
        '
        'Lbl_Telp
        '
        Me.Lbl_Telp.AutoSize = True
        Me.Lbl_Telp.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Telp.Location = New System.Drawing.Point(23, 209)
        Me.Lbl_Telp.Name = "Lbl_Telp"
        Me.Lbl_Telp.Size = New System.Drawing.Size(131, 20)
        Me.Lbl_Telp.TabIndex = 374
        Me.Lbl_Telp.Text = "Telepon Ekspedisi"
        '
        'Txt_PenanggungJawab
        '
        Me.Txt_PenanggungJawab.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_PenanggungJawab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_PenanggungJawab.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_PenanggungJawab.Location = New System.Drawing.Point(211, 239)
        Me.Txt_PenanggungJawab.MaxLength = 50
        Me.Txt_PenanggungJawab.Name = "Txt_PenanggungJawab"
        Me.Txt_PenanggungJawab.Size = New System.Drawing.Size(350, 22)
        Me.Txt_PenanggungJawab.TabIndex = 377
        '
        'Lbl_PIC
        '
        Me.Lbl_PIC.AutoSize = True
        Me.Lbl_PIC.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_PIC.Location = New System.Drawing.Point(23, 240)
        Me.Lbl_PIC.Name = "Lbl_PIC"
        Me.Lbl_PIC.Size = New System.Drawing.Size(102, 20)
        Me.Lbl_PIC.TabIndex = 376
        Me.Lbl_PIC.Text = "PIC Ekspedisi"
        '
        'Lbl_Pembayaran
        '
        Me.Lbl_Pembayaran.AutoSize = True
        Me.Lbl_Pembayaran.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Pembayaran.Location = New System.Drawing.Point(23, 271)
        Me.Lbl_Pembayaran.Name = "Lbl_Pembayaran"
        Me.Lbl_Pembayaran.Size = New System.Drawing.Size(94, 20)
        Me.Lbl_Pembayaran.TabIndex = 383
        Me.Lbl_Pembayaran.Text = "Pembayaran"
        '
        'Cmb_Pembayaran
        '
        Me.Cmb_Pembayaran.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Pembayaran.FormattingEnabled = True
        Me.Cmb_Pembayaran.Location = New System.Drawing.Point(211, 270)
        Me.Cmb_Pembayaran.Name = "Cmb_Pembayaran"
        Me.Cmb_Pembayaran.Size = New System.Drawing.Size(228, 24)
        Me.Cmb_Pembayaran.TabIndex = 382
        '
        'Lbl_GolPPH
        '
        Me.Lbl_GolPPH.AutoSize = True
        Me.Lbl_GolPPH.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_GolPPH.Location = New System.Drawing.Point(23, 301)
        Me.Lbl_GolPPH.Name = "Lbl_GolPPH"
        Me.Lbl_GolPPH.Size = New System.Drawing.Size(105, 20)
        Me.Lbl_GolPPH.TabIndex = 385
        Me.Lbl_GolPPH.Text = "Golongan PPH"
        '
        'Cmb_GolPPH
        '
        Me.Cmb_GolPPH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_GolPPH.FormattingEnabled = True
        Me.Cmb_GolPPH.Location = New System.Drawing.Point(211, 300)
        Me.Cmb_GolPPH.Name = "Cmb_GolPPH"
        Me.Cmb_GolPPH.Size = New System.Drawing.Size(228, 24)
        Me.Cmb_GolPPH.TabIndex = 384
        '
        'Txt_NilaiPPH
        '
        Me.Txt_NilaiPPH.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NilaiPPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NilaiPPH.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NilaiPPH.Location = New System.Drawing.Point(210, 330)
        Me.Txt_NilaiPPH.MaxLength = 50
        Me.Txt_NilaiPPH.Name = "Txt_NilaiPPH"
        Me.Txt_NilaiPPH.Size = New System.Drawing.Size(54, 22)
        Me.Txt_NilaiPPH.TabIndex = 387
        '
        'Lbl_NilaiPPH
        '
        Me.Lbl_NilaiPPH.AutoSize = True
        Me.Lbl_NilaiPPH.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_NilaiPPH.Location = New System.Drawing.Point(22, 331)
        Me.Lbl_NilaiPPH.Name = "Lbl_NilaiPPH"
        Me.Lbl_NilaiPPH.Size = New System.Drawing.Size(72, 20)
        Me.Lbl_NilaiPPH.TabIndex = 386
        Me.Lbl_NilaiPPH.Text = "Nilai PPH"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(270, 331)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(21, 20)
        Me.Label11.TabIndex = 388
        Me.Label11.Text = "%"
        '
        'Master_Ekspedisi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(942, 715)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Txt_NilaiPPH)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lbl_NilaiPPH)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Lbl_GolPPH)
        Me.Controls.Add(Me.Lbl_Kolom)
        Me.Controls.Add(Me.Cmb_GolPPH)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Lbl_Pembayaran)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Cmb_Pembayaran)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Txt_PenanggungJawab)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Lbl_PIC)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Txt_TelpEkspedisi)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.Lbl_Telp)
        Me.Controls.Add(Me.Txt_AlmtEkspedisi)
        Me.Controls.Add(Me.Lbl_Alamat)
        Me.Controls.Add(Me.Txt_NmEkspedisi)
        Me.Controls.Add(Me.Lbl_Nama)
        Me.Controls.Add(Me.Txt_KodeEkspedisi)
        Me.Controls.Add(Me.Lbl_Kode)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Master_Ekspedisi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lbl_Kode As Label
    Friend WithEvents Txt_KodeEkspedisi As TextBox
    Friend WithEvents Txt_NmEkspedisi As TextBox
    Friend WithEvents Lbl_Nama As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents ListView1 As ListView
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Lbl_Kolom As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lbl_Alamat As Label
    Friend WithEvents Txt_AlmtEkspedisi As TextBox
    Friend WithEvents Txt_TelpEkspedisi As TextBox
    Friend WithEvents Lbl_Telp As Label
    Friend WithEvents Txt_PenanggungJawab As TextBox
    Friend WithEvents Lbl_PIC As Label
    Friend WithEvents Lbl_Pembayaran As Label
    Friend WithEvents Cmb_Pembayaran As ComboBox
    Friend WithEvents Lbl_GolPPH As Label
    Friend WithEvents Cmb_GolPPH As ComboBox
    Friend WithEvents Txt_NilaiPPH As TextBox
    Friend WithEvents Lbl_NilaiPPH As Label
    Friend WithEvents Label11 As Label
End Class
