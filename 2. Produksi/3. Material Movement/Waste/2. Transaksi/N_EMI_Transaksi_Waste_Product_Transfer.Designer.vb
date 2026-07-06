<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Transaksi_Waste_Product_Transfer
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
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel9 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Lv_Data = New System.Windows.Forms.ListView()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
		Me.Btn_Cari = New System.Windows.Forms.Button()
		Me.Txt_Filter = New System.Windows.Forms.TextBox()
		Me.Dtp_2 = New System.Windows.Forms.DateTimePicker()
		Me.Dtp_1 = New System.Windows.Forms.DateTimePicker()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Btn_Simpan = New System.Windows.Forms.Button()
		Me.Btn_Refresh = New System.Windows.Forms.Button()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.Txt_No_Transaksi = New System.Windows.Forms.TextBox()
		Me.Panel10 = New System.Windows.Forms.Panel()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
		Me.Panel11 = New System.Windows.Forms.Panel()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Cmb_Gudang_Tujuan = New System.Windows.Forms.ComboBox()
		Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Panel1.SuspendLayout()
		Me.Panel3.SuspendLayout()
		Me.Panel4.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Label3)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(1183, 45)
		Me.Panel1.TabIndex = 488
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 43)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(1183, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(18, 8)
		Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(424, 29)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Transaksi - Pengajuan Pemindahan Waste"
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Controls.Add(Me.Panel9)
		Me.Panel3.Location = New System.Drawing.Point(0, 45)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 649)
		Me.Panel3.TabIndex = 489
		Me.Panel3.Visible = False
		'
		'Panel9
		'
		Me.Panel9.BackColor = System.Drawing.Color.Red
		Me.Panel9.Location = New System.Drawing.Point(13, 146)
		Me.Panel9.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel9.Name = "Panel9"
		Me.Panel9.Size = New System.Drawing.Size(1091, 12)
		Me.Panel9.TabIndex = 38
		Me.Panel9.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(19, 44)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1087, 12)
		Me.Panel7.TabIndex = 490
		Me.Panel7.Visible = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(15, 596)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1087, 15)
		Me.Panel2.TabIndex = 490
		Me.Panel2.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Controls.Add(Me.Panel5)
		Me.Panel4.Location = New System.Drawing.Point(1165, 47)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(19, 649)
		Me.Panel4.TabIndex = 489
		Me.Panel4.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(13, 146)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(1091, 12)
		Me.Panel5.TabIndex = 38
		Me.Panel5.Visible = False
		'
		'Lv_Data
		'
		Me.Lv_Data.CheckBoxes = True
		Me.Lv_Data.FullRowSelect = True
		Me.Lv_Data.GridLines = True
		Me.Lv_Data.HideSelection = False
		Me.Lv_Data.Location = New System.Drawing.Point(20, 213)
		Me.Lv_Data.Name = "Lv_Data"
		Me.Lv_Data.Size = New System.Drawing.Size(1145, 270)
		Me.Lv_Data.TabIndex = 1
		Me.Lv_Data.UseCompatibleStateImageBehavior = False
		Me.Lv_Data.View = System.Windows.Forms.View.Details
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi)
		Me.GroupBox1.Controls.Add(Me.Label7)
		Me.GroupBox1.Controls.Add(Me.Cmb_Filter)
		Me.GroupBox1.Controls.Add(Me.Btn_Cari)
		Me.GroupBox1.Controls.Add(Me.Txt_Filter)
		Me.GroupBox1.Controls.Add(Me.Dtp_2)
		Me.GroupBox1.Controls.Add(Me.Dtp_1)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox1.Location = New System.Drawing.Point(20, 88)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(1145, 112)
		Me.GroupBox1.TabIndex = 0
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Filter"
		'
		'Cmb_Filter
		'
		Me.Cmb_Filter.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Filter.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Filter.FormattingEnabled = True
		Me.Cmb_Filter.Location = New System.Drawing.Point(90, 77)
		Me.Cmb_Filter.Name = "Cmb_Filter"
		Me.Cmb_Filter.Size = New System.Drawing.Size(166, 24)
		Me.Cmb_Filter.TabIndex = 4
		'
		'Btn_Cari
		'
		Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans Medium", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Btn_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari.Location = New System.Drawing.Point(557, 75)
		Me.Btn_Cari.Name = "Btn_Cari"
		Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
		Me.Btn_Cari.TabIndex = 3
		Me.Btn_Cari.Text = "&Cari"
		Me.Btn_Cari.UseVisualStyleBackColor = False
		'
		'Txt_Filter
		'
		Me.Txt_Filter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Filter.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Filter.Location = New System.Drawing.Point(262, 79)
		Me.Txt_Filter.Name = "Txt_Filter"
		Me.Txt_Filter.Size = New System.Drawing.Size(290, 20)
		Me.Txt_Filter.TabIndex = 2
		'
		'Dtp_2
		'
		Me.Dtp_2.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Dtp_2.Location = New System.Drawing.Point(388, 21)
		Me.Dtp_2.Name = "Dtp_2"
		Me.Dtp_2.Size = New System.Drawing.Size(247, 20)
		Me.Dtp_2.TabIndex = 1
		'
		'Dtp_1
		'
		Me.Dtp_1.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Dtp_1.Location = New System.Drawing.Point(90, 21)
		Me.Dtp_1.Name = "Dtp_1"
		Me.Dtp_1.Size = New System.Drawing.Size(247, 20)
		Me.Dtp_1.TabIndex = 0
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(345, 23)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(30, 17)
		Me.Label2.TabIndex = 0
		Me.Label2.Text = "S/D"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label4.Location = New System.Drawing.Point(12, 79)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(39, 17)
		Me.Label4.TabIndex = 0
		Me.Label4.Text = "Filter"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label1.Location = New System.Drawing.Point(12, 23)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(51, 17)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Tanggal"
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(22, 200)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(1087, 12)
		Me.Panel6.TabIndex = 490
		Me.Panel6.Visible = False
		'
		'Btn_Simpan
		'
		Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans Medium", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan.Location = New System.Drawing.Point(20, 563)
		Me.Btn_Simpan.Name = "Btn_Simpan"
		Me.Btn_Simpan.Size = New System.Drawing.Size(103, 35)
		Me.Btn_Simpan.TabIndex = 3
		Me.Btn_Simpan.Text = "&Simpan"
		Me.Btn_Simpan.UseVisualStyleBackColor = False
		'
		'Btn_Refresh
		'
		Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans Medium", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Refresh.Location = New System.Drawing.Point(126, 563)
		Me.Btn_Refresh.Name = "Btn_Refresh"
		Me.Btn_Refresh.Size = New System.Drawing.Size(103, 35)
		Me.Btn_Refresh.TabIndex = 4
		Me.Btn_Refresh.Text = "&Refresh"
		Me.Btn_Refresh.UseVisualStyleBackColor = False
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.Red
		Me.Panel8.Location = New System.Drawing.Point(21, 552)
		Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Size = New System.Drawing.Size(1087, 12)
		Me.Panel8.TabIndex = 490
		Me.Panel8.Visible = False
		'
		'Txt_No_Transaksi
		'
		Me.Txt_No_Transaksi.BackColor = System.Drawing.Color.Goldenrod
		Me.Txt_No_Transaksi.Enabled = False
		Me.Txt_No_Transaksi.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Txt_No_Transaksi.Location = New System.Drawing.Point(18, 56)
		Me.Txt_No_Transaksi.MaxLength = 50
		Me.Txt_No_Transaksi.Name = "Txt_No_Transaksi"
		Me.Txt_No_Transaksi.Size = New System.Drawing.Size(231, 22)
		Me.Txt_No_Transaksi.TabIndex = 493
		'
		'Panel10
		'
		Me.Panel10.BackColor = System.Drawing.Color.Red
		Me.Panel10.Location = New System.Drawing.Point(20, 79)
		Me.Panel10.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel10.Name = "Panel10"
		Me.Panel10.Size = New System.Drawing.Size(1087, 12)
		Me.Panel10.TabIndex = 490
		Me.Panel10.Visible = False
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label5.Location = New System.Drawing.Point(26, 528)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(73, 17)
		Me.Label5.TabIndex = 0
		Me.Label5.Text = "Keterangan"
		'
		'Txt_Keterangan
		'
		Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Keterangan.Location = New System.Drawing.Point(148, 526)
		Me.Txt_Keterangan.MaxLength = 100
		Me.Txt_Keterangan.Name = "Txt_Keterangan"
		Me.Txt_Keterangan.Size = New System.Drawing.Size(541, 20)
		Me.Txt_Keterangan.TabIndex = 2
		'
		'Panel11
		'
		Me.Panel11.BackColor = System.Drawing.Color.Red
		Me.Panel11.Location = New System.Drawing.Point(23, 484)
		Me.Panel11.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel11.Name = "Panel11"
		Me.Panel11.Size = New System.Drawing.Size(1087, 12)
		Me.Panel11.TabIndex = 490
		Me.Panel11.Visible = False
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label6.Location = New System.Drawing.Point(26, 497)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(86, 17)
		Me.Label6.TabIndex = 0
		Me.Label6.Text = "Lokasi Tujuan"
		'
		'Cmb_Gudang_Tujuan
		'
		Me.Cmb_Gudang_Tujuan.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Gudang_Tujuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Gudang_Tujuan.FormattingEnabled = True
		Me.Cmb_Gudang_Tujuan.Location = New System.Drawing.Point(148, 496)
		Me.Cmb_Gudang_Tujuan.Name = "Cmb_Gudang_Tujuan"
		Me.Cmb_Gudang_Tujuan.Size = New System.Drawing.Size(209, 24)
		Me.Cmb_Gudang_Tujuan.TabIndex = 494
		'
		'Cmb_Lokasi
		'
		Me.Cmb_Lokasi.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Lokasi.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Lokasi.FormattingEnabled = True
		Me.Cmb_Lokasi.Location = New System.Drawing.Point(90, 47)
		Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
		Me.Cmb_Lokasi.Size = New System.Drawing.Size(166, 24)
		Me.Cmb_Lokasi.TabIndex = 6
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label7.Location = New System.Drawing.Point(12, 50)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(45, 17)
		Me.Label7.TabIndex = 5
		Me.Label7.Text = "Lokasi"
		'
		'N_EMI_Transaksi_Waste_Product_Transfer
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1183, 611)
		Me.Controls.Add(Me.Cmb_Gudang_Tujuan)
		Me.Controls.Add(Me.Txt_No_Transaksi)
		Me.Controls.Add(Me.Txt_Keterangan)
		Me.Controls.Add(Me.Btn_Refresh)
		Me.Controls.Add(Me.Btn_Simpan)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.Lv_Data)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel11)
		Me.Controls.Add(Me.Panel8)
		Me.Controls.Add(Me.Panel10)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_Transaksi_Waste_Product_Transfer"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel3.ResumeLayout(False)
		Me.Panel4.ResumeLayout(False)
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Label3 As Label
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel9 As Panel
	Friend WithEvents Panel7 As Panel
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Lv_Data As ListView
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Txt_Filter As TextBox
	Friend WithEvents Dtp_2 As DateTimePicker
	Friend WithEvents Dtp_1 As DateTimePicker
	Friend WithEvents Label2 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents Btn_Cari As Button
	Friend WithEvents Panel6 As Panel
	Friend WithEvents Btn_Simpan As Button
	Friend WithEvents Btn_Refresh As Button
	Friend WithEvents Panel8 As Panel
	Friend WithEvents Txt_No_Transaksi As TextBox
	Friend WithEvents Panel10 As Panel
	Friend WithEvents Label5 As Label
	Friend WithEvents Txt_Keterangan As TextBox
	Friend WithEvents Panel11 As Panel
	Friend WithEvents Label6 As Label
	Friend WithEvents Cmb_Gudang_Tujuan As ComboBox
	Friend WithEvents Cmb_Filter As ComboBox
	Friend WithEvents Cmb_Lokasi As ComboBox
	Friend WithEvents Label7 As Label
End Class
