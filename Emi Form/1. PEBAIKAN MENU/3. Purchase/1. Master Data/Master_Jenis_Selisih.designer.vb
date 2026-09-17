<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Jenis_Selisih
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
		Me.Txt_Kd = New System.Windows.Forms.TextBox()
		Me.Lbl_Kd = New System.Windows.Forms.Label()
		Me.Lbl_Keterangan = New System.Windows.Forms.Label()
		Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Btn_Refresh = New System.Windows.Forms.Button()
		Me.Btn_Hapus = New System.Windows.Forms.Button()
		Me.Btn_Simpan = New System.Windows.Forms.Button()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Cmb_Kolom = New System.Windows.Forms.ComboBox()
		Me.Btn_Cari = New System.Windows.Forms.Button()
		Me.Lbl_Value = New System.Windows.Forms.Label()
		Me.Lbl_Kolom = New System.Windows.Forms.Label()
		Me.Txt_Value = New System.Windows.Forms.TextBox()
		Me.Lv_MasterBiaya = New System.Windows.Forms.ListView()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.TxtKodeAkunBhn = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.TxtNamaAkunBahan = New System.Windows.Forms.TextBox()
		Me.TxtNamaAkunPerjalanan = New System.Windows.Forms.TextBox()
		Me.TxtKodeAkunPerjalanan = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.LvAkunBahan = New System.Windows.Forms.ListView()
		Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
		Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
		Me.LvAkunPerjalanan = New System.Windows.Forms.ListView()
		Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
		Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
		Me.idJenisSelisih = New System.Windows.Forms.Label()
		Me.ChkMasukHutang = New System.Windows.Forms.CheckBox()
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
		Me.Panel1.Size = New System.Drawing.Size(677, 51)
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
		Me.PanelGradient1.Size = New System.Drawing.Size(677, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Lbl_Judul
		'
		Me.Lbl_Judul.AutoSize = True
		Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
		Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Lbl_Judul.Name = "Lbl_Judul"
		Me.Lbl_Judul.Size = New System.Drawing.Size(292, 30)
		Me.Lbl_Judul.TabIndex = 0
		Me.Lbl_Judul.Text = "Master Data - Jenis Selisih"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(0, 51)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(942, 12)
		Me.Panel2.TabIndex = 35
		Me.Panel2.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(1, 63)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 498)
		Me.Panel3.TabIndex = 36
		Me.Panel3.Visible = False
		'
		'Txt_Kd
		'
		Me.Txt_Kd.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Kd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Kd.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.Txt_Kd.Location = New System.Drawing.Point(151, 83)
		Me.Txt_Kd.MaxLength = 50
		Me.Txt_Kd.Name = "Txt_Kd"
		Me.Txt_Kd.Size = New System.Drawing.Size(228, 22)
		Me.Txt_Kd.TabIndex = 228
		'
		'Lbl_Kd
		'
		Me.Lbl_Kd.AutoSize = True
		Me.Lbl_Kd.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Lbl_Kd.Location = New System.Drawing.Point(23, 84)
		Me.Lbl_Kd.Name = "Lbl_Kd"
		Me.Lbl_Kd.Size = New System.Drawing.Size(42, 20)
		Me.Lbl_Kd.TabIndex = 229
		Me.Lbl_Kd.Text = "Kode"
		'
		'Lbl_Keterangan
		'
		Me.Lbl_Keterangan.AutoSize = True
		Me.Lbl_Keterangan.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Lbl_Keterangan.Location = New System.Drawing.Point(23, 116)
		Me.Lbl_Keterangan.Name = "Lbl_Keterangan"
		Me.Lbl_Keterangan.Size = New System.Drawing.Size(86, 20)
		Me.Lbl_Keterangan.TabIndex = 230
		Me.Lbl_Keterangan.Text = "Keterangan"
		'
		'Txt_Keterangan
		'
		Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.Txt_Keterangan.Location = New System.Drawing.Point(151, 115)
		Me.Txt_Keterangan.MaxLength = 50
		Me.Txt_Keterangan.Name = "Txt_Keterangan"
		Me.Txt_Keterangan.Size = New System.Drawing.Size(228, 22)
		Me.Txt_Keterangan.TabIndex = 231
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(5, 207)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(924, 12)
		Me.Panel6.TabIndex = 232
		Me.Panel6.Visible = False
		'
		'Btn_Refresh
		'
		Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Refresh.Location = New System.Drawing.Point(331, 226)
		Me.Btn_Refresh.Name = "Btn_Refresh"
		Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
		Me.Btn_Refresh.TabIndex = 235
		Me.Btn_Refresh.Text = "&Refresh"
		Me.Btn_Refresh.UseVisualStyleBackColor = False
		'
		'Btn_Hapus
		'
		Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
		Me.Btn_Hapus.Location = New System.Drawing.Point(241, 226)
		Me.Btn_Hapus.Name = "Btn_Hapus"
		Me.Btn_Hapus.Size = New System.Drawing.Size(84, 36)
		Me.Btn_Hapus.TabIndex = 234
		Me.Btn_Hapus.Text = "&Hapus"
		Me.Btn_Hapus.UseVisualStyleBackColor = False
		'
		'Btn_Simpan
		'
		Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan.Location = New System.Drawing.Point(151, 226)
		Me.Btn_Simpan.Name = "Btn_Simpan"
		Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
		Me.Btn_Simpan.TabIndex = 233
		Me.Btn_Simpan.Text = "&Simpan"
		Me.Btn_Simpan.UseVisualStyleBackColor = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(4, 259)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(938, 19)
		Me.Panel7.TabIndex = 236
		Me.Panel7.Visible = False
		'
		'Cmb_Kolom
		'
		Me.Cmb_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Kolom.DropDownWidth = 150
		Me.Cmb_Kolom.Font = New System.Drawing.Font("Work Sans", 8.9!)
		Me.Cmb_Kolom.FormattingEnabled = True
		Me.Cmb_Kolom.Location = New System.Drawing.Point(82, 284)
		Me.Cmb_Kolom.Margin = New System.Windows.Forms.Padding(2)
		Me.Cmb_Kolom.Name = "Cmb_Kolom"
		Me.Cmb_Kolom.Size = New System.Drawing.Size(195, 25)
		Me.Cmb_Kolom.TabIndex = 338
		'
		'Btn_Cari
		'
		Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari.Location = New System.Drawing.Point(529, 281)
		Me.Btn_Cari.Name = "Btn_Cari"
		Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
		Me.Btn_Cari.TabIndex = 340
		Me.Btn_Cari.Text = "Cari"
		Me.Btn_Cari.UseVisualStyleBackColor = False
		'
		'Lbl_Value
		'
		Me.Lbl_Value.AutoSize = True
		Me.Lbl_Value.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Lbl_Value.Location = New System.Drawing.Point(282, 286)
		Me.Lbl_Value.Name = "Lbl_Value"
		Me.Lbl_Value.Size = New System.Drawing.Size(46, 20)
		Me.Lbl_Value.TabIndex = 342
		Me.Lbl_Value.Text = "Value"
		'
		'Lbl_Kolom
		'
		Me.Lbl_Kolom.AutoSize = True
		Me.Lbl_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Lbl_Kolom.Location = New System.Drawing.Point(23, 286)
		Me.Lbl_Kolom.Name = "Lbl_Kolom"
		Me.Lbl_Kolom.Size = New System.Drawing.Size(50, 20)
		Me.Lbl_Kolom.TabIndex = 341
		Me.Lbl_Kolom.Text = "Kolom"
		'
		'Txt_Value
		'
		Me.Txt_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.Txt_Value.Location = New System.Drawing.Point(334, 285)
		Me.Txt_Value.MaxLength = 50
		Me.Txt_Value.Name = "Txt_Value"
		Me.Txt_Value.Size = New System.Drawing.Size(189, 22)
		Me.Txt_Value.TabIndex = 339
		'
		'Lv_MasterBiaya
		'
		Me.Lv_MasterBiaya.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Lv_MasterBiaya.FullRowSelect = True
		Me.Lv_MasterBiaya.GridLines = True
		Me.Lv_MasterBiaya.HideSelection = False
		Me.Lv_MasterBiaya.Location = New System.Drawing.Point(21, 314)
		Me.Lv_MasterBiaya.Name = "Lv_MasterBiaya"
		Me.Lv_MasterBiaya.Size = New System.Drawing.Size(636, 259)
		Me.Lv_MasterBiaya.TabIndex = 343
		Me.Lv_MasterBiaya.UseCompatibleStateImageBehavior = False
		Me.Lv_MasterBiaya.View = System.Windows.Forms.View.Details
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(659, 56)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(19, 515)
		Me.Panel5.TabIndex = 344
		Me.Panel5.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(-247, 576)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(1436, 15)
		Me.Panel4.TabIndex = 345
		Me.Panel4.Visible = False
		'
		'TxtKodeAkunBhn
		'
		Me.TxtKodeAkunBhn.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtKodeAkunBhn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtKodeAkunBhn.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.TxtKodeAkunBhn.Location = New System.Drawing.Point(151, 146)
		Me.TxtKodeAkunBhn.MaxLength = 50
		Me.TxtKodeAkunBhn.Name = "TxtKodeAkunBhn"
		Me.TxtKodeAkunBhn.Size = New System.Drawing.Size(117, 22)
		Me.TxtKodeAkunBhn.TabIndex = 347
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Label1.Location = New System.Drawing.Point(23, 147)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(92, 20)
		Me.Label1.TabIndex = 346
		Me.Label1.Text = "Akun Bahan"
		'
		'TxtNamaAkunBahan
		'
		Me.TxtNamaAkunBahan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtNamaAkunBahan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtNamaAkunBahan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.TxtNamaAkunBahan.Location = New System.Drawing.Point(274, 146)
		Me.TxtNamaAkunBahan.MaxLength = 50
		Me.TxtNamaAkunBahan.Name = "TxtNamaAkunBahan"
		Me.TxtNamaAkunBahan.Size = New System.Drawing.Size(189, 22)
		Me.TxtNamaAkunBahan.TabIndex = 348
		'
		'TxtNamaAkunPerjalanan
		'
		Me.TxtNamaAkunPerjalanan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtNamaAkunPerjalanan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtNamaAkunPerjalanan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.TxtNamaAkunPerjalanan.Location = New System.Drawing.Point(274, 177)
		Me.TxtNamaAkunPerjalanan.MaxLength = 50
		Me.TxtNamaAkunPerjalanan.Name = "TxtNamaAkunPerjalanan"
		Me.TxtNamaAkunPerjalanan.Size = New System.Drawing.Size(189, 22)
		Me.TxtNamaAkunPerjalanan.TabIndex = 351
		'
		'TxtKodeAkunPerjalanan
		'
		Me.TxtKodeAkunPerjalanan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtKodeAkunPerjalanan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtKodeAkunPerjalanan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.TxtKodeAkunPerjalanan.Location = New System.Drawing.Point(151, 177)
		Me.TxtKodeAkunPerjalanan.MaxLength = 50
		Me.TxtKodeAkunPerjalanan.Name = "TxtKodeAkunPerjalanan"
		Me.TxtKodeAkunPerjalanan.Size = New System.Drawing.Size(117, 22)
		Me.TxtKodeAkunPerjalanan.TabIndex = 350
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Label2.Location = New System.Drawing.Point(23, 178)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(122, 20)
		Me.Label2.TabIndex = 349
		Me.Label2.Text = "Akun Perjalanan"
		'
		'LvAkunBahan
		'
		Me.LvAkunBahan.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
		Me.LvAkunBahan.FullRowSelect = True
		Me.LvAkunBahan.GridLines = True
		Me.LvAkunBahan.HideSelection = False
		Me.LvAkunBahan.Location = New System.Drawing.Point(685, 331)
		Me.LvAkunBahan.Name = "LvAkunBahan"
		Me.LvAkunBahan.Size = New System.Drawing.Size(360, 194)
		Me.LvAkunBahan.TabIndex = 360
		Me.LvAkunBahan.UseCompatibleStateImageBehavior = False
		Me.LvAkunBahan.View = System.Windows.Forms.View.Details
		Me.LvAkunBahan.Visible = False
		'
		'ColumnHeader1
		'
		Me.ColumnHeader1.Text = "Kode Akun"
		Me.ColumnHeader1.Width = 150
		'
		'ColumnHeader2
		'
		Me.ColumnHeader2.Text = "Akun"
		Me.ColumnHeader2.Width = 220
		'
		'LvAkunPerjalanan
		'
		Me.LvAkunPerjalanan.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4})
		Me.LvAkunPerjalanan.FullRowSelect = True
		Me.LvAkunPerjalanan.GridLines = True
		Me.LvAkunPerjalanan.HideSelection = False
		Me.LvAkunPerjalanan.Location = New System.Drawing.Point(685, 70)
		Me.LvAkunPerjalanan.Name = "LvAkunPerjalanan"
		Me.LvAkunPerjalanan.Size = New System.Drawing.Size(387, 194)
		Me.LvAkunPerjalanan.TabIndex = 361
		Me.LvAkunPerjalanan.UseCompatibleStateImageBehavior = False
		Me.LvAkunPerjalanan.View = System.Windows.Forms.View.Details
		Me.LvAkunPerjalanan.Visible = False
		'
		'ColumnHeader3
		'
		Me.ColumnHeader3.Text = "Kode Akun"
		Me.ColumnHeader3.Width = 150
		'
		'ColumnHeader4
		'
		Me.ColumnHeader4.Text = "Akun"
		Me.ColumnHeader4.Width = 220
		'
		'idJenisSelisih
		'
		Me.idJenisSelisih.AutoSize = True
		Me.idJenisSelisih.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.idJenisSelisih.Location = New System.Drawing.Point(434, 96)
		Me.idJenisSelisih.Name = "idJenisSelisih"
		Me.idJenisSelisih.Size = New System.Drawing.Size(113, 20)
		Me.idJenisSelisih.TabIndex = 362
		Me.idJenisSelisih.Text = "id Jenis Selisih"
		Me.idJenisSelisih.Visible = False
		'
		'ChkMasukHutang
		'
		Me.ChkMasukHutang.AutoSize = True
		Me.ChkMasukHutang.Location = New System.Drawing.Point(469, 149)
		Me.ChkMasukHutang.Name = "ChkMasukHutang"
		Me.ChkMasukHutang.Size = New System.Drawing.Size(96, 17)
		Me.ChkMasukHutang.TabIndex = 363
		Me.ChkMasukHutang.Text = "Masuk Hutang"
		Me.ChkMasukHutang.UseVisualStyleBackColor = True
		'
		'Master_Jenis_Selisih
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(677, 593)
		Me.Controls.Add(Me.ChkMasukHutang)
		Me.Controls.Add(Me.idJenisSelisih)
		Me.Controls.Add(Me.LvAkunPerjalanan)
		Me.Controls.Add(Me.LvAkunBahan)
		Me.Controls.Add(Me.TxtNamaAkunPerjalanan)
		Me.Controls.Add(Me.TxtKodeAkunPerjalanan)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.TxtNamaAkunBahan)
		Me.Controls.Add(Me.TxtKodeAkunBhn)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Lv_MasterBiaya)
		Me.Controls.Add(Me.Cmb_Kolom)
		Me.Controls.Add(Me.Btn_Cari)
		Me.Controls.Add(Me.Lbl_Value)
		Me.Controls.Add(Me.Lbl_Kolom)
		Me.Controls.Add(Me.Txt_Value)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Btn_Refresh)
		Me.Controls.Add(Me.Btn_Hapus)
		Me.Controls.Add(Me.Btn_Simpan)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Txt_Keterangan)
		Me.Controls.Add(Me.Lbl_Keterangan)
		Me.Controls.Add(Me.Txt_Kd)
		Me.Controls.Add(Me.Lbl_Kd)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "Master_Jenis_Selisih"
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
    Friend WithEvents Txt_Kd As TextBox
    Friend WithEvents Lbl_Kd As Label
    Friend WithEvents Lbl_Keterangan As Label
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Cmb_Kolom As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Lbl_Value As Label
    Friend WithEvents Lbl_Kolom As Label
    Friend WithEvents Txt_Value As TextBox
    Friend WithEvents Lv_MasterBiaya As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents TxtKodeAkunBhn As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtNamaAkunBahan As TextBox
    Friend WithEvents TxtNamaAkunPerjalanan As TextBox
    Friend WithEvents TxtKodeAkunPerjalanan As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents LvAkunBahan As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents LvAkunPerjalanan As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents idJenisSelisih As Label
    Friend WithEvents ChkMasukHutang As CheckBox
End Class
