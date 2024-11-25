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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Tb_PrefixKategori1 = New System.Windows.Forms.TextBox()
        Me.LblKlasifikasiBahan_Prefix = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Cmb_Kolom = New System.Windows.Forms.ComboBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.LblKlasifikasiBahan_Value = New System.Windows.Forms.Label()
        Me.LblKlasifikasiBahan_Kolom = New System.Windows.Forms.Label()
        Me.Tb_Value = New System.Windows.Forms.TextBox()
        Me.Lv_KlasifikasiBahan = New System.Windows.Forms.ListView()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Tb_Keterangan = New System.Windows.Forms.TextBox()
        Me.LblKlasifikasiBahan_Ket = New System.Windows.Forms.Label()
        Me.Tb_Kode = New System.Windows.Forms.TextBox()
        Me.LblKlasifikasiBahan_Kode = New System.Windows.Forms.Label()
        Me.Tb_PrefixKategori2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Cmb_Kategori1 = New System.Windows.Forms.ComboBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
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
        'Tb_PrefixKategori1
        '
        Me.Tb_PrefixKategori1.BackColor = System.Drawing.Color.White
        Me.Tb_PrefixKategori1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tb_PrefixKategori1.Enabled = False
        Me.Tb_PrefixKategori1.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Tb_PrefixKategori1.Location = New System.Drawing.Point(149, 156)
        Me.Tb_PrefixKategori1.MaxLength = 2
        Me.Tb_PrefixKategori1.Name = "Tb_PrefixKategori1"
        Me.Tb_PrefixKategori1.Size = New System.Drawing.Size(49, 22)
        Me.Tb_PrefixKategori1.TabIndex = 3
        Me.Tb_PrefixKategori1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblKlasifikasiBahan_Prefix
        '
        Me.LblKlasifikasiBahan_Prefix.AutoSize = True
        Me.LblKlasifikasiBahan_Prefix.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Prefix.Location = New System.Drawing.Point(21, 158)
        Me.LblKlasifikasiBahan_Prefix.Name = "LblKlasifikasiBahan_Prefix"
        Me.LblKlasifikasiBahan_Prefix.Size = New System.Drawing.Size(48, 20)
        Me.LblKlasifikasiBahan_Prefix.TabIndex = 390
        Me.LblKlasifikasiBahan_Prefix.Text = "Prefix"
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
        'Cmb_Kolom
        '
        Me.Cmb_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Kolom.DropDownWidth = 150
        Me.Cmb_Kolom.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.Cmb_Kolom.FormattingEnabled = True
        Me.Cmb_Kolom.Location = New System.Drawing.Point(76, 238)
        Me.Cmb_Kolom.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_Kolom.Name = "Cmb_Kolom"
        Me.Cmb_Kolom.Size = New System.Drawing.Size(195, 25)
        Me.Cmb_Kolom.TabIndex = 9
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(535, 235)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Cari.TabIndex = 11
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'LblKlasifikasiBahan_Value
        '
        Me.LblKlasifikasiBahan_Value.AutoSize = True
        Me.LblKlasifikasiBahan_Value.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Value.Location = New System.Drawing.Point(289, 239)
        Me.LblKlasifikasiBahan_Value.Name = "LblKlasifikasiBahan_Value"
        Me.LblKlasifikasiBahan_Value.Size = New System.Drawing.Size(46, 20)
        Me.LblKlasifikasiBahan_Value.TabIndex = 389
        Me.LblKlasifikasiBahan_Value.Text = "Value"
        '
        'LblKlasifikasiBahan_Kolom
        '
        Me.LblKlasifikasiBahan_Kolom.AutoSize = True
        Me.LblKlasifikasiBahan_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Kolom.Location = New System.Drawing.Point(18, 239)
        Me.LblKlasifikasiBahan_Kolom.Name = "LblKlasifikasiBahan_Kolom"
        Me.LblKlasifikasiBahan_Kolom.Size = New System.Drawing.Size(50, 20)
        Me.LblKlasifikasiBahan_Kolom.TabIndex = 388
        Me.LblKlasifikasiBahan_Kolom.Text = "Kolom"
        '
        'Tb_Value
        '
        Me.Tb_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Tb_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tb_Value.Enabled = False
        Me.Tb_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Tb_Value.Location = New System.Drawing.Point(343, 238)
        Me.Tb_Value.MaxLength = 50
        Me.Tb_Value.Name = "Tb_Value"
        Me.Tb_Value.Size = New System.Drawing.Size(189, 22)
        Me.Tb_Value.TabIndex = 10
        '
        'Lv_KlasifikasiBahan
        '
        Me.Lv_KlasifikasiBahan.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_KlasifikasiBahan.FullRowSelect = True
        Me.Lv_KlasifikasiBahan.GridLines = True
        Me.Lv_KlasifikasiBahan.HideSelection = False
        Me.Lv_KlasifikasiBahan.Location = New System.Drawing.Point(20, 269)
        Me.Lv_KlasifikasiBahan.Name = "Lv_KlasifikasiBahan"
        Me.Lv_KlasifikasiBahan.Size = New System.Drawing.Size(936, 257)
        Me.Lv_KlasifikasiBahan.TabIndex = 8
        Me.Lv_KlasifikasiBahan.UseCompatibleStateImageBehavior = False
        Me.Lv_KlasifikasiBahan.View = System.Windows.Forms.View.Details
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(334, 185)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 7
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(244, 185)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Hapus.TabIndex = 6
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(149, 185)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 5
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Tb_Keterangan
        '
        Me.Tb_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Tb_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tb_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Tb_Keterangan.Location = New System.Drawing.Point(149, 128)
        Me.Tb_Keterangan.MaxLength = 100
        Me.Tb_Keterangan.Name = "Tb_Keterangan"
        Me.Tb_Keterangan.Size = New System.Drawing.Size(344, 22)
        Me.Tb_Keterangan.TabIndex = 2
        '
        'LblKlasifikasiBahan_Ket
        '
        Me.LblKlasifikasiBahan_Ket.AutoSize = True
        Me.LblKlasifikasiBahan_Ket.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Ket.Location = New System.Drawing.Point(21, 129)
        Me.LblKlasifikasiBahan_Ket.Name = "LblKlasifikasiBahan_Ket"
        Me.LblKlasifikasiBahan_Ket.Size = New System.Drawing.Size(86, 20)
        Me.LblKlasifikasiBahan_Ket.TabIndex = 386
        Me.LblKlasifikasiBahan_Ket.Text = "Keterangan"
        '
        'Tb_Kode
        '
        Me.Tb_Kode.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Tb_Kode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tb_Kode.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Tb_Kode.Location = New System.Drawing.Point(149, 99)
        Me.Tb_Kode.MaxLength = 50
        Me.Tb_Kode.Name = "Tb_Kode"
        Me.Tb_Kode.Size = New System.Drawing.Size(228, 22)
        Me.Tb_Kode.TabIndex = 1
        '
        'LblKlasifikasiBahan_Kode
        '
        Me.LblKlasifikasiBahan_Kode.AutoSize = True
        Me.LblKlasifikasiBahan_Kode.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Kode.Location = New System.Drawing.Point(21, 100)
        Me.LblKlasifikasiBahan_Kode.Name = "LblKlasifikasiBahan_Kode"
        Me.LblKlasifikasiBahan_Kode.Size = New System.Drawing.Size(42, 20)
        Me.LblKlasifikasiBahan_Kode.TabIndex = 385
        Me.LblKlasifikasiBahan_Kode.Text = "Kode"
        '
        'Tb_PrefixKategori2
        '
        Me.Tb_PrefixKategori2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Tb_PrefixKategori2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tb_PrefixKategori2.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Tb_PrefixKategori2.Location = New System.Drawing.Point(204, 156)
        Me.Tb_PrefixKategori2.MaxLength = 2
        Me.Tb_PrefixKategori2.Name = "Tb_PrefixKategori2"
        Me.Tb_PrefixKategori2.Size = New System.Drawing.Size(49, 22)
        Me.Tb_PrefixKategori2.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(21, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 20)
        Me.Label2.TabIndex = 385
        Me.Label2.Text = "Kategori 1"
        '
        'Cmb_Kategori1
        '
        Me.Cmb_Kategori1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Kategori1.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Cmb_Kategori1.FormattingEnabled = True
        Me.Cmb_Kategori1.Location = New System.Drawing.Point(149, 68)
        Me.Cmb_Kategori1.Name = "Cmb_Kategori1"
        Me.Cmb_Kategori1.Size = New System.Drawing.Size(228, 25)
        Me.Cmb_Kategori1.TabIndex = 0
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
        'Master_Klasifikasi_Bahan2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(976, 539)
        Me.Controls.Add(Me.Cmb_Kategori1)
        Me.Controls.Add(Me.Tb_PrefixKategori2)
        Me.Controls.Add(Me.Tb_PrefixKategori1)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Prefix)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Cmb_Kolom)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Value)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Kolom)
        Me.Controls.Add(Me.Tb_Value)
        Me.Controls.Add(Me.Lv_KlasifikasiBahan)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Tb_Keterangan)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Ket)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Tb_Kode)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Kode)
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
    Friend WithEvents Tb_PrefixKategori1 As TextBox
    Friend WithEvents LblKlasifikasiBahan_Prefix As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Cmb_Kolom As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents LblKlasifikasiBahan_Value As Label
    Friend WithEvents LblKlasifikasiBahan_Kolom As Label
    Friend WithEvents Tb_Value As TextBox
    Friend WithEvents Lv_KlasifikasiBahan As ListView
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Tb_Keterangan As TextBox
    Friend WithEvents LblKlasifikasiBahan_Ket As Label
    Friend WithEvents Tb_Kode As TextBox
    Friend WithEvents LblKlasifikasiBahan_Kode As Label
    Friend WithEvents Tb_PrefixKategori2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Cmb_Kategori1 As ComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel7 As Panel
End Class
