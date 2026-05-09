<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Transaksi_Validasi_Trial_Formula
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
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.LblFormulator_Judul = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_NoFormula = New System.Windows.Forms.TextBox()
        Me.LblFormulator_KodeBarang = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_Kd_Barang = New System.Windows.Forms.TextBox()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Hasil = New System.Windows.Forms.TextBox()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TBDeskripsi = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.LblFormulator_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(558, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(558, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1279, 49)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 416)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'LblFormulator_Judul
        '
        Me.LblFormulator_Judul.AutoSize = True
        Me.LblFormulator_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold)
        Me.LblFormulator_Judul.Location = New System.Drawing.Point(15, 11)
        Me.LblFormulator_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblFormulator_Judul.Name = "LblFormulator_Judul"
        Me.LblFormulator_Judul.Size = New System.Drawing.Size(389, 29)
        Me.LblFormulator_Judul.TabIndex = 0
        Me.LblFormulator_Judul.Text = "Show Dialog - Setup Susunan Formula"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 59)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 574)
        Me.Panel3.TabIndex = 24
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(4, 50)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1142, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Txt_NoFormula
        '
        Me.Txt_NoFormula.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoFormula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoFormula.Enabled = False
        Me.Txt_NoFormula.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_NoFormula.Location = New System.Drawing.Point(114, 65)
        Me.Txt_NoFormula.MaxLength = 50
        Me.Txt_NoFormula.Name = "Txt_NoFormula"
        Me.Txt_NoFormula.Size = New System.Drawing.Size(424, 20)
        Me.Txt_NoFormula.TabIndex = 377
        Me.Txt_NoFormula.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblFormulator_KodeBarang
        '
        Me.LblFormulator_KodeBarang.AutoSize = True
        Me.LblFormulator_KodeBarang.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.LblFormulator_KodeBarang.Location = New System.Drawing.Point(22, 66)
        Me.LblFormulator_KodeBarang.Name = "LblFormulator_KodeBarang"
        Me.LblFormulator_KodeBarang.Size = New System.Drawing.Size(71, 17)
        Me.LblFormulator_KodeBarang.TabIndex = 378
        Me.LblFormulator_KodeBarang.Text = "No Fornula"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(22, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 17)
        Me.Label1.TabIndex = 378
        Me.Label1.Text = "Barang"
        '
        'Txt_Kd_Barang
        '
        Me.Txt_Kd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kd_Barang.Enabled = False
        Me.Txt_Kd_Barang.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Kd_Barang.Location = New System.Drawing.Point(114, 91)
        Me.Txt_Kd_Barang.MaxLength = 50
        Me.Txt_Kd_Barang.Name = "Txt_Kd_Barang"
        Me.Txt_Kd_Barang.Size = New System.Drawing.Size(108, 20)
        Me.Txt_Kd_Barang.TabIndex = 377
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmBarang.Enabled = False
        Me.Txt_NmBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_NmBarang.Location = New System.Drawing.Point(228, 91)
        Me.Txt_NmBarang.MaxLength = 50
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(310, 20)
        Me.Txt_NmBarang.TabIndex = 379
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(22, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 17)
        Me.Label2.TabIndex = 378
        Me.Label2.Text = "Hasil"
        '
        'Txt_Hasil
        '
        Me.Txt_Hasil.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Hasil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Hasil.Enabled = False
        Me.Txt_Hasil.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Hasil.Location = New System.Drawing.Point(114, 117)
        Me.Txt_Hasil.MaxLength = 50
        Me.Txt_Hasil.Name = "Txt_Hasil"
        Me.Txt_Hasil.Size = New System.Drawing.Size(108, 20)
        Me.Txt_Hasil.TabIndex = 377
        Me.Txt_Hasil.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan.Enabled = False
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Items.AddRange(New Object() {"Ali"})
        Me.Cmb_Satuan.Location = New System.Drawing.Point(228, 116)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(65, 24)
        Me.Cmb_Satuan.TabIndex = 380
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TBDeskripsi)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 149)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(516, 94)
        Me.GroupBox1.TabIndex = 381
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Deskripsi"
        '
        'TBDeskripsi
        '
        Me.TBDeskripsi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TBDeskripsi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TBDeskripsi.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.TBDeskripsi.Location = New System.Drawing.Point(6, 21)
        Me.TBDeskripsi.MaxLength = 50
        Me.TBDeskripsi.Multiline = True
        Me.TBDeskripsi.Name = "TBDeskripsi"
        Me.TBDeskripsi.Size = New System.Drawing.Size(504, 67)
        Me.TBDeskripsi.TabIndex = 378
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(539, 68)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 574)
        Me.Panel4.TabIndex = 24
        Me.Panel4.Visible = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(22, 250)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(170, 36)
        Me.Btn_Simpan.TabIndex = 401
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(198, 250)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(141, 36)
        Me.Btn_Refresh.TabIndex = 402
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(21, 288)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1142, 15)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'N_EMI_SD_Transaksi_Validasi_Trial_Formula
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(558, 303)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Cmb_Satuan)
        Me.Controls.Add(Me.Txt_NmBarang)
        Me.Controls.Add(Me.Txt_Hasil)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Txt_Kd_Barang)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_NoFormula)
        Me.Controls.Add(Me.LblFormulator_KodeBarang)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "N_EMI_SD_Transaksi_Validasi_Trial_Formula"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Panel5 As Panel
	Friend WithEvents LblFormulator_Judul As Label
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Txt_NoFormula As TextBox
	Friend WithEvents LblFormulator_KodeBarang As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents Txt_Kd_Barang As TextBox
	Friend WithEvents Txt_NmBarang As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents Txt_Hasil As TextBox
	Friend WithEvents Cmb_Satuan As ComboBox
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Btn_Simpan As Button
	Friend WithEvents Btn_Refresh As Button
	Friend WithEvents Panel6 As Panel
    Friend WithEvents TBDeskripsi As TextBox
End Class
