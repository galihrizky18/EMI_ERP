<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Media_Kirim
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
        Me.Lbl_CaraKirim = New System.Windows.Forms.Label()
        Me.Lbl_MediaKirim = New System.Windows.Forms.Label()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.ListView_MediaKirim = New System.Windows.Forms.ListView()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Lbl_Kolom = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lbl_Ukuran = New System.Windows.Forms.Label()
        Me.Txt_Berat = New System.Windows.Forms.TextBox()
        Me.Lbl_Berat = New System.Windows.Forms.Label()
        Me.Txt_Panjang = New System.Windows.Forms.TextBox()
        Me.Lbl_Panjang = New System.Windows.Forms.Label()
        Me.Lbl_Lebar = New System.Windows.Forms.Label()
        Me.Txt_Lebar = New System.Windows.Forms.TextBox()
        Me.Lbl_Tinggi = New System.Windows.Forms.Label()
        Me.Txt_Tinggi = New System.Windows.Forms.TextBox()
        Me.Txt_Volume = New System.Windows.Forms.TextBox()
        Me.Lbl_Volume = New System.Windows.Forms.Label()
        Me.Cmb_SatuanVolume = New System.Windows.Forms.ComboBox()
        Me.Lbl_SatuanVolume = New System.Windows.Forms.Label()
        Me.Cmb_SatuanBerat = New System.Windows.Forms.ComboBox()
        Me.Lbl_SatuanBerat = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Cmb_CaraKirim = New System.Windows.Forms.ComboBox()
        Me.Txt_MediaKirim = New System.Windows.Forms.TextBox()
        Me.Txt_KdMediaKirim = New System.Windows.Forms.TextBox()
        Me.Lbl_KdMediaKirim = New System.Windows.Forms.Label()
        Me.Lbl_plt = New System.Windows.Forms.Label()
        Me.Lbl_SatuanPanjang = New System.Windows.Forms.Label()
        Me.Cmb_SatuanPanjang = New System.Windows.Forms.ComboBox()
        Me.Cmb_ValueCaraKirim = New System.Windows.Forms.ComboBox()
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
        Me.Lbl_Judul.Size = New System.Drawing.Size(285, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master Data - Media Kirim"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 577)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(922, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 581)
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
        'Lbl_CaraKirim
        '
        Me.Lbl_CaraKirim.AutoSize = True
        Me.Lbl_CaraKirim.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_CaraKirim.Location = New System.Drawing.Point(27, 70)
        Me.Lbl_CaraKirim.Name = "Lbl_CaraKirim"
        Me.Lbl_CaraKirim.Size = New System.Drawing.Size(82, 20)
        Me.Lbl_CaraKirim.TabIndex = 227
        Me.Lbl_CaraKirim.Text = "Cara Kirim"
        '
        'Lbl_MediaKirim
        '
        Me.Lbl_MediaKirim.AutoSize = True
        Me.Lbl_MediaKirim.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_MediaKirim.Location = New System.Drawing.Point(27, 128)
        Me.Lbl_MediaKirim.Name = "Lbl_MediaKirim"
        Me.Lbl_MediaKirim.Size = New System.Drawing.Size(91, 20)
        Me.Lbl_MediaKirim.TabIndex = 229
        Me.Lbl_MediaKirim.Text = "Media Kirim"
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(207, 342)
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
        Me.Btn_Hapus.Location = New System.Drawing.Point(297, 342)
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
        Me.Btn_Refresh.Location = New System.Drawing.Point(387, 342)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 233
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'ListView_MediaKirim
        '
        Me.ListView_MediaKirim.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView_MediaKirim.FullRowSelect = True
        Me.ListView_MediaKirim.GridLines = True
        Me.ListView_MediaKirim.HideSelection = False
        Me.ListView_MediaKirim.Location = New System.Drawing.Point(21, 428)
        Me.ListView_MediaKirim.Name = "ListView_MediaKirim"
        Me.ListView_MediaKirim.Size = New System.Drawing.Size(900, 264)
        Me.ListView_MediaKirim.TabIndex = 234
        Me.ListView_MediaKirim.UseCompatibleStateImageBehavior = False
        Me.ListView_MediaKirim.View = System.Windows.Forms.View.Details
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TextBox3.Location = New System.Drawing.Point(343, 400)
        Me.TextBox3.MaxLength = 50
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(189, 22)
        Me.TextBox3.TabIndex = 235
        '
        'Lbl_Kolom
        '
        Me.Lbl_Kolom.AutoSize = True
        Me.Lbl_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kolom.Location = New System.Drawing.Point(23, 401)
        Me.Lbl_Kolom.Name = "Lbl_Kolom"
        Me.Lbl_Kolom.Size = New System.Drawing.Size(50, 20)
        Me.Lbl_Kolom.TabIndex = 236
        Me.Lbl_Kolom.Text = "Kolom"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(289, 401)
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
        Me.Btn_Cari.Location = New System.Drawing.Point(535, 397)
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
        Me.ComboBox1.Location = New System.Drawing.Point(88, 400)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(195, 25)
        Me.ComboBox1.TabIndex = 339
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(21, 330)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(942, 12)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(21, 378)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(938, 19)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'Lbl_Ukuran
        '
        Me.Lbl_Ukuran.AutoSize = True
        Me.Lbl_Ukuran.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Ukuran.Location = New System.Drawing.Point(27, 156)
        Me.Lbl_Ukuran.Name = "Lbl_Ukuran"
        Me.Lbl_Ukuran.Size = New System.Drawing.Size(59, 20)
        Me.Lbl_Ukuran.TabIndex = 340
        Me.Lbl_Ukuran.Text = "Ukuran"
        '
        'Txt_Berat
        '
        Me.Txt_Berat.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Berat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Berat.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Berat.Location = New System.Drawing.Point(211, 271)
        Me.Txt_Berat.MaxLength = 50
        Me.Txt_Berat.Name = "Txt_Berat"
        Me.Txt_Berat.Size = New System.Drawing.Size(350, 22)
        Me.Txt_Berat.TabIndex = 377
        '
        'Lbl_Berat
        '
        Me.Lbl_Berat.AutoSize = True
        Me.Lbl_Berat.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Berat.Location = New System.Drawing.Point(27, 272)
        Me.Lbl_Berat.Name = "Lbl_Berat"
        Me.Lbl_Berat.Size = New System.Drawing.Size(46, 20)
        Me.Lbl_Berat.TabIndex = 376
        Me.Lbl_Berat.Text = "Berat"
        '
        'Txt_Panjang
        '
        Me.Txt_Panjang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Panjang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Panjang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Panjang.Location = New System.Drawing.Point(276, 155)
        Me.Txt_Panjang.MaxLength = 100
        Me.Txt_Panjang.Name = "Txt_Panjang"
        Me.Txt_Panjang.Size = New System.Drawing.Size(47, 22)
        Me.Txt_Panjang.TabIndex = 389
        '
        'Lbl_Panjang
        '
        Me.Lbl_Panjang.AutoSize = True
        Me.Lbl_Panjang.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Panjang.Location = New System.Drawing.Point(207, 156)
        Me.Lbl_Panjang.Name = "Lbl_Panjang"
        Me.Lbl_Panjang.Size = New System.Drawing.Size(63, 20)
        Me.Lbl_Panjang.TabIndex = 390
        Me.Lbl_Panjang.Text = "Panjang"
        '
        'Lbl_Lebar
        '
        Me.Lbl_Lebar.AutoSize = True
        Me.Lbl_Lebar.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Lebar.Location = New System.Drawing.Point(329, 156)
        Me.Lbl_Lebar.Name = "Lbl_Lebar"
        Me.Lbl_Lebar.Size = New System.Drawing.Size(48, 20)
        Me.Lbl_Lebar.TabIndex = 392
        Me.Lbl_Lebar.Text = "Lebar"
        '
        'Txt_Lebar
        '
        Me.Txt_Lebar.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Lebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Lebar.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Lebar.Location = New System.Drawing.Point(394, 155)
        Me.Txt_Lebar.MaxLength = 100
        Me.Txt_Lebar.Name = "Txt_Lebar"
        Me.Txt_Lebar.Size = New System.Drawing.Size(47, 22)
        Me.Txt_Lebar.TabIndex = 391
        '
        'Lbl_Tinggi
        '
        Me.Lbl_Tinggi.AutoSize = True
        Me.Lbl_Tinggi.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Tinggi.Location = New System.Drawing.Point(447, 156)
        Me.Lbl_Tinggi.Name = "Lbl_Tinggi"
        Me.Lbl_Tinggi.Size = New System.Drawing.Size(48, 20)
        Me.Lbl_Tinggi.TabIndex = 394
        Me.Lbl_Tinggi.Text = "Tinggi"
        '
        'Txt_Tinggi
        '
        Me.Txt_Tinggi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Tinggi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Tinggi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Tinggi.Location = New System.Drawing.Point(514, 155)
        Me.Txt_Tinggi.MaxLength = 100
        Me.Txt_Tinggi.Name = "Txt_Tinggi"
        Me.Txt_Tinggi.Size = New System.Drawing.Size(47, 22)
        Me.Txt_Tinggi.TabIndex = 393
        '
        'Txt_Volume
        '
        Me.Txt_Volume.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Volume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Volume.Enabled = False
        Me.Txt_Volume.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Volume.Location = New System.Drawing.Point(211, 183)
        Me.Txt_Volume.MaxLength = 50
        Me.Txt_Volume.Name = "Txt_Volume"
        Me.Txt_Volume.ReadOnly = True
        Me.Txt_Volume.Size = New System.Drawing.Size(350, 22)
        Me.Txt_Volume.TabIndex = 395
        '
        'Lbl_Volume
        '
        Me.Lbl_Volume.AutoSize = True
        Me.Lbl_Volume.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Volume.Location = New System.Drawing.Point(27, 184)
        Me.Lbl_Volume.Name = "Lbl_Volume"
        Me.Lbl_Volume.Size = New System.Drawing.Size(59, 20)
        Me.Lbl_Volume.TabIndex = 396
        Me.Lbl_Volume.Text = "Volume"
        '
        'Cmb_SatuanVolume
        '
        Me.Cmb_SatuanVolume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_SatuanVolume.Enabled = False
        Me.Cmb_SatuanVolume.FormattingEnabled = True
        Me.Cmb_SatuanVolume.Location = New System.Drawing.Point(211, 241)
        Me.Cmb_SatuanVolume.Name = "Cmb_SatuanVolume"
        Me.Cmb_SatuanVolume.Size = New System.Drawing.Size(350, 24)
        Me.Cmb_SatuanVolume.TabIndex = 397
        '
        'Lbl_SatuanVolume
        '
        Me.Lbl_SatuanVolume.AutoSize = True
        Me.Lbl_SatuanVolume.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_SatuanVolume.Location = New System.Drawing.Point(27, 242)
        Me.Lbl_SatuanVolume.Name = "Lbl_SatuanVolume"
        Me.Lbl_SatuanVolume.Size = New System.Drawing.Size(113, 20)
        Me.Lbl_SatuanVolume.TabIndex = 398
        Me.Lbl_SatuanVolume.Text = "Satuan Volume"
        '
        'Cmb_SatuanBerat
        '
        Me.Cmb_SatuanBerat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_SatuanBerat.FormattingEnabled = True
        Me.Cmb_SatuanBerat.Location = New System.Drawing.Point(211, 299)
        Me.Cmb_SatuanBerat.Name = "Cmb_SatuanBerat"
        Me.Cmb_SatuanBerat.Size = New System.Drawing.Size(350, 24)
        Me.Cmb_SatuanBerat.TabIndex = 399
        '
        'Lbl_SatuanBerat
        '
        Me.Lbl_SatuanBerat.AutoSize = True
        Me.Lbl_SatuanBerat.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_SatuanBerat.Location = New System.Drawing.Point(27, 300)
        Me.Lbl_SatuanBerat.Name = "Lbl_SatuanBerat"
        Me.Lbl_SatuanBerat.Size = New System.Drawing.Size(100, 20)
        Me.Lbl_SatuanBerat.TabIndex = 400
        Me.Lbl_SatuanBerat.Text = "Satuan Berat"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(1, 695)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(938, 19)
        Me.Panel8.TabIndex = 401
        Me.Panel8.Visible = False
        '
        'Cmb_CaraKirim
        '
        Me.Cmb_CaraKirim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_CaraKirim.FormattingEnabled = True
        Me.Cmb_CaraKirim.Location = New System.Drawing.Point(211, 69)
        Me.Cmb_CaraKirim.Name = "Cmb_CaraKirim"
        Me.Cmb_CaraKirim.Size = New System.Drawing.Size(350, 24)
        Me.Cmb_CaraKirim.TabIndex = 402
        '
        'Txt_MediaKirim
        '
        Me.Txt_MediaKirim.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_MediaKirim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_MediaKirim.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_MediaKirim.Location = New System.Drawing.Point(211, 127)
        Me.Txt_MediaKirim.MaxLength = 50
        Me.Txt_MediaKirim.Name = "Txt_MediaKirim"
        Me.Txt_MediaKirim.Size = New System.Drawing.Size(350, 22)
        Me.Txt_MediaKirim.TabIndex = 403
        '
        'Txt_KdMediaKirim
        '
        Me.Txt_KdMediaKirim.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdMediaKirim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdMediaKirim.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_KdMediaKirim.Location = New System.Drawing.Point(211, 99)
        Me.Txt_KdMediaKirim.MaxLength = 50
        Me.Txt_KdMediaKirim.Name = "Txt_KdMediaKirim"
        Me.Txt_KdMediaKirim.Size = New System.Drawing.Size(350, 22)
        Me.Txt_KdMediaKirim.TabIndex = 404
        '
        'Lbl_KdMediaKirim
        '
        Me.Lbl_KdMediaKirim.AutoSize = True
        Me.Lbl_KdMediaKirim.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_KdMediaKirim.Location = New System.Drawing.Point(27, 100)
        Me.Lbl_KdMediaKirim.Name = "Lbl_KdMediaKirim"
        Me.Lbl_KdMediaKirim.Size = New System.Drawing.Size(129, 20)
        Me.Lbl_KdMediaKirim.TabIndex = 405
        Me.Lbl_KdMediaKirim.Text = "Kode Media Kirim"
        '
        'Lbl_plt
        '
        Me.Lbl_plt.AutoSize = True
        Me.Lbl_plt.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_plt.Location = New System.Drawing.Point(80, 156)
        Me.Lbl_plt.Name = "Lbl_plt"
        Me.Lbl_plt.Size = New System.Drawing.Size(80, 20)
        Me.Lbl_plt.TabIndex = 406
        Me.Lbl_plt.Text = "(P X L X T)"
        '
        'Lbl_SatuanPanjang
        '
        Me.Lbl_SatuanPanjang.AutoSize = True
        Me.Lbl_SatuanPanjang.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_SatuanPanjang.Location = New System.Drawing.Point(27, 212)
        Me.Lbl_SatuanPanjang.Name = "Lbl_SatuanPanjang"
        Me.Lbl_SatuanPanjang.Size = New System.Drawing.Size(117, 20)
        Me.Lbl_SatuanPanjang.TabIndex = 407
        Me.Lbl_SatuanPanjang.Text = "Satuan Panjang"
        '
        'Cmb_SatuanPanjang
        '
        Me.Cmb_SatuanPanjang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_SatuanPanjang.FormattingEnabled = True
        Me.Cmb_SatuanPanjang.Location = New System.Drawing.Point(211, 211)
        Me.Cmb_SatuanPanjang.Name = "Cmb_SatuanPanjang"
        Me.Cmb_SatuanPanjang.Size = New System.Drawing.Size(350, 24)
        Me.Cmb_SatuanPanjang.TabIndex = 408
        '
        'Cmb_ValueCaraKirim
        '
        Me.Cmb_ValueCaraKirim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_ValueCaraKirim.FormattingEnabled = True
        Me.Cmb_ValueCaraKirim.Location = New System.Drawing.Point(343, 400)
        Me.Cmb_ValueCaraKirim.Name = "Cmb_ValueCaraKirim"
        Me.Cmb_ValueCaraKirim.Size = New System.Drawing.Size(189, 24)
        Me.Cmb_ValueCaraKirim.TabIndex = 409
        Me.Cmb_ValueCaraKirim.Visible = False
        '
        'Master_Media_Kirim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(942, 716)
        Me.Controls.Add(Me.Cmb_SatuanPanjang)
        Me.Controls.Add(Me.Lbl_SatuanPanjang)
        Me.Controls.Add(Me.Lbl_plt)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lbl_KdMediaKirim)
        Me.Controls.Add(Me.Txt_KdMediaKirim)
        Me.Controls.Add(Me.Txt_MediaKirim)
        Me.Controls.Add(Me.Cmb_CaraKirim)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Lbl_SatuanBerat)
        Me.Controls.Add(Me.Cmb_SatuanBerat)
        Me.Controls.Add(Me.Lbl_SatuanVolume)
        Me.Controls.Add(Me.Cmb_SatuanVolume)
        Me.Controls.Add(Me.Lbl_Volume)
        Me.Controls.Add(Me.Txt_Volume)
        Me.Controls.Add(Me.Lbl_Tinggi)
        Me.Controls.Add(Me.Txt_Tinggi)
        Me.Controls.Add(Me.Lbl_Lebar)
        Me.Controls.Add(Me.Txt_Lebar)
        Me.Controls.Add(Me.Lbl_Panjang)
        Me.Controls.Add(Me.Txt_Panjang)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Lbl_Kolom)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.ListView_MediaKirim)
        Me.Controls.Add(Me.Txt_Berat)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Lbl_Berat)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.Lbl_Ukuran)
        Me.Controls.Add(Me.Lbl_MediaKirim)
        Me.Controls.Add(Me.Lbl_CaraKirim)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Cmb_ValueCaraKirim)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Master_Media_Kirim"
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
    Friend WithEvents Lbl_CaraKirim As Label
    Friend WithEvents Lbl_MediaKirim As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents ListView_MediaKirim As ListView
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Lbl_Kolom As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lbl_Ukuran As Label
    Friend WithEvents Txt_Berat As TextBox
    Friend WithEvents Lbl_Berat As Label
    Friend WithEvents Txt_Panjang As TextBox
    Friend WithEvents Lbl_Panjang As Label
    Friend WithEvents Lbl_Lebar As Label
    Friend WithEvents Txt_Lebar As TextBox
    Friend WithEvents Lbl_Tinggi As Label
    Friend WithEvents Txt_Tinggi As TextBox
    Friend WithEvents Txt_Volume As TextBox
    Friend WithEvents Lbl_Volume As Label
    Friend WithEvents Cmb_SatuanVolume As ComboBox
    Friend WithEvents Lbl_SatuanVolume As Label
    Friend WithEvents Cmb_SatuanBerat As ComboBox
    Friend WithEvents Lbl_SatuanBerat As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Cmb_CaraKirim As ComboBox
    Friend WithEvents Txt_MediaKirim As TextBox
    Friend WithEvents Txt_KdMediaKirim As TextBox
    Friend WithEvents Lbl_KdMediaKirim As Label
    Friend WithEvents Lbl_plt As Label
    Friend WithEvents Lbl_SatuanPanjang As Label
    Friend WithEvents Cmb_SatuanPanjang As ComboBox
    Friend WithEvents Cmb_ValueCaraKirim As ComboBox
End Class
