<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_Transfer_Quality_QC
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
		Me.Label11 = New System.Windows.Forms.Label()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.BtnCetak = New System.Windows.Forms.Button()
		Me.BtnExit = New System.Windows.Forms.Button()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Cmb_Lain = New System.Windows.Forms.ComboBox()
		Me.Cmb_Kualitas_Akhir = New System.Windows.Forms.ComboBox()
		Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
		Me.Cmb_Kualitas_Awal = New System.Windows.Forms.ComboBox()
		Me.Txt_NoFaktur = New System.Windows.Forms.TextBox()
		Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
		Me.Txt_Lain = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
		Me.Lv_NoFaktur = New System.Windows.Forms.ListView()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Lv_Barang = New System.Windows.Forms.ListView()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Panel1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Label11)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(635, 45)
		Me.Panel1.TabIndex = 91
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label11.Location = New System.Drawing.Point(20, 8)
		Me.Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(256, 29)
		Me.Label11.TabIndex = 0
		Me.Label11.Text = "Laporan Transfer Quality"
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(0, 44)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(942, 12)
		Me.Panel5.TabIndex = 92
		Me.Panel5.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 64)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 601)
		Me.Panel3.TabIndex = 93
		Me.Panel3.Visible = False
		'
		'BtnCetak
		'
		Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
		Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.BtnCetak.ForeColor = System.Drawing.Color.White
		Me.BtnCetak.Location = New System.Drawing.Point(426, 271)
		Me.BtnCetak.Name = "BtnCetak"
		Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
		Me.BtnCetak.TabIndex = 1
		Me.BtnCetak.Text = "&Cetak"
		Me.BtnCetak.UseVisualStyleBackColor = False
		'
		'BtnExit
		'
		Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
		Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.BtnExit.ForeColor = System.Drawing.Color.White
		Me.BtnExit.Location = New System.Drawing.Point(515, 271)
		Me.BtnExit.Name = "BtnExit"
		Me.BtnExit.Size = New System.Drawing.Size(84, 33)
		Me.BtnExit.TabIndex = 2
		Me.BtnExit.Text = "&Keluar"
		Me.BtnExit.UseVisualStyleBackColor = False
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Txt_Keterangan)
		Me.GroupBox1.Controls.Add(Me.Label7)
		Me.GroupBox1.Controls.Add(Me.Cmb_Lain)
		Me.GroupBox1.Controls.Add(Me.Cmb_Kualitas_Akhir)
		Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi)
		Me.GroupBox1.Controls.Add(Me.Cmb_Kualitas_Awal)
		Me.GroupBox1.Controls.Add(Me.Txt_NoFaktur)
		Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Controls.Add(Me.Label8)
		Me.GroupBox1.Controls.Add(Me.Label5)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Label6)
		Me.GroupBox1.Controls.Add(Me.Tgl2)
		Me.GroupBox1.Controls.Add(Me.Label3)
		Me.GroupBox1.Controls.Add(Me.Tgl1)
		Me.GroupBox1.Controls.Add(Me.Txt_Lain)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
		Me.GroupBox1.Location = New System.Drawing.Point(20, 58)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(595, 207)
		Me.GroupBox1.TabIndex = 0
		Me.GroupBox1.TabStop = False
		'
		'Txt_Keterangan
		'
		Me.Txt_Keterangan.Enabled = False
		Me.Txt_Keterangan.Location = New System.Drawing.Point(260, 110)
		Me.Txt_Keterangan.Name = "Txt_Keterangan"
		Me.Txt_Keterangan.Size = New System.Drawing.Size(319, 20)
		Me.Txt_Keterangan.TabIndex = 8
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Font = New System.Drawing.Font("Work Sans SemiBold", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Label7.Location = New System.Drawing.Point(266, 52)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(22, 17)
		Me.Label7.TabIndex = 7
		Me.Label7.Text = "=>"
		'
		'Cmb_Lain
		'
		Me.Cmb_Lain.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Lain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Lain.FormattingEnabled = True
		Me.Cmb_Lain.Location = New System.Drawing.Point(93, 162)
		Me.Cmb_Lain.Name = "Cmb_Lain"
		Me.Cmb_Lain.Size = New System.Drawing.Size(163, 24)
		Me.Cmb_Lain.TabIndex = 8
		'
		'Cmb_Kualitas_Akhir
		'
		Me.Cmb_Kualitas_Akhir.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Kualitas_Akhir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Kualitas_Akhir.FormattingEnabled = True
		Me.Cmb_Kualitas_Akhir.Location = New System.Drawing.Point(304, 49)
		Me.Cmb_Kualitas_Akhir.Name = "Cmb_Kualitas_Akhir"
		Me.Cmb_Kualitas_Akhir.Size = New System.Drawing.Size(163, 24)
		Me.Cmb_Kualitas_Akhir.TabIndex = 3
		'
		'Cmb_Lokasi
		'
		Me.Cmb_Lokasi.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Lokasi.FormattingEnabled = True
		Me.Cmb_Lokasi.Location = New System.Drawing.Point(93, 79)
		Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
		Me.Cmb_Lokasi.Size = New System.Drawing.Size(163, 24)
		Me.Cmb_Lokasi.TabIndex = 4
		'
		'Cmb_Kualitas_Awal
		'
		Me.Cmb_Kualitas_Awal.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Kualitas_Awal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Kualitas_Awal.FormattingEnabled = True
		Me.Cmb_Kualitas_Awal.Location = New System.Drawing.Point(93, 49)
		Me.Cmb_Kualitas_Awal.Name = "Cmb_Kualitas_Awal"
		Me.Cmb_Kualitas_Awal.Size = New System.Drawing.Size(163, 24)
		Me.Cmb_Kualitas_Awal.TabIndex = 2
		'
		'Txt_NoFaktur
		'
		Me.Txt_NoFaktur.Location = New System.Drawing.Point(93, 109)
		Me.Txt_NoFaktur.Name = "Txt_NoFaktur"
		Me.Txt_NoFaktur.Size = New System.Drawing.Size(163, 20)
		Me.Txt_NoFaktur.TabIndex = 5
		'
		'Txt_KdBarang
		'
		Me.Txt_KdBarang.Location = New System.Drawing.Point(93, 136)
		Me.Txt_KdBarang.Name = "Txt_KdBarang"
		Me.Txt_KdBarang.Size = New System.Drawing.Size(163, 20)
		Me.Txt_KdBarang.TabIndex = 6
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(8, 112)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(41, 16)
		Me.Label1.TabIndex = 4
		Me.Label1.Text = "Faktur"
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(8, 82)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(42, 16)
		Me.Label8.TabIndex = 4
		Me.Label8.Text = "Lokasi"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(8, 169)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(49, 16)
		Me.Label5.TabIndex = 4
		Me.Label5.Text = "Lainnya"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(8, 52)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(50, 16)
		Me.Label4.TabIndex = 4
		Me.Label4.Text = "Kualitas"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(8, 139)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(43, 16)
		Me.Label6.TabIndex = 4
		Me.Label6.Text = "Barang"
		'
		'Tgl2
		'
		Me.Tgl2.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Tgl2.CustomFormat = "dd MMMM yyyy"
		Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Tgl2.Location = New System.Drawing.Point(304, 23)
		Me.Tgl2.Name = "Tgl2"
		Me.Tgl2.Size = New System.Drawing.Size(163, 20)
		Me.Tgl2.TabIndex = 1
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(266, 25)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(25, 16)
		Me.Label3.TabIndex = 2
		Me.Label3.Text = "s/d"
		'
		'Tgl1
		'
		Me.Tgl1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Tgl1.CustomFormat = "dd MMMM yyyy"
		Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Tgl1.Location = New System.Drawing.Point(93, 23)
		Me.Tgl1.Name = "Tgl1"
		Me.Tgl1.Size = New System.Drawing.Size(163, 20)
		Me.Tgl1.TabIndex = 0
		'
		'Txt_Lain
		'
		Me.Txt_Lain.Location = New System.Drawing.Point(260, 166)
		Me.Txt_Lain.Name = "Txt_Lain"
		Me.Txt_Lain.Size = New System.Drawing.Size(319, 20)
		Me.Txt_Lain.TabIndex = 9
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(8, 23)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(47, 16)
		Me.Label2.TabIndex = 0
		Me.Label2.Text = "Periode"
		'
		'Txt_NmBarang
		'
		Me.Txt_NmBarang.Location = New System.Drawing.Point(260, 136)
		Me.Txt_NmBarang.Name = "Txt_NmBarang"
		Me.Txt_NmBarang.Size = New System.Drawing.Size(319, 20)
		Me.Txt_NmBarang.TabIndex = 7
		'
		'Lv_NoFaktur
		'
		Me.Lv_NoFaktur.BackColor = System.Drawing.Color.White
		Me.Lv_NoFaktur.FullRowSelect = True
		Me.Lv_NoFaktur.GridLines = True
		Me.Lv_NoFaktur.HideSelection = False
		Me.Lv_NoFaktur.Location = New System.Drawing.Point(650, 189)
		Me.Lv_NoFaktur.Name = "Lv_NoFaktur"
		Me.Lv_NoFaktur.Size = New System.Drawing.Size(486, 200)
		Me.Lv_NoFaktur.TabIndex = 99
		Me.Lv_NoFaktur.UseCompatibleStateImageBehavior = False
		Me.Lv_NoFaktur.View = System.Windows.Forms.View.Details
		Me.Lv_NoFaktur.Visible = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(616, 57)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(19, 601)
		Me.Panel2.TabIndex = 93
		Me.Panel2.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(20, 305)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(942, 15)
		Me.Panel4.TabIndex = 92
		Me.Panel4.Visible = False
		'
		'Lv_Barang
		'
		Me.Lv_Barang.BackColor = System.Drawing.Color.White
		Me.Lv_Barang.FullRowSelect = True
		Me.Lv_Barang.GridLines = True
		Me.Lv_Barang.HideSelection = False
		Me.Lv_Barang.Location = New System.Drawing.Point(650, 216)
		Me.Lv_Barang.Name = "Lv_Barang"
		Me.Lv_Barang.Size = New System.Drawing.Size(486, 200)
		Me.Lv_Barang.TabIndex = 100
		Me.Lv_Barang.UseCompatibleStateImageBehavior = False
		Me.Lv_Barang.View = System.Windows.Forms.View.Details
		Me.Lv_Barang.Visible = False
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
		Me.PanelGradient1.Size = New System.Drawing.Size(635, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'N_EMI_Laporan_Transfer_Quality_QC
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(635, 320)
		Me.Controls.Add(Me.Lv_Barang)
		Me.Controls.Add(Me.Lv_NoFaktur)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel1)
		Me.Controls.Add(Me.BtnCetak)
		Me.Controls.Add(Me.BtnExit)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.Name = "N_EMI_Laporan_Transfer_Quality_QC"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Label11 As Label
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Panel3 As Panel
	Friend WithEvents BtnCetak As Button
	Friend WithEvents BtnExit As Button
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Cmb_Lain As ComboBox
	Friend WithEvents Cmb_Kualitas_Awal As ComboBox
	Friend WithEvents Txt_NoFaktur As TextBox
	Friend WithEvents Txt_KdBarang As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Label5 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents Label6 As Label
	Friend WithEvents Tgl2 As DateTimePicker
	Friend WithEvents Label3 As Label
	Friend WithEvents Tgl1 As DateTimePicker
	Friend WithEvents Txt_Lain As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents Txt_NmBarang As TextBox
	Friend WithEvents Label7 As Label
	Friend WithEvents Cmb_Kualitas_Akhir As ComboBox
	Friend WithEvents Txt_Keterangan As TextBox
	Friend WithEvents Cmb_Lokasi As ComboBox
	Friend WithEvents Label8 As Label
	Friend WithEvents Lv_NoFaktur As ListView
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Lv_Barang As ListView
End Class
