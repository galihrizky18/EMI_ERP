<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Master_Persentase_Penentu_Barang
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Labeljudul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Lbl_Kd = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.LvPilihBarang_DataBarang = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Lv_DataPersentase = New System.Windows.Forms.ListView()
        Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_ValueFilter = New System.Windows.Forms.TextBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.TxtPilihBarang_NamaBarang = New System.Windows.Forms.TextBox()
        Me.TxtPilihBarang_KodeBarang = New System.Windows.Forms.TextBox()
        Me.TxtPilihBarang_Satuan = New System.Windows.Forms.TextBox()
        Me.TxtPersentase = New System.Windows.Forms.TextBox()
        Me.BtnMinus = New System.Windows.Forms.Button()
        Me.BtnPlus = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Labeljudul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(679, 40)
        Me.Panel1.TabIndex = 25
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 38)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(679, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Labeljudul
        '
        Me.Labeljudul.AutoSize = True
        Me.Labeljudul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Labeljudul.Location = New System.Drawing.Point(4, 4)
        Me.Labeljudul.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Labeljudul.Name = "Labeljudul"
        Me.Labeljudul.Size = New System.Drawing.Size(419, 25)
        Me.Labeljudul.TabIndex = 0
        Me.Labeljudul.Text = "Master Data - Persentase Penentu Barang"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(1, 40)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1227, 12)
        Me.Panel2.TabIndex = 37
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 50)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 877)
        Me.Panel3.TabIndex = 38
        Me.Panel3.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(21, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 15)
        Me.Label1.TabIndex = 354
        Me.Label1.Text = "Persentase (%)"
        '
        'Lbl_Kd
        '
        Me.Lbl_Kd.AutoSize = True
        Me.Lbl_Kd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lbl_Kd.Location = New System.Drawing.Point(21, 53)
        Me.Lbl_Kd.Name = "Lbl_Kd"
        Me.Lbl_Kd.Size = New System.Drawing.Size(79, 15)
        Me.Lbl_Kd.TabIndex = 355
        Me.Lbl_Kd.Text = "Kode Barang"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(19, 98)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1227, 12)
        Me.Panel6.TabIndex = 356
        Me.Panel6.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(297, 111)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(85, 30)
        Me.Btn_Refresh.TabIndex = 359
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(209, 111)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(85, 30)
        Me.Btn_Hapus.TabIndex = 358
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(121, 111)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(85, 30)
        Me.Btn_Simpan.TabIndex = 357
        Me.Btn_Simpan.Tag = "SIMPAN"
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(19, 143)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1227, 12)
        Me.Panel4.TabIndex = 356
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(660, 51)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 877)
        Me.Panel5.TabIndex = 38
        Me.Panel5.Visible = False
        '
        'LvPilihBarang_DataBarang
        '
        Me.LvPilihBarang_DataBarang.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9})
        Me.LvPilihBarang_DataBarang.FullRowSelect = True
        Me.LvPilihBarang_DataBarang.GridLines = True
        Me.LvPilihBarang_DataBarang.HideSelection = False
        Me.LvPilihBarang_DataBarang.Location = New System.Drawing.Point(246, 185)
        Me.LvPilihBarang_DataBarang.Name = "LvPilihBarang_DataBarang"
        Me.LvPilihBarang_DataBarang.Size = New System.Drawing.Size(414, 128)
        Me.LvPilihBarang_DataBarang.TabIndex = 373
        Me.LvPilihBarang_DataBarang.UseCompatibleStateImageBehavior = False
        Me.LvPilihBarang_DataBarang.View = System.Windows.Forms.View.Details
        Me.LvPilihBarang_DataBarang.Visible = False
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
        'Lv_DataPersentase
        '
        Me.Lv_DataPersentase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Lv_DataPersentase.FullRowSelect = True
        Me.Lv_DataPersentase.GridLines = True
        Me.Lv_DataPersentase.HideSelection = False
        Me.Lv_DataPersentase.Location = New System.Drawing.Point(20, 185)
        Me.Lv_DataPersentase.Name = "Lv_DataPersentase"
        Me.Lv_DataPersentase.Size = New System.Drawing.Size(640, 307)
        Me.Lv_DataPersentase.TabIndex = 364
        Me.Lv_DataPersentase.UseCompatibleStateImageBehavior = False
        Me.Lv_DataPersentase.View = System.Windows.Forms.View.Details
        '
        'Cmb_Filter
        '
        Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter.DropDownWidth = 150
        Me.Cmb_Filter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Cmb_Filter.FormattingEnabled = True
        Me.Cmb_Filter.Location = New System.Drawing.Point(74, 154)
        Me.Cmb_Filter.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_Filter.Name = "Cmb_Filter"
        Me.Cmb_Filter.Size = New System.Drawing.Size(160, 21)
        Me.Cmb_Filter.TabIndex = 369
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(483, 151)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Cari.TabIndex = 368
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(245, 157)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 15)
        Me.Label5.TabIndex = 367
        Me.Label5.Text = "Value"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(25, 157)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 15)
        Me.Label4.TabIndex = 366
        Me.Label4.Text = "Kolom"
        '
        'Txt_ValueFilter
        '
        Me.Txt_ValueFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_ValueFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ValueFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Txt_ValueFilter.Location = New System.Drawing.Point(288, 155)
        Me.Txt_ValueFilter.MaxLength = 50
        Me.Txt_ValueFilter.Name = "Txt_ValueFilter"
        Me.Txt_ValueFilter.Size = New System.Drawing.Size(189, 20)
        Me.Txt_ValueFilter.TabIndex = 365
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(19, 493)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1227, 15)
        Me.Panel7.TabIndex = 356
        Me.Panel7.Visible = False
        '
        'TxtPilihBarang_NamaBarang
        '
        Me.TxtPilihBarang_NamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPilihBarang_NamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPilihBarang_NamaBarang.Enabled = False
        Me.TxtPilihBarang_NamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtPilihBarang_NamaBarang.Location = New System.Drawing.Point(288, 50)
        Me.TxtPilihBarang_NamaBarang.MaxLength = 100
        Me.TxtPilihBarang_NamaBarang.Name = "TxtPilihBarang_NamaBarang"
        Me.TxtPilihBarang_NamaBarang.Size = New System.Drawing.Size(276, 21)
        Me.TxtPilihBarang_NamaBarang.TabIndex = 372
        '
        'TxtPilihBarang_KodeBarang
        '
        Me.TxtPilihBarang_KodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPilihBarang_KodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPilihBarang_KodeBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtPilihBarang_KodeBarang.Location = New System.Drawing.Point(120, 50)
        Me.TxtPilihBarang_KodeBarang.MaxLength = 50
        Me.TxtPilihBarang_KodeBarang.Name = "TxtPilihBarang_KodeBarang"
        Me.TxtPilihBarang_KodeBarang.Size = New System.Drawing.Size(163, 21)
        Me.TxtPilihBarang_KodeBarang.TabIndex = 370
        '
        'TxtPilihBarang_Satuan
        '
        Me.TxtPilihBarang_Satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPilihBarang_Satuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPilihBarang_Satuan.Enabled = False
        Me.TxtPilihBarang_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtPilihBarang_Satuan.Location = New System.Drawing.Point(570, 50)
        Me.TxtPilihBarang_Satuan.MaxLength = 50
        Me.TxtPilihBarang_Satuan.Name = "TxtPilihBarang_Satuan"
        Me.TxtPilihBarang_Satuan.Size = New System.Drawing.Size(35, 21)
        Me.TxtPilihBarang_Satuan.TabIndex = 375
        '
        'TxtPersentase
        '
        Me.TxtPersentase.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPersentase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPersentase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.TxtPersentase.Location = New System.Drawing.Point(151, 77)
        Me.TxtPersentase.MaxLength = 3
        Me.TxtPersentase.Name = "TxtPersentase"
        Me.TxtPersentase.Size = New System.Drawing.Size(30, 20)
        Me.TxtPersentase.TabIndex = 376
        Me.TxtPersentase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BtnMinus
        '
        Me.BtnMinus.Location = New System.Drawing.Point(118, 76)
        Me.BtnMinus.Name = "BtnMinus"
        Me.BtnMinus.Size = New System.Drawing.Size(27, 23)
        Me.BtnMinus.TabIndex = 377
        Me.BtnMinus.Text = "-"
        Me.BtnMinus.UseVisualStyleBackColor = True
        '
        'BtnPlus
        '
        Me.BtnPlus.Location = New System.Drawing.Point(187, 76)
        Me.BtnPlus.Name = "BtnPlus"
        Me.BtnPlus.Size = New System.Drawing.Size(27, 23)
        Me.BtnPlus.TabIndex = 378
        Me.BtnPlus.Text = "+"
        Me.BtnPlus.UseVisualStyleBackColor = True
        '
        'N_EMI_Master_Persentase_Penentu_Barang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(679, 509)
        Me.Controls.Add(Me.LvPilihBarang_DataBarang)
        Me.Controls.Add(Me.BtnPlus)
        Me.Controls.Add(Me.BtnMinus)
        Me.Controls.Add(Me.TxtPersentase)
        Me.Controls.Add(Me.TxtPilihBarang_Satuan)
        Me.Controls.Add(Me.TxtPilihBarang_NamaBarang)
        Me.Controls.Add(Me.TxtPilihBarang_KodeBarang)
        Me.Controls.Add(Me.Lv_DataPersentase)
        Me.Controls.Add(Me.Cmb_Filter)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Txt_ValueFilter)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Lbl_Kd)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Master_Persentase_Penentu_Barang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Labeljudul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Lbl_Kd As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_DataPersentase As ListView
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_ValueFilter As TextBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents LvPilihBarang_DataBarang As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents TxtPilihBarang_NamaBarang As TextBox
    Friend WithEvents TxtPilihBarang_KodeBarang As TextBox
    Friend WithEvents TxtPilihBarang_Satuan As TextBox
    Friend WithEvents TxtPersentase As TextBox
    Friend WithEvents BtnMinus As Button
    Friend WithEvents BtnPlus As Button
End Class
