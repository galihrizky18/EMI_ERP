<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Perhitungan_Jatuh_Tempo
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Master_Perhitungan_Jatuh_Tempo))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Lbl_Keterangan = New System.Windows.Forms.Label()
        Me.Txt_Kd = New System.Windows.Forms.TextBox()
        Me.Lbl_Kd = New System.Windows.Forms.Label()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Lv_MasterBiaya = New System.Windows.Forms.ListView()
        Me.Cmb_Kolom = New System.Windows.Forms.ComboBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Lbl_Value = New System.Windows.Forms.Label()
        Me.Lbl_Kolom = New System.Windows.Forms.Label()
        Me.Txt_Value = New System.Windows.Forms.TextBox()
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
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(678, 51)
        Me.Panel1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(490, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Master Data - Jenis Perhitungan Jatuh Tempo"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 52)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1312, 10)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(16, 619)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(660, 63)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(16, 619)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(18, 516)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1231, 12)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.Red
        Me.Panel17.Location = New System.Drawing.Point(3, 128)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(1264, 10)
        Me.Panel17.TabIndex = 35
        Me.Panel17.Visible = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "3.png")
        Me.ImageList1.Images.SetKeyName(1, "1.png")
        Me.ImageList1.Images.SetKeyName(2, "6.png")
        Me.ImageList1.Images.SetKeyName(3, "5.png")
        Me.ImageList1.Images.SetKeyName(4, "4.png")
        Me.ImageList1.Images.SetKeyName(5, "2.png")
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(209, 98)
        Me.Txt_Keterangan.MaxLength = 50
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(228, 22)
        Me.Txt_Keterangan.TabIndex = 1
        '
        'Lbl_Keterangan
        '
        Me.Lbl_Keterangan.AutoSize = True
        Me.Lbl_Keterangan.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Keterangan.Location = New System.Drawing.Point(21, 99)
        Me.Lbl_Keterangan.Name = "Lbl_Keterangan"
        Me.Lbl_Keterangan.Size = New System.Drawing.Size(86, 20)
        Me.Lbl_Keterangan.TabIndex = 234
        Me.Lbl_Keterangan.Text = "Keterangan"
        '
        'Txt_Kd
        '
        Me.Txt_Kd.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kd.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Kd.Location = New System.Drawing.Point(209, 66)
        Me.Txt_Kd.MaxLength = 50
        Me.Txt_Kd.Name = "Txt_Kd"
        Me.Txt_Kd.Size = New System.Drawing.Size(228, 22)
        Me.Txt_Kd.TabIndex = 0
        '
        'Lbl_Kd
        '
        Me.Lbl_Kd.AutoSize = True
        Me.Lbl_Kd.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kd.Location = New System.Drawing.Point(21, 67)
        Me.Lbl_Kd.Name = "Lbl_Kd"
        Me.Lbl_Kd.Size = New System.Drawing.Size(42, 20)
        Me.Lbl_Kd.TabIndex = 233
        Me.Lbl_Kd.Text = "Kode"
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(390, 139)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 4
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(300, 139)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Hapus.TabIndex = 3
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(210, 139)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 2
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(4, 177)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1264, 10)
        Me.Panel6.TabIndex = 36
        Me.Panel6.Visible = False
        '
        'Lv_MasterBiaya
        '
        Me.Lv_MasterBiaya.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_MasterBiaya.FullRowSelect = True
        Me.Lv_MasterBiaya.GridLines = True
        Me.Lv_MasterBiaya.HideSelection = False
        Me.Lv_MasterBiaya.Location = New System.Drawing.Point(20, 219)
        Me.Lv_MasterBiaya.Name = "Lv_MasterBiaya"
        Me.Lv_MasterBiaya.Size = New System.Drawing.Size(636, 296)
        Me.Lv_MasterBiaya.TabIndex = 8
        Me.Lv_MasterBiaya.UseCompatibleStateImageBehavior = False
        Me.Lv_MasterBiaya.View = System.Windows.Forms.View.Details
        '
        'Cmb_Kolom
        '
        Me.Cmb_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Kolom.DropDownWidth = 150
        Me.Cmb_Kolom.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.Cmb_Kolom.FormattingEnabled = True
        Me.Cmb_Kolom.Location = New System.Drawing.Point(84, 190)
        Me.Cmb_Kolom.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_Kolom.Name = "Cmb_Kolom"
        Me.Cmb_Kolom.Size = New System.Drawing.Size(195, 25)
        Me.Cmb_Kolom.TabIndex = 5
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(531, 187)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Cari.TabIndex = 7
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Lbl_Value
        '
        Me.Lbl_Value.AutoSize = True
        Me.Lbl_Value.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Value.Location = New System.Drawing.Point(284, 192)
        Me.Lbl_Value.Name = "Lbl_Value"
        Me.Lbl_Value.Size = New System.Drawing.Size(46, 20)
        Me.Lbl_Value.TabIndex = 348
        Me.Lbl_Value.Text = "Value"
        '
        'Lbl_Kolom
        '
        Me.Lbl_Kolom.AutoSize = True
        Me.Lbl_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kolom.Location = New System.Drawing.Point(25, 192)
        Me.Lbl_Kolom.Name = "Lbl_Kolom"
        Me.Lbl_Kolom.Size = New System.Drawing.Size(50, 20)
        Me.Lbl_Kolom.TabIndex = 347
        Me.Lbl_Kolom.Text = "Kolom"
        '
        'Txt_Value
        '
        Me.Txt_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Value.Location = New System.Drawing.Point(336, 191)
        Me.Txt_Value.MaxLength = 50
        Me.Txt_Value.Name = "Txt_Value"
        Me.Txt_Value.Size = New System.Drawing.Size(189, 22)
        Me.Txt_Value.TabIndex = 6
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
        Me.PanelGradient1.Size = New System.Drawing.Size(678, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Master_Perhitungan_Jatuh_Tempo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(678, 527)
        Me.Controls.Add(Me.Lv_MasterBiaya)
        Me.Controls.Add(Me.Cmb_Kolom)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Lbl_Value)
        Me.Controls.Add(Me.Lbl_Kolom)
        Me.Controls.Add(Me.Txt_Value)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.Lbl_Keterangan)
        Me.Controls.Add(Me.Txt_Kd)
        Me.Controls.Add(Me.Lbl_Kd)
        Me.Controls.Add(Me.Panel17)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Master_Perhitungan_Jatuh_Tempo"
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
    Friend WithEvents Panel17 As Panel
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Lbl_Keterangan As Label
    Friend WithEvents Txt_Kd As TextBox
    Friend WithEvents Lbl_Kd As Label
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Lv_MasterBiaya As ListView
    Friend WithEvents Cmb_Kolom As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Lbl_Value As Label
    Friend WithEvents Lbl_Kolom As Label
    Friend WithEvents Txt_Value As TextBox
End Class
