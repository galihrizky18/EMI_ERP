<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SD_Pilih_Barang
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
        Me.TxtPilihBarang_NamaBarang = New System.Windows.Forms.TextBox()
        Me.LblPilihBarang_NamaBarang = New System.Windows.Forms.Label()
        Me.TxtPilihBarang_KodeBarang = New System.Windows.Forms.TextBox()
        Me.LblPilihBarang_KodeBarang = New System.Windows.Forms.Label()
        Me.LblPilihBarang_Satuan = New System.Windows.Forms.Label()
        Me.LvPilihBarang_DataBarang = New System.Windows.Forms.ListView()
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LblPilihBarang_Lokasi = New System.Windows.Forms.Label()
        Me.CmbPilihBarang_Lokasi = New System.Windows.Forms.ComboBox()
        Me.CmbPilihBarang_Satuan = New System.Windows.Forms.ComboBox()
        Me.TxtPilihBarang_Satuan = New System.Windows.Forms.TextBox()
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
        Me.Panel1.Size = New System.Drawing.Size(578, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(578, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'LblPilihBarang_Judul
        '
        Me.LblPilihBarang_Judul.AutoSize = True
        Me.LblPilihBarang_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPilihBarang_Judul.Location = New System.Drawing.Point(15, 11)
        Me.LblPilihBarang_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblPilihBarang_Judul.Name = "LblPilihBarang_Judul"
        Me.LblPilihBarang_Judul.Size = New System.Drawing.Size(189, 30)
        Me.LblPilihBarang_Judul.TabIndex = 0
        Me.LblPilihBarang_Judul.Text = "SD - Pilih Barang"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 270)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(559, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 270)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(4, 244)
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
        Me.BtnPilihBarang_Refresh.Location = New System.Drawing.Point(120, 205)
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
        Me.BtnPilihBarang_Simpan.Location = New System.Drawing.Point(30, 205)
        Me.BtnPilihBarang_Simpan.Name = "BtnPilihBarang_Simpan"
        Me.BtnPilihBarang_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.BtnPilihBarang_Simpan.TabIndex = 353
        Me.BtnPilihBarang_Simpan.Text = "&Simpan"
        Me.BtnPilihBarang_Simpan.UseVisualStyleBackColor = False
        '
        'TxtPilihBarang_NamaBarang
        '
        Me.TxtPilihBarang_NamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPilihBarang_NamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPilihBarang_NamaBarang.Enabled = False
        Me.TxtPilihBarang_NamaBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtPilihBarang_NamaBarang.Location = New System.Drawing.Point(163, 133)
        Me.TxtPilihBarang_NamaBarang.MaxLength = 100
        Me.TxtPilihBarang_NamaBarang.Name = "TxtPilihBarang_NamaBarang"
        Me.TxtPilihBarang_NamaBarang.Size = New System.Drawing.Size(344, 22)
        Me.TxtPilihBarang_NamaBarang.TabIndex = 352
        '
        'LblPilihBarang_NamaBarang
        '
        Me.LblPilihBarang_NamaBarang.AutoSize = True
        Me.LblPilihBarang_NamaBarang.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblPilihBarang_NamaBarang.Location = New System.Drawing.Point(24, 133)
        Me.LblPilihBarang_NamaBarang.Name = "LblPilihBarang_NamaBarang"
        Me.LblPilihBarang_NamaBarang.Size = New System.Drawing.Size(56, 20)
        Me.LblPilihBarang_NamaBarang.TabIndex = 351
        Me.LblPilihBarang_NamaBarang.Text = "NamaX"
        '
        'TxtPilihBarang_KodeBarang
        '
        Me.TxtPilihBarang_KodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPilihBarang_KodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPilihBarang_KodeBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtPilihBarang_KodeBarang.Location = New System.Drawing.Point(163, 101)
        Me.TxtPilihBarang_KodeBarang.MaxLength = 50
        Me.TxtPilihBarang_KodeBarang.Name = "TxtPilihBarang_KodeBarang"
        Me.TxtPilihBarang_KodeBarang.Size = New System.Drawing.Size(228, 22)
        Me.TxtPilihBarang_KodeBarang.TabIndex = 350
        '
        'LblPilihBarang_KodeBarang
        '
        Me.LblPilihBarang_KodeBarang.AutoSize = True
        Me.LblPilihBarang_KodeBarang.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblPilihBarang_KodeBarang.Location = New System.Drawing.Point(24, 101)
        Me.LblPilihBarang_KodeBarang.Name = "LblPilihBarang_KodeBarang"
        Me.LblPilihBarang_KodeBarang.Size = New System.Drawing.Size(102, 20)
        Me.LblPilihBarang_KodeBarang.TabIndex = 349
        Me.LblPilihBarang_KodeBarang.Text = "Kode BarangX"
        '
        'LblPilihBarang_Satuan
        '
        Me.LblPilihBarang_Satuan.AutoSize = True
        Me.LblPilihBarang_Satuan.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblPilihBarang_Satuan.Location = New System.Drawing.Point(24, 164)
        Me.LblPilihBarang_Satuan.Name = "LblPilihBarang_Satuan"
        Me.LblPilihBarang_Satuan.Size = New System.Drawing.Size(66, 20)
        Me.LblPilihBarang_Satuan.TabIndex = 356
        Me.LblPilihBarang_Satuan.Text = "SatuanX"
        '
        'LvPilihBarang_DataBarang
        '
        Me.LvPilihBarang_DataBarang.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9})
        Me.LvPilihBarang_DataBarang.FullRowSelect = True
        Me.LvPilihBarang_DataBarang.GridLines = True
        Me.LvPilihBarang_DataBarang.HideSelection = False
        Me.LvPilihBarang_DataBarang.Location = New System.Drawing.Point(286, 200)
        Me.LvPilihBarang_DataBarang.Name = "LvPilihBarang_DataBarang"
        Me.LvPilihBarang_DataBarang.Size = New System.Drawing.Size(403, 130)
        Me.LvPilihBarang_DataBarang.TabIndex = 358
        Me.LvPilihBarang_DataBarang.UseCompatibleStateImageBehavior = False
        Me.LvPilihBarang_DataBarang.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Stock Owner"
        Me.ColumnHeader6.Width = 0
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
        '
        'LblPilihBarang_Lokasi
        '
        Me.LblPilihBarang_Lokasi.AutoSize = True
        Me.LblPilihBarang_Lokasi.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblPilihBarang_Lokasi.Location = New System.Drawing.Point(24, 72)
        Me.LblPilihBarang_Lokasi.Name = "LblPilihBarang_Lokasi"
        Me.LblPilihBarang_Lokasi.Size = New System.Drawing.Size(60, 20)
        Me.LblPilihBarang_Lokasi.TabIndex = 359
        Me.LblPilihBarang_Lokasi.Text = "LokasiX"
        '
        'CmbPilihBarang_Lokasi
        '
        Me.CmbPilihBarang_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPilihBarang_Lokasi.Enabled = False
        Me.CmbPilihBarang_Lokasi.FormattingEnabled = True
        Me.CmbPilihBarang_Lokasi.Location = New System.Drawing.Point(163, 68)
        Me.CmbPilihBarang_Lokasi.Name = "CmbPilihBarang_Lokasi"
        Me.CmbPilihBarang_Lokasi.Size = New System.Drawing.Size(228, 24)
        Me.CmbPilihBarang_Lokasi.TabIndex = 360
        '
        'CmbPilihBarang_Satuan
        '
        Me.CmbPilihBarang_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPilihBarang_Satuan.FormattingEnabled = True
        Me.CmbPilihBarang_Satuan.Location = New System.Drawing.Point(163, 163)
        Me.CmbPilihBarang_Satuan.Name = "CmbPilihBarang_Satuan"
        Me.CmbPilihBarang_Satuan.Size = New System.Drawing.Size(111, 24)
        Me.CmbPilihBarang_Satuan.TabIndex = 361
        '
        'TxtPilihBarang_Satuan
        '
        Me.TxtPilihBarang_Satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPilihBarang_Satuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPilihBarang_Satuan.Enabled = False
        Me.TxtPilihBarang_Satuan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtPilihBarang_Satuan.Location = New System.Drawing.Point(397, 101)
        Me.TxtPilihBarang_Satuan.MaxLength = 50
        Me.TxtPilihBarang_Satuan.Name = "TxtPilihBarang_Satuan"
        Me.TxtPilihBarang_Satuan.Size = New System.Drawing.Size(110, 22)
        Me.TxtPilihBarang_Satuan.TabIndex = 362
        '
        'SD_Pilih_Barang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(578, 260)
        Me.Controls.Add(Me.TxtPilihBarang_Satuan)
        Me.Controls.Add(Me.LvPilihBarang_DataBarang)
        Me.Controls.Add(Me.CmbPilihBarang_Lokasi)
        Me.Controls.Add(Me.LblPilihBarang_Lokasi)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.LblPilihBarang_Satuan)
        Me.Controls.Add(Me.BtnPilihBarang_Refresh)
        Me.Controls.Add(Me.BtnPilihBarang_Simpan)
        Me.Controls.Add(Me.TxtPilihBarang_NamaBarang)
        Me.Controls.Add(Me.LblPilihBarang_NamaBarang)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.TxtPilihBarang_KodeBarang)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.LblPilihBarang_KodeBarang)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CmbPilihBarang_Satuan)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SD_Pilih_Barang"
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
    Friend WithEvents TxtPilihBarang_NamaBarang As TextBox
    Friend WithEvents LblPilihBarang_NamaBarang As Label
    Friend WithEvents TxtPilihBarang_KodeBarang As TextBox
    Friend WithEvents LblPilihBarang_KodeBarang As Label
    Friend WithEvents LblPilihBarang_Satuan As Label
    Friend WithEvents LvPilihBarang_DataBarang As ListView
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents LblPilihBarang_Lokasi As Label
    Friend WithEvents CmbPilihBarang_Lokasi As ComboBox
    Friend WithEvents CmbPilihBarang_Satuan As ComboBox
    Friend WithEvents TxtPilihBarang_Satuan As TextBox
End Class
