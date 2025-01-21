<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Schedule_SD_Tambah
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
        Me.LblSchedule_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.BtnPilihBarang_Refresh = New System.Windows.Forms.Button()
        Me.BtnPilihBarang_Simpan = New System.Windows.Forms.Button()
        Me.TxtSchedule_Deskripsi = New System.Windows.Forms.TextBox()
        Me.LblSchedule_Deskripsi = New System.Windows.Forms.Label()
        Me.LblSchedule_TanggalStart = New System.Windows.Forms.Label()
        Me.LblSchedule_Kategori = New System.Windows.Forms.Label()
        Me.CmbSchedule_Kategori = New System.Windows.Forms.ComboBox()
        Me.DtpSchedule_DateStart = New System.Windows.Forms.DateTimePicker()
        Me.DtpSchedule_DateEnd = New System.Windows.Forms.DateTimePicker()
        Me.LblSchedule_TanggalEnd = New System.Windows.Forms.Label()
        Me.DtpSchedule_TimeStart = New System.Windows.Forms.DateTimePicker()
        Me.DtpSchedule_TimeEnd = New System.Windows.Forms.DateTimePicker()
        Me.TxtSchedule_JenisProduk = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtSchedule_NoFaktur = New System.Windows.Forms.TextBox()
        Me.TxtSchedule_IdJenisProduk = New System.Windows.Forms.TextBox()
        Me.TxtKodeBarang = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtNamaBarang = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtJumlah = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtSatuan = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.LblSchedule_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(579, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(579, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'LblSchedule_Judul
        '
        Me.LblSchedule_Judul.AutoSize = True
        Me.LblSchedule_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSchedule_Judul.Location = New System.Drawing.Point(15, 11)
        Me.LblSchedule_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblSchedule_Judul.Name = "LblSchedule_Judul"
        Me.LblSchedule_Judul.Size = New System.Drawing.Size(140, 30)
        Me.LblSchedule_Judul.TabIndex = 0
        Me.LblSchedule_Judul.Text = "Pilih Jadwal"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 377)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(559, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 401)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1, 461)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1028, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'BtnPilihBarang_Refresh
        '
        Me.BtnPilihBarang_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnPilihBarang_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnPilihBarang_Refresh.ForeColor = System.Drawing.Color.White
        Me.BtnPilihBarang_Refresh.Location = New System.Drawing.Point(118, 423)
        Me.BtnPilihBarang_Refresh.Name = "BtnPilihBarang_Refresh"
        Me.BtnPilihBarang_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.BtnPilihBarang_Refresh.TabIndex = 355
        Me.BtnPilihBarang_Refresh.Text = "&Refresh"
        Me.BtnPilihBarang_Refresh.UseVisualStyleBackColor = False
        '
        'BtnPilihBarang_Simpan
        '
        Me.BtnPilihBarang_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnPilihBarang_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnPilihBarang_Simpan.ForeColor = System.Drawing.Color.White
        Me.BtnPilihBarang_Simpan.Location = New System.Drawing.Point(28, 423)
        Me.BtnPilihBarang_Simpan.Name = "BtnPilihBarang_Simpan"
        Me.BtnPilihBarang_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.BtnPilihBarang_Simpan.TabIndex = 353
        Me.BtnPilihBarang_Simpan.Text = "&Simpan"
        Me.BtnPilihBarang_Simpan.UseVisualStyleBackColor = False
        '
        'TxtSchedule_Deskripsi
        '
        Me.TxtSchedule_Deskripsi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSchedule_Deskripsi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSchedule_Deskripsi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtSchedule_Deskripsi.Location = New System.Drawing.Point(162, 239)
        Me.TxtSchedule_Deskripsi.MaxLength = 100
        Me.TxtSchedule_Deskripsi.Multiline = True
        Me.TxtSchedule_Deskripsi.Name = "TxtSchedule_Deskripsi"
        Me.TxtSchedule_Deskripsi.Size = New System.Drawing.Size(344, 110)
        Me.TxtSchedule_Deskripsi.TabIndex = 352
        '
        'LblSchedule_Deskripsi
        '
        Me.LblSchedule_Deskripsi.AutoSize = True
        Me.LblSchedule_Deskripsi.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblSchedule_Deskripsi.Location = New System.Drawing.Point(24, 239)
        Me.LblSchedule_Deskripsi.Name = "LblSchedule_Deskripsi"
        Me.LblSchedule_Deskripsi.Size = New System.Drawing.Size(72, 20)
        Me.LblSchedule_Deskripsi.TabIndex = 351
        Me.LblSchedule_Deskripsi.Text = "Deskripsi"
        '
        'LblSchedule_TanggalStart
        '
        Me.LblSchedule_TanggalStart.AutoSize = True
        Me.LblSchedule_TanggalStart.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblSchedule_TanggalStart.Location = New System.Drawing.Point(23, 357)
        Me.LblSchedule_TanggalStart.Name = "LblSchedule_TanggalStart"
        Me.LblSchedule_TanggalStart.Size = New System.Drawing.Size(101, 20)
        Me.LblSchedule_TanggalStart.TabIndex = 356
        Me.LblSchedule_TanggalStart.Text = "Tanggal Mulai"
        '
        'LblSchedule_Kategori
        '
        Me.LblSchedule_Kategori.AutoSize = True
        Me.LblSchedule_Kategori.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblSchedule_Kategori.Location = New System.Drawing.Point(23, 209)
        Me.LblSchedule_Kategori.Name = "LblSchedule_Kategori"
        Me.LblSchedule_Kategori.Size = New System.Drawing.Size(61, 20)
        Me.LblSchedule_Kategori.TabIndex = 359
        Me.LblSchedule_Kategori.Text = "Routing"
        '
        'CmbSchedule_Kategori
        '
        Me.CmbSchedule_Kategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSchedule_Kategori.Enabled = False
        Me.CmbSchedule_Kategori.FormattingEnabled = True
        Me.CmbSchedule_Kategori.Location = New System.Drawing.Point(162, 209)
        Me.CmbSchedule_Kategori.Name = "CmbSchedule_Kategori"
        Me.CmbSchedule_Kategori.Size = New System.Drawing.Size(344, 24)
        Me.CmbSchedule_Kategori.TabIndex = 360
        '
        'DtpSchedule_DateStart
        '
        Me.DtpSchedule_DateStart.CustomFormat = "ddddd MM yyyy"
        Me.DtpSchedule_DateStart.Location = New System.Drawing.Point(162, 357)
        Me.DtpSchedule_DateStart.Name = "DtpSchedule_DateStart"
        Me.DtpSchedule_DateStart.Size = New System.Drawing.Size(197, 20)
        Me.DtpSchedule_DateStart.TabIndex = 361
        '
        'DtpSchedule_DateEnd
        '
        Me.DtpSchedule_DateEnd.Location = New System.Drawing.Point(162, 385)
        Me.DtpSchedule_DateEnd.Name = "DtpSchedule_DateEnd"
        Me.DtpSchedule_DateEnd.Size = New System.Drawing.Size(197, 20)
        Me.DtpSchedule_DateEnd.TabIndex = 363
        '
        'LblSchedule_TanggalEnd
        '
        Me.LblSchedule_TanggalEnd.AutoSize = True
        Me.LblSchedule_TanggalEnd.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblSchedule_TanggalEnd.Location = New System.Drawing.Point(23, 385)
        Me.LblSchedule_TanggalEnd.Name = "LblSchedule_TanggalEnd"
        Me.LblSchedule_TanggalEnd.Size = New System.Drawing.Size(112, 20)
        Me.LblSchedule_TanggalEnd.TabIndex = 362
        Me.LblSchedule_TanggalEnd.Text = "Tanggal Selesai"
        '
        'DtpSchedule_TimeStart
        '
        Me.DtpSchedule_TimeStart.CustomFormat = "HH:mm"
        Me.DtpSchedule_TimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DtpSchedule_TimeStart.Location = New System.Drawing.Point(365, 357)
        Me.DtpSchedule_TimeStart.Name = "DtpSchedule_TimeStart"
        Me.DtpSchedule_TimeStart.ShowUpDown = True
        Me.DtpSchedule_TimeStart.Size = New System.Drawing.Size(73, 20)
        Me.DtpSchedule_TimeStart.TabIndex = 364
        '
        'DtpSchedule_TimeEnd
        '
        Me.DtpSchedule_TimeEnd.CustomFormat = "HH:mm"
        Me.DtpSchedule_TimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DtpSchedule_TimeEnd.Location = New System.Drawing.Point(365, 385)
        Me.DtpSchedule_TimeEnd.Name = "DtpSchedule_TimeEnd"
        Me.DtpSchedule_TimeEnd.ShowUpDown = True
        Me.DtpSchedule_TimeEnd.Size = New System.Drawing.Size(73, 20)
        Me.DtpSchedule_TimeEnd.TabIndex = 365
        '
        'TxtSchedule_JenisProduk
        '
        Me.TxtSchedule_JenisProduk.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSchedule_JenisProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSchedule_JenisProduk.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtSchedule_JenisProduk.Location = New System.Drawing.Point(162, 94)
        Me.TxtSchedule_JenisProduk.MaxLength = 50
        Me.TxtSchedule_JenisProduk.Name = "TxtSchedule_JenisProduk"
        Me.TxtSchedule_JenisProduk.ReadOnly = True
        Me.TxtSchedule_JenisProduk.Size = New System.Drawing.Size(344, 22)
        Me.TxtSchedule_JenisProduk.TabIndex = 367
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(23, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 20)
        Me.Label1.TabIndex = 366
        Me.Label1.Text = "Jenis Produk"
        '
        'TxtSchedule_NoFaktur
        '
        Me.TxtSchedule_NoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtSchedule_NoFaktur.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSchedule_NoFaktur.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtSchedule_NoFaktur.Location = New System.Drawing.Point(20, 63)
        Me.TxtSchedule_NoFaktur.MaxLength = 30
        Me.TxtSchedule_NoFaktur.Name = "TxtSchedule_NoFaktur"
        Me.TxtSchedule_NoFaktur.ReadOnly = True
        Me.TxtSchedule_NoFaktur.Size = New System.Drawing.Size(227, 22)
        Me.TxtSchedule_NoFaktur.TabIndex = 390
        '
        'TxtSchedule_IdJenisProduk
        '
        Me.TxtSchedule_IdJenisProduk.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSchedule_IdJenisProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSchedule_IdJenisProduk.Enabled = False
        Me.TxtSchedule_IdJenisProduk.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtSchedule_IdJenisProduk.Location = New System.Drawing.Point(386, 423)
        Me.TxtSchedule_IdJenisProduk.MaxLength = 50
        Me.TxtSchedule_IdJenisProduk.Name = "TxtSchedule_IdJenisProduk"
        Me.TxtSchedule_IdJenisProduk.Size = New System.Drawing.Size(166, 22)
        Me.TxtSchedule_IdJenisProduk.TabIndex = 391
        Me.TxtSchedule_IdJenisProduk.Visible = False
        '
        'TxtKodeBarang
        '
        Me.TxtKodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKodeBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKodeBarang.Location = New System.Drawing.Point(162, 122)
        Me.TxtKodeBarang.MaxLength = 50
        Me.TxtKodeBarang.Name = "TxtKodeBarang"
        Me.TxtKodeBarang.ReadOnly = True
        Me.TxtKodeBarang.Size = New System.Drawing.Size(344, 22)
        Me.TxtKodeBarang.TabIndex = 393
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(23, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 20)
        Me.Label2.TabIndex = 392
        Me.Label2.Text = "Kode Barang"
        '
        'TxtNamaBarang
        '
        Me.TxtNamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNamaBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtNamaBarang.Location = New System.Drawing.Point(162, 151)
        Me.TxtNamaBarang.MaxLength = 50
        Me.TxtNamaBarang.Name = "TxtNamaBarang"
        Me.TxtNamaBarang.ReadOnly = True
        Me.TxtNamaBarang.Size = New System.Drawing.Size(344, 22)
        Me.TxtNamaBarang.TabIndex = 395
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(23, 151)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 20)
        Me.Label3.TabIndex = 394
        Me.Label3.Text = "Nama"
        '
        'TxtJumlah
        '
        Me.TxtJumlah.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJumlah.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtJumlah.Location = New System.Drawing.Point(162, 179)
        Me.TxtJumlah.MaxLength = 50
        Me.TxtJumlah.Name = "TxtJumlah"
        Me.TxtJumlah.ReadOnly = True
        Me.TxtJumlah.Size = New System.Drawing.Size(180, 22)
        Me.TxtJumlah.TabIndex = 397
        Me.TxtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(23, 179)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 20)
        Me.Label4.TabIndex = 396
        Me.Label4.Text = "Jumlah"
        '
        'TxtSatuan
        '
        Me.TxtSatuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSatuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSatuan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtSatuan.Location = New System.Drawing.Point(348, 179)
        Me.TxtSatuan.MaxLength = 50
        Me.TxtSatuan.Name = "TxtSatuan"
        Me.TxtSatuan.ReadOnly = True
        Me.TxtSatuan.Size = New System.Drawing.Size(158, 22)
        Me.TxtSatuan.TabIndex = 399
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(19, 408)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(942, 12)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'EMI_Schedule_SD_Tambah
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(579, 477)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.TxtSatuan)
        Me.Controls.Add(Me.TxtJumlah)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtNamaBarang)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtKodeBarang)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtSchedule_IdJenisProduk)
        Me.Controls.Add(Me.TxtSchedule_NoFaktur)
        Me.Controls.Add(Me.TxtSchedule_JenisProduk)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DtpSchedule_TimeEnd)
        Me.Controls.Add(Me.DtpSchedule_TimeStart)
        Me.Controls.Add(Me.DtpSchedule_DateEnd)
        Me.Controls.Add(Me.LblSchedule_TanggalEnd)
        Me.Controls.Add(Me.DtpSchedule_DateStart)
        Me.Controls.Add(Me.CmbSchedule_Kategori)
        Me.Controls.Add(Me.LblSchedule_Kategori)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.LblSchedule_TanggalStart)
        Me.Controls.Add(Me.BtnPilihBarang_Refresh)
        Me.Controls.Add(Me.BtnPilihBarang_Simpan)
        Me.Controls.Add(Me.TxtSchedule_Deskripsi)
        Me.Controls.Add(Me.LblSchedule_Deskripsi)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Schedule_SD_Tambah"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents LblSchedule_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents BtnPilihBarang_Refresh As Button
    Friend WithEvents BtnPilihBarang_Simpan As Button
    Friend WithEvents TxtSchedule_Deskripsi As TextBox
    Friend WithEvents LblSchedule_Deskripsi As Label
    Friend WithEvents LblSchedule_TanggalStart As Label
    Friend WithEvents LblSchedule_Kategori As Label
    Friend WithEvents CmbSchedule_Kategori As ComboBox
    Friend WithEvents DtpSchedule_DateStart As DateTimePicker
    Friend WithEvents DtpSchedule_DateEnd As DateTimePicker
    Friend WithEvents LblSchedule_TanggalEnd As Label
    Friend WithEvents DtpSchedule_TimeStart As DateTimePicker
    Friend WithEvents DtpSchedule_TimeEnd As DateTimePicker
    Friend WithEvents TxtSchedule_JenisProduk As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtSchedule_NoFaktur As TextBox
    Friend WithEvents TxtSchedule_IdJenisProduk As TextBox
    Friend WithEvents TxtKodeBarang As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtNamaBarang As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtJumlah As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtSatuan As TextBox
    Friend WithEvents Panel6 As Panel
End Class
