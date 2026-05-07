<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Klasifikasi_Bahan
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
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.CmbKlasifikasiBahan_Kolom = New System.Windows.Forms.ComboBox()
        Me.BtnKlasifikasiBahan_Cari = New System.Windows.Forms.Button()
        Me.LblKlasifikasiBahan_Value = New System.Windows.Forms.Label()
        Me.LblKlasifikasiBahan_Kolom = New System.Windows.Forms.Label()
        Me.TxtKlasifikasiBahan_Value = New System.Windows.Forms.TextBox()
        Me.LvwKlasifikasiBahan_Data = New System.Windows.Forms.ListView()
        Me.BtnKlasifikasiBahan_Refresh = New System.Windows.Forms.Button()
        Me.BtnKlasifikasiBahan_Hapus = New System.Windows.Forms.Button()
        Me.BtnKlasifikasiBahan_Simpan = New System.Windows.Forms.Button()
        Me.TxtKlasifikasiBahan_Ket = New System.Windows.Forms.TextBox()
        Me.LblKlasifikasiBahan_Ket = New System.Windows.Forms.Label()
        Me.TxtKlasifikasiBahan_Kode = New System.Windows.Forms.TextBox()
        Me.LblKlasifikasiBahan_Kode = New System.Windows.Forms.Label()
        Me.TxtKlasifikasiBahan_Prefix = New System.Windows.Forms.TextBox()
        Me.LblKlasifikasiBahan_Prefix = New System.Windows.Forms.Label()
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
        Me.Panel1.Size = New System.Drawing.Size(1047, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1047, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(322, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Master Data - Klasifikasi Bahan"
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
        Me.Panel5.Location = New System.Drawing.Point(1026, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 416)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1, 513)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(245, 162)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(344, 12)
        Me.Panel6.TabIndex = 360
        Me.Panel6.Visible = False
        '
        'CmbKlasifikasiBahan_Kolom
        '
        Me.CmbKlasifikasiBahan_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbKlasifikasiBahan_Kolom.DropDownWidth = 150
        Me.CmbKlasifikasiBahan_Kolom.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.CmbKlasifikasiBahan_Kolom.FormattingEnabled = True
        Me.CmbKlasifikasiBahan_Kolom.Location = New System.Drawing.Point(111, 221)
        Me.CmbKlasifikasiBahan_Kolom.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbKlasifikasiBahan_Kolom.Name = "CmbKlasifikasiBahan_Kolom"
        Me.CmbKlasifikasiBahan_Kolom.Size = New System.Drawing.Size(195, 25)
        Me.CmbKlasifikasiBahan_Kolom.TabIndex = 6
        '
        'BtnKlasifikasiBahan_Cari
        '
        Me.BtnKlasifikasiBahan_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnKlasifikasiBahan_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnKlasifikasiBahan_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnKlasifikasiBahan_Cari.Location = New System.Drawing.Point(565, 218)
        Me.BtnKlasifikasiBahan_Cari.Name = "BtnKlasifikasiBahan_Cari"
        Me.BtnKlasifikasiBahan_Cari.Size = New System.Drawing.Size(80, 28)
        Me.BtnKlasifikasiBahan_Cari.TabIndex = 8
        Me.BtnKlasifikasiBahan_Cari.Text = "Cari"
        Me.BtnKlasifikasiBahan_Cari.UseVisualStyleBackColor = False
        '
        'LblKlasifikasiBahan_Value
        '
        Me.LblKlasifikasiBahan_Value.AutoSize = True
        Me.LblKlasifikasiBahan_Value.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Value.Location = New System.Drawing.Point(319, 222)
        Me.LblKlasifikasiBahan_Value.Name = "LblKlasifikasiBahan_Value"
        Me.LblKlasifikasiBahan_Value.Size = New System.Drawing.Size(46, 20)
        Me.LblKlasifikasiBahan_Value.TabIndex = 371
        Me.LblKlasifikasiBahan_Value.Text = "Value"
        '
        'LblKlasifikasiBahan_Kolom
        '
        Me.LblKlasifikasiBahan_Kolom.AutoSize = True
        Me.LblKlasifikasiBahan_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Kolom.Location = New System.Drawing.Point(53, 222)
        Me.LblKlasifikasiBahan_Kolom.Name = "LblKlasifikasiBahan_Kolom"
        Me.LblKlasifikasiBahan_Kolom.Size = New System.Drawing.Size(50, 20)
        Me.LblKlasifikasiBahan_Kolom.TabIndex = 370
        Me.LblKlasifikasiBahan_Kolom.Text = "Kolom"
        '
        'TxtKlasifikasiBahan_Value
        '
        Me.TxtKlasifikasiBahan_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKlasifikasiBahan_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKlasifikasiBahan_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKlasifikasiBahan_Value.Location = New System.Drawing.Point(373, 221)
        Me.TxtKlasifikasiBahan_Value.MaxLength = 50
        Me.TxtKlasifikasiBahan_Value.Name = "TxtKlasifikasiBahan_Value"
        Me.TxtKlasifikasiBahan_Value.Size = New System.Drawing.Size(189, 22)
        Me.TxtKlasifikasiBahan_Value.TabIndex = 7
        '
        'LvwKlasifikasiBahan_Data
        '
        Me.LvwKlasifikasiBahan_Data.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.LvwKlasifikasiBahan_Data.FullRowSelect = True
        Me.LvwKlasifikasiBahan_Data.GridLines = True
        Me.LvwKlasifikasiBahan_Data.HideSelection = False
        Me.LvwKlasifikasiBahan_Data.Location = New System.Drawing.Point(55, 252)
        Me.LvwKlasifikasiBahan_Data.Name = "LvwKlasifikasiBahan_Data"
        Me.LvwKlasifikasiBahan_Data.Size = New System.Drawing.Size(936, 257)
        Me.LvwKlasifikasiBahan_Data.TabIndex = 368
        Me.LvwKlasifikasiBahan_Data.UseCompatibleStateImageBehavior = False
        Me.LvwKlasifikasiBahan_Data.View = System.Windows.Forms.View.Details
        '
        'BtnKlasifikasiBahan_Refresh
        '
        Me.BtnKlasifikasiBahan_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnKlasifikasiBahan_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnKlasifikasiBahan_Refresh.ForeColor = System.Drawing.Color.White
        Me.BtnKlasifikasiBahan_Refresh.Location = New System.Drawing.Point(425, 169)
        Me.BtnKlasifikasiBahan_Refresh.Name = "BtnKlasifikasiBahan_Refresh"
        Me.BtnKlasifikasiBahan_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.BtnKlasifikasiBahan_Refresh.TabIndex = 5
        Me.BtnKlasifikasiBahan_Refresh.Text = "&Refresh"
        Me.BtnKlasifikasiBahan_Refresh.UseVisualStyleBackColor = False
        '
        'BtnKlasifikasiBahan_Hapus
        '
        Me.BtnKlasifikasiBahan_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnKlasifikasiBahan_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnKlasifikasiBahan_Hapus.ForeColor = System.Drawing.Color.White
        Me.BtnKlasifikasiBahan_Hapus.Location = New System.Drawing.Point(335, 169)
        Me.BtnKlasifikasiBahan_Hapus.Name = "BtnKlasifikasiBahan_Hapus"
        Me.BtnKlasifikasiBahan_Hapus.Size = New System.Drawing.Size(84, 36)
        Me.BtnKlasifikasiBahan_Hapus.TabIndex = 4
        Me.BtnKlasifikasiBahan_Hapus.Text = "&Hapus"
        Me.BtnKlasifikasiBahan_Hapus.UseVisualStyleBackColor = False
        '
        'BtnKlasifikasiBahan_Simpan
        '
        Me.BtnKlasifikasiBahan_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnKlasifikasiBahan_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnKlasifikasiBahan_Simpan.ForeColor = System.Drawing.Color.White
        Me.BtnKlasifikasiBahan_Simpan.Location = New System.Drawing.Point(245, 169)
        Me.BtnKlasifikasiBahan_Simpan.Name = "BtnKlasifikasiBahan_Simpan"
        Me.BtnKlasifikasiBahan_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.BtnKlasifikasiBahan_Simpan.TabIndex = 3
        Me.BtnKlasifikasiBahan_Simpan.Text = "&Simpan"
        Me.BtnKlasifikasiBahan_Simpan.UseVisualStyleBackColor = False
        '
        'TxtKlasifikasiBahan_Ket
        '
        Me.TxtKlasifikasiBahan_Ket.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKlasifikasiBahan_Ket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKlasifikasiBahan_Ket.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKlasifikasiBahan_Ket.Location = New System.Drawing.Point(245, 112)
        Me.TxtKlasifikasiBahan_Ket.MaxLength = 100
        Me.TxtKlasifikasiBahan_Ket.Name = "TxtKlasifikasiBahan_Ket"
        Me.TxtKlasifikasiBahan_Ket.Size = New System.Drawing.Size(344, 22)
        Me.TxtKlasifikasiBahan_Ket.TabIndex = 1
        '
        'LblKlasifikasiBahan_Ket
        '
        Me.LblKlasifikasiBahan_Ket.AutoSize = True
        Me.LblKlasifikasiBahan_Ket.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Ket.Location = New System.Drawing.Point(57, 113)
        Me.LblKlasifikasiBahan_Ket.Name = "LblKlasifikasiBahan_Ket"
        Me.LblKlasifikasiBahan_Ket.Size = New System.Drawing.Size(86, 20)
        Me.LblKlasifikasiBahan_Ket.TabIndex = 363
        Me.LblKlasifikasiBahan_Ket.Text = "Keterangan"
        '
        'TxtKlasifikasiBahan_Kode
        '
        Me.TxtKlasifikasiBahan_Kode.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKlasifikasiBahan_Kode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKlasifikasiBahan_Kode.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKlasifikasiBahan_Kode.Location = New System.Drawing.Point(245, 83)
        Me.TxtKlasifikasiBahan_Kode.MaxLength = 50
        Me.TxtKlasifikasiBahan_Kode.Name = "TxtKlasifikasiBahan_Kode"
        Me.TxtKlasifikasiBahan_Kode.Size = New System.Drawing.Size(228, 22)
        Me.TxtKlasifikasiBahan_Kode.TabIndex = 0
        '
        'LblKlasifikasiBahan_Kode
        '
        Me.LblKlasifikasiBahan_Kode.AutoSize = True
        Me.LblKlasifikasiBahan_Kode.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Kode.Location = New System.Drawing.Point(57, 84)
        Me.LblKlasifikasiBahan_Kode.Name = "LblKlasifikasiBahan_Kode"
        Me.LblKlasifikasiBahan_Kode.Size = New System.Drawing.Size(42, 20)
        Me.LblKlasifikasiBahan_Kode.TabIndex = 361
        Me.LblKlasifikasiBahan_Kode.Text = "Kode"
        '
        'TxtKlasifikasiBahan_Prefix
        '
        Me.TxtKlasifikasiBahan_Prefix.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKlasifikasiBahan_Prefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKlasifikasiBahan_Prefix.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKlasifikasiBahan_Prefix.Location = New System.Drawing.Point(245, 140)
        Me.TxtKlasifikasiBahan_Prefix.MaxLength = 2
        Me.TxtKlasifikasiBahan_Prefix.Name = "TxtKlasifikasiBahan_Prefix"
        Me.TxtKlasifikasiBahan_Prefix.Size = New System.Drawing.Size(49, 22)
        Me.TxtKlasifikasiBahan_Prefix.TabIndex = 2
        '
        'LblKlasifikasiBahan_Prefix
        '
        Me.LblKlasifikasiBahan_Prefix.AutoSize = True
        Me.LblKlasifikasiBahan_Prefix.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Prefix.Location = New System.Drawing.Point(57, 142)
        Me.LblKlasifikasiBahan_Prefix.Name = "LblKlasifikasiBahan_Prefix"
        Me.LblKlasifikasiBahan_Prefix.Size = New System.Drawing.Size(48, 20)
        Me.LblKlasifikasiBahan_Prefix.TabIndex = 374
        Me.LblKlasifikasiBahan_Prefix.Text = "Prefix"
        '
        'Master_Klasifikasi_Bahan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1047, 526)
        Me.Controls.Add(Me.TxtKlasifikasiBahan_Prefix)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Prefix)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.CmbKlasifikasiBahan_Kolom)
        Me.Controls.Add(Me.BtnKlasifikasiBahan_Cari)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Value)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Kolom)
        Me.Controls.Add(Me.TxtKlasifikasiBahan_Value)
        Me.Controls.Add(Me.LvwKlasifikasiBahan_Data)
        Me.Controls.Add(Me.BtnKlasifikasiBahan_Refresh)
        Me.Controls.Add(Me.BtnKlasifikasiBahan_Hapus)
        Me.Controls.Add(Me.BtnKlasifikasiBahan_Simpan)
        Me.Controls.Add(Me.TxtKlasifikasiBahan_Ket)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Ket)
        Me.Controls.Add(Me.TxtKlasifikasiBahan_Kode)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Kode)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Master_Klasifikasi_Bahan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents CmbKlasifikasiBahan_Kolom As ComboBox
    Friend WithEvents BtnKlasifikasiBahan_Cari As Button
    Friend WithEvents LblKlasifikasiBahan_Value As Label
    Friend WithEvents LblKlasifikasiBahan_Kolom As Label
    Friend WithEvents TxtKlasifikasiBahan_Value As TextBox
    Friend WithEvents LvwKlasifikasiBahan_Data As ListView
    Friend WithEvents BtnKlasifikasiBahan_Refresh As Button
    Friend WithEvents BtnKlasifikasiBahan_Hapus As Button
    Friend WithEvents BtnKlasifikasiBahan_Simpan As Button
    Friend WithEvents TxtKlasifikasiBahan_Ket As TextBox
    Friend WithEvents LblKlasifikasiBahan_Ket As Label
    Friend WithEvents TxtKlasifikasiBahan_Kode As TextBox
    Friend WithEvents LblKlasifikasiBahan_Kode As Label
    Friend WithEvents TxtKlasifikasiBahan_Prefix As TextBox
    Friend WithEvents LblKlasifikasiBahan_Prefix As Label
End Class
