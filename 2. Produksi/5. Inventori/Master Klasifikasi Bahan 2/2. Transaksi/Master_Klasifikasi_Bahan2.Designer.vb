<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Klasifikasi_Bahan2
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
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Txt_Klasifikasi2_Prefix = New System.Windows.Forms.TextBox()
		Me.Lbl_Klasifikasi2_Prefix = New System.Windows.Forms.Label()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Cmb_Klasifikasi2_Kolom = New System.Windows.Forms.ComboBox()
		Me.Btn_Klasifikasi2_Cari = New System.Windows.Forms.Button()
		Me.Lbl_Klasifikasi2_Value = New System.Windows.Forms.Label()
		Me.Lbl_Klasifikasi2_Kolom = New System.Windows.Forms.Label()
		Me.Txt_Klasifikasi2_Value = New System.Windows.Forms.TextBox()
		Me.Lv_Klasifikasi_Bahan2 = New System.Windows.Forms.ListView()
		Me.Btn_Klasifikasi2_Refresh = New System.Windows.Forms.Button()
		Me.Btn_Klasifikasi2_Hapus = New System.Windows.Forms.Button()
		Me.Btn_Klasifikasi2_Simpan = New System.Windows.Forms.Button()
		Me.Txt_Klasifikasi2_Keterangan = New System.Windows.Forms.TextBox()
		Me.Lbl_Klasifikasi2_Keterangan = New System.Windows.Forms.Label()
		Me.Txt_Klasifikasi2_Kode = New System.Windows.Forms.TextBox()
		Me.Lbl_Klasifikasi2_Kode = New System.Windows.Forms.Label()
		Me.Txt_Klasifikasi2_PrefixKategori = New System.Windows.Forms.TextBox()
		Me.Lbl_Klasifikasi2_Kategori1 = New System.Windows.Forms.Label()
		Me.Cmb_Klasifikasi2_Kategori1 = New System.Windows.Forms.ComboBox()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Panel1.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(976, 51)
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
		Me.PanelGradient1.Size = New System.Drawing.Size(976, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(15, 11)
		Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(356, 30)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Master Data - Klasifikasi Bahan 2"
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(1, 63)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 416)
		Me.Panel3.TabIndex = 37
		Me.Panel3.Visible = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(0, 51)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(942, 12)
		Me.Panel2.TabIndex = 36
		Me.Panel2.Visible = False
		'
		'Txt_Klasifikasi2_Prefix
		'
		Me.Txt_Klasifikasi2_Prefix.BackColor = System.Drawing.Color.White
		Me.Txt_Klasifikasi2_Prefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Klasifikasi2_Prefix.Enabled = False
		Me.Txt_Klasifikasi2_Prefix.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.Txt_Klasifikasi2_Prefix.Location = New System.Drawing.Point(149, 156)
		Me.Txt_Klasifikasi2_Prefix.MaxLength = 2
		Me.Txt_Klasifikasi2_Prefix.Name = "Txt_Klasifikasi2_Prefix"
		Me.Txt_Klasifikasi2_Prefix.Size = New System.Drawing.Size(49, 22)
		Me.Txt_Klasifikasi2_Prefix.TabIndex = 3
		Me.Txt_Klasifikasi2_Prefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Lbl_Klasifikasi2_Prefix
		'
		Me.Lbl_Klasifikasi2_Prefix.AutoSize = True
		Me.Lbl_Klasifikasi2_Prefix.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Lbl_Klasifikasi2_Prefix.Location = New System.Drawing.Point(21, 158)
		Me.Lbl_Klasifikasi2_Prefix.Name = "Lbl_Klasifikasi2_Prefix"
		Me.Lbl_Klasifikasi2_Prefix.Size = New System.Drawing.Size(48, 20)
		Me.Lbl_Klasifikasi2_Prefix.TabIndex = 390
		Me.Lbl_Klasifikasi2_Prefix.Text = "Prefix"
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(149, 178)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(344, 12)
		Me.Panel6.TabIndex = 384
		Me.Panel6.Visible = False
		'
		'Cmb_Klasifikasi2_Kolom
		'
		Me.Cmb_Klasifikasi2_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Klasifikasi2_Kolom.DropDownWidth = 150
		Me.Cmb_Klasifikasi2_Kolom.Font = New System.Drawing.Font("Work Sans", 8.9!)
		Me.Cmb_Klasifikasi2_Kolom.FormattingEnabled = True
		Me.Cmb_Klasifikasi2_Kolom.Location = New System.Drawing.Point(76, 238)
		Me.Cmb_Klasifikasi2_Kolom.Margin = New System.Windows.Forms.Padding(2)
		Me.Cmb_Klasifikasi2_Kolom.Name = "Cmb_Klasifikasi2_Kolom"
		Me.Cmb_Klasifikasi2_Kolom.Size = New System.Drawing.Size(195, 25)
		Me.Cmb_Klasifikasi2_Kolom.TabIndex = 9
		'
		'Btn_Klasifikasi2_Cari
		'
		Me.Btn_Klasifikasi2_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Klasifikasi2_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Klasifikasi2_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn_Klasifikasi2_Cari.Location = New System.Drawing.Point(535, 235)
		Me.Btn_Klasifikasi2_Cari.Name = "Btn_Klasifikasi2_Cari"
		Me.Btn_Klasifikasi2_Cari.Size = New System.Drawing.Size(80, 28)
		Me.Btn_Klasifikasi2_Cari.TabIndex = 11
		Me.Btn_Klasifikasi2_Cari.Text = "Cari"
		Me.Btn_Klasifikasi2_Cari.UseVisualStyleBackColor = False
		'
		'Lbl_Klasifikasi2_Value
		'
		Me.Lbl_Klasifikasi2_Value.AutoSize = True
		Me.Lbl_Klasifikasi2_Value.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Lbl_Klasifikasi2_Value.Location = New System.Drawing.Point(289, 239)
		Me.Lbl_Klasifikasi2_Value.Name = "Lbl_Klasifikasi2_Value"
		Me.Lbl_Klasifikasi2_Value.Size = New System.Drawing.Size(46, 20)
		Me.Lbl_Klasifikasi2_Value.TabIndex = 389
		Me.Lbl_Klasifikasi2_Value.Text = "Value"
		'
		'Lbl_Klasifikasi2_Kolom
		'
		Me.Lbl_Klasifikasi2_Kolom.AutoSize = True
		Me.Lbl_Klasifikasi2_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Lbl_Klasifikasi2_Kolom.Location = New System.Drawing.Point(18, 239)
		Me.Lbl_Klasifikasi2_Kolom.Name = "Lbl_Klasifikasi2_Kolom"
		Me.Lbl_Klasifikasi2_Kolom.Size = New System.Drawing.Size(50, 20)
		Me.Lbl_Klasifikasi2_Kolom.TabIndex = 388
		Me.Lbl_Klasifikasi2_Kolom.Text = "Kolom"
		'
		'Txt_Klasifikasi2_Value
		'
		Me.Txt_Klasifikasi2_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Klasifikasi2_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Klasifikasi2_Value.Enabled = False
		Me.Txt_Klasifikasi2_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.Txt_Klasifikasi2_Value.Location = New System.Drawing.Point(343, 238)
		Me.Txt_Klasifikasi2_Value.MaxLength = 50
		Me.Txt_Klasifikasi2_Value.Name = "Txt_Klasifikasi2_Value"
		Me.Txt_Klasifikasi2_Value.Size = New System.Drawing.Size(189, 22)
		Me.Txt_Klasifikasi2_Value.TabIndex = 10
		'
		'Lv_Klasifikasi_Bahan2
		'
		Me.Lv_Klasifikasi_Bahan2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Lv_Klasifikasi_Bahan2.FullRowSelect = True
		Me.Lv_Klasifikasi_Bahan2.GridLines = True
		Me.Lv_Klasifikasi_Bahan2.HideSelection = False
		Me.Lv_Klasifikasi_Bahan2.Location = New System.Drawing.Point(20, 269)
		Me.Lv_Klasifikasi_Bahan2.Name = "Lv_Klasifikasi_Bahan2"
		Me.Lv_Klasifikasi_Bahan2.Size = New System.Drawing.Size(936, 257)
		Me.Lv_Klasifikasi_Bahan2.TabIndex = 8
		Me.Lv_Klasifikasi_Bahan2.UseCompatibleStateImageBehavior = False
		Me.Lv_Klasifikasi_Bahan2.View = System.Windows.Forms.View.Details
		'
		'Btn_Klasifikasi2_Refresh
		'
		Me.Btn_Klasifikasi2_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Klasifikasi2_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Klasifikasi2_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Klasifikasi2_Refresh.Location = New System.Drawing.Point(334, 185)
		Me.Btn_Klasifikasi2_Refresh.Name = "Btn_Klasifikasi2_Refresh"
		Me.Btn_Klasifikasi2_Refresh.Size = New System.Drawing.Size(84, 36)
		Me.Btn_Klasifikasi2_Refresh.TabIndex = 7
		Me.Btn_Klasifikasi2_Refresh.Text = "&Refresh"
		Me.Btn_Klasifikasi2_Refresh.UseVisualStyleBackColor = False
		'
		'Btn_Klasifikasi2_Hapus
		'
		Me.Btn_Klasifikasi2_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Klasifikasi2_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Klasifikasi2_Hapus.ForeColor = System.Drawing.Color.White
		Me.Btn_Klasifikasi2_Hapus.Location = New System.Drawing.Point(244, 185)
		Me.Btn_Klasifikasi2_Hapus.Name = "Btn_Klasifikasi2_Hapus"
		Me.Btn_Klasifikasi2_Hapus.Size = New System.Drawing.Size(84, 36)
		Me.Btn_Klasifikasi2_Hapus.TabIndex = 6
		Me.Btn_Klasifikasi2_Hapus.Text = "&Hapus"
		Me.Btn_Klasifikasi2_Hapus.UseVisualStyleBackColor = False
		'
		'Btn_Klasifikasi2_Simpan
		'
		Me.Btn_Klasifikasi2_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Klasifikasi2_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Klasifikasi2_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Klasifikasi2_Simpan.Location = New System.Drawing.Point(149, 185)
		Me.Btn_Klasifikasi2_Simpan.Name = "Btn_Klasifikasi2_Simpan"
		Me.Btn_Klasifikasi2_Simpan.Size = New System.Drawing.Size(84, 36)
		Me.Btn_Klasifikasi2_Simpan.TabIndex = 5
		Me.Btn_Klasifikasi2_Simpan.Text = "&Simpan"
		Me.Btn_Klasifikasi2_Simpan.UseVisualStyleBackColor = False
		'
		'Txt_Klasifikasi2_Keterangan
		'
		Me.Txt_Klasifikasi2_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Klasifikasi2_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Klasifikasi2_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.Txt_Klasifikasi2_Keterangan.Location = New System.Drawing.Point(149, 128)
		Me.Txt_Klasifikasi2_Keterangan.MaxLength = 100
		Me.Txt_Klasifikasi2_Keterangan.Name = "Txt_Klasifikasi2_Keterangan"
		Me.Txt_Klasifikasi2_Keterangan.Size = New System.Drawing.Size(344, 22)
		Me.Txt_Klasifikasi2_Keterangan.TabIndex = 2
		'
		'Lbl_Klasifikasi2_Keterangan
		'
		Me.Lbl_Klasifikasi2_Keterangan.AutoSize = True
		Me.Lbl_Klasifikasi2_Keterangan.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Lbl_Klasifikasi2_Keterangan.Location = New System.Drawing.Point(21, 129)
		Me.Lbl_Klasifikasi2_Keterangan.Name = "Lbl_Klasifikasi2_Keterangan"
		Me.Lbl_Klasifikasi2_Keterangan.Size = New System.Drawing.Size(86, 20)
		Me.Lbl_Klasifikasi2_Keterangan.TabIndex = 386
		Me.Lbl_Klasifikasi2_Keterangan.Text = "Keterangan"
		'
		'Txt_Klasifikasi2_Kode
		'
		Me.Txt_Klasifikasi2_Kode.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Klasifikasi2_Kode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Klasifikasi2_Kode.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.Txt_Klasifikasi2_Kode.Location = New System.Drawing.Point(149, 99)
		Me.Txt_Klasifikasi2_Kode.MaxLength = 50
		Me.Txt_Klasifikasi2_Kode.Name = "Txt_Klasifikasi2_Kode"
		Me.Txt_Klasifikasi2_Kode.Size = New System.Drawing.Size(228, 22)
		Me.Txt_Klasifikasi2_Kode.TabIndex = 1
		'
		'Lbl_Klasifikasi2_Kode
		'
		Me.Lbl_Klasifikasi2_Kode.AutoSize = True
		Me.Lbl_Klasifikasi2_Kode.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Lbl_Klasifikasi2_Kode.Location = New System.Drawing.Point(21, 100)
		Me.Lbl_Klasifikasi2_Kode.Name = "Lbl_Klasifikasi2_Kode"
		Me.Lbl_Klasifikasi2_Kode.Size = New System.Drawing.Size(42, 20)
		Me.Lbl_Klasifikasi2_Kode.TabIndex = 385
		Me.Lbl_Klasifikasi2_Kode.Text = "Kode"
		'
		'Txt_Klasifikasi2_PrefixKategori
		'
		Me.Txt_Klasifikasi2_PrefixKategori.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Klasifikasi2_PrefixKategori.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Klasifikasi2_PrefixKategori.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.Txt_Klasifikasi2_PrefixKategori.Location = New System.Drawing.Point(204, 156)
		Me.Txt_Klasifikasi2_PrefixKategori.MaxLength = 2
		Me.Txt_Klasifikasi2_PrefixKategori.Name = "Txt_Klasifikasi2_PrefixKategori"
		Me.Txt_Klasifikasi2_PrefixKategori.Size = New System.Drawing.Size(49, 22)
		Me.Txt_Klasifikasi2_PrefixKategori.TabIndex = 4
		'
		'Lbl_Klasifikasi2_Kategori1
		'
		Me.Lbl_Klasifikasi2_Kategori1.AutoSize = True
		Me.Lbl_Klasifikasi2_Kategori1.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Lbl_Klasifikasi2_Kategori1.Location = New System.Drawing.Point(21, 70)
		Me.Lbl_Klasifikasi2_Kategori1.Name = "Lbl_Klasifikasi2_Kategori1"
		Me.Lbl_Klasifikasi2_Kategori1.Size = New System.Drawing.Size(75, 20)
		Me.Lbl_Klasifikasi2_Kategori1.TabIndex = 385
		Me.Lbl_Klasifikasi2_Kategori1.Text = "Kategori 1"
		'
		'Cmb_Klasifikasi2_Kategori1
		'
		Me.Cmb_Klasifikasi2_Kategori1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Klasifikasi2_Kategori1.Font = New System.Drawing.Font("Work Sans", 8.999999!)
		Me.Cmb_Klasifikasi2_Kategori1.FormattingEnabled = True
		Me.Cmb_Klasifikasi2_Kategori1.Location = New System.Drawing.Point(149, 68)
		Me.Cmb_Klasifikasi2_Kategori1.Name = "Cmb_Klasifikasi2_Kategori1"
		Me.Cmb_Klasifikasi2_Kategori1.Size = New System.Drawing.Size(228, 25)
		Me.Cmb_Klasifikasi2_Kategori1.TabIndex = 0
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(149, 220)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(344, 12)
		Me.Panel4.TabIndex = 384
		Me.Panel4.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(957, 81)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(19, 416)
		Me.Panel5.TabIndex = 37
		Me.Panel5.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(1, 528)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(942, 12)
		Me.Panel7.TabIndex = 36
		Me.Panel7.Visible = False
		'
		'Master_Klasifikasi_Bahan2
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(976, 539)
		Me.Controls.Add(Me.Cmb_Klasifikasi2_Kategori1)
		Me.Controls.Add(Me.Txt_Klasifikasi2_PrefixKategori)
		Me.Controls.Add(Me.Txt_Klasifikasi2_Prefix)
		Me.Controls.Add(Me.Lbl_Klasifikasi2_Prefix)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Cmb_Klasifikasi2_Kolom)
		Me.Controls.Add(Me.Btn_Klasifikasi2_Cari)
		Me.Controls.Add(Me.Lbl_Klasifikasi2_Value)
		Me.Controls.Add(Me.Lbl_Klasifikasi2_Kolom)
		Me.Controls.Add(Me.Txt_Klasifikasi2_Value)
		Me.Controls.Add(Me.Lv_Klasifikasi_Bahan2)
		Me.Controls.Add(Me.Btn_Klasifikasi2_Refresh)
		Me.Controls.Add(Me.Btn_Klasifikasi2_Hapus)
		Me.Controls.Add(Me.Btn_Klasifikasi2_Simpan)
		Me.Controls.Add(Me.Txt_Klasifikasi2_Keterangan)
		Me.Controls.Add(Me.Lbl_Klasifikasi2_Keterangan)
		Me.Controls.Add(Me.Lbl_Klasifikasi2_Kategori1)
		Me.Controls.Add(Me.Txt_Klasifikasi2_Kode)
		Me.Controls.Add(Me.Lbl_Klasifikasi2_Kode)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel1)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel2)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "Master_Klasifikasi_Bahan2"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Txt_Klasifikasi2_Prefix As TextBox
    Friend WithEvents Lbl_Klasifikasi2_Prefix As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Cmb_Klasifikasi2_Kolom As ComboBox
    Friend WithEvents Btn_Klasifikasi2_Cari As Button
    Friend WithEvents Lbl_Klasifikasi2_Value As Label
    Friend WithEvents Lbl_Klasifikasi2_Kolom As Label
    Friend WithEvents Txt_Klasifikasi2_Value As TextBox
    Friend WithEvents Lv_Klasifikasi_Bahan2 As ListView
    Friend WithEvents Btn_Klasifikasi2_Refresh As Button
    Friend WithEvents Btn_Klasifikasi2_Hapus As Button
    Friend WithEvents Btn_Klasifikasi2_Simpan As Button
    Friend WithEvents Txt_Klasifikasi2_Keterangan As TextBox
    Friend WithEvents Lbl_Klasifikasi2_Keterangan As Label
    Friend WithEvents Txt_Klasifikasi2_Kode As TextBox
    Friend WithEvents Lbl_Klasifikasi2_Kode As Label
    Friend WithEvents Txt_Klasifikasi2_PrefixKategori As TextBox
    Friend WithEvents Lbl_Klasifikasi2_Kategori1 As Label
    Friend WithEvents Cmb_Klasifikasi2_Kategori1 As ComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel7 As Panel
End Class
